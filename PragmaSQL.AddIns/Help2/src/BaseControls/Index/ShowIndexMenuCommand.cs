/********************************************************************
  Class ShowIndexMenuCommand
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
  public class ShowIndexMenuCommand : AbstractMenuCommand
  {
    public override void Run( )
    {
      HtmlHelp2IndexPad pad  = InitializeIndexPad();
      HostServicesSingleton.HostServices.ShowForm(pad, AddInDockState.DockRightAutoHide);
    }

    public HtmlHelp2IndexPad InitializeIndexPad( )
    {
      HtmlHelp2IndexPad pad = HtmlHelp2IndexPad.Current;
      if (pad == null)
      {
        HtmlHelp2IndexPad.Current = new HtmlHelp2IndexPad();
        pad = HtmlHelp2IndexPad.Current;
      }

      return pad;
    }
  }
}
