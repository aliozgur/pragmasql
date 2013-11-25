using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public interface IObjectExplorerService
  {
    ObjectExplorerNode SelNode { get; }
    IList<ObjectExplorerNode> SelNodes { get; }
    IList<TreeNode> SelectedNodes { get; }
    short DbCompatibilityLevel { get; }
    void ExecuteCommand( ObjectExplorerCommand cmd );
    
    void ChangeObjectName(TreeNode node, string name);
    void ChangeObjectId(TreeNode node, long? id);
    void ChangeObjectID(string serverName, string databaseName, long oldId, long newId);

    /// <summary>
    /// Refreshes specified node data
    /// </summary>
    /// <param name="node"></param>
    /// <param name="forceRefresh"></param>
    void LoadNodeData( TreeNode node, bool forceRefresh );

    event AfterConnectedDelegate AfterConnected;
    event AfterDisconnectedDelegate AfterDisconnected;
    event AfterContextMenuActionExecutedDelegate AfterContextMenuActionExecuted;
    event BeforeContextMenuActionExecutedDelegate BeforeContextMenuActionExecuted;

    event EventHandler AfterObjectExplorerClosed;
    event EventHandler AfterSelectedNodesChanged;
    
    void ShowObjectExplorer( );
  }
}
