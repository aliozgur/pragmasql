namespace SQLManagement
{
  partial class NewUser
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
      this.txtUsername = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbLogin = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.lvRoles = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.txtUsername);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cmbLogin);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(486, 92);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "User";
      // 
      // txtUsername
      // 
      this.txtUsername.Location = new System.Drawing.Point(90, 51);
      this.txtUsername.Name = "txtUsername";
      this.txtUsername.Size = new System.Drawing.Size(233, 23);
      this.txtUsername.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(21, 54);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(59, 15);
      this.label2.TabIndex = 2;
      this.label2.Text = "User name";
      // 
      // cmbLogin
      // 
      this.cmbLogin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbLogin.FormattingEnabled = true;
      this.cmbLogin.Location = new System.Drawing.Point(90, 20);
      this.cmbLogin.Name = "cmbLogin";
      this.cmbLogin.Size = new System.Drawing.Size(233, 23);
      this.cmbLogin.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(21, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Login name";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.lvRoles);
      this.groupBox2.Location = new System.Drawing.Point(3, 101);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(486, 250);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Roles";
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
      this.lvRoles.Location = new System.Drawing.Point(24, 20);
      this.lvRoles.Name = "lvRoles";
      this.lvRoles.Size = new System.Drawing.Size(445, 217);
      this.lvRoles.TabIndex = 3;
      this.lvRoles.UseCompatibleStateImageBehavior = false;
      this.lvRoles.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "";
      this.columnHeader1.Width = 299;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem,
            this.toggleToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(140, 70);
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
      // NewUser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "NewUser";
      this.Size = new System.Drawing.Size(497, 365);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbLogin;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListView lvRoles;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
  }
}
