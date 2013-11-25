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
  public partial class frmNewUser : KryptonForm
  {
    private NewUser _newUser = null;
    private ConnectionParams _cp = null;
    
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = null;
          _cp = value.CreateCopy();
        }
        else
        {
          _cp = null;
        }

        if (_newUser != null)
          _newUser.ConnectionParams = _cp;
      }
    }

    public static DialogResult ShowNewUserDialog( ConnectionParams cp )
    {
      frmNewUser frm = new frmNewUser();
      frm.Text = "New User {" + cp.InfoDbServer + "} ";
      frm.ConnectionParams = cp;
      return frm.ShowDialog();
    }

    public frmNewUser( )
    {
      InitializeComponent();
      InitializeNewUserControl();
    }

    private void InitializeNewUserControl()
    {
      _newUser = new NewUser();
      panNewUserControl.Controls.Add(_newUser);
      _newUser.Parent = panNewUserControl;
      _newUser.Dock = DockStyle.Fill;
    }

    private void btnCreate_Click( object sender, EventArgs e )
    {
      if (_newUser.CreateUser())
      {
        if (MessageService.AskQuestion("Create another user?"))
        {
          _newUser.ResetUserDefinition();
        }
        else
        {
          DialogResult = DialogResult.OK;
        }
      }
    }
  }
}