namespace PragmaSQL
{
  partial class ucObjectGrouping
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucObjectGrouping));
      this.tv = new MWControls.MWTreeView();
      this.popUpTv = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.popUpTvModify = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.popUpTvAddSubFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvAddRootFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvRenameFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.popUpTvReload = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.popUpTvShowHelpText = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvSaveHelpText = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
      this.miImportSelectedNodes = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
      this.miScriptSelectedObjects = new System.Windows.Forms.ToolStripMenuItem();
      this.miDumpObjName = new System.Windows.Forms.ToolStripMenuItem();
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.btnReload = new System.Windows.Forms.ToolStripButton();
      this.btnNewRootFolder = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnRefresh = new System.Windows.Forms.ToolStripButton();
      this.btnNewSubFolder = new System.Windows.Forms.ToolStripButton();
      this.btnRename = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSaveHelpText = new System.Windows.Forms.ToolStripButton();
      this.btnHelpText = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.btnDumpObjName = new System.Windows.Forms.ToolStripButton();
      this.btnScriptSelectedObjects = new System.Windows.Forms.ToolStripButton();
      this.btnImportSelNodes = new System.Windows.Forms.ToolStripButton();
      this.tsServerAndDb = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cmbServers = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.cmbDatabases = new System.Windows.Forms.ToolStripComboBox();
      this.panHelpText = new System.Windows.Forms.Panel();
      this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
      this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
      this.splitterHelpText = new System.Windows.Forms.Splitter();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lblItemName = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblCreatedBy = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblUpdatedBy = new System.Windows.Forms.ToolStripStatusLabel();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuSaveHelpText = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTv.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.tsServerAndDb.SuspendLayout();
      this.panHelpText.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tv
      // 
      this.tv.AllowDrop = true;
      this.tv.CheckedNodes = ((System.Collections.Hashtable)(resources.GetObject("tv.CheckedNodes")));
      this.tv.ContextMenuStrip = this.popUpTv;
      this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tv.FullRowSelect = true;
      this.tv.HideSelection = false;
      this.tv.ImageIndex = 0;
      this.tv.ImageList = this.ImageList1;
      this.tv.Location = new System.Drawing.Point(0, 48);
      this.tv.MultiSelect = MWCommon.TreeViewMultiSelect.MultiSameBranchAndLevel;
      this.tv.Name = "tv";
      this.tv.SelectedImageIndex = 0;
      this.tv.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("tv.SelNodes")));
      this.tv.Size = new System.Drawing.Size(375, 307);
      this.tv.TabIndex = 1;
      this.tv.AfterSelNodeChanged += new System.EventHandler(this.tv_AfterSelNodeChanged);
      this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
      this.tv.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
      this.tv.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tv_ItemDrag);
      this.tv.BeforeSelNodeChanged += new System.EventHandler(this.tv_BeforeSelNodeChanged);
      this.tv.DoubleClick += new System.EventHandler(this.tv_DoubleClick);
      this.tv.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_BeforeExpand);
      this.tv.DragOver += new System.Windows.Forms.DragEventHandler(this.tv_DragOver);
      // 
      // popUpTv
      // 
      this.popUpTv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popUpTvModify,
            this.popUpTvOpen,
            this.toolStripMenuItem3,
            this.popUpTvAddSubFolder,
            this.popUpTvAddRootFolder,
            this.popUpTvRenameFolder,
            this.popUpTvDeleteItem,
            this.toolStripMenuItem2,
            this.popUpTvReload,
            this.popUpTvRefresh,
            this.toolStripMenuItem1,
            this.popUpTvShowHelpText,
            this.popUpTvSaveHelpText,
            this.toolStripMenuItem4,
            this.expandToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.collapseToolStripMenuItem,
            this.collapseAllToolStripMenuItem,
            this.toolStripMenuItem5,
            this.miImportSelectedNodes,
            this.toolStripMenuItem6,
            this.miScriptSelectedObjects,
            this.miDumpObjName});
      this.popUpTv.Name = "popUpTv";
      this.popUpTv.Size = new System.Drawing.Size(227, 414);
      // 
      // popUpTvModify
      // 
      this.popUpTvModify.Name = "popUpTvModify";
      this.popUpTvModify.Size = new System.Drawing.Size(226, 22);
      this.popUpTvModify.Text = "Modify";
      // 
      // popUpTvOpen
      // 
      this.popUpTvOpen.Name = "popUpTvOpen";
      this.popUpTvOpen.Size = new System.Drawing.Size(226, 22);
      this.popUpTvOpen.Text = "Open";
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(223, 6);
      // 
      // popUpTvAddSubFolder
      // 
      this.popUpTvAddSubFolder.Name = "popUpTvAddSubFolder";
      this.popUpTvAddSubFolder.Size = new System.Drawing.Size(226, 22);
      this.popUpTvAddSubFolder.Text = "Add Sub Folder";
      // 
      // popUpTvAddRootFolder
      // 
      this.popUpTvAddRootFolder.Name = "popUpTvAddRootFolder";
      this.popUpTvAddRootFolder.Size = new System.Drawing.Size(226, 22);
      this.popUpTvAddRootFolder.Text = "Add Root Folder";
      // 
      // popUpTvRenameFolder
      // 
      this.popUpTvRenameFolder.Name = "popUpTvRenameFolder";
      this.popUpTvRenameFolder.Size = new System.Drawing.Size(226, 22);
      this.popUpTvRenameFolder.Text = "Rename";
      // 
      // popUpTvDeleteItem
      // 
      this.popUpTvDeleteItem.Name = "popUpTvDeleteItem";
      this.popUpTvDeleteItem.Size = new System.Drawing.Size(226, 22);
      this.popUpTvDeleteItem.Text = "Delete ";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(223, 6);
      // 
      // popUpTvReload
      // 
      this.popUpTvReload.Name = "popUpTvReload";
      this.popUpTvReload.Size = new System.Drawing.Size(226, 22);
      this.popUpTvReload.Text = "Reload";
      // 
      // popUpTvRefresh
      // 
      this.popUpTvRefresh.Name = "popUpTvRefresh";
      this.popUpTvRefresh.Size = new System.Drawing.Size(226, 22);
      this.popUpTvRefresh.Text = "Refresh";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(223, 6);
      // 
      // popUpTvShowHelpText
      // 
      this.popUpTvShowHelpText.Name = "popUpTvShowHelpText";
      this.popUpTvShowHelpText.Size = new System.Drawing.Size(226, 22);
      this.popUpTvShowHelpText.Text = "Show HelpText";
      // 
      // popUpTvSaveHelpText
      // 
      this.popUpTvSaveHelpText.Name = "popUpTvSaveHelpText";
      this.popUpTvSaveHelpText.Size = new System.Drawing.Size(226, 22);
      this.popUpTvSaveHelpText.Text = "Save HelpText";
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(223, 6);
      // 
      // expandToolStripMenuItem
      // 
      this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
      this.expandToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.expandToolStripMenuItem.Text = "Expand";
      this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
      // 
      // expandAllToolStripMenuItem
      // 
      this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
      this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.expandAllToolStripMenuItem.Text = "Expand All";
      this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
      // 
      // collapseToolStripMenuItem
      // 
      this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
      this.collapseToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.collapseToolStripMenuItem.Text = "Collapse";
      this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
      // 
      // collapseAllToolStripMenuItem
      // 
      this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
      this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.collapseAllToolStripMenuItem.Text = "Collapse All";
      this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new System.Drawing.Size(223, 6);
      // 
      // miImportSelectedNodes
      // 
      this.miImportSelectedNodes.Name = "miImportSelectedNodes";
      this.miImportSelectedNodes.Size = new System.Drawing.Size(226, 22);
      this.miImportSelectedNodes.Text = "Import Selected Objects";
      // 
      // toolStripMenuItem6
      // 
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new System.Drawing.Size(223, 6);
      // 
      // miScriptSelectedObjects
      // 
      this.miScriptSelectedObjects.Name = "miScriptSelectedObjects";
      this.miScriptSelectedObjects.Size = new System.Drawing.Size(226, 22);
      this.miScriptSelectedObjects.Text = "Script Selected Objects";
      // 
      // miDumpObjName
      // 
      this.miDumpObjName.Name = "miDumpObjName";
      this.miDumpObjName.Size = new System.Drawing.Size(226, 22);
      this.miDumpObjName.Text = "Dump Selected Object Names";
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.ImageList1.Images.SetKeyName(0, "");
      this.ImageList1.Images.SetKeyName(1, "");
      this.ImageList1.Images.SetKeyName(2, "");
      this.ImageList1.Images.SetKeyName(3, "");
      this.ImageList1.Images.SetKeyName(4, "");
      this.ImageList1.Images.SetKeyName(5, "");
      this.ImageList1.Images.SetKeyName(6, "");
      this.ImageList1.Images.SetKeyName(7, "");
      this.ImageList1.Images.SetKeyName(8, "");
      this.ImageList1.Images.SetKeyName(9, "");
      this.ImageList1.Images.SetKeyName(10, "");
      this.ImageList1.Images.SetKeyName(11, "");
      this.ImageList1.Images.SetKeyName(12, "");
      this.ImageList1.Images.SetKeyName(13, "");
      this.ImageList1.Images.SetKeyName(14, "");
      this.ImageList1.Images.SetKeyName(15, "");
      this.ImageList1.Images.SetKeyName(16, "");
      this.ImageList1.Images.SetKeyName(17, "");
      this.ImageList1.Images.SetKeyName(18, "");
      this.ImageList1.Images.SetKeyName(19, "");
      this.ImageList1.Images.SetKeyName(20, "");
      this.ImageList1.Images.SetKeyName(21, "Serious.bmp");
      this.ImageList1.Images.SetKeyName(22, "");
      // 
      // toolStripContainer1
      // 
      this.toolStripContainer1.BottomToolStripPanelVisible = false;
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Controls.Add(this.toolStrip2);
      this.toolStripContainer1.ContentPanel.Controls.Add(this.tsServerAndDb);
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(375, 48);
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Top;
      this.toolStripContainer1.LeftToolStripPanelVisible = false;
      this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.RightToolStripPanelVisible = false;
      this.toolStripContainer1.Size = new System.Drawing.Size(375, 48);
      this.toolStripContainer1.TabIndex = 2;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanelVisible = false;
      // 
      // toolStrip2
      // 
      this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnReload,
            this.btnNewRootFolder,
            this.toolStripSeparator1,
            this.btnRefresh,
            this.btnNewSubFolder,
            this.btnRename,
            this.toolStripSeparator2,
            this.btnDelete,
            this.toolStripSeparator3,
            this.btnSaveHelpText,
            this.btnHelpText,
            this.toolStripSeparator4,
            this.btnDumpObjName,
            this.btnScriptSelectedObjects,
            this.btnImportSelNodes});
      this.toolStrip2.Location = new System.Drawing.Point(0, 25);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(375, 25);
      this.toolStrip2.TabIndex = 1;
      this.toolStrip2.Text = "toolStrip2";
      // 
      // btnReload
      // 
      this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnReload.Image = global::PragmaSQL.Properties.Resources.reload24bit;
      this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnReload.Name = "btnReload";
      this.btnReload.Size = new System.Drawing.Size(23, 22);
      this.btnReload.Text = "toolStripButton1";
      // 
      // btnNewRootFolder
      // 
      this.btnNewRootFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewRootFolder.Image = global::PragmaSQL.Properties.Resources.new_case;
      this.btnNewRootFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewRootFolder.Name = "btnNewRootFolder";
      this.btnNewRootFolder.Size = new System.Drawing.Size(23, 22);
      this.btnNewRootFolder.Text = "New Root Folder";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnRefresh
      // 
      this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRefresh.Image = global::PragmaSQL.Properties.Resources.Refresh;
      this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(23, 22);
      this.btnRefresh.Text = "Refresh";
      // 
      // btnNewSubFolder
      // 
      this.btnNewSubFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewSubFolder.Image = global::PragmaSQL.Properties.Resources.NewFolder;
      this.btnNewSubFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewSubFolder.Name = "btnNewSubFolder";
      this.btnNewSubFolder.Size = new System.Drawing.Size(23, 22);
      this.btnNewSubFolder.Text = "New Sub Folder";
      // 
      // btnRename
      // 
      this.btnRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRename.Image = global::PragmaSQL.Properties.Resources.RenameFolder;
      this.btnRename.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(23, 22);
      this.btnRename.Text = "Rename Folder";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnDelete
      // 
      this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDelete.Image = global::PragmaSQL.Properties.Resources.delete;
      this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(23, 22);
      this.btnDelete.Text = "Delete Item";
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // btnSaveHelpText
      // 
      this.btnSaveHelpText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSaveHelpText.Image = global::PragmaSQL.Properties.Resources.SaveAsWebPage;
      this.btnSaveHelpText.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSaveHelpText.Name = "btnSaveHelpText";
      this.btnSaveHelpText.Size = new System.Drawing.Size(23, 22);
      this.btnSaveHelpText.Text = "Save Help Text";
      // 
      // btnHelpText
      // 
      this.btnHelpText.CheckOnClick = true;
      this.btnHelpText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnHelpText.Image = global::PragmaSQL.Properties.Resources.help_2;
      this.btnHelpText.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHelpText.Name = "btnHelpText";
      this.btnHelpText.Size = new System.Drawing.Size(23, 22);
      this.btnHelpText.Text = "Show Help Text";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // btnDumpObjName
      // 
      this.btnDumpObjName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDumpObjName.Image = global::PragmaSQL.Properties.Resources.application_go;
      this.btnDumpObjName.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDumpObjName.Name = "btnDumpObjName";
      this.btnDumpObjName.Size = new System.Drawing.Size(23, 22);
      this.btnDumpObjName.Text = "Dump Selected Object Names";
      // 
      // btnScriptSelectedObjects
      // 
      this.btnScriptSelectedObjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnScriptSelectedObjects.Image = global::PragmaSQL.Properties.Resources.app;
      this.btnScriptSelectedObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnScriptSelectedObjects.Name = "btnScriptSelectedObjects";
      this.btnScriptSelectedObjects.Size = new System.Drawing.Size(23, 22);
      this.btnScriptSelectedObjects.Text = "toolStripButton1";
      this.btnScriptSelectedObjects.ToolTipText = "Script Selected Objects";
      // 
      // btnImportSelNodes
      // 
      this.btnImportSelNodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnImportSelNodes.Image = global::PragmaSQL.Properties.Resources.db;
      this.btnImportSelNodes.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnImportSelNodes.Name = "btnImportSelNodes";
      this.btnImportSelNodes.Size = new System.Drawing.Size(23, 22);
      this.btnImportSelNodes.Text = "Import Selected Objects";
      // 
      // tsServerAndDb
      // 
      this.tsServerAndDb.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.tsServerAndDb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tsServerAndDb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbServers,
            this.toolStripLabel2,
            this.cmbDatabases});
      this.tsServerAndDb.Location = new System.Drawing.Point(0, 0);
      this.tsServerAndDb.Name = "tsServerAndDb";
      this.tsServerAndDb.Size = new System.Drawing.Size(375, 25);
      this.tsServerAndDb.TabIndex = 0;
      this.tsServerAndDb.Text = "toolStrip1";
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(38, 22);
      this.toolStripLabel1.Text = "Server";
      // 
      // cmbServers
      // 
      this.cmbServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbServers.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbServers.Name = "cmbServers";
      this.cmbServers.Size = new System.Drawing.Size(130, 25);
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
      this.cmbDatabases.Name = "cmbDatabases";
      this.cmbDatabases.Size = new System.Drawing.Size(130, 25);
      this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
      // 
      // panHelpText
      // 
      this.panHelpText.Controls.Add(this.kryptonHeader1);
      this.panHelpText.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panHelpText.Location = new System.Drawing.Point(0, 358);
      this.panHelpText.Name = "panHelpText";
      this.panHelpText.Size = new System.Drawing.Size(375, 196);
      this.panHelpText.TabIndex = 3;
      // 
      // kryptonHeader1
      // 
      this.kryptonHeader1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
      this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
      this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.kryptonHeader1.Size = new System.Drawing.Size(375, 29);
      this.kryptonHeader1.TabIndex = 3;
      this.kryptonHeader1.Text = "Help Text";
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "Help Text";
      this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
      // 
      // buttonSpecAny1
      // 
      this.buttonSpecAny1.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Inherit;
      this.buttonSpecAny1.ExtraText = "";
      this.buttonSpecAny1.Image = null;
      this.buttonSpecAny1.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
      this.buttonSpecAny1.Text = "Close";
      this.buttonSpecAny1.UniqueName = "B59AECA65D624C16B59AECA65D624C16";
      this.buttonSpecAny1.Click += new System.EventHandler(this.buttonSpecAny1_Click);
      // 
      // splitterHelpText
      // 
      this.splitterHelpText.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterHelpText.Location = new System.Drawing.Point(0, 355);
      this.splitterHelpText.Name = "splitterHelpText";
      this.splitterHelpText.Size = new System.Drawing.Size(375, 3);
      this.splitterHelpText.TabIndex = 4;
      this.splitterHelpText.TabStop = false;
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblItemName,
            this.lblCreatedBy,
            this.lblUpdatedBy});
      this.statusStrip1.Location = new System.Drawing.Point(0, 554);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(375, 22);
      this.statusStrip1.TabIndex = 5;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // lblItemName
      // 
      this.lblItemName.Name = "lblItemName";
      this.lblItemName.Size = new System.Drawing.Size(34, 17);
      this.lblItemName.Text = "Name";
      // 
      // lblCreatedBy
      // 
      this.lblCreatedBy.Name = "lblCreatedBy";
      this.lblCreatedBy.Size = new System.Drawing.Size(61, 17);
      this.lblCreatedBy.Text = "Created By";
      // 
      // lblUpdatedBy
      // 
      this.lblUpdatedBy.Name = "lblUpdatedBy";
      this.lblUpdatedBy.Size = new System.Drawing.Size(63, 17);
      this.lblUpdatedBy.Text = "Updated By";
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveHelpText});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(156, 26);
      // 
      // mnuSaveHelpText
      // 
      this.mnuSaveHelpText.Name = "mnuSaveHelpText";
      this.mnuSaveHelpText.Size = new System.Drawing.Size(155, 22);
      this.mnuSaveHelpText.Text = "Save HelpText";
      // 
      // ucObjectGrouping
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tv);
      this.Controls.Add(this.splitterHelpText);
      this.Controls.Add(this.panHelpText);
      this.Controls.Add(this.toolStripContainer1);
      this.Controls.Add(this.statusStrip1);
      this.Name = "ucObjectGrouping";
      this.Size = new System.Drawing.Size(375, 576);
      this.Load += new System.EventHandler(this.ucObjectGrouping_Load);
      this.popUpTv.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.ContentPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.tsServerAndDb.ResumeLayout(false);
      this.tsServerAndDb.PerformLayout();
      this.panHelpText.ResumeLayout(false);
      this.panHelpText.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private MWControls.MWTreeView tv;
    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip tsServerAndDb;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton btnNewSubFolder;
    private System.Windows.Forms.ToolStripButton btnDelete;
    private System.Windows.Forms.ToolStripButton btnRename;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripComboBox cmbServers;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripComboBox cmbDatabases;
    private System.Windows.Forms.ImageList ImageList1;
    private System.Windows.Forms.ToolStripButton btnRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnReload;
    private System.Windows.Forms.ToolStripButton btnNewRootFolder;
    private System.Windows.Forms.Panel panHelpText;
    private System.Windows.Forms.Splitter splitterHelpText;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnHelpText;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lblItemName;
    private System.Windows.Forms.ToolStripStatusLabel lblCreatedBy;
    private System.Windows.Forms.ToolStripStatusLabel lblUpdatedBy;
    private System.Windows.Forms.ToolStripButton btnSaveHelpText;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveHelpText;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ContextMenuStrip popUpTv;
    private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem popUpTvAddSubFolder;
    private System.Windows.Forms.ToolStripMenuItem popUpTvAddRootFolder;
    private System.Windows.Forms.ToolStripMenuItem popUpTvRenameFolder;
    private System.Windows.Forms.ToolStripMenuItem popUpTvDeleteItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem popUpTvReload;
    private System.Windows.Forms.ToolStripMenuItem popUpTvRefresh;
    private System.Windows.Forms.ToolStripMenuItem popUpTvShowHelpText;
    private System.Windows.Forms.ToolStripMenuItem popUpTvSaveHelpText;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem popUpTvModify;
    private System.Windows.Forms.ToolStripMenuItem popUpTvOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripButton btnDumpObjName;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    private System.Windows.Forms.ToolStripMenuItem miDumpObjName;
    private System.Windows.Forms.ToolStripMenuItem miScriptSelectedObjects;
    private System.Windows.Forms.ToolStripButton btnScriptSelectedObjects;
    private System.Windows.Forms.ToolStripButton btnImportSelNodes;
    private System.Windows.Forms.ToolStripMenuItem miImportSelectedNodes;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
    private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
  }
}
