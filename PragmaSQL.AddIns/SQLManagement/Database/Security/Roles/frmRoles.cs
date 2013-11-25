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
  public partial class frmRoles : DockContent
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

    private RolesList _roleList = null;
    public RolesList RoleList
    {
      get { return _roleList; }
    }

    #endregion //Fields and properties
      
    #region Static show
    public static void ShowRoles( )
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

      frmRoles frm = new frmRoles();
      frm.Caption = "Roles : " + srv.SelNode.ServerName + " { " + srv.SelNode.DatabaseName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;
      frm.LoadRoles();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmRoles( )
    {
      InitializeComponent();
      InitializeRoleList();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeRoleList( )
    {
      // Login List
      _roleList = new RolesList();
      panRoles.Controls.Add(_roleList);
      _roleList.Parent = panRoles;
      _roleList.Dock = DockStyle.Fill;
      _roleList.ContextMenuStrip = contextMenuStrip1;
      panRoles.BringToFront();
    }

    public void LoadRoles( )
    {
      _roleList.LoadRoles(_cp);
    }

    #endregion //Initialization

    #region Internal Operations
    private void RefreshRolesList( )
    {
      _roleList.RefreshRoles();
    }

    private void DropSelectedRoles( )
    {
      _roleList.DropSelectedRoles(true);
    }

    private void ModifySelectedRoles( )
    {
      _roleList.ModifySelectedRoles(this);
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
      RefreshRolesList();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelectedRoles();
    }

    private void drepToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelectedRoles();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshRolesList();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifySelectedRoles();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifySelectedRoles();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      if (frmNewRole.ShowNewRoleDialog(_cp) == DialogResult.OK)
        RefreshRolesList();

    }

  }
}