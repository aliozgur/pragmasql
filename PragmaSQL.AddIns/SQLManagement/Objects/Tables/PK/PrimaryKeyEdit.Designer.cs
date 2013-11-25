namespace SQLManagement
{
  partial class PrimaryKeyEdit
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnDrop = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnRename = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbPk = new System.Windows.Forms.ComboBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnRemoveCols = new System.Windows.Forms.Button();
			this.btnAddCols = new System.Windows.Forms.Button();
			this.lbPkCols = new System.Windows.Forms.ListBox();
			this.lbTableCols = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbFileGroup = new System.Windows.Forms.ComboBox();
			this.txtFillFactor = new System.Windows.Forms.MaskedTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chkClustered = new System.Windows.Forms.CheckBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbTables = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnDrop);
			this.panel1.Controls.Add(this.btnNew);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.btnRename);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.cmbPk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(598, 49);
			this.panel1.TabIndex = 0;
			// 
			// btnDrop
			// 
			this.btnDrop.Location = new System.Drawing.Point(359, 19);
			this.btnDrop.Name = "btnDrop";
			this.btnDrop.Size = new System.Drawing.Size(55, 22);
			this.btnDrop.TabIndex = 19;
			this.btnDrop.Text = "Drop";
			this.btnDrop.UseVisualStyleBackColor = true;
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(478, 19);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(55, 22);
			this.btnNew.TabIndex = 18;
			this.btnNew.Text = "New";
			this.btnNew.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(421, 19);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(55, 22);
			this.btnSave.TabIndex = 17;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// btnRename
			// 
			this.btnRename.Location = new System.Drawing.Point(303, 19);
			this.btnRename.Name = "btnRename";
			this.btnRename.Size = new System.Drawing.Size(55, 22);
			this.btnRename.TabIndex = 16;
			this.btnRename.Text = "Rename";
			this.btnRename.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Primary Keys";
			// 
			// cmbPk
			// 
			this.cmbPk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPk.FormattingEnabled = true;
			this.cmbPk.Location = new System.Drawing.Point(13, 21);
			this.cmbPk.Name = "cmbPk";
			this.cmbPk.Size = new System.Drawing.Size(284, 21);
			this.cmbPk.TabIndex = 0;
			this.cmbPk.SelectedIndexChanged += new System.EventHandler(this.cmbPk_SelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 49);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(598, 272);
			this.panel2.TabIndex = 1;
			// 
			// panel4
			// 
			this.panel4.AutoScroll = true;
			this.panel4.Controls.Add(this.label5);
			this.panel4.Controls.Add(this.label4);
			this.panel4.Controls.Add(this.btnRemoveCols);
			this.panel4.Controls.Add(this.btnAddCols);
			this.panel4.Controls.Add(this.lbPkCols);
			this.panel4.Controls.Add(this.lbTableCols);
			this.panel4.Controls.Add(this.label3);
			this.panel4.Controls.Add(this.cmbFileGroup);
			this.panel4.Controls.Add(this.txtFillFactor);
			this.panel4.Controls.Add(this.label2);
			this.panel4.Controls.Add(this.chkClustered);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 68);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(598, 204);
			this.panel4.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(253, 79);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 13);
			this.label5.TabIndex = 23;
			this.label5.Text = "PK Columns";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(60, 79);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 13);
			this.label4.TabIndex = 22;
			this.label4.Text = "Table Columns";
			// 
			// btnRemoveCols
			// 
			this.btnRemoveCols.Location = new System.Drawing.Point(210, 128);
			this.btnRemoveCols.Name = "btnRemoveCols";
			this.btnRemoveCols.Size = new System.Drawing.Size(38, 20);
			this.btnRemoveCols.TabIndex = 21;
			this.btnRemoveCols.Text = "<";
			this.btnRemoveCols.UseVisualStyleBackColor = true;
			this.btnRemoveCols.Click += new System.EventHandler(this.btnRemoveCols_Click);
			// 
			// btnAddCols
			// 
			this.btnAddCols.Location = new System.Drawing.Point(210, 103);
			this.btnAddCols.Name = "btnAddCols";
			this.btnAddCols.Size = new System.Drawing.Size(38, 20);
			this.btnAddCols.TabIndex = 20;
			this.btnAddCols.Text = ">";
			this.btnAddCols.UseVisualStyleBackColor = true;
			this.btnAddCols.Click += new System.EventHandler(this.btnAddCols_Click);
			// 
			// lbPkCols
			// 
			this.lbPkCols.FormattingEnabled = true;
			this.lbPkCols.Location = new System.Drawing.Point(256, 95);
			this.lbPkCols.Name = "lbPkCols";
			this.lbPkCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lbPkCols.Size = new System.Drawing.Size(142, 95);
			this.lbPkCols.TabIndex = 19;
			// 
			// lbTableCols
			// 
			this.lbTableCols.FormattingEnabled = true;
			this.lbTableCols.Location = new System.Drawing.Point(63, 95);
			this.lbTableCols.Name = "lbTableCols";
			this.lbTableCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lbTableCols.Size = new System.Drawing.Size(141, 95);
			this.lbTableCols.TabIndex = 18;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 17;
			this.label3.Text = "File Group";
			// 
			// cmbFileGroup
			// 
			this.cmbFileGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFileGroup.FormattingEnabled = true;
			this.cmbFileGroup.Location = new System.Drawing.Point(67, 53);
			this.cmbFileGroup.Name = "cmbFileGroup";
			this.cmbFileGroup.Size = new System.Drawing.Size(169, 21);
			this.cmbFileGroup.TabIndex = 16;
			this.cmbFileGroup.SelectedIndexChanged += new System.EventHandler(this.cmbFileGroup_SelectedIndexChanged);
			// 
			// txtFillFactor
			// 
			this.txtFillFactor.Location = new System.Drawing.Point(67, 28);
			this.txtFillFactor.Mask = "000";
			this.txtFillFactor.Name = "txtFillFactor";
			this.txtFillFactor.Size = new System.Drawing.Size(58, 20);
			this.txtFillFactor.TabIndex = 15;
			this.txtFillFactor.Text = "0";
			this.txtFillFactor.TextChanged += new System.EventHandler(this.txtFillFactor_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Fill Factor";
			// 
			// chkClustered
			// 
			this.chkClustered.AutoSize = true;
			this.chkClustered.Location = new System.Drawing.Point(67, 7);
			this.chkClustered.Name = "chkClustered";
			this.chkClustered.Size = new System.Drawing.Size(70, 17);
			this.chkClustered.TabIndex = 13;
			this.chkClustered.Text = "Clustered";
			this.chkClustered.UseVisualStyleBackColor = true;
			this.chkClustered.CheckStateChanged += new System.EventHandler(this.chkClustered_CheckStateChanged);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.txtName);
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.label6);
			this.panel3.Controls.Add(this.cmbTables);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(598, 68);
			this.panel3.TabIndex = 0;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(113, 35);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(184, 20);
			this.txtName.TabIndex = 12;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 39);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(93, 13);
			this.label7.TabIndex = 11;
			this.label7.Text = "Primary Key Name";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(34, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Table";
			// 
			// cmbTables
			// 
			this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTables.FormattingEnabled = true;
			this.cmbTables.Location = new System.Drawing.Point(113, 10);
			this.cmbTables.Name = "cmbTables";
			this.cmbTables.Size = new System.Drawing.Size(184, 21);
			this.cmbTables.TabIndex = 9;
			this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
			// 
			// PrimaryKeyEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "PrimaryKeyEdit";
			this.Size = new System.Drawing.Size(598, 321);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.ComboBox cmbPk;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnRemoveCols;
    private System.Windows.Forms.Button btnAddCols;
    private System.Windows.Forms.ListBox lbPkCols;
    private System.Windows.Forms.ListBox lbTableCols;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cmbFileGroup;
    private System.Windows.Forms.MaskedTextBox txtFillFactor;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chkClustered;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cmbTables;
    private System.Windows.Forms.Button btnNew;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnRename;
    private System.Windows.Forms.Button btnDrop;
  }
}
