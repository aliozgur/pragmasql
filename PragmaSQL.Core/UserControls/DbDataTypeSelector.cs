using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public partial class DbDataTypeSelector : UserControl
  {
    public DbDataTypeSelector( )
    {
      InitializeComponent();
    }

    [Browsable(false)]
    public string SelectedDataType
    {
      get
      { return cmbType.Text; }
      set 
      {
        if (cmbType.Items.Count == 0)
          return;

        if (!cmbType.Items.Contains(value))
          throw new Exception("Selection is not a valid data type");
        cmbType.SelectedItem = value;
      }
    }

    public int DTWidth
    {
      get
      {
        int result = 0;
        Int32.TryParse(txtWidth.Text, out result);
        return result;
      }
      set{ txtWidth.Text = value.ToString();}
    }

    public bool WidthEnabled
    {
      get { return txtWidth.Enabled; }
    }

    public int DTScale
    {
      get
      {
        int result = 0;
        Int32.TryParse(txtScale.Text, out result);
        return result;
      }
      set { txtScale.Text = value.ToString(); }
    }
    public bool ScaleEnabled
    {
      get { return txtScale.Enabled; }
    }

    public int DTPrecision
    {
      get
      {
        int result = -1;
        Int32.TryParse(txtPrecision.Text, out result);
        return result;
      }
      set { txtPrecision.Text = value.ToString(); }
    }

    public bool PrecisionEnabled
    {
      get { return txtPrecision.Enabled; }
    }
    [Browsable(false)]
    public ComboBox DataTypes
    {
      get
      {
        return cmbType;
      }
    }

    
    private ConnectionParams _cp;
    [Browsable(false)]
    public ConnectionParams Cp
    {
      get { return _cp; }
      set 
      {
        if (value != null)
          _cp = value.CreateCopy();
        else
          _cp = value; 
      }
    }

    private DataTable _tbl;
    [Browsable(false)]
    public DataTable DataTypesAsTable
    {
      get { return _tbl.Copy(); }
    }

    public void ClearWidth( )
    {
      txtWidth.Text = String.Empty;
    }

    public void ClearScale( )
    {
      txtScale.Text = String.Empty;
    }

    public void ClearPrecision( )
    {
      txtPrecision.Text = String.Empty;
    }

    public void ClearAll( )
    {
      ClearWidth();
      ClearScale();
      ClearPrecision();
    }

    public void ResetControl( )
    {
      _tbl = null;
      cmbType.Items.Clear();
      ClearAll();
    }

    public void LoadDataTypes( ConnectionParams cp , bool wantEmpty)
    {
      ConnectionParams tmpCp = cp;
      if (tmpCp == null)
        tmpCp = _cp;

      if (_tbl != null)
      {
        _tbl.PrimaryKey = null;
        _tbl = null;
      }

      cmbType.Items.Clear();
      ClearAll();

      string cmdText = ResManager.GetDBScript("Script_GetDataTypes");
      using (SqlConnection conn = tmpCp.CreateSqlConnection(true, false))
      {
        _tbl = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(cmdText, conn);
        adapter.Fill(_tbl);
      }

      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = _tbl.Columns["name"];
      _tbl.PrimaryKey = PrimaryKeyColumns;

      if(wantEmpty)
        cmbType.Items.Add(String.Empty);
      
      foreach (DataRow row in _tbl.Rows)
      {
        cmbType.Items.Add(((string)row["name"]).ToLowerInvariant());
      }

      if (cmbType.Items.Count > 0)
        cmbType.SelectedIndex = 0;

      EvaluateDataType();
    }

    private void EvaluateDataType( )
    {
      string type = cmbType.SelectedItem as string;
      string baseType = String.Empty;
      if (!String.IsNullOrEmpty(type))
      {
        DataRow row = _tbl.Rows.Find(type.ToLowerInvariant());
        baseType = (string)row["basename"];
        baseType = baseType.ToLowerInvariant();
      }
      switch (baseType)
      {
        case "bigint":
        case "bit":
        case "datetime":
        case "float":
        case "image":
        case "int":
        case "money":
        case "ntext":
        case "real":
        case "smalldatetime":
        case "smallint":
        case "smallmoney":
        case "sql_variant":
        case "text":
        case "timestamp":
        case "tinyint":
        case "uniqueidentifier":
        case "xml":
          txtWidth.Enabled = false;
          txtScale.Enabled = false;
          txtPrecision.Enabled = false;
          break;
        case "binary":
        case "char":
        case "nchar":
        case "nvarchar":
        case "varbinary":
        case "varchar":
          txtWidth.Enabled = true;
          txtWidth.Text = "8000";
          txtScale.Enabled = false;
          txtPrecision.Enabled = false;
          break;
        case "decimal":
        case "numeric":
          txtWidth.Enabled = false;
          txtScale.Text = "38";
          txtScale.Enabled = true;
          txtPrecision.Enabled = true;
          txtPrecision.Text = "38";
          break;
        default:
          txtWidth.Enabled = false;
          txtScale.Enabled = false;
          txtPrecision.Enabled = false;
          break;
      }
    }

    private void cmbType_SelectedIndexChanged( object sender, EventArgs e )
    {
      EvaluateDataType();
    }
  }
}
