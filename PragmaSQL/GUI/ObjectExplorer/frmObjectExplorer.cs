using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;

using WeifenLuo.WinFormsUI;

using PragmaSQL.Database;
using PragmaSQL.Common;

namespace PragmaSQL.GUI
{
  public partial class frmObjectExplorer : DockContent
  {

    private IDictionary<string, SqlConnection> _connections = new Dictionary<string, SqlConnection>();
    private IDictionary<int, ContextMenuStrip> _contextMenus = new Dictionary<int, ContextMenuStrip>();

    private bool _tvInitializing = false;
    private bool _isInitialShow = true;

    public frmObjectExplorer()
    {
      InitializeComponent();
    }

    #region Initialization
    private bool IsSelectedObjectScriptable()
    {
      if (tv.SelectedNode == null)
      {
        return false;
      }

      NodeData data = NodeDataFactory.GetNodeData(tv.SelectedNode.Tag);
      if (data == null)
      {
        return false;
      }

      if (data.Type == DBObjectType.View
        || data.Type == DBObjectType.Trigger
        || data.Type == DBObjectType.StoredProc
        || data.Type == DBObjectType.TableValuedFunction
        || data.Type == DBObjectType.ScalarValuedFunction
        )
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion //Initialization

    #region Connection Management
    private void OpenConnection(ConnectionParams cp)
    {
      try
      {
        _tvInitializing = true;
        if (cp == null)
        {
          return;
        }
        if (_connections.Keys.Contains(cp.Name))
        {
          return;
        }

        SqlConnection conn = new SqlConnection(cp.ConnectionString);
        conn.Open();

        _connections.Add(cp.Name, conn);

        TreeNode serverNode = AddServerNode(cp);
        if (serverNode == null)
        {
          return;
        }

        PopulateDatabases(cp, serverNode);
        TreeNode usersNode = CreateUsersNode(cp, serverNode);
        PopulateUsers(cp, usersNode);

        serverNode.Expand();
        if (String.IsNullOrEmpty(cp.InitialCatalog))
        {
          tv.SelectedNode = serverNode;
        }
      }
      finally
      {
        _tvInitializing = false;
      }
    }

    private void CreateNewConnection(bool wantEmptyScript)
    {
      frmConnectionParams frm = new frmConnectionParams(false);
      frm.TopMost = true;
      DialogResult dlgRes = frm.ShowDialog();
      if (dlgRes != DialogResult.OK)
      {
        return;
      }

      OpenConnection(frm.GetCurrentConnectionSpec());
      if (wantEmptyScript)
      {
        CreateScriptEditor();
      }
    }

    private void CreateNewConnectionFromRepository(bool wantEmptyScript)
    {
      frmConnectionRepository frm = new frmConnectionRepository();
      frm.TopMost = true;
      DialogResult dlgRes = frm.ShowDialog();
      if (dlgRes != DialogResult.OK)
      {
        return;
      }

      OpenConnection(frm.SelectedDataSource);
      if (wantEmptyScript)
      {
        CreateScriptEditor();
      }
    }

    private void DisconnectFromServer(TreeNode serverNode)
    {

      if ((serverNode == null))
      {
        return;
      }

      NodeData data = NodeDataFactory.GetNodeData(serverNode.Tag);
      if (data == null || data.Type != DBObjectType.Server)
      {
        return;
      }

      ConnectionParams cp = data.ConnParams;

      if (!_connections.Keys.Contains(cp.Name))
      {
        return;
      }

      SqlConnection conn = _connections[cp.Name];
      conn.Close();
      conn.Dispose();

      _connections.Remove(cp.Name);
      serverNode.Nodes.Clear();
      tv.Nodes.Remove(serverNode);
    }

    #endregion //Connection Management

    #region Object Info
    private void OnHelpPermissionClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_table_privileges", "PERMISSIONS", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpForeignKeyInClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_fkeys @pktable_name = N", "FOREIGN_KEY_IN", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpForeignKeysClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_fkeys @fktable_name = N", "FOREIGN_KEYS", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpConstraintsClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_helpconstraint", "CONSTRAINTS", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpSpecialColumnsClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_special_columns", "IDENTITY_COLS", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpUsedSpaceClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_spaceused", "USED_SPACE", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpStatisticsClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_statistics", "STATISTICS", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpDependsClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_depends", "DEPENDENCIES", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    private void OnHelpObjectPermissionClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateObjectPermissionsScriptEditor(true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }


    private void OnObjectHelpClick(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateScriptEditor("sp_help", "OBJECT HELP", true);
      editor.Icon = global::PragmaSQL.Properties.Resources.Help;
    }

    #endregion

    #region Data Editor

    private void OnOpenTableDataEditorClick(object sender, EventArgs e)
    {
      NodeData data = GetCurrentSelectedNodeData();
      if (data == null)
      {
        return;
      }

      if ((data.Type != DBObjectType.UserTable) && (data.Type != DBObjectType.SystemTable))
      {
        return;
      }

      string caption = data.Name;
      string script = "select * from [" + data.Name + "]";
      frmTableEditor editor = TableEditorFactory.CreateTableEditor(data, caption, script, false, true);
      editor.Icon = global::PragmaSQL.Properties.Resources.dbTable;
      TableEditorFactory.ShowEditor(editor);
    }

    private void OnOpenViewDataEditorClick(object sender, EventArgs e)
    {
      NodeData data = GetCurrentSelectedNodeData();
      if (data == null)
      {
        return;
      }

      if (data.Type != DBObjectType.View)
      {
        return;
      }
      string caption = data.Name;
      string script = "select * from [" + data.Name + "]";
      frmTableEditor editor = TableEditorFactory.CreateTableEditor(data, caption, script, true, true);
      editor.Icon = global::PragmaSQL.Properties.Resources.dbView;
      TableEditorFactory.ShowEditor(editor);
    }

    #endregion //Data Editor

    #region Context Menu Factory

    private void OnNewConnectionClick(object sender, EventArgs e)
    {
      CreateNewConnection(false);
    }

    private void OnNewConnectionFromRepositoryClick(object sender, EventArgs e)
    {
      CreateNewConnectionFromRepository(false);
    }

    private void OnDisconnectClick(object sender, EventArgs e)
    {
      DisconnectFromServer(tv.SelectedNode);
    }

    private void OnRefreshNodeClick(object sender, EventArgs e)
    {
      LoadNodeDataDynamically(tv.SelectedNode, true);
    }

    private void OnModifyClick(object sender, EventArgs e)
    {
      if (!IsSelectedObjectScriptable())
      {
        return;
      }

      ModifySelectedObjectInScriptWindow();
    }


    public ContextMenuStrip CreateContextMenuStrip(int objectType)
    {
      if (_contextMenus.Keys.Contains(objectType))
      {
        return _contextMenus[objectType];
      }
      ToolStripMenuItem item = null;
      ContextMenuStrip mnu = null;
      switch (objectType)
      {
        case DBObjectType.Server:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Connect To", null, OnNewConnectionClick);
          mnu.Items.Add("Connection From List", null, OnNewConnectionFromRepositoryClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Disconnect From Server", null, OnDisconnectClick);
          break;
        case DBObjectType.UsersGroup:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("View As List", null, null);
          mnu.Items.Add("Filter", null, OnFilterClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.Database:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.GroupingFolderB:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("View As List", null, null);
          mnu.Items.Add("Filter", null, OnFilterClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.GroupingFolderY:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("View As List", null, null);
          mnu.Items.Add("Filter", null, OnFilterClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.UserTable:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Open", null, OnOpenTableDataEditorClick);
          mnu.Items.Add("View Columns", null, null);

          mnu.Items.Add("-", null, null);

          item = (ToolStripMenuItem)mnu.Items.Add("Table Info", null, null);
          item.DropDownItems.Add("Permissions", null, OnHelpPermissionClick);
          item.DropDownItems.Add("Foreign Keys", null, OnHelpForeignKeysClick);
          item.DropDownItems.Add("Foreign Key In", null, OnHelpForeignKeyInClick);
          item.DropDownItems.Add("Constraints", null, OnHelpConstraintsClick);
          item.DropDownItems.Add("Identity Columns", null, OnHelpSpecialColumnsClick);
          item.DropDownItems.Add("-", null, null);
          item.DropDownItems.Add("Used Space", null, OnHelpUsedSpaceClick);
          item.DropDownItems.Add("Statistics", null, OnHelpStatisticsClick);

          mnu.Items.Add("Object Help", null, OnObjectHelpClick);
          mnu.Items.Add("Dependencies", null, OnHelpDependsClick);

          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);

          break;
        case DBObjectType.SystemTable:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Open", null, OnOpenTableDataEditorClick);
          mnu.Items.Add("View Columns", null, null);

          mnu.Items.Add("-", null, null);

          item = (ToolStripMenuItem)mnu.Items.Add("Table Info", null, null);
          item.DropDownItems.Add("Permissions", null, OnHelpPermissionClick);
          item.DropDownItems.Add("Foreign Keys", null, OnHelpForeignKeysClick);
          item.DropDownItems.Add("Foreign Key In", null, OnHelpForeignKeyInClick);
          item.DropDownItems.Add("Constraints", null, OnHelpConstraintsClick);
          item.DropDownItems.Add("Identity Columns", null, OnHelpSpecialColumnsClick);
          item.DropDownItems.Add("-", null, null);
          item.DropDownItems.Add("Used Space", null, OnHelpUsedSpaceClick);
          item.DropDownItems.Add("Statistics", null, OnHelpStatisticsClick);

          mnu.Items.Add("Object Help", null, OnObjectHelpClick);
          mnu.Items.Add("Dependencies", null, OnHelpDependsClick);

          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.View:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Open", null, OnOpenViewDataEditorClick);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("-", null, null);

          item = (ToolStripMenuItem)mnu.Items.Add("Info", null, null);
          item.DropDownItems.Add("Object Help", null, OnObjectHelpClick);
          item.DropDownItems.Add("-", null, null);
          item.DropDownItems.Add("Constraints", null, OnHelpConstraintsClick);
          item.DropDownItems.Add("Permissions", null, OnHelpPermissionClick);

          mnu.Items.Add("Dependencies", null, OnHelpDependsClick);

          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.Trigger:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Object Help", null, OnObjectHelpClick);
          mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
          mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.StoredProc:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("Execute", null, null);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Object Help", null, OnObjectHelpClick);
          mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
          mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.TableValuedFunction:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Object Help", null, OnObjectHelpClick);
          mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
          mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.ScalarValuedFunction:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Object Help", null, OnObjectHelpClick);
          mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
          mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        default:
          break;
      }

      if (mnu != null)
      {
        _contextMenus.Add(objectType, mnu);
      }

      return mnu;
    }

    #endregion //Context Menu Factory

    #region TreeView Utilities
    public NodeData GetCurrentSelectedNodeData()
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        return null;
      }
      return NodeDataFactory.GetNodeData(node.Tag);
    }

    public void ShowFilterDialog(TreeNode node, NodeData data)
    {
      frmObjectExplorerFilter frm = new frmObjectExplorerFilter();
      frm.InitializeFilterDlg(data);

      Filter? filter = GetFilter(node);
      if (filter.HasValue)
      {
        frm.LoadFrom(filter);
      }

      filter = GetParentFilter(node);
      frm.HasParentFilter = filter.HasValue;

      DialogResult dlgRes = frm.ShowDialog();
      if (dlgRes == DialogResult.OK)
      {
        if (String.IsNullOrEmpty(data.Filter.Value.FilterText))
        {
          node.Text = data.Name;
        }
        else
        {
          node.Text = data.Name + " {Filtered}";
        }

        data.Populated = false;
        LoadNodeDataDynamically(node, true);
      }
    }

    public Regex CreateRegExFromFilter(Filter? filter)
    {
      if (!filter.HasValue || String.IsNullOrEmpty(filter.Value.FilterText))
      {
        return null;
      }

      Filter tmpFilter = filter.Value;

      RegexOptions options = RegexOptions.None;
      options |= RegexOptions.IgnoreCase;
      string text = String.Empty;
      switch ((FilterType)tmpFilter.FilterType)
      {
        case FilterType.RegularExpression:
          text = Regex.Escape(tmpFilter.FilterText).Replace(@"\*", ".*").Replace(@"\?", ".");
          break;

        case FilterType.Wildcards:
          text = tmpFilter.FilterText;
          break;

        case FilterType.Plain:
        default:
          text = Regex.Escape(tmpFilter.FilterText);
          break;
      }

      Regex regex = null;
      try
      {
        regex = new Regex(text, options);
      }
      catch (Exception exception)
      {
        MessageBox.Show(this, exception.Message, "Regular expression error");
      }
      return regex;
    }

    public Filter? GetParentFilter(TreeNode node)
    {

      TreeNode parentNode = node.Parent;
      if (parentNode == null)
      {
        return null;
      }
      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      if (parentData == null)
      {
        return null;
      }

      do
      {
        if (parentData.Filter.HasValue && parentData.Filter.Value.ApplyToChildren)
        {
          if (!String.IsNullOrEmpty(parentData.Filter.Value.FilterText))
          {
            return parentData.Filter;
          }
          else
          {
            break;
          }
        }
        else if (parentData.Filter.HasValue && !parentData.Filter.Value.ApplyToChildren && !String.IsNullOrEmpty(parentData.Filter.Value.FilterText))
        {
          break;
        }

        parentNode = parentNode.Parent;
        if (parentNode == null)
        {
          break;
        }
        parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      }
      while (parentNode != null && parentData != null);

      return null;
    }


    public Filter? GetFilter(TreeNode node)
    {
      if (node == null)
      {
        return null;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      if (data == null)
      {
        return null;
      }


      // Node has own filter defined
      if (data.Filter != null && data.Filter.Value.UseOwn)
      {
        return data.Filter;
      }
      // Trace up to parent nodes
      else
      {
        TreeNode parentNode = node.Parent;
        if (parentNode == null)
        {
          return null;
        }
        NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
        if (parentData == null)
        {
          return null;
        }

        do
        {
          if (parentData.Filter.HasValue && parentData.Filter.Value.ApplyToChildren)
          {
            if (!String.IsNullOrEmpty(parentData.Filter.Value.FilterText))
            {
              return parentData.Filter;
            }
            else
            {
              break;
            }
          }
          else if (parentData.Filter.HasValue && !parentData.Filter.Value.ApplyToChildren && !String.IsNullOrEmpty(parentData.Filter.Value.FilterText))
          {
            break;
          }

          parentNode = parentNode.Parent;
          if (parentNode == null)
          {
            break;
          }
          parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
        }
        while (parentNode != null && parentData != null);
      }

      return null;
    }

    private void OnFilterClick(object sender, EventArgs e)
    {
      btnFilter_Click(sender, e);
    }

    private TreeNode AddNode(TreeNodeCollection parent, string name, int imageIndex)
    {

      return AddNode(parent, name, imageIndex, imageIndex);
    }

    private TreeNode AddNode(TreeNodeCollection parent, string name, int imageIndex, int selImageIndex)
    {
      TreeNode result = null;
      if (parent != null)
      {
        result = parent.Add(name);
      }
      else
      {
        result = tv.Nodes.Add(name);
      }

      result.ImageIndex = imageIndex;
      result.SelectedImageIndex = selImageIndex;
      result.ContextMenuStrip = CreateContextMenuStrip(imageIndex);
      return result;
    }

    private TreeNode AddErrorNode(TreeNodeCollection parent, string name, string errText)
    {
      string err = String.Empty;
      if (!String.IsNullOrEmpty(errText))
      {
        err = " (" + errText + ")";
      }


      TreeNode result = null;
      if (parent != null)
      {
        result = parent.Add(name + err);
      }
      else
      {
        result = tv.Nodes.Add(name + err);
      }

      result.ImageIndex = DBObjectType.Error;
      result.SelectedImageIndex = DBObjectType.Error;

      return result;
    }

    private void LoadNodeDataDynamically(TreeNode node, bool forceRefresh)
    {
      if (_tvInitializing)
        return;

      if (node == null)
      {
        return;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      if (data == null)
      {
        return;
      }

      bool oldExpandedState = node.IsExpanded;
      try
      {
        Cursor = Cursors.AppStarting;
        tv.BeginUpdate();
        if (data.Type == DBObjectType.GroupingFolderB || data.Type == DBObjectType.GroupingFolderY)
        {
          if (data.Populated && !forceRefresh)
          {
            return;
          }

          if (data.Name == FolderType.Tables)
          {
            node.Nodes.Clear();
            AddGroupingFolderY(node, FolderType.SystemTables, data.DBName, true, DBObjectType.Database);
            AddGroupingFolderY(node, FolderType.UserTables, data.DBName, true, DBObjectType.Database);
          }
          else if (data.Name == FolderType.Functions)
          {
            node.Nodes.Clear();
            AddGroupingFolderY(node, FolderType.TableValuedFunctions, data.DBName, true, DBObjectType.Database);
            AddGroupingFolderY(node, FolderType.ScalarValuedFunctions, data.DBName, true, DBObjectType.Database);
          }
          else if (data.Name == FolderType.Triggers)
          {
            node.Nodes.Clear();
            PopulateTriggers(node);
          }
          else if (data.Name == FolderType.SystemTables)
          {
            node.Nodes.Clear();
            PopulateObjects(node, DBObjectType.SystemTable);
          }
          else if (data.Name == FolderType.UserTables)
          {
            node.Nodes.Clear();
            PopulateObjects(node, DBObjectType.UserTable);
          }
          else if (data.Name == "Views")
          {
            node.Nodes.Clear();
            PopulateObjects(node, DBObjectType.View);
          }
          else if (data.Name == FolderType.Procedures)
          {
            node.Nodes.Clear();
            PopulateObjects(node, DBObjectType.StoredProc);
          }
          else if (data.Name == FolderType.TableValuedFunctions)
          {
            node.Nodes.Clear();
            PopulateObjects(node, DBObjectType.TableValuedFunction);
          }
          else if (data.Name == FolderType.ScalarValuedFunctions)
          {
            node.Nodes.Clear();
            PopulateObjects(node, DBObjectType.ScalarValuedFunction);
          }
          else if (data.Name == FolderType.Triggers)
          {
            node.Nodes.Clear();
            PopulateTriggers(node);
          }
          else if (data.Name == FolderType.Columns)
          {
            node.Nodes.Clear();
            PopulateParamsAndColumns(node, data.ParentType);
          }
          else if (data.Name == FolderType.ForeignKeys)
          {
            node.Nodes.Clear();
            PopulateForignKeys(node.Parent, node, true);
          }
          else if (data.Name == FolderType.ForeignKeyIn)
          {
            node.Nodes.Clear();
            PopulateForignKeys(node.Parent, node, false);
          }
          else if (data.Name == FolderType.Parameters)
          {
            node.Nodes.Clear();
            PopulateParamsAndColumns(node, data.ParentType);
          }
        }
        else if (data.HasParamsOrCols)
        {
          if (data.Populated && !forceRefresh)
          {
            return;
          }

          node.Nodes.Clear();
          TryToAddTriggerGroupingFolder(node);
          TryToAddObjectFolders(data.Type, node, data);
        }
        else if (data.Type == DBObjectType.UsersGroup)
        {
          if (data.Populated && !forceRefresh)
          {
            return;
          }
          node.Nodes.Clear();
          PopulateUsers(data.ConnParams, node);
        }
        else if (data.Type == DBObjectType.Server)
        {
          if (data.Populated && !forceRefresh)
          {
            return;
          }

          node.Nodes.Clear();
          PopulateDatabases(data.ConnParams, node);
          TreeNode usersNode = CreateUsersNode(data.ConnParams, node);
          PopulateUsers(data.ConnParams, usersNode);
        }
        else if (data.Type == DBObjectType.Database)
        {
          if (data.Populated && !forceRefresh)
          {
            return;
          }
          node.Nodes.Clear();
          PopulateDatabase(node);
        }


        if (oldExpandedState)
        {
          node.Expand();
        }
      }
      finally
      {
        tv.EndUpdate();
        Cursor = Cursors.Default;
      }

    }

    #endregion //TreeView Utilities

    #region Populate Explorer With Objects


    private TreeNode AddServerNode(ConnectionParams cp)
    {
      if (cp == null)
      {
        return null;
      }

      TreeNode node = AddNode(null, cp.Name, DBObjectType.Server);
      NodeData data = NodeDataFactory.Create(cp, cp.Name, DBObjectType.Server, String.Empty);
      node.Tag = data;

      return node;
    }

    private void PopulateDatabases(ConnectionParams cp, TreeNode parentNode)
    {
      if (cp == null || parentNode == null)
      {
        return;
      }

      if (!_connections.Keys.Contains(cp.Name))
      {
        return;
      }

      SqlConnection conn = _connections[cp.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate databases!");
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode);
      TreeNode sysDbGroupNode = AddGroupingFolder(parentNode, "System Databases", DBObjectType.GroupingFolderB, true, DBObjectType.Server);
      sysDbGroupNode.Nodes.Clear();

      DataTable dbs = conn.GetSchema("Databases");

      dbs.DefaultView.Sort = "database_name";
      dbs = dbs.DefaultView.ToTable();

      foreach (DataRow row in dbs.Rows)
      {
        string dbName = (string)row["database_name"];
        TreeNode node = null;
        if (DBConstants.SystemDbNames.Contains(dbName.ToLowerInvariant()))
        {
          node = AddNode(sysDbGroupNode.Nodes, dbName, DBObjectType.Database);
        }
        else
        {
          node = AddNode(parentNode.Nodes, dbName, DBObjectType.Database);
        }

        NodeData data = NodeDataFactory.Create(cp, dbName, DBObjectType.Database, dbName);
        data.Populated = true;
        node.Tag = data;

        TreeNode tblsNode = AddGroupingFolderY(node, FolderType.Tables, dbName, true, DBObjectType.Database);
        tblsNode.Nodes.Clear();
        AddGroupingFolderY(tblsNode, FolderType.SystemTables, dbName, true, DBObjectType.Database);
        AddGroupingFolderY(tblsNode, FolderType.UserTables, dbName, true, DBObjectType.Database);

        AddGroupingFolderY(node, FolderType.Views, dbName, true, DBObjectType.Database);
        AddGroupingFolderY(node, FolderType.Procedures, dbName, true, DBObjectType.Database);

        TreeNode fnNode = AddGroupingFolderY(node, FolderType.Functions, dbName, true, DBObjectType.Database);
        fnNode.Nodes.Clear();
        AddGroupingFolderY(fnNode, FolderType.TableValuedFunctions, dbName, true, DBObjectType.Database);
        AddGroupingFolderY(fnNode, FolderType.ScalarValuedFunctions, dbName, true, DBObjectType.Database);

        if (cp.InitialCatalog.ToLowerInvariant() == dbName.ToLowerInvariant())
        {
          node.Expand();
          tv.SelectedNode = node;
        }
      }
    }

    private void PopulateDatabase(TreeNode parentNode)
    {
      if (parentNode == null)
      {
        return;
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      if (parentData == null)
      {
        AddErrorNode(parentNode.Nodes, String.Empty, "Parent node data is null!");
        return;
      }

      TreeNode tblsNode = AddGroupingFolderY(parentNode, FolderType.Tables, parentData.DBName, true, DBObjectType.Database);
      tblsNode.Nodes.Clear();
      AddGroupingFolderY(tblsNode, FolderType.SystemTables, parentData.DBName, true, DBObjectType.Database);
      AddGroupingFolderY(tblsNode, FolderType.UserTables, parentData.DBName, true, DBObjectType.Database);

      AddGroupingFolderY(parentNode, FolderType.Views, parentData.DBName, true, DBObjectType.Database);
      AddGroupingFolderY(parentNode, FolderType.Procedures, parentData.DBName, true, DBObjectType.Database);

      TreeNode fnNode = AddGroupingFolderY(parentNode, FolderType.Functions, parentData.DBName, true, DBObjectType.Database);
      fnNode.Nodes.Clear();
      AddGroupingFolderY(fnNode, FolderType.TableValuedFunctions, parentData.DBName, true, DBObjectType.Database);
      AddGroupingFolderY(fnNode, FolderType.ScalarValuedFunctions, parentData.DBName, true, DBObjectType.Database);

    }

    private TreeNode CreateUsersNode(ConnectionParams cp, TreeNode parentNode)
    {
      if (cp == null || parentNode == null)
      {
        return null;
      }

      if (!_connections.Keys.Contains(cp.Name))
      {
        return null;
      }

      SqlConnection conn = _connections[cp.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate users!");
      }

      TreeNode usersNode = AddNode(parentNode.Nodes, FolderType.Users, DBObjectType.UsersGroup);
      usersNode.Nodes.Clear();
      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      NodeData usersNodeData = NodeDataFactory.Create(parentData.ConnParams, FolderType.Users, DBObjectType.UsersGroup, parentData.DBName);
      usersNodeData.Populated = true;
      usersNodeData.Filter = Filter.CreateFilter();

      usersNode.Tag = usersNodeData;
      return usersNode;
    }

    private void PopulateUsers(ConnectionParams cp, TreeNode usersNode)
    {
      if (cp == null || usersNode == null)
      {
        return;
      }

      if (!_connections.Keys.Contains(cp.Name))
      {
        return;
      }

      SqlConnection conn = _connections[cp.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate users!");
      }

      NodeData usersNodeData = NodeDataFactory.GetNodeData(usersNode.Tag);
      if (usersNodeData == null)
      {
        throw new Exception("Users node does not exist!");
      }

      DataTable users = conn.GetSchema("Users");

      users.DefaultView.Sort = "user_name";
      users = users.DefaultView.ToTable();

      Filter? filter = GetFilter(usersNode);
      Regex regex = CreateRegExFromFilter(filter);
      int usrCount = 0;

      foreach (DataRow row in users.Rows)
      {
        string userName = (string)row["user_name"];
        if (regex != null && !regex.Match(userName).Success)
        {
          continue;
        }
        TreeNode node = AddNode(usersNode.Nodes, userName, DBObjectType.User);

        NodeData userNodeData = NodeDataFactory.Create(usersNodeData.ConnParams, userName, DBObjectType.User, usersNodeData.DBName);
        userNodeData.Populated = false;

        node.Tag = userNodeData;
        usrCount++;
      }

      if (regex == null)
      {
        usersNode.Text = usersNodeData.Name + " (" + usrCount.ToString() + ")";
      }
      else
      {
        usersNode.Text = usersNodeData.Name + " (" + usrCount.ToString() + ") { Filtered }";
      }
    }


    private void PopulateObjects(TreeNode parentNode, int nodeType)
    {
      if (parentNode == null)
      {
        return;
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);

      if (parentData == null)
      {
        AddErrorNode(parentNode.Nodes, String.Empty, "Parent data is null.");
        return;
      }

      if (!_connections.Keys.Contains(parentData.ConnParams.Name))
      {
        return;
      }

      SqlConnection conn = _connections[parentData.ConnParams.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate table-valued function!");
      }


      string script = String.Empty;
      switch (nodeType)
      {
        case DBObjectType.UserTable:
          script = global::PragmaSQL.Properties.Resources.Script_GetUserTables;
          break;
        case DBObjectType.SystemTable:
          script = global::PragmaSQL.Properties.Resources.Script_GetSystemTables;
          break;
        case DBObjectType.View:
          script = global::PragmaSQL.Properties.Resources.Script_GetViews;
          break;

        case DBObjectType.StoredProc:
          script = global::PragmaSQL.Properties.Resources.Script_GetStoredProcedures;
          break;
        case DBObjectType.TableValuedFunction:
          script = global::PragmaSQL.Properties.Resources.Script_GetTableValuedFunctions;
          break;
        case DBObjectType.ScalarValuedFunction:
          script = global::PragmaSQL.Properties.Resources.Script_GetScalarValuedFunctions;
          break;
        default:
          AddErrorNode(parentNode.Nodes, String.Empty, String.Format("Invalid object type. \"{0}\" ", nodeType));
          return;
      }

      if (conn.Database.ToLowerInvariant() != parentData.DBName.ToLowerInvariant())
      {
        try
        {
          conn.ChangeDatabase(parentData.DBName);
        }
        catch (Exception ex)
        {
          AddErrorNode(parentNode.Nodes, String.Empty, ex.Message);
          return;
        }
      }

      Filter? filter = GetFilter(parentNode);
      Regex regex = CreateRegExFromFilter(filter);


      SqlCommand cmd = new SqlCommand(script, conn);
      SqlDataReader reader = cmd.ExecuteReader();
      int objCount = 0;

      while (reader.Read())
      {
        string objName = (string)reader["name"];
        if (regex != null && !regex.Match(objName).Success)
        {
          continue;
        }

        TreeNode node = AddNode(parentNode.Nodes, objName, nodeType);

        NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, objName, nodeType, parentData.DBName);
        nodeData.ID = (int)reader["id"];
        nodeData.HasParamsOrCols = true;

        node.Tag = nodeData;
        objCount++;

        AddNode(node.Nodes, "...", -1);
      }

      reader.Close();
      parentData.Populated = true;

      if (regex == null)
      {
        parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")";
      }
      else
      {
        parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")" + " { Filtered }";
      }

    }

    private void TryToAddObjectFolders(int nodeType, TreeNode node, NodeData nodeData)
    {
      switch (nodeType)
      {
        case DBObjectType.UserTable:
          AddGroupingFolderY(node, FolderType.Columns, nodeData.DBName, true, nodeType);
          AddGroupingFolderY(node, FolderType.ForeignKeys, nodeData.DBName, true, nodeType);
          AddGroupingFolderY(node, FolderType.ForeignKeyIn, nodeData.DBName, true, nodeType);
          break;
        case DBObjectType.View:
          AddGroupingFolderY(node, FolderType.Columns, nodeData.DBName, true, nodeType);
          break;
        case DBObjectType.SystemTable:
          AddGroupingFolderY(node, FolderType.Columns, nodeData.DBName, true, nodeType);
          AddGroupingFolderY(node, FolderType.ForeignKeys, nodeData.DBName, true, nodeType);
          AddGroupingFolderY(node, FolderType.ForeignKeyIn, nodeData.DBName, true, nodeType);
          break;
        case DBObjectType.StoredProc:
          AddGroupingFolderY(node, FolderType.Parameters, nodeData.DBName, true, nodeType);
          break;
        case DBObjectType.ScalarValuedFunction:
          AddGroupingFolderY(node, FolderType.Parameters, nodeData.DBName, true, nodeType);
          break;
        case DBObjectType.TableValuedFunction:
          AddGroupingFolderY(node, FolderType.Parameters, nodeData.DBName, true, nodeType);
          AddGroupingFolderY(node, FolderType.Columns, nodeData.DBName, true, nodeType);
          break;
        default:
          AddNode(node.Nodes, "...", -1);
          break;
      }
    }

    private TreeNode TryToAddTriggerGroupingFolder(TreeNode parentNode)
    {
      if (parentNode == null)
      {
        return null;
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      if (parentData == null)
      {
        return null;
      }

      if (!((parentData.Type == DBObjectType.UserTable)
        || (parentData.Type == DBObjectType.SystemTable)
        || (parentData.Type == DBObjectType.View))
        )
      {
        return null;
      }


      TreeNode node = AddGroupingFolderY(parentNode, FolderType.Triggers, parentData.DBName, false, parentData.Type);
      NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, FolderType.Triggers, DBObjectType.GroupingFolderY, parentData.DBName);
      nodeData.Filter = Filter.CreateFilter();
      nodeData.ID = parentData.ID;
      node.Tag = nodeData;

      if (!PopulateTriggers(node))
      {
        parentNode.Nodes.Remove(node);
        return null;
      }
      else
      {
        return node;
      }

    }


    private void PopulateParamsAndColumns(TreeNode parentNode, int nodeType)
    {
      if (parentNode == null)
      {
        return;
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);

      if (parentData == null)
      {
        AddErrorNode(parentNode.Nodes, String.Empty, "Parent data is null.");
        return;
      }

      if (!_connections.Keys.Contains(parentData.ConnParams.Name))
      {
        return;
      }

      SqlConnection conn = _connections[parentData.ConnParams.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate table-valued function!");
      }


      string script = global::PragmaSQL.Properties.Resources.Script_GetColsAndParams;



      if (conn.Database.ToLowerInvariant() != parentData.DBName.ToLowerInvariant())
      {
        try
        {
          conn.ChangeDatabase(parentData.DBName);
        }
        catch (Exception ex)
        {
          AddErrorNode(parentNode.Nodes, String.Empty, ex.Message);
          return;
        }
      }

      SqlCommand cmd = new SqlCommand(script, conn);
      SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.Int);
      param.Value = parentData.ID;
      SqlDataReader reader = cmd.ExecuteReader();

      Filter? filter = GetFilter(parentNode);
      Regex regex = CreateRegExFromFilter(filter);
      int objCnt = 0;

      while (reader.Read())
      {
        string pName = (string)reader["name"];

        pName = pName + " [ " + (string)reader["typename"] + " (" + ((short)reader["length"]).ToString() + ") " + "]";

        int type = -1;


        if ((int)reader["isoutparam"] == 1)
        {
          type = DBObjectType.ParameterOut;
        }
        else if (nodeType == DBObjectType.StoredProc || nodeType == DBObjectType.ScalarValuedFunction || nodeType == DBObjectType.TableValuedFunction)
        {

          if (nodeType == DBObjectType.TableValuedFunction)
          {
            if (parentData.Name == FolderType.Columns)
            {
              if (!pName.StartsWith("@"))
              {
                type = DBObjectType.ParameterOut;
              }
              else
              {
                continue;
              }
            }
            else if (parentData.Name == FolderType.Parameters)
            {
              if (pName.StartsWith("@"))
              {
                type = DBObjectType.ParameterOut;
              }
              else
              {
                continue;
              }
            }
            else
            {
              continue;
            }
          }
          else
          {
            if (pName.StartsWith("@"))
            {
              type = DBObjectType.ParameterIn;
            }
            else
            {
              type = DBObjectType.ParameterOut;
            }
          }
        }
        else if (nodeType == DBObjectType.UserTable || nodeType == DBObjectType.SystemTable || nodeType == DBObjectType.View)
        {
          byte status = (byte)reader["status"];
          if ((status & 128) == 128)
          {
            type = DBObjectType.IdentityCol;
          }
          else
          {
            type = DBObjectType.Column;
          }
        }

        if (regex != null && !regex.Match(pName).Success)
        {
          continue;
        }

        TreeNode node = AddNode(parentNode.Nodes, pName, type);

        NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, pName, type, parentData.DBName);
        nodeData.ID = parentData.ID;
        node.Tag = nodeData;
        objCnt++;
      }

      reader.Close();
      parentData.Populated = true;
      if (regex == null)
      {
        parentNode.Text = parentData.Name + " (" + objCnt.ToString() + ")";
      }
      else
      {
        parentNode.Text = parentData.Name + " (" + objCnt.ToString() + ")" + " { Filtered }";
      }
    }

    private void PopulateForignKeys(TreeNode tableNode, TreeNode parentNode, bool ownOnly)
    {
      if (parentNode == null)
      {
        return;
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      NodeData tableData = NodeDataFactory.GetNodeData(tableNode.Tag);

      if (parentData == null || tableData == null)
      {
        AddErrorNode(parentNode.Nodes, String.Empty, "Parent data is null.");
        return;
      }

      if (!_connections.Keys.Contains(tableData.ConnParams.Name))
      {
        return;
      }

      SqlConnection conn = _connections[tableData.ConnParams.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate table-valued function!");
      }

      string script = String.Empty;
      if (ownOnly)
      {
        script = global::PragmaSQL.Properties.Resources.Script_ForeignKeys;
      }
      else
      {
        script = global::PragmaSQL.Properties.Resources.Script_ForeignKeyIn;
      }

      script += "'" + tableData.Name + "'";

      if (conn.Database.ToLowerInvariant() != tableData.DBName.ToLowerInvariant())
      {
        try
        {
          conn.ChangeDatabase(tableData.DBName);
        }
        catch (Exception ex)
        {
          AddErrorNode(parentNode.Nodes, String.Empty, ex.Message);
          return;
        }
      }

      Filter? filter = GetFilter(parentNode);
      Regex regex = CreateRegExFromFilter(filter);

      SqlCommand cmd = new SqlCommand(script, conn);
      SqlDataReader reader = cmd.ExecuteReader();
      int objCount = 0;
      string fkInfo = String.Empty;
      while (reader.Read())
      {
        fkInfo = (string)reader["FK_NAME"];
        fkInfo += " {";
        fkInfo += (string)reader["PKTABLE_NAME"] + "." + (string)reader["PKCOLUMN_NAME"];
        fkInfo += " -> " + (string)reader["FKTABLE_NAME"] + "." + (string)reader["FKCOLUMN_NAME"];
        fkInfo += "}";

        if (regex != null && !regex.Match(fkInfo).Success)
        {
          continue;
        }

        TreeNode node = AddNode(parentNode.Nodes, fkInfo, DBObjectType.ForeignKey);

        NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, fkInfo, DBObjectType.ForeignKey, parentData.DBName);
        nodeData.ID = parentData.ID;
        node.Tag = nodeData;
        objCount++;
      }

      reader.Close();
      parentData.Populated = true;

      if (regex == null)
      {
        parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")";
      }
      else
      {
        parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")" + " { Filtered }";
      }


    }
    private bool PopulateTriggers(TreeNode parentNode)
    {
      if (parentNode == null)
      {
        return false;
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);

      if (parentData == null)
      {
        AddErrorNode(parentNode.Nodes, String.Empty, "Parent data is null.");
        return false;
      }

      if (!_connections.Keys.Contains(parentData.ConnParams.Name))
      {
        return false;
      }

      SqlConnection conn = _connections[parentData.ConnParams.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate table-valued function!");
      }


      string script = global::PragmaSQL.Properties.Resources.Script_GetTriggers;

      if (conn.Database.ToLowerInvariant() != parentData.DBName.ToLowerInvariant())
      {
        try
        {
          conn.ChangeDatabase(parentData.DBName);
        }
        catch (Exception ex)
        {
          AddErrorNode(parentNode.Nodes, String.Empty, ex.Message);
          return true;
        }
      }

      SqlCommand cmd = new SqlCommand(script, conn);
      SqlParameter param = cmd.Parameters.Add("@parentid", SqlDbType.Int);
      param.Value = parentData.ID;

      SqlDataReader reader = cmd.ExecuteReader();
      int objCount = 0;

      Filter? filter = GetFilter(parentNode);
      Regex regex = CreateRegExFromFilter(filter);

      while (reader.Read())
      {
        string objName = (string)reader["name"];
        if (regex != null && !regex.Match(objName).Success)
        {
          continue;
        }

        TreeNode node = AddNode(parentNode.Nodes, objName, DBObjectType.Trigger);

        NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, objName, DBObjectType.Trigger, parentData.DBName);
        nodeData.ID = (int)reader["id"];

        node.Tag = nodeData;
        objCount++;
      }

      reader.Close();
      parentData.Populated = true;

      if (regex == null)
      {
        parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")";
      }
      else
      {
        parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")" + " { Filtered }";
      }


      if (objCount == 0)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    private TreeNode AddGroupingFolder(TreeNode parentNode, string name, int nodeType, string dbName, bool addFakeChild, int parentType)
    {

      if ((parentNode == null) || (nodeType != DBObjectType.GroupingFolderB && nodeType != DBObjectType.GroupingFolderY))
      {
        return null;
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      if (parentData == null)
      {
        return AddErrorNode(parentNode.Nodes, name, "Parent data is empty");
      }


      TreeNode node = AddNode(parentNode.Nodes, name, nodeType);
      if (addFakeChild)
      {
        node.Nodes.Add("...");
      }

      NodeData data = NodeDataFactory.Create(parentData.ConnParams, name, nodeType, dbName);
      data.ConnParams = ((NodeData)parentNode.Tag).ConnParams;
      data.ParentType = parentType;
      data.ID = parentData.ID;
      data.Filter = Filter.CreateFilter();
      node.Tag = data;

      return node;
    }

    private TreeNode AddGroupingFolder(TreeNode parentNode, string name, int nodeType, bool addFakeChild, int parentType)
    {

      return AddGroupingFolder(parentNode, name, nodeType, String.Empty, addFakeChild, parentType);
    }

    private TreeNode AddGroupingFolderY(TreeNode parentNode, string name, string dbName, bool addFakeChild, int parentType)
    {
      return AddGroupingFolder(parentNode, name, DBObjectType.GroupingFolderY, dbName, addFakeChild, parentType);
    }

    private TreeNode AddGroupingFolderB(TreeNode parentNode, string name, string dbName, bool addFakeChild, int parentType)
    {
      return AddGroupingFolder(parentNode, name, DBObjectType.GroupingFolderB, dbName, addFakeChild, parentType);
    }

    #endregion //Populate Explorer With Objects

    #region Script editor related



    public frmScriptEditor CreateScriptEditor()
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        CreateNewConnectionFromRepository(false);
      }
      node = tv.SelectedNode;
      if (node == null)
      {
        return null;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      frmScriptEditor editor = ScriptEditorFactory.CreateScriptEditor(data);
      ScriptEditorFactory.ShowScriptEditor(editor);
      return editor;
    }

    public frmScriptEditor CreateScriptEditor(string procName, string captionPostfix, bool canRun)
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        CreateNewConnectionFromRepository(false);
      }
      node = tv.SelectedNode;
      if (node == null)
      {
        return null;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);

      string script = "EXEC " + procName + "'" + data.Name + "'";
      string caption = data.Name + " {" + captionPostfix + "}";
      frmScriptEditor editor = ScriptEditorFactory.CreateScriptEditor(caption, script, data);
      editor.HideTextEditor();
      ScriptEditorFactory.ShowScriptEditor(editor);
      editor.RunScript(RunType.Execute);

      return editor;
    }

    public frmScriptEditor CreateObjectPermissionsScriptEditor(bool canRun)
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        CreateNewConnectionFromRepository(false);
      }
      node = tv.SelectedNode;
      if (node == null)
      {
        return null;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);

      string script = global::PragmaSQL.Properties.Resources.Script_Permissions;
      script = script.Replace("$objid$", data.ID.ToString());

      string caption = data.Name + " {PERMISSIONS}";
      frmScriptEditor editor = ScriptEditorFactory.CreateScriptEditor(caption, script, data);
      editor.HideTextEditor();
      ScriptEditorFactory.ShowScriptEditor(editor);

      editor.RunScript(RunType.Execute);
      //editor.ScriptText = String.Empty;
      //editor.ScriptModified = false;
      return editor;
    }

    public frmScriptEditor OpenFileInNewScriptEditor()
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        CreateNewConnectionFromRepository(false);
      }
      node = tv.SelectedNode;
      if (node == null)
      {
        return null;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      frmScriptEditor editor = ScriptEditorFactory.OpenFile(String.Empty, data);
      ScriptEditorFactory.ShowScriptEditor(editor);
      return editor;
    }

    private void ModifySelectedObjectInScriptWindow()
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        return;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      if (data == null)
      {
        MessageBox.Show("Node data not assigned", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      ProgrammabilityHelper.SqlConn = _connections[data.ConnParams.Name];
      string script = ProgrammabilityHelper.GetObjectCreateScript(data.ID);
      script = ProgrammabilityHelper.ReplaceCreateWithAlter(data.Type, script);

      frmScriptEditor editor = ScriptEditorFactory.CreateScriptEditor(data.Name, script, data.Type, data);
      ScriptEditorFactory.ShowScriptEditor(editor);
    }

    #endregion //Script editor related

    #region Other Methods
    
    public void CreateDatabaseSearchForm()
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        return;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      if (data == null)
      {
        return;
      }

      if (data.ConnParams == null)
      {
        return;
      }

      if (String.IsNullOrEmpty(data.DBName))
      {
        return;
      }

      string caption = "Search " + data.ConnParams.Name + " {" + data.DBName + "}";
      frmDbObjectSearch frm = DBObjectSearchFactory.CreateDBObjectSearchForm(data, caption, String.Empty, false);
      DBObjectSearchFactory.ShowForm(frm);
    }
    #endregion
    
    private void connectToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CreateNewConnection(false);
    }

    private void connectionFromRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CreateNewConnectionFromRepository(false);
    }


    private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DisconnectFromServer(tv.SelectedNode);
    }


    private void tv_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
      LoadNodeDataDynamically(e.Node, false);
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
      LoadNodeDataDynamically(tv.SelectedNode, true);
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      CreateScriptEditor();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      OpenFileInNewScriptEditor();
    }



    private void tv_DoubleClick(object sender, EventArgs e)
    {
      if (!IsSelectedObjectScriptable())
      {
        return;
      }

      ModifySelectedObjectInScriptWindow();

    }

    private void frmObjectExplorer_Shown(object sender, EventArgs e)
    {
      if (_isInitialShow)
      {
        CreateNewConnectionFromRepository(true);
        _isInitialShow = false;
      }
    }

    private void tv_AfterSelect(object sender, TreeViewEventArgs e)
    {
      edtPath.Text = e.Node.FullPath;

      TreeNode node = e.Node;
      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      if (
          (data.Type == DBObjectType.GroupingFolderB
            || data.Type == DBObjectType.GroupingFolderY
            || data.Type == DBObjectType.UsersGroup
          )
          &&
          (data.Filter.HasValue)
        )
      {
        btnFilter.Enabled = true;
      }
      else
      {
        btnFilter.Enabled = false;
      }
    }

    private void btnFilter_Click(object sender, EventArgs e)
    {
      TreeNode node = tv.SelectedNode;
      if (node == null)
      {
        return;
      }

      NodeData data = NodeDataFactory.GetNodeData(node.Tag);
      if (data == null)
      {
        return;
      }
      ShowFilterDialog(node, data);
    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
      CreateDatabaseSearchForm();
    }


  }
}