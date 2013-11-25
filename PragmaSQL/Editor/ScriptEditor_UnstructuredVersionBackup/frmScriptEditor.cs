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
using System.Reflection;
using WeifenLuo.WinFormsUI;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Crad.Windows.Forms.Actions;
using AsynchronousCodeBlocks;

using com.calitha.goldparser;

using PragmaSQL;
using PragmaSQL.Common;
using PragmaSQL.Database;


namespace PragmaSQL
{
  public partial class frmScriptEditor : DockContent, IPragmaEditor
  {
    private IList<ConnectionParams> _cpList = new List<ConnectionParams>();
    private TextEditorControl _textEditor = null;
    private CodeCompletionPresenter _codeCompWindow;
    private CodeCompletionPresenterEx _codeCompWindowEx;
    private SqlConnection _conn = new SqlConnection();
    private SqlCommand _cmd = null;
    private BackgroundWorker _scriptExecuterThread = new BackgroundWorker();
    private IList<DataGridView> _grids = new List<DataGridView>();
    private IList<SqlMessage> _sqlMessages = new List<SqlMessage>();
    private frmSearchAndReplace _frmSearchAndReplace = null;
    private frmGoToLine _frmGoToLine = null;
    private DateTime _startTime = DateTime.Now;
    private bool _completedWithErrors = false;
    private int _currentServerIndex = -1;
    private bool _isExecuting = false;
    private ActionList _actionList = new ActionList();
    private ObjectHelperPopupBuilder _objHelperPopupBuilder = null;

    #region Properties

    private ConnectionParams _connParams;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
    }

    private bool _initializing = false;
    public bool Initializing
    {
      get { return _initializing; }
    }

    public bool AutoRemoveModSignAfterCommit
    {
      get
      {
        if (ConfigurationHelper.Current == null || ConfigurationHelper.Current.TextEditorOptions == null)
        {
          return true;
        }
        else
        {
          return ConfigurationHelper.Current.TextEditorOptions.AutoRemoveModifiedSignForKnownScriptableObjects;
        }
      }
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

    private bool _contentSaved = true;
    public bool ContentSaved
    {
      get { return _contentSaved; }
      set
      {
        _contentSaved = value;
        statContentModified.Visible = !_contentSaved;
      }
    }


    #endregion

    #region Sql Shallow Parser Related Fields And Properties
    
    private Stack<Paranthesis> _openingParanths = new Stack<Paranthesis>();
    private StringTokenizer _tokenizer = null;
    private SqlParserResult _bgParseResults = null;
    private bool _parseSelectsAndCases = false;
    private bool _parseCodeBlocks = false;

    private Color _selectBlockColor = Color.LightGray;
    private Color _caseBlockColor = Color.LightGray;
    private SqlStatement _currentSqlStmn = null;

    private List<FoldMarker> _selectCaseFoldMarkers = new List<FoldMarker>();
    private List<FoldMarker> _codeBlockFoldMarkers = new List<FoldMarker>();
    private List<FoldMarker> MergedFoldMarkers
    {
      get
      {
        List<FoldMarker> result = new List<FoldMarker>();
        result.AddRange(_selectCaseFoldMarkers);
        result.AddRange(_codeBlockFoldMarkers);
        return result;
      }
    }

    #endregion

    #region Constructor

    public frmScriptEditor( )
    {
      InitializeComponent();
      InitializeGoldParserEngine();
      InitializeParserThread();
    }

    #endregion

    #region Initialization

    private ConnectionParamsCollection Connections
    {
      get
      {
        return ConnectionParamsFactory.GetConnections();
      }
    }

    private void InitializeForm( )
    {

      OutputPaneVisible = false;

      SharedScriptOperationsVisible = ConfigurationHelper.Current.PragmaSql_SharedScriptsEnabled;
      InitializeTextEditor();
      InitializeCodeCompletionWindow();
      InitializeCodeCompletionWindow_Ex();

      this.ContextMenuStrip = popUpTab;

      _scriptExecuterThread.WorkerSupportsCancellation = true;
      _scriptExecuterThread.DoWork += new System.ComponentModel.DoWorkEventHandler(DoExecuteScriptInBackground);
      _scriptExecuterThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(OnScriptExecutionCompleted);

      InitiailizeActions();

      _objHelperPopupBuilder = new ObjectHelperPopupBuilder(popUpEditor);
      _objHelperPopupBuilder.ObjectHelpRequested += new ObjectHelpActionEventHandler(OnObjectHelpRequested);

      ApplyTextEditorOptionsFromCurrentConfig();
    }

    public void InitializeScriptEditor( string caption, string script, int objType, ConnectionParams source, string initialCatalog )
    {
      try
      {
        _caption = caption;
        this.Text = _caption;
        this.TabText = _caption;

        InitializeForm();

        _initializing = true;

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
          _connParams.Database = "master";
        }
        else
        {
          _connParams.Database = initialCatalog;
        }

        _connParams.IsConnected = false;
        try
        {
          _conn.ConnectionString = _connParams.ConnectionString;
          _conn.Open();
          PopulateServers(_connParams);
          PopulateDatabases(_connParams.Database);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Can not open connection!\nException:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          cmbServers.Items.Clear();
          cmbServers.Items.Add(_connParams.Server);
          cmbServers.SelectedIndex = 0;
          _currentServerIndex = cmbServers.SelectedIndex;

          cmbDatabases.Items.Clear();
          cmbDatabases.Items.Add(_connParams.Database);
          cmbDatabases.SelectedIndex = 0;
        }
      }
      finally
      {
        _initializing = false;
      }
    }

    private void InitializeGoldParserEngine( )
    {
      /*
       * Code block below instantiates tokenizer for each script editor. This
       * method is consumes too much memory and worst is memory can not be released
       * properly.
       * The solution is using single application wide instance of the tokenizer 
       * object. Since only one editor can be modified by the user no sharing problems
       * are expected.
       */

      /*
      Assembly thisExe;
      thisExe = System.Reflection.Assembly.GetExecutingAssembly();
      Stream file = thisExe.GetManifestResourceStream("PragmaSQL.sqlselect.cgt");
      CGTReader reader = new CGTReader(file);
      _tokenizer = reader.CreateNewTokenizer();
      */
      _tokenizer = GoldTokenizer.Tokenizer;
    }

    private void PopulateServers( ConnectionParams defaultParams )
    {
      _cpList.Clear();
      if (_conn.State != ConnectionState.Open)
      {
        return;
      }

      int serverIndex = -1;
      bool defaultIsInList = false;
      ConnectionParamsCollection cons = Connections;
      foreach (ConnectionParams cp in cons)
      {
        cmbServers.Items.Add(cp.Server);
        if (defaultParams != null && defaultParams.Server.ToLowerInvariant() == cp.Server.ToLowerInvariant())
        {
          cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
          serverIndex = cmbServers.SelectedIndex;
          defaultIsInList = true;
        }
        _cpList.Add(cp);
      }

      if (!defaultIsInList)
      {
        cmbServers.Items.Add(defaultParams.Server);
        cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
        _cpList.Add(defaultParams);
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

    #region Code Completion

    private void InitializeCodeCompletionWindow( )
    {
      if (_codeCompWindow != null)
      {
        return;
      }

      _codeCompWindow = new CodeCompletionPresenter();
      RegisterForCodeCompletionEvents();
    }

    private void InitializeCodeCompletionWindow_Ex( )
    {
      if (_codeCompWindowEx != null)
      {
        return;
      }

      _codeCompWindowEx = new CodeCompletionPresenterEx();
      RegisterForCodeCompletionEventsEx();
    }

    private void ShowSelector( )
    {
      Point final = GetCaretPosition();
      _codeCompWindow.SetConnection(_conn);

      string sqlText = ActiveDocument.TextContent;
      sqlText = SqlAnalyzer.RemoveComments(sqlText);

      SqlAnalyzerResults r = SqlAnalyzer.AnalyzeSql(sqlText, false);

      _codeCompWindow.JumpTo(GetPreviousNonWSLineParts, r);
      _codeCompWindow.SetLocation(final.X + 10, final.Y - 10);
      _codeCompWindow.ShowSelector();

    }

    private void ShowSelector_Ex( )
    {
      Point final = GetCaretPosition();
      _codeCompWindowEx.JumpTo(GetPreviousNonWSLineParts);
      _codeCompWindowEx.SetLocation(final.X + 10, final.Y - 10);
      _codeCompWindowEx.ShowSelector();

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


    private void HandleCodeCompletionKeyPress( char c )
    {
      switch (c)
      {
        case (char)27: //ESC
          _codeCompWindow.DismissSelector();
          _textEditor.Focus();
          return;
        default:
          if (Char.IsControl(c))
          {
            return;
          }
          ActiveTextArea.InsertChar(c);
          _codeCompWindow.JumpTo(GetPreviousNonWSLineParts, null);
          break;
      }
    }

    private void HandleCodeCompletionSelection( )
    {
      try
      {
        ActiveTextArea.BeginUpdate();
        if (_codeCompWindow.Selector.HasMultipleSelection)
        {
          ActiveTextArea.InsertString(_codeCompWindow.SelectedItemsAsCommaSeperatedString);
        }
        else
        {
          DeleteWordBeforeCaret();
          ActiveTextArea.InsertString(_codeCompWindow.SelectedItem);
        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
      _codeCompWindow.DismissSelector();
      _textEditor.Focus();
    }

    private void HandleCodeCompletionBackSpace( )
    {
      new ICSharpCode.TextEditor.Actions.Backspace().Execute(ActiveTextArea);
      _codeCompWindow.JumpTo(GetPreviousNonWSLineParts, null);
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

    #endregion

    #region Text editor related Utils

    public ITextEditorProperties ActiveTextEditorProps
    {
      get
      {
        if (ActiveTextArea == null || ActiveTextArea.MotherTextAreaControl == null)
        {
          return null;
        }
        else
        {
          return ActiveTextArea.TextEditorProperties;
        }
      }
    }

    private bool CanFoldSelectCase
    {
      get
      {
        if
          (
            ConfigurationHelper.Current == null
            || ConfigurationHelper.Current.TextEditorOptions == null
            || ActiveTextArea == null
            || ActiveTextEditorProps == null
          )
        {
          return false;
        }

        TextEditorOptions opts = ConfigurationHelper.Current.TextEditorOptions;
        return (opts != null && opts.ShowFoldMarkers && opts.FoldHighlightedSelectAndCase && opts.HighlightSelectAndCase);
      }
    }

    private bool CanFoldCodeBlocks
    {
      get
      {
        if
          (
            ConfigurationHelper.Current == null
            || ConfigurationHelper.Current.TextEditorOptions == null
            || ActiveTextArea == null
            || ActiveTextEditorProps == null
          )
        {
          return false;
        }
        TextEditorOptions opts = ConfigurationHelper.Current.TextEditorOptions;
        return (opts != null && opts.ShowFoldMarkers && opts.FoldCodeBlocks);
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
        char c = ActiveDocument.GetCharAt(offset);
        while (
          offset >= 0
          && !Char.IsWhiteSpace(c)
          )
        {
          if (c == '(' || c == ')')
          {
            break;
          }
          offset--;
          if (offset >= 0)
          {
            c = ActiveDocument.GetCharAt(offset);
          }
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

      ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextEditorKeyDown);
      ActiveTextArea.Caret.PositionChanged += new EventHandler(OnCaretPositionChanged);
      ActiveTextEditorProps.UseCustomLine = true;
      _textEditor.Focus();
    }

    void OnCaretPositionChanged( object sender, EventArgs e )
    {
      statCaretPos.Text = "Ln: " + (ActiveTextArea.Caret.Line + 1).ToString()
        + " , Col: " + (ActiveTextArea.Caret.Column + 1).ToString()
        + " ( Offset: " + ActiveTextArea.Caret.Offset.ToString() + " )";

      if (_parseSelectsAndCases && !ActiveTextArea.SelectionManager.HasSomethingSelected)
      {
        //HighlightStatmentsByOffset();
        HighlightStatmentsByLineNr();
      }
    }

    private void OnTextEditorKeyDown( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Space && e.Control)
      {
        e.SuppressKeyPress = true;
        ShowSelector();
      }
      else if (e.KeyCode == Keys.J && e.Control)
      {
        e.SuppressKeyPress = true;
        ShowSelector_Ex();
      }
    }

    private void OnDocumentChanged( object sender, DocumentEventArgs e )
    {
      if (e.Document.UndoStack.CanUndo)
      {
        ContentModified = true;
        _lastModifiedOn = DateTime.Now;
      }
      else
      {
        ContentModified = false;
        ContentSaved = true;
        _lastModifiedOn = null;
      }

      if (_parseSelectsAndCases || _parseCodeBlocks)
      {
        RunParserThread();
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

    private void MarkSelectionAsCodeBlock( )
    {
      if (!ActiveTextArea.SelectionManager.HasSomethingSelected)
      {
        return;
      }

      try
      {
        ActiveTextArea.BeginUpdate();

        int startOffset = Int32.MaxValue;
        foreach (ISelection sel in ActiveTextArea.SelectionManager.SelectionCollection)
        {
          if (sel.Offset < startOffset)
          {
            startOffset = sel.Offset;
          }
        }

        string codeBlockComment = "New Code Block";

        if (InputDialog.ShowDialog("Code Block From Selection", "Comment", ref codeBlockComment) != DialogResult.OK)
        {
          return;
        }

        string selText = ActiveTextArea.SelectionManager.SelectedText;
        selText = "\n-- #block < " + codeBlockComment + " >\n" + selText;
        selText = selText + "\n-- #endblock < " + codeBlockComment + " >\n";

        ActiveTextArea.SelectionManager.RemoveSelectedText();

        Point p = ActiveDocument.OffsetToPosition(startOffset);
        ActiveTextArea.Caret.Line = p.Y;
        ActiveTextArea.Caret.Column = p.X;

        ActiveTextArea.InsertString(selText);
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
    }

    public void AppendScriptAsCodeBlock( string script, string codeBlockComment )
    {
      try
      {
        ActiveTextArea.BeginUpdate();
        string appendText = "\n-- #block < " + codeBlockComment + " >\n" + script;
        appendText = appendText + "\n-- #endblock < " + codeBlockComment + " >\n";
        ActiveDocument.TextContent += appendText;
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
    }

    #endregion //Text editor related Utils

    #region Utilities

    public string ObjectNameInEditor
    {
      get
      {
        int objType = DBObjectType.None;
        bool isAlter = false;
        return ScriptingHelper.GetObjectNameFromScript(_textEditor.Text, ref objType, ref isAlter);
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
          //panOutput.Height = (this.Height / 3) - 10;
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

    public void AddInfoMsqToList( string Msg )
    {
      AddMessageToList(SqlMessage.CreateInfoMessage(Msg));
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

    private bool IsResultGridFocused( )
    {
      if (tabOutput.SelectedIndex <= 0)
      {
        return false;
      }

      DataGridView grd = tabOutput.SelectedTab.Tag as DataGridView;
      return (grd != null && grd.Focused);
    }

    public void CopyGridContentToClipboard( )
    {
      if (tabOutput.SelectedIndex <= 0)
      {
        return;
      }


      DataGridView grd = tabOutput.SelectedTab.Tag as DataGridView;
      if (grd != null)
      {
        DataObject dtObj = grd.GetClipboardContent();
        if (dtObj == null)
        {
          return;
        }
        Clipboard.SetDataObject(dtObj);
        return;
      }


      ObjectRefList refList = tabOutput.SelectedTab.Tag as ObjectRefList;
      if (refList != null)
      {
        DataObject dtObj = refList.Grid.GetClipboardContent();
        if (dtObj == null)
        {
          return;
        }


        Clipboard.SetDataObject(dtObj);
        return;
      }
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
      lblCloseOutputPane.Visible = false;
      lblOutputPaneHeader.Text = String.Empty;
    }

    public string WordAtCursor
    {
      get
      {
        return SharpDevelopTextEditorUtilities.GetWordAt(ActiveDocument, ActiveTextArea.Caret.Offset);
      }
    }

    private void SendSelectedTextToTextDiff( bool isSource )
    {
      string script = ActiveTextArea.SelectionManager.HasSomethingSelected ? ActiveTextArea.SelectionManager.SelectedText : _textEditor.Text;


      frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
      if (diffForm == null)
      {
        diffForm = TextDiffFactory.CreateDiff();
      }

      if (isSource)
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

    private void CreateNewScriptEditor( )
    {
      frmScriptEditor editor = ScriptEditorFactory.Create(_connParams);
      ScriptEditorFactory.ShowScriptEditor(editor);
    }

    private void AnalyzeSQL( )
    {
      ClearMessagesList();
      if (!OutputPaneVisible)
      {
        OutputPaneVisible = true;
      }

      string sqlText = ActiveDocument.TextContent;
      AddInfoMsqToList("---- SQL Text Original----");
      AddInfoMsqToList(sqlText);
      sqlText = SqlAnalyzer.RemoveComments(sqlText);
      AddInfoMsqToList("---- SQL Text Comments removes----");
      AddInfoMsqToList(sqlText);


      SqlAnalyzerResults r = SqlAnalyzer.AnalyzeSql(sqlText, true);
      AddInfoMsqToList("---- Variables ----");
      foreach (SqlVariable v in r.Variables.Values)
      {
        AddInfoMsqToList("Name = " + v.Name + ", DataType = " + v.DataType + ", FullName = " + v.FullyQualifiedName);
      }

      AddInfoMsqToList("---- Table Aliases ----");
      foreach (SqlTableAlias t in r.TableAliases.Values)
      {
        AddInfoMsqToList("AliasType = " + t.AliasType.ToString() + ", TableName = " + t.TableName + ", Alias = " + t.TableAlias);
      }
      AddInfoMsqToList("---- Table Definitions ----");

      foreach (SqlTable t in r.Tables.Values)
      {
        AddInfoMsqToList(" *** TableName = " + t.TableName + ", Type = " + t.TableType.ToString() + " ***");
        foreach (string col in t.FullyQualifiedColumns)
        {
          AddInfoMsqToList(col);
        }
      }
    }

    #endregion //Utilities

    #region Query Execution

    public void ExecScript( string script, RunType scriptRunType, int selStartLine )
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

      ClearOutputPane();

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
      scriptData.SelStartLineNo = (selStartLine >= 0) ? selStartLine : 0;

      string scriptText = script;


      scriptData.ScriptText = scriptText;
      scriptData.ScriptRunType = scriptRunType;
      _scriptExecuterThread.RunWorkerAsync(scriptData);
      InvalidateButtonsAndMenuItems(true);
    }

    public void ExecScript( RunType scriptRunType )
    {
      int selStartLineNo = 0;
      string scriptText = String.Empty;
      if (ActiveTextArea.SelectionManager.HasSomethingSelected)
      {
        scriptText = ActiveTextArea.SelectionManager.SelectedText;
        selStartLineNo = Int32.MaxValue;
        foreach (ISelection sel in ActiveTextArea.SelectionManager.SelectionCollection)
        {
          if (sel.StartPosition.Y < selStartLineNo)
          {
            selStartLineNo = sel.StartPosition.Y;
          }
        }
      }
      else
      {
        scriptText = _textEditor.Text;
      }
      ExecScript(scriptText, scriptRunType, selStartLineNo);
    }

    private void CancelScriptExecution( )
    {
      timerExec.Enabled = false;
      _scriptExecuterThread.CancelAsync();
      if (_cmd != null)
      {
        _cmd.Cancel();
      }
    }

    private void DoExecuteScriptInBackground( object sender, DoWorkEventArgs e )
    {
      // Do not access the form's BackgroundWorker reference directly.
      // Instead, use the reference provided by the sender parameter.
      BackgroundWorker bw = sender as BackgroundWorker;

      // Extract the argument.
      ScriptData arg = (ScriptData)e.Argument;


      // Start the time-consuming operation.
      e.Result = ExecuteScriptWithDataAdapter(bw, arg);
      //e.Result = ExecuteScriptWithReader(bw, arg);

      // If the operation was canceled by the user, 
      // set the DoWorkEventArgs.Cancel property to true.
      if (bw.CancellationPending)
      {
        e.Cancel = true;
      }
    }

    private void OnScriptExecutionCompleted( object sender, RunWorkerCompletedEventArgs e )
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
        if (AutoRemoveModSignAfterCommit && ContentModified && DBConstants.DoesObjectTypeHasScript(_objectType))
        {
          ContentModified = false;
        }
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

    private IList<DataSet> ExecuteScriptWithDataAdapter( BackgroundWorker bw, ScriptData scriptData )
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
      bool tryToLogObjectScript = false;

      switch (scriptData.ScriptRunType)
      {
        case RunType.Execute:
          tryToLogObjectScript = true;
          completionMessage = "Command(s) completed.";
          break;
        case RunType.CheckSyntax:
          completionMessage = "Syntax check completed.";
          break;
        case RunType.ShowPlan:
          completionMessage = "Show plan completed.";
          break;
      }

      IList<DataSet> dataSets = new List<DataSet>();
      if (_scriptExecuterThread.CancellationPending)
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
            _cmd.CommandTimeout = 0;
            _cmd.ExecuteNonQuery();
            break;
          case RunType.ShowPlan:
            _cmd = new SqlCommand("SET SHOWPLAN_ALL ON", _conn);
            _cmd.CommandTimeout = 0;
            _cmd.ExecuteNonQuery();
            break;
          default:
            break;
        }


        IList<string> batches = ScriptingHelper.SplitBatches(scriptData.ScriptText);
        while (batches.Count > 0)
        {
          try
          {
            DataSet toFill = new DataSet();
            dataSets.Add(toFill);

            string batch = batches[0];
            batch = batch.Replace("\r", "");
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
            _cmd.CommandTimeout = 0;


            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = _cmd;
            int recordsAffected = adapter.Fill(toFill);
            if (tryToLogObjectScript)
            {
              try
              {
                ObjectChangeHistoryFacade.InsertObjectChangeHistoryRecord(ObjectChangeHistoryData.CreateFromScript(_conn, batch));
              }
              catch (Exception ex)
              {
                _sqlMessages.Add(SqlMessage.CreateWarningMessage("PragmaSQL can not log object change history. Error was:" + ex.Message));
              }
            }

            if (batches.Count == 0)
            {
              _sqlMessages.Add(SqlMessage.CreateInfoMessage(completionMessage));
              _sqlMessages.Add(SqlMessage.CreateMessage(""));
            }

            /*
            if (toFill.Tables.Count == 0 && scriptData.ScriptRunType == RunType.Execute)
            {
              _sqlMessages.Add(SqlMessage.CreateInfoMessage("( " + recordsAffected.ToString() + " row(s) affected ) "));
            }
            */

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
            _cmd.CommandTimeout = 0;
            _cmd.ExecuteNonQuery();
            break;
          case RunType.ShowPlan:
            _cmd = new SqlCommand("SET SHOWPLAN_ALL OFF", _conn);
            _cmd.CommandTimeout = 0;
            _cmd.ExecuteNonQuery();
            break;
          default:
            break;
        }
        _conn.InfoMessage -= new SqlInfoMessageEventHandler(HandleSqlInfo);
      }

      return dataSets;
    }

    
    public DataTable GetTableFromReader( SqlDataReader _reader )
    {

      System.Data.DataTable schemaTbl = _reader.GetSchemaTable();
      if (schemaTbl == null)
      {
        return null;
      }

      System.Data.DataTable dt = new System.Data.DataTable();
      System.Data.DataColumn col;
      System.Data.DataRow row;
      ArrayList al = new ArrayList();

      for (int i = 0; i < schemaTbl.Rows.Count; i++)
      {

        col = new System.Data.DataColumn();

        if (!dt.Columns.Contains(schemaTbl.Rows[i]["ColumnName"].ToString()))
        {

          col.ColumnName = schemaTbl.Rows[i]["ColumnName"].ToString();
          col.Unique = Convert.ToBoolean(schemaTbl.Rows[i]["IsUnique"]);
          col.AllowDBNull = Convert.ToBoolean(schemaTbl.Rows[i]["AllowDBNull"]);
          col.ReadOnly = Convert.ToBoolean(schemaTbl.Rows[i]["IsReadOnly"]);
          al.Add(col.ColumnName);
          dt.Columns.Add(col);

        }

      }

      //RenderResult(dt,1);
      while (_reader.Read() && !_scriptExecuterThread.CancellationPending)
      {
        row = dt.NewRow();
        for (int i = 0; i < al.Count; i++)
        {
          row[((System.String)al[i])] = _reader[(System.String)al[i]];
        }
        dt.Rows.Add(row);
      }
      return dt;
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
      int totalNumOfResultsets = 0;

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
              totalNumOfResultsets++;
              if (totalNumOfResultsets == 101)
              {
                string msg = "The query has exceeded the maximum number of result sets "
                  + " that can be displayed in the results grid. Only the first"
                  + " 100 result sets are displayed in the grid.";
                _sqlMessages.Add(SqlMessage.CreateWarningMessage(msg));
                return;
              }

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

              grd.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
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

      DataTable dt = grd.DataSource as DataTable;
      if (dt == null)
      {
        throw new Exception("DataTable can not be extracted from the current gird!");
      }
      DataExport.ExportGridToFile(dt);
    }

    public void ClearMessagesList( )
    {
      lv.Items.Clear();
      _sqlMessages.Clear();
    }

    private void ClearResults( )
    {
      try
      {
        tabOutput.SuspendLayout();
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

    private void ClearOutputPane( )
    {
      ClearMessagesList();
      ClearResults();
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
    #endregion

    #region Grid Print

    private DataGridViewPrinter _dataGridViewPrinter;
    private bool SetupThePrinting( DataGridView grd, string docName, string title )
    {
      PrintDialog printDlg = new PrintDialog();
      printDlg.AllowCurrentPage = false;
      printDlg.AllowPrintToFile = false;
      printDlg.AllowSelection = false;
      printDlg.AllowSomePages = false;
      printDlg.PrintToFile = false;
      printDlg.ShowHelp = false;
      printDlg.ShowNetwork = false;

      if (printDlg.ShowDialog() != DialogResult.OK)
      {
        return false;
      }

      printDocument1.DocumentName = docName;
      printDocument1.PrinterSettings = printDlg.PrinterSettings;
      printDocument1.DefaultPageSettings = printDlg.PrinterSettings.DefaultPageSettings;
      printDocument1.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

      _dataGridViewPrinter = new DataGridViewPrinter(grd, printDocument1, true, true, title, new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
      return true;
    }

    private void PrintGrid( bool isPreview )
    {

      DataGridView grd = tabOutput.SelectedTab.Tag as DataGridView;
      if (grd == null)
      {
        return;
      }

      InputDialog inputDlg = new InputDialog();
      inputDlg.Text = "Print Document Info";
      inputDlg.InitializeInputEdits(new string[2] { "Document Name", "Document Title" }, new string[] { "Document", "Title" });
      if (inputDlg.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      if (SetupThePrinting(grd, inputDlg["Document Name"], inputDlg["Document Title"]))
      {
        if (isPreview)
        {
          PrintPreviewDialog printPreviewDlg = new PrintPreviewDialog();
          printPreviewDlg.Document = printDocument1;
          printPreviewDlg.ShowDialog();
        }
        else
        {
          printDocument1.Print();
        }
      }

    }

    #endregion

    #region ObjectHelp Requested

    private ObjectInfo GetObjectInfoForWordAtCursor( )
    {

      return ProgrammabilityHelper.GetObjectInfo(_conn, _conn.Database, WordAtCursor);
      /*
      ObjectInfo result = null;
      string script = PragmaSQL.Scripts.Properties.Resources.Script_GetObjectInfoByName;
      script = String.Format(script, WordAtCursor);
      SqlCommand cmd = new SqlCommand(script, _conn);
      SqlDataReader reader = cmd.ExecuteReader();
      try
      {
        while (reader.Read())
        {
          result = new ObjectInfo();
          result.ObjectID = (int)reader["id"];
          result.ObjectName = (string)reader["name"];
          result.ObjectTypeAbb = (string)reader["xtype"];
          result.ObjectType = DBConstants.GetDBObjectType((reader["xtype"] as string));
        }
      }
      finally
      {
        reader.Close();
      }
      return result;
     */
    }


    private void OnObjectHelpRequested( object sender, ObjectHelpActionEventArgs e )
    {
      try
      {
        Cursor = Cursors.WaitCursor;
        if (ExecuteObjectHelp(e.ActionType, e.objectInfo))
        {
          return;
        }

        if (AppendColsOrParamsAction(e.ActionType, e.objectInfo))
        {
          return;
        }

        if (e.ActionType == ActionType.Modify)
        {
          OpenObjectScripInNewEditor(e.objectInfo);
        }
        else if (e.ActionType == ActionType.Open)
        {
          LoadTableOrViewData(e.objectInfo);
        }
        else if (e.ActionType == ActionType.References)
        {
          ShowReferences(e.objectInfo);
        }
        else if (e.ActionType == ActionType.Execute)
        {
          OpenExecuteScriptInNewEditor(e.objectInfo);
        }
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    private void OpenObjectScripInNewEditor( ObjectInfo objInfo )
    {
      if (objInfo == null)
      {
        return;
      }

      string script = ScriptingHelper.GetAlterScript(_conn, objInfo.ObjectID, objInfo.ObjectType);
      string caption = objInfo.ObjectName;

      frmScriptEditor editor = ScriptEditorFactory.Create(caption, script, objInfo.ObjectType, _connParams, _conn.Database);
      ScriptEditorFactory.ShowScriptEditor(editor);

    }

    private void OpenExecuteScriptInNewEditor( ObjectInfo objInfo )
    {
      if (objInfo == null)
      {
        return;
      }

      string script = ProgrammabilityHelper.GetProcedureExecuteScript(_conn, objInfo.ObjectName);
      string caption = "EXEC " + objInfo.ObjectName;

      frmScriptEditor editor = ScriptEditorFactory.Create(caption, script, objInfo.ObjectType, _connParams, _conn.Database);
      ScriptEditorFactory.ShowScriptEditor(editor);
    }

    private void LoadTableOrViewData( ObjectInfo objInfo )
    {
      if (objInfo == null)
      {
        return;
      }

      string script = "select * from [" + objInfo.ObjectName + "]";
      string caption = objInfo.ObjectName;
      bool isReadonly = objInfo.ObjectType == DBObjectType.View ? true : false;

      frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, _conn.Database, objInfo.ObjectName, caption, script, isReadonly, true);

      if (!isReadonly)
      {
        viewer.Icon = global::PragmaSQL.Properties.Resources.table;
      }
      else
      {
        viewer.Icon = global::PragmaSQL.Properties.Resources.Preview;
      }
      DataViewerFactory.ShowDataViewer(viewer);
    }

    private void ShowReferences( ObjectInfo objInfo )
    {
      if (objInfo == null)
      {
        return;
      }

      ClearOutputPane();

      ObjectRefList refList = new ObjectRefList();
      refList.Dock = DockStyle.Fill;
      refList.Initialize(objInfo.ObjectName, _connParams, _conn.Database, RefDetail.Any);
      refList.LoadData();

      tabOutput.TabPages.Add("References {" + objInfo.ObjectName + "}");
      TabPage tab = tabOutput.TabPages[tabOutput.TabCount - 1];

      refList.Parent = tab;
      tabOutput.SelectTab(tab);
      tab.Tag = refList;
      OutputPaneVisible = true;
      AddMessageToList(SqlMessage.CreateInfoMessage("Object info action executed. Action = References"));

    }

    private bool AppendColsOrParamsAction( ActionType actionType, ObjectInfo objInfo )
    {
      bool result = false;
      if (objInfo == null)
      {
        return result;
      }
      string insertThis = String.Empty;
      switch (actionType)
      {
        case ActionType.AppendColsAsSelectList:
          insertThis = ProgrammabilityHelper.GetColumns(_conn, objInfo.ObjectName, false);
          break;
        case ActionType.AppendColsAsParamList1:
          insertThis = ProgrammabilityHelper.GetColumnsAsParamListWithDataType(_conn, objInfo.ObjectName, false);
          break;
        case ActionType.AppendColsAsParamList2:
          insertThis = ProgrammabilityHelper.GetColumns(_conn, objInfo.ObjectName, true);
          break;
        case ActionType.AppendProcedureParams:
          insertThis = ProgrammabilityHelper.GetRoutineParams(_conn, objInfo.ObjectName);
          break;
        case ActionType.AppendFunctionParams:
          insertThis = ProgrammabilityHelper.GetRoutineParams(_conn, objInfo.ObjectName);
          break;
        case ActionType.AppendProcedureParamsWithDataType:
          insertThis = ProgrammabilityHelper.GetRoutineParamsWithDataType(_conn, objInfo.ObjectName);
          break;
        case ActionType.AppendFunctionParamsWithDataType:
          insertThis = ProgrammabilityHelper.GetRoutineParamsWithDataType(_conn, objInfo.ObjectName);
          break;
        case ActionType.AppendFunctionResultAsLocalTable:
          insertThis = ProgrammabilityHelper.GetTableValuedFunctionResultColumns(_conn, objInfo.ObjectName, true, String.Empty);
          break;
        default:
          result = false;
          break;
      }

      if (String.IsNullOrEmpty(insertThis))
      {
        return result;
      }


      try
      {

        ActiveTextArea.BeginUpdate();

        ActiveTextArea.Caret.Position = ActiveDocument.OffsetToPosition(SharpDevelopTextEditorUtilities.FindWordEnd(ActiveDocument, ActiveTextArea.Caret.Offset));
        ActiveTextArea.InsertString(" ");
        ActiveTextArea.InsertString(insertThis);
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
      return true;
    }

    private bool ExecuteObjectHelp( ActionType actionType, ObjectInfo objInfo )
    {
      string script = String.Empty;
      bool result = false;
      if (objInfo == null)
      {
        return result;
      }

      switch (actionType)
      {
        case ActionType.Permissions:
          script = ResManager.GetDBScript("Script_Permissions");
          script = script.Replace("$objid$", objInfo.ObjectID.ToString());
          break;
        case ActionType.TablePermissions:
          script = String.Format(ResManager.GetDBScript("spHelp_table_permissions"), objInfo.ObjectName);
          break;
        case ActionType.ForeignKeys:
          script = String.Format(ResManager.GetDBScript("spHelp_foreignkeys"), objInfo.ObjectName);
          break;
        case ActionType.ForeignKeysIn:
          script = String.Format(ResManager.GetDBScript("spHelp_foreignkeysin"), objInfo.ObjectName);
          break;
        case ActionType.Constraints:
          script = String.Format(ResManager.GetDBScript("spHelp_constraints"), objInfo.ObjectName);
          break;
        case ActionType.IdentityColumns:
          script = String.Format(ResManager.GetDBScript("spHelp_IdentityColumns"), objInfo.ObjectName);
          break;
        case ActionType.UsedSpace:
          script = String.Format(ResManager.GetDBScript("spHelp_UsedSpace"), objInfo.ObjectName);
          break;
        case ActionType.Statistics:
          script = String.Format(ResManager.GetDBScript("spHelp_Statistics"), objInfo.ObjectName);
          break;
        case ActionType.Dependencies:
          script = String.Format(ResManager.GetDBScript("spHelp_Depends"), objInfo.ObjectName);
          break;
        case ActionType.ObjectHelp:
          script = String.Format(ResManager.GetDBScript("spHelp"), objInfo.ObjectName);
          break;
        default:
          script = String.Empty;
          break;
      }

      if (!String.IsNullOrEmpty(script))
      {
        result = true;
        ExecScript(script, RunType.Execute, 0);
        AddMessageToList(SqlMessage.CreateInfoMessage("Object info action executed. Action = " + actionType.ToString()));
      }
      return result;
    }

    #endregion

    #region WndProc Override
    private const int WM_CLOSE = 16;
    protected override void WndProc( ref Message m )
    {
      if (m.Msg == WM_CLOSE)
      {
        bool askedToStop = false;
        while (_isExecuting)
        {
          if (!askedToStop)
          {
            askedToStop = true;
            CancelScriptExecution();
          }
          Application.DoEvents();
        }

        if (_isParseInProgress)
        {
          CancelParserThread();
        }

        while (_parserThread.IsBusy)
        {
          Application.DoEvents();
        }


        base.WndProc(ref m);
      }
      else
      {
        base.WndProc(ref m);
      }
    }
    #endregion

    #region Help On WordAtCursor
    public void ShowHelpOnWordAtCursor( )
    {
      string keyword = ActiveTextArea.SelectionManager.SelectedText;
      if (String.IsNullOrEmpty(keyword))
      {
        keyword = WordAtCursor;
      }

      //MessageBox.Show("Not implemented yet.","Not Implemented",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
      HelpProvider.ProvideHelpFor(keyword);
    }

    #endregion

    #region Parser BackgroundWorkerThread

    private BackgroundWorker _parserThread = new BackgroundWorker();
    private bool _isParseInProgress = false;

    private void InitializeParserThread( )
    {
      _parserThread.WorkerSupportsCancellation = true;
      _parserThread.DoWork += new System.ComponentModel.DoWorkEventHandler(DoBackgroundSelectCaseParsing);
      _parserThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(OnBackgroundSelectCaseParsingCompleted);
    }

    private void DoBackgroundSelectCaseParsing( object sender, DoWorkEventArgs e )
    {
      // Do not access the form's BackgroundWorker reference directly.
      // Instead, use the reference provided by the sender parameter.
      BackgroundWorker bw = sender as BackgroundWorker;

      // Extract the argument.
      string arg = (string)e.Argument;


      // Start the time-consuming operation.
      _isParseInProgress = true;
      e.Result = ParseScript2(arg);

      // If the operation was canceled by the user, 
      // set the DoWorkEventArgs.Cancel property to true.

      if (bw.CancellationPending)
      {
        e.Cancel = true;
      }
    }

    private void OnBackgroundSelectCaseParsingCompleted( object sender, RunWorkerCompletedEventArgs e )
    {
      _isParseInProgress = false;
      if (e.Cancelled)
      {
        return;
      }

      if (e.Error != null)
      {
        System.Diagnostics.Trace.Write(e.Error.Message, "BackgroundParser ThreadError");
        return;
      }

      SqlParserResult result = e.Result as SqlParserResult;
      if (result == null)
      {
        return;
      }

      if (result.ParseErrors.Count > 0)
      {
        foreach (string error in result.ParseErrors)
        {
          System.Diagnostics.Trace.Write(error, "BackgroundParser SyntaxError");
        }
      }
      _bgParseResults = result;
      RenderFoldMarkers();
    }

    private void RunParserThread( )
    {
      if (_isParseInProgress)
      {
        CancelParserThread();
      }

      while (_parserThread.IsBusy)
      {
        Application.DoEvents();
      }

      _parserThread.RunWorkerAsync(ActiveDocument.TextContent);
    }

    private void CancelParserThread( )
    {
      _parserThread.CancelAsync();
      while (_parserThread.IsBusy)
      {
        Application.DoEvents();
      }
    }

    private void RenderFoldMarkers( )
    {
      if (ActiveDocument == null || ActiveTextArea == null || ActiveTextEditorProps == null)
      {
        return;
      }

      try
      {
        ActiveTextArea.BeginUpdate();
        if (ActiveDocument.FoldingManager.FoldMarker.Count > 0)
        {
          //ActiveDocument.FoldingManager.UpdateFoldings(null);
        }
        
        if (!ActiveDocument.TextEditorProperties.EnableFolding)
        {
          return;
        }

        PrepareFoldMarkersForSelectAndCase();
        PrepareFoldMarkersForCodeBlocks();

        ActiveDocument.FoldingManager.UpdateFoldings(MergedFoldMarkers);
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
    }

    private void PrepareFoldMarkersForCodeBlocks( )
    {
      if (ActiveDocument == null || ActiveTextArea == null || ActiveTextEditorProps == null)
      {
        return;
      }

      try
      {
        ActiveTextArea.BeginUpdate();
        _codeBlockFoldMarkers.Clear();
        if (!ActiveDocument.TextEditorProperties.EnableFolding || !CanFoldCodeBlocks)
        {
          return;
        }


        if (_bgParseResults == null)
        {
          return;
        }

        for (int i = 0; i < _bgParseResults.CodeBlocks.Count; i++)
        {
          SqlShBoundary b = _bgParseResults.CodeBlocks[i];
          string lineText = ActiveDocument.GetText(ActiveDocument.GetLineSegment(b.StartLine));
          string blockText = String.Empty;
          int sIndex = lineText.IndexOf("<");
          int eIndex = lineText.IndexOf(">");
          if ((sIndex >= 0 && eIndex >= 0) && (sIndex < eIndex))
          {
            blockText = lineText.Substring(sIndex + 1, eIndex - sIndex - 1);
            blockText = blockText.Trim();
          }
          _codeBlockFoldMarkers.Add(new FoldMarker(ActiveDocument, b.StartLine, 0, b.EndLine, ActiveDocument.GetLineSegment(b.EndLine).Length, FoldType.Region, blockText));

        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }

    }

    private void PrepareFoldMarkersForSelectAndCase( )
    {
      if (ActiveDocument == null || ActiveTextArea == null || ActiveTextEditorProps == null)
      {
        return;
      }

      try
      {
        ActiveTextArea.BeginUpdate();
        _selectCaseFoldMarkers.Clear();
        if (!ActiveDocument.TextEditorProperties.EnableFolding || !CanFoldSelectCase)
        {
          return;
        }


        if (_bgParseResults == null)
        {
          return;
        }

        int minStartLine = Int32.MaxValue;
        int maxEndLine = -1;

        for (int i = 0; i < _bgParseResults.Statements.Count; i++)
        {
          SqlStatement s = _bgParseResults.Statements[i];
          s.CalculateMinMaxLine(out minStartLine, out maxEndLine);
          if (minStartLine < Int32.MaxValue && maxEndLine > -1)
          {
            _selectCaseFoldMarkers.Add(new FoldMarker(ActiveDocument, minStartLine, 0, maxEndLine, ActiveDocument.GetLineSegment(maxEndLine).Length, FoldType.TypeBody));
          }
        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
    }


    private void HighlightStatmentsByOffset( )
    {
      try
      {
        ActiveTextArea.SelectionManager.ClearSelection();
        ActiveDocument.CustomLineManager.Clear();

        if (_bgParseResults == null)
        {
          return;
        }

        ArrayList tmpList = null;
        SqlStatement stm = null;
        SqlShParserLocation loc = null;
        int caretPos = ActiveTextArea.Caret.Offset;

        int indexOf = -1;

        _bgParseResults.StartLocations.Sort(new LocationOffsetComparer());
        indexOf = _bgParseResults.StartLocations.BinarySearch(
            new SqlShParserLocation(ActiveTextArea.Caret.Line
            , ActiveTextArea.Caret.Column
            , ActiveTextArea.Caret.Offset)
          , new LocationLineComparer());

        if (indexOf > -1)
        {
          loc = _bgParseResults.StartLocations[indexOf] as SqlShParserLocation;
          if (loc == null)
          {
            return;
          }
          stm = loc.Statement;
        }
        else
        {
          tmpList = _bgParseResults.StartLocations;

          SqlShParserLocation tmpLoc = new SqlShParserLocation();
          tmpLoc.Position = caretPos;
          tmpLoc.LineNr = ActiveTextArea.Caret.Line;
          tmpLoc.ColumnNr = ActiveTextArea.Caret.Column;

          tmpList.Add(tmpLoc);
          tmpList.Sort(new LocationOffsetComparer());
          indexOf = tmpList.IndexOf(tmpLoc);
          indexOf--;
          if (indexOf < 0)
          {
            tmpList.Remove(tmpLoc);
            return;
          }

          loc = tmpList[indexOf] as SqlShParserLocation;
          tmpList.Remove(tmpLoc);
          if (loc == null || loc.Boundary == null || loc.Boundary.EndOffset <= 0)
          {
            return;
          }

          if (!(caretPos >= loc.Boundary.StartOffset && caretPos <= loc.Boundary.EndOffset))
          {
            return;
          }
        }

        if (loc.Statement == null)
        {
          return;
        }

        _currentSqlStmn = loc.Statement;
        Color c = Color.LightGray;
        switch (_currentSqlStmn.Type)
        {
          case SqlShParserStatmentType.Select:
            c = _selectBlockColor;
            break;
          case SqlShParserStatmentType.Case:
            c = _caseBlockColor;
            break;
          default:
            break;
        }

        foreach (SqlShBoundary b in loc.Statement.Boundaries)
        {
          for (int i = b.StartLine; i <= b.EndLine; i++)
          {
            ActiveDocument.CustomLineManager.AddCustomLine(i, c, false);
          }
        }
      }
      finally
      {
        ActiveTextArea.Invalidate();
      }
    }

    private void HighlightStatmentsByLineNr( )
    {
      try
      {
        ActiveTextArea.SelectionManager.ClearSelection();
        ActiveDocument.CustomLineManager.Clear();

        if (_bgParseResults == null)
        {
          return;
        }

        ArrayList tmpList = null;
        SqlStatement stm = null;
        SqlShParserLocation loc = null;
        int caretLineNr = ActiveTextArea.Caret.Line;
        int indexOf = -1;

        _bgParseResults.StartLocations.Sort(new LocationLineComparer());
        indexOf = _bgParseResults.StartLocations.BinarySearch(
            new SqlShParserLocation(ActiveTextArea.Caret.Line
            , ActiveTextArea.Caret.Column
            , ActiveTextArea.Caret.Offset)
          , new LocationLineComparer());

        if (indexOf > -1)
        {
          loc = _bgParseResults.StartLocations[indexOf] as SqlShParserLocation;
          if (loc == null)
          {
            return;
          }
          stm = loc.Statement;
        }
        else
        {
          tmpList = _bgParseResults.StartLocations;

          SqlShParserLocation tmpLoc = new SqlShParserLocation();
          tmpLoc.Position = ActiveTextArea.Caret.Offset;
          tmpLoc.LineNr = ActiveTextArea.Caret.Line;
          tmpLoc.ColumnNr = ActiveTextArea.Caret.Column;

          tmpList.Add(tmpLoc);
          tmpList.Sort(new LocationLineComparer());
          indexOf = tmpList.IndexOf(tmpLoc);
          indexOf--;
          if (indexOf < 0)
          {
            tmpList.Remove(tmpLoc);
            return;
          }

          loc = tmpList[indexOf] as SqlShParserLocation;
          tmpList.Remove(tmpLoc);
          if (loc == null || loc.Boundary == null || loc.Boundary.EndOffset <= 0)
          {
            return;
          }

          if (!(caretLineNr >= loc.Boundary.StartLine && caretLineNr <= loc.Boundary.EndLine))
          {
            return;
          }
        }

        if (loc.Statement == null)
        {
          return;
        }

        _currentSqlStmn = loc.Statement;
        Color c = Color.LightGray;
        switch (_currentSqlStmn.Type)
        {
          case SqlShParserStatmentType.Select:
            c = _selectBlockColor;
            break;
          case SqlShParserStatmentType.Case:
            c = _caseBlockColor;
            break;
          default:
            break;
        }

        foreach (SqlShBoundary b in loc.Statement.Boundaries)
        {
          for (int i = b.StartLine; i <= b.EndLine; i++)
          {
            ActiveDocument.CustomLineManager.AddCustomLine(i, c, false);
          }
        }
      }
      finally
      {
        ActiveTextArea.Invalidate();
      }
    }

    private SqlParserResult ParseScript2( string ScriptText )
    {
      _tokenizer.SetInput(ScriptText);
      SqlShallowParser parser = new SqlShallowParser();
      parser.ParseSelectsAndCases = _parseSelectsAndCases;
      parser.ParseCodeBlocks = _parseCodeBlocks;
      return parser.ParseScript(_parserThread, _tokenizer, ScriptText);
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
          ContentSaved = false;
        }
        else
        {
          this.Text = _caption;
          this.TabText = this.Text;
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

      return true;
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
        ContentSaved = true;
      }
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
        ContentSaved = true;
      }
    }
    #endregion //Content I/O

    #endregion IPragmaEditor Members

    public void InspectPragmaSQLDbConnection( )
    {
      if (ConfigurationHelper.Current == null)
      {
        lblPragmaSQLDbConnectionInfo.Text = "PragmaSQL options can not be loaded!";
        lblPragmaSQLDbConnectionInfo.Visible = true;
        return;
      }
      else
      {
        if (ConfigurationHelper.Current.PragmaSql_UtilsDisabled)
        {
          lblPragmaSQLDbConnectionInfo.Text = "Not connected to PragmaSQL database!";
          lblPragmaSQLDbConnectionInfo.Visible = true;
        }
        else
        {
          lblPragmaSQLDbConnectionInfo.Visible = false;
        }
      }
    }

    public void ApplyTextEditorOptionsFromCurrentConfig( )
    {
      InspectPragmaSQLDbConnection();
      try
      {
        ActiveTextArea.BeginUpdate();
        if
          (
            ConfigurationHelper.Current == null
            || ConfigurationHelper.Current.TextEditorOptions == null
            || ActiveTextArea == null
            || ActiveTextEditorProps == null
          )
        {
          return;
        }

        TextEditorOptions opts = ConfigurationHelper.Current.TextEditorOptions;
        _parseSelectsAndCases = opts.HighlightSelectAndCase;
        _parseCodeBlocks = opts.FoldCodeBlocks;

        ActiveTextEditorProps.EnableFolding = opts.ShowFoldMarkers;
        bool shallUpdateFoldMarkers = false;

        if ((!opts.ShowFoldMarkers || !opts.FoldHighlightedSelectAndCase || !opts.HighlightSelectAndCase) && _selectCaseFoldMarkers.Count > 0)
        {
          _selectCaseFoldMarkers.Clear();
          shallUpdateFoldMarkers = true;
        }

        if ((!opts.ShowFoldMarkers || !opts.FoldCodeBlocks) && _codeBlockFoldMarkers.Count > 0)
        {
          _codeBlockFoldMarkers.Clear();
          shallUpdateFoldMarkers = true;
        }

        if (shallUpdateFoldMarkers)
        {
          ActiveDocument.FoldingManager.UpdateFoldings(MergedFoldMarkers);
        }

        CancelParserThread();
        ActiveDocument.CustomLineManager.Clear();
        ActiveTextArea.Invalidate();

        /*
        if (!opts.HighlightSelectAndCase)
        {
          CancelParserThread();
          ActiveDocument.CustomLineManager.Clear();
          ActiveTextArea.Invalidate();
        }
        */

        if (_parseSelectsAndCases || _parseCodeBlocks)
        {
          RunParserThread();
          if (_selectBlockColor == opts.SelectHighlightColor && _caseBlockColor == opts.CaseHighlightColor)
          {
            return;
          }

          _selectBlockColor = opts.SelectHighlightColor;
          _caseBlockColor = opts.CaseHighlightColor;
          Color c = Color.LightGray;
          if (_currentSqlStmn != null)
          {
            switch (_currentSqlStmn.Type)
            {
              case SqlShParserStatmentType.Case:
                c = _caseBlockColor;
                break;
              case SqlShParserStatmentType.Select:
                c = _selectBlockColor;
                break;
              default:
                break;
            }

            foreach (CustomLine ln in ActiveDocument.CustomLineManager.CustomLines)
            {
              ln.Color = c;
            }
            ActiveTextArea.Invalidate();
          }
        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
      }
    }

    private void cmbDatabases_SelectedIndexChanged( object sender, EventArgs e )
    {
      string tmp = String.Empty;

      if (_initializing)
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
        _initializing = true;
        cmbDatabases.Text = tmp;
        _initializing = false;
      }
    }

    private void cmbServers_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializing)
      {
        return;
      }


      if (_conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
      }


      string tmp = String.Empty;
      tmp = _conn.ConnectionString;

      ConnectionParams cp = _cpList[cmbServers.SelectedIndex];

      bool changeOk = false;
      try
      {
        _conn.Close();
        _conn.ConnectionString = cp.ConnectionString;
        _conn.Open();
        _currentServerIndex = cmbServers.SelectedIndex;
        if (String.IsNullOrEmpty(cp.Database))
        {
          cp.Database = "master";
        }
        PopulateDatabases(cp.Database);
        changeOk = true;
      }
      catch
      {
        MessageBox.Show("Can not connect to selected server!\nPrevious connection will be restored.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _conn.ConnectionString = tmp;
        _conn.Open();

        _initializing = true;
        cmbServers.SelectedIndex = _currentServerIndex;
        _initializing = false;
      }

      if (changeOk && _codeCompWindow != null)
      {
        _codeCompWindow.InitializeCompletionProposal(_conn);
      }
    }

    private void lblCloseoutputPane_MouseClick( object sender, MouseEventArgs e )
    {
      OutputPaneVisible = false;
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
        if (lineNo > ActiveDocument.TotalNumberOfLines)
        {
          MessageBox.Show("Can not locate line in script.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        ActiveTextArea.Caret.Column = 0;
        ActiveTextArea.Caret.Line = lineNo - 1;
        ActiveTextArea.Select();
        Point startPoint = ActiveTextArea.Caret.Position;
        Point endPoint = ActiveTextArea.Caret.Position;
        endPoint.X = endPoint.X + ActiveDocument.GetLineSegment(lineNo - 1).Length;
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
      SaveContentAs();
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

    private void exportToFileToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ExportGridToFile();
    }

    private void lblCloseoutputPane_Click( object sender, EventArgs e )
    {
      OutputPaneVisible = false;
    }

    private void printDocument1_PrintPage( object sender, PrintPageEventArgs e )
    {
      e.HasMorePages = _dataGridViewPrinter.DrawDataGridView(e.Graphics);
    }

    private void printPreviewToolStripMenuItem_Click( object sender, EventArgs e )
    {
      PrintGrid(true);
    }

    private void printToolStripMenuItem_Click( object sender, EventArgs e )
    {
      PrintGrid(false);
    }

    private void popUpEditor_Opening( object sender, CancelEventArgs e )
    {
      if (_objHelperPopupBuilder == null)
      {
        return;
      }

      _objHelperPopupBuilder.BuildMenuItems(_conn, MergeType.Insert, WordAtCursor);
    }

    private void clearResultsToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ClearResults();
    }

    private void clearMessagesToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ClearMessagesList();
    }

    private void clearToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ClearOutputPane();
    }

    private void openSharedScriptToolStripMenuItem_Click( object sender, EventArgs e )
    {
      frmSharedScriptSelectDialog.OpenSharedScript(this, _connParams);
    }

    private void saveAsSharedScriptToolStripMenuItem_Click( object sender, EventArgs e )
    {
      frmSharedScriptSelectDialog.SaveAsSharedScript(this);
    }

    private void parseSqlStatementToolStripMenuItem_Click( object sender, EventArgs e )
    {
      AnalyzeSQL();
    }

    private void toolStripLabel4_Click( object sender, EventArgs e )
    {
      Program.MainForm.ShowOptionsDialog();
    }

    private void btnNewScript_Click( object sender, EventArgs e )
    {
      CreateNewScriptEditor();
    }

    private void OnDiffScriptAsSource_Click( object sender, EventArgs e )
    {
      SendSelectedTextToTextDiff(true);
    }

    private void OnDiffScriptAsDest_Click( object sender, EventArgs e )
    {
      SendSelectedTextToTextDiff(false);
    }

    private void frmScriptEditor_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (_tokenizer != null)
      {
        _tokenizer = null;
      }
    }


  } //Class end
}//Namespace end