using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Threading;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.Core;

using Crad.Windows.Forms.Actions;
using AsynchronousCodeBlocks;

using com.calitha.goldparser;
using PragmaSQL.Core;
using MWCommon;

namespace PragmaSQL
{
    public partial class frmScriptEditor : frmEditorBase, IPragmaEditor, IScriptEditor, IToolTipProvider
    {
        #region Private and Internal Fields
        internal int? WindowNo = null;

        private ToolStrip _addInToolStrip;
        private IList<ConnectionParams> _cpList = new List<ConnectionParams>();
        private CodeCompletionPresenter _codeCompWindow;
        private CodeCompletionPresenterEx _codeCompWindowEx;

        private SqlConnection _sqlConn = null;
        public SqlConnection SqlConn
        {
            get { return _sqlConn; }
            set
            {
                _sqlConn = value;
                _activeConn = value;
            }
        }

        private SqlConnection _activeConn = null;

        private SqlCommand _cmd = null;
        private BackgroundWorker _scriptExecuterThread = new BackgroundWorker();

        private IList<SqlMessage> _sqlMessages = new List<SqlMessage>();
        private SearchAndReplaceForm _frmSearchAndReplace = null;
        private DateTime _startTime = DateTime.Now;
        private bool _completedWithErrors = false;
        private int _currentServerIndex = -1;
        private bool _isExecuting = false;
        private ActionList _actionList = new ActionList();
        private ObjectHelperPopupBuilder _objHelperPopupBuilder = null;
        private ScriptData _lastExecutedScriptData;
        private bool _canAnalyzeSql = true;
        private IList<DataTable> _resultTables = new List<DataTable>();
        private string _currentConnUsername = String.Empty;

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
        #endregion //Private Fields

        #region Properties

        private ConnectionParams CurrentConnectionParams
        {
            get
            {
                if (InvokeRequired)
                    return (ConnectionParams)Invoke(new GetCurrentConnectionParamsDelegate(GetCurrentConnectionParams));
                else
                    return GetCurrentConnectionParams();
            }
        }


        private ConnectionParams SqlConnParams;
        public ConnectionParams ConnParams
        {
            get { return SqlConnParams; }
        }

        //private bool ConnectionAvailable
        //{
        //  get
        //  {
        //    return SqlConn != null && SqlConn.State == ConnectionState.Open;
        //  }
        //}

        private bool _initializing = false;
        public bool Initializing
        {
            get { return _initializing; }
        }

        public bool AutoRemoveModSignAfterCommit
        {
            get
            {
                if (ConfigHelper.Current == null || ConfigHelper.Current.TextEditorOptions == null)
                {
                    return true;
                }
                else
                {
                    return ConfigHelper.Current.TextEditorOptions.AutoRemoveModifiedSignForKnownScriptableObjects;
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

        #endregion

        #region Sql Shallow Parser Related Fields And Properties

        private Stack<Paranthesis> _openingParanths = new Stack<Paranthesis>();
        private StringTokenizer _tokenizer = null;
        private SqlParserResult _bgParseResults = null;
        private bool _parseSelectsAndCases = false;
        private bool _parseCodeBlocks = false;
        private bool _parseComments = false;

        private Color _selectBlockColor = Color.LightGray;
        private Color _caseBlockColor = Color.LightGray;
        private Color _commentBlockColor = Color.LightGray;

        private SqlStatement _currentSqlStmn = null;

        private List<FoldMarker> _selectCaseFoldMarkers = new List<FoldMarker>();
        private List<FoldMarker> _codeBlockFoldMarkers = new List<FoldMarker>();
        private List<FoldMarker> _commentFoldMarkers = new List<FoldMarker>();

        private IList<int> _highlightedSelectCaseLines = new List<int>();
        private IList<int> _highlightedCommentLines = new List<int>();

        private List<FoldMarker> MergedFoldMarkers
        {
            get
            {
                List<FoldMarker> result = new List<FoldMarker>();
                result.AddRange(_selectCaseFoldMarkers);
                result.AddRange(_codeBlockFoldMarkers);
                result.AddRange(_commentFoldMarkers);
                return result;
            }
        }

        #endregion

        #region CTOR

        public frmScriptEditor()
        {
            InitializeComponent();
            base.ContentPersister.ContentType = EditorContentType.Script;
            base.SearchToolStripTextBox = edtMatchText;
            base.ContentModifiedIndicatorToolStripItem = this.statContentModifiedIndicator;
            base.ContentInfoToolStripItem = this.statLblContentInfo;
            edtMatchText.Text = Program.MainForm.SearchTerm;
            InitializeResultFactoryCombo();
            InitializeAddInSupport();
            InitializeTextEditor();
            InitializeGoldParserEngine();
            InitializeParserThread();
            if (ConfigHelper.Current != null && ConfigHelper.Current.TextEditorOptions != null)
                _canAnalyzeSql = ConfigHelper.Current.TextEditorOptions.AnalyzeSql;

            //this._saBuf = Marshal.AllocHGlobal(0x100);
            ScriptEditorFactory.Numerator.WindowCount++;
        }

        #endregion //CTOR

        #region Initialization

        private ConnectionParamsCollection Connections
        {
            get
            {
                return ConnectionParamsFactory.GetConnections();
            }
        }

        private void InitializeResultFactoryCombo()
        {
            cmbResultRenderers.Items.Clear();

            foreach (KeyValuePair<string, ResultRendererSpec> entry in ResultRendererService._renderers)
            {
                ResultRendererSpec item = entry.Value;
                cmbResultRenderers.Items.Add(item);
                if (String.Compare(item.FullName, Program.MainForm.DefaultResultRenderer, true, CultureInfo.InvariantCulture) == 0)
                    cmbResultRenderers.SelectedItem = item;
            }

            if (cmbResultRenderers.SelectedItem == null && cmbResultRenderers.Items.Count > 0)
                cmbResultRenderers.SelectedIndex = 0;
        }

        private void InitializeForm()
        {

            WireUpTextEditor();
            OutputPaneVisible = false;
            SharedScriptOperationsVisible = ConfigHelper.Current.SharedScriptsEnabled();
            InitializeCodeCompletionWindow();
            InitializeCodeCompletionWindow_Ex();

            this.ContextMenuStrip = popUpTab;

            _scriptExecuterThread.WorkerSupportsCancellation = true;
            _scriptExecuterThread.DoWork += new System.ComponentModel.DoWorkEventHandler(DoExecuteScriptInBackground);
            _scriptExecuterThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(OnScriptExecutionCompleted);

            InitiailizeActions();

            _objHelperPopupBuilder = new ObjectHelperPopupBuilder(this, popUpEditor);
            _objHelperPopupBuilder.ObjectHelpRequested += new ObjectHelpActionEventHandler(OnObjectHelpRequested);

            ApplyTextEditorOptionsFromCurrentConfig();
        }

        private string _windowId = String.Empty;
        private long _objectId = -1;
        public long ObjectId
        {
            get { return _objectId; }
        }

        public void InitializeScriptEditor(string caption, string script, long objectId, int objType, ConnectionParams source, string initialCatalog)
        {
            InitializeScriptEditor(caption, script, objectId, objType, source, initialCatalog, true);
        }

        private LayoutProvider _lp = new LayoutProvider();

        public void InitializeScriptEditor(string caption, string script, long objectId, int objType, ConnectionParams source, string initialCatalog, bool manageWindow)
        {
            if ((Program.MainForm.DetermineStartPosition() ?? -1) == -1)
                _lp.SuspendLayout();

            if (source == null)
            {
                throw new NullReferenceException("Connection parameters object can not be null!");
            }

            if (manageWindow && source != null)
            {
                _windowId = ScriptEditorManager.ProduceWindowId(caption, objectId, objType, source.Server, initialCatalog);
                ScriptEditorManager.Remember(_windowId, this);
            }

            _caption = caption;
            this.Text = _caption;
            this.TabText = _caption;

            try
            {
                //FuzzyWait.ShowFuzzyWait("Initializing editor...");
                InitializeForm();
                _initializing = true;

                _scriptText = script;
                _objectType = objType;
                _objectId = objectId;

                if (_objectId > 0)
                    _editorObjectName = _caption;

                _textEditor.Text = _scriptText;


                SqlConnParams = source.CreateCopy();
                if (String.IsNullOrEmpty(initialCatalog))
                {
                    SqlConnParams.Database = "master";
                }
                else
                {
                    SqlConnParams.Database = initialCatalog;
                }



                SqlConnParams.IsConnected = false;
                try
                {
                    if (SqlConn != null)
                    {
                        if (SqlConn.State == ConnectionState.Open)
                            SqlConn.Close();

                        SqlConn.Dispose();
                    }

                    SqlConn = SqlConnParams.CreateSqlConnection(true, false);
                    SqlConn.StateChange += new StateChangeEventHandler(SqlConn_StateChange);

                    _currentConnUsername = SqlConnParams.CurrentUsername;

                    PopulateServers(SqlConnParams);
                    PopulateDatabases(SqlConnParams.Database);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                Program.MainForm.HostSvc.FireTextEditorReadyEvent(this);
            }
            finally
            {
                _initializing = false;
            }
        }

        void SqlConn_StateChange(object sender, StateChangeEventArgs e)
        {
        }

        public void InitializeScriptEditorWithAsyncConnection(string caption, string script, long objectId, int objType, ConnectionParams source, string initialCatalog, bool manageWindow)
        {
            if ((Program.MainForm.DetermineStartPosition() ?? -1) == -1)
                _lp.SuspendLayout();

            if (source == null)
                throw new NullReferenceException("Connection parameters object can not be null!");

            if (manageWindow && source != null)
            {
                _windowId = ScriptEditorManager.ProduceWindowId(caption, objectId, objType, source.Server, initialCatalog);
                ScriptEditorManager.Remember(_windowId, this);
            }

            _caption = caption;
            this.Text = _caption;
            this.TabText = _caption;

            try
            {
                //FuzzyWait.ShowFuzzyWait("Initializing editor...");
                InitializeForm();
                _initializing = true;

                _scriptText = script;
                _objectType = objType;
                _objectId = objectId;

                if (_objectId > 0)
                    _editorObjectName = _caption;

                _textEditor.Text = _scriptText;

                SqlConnParams = source.CreateCopy();
                SqlConnParams.Database = String.IsNullOrEmpty(initialCatalog) ? "master" : initialCatalog;
                SqlConnParams.IsConnected = false;
                AsyncInitializeConnection();
            }
            finally
            {
                _initializing = false;
            }
        }

        private void EnableControlsDuringAsyncConnection(bool enabled)
        {
            strip2.Enabled = enabled;
            cmbServers.Enabled = enabled;
            cmbDatabases.Enabled = enabled;
            btnChangeDb.Enabled = enabled;
            btnReconnect.Enabled = enabled;
        }

        private bool _initializingServers = false;
        private bool _asyncConnCancelled = false;

        private void AsyncInitializeConnection()
        {
            _asyncConnCancelled = false;
            SynchronizationContext sc = SynchronizationContext.Current;
            Async asyncObj = new Async
              (
                delegate
                {
                    sc.Send(
                            delegate(object state)
                            {
                                EnableControlsDuringAsyncConnection(false);
                                hdrAsyncConn.Visible = true;
                                hdrAsyncConn.Values.Image = Properties.Resources.dbs;
                                hdrAsyncConn.Text = String.Format("Connecting to {0} ...", SqlConnParams.InfoDbServer);

                                buttonSpecAny2.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
                                buttonSpecAny3.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
                            }
                            , null);

                    SqlConnection tmpConn = SqlConnParams.CreateSqlConnection(true, false);
                    if (!_asyncConnCancelled)
                    {
                        if (SqlConn != null)
                        {
                            if (SqlConn.State == ConnectionState.Open)
                                SqlConn.Close();

                            SqlConn.Dispose();
                        }

                        sc.Send(
                          delegate(object state)
                          {
                              _currentConnUsername = SqlConnParams.CurrentUsername;
                              _cpList.Clear();
                              _currentServerIndex = -1;
                              cmbServers.Items.Clear();
                              cmbDatabases.Items.Clear();
                          }, null);

                        SqlConn = tmpConn;
                        SqlConn.StateChange += new StateChangeEventHandler(SqlConn_StateChange);

                    }

                    sc.Send(
                            delegate(object state)
                            {
                                PopulateServers(SqlConnParams);
                                PopulateDatabases(SqlConnParams.Database);
                            }
                            , null);

                },
                delegate(Async objAsync)
                {
                    if (_asyncConnCancelled)
                        return;

                    sc.Send(
                            delegate(object state)
                            {
                                EnableControlsDuringAsyncConnection(true);
                                if (objAsync.CodeException != null)
                                {
                                    hdrAsyncConn.Text = String.Format("Can not connect to {0}.\r\nError:{1}", SqlConnParams.InfoDbServer, objAsync.CodeException.Message);
                                    hdrAsyncConn.Values.Image = Properties.Resources.attention;
                                    buttonSpecAny2.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
                                    buttonSpecAny3.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
                                }
                                else
                                {
                                    ResetAsyncConnHeader();
                                }
                            }
                            , null);
                }
              );
        }

        private void ResetAsyncConnHeader()
        {
            hdrAsyncConn.Visible = false;
            hdrAsyncConn.Text = String.Empty;
            hdrAsyncConn.Values.Image = null;
            buttonSpecAny2.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            buttonSpecAny3.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True;
        }

        private void InitializeGoldParserEngine()
        {
            /*
             * Code block below instantiates tokenizer for each script editor. This
             * method consumes too much memory and worst is memory can not be released
             * properly.
             * The solution is using single application wide instance of the tokenizer 
             * object. Since only one editor can be modified by the user no sharing problems
             * are expected.
             */

            _tokenizer = GoldTokenizer.Tokenizer;
        }

        private void PopulateServers(ConnectionParams defaultParams)
        {
            try
            {
                _initializingServers = true;

                cmbServers.Items.Clear();
                _currentServerIndex = -1;

                if (SqlConn.State != ConnectionState.Open)
                    return;

                ConnectionParamsCollection cons = Connections;
                foreach (ConnectionParams cp in cons)
                {
                    //if (cmbServers.FindStringExact(cp.Server) != -1)
                    if (cmbServers.FindStringExact(ConnectionParams.PrepareConnKey(cp)) != -1)
                        continue;

                    //cmbServers.Items.Add(cp.Server);
                    cmbServers.Items.Add(ConnectionParams.PrepareConnKey(cp));
                    _cpList.Add(cp);
                }

                if (defaultParams != null)
                {
                    int dIdx = cmbServers.FindStringExact(ConnectionParams.PrepareConnKey(defaultParams));
                    if (dIdx < 0)
                    {
                        cmbServers.Items.Add(ConnectionParams.PrepareConnKey(defaultParams));
                        cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
                        _cpList.Add(defaultParams);
                    }
                    else
                        cmbServers.SelectedIndex = dIdx;
                }
                _currentServerIndex = cmbServers.SelectedIndex;
            }
            finally
            {
                _initializingServers = false;
            }
        }

        private void PopulateDatabases(string defaultDatabaseName)
        {
            cmbDatabases.Items.Clear();

            if (SqlConn == null || SqlConn.State != ConnectionState.Open)
                return;

            //DataTable dbs = SqlConn.GetSchema("Databases");

            int dbIndex = -1;
            using (SqlDataAdapter ad = new SqlDataAdapter(ResManager.GetDBScript("Script_GetDatabases"), SqlConn))
            {
                DataTable dbs = new DataTable();
                ad.Fill(dbs);

                //dbs.DefaultView.Sort = "database_name";
                dbs.DefaultView.Sort = "name";
                dbs = dbs.DefaultView.ToTable();

                foreach (DataRow row in dbs.Rows)
                {
                    //string dbName = (string)row["database_name"];
                    string dbName = (string)row["name"];

                    cmbDatabases.Items.Add(dbName);
                    if (defaultDatabaseName.ToLowerInvariant() == dbName.ToLowerInvariant())
                    {
                        cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
                        dbIndex = cmbDatabases.SelectedIndex;
                        prevSelectedDatabase = cmbDatabases.Text;
                    }
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

        private void InitializeCodeCompletionWindow()
        {
            if (_codeCompWindow != null)
            {
                return;
            }

            _codeCompWindow = new CodeCompletionPresenter();
            RegisterForCodeCompletionEvents();
        }

        private void InitializeCodeCompletionWindow_Ex()
        {
            if (_codeCompWindowEx != null)
            {
                return;
            }

            _codeCompWindowEx = new CodeCompletionPresenterEx();
            RegisterForCodeCompletionEventsEx();
        }

        private void ShowSelector()
        {
            Cursor currentCursor = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string requestedFor = GetPreviousNonWSLineParts;
                FireBeforeCodeCompletionShowed(CodeCompletionType.DatabaseObjectList, requestedFor);

                Point final = GetCaretPosition();
                _codeCompWindow.SetConnection(SqlConn);

                SqlAnalyzerResults r = null;
                if (_canAnalyzeSql)
                {
                    string sqlText = ActiveDocument.TextContent;
                    sqlText = SqlAnalyzeHelper.RemoveComments(sqlText);
                    r = SqlAnalyzer.AnalyzeSql(sqlText, false);
                }

                _codeCompWindow.JumpTo(_textEditor, requestedFor, r);
                _codeCompWindow.SetLocation(final.X + 10, final.Y - 10);
                _codeCompWindow.ShowSelector();
            }
            finally
            {
                Cursor.Current = currentCursor;
            }
        }

        private void ShowSelector_Ex()
        {
            Cursor currentCursor = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string requestedFor = GetPreviousNonWSLineParts;
                FireBeforeCodeCompletionShowed(CodeCompletionType.UserDefinedList, requestedFor);

                Point final = GetCaretPosition();
                _codeCompWindowEx.JumpTo(requestedFor);
                _codeCompWindowEx.SetLocation(final.X + 10, final.Y - 10);
                _codeCompWindowEx.ShowSelector();
            }
            finally
            {
                Cursor.Current = currentCursor;
            }
        }

        private void RegisterForCodeCompletionEvents()
        {
            if (_codeCompWindow == null)
            {
                return;
            }

            _codeCompWindow.KeyPressed += new KeyPressHandler(HandleCodeCompletionKeyPress);
            _codeCompWindow.UserMadeFinalSelection += new NotificationHandler(HandleCodeCompletionSelection);
            _codeCompWindow.UserPressedBackSpace += new NotificationHandler(HandleCodeCompletionBackSpace);
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


        private void HandleCodeCompletionKeyPress(char c)
        {
            switch (c)
            {
                case (char)27: //ESC
                    _codeCompWindow.DismissSelector();
                    _textEditor.Focus();
                    FireAfterCodeCompletionShowed(CodeCompletionType.DatabaseObjectList, String.Empty, false);
                    return;
                default:
                    if (Char.IsControl(c))
                    {
                        return;
                    }
                    ActiveTextArea.Caret.RecreateCaret();
                    ActiveTextArea.InsertChar(c);
                    _codeCompWindow.JumpTo(_textEditor, GetPreviousNonWSLineParts, null);
                    break;
            }
        }

        private void HandleCodeCompletionSelection()
        {
            try
            {
                ActiveTextArea.BeginUpdate();
                string userSelection = String.Empty;
                if (_codeCompWindow.Selector.HasMultipleSelection)
                {
                    userSelection = _codeCompWindow.SelectedItemsAsCommaSeperatedString;
                }
                else
                {
                    userSelection = _codeCompWindow.SelectedItem;
                }

                if (_codeCompWindow.Selector.HasMultipleSelection)
                {
                    ActiveTextArea.InsertString(userSelection);
                }
                else
                {
                    DeleteWordBeforeCaret();
                    ActiveTextArea.InsertString(userSelection);
                }
                FireAfterCodeCompletionShowed(CodeCompletionType.DatabaseObjectList, userSelection, true);
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();

            }
            _codeCompWindow.DismissSelector();
            _textEditor.Focus();
        }

        private void HandleCodeCompletionBackSpace()
        {
            new ICSharpCode.TextEditor.Actions.Backspace().Execute(ActiveTextArea);
            _codeCompWindow.JumpTo(_textEditor, GetPreviousNonWSLineParts, null);
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
                ActiveTextArea.Invalidate();
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
                    ConfigHelper.Current == null
                    || ConfigHelper.Current.TextEditorOptions == null
                    || ActiveTextArea == null
                    || ActiveTextEditorProps == null
                  )
                {
                    return false;
                }

                TextEditorOptions opts = ConfigHelper.Current.TextEditorOptions;
                return (opts != null && opts.ShowFoldMarkers && opts.FoldHighlightedSelectAndCase);
            }
        }

        private bool CanFoldComments
        {
            get
            {
                if
                  (
                    ConfigHelper.Current == null
                    || ConfigHelper.Current.TextEditorOptions == null
                    || ActiveTextArea == null
                    || ActiveTextEditorProps == null
                  )
                {
                    return false;
                }

                TextEditorOptions opts = ConfigHelper.Current.TextEditorOptions;
                return (opts != null && opts.ShowFoldMarkers && opts.FoldComments);
            }
        }

        private bool CanFoldCodeBlocks
        {
            get
            {
                if
                  (
                    ConfigHelper.Current == null
                    || ConfigHelper.Current.TextEditorOptions == null
                    || ActiveTextArea == null
                    || ActiveTextEditorProps == null
                  )
                {
                    return false;
                }
                TextEditorOptions opts = ConfigHelper.Current.TextEditorOptions;
                return (opts != null && opts.ShowFoldMarkers && opts.FoldCodeBlocks);
            }
        }

        private bool CanHighlightSelectCaseBlocks
        {
            get
            {
                if
                  (
                    ConfigHelper.Current == null
                    || ConfigHelper.Current.TextEditorOptions == null
                    || ActiveTextArea == null
                    || ActiveTextEditorProps == null
                  )
                {
                    return false;
                }
                TextEditorOptions opts = ConfigHelper.Current.TextEditorOptions;
                return (opts != null && opts.HighlightSelectAndCase);
            }
        }

        private bool CanHighlightComments
        {
            get
            {
                if
                  (
                    ConfigHelper.Current == null
                    || ConfigHelper.Current.TextEditorOptions == null
                    || ActiveTextArea == null
                    || ActiveTextEditorProps == null
                  )
                {
                    return false;
                }
                TextEditorOptions opts = ConfigHelper.Current.TextEditorOptions;
                return (opts != null && opts.HighlightComments);
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

        private void WireUpTextEditor()
        {

            base.CurrentSytaxMode = "SQL";
            _textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");

            ConfigHelper.Current.TextEditorOptions.ApplyToControl(_textEditor);
            _textEditor.ContextMenuStrip = popUpEditor;
            _textEditor.Document.DocumentChanged += new DocumentEventHandler(OnDocumentChanged);
            ActiveTextArea.DragDrop += new DragEventHandler(_textEditor_DragDrop);
            ActiveTextArea.DragOver += new DragEventHandler(_textEditor_DragOver);

            ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextEditorKeyDown);
            ActiveTextArea.Caret.PositionChanged += new EventHandler(OnCaretPositionChanged);
            ActiveTextEditorProps.UseCustomLine = true;
        }


        private void OnCaretPositionChanged(object sender, EventArgs e)
        {
            FireBeforeCaretPositionChanged();

            statCaretPos.Text = "Ln: " + (ActiveTextArea.Caret.Line + 1).ToString()
              + " , Col: " + (ActiveTextArea.Caret.Column + 1).ToString()
              + " ( Offset: " + ActiveTextArea.Caret.Offset.ToString() + " )";

            if (!ActiveTextArea.SelectionManager.HasSomethingSelected)
            {
                ApplyCustomLineHighlighting();
            }

            FireAfterCaretPositionChanged();
        }

        private void OnTextEditorKeyDown(object sender, KeyEventArgs e)
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

        private DateTime? _lastContentChangeTime = null;
        private void OnDocumentChanged(object sender, DocumentEventArgs e)
        {
            if (e.Document.UndoStack.CanUndo)
            {
                if (!ContentModified)
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

            if ((_parseSelectsAndCases || _parseCodeBlocks || _parseComments))
            {
                _lastContentChangeTime = DateTime.Now;
                //RunParserThread();
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

        private void MarkSelectionAsCodeBlock()
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
                ActiveTextArea.Invalidate();
            }
        }

        public void AppendScriptAsCodeBlock(string script, string codeBlockComment)
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
                ActiveTextArea.Invalidate();
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

        private void InvalidateButtonsAndMenuItems(bool isRunning)
        {
            btnRun.Enabled = !isRunning;
            btnMultiExec.Enabled = !isRunning;
            btnCheckSyntax.Enabled = !isRunning;
            btnShowPlan.Enabled = !isRunning;
            btnStop.Enabled = isRunning;


            btnSave.Enabled = !isRunning;
            btnSaveAs.Enabled = !isRunning;
            btnOpen.Enabled = !isRunning;
            cmbDatabases.Enabled = !isRunning;
            cmbServers.Enabled = !isRunning;



            mnuItemRun.Enabled = btnRun.Enabled;
            mnuItemMultiRun.Enabled = btnMultiExec.Enabled;
            mnuItemCheckSyntax.Enabled = btnCheckSyntax.Enabled;
            mnuItemShowPlan.Enabled = btnShowPlan.Enabled;
            mnuItemStop.Enabled = btnStop.Enabled;

            cMnuItemSave.Enabled = !isRunning;
            mnuItemCheckSyntax.Enabled = !isRunning;
            cmbResultRenderers.Enabled = !isRunning;
            btnDefaultRenderer.Enabled = !isRunning;
            strip3.Enabled = !isRunning;
            if (_addInToolStrip != null)
                _addInToolStrip.Enabled = !isRunning;

            btnReconnect.Enabled = !isRunning;
            btnChangeDb.Enabled = !isRunning;
        }

        public int LineCount(string value)
        {
            string[] lines = value.Replace("\r\n", "\r").Split('\r');
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

        public void AddInfoMsqToList(string Msg)
        {
            AddMessageToList(SqlMessage.CreateInfoMessage(Msg));
        }

        public void AddMsgToList(string Msg)
        {
            AddMessageToList(SqlMessage.CreateMessage(Msg));
        }

        private void AddMessageToList(SqlMessage sqlMessage)
        {
            if (!lv.InvokeRequired)
            {
                AddMessageToList_Internal(sqlMessage);
            }
            else
            {
                object[] param = new object[1] { sqlMessage };
                lv.Invoke(new OnPublishSqlMessageInListViewDelegate(AddMessageToList_Internal), param);
            }
        }

        public void AddMessageToList_Internal(SqlMessage sqlMessage)
        {

            if (sqlMessage == null)
            {
                return;
            }

            int imgIndex = sqlMessage.MsgType == MessageType.Bold ? -1 : (int)sqlMessage.MsgType;
            ListViewItem item = lv.Items.Add(String.Empty, imgIndex);
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
                case MessageType.Bold:
                    foreColor = Color.Black;
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

            Font f = null;
            if (sqlMessage.MsgType == MessageType.Bold)
            {
                f = new Font(lv.Font, FontStyle.Bold);
            }
            else
                f = lv.Font;

            item.SubItems.Add(sqlMessage.Message, foreColor, bgColor, f);
            item.SubItems.Add(lineText, foreColor, bgColor, f);
            item.SubItems.Add(typeText, foreColor, bgColor, f);
            item.SubItems.Add(stateText, foreColor, bgColor, f);
            item.SubItems.Add(sqlMessage.Server, foreColor, bgColor, f);
            item.SubItems.Add(sqlMessage.Database, foreColor, bgColor, f);
        }

        private void AutoResizeMessageListColumns()
        {
            if (!lv.InvokeRequired)
            {
                AutoResizeMessageListColumns_Internal();
            }
            else
            {
                lv.Invoke(new ActionF(AutoResizeMessageListColumns_Internal));
            }
        }

        private void AutoResizeMessageListColumns_Internal()
        {
            lv.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            lv.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        public void HideTextEditor()
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
            buttonSpecAny1.Visible = false;
        }


        private void CreateNewScriptEditor()
        {
            frmScriptEditor editor = ScriptEditorFactory.Create(CurrentConnectionParams);
            ScriptEditorFactory.ShowScriptEditor(editor);
        }

        private void AnalyzeSQL()
        {
            ClearMessagesList();
            if (!OutputPaneVisible)
            {
                OutputPaneVisible = true;
            }

            string sqlText = ActiveDocument.TextContent;
            AddInfoMsqToList("SQL Text Original");
            AddMsgToList(sqlText);
            sqlText = SqlAnalyzeHelper.RemoveComments(sqlText);
            AddInfoMsqToList("SQL Text Comments removed");
            AddMsgToList(sqlText);


            SqlAnalyzerResults r = SqlAnalyzer.AnalyzeSql(sqlText, true);
            AddInfoMsqToList("Variables");
            foreach (SqlVariable v in r.Variables.Values)
            {
                AddMsgToList("Name = " + v.Name + ", DataType = " + v.DataType + ", FullName = " + v.FullyQualifiedName);
            }

            AddInfoMsqToList("Table Aliases");
            foreach (SqlTableAlias t in r.TableAliases.Values)
            {
                AddMsgToList("AliasType = " + t.AliasType.ToString() + ", TableName = " + t.TableName + ", Alias = " + t.TableAlias);
            }

            AddInfoMsqToList("Table Definitions");
            foreach (SqlTable t in r.Tables.Values)
            {
                AddMsgToList("Table \"" + t.TableName + "\" Type \"" + t.TableType.ToString() + "\"");
                foreach (string col in t.FullyQualifiedColumns)
                {
                    AddMsgToList(" Column: " + col);
                }
            }
            AutoResizeMessageListColumns();
        }

        private frmDbObjectSearch _searchForm = null;

        private void ShowDatabaseSearchForm()
        {
            string caption = String.Format("Search Db ({0})", SqlConnParams.InfoDbServer);
            string keyword = ActiveTextArea.SelectionManager.SelectedText;
            if (String.IsNullOrEmpty(keyword))
            {
                keyword = WordAtCursor;
            }

            if (_searchForm != null)
            {

                if (_searchForm.IsSearching)
                {
                    if (MessageService.AskQuestion("Search is in progress!\r\nStop search?"))
                        _searchForm.StopSearch();
                    else
                    {
                        DBObjectSearchFactory.ShowForm(_searchForm);
                        return;
                    }
                }

                _searchForm.Caption = caption;
                _searchForm.ClearResultsAndCriterias();
                _searchForm.AddSearchTextCriteria(keyword);
                _searchForm.ConnParams = null;
                _searchForm.ConnParams = SqlConnParams;
                _searchForm.ConnParams.Database = cmbDatabases.Text;
                _searchForm.PerformSearch();
            }
            else
            {
                _searchForm = DBObjectSearchFactory.CreateDBObjectSearchForm(SqlConnParams, cmbDatabases.Text, caption, keyword);
                _searchForm.FormClosed += new FormClosedEventHandler(_searchForm_FormClosed);
            }

            DBObjectSearchFactory.ShowForm(_searchForm);
        }

        void _searchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _searchForm = null;
        }

        private void AddObjectToGroup()
        {
            if (_isExecuting)
            {
                MessageBox.Show("Can not perform this operation while script is being executed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string keyword = ActiveTextArea.SelectionManager.SelectedText;
            if (String.IsNullOrEmpty(keyword))
            {
                keyword = WordAtCursor;
            }

            if (String.IsNullOrEmpty(keyword))
            {
                return;
            }


            ObjectInfo objInfo = ProgrammabilityHelper.GetObjectInfo(SqlConn, String.Empty, keyword);
            if ((objInfo == null || objInfo.ObjectID <= 0) || (!DBConstants.DoesObjectTypeHasScript(objInfo.ObjectType) && !DBConstants.DoesObjectTypeHoldsData(objInfo.ObjectType)))
            {
                MessageBox.Show(String.Format("Object not found in database: {0}", keyword), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmObjGroupDlg.ShowObjectGroupingDlg(CurrentConnectionParams, objInfo, "Add To Object Group");

        }

#if PERSONAL_EDITION
    private void ShowObjectGroupingStats()
    {
      throw new PersonalEditionLimitation();
    }
#else
        private void ShowObjectGroupingStats()
        {
            if (_isExecuting)
            {
                MessageBox.Show("Can not perform this operation while script is being executed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            string keyword = ActiveTextArea.SelectionManager.SelectedText;
            if (String.IsNullOrEmpty(keyword))
            {
                keyword = WordAtCursor;
            }

            if (String.IsNullOrEmpty(keyword))
            {
                return;
            }

            ObjectGroupingService svc = new ObjectGroupingService();
            ConnectionParams cp = CurrentConnectionParams;
            svc.ConnParams = cp;
            if (!svc.IsObjectGroupingSupportInstalled())
            {
                MessageBox.Show("Object grouping not installed to this database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ObjectInfo objInfo = ProgrammabilityHelper.GetObjectInfo(SqlConn, String.Empty, keyword);
            if ((objInfo == null || objInfo.ObjectID <= 0) || (!DBConstants.DoesObjectTypeHasScript(objInfo.ObjectType) && !DBConstants.DoesObjectTypeHoldsData(objInfo.ObjectType)))
            {
                MessageBox.Show(String.Format("Object not found in database: {0}", keyword), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string caption = "Obj. Grp. Stat. for " + keyword + " {" + cp.Database + " on " + cp.Server + "}";
            string script = String.Format(PragmaSQL.Core.ResManager.GetDBScript("Script_ObjectGroupingSupportGroupStats"), keyword);

            NodeData data = new NodeData(-1);
            data.ConnParams = cp.CreateCopy();
            data.DBName = cp.Database;
            data.Name = keyword;

            frmDataViewer editor = DataViewerFactory.CreateDataViewer(data, caption, script, true, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.library_icon;
            DataViewerFactory.ShowDataViewer(editor);

        }
#endif

        private frmObjectChangeHistoryViewer _objChangeHist = null;
        private void ShowObjectChangeHistory(ObjectInfo objInfo)
        {
            if (objInfo == null)
                return;

            if (_objChangeHist != null)
            {

                if (_objChangeHist.IsSearching)
                {
                    if (MessageService.AskQuestion("Loading object change history data for another object!\r\nDo you want to cancel this operation?"))
                        _objChangeHist.StopSearch();
                    else
                    {
                        ObjectChangeHistoryViewerFactory.ShowViewer(_objChangeHist);
                        return;
                    }
                }

            }
            else
            {
                _objChangeHist = ObjectChangeHistoryViewerFactory.CreateViewer();
                _objChangeHist.FormClosed += new FormClosedEventHandler(_objChangeHist_FormClosed);
            }

            _objChangeHist.SetCriteria(SqlConnParams.Server, cmbDatabases.Text, objInfo.ObjectType, objInfo.ObjectName);
            _objChangeHist.PerformSearch();
            ObjectChangeHistoryViewerFactory.ShowViewer(_objChangeHist);
        }

        void _objChangeHist_FormClosed(object sender, FormClosedEventArgs e)
        {
            _objChangeHist = null;
        }

        private void ReOpenConnection(SqlConnection conn)
        {
            if (conn == null)
                return;

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
        }

        #endregion //Utilities

        #region Query Execution

        public void ExecScript(string script, ScriptRunType scriptRunType, int selStartLine, bool isMultiDbOperation, bool isObjectHelpScript)
        {
            if (SqlConn == null)
                throw new Exception("No database connection selected.");


            timerExec.Enabled = true;

            if (SqlConn.State != ConnectionState.Open)
            {
                SqlConn.Open();
            }

            if (String.IsNullOrEmpty(_textEditor.Text))
            {
                return;
            }

            ClearOutputPane();

            OutputPaneVisible = true;
            switch (scriptRunType)
            {
                case ScriptRunType.Execute:
                    AddMessageToList(SqlMessage.CreateMessage("Executing script. Please wait."));
                    break;
                case ScriptRunType.CheckSyntax:
                    AddMessageToList(SqlMessage.CreateMessage("Checking syntax. Please wait."));
                    break;
                case ScriptRunType.ShowPlan:
                    AddMessageToList(SqlMessage.CreateMessage("Showing plan. Please wait."));
                    break;
            }

            _lastExecutedScriptData = new ScriptData();
            _lastExecutedScriptData.Server = SqlConn.DataSource;
            _lastExecutedScriptData.Database = SqlConn.Database;
            _lastExecutedScriptData.IsObjectHelpScript = isObjectHelpScript;

            _lastExecutedScriptData.SelStartLineNo = (selStartLine >= 0) ? selStartLine : 0;

            string scriptText = script;


            _lastExecutedScriptData.ScriptText = scriptText;
            _lastExecutedScriptData.ScriptRunType = scriptRunType;
            _lastExecutedScriptData.IsMultiDbOperation = isMultiDbOperation;
            _scriptExecuterThread.RunWorkerAsync(_lastExecutedScriptData);
            InvalidateButtonsAndMenuItems(true);
        }

        public void ExecScript(ScriptRunType scriptRunType)
        {
            ExecScript(scriptRunType, false);
        }

        public void ExecScript(ScriptRunType scriptRunType, bool isMultiDbOperation)
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

            ExecScript(scriptText, scriptRunType, selStartLineNo, isMultiDbOperation, false);
        }

        private void ExecuteScriptInMultiDb()
        {
            //SerializableDictionary<string, ConnectionParams> tmp = frmMultiConnectionSpec.SelectConnections(ConnParams, _multiExecDbList);
            SerializableDictionary<string, ConnectionParams> tmp = frmMultiConnectionSpec2.SelectConnections(_multiExecDbList, ref _multiExecTemplateName);
            if (tmp == null || tmp.Count == 0)
                return;

            _multiExecDbList = tmp;
            ExecScript(ScriptRunType.Execute, true);
        }

        private void CancelScriptExecution()
        {
            timerExec.Enabled = false;
            _scriptExecuterThread.CancelAsync();
            if (_cmd != null)
            {
                _cmd.Cancel();
            }
        }

        private void DoExecuteScriptInBackground(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            ScriptData arg = (ScriptData)e.Argument;


            // Start the time-consuming operation.
            if (!arg.IsMultiDbOperation)
            {
                ScriptExecutionResult eResult = ExecuteScriptAsync(SqlConn, ConnParams, bw, arg);
                IList<ScriptExecutionResult> eList = new List<ScriptExecutionResult>();
                if (eResult != null)
                    eList.Add(eResult);

                e.Result = eList;
            }
            else
                e.Result = ExecuteScriptInMultiDbAsync(bw, arg);

            //e.Result = ExecuteScriptWithReader(bw, arg);

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void OnScriptExecutionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            _execResults = null;

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
                //_sqlMessages.Add
                AddMessageToList(SqlMessage.CreateWarningMessage("Operation cancelled by the user.", _activeConn));
                //status = ScriptCompletionStatus.Cancelled;
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);

                //MessageBox.Show(msg);
                //status = ScriptCompletionStatus.HasErrors;
            }
            else
            {
                //Completed normally
                _execResults = (IList<ScriptExecutionResult>)e.Result;
                RenderResults(_execResults);
                //DisposeScriptExecutionResults(_execResults);
                //_execResults = null;

                if (
                  _lastExecutedScriptData.ScriptRunType == ScriptRunType.Execute
                    && AutoRemoveModSignAfterCommit
                    && ContentModified
                    && DBConstants.DoesObjectTypeHasScript(_objectType)
                  )
                {
                    ContentModified = false;
                }
                //status = ScriptCompletionStatus.Completed;
                IsRecoveredContent = false;
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

        int _totalLineCnt = 0;
        int _currentLineCnt = 0;
        int _selStartLineNo = 0;

        private IList<ScriptExecutionResult> ExecuteScriptInMultiDbAsync(BackgroundWorker bw, ScriptData scriptData)
        {
            IList<ScriptExecutionResult> result = new List<ScriptExecutionResult>();
            ScriptExecutionResult tmp = null;
            string tmpUsername = _currentConnUsername;

            try
            {
                foreach (ConnectionParams cp in _multiExecDbList.Values)
                {
                    using (SqlConnection conn = cp.CreateSqlConnection(true, false))
                    {
                        _currentConnUsername = cp.CurrentUsername;
                        tmp = ExecuteScriptAsync(conn, cp, bw, scriptData);
                        if (tmp != null)
                            result.Add(tmp);
                    }
                }
            }
            finally
            {
                _activeConn = SqlConn;
                _currentConnUsername = tmpUsername;
            }

            return result;
        }

        private ScriptExecutionResult ExecuteScriptAsync(SqlConnection conn, ConnectionParams currentCp, BackgroundWorker bw, ScriptData scriptData)
        {
            _activeConn = conn;
            _isExecuting = true;
            _completedWithErrors = false;
            _startTime = DateTime.Now;
            ScriptExecutionResult result = new ScriptExecutionResult();
            result.ConnParams = currentCp.CreateCopy();

            //ReOpenConnection(conn);

            if (conn.State != ConnectionState.Open)
            {
                AddMessageToList(SqlMessage.CreateErrorMessage("Invalid connection state: " + conn.State.ToString(), -1, -1, -1));
                return null;
            }

            _totalLineCnt = 0;
            _currentLineCnt = 0;
            _selStartLineNo = scriptData.SelStartLineNo;


            string completionMessage = String.Empty;
            bool tryToLogObjectScript = false;

            switch (scriptData.ScriptRunType)
            {
                case ScriptRunType.Execute:
                    tryToLogObjectScript = ConfigHelper.Current.CanLogObjectChanges();
                    completionMessage = "Command(s) completed.";
                    break;
                case ScriptRunType.CheckSyntax:
                    completionMessage = "Syntax check completed.";
                    break;
                case ScriptRunType.ShowPlan:
                    completionMessage = "Show plan completed.";
                    break;
            }


            IList<DataSet> dataSets = new List<DataSet>();
            result.DataSets = dataSets;

            if (_scriptExecuterThread.CancellationPending)
            {
                return result;
            }


            object dResult = this.Invoke(new FireScriptExecutingDelegate(FireScriptExecutingEvent), scriptData.ScriptRunType, scriptData.IsObjectHelpScript);
            if (Convert.ToBoolean(dResult))
            {
                if (!scriptData.IsMultiDbOperation)
                {
                    _scriptExecuterThread.CancelAsync();
                    return result;
                }
                else
                {
                    AddMessageToList(SqlMessage.CreateWarningMessage("Operation cancelled by the user.", _activeConn));
                    return null;
                }
            }

            try
            {
                conn.FireInfoMessageEventOnUserErrors = true;
                conn.InfoMessage += new SqlInfoMessageEventHandler(HandleSqlInfo);
                conn.StatisticsEnabled = true;

                switch (scriptData.ScriptRunType)
                {
                    case ScriptRunType.CheckSyntax:
                        _cmd = new SqlCommand("SET PARSEONLY ON", conn);
                        _cmd.CommandTimeout = 0;
                        _cmd.ExecuteNonQuery();
                        break;
                    case ScriptRunType.ShowPlan:
                        _cmd = new SqlCommand("SET SHOWPLAN_ALL ON", conn);
                        _cmd.CommandTimeout = 0;
                        _cmd.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }

                int batchNo = 0;
                IList<BatchInfo> batches = ScriptingHelper.SplitBatches(scriptData.ScriptText);
                while (batches.Count > 0)
                {
                    batchNo++;
                    try
                    {
                        conn.ResetStatistics();

                        string batch = batches[0].Content;

                        _currentLineCnt = batches[0].LineCount;// LineCount(batch);
                        //_currentLineCnt++;
                        _totalLineCnt += _currentLineCnt;

                        if (String.IsNullOrEmpty(batch))
                        {
                            batches.RemoveAt(0);
                            AddMessageToList(SqlMessage.CreateMessage(completionMessage, _activeConn));
                            continue;
                        }


                        batches.RemoveAt(0);

                        _cmd = new SqlCommand(batch, conn);
                        _cmd.CommandTimeout = 0;

                        DataSet toFill = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = _cmd;
                        int recordsAffected = adapter.Fill(toFill);


                        toFill.ExtendedProperties.Add("ConnInfo", ConnectionParams.PrepareConnKeyWithDb(currentCp));
                        toFill.ExtendedProperties.Add("BatchNo", batchNo.ToString());

                        if (toFill.Tables.Count > 0)
                            dataSets.Add(toFill);

                        ProcessConnectionStatistics(conn.RetrieveStatistics());

                        if (tryToLogObjectScript)
                        {
                            try
                            {
                                ObjectChangeHistoryFacade.InsertObjectChangeHistoryRecord(ObjectChangeHistoryData.CreateFromScript(SqlConnParams.CurrentUsername, conn, batch));
                            }
                            catch (Exception ex)
                            {
                                AddMessageToList(SqlMessage.CreateWarningMessage("PragmaSQL can not log object change history. Error was:" + ex.Message, _activeConn));
                            }
                        }

                        if (batches.Count == 0)
                        {
                            AddMessageToList(SqlMessage.CreateMessage(completionMessage, _activeConn));
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        if (bw.CancellationPending)
                        {
                            return result;
                        }

                        _completedWithErrors = true;
                        int lineNo = _totalLineCnt - _currentLineCnt + sqlEx.LineNumber + scriptData.SelStartLineNo;
                        AddMessageToList(SqlMessage.CreateErrorMessage(sqlEx.Message, _activeConn, lineNo, sqlEx.Number, sqlEx.State));
                    }
                    catch (Exception ex)
                    {
                        if (bw.CancellationPending)
                        {
                            return result;
                        }

                        _completedWithErrors = true;
                        AddMessageToList(SqlMessage.CreateErrorMessage("Exception of type \"" + ex.GetType().ToString() + "\": " + ex.Message, _activeConn));
                    }
                }
            }
            finally
            {
                switch (scriptData.ScriptRunType)
                {
                    case ScriptRunType.CheckSyntax:
                        _cmd = new SqlCommand("SET PARSEONLY OFF", conn);
                        _cmd.CommandTimeout = 0;
                        _cmd.ExecuteNonQuery();
                        break;
                    case ScriptRunType.ShowPlan:
                        _cmd = new SqlCommand("SET SHOWPLAN_ALL OFF", conn);
                        _cmd.CommandTimeout = 0;
                        _cmd.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }

                conn.FireInfoMessageEventOnUserErrors = false;
                conn.InfoMessage -= new SqlInfoMessageEventHandler(HandleSqlInfo);
                conn.StatisticsEnabled = false;
            }

            ScriptCompletionStatus status = ScriptCompletionStatus.Completed;

            if (_completedWithErrors)
                status = ScriptCompletionStatus.HasErrors;
            else if (bw.CancellationPending)
                status = ScriptCompletionStatus.Cancelled;

            this.Invoke(new FireScriptExecutedDelegate(FireScriptExecutedEvent), status);
            return result;
        }

        private void ProcessConnectionStatistics(IDictionary stats)
        {
            foreach (object key in stats.Keys)
            {
                object statVal = stats[key];
                if (key == null || key.ToString().ToLowerInvariant() != "IduRows".ToLowerInvariant())
                    continue;

                string statValStr = statVal.ToString();
                if (String.IsNullOrEmpty(statValStr) || statValStr.Trim() == "0")
                    continue;

                //_sqlMessages.Add
                AddMessageToList(SqlMessage.CreateInfoMessage("( " + statValStr + " row(s) affected )", _activeConn));
            }

        }

        public DataTable GetTableFromReader(SqlDataReader _reader)
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

        private void HandleSqlInfo(object infoObj, SqlInfoMessageEventArgs e)
        {
            foreach (SqlError err in e.Errors)
            {
                int lineNo = _totalLineCnt - _currentLineCnt + err.LineNumber + _selStartLineNo;
                if (err.Number == 0 && !String.IsNullOrEmpty(err.Message.Trim()))
                {
                    AddMessageToList(SqlMessage.CreateInfoMessage(err.Message, _activeConn, lineNo, err.State));
                }
                else if (err.Number != 0)
                {
                    _completedWithErrors = true;
                    AddMessageToList(SqlMessage.CreateErrorMessage(err.Message, _activeConn, lineNo, err.Number, err.State));
                }
            }
        }

        #endregion //Query Execution

        #region Rendering functions
        IResultRendererFactory _currentResultRendererFactory = null;
        private void RenderResults(IList<ScriptExecutionResult> dataSets)
        {

            try
            {
                RenderResultMessages(dataSets);
                ResultRendererSpec item = cmbResultRenderers.SelectedItem as ResultRendererSpec;

                if (item == null)
                {
                    MessageService.ShowWarning("Result renderer not selected.\r\nWill render result with built in renderer.");
                    UseDefaultResultRenderer(dataSets);
                    return;
                }


                Type t = item.RendererType;
                if (t == null)
                {
                    MessageService.ShowWarning(String.Format("Can not load result renderer of type \"{0}\"\r\n.", item.FullName) + "Will render result with built in renderer.");
                    UseDefaultResultRenderer(dataSets);
                    return;
                }

                _currentResultRendererFactory = Activator.CreateInstance(t) as IResultRendererFactory;
                _currentResultRendererFactory.RenderResults(this, tabOutput, dataSets);

            }
            catch (Exception ex)
            {
                GenericErrorDialog.ShowError("Error", "Result renderer can not be created.", ex);
            }

        }

        private void UseDefaultResultRenderer(IList<ScriptExecutionResult> execResults)
        {
            _currentResultRendererFactory = new ResultRendererFactory_SeperatePageForEach();
            _currentResultRendererFactory.RenderResults(this, tabOutput, execResults);
        }

        private void RenderResultMessages(IList<ScriptExecutionResult> execResults)
        {

            string batchMsg = String.Empty;
            string qryMsg = String.Empty;

            int queryNo = 0;

            if (execResults != null && execResults.Count > 0)
            {
                foreach (ScriptExecutionResult eResult in execResults)
                {
                    if (eResult == null || eResult.DataSets == null || eResult.DataSets.Count == 0)
                        continue;

                    AddMessageToList(SqlMessage.CreateBoldMessage(String.Format("Execution summary for ({0})", eResult.ConnParams.InfoDbServer), eResult.ConnParams.Server, eResult.ConnParams.Database));
                    foreach (DataSet ds in eResult.DataSets)
                    {
                        batchMsg = String.Empty;
                        DataSetInfo dsInfo = GetDataSetInfo(ds);
                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables.Count; i++)
                            {
                                qryMsg = String.Empty;
                                queryNo = i + 1;
                                int recordCnt = ds.Tables[i].Rows.Count;

                                if (i == 0)
                                {
                                    batchMsg = String.Format("{0} resultset(s) were returned.", ds.Tables.Count);
                                    batchMsg = String.Format("Batch #{0} ({1}) : {2}", dsInfo.BatchNo, dsInfo.ServerDbInfo, batchMsg);
                                    AddMessageToList(SqlMessage.CreateInfoMessage(batchMsg, dsInfo.ServerName, dsInfo.DbName));
                                }

                                qryMsg = String.Format("  Qry {0}.{1} : Returned {2} record(s)", dsInfo.BatchNo, queryNo, recordCnt);
                                AddMessageToList(SqlMessage.CreateMessage(qryMsg, dsInfo.ServerName, dsInfo.DbName));
                            }
                        }
                        else
                        {
                            AddMessageToList(SqlMessage.CreateWarningMessage(String.Format("Batch #{0} ({1})", dsInfo.BatchNo, dsInfo.ServerDbInfo), dsInfo.ServerName, dsInfo.DbName));
                            AddMessageToList(SqlMessage.CreateMessage("  No results returned.", dsInfo.ServerName, dsInfo.DbName));
                        }

                    }

                }
            }
        }

        void grd_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0)
                e.ToolTipText = Properties.Resources.MultColumnSortTooltip;
        }




        private DataGridView _currentGrd = null;
        private void grd_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_currentGrd == null)
                return;

            foreach (DataGridViewColumn col in _currentGrd.Columns)
            {
                //if (col.ValueType == typeof(bool))
                //	col.SortMode = DataGridViewColumnSortMode.Automatic;
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        public DataSetInfo GetDataSetInfo(DataSet dataSet)
        {
            DataSetInfo result = new DataSetInfo();


            if (dataSet == null)
                return result;

            if (dataSet.ExtendedProperties.ContainsKey("ConnInfo"))
            {
                string[] ck = ConnectionParams.ParseConnKey(dataSet.ExtendedProperties["ConnInfo"] as string);
                if (ck.Length >= 1)
                    result.ServerName = ck[0];
                if (ck.Length >= 3)
                    result.DbName = ck[2];

                result.ServerDbInfo = String.Format("{0} on {1} ", result.DbName, result.ServerName);
            }
            else if (_activeConn != null)
            {
                result.ServerName = _activeConn.DataSource;
                result.DbName = _activeConn.Database;
                result.ServerDbInfo = String.Format("{0} on {1} ", result.DbName, result.ServerName);
            }

            if (dataSet.ExtendedProperties.ContainsKey("BatchNo"))
            {
                result.BatchNo = dataSet.ExtendedProperties["BatchNo"] as string;
            }

            return result;
        }


        private void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                DataGridViewColumn col = ((DataGridView)sender).Columns[e.ColumnIndex];
                if (col.ValueType == typeof(bool) && col.SortMode != DataGridViewColumnSortMode.Automatic)
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

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

                        SizeF sizeF = e.Graphics.MeasureString("(NULL)", e.CellStyle.Font);

                        /*
                        e.Graphics.DrawString((string)"(NULL)", e.CellStyle.Font,
                            br,e.CellBounds.X + 2,
                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        */
                        e.Graphics.DrawString((string)"(NULL)", e.CellStyle.Font,
                            br, e.CellBounds.Left + (e.CellBounds.Width - sizeF.Width) / 2,
                            e.CellBounds.Top + (e.CellBounds.Height - sizeF.Height) / 2, StringFormat.GenericDefault);

                        e.Handled = true;
                    }

                }

            }
        }

        private void RenderMessagesAndErrors()
        {
            OutputPaneVisible = true;
            foreach (SqlMessage msg in _sqlMessages)
            {
                AddMessageToList(msg);
            }

            AutoResizeMessageListColumns();
        }

        #endregion //Rendering function

        #region ObjectHelp Requested


        private void OnObjectHelpRequested(object sender, ObjectHelpActionEventArgs e)
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
                else if (e.ActionType == ActionType.SelectTop100Rows)
                {
                    ExecScript($"SELECT TOP 100 * FROM {e.objectInfo.FullNameQuoted}", ScriptRunType.Execute, 0, false, false);
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
                else if (e.ActionType == ActionType.ChangeHistory)
                {
                    ShowObjectChangeHistory(e.objectInfo);
                }
                else if (e.ActionType == ActionType.FastScriptPreview)
                {
                    ShowFastScriptPreview(e.objectInfo);
                }

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void OpenObjectScripInNewEditor(ObjectInfo objInfo)
        {
            if (objInfo == null)
            {
                return;
            }


            string script = ScriptingHelper.GetAlterScript(CurrentConnectionParams, objInfo.ObjectID, objInfo.ObjectType);
            string caption = objInfo.ObjectName;

            frmScriptEditor editor = ScriptEditorFactory.Create(caption, script, objInfo.ObjectID, objInfo.ObjectType, CurrentConnectionParams, SqlConn.Database);
            ScriptEditorFactory.ShowScriptEditor(editor);

        }



        private void OpenExecuteScriptInNewEditor(ObjectInfo objInfo)
        {
            if (objInfo == null)
            {
                return;
            }

            ConnectionParams tmp = CurrentConnectionParams;
            string script = String.Empty;
            string caption = String.Empty;
            using (SqlConnection conn = tmp.CreateSqlConnection(true, false))
            {
                script = ProgrammabilityHelper.GetProcedureExecuteScript(conn, objInfo.ObjectName);
                caption = "EXEC " + objInfo.ObjectName;
            }
            frmScriptEditor editor = ScriptEditorFactory.Create(caption, script, objInfo.ObjectID, objInfo.ObjectType, tmp, tmp.Database);
            ScriptEditorFactory.ShowScriptEditor(editor);
        }

        private void LoadTableOrViewData(ObjectInfo objInfo)
        {
            if (objInfo == null)
            {
                return;
            }

            ConnectionParams tmp = CurrentConnectionParams;
            string script = "select * from " + objInfo.FullNameQuoted;
            string caption = objInfo.FullName + " [" + tmp.InfoDbServer + "]";
            bool isReadonly = objInfo.ObjectType == DBObjectType.View ? true : false;


            frmDataViewer viewer = DataViewerFactory.CreateDataViewer(tmp, tmp.Database, objInfo.ObjectName, caption, script, isReadonly, true);

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

        private void ShowReferences(ObjectInfo objInfo)
        {
            if (objInfo == null)
            {
                return;
            }

            ClearOutputPane();

            ConnectionParams tmp = CurrentConnectionParams;

            ObjectRefList refList = new ObjectRefList();
            refList.Dock = DockStyle.Fill;
            refList.Initialize(objInfo.ObjectName, tmp, tmp.Database, RefDetail.Any);
            refList.LoadData();

            tabOutput.TabPages.Add("References [" + objInfo.FullName + "]");
            TabPage tab = tabOutput.TabPages[tabOutput.TabCount - 1];

            refList.Parent = tab;
            tabOutput.SelectTab(tab);
            tab.Tag = refList;
            OutputPaneVisible = true;
            AddMessageToList(SqlMessage.CreateBoldMessage(String.Format("Object info action executed. Object = {0}, Action = {1}", objInfo.ObjectName, "References")));

        }

        private bool AppendColsOrParamsAction(ActionType actionType, ObjectInfo objInfo)
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
                    insertThis = ProgrammabilityHelper.GetColumns(SqlConn, objInfo.ObjectName, false);
                    break;
                case ActionType.AppendColsAsParamList1:
                    insertThis = ProgrammabilityHelper.GetColumnsAsParamListWithDataType(SqlConn, objInfo.ObjectName, false);
                    break;
                case ActionType.AppendColsAsParamList2:
                    insertThis = ProgrammabilityHelper.GetColumns(SqlConn, objInfo.ObjectName, true);
                    break;
                case ActionType.AppendProcedureParams:
                    insertThis = ProgrammabilityHelper.GetRoutineParams(SqlConn, objInfo.ObjectName);
                    break;
                case ActionType.AppendFunctionParams:
                    insertThis = ProgrammabilityHelper.GetRoutineParams(SqlConn, objInfo.ObjectName);
                    break;
                case ActionType.AppendProcedureParamsWithDataType:
                    insertThis = ProgrammabilityHelper.GetRoutineParamsWithDataType(SqlConn, objInfo.ObjectName);
                    break;
                case ActionType.AppendFunctionParamsWithDataType:
                    insertThis = ProgrammabilityHelper.GetRoutineParamsWithDataType(SqlConn, objInfo.ObjectName);
                    break;
                case ActionType.AppendFunctionResultAsLocalTable:
                    insertThis = ProgrammabilityHelper.GetTableValuedFunctionResultColumns(SqlConn, objInfo.ObjectName, true, String.Empty);
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
                ActiveTextArea.Invalidate();
            }
            return true;
        }

        private bool ExecuteObjectHelp(ActionType actionType, ObjectInfo objInfo)
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
                    script = String.Format(ResManager.GetDBScript("spHelp_foreignkeys"), objInfo.ObjectName, objInfo.ObjectOwner);
                    break;
                case ActionType.ForeignKeysIn:
                    script = String.Format(ResManager.GetDBScript("spHelp_foreignkeysin"), objInfo.ObjectName, objInfo.ObjectOwner);
                    break;
                case ActionType.Constraints:
                    script = String.Format(ResManager.GetDBScript("spHelp_constraints"), objInfo.FullNameQuoted);
                    break;
                case ActionType.IdentityColumns:
                    script = String.Format(ResManager.GetDBScript("spHelp_IdentityColumns"), objInfo.ObjectName, objInfo.ObjectOwner);
                    break;
                case ActionType.UsedSpace:
                    script = String.Format(ResManager.GetDBScript("spHelp_UsedSpace"), objInfo.FullNameQuoted);
                    break;
                case ActionType.Statistics:
                    script = String.Format(ResManager.GetDBScript("spHelp_Statistics"), objInfo.ObjectName, objInfo.ObjectOwner);
                    break;
                case ActionType.Dependencies:
                    script = String.Format(ResManager.GetDBScript("spHelp_Depends"), objInfo.FullNameQuoted);
                    break;
                case ActionType.ObjectHelp:
                    script = String.Format(ResManager.GetDBScript("spHelp"), objInfo.FullNameQuoted);
                    break;
                default:
                    script = String.Empty;
                    break;
            }

            if (!String.IsNullOrEmpty(script))
            {
                result = true;
                ExecScript(script, ScriptRunType.Execute, 0, false, true);
                AddMessageToList(SqlMessage.CreateBoldMessage(String.Format("Object info action executed. Object={0}, Action={1}", objInfo.ObjectName, actionType.ToString())));
            }
            return result;
        }

        #endregion

        #region Fast Script Preview
        private IList<frmScriptPreview> _scriptPreviewForms = new List<frmScriptPreview>();
        private void ShowFastScriptPreview(ObjectInfo objInfo)
        {
            Point p = GetCaretPosition();
            frmScriptPreview frm = frmScriptPreview.ShowFastPreview(objInfo, this.SqlConnParams, p.X + 10, p.Y - 10);
            _scriptPreviewForms.Add(frm);
            frm.FormClosed += new FormClosedEventHandler(OwnedScriptPreviewFormClosed);
        }

        private void OwnedScriptPreviewFormClosed(object sender, FormClosedEventArgs e)
        {
            frmScriptPreview frm = sender as frmScriptPreview;
            if (frm == null)
                return;

            if (_scriptPreviewForms.Contains(frm))
            {
                frm.FormClosed -= new FormClosedEventHandler(OwnedScriptPreviewFormClosed);
                _scriptPreviewForms.Remove(frm);
            }
        }

        private void CloseOwnedScriptPreviewForms()
        {
            CloseOwnedScriptPreviewForms(false);
        }

        private void CloseOwnedScriptPreviewForms(bool unstickedOnly)
        {
            frmScriptPreview[] arr = new frmScriptPreview[_scriptPreviewForms.Count];
            _scriptPreviewForms.CopyTo(arr, 0);

            foreach (frmScriptPreview frm in arr)
            {
                if (unstickedOnly && frm.IsSticked)
                    continue;

                _scriptPreviewForms.Remove(frm);
                frm.FormClosed -= new FormClosedEventHandler(OwnedScriptPreviewFormClosed);
                frm.Close();

            }

            if (!unstickedOnly)
                _scriptPreviewForms.Clear();
        }

        private void ShowHideOwnedScriptPreviewForms(bool hide)
        {
            frmScriptPreview[] arr = new frmScriptPreview[_scriptPreviewForms.Count];
            _scriptPreviewForms.CopyTo(arr, 0);

            foreach (frmScriptPreview frm in arr)
            {
                if (hide)
                    frm.Hide();
                else
                    frm.Show();
            }
        }

        #endregion //Fast Script Preview

        #region WndProc Override
        private const int WM_CLOSE = 16;
        protected override void WndProc(ref Message m)
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

        public void ShowHelpOnWordAtCursor()
        {
            string keyword = ActiveTextArea.SelectionManager.SelectedText;
            if (String.IsNullOrEmpty(keyword))
            {
                keyword = WordAtCursor;
            }

            if (FireBeforeHelpRequested(keyword))
            {
                HelpProvider.ProvideHelpFor(keyword);
            }

            FireAfterHelpRequested(keyword);
        }

        #endregion

        #region Parser BackgroundWorkerThread

        private BackgroundWorker _parserThread = new BackgroundWorker();
        private bool _isParseInProgress = false;

        private void InitializeParserThread()
        {
            _parserThread.WorkerSupportsCancellation = true;
            _parserThread.DoWork += new System.ComponentModel.DoWorkEventHandler(DoBackgroundSelectCaseParsing);
            _parserThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(OnBackgroundSelectCaseParsingCompleted);
        }

        private void DoBackgroundSelectCaseParsing(object sender, DoWorkEventArgs e)
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

        private void OnBackgroundSelectCaseParsingCompleted(object sender, RunWorkerCompletedEventArgs e)
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
            ApplyCustomLineHighlighting();
        }

        private void RunParserThread()
        {
            if (_isParseInProgress)
            {
                CancelParserThread();
            }

            string tmp = ActiveDocument.TextContent.Replace("'", "?");
            tmp = tmp.Replace("\"", "?");
            _parserThread.RunWorkerAsync(tmp);
        }

        private void CancelParserThread()
        {
            _parserThread.CancelAsync();
            while (_parserThread.IsBusy)
            {
                Application.DoEvents();
            }
        }

        private void RenderFoldMarkers()
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
                PrepareFoldMarkersForComments();
                ActiveDocument.FoldingManager.UpdateFoldings(MergedFoldMarkers);
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        private void PrepareFoldMarkersForCodeBlocks()
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
                    _codeBlockFoldMarkers.Add(new FoldMarker(ActiveDocument, b.StartLine, b.StartCol, b.EndLine, ActiveDocument.GetLineSegment(b.EndLine).Length, FoldType.Region, blockText));

                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }

        }

        private void PrepareFoldMarkersForSelectAndCase()
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

                    if (maxEndLine > ActiveDocument.TotalNumberOfLines - 1)
                        maxEndLine = ActiveDocument.TotalNumberOfLines - 1;

                    if (minStartLine < Int32.MaxValue && maxEndLine > -1)
                    {
                        _selectCaseFoldMarkers.Add(new FoldMarker(ActiveDocument, minStartLine, 0, maxEndLine, ActiveDocument.GetLineSegment(maxEndLine).Length, FoldType.Region));
                    }
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        private void PrepareFoldMarkersForComments()
        {
            if (ActiveDocument == null || ActiveTextArea == null || ActiveTextEditorProps == null)
            {
                return;
            }

            try
            {
                ActiveTextArea.BeginUpdate();
                _commentFoldMarkers.Clear();
                if (!ActiveDocument.TextEditorProperties.EnableFolding || !CanFoldComments)
                {
                    return;
                }


                if (_bgParseResults == null)
                {
                    return;
                }

                for (int i = 0; i < _bgParseResults.Comments.Count; i++)
                {
                    SqlShBoundary b = _bgParseResults.Comments[i];
                    if (b.DoNotFold)
                        continue;
                    string commentText = "/*....*/";
                    _codeBlockFoldMarkers.Add(new FoldMarker(ActiveDocument, b.StartLine, b.StartCol, b.EndLine, ActiveDocument.GetLineSegment(b.EndLine).Length, FoldType.Region, commentText));
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        private void HighlightComments()
        {

            if (!CanHighlightComments)
                return;

            try
            {
                ActiveTextArea.BeginUpdate();
                if (_bgParseResults == null)
                {
                    return;
                }

                for (int i = 0; i < _bgParseResults.Comments.Count; i++)
                {
                    SqlShBoundary b = _bgParseResults.Comments[i];
                    if (b.DoNotPaint)
                        continue;

                    for (int j = b.StartLine; j <= b.EndLine; j++)
                    {
                        ActiveDocument.CustomLineManager.AddCustomLine(j, _commentBlockColor, false);
                        RememberHighlightedLine(_highlightedCommentLines, j);
                    }
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        IList<ISelection> _userSelBackup = null;
        private void BackupUserSelection()
        {
            if (_userSelBackup != null)
                _userSelBackup = null;

            _userSelBackup = new List<ISelection>(ActiveTextArea.SelectionManager.SelectionCollection);
        }

        private void RestoreUserSelection(bool clearCurrent)
        {
            if (_userSelBackup == null)
                return;

            if (clearCurrent)
                ActiveTextArea.SelectionManager.ClearSelection();

            foreach (ISelection s in _userSelBackup)
            {
                ActiveTextArea.SelectionManager.SetSelection(s);
            }
            _userSelBackup = null;
        }

        private void HighlightStatmentsByLineNr()
        {
            if (!CanHighlightSelectCaseBlocks)
                return;

            try
            {
                ActiveTextArea.BeginUpdate();

                BackupUserSelection();
                ActiveTextArea.SelectionManager.ClearSelection();

                if (_bgParseResults == null)
                {
                    return;
                }

                if (_bgParseResults.StartLocations.Count == 0)
                    return;

                SqlShParserLocation loc = SqlShParserLocation.NullParserLocation();
                int caretLineNr = ActiveTextArea.Caret.Line;
                int indexOf = -1;

                ArrayList locs = (ArrayList)_bgParseResults.StartLocations.Clone();
                locs.Sort(new LocationLineComparer());
                indexOf = locs.BinarySearch(
                    new SqlShParserLocation(ActiveTextArea.Caret.Line
                    , ActiveTextArea.Caret.Column
                    , ActiveTextArea.Caret.Offset)
                  , new LocationLineComparer());


                if (indexOf < 0)
                {
                    indexOf = ~indexOf;
                    indexOf--;
                }

                if (indexOf < 0)
                    return;

                loc = (SqlShParserLocation)locs[indexOf];
                if (loc.IsNullParserLocation)
                {
                    return;
                }

                _currentSqlStmn = loc.Statement;
                if (_currentSqlStmn == null)
                {
                    return;
                }

                if (!_currentSqlStmn.IsOffsetBetweenAnyBoundary(ActiveTextArea.Caret.Offset))
                    return;

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

                foreach (SqlShBoundary b in _currentSqlStmn.Boundaries)
                {
                    for (int i = b.StartLine; i <= b.EndLine; i++)
                    {
                        ActiveDocument.CustomLineManager.AddCustomLine(i, c, false);
                        RememberHighlightedLine(_highlightedSelectCaseLines, i);
                    }
                }
            }
            finally
            {
                RestoreUserSelection(false);
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        private void ApplyCustomLineHighlighting()
        {
            ActiveDocument.CustomLineManager.Clear();
            HighlightStatmentsByLineNr();
            HighlightComments();
        }

        private SqlParserResult ParseScript2(string ScriptText)
        {
            _tokenizer.SetInput(ScriptText);
            SqlShallowParser parser = new SqlShallowParser();
            parser.ParseSelectsAndCases = _parseSelectsAndCases;
            parser.ParseCodeBlocks = _parseCodeBlocks;
            parser.ParseComments = _parseComments;

            return parser.ParseScript(_parserThread, _tokenizer, ScriptText);
        }

        private void RememberHighlightedLine(IList<int> list, int lineNr)
        {
            if (list == null || list.Contains(lineNr))
                return;
            list.Add(lineNr);
        }

        private void ForgetHighlightedLine(IList<int> list, int lineNr)
        {
            if (list == null || !list.Contains(lineNr))
                return;
            list.Remove(lineNr);
        }

        private void ClearCustomLines(IList<int> list)
        {
            if (list == null)
                return;

            foreach (int lineNr in list)
            {
                ActiveDocument.CustomLineManager.RemoveCustomLine(lineNr);
            }
            list.Clear();
        }

        #endregion

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

            ReclaimWindowNumber();
            ScriptEditorManager.Forget(_windowId);

            FireAfterOpenedFile(statLblContentInfo.Text);

            return true;
        }

        public void SaveContentAs()
        {
            if (_contentPersister == null)
                _contentPersister = new DefaultContentPersister();

            _contentPersister.BeforeSavedContentToFile += new BeforeSavedContentToFileDelegate(_contentPersister_BeforeSavedContentToFile);
            try
            {
                if (_contentPersister.SaveContentAs(_caption, _textEditor))
                {
                    statLblContentInfo.Text = _contentPersister.FilePath;
                    Caption = _contentPersister.Hint;
                    ContentModified = false;
                    IsRecoveredContent = false;
                    //ContentSaved = true;
                    base.FileName = _contentPersister.FilePath;
                    ReclaimWindowNumber();
                    ScriptEditorManager.Forget(_windowId);
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
            base.FireBeforeSaveContentToFile(args.FileName);
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
                statLblContentInfo.Text = _contentPersister.FilePath;
                Caption = _contentPersister.Hint;
                ContentModified = false;
                IsRecoveredContent = false;

                base.FileName = _contentPersister.FilePath;

                ReclaimWindowNumber();
                ScriptEditorManager.Forget(_windowId);

                FireAfterSavedContent(statLblContentInfo.Text);
            }
        }
        #endregion //Content I/O

        #endregion IPragmaEditor Members

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
                    statLblContentInfo.Text = _contentPersister.FilePath;
                    Caption = _contentPersister.Hint;
                    ContentModified = false;
                    IsRecoveredContent = false;

                    base.FileName = _contentPersister.FilePath;

                    ReclaimWindowNumber();
                    ScriptEditorManager.Forget(_windowId);
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

        #region IScriptEditor Members

        public void EditObject(string objName)
        {
            ObjectInfo objInfo = ProgrammabilityHelper.GetObjectInfo(SqlConn, SqlConn.Database, objName);
            OpenObjectScripInNewEditor(objInfo);
        }

        public void AddMessage(MessageType msgType, string msg)
        {
            switch (msgType)
            {
                case MessageType.Info:
                    AddMessageToList(SqlMessage.CreateInfoMessage(msg));
                    break;
                case MessageType.Error:
                    AddMessageToList(SqlMessage.CreateErrorMessage(msg, -1, -1, -1));
                    break;
                case MessageType.Warning:
                    AddMessageToList(SqlMessage.CreateWarningMessage(msg));
                    break;
                case MessageType.None:
                    AddMessageToList(SqlMessage.CreateMessage(msg));
                    break;
                default:
                    throw new Exception("Unknown message type!");
            }
        }

        public void ClearMessagesList()
        {
            if (!lv.InvokeRequired)
            {
                ClearMessagesList_Internal();
            }
            else
            {
                lv.Invoke(new ActionF(ClearMessagesList_Internal));
            }
        }

        public void ClearMessagesList_Internal()
        {
            lv.Items.Clear();
            _sqlMessages.Clear();
        }

        private void DisposeResultTables()
        {
            foreach (DataTable t in _resultTables)
            {
                t.Clear();
                t.Dispose();
            }
            _resultTables.Clear();
        }

        private void DisposeScriptExecutionResults(IList<ScriptExecutionResult> execResults)
        {
            if (execResults == null)
                return;

            foreach (ScriptExecutionResult eResult in execResults)
            {
                if (eResult.DataSets == null || eResult.DataSets.Count == 0)
                    continue;

                foreach (DataSet d in eResult.DataSets)
                {
                    d.Tables.Clear();
                    d.Dispose();
                }
            }

            execResults.Clear();
        }

        public void ClearResults()
        {
            try
            {
                if (_currentResultRendererFactory != null)
                {
                    try
                    {
                        _currentResultRendererFactory.DisposeFactory();
                        _currentResultRendererFactory = null;
                    }
                    catch (Exception ex)
                    {
                        HostServicesSingleton.HostServices.MsgService.AddMessage(MessageType.Error, ex.Message, MethodInfo.GetCurrentMethod());
                        _currentResultRendererFactory = null;
                    }
                }

                TabPage tPage = null;
                tabOutput.SuspendLayout();
                tabOutput.SelectTab(0);
                while (tabOutput.TabPages.Count > 1)
                {
                    tPage = tabOutput.TabPages[1];
                    tPage.Controls.Clear();
                    tabOutput.TabPages.Remove(tPage);
                    Application.DoEvents();
                }
                DisposeResultTables();
                DisposeScriptExecutionResults(_execResults);
                _execResults = null;
            }
            finally
            {
                tabOutput.ResumeLayout();
            }
        }

        public void ClearOutputPane()
        {
            ClearMessagesList();
            ClearResults();
        }

        private int _objectType = DBObjectType.None;
        public int ObjectType
        {
            get { return _objectType; }
            set { _objectType = value; }
        }


        public string Server
        {
            //get { return cmbServers.SelectedItem as string; }
            get
            {
                string[] ck = ConnectionParams.ParseConnKey(cmbServers.SelectedItem as string);
                if (ck.Length >= 1)
                    return ck[0];
                else
                    return String.Empty;
            }
        }

        public IList<string> Servers
        {
            get
            {
                IList<string> result = new List<string>();
                foreach (string server in cmbServers.Items)
                {
                    //result.Add(server);
                    string[] ck = ConnectionParams.ParseConnKey(server);
                    if (ck.Length >= 1)
                        result.Add(ck[0]);
                }
                return result;
            }
        }

        public string Database
        {
            get { return cmbDatabases.SelectedItem as string; }
        }

        public IList<string> Databases
        {
            get
            {
                IList<string> result = new List<string>();
                foreach (string db in cmbDatabases.Items)
                {
                    result.Add(db);
                }
                return result;
            }
        }

        private IList<ScriptExecutionResult> _execResults = null;
        public IList<ScriptExecutionResult> ScriptExecutionResults
        {
            get { return _execResults; }
        }

        public DataView ActiveDataView
        {
            get
            {
                return _currentResultRendererFactory != null ? _currentResultRendererFactory.ActiveDataView : null;
            }
        }

        public ObjectInfo GetObjectInfo(string objName)
        {
            return ProgrammabilityHelper.GetObjectInfo(SqlConn, SqlConn.Database, objName);
        }

        public ObjectInfo GetObjectInfoForWordAtCursor()
        {
            string wordAtCursor = WordAtCursor;
            return GetObjectInfo(wordAtCursor);
        }
        public void PrepareAddInSupportForResultContextMenu(ToolStripItemCollection parent)
        {
            if (parent == null)
                return;

            int parentLastIdx = parent.Count - 1;
            int parentItemCnt = parent.Count;

            MenuService.AddItemsToMenu(parent, this, "/Workspace/ScriptEditor/ResultsContextMenu");

            if (parentItemCnt != parent.Count && parentLastIdx >= 0 && !(parent[parentLastIdx] is ToolStripSeparator))
                parent.Insert(parentLastIdx + 1, new ToolStripSeparator());
        }

        public IList<BatchInfo> Batches
        {
            get
            {
                return ScriptingHelper.SplitBatches(Content);
            }
        }

        public IList<string> ObjectNames
        {
            get
            {
                IList<BatchInfo> batches = ScriptingHelper.SplitBatches(Content);
                IList<string> result = new List<string>();
                int objType = -1;
                bool isAlter = false;

                foreach (BatchInfo batch in batches)
                {
                    string objName = ScriptingHelper.GetObjectNameFromScript(batch.Content, ref objType, ref isAlter);
                    if (!String.IsNullOrEmpty(objName))
                        result.Add(objName);
                }

                return result;
            }
        }


        private string _editorObjectName = String.Empty;
        public string ObjectName
        {
            get
            {
                return _editorObjectName;
            }
        }

        public string ConnectionString
        {
            get { return SqlConn.ConnectionString; }
        }

        public string Username
        {
            get { return SqlConnParams.CurrentUsername; }
        }

        public string IntegratedSecurity
        {
            get { return SqlConnParams.IntegratedSecurity; }
        }

        public ConnectionParams CurrentConnection
        {
            get
            {
                //ConnectionParams result = SqlConnParams.CreateCopy();
                //result.Password = String.Empty;
                //return result;

                ConnectionParams tmp = CurrentConnectionParams;
                if (tmp == null)
                    return null;

                ConnectionParams result = tmp.CreateCopy();
                result.Password = String.Empty;
                return result;

            }
        }


        private ScriptExecutedDelegate _scriptExecuted;
        public event ScriptExecutedDelegate ScriptExecuted
        {
            add { _scriptExecuted += value; }
            remove { _scriptExecuted -= value; }
        }

        private ScriptExecutingDelegate _scriptExecuting;
        public event ScriptExecutingDelegate ScriptExecuting
        {
            add { _scriptExecuting += value; }
            remove { _scriptExecuting -= value; }
        }



        private AfterHelpRequestedDelegate _afterHelpRequested;
        public event AfterHelpRequestedDelegate AfterHelpRequested
        {
            add { _afterHelpRequested += value; }
            remove { _afterHelpRequested -= value; }
        }

        private BeforeHelpRequestedDelegate _beforeHelpRequested;
        public event BeforeHelpRequestedDelegate BeforeHelpRequested
        {
            add { _beforeHelpRequested += value; }
            remove { _beforeHelpRequested -= value; }
        }


        private void FireAfterHelpRequested(string requestedFor)
        {
            if (_afterHelpRequested == null)
            {
                return;
            }

            Delegate[] delegates = _afterHelpRequested.GetInvocationList();
            foreach (AfterHelpRequestedDelegate del in delegates)
            {
                try
                {
                    HelpRequestedEventArgs args = new HelpRequestedEventArgs();
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

        private bool FireBeforeHelpRequested(string requestedFor)
        {
            bool result = true;
            if (_beforeHelpRequested == null)
            {
                return result;
            }

            bool canEval = true;
            Delegate[] delegates = _beforeHelpRequested.GetInvocationList();
            foreach (BeforeHelpRequestedDelegate del in delegates)
            {
                try
                {
                    HelpRequestedEventArgs args = new HelpRequestedEventArgs();
                    args.RequestedFor = requestedFor;
                    del.Invoke(this, args);

                    if (!args.Cancel && canEval)
                    {
                        canEval = false;
                        result = true;
                    }
                    else if (canEval)
                    {
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }

            return result;
        }

        private void FireScriptExecutedEvent(ScriptCompletionStatus status)
        {
            FireScriptExecutedEvent(_lastExecutedScriptData, status);
        }


        private void FireScriptExecutedEvent(ScriptData data, ScriptCompletionStatus status)
        {
            if (_scriptExecuted == null)
            {
                return;
            }

            Delegate[] delegates = _scriptExecuted.GetInvocationList();
            foreach (ScriptExecutedDelegate del in delegates)
            {
                try
                {
                    ScriptExecutedEventArgs args = new ScriptExecutedEventArgs();

                    args.Type = data.ScriptRunType;
                    args.Database = data.Database;
                    args.Server = data.Server;
                    args.Username = _currentConnUsername;
                    args.IsObjectHelpScript = data.IsObjectHelpScript;

                    args.Status = status;
                    List<SqlMessage> msgs = (List<SqlMessage>)_sqlMessages;
                    args.Messages = msgs.AsReadOnly();
                    del.Invoke(this, args);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }


        private bool FireScriptExecutingEvent(ScriptRunType scriptRunType, bool isObjectHelpScript)
        {
            ScriptEventArgs args = new ScriptEventArgs();
            args.Type = scriptRunType;
            args.Database = SqlConn.Database;
            args.Server = SqlConn.DataSource;
            args.IsObjectHelpScript = isObjectHelpScript;
            args.Username = _currentConnUsername;
            return FireScriptExecutingEvent(args);
        }

        private bool FireScriptExecutingEvent(ScriptEventArgs e)
        {
            if (_scriptExecuting == null)
            {
                return false;
            }

            bool cancel = false;
            Delegate[] delegates = _scriptExecuting.GetInvocationList();
            foreach (ScriptExecutingDelegate del in delegates)
            {
                try
                {
                    ScriptExecutingEventArgs args = new ScriptExecutingEventArgs();
                    if (e != EventArgs.Empty)
                        args.CopyFrom(e);
                    args.Cancel = false;
                    del.Invoke(this, args);
                    cancel = cancel || args.Cancel;
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }

            return cancel;
        }


        #endregion

        #region AddIn Support
        private void InitializeAddInSupport()
        {
            _addInToolStrip = ToolbarService.CreateToolStrip(this, "/Workspace/ScriptEditor/Toolbar");
            if (_addInToolStrip.Items.Count == 0)
            {
                _addInToolStrip.Visible = false;
            }
            else
            {

                this.Controls.Add(_addInToolStrip);
                _addInToolStrip.Visible = true;
                _addInToolStrip.RenderMode = ToolStripRenderMode.ManagerRenderMode;
                _addInToolStrip.GripStyle = ToolStripGripStyle.Hidden;
                _addInToolStrip.Dock = DockStyle.Top;
                _addInToolStrip.BringToFront();
                panEditor.BringToFront();
            }

            MenuService.AddItemsToMenu(popUpEditor.Items, this, "/Workspace/ScriptEditor/ContextMenu");
            MenuService.AddItemsToMenu(mainMenu.Items, this, "/Workspace/ScriptEditor/MainMenu");
            MenuService.AddItemsToMenu(popUpTab.Items, this, "/Workspace/ScriptEditor/ContentContextMenu");

            //MenuService.AddItemsToMenu(popUpGrid.Items, this, "/Workspace/ScriptEditor/ResultsContextMenu");
            MenuService.AddItemsToMenu(popUpMessages.Items, this, "/Workspace/ScriptEditor/MessagesContextMenu");
        }

        #endregion //AddIn Support

        #region IToolTipProvider Members
        public void UpdateToolTipText()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Script Editor: " + Caption);
                if (!String.IsNullOrEmpty(statLblContentInfo.Text))
                {
                    sb.AppendLine(statLblContentInfo.Text);
                }

                sb.AppendLine(String.Empty);
                string[] ck = ConnectionParams.ParseConnKey(cmbServers.Text);
                if (ck.Length >= 1)
                    sb.AppendLine("Server: " + ck[0]);

                sb.AppendLine("Database: " + cmbDatabases.Text);
                sb.AppendLine("Modified: " + ContentModified.ToString());
                sb.AppendLine("Last Modified On: " + _lastModifiedOn.ToString());
                sb.AppendLine(String.Empty);

                if (_isExecuting)
                {
                    sb.AppendLine("Executing for " + statLblQueryCompletionTime.Text);
                }
                sb.AppendLine("Preview:");
                sb.Append(ActiveDocument.TextContent.Length > 512 ? ActiveDocument.TextContent.Substring(0, 512) : ActiveDocument.TextContent);
                this.ToolTipText = sb.ToString();
            }
            catch (Exception ex)
            {
                HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, (MethodInfo)MethodInfo.GetCurrentMethod());
            }
        }
        #endregion

        #region Misc Utils

        public void InspectPragmaSQLDbConnection()
        {

#if PERSONAL_EDITION
      lblPragmaSQLDbConnectionInfo.Text = "Personal Edition does not support change log.";
      lblPragmaSQLDbConnectionInfo.Visible = true;
#else
            if (ConfigHelper.Current == null)
            {
                lblPragmaSQLDbConnectionInfo.Text = "PragmaSQL options can not be loaded!";
                lblPragmaSQLDbConnectionInfo.Visible = true;
                return;
            }
            else
            {
                if (!ConfigHelper.Current.CanLogObjectChanges())
                {
                    lblPragmaSQLDbConnectionInfo.Text = "Object changes will not be logged!";
                    lblPragmaSQLDbConnectionInfo.Visible = true;
                }
                else
                {
                    lblPragmaSQLDbConnectionInfo.Visible = false;
                }
            }
#endif
        }

        public override void ApplyTextEditorOptionsFromCurrentConfig()
        {
            base.ApplyTextEditorOptionsFromCurrentConfig();
            InspectPragmaSQLDbConnection();
            try
            {
                ActiveTextArea.BeginUpdate();
                if
                  (
                    ConfigHelper.Current == null
                    || ConfigHelper.Current.TextEditorOptions == null
                    || ActiveTextArea == null
                    || ActiveTextEditorProps == null
                  )
                {
                    return;
                }

                TextEditorOptions opts = ConfigHelper.Current.TextEditorOptions;
                _parseSelectsAndCases = opts.HighlightSelectAndCase || opts.FoldHighlightedSelectAndCase;
                _parseCodeBlocks = opts.FoldCodeBlocks;
                _parseComments = opts.HighlightComments || opts.FoldComments;

                _canAnalyzeSql = opts.AnalyzeSql;

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
                _highlightedCommentLines.Clear();
                _highlightedCommentLines.Clear();


                if (_parseSelectsAndCases || _parseCodeBlocks || _parseComments)
                {
                    RunParserThread();
                    _lastContentChangeTime = null;

                    if (_selectBlockColor == opts.SelectHighlightColor && _caseBlockColor == opts.CaseHighlightColor
                      && _commentBlockColor == opts.CommentHighlightColor
                    )
                    {
                        return;
                    }

                    _selectBlockColor = opts.SelectHighlightColor;
                    _caseBlockColor = opts.CaseHighlightColor;
                    _commentBlockColor = opts.CommentHighlightColor;

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
                    }
                }
            }
            finally
            {
                ActiveTextArea.EndUpdate();
                ActiveTextArea.Invalidate();
            }
        }

        public void UpdateWindowId()
        {
            try
            {
                int type = _objectType;
                bool isAlter = false;
                string objName = ScriptingHelper.GetObjectNameFromScript(ScriptText, ref type, ref isAlter);

                if (String.IsNullOrEmpty(objName) || type == DBObjectType.None)
                {
                    ScriptEditorManager.Forget(_windowId);
                    return;
                }


                ObjectInfo objInfo = ProgrammabilityHelper.GetObjectInfo(SqlConnParams, SqlConnParams.Database, objName);
                if (objInfo.ObjectID == -1)
                {
                    ScriptEditorManager.Forget(_windowId);
                    return;
                }

                string tmpWindowId = ScriptEditorManager.ProduceWindowId(Caption, objInfo.ObjectID, objInfo.ObjectType, SqlConnParams.Server, SqlConnParams.Database);
                ScriptEditorManager.Forget(_windowId);
                _windowId = tmpWindowId;

                if (ScriptEditorManager.Contains(tmpWindowId))
                {
                    _windowId = String.Empty;
                    return;
                }
                else
                {
                    ScriptEditorManager.Remember(_windowId, this);
                }
            }
            catch (Exception ex)
            {
                HostServicesSingleton.HostServices.MsgService.ErrorMsg("ScriptManager window id can not be updated! Error Detail:" + ex.Message, (MethodInfo)MethodInfo.GetCurrentMethod());
            }
        }

        private void CopySelectedMessagesToClipboard(bool copyWithHeaderInfo)
        {
            StringBuilder sb = new StringBuilder();
            string selLine = String.Empty;
            string tab = String.Empty;
            ColumnHeader header = null;
            ListViewItem item = null;

            if (copyWithHeaderInfo)
            {
                for (int i = 1; i < lv.Columns.Count; i++)
                {
                    header = lv.Columns[i];
                    selLine += tab + header.Text;
                    tab = ((Char)9).ToString();
                }
                sb.AppendLine(selLine);
            }

            for (int j = 0; j < lv.SelectedItems.Count; j++)
            {
                item = lv.SelectedItems[j];
                selLine = String.Empty;
                tab = String.Empty;

                for (int k = 1; k < item.SubItems.Count; k++)
                {
                    selLine += tab + item.SubItems[k].Text;
                    tab = ((Char)9).ToString();
                }
                sb.AppendLine(selLine);
            }

            Clipboard.SetText(sb.ToString(),TextDataFormat.UnicodeText);
        }

        private void ReclaimWindowNumber()
        {
            ScriptEditorFactory.Numerator.ReclaimNumber(WindowNo);
            WindowNo = null;
        }

        private void ReclaimWindowNumber_OnClose()
        {
            ReclaimWindowNumber();
            ScriptEditorFactory.Numerator.WindowCount--;
            ScriptEditorFactory.ResetNumerator();
        }

        public void SetFilePath(string filePath)
        {
            FileName = filePath;
            statLblContentInfo.Text = filePath;
        }

        private void ChangeConnection()
        {
            ConnectionParams cp = frmConnectionRepository.SelectSingleConnection(true);
            if (cp == null)
                return;

            SqlConnParams = cp.CreateCopy();
            AsyncInitializeConnection();
        }

        private void SaveAsSharedScript()
        {
            if (frmSharedScriptSelectDialog.SaveAsSharedScript(this, _caption))
            {
                ContentPersister.ContentType = EditorContentType.SharedScript;
                ReclaimWindowNumber();
            }
        }

        private void OpenSharedScript()
        {
            if (frmSharedScriptSelectDialog.OpenSharedScript(this, CurrentConnectionParams))
            {
                ContentPersister.ContentType = EditorContentType.SharedScript;
                ReclaimWindowNumber();
            }
        }

        #endregion //Misc Utils

        #region Workspace State Related
        private void PrepareWorkspaceState()
        {
            if (!Program.MainForm.ApplicationIsClosing || String.IsNullOrEmpty(this.Content))
                return;

            RecoverContent.Save(RecoverContent.WorkspaceFolder, RecoverContent.CreateScriptContent(this));
        }

        #endregion //Workspace State Related

        #region Sql Assistance
        /*
    private bool _saAttached = false;
    private IntPtr _saBuf;

    [DllImport("SqlAssistPrefs.dll")]
    public static extern int AttachWindow(IntPtr hwnd, IntPtr pConn, string lpzTargetName);

    [DllImport("SqlAssistPrefs.dll")]
    public static extern void DetachWindow(IntPtr hwnd);

    private void AttachToSqlAssist(bool attach)
    {
      if (!attach)
      {
        DetachWindow(this.ActiveTextArea.Handle);
        this._saAttached = false;
      }
      else
      {
        Marshal.GetNativeVariantForObject(this.SqlConn, this._saBuf);
        this._saAttached = AttachWindow(this.ActiveTextArea.Handle, this._saBuf, "PragmaSQL") != 0;
      }

      Utils.ShowInfo("Sql Assist Attach Status: " + _saAttached.ToString(), MessageBoxButtons.OK);
    }
    */
        #endregion //Sql Assistance


        private string prevSelectedDatabase = String.Empty;
        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing || _initializingServers)
            {
                return;
            }

            //if (_scriptPreviewForms.Count > 0 && Utils.AskYesNoQuestion(Properties.Resources.KeepScriptPreviewWindowsQuestion, MessageBoxDefaultButton.Button1) == DialogResult.No)
            if (_scriptPreviewForms.Count > 0)
            {
                CloseOwnedScriptPreviewForms();
            }


            if (SqlConn.State == ConnectionState.Open)
            {
                SqlConn.Close();
            }


            string prevConnStr = SqlConn.ConnectionString;
            ConnectionParams newCp = SqlConnParams.CreateCopy();
            newCp.Database = cmbDatabases.Text;
            try
            {
                SqlConn.ConnectionString = newCp.NonPooledConnectionString;
                SqlConn.Open();
                SqlConnParams = newCp.CreateCopy();
                prevSelectedDatabase = cmbDatabases.Text;
                UpdateWindowId();
            }
            catch
            {
                bool tryToReOpen = true;
                if (!String.IsNullOrEmpty(prevSelectedDatabase))
                {
                    Utils.ShowError(Properties.Resources.DbChangeFailRestoreNotification, MessageBoxButtons.OK);
                }
                else
                {
                    Utils.ShowError(Properties.Resources.DbChangeFailNotification, MessageBoxButtons.OK);
                    tryToReOpen = false;
                }

                _initializing = true;
                cmbDatabases.Text = prevSelectedDatabase;
                cmbDatabases.ToolTipText = prevSelectedDatabase;
                _initializing = false;

                SqlConn.ConnectionString = prevConnStr;

                if (tryToReOpen)
                    SqlConn.Open();
            }

            cmbDatabases.ToolTipText = cmbDatabases.Text;
        }

        private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing || _initializingServers)
                return;


            if (SqlConn.State != ConnectionState.Open)
                throw new InvalidConnectionState(SqlConn.State);

            string tmp = String.Empty;
            tmp = SqlConn.ConnectionString;

            ConnectionParams cp = _cpList[cmbServers.SelectedIndex];
            SqlConnParams = cp.CreateCopy();

            bool changeOk = false;
            try
            {
                if (SqlConn != null)
                {
                    if (SqlConn.State == ConnectionState.Open)
                        SqlConn.Close();
                    SqlConn.Dispose();
                }

                SqlConn = cp.CreateSqlConnection(true, false);
                SqlConn.StateChange += new StateChangeEventHandler(SqlConn_StateChange);

                _currentConnUsername = cp.CurrentUsername;

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
                MessageBox.Show("Can not connect to selected server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConn.ConnectionString = tmp;
                SqlConn.Open();

                _initializing = true;
                cmbServers.SelectedIndex = _currentServerIndex;
                _initializing = false;
            }

            cmbServers.ToolTipText = cmbServers.Text;

            if (changeOk && _codeCompWindow != null)
            {
                _codeCompWindow.InitializeCompletionProposal(SqlConn);
            }
        }

        private void lv_DoubleClick(object sender, EventArgs e)
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

        private void edtMatchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MatchNext(edtMatchText.Text);
            }
        }

        private void frmScriptEditor_Leave(object sender, EventArgs e)
        {
            if (_frmSearchAndReplace != null)
            {
                _frmSearchAndReplace.Close();
            }

            ShowHideOwnedScriptPreviewForms(true);
            //CloseOwnedScriptPreviewForms();
        }

        private void frmScriptEditor_Load(object sender, EventArgs e)
        {
            statLblQueryCompletionTime.Text = "Elapsed: 00:00:00:000";
            toolStripContainer1.Height = strip2.Height + strip3.Height;
        }

        private void timer1_Tick(object sender, EventArgs e)
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

        private void frmScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel)
                return;

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

            if (!e.Cancel)
            {
                //Marshal.FreeHGlobal(this._saBuf);
            }
        }

        private void popUpEditor_Opening(object sender, CancelEventArgs e)
        {
            if (_objHelperPopupBuilder == null)
            {
                return;
            }

            _objHelperPopupBuilder.BuildMenuItems(SqlConn, MergeType.Insert);
        }

        private void clearResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearResults();
        }

        private void clearMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearMessagesList();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearOutputPane();
        }

        private void openSharedScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSharedScript();

        }

        private void saveAsSharedScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsSharedScript();

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
#if ( PERSONAL_EDITION == false)
            Program.MainForm.ShowOptionsDialog("PragmaSQL System");
#endif
        }

        private void btnNewScript_Click(object sender, EventArgs e)
        {
            CreateNewScriptEditor();
        }

        private void OnDiffScriptAsSource_Click(object sender, EventArgs e)
        {

            SendSelectedTextToTextDiff(true);
        }

        private void OnDiffScriptAsDest_Click(object sender, EventArgs e)
        {
            SendSelectedTextToTextDiff(false);
        }

        private void frmScriptEditor_FormClosed(object sender, FormClosedEventArgs e)
        {

            tmParse.Enabled = false;
            DisposeIncSearchObjects();
            CloseOwnedScriptPreviewForms();
            ClearOutputPane();

            if (_tokenizer != null)
            {
                _tokenizer = null;
            }

            if (SqlConn != null)
            {
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
                SqlConn.Dispose();
            }

            if (_codeCompWindow != null)
                _codeCompWindow.CloseSelector();

            if (_searchForm != null)
                _searchForm.FormClosed -= new FormClosedEventHandler(_searchForm_FormClosed);

            if (_objChangeHist != null)
                _objChangeHist.FormClosed -= new FormClosedEventHandler(_objChangeHist_FormClosed);

            try
            {
                PrepareWorkspaceState();
            }
            catch { }

            ReclaimWindowNumber_OnClose();
            ScriptEditorManager.Forget(_windowId);
        }

        private void tmParse_Tick(object sender, EventArgs e)
        {
            if (!_lastContentChangeTime.HasValue)
                return;

            TimeSpan ts = DateTime.Now.Subtract(_lastContentChangeTime.Value);
            if (ts.TotalSeconds >= 1 && !_isParseInProgress)
            {
                RunParserThread();
                _lastContentChangeTime = null;
            }
        }

        private void findInDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDatabaseSearchForm();
        }

        private SerializableDictionary<string, ConnectionParams> _multiExecDbList;
        private string _multiExecTemplateName;
        private void btnEditMultiExecDbList_Click(object sender, EventArgs e)
        {
            //SerializableDictionary<string, ConnectionParams> tmp = frmMultiConnectionSpec.SelectConnections(ConnParams, _multiExecDbList);
            SerializableDictionary<string, ConnectionParams> tmp = frmMultiConnectionSpec2.SelectConnections(_multiExecDbList, ref _multiExecTemplateName);
            if (tmp != null)
            {
                _multiExecDbList = tmp;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedMessagesToClipboard(false);
        }

        private void copyWithHeadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedMessagesToClipboard(true);
        }

        private void frmScriptEditor_Enter(object sender, EventArgs e)
        {
            ShowHideOwnedScriptPreviewForms(false);
            CheckDefaultRenderer();
        }

        private void CheckDefaultRenderer()
        {
            ResultRendererSpec selItem = cmbResultRenderers.SelectedItem as ResultRendererSpec;
            btnDefaultRenderer.Checked = selItem != null && (String.Compare(selItem.FullName, Program.MainForm.DefaultResultRenderer) == 0);
        }


        private bool _viewSplitted = false;
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.TextEditor.Split();
            _viewSplitted = !_viewSplitted;
            toolStripButton2.Checked = _viewSplitted;
            toolStripButton2.Text = !_viewSplitted ? "Split View" : "Merge Views";
        }

        private void searchInWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MainForm.PerformWebSearch(SelectedTextOrWordAtCursor);
        }


        private void cmbResultRenderers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResultRendererSpec selItem = cmbResultRenderers.SelectedItem as ResultRendererSpec;
            cmbResultRenderers.ToolTipText = selItem != null ? selItem.Description : String.Empty;

            btnDefaultRenderer.Enabled = selItem != null;
            btnDefaultRenderer.Checked = selItem != null && (String.Compare(selItem.FullName, Program.MainForm.DefaultResultRenderer, true, CultureInfo.InvariantCulture) == 0);
        }

        private void btnDefaultRenderer_Click(object sender, EventArgs e)
        {
            ResultRendererSpec selItem = cmbResultRenderers.SelectedItem as ResultRendererSpec;
            if (selItem == null || String.Compare(selItem.FullName, Program.MainForm.DefaultResultRenderer) == 0)
                return;


            Program.MainForm.DefaultResultRenderer = selItem.FullName;
            btnDefaultRenderer.Checked = true;
        }

        private void _textEditor_DragOver(object sender, DragEventArgs e)
        {
            Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;

            if (sourceNodes == null || sourceNodes.Count == 0)
            {
                return;
            }

            TreeNode firstNode = null;
            foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
            {
                firstNode = nodeWrapper.Node;
                if (firstNode != null)
                {
                    break;
                }
            }

            if (firstNode == null)
            {
                return;
            }

            if (!(firstNode.Tag is NodeData))
            {
                return;
            }

            foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
            {
                NodeData sourceData = NodeDataFactory.GetNodeData(nodeWrapper.Node);
                if (sourceData == null)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
            }
            e.Effect = DragDropEffects.Copy;
        }

        private void _textEditor_DragDrop(object sender, DragEventArgs e)
        {
            Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;

            if (sourceNodes == null || sourceNodes.Count == 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            TreeNode firstNode = null;
            foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
            {
                firstNode = nodeWrapper.Node;
                if (firstNode != null)
                {
                    break;
                }
            }

            if (firstNode == null || !(firstNode.Tag is NodeData))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            StringBuilder sb = new StringBuilder();
            StringBuilder objectNames = new StringBuilder();

            if (firstNode.Tag is NodeData)
            {
                foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
                {
                    try
                    {
                        NodeData nodeData = NodeDataFactory.GetNodeData(nodeWrapper.Node);
                        if (nodeData == null)
                            continue;

                        objectNames.AppendLine(String.Format("{0}.{1}", nodeData.Owner, nodeData.Name));
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine("- " + ex.Message);
                    }
                }

                ActiveTextArea.Document.Insert(ActiveTextArea.Document.PositionToOffset(ActiveTextArea.Caret.Position), objectNames.ToString());
                ActiveTextArea.Invalidate();
            }

            if (sb.Length > 0)
            {
                GenericErrorDialog.ShowError("Drag/Drop Error", "Some objects can not be dropped to script editor.", sb.ToString());
            }
        }

        private void buttonSpecAny1_Click(object sender, EventArgs e)
        {
            OutputPaneVisible = false;
        }

        private void buttonSpecAny2_Click(object sender, EventArgs e)
        {
            _asyncConnCancelled = true;
            EnableControlsDuringAsyncConnection(true);
            ResetAsyncConnHeader();
        }

        private void buttonSpecAny3_Click(object sender, EventArgs e)
        {
            hdrAsyncConn.Visible = false;
        }

        private void mnuItemAddObjToGroup_Click(object sender, EventArgs e)
        {
            AddObjectToGroup();
        }

        private void mnuItemShowGroupStats_Click(object sender, EventArgs e)
        {
            ShowObjectGroupingStats();
        }

        private void parseSqlStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalyzeSQL();

        }

        private void btnChangeDb_Click(object sender, EventArgs e)
        {
            ChangeConnection();
        }

        private void btnReconnect_Click(object sender, EventArgs e)
        {
            if (SqlConn == null)
            {
                Utils.ShowError("No database connection selected.", MessageBoxButtons.OK);
                return;
            }

            if (SqlConn.State == ConnectionState.Open)
                SqlConn.Close();

            SqlConn.Open();
            Utils.ShowInfo("Reconnected to the selected database.", MessageBoxButtons.OK);
        }

        private ConnectionParams GetCurrentConnectionParams()
        {
            if (cmbServers.SelectedIndex < 0)
                return null;

            ConnectionParams tmp = _cpList[cmbServers.SelectedIndex].CreateCopy();
            if (tmp == null)
                return null;

            ConnectionParams result = tmp.CreateCopy();
            result.Database = cmbDatabases.SelectedItem as string;
            return result;

        }

        private string GetAlterScriptForWordAtCursor(out string objName)
        {
            var objInfo = GetObjectInfoForWordAtCursor();
            var cp = CurrentConnection;
            objName = objInfo == null ? String.Empty : String.Format("[{0}].[{1}].{2}",cp.Server, cp.Database,objInfo.FullNameQuoted);
            return objInfo != null && objInfo.HasScript ? ScriptingHelper.GetAlterScript(CurrentConnection, objInfo.ObjectID, objInfo.ObjectType) : String.Empty;
        }

        private void scriptObjectAsSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string objName = String.Empty;
            var script = GetAlterScriptForWordAtCursor(out objName);
            if (String.IsNullOrEmpty(script))
            {
                Utils.ShowError("Word at cursor does not match an object in the database or matched object is not scriptable", MessageBoxButtons.OKCancel);
                return;
            }
            SendSelectedTextToTextDiff(true,objName, script);
        }

        private void scriptObjectAsDestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string objName = String.Empty;

            var script = GetAlterScriptForWordAtCursor(out objName);
            if (String.IsNullOrEmpty(script))
            {
                Utils.ShowError("Word at cursor does not match an object in the database or matched object is not scriptable", MessageBoxButtons.OKCancel);
                return;
            }

            SendSelectedTextToTextDiff(false,objName, script);
        }

    }

    internal delegate void OnPublishSqlMessageInListViewDelegate(SqlMessage msg);
    internal delegate bool FireScriptExecutingDelegate(ScriptRunType scriptRunType, bool isObjectHelpScript);
    internal delegate void FireScriptExecutedDelegate(ScriptCompletionStatus status);
    internal delegate ConnectionParams GetCurrentConnectionParamsDelegate();

}