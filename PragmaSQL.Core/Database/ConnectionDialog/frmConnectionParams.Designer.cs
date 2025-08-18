namespace PragmaSQL.Core
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectionParams));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpServerDb = new System.Windows.Forms.GroupBox();
            this.btnLoadTemplateFromRepo = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.ComboBox();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDefultDB = new System.Windows.Forms.TextBox();
            this.txtFriendlyName = new System.Windows.Forms.TextBox();
            this.grpbLogon = new System.Windows.Forms.GroupBox();
            this.grpbUserSpec = new System.Windows.Forms.GroupBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.rbUseIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.lblFriendlyName = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblInfoMsg = new System.Windows.Forms.Label();
            this.bwTestConn = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.grpServerDb.SuspendLayout();
            this.grpbLogon.SuspendLayout();
            this.grpbUserSpec.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(203, 347);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 27);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(291, 347);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 27);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grpServerDb);
            this.groupBox2.Controls.Add(this.txtFriendlyName);
            this.groupBox2.Controls.Add(this.grpbLogon);
            this.groupBox2.Controls.Add(this.lblFriendlyName);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(5, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 319);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // grpServerDb
            // 
            this.grpServerDb.Controls.Add(this.chkEncrypt);
            this.grpServerDb.Controls.Add(this.btnLoadTemplateFromRepo);
            this.grpServerDb.Controls.Add(this.txtServer);
            this.grpServerDb.Controls.Add(this.txtTimeOut);
            this.grpServerDb.Controls.Add(this.label6);
            this.grpServerDb.Controls.Add(this.label7);
            this.grpServerDb.Controls.Add(this.label3);
            this.grpServerDb.Controls.Add(this.txtDefultDB);
            this.grpServerDb.Location = new System.Drawing.Point(15, 39);
            this.grpServerDb.Name = "grpServerDb";
            this.grpServerDb.Size = new System.Drawing.Size(338, 136);
            this.grpServerDb.TabIndex = 0;
            this.grpServerDb.TabStop = false;
            this.grpServerDb.Text = "Server/Database Specification";
            // 
            // btnLoadTemplateFromRepo
            // 
            this.btnLoadTemplateFromRepo.Enabled = false;
            this.btnLoadTemplateFromRepo.Image = global::PragmaSQL.Core.Properties.Resources.application_form_magnify;
            this.btnLoadTemplateFromRepo.Location = new System.Drawing.Point(300, 21);
            this.btnLoadTemplateFromRepo.Name = "btnLoadTemplateFromRepo";
            this.btnLoadTemplateFromRepo.Size = new System.Drawing.Size(29, 26);
            this.btnLoadTemplateFromRepo.TabIndex = 54;
            this.toolTip1.SetToolTip(this.btnLoadTemplateFromRepo, "Load template from saved connections");
            this.btnLoadTemplateFromRepo.UseVisualStyleBackColor = true;
            this.btnLoadTemplateFromRepo.Click += new System.EventHandler(this.btnLoadTemplateFromRepo_Click);
            // 
            // txtServer
            // 
            this.txtServer.FormattingEnabled = true;
            this.txtServer.Location = new System.Drawing.Point(125, 23);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(174, 21);
            this.txtServer.TabIndex = 1;
            this.txtServer.DropDown += new System.EventHandler(this.txtServer_DropDown);
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(125, 79);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(97, 20);
            this.txtTimeOut.TabIndex = 3;
            this.txtTimeOut.Text = "15";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Server";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "Time out (default=15)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Default database";
            // 
            // txtDefultDB
            // 
            this.txtDefultDB.Location = new System.Drawing.Point(125, 51);
            this.txtDefultDB.Name = "txtDefultDB";
            this.txtDefultDB.Size = new System.Drawing.Size(203, 20);
            this.txtDefultDB.TabIndex = 2;
            // 
            // txtFriendlyName
            // 
            this.txtFriendlyName.Location = new System.Drawing.Point(140, 16);
            this.txtFriendlyName.Name = "txtFriendlyName";
            this.txtFriendlyName.Size = new System.Drawing.Size(204, 20);
            this.txtFriendlyName.TabIndex = 0;
            this.txtFriendlyName.Text = "SQL 2000/2005";
            // 
            // grpbLogon
            // 
            this.grpbLogon.Controls.Add(this.grpbUserSpec);
            this.grpbLogon.Controls.Add(this.rbUseIntegratedSecurity);
            this.grpbLogon.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpbLogon.Location = new System.Drawing.Point(15, 181);
            this.grpbLogon.Name = "grpbLogon";
            this.grpbLogon.Size = new System.Drawing.Size(338, 124);
            this.grpbLogon.TabIndex = 1;
            this.grpbLogon.TabStop = false;
            this.grpbLogon.Text = "LogOn Specification";
            // 
            // grpbUserSpec
            // 
            this.grpbUserSpec.Controls.Add(this.txtUserName);
            this.grpbUserSpec.Controls.Add(this.label5);
            this.grpbUserSpec.Controls.Add(this.label4);
            this.grpbUserSpec.Controls.Add(this.txtPassword);
            this.grpbUserSpec.Location = new System.Drawing.Point(13, 36);
            this.grpbUserSpec.Name = "grpbUserSpec";
            this.grpbUserSpec.Size = new System.Drawing.Size(309, 73);
            this.grpbUserSpec.TabIndex = 30;
            this.grpbUserSpec.TabStop = false;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(122, 18);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(168, 20);
            this.txtUserName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "User name";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(122, 42);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(168, 20);
            this.txtPassword.TabIndex = 6;
            // 
            // rbUseIntegratedSecurity
            // 
            this.rbUseIntegratedSecurity.AutoSize = true;
            this.rbUseIntegratedSecurity.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbUseIntegratedSecurity.Location = new System.Drawing.Point(13, 19);
            this.rbUseIntegratedSecurity.Name = "rbUseIntegratedSecurity";
            this.rbUseIntegratedSecurity.Size = new System.Drawing.Size(143, 18);
            this.rbUseIntegratedSecurity.TabIndex = 4;
            this.rbUseIntegratedSecurity.Text = "Use Integrated Security";
            this.rbUseIntegratedSecurity.CheckedChanged += new System.EventHandler(this.rbUseIntegratedSecurity_CheckedChanged);
            // 
            // lblFriendlyName
            // 
            this.lblFriendlyName.AutoSize = true;
            this.lblFriendlyName.Location = new System.Drawing.Point(25, 16);
            this.lblFriendlyName.Name = "lblFriendlyName";
            this.lblFriendlyName.Size = new System.Drawing.Size(72, 13);
            this.lblFriendlyName.TabIndex = 54;
            this.lblFriendlyName.Text = "Friendly name";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(2, 347);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(94, 27);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblInfoMsg
            // 
            this.lblInfoMsg.AutoSize = true;
            this.lblInfoMsg.Location = new System.Drawing.Point(5, 6);
            this.lblInfoMsg.Name = "lblInfoMsg";
            this.lblInfoMsg.Size = new System.Drawing.Size(236, 13);
            this.lblInfoMsg.TabIndex = 10;
            this.lblInfoMsg.Text = "Please specify database connection parameters.";
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.Location = new System.Drawing.Point(124, 111);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(74, 17);
            this.chkEncrypt.TabIndex = 55;
            this.chkEncrypt.Text = "Encrypted";
            this.chkEncrypt.UseVisualStyleBackColor = true;
            // 
            // frmConnectionParams
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 381);
            this.Controls.Add(this.lblInfoMsg);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(390, 420);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 390);
            this.Name = "frmConnectionParams";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PragmaSQL DB Connection";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpServerDb.ResumeLayout(false);
            this.grpServerDb.PerformLayout();
            this.grpbLogon.ResumeLayout(false);
            this.grpbLogon.PerformLayout();
            this.grpbUserSpec.ResumeLayout(false);
            this.grpbUserSpec.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox grpbLogon;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.GroupBox grpServerDb;
    private System.Windows.Forms.ComboBox txtServer;
    private System.Windows.Forms.TextBox txtFriendlyName;
    private System.Windows.Forms.Label lblFriendlyName;
    private System.Windows.Forms.TextBox txtTimeOut;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.TextBox txtDefultDB;
    private System.Windows.Forms.GroupBox grpbUserSpec;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.CheckBox rbUseIntegratedSecurity;
    private System.Windows.Forms.Label lblInfoMsg;
		private System.ComponentModel.BackgroundWorker bwTestConn;
		private System.Windows.Forms.Button btnLoadTemplateFromRepo;
		private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkEncrypt;
    }
}