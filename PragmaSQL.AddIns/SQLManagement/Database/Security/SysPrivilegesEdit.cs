using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class SysPrivilegesEdit : UserControl
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
          PopulateSystemPrivileges();
        }
        else
        {
          _cp = null;
          bsPrivileges.DataSource = null;
          _tblPrivileges.Clear();
        }
      }
    }

    private string _principal = String.Empty;
    public string Principal
    {
      get { return _principal; }
      set { _principal = value; }
    }

    private DataTable _tblPrivileges = new DataTable();

    #endregion //Fields And properties

    #region CTOR
    public SysPrivilegesEdit( )
    {
      InitializeComponent();
      SetModified(false);
    }

    #endregion //CTOR

    #region Private Methods

    private void InitializeSystemPrivilegesTable( )
    {
      DataColumn column;

      _tblPrivileges.Clear();
      _tblPrivileges.PrimaryKey = null;
      _tblPrivileges.Columns.Clear();
      _tblPrivileges.PrimaryKey = null;


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "isin";
      _tblPrivileges.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "original";
      _tblPrivileges.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "action";
      _tblPrivileges.Columns.Add(column);

      DataRow row = _tblPrivileges.NewRow();
      row["isin"] = false;
      row["original"] = false;
      row["action"] = "Backup Database";
      _tblPrivileges.Rows.Add(row);

      row = _tblPrivileges.NewRow();
      row["isin"] = false;
      row["original"] = false;
      row["action"] = "Backup Transaction";
      _tblPrivileges.Rows.Add(row);

      row = _tblPrivileges.NewRow();
      row["isin"] = false;
      row["original"] = false;
      row["action"] = "Create Default";
      _tblPrivileges.Rows.Add(row);

      row = _tblPrivileges.NewRow();
      row["isin"] = false;
      row["original"] = false;
      row["action"] = "Create Procedure";
      _tblPrivileges.Rows.Add(row);

      row = _tblPrivileges.NewRow();
      row["isin"] = false;
      row["original"] = false;
      row["action"] = "Create Rule";
      _tblPrivileges.Rows.Add(row);

      row = _tblPrivileges.NewRow();
      row["isin"] = false;
      row["original"] = false;
      row["action"] = "Create Table";
      _tblPrivileges.Rows.Add(row);

      row = _tblPrivileges.NewRow();
      row["isin"] = false;
      row["original"] = false;
      row["action"] = "Create View";
      _tblPrivileges.Rows.Add(row);

      // Make the action column the primary key column.
      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = _tblPrivileges.Columns["action"];
      _tblPrivileges.PrimaryKey = PrimaryKeyColumns;

      _tblPrivileges.ColumnChanged += new DataColumnChangeEventHandler(_tblPrivileges_ColumnChanged);
    }

    void _tblPrivileges_ColumnChanged( object sender, DataColumnChangeEventArgs e )
    {
      SetModified(true);
    }

    public void PopulateSystemPrivileges( )
    {
      SqlDataReader reader = null;
      DataRow row = null;

      bsPrivileges.DataSource = null;
      InitializeSystemPrivilegesTable();

      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        try
        {
            reader = DbCmd.GetSystemPrivilegesAsDataReader(conn, Principal);

            string action = String.Empty;
            string protectType = String.Empty;

            while (reader.Read())
            {
              if (!Utils.IsReaderItemValid(reader, "ProtectType"))
                continue;

              protectType = (string)reader["ProtectType"];
              if (protectType.ToLowerInvariant().Trim() != "grant")
                continue;

              action = (string)reader["Action"];
              row = _tblPrivileges.Rows.Find(action.Trim());

              if (row == null)
                continue;

              row[0] = true;
              row[1] = true;
            }

        }
        catch (Exception ex)
        {
          string errMsg = "System privileges not available! Principal: {0}, Error message: {1}";
          errMsg = String.Format(errMsg, _principal, ex.Message);
          HostServicesSingleton.HostServices.MsgService.ErrorMsg(errMsg, (MethodInfo)MethodInfo.GetCurrentMethod());
        }
        finally
        {
          if (reader != null)
            reader.Close();
        }
        bsPrivileges.DataSource = _tblPrivileges;
        SetModified(false);
      }
    }

    private void SetModified( bool value )
    {
      lblModified.Visible = value;
      pbModified.Visible = value;
    }

    private void UpdatePrivileges( )
    {
      bool isin = false;
      bool original = false;
      string action = String.Empty;

      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        foreach (DataRow row in _tblPrivileges.Rows)
        {
          if (!Utils.IsRowItemValid(row, 0) || !Utils.IsRowItemValid(row, 1) || !Utils.IsRowItemValid(row, 2))
            continue;

          isin = (bool)row.ItemArray[0];
          original = (bool)row.ItemArray[1];
          action = (string)row.ItemArray[2];

          if (isin != original)
          {
            switch (isin)
            {
              case true:
                DbCmd.GrantSystemPrivilege(conn, _principal, action);
                break;
              case false:
                DbCmd.RevokeSystemPrivilege(conn, _principal, action);
                break;
              default:
                break;
            }
          }
        }
      }

      SetModified(false);
    }

    #endregion //Private Methods

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      UpdatePrivileges();
    }
  }
}
