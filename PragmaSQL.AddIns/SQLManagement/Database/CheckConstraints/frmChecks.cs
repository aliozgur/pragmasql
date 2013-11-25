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
  public partial class frmChecks: DockContent
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

    private ChecksList _checksList = null;
    public ChecksList ChecksList
    {
      get { return _checksList; }
    }

    #endregion //Fields and properties
      
    #region Static show
    public static void ShowChecks( )
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

      frmChecks frm = new frmChecks();
      frm.Caption = "Check Constraints : " + srv.SelNode.ServerName + " { " + srv.SelNode.DatabaseName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;
      frm.LoadChecks();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmChecks( )
    {
      InitializeComponent();
      InitializeChecksList();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeChecksList( )
    {
      // Login List
      _checksList = new ChecksList();
      panChecks.Controls.Add(_checksList);
      _checksList.Parent = panChecks;
      _checksList.Dock = DockStyle.Fill;
      _checksList.ContextMenuStrip = contextMenuStrip1;
      panChecks.BringToFront();
    }

    public void LoadChecks( )
    {
      _checksList.LoadChecks(_cp);
    }

    #endregion //Initialization

    #region Internal Operations
    private void RefreshChecksList( )
    {
      _checksList.RefreshChecks();
    }

    private void DropSelectedChecks( )
    {
      _checksList.DropSelectedChecks(true);
    }

    private void ModifySelectedChecks( )
    {
      _checksList.ModifySelectedChecks();
    }

    private void CreateNewCheck( )
    {
      frmCheckEdit.ShowCreateCheck(_cp);
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
      RefreshChecksList();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelectedChecks();
    }

    private void drepToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelectedChecks();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshChecksList();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifySelectedChecks();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifySelectedChecks();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      CreateNewCheck();
    }

    private void newToolStripMenuItem_Click( object sender, EventArgs e )
    {
      CreateNewCheck();
    }

  }
}