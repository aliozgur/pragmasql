/********************************************************************
  Class DbObjectScripterArgs
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Scripting.Smo;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public enum ScriptDestination
  {
    Window,
    File,
    Folder
  }
  public class DbObjectScripterArgs
  {
    public ScriptDestination destination = ScriptDestination.Window;
    public string path = String.Empty;
    public ScriptObjectTypesList objectTypes = new ScriptObjectTypesList();
    public ConnectionParams cp = null;

    public SearchType searchType = SearchType.PlainText;
    public string searchText = String.Empty;
  }
}
