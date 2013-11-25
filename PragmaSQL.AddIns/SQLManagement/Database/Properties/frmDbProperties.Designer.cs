namespace SQLManagement
{
  partial class frmDbProperties
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDbProperties));
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.panFiles = new System.Windows.Forms.Panel();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.truncateLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.shrinkDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.detachDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dropDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip2.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(167, 70);
      // 
      // closeToolStripMenuItem
      // 
      this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
      this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeToolStripMenuItem.Text = "Close";
      this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
      // 
      // closeAllToolStripMenuItem
      // 
      this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
      this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllToolStripMenuItem.Text = "Close All";
      this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
      // 
      // closeAllButThisToolStripMenuItem
      // 
      this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
      this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllButThisToolStripMenuItem.Text = "Close All But This";
      this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.panFiles);
      this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
      this.splitContainer1.Size = new System.Drawing.Size(780, 592);
      this.splitContainer1.SplitterDistance = 312;
      this.splitContainer1.TabIndex = 1;
      // 
      // panFiles
      // 
      this.panFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panFiles.Location = new System.Drawing.Point(0, 25);
      this.panFiles.Name = "panFiles";
      this.panFiles.Size = new System.Drawing.Size(780, 287);
      this.panFiles.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripDropDownButton1});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(780, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.Image = global::SQLManagement.Properties.Resources.Refresh;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(65, 22);
      this.toolStripButton5.Text = "Refresh";
      this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.truncateLogsToolStripMenuItem,
            this.shrinkDBToolStripMenuItem,
            this.toolStripMenuItem1,
            this.detachDBToolStripMenuItem,
            this.dropDBToolStripMenuItem});
      this.toolStripDropDownButton1.Image = global::SQLManagement.Properties.Resources.gear;
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(63, 22);
      this.toolStripDropDownButton1.Text = "Tasks";
      // 
      // truncateLogsToolStripMenuItem
      // 
      this.truncateLogsToolStripMenuItem.Name = "truncateLogsToolStripMenuItem";
      this.truncateLogsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.truncateLogsToolStripMenuItem.Text = "Truncate Logs";
      this.truncateLogsToolStripMenuItem.Click += new System.EventHandler(this.truncateLogsToolStripMenuItem_Click);
      // 
      // shrinkDBToolStripMenuItem
      // 
      this.shrinkDBToolStripMenuItem.Name = "shrinkDBToolStripMenuItem";
      this.shrinkDBToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.shrinkDBToolStripMenuItem.Text = "Shrink DB";
      this.shrinkDBToolStripMenuItem.Click += new System.EventHandler(this.shrinkDBToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
      // 
      // detachDBToolStripMenuItem
      // 
      this.detachDBToolStripMenuItem.Name = "detachDBToolStripMenuItem";
      this.detachDBToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.detachDBToolStripMenuItem.Text = "Detach DB";
      this.detachDBToolStripMenuItem.Click += new System.EventHandler(this.detachDBToolStripMenuItem_Click);
      // 
      // dropDBToolStripMenuItem
      // 
      this.dropDBToolStripMenuItem.Name = "dropDBToolStripMenuItem";
      this.dropDBToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
      this.dropDBToolStripMenuItem.Text = "Drop DB";
      this.dropDBToolStripMenuItem.Click += new System.EventHandler(this.dropDBToolStripMenuItem_Click);
      // 
      // frmDbProperties
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(780, 592);
      this.Controls.Add(this.splitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmDbProperties";
      this.TabPageContextMenuStrip = this.contextMenuStrip2;
      this.TabText = "Database Properties";
      this.Text = "Database Properties";
      this.contextMenuStrip2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panFiles;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem truncateLogsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem shrinkDBToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem dropDBToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem detachDBToolStripMenuItem;
  }
}