namespace PragmaSQL
{
  partial class frmProjectExplorer
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectExplorer));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.mnuNewProject = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuOpenProject = new System.Windows.Forms.ToolStripButton();
      this.mnuCloseProject = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuRefreshNode = new System.Windows.Forms.ToolStripButton();
      this.mnuRename = new System.Windows.Forms.ToolStripButton();
      this.mnuModifyConnectionSpec = new System.Windows.Forms.ToolStripButton();
      this.mnuDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.mnuConnectionFromList = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuNewFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuNewScript = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuNewText = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAddFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuAddObjectFromDatabase = new System.Windows.Forms.ToolStripMenuItem();
      this.tv = new MWControls.MWTreeView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lblProjectFileInfo = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewProject,
            this.toolStripSeparator1,
            this.mnuOpenProject,
            this.mnuCloseProject,
            this.toolStripSeparator2,
            this.mnuRefreshNode,
            this.mnuRename,
            this.mnuModifyConnectionSpec,
            this.mnuDelete,
            this.toolStripDropDownButton1});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(270, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // mnuNewProject
      // 
      this.mnuNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mnuNewProject.Image = global::PragmaSQL.Properties.Resources.NewWindow;
      this.mnuNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuNewProject.Name = "mnuNewProject";
      this.mnuNewProject.Size = new System.Drawing.Size(23, 22);
      this.mnuNewProject.Text = "New Project";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // mnuOpenProject
      // 
      this.mnuOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mnuOpenProject.Image = global::PragmaSQL.Properties.Resources.open;
      this.mnuOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuOpenProject.Name = "mnuOpenProject";
      this.mnuOpenProject.Size = new System.Drawing.Size(23, 22);
      this.mnuOpenProject.Text = "Open Project";
      // 
      // mnuCloseProject
      // 
      this.mnuCloseProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mnuCloseProject.Image = global::PragmaSQL.Properties.Resources.Exit;
      this.mnuCloseProject.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuCloseProject.Name = "mnuCloseProject";
      this.mnuCloseProject.Size = new System.Drawing.Size(23, 22);
      this.mnuCloseProject.Text = "Close Project";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // mnuRefreshNode
      // 
      this.mnuRefreshNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mnuRefreshNode.Image = global::PragmaSQL.Properties.Resources.Refresh;
      this.mnuRefreshNode.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuRefreshNode.Name = "mnuRefreshNode";
      this.mnuRefreshNode.Size = new System.Drawing.Size(23, 22);
      this.mnuRefreshNode.Text = "Refresh";
      // 
      // mnuRename
      // 
      this.mnuRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mnuRename.Image = global::PragmaSQL.Properties.Resources.RenameFolder;
      this.mnuRename.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuRename.Name = "mnuRename";
      this.mnuRename.Size = new System.Drawing.Size(23, 22);
      this.mnuRename.Text = "Rename";
      // 
      // mnuModifyConnectionSpec
      // 
      this.mnuModifyConnectionSpec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mnuModifyConnectionSpec.Image = global::PragmaSQL.Properties.Resources.db_edit;
      this.mnuModifyConnectionSpec.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuModifyConnectionSpec.Name = "mnuModifyConnectionSpec";
      this.mnuModifyConnectionSpec.Size = new System.Drawing.Size(23, 22);
      this.mnuModifyConnectionSpec.Text = "Modify Connection";
      // 
      // mnuDelete
      // 
      this.mnuDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.mnuDelete.Image = global::PragmaSQL.Properties.Resources.delete;
      this.mnuDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.mnuDelete.Name = "mnuDelete";
      this.mnuDelete.Size = new System.Drawing.Size(23, 22);
      this.mnuDelete.Text = "Delete Selected";
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConnectionFromList,
            this.mnuNewConnection,
            this.toolStripMenuItem1,
            this.mnuNewFolder,
            this.toolStripMenuItem3,
            this.mnuNewScript,
            this.mnuNewText,
            this.mnuAddFile,
            this.toolStripMenuItem2,
            this.mnuAddObjectFromDatabase});
      this.toolStripDropDownButton1.Image = global::PragmaSQL.Properties.Resources.AddToFavorites;
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(55, 22);
      this.toolStripDropDownButton1.Text = "Add";
      // 
      // mnuConnectionFromList
      // 
      this.mnuConnectionFromList.Name = "mnuConnectionFromList";
      this.mnuConnectionFromList.Size = new System.Drawing.Size(193, 22);
      this.mnuConnectionFromList.Text = "Connection From List";
      // 
      // mnuNewConnection
      // 
      this.mnuNewConnection.Name = "mnuNewConnection";
      this.mnuNewConnection.Size = new System.Drawing.Size(193, 22);
      this.mnuNewConnection.Text = "Connection";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
      // 
      // mnuNewFolder
      // 
      this.mnuNewFolder.Name = "mnuNewFolder";
      this.mnuNewFolder.Size = new System.Drawing.Size(193, 22);
      this.mnuNewFolder.Text = "Folder";
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(190, 6);
      // 
      // mnuNewScript
      // 
      this.mnuNewScript.Name = "mnuNewScript";
      this.mnuNewScript.Size = new System.Drawing.Size(193, 22);
      this.mnuNewScript.Text = "Script";
      // 
      // mnuNewText
      // 
      this.mnuNewText.Name = "mnuNewText";
      this.mnuNewText.Size = new System.Drawing.Size(193, 22);
      this.mnuNewText.Text = "Text";
      // 
      // mnuAddFile
      // 
      this.mnuAddFile.Name = "mnuAddFile";
      this.mnuAddFile.Size = new System.Drawing.Size(193, 22);
      this.mnuAddFile.Text = "File";
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(190, 6);
      // 
      // mnuAddObjectFromDatabase
      // 
      this.mnuAddObjectFromDatabase.Name = "mnuAddObjectFromDatabase";
      this.mnuAddObjectFromDatabase.Size = new System.Drawing.Size(193, 22);
      this.mnuAddObjectFromDatabase.Text = "Object From Database";
      // 
      // tv
      // 
      this.tv.AllowDrop = true;
      this.tv.CheckedNodes = ((System.Collections.Hashtable)(resources.GetObject("tv.CheckedNodes")));
      this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tv.HideSelection = false;
      this.tv.ImageIndex = 0;
      this.tv.ImageList = this.imageList1;
      this.tv.Location = new System.Drawing.Point(0, 25);
      this.tv.MultiSelect = MWCommon.TreeViewMultiSelect.MultiSameBranchAndLevel;
      this.tv.Name = "tv";
      this.tv.SelectedImageIndex = 0;
      this.tv.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("tv.SelNodes")));
      this.tv.ShowNodeToolTips = true;
      this.tv.Size = new System.Drawing.Size(270, 469);
      this.tv.TabIndex = 3;
      this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
      this.tv.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
      this.tv.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tv_ItemDrag);
      this.tv.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_BeforeExpand);
      this.tv.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tv_KeyUp);
      this.tv.DragOver += new System.Windows.Forms.DragEventHandler(this.tv_DragOver);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.imageList1.Images.SetKeyName(0, "");
      this.imageList1.Images.SetKeyName(1, "");
      this.imageList1.Images.SetKeyName(2, "");
      this.imageList1.Images.SetKeyName(3, "");
      this.imageList1.Images.SetKeyName(4, "");
      this.imageList1.Images.SetKeyName(5, "");
      this.imageList1.Images.SetKeyName(6, "");
      this.imageList1.Images.SetKeyName(7, "");
      this.imageList1.Images.SetKeyName(8, "");
      this.imageList1.Images.SetKeyName(9, "");
      this.imageList1.Images.SetKeyName(10, "");
      this.imageList1.Images.SetKeyName(11, "");
      this.imageList1.Images.SetKeyName(12, "");
      this.imageList1.Images.SetKeyName(13, "");
      this.imageList1.Images.SetKeyName(14, "");
      this.imageList1.Images.SetKeyName(15, "");
      this.imageList1.Images.SetKeyName(16, "");
      this.imageList1.Images.SetKeyName(17, "");
      this.imageList1.Images.SetKeyName(18, "");
      this.imageList1.Images.SetKeyName(19, "");
      this.imageList1.Images.SetKeyName(20, "");
      this.imageList1.Images.SetKeyName(21, "");
      this.imageList1.Images.SetKeyName(22, "");
      this.imageList1.Images.SetKeyName(23, "");
      this.imageList1.Images.SetKeyName(24, "");
      this.imageList1.Images.SetKeyName(25, "");
      this.imageList1.Images.SetKeyName(26, "");
      // 
      // folderBrowserDialog1
      // 
      this.folderBrowserDialog1.Description = "Select Project Folder";
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.Filter = "PragmaSQL Project Files|*.sqlprj";
      this.openFileDialog1.Title = "Open PragmaSQL Project";
      // 
      // openFileDialog2
      // 
      this.openFileDialog2.Filter = "All Files|*.*";
      this.openFileDialog2.Multiselect = true;
      this.openFileDialog2.Title = "Add File";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblProjectFileInfo});
      this.statusStrip1.Location = new System.Drawing.Point(0, 494);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.ShowItemToolTips = true;
      this.statusStrip1.Size = new System.Drawing.Size(270, 22);
      this.statusStrip1.TabIndex = 4;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // lblProjectFileInfo
      // 
      this.lblProjectFileInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.lblProjectFileInfo.Name = "lblProjectFileInfo";
      this.lblProjectFileInfo.Size = new System.Drawing.Size(255, 17);
      this.lblProjectFileInfo.Spring = true;
      this.lblProjectFileInfo.Text = "Project File:";
      this.lblProjectFileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // frmProjectExplorer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(270, 516);
      this.Controls.Add(this.tv);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip1);
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmProjectExplorer";
      this.TabText = "Project Explorer";
      this.Text = "Project Explorer";
      this.Load += new System.EventHandler(this.frmProjectExplorer_Load);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProjectExplorer_FormClosed);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton mnuOpenProject;
    private System.Windows.Forms.ToolStripButton mnuRefreshNode;
    private System.Windows.Forms.ToolStripButton mnuNewProject;
    private MWControls.MWTreeView tv;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem mnuNewFolder;
    private System.Windows.Forms.ToolStripMenuItem mnuNewScript;
    private System.Windows.Forms.ToolStripMenuItem mnuAddFile;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.ToolStripButton mnuCloseProject;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem mnuConnectionFromList;
    private System.Windows.Forms.ToolStripMenuItem mnuNewConnection;
    private System.Windows.Forms.ToolStripMenuItem mnuAddObjectFromDatabase;
    private System.Windows.Forms.ToolStripButton mnuRename;
    private System.Windows.Forms.ToolStripButton mnuDelete;
    private System.Windows.Forms.OpenFileDialog openFileDialog2;
    private System.Windows.Forms.ToolStripButton mnuModifyConnectionSpec;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lblProjectFileInfo;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuNewText;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
  }
}