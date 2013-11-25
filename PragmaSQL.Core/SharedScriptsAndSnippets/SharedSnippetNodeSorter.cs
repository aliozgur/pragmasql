/********************************************************************
  Class SharedSnippetNodeSorter
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
    public class SharedSnippetNodeSorter : IComparer
    {
      #region IComparer Members

      public int Compare( object x, object y )
      {
        TreeNode nx = x as TreeNode;
        TreeNode ny = y as TreeNode;

        SharedSnippetItemData dx = SharedSnippetItemDataFactory.GetNodeData(nx);
        SharedSnippetItemData dy = SharedSnippetItemDataFactory.GetNodeData(ny);

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
        else if ((dx.Type ?? -1) == (dy.Type ?? -1))
        {
          return nx.Text.ToLowerInvariant().CompareTo(ny.Text.ToLowerInvariant());
        }
        else
        {

          return (dx.Type ?? -1).CompareTo(dy.Type ?? -1);
        }
      }

      #endregion
    }
}
