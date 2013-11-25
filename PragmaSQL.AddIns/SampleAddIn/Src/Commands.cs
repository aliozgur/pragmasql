/********************************************************************
  Class Commands
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.Core;

namespace PragmaSQL.AddIns.SampleAddIn
{
  public class ShowCommand: AbstractCommand
  {
    public override void Run( )
    {
      SampleAddIn.ShowForm();
    }
  }
  
  public class ShowNewFormCommand : AbstractCommand
  {
    public override void Run( )
    {
      SampleAddIn.ShowNewFormEveryTime();
    }
  }
}
