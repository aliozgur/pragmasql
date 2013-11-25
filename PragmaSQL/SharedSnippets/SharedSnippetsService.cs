/********************************************************************
  Class      : SharedScriptsFacade
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;


using System.Data;
using System.Data.SqlClient;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public class SharedSnippetsService:ISharedSnippetsService
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

    private bool _canSynchronize = true;
    public bool CanSynchronize
    {
      get { return _canSynchronize; }
    }

    #endregion //Fields and Properties

    #region Constructor

    public SharedSnippetsService( )
    {

    }

    public SharedSnippetsService( ConnectionParams connParams )
      :this()
    {
      ConnParams = connParams;
    }

    public SharedSnippetsService( bool canSynchronize )
      :this()
    {
      _canSynchronize = canSynchronize;
    }
    public SharedSnippetsService( ConnectionParams connParams, bool canSynchronize )
      : this(connParams)
    {
      _canSynchronize = canSynchronize;
    }

    #endregion //Constructor

    #region Methods

    private void AddParam( SqlCommand cmd, string paramName, SqlDbType type, object value )
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

    public IList<SharedSnippetItemData> GetChildren( int? parentID )
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      IList<SharedSnippetItemData> result = new List<SharedSnippetItemData>();

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = "exec spPragmaSQL_CodeSnippet_List @ParentID";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd, "@ParentID", System.Data.SqlDbType.Int, parentID);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        string name = String.Empty;
        string desc = String.Empty;
        int? type = null;
        int? id = null;
        DateTime? createdOn = null;
        string createdBy = String.Empty;
        string updatedBy = String.Empty;
        string snippet = String.Empty;
        bool isDeleted = false;

        SharedSnippetItemData data = null;
        try
        {
          while (reader.Read())
          {
            name = reader["Name"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Name"];
            desc = reader["Description"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Description"];
            type = reader["Type"].GetType() == typeof(DBNull) ? null : (int?)reader["Type"];
            id = reader["SnippetID"].GetType() == typeof(DBNull) ? null : (int?)reader["SnippetID"];
            createdOn = reader["CreatedOn"].GetType() == typeof(DBNull) ? null : (DateTime?)reader["CreatedOn"];
            createdBy = reader["CreatedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["CreatedBy"];
            updatedBy = reader["UpdatedBy"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["UpdatedBy"];

            snippet = reader["Snippet"].GetType() == typeof(DBNull) ? String.Empty : (string)reader["Snippet"];
            isDeleted = reader["IsDeleted"].GetType() == typeof(DBNull) ? false : (bool)reader["IsDeleted"];

            data = SharedSnippetItemDataFactory.Create(name, type, id, parentID, createdBy);
            data.Snippet = snippet;
            data.IsDeleted = isDeleted;
            data.CreatedOn = createdOn;
            data.UpdatedBy = updatedBy;
            data.Description = desc;
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

    public void AddItem( SharedSnippetItemData data )
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
        string cmdText = "exec spPragmaSQL_CodeSnippet_Insert @Type, @ParentID, @Name, @Snippet, @Description, @CreatedBy, @SnippetID out";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;

        AddParam(cmd, "@Type", SqlDbType.Int, data.Type);
        AddParam(cmd, "@ParentID", SqlDbType.Int, data.ParentID);
        AddParam(cmd, "@Name", SqlDbType.VarChar, data.Name);
        AddParam(cmd, "@Snippet", SqlDbType.Text, data.Snippet);
        AddParam(cmd, "@Description", SqlDbType.Text, data.Description);
        AddParam(cmd, "@CreatedBy", SqlDbType.VarChar, _connParams.CurrentUsername);
        SqlParameter param = cmd.Parameters.Add("@SnippetID", System.Data.SqlDbType.Int);
        param.Direction = System.Data.ParameterDirection.Output;
        conn.Open();
        cmd.ExecuteNonQuery();
        data.ID = (int?)param.Value;

        if (_canSynchronize)
        {
          SharedSnippetProvider.SynchronizationActionAdd(data);
        }
      }
    }

    public void DeleteItem( int? id )
    {
      if (ConnParams == null)
      {
        throw new NullPropertyException("Connection parameters is null!");
      }

      using (SqlConnection conn = new SqlConnection(_connParams.ConnectionString))
      {
        string cmdText = "exec spPragmaSQL_CodeSnippet_Delete @SnippetID";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        AddParam(cmd, "@SnippetID", SqlDbType.Int, id);
        conn.Open();
        cmd.ExecuteNonQuery();
        if (_canSynchronize)
        {
          SharedSnippetProvider.SynchronizationActionDelete(id);
        }
      }
    }

    public void UpdateItem( SharedSnippetItemData data )
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
        string cmdText = "exec spPragmaSQL_CodeSnippet_Update @SnippetID, @ParentID, @Name, @Snippet, @Description, @UpdatedBy";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;

        AddParam(cmd, "@SnippetID", SqlDbType.Int, data.ID);
        AddParam(cmd, "@ParentID", SqlDbType.Int, data.ParentID);
        AddParam(cmd, "@Name", SqlDbType.VarChar, data.Name);
        AddParam(cmd, "@Snippet", SqlDbType.Text, data.Snippet);
        AddParam(cmd, "@Description", SqlDbType.Text, data.Description);
        AddParam(cmd, "@UpdatedBy", SqlDbType.VarChar, _connParams.CurrentUsername);
        conn.Open();
        cmd.ExecuteNonQuery();
        if (_canSynchronize)
        {
          SharedSnippetProvider.SynchronizationActionUpdate(data);
        }
      }
    }
    #endregion //Methods

  }// Class end
} // Namespace end
