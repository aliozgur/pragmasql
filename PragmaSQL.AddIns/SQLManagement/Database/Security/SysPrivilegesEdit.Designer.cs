namespace SQLManagement
{
  partial class SysPrivilegesEdit
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
      this.bsPrivileges = new System.Windows.Forms.BindingSource(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblModified = new System.Windows.Forms.Label();
      this.pbModified = new System.Windows.Forms.PictureBox();
      this.btnUpdate = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsPrivileges)).BeginInit();
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
      this.grd.DataSource = this.bsPrivileges;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.Name = "grd";
      this.grd.RowHeadersVisible = false;
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(454, 195);
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
      this.Column2.DataPropertyName = "action";
      this.Column2.FillWeight = 169.5432F;
      this.Column2.HeaderText = "Action";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblModified);
      this.panel1.Controls.Add(this.pbModified);
      this.panel1.Controls.Add(this.btnUpdate);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 195);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(454, 41);
      this.panel1.TabIndex = 16;
      // 
      // lblModified
      // 
      this.lblModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblModified.AutoSize = true;
      this.lblModified.Location = new System.Drawing.Point(380, 14);
      this.lblModified.Name = "lblModified";
      this.lblModified.Size = new System.Drawing.Size(47, 13);
      this.lblModified.TabIndex = 2;
      this.lblModified.Text = "Modified";
      // 
      // pbModified
      // 
      this.pbModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pbModified.Image = global::SQLManagement.Properties.Resources.EditInformationHS1;
      this.pbModified.Location = new System.Drawing.Point(432, 13);
      this.pbModified.Name = "pbModified";
      this.pbModified.Size = new System.Drawing.Size(16, 16);
      this.pbModified.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbModified.TabIndex = 1;
      this.pbModified.TabStop = false;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Location = new System.Drawing.Point(3, 8);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(161, 24);
      this.btnUpdate.TabIndex = 0;
      this.btnUpdate.Text = "Update System Privileges";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // SysPrivilegesEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Controls.Add(this.panel1);
      this.Name = "SysPrivilegesEdit";
      this.Size = new System.Drawing.Size(454, 236);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsPrivileges)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.BindingSource bsPrivileges;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.PictureBox pbModified;
    private System.Windows.Forms.Label lblModified;
  }
}
