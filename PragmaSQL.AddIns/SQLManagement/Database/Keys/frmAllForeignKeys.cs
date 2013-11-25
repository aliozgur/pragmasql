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
  public partial class frmAllForeignKeys: DockContent
  {
    #region Fields and properties
    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set 
      { 
        _cp = value;
        if (_foreignKeyEdit != null)
          _foreignKeyEdit.ConnectionParams = value;
      }
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

    private ForeignKeyEdit _foreignKeyEdit = null;
    public ForeignKeyEdit ForeignKeyEdit
    {
      get { return _foreignKeyEdit; }
    }

    private DataTable _tbl = null;

    public bool KeyPanelVisible
    {
      get
      {
        return panKeyEdit.Visible;
      }
      set
      {
        splitter1.Visible = value;
        panKeyEdit.Visible = value;
        panKeyEdit.SendToBack();

        if (!value)
          _foreignKeyEdit.ResetEditor();
      }
    }

    #endregion //Fields and properties
      
    #region Static show
    public static void ShowAllForeignKeys( )
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


      frmAllForeignKeys frm = new frmAllForeignKeys();

      ConnectionParams cp = srv.SelNode.ConnParams.CreateCopy();
      cp.Database = srv.SelNode.DatabaseName;
      frm.ConnectionParams = cp;
      frm.Caption = String.Format("Foreign Keys ({0} on {1})", srv.SelNode.ServerName, srv.SelNode.DatabaseName);
      cp = null;

      frm.LoadKeys();
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
      
    }

    #endregion //Static show

    #region CTOR

    public frmAllForeignKeys( )
    {
      InitializeComponent();
      InitializeForeignKeyEditor();
      KeyPanelVisible = false;
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeForeignKeyEditor( )
    {
      _foreignKeyEdit = new ForeignKeyEdit();
      panKeyEdit.Controls.Add(_foreignKeyEdit);
      _foreignKeyEdit.Parent = panKeyEdit;
      _foreignKeyEdit.Dock = DockStyle.Fill;
      _foreignKeyEdit.BringToFront();
      _foreignKeyEdit.CanUserAddNewKey = false;

      _foreignKeyEdit.AfterKeyDropped += new ForeignKeyEditorEventHandler(_foreignKeyEdit_AfterKeyDropped);
      _foreignKeyEdit.AfterKeyRenamed += new ForeignKeyEditorEventHandler(_foreignKeyEdit_AfterKeyRenamed);
      _foreignKeyEdit.AfterNewKeySaved += new ForeignKeyEditorEventHandler(_foreignKeyEdit_AfterNewKeySaved);
    }

    void _foreignKeyEdit_AfterNewKeySaved( object sender, ForeignKeyWrapper key )
    {
      ChangeHeaderText(key.HostTable.FullName, key.Name);
    }

    void _foreignKeyEdit_AfterKeyRenamed( object sender, ForeignKeyWrapper key )
    {
      ChangeHeaderText(key.HostTable.FullName, key.Name);      
    }

    void _foreignKeyEdit_AfterKeyDropped( object sender, ForeignKeyWrapper key )
    {
      KeyPanelVisible = false;
    }

    public void LoadKeys( )
    {
      bs.DataSource = null;
      _tbl = null;

      _tbl = DbCmd.GetAllForeignKeys(_cp);
      _tbl.PrimaryKey = null;
      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = _tbl.Columns["id"];
      _tbl.PrimaryKey = PrimaryKeyColumns; 
      
      bs.DataSource = _tbl;
    }

    #endregion //Initialization

    #region Internal Operations
   

    private void UpdateHeaderText( )
    {
      if (grd.CurrentRow == null)
      {
        lblHeader.Text = String.Empty;
        return;
      }

      DataGridViewRow row = grd.CurrentRow;
      ChangeHeaderText((string)row.Cells[3].Value, (string)row.Cells[4].Value);    
    }

    private void ChangeHeaderText( string hostTable, string key )
    {
      lblHeader.Text = " Foreign Keys {" + hostTable + "} " + key;        
    }
    private void RefreshForeignKeys( )
    {
      LoadKeys();
      //KeyPanelVisible = false;
    }

    private void DropSelectedForeignKeys( )
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      if (!MessageService.AskQuestion("Are you sure you want to drop selected foreign keys?"))
        return;

      ForeignKeyWrapper key = null;
      try
      {
        IList<DataGridViewRow > delList = new List<DataGridViewRow>();
        foreach (DataGridViewRow row in grd.SelectedRows)
        {
          key = new ForeignKeyWrapper(_cp);
          key.ID = (int)row.Cells[0].Value;
          ForeignKeyWrapper.Drop(_cp, (string)row.Cells[3].Value, (string)row.Cells[4].Value);
          delList.Add(row);
        }

        foreach (DataGridViewRow row in delList)
        {
          grd.Rows.Remove(row);
        }
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
      finally
      {
        //KeyPanelVisible = false;
      }
    }

    private void ModifySelectedForeignKey( )
    {
      if (grd.CurrentRow == null)
      {
        KeyPanelVisible = false;
        return;
      }

      DataGridViewRow row = grd.CurrentRow;
      
      long tableId = (int)row.Cells[1].Value;
      TableWrapper hostTbl = new TableWrapper(tableId);
      hostTbl.ConnectionParams = _cp;
      hostTbl.LoadProperties();
      _foreignKeyEdit.InitializeForeignKeys(TableKeyEditorMode.SingleTable, hostTbl, (int)row.Cells[0].Value);

      UpdateHeaderText();
      KeyPanelVisible = true;
    }

    private void CreateNewKey()
    {
      lblHeader.Text = "New Foreign Key";
      _foreignKeyEdit.ExternalCreateNewKey();
      KeyPanelVisible = true;
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
      RefreshForeignKeys();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelectedForeignKeys();
    }

    private void drepToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelectedForeignKeys();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshForeignKeys();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifySelectedForeignKey();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifySelectedForeignKey();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      CreateNewKey();
    }

    private void newToolStripMenuItem_Click( object sender, EventArgs e )
    {
      CreateNewKey();
    }

    private void label1_Click( object sender, EventArgs e )
    {
      KeyPanelVisible = false;
    }

    private void grd_DoubleClick( object sender, EventArgs e )
    {
      ModifySelectedForeignKey();
    }
  }
}