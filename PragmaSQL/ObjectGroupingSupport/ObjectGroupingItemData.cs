/********************************************************************
  Class      : ObjectGroupingItemData
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL
{
  public class ObjectGroupingItemData
  {
    public int? Type = null;
    public string Name = String.Empty;
    public bool Populated = false;

    public int? ID = null;
    public int? ParentID = null;
    public string ParentObjectName = String.Empty;
    public string CreatedBy = String.Empty;
    public string UpdatedBy = String.Empty;

    public string HelpText = String.Empty;
    public string HelpTextFormat = "RTF";

    public ObjectGroupingItemData( int? type )
    {
      Type = type;
    }

    public ObjectGroupingItemData CreateCopy( )
    {
      ObjectGroupingItemData result = new ObjectGroupingItemData(this.Type);
      result.Name = this.Name;
      result.Populated = this.Populated;
      result.ID = this.ID;
      result.ParentID = this.ParentID;
      result.ParentObjectName = this.ParentObjectName;
      result.CreatedBy = this.CreatedBy;
      result.UpdatedBy = this.UpdatedBy;
      result.HelpText = this.HelpText;
      result.HelpTextFormat = this.HelpTextFormat;

      return result;
    }
  }

  public class ObjectGroupingItemDataFactory
  {
    public static ObjectGroupingItemData Create( string name, int? type, int? id, string parentObjectName, int? parentID, string createdBy )
    {
      ObjectGroupingItemData result = new ObjectGroupingItemData(type);
      result.Name = name;
      result.ID = id;
      result.ParentObjectName = parentObjectName;
      result.ParentID = parentID;
      result.CreatedBy = createdBy;
      return result;
    }

    public static ObjectGroupingItemData GetNodeData( object tag )
    {
      if (tag != null && (tag is ObjectGroupingItemData))
      {
        return (ObjectGroupingItemData)tag;
      }
      else
      {
        return null;
      }
    }

    public static ObjectGroupingItemData GetNodeData( TreeNode node )
    {
      if (node == null || node.Tag == null)
      {
        return null;
      }

      return GetNodeData(node.Tag);
    }
  }
}
