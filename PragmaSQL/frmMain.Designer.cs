namespace PragmaSQL
{
  partial class frmMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.mnuMain = new System.Windows.Forms.MenuStrip();
      this.mnuItemFile = new System.Windows.Forms.ToolStripMenuItem();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
      this.textEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.textDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.webBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemSepRecentFiles = new System.Windows.Forms.ToolStripSeparator();
      this.autoSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemRecentFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemExit = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.objectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuProjectExplorer = new System.Windows.Forms.ToolStripMenuItem();
      this.applicationMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.objectGroupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.searchDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.objectChangeHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.sharedSnippetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sharedScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuItemSavedConnections = new System.Windows.Forms.ToolStripMenuItem();
      this.jumpToWebSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemTools = new System.Windows.Forms.ToolStripMenuItem();
      this.sepOptions = new System.Windows.Forms.ToolStripSeparator();
      this.mnuReloadHighlighters = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
      this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cmbWebSearch = new System.Windows.Forms.ToolStripComboBox();
      this.mnuWebSearch = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuUpgradeProfessional = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.statMsg = new System.Windows.Forms.ToolStripStatusLabel();
      this.bwUpd = new System.ComponentModel.BackgroundWorker();
      this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
      this.kryptonPaletteCustom = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
      this.mnuMain.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuMain
      // 
      this.mnuMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
      this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemFile,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mnuItemTools,
            this.windowToolStripMenuItem,
            this.mnuHelp,
            this.toolStripMenuItem6,
            this.searchToolStripMenuItem,
            this.cmbWebSearch,
            this.mnuWebSearch,
            this.mnuUpgradeProfessional});
      this.mnuMain.Location = new System.Drawing.Point(0, 0);
      this.mnuMain.MdiWindowListItem = this.windowToolStripMenuItem;
      this.mnuMain.Name = "mnuMain";
      this.mnuMain.Size = new System.Drawing.Size(890, 32);
      this.mnuMain.TabIndex = 1;
      this.mnuMain.Text = "Main Menu";
      // 
      // mnuItemFile
      // 
      this.mnuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.openScriptToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.mnuItemSepRecentFiles,
            this.autoSaveToolStripMenuItem,
            this.mnuItemRecentFiles,
            this.mnuItemExit});
      this.mnuItemFile.Name = "mnuItemFile";
      this.mnuItemFile.Size = new System.Drawing.Size(37, 28);
      this.mnuItemFile.Text = "File";
      this.mnuItemFile.DropDownOpening += new System.EventHandler(this.mnuItemFile_DropDownOpening);
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.textEditorToolStripMenuItem,
            this.textDiffToolStripMenuItem,
            this.webBrowserToolStripMenuItem});
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
      this.newToolStripMenuItem.Text = "New";
      // 
      // newScriptToolStripMenuItem1
      // 
      this.newScriptToolStripMenuItem1.Name = "newScriptToolStripMenuItem1";
      this.newScriptToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.newScriptToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
      this.newScriptToolStripMenuItem1.Text = "Script";
      this.newScriptToolStripMenuItem1.Click += new System.EventHandler(this.newScriptToolStripMenuItem1_Click);
      // 
      // toolStripMenuItem5
      // 
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new System.Drawing.Size(144, 6);
      // 
      // textEditorToolStripMenuItem
      // 
      this.textEditorToolStripMenuItem.Name = "textEditorToolStripMenuItem";
      this.textEditorToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
      this.textEditorToolStripMenuItem.Text = "Text";
      this.textEditorToolStripMenuItem.Click += new System.EventHandler(this.offlineEditorToolStripMenuItem_Click);
      // 
      // textDiffToolStripMenuItem
      // 
      this.textDiffToolStripMenuItem.Name = "textDiffToolStripMenuItem";
      this.textDiffToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
      this.textDiffToolStripMenuItem.Text = "Diff";
      this.textDiffToolStripMenuItem.Click += new System.EventHandler(this.textDiffToolStripMenuItem_Click);
      // 
      // webBrowserToolStripMenuItem
      // 
      this.webBrowserToolStripMenuItem.Name = "webBrowserToolStripMenuItem";
      this.webBrowserToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
      this.webBrowserToolStripMenuItem.Text = "Web Browser";
      this.webBrowserToolStripMenuItem.Click += new System.EventHandler(this.webBrowserToolStripMenuItem_Click);
      // 
      // openFileToolStripMenuItem
      // 
      this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
      this.openFileToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
      this.openFileToolStripMenuItem.Text = "Open File";
      this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
      // 
      // openScriptToolStripMenuItem
      // 
      this.openScriptToolStripMenuItem.Name = "openScriptToolStripMenuItem";
      this.openScriptToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
      this.openScriptToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
      this.openScriptToolStripMenuItem.Text = "Open Script";
      this.openScriptToolStripMenuItem.Click += new System.EventHandler(this.openScriptToolStripMenuItem_Click);
      // 
      // openProjectToolStripMenuItem
      // 
      this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
      this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
      this.openProjectToolStripMenuItem.Text = "Open Project";
      this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
      // 
      // mnuItemSepRecentFiles
      // 
      this.mnuItemSepRecentFiles.Name = "mnuItemSepRecentFiles";
      this.mnuItemSepRecentFiles.Size = new System.Drawing.Size(208, 6);
      // 
      // autoSaveToolStripMenuItem
      // 
      this.autoSaveToolStripMenuItem.Name = "autoSaveToolStripMenuItem";
      this.autoSaveToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
      this.autoSaveToolStripMenuItem.Text = "Save Scripts For Recovery";
      this.autoSaveToolStripMenuItem.Click += new System.EventHandler(this.autoSaveToolStripMenuItem_Click);
      // 
      // mnuItemRecentFiles
      // 
      this.mnuItemRecentFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemRecentFile});
      this.mnuItemRecentFiles.Name = "mnuItemRecentFiles";
      this.mnuItemRecentFiles.Size = new System.Drawing.Size(211, 22);
      this.mnuItemRecentFiles.Text = "Recent Files";
      // 
      // mnuItemRecentFile
      // 
      this.mnuItemRecentFile.Name = "mnuItemRecentFile";
      this.mnuItemRecentFile.Size = new System.Drawing.Size(131, 22);
      this.mnuItemRecentFile.Text = "Recent File";
      // 
      // mnuItemExit
      // 
      this.mnuItemExit.Name = "mnuItemExit";
      this.mnuItemExit.Size = new System.Drawing.Size(211, 22);
      this.mnuItemExit.Text = "Exit";
      this.mnuItemExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 28);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectExplorerToolStripMenuItem,
            this.mnuProjectExplorer,
            this.applicationMessagesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.objectGroupingToolStripMenuItem,
            this.searchDatabaseToolStripMenuItem,
            this.objectChangeHToolStripMenuItem,
            this.toolStripMenuItem2,
            this.sharedSnippetsToolStripMenuItem,
            this.sharedScriptToolStripMenuItem,
            this.toolStripMenuItem4,
            this.mnuItemSavedConnections,
            this.jumpToWebSearchToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 28);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // objectExplorerToolStripMenuItem
      // 
      this.objectExplorerToolStripMenuItem.Name = "objectExplorerToolStripMenuItem";
      this.objectExplorerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
      this.objectExplorerToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.objectExplorerToolStripMenuItem.Text = "Object Explorer";
      this.objectExplorerToolStripMenuItem.Click += new System.EventHandler(this.objectExplorerToolStripMenuItem_Click);
      // 
      // mnuProjectExplorer
      // 
      this.mnuProjectExplorer.Name = "mnuProjectExplorer";
      this.mnuProjectExplorer.ShortcutKeys = System.Windows.Forms.Keys.F7;
      this.mnuProjectExplorer.Size = new System.Drawing.Size(232, 22);
      this.mnuProjectExplorer.Text = "Project Explorer";
      this.mnuProjectExplorer.Click += new System.EventHandler(this.localScriptTemplatesToolStripMenuItem_Click);
      // 
      // applicationMessagesToolStripMenuItem
      // 
      this.applicationMessagesToolStripMenuItem.Name = "applicationMessagesToolStripMenuItem";
      this.applicationMessagesToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
      this.applicationMessagesToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.applicationMessagesToolStripMenuItem.Text = "Application Messages";
      this.applicationMessagesToolStripMenuItem.Click += new System.EventHandler(this.applicationMessagesToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(229, 6);
      // 
      // objectGroupingToolStripMenuItem
      // 
      this.objectGroupingToolStripMenuItem.Name = "objectGroupingToolStripMenuItem";
      this.objectGroupingToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.objectGroupingToolStripMenuItem.Text = "Object Grouping";
      this.objectGroupingToolStripMenuItem.Click += new System.EventHandler(this.objectGroupingToolStripMenuItem_Click);
      // 
      // searchDatabaseToolStripMenuItem
      // 
      this.searchDatabaseToolStripMenuItem.Name = "searchDatabaseToolStripMenuItem";
      this.searchDatabaseToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.searchDatabaseToolStripMenuItem.Text = "Search On Db";
      this.searchDatabaseToolStripMenuItem.Click += new System.EventHandler(this.searchDatabaseToolStripMenuItem_Click);
      // 
      // objectChangeHToolStripMenuItem
      // 
      this.objectChangeHToolStripMenuItem.Name = "objectChangeHToolStripMenuItem";
      this.objectChangeHToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.objectChangeHToolStripMenuItem.Text = "Object Change History Viewer";
      this.objectChangeHToolStripMenuItem.Click += new System.EventHandler(this.objectChangeHToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(229, 6);
      // 
      // sharedSnippetsToolStripMenuItem
      // 
      this.sharedSnippetsToolStripMenuItem.Name = "sharedSnippetsToolStripMenuItem";
      this.sharedSnippetsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.sharedSnippetsToolStripMenuItem.Text = "Shared Snippets";
      this.sharedSnippetsToolStripMenuItem.Click += new System.EventHandler(this.sharedSnippetsToolStripMenuItem_Click);
      // 
      // sharedScriptToolStripMenuItem
      // 
      this.sharedScriptToolStripMenuItem.Name = "sharedScriptToolStripMenuItem";
      this.sharedScriptToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.sharedScriptToolStripMenuItem.Text = "Shared Script";
      this.sharedScriptToolStripMenuItem.Click += new System.EventHandler(this.sharedScriptToolStripMenuItem_Click);
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(229, 6);
      // 
      // mnuItemSavedConnections
      // 
      this.mnuItemSavedConnections.Name = "mnuItemSavedConnections";
      this.mnuItemSavedConnections.Size = new System.Drawing.Size(232, 22);
      this.mnuItemSavedConnections.Text = "Saved Connections";
      this.mnuItemSavedConnections.Click += new System.EventHandler(this.dataSourcesRespoitoryToolStripMenuItem_Click);
      // 
      // jumpToWebSearchToolStripMenuItem
      // 
      this.jumpToWebSearchToolStripMenuItem.Name = "jumpToWebSearchToolStripMenuItem";
      this.jumpToWebSearchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
      this.jumpToWebSearchToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
      this.jumpToWebSearchToolStripMenuItem.Text = "Jump To Web Search";
      this.jumpToWebSearchToolStripMenuItem.Click += new System.EventHandler(this.jumpToWebSearchToolStripMenuItem_Click);
      // 
      // mnuItemTools
      // 
      this.mnuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sepOptions,
            this.mnuReloadHighlighters,
            this.mnuOptions});
      this.mnuItemTools.Name = "mnuItemTools";
      this.mnuItemTools.Size = new System.Drawing.Size(48, 28);
      this.mnuItemTools.Text = "Tools";
      // 
      // sepOptions
      // 
      this.sepOptions.Name = "sepOptions";
      this.sepOptions.Size = new System.Drawing.Size(178, 6);
      // 
      // mnuReloadHighlighters
      // 
      this.mnuReloadHighlighters.Name = "mnuReloadHighlighters";
      this.mnuReloadHighlighters.Size = new System.Drawing.Size(181, 22);
      this.mnuReloadHighlighters.Text = "Refresh Highlighters";
      this.mnuReloadHighlighters.Click += new System.EventHandler(this.mnuReloadHighlighters_Click);
      // 
      // mnuOptions
      // 
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new System.Drawing.Size(181, 22);
      this.mnuOptions.Text = "Options...";
      this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
      // 
      // windowToolStripMenuItem
      // 
      this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAllDocumentsToolStripMenuItem});
      this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
      this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 28);
      this.windowToolStripMenuItem.Text = "Window";
      // 
      // closeAllDocumentsToolStripMenuItem
      // 
      this.closeAllDocumentsToolStripMenuItem.Name = "closeAllDocumentsToolStripMenuItem";
      this.closeAllDocumentsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
      this.closeAllDocumentsToolStripMenuItem.Text = "Close All Documents";
      this.closeAllDocumentsToolStripMenuItem.Click += new System.EventHandler(this.closeAllDocumentsToolStripMenuItem_Click);
      // 
      // mnuHelp
      // 
      this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.Size = new System.Drawing.Size(44, 28);
      this.mnuHelp.Text = "Help";
      // 
      // mnuAbout
      // 
      this.mnuAbout.Name = "mnuAbout";
      this.mnuAbout.Size = new System.Drawing.Size(107, 22);
      this.mnuAbout.Text = "About";
      this.mnuAbout.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // toolStripMenuItem6
      // 
      this.toolStripMenuItem6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.toolStripMenuItem6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
      this.toolStripMenuItem6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new System.Drawing.Size(72, 28);
      this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
      // 
      // searchToolStripMenuItem
      // 
      this.searchToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.searchToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.searchToolStripMenuItem.Image = global::PragmaSQL.Properties.Resources.SearchWeb;
      this.searchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
      this.searchToolStripMenuItem.Size = new System.Drawing.Size(28, 28);
      this.searchToolStripMenuItem.Text = "Search";
      this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
      // 
      // cmbWebSearch
      // 
      this.cmbWebSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.cmbWebSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.cmbWebSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbWebSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbWebSearch.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmbWebSearch.Name = "cmbWebSearch";
      this.cmbWebSearch.Size = new System.Drawing.Size(150, 28);
      this.cmbWebSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbWebSearch_KeyUp);
      // 
      // mnuWebSearch
      // 
      this.mnuWebSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.mnuWebSearch.ForeColor = System.Drawing.Color.Blue;
      this.mnuWebSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuWebSearch.Name = "mnuWebSearch";
      this.mnuWebSearch.Size = new System.Drawing.Size(108, 28);
      this.mnuWebSearch.Text = "Web Search Sites";
      // 
      // mnuUpgradeProfessional
      // 
      this.mnuUpgradeProfessional.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.mnuUpgradeProfessional.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.mnuUpgradeProfessional.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
      this.mnuUpgradeProfessional.ForeColor = System.Drawing.Color.Blue;
      this.mnuUpgradeProfessional.Name = "mnuUpgradeProfessional";
      this.mnuUpgradeProfessional.Size = new System.Drawing.Size(171, 28);
      this.mnuUpgradeProfessional.Text = "Upgrade To Professional Edition";
      this.mnuUpgradeProfessional.ToolTipText = "Upgrade To Professional Edition";
      this.mnuUpgradeProfessional.Visible = false;
      this.mnuUpgradeProfessional.Click += new System.EventHandler(this.mnuUpgradeProfessional_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(212, 6);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statMsg});
      this.statusStrip1.Location = new System.Drawing.Point(0, 368);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
      this.statusStrip1.Size = new System.Drawing.Size(890, 22);
      this.statusStrip1.TabIndex = 3;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // statMsg
      // 
      this.statMsg.Name = "statMsg";
      this.statMsg.Size = new System.Drawing.Size(39, 17);
      this.statMsg.Text = "Ready";
      // 
      // kryptonManager1
      // 
      this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.ProfessionalSystem;
      // 
      // kryptonPaletteCustom
      // 
      this.kryptonPaletteCustom.AllowFormChrome = ComponentFactory.Krypton.Toolkit.InheritBool.True;
      this.kryptonPaletteCustom.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
      this.kryptonPaletteCustom.GridStyles.GridCommon.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
      this.kryptonPaletteCustom.GridStyles.GridCustom1.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
      this.kryptonPaletteCustom.GridStyles.GridList.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
      this.kryptonPaletteCustom.GridStyles.GridSheet.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
      // 
      // frmMain
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(890, 390);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.mnuMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.MainMenuStrip = this.mnuMain;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "PragmaSQL";
      this.TextExtra = "";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMain_DragDrop);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmMain_DragOver);
      this.mnuMain.ResumeLayout(false);
      this.mnuMain.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mnuMain;
    private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem objectExplorerToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem mnuProjectExplorer;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSavedConnections;
    private System.Windows.Forms.ToolStripMenuItem mnuItemFile;
    private System.Windows.Forms.ToolStripMenuItem closeAllDocumentsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem searchDatabaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newScriptToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem webBrowserToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem objectGroupingToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem objectChangeHToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sharedSnippetsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sharedScriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem textEditorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemRecentFiles;
		private System.Windows.Forms.ToolStripMenuItem mnuItemRecentFile;
    private System.Windows.Forms.ToolStripMenuItem textDiffToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemTools;
    private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openScriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem applicationMessagesToolStripMenuItem;
    private System.Windows.Forms.ToolStripComboBox cmbWebSearch;
    private System.Windows.Forms.ToolStripMenuItem mnuWebSearch;
    private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem jumpToWebSearchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem mnuItemExit;
		private System.Windows.Forms.ToolStripSeparator mnuItemSepRecentFiles;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel statMsg;
		private System.Windows.Forms.ToolStripMenuItem mnuOptions;
    private System.Windows.Forms.ToolStripSeparator sepOptions;
    private System.ComponentModel.BackgroundWorker bwUpd;
    private System.Windows.Forms.ToolStripMenuItem mnuUpgradeProfessional;
    private System.Windows.Forms.ToolStripMenuItem mnuReloadHighlighters;
    private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
    private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPaletteCustom;
    private System.Windows.Forms.ToolStripMenuItem autoSaveToolStripMenuItem;
  }
}

