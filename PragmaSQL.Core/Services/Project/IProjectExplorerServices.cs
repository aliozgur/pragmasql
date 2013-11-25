/********************************************************************
  Class IProjectServices
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public interface IProjectExplorerServices
  {
    ProjectNodeData SelectedItem { get; }
    IList<ProjectNodeData> SelectedItems { get; }
    IList<TreeNode> SelectedNodes { get; }
    
    Project CurrentProject { get; }

    void ExecuteCommand( ProjectExplorerCommand action );
    bool CanPerformCommand( ProjectExplorerCommand action );
    void ShowProjectExplorer( );
		void OpenProject(string fileName);

    event BeforeProjectExplorerActionDelegate BeforeProjectExplorerAction;
    event AfterProjectExplorerActionDelegate AfterProjectExplorerAction;
    event EventHandler AfterProjectExplorerClosed;
    event EventHandler AfterSelectedNodesChanged;

  }
}
