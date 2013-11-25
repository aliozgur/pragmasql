namespace SQLManagement
{
  partial class RoleUsersEdit
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
      this.grd = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsUsers = new System.Windows.Forms.BindingSource(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblModified = new System.Windows.Forms.Label();
      this.pbModified = new System.Windows.Forms.PictureBox();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.chkFilterIn = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsUsers)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).BeginInit();
      this.SuspendLayout();
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
            this.Column2});
      this.grd.DataSource = this.bsUsers;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.Name = "grd";
      this.grd.RowHeadersVisible = false;
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(385, 222);
      this.grd.TabIndex = 15;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "isin";
      this.Column1.FillWeight = 30.45685F;
      this.Column1.HeaderText = "Is In?";
      this.Column1.Name = "Column1";
      this.Column1.Width = 50;
      // 
      // Column2
      // 
      this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Column2.DataPropertyName = "name";
      this.Column2.FillWeight = 169.5432F;
      this.Column2.HeaderText = "User";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.chkFilterIn);
      this.panel1.Controls.Add(this.lblModified);
      this.panel1.Controls.Add(this.pbModified);
      this.panel1.Controls.Add(this.btnUpdate);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 222);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(385, 47);
      this.panel1.TabIndex = 16;
      // 
      // lblModified
      // 
      this.lblModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblModified.AutoSize = true;
      this.lblModified.Location = new System.Drawing.Point(311, 16);
      this.lblModified.Name = "lblModified";
      this.lblModified.Size = new System.Drawing.Size(50, 15);
      this.lblModified.TabIndex = 4;
      this.lblModified.Text = "Modified";
      // 
      // pbModified
      // 
      this.pbModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pbModified.Image = global::SQLManagement.Properties.Resources.EditInformationHS1;
      this.pbModified.Location = new System.Drawing.Point(363, 15);
      this.pbModified.Name = "pbModified";
      this.pbModified.Size = new System.Drawing.Size(16, 16);
      this.pbModified.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbModified.TabIndex = 3;
      this.pbModified.TabStop = false;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Location = new System.Drawing.Point(1, 9);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(91, 28);
      this.btnUpdate.TabIndex = 0;
      this.btnUpdate.Text = "Update Users";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // chkFilterIn
      // 
      this.chkFilterIn.AutoSize = true;
      this.chkFilterIn.Location = new System.Drawing.Point(100, 15);
      this.chkFilterIn.Name = "chkFilterIn";
      this.chkFilterIn.Size = new System.Drawing.Size(113, 19);
      this.chkFilterIn.TabIndex = 5;
      this.chkFilterIn.Text = "Filter Is In = TRUE";
      this.chkFilterIn.UseVisualStyleBackColor = true;
      this.chkFilterIn.CheckedChanged += new System.EventHandler(this.chkFilterIn_CheckedChanged);
      // 
      // RoleUsersEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Controls.Add(this.panel1);
      this.Name = "RoleUsersEdit";
      this.Size = new System.Drawing.Size(385, 269);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsUsers)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.BindingSource bsUsers;
    private System.Windows.Forms.Label lblModified;
    private System.Windows.Forms.PictureBox pbModified;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.CheckBox chkFilterIn;
  }
}
