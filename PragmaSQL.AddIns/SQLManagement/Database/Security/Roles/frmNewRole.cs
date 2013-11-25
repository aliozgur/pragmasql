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
  public partial class frmNewRole: KryptonForm
  {
    private NewRole _newRole = null;
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

        if (_newRole != null)
          _newRole.ConnectionParams = _cp;
      }
    }

    public static DialogResult ShowNewRoleDialog( ConnectionParams cp )
    {
      frmNewRole frm = new frmNewRole();
      frm.Text = "New Role {" + cp.InfoDbServer + "} ";
      frm.ConnectionParams = cp;
      return frm.ShowDialog();
    }

    public frmNewRole( )
    {
      InitializeComponent();
      InitializeNewRoleControl();
    }

    private void InitializeNewRoleControl()
    {
      _newRole = new NewRole();
      panNewRoleControl.Controls.Add(_newRole);
      _newRole.Parent = panNewRoleControl;
      _newRole.Dock = DockStyle.Fill;
    }

    private void btnCreate_Click( object sender, EventArgs e )
    {
      if (_newRole.CreateRole())
      {
        if (MessageService.AskQuestion("Create another role?"))
        {
          _newRole.ResetRoleDefinition();
        }
        else
        {
          DialogResult = DialogResult.OK;
        }
      }
    }
  }
}