/********************************************************************
  Class      : ResManager
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public static class ResManager
  {
    public static string GetDBScript(string scriptName)
    {
      return  global::PragmaSQL.Core.Resources.DBScripts.ResourceManager.GetString(scriptName);
    }

    public static string GetRegularExpression(string regexName)
    {
      return  global::PragmaSQL.Core.Resources.RegularExpressions.ResourceManager.GetString(regexName);
    }
  }
}
