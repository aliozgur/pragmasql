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

using WeifenLuo.WinFormsUI;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using PragmaSQL.GUI;
using PragmaSQL.Common;

using PragmaSQL.Database;
using Crad.Windows.Forms.Actions;

namespace PragmaSQL.GUI
{
  public enum TokenConversionType
  {
    Lower,
    Upper,
    Capitalize
  }
  public partial class frmScriptEditor : DockContent
  {
    private TextEditorControl _textEditor = null;
    private CodeCompletionPresenter _codeCompWindow;

    private ConnectionParams _connParams;
    private ConnectionParamsCollection _connParamsCollection = null;

    private SqlConnection _conn = new SqlConnection();


    private SqlCommand _cmd = null;
    private BackgroundWorker workerThread = new BackgroundWorker();

    private IList<DataGridView> _grids = new List<DataGridView>();

    private IList<SqlMessage> _sqlMessages = new List<SqlMessage>();

    private bool _isInitializing = false;

    private frmSearchAndReplace _frmSearchAndReplace = null;
    private frmGoToLine _frmGoToLine = null;

    private DateTime _startTime = DateTime.Now;
    private bool _completedWithErrors = false;
    private string _caption = String.Empty;
    public bool CheckSave = true;
    private int _currentServerIndex = -1;
    private bool _isExecuting = false;

    private ActionList _actionList = new ActionList();

    #region Properties

    private string _fileName = String.Empty;
    public string FileName
    {
      get { return _fileName; }
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

    #endregion

    #region Constructor

    public frmScriptEditor( )
    {
      InitializeComponent();
    }

    #endregion

    #region Initialize Actions
    
    private void InitiailizeActions()
    {
      InitializeActions_FileOperations();
      InitializeActions_EditorOperations();
      InitializeActions_ExecuteScriptOperations();
      InitializeActions_SearchAndReplaceOperations();
      InitializeActions_ScriptFormatOperations();
      InitializeActions_ScriptEditOperations();
    }

    private void InitializeActions_FileOperations()
    {
      #region  Open file action
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.open;
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_OpenFile_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.O;
      ac.Text = "Open";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemOpen,ac);
      _actionList.SetAction(btnOpen,ac);
      #endregion

      #region Open New Editor from File
      ac = new Action();
      //ac.Image = global::PragmaSQL.Properties.Resources.open;
      ac.ShortcutKeys = Keys.Control | Keys.Shift | Keys.O;
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_OpenFromFile_Execute);
      ac.Text = "New Script From File";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuScriptFromFile, ac);
      
      #endregion

      #region Save file action
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.save;
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_SaveFile_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.S;
      ac.Text = "Save";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(mnuItemSave,ac);
      _actionList.SetAction(btnSave,ac);
      _actionList.SetAction(cMnuItemSave,ac);
      
      #endregion

      #region Save file as action
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.SaveAs;
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_SaveFileAs_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S ;
      ac.Text = "Save As";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(mnuItemSaveAs,ac);
      _actionList.SetAction(btnSaveAs,ac);

      #endregion

    }

    private void InitializeActions_EditorOperations()
    {
      #region  New script
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.new1;
      ac.Execute += new EventHandler(OnAction_NewScript_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.N ;
      ac.Text = "New Script";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(cMnuNewScript,ac);
      #endregion

      #region  Close
      ac = new Action();
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_Close_Execute);
      ac.Text = "Close";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(cMnuItemClose,ac);

      #endregion

      #region Close All
      ac = new Action();
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_CloseAll_Execute);
      ac.Text = "Close All";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(cMnuCloseAll,ac);

      #endregion

      #region  Close All But This
      ac = new Action();
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_CloseAllButThis_Execute);
      ac.Text = "Close All But This";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(cMnuCloseAllButThis,ac);

      #endregion

      #region Toggle output pane
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_ToggleOutputPane_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.R;
      ac.Text = "Toggle Output Pane";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(mnuItemToggleOutputPane,ac);
      
      #endregion

    }

    private void InitializeActions_ExecuteScriptOperations()
    {
      #region Run Script
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.Run;
      ac.Update +=new EventHandler(OnAction_RunScript_Update);
      ac.Execute += new EventHandler(OnAction_RunScript_Execute);
      ac.ShortcutKeys = Keys.F5;
      ac.Text = "Execute";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnRun,ac);
      _actionList.SetAction(mnuItemRun,ac);

      #endregion

      #region Check syntax
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.correct;
      ac.Update +=new EventHandler(OnAction_CheckSyntax_Update);
      ac.Execute += new EventHandler(OnAction_CheckSyntax_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.F5;
      ac.Text = "Check Syntax";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnCheckSyntax,ac);
      _actionList.SetAction(mnuItemCheckSyntax,ac);
      #endregion

      #region Show Plan
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.gear_1;
      ac.Update +=new EventHandler(OnAction_ShowPlan_Update);
      ac.Execute += new EventHandler(OnAction_ShowPlan_Execute);
      ac.Text = "Show Plan";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnShowPlan,ac);
      _actionList.SetAction(mnuItemShowPlan,ac);
      #endregion

      #region Stop execution
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.Stop;
      ac.Update +=new EventHandler(OnAction_StopExecution_Update);
      ac.Execute += new EventHandler(OnAction_StopExecution_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.F2;
      ac.Text = "Stop";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnStop,ac);
      _actionList.SetAction(mnuItemStop,ac);
      
      #endregion
    }

    private void InitializeActions_SearchAndReplaceOperations()
    {
      #region Quick search forward
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.down;
      ac.Execute += new EventHandler(OnAction_QuickSearchForward_Execute);
      ac.ShortcutKeys = Keys.F3;
      ac.Text = "Search Forward";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnFindNext,ac);
      _actionList.SetAction(mnuItemSearchForward,ac);

      #endregion

      #region Quick search backward
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.up;
      ac.Execute += new EventHandler(OnAction_QuickSearchBackward_Execute);
      ac.ShortcutKeys = Keys.Control | Keys.F3;
      ac.Text = "Search Backwards";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnFindPrev,ac);
      _actionList.SetAction(mnuItemSearchBackward,ac);
      #endregion

      #region GoTo Line
      ac = new Action();
      ac.ShortcutKeys = Keys.Control | Keys.G;
      ac.Execute += new EventHandler(OnAction_GoToLine_Execute);
      ac.Text = "Go To Line";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemGoToLine,ac);
      #endregion

      #region Find
      ac = new Action();
      ac.ShortcutKeys = Keys.Control | Keys.F;
      ac.Execute += new EventHandler(OnAction_Find_Execute);
      ac.Text = "Find";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemFind,ac);
      #endregion

      #region Replace
      ac = new Action();
      ac.ShortcutKeys = Keys.Control | Keys.H;
      ac.Execute += new EventHandler(OnAction_Replace_Execute);
      ac.Text = "Replace";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemReplace,ac);
      #endregion
    }

    private void InitializeActions_ScriptFormatOperations()
    {
      #region Outdent
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.IndentRTL;
      ac.Execute += new EventHandler(OnAction_OutdentSelection_Execute);
      ac.Text = "Outdent selection";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnOutDent,ac);

      #endregion

      #region Indent
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.Indent;
      ac.Execute += new EventHandler(OnAction_IndentSelection_Execute);
      ac.Text = "Outdent selection";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnIndent,ac);

      #endregion

      #region Keywords to uppercase
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.font_increase;
      ac.Execute += new EventHandler(OnAction_KeywordsToUppercase_Execute);
      ac.Text = "Keywords To Uppercase ";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnKeywordsToUppercase,ac);
      _actionList.SetAction(mnuItemKeywordsToUppercase,ac);

      #endregion

      #region Keywords to lowercase
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.font_decrease;
      ac.Execute += new EventHandler(OnAction_KeywordsToLowercase_Execute);
      ac.Text = "Keywords To Lowercase ";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnKeywordsToLowercase,ac);
      _actionList.SetAction(mnuItemKeywordsToLowercase,ac);

      #endregion

      #region Capitalize keywords
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.font_capitalize;
      ac.Execute += new EventHandler(OnAction_CapitalizeKeywords_Execute);
      ac.Text = "Captalize Keywords";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(btnCapitalizeKeywords,ac);
      _actionList.SetAction(mnuItemCapitilizeKeywords,ac);

      #endregion
      
      #region Script to uppercase
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_ScriptToUppercase_Execute);
      ac.Text = "Script To Uppercase";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(mnuItemScriptToUppercase,ac);

      #endregion

      #region Script to lowercase
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_ScriptToLowercase_Execute);
      ac.Text = "Script To Lowercase";
      _actionList.Actions.Add(ac);
      
      _actionList.SetAction(mnuItemScriptToLowercase,ac);

      #endregion
    }

    private void InitializeActions_ScriptEditOperations()
    {
      #region Undo
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.undo;
      ac.ShortcutKeys = Keys.Control | Keys.Z;
      ac.Execute += new EventHandler(OnAction_Undo_Execute);
      
      ac.Text = "Undo";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemUndo,ac);
      _actionList.SetAction(tsMnuItemUndo,ac);
      
      #endregion

      #region Redo
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.undo;
      ac.ShortcutKeys = Keys.Control | Keys.Y;
      ac.Execute += new EventHandler(OnAction_Redo_Execute);
      
      ac.Text = "Redo";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemRedo,ac);
      _actionList.SetAction(tsMnuItemRedo,ac);
      
      #endregion

      #region Cut
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.cut_2;
      ac.ShortcutKeys = Keys.Control | Keys.X;
      ac.Execute += new EventHandler(OnAction_Cut_Execute);
      
      ac.Text = "Cut";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCut,ac);
      _actionList.SetAction(tsMnuItemCut,ac);
      
      #endregion

      #region Copy
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.copy;
      ac.ShortcutKeys = Keys.Control | Keys.C;
      ac.Execute += new EventHandler(OnAction_Copy_Execute);
      
      ac.Text = "Copy";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCopy,ac);
      _actionList.SetAction(tsMnuItemCopy,ac);
      
      #endregion


      #region Paste
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.copy;
      ac.ShortcutKeys = Keys.Control | Keys.V;
      ac.Execute += new EventHandler(OnAction_Paste_Execute);
      
      ac.Text = "Paste";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemPaste,ac);
      _actionList.SetAction(tsMnuItemPaste,ac);
      
      #endregion

      #region Copy grid to clipboard
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_CopyGridToClipboard_Execute);
      ac.Text = "Copy To Clipboard";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(popUpItemCopyGridToClipboard,ac);
      #endregion

    }

    private void OnAction_Generic_Update(object sender, EventArgs e)
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    private void OnAction_OpenFromFile_Execute(object sender, EventArgs e)
    {
      NodeData data = Program.MainForm.GetCurrentSelectedNodeDataFromObjectExplorer();
      if (data == null)
      {
        return;
      }
      frmScriptEditor editor = ScriptEditorFactory.OpenFile(String.Empty, data);
      ScriptEditorFactory.ShowScriptEditor(editor);
    }

    private void OnAction_OpenFile_Execute(object sender, EventArgs e)
    {
      OpenScriptFromFile(String.Empty);
    }
    
    private void OnAction_SaveFile_Execute(object sender, EventArgs e)
    {
      SaveScript();
    }

    private void OnAction_SaveFileAs_Execute(object sender, EventArgs e)
    {
      SaveScriptAs();
    }

    private void OnAction_NewScript_Execute(object sender, EventArgs e)
    {
      NodeData data = Program.MainForm.GetCurrentSelectedNodeDataFromObjectExplorer();
      if (data == null)
      {
        return;
      }
      
      frmScriptEditor editor = ScriptEditorFactory.CreateScriptEditor(data);
      ScriptEditorFactory.ShowScriptEditor(editor);
    }

    private void OnAction_Close_Execute(object sender, EventArgs e)
    {
      Close();
    }

    private void OnAction_CloseAll_Execute(object sender, EventArgs e)
    {
      if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, null) == DialogResult.Cancel)
      {
        return;
      }
      Program.MainForm.CloseDocuments(null);
    }
    
    private void OnAction_CloseAllButThis_Execute(object sender, EventArgs e)
    {
      if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, this) == DialogResult.Cancel)
      {
        return;
      }
      Program.MainForm.CloseDocuments(this);
    }

    private void OnAction_RunScript_Update(object sender, EventArgs e)
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    private void OnAction_RunScript_Execute(object sender, EventArgs e)
    {
      RunScript(RunType.Execute);
    }

    private void OnAction_CheckSyntax_Update(object sender, EventArgs e)
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    private void OnAction_CheckSyntax_Execute(object sender, EventArgs e)
    {
      RunScript(RunType.CheckSyntax);
    }

    private void OnAction_ShowPlan_Update(object sender, EventArgs e)
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    private void OnAction_ShowPlan_Execute(object sender, EventArgs e)
    {
      RunScript(RunType.ShowPlan);
    }

    private void OnAction_StopExecution_Update(object sender, EventArgs e)
    {
      Action ac = sender as Action;
      ac.Enabled = _isExecuting;
    }

    private void OnAction_StopExecution_Execute(object sender, EventArgs e)
    {
      StopExecution();
    }

    private void OnAction_ToggleOutputPane_Execute(object sender, EventArgs e)
    {
      OutputPaneVisible = !OutputPaneVisible;
    }

    private void OnAction_QuickSearchForward_Execute(object sender, EventArgs e)
    {
      MatchNext(edtMatchText.Text); 
    }

    private void OnAction_QuickSearchBackward_Execute(object sender, EventArgs e)
    {
      MatchPrev(edtMatchText.Text); 
    }

    private void OnAction_IndentSelection_Execute(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Tab().Execute(ActiveTextArea);
    }
    
    private void OnAction_OutdentSelection_Execute(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.ShiftTab().Execute(ActiveTextArea);
    }

    private void OnAction_KeywordsToUppercase_Execute(object sender, EventArgs e)
    {
      ConvertTokensTo(TokenConversionType.Upper);
    }

    private void OnAction_KeywordsToLowercase_Execute(object sender, EventArgs e)
    {
      ConvertTokensTo(TokenConversionType.Lower);
    }

    private void OnAction_CapitalizeKeywords_Execute(object sender, EventArgs e)
    {
      ConvertTokensTo(TokenConversionType.Capitalize);
    }
    
    private void OnAction_ScriptToUppercase_Execute(object sender, EventArgs e)
    {
      ChangeScriptCase(TokenConversionType.Upper);
    }
    
    private void OnAction_ScriptToLowercase_Execute(object sender, EventArgs e)
    {
      ChangeScriptCase(TokenConversionType.Lower);
    }

    private void OnAction_GoToLine_Execute(object sender, EventArgs e)
    {
      ShowGoToLineDialog();
    }
    
    private void OnAction_Find_Execute(object sender, EventArgs e)
    {
      ShowSearchDialog();
    }

    private void OnAction_Replace_Execute(object sender, EventArgs e)
    {
      ShowReplaceDialog();
    }

    private void OnAction_Undo_Execute(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Undo().Execute(ActiveTextArea);
    }

    private void OnAction_Redo_Execute(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Redo().Execute(ActiveTextArea);
    }

    private void OnAction_Cut_Execute(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Cut().Execute(ActiveTextArea);
    }
    
    private void OnAction_Copy_Execute(object sender, EventArgs e)
    {
      if (ActiveTextArea.Focused)
      {
        new ICSharpCode.TextEditor.Actions.Copy().Execute(ActiveTextArea);
      }
      else
      {
        CopyGridContentToClipboard();
      }
    }

    private void OnAction_Paste_Execute(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Paste().Execute(ActiveTextArea);
    }

    private void OnAction_CopyGridToClipboard_Execute(object sender, EventArgs e)
    {
      CopyGridContentToClipboard();
    }

    #endregion

    #region Initialization


    private void InitializeForm( )
    {

      OutputPaneVisible = false;
      InitializeTextEditor();
      InitializeCodeCompletionWindow();

      this.ContextMenuStrip = popUpTab;
      workerThread.WorkerSupportsCancellation = true;
      workerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(DoBackgroundWork);
      workerThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorkCompleted);
      
      InitiailizeActions();
    }

    public void InitializeScriptEditor( string caption, string script, int objType, ConnectionParams source, string initialCatalog )
    {
      try
      {
        _caption = caption;
        this.Text = _caption;
        this.TabText = _caption;

        InitializeForm();

        _isInitializing = true;

        if (source == null)
        {
          throw new NullReferenceException("Connection parameters object can not be null!");
        }


        _scriptText = script;
        _objectType = objType;
        _textEditor.Text = _scriptText;

        _connParams = source.CreateCopy();
        if (String.IsNullOrEmpty(initialCatalog))
        {
          _connParams.InitialCatalog = "master";
        }
        else
        {
          _connParams.InitialCatalog = initialCatalog;
        }

        _connParams.IsConnected = false;
        try
        {
          _conn.ConnectionString = _connParams.ConnectionString;
          _conn.Open();
          PopulateServers(_connParams.Name);
          PopulateDatabases(_connParams.InitialCatalog);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Can not open connection!\nException:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          cmbServers.Items.Clear();
          cmbServers.Items.Add(_connParams.Name);
          cmbServers.SelectedIndex = 0;
          _currentServerIndex = cmbServers.SelectedIndex;

          cmbDatabases.Items.Clear();
          cmbDatabases.Items.Add(_connParams.InitialCatalog);
          cmbDatabases.SelectedIndex = 0;
        }
      }
      finally
      {
        _isInitializing = false;
      }
    }

    private void PopulateServers( string defaultServerName )
    {
      cmbServers.Items.Clear();

      if (_conn.State != ConnectionState.Open)
      {
        return;
      }

      int serverIndex = -1;

      _connParamsCollection = ConnectionParamsFactory.GetConnections();
      foreach (ConnectionParams cp in _connParamsCollection)
      {
        cmbServers.Items.Add(cp.Name);
        if (defaultServerName.ToLowerInvariant() == cp.Name.ToLowerInvariant())
        {
          cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
          serverIndex = cmbServers.SelectedIndex;
        }
      }

      if (serverIndex == -1)
      {
        cmbServers.Items.Add(defaultServerName);
        cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
      }
      _currentServerIndex = serverIndex;
    }

    private void PopulateDatabases( string defaultDatabaseName )
    {
      cmbDatabases.Items.Clear();

      if (_conn.State != ConnectionState.Open)
      {
        return;
      }

      DataTable dbs = _conn.GetSchema("Databases");

      dbs.DefaultView.Sort = "database_name";
      dbs = dbs.DefaultView.ToTable();

      int dbIndex = -1;
      foreach (DataRow row in dbs.Rows)
      {
        string dbName = (string)row["database_name"];

        cmbDatabases.Items.Add(dbName);
        if (defaultDatabaseName.ToLowerInvariant() == dbName.ToLowerInvariant())
        {
          cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
          dbIndex = cmbDatabases.SelectedIndex;
        }
      }

      if (dbIndex == -1)
      {
        cmbDatabases.Items.Add(defaultDatabaseName);
        cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
      }
    }


    #endregion //Initialization

    #region Text editor related

    private TextArea ActiveTextArea
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

        while (offset >= 0 && !Char.IsWhiteSpace(ActiveTextArea.Document.GetCharAt(offset)))
        {
          offset--;
        }

        if (offset == -1)
        {
          return ActiveTextArea.Document.GetText(0, caretPos);
        }
        else
        {
          return ActiveTextArea.Document.GetText(offset + 1, caretPos - (offset + 1));
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
      _textEditor.ConvertTabsToSpaces = true;
      _textEditor.ShowEOLMarkers = false;
      _textEditor.ShowInvalidLines = false;
      _textEditor.ShowLineNumbers = true;
      _textEditor.ShowMatchingBracket = true;
      _textEditor.ShowSpaces = false;
      _textEditor.ShowTabs = false;
      _textEditor.IndentStyle = IndentStyle.Smart;
      _textEditor.TabIndent = 2;
      _textEditor.VRulerRow = 120;
      _textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
      _textEditor.ContextMenuStrip = popUpEditor;
      ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextEditorKeyDown);
      _textEditor.Document.DocumentChanged += new DocumentEventHandler(OnDocumentChanged);
      _textEditor.Focus();
    }

    private void InitializeCodeCompletionWindow( )
    {
      if (_codeCompWindow != null)
      {
        return;
      }

      _codeCompWindow = new CodeCompletionPresenter();
      RegisterForCodeCompletionEvents();
    }

    private void OnTextEditorKeyDown( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Space && e.Control)
      {
        e.SuppressKeyPress = true;
        ShowSelector();
      }
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

    private void ShowSelector( )
    {
      Point final = GetCaretPosition();
      _codeCompWindow.InitializeSelector(_conn);

      _codeCompWindow.SetLocation(final.X + 10, final.Y - 10);
      _codeCompWindow.ShowSelector();

      _codeCompWindow.JumpTo(GetPreviousNonWSLineParts);
    }

    private void RegisterForCodeCompletionEvents( )
    {
      if (_codeCompWindow == null)
      {
        return;
      }

      _codeCompWindow.KeyPressed += new KeyPressHandler(HandleCodeCompletionKeyPress);
      _codeCompWindow.UserMadeFinalSelection += new NotificationHandler(HandleCodeCompletionSelection);
      _codeCompWindow.UserPressedBackSpace += new NotificationHandler(HandleCodeCompletionBackSpace);
    }


    private void HandleCodeCompletionKeyPress( char c )
    {
      switch (c)
      {
        case (char)27: //ESC
          _codeCompWindow.DismissSelector();
          _textEditor.Focus();
          return;
        /*
        case '\b':
          return;
        case '\n':
          return;
        */
        default:
          if (Char.IsControl(c))
          {
            return;
          }
          ActiveTextArea.InsertChar(c);
          _codeCompWindow.JumpTo(GetPreviousNonWSLineParts);
          break;
      }
    }

    private void HandleCodeCompletionSelection( )
    {
      if (_codeCompWindow.Selector.HasMultipleSelection)
      {
        ActiveTextArea.InsertString(_codeCompWindow.SelectedItemsAsCommaSeparatedString);
      }
      else
      {
        DeleteWordBeforeCaret();
        ActiveTextArea.InsertString(_codeCompWindow.SelectedItem);
      }
      _codeCompWindow.DismissSelector();
      _textEditor.Focus();
    }

    private void MoveCaretToEOL( )
    {
      ActiveTextArea.Caret.Column = TextUtilities.GetLineAsString(ActiveTextArea.Document, ActiveTextArea.Caret.Line).Length;
    }

    private void HandleCodeCompletionBackSpace( )
    {
      new ICSharpCode.TextEditor.Actions.Backspace().Execute(ActiveTextArea);
      _codeCompWindow.JumpTo(GetPreviousNonWSLineParts);
    }

    public int DeleteWordBeforeCaret( )
    {
      int start = TextUtilities.FindPrevWordStart(ActiveTextArea.Document, ActiveTextArea.Caret.Offset);

      if (ActiveTextArea.Document.GetText(start, ActiveTextArea.Caret.Offset - start) != ".")
      {
        ActiveTextArea.Document.Remove(start, ActiveTextArea.Caret.Offset - start);
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
        HighlightRuleSet rules = ActiveTextArea.Document.HighlightingStrategy.GetRuleSet(null);
        IList<LineSegment> lines = ActiveTextArea.Document.LineSegmentCollection;
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
            ActiveTextArea.Document.Replace(segment.Offset + word.Offset, word.Length, newVal);
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
        HighlightRuleSet rules = ActiveTextArea.Document.HighlightingStrategy.GetRuleSet(null);
        IList<LineSegment> lines = ActiveTextArea.Document.LineSegmentCollection;
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
            if (rules.KeyWords[ActiveTextArea.Document, segment, word.Offset, word.Length] != null)
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
              ActiveTextArea.Document.Replace(segment.Offset + word.Offset, word.Length, newVal);
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

    public bool OpenScriptFromFile( string fileName )
    {
      string tmp = fileName;

      if (String.IsNullOrEmpty(fileName))
      {
        openFileDialog1.FileName = String.Empty;
        if (openFileDialog1.ShowDialog() != DialogResult.OK)
        {
          return false;
        }
        tmp = openFileDialog1.FileName;
      }

      _fileName = tmp;
      _textEditor.LoadFile(_fileName, false, true);
      statLblFileName.Text = _fileName;

      FileInfo fi = new FileInfo(_fileName);
      Caption = fi.Name;
      ScriptModified = false;

      return true;
    }

    public void SaveScriptAs( )
    {
      string objName = ObjectNameInEditor;
      saveFileDialog1.FileName = _caption;

      if (saveFileDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }


      _textEditor.SaveFile(saveFileDialog1.FileName);
      _fileName = saveFileDialog1.FileName;
      statLblFileName.Text = _fileName;

      FileInfo fi = new FileInfo(_fileName);

      Caption = fi.Name;
      ScriptModified = false;
    }

    public void SaveScript( )
    {
      if (String.IsNullOrEmpty(_fileName))
      {
        SaveScriptAs();
      }
      else
      {
        _textEditor.SaveFile(_fileName);
        FileInfo fi = new FileInfo(_fileName);
        this.TabText = fi.Name;
        this.Text = this.TabText;
        ScriptModified = false;
      }
    }

    #endregion //Script I/O

    #region Utilities

    public string ObjectNameInEditor
    {
      get
      {
        int objType = DBObjectType.None;
        bool isAlter = false;
        return ProgrammabilityHelper.GetObjectNameFromScript(_textEditor.Text, ref objType, ref isAlter);
      }
    }

    public bool OutputPaneVisible
    {
      get
      {
        return panOutput.Visible;
      }
      set
      {
        if (value)
        {
          panOutput.Height = (this.Height / 2) - 10;
        }
        panOutput.Visible = value;
        splitterOutput.Visible = value;
        splitterOutput.BringToFront();
        panEditor.BringToFront();
      }
    }

    private void InvalidateButtonsAndMenuItems( bool isRunning )
    {
      btnRun.Enabled = !isRunning;
      btnCheckSyntax.Enabled = !isRunning;
      btnShowPlan.Enabled = !isRunning;

      btnStop.Enabled = isRunning;


      btnSave.Enabled = !isRunning;
      btnSaveAs.Enabled = !isRunning;
      btnOpen.Enabled = !isRunning;
      cmbDatabases.Enabled = !isRunning;
      cmbServers.Enabled = !isRunning;



      mnuItemRun.Enabled = btnRun.Enabled;
      mnuItemCheckSyntax.Enabled = btnCheckSyntax.Enabled;
      mnuItemShowPlan.Enabled = btnShowPlan.Enabled;
      mnuItemStop.Enabled = btnStop.Enabled;
     
      cMnuItemSave.Enabled = !isRunning;
      mnuItemCheckSyntax.Enabled = !isRunning;
      
    }

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

    public void AddMessageToList( SqlMessage sqlMessage )
    {

      if (sqlMessage == null)
      {
        return;
      }


      ListViewItem item = lv.Items.Add(String.Empty, (int)sqlMessage.MsgType);
      item.UseItemStyleForSubItems = false;

      Color foreColor = Color.Navy;
      Color bgColor = Color.White;
      switch (sqlMessage.MsgType)
      {
        case MessageType.Info:
          foreColor = Color.Navy;
          break;
        case MessageType.Warning:
          foreColor = Color.Maroon;
          break;
        case MessageType.Error:
          foreColor = Color.Red;
          break;
        case MessageType.None:
        default:
          foreColor = Color.Black;
          break;

      }

      string lineText = String.Empty;
      string stateText = String.Empty;
      string typeText = String.Empty;
      if (sqlMessage.Line > 0)
      {
        lineText = sqlMessage.Line.ToString();
      }
      if (sqlMessage.Type > 0)
      {
        typeText = sqlMessage.Type.ToString();
      }
      if (sqlMessage.State > 0)
      {
        stateText = sqlMessage.State.ToString();
      }

      item.SubItems.Add(sqlMessage.Message, foreColor, bgColor, lv.Font);
      item.SubItems.Add(lineText, foreColor, bgColor, lv.Font);
      item.SubItems.Add(typeText, foreColor, bgColor, lv.Font);
      item.SubItems.Add(stateText, foreColor, bgColor, lv.Font);

      lv.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
      lv.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    public void ClearMessagesList( )
    {
      lv.Items.Clear();
    }

    public void CopyGridContentToClipboard( )
    {
      if (tabOutput.SelectedIndex <= 0)
      {
        return;
      }

      DataGridView grd = tabOutput.SelectedTab.Tag as DataGridView;
      if (grd == null)
      {
        return;
      }

      Clipboard.SetDataObject(grd.GetClipboardContent());
    }

    public void HideTextEditor( )
    {
      panEditor.Visible = false;

      mnuEdit.MergeAction = MergeAction.Insert;
      mnuFile.MergeAction = MergeAction.Insert;

      mnuEdit.Visible = false;
      mnuQuery.Visible = false;
      mnuFile.Visible = false;

      splitterOutput.Visible = false;
      toolStripContainer1.Visible = false;
      panOutput.Dock = DockStyle.Fill;
      panOutput.BringToFront();
    }

    #endregion //Utilities

    #region Query Execution


    private void ClearResults( )
    {
      try
      {
        tabOutput.SuspendLayout();
        ClearMessagesList();
        _sqlMessages.Clear();

        tabOutput.SelectTab(0);
        _grids.Clear();
        while (tabOutput.TabPages.Count > 1)
        {
          tabOutput.TabPages.Remove(tabOutput.TabPages[1]);
          Application.DoEvents();
        }
      }
      finally
      {
        tabOutput.ResumeLayout();
      }
    }

    public void RunScript( RunType scriptRunType )
    {

      timerExec.Enabled = true;
      if (_conn.State != ConnectionState.Open)
      {
        _conn.Open();
      }

      if (String.IsNullOrEmpty(_textEditor.Text))
      {
        return;
      }

      ClearResults();

      OutputPaneVisible = true;
      switch (scriptRunType)
      {
        case RunType.Execute:
          AddMessageToList(SqlMessage.CreateInfoMessage("Executing script. Please wait."));
          break;
        case RunType.CheckSyntax:
          AddMessageToList(SqlMessage.CreateInfoMessage("Checking syntax. Please wait."));
          break;
        case RunType.ShowPlan:
          AddMessageToList(SqlMessage.CreateInfoMessage("Showing plan. Please wait."));
          break;
      }

      ScriptData scriptData = new ScriptData();
      scriptData.SelStartLineNo = 0;

      string scriptText = String.Empty;
      if (ActiveTextArea.SelectionManager.HasSomethingSelected)
      {
        scriptText = ActiveTextArea.SelectionManager.SelectedText;
        scriptData.SelStartLineNo = Int32.MaxValue;
        foreach (ISelection sel in ActiveTextArea.SelectionManager.SelectionCollection)
        {
          if (sel.StartPosition.Y < scriptData.SelStartLineNo)
          {
            scriptData.SelStartLineNo = sel.StartPosition.Y;
          }
        }
      }
      else
      {
        scriptText = _textEditor.Text;
      }

      scriptData.ScriptText = scriptText;
      scriptData.ScriptRunType = scriptRunType;

      workerThread.RunWorkerAsync(scriptData);

      InvalidateButtonsAndMenuItems(true);
    }

    private void StopExecution( )
    {
      timerExec.Enabled = false;
      workerThread.CancelAsync();
      if (_cmd != null)
      {
        _cmd.Cancel();
      }
    }

    private void DoBackgroundWork( object sender, DoWorkEventArgs e )
    {

      // Do not access the form's BackgroundWorker reference directly.
      // Instead, use the reference provided by the sender parameter.
      BackgroundWorker bw = sender as BackgroundWorker;

      // Extract the argument.
      ScriptData arg = (ScriptData)e.Argument;


      // Start the time-consuming operation.
      e.Result = ExecuteScript(bw, arg);

      // If the operation was canceled by the user, 
      // set the DoWorkEventArgs.Cancel property to true.
      if (bw.CancellationPending)
      {
        e.Cancel = true;
      }
    }

    private void BackgroundWorkCompleted( object sender, RunWorkerCompletedEventArgs e )
    {
      _isExecuting = false;
      timerExec.Enabled = false;
      TimeSpan startSpan = TimeSpan.FromTicks(_startTime.Ticks);
      TimeSpan endSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);

      TimeSpan diff = endSpan - startSpan;
      diff = endSpan.Subtract(startSpan);


      statLblQueryCompletionTime.Text = "Elapsed: " + diff.Hours.ToString("00")
        + ":" + diff.Minutes.ToString("00")
        + ":" + diff.Seconds.ToString("00")
        + ":" + diff.Milliseconds.ToString("000"); ;


      if (e.Cancelled)
      {
        _sqlMessages.Clear();
        _sqlMessages.Add(SqlMessage.CreateWarningMessage("Operation cancelled by the user."));
      }
      else if (e.Error != null)
      {
        // There was an error during the operation.
        string msg = String.Format("An error occurred: {0}", e.Error.Message);
        //MessageBox.Show(msg);
      }
      else
      {
        //Completed normally
        RenderResults((IList<DataSet>)e.Result);
      }

      if (_completedWithErrors)
      {
        System.Media.SystemSounds.Exclamation.Play();
      }

      RenderMessagesAndErrors();
      InvalidateButtonsAndMenuItems(false);
      if (tabOutput.TabPages.Count > 1 && !_completedWithErrors)
      {
        tabOutput.SelectTab(1);
      }
      else
      {
        tabOutput.SelectTab(0);
      }
    }

    private IList<DataSet> ExecuteScript( BackgroundWorker bw, ScriptData scriptData )
    {
      _isExecuting = true;
      _completedWithErrors = false;
      _startTime = DateTime.Now;

      if (_conn.State != ConnectionState.Open)
      {
        _sqlMessages.Add(SqlMessage.CreateErrorMessage("Invalid connection state: " + _conn.State.ToString(), -1, -1, -1));
        return new List<DataSet>();
      }

      int totalLineCnt = 0;
      int currentLineCnt = 0;
      string completionMessage = String.Empty;


      switch (scriptData.ScriptRunType)
      {
        case RunType.Execute:
          completionMessage = "Command(s) completed.";
          break;
        case RunType.CheckSyntax:
          completionMessage = "Syntax check completed.";
          break;
        case RunType.ShowPlan:
          completionMessage = "Show plan completed.";
          break;
      }

      _grids.Clear();
      IList<DataSet> dataSets = new List<DataSet>();
      if (workerThread.CancellationPending)
      {
        return dataSets;
      }

      _conn.InfoMessage += new SqlInfoMessageEventHandler(HandleSqlInfo);
      try
      {
        switch (scriptData.ScriptRunType)
        {
          case RunType.CheckSyntax:
            _cmd = new SqlCommand("SET PARSEONLY ON", _conn);
            _cmd.CommandTimeout = 30;
            _cmd.ExecuteNonQuery();
            break;
          case RunType.ShowPlan:
            _cmd = new SqlCommand("SET SHOWPLAN_ALL ON", _conn);
            _cmd.CommandTimeout = 30;
            _cmd.ExecuteNonQuery();
            break;
          default:
            break;
        }


        IList<string> batches = ProgrammabilityHelper.SplitBatches(scriptData.ScriptText);
        while (batches.Count > 0)
        {
          try
          {
            DataSet toFill = new DataSet();
            dataSets.Add(toFill);

            string batch = batches[0];

            currentLineCnt = LineCount(batch);
            currentLineCnt++;
            totalLineCnt += currentLineCnt;

            if (String.IsNullOrEmpty(batch))
            {
              batches.RemoveAt(0);
              _sqlMessages.Add(SqlMessage.CreateInfoMessage(completionMessage));
              _sqlMessages.Add(SqlMessage.CreateMessage(""));
              continue;
            }

            batches.RemoveAt(0);

            _cmd = new SqlCommand(batch, _conn);
            _cmd.CommandTimeout = 30;


            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = _cmd;
            int recordsAffected = adapter.Fill(toFill);

            if (batches.Count == 0)
            {
              _sqlMessages.Add(SqlMessage.CreateInfoMessage(completionMessage));
              _sqlMessages.Add(SqlMessage.CreateMessage(""));
            }

            if (toFill.Tables.Count == 0 && scriptData.ScriptRunType != RunType.Execute)
            {
              _sqlMessages.Add(SqlMessage.CreateInfoMessage("( " + recordsAffected.ToString() + " row(s) affected ) "));
            }

          }
          catch (SqlException sqlEx)
          {
            _completedWithErrors = true;
            int lineNo = totalLineCnt - currentLineCnt + sqlEx.LineNumber + scriptData.SelStartLineNo;
            _sqlMessages.Add(SqlMessage.CreateErrorMessage(sqlEx.Message, lineNo, sqlEx.Number, sqlEx.State));

          }
          catch (Exception ex)
          {
            _completedWithErrors = true;
            _sqlMessages.Add(SqlMessage.CreateErrorMessage("Exception of type \"" + ex.GetType().ToString() + "\": " + ex.Message, -1, -1, -1));
          }
        }
      }
      finally
      {
        switch (scriptData.ScriptRunType)
        {
          case RunType.CheckSyntax:
          _cmd = new SqlCommand("SET PARSEONLY OFF", _conn);
          _cmd.CommandTimeout = 30;
          _cmd.ExecuteNonQuery();
            break;
          case RunType.ShowPlan:
          _cmd = new SqlCommand("SET SHOWPLAN_ALL OFF", _conn);
          _cmd.CommandTimeout = 30;
          _cmd.ExecuteNonQuery();
            break;
          default:
            break;
        }
        _conn.InfoMessage -= new SqlInfoMessageEventHandler(HandleSqlInfo);
      }

      return dataSets;
    }


    private void HandleSqlInfo( object o, SqlInfoMessageEventArgs e )
    {
      foreach (SqlError err in e.Errors)
      {
        _sqlMessages.Add(SqlMessage.CreateInfoMessage(err.Message));
      }
    }

    #endregion //Query Execution

    #region Rendering functions

    private void RenderResults( IList<DataSet> dataSets )
    {
      if (dataSets != null && dataSets.Count > 0)
      {
        int queryNo = 0;
        int batchNo = 0;

        foreach (DataSet ds in dataSets)
        {
          batchNo++;
          if (ds != null && ds.Tables.Count > 0)
          {
            for (int i = 0; i < ds.Tables.Count; i++)
            {
              queryNo = i + 1;
              int recordCnt = ds.Tables[i].Rows.Count;

              if (i == 0)
              {
                _sqlMessages.Add(SqlMessage.CreateInfoMessage("Batch #" + batchNo.ToString() + " : " + ds.Tables.Count.ToString() + " resultset(s) were returned."));
              }

              _sqlMessages.Add(SqlMessage.CreateInfoMessage("Query " + queryNo.ToString() + ": Returned " + recordCnt.ToString() + " record(s)."));

              // create tab page
              tabOutput.TabPages.Add("Query " + queryNo.ToString() + " [" + recordCnt.ToString() + "]");

              //crate individual grids
              DataGridView grd = new DataGridView();
              grd.DataSource = ds.Tables[i];
              grd.ReadOnly = true;
              grd.Parent = tabOutput.TabPages[tabOutput.TabPages.Count - 1];
              tabOutput.TabPages[tabOutput.TabPages.Count - 1].Tag = grd;

              grd.Dock = DockStyle.Fill;

              grd.CellPainting += new DataGridViewCellPaintingEventHandler(OnCellPainting);
              grd.DataError += new DataGridViewDataErrorEventHandler(OnDataError);

              grd.AllowUserToAddRows = false;
              grd.AllowUserToResizeRows = true;
              grd.BorderStyle = BorderStyle.None;
              grd.ShowEditingIcon = false;
              grd.BackgroundColor = SystemColors.Window;

              grd.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
              grd.ContextMenuStrip = popUpGrid;
              _grids.Add(grd);
            }
            _sqlMessages.Add(SqlMessage.CreateMessage(""));
          }
        }
      }
      else
      {
        _grids.Clear();
      }
    }

    private void OnDataError( object sender, DataGridViewDataErrorEventArgs e )
    {
      e.ThrowException = false;
    }
    private void OnCellPainting( object sender, DataGridViewCellPaintingEventArgs e )
    {
      if (e.ColumnIndex == -1)
      {
        Color bgColor = SystemColors.Control;
        Color foreColor = Color.Black;

        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
        {
          bgColor = SystemColors.Highlight;
          foreColor = SystemColors.HighlightText;
        }

        Color gridBrushColor = ((DataGridView)sender).GridColor;
        using (Brush gridBrush = new SolidBrush(gridBrushColor))
        {
          using (Pen gridLinePen = new Pen(gridBrush))
          {
            e.Graphics.FillRectangle(new SolidBrush(bgColor), e.CellBounds);
            // Draw the grid lines (only the right and bottom lines;
            // DataGridView takes care of the others).
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
              e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
              e.CellBounds.Bottom - 1);
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
              e.CellBounds.Top, e.CellBounds.Right - 1,
              e.CellBounds.Bottom);

            if (e.RowIndex != -1)
            {
              e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.CellStyle.Font,
                      new SolidBrush(foreColor), e.CellBounds.X + 2,
                      e.CellBounds.Y + 2, StringFormat.GenericDefault);
            }
            e.Handled = true;
          }
        }
      }
      else if ((e.Value != null) && (e.Value.GetType() == typeof(DBNull)))
      {
        Color gridBrushColor = ((DataGridView)sender).GridColor;
        Color bgColor = Color.LemonChiffon;

        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
        {
          bgColor = SystemColors.Highlight;
        }

        Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
            e.CellBounds.Y + 1, e.CellBounds.Width - 4,
            e.CellBounds.Height - 4);


        using (
            Brush gridBrush = new SolidBrush(gridBrushColor),
            backColorBrush = new SolidBrush(bgColor))
        {


          using (Pen gridLinePen = new Pen(gridBrush))
          {

            // Erase the cell.
            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            // Draw the grid lines (only the right and bottom lines;
            // DataGridView takes care of the others).
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                e.CellBounds.Bottom - 1);
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                e.CellBounds.Top, e.CellBounds.Right - 1,
                e.CellBounds.Bottom);

            // Draw the inset highlight box.
            //e.Graphics.DrawRectangle(Pens.Blue, newRect);


            // Draw the text content of the cell, ignoring alignment.
            Brush br = null;
            if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected)
            {
              br = Brushes.Black;
            }
            else
            {
              br = SystemBrushes.HighlightText;
            }

            e.Graphics.DrawString((string)"(NULL)", e.CellStyle.Font,
                br, e.CellBounds.X + 2,
                e.CellBounds.Y + 2, StringFormat.GenericDefault);
            e.Handled = true;
          }

        }

      }

    }

    private void RenderMessagesAndErrors( )
    {
      OutputPaneVisible = true;
      foreach (SqlMessage msg in _sqlMessages)
      {
        AddMessageToList(msg);
      }
    }

    private void ExportGridToFile( )
    {
      DataGridView grd = tabOutput.SelectedTab.Tag as DataGridView;
      if (grd == null)
      {
        return;
      }

      if (saveDlgExport.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      DataExport.ExportFormat format = DataExport.ExportFormat.CSV;
      if (saveDlgExport.FilterIndex == 1)
      {
        format = DataExport.ExportFormat.Excel;
      }
      else if (saveDlgExport.FilterIndex == 2)
      {
        format = DataExport.ExportFormat.CSV;
      }

      DataTable dt = grd.DataSource as DataTable;
      if (dt == null)
      {
        throw new Exception("DataTable can not be extracted from the current gird!");
      }

      if (saveDlgExport.FilterIndex != 3)
      {
        DataExport exporter = new DataExport("Win");

        exporter.ExportDetails(dt, format, saveDlgExport.FileName);
      }
      else if (saveDlgExport.FilterIndex == 3)
      {
        dt.WriteXml(saveDlgExport.FileName);
      }
    }
    #endregion //Rendering function

    #region GoTo Line

    public void ShowGoToLineDialog( )
    {
      if (_frmGoToLine == null)
      {
        _frmGoToLine = new frmGoToLine();
        _frmGoToLine.FormClosed += new FormClosedEventHandler(OnGoToFormClosed);
        _frmGoToLine.GoToLineRequested += new GoToLineEventHandler(OnGoToLine);
      }

      _frmGoToLine.MaxLineCnt = ActiveTextArea.Document.TotalNumberOfLines;
      _frmGoToLine.Show();
      _frmGoToLine.TopMost = true;
    }

    private void OnGoToFormClosed( object sender, FormClosedEventArgs e )
    {
      _frmGoToLine = null;
    }

    private void OnGoToLine( object sender, int lineNo )
    {
      if (lineNo <= 0 || lineNo > ActiveTextArea.Document.TotalNumberOfLines)
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
      int totalNumOfLines = ActiveTextArea.Document.TotalNumberOfLines;
      string LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
      LineText = ActiveTextArea.Document.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

      int indexOf = LineText.IndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
      int offset = colNo;

      if (indexOf < 0)
      {
        offset = 0;
        do
        {
          int tmpLineNo = ActiveTextArea.Document.GetNextVisibleLineAbove(lineNo, 1);
          if (tmpLineNo == lineNo)
          {
            break;
          }
          lineNo = tmpLineNo;
          LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
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

      string LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
      LineText = LineText.Substring(0, ActiveTextArea.Caret.Column);

      int indexOf = LineText.LastIndexOf(matchText, StringComparison.InvariantCultureIgnoreCase);
      if (indexOf < 0)
      {
        do
        {
          int tmpLineNo = ActiveTextArea.Document.GetNextVisibleLineBelow(lineNo, 1);
          if (tmpLineNo == lineNo)
          {
            break;
          }
          lineNo = tmpLineNo;
          LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);

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
      int totalNumOfLines = ActiveTextArea.Document.TotalNumberOfLines;
      string LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
      LineText = ActiveTextArea.Document.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

      Match m = regularExpression.Match(LineText);
      int offset = colNo;

      if (!m.Success)
      {
        offset = 0;
        do
        {
          int tmpLineNo = ActiveTextArea.Document.GetNextVisibleLineAbove(lineNo, 1);
          if (tmpLineNo == lineNo)
          {
            break;
          }
          lineNo = tmpLineNo;
          LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
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

      string LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
      LineText = ActiveTextArea.Document.GetText(ActiveTextArea.Caret.Offset, LineText.Length - colNo);

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

              ActiveTextArea.Document.Replace(ActiveTextArea.Caret.Offset, m.Length, replaceText);
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

              ActiveTextArea.Document.Replace(ActiveTextArea.Caret.Offset, m.Length, replaceText);

              endPoint.X = startPoint.X + replaceText.Length;
              ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);

              ActiveTextArea.Caret.Line = lineNo;
              ActiveTextArea.Caret.Column = endPoint.X;

              LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
              LineText = ActiveTextArea.Document.GetText(ActiveTextArea.Caret.Offset, LineText.Length - endPoint.X);

              replaceCnt++;

              m = regularExpression.Match(LineText);
              matchIndex = m.Index + endPoint.X;

              LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
            }
            while (m.Success);
          }

        }


        int tmpLineNo = ActiveTextArea.Document.GetNextVisibleLineAbove(lineNo, 1);
        if (tmpLineNo == lineNo)
        {
          break;
        }
        lineNo = tmpLineNo;
        LineText = TextUtilities.GetLineAsString(ActiveTextArea.Document, lineNo);
        offset = 0;
      }
      while (lineNo < ActiveTextArea.Document.TotalNumberOfLines);

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
   

    private void cmbDatabases_SelectedIndexChanged( object sender, EventArgs e )
    {
      string tmp = String.Empty;

      if (_isInitializing)
      {
        return;
      }

      if (_conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
      }
      try
      {
        tmp = _conn.Database;
        _conn.ChangeDatabase(cmbDatabases.Text);
      }
      catch
      {
        MessageBox.Show("Can not change connection to selected database!\nPrevious database will be restored.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _conn.ChangeDatabase(tmp);
        _isInitializing = true;
        cmbDatabases.Text = tmp;
        _isInitializing = false;
      }
    }

    private void cmbServers_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }


      if (_conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
      }

      if (_connParamsCollection == null)
      {
        throw new Exception("Connections collection is null!");
      }

      string tmp = String.Empty;
      tmp = _conn.ConnectionString;



      ConnectionParams cp = _connParamsCollection[cmbServers.SelectedIndex];
      if (cp == null)
      {
        MessageBox.Show("Connection to selected server does not exist anymore!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        _conn.Close();
        _conn.ConnectionString = cp.ConnectionString;
        _conn.Open();
        _currentServerIndex = cmbServers.SelectedIndex;
        if (String.IsNullOrEmpty(cp.InitialCatalog))
        {
          cp.InitialCatalog = "master";
        }
        PopulateDatabases(cp.InitialCatalog);
      }
      catch
      {
        MessageBox.Show("Can not connect to selected server!\nPrevious connection will be restored.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _conn.ConnectionString = tmp;
        _conn.Open();

        _isInitializing = true;
        cmbServers.SelectedIndex = _currentServerIndex;
        _isInitializing = false;
      }
    }


    private void lblCloseoutputPane_MouseEnter( object sender, EventArgs e )
    {
      lblCloseoutputPane.ForeColor = Color.FromKnownColor(KnownColor.PaleVioletRed);
    }

    private void lblCloseoutputPane_MouseLeave( object sender, EventArgs e )
    {
      lblCloseoutputPane.ForeColor = Color.White;
    }

    private void lblCloseoutputPane_MouseClick( object sender, MouseEventArgs e )
    {
      OutputPaneVisible = false;
    }

    private void lblCloseoutputPane_MouseDown( object sender, MouseEventArgs e )
    {
      lblCloseoutputPane.ForeColor = Color.FromKnownColor(KnownColor.Crimson);
    }

    private void lblCloseoutputPane_MouseUp( object sender, MouseEventArgs e )
    {
      lblCloseoutputPane.ForeColor = Color.FromKnownColor(KnownColor.PaleVioletRed);
    }

    private void lv_DoubleClick( object sender, EventArgs e )
    {

      try
      {
        if (lv.SelectedItems.Count == 0)
        {
          return;
        }

        ListViewItem selItem = lv.SelectedItems[0];
        string selText = selItem.SubItems[2].Text;
        if (String.IsNullOrEmpty(selText))
        {
          return;
        }

        int lineNo = Convert.ToInt32(selText);
        if (lineNo > ActiveTextArea.Document.TotalNumberOfLines)
        {
          MessageBox.Show("Can not locate line in script.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        ActiveTextArea.Caret.Column = 0;
        ActiveTextArea.Caret.Line = lineNo - 1;
        ActiveTextArea.Select();
        Point startPoint = ActiveTextArea.Caret.Position;
        Point endPoint = ActiveTextArea.Caret.Position;
        endPoint.X = endPoint.X + ActiveTextArea.Document.GetLineSegment(lineNo - 1).Length;
        ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Can not locate line in script.\n\r" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private void btnToggleBlockComment_Click( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.ToggleBlockComment().Execute(ActiveTextArea);
    }

    private void btnToggleLineComment_Click( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.ToggleLineComment().Execute(ActiveTextArea);
    }

   


    private void mnuItemSaveAs_Click( object sender, EventArgs e )
    {
      SaveScriptAs();
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


    private void frmScriptEditor_Load( object sender, EventArgs e )
    {
      statLblQueryCompletionTime.Text = "Elapsed: 00:00:00:000";
      toolStripContainer1.Height = strip2.Height + strip3.Height;
    }

    private void timer1_Tick( object sender, EventArgs e )
    {
      TimeSpan startSpan = TimeSpan.FromTicks(_startTime.Ticks);
      TimeSpan endSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);

      TimeSpan diff = endSpan - startSpan;
      diff = endSpan.Subtract(startSpan);


      statLblQueryCompletionTime.Text = "Elapsed: " + diff.Hours.ToString("00")
        + ":" + diff.Minutes.ToString("00")
        + ":" + diff.Seconds.ToString("00")
        + ":" + diff.Milliseconds.ToString("000"); ;
    }


    private void frmScriptEditor_FormClosing( object sender, FormClosingEventArgs e )
    {
      if (!CheckSave || !ScriptModified)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Save changes to \"" + _caption + "\"", "Save File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
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

    private void exportToFileToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ExportGridToFile();
    }

    

    private void lblCloseoutputPane_Click( object sender, EventArgs e )
    {
      OutputPaneVisible = false;
    }

  }
}