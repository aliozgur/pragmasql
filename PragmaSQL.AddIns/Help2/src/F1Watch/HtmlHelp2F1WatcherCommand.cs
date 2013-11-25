/********************************************************************
  Class HtmlHelp2F1Watcher
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace HtmlHelp2
{
  public class HtmlHelp2F1WatcherCommand:AbstractCommand
  {
    public override void Run( )
    {
      HtmlHelp2F1Watcher.Initialize();
    }
  }
}
