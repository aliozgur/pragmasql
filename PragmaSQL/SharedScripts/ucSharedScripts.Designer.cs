namespace PragmaSQL
{
  partial class ucSharedScripts
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSharedScripts));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lblItemName = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblCreatedBy = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblCreatedOn = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblUpdatedBy = new System.Windows.Forms.ToolStripStatusLabel();
      this.panHelpText = new System.Windows.Forms.Panel();
      this.splitterHelpText = new System.Windows.Forms.Splitter();
      this.popUpTv = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuPopUpTvOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.diffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPopUpTvSendToDiffAsSource = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuPopUpTvSendToDiffAsDest = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.popUpTvNewRootFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvNewSubFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvNewScript = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvRename = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.popUpTvShowHelpText = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvSaveHelpText = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.popUpTvReload = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTvRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnReload = new System.Windows.Forms.ToolStripButton();
      this.btnRefresh = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnNewRootFolder = new System.Windows.Forms.ToolStripButton();
      this.btnNewSubFolder = new System.Windows.Forms.ToolStripButton();
      this.btnNewScript = new System.Windows.Forms.ToolStripButton();
      this.btnRename = new System.Windows.Forms.ToolStripButton();
      this.btnDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnSaveHelpText = new System.Windows.Forms.ToolStripButton();
      this.btnShowHelpText = new System.Windows.Forms.ToolStripButton();
      this.tv = new MWControls.MWTreeView();
      this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
      this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
      this.statusStrip1.SuspendLayout();
      this.panHelpText.SuspendLayout();
      this.popUpTv.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblItemName,
            this.lblCreatedBy,
            this.lblCreatedOn,
            this.lblUpdatedBy});
      this.statusStrip1.Location = new System.Drawing.Point(0, 530);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(293, 22);
      this.statusStrip1.SizingGrip = false;
      this.statusStrip1.TabIndex = 6;
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
      // lblCreatedOn
      // 
      this.lblCreatedOn.Name = "lblCreatedOn";
      this.lblCreatedOn.Size = new System.Drawing.Size(67, 17);
      this.lblCreatedOn.Text = "Created On:";
      // 
      // lblUpdatedBy
      // 
      this.lblUpdatedBy.Name = "lblUpdatedBy";
      this.lblUpdatedBy.Size = new System.Drawing.Size(63, 17);
      this.lblUpdatedBy.Text = "Updated By";
      // 
      // panHelpText
      // 
      this.panHelpText.Controls.Add(this.kryptonHeader1);
      this.panHelpText.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panHelpText.Location = new System.Drawing.Point(0, 301);
      this.panHelpText.Name = "panHelpText";
      this.panHelpText.Size = new System.Drawing.Size(293, 229);
      this.panHelpText.TabIndex = 7;
      // 
      // splitterHelpText
      // 
      this.splitterHelpText.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterHelpText.Location = new System.Drawing.Point(0, 298);
      this.splitterHelpText.Name = "splitterHelpText";
      this.splitterHelpText.Size = new System.Drawing.Size(293, 3);
      this.splitterHelpText.TabIndex = 8;
      this.splitterHelpText.TabStop = false;
      // 
      // popUpTv
      // 
      this.popUpTv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPopUpTvOpen,
            this.diffToolStripMenuItem,
            this.toolStripMenuItem4,
            this.popUpTvNewRootFolder,
            this.popUpTvNewSubFolder,
            this.popUpTvNewScript,
            this.popUpTvRename,
            this.popUpTvDelete,
            this.toolStripMenuItem1,
            this.popUpTvShowHelpText,
            this.popUpTvSaveHelpText,
            this.toolStripMenuItem2,
            this.popUpTvReload,
            this.popUpTvRefresh,
            this.toolStripMenuItem3,
            this.expandToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.collapseToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
      this.popUpTv.Name = "popUpTv";
      this.popUpTv.Size = new System.Drawing.Size(168, 358);
      // 
      // mnuPopUpTvOpen
      // 
      this.mnuPopUpTvOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuPopUpTvOpen.Name = "mnuPopUpTvOpen";
      this.mnuPopUpTvOpen.Size = new System.Drawing.Size(167, 22);
      this.mnuPopUpTvOpen.Text = "Open";
      // 
      // diffToolStripMenuItem
      // 
      this.diffToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPopUpTvSendToDiffAsSource,
            this.mnuPopUpTvSendToDiffAsDest});
      this.diffToolStripMenuItem.Name = "diffToolStripMenuItem";
      this.diffToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
      this.diffToolStripMenuItem.Text = "Diff";
      // 
      // mnuPopUpTvSendToDiffAsSource
      // 
      this.mnuPopUpTvSendToDiffAsSource.Name = "mnuPopUpTvSendToDiffAsSource";
      this.mnuPopUpTvSendToDiffAsSource.Size = new System.Drawing.Size(133, 22);
      this.mnuPopUpTvSendToDiffAsSource.Text = "As Source";
      // 
      // mnuPopUpTvSendToDiffAsDest
      // 
      this.mnuPopUpTvSendToDiffAsDest.Name = "mnuPopUpTvSendToDiffAsDest";
      this.mnuPopUpTvSendToDiffAsDest.Size = new System.Drawing.Size(133, 22);
      this.mnuPopUpTvSendToDiffAsDest.Text = "As Dest";
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(164, 6);
      // 
      // popUpTvNewRootFolder
      // 
      this.popUpTvNewRootFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.popUpTvNewRootFolder.Name = "popUpTvNewRootFolder";
      this.popUpTvNewRootFolder.Size = new System.Drawing.Size(167, 22);
      this.popUpTvNewRootFolder.Text = "New Root Folder";
      // 
      // popUpTvNewSubFolder
      // 
      this.popUpTvNewSubFolder.Name = "popUpTvNewSubFolder";
      this.popUpTvNewSubFolder.Size = new System.Drawing.Size(167, 22);
      this.popUpTvNewSubFolder.Text = "New Sub Folder";
      // 
      // popUpTvNewScript
      // 
      this.popUpTvNewScript.Name = "popUpTvNewScript";
      this.popUpTvNewScript.Size = new System.Drawing.Size(167, 22);
      this.popUpTvNewScript.Text = "New Snippet";
      // 
      // popUpTvRename
      // 
      this.popUpTvRename.Name = "popUpTvRename";
      this.popUpTvRename.Size = new System.Drawing.Size(167, 22);
      this.popUpTvRename.Text = "Rename ";
      // 
      // popUpTvDelete
      // 
      this.popUpTvDelete.Name = "popUpTvDelete";
      this.popUpTvDelete.Size = new System.Drawing.Size(167, 22);
      this.popUpTvDelete.Text = "Delete";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(164, 6);
      // 
      // popUpTvShowHelpText
      // 
      this.popUpTvShowHelpText.Name = "popUpTvShowHelpText";
      this.popUpTvShowHelpText.Size = new System.Drawing.Size(167, 22);
      this.popUpTvShowHelpText.Text = "Show Description";
      // 
      // popUpTvSaveHelpText
      // 
      this.popUpTvSaveHelpText.Name = "popUpTvSaveHelpText";
      this.popUpTvSaveHelpText.Size = new System.Drawing.Size(167, 22);
      this.popUpTvSaveHelpText.Text = "Save Description";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 6);
      // 
      // popUpTvReload
      // 
      this.popUpTvReload.Name = "popUpTvReload";
      this.popUpTvReload.Size = new System.Drawing.Size(167, 22);
      this.popUpTvReload.Text = "Reload";
      // 
      // popUpTvRefresh
      // 
      this.popUpTvRefresh.Name = "popUpTvRefresh";
      this.popUpTvRefresh.Size = new System.Drawing.Size(167, 22);
      this.popUpTvRefresh.Text = "Refresh";
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 6);
      // 
      // expandToolStripMenuItem
      // 
      this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
      this.expandToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
      this.expandToolStripMenuItem.Text = "Expand";
      this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
      // 
      // expandAllToolStripMenuItem
      // 
      this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
      this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
      this.expandAllToolStripMenuItem.Text = "Expand All";
      this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
      // 
      // collapseToolStripMenuItem
      // 
      this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
      this.collapseToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
      this.collapseToolStripMenuItem.Text = "Collapse";
      this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
      // 
      // collapseAllToolStripMenuItem
      // 
      this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
      this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
      this.collapseAllToolStripMenuItem.Text = "Collapse All";
      this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.imageList1.Images.SetKeyName(0, "Folder.bmp");
      this.imageList1.Images.SetKeyName(1, "VSProject_script.bmp");
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnReload,
            this.btnRefresh,
            this.toolStripSeparator1,
            this.btnNewRootFolder,
            this.btnNewSubFolder,
            this.btnNewScript,
            this.btnRename,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnSaveHelpText,
            this.btnShowHelpText});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(293, 25);
      this.toolStrip1.TabIndex = 11;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnReload
      // 
      this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnReload.Image = global::PragmaSQL.Properties.Resources.reload24bit;
      this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnReload.Name = "btnReload";
      this.btnReload.Size = new System.Drawing.Size(23, 22);
      this.btnReload.Text = "Reload";
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
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
      // btnNewSubFolder
      // 
      this.btnNewSubFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewSubFolder.Image = global::PragmaSQL.Properties.Resources.NewFolder;
      this.btnNewSubFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewSubFolder.Name = "btnNewSubFolder";
      this.btnNewSubFolder.Size = new System.Drawing.Size(23, 22);
      this.btnNewSubFolder.Text = "New Folder";
      // 
      // btnNewScript
      // 
      this.btnNewScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNewScript.Image = global::PragmaSQL.Properties.Resources.AddToFavorites;
      this.btnNewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNewScript.Name = "btnNewScript";
      this.btnNewScript.Size = new System.Drawing.Size(23, 22);
      this.btnNewScript.Text = "New Snippet";
      // 
      // btnRename
      // 
      this.btnRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRename.Image = global::PragmaSQL.Properties.Resources.RenameFolder;
      this.btnRename.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(23, 22);
      this.btnRename.Text = "Rename";
      // 
      // btnDelete
      // 
      this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDelete.Image = global::PragmaSQL.Properties.Resources.delete;
      this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(23, 22);
      this.btnDelete.Text = "Delete";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnSaveHelpText
      // 
      this.btnSaveHelpText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnSaveHelpText.Image = global::PragmaSQL.Properties.Resources.SaveAsWebPage;
      this.btnSaveHelpText.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnSaveHelpText.Name = "btnSaveHelpText";
      this.btnSaveHelpText.Size = new System.Drawing.Size(23, 22);
      this.btnSaveHelpText.Text = "Save Description";
      // 
      // btnShowHelpText
      // 
      this.btnShowHelpText.Checked = true;
      this.btnShowHelpText.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnShowHelpText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnShowHelpText.Image = global::PragmaSQL.Properties.Resources.help_2;
      this.btnShowHelpText.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnShowHelpText.Name = "btnShowHelpText";
      this.btnShowHelpText.Size = new System.Drawing.Size(23, 22);
      this.btnShowHelpText.Text = "Show Description";
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
      this.tv.ImageList = this.imageList1;
      this.tv.Location = new System.Drawing.Point(0, 25);
      this.tv.MultiSelect = MWCommon.TreeViewMultiSelect.MultiSameBranchAndLevel;
      this.tv.Name = "tv";
      this.tv.SelectedImageIndex = 0;
      this.tv.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("tv.SelNodes")));
      this.tv.Size = new System.Drawing.Size(293, 273);
      this.tv.TabIndex = 10;
      this.tv.AfterSelNodeChanged += new System.EventHandler(this.tv_AfterSelNodeChanged);
      this.tv.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
      this.tv.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tv_ItemDrag);
      this.tv.BeforeSelNodeChanged += new System.EventHandler(this.tv_BeforeSelNodeChanged);
      this.tv.DoubleClick += new System.EventHandler(this.tv_DoubleClick);
      this.tv.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_BeforeExpand);
      this.tv.DragOver += new System.Windows.Forms.DragEventHandler(this.tv_DragOver);
      // 
      // kryptonHeader1
      // 
      this.kryptonHeader1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
      this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
      this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.kryptonHeader1.Size = new System.Drawing.Size(293, 29);
      this.kryptonHeader1.TabIndex = 4;
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
      this.buttonSpecAny1.UniqueName = "4A090F3D5BD24FF84A090F3D5BD24FF8";
      this.buttonSpecAny1.Click += new System.EventHandler(this.buttonSpecAny1_Click);
      // 
      // ucSharedScripts
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tv);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.splitterHelpText);
      this.Controls.Add(this.panHelpText);
      this.Controls.Add(this.statusStrip1);
      this.Name = "ucSharedScripts";
      this.Size = new System.Drawing.Size(293, 552);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.panHelpText.ResumeLayout(false);
      this.panHelpText.PerformLayout();
      this.popUpTv.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lblItemName;
    private System.Windows.Forms.ToolStripStatusLabel lblCreatedBy;
    private System.Windows.Forms.ToolStripStatusLabel lblUpdatedBy;
    private System.Windows.Forms.Panel panHelpText;
    private System.Windows.Forms.Splitter splitterHelpText;
    private System.Windows.Forms.ToolStripStatusLabel lblCreatedOn;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ContextMenuStrip popUpTv;
    private System.Windows.Forms.ToolStripMenuItem popUpTvNewRootFolder;
    private System.Windows.Forms.ToolStripMenuItem popUpTvNewSubFolder;
    private System.Windows.Forms.ToolStripMenuItem popUpTvNewScript;
    private System.Windows.Forms.ToolStripMenuItem popUpTvRename;
    private System.Windows.Forms.ToolStripMenuItem popUpTvDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem popUpTvShowHelpText;
    private System.Windows.Forms.ToolStripMenuItem popUpTvSaveHelpText;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem popUpTvReload;
    private System.Windows.Forms.ToolStripMenuItem popUpTvRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnReload;
    private System.Windows.Forms.ToolStripButton btnRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnNewRootFolder;
    private System.Windows.Forms.ToolStripButton btnNewSubFolder;
    private System.Windows.Forms.ToolStripButton btnNewScript;
    private System.Windows.Forms.ToolStripButton btnRename;
    private System.Windows.Forms.ToolStripButton btnDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnSaveHelpText;
    private System.Windows.Forms.ToolStripButton btnShowHelpText;
    private System.Windows.Forms.ToolStripMenuItem mnuPopUpTvOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    internal MWControls.MWTreeView tv;
    private System.Windows.Forms.ToolStripMenuItem diffToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuPopUpTvSendToDiffAsSource;
    private System.Windows.Forms.ToolStripMenuItem mnuPopUpTvSendToDiffAsDest;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
    private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
  }
}
