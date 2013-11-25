namespace PragmaSQL.GUI
{
  partial class frmConnectionRepository
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectionRepository));
      this.lvConnections = new System.Windows.Forms.ListView();
      this.colName = new System.Windows.Forms.ColumnHeader();
      this.colInitialCat = new System.Windows.Forms.ColumnHeader();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnNew = new System.Windows.Forms.ToolStripButton();
      this.btnEdit = new System.Windows.Forms.ToolStripButton();
      this.btnRemove = new System.Windows.Forms.ToolStripButton();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.btnOk = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lvConnections
      // 
      this.lvConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colInitialCat});
      this.lvConnections.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvConnections.FullRowSelect = true;
      this.lvConnections.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvConnections.HideSelection = false;
      this.lvConnections.Location = new System.Drawing.Point(3, 41);
      this.lvConnections.Name = "lvConnections";
      this.lvConnections.Size = new System.Drawing.Size(353, 179);
      this.lvConnections.TabIndex = 1;
      this.lvConnections.UseCompatibleStateImageBehavior = false;
      this.lvConnections.View = System.Windows.Forms.View.Details;
      this.lvConnections.DoubleClick += new System.EventHandler(this.lvConnections_DoubleClick);
      this.lvConnections.SelectedIndexChanged += new System.EventHandler(this.lvConnections_SelectedIndexChanged);
      // 
      // colName
      // 
      this.colName.Text = "Friendly Name";
      this.colName.Width = 225;
      // 
      // colInitialCat
      // 
      this.colInitialCat.Text = "Init. Cat.";
      this.colInitialCat.Width = 120;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btnCancel.Location = new System.Drawing.Point(278, 236);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(80, 27);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lvConnections);
      this.groupBox1.Controls.Add(this.toolStrip1);
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBox1.Location = new System.Drawing.Point(5, 4);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(359, 223);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select connection from the list";
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnEdit,
            this.btnRemove});
      this.toolStrip1.Location = new System.Drawing.Point(3, 16);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(353, 25);
      this.toolStrip1.TabIndex = 8;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnNew
      // 
      this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNew.Image = global::PragmaSQL.Properties.Resources.db_add;
      this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(23, 22);
      this.btnNew.Text = "Add Data Source";
      this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
      // 
      // btnEdit
      // 
      this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnEdit.Image = global::PragmaSQL.Properties.Resources.db_edit;
      this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(23, 22);
      this.btnEdit.Text = "Edit Data Source";
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // btnRemove
      // 
      this.btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRemove.Image = global::PragmaSQL.Properties.Resources.db_remove;
      this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new System.Drawing.Size(23, 22);
      this.btnRemove.Text = "Remove Data Source";
      this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
      // 
      // imageList1
      // 
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btnOk.Location = new System.Drawing.Point(190, 236);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(80, 27);
      this.btnOk.TabIndex = 2;
      this.btnOk.Text = "OK";
      // 
      // frmConnectionRepository
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(373, 273);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btnOk);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmConnectionRepository";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Saved Connections";
      this.Load += new System.EventHandler(this.frmConnectionRepository_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lvConnections;
    private System.Windows.Forms.ColumnHeader colName;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.ColumnHeader colInitialCat;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnNew;
    private System.Windows.Forms.ToolStripButton btnEdit;
    private System.Windows.Forms.ToolStripButton btnRemove;
  }
}