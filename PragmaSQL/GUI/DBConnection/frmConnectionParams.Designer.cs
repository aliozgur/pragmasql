namespace PragmaSQL.GUI
{
  partial class frmConnectionParams
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
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
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectionParams));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txtServer = new System.Windows.Forms.ComboBox();
      this.rbUseIntegratedSecurity = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.txtFriendlyName = new System.Windows.Forms.TextBox();
      this.lblFriendlyName = new System.Windows.Forms.Label();
      this.txtTimeOut = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtDefultDB = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.Location = new System.Drawing.Point(201, 309);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(80, 25);
      this.btnOK.TabIndex = 21;
      this.btnOK.Text = "OK";
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(289, 309);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(80, 25);
      this.btnCancel.TabIndex = 22;
      this.btnCancel.Text = "Cancel";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.txtServer);
      this.groupBox2.Controls.Add(this.rbUseIntegratedSecurity);
      this.groupBox2.Controls.Add(this.groupBox1);
      this.groupBox2.Controls.Add(this.txtFriendlyName);
      this.groupBox2.Controls.Add(this.lblFriendlyName);
      this.groupBox2.Controls.Add(this.txtTimeOut);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.txtDefultDB);
      this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBox2.Location = new System.Drawing.Point(5, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(380, 287);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      // 
      // txtServer
      // 
      this.txtServer.FormattingEnabled = true;
      this.txtServer.Location = new System.Drawing.Point(133, 53);
      this.txtServer.Name = "txtServer";
      this.txtServer.Size = new System.Drawing.Size(232, 21);
      this.txtServer.TabIndex = 1;
      this.txtServer.DropDown += new System.EventHandler(this.txtServer_DropDown);
      // 
      // rbUseIntegratedSecurity
      // 
      this.rbUseIntegratedSecurity.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.rbUseIntegratedSecurity.Location = new System.Drawing.Point(24, 134);
      this.rbUseIntegratedSecurity.Name = "rbUseIntegratedSecurity";
      this.rbUseIntegratedSecurity.Size = new System.Drawing.Size(227, 28);
      this.rbUseIntegratedSecurity.TabIndex = 4;
      this.rbUseIntegratedSecurity.Text = "Use Integrated Security";
      this.rbUseIntegratedSecurity.CheckedChanged += new System.EventHandler(this.rbUseIntegratedSecurity_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtUserName);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.txtPassword);
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBox1.Location = new System.Drawing.Point(24, 168);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(336, 92);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point(128, 24);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(184, 20);
      this.txtUserName.TabIndex = 5;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(16, 48);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(80, 16);
      this.label5.TabIndex = 28;
      this.label5.Text = "Password";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(16, 24);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(80, 16);
      this.label4.TabIndex = 25;
      this.label4.Text = "User name";
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(128, 48);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(184, 20);
      this.txtPassword.TabIndex = 6;
      // 
      // txtFriendlyName
      // 
      this.txtFriendlyName.Location = new System.Drawing.Point(133, 26);
      this.txtFriendlyName.Name = "txtFriendlyName";
      this.txtFriendlyName.Size = new System.Drawing.Size(232, 20);
      this.txtFriendlyName.TabIndex = 0;
      this.txtFriendlyName.Text = "SQL 2000/2005";
      // 
      // lblFriendlyName
      // 
      this.lblFriendlyName.Location = new System.Drawing.Point(21, 26);
      this.lblFriendlyName.Name = "lblFriendlyName";
      this.lblFriendlyName.Size = new System.Drawing.Size(104, 24);
      this.lblFriendlyName.TabIndex = 46;
      this.lblFriendlyName.Text = "Friendly name";
      // 
      // txtTimeOut
      // 
      this.txtTimeOut.Location = new System.Drawing.Point(133, 108);
      this.txtTimeOut.Name = "txtTimeOut";
      this.txtTimeOut.Size = new System.Drawing.Size(232, 20);
      this.txtTimeOut.TabIndex = 3;
      this.txtTimeOut.Text = "15";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(21, 58);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(80, 16);
      this.label6.TabIndex = 43;
      this.label6.Text = "Server";
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(21, 106);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(120, 24);
      this.label7.TabIndex = 45;
      this.label7.Text = "Time out (default=15)";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(21, 82);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(96, 16);
      this.label3.TabIndex = 44;
      this.label3.Text = "Default database";
      // 
      // txtDefultDB
      // 
      this.txtDefultDB.Location = new System.Drawing.Point(133, 81);
      this.txtDefultDB.Name = "txtDefultDB";
      this.txtDefultDB.Size = new System.Drawing.Size(232, 20);
      this.txtDefultDB.TabIndex = 2;
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(4, 309);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(120, 25);
      this.button1.TabIndex = 55;
      this.button1.Text = "Test Connection";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // frmConnectionParams
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(388, 348);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.btnCancel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(394, 373);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(394, 0);
      this.Name = "frmConnectionParams";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Database Connection Parameters";
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox rbUseIntegratedSecurity;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.TextBox txtFriendlyName;
    private System.Windows.Forms.Label lblFriendlyName;
    private System.Windows.Forms.TextBox txtTimeOut;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.TextBox txtDefultDB;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ComboBox txtServer;
  }
}