using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public class SharedScriptsService:ISharedScriptsService
  {

    #region Fields and Properties

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get
      {
        return _connParams;
      }
      set
      {
        if (value == null)
        {
          _connParams = value;
          return;
        }
        _connParams = value.CreateCopy();
      }
    }

    #endregion //Fields and Properties

    #region Constructor
    public SharedScriptsService( )
    {
    
    }
    public SharedScriptsService( ConnectionParams connParams )
    {
      ConnParams = connParams;
    }

    #endregion //Constructor
    
    #region Methods
    private void AddParam(SqlCommand cmd, string paramName, SqlDbType type, object value)
    {
      cmd.Parameters.Add(paramName, type);
      if (value != null)
      {
        cmd.Parameters[paramName].Value = value;
      }
      else
      {
        cmd.Parameters[paramName].Value = DBNull.Value;
      }
    }

    public IList<SharedScriptsItemData> GetChildren(int? parentID)
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      IList<SharedScriptsItemData> result = new List<SharedScriptsItemData>();

      using (SqlConnection conn = _connParams.CreateSqlConnection(true,false) )
      {
        string cmdText = "exec spPragmaSQL_Script_GetChildren @ParentID";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd, "@ParentID", System.Data.SqlDbType.Int, parentID);

        SqlDataReader reader = cmd.ExecuteReader();

        string name = String.Empty;
        string serverName = String.Empty;
        string databaseName = String.Empty;
        string helpText = String.Empty;
        int? type = null;
        int? id = null;
        DateTime? createdOn = null;
        string createdBy = String.Empty;
        string updatedBy = String.Empty;
        DateTime? updatedOn = null;
        string script = String.Empty;
        bool isDeleted = false;

        SharedScriptsItemData data = null;
        try
        {
          while (reader.Read())
          {
            name = reader["Name"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Name"];
            serverName = reader["ServerName"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["ServerName"];
            databaseName = reader["DatabaseName"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["DatabaseName"];
            type = reader["Type"].GetType() == typeof(DBNull) ? null : (int?)reader["Type"];
            id = reader["ScriptID"].GetType() == typeof(DBNull) ? null : (int?)reader["ScriptID"];
            createdOn = reader["CreatedOn"].GetType() == typeof(DBNull) ? null : (DateTime?)reader["CreatedOn"];
            createdBy = reader["CreatedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["CreatedBy"];
            updatedOn = reader["ModifiedOn"].GetType() == typeof(DBNull) ? null : (DateTime?)reader["ModifiedOn"];
            updatedBy = reader["ModifiedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["ModifiedBy"];

            script = reader["Script"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Script"];
            isDeleted = reader["IsDeleted"].GetType() == typeof(DBNull) ? false : (bool)reader["IsDeleted"];
            helpText = reader["HelpText"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["HelpText"];

            data = SharedScriptsItemDataFactory.Create(name, type, id, parentID, createdBy);
            data.Script = script;
            data.IsDeleted = isDeleted;
            data.CreatedOn = createdOn;
            data.UpdatedBy = updatedBy;
            data.UpdatedOn = updatedOn;
            data.HelpText = helpText;

            data.ServerName = serverName;
            data.DatabaseName = databaseName;
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

    public void AddItem(SharedScriptsItemData data)
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
        string cmdText = "exec spPragmaSQL_Script_Insert @ParentID , @Name , @Script , @CreatedBy , @Type , @ServerName, @DatabaseName , @HelpText , @UID, @ScriptID out";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        data.CreatedBy = _connParams.CurrentUsername;

        AddParam(cmd, "@ParentID", SqlDbType.Int, data.ParentID);
        AddParam(cmd, "@Name", SqlDbType.VarChar, data.Name);
        AddParam(cmd, "@Script", SqlDbType.Text, data.Script);
        AddParam(cmd, "@CreatedBy", SqlDbType.VarChar, data.CreatedBy);
        AddParam(cmd, "@Type", SqlDbType.Int, data.Type);
        AddParam(cmd, "@ServerName", SqlDbType.VarChar, data.ServerName);
        AddParam(cmd, "@DatabaseName", SqlDbType.VarChar, data.DatabaseName);
        AddParam(cmd, "@HelpText", SqlDbType.Text, data.HelpText);
        AddParam(cmd, "@UID", SqlDbType.VarChar, null);
        SqlParameter param = cmd.Parameters.Add("@ScriptID", System.Data.SqlDbType.Int);
        param.Direction = System.Data.ParameterDirection.Output;
        conn.Open();
        cmd.ExecuteNonQuery();
        data.ID = (int?)param.Value;
      }
    }

    public void DeleteItem(int? id)
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = "exec spPragmaSQL_Script_Delete @ScriptID";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd, "@ScriptID", SqlDbType.Int, id);
        conn.Open();
        cmd.ExecuteNonQuery();
      }
    }

    public void UpdateItem(SharedScriptsItemData data)
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
        string cmdText = "exec spPragmaSQL_Script_Update @ScriptID, @ParentID, @Name, @Script, @ModifiedBy, @HelpText";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;

        AddParam(cmd, "@ScriptID", SqlDbType.Int, data.ID);
        AddParam(cmd, "@ParentID", SqlDbType.Int, data.ParentID);
        AddParam(cmd, "@Name", SqlDbType.VarChar, data.Name);
        AddParam(cmd, "@Script", SqlDbType.Text, data.Script);
        AddParam(cmd, "@ModifiedBy", SqlDbType.VarChar, _connParams.CurrentUsername);
        AddParam(cmd, "@HelpText", SqlDbType.Text, data.HelpText);
        conn.Open();
        cmd.ExecuteNonQuery();
      }
    }


    #endregion //Methods
  }
}
