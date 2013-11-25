namespace SQLManagement
{
  partial class DbOptions
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.bs = new System.Windows.Forms.BindingSource(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.grd = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.lblModified = new System.Windows.Forms.Label();
      this.pbModified = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblModified);
      this.panel1.Controls.Add(this.pbModified);
      this.panel1.Controls.Add(this.btnUpdate);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 360);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(592, 49);
      this.panel1.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.grd);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(592, 360);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Database Options";
      // 
      // btnUpdate
      // 
      this.btnUpdate.Location = new System.Drawing.Point(4, 10);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(172, 27);
      this.btnUpdate.TabIndex = 0;
      this.btnUpdate.Text = "Update Database Options";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.AutoGenerateColumns = false;
      this.grd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
      this.grd.DataSource = this.bs;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(3, 19);
      this.grd.Name = "grd";
      this.grd.RowHeadersVisible = false;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(586, 338);
      this.grd.TabIndex = 0;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "name";
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
      this.Column1.HeaderText = "Option";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Width = 65;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "isset";
      this.Column2.HeaderText = "Is Set?";
      this.Column2.Name = "Column2";
      this.Column2.Width = 43;
      // 
      // lblModified
      // 
      this.lblModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblModified.AutoSize = true;
      this.lblModified.Location = new System.Drawing.Point(513, 16);
      this.lblModified.Name = "lblModified";
      this.lblModified.Size = new System.Drawing.Size(50, 15);
      this.lblModified.TabIndex = 6;
      this.lblModified.Text = "Modified";
      // 
      // pbModified
      // 
      this.pbModified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.pbModified.Image = global::SQLManagement.Properties.Resources.EditInformationHS1;
      this.pbModified.Location = new System.Drawing.Point(565, 15);
      this.pbModified.Name = "pbModified";
      this.pbModified.Size = new System.Drawing.Size(16, 16);
      this.pbModified.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbModified.TabIndex = 5;
      this.pbModified.TabStop = false;
      // 
      // DbOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.panel1);
      this.Name = "DbOptions";
      this.Size = new System.Drawing.Size(592, 409);
      ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbModified)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource bs;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
    private System.Windows.Forms.Label lblModified;
    private System.Windows.Forms.PictureBox pbModified;
  }
}
