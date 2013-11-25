using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class DbFiles : UserControl
  {
    #region Fields And properties

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = null;
          _cp = value.CreateCopy();
          PopulateDatabaseFileInformation();
        }
        else
        {
          _cp = null;
          UnbindAllSources();
        }
      }
    }

    private DataTable _tblSpaceUsage = new DataTable();

    #endregion //Fields And properties

    public DbFiles( )
    {
      InitializeComponent();
      InitializeSpaceUsageTable();
    }
    
    private void UnbindAllSources( )
    {
      bsDataFiles.DataSource = null;
      bsLogs.DataSource = null;
      bsSpaceUsage.DataSource = null;
    }

    private void InitializeSpaceUsageTable( )
    {
      _tblSpaceUsage.Clear();
      _tblSpaceUsage.Columns.Clear();
      _tblSpaceUsage.PrimaryKey = null;

      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "prop";
      _tblSpaceUsage.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "value";
      _tblSpaceUsage.Columns.Add(column);

      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = _tblSpaceUsage.Columns["prop"];
      _tblSpaceUsage.PrimaryKey = PrimaryKeyColumns;


      DataRow row = _tblSpaceUsage.NewRow();
      row["prop"] = "Database Size";
      row["value"] = String.Empty;
      _tblSpaceUsage.Rows.Add(row);

      row = _tblSpaceUsage.NewRow();
      row["prop"] = "Data";
      row["value"] = String.Empty;
      _tblSpaceUsage.Rows.Add(row);

      row = _tblSpaceUsage.NewRow();
      row["prop"] = "Indexes";
      row["value"] = String.Empty;
      _tblSpaceUsage.Rows.Add(row);

      row = _tblSpaceUsage.NewRow();
      row["prop"] = "Unused";
      row["value"] = String.Empty;
      _tblSpaceUsage.Rows.Add(row);

      row = _tblSpaceUsage.NewRow();
      row["prop"] = "Unreserved";
      row["value"] = String.Empty;
      _tblSpaceUsage.Rows.Add(row);
    }

    private void PopulateDatabaseFileInformation( )
    {
      DataRow row = null;
      DataTable tbl = null;
      SqlDataReader reader = null;
      UnbindAllSources();
      using (SqlConnection conn = _cp.CreateSqlConnection(true,false))
      {
        // 1 - Get data file details 
        tbl = DbCmd.GetDataFiles(conn);
        bsDataFiles.DataSource = tbl;

        // 2- Get transaction log details
        tbl = DbCmd.GetLogFiles(conn);
        bsLogs.DataSource = tbl;
        

        // 3- Get space usage details
        try
        {

          // 3.1 Get general space usage details
          reader = DbCmd.GetSpaceUsage(conn);
          reader.Read();

          row = _tblSpaceUsage.Rows.Find("Database Size");
          if (row != null)
          {
            row[1] = (string)reader["database_size"];
          }

          row = _tblSpaceUsage.Rows.Find("Unreserved");
          if (row != null)
          {
            row[1] = (string)reader["unallocated space"];
          }

          // 3.2 Get other space usage details
          reader.NextResult();
          reader.Read();

          row = _tblSpaceUsage.Rows.Find("Data");
          if (row != null)
          {
            row[1] = (string)reader["data"];
          }

          row = _tblSpaceUsage.Rows.Find("Indexes");
          if (row != null)
          {
            row[1] = (string)reader["index_size"];
          }

          row = _tblSpaceUsage.Rows.Find("Unused");
          if (row != null)
          {
            row[1] = (string)reader["unused"];
          }

          bsSpaceUsage.DataSource = _tblSpaceUsage;
         
        }
        finally
        {
          if (reader != null)
            reader.Close();
        }
      }
    }

    private bool ModifyDatabaseFileClick( BindingSource bs )
    {
      string fileName = String.Empty;
      double size = 0;
      double growth = 0;
      double maxSize = 0;
      double growthPerc = 0;


      DataRowView row = bs.Current as DataRowView;

      if (row == null)
        return false;

      fileName = (string)row["name"];
      size = Convert.ToDouble(row["size"]);
      maxSize = Convert.ToDouble(row["maxsize"]);
      growth = Convert.ToDouble(row["growth"]);
      growthPerc = Convert.ToDouble(row["growthPerc"]);

      return frmModifyDatabaseFile.ShowModifyDbFileDialog(_cp, fileName, size, growth, growthPerc, maxSize) == DialogResult.OK;
    }

    private void grdDataFiles_CellContentClick( object sender, DataGridViewCellEventArgs e )
    {
      if (e.ColumnIndex != 6)
        return;

      if (ModifyDatabaseFileClick(bsDataFiles))
        PopulateDatabaseFileInformation();
    }

    private void grdLogs_CellContentClick( object sender, DataGridViewCellEventArgs e )
    {
      if (e.ColumnIndex != 5)
        return;

      if (ModifyDatabaseFileClick(bsLogs))
        PopulateDatabaseFileInformation();
    }
  }
}
