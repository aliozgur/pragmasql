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
  public partial class frmAllIndexes: DockContent
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

    private IndexList _indexList = null;
    public IndexList IndexList
    {
      get { return _indexList; }
    }


    #endregion //Fields and properties
      
    #region Static show
    public static void ShowAllIndexes( )
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


      frmAllIndexes frm = new frmAllIndexes ();
      frm.Caption = String.Format("Indexes ({0} on {1})", srv.SelNode.ServerName, srv.SelNode.DatabaseName);
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;

      frm._indexList.ShowAllIndexes = true;
      frm._indexList.ObjectNameVisible = true;

      frm.LoadIndexes();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmAllIndexes( )
    {
      InitializeComponent();
      InitializeIndexList();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeIndexList( )
    {
      // Login List
      _indexList = new IndexList();
      panIndexes.Controls.Add(_indexList);
      _indexList.Parent = panIndexes;
      _indexList.Dock = DockStyle.Fill;
      _indexList.ContextMenuStrip = contextMenuStrip1;
      panIndexes.BringToFront();
    }

    public void LoadIndexes( )
    {
      _indexList.ConnectionParams = _cp;
    }


    #endregion //Initialization

    #region Internal Operations
    private void RefreshIndexes( )
    {
      _indexList.RefreshIndexes();
    }

    private void DropSelectedIndexes( )
    {
      _indexList.DropSelectedIndexes(true);
    }

    private void ModifySelectedIndexes( )
    {
      _indexList.ModifySelectedIndexes();
    }

    private void ShowDBCC( )
    {
      _indexList.ShowDBCC();
    }

    private void CreateNewIndex( )
    {
      DbObjectSelectorItem item = frmDbObjectSelector.ShowSelector(_cp);
      if (item == null)
        return;
      frmIndexEdit.ShowCreateIndex(_cp, item.Owner, item.ID, item.Name);
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
      RefreshIndexes();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelectedIndexes();
    }

    private void drepToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelectedIndexes();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshIndexes();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifySelectedIndexes();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifySelectedIndexes();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      CreateNewIndex();
    }

    private void newToolStripMenuItem_Click( object sender, EventArgs e )
    {
      CreateNewIndex();
    }

    private void toolStripButton5_Click( object sender, EventArgs e )
    {
      ShowDBCC();
    }

  }
}