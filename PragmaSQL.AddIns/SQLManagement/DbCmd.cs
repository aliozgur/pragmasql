/********************************************************************
  Class ServerUtils
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public static class DbCmd
  {

    #region Server version
    public static string QueryServerVersion(ConnectionParams cp)
    {
      return QueryServerVersion(cp.CreateSqlConnection(false, false));
    }

    public static string QueryServerVersion(SqlConnection conn)
    {
      try
      {
        DataTable tbl = ExecuteDataTable("EXEC master.dbo.xp_msver 'ProductVersion'", conn);
        object value = tbl.Rows[0]["Character_Value"];
        return value != null && value.GetType() != typeof(DBNull) ? value.ToString() : String.Empty;
      }
      catch (Exception ex)
      {
        HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex.Message, (MethodInfo)MethodInfo.GetCurrentMethod());
        return String.Empty;
      }
    }
    #endregion //Server version

    #region Combo population
    public static void PopulateDbCombo( ComboBox cmb, ConnectionParams cp )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      DataTable tbl = DbCmd.GetDatabasesAsDataTable(cp);

      DataRow newRow = tbl.NewRow();
      newRow["description"] = "<Default>";
      newRow["name"] = String.Empty;
      tbl.Rows.Add(newRow);

      foreach (DataRow row in tbl.Rows)
      {
        cmb.Items.Add(row["description"] != null && row["description"].GetType() != typeof(DBNull) ? (string)row["description"] : String.Empty);
      }
    }

    public static void PopulateLanguagesCombo( ComboBox cmb, ConnectionParams cp )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      DataTable tbl = DbCmd.GetLanguages(cp);

      DataRow newRow = tbl.NewRow();
      newRow["description"] = "<Default>";
      newRow["alias"] = String.Empty;
      tbl.Rows.Add(newRow);

      foreach (DataRow row in tbl.Rows)
      {
        cmb.Items.Add(row["description"] != null && row["description"].GetType() != typeof(DBNull) ? (string)row["description"] : String.Empty);
      }
    }

    public static void PopulateLoginsCombo( ComboBox cmb, ConnectionParams cp )
    {

      if (cmb == null)
        return;

      cmb.Items.Clear();

      string cmdText = "USE [master]; ";
      cmdText += " SELECT loginname"
        + " FROM master.dbo.syslogins"
        + " ORDER BY loginname";

      cmb.Items.Add("");
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = ExecuteReader(cmdText, conn);
        while (reader.Read())
        {
          cmb.Items.Add(reader["loginname"]);
        }
        reader.Close();
      }
    }

    public static void PopulateOwnerCombo( ComboBox cmb, SqlConnection conn, string database )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      DataTable tbl = DbCmd.GetOwners(conn, database);

      foreach (DataRow row in tbl.Rows)
      {
        cmb.Items.Add(row["name"] != null && row["name"].GetType() != typeof(DBNull) ? (string)row["name"] : String.Empty);
      }
    }

    public static void PopulateOwnerCombo( ComboBox cmb, ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        PopulateOwnerCombo(cmb, conn, database);
      }
    }


    public static void PopulateTablesComboForCheckConstraint( ComboBox cmb, SqlConnection conn )
    {

      if (cmb == null)
        return;

      cmb.Items.Clear();

      string cmdText = String.Format(Properties.Resources.Script_GetTablesForCheckConstraint, Utils.ReplaceQuatations(conn.Database));

      DataTable tbl = ExecuteDataTable(cmdText, conn);

      foreach (DataRow row in tbl.Rows)
      {
        cmb.Items.Add(new CheckTableSpec((string)row["owner"], (string)row["qualifiedname"]));
      }
    }

    public static void PopulateTablesComboForCheckConstraint( ComboBox cmb, ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        PopulateTablesComboForCheckConstraint(cmb, conn);
      }
    }

    public static void PopulateFileGroupsCombo( ComboBox cmb, SqlConnection conn, bool wantDefault )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();

      if (wantDefault)
        cmb.Items.Add("Default");

      string cmdText = "SELECT groupname FROM sysfilegroups ORDER BY groupname";
      DataTable tbl = ExecuteDataTable(cmdText, conn);

      foreach (DataRow row in tbl.Rows)
      {
        if (!Utils.IsRowItemValid(row, 0))
          continue;
        cmb.Items.Add(row["groupname"].ToString());
      }
    }

    public static void PopulateFileGroupsCombo( ComboBox cmb, ConnectionParams cp, bool wantDefault )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, wantDefault))
      {
        PopulateFileGroupsCombo(cmb, conn, wantDefault);
      }
    }

    public static void PopulateRulesCombo( ComboBox cmb, SqlConnection conn, bool fullName )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      cmb.Items.Add("");

      DataTable tbl = GetRules(conn, conn.Database);

      foreach (DataRow row in tbl.Rows)
      {
        if (!Utils.IsRowItemValid(row, 0))
          continue;
        if(fullName)
          cmb.Items.Add(row["name"]);
        else
          cmb.Items.Add(row["ruleName"]);
      }
    }

    public static void PopulateRulesCombo( ComboBox cmb, ConnectionParams cp, bool fullName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        PopulateRulesCombo(cmb, conn, fullName);
      }
    }

    public static void PopulateDefaultsCombo( ComboBox cmb, SqlConnection conn, bool fullName, bool wantEmptyItem)
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      if(wantEmptyItem)
        cmb.Items.Add("");

      DataTable tbl = GetDefaults(conn);
      foreach (DataRow row in tbl.Rows)
      {
        if (!Utils.IsRowItemValid(row, 0))
          continue;
        if (fullName)
        {
          cmb.Items.Add(row["name"]);
        }
        else
        {
          cmb.Items.Add(row["defaultName"]);
        }
      }
    }


    public static void PopulateDefaultsCombo( ComboBox cmb, ConnectionParams cp, bool fullName, bool wantEmptyItem )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        PopulateDefaultsCombo(cmb, conn, fullName, wantEmptyItem);
      }
    }

    public static void PopulateCollationsCombo( ComboBox cmb, SqlConnection conn )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      cmb.Items.Add("");

      string cmdText = "select name from ::fn_helpcollations()";
      SqlDataReader reader = ExecuteReader(cmdText, conn);
      try
      {
        while (reader.Read())
        {
          cmb.Items.Add(reader.GetString(0));
        }
      }
      finally
      {
        reader.Close();
      }
    }

    public static void PopulateCollationsCombo( ComboBox cmb, ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        PopulateCollationsCombo(cmb, conn);
      }
    }


    public static void PopulatePrimaryKeysCombo( ComboBox cmb, ConnectionParams cp, bool loadProps, long tableId )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();

      PrimaryKeyWrapper key = null;
      DataTable tbl = GetPrimaryKeys(cp, tableId);
      foreach (DataRow row in tbl.Rows)
      {
        if (!Utils.IsDbValueValid(row["id"]))
          continue;
        key = new PrimaryKeyWrapper(cp);
        key.ID = (int)row["id"];
        if (loadProps)
        {
          key.LoadProperties();
          key.GetOtherInformation();
        }
        cmb.Items.Add(key);
      }
    }

    public static void PopulateForeignKeysCombo( ComboBox cmb, ConnectionParams cp, bool loadProps, long tableId )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();

      ForeignKeyWrapper key = null;
      DataTable tbl = GetForeignKeys(cp, tableId);
      foreach (DataRow row in tbl.Rows)
      {
        if (!Utils.IsDbValueValid(row["id"]))
          continue;
        key = new ForeignKeyWrapper(cp);
        key.ID = (int)row["id"];
        if (loadProps)
        {
          key.LoadAllProperties();
        }
        cmb.Items.Add(key);
      }
    }

    public static void PopulateForeignKeysComboSimple(ComboBox cmb, ConnectionParams cp, long tableId)
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();

      ForeignKeyWrapper key = null;
      DataTable tbl = GetForeignKeys(cp, tableId);
      foreach (DataRow row in tbl.Rows)
      {
        if (!Utils.IsDbValueValid(row["id"]))
          continue;
        key = new ForeignKeyWrapper(cp);
        key.ID = (int)row["id"];
        key.LoadBasicProperties();
        key.AllPropsLoaded = false;
        cmb.Items.Add(key);
      }
    }



    public static void PopulateReferenceTablePKCombo( ComboBox cmb, ConnectionParams cp, long tableId )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      DataTable tbl = GetConstraintForForeignKey(cp, tableId);
      foreach (DataRow row in tbl.Rows)
      {
        if (!Utils.IsDbValueValid(row["qualifiedname"]))
          continue;

        cmb.Items.Add(new NameIdPair((short)row["indid"], (string)row["qualifiedname"]));
      }
    }


    public static void PopulateUserDefinedTablesCombo( ComboBox cmb, ConnectionParams cp, long tableId )
    {
      if (cmb == null)
        return;

      cmb.Items.Clear();
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        TableWrapper table = null;
        SqlDataReader reader = GetUserDefinedTablesReader(conn, tableId);
        if (reader.HasRows)
        {
          try
          {
            while (reader.Read())
            {
              if (!Utils.IsDbValueValid(reader["id"]))
                continue;
              table = new TableWrapper(cp);
              table.ID = (int)reader["id"];
              table.Name = (string)reader["name"];
              table.Owner = (string)reader["owner"];
              cmb.Items.Add(table);
            }
          }
          finally
          {
            reader.Close();
          }
        }
      }
    }

    #endregion //Combo population

    #region Execution helper functions
    public static DataTable ExecuteDataTable( string cmdText, ConnectionParams cp )
    {
      if (cp == null)
      {
        throw new Exception("Connection parameters not specified!");
      }

      return ExecuteDataTable(cmdText, cp.CreateSqlConnection(true, false));
    }

    public static DataTable ExecuteDataTable( string cmdText, SqlConnection conn )
    {
      if (conn == null)
      {
        throw new Exception("Connection not specified!");
      }

      DataTable result = new DataTable();
      SqlDataAdapter adapter = new SqlDataAdapter(cmdText, conn);
      adapter.Fill(result);
      return result;
    }

    public static SqlDataReader ExecuteReader( string cmdText, SqlConnection conn )
    {
      if (conn == null)
      {
        throw new Exception("Connection not specified!");
      }

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      return cmd.ExecuteReader();
    }

    public static int ExecuteCommand( string cmdText, ConnectionParams cp )
    {
      if (cp == null)
      {
        throw new Exception("Connection parameters not specified!");
      }

      return ExecuteCommand(cmdText, cp.CreateSqlConnection(true, false));
    }

    public static int ExecuteCommand( string cmdText, SqlConnection conn )
    {
      return ExecuteCommand(cmdText, conn, null);
    }

    public static int ExecuteCommand( string cmdText, SqlConnection conn, SqlTransaction tran )
    {
      if (conn == null)
      {
        throw new Exception("Connection parameters not specified!");
      }

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      if (tran != null)
        cmd.Transaction = tran;

      return cmd.ExecuteNonQuery();
    }

    #endregion //Execution helper functions


    public static DataTable GetDatabasesAsDataTable( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetDatabasesAsDataTable(conn);
      }
    }

    public static DataTable GetDatabasesAsDataTable( SqlConnection conn )
    {
      string cmdText = "SELECT dbid, name as description, name, status & 512 as online, crdate, has_dbaccess(name) as access "
        + " FROM master.dbo.sysdatabases WHERE has_dbaccess(name) = 1 ORDER BY description";
      return ExecuteDataTable(cmdText, conn);
    }


    public static SqlDataReader GetDatabasesAsDataReader( SqlConnection conn )
    {
      string cmdText = "SELECT dbid, name as description, name, status & 512 as online, crdate, has_dbaccess(name) as access "
        + " FROM master.dbo.sysdatabases WHERE has_dbaccess(name) = 1 ORDER BY description";
      return ExecuteReader(cmdText, conn);
    }

    public static DataTable GetUserDefinedTables( SqlConnection conn, long tableId )
    {
      string cmdText = tableId > 0 ? String.Format(Properties.Resources.Script_GetUserTableById, tableId) : Properties.Resources.Script_GetAllUserDefinedTables;
      return ExecuteDataTable(cmdText, conn);
    }

    public static SqlDataReader GetUserDefinedTablesReader( SqlConnection conn, long tableId )
    {
      string cmdText = tableId > 0 ? String.Format(Properties.Resources.Script_GetUserTableById, tableId) : Properties.Resources.Script_GetAllUserDefinedTables;
      return ExecuteReader(cmdText, conn);
    }

    public static DataTable GetUserDefinedTables( ConnectionParams cp, long tableId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetUserDefinedTables(conn, tableId);
      }
    }

    public static DataTable GetColumnsSimple(SqlConnection conn, string tblName )
    {
      string cmdText = "SELECT dbo.sysobjects.name as tableName, dbo.syscolumns.name as colName ";
      cmdText += " FROM        dbo.syscolumns";
      cmdText += " JOIN dbo.sysobjects on  dbo.syscolumns.id = dbo.sysobjects.id";
      
      cmdText += !String.IsNullOrEmpty(tblName) ? " WHERE       dbo.syscolumns.id = OBJECT_ID('" + tblName + "')" : String.Empty ;
      cmdText += " ORDER BY    dbo.sysobjects.name, dbo.syscolumns.name";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetColumnsSimple( ConnectionParams cp, string tblName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetColumnsSimple(conn, tblName);
      }
    }


    public static DataTable GetLanguages( SqlConnection conn )
    {
      string cmdText = "SELECT alias as description, alias FROM master.dbo.syslanguages ORDER BY description";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetLanguages( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetLanguages(conn);
      }
    }

    public static DataTable GetOwners( SqlConnection conn, string database )
    {
      string cmdText = "USE " + Utils.Qualify(String.IsNullOrEmpty(database) ? conn.Database : database) + ";";
      cmdText += @" declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() 
                    IF @cmplevel < 90 
	                    SELECT name FROM sysusers WHERE uid!=0 AND isaliased=0 ORDER BY uid
                    ELSE
	                    SELECT name from sys.schemas";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetOwners( ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetOwners(conn, database);
      }
    }

    public static DataTable GetDataTypes( SqlConnection conn )
    {
      string cmdText = Properties.Resources.Script_GetDataTypes;

      DataTable tbl = ExecuteDataTable(cmdText, conn);
      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = tbl.Columns["name"];
      tbl.PrimaryKey = PrimaryKeyColumns;
      return tbl;
    }

    public static DataTable GetDataTypes( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetDataTypes(conn);
      }
    }

    public static SqlDataReader GetRolesAsDataReader(string dbname, SqlConnection conn)
    {
      string cmdText = !String.IsNullOrEmpty(dbname) ? "USE [" + dbname + "];" : "USE [master]";
      cmdText += "SELECT name FROM sysusers WHERE issqlrole=1";
      return ExecuteReader(cmdText, conn);
    }

    public static DataTable GetRolesAsDataTable( string dbname, SqlConnection conn )
    {
      string cmdText = !String.IsNullOrEmpty(dbname) ? "USE [" + dbname + "];" : "USE [master]";
      cmdText += "SELECT CAST(0 as bit) as isin, name FROM sysusers WHERE issqlrole=1";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetRolesAsDataTable( string dbname, ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetRolesAsDataTable(dbname, conn);
      }
    }


    public static string PrepareCreateLoginStatments( string serverVersion, bool isWindowsLogin, string loginName, string pwd, string defdb, string deflang )
    {
      if (String.IsNullOrEmpty(serverVersion))
      {
        throw new Exception("Server version can not be determined!\nLogin will not be created.");
      }

      int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      string cmdText = "USE [master];";
      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8)
      {
        if (isWindowsLogin)
        {
          cmdText += "EXEC sp_grantlogin '" + Utils.ReplaceQuatations(loginName) + "';";
          cmdText += !String.IsNullOrEmpty(defdb)
            ? "EXEC sp_defaultdb '" + Utils.ReplaceQuatations(loginName) + "', '" + Utils.ReplaceQuatations(defdb) + "';"
            : String.Empty;

          cmdText += !String.IsNullOrEmpty(deflang)
            ? "EXEC sp_defaultlanguage '" + Utils.ReplaceQuatations(loginName) + "', '" + Utils.ReplaceQuatations(deflang) + "';"
            : String.Empty;
        }
        else
        {
          cmdText += "EXEC sp_addlogin '" + Utils.ReplaceQuatations(loginName) + "', '" + Utils.ReplaceQuatations(pwd) + "'";
          cmdText += String.IsNullOrEmpty(defdb) ? ", 'master'" : ", @defdb ='" + Utils.ReplaceQuatations(defdb) + "'";
          cmdText += String.IsNullOrEmpty(deflang) ? ", null" : ", @deflanguage = '" + Utils.ReplaceQuatations(deflang) + "';";
        }

      }
      // 9.xxx or up
      else if (versionNo >= 9)
      {
        if (!isWindowsLogin)
        {
          cmdText += "CREATE LOGIN " + Utils.Qualify(loginName) + " WITH PASSWORD = '" + Utils.ReplaceQuatations(pwd) + "'";
          cmdText += !String.IsNullOrEmpty(defdb) ? ", DEFAULT_DATABASE = " + Utils.ReplaceQuatations(defdb) : String.Empty;
          cmdText += !String.IsNullOrEmpty(deflang) ? ", DEFAULT_LANGUAGE = " + Utils.ReplaceQuatations(deflang) : String.Empty;
        }
        else
        {
          cmdText += "CREATE LOGIN " + Utils.Qualify(loginName);
          cmdText += " FROM WINDOWS ";
          cmdText += !String.IsNullOrEmpty(defdb) || !String.IsNullOrEmpty(deflang) ? " WITH " : String.Empty;
          cmdText += !String.IsNullOrEmpty(defdb)
              ? " DEFAULT_DATABASE = " + Utils.ReplaceQuatations(defdb) + (!String.IsNullOrEmpty(deflang) ? "," : String.Empty)
              : String.Empty;
          cmdText += !String.IsNullOrEmpty(deflang)
            ? " DEFAULT_LANGUAGE = " + Utils.ReplaceQuatations(deflang)
            : String.Empty;

        }
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      return cmdText;
    }


    public static string PrepareCreateUserAndAddToRoleStatments( string serverVersion, string database, string loginName, string username, string roles )
    {
      string cmdText = "";
      cmdText += "USE [" + database + "];";

      if (String.IsNullOrEmpty(serverVersion))
      {
        throw new Exception("Server version can not be determined!\nLogin will not be added to role(s).");
      }

      int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8) 
      {
        cmdText += "EXEC sp_grantdbaccess '" + Utils.ReplaceQuatations(loginName) + "', '" + Utils.ReplaceQuatations(username) + "';";
      }
      // 9.xxx or up
      else if (versionNo >= 9 )
      {
        cmdText += "CREATE USER " + Utils.Qualify(Utils.ReplaceQuatations(username)) + " FOR LOGIN " + Utils.Qualify(Utils.ReplaceQuatations(loginName)) + ";";
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }


      string[] _strArrayRoles = roles.Split(new char[] { ';' });
      foreach (string _role in _strArrayRoles)
      {
        if (_role != "")
          cmdText += "EXEC sp_addrolemember '" + _role + "', '" + Utils.ReplaceQuatations(username) + "';";
      }
      return cmdText;
    }


    public static void DropUser( ConnectionParams cp, string user, string database )
    {
      string cmdText = "USE " + Utils.Qualify(database) + ";";
      string serverVersion = QueryServerVersion(cp);
      if (String.IsNullOrEmpty(serverVersion))
      {
        MessageService.ShowError("SQL Server version can not be retreived.\n Login will not be dropped.");
        return;
      }

        int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8) 
      {
        cmdText += "IF USER_ID('" + Utils.ReplaceQuatations(user) + "') IS NOT NULL";
        cmdText += "	BEGIN";
        cmdText += "		EXEC sp_revokedbaccess '" + Utils.ReplaceQuatations(user) + "'";
        cmdText += "	END";
      }
      else if (versionNo >= 9 )
      {
        cmdText += "DROP USER " + Utils.Qualify(Utils.ReplaceQuatations(user));
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }

      ExecuteCommand(cmdText, cp);
    }

    public static void DeleteLogin( ConnectionParams cp, bool isWindowsLogin, string login )
    {
      string cmdText = "USE [master]; ";
      string serverVersion = QueryServerVersion(cp);
      if (String.IsNullOrEmpty(serverVersion))
      {
        MessageService.ShowError("SQL Server version can not be retreived.\n Login will not be dropped.");
        return;
      }

      int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8) 
      {
        if (!isWindowsLogin)
        {
          cmdText += "IF EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname='" + Utils.ReplaceQuatations(login) + "')";
          cmdText += "	BEGIN";
          cmdText += "		EXEC sp_droplogin '" + Utils.ReplaceQuatations(login) + "'";
          cmdText += "	END";
        }
        else
        {
          cmdText += "EXEC sp_revokelogin @loginame = N'" + Utils.ReplaceQuatations(login) + "'";

        }
      }
      else if (versionNo >= 9)
      {
        cmdText += "DROP LOGIN" + Utils.Qualify(Utils.ReplaceQuatations(login));
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }

      ExecuteCommand(cmdText, cp);
    }

    public static void ChangeDefaultDB( SqlConnection conn, string serverVersion, string login, string database )
    {
      string cmdText = "";
      int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8) 
      {
        cmdText += "USE [master];";
        cmdText += "EXEC sp_defaultdb '" + Utils.ReplaceQuatations(login) + "', '" + Utils.ReplaceQuatations(database) + "';";
      }
      else if (versionNo >= 9)
      {
        cmdText += "ALTER LOGIN " + Utils.Qualify(Utils.ReplaceQuatations(login)) + " WITh DEFAULT_DATABASE = " + Utils.Qualify(Utils.ReplaceQuatations(database));
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }

      ExecuteCommand(cmdText, conn);
    }



    public static void ChangeDefaultLanguage( SqlConnection conn, string serverVersion, string login, string language )
    {
      string cmdText = "";
      int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8) 
      {
        cmdText += "USE [master];";
        cmdText += "EXEC sp_defaultlanguage '" + Utils.ReplaceQuatations(login) + "', '" + Utils.ReplaceQuatations(language) + "';";
      }
      else if (versionNo >= 9)
      {
        cmdText += "ALTER LOGIN " + Utils.Qualify(Utils.ReplaceQuatations(login)) + " WITH DEFAULT_LANGUAGE = " + Utils.Qualify(Utils.ReplaceQuatations(language));
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }

      ExecuteCommand(cmdText, conn);

    }

    public static void AddToServerRole( SqlConnection conn, string login, string role )
    {
      string cmdText = "";
      cmdText += "USE [master];";
      cmdText += "EXEC sp_addsrvrolemember '" + Utils.ReplaceQuatations(login) + "', '" + Utils.ReplaceQuatations(role) + "';";
      ExecuteCommand(cmdText, conn);
    }

    public static void DropServerRole( SqlConnection conn, string login, string role )
    {
      string cmdText = "";
      cmdText += "USE [master];";
      cmdText += "EXEC sp_dropsrvrolemember '" + Utils.ReplaceQuatations(login) + "', '" + Utils.ReplaceQuatations(role) + "';";
      ExecuteCommand(cmdText, conn);
    }

    public static bool IsInServerRole( SqlConnection conn, string login, string role )
    {
      bool result = false;
      DataTable isInTbl = null;
      string isInRoleCmd = "SELECT IS_SRVROLEMEMBER('{0}','{1}') as isin";
      isInTbl = DbCmd.ExecuteDataTable(String.Format(isInRoleCmd, role, Utils.ReplaceQuatations(login)), conn);
      if (isInTbl != null && isInTbl.Rows.Count != 0)
      {
        if (isInTbl.Rows[0].ItemArray[0] != null && isInTbl.Rows[0].ItemArray[0].GetType() != typeof(DBNull))
        {
          switch (isInTbl.Rows[0].ItemArray[0].ToString())
          {
            case "1":
              result = true;
              break;
            default:
              break;
          }
        }
      }
      return result;
    }

    public static DataTable GetUsers( string database, ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetUsers(database, conn);
      }
    }

    public static DataTable GetUsers( string database, SqlConnection conn )
    {
      string cmdText = DatabaseUsersScript(database);

      return ExecuteDataTable(cmdText, conn);
    }

    private static string DatabaseUsersScript( string database )
    {
      string cmdText = "USE [" + Utils.ReplaceQuatations(database) + "];";
      cmdText += "SELECT uid, name, SUSER_SNAME(sid) as loginName, createdate, updatedate, '" + Utils.ReplaceQuatations(database) + "' as databasename FROM sysusers WHERE (issqluser=1 OR isntname=1) AND sid IS NOT NULL  ORDER BY name";
      return cmdText;
    }

    public static DataTable GetUserRolesAsDataTable( SqlConnection conn, string user, string database )
    {
      string cmdText = "USE " + Utils.Qualify(database) + " ;";
      cmdText += " SELECT U.name FROM dbo.sysmembers R,dbo.sysusers U ";
      cmdText += " WHERE R.memberuid=USER_ID('" + Utils.ReplaceQuatations(user) + "') ";
      cmdText += " AND (U.issqlrole=1 OR U.isapprole=1) AND U.uid!=0 AND U.uid=R.groupuid ORDER BY 1";

      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetUserRolesAsDataTable( ConnectionParams cp, string user, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetUserRolesAsDataTable(conn, user, database);
      }
    }

    public static SqlDataReader GetUserRolesAsDataReader( SqlConnection conn, string user, string database )
    {
      string cmdText = "USE " + Utils.Qualify(database) + " ;";
      cmdText += " SELECT U.name FROM dbo.sysmembers R,dbo.sysusers U ";
      cmdText += " WHERE R.memberuid=USER_ID('" + Utils.ReplaceQuatations(user) + "') ";
      cmdText += " AND (U.issqlrole=1 OR U.isapprole=1) AND U.uid!=0 AND U.uid=R.groupuid ORDER BY 1";

      return ExecuteReader(cmdText, conn);
    }


    public static DataTable GetUserRoleMap( SqlConnection conn, string username )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += " SELECT CAST ( ( CASE WHEN UR.name is null THEN 0 ELSE 1 END ) as bit ) as isin";
      cmdText += " , SU.name ";
      cmdText += " FROM sysusers SU LEFT OUTER JOIN";
      cmdText += " ( SELECT U.name FROM dbo.sysmembers R,dbo.sysusers U";
      cmdText += " WHERE R.memberuid=USER_ID('" + Utils.ReplaceQuatations(username) + "') ";
      cmdText += " AND (U.issqlrole=1 OR U.isapprole=1) AND U.uid!=0 AND U.uid=R.groupuid )";
      cmdText += " UR on SU.name = UR.name";
      cmdText += " WHERE SU.issqlrole=1";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetUserRoleMap( ConnectionParams cp, string username )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetUserRoleMap(conn, username);
      }
    }

    public static bool IsUserInRole( SqlConnection conn, string username, string role )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += " SELECT count(U.name) as isin";
      cmdText += " FROM dbo.sysmembers R,dbo.sysusers U WHERE ";
      cmdText += " R.memberuid=USER_ID('" + Utils.ReplaceQuatations(username) + "')";
      cmdText += " AND U.name = '" + Utils.ReplaceQuatations(role) + "'";
      cmdText += " AND (U.issqlrole=1 OR U.isapprole=1) AND U.uid!=0 AND U.uid=R.groupuid";

      DataTable tbl = ExecuteDataTable(cmdText, conn);
      if (tbl == null || tbl.Rows.Count == 0)
        return false;

      if (tbl.Rows[0].ItemArray[0] == null || tbl.Rows[0].ItemArray[0].GetType() == typeof(DBNull))
        return false;

      int cnt = (int)tbl.Rows[0].ItemArray[0];
      return cnt > 0;
    }

    public static void DropUserFromRole( SqlConnection conn, string username, string role )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += " EXEC sp_droprolemember '" + Utils.ReplaceQuatations(role) + "', '" + Utils.ReplaceQuatations(username) + "'";
      ExecuteCommand(cmdText, conn);
    }

    public static void DropUserFromRole( ConnectionParams cp, string username, string role )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        DropUserFromRole(conn, username, role);
      }
    }

    public static void AddUserToRole( SqlConnection conn, string username, string role )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += " EXEC sp_addrolemember '" + Utils.ReplaceQuatations(role) + "', '" + Utils.ReplaceQuatations(username) + "'";
      ExecuteCommand(cmdText, conn);
    }

    public static void AddUserToRole( ConnectionParams cp, string username, string role )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        AddUserToRole(conn, username, role);
      }
    }

    public static SqlDataReader GetSystemPrivilegesAsDataReader( SqlConnection conn, string username )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += " EXEC sp_helprotect NULL, '" + Utils.ReplaceQuatations(username) + "', NULL, 's'";
      return ExecuteReader(cmdText, conn);
    }


    public static DataTable GetSystemPrivilegesAsDataTable( SqlConnection conn, string username )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += " EXEC sp_helprotect NULL, '" + Utils.ReplaceQuatations(username) + "', NULL, 's'";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetSystemPrivilegesAsDataTable( ConnectionParams cp, string username )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetSystemPrivilegesAsDataTable(conn, username);
      }
    }

    public static void GrantSystemPrivilege( SqlConnection conn, string principal, string privilege )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += "GRANT " + privilege + " TO " + Utils.Qualify(Utils.ReplaceQuatations(principal));
      ExecuteCommand(cmdText, conn);
    }

    public static void GrantSystemPrivilege( ConnectionParams cp, string userName, string privilege )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        GrantSystemPrivilege(conn, userName, privilege);
      }
    }

    public static void RevokeSystemPrivilege( SqlConnection conn, string principal, string privilege )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += "REVOKE " + privilege + " TO " + Utils.Qualify(Utils.ReplaceQuatations(principal));
      ExecuteCommand(cmdText, conn);
    }

    public static void RevokeSystemPrivilege( ConnectionParams cp, string userName, string privilege )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        RevokeSystemPrivilege(conn, userName, privilege);
      }
    }

    private static string UserObjectPrivilegesScript( string userName, string database )
    {
      string cmdText = "USE " + Utils.Qualify(database);
      cmdText += "EXEC sp_helprotect NULL,'" + Utils.ReplaceQuatations(userName) + "',NULL,'o'";
      return cmdText;
    }

    public static DataTable GetObjectPrivilegesAsDataTable( SqlConnection conn, string userName )
    {
      return ExecuteDataTable(UserObjectPrivilegesScript(userName, conn.Database), conn);
    }

    public static DataTable GetObjectPrivilegesAsDataTable( ConnectionParams cp, string userName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetObjectPrivilegesAsDataTable(conn, userName);
      }
    }

    public static SqlDataReader GetObjectPrivilegesAsDataReader( SqlConnection conn, string userName )
    {
      return ExecuteReader(UserObjectPrivilegesScript(userName, conn.Database), conn);
    }

    private static string GetDatabaseObjectScript( string database )
    {
      string cmdText = "USE " + Utils.Qualify(database);
      cmdText += " SELECT sysobjects.name , sysobjects.type as type ";
      cmdText += " FROM sysobjects ";
      cmdText += " WHERE sysobjects.type IN ('U','S','V','X','TF','P','IF','FN') ";
      cmdText += " ORDER BY sysobjects.name ";

      return cmdText;
    }

    public static SqlDataReader GetDatabaseObjectsAsDataReader( SqlConnection conn )
    {
      return ExecuteReader(GetDatabaseObjectScript(conn.Database), conn);
    }


    public static DataTable GetBaseDatabaseObjectsForPrivileges( SqlConnection conn )
    {
      string cmdText = String.Format("USE {0}\r\n" ,Utils.Qualify(conn.Database)) ;
      cmdText += Properties.Resources.Script_ObjectsForPrivileges;
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetBaseDatabaseObjectsForPrivileges( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetBaseDatabaseObjectsForPrivileges(conn);
      }
    }

    public static void GrantObjectPrivilege( SqlConnection conn, string principal, string owner, string privilege, string objectName )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += "GRANT " + privilege + " ON " + Utils.Qualify(owner) + "." + Utils.Qualify(objectName) + " TO " + Utils.Qualify(principal);
      ExecuteCommand(cmdText, conn);
    }

    public static void RevokeObjectPrivilege( SqlConnection conn, string principal, string owner, string privilege, string objectName )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database);
      cmdText += "REVOKE " + privilege + " ON " + Utils.Qualify(owner) + "." + Utils.Qualify(objectName) + " TO " + Utils.Qualify(principal);
      ExecuteCommand(cmdText, conn);
    }

    public static void ChangeLoginPwd(SqlConnection conn, string login, string pwd)
    {
      if (String.IsNullOrEmpty(login))
        throw new Exception("Login not specified!");

      string serverVersion = QueryServerVersion(conn);
      string cmdText = String.Empty;
      int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8) 
      {
        cmdText = "sp_password null,'" + Utils.ReplaceQuatations(pwd) + "','" + Utils.ReplaceQuatations(login) + "'";
      }
      else if (versionNo >= 9)
      {
        cmdText = "ALTER LOGIN " + Utils.Qualify(login)
          + " WITH PASSWORD = '" + Utils.ReplaceQuatations(pwd) + "'";
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }

      ExecuteCommand(cmdText, conn);
    }

    public static void ChangeLoginPwd(ConnectionParams cp, string login, string pwd)
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        ChangeLoginPwd(conn, login, pwd);
      }
    }

    #region Commands for getting and modifying database properties
    public static DataTable GetDataFiles( SqlConnection conn )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "SELECT";
      cmdText += " dbo.sysfiles.fileid,";
      cmdText += " (dbo.sysfiles.size*8/1024.0) AS size,";
      cmdText += " dbo.sysfiles.maxsize,";
      cmdText += " dbo.sysfiles.growth,";
      cmdText += " dbo.sysfiles.status,";
      cmdText += " (dbo.sysfiles.status&0x100000) as growthPerc,";
      cmdText += " dbo.sysfiles.name,";
      cmdText += " dbo.sysfiles.filename,";
      cmdText += " dbo.sysfilegroups.groupname";
      cmdText += " FROM dbo.sysfiles inner join dbo.sysfilegroups on dbo.sysfiles.groupid = dbo.sysfilegroups.groupid";
      cmdText += " ORDER BY dbo.sysfilegroups.groupid";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetDataFiles( ConnectionParams cp )
    {

      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetDataFiles(conn);
      }
    }

    public static DataTable GetLogFiles( SqlConnection conn )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "SELECT fileid,(size*8/1024.0) AS size,maxsize,growth,status,(status&0x100000) as growthPerc,name,filename";
      cmdText += " FROM sysfiles WHERE (status&0x40)=0x40 ORDER BY fileid";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetLogFiles( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetLogFiles(conn);
      }
    }

    public static SqlDataReader GetSpaceUsage( SqlConnection conn )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "EXEC sp_spaceused";
      return ExecuteReader(cmdText, conn);
    }

    public static void DropDb( SqlConnection conn, string database, long dbid )
    {
      string cmdText = "IF DB_NAME(" + dbid.ToString() + ") IS NOT NULL";
      cmdText += " BEGIN";
      cmdText += " DROP DATABASE " + Utils.Qualify(database) + ";";
      cmdText += " END";
      ExecuteCommand(cmdText, conn);
    }

    public static void DropDb( ConnectionParams cp, string database, long dbid )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        DropDb(conn, database, dbid);
      }
    }


    public static void ShrinkDB( SqlConnection conn, string database, string targetPercent, string truncOption )
    {
      string cmdText = "";
      if (truncOption == "TRUNCATEONLY")
      {
        cmdText = "DBCC SHRINKDATABASE (" + Utils.Qualify(database) + ", " + targetPercent + "," + truncOption + ")";
      }
      else
      {
        cmdText = "DBCC SHRINKDATABASE (" + Utils.Qualify(database) + ", " + targetPercent + ")";
      }
      ExecuteCommand(cmdText, conn);
    }

    public static void ShrinkDB( ConnectionParams cp, string database, string targetPercent, string truncOption )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        ShrinkDB(conn, database, targetPercent, truncOption);
      }
    }

    public static void TruncLog( SqlConnection conn, string database )
    {
      string cmdText = "dump tran " + Utils.Qualify(database) + " with no_log";
      ExecuteCommand(cmdText, conn);
    }

    public static void TruncLog( ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        TruncLog(conn, database);
      }
    }

    public static void DetachDb( SqlConnection conn, string database )
    {
      string cmdText = "EXEC sp_detach_db  '" + Utils.ReplaceQuatations(database) + "', 'false'";
      ExecuteCommand(cmdText, conn);
    }

    public static void DetachDb( ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        DetachDb(conn, database);
      }
    }


    public static void Attach( SqlConnection conn, string name, string dataPath, string logPath )
    {
      string cmdText = "EXEC sp_attach_db '" + Utils.ReplaceQuatations(name) + "',";
      cmdText += "'" + dataPath + "',";
      cmdText += "'" + logPath + "'";
      ExecuteCommand(cmdText, conn);
    }

    public static void Attach( ConnectionParams cp, string name, string dataPath, string logPath )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        Attach(conn, name, dataPath, logPath);
      }
    }

    public static void AttachSingle( SqlConnection conn, string name, string dataPath )
    {
      string cmdText = "EXEC sp_attach_single_file_db '" + Utils.ReplaceQuatations(name) + "',";
      cmdText += "'" + dataPath + "'";
      ExecuteCommand(cmdText, conn);
    }

    public static void AttachSingle( ConnectionParams cp, string name, string dataPath )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        AttachSingle(conn, name, dataPath);
      }
    }

    private static string DatabaseOptionScript( string database )
    {
      string cmdText = "EXEC master.dbo.sp_dboption";
      cmdText += !String.IsNullOrEmpty(database) ? " '" + Utils.ReplaceQuatations(database) + "' " : String.Empty;
      return cmdText;
    }

    /// <summary>
    /// Returns database options.
    /// </summary>
    /// <param name="conn">Database Connection</param>
    /// <param name="database">Id this is empty returns supported set of options, else returns set options for the specified database</param>
    /// <returns></returns>
    public static DataTable QueryDatabaseOptions( SqlConnection conn, string database )
    {
      return ExecuteDataTable(DatabaseOptionScript(database), conn);
    }

    /// <summary>
    /// Returns database options.
    /// </summary>
    /// <param name="cp">Connection parameters</param>
    /// <param name="database">Id this is empty returns supported set of options, else returns set options for the specified database</param>
    /// <returns></returns>
    public static DataTable QueryDatabaseOptions( ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return QueryDatabaseOptions(conn, database);
      }
    }

    public static DataTable PrepareDatabaseOptionsTable( SqlConnection conn )
    {
      SqlDataReader readerDef = null;

      #region Data Table Definition
      DataTable tbl = new DataTable();
      DataColumn column;
      DataRow row = null;
      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "name";
      tbl.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "isset";
      tbl.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "oldisset";
      tbl.Columns.Add(column);

      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = tbl.Columns["name"];
      tbl.PrimaryKey = PrimaryKeyColumns;
      #endregion //Data Table Definition

      try
      {
        readerDef = ExecuteReader(DatabaseOptionScript(String.Empty), conn);
        while (readerDef.Read())
        {
          row = tbl.NewRow();
          row[0] = (string)readerDef[0];
          row[1] = false;
          row[2] = false;
          tbl.Rows.Add(row);
        }

      }
      finally
      {
        if (readerDef != null && !readerDef.IsClosed)
          readerDef.Close();
      }

      return tbl;
    }

    public static DataTable PrepareDatabaseOptionsTable( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return PrepareDatabaseOptionsTable(conn);
      }
    }


    public static DataTable GetDatabaseOptions( SqlConnection conn, string database )
    {
      SqlDataReader readerDef = null;
      SqlDataReader readerSet = null;

      #region Data Table Definition
      DataTable tbl = new DataTable();
      DataColumn column;
      DataRow row = null;
      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "name";
      tbl.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "isset";
      tbl.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "oldisset";
      tbl.Columns.Add(column);

      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = tbl.Columns["name"];
      tbl.PrimaryKey = PrimaryKeyColumns;
      #endregion //Data Table Definition

      try
      {
        readerDef = ExecuteReader(DatabaseOptionScript(String.Empty), conn);
        while (readerDef.Read())
        {
          row = tbl.NewRow();
          row[0] = (string)readerDef[0];
          row[1] = false;
          row[2] = false;
          tbl.Rows.Add(row);
        }

        readerDef.Close();
        readerSet = ExecuteReader(DatabaseOptionScript(database), conn);
        while (readerSet.Read())
        {

          row = tbl.Rows.Find((string)readerSet[0]);
          if (row == null)
            continue;

          row[1] = true;
          row[2] = true;
        }

        readerSet.Close();
      }
      finally
      {
        if (readerDef != null && !readerDef.IsClosed)
          readerDef.Close();
        if (readerSet != null && !readerSet.IsClosed)
          readerSet.Close();
      }

      return tbl;
    }

    public static DataTable GetDatabaseOptions( ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetDatabaseOptions(conn, database);
      }
    }

    public static void AddDatabaseOption( SqlConnection conn, string database, string option )
    {
      string cmdText = "EXEC sp_dboption '" + Utils.ReplaceQuatations(database) + "','" + option + "',true";
      ExecuteCommand(cmdText, conn);
    }

    public static void AddDatabaseOption( ConnectionParams cp, string database, string option )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        AddDatabaseOption(conn, database, option);
      }
    }

    public static void DropDatabaseOption( SqlConnection conn, string database, string option )
    {
      string cmdText = "EXEC sp_dboption '" + Utils.ReplaceQuatations(database) + "','" + option + "',false";
      ExecuteCommand(cmdText, conn);
    }

    public static void DropDatabaseOption( ConnectionParams cp, string database, string option )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        DropDatabaseOption(conn, database, option);
      }
    }


    public static void ModifyFile( SqlConnection conn, string database, string fileName, string size, string maxSize, string growth )
    {
      string cmdText = "USE [master];";
      cmdText += "ALTER DATABASE " + Utils.Qualify(database) + " MODIFY FILE ";
      cmdText += "(";
      cmdText += " NAME='" + fileName + "',";
      if (size != "")
        cmdText += " SIZE=" + size + ",";
      if (maxSize != "")
        cmdText += " MAXSIZE=" + maxSize + ",";
      if (growth != "")
        cmdText += " FILEGROWTH=" + growth;
      cmdText += ")";
      ExecuteCommand(cmdText, conn);
    }
    public static void ModifyFile( ConnectionParams cp, string database, string fileName, string size, string maxSize, string growth )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        ModifyFile(conn, database, fileName, size, maxSize, growth);
      }
    }

    public static void CreateDatabase( SqlConnection conn, string name, string dataName, string dataPath, string dataSize, string dataMaxSize, string dataGrowth, string logName, string logPath, string logSize, string logMaxSize, string logGrowth, bool forAttach )
    {
      string cmdText = "";
      cmdText += "USE [master];";
      cmdText += " CREATE DATABASE " + Utils.Qualify(name);
      cmdText += " ON PRIMARY ";
      cmdText += " (";
      cmdText += " NAME='" + dataName + "',";
      cmdText += " FILENAME='" + dataPath + "',";
      cmdText += " SIZE=" + dataSize + ",";
      cmdText += " MAXSIZE=" + dataMaxSize + ",";
      cmdText += " FILEGROWTH=" + dataGrowth;
      cmdText += ")";
      cmdText += " LOG ON (";
      cmdText += " NAME='" + logName + "',";
      cmdText += " FILENAME='" + logPath + "',";
      cmdText += " SIZE=" + logSize + ",";
      cmdText += " MAXSIZE=" + logMaxSize + ",";
      cmdText += " FILEGROWTH=" + logGrowth;
      cmdText += ")";
      if (forAttach)
      {
        cmdText += " FOR ATTACH";
      }
      cmdText += ";CHECKPOINT;";
      ExecuteCommand(cmdText, conn);
    }

    public static void CreateDatabase( ConnectionParams cp, string name, string dataName, string dataPath, string dataSize, string dataMaxSize, string dataGrowth, string logName, string logPath, string logSize, string logMaxSize, string logGrowth, bool forAttach )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        CreateDatabase(conn, name, dataName, dataPath, dataSize, dataMaxSize, dataGrowth, logName, logPath, logSize, logMaxSize, logGrowth, forAttach);
      }
    }

    #endregion //Commands for getting and modifying database properties


    #region Roles

    public static DataTable GetRoles( SqlConnection conn, string database )
    {
      string cmdText = "USE " + Utils.Qualify(database) + ";";
      cmdText += " SELECT DISTINCT A.Uid as id, Owner = CASE A.issqlrole WHEN 1 Then B.name ELSE '[NULL]' END,  A.name Role,   roletype = CASE A.issqlrole   WHEN 1 Then 'Standard' ELSE 'Application' END,";
      cmdText += " creation = A.createdate, updatedt = A.updatedate, '" + Utils.ReplaceQuatations(database) + "' as databasename";
      cmdText += " FROM  sysusers A, sysusers B WHERE B.uid = A.altuid AND (A.issqlrole=1 OR A.isapprole=1) AND  A.name <> 'public'  ORDER BY 1, 2";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetRoles( ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetRoles(conn, database);
      }
    }

    public static void DropRole( SqlConnection conn, string database, string roleName, bool isSqlRole )
    {
      string cmdText = "USE " + Utils.Qualify(String.IsNullOrEmpty(database) ? conn.Database : database) + ";";
      if (isSqlRole)
      {
        cmdText += "EXEC sp_droprole " + Utils.Qualify(roleName);
      }
      else
      {
        cmdText += "EXEC sp_dropapprole " + Utils.Qualify(roleName);
      }
      ExecuteCommand(cmdText, conn);
    }

    public static void DropRole( ConnectionParams cp, string database, string roleName, bool isSqlRole )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        DropRole(conn, database, roleName, isSqlRole);
      }
    }

    public static void RemoveUsersFromRole( SqlConnection conn, string roleName )
    {
      string cmdText = "EXEC sp_helprolemember '" + Utils.ReplaceQuatations(roleName) + "'";
      DataTable tbl = ExecuteDataTable(cmdText, conn);
      foreach (DataRow row in tbl.Rows)
      {
        DropUserFromRole(conn, (string)row[1], roleName);
      }
    }

    public static void CreateRole( string serverVersion, SqlConnection conn, string database, string roleName, string owner, bool isSqlRole, string pwd )
    {
      string cmdText = "USE " + Utils.Qualify(String.IsNullOrEmpty(database) ? conn.Database : database) + ";";


      int idx = serverVersion.IndexOf('.');
      if (idx < 0)
        throw new Exception("Server version can not be determined.");

      int versionNo = int.Parse(serverVersion.Substring(0, idx));

      if (versionNo < 7)
      {
        new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      else if (versionNo <= 8 )
      {
        if (isSqlRole)
        {
          cmdText += "EXEC sp_addrole '" + Utils.ReplaceQuatations(roleName) + "','" + Utils.ReplaceQuatations(owner) + "'";
        }
        else
        {
          cmdText += "EXEC sp_addapprole '" + Utils.ReplaceQuatations(roleName) + "','" + Utils.ReplaceQuatations(pwd) + "'";
        }
      }
      else if (versionNo >= 9 )
      {
        if (isSqlRole)
        {
          cmdText += "CREATE ROLE " + Utils.Qualify(roleName) + " AUTHORIZATION " + Utils.Qualify(owner);
        }
        else
        {
          cmdText += "CREATE APPLICATION ROLE " + Utils.Qualify(roleName) + " WITH PASSWORD = " + Utils.Qualify(pwd);
        }
      }
      else
      {
        throw new Exception(String.Format("Database version \"{0}\" not supported!", serverVersion));
      }
      ExecuteCommand(cmdText, conn);
    }

    public static void CreateRole( string serverVersion, ConnectionParams cp, string database, string roleName, string owner, bool isSqlRole, string pwd )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        CreateRole(serverVersion, conn, database, roleName, owner, isSqlRole, pwd);
      }
    }

    public static SqlDataReader PrepareUsersInRoleReader( SqlConnection conn, string database, string roleName )
    {
      string cmdText = "USE " + Utils.Qualify(String.IsNullOrEmpty(database) ? conn.Database : database) + "; ";
      cmdText += "EXEC sp_helprolemember '" + Utils.ReplaceQuatations(roleName) + "'";
      return ExecuteReader(cmdText, conn);
    }

    public static DataTable GetUsersInRole( SqlConnection conn, string database, string roleName )
    {
      SqlDataReader readerDef = null;
      SqlDataReader readerSet = null;

      #region Data Table Definition
      DataTable tbl = new DataTable();
      DataColumn column;
      DataRow row = null;
      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "name";
      tbl.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "isin";
      tbl.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "oldisin";
      tbl.Columns.Add(column);

      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = tbl.Columns["name"];
      tbl.PrimaryKey = PrimaryKeyColumns;
      #endregion //Data Table Definition

      try
      {
        readerDef = ExecuteReader(DatabaseUsersScript(String.IsNullOrEmpty(database) ? conn.Database : database), conn);
        while (readerDef.Read())
        {
          if (((string)readerDef[1]).ToLower() == "dbo")
            continue;
          row = tbl.NewRow();
          row[0] = (string)readerDef[1];
          row[1] = false;
          row[2] = false;
          tbl.Rows.Add(row);
        }

        readerDef.Close();
        readerSet = PrepareUsersInRoleReader(conn, database, roleName);
        while (readerSet.Read())
        {

          row = tbl.Rows.Find((string)readerSet[1]);
          if (row == null)
            continue;

          row[1] = true;
          row[2] = true;
        }

        readerSet.Close();
      }
      finally
      {
        if (readerDef != null && !readerDef.IsClosed)
          readerDef.Close();
        if (readerSet != null && !readerSet.IsClosed)
          readerSet.Close();
      }

      return tbl;
    }

    #endregion //Roles

    #region Rules

    public static DataTable GetRules( SqlConnection conn, string database )
    {
      string cmdText = String.Format("USE {0}\r\n",Utils.Qualify(database));
      cmdText += String.Format(Properties.Resources.Script_GetRules, Utils.ReplaceQuatations(database));
      return ExecuteDataTable(cmdText, conn);
    }

    public static int GetRuleId( SqlConnection conn, string ruleName )
    {
      string cmdText = String.Empty;
      cmdText += "SELECT id FROM sysobjects "
        + " WHERE type='R' AND id NOT IN (SELECT constid FROM sysconstraints) "
        + " AND name = '" + Utils.ReplaceQuatations(ruleName) + "'";

      DataTable tmp = ExecuteDataTable(cmdText, conn);
      if (tmp == null || tmp.Rows.Count == 0 || !Utils.IsRowItemValid(tmp.Rows[0], 0))
      {
        return -1;
      }
      else
      {
        return (int)tmp.Rows[0].ItemArray[0];
      }
    }

    public static int GetRuleId( ConnectionParams cp, string ruleName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetRuleId(conn, ruleName);
      }
    }

    public static DataTable GetRules( ConnectionParams cp, string database )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetRules(conn, database);
      }
    }

    public static void DropRule( SqlConnection conn, int ruleId, string ruleName, string owner )
    {
      //create the reader for cols dependencies
      string cmdText = "SELECT O.name + '.' + C.name as objName FROM sysobjects O,syscolumns C WHERE C.domain=" + ruleId.ToString() + " AND O.type='U' AND O.id=C.id";
      DataTable tblCols = ExecuteDataTable(cmdText, conn);

      //reader types dependencies
      cmdText = "SELECT name as objName FROM systypes WHERE domain=" + ruleId.ToString();
      DataTable tblDTypes = ExecuteDataTable(cmdText, conn);

      //Unbind rule
      foreach (DataRow colRow in tblCols.Rows)
      {
        ExecuteCommand("EXEC sp_unbindrule '" + colRow["objName"] + "'", conn);
      }

      foreach (DataRow dtypeRow in tblDTypes.Rows)
      {
        ExecuteCommand("EXEC sp_unbindrule '" + dtypeRow["objName"] + "'", conn);
      }

      cmdText = "DROP RULE " + Utils.Qualify(owner) + "." + Utils.Qualify(ruleName);
      ExecuteCommand(cmdText, conn);
    }

    public static void DropRule( ConnectionParams cp, int ruleId, string ruleName, string owner )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        DropRule(conn, ruleId, ruleName, owner);
      }
    }

    public static void CreateRule( SqlConnection conn, string owner, string ruleName, string definition )
    {
      string cmdText = "CREATE RULE " + Utils.Qualify(owner) + "." + Utils.Qualify(ruleName) + " AS " + definition;
      ExecuteCommand(cmdText, conn);
    }

    public static void CreateRule( ConnectionParams cp, string owner, string ruleName, string definition )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        CreateRule(conn, owner, ruleName, definition);
      }
    }

    public static string GetRuleDefinition( SqlConnection conn, string owner, string ruleName )
    {
      string lines = String.Empty;

      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "EXEC sp_helptext '" + Utils.ReplaceQuatations(owner) + "." + Utils.ReplaceQuatations(ruleName) + "'";

      SqlDataReader reader = ExecuteReader(cmdText, conn);
      while (reader.Read())
      {
        lines = reader.GetString(0);
      }

      return System.Text.RegularExpressions.Regex.Replace(lines, @"CREATE\sRULE\s*([^;]+?)\sAS\s", "");
    }

    public static string GetRuleDefinition( ConnectionParams cp, string owner, string ruleName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetRuleDefinition(conn, owner, ruleName);
      }
    }


    public static DataTable GetRuleDepends( SqlConnection conn, int ruleId )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "SELECT O.id, O.name+'.'+C.name as objName, 'Column' as type FROM sysobjects O,syscolumns C WHERE C.domain=" + ruleId.ToString() + " AND O.type='U' AND O.id=C.id";
      cmdText += " UNION SELECT usertype as id, name as objName, 'DataType' as type FROM systypes WHERE domain=" + ruleId.ToString();
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetRuleDepends( ConnectionParams cp, int ruleId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetRuleDepends(conn, ruleId);
      }
    }

    public static void RenameRule( SqlConnection conn, string owner, string ruleName, string newName )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "EXEC sp_rename '" + Utils.ReplaceQuatations(owner) + "." + Utils.ReplaceQuatations(ruleName) + "', '" + Utils.ReplaceQuatations(newName) + "', OBJECT";
      ExecuteCommand(cmdText, conn);
    }

    public static void RenameRule( ConnectionParams cp, string owner, string ruleName, string newName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        RenameRule(conn, owner, ruleName, newName);
      }
    }

    public static void BackupRule( SqlConnection conn, string owner, string ruleName )
    {
      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "EXEC sp_rename '" + Utils.ReplaceQuatations(owner) + "." + Utils.ReplaceQuatations(ruleName) + "', '" + Utils.ReplaceQuatations(ruleName + "_bak_" + Utils.GetFormattedNow()) + "', OBJECT";
      ExecuteCommand(cmdText, conn);
    }

    public static void AlterRuleDefinition( SqlConnection conn, int ruleId, string ruleName, string definition, string owner, bool backup )
    {
      string objName = Utils.ReplaceQuatations(owner) + "." + Utils.ReplaceQuatations(ruleName);
      string cmdText = "SELECT O.name + '.' + C.name as objName FROM sysobjects O,syscolumns C WHERE C.domain=" + ruleId.ToString() + " AND O.type='U' AND O.id=C.id";
      DataTable colDepends = ExecuteDataTable(cmdText, conn);


      cmdText = "SELECT name as objName FROM systypes WHERE domain=" + ruleId.ToString();
      DataTable dtypeDepends = ExecuteDataTable(cmdText, conn);

      if (backup)
      {
        BackupRule(conn, owner, ruleName);
      }
      else
      {
        //Unbind rule
        foreach (DataRow colRow in colDepends.Rows)
        {
          ExecuteCommand("EXEC sp_unbindrule '" + colRow["objName"] + "'", conn);
        }

        foreach (DataRow dtypeRow in dtypeDepends.Rows)
        {
          ExecuteCommand("EXEC sp_unbindrule '" + dtypeRow["objName"] + "'", conn);
        }

        //Drop rule
        DropRule(conn, ruleId, ruleName, owner);
      }

      CreateRule(conn, owner, ruleName, definition);

      //cols dependences
      foreach (DataRow colRow in colDepends.Rows)
      {
        ExecuteCommand("EXEC sp_bindrule '" + objName + "','" + colRow["objName"] + "'", conn);
      }

      //dtype dependences
      foreach (DataRow dtypeRow in dtypeDepends.Rows)
      {
        ExecuteCommand("EXEC sp_bindrule '" + objName + "','" + dtypeRow["objName"] + "'", conn);
      }
    }

    public static void AlterRuleDefinition( ConnectionParams cp, int ruleId, string ruleName, string definition, string owner, bool backup )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        AlterRuleDefinition(conn, ruleId, ruleName, definition, owner, backup);
      }
    }
    #endregion //Rules

    #region Check Constraints

    public static DataTable GetCheckConstraints( SqlConnection conn )
    {
      string cmdText = String.Format(Properties.Resources.Script_GetCheckConstraints,Utils.ReplaceQuatations(conn.Database));
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetCheckConstraints( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetCheckConstraints(conn);
      }
    }

    public static int GetCheckConstraintId( SqlConnection conn, string checkName )
    {
      string cmdText = String.Format(Properties.Resources.Script_GetConstraintIdByName, Utils.ReplaceQuatations(conn.Database),Utils.ReplaceQuatations(checkName));
      DataTable tmp = ExecuteDataTable(cmdText, conn);
      if (tmp == null || tmp.Rows.Count == 0 || !Utils.IsRowItemValid(tmp.Rows[0], 0))
      {
        return -1;
      }
      else
      {
        return (int)tmp.Rows[0].ItemArray[0];
      }
    }

    public static int GetCheckConstraintId( ConnectionParams cp, string checkName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetCheckConstraintId(conn, checkName);
      }
    }

    public static void DropCheck( SqlConnection conn, string owner, string tableName, string checkName )
    {
      string fullTblName = Utils.Qualify(owner) + "." + Utils.Qualify(tableName);

      string cmdText = "ALTER TABLE " + fullTblName + " DROP CONSTRAINT " + checkName;
      ExecuteCommand(cmdText, conn);
    }

    public static void DropCheck( ConnectionParams cp, string owner, string tableName, string checkName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        DropCheck(conn, owner, tableName, checkName);
      }
    }

    public static void CreateCheck( SqlConnection conn, string owner, string tableName, string checkName, string definition, bool notForReplication )
    {
      string cmdText = "ALTER TABLE " + Utils.Qualify(owner) + "." + Utils.Qualify(tableName)
        + " ADD CONSTRAINT " + Utils.Qualify(checkName) + " CHECK";
      //Check not for replication
      if (notForReplication)
      {
        cmdText += " NOT FOR REPLICATION";
      }
      cmdText += " (" + definition + ")";
      ExecuteCommand(cmdText, conn);
    }

    public static void CreateCheck( ConnectionParams cp, string owner, string tableName, string checkName, string definition, bool notForReplication )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        CreateCheck(conn, owner, tableName, checkName, definition, notForReplication);
      }
    }

    public static void RenameCheck( SqlConnection conn, string owner, string checkName, string newName )
    {
      string cmdText = "EXEC sp_rename '" + Utils.ReplaceQuatations(owner + "." + checkName) + "', '" + Utils.ReplaceQuatations(newName) + "',OBJECT";
      ExecuteCommand(cmdText, conn);
    }

    public static void RenameCheck( ConnectionParams cp, string owner, string checkName, string newName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        RenameCheck(conn, owner, checkName, newName);
      }
    }

    public static void BackupCheck( SqlConnection conn, string owner, string checkName )
    {
      string cmdText = "EXEC sp_rename '" + Utils.ReplaceQuatations(owner + "." + checkName) + "', '" + Utils.ReplaceQuatations(checkName) + "_bak_" + Utils.GetFormattedNow() + "',OBJECT";
      ExecuteCommand(cmdText, conn);
    }

    public static void BackupCheck( ConnectionParams cp, string owner, string checkName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        BackupCheck(conn, owner, checkName);
      }
    }

    public static string GetCheckDefinition( SqlConnection conn, string owner, string checkName )
    {
      string lines = String.Empty;

      string cmdText = "USE " + Utils.Qualify(conn.Database) + ";";
      cmdText += "EXEC sp_helptext '" + Utils.ReplaceQuatations(owner) + "." + Utils.ReplaceQuatations(checkName) + "'";

      SqlDataReader reader = ExecuteReader(cmdText, conn);
      while (reader.Read())
      {
        lines = reader.GetString(0);
      }

      lines = lines.Trim();
      if (lines.StartsWith("("))
      {
        lines = lines.Substring(1, lines.Length - 1);
      }

      if (lines.EndsWith(")"))
      {
        lines = lines.Substring(0, lines.Length - 1);
      }
      return lines;
    }

    public static string GetCheckDefinition( ConnectionParams cp, string owner, string checkName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetCheckDefinition(conn, owner, checkName);
      }
    }

    public static void ToggleCheckState( SqlConnection conn, bool enable, string owner, string tableName, string checkName )
    {
      string cmdText = "ALTER TABLE " + Utils.Qualify(owner) + "." + Utils.Qualify(tableName)
        + (!enable ? " NO" : String.Empty) + "CHECK CONSTRAINT " + Utils.Qualify(checkName);
      ExecuteCommand(cmdText, conn);
    }

    public static void ToggleCheckState( ConnectionParams cp, bool disable, string owner, string tableName, string checkName )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        ToggleCheckState(conn, disable, owner, tableName, checkName);
      }
    }

    #endregion //Check Constraints

    #region Indexes
    public static DataTable GetIndexes( SqlConnection conn, long objectId )
    {
      string cmdText = String.Format(Properties.Resources.Script_GetIndexesForObjects,objectId);
      return ExecuteDataTable(cmdText, conn);
    }



    public static DataTable GetIndexes( ConnectionParams cp, long objectId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetIndexes(conn, objectId);
      }
    }

    public static DataTable GetAllIndexes( SqlConnection conn )
    {
      string cmdText = Properties.Resources.Script_GetAllIndexes;
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetAllIndexes( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetAllIndexes(conn);
      }
    }

    public static DataTable GetIndexNoColumns( SqlConnection conn, int indexId, int objectId )
    {
      string cmdText = "SELECT dbo.syscolumns.name AS [column], dbo.systypes.name AS type";
      cmdText += " FROM dbo.syscolumns LEFT OUTER JOIN";
      cmdText += " dbo.systypes ON dbo.syscolumns.xusertype = dbo.systypes.xusertype";
      cmdText += " WHERE dbo.syscolumns.id = " + objectId.ToString();
      cmdText += " AND dbo.syscolumns.name NOT IN (SELECT COL_NAME(id, colid) FROM sysindexkeys WHERE id=" + objectId.ToString() + " AND indid=" + indexId.ToString() + ")";

      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetIndexNoColumns( ConnectionParams cp, int indexId, int objectId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetIndexNoColumns(conn, indexId, objectId);
      }
    }


    public static DataTable GetIndexColumns( SqlConnection conn, int indexId, int objectId )
    {
      string cmdText = " SELECT keyno, COL_NAME(id, colid) as [column]";
      cmdText += " FROM sysindexkeys ";
      cmdText += " WHERE id= " + objectId.ToString() + " AND indid=" + indexId.ToString() + " ORDER BY keyno";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetIndexColumns( ConnectionParams cp, int indexId, int objectId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetIndexColumns(conn, indexId, objectId);
      }
    }


    #endregion //Indexes

    #region Defaults

    public static DataTable GetDefaults( SqlConnection conn )
    {
      string cmdText = String.Format("USE [{0}]\r\n",conn.Database);
      cmdText += Properties.Resources.Script_GetDefaults;
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetDefaults( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetDefaults(conn);
      }
    }

    #endregion //Defaults

    #region Primary Keys

    public static DataTable GetPrimaryKeys( SqlConnection conn, long tableId )
    {
      string cmdText = String.Format("USE [{0}]\r\n", conn.Database);
      cmdText += tableId > 0 ? String.Format(Properties.Resources.Script_GetPrimaryKeysForTable,tableId) : Properties.Resources.Script_GetAllPrimaryKeys;
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetPrimaryKeys( ConnectionParams cp, long tableId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetPrimaryKeys(conn, tableId);
      }
    }

    public static IList<PrimaryKeyWrapper> GetPrimaryKeyList( ConnectionParams cp, long tableId )
    {
      string cmdText = String.Format("USE [{0}]\r\n", cp.Database);
      cmdText += tableId > 0 ? String.Format(Properties.Resources.Script_GetPrimaryKeysForTable, tableId) : Properties.Resources.Script_GetAllPrimaryKeys;

      IList<PrimaryKeyWrapper> primaryKeys = new List<PrimaryKeyWrapper>();
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = ExecuteReader(cmdText, conn);
        if (reader.HasRows)
        {

          try
          {
            PrimaryKeyWrapper key = null;
            while (reader.Read())
            {
              if (!Utils.IsDbValueValid(reader["id"]))
                continue;
              key = new PrimaryKeyWrapper(cp);
              key.ID = (int)reader["id"];
              key.LoadProperties();
              key.GetOtherInformation();
              primaryKeys.Add(key);
            }
          }
          finally
          {
            reader.Close();
          }
        }
      }
      return primaryKeys;
    }


    #endregion //Primarykeys

    #region Foreign Keys
    public static IList<ForeignKeyWrapper> GetForeignKeyList( ConnectionParams cp, long tableId )
    {
      string cmdText = String.Format("USE [{0}]\r\n",cp.Database);
      cmdText += tableId > 0 ? String.Format(Properties.Resources.Script_GetForeignKeysForTable, tableId) : Properties.Resources.Script_GetAllForeignKeys;

      IList<ForeignKeyWrapper> foreignKeys = new List<ForeignKeyWrapper>();
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = ExecuteReader(cmdText, conn);
        if (reader.HasRows)
        {

          try
          {
            ForeignKeyWrapper key = null;
            while (reader.Read())
            {
              if (!Utils.IsDbValueValid(reader["id"]))
                continue;
              key = new ForeignKeyWrapper(cp);
              key.ID = (int)reader["id"];
              key.LoadProperties();
              key.GetKey();
              key.PrepareHostAndRefCols();
              foreignKeys.Add(key);
            }
          }
          finally
          {
            reader.Close();
          }
        }
      }
      return foreignKeys;
    }

    public static DataTable GetForeignKeys( SqlConnection conn, long tableId )
    {

      string cmdText = String.Format("USE [{0}]\r\n", conn.Database);
      cmdText += tableId > 0 ? String.Format(Properties.Resources.Script_GetForeignKeysForTable, tableId) : Properties.Resources.Script_GetAllForeignKeys;

     
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetForeignKeys( ConnectionParams cp, long tableId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetForeignKeys(conn, tableId);
      }
    }

    public static DataTable GetAllForeignKeys( SqlConnection conn )
    {

      string cmdText = String.Format("USE [{0}]\r\n", conn.Database);
      cmdText += Properties.Resources.Script_GetAllForeignKeys;
     
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetAllForeignKeys( ConnectionParams cp )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetAllForeignKeys(conn);
      }
    }

    public static DataTable GetConstraintForForeignKey( SqlConnection conn, long tableId )
    {
      string cmdText = "SELECT (name) as qualifiedname,name, status,indid FROM sysindexes WHERE id=" + tableId + " AND (status & 2=2)";
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetConstraintForForeignKey( ConnectionParams cp, long tableId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetConstraintForForeignKey(conn, tableId);
      }
    }


    public static long GetIndexOfConstraintForForeignKey( SqlConnection conn, long tableId )
    {
      long result = -1;
      string cmdText = "SELECT name,status,indid FROM sysindexes WHERE id=" + tableId + " AND (status & 2=2)";
      SqlDataReader reader = ExecuteReader(cmdText, conn);
      
      if (reader.HasRows)
      {
        try
        {
          reader.Read();
          result = (short)reader["indid"];
        }
        finally
        {
          reader.Close();
        }
      }

      return result;
    }
    public static long GetIndexOfConstraintForForeignKey( ConnectionParams cp, long tableId )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetIndexOfConstraintForForeignKey(conn, tableId);
      }
    }



    public static DataTable GetColumnsByForeignKey( SqlConnection conn, long hostTableID, long indid )
    {
      string cmdText = "SELECT COL_NAME(id, colid) as colName FROM sysindexkeys WHERE id=" + hostTableID.ToString() + " AND indid=" + indid.ToString() + " ORDER BY keyno";
      return ExecuteDataTable(cmdText, conn);
    }



    public static DataTable GetColumnsByForeignKey( ConnectionParams cp, long hostTableID, long indid )
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetColumnsByForeignKey(conn, hostTableID, indid);
      }
    }

    #endregion //Fooreign Keys

    #region User Defined Data Types
    public static DataTable GetUserDataTypes( SqlConnection conn )
    {
      string cmdText = String.Format("USE [{0}\r\n]", conn.Database);
      cmdText += Properties.Resources.Script_GetUserDataTypes;     
      return ExecuteDataTable(cmdText, conn);
    }

    public static DataTable GetUserDataTypes( ConnectionParams cp)
    {
      using (SqlConnection conn = cp.CreateSqlConnection(true, false))
      {
        return GetUserDataTypes(conn);
      }
    }
    #endregion //User Defined Data Types
  }
}
