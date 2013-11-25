/********************************************************************
  Class      : ScriptData
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public struct ScriptData
  {
    public string ScriptText;
    public ScriptRunType ScriptRunType;
    public int SelStartLineNo;
		public bool IsMultiDbOperation;
		public string Server;
		public string Database;
		public bool IsObjectHelpScript;
	}
}
