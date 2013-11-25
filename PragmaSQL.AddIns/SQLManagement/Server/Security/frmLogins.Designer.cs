namespace SQLManagement
{
  partial class frmLogins
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogins));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuStripLoginList = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.toolStrip1.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      this.mnuStripLoginList.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton4,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton5});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(831, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = global::SQLManagement.Properties.Resources.Refresh;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(65, 22);
      this.toolStripButton1.Text = "Refresh";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.Image = global::SQLManagement.Properties.Resources.UserAdd;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(48, 22);
      this.toolStripButton4.Text = "New";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.Image = global::SQLManagement.Properties.Resources.UserEdit;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(59, 22);
      this.toolStripButton2.Text = "Modify";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.Image = global::SQLManagement.Properties.Resources.UserDelete;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(50, 22);
      this.toolStripButton3.Text = "Drop";
      this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshToolStripMenuItem1});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(167, 98);
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
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
      // 
      // refreshToolStripMenuItem1
      // 
      this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
      this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
      this.refreshToolStripMenuItem1.Text = "Refresh";
      this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
      // 
      // mnuStripLoginList
      // 
      this.mnuStripLoginList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.modifyToolStripMenuItem,
            this.dropToolStripMenuItem,
            this.toolStripMenuItem2,
            this.refreshToolStripMenuItem});
      this.mnuStripLoginList.Name = "contextMenuStrip1";
      this.mnuStripLoginList.Size = new System.Drawing.Size(175, 98);
      // 
      // addToolStripMenuItem
      // 
      this.addToolStripMenuItem.Name = "addToolStripMenuItem";
      this.addToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.addToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.addToolStripMenuItem.Text = "New Login";
      this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
      // 
      // modifyToolStripMenuItem
      // 
      this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
      this.modifyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
      this.modifyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.modifyToolStripMenuItem.Text = "Modify";
      this.modifyToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
      // 
      // dropToolStripMenuItem
      // 
      this.dropToolStripMenuItem.Name = "dropToolStripMenuItem";
      this.dropToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.dropToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.dropToolStripMenuItem.Text = "Drop Selected";
      this.dropToolStripMenuItem.Click += new System.EventHandler(this.dropToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 6);
      // 
      // refreshToolStripMenuItem
      // 
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.refreshToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
      this.refreshToolStripMenuItem.Text = "Refresh";
      this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.Image = global::SQLManagement.Properties.Resources.PrimaryKeyHS;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(87, 22);
      this.toolStripButton5.Text = "Change Pwd";
      this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
      // 
      // frmLogins
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(831, 452);
      this.Controls.Add(this.toolStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmLogins";
      this.ShowInTaskbar = false;
      this.TabPageContextMenuStrip = this.contextMenuStrip2;
      this.TabText = "Logins";
      this.Text = "Logins";
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.contextMenuStrip2.ResumeLayout(false);
      this.mnuStripLoginList.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
    private System.Windows.Forms.ContextMenuStrip mnuStripLoginList;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem dropToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
  }
}