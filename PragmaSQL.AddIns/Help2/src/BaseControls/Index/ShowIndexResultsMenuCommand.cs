/********************************************************************
  Class ShowIndexResultsMenuCommand
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
  public class ShowIndexResultsMenuCommand : AbstractMenuCommand
  {
    public override void Run( )
    {
      HtmlHelp2IndexResultsPad pad = HtmlHelp2IndexResultsPad.Current;
      if (pad == null)
      {
        HtmlHelp2IndexResultsPad.Current = new HtmlHelp2IndexResultsPad();
        pad = HtmlHelp2IndexResultsPad.Current;
        HostServicesSingleton.HostServices.ShowForm(pad, AddInDockState.DockRightAutoHide);
      }
      else
      {
        HostServicesSingleton.HostServices.ShowForm(pad);
      }
    }
  }
}
