namespace SQLManagement
{
  partial class CheckEdit
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnDrop = new System.Windows.Forms.Button();
      this.btnToggleState = new System.Windows.Forms.Button();
      this.chkNotForRep = new System.Windows.Forms.CheckBox();
      this.btnCreate = new System.Windows.Forms.Button();
      this.btnRename = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cmbTable = new System.Windows.Forms.ComboBox();
      this.gbDefinition = new System.Windows.Forms.GroupBox();
      this.lblModified = new System.Windows.Forms.Label();
      this.pbModified = new System.Windows.Forms.PictureBox();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.chkBackup = new System.Windows.Forms.CheckBox();
      this.panEditor = new System.Windows.Forms.Panel();
      this.bsDepends = new System.Windows.Forms.BindingSource(this.components);
      this.groupBox1.SuspendLayout();
      this.gbDefinition.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDepends)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.btnDrop);
      this.groupBox1.Controls.Add(this.btnToggleState);
      this.groupBox1.Controls.Add(this.chkNotForRep);
      this.groupBox1.Controls.Add(this.btnCreate);
      this.groupBox1.Controls.Add(this.btnRename);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.txtName);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.cmbTable);
      this.groupBox1.Location = new System.Drawing.Point(1, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(632, 108);
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Properties";
      // 
      // btnDrop
      // 
      this.btnDrop.Location = new System.Drawing.Point(429, 46);
      this.btnDrop.Name = "btnDrop";
      this.btnDrop.Size = new System.Drawing.Size(57, 27);
      this.btnDrop.TabIndex = 12;
      this.btnDrop.Text = "Drop";
      this.btnDrop.UseVisualStyleBackColor = true;
      this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
      // 
      // btnToggleState
      // 
      this.btnToggleState.Location = new System.Drawing.Point(371, 46);
      this.btnToggleState.Name = "btnToggleState";
      this.btnToggleState.Size = new System.Drawing.Size(57, 27);
      this.btnToggleState.TabIndex = 11;
      this.btnToggleState.Text = "Disable";
      this.btnToggleState.UseVisualStyleBackColor = true;
      this.btnToggleState.Click += new System.EventHandler(this.btnToggleState_Click);
      // 
      // chkNotForRep
      // 
      this.chkNotForRep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkNotForRep.AutoSize = true;
      this.chkNotForRep.Location = new System.Drawing.Point(79, 80);
      this.chkNotForRep.Name = "chkNotForRep";
      this.chkNotForRep.Size = new System.Drawing.Size(115, 19);
      this.chkNotForRep.TabIndex = 10;
      this.chkNotForRep.Text = "Not for replication";
      this.chkNotForRep.UseVisualStyleBackColor = true;
      // 
      // btnCreate
      // 
      this.btnCreate.Location = new System.Drawing.Point(487, 46);
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new System.Drawing.Size(57, 27);
      this.btnCreate.TabIndex = 9;
      this.btnCreate.Text = "Create";
      this.btnCreate.UseVisualStyleBackColor = true;
      this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
      // 
      // btnRename
      // 
      this.btnRename.Location = new System.Drawing.Point(313, 46);
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(57, 27);
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
      this.label2.Size = new System.Drawing.Size(68, 15);
      this.label2.TabIndex = 7;
      this.label2.Text = "Check Name";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(79, 48);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(228, 23);
      this.txtName.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 15);
      this.label1.TabIndex = 5;
      this.label1.Text = "Table";
      // 
      // cmbTable
      // 
      this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbTable.FormattingEnabled = true;
      this.cmbTable.Location = new System.Drawing.Point(79, 19);
      this.cmbTable.Name = "cmbTable";
      this.cmbTable.Size = new System.Drawing.Size(228, 23);
      this.cmbTable.TabIndex = 4;
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
      this.gbDefinition.Location = new System.Drawing.Point(1, 113);
      this.gbDefinition.Name = "gbDefinition";
      this.gbDefinition.Size = new System.Drawing.Size(632, 346);
      this.gbDefinition.TabIndex = 5;
      this.gbDefinition.TabStop = false;
      this.gbDefinition.Text = "Definition";
      // 
      // lblModified
      // 
      this.lblModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lblModified.AutoSize = true;
      this.lblModified.Location = new System.Drawing.Point(543, 318);
      this.lblModified.Name = "lblModified";
      this.lblModified.Size = new System.Drawing.Size(50, 15);
      this.lblModified.TabIndex = 8;
      this.lblModified.Text = "Modified";
      // 
      // pbModified
      // 
      this.pbModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.pbModified.Image = global::SQLManagement.Properties.Resources.EditInformationHS1;
      this.pbModified.Location = new System.Drawing.Point(595, 317);
      this.pbModified.Name = "pbModified";
      this.pbModified.Size = new System.Drawing.Size(16, 16);
      this.pbModified.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbModified.TabIndex = 7;
      this.pbModified.TabStop = false;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnUpdate.Location = new System.Drawing.Point(9, 312);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(145, 28);
      this.btnUpdate.TabIndex = 2;
      this.btnUpdate.Text = "Update  Check Constraint";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // chkBackup
      // 
      this.chkBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkBackup.AutoSize = true;
      this.chkBackup.Checked = true;
      this.chkBackup.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkBackup.Location = new System.Drawing.Point(159, 318);
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
      this.panEditor.Size = new System.Drawing.Size(604, 283);
      this.panEditor.TabIndex = 0;
      // 
      // CheckEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.gbDefinition);
      this.Name = "CheckEdit";
      this.Size = new System.Drawing.Size(636, 465);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.gbDefinition.ResumeLayout(false);
      this.gbDefinition.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDepends)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource bsDepends;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnCreate;
    private System.Windows.Forms.Button btnRename;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbTable;
    private System.Windows.Forms.GroupBox gbDefinition;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.CheckBox chkBackup;
    private System.Windows.Forms.Panel panEditor;
    private System.Windows.Forms.CheckBox chkNotForRep;
    private System.Windows.Forms.Button btnToggleState;
    private System.Windows.Forms.Button btnDrop;
    private System.Windows.Forms.Label lblModified;
    private System.Windows.Forms.PictureBox pbModified;

  }
}
