/********************************************************************
  Class      : ucSharedSnippets
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Crad.Windows.Forms.Actions;
using RichTextBoxEx;
using MWControls;
using MWCommon;
using ICSharpCode.Core;

using PragmaSQL.Core;
using PragmaSQL.WebBrowserEx;

namespace PragmaSQL
{
  public enum SharedSnippetMode
  {
    Edit,
    Open,
    Save
  }

  public partial class ucSharedSnippets : UserControl, ISharedSnippetsViewerService
  {
    private RichTextEditor _rtbDescription = null;
    private SharedSnippetsService _sharedSnippetsFacade = new SharedSnippetsService();
    private bool _isInitializing = false;

    public ucSharedSnippets( )
    {
      InitializeComponent();


      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedSnippetsOptions != null)
      {
        DescriptionVisible = ConfigHelper.Current.SharedSnippetsOptions.AlwaysShowDescription;
        btnShowDescription.Checked = DescriptionVisible;
      }
      else
      {
        DescriptionVisible = false;
      }

      InitializeDescriptionControl();
      InitActions();
      //InitializeAddInSupport();
      tv.TreeViewNodeSorter = new SharedSnippetNodeSorter();

    }

    #region AddIn Support
    internal void InitializeAddInSupport( )
    {
      MenuService.AddItemsToMenu(popUpTv.Items, this, "/Workspace/SharedSnippetsViewer/ContextMenu");
      ToolStrip toolbar = ToolbarService.CreateToolStrip(this, "/Workspace/SharedSnippetsViewer/Toolbar");
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

    #region Initialization

    public void InitializeSharedSnippets( ConnectionParams connParams )
    {
      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedSnippetsOptions != null)
      {
        tv.ShowNodeToolTips = ConfigHelper.Current.SharedSnippetsOptions.ShowItemToolTip;
      }

      try
      {
        _isInitializing = true;
        if (connParams == null)
        {
          Disabled = true;
          return;
        }

        ConnParams = connParams;
        using (SqlConnection conn = new SqlConnection(ConnParams.ConnectionString))
        {
          try
          {
            conn.Open();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Can not load shared code snippets!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Disabled = true;
            ConnParams = null;
            return;
          }
        }

        Disabled = false;
        LoadSharedSnippets();
      }
      finally
      {
        _isInitializing = false;
      }
    }

    private void InitializeDescriptionControl( )
    {
      if (_rtbDescription != null)
      {
        return;
      }

      _rtbDescription = new RichTextEditor();
      panDescription.Controls.Add(_rtbDescription);
      _rtbDescription.Parent = panDescription;
      _rtbDescription.Dock = DockStyle.Fill;
      _rtbDescription.BringToFront();
      _rtbDescription.ShowOpen = false;
      _rtbDescription.ShowSave = false;
      _rtbDescription.ShowInsertHyperlink = false;
      _rtbDescription.AcceptsTab = true;
      _rtbDescription.RichTextBox.DetectUrls = true;
      _rtbDescription.RichTextBox.TextChanged += new EventHandler(OnHelpTextChanged);
      _rtbDescription.RichTextBox.LinkClicked += new LinkClickedEventHandler(RichTextBox_LinkClicked);
      _rtbDescription.RichTextBox.Show();
    }

    void RichTextBox_LinkClicked( object sender, LinkClickedEventArgs e )
    {
      frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, e.LinkText);
      WebBrowserFactory.ShowWebBrowser(frm);
    }

    void OnHelpTextChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      _descriptionChanged = true;
    }

    #endregion

    #region Initialize Actions

    private ActionList _actionList = new ActionList();

    private void InitActions( )
    {
     Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.new_case;
      ac.Update +=new EventHandler(OnAction_NewRootFolder_Update);
      ac.Execute += new EventHandler(OnAction_NewRootFolder_Execute);
      ac.Text = "New Root Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewRootFolder, ac);
      _actionList.SetAction(popUpTvNewRootFolder, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.NewFolder;
      ac.Update +=new EventHandler(OnAction_NewSubFolder_Update);
      ac.Execute += new EventHandler(OnAction_NewSubFolder_Execute);
      ac.Text = "New Sub Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewSubFolder, ac);
      _actionList.SetAction(popUpTvNewSubFolder, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.AddToFavorites;
      ac.Update +=new EventHandler(OnAction_NewSnippet_Update);
      ac.Execute += new EventHandler(OnAction_NewSnippet_Execute);
      ac.Text = "New Snippet";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewSnippet, ac);
      _actionList.SetAction(popUpTvNewSnippet, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      //ac.ShortcutKeys = Keys.F5;
      ac.Image = PragmaSQL.Properties.Resources.Refresh;
      ac.Update +=new EventHandler(OnAction_Refresh_Update);
      ac.Execute += new EventHandler(OnAction_Refresh_Execute);
      ac.Text = "Refresh";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnRefresh, ac);
      _actionList.SetAction(popUpTvRefresh, ac);


     ac = new Crad.Windows.Forms.Actions.Action();
      //ac.ShortcutKeys = Keys.Control | Keys.F5;
      ac.Image = PragmaSQL.Properties.Resources.reload24bit;
      ac.Update +=new EventHandler(OnAction_LoadInitial_Update);
      ac.Execute += new EventHandler(OnAction_LoadInitial_Execute);
      ac.Text = "Reload";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnReload, ac);
      _actionList.SetAction(popUpTvReload, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      //ac.ShortcutKeys = Keys.Control | Keys.Delete;
      ac.Image = PragmaSQL.Properties.Resources.delete;
      ac.Update +=new EventHandler(OnAction_DeleteItem_Update);
      ac.Execute += new EventHandler(OnAction_DeleteItem_Execute);
      ac.Text = "Delete";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnDelete, ac);
      _actionList.SetAction(popUpTvDelete, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.RenameFolder;
      //ac.ShortcutKeys = Keys.F2;
      ac.Update +=new EventHandler(OnAction_RenameItem_Update);
      ac.Execute += new EventHandler(OnAction_RenameItem_Execute);
      ac.Text = "Rename";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnRename, ac);
      _actionList.SetAction(popUpTvRename, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.help_2;
      ac.Update +=new EventHandler(OnAction_ShowDescription_Update);
      ac.Execute += new EventHandler(OnAction_ShowDescription_Execute);
      ac.Text = "Show Description";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnShowDescription, ac);
      _actionList.SetAction(popUpTvShowDescription, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.SaveAsWebPage;
      //ac.ShortcutKeys = Keys.Control | Keys.S;
      ac.Update +=new EventHandler(OnAction_SaveDescription_Update);
      ac.Execute += new EventHandler(OnAction_SaveDescription_Execute);
      ac.Text = "Save Description";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnSaveDescription, ac);
      _actionList.SetAction(popUpTvSaveDescription, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.edit;
      ac.Update +=new EventHandler(OnAction_EditSnippet_Update);
      ac.Execute += new EventHandler(OnAction_EditSnippet_Execute);
      ac.Text = "Edit";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuPopUpTvOpen, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update +=new EventHandler(OnAction_DiffSnippet_Update);
      ac.Execute += new EventHandler(OnAction_SendSnippetToDiffAsSource_Execute);
      ac.Text = "As Source";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuPopUpTvSendToDiffAsSource, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update +=new EventHandler(OnAction_DiffSnippet_Update);
      ac.Execute += new EventHandler(OnAction_SendSnippetToDiffAsDest_Execute);
      ac.Text = "As Dest";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuPopUpTvSendToDiffAsDest, ac);
    }


    private void OnAction_NewRootFolder_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.NewRootFolder);
    }

    private void OnAction_NewSubFolder_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.NewSubFolder);
      
    }

    private void OnAction_NewSubFolder_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.NewSubFolder);
      AddFolder(SelectedNode);
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.NewSubFolder);
    }

    private void OnAction_NewRootFolder_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.NewRootFolder);
      AddFolder(null);
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.NewRootFolder);
    }

    private void OnAction_NewSnippet_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.NewSnippet);
      
    }

    private void OnAction_NewSnippet_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.NewSnippet);
      AddSnippet(SelectedNode);
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.NewSnippet);
    }

    private void OnAction_Refresh_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.RefreshNode);
    }

    private void OnAction_Refresh_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.RefreshNode);
      LoadChildren(SelectedNode, true);
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.RefreshNode);
    }

    private void OnAction_LoadInitial_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.Reload);
    }

    private void OnAction_LoadInitial_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.Reload);
      LoadInitial();
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.Reload);
    }

    

    private void OnAction_DeleteItem_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.DeleteSelectedItems);
    }

    private void OnAction_DeleteItem_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.DeleteSelectedItems);
      DeleteSelectedItems();
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.DeleteSelectedItems);
    }

    private void OnAction_RenameItem_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.RenameItem);
      
    }

    private void OnAction_RenameItem_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.RenameItem);
      RenameItem();
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.RenameItem);
    }

    private void OnAction_ShowDescription_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      SharedSnippetItemData data = SelectedNodeData;

      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.ShowDescription);
      _rtbDescription.Enabled = ac.Enabled;

      ac.Checked = DescriptionVisible;
    }

    private void OnAction_ShowDescription_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(DescriptionVisible?SharedSnippetsViewerCommand.HideDescription:SharedSnippetsViewerCommand.ShowDescription);
      DescriptionVisible = !DescriptionVisible;
      FireAfterSharedSnippetsViewerAction(DescriptionVisible ? SharedSnippetsViewerCommand.ShowDescription : SharedSnippetsViewerCommand.HideDescription);
    }

    private void OnAction_SaveDescription_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.SaveDescription);
    }

    private void OnAction_SaveDescription_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.SaveDescription);
      SaveDescriptionIfCahanged(true);
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.SaveDescription);
    }

    private void OnAction_DiffSnippet_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      SharedSnippetItemData data = SelectedNodeData;

      ac.Enabled = (_connParams != null && (data != null && data.Type == GenericItemType.Item));
    }

    private void OnAction_SendSnippetToDiffAsSource_Execute( object sender, EventArgs e )
    {
      SendSelectedSnippetToDiff(true);
    }

    private void OnAction_SendSnippetToDiffAsDest_Execute( object sender, EventArgs e )
    {
      SendSelectedSnippetToDiff(false);
    }

    private void OnAction_EditSnippet_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedSnippetsViewerCommand.EditSnippet);
    }

    private void OnAction_EditSnippet_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedSnippetsViewerAction(SharedSnippetsViewerCommand.EditSnippet);
      EditSnippet();
      FireAfterSharedSnippetsViewerAction(SharedSnippetsViewerCommand.EditSnippet);
    }

    
    #endregion

    #region Properties
    private SharedSnippetMode _mode = SharedSnippetMode.Edit;
    public SharedSnippetMode Mode
    {
      get { return _mode; }
      set
      {
        _mode = value;
        ApplyMode();
      }
    }

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
      private set
      {
        if (value == null)
        {
          _connParams = null;
        }
        else
        {
          _connParams = value.CreateCopy();
        }
        _sharedSnippetsFacade.ConnParams = value;
      }
    }

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

    public SharedSnippetItemData SelectedNodeData
    {
      get
      {
        return SharedSnippetItemDataFactory.GetNodeData(SelectedNode);
      }
    }

    private bool _descriptionVisible = false;
    [Browsable(false)]

    public bool DescriptionVisible
    {
      get
      {
        return _descriptionVisible;
      }
      set
      {
        panDescription.Visible = value;
        splitterDescription.Visible = value;
        splitterDescription.BringToFront();
        tv.BringToFront();
        btnShowDescription.Checked = value;
        _descriptionVisible = value;
      }
    }

    private bool _descriptionChanged = false;
    public bool DescriptionChanged
    {
      get { return _descriptionChanged; }
    }

    private string CurrentDescription
    {
      get
      {
        if (_rtbDescription == null)
        {
          return String.Empty;
        }
        else
        {
          return _rtbDescription.RichTextBox.Rtf;
        }
      }
      set
      {
        if (_rtbDescription != null)
        {
          _rtbDescription.RichTextBox.Rtf = value;
          _rtbDescription.RichTextBox.Invalidate();
          _rtbDescription.Refresh();
        }
      }
    }

    private bool _disabled = false;
    [Browsable(false)]
    public bool Disabled
    {
      get
      {
        return _disabled;
      }
      set
      {
        if (value)
        {
          tv.Nodes.Clear();
          DescriptionVisible = !value;
        }

        toolStrip1.Enabled = !value;
        panDescription.Enabled = !value;
        tv.Enabled = !value;
        _disabled = value;
      }
    }

    private ConnectionParamsCollection AvailableConnections
    {
      get
      {
        return ConnectionParamsFactory.GetConnections();
      }
    }

    #endregion

    #region Utilities

    private TreeNode AddNode( TreeNode parentNode, SharedSnippetItemData data )
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
      result.ImageIndex = data.Type ?? -1;
      result.SelectedImageIndex = result.ImageIndex;
      result.Tag = data;
      result.ToolTipText = data.ToolTipText;
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
      SharedSnippetItemData data = SelectedNodeData;

      if (data == null)
      {
        lblItemName.Text = String.Empty;
        lblCreatedBy.Text = String.Empty;
        lblUpdatedBy.Text = String.Empty;
        kryptonHeader1.Text = "Description {?}";
        _descriptionChanged = false;
      }
      else
      {
        lblItemName.Text = "Name: " + data.Name;
        lblCreatedBy.Text = "Created By: " + data.CreatedBy;
        if (!data.CreatedOn.HasValue)
        {
          lblCreatedOn.Text = "Created On: ";
        }
        else
        {
          lblCreatedOn.Text = "Created On: " + data.CreatedOn.Value.ToString();
        }
        lblUpdatedBy.Text = "Updated By: " + data.UpdatedBy;
        kryptonHeader1.Text = "Description {" + data.Name + "}";
      }
    }

    public void ApplyMode( )
    {
      //TODO:
      switch (_mode)
      {
        case SharedSnippetMode.Edit:
          break;
        case SharedSnippetMode.Save:
          break;
        case SharedSnippetMode.Open:
          break;
        default:
          break;
      }
    }
    #endregion

    #region Snippet operations

    public void LoadInitial( )
    {
      if (_connParams == null)
      {
        if (!LoadFromDefaultConnection(true))
        {
          return;
        }
      }
      else
      {
        LoadSharedSnippets();
      }
    }

    public void LoadSharedSnippets( )
    {
      try
      {
        _isInitializing = true;
        tv.BeginUpdate();
        tv.Nodes.Clear();
        TreeNode node = null;
        IList<SharedSnippetItemData> children = _sharedSnippetsFacade.GetChildren(null);
        foreach (SharedSnippetItemData data in children)
        {
          node = AddNode(null, data);
          if (data.Type == GenericItemType.Folder)
          {
            AddEmptyNode(node);
          }
        }

        if (tv.Nodes.Count > 0)
        {
          tv.Nodes[0].Expand();
          SelectedNode = tv.Nodes[0];
          LoadItemDescription(tv.Nodes[0]);
        }
      }
      finally
      {
        UpdateSelectedItemInfo();
        tv.Sort();
        tv.EndUpdate();
        _isInitializing = false;
      }
    }

    public void LoadChildren( TreeNode node, bool forceRefresh )
    {
      SharedSnippetItemData data = SharedSnippetItemDataFactory.GetNodeData(node);
      if (data == null)
      {
        return;
      }

      if ((data.Populated && !forceRefresh) || data.Type != GenericItemType.Folder)
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        TreeNode childNode = null;
        node.Nodes.Clear();
        IList<SharedSnippetItemData> children = _sharedSnippetsFacade.GetChildren(data.ID);
        foreach (SharedSnippetItemData childData in children)
        {
          childNode = AddNode(node, childData);
          data.Populated = true;
          if (childData.Type == GenericItemType.Folder)
          {
            AddEmptyNode(childNode);
          }
        }
      }
      finally
      {
        UpdateSelectedItemInfo();
        tv.Sort();
        tv.EndUpdate();
      }
    }

    private void AddItem( TreeNode parentNode, int? type, string name, string snippet )
    {
      int? parentID = null;

      SharedSnippetItemData parentData = SharedSnippetItemDataFactory.GetNodeData(parentNode);

      if (parentData != null)
      {
        parentID = parentData.ID;
      }


      try
      {
        tv.BeginUpdate();
        SharedSnippetItemData newItemData = SharedSnippetItemDataFactory.Create(name, type, null, parentID, _connParams.CurrentUsername);
        newItemData.Snippet = snippet;
        newItemData.ParentID = parentID;
        newItemData.CreatedBy = _connParams.CurrentUsername;
        try
        {
          _sharedSnippetsFacade.AddItem(newItemData);
          TreeNode newItem = AddNode(parentNode, newItemData);
          if (newItemData.Type == GenericItemType.Folder)
          {
            AddEmptyNode(newItem);
          }

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
        tv.Sort();
        tv.EndUpdate();
      }
    }

    private void AddItem( TreeNode parentNode, int? type, string name )
    {
      AddItem(parentNode, type, name, String.Empty);
    }

    private void AddFolder( TreeNode parentNode )
    {
      string name = String.Empty;

      DialogResult dlgRes = InputDialog.ShowDialog("New Folder", "Folder Name", ref name);
      if (dlgRes != DialogResult.OK || String.IsNullOrEmpty(name))
      {
        return;
      }
      AddItem(parentNode, GenericItemType.Folder, name);
    }

    private void AddSnippet( TreeNode parentNode )
    {
      AddSnippet(parentNode, String.Empty);
    }

    private void AddSnippet( TreeNode parentNode, string snippet )
    {
      string name = String.Empty;

      DialogResult dlgRes = InputDialog.ShowDialog("New Snippet", "Snippet Name", ref name);
      if (dlgRes != DialogResult.OK || String.IsNullOrEmpty(name))
      {
        return;
      }
      AddItem(parentNode, GenericItemType.Item, name, snippet);
    }

    private void DeleteItem( TreeNode node )
    {
      SharedSnippetItemData data = SharedSnippetItemDataFactory.GetNodeData(node);

      if (node == null || data == null)
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        _sharedSnippetsFacade.DeleteItem(data.ID);

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
        tv.Sort();
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
      try
      {
        DeleteItem(SelectedNode);
        tv.DeselectNode(SelectedNode, true);
      }
      finally
      {
        LoadItemDescription(SelectedNode);
        UpdateSelectedItemInfo();
        tv.Sort();
      }
    }

    public void DeleteSelectedItems( )
    {
      if (tv.SelNodes.Count == 0)
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
        foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
        {
          DeleteItem(nodeWrapper.Node);
        }

        tv.ClearSelNodes();
        tv.DeselectNode(SelectedNode, true);
      }
      finally
      {
        LoadItemDescription(SelectedNode);
        UpdateSelectedItemInfo();
        tv.Sort();
      }
    }

    public void RenameItem( )
    {
      string name = String.Empty;
      string prevName = String.Empty;

      SharedSnippetItemData selNodeData = SelectedNodeData;
      TreeNode selNode = SelectedNode;

      if (selNode == null || selNodeData == null)
      {
        return;
      }

      try
      {
        name = selNodeData.Name;
        prevName = name;
        DialogResult dlgRes = InputDialog.ShowDialog("Rename Item", "New Name", ref name);
        if (dlgRes != DialogResult.OK || String.IsNullOrEmpty(name))
        {
          return;
        }

        tv.BeginUpdate();
        try
        {
          selNodeData.Name = name;
          _sharedSnippetsFacade.UpdateItem(selNodeData);
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
        tv.Sort();
        tv.EndUpdate();
      }
    }

    public void LoadItemDescription( TreeNode node )
    {
      SharedSnippetItemData nodeData = SharedSnippetItemDataFactory.GetNodeData(node);
      if (nodeData == null)
      {
        kryptonHeader1.Text = "Description {?}";
        CurrentDescription = String.Empty;
        _descriptionChanged = false;
        return;
      }
      _isInitializing = true;
      CurrentDescription = nodeData.Description;
      UpdateSelectedItemInfo();
      _isInitializing = false;
    }

    public void SaveDescriptionIfCahanged( bool confirmSave )
    {
      if (!_descriptionChanged)
      {
        return;
      }
      SaveDescription(confirmSave);
    }

    public void SaveDescription( bool confirmSave )
    {
      TreeNode selNode = SelectedNode;
      SharedSnippetItemData selData = SharedSnippetItemDataFactory.GetNodeData(SelectedNode);
      if (selData == null)
      {
        return;
      }

      bool confirmDescriptionSave = true;
      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedSnippetsOptions != null)
      {
        confirmDescriptionSave = ConfigHelper.Current.SharedSnippetsOptions.ConfirmDescriptionSave;
      }

      if (confirmSave && confirmDescriptionSave)
      {
        DialogResult dlgRes = MessageBox.Show("Save changes to description?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dlgRes == DialogResult.No)
        {
          _descriptionChanged = false;
          return;
        }
      }

      selData.Description = CurrentDescription;
      try
      {
        _sharedSnippetsFacade.UpdateItem(selData);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      UpdateSelectedItemInfo();
      _descriptionChanged = false;
    }

    public void ChangeNodeParent( TreeNode source, TreeNode dropNode )
    {
      if (source == null)
      {
        return;
      }

      SharedSnippetItemData sourceData = SharedSnippetItemDataFactory.GetNodeData(source);
      if (sourceData == null)
      {
        return;
      }

      try
      {
        int? parentID = null;
        TreeNode parentNode = null;

        SharedSnippetItemData dropData = SharedSnippetItemDataFactory.GetNodeData(dropNode);
        if (dropNode != null && dropData != null)
        {
          parentID = dropData.ID;
          parentNode = dropNode;
        }


        tv.BeginUpdate();

        try
        {
          sourceData.ParentID = parentID;
          _sharedSnippetsFacade.UpdateItem(sourceData);

          if (source.Parent != null)
          {
            source.Parent.Nodes.Remove(source);
          }

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
        tv.Sort();
        tv.EndUpdate();
      }

    }

    public bool LoadFromDefaultConnection( bool showWarning )
    {
			if (!ConfigHelper.Current.SharedSnippetsEnabled())
      {
        if (showWarning)
        {
          MessageBox.Show("Shared code snippets option is not set."
            + "\nPlease enable \"Tools -> Options -> PragmaSQL System -> Use shared code snippets\" option."
            , "Information"
            , MessageBoxButtons.OK
            , MessageBoxIcon.Information);
        }
        return false;
      }

      InitializeSharedSnippets(ConfigHelper.Current.PragmaSqlDbConn);
      return true;
    }

    private void SendSelectedSnippetToDiff( bool isSource )
    {
      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      SharedSnippetItemData itemData = null;
      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        itemData = SharedSnippetItemDataFactory.GetNodeData(nodeWrapper.Node);
        if (itemData == null)
        {
          break;
        }

        string script = itemData.Snippet;


        frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
        if (diffForm == null)
        {
          diffForm = TextDiffFactory.CreateDiff();
        }

        if (isSource)
        {
          diffForm.diffControl.SourceText = script;
          diffForm.diffControl.SourceHeaderText = itemData.Name;
        }
        else
        {
          diffForm.diffControl.DestText = script;
          diffForm.diffControl.DestHeaderText = itemData.Name;
        }
        diffForm.Show();
        diffForm.BringToFront();
        break;
      }
    }

    public void EditSnippet( )
    {
      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      ConnectionParams cp = null;
      if (AvailableConnections.Count > 0)
      {
        cp = AvailableConnections[0];
      }

      bool alwaysUseOfflineEditor = false;
      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedSnippetsOptions != null)
      {
        alwaysUseOfflineEditor = ConfigHelper.Current.SharedSnippetsOptions.AlwaysUseOfflineScriptEditor;
      }

      SharedSnippetItemData itemData = null;
      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        itemData = SharedSnippetItemDataFactory.GetNodeData(nodeWrapper.Node);
        if (itemData == null)
        {
          continue;
        }

        if (cp != null && !alwaysUseOfflineEditor)
        {
          frmScriptEditor frm = ScriptEditorFactory.OpenSharedSnippet(itemData, cp);
          ScriptEditorFactory.ShowScriptEditor(frm);
        }
        else
        {
          frmTextEditor frm = TextEditorFactory.OpenSharedSnippet(itemData);
          TextEditorFactory.ShowTextEditor(frm);
        }
      }
    }

    #endregion

    #region ISharedSnippetsViewerService
    public SharedSnippetItemData SelectedItem
    {
      get { return SelectedNodeData; }
    }

    public IList<SharedSnippetItemData> SelectedItems
    {
      get
      {
        IList<SharedSnippetItemData> result = new List<SharedSnippetItemData>();
        foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
        {
          SharedSnippetItemData data = SharedSnippetItemDataFactory.GetNodeData(nodeWrapper.Node);
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

    public bool CanPerformCommand( SharedSnippetsViewerCommand action )
    {
      SharedSnippetItemData data = SelectedNodeData; 
      bool result = false;
      
      switch (action)
      {
        case SharedSnippetsViewerCommand.None:
          result = false;
          break;
        case SharedSnippetsViewerCommand.Reload:
          result = true;// (data == null);
          break;
        case SharedSnippetsViewerCommand.RefreshNode:
          result = (_connParams != null && (data != null && data.Type == GenericItemType.Folder));
          break;
        case SharedSnippetsViewerCommand.NewRootFolder:
          result = (_connParams != null && (data == null || (data.Type == GenericItemType.Folder)));
          break;
        case SharedSnippetsViewerCommand.NewSubFolder:
          result = (_connParams != null && (data == null || (data.Type == GenericItemType.Folder)));
          break;
        case SharedSnippetsViewerCommand.NewSnippet:
          result = (_connParams != null && (data == null || (data.Type == GenericItemType.Folder)));
          break;
        case SharedSnippetsViewerCommand.RenameItem:
          result = (_connParams != null && data != null);
          break;
        case SharedSnippetsViewerCommand.DeleteSelectedItems:
          result = (_connParams != null && data != null);
          break;
        case SharedSnippetsViewerCommand.SaveDescription:
          result = (_connParams != null && (data != null && _descriptionChanged));
          break;
        case SharedSnippetsViewerCommand.ShowDescription:
          result = (_connParams != null && data != null);
          break;
        case SharedSnippetsViewerCommand.HideDescription:
          result = (_connParams != null && data != null);
          break;
        case SharedSnippetsViewerCommand.EditSnippet:
          result = (_connParams != null && (data != null && data.Type == GenericItemType.Item));
          break;
        default:
          result = false;
          break;
      }

      return result;
      
    }

    public void ExecuteCommand( SharedSnippetsViewerCommand action )
    {
      
      if (!CanPerformCommand(action))
      {
        HostServicesSingleton.HostServices.MsgService.ErrorMsg(String.Format("Selecte node(s) state is invalid! Can not perform required action: \"{0}\".", action), (MethodInfo)MethodInfo.GetCurrentMethod());
        HostServicesSingleton.HostServices.MsgService.ShowMessages();
      }

      switch (action)
      {
        case SharedSnippetsViewerCommand.None:
          break;
        case SharedSnippetsViewerCommand.Reload:
          LoadInitial();
          break;
        case SharedSnippetsViewerCommand.RefreshNode:
          LoadChildren(SelectedNode, true);
          break;
        case SharedSnippetsViewerCommand.NewRootFolder:
          AddFolder(null);
          break;
        case SharedSnippetsViewerCommand.NewSubFolder:
          AddFolder(SelectedNode);
          break;
        case SharedSnippetsViewerCommand.NewSnippet:
          AddSnippet(SelectedNode);
          break;
        case SharedSnippetsViewerCommand.RenameItem:
          RenameItem();
          break;
        case SharedSnippetsViewerCommand.DeleteSelectedItems:
          DeleteSelectedItems();
          break;
        case SharedSnippetsViewerCommand.SaveDescription:
          SaveDescriptionIfCahanged(true);
          break;
        case SharedSnippetsViewerCommand.ShowDescription:
          DescriptionVisible = true;
          break;
        case SharedSnippetsViewerCommand.HideDescription:
          DescriptionVisible  = false;
          break;
        case SharedSnippetsViewerCommand.EditSnippet:
          EditSnippet();
          break;
        default:
          break;
      }
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

    private BeforeSharedSnippetsViewerActionDelegate _beforeSharedSnippetsViewerAction;
    public event BeforeSharedSnippetsViewerActionDelegate BeforeSharedSnippetsViewerAction
    {
      add { _beforeSharedSnippetsViewerAction += value; }
      remove { _beforeSharedSnippetsViewerAction -= value; }
    }
    private void FireBeforeSharedSnippetsViewerAction( SharedSnippetsViewerCommand action )
    {
      if (_beforeSharedSnippetsViewerAction == null)
      {
        return;
      }

      Delegate[] delegates = _beforeSharedSnippetsViewerAction.GetInvocationList();
      foreach (BeforeSharedSnippetsViewerActionDelegate del in delegates)
      {
        try
        {
          SharedSnippetsViewerEventArgs args = new SharedSnippetsViewerEventArgs();
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

    private AfterSharedSnippetsViewerActionDelegate _afterSharedSnippetsViewerAction;
    public event AfterSharedSnippetsViewerActionDelegate AfterSharedSnippetsViewerAction
    {
      add { _afterSharedSnippetsViewerAction += value; }
      remove { _afterSharedSnippetsViewerAction -= value; }
    }
    private void FireAfterSharedSnippetsViewerAction( SharedSnippetsViewerCommand action )
    {
      if (_afterSharedSnippetsViewerAction == null)
      {
        return;
      }

      Delegate[] delegates = _afterSharedSnippetsViewerAction.GetInvocationList();
      foreach (AfterSharedSnippetsViewerActionDelegate del in delegates)
      {
        try
        {
          SharedSnippetsViewerEventArgs args = new SharedSnippetsViewerEventArgs();
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

    private EventHandler _afterSharedSnippetsViewerClosed;
    public event EventHandler AfterSharedSnippetsViewerClosed
    {
      add { _afterSharedSnippetsViewerClosed += value; }
      remove { _afterSharedSnippetsViewerClosed -= value; }
    }
    public void FireAfterSharedSnippetsViewerClosed( )
    {
      if (_afterSharedSnippetsViewerClosed == null)
      {
        return;
      }

      Delegate[] delegates = _afterSharedSnippetsViewerClosed.GetInvocationList();
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

    public void ShowSharedSnippetsViewer( )
    {
      Program.MainForm.ShowSharedSnippets();
    }

    #endregion

    private void tv_ItemDrag( object sender, ItemDragEventArgs e )
    {
      DoDragDrop(tv.SelNodes, DragDropEffects.Move);
    }

    private void tv_DragOver( object sender, DragEventArgs e )
    {
      string dropText = e.Data.GetData(typeof(string)) as string;
      if (!String.IsNullOrEmpty(dropText))
      {
        e.Effect = DragDropEffects.Copy;
      }
      else
      {

        Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;

        if (sourceNodes == null || sourceNodes.Count == 0)
        {
          e.Effect = DragDropEffects.None;
          return;
        }

        TreeNode firstNode = null;
        foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
        {
          firstNode = nodeWrapper.Node;
          if (firstNode != null)
          {
            break;
          }
        }

        if (firstNode.Tag is SharedSnippetItemData)
        {
          Point pos = new Point();
          pos.X = e.X;
          pos.Y = e.Y;
          pos = tv.PointToClient(pos);

          TreeNode dropNode = tv.GetNodeAt(pos);
          SharedSnippetItemData dropData = SharedSnippetItemDataFactory.GetNodeData(dropNode);
          if (dropData == null || dropData.Type != GenericItemType.Folder)
          {
            e.Effect = DragDropEffects.None;
            return;
          }

          e.Effect = DragDropEffects.Move;
        }
        else
        {
          e.Effect = DragDropEffects.None;
        }
      }
    }

    private void tv_DragDrop( object sender, DragEventArgs e )
    {
      Point pos = new Point();
      pos.X = e.X;
      pos.Y = e.Y;
      pos = tv.PointToClient(pos);

      TreeNode dropNode = tv.GetNodeAt(pos);

      string dropText = e.Data.GetData(typeof(string)) as string;
      if (!String.IsNullOrEmpty(dropText))
      {
        AddSnippet(dropNode, dropText);
        e.Effect = DragDropEffects.Copy;
      }
      else
      {
        Hashtable sourceNodes = e.Data.GetData(typeof(Hashtable)) as Hashtable;

        if (sourceNodes == null || sourceNodes.Count == 0)
        {
          e.Effect = DragDropEffects.None;
          return;
        }



        TreeNode firstNode = null;
        foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
        {
          firstNode = nodeWrapper.Node;
          if (firstNode != null)
          {
            break;
          }
        }
        // This is a node from own MWTreeView
        if (firstNode.Tag is SharedSnippetItemData)
        {
          e.Effect = DragDropEffects.Move;
          foreach (MWTreeNodeWrapper nodeWrapper in sourceNodes.Values)
          {
            ChangeNodeParent(nodeWrapper.Node, dropNode);
          }
        }
      }
    }


    private void tv_BeforeExpand( object sender, TreeViewCancelEventArgs e )
    {
      LoadChildren(e.Node, false);
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


    private void tv_BeforeSelNodeChanged( object sender, EventArgs e )
    {
      SaveDescriptionIfCahanged(true);
    }

    private void tv_AfterSelNodeChanged( object sender, EventArgs e )
    {
      LoadItemDescription(SelectedNode);
      FireAfterSelectedNodesChanged();
    }


    private void tv_DoubleClick( object sender, EventArgs e )
    {
      if (_connParams == null || SelectedNodeData == null || SelectedNodeData.Type != GenericItemType.Item)
      {
        return;
      }
      OnAction_EditSnippet_Execute(sender, e);
    }

    private void buttonSpecAny1_Click(object sender, EventArgs e)
    {
      DescriptionVisible = !DescriptionVisible;      
    }
  }
}
