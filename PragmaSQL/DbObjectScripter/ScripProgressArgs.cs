/********************************************************************
  Class ScripProgressArgs
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL
{
  public class ScripProgressArgs
  {
    public string objectType = String.Empty;
    public  string objectName = String.Empty;
    public int total = 0;
    public int current = 0;
  }
}
