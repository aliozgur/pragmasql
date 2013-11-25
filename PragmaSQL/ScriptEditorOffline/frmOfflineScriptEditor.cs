using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Drawing.Printing;

using WeifenLuo.WinFormsUI;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using PragmaSQL;
using PragmaSQL.Common;

using PragmaSQL.Database;
using Crad.Windows.Forms.Actions;
using AsynchronousCodeBlocks;

namespace PragmaSQL
{
  
  public partial class frmOfflineScriptEditor : DockContent,IScriptEditor
  {

    private TextEditorControl _textEditor = null;
    private CodeCompletionPresenterEx _codeCompWindowEx;

    private bool _isInitializing = false;

    private frmSearchAndReplace _frmSearchAndReplace = null;
    private frmGoToLine _frmGoToLine = null;

    private string _caption = String.Empty;

    private ActionList _actionList = new ActionList();


    #region Properties
    
    private bool _checkSave = true;
    public bool CheckSave
    {
      get { return _checkSave; }
      set { _checkSave = value; }
    }

    private IContentProvider _contentProvider = new DefaultContentProvider();
    public IContentProvider ContentProvider
    {
      get { return _contentProvider; }
      set { _contentProvider = value; }
    }
    
    private string _scriptText = String.Empty;
    public string ScriptText
    {
      get { return _scriptText; }
      set
      {
        _scriptText = value;
        _textEditor.Text = value;
      }
    }

    private int _objectType = DBObjectType.None;
    public int ObjectType
    {
      get { return _objectType; }
      set { _objectType = value; }
    }

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

    private bool _scriptModified = false;
    public bool ScriptModified
    {
      get
      {
        return _scriptModified;
      }
      set
      {
        _scriptModified = value;
        if (value)
        {
          this.Text = "* " + _caption;
          this.TabText = this.Text;
        }
        else
        {
          this.Text = _caption;
          this.TabText = this.Text;
        }
      }
    }

    private DateTime? _lastModifiedOn = null;
    public DateTime? LastModifiedOn
    {
      get
      {
        return _lastModifiedOn;
      }
    }

    public TextEditorControl TextEditor
    {
      get
      {
        return _textEditor;
      }
    }

    public string ContentInfo
    {
      get
      {
        return statContentName.Text;
      }
      set
      {
        statContentName.Text = value;
      }
    }

    public bool SharedScriptOperationsVisible
    {
      get
      {
        return mnuItemSharedScriptOperations.Visible;
      }
      set
      {
        mnuItemSharedScriptOperations.Visible = value;
      }
    }
    #endregion

    #region Constructor

    public frmOfflineScriptEditor( )
    {
      InitializeComponent();
    }

    #endregion

    #region Initialization

    private void InitializeForm( )
    {
      InitializeTextEditor();
      SharedScriptOperationsVisible = ConfigurationLoader.CurrentConfig.PragmaSql_SharedScriptsEnabled;
      InitializeCodeCompletionWindow_Ex();
      InitiailizeActions();
    }

    public void InitializeScriptEditor( string caption, string script, int objType)
    {
      try
      {
        _caption = caption;
        this.Text = _caption;
        this.TabText = _caption;

        InitializeForm();

        _isInitializing = true;


        _scriptText = script;
        _objectType = objType;
        _textEditor.Text = _scriptText;
      }
      finally
      {
        _isInitializing = false;
      }
    }

    #endregion //Initialization

    #region Text editor related

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
        return ActiveTextArea.Document;
      }
    }

    private string GetPreviousNonWSLineParts
    {
      get
      {
        int caretPos = ActiveTextArea.Caret.Offset;
        if (caretPos == 0)
        {
          return string.Empty;
        }

        int offset = caretPos - 1;

        while (offset >= 0 && !Char.IsWhiteSpace(ActiveDocument.GetCharAt(offset)))
        {
          offset--;
        }

        if (offset == -1)
        {
          return ActiveDocument.GetText(0, caretPos);
        }
        else
        {
          return ActiveDocument.GetText(offset + 1, caretPos - (offset + 1));
        }
      }
    }

    private void InitializeTextEditor( )
    {
      if (_textEditor != null)
      {
        return;
      }


      _textEditor = new TextEditorControl();
      panEditor.Controls.Add(_textEditor);
      _textEditor.Dock = DockStyle.Fill;
      _textEditor.BringToFront();
      ConfigurationLoader.CurrentConfig.TextEditorOptions.ApplyToControl(_textEditor);
      _textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
      _textEditor.ContextMenuStrip = popUpEditor;
      ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextEditorKeyDown);
      _textEditor.Document.DocumentChanged += new DocumentEventHandler(OnDocumentChanged);
      _textEditor.Focus();
    }



   
    private void OnDocumentChanged( object sender, DocumentEventArgs e )
    {
      if (e.Document.UndoStack.CanUndo)
      {
        ScriptModified = true;
        _lastModifiedOn = DateTime.Now;
      }
      else
      {
        ScriptModified = false;
        _lastModifiedOn = null;
      }
    }

    private void OnTextEditorKeyDown(object sender, KeyEventArgs e)
    { 
      if (e.KeyCode == Keys.J && e.Control)
      {
        e.SuppressKeyPress = true;
        ShowSelector_Ex();
      }
    }



    private void MoveCaretToEOL( )
    {
      ActiveTextArea.Caret.Column = TextUtilities.GetLineAsString(ActiveDocument, ActiveTextArea.Caret.Line).Length;
    }


    public int DeleteWordBeforeCaret( )
    {
      int start = TextUtilities.FindPrevWordStart(ActiveDocument, ActiveTextArea.Caret.Offset);

      string partToRemove = ActiveDocument.GetText(start, ActiveTextArea.Caret.Offset - start);
      if (partToRemove != "." && partToRemove != "..")
      {
        ActiveDocument.Remove(start, ActiveTextArea.Caret.Offset - start);
        ActiveTextArea.Caret.Column = start;
      }
      return start;
    }

    private Point GetCaretPosition( )
    {
      TextArea textArea = ActiveTextArea;
      Point caretPos = textArea.Caret.Position;

      int xpos = textArea.TextView.GetDrawingXPos(caretPos.Y, caretPos.X);
      int rulerHeight = textArea.TextEditorProperties.ShowHorizontalRuler ? textArea.TextView.FontHeight : 0;
      Point pos = new Point(textArea.TextView.DrawingPosition.X + xpos,
                            textArea.TextView.DrawingPosition.Y + (textArea.Document.GetVisibleLine(caretPos.Y)) * textArea.TextView.FontHeight - textArea.TextView.TextArea.VirtualTop.Y + textArea.TextView.FontHeight + rulerHeight);

      Point location = _textEditor.ActiveTextAreaControl.PointToScreen(pos);
      return location;
    }

    public void ChangeScriptCase( TokenConversionType conversionType )
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

    public void ConvertTokensTo( TokenConversionType conversionType )
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
    #endregion //Text editor related

    #region Script I/O

    public bool OpenScript( string fileName )
    {
      if(_contentProvider == null)
      {
        return false;
      }

      if(_contentProvider.GetType() != typeof(DefaultContentProvider))
      {
        _contentProvider = new DefaultContentProvider();
      }

      bool result = _contentProvider.LoadContent(fileName,_textEditor);
      statContentName.Text = _contentProvider.ContentName;
      Caption = _contentProvider.Hint;
      ScriptModified = false;
      
      return result;
    }

    public void SaveScriptAs( )
    {
      if(_contentProvider == null)
      {
        return;
      }
      
      if(_contentProvider.GetType() != typeof(DefaultContentProvider))
      {
        _contentProvider = new DefaultContentProvider();
      }

      if( _contentProvider.SaveContentAs(_caption,_textEditor))
      {
        statContentName.Text = _contentProvider.ContentName;
        Caption = _contentProvider.Hint;
        ScriptModified = false;
      }
    }

    public void SaveScript( )
    {
      if(_contentProvider == null)
      {
        return;
      }

      _contentProvider.SaveContent(_caption,_textEditor);      
      statContentName.Text = _contentProvider.ContentName;
      this.TabText = _contentProvider.Hint;
      this.Text = this.TabText;
      ScriptModified = false;
    }

    #endregion //Script I/O

    #region Utilities   
    public int LineCount( string value )
    {
      string[] lines = value.Replace("\n\r", "\r").Split('\r');
      int len = lines.Length;
      if (len > 0)
      {
        return len - 1;
      }
      else
      {
        return len;
      }
    }

    public string WordAtCursor
    {
      get
      {
        return TextUtilities.GetWordAt(ActiveDocument, ActiveTextArea.Caret.Offset);
      }
    }
    #endregion //Utilities

    #region GoTo Line

    public void ShowGoToLineDialog( )
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

    private void OnGoToFormClosed( object sender, FormClosedEventArgs e )
    {
      _frmGoToLine = null;
    }

    private void OnGoToLine( object sender, int lineNo )
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

    #region Search And Replace

    private int MatchNext( string matchText )
    {
      if (String.IsNullOrEmpty(matchText))
      {
        return -1;
      }

      int lineNo = ActiveTextArea.Caret.Line;
      int colNo = ActiveTextArea.Caret.Column;
      int totalNumOfLines = ActiveDocument.TotalNumberOfLines;
      string LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
      LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

      int indexOf = LineText.IndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
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
          LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
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
        MessageBox.Show("Reached end of document", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      return indexOf;
    }

    private int MatchPrev( string matchText )
    {
      if (String.IsNullOrEmpty(matchText))
      {
        return -1;
      }

      int lineNo = ActiveTextArea.Caret.Line;
      int colNo = ActiveTextArea.Caret.Column;

      string LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
      LineText = LineText.Substring(0, ActiveTextArea.Caret.Column);

      int indexOf = LineText.LastIndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
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
          LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);

          indexOf = LineText.LastIndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
        }
        while (indexOf < 0 && lineNo >= 0);
      }

      if (indexOf > 0)
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
        MessageBox.Show("Reached start of document", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      return indexOf;
    }

    public void ShowSearchDialog( )
    {
      if (_frmSearchAndReplace == null)
      {
        _frmSearchAndReplace = new frmSearchAndReplace();
        _frmSearchAndReplace.FormClosed += new FormClosedEventHandler(OnSarchAndReplaceFormClosed);
        _frmSearchAndReplace.SearchRequested += new SearchEventHandler(OnSearchRequested);
        _frmSearchAndReplace.ReplaceRequested += new ReplaceEventHandler(OnReplaceRequested);
      }
      _frmSearchAndReplace.ValidateMode(SearchAndRelaceDialogMode.Search);
      _frmSearchAndReplace.Show();
      _frmSearchAndReplace.TopMost = true;
    }

    public void ShowReplaceDialog( )
    {
      if (_frmSearchAndReplace == null)
      {
        _frmSearchAndReplace = new frmSearchAndReplace();
        _frmSearchAndReplace.FormClosed += new FormClosedEventHandler(OnSarchAndReplaceFormClosed);
        _frmSearchAndReplace.SearchRequested += new SearchEventHandler(OnSearchRequested);
        _frmSearchAndReplace.ReplaceRequested += new ReplaceEventHandler(OnReplaceRequested);
      }
      _frmSearchAndReplace.ValidateMode(SearchAndRelaceDialogMode.Replace);
      _frmSearchAndReplace.Show();
      _frmSearchAndReplace.TopMost = true;
    }



    private void OnSarchAndReplaceFormClosed( object sender, FormClosedEventArgs e )
    {
      _frmSearchAndReplace = null;
    }

    private void OnSearchRequested( object sender, SearchEventArgs e )
    {
      if (e.SearchRegularExpression == null)
      {
        return;
      }

      PerformSearch(e.SearchRegularExpression);
    }

    private void OnReplaceRequested( object sender, ReplaceEventArgs e )
    {
      if (e.SearchRegularExpression == null)
      {
        return;
      }

      PerformReplace(e.SearchRegularExpression, e.ReplaceText, e.IsReplaceAll);
    }

    private void PerformSearch( Regex regularExpression )
    {
      if (regularExpression == null)
      {
        return;
      }

      int lineNo = ActiveTextArea.Caret.Line;
      int colNo = ActiveTextArea.Caret.Column;
      int totalNumOfLines = ActiveDocument.TotalNumberOfLines;
      string LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
      LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

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
          LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
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
        MessageBox.Show("Reached end of document", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

    }

    private void PerformReplace( Regex regularExpression, string replaceText, bool replaceAll )
    {
      if (regularExpression == null)
      {
        return;
      }

      bool replaceAllConfirmed = false;
      bool matchExist = false;

      int replaceCnt = 0;

      int lineNo = ActiveTextArea.Caret.Line;
      int colNo = ActiveTextArea.Caret.Column;
      int matchIndex = 1;

      string LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
      LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

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

            Point startPoint = ActiveTextArea.Caret.Position;
            startPoint.X = m.Index + offset;
            Point endPoint = startPoint;

            endPoint.X = startPoint.X + m.Length;
            ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);

            DialogResult dlgRes = MessageBox.Show("Replace selected text?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                DialogResult dlgRes = MessageBox.Show("Replace all occurances ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

              LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
              LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - endPoint.X);

              replaceCnt++;

              m = regularExpression.Match(LineText);
              matchIndex = m.Index + endPoint.X;

              LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
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
        LineText = TextUtilities.GetLineAsString(ActiveDocument, lineNo);
        offset = 0;
      }
      while (lineNo < ActiveDocument.TotalNumberOfLines);

      if (replaceCnt > 0)
      {
        MessageBox.Show(replaceCnt.ToString() + " ocurrence(s) replaced.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else if (!matchExist)
      {
        MessageBox.Show("Nothing found to be replaced.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
    #endregion

    #region Code Completion
    private void InitializeCodeCompletionWindow_Ex()
    {
      if (_codeCompWindowEx != null)
      {
        return;
      }

      _codeCompWindowEx = new CodeCompletionPresenterEx();
      RegisterForCodeCompletionEventsEx();
    }

    private void ShowSelector_Ex()
    {
      Point final = GetCaretPosition();
      _codeCompWindowEx.JumpTo(GetPreviousNonWSLineParts);
      _codeCompWindowEx.SetLocation(final.X + 10, final.Y - 10);
      _codeCompWindowEx.ShowSelector();

    }

    public void RefreshCodeCompletionLists()
    {
      if (_codeCompWindowEx == null)
      {
        return;
      }

      _codeCompWindowEx.RefreshCodeCompletionLists();
    }

    private void RegisterForCodeCompletionEventsEx()
    {
      if (_codeCompWindowEx == null)
      {
        return;
      }

      _codeCompWindowEx.KeyPressed += new KeyPressHandler(HandleCodeCompletionKeyPress_Ex);
      _codeCompWindowEx.UserMadeFinalSelection += new NotificationHandler(HandleCodeCompletionSelection_Ex);
      _codeCompWindowEx.UserPressedBackSpace += new NotificationHandler(HandleCodeCompletionBackSpace_Ex);
    }

    private void HandleCodeCompletionKeyPress_Ex(char c)
    {
      switch (c)
      {
        case (char)27: //ESC
          _codeCompWindowEx.DismissSelector();
          _textEditor.Focus();
          return;
        default:
          if (Char.IsControl(c))
          {
            return;
          }
          ActiveTextArea.InsertChar(c);
          _codeCompWindowEx.JumpTo(GetPreviousNonWSLineParts);
          break;
      }
    }

    private void HandleCodeCompletionSelection_Ex()
    {
      if (_codeCompWindowEx.Selector.HasMultipleSelection)
      {
        ActiveTextArea.InsertString(_codeCompWindowEx.SelectedItemsAsCommaSeparatedString);
      }
      else
      {
        DeleteWordBeforeCaret();
        ActiveTextArea.InsertString(_codeCompWindowEx.SelectedItem);
      }
      _codeCompWindowEx.DismissSelector();
      _textEditor.Focus();
    }

    private void HandleCodeCompletionBackSpace_Ex()
    {
      new ICSharpCode.TextEditor.Actions.Backspace().Execute(ActiveTextArea);
      _codeCompWindowEx.JumpTo(GetPreviousNonWSLineParts);
    }

    #endregion
    
    private void btnToggleBlockComment_Click( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.ToggleBlockComment().Execute(ActiveTextArea);
    }

    private void btnToggleLineComment_Click( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.ToggleLineComment().Execute(ActiveTextArea);
    }



    private void edtMatchText_KeyDown( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Enter)
      {
        MatchNext(edtMatchText.Text);
      }
    }



    private void frmScriptEditor_Leave( object sender, EventArgs e )
    {
      if (_frmSearchAndReplace != null)
      {
        _frmSearchAndReplace.Hide();
      }
    }
   
    private void frmScriptEditor_FormClosing( object sender, FormClosingEventArgs e )
    {

      if (!CheckSave || !ScriptModified)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Save changes to \"" + _caption + "\"", "Save Script", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
      if (dlgRes == DialogResult.No)
      {
        e.Cancel = false;
      }
      else if (dlgRes == DialogResult.Yes)
      {
        SaveScript();
        e.Cancel = false;
      }
      else if (dlgRes == DialogResult.Cancel)
      {
        e.Cancel = true;
      }
    }

    private void edtMatchText_KeyDown_1( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Enter)
      {
        MatchNext(edtMatchText.Text);
      }
    }

    private void openSharedScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmSharedScriptSelectDialog.OpenSharedScript(this, null);
    }

    private void saveAsSharedScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmSharedScriptSelectDialog.SaveAsSharedScript(this);
    }

   
  }
}