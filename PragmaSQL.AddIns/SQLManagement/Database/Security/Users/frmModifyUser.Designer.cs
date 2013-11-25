namespace SQLManagement
{
  partial class frmModifyUser
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModifyUser));
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.usersListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnGoToParent = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.lblInfo = new System.Windows.Forms.ToolStripLabel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.panSysPrivileges = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.panRoles = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.panObjPrivileges = new System.Windows.Forms.Panel();
      this.bs = new System.Windows.Forms.BindingSource(this.components);
      this.contextMenuStrip2.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
      this.SuspendLayout();
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem,
            this.toolStripMenuItem1,
            this.usersListToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(167, 98);
      // 
      // closeToolStripMenuItem
      // 
      this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
      this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeToolStripMenuItem.Text = "Close";
      this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
      // 
      // closeAllToolStripMenuItem
      // 
      this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
      this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllToolStripMenuItem.Text = "Close All";
      this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
      // 
      // closeAllButThisToolStripMenuItem
      // 
      this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
      this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllButThisToolStripMenuItem.Text = "Close All But This";
      this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
      // 
      // usersListToolStripMenuItem
      // 
      this.usersListToolStripMenuItem.Name = "usersListToolStripMenuItem";
      this.usersListToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.usersListToolStripMenuItem.Text = "Users List";
      this.usersListToolStripMenuItem.Click += new System.EventHandler(this.usersListToolStripMenuItem_Click);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGoToParent,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.lblInfo});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(792, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnGoToParent
      // 
      this.btnGoToParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnGoToParent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.btnGoToParent.ForeColor = System.Drawing.SystemColors.ControlText;
      this.btnGoToParent.Image = global::SQLManagement.Properties.Resources.Output;
      this.btnGoToParent.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnGoToParent.Name = "btnGoToParent";
      this.btnGoToParent.Size = new System.Drawing.Size(23, 22);
      this.btnGoToParent.Text = "Back To Users List";
      this.btnGoToParent.Visible = false;
      this.btnGoToParent.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::SQLManagement.Properties.Resources.Refresh;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Refresh";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // lblInfo
      // 
      this.lblInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblInfo.Image = global::SQLManagement.Properties.Resources.Information;
      this.lblInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(68, 22);
      this.lblInfo.Text = "User Info";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 25);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.label3);
      this.splitContainer1.Panel2.Controls.Add(this.panObjPrivileges);
      this.splitContainer1.Size = new System.Drawing.Size(792, 470);
      this.splitContainer1.SplitterDistance = 190;
      this.splitContainer1.SplitterWidth = 3;
      this.splitContainer1.TabIndex = 2;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 190);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.panSysPrivileges);
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(399, 3);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(390, 184);
      this.panel2.TabIndex = 1;
      // 
      // panSysPrivileges
      // 
      this.panSysPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panSysPrivileges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panSysPrivileges.Location = new System.Drawing.Point(3, 18);
      this.panSysPrivileges.Name = "panSysPrivileges";
      this.panSysPrivileges.Size = new System.Drawing.Size(381, 164);
      this.panSysPrivileges.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label2.Location = new System.Drawing.Point(1, 3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(111, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "System privileges:";
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.Control;
      this.panel3.Controls.Add(this.panRoles);
      this.panel3.Controls.Add(this.label1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point(3, 3);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(390, 184);
      this.panel3.TabIndex = 0;
      // 
      // panRoles
      // 
      this.panRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panRoles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panRoles.Location = new System.Drawing.Point(4, 18);
      this.panRoles.Name = "panRoles";
      this.panRoles.Size = new System.Drawing.Size(382, 163);
      this.panRoles.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Location = new System.Drawing.Point(2, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "User is in roles:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label3.Location = new System.Drawing.Point(3, 4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(105, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Object privileges:";
      // 
      // panObjPrivileges
      // 
      this.panObjPrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panObjPrivileges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panObjPrivileges.Location = new System.Drawing.Point(4, 21);
      this.panObjPrivileges.Name = "panObjPrivileges";
      this.panObjPrivileges.Size = new System.Drawing.Size(784, 252);
      this.panObjPrivileges.TabIndex = 2;
      // 
      // frmModifyUser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 495);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.toolStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmModifyUser";
      this.TabPageContextMenuStrip = this.contextMenuStrip2;
      this.TabText = "Modify User";
      this.Text = "Modify User";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmModifyUser_FormClosed);
      this.contextMenuStrip2.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.BindingSource bs;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripLabel lblInfo;
    private System.Windows.Forms.ToolStripButton btnGoToParent;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem usersListToolStripMenuItem;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panRoles;
    private System.Windows.Forms.Panel panSysPrivileges;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panObjPrivileges;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
  }
}