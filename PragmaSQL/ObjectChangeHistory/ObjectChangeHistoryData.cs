/********************************************************************
  Class      : ObjectChangeHistoryData
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public class ObjectChangeHistoryData
  {
    public string ServerName = String.Empty;
    public string DatabaseName = String.Empty;
    public string ObjectName = String.Empty;
    public string ObjectScript = String.Empty;
    public string ObjectType = String.Empty;
    public string CreatedBy = String.Empty;
    public string Comment = String.Empty;

    public ObjectChangeHistoryData CreateCopy()
    {
      ObjectChangeHistoryData result = new ObjectChangeHistoryData();
      result.ServerName = ServerName;
      result.DatabaseName = DatabaseName;
      result.ObjectName = ObjectName;
      result.ObjectScript = ObjectScript;
      result.ObjectType = ObjectType;
      result.CreatedBy = CreatedBy;
      result.Comment = Comment;

      return result;
    }

    public void CopyFrom(ObjectChangeHistoryData source)
    {
      if(source == null)
      {
        throw new NullParameterException("Source is null!");
      }

      ServerName = source.ServerName;
      DatabaseName = source.DatabaseName;
      ObjectName = source.ObjectName;
      ObjectScript = source.ObjectScript;
      ObjectType = source.ObjectType;
      CreatedBy = source.CreatedBy;
      Comment = source.Comment;
    }

    public static ObjectChangeHistoryData CreateFromScript(string username,SqlConnection conn, string script)
    {
      if(ConfigHelper.Current == null || !ConfigHelper.Current.PragmaSql_ObjectChangeHistoryLogEnabled)
      {
        return null;
      }

      if(conn == null)
      {
        return null;
      }

      int ObjectType = DBObjectType.None;
      bool isAlter = false;
      string objectName = String.Empty;

      objectName = ScriptingHelper.GetObjectNameFromScript(script,ref ObjectType,ref isAlter);
      if(String.IsNullOrEmpty( objectName) || ObjectType == DBObjectType.None)
      {
        return null;
      }

      ObjectChangeHistoryData result = new ObjectChangeHistoryData();
      result.ServerName = conn.DataSource;
      result.DatabaseName = conn.Database;
      result.ObjectName = objectName;
      result.ObjectScript = script;
      result.ObjectType = DBConstants.GetObjectTypeAbb(ObjectType);
      result.Comment = String.Empty;
      result.CreatedBy = username;

      return result;
    }
  }
}
