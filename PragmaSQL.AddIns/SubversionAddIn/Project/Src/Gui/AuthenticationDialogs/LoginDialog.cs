using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NSvn.Core;

namespace PragmaSQL.Svn.Gui
{
  public partial class LoginDialog : SvnDialogBase
  {
    public LoginDialog( )
    {
      InitializeComponent();
    }

    public LoginDialog( string realm, string userName, bool maySave )
      : this()
    {
      this.UserName = userName;
      this.Realm = realm;
      this.MaySave = maySave;
      showPasswordCheckBox.CheckedChanged += new EventHandler(ShowPasswordCheckBoxCheckedChanged);
      pwd2TextBox.TextBox.PasswordChar = '*';

      pwd1TextBox.TextBox.TextChanged += new EventHandler(PasswordTextChanged);
      pwd2TextBox.TextBox.TextChanged += new EventHandler(PasswordTextChanged);

      btnOk.Click += new EventHandler(OkButtonClicked);
    }


    public bool MaySave
    {
      get { return saveCredentialsCheckBox.Checked; }
      set { saveCredentialsCheckBox.Checked = value; }
    }

    public string Realm
    {
      get { return realmLabel.Text; }
      set { realmLabel.Text = value; }
    }

    public string UserName
    {
      get { return userNameTextBox.TextBoxText; }
      set { userNameTextBox.TextBoxText = value; }
    }

    public SimpleCredential Credential
    {
      get { return new SimpleCredential(UserName, Password, MaySave); }
    }

    string Password
    {
      get { return pwd1TextBox.TextBoxText; }
    }

    string ReTypedPassword
    {
      get { return pwd2TextBox.TextBoxText; }
    }

    bool ShowPasswords
    {
      get { return showPasswordCheckBox.Checked; }
    }

    void ShowPasswordCheckBoxCheckedChanged( object sender, EventArgs e )
    {
      if (ShowPasswords)
      {
        pwd1TextBox.TextBox.PasswordChar = '\0';
        pwd2TextBox.TextBox.Enabled = false;
      }
      else
      {
        pwd1TextBox.TextBox.PasswordChar = '*';
      }
    }

    void PasswordTextChanged( object sender, EventArgs e )
    {
      btnOk.Enabled = ( ShowPasswords || Password == ReTypedPassword);
    }

    void OkButtonClicked( object sender, EventArgs e )
    {
      bool done = false;
      if (ShowPasswords)
      {
        done = UserName.Length > 0 && Password.Length > 0;
      }
      else
      {
        done = Password == ReTypedPassword;
      }

      if (done)
      {
        DialogResult = DialogResult.OK;
      }
    }
  }
}