using System;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

using WeifenLuo.WinFormsUI.Docking;
using MWCommon;
using ICSharpCode.Core;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public partial class frmProjectExplorer : DockContent, IProjectExplorerServices
  {
    #region Fields And Properties
    private bool _isInitializing = false;
    private TreeNode _projectNode = null;
    private Project _project = null;
    private IDictionary<string, ConnectionParams> _confirmedConnections = new Dictionary<string, ConnectionParams>();

    public TreeNode SelectedNode
    {
      get
      {
        return tv.SelNode;
      }
      set
      {
        tv.SelectNode(value, true);
      }
    }
    public ProjectNodeData SelectedNodeData
    {
      get
      {
        return GetNodeData(SelectedNode);
      }
    }

    #endregion //Fields And Properties

    public frmProjectExplorer( )
    {
      InitializeComponent();
      InitializeActions();
      BuildTreeViewMenu();
      tv.DoubleClick += new EventHandler(tv_DoubleClick);
      tv.TreeViewNodeSorter = new ProjectNodeSorter();
    }


    #region Tree Utilities

    private TreeNode AddProjectNode( Project prj )
    {
      if (prj == null)
      {
        return null;
      }

      TreeNode result = tv.Nodes.Add(prj.Name);
      result.ImageIndex = 0;
      result.SelectedImageIndex = result.ImageIndex;
      ProjectNodeData data = ProjectNodeDataFactory.Create(prj);
      result.Tag = data;
      _projectNode = result;
      _projectNode.ContextMenuStrip = BuildContextMenu(_projectNode);
      tv.Sort();
      return result;
    }

    private TreeNode AddNode( TreeNode parentNode, ProjectItem item )
    {
      if (item == null)
      {
        return null;
      }

      int imageIndex = -1;
      switch (item.ItemType)
      {
        case ProjectItemType.Unknown:
          imageIndex = 1;
          break;
        case ProjectItemType.ConnectionSpec:
          imageIndex = 2;
          break;
        case ProjectItemType.ScriptFile:
          imageIndex = 3;
          break;
        case ProjectItemType.TextFile:
          imageIndex = GetExternalFileImageIndex(item);
          break;
        case ProjectItemType.ExternalFile:
          imageIndex = GetExternalFileImageIndex(item);
          break;
        case ProjectItemType.Folder:
          imageIndex = 5;
          break;
        case ProjectItemType.ScriptableDBObject:
        case ProjectItemType.NonScriptableDBObject:
          imageIndex = item.DbObjectType + 4;
          break;
        default:
          break;
      }

      TreeNode result = null;
      if (parentNode == null)
      {
        result = tv.Nodes.Add(item.Name);
      }
      else
      {
        result = parentNode.Nodes.Add(item.Name);
      }

      ProjectNodeData data = ProjectNodeDataFactory.Create(_project, item);
      result.Tag = data;
      result.ImageIndex = imageIndex;
      result.SelectedImageIndex = result.ImageIndex;
      result.ContextMenuStrip = BuildContextMenu(result);
      tv.Sort();
      return result;
    }

    private int GetExternalFileImageIndex( ProjectItem item )
    {
      int result = 4;
      string fullName = item.GetFullFileName(_project);
      if (!imageList1.Images.ContainsKey(item.Extension))
      {
        imageList1.Images.Add(item.Extension, IconExtractor.GetFileIcon(fullName, IconSize.Small));
      }
      if (imageList1.Images.ContainsKey(item.Extension))
      {
        result = imageList1.Images.IndexOfKey(item.Extension);
      }
      return result;
    }

    private TreeNode AddEmptyNode( TreeNode parentNode )
    {
      TreeNode result = null;
      if (parentNode == null)
      {
        result = tv.Nodes.Add(String.Empty);
      }
      else
      {
        result = parentNode.Nodes.Add(String.Empty);
      }

      result.Tag = new ProjectNodeData();
      result.ImageIndex = -1;
      result.SelectedImageIndex = -1;
      tv.Sort();
      return result;
    }

    private ProjectNodeData GetSelectedNodeData( )
    {
      if (tv.SelNode == null)
      {
        return null;
      }

      return tv.SelNode.Tag as ProjectNodeData;
    }

    private ProjectNodeData GetNodeData( TreeNode node )
    {
      if (node == null || node.Tag == null)
      {
        return null;
      }
      return node.Tag as ProjectNodeData;
    }

    private void RefreshNode( TreeNode node, bool forceRefresh )
    {
      if (_isInitializing)
      {
        return;
      }

      TreeNode nd = null;
      ProjectNodeData data = null;

      if (node == null)
      {
        nd = tv.SelNode;
      }
      else
      {
        nd = node;
      }

      data = GetNodeData(nd);
      if (data == null || (data.Project == null && data.Item == null))
      {
        return;
      }
      if (data.IsPopulated && !forceRefresh)
      {
        return;
      }

      try
      {
        _isInitializing = true;
        LoadChildren(nd);
      }
      finally
      {
        _isInitializing = false;
      }
    }

    private void LoadChildren( TreeNode node )
    {
      if (node == null)
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        node.Nodes.Clear();
        ProjectNodeData data = GetNodeData(node);
        if (data == null)
        {
          return;
        }

        if (data.ItemType == ProjectItemType.Project)
        {
          foreach (ProjectItem item in _project.ConnectedItems.Values)
          {
            TreeNode nd = AddNode(_projectNode, item);
            if (item.ItemType == ProjectItemType.Folder || item.ItemType == ProjectItemType.ConnectionSpec)
            {
              AddEmptyNode(nd);
            }
          }

          foreach (ProjectItem item in _project.DisconnectedItems.Values)
          {
            TreeNode nd = AddNode(_projectNode, item);
            if (item.ItemType == ProjectItemType.Folder || item.ItemType == ProjectItemType.ConnectionSpec)
            {
              AddEmptyNode(nd);
            }
          }
          _projectNode.Expand();
          data.IsPopulated = true;
        }
        else
        {
          foreach (ProjectItem item in data.Item.Children.Values)
          {
            TreeNode nd = AddNode(node, item);
            if (item.ItemType == ProjectItemType.Folder || item.ItemType == ProjectItemType.ConnectionSpec)
            {
              AddEmptyNode(nd);
            }
          }
          node.Expand();
          data.IsPopulated = true;
        }
      }
      finally
      {
        tv.EndUpdate();
      }
    }

    #endregion

    #region Connection Confirmation

    private bool ConfirmConnection( ConnectionParams cp )
    {
      if (cp == null)
      {
        throw new InvalidProgramException("ConnectionParams parameter is null!");
      }

      if (_confirmedConnections.ContainsKey(cp.ID.ToString()))
      {
        return true;
      }

      frmConnectionParams frm = new frmConnectionParams(cp, false, true);

			frm.LogonOnly = true;
      frm.ResetLogOnInformation();
      if (frm.ShowDialog() != DialogResult.OK)
      {
        return false;
      }
      else
      {
        _confirmedConnections.Add(cp.ID.ToString(), cp);
        return true;
      }
    }


    #endregion

    #region Methods

    private void CreateNewProject( )
    {
      if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      string prjName = String.Empty;
      if (InputDialog.ShowDialog("New Project", "Project Name", ref prjName) != DialogResult.OK || String.IsNullOrEmpty(prjName))
      {
        return;
      }
      string fullPath = folderBrowserDialog1.SelectedPath + @"\" + prjName + ".sqlprj";
      if (File.Exists(fullPath))
      {
        string errStr = String.Format("Project with name \"{0}\" already exists!\nFolder Name:{1}", prjName, folderBrowserDialog1.SelectedPath);
        MessageBox.Show(errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      Project prj = new Project();
      prj.CreatedBy = SystemInformation.UserName;
      prj.CreatedOn = DateTime.Now;
      prj.Name = prjName;
      prj.FullPath = fullPath;

      ProjectFactory.SaveProject(prj);
      _project = ProjectFactory.LoadProject(prj.FullPath);

      lblProjectFileInfo.Text = prj.FullPath;
      lblProjectFileInfo.ToolTipText = lblProjectFileInfo.Text;


      tv.Nodes.Clear();
      AddProjectNode(_project);
      tv.SelectNode(_projectNode, true);
    }

    private IList<ProjectItem> AddConnectionFromList( )
    {

      IList<ProjectItem> items = new List<ProjectItem>();
      if (_project == null)
      {
        return items;
      }

      ProjectItem item = null;
      IList<ConnectionParams> cpList = frmConnectionRepository.SelectConnection(true);
      bool addedItem = false;

      try
      {
        tv.BeginUpdate();
        foreach (ConnectionParams cp in cpList)
        {
          if (_project.ConnectedItems.ContainsKey(cp.ID.ToString()))
          {
            continue;
          }
          item = ProjectItemFactory.CreateConnectionSpec(_project, cp);
          TreeNode node = AddNode(_projectNode, item);
          AddEmptyNode(node);
          addedItem = true;
        }

        if (addedItem)
        {
          ProjectFactory.SaveProject(_project);
          if (_projectNode != null)
          {
            _projectNode.Expand();
          }
        }
      }
      finally
      {
        tv.EndUpdate();
      }
      return items;
    }

    private ProjectItem AddNewConnection( )
    {
      if (_project == null)
      {
        return null;
      }

      ConnectionParams cp = frmConnectionParams.CreateConnection();
      if (cp == null)
      {
        return null;
      }

      ProjectItem item = ProjectItemFactory.CreateConnectionSpec(_project, cp);
      TreeNode node = AddNode(_projectNode, item);
      AddEmptyNode(node);
      if (_projectNode != null)
      {
        _projectNode.Expand();
      }

      ProjectFactory.SaveProject(_project);
      return item;
    }

		private void InternalOpenProject(string fileName)
		{
			tv.Nodes.Clear();
			_confirmedConnections.Clear();
			_project = ProjectFactory.LoadProject(fileName);

			lblProjectFileInfo.Text = _project.FullPath;
			lblProjectFileInfo.ToolTipText = lblProjectFileInfo.Text;

			AddProjectNode(_project);
			foreach (ProjectItem item in _project.ConnectedItems.Values)
			{
				TreeNode node = AddNode(_projectNode, item);

				if (item.ItemType == ProjectItemType.Folder || item.ItemType == ProjectItemType.ConnectionSpec)
				{
					AddEmptyNode(node);
				}
			}

			foreach (ProjectItem item in _project.DisconnectedItems.Values)
			{
				TreeNode node = AddNode(_projectNode, item);
				if (item.ItemType == ProjectItemType.Folder || item.ItemType == ProjectItemType.ConnectionSpec)
				{
					AddEmptyNode(node);
				}
			}
			_projectNode.Expand();
			tv.SelectNode(_projectNode, true);

			Program.MainForm.AddFileToMru(fileName);
		}


    public bool OpenProject( )
    {
      if (openFileDialog1.ShowDialog() != DialogResult.OK)
      {
        return false;
      }

      InternalOpenProject(openFileDialog1.FileName);
      return true;
    }

    private void AddFolder( )
    {
      ProjectNodeData selNodeData = GetSelectedNodeData();
      if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
      {
        return;
      }

      if (selNodeData.ItemType != ProjectItemType.Project
        && selNodeData.ItemType != ProjectItemType.Folder
        && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
      {
        return;
      }


      string parentPath = String.Empty;
      string folderName = "New Folder";
      if (InputDialog.ShowDialog("New Folder", "Folder Name", ref folderName) != DialogResult.OK || String.IsNullOrEmpty(folderName))
      {
        return;
      }

      if (selNodeData.ItemType == ProjectItemType.Project)
      {
        parentPath = selNodeData.Project.GetDirectory();
      }
      else
      {
        parentPath = selNodeData.Item.GetFullPath(_project);
      }


      string fullPath = parentPath + @"\" + folderName;
      if (Directory.Exists(fullPath))
      {
        string errStr = String.Format("Folder with name \"{0}\" already exists!\nParent Folder: {1}", folderName, parentPath);
        MessageBox.Show(errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      Directory.CreateDirectory(fullPath);

      ProjectItem item = null;
      if (selNodeData.ItemType == ProjectItemType.Project)
      {
        item = ProjectItemFactory.CreateFolder(null, folderName);
        _project.DisconnectedItems.Add(item.Uid.ToString(), item);
      }
      else
      {
        item = ProjectItemFactory.CreateFolder(selNodeData.Item, folderName);
      }

      ProjectFactory.SaveProject(_project);

      TreeNode node = AddNode(tv.SelNode, item);
      AddEmptyNode(node);
      tv.SelNode.Expand();
    }

    private void AddContentFile( ProjectItemType itemType )
    {
      if (itemType != ProjectItemType.ScriptFile && itemType != ProjectItemType.TextFile)
      {

        throw new Exception(String.Format("Invalid item type \"{0}\". Valid item types are ScriptFile and TextFile.", itemType));
      }
      ProjectNodeData selNodeData = GetSelectedNodeData();
      if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
      {
        return;
      }

      if (selNodeData.ItemType != ProjectItemType.Project
        && selNodeData.ItemType != ProjectItemType.Folder
        && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
      {
        return;
      }


      string parentPath = String.Empty;
      string itemName = "New Item";
      string dlgCaption = String.Empty;
      string caption = String.Empty;
      string fileExt = String.Empty;

      if (itemType == ProjectItemType.ScriptFile)
      {
        dlgCaption = "New Script File";
        caption = "Script File Name";
        fileExt = ".sql";
      }
      else if (itemType == ProjectItemType.TextFile)
      {
        dlgCaption = "New Text File";
        caption = "Text File Name";
        fileExt = ".txt";
      }

      if (InputDialog.ShowDialog(dlgCaption, caption, ref itemName) != DialogResult.OK || String.IsNullOrEmpty(itemName))
      {
        return;
      }

      if (selNodeData.ItemType == ProjectItemType.Project)
      {
        parentPath = selNodeData.Project.GetDirectory();
      }
      else
      {
        parentPath = selNodeData.Item.GetFullPath(_project);
      }


      string fullPath = parentPath + @"\" + itemName + fileExt;

      if (File.Exists(fullPath))
      {
        string errStr = String.Format("File with name \"{0}\" already exists!\nFolder: {1}", itemName, parentPath);
        MessageBox.Show(errStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      File.Create(fullPath);

      ProjectItem item = null;

      if (itemType == ProjectItemType.ScriptFile)
      {
        item = ProjectItemFactory.CreateScriptFile(selNodeData.Item, fullPath);
      }
      else if (itemType == ProjectItemType.TextFile)
      {
        item = ProjectItemFactory.CreateTextFile(selNodeData.Item, fullPath);
      }

      if (selNodeData.ItemType == ProjectItemType.Project)
      {
        _project.DisconnectedItems.Add(item.Uid.ToString(), item);
      }

      ProjectFactory.SaveProject(_project);
      TreeNode node = AddNode(tv.SelNode, item);
      tv.SelNode.Expand();
    }


    private void AddExternalFile( )
    {
      string errStr = String.Empty;
      ProjectNodeData selNodeData = GetSelectedNodeData();
      if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
      {
        return;
      }

      if (selNodeData.ItemType != ProjectItemType.Project
        && selNodeData.ItemType != ProjectItemType.Folder
        && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
      {
        return;
      }

      if (openFileDialog2.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      string parentPath = String.Empty;
      if (selNodeData.ItemType == ProjectItemType.Project)
      {
        parentPath = selNodeData.Project.GetDirectory();
      }
      else
      {
        parentPath = selNodeData.Item.GetFullPath(_project);
      }

      string fullPath = String.Empty;
      string[] fileNames = openFileDialog2.FileNames;
      foreach (string fileName in fileNames)
      {
        FileInfo fi = new FileInfo(fileName);
        fullPath = parentPath + @"\" + fi.Name;
        if (File.Exists(fullPath))
        {
          // Skip if file already exists!
          errStr += "- File already exists! \"" + fullPath + "\"." + "\r\n";
          continue;
        }

        File.Copy(fileName, fullPath);
        ProjectItem item = null;
        if (selNodeData.ItemType == ProjectItemType.Project)
        {
          item = ProjectItemFactory.CreateExternalFile(null, fullPath);
          _project.DisconnectedItems.Add(item.Uid.ToString(), item);
        }
        else
        {
          item = ProjectItemFactory.CreateExternalFile(selNodeData.Item, fullPath);
        }
        TreeNode node = AddNode(tv.SelNode, item);
      }

      ProjectFactory.SaveProject(_project);
      tv.SelNode.Expand();

      if (!String.IsNullOrEmpty(errStr))
      {
        GenericErrorDialog.ShowError("Error", "Can not add file to project.", errStr);
      }
    }

    private void CloseProject( )
    {
      if (_project == null)
      {
        return;
      }

      _project = null;
      tv.Nodes.Clear();
      _projectNode = null;
      _confirmedConnections.Clear();

      lblProjectFileInfo.Text = "Project File:";
      lblProjectFileInfo.ToolTipText = lblProjectFileInfo.Text;

    }

    private void RenameItem( )
    {
      ProjectNodeData data = GetSelectedNodeData();
      if (data == null)
      {
        return;
      }

      if (data.ItemType == ProjectItemType.ConnectionSpec
        || data.ItemType == ProjectItemType.Project
        || data.ItemType == ProjectItemType.Unknown
        )
      {
        return;
      }


      string newName = data.Item.Name;
      string oldName = newName;
      if (InputDialog.ShowDialog("Rename Item", "Item Name", ref newName) != DialogResult.OK || String.IsNullOrEmpty(newName))
      {
        return;
      }

      if (oldName.Trim().ToLowerInvariant() == newName.Trim().ToLowerInvariant())
      {
        return;
      }

      data.Item.RenameItem(_project, newName);
      TreeNode node = tv.SelNode;
      node.Text = newName;

      ProjectFactory.SaveProject(_project);
    }

    private void RenameProject( )
    {
      ProjectNodeData data = GetSelectedNodeData();
      if (data == null)
      {
        return;
      }

      if (data.ItemType != ProjectItemType.Project)
      {
        return;
      }

      string newName = data.Project.Name;
      if (InputDialog.ShowDialog("Rename Project", "Project Name", ref newName) != DialogResult.OK || String.IsNullOrEmpty(newName))
      {
        return;
      }

      _project.Name = newName;
      ProjectFactory.SaveProject(_project);

      string newFullPath = _project.GetDirectory() + @"\" + newName + ".sqlprj";
      File.Move(_project.FullPath, newFullPath);
      //File.Delete(_project.FullPath);

      InternalOpenProject(newFullPath);
    }

    private void RenameSelectedItem( )
    {
      ProjectNodeData data = GetSelectedNodeData();
      if (data == null)
      {
        return;
      }

      if (data.ItemType == ProjectItemType.Project)
      {
        RenameProject();
      }
      else
      {
        RenameItem();
      }
    }

    private void DeleteSelectedItems( )
    {
      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Selected items and associated files will be removed permanently from the project!\n\nDo you want to delete selected item(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dlgRes == DialogResult.No)
      {
        return;
      }
      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        DeleteItem(nodeWrapper.Node);
      }

      tv.ClearSelNodes();
      tv.DeselectNode(SelectedNode, true);
    }

    private void DeleteItem( TreeNode node )
    {
      if (node == null)
      {
        return;
      }

      ProjectNodeData data = GetNodeData(node);
      if (data == null || data.Item == null)
      {
        return;
      }

      ProjectItem item = data.Item;
      item.DeleteItemPhysically(data.Project);
      if (_project.ConnectedItems.ContainsValue(item))
      {
        _project.ConnectedItems.Remove(item.ConnectionSpec.ID.ToString());
      }
      else if (_project.DisconnectedItems.ContainsKey(item.Uid.ToString()))
      {
        _project.DisconnectedItems.Remove(item.Uid.ToString());
      }


      ProjectNodeData parentData = GetNodeData(node.Parent);
      if (parentData != null && parentData.Item != null)
      {
        if (parentData.Item.Children.ContainsValue(data.Item))
        {
          parentData.Item.Children.Remove(data.Item.Uid.ToString());
        }
      }


      node.Nodes.Clear();
      TreeNodeCollection parentNodeCol = null;
      if (node.Parent == null)
      {
        parentNodeCol = tv.Nodes;
      }
      else
      {
        parentNodeCol = node.Parent.Nodes;
      }
      parentNodeCol.Remove(node);


      ProjectFactory.SaveProject(_project);
    }

    private ConnectionParams TryToGetConnectionParams( TreeNode node )
    {
      ConnectionParams cp = null;
      ProjectNodeData data = GetNodeData(node);
      if (data == null || data.Item == null)
      {
        return cp;
      }

      if (data.ItemType == ProjectItemType.ConnectionSpec)
      {
        return data.Item.ConnectionSpec;
      }

      if (node.Parent != null)
      {
        cp = TryToGetConnectionParams(node.Parent);
      }

      return cp;
    }

    private void AddDatabaseObject( )
    {
      ProjectNodeData data = GetSelectedNodeData();
      if (data == null)
      {
        return;
      }

      ConnectionParams cp = TryToGetConnectionParams(SelectedNode);
      if (cp == null)
      {
        MessageBox.Show("Can not perform this operation for disconnected items!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (!ConfirmConnection(cp))
      {
        MessageBox.Show("Connection to database not confirmed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string caption = String.Format("Search ({0})", cp.InfoDbServer);
      IList<ObjectInfo> objects = DBObjectSearchFactory.SearchAndSelectObjectFromDb(cp, caption, false);
      if (objects == null || objects.Count == 0)
      {
        return;
      }

      string errors = String.Empty;
      string tmpError = String.Empty;
      foreach (ObjectInfo objInfo in objects)
      {
        if (objInfo.HasScript)
        {
          tmpError = AddScriptableDatabaseObject(cp, objInfo);
        }
        else
        {
          AddNonScriptableDatabaseObject(objInfo);
        }

        if (!String.IsNullOrEmpty(tmpError))
        {
          errors += "- " + tmpError + "\r\n";
        }
      }

      if (!String.IsNullOrEmpty(errors))
      {
        GenericErrorDialog.ShowError("Error", "Some errors occured during the operation. See details below.", errors);
      }

      if (objects.Count > 0)
      {
        tv.SelNode.Expand();
      }
    }

    private string AddScriptableDatabaseObject( ConnectionParams cp, ObjectInfo objInfo )
    {
      string errStr = String.Empty;
      if (cp == null || objInfo == null)
      {
        return errStr;
      }

      ProjectNodeData selNodeData = GetSelectedNodeData();
      if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
      {
        return errStr;
      }

      if (selNodeData.ItemType != ProjectItemType.Project
        && selNodeData.ItemType != ProjectItemType.Folder
        && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
      {
        return errStr;
      }

      string parentPath = selNodeData.Item.GetFullPath(_project);
      string fullPath = parentPath + @"\" + objInfo.ObjectName + ".sql";

      if (File.Exists(fullPath))
      {
        errStr = String.Format("File with name \"{0}\" already exists in folder \"{1}\"!", objInfo.ObjectName, parentPath);
        return errStr;
      }


      File.AppendAllText(fullPath, ScriptingHelper.GetAlterScript(cp, cp.Database, objInfo.ObjectID, objInfo.ObjectType));
      ProjectItem item = null;
      item = ProjectItemFactory.CreateScriptableDBObject(selNodeData.Item, objInfo.ObjectType, fullPath);

      ProjectFactory.SaveProject(_project);
      TreeNode node = AddNode(tv.SelNode, item);
      return errStr;
    }

    private void AddNonScriptableDatabaseObject( ObjectInfo objInfo )
    {
      if (objInfo == null)
      {
        return;
      }

      ProjectNodeData selNodeData = GetSelectedNodeData();
      if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
      {
        return;
      }

      if (selNodeData.ItemType != ProjectItemType.Project
        && selNodeData.ItemType != ProjectItemType.Folder
        && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
      {
        return;
      }


      ProjectItem item = ProjectItemFactory.CreateNonScriptableDBObject(selNodeData.Item, objInfo.ObjectType, objInfo.ObjectName);

      ProjectFactory.SaveProject(_project);
      TreeNode node = AddNode(tv.SelNode, item);
    }

    private void ModifyConnectionSpec( )
    {
      ProjectNodeData data = GetNodeData(SelectedNode);
      if (_project == null || data == null || data.Item == null || data.Item.ItemType != ProjectItemType.ConnectionSpec)
      {
        return;
      }

      ProjectItem item = data.Item;
      ConnectionParams cp = item.ConnectionSpec;
      if (cp == null)
      {
        return;
      }

      frmConnectionParams frm = new frmConnectionParams(cp, true, false);
      if (frm.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      cp = frm.GetCurrentConnectionSpec();
      if (String.IsNullOrEmpty(cp.IntegratedSecurity) && String.IsNullOrEmpty(cp.UserName))
      {
        MessageBox.Show("Logon information not specified correctly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (String.IsNullOrEmpty(cp.Server))
      {
        MessageBox.Show("Server not specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (String.IsNullOrEmpty(cp.Database))
      {
        MessageBox.Show("Database not specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      _confirmedConnections.Remove(item.ConnectionSpec.ID.ToString());
      item.ConnectionSpec = null;
      item.ConnectionSpec = cp.CreateCopy();
      item.Name = cp.Server + " {" + cp.Database + "}";
      SelectedNode.Text = item.Name;
      _confirmedConnections.Add(item.ConnectionSpec.ID.ToString(), item.ConnectionSpec);

      ProjectFactory.SaveProject(_project);
    }

    private bool ChangeParent( TreeNode node, TreeNode newParentNode, ProjectItem newParentItem )
    {
      ProjectItem item = null;
      ProjectNodeData data = GetNodeData(node);
      ProjectNodeData newParentNodeData = GetNodeData(newParentNode);

      if (data == null || data.Item == null || newParentNodeData == null)
      {
        return false;
      }

      item = data.Item;
      item.ChangeParent(_project, newParentItem);

      TreeNode oldParent = node.Parent;
      ProjectNodeData oldParentData = GetNodeData(oldParent);

      // Old parent is the project
      if (oldParentData.ItemType == ProjectItemType.Project)
      {
        if (item.ConnectionSpec != null && _project.ConnectedItems.ContainsKey(item.ConnectionSpec.ID.ToString()))
        {
          _project.ConnectedItems.Remove(item.ConnectionSpec.ID.ToString());
        }
        if (_project.DisconnectedItems.ContainsKey(item.Uid.ToString()))
        {
          _project.DisconnectedItems.Remove(item.Uid.ToString());
        }
      }
      else
      {
        if (oldParentData.Item.Children.ContainsKey(item.Uid.ToString()))
        {
          oldParentData.Item.Children.Remove(item.Uid.ToString());
        }
      }

      if (newParentNodeData.ItemType == ProjectItemType.Project)
      {
        if (item.ConnectionSpec != null && item.ItemType == ProjectItemType.ConnectionSpec)
        {
          _project.ConnectedItems.Add(item.ConnectionSpec.ID.ToString(), item);
        }
        else
        {
          _project.DisconnectedItems.Add(item.Uid.ToString(), item);
        }
      }
      else
      {
        newParentItem.Children.Add(item.Uid.ToString(), item);
      }

      if (node.Parent != null)
      {
        node.Parent.Nodes.Remove(node);
      }

      if (newParentNode != null)
      {
        newParentNode.Nodes.Add(node);
        if (!newParentNode.IsExpanded)
        {
          newParentNode.Expand();
        }
      }
      return true;
    }

    #endregion //Methods

    #region Dynamic Contenxt Menu Build

    private void BuildTreeViewMenu( )
    {
      ContextMenuStrip mnu = null;


      mnu = new ContextMenuStrip(this.components);
      ToolStripItem mnuItem = mnu.Items.Add("Open Project", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.OpenProject]);

      mnuItem = mnu.Items.Add("Close Project", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.CloseProject]);

      mnuItem = mnu.Items.Add("New Project", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.CreateProject]);

      tv.ContextMenuStrip = mnu;


    }

    private ContextMenuStrip BuildContextMenu( TreeNode node )
    {
      ContextMenuStrip mnu = null;

      ProjectNodeData data = GetNodeData(node);
      if (data == null)
      {
        return mnu;
      }

      mnu = new ContextMenuStrip(this.components);
      ToolStripItem mnuItem = null;

      switch (data.ItemType)
      {
        case ProjectItemType.Project:
          MergeAddNewContextMenuItems(mnu, data);
          break;
        case ProjectItemType.ConnectionSpec:
          mnuItem = mnu.Items.Add("Modify Connection", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.ModifyConnection]);

          mnuItem = mnu.Items.Add("-", null, null);
          MergeAddNewContextMenuItems(mnu, data);
          mnuItem = mnu.Items.Add("Delete", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.DeleteSelectedItems]);
          break;
        case ProjectItemType.Folder:
          MergeAddNewContextMenuItems(mnu, data);
          mnuItem = mnu.Items.Add("Delete", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.DeleteSelectedItems]);
          break;
        case ProjectItemType.ScriptableDBObject:
          BuildScriptableDBObjectContentMenu(mnu, data.Item.DbObjectType);
          MergeDiffContextMenuItems(mnu, OnSendSelectedObjectAsSourceToDiff, OnSendSelectedObjectAsDestToDiff);
          mnuItem = mnu.Items.Add("Delete", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.DeleteSelectedItems]);
          break;
        case ProjectItemType.NonScriptableDBObject:
          BuildNonScriptableDBObjectContentMenu(mnu, data.Item.DbObjectType);
          mnuItem = mnu.Items.Add("Delete", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.DeleteSelectedItems]);
          break;
        case ProjectItemType.ScriptFile:
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          MergeDiffContextMenuItems(mnu, OnSendSelectedObjectAsSourceToDiff, OnSendSelectedObjectAsDestToDiff);
          mnuItem = mnu.Items.Add("Delete", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.DeleteSelectedItems]);
          break;
        case ProjectItemType.TextFile:
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          MergeDiffContextMenuItems(mnu, OnSendSelectedObjectAsSourceToDiff, OnSendSelectedObjectAsDestToDiff);
          mnuItem = mnu.Items.Add("Delete", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.DeleteSelectedItems]);
          break;
        case ProjectItemType.ExternalFile:
          mnuItem = mnu.Items.Add("Edit With Associated Application", null, OnEditSelectedExternalFilesWithAssociatedApp);
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          MergeDiffContextMenuItems(mnu, OnSendSelectedObjectAsSourceToDiff, OnSendSelectedObjectAsDestToDiff);
          mnuItem = mnu.Items.Add("Delete", null, null);
          _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.DeleteSelectedItems]);
          break;
        default:
          break;
      }

      mnuItem = mnu.Items.Add("-", null, null);

      mnuItem = mnu.Items.Add("Rename", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.RenameItems]);
      mnuItem = mnu.Items.Add("Refresh", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.RefreshNode]);

      mnuItem = mnu.Items.Add("-", null, null);


      mnuItem = mnu.Items.Add("Open Project", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.OpenProject]);

      mnuItem = mnu.Items.Add("Close Project", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.CloseProject]);

      mnuItem = mnu.Items.Add("New Project", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.CreateProject]);

      mnuItem = mnu.Items.Add("-", null, null);

      MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ProjectExplorer/ContextMenu");
      MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ProjectExplorer/ContextMenu/" + data.ItemType.ToString());

      return mnu;
    }

    private void MergeDiffContextMenuItems( ContextMenuStrip mnu, EventHandler AsSourceHandler, EventHandler AsDestHandler )
    {
      ToolStripItem mnuItem = null;
      ToolStripMenuItem item = null;

      item = (ToolStripMenuItem)mnu.Items.Add("Diff", null, null);
      mnuItem = item.DropDownItems.Add("As Source", null, AsSourceHandler);
      mnuItem = item.DropDownItems.Add("As Dest", null, AsDestHandler);
    }

    private void MergeAddNewContextMenuItems( ContextMenuStrip mnu, ProjectNodeData data )
    {
      ToolStripItem mnuItem = null;
      ToolStripMenuItem item = null;

      item = (ToolStripMenuItem)mnu.Items.Add("Add", null, null);
      mnuItem = item.DropDownItems.Add("Connection From List", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.AddConnectionFromList]);
      mnuItem = item.DropDownItems.Add("Connection", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.AddNewConnection]);
      mnuItem = item.DropDownItems.Add("-", null, null);
      mnuItem = item.DropDownItems.Add("Folder", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.AddFolder]);
      mnuItem = item.DropDownItems.Add("-", null, null);
      mnuItem = item.DropDownItems.Add("Script", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.AddScriptFile]);
      mnuItem = item.DropDownItems.Add("Text", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.AddTextFile]);
      mnuItem = item.DropDownItems.Add("File", null, null);
      _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.AddExternalFile]);

      if (data != null && data.Item != null && (data.ItemType == ProjectItemType.ConnectionSpec || data.ItemType == ProjectItemType.Folder)
        && data.Item.IsConnectedItem)
      {
        mnuItem = item.DropDownItems.Add("Object From Database", null, null);
        _actionList.SetAction(mnuItem, _actions[ProjectNodeActions.AddDatabaseObject]);
      }
    }

    private void BuildScriptableDBObjectContentMenu( ContextMenuStrip mnu, int dbObjectType )
    {
      ToolStripItem mnuItem = null;

      switch (dbObjectType)
      {
        case DBObjectType.StoredProc:
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          break;
        case DBObjectType.TableValuedFunction:
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          break;
        case DBObjectType.ScalarValuedFunction:
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          break;
        case DBObjectType.View:
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          mnuItem = mnu.Items.Add("Open", null, OnOpenSelectedDatabaseObjects);
          break;
        case DBObjectType.Trigger:
          mnuItem = mnu.Items.Add("Edit", null, OnEditSelectedObjects);
          break;
        default:
          break;
      }
    }

    private void BuildNonScriptableDBObjectContentMenu( ContextMenuStrip mnu, int dbObjectType )
    {
      ToolStripItem mnuItem = null;

      switch (dbObjectType)
      {
        case DBObjectType.SystemTable:
          mnuItem = mnu.Items.Add("Open", null, OnOpenSelectedDatabaseObjects);
          break;
        case DBObjectType.UserTable:
          mnuItem = mnu.Items.Add("Open", null, OnOpenSelectedDatabaseObjects);
          break;
        default:
          break;
      }
    }

    private void OnEditSelectedObjects( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.EditSelectedObjects);
      EditSelectedObjects();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.EditSelectedObjects);
    }

    private void OnSendSelectedObjectAsSourceToDiff( object sender, EventArgs args )
    {
      SendSelectedObjectToDiff(true);
    }

    private void OnSendSelectedObjectAsDestToDiff( object sender, EventArgs args )
    {
      SendSelectedObjectToDiff(false);
    }

    private void OnOpenSelectedDatabaseObjects( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.OpenDatabaseObject);
      OpenSelectedDatabaseObjects();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.OpenDatabaseObject);
    }

    private void OnEditSelectedExternalFilesWithAssociatedApp( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.EditWithExternalApp);
      EditSelectedExternalFilesWithAssociatedApp();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.EditWithExternalApp);
    }

    private void EditSelectedObjects( )
    {
      string errors = String.Empty;

      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      TreeNode node = null;
      ProjectNodeData data = null;
      ConnectionParams cp = null;
      string script = String.Empty;
      IList<IPragmaEditor> editors = new List<IPragmaEditor>();
      IPragmaEditor tmpEditor = null;
      string fileName = String.Empty;

      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        node = nodeWrapper.Node;
        data = GetNodeData(node);
        if (data == null)
        {
          continue;
        }

        if ((data.ItemType != ProjectItemType.ScriptableDBObject)
            && (data.ItemType != ProjectItemType.ScriptFile)
            && (data.ItemType != ProjectItemType.TextFile)
            && (data.ItemType != ProjectItemType.ExternalFile)
          )
        {
          continue;
        }

        fileName = data.Item.GetFullFileName(_project);
        cp = TryToGetConnectionParams(node);

        if (cp != null && !ConfirmConnection(cp))
        {
          MessageBox.Show("Connection to database not confirmed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (String.IsNullOrEmpty(fileName) || !File.Exists(fileName))
        {
          errors += "- Can not open item! FileName is: \"" + fileName + "\".\r\n";
          continue;
        }

        if ((cp != null) && data.ItemType != ProjectItemType.TextFile)
        {
          tmpEditor = ScriptEditorFactory.OpenFile(fileName, cp);
        }
        else
        {
          tmpEditor = TextEditorFactory.OpenFile(fileName);
        }
        if (tmpEditor != null)
        {
          editors.Add(tmpEditor);
        }
      }

      foreach (IPragmaEditor editor in editors)
      {
        if (editor.GetType() == typeof(frmScriptEditor))
        {
          ScriptEditorFactory.ShowScriptEditor(editor as frmScriptEditor);
        }
        else if (editor.GetType() == typeof(frmTextEditor))
        {
          TextEditorFactory.ShowTextEditor(editor as frmTextEditor);
        }
      }

      if (!String.IsNullOrEmpty(errors))
      {
        GenericErrorDialog.ShowError("Error", "One or more files can not be opened for editing!", errors);
      }
    }

    private void SendSelectedObjectToDiff( bool isSource )
    {
      string errors = String.Empty;

      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      TreeNode node = null;
      ProjectNodeData data = null;
      ConnectionParams cp = null;
      string script = String.Empty;
      IList<IPragmaEditor> editors = new List<IPragmaEditor>();
      string fileName = String.Empty;

      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        node = nodeWrapper.Node;
        data = GetNodeData(node);
        if (data == null)
        {
          break;
        }

        if ((data.ItemType != ProjectItemType.ScriptableDBObject)
            && (data.ItemType != ProjectItemType.ScriptFile)
            && (data.ItemType != ProjectItemType.TextFile)
            && (data.ItemType != ProjectItemType.ExternalFile)
          )
        {
          break;
        }

        fileName = data.Item.GetFullFileName(_project);
        cp = TryToGetConnectionParams(node);

        if (cp != null && !ConfirmConnection(cp))
        {
          MessageBox.Show("Connection to database not confirmed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          break;
        }

        if (String.IsNullOrEmpty(fileName) || !File.Exists(fileName))
        {
          MessageBox.Show("Can not open item!\nFileName is: \"" + fileName + "\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          break;
        }


        frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
        if (diffForm == null)
        {
          diffForm = TextDiffFactory.CreateDiff();
        }

        if (isSource)
        {
          diffForm.diffControl.OpenSourceFile(fileName);
          diffForm.diffControl.SourceHeaderText = data.Item.Name;
        }
        else
        {
          diffForm.diffControl.OpenDestFile(fileName);
          diffForm.diffControl.DestHeaderText = data.Item.Name;
        }
        diffForm.Show();
        diffForm.BringToFront();

        break;
      }
    }

    private void OpenSelectedDatabaseObjects( )
    {
      string errors = String.Empty;
      string tmpError = String.Empty;

      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      TreeNode node = null;
      ProjectNodeData data = null;
      ConnectionParams cp = null;
      IList<frmDataViewer> viewers = new List<frmDataViewer>();

      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        node = nodeWrapper.Node;
        data = GetNodeData(node);
        if (data == null)
        {
          continue;
        }

        if (
            !(
              (data.ItemType == ProjectItemType.NonScriptableDBObject)
              || (data.ItemType == ProjectItemType.ScriptableDBObject && data.Item.DbObjectType == DBObjectType.View)
             )
          )
        {
          continue;
        }

        cp = TryToGetConnectionParams(node);
        if (cp == null)
        {
          MessageBox.Show("Can not perform this operation for disconnected items!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (!ConfirmConnection(cp))
        {
          MessageBox.Show("Connection to database not confirmed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (cp != null)
        {
          try
          {
            string caption = node.Text + "{" + cp.InfoDbServer + "}";
            string script = " select * from [" + node.Text + "]";
            bool isReadOnly = (data.Item.DbObjectType == DBObjectType.View) ? true : false;
            frmDataViewer viewer = DataViewerFactory.CreateDataViewer(cp, cp.Database, node.Name, caption, script, isReadOnly, false);
            viewers.Add(viewer);
          }
          catch (Exception ex)
          {
            errors += "- " + ex.Message + "\r\n";
          }
        }
      }

      foreach (frmDataViewer viewer in viewers)
      {
        try
        {
          viewer.LoadData(true);
          DataViewerFactory.ShowDataViewer(viewer);
        }
        catch (Exception ex)
        {
          errors += "- " + ex.Message + "\r\n";
        }
      }

      if (!String.IsNullOrEmpty(errors))
      {
        GenericErrorDialog.ShowError("Error", "One or more objects can not be opened!", errors);
      }
    }

    private void EditSelectedExternalFilesWithAssociatedApp( )
    {
      string errors = String.Empty;
      string tmpError = String.Empty;

      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      string fileName = String.Empty;
      TreeNode node = null;
      ProjectNodeData data = null;
      IList<frmDataViewer> viewers = new List<frmDataViewer>();

      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        node = nodeWrapper.Node;
        data = GetNodeData(node);
        if (data == null)
        {
          continue;
        }

        if (data.ItemType != ProjectItemType.ExternalFile)
        {
          continue;
        }

        fileName = data.Item.GetFullFileName(_project);
        if (String.IsNullOrEmpty(fileName) || !File.Exists(fileName))
        {
          errors += "- Can not open item! FileName is: \"" + fileName + "\".\r\n";
          continue;
        }
        try
        {
          System.Diagnostics.Process.Start(fileName);
        }
        catch (Exception ex)
        {
          errors += "- " + ex.Message + "\r\n";
        }
      }

      if (!String.IsNullOrEmpty(errors))
      {
        GenericErrorDialog.ShowError("Error", "One or more files can not be opened!", errors);
      }
    }

    private void PerformNodeDoubleClick( TreeNode node )
    {
      ProjectNodeData data = GetNodeData(node);
      if (data == null || data.Item == null)
      {
        return;
      }

      ProjectItem item = data.Item;
      switch (item.ItemType)
      {
        case ProjectItemType.ConnectionSpec:
          break;
        case ProjectItemType.ExternalFile:
          EditSelectedObjects();
          break;
        case ProjectItemType.Folder:
          break;
        case ProjectItemType.NonScriptableDBObject:
          OpenSelectedDatabaseObjects();
          break;
        case ProjectItemType.Project:
          break;
        case ProjectItemType.ScriptableDBObject:
          EditSelectedObjects();
          break;
        case ProjectItemType.TextFile:
          EditSelectedObjects();
          break;
        case ProjectItemType.ScriptFile:
          EditSelectedObjects();
          break;
        case ProjectItemType.Unknown:
          break;
        default:
          break;
      }

    }

    #endregion //Dynamic Contenxt Menu Build

    #region AddIn Support
    public void InitializeAddInSupport( )
    {
      ToolStrip toolbar = ToolbarService.CreateToolStrip(this, "/Workspace/ProjectExplorer/Toolbar");
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

    #region IProjectExplorerServices Members

    public ProjectNodeData SelectedItem
    {
      get { return SelectedNodeData; }
    }

    public IList<ProjectNodeData> SelectedItems
    {
      get
      {
        IList<ProjectNodeData> result = new List<ProjectNodeData>();
        foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
        {
          ProjectNodeData data = GetNodeData(nodeWrapper.Node);
          if (data == null)
          {
            continue;
          }
          result.Add(data);
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

    public Project CurrentProject
    {
      get { return _project; }
    }

    public void ExecuteCommand( ProjectExplorerCommand action )
    {
      if(!CanPerformCommand(action))
      {
        HostServicesSingleton.HostServices.MsgService.ErrorMsg(String.Format("Selecte node(s) state is invalid! Can not perform required action: \"{0}\".", action), (MethodInfo)MethodInfo.GetCurrentMethod());
        HostServicesSingleton.HostServices.MsgService.ShowMessages();
        return;
      }

      switch (action)
      {
        case ProjectExplorerCommand.None:
          break;
        case ProjectExplorerCommand.OpenProject:
          OpenProject();
          break;
        case ProjectExplorerCommand.CreateProject:
          CreateNewProject();
          break;
        case ProjectExplorerCommand.CloseProject:
          CloseProject();
          break;
        case ProjectExplorerCommand.NewConnection:
          AddNewConnection();
          break;
        case ProjectExplorerCommand.NewConnectionFromList:
          AddConnectionFromList();
          break;
        case ProjectExplorerCommand.Refresh:
          RefreshNode(tv.SelNode, true);
          break;
        case ProjectExplorerCommand.RenameSelectedItem:
          RenameSelectedItem();
          break;
        case ProjectExplorerCommand.ModifyConnection:
          ModifyConnectionSpec();
          break;
        case ProjectExplorerCommand.DeleteSelectedItems:
          DeleteSelectedItems();
          break;
        case ProjectExplorerCommand.AddFolder:
          AddFolder();
          break;
        case ProjectExplorerCommand.AddScript:
          AddContentFile(ProjectItemType.ScriptFile);
          break;
        case ProjectExplorerCommand.AddText:
          AddContentFile(ProjectItemType.TextFile);
          break;
        case ProjectExplorerCommand.AddExternalFile:
          AddExternalFile();
          break;
        case ProjectExplorerCommand.AddObjectFromDatabase:
          AddDatabaseObject();
          break;
        case ProjectExplorerCommand.EditSelectedObjects:
          EditSelectedObjects();
          break;
        case ProjectExplorerCommand.EditWithExternalApp:
          EditSelectedExternalFilesWithAssociatedApp();
          break;
        case ProjectExplorerCommand.OpenDatabaseObject:
          OpenSelectedDatabaseObjects();
          break;
        default:
          break;
      }
    }

    public bool CanPerformCommand( ProjectExplorerCommand action )
    {
      ProjectNodeData selNodeData = null;
      bool result = false;
      switch (action)
      {
        case ProjectExplorerCommand.None:
          result = true;
          break;
        case ProjectExplorerCommand.OpenProject:
          result = true;
          break;
        case ProjectExplorerCommand.CreateProject:
          result = true;
          break;
        case ProjectExplorerCommand.CloseProject:
          result = (_project != null);
          break;
        case ProjectExplorerCommand.NewConnection:
          result = (_project != null);
          break;
        case ProjectExplorerCommand.NewConnectionFromList:
          result = (_project != null);
          break;
        case ProjectExplorerCommand.Refresh:
          result = (_project != null && tv.SelNode != null);
          break;
        case ProjectExplorerCommand.RenameSelectedItem:
          ProjectNodeData data = GetSelectedNodeData();
          if (data == null)
          {
            result = false;
            break;
          }

          if (data.ItemType == ProjectItemType.ConnectionSpec
            || data.ItemType == ProjectItemType.Unknown)
          {
            result = false;
            break;
          }

          result = (_project != null);
          break;
        case ProjectExplorerCommand.ModifyConnection:
          selNodeData = GetSelectedNodeData();
          if (selNodeData == null || selNodeData.Item == null || selNodeData.ItemType != ProjectItemType.ConnectionSpec)
          {
            result = false;
            break;
          }

          result = (_project != null);
          break;
        case ProjectExplorerCommand.DeleteSelectedItems:
          selNodeData = GetSelectedNodeData();
          if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
          {
            result = false;
            break;
          }

          if (selNodeData.ItemType == ProjectItemType.Project)
          {
            result = false;
            break;
          }
          result = (_project != null);
          break;
        case ProjectExplorerCommand.AddFolder:
          selNodeData = GetSelectedNodeData();
          if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
          {
            result = false;
            break;
          }

          if (selNodeData.ItemType != ProjectItemType.Project
            && selNodeData.ItemType != ProjectItemType.Folder
            && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
          {
            result = false;
            break;
          }
          result = (_project != null);
          break;
        case ProjectExplorerCommand.AddScript:
          selNodeData = GetSelectedNodeData();

          if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
          {
            result = false;
            break; ;
          }

          if (selNodeData.ItemType != ProjectItemType.Project
            && selNodeData.ItemType != ProjectItemType.Folder
            && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
          {
            result = false;
            break;
          }
          result = (_project != null);
          break;
        case ProjectExplorerCommand.AddText:
          selNodeData = GetSelectedNodeData();

          if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
          {
            result = false;
            break; ;
          }

          if (selNodeData.ItemType != ProjectItemType.Project
            && selNodeData.ItemType != ProjectItemType.Folder
            && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
          {
            result = false;
            break;
          }
          result = (_project != null);
          break;
        case ProjectExplorerCommand.AddExternalFile:
          selNodeData = GetSelectedNodeData();

          if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
          {
            result = false;
            break;
          }

          if (selNodeData.ItemType != ProjectItemType.Project
            && selNodeData.ItemType != ProjectItemType.Folder
            && selNodeData.ItemType != ProjectItemType.ConnectionSpec)
          {
            result = false;
            break;
          }

          result = (_project != null);
          break;
        case ProjectExplorerCommand.AddObjectFromDatabase:

          selNodeData = GetSelectedNodeData();
          if (selNodeData == null || (selNodeData.Project == null && selNodeData.Item == null))
          {
            result = false;
            break;
          }

          if (((selNodeData.ItemType != ProjectItemType.ConnectionSpec) && (selNodeData.ItemType != ProjectItemType.Folder))
            || (!selNodeData.Item.IsConnectedItem))
          {
            result = false;
            break;
          }

          result = (_project != null);
          break;
        case ProjectExplorerCommand.EditSelectedObjects:
          selNodeData = SelectedNodeData;
          if (selNodeData == null)
          {
            result = false;
            break;
          }

          if (selNodeData.ItemType == ProjectItemType.ScriptFile || selNodeData.ItemType == ProjectItemType.TextFile)
          {
            result = true;
            break;
          }


          if (selNodeData.Item != null)
          {
            switch (selNodeData.Item.DbObjectType)
            {
              case DBObjectType.StoredProc:
                result = true;
                break;
              case DBObjectType.TableValuedFunction:
                result = true;
                break;
              case DBObjectType.ScalarValuedFunction:
                result = true;
                break;
              case DBObjectType.View:
                result = true;
                break;
              case DBObjectType.Trigger:
                result = true;
                break;
              default:
                result = false;
                break;
            }
            break;
          }
          else
          {
            result = false;
            break;          
          }
        case ProjectExplorerCommand.EditWithExternalApp:
          selNodeData = SelectedNodeData;
          if (selNodeData == null)
          {
            result = false;
            break;
          }
          if (selNodeData.ItemType == ProjectItemType.ExternalFile)
          {
            result = true;
            break;
          }
          result = false;
          break;
        case ProjectExplorerCommand.OpenDatabaseObject:
          if (selNodeData.Item != null)
          {
            switch (selNodeData.Item.DbObjectType)
            {
              case DBObjectType.View:
                result = true;
                break;
              case DBObjectType.SystemTable:
                result = true;
                break;
              case DBObjectType.UserTable:
                result = true;
                break;
              default:
                result = false;
                break;
            }
            break;
          }
          else
          {
            result = false;
            break;
          } 
        default:
          result = false;
          break;
        //throw new Exception("Unsupported project explorer action!");
      }

      return result;
    }

		public void OpenProject(string fileName)
		{
			InternalOpenProject(fileName);
		}

    private event EventHandler _afterSelectedNodesChanged;
    public event EventHandler AfterSelectedNodesChanged
    {
      add { _afterSelectedNodesChanged += value; }
      remove { _afterSelectedNodesChanged += value; }
    }
    private void FireAfterSelectedNodesChanged( )
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
    
    private BeforeProjectExplorerActionDelegate _beforeProjectExplorerAction;
    public event BeforeProjectExplorerActionDelegate BeforeProjectExplorerAction
    {
      add { _beforeProjectExplorerAction += value; }
      remove { _beforeProjectExplorerAction -= value; }
    }
    private void FireBeforeProjectExplorerAction( ProjectExplorerCommand action )
    {
      if (_beforeProjectExplorerAction == null)
      {
        return;
      }

      Delegate[] delegates = _beforeProjectExplorerAction.GetInvocationList();
      foreach (BeforeProjectExplorerActionDelegate del in delegates)
      {
        try
        {
          ProjectExplorerEventArgs args = new ProjectExplorerEventArgs();
          args.Action = action;
          args.Items = SelectedItems;
          del.Invoke(this, args);
        }
        catch (Exception ex)
        {
          HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
          HostServicesSingleton.HostServices.MsgService.ShowMessages();
        }
      }
    }

    private AfterProjectExplorerActionDelegate _afterProjectExplorerAction;
    public event AfterProjectExplorerActionDelegate AfterProjectExplorerAction
    {
      add { _afterProjectExplorerAction += value; }
      remove { _afterProjectExplorerAction -= value; }
    }
    private void FireAfterProjectExplorerAction( ProjectExplorerCommand action )
    {
      if (_afterProjectExplorerAction == null)
      {
        return;
      }

      Delegate[] delegates = _afterProjectExplorerAction.GetInvocationList();
      foreach (AfterProjectExplorerActionDelegate del in delegates)
      {
        try
        {
          ProjectExplorerEventArgs args = new ProjectExplorerEventArgs();
          args.Action = action;
          args.Items = SelectedItems;
          del.Invoke(this, args);
        }
        catch (Exception ex)
        {
          HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
          HostServicesSingleton.HostServices.MsgService.ShowMessages();
        }
      }
    }

    private EventHandler _afterProjectExplorerClosed;
    public event EventHandler AfterProjectExplorerClosed
    {
      add { _afterProjectExplorerClosed += value; }
      remove { _afterProjectExplorerClosed -= value; }
    }
    private void FireAfterProjectExplorerClosed( )
    {
      if (_afterProjectExplorerClosed == null)
      {
        return;
      }

      Delegate[] delegates = _afterProjectExplorerClosed.GetInvocationList();
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

    public void ShowProjectExplorer( )
    {
      Program.MainForm.ShowProjectExplorer();
    }

    #endregion

    void tv_DoubleClick( object sender, EventArgs e )
    {
      PerformNodeDoubleClick(SelectedNode);
    }

    private void tv_BeforeExpand( object sender, TreeViewCancelEventArgs e )
    {
      RefreshNode(e.Node, false);
    }

    private void tv_KeyUp( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Delete)
      {
        if (_actions[ProjectNodeActions.DeleteSelectedItems].Enabled)
        {
          _actions[ProjectNodeActions.DeleteSelectedItems].DoExecute();
        }
      }
      else if (e.KeyCode == Keys.F2)
      {
        if (_actions[ProjectNodeActions.RenameItems].Enabled)
        {
          _actions[ProjectNodeActions.RenameItems].DoExecute();
        }
      }
      e.SuppressKeyPress = false;
    }

    private void tv_ItemDrag( object sender, ItemDragEventArgs e )
    {
      DoDragDrop(tv.SelNodes, DragDropEffects.Move);
    }

    private void tv_DragOver( object sender, DragEventArgs e )
    {
      if (_project == null)
      {
        e.Effect = DragDropEffects.None;
        return;
      }

      Point pos = new Point();
      pos.X = e.X;
      pos.Y = e.Y;
      pos = tv.PointToClient(pos);

      TreeNode dropNode = tv.GetNodeAt(pos);
      ProjectNodeData dropNodeData = GetNodeData(dropNode);

      /*
      if(data == null
          || data.ItemType == ProjectItemType.NonScriptableDBObject 
          || data.ItemType == ProjectItemType.ScriptableDBObject
          || data.ItemType == ProjectItemType.ExternalFile
          || data.ItemType == ProjectItemType.ScriptFile
          || data.ItemType == ProjectItemType.Unknown
        )
      {
        e.Effect = DragDropEffects.None;
        return;
      }
      */

      string dropText = e.Data.GetData(typeof(string)) as string;
      if (!String.IsNullOrEmpty(dropText))
      {
        //TODO:
        e.Effect = DragDropEffects.None;
        //e.Effect = DragDropEffects.Copy;
      }
      else
      {
        Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;
        if (sourceNodes == null || sourceNodes.Count == 0)
        {
          e.Effect = DragDropEffects.None;
          return;
        }
        // Check if we can drop all selected nodes. If there is any non droppable node
        // we cancel drag drop operation
        foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
        {
          TreeNode firstNode = nodeWrapper.Node;
          ProjectNodeData firstNodeData = GetNodeData(firstNode);

          if (
            firstNodeData != null
            && (
                  firstNodeData.ItemType == ProjectItemType.ConnectionSpec
                  || firstNodeData.ItemType == ProjectItemType.Project
                )
            )
          {
            e.Effect = DragDropEffects.None;
            return;
          }
        }

        e.Effect = DragDropEffects.Move;

      }
    }

    private void tv_DragDrop( object sender, DragEventArgs e )
    {
      if (_project == null)
      {
        e.Effect = DragDropEffects.None;
        return;
      }

      Point pos = new Point();
      pos.X = e.X;
      pos.Y = e.Y;
      pos = tv.PointToClient(pos);

      TreeNode dropNode = tv.GetNodeAt(pos);
      ProjectNodeData dropNodeData = GetNodeData(dropNode);

      if (dropNodeData == null
          || dropNodeData.ItemType == ProjectItemType.NonScriptableDBObject
          || dropNodeData.ItemType == ProjectItemType.ScriptableDBObject
          || dropNodeData.ItemType == ProjectItemType.ExternalFile
          || dropNodeData.ItemType == ProjectItemType.ScriptFile
          || dropNodeData.ItemType == ProjectItemType.Unknown
        )
      {
        dropNode = dropNode.Parent;
        dropNodeData = GetNodeData(dropNode);
      }

      if (dropNodeData == null)
      {
        e.Effect = DragDropEffects.None;
        return;
      }

      ProjectItem parentItem = null;
      if (dropNodeData.ItemType != ProjectItemType.Project)
      {
        parentItem = dropNodeData.Item;
        if (parentItem == null)
        {
          e.Effect = DragDropEffects.None;
          return;
        }
      }

      string dropText = e.Data.GetData(typeof(string)) as string;
      if (!String.IsNullOrEmpty(dropText))
      {
        //TODO:
        e.Effect = DragDropEffects.None;
        //e.Effect = DragDropEffects.Copy;
      }
      else
      {
        Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;
        if (sourceNodes == null || sourceNodes.Count == 0)
        {
          e.Effect = DragDropEffects.None;
          return;
        }

        int successCnt = 0;
        // Check if we can drop all selected nodes. If there is any non droppable node
        // we cancel drag drop operation
        foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
        {
          if (ChangeParent(nodeWrapper.Node, dropNode, parentItem))
          {
            successCnt++;
          }
        }
        if (successCnt > 0)
        {
          ProjectFactory.SaveProject(_project);
          tv.Sort();
        }
        e.Effect = DragDropEffects.Move;
      }
    }

    private void frmProjectExplorer_Load( object sender, EventArgs e )
    {

    }

    private void frmProjectExplorer_FormClosed( object sender, FormClosedEventArgs e )
    {
      CloseProject();
      FireAfterProjectExplorerClosed();
    }

    private void tv_AfterSelect( object sender, TreeViewEventArgs e )
    {
      FireAfterSelectedNodesChanged();
    }


  }


  public delegate void OpenProjectFileDelegate( string fileName );

}