/********************************************************************
  Class ProjectNodeSorter
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public class ProjectNodeSorter : IComparer
  {
    public int Compare( Object x, Object y )
    {
      TreeNode nx = x as TreeNode;
      TreeNode ny = y as TreeNode;

      ProjectNodeData dx = GetNodeData(nx);
      ProjectNodeData dy = GetNodeData(ny);

      if (dx == null && dy == null)
      {
        return 0;
      }
      else if (dx != null && dy == null)
      {
        return -1;
      }
      else if (dx == null && dy != null)
      {
        return 1;
      }
      else if (dx.ItemType == ProjectItemType.Project && dy.ItemType != ProjectItemType.Project)
      {
        return -1;
      }
      else if (dx.ItemType != ProjectItemType.Project && dy.ItemType == ProjectItemType.Project)
      {
        return 1;
      }
      else if (dx.ItemType == ProjectItemType.Project && dy.ItemType == ProjectItemType.Project)
      {
        if (dx.Project != null && dy.Project != null)
        {
          return dx.Project.Name.ToLowerInvariant().CompareTo(dy.Project.Name.ToLowerInvariant());
        }
        else
        {
          return 0;
        }
      }
      else if (dx.Item != null && dy.Item == null)
      {
        return 1;
      }
      else if (dx.Item == null && dy.Item != null)
      {
        return 0;
      }
      else if (dx.Item == null && dy.Item == null)
      {
        return nx.Text.ToLowerInvariant().CompareTo(nx.Text.ToLowerInvariant());
      }
      else
      {
        return dx.Item.CompareTo(dy.Item);
      }
    }

    private ProjectNodeData GetNodeData( TreeNode node )
    {
      if (node == null || node.Tag == null)
      {
        return null;
      }
      return node.Tag as ProjectNodeData;
    }
  }
}
