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
  public class AfterContextMenuActionExecutedEventArgs : EventArgs
  {
    public ObjectExplorerNode SelectedNode = null;
    public string ActionPath = String.Empty;
    public ObjectExplorerAction Action = ObjectExplorerAction.None;
  }
  
  public class BeforeContextMenuActionExecutedEventArgs : AfterContextMenuActionExecutedEventArgs{ }

  public delegate void AfterConnectedDelegate(string serverName,string connectionString);
  public delegate void AfterDisconnectedDelegate( string serverName);
  public delegate void AfterContextMenuActionExecutedDelegate(object sender, AfterContextMenuActionExecutedEventArgs args);
  public delegate void BeforeContextMenuActionExecutedDelegate( object sender, BeforeContextMenuActionExecutedEventArgs args );
}
