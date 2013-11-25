namespace PragmaSQL.GUI
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
      this.tabMessages = new System.Windows.Forms.TabPage();
      this.lv = new System.Windows.Forms.ListView();
      this.colMessageType = new System.Windows.Forms.ColumnHeader();
      this.colMessage = new System.Windows.Forms.ColumnHeader();
      this.colLine = new System.Windows.Forms.ColumnHeader();
      this.colType = new System.Windows.Forms.ColumnHeader();
      this.colState = new System.Windows.Forms.ColumnHeader();
      this.imgListMessages = new System.Windows.Forms.ImageList(this.components);
      this.panel3 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.lblCloseoutputPane = new System.Windows.Forms.Label();
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
      this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
      this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemSearchForward = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemSearchBackward = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemFind = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemReplace = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemGoToLine = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
      this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemKeywordsToUppercase = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemKeywordsToLowercase = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemCapitilizeKeywords = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemScriptToUppercase = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemScriptToLowercase = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuQuery = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemRun = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemStop = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemCheckSyntax = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemShowPlan = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemToggleOutputPane = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.tsMnuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
      this.tsMnuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
      this.tsMnuItemCut = new System.Windows.Forms.ToolStripMenuItem();
      this.tsMnuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.tsMnuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.statLblFileName = new System.Windows.Forms.ToolStripStatusLabel();
      this.statLblQueryCompletionTime = new System.Windows.Forms.ToolStripStatusLabel();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.timerExec = new System.Windows.Forms.Timer(this.components);
      this.popUpGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.popUpItemCopyGridToClipboard = new System.Windows.Forms.ToolStripMenuItem();
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
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.strip1 = new System.Windows.Forms.ToolStrip();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.btnSave = new System.Windows.Forms.ToolStripButton();
      this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cmbServers = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.cmbDatabases = new System.Windows.Forms.ToolStripComboBox();
      this.panOutput.SuspendLayout();
      this.tabOutput.SuspendLayout();
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
      this.panOutput.Location = new System.Drawing.Point(0, 158);
      this.panOutput.Name = "panOutput";
      this.panOutput.Size = new System.Drawing.Size(688, 217);
      this.panOutput.TabIndex = 1;
      // 
      // tabOutput
      // 
      this.tabOutput.Controls.Add(this.tabMessages);
      this.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabOutput.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.tabOutput.Location = new System.Drawing.Point(0, 20);
      this.tabOutput.Name = "tabOutput";
      this.tabOutput.SelectedIndex = 0;
      this.tabOutput.ShowToolTips = true;
      this.tabOutput.Size = new System.Drawing.Size(688, 197);
      this.tabOutput.TabIndex = 0;
      // 
      // tabMessages
      // 
      this.tabMessages.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.tabMessages.Controls.Add(this.lv);
      this.tabMessages.Location = new System.Drawing.Point(4, 25);
      this.tabMessages.Name = "tabMessages";
      this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
      this.tabMessages.Size = new System.Drawing.Size(680, 168);
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
      this.lv.Size = new System.Drawing.Size(670, 158);
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
      this.imgListMessages.Images.SetKeyName(0, "Information.bmp");
      this.imgListMessages.Images.SetKeyName(1, "Alert.png");
      this.imgListMessages.Images.SetKeyName(2, "attention.png");
      this.imgListMessages.Images.SetKeyName(3, "Information.bmp");
      this.imgListMessages.Images.SetKeyName(4, "Information-2.bmp");
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.panel3.Controls.Add(this.label1);
      this.panel3.Controls.Add(this.lblCloseoutputPane);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(688, 20);
      this.panel3.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(664, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "Messages And Results";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblCloseoutputPane
      // 
      this.lblCloseoutputPane.Cursor = System.Windows.Forms.Cursors.Default;
      this.lblCloseoutputPane.Dock = System.Windows.Forms.DockStyle.Right;
      this.lblCloseoutputPane.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblCloseoutputPane.ForeColor = System.Drawing.Color.White;
      this.lblCloseoutputPane.Location = new System.Drawing.Point(664, 0);
      this.lblCloseoutputPane.Name = "lblCloseoutputPane";
      this.lblCloseoutputPane.Size = new System.Drawing.Size(24, 20);
      this.lblCloseoutputPane.TabIndex = 1;
      this.lblCloseoutputPane.Text = "X";
      this.lblCloseoutputPane.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblCloseoutputPane.MouseLeave += new System.EventHandler(this.lblCloseoutputPane_MouseLeave);
      this.lblCloseoutputPane.Click += new System.EventHandler(this.lblCloseoutputPane_Click);
      this.lblCloseoutputPane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblCloseoutputPane_MouseDown);
      this.lblCloseoutputPane.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblCloseoutputPane_MouseClick);
      this.lblCloseoutputPane.MouseEnter += new System.EventHandler(this.lblCloseoutputPane_MouseEnter);
      this.lblCloseoutputPane.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblCloseoutputPane_MouseUp);
      // 
      // splitterOutput
      // 
      this.splitterOutput.BackColor = System.Drawing.SystemColors.Control;
      this.splitterOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterOutput.Location = new System.Drawing.Point(0, 154);
      this.splitterOutput.Name = "splitterOutput";
      this.splitterOutput.Size = new System.Drawing.Size(688, 4);
      this.splitterOutput.TabIndex = 2;
      this.splitterOutput.TabStop = false;
      // 
      // panEditor
      // 
      this.panEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panEditor.Location = new System.Drawing.Point(0, 53);
      this.panEditor.Name = "panEditor";
      this.panEditor.Size = new System.Drawing.Size(688, 101);
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
      this.popUpTab.Size = new System.Drawing.Size(183, 170);
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
            this.mnuQuery});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(688, 24);
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
            this.toolStripMenuItem6,
            this.searchToolStripMenuItem,
            this.mnuItemGoToLine,
            this.toolStripMenuItem8,
            this.formatToolStripMenuItem});
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
      this.mnuItemUndo.Size = new System.Drawing.Size(168, 22);
      this.mnuItemUndo.Text = "Undo";
      // 
      // mnuItemRedo
      // 
      this.mnuItemRedo.Image = global::PragmaSQL.Properties.Resources.redo;
      this.mnuItemRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemRedo.Name = "mnuItemRedo";
      this.mnuItemRedo.Size = new System.Drawing.Size(168, 22);
      this.mnuItemRedo.Text = "Redo";
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new System.Drawing.Size(165, 6);
      // 
      // mnuItemCut
      // 
      this.mnuItemCut.Image = global::PragmaSQL.Properties.Resources.cut_2;
      this.mnuItemCut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemCut.Name = "mnuItemCut";
      this.mnuItemCut.Size = new System.Drawing.Size(168, 22);
      this.mnuItemCut.Text = "Cut";
      // 
      // mnuItemCopy
      // 
      this.mnuItemCopy.Image = global::PragmaSQL.Properties.Resources.copy;
      this.mnuItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemCopy.Name = "mnuItemCopy";
      this.mnuItemCopy.Size = new System.Drawing.Size(168, 22);
      this.mnuItemCopy.Text = "Copy";
      // 
      // mnuItemPaste
      // 
      this.mnuItemPaste.Image = global::PragmaSQL.Properties.Resources.paste;
      this.mnuItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemPaste.Name = "mnuItemPaste";
      this.mnuItemPaste.Size = new System.Drawing.Size(168, 22);
      this.mnuItemPaste.Text = "Paste";
      // 
      // toolStripMenuItem6
      // 
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new System.Drawing.Size(165, 6);
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
      this.searchToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
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
      this.mnuItemGoToLine.Size = new System.Drawing.Size(168, 22);
      this.mnuItemGoToLine.Text = "Go To Line";
      // 
      // toolStripMenuItem8
      // 
      this.toolStripMenuItem8.Name = "toolStripMenuItem8";
      this.toolStripMenuItem8.Size = new System.Drawing.Size(165, 6);
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
      this.formatToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
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
      this.mnuQuery.MergeIndex = 2;
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
      // popUpEditor
      // 
      this.popUpEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMnuItemUndo,
            this.tsMnuItemRedo,
            this.toolStripMenuItem9,
            this.tsMnuItemCut,
            this.tsMnuItemCopy,
            this.tsMnuItemPaste});
      this.popUpEditor.Name = "contextMenuEditor";
      this.popUpEditor.Size = new System.Drawing.Size(113, 120);
      // 
      // tsMnuItemUndo
      // 
      this.tsMnuItemUndo.Name = "tsMnuItemUndo";
      this.tsMnuItemUndo.Size = new System.Drawing.Size(112, 22);
      this.tsMnuItemUndo.Text = "Undo";
      // 
      // tsMnuItemRedo
      // 
      this.tsMnuItemRedo.Name = "tsMnuItemRedo";
      this.tsMnuItemRedo.Size = new System.Drawing.Size(112, 22);
      this.tsMnuItemRedo.Text = "Redo";
      // 
      // toolStripMenuItem9
      // 
      this.toolStripMenuItem9.Name = "toolStripMenuItem9";
      this.toolStripMenuItem9.Size = new System.Drawing.Size(109, 6);
      // 
      // tsMnuItemCut
      // 
      this.tsMnuItemCut.Image = global::PragmaSQL.Properties.Resources.cut_2;
      this.tsMnuItemCut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsMnuItemCut.Name = "tsMnuItemCut";
      this.tsMnuItemCut.Size = new System.Drawing.Size(112, 22);
      this.tsMnuItemCut.Text = "Cut";
      // 
      // tsMnuItemCopy
      // 
      this.tsMnuItemCopy.Image = global::PragmaSQL.Properties.Resources.copy;
      this.tsMnuItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsMnuItemCopy.Name = "tsMnuItemCopy";
      this.tsMnuItemCopy.ShortcutKeyDisplayString = "";
      this.tsMnuItemCopy.Size = new System.Drawing.Size(112, 22);
      this.tsMnuItemCopy.Text = "Copy";
      // 
      // tsMnuItemPaste
      // 
      this.tsMnuItemPaste.Image = global::PragmaSQL.Properties.Resources.paste;
      this.tsMnuItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsMnuItemPaste.Name = "tsMnuItemPaste";
      this.tsMnuItemPaste.ShortcutKeyDisplayString = "";
      this.tsMnuItemPaste.Size = new System.Drawing.Size(112, 22);
      this.tsMnuItemPaste.Text = "Paste";
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
            this.statLblFileName,
            this.statLblQueryCompletionTime});
      this.statusStrip1.Location = new System.Drawing.Point(0, 375);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(688, 25);
      this.statusStrip1.TabIndex = 6;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // statLblFileName
      // 
      this.statLblFileName.Name = "statLblFileName";
      this.statLblFileName.Size = new System.Drawing.Size(534, 20);
      this.statLblFileName.Spring = true;
      this.statLblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // statLblQueryCompletionTime
      // 
      this.statLblQueryCompletionTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                  | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                  | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.statLblQueryCompletionTime.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
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
            this.exportToFileToolStripMenuItem});
      this.popUpGrid.Name = "popUpGrid";
      this.popUpGrid.Size = new System.Drawing.Size(152, 48);
      // 
      // popUpItemCopyGridToClipboard
      // 
      this.popUpItemCopyGridToClipboard.Name = "popUpItemCopyGridToClipboard";
      this.popUpItemCopyGridToClipboard.Size = new System.Drawing.Size(151, 22);
      this.popUpItemCopyGridToClipboard.Text = "Copy";
      // 
      // exportToFileToolStripMenuItem
      // 
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
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
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(688, 53);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(688, 53);
      this.toolStripContainer1.TabIndex = 9;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanelVisible = false;
      // 
      // strip2
      // 
      this.strip2.Dock = System.Windows.Forms.DockStyle.None;
      this.strip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.strip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRun,
            this.btnStop,
            this.btnCheckSyntax,
            this.btnShowPlan});
      this.strip2.Location = new System.Drawing.Point(422, 0);
      this.strip2.Name = "strip2";
      this.strip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.strip2.Size = new System.Drawing.Size(95, 25);
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
            this.toolStripButton4});
      this.strip3.Location = new System.Drawing.Point(0, 25);
      this.strip3.Name = "strip3";
      this.strip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.strip3.Size = new System.Drawing.Size(688, 25);
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
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
      // 
      // strip1
      // 
      this.strip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.strip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbServers,
            this.toolStripLabel2,
            this.cmbDatabases});
      this.strip1.Location = new System.Drawing.Point(0, 0);
      this.strip1.Name = "strip1";
      this.strip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.strip1.Size = new System.Drawing.Size(688, 25);
      this.strip1.TabIndex = 6;
      this.strip1.Text = "toolStrip1";
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
      // frmScriptEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(688, 400);
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
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScriptEditor_FormClosing);
      this.Leave += new System.EventHandler(this.frmScriptEditor_Leave);
      this.Load += new System.EventHandler(this.frmScriptEditor_Load);
      this.panOutput.ResumeLayout(false);
      this.tabOutput.ResumeLayout(false);
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
    private System.Windows.Forms.ToolStripStatusLabel statLblFileName;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuItemToggleOutputPane;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuItemOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSave;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSaveAs;
    private System.Windows.Forms.Label lblCloseoutputPane;
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
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
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
    private System.Windows.Forms.ToolStripButton toolStripButton4;
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

  }
}