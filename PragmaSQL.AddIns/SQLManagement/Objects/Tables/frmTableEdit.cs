using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmTableEdit : DockContent
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

    private TableEdit _tableEdit = null;
    public TableEdit TableEdit
    {
      get { return _tableEdit; }
    }


    #endregion //Fields and properties

    #region Static Show

    public static void CreateTable( )
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



      frmTableEdit frm = new frmTableEdit();
      frm.Caption = String.Format("New Table ({0} on {1})", srv.SelNode.ServerName, srv.SelNode.DatabaseName);
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;

      frm.TableEdit.CreateTable(frm.ConnectionParams);
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    public static void EditTable()
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
      if (type != DBObjectType.UserTable )
      {
        MessageService.ShowError("Selected node is not a user defined table!");
        return;
      }

      frmTableEdit frm = new frmTableEdit();
      frm.Caption = String.Format("{0} ({1} on {2})",srv.SelNode.Name,srv.SelNode.ServerName,srv.SelNode.DatabaseName);
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;

      frm.TableEdit.ModifyTable(frm.ConnectionParams, srv.SelNode.id);
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static Show


    public frmTableEdit()
    {
      InitializeComponent();
      InitializeTableEditControl();
    }

    private void InitializeTableEditControl()
    {
      _tableEdit = new TableEdit();
      _tableEdit.Parent = this;
      _tableEdit.Dock = DockStyle.Fill;
      _tableEdit.AfterTableCreated += new EventHandler(_tableEdit_AfterTableCreated);
    }

    private void _tableEdit_AfterTableCreated( object sender, EventArgs e )
    {
      TableWrapper tbl = sender as TableWrapper;
      Caption = tbl.Name + " {"  + _cp.InfoDbServer + "}";
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }

    private void frmTableEdit_Shown(object sender, EventArgs e)
    {
      if (_tableEdit != null)
        _tableEdit.PostInitialize();
    }
  }
}