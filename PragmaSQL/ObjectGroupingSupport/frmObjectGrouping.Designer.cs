namespace PragmaSQL
{
  partial class frmObjectGrouping
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectGrouping));
			this.popUpTab = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cMnuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.cMnuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.cMnuCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemSaveHelpText = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.objectGroupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemReload = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemShowHelpText = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuItemNewRootFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemNewSubFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuItemRemoveSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.ucObjectGrouping1 = new PragmaSQL.ucObjectGrouping();
			this.popUpTab.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// popUpTab
			// 
			this.popUpTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMnuItemClose,
            this.cMnuCloseAll,
            this.cMnuCloseAllButThis});
			this.popUpTab.Name = "contextMenuTab";
			this.popUpTab.Size = new System.Drawing.Size(167, 70);
			// 
			// cMnuItemClose
			// 
			this.cMnuItemClose.Name = "cMnuItemClose";
			this.cMnuItemClose.Size = new System.Drawing.Size(166, 22);
			this.cMnuItemClose.Text = "Close";
			this.cMnuItemClose.Click += new System.EventHandler(this.cMnuItemClose_Click);
			// 
			// cMnuCloseAll
			// 
			this.cMnuCloseAll.Name = "cMnuCloseAll";
			this.cMnuCloseAll.Size = new System.Drawing.Size(166, 22);
			this.cMnuCloseAll.Text = "Close All";
			this.cMnuCloseAll.Click += new System.EventHandler(this.cMnuCloseAll_Click);
			// 
			// cMnuCloseAllButThis
			// 
			this.cMnuCloseAllButThis.Name = "cMnuCloseAllButThis";
			this.cMnuCloseAllButThis.Size = new System.Drawing.Size(166, 22);
			this.cMnuCloseAllButThis.Text = "Close All But This";
			this.cMnuCloseAllButThis.Click += new System.EventHandler(this.cMnuCloseAllButThis_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.objectGroupingToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(695, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemSaveHelpText});
			this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.fileToolStripMenuItem.MergeIndex = 0;
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// mnuItemSaveHelpText
			// 
			this.mnuItemSaveHelpText.Name = "mnuItemSaveHelpText";
			this.mnuItemSaveHelpText.Size = new System.Drawing.Size(155, 22);
			this.mnuItemSaveHelpText.Text = "Save HelpText";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemCut,
            this.mnuItemCopy,
            this.mnuItemPaste});
			this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.editToolStripMenuItem.MergeIndex = 1;
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// mnuItemCut
			// 
			this.mnuItemCut.Name = "mnuItemCut";
			this.mnuItemCut.Size = new System.Drawing.Size(112, 22);
			this.mnuItemCut.Text = "Cut";
			// 
			// mnuItemCopy
			// 
			this.mnuItemCopy.Name = "mnuItemCopy";
			this.mnuItemCopy.Size = new System.Drawing.Size(112, 22);
			this.mnuItemCopy.Text = "Copy";
			// 
			// mnuItemPaste
			// 
			this.mnuItemPaste.Name = "mnuItemPaste";
			this.mnuItemPaste.Size = new System.Drawing.Size(112, 22);
			this.mnuItemPaste.Text = "Paste";
			// 
			// objectGroupingToolStripMenuItem
			// 
			this.objectGroupingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemReload,
            this.mnuItemRefresh,
            this.mnuItemShowHelpText,
            this.toolStripMenuItem1,
            this.mnuItemNewRootFolder,
            this.mnuItemNewSubFolder,
            this.mnuItemRename,
            this.toolStripMenuItem2,
            this.mnuItemRemoveSelected});
			this.objectGroupingToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.objectGroupingToolStripMenuItem.MergeIndex = 3;
			this.objectGroupingToolStripMenuItem.Name = "objectGroupingToolStripMenuItem";
			this.objectGroupingToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
			this.objectGroupingToolStripMenuItem.Text = "Object Grouping";
			// 
			// mnuItemReload
			// 
			this.mnuItemReload.Name = "mnuItemReload";
			this.mnuItemReload.Size = new System.Drawing.Size(168, 22);
			this.mnuItemReload.Text = "Reload ";
			// 
			// mnuItemRefresh
			// 
			this.mnuItemRefresh.Name = "mnuItemRefresh";
			this.mnuItemRefresh.Size = new System.Drawing.Size(168, 22);
			this.mnuItemRefresh.Text = "Refresh Folder";
			// 
			// mnuItemShowHelpText
			// 
			this.mnuItemShowHelpText.Name = "mnuItemShowHelpText";
			this.mnuItemShowHelpText.Size = new System.Drawing.Size(168, 22);
			this.mnuItemShowHelpText.Text = "Show HelpText";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 6);
			// 
			// mnuItemNewRootFolder
			// 
			this.mnuItemNewRootFolder.Name = "mnuItemNewRootFolder";
			this.mnuItemNewRootFolder.Size = new System.Drawing.Size(168, 22);
			this.mnuItemNewRootFolder.Text = "New Root Folder";
			// 
			// mnuItemNewSubFolder
			// 
			this.mnuItemNewSubFolder.Name = "mnuItemNewSubFolder";
			this.mnuItemNewSubFolder.Size = new System.Drawing.Size(168, 22);
			this.mnuItemNewSubFolder.Text = "New Sub Folder";
			// 
			// mnuItemRename
			// 
			this.mnuItemRename.Name = "mnuItemRename";
			this.mnuItemRename.Size = new System.Drawing.Size(168, 22);
			this.mnuItemRename.Text = "Rename Folder";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
			// 
			// mnuItemRemoveSelected
			// 
			this.mnuItemRemoveSelected.Name = "mnuItemRemoveSelected";
			this.mnuItemRemoveSelected.Size = new System.Drawing.Size(168, 22);
			this.mnuItemRemoveSelected.Text = "Remove Selected";
			// 
			// ucObjectGrouping1
			// 
			this.ucObjectGrouping1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucObjectGrouping1.HelpTextVisible = false;
			this.ucObjectGrouping1.Location = new System.Drawing.Point(0, 24);
			this.ucObjectGrouping1.Name = "ucObjectGrouping1";
			this.ucObjectGrouping1.SelectedNode = null;
			this.ucObjectGrouping1.Size = new System.Drawing.Size(695, 492);
			this.ucObjectGrouping1.TabIndex = 0;
			this.ucObjectGrouping1.ConnectionParamsChanged += new PragmaSQL.ConnectionParamsChangedEventHandler(this.ucObjectGrouping1_ConnectionParamsChanged);
			// 
			// frmObjectGrouping
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(695, 516);
			this.Controls.Add(this.ucObjectGrouping1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmObjectGrouping";
			this.TabPageContextMenuStrip = this.popUpTab;
			this.TabText = "Object Grouping";
			this.Text = "Object Grouping";
			this.popUpTab.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private ucObjectGrouping ucObjectGrouping1;
    private System.Windows.Forms.ContextMenuStrip popUpTab;
    private System.Windows.Forms.ToolStripMenuItem cMnuItemClose;
    private System.Windows.Forms.ToolStripMenuItem cMnuCloseAll;
    private System.Windows.Forms.ToolStripMenuItem cMnuCloseAllButThis;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem objectGroupingToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemReload;
    private System.Windows.Forms.ToolStripMenuItem mnuItemRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mnuItemNewRootFolder;
    private System.Windows.Forms.ToolStripMenuItem mnuItemNewSubFolder;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem mnuItemRemoveSelected;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCut;
    private System.Windows.Forms.ToolStripMenuItem mnuItemCopy;
    private System.Windows.Forms.ToolStripMenuItem mnuItemPaste;
    private System.Windows.Forms.ToolStripMenuItem mnuItemRename;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemSaveHelpText;
    private System.Windows.Forms.ToolStripMenuItem mnuItemShowHelpText;
  }
}