namespace PragmaSQL.Svn.Gui
{
  partial class SslServerTrustDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SslServerTrustDialog));
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.certificateAuthorityStatusLabel = new System.Windows.Forms.Label();
      this.certificateNameStatusLabel = new System.Windows.Forms.Label();
      this.certificateDateStatusLabel = new System.Windows.Forms.Label();
      this.issuerLabel = new System.Windows.Forms.Label();
      this.validLabel = new System.Windows.Forms.Label();
      this.fingerPrintlabel = new System.Windows.Forms.Label();
      this.hostNameLabel = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.certificateTextBox = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.saveCredentialsCheckBox = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(313, 291);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 26);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "Accept";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(395, 291);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 26);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Reject";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.certificateAuthorityStatusLabel);
      this.groupBox1.Controls.Add(this.certificateNameStatusLabel);
      this.groupBox1.Controls.Add(this.certificateDateStatusLabel);
      this.groupBox1.Location = new System.Drawing.Point(9, 13);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(462, 104);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Problems and Issues with this certificate";
      // 
      // certificateAuthorityStatusLabel
      // 
      this.certificateAuthorityStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateAuthorityStatusLabel.AutoSize = true;
      this.certificateAuthorityStatusLabel.Location = new System.Drawing.Point(7, 72);
      this.certificateAuthorityStatusLabel.Name = "certificateAuthorityStatusLabel";
      this.certificateAuthorityStatusLabel.Size = new System.Drawing.Size(37, 15);
      this.certificateAuthorityStatusLabel.TabIndex = 2;
      this.certificateAuthorityStatusLabel.Text = "label3";
      // 
      // certificateNameStatusLabel
      // 
      this.certificateNameStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateNameStatusLabel.AutoSize = true;
      this.certificateNameStatusLabel.Location = new System.Drawing.Point(7, 47);
      this.certificateNameStatusLabel.Name = "certificateNameStatusLabel";
      this.certificateNameStatusLabel.Size = new System.Drawing.Size(37, 15);
      this.certificateNameStatusLabel.TabIndex = 1;
      this.certificateNameStatusLabel.Text = "label2";
      // 
      // certificateDateStatusLabel
      // 
      this.certificateDateStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateDateStatusLabel.AutoSize = true;
      this.certificateDateStatusLabel.Location = new System.Drawing.Point(7, 23);
      this.certificateDateStatusLabel.Name = "certificateDateStatusLabel";
      this.certificateDateStatusLabel.Size = new System.Drawing.Size(37, 15);
      this.certificateDateStatusLabel.TabIndex = 0;
      this.certificateDateStatusLabel.Text = "label1";
      // 
      // issuerLabel
      // 
      this.issuerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.issuerLabel.AutoSize = true;
      this.issuerLabel.Location = new System.Drawing.Point(83, 124);
      this.issuerLabel.Name = "issuerLabel";
      this.issuerLabel.Size = new System.Drawing.Size(37, 15);
      this.issuerLabel.TabIndex = 6;
      this.issuerLabel.Text = "label1";
      // 
      // validLabel
      // 
      this.validLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.validLabel.AutoSize = true;
      this.validLabel.Location = new System.Drawing.Point(83, 150);
      this.validLabel.Name = "validLabel";
      this.validLabel.Size = new System.Drawing.Size(37, 15);
      this.validLabel.TabIndex = 7;
      this.validLabel.Text = "label2";
      // 
      // fingerPrintlabel
      // 
      this.fingerPrintlabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fingerPrintlabel.AutoSize = true;
      this.fingerPrintlabel.Location = new System.Drawing.Point(83, 176);
      this.fingerPrintlabel.Name = "fingerPrintlabel";
      this.fingerPrintlabel.Size = new System.Drawing.Size(37, 15);
      this.fingerPrintlabel.TabIndex = 8;
      this.fingerPrintlabel.Text = "label3";
      // 
      // hostNameLabel
      // 
      this.hostNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.hostNameLabel.AutoSize = true;
      this.hostNameLabel.Location = new System.Drawing.Point(83, 203);
      this.hostNameLabel.Name = "hostNameLabel";
      this.hostNameLabel.Size = new System.Drawing.Size(37, 15);
      this.hostNameLabel.TabIndex = 9;
      this.hostNameLabel.Text = "label4";
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(8, 238);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(63, 15);
      this.label5.TabIndex = 10;
      this.label5.Text = "Certificate: ";
      // 
      // certificateTextBox
      // 
      this.certificateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateTextBox.Location = new System.Drawing.Point(86, 235);
      this.certificateTextBox.Name = "certificateTextBox";
      this.certificateTextBox.Size = new System.Drawing.Size(385, 23);
      this.certificateTextBox.TabIndex = 11;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(8, 203);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(58, 15);
      this.label6.TabIndex = 15;
      this.label6.Text = "Hostname:";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(8, 176);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(62, 15);
      this.label7.TabIndex = 14;
      this.label7.Text = "Fingerprint:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(8, 150);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(34, 15);
      this.label8.TabIndex = 13;
      this.label8.Text = "Valid:";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(8, 124);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(36, 15);
      this.label9.TabIndex = 12;
      this.label9.Text = "Issuer:";
      // 
      // saveCredentialsCheckBox
      // 
      this.saveCredentialsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.saveCredentialsCheckBox.AutoSize = true;
      this.saveCredentialsCheckBox.Location = new System.Drawing.Point(85, 266);
      this.saveCredentialsCheckBox.Name = "saveCredentialsCheckBox";
      this.saveCredentialsCheckBox.Size = new System.Drawing.Size(109, 19);
      this.saveCredentialsCheckBox.TabIndex = 16;
      this.saveCredentialsCheckBox.Text = "Save credentials?";
      this.saveCredentialsCheckBox.UseVisualStyleBackColor = true;
      // 
      // SslServerTrustDialog
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(483, 329);
      this.Controls.Add(this.saveCredentialsCheckBox);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.certificateTextBox);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.hostNameLabel);
      this.Controls.Add(this.fingerPrintlabel);
      this.Controls.Add(this.validLabel);
      this.Controls.Add(this.issuerLabel);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "SslServerTrustDialog";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Security alert (SVN) ";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label certificateAuthorityStatusLabel;
    private System.Windows.Forms.Label certificateNameStatusLabel;
    private System.Windows.Forms.Label certificateDateStatusLabel;
    private System.Windows.Forms.Label issuerLabel;
    private System.Windows.Forms.Label validLabel;
    private System.Windows.Forms.Label fingerPrintlabel;
    private System.Windows.Forms.Label hostNameLabel;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox certificateTextBox;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.CheckBox saveCredentialsCheckBox;


  }
}