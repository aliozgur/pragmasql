/********************************************************************
  Class ScriptObjectTypesList
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Scripting.Smo
{
  public class ScriptObjectTypesList:List<ScriptObjectTypes>
  {
    public ScriptObjectTypes this[ScriptObjectTypes type]
    {
      get
      {
        return this[this.IndexOf(type)];
      }
      set
      {
       if(!this.Contains(type)) 
       {
        this.Add(type);
       }
      }
    }
  }
}
