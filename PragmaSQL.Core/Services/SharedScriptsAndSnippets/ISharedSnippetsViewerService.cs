using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;


namespace PragmaSQL.Core
{
  public interface ISharedSnippetsViewerService
  {
    SharedSnippetItemData SelectedItem { get; }
    IList<SharedSnippetItemData> SelectedItems { get; }
    IList<TreeNode> SelectedNodes { get; }

    void ExecuteCommand( SharedSnippetsViewerCommand action );
    bool CanPerformCommand( SharedSnippetsViewerCommand action );
    void ShowSharedSnippetsViewer( );

    event BeforeSharedSnippetsViewerActionDelegate BeforeSharedSnippetsViewerAction;
    event AfterSharedSnippetsViewerActionDelegate AfterSharedSnippetsViewerAction;
    event EventHandler AfterSharedSnippetsViewerClosed;
    event EventHandler AfterSelectedNodesChanged;
  }
}
