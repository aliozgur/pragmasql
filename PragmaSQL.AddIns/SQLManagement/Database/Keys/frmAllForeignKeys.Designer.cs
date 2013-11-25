namespace SQLManagement
{
  partial class frmAllForeignKeys
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAllForeignKeys));
      this.panIndexes = new System.Windows.Forms.Panel();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel1 = new System.Windows.Forms.Panel();
      this.grd = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.drepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.bs = new System.Windows.Forms.BindingSource(this.components);
      this.panKeyEdit = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.lblHeader = new System.Windows.Forms.Label();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.panIndexes.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
      this.panKeyEdit.SuspendLayout();
      this.panel3.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panIndexes
      // 
      this.panIndexes.Controls.Add(this.splitter1);
      this.panIndexes.Controls.Add(this.panel1);
      this.panIndexes.Controls.Add(this.panKeyEdit);
      this.panIndexes.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panIndexes.Location = new System.Drawing.Point(0, 25);
      this.panIndexes.Name = "panIndexes";
      this.panIndexes.Size = new System.Drawing.Size(845, 572);
      this.panIndexes.TabIndex = 0;
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter1.Location = new System.Drawing.Point(0, 143);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(845, 3);
      this.splitter1.TabIndex = 1;
      this.splitter1.TabStop = false;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.grd);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(845, 146);
      this.panel1.TabIndex = 0;
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AutoGenerateColumns = false;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
      this.grd.ContextMenuStrip = this.contextMenuStrip1;
      this.grd.DataSource = this.bs;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.Size = new System.Drawing.Size(845, 146);
      this.grd.TabIndex = 0;
      this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "id";
      this.Column1.HeaderText = "ID";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Visible = false;
      // 
      // Column6
      // 
      this.Column6.DataPropertyName = "tableId";
      this.Column6.HeaderText = "Host ID ";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      this.Column6.Visible = false;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "owner";
      this.Column2.HeaderText = "Table Owner";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "tableName";
      this.Column3.HeaderText = "Host Table";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "keyName";
      this.Column4.HeaderText = "Key Name";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      // 
      // Column5
      // 
      this.Column5.DataPropertyName = "status";
      this.Column5.HeaderText = "Status";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifyToolStripMenuItem,
            this.drepToolStripMenuItem,
            this.toolStripMenuItem1,
            this.newToolStripMenuItem,
            this.refreshToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(143, 98);
      // 
      // modifyToolStripMenuItem
      // 
      this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
      this.modifyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
      this.modifyToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.modifyToolStripMenuItem.Text = "Modify";
      this.modifyToolStripMenuItem.Click += new System.EventHandler(this.modifyToolStripMenuItem_Click);
      // 
      // drepToolStripMenuItem
      // 
      this.drepToolStripMenuItem.Name = "drepToolStripMenuItem";
      this.drepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.drepToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.drepToolStripMenuItem.Text = "Drop";
      this.drepToolStripMenuItem.Click += new System.EventHandler(this.drepToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(139, 6);
      // 
      // newToolStripMenuItem
      // 
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.newToolStripMenuItem.Text = "New ";
      this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
      // 
      // refreshToolStripMenuItem
      // 
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.refreshToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
      this.refreshToolStripMenuItem.Text = "Refresh";
      this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
      // 
      // panKeyEdit
      // 
      this.panKeyEdit.Controls.Add(this.panel3);
      this.panKeyEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panKeyEdit.Location = new System.Drawing.Point(0, 146);
      this.panKeyEdit.Name = "panKeyEdit";
      this.panKeyEdit.Size = new System.Drawing.Size(845, 426);
      this.panKeyEdit.TabIndex = 2;
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.panel3.Controls.Add(this.label1);
      this.panel3.Controls.Add(this.lblHeader);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(845, 23);
      this.panel3.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.label1.Dock = System.Windows.Forms.DockStyle.Right;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(818, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(27, 23);
      this.label1.TabIndex = 1;
      this.label1.Text = "X";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.label1.Click += new System.EventHandler(this.label1_Click);
      // 
      // lblHeader
      // 
      this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lblHeader.Location = new System.Drawing.Point(0, 0);
      this.lblHeader.Name = "lblHeader";
      this.lblHeader.Size = new System.Drawing.Size(845, 23);
      this.lblHeader.TabIndex = 0;
      this.lblHeader.Text = "Foreign Key";
      this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton4,
            this.toolStripButton2,
            this.toolStripButton3});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(845, 25);
      this.toolStrip1.TabIndex = 2;
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
      this.toolStripButton4.Image = global::SQLManagement.Properties.Resources.newstyle3;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(48, 22);
      this.toolStripButton4.Text = "New";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.Image = global::SQLManagement.Properties.Resources.editstyle2;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(59, 22);
      this.toolStripButton2.Text = "Modify";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.Image = global::SQLManagement.Properties.Resources.delete1;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(50, 22);
      this.toolStripButton3.Text = "Drop";
      this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
      // 
      // frmAllForeignKeys
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(845, 597);
      this.Controls.Add(this.panIndexes);
      this.Controls.Add(this.toolStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmAllForeignKeys";
      this.ShowInTaskbar = false;
      this.TabPageContextMenuStrip = this.contextMenuStrip2;
      this.TabText = "Foreign Keys";
      this.Text = "Foreign Keys";
      this.panIndexes.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
      this.panKeyEdit.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.contextMenuStrip2.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panIndexes;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem drepToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panKeyEdit;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblHeader;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.BindingSource bs;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
  }
}