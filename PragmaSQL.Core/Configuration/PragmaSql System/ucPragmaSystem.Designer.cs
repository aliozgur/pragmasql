namespace PragmaSQL.Core
{
  partial class ucPragmaSqlSystem
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtTimeOut = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtDefultDB = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.txtServer = new System.Windows.Forms.ComboBox();
      this.rbUseIntegratedSecurity = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.chkUtilsDisabled = new System.Windows.Forms.CheckBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.chkScriptSharingEnabled = new System.Windows.Forms.CheckBox();
      this.chkUseSharedSnippets = new System.Windows.Forms.CheckBox();
      this.chkObjectLoggingEnabled = new System.Windows.Forms.CheckBox();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point(128, 16);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size(162, 20);
      this.txtUserName.TabIndex = 5;
      this.txtUserName.TextChanged += new System.EventHandler(this.txtDefultDB_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(16, 40);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(53, 13);
      this.label5.TabIndex = 28;
      this.label5.Text = "Password";
      // 
      // txtTimeOut
      // 
      this.txtTimeOut.Location = new System.Drawing.Point(133, 79);
      this.txtTimeOut.Name = "txtTimeOut";
      this.txtTimeOut.Size = new System.Drawing.Size(72, 20);
      this.txtTimeOut.TabIndex = 3;
      this.txtTimeOut.Text = "15";
      this.txtTimeOut.TextChanged += new System.EventHandler(this.txtDefultDB_TextChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(21, 25);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(38, 13);
      this.label6.TabIndex = 43;
      this.label6.Text = "Server";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(21, 81);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(107, 13);
      this.label7.TabIndex = 45;
      this.label7.Text = "Time out (default=15)";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(21, 53);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(53, 13);
      this.label3.TabIndex = 44;
      this.label3.Text = "Database";
      // 
      // txtDefultDB
      // 
      this.txtDefultDB.Location = new System.Drawing.Point(133, 52);
      this.txtDefultDB.Name = "txtDefultDB";
      this.txtDefultDB.Size = new System.Drawing.Size(192, 20);
      this.txtDefultDB.TabIndex = 2;
      this.txtDefultDB.TextChanged += new System.EventHandler(this.txtDefultDB_TextChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(16, 16);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(58, 13);
      this.label4.TabIndex = 25;
      this.label4.Text = "User name";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.button2);
      this.groupBox2.Controls.Add(this.button1);
      this.groupBox2.Controls.Add(this.txtServer);
      this.groupBox2.Controls.Add(this.rbUseIntegratedSecurity);
      this.groupBox2.Controls.Add(this.groupBox1);
      this.groupBox2.Controls.Add(this.txtTimeOut);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.txtDefultDB);
      this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBox2.Location = new System.Drawing.Point(10, 35);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(352, 223);
      this.groupBox2.TabIndex = 56;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Database Connection";
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button2.ForeColor = System.Drawing.Color.Green;
      this.button2.Location = new System.Drawing.Point(154, 196);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(70, 22);
      this.button2.TabIndex = 63;
      this.button2.Text = "Install";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button1.ForeColor = System.Drawing.Color.Blue;
      this.button1.Location = new System.Drawing.Point(229, 196);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(96, 22);
      this.button1.TabIndex = 62;
      this.button1.Text = "Test Connection";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // txtServer
      // 
      this.txtServer.FormattingEnabled = true;
      this.txtServer.Location = new System.Drawing.Point(133, 24);
      this.txtServer.Name = "txtServer";
      this.txtServer.Size = new System.Drawing.Size(192, 21);
      this.txtServer.TabIndex = 1;
      this.txtServer.SelectedIndexChanged += new System.EventHandler(this.txtServer_SelectedIndexChanged);
      this.txtServer.DropDown += new System.EventHandler(this.txtServer_DropDown);
      this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
      // 
      // rbUseIntegratedSecurity
      // 
      this.rbUseIntegratedSecurity.AutoSize = true;
      this.rbUseIntegratedSecurity.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.rbUseIntegratedSecurity.Location = new System.Drawing.Point(24, 106);
      this.rbUseIntegratedSecurity.Name = "rbUseIntegratedSecurity";
      this.rbUseIntegratedSecurity.Size = new System.Drawing.Size(143, 18);
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
      this.groupBox1.Location = new System.Drawing.Point(24, 125);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(301, 68);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point(128, 40);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(162, 20);
      this.txtPassword.TabIndex = 6;
      this.txtPassword.TextChanged += new System.EventHandler(this.txtDefultDB_TextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.ForeColor = System.Drawing.Color.Blue;
      this.label2.Location = new System.Drawing.Point(7, 355);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(239, 13);
      this.label2.TabIndex = 60;
      this.label2.Text = "Why PragmaSQL needs a database connection?";
      this.label2.Click += new System.EventHandler(this.label2_Click);
      // 
      // chkUtilsDisabled
      // 
      this.chkUtilsDisabled.AutoSize = true;
      this.chkUtilsDisabled.Location = new System.Drawing.Point(10, 12);
      this.chkUtilsDisabled.Name = "chkUtilsDisabled";
      this.chkUtilsDisabled.Size = new System.Drawing.Size(268, 17);
      this.chkUtilsDisabled.TabIndex = 61;
      this.chkUtilsDisabled.Text = "Do not use PragmaSQL logging and sharing system";
      this.chkUtilsDisabled.UseVisualStyleBackColor = true;
      this.chkUtilsDisabled.CheckedChanged += new System.EventHandler(this.chkUtilsDisabled_CheckedChanged);
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.chkScriptSharingEnabled);
      this.groupBox3.Controls.Add(this.chkUseSharedSnippets);
      this.groupBox3.Controls.Add(this.chkObjectLoggingEnabled);
      this.groupBox3.Location = new System.Drawing.Point(10, 264);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(352, 86);
      this.groupBox3.TabIndex = 62;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Logging and sharing";
      // 
      // chkScriptSharingEnabled
      // 
      this.chkScriptSharingEnabled.AutoSize = true;
      this.chkScriptSharingEnabled.Location = new System.Drawing.Point(24, 67);
      this.chkScriptSharingEnabled.Name = "chkScriptSharingEnabled";
      this.chkScriptSharingEnabled.Size = new System.Drawing.Size(131, 17);
      this.chkScriptSharingEnabled.TabIndex = 64;
      this.chkScriptSharingEnabled.Text = "Script sharing enabled";
      this.chkScriptSharingEnabled.UseVisualStyleBackColor = true;
      this.chkScriptSharingEnabled.CheckedChanged += new System.EventHandler(this.chkObjectLoggingEnabled_CheckedChanged);
      // 
      // chkUseSharedSnippets
      // 
      this.chkUseSharedSnippets.AutoSize = true;
      this.chkUseSharedSnippets.Location = new System.Drawing.Point(24, 44);
      this.chkUseSharedSnippets.Name = "chkUseSharedSnippets";
      this.chkUseSharedSnippets.Size = new System.Drawing.Size(149, 17);
      this.chkUseSharedSnippets.TabIndex = 63;
      this.chkUseSharedSnippets.Text = "Use shared code snippets";
      this.chkUseSharedSnippets.UseVisualStyleBackColor = true;
      this.chkUseSharedSnippets.CheckedChanged += new System.EventHandler(this.chkObjectLoggingEnabled_CheckedChanged);
      // 
      // chkObjectLoggingEnabled
      // 
      this.chkObjectLoggingEnabled.AutoSize = true;
      this.chkObjectLoggingEnabled.Location = new System.Drawing.Point(24, 21);
      this.chkObjectLoggingEnabled.Name = "chkObjectLoggingEnabled";
      this.chkObjectLoggingEnabled.Size = new System.Drawing.Size(174, 17);
      this.chkObjectLoggingEnabled.TabIndex = 62;
      this.chkObjectLoggingEnabled.Text = "Object change logging enabled";
      this.chkObjectLoggingEnabled.UseVisualStyleBackColor = true;
      this.chkObjectLoggingEnabled.CheckedChanged += new System.EventHandler(this.chkObjectLoggingEnabled_CheckedChanged);
      // 
      // ucPragmaSqlSystem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.chkUtilsDisabled);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.groupBox2);
      this.Name = "ucPragmaSqlSystem";
      this.Size = new System.Drawing.Size(377, 382);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtTimeOut;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.TextBox txtDefultDB;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ComboBox txtServer;
    private System.Windows.Forms.CheckBox rbUseIntegratedSecurity;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chkUtilsDisabled;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.CheckBox chkObjectLoggingEnabled;
    private System.Windows.Forms.CheckBox chkScriptSharingEnabled;
    private System.Windows.Forms.CheckBox chkUseSharedSnippets;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
  }
}
