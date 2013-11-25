/********************************************************************
  Class ShowSearchMenuCommand
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
  public class ShowSearchMenuCommand : AbstractMenuCommand
  {
    public override void Run( )
    {
      HtmlHelp2SearchPad pad = InitializeSearchPad();
      HostServicesSingleton.HostServices.ShowForm(pad, AddInDockState.DockRightAutoHide);
    }

    public HtmlHelp2SearchPad InitializeSearchPad( )
    {
      HtmlHelp2SearchPad pad = HtmlHelp2SearchPad.Current;
      if (pad == null)
      {
        HtmlHelp2SearchPad.Current = new HtmlHelp2SearchPad();
        pad = HtmlHelp2SearchPad.Current;
      }
      return pad;
    }
  }
}
