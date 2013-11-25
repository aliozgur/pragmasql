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
  public partial class frmNewLogin : KryptonForm
  {
    private NewLogin _newLogin = null;
    public static DialogResult ShowNewLoginDalog(ConnectionParams cp)
    {
      frmNewLogin frm = new frmNewLogin();
      frm.InitializeDialog(cp);
      return frm.ShowDialog();
    }
    
    private frmNewLogin( )
    {
      InitializeComponent();
    }

    private void InitializeDialog( ConnectionParams cp )
    {
      _newLogin = new NewLogin(cp);
      panNewLogin.Controls.Add(_newLogin);
      _newLogin.Parent = panNewLogin;
      _newLogin.Dock = DockStyle.Fill;
    }

    private void btnCreate_Click( object sender, EventArgs e )
    {
      
      if (_newLogin.CreateLogin())
      {
        if (!MessageService.AskQuestion("Create another login?"))
        {
          DialogResult = DialogResult.OK;
        }
        _newLogin.ResetLoginDefinition();
      }
      
    }
  }
}