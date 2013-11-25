namespace SQLManagement
{
  partial class frmIndexEdit
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndexEdit));
      this.panIndexEdit = new System.Windows.Forms.Panel();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.drepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip2.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panIndexEdit
      // 
      this.panIndexEdit.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panIndexEdit.Location = new System.Drawing.Point(0, 0);
      this.panIndexEdit.Name = "panIndexEdit";
      this.panIndexEdit.Size = new System.Drawing.Size(691, 520);
      this.panIndexEdit.TabIndex = 0;
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
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifyToolStripMenuItem,
            this.drepToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(143, 76);
      // 
      // modifyToolStripMenuItem
      // 
      this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
      this.modifyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
      this.modifyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.modifyToolStripMenuItem.Text = "Modify";
      // 
      // drepToolStripMenuItem
      // 
      this.drepToolStripMenuItem.Name = "drepToolStripMenuItem";
      this.drepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.drepToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.drepToolStripMenuItem.Text = "Drop";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 6);
      // 
      // refreshToolStripMenuItem
      // 
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.refreshToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.refreshToolStripMenuItem.Text = "Refresh";
      // 
      // frmIndexEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(691, 520);
      this.Controls.Add(this.panIndexEdit);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmIndexEdit";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.TabPageContextMenuStrip = this.contextMenuStrip2;
      this.TabText = "Index Edit";
      this.Text = "Index Edit";
      this.contextMenuStrip2.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panIndexEdit;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem drepToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
  }
}