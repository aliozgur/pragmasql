/********************************************************************
  Class      : ListViewItemComparer
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
  /// <summary>
  /// Implements the manual sorting of items by column.
  /// </summary>
  public class ListViewItemComparer : IComparer
  {
    private int col;
    private SortOrder order;
    public ListViewItemComparer()
    {
      col = 0;
      order = SortOrder.Ascending;
    }
    public ListViewItemComparer(int column, SortOrder order)
    {
      col = column;
      this.order = order;
    }
    public int Compare(object x, object y)
    {
      int returnVal = -1;
      returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                              ((ListViewItem)y).SubItems[col].Text);
      // Determine whether the sort order is descending.
      if (order == SortOrder.Descending)
        // Invert the value returned by String.Compare.
        returnVal *= -1;
      return returnVal;
    }
  }
}
