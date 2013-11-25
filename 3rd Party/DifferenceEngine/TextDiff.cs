/********************************************************************
  Class TextDiff
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DifferenceEngine
{
  public class TextDiff : IDiffList
	{
		private ArrayList _lines;

		public TextDiff(string inText)
		{
			_lines = new ArrayList();
      
	    string[] inTextLines = inText.Split('\n');
      for(int i= 0; i < inTextLines.Length;i++)
      {
        _lines.Add(new DiffTextLine(inTextLines[i]));
      }
    }

		#region IDiffList Members

		public int Count()
		{
			return _lines.Count;
		}

		public IComparable GetByIndex(int index)
		{
			return (DiffTextLine)_lines[index];
		}

		#endregion
  }
}
