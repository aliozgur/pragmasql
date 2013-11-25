namespace SQLManagement
{
  partial class ModifyLogin
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
      this.lblLangError = new System.Windows.Forms.Label();
      this.lblDbError = new System.Windows.Forms.Label();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.cmbLanguage = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbDb = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.grd = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsRoles = new System.Windows.Forms.BindingSource(this.components);
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsRoles)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.lblLangError);
      this.groupBox1.Controls.Add(this.lblDbError);
      this.groupBox1.Controls.Add(this.checkBox1);
      this.groupBox1.Controls.Add(this.cmbLanguage);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cmbDb);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.groupBox1.Location = new System.Drawing.Point(4, 2);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(465, 98);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Definition";
      // 
      // lblLangError
      // 
      this.lblLangError.AutoSize = true;
      this.lblLangError.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblLangError.ForeColor = System.Drawing.Color.Red;
      this.lblLangError.Location = new System.Drawing.Point(303, 51);
      this.lblLangError.Name = "lblLangError";
      this.lblLangError.Size = new System.Drawing.Size(150, 13);
      this.lblLangError.TabIndex = 19;
      this.lblLangError.Text = "Language does not exist!";
      this.lblLangError.Visible = false;
      // 
      // lblDbError
      // 
      this.lblDbError.AutoSize = true;
      this.lblDbError.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblDbError.ForeColor = System.Drawing.Color.Red;
      this.lblDbError.Location = new System.Drawing.Point(303, 25);
      this.lblDbError.Name = "lblDbError";
      this.lblDbError.Size = new System.Drawing.Size(149, 13);
      this.lblDbError.TabIndex = 18;
      this.lblDbError.Text = "Database does not exist!";
      this.lblDbError.Visible = false;
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Enabled = false;
      this.checkBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.checkBox1.Location = new System.Drawing.Point(105, 74);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(86, 17);
      this.checkBox1.TabIndex = 17;
      this.checkBox1.Text = "Is logged in?";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // cmbLanguage
      // 
      this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.cmbLanguage.FormattingEnabled = true;
      this.cmbLanguage.Location = new System.Drawing.Point(105, 45);
      this.cmbLanguage.Name = "cmbLanguage";
      this.cmbLanguage.Size = new System.Drawing.Size(192, 21);
      this.cmbLanguage.Sorted = true;
      this.cmbLanguage.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.Location = new System.Drawing.Point(9, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(92, 13);
      this.label2.TabIndex = 16;
      this.label2.Text = "Default Language";
      // 
      // cmbDb
      // 
      this.cmbDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbDb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.cmbDb.FormattingEnabled = true;
      this.cmbDb.Location = new System.Drawing.Point(105, 20);
      this.cmbDb.Name = "cmbDb";
      this.cmbDb.Size = new System.Drawing.Size(192, 21);
      this.cmbDb.Sorted = true;
      this.cmbDb.TabIndex = 13;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.Location = new System.Drawing.Point(9, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(91, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "Default Database";
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
      this.grd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
      this.grd.DataSource = this.bsRoles;
      this.grd.Location = new System.Drawing.Point(4, 128);
      this.grd.Name = "grd";
      this.grd.RowHeadersVisible = false;
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(465, 319);
      this.grd.TabIndex = 14;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "isin";
      this.Column1.FillWeight = 30.45685F;
      this.Column1.HeaderText = "Is In?";
      this.Column1.Name = "Column1";
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "ServerRole";
      this.Column2.FillWeight = 169.5432F;
      this.Column2.HeaderText = "Server Role";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label3.Location = new System.Drawing.Point(3, 109);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(88, 16);
      this.label3.TabIndex = 15;
      this.label3.Text = "Server roles";
      // 
      // ModifyLogin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.label3);
      this.Controls.Add(this.grd);
      this.Controls.Add(this.groupBox1);
      this.Name = "ModifyLogin";
      this.Size = new System.Drawing.Size(474, 452);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsRoles)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.ComboBox cmbLanguage;
    private System.Windows.Forms.ComboBox cmbDb;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.BindingSource bsRoles;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.Label lblDbError;
    private System.Windows.Forms.Label lblLangError;
    private System.Windows.Forms.Label label2;
  }
}
