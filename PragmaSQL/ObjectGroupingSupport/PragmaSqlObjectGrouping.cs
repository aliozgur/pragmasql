using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


using PragmaSQL.Common;
using PragmaSQL.Database;
using PragmaSQL.Scripts;

namespace PragmaSQL
{
  public class PragmaSqlObjectGrouping
  {
    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
      set { _connParams = value; }
    }




    public void InstallObjectGroupingSupport()
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }
      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = global::PragmaSQL.Scripts.Properties.Resources.Script_ObjectGroupingSupport;
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        conn.Open();
        cmd.ExecuteNonQuery();
      }
    }

    public IList<ObjectGroupingItemData> GetChildItemsOf(int? parentID)
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      IList<ObjectGroupingItemData> result = new List<ObjectGroupingItemData>();

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = global::PragmaSQL.Scripts.Properties.Resources.spPragmaSQL_ObjectGroup_List;
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.Parameters.Add("@ParentID", System.Data.SqlDbType.Int);
        cmd.Parameters["@ParentID"].Value = parentID;
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        string name = String.Empty;
        int? type = null;
        int? id = null;
        string tableName = String.Empty;
        string createdBy = String.Empty;

        ObjectGroupingItemData data = null;
        try
        {
          while (reader.Read())
          {
            name = reader["Name"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Name"];
            type = reader["ObjType"].GetType() == typeof(DBNull) ? null : (int?)reader["ObjType"];
            id = reader["ObjectID"].GetType() == typeof(DBNull) ? null : (int?)reader["ObjectID"];
            tableName = reader["TableName"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["TableName"];
            createdBy = reader["CreatedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["CreatedBy"];

            data = ObjectGroupingItemDataFactory.Create(name, type, id, tableName, parentID, createdBy);
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


    public void AddItem(ObjectGroupingItemData data, string desc, string descFormat)
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = global::PragmaSQL.Scripts.Properties.Resources.spPragmaSQL_ObjectGroup_Insert;
        SqlCommand cmd = new SqlCommand(cmdText, conn);

        cmd.Parameters.AddWithValue("@ParentID", data.ParentID);
        cmd.Parameters.AddWithValue("@Name", data.Name);
        cmd.Parameters.AddWithValue("@TableName", data.TableName);
        cmd.Parameters.AddWithValue("@Description", desc);
        cmd.Parameters.AddWithValue("@DescriptionFormat", descFormat);
        cmd.Parameters.AddWithValue("@ObjType", data.Type);
        cmd.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
        SqlParameter param = cmd.Parameters.Add("@ObjectID", System.Data.SqlDbType.Int);
        param.Direction = System.Data.ParameterDirection.Output;
        conn.Open();
        cmd.ExecuteNonQuery();
        data.ID = (int?)param.Value;
      }
    }

    public void AddItem(ObjectGroupingItemData data)
    {
      AddItem(data, String.Empty, String.Empty);
    }


    public void DeleteItem(int? id)
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = global::PragmaSQL.Scripts.Properties.Resources.spPragmaSQL_ObjectGroup_Delete;
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.Parameters.AddWithValue("@ObjectID", id);
        cmd.ExecuteNonQuery();
      }
    }

    public void DeleteItem(ObjectGroupingItemData data)
    {
      if (data == null)
      {
        return;
      }
      DeleteItem(data.ID);
    }
  }
}
