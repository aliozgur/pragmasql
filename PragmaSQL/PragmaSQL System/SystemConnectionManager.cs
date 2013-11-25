using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class SystemConnectionManager
  {
    private static IList<SqlConnection> _connections = new List<SqlConnection>();


    public static SqlConnection GetConnection()
    {
      return GetConnection(false);
    }

    public static SqlConnection GetConnection(bool autoOpen)
    {
      ConfigurationContent config = ConfigHelper.Current;
      if (config == null)
      {
        throw new InvalidConfiguration("Application configuration not loaded properly!");
      }

      if (config.PragmaSqlDbConn == null)
      {
        throw new NullPropertyException("Configuration content does not contain PragmaSqlDbConn item!");
      }

      SqlConnection conn = new SqlConnection();
      conn.ConnectionString = config.PragmaSqlDbConn.ConnectionString;
      if (autoOpen)
      {
        conn.Open();
      }

      _connections.Add(conn);
      return conn;
    }

    public static void DismissConnection(SqlConnection conn)
    {
      if (_connections.Contains(conn))
      {
        _connections.Remove(conn);
      }
    }

    public static void DismissAllConnections(bool autoClose)
    {
      while (_connections.Count > 0)
      {
        if (autoClose)
        {
          if (_connections[0].State == System.Data.ConnectionState.Open)
          {
            _connections[0].Close();
          }
        }
        _connections.RemoveAt(0);
      }
    }

    public static void CloseAllConnections()
    {
      foreach (SqlConnection conn in _connections)
      {
        if (conn.State == System.Data.ConnectionState.Open)
        {
          conn.Close();
        }
      }

    }
  }
}
