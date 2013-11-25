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
  public partial class frmIndexes: DockContent
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
    public static void ShowIndexes( )
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
      int type = srv.SelNode.Type;
      if ( type != DBObjectType.UserTable && type != DBObjectType.SystemTable && type != DBObjectType.View)
      {
        MessageService.ShowError("Selected node can not have indexes!");
        return;
      }

      frmIndexes frm = new frmIndexes();
      frm.Caption = "Indexes: " +  srv.SelNode.Name + " {" + srv.SelNode.ConnParams.InfoDbServer + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;
      frm._indexList.ObjectId = srv.SelNode.id;
      frm._indexList.ObjectName = srv.SelNode.Name;
      frm._indexList.ObjectOwner = srv.SelNode.Owner;
      frm.LoadIndexes();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmIndexes( )
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

    private void CreateNewIndex( )
    {
      frmIndexEdit.ShowCreateIndex(_cp, _indexList.ObjectOwner, _indexList.ObjectId, _indexList.ObjectName);
    }

    private void ShowDBCC( )
    {
      _indexList.ShowDBCC();
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

    private void frmIndexes_Load(object sender, EventArgs e)
    {

    }

  }
}