namespace PragmaSQL.Svn.Gui
{
  partial class LoginDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDialog));
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.saveCredentialsCheckBox = new System.Windows.Forms.CheckBox();
      this.showPasswordCheckBox = new System.Windows.Forms.CheckBox();
      this.pwd2TextBox = new PragmaSQL.Core.LabelTextBox();
      this.pwd1TextBox = new PragmaSQL.Core.LabelTextBox();
      this.userNameTextBox = new PragmaSQL.Core.LabelTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.realmLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(131, 228);
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
      this.btnCancel.Location = new System.Drawing.Point(213, 228);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 26);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // saveCredentialsCheckBox
      // 
      this.saveCredentialsCheckBox.AutoSize = true;
      this.saveCredentialsCheckBox.Location = new System.Drawing.Point(10, 180);
      this.saveCredentialsCheckBox.Name = "saveCredentialsCheckBox";
      this.saveCredentialsCheckBox.Size = new System.Drawing.Size(104, 19);
      this.saveCredentialsCheckBox.TabIndex = 5;
      this.saveCredentialsCheckBox.Text = "Save credentials";
      this.saveCredentialsCheckBox.UseVisualStyleBackColor = true;
      // 
      // showPasswordCheckBox
      // 
      this.showPasswordCheckBox.AutoSize = true;
      this.showPasswordCheckBox.Location = new System.Drawing.Point(120, 181);
      this.showPasswordCheckBox.Name = "showPasswordCheckBox";
      this.showPasswordCheckBox.Size = new System.Drawing.Size(98, 19);
      this.showPasswordCheckBox.TabIndex = 6;
      this.showPasswordCheckBox.Text = "Show password";
      this.showPasswordCheckBox.UseVisualStyleBackColor = true;
      // 
      // pwd2TextBox
      // 
      this.pwd2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pwd2TextBox.LabelText = "Re type";
      this.pwd2TextBox.Location = new System.Drawing.Point(3, 127);
      this.pwd2TextBox.Name = "pwd2TextBox";
      this.pwd2TextBox.ReadOnly = false;
      this.pwd2TextBox.Size = new System.Drawing.Size(289, 51);
      this.pwd2TextBox.TabIndex = 8;
      this.pwd2TextBox.TextBoxText = "";
      // 
      // pwd1TextBox
      // 
      this.pwd1TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pwd1TextBox.LabelText = "Password";
      this.pwd1TextBox.Location = new System.Drawing.Point(3, 80);
      this.pwd1TextBox.Name = "pwd1TextBox";
      this.pwd1TextBox.ReadOnly = false;
      this.pwd1TextBox.Size = new System.Drawing.Size(289, 47);
      this.pwd1TextBox.TabIndex = 7;
      this.pwd1TextBox.TextBoxText = "";
      // 
      // userNameTextBox
      // 
      this.userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.userNameTextBox.LabelText = "Username";
      this.userNameTextBox.Location = new System.Drawing.Point(3, 29);
      this.userNameTextBox.Name = "userNameTextBox";
      this.userNameTextBox.ReadOnly = false;
      this.userNameTextBox.Size = new System.Drawing.Size(289, 47);
      this.userNameTextBox.TabIndex = 9;
      this.userNameTextBox.TextBoxText = "";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 15);
      this.label1.TabIndex = 11;
      this.label1.Text = "Realm: ";
      // 
      // realmLabel
      // 
      this.realmLabel.AutoSize = true;
      this.realmLabel.Location = new System.Drawing.Point(54, 9);
      this.realmLabel.Name = "realmLabel";
      this.realmLabel.Size = new System.Drawing.Size(37, 15);
      this.realmLabel.TabIndex = 10;
      this.realmLabel.Text = "Realm";
      // 
      // LoginDialog
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(300, 267);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.realmLabel);
      this.Controls.Add(this.userNameTextBox);
      this.Controls.Add(this.pwd2TextBox);
      this.Controls.Add(this.pwd1TextBox);
      this.Controls.Add(this.showPasswordCheckBox);
      this.Controls.Add(this.saveCredentialsCheckBox);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "LoginDialog";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Login (SVN) ";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.CheckBox saveCredentialsCheckBox;
    private System.Windows.Forms.CheckBox showPasswordCheckBox;
    private PragmaSQL.Core.LabelTextBox pwd1TextBox;
    private PragmaSQL.Core.LabelTextBox pwd2TextBox;
    private PragmaSQL.Core.LabelTextBox userNameTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label realmLabel;


  }
}