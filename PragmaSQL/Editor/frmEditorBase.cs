using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.Core;
using ICSharpCode.TextEditor.Util;
using PragmaSQL.Proxy;
using PragmaSQL.Core;

namespace PragmaSQL
{
    public partial class frmEditorBase : DockContent, ITextEditor
    {
        #region FileChangeInfo Class

        protected class FileChangeInfo
        {
            public enum FileChangeType
            {
                None,
                Change,
                Rename,
                Delete
            }

            public string FullPath = String.Empty;
            public string NewFullPath = String.Empty;
            public FileChangeType ChangeType = FileChangeType.None;

            public void Reset()
            {
                this.ChangeType = FileChangeType.None;
                this.FullPath = String.Empty;
                this.NewFullPath = String.Empty;
            }
        }

        #endregion //FileChangeInfo Class

        #region Fields And Properties

        public delegate string GetDocumentContent();
        public GetDocumentContent GetDocumentContentDelegate;

        protected FileChangeInfo _fileChangeInfo = new FileChangeInfo();
        protected TextEditorControl _textEditor = null;

        private IList<string> _syntaxModes = new List<string>();

        protected string _fileName = String.Empty;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                if (ContentPersister != null)
                    ContentPersister.FilePath = FileName;
                WatchFile(value);
            }
        }

        protected IList<string> SyntaxModes
        {
            get { return new List<string>(_syntaxModes).AsReadOnly(); }
        }

        public TextArea ActiveTextArea
        {
            get
            {
                if (_textEditor == null)
                {
                    return null;
                }
                else
                {
                    return _textEditor.ActiveTextAreaControl.TextArea;
                }
            }
        }

        public IDocument ActiveDocument
        {
            get
            {
                if (ActiveTextArea == null)
                    return null;

                return ActiveTextArea.Document;
            }
        }


        private ToolStripTextBox _searchToolStripTextBox;
        protected ToolStripTextBox SearchToolStripTextBox
        {
            get { return _searchToolStripTextBox; }
            set { _searchToolStripTextBox = value; }
        }

        private frmGoToLine _frmGoToLine = null;

        public string SelectedTextOrWordAtCursor
        {
            get
            {
                string keyword = ActiveTextArea.SelectionManager.SelectedText;
                if (String.IsNullOrEmpty(keyword))
                    keyword = WordAtCursor;
                return keyword;
            }
        }

        protected ToolStripItem ContentModifiedIndicatorToolStripItem = null;
        protected bool _contentModified = false;
        public virtual bool ContentModified
        {
            get
            {
                return _contentModified;
            }
            set
            {
                _contentModified = value;
                if (ContentModifiedIndicatorToolStripItem != null)
                    ContentModifiedIndicatorToolStripItem.Visible = value;
                //if (value)
                //{
                //  this.Text = "* " + _caption;
                //  this.TabText = this.Text;
                //}
                //else
                //{
                //  this.Text = _caption;
                //  this.TabText = this.Text;
                //}
                BuildTitle();
            }
        }

        private bool _isRecoveredContent = false;
        public virtual bool IsRecoveredContent
        {
            get
            {
                return _isRecoveredContent;
            }
            set
            {
                _isRecoveredContent = value;
                BuildTitle();
            }
        }

        protected ToolStripItem ContentInfoToolStripItem = null;

        protected string _contentnInfo = String.Empty;
        public string ContentInfo
        {
            get
            {
                return _contentnInfo;
            }
            set
            {
                _contentnInfo = value;
                if (ContentInfoToolStripItem != null)
                    ContentInfoToolStripItem.Text = _contentnInfo;
            }
        }


        protected IContentPersister _contentPersister = new DefaultContentPersister();
        public IContentPersister ContentPersister
        {
            get { return _contentPersister; }
            set { _contentPersister = value; }
        }

        protected DateTime? _lastModifiedOn = null;
        public DateTime? LastModifiedOn
        {
            get
            {
                return _lastModifiedOn;
            }
        }

        private bool _checkSave = true;
        public bool CheckSave
        {
            get { return _checkSave; }
            set { _checkSave = value; }
        }

        public TextEditorControl TextEditor
        {
            get
            {
                return _textEditor;
            }
        }

        private bool _canWatchFiles = true;
        public bool CanWatchFiles
        {
            get { return _canWatchFiles; }
        }

        private string _currentSytaxMode = "Default";
        public string CurrentSytaxMode
        {
            get { return _currentSytaxMode; }
            protected set { _currentSytaxMode = value; }
        }



        #endregion //Fields And Properties

        #region CTOR

        public frmEditorBase()
        {
            InitializeComponent();
            InitializeTextEditor();
            GetDocumentContentDelegate = new GetDocumentContent(GetDocumentContentMethod);
        }

        #endregion //CTOR

        #region Initialization

        protected virtual void InitializeTextEditor()
        {
            if (_textEditor != null)
            {
                return;
            }

            _textEditor = new TextEditorControl();
            panEditor.Controls.Add(_textEditor);
            _textEditor.Dock = DockStyle.Fill;
            _textEditor.BringToFront();


            //_textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");

            _textEditor.GotFocus += new EventHandler(OnProcessFileChange);

            ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(ActiveTextArea_KeyDown);
            ActiveTextArea.TextEditorProperties.EnableFolding = false;
            ActiveTextArea.TextEditorProperties.AllowCaretBeyondEOL = true;
            ActiveTextArea.DoubleClick += new EventHandler(ActiveTextArea_DoubleClick);
            ActiveTextArea.Click += new EventHandler(ActiveTextArea_Click);
            ActiveTextArea.KeyPress += new KeyPressEventHandler(ActiveTextArea_KeyPress);
            if (ConfigHelper.Current != null && ConfigHelper.Current.TextEditorOptions != null)
                ActiveTextArea.MotherTextEditorControl.Encoding = ConfigHelper.Current.TextEditorOptions.Encoding;
            else
                ActiveTextArea.MotherTextEditorControl.Encoding = Encoding.Default;

            RetrieveSyntaxModes();
            _textEditor.Focus();
        }

        void ActiveTextArea_KeyDown(object sender, KeyEventArgs e)
        {

        }

        void ActiveTextArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClearDblClickTextMarkers(true);
        }



        void ActiveTextArea_Click(object sender, EventArgs e)
        {
            ClearDblClickTextMarkers(true);
        }

        void ActiveTextArea_DoubleClick(object sender, EventArgs e)
        {
            if (!ActiveTextArea.SelectionManager.HasSomethingSelected)
                return;

            string matchText = ActiveTextArea.SelectionManager.SelectedText;
            if (String.IsNullOrEmpty(matchText) || matchText.Length == 1)
                return;

            HighlightMatchingWords(matchText);
        }

        void OnProcessFileChange(object sender, EventArgs e)
        {
            ProcessFileChange();
        }

        private void RetrieveSyntaxModes()
        {
            _syntaxModes.Clear();
            IEnumerable keys = ICSharpCode.TextEditor.Document.HighlightingManager.Manager.HighlightingDefinitions.Keys;

            foreach (string name in keys)
            {
                _syntaxModes.Add(name);
            }
        }

        #endregion //Initialization


        #region Search And Replace

        protected int MatchNext(string matchText)
        {
            if (String.IsNullOrEmpty(matchText))
            {
                return -1;
            }

            int indexOf = -1;

            try
            {
                ActiveTextArea.BeginUpdate();
                int lineNo = ActiveTextArea.Caret.Line;
                if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                    ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

                int colNo = ActiveTextArea.Caret.Column;
                int totalNumOfLines = ActiveDocument.TotalNumberOfLines;
                string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                if (colNo >= LineText.Length)
                {
                    LineText = String.Empty;
                }
                else
                {
                    LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);
                }

                indexOf = LineText.IndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
                int offset = colNo;

                if (indexOf < 0)
                {
                    offset = 0;
                    do
                    {
                        int tmpLineNo = ActiveDocument.GetNextVisibleLineAbove(lineNo, 1);
                        if (tmpLineNo == lineNo)
                        {
                            break;
                        }
                        lineNo = tmpLineNo;
                        if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                            ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));
                        LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                        indexOf = LineText.IndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
                    }
                    while (indexOf < 0 && lineNo < totalNumOfLines);
                }

                if (indexOf >= 0)
                {
                    ActiveTextArea.Caret.Column = 0;
                    ActiveTextArea.Caret.Line = lineNo;

                    Point startPoint = ActiveTextArea.Caret.Position;
                    startPoint.X = indexOf + offset;
                    Point endPoint = startPoint;
                    endPoint.X = endPoint.X + matchText.Length;
                    ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
                    ActiveTextArea.Caret.Column = endPoint.X;
                }
                else if (lineNo == totalNumOfLines - 1)
                {
                    if (Utils.AskYesNoQuestion(String.Format(Properties.Resources.StartSearchReplaceFromBeginning, "search"), MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ActiveTextArea.Caret.Line = 0;
                        ActiveTextArea.Caret.Column = 0;
                        ActiveTextArea.SelectionManager.ClearSelection();
                    }
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
            return indexOf;
        }

        protected int MatchPrev(string matchText)
        {
            if (String.IsNullOrEmpty(matchText))
            {
                return -1;
            }

            int indexOf = -1;
            try
            {
                ActiveTextArea.BeginUpdate();

                int lineNo = ActiveTextArea.Caret.Line;
                if (ActiveDocument.FoldingManager.IsFoldEnd(lineNo))
                    ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithEnd(lineNo));
                int colNo = ActiveTextArea.Caret.Column;
                string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                LineText = LineText.Substring(0, colNo > LineText.Length ? LineText.Length : ActiveTextArea.Caret.Column);
                indexOf = LineText.LastIndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
                if (indexOf < 0)
                {
                    do
                    {
                        int tmpLineNo = ActiveDocument.GetNextVisibleLineBelow(lineNo, 1);
                        if (tmpLineNo == lineNo)
                        {
                            break;
                        }
                        lineNo = tmpLineNo;
                        if (ActiveDocument.FoldingManager.IsFoldEnd(lineNo))
                            ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithEnd(lineNo));

                        LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);

                        indexOf = LineText.LastIndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
                    }
                    while (indexOf < 0 && lineNo >= 0);
                }

                if (indexOf >= 0)
                {
                    ActiveTextArea.Caret.Column = 0;
                    ActiveTextArea.Caret.Line = lineNo;

                    Point startPoint = ActiveTextArea.Caret.Position;
                    startPoint.X = indexOf;
                    Point endPoint = startPoint;
                    endPoint.X = endPoint.X + matchText.Length;
                    ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
                    ActiveTextArea.Caret.Column = startPoint.X;
                }
                else if (lineNo == 0)
                {
                    int maxLineNo = ActiveDocument.TotalNumberOfLines - 1;
                    if (Utils.AskYesNoQuestion(String.Format(Properties.Resources.StartSearchReplaceFromEnd, "search"), MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ActiveTextArea.Caret.Line = maxLineNo;
                        ActiveTextArea.Caret.Column = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, maxLineNo).Length;
                        ActiveTextArea.SelectionManager.ClearSelection();
                    }

                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
            return indexOf;
        }

        protected void ShowSearchDialog()
        {
            SearchAndReplaceForm.ShowSearchAndReplaceForm(SearchAndReplaceDialogMode.Search);
            SearchAndReplaceForm.RegisterToEvents(this, OnSearchRequested, OnReplaceRequested, OnSarchAndReplaceFormClosed);

            if (ActiveTextArea.SelectionManager.HasSomethingSelected)
            {
                string selText = ActiveTextArea.SelectionManager.SelectedText;
                if (SearchToolStripTextBox != null)
                {
                    SearchToolStripTextBox.Text = selText;
                }
                SearchAndReplaceForm.InitialSearchText = selText;
            }
        }

        protected void ShowReplaceDialog()
        {
            SearchAndReplaceForm.ShowSearchAndReplaceForm(SearchAndReplaceDialogMode.Replace);
            SearchAndReplaceForm.RegisterToEvents(this, OnSearchRequested, OnReplaceRequested, OnSarchAndReplaceFormClosed);

            if (ActiveTextArea.SelectionManager.HasSomethingSelected)
            {
                string selText = ActiveTextArea.SelectionManager.SelectedText;
                if (SearchToolStripTextBox != null)
                {
                    SearchToolStripTextBox.Text = selText;
                }
                SearchAndReplaceForm.InitialSearchText = selText;
            }
        }

        protected void OnSarchAndReplaceFormClosed(object sender, FormClosedEventArgs e)
        {
            if (SearchToolStripTextBox != null)
            {
                SearchToolStripTextBox.Text = SearchAndReplaceForm.CurrentSearchText;
                if (String.IsNullOrEmpty(Program.MainForm.SearchTerm) && !String.IsNullOrEmpty(SearchAndReplaceForm.CurrentSearchText))
                    Program.MainForm.SearchTerm = SearchAndReplaceForm.CurrentSearchText;

            }
            SearchAndReplaceForm.UnRegisterFromEvents(OnSearchRequested, OnReplaceRequested, OnSarchAndReplaceFormClosed);
        }

        protected void OnSearchRequested(object sender, SearchEventArgs e)
        {
            if (e.SearchRegularExpression == null)
            {
                return;
            }

            PerformSearch(e.SearchRegularExpression);
        }

        protected void OnReplaceRequested(object sender, ReplaceEventArgs e)
        {
            if (e.SearchRegularExpression == null)
            {
                return;
            }

            PerformReplace(e.SearchRegularExpression, e.ReplaceText, e.IsReplaceAll);
        }

        protected void PerformSearch(Regex regularExpression)
        {
            if (regularExpression == null)
            {
                return;
            }

            int lineNo = ActiveTextArea.Caret.Line;
            if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));


            int colNo = ActiveTextArea.Caret.Column;
            int totalNumOfLines = ActiveDocument.TotalNumberOfLines;
            string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
            if (colNo >= LineText.Length)
            {
                LineText = String.Empty;
            }
            else
            {
                LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);
            }

            Match m = regularExpression.Match(LineText);
            int offset = colNo;

            if (!m.Success)
            {
                offset = 0;
                do
                {
                    int tmpLineNo = ActiveDocument.GetNextVisibleLineAbove(lineNo, 1);
                    if (tmpLineNo == lineNo)
                    {
                        break;
                    }
                    lineNo = tmpLineNo;
                    if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                        ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

                    LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                    m = regularExpression.Match(LineText);
                }
                while (!m.Success && lineNo < totalNumOfLines);
            }

            if (m.Success)
            {
                ActiveTextArea.Caret.Column = 0;
                ActiveTextArea.Caret.Line = lineNo;

                Point startPoint = ActiveTextArea.Caret.Position;
                startPoint.X = m.Index + offset;
                Point endPoint = startPoint;
                endPoint.X = endPoint.X + m.Length;
                ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
                ActiveTextArea.Caret.Column = endPoint.X;
            }
            else if (lineNo == totalNumOfLines - 1)
            {
                if (Utils.AskYesNoQuestion(String.Format(Properties.Resources.StartSearchReplaceFromBeginning, "search"), MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ActiveTextArea.Caret.Line = 0;
                    ActiveTextArea.Caret.Column = 0;
                    ActiveTextArea.SelectionManager.ClearSelection();
                }
            }

        }

        protected void PerformReplace_All(Regex regularExpression, string replaceText)
        {
            if (regularExpression == null)
                return;

            if (ActiveDocument.FoldingManager.IsFoldStart(ActiveTextArea.Caret.Line))
                ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(ActiveTextArea.Caret.Line));

            string txt = String.Empty;
            string result = String.Empty;
            int offset = ActiveTextArea.Caret.Offset;

            // Check if there is a match
            if (ActiveTextArea.SelectionManager.HasSomethingSelected)
                txt = ActiveTextArea.SelectionManager.SelectedText;
            else
                txt = ActiveDocument.TextContent.Substring(offset, ActiveDocument.TextContent.Length - offset);

            int matchCnt = regularExpression.Matches(txt).Count;
            if (matchCnt == 0)
            {
                // No match exit
                Utils.ShowWarning("Nothing found to be replaced.", MessageBoxButtons.OK);
                return;
            }


            DialogResult dlgRes = Utils.AskYesNoQuestion("Replace all occurances ?", MessageBoxDefaultButton.Button2);
            if (dlgRes == DialogResult.No)
                return;


            if (ActiveTextArea.SelectionManager.HasSomethingSelected)
            {
                //Perform replace for each selection
                foreach (ISelection s in ActiveTextArea.SelectionManager.SelectionCollection)
                {
                    txt = ActiveDocument.TextContent.Substring(s.Offset, s.SelectedText.Length);
                    result = regularExpression.Replace(txt, replaceText);
                    ActiveDocument.Replace(s.Offset, txt.Length, result);
                }
            }
            else
            {
                // Perform replace for all
                txt = ActiveDocument.TextContent.Substring(offset, ActiveDocument.TextContent.Length - offset);
                result = regularExpression.Replace(txt, replaceText);
                ActiveDocument.Replace(offset, txt.Length, result);
            }

            Utils.ShowInfo(String.Format("Replaced {0} occurences of the searched text.", matchCnt), MessageBoxButtons.OK);
        }

        protected void PerformReplace_Single(Regex regularExpression, string replaceText)
        {
            if (regularExpression == null)
                return;

            if (ActiveDocument.FoldingManager.IsFoldStart(ActiveTextArea.Caret.Line))
                ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(ActiveTextArea.Caret.Line));

            int lineNo = ActiveTextArea.Caret.Line;
            if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

            bool matchExist = false;
            int colNo = ActiveTextArea.Caret.Column;
            int matchIndex = 1;

            string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
            if (colNo >= LineText.Length)
                LineText = String.Empty;
            else
                LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

            try
            {
                ActiveTextArea.BeginUpdate();
                int offset = colNo;
                Match m = null;
                do
                {
                    m = regularExpression.Match(LineText);
                    matchIndex = m.Index;

                    if (m.Success)
                    {
                        ActiveTextArea.Caret.Column = 0;
                        ActiveTextArea.Caret.Line = lineNo;

                        lineNo = ActiveTextArea.Caret.Line;
                        if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                            ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

                        Point startPoint = ActiveTextArea.Caret.Position;
                        startPoint.X = m.Index + offset;
                        Point endPoint = startPoint;

                        endPoint.X = startPoint.X + m.Length;
                        ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
                        ActiveTextArea.Invalidate();

                        DialogResult dlgRes = Utils.AskYesNoQuestion("Replace selected text?", MessageBoxDefaultButton.Button2);
                        if (dlgRes == DialogResult.Yes)
                        {
                            ActiveTextArea.Caret.Line = lineNo;
                            ActiveTextArea.Caret.Column = startPoint.X;

                            ActiveDocument.Replace(ActiveTextArea.Caret.Offset, m.Length, replaceText);
                            endPoint.X = startPoint.X + replaceText.Length;
                            ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);

                            ActiveTextArea.Caret.Line = lineNo;
                            ActiveTextArea.Caret.Column = endPoint.X;
                        }

                        ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
                        ActiveTextArea.Caret.Column = endPoint.X;
                        matchExist = true;

                        break;


                    }


                    int tmpLineNo = ActiveDocument.GetNextVisibleLineAbove(lineNo, 1);
                    if (tmpLineNo == lineNo)
                    {
                        break;
                    }
                    lineNo = tmpLineNo;
                    if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                        ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

                    LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                    offset = 0;


                }
                while (lineNo < ActiveDocument.TotalNumberOfLines);
            }
            finally
            {
                ActiveTextArea.EndUpdate();
            }

            if (!matchExist)
            {
                Utils.ShowWarning("Nothing found to be replaced.", MessageBoxButtons.OK);
            }
        }

        protected void PerformReplace(Regex regularExpression, string replaceText, bool replaceAll)
        {

            if (replaceAll)
                PerformReplace_All(regularExpression, replaceText);
            else
                PerformReplace_Single(regularExpression, replaceText);


            /*
            bool replaceAllConfirmed = false;
            bool matchExist = false;

            int replaceCnt = 0;

            int lineNo = ActiveTextArea.Caret.Line;
            if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
              ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

            int colNo = ActiveTextArea.Caret.Column;
            int matchIndex = 1;

            string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
            if (colNo >= LineText.Length)
            {
              LineText = String.Empty;
            }
            else
            {
              LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);
            }

            try
            {
              ActiveTextArea.BeginUpdate();
              int offset = colNo;
              Match m = null;
              do
              {
                m = regularExpression.Match(LineText);
                matchIndex = m.Index;

                if (m.Success)
                {
                  if (!replaceAll)
                  {
                    ActiveTextArea.Caret.Column = 0;
                    ActiveTextArea.Caret.Line = lineNo;

                    lineNo = ActiveTextArea.Caret.Line;
                    if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                      ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

                    Point startPoint = ActiveTextArea.Caret.Position;
                    startPoint.X = m.Index + offset;
                    Point endPoint = startPoint;

                    endPoint.X = startPoint.X + m.Length;
                    ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
                    ActiveTextArea.Invalidate();

                    DialogResult dlgRes = Utils.AskYesNoQuestion("Replace selected text?", MessageBoxDefaultButton.Button2);
                    if (dlgRes == DialogResult.Yes)
                    {
                      ActiveTextArea.Caret.Line = lineNo;
                      ActiveTextArea.Caret.Column = startPoint.X;

                      ActiveDocument.Replace(ActiveTextArea.Caret.Offset, m.Length, replaceText);
                      endPoint.X = startPoint.X + replaceText.Length;
                      ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);

                      ActiveTextArea.Caret.Line = lineNo;
                      ActiveTextArea.Caret.Column = endPoint.X;
                    }

                    ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
                    ActiveTextArea.Caret.Column = endPoint.X;
                    matchExist = true;

                    break;
                  }
                  else
                  {
                    ActiveTextArea.Caret.Column = 0;
                    ActiveTextArea.Caret.Line = lineNo;

                    do
                    {
                      Point startPoint = ActiveTextArea.Caret.Position;
                      startPoint.X = matchIndex + offset;
                      Point endPoint = startPoint;

                      endPoint.X = startPoint.X + m.Length;
                      ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);

                      if (!replaceAllConfirmed)
                      {
                        DialogResult dlgRes = Utils.AskYesNoQuestion("Replace all occurances ?", MessageBoxDefaultButton.Button2);
                        if (dlgRes == DialogResult.No)
                        {
                          return;
                        }
                        replaceAllConfirmed = true;
                        matchExist = true;
                      }
                      ActiveTextArea.Caret.Line = lineNo;
                      ActiveTextArea.Caret.Column = startPoint.X;

                      ActiveDocument.Replace(ActiveTextArea.Caret.Offset, m.Length, replaceText);

                      endPoint.X = startPoint.X + replaceText.Length;
                      ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);

                      ActiveTextArea.Caret.Line = lineNo;
                      ActiveTextArea.Caret.Column = endPoint.X;

                      LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                      LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - endPoint.X);

                      replaceCnt++;

                      m = regularExpression.Match(LineText);
                      matchIndex = m.Index + endPoint.X;

                      LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                    }
                    while (m.Success);
                  }

                }


                int tmpLineNo = ActiveDocument.GetNextVisibleLineAbove(lineNo, 1);
                if (tmpLineNo == lineNo)
                {
                  break;
                }
                lineNo = tmpLineNo;
                if (ActiveDocument.FoldingManager.IsFoldStart(lineNo))
                  ExpandFoldings(ActiveDocument.FoldingManager.GetFoldedFoldingsWithStart(lineNo));

                LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
                offset = 0;


              }
              while (lineNo < ActiveDocument.TotalNumberOfLines);
            }
            finally
            {
              ActiveTextArea.EndUpdate();
            }

            if (replaceCnt > 0)
            {
              MessageBox.Show(replaceCnt.ToString() + " ocurrence(s) replaced.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!matchExist)
            {
              MessageBox.Show("Nothing found to be replaced.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
        }

        private void ClearDblClickTextMarkers(bool invalidate)
        {
            ActiveDocument.MarkerStrategy.RemoveAll(
              delegate(TextMarker tm)
              {
                  return true;
              }
              );
            if (invalidate)
                ActiveTextArea.Invalidate();
        }

        private void HighlightMatchingWords(string matchText)
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                ClearDblClickTextMarkers(false);
                int totalLineCnt = ActiveDocument.TotalNumberOfLines;
                string matchPattern = String.Format(@"\b{0}\b", Regex.Escape(matchText));
                string lineText = String.Empty;


                for (int i = 0; i < totalLineCnt; i++)
                {
                    lineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, i);

                    MatchCollection matches = Regex.Matches(lineText, matchPattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                    foreach (Match m in matches)
                    {
                        int offset = ActiveDocument.PositionToOffset(new Point(m.Index, i));
                        TextMarker tm = new TextMarker(offset, m.Length, TextMarkerType.SolidBlock, Color.LightGreen);

                        ActiveDocument.MarkerStrategy.AddMarker(tm);

                    }
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        #endregion //Search And Replace

        #region GoTo Line

        public void ShowGoToLineDialog()
        {
            if (_frmGoToLine == null)
            {
                _frmGoToLine = new frmGoToLine();
                _frmGoToLine.FormClosed += new FormClosedEventHandler(OnGoToFormClosed);
                _frmGoToLine.GoToLineRequested += new GoToLineEventHandler(OnGoToLine);
            }

            _frmGoToLine.MaxLineCnt = ActiveDocument.TotalNumberOfLines;
            _frmGoToLine.Show();
            _frmGoToLine.TopMost = true;
        }

        private void OnGoToFormClosed(object sender, FormClosedEventArgs e)
        {
            _frmGoToLine = null;
        }

        private void OnGoToLine(object sender, int lineNo)
        {
            if (lineNo <= 0 || lineNo > ActiveDocument.TotalNumberOfLines)
            {
                MessageBox.Show("Can not locate line in script!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ActiveTextArea.Caret.Line = lineNo - 1;
            ActiveTextArea.Caret.Column = 0;
            ActiveTextArea.Focus();
            _frmGoToLine.Hide();
        }

        #endregion //GoTo Line

        #region Folding Operations

        protected void ExpandFoldings(IList<FoldMarker> foldings)
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                foreach (FoldMarker marker in foldings)
                {
                    if (marker.IsFolded)
                        marker.IsFolded = false;
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        protected void ExpandAllFoldings()
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                foreach (FoldMarker marker in ActiveDocument.FoldingManager.FoldMarker)
                {
                    marker.IsFolded = false;
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        protected void ToggleFoldings()
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                foreach (FoldMarker marker in ActiveDocument.FoldingManager.FoldMarker)
                {
                    marker.IsFolded = !marker.IsFolded;
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        protected void CollapseAllFoldings()
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                foreach (FoldMarker marker in ActiveDocument.FoldingManager.FoldMarker)
                {
                    marker.IsFolded = true;
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        #endregion //Folding Operations

        #region Script Formatting
        protected void ChangeScriptCase(TokenConversionType conversionType)
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                HighlightRuleSet rules = ActiveDocument.HighlightingStrategy.GetRuleSet(null);
                IList<LineSegment> lines = ActiveDocument.LineSegmentCollection;
                for (int k = 0; k < lines.Count; k++)
                {
                    LineSegment segment = lines[k];
                    for (int i = 0; i < segment.Words.Count; i++)
                    {
                        TextWord word = segment.Words[i];
                        if (word.Type != TextWordType.Word)
                        {
                            continue;
                        }

                        string newVal = word.Word;
                        switch (conversionType)
                        {
                            case TokenConversionType.Lower:
                                newVal = word.Word.ToLowerInvariant();
                                break;
                            case TokenConversionType.Upper:
                                newVal = word.Word.ToUpperInvariant();
                                break;
                            default:
                                break;
                        }
                        ActiveDocument.Replace(segment.Offset + word.Offset, word.Length, newVal);
                    }
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
            }
        }

        protected void ConvertTokensTo(TokenConversionType conversionType)
        {
            try
            {
                ActiveTextArea.BeginUpdate();

                StringBuilder sb = null;
                HighlightRuleSet rules = ActiveDocument.HighlightingStrategy.GetRuleSet(null);
                IList<LineSegment> lines = ActiveDocument.LineSegmentCollection;
                for (int k = 0; k < lines.Count; k++)
                {
                    LineSegment segment = lines[k];
                    for (int i = 0; i < segment.Words.Count; i++)
                    {
                        TextWord word = segment.Words[i];
                        if (word.Type != TextWordType.Word)
                        {
                            continue;
                        }

                        if (rules.KeyWords[ActiveDocument, segment, word.Offset, word.Length] != null)
                        {
                            string newVal = word.Word;
                            switch (conversionType)
                            {
                                case TokenConversionType.Lower:
                                    newVal = word.Word.ToLowerInvariant();
                                    break;
                                case TokenConversionType.Upper:
                                    newVal = word.Word.ToUpperInvariant();
                                    break;
                                case TokenConversionType.Capitalize:
                                    newVal = word.Word;
                                    char[] chars = newVal.ToCharArray();
                                    chars[0] = Char.ToUpperInvariant(newVal[0]);
                                    sb = new StringBuilder();
                                    sb.Append(chars);
                                    newVal = sb.ToString();
                                    break;
                                default:
                                    break;
                            }
                            ActiveDocument.Replace(segment.Offset + word.Offset, word.Length, newVal);
                        }
                    }
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
            }

        }

        #endregion //Script Formatting

        #region Other

        protected void SendSelectedTextToTextDiff(bool isSource, string caption = "", string text="")
        {
            string script = !String.IsNullOrEmpty(text) ? text : ActiveTextArea.SelectionManager.HasSomethingSelected ? ActiveTextArea.SelectionManager.SelectedText : _textEditor.Text;


            frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
            if (diffForm == null)
            {
                diffForm = TextDiffFactory.CreateDiff();
            }

            //diffForm.diffControl.SourceHighlightingStrategy = _textEditor.Document.HighlightingStrategy;
            //diffForm.diffControl.DestHighlightingStrategy = _textEditor.Document.HighlightingStrategy;

            if (isSource)
            {
                diffForm.diffControl.SourceText = script;
                diffForm.diffControl.SourceHeaderText = !String.IsNullOrEmpty(caption) ? caption : Caption;
            }
            else
            {
                diffForm.diffControl.DestText = script;
                diffForm.diffControl.DestHeaderText = !String.IsNullOrEmpty(caption) ? caption : Caption;
            }
            diffForm.Show();
            diffForm.BringToFront();
        }

        public virtual void ApplyTextEditorOptionsFromCurrentConfig()
        {
            ApplyWatchFileConfig();
        }

        private void BuildTitle()
        {
            string title = String.Format("{0}{1}{2}"
              , _isRecoveredContent ? "[R] " : String.Empty
              , _contentModified ? "* " : String.Empty
              , _caption);

            this.Text = title;
            this.TabText = title;
        }

        #endregion //Other

        #region ITextEditor Members

        private bool _readOnly = false;
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                if (ActiveDocument == null)
                    throw new Exception("Active document is null.");
                _readOnly = value;
                ActiveDocument.ReadOnly = _readOnly;
            }
        }

        protected string _caption = String.Empty;
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                this.Text = value;
                this.TabText = value;
            }
        }

        private Guid _uid = Guid.NewGuid();
        public Guid Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public EditorContentType ContentType
        {
            get
            {
                return _contentPersister == null ? EditorContentType.Unknown : _contentPersister.ContentType;

            }
        }

        private void PositionCaretTo(CaretPosition pos)
        {
            ActiveTextArea.Caret.Line = pos.Line;
            ActiveTextArea.Caret.Column = pos.Col;
        }

        public virtual bool SaveContentToFile(string fileName)
        {
            _textEditor.SaveFile(fileName);
            _fileName = fileName;
            return true;
        }

        public virtual bool LoadContentFromFile(string fileName)
        {
            _textEditor.LoadFile(fileName, true, true);
            _fileName = fileName;
            return true;
        }

        public void InsertContent(CaretPosition startPos, string content)
        {
            PositionCaretTo(startPos);
            ActiveTextArea.InsertString(content);
        }

        public void InsertContent(string content)
        {
            ActiveTextArea.InsertString(content);
        }

        public void AppendContent(string content)
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                ActiveDocument.TextContent += content;
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        public void RemoveContent(CaretPosition startPos, CaretPosition endPos)
        {
            try
            {
                ActiveTextArea.BeginUpdate();

                ActiveTextArea.SelectionManager.SetSelection(startPos.ToPoint(), endPos.ToPoint());
                ActiveTextArea.SelectionManager.RemoveSelectedText();
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        public string GetContent(CaretPosition startPos, CaretPosition endPos)
        {
            string result = String.Empty;
            try
            {
                ActiveTextArea.BeginUpdate();

                ActiveTextArea.SelectionManager.SetSelection(startPos.ToPoint(), endPos.ToPoint());
                result = ActiveTextArea.SelectionManager.SelectedText;
                ActiveTextArea.SelectionManager.ClearSelection();
                return result;
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        public string GetDocumentContentMethod()
        {
            return ActiveDocument.TextContent;
        }

        public string Content
        {
            get
            {

                if (_textEditor.ActiveTextAreaControl.InvokeRequired)
                {
                    return _textEditor.Invoke(GetDocumentContentDelegate) as string;
                }
                else
                    return ActiveDocument.TextContent;
            }
            set
            {
                try
                {
                    ActiveTextArea.BeginUpdate();
                    ActiveDocument.TextContent = value;
                }
                finally
                {
                    ActiveTextArea.EndUpdate();
                    ActiveTextArea.Invalidate();
                }
            }
        }

        public void Select(CaretPosition startPos, CaretPosition endPos)
        {
            ActiveTextArea.SelectionManager.SetSelection(startPos.ToPoint(), endPos.ToPoint());
        }

        public void Select(int startOffset, int endOffset)
        {
            Point sPoint = ActiveDocument.OffsetToPosition(startOffset);
            Point ePoint = ActiveDocument.OffsetToPosition(endOffset);
            ActiveTextArea.SelectionManager.SetSelection(sPoint, ePoint);
        }

        public void ClearSelection()
        {
            ActiveTextArea.SelectionManager.ClearSelection();
        }

        public void DeleteSelection()
        {
            ActiveTextArea.SelectionManager.RemoveSelectedText();
        }

        public CaretPosition CaretPos
        {
            get
            {
                CaretPosition result = new CaretPosition();
                result.Line = ActiveTextArea.Caret.Line;
                result.Col = ActiveTextArea.Caret.Column;
                return result;
            }
            set
            {
                PositionCaretTo(value);
            }
        }

        public string SelectedText
        {
            get
            {
                return ActiveTextArea.SelectionManager.SelectedText;
            }
        }

        public string WordAtCursor
        {
            get
            {
                return SharpDevelopTextEditorUtilities.GetWordAt(ActiveDocument, ActiveTextArea.Caret.Offset - 1);
            }
        }

        public int NotificationStripItemCount
        {
            get { return tsNotifications.Items.Count; }
        }

        public virtual int AddInStripItemCount
        {
            get { throw new NotImplementedException(); }
        }

        public Cursor ActiveTextAreaCursor
        {
            get { return ActiveTextArea.TextView.Cursor; }
            set { ActiveTextArea.TextView.Cursor = value; }

        }

        public void AddItemToNotificationStrip(ToolStripItem item)
        {
            AddItemToNotificationStrip(item, -1);
        }

        public void AddItemToNotificationStrip(ToolStripItem item, int index)
        {
            if (item == null)
                return;

            if (tsNotifications.Items.Contains(item))
                return;

            if (index < 0)
                tsNotifications.Items.Add(item);
            else
                tsNotifications.Items.Insert(index, item);

            if (!tsNotifications.Visible)
                tsNotifications.Visible = true;
        }

        public void RemoveItemFromNotificationStrip(ToolStripItem item)
        {
            if (item == null)
                return;

            if (!tsNotifications.Items.Contains(item))
                return;

            tsNotifications.Items.Remove(item);
            if (tsNotifications.Items.Count == 0)
                tsNotifications.Visible = false;
        }

        public virtual void AddItemToAddInStrip(ToolStripItem item)
        {
            AddItemToAddInStrip(item, 0);
        }
        public virtual void AddItemToAddInStrip(ToolStripItem item, int index)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveItemFromAddInStrip(ToolStripItem item)
        {

        }

        protected event AfterOpenedFileDelegate _afterOpenedFile;
        public event AfterOpenedFileDelegate AfterOpenedFile
        {
            add { _afterOpenedFile += value; }
            remove { _afterOpenedFile -= value; }
        }

        protected event BeforeOpenedFileDelegate _beforeOpenedFile;
        public event BeforeOpenedFileDelegate BeforeOpenedFile
        {
            add { _beforeOpenedFile += value; }
            remove { _beforeOpenedFile -= value; }
        }

        protected event AfterSavedContentDelegate _afterSavedContent;
        public event AfterSavedContentDelegate AfterSavedContent
        {
            add { _afterSavedContent += value; }
            remove { _afterSavedContent -= value; }
        }

        protected event BeforeSavedContentDelegate _beforeSavedContent;
        public event BeforeSavedContentDelegate BeforeSavedContent
        {
            add { _beforeSavedContent += value; }
            remove { _beforeSavedContent -= value; }
        }

        protected event AfterSavedContentToFileDelegate _afterSaveContentToFile;
        public event AfterSavedContentToFileDelegate AfterSaveContentToFile
        {
            add { _afterSaveContentToFile += value; }
            remove { _afterSaveContentToFile -= value; }
        }

        protected event BeforeSavedContentToFileDelegate _beforeSaveContentToFile;
        public event BeforeSavedContentToFileDelegate BeforeSaveContentToFile
        {
            add { _beforeSaveContentToFile += value; }
            remove { _beforeSaveContentToFile -= value; }
        }

        protected event AfterCaretPositionChangedDelegate _afterCaretPositionChanged;
        public event AfterCaretPositionChangedDelegate AfterCaretPositionChanged
        {
            add { _afterCaretPositionChanged += value; }
            remove { _afterCaretPositionChanged -= value; }
        }

        protected event BeforeCaretPositionChangedDelegate _beforeCaretPositionChanged;
        public event BeforeCaretPositionChangedDelegate BeforeCaretPositionChanged
        {
            add { _beforeCaretPositionChanged += value; }
            remove { _beforeCaretPositionChanged -= value; }
        }

        protected event EventHandler _afterContentChanged;
        public event EventHandler AfterContentChanged
        {
            add { _afterContentChanged += value; }
            remove { _afterContentChanged -= value; }
        }

        private AfterCodeCompletionShowedDelegete _afterCodeCompletionShowed;
        public event AfterCodeCompletionShowedDelegete AfterCodeCompletionShowed
        {
            add { _afterCodeCompletionShowed += value; }
            remove { _afterCodeCompletionShowed -= value; }
        }

        private BeforeCodeCompletionShowedDelegete _beforeCodeCompletionShowed;
        public event BeforeCodeCompletionShowedDelegete BeforeCodeCompletionShowed
        {
            add { _beforeCodeCompletionShowed += value; }
            remove { _beforeCodeCompletionShowed -= value; }
        }

        private FormClosedEventHandler _editorClosed;
        public event FormClosedEventHandler EditorClosed
        {
            add { _editorClosed += value; }
            remove { _editorClosed -= value; }
        }

        private FormClosingEventHandler _editorClosing;
        public event FormClosingEventHandler EditorClosing
        {
            add { _editorClosing += value; }
            remove { _editorClosing -= value; }
        }

        public void RefreshSyntaxHighlighter()
        {
            _textEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(CurrentSytaxMode);
        }

        public void ShowInfo(string infoText)
        {
            infoHeader.Text = infoText;
            if (!infoHeader.Visible)
                infoHeader.Visible = true;

            infoHeader.SendToBack();
        }

        #endregion

        #region Watch For File Changes
        public virtual void WatchFile(string fileName)
        {
            if (!_canWatchFiles)
                return;

            if (fsWatcher.EnableRaisingEvents)
                fsWatcher.EnableRaisingEvents = false;

            if (String.IsNullOrEmpty(fileName))
                return;

            FileInfo fi = new FileInfo(fileName);
            fsWatcher.Path = fi.DirectoryName;
            fsWatcher.Filter = fi.Name;
            fsWatcher.IncludeSubdirectories = false;
            fsWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
            fsWatcher.EnableRaisingEvents = true;
        }

        public virtual void WatchFile()
        {
            WatchFile(_fileName);
        }

        public virtual void StopFileWatcher()
        {
            if (fsWatcher.EnableRaisingEvents)
                fsWatcher.EnableRaisingEvents = false;
        }

        private void ProcessFileChange()
        {
            if (!_canWatchFiles || String.IsNullOrEmpty(_fileChangeInfo.FullPath) || _fileChangeInfo.ChangeType == FileChangeInfo.FileChangeType.None)
                return;

            try
            {
                _textEditor.GotFocus -= new EventHandler(OnProcessFileChange);
                switch (_fileChangeInfo.ChangeType)
                {
                    case FileChangeInfo.FileChangeType.Change:
                        if (MessageService.AskQuestionFormatted("PragmaSQL File Change Notification", Properties.Resources.FileWatch_Changed, _fileChangeInfo.FullPath))
                        {
                            ContentModified = false;
                            _textEditor.LoadFile(_fileChangeInfo.FullPath);
                        }
                        else
                            ContentModified = true;

                        break;
                    case FileChangeInfo.FileChangeType.Delete:
                        StopFileWatcher();
                        if (!MessageService.AskQuestionFormatted("PragmaSQL File Delete Notification", Properties.Resources.FileWatch_Deleted, _fileChangeInfo.FullPath))
                        {
                            ContentModified = false;
                            this.Close();
                        }
                        else
                        {
                            FileName = String.Empty;
                            ContentInfo = String.Empty;
                            ContentModified = true;
                        }
                        break;
                    case FileChangeInfo.FileChangeType.Rename:
                        StopFileWatcher();

                        DialogResult dlgRes = MessageBox.Show(String.Format(Properties.Resources.FileWatch_Renamed, _fileChangeInfo.FullPath), "PragmaSQL File Rename Notification", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (dlgRes == DialogResult.No)
                        {
                            ContentModified = false;
                            this.Close();
                        }
                        else if (dlgRes == DialogResult.Cancel)
                        {
                            ContentInfo = String.Empty;
                            FileName = String.Empty;
                            ContentModified = true;
                        }
                        else if (dlgRes == DialogResult.Yes)
                        {
                            FileInfo fi = new FileInfo(_fileChangeInfo.NewFullPath);
                            Caption = fi.Name;
                            ContentInfo = _fileChangeInfo.NewFullPath;
                            FileName = _fileChangeInfo.NewFullPath;
                            _textEditor.LoadFile(_fileChangeInfo.NewFullPath);
                        }
                        break;
                    default:
                        break;
                }
                _fileChangeInfo.ChangeType = FileChangeInfo.FileChangeType.None;
            }
            finally
            {
                _textEditor.GotFocus += new EventHandler(OnProcessFileChange);
            }
        }

        protected void ApplyWatchFileConfig()
        {
            bool configValue = true;
            if (ConfigHelper.Current != null && ConfigHelper.Current.TextEditorOptions != null)
                configValue = ConfigHelper.Current.TextEditorOptions.WatchOpenedFiles;

            if (configValue == _canWatchFiles)
                return;

            _canWatchFiles = configValue;

            if (!_canWatchFiles)
            {
                StopFileWatcher();
                _fileChangeInfo.Reset();
            }
            else
            {
                WatchFile();
            }
        }

        #endregion //Watch For File Changes

        #region Fire Events
        protected void FireAfterCaretPositionChanged()
        {
            if (_afterCaretPositionChanged == null)
            {
                return;

            }

            Delegate[] delegates = _afterCaretPositionChanged.GetInvocationList();
            foreach (AfterCaretPositionChangedDelegate del in delegates)
            {
                try
                {
                    del.Invoke(this, this.CaretPos);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireBeforeCaretPositionChanged()
        {

            if (_beforeCaretPositionChanged == null)
            {
                return;
            }

            Delegate[] delegates = _beforeCaretPositionChanged.GetInvocationList();
            foreach (BeforeCaretPositionChangedDelegate del in delegates)
            {
                try
                {
                    del.Invoke(this);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireAfterContentChanged()
        {
            if (_afterContentChanged == null)
            {
                return;
            }

            Delegate[] delegates = _afterContentChanged.GetInvocationList();
            foreach (EventHandler del in delegates)
            {
                try
                {
                    del.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireAfterOpenedFile(string fileName)
        {

            if (_afterOpenedFile == null)
            {
                return;

            }

            Delegate[] delegates = _afterOpenedFile.GetInvocationList();
            foreach (AfterOpenedFileDelegate del in delegates)
            {
                try
                {
                    del.Invoke(this, fileName);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireBeforeOpenedFile(string fileName)
        {
            if (_beforeOpenedFile == null)
            {
                return;
            }

            Delegate[] delegates = _beforeOpenedFile.GetInvocationList();
            foreach (BeforeOpenedFileDelegate del in delegates)
            {
                try
                {
                    BeforeOpenedFileEventArgs args = new BeforeOpenedFileEventArgs();
                    args.FileName = fileName;
                    del.Invoke(this, args);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireAfterSavedContent(string fileName)
        {
            if (_afterSavedContent == null)
            {
                return;
            }

            Delegate[] delegates = _afterSavedContent.GetInvocationList();
            foreach (AfterSavedContentDelegate del in delegates)
            {
                try
                {
                    del.Invoke(this, fileName);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireBeforeSavedContent()
        {
            if (_beforeSavedContent == null)
            {
                return;
            }

            Delegate[] delegates = _beforeSavedContent.GetInvocationList();
            foreach (BeforeSavedContentDelegate del in delegates)
            {
                try
                {
                    del.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireAfterSaveContentToFile(string fileName)
        {
            if (_afterSaveContentToFile == null)
            {
                return;
            }

            Delegate[] delegates = _afterSaveContentToFile.GetInvocationList();
            foreach (AfterSavedContentToFileDelegate del in delegates)
            {
                try
                {
                    del.Invoke(this, fileName);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireBeforeSaveContentToFile(string fileName)
        {
            if (_beforeSaveContentToFile == null)
            {
                return;
            }

            Delegate[] delegates = _beforeSaveContentToFile.GetInvocationList();
            foreach (BeforeSavedContentToFileDelegate del in delegates)
            {
                try
                {
                    FileOperationEventArgs args = new FileOperationEventArgs();
                    args.FileName = fileName;
                    del.Invoke(this, args);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireBeforeCodeCompletionShowed(CodeCompletionType type, string requestedFor)
        {
            if (_beforeCodeCompletionShowed == null)
            {
                return;
            }

            Delegate[] delegates = _beforeCodeCompletionShowed.GetInvocationList();
            foreach (BeforeCodeCompletionShowedDelegete del in delegates)
            {
                try
                {
                    BeforeCodeCompletionShowedEventArgs args = new BeforeCodeCompletionShowedEventArgs();
                    args.Type = type;
                    args.RequestedFor = requestedFor;
                    del.Invoke(this, args);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }


        }

        protected void FireAfterCodeCompletionShowed(CodeCompletionType type, string userSelection, bool userMadeSelection)
        {
            if (_afterCodeCompletionShowed == null)
            {
                return;
            }

            Delegate[] delegates = _afterCodeCompletionShowed.GetInvocationList();
            foreach (AfterCodeCompletionShowedDelegete del in delegates)
            {
                try
                {
                    AfterCodeCompletionShowedEventArgs args = new AfterCodeCompletionShowedEventArgs();
                    args.Type = type;
                    args.UserSelection = userSelection;
                    args.UserMadeSelection = userMadeSelection;
                    del.Invoke(this, args);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected void FireTextEditorClosed(FormClosedEventArgs e)
        {
            if (_editorClosed == null)
            {
                return;
            }

            Delegate[] delegates = _editorClosed.GetInvocationList();
            foreach (FormClosedEventHandler del in delegates)
            {
                try
                {
                    del.Invoke(this, e);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        protected bool FireTextEditorClosing(FormClosingEventArgs e)
        {
            if (_editorClosing == null)
            {
                return false;
            }

            bool cancel = false;
            Delegate[] delegates = _editorClosing.GetInvocationList();
            foreach (FormClosingEventHandler del in delegates)
            {
                try
                {
                    FormClosingEventArgs args = new FormClosingEventArgs(e.CloseReason, false);
                    del.Invoke(this, args);
                    cancel = args.Cancel || cancel;
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }

            return cancel;
        }
        #endregion //Fire Events

        private void frmEditorBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                RecoverContent.RemoveContent(this);
            }
            catch { }

            SearchAndReplaceForm.UnRegisterFromEvents(OnSearchRequested, OnReplaceRequested, OnSarchAndReplaceFormClosed);
            FireTextEditorClosed(e);
            Program.MainForm.HostSvc.FireTextEditorClosed(this, e);
        }

        private void frmEditorBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FireTextEditorClosing(e);
            e.Cancel = Program.MainForm.HostSvc.FireTextEditorClosing(this, e);
        }

        private void frmEditorBase_Enter(object sender, EventArgs e)
        {
            SearchAndReplaceForm.RegisterToEvents(this, OnSearchRequested, OnReplaceRequested, OnSarchAndReplaceFormClosed);
        }

        private void frmEditorBase_Leave(object sender, EventArgs e)
        {
            SearchAndReplaceForm.UnRegisterFromEvents(OnSearchRequested, OnReplaceRequested, OnSarchAndReplaceFormClosed);
        }

        private void fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _fileChangeInfo.FullPath = e.FullPath;
            _fileChangeInfo.NewFullPath = String.Empty;
            _fileChangeInfo.ChangeType = FileChangeInfo.FileChangeType.Change;
        }

        private void fsWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _fileChangeInfo.FullPath = e.FullPath;
            _fileChangeInfo.NewFullPath = String.Empty;
            _fileChangeInfo.ChangeType = FileChangeInfo.FileChangeType.Delete;
        }

        private void fsWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            _fileChangeInfo.FullPath = e.OldFullPath;
            _fileChangeInfo.NewFullPath = e.FullPath;

            _fileChangeInfo.ChangeType = FileChangeInfo.FileChangeType.Rename;
        }

        private void OnCloseInfoHeader(object sender, EventArgs e)
        {
            infoHeader.Visible = false;
        }


    }
}