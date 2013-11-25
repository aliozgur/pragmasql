namespace SQLManagement
{
  partial class LoginList
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.grd = new System.Windows.Forms.DataGridView();
      this.colLoginName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colChangePwd = new System.Windows.Forms.DataGridViewButtonColumn();
      this.bs = new System.Windows.Forms.BindingSource(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
      this.SuspendLayout();
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.AutoGenerateColumns = false;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLoginName,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column4,
            this.Column6,
            this.Column5,
            this.colChangePwd});
      this.grd.DataSource = this.bs;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.Size = new System.Drawing.Size(665, 367);
      this.grd.TabIndex = 0;
      this.grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grd_CellPainting);
      this.grd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CellContentClick);
      // 
      // colLoginName
      // 
      this.colLoginName.DataPropertyName = "loginname";
      this.colLoginName.HeaderText = "Login Name";
      this.colLoginName.Name = "colLoginName";
      this.colLoginName.ReadOnly = true;
      this.colLoginName.Width = 89;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "isntname";
      this.Column2.HeaderText = "Type";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Width = 56;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "hasaccess";
      this.Column3.HeaderText = "Access";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.TrueValue = "1";
      this.Column3.Width = 48;
      // 
      // Column7
      // 
      this.Column7.DataPropertyName = "isntuser";
      this.Column7.HeaderText = "NT User";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      this.Column7.Visible = false;
      this.Column7.Width = 72;
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "dbname";
      this.Column4.HeaderText = "Database Name";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.Width = 109;
      // 
      // Column6
      // 
      this.Column6.DataPropertyName = "createdate";
      this.Column6.HeaderText = "Created On";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      this.Column6.Width = 86;
      // 
      // Column5
      // 
      this.Column5.DataPropertyName = "language";
      this.Column5.HeaderText = "Language";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      this.Column5.Width = 80;
      // 
      // colChangePwd
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
      this.colChangePwd.DefaultCellStyle = dataGridViewCellStyle1;
      this.colChangePwd.HeaderText = "";
      this.colChangePwd.Name = "colChangePwd";
      this.colChangePwd.ReadOnly = true;
      this.colChangePwd.Text = "Change Pwd.";
      this.colChangePwd.UseColumnTextForButtonValue = true;
      this.colChangePwd.Width = 80;
      // 
      // LoginList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Name = "LoginList";
      this.Size = new System.Drawing.Size(665, 367);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.BindingSource bs;
    private System.Windows.Forms.DataGridViewTextBoxColumn colLoginName;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewButtonColumn colChangePwd;
  }
}
