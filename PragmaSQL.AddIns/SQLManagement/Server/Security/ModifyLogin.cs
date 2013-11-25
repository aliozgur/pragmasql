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
  public partial class ModifyLogin : UserControl
  {
    private DataTable _tblRoles = new DataTable();
    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      private set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
        }
        else
        {
          _cp = null;
        }
      }
    }

    private string _loginName = String.Empty;
    public string LoginName
    {
      get { return _loginName; }
      set 
      { 
        _loginName = value;
        CheckUserOnline();
        PopulateServerRoles();
      }
    }

    private string _defaultDb = String.Empty;
    public string DefaultDb
    {
      get { return _defaultDb; }
      set 
      { 
        _defaultDb = value;
        if (!String.IsNullOrEmpty(_defaultDb) && !cmbDb.Items.Contains(_defaultDb))
        {
          cmbDb.Items.Add(value);
          lblDbError.Visible = true;
        }
        else
        {
          lblDbError.Visible = false;
        }

        if (String.IsNullOrEmpty(_defaultDb))
          cmbDb.Text = "<Default>";
        else
          cmbDb.Text = value;
      }
    }

    private string _defaultLanguage = String.Empty;
    public string DefaultLanguage
    {
      get { return _defaultLanguage; }
      set 
      { 
        _defaultLanguage = value;
        if (!String.IsNullOrEmpty(_defaultLanguage) && !cmbLanguage.Items.Contains(_defaultLanguage))
        {
          cmbLanguage.Items.Add(value);
          lblLangError.Visible = true;
        }
        else
        {
          lblLangError.Visible = false;        
        }

        if (String.IsNullOrEmpty(_defaultLanguage))
          cmbLanguage.Text = "<Default>";
        else
          cmbLanguage.Text = value;

      }
    }

    private string SelDatabase
    {
      get
      {
        return cmbDb.Text == "<Default>" ? String.Empty : cmbDb.Text;
      }
    }

    private string SelLanguage
    {
      get
      {
        return cmbLanguage.Text == "<Default>" ? String.Empty : cmbLanguage.Text;
      }
    }

    public ModifyLogin(ConnectionParams cp)
    {
      InitializeComponent();
      ConnectionParams = cp;
      PopulateDbsAndLanguages();
      CreateServerRolesTable();
    }

    private void PopulateDbsAndLanguages( )
    {
      DbCmd.PopulateDbCombo(cmbDb, _cp);
      DbCmd.PopulateLanguagesCombo(cmbLanguage, _cp);
    }

    private void CreateServerRolesTable( )
    {
      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "isin";
      _tblRoles.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "ServerRole";
      _tblRoles.Columns.Add(column);
    }

    private void PopulateServerRoles( )
    {
      _tblRoles.Clear();

      DataRow row = null;
      DataTable isInTbl = null;
      string isInRoleCmd = "SELECT IS_SRVROLEMEMBER('{0}','{1}') as isin";
      string cmdText = "EXEC sp_helpsrvrole";
      SqlDataReader reader = DbCmd.ExecuteReader(cmdText, _cp.CreateSqlConnection(true));
      try
      {
        while (reader.Read())
        {
          row = _tblRoles.NewRow();
          row["ServerRole"] = reader["ServerRole"];
          row["isin"] = false;

          isInTbl = DbCmd.ExecuteDataTable(String.Format(isInRoleCmd, (string)reader["ServerRole"], Utils.ReplaceQuatations(_loginName)), _cp);
          if (isInTbl != null && isInTbl.Rows.Count != 0)
          {
            if (isInTbl.Rows[0].ItemArray[0] != null && isInTbl.Rows[0].ItemArray[0].GetType() != typeof(DBNull))
            {
              switch (isInTbl.Rows[0].ItemArray[0].ToString())
              {
                case "1":
                  row["isin"] = true;
                  break;
                default:
                  break;
              }
            }
          }
          isInTbl = null;
          _tblRoles.Rows.Add(row);
        }

        bsRoles.DataSource = _tblRoles;
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
    }

    private void CheckUserOnline( )
    {
      string cmdText = "SELECT sid FROM master.dbo.sysprocesses WHERE sid=SUSER_SID('" + Utils.ReplaceQuatations(_loginName) + "')";
      DataTable tbl = DbCmd.ExecuteDataTable(cmdText,_cp);
      checkBox1.Checked = !(tbl == null || tbl.Rows.Count == 0);
    }

    public bool UpdateLogin( )
    {
      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        string serverVersion = DbCmd.QueryServerVersion(_cp);
        if (String.IsNullOrEmpty(serverVersion))
        {
          MessageService.ShowError("Can not retreive server version!");
          return false;
        }

        if (cmbDb.Text != _defaultDb)
        {
          DbCmd.ChangeDefaultDB(conn, serverVersion, _loginName, cmbDb.Text);
        }
        if (cmbLanguage.Text != _defaultLanguage)
        {
          DbCmd.ChangeDefaultLanguage(conn, serverVersion, _loginName, cmbLanguage.Text);
        }

        bool isinrole = false;
        string serverrole = String.Empty;
        foreach (DataRow row in _tblRoles.Rows)
        {
          if (row["ServerRole"] == null || row["ServerRole"].GetType() == null)
            continue;
          isinrole = (bool)row["isin"];
          if (isinrole)
          {
            if (DbCmd.IsInServerRole(conn, _loginName, (string)row["ServerRole"]))
              continue;
            DbCmd.AddToServerRole(conn, _loginName, (string)row["ServerRole"]);
          }
          else
          {
            if (!DbCmd.IsInServerRole(conn, _loginName, (string)row["ServerRole"]))
              continue;
            DbCmd.DropServerRole(conn, _loginName, (string)row["ServerRole"]);          
          }
        }
      }
      return true;
    }

    

  }
}
