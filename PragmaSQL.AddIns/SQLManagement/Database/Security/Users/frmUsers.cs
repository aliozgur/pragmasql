using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmUsers : DockContent
  {
    #region Fields and properties
    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set { _cp = value; }
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

    private UsersList _userList = null;
    public UsersList UserList
    {
      get { return _userList; }
    }

    #endregion //Fields and properties
      
    #region Static show
    
    public static void ShowUsers( )
    {
      IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
      if (srv == null)
      {
        MessageService.ShowError("No object explorer available!");
        return;
      }

      if (srv.SelNode == null || srv.SelNode.ConnParams == null)
      {
        MessageService.ShowError("Database data is not available!");
        return;
      }

      if (String.IsNullOrEmpty(srv.SelNode.ConnParams.Database))
      {
        MessageService.ShowError("Selected node is not a database or child of a database!");
        return;
      }

      frmUsers frm = new frmUsers();
      frm.Caption = "Users : " + srv.SelNode.ServerName + " { " + srv.SelNode.DatabaseName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;
      frm.LoadUsers();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmUsers( )
    {
      InitializeComponent();
      InitializeUserList();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeUserList( )
    {
      // Login List
      _userList = new UsersList();
      panUsers.Controls.Add(_userList);
      _userList.Parent = panUsers;
      _userList.Dock = DockStyle.Fill;
      _userList.ContextMenuStrip = contextMenuStrip1;
      panUsers.BringToFront();
    }

    public void LoadUsers( )
    {
      _userList.LoadUsers(_cp);
    }

    #endregion //Initialization

    #region Internal Operations
    private void RefreshUsersList( )
    {
      _userList.RefreshUsers();
    }

    private void DropSelectedUsers( )
    {
      _userList.DropSelectedUsers(true);
    }

    private void ModifySelectedUsers( )
    {
      _userList.ModifySelectedUsers(this);
    }

    #endregion //Internal Operations

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      this.Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }

    private void toolStripButton1_Click( object sender, EventArgs e )
    {
      RefreshUsersList();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelectedUsers();
    }

    private void drepToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelectedUsers();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshUsersList();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifySelectedUsers();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifySelectedUsers();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      if (frmNewUser.ShowNewUserDialog(_cp) == DialogResult.OK)
        RefreshUsersList();
    }
  }
}