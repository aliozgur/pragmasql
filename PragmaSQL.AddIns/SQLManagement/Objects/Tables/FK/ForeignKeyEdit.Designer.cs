namespace SQLManagement
{
  partial class ForeignKeyEdit
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
			this.btnToggleStatus = new System.Windows.Forms.Button();
			this.btnDrop = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnRename = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbFk = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblDisableNotify = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.chkCascadeUpdate = new System.Windows.Forms.CheckBox();
			this.chkCascadeDelete = new System.Windows.Forms.CheckBox();
			this.chkNotForRep = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbHostTables = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.grd = new System.Windows.Forms.DataGridView();
			this.colHostColumns = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bsCols = new System.Windows.Forms.BindingSource(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.cmbRefPk = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbRefTables = new System.Windows.Forms.ComboBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsCols)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnToggleStatus);
			this.panel1.Controls.Add(this.btnDrop);
			this.panel1.Controls.Add(this.btnNew);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.btnRename);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.cmbFk);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(696, 44);
			this.panel1.TabIndex = 1;
			// 
			// btnToggleStatus
			// 
			this.btnToggleStatus.Location = new System.Drawing.Point(415, 18);
			this.btnToggleStatus.Name = "btnToggleStatus";
			this.btnToggleStatus.Size = new System.Drawing.Size(54, 22);
			this.btnToggleStatus.TabIndex = 20;
			this.btnToggleStatus.Text = "Disable";
			this.btnToggleStatus.UseVisualStyleBackColor = true;
			// 
			// btnDrop
			// 
			this.btnDrop.Location = new System.Drawing.Point(359, 18);
			this.btnDrop.Name = "btnDrop";
			this.btnDrop.Size = new System.Drawing.Size(55, 22);
			this.btnDrop.TabIndex = 19;
			this.btnDrop.Text = "Drop";
			this.btnDrop.UseVisualStyleBackColor = true;
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(542, 18);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(55, 22);
			this.btnNew.TabIndex = 18;
			this.btnNew.Text = "New";
			this.btnNew.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(486, 18);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(55, 22);
			this.btnSave.TabIndex = 17;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// btnRename
			// 
			this.btnRename.Location = new System.Drawing.Point(303, 18);
			this.btnRename.Name = "btnRename";
			this.btnRename.Size = new System.Drawing.Size(55, 22);
			this.btnRename.TabIndex = 16;
			this.btnRename.Text = "Rename";
			this.btnRename.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Foreign Keys";
			// 
			// cmbFk
			// 
			this.cmbFk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFk.FormattingEnabled = true;
			this.cmbFk.Location = new System.Drawing.Point(13, 19);
			this.cmbFk.Name = "cmbFk";
			this.cmbFk.Size = new System.Drawing.Size(284, 21);
			this.cmbFk.TabIndex = 0;
			this.cmbFk.SelectedIndexChanged += new System.EventHandler(this.cmbFk_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblDisableNotify);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtName);
			this.groupBox1.Controls.Add(this.chkCascadeUpdate);
			this.groupBox1.Controls.Add(this.chkCascadeDelete);
			this.groupBox1.Controls.Add(this.chkNotForRep);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.cmbHostTables);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 44);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(696, 165);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Host Specification";
			// 
			// lblDisableNotify
			// 
			this.lblDisableNotify.AutoSize = true;
			this.lblDisableNotify.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblDisableNotify.ForeColor = System.Drawing.Color.Red;
			this.lblDisableNotify.Location = new System.Drawing.Point(259, 36);
			this.lblDisableNotify.Name = "lblDisableNotify";
			this.lblDisableNotify.Size = new System.Drawing.Size(158, 16);
			this.lblDisableNotify.TabIndex = 27;
			this.lblDisableNotify.Text = "Foreign key is disabled!";
			this.lblDisableNotify.Visible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 13);
			this.label4.TabIndex = 24;
			this.label4.Text = "Name (Optional)";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(17, 72);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(233, 20);
			this.txtName.TabIndex = 23;
			this.toolTip1.SetToolTip(this.txtName, "Leave empty for automatic name generation");
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// chkCascadeUpdate
			// 
			this.chkCascadeUpdate.AutoSize = true;
			this.chkCascadeUpdate.Location = new System.Drawing.Point(16, 142);
			this.chkCascadeUpdate.Name = "chkCascadeUpdate";
			this.chkCascadeUpdate.Size = new System.Drawing.Size(121, 17);
			this.chkCascadeUpdate.TabIndex = 22;
			this.chkCascadeUpdate.Text = "Cascade on Update";
			this.chkCascadeUpdate.UseVisualStyleBackColor = true;
			this.chkCascadeUpdate.CheckedChanged += new System.EventHandler(this.chkCascadeUpdate_CheckedChanged);
			// 
			// chkCascadeDelete
			// 
			this.chkCascadeDelete.AutoSize = true;
			this.chkCascadeDelete.Location = new System.Drawing.Point(17, 120);
			this.chkCascadeDelete.Name = "chkCascadeDelete";
			this.chkCascadeDelete.Size = new System.Drawing.Size(117, 17);
			this.chkCascadeDelete.TabIndex = 21;
			this.chkCascadeDelete.Text = "Cascade on Delete";
			this.chkCascadeDelete.UseVisualStyleBackColor = true;
			this.chkCascadeDelete.CheckedChanged += new System.EventHandler(this.chkCascadeDelete_CheckedChanged);
			// 
			// chkNotForRep
			// 
			this.chkNotForRep.AutoSize = true;
			this.chkNotForRep.Location = new System.Drawing.Point(17, 99);
			this.chkNotForRep.Name = "chkNotForRep";
			this.chkNotForRep.Size = new System.Drawing.Size(109, 17);
			this.chkNotForRep.TabIndex = 20;
			this.chkNotForRep.Text = "Not for replication";
			this.chkNotForRep.UseVisualStyleBackColor = true;
			this.chkNotForRep.CheckedChanged += new System.EventHandler(this.chkNotForRep_CheckedChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Host Table";
			// 
			// cmbHostTables
			// 
			this.cmbHostTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHostTables.FormattingEnabled = true;
			this.cmbHostTables.Location = new System.Drawing.Point(17, 34);
			this.cmbHostTables.Name = "cmbHostTables";
			this.cmbHostTables.Size = new System.Drawing.Size(233, 21);
			this.cmbHostTables.TabIndex = 17;
			this.cmbHostTables.SelectedIndexChanged += new System.EventHandler(this.cmbHostTables_SelectedIndexChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.grd);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.cmbRefPk);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.cmbRefTables);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 209);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(696, 124);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Reference Specification";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(268, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Columns";
			// 
			// grd
			// 
			this.grd.AllowUserToAddRows = false;
			this.grd.AllowUserToDeleteRows = false;
			this.grd.AllowUserToResizeRows = false;
			this.grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.grd.AutoGenerateColumns = false;
			this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
			this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHostColumns,
            this.Column2});
			this.grd.DataSource = this.bsCols;
			this.grd.Location = new System.Drawing.Point(270, 32);
			this.grd.Name = "grd";
			this.grd.RowHeadersVisible = false;
			this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.grd.RowTemplate.Height = 25;
			this.grd.Size = new System.Drawing.Size(416, 83);
			this.grd.TabIndex = 23;
			// 
			// colHostColumns
			// 
			this.colHostColumns.DataPropertyName = "hostCol";
			this.colHostColumns.HeaderText = "Host Column";
			this.colHostColumns.Name = "colHostColumns";
			this.colHostColumns.Width = 120;
			// 
			// Column2
			// 
			this.Column2.DataPropertyName = "refCol";
			this.Column2.HeaderText = "Reference Column";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Width = 120;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(115, 13);
			this.label3.TabIndex = 22;
			this.label3.Text = "Reference Primary Key";
			// 
			// cmbRefPk
			// 
			this.cmbRefPk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRefPk.FormattingEnabled = true;
			this.cmbRefPk.Location = new System.Drawing.Point(16, 71);
			this.cmbRefPk.Name = "cmbRefPk";
			this.cmbRefPk.Size = new System.Drawing.Size(233, 21);
			this.cmbRefPk.TabIndex = 21;
			this.cmbRefPk.SelectedIndexChanged += new System.EventHandler(this.cmbRefPk_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(87, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Reference Table";
			// 
			// cmbRefTables
			// 
			this.cmbRefTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRefTables.FormattingEnabled = true;
			this.cmbRefTables.Location = new System.Drawing.Point(17, 32);
			this.cmbRefTables.Name = "cmbRefTables";
			this.cmbRefTables.Size = new System.Drawing.Size(233, 21);
			this.cmbRefTables.TabIndex = 19;
			this.cmbRefTables.SelectedIndexChanged += new System.EventHandler(this.cmbRefTables_SelectedIndexChanged);
			// 
			// ForeignKeyEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel1);
			this.Name = "ForeignKeyEdit";
			this.Size = new System.Drawing.Size(696, 333);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsCols)).EndInit();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnDrop;
    private System.Windows.Forms.Button btnNew;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnRename;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbFk;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkCascadeUpdate;
    private System.Windows.Forms.CheckBox chkCascadeDelete;
    private System.Windows.Forms.CheckBox chkNotForRep;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cmbHostTables;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbRefTables;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cmbRefPk;
    private System.Windows.Forms.BindingSource bsCols;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button btnToggleStatus;
    private System.Windows.Forms.Label lblDisableNotify;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.DataGridViewComboBoxColumn colHostColumns;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
  }
}
