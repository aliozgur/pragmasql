/********************************************************************
  Class      : ObjectChangeHistoryFacade
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class ObjectChangeHistoryFacade
  {
    private static void AddParam(SqlCommand cmd, string paramName, SqlDbType type, object value)
    {
      cmd.Parameters.Add(paramName,type);
      if(value != null)
      {
        cmd.Parameters[paramName].Value = value;
      }
      else
      {
        cmd.Parameters[paramName].Value = DBNull.Value;
      }
    }


    public static void InsertObjectChangeHistoryRecord(ObjectChangeHistoryData data)
    {
#if PERSONAL_EDITION 
      return;
#else
      if(ConfigHelper.Current == null || !ConfigHelper.Current.PragmaSql_ObjectChangeHistoryLogEnabled)
      {
        return;
      }

      if(data == null)
      {
        return;
      }

      ConnectionParams cp = ConfigHelper.Current.PragmaSqlDbConn;
      if (cp == null)
      {
        return;
      }
      
      using( SqlConnection conn = cp.CreateSqlConnection(true,false) )
      {
        string cmdText = "exec spPragmaSQL_ObjectChangeHist_Insert @ServerName, @DatabaseName, @ObjectName, @ObjectScript, @ObjectType, @CreatedBy, @Comment";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd,"@ServerName",SqlDbType.VarChar, data.ServerName);
        AddParam(cmd,"@DatabaseName",SqlDbType.VarChar,data.DatabaseName);
        AddParam(cmd,"@ObjectName",SqlDbType.VarChar, data.ObjectName);
        AddParam(cmd,"@ObjectScript",SqlDbType.Text, data.ObjectScript);
        AddParam(cmd,"@ObjectType",SqlDbType.VarChar, data.ObjectType);
        AddParam(cmd,"@CreatedBy",SqlDbType.VarChar, data.CreatedBy);
        AddParam(cmd,"@Comment",SqlDbType.VarChar, data.Comment);
        cmd.ExecuteNonQuery();   
      }
#endif
    }
  }
}
