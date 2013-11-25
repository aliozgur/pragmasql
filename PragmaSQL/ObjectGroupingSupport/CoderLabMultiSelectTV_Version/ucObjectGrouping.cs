/************************************************************************************************************
 * Ali Özgür
 * ali_ozgur@hotmail.com
 * www.pragmasql.com 
 * 
 * Source code included in this file can not be used without written
 * permissions of the owner mentioned above. 
 * All rigths reserver
 * Copyright PragmaSQL 2007 
 ************************************************************************************************************/

using System;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using Crad.Windows.Forms.Actions;
using RichTextBoxEx;
using CodersLab.Windows.Controls;

using PragmaSQL.Core;
using PragmaSQL.WebBrowserEx;
using ICSharpCode.Core;

namespace PragmaSQL
{
  public partial class ucObjectGrouping : UserControl
  {
    private IList<ConnectionParams> _cpList = new List<ConnectionParams>();
    private RichTextEditor _rtbHelpText = null;

    private ObjectGroupingService _grpFacade = new ObjectGroupingService();
    private bool _isInitializing = false;

    #region Properties

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
      private set
      {
        _connParams = value.CreateCopy();
        _grpFacade.ConnParams = value;
      }
    }


    private ConnectionParamsChangedEventHandler _connectionParamsChanged;
    public event ConnectionParamsChangedEventHandler ConnectionParamsChanged
    {
      add
      {
        _connectionParamsChanged += value;
      }
      remove
      {
        _connectionParamsChanged -= value;
      }
    }


    private bool CanEditObjectGrouping
    {
      get
      {
        return tv.Enabled;
      }
      set
      {
        tv.Enabled = value;
        toolStrip2.Enabled = value;
        if (!value)
        {
          HelpTextVisible = false;
        }
        UpdateSelectedItemInfo();
      }
    }


    private string CurrentHelpText
    {
      get
      {
        if (_rtbHelpText == null)
        {
          return String.Empty;
        }
        else
        {
          return _rtbHelpText.RichTextBox.Rtf;
        }
      }
      set
      {
        if (_rtbHelpText != null)
        {
          _rtbHelpText.RichTextBox.Rtf = value;
        }
      }
    }



    private ConnectionParamsCollection Connections
    {
      get
      {
        return ConnectionParamsFactory.GetConnections();
      }
    }

    public TreeNode SelectedNode
    {
      get
      {
				if (tv.SelectedNodes.Count > 0)
					return tv.SelectedNodes[0];
				else
					return null;
      }
      
    }

    private ObjectGroupingItemData SelectedNodeData
    {
      get
      {
        return ObjectGroupingItemDataFactory.GetNodeData(SelectedNode);
      }
    }

    public bool HelpTextVisible
    {
      get
      {
        return panHelpText.Visible;
      }
      set
      {
        if (value)
        {
          panHelpText.Height = (this.Height / 2) - 10;
        }
        panHelpText.Visible = value;
        splitterHelpText.Visible = value;
        splitterHelpText.BringToFront();
        tv.BringToFront();
        btnHelpText.Checked = value;
      }
    }

    private bool _helpTextChanged = false;
    public bool HelpTextChanged
    {
      get { return _helpTextChanged; }
    }

    #endregion

    public ucObjectGrouping( )
    {
      InitializeComponent();
      toolStripContainer1.Height = toolStrip1.Height + toolStrip2.Height;
      InitActions();
      InitializeHelpTextControl();
    }

    #region Initialization

    public bool InitializeObjectGrouping( ConnectionParams connParams )
    {

      bool result = false;
      string infoMsg = String.Empty;
      tv.Nodes.Clear();

      try
      {
        _isInitializing = true;
        
        ConnParams = connParams;
        PopulateServerAndDatabaseCombos();

        if (!_grpFacade.IsObjectGroupingSupportInstalled())
        {
          string msg = String.Format(PragmaSQL.Properties.Resources.DialogMessage_ObjectGroupSupportNotInstalled, _connParams.Server, _connParams.Database);

          if(!MessageService.AskQuestion(msg))
          {
            result = false;
          }
          else
          {
            _grpFacade.InstallObjectGroupingSupport();
            MessageService.ShowMessage("Object grouping support installed.");
            result = true;
          }
        }
        else
        {
          result = true;
        }

        if (result)
        {
          LoadInitial();
        }

        RaiseConnectionParamsChangedEvent(!result);
      }
      finally
      {
        CanEditObjectGrouping = result;
        _isInitializing = false;
      }
      return result;
    }

    private void InitializeHelpTextControl( )
    {
      if (_rtbHelpText != null)
      {
        return;
      }

      _rtbHelpText = new RichTextEditor();
      panHelpText.Controls.Add(_rtbHelpText);
      _rtbHelpText.Parent = panHelpText;
      _rtbHelpText.Dock = DockStyle.Fill;
      _rtbHelpText.BringToFront();
      _rtbHelpText.ShowOpen = false;
      _rtbHelpText.ShowSave = false;
      _rtbHelpText.ShowInsertHyperlink = false;
      _rtbHelpText.AcceptsTab = true;
      _rtbHelpText.RichTextBox.DetectUrls = true;
      _rtbHelpText.RichTextBox.TextChanged += new EventHandler(OnHelpTextChanged);
      _rtbHelpText.RichTextBox.LinkClicked +=new LinkClickedEventHandler(RichTextBox_LinkClicked);
    }

    void RichTextBox_LinkClicked( object sender, LinkClickedEventArgs e )
    {
      frmWebBrowser frm = WebBrowserFactory.CreateWebBrowser(String.Empty, e.LinkText);
      WebBrowserFactory.ShowWebBrowser(frm);
    }

    void OnHelpTextChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

    _helpTextChanged = true;
    }

    #endregion //Initialization

    #region Initialize Actions
    private ActionList _actionList = new ActionList();

    private void InitActions( )
    {
      Action ac = new Action();
      ac.Image = PragmaSQL.Properties.Resources.NewFolder;
      ac.Update +=new EventHandler(OnAction_FolderOnlyAction_Update);
      ac.Execute += new EventHandler(OnAction_AddSubFolder_Execute);
      ac.Text = "New Sub Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewSubFolder, ac);
      _actionList.SetAction(popUpTvAddSubFolder, ac);

      ac = new Action();
      ac.Image = PragmaSQL.Properties.Resources.new_case;
      ac.Execute += new EventHandler(OnAction_AddRootFolder_Execute);
      ac.Text = "New Root Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewRootFolder, ac);
      _actionList.SetAction(popUpTvAddRootFolder, ac);

      ac = new Action();
      ac.ShortcutKeys = Keys.F5;
      ac.Image = PragmaSQL.Properties.Resources.Refresh;
      ac.Update +=new EventHandler(OnAction_FolderOnlyAction_Update);
      ac.Execute += new EventHandler(OnAction_RefreshFolder_Execute);
      ac.Text = "Refresh Folder";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnRefresh, ac);
      _actionList.SetAction(popUpTvRefresh, ac);


      ac = new Action();
      ac.ShortcutKeys = Keys.Control | Keys.F5;
      ac.Image = PragmaSQL.Properties.Resources.reload24bit;
      ac.Execute += new EventHandler(OnAction_LoadInitial_Execute);
      ac.Text = "Reload";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnReload, ac);
      _actionList.SetAction(popUpTvReload, ac);

      ac = new Action();
      ac.ShortcutKeys = Keys.Control | Keys.Delete;
      ac.Image = PragmaSQL.Properties.Resources.delete;
      ac.Update +=new EventHandler(OnAction_DeleteItem_Update);
      ac.Execute += new EventHandler(OnAction_DeleteItem_Execute);
      ac.Text = "Delete";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnDelete, ac);
      _actionList.SetAction(popUpTvDeleteItem, ac);

      ac = new Action();
      ac.Image = PragmaSQL.Properties.Resources.RenameFolder;
      ac.ShortcutKeys = Keys.F2;
      ac.Update +=new EventHandler(OnAction_FolderOnlyAction_Update);
      ac.Execute += new EventHandler(OnAction_RenameItem_Execute);
      ac.Text = "Rename Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnRename, ac);
      _actionList.SetAction(popUpTvRenameFolder, ac);

      ac = new Action();
      ac.Image = PragmaSQL.Properties.Resources.help_2;
      ac.Update +=new EventHandler(OnAction_ShowHelpText_Update);
      ac.Execute += new EventHandler(OnAction_ShowHelpText_Execute);
      ac.Text = "Show HelpText";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnHelpText, ac);
      _actionList.SetAction(popUpTvShowHelpText, ac);

      ac = new Action();
      ac.Image = PragmaSQL.Properties.Resources.SaveAsWebPage;
      ac.ShortcutKeys = Keys.Control | Keys.S;
      ac.Update +=new EventHandler(OnAction_SaveHelpText_Update);
      ac.Execute += new EventHandler(OnAction_SaveHelpText_Execute);
      ac.Text = "Save HelpText";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnSaveHelpText, ac);
      _actionList.SetAction(mnuSaveHelpText, ac);
      _actionList.SetAction(popUpTvSaveHelpText, ac);


      ac = new Action();
      ac.Update +=new EventHandler(OnAction_ModifyObject_Update);
      ac.Execute += new EventHandler(OnAction_ModifyObject_Execute);
      ac.Text = "Modify";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(popUpTvModify, ac);

      ac = new Action();
      ac.Update +=new EventHandler(OnAction_OpenObject_Update);
      ac.Execute += new EventHandler(OnAction_OpenObject_Execute);
      ac.Text = "Open";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(popUpTvOpen, ac);


    }


    private void OnAction_FolderOnlyAction_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = CanPerformFolderOnlyAction();
    }


    private void OnAction_AddRootFolder_Execute( object sender, EventArgs e )
    {
      AddFolder(null);
    }

    private void OnAction_AddSubFolder_Execute( object sender, EventArgs e )
    {
      AddFolder(SelectedNode);
    }


    private void OnAction_RefreshFolder_Execute( object sender, EventArgs e )
    {
      RefreshFolder();
    }

    private void OnAction_LoadInitial_Execute( object sender, EventArgs e )
    {
      LoadInitial();
    }

    private void OnAction_DeleteItem_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;

      ac.Enabled = CanDeleteItem();
    }


    private void OnAction_DeleteItem_Execute( object sender, EventArgs e )
    {
      DeleteSelectedItems();
    }


    private void OnAction_RenameItem_Execute( object sender, EventArgs e )
    {
      RenameItem();
    }



    private void OnAction_ShowHelpText_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      if (!CanShowHelpText())
      {
        HelpTextVisible = false;
        btnHelpText.Checked = false;
      }
      ac.Enabled = CanShowHelpText();
    }


    private void OnAction_ShowHelpText_Execute( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      HelpTextVisible = !HelpTextVisible;
    }


    private void OnAction_SaveHelpText_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = CanPerformSaveHelpText();
    }

    private void OnAction_SaveHelpText_Execute( object sender, EventArgs e )
    {
      SaveHelpTextIfChanged(true);
    }

    private void OnAction_ModifyObject_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = CanModifySelectedObjects();
    }

    private void OnAction_ModifyObject_Execute( object sender, EventArgs e )
    {
      ModifySelectedObjects();
    }

    private void OnAction_OpenObject_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = CanOpenSelectedObjects();

    }

    private void OnAction_OpenObject_Execute( object sender, EventArgs e )
    {
      OpenSelectedObjects();
    }

    #endregion //Initialize Actions

    #region Populate servers and databases

    private void PopulateServerAndDatabaseCombos( )
    {
      cmbServers.Items.Clear();
      cmbDatabases.Items.Clear();

      PopulateServers(_connParams);
      PopulateDatabases(_connParams.Database);
    }

    private void PopulateServers( ConnectionParams defaultParams )
    {
      _cpList.Clear();
      
      int serverIndex = -1;
      bool defaultIsInList = false;
      ConnectionParamsCollection cons = Connections;
      foreach (ConnectionParams cp in cons)
      {
        cmbServers.Items.Add(cp.Server);
        if (defaultParams != null && defaultParams.Server.ToLowerInvariant() == cp.Server.ToLowerInvariant())
        {
          cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
          serverIndex = cmbServers.SelectedIndex;
          defaultIsInList = true;
        }
        _cpList.Add(cp);
      }

      if (!defaultIsInList)
      {
        cmbServers.Items.Add(defaultParams.Server);
        cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
        _cpList.Add(defaultParams);
      }
    }
    

    private void PopulateDatabases( string defaultDatabaseName )
    {
      cmbDatabases.Items.Clear();

      using (SqlConnection conn = _connParams.CreateSqlConnection(true,false))
      {
        DataTable dbs = conn.GetSchema("Databases");

        dbs.DefaultView.Sort = "database_name";
        dbs = dbs.DefaultView.ToTable();

        int dbIndex = -1;
        foreach (DataRow row in dbs.Rows)
        {
          string dbName = (string)row["database_name"];

          cmbDatabases.Items.Add(dbName);
          if (defaultDatabaseName.ToLowerInvariant() == dbName.ToLowerInvariant())
          {
            cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
            dbIndex = cmbDatabases.SelectedIndex;
          }
        }

        if (dbIndex == -1)
        {
          cmbDatabases.Items.Add(defaultDatabaseName);
          cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
        }
      }
    }

    #endregion

    #region Utilities

    private TreeNode AddNode( TreeNode parentNode, ObjectGroupingItemData data )
    {
      if (data == null)
      {
        return null;
      }

      TreeNodeCollection parentCol = null;
      if (parentNode != null)
      {
        parentCol = parentNode.Nodes;
      }
      else
      {
        parentCol = tv.Nodes;
      }

      TreeNode result = parentCol.Add(data.Name);
      if (!data.DbObjectMissing)
        result.ImageIndex = data.Type ?? -1;
      else
        result.ImageIndex = DBObjectType.Error;

      result.SelectedImageIndex = result.ImageIndex;
      result.Tag = data;
      return result;
    }

    private TreeNode AddEmptyNode( TreeNode parentNode )
    {
      TreeNodeCollection parentCol = null;
      if (parentNode != null)
      {
        parentCol = parentNode.Nodes;
      }
      else
      {
        parentCol = tv.Nodes;
      }

      TreeNode result = parentCol.Add(String.Empty);
      result.ImageIndex = -1;
      result.SelectedImageIndex = result.ImageIndex;
      return result;
    }

    private void UpdateSelectedItemInfo( )
    {
      ObjectGroupingItemData data = SelectedNodeData;
      if (data == null)
      {
        lblItemName.Text = String.Empty;
        lblCreatedBy.Text = String.Empty;
        lblUpdatedBy.Text = String.Empty;
        lblHelpText.Text = "Help Text {?}";
      }
      else
      {
        lblItemName.Text = "Item Name: " + data.Name;
        lblCreatedBy.Text = "Created By: " + data.CreatedBy;
        lblUpdatedBy.Text = "Updated By: " + data.UpdatedBy;
        lblHelpText.Text = "Help Text {" + data.Name + "}";
      }
    }

    private void RaiseConnectionParamsChangedEvent( bool hasError )
    {
      if (_connectionParamsChanged != null)
      {
        _connectionParamsChanged(this, cmbServers.Text, cmbDatabases.Text, hasError);
      }
    }

    #endregion

    #region Object Grouping

    public void LoadInitial( )
    {
      try
      {
        tv.BeginUpdate();
        tv.Nodes.Clear();
        TreeNode node = null;
        IList<ObjectGroupingItemData> children = _grpFacade.GetChildren(null);
        foreach (ObjectGroupingItemData data in children)
        {
          node = AddNode(null, data);
          if (data.Type == DBObjectType.GroupingFolderY)
          {
            AddEmptyNode(node);
          }
        }

        if (tv.Nodes.Count > 0)
        {
          tv.Nodes[0].Expand();
          LoadHelpText(tv.Nodes[0]);
        }
      }
      finally
      {
        UpdateSelectedItemInfo();
        tv.EndUpdate();
      }
    }

    public void LoadChildren( TreeNode node, bool forceRefresh )
    {
      ObjectGroupingItemData data = ObjectGroupingItemDataFactory.GetNodeData(node);
      if (data == null)
      {
        return;
      }

      if ((data.Populated && !forceRefresh) || data.Type != DBObjectType.GroupingFolderY)
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        TreeNode childNode = null;
        node.Nodes.Clear();
        IList<ObjectGroupingItemData> children = _grpFacade.GetChildren(data.ID);
        foreach (ObjectGroupingItemData childData in children)
        {
          childNode = AddNode(node, childData);
          data.Populated = true;
          if (childData.Type == DBObjectType.GroupingFolderY)
          {
            AddEmptyNode(childNode);
          }
        }
      }
      finally
      {
        UpdateSelectedItemInfo();
        tv.EndUpdate();
      }
    }

    public void AddFolderUnderSelectedNode( )
    {
      AddFolder(SelectedNode);
    }

    public void AddFolder( TreeNode parentNode )
    {
      string name = String.Empty;
      int? parentID = null;

      ObjectGroupingItemData parentData = ObjectGroupingItemDataFactory.GetNodeData(parentNode);

      if (parentData != null)
      {
        parentID = parentData.ID;
      }

      DialogResult dlgRes = InputDialog.ShowDialog("New Folder", "Folder Name", ref name);
      if (dlgRes != DialogResult.OK || String.IsNullOrEmpty(name))
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        ObjectGroupingItemData newFolderData = ObjectGroupingItemDataFactory.Create(name, DBObjectType.GroupingFolderY, null, String.Empty, parentID, _connParams.CurrentUsername);
        newFolderData.ParentID = parentID;
        try
        {
          _grpFacade.AddItem(newFolderData);
          TreeNode newFolder = AddNode(parentNode, newFolderData);
          AddEmptyNode(newFolder);

          if (parentNode != null && !parentNode.IsExpanded)
          {
            parentNode.Expand();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      finally
      {
        UpdateSelectedItemInfo();
        tv.EndUpdate();
      }
    }

    public void AddObjectFromObjectExplorer( TreeNode source, TreeNode dropNode )
    {
      if (source == null)
      {
        return;
      }

      NodeData sourceData = NodeDataFactory.GetNodeData(source);
      if (sourceData == null)
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        ObjectGroupingItemData dropData = ObjectGroupingItemDataFactory.GetNodeData(dropNode);

        int? parentID = null;
        TreeNode parentNode = null;


        if (dropNode != null && dropData != null)
        {
          if (dropData.Type == DBObjectType.GroupingFolderY)
          {
            parentID = dropData.ID;
            parentNode = dropNode;
          }
          else
          {
            parentID = dropData.ParentID;
            parentNode = dropNode.Parent;
          }
        }

        string tableName = String.Empty;
        if (sourceData.Type == DBObjectType.Trigger)
        {
          tableName = sourceData.ParentName;
        }


        ObjectGroupingItemData data = ObjectGroupingItemDataFactory.Create(sourceData.Name, sourceData.Type, null, tableName, parentID, _connParams.CurrentUsername);
        try
        {
          _grpFacade.AddItem(data);
          AddNode(parentNode, data);
          if (parentNode != null && !parentNode.IsExpanded)
          {
            parentNode.Expand();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

      }
      finally
      {
        tv.EndUpdate();
        UpdateSelectedItemInfo();
      }
    }

    public void ChangeNodeParent( TreeNode source, TreeNode dropNode )
    {
      if (source == null)
      {
        return;
      }

      ObjectGroupingItemData sourceData = ObjectGroupingItemDataFactory.GetNodeData(source);
      if (sourceData == null)
      {
        return;
      }

      try
      {
        int? parentID = null;
        TreeNode parentNode = null;

        ObjectGroupingItemData dropData = ObjectGroupingItemDataFactory.GetNodeData(dropNode);
        if (dropNode != null && dropData != null)
        {
          parentID = dropData.ID;
          parentNode = dropNode;
        }


        tv.BeginUpdate();

        try
        {
          sourceData.ParentID = parentID;
          _grpFacade.UpdateItem(sourceData);

					if (source.Parent != null)
						source.Parent.Nodes.Remove(source);
					else
						tv.Nodes.Remove(source);

          if (parentNode != null)
          {
            parentNode.Nodes.Add(source);
            if (!parentNode.IsExpanded)
            {
              parentNode.Expand();
            }
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      finally
      {
        tv.EndUpdate();
      }

    }

    public void SaveHelpTextIfChanged( bool confirmSave )
    {
      if (!_helpTextChanged)
      {
        return;
      }
      SaveHelpText(confirmSave);
    }

    public void SaveHelpText( bool confirmSave )
    {
      TreeNode selNode = SelectedNode;
      ObjectGroupingItemData selData = ObjectGroupingItemDataFactory.GetNodeData(SelectedNode);
      if (selData == null)
      {
        return;
      }

      if (confirmSave)
      {
        DialogResult dlgRes = MessageBox.Show("Save changes to help text?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dlgRes == DialogResult.No)
        {
          _helpTextChanged = false;
          return;
        }
      }

      selData.HelpText = CurrentHelpText;
      try
      {
        _grpFacade.UpdateHelpText(selData);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      UpdateSelectedItemInfo();
      _helpTextChanged = false;
    }

    private void LoadHelpText( TreeNode node )
    {
      ObjectGroupingItemData nodeData = ObjectGroupingItemDataFactory.GetNodeData(node);
      if (nodeData == null)
      {
        CurrentHelpText = String.Empty;
        _helpTextChanged = false;
        HelpTextVisible = false;
        lblHelpText.Text = "Help Text {?}";
        return;
      }
      _isInitializing = true;
      _rtbHelpText.RichTextBox.Rtf = nodeData.HelpText;
      _isInitializing = false;
    }

    public bool CanPerformFolderOnlyAction( )
    {
      ObjectGroupingItemData data = SelectedNodeData;
      return (data != null && (data.Type == DBObjectType.GroupingFolderY));
    }

    public void RefreshFolder( )
    {
      if (SelectedNode != null)
      {
        LoadChildren(SelectedNode, true);
      }
      else
      {
        LoadInitial();
      }
    }

    public bool CanDeleteItem( )
    {
      return (SelectedNodeData != null);
    }

    private void DeleteItem( TreeNode node )
    {
      ObjectGroupingItemData data = ObjectGroupingItemDataFactory.GetNodeData(node);

      if (node == null || data == null)
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        _grpFacade.DeleteItem(data);

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
      }
      finally
      {
        tv.EndUpdate();
      }
    }

    public void DeleteSelectedItem( )
    {
      if (SelectedNode == null)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Remove selected item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dlgRes == DialogResult.No)
      {
        return;
      }
      DeleteItem(SelectedNode);
    }

    public void DeleteSelectedItems( )
    {
      if (tv.SelectedNodes.Count == 0)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Remove selected item(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dlgRes == DialogResult.No)
      {
        return;
      }
      try
      {
        foreach (TreeNode node in tv.SelectedNodes)
        {
          DeleteItem(node);
        }
				tv.SelectedNodes.Clear();
				tv.SelectedNode = null;
      }
      finally
      {
        LoadHelpText(SelectedNode);
        UpdateSelectedItemInfo();
      }
    }

    public void RenameItem( )
    {
      string name = String.Empty;
      string prevName = String.Empty;

      ObjectGroupingItemData selNodeData = SelectedNodeData;
      TreeNode selNode = SelectedNode;

      if (selNode == null || selNodeData == null)
      {
        return;
      }

      try
      {
        name = selNodeData.Name;
        prevName = name;
        DialogResult dlgRes = InputDialog.ShowDialog("Rename Folder", "Folder Name", ref name);
        if (dlgRes != DialogResult.OK || String.IsNullOrEmpty(name))
        {
          return;
        }
        tv.BeginUpdate();
        try
        {
          selNodeData.Name = name;
          _grpFacade.UpdateItem(selNodeData);
          selNode.Text = name;
        }
        catch (Exception ex)
        {
          selNodeData.Name = prevName;
          MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      finally
      {
        UpdateSelectedItemInfo();
        tv.EndUpdate();
      }
    }

    public bool CanShowHelpText( )
    {
      if (SelectedNode == null && HelpTextVisible)
      {
        return false;
      }
      return (SelectedNode != null);
    }

    public bool CanPerformSaveHelpText( )
    {
      return (SelectedNode != null && _helpTextChanged);
    }

    #endregion

    #region Edit Actions

    public bool CanPerformEditOperation( )
    {
      return (HelpTextVisible && (_rtbHelpText != null));
    }



    public void PerformCopy( )
    {
      if (!HelpTextVisible)
      {
        return;
      }
      _rtbHelpText.RichTextBox.Copy();
    }

    public void PerformCut( )
    {
      if (!HelpTextVisible)
      {
        return;
      }
      _rtbHelpText.RichTextBox.Cut();

    }

    public void PerformPaste( )
    {
      if (!HelpTextVisible)
      {
        return;
      }
      _rtbHelpText.RichTextBox.Paste();
    }

    #endregion

    #region Object Editing And Viewing

    public bool CanModifySelectedObjects( )
    {
      bool result = false;

      if (tv.SelectedNodes.Count == 0)
      {
        return result;
      }

      foreach (TreeNode node in tv.SelectedNodes)
      {
        ObjectGroupingItemData data = ObjectGroupingItemDataFactory.GetNodeData(node);
        if (data == null)
        {
          continue;
        }

        if (DBConstants.DoesObjectTypeHasScript(data.Type ?? -1))
        {
          result = true;
          break;
        }

      }

      return result;
    }

    public bool CanOpenSelectedObjects( )
    {
      bool result = false;

      if (tv.SelectedNodes.Count == 0)
      {
        return result;
      }

      foreach (TreeNode node in tv.SelectedNodes)
      {
        ObjectGroupingItemData data = ObjectGroupingItemDataFactory.GetNodeData(node);
        if (data == null)
        {
          continue;
        }

        if (DBConstants.DoesObjectTypeHoldsData(data.Type ?? -1))
        {
          result = true;
          break;
        }

      }

      return result;
    }

    public void ModifySelectedObjects( )
    {
      string error = String.Empty;

      IList<frmScriptEditor> editors = new List<frmScriptEditor>();
      foreach (TreeNode node in tv.SelectedNodes)
      {
        ObjectGroupingItemData data = ObjectGroupingItemDataFactory.GetNodeData(node);
        if (data == null)
        {
          continue;
        }

        ObjectInfo objInfo = ProgrammabilityHelper.GetObjectInfo(_connParams,String.Empty, data.Name);
        if (objInfo == null)
        {
          error += " - " + data.Name + "\r\n";
          continue;
        }

        if (!DBConstants.DoesObjectTypeHasScript(data.Type ?? -1))
        {
          continue;
        }
        string script = ScriptingHelper.GetAlterScript(_connParams,_connParams.Database, objInfo.ObjectID, objInfo.ObjectType);
        frmScriptEditor editor = ScriptEditorFactory.Create(objInfo.ObjectName, script, objInfo.ObjectID, objInfo.ObjectType, _connParams, cmbDatabases.Text);
        editors.Add(editor);
      }

      foreach (frmScriptEditor editor in editors)
      {
        ScriptEditorFactory.ShowScriptEditor(editor);
      }

      if (!String.IsNullOrEmpty(error))
      {
        MessageService.ShowError("Objects listed below do not exist in the database!\r\n" + error);
      }
    }

    public void OpenSelectedObjects( )
    {
      string error = String.Empty;
      IList<frmDataViewer> viewers = new List<frmDataViewer>();
      foreach (TreeNode node in tv.SelectedNodes)
      {
        ObjectGroupingItemData data = ObjectGroupingItemDataFactory.GetNodeData(node);
        if (data == null)
        {
          continue;
        }

        ObjectInfo objInfo = ProgrammabilityHelper.GetObjectInfo(_connParams,String.Empty, data.Name);
        if (objInfo == null)
        {
          error += " - " + data.Name + "\r\n";
          continue;
        }

        if (!DBConstants.DoesObjectTypeHoldsData(data.Type ?? -1))
        {
          continue;
        }
        string caption = data.Name + "{" + cmbDatabases.Text + " on " + cmbServers.Text + "}";
        string script = " select * from [" + data.Name + "]";
        bool isReadOnly = (data.Type == DBObjectType.View) ? true : false;

        frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, cmbDatabases.Text, data.Name, caption, script, isReadOnly, false);
        viewers.Add(viewer);
      }

      foreach (frmDataViewer viewer in viewers)
      {
        viewer.LoadData(true);
        DataViewerFactory.ShowDataViewer(viewer);
      }

      if (!String.IsNullOrEmpty(error))
      {
        MessageService.ShowError("Objects listed below do not exist in the database!\r\n" + error);
      }

    }

    #endregion

    private void tv_BeforeExpand( object sender, TreeViewCancelEventArgs e )
    {
      LoadChildren(e.Node, false);
    }

    private void cmbServers_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      ConnParams = _cpList[cmbServers.SelectedIndex];
      PopulateDatabases(ConnParams.Database);
    }

    private void cmbDatabases_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      ConnParams.Database = cmbDatabases.Text;
      InitializeObjectGrouping(ConnParams);
    }

    private void lblCloseOutputPane_Click( object sender, EventArgs e )
    {
      HelpTextVisible = false;
    }


    private void ucObjectGrouping_Load( object sender, EventArgs e )
    {
      HelpTextVisible = false;
    }

    private void tv_DragOver( object sender, DragEventArgs e )
    {

      Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;

      if (sourceNodes == null || sourceNodes.Count == 0)
      {
        e.Effect = DragDropEffects.None;
        return;
      }

      TreeNode firstNode = null;
      foreach (TreeNode node in sourceNodes.Values)
      {
        firstNode = node;
        if (firstNode != null)
        {
          break;
        }
      }

			if (firstNode == null)
			{
				e.Effect = DragDropEffects.None;
				return;			
			}

			Point pos = new Point();
			pos.X = e.X;
			pos.Y = e.Y;
			pos = tv.PointToClient(pos);

			TreeNode dropNode = tv.GetNodeAt(pos);
			if (dropNode == null)
			{
				e.Effect = DragDropEffects.None;
				return;						
			}

      // This is an object from ObjectExplorer
      if (firstNode.Tag is NodeData)
      {
        foreach (TreeNode node in sourceNodes.Values)
        {
          NodeData sourceData = NodeDataFactory.GetNodeData(node);
          if (sourceData == null || sourceData.ConnParams == null || _connParams == null)
          {
            e.Effect = DragDropEffects.None;
            return;
          }

          if (
              (sourceData.ConnParams.Server.ToLowerInvariant() != _connParams.Server.ToLowerInvariant())
              ||
              (sourceData.DBName.ToLowerInvariant() != _connParams.Database.ToLowerInvariant())
            )
          {
            e.Effect = DragDropEffects.None;
            return;
          }

        }
        e.Effect = DragDropEffects.Copy;
      }
      // This is a node from own MWTreeView
      else if (firstNode.Tag is ObjectGroupingItemData)
      {

		

				ObjectGroupingItemData dropData = ObjectGroupingItemDataFactory.GetNodeData(dropNode);
				ObjectGroupingItemData firstNodeData = ObjectGroupingItemDataFactory.GetNodeData(firstNode);

				if (dropData == null || (firstNodeData.ID == dropData.ParentID || firstNodeData.ID == dropData.ID))
				{
					e.Effect = DragDropEffects.None;
					return;
				}

        if (dropData == null || dropData.Type != DBObjectType.GroupingFolderY)
        {
          e.Effect = DragDropEffects.None;
          return;
        }

        e.Effect = DragDropEffects.Move;
      }
      // Unknown 
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void tv_DragDrop( object sender, DragEventArgs e )
    {
      Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;

      if (sourceNodes == null || sourceNodes.Count == 0)
      {
        e.Effect = DragDropEffects.None;
        return;
      }
    
      TreeNode firstNode = null;
      foreach (TreeNode node in sourceNodes.Values)
      {
        firstNode = node;
        if (firstNode != null)
        {
          break;
        }
      }

			if (firstNode == null )
			{
				e.Effect = DragDropEffects.None;
				return;
			}

			Point pos = new Point();
			pos.X = e.X;
			pos.Y = e.Y;
			pos = tv.PointToClient(pos);

			TreeNode dropNode = tv.GetNodeAt(pos);

      if (firstNode.Tag is NodeData)
      {
        foreach (TreeNode node in sourceNodes.Values)
        {
          AddObjectFromObjectExplorer(node, dropNode);
        }
        e.Effect = DragDropEffects.Copy;
				tv.SelectedNodes.Clear();
			}
      // This is a node from own MWTreeView
      else if (firstNode.Tag is ObjectGroupingItemData)
      {
			
				ObjectGroupingItemData dropData = ObjectGroupingItemDataFactory.GetNodeData(dropNode);
				ObjectGroupingItemData firstNodeData = ObjectGroupingItemDataFactory.GetNodeData(firstNode);

				if (dropData != null && firstNodeData != null)
				{
					List<string> parents = _grpFacade.GetAllParents(dropData.ID.Value);
					if (parents != null && parents.Contains(firstNodeData.ID.ToString()))
					{
						MessageBox.Show(String.Format("Source node '{0}' is parent of the target node '{1}'!", firstNodeData.Name, dropData.Name), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				e.Effect = DragDropEffects.Move;
        foreach (TreeNode node in sourceNodes.Values)
        {
          ChangeNodeParent(node, dropNode);
        }
				tv.SelectedNodes.Clear();
      }
    }

    private void tv_ItemDrag( object sender, ItemDragEventArgs e )
    {
			DoDragDrop(tv.SelectedNodes, DragDropEffects.Move);
    }


    private void expandToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (SelectedNode == null)
      {
        return;
      }
      SelectedNode.Expand();
    }

    private void expandAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (SelectedNode == null)
      {
        return;
      }
      SelectedNode.ExpandAll();
    }

    private void collapseToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (SelectedNode == null)
      {
        return;
      }
      SelectedNode.Collapse(true);

    }

    private void collapseAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (SelectedNode == null)
      {
        return;
      }
      SelectedNode.Collapse(false);
    }

		private void tv_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			SaveHelpTextIfChanged(true);
		}

    private void tv_AfterSelect( object sender, TreeViewEventArgs e )
    {
			LoadHelpText(SelectedNode);
			UpdateSelectedItemInfo();
    }

    private void tv_DoubleClick( object sender, EventArgs e )
    {
      if (CanModifySelectedObjects())
      {
        ModifySelectedObjects();
        return;
      }

      if (CanOpenSelectedObjects())
      {
        OpenSelectedObjects();
        return;
      }
    }




  }//Class end

  public delegate void ConnectionParamsChangedEventHandler( object sender, string serverName, string databaseName, bool hasError );

}//Namespace end
