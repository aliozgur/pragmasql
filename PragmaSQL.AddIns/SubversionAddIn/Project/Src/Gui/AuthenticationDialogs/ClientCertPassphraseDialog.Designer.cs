namespace PragmaSQL.Svn.Gui
{
  partial class ClientCertPassphraseDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientCertPassphraseDialog));
      this.realmLabel = new System.Windows.Forms.Label();
      this.saveCredentialsCheckBox = new System.Windows.Forms.CheckBox();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.passPhraseTextBox = new PragmaSQL.Core.LabelTextBox();
      this.SuspendLayout();
      // 
      // realmLabel
      // 
      this.realmLabel.AutoSize = true;
      this.realmLabel.Location = new System.Drawing.Point(53, 10);
      this.realmLabel.Name = "realmLabel";
      this.realmLabel.Size = new System.Drawing.Size(37, 15);
      this.realmLabel.TabIndex = 1;
      this.realmLabel.Text = "Realm";
      // 
      // saveCredentialsCheckBox
      // 
      this.saveCredentialsCheckBox.AutoSize = true;
      this.saveCredentialsCheckBox.Location = new System.Drawing.Point(9, 87);
      this.saveCredentialsCheckBox.Name = "saveCredentialsCheckBox";
      this.saveCredentialsCheckBox.Size = new System.Drawing.Size(106, 19);
      this.saveCredentialsCheckBox.TabIndex = 2;
      this.saveCredentialsCheckBox.Text = "Save Credentials";
      this.saveCredentialsCheckBox.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(269, 127);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 26);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(351, 127);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 26);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 10);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 15);
      this.label1.TabIndex = 5;
      this.label1.Text = "Realm: ";
      // 
      // passPhraseTextBox
      // 
      this.passPhraseTextBox.LabelText = "Passphrase";
      this.passPhraseTextBox.Location = new System.Drawing.Point(4, 29);
      this.passPhraseTextBox.Name = "passPhraseTextBox";
      this.passPhraseTextBox.ReadOnly = false;
      this.passPhraseTextBox.Size = new System.Drawing.Size(426, 51);
      this.passPhraseTextBox.TabIndex = 6;
      this.passPhraseTextBox.TextBoxText = "";
      // 
      // ClientCertPassphraseDialog
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(438, 166);
      this.Controls.Add(this.passPhraseTextBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.saveCredentialsCheckBox);
      this.Controls.Add(this.realmLabel);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ClientCertPassphraseDialog";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Client certificate passphrase (SVN) ";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label realmLabel;
    private System.Windows.Forms.CheckBox saveCredentialsCheckBox;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label1;
    private PragmaSQL.Core.LabelTextBox passPhraseTextBox;


  }
}