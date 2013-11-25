using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI;
using PragmaSQL.Database;
using PragmaSQL.Common;

namespace PragmaSQL.GUI
{
  public partial class frmDbObjectSearch : DockContent
  {
    private SqlConnection _conn = new SqlConnection();
    private BindingSource _bs = new BindingSource();
    private DataTable _dataTbl = new DataTable("Data");
    private SqlDataAdapter _adapter = new SqlDataAdapter();

    private string _searchText = String.Empty;
    public string SearchText
    {
      get { return _searchText; }
      set 
      { 
        _searchText = value;
        txtCriteria.Text = value;
      }
    }

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
    }

    private string _caption = String.Empty;
    public string Caption
    {
      get { return _caption; }
      set
      {
        _caption = value;
        this.Text = value;
        this.TabText = value;
      }
    }


    public frmDbObjectSearch()
    {
      InitializeComponent();
    }

    public void InitializeForm(string caption, string searchText, ConnectionParams connParams)
    {
      if (connParams == null)
      {
        throw new NullParameterException("Connection params object is null!");
      }

      _connParams = connParams.CreateCopy();
      Caption = caption;
      SearchText = searchText;
    }

    public void LoadData()
    {
      if (_conn.State != ConnectionState.Open)
      {
        _conn.ConnectionString = _connParams.ConnectionString;
        _conn.Open();
      }

      _dataTbl.Clear();

      string cmdText = global::PragmaSQL.Properties.Resources.Script_SearchDb;
      cmdText = cmdText.Replace("$searchText$", "'" + txtCriteria.Text + "'");

      SqlCommand cmd = new SqlCommand(cmdText, _conn);
      _adapter.SelectCommand = cmd;
      _adapter.Fill(_dataTbl);

      _bs.DataSource = _dataTbl;
      grd.DataSource = _bs;
      foreach (DataGridViewColumn col in grd.Columns)
      {
        if 
          (
            (col.DataPropertyName != "ObjName") && ( col.DataPropertyName != "ParentObjName" )
            && (col.DataPropertyName != "DisplayType") && (col.DataPropertyName != "ParentType")
          )
        {
          col.Visible = false;
        }
      }

      grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
    }

   


    private void btnFilter_Click(object sender, EventArgs e)
    {
      LoadData();
    }

    private void txtCriteria_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        LoadData();
      }
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Program.MainForm.CloseDocuments(null);
    }

    private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Program.MainForm.CloseDocuments(this);
    }
  }
}