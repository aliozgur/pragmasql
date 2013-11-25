using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public interface ISharedScriptsViewerService
  {
    SharedScriptsItemData SelectedItem { get; }
    IList<SharedScriptsItemData> SelectedItems { get; }
    IList<TreeNode> SelectedNodes { get; }

    void ExecuteCommand( SharedScriptsViewerCommand action );
    bool CanPerformCommand( SharedScriptsViewerCommand action );
    void ShowSharedScriptsViewer( );

    event BeforeSharedScriptsViewerActionDelegate BeforeSharedScriptsViewerAction;
    event AfterSharedScriptsViewerActionDelegate AfterSharedScriptsViewerAction;
    event EventHandler AfterSharedScriptsViewerClosed;
    event EventHandler AfterSelectedNodesChanged;

  }
}
