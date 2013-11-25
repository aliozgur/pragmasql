/********************************************************************
  Class CaretPosition
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
namespace PragmaSQL.Core
{
  public struct CaretPosition
  {
    public int Line;
    public int Col;
   

    public Point ToPoint()
    {
      Point result = new Point();
      result.X = Col;
      result.Y = Line;
      return result;
    }

    public override string ToString( )
    {
      return "Line:" + Line.ToString() + ", Column:" + Col.ToString();
    }
  }
}
