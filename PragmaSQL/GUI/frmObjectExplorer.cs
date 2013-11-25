using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
    private int _scriptEditorCnt = 0;
    private IDictionary<int,ContextMenuStrip> _contextMenus = new Dictionary<int,ContextMenuStrip>();

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
        PopulateUsers(cp, serverNode);

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
      DialogResult dlgRes = frm.ShowDialog();
      if (dlgRes != DialogResult.OK)
      {
        return;
      }
      
      OpenConnection(frm.GetCurrentConnectionSpec());
      if (wantEmptyScript)
      {
        CreateNewScriptEditor();
      }
    }

    private void CreateNewConnectionFromRepository(bool wantEmptyScript)
    {
      frmConnectionRepository frm = new frmConnectionRepository();
      DialogResult dlgRes = frm.ShowDialog();
      if (dlgRes != DialogResult.OK)
      {
        return;
      }

      OpenConnection(frm.SelectedDataSource);
      if (wantEmptyScript)
      {
        CreateNewScriptEditor();
      }
    }

    private void DisconnectFromServer(TreeNode serverNode )
    {

      if ( ( serverNode == null ))
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
      ModifySelectedObjectInScriptWindow();
    }

    public ContextMenuStrip CreateContextMenuStrip(int objectType)
    {
      if (_contextMenus.Keys.Contains(objectType))
      {
        return _contextMenus[objectType];
      }

      ContextMenuStrip mnu = null;
      switch (objectType)
      {
        case DBObjectType.Server:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Connect To",null,OnNewConnectionClick);
          mnu.Items.Add("Connection From List",null,OnNewConnectionFromRepositoryClick);
          mnu.Items.Add("-",null,null);
          mnu.Items.Add("Disconnect From Server",null,OnDisconnectClick);
          break;
        case DBObjectType.UsersGroup:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("View As List",null,null);
          mnu.Items.Add("Refresh",null, OnRefreshNodeClick);
          break;
        case DBObjectType.Database:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.GroupingFolderB:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("View As List", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.GroupingFolderY:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("View As List", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.UserTable:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Open",null,null);
          mnu.Items.Add("View Columns",null,null);
          mnu.Items.Add("-",null,null);
          mnu.Items.Add("Refresh",null,OnRefreshNodeClick);
          break;
        case DBObjectType.SystemTable:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Open",null,null);
          mnu.Items.Add("View Columns",null,null);
          mnu.Items.Add("-",null,null);
          mnu.Items.Add("Refresh",null,OnRefreshNodeClick);
          break;
        case DBObjectType.View:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("Open",null,null);
          mnu.Items.Add("-",null,null);
          mnu.Items.Add("Refresh",null,OnRefreshNodeClick);
          break;
        case DBObjectType.Trigger:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.StoredProc:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("Execute", null, null);
          mnu.Items.Add("-", null, null);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.TableValuedFunction:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
          mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
          break;
        case DBObjectType.ScalarValuedFunction:
          mnu = new ContextMenuStrip(this.components);
          mnu.Items.Add("Modify", null, OnModifyClick);
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

          if (data.Name == FolderType.SystemTables)
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
          TryToAddObjectFolders(data.Type,node,data);
        }
        else if (data.Type == DBObjectType.Server)
        {
          if (data.Populated && !forceRefresh)
          {
            return;
          }

          node.Nodes.Clear();
          PopulateDatabases(data.ConnParams, node);
          PopulateUsers(data.ConnParams, node);
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
      NodeData data = NodeDataFactory.Create(cp, cp.Name, DBObjectType.Server,String.Empty);
      node.Tag = data;

      return node;
    }

    private void PopulateDatabases(ConnectionParams cp, TreeNode parentNode)
    {
      if ( cp == null || parentNode == null )
      {
        return;
      }
      
      if(!_connections.Keys.Contains(cp.Name))
      {
        return;
      }

      SqlConnection conn = _connections[cp.Name];
      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Can not populate databases!");
      }

      NodeData parentData = NodeDataFactory.GetNodeData(parentNode);
      TreeNode sysDbGroupNode = AddGroupingFolder(parentNode, "System Databases", DBObjectType.GroupingFolderB,true,DBObjectType.Server);
      sysDbGroupNode.Nodes.Clear();

      DataTable dbs = conn.GetSchema("Databases");

      dbs.DefaultView.Sort = "database_name";
      dbs = dbs.DefaultView.ToTable();

      foreach ( DataRow row in dbs.Rows)
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

        TreeNode  tblsNode = AddGroupingFolderY(node, FolderType.Tables,dbName,true,DBObjectType.Database);
        tblsNode.Nodes.Clear();
        AddGroupingFolderY(tblsNode, FolderType.SystemTables, dbName, true, DBObjectType.Database);
        AddGroupingFolderY(tblsNode, FolderType.UserTables, dbName, true, DBObjectType.Database);

        AddGroupingFolderY(node, FolderType.Views, dbName, true, DBObjectType.Database);
        AddGroupingFolderY(node, FolderType.Procedures, dbName, true, DBObjectType.Database);

        TreeNode fnNode = AddGroupingFolderY(node, FolderType.Functions, dbName, true, DBObjectType.Database);
        fnNode.Nodes.Clear();
        AddGroupingFolderY(fnNode,FolderType.TableValuedFunctions, dbName, true, DBObjectType.Database);
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
      
      TreeNode tblsNode = AddGroupingFolderY(parentNode, FolderType.Tables,parentData.DBName, true, DBObjectType.Database);
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

    private void PopulateUsers(ConnectionParams cp, TreeNode parentNode)
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
        throw new InvalidConnectionState("Can not populate users!");
      }

      TreeNode usersNode = AddNode(parentNode.Nodes, FolderType.Users, DBObjectType.UsersGroup);
      usersNode.Nodes.Clear();
      NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
      NodeData usersNodeData = NodeDataFactory.Create(parentData.ConnParams, FolderType.Users, DBObjectType.UsersGroup,parentData.DBName);
      usersNodeData.Populated = true;
      usersNode.Tag = usersNodeData;

      DataTable users = conn.GetSchema("Users");

      users.DefaultView.Sort = "user_name";
      users = users.DefaultView.ToTable();

      foreach (DataRow row in users.Rows)
      {
        string userName = (string)row["user_name"];
        TreeNode node = AddNode(usersNode.Nodes, userName, DBObjectType.User);

        NodeData userNodeData = NodeDataFactory.Create(parentData.ConnParams, userName, DBObjectType.User,parentData.DBName);
        userNodeData.Populated = false;

        node.Tag = userNodeData;
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

      SqlCommand cmd = new SqlCommand(script, conn);
      SqlDataReader reader = cmd.ExecuteReader();
      int objCount = 0;

      while (reader.Read())
      {
        string objName = (string)reader["name"];
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
      parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")";
    }

    private void TryToAddObjectFolders(int nodeType, TreeNode node, NodeData nodeData)
    {
      switch (nodeType)
      {
        case DBObjectType.UserTable:
          AddGroupingFolderY(node, FolderType.Columns, nodeData.DBName, true, nodeType);
          break;
        case DBObjectType.View:
          AddGroupingFolderY(node, FolderType.Columns, nodeData.DBName, true, nodeType);
          break;
        case DBObjectType.SystemTable:
          AddGroupingFolderY(node, FolderType.Columns, nodeData.DBName, true, nodeType);
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
          
          if ( nodeType == DBObjectType.TableValuedFunction) 
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

        TreeNode node = AddNode(parentNode.Nodes, pName, type);

        NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, pName, type, parentData.DBName);
        nodeData.ID = parentData.ID;
        node.Tag = nodeData;
      }

      reader.Close();
      parentData.Populated = true;
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

      while (reader.Read())
      {
        string objName = (string)reader["name"];
        TreeNode node = AddNode(parentNode.Nodes, objName, DBObjectType.Trigger);

        NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, objName, DBObjectType.Trigger, parentData.DBName);
        nodeData.ID = (int)reader["id"];

        node.Tag = nodeData;
        objCount++;
      }

      reader.Close();
      parentData.Populated = true;
      parentNode.Text = parentData.Name + " (" + objCount.ToString() + ")";

      if (objCount == 0)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    private TreeNode AddGroupingFolder(TreeNode parentNode, string name, int nodeType, string dbName,bool addFakeChild, int parentType)
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
    
    private frmScriptEditor CreateNewScriptEditor()
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
      if (data == null)
      {
        MessageBox.Show("Can not create connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
      }

      frmScriptEditor editor = new frmScriptEditor();
      editor.Text = "Script " + (++_scriptEditorCnt).ToString();
      editor.TabText = editor.Text;

      if (DockPanel.DocumentStyle == DocumentStyles.SystemMdi)
      {
        editor.MdiParent = this;
        editor.Show();
      }
      else
      {
        editor.Show(DockPanel);
      }
      editor.InitializeScriptEditor(String.Empty, DBObjectType.None, data.ConnParams, data.DBName);
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

      frmScriptEditor editor = new frmScriptEditor();
      editor.Text = data.Name;
      editor.TabText = editor.Text;

      if (DockPanel.DocumentStyle == DocumentStyles.SystemMdi)
      {
        editor.MdiParent = this;
        editor.Show();
      }
      else
      {
        editor.Show(DockPanel);
      }

      ProgrammabilityHelper.SqlConn = _connections[data.ConnParams.Name];
      string script = ProgrammabilityHelper.GetObjectCreateScript(data.ID);
      script = ProgrammabilityHelper.ReplaceCreateWithAlter(data.Type, script);
      
      editor.InitializeScriptEditor(script, data.Type, data.ConnParams, data.DBName);
    }

    #endregion //Script editor related
    
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
      LoadNodeDataDynamically(e.Node,false);
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
      LoadNodeDataDynamically(tv.SelectedNode,true);
    }
   
    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      CreateNewScriptEditor();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      frmScriptEditor editor = CreateNewScriptEditor();
      if (editor != null)
      {
        editor.OpenScriptFromFile();
      }
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

  }
}