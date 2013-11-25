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
using System.ComponentModel;

namespace PragmaSQL.Core
{
  public class ProjectExplorerEventArgs:EventArgs
  {
    public IList<ProjectNodeData> Items;
    public ProjectExplorerCommand Action = ProjectExplorerCommand.None;
  }

  public delegate void BeforeProjectExplorerActionDelegate(object sender,ProjectExplorerEventArgs args);
  public delegate void AfterProjectExplorerActionDelegate( object sender, ProjectExplorerEventArgs args );

}
