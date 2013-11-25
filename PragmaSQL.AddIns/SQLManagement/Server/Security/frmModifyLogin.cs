using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace SQLManagement
{
  public partial class frmModifyLogin : KryptonForm
  {
    private ModifyLogin _modifyLogin = null;
    private bool _loginWasUpdated = false;

    public static bool ShowLoginDetails(ConnectionParams cp, string loginName, string defaultDb, string defaultLanguage)
    {
      frmModifyLogin frm = new frmModifyLogin();
      frm.InitializeDialog(cp);
      frm._modifyLogin.LoginName = loginName;
      frm._modifyLogin.DefaultDb = defaultDb;
      frm._modifyLogin.DefaultLanguage = defaultLanguage;
      frm.Text = "Modify Login {" + loginName + "} ";
      frm.label1.Text = "Login : " + loginName;
      frm.ShowDialog();
      return frm._loginWasUpdated;
    }

    private frmModifyLogin( )
    {
      InitializeComponent();
    }

    private void InitializeDialog( ConnectionParams cp )
    {
      _modifyLogin = new ModifyLogin(cp);
      pan.Controls.Add(_modifyLogin);
      _modifyLogin.Parent = pan;
      _modifyLogin.Dock = DockStyle.Fill;
    }

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      if (_modifyLogin.UpdateLogin())
      {
        MessageService.ShowMessage("Login updated sucessfully.");
        _loginWasUpdated = true;
      }
    }    
  }
}