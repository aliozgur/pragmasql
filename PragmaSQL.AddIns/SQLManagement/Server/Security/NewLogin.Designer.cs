namespace SQLManagement
{
  partial class NewLogin
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
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.refreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tab = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rdSqlServerAuth = new System.Windows.Forms.RadioButton();
      this.rdWindowsAuth = new System.Windows.Forms.RadioButton();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtLoginName = new System.Windows.Forms.TextBox();
      this.txtRePassword = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.cmbLanguage = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbDb = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.label7 = new System.Windows.Forms.Label();
      this.lvRoles = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.label6 = new System.Windows.Forms.Label();
      this.grd = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsDbs = new System.Windows.Forms.BindingSource(this.components);
      this.contextMenuStrip2.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.tab.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDbs)).BeginInit();
      this.SuspendLayout();
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(124, 26);
      // 
      // refreToolStripMenuItem
      // 
      this.refreToolStripMenuItem.Name = "refreToolStripMenuItem";
      this.refreToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
      this.refreToolStripMenuItem.Text = "Refresh";
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem,
            this.toggleToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(140, 98);
      // 
      // checkAllToolStripMenuItem
      // 
      this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
      this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
      this.checkAllToolStripMenuItem.Text = "Check All";
      this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
      // 
      // uncheckAllToolStripMenuItem
      // 
      this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
      this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
      this.uncheckAllToolStripMenuItem.Text = "Uncheck All";
      this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.uncheckAllToolStripMenuItem_Click);
      // 
      // toggleToolStripMenuItem
      // 
      this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
      this.toggleToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
      this.toggleToolStripMenuItem.Text = "Toggle";
      this.toggleToolStripMenuItem.Click += new System.EventHandler(this.toggleToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 6);
      // 
      // refreshToolStripMenuItem
      // 
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
      this.refreshToolStripMenuItem.Text = "Refresh";
      // 
      // tab
      // 
      this.tab.Controls.Add(this.tabPage1);
      this.tab.Controls.Add(this.tabPage2);
      this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tab.Location = new System.Drawing.Point(0, 0);
      this.tab.Name = "tab";
      this.tab.SelectedIndex = 0;
      this.tab.Size = new System.Drawing.Size(527, 515);
      this.tab.TabIndex = 2;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.groupBox1);
      this.tabPage1.Location = new System.Drawing.Point(4, 24);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(519, 487);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "General";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.ContextMenuStrip = this.contextMenuStrip2;
      this.groupBox1.Controls.Add(this.groupBox2);
      this.groupBox1.Controls.Add(this.txtPassword);
      this.groupBox1.Controls.Add(this.txtLoginName);
      this.groupBox1.Controls.Add(this.txtRePassword);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.cmbLanguage);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cmbDb);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(513, 481);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Login Definition";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rdSqlServerAuth);
      this.groupBox2.Controls.Add(this.rdWindowsAuth);
      this.groupBox2.Location = new System.Drawing.Point(19, 22);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(336, 81);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Authentication type";
      // 
      // rdSqlServerAuth
      // 
      this.rdSqlServerAuth.AutoSize = true;
      this.rdSqlServerAuth.Checked = true;
      this.rdSqlServerAuth.Location = new System.Drawing.Point(11, 22);
      this.rdSqlServerAuth.Name = "rdSqlServerAuth";
      this.rdSqlServerAuth.Size = new System.Drawing.Size(145, 19);
      this.rdSqlServerAuth.TabIndex = 19;
      this.rdSqlServerAuth.TabStop = true;
      this.rdSqlServerAuth.Text = "Sql server authentication";
      this.rdSqlServerAuth.UseVisualStyleBackColor = true;
      this.rdSqlServerAuth.CheckedChanged += new System.EventHandler(this.rdSqlServerAuth_CheckedChanged);
      // 
      // rdWindowsAuth
      // 
      this.rdWindowsAuth.AutoSize = true;
      this.rdWindowsAuth.Location = new System.Drawing.Point(11, 47);
      this.rdWindowsAuth.Name = "rdWindowsAuth";
      this.rdWindowsAuth.Size = new System.Drawing.Size(143, 19);
      this.rdWindowsAuth.TabIndex = 18;
      this.rdWindowsAuth.Text = "Windows Authentication";
      this.rdWindowsAuth.UseVisualStyleBackColor = true;
      this.rdWindowsAuth.CheckedChanged += new System.EventHandler(this.rdWindowsAuth_CheckedChanged);
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(144, 147);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(212, 23);
      this.txtPassword.TabIndex = 2;
      // 
      // txtLoginName
      // 
      this.txtLoginName.Location = new System.Drawing.Point(144, 118);
      this.txtLoginName.Name = "txtLoginName";
      this.txtLoginName.Size = new System.Drawing.Size(212, 23);
      this.txtLoginName.TabIndex = 1;
      // 
      // txtRePassword
      // 
      this.txtRePassword.Location = new System.Drawing.Point(144, 176);
      this.txtRePassword.Name = "txtRePassword";
      this.txtRePassword.PasswordChar = '*';
      this.txtRePassword.Size = new System.Drawing.Size(212, 23);
      this.txtRePassword.TabIndex = 3;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(19, 150);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(53, 15);
      this.label4.TabIndex = 11;
      this.label4.Text = "Password:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(19, 121);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(67, 15);
      this.label3.TabIndex = 10;
      this.label3.Text = "Login Name:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(19, 179);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(89, 15);
      this.label5.TabIndex = 12;
      this.label5.Text = "Retype Password:";
      // 
      // cmbLanguage
      // 
      this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbLanguage.FormattingEnabled = true;
      this.cmbLanguage.Location = new System.Drawing.Point(144, 258);
      this.cmbLanguage.Name = "cmbLanguage";
      this.cmbLanguage.Size = new System.Drawing.Size(212, 23);
      this.cmbLanguage.Sorted = true;
      this.cmbLanguage.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(19, 261);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(93, 15);
      this.label2.TabIndex = 8;
      this.label2.Text = "Default Language";
      // 
      // cmbDb
      // 
      this.cmbDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbDb.FormattingEnabled = true;
      this.cmbDb.Location = new System.Drawing.Point(144, 229);
      this.cmbDb.Name = "cmbDb";
      this.cmbDb.Size = new System.Drawing.Size(212, 23);
      this.cmbDb.Sorted = true;
      this.cmbDb.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(19, 232);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 15);
      this.label1.TabIndex = 6;
      this.label1.Text = "Default Database";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.label7);
      this.tabPage2.Controls.Add(this.lvRoles);
      this.tabPage2.Controls.Add(this.label6);
      this.tabPage2.Controls.Add(this.grd);
      this.tabPage2.Location = new System.Drawing.Point(4, 24);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(519, 487);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "User Mapping";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(9, 223);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(155, 15);
      this.label7.TabIndex = 3;
      this.label7.Text = "Database role membership for:";
      // 
      // lvRoles
      // 
      this.lvRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvRoles.CheckBoxes = true;
      this.lvRoles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.lvRoles.ContextMenuStrip = this.contextMenuStrip1;
      this.lvRoles.FullRowSelect = true;
      this.lvRoles.GridLines = true;
      this.lvRoles.Location = new System.Drawing.Point(12, 241);
      this.lvRoles.Name = "lvRoles";
      this.lvRoles.Size = new System.Drawing.Size(494, 224);
      this.lvRoles.TabIndex = 2;
      this.lvRoles.UseCompatibleStateImageBehavior = false;
      this.lvRoles.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "";
      this.columnHeader1.Width = 406;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(9, 16);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(137, 15);
      this.label6.TabIndex = 1;
      this.label6.Text = "Users mapped to this login:";
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.grd.AutoGenerateColumns = false;
      this.grd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
      this.grd.DataSource = this.bsDbs;
      this.grd.Location = new System.Drawing.Point(12, 36);
      this.grd.Name = "grd";
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(494, 175);
      this.grd.TabIndex = 0;
      this.grd.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CellValueChanged);
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "Map";
      this.Column1.HeaderText = "Map";
      this.Column1.Name = "Column1";
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "Database";
      this.Column2.HeaderText = "Database";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "User";
      this.Column3.HeaderText = "User";
      this.Column3.Name = "Column3";
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "DefaultSchema";
      this.Column4.HeaderText = "Default Schema";
      this.Column4.Name = "Column4";
      // 
      // bsDbs
      // 
      this.bsDbs.CurrentChanged += new System.EventHandler(this.bsDbs_CurrentChanged);
      this.bsDbs.CurrentItemChanged += new System.EventHandler(this.bsDbs_CurrentItemChanged);
      // 
      // NewLogin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tab);
      this.Name = "NewLogin";
      this.Size = new System.Drawing.Size(527, 515);
      this.contextMenuStrip2.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.tab.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDbs)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem refreToolStripMenuItem;
    private System.Windows.Forms.TabControl tab;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtLoginName;
    private System.Windows.Forms.TextBox txtRePassword;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cmbLanguage;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbDb;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton rdSqlServerAuth;
    private System.Windows.Forms.RadioButton rdWindowsAuth;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ListView lvRoles;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.BindingSource bsDbs;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;

  }
}
