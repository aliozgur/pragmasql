/*************************************************************************************
 * Ali Özgür
 * ali_ozgur@hotmail.com
 * www.pragmasql.com 
 * 
 * Source code included in this file can not be used without written
 * permissions of the owner mentioned above. 
 * All rigths reserver
 * Copyright PragmaSQL 2007 
 *************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Media;

using WeifenLuo.WinFormsUI.Docking;
using MWControls;
using MWCommon;
using ICSharpCode.Core;

using PragmaSQL.Core;
using PragmaSQL.WebBrowserEx;

namespace PragmaSQL
{
    public partial class frmObjectExplorer : DockContent, IObjectExplorerService
    {
        #region Fields And Properties
        private IDictionary<string, SqlConnection> _connections = new Dictionary<string, SqlConnection>();



        private IDictionary<int, ContextMenuStrip> _contextMenus = new Dictionary<int, ContextMenuStrip>();

        private bool _tvInitializing = false;

        private bool _isInitialShow = true;
        public bool IsInitialShow
        {
            get { return _isInitialShow; }
            set { _isInitialShow = value; }
        }

        /*
        private bool _connectInitial = true;
        public bool ConnectInitial
        {
          get { return _connectInitial; }
          set { _connectInitial = value; }
        }
        */

        public TreeNode SelectedNode
        {
            get
            {
                if (tv.SelNode == null)
                {
                    return tv.SelectedNode;
                }
                else
                {
                    return tv.SelNode;
                }
            }
            set
            {
                tv.SelectNode(value, true);
            }
        }

        private bool IsSelectedObjectScriptable()
        {
            if (SelectedNode == null)
            {
                return false;
            }

            NodeData data = NodeDataFactory.GetNodeData(SelectedNode.Tag);
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

        private bool CanDragSelectedNode()
        {
            if (SelectedNode == null)
            {
                return false;
            }

            NodeData data = NodeDataFactory.GetNodeData(SelectedNode.Tag);
            if (data == null)
            {
                return false;
            }
            if (data.Type == DBObjectType.View
              || data.Type == DBObjectType.Trigger
              || data.Type == DBObjectType.StoredProc
              || data.Type == DBObjectType.TableValuedFunction
              || data.Type == DBObjectType.ScalarValuedFunction
              || data.Type == DBObjectType.UserTable
              || data.Type == DBObjectType.SystemTable
              )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ShowFullNames
        {
            get
            {
                //if (ConfigHelper.Current != null)
                //  return ConfigHelper.Current.ObjectExplorerOptions.ShowFullObjectNames;
                //else
                //  return true;
                return true;
            }
        }

        private bool ShowSysTables
        {
            get
            {
                if (ConfigHelper.Current != null)
                    return ConfigHelper.Current.ObjectExplorerOptions.ShowSystemTables;
                else
                    return false;
            }
        }

        private bool ShowDbCompatLevel
        {
            get
            {
                if (ConfigHelper.Current != null)
                    return ConfigHelper.Current.ObjectExplorerOptions.ShowDatabaseCompatibilityLevel;
                else
                    return false;
            }
        }

        private bool ShowSysDbs
        {
            get
            {
                if (ConfigHelper.Current != null)
                    return ConfigHelper.Current.ObjectExplorerOptions.ShowSystemDatabases;
                else
                    return false;
            }
        }


        #endregion Fields And Properties

        #region CTOR

        public frmObjectExplorer()
        {
            InitializeComponent();
        }

        #endregion //CTOR

        #region Connection Management
        private bool OpenConnection(ConnectionParams cp)
        {
            try
            {
                _tvInitializing = true;

                if (cp == null)
                {
                    return false;
                }

                string connKey = cp.PrepareConnKey();
                if (_connections.Keys.Contains(connKey))
                {
                    return false;
                }

                string error = String.Empty;

                SqlConnection conn = cp.CreateSqlConnection(false, false);

                AsyncConnectionResult aResult = frmAsyncConnectionOpener.TryToOpenConnection(conn, ref error);

                if (aResult == AsyncConnectionResult.Error)
                {
                    conn.Dispose();
                    conn = null;
                    throw new Exception(error);
                }
                else if (aResult != AsyncConnectionResult.Success)
                {
                    return false;
                }


                _connections.Add(connKey, conn);
                TreeNode serverNode = AddServerNode(cp, conn.ServerVersion);
                if (serverNode == null)
                {
                    return false;
                }

                PopulateDatabases(cp, serverNode);
                TreeNode usersNode = CreateUsersNode(cp, serverNode);
                PopulateUsers(cp, usersNode);

                serverNode.Expand();
                if (String.IsNullOrEmpty(cp.Database))
                {
                    SelectedNode = serverNode;
                }

                FireAfterConnectedEvent(cp.Server, conn.ConnectionString);
            }
            finally
            {
                _tvInitializing = false;
            }
            return true;
        }

        public void CreateConnection(ConnectionParams cp, bool wantEmptyScript)
        {
            try
            {
                if (OpenConnection(cp) && wantEmptyScript)
                {
                    CreateScriptEditor();
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex.Message);
                return;
            }
        }

        internal bool CreateNewConnection(bool wantEmptyScript)
        {
            frmConnectionParams frm = new frmConnectionParams(false, true);
            DialogResult dlgRes = frm.ShowDialog();
            if (dlgRes != DialogResult.OK)
            {
                return false;
            }

            try
            {
                OpenConnection(frm.GetCurrentConnectionSpec());
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex.Message);
                return false;
            }

            if (wantEmptyScript)
            {
                CreateScriptEditor();
            }

            return true;
        }

        internal bool CreateNewConnectionFromRepository(bool wantEmptyScript)
        {
            frmConnectionRepository frm = new frmConnectionRepository();
            if (frm.ConnParams == null || frm.ConnParams.Count == 0)
            {
                frm.Dispose();
                frm = null;
                MessageBox.Show("Saved connections list is empty and new connection dialog will be opened.\n"
                  + "NOTE: You can define saved connections by using \"View -> Saved Connections\" menu."
                  , "Warning"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Warning
                  );
                return CreateNewConnection(wantEmptyScript);
            }

            DialogResult dlgRes = frm.ShowDialog();
            if (dlgRes != DialogResult.OK)
            {
                return false;
            }

            bool isFirstConn = true;
            foreach (ConnectionParams cp in frm.SelectedDataSources)
            {
                try
                {
                    OpenConnection(cp);
                    if (wantEmptyScript && isFirstConn)
                    {
                        isFirstConn = false;
                        CreateScriptEditor();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not connect.\n" + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return true;
        }

        private TreeNode GetServerNode(TreeNode node)
        {
            if (node == null || node.Parent == null)
                return node;

            return GetServerNode(node.Parent);
        }

        private void DisconnectFromServer(TreeNode node)
        {
            TreeNode serverNode = GetServerNode(node);
            if (serverNode == null)
            {
                return;
            }

            NodeData data = NodeDataFactory.GetNodeData(serverNode.Tag);
            if (data == null || data.Type != DBObjectType.Server)
            {
                return;
            }

            ConnectionParams cp = data.ConnParams;
            if (cp == null)
                return;

            string connKey = cp.PrepareConnKey();
            if (!_connections.Keys.Contains(connKey))
            {
                return;
            }

            SqlConnection conn = _connections[connKey];
            if (conn.State == ConnectionState.Open)
                conn.Close();

            conn.Dispose();
            _connections.Remove(connKey);
            conn = null;

            try
            {
                tv.BeginUpdate();
                while (serverNode.Nodes.Count > 0)
                    serverNode.Nodes.RemoveAt(0);
            }
            finally
            {
                tv.EndUpdate();
            }

            //serverNode.Nodes.Clear();
            tv.Nodes.Remove(serverNode);
            FireAfterDisconnectedEvent(cp.Server);
        }

        #endregion //Connection Management

        #region Filter Related

        public void ShowFilterDialog(string caption, TreeNode node, NodeData data)
        {
            frmObjectExplorerFilter frm = new frmObjectExplorerFilter();
            if (!String.IsNullOrEmpty(caption))
            {
                frm.Text = "Filter {" + caption + "}";
            }

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
                LoadNodeData(node, true);
            }
        }

        public void ClearNodeFilter(TreeNode node)
        {
            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data == null || !data.Filter.HasValue || String.IsNullOrEmpty(data.Filter.Value.FilterText))
            {
                return;
            }

            Filter f = data.Filter.Value;
            f.FilterText = String.Empty;
            data.Filter = f;
            data.Populated = false;
            LoadNodeData(node, true);

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
                    text = tmpFilter.FilterText;
                    break;
                case FilterType.Wildcards:
                    text = Regex.Escape(tmpFilter.FilterText).Replace(@"\*", ".*").Replace(@"\?", ".");
                    break;
                case FilterType.PlainText:
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

        #endregion //Filter Related

        #region MWTreeView Utilities
        public NodeData GetCurrentSelectedNodeData()
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                return null;
            }
            return NodeDataFactory.GetNodeData(node.Tag);
        }

        private TreeNode AddNode(string key, TreeNodeCollection parent, string name, int imageIndex, int selImageIndex)
        {
            TreeNode result = null;
            string tmpKey = !String.IsNullOrEmpty(key) ? key.ToLowerInvariant() : String.Empty;
            if (parent != null)
            {
                result = parent.Add(tmpKey, name);
            }
            else
            {
                result = tv.Nodes.Add(tmpKey, name);
            }

            result.ImageIndex = imageIndex;
            result.SelectedImageIndex = selImageIndex;
            result.ContextMenuStrip = CreateNodeContextMenu(imageIndex);
            return result;
        }

        private TreeNode AddNode(TreeNodeCollection parent, string name, int imageIndex, int selImageIndex)
        {
            return AddNode(String.Empty, parent, name, imageIndex, selImageIndex);
        }

        private TreeNode AddNode(TreeNodeCollection parent, string name, int imageIndex)
        {
            return AddNode(String.Empty, parent, name, imageIndex, imageIndex);
        }

        private TreeNode AddNode(String key, TreeNodeCollection parent, string name, int imageIndex)
        {
            return AddNode(key, parent, name, imageIndex, imageIndex);
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



        private void CustomDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //TODO: overlay image drawing may come here!
        }

        #endregion //MWMWTreeView Utilities

        #region Populate Explorer With Objects


        private TreeNode AddServerNode(ConnectionParams cp, string serverVersion)
        {
            if (cp == null)
            {
                return null;
            }

            string name = cp.PrepareConnKey();
            name = name.Replace(((Char)29).ToString(), " as ");
            TreeNode node = AddNode(null, String.Format("{0} ({1})", name, serverVersion), DBObjectType.Server);
            NodeData data = NodeDataFactory.Create(cp, cp.Server, DBObjectType.Server, String.Empty, -1, String.Empty);
            node.Tag = data;

            return node;
        }

        private void PopulateDatabases(ConnectionParams cp, TreeNode parentNode)
        {
            if (cp == null || parentNode == null)
            {
                return;
            }
            string connKey = cp.PrepareConnKey();
            if (!_connections.Keys.Contains(connKey))
            {
                return;
            }

            SqlConnection conn = _connections[connKey];
            if (conn.State != ConnectionState.Open)
            {
                throw new InvalidConnectionState("Can not populate databases!");
            }

            NodeData parentData = NodeDataFactory.GetNodeData(parentNode);
            TreeNode sysDbGroupNode = null;
            if (ShowSysDbs)
            {
                sysDbGroupNode = AddGroupingFolder(parentNode, "System Databases", DBObjectType.GroupingFolderB, true, DBObjectType.Server);
                sysDbGroupNode.Nodes.Clear();
            }

            using (SqlDataAdapter ad = new SqlDataAdapter(ResManager.GetDBScript("Script_GetDatabases"), conn))
            {
                DataTable dbs = new DataTable();
                ad.Fill(dbs);
                //DataTable dbs = conn.GetSchema("Databases");

                //dbs.DefaultView.Sort = "database_name";
                dbs.DefaultView.Sort = "name";
                dbs = dbs.DefaultView.ToTable();

                string dbName = String.Empty;
                short compatLevel = -1;

                int dbid = -1;
                TreeNode node = null;

                foreach (DataRow row in dbs.Rows)
                {

                    //dbName = (string)row["database_name"];
                    dbName = (string)row["name"];
                    dbid = (short)row["dbid"];
                    compatLevel = (short)(byte)row["CompatibilityLevel"];

                    if (DBConstants.SystemDbNames.Contains(dbName.ToLowerInvariant()))
                    {
                        if (ShowSysDbs)
                            node = AddNode(sysDbGroupNode.Nodes, dbName, DBObjectType.Database);
                        else
                            continue;
                    }
                    else
                    {
                        node = AddNode(parentNode.Nodes, dbName, DBObjectType.Database);
                    }

                    if (ShowDbCompatLevel)
                        node.Text = String.Format("{0} ({1})", dbName, compatLevel);

                    NodeData data = NodeDataFactory.Create(cp, dbName, DBObjectType.Database, dbName, dbid, String.Empty);
                    data.CompatibilityLevel = compatLevel;
                    data.Populated = true;
                    node.Tag = data;
                    TreeNode tblsNode = AddGroupingFolderY(node, FolderType.Tables, dbName, true, DBObjectType.Database);


                    if (ShowSysTables)
                    {
                        tblsNode.Nodes.Clear();
                        AddGroupingFolderY(tblsNode, FolderType.SystemTables, dbName, true, DBObjectType.Database);
                        AddGroupingFolderY(tblsNode, FolderType.UserTables, dbName, true, DBObjectType.Database);
                    }

                    AddGroupingFolderY(node, FolderType.Views, dbName, true, DBObjectType.Database);
                    AddGroupingFolderY(node, FolderType.Procedures, dbName, true, DBObjectType.Database);

                    TreeNode fnNode = AddGroupingFolderY(node, FolderType.Functions, dbName, true, DBObjectType.Database);
                    fnNode.Nodes.Clear();
                    AddGroupingFolderY(fnNode, FolderType.TableValuedFunctions, dbName, true, DBObjectType.Database);
                    AddGroupingFolderY(fnNode, FolderType.ScalarValuedFunctions, dbName, true, DBObjectType.Database);

                    AddGroupingFolderY(node, FolderType.AllTriggers, dbName, true, DBObjectType.Database);
                    if (data.CompatibilityLevel >= 90)
                        AddGroupingFolderY(node, FolderType.Synonyms, dbName, true, DBObjectType.Database);

                    if (cp.Database.ToLowerInvariant() == dbName.ToLowerInvariant())
                    {
                        node.Expand();
                        SelectedNode = node;
                    }
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
            if (ShowSysTables)
            {
                tblsNode.Nodes.Clear();
                AddGroupingFolderY(tblsNode, FolderType.SystemTables, parentData.DBName, true, DBObjectType.Database);
                AddGroupingFolderY(tblsNode, FolderType.UserTables, parentData.DBName, true, DBObjectType.Database);
            }

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

            string connKey = cp.PrepareConnKey();
            if (!_connections.Keys.Contains(connKey))
            {
                return null;
            }

            SqlConnection conn = _connections[connKey];
            if (conn.State != ConnectionState.Open)
            {
                throw new InvalidConnectionState("Can not populate users!");
            }

            TreeNode usersNode = AddNode(parentNode.Nodes, FolderType.Users, DBObjectType.UsersGroup);
            usersNode.Nodes.Clear();
            NodeData parentData = NodeDataFactory.GetNodeData(parentNode.Tag);
            NodeData usersNodeData = NodeDataFactory.Create(parentData.ConnParams, FolderType.Users, DBObjectType.UsersGroup, parentData.DBName, -1, String.Empty);
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

            string connKey = cp.PrepareConnKey();
            if (!_connections.Keys.Contains(connKey))
            {
                return;
            }

            SqlConnection conn = _connections[connKey];
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

                NodeData userNodeData = NodeDataFactory.Create(usersNodeData.ConnParams, userName, DBObjectType.User, usersNodeData.DBName, -1, String.Empty);
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

            string cKey = parentData.ConnParams.PrepareConnKey();
            if (!_connections.Keys.Contains(cKey))
            {
                return;
            }

            SqlConnection conn = _connections[cKey];
            if (conn.State != ConnectionState.Open)
            {
                throw new InvalidConnectionState("Can not populate table-valued function!");
            }


            string script = String.Empty;
            switch (nodeType)
            {
                case DBObjectType.UserTable:
                    script = ResManager.GetDBScript("Script_GetUserTables");
                    break;
                case DBObjectType.SystemTable:
                    script = ResManager.GetDBScript("Script_GetSystemTables");
                    break;
                case DBObjectType.View:
                    script = ResManager.GetDBScript("Script_GetViews");
                    break;
                case DBObjectType.StoredProc:
                    script = ResManager.GetDBScript("Script_GetStoredProcedures");
                    script = String.Format(script, parentData.CompatibilityLevel);
                    break;
                case DBObjectType.TableValuedFunction:
                    script = ResManager.GetDBScript("Script_GetTableValuedFunctions");
                    script = String.Format(script, parentData.CompatibilityLevel);
                    break;
                case DBObjectType.ScalarValuedFunction:
                    script = ResManager.GetDBScript("Script_GetScalarValuedFunctions");
                    script = String.Format(script, parentData.CompatibilityLevel);
                    break;
                case DBObjectType.Synonym:
                    script = ResManager.GetDBScript("Script_GetSynonyms");
                    break;
                case DBObjectType.Trigger:
                    script = ResManager.GetDBScript("Script_GetAllTriggers");
                    script = String.Format(script, parentData.CompatibilityLevel);
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
            cmd.CommandTimeout = 0;
            SqlDataReader reader = cmd.ExecuteReader();
            int objCount = 0;

            try
            {
                while (reader.Read())
                {
                    string objName = (string)reader["name"];
                    if (regex != null && !regex.Match(objName).Success)
                    {
                        continue;
                    }

                    string objOwner = Utils.IsDbValueValid(reader["owner"]) ? (string)reader["owner"] : String.Empty;
                    string fullObjName = Utils.IsDbValueValid(reader["FullName"]) ? (string)reader["FullName"] : String.Empty;
                    string connKey = parentData.ServerName + "_" + parentData.DBName + ((int)reader["id"]).ToString();

                    TreeNode node = AddNode(connKey, parentNode.Nodes, ShowFullNames ? fullObjName : objName, nodeType);
                    NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, objName, nodeType
                      , parentData.DBName, parentData.DbId, objOwner);

                    nodeData.FullName = fullObjName;
                    nodeData.ID = (int)reader["id"];
                    nodeData.HasParamsOrCols = true;
                    node.Tag = nodeData;
                    nodeData.CompatibilityLevel = parentData.CompatibilityLevel;
                    objCount++;

                    if (nodeType == DBObjectType.Synonym || nodeType == DBObjectType.Trigger)
                        continue;


                    AddNode(node.Nodes, "...", -1);
                }
                parentData.Populated = true;
            }
            finally
            {
                reader.Close();
            }

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


            TreeNode node = AddGroupingFolderY(parentNode, FolderType.Triggers, parentData.DBName
              , false, parentData.Type);

            NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, FolderType.Triggers
              , DBObjectType.GroupingFolderY, parentData.DBName, parentData.DbId
              , String.Empty);
            nodeData.Filter = Filter.CreateFilter();
            nodeData.ID = parentData.ID;
            nodeData.CompatibilityLevel = parentData.CompatibilityLevel;
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

            string connKey = parentData.ConnParams.PrepareConnKey();
            if (!_connections.Keys.Contains(connKey))
            {
                return;
            }

            SqlConnection conn = _connections[connKey];
            if (conn.State != ConnectionState.Open)
            {
                throw new InvalidConnectionState("Can not populate table-valued function!");
            }


            string script = ResManager.GetDBScript("Script_GetColsAndParams");
            script = String.Format(script, parentData.CompatibilityLevel);


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
            cmd.CommandTimeout = 0;
            SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.Int);
            param.Value = parentData.ID;
            SqlDataReader reader = cmd.ExecuteReader();

            Filter? filter = GetFilter(parentNode);
            Regex regex = CreateRegExFromFilter(filter);
            int objCnt = 0;
            try
            {
                while (reader.Read())
                {
                    string pName = (string)reader["name"];
                    string name = (string)reader["name"];

                    //pName = pName + " [ " + (string)reader["typename"] + " (" + ((short)reader["length"]).ToString() + ") " + "]";
                    int len = reader["lengthx"].GetType() == typeof(DBNull) ? (int)0 : (int)reader["lengthx"];
                    short prec = reader["prec"].GetType() == typeof(DBNull) ? (short)0 : (short)reader["prec"];
                    int scale = reader["scale"].GetType() == typeof(DBNull) ? 0 : (int)reader["scale"];

                    pName = pName + " [ " + DBConstants.GetFullyQualifiedDataTypeName(false, (string)reader["typename"], len, scale, prec) + "]";

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

                    NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, name, type, parentData.DBName
                      , parentData.DbId
                      , Utils.IsDbValueValid(reader["owner"]) ? (string)reader["owner"] : String.Empty);
                    nodeData.ID = parentData.ID;
                    nodeData.ParentName = parentData.ParentName;
                    nodeData.CompatibilityLevel = parentData.CompatibilityLevel;
                    node.Tag = nodeData;

                    objCnt++;
                }
                parentData.Populated = true;
            }
            finally
            {
                reader.Close();
            }

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

            string connKey = tableData.ConnParams.PrepareConnKey();

            if (!_connections.Keys.Contains(connKey))
            {
                return;
            }

            SqlConnection conn = _connections[connKey];
            if (conn.State != ConnectionState.Open)
            {
                throw new InvalidConnectionState("Can not populate table-valued function!");
            }

            string script = String.Empty;
            if (ownOnly)
            {
                script = ResManager.GetDBScript("Script_ForeignKeys");
            }
            else
            {
                script = ResManager.GetDBScript("Script_ForeignKeyIn");
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
            cmd.CommandTimeout = 0;

            SqlDataReader reader = cmd.ExecuteReader();
            int objCount = 0;
            string fkInfo = String.Empty;
            try
            {
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

                    NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, fkInfo, DBObjectType.ForeignKey
                      , parentData.DBName, parentData.DbId, String.Empty);
                    nodeData.ID = parentData.ID;
                    nodeData.CompatibilityLevel = parentData.CompatibilityLevel;
                    node.Tag = nodeData;
                    objCount++;
                }
                parentData.Populated = true;
            }
            finally
            {
                reader.Close();
            }

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

            string connKey = parentData.ConnParams.PrepareConnKey();
            if (!_connections.Keys.Contains(connKey))
            {
                return false;
            }

            SqlConnection conn = _connections[connKey];
            if (conn.State != ConnectionState.Open)
            {
                throw new InvalidConnectionState("Can not populate triggers!");
            }


            string script = ResManager.GetDBScript("Script_GetTriggers");
            script = String.Format(script, parentData.CompatibilityLevel);
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
            cmd.CommandTimeout = 0;

            SqlParameter param = cmd.Parameters.Add("@parentid", SqlDbType.Int);
            param.Value = parentData.ID;

            SqlDataReader reader = cmd.ExecuteReader();
            int objCount = 0;

            Filter? filter = GetFilter(parentNode);
            Regex regex = CreateRegExFromFilter(filter);
            try
            {
                while (reader.Read())
                {
                    string objName = (string)reader["name"];
                    if (regex != null && !regex.Match(objName).Success)
                    {
                        continue;
                    }

                    string objOwner = Utils.IsDbValueValid(reader["owner"]) ? (string)reader["owner"] : String.Empty;
                    string fullObjName = Utils.IsDbValueValid(reader["FullName"]) ? (string)reader["FullName"] : String.Empty; // String.Format("{0}{1}", !String.IsNullOrEmpty(objOwner) ? objOwner + "." : String.Empty, objName);

                    TreeNode node = AddNode(parentNode.Nodes, ShowFullNames ? fullObjName : objName, DBObjectType.Trigger);

                    NodeData nodeData = NodeDataFactory.Create(parentData.ConnParams, objName, DBObjectType.Trigger
                      , parentData.DBName, parentData.DbId
                      , objOwner);

                    nodeData.FullName = fullObjName;
                    nodeData.ID = (int)reader["id"];
                    nodeData.CompatibilityLevel = parentData.CompatibilityLevel;

                    node.Tag = nodeData;
                    objCount++;
                }
                parentData.Populated = true;
            }
            finally
            {
                reader.Close();
            }

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

            NodeData data = NodeDataFactory.Create(parentData.ConnParams, name, nodeType, dbName, parentData.DbId, String.Empty);
            data.ConnParams = ((NodeData)parentNode.Tag).ConnParams;
            data.ParentType = parentType;
            data.ID = parentData.ID;
            data.ParentName = parentData.Name;
            data.Filter = Filter.CreateFilter();
            data.CompatibilityLevel = parentData.CompatibilityLevel;

            data.CompatibilityLevel = parentData.CompatibilityLevel;
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
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);

            ConnectionParams tmp = data.ConnParams.CreateCopy();
            tmp.Database = data.DBName;
            frmScriptEditor editor = ScriptEditorFactory.Create(tmp);
            ScriptEditorFactory.ShowScriptEditor(editor);
            return editor;
        }

        public frmScriptEditor CreateScriptEditorForObjectInfo(string procName, string captionPostfix, bool canRun)
        {
            return CreateScriptEditorForObjectInfo(procName, captionPostfix, canRun, false);
        }

        public frmScriptEditor CreateScriptEditorForObjectInfo(string procName, string captionPostfix, bool canRun, bool useFullObjectName)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);

            string script = "EXEC " + procName + "'" + (useFullObjectName ? data.QualifiedFullName : data.Name) + "'";
            string caption = data.Name + " {" + captionPostfix + "}";
            return CreateScriptEditorForObjectInfoEx(data, script, caption);
        }

        public frmScriptEditor CreateScriptEditorForObjectInfo(string procName, string captionPostfix, bool canRun, params string[] parameters)
        {
            return CreateScriptEditorForObjectInfo(procName, captionPostfix, canRun, false, parameters);
        }

        public frmScriptEditor CreateScriptEditorForObjectInfo(string procName, string captionPostfix, bool canRun, bool withNamedParams, params string[] parameters)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }

            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);

            string script = "EXEC " + procName + " ";
            string comma = String.Empty;
            foreach (string p in parameters)
            {
                if (!withNamedParams)
                    script += comma + "'" + p.Replace("[", "").Replace("]", "") + "'";
                else
                    script += comma + p.Replace("[", "").Replace("]", "");
                comma = ", ";
            }

            string caption = data.Name + " {" + captionPostfix + "}";

            return CreateScriptEditorForObjectInfoEx(data, script, caption);
        }

        private static frmScriptEditor CreateScriptEditorForObjectInfoEx(NodeData data, string script, string caption)
        {
            frmScriptEditor editor = ScriptEditorFactory.CreateNew(caption, script, data);
            editor.HideTextEditor();
            ScriptEditorFactory.ShowScriptEditor(editor);
            editor.ExecScript(ScriptRunType.Execute);

            return editor;
        }

        public frmScriptEditor ShowSynonymInfo(bool canRun)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);

            string script = String.Format(ResManager.GetDBScript("Script_GetSynonymInformation"), data.Name);
            string caption = data.Name + " {" + data.ConnParams.InfoDbServer + "}";
            frmScriptEditor editor = ScriptEditorFactory.CreateNew(caption, script, data);
            ScriptEditorFactory.ShowScriptEditor(editor);
            editor.ExecScript(ScriptRunType.Execute);
            editor.Icon = Properties.Resources.Web_StyleSheet;
            return editor;
        }

        public frmScriptEditor CreateScriptEditorForColumnPermissions(string captionPostfix, bool canRun)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data.Type != DBObjectType.Column)
            {
                return null;
            }

            string script = "exec sp_column_privileges '" + data.ParentName + "',NULL,NULL,'" + data.Name + "'";
            string caption = data.ParentName + "." + data.Name + " {" + captionPostfix + "}";
            frmScriptEditor editor = ScriptEditorFactory.Create(caption, script, data);
            editor.HideTextEditor();
            ScriptEditorFactory.ShowScriptEditor(editor);
            editor.ExecScript(ScriptRunType.Execute);

            return editor;
        }

        public frmScriptEditor CreateObjectPermissionsScriptEditor(bool canRun)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);

            string script = ResManager.GetDBScript("Script_Permissions");
            script = script.Replace("$objid$", data.ID.ToString());

            string caption = data.Name + " {PERMISSIONS}";
            frmScriptEditor editor = ScriptEditorFactory.Create(caption, script, data);
            editor.HideTextEditor();
            ScriptEditorFactory.ShowScriptEditor(editor);

            editor.ExecScript(ScriptRunType.Execute);
            //editor.ScriptText = String.Empty;
            //editor.ScriptModified = false;
            return editor;
        }

        public frmScriptEditor OpenFileInNewScriptEditor()
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            frmScriptEditor editor = ScriptEditorFactory.OpenFile(String.Empty, data);
            ScriptEditorFactory.ShowScriptEditor(editor);
            return editor;
        }

        public frmScriptEditor OpenFileInNewScriptEditor(string fileName)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            frmScriptEditor editor = ScriptEditorFactory.OpenFile(fileName, data);
            ScriptEditorFactory.ShowScriptEditor(editor);
            return editor;
        }

        public frmScriptEditor OpenFileInNewScriptEditor2(string fileName)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            frmScriptEditor editor = ScriptEditorFactory.OpenFile(fileName, data);
            return editor;
        }

        public frmScriptEditor NewScriptEditor(string script)
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                CreateNewConnectionFromRepository(false);
            }
            node = SelectedNode;
            if (node == null)
            {
                return null;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            frmScriptEditor editor = ScriptEditorFactory.Create(data);
            editor.ScriptText = script;
            ScriptEditorFactory.ShowScriptEditor(editor);
            return editor;
        }

        private void GenerateTableCrudProceduresToScriptWindow()
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                return;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data == null)
            {
                return;
            }

            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;
            string script = String.Empty;

            DialogResult result = frmCrudGenerator.ShowGenerateCrudDialog(cp, data.Name, data.QualifiedFullName, out script);
            if (result != DialogResult.OK)
            {
                return;
            }

            frmScriptEditor editor = ScriptEditorFactory.Create("CRUD " + data.QualifiedFullName, script, data.Type, data);
            ScriptEditorFactory.ShowScriptEditor(editor);
        }

        private void DropSelectedObjects(bool confirm)
        {
            if (confirm && !MessageService.AskQuestion("Drop selected objects?"))
            {
                return;
            }

            bool hasErrors = false;
            ConnectionParams cp = null;
            SqlCommand cmd = new SqlCommand();

            foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
            {
                NodeData data = NodeDataFactory.GetNodeData(nodeWrapper.Node.Tag);
                if (data == null)
                {
                    continue;
                }

                try
                {
                    cp = data.ConnParams.CreateCopy();
                    cp.Database = data.DBName;
                    string script = ScriptingHelper.GenerateDropScript(cp, data.QualifiedFullName, data.Type, false, false);
                    if (String.IsNullOrEmpty(script))
                    {
                        continue;
                    }

                    using (SqlConnection conn = cp.CreateSqlConnection(true, false))
                    {
                        cmd.CommandText = script;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        HostServicesSingleton.HostServices.MsgService.InfoMsg(String.Format("Dropped ({0}): " + data.Name, DBObjectType.GetNameOfType(data.Type)));
                        nodeWrapper.Node.Remove();
                    }
                }
                catch (Exception ex)
                {
                    if (!hasErrors)
                    {
                        hasErrors = true;
                    }
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex.Message, (MethodInfo)MethodInfo.GetCurrentMethod());
                }
            }

            if (hasErrors)
            {
                HostServicesSingleton.HostServices.MsgService.ShowMessages();
                System.Media.SystemSounds.Exclamation.Play();
                //MessageService.ShowError("Command completed with errors!");
            }


        }

        private void GenerateObjectDropScriptToScriptWindow()
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                return;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data == null)
            {
                return;
            }

            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;
            string script = ScriptingHelper.GenerateDropScript(cp, data.QualifiedFullName, data.Type);
            frmScriptEditor editor = ScriptEditorFactory.CreateNew("DROP " + data.FullName, script, data);
            ScriptEditorFactory.ShowScriptEditor(editor);

        }

        //private void GenerateTableCreateScriptToScriptWindow()
        //{
        //  TreeNode node = SelectedNode;
        //  if (node == null)
        //  {
        //    return;
        //  }

        //  NodeData data = NodeDataFactory.GetNodeData(node.Tag);
        //  if (data == null)
        //  {
        //    return;
        //  }

        //  try
        //  {
        //    string script = String.Empty;
        //    FuzzyWait.ShowFuzzyWait("Generating table create script...");
        //    ConnectionParams cp = data.ConnParams.CreateCopy();
        //    cp.Database = data.DBName;
        //    using (DbObjectScripter scripter = new DbObjectScripter(cp))
        //    {
        //      script = scripter.ScriptUserTable((int)data.ID);
        //    }

        //    frmScriptEditor editor = ScriptEditorFactory.CreateNew("Create " + data.Name, script, data);
        //    ScriptEditorFactory.ShowScriptEditor(editor);
        //  }
        //  catch (Exception ex)
        //  {
        //    FuzzyWait.CloseFuzzyWait();
        //    throw ex;
        //  }
        //  finally
        //  {
        //    FuzzyWait.CloseFuzzyWait();
        //  }
        //}

        private void ViewColumnsOfSelectedObjectInScriptWindow()
        {
            TreeNode node = SelectedNode;
            if (node == null)
            {
                return;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data == null)
            {
                return;
            }

            if ((data.Type != DBObjectType.UserTable) && (data.Type != DBObjectType.SystemTable) && (data.Type != DBObjectType.View))
            {
                return;
            }

            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;
            string script = ProgrammabilityHelper.GetColumnDetails(cp, data.Name);
            frmScriptEditor editor = ScriptEditorFactory.Create("COLUMNS " + data.Name, script, data.Type, data);
            ScriptEditorFactory.ShowScriptEditor(editor);

        }

        private void ExecuteSelectedObjectInScriptWindow()
        {
            TreeNode node = SelectedNode;
            if (node == null)
                return;

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data == null)
                return;

            if (data.Type != DBObjectType.StoredProc)
                return;

            string connKey = data.ConnParams.PrepareConnKey();
            string script = ProgrammabilityHelper.GetProcedureExecuteScript(_connections[connKey], data.Name);
            frmScriptEditor editor = ScriptEditorFactory.Create("EXEC " + data.Name, script, data);
            ScriptEditorFactory.ShowScriptEditor(editor);

        }

        private void ModifySelectedObjectInScriptWindow()
        {
            TreeNode node = SelectedNode;
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
            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;
            string script = ScriptingHelper.GetAlterScript(cp, cp.Database, data.ID, data.Type);
            frmScriptEditor editor = ScriptEditorFactory.Create(data.Name, script, data.Type, data);
            ScriptEditorFactory.ShowScriptEditor(editor);
        }

        private frmObjectChangeHistoryViewer _objChangeHist = null;
        private void ShowObjectChangeHistoryOfSelectedObject()
        {
            TreeNode node = SelectedNode;
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
            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;

            if (_objChangeHist != null)
            {

                if (_objChangeHist.IsSearching)
                {
                    if (MessageService.AskQuestion("Loading object change history data for another object!\r\nDo you want to cancel this operation?"))
                        _objChangeHist.StopSearch();
                    else
                    {
                        ObjectChangeHistoryViewerFactory.ShowViewer(_objChangeHist);
                        return;
                    }
                }

            }
            else
            {
                _objChangeHist = ObjectChangeHistoryViewerFactory.CreateViewer();
                _objChangeHist.FormClosed += new FormClosedEventHandler(_objChangeHist_FormClosed);
            }

            _objChangeHist.SetCriteria(cp.Server, cp.Database, data.Type, data.Name);
            _objChangeHist.PerformSearch();
            ObjectChangeHistoryViewerFactory.ShowViewer(_objChangeHist);
        }

        void _objChangeHist_FormClosed(object sender, FormClosedEventArgs e)
        {
            _objChangeHist = null;
        }


        private void SendSelectedObjectToDiff(bool isSource)
        {
            TreeNode node = SelectedNode;
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
            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;
            string script = ScriptingHelper.GetAlterScript(cp, cp.Database, data.ID, data.Type);

            frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
            if (diffForm == null)
            {
                diffForm = TextDiffFactory.CreateDiff();
            }

            /*
            var sqlHighlightStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("sql");

            diffForm.diffControl.SourceHighlightingStrategy = sqlHighlightStrategy;
            diffForm.diffControl.DestHighlightingStrategy = sqlHighlightStrategy;
            */

            if (isSource)
            {
                diffForm.diffControl.SourceText = script;
                diffForm.diffControl.SourceHeaderText = data.Name;
            }
            else
            {
                diffForm.diffControl.DestText = script;
                diffForm.diffControl.DestHeaderText = data.Name;
            }
            diffForm.Show();
            diffForm.BringToFront();
        }

        private void ModifySelectedObjectInCurrentScriptWindow()
        {
            TreeNode node = SelectedNode;
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

            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;
            string script = ScriptingHelper.GetAlterScript(cp, cp.Database, data.ID, data.Type);
            frmScriptEditor editor = Program.MainForm.GetCurrentScriptEditor();
            if (editor == null)
            {
                //MessageBox.Show("There is not any active script editor to append.\nScript will be opened in a new script editor instead?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                editor = ScriptEditorFactory.Create(data.Name, script, data.Type, data);
                ScriptEditorFactory.ShowScriptEditor(editor);
                return;
            }
            editor.AppendScriptAsCodeBlock(script, data.Name);
        }

        #endregion //Script editor related

        #region Other Methods

        public void CreateDatabaseSearchForm()
        {
            TreeNode node = SelectedNode;
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

            string caption = String.Format("Search Db ({0})", ConnectionParams.CreateInfoDbServer(data.DBName, data.ConnParams));
            frmDbObjectSearch frm = DBObjectSearchFactory.CreateDBObjectSearchForm(data, caption, String.Empty);
            DBObjectSearchFactory.ShowForm(frm);
        }

        public void CreateObjectGroupingForm()
        {
            TreeNode node = SelectedNode;
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

            frmObjectGrouping frm = ObjectGroupingFormFactory.CreateObjectGroupingForm(String.Empty, data.ConnParams, data.DBName);
            ObjectGroupingFormFactory.ShowForm(frm);
        }

        //private void ShowDatabaseObjectScriptDialog( )
        //{
        //  NodeData data = NodeDataFactory.GetNodeData(SelectedNode.Tag);
        //  if (data == null || data.Type != DBObjectType.Database)
        //  {
        //    return;
        //  }

        //  ConnectionParams cp = data.ConnParams.CreateCopy();
        //  cp.Database = data.DBName;
        //  frmDBObjectScripter.ShowScripterDialog(cp);
        //}



        //private void ShowDatabaseObjectScriptDialog()
        //{
        //  NodeData data = NodeDataFactory.GetNodeData(SelectedNode.Tag);
        //  if (data == null || data.Type != DBObjectType.Database)
        //  {
        //    return;
        //  }

        //  ConnectionParams cp = data.ConnParams.CreateCopy();
        //  cp.Database = data.DBName;
        //  BatchScripterDialog.ShowBatchScriptDialog(cp);
        //}

        //private void ShowBulkCopyDataDialog()
        //{
        //  NodeData data = NodeDataFactory.GetNodeData(SelectedNode.Tag);
        //  if (data == null || data.Type != DBObjectType.Database)
        //  {
        //    return;
        //  }

        //  ConnectionParams cp = data.ConnParams.CreateCopy();
        //  cp.Database = data.DBName;
        //  BulkCopyDialog.ShowBulkCopyDialog(cp);
        //}


        private void ShowFilterDialog(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data == null)
            {
                return;
            }

            ShowFilterDialog(node == null ? String.Empty : node.FullPath, node, data);
        }

        private void ShowObjectChangeHistoryViewer(TreeNode node)
        {
            frmObjectChangeHistoryViewer frm = null;
            NodeData data = NodeDataFactory.GetNodeData(node);
            if (data == null)
            {
                frm = ObjectChangeHistoryViewerFactory.CreateViewer();
            }
            else
            {
                frm = ObjectChangeHistoryViewerFactory.CreateViewer(data.ConnParams.Server, data.DBName);
            }

            ObjectChangeHistoryViewerFactory.ShowViewer(frm);
        }

        public IList<ConnectionParams> EnumerateConnections()
        {
            IList<ConnectionParams> result = new List<ConnectionParams>();
            ConnectionParams cp = null;
            foreach (SqlConnection conn in _connections.Values)
            {
                cp = new ConnectionParams();
                cp.Server = conn.DataSource;
                cp.Database = conn.Database;
                result.Add(cp);
            }
            return result;
        }

        #endregion

        #region AddIn Support
        public void InitializeAddInSupport()
        {
            ToolStrip toolbar = ToolbarService.CreateToolStrip(this, "/Workspace/ObjectExplorer/Toolbar");
            if (toolbar.Items.Count == 0)
            {
                toolbar.Dispose();
                toolbar = null;
            }
            else
            {
                this.Controls.Add(toolbar);
                toolbar.GripStyle = ToolStripGripStyle.Hidden;
                toolbar.RenderMode = ToolStripRenderMode.ManagerRenderMode;
                toolbar.Dock = DockStyle.Top;
                toolbar.BringToFront();
                tv.BringToFront();
            }
        }

        #endregion //AddIn Support

        #region IObjectExplorer Members

        private ObjectExplorerNode CreateObjectExplorerNodeData(NodeData data)
        {
            ObjectExplorerNode node = new ObjectExplorerNode();
            node.id = data.ID;
            node.Name = data.Name;
            node.ParentName = data.ParentName;
            node.ParentType = data.ParentType;
            node.ServerName = data.ServerName;
            node.DatabaseName = data.DBName;
            node.DbId = data.DbId;
            node.Type = data.Type;
            node.ConnParams = data.ConnParams;
            node.Owner = data.Owner;
            node.Node = SelectedNode;
            node.DbCompatibilityLevel = data.CompatibilityLevel;
            return node;
        }

        public ObjectExplorerNode SelNode
        {
            get
            {
                NodeData data = NodeDataFactory.GetNodeData(SelectedNode);
                if (data == null)
                {
                    return null;
                }
                return CreateObjectExplorerNodeData(data);
            }
        }

        public short DbCompatibilityLevel
        {
            get
            {
                ObjectExplorerNode node = HostServicesSingleton.HostServices.ObjectExplorerService.SelNode;
                return node == null ? (short)-1 : node.DbCompatibilityLevel;
            }
        }


        public IList<ObjectExplorerNode> SelNodes
        {
            get
            {
                IList<ObjectExplorerNode> result = new List<ObjectExplorerNode>();
                foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
                {
                    NodeData data = NodeDataFactory.GetNodeData(nodeWrapper.Node);
                    if (data == null)
                    {
                        continue;
                    }
                    result.Add(CreateObjectExplorerNodeData(data));
                }
                return result;
            }
        }

        public IList<TreeNode> SelectedNodes
        {
            get
            {
                IList<TreeNode> result = new List<TreeNode>();
                foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
                {
                    result.Add(nodeWrapper.Node);
                }
                return result;
            }
        }

        public void ChangeObjectID(string serverName, string databaseName, long oldId, long newId)
        {
            string key = serverName + "_" + databaseName + oldId.ToString();
            TreeNode[] matchingNode = tv.Nodes.Find(key, true);
            TreeNode node = null;
            NodeData data = null;
            for (int i = 0; i < matchingNode.Length; i++)
            {
                node = matchingNode[i];
                data = NodeDataFactory.GetNodeData(node);
                if (data != null && data.ID != newId)
                {
                    data.ID = newId;
                    LoadNodeData(matchingNode[i], true);
                }
            }
        }

        public void ChangeObjectName(TreeNode node, string name)
        {
            NodeData nodeData = NodeDataFactory.GetNodeData(node);
            nodeData.Name = name;
            nodeData.FullName = String.Format("{0}.{1}", nodeData.Owner, nodeData.Name);

            node.Text = ShowFullNames ? nodeData.FullName : nodeData.Name;
        }


        public void ChangeObjectId(TreeNode node, long? id)
        {
            NodeData nodeData = NodeDataFactory.GetNodeData(node);
            if (id.HasValue)
                nodeData.ID = id.Value;
        }



        public void LoadNodeData(TreeNode node, bool forceRefresh)
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

                    if (data.Name == FolderType.Tables && ShowSysTables)
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
                    else if (data.Name == FolderType.SystemTables && ShowSysTables)
                    {
                        node.Nodes.Clear();
                        PopulateObjects(node, DBObjectType.SystemTable);
                    }
                    else if (data.Name == FolderType.UserTables && ShowSysTables)
                    {
                        node.Nodes.Clear();
                        PopulateObjects(node, DBObjectType.UserTable);
                    }
                    else if (data.Name == FolderType.Tables && !ShowSysTables)
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
                    else if (data.Name == FolderType.Synonyms)
                    {
                        node.Nodes.Clear();
                        PopulateObjects(node, DBObjectType.Synonym);
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
                    else if (data.Name == FolderType.AllTriggers)
                    {
                        node.Nodes.Clear();
                        PopulateObjects(node, DBObjectType.Trigger);
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
                    try
                    {
                        _tvInitializing = true;
                        if (data.Populated && !forceRefresh)
                        {
                            return;
                        }

                        node.Nodes.Clear();
                        PopulateDatabases(data.ConnParams, node);
                        TreeNode usersNode = CreateUsersNode(data.ConnParams, node);
                        PopulateUsers(data.ConnParams, usersNode);
                    }
                    finally
                    {
                        _tvInitializing = false;
                    }
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

                _tvInitializing = true;
                if (oldExpandedState)
                {
                    node.Expand();
                }
                _tvInitializing = false;

            }
            finally
            {
                tv.EndUpdate();

                Cursor = Cursors.Default;
            }

        }

        public void ExecuteCommand(ObjectExplorerCommand cmd)
        {
            switch (cmd)
            {
                case ObjectExplorerCommand.CloseCurrentConnection:
                    DisconnectFromServer(SelectedNode);
                    break;
                case ObjectExplorerCommand.NewConnection:
                    CreateNewConnection(false);
                    break;
                case ObjectExplorerCommand.NewConnectionFromList:
                    CreateNewConnectionFromRepository(false);
                    break;
                case ObjectExplorerCommand.NewScript:
                    CreateScriptEditor();
                    break;
                case ObjectExplorerCommand.OpenFile:
                    OpenFileInNewScriptEditor();
                    break;
                case ObjectExplorerCommand.RefreshCurrentNode:
                    LoadNodeData(SelectedNode, true);
                    break;
                case ObjectExplorerCommand.ShowDatabaseSearch:
                    CreateDatabaseSearchForm();
                    break;
                case ObjectExplorerCommand.ShowFilterDialog:
                    ShowFilterDialog(SelectedNode);
                    break;
                case ObjectExplorerCommand.ShowObjectChangeHistoryViewer:
                    ShowObjectChangeHistoryViewer(SelectedNode);
                    break;
                case ObjectExplorerCommand.ShowObjectGrouping:
                    CreateObjectGroupingForm();
                    break;
                default:
                    break;
            }
        }
        public void ShowObjectExplorer()
        {
            Program.MainForm.ShowObjectExplorer();
        }

        private event EventHandler _afterSelectedNodesChanged;
        public event EventHandler AfterSelectedNodesChanged
        {
            add { _afterSelectedNodesChanged += value; }
            remove { _afterSelectedNodesChanged += value; }
        }
        private void FireAfterSelectedNodesChanged()
        {
            if (_afterSelectedNodesChanged == null)
            {
                return;
            }
            Delegate[] delegates = _afterSelectedNodesChanged.GetInvocationList();
            foreach (EventHandler del in delegates)
            {
                try
                {
                    del.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }


        private event AfterConnectedDelegate _afterConnected;
        public event AfterConnectedDelegate AfterConnected
        {
            add { _afterConnected += value; }
            remove { _afterConnected -= value; }
        }
        private void FireAfterConnectedEvent(string serverName, string connString)
        {
            if (_afterConnected == null)
            {
                return;
            }

            Delegate[] delegates = _afterConnected.GetInvocationList();
            foreach (AfterConnectedDelegate del in delegates)
            {
                try
                {
                    del.Invoke(serverName, connString);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        private event AfterDisconnectedDelegate _afterDisconnected;
        public event AfterDisconnectedDelegate AfterDisconnected
        {
            add { _afterDisconnected += value; }
            remove { _afterDisconnected -= value; }
        }
        private void FireAfterDisconnectedEvent(string serverName)
        {
            if (_afterDisconnected == null)
            {
                return;
            }

            Delegate[] delegates = _afterDisconnected.GetInvocationList();
            foreach (AfterDisconnectedDelegate del in delegates)
            {
                try
                {
                    del.Invoke(serverName);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        private event AfterContextMenuActionExecutedDelegate _afterContextMenuActionExecuted;
        public event AfterContextMenuActionExecutedDelegate AfterContextMenuActionExecuted
        {
            add { _afterContextMenuActionExecuted += value; }
            remove { _afterContextMenuActionExecuted -= value; }
        }
        private void FireAfterContextMenuActionExecuted(object sender, ObjectExplorerAction action)
        {
            if (_afterContextMenuActionExecuted == null)
            {
                return;
            }

            ToolStripItem tsItem = sender as ToolStripItem;
            if (_afterContextMenuActionExecuted == null || tsItem == null)
            {
                return;
            }

            ObjectExplorerNode node = null;
            NodeData data = NodeDataFactory.GetNodeData(SelectedNode);
            if (data != null)
            {
                node = new ObjectExplorerNode();
                node.id = data.ID;
                node.Name = data.Name;
                node.ParentName = data.ParentName;
                node.ParentType = data.ParentType;
                node.ServerName = data.ServerName;
                node.DatabaseName = data.DBName;
                node.Type = data.Type;
            }

            string itemPath = GetToolStripItemPath(tsItem);
            AfterContextMenuActionExecutedEventArgs args = new AfterContextMenuActionExecutedEventArgs();
            args.SelectedNode = node;
            args.ActionPath = itemPath;
            args.Action = action;


            Delegate[] delegates = _afterContextMenuActionExecuted.GetInvocationList();
            foreach (AfterContextMenuActionExecutedDelegate del in delegates)
            {
                try
                {
                    del.Invoke(this, args);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }

        private event BeforeContextMenuActionExecutedDelegate _beforeContextMenuActionExecuted;
        public event BeforeContextMenuActionExecutedDelegate BeforeContextMenuActionExecuted
        {
            add { _beforeContextMenuActionExecuted += value; }
            remove { _beforeContextMenuActionExecuted -= value; }
        }
        private void FireBeforeContextMenuActionExecuted(object sender, ObjectExplorerAction action)
        {
            ToolStripItem tsItem = sender as ToolStripItem;
            if (_beforeContextMenuActionExecuted == null || tsItem == null)
            {
                return;
            }

            ObjectExplorerNode node = null;
            NodeData data = NodeDataFactory.GetNodeData(SelectedNode);
            if (data != null)
            {
                node = new ObjectExplorerNode();
                node.id = data.ID;
                node.Name = data.Name;
                node.ParentName = data.ParentName;
                node.ParentType = data.ParentType;
                node.ServerName = data.ServerName;
                node.DatabaseName = data.DBName;
                node.Type = data.Type;
            }

            string itemPath = GetToolStripItemPath(tsItem);

            Delegate[] delegates = _beforeContextMenuActionExecuted.GetInvocationList();
            foreach (BeforeContextMenuActionExecutedDelegate del in delegates)
            {
                try
                {
                    BeforeContextMenuActionExecutedEventArgs args = new BeforeContextMenuActionExecutedEventArgs();
                    args.SelectedNode = node;
                    args.ActionPath = itemPath;
                    args.Action = action;
                    del.Invoke(this, args);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }
        private string GetToolStripItemPath(ToolStripItem tsItem)
        {
            string result = String.Empty;
            if (tsItem == null)
            {
                return result;
            }
            else
            {
                string tmp = GetToolStripItemPath(tsItem.OwnerItem);
                result = String.IsNullOrEmpty(tmp) ? tsItem.Text : tmp + "\\" + tsItem.Text;
                return result;
            }
        }

        private event EventHandler _afterObjectExplorerClosed;
        public event EventHandler AfterObjectExplorerClosed
        {
            add { _afterObjectExplorerClosed += value; }
            remove { _afterObjectExplorerClosed -= value; }
        }
        protected void FireAfterObjectExplorerClosed()
        {
            if (_afterObjectExplorerClosed == null)
            {
                return;
            }

            Delegate[] delegates = _afterObjectExplorerClosed.GetInvocationList();
            foreach (EventHandler del in delegates)
            {
                try
                {
                    del.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
                    HostServicesSingleton.HostServices.MsgService.ShowMessages();
                }
            }
        }


        #endregion


        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnNewConnectionClick(sender, e);
        }

        private void connectionFromRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnNewConnectionFromRepositoryClick(sender, e);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDisconnectClick(sender, e);
        }

        private void tv_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            LoadNodeData(e.Node, false);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OnRefreshNodeClick(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OnCreateScriptEditorClick(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OnOpenFileInNewScriptEditor(sender, e);
        }

        private void tv_DoubleClick(object sender, EventArgs e)
        {
            OnModifyClick(sender, e);
        }

        private void frmObjectExplorer_Shown(object sender, EventArgs e)
        {
            /*
            if (_isInitialShow)
            {
              bool showBrowser = false;
              if (ConfigHelper.Current != null && ConfigHelper.Current.GeneralOptions != null)
              {
                showBrowser = ConfigHelper.Current.GeneralOptions.WebBrowser_ShowonStart;
              }

              if (!showBrowser)
              {
                CreateNewConnectionFromRepository(true);
              }
              _isInitialShow = false;
            }
            */
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                edtPath.Text = String.Empty;
                return;
            }


            TreeNode node = e.Node;
            NodeData data = NodeDataFactory.GetNodeData(node.Tag);
            if (data == null)
                return;

            edtPath.Text = e.Node.FullPath;
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

            FireAfterSelectedNodesChanged();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            OnFilterClick(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OnCreateDatabaseSearchForm(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OnCreateObjectGroupingForm(sender, e);
        }

        private void tv_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (CanDragSelectedNode())
            {
                tv.DoDragDrop(tv.SelNodes, DragDropEffects.Copy);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            OnShowObjectChangeHistoryViewer(sender, e);
        }

        private void frmObjectExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            FireAfterObjectExplorerClosed();
        }

        private void tv_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (this.SelectedNodes.Count <= 1)
                {
                    TreeNode selNode = this.SelectedNode;
                    tv.DeselectNode(selNode, false);

                    TreeNode node = tv.GetNodeAt(e.X, e.Y);
                    if (node != null)
                    {
                        this.SelectedNode = node;
                    }
                }
            }
        }


    }
}