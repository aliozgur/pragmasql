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
  public partial class frmUdtList: DockContent
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

   
    #endregion //Fields and properties
      
    #region Static show
    public static void ShowUdtList( )
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

      frmUdtList frm = new frmUdtList();
      frm.Caption = "Udt : " + srv.SelNode.ServerName + " { " + srv.SelNode.DatabaseName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;
      frm.LoadUdtList();
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmUdtList( )
    {
      InitializeComponent();
    }

    #endregion //CTOR

    #region Initialization



    public void LoadUdtList( )
    {
      bs.DataSource = DbCmd.GetUserDataTypes(_cp);
      grd.DataSource = bs;
    }

    #endregion //Initialization

    #region Internal Operations
    private void RefreshList( )
    {
      LoadUdtList();
    }

    private void DropSelected( )
    {
      if (grd.SelectedRows.Count == 0)
        return;

      if (!MessageService.AskQuestion("Are you sure you want to delete selected types?"))
        return;

      UdtWrapper udt = null;
      string errorText = String.Empty;
      bool ok = false;
      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (!Utils.IsDbValueValid(row.Cells[0].Value))
          continue;        
        udt = new UdtWrapper(_cp);
        udt.ID = (long)((short)row.Cells[0].Value);
        udt.Name = (string)row.Cells[2].Value;
        try
        {
          udt.Drop();
          ok = true;
        }
        catch(Exception ex)
        {
          if(String.IsNullOrEmpty(errorText))
            errorText += "Can not drop data type(s) listed below.\r\n";
          errorText += " - " + (string)row.Cells[2].Value + " : " + ex.Message;
        }
      }


      if (!String.IsNullOrEmpty(errorText))
        MessageService.ShowError(errorText);

      if (ok)
        LoadUdtList();
    }

    private void ModifySelected( )
    {
      if (grd.SelectedRows.Count == 0)
        return;

      DataGridViewRow  row = grd.SelectedRows[0];

      if (!Utils.IsDbValueValid(row.Cells[0].Value))
        return;

      long id = (long)((short)row.Cells[0].Value);
      frmUdtEdit.ShowUdt(_cp, id);
    }

    private void CreateNew( )
    {
      frmUdtEdit.ShowUdt(_cp,-1);

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
      RefreshList();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelected();
    }

    private void drepToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelected();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshList();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifySelected();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifySelected();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      CreateNew();
    }

    private void newToolStripMenuItem_Click( object sender, EventArgs e )
    {
      CreateNew();
    }

  }
}