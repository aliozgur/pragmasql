namespace PragmaSQL.GUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newScriptFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dataSourcesRespoitoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.objectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.sharedRepositoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sharedScriptTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.fileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.localScriptTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.searchDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(756, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptToolStripMenuItem,
            this.newScriptFromFileToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // newScriptToolStripMenuItem
      // 
      this.newScriptToolStripMenuItem.Name = "newScriptToolStripMenuItem";
      this.newScriptToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.newScriptToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
      this.newScriptToolStripMenuItem.Text = "New Script";
      this.newScriptToolStripMenuItem.Click += new System.EventHandler(this.newScriptToolStripMenuItem_Click);
      // 
      // newScriptFromFileToolStripMenuItem
      // 
      this.newScriptFromFileToolStripMenuItem.Name = "newScriptFromFileToolStripMenuItem";
      this.newScriptFromFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                  | System.Windows.Forms.Keys.O)));
      this.newScriptFromFileToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
      this.newScriptFromFileToolStripMenuItem.Text = "New Script From File";
      this.newScriptFromFileToolStripMenuItem.Click += new System.EventHandler(this.newScriptFromFileToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.editToolStripMenuItem.Text = "Edit";
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataSourcesRespoitoryToolStripMenuItem,
            this.objectExplorerToolStripMenuItem,
            this.searchDatabaseToolStripMenuItem,
            this.toolStripMenuItem2,
            this.sharedRepositoryToolStripMenuItem,
            this.sharedScriptTemplatesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.fileExplorerToolStripMenuItem,
            this.localScriptTemplatesToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // dataSourcesRespoitoryToolStripMenuItem
      // 
      this.dataSourcesRespoitoryToolStripMenuItem.Name = "dataSourcesRespoitoryToolStripMenuItem";
      this.dataSourcesRespoitoryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.dataSourcesRespoitoryToolStripMenuItem.Text = "Data Sources Respoitory";
      this.dataSourcesRespoitoryToolStripMenuItem.Click += new System.EventHandler(this.dataSourcesRespoitoryToolStripMenuItem_Click);
      // 
      // objectExplorerToolStripMenuItem
      // 
      this.objectExplorerToolStripMenuItem.Name = "objectExplorerToolStripMenuItem";
      this.objectExplorerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.objectExplorerToolStripMenuItem.Text = "Object Explorer";
      this.objectExplorerToolStripMenuItem.Click += new System.EventHandler(this.objectExplorerToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(201, 6);
      // 
      // sharedRepositoryToolStripMenuItem
      // 
      this.sharedRepositoryToolStripMenuItem.Name = "sharedRepositoryToolStripMenuItem";
      this.sharedRepositoryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.sharedRepositoryToolStripMenuItem.Text = "Shared Repository";
      this.sharedRepositoryToolStripMenuItem.Click += new System.EventHandler(this.sharedRepositoryToolStripMenuItem_Click);
      // 
      // sharedScriptTemplatesToolStripMenuItem
      // 
      this.sharedScriptTemplatesToolStripMenuItem.Name = "sharedScriptTemplatesToolStripMenuItem";
      this.sharedScriptTemplatesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.sharedScriptTemplatesToolStripMenuItem.Text = "Shared Script Templates";
      this.sharedScriptTemplatesToolStripMenuItem.Click += new System.EventHandler(this.sharedScriptTemplatesToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(201, 6);
      // 
      // fileExplorerToolStripMenuItem
      // 
      this.fileExplorerToolStripMenuItem.Name = "fileExplorerToolStripMenuItem";
      this.fileExplorerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.fileExplorerToolStripMenuItem.Text = "File Explorer";
      this.fileExplorerToolStripMenuItem.Click += new System.EventHandler(this.fileExplorerToolStripMenuItem_Click);
      // 
      // localScriptTemplatesToolStripMenuItem
      // 
      this.localScriptTemplatesToolStripMenuItem.Name = "localScriptTemplatesToolStripMenuItem";
      this.localScriptTemplatesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.localScriptTemplatesToolStripMenuItem.Text = "Local Script Templates";
      this.localScriptTemplatesToolStripMenuItem.Click += new System.EventHandler(this.localScriptTemplatesToolStripMenuItem_Click);
      // 
      // windowToolStripMenuItem
      // 
      this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAllDocumentsToolStripMenuItem});
      this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
      this.windowToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
      this.windowToolStripMenuItem.Text = "Window";
      // 
      // closeAllDocumentsToolStripMenuItem
      // 
      this.closeAllDocumentsToolStripMenuItem.Name = "closeAllDocumentsToolStripMenuItem";
      this.closeAllDocumentsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
      this.closeAllDocumentsToolStripMenuItem.Text = "Close All Documents";
      this.closeAllDocumentsToolStripMenuItem.Click += new System.EventHandler(this.closeAllDocumentsToolStripMenuItem_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
      this.aboutToolStripMenuItem.Text = "About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(212, 6);
      // 
      // searchDatabaseToolStripMenuItem
      // 
      this.searchDatabaseToolStripMenuItem.Name = "searchDatabaseToolStripMenuItem";
      this.searchDatabaseToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
      this.searchDatabaseToolStripMenuItem.Text = "Search Database";
      this.searchDatabaseToolStripMenuItem.Click += new System.EventHandler(this.searchDatabaseToolStripMenuItem_Click);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(756, 431);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.IsMdiContainer = true;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "frmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "PragmaSQL";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem objectExplorerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fileExplorerToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem sharedRepositoryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sharedScriptTemplatesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem localScriptTemplatesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem dataSourcesRespoitoryToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newScriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newScriptFromFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllDocumentsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem searchDatabaseToolStripMenuItem;
  }
}

