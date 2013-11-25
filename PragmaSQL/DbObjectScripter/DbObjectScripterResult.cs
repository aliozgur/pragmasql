/********************************************************************
  Class DbObjectScripterResult
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
  public class DbObjectScripterResult
  {
    public IList<Exception> errors = null;
    public string script = String.Empty;
  }
}
