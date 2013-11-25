using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public class NodeData
  {
    public ConnectionParams ConnParams = null;
    public int Type = -1;
    public string Name = String.Empty;
    public bool Populated = false;
    public string DBName = String.Empty;
    public string ServerName = String.Empty;
    public long ID = -1;
    public long DbId = -1;
    public string Owner = String.Empty;
    public bool HasParamsOrCols = false;
    public int ParentType = -1;
    public Filter? Filter = null;
    public string ParentName = String.Empty;
    public string FullName = String.Empty;
    public short CompatibilityLevel;
    public NodeData(int type)
    {
      Type = type;
    }

    public string QualifiedFullName
    {
      get { return String.Format("[{0}].[{1}]",Owner,Name); }
    }
  }


  public static class NodeDataFactory
  {
    public static NodeData Create(ConnectionParams connParams, string name,int type, string dbName, long dbid, string owner)
    {
      if (connParams == null)
      {
        throw new NullReferenceException("ConnParams parameter can not be null");
      }

      NodeData result = new NodeData(type);
      result.ConnParams = connParams;
      result.DBName = dbName;
      result.Name = name;
      result.ServerName = connParams.Server;
      result.DbId = dbid;
      result.Owner = owner;
      return result;
    }

    public static NodeData GetNodeData(object tag)
    {
      if (tag != null && tag is NodeData)
      {
        return (NodeData)tag;
      }
      else
      {
        return null;
      }
    }

    public static NodeData GetNodeData(TreeNode node)
    {
      if (node == null || node.Tag == null)
      {
        return null;
      }

      return GetNodeData(node.Tag);
    }
  }
}
