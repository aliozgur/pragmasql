/********************************************************************
  Class      : ProgrammabilityHelper
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Data.SqlClient;
using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public static class ProgrammabilityHelper
  {
    public static SqlConnection SqlConn = null;

    public static string GetColumns( SqlConnection sqlConnection, string objName, bool isParamList )
    {
      StringBuilder sb = new StringBuilder();

      SqlConnection conn = SqlConn;
      if (sqlConnection != null)
      {
        conn = sqlConnection;
      }

      string cmdText = ResManager.GetDBScript("Script_Schema_Columns");
      cmdText = String.Format(cmdText, objName);

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();
      string colName = String.Empty;
      try
      {
        int rowCnt = 0;
        while (reader.Read())
        {
          rowCnt++;
          colName = (string)reader["COLUMN_NAME"];
          if (rowCnt == 1)
          {
            sb.AppendLine("\t" + (isParamList ? "@" : String.Empty) + colName);
          }
          else
          {
            sb.AppendLine("\t, " + (isParamList ? "@" : String.Empty) + colName);
          }
        }
      }
      finally
      {
        reader.Close();
      }

      return sb.ToString();
    }

    public static string GetColumnsAsParamListWithDataType( SqlConnection sqlConnection, string objName, bool eachInNewLine )
    {
      StringBuilder sb = new StringBuilder();

      SqlConnection conn = SqlConn;
      if (sqlConnection != null)
      {
        conn = sqlConnection;
      }

      string cmdText = ResManager.GetDBScript("Script_Schema_Columns");
      cmdText = String.Format(cmdText, objName);

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();

      string colName = String.Empty;
      string dataType = String.Empty;
      int len = 0;
      byte prec = 0;
      int scale = 0;
      string fullParamName = String.Empty;
      try
      {
        int rowCnt = 0;
        while (reader.Read())
        {
          rowCnt++;


          colName = (string)reader["COLUMN_NAME"];
          len = (reader["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull)) ? (int)reader["CHARACTER_MAXIMUM_LENGTH"] : 0;
          prec = (reader["NUMERIC_PRECISION"].GetType() != typeof(DBNull)) ? (byte)reader["NUMERIC_PRECISION"] : (byte)0;
          scale = (reader["NUMERIC_SCALE"].GetType() != typeof(DBNull)) ? (int)reader["NUMERIC_SCALE"] : 0;
          dataType = (string)reader["DATA_TYPE"];

          dataType = DBConstants.GetFullyQualifiedDataTypeName(true, dataType, len, scale, prec);

          if (rowCnt == 1)
          {
            fullParamName = "@" + colName + " " + dataType;
          }
          else
          {
            fullParamName = ", @" + colName + " " + dataType;
          }

          if (eachInNewLine)
          {
            sb.AppendLine(fullParamName);
          }
          else
          {
            sb.Append(fullParamName);
          }
        }
      }
      finally
      {
        reader.Close();
      }

      return sb.ToString();
    }

    public static string GetColumnDetails( ConnectionParams cp, string tblName )
    {
      IList<TableColumnSpec> cols = GetTableColumnsSpecification(cp, tblName);
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("/*");
      sb.AppendLine("\t" + tblName + " Columns");
      sb.AppendLine("*/");

      int rowCnt = 0;
      string identitySpec = String.Empty;
      string collationSpec = String.Empty;

      foreach (TableColumnSpec col in cols)
      {
        rowCnt++;
        if (col.IsIdentity)
        {
          identitySpec = " IDENTITY(" + col.IdentitySeed.ToString() + "," + col.IdentityIncrement.ToString() + ")";
        }
        else
        {
          identitySpec = String.Empty;
        }

        if (String.IsNullOrEmpty(col.Collation))
        {
          collationSpec = String.Empty;
        }
        else
        {
          collationSpec = "COLLATE " + col.Collation;
        }

        if (rowCnt == 1)
        {
          sb.AppendLine("\t" + col.Name + " " + col.FullyQualifiedDataType + identitySpec + " " + collationSpec + " " + (col.IsNullable ? " NULL" : " NOT NULL"));
        }
        else
        {
          sb.AppendLine("\t, " + col.Name + " " + col.FullyQualifiedDataType + identitySpec + " " + collationSpec + " " + (col.IsNullable ? " NULL" : " NOT NULL"));
        }

      }

      return sb.ToString();
    }

    public static string GetColumnDetails( SqlConnection sqlConnection, string tblName, bool localTableVar, string localTableVarName )
    {
      StringBuilder sb = new StringBuilder();

      SqlConnection conn = SqlConn;
      if (sqlConnection != null)
      {
        conn = sqlConnection;
      }

      string cmdText = ResManager.GetDBScript("Script_Schema_Columns");
      cmdText = String.Format(cmdText, tblName);

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();

      string colName = String.Empty;
      string dataType = String.Empty;
      int len = 0;
      byte prec = 0;
      int scale = 0;
      string nullableMode = String.Empty;
      string collationName = String.Empty;

      sb.AppendLine("/*");
      sb.AppendLine("\t" + tblName + " Columns");
      sb.AppendLine("*/");

      try
      {
        int rowCnt = 0;
        while (reader.Read())
        {
          rowCnt++;


          colName = "[" + (string)reader["COLUMN_NAME"] + "]";
          len = (reader["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull)) ? (int)reader["CHARACTER_MAXIMUM_LENGTH"] : 0;
          prec = (reader["NUMERIC_PRECISION"].GetType() != typeof(DBNull)) ? (byte)reader["NUMERIC_PRECISION"] : (byte)0;
          scale = (reader["NUMERIC_SCALE"].GetType() != typeof(DBNull)) ? (int)reader["NUMERIC_SCALE"] : 0;
          dataType = (string)reader["DATA_TYPE"];
          nullableMode = (reader["IS_NULLABLE"].GetType() != typeof(DBNull)) ? (string)reader["IS_NULLABLE"] : "NO";
          nullableMode = (nullableMode.ToLowerInvariant() == "YES") ? " NULL" : " NOT NULL";

          collationName = (reader["COLLATION_NAME"].GetType() != typeof(DBNull)) ? (string)reader["COLLATION_NAME"] : String.Empty;
          collationName = (!String.IsNullOrEmpty(collationName)) ? " COLLATE " + collationName : String.Empty;

          dataType = DBConstants.GetFullyQualifiedDataTypeName(true, dataType, len, scale, prec);

          if (rowCnt == 1)
          {
            sb.AppendLine("\t" + colName + " " + dataType + collationName + nullableMode);
          }
          else
          {
            sb.AppendLine("\t, " + colName + " " + dataType + collationName + nullableMode);
          }
        }
      }
      finally
      {
        reader.Close();
      }

      string result = sb.ToString();

      if (localTableVar)
      {
        result = "DECLARE @" + (String.IsNullOrEmpty(localTableVarName) ? "tbl" : localTableVarName) + " TABLE"
          + "\n(\n"
          + result
          + "\n)\n";
      }
      return result;
    }

    public static string GetRoutineParams( SqlConnection sqlConnection, string routineName )
    {
      StringBuilder sb = new StringBuilder();

      SqlConnection conn = SqlConn;
      if (sqlConnection != null)
      {
        conn = sqlConnection;
      }

      string cmdText = ResManager.GetDBScript("Script_Schema_RoutineParams");
      cmdText = String.Format(cmdText, routineName);

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();

      string paramName = String.Empty;
      string paramMode = String.Empty;

      try
      {
        int rowCnt = 0;
        while (reader.Read())
        {
          paramName = (string)reader["PARAMETER_NAME"];
          if (String.IsNullOrEmpty(paramName))
          {
            continue;
          }
          rowCnt++;

          paramMode = (reader["PARAMETER_MODE"].GetType() != typeof(DBNull)) ? (string)reader["PARAMETER_MODE"] : "IN";
          paramMode = (paramMode.ToLowerInvariant().Contains("out")) ? " OUTPUT" : String.Empty;
          if (rowCnt == 1)
          {
            sb.Append(paramName + paramMode);
          }
          else
          {
            sb.Append(", " + paramName + paramMode);
          }
        }
      }
      finally
      {
        reader.Close();
      }

      return sb.ToString();
    }

    public static string GetRoutineParamsWithDataType( SqlConnection sqlConnection, string routineName )
    {
      StringBuilder sb = new StringBuilder();

      SqlConnection conn = SqlConn;
      if (sqlConnection != null)
      {
        conn = sqlConnection;
      }

      string cmdText = ResManager.GetDBScript("Script_Schema_RoutineParams");
      cmdText = String.Format(cmdText, routineName);

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();

      string paramName = String.Empty;
      string dataType = String.Empty;
      int len = 0;
      byte prec = 0;
      int scale = 0;
      string paramMode = String.Empty;

      try
      {
        int rowCnt = 0;
        while (reader.Read())
        {
          paramName = (string)reader["PARAMETER_NAME"];
          if (String.IsNullOrEmpty(paramName))
          {
            continue;
          }
          rowCnt++;

          len = (reader["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull)) ? (int)reader["CHARACTER_MAXIMUM_LENGTH"] : 0;
          prec = (reader["NUMERIC_PRECISION"].GetType() != typeof(DBNull)) ? (byte)reader["NUMERIC_PRECISION"] : (byte)0;
          scale = (reader["NUMERIC_SCALE"].GetType() != typeof(DBNull)) ? (int)reader["NUMERIC_SCALE"] : 0;
          dataType = (string)reader["DATA_TYPE"];
          paramMode = (reader["PARAMETER_MODE"].GetType() != typeof(DBNull)) ? (string)reader["PARAMETER_MODE"] : "IN";
          paramMode = (paramMode.ToLowerInvariant().Contains("out")) ? " OUTPUT" : String.Empty;

          dataType = DBConstants.GetFullyQualifiedDataTypeName(true, dataType, len, scale, prec);

          if (rowCnt == 1)
          {
            sb.Append(paramName + " " + dataType + paramMode);
          }
          else
          {
            sb.Append(", " + paramName + " " + dataType + paramMode);
          }
        }
      }
      finally
      {
        reader.Close();
      }

      return sb.ToString();
    }

    public static string GetProcedureExecuteScript( SqlConnection sqlConnection, string spName )
    {
      StringBuilder sb = new StringBuilder();
      StringBuilder sbDeclare = new StringBuilder();
      StringBuilder sbExecute = new StringBuilder();

      SqlConnection conn = SqlConn;
      if (sqlConnection != null)
      {
        conn = sqlConnection;
      }

      string cmdText = ResManager.GetDBScript("Script_Schema_RoutineParams");
      cmdText = String.Format(cmdText, spName);

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();

      string paramName = String.Empty;
      string dataType = String.Empty;
      int len = 0;
      byte prec = 0;
      int scale = 0;
      string paramMode = String.Empty;
      string schemaSpec = String.Empty;


      try
      {
        sbDeclare.AppendLine("DECLARE @RC int");
        sbExecute.Append("EXECUTE @RC = ");

        int rowCnt = 0;
        while (reader.Read())
        {
          rowCnt++;


          paramName = (string)reader["PARAMETER_NAME"];
          len = (reader["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull)) ? (int)reader["CHARACTER_MAXIMUM_LENGTH"] : 0;
          prec = (reader["NUMERIC_PRECISION"].GetType() != typeof(DBNull)) ? (byte)reader["NUMERIC_PRECISION"] : (byte)0;
          scale = (reader["NUMERIC_SCALE"].GetType() != typeof(DBNull)) ? (int)reader["NUMERIC_SCALE"] : 0;
          dataType = (string)reader["DATA_TYPE"];
          paramMode = (reader["PARAMETER_MODE"].GetType() != typeof(DBNull)) ? (string)reader["PARAMETER_MODE"] : "IN";
          paramMode = (paramMode.ToLowerInvariant().Contains("out")) ? " OUTPUT" : String.Empty;

          dataType = DBConstants.GetFullyQualifiedDataTypeName(true, dataType, len, scale, prec);

          if (String.IsNullOrEmpty(schemaSpec))
          {
            schemaSpec = "[" + (string)reader["SPECIFIC_CATALOG"] + "].[" + (string)reader["SPECIFIC_SCHEMA"] + "].[" + spName + "] ";
            sbExecute.Append(schemaSpec);
          }

          sbDeclare.AppendLine("DECLARE " + paramName + " " + dataType);
          if (rowCnt == 1)
          {
            sbExecute.Append(paramName + paramMode);
          }
          else
          {
            sbExecute.Append(", " + paramName + paramMode);
          }
        }
      }
      finally
      {
        reader.Close();
      }

      if (String.IsNullOrEmpty(schemaSpec))
      {
        schemaSpec = "[dbo].[" + sqlConnection.Database + "].[" + spName + "] ";
        sbExecute.Append(schemaSpec);
      }

      sb.Append(sbDeclare.ToString());
      sb.AppendLine();
      sb.Append(sbExecute.ToString());
      return sb.ToString();
    }

    public static string GetTableValuedFunctionResultColumns( SqlConnection sqlConnection, string functionName, bool localTableVar, string localTableVarName )
    {
      StringBuilder sb = new StringBuilder();

      SqlConnection conn = SqlConn;
      if (sqlConnection != null)
      {
        conn = sqlConnection;
      }

      string cmdText = ResManager.GetDBScript("Script_Schema_RoutineColumns");
      cmdText = String.Format(cmdText, functionName);

      SqlCommand cmd = new SqlCommand(cmdText, conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();

      string colName = String.Empty;
      string dataType = String.Empty;
      int len = 0;
      byte prec = 0;
      int scale = 0;
      string nullableMode = String.Empty;
      string collationName = String.Empty;

      try
      {
        int rowCnt = 0;
        while (reader.Read())
        {
          rowCnt++;


          colName = "[" + (string)reader["COLUMN_NAME"] + "]";
          len = (reader["CHARACTER_MAXIMUM_LENGTH"].GetType() != typeof(DBNull)) ? (int)reader["CHARACTER_MAXIMUM_LENGTH"] : 0;
          prec = (reader["NUMERIC_PRECISION"].GetType() != typeof(DBNull)) ? (byte)reader["NUMERIC_PRECISION"] : (byte)0;
          scale = (reader["NUMERIC_SCALE"].GetType() != typeof(DBNull)) ? (int)reader["NUMERIC_SCALE"] : 0;
          dataType = (string)reader["DATA_TYPE"];
          nullableMode = (reader["IS_NULLABLE"].GetType() != typeof(DBNull)) ? (string)reader["IS_NULLABLE"] : "NO";
          nullableMode = (nullableMode.ToLowerInvariant() == "YES") ? " NULL" : " NOT NULL";

          collationName = (reader["COLLATION_NAME"].GetType() != typeof(DBNull)) ? (string)reader["COLLATION_NAME"] : String.Empty;
          collationName = (!String.IsNullOrEmpty(collationName)) ? " COLLATE " + collationName : String.Empty;

          dataType = DBConstants.GetFullyQualifiedDataTypeName(true, dataType, len, scale, prec);

          if (rowCnt == 1)
          {
            sb.AppendLine("\t" + colName + " " + dataType + collationName + nullableMode);
          }
          else
          {
            sb.AppendLine("\t, " + colName + " " + dataType + collationName + nullableMode);
          }
        }
      }
      finally
      {
        reader.Close();
      }

      string result = sb.ToString();

      if (localTableVar)
      {
        result = "DECLARE @" + (String.IsNullOrEmpty(localTableVarName) ? "tbl" : localTableVarName) + " TABLE"
          + "\n(\n"
          + result
          + "\n)\n";
      }
      return result; ;
    }

    public static ObjectInfo GetObjectInfo( ConnectionParams connParams, string database, string objName )
    {
      if (connParams == null)
      {
        throw new NullParameterException("Connection params is null!");
      }
      return GetObjectInfo(connParams.ConnectionString, database, objName);
    }

    public static ObjectInfo GetObjectInfo( SqlConnection conn, string database, string objName )
    {
      if (conn == null)
      {
        throw new NullParameterException("Connection is null!");
      }
      return GetObjectInfo(conn.ConnectionString, database, objName);
    }

    public static ObjectInfo GetObjectInfo( string connectionString, string database, string objName )
    {
      if (String.IsNullOrEmpty(connectionString))
      {
        throw new NullParameterException("Connection string is null or empty!");
      }

      ObjectInfo result = null;
      using (SqlConnection conn = new SqlConnection(connectionString))
      {
        conn.Open();
        if (!String.IsNullOrEmpty(database))
        {
          conn.ChangeDatabase(database);
        }

        string script = ResManager.GetDBScript("Script_GetObjectInfoByName");
        script = String.Format(script, objName);
        SqlCommand cmd = new SqlCommand(script, conn);
        cmd.CommandTimeout = 0;
        SqlDataReader reader = cmd.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            result = new ObjectInfo();
            result.ObjectID = (int)reader["id"];
            result.ObjectName = (string)reader["name"];
            result.ObjectTypeAbb = (string)reader["xtype"];
            result.ObjectType = DBConstants.GetDBObjectType((reader["xtype"] as string));
						result.ObjectOwner = (string)reader["owner"];
					}
        }
        finally
        {
          reader.Close();
        }
      }
      return result;
    }

    public static IList<TableColumnSpec> GetTableColumnsSpecification( ConnectionParams cp, string tableName )
    {
      IList<TableColumnSpec> result = new List<TableColumnSpec>();
      IList<string> primaryKeys = new List<string>();

      TableColumnSpec col = null;
      TableColumnSpec identityCol = null;

      using (SqlConnection conn = new SqlConnection(cp.ConnectionString))
      {
        conn.Open();
        // 1- Read primary keys
        string script = "exec sp_pkeys '{0}'";
        script = String.Format(script, tableName);
        SqlCommand cmd = new SqlCommand(script, conn);
        cmd.CommandTimeout = 0;
        SqlDataReader reader = cmd.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            primaryKeys.Add(((string)reader["COLUMN_NAME"]).ToLowerInvariant());
          }
        }
        finally
        {
          reader.Close();
        }


        // 2- Read columns
        script = ResManager.GetDBScript("Script_GetTableColumns");
        script = String.Format(script, tableName);
        cmd.CommandText = script;
        reader = cmd.ExecuteReader();

        try
        {
          while (reader.Read())
          {
            col = new TableColumnSpec();
            col.TableName = tableName;
            col.TableId = (int)reader["id"];
            col.Name = (string)reader["name"];
            col.Collation = reader["collation"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["collation"];
            col.IsComputed = (int)reader["iscomputed"] == 0 ? false : true;
            col.IsNullable = (int)reader["isnullable"] == 0 ? false : true;
            col.IsIdentity = (int)reader["isIdentity"] == 0 ? false : true;
            col.DataType = reader["typename"].GetType() == typeof(DBNull) ? "[???]" : (string)reader["typename"];
            col.Length = reader["lengthx"].GetType() == typeof(DBNull) ? (int)0 : (int)reader["lengthx"];
            col.Precision = reader["prec"].GetType() == typeof(DBNull) ? (short)0 : (short)reader["prec"];
            col.Scale = reader["scale"].GetType() == typeof(DBNull) ? 0 : (int)reader["scale"];
            col.FullyQualifiedDataType = DBConstants.GetFullyQualifiedDataTypeName(true, col.DataType, col.Length, col.Scale, col.Precision);
            col.IsInPrimaryKey = primaryKeys.Contains(col.Name.ToLowerInvariant());

            result.Add(col);

            if (col.IsIdentity && identityCol == null)
            {
              identityCol = col;
            }
          }
        }
        finally
        {
          reader.Close();
        }

        // 3- Read identity seed and increment
        if (identityCol != null)
        {
          script = ResManager.GetDBScript("Script_GetTableIdentityIncrementAndSeed");
          script = String.Format(script, tableName);
          cmd.CommandText = script;
          reader = cmd.ExecuteReader();
          try
          {
            while (reader.Read())
            {
              identityCol.IdentitySeed = reader["idSeed"].GetType() == typeof(DBNull) ? 0 : (decimal)reader["idSeed"];
              identityCol.IdentityIncrement = reader["idIncrement"].GetType() == typeof(DBNull) ? 0 : (decimal)reader["idIncrement"];
              break;
            }
          }
          finally
          {
            reader.Close();
          }
        }
      }

      return result;
    }

    public static string GenerateCrudProc_List( IList<TableColumnSpec> columns, string username, string prefix, string group, string tableNameAbb, string operation, string tableName )
    {

      string procScript = String.Empty;

      procScript = "/*** Generated with PragmaSQL  on " + DateTime.Now.ToString() + (String.IsNullOrEmpty(username) ? String.Empty : " by " + username) + " ***/\n";
      procScript += "\n" + "CREATE PROCEDURE dbo." + prefix + group + tableNameAbb + operation;
      procScript += "\n" + "AS BEGIN";
      procScript += "\n\t" + "SELECT";
      procScript += "\n";

      int i = 0;
      foreach (TableColumnSpec col in columns)
      {
        if (i == 0)
        {
          procScript += "\t\t" + col.Name + "\n";
        }
        else
        {
          procScript += "\t\t, " + col.Name + "\n";
        }
        i++;
      }

      procScript += "\tFROM " + tableName + "\n";
      procScript += "END\n\nGO\n\n";

      return procScript;
    }

    public static string GenerateCrudProc_Get( IList<TableColumnSpec> columns, string username, string prefix, string group, string tableNameAbb, string operation, string tableName )
    {
      string noWhereStr = "\t\t" + "<TODO: Add get criteria here>" + "\n";

      string procScript = String.Empty;
      string colStr = String.Empty;
      string paramStr = String.Empty;
      string whereStr = String.Empty;

      int pOrder = 1;
      int wOrder = 1;
      int i = 0;

      foreach (TableColumnSpec col in columns)
      {
        if (i == 0)
        {
          colStr += "\t\t" + col.Name + "\n";
        }
        else
        {
          colStr += "\t\t, " + col.Name + "\n";
        }

        if (col.IsInPrimaryKey)
        {
          if (pOrder == 1)
          {
            paramStr += "@" + col.Name + " " + col.FullyQualifiedDataType + "\n";
          }
          else
          {
            paramStr += ", @" + col.Name + " " + col.FullyQualifiedDataType + "\n";
          }
          pOrder++;

          if (wOrder == 1)
          {
            whereStr += "\t\t" + "( " + col.Name + " = @" + col.Name + " ) " + "\n";
          }
          else
          {
            whereStr += "\t\t" + " AND ( " + col.Name + " = @" + col.Name + " ) " + "\n";
          }
          wOrder++;
        }
        i++;
      }

      procScript = "/*** Generated with PragmaSQL  on " + DateTime.Now.ToString() + (String.IsNullOrEmpty(username) ? String.Empty : " by " + username) + " ***/\n";
      procScript += "CREATE PROCEDURE dbo." + prefix + group + tableNameAbb + operation + "\n";
      procScript += paramStr;
      procScript += "AS BEGIN" + "\n";
      procScript += "\t" + "SELECT" + "\n";
      procScript += colStr;
      procScript += "\t" + "FROM " + tableName + "\n";
      procScript += "\t" + "WHERE" + "\n";

      if (String.IsNullOrEmpty(whereStr.Trim()))
      {
        procScript += noWhereStr + "\n";
      }
      else
      {
        procScript += whereStr + "\n";
      }

      procScript += "END\n\nGO\n\n";
      return procScript;
    }

    public static string GenerateCrudProc_Insert( IList<TableColumnSpec> columns, string username, string prefix, string group, string tableNameAbb, string operation, string tableName )
    {
      int pOrder = 1;
      string paramStr = String.Empty;
      string valueStr = String.Empty;
      string colStr = String.Empty;
      string procScript = String.Empty;

      foreach (TableColumnSpec col in columns)
      {
        if (col.IsIdentity || col.IsComputed)
        {
          continue;
        }

        if (pOrder == 1)
        {
          colStr += col.Name;
          paramStr += "@" + col.Name + " " + col.FullyQualifiedDataType + "\n";
          valueStr += "@" + col.Name;
        }
        else
        {
          colStr += ", " + col.Name;
          paramStr += ", @" + col.Name + " " + col.FullyQualifiedDataType + "\n";
          valueStr += ", @" + col.Name;
        }

        pOrder++;
      }

      procScript = "/*** Generated with PragmaSQL  on " + DateTime.Now.ToString() + (String.IsNullOrEmpty(username) ? String.Empty : " by " + username) + " ***/\n";
      procScript += "CREATE PROCEDURE dbo." + prefix + group + tableNameAbb + operation + "\n";
      procScript += paramStr;
      procScript += "AS BEGIN\n";
      procScript += "\t" + "INSERT INTO " + tableName + "( " + colStr + " )" + "\n";
      procScript += "\t" + "VALUES( " + valueStr + " )" + "\n";
      procScript += "END\n\nGO\n\n";

      return procScript;
    }

    public static string GenerateCrudProc_Update( IList<TableColumnSpec> columns, string username, string prefix, string group, string tableNameAbb, string operation, string tableName )
    {
      string noWhereStr = "\t\t" + " <TODO: Add update criteria here>" + "\n";
      string noParamStr = "\t\t" + " <TODO: Add update criteria parameters here>" + "\n";

      string setStr = String.Empty;
      string paramStr = String.Empty;
      string whereStr = String.Empty;
      string procScript = String.Empty;

      int sOrder = 1;
      int pOrder = 1;
      int wOrder = 1;
      foreach (TableColumnSpec col in columns)
      {
        if (!(col.IsIdentity || col.IsComputed))
        {
          if (sOrder == 1)
          {
            setStr += "\t\t" + col.Name + " = @" + col.Name + "\n";
          }
          else
          {
            setStr += "\t\t, " + col.Name + " = @" + col.Name + "\n";
          }
          sOrder++;
        }

        if (!col.IsComputed)
        {
          if (pOrder == 1)
          {
            paramStr += "@" + col.Name + " " + col.FullyQualifiedDataType + "\n";
          }
          else
          {
            paramStr += ", @" + col.Name + " " + col.FullyQualifiedDataType + "\n";
          }
          pOrder++;
        }

        if (col.IsInPrimaryKey)
        {
          if (wOrder == 1)
          {
            whereStr += "\t\t( " + col.Name + " = @" + col.Name + " ) " + "\n";
          }
          else
          {
            whereStr += "\t\t AND ( " + col.Name + " = @" + col.Name + " ) " + "\n";
          }
          wOrder++;
        }
      }

      procScript = "/*** Generated with PragmaSQL  on " + DateTime.Now.ToString() + (String.IsNullOrEmpty(username) ? String.Empty : " by " + username) + " ***/\n";
      procScript += "CREATE PROCEDURE dbo." + prefix + group + tableNameAbb + operation + "\n";

      if (String.IsNullOrEmpty(paramStr.Trim()))
      {
        procScript += noParamStr;
      }
      else
      {
        procScript += paramStr;
      }

      procScript += "AS BEGIN\n";
      procScript += "\t" + "UPDATE " + tableName + "\n";
      procScript += "\t" + "SET" + "\n";
      procScript += setStr + "\n";
      procScript += "\t" + "WHERE" + "\n";

      if (String.IsNullOrEmpty(whereStr.Trim()))
      {
        procScript += noWhereStr + "\n";
      }
      else
      {
        procScript += whereStr + "\n";
      }

      procScript += "END\n\nGO\n\n";

      return procScript;
    }

    public static string GenerateCrudProc_Delete( IList<TableColumnSpec> columns, string username, string prefix, string group, string tableNameAbb, string operation, string tableName )
    {
      string noWhereStr = "\t\t" + "<TODO: Add delete criteria here>\n";
      string noParamsStr = "<TODO: Add delete criteria parameters here>\n";

      string paramStr = String.Empty;
      string whereStr = String.Empty;
      string procScript = String.Empty;

      int pOrder = 1;
      int wOrder = 1;

      foreach (TableColumnSpec col in columns)
      {
        if (!col.IsInPrimaryKey)
        {
          continue;
        }

        if (pOrder == 1)
        {
          paramStr += "@" + col.Name + " " + col.FullyQualifiedDataType + "\n";
        }
        else
        {
          paramStr += ", @" + col.Name + " " + col.FullyQualifiedDataType + "\n";
        }

        pOrder++;

        if (wOrder == 1)
        {
          whereStr += "\t\t( " + col.Name + " = @" + col.Name + " ) " + "\n";
        }
        else
        {
          whereStr += "\t\t AND ( " + col.Name + " = @" + col.Name + " ) " + "\n";
        }
        wOrder++;
      }


      procScript = "/*** Generated with PragmaSQL  on " + DateTime.Now.ToString() + (String.IsNullOrEmpty(username) ? String.Empty : " by " + username) + " ***/\n";
      procScript += "CREATE PROCEDURE dbo." + prefix + group + tableNameAbb + operation + "\n";

      if (String.IsNullOrEmpty(paramStr.Trim()))
      {
        procScript += noParamsStr;
      }
      else
      {
        procScript += paramStr;
      }

      procScript += "AS BEGIN\n";
      procScript += "\t" + "DELETE " + tableName + "\n";
      procScript += "\t" + "WHERE" + "\n";

      if (String.IsNullOrEmpty(whereStr.Trim()))
      {
        procScript += noWhereStr + "\n";
      }
      else
      {
        procScript += whereStr + "\n";
      }
      procScript += "END\n\nGO\n\n";
      return procScript;
    }


  }// Class end
} // Namespace end
