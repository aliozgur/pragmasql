/********************************************************************
  Class DiffTextLine
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace DifferenceEngine
{
  public class DiffTextLine : IComparable
  {
    public string Line;
    public int _hash;

    public DiffTextLine( string str )
    {
      Line = str.Replace("\t", "    ");
      _hash = str.GetHashCode();
    }
    #region IComparable Members

    public int CompareTo( object obj )
    {
      return _hash.CompareTo(((DiffTextLine)obj)._hash);
      /*
      string inVal = obj as string;
		  return Line.ToLowerInvariant().CompareTo(inVal.ToLowerInvariant());
      */
    }

    #endregion
  }

}
