/********************************************************************
  Class      : Filter
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public enum FilterType
  {
    PlainText = 0,
    Wildcards = 1,
    RegularExpression = 2,
  }

  public struct Filter
  {
    public FilterType FilterType;
    public string FilterText;
    public bool ApplyToChildren;
    public bool UseOwn;

    public static Filter CreateFilter(FilterType filterType, string filterText, bool applyToChildren)
    {
      Filter result = new Filter();
      result.FilterType = filterType;
      result.FilterText = filterText;
      result.ApplyToChildren = applyToChildren ;
      result.UseOwn = false;
      return result;
    }
    
    public static Filter CreateFilter(string filterText)
    {
      Filter result = CreateFilter(FilterType.PlainText, filterText, false);
      return result;
    }

    public static Filter CreateFilter(string filterText, bool applyToChildern)
    {
      Filter result = CreateFilter(FilterType.PlainText, filterText, applyToChildern);
      return result;
    }

    public static Filter CreateFilter()
    {
      Filter result = CreateFilter(FilterType.PlainText, String.Empty, false);
      return result;
    }
  }
}
