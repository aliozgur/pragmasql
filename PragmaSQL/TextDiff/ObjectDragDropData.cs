/********************************************************************
  Class ObjectDragDropData
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public class ObjectDragDropData
  {
    public string Script = String.Empty;
    public string TypeAbb = String.Empty;
    public int Type = DBObjectType.None;
    public int ID = DBObjectType.None;
  }
}
