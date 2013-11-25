/********************************************************************
  Class ProjectNodeData
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class ProjectNodeData
  {
    public ProjectItem Item = null;
    public Project Project = null;
    public bool IsPopulated = false;
    public ProjectItemType ItemType = ProjectItemType.Unknown;

    public override string ToString( )
    {
      StringBuilder sb = new StringBuilder();

      sb.AppendLine("ItemType:" + ItemType.ToString());
      sb.AppendLine("IsPopulated:" + IsPopulated.ToString());
      if (Item != null)
      {
        sb.AppendLine("Item:");
        sb.AppendLine(Item.ToString());
      }
      
      if( Project != null)
      {
        sb.AppendLine("Project:");
        sb.AppendLine(Project.ToString());
      }

      return sb.ToString();
    }
  }
  
  public static class ProjectNodeDataFactory
  {
    public static ProjectNodeData Create(Project prj)
    {
      ProjectNodeData result = new ProjectNodeData();
      result.Project = prj;
      result.ItemType = ProjectItemType.Project;
      return result;
    }

    public static ProjectNodeData Create(Project prj, ProjectItem item)
    {
      ProjectNodeData result = new ProjectNodeData();
      result.Project = prj;
      result.Item = item;
      result.ItemType = item.ItemType;
      return result;    
    }
  }
}
