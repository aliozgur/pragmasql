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
      this.colMessageType = new System.Windows.Forms.ColumnHeader();
      this.colMessage = new System.Windows.Forms.ColumnHeader();
      this.colLine = new System.Windows.Forms.ColumnHeader();
      this.colType = new System.Windows.Forms.ColumnHeader();
      this.colState = new System.Windows.Forms.ColumnHeader();
      this.imgListMessages = new System.Windows.Forms.ImageList(this.components);
      this.panel3 = new System.Windows.Forms.Panel();
      this.lblOutputPaneHeader = new System.Windows.Forms.Label();
      this.lblCloseOutputPane = new System.Windows.Forms.Label();
      this.splitterOutput = new System.Windows.Forms.Splitter();
      this.panEditor = new System.Windows.Forms.Panel();
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
      this.textDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.destToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
      this.tsMnuItemMarkSelAsCodeBlock = new System.Windows.Forms.ToolStripMenuItem();
      this.foldingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tsMnuItemCollapseFoldings = new System.Windows.Forms.ToolStripMenuItem();
      this.tsMnuItemExpandFoldings = new System.Windows.Forms.ToolStripMenuItem();
      this.tsMnuItemToggleFoldings = new System.Windows.Forms.ToolStripMenuItem();
      this.parseSqlStatementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.statCaretPos = new System.Windows.Forms.ToolStripStatusLabel();
      this.statContentModified = new System.Windows.Forms.ToolStripStatusLabel();
      this.statContentName = new System.Windows.Forms.ToolStripStatusLabel();
      this.statLblQueryCompletionTime = new System.Windows.Forms.ToolStripStatusLabel();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.timerExec = new System.Windows.Forms.Timer(this.components);
      this.popUpGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.popUpItemCopyGridToClipboard = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
      this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
      this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveDlgExport = new System.Windows.Forms.SaveFileDialog();
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.strip2 = new System.Windows.Forms.ToolStrip();
      this.btnRun = new System.Windows.Forms.ToolStripButton();
      this.btnStop = new System.Windows.Forms.ToolStripButton();
      this.btnCheckSyntax = new System.Windows.Forms.ToolStripButton();
      this.btnShowPlan = new System.Windows.Forms.ToolStripButton();
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
      this.mnuItemOpenSelObject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemExecProc = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemListReferences = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemListPermissions = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemListDependencies = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemObjectHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.lblPragmaSQLDbConnectionInfo = new System.Windows.Forms.ToolStripLabel();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.asSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.asDestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.strip1 = new System.Windows.Forms.ToolStrip();
      this.btnNewScript = new System.Windows.Forms.ToolStripButton();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.btnSave = new System.Windows.Forms.ToolStripButton();
      this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
      this.mnuItemSharedScriptOperations = new System.Windows.Forms.ToolStripDropDownButton();
      this.openSharedScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsSharedScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cmbServers = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.cmbDatabases = new System.Windows.Forms.ToolStripComboBox();
      this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.panOutput.SuspendLayout();
      this.tabOutput.SuspendLayout();
      this.popUpOutputPane.SuspendLayout();
      this.tabMessages.SuspendLayout();
      this.panel3.SuspendLayout();
      this.popUpTab.SuspendLayout();
      this.mainMenu.SuspendLayout();
      this.popUpEditor.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.popUpGrid.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.strip2.SuspendLayout();
      this.strip3.SuspendLayout();
      this.strip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panOutput
      // 
      this.panOutput.Controls.Add(this.tabOutput);
      this.panOutput.Controls.Add(this.panel3);
      this.panOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panOutput.Location = new System.Drawing.Point(0, 262);
      this.panOutput.Name = "panOutput";
      this.panOutput.Size = new System.Drawing.Size(864, 250);
      this.panOutput.TabIndex = 1;
      // 
      // tabOutput
      // 
      this.tabOutput.ContextMenuStrip = this.popUpOutputPane;
      this.tabOutput.Controls.Add(this.tabMessages);
      this.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabOutput.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.tabOutput.Location = new System.Drawing.Point(0, 23);
      this.tabOutput.Name = "tabOutput";
      this.tabOutput.SelectedIndex = 0;
      this.tabOutput.ShowToolTips = true;
      this.tabOutput.Size = new System.Drawing.Size(864, 227);
      this.tabOutput.TabIndex = 0;
      // 
      // popUpOutputPane
      // 
      this.popUpOutputPane.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.toolStripMenuItem15,
            this.clearResultsToolStripMenuItem,
            this.clearMessagesToolStripMenuItem});
      this.popUpOutputPane.Name = "popUpOutputPane";
      this.popUpOutputPane.Size = new System.Drawing.Size(161, 76);
      // 
      // clearToolStripMenuItem
      // 
      this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
      this.clearToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
      this.clearToolStripMenuItem.Text = "Clear";
      this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
      // 
      // toolStripMenuItem15
      // 
      this.toolStripMenuItem15.Name = "toolStripMenuItem15";
      this.toolStripMenuItem15.Size = new System.Drawing.Size(157, 6);
      // 
      // clearResultsToolStripMenuItem
      // 
      this.clearResultsToolStripMenuItem.Name = "clearResultsToolStripMenuItem";
      this.clearResultsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
      this.clearResultsToolStripMenuItem.Text = "Clear Results";
      this.clearResultsToolStripMenuItem.Click += new System.EventHandler(this.clearResultsToolStripMenuItem_Click);
      // 
      // clearMessagesToolStripMenuItem
      // 
      this.clearMessagesToolStripMenuItem.Name = "clearMessagesToolStripMenuItem";
      this.clearMessagesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
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
      this.tabMessages.Size = new System.Drawing.Size(856, 198);
      this.tabMessages.TabIndex = 0;
      this.tabMessages.Text = "Messages";
      this.tabMessages.UseVisualStyleBackColor = true;
      // 
      // lv
      // 
      this.lv.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessageType,
            this.colMessage,
            this.colLine,
            this.colType,
            this.colState});
      this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lv.FullRowSelect = true;
      this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lv.HideSelection = false;
      this.lv.LabelWrap = false;
      this.lv.LargeImageList = this.imgListMessages;
      this.lv.Location = new System.Drawing.Point(3, 3);
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(846, 188);
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
      // imgListMessages
      // 
      this.imgListMessages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListMessages.ImageStream")));
      this.imgListMessages.TransparentColor = System.Drawing.Color.Magenta;
      this.imgListMessages.Images.SetKeyName(0, "");
      this.imgListMessages.Images.SetKeyName(1, "");
      this.imgListMessages.Images.SetKeyName(2, "");
      this.imgListMessages.Images.SetKeyName(3, "");
      this.imgListMessages.Images.SetKeyName(4, "");
      this.imgListMessages.Images.SetKeyName(5, "");
      this.imgListMessages.Images.SetKeyName(6, "");
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.panel3.Controls.Add(this.lblOutputPaneHeader);
      this.panel3.Controls.Add(this.lblCloseOutputPane);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(864, 23);
      this.panel3.TabIndex = 1;
      // 
      // lblOutputPaneHeader
      // 
      this.lblOutputPaneHeader.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblOutputPaneHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblOutputPaneHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lblOutputPaneHeader.Location = new System.Drawing.Point(0, 0);
      this.lblOutputPaneHeader.Name = "lblOutputPaneHeader";
      this.lblOutputPaneHeader.Size = new System.Drawing.Size(840, 23);
      this.lblOutputPaneHeader.TabIndex = 0;
      this.lblOutputPaneHeader.Text = "Messages And Results";
      this.lblOutputPaneHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblCloseOutputPane
      // 
      this.lblCloseOutputPane.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblCloseOutputPane.Dock = System.Windows.Forms.DockStyle.Right;
      this.lblCloseOutputPane.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblCloseOutputPane.ForeColor = System.Drawing.Color.White;
      this.lblCloseOutputPane.Location = new System.Drawing.Point(840, 0);
      this.lblCloseOutputPane.Name = "lblCloseOutputPane";
      this.lblCloseOutputPane.Size = new System.Drawing.Size(24, 23);
      this.lblCloseOutputPane.TabIndex = 1;
      this.lblCloseOutputPane.Text = "X";
      this.lblCloseOutputPane.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblCloseOutputPane.Click += new System.EventHandler(this.lblCloseoutputPane_Click);
      this.lblCloseOutputPane.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblCloseoutputPane_MouseClick);
      // 
      // splitterOutput
      // 
      this.splitterOutput.BackColor = System.Drawing.SystemColors.Control;
      this.splitterOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterOutput.Location = new System.Drawing.Point(0, 257);
      this.splitterOutput.Name = "splitterOutput";
      this.splitterOutput.Size = new System.Drawing.Size(864, 5);
      this.splitterOutput.TabIndex = 2;
      this.splitterOutput.TabStop = false;
      // 
      // panEditor
      // 
      this.panEditor.BackColor = System.Drawing.SystemColors.Control;
      this.panEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panEditor.Location = new System.Drawing.Point(0, 55);
      this.panEditor.Name = "panEditor";
      this.panEditor.Size = new System.Drawing.Size(864, 202);
      this.panEditor.TabIndex = 3;
      // 
      // popUpTab
      // 
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
      this.popUpTab.Size = new System.Drawing.Size(183, 148);
      // 
      // cMnuItemClose
      // 
      this.cMnuItemClose.Name = "cMnuItemClose";
      this.cMnuItemClose.Size = new System.Drawing.Size(182, 22);
      this.cMnuItemClose.Text = "Close";
      // 
      // cMnuCloseAll
      // 
      this.cMnuCloseAll.Name = "cMnuCloseAll";
      this.cMnuCloseAll.Size = new System.Drawing.Size(182, 22);
      this.cMnuCloseAll.Text = "Close All";
      // 
      // cMnuCloseAllButThis
      // 
      this.cMnuCloseAllButThis.Name = "cMnuCloseAllButThis";
      this.cMnuCloseAllButThis.Size = new System.Drawing.Size(182, 22);
      this.cMnuCloseAllButThis.Text = "Close All But This";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(179, 6);
      // 
      // cMnuItemSave
      // 
      this.cMnuItemSave.Image = global::PragmaSQL.Properties.Resources.save;
      this.cMnuItemSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cMnuItemSave.Name = "cMnuItemSave";
      this.cMnuItemSave.Size = new System.Drawing.Size(182, 22);
      this.cMnuItemSave.Text = "Save Script";
      // 
      // toolStripMenuItem11
      // 
      this.toolStripMenuItem11.Name = "toolStripMenuItem11";
      this.toolStripMenuItem11.Size = new System.Drawing.Size(179, 6);
      // 
      // cMnuNewScript
      // 
      this.cMnuNewScript.Image = global::PragmaSQL.Properties.Resources.new1;
      this.cMnuNewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cMnuNewScript.Name = "cMnuNewScript";
      this.cMnuNewScript.Size = new System.Drawing.Size(182, 22);
      this.cMnuNewScript.Text = "New Script";
      // 
      // cMnuScriptFromFile
      // 
      this.cMnuScriptFromFile.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cMnuScriptFromFile.Name = "cMnuScriptFromFile";
      this.cMnuScriptFromFile.Size = new System.Drawing.Size(182, 22);
      this.cMnuScriptFromFile.Text = "New Script From File";
      // 
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuQuery,
            this.helpToolStripMenuItem});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(876, 24);
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
      this.mnuFile.Size = new System.Drawing.Size(35, 20);
      this.mnuFile.Text = "File";
      // 
      // mnuItemOpen
      // 
      this.mnuItemOpen.Name = "mnuItemOpen";
      this.mnuItemOpen.Size = new System.Drawing.Size(192, 22);
      this.mnuItemOpen.Text = "Open";
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(189, 6);
      // 
      // mnuItemSave
      // 
      this.mnuItemSave.Name = "mnuItemSave";
      this.mnuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.mnuItemSave.Size = new System.Drawing.Size(192, 22);
      this.mnuItemSave.Text = "Save";
      // 
      // mnuItemSaveAs
      // 
      this.mnuItemSaveAs.Name = "mnuItemSaveAs";
      this.mnuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                  | System.Windows.Forms.Keys.S)));
      this.mnuItemSaveAs.Size = new System.Drawing.Size(192, 22);
      this.mnuItemSaveAs.Text = "Save As";
      this.mnuItemSaveAs.Click += new System.EventHandler(this.mnuItemSaveAs_Click);
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
      this.mnuEdit.Size = new System.Drawing.Size(37, 20);
      this.mnuEdit.Text = "Edit";
      // 
      // mnuItemUndo
      // 
      this.mnuItemUndo.Image = global::PragmaSQL.Properties.Resources.undo;
      this.mnuItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemUndo.Name = "mnuItemUndo";
      this.mnuItemUndo.Size = new System.Drawing.Size(224, 22);
      this.mnuItemUndo.Text = "Undo";
      // 
      // mnuItemRedo
      // 
      this.mnuItemRedo.Image = global::PragmaSQL.Properties.Resources.redo;
      this.mnuItemRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemRedo.Name = "mnuItemRedo";
      this.mnuItemRedo.Size = new System.Drawing.Size(224, 22);
      this.mnuItemRedo.Text = "Redo";
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new System.Drawing.Size(221, 6);
      // 
      // mnuItemCut
      // 
      this.mnuItemCut.Image = global::PragmaSQL.Properties.Resources.cut_2;
      this.mnuItemCut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemCut.Name = "mnuItemCut";
      this.mnuItemCut.Size = new System.Drawing.Size(224, 22);
      this.mnuItemCut.Text = "Cut";
      // 
      // mnuItemCopy
      // 
      this.mnuItemCopy.Image = global::PragmaSQL.Properties.Resources.copy;
      this.mnuItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemCopy.Name = "mnuItemCopy";
      this.mnuItemCopy.Size = new System.Drawing.Size(224, 22);
      this.mnuItemCopy.Text = "Copy";
      // 
      // mnuItemPaste
      // 
      this.mnuItemPaste.Image = global::PragmaSQL.Properties.Resources.paste;
      this.mnuItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemPaste.Name = "mnuItemPaste";
      this.mnuItemPaste.Size = new System.Drawing.Size(224, 22);
      this.mnuItemPaste.Text = "Paste";
      // 
      // toolStripMenuItem17
      // 
      this.toolStripMenuItem17.Name = "toolStripMenuItem17";
      this.toolStripMenuItem17.Size = new System.Drawing.Size(221, 6);
      // 
      // searchToolStripMenuItem
      // 
      this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemSearchForward,
            this.mnuItemSearchBackward,
            this.toolStripMenuItem7,
            this.mnuItemFind,
            this.mnuItemReplace});
      this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
      this.searchToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
      this.searchToolStripMenuItem.Text = "Find And Replace";
      // 
      // mnuItemSearchForward
      // 
      this.mnuItemSearchForward.Name = "mnuItemSearchForward";
      this.mnuItemSearchForward.Size = new System.Drawing.Size(167, 22);
      this.mnuItemSearchForward.Text = "Search Forward";
      // 
      // mnuItemSearchBackward
      // 
      this.mnuItemSearchBackward.Name = "mnuItemSearchBackward";
      this.mnuItemSearchBackward.Size = new System.Drawing.Size(167, 22);
      this.mnuItemSearchBackward.Text = "Serach Backward";
      // 
      // toolStripMenuItem7
      // 
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      this.toolStripMenuItem7.Size = new System.Drawing.Size(164, 6);
      // 
      // mnuItemFind
      // 
      this.mnuItemFind.Name = "mnuItemFind";
      this.mnuItemFind.Size = new System.Drawing.Size(167, 22);
      this.mnuItemFind.Text = "Find";
      // 
      // mnuItemReplace
      // 
      this.mnuItemReplace.Name = "mnuItemReplace";
      this.mnuItemReplace.Size = new System.Drawing.Size(167, 22);
      this.mnuItemReplace.Text = "Replace";
      // 
      // mnuItemGoToLine
      // 
      this.mnuItemGoToLine.Name = "mnuItemGoToLine";
      this.mnuItemGoToLine.Size = new System.Drawing.Size(224, 22);
      this.mnuItemGoToLine.Text = "Go To Line";
      // 
      // toolStripMenuItem8
      // 
      this.toolStripMenuItem8.Name = "toolStripMenuItem8";
      this.toolStripMenuItem8.Size = new System.Drawing.Size(221, 6);
      // 
      // mnuItemMarkSelAsCodeBlock
      // 
      this.mnuItemMarkSelAsCodeBlock.Name = "mnuItemMarkSelAsCodeBlock";
      this.mnuItemMarkSelAsCodeBlock.Size = new System.Drawing.Size(224, 22);
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
      this.formatToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
      this.formatToolStripMenuItem.Text = "Format";
      // 
      // mnuItemKeywordsToUppercase
      // 
      this.mnuItemKeywordsToUppercase.Name = "mnuItemKeywordsToUppercase";
      this.mnuItemKeywordsToUppercase.Size = new System.Drawing.Size(201, 22);
      this.mnuItemKeywordsToUppercase.Text = "Keywords To Uppercase";
      // 
      // mnuItemKeywordsToLowercase
      // 
      this.mnuItemKeywordsToLowercase.Name = "mnuItemKeywordsToLowercase";
      this.mnuItemKeywordsToLowercase.Size = new System.Drawing.Size(201, 22);
      this.mnuItemKeywordsToLowercase.Text = "Keywords To Lowercase";
      // 
      // mnuItemCapitilizeKeywords
      // 
      this.mnuItemCapitilizeKeywords.Name = "mnuItemCapitilizeKeywords";
      this.mnuItemCapitilizeKeywords.Size = new System.Drawing.Size(201, 22);
      this.mnuItemCapitilizeKeywords.Text = "Captialize Keywords";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
      // 
      // mnuItemScriptToUppercase
      // 
      this.mnuItemScriptToUppercase.Name = "mnuItemScriptToUppercase";
      this.mnuItemScriptToUppercase.Size = new System.Drawing.Size(201, 22);
      this.mnuItemScriptToUppercase.Text = "Script To Uppercase";
      // 
      // mnuItemScriptToLowercase
      // 
      this.mnuItemScriptToLowercase.Name = "mnuItemScriptToLowercase";
      this.mnuItemScriptToLowercase.Size = new System.Drawing.Size(201, 22);
      this.mnuItemScriptToLowercase.Text = "Script To Lowercase";
      // 
      // foldingsToolStripMenuItem
      // 
      this.foldingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemCollapseAllFoldings,
            this.mnuItemExpandAllFoldings,
            this.mnuItemToggleFoldings});
      this.foldingsToolStripMenuItem.Name = "foldingsToolStripMenuItem";
      this.foldingsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
      this.foldingsToolStripMenuItem.Text = "Foldings";
      // 
      // mnuItemCollapseAllFoldings
      // 
      this.mnuItemCollapseAllFoldings.Name = "mnuItemCollapseAllFoldings";
      this.mnuItemCollapseAllFoldings.Size = new System.Drawing.Size(139, 22);
      this.mnuItemCollapseAllFoldings.Text = "Collapse All";
      // 
      // mnuItemExpandAllFoldings
      // 
      this.mnuItemExpandAllFoldings.Name = "mnuItemExpandAllFoldings";
      this.mnuItemExpandAllFoldings.Size = new System.Drawing.Size(139, 22);
      this.mnuItemExpandAllFoldings.Text = "Expand All";
      // 
      // mnuItemToggleFoldings
      // 
      this.mnuItemToggleFoldings.Name = "mnuItemToggleFoldings";
      this.mnuItemToggleFoldings.Size = new System.Drawing.Size(139, 22);
      this.mnuItemToggleFoldings.Text = "Toggle";
      // 
      // mnuQuery
      // 
      this.mnuQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemRun,
            this.mnuItemStop,
            this.mnuItemCheckSyntax,
            this.mnuItemShowPlan,
            this.toolStripMenuItem3,
            this.mnuItemToggleOutputPane});
      this.mnuQuery.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.mnuQuery.MergeIndex = 3;
      this.mnuQuery.Name = "mnuQuery";
      this.mnuQuery.Size = new System.Drawing.Size(49, 20);
      this.mnuQuery.Text = "Query";
      // 
      // mnuItemRun
      // 
      this.mnuItemRun.Name = "mnuItemRun";
      this.mnuItemRun.Size = new System.Drawing.Size(181, 22);
      this.mnuItemRun.Text = "Run";
      // 
      // mnuItemStop
      // 
      this.mnuItemStop.Name = "mnuItemStop";
      this.mnuItemStop.Size = new System.Drawing.Size(181, 22);
      this.mnuItemStop.Text = "Stop ";
      // 
      // mnuItemCheckSyntax
      // 
      this.mnuItemCheckSyntax.Name = "mnuItemCheckSyntax";
      this.mnuItemCheckSyntax.Size = new System.Drawing.Size(181, 22);
      this.mnuItemCheckSyntax.Text = "Check Syntax";
      // 
      // mnuItemShowPlan
      // 
      this.mnuItemShowPlan.Name = "mnuItemShowPlan";
      this.mnuItemShowPlan.Size = new System.Drawing.Size(181, 22);
      this.mnuItemShowPlan.Text = "Show Plan";
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(178, 6);
      // 
      // mnuItemToggleOutputPane
      // 
      this.mnuItemToggleOutputPane.Name = "mnuItemToggleOutputPane";
      this.mnuItemToggleOutputPane.Size = new System.Drawing.Size(181, 22);
      this.mnuItemToggleOutputPane.Text = "Toggle Output Pane";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemHelpOnWordAtCursor});
      this.helpToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
      this.helpToolStripMenuItem.MergeIndex = 4;
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // mnuItemHelpOnWordAtCursor
      // 
      this.mnuItemHelpOnWordAtCursor.Name = "mnuItemHelpOnWordAtCursor";
      this.mnuItemHelpOnWordAtCursor.Size = new System.Drawing.Size(195, 22);
      this.mnuItemHelpOnWordAtCursor.Text = "Help On WordAtCursor";
      // 
      // popUpEditor
      // 
      this.popUpEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMnuItemUndo,
            this.tsMnuItemRedo,
            this.toolStripMenuItem9,
            this.tsMnuItemCut,
            this.tsMnuItemCopy,
            this.tsMnuItemPaste,
            this.textDiffToolStripMenuItem,
            this.toolStripMenuItem16,
            this.tsMnuItemMarkSelAsCodeBlock,
            this.foldingToolStripMenuItem,
            this.parseSqlStatementToolStripMenuItem});
      this.popUpEditor.Name = "contextMenuEditor";
      this.popUpEditor.Size = new System.Drawing.Size(225, 214);
      this.popUpEditor.Opening += new System.ComponentModel.CancelEventHandler(this.popUpEditor_Opening);
      // 
      // tsMnuItemUndo
      // 
      this.tsMnuItemUndo.Name = "tsMnuItemUndo";
      this.tsMnuItemUndo.Size = new System.Drawing.Size(224, 22);
      this.tsMnuItemUndo.Text = "Undo";
      // 
      // tsMnuItemRedo
      // 
      this.tsMnuItemRedo.Name = "tsMnuItemRedo";
      this.tsMnuItemRedo.Size = new System.Drawing.Size(224, 22);
      this.tsMnuItemRedo.Text = "Redo";
      // 
      // toolStripMenuItem9
      // 
      this.toolStripMenuItem9.Name = "toolStripMenuItem9";
      this.toolStripMenuItem9.Size = new System.Drawing.Size(221, 6);
      // 
      // tsMnuItemCut
      // 
      this.tsMnuItemCut.Image = global::PragmaSQL.Properties.Resources.cut_2;
      this.tsMnuItemCut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsMnuItemCut.Name = "tsMnuItemCut";
      this.tsMnuItemCut.Size = new System.Drawing.Size(224, 22);
      this.tsMnuItemCut.Text = "Cut";
      // 
      // tsMnuItemCopy
      // 
      this.tsMnuItemCopy.Image = global::PragmaSQL.Properties.Resources.copy;
      this.tsMnuItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsMnuItemCopy.Name = "tsMnuItemCopy";
      this.tsMnuItemCopy.ShortcutKeyDisplayString = "";
      this.tsMnuItemCopy.Size = new System.Drawing.Size(224, 22);
      this.tsMnuItemCopy.Text = "Copy";
      // 
      // tsMnuItemPaste
      // 
      this.tsMnuItemPaste.Image = global::PragmaSQL.Properties.Resources.paste;
      this.tsMnuItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsMnuItemPaste.Name = "tsMnuItemPaste";
      this.tsMnuItemPaste.ShortcutKeyDisplayString = "";
      this.tsMnuItemPaste.Size = new System.Drawing.Size(224, 22);
      this.tsMnuItemPaste.Text = "Paste";
      // 
      // textDiffToolStripMenuItem
      // 
      this.textDiffToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceToolStripMenuItem,
            this.destToolStripMenuItem});
      this.textDiffToolStripMenuItem.Name = "textDiffToolStripMenuItem";
      this.textDiffToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
      this.textDiffToolStripMenuItem.Text = "Diff";
      // 
      // sourceToolStripMenuItem
      // 
      this.sourceToolStripMenuItem.Name = "sourceToolStripMenuItem";
      this.sourceToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.sourceToolStripMenuItem.Text = "As Source";
      this.sourceToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsSource_Click);
      // 
      // destToolStripMenuItem
      // 
      this.destToolStripMenuItem.Name = "destToolStripMenuItem";
      this.destToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.destToolStripMenuItem.Text = "As Dest";
      this.destToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsDest_Click);
      // 
      // toolStripMenuItem16
      // 
      this.toolStripMenuItem16.Name = "toolStripMenuItem16";
      this.toolStripMenuItem16.Size = new System.Drawing.Size(221, 6);
      // 
      // tsMnuItemMarkSelAsCodeBlock
      // 
      this.tsMnuItemMarkSelAsCodeBlock.Name = "tsMnuItemMarkSelAsCodeBlock";
      this.tsMnuItemMarkSelAsCodeBlock.Size = new System.Drawing.Size(224, 22);
      this.tsMnuItemMarkSelAsCodeBlock.Text = "Mark Selection As Code Block";
      // 
      // foldingToolStripMenuItem
      // 
      this.foldingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMnuItemCollapseFoldings,
            this.tsMnuItemExpandFoldings,
            this.tsMnuItemToggleFoldings});
      this.foldingToolStripMenuItem.Name = "foldingToolStripMenuItem";
      this.foldingToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
      this.foldingToolStripMenuItem.Text = "Folding";
      // 
      // tsMnuItemCollapseFoldings
      // 
      this.tsMnuItemCollapseFoldings.Name = "tsMnuItemCollapseFoldings";
      this.tsMnuItemCollapseFoldings.Size = new System.Drawing.Size(139, 22);
      this.tsMnuItemCollapseFoldings.Text = "Collapse All";
      // 
      // tsMnuItemExpandFoldings
      // 
      this.tsMnuItemExpandFoldings.Name = "tsMnuItemExpandFoldings";
      this.tsMnuItemExpandFoldings.Size = new System.Drawing.Size(139, 22);
      this.tsMnuItemExpandFoldings.Text = "Expand All";
      // 
      // tsMnuItemToggleFoldings
      // 
      this.tsMnuItemToggleFoldings.Name = "tsMnuItemToggleFoldings";
      this.tsMnuItemToggleFoldings.Size = new System.Drawing.Size(139, 22);
      this.tsMnuItemToggleFoldings.Text = "Toggle";
      // 
      // parseSqlStatementToolStripMenuItem
      // 
      this.parseSqlStatementToolStripMenuItem.Name = "parseSqlStatementToolStripMenuItem";
      this.parseSqlStatementToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
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
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statCaretPos,
            this.statContentModified,
            this.statContentName,
            this.statLblQueryCompletionTime});
      this.statusStrip1.Location = new System.Drawing.Point(0, 512);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.ShowItemToolTips = true;
      this.statusStrip1.Size = new System.Drawing.Size(864, 25);
      this.statusStrip1.TabIndex = 6;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // statCaretPos
      // 
      this.statCaretPos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                  | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                  | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.statCaretPos.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
      this.statCaretPos.Name = "statCaretPos";
      this.statCaretPos.Size = new System.Drawing.Size(41, 20);
      this.statCaretPos.Text = "Caret ";
      this.statCaretPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // statContentModified
      // 
      this.statContentModified.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.statContentModified.Image = global::PragmaSQL.Properties.Resources.edit;
      this.statContentModified.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.statContentModified.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.statContentModified.Name = "statContentModified";
      this.statContentModified.Size = new System.Drawing.Size(16, 20);
      this.statContentModified.Text = "Modified";
      this.statContentModified.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.statContentModified.Visible = false;
      // 
      // statContentName
      // 
      this.statContentName.Name = "statContentName";
      this.statContentName.Size = new System.Drawing.Size(669, 20);
      this.statContentName.Spring = true;
      this.statContentName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // statLblQueryCompletionTime
      // 
      this.statLblQueryCompletionTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                  | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                  | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.statLblQueryCompletionTime.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
      this.statLblQueryCompletionTime.Image = global::PragmaSQL.Properties.Resources.clock2;
      this.statLblQueryCompletionTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.statLblQueryCompletionTime.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.statLblQueryCompletionTime.Name = "statLblQueryCompletionTime";
      this.statLblQueryCompletionTime.Size = new System.Drawing.Size(139, 20);
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
      // popUpGrid
      // 
      this.popUpGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popUpItemCopyGridToClipboard,
            this.toolStripMenuItem10,
            this.printPreviewToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripMenuItem12,
            this.exportToFileToolStripMenuItem});
      this.popUpGrid.Name = "popUpGrid";
      this.popUpGrid.Size = new System.Drawing.Size(153, 126);
      // 
      // popUpItemCopyGridToClipboard
      // 
      this.popUpItemCopyGridToClipboard.Name = "popUpItemCopyGridToClipboard";
      this.popUpItemCopyGridToClipboard.Size = new System.Drawing.Size(152, 22);
      this.popUpItemCopyGridToClipboard.Text = "Copy";
      // 
      // toolStripMenuItem10
      // 
      this.toolStripMenuItem10.Name = "toolStripMenuItem10";
      this.toolStripMenuItem10.Size = new System.Drawing.Size(149, 6);
      // 
      // printPreviewToolStripMenuItem
      // 
      this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
      this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.printPreviewToolStripMenuItem.Text = "Print Preview";
      this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.printToolStripMenuItem.Text = "Print";
      this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
      // 
      // toolStripMenuItem12
      // 
      this.toolStripMenuItem12.Name = "toolStripMenuItem12";
      this.toolStripMenuItem12.Size = new System.Drawing.Size(149, 6);
      // 
      // exportToFileToolStripMenuItem
      // 
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.exportToFileToolStripMenuItem.Text = "Export To File";
      this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
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
      this.toolStripContainer1.ContentPanel.Controls.Add(this.strip2);
      this.toolStripContainer1.ContentPanel.Controls.Add(this.strip3);
      this.toolStripContainer1.ContentPanel.Controls.Add(this.strip1);
      this.toolStripContainer1.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(864, 55);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(864, 55);
      this.toolStripContainer1.TabIndex = 9;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanelVisible = false;
      // 
      // strip2
      // 
      this.strip2.AllowItemReorder = true;
      this.strip2.Dock = System.Windows.Forms.DockStyle.None;
      this.strip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnStop,
            this.btnCheckSyntax,
            this.btnShowPlan});
      this.strip2.Location = new System.Drawing.Point(493, 0);
      this.strip2.Name = "strip2";
      this.strip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.strip2.Size = new System.Drawing.Size(104, 25);
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
      // strip3
      // 
      this.strip3.AllowItemReorder = true;
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
            this.toolStripDropDownButton1});
      this.strip3.Location = new System.Drawing.Point(0, 25);
      this.strip3.Name = "strip3";
      this.strip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.strip3.Size = new System.Drawing.Size(864, 25);
      this.strip3.TabIndex = 8;
      this.strip3.Text = "toolStrip2";
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(56, 22);
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
      this.btnToggleBlockComment.Image = ((System.Drawing.Image)(resources.GetObject("btnToggleBlockComment.Image")));
      this.btnToggleBlockComment.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnToggleBlockComment.Name = "btnToggleBlockComment";
      this.btnToggleBlockComment.Size = new System.Drawing.Size(23, 22);
      this.btnToggleBlockComment.Text = "Toggle Block Comment";
      this.btnToggleBlockComment.Click += new System.EventHandler(this.btnToggleBlockComment_Click);
      // 
      // btnToggleLineComment
      // 
      this.btnToggleLineComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnToggleLineComment.Image = ((System.Drawing.Image)(resources.GetObject("btnToggleLineComment.Image")));
      this.btnToggleLineComment.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnToggleLineComment.Name = "btnToggleLineComment";
      this.btnToggleLineComment.Size = new System.Drawing.Size(23, 22);
      this.btnToggleLineComment.Text = "Toggle Line Comment";
      this.btnToggleLineComment.Click += new System.EventHandler(this.btnToggleLineComment_Click);
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
            this.mnuItemOpenSelObject,
            this.mnuItemExecProc,
            this.toolStripMenuItem13,
            this.mnuItemListReferences,
            this.mnuItemListPermissions,
            this.mnuItemListDependencies,
            this.toolStripMenuItem14,
            this.mnuItemObjectHelp});
      this.mnuItemObjectHelpOperations.Image = global::PragmaSQL.Properties.Resources.appwindow_info_annotation;
      this.mnuItemObjectHelpOperations.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemObjectHelpOperations.Name = "mnuItemObjectHelpOperations";
      this.mnuItemObjectHelpOperations.Size = new System.Drawing.Size(29, 22);
      this.mnuItemObjectHelpOperations.Text = "toolStripDropDownButton1";
      // 
      // mnuItemModifySelObject
      // 
      this.mnuItemModifySelObject.Name = "mnuItemModifySelObject";
      this.mnuItemModifySelObject.Size = new System.Drawing.Size(152, 22);
      this.mnuItemModifySelObject.Text = "Modify";
      // 
      // mnuItemOpenSelObject
      // 
      this.mnuItemOpenSelObject.Name = "mnuItemOpenSelObject";
      this.mnuItemOpenSelObject.Size = new System.Drawing.Size(152, 22);
      this.mnuItemOpenSelObject.Text = "Open";
      // 
      // mnuItemExecProc
      // 
      this.mnuItemExecProc.Name = "mnuItemExecProc";
      this.mnuItemExecProc.Size = new System.Drawing.Size(152, 22);
      this.mnuItemExecProc.Text = "Execute";
      // 
      // toolStripMenuItem13
      // 
      this.toolStripMenuItem13.Name = "toolStripMenuItem13";
      this.toolStripMenuItem13.Size = new System.Drawing.Size(149, 6);
      // 
      // mnuItemListReferences
      // 
      this.mnuItemListReferences.Name = "mnuItemListReferences";
      this.mnuItemListReferences.Size = new System.Drawing.Size(152, 22);
      this.mnuItemListReferences.Text = "References";
      // 
      // mnuItemListPermissions
      // 
      this.mnuItemListPermissions.Name = "mnuItemListPermissions";
      this.mnuItemListPermissions.Size = new System.Drawing.Size(152, 22);
      this.mnuItemListPermissions.Text = "Permissions";
      // 
      // mnuItemListDependencies
      // 
      this.mnuItemListDependencies.Name = "mnuItemListDependencies";
      this.mnuItemListDependencies.Size = new System.Drawing.Size(152, 22);
      this.mnuItemListDependencies.Text = "Dependencies";
      // 
      // toolStripMenuItem14
      // 
      this.toolStripMenuItem14.Name = "toolStripMenuItem14";
      this.toolStripMenuItem14.Size = new System.Drawing.Size(149, 6);
      // 
      // mnuItemObjectHelp
      // 
      this.mnuItemObjectHelp.Name = "mnuItemObjectHelp";
      this.mnuItemObjectHelp.Size = new System.Drawing.Size(152, 22);
      this.mnuItemObjectHelp.Text = "Object Help";
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
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(53, 22);
      this.toolStripDropDownButton1.Text = "Diff";
      // 
      // asSourceToolStripMenuItem
      // 
      this.asSourceToolStripMenuItem.Name = "asSourceToolStripMenuItem";
      this.asSourceToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.asSourceToolStripMenuItem.Text = "As Source";
      this.asSourceToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsSource_Click);
      // 
      // asDestToolStripMenuItem
      // 
      this.asDestToolStripMenuItem.Name = "asDestToolStripMenuItem";
      this.asDestToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.asDestToolStripMenuItem.Text = "As Dest";
      this.asDestToolStripMenuItem.Click += new System.EventHandler(this.OnDiffScriptAsDest_Click);
      // 
      // strip1
      // 
      this.strip1.AllowItemReorder = true;
      this.strip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewScript,
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.mnuItemSharedScriptOperations,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbServers,
            this.toolStripLabel2,
            this.cmbDatabases});
      this.strip1.Location = new System.Drawing.Point(0, 0);
      this.strip1.Name = "strip1";
      this.strip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.strip1.Size = new System.Drawing.Size(864, 25);
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
      this.mnuItemSharedScriptOperations.Text = "Shared Script";
      // 
      // openSharedScriptToolStripMenuItem
      // 
      this.openSharedScriptToolStripMenuItem.Name = "openSharedScriptToolStripMenuItem";
      this.openSharedScriptToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
      this.openSharedScriptToolStripMenuItem.Text = "Open Shared Script";
      this.openSharedScriptToolStripMenuItem.Click += new System.EventHandler(this.openSharedScriptToolStripMenuItem_Click);
      // 
      // saveAsSharedScriptToolStripMenuItem
      // 
      this.saveAsSharedScriptToolStripMenuItem.Name = "saveAsSharedScriptToolStripMenuItem";
      this.saveAsSharedScriptToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
      this.saveAsSharedScriptToolStripMenuItem.Text = "Save As Shared Script";
      this.saveAsSharedScriptToolStripMenuItem.Click += new System.EventHandler(this.saveAsSharedScriptToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
      this.cmbServers.Size = new System.Drawing.Size(121, 25);
      this.cmbServers.SelectedIndexChanged += new System.EventHandler(this.cmbServers_SelectedIndexChanged);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(53, 22);
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
      // printDocument1
      // 
      this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
      // 
      // frmScriptEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(864, 537);
      this.Controls.Add(this.panEditor);
      this.Controls.Add(this.toolStripContainer1);
      this.Controls.Add(this.splitterOutput);
      this.Controls.Add(this.mainMenu);
      this.Controls.Add(this.panOutput);
      this.Controls.Add(this.statusStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mainMenu;
      this.Name = "frmScriptEditor";
      this.TabPageContextMenuStrip = this.popUpTab;
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScriptEditor_FormClosed);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScriptEditor_FormClosing);
      this.Leave += new System.EventHandler(this.frmScriptEditor_Leave);
      this.Load += new System.EventHandler(this.frmScriptEditor_Load);
      this.panOutput.ResumeLayout(false);
      this.tabOutput.ResumeLayout(false);
      this.popUpOutputPane.ResumeLayout(false);
      this.tabMessages.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.popUpTab.ResumeLayout(false);
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.popUpEditor.ResumeLayout(false);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.popUpGrid.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.strip2.ResumeLayout(false);
      this.strip2.PerformLayout();
      this.strip3.ResumeLayout(false);
      this.strip3.PerformLayout();
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
    private System.Windows.Forms.Panel panEditor;
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
    private System.Windows.Forms.ToolStripStatusLabel statContentName;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label lblOutputPaneHeader;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuItemToggleOutputPane;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuItemOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSave;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSaveAs;
    private System.Windows.Forms.Label lblCloseOutputPane;
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
    private System.Windows.Forms.ContextMenuStrip popUpGrid;
    private System.Windows.Forms.ToolStripMenuItem popUpItemCopyGridToClipboard;
    private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
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
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
    private System.Drawing.Printing.PrintDocument printDocument1;
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
    private System.Windows.Forms.ToolStripStatusLabel statContentModified;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemMarkSelAsCodeBlock;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem17;
    private System.Windows.Forms.ToolStripMenuItem mnuItemMarkSelAsCodeBlock;
    private System.Windows.Forms.ToolStripMenuItem textDiffToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sourceToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem destToolStripMenuItem;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem asSourceToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem asDestToolStripMenuItem;

  }
}