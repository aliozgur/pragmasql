namespace PragmaSQL
{
  partial class frmScriptEditor
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScriptEditor));
            this.panOutput = new System.Windows.Forms.Panel();
            this.tabOutput = new System.Windows.Forms.TabControl();
            this.popUpOutputPane = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.clearResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.lv = new System.Windows.Forms.ListView();
            this.colMessageType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colServer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.popUpMessages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyWithHeadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListMessages = new System.Windows.Forms.ImageList(this.components);
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.splitterOutput = new System.Windows.Forms.Splitter();
            this.popUpTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMnuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.cMnuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cMnuCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cMnuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.cMnuNewScript = new System.Windows.Forms.ToolStripMenuItem();
            this.cMnuScriptFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSearchForward = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSearchBackward = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemIncSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemRevIncSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemGoToLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemMarkSelAsCodeBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemKeywordsToUppercase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemKeywordsToLowercase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemCapitilizeKeywords = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemScriptToUppercase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemScriptToLowercase = new System.Windows.Forms.ToolStripMenuItem();
            this.foldingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemCollapseAllFoldings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemExpandAllFoldings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemToggleFoldings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemMultiRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemCheckSyntax = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemShowPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemToggleOutputPane = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemHelpOnWordAtCursor = new System.Windows.Forms.ToolStripMenuItem();
            this.popUpEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMnuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMnuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMnuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMnuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMnuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemAddObjToGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemShowGroupStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripSeparator();
            this.textDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptObjectAsSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.destToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptObjectAsDestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMnuItemMarkSelAsCodeBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.foldingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMnuItemCollapseFoldings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMnuItemExpandFoldings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMnuItemToggleFoldings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripSeparator();
            this.findInDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchInWebToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripSeparator();
            this.parseSqlStatementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statCaretPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.statIncSearch = new System.Windows.Forms.ToolStripStatusLabel();
            this.statContentModifiedIndicator = new System.Windows.Forms.ToolStripStatusLabel();
            this.statLblContentInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statLblQueryCompletionTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timerExec = new System.Windows.Forms.Timer(this.components);
            this.saveDlgExport = new System.Windows.Forms.SaveFileDialog();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.strip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.edtMatchText = new System.Windows.Forms.ToolStripTextBox();
            this.btnFindNext = new System.Windows.Forms.ToolStripButton();
            this.btnFindPrev = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOutDent = new System.Windows.Forms.ToolStripButton();
            this.btnIndent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnToggleBlockComment = new System.Windows.Forms.ToolStripButton();
            this.btnToggleLineComment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnKeywordsToUppercase = new System.Windows.Forms.ToolStripButton();
            this.btnKeywordsToLowercase = new System.Windows.Forms.ToolStripButton();
            this.btnCapitalizeKeywords = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemObjectHelpOperations = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuItemModifySelObject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSelectTop100SelObject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemOpenSelObject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemExecProc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemObjectChangeHist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemListReferences = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemListPermissions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemListDependencies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemObjectHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemFastScriptPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPragmaSQLDbConnectionInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.asSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asDestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.strip2 = new System.Windows.Forms.ToolStrip();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnCheckSyntax = new System.Windows.Forms.ToolStripButton();
            this.btnShowPlan = new System.Windows.Forms.ToolStripButton();
            this.btnMultiExec = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbResultRenderers = new System.Windows.Forms.ToolStripComboBox();
            this.btnDefaultRenderer = new System.Windows.Forms.ToolStripButton();
            this.strip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewScript = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.mnuItemSharedScriptOperations = new System.Windows.Forms.ToolStripDropDownButton();
            this.openSharedScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsSharedScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnChangeDb = new System.Windows.Forms.ToolStripButton();
            this.btnReconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbServers = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmbDatabases = new System.Windows.Forms.ToolStripComboBox();
            this.btnEditMultiExecDbList = new System.Windows.Forms.ToolStripButton();
            this.tmParse = new System.Windows.Forms.Timer(this.components);
            this.hdrAsyncConn = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.buttonSpecAny2 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.buttonSpecAny3 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.panEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).BeginInit();
            this.panOutput.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.popUpOutputPane.SuspendLayout();
            this.tabMessages.SuspendLayout();
            this.popUpMessages.SuspendLayout();
            this.popUpTab.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.popUpEditor.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.strip3.SuspendLayout();
            this.strip2.SuspendLayout();
            this.strip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _textEditor
            // 
            this._textEditor.Location = new System.Drawing.Point(0, 53);
            this._textEditor.Size = new System.Drawing.Size(1353, 348);
            // 
            // panEditor
            // 
            this.panEditor.Location = new System.Drawing.Point(0, 78);
            this.panEditor.Size = new System.Drawing.Size(1353, 401);
            // 
            // panOutput
            // 
            this.panOutput.Controls.Add(this.tabOutput);
            this.panOutput.Controls.Add(this.kryptonHeader1);
            this.panOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panOutput.Location = new System.Drawing.Point(0, 483);
            this.panOutput.Name = "panOutput";
            this.panOutput.Size = new System.Drawing.Size(1353, 217);
            this.panOutput.TabIndex = 1;
            // 
            // tabOutput
            // 
            this.tabOutput.ContextMenuStrip = this.popUpOutputPane;
            this.tabOutput.Controls.Add(this.tabMessages);
            this.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutput.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabOutput.Location = new System.Drawing.Point(0, 31);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.SelectedIndex = 0;
            this.tabOutput.ShowToolTips = true;
            this.tabOutput.Size = new System.Drawing.Size(1353, 186);
            this.tabOutput.TabIndex = 0;
            // 
            // popUpOutputPane
            // 
            this.popUpOutputPane.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.popUpOutputPane.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.toolStripMenuItem15,
            this.clearResultsToolStripMenuItem,
            this.clearMessagesToolStripMenuItem});
            this.popUpOutputPane.Name = "popUpOutputPane";
            this.popUpOutputPane.Size = new System.Drawing.Size(156, 76);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(152, 6);
            // 
            // clearResultsToolStripMenuItem
            // 
            this.clearResultsToolStripMenuItem.Name = "clearResultsToolStripMenuItem";
            this.clearResultsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.clearResultsToolStripMenuItem.Text = "Clear Results";
            this.clearResultsToolStripMenuItem.Click += new System.EventHandler(this.clearResultsToolStripMenuItem_Click);
            // 
            // clearMessagesToolStripMenuItem
            // 
            this.clearMessagesToolStripMenuItem.Name = "clearMessagesToolStripMenuItem";
            this.clearMessagesToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.clearMessagesToolStripMenuItem.Text = "Clear Messages";
            this.clearMessagesToolStripMenuItem.Click += new System.EventHandler(this.clearMessagesToolStripMenuItem_Click);
            // 
            // tabMessages
            // 
            this.tabMessages.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabMessages.Controls.Add(this.lv);
            this.tabMessages.Location = new System.Drawing.Point(4, 25);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabMessages.Size = new System.Drawing.Size(1345, 157);
            this.tabMessages.TabIndex = 0;
            this.tabMessages.Text = "Messages";
            this.tabMessages.ToolTipText = "Messages";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessageType,
            this.colMessage,
            this.colLine,
            this.colType,
            this.colState,
            this.colServer,
            this.colDb});
            this.lv.ContextMenuStrip = this.popUpMessages;
            this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv.FullRowSelect = true;
            this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv.HideSelection = false;
            this.lv.LabelWrap = false;
            this.lv.LargeImageList = this.imgListMessages;
            this.lv.Location = new System.Drawing.Point(3, 3);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(1335, 147);
            this.lv.SmallImageList = this.imgListMessages;
            this.lv.TabIndex = 1;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            // 
            // colMessageType
            // 
            this.colMessageType.Text = "";
            this.colMessageType.Width = 28;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 71;
            // 
            // colLine
            // 
            this.colLine.Text = "Line";
            // 
            // colType
            // 
            this.colType.Text = "Type";
            // 
            // colState
            // 
            this.colState.Text = "State";
            // 
            // colServer
            // 
            this.colServer.Text = "Server";
            this.colServer.Width = 118;
            // 
            // colDb
            // 
            this.colDb.Text = "Db";
            this.colDb.Width = 162;
            // 
            // popUpMessages
            // 
            this.popUpMessages.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.popUpMessages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copyWithHeadersToolStripMenuItem});
            this.popUpMessages.Name = "popUpMessages";
            this.popUpMessages.Size = new System.Drawing.Size(223, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyWithHeadersToolStripMenuItem
            // 
            this.copyWithHeadersToolStripMenuItem.Name = "copyWithHeadersToolStripMenuItem";
            this.copyWithHeadersToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.copyWithHeadersToolStripMenuItem.Text = "Copy With Column Headers";
            this.copyWithHeadersToolStripMenuItem.Click += new System.EventHandler(this.copyWithHeadersToolStripMenuItem_Click);
            // 
            // imgListMessages
            // 
            this.imgListMessages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListMessages.ImageStream")));
            this.imgListMessages.TransparentColor = System.Drawing.Color.Magenta;
            this.imgListMessages.Images.SetKeyName(0, "Info.png");
            this.imgListMessages.Images.SetKeyName(1, "Warning.png");
            this.imgListMessages.Images.SetKeyName(2, "Error.png");
            this.imgListMessages.Images.SetKeyName(3, "");
            this.imgListMessages.Images.SetKeyName(4, "");
            this.imgListMessages.Images.SetKeyName(5, "");
            this.imgListMessages.Images.SetKeyName(6, "");
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader1.Size = new System.Drawing.Size(1353, 31);
            this.kryptonHeader1.TabIndex = 3;
            this.kryptonHeader1.Text = "Messages And Results";
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "Messages And Results";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Inherit;
            this.buttonSpecAny1.ExtraText = "";
            this.buttonSpecAny1.Image = null;
            this.buttonSpecAny1.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
            this.buttonSpecAny1.Text = "Close";
            this.buttonSpecAny1.UniqueName = "CB3380104F674FAFCB3380104F674FAF";
            this.buttonSpecAny1.Click += new System.EventHandler(this.buttonSpecAny1_Click);
            // 
            // splitterOutput
            // 
            this.splitterOutput.BackColor = System.Drawing.SystemColors.Control;
            this.splitterOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterOutput.Location = new System.Drawing.Point(0, 479);
            this.splitterOutput.Name = "splitterOutput";
            this.splitterOutput.Size = new System.Drawing.Size(1353, 4);
            this.splitterOutput.TabIndex = 2;
            this.splitterOutput.TabStop = false;
            // 
            // popUpTab
            // 
            this.popUpTab.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.popUpTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMnuItemClose,
            this.cMnuCloseAll,
            this.cMnuCloseAllButThis,
            this.toolStripMenuItem1,
            this.cMnuItemSave,
            this.toolStripMenuItem11,
            this.cMnuNewScript,
            this.cMnuScriptFromFile});
            this.popUpTab.Name = "contextMenuTab";
            this.popUpTab.Size = new System.Drawing.Size(184, 148);
            // 
            // cMnuItemClose
            // 
            this.cMnuItemClose.Name = "cMnuItemClose";
            this.cMnuItemClose.Size = new System.Drawing.Size(183, 22);
            this.cMnuItemClose.Text = "Close";
            // 
            // cMnuCloseAll
            // 
            this.cMnuCloseAll.Name = "cMnuCloseAll";
            this.cMnuCloseAll.Size = new System.Drawing.Size(183, 22);
            this.cMnuCloseAll.Text = "Close All";
            // 
            // cMnuCloseAllButThis
            // 
            this.cMnuCloseAllButThis.Name = "cMnuCloseAllButThis";
            this.cMnuCloseAllButThis.Size = new System.Drawing.Size(183, 22);
            this.cMnuCloseAllButThis.Text = "Close All But This";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // cMnuItemSave
            // 
            this.cMnuItemSave.Image = global::PragmaSQL.Properties.Resources.save;
            this.cMnuItemSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cMnuItemSave.Name = "cMnuItemSave";
            this.cMnuItemSave.Size = new System.Drawing.Size(183, 22);
            this.cMnuItemSave.Text = "Save Script";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(180, 6);
            // 
            // cMnuNewScript
            // 
            this.cMnuNewScript.Image = global::PragmaSQL.Properties.Resources.new1;
            this.cMnuNewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cMnuNewScript.Name = "cMnuNewScript";
            this.cMnuNewScript.Size = new System.Drawing.Size(183, 22);
            this.cMnuNewScript.Text = "New Script";
            // 
            // cMnuScriptFromFile
            // 
            this.cMnuScriptFromFile.Image = global::PragmaSQL.Properties.Resources.folder_page_white;
            this.cMnuScriptFromFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cMnuScriptFromFile.Name = "cMnuScriptFromFile";
            this.cMnuScriptFromFile.Size = new System.Drawing.Size(183, 22);
            this.cMnuScriptFromFile.Text = "New Script From File";
            // 
            // mainMenu
            // 
            this.mainMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuQuery,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 30);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(947, 24);
            this.mainMenu.TabIndex = 4;
            this.mainMenu.Visible = false;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemOpen,
            this.toolStripMenuItem4,
            this.mnuItemSave,
            this.mnuItemSaveAs});
            this.mnuFile.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.mnuFile.MergeIndex = 0;
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuItemOpen
            // 
            this.mnuItemOpen.Name = "mnuItemOpen";
            this.mnuItemOpen.Size = new System.Drawing.Size(186, 22);
            this.mnuItemOpen.Text = "Open";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(183, 6);
            // 
            // mnuItemSave
            // 
            this.mnuItemSave.Name = "mnuItemSave";
            this.mnuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuItemSave.Size = new System.Drawing.Size(186, 22);
            this.mnuItemSave.Text = "Save";
            // 
            // mnuItemSaveAs
            // 
            this.mnuItemSaveAs.Name = "mnuItemSaveAs";
            this.mnuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuItemSaveAs.Size = new System.Drawing.Size(186, 22);
            this.mnuItemSaveAs.Text = "Save As";
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemUndo,
            this.mnuItemRedo,
            this.toolStripMenuItem5,
            this.mnuItemCut,
            this.mnuItemCopy,
            this.mnuItemPaste,
            this.toolStripMenuItem17,
            this.searchToolStripMenuItem,
            this.mnuItemGoToLine,
            this.toolStripMenuItem8,
            this.mnuItemMarkSelAsCodeBlock,
            this.formatToolStripMenuItem,
            this.foldingsToolStripMenuItem});
            this.mnuEdit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.mnuEdit.MergeIndex = 1;
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuItemUndo
            // 
            this.mnuItemUndo.Image = global::PragmaSQL.Properties.Resources.undo;
            this.mnuItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemUndo.Name = "mnuItemUndo";
            this.mnuItemUndo.Size = new System.Drawing.Size(231, 22);
            this.mnuItemUndo.Text = "Undo";
            // 
            // mnuItemRedo
            // 
            this.mnuItemRedo.Image = global::PragmaSQL.Properties.Resources.redo;
            this.mnuItemRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemRedo.Name = "mnuItemRedo";
            this.mnuItemRedo.Size = new System.Drawing.Size(231, 22);
            this.mnuItemRedo.Text = "Redo";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(228, 6);
            // 
            // mnuItemCut
            // 
            this.mnuItemCut.Image = global::PragmaSQL.Properties.Resources.cut_2;
            this.mnuItemCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemCut.Name = "mnuItemCut";
            this.mnuItemCut.Size = new System.Drawing.Size(231, 22);
            this.mnuItemCut.Text = "Cut";
            // 
            // mnuItemCopy
            // 
            this.mnuItemCopy.Image = global::PragmaSQL.Properties.Resources.copy;
            this.mnuItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemCopy.Name = "mnuItemCopy";
            this.mnuItemCopy.Size = new System.Drawing.Size(231, 22);
            this.mnuItemCopy.Text = "Copy";
            // 
            // mnuItemPaste
            // 
            this.mnuItemPaste.Image = global::PragmaSQL.Properties.Resources.paste;
            this.mnuItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemPaste.Name = "mnuItemPaste";
            this.mnuItemPaste.Size = new System.Drawing.Size(231, 22);
            this.mnuItemPaste.Text = "Paste";
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(228, 6);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemSearchForward,
            this.mnuItemSearchBackward,
            this.toolStripMenuItem7,
            this.mnuItemFind,
            this.mnuItemReplace,
            this.toolStripMenuItem18,
            this.mnuItemIncSearch,
            this.mnuItemRevIncSearch});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.searchToolStripMenuItem.Text = "Find And Replace";
            // 
            // mnuItemSearchForward
            // 
            this.mnuItemSearchForward.Name = "mnuItemSearchForward";
            this.mnuItemSearchForward.Size = new System.Drawing.Size(218, 22);
            this.mnuItemSearchForward.Text = "Search Forward";
            // 
            // mnuItemSearchBackward
            // 
            this.mnuItemSearchBackward.Name = "mnuItemSearchBackward";
            this.mnuItemSearchBackward.Size = new System.Drawing.Size(218, 22);
            this.mnuItemSearchBackward.Text = "Serach Backward";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(215, 6);
            // 
            // mnuItemFind
            // 
            this.mnuItemFind.Name = "mnuItemFind";
            this.mnuItemFind.Size = new System.Drawing.Size(218, 22);
            this.mnuItemFind.Text = "Find";
            // 
            // mnuItemReplace
            // 
            this.mnuItemReplace.Name = "mnuItemReplace";
            this.mnuItemReplace.Size = new System.Drawing.Size(218, 22);
            this.mnuItemReplace.Text = "Replace";
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(215, 6);
            // 
            // mnuItemIncSearch
            // 
            this.mnuItemIncSearch.Name = "mnuItemIncSearch";
            this.mnuItemIncSearch.Size = new System.Drawing.Size(218, 22);
            this.mnuItemIncSearch.Text = "Incremental Search";
            // 
            // mnuItemRevIncSearch
            // 
            this.mnuItemRevIncSearch.Name = "mnuItemRevIncSearch";
            this.mnuItemRevIncSearch.Size = new System.Drawing.Size(218, 22);
            this.mnuItemRevIncSearch.Text = "Reverse Incremental Search";
            // 
            // mnuItemGoToLine
            // 
            this.mnuItemGoToLine.Name = "mnuItemGoToLine";
            this.mnuItemGoToLine.Size = new System.Drawing.Size(231, 22);
            this.mnuItemGoToLine.Text = "Go To Line";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(228, 6);
            // 
            // mnuItemMarkSelAsCodeBlock
            // 
            this.mnuItemMarkSelAsCodeBlock.Name = "mnuItemMarkSelAsCodeBlock";
            this.mnuItemMarkSelAsCodeBlock.Size = new System.Drawing.Size(231, 22);
            this.mnuItemMarkSelAsCodeBlock.Text = "Mark Selection As Code Block";
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemKeywordsToUppercase,
            this.mnuItemKeywordsToLowercase,
            this.mnuItemCapitilizeKeywords,
            this.toolStripMenuItem2,
            this.mnuItemScriptToUppercase,
            this.mnuItemScriptToLowercase});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // mnuItemKeywordsToUppercase
            // 
            this.mnuItemKeywordsToUppercase.Name = "mnuItemKeywordsToUppercase";
            this.mnuItemKeywordsToUppercase.Size = new System.Drawing.Size(199, 22);
            this.mnuItemKeywordsToUppercase.Text = "Keywords To Uppercase";
            // 
            // mnuItemKeywordsToLowercase
            // 
            this.mnuItemKeywordsToLowercase.Name = "mnuItemKeywordsToLowercase";
            this.mnuItemKeywordsToLowercase.Size = new System.Drawing.Size(199, 22);
            this.mnuItemKeywordsToLowercase.Text = "Keywords To Lowercase";
            // 
            // mnuItemCapitilizeKeywords
            // 
            this.mnuItemCapitilizeKeywords.Name = "mnuItemCapitilizeKeywords";
            this.mnuItemCapitilizeKeywords.Size = new System.Drawing.Size(199, 22);
            this.mnuItemCapitilizeKeywords.Text = "Captialize Keywords";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // mnuItemScriptToUppercase
            // 
            this.mnuItemScriptToUppercase.Name = "mnuItemScriptToUppercase";
            this.mnuItemScriptToUppercase.Size = new System.Drawing.Size(199, 22);
            this.mnuItemScriptToUppercase.Text = "Script To Uppercase";
            // 
            // mnuItemScriptToLowercase
            // 
            this.mnuItemScriptToLowercase.Name = "mnuItemScriptToLowercase";
            this.mnuItemScriptToLowercase.Size = new System.Drawing.Size(199, 22);
            this.mnuItemScriptToLowercase.Text = "Script To Lowercase";
            // 
            // foldingsToolStripMenuItem
            // 
            this.foldingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemCollapseAllFoldings,
            this.mnuItemExpandAllFoldings,
            this.mnuItemToggleFoldings});
            this.foldingsToolStripMenuItem.Name = "foldingsToolStripMenuItem";
            this.foldingsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.foldingsToolStripMenuItem.Text = "Foldings";
            // 
            // mnuItemCollapseAllFoldings
            // 
            this.mnuItemCollapseAllFoldings.Name = "mnuItemCollapseAllFoldings";
            this.mnuItemCollapseAllFoldings.Size = new System.Drawing.Size(136, 22);
            this.mnuItemCollapseAllFoldings.Text = "Collapse All";
            // 
            // mnuItemExpandAllFoldings
            // 
            this.mnuItemExpandAllFoldings.Name = "mnuItemExpandAllFoldings";
            this.mnuItemExpandAllFoldings.Size = new System.Drawing.Size(136, 22);
            this.mnuItemExpandAllFoldings.Text = "Expand All";
            // 
            // mnuItemToggleFoldings
            // 
            this.mnuItemToggleFoldings.Name = "mnuItemToggleFoldings";
            this.mnuItemToggleFoldings.Size = new System.Drawing.Size(136, 22);
            this.mnuItemToggleFoldings.Text = "Toggle";
            // 
            // mnuQuery
            // 
            this.mnuQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemRun,
            this.mnuItemMultiRun,
            this.mnuItemStop,
            this.mnuItemCheckSyntax,
            this.mnuItemShowPlan,
            this.toolStripMenuItem3,
            this.mnuItemToggleOutputPane});
            this.mnuQuery.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuQuery.MergeIndex = 3;
            this.mnuQuery.Name = "mnuQuery";
            this.mnuQuery.Size = new System.Drawing.Size(51, 20);
            this.mnuQuery.Text = "Query";
            // 
            // mnuItemRun
            // 
            this.mnuItemRun.Name = "mnuItemRun";
            this.mnuItemRun.Size = new System.Drawing.Size(180, 22);
            this.mnuItemRun.Text = "Run";
            // 
            // mnuItemMultiRun
            // 
            this.mnuItemMultiRun.Name = "mnuItemMultiRun";
            this.mnuItemMultiRun.Size = new System.Drawing.Size(180, 22);
            this.mnuItemMultiRun.Text = "Multi Run";
            // 
            // mnuItemStop
            // 
            this.mnuItemStop.Name = "mnuItemStop";
            this.mnuItemStop.Size = new System.Drawing.Size(180, 22);
            this.mnuItemStop.Text = "Stop ";
            // 
            // mnuItemCheckSyntax
            // 
            this.mnuItemCheckSyntax.Name = "mnuItemCheckSyntax";
            this.mnuItemCheckSyntax.Size = new System.Drawing.Size(180, 22);
            this.mnuItemCheckSyntax.Text = "Check Syntax";
            // 
            // mnuItemShowPlan
            // 
            this.mnuItemShowPlan.Name = "mnuItemShowPlan";
            this.mnuItemShowPlan.Size = new System.Drawing.Size(180, 22);
            this.mnuItemShowPlan.Text = "Show Plan";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuItemToggleOutputPane
            // 
            this.mnuItemToggleOutputPane.Name = "mnuItemToggleOutputPane";
            this.mnuItemToggleOutputPane.Size = new System.Drawing.Size(180, 22);
            this.mnuItemToggleOutputPane.Text = "Toggle Output Pane";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemHelpOnWordAtCursor});
            this.helpToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.helpToolStripMenuItem.MergeIndex = 5;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // mnuItemHelpOnWordAtCursor
            // 
            this.mnuItemHelpOnWordAtCursor.Name = "mnuItemHelpOnWordAtCursor";
            this.mnuItemHelpOnWordAtCursor.Size = new System.Drawing.Size(197, 22);
            this.mnuItemHelpOnWordAtCursor.Text = "Help On WordAtCursor";
            // 
            // popUpEditor
            // 
            this.popUpEditor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.popUpEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMnuItemUndo,
            this.tsMnuItemRedo,
            this.toolStripMenuItem9,
            this.tsMnuItemCut,
            this.tsMnuItemCopy,
            this.tsMnuItemPaste,
            this.toolStripMenuItem6,
            this.mnuItemAddObjToGroup,
            this.mnuItemShowGroupStats,
            this.toolStripMenuItem20,
            this.textDiffToolStripMenuItem,
            this.toolStripMenuItem16,
            this.tsMnuItemMarkSelAsCodeBlock,
            this.foldingToolStripMenuItem,
            this.toolStripMenuItem19,
            this.findInDatabaseToolStripMenuItem,
            this.searchInWebToolStripMenuItem,
            this.toolStripMenuItem21,
            this.parseSqlStatementToolStripMenuItem});
            this.popUpEditor.Name = "contextMenuEditor";
            this.popUpEditor.Size = new System.Drawing.Size(234, 326);
            this.popUpEditor.Opening += new System.ComponentModel.CancelEventHandler(this.popUpEditor_Opening);
            // 
            // tsMnuItemUndo
            // 
            this.tsMnuItemUndo.Name = "tsMnuItemUndo";
            this.tsMnuItemUndo.Size = new System.Drawing.Size(233, 22);
            this.tsMnuItemUndo.Text = "Undo";
            // 
            // tsMnuItemRedo
            // 
            this.tsMnuItemRedo.Name = "tsMnuItemRedo";
            this.tsMnuItemRedo.Size = new System.Drawing.Size(233, 22);
            this.tsMnuItemRedo.Text = "Redo";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(230, 6);
            // 
            // tsMnuItemCut
            // 
            this.tsMnuItemCut.Image = global::PragmaSQL.Properties.Resources.cut_2;
            this.tsMnuItemCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMnuItemCut.Name = "tsMnuItemCut";
            this.tsMnuItemCut.Size = new System.Drawing.Size(233, 22);
            this.tsMnuItemCut.Text = "Cut";
            // 
            // tsMnuItemCopy
            // 
            this.tsMnuItemCopy.Image = global::PragmaSQL.Properties.Resources.copy;
            this.tsMnuItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMnuItemCopy.Name = "tsMnuItemCopy";
            this.tsMnuItemCopy.ShortcutKeyDisplayString = "";
            this.tsMnuItemCopy.Size = new System.Drawing.Size(233, 22);
            this.tsMnuItemCopy.Text = "Copy";
            // 
            // tsMnuItemPaste
            // 
            this.tsMnuItemPaste.Image = global::PragmaSQL.Properties.Resources.paste;
            this.tsMnuItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMnuItemPaste.Name = "tsMnuItemPaste";
            this.tsMnuItemPaste.ShortcutKeyDisplayString = "";
            this.tsMnuItemPaste.Size = new System.Drawing.Size(233, 22);
            this.tsMnuItemPaste.Text = "Paste";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(230, 6);
            // 
            // mnuItemAddObjToGroup
            // 
            this.mnuItemAddObjToGroup.Name = "mnuItemAddObjToGroup";
            this.mnuItemAddObjToGroup.Size = new System.Drawing.Size(233, 22);
            this.mnuItemAddObjToGroup.Text = "Add Object To Group";
            this.mnuItemAddObjToGroup.Click += new System.EventHandler(this.mnuItemAddObjToGroup_Click);
            // 
            // mnuItemShowGroupStats
            // 
            this.mnuItemShowGroupStats.Name = "mnuItemShowGroupStats";
            this.mnuItemShowGroupStats.Size = new System.Drawing.Size(233, 22);
            this.mnuItemShowGroupStats.Text = "Show Grouping Statistics";
            this.mnuItemShowGroupStats.Click += new System.EventHandler(this.mnuItemShowGroupStats_Click);
            // 
            // toolStripMenuItem20
            // 
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(230, 6);
            // 
            // textDiffToolStripMenuItem
            // 
            this.textDiffToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceToolStripMenuItem,
            this.scriptObjectAsSourceToolStripMenuItem,
            this.toolStripMenuItem10,
            this.destToolStripMenuItem,
            this.scriptObjectAsDestToolStripMenuItem});
            this.textDiffToolStripMenuItem.Name = "textDiffToolStripMenuItem";
            this.textDiffToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.textDiffToolStripMenuItem.Text = "Diff";
            // 
            // sourceToolStripMenuItem
            // 
            this.sourceToolStripMenuItem.Name = "sourceToolStripMenuItem";
            this.sourceToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.sourceToolStripMenuItem.Text = "As Source [Editor Content]";
            this.sourceToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsSource_Click);
            // 
            // scriptObjectAsSourceToolStripMenuItem
            // 
            this.scriptObjectAsSourceToolStripMenuItem.Name = "scriptObjectAsSourceToolStripMenuItem";
            this.scriptObjectAsSourceToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.scriptObjectAsSourceToolStripMenuItem.Text = "As Source [Object At Cursor]";
            this.scriptObjectAsSourceToolStripMenuItem.Click += new System.EventHandler(this.scriptObjectAsSourceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(222, 6);
            // 
            // destToolStripMenuItem
            // 
            this.destToolStripMenuItem.Name = "destToolStripMenuItem";
            this.destToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.destToolStripMenuItem.Text = "As Dest [Editor Content]";
            this.destToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsDest_Click);
            // 
            // scriptObjectAsDestToolStripMenuItem
            // 
            this.scriptObjectAsDestToolStripMenuItem.Name = "scriptObjectAsDestToolStripMenuItem";
            this.scriptObjectAsDestToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.scriptObjectAsDestToolStripMenuItem.Text = "As Dest [Object At Cursor]";
            this.scriptObjectAsDestToolStripMenuItem.Click += new System.EventHandler(this.scriptObjectAsDestToolStripMenuItem_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(230, 6);
            // 
            // tsMnuItemMarkSelAsCodeBlock
            // 
            this.tsMnuItemMarkSelAsCodeBlock.Name = "tsMnuItemMarkSelAsCodeBlock";
            this.tsMnuItemMarkSelAsCodeBlock.Size = new System.Drawing.Size(233, 22);
            this.tsMnuItemMarkSelAsCodeBlock.Text = "Mark Selection As Code Block";
            // 
            // foldingToolStripMenuItem
            // 
            this.foldingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMnuItemCollapseFoldings,
            this.tsMnuItemExpandFoldings,
            this.tsMnuItemToggleFoldings});
            this.foldingToolStripMenuItem.Name = "foldingToolStripMenuItem";
            this.foldingToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.foldingToolStripMenuItem.Text = "Folding";
            // 
            // tsMnuItemCollapseFoldings
            // 
            this.tsMnuItemCollapseFoldings.Name = "tsMnuItemCollapseFoldings";
            this.tsMnuItemCollapseFoldings.Size = new System.Drawing.Size(136, 22);
            this.tsMnuItemCollapseFoldings.Text = "Collapse All";
            // 
            // tsMnuItemExpandFoldings
            // 
            this.tsMnuItemExpandFoldings.Name = "tsMnuItemExpandFoldings";
            this.tsMnuItemExpandFoldings.Size = new System.Drawing.Size(136, 22);
            this.tsMnuItemExpandFoldings.Text = "Expand All";
            // 
            // tsMnuItemToggleFoldings
            // 
            this.tsMnuItemToggleFoldings.Name = "tsMnuItemToggleFoldings";
            this.tsMnuItemToggleFoldings.Size = new System.Drawing.Size(136, 22);
            this.tsMnuItemToggleFoldings.Text = "Toggle";
            // 
            // toolStripMenuItem19
            // 
            this.toolStripMenuItem19.Name = "toolStripMenuItem19";
            this.toolStripMenuItem19.Size = new System.Drawing.Size(230, 6);
            // 
            // findInDatabaseToolStripMenuItem
            // 
            this.findInDatabaseToolStripMenuItem.Name = "findInDatabaseToolStripMenuItem";
            this.findInDatabaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.findInDatabaseToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.findInDatabaseToolStripMenuItem.Text = "Find In Database";
            this.findInDatabaseToolStripMenuItem.Click += new System.EventHandler(this.findInDatabaseToolStripMenuItem_Click);
            // 
            // searchInWebToolStripMenuItem
            // 
            this.searchInWebToolStripMenuItem.Name = "searchInWebToolStripMenuItem";
            this.searchInWebToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.searchInWebToolStripMenuItem.Text = "Find In Web";
            this.searchInWebToolStripMenuItem.Click += new System.EventHandler(this.searchInWebToolStripMenuItem_Click);
            // 
            // toolStripMenuItem21
            // 
            this.toolStripMenuItem21.Name = "toolStripMenuItem21";
            this.toolStripMenuItem21.Size = new System.Drawing.Size(230, 6);
            // 
            // parseSqlStatementToolStripMenuItem
            // 
            this.parseSqlStatementToolStripMenuItem.Name = "parseSqlStatementToolStripMenuItem";
            this.parseSqlStatementToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.parseSqlStatementToolStripMenuItem.Text = "Analyze SQL";
            this.parseSqlStatementToolStripMenuItem.Click += new System.EventHandler(this.parseSqlStatementToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "sql";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "SQL Files|*.sql|Query Files|*.qry|All Files|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statCaretPos,
            this.statIncSearch,
            this.statContentModifiedIndicator,
            this.statLblContentInfo,
            this.statLblQueryCompletionTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 700);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1353, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statCaretPos
            // 
            this.statCaretPos.Name = "statCaretPos";
            this.statCaretPos.Size = new System.Drawing.Size(37, 17);
            this.statCaretPos.Text = "Caret ";
            this.statCaretPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statIncSearch
            // 
            this.statIncSearch.BackColor = System.Drawing.SystemColors.Highlight;
            this.statIncSearch.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.statIncSearch.Name = "statIncSearch";
            this.statIncSearch.Size = new System.Drawing.Size(0, 17);
            this.statIncSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statContentModifiedIndicator
            // 
            this.statContentModifiedIndicator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statContentModifiedIndicator.Image = global::PragmaSQL.Properties.Resources.edit;
            this.statContentModifiedIndicator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statContentModifiedIndicator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statContentModifiedIndicator.Name = "statContentModifiedIndicator";
            this.statContentModifiedIndicator.Size = new System.Drawing.Size(16, 17);
            this.statContentModifiedIndicator.Text = "Modified";
            this.statContentModifiedIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statContentModifiedIndicator.Visible = false;
            // 
            // statLblContentInfo
            // 
            this.statLblContentInfo.Name = "statLblContentInfo";
            this.statLblContentInfo.Size = new System.Drawing.Size(1166, 17);
            this.statLblContentInfo.Spring = true;
            this.statLblContentInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statLblQueryCompletionTime
            // 
            this.statLblQueryCompletionTime.Image = global::PragmaSQL.Properties.Resources.clock2;
            this.statLblQueryCompletionTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statLblQueryCompletionTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statLblQueryCompletionTime.Name = "statLblQueryCompletionTime";
            this.statLblQueryCompletionTime.Size = new System.Drawing.Size(135, 17);
            this.statLblQueryCompletionTime.Text = "Query ccompletion time";
            this.statLblQueryCompletionTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "sql";
            this.saveFileDialog1.Filter = "SQL Files|*.sql|Query Files|*.qry|All Files|*.*";
            // 
            // timerExec
            // 
            this.timerExec.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // saveDlgExport
            // 
            this.saveDlgExport.DefaultExt = "xls";
            this.saveDlgExport.Filter = "Excel Files|*.xls|CSV Files|*.csv|XML Files|*.xml|All Files|*.*";
            this.saveDlgExport.Title = "Export Data To File";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.strip3);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.strip2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.strip1);
            this.toolStripContainer1.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1353, 48);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 30);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1353, 48);
            this.toolStripContainer1.TabIndex = 9;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // strip3
            // 
            this.strip3.AllowItemReorder = true;
            this.strip3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.strip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.strip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.edtMatchText,
            this.btnFindNext,
            this.btnFindPrev,
            this.toolStripSeparator4,
            this.btnOutDent,
            this.btnIndent,
            this.toolStripSeparator5,
            this.btnToggleBlockComment,
            this.btnToggleLineComment,
            this.toolStripSeparator2,
            this.btnKeywordsToUppercase,
            this.btnKeywordsToLowercase,
            this.btnCapitalizeKeywords,
            this.toolStripSeparator3,
            this.mnuItemObjectHelpOperations,
            this.lblPragmaSQLDbConnectionInfo,
            this.toolStripDropDownButton1,
            this.toolStripButton2});
            this.strip3.Location = new System.Drawing.Point(0, 25);
            this.strip3.Name = "strip3";
            this.strip3.Size = new System.Drawing.Size(1353, 25);
            this.strip3.TabIndex = 8;
            this.strip3.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabel3.Text = "Quick Find";
            // 
            // edtMatchText
            // 
            this.edtMatchText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtMatchText.ForeColor = System.Drawing.Color.Navy;
            this.edtMatchText.HideSelection = false;
            this.edtMatchText.Name = "edtMatchText";
            this.edtMatchText.Size = new System.Drawing.Size(150, 25);
            this.edtMatchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edtMatchText_KeyDown);
            // 
            // btnFindNext
            // 
            this.btnFindNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFindNext.Image = global::PragmaSQL.Properties.Resources.down;
            this.btnFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(23, 22);
            // 
            // btnFindPrev
            // 
            this.btnFindPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFindPrev.Image = global::PragmaSQL.Properties.Resources.up;
            this.btnFindPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindPrev.Name = "btnFindPrev";
            this.btnFindPrev.Size = new System.Drawing.Size(23, 22);
            this.btnFindPrev.Text = "Find Prev";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOutDent
            // 
            this.btnOutDent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOutDent.Image = global::PragmaSQL.Properties.Resources.IndentRTL;
            this.btnOutDent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOutDent.Name = "btnOutDent";
            this.btnOutDent.Size = new System.Drawing.Size(23, 22);
            this.btnOutDent.Text = "Outdent Selection";
            // 
            // btnIndent
            // 
            this.btnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIndent.Image = global::PragmaSQL.Properties.Resources.Indent;
            this.btnIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIndent.Name = "btnIndent";
            this.btnIndent.Size = new System.Drawing.Size(23, 22);
            this.btnIndent.Text = "Indent Selection";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnToggleBlockComment
            // 
            this.btnToggleBlockComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToggleBlockComment.Image = global::PragmaSQL.Properties.Resources.CommentOut_Line;
            this.btnToggleBlockComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToggleBlockComment.Name = "btnToggleBlockComment";
            this.btnToggleBlockComment.Size = new System.Drawing.Size(23, 22);
            this.btnToggleBlockComment.Text = "Toggle Block Comment";
            // 
            // btnToggleLineComment
            // 
            this.btnToggleLineComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToggleLineComment.Image = ((System.Drawing.Image)(resources.GetObject("btnToggleLineComment.Image")));
            this.btnToggleLineComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToggleLineComment.Name = "btnToggleLineComment";
            this.btnToggleLineComment.Size = new System.Drawing.Size(23, 22);
            this.btnToggleLineComment.Text = "Toggle Line Comment";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnKeywordsToUppercase
            // 
            this.btnKeywordsToUppercase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnKeywordsToUppercase.Image = global::PragmaSQL.Properties.Resources.font_increase;
            this.btnKeywordsToUppercase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKeywordsToUppercase.Name = "btnKeywordsToUppercase";
            this.btnKeywordsToUppercase.Size = new System.Drawing.Size(23, 22);
            this.btnKeywordsToUppercase.Text = "Convert Keywords To Uppercase";
            // 
            // btnKeywordsToLowercase
            // 
            this.btnKeywordsToLowercase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnKeywordsToLowercase.Image = global::PragmaSQL.Properties.Resources.font_decrease;
            this.btnKeywordsToLowercase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnKeywordsToLowercase.Name = "btnKeywordsToLowercase";
            this.btnKeywordsToLowercase.Size = new System.Drawing.Size(23, 22);
            this.btnKeywordsToLowercase.Text = "Convert Keywords To Lowercase";
            // 
            // btnCapitalizeKeywords
            // 
            this.btnCapitalizeKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCapitalizeKeywords.Image = global::PragmaSQL.Properties.Resources.font_capitalize;
            this.btnCapitalizeKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCapitalizeKeywords.Name = "btnCapitalizeKeywords";
            this.btnCapitalizeKeywords.Size = new System.Drawing.Size(23, 22);
            this.btnCapitalizeKeywords.Text = "Captialize Keywords";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuItemObjectHelpOperations
            // 
            this.mnuItemObjectHelpOperations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuItemObjectHelpOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemModifySelObject,
            this.mnuItemSelectTop100SelObject,
            this.mnuItemOpenSelObject,
            this.mnuItemExecProc,
            this.mnuItemObjectChangeHist,
            this.toolStripMenuItem13,
            this.mnuItemListReferences,
            this.mnuItemListPermissions,
            this.mnuItemListDependencies,
            this.toolStripMenuItem14,
            this.mnuItemObjectHelp,
            this.mnuItemFastScriptPreview});
            this.mnuItemObjectHelpOperations.Image = global::PragmaSQL.Properties.Resources.appwindow_info_annotation;
            this.mnuItemObjectHelpOperations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemObjectHelpOperations.Name = "mnuItemObjectHelpOperations";
            this.mnuItemObjectHelpOperations.Size = new System.Drawing.Size(29, 22);
            this.mnuItemObjectHelpOperations.Text = "toolStripDropDownButton1";
            // 
            // mnuItemModifySelObject
            // 
            this.mnuItemModifySelObject.Name = "mnuItemModifySelObject";
            this.mnuItemModifySelObject.Size = new System.Drawing.Size(194, 22);
            this.mnuItemModifySelObject.Text = "Modify";
            // 
            // mnuItemSelectTop100SelObject
            // 
            this.mnuItemSelectTop100SelObject.Name = "mnuItemSelectTop100SelObject";
            this.mnuItemSelectTop100SelObject.Size = new System.Drawing.Size(194, 22);
            this.mnuItemSelectTop100SelObject.Text = "Select Top 100 Rows";
            // 
            // mnuItemOpenSelObject
            // 
            this.mnuItemOpenSelObject.Name = "mnuItemOpenSelObject";
            this.mnuItemOpenSelObject.Size = new System.Drawing.Size(194, 22);
            this.mnuItemOpenSelObject.Text = "Open";
            // 
            // mnuItemExecProc
            // 
            this.mnuItemExecProc.Name = "mnuItemExecProc";
            this.mnuItemExecProc.Size = new System.Drawing.Size(194, 22);
            this.mnuItemExecProc.Text = "Execute";
            // 
            // mnuItemObjectChangeHist
            // 
            this.mnuItemObjectChangeHist.Name = "mnuItemObjectChangeHist";
            this.mnuItemObjectChangeHist.Size = new System.Drawing.Size(194, 22);
            this.mnuItemObjectChangeHist.Text = "Object Change History";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuItemListReferences
            // 
            this.mnuItemListReferences.Name = "mnuItemListReferences";
            this.mnuItemListReferences.Size = new System.Drawing.Size(194, 22);
            this.mnuItemListReferences.Text = "References";
            // 
            // mnuItemListPermissions
            // 
            this.mnuItemListPermissions.Name = "mnuItemListPermissions";
            this.mnuItemListPermissions.Size = new System.Drawing.Size(194, 22);
            this.mnuItemListPermissions.Text = "Permissions";
            // 
            // mnuItemListDependencies
            // 
            this.mnuItemListDependencies.Name = "mnuItemListDependencies";
            this.mnuItemListDependencies.Size = new System.Drawing.Size(194, 22);
            this.mnuItemListDependencies.Text = "Dependencies";
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuItemObjectHelp
            // 
            this.mnuItemObjectHelp.Name = "mnuItemObjectHelp";
            this.mnuItemObjectHelp.Size = new System.Drawing.Size(194, 22);
            this.mnuItemObjectHelp.Text = "Object Help";
            // 
            // mnuItemFastScriptPreview
            // 
            this.mnuItemFastScriptPreview.Name = "mnuItemFastScriptPreview";
            this.mnuItemFastScriptPreview.Size = new System.Drawing.Size(194, 22);
            this.mnuItemFastScriptPreview.Text = "Fast Script Preview";
            // 
            // lblPragmaSQLDbConnectionInfo
            // 
            this.lblPragmaSQLDbConnectionInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblPragmaSQLDbConnectionInfo.AutoToolTip = true;
            this.lblPragmaSQLDbConnectionInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblPragmaSQLDbConnectionInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPragmaSQLDbConnectionInfo.IsLink = true;
            this.lblPragmaSQLDbConnectionInfo.LinkColor = System.Drawing.Color.Red;
            this.lblPragmaSQLDbConnectionInfo.Name = "lblPragmaSQLDbConnectionInfo";
            this.lblPragmaSQLDbConnectionInfo.Size = new System.Drawing.Size(230, 22);
            this.lblPragmaSQLDbConnectionInfo.Text = "Not connected to PragmaSQL database!";
            this.lblPragmaSQLDbConnectionInfo.ToolTipText = "Not connected to PragmaSQL database!";
            this.lblPragmaSQLDbConnectionInfo.Visible = false;
            this.lblPragmaSQLDbConnectionInfo.Click += new System.EventHandler(this.toolStripLabel4_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asSourceToolStripMenuItem,
            this.asDestToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(55, 22);
            this.toolStripDropDownButton1.Text = "Diff";
            // 
            // asSourceToolStripMenuItem
            // 
            this.asSourceToolStripMenuItem.Name = "asSourceToolStripMenuItem";
            this.asSourceToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.asSourceToolStripMenuItem.Text = "As Source";
            this.asSourceToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsSource_Click);
            // 
            // asDestToolStripMenuItem
            // 
            this.asDestToolStripMenuItem.Name = "asDestToolStripMenuItem";
            this.asDestToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.asDestToolStripMenuItem.Text = "As Dest";
            this.asDestToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsDest_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::PragmaSQL.Properties.Resources.application_split;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Split";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // strip2
            // 
            this.strip2.AllowItemReorder = true;
            this.strip2.Dock = System.Windows.Forms.DockStyle.None;
            this.strip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.strip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.strip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnStop,
            this.btnCheckSyntax,
            this.btnShowPlan,
            this.btnMultiExec,
            this.toolStripSeparator6,
            this.cmbResultRenderers,
            this.btnDefaultRenderer});
            this.strip2.Location = new System.Drawing.Point(576, 0);
            this.strip2.Name = "strip2";
            this.strip2.Size = new System.Drawing.Size(293, 25);
            this.strip2.TabIndex = 0;
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRun.Image = global::PragmaSQL.Properties.Resources.Run;
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(23, 22);
            this.btnRun.Text = "toolStripButton1";
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::PragmaSQL.Properties.Resources.Stop;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 22);
            this.btnStop.Text = "toolStripButton2";
            // 
            // btnCheckSyntax
            // 
            this.btnCheckSyntax.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckSyntax.Image = global::PragmaSQL.Properties.Resources.correct;
            this.btnCheckSyntax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckSyntax.Name = "btnCheckSyntax";
            this.btnCheckSyntax.Size = new System.Drawing.Size(23, 22);
            this.btnCheckSyntax.Text = "toolStripButton3";
            // 
            // btnShowPlan
            // 
            this.btnShowPlan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowPlan.Image = global::PragmaSQL.Properties.Resources.gear_1;
            this.btnShowPlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowPlan.Name = "btnShowPlan";
            this.btnShowPlan.Size = new System.Drawing.Size(23, 22);
            this.btnShowPlan.Text = "Show Plan";
            // 
            // btnMultiExec
            // 
            this.btnMultiExec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMultiExec.Image = global::PragmaSQL.Properties.Resources.db;
            this.btnMultiExec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMultiExec.Name = "btnMultiExec";
            this.btnMultiExec.Size = new System.Drawing.Size(23, 22);
            this.btnMultiExec.Text = "toolStripButton1";
            this.btnMultiExec.ToolTipText = "Execute In Multiple Databases";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator6.Visible = false;
            // 
            // cmbResultRenderers
            // 
            this.cmbResultRenderers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResultRenderers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbResultRenderers.Name = "cmbResultRenderers";
            this.cmbResultRenderers.Size = new System.Drawing.Size(150, 25);
            this.cmbResultRenderers.SelectedIndexChanged += new System.EventHandler(this.cmbResultRenderers_SelectedIndexChanged);
            // 
            // btnDefaultRenderer
            // 
            this.btnDefaultRenderer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDefaultRenderer.Image = global::PragmaSQL.Properties.Resources.asterisk_orange;
            this.btnDefaultRenderer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDefaultRenderer.Name = "btnDefaultRenderer";
            this.btnDefaultRenderer.Size = new System.Drawing.Size(23, 22);
            this.btnDefaultRenderer.Text = "Make Default Renderer";
            this.btnDefaultRenderer.Click += new System.EventHandler(this.btnDefaultRenderer_Click);
            // 
            // strip1
            // 
            this.strip1.AllowItemReorder = true;
            this.strip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.strip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.strip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewScript,
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.mnuItemSharedScriptOperations,
            this.toolStripSeparator1,
            this.btnChangeDb,
            this.btnReconnect,
            this.toolStripLabel1,
            this.cmbServers,
            this.toolStripLabel2,
            this.cmbDatabases,
            this.btnEditMultiExecDbList});
            this.strip1.Location = new System.Drawing.Point(0, 0);
            this.strip1.Name = "strip1";
            this.strip1.Size = new System.Drawing.Size(1353, 25);
            this.strip1.TabIndex = 6;
            this.strip1.Text = "toolStrip1";
            // 
            // btnNewScript
            // 
            this.btnNewScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewScript.Image = global::PragmaSQL.Properties.Resources.new_style_2;
            this.btnNewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewScript.Name = "btnNewScript";
            this.btnNewScript.Size = new System.Drawing.Size(23, 22);
            this.btnNewScript.Text = "New Script";
            this.btnNewScript.Click += new System.EventHandler(this.btnNewScript_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::PragmaSQL.Properties.Resources.open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Open";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::PragmaSQL.Properties.Resources.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = global::PragmaSQL.Properties.Resources.SaveAs;
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(23, 22);
            this.btnSaveAs.Text = "Save As";
            // 
            // mnuItemSharedScriptOperations
            // 
            this.mnuItemSharedScriptOperations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuItemSharedScriptOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSharedScriptToolStripMenuItem,
            this.saveAsSharedScriptToolStripMenuItem});
            this.mnuItemSharedScriptOperations.Image = global::PragmaSQL.Properties.Resources.generic;
            this.mnuItemSharedScriptOperations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuItemSharedScriptOperations.Name = "mnuItemSharedScriptOperations";
            this.mnuItemSharedScriptOperations.Size = new System.Drawing.Size(29, 22);
            this.mnuItemSharedScriptOperations.Text = "Shared Script Operation";
            // 
            // openSharedScriptToolStripMenuItem
            // 
            this.openSharedScriptToolStripMenuItem.Name = "openSharedScriptToolStripMenuItem";
            this.openSharedScriptToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openSharedScriptToolStripMenuItem.Text = "Open Shared Script";
            this.openSharedScriptToolStripMenuItem.Click += new System.EventHandler(this.openSharedScriptToolStripMenuItem_Click);
            // 
            // saveAsSharedScriptToolStripMenuItem
            // 
            this.saveAsSharedScriptToolStripMenuItem.Name = "saveAsSharedScriptToolStripMenuItem";
            this.saveAsSharedScriptToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsSharedScriptToolStripMenuItem.Text = "Save As Shared Script";
            this.saveAsSharedScriptToolStripMenuItem.Click += new System.EventHandler(this.saveAsSharedScriptToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnChangeDb
            // 
            this.btnChangeDb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChangeDb.Image = global::PragmaSQL.Properties.Resources.dbex;
            this.btnChangeDb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeDb.Name = "btnChangeDb";
            this.btnChangeDb.Size = new System.Drawing.Size(23, 22);
            this.btnChangeDb.Text = "Change Database";
            this.btnChangeDb.Click += new System.EventHandler(this.btnChangeDb_Click);
            // 
            // btnReconnect
            // 
            this.btnReconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReconnect.Image = global::PragmaSQL.Properties.Resources.DbRefresh;
            this.btnReconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReconnect.Name = "btnReconnect";
            this.btnReconnect.Size = new System.Drawing.Size(23, 22);
            this.btnReconnect.Text = "Reconnect";
            this.btnReconnect.Click += new System.EventHandler(this.btnReconnect_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Server";
            // 
            // cmbServers
            // 
            this.cmbServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServers.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbServers.ForeColor = System.Drawing.Color.Navy;
            this.cmbServers.Name = "cmbServers";
            this.cmbServers.Size = new System.Drawing.Size(150, 25);
            this.cmbServers.SelectedIndexChanged += new System.EventHandler(this.cmbServers_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel2.Text = "Database";
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabases.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbDatabases.ForeColor = System.Drawing.Color.Navy;
            this.cmbDatabases.MaxDropDownItems = 12;
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(121, 25);
            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
            // 
            // btnEditMultiExecDbList
            // 
            this.btnEditMultiExecDbList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditMultiExecDbList.Image = global::PragmaSQL.Properties.Resources.dbs;
            this.btnEditMultiExecDbList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditMultiExecDbList.Name = "btnEditMultiExecDbList";
            this.btnEditMultiExecDbList.Size = new System.Drawing.Size(23, 22);
            this.btnEditMultiExecDbList.Text = "Multi Exec";
            this.btnEditMultiExecDbList.ToolTipText = "Multi Execute Database List";
            this.btnEditMultiExecDbList.Click += new System.EventHandler(this.btnEditMultiExecDbList_Click);
            // 
            // tmParse
            // 
            this.tmParse.Enabled = true;
            this.tmParse.Interval = 500;
            this.tmParse.Tick += new System.EventHandler(this.tmParse_Tick);
            // 
            // hdrAsyncConn
            // 
            this.hdrAsyncConn.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny2,
            this.buttonSpecAny3});
            this.hdrAsyncConn.Dock = System.Windows.Forms.DockStyle.Top;
            this.hdrAsyncConn.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.hdrAsyncConn.Location = new System.Drawing.Point(0, 0);
            this.hdrAsyncConn.Name = "hdrAsyncConn";
            this.hdrAsyncConn.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.hdrAsyncConn.Size = new System.Drawing.Size(1353, 30);
            this.hdrAsyncConn.TabIndex = 11;
            this.hdrAsyncConn.Values.Description = "";
            this.hdrAsyncConn.Values.Heading = "";
            this.hdrAsyncConn.Values.Image = global::PragmaSQL.Properties.Resources.dbs;
            this.hdrAsyncConn.Visible = false;
            // 
            // buttonSpecAny2
            // 
            this.buttonSpecAny2.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Inherit;
            this.buttonSpecAny2.ExtraText = "";
            this.buttonSpecAny2.Image = null;
            this.buttonSpecAny2.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
            this.buttonSpecAny2.Text = "Cancel Connection";
            this.buttonSpecAny2.UniqueName = "5F7CD8528DE0407C5F7CD8528DE0407C";
            this.buttonSpecAny2.Click += new System.EventHandler(this.buttonSpecAny2_Click);
            // 
            // buttonSpecAny3
            // 
            this.buttonSpecAny3.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Inherit;
            this.buttonSpecAny3.ExtraText = "";
            this.buttonSpecAny3.Image = null;
            this.buttonSpecAny3.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
            this.buttonSpecAny3.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.buttonSpecAny3.Text = "X";
            this.buttonSpecAny3.UniqueName = "DD8C047C7C164E4EDD8C047C7C164E4E";
            this.buttonSpecAny3.Click += new System.EventHandler(this.buttonSpecAny3_Click);
            // 
            // frmScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 722);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.splitterOutput);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.panOutput);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.hdrAsyncConn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "frmScriptEditor";
            this.TabPageContextMenuStrip = this.popUpTab;
            this.TabText = "Script Editor";
            this.Text = "Script Editor";
            this.ToolTipText = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScriptEditor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScriptEditor_FormClosed);
            this.Load += new System.EventHandler(this.frmScriptEditor_Load);
            this.Enter += new System.EventHandler(this.frmScriptEditor_Enter);
            this.Leave += new System.EventHandler(this.frmScriptEditor_Leave);
            this.Controls.SetChildIndex(this.hdrAsyncConn, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.panOutput, 0);
            this.Controls.SetChildIndex(this.mainMenu, 0);
            this.Controls.SetChildIndex(this.splitterOutput, 0);
            this.Controls.SetChildIndex(this.toolStripContainer1, 0);
            this.Controls.SetChildIndex(this.panEditor, 0);
            this.panEditor.ResumeLayout(false);
            this.panEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).EndInit();
            this.panOutput.ResumeLayout(false);
            this.panOutput.PerformLayout();
            this.tabOutput.ResumeLayout(false);
            this.popUpOutputPane.ResumeLayout(false);
            this.tabMessages.ResumeLayout(false);
            this.popUpMessages.ResumeLayout(false);
            this.popUpTab.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.popUpEditor.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.strip3.ResumeLayout(false);
            this.strip3.PerformLayout();
            this.strip2.ResumeLayout(false);
            this.strip2.PerformLayout();
            this.strip1.ResumeLayout(false);
            this.strip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panOutput;
    private System.Windows.Forms.TabControl tabOutput;
    private System.Windows.Forms.TabPage tabMessages;
    private System.Windows.Forms.Splitter splitterOutput;
    private System.Windows.Forms.ContextMenuStrip popUpTab;
    private System.Windows.Forms.ToolStripMenuItem cMnuItemSave;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem cMnuItemClose;
    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.ToolStripMenuItem mnuQuery;
    private System.Windows.Forms.ToolStripMenuItem mnuItemRun;
    private System.Windows.Forms.ToolStripMenuItem mnuItemStop;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCheckSyntax;
    private System.Windows.Forms.ContextMenuStrip popUpEditor;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemCopy;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemCut;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemPaste;
    private System.Windows.Forms.ToolStripMenuItem cMnuCloseAll;
    private System.Windows.Forms.ToolStripMenuItem cMnuCloseAllButThis;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel statLblContentInfo;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuItemToggleOutputPane;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuItemOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSave;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSaveAs;
        private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader colMessageType;
    private System.Windows.Forms.ColumnHeader colMessage;
    private System.Windows.Forms.ColumnHeader colLine;
    private System.Windows.Forms.ColumnHeader colType;
    private System.Windows.Forms.ColumnHeader colState;
    private System.Windows.Forms.ImageList imgListMessages;
    private System.Windows.Forms.ToolStripMenuItem mnuEdit;
    private System.Windows.Forms.ToolStripMenuItem mnuItemRedo;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCut;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCopy;
    private System.Windows.Forms.ToolStripMenuItem mnuItemPaste;
    private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSearchForward;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSearchBackward;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
    private System.Windows.Forms.ToolStripMenuItem mnuItemFind;
    private System.Windows.Forms.ToolStripMenuItem mnuItemReplace;
    private System.Windows.Forms.ToolStripMenuItem mnuItemUndo;
    private System.Windows.Forms.ToolStripStatusLabel statLblQueryCompletionTime;
        private System.Windows.Forms.Timer timerExec;
    private System.Windows.Forms.SaveFileDialog saveDlgExport;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip strip2;
    private System.Windows.Forms.ToolStripButton btnRun;
    private System.Windows.Forms.ToolStripButton btnStop;
    private System.Windows.Forms.ToolStripButton btnCheckSyntax;
    private System.Windows.Forms.ToolStrip strip3;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripTextBox edtMatchText;
    private System.Windows.Forms.ToolStripButton btnFindNext;
    private System.Windows.Forms.ToolStripButton btnFindPrev;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton btnOutDent;
    private System.Windows.Forms.ToolStripButton btnIndent;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripButton btnToggleBlockComment;
    private System.Windows.Forms.ToolStripButton btnToggleLineComment;
    private System.Windows.Forms.ToolStrip strip1;
    private System.Windows.Forms.ToolStripButton btnOpen;
    private System.Windows.Forms.ToolStripButton btnSave;
    private System.Windows.Forms.ToolStripButton btnSaveAs;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripComboBox cmbServers;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripComboBox cmbDatabases;
    private System.Windows.Forms.ToolStripMenuItem mnuItemGoToLine;
    private System.Windows.Forms.ToolStripButton btnKeywordsToUppercase;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnKeywordsToLowercase;
    private System.Windows.Forms.ToolStripButton btnCapitalizeKeywords;
    private System.Windows.Forms.ToolStripMenuItem cMnuNewScript;
    private System.Windows.Forms.ToolStripMenuItem cMnuScriptFromFile;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
    private System.Windows.Forms.ToolStripButton btnShowPlan;
    private System.Windows.Forms.ToolStripMenuItem mnuItemShowPlan;
    private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemKeywordsToUppercase;
    private System.Windows.Forms.ToolStripMenuItem mnuItemKeywordsToLowercase;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCapitilizeKeywords;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem mnuItemScriptToUppercase;
    private System.Windows.Forms.ToolStripMenuItem mnuItemScriptToLowercase;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemUndo;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
    private System.Windows.Forms.ToolStripDropDownButton mnuItemObjectHelpOperations;
    private System.Windows.Forms.ToolStripMenuItem mnuItemModifySelObject;
    private System.Windows.Forms.ToolStripMenuItem mnuItemOpenSelObject;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
    private System.Windows.Forms.ToolStripMenuItem mnuItemListReferences;
    private System.Windows.Forms.ToolStripMenuItem mnuItemListPermissions;
    private System.Windows.Forms.ToolStripMenuItem mnuItemObjectHelp;
    private System.Windows.Forms.ToolStripMenuItem mnuItemListDependencies;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
    private System.Windows.Forms.ToolStripMenuItem mnuItemExecProc;
    private System.Windows.Forms.ContextMenuStrip popUpOutputPane;
    private System.Windows.Forms.ToolStripMenuItem clearResultsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearMessagesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
    private System.Windows.Forms.ToolStripDropDownButton mnuItemSharedScriptOperations;
    private System.Windows.Forms.ToolStripMenuItem openSharedScriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsSharedScriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem16;
    private System.Windows.Forms.ToolStripMenuItem parseSqlStatementToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemHelpOnWordAtCursor;
    private System.Windows.Forms.ToolStripStatusLabel statCaretPos;
    private System.Windows.Forms.ToolStripMenuItem foldingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCollapseAllFoldings;
    private System.Windows.Forms.ToolStripMenuItem mnuItemExpandAllFoldings;
    private System.Windows.Forms.ToolStripMenuItem mnuItemToggleFoldings;
    private System.Windows.Forms.ToolStripMenuItem foldingToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemCollapseFoldings;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemExpandFoldings;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemToggleFoldings;
    private System.Windows.Forms.ToolStripLabel lblPragmaSQLDbConnectionInfo;
    private System.Windows.Forms.ToolStripButton btnNewScript;
    private System.Windows.Forms.ToolStripStatusLabel statContentModifiedIndicator;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemMarkSelAsCodeBlock;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem17;
    private System.Windows.Forms.ToolStripMenuItem mnuItemMarkSelAsCodeBlock;
    private System.Windows.Forms.ToolStripMenuItem textDiffToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sourceToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem destToolStripMenuItem;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem asSourceToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem asDestToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip popUpMessages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripComboBox cmbResultRenderers;
    private System.Windows.Forms.Timer tmParse;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    private System.Windows.Forms.ToolStripMenuItem findInDatabaseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemObjectChangeHist;
        private System.Windows.Forms.ColumnHeader colServer;
        private System.Windows.Forms.ColumnHeader colDb;
        private System.Windows.Forms.ToolStripButton btnEditMultiExecDbList;
        private System.Windows.Forms.ToolStripButton btnMultiExec;
        private System.Windows.Forms.ToolStripMenuItem mnuItemMultiRun;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyWithHeadersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem mnuItemIncSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuItemRevIncSearch;
        private System.Windows.Forms.ToolStripStatusLabel statIncSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAddObjToGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuItemShowGroupStats;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem20;
        private System.Windows.Forms.ToolStripMenuItem mnuItemFastScriptPreview;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem searchInWebToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem19;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem21;
    private System.Windows.Forms.ToolStripButton btnDefaultRenderer;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
    private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader hdrAsyncConn;
    private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny2;
    private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny3;
    private System.Windows.Forms.ToolStripButton btnChangeDb;
    private System.Windows.Forms.ToolStripButton btnReconnect;
    private System.Windows.Forms.ToolStripMenuItem scriptObjectAsSourceToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
    private System.Windows.Forms.ToolStripMenuItem scriptObjectAsDestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItemSelectTop100SelObject;
    }
}