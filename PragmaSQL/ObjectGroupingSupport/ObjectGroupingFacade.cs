/********************************************************************
  Class      : ObjectGroupingFacade
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


using PragmaSQL.Core;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public struct HelpTextDefinition
  {
    public string HelpText;
    public string Format;
    public string UpdatedBy;
  }

  public class ObjectGroupingFacade
  {
    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get
      {
        return _connParams;
      }
      set
      {
        _connParams = value.CreateCopy();
      }
    }


    private void AddParam(SqlCommand cmd, string paramName, SqlDbType type, object value)
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



    public bool IsObjectGroupingSupportInstalled( )
    {
      int result = 0;
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("Script_ObjectGroupingSupportCheck");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            result = reader["Result"].GetType() == typeof(DBNull) ? 0 : (int)reader["Result"];
          }
        }
        finally
        {
          reader.Close();
        }
      }

      return result == 1 ? true : false;
    }

    public void InstallObjectGroupingSupport( )
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }
      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("Script_ObjectGroupingSupport");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        conn.Open();
        cmd.ExecuteNonQuery();
      }
    }

    public IList<ObjectGroupingItemData> GetChildren( int? parentID )
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      IList<ObjectGroupingItemData> result = new List<ObjectGroupingItemData>();

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("spPragmaSQL_ObjectGroup_List");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd,"@ParentID",System.Data.SqlDbType.Int,parentID);
        
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        string name = String.Empty;
        int? type = null;
        int? id = null;
        string parentObjName = String.Empty;
        string createdBy = String.Empty;
        string updatedBy = String.Empty;
        string helpText = String.Empty;
        string helpTextFormat = String.Empty;

        ObjectGroupingItemData data = null;
        try
        {
          while (reader.Read())
          {
            name = reader["Name"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Name"];
            type = reader["ObjType"].GetType() == typeof(DBNull) ? null : (int?)reader["ObjType"];
            id = reader["ObjectID"].GetType() == typeof(DBNull) ? null : (int?)reader["ObjectID"];
            parentObjName = reader["ParentObjName"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["ParentObjName"];
            createdBy = reader["CreatedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["CreatedBy"];
            updatedBy = reader["UpdatedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["UpdatedBy"];

            helpText = reader["Description"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Description"];
            helpTextFormat = reader["DescriptionFormat"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["DescriptionFormat"];

            data = ObjectGroupingItemDataFactory.Create(name, type, id, parentObjName, parentID, createdBy);
            data.HelpText = helpText;
            data.HelpTextFormat = helpTextFormat;
            data.UpdatedBy = updatedBy;
            result.Add(data);
          }
        }
        finally
        {
          reader.Close();
        }
      }

      return result;
    }


    public void AddItem( ObjectGroupingItemData data, string desc, string descFormat )
    {

      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }
      if (data == null)
      {
        throw new NullParameterException("Item data is null!");
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("spPragmaSQL_ObjectGroup_Insert");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;

        AddParam(cmd,"@ParentID",SqlDbType.Int,data.ParentID);
        AddParam(cmd,"@Name",SqlDbType.VarChar, data.Name);
        AddParam(cmd,"@ParentObjName",SqlDbType.VarChar, data.ParentObjectName);
        AddParam(cmd,"@Description",SqlDbType.Text, desc);
        AddParam(cmd,"@DescriptionFormat",SqlDbType.VarChar, descFormat);
        AddParam(cmd,"@ObjType",SqlDbType.Int, data.Type);
        AddParam(cmd,"@CreatedBy",SqlDbType.VarChar, _connParams.CurrentUsername);
        SqlParameter param = cmd.Parameters.Add("@ObjectID", System.Data.SqlDbType.Int);
        param.Direction = System.Data.ParameterDirection.Output;
        conn.Open();
        cmd.ExecuteNonQuery();
        data.ID = (int?)param.Value;
      }
    }

    public void AddItem( ObjectGroupingItemData data )
    {
      AddItem(data, String.Empty, String.Empty);
    }


    public void DeleteItem( int? id )
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("spPragmaSQL_ObjectGroup_Delete");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd,"@ObjectID",SqlDbType.Int, id);
        conn.Open();
        cmd.ExecuteNonQuery();
      }
    }

    public void DeleteItem( ObjectGroupingItemData data )
    {
      if (data == null)
      {
        throw new NullParameterException("Item data is null!");
      }
      DeleteItem(data.ID);
    }

    public void UpdateHelpText( ObjectGroupingItemData data )
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      if(data == null)
      {
        return;
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("spPragmaSQL_ObjectGroup_UpdateDescription");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;

        AddParam(cmd,"@ObjectID",SqlDbType.Int, data.ID);
        AddParam(cmd,"@Description",SqlDbType.Text, data.HelpText);
        AddParam(cmd,"@DescriptionFormat",SqlDbType.VarChar, data.HelpTextFormat);
        AddParam(cmd,"@UpdatedBy",SqlDbType.VarChar, _connParams.CurrentUsername);
        conn.Open();
        cmd.ExecuteNonQuery();
        data.UpdatedBy = _connParams.CurrentUsername;
      }
    }

    public HelpTextDefinition GetHelpText( int? id )
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      HelpTextDefinition result = new HelpTextDefinition();

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("spPragmaSQL_ObjectGroup_GetHelpText");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd,"@ObjectID",SqlDbType.Int, id);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        string desc = String.Empty;
        string descFormat = String.Empty;
        string updatedBy = String.Empty;

        try
        {
          while (reader.Read())
          {
            desc = reader["Description"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Description"];
            descFormat = reader["DescriptionFormat"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["DescriptionFormat"];
            updatedBy = reader["UpdatedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["UpdatedBy"];
            result.HelpText = desc;
            result.Format = descFormat;
            result.UpdatedBy = updatedBy;
            break;
          }
        }
        finally
        {
          reader.Close();
        }
      }

      return result;
    }

    public void UpdateItem( ObjectGroupingItemData data )
    {
      if (data == null)
      {
        throw new NullParameterException("Item data is null!");
      }
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }
      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = ResManager.GetDBScript("spPragmaSQL_ObjectGroup_Update");
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;

        AddParam(cmd,"@ObjectID",SqlDbType.Int, data.ID);
        AddParam(cmd,"@ParentID",SqlDbType.Int, data.ParentID);
        AddParam(cmd,"@Name",SqlDbType.VarChar, data.Name);
        AddParam(cmd,"@UpdatedBy",SqlDbType.VarChar, _connParams.CurrentUsername);
        conn.Open();
        cmd.ExecuteNonQuery();
      }

    }
  } // Class end
} // Namespace end
