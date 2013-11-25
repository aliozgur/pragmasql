using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  [Serializable]
  public class SharedScriptsItemData
  {
    public bool Populated = false;

    public int? Type = null;
    public string Name = String.Empty;

    public int? ID = null;
    public int? ParentID = null;
    public string CreatedBy = String.Empty;
    public DateTime? CreatedOn = null;
    public string UpdatedBy = String.Empty;
    public DateTime? UpdatedOn = null;

    public string Script = String.Empty;
    public string ServerName = String.Empty;
    public string DatabaseName = String.Empty;
    public string HelpText = String.Empty;
    public bool IsDeleted = false;

    public SharedScriptsItemData(int? type)
    {
      Type = type;
    }

    public SharedScriptsItemData CreateCopy()
    {
      SharedScriptsItemData result = new SharedScriptsItemData(this.Type);
      result.Name = this.Name;
      result.Populated = this.Populated;
      result.ID = this.ID;
      result.ParentID = this.ParentID;
      result.CreatedBy = this.CreatedBy;
      result.CreatedOn = this.CreatedOn;
      result.UpdatedBy = this.UpdatedBy;
      result.UpdatedOn = this.UpdatedOn;
      result.Script = this.Script;
      result.ServerName = this.ServerName;
      result.DatabaseName = this.DatabaseName;
      result.HelpText = this.HelpText;
      return result;
    }

    public string ToolTipText
    {
      get
      {
        string createdOn  = String.Empty;
        if(CreatedOn.HasValue)
        {
          createdOn = CreatedOn.Value.ToString();
        }
        else
        {
          createdOn = "?";
        }

        return "{ Name: " + Name + " } { Created By: " + CreatedBy + " } { Created On: " + createdOn + " } { Updated By: " + UpdatedBy + "} ";
      }
    }
  }

  public class SharedScriptsItemDataFactory
  {
    public static SharedScriptsItemData Create(string name, int? type, int? id, int? parentID, string createdBy)
    {
      SharedScriptsItemData result = new SharedScriptsItemData(type);
      result.Name = name;
      result.ID = id;
      result.ParentID = parentID;
      result.CreatedBy = createdBy;
      result.CreatedOn = DateTime.Now;
      return result;
    }

    public static SharedScriptsItemData GetNodeData(object tag)
    {
      if (tag != null && (tag is SharedScriptsItemData))
      {
        return (SharedScriptsItemData)tag;
      }
      else
      {
        return null;
      }
    }

    public static SharedScriptsItemData GetNodeData(TreeNode node)
    {
      if (node == null || node.Tag == null)
      {
        return null;
      }

      return GetNodeData(node.Tag);
    }

  }
}
