/********************************************************************
  Class Delegates
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class SharedScriptsViewerEventArgs : EventArgs
  {
    public IList<SharedScriptsItemData> Items;
    public SharedScriptsViewerCommand Action = SharedScriptsViewerCommand.None;
  }

  public class SharedSnippetsViewerEventArgs : EventArgs
  {
    public IList<SharedSnippetItemData> Items;
    public SharedSnippetsViewerCommand Action = SharedSnippetsViewerCommand.None;
  }

  public delegate void BeforeSharedScriptsViewerActionDelegate( object sender, SharedScriptsViewerEventArgs args );
  public delegate void AfterSharedScriptsViewerActionDelegate( object sender, SharedScriptsViewerEventArgs args );

  public delegate void BeforeSharedSnippetsViewerActionDelegate( object sender, SharedSnippetsViewerEventArgs args );
  public delegate void AfterSharedSnippetsViewerActionDelegate( object sender, SharedSnippetsViewerEventArgs args );
}
