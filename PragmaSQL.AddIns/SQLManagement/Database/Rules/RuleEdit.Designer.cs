namespace SQLManagement
{
  partial class RuleEdit
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnDrop = new System.Windows.Forms.Button();
      this.btnCreate = new System.Windows.Forms.Button();
      this.btnRename = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cmbOwner = new System.Windows.Forms.ComboBox();
      this.gbDefinition = new System.Windows.Forms.GroupBox();
      this.lblModified = new System.Windows.Forms.Label();
      this.pbModified = new System.Windows.Forms.PictureBox();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.chkBackup = new System.Windows.Forms.CheckBox();
      this.panEditor = new System.Windows.Forms.Panel();
      this.gbDepends = new System.Windows.Forms.GroupBox();
      this.grdDepends = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsDepends = new System.Windows.Forms.BindingSource(this.components);
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.gbDefinition.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).BeginInit();
      this.gbDepends.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdDepends)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDepends)).BeginInit();
      this.SuspendLayout();
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
      this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
      this.splitContainer1.Panel1.Controls.Add(this.gbDefinition);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.gbDepends);
      this.splitContainer1.Size = new System.Drawing.Size(662, 600);
      this.splitContainer1.SplitterDistance = 372;
      this.splitContainer1.TabIndex = 3;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.btnDrop);
      this.groupBox1.Controls.Add(this.btnCreate);
      this.groupBox1.Controls.Add(this.btnRename);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.txtName);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.cmbOwner);
      this.groupBox1.Location = new System.Drawing.Point(2, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(658, 85);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Properties";
      // 
      // btnDrop
      // 
      this.btnDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnDrop.Location = new System.Drawing.Point(366, 46);
      this.btnDrop.Name = "btnDrop";
      this.btnDrop.Size = new System.Drawing.Size(57, 26);
      this.btnDrop.TabIndex = 10;
      this.btnDrop.Text = "Drop";
      this.btnDrop.UseVisualStyleBackColor = true;
      this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
      // 
      // btnCreate
      // 
      this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnCreate.Location = new System.Drawing.Point(425, 46);
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new System.Drawing.Size(57, 26);
      this.btnCreate.TabIndex = 9;
      this.btnCreate.Text = "Create";
      this.btnCreate.UseVisualStyleBackColor = true;
      this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
      // 
      // btnRename
      // 
      this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnRename.Location = new System.Drawing.Point(307, 46);
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(57, 26);
      this.btnRename.TabIndex = 8;
      this.btnRename.Text = "Rename";
      this.btnRename.UseVisualStyleBackColor = true;
      this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 51);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 15);
      this.label2.TabIndex = 7;
      this.label2.Text = "Rule Name";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(73, 48);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(228, 23);
      this.txtName.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(39, 15);
      this.label1.TabIndex = 5;
      this.label1.Text = "Owner";
      // 
      // cmbOwner
      // 
      this.cmbOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbOwner.FormattingEnabled = true;
      this.cmbOwner.Location = new System.Drawing.Point(73, 19);
      this.cmbOwner.Name = "cmbOwner";
      this.cmbOwner.Size = new System.Drawing.Size(228, 23);
      this.cmbOwner.TabIndex = 4;
      // 
      // gbDefinition
      // 
      this.gbDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.gbDefinition.Controls.Add(this.lblModified);
      this.gbDefinition.Controls.Add(this.pbModified);
      this.gbDefinition.Controls.Add(this.btnUpdate);
      this.gbDefinition.Controls.Add(this.chkBackup);
      this.gbDefinition.Controls.Add(this.panEditor);
      this.gbDefinition.Location = new System.Drawing.Point(2, 94);
      this.gbDefinition.Name = "gbDefinition";
      this.gbDefinition.Size = new System.Drawing.Size(658, 276);
      this.gbDefinition.TabIndex = 3;
      this.gbDefinition.TabStop = false;
      this.gbDefinition.Text = "Definition";
      // 
      // lblModified
      // 
      this.lblModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lblModified.AutoSize = true;
      this.lblModified.Location = new System.Drawing.Point(570, 249);
      this.lblModified.Name = "lblModified";
      this.lblModified.Size = new System.Drawing.Size(50, 15);
      this.lblModified.TabIndex = 10;
      this.lblModified.Text = "Modified";
      // 
      // pbModified
      // 
      this.pbModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pbModified.Image = global::SQLManagement.Properties.Resources.EditInformationHS1;
      this.pbModified.Location = new System.Drawing.Point(622, 248);
      this.pbModified.Name = "pbModified";
      this.pbModified.Size = new System.Drawing.Size(16, 16);
      this.pbModified.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbModified.TabIndex = 9;
      this.pbModified.TabStop = false;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnUpdate.Location = new System.Drawing.Point(9, 242);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(145, 28);
      this.btnUpdate.TabIndex = 2;
      this.btnUpdate.Text = "Update  Rule Definition";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // chkBackup
      // 
      this.chkBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkBackup.AutoSize = true;
      this.chkBackup.Location = new System.Drawing.Point(159, 248);
      this.chkBackup.Name = "chkBackup";
      this.chkBackup.Size = new System.Drawing.Size(65, 19);
      this.chkBackup.TabIndex = 1;
      this.chkBackup.Text = "Backup?";
      this.chkBackup.UseVisualStyleBackColor = true;
      // 
      // panEditor
      // 
      this.panEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panEditor.Location = new System.Drawing.Point(9, 23);
      this.panEditor.Name = "panEditor";
      this.panEditor.Size = new System.Drawing.Size(630, 213);
      this.panEditor.TabIndex = 0;
      // 
      // gbDepends
      // 
      this.gbDepends.Controls.Add(this.grdDepends);
      this.gbDepends.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbDepends.Location = new System.Drawing.Point(0, 0);
      this.gbDepends.Name = "gbDepends";
      this.gbDepends.Size = new System.Drawing.Size(662, 224);
      this.gbDepends.TabIndex = 2;
      this.gbDepends.TabStop = false;
      this.gbDepends.Text = "Dependencies";
      // 
      // grdDepends
      // 
      this.grdDepends.AllowUserToAddRows = false;
      this.grdDepends.AllowUserToDeleteRows = false;
      this.grdDepends.AllowUserToResizeRows = false;
      this.grdDepends.AutoGenerateColumns = false;
      this.grdDepends.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grdDepends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdDepends.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
      this.grdDepends.DataSource = this.bsDepends;
      this.grdDepends.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdDepends.Location = new System.Drawing.Point(3, 19);
      this.grdDepends.Name = "grdDepends";
      this.grdDepends.ReadOnly = true;
      this.grdDepends.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grdDepends.RowTemplate.Height = 25;
      this.grdDepends.Size = new System.Drawing.Size(656, 202);
      this.grdDepends.TabIndex = 0;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "id";
      this.Column1.HeaderText = "ID";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Visible = false;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "objName";
      this.Column2.HeaderText = "Object Name";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "type";
      this.Column3.HeaderText = "Type";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      // 
      // RuleEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "RuleEdit";
      this.Size = new System.Drawing.Size(662, 600);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.gbDefinition.ResumeLayout(false);
      this.gbDefinition.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).EndInit();
      this.gbDepends.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdDepends)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDepends)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbOwner;
    private System.Windows.Forms.GroupBox gbDefinition;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.CheckBox chkBackup;
    private System.Windows.Forms.Panel panEditor;
    private System.Windows.Forms.GroupBox gbDepends;
    private System.Windows.Forms.DataGridView grdDepends;
    private System.Windows.Forms.BindingSource bsDepends;
    private System.Windows.Forms.Button btnCreate;
    private System.Windows.Forms.Button btnRename;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.Button btnDrop;
    private System.Windows.Forms.Label lblModified;
    private System.Windows.Forms.PictureBox pbModified;

  }
}
