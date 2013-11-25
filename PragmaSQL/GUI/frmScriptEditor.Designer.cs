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
      this.panel2 = new System.Windows.Forms.Panel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemSave = new System.Windows.Forms.ToolStripMenuItem();
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.menuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemRun = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemStop = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemCheckSyntax = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemToggleOutputPane = new System.Windows.Forms.ToolStripMenuItem();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnOpen = new System.Windows.Forms.ToolStripButton();
      this.btnSave = new System.Windows.Forms.ToolStripButton();
      this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cmbServers = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.cmbDatabases = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnRun = new System.Windows.Forms.ToolStripButton();
      this.btnStop = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnCheckSyntax = new System.Windows.Forms.ToolStripButton();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.statLblFileName = new System.Windows.Forms.ToolStripStatusLabel();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
      this.btnFindNext = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnOutDent = new System.Windows.Forms.ToolStripButton();
      this.btnIndent = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.btnToggleBlockComment = new System.Windows.Forms.ToolStripButton();
      this.btnToggleLineComment = new System.Windows.Forms.ToolStripButton();
      this.panOutput.SuspendLayout();
      this.tabOutput.SuspendLayout();
      this.tabMessages.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panEditor.SuspendLayout();
      this.contextMenuTabPage.SuspendLayout();
      this.mainMenu.SuspendLayout();
      this.contextMenuEditor.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panOutput
      // 
      this.panOutput.Controls.Add(this.tabOutput);
      this.panOutput.Controls.Add(this.panel3);
      this.panOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panOutput.Location = new System.Drawing.Point(0, 250);
      this.panOutput.Name = "panOutput";
      this.panOutput.Size = new System.Drawing.Size(825, 236);
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
      this.tabOutput.Size = new System.Drawing.Size(825, 216);
      this.tabOutput.TabIndex = 0;
      // 
      // tabMessages
      // 
      this.tabMessages.Controls.Add(this.lv);
      this.tabMessages.Location = new System.Drawing.Point(4, 25);
      this.tabMessages.Name = "tabMessages";
      this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
      this.tabMessages.Size = new System.Drawing.Size(817, 187);
      this.tabMessages.TabIndex = 0;
      this.tabMessages.Text = "Messages";
      this.tabMessages.UseVisualStyleBackColor = true;
      // 
      // lv
      // 
      this.lv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
      this.lv.Size = new System.Drawing.Size(811, 181);
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
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.panel3.Controls.Add(this.label1);
      this.panel3.Controls.Add(this.lblCloseoutputPane);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(825, 20);
      this.panel3.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(801, 20);
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
      this.lblCloseoutputPane.Location = new System.Drawing.Point(801, 0);
      this.lblCloseoutputPane.Name = "lblCloseoutputPane";
      this.lblCloseoutputPane.Size = new System.Drawing.Size(24, 20);
      this.lblCloseoutputPane.TabIndex = 1;
      this.lblCloseoutputPane.Text = "X";
      this.lblCloseoutputPane.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lblCloseoutputPane.MouseLeave += new System.EventHandler(this.lblCloseoutputPane_MouseLeave);
      this.lblCloseoutputPane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblCloseoutputPane_MouseDown);
      this.lblCloseoutputPane.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblCloseoutputPane_MouseClick);
      this.lblCloseoutputPane.MouseEnter += new System.EventHandler(this.lblCloseoutputPane_MouseEnter);
      this.lblCloseoutputPane.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblCloseoutputPane_MouseUp);
      // 
      // splitterOutput
      // 
      this.splitterOutput.BackColor = System.Drawing.SystemColors.Control;
      this.splitterOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterOutput.Location = new System.Drawing.Point(0, 246);
      this.splitterOutput.Name = "splitterOutput";
      this.splitterOutput.Size = new System.Drawing.Size(825, 4);
      this.splitterOutput.TabIndex = 2;
      this.splitterOutput.TabStop = false;
      // 
      // panEditor
      // 
      this.panEditor.Controls.Add(this.panel2);
      this.panEditor.Controls.Add(this.panel1);
      this.panEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panEditor.Location = new System.Drawing.Point(0, 50);
      this.panEditor.Name = "panEditor";
      this.panEditor.Size = new System.Drawing.Size(825, 196);
      this.panEditor.TabIndex = 3;
      // 
      // panel2
      // 
      this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel2.Location = new System.Drawing.Point(820, 5);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(5, 191);
      this.panel2.TabIndex = 1;
      // 
      // panel1
      // 
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(825, 5);
      this.panel1.TabIndex = 0;
      // 
      // contextMenuTabPage
      // 
      this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mnuItemSave});
      this.contextMenuTabPage.Name = "contextMenuTab";
      this.contextMenuTabPage.Size = new System.Drawing.Size(167, 104);
      // 
      // closeToolStripMenuItem
      // 
      this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
      this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeToolStripMenuItem.Text = "Close";
      this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
      // 
      // closeAllButThisToolStripMenuItem
      // 
      this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
      this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllButThisToolStripMenuItem.Text = "Close All But This";
      this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
      // 
      // closeAllToolStripMenuItem
      // 
      this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
      this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllToolStripMenuItem.Text = "Close All";
      this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
      // 
      // mnuItemSave
      // 
      this.mnuItemSave.Image = global::PragmaSQL.Properties.Resources.save;
      this.mnuItemSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuItemSave.Name = "mnuItemSave";
      this.mnuItemSave.Size = new System.Drawing.Size(166, 22);
      this.mnuItemSave.Text = "Save";
      this.mnuItemSave.Click += new System.EventHandler(this.mnuItemSave_Click);
      // 
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem1,
            this.fileToolStripMenuItem});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(825, 24);
      this.mainMenu.TabIndex = 4;
      this.mainMenu.Visible = false;
      // 
      // menuItem1
      // 
      this.menuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemRun,
            this.mnuItemStop,
            this.mnuItemCheckSyntax,
            this.toolStripMenuItem3,
            this.mnuItemToggleOutputPane});
      this.menuItem1.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.menuItem1.MergeIndex = 1;
      this.menuItem1.Name = "menuItem1";
      this.menuItem1.Size = new System.Drawing.Size(49, 20);
      this.menuItem1.Text = "Query";
      // 
      // mnuItemRun
      // 
      this.mnuItemRun.Name = "mnuItemRun";
      this.mnuItemRun.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.mnuItemRun.Size = new System.Drawing.Size(220, 22);
      this.mnuItemRun.Text = "Run";
      this.mnuItemRun.Click += new System.EventHandler(this.mnuItemRun_Click);
      // 
      // mnuItemStop
      // 
      this.mnuItemStop.Name = "mnuItemStop";
      this.mnuItemStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
      this.mnuItemStop.Size = new System.Drawing.Size(220, 22);
      this.mnuItemStop.Text = "Stop ";
      this.mnuItemStop.Click += new System.EventHandler(this.mnuItemStop_Click);
      // 
      // mnuItemCheckSyntax
      // 
      this.mnuItemCheckSyntax.Name = "mnuItemCheckSyntax";
      this.mnuItemCheckSyntax.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
      this.mnuItemCheckSyntax.Size = new System.Drawing.Size(220, 22);
      this.mnuItemCheckSyntax.Text = "Check Syntax";
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(217, 6);
      // 
      // mnuItemToggleOutputPane
      // 
      this.mnuItemToggleOutputPane.Name = "mnuItemToggleOutputPane";
      this.mnuItemToggleOutputPane.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
      this.mnuItemToggleOutputPane.Size = new System.Drawing.Size(220, 22);
      this.mnuItemToggleOutputPane.Text = "Toggle Output Pane";
      this.mnuItemToggleOutputPane.Click += new System.EventHandler(this.toggleOutputPaneToolStripMenuItem_Click);
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem4,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
      this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
      this.fileToolStripMenuItem.MergeIndex = 0;
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
      this.fileToolStripMenuItem.Text = "File";
      this.fileToolStripMenuItem.Visible = false;
      // 
      // newScriptToolStripMenuItem
      // 
      this.newScriptToolStripMenuItem.Name = "newScriptToolStripMenuItem";
      this.newScriptToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.newScriptToolStripMenuItem.Text = "New Script";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.openToolStripMenuItem.Text = "Open";
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(133, 6);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.saveToolStripMenuItem.Text = "Save";
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
      this.saveAsToolStripMenuItem.Text = "Save As";
      // 
      // contextMenuEditor
      // 
      this.contextMenuEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem});
      this.contextMenuEditor.Name = "contextMenuEditor";
      this.contextMenuEditor.Size = new System.Drawing.Size(113, 70);
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Image = global::PragmaSQL.Properties.Resources.copy;
      this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      // 
      // cutToolStripMenuItem
      // 
      this.cutToolStripMenuItem.Image = global::PragmaSQL.Properties.Resources.cut_2;
      this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
      this.cutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
      this.cutToolStripMenuItem.Text = "Cut";
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Image = global::PragmaSQL.Properties.Resources.paste;
      this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
      this.pasteToolStripMenuItem.Text = "Paste";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbServers,
            this.toolStripLabel2,
            this.cmbDatabases,
            this.toolStripSeparator2,
            this.btnRun,
            this.btnStop,
            this.toolStripSeparator3,
            this.btnCheckSyntax});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(825, 25);
      this.toolStrip1.TabIndex = 5;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnOpen
      // 
      this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnOpen.Image = global::PragmaSQL.Properties.Resources.open;
      this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(23, 22);
      this.btnOpen.Text = "Open";
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // btnSave
      // 
      this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSave.Image = global::PragmaSQL.Properties.Resources.save;
      this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(23, 22);
      this.btnSave.Text = "Save";
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnSaveAs
      // 
      this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSaveAs.Image = global::PragmaSQL.Properties.Resources.Save_As;
      this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSaveAs.Name = "btnSaveAs";
      this.btnSaveAs.Size = new System.Drawing.Size(23, 22);
      this.btnSaveAs.Text = "Save As";
      this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
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
      this.cmbDatabases.ForeColor = System.Drawing.Color.Navy;
      this.cmbDatabases.MaxDropDownItems = 12;
      this.cmbDatabases.Name = "cmbDatabases";
      this.cmbDatabases.Size = new System.Drawing.Size(121, 25);
      this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnRun
      // 
      this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRun.Image = global::PragmaSQL.Properties.Resources.Run;
      this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(23, 22);
      this.btnRun.Text = "toolStripButton4";
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
      // 
      // btnStop
      // 
      this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStop.Enabled = false;
      this.btnStop.Image = global::PragmaSQL.Properties.Resources.Stop;
      this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(23, 22);
      this.btnStop.Text = "toolStripButton6";
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnCheckSyntax
      // 
      this.btnCheckSyntax.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnCheckSyntax.Image = global::PragmaSQL.Properties.Resources.OK;
      this.btnCheckSyntax.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnCheckSyntax.Name = "btnCheckSyntax";
      this.btnCheckSyntax.Size = new System.Drawing.Size(23, 22);
      this.btnCheckSyntax.Text = "toolStripButton5";
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
            this.statLblFileName});
      this.statusStrip1.Location = new System.Drawing.Point(0, 486);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(825, 22);
      this.statusStrip1.TabIndex = 6;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // statLblFileName
      // 
      this.statLblFileName.Name = "statLblFileName";
      this.statLblFileName.Size = new System.Drawing.Size(0, 17);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "sql";
      this.saveFileDialog1.Filter = "SQL Files|*.sql|Query Files|*.qry|All Files|*.*";
      // 
      // toolStrip2
      // 
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripTextBox1,
            this.btnFindNext,
            this.toolStripSeparator4,
            this.btnOutDent,
            this.btnIndent,
            this.toolStripSeparator5,
            this.btnToggleBlockComment,
            this.btnToggleLineComment});
      this.toolStrip2.Location = new System.Drawing.Point(0, 25);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(825, 25);
      this.toolStrip2.TabIndex = 7;
      this.toolStrip2.Text = "toolStrip2";
      // 
      // toolStripLabel3
      // 
      this.toolStripLabel3.Name = "toolStripLabel3";
      this.toolStripLabel3.Size = new System.Drawing.Size(27, 22);
      this.toolStripLabel3.Text = "Find";
      // 
      // toolStripTextBox1
      // 
      this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.toolStripTextBox1.Name = "toolStripTextBox1";
      this.toolStripTextBox1.Size = new System.Drawing.Size(150, 25);
      // 
      // btnFindNext
      // 
      this.btnFindNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnFindNext.Image = ((System.Drawing.Image)(resources.GetObject("btnFindNext.Image")));
      this.btnFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFindNext.Name = "btnFindNext";
      this.btnFindNext.Size = new System.Drawing.Size(23, 22);
      this.btnFindNext.Text = "Find Next";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // btnOutDent
      // 
      this.btnOutDent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnOutDent.Image = ((System.Drawing.Image)(resources.GetObject("btnOutDent.Image")));
      this.btnOutDent.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOutDent.Name = "btnOutDent";
      this.btnOutDent.Size = new System.Drawing.Size(23, 22);
      this.btnOutDent.Text = "Outdent Selection";
      this.btnOutDent.Click += new System.EventHandler(this.btnOutDent_Click);
      // 
      // btnIndent
      // 
      this.btnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnIndent.Image = ((System.Drawing.Image)(resources.GetObject("btnIndent.Image")));
      this.btnIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnIndent.Name = "btnIndent";
      this.btnIndent.Size = new System.Drawing.Size(23, 22);
      this.btnIndent.Text = "Indent Selection";
      this.btnIndent.Click += new System.EventHandler(this.btnIndent_Click);
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
      // frmScriptEditor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(825, 508);
      this.Controls.Add(this.panEditor);
      this.Controls.Add(this.toolStrip2);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.mainMenu);
      this.Controls.Add(this.splitterOutput);
      this.Controls.Add(this.panOutput);
      this.Controls.Add(this.statusStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mainMenu;
      this.Name = "frmScriptEditor";
      this.TabPageContextMenuStrip = this.contextMenuTabPage;
      this.Load += new System.EventHandler(this.frmQueryEditor_Load);
      this.panOutput.ResumeLayout(false);
      this.tabOutput.ResumeLayout(false);
      this.tabMessages.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panEditor.ResumeLayout(false);
      this.contextMenuTabPage.ResumeLayout(false);
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.contextMenuEditor.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panOutput;
    private System.Windows.Forms.TabControl tabOutput;
    private System.Windows.Forms.TabPage tabMessages;
    private System.Windows.Forms.Splitter splitterOutput;
    private System.Windows.Forms.Panel panEditor;
    private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSave;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.ToolStripMenuItem menuItem1;
    private System.Windows.Forms.ToolStripMenuItem mnuItemRun;
    private System.Windows.Forms.ToolStripMenuItem mnuItemStop;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCheckSyntax;
    private System.Windows.Forms.ContextMenuStrip contextMenuEditor;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnOpen;
    private System.Windows.Forms.ToolStripButton btnSave;
    private System.Windows.Forms.ToolStripButton btnSaveAs;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripComboBox cmbServers;
    private System.Windows.Forms.ToolStripComboBox cmbDatabases;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnRun;
    private System.Windows.Forms.ToolStripButton btnCheckSyntax;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripButton btnStop;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel statLblFileName;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuItemToggleOutputPane;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newScriptToolStripMenuItem;
    private System.Windows.Forms.Label lblCloseoutputPane;
    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader colMessageType;
    private System.Windows.Forms.ColumnHeader colMessage;
    private System.Windows.Forms.ColumnHeader colLine;
    private System.Windows.Forms.ColumnHeader colType;
    private System.Windows.Forms.ColumnHeader colState;
    private System.Windows.Forms.ImageList imgListMessages;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
    private System.Windows.Forms.ToolStripButton btnFindNext;
    private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripButton btnOutDent;
    private System.Windows.Forms.ToolStripButton btnIndent;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripButton btnToggleBlockComment;
    private System.Windows.Forms.ToolStripButton btnToggleLineComment;

  }
}