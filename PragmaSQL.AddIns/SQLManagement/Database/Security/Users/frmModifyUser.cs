using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace SQLManagement
{
  public partial class frmModifyUser : DockContent
  {
    private UserRoleEdit _roleEdit = null;
    private SysPrivilegesEdit _sysPrivilegeEdit = null;
    private ObjectPrivilegesEdit _objPrivilegeEdit = null;

    private DockContent _parentContent = null;

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      private set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
        }
        else
        {
          _cp = null;
        }
      }
    }
    private string _userName = String.Empty;
    public string UserName
    {
      get { return _userName; }
      set { _userName = value; }
    }

    private string _caption = String.Empty;
    public string Caption
    {
      get { return _caption; }
      set
      {
        _caption = value;
        TabText = value;
        Text = value;
      }
    }


    public static void ModifyUserDlg(DockContent parent, ConnectionParams cp,string username)
    {
      frmModifyUser frm = new frmModifyUser();
      frm.ConnectionParams = cp;
      frm.UserName = username;

      frm.lblInfo.Text = username + " {" + cp.InfoDbServer + "}";
      frm.Caption = "Modify User: " + frm.lblInfo.Text;
      frm._parentContent = parent;
      frm.WireUpParentFormEvents();
      frm.btnGoToParent.Visible = parent != null;
      frm.InitializeUserControls();
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    private frmModifyUser( )
    {
      InitializeComponent();
      CreateUserControls();
    }

    private void WireUpParentFormEvents()
    {
      if (_parentContent == null)
        return;

      _parentContent.FormClosed += new FormClosedEventHandler(_parentContent_FormClosed);
    }

    private void GoToParentList( )
    {
      if (_parentContent == null)
        return;
      
      HostServicesSingleton.HostServices.ShowForm(_parentContent);
    }

    private void CreateUserControls( )
    {
      _roleEdit = new UserRoleEdit();
      panRoles.Controls.Add(_roleEdit);
      _roleEdit.Parent = panRoles;
      _roleEdit.Dock = DockStyle.Fill;
      
      _sysPrivilegeEdit = new SysPrivilegesEdit();
      panSysPrivileges.Controls.Add(_sysPrivilegeEdit);
      _sysPrivilegeEdit.Parent = panSysPrivileges;
      _sysPrivilegeEdit.Dock = DockStyle.Fill;

      _objPrivilegeEdit = new ObjectPrivilegesEdit();
      panObjPrivileges.Controls.Add(_objPrivilegeEdit);
      _objPrivilegeEdit.Parent = panObjPrivileges;
      _objPrivilegeEdit.Dock = DockStyle.Fill;
    }

    private void InitializeUserControls( )
    {
      _roleEdit.UserName = _userName;
      _roleEdit.ConnectionParams = _cp;

      _sysPrivilegeEdit.Principal = _userName;
      _sysPrivilegeEdit.ConnectionParams = _cp;

      _objPrivilegeEdit.Principal = _userName;
      _objPrivilegeEdit.ConnectionParams = _cp;
    }

    private void _parentContent_FormClosed( object sender, FormClosedEventArgs e )
    {
      _parentContent.FormClosed -= new FormClosedEventHandler(_parentContent_FormClosed);
      _parentContent = null;
      btnGoToParent.Visible = false;
    }

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }

    private void frmModifyUser_FormClosed( object sender, FormClosedEventArgs e )
    {
      if (_parentContent == null)
        return;

      _parentContent.FormClosed -= new FormClosedEventHandler(_parentContent_FormClosed);
    }

    private void toolStripButton1_Click( object sender, EventArgs e )
    {
      GoToParentList();
    }

    private void usersListToolStripMenuItem_Click( object sender, EventArgs e )
    {
      GoToParentList();
    }

    private void toolStripButton1_Click_1( object sender, EventArgs e )
    {
      InitializeUserControls();
    }   
  }
}