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

using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.Core;

using Crad.Windows.Forms.Actions;
using AsynchronousCodeBlocks;

using PragmaSQL;
using PragmaSQL.Core;

namespace PragmaSQL
{

  public partial class frmTextEditor : frmEditorBase, IPragmaEditor
  {
    #region Fields And Properties
    internal int? WindowNo = null;

    private ToolStrip _addInToolStrip;
    private CodeCompletionPresenterEx _codeCompWindowEx;
    private SearchAndReplaceForm _frmSearchAndReplace = null;
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

    private bool _switchToScriptEditorAllowed = true;
    public bool SwitchToScriptEditorAllowed
    {
      get { return _switchToScriptEditorAllowed; }
      set
      {
        _switchToScriptEditorAllowed = value;
        ApplyCanSwitchToScriptEditor(value);
      }
    }

    private void ApplyCanSwitchToScriptEditor(bool value)
    {
      switchToToolStripMenuItem.Visible = value;
      switchToToolStripMenuItem.Visible = value;
      btnSwitchToScriptEditor.Visible = value;
    }

    private string SearchTerm
    {
      get
      {
        if (String.IsNullOrEmpty(edtMatchText.Text))
          edtMatchText.Text = Program.MainForm.SearchTerm;
        return edtMatchText.Text;
      }
      set
      {
        edtMatchText.Text = value;
      }
    }

    #endregion //Fields And Properties

    #region CTOR

    public frmTextEditor()
    {
      InitializeComponent();
      base.ContentPersister.ContentType = EditorContentType.Text;
      base.SearchToolStripTextBox = edtMatchText;
      base.ContentModifiedIndicatorToolStripItem = statLblContentModifiedIndicator;
      base.ContentInfoToolStripItem = statLblContentInfo;
      
      edtMatchText.Text = Program.MainForm.SearchTerm;

      InitializeTextEditor();
      InitializeAddInSupport();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeForm()
    {
      WireUpTextEditor();
      SharedScriptOperationsVisible = ConfigHelper.Current.SharedScriptsEnabled();
      InitializeCodeCompletionWindow_Ex();
      InitiailizeActions();
      PopulateSyntaxModes();

      //base._textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("Default");
      cmbSyntaxModes.ComboBox.SelectedIndex = -1;
      SetSyntaxMode(cmbSyntaxModes.ComboBox, base._textEditor.Document.HighlightingStrategy.Name);
    }

    private LayoutProvider _lp = new LayoutProvider();
    public void InitializeTextEditor(string caption, string script)
    {
      if ((Program.MainForm.DetermineStartPosition() ?? -1) == -1)
        _lp.SuspendLayout();

      try
      {
        _caption = caption;
        this.Text = _caption;
        this.TabText = _caption;

        InitializeForm();
        _initializing = true;
        _scriptText = script;
        _textEditor.Text = _scriptText;
        Program.MainForm.HostSvc.FireTextEditorReadyEvent(this);
      }
      finally
      {
        _initializing = false;
      }
    }

    #endregion //Initialization

    #region Utilities

    public int LineCount(string value)
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

    private void WireUpTextEditor()
    {
      ConfigHelper.Current.TextEditorOptions.ApplyToControl(_textEditor);
      _textEditor.ContextMenuStrip = popUpEditor;
      _textEditor.Document.DocumentChanged += new DocumentEventHandler(OnDocumentChanged);

      ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextEditorKeyDown);
      ActiveTextArea.Caret.PositionChanged += new EventHandler(OnCaretPositionChanged);
    }

    private void OnCaretPositionChanged(object sender, EventArgs e)
    {
      FireBeforeCaretPositionChanged();

      statCaretPos.Text = "Ln: " + (ActiveTextArea.Caret.Line + 1).ToString()
        + ", Col: " + (ActiveTextArea.Caret.Column + 1).ToString()
        + " ( Offset: " + ActiveTextArea.Caret.Offset.ToString() + " )";

      FireAfterCaretPositionChanged();
    }

    private void OnDocumentChanged(object sender, DocumentEventArgs e)
    {
      if (e.Document.UndoStack.CanUndo)
      {
        if(!ContentModified)
          ContentModified = true;
        _lastModifiedOn = DateTime.Now;
        FireAfterContentChanged();
      }
      else
      {
        if (ContentModified)
          ContentModified = false;
        
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

    private void MoveCaretToEOL()
    {
      ActiveTextArea.Caret.Column = SharpDevelopTextEditorUtilities.GetLineAsString(ActiveDocument, ActiveTextArea.Caret.Line).Length;
    }

    public int DeleteWordBeforeCaret()
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

    private Point GetCaretPosition()
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

    private void ReclaimWindowNumber()
    {
      TextEditorFactory.Numerator.ReclaimNumber(WindowNo);
      WindowNo = null;
    }

    private void ReclaimWindowNumber_OnClose()
    {
      ReclaimWindowNumber();
      TextEditorFactory.Numerator.WindowCount--;
      TextEditorFactory.ResetNumerator();
    }

    public void SetFilePath(string filePath)
    {
      FileName = filePath;
      statLblContentInfo.Text = filePath;


      if (!String.IsNullOrEmpty(filePath))
        ApplyFileIcon(filePath);
    }

    private void ApplyFileIcon(string filePath)
    {
      if (_contentPersister != null && _contentPersister.GetType() != typeof(DefaultContentPersister))
        return;

      System.Drawing.Icon icon = null;
      icon = IconExtractor.GetFileIcon(filePath, IconSize.Small);
      if (icon == null)
        icon = Properties.Resources.TextEditor;
      
      this.Icon = icon;
    }

    private void SwitchToScriptEditor()
    {
      ConnectionParams cp = frmConnectionRepository.SelectSingleConnection(true, true);
      if (cp == null)
        return;

      string content = this.Content;
      string caption = this.Caption;
      string filePath = this.FileName;

      frmScriptEditor scriptEditor = ScriptEditorFactory.Create(caption, content, cp, filePath);
      scriptEditor.ContentModified = base.ContentModified;
      scriptEditor.IsRecoveredContent = base.IsRecoveredContent;
      scriptEditor.ContentPersister = ContentPersister;
      if (ContentPersister.ContentType == EditorContentType.SharedScript || ContentPersister.ContentType == EditorContentType.SharedSnippet)
        scriptEditor.Icon = this.Icon;

      if (base.ContentModified)
        base.ContentModified = false;

      ScriptEditorFactory.ShowScriptEditor(scriptEditor);
      this.Close();
    }


    #endregion //Utilities

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
      string requestedFor = GetPreviousNonWSLineParts;
      FireBeforeCodeCompletionShowed(CodeCompletionType.UserDefinedList, requestedFor);

      Point final = GetCaretPosition();
      _codeCompWindowEx.JumpTo(requestedFor);
      _codeCompWindowEx.SetLocation(final.X + 10, final.Y - 10);
      _codeCompWindowEx.ShowSelector();

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
          FireAfterCodeCompletionShowed(CodeCompletionType.UserDefinedList, String.Empty, false);
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
      try
      {
        ActiveTextArea.BeginUpdate();
        string userSelection = String.Empty;
        if (_codeCompWindowEx.Selector.HasMultipleSelection)
        {
          userSelection = _codeCompWindowEx.SelectedItemsAsCommaSeparatedString;
        }
        else
        {
          userSelection = _codeCompWindowEx.SelectedItem;
        }


        if (_codeCompWindowEx.Selector.HasMultipleSelection)
        {
          ActiveTextArea.InsertString(userSelection);
        }
        else
        {
          DeleteWordBeforeCaret();
          ActiveTextArea.InsertString(userSelection);
        }
        FireAfterCodeCompletionShowed(CodeCompletionType.UserDefinedList, userSelection, true);

      }
      finally
      {
        ActiveTextArea.EndUpdate();

      }
      _codeCompWindowEx.DismissSelector();
      _textEditor.Focus();

    }

    private void HandleCodeCompletionBackSpace_Ex()
    {
      new ICSharpCode.TextEditor.Actions.Backspace().Execute(ActiveTextArea);
      _codeCompWindowEx.JumpTo(GetPreviousNonWSLineParts);
    }

    #endregion //Code Completion

    #region IPragmaEditor Members

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


    public void RefreshCodeCompletionLists()
    {
      if (_codeCompWindowEx == null)
      {
        return;
      }

      _codeCompWindowEx.RefreshCodeCompletionLists();
    }

    public void RefreshShortcuts()
    {
      _actionKeys.Clear();
      InitiailizeActions();
    }


    #region Content I/O

    public bool OpenFile(string fileName)
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

      FireBeforeOpenedFile(fileName);

      statLblContentInfo.Text = _contentPersister.FilePath;
      Caption = _contentPersister.Hint;
      ContentModified = false;
      IsRecoveredContent = false;

      base.FileName = _contentPersister.FilePath;
      SetSyntaxMode(cmbSyntaxModes.ComboBox, base._textEditor.Document.HighlightingStrategy.Name);
      ApplyFileIcon(_contentPersister.FilePath);
      ReclaimWindowNumber();
      FireAfterOpenedFile(statLblContentInfo.Text);
      return true;
    }

    public void SaveContent()
    {
      if (_contentPersister == null)
        throw new Exception("Content persister object is null!");

      if (_contentPersister.GetType() == typeof(SharedScriptContentPersister) && (_contentPersister.Data as SharedScriptsItemData) == null)
      {
        SaveAsSharedScript();
        return;
      }

      FireBeforeSavedContent();
      base.StopFileWatcher();

      if (_contentPersister.SaveContent(_caption, _textEditor))
      {
        ApplyFileIcon(_contentPersister.FilePath);
        statLblContentInfo.Text = _contentPersister.FilePath;
        Caption = _contentPersister.Hint;
        ContentModified = false;
        IsRecoveredContent = false;

        base.FileName = _contentPersister.FilePath;
        ReclaimWindowNumber();
        FireAfterSavedContent(statLblContentInfo.Text);
      }
    }

    public void SaveContentAs()
    {
      if (_contentPersister == null)
        _contentPersister = new DefaultContentPersister();
      
      try
      {
        _contentPersister.BeforeSavedContentToFile += new BeforeSavedContentToFileDelegate(_contentPersister_BeforeSavedContentToFile);
        if (_contentPersister.SaveContentAs(_caption, _textEditor))
        {
          ApplyFileIcon(_contentPersister.FilePath);
          statLblContentInfo.Text = _contentPersister.FilePath;
          Caption = _contentPersister.Hint;
          ContentModified = false;
          IsRecoveredContent = false;

          base.FileName = _contentPersister.FilePath;
          ReclaimWindowNumber();
          FireAfterSaveContentToFile(statLblContentInfo.Text);
        }
      }
      finally
      {
        _contentPersister.BeforeSavedContentToFile -= new BeforeSavedContentToFileDelegate(_contentPersister_BeforeSavedContentToFile);
      }
    }

    private void _contentPersister_BeforeSavedContentToFile(object sender, FileOperationEventArgs args)
    {
      FireBeforeSaveContentToFile(args.FileName);
    }

    public void InspectPragmaSQLDbConnection()
    {
      //Nothing need to be done    
    }


    #endregion //Content I/O



    #endregion

    #region Base Overrides
    public override bool LoadContentFromFile(string fileName)
    {
      return this.OpenFile(fileName);
    }

    public override bool SaveContentToFile(string fileName)
    {
      if (_contentPersister == null)
        _contentPersister = new DefaultContentPersister();


      _contentPersister.BeforeSavedContentToFile += new BeforeSavedContentToFileDelegate(_contentPersister_BeforeSavedContentToFile);
      try
      {
        if (_contentPersister.SaveContentToFile(fileName, _textEditor))
        {
          ApplyFileIcon(_contentPersister.FilePath);
          statLblContentInfo.Text = _contentPersister.FilePath;
          Caption = _contentPersister.Hint;
          ContentModified = false;
          IsRecoveredContent = false;

          base.FileName = _contentPersister.FilePath;
          ReclaimWindowNumber();
          FireAfterSaveContentToFile(statLblContentInfo.Text);
        }
        return true;
      }
      finally
      {
        _contentPersister.BeforeSavedContentToFile -= new BeforeSavedContentToFileDelegate(_contentPersister_BeforeSavedContentToFile);
      }
    }

    public override int AddInStripItemCount
    {
      get { return _addInToolStrip.Items.Count; }
    }

    public override void AddItemToAddInStrip(ToolStripItem item)
    {
      AddItemToAddInStrip(item, -1);
    }

    public override void AddItemToAddInStrip(ToolStripItem item, int index)
    {
      if (item == null)
        return;

      if (_addInToolStrip.Items.Contains(item))
        return;

      if (index < 0)
        _addInToolStrip.Items.Add(item);
      else
        _addInToolStrip.Items.Insert(index, item);

      if (!_addInToolStrip.Visible)
        _addInToolStrip.Visible = true;
    }

    public override void RemoveItemFromAddInStrip(ToolStripItem item)
    {
      if (item == null)
        return;

      if (!_addInToolStrip.Items.Contains(item))
        return;

      _addInToolStrip.Items.Remove(item);
      if (_addInToolStrip.Items.Count == 0)
        _addInToolStrip.Visible = false;
    }


    #endregion //Base Overrides

    #region AddIn Support
    private void InitializeAddInSupport()
    {

      _addInToolStrip = ToolbarService.CreateToolStrip(this, "/Workspace/TextEditor/Toolbar");
      if (_addInToolStrip.Items.Count == 0)
      {
        _addInToolStrip.Visible = false;
      }
      else
      {
        _addInToolStrip.Visible = true;
        this.Controls.Add(_addInToolStrip);
        _addInToolStrip.RenderMode = ToolStripRenderMode.ManagerRenderMode;
        _addInToolStrip.GripStyle = ToolStripGripStyle.Hidden;
        _addInToolStrip.Dock = DockStyle.Top;
        _addInToolStrip.BringToFront();
        panEditor.BringToFront();

      }

      MenuService.AddItemsToMenu(popUpEditor.Items, this, "/Workspace/TextEditor/ContextMenu");
      MenuService.AddItemsToMenu(mainMenu.Items, this, "/Workspace/TextEditor/MainMenu");
      MenuService.AddItemsToMenu(popUpTab.Items, this, "/Workspace/TextEditor/ContentContextMenu");
    }

    #endregion //AddInSupport

    #region Sytax Highlighting
    private void PopulateSyntaxModes()
    {
      foreach (string mode in base.SyntaxModes)
        cmbSyntaxModes.Items.Add(mode);
    }

    private void SetSyntaxMode(object sender, string mode)
    {
      TextEditorControl te = sender as TextEditorControl;
      ComboBox cmb = sender as ComboBox;
      if (te != null)
      {
        base.CurrentSytaxMode = mode;
        te.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy(mode);
      }
      else if (cmb != null)
      {
        cmb.SelectedIndex = cmb.FindStringExact(mode);
      }
    }

    public void SetSyntaxMode(string mode)
    {
      SetSyntaxMode(cmbSyntaxModes.ComboBox, mode);
    }

    #endregion

    #region Workspace State Related
    private void PrepareWorkspaceState()
    {
      if (!Program.MainForm.ApplicationIsClosing || String.IsNullOrEmpty(this.Content))
        return;

      RecoverContent.Save(RecoverContent.WorkspaceFolder, RecoverContent.CreateTextContent(this));
    }



    #endregion //Workspace State Related

    #region Misc Utils
    private void SaveAsSharedScript()
    {
      if (frmSharedScriptSelectDialog.SaveAsSharedScript(this,_caption))
      {
        ContentPersister.ContentType = EditorContentType.SharedScript;
        ReclaimWindowNumber();
      }
    }

    private void OpenSharedScript()
    {
      if (frmSharedScriptSelectDialog.OpenSharedScript(this, null))
      {
        ContentPersister.ContentType = EditorContentType.SharedScript;
        ReclaimWindowNumber();
      }
    }

    #endregion //Misc Utils

    private void edtMatchText_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        MatchNext(edtMatchText.Text);
      }
    }

    private void frmTextEditor_Leave(object sender, EventArgs e)
    {
      if (_frmSearchAndReplace != null)
      {
        _frmSearchAndReplace.Hide();
      }
    }

    private void frmTextEditor_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.Cancel)
        return;

      if (!CheckSave || !ContentModified)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Save changes to \"" + _caption + "\"", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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

    private void edtMatchText_KeyDown_1(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        MatchNext(edtMatchText.Text);
      }
    }

    private void openSharedScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenSharedScript();
    }

  

    private void saveAsSharedScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveAsSharedScript();
    }

    private void OnDiffScriptAsSource_Click(object sender, EventArgs e)
    {
      SendSelectedTextToTextDiff(true);
    }

    private void OnDiffScriptAsDest_Click(object sender, EventArgs e)
    {
      SendSelectedTextToTextDiff(false);

    }

    private void cmbSyntaxModes_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetSyntaxMode(base._textEditor, cmbSyntaxModes.SelectedItem as string);
    }

    private void frmTextEditor_FormClosed(object sender, FormClosedEventArgs e)
    {
      DisposeIncSearchObjects();
      try
      {
        PrepareWorkspaceState();
      }
      catch { }
      ReclaimWindowNumber_OnClose();
    }

    private void btnNewScript_Click(object sender, EventArgs e)
    {
      frmTextEditor editor = TextEditorFactory.Create();
      TextEditorFactory.ShowTextEditor(editor);
    }

    private void findOnWebToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Program.MainForm.PerformWebSearch(SelectedTextOrWordAtCursor);
    }


  }
}