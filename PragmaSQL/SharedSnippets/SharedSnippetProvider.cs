/********************************************************************
  Class      : SharedSnippetProvider
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
  public static class SharedSnippetProvider
  {
    private static DataTable _tblSnippets = null;
    public static DataTable SnippetsData
    {
      get { return SharedSnippetProvider._tblSnippets; }
    }

    static SharedSnippetProvider( )
    {
      InitializeDataTable();
      InitialLoadFromDefaultShare();
    }

    private static void InitializeDataTable( )
    {
      _tblSnippets = new DataTable();
      _tblSnippets.TableName = "Shared Snippets";
      DataColumn col = null;

      col = new DataColumn();
      col.ColumnName = "id";
      col.DataType = System.Type.GetType("System.Int32");
      _tblSnippets.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "Type";
      col.DataType = System.Type.GetType("System.String");
      _tblSnippets.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "DisplayValue";
      col.DataType = System.Type.GetType("System.String");
      _tblSnippets.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "Value";
      col.DataType = System.Type.GetType("System.String");
      _tblSnippets.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "DisplayName";
      col.DataType = System.Type.GetType("System.String");
      _tblSnippets.Columns.Add(col);

      // Make the ID column the primary key column.
      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = _tblSnippets.Columns["id"];
      _tblSnippets.PrimaryKey = PrimaryKeyColumns;
    }

    public static void InitialLoadFromDefaultShare( )
    {

      if (_tblSnippets == null)
      {
        throw new Exception("Snippets table not initialized correctly!");
      }

      _tblSnippets.Rows.Clear();
			if (ConfigHelper.Current == null || ConfigHelper.Current.PragmaSqlDbConn == null || !ConfigHelper.Current.SharedSnippetsEnabled())
      {
        return;
      }

      DataRow row;
      string snippet = String.Empty;
      using (SqlConnection conn = new SqlConnection(ConfigHelper.Current.PragmaSqlDbConn.ConnectionString))
      {
        string cmdText = "exec spPragmaSQL_CodeSnippet_ListAll";
        SqlCommand cmd = new SqlCommand(cmdText, conn);
        cmd.CommandTimeout = 0;
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {

          row = _tblSnippets.NewRow();
          row["id"] = reader["SnippetID"];
          row["Type"] = "ccListItem";
          row["DisplayName"] = reader["Name"];
          row["Value"] = reader["Snippet"];

          snippet = reader["Snippet"] as string;
          if (!String.IsNullOrEmpty(snippet))
          {
            snippet = snippet.Replace("\n", " ");
            snippet = snippet.Replace("\r", " ");
            snippet = snippet.Replace("\t", " ");
          }
          row["DisplayValue"] = snippet;
          _tblSnippets.Rows.Add(row);
        }
        reader.Close();
      }
    }

    public static void SynchronizationActionAdd( SharedSnippetItemData data )
    {
			if (_tblSnippets == null || ConfigHelper.Current == null || ConfigHelper.Current.PragmaSqlDbConn == null || !ConfigHelper.Current.SharedSnippetsEnabled())
      {
        return;
      }

      if (data == null)
      {
        return;
      }
      
      DataRow row = _tblSnippets.NewRow();
      row["id"] = data.ID;
      row["Type"] = "ccListItem";
      row["DisplayName"] = data.Name;
      row["Value"] = data.Snippet;

      string snippet = data.Snippet;
      if (!String.IsNullOrEmpty(snippet))
      {
        snippet = snippet.Replace("\n", " ");
        snippet = snippet.Replace("\r", " ");
        snippet = snippet.Replace("\t", " ");

      }
      row["DisplayValue"] = snippet;
      _tblSnippets.Rows.Add(row);
    }
    
    public static void SynchronizationActionDelete( int? id )
    {
			if (_tblSnippets == null || ConfigHelper.Current == null || ConfigHelper.Current.PragmaSqlDbConn == null || !ConfigHelper.Current.SharedSnippetsEnabled())
      {
        return;
      }

      if(!id.HasValue)
      {
        return;
      }

      DataRow row = _tblSnippets.Rows.Find(id.Value);
      if(row != null)
      {
        _tblSnippets.Rows.Remove(row);
      }
    }

    public static void SynchronizationActionUpdate( SharedSnippetItemData data )
    {
			if (_tblSnippets == null || ConfigHelper.Current == null || ConfigHelper.Current.PragmaSqlDbConn == null || !ConfigHelper.Current.SharedSnippetsEnabled())
      {
        return;
      }

      if (data == null || !data.ID.HasValue)
      {
        return;
      }
      
      DataRow row = _tblSnippets.Rows.Find(data.ID.Value);
      if(row == null)
      {
        return;
      }
        
      row["DisplayName"] = data.Name;
      row["Value"] = data.Snippet;

      string snippet = data.Snippet;
      if (!String.IsNullOrEmpty(snippet))
      {
        snippet = snippet.Replace("\n", " ");
        snippet = snippet.Replace("\r", " ");
        snippet = snippet.Replace("\t", " ");
      }
      row["DisplayValue"] = snippet;
    }
  }
}
