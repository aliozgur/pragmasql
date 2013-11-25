/********************************************************************
  Class      : SharedScriptItemData
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
  [Serializable]
  public class SharedSnippetItemData
  {
    public bool Populated = false;

    public int? Type = null;
    public string Name = String.Empty;
    public string Description = String.Empty;

    public int? ID = null;
    public int? ParentID = null;
    public string CreatedBy = String.Empty;
    public DateTime? CreatedOn = null;
    public string UpdatedBy = String.Empty;

    public string Snippet = String.Empty;
    public bool IsDeleted = false;

    public SharedSnippetItemData( int? type )
    {
      Type = type;
    }

    public SharedSnippetItemData CreateCopy( )
    {
      SharedSnippetItemData result = new SharedSnippetItemData(this.Type);
      result.Name = this.Name;
      result.Populated = this.Populated;
      result.ID = this.ID;
      result.ParentID = this.ParentID;
      result.CreatedBy = this.CreatedBy;
      result.CreatedOn = this.CreatedOn;
      result.UpdatedBy = this.UpdatedBy;
      result.Snippet = this.Snippet;
      result.Description = this.Description;
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

  public class SharedSnippetItemDataFactory
  {
    public static SharedSnippetItemData Create( string name, int? type, int? id, int? parentID, string createdBy )
    {
      SharedSnippetItemData result = new SharedSnippetItemData(type);
      result.Name = name;
      result.ID = id;
      result.ParentID = parentID;
      result.CreatedBy = createdBy;
      result.CreatedOn = DateTime.Now;
      return result;
    }

    public static SharedSnippetItemData GetNodeData( object tag )
    {
      if (tag != null && (tag is SharedSnippetItemData))
      {
        return (SharedSnippetItemData)tag;
      }
      else
      {
        return null;
      }
    }

    public static SharedSnippetItemData GetNodeData( TreeNode node )
    {
      if (node == null || node.Tag == null)
      {
        return null;
      }

      return GetNodeData(node.Tag);
    }

  }//Class end

}// Namespace end
