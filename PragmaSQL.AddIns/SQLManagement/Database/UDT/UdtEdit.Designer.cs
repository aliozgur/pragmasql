namespace SQLManagement
{
  partial class UdtEdit
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cmbRuleBinding = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbDefaultBinding = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.chkAllowNulls = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnNew = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnDrop = new System.Windows.Forms.Button();
      this.btnRename = new System.Windows.Forms.Button();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.grd = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsDepends = new System.Windows.Forms.BindingSource(this.components);
      this.dtSelector = new PragmaSQL.Core.DbDataTypeSelector();
      this.panel1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDepends)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.AutoScroll = true;
      this.panel1.Controls.Add(this.groupBox2);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(655, 229);
      this.panel1.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cmbRuleBinding);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.cmbDefaultBinding);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.chkAllowNulls);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 122);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(655, 104);
      this.groupBox2.TabIndex = 3;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Properties";
      // 
      // cmbRuleBinding
      // 
      this.cmbRuleBinding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbRuleBinding.FormattingEnabled = true;
      this.cmbRuleBinding.Location = new System.Drawing.Point(101, 50);
      this.cmbRuleBinding.Name = "cmbRuleBinding";
      this.cmbRuleBinding.Size = new System.Drawing.Size(181, 23);
      this.cmbRuleBinding.TabIndex = 19;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 53);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(67, 15);
      this.label2.TabIndex = 18;
      this.label2.Text = "Rule Binding";
      // 
      // cmbDefaultBinding
      // 
      this.cmbDefaultBinding.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.cmbDefaultBinding.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbDefaultBinding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbDefaultBinding.FormattingEnabled = true;
      this.cmbDefaultBinding.Location = new System.Drawing.Point(101, 21);
      this.cmbDefaultBinding.Name = "cmbDefaultBinding";
      this.cmbDefaultBinding.Size = new System.Drawing.Size(181, 23);
      this.cmbDefaultBinding.TabIndex = 17;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(15, 24);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(82, 15);
      this.label3.TabIndex = 16;
      this.label3.Text = "Default Binding";
      // 
      // chkAllowNulls
      // 
      this.chkAllowNulls.AutoSize = true;
      this.chkAllowNulls.Location = new System.Drawing.Point(101, 79);
      this.chkAllowNulls.Name = "chkAllowNulls";
      this.chkAllowNulls.Size = new System.Drawing.Size(80, 19);
      this.chkAllowNulls.TabIndex = 0;
      this.chkAllowNulls.Text = "Allow Nulls";
      this.chkAllowNulls.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnNew);
      this.groupBox1.Controls.Add(this.btnSave);
      this.groupBox1.Controls.Add(this.btnDrop);
      this.groupBox1.Controls.Add(this.btnRename);
      this.groupBox1.Controls.Add(this.dtSelector);
      this.groupBox1.Controls.Add(this.txtName);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(655, 122);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Definition";
      // 
      // btnNew
      // 
      this.btnNew.Location = new System.Drawing.Point(574, 39);
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(62, 24);
      this.btnNew.TabIndex = 6;
      this.btnNew.Text = "New";
      this.btnNew.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(506, 39);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(62, 24);
      this.btnSave.TabIndex = 5;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnDrop
      // 
      this.btnDrop.Location = new System.Drawing.Point(438, 39);
      this.btnDrop.Name = "btnDrop";
      this.btnDrop.Size = new System.Drawing.Size(62, 24);
      this.btnDrop.TabIndex = 4;
      this.btnDrop.Text = "Drop";
      this.btnDrop.UseVisualStyleBackColor = true;
      // 
      // btnRename
      // 
      this.btnRename.Location = new System.Drawing.Point(370, 39);
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(62, 24);
      this.btnRename.TabIndex = 3;
      this.btnRename.Text = "Rename";
      this.btnRename.UseVisualStyleBackColor = true;
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(16, 39);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(322, 23);
      this.txtName.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(36, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Name";
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter1.Location = new System.Drawing.Point(0, 229);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(655, 3);
      this.splitter1.TabIndex = 1;
      this.splitter1.TabStop = false;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.grd);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(0, 232);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(655, 253);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Dependent Columns";
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
            this.Column1,
            this.Column3,
            this.Column2});
      this.grd.DataSource = this.bsDepends;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(3, 19);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowHeadersVisible = false;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(649, 231);
      this.grd.TabIndex = 0;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "tableOwner";
      this.Column1.HeaderText = "Table Owner";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "tableName";
      this.Column3.HeaderText = "Table Name";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 160;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "columnName";
      this.Column2.HeaderText = "Column Name";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Width = 160;
      // 
      // dtSelector
      // 
      this.dtSelector.Cp = null;
      this.dtSelector.DTPrecision = 0;
      this.dtSelector.DTScale = 0;
      this.dtSelector.DTWidth = 0;
      this.dtSelector.Location = new System.Drawing.Point(15, 64);
      this.dtSelector.Name = "dtSelector";
      this.dtSelector.SelectedDataType = "";
      this.dtSelector.Size = new System.Drawing.Size(323, 47);
      this.dtSelector.TabIndex = 2;
      // 
      // UdtEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.panel1);
      this.Name = "UdtEdit";
      this.Size = new System.Drawing.Size(655, 485);
      this.panel1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDepends)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ComboBox cmbRuleBinding;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbDefaultBinding;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox chkAllowNulls;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnNew;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnDrop;
    private System.Windows.Forms.Button btnRename;
    private PragmaSQL.Core.DbDataTypeSelector dtSelector;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.BindingSource bsDepends;

  }
}
