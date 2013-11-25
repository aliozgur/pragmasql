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
using Crad.Windows.Forms.Actions;
using AsynchronousCodeBlocks;

using PragmaSQL;
using PragmaSQL.Common;
using PragmaSQL.Database;
using PragmaSQL.Interfaces;

namespace PragmaSQL
{

  public partial class frmTextEditor : DockContent, IPragmaEditor,ITextEditor
  {
    #region Fields And Properties
    private TextEditorControl _textEditor = null;
    private CodeCompletionPresenterEx _codeCompWindowEx;
    private frmSearchAndReplace _frmSearchAndReplace = null;
    private frmGoToLine _frmGoToLine = null;
    private ActionList _actionList = new ActionList();

    
    private bool _initializing = false;
    public bool Initializing
    {
      get { return _initializing; }
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

    private int _ObjectType = DBObjectType.None;
    public int ObjectType
    {
      get { return _ObjectType; }
      set { _ObjectType = value; }
    }

    #endregion //Fields And Properties

    #region Constructor

    public frmTextEditor( )
    {
      InitializeComponent();
    }

    #endregion //Constructor

    #region Initialization

    private void InitializeForm( )
    {
      InitializeTextEditor();
      SharedScriptOperationsVisible = ConfigurationHelper.Current.PragmaSql_SharedScriptsEnabled;
      InitializeCodeCompletionWindow_Ex();
      InitiailizeActions();
    }

    public void InitializeScriptEditor( string caption, string script, int objType )
    {
      try
      {
        _caption = caption;
        this.Text = _caption;
        this.TabText = _caption;

        InitializeForm();

        _initializing = true;


        _scriptText = script;
        _ObjectType = objType;
        _textEditor.Text = _scriptText;
      }
      finally
      {
        _initializing = false;
      }
    }

    #endregion //Initialization

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
      ConfigurationHelper.Current.TextEditorOptions.ApplyToControl(_textEditor);
      _textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
      _textEditor.ContextMenuStrip = popUpEditor;
      _textEditor.Document.DocumentChanged += new DocumentEventHandler(OnDocumentChanged);
      
      ActiveTextArea.TextEditorProperties.EnableFolding = false;
      ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextEditorKeyDown);
      ActiveTextArea.Caret.PositionChanged += new EventHandler(OnCaretPositionChanged);

      _textEditor.Focus();
    }

    private void OnCaretPositionChanged( object sender, EventArgs e )
    {
      statCaretPos.Text = "Ln: " + (ActiveTextArea.Caret.Line + 1).ToString()
        + ", Col: " + (ActiveTextArea.Caret.Column + 1).ToString()
        + " ( Offset: " + ActiveTextArea.Caret.Offset.ToString() + " )";

      if (_afterCaretPositionChanged != null)
      {
        _afterCaretPositionChanged(this, this.CaretPos);
      }
    }

    private void OnDocumentChanged( object sender, DocumentEventArgs e )
    {
      if (e.Document.UndoStack.CanUndo)
      {
        ContentModified = true;
        _lastModifiedOn = DateTime.Now;
        if (_afterContentChanged != null)
        {
          _afterContentChanged(this, EventArgs.Empty);
        }

      }
      else
      {
        ContentModified = false;
        _lastModifiedOn = null;
      }
    }

    private void OnTextEditorKeyDown( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.J && e.Control)
      {
        e.SuppressKeyPress = true;
        ShowSelector_Ex();
      }
    }

    private void MoveCaretToEOL( )
    {
      ActiveTextArea.Caret.Column = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, ActiveTextArea.Caret.Line).Length;
    }

    public int DeleteWordBeforeCaret( )
    {
      int start = SharpDevelopTextEditorUtilities.FindPrevWordStart(ActiveDocument, ActiveTextArea.Caret.Offset);

      string partToRemove = ActiveDocument.GetText(start, ActiveTextArea.Caret.Offset - start);
      if (partToRemove != "." && partToRemove != "..")
      {
        ActiveDocument.Remove(start, partToRemove.Length);// ActiveTextArea.Caret.Offset - start);
        Point p = ActiveDocument.OffsetToPosition(start);
        ActiveTextArea.Caret.Column = p.X;
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

      int indexOf = -1;
      try
      {
        ActiveTextArea.BeginUpdate();
        int lineNo = ActiveTextArea.Caret.Line;
        int colNo = ActiveTextArea.Caret.Column;
        int totalNumOfLines = ActiveDocument.TotalNumberOfLines;
        string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
        LineText = ActiveDocument.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

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
          MessageBox.Show("Reached end of document", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
      return indexOf;
    }

    private int MatchPrev( string matchText )
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
        int colNo = ActiveTextArea.Caret.Column;

        string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
        LineText = LineText.Substring(0, ActiveTextArea.Caret.Column);

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
            LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);

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
      }
      finally
      {
        ActiveTextArea.EndUpdate();
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
      edtMatchText.Text = WordAtCursor;
      _frmSearchAndReplace.InitialSerachText = edtMatchText.Text;
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
      edtMatchText.Text = WordAtCursor;
      _frmSearchAndReplace.InitialSerachText = edtMatchText.Text;
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
      string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
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

      string LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
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
        LineText = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, lineNo);
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

    private void TextDiffScript(bool isSource)
    {
      string script = ActiveTextArea.SelectionManager.HasSomethingSelected ? ActiveTextArea.SelectionManager.SelectedText : _textEditor.Text;
      

      frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
      if(diffForm == null)
      {
        diffForm = TextDiffFactory.CreateDiff();
      }

      if(isSource)
      {
        diffForm.diffControl.SourceText = script;
        diffForm.diffControl.SourceHeaderText = Caption;
      }
      else
      {
        diffForm.diffControl.DestText = script;      
        diffForm.diffControl.DestHeaderText = Caption;
      }
      diffForm.Show();
      diffForm.BringToFront();
    }


    #endregion //Search And Replace

    #region Code Completion
    private void InitializeCodeCompletionWindow_Ex( )
    {
      if (_codeCompWindowEx != null)
      {
        return;
      }

      _codeCompWindowEx = new CodeCompletionPresenterEx();
      RegisterForCodeCompletionEventsEx();
    }

    private void ShowSelector_Ex( )
    {
      Point final = GetCaretPosition();
      _codeCompWindowEx.JumpTo(GetPreviousNonWSLineParts);
      _codeCompWindowEx.SetLocation(final.X + 10, final.Y - 10);
      _codeCompWindowEx.ShowSelector();

    }


    private void RegisterForCodeCompletionEventsEx( )
    {
      if (_codeCompWindowEx == null)
      {
        return;
      }

      _codeCompWindowEx.KeyPressed += new KeyPressHandler(HandleCodeCompletionKeyPress_Ex);
      _codeCompWindowEx.UserMadeFinalSelection += new NotificationHandler(HandleCodeCompletionSelection_Ex);
      _codeCompWindowEx.UserPressedBackSpace += new NotificationHandler(HandleCodeCompletionBackSpace_Ex);
    }

    private void HandleCodeCompletionKeyPress_Ex( char c )
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

    private void HandleCodeCompletionSelection_Ex( )
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

    private void HandleCodeCompletionBackSpace_Ex( )
    {
      new ICSharpCode.TextEditor.Actions.Backspace().Execute(ActiveTextArea);
      _codeCompWindowEx.JumpTo(GetPreviousNonWSLineParts);
    }

    #endregion //Code Completion

    #region ITextEditor Members

    private void PositionCaretTo( CaretPosition pos )
    {
      ActiveTextArea.Caret.Line = pos.Line;
      ActiveTextArea.Caret.Column = pos.Col;
    }


    public bool SaveToFile( string fileName )
    {
      _textEditor.SaveFile(fileName);
      return true;
    }

    public bool LoadFromFile( string fileName )
    {
      _textEditor.LoadFile(fileName, true, true);
      return true;
    }


    public void InsertContent( CaretPosition startPos, string content )
    {
      PositionCaretTo(startPos);
      ActiveTextArea.InsertString(content);
    }

    public void InsertContent( string content )
    {
      ActiveTextArea.InsertString(content);
    }

    public void AppendContent( string content )
    {
      try
      {
        ActiveTextArea.BeginUpdate();
        ActiveTextArea.Text += content;
      }
      finally
      {
        ActiveTextArea.EndUpdate();
        ActiveTextArea.Invalidate();
      }
    }

    public void RemoveContent( CaretPosition startPos, CaretPosition endPos )
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

    public string GetContent( CaretPosition startPos, CaretPosition endPos )
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

    public string Content
    {
      get
      {
        return ActiveTextArea.Text;
      }
      set
      {
        try
        {
          ActiveTextArea.BeginUpdate();
          ActiveTextArea.Text = value;
        }
        finally
        {
          ActiveTextArea.EndUpdate();
          ActiveTextArea.Invalidate();
        }
      }
    }

    public void Select( CaretPosition startPos, CaretPosition endPos )
    {
      ActiveTextArea.SelectionManager.SetSelection(startPos.ToPoint(), endPos.ToPoint());
    }

    public void ClearSelection( )
    {
      ActiveTextArea.SelectionManager.ClearSelection();
    }

    public void DeleteSelection( )
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
        result.Offset = ActiveTextArea.Caret.Offset;
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
        return SharpDevelopTextEditorUtilities.GetWordAt(ActiveDocument, ActiveTextArea.Caret.Offset);
      }
    }

    private event AfterOpenedFileDelegate _afterOpenedFile;
    public event AfterOpenedFileDelegate AfterOpenedFile
    {
      add
      {
        _afterOpenedFile += value;
      }
      remove
      {
        _afterOpenedFile -= value;
      }
    }

    private event AfterSavedContentDelegate _afterSavedContent;
    public event AfterSavedContentDelegate AfterSavedContent
    {
      add
      {
        _afterSavedContent += value;
      }
      remove
      {
        _afterSavedContent -= value;
      }
    }

    private event AfterSavedContentToFileDelegate _afterSaveContentToFile;
    public event AfterSavedContentToFileDelegate AfterSaveContentToFile
    {
      add
      {
        _afterSaveContentToFile += value;
      }
      remove
      {
        _afterSaveContentToFile -= value;
      }
    }

    private event EventHandler _afterContentChanged;
    public event EventHandler AfterContentChanged
    {
      add
      {
        _afterContentChanged += value;
      }
      remove
      {
        _afterContentChanged -= value;
      }
    }


    private event AfterCaretPositionChangedEvent _afterCaretPositionChanged;
    public event AfterCaretPositionChangedEvent AfterCaretPositionChanged
    {
      add
      {
        _afterCaretPositionChanged += value;
      }
      remove
      {
        _afterCaretPositionChanged -= value;
      }
    }

    #endregion

    #region IPragmaEditor Members
    private bool _contentModified = false;
    public bool ContentModified
    {
      get
      {
        return _contentModified;
      }
      set
      {
        _contentModified = value;
        if (value)
        {
          this.Text = "* " + _caption;
          this.TabText = this.Text;
          statContentModified.Visible = true;
        }
        else
        {
          this.Text = _caption;
          this.TabText = this.Text;
          statContentModified.Visible = false;
        }
      }
    }

    private IContentPersister _contentPersister = new DefaultContentPersister();
    public IContentPersister ContentPersister
    {
      get { return _contentPersister; }
      set { _contentPersister = value; }
    }

    private string _caption = String.Empty;
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

    private DateTime? _lastModifiedOn = null;
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

    public void RefreshCodeCompletionLists( )
    {
      if (_codeCompWindowEx == null)
      {
        return;
      }

      _codeCompWindowEx.RefreshCodeCompletionLists();
    }
    public void RefreshShortcuts( )
    {
      _actionKeys.Clear();
      InitiailizeActions();
    }

    #region Content I/O

    public bool OpenFile( string fileName )
    {
      if (_contentPersister == null)
      {
        return false;
      }

      if (_contentPersister.GetType() != typeof(DefaultContentPersister))
      {
        _contentPersister = new DefaultContentPersister();
      }

      if (!_contentPersister.LoadContent(fileName, _textEditor))
      {
        return false;
      }

      statContentName.Text = _contentPersister.ContentName;
      Caption = _contentPersister.Hint;
      ContentModified = false;

      if (_afterOpenedFile != null)
      {
        _afterOpenedFile(this, statContentName.Text);
      }
      return true;
    }

    public void SaveContent( )
    {
      if (_contentPersister == null)
      {
        return;
      }

      if (_contentPersister.SaveContent(_caption, _textEditor))
      {
        statContentName.Text = _contentPersister.ContentName;
        Caption = _contentPersister.Hint;
        ContentModified = false;

        if (_afterSavedContent != null)
        {
          _afterSavedContent(this, statContentName.Text);
        }
      }
    }

    public void SaveContentAs( )
    {
      if (_contentPersister == null)
      {
        return;
      }

      if (_contentPersister.GetType() != typeof(DefaultContentPersister))
      {
        _contentPersister = new DefaultContentPersister();
      }

      if (_contentPersister.SaveContentAs(_caption, _textEditor))
      {
        statContentName.Text = _contentPersister.ContentName;
        Caption = _contentPersister.Hint;
        ContentModified = false;

        if (_afterSaveContentToFile != null)
        {
          _afterSaveContentToFile(this, statContentName.Text);
        }
      }
    }

    public void InspectPragmaSQLDbConnection( )
    {
      //Nothing need to be done    
    }

    public void ApplyTextEditorOptionsFromCurrentConfig( )
    {
      //Nothing need to be done
    }


    #endregion //Content I/O



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

      if (!CheckSave || !ContentModified)
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
        SaveContent();
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

    private void openSharedScriptToolStripMenuItem_Click( object sender, EventArgs e )
    {
      frmSharedScriptSelectDialog.OpenSharedScript(this, null);
    }

    private void saveAsSharedScriptToolStripMenuItem_Click( object sender, EventArgs e )
    {
      frmSharedScriptSelectDialog.SaveAsSharedScript(this);
    }

    private void OnDiffScriptAsSource_Click( object sender, EventArgs e )
    {
      TextDiffScript(true);
    }

    private void OnDiffScriptAsDest_Click( object sender, EventArgs e )
    {
      TextDiffScript(false);

    }


  }
}