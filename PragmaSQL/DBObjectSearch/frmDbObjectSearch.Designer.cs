namespace PragmaSQL
{
  partial class frmDbObjectSearch
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDbObjectSearch));
      this.grd = new System.Windows.Forms.DataGridView();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuItemModify = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemDiff = new System.Windows.Forms.ToolStripMenuItem();
      this.asSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.asDestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemSperator = new System.Windows.Forms.ToolStripSeparator();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panButtons = new System.Windows.Forms.Panel();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cmbServers = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.cmbDatabases = new System.Windows.Forms.ToolStripComboBox();
      this.btnAddCriteria = new System.Windows.Forms.ToolStripButton();
      this.btnRemoveCriteria = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnStartSearch = new System.Windows.Forms.ToolStripButton();
      this.btnChangeDb = new System.Windows.Forms.ToolStripButton();
      this.btnStopSearch = new System.Windows.Forms.ToolStripButton();
      this.lblProgress = new System.Windows.Forms.ToolStripLabel();
      this._criterias = new PragmaSQL.DBObjectSearchWhereBuilder();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.contextMenuStrip2.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.panButtons.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToOrderColumns = true;
      this.grd.AllowUserToResizeRows = false;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.grd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.ContextMenuStrip = this.contextMenuStrip2;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 283);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(886, 203);
      this.grd.TabIndex = 1;
      this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
      this.grd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grd_KeyDown);
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemModify,
            this.mnuItemOpen,
            this.mnuItemDiff,
            this.mnuItemSperator,
            this.copyToolStripMenuItem,
            this.exportToFileToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip1";
      this.contextMenuStrip2.Size = new System.Drawing.Size(167, 120);
      this.contextMenuStrip2.Opened += new System.EventHandler(this.contextMenuStrip2_Opened);
      this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
      // 
      // mnuItemModify
      // 
      this.mnuItemModify.Name = "mnuItemModify";
      this.mnuItemModify.Size = new System.Drawing.Size(166, 22);
      this.mnuItemModify.Text = "Modify";
      this.mnuItemModify.Click += new System.EventHandler(this.mnuItemModify_Click);
      // 
      // mnuItemOpen
      // 
      this.mnuItemOpen.Name = "mnuItemOpen";
      this.mnuItemOpen.Size = new System.Drawing.Size(166, 22);
      this.mnuItemOpen.Text = "Open";
      this.mnuItemOpen.Click += new System.EventHandler(this.mnuItemOpen_Click);
      // 
      // mnuItemDiff
      // 
      this.mnuItemDiff.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asSourceToolStripMenuItem,
            this.asDestToolStripMenuItem});
      this.mnuItemDiff.Name = "mnuItemDiff";
      this.mnuItemDiff.Size = new System.Drawing.Size(166, 22);
      this.mnuItemDiff.Text = "Diff";
      // 
      // asSourceToolStripMenuItem
      // 
      this.asSourceToolStripMenuItem.Name = "asSourceToolStripMenuItem";
      this.asSourceToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.asSourceToolStripMenuItem.Text = "As Source";
      this.asSourceToolStripMenuItem.Click += new System.EventHandler(this.asSourceToolStripMenuItem_Click);
      // 
      // asDestToolStripMenuItem
      // 
      this.asDestToolStripMenuItem.Name = "asDestToolStripMenuItem";
      this.asDestToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.asDestToolStripMenuItem.Text = "As Dest";
      this.asDestToolStripMenuItem.Click += new System.EventHandler(this.asDestToolStripMenuItem_Click);
      // 
      // mnuItemSperator
      // 
      this.mnuItemSperator.Name = "mnuItemSperator";
      this.mnuItemSperator.Size = new System.Drawing.Size(163, 6);
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
      // 
      // exportToFileToolStripMenuItem
      // 
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.exportToFileToolStripMenuItem.Text = "Export List To File";
      this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(167, 70);
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
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter1.Location = new System.Drawing.Point(0, 279);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(886, 4);
      this.splitter1.TabIndex = 3;
      this.splitter1.TabStop = false;
      // 
      // panButtons
      // 
      this.panButtons.Controls.Add(this.btnOk);
      this.panButtons.Controls.Add(this.btnCancel);
      this.panButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panButtons.Location = new System.Drawing.Point(0, 486);
      this.panButtons.Name = "panButtons";
      this.panButtons.Size = new System.Drawing.Size(886, 33);
      this.panButtons.TabIndex = 4;
      this.panButtons.Visible = false;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(718, 5);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(799, 5);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbServers,
            this.toolStripLabel2,
            this.cmbDatabases,
            this.btnAddCriteria,
            this.btnRemoveCriteria,
            this.toolStripSeparator1,
            this.btnStartSearch,
            this.btnChangeDb,
            this.btnStopSearch,
            this.lblProgress});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(886, 25);
      this.toolStrip1.TabIndex = 5;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
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
      // btnAddCriteria
      // 
      this.btnAddCriteria.Image = global::PragmaSQL.Properties.Resources.add;
      this.btnAddCriteria.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAddCriteria.Name = "btnAddCriteria";
      this.btnAddCriteria.Size = new System.Drawing.Size(90, 22);
      this.btnAddCriteria.Text = "Add Criteria";
      this.btnAddCriteria.Click += new System.EventHandler(this.btnAddCriteria_Click);
      // 
      // btnRemoveCriteria
      // 
      this.btnRemoveCriteria.Image = global::PragmaSQL.Properties.Resources.delete;
      this.btnRemoveCriteria.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRemoveCriteria.Name = "btnRemoveCriteria";
      this.btnRemoveCriteria.Size = new System.Drawing.Size(111, 22);
      this.btnRemoveCriteria.Text = "Remove Criteria";
      this.btnRemoveCriteria.Click += new System.EventHandler(this.btnRemoveCriteria_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnStartSearch
      // 
      this.btnStartSearch.Image = global::PragmaSQL.Properties.Resources.search;
      this.btnStartSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStartSearch.Name = "btnStartSearch";
      this.btnStartSearch.Size = new System.Drawing.Size(62, 22);
      this.btnStartSearch.Text = "Search";
      this.btnStartSearch.Click += new System.EventHandler(this.btnSearch_Click);
      // 
      // btnChangeDb
      // 
      this.btnChangeDb.Image = global::PragmaSQL.Properties.Resources.dbs;
      this.btnChangeDb.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnChangeDb.Name = "btnChangeDb";
      this.btnChangeDb.Size = new System.Drawing.Size(119, 22);
      this.btnChangeDb.Text = "Change Database";
      this.btnChangeDb.Visible = false;
      this.btnChangeDb.Click += new System.EventHandler(this.btnChangeDb_Click);
      // 
      // btnStopSearch
      // 
      this.btnStopSearch.Enabled = false;
      this.btnStopSearch.Image = global::PragmaSQL.Properties.Resources.Stop;
      this.btnStopSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStopSearch.Name = "btnStopSearch";
      this.btnStopSearch.Size = new System.Drawing.Size(51, 22);
      this.btnStopSearch.Text = "Stop";
      this.btnStopSearch.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // lblProgress
      // 
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(0, 22);
      // 
      // _criterias
      // 
      this._criterias.Dock = System.Windows.Forms.DockStyle.Top;
      this._criterias.Location = new System.Drawing.Point(0, 25);
      this._criterias.Name = "_criterias";
      this._criterias.Size = new System.Drawing.Size(886, 254);
      this._criterias.TabIndex = 2;
      // 
      // frmDbObjectSearch
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(886, 519);
      this.Controls.Add(this.grd);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this._criterias);
      this.Controls.Add(this.panButtons);
      this.Controls.Add(this.toolStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmDbObjectSearch";
      this.ShowInTaskbar = false;
      this.TabPageContextMenuStrip = this.contextMenuStrip1;
      this.TabText = "Search On Db";
      this.Text = "Search On Db";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDbObjectSearch_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.contextMenuStrip2.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.panButtons.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private PragmaSQL.DBObjectSearchWhereBuilder _criterias;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem mnuItemModify;
    private System.Windows.Forms.ToolStripMenuItem mnuItemOpen;
    private System.Windows.Forms.ToolStripSeparator mnuItemSperator;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
    private System.Windows.Forms.Panel panButtons;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ToolStripMenuItem mnuItemDiff;
    private System.Windows.Forms.ToolStripMenuItem asSourceToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem asDestToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnAddCriteria;
    private System.Windows.Forms.ToolStripButton btnRemoveCriteria;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnStartSearch;
    private System.Windows.Forms.ToolStripButton btnStopSearch;
    private System.Windows.Forms.ToolStripLabel lblProgress;
		private System.Windows.Forms.ToolStripButton btnChangeDb;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripComboBox cmbServers;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripComboBox cmbDatabases;
  }
}