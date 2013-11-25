namespace SQLManagement
{
  partial class ObjectPrivilegesEdit
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectPrivilegesEdit));
			this.panel3 = new System.Windows.Forms.Panel();
			this.grd = new System.Windows.Forms.DataGridView();
			this.colObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colexecute = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colreferences = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colinsert = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.colupdate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.coldelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.bs = new System.Windows.Forms.BindingSource(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.comboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.btnShowFilter = new System.Windows.Forms.ToolStripDropDownButton();
			this.rbAll = new System.Windows.Forms.ToolStripMenuItem();
			this.rbChecked = new System.Windows.Forms.ToolStripMenuItem();
			this.rbUnchecked = new System.Windows.Forms.ToolStripMenuItem();
			this.btnUpdate = new System.Windows.Forms.ToolStripButton();
			this.btnUpdateAll = new System.Windows.Forms.ToolStripButton();
			this.btnChangePrivileges = new System.Windows.Forms.ToolStripButton();
			this.lblModified = new System.Windows.Forms.ToolStripLabel();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.grd);
			this.panel3.Controls.Add(this.toolStrip1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(754, 428);
			this.panel3.TabIndex = 3;
			// 
			// grd
			// 
			this.grd.AllowUserToAddRows = false;
			this.grd.AllowUserToDeleteRows = false;
			this.grd.AllowUserToResizeRows = false;
			this.grd.AutoGenerateColumns = false;
			this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
			this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colObject,
            this.colselect,
            this.colexecute,
            this.colreferences,
            this.colinsert,
            this.colupdate,
            this.coldelete});
			this.grd.DataSource = this.bs;
			this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd.Location = new System.Drawing.Point(0, 25);
			this.grd.Name = "grd";
			this.grd.RowTemplate.Height = 25;
			this.grd.Size = new System.Drawing.Size(754, 403);
			this.grd.TabIndex = 2;
			// 
			// colObject
			// 
			this.colObject.DataPropertyName = "objName";
			this.colObject.Frozen = true;
			this.colObject.HeaderText = "Object";
			this.colObject.Name = "colObject";
			this.colObject.Width = 200;
			// 
			// colselect
			// 
			this.colselect.DataPropertyName = "canselect";
			this.colselect.HeaderText = "Select";
			this.colselect.Name = "colselect";
			this.colselect.Width = 50;
			// 
			// colexecute
			// 
			this.colexecute.DataPropertyName = "canexecute";
			this.colexecute.HeaderText = "Execute";
			this.colexecute.Name = "colexecute";
			this.colexecute.Width = 50;
			// 
			// colreferences
			// 
			this.colreferences.DataPropertyName = "canreferences";
			this.colreferences.HeaderText = "Refs";
			this.colreferences.Name = "colreferences";
			this.colreferences.Width = 50;
			// 
			// colinsert
			// 
			this.colinsert.DataPropertyName = "caninsert";
			this.colinsert.HeaderText = "Insert";
			this.colinsert.Name = "colinsert";
			this.colinsert.Width = 50;
			// 
			// colupdate
			// 
			this.colupdate.DataPropertyName = "canupdate";
			this.colupdate.HeaderText = "Update";
			this.colupdate.Name = "colupdate";
			this.colupdate.Width = 50;
			// 
			// coldelete
			// 
			this.coldelete.DataPropertyName = "candelete";
			this.coldelete.HeaderText = "Delete";
			this.coldelete.Name = "coldelete";
			this.coldelete.Width = 50;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboBox1,
            this.btnShowFilter,
            this.btnUpdate,
            this.btnUpdateAll,
            this.btnChangePrivileges,
            this.lblModified});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(754, 25);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.comboBox1.Items.AddRange(new object[] {
            "Tables",
            "Views",
            "Procedures",
            "Table Valued Functions",
            "Scalar Valued Functions",
            "Inlined Functions"});
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(140, 25);
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// btnShowFilter
			// 
			this.btnShowFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnShowFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rbAll,
            this.rbChecked,
            this.rbUnchecked});
			this.btnShowFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnShowFilter.Image")));
			this.btnShowFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnShowFilter.Name = "btnShowFilter";
			this.btnShowFilter.Size = new System.Drawing.Size(60, 22);
			this.btnShowFilter.Text = "Show All";
			// 
			// rbAll
			// 
			this.rbAll.Checked = true;
			this.rbAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rbAll.Name = "rbAll";
			this.rbAll.Size = new System.Drawing.Size(152, 22);
			this.rbAll.Tag = "all";
			this.rbAll.Text = "All";
			this.rbAll.Click += new System.EventHandler(this.OnShowFilterClicked);
			// 
			// rbChecked
			// 
			this.rbChecked.Name = "rbChecked";
			this.rbChecked.Size = new System.Drawing.Size(152, 22);
			this.rbChecked.Tag = "checked";
			this.rbChecked.Text = "Checked";
			this.rbChecked.Click += new System.EventHandler(this.OnShowFilterClicked);
			// 
			// rbUnchecked
			// 
			this.rbUnchecked.Name = "rbUnchecked";
			this.rbUnchecked.Size = new System.Drawing.Size(152, 22);
			this.rbUnchecked.Tag = "unchecked";
			this.rbUnchecked.Text = "Unchecked";
			this.rbUnchecked.Click += new System.EventHandler(this.OnShowFilterClicked);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Image = global::SQLManagement.Properties.Resources.save;
			this.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(139, 22);
			this.btnUpdate.Text = "Update Table Privileges";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnUpdateAll
			// 
			this.btnUpdateAll.Image = global::SQLManagement.Properties.Resources.saveall;
			this.btnUpdateAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnUpdateAll.Name = "btnUpdateAll";
			this.btnUpdateAll.Size = new System.Drawing.Size(119, 22);
			this.btnUpdateAll.Text = "Update All Modified";
			this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
			// 
			// btnChangePrivileges
			// 
			this.btnChangePrivileges.Image = global::SQLManagement.Properties.Resources.editstyle2;
			this.btnChangePrivileges.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnChangePrivileges.Name = "btnChangePrivileges";
			this.btnChangePrivileges.Size = new System.Drawing.Size(112, 22);
			this.btnChangePrivileges.Text = "Change Privileges";
			this.btnChangePrivileges.Click += new System.EventHandler(this.btnChangePrivileges_Click);
			// 
			// lblModified
			// 
			this.lblModified.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.lblModified.Image = global::SQLManagement.Properties.Resources.Warning;
			this.lblModified.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.lblModified.Name = "lblModified";
			this.lblModified.Size = new System.Drawing.Size(63, 22);
			this.lblModified.Text = "Modified";
			this.lblModified.Visible = false;
			// 
			// ObjectPrivilegesEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel3);
			this.Name = "ObjectPrivilegesEdit";
			this.Size = new System.Drawing.Size(754, 428);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource bs;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnUpdate;
    private System.Windows.Forms.ToolStripButton btnUpdateAll;
    private System.Windows.Forms.ToolStripButton btnChangePrivileges;
    private System.Windows.Forms.ToolStripLabel lblModified;
    private System.Windows.Forms.ToolStripComboBox comboBox1;
    private System.Windows.Forms.ToolStripDropDownButton btnShowFilter;
    private System.Windows.Forms.ToolStripMenuItem rbAll;
    private System.Windows.Forms.ToolStripMenuItem rbChecked;
    private System.Windows.Forms.ToolStripMenuItem rbUnchecked;
    private System.Windows.Forms.DataGridViewTextBoxColumn colObject;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colselect;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colexecute;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colreferences;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colinsert;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colupdate;
    private System.Windows.Forms.DataGridViewCheckBoxColumn coldelete;

  }
}
