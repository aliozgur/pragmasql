/********************************************************************
  Class      : ucSharedScripts
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
  public enum SharedScriptsMode
  {
    Edit,
    Open,
    Save
  }

  public partial class ucSharedScripts : UserControl, ISharedScriptsViewerService
  {
    private RichTextEditor _rtbHelpText = null;
    private SharedScriptsService _sharedScriptsFacade = new SharedScriptsService();
    private bool _isInitializing = false;

    private SelectedItemChangedEventHandler _selectedItemChangedEventHandler;

    public event SelectedItemChangedEventHandler SelectedItemChanged
    {
      add
      {
        _selectedItemChangedEventHandler += value;
      }
      remove
      {
        _selectedItemChangedEventHandler -= value;
      }
    }

    public ucSharedScripts( )
    {
      InitializeComponent();

      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedScriptsOptions != null)
      {
        HelpTextVisible = ConfigHelper.Current.SharedScriptsOptions.AlwaysShowHelpText;
        btnShowHelpText.Checked = HelpTextVisible;
      }
      else
      {
        HelpTextVisible = false;
      }

      InitializeHelpTextControl();
      InitActions();
      //InitializeAddInSupport();
      tv.TreeViewNodeSorter = new SharedScriptNodeSorter();
      
    }

    #region Initialization

    internal void InitializeSharedScripts( ConnectionParams connParams )
    {
      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedScriptsOptions != null)
      {
        tv.ShowNodeToolTips = ConfigHelper.Current.SharedScriptsOptions.ShowItemToolTip;
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
            MessageBox.Show("Can not load shared scripts!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Disabled = true;
            ConnParams = null;
            return;
          }
        }

        Disabled = false;
        LoadSharedScripts();
      }
      finally
      {
        _isInitializing = false;
      }
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
      _rtbHelpText.RichTextBox.LinkClicked += new LinkClickedEventHandler(RichTextBox_LinkClicked);
      _rtbHelpText.RichTextBox.Show();
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

      _helpTextChanged = true;
    }

    #endregion

    #region AddIn Support
    public void InitializeAddInSupport( )
    {      
      MenuService.AddItemsToMenu(popUpTv.Items, this, "/Workspace/SharedScriptsViewer/ContextMenu");
      ToolStrip toolbar = ToolbarService.CreateToolStrip(this, "/Workspace/SharedScriptsViewer/Toolbar");
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

    #region Initialize Actions

    private ActionList _actionList = new ActionList();

    private void InitActions( )
    {
     Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.new_case;
      ac.Update += new EventHandler(OnAction_NewRootFolder_Update);
      ac.Execute += new EventHandler(OnAction_NewRootFolder_Execute);
      ac.Text = "New Root Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewRootFolder, ac);
      _actionList.SetAction(popUpTvNewRootFolder, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.NewFolder;
      ac.Update += new EventHandler(OnAction_NewSubFolder_Update);
      ac.Execute += new EventHandler(OnAction_NewSubFolder_Execute);
      ac.Text = "New Sub Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewSubFolder, ac);
      _actionList.SetAction(popUpTvNewSubFolder, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.AddToFavorites;
      ac.Update += new EventHandler(OnAction_NewScript_Update);
      ac.Execute += new EventHandler(OnAction_NewScript_Execute);
      ac.Text = "New Script";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnNewScript, ac);
      _actionList.SetAction(popUpTvNewScript, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      //ac.ShortcutKeys = Keys.F5;
      ac.Image = PragmaSQL.Properties.Resources.Refresh;
      ac.Update += new EventHandler(OnAction_Refresh_Update);
      ac.Execute += new EventHandler(OnAction_Refresh_Execute);
      ac.Text = "Refresh";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnRefresh, ac);
      _actionList.SetAction(popUpTvRefresh, ac);


     ac = new Crad.Windows.Forms.Actions.Action();
      //ac.ShortcutKeys = Keys.Control | Keys.F5;
      ac.Image = PragmaSQL.Properties.Resources.reload24bit;
      ac.Update += new EventHandler(OnAction_LoadInitial_Update);
      ac.Execute += new EventHandler(OnAction_LoadInitial_Execute);
      ac.Text = "Reload";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnReload, ac);
      _actionList.SetAction(popUpTvReload, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      //ac.ShortcutKeys = Keys.Control | Keys.Delete;
      ac.Image = PragmaSQL.Properties.Resources.delete;
      ac.Update += new EventHandler(OnAction_DeleteItem_Update);
      ac.Execute += new EventHandler(OnAction_DeleteItem_Execute);
      ac.Text = "Delete";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnDelete, ac);
      _actionList.SetAction(popUpTvDelete, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.RenameFolder;
      //ac.ShortcutKeys = Keys.F2;
      ac.Update += new EventHandler(OnAction_RenameItem_Update);
      ac.Execute += new EventHandler(OnAction_RenameItem_Execute);
      ac.Text = "Rename";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnRename, ac);
      _actionList.SetAction(popUpTvRename, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.help_2;
      ac.Update += new EventHandler(OnAction_ShowHelpText_Update);
      ac.Execute += new EventHandler(OnAction_ShowHelpText_Execute);
      ac.Text = "Show HelpText";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnShowHelpText, ac);
      _actionList.SetAction(popUpTvShowHelpText, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.SaveAsWebPage;
      //ac.ShortcutKeys = Keys.Control | Keys.S;
      ac.Update += new EventHandler(OnAction_SaveHelpText_Update);
      ac.Execute += new EventHandler(OnAction_SaveHelpText_Execute);
      ac.Text = "Save HelpText";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(btnSaveHelpText, ac);
      _actionList.SetAction(popUpTvSaveHelpText, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.edit;
      ac.Update += new EventHandler(OnAction_EditScript_Update);
      ac.Execute += new EventHandler(OnAction_EditScript_Execute);
      ac.Text = "Edit";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuPopUpTvOpen, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_DiffScript_Update);
      ac.Execute += new EventHandler(OnAction_SendScriptAsSourceToDiff_Execute);
      ac.Text = "As Source";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuPopUpTvSendToDiffAsSource, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_DiffScript_Update);
      ac.Execute += new EventHandler(OnAction_SendScriptAsDestToDiff_Execute);
      ac.Text = "As Dest";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuPopUpTvSendToDiffAsDest, ac);
    }



    private void OnAction_NewRootFolder_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.NewRootFolder);
    }

    private void OnAction_NewSubFolder_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.NewSubFolder);
    }

    private void OnAction_NewSubFolder_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.NewSubFolder);
      AddFolder(SelectedNode);
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.NewSubFolder);
    }

    private void OnAction_NewRootFolder_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.NewRootFolder);
      AddFolder(null);
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.NewRootFolder);
    }

    private void OnAction_NewScript_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.NewScript);
    }

    private void OnAction_NewScript_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.NewScript);
      AddScript(SelectedNode);
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.NewScript);
    }

    private void OnAction_Refresh_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.RefreshNode);
    }

    private void OnAction_Refresh_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.RefreshNode);
      LoadChildren(SelectedNode, true);
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.RefreshNode);
    }

    private void OnAction_LoadInitial_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.Reload);
    }

    private void OnAction_LoadInitial_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.Reload);
      LoadInitial();
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.Reload);
    }



    private void OnAction_DeleteItem_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.DeleteSelectedItems);
    }

    private void OnAction_DeleteItem_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.DeleteSelectedItems);
      DeleteSelectedItems();
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.DeleteSelectedItems);
    }

    private void OnAction_RenameItem_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.RenameItem);
    }

    private void OnAction_RenameItem_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.RenameItem);
      RenameItem();
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.RenameItem);
    }

    private void OnAction_ShowHelpText_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.ShowHelpText);
      _rtbHelpText.Enabled = ac.Enabled;
      ac.Checked = HelpTextVisible;
    }

    private void OnAction_ShowHelpText_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(!HelpTextVisible ? SharedScriptsViewerCommand.ShowHelpText : SharedScriptsViewerCommand.HideHelpText);
      HelpTextVisible = !HelpTextVisible;
      FireAfterSharedScriptsViewerAction(HelpTextVisible ? SharedScriptsViewerCommand.ShowHelpText : SharedScriptsViewerCommand.HideHelpText);
    }

    private void OnAction_SaveHelpText_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.SaveHelpText);
    }

    private void OnAction_SaveHelpText_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.SaveHelpText);
      SaveHelpTextIfCahanged(true);
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.SaveHelpText);
    }

    private void OnAction_EditScript_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(SharedScriptsViewerCommand.EditScript);
    }

    private void OnAction_EditScript_Execute( object sender, EventArgs e )
    {
      FireBeforeSharedScriptsViewerAction(SharedScriptsViewerCommand.EditScript);
      EditScript();
      FireAfterSharedScriptsViewerAction(SharedScriptsViewerCommand.EditScript);
    }


    private void OnAction_DiffScript_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      SharedScriptsItemData data = SelectedNodeData;

      ac.Enabled = (_connParams != null && (data != null && data.Type == GenericItemType.Item));
    }

    private void OnAction_SendScriptAsSourceToDiff_Execute( object sender, EventArgs e )
    {
      SendSelectedScriptToDiff(true);
    }

    private void OnAction_SendScriptAsDestToDiff_Execute( object sender, EventArgs e )
    {
      SendSelectedScriptToDiff(false);
    }

    #endregion

    #region Properties
    private SharedScriptsMode _mode = SharedScriptsMode.Edit;
    public SharedScriptsMode Mode
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
        _sharedScriptsFacade.ConnParams = value;
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

    public Hashtable SelectedNodesRaw
    {
      get
      {
        return tv.SelNodes;
      }
    }

    public SharedScriptsItemData SelectedNodeData
    {
      get
      {
        return SharedScriptsItemDataFactory.GetNodeData(SelectedNode);
      }
    }

    private bool _helpTextVisible = false;
    [Browsable(false)]

    public bool HelpTextVisible
    {
      get
      {
        return _helpTextVisible;
      }
      set
      {
        panHelpText.Visible = value;
        splitterHelpText.Visible = value;
        splitterHelpText.BringToFront();
        tv.BringToFront();
        btnShowHelpText.Checked = value;
        _helpTextVisible = value;
      }
    }

    private bool _helpTextChanged = false;
    public bool HelpTextChanged
    {
      get { return _helpTextChanged; }
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
          _rtbHelpText.RichTextBox.Invalidate();
          _rtbHelpText.Refresh();
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
          HelpTextVisible = !value;
        }

        toolStrip1.Enabled = !value;
        panHelpText.Enabled = !value;
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

    private TreeNode AddNode( TreeNode parentNode, SharedScriptsItemData data )
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
      SharedScriptsItemData data = SelectedNodeData;

      if (data == null)
      {
        lblItemName.Text = String.Empty;
        lblCreatedBy.Text = String.Empty;
        lblUpdatedBy.Text = String.Empty;
        kryptonHeader1.Text = "HelpText {?}";
        _helpTextChanged = false;
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
        kryptonHeader1.Text = "HelpText {" + data.Name + "}";
      }
    }

    public void ApplyMode( )
    {
      switch (_mode)
      {
        case SharedScriptsMode.Edit:
          tv.MultiSelect = TreeViewMultiSelect.MultiSameBranchAndLevel;
          statusStrip1.Visible = true;
          break;
        case SharedScriptsMode.Save:
          tv.MultiSelect = TreeViewMultiSelect.NoMulti;
          statusStrip1.Visible = false;
          break;
        case SharedScriptsMode.Open:
          tv.MultiSelect = TreeViewMultiSelect.Multi;
          statusStrip1.Visible = false;
          break;
        default:
          break;
      }
    }
    #endregion

    #region Script operations

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
        LoadSharedScripts();
      }
    }

    public void LoadSharedScripts( )
    {
      try
      {
        _isInitializing = true;
        tv.BeginUpdate();
        tv.Nodes.Clear();
        TreeNode node = null;
        IList<SharedScriptsItemData> children = _sharedScriptsFacade.GetChildren(null);
        foreach (SharedScriptsItemData data in children)
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
          LoadItemHelpText(tv.Nodes[0]);
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
      SharedScriptsItemData data = SharedScriptsItemDataFactory.GetNodeData(node);
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
        IList<SharedScriptsItemData> children = _sharedScriptsFacade.GetChildren(data.ID);
        foreach (SharedScriptsItemData childData in children)
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

    private void AddItem( TreeNode parentNode, int? type, string name, string script )
    {
      int? parentID = null;

      SharedScriptsItemData parentData = SharedScriptsItemDataFactory.GetNodeData(parentNode);

      if (parentData != null)
      {
        parentID = parentData.ID;
      }


      try
      {
        tv.BeginUpdate();
        SharedScriptsItemData newItemData = SharedScriptsItemDataFactory.Create(name, type, null, parentID, _connParams.CurrentUsername);
        newItemData.Script = script;
        newItemData.ParentID = parentID;
        newItemData.CreatedBy = _connParams.CurrentUsername;
        try
        {
          _sharedScriptsFacade.AddItem(newItemData);
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

    private void AddScript( TreeNode parentNode )
    {
      AddScript(parentNode, String.Empty);
    }

    private void AddScript( TreeNode parentNode, string script )
    {
      string name = String.Empty;

      DialogResult dlgRes = InputDialog.ShowDialog("New Script", "Script Name", ref name);
      if (dlgRes != DialogResult.OK || String.IsNullOrEmpty(name))
      {
        return;
      }
      AddItem(parentNode, GenericItemType.Item, name, script);
    }

    private void DeleteItem( TreeNode node )
    {
      SharedScriptsItemData data = SharedScriptsItemDataFactory.GetNodeData(node);

      if (node == null || data == null)
      {
        return;
      }

      try
      {
        tv.BeginUpdate();
        _sharedScriptsFacade.DeleteItem(data.ID);

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
        LoadItemHelpText(SelectedNode);
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
        LoadItemHelpText(SelectedNode);
        UpdateSelectedItemInfo();
        tv.Sort();
      }
    }

    public void RenameItem( )
    {
      string name = String.Empty;
      string prevName = String.Empty;

      SharedScriptsItemData selNodeData = SelectedNodeData;
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
          _sharedScriptsFacade.UpdateItem(selNodeData);
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

    public void LoadItemHelpText( TreeNode node )
    {
      SharedScriptsItemData nodeData = SharedScriptsItemDataFactory.GetNodeData(node);
      if (nodeData == null)
      {
        kryptonHeader1.Text = "HelpText {?}";
        CurrentHelpText = String.Empty;
        _helpTextChanged = false;
        return;
      }
      _isInitializing = true;
      CurrentHelpText = nodeData.HelpText;
      UpdateSelectedItemInfo();
      _isInitializing = false;
    }

    public void SaveHelpTextIfCahanged( bool confirmSave )
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
      SharedScriptsItemData selData = SharedScriptsItemDataFactory.GetNodeData(SelectedNode);
      if (selData == null)
      {
        return;
      }


      bool confirmHelpTextSave = true;
      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedScriptsOptions != null)
      {
        confirmHelpTextSave = ConfigHelper.Current.SharedScriptsOptions.ConfirmHelpTextSave;
      }

      if (confirmSave && confirmHelpTextSave)
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
        _sharedScriptsFacade.UpdateItem(selData);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      UpdateSelectedItemInfo();
      _helpTextChanged = false;
    }

    public void ChangeNodeParent( TreeNode source, TreeNode dropNode )
    {
      if (source == null)
      {
        return;
      }

      SharedScriptsItemData sourceData = SharedScriptsItemDataFactory.GetNodeData(source);
      if (sourceData == null)
      {
        return;
      }

      try
      {
        int? parentID = null;
        TreeNode parentNode = null;

        SharedScriptsItemData dropData = SharedScriptsItemDataFactory.GetNodeData(dropNode);
        if (dropNode != null && dropData != null)
        {
          parentID = dropData.ID;
          parentNode = dropNode;
        }


        tv.BeginUpdate();

        try
        {
          sourceData.ParentID = parentID;
          _sharedScriptsFacade.UpdateItem(sourceData);

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
			if (!ConfigHelper.Current.SharedScriptsEnabled())
      {
        if (showWarning)
        {
          MessageBox.Show("Shared scripts option is not set."
            + "\nPlease enable \"Tools -> Options -> PragmaSQL System -> Use shared scripts\" option."
            , "Information"
            , MessageBoxButtons.OK
            , MessageBoxIcon.Information);
        }
        return false;
      }

      InitializeSharedScripts(ConfigHelper.Current.PragmaSqlDbConn);
      return true;
    }

    private void SendSelectedScriptToDiff( bool isSource )
    {
      if (tv.SelNodes.Count == 0)
      {
        return;
      }

      SharedScriptsItemData itemData = null;
      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        itemData = SharedScriptsItemDataFactory.GetNodeData(nodeWrapper.Node);
        if (itemData == null)
        {
          break;
        }

        string script = itemData.Script;


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

    private void EditScript( )
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

      bool alwaysUseTextEditor = false;
      if (ConfigHelper.Current != null && ConfigHelper.Current.SharedScriptsOptions != null)
      {
        alwaysUseTextEditor = ConfigHelper.Current.SharedScriptsOptions.AlwaysUseOfflineScriptEditor;
      }

      SharedScriptsItemData itemData = null;
      foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
      {
        itemData = SharedScriptsItemDataFactory.GetNodeData(nodeWrapper.Node);
        if (itemData == null)
        {
          continue;
        }

        if (cp != null && !alwaysUseTextEditor)
        {
          frmScriptEditor frm = ScriptEditorFactory.OpenSharedScript(itemData, cp);
          ScriptEditorFactory.ShowScriptEditor(frm);
        }
        else
        {
          frmTextEditor frm = TextEditorFactory.OpenSharedScript(itemData);
          TextEditorFactory.ShowTextEditor(frm);
        }
      }
    }
    #endregion

    #region ISharedScriptsViewerService
    public SharedScriptsItemData SelectedItem
    {
      get { return SelectedNodeData; }
    }

    public IList<SharedScriptsItemData> SelectedItems
    {
      get
      {
        IList<SharedScriptsItemData> result = new List<SharedScriptsItemData>();
        foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
        {
          SharedScriptsItemData data = SharedScriptsItemDataFactory.GetNodeData(nodeWrapper.Node);
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

    public bool CanPerformCommand( SharedScriptsViewerCommand action )
    {
      SharedScriptsItemData nodeData = SelectedNodeData; ;
      bool result = false;
      switch (action)
      {
        case SharedScriptsViewerCommand.None:
          result = false;
          break;
        case SharedScriptsViewerCommand.Reload:
          result = true;// (nodeData == null);
          break;
        case SharedScriptsViewerCommand.RefreshNode:
          result = (_connParams != null && (nodeData != null && nodeData.Type == GenericItemType.Folder));
          break;
        case SharedScriptsViewerCommand.NewRootFolder:
          result = (_connParams != null && (nodeData == null || (nodeData.Type == GenericItemType.Folder)));
          break;
        case SharedScriptsViewerCommand.NewSubFolder:
          result = (_connParams != null && (nodeData == null || (nodeData.Type == GenericItemType.Folder)));
          break;
        case SharedScriptsViewerCommand.NewScript:
          result = (_connParams != null && (nodeData == null || (nodeData.Type == GenericItemType.Folder)));
          break;
        case SharedScriptsViewerCommand.RenameItem:
          result = (_connParams != null && nodeData != null);
          break;
        case SharedScriptsViewerCommand.DeleteSelectedItems:
          result = (_connParams != null && nodeData != null);
          break;
        case SharedScriptsViewerCommand.SaveHelpText:
          result = (_connParams != null && (nodeData != null && _helpTextChanged));
          break;
        case SharedScriptsViewerCommand.ShowHelpText:
          result = (_connParams != null && nodeData != null);
          break;
        case SharedScriptsViewerCommand.HideHelpText:
          result = (_connParams != null && nodeData != null);
          break;
        case SharedScriptsViewerCommand.EditScript:
          result = (_connParams != null && (nodeData != null && nodeData.Type == GenericItemType.Item));
          break;
        default:
          result = false;
          break;
      }
      return result;
    }

    public void ExecuteCommand( SharedScriptsViewerCommand action )
    {
      if (!CanPerformCommand(action))
      {
        HostServicesSingleton.HostServices.MsgService.ErrorMsg(String.Format("Selecte node(s) state is invalid! Can not perform required action: \"{0}\".", action), (MethodInfo)MethodInfo.GetCurrentMethod());
        HostServicesSingleton.HostServices.MsgService.ShowMessages();
      }

      switch (action)
      {
        case SharedScriptsViewerCommand.None:
          break;
        case SharedScriptsViewerCommand.Reload:
          LoadInitial();
          break;
        case SharedScriptsViewerCommand.RefreshNode:
          LoadChildren(SelectedNode, true);
          break;
        case SharedScriptsViewerCommand.NewRootFolder:
          AddFolder(null);
          break;
        case SharedScriptsViewerCommand.NewSubFolder:
          AddFolder(SelectedNode);
          break;
        case SharedScriptsViewerCommand.NewScript:
          AddScript(SelectedNode);
          break;
        case SharedScriptsViewerCommand.RenameItem:
          RenameItem();
          break;
        case SharedScriptsViewerCommand.DeleteSelectedItems:
          DeleteSelectedItems();
          break;
        case SharedScriptsViewerCommand.SaveHelpText:
          SaveHelpTextIfCahanged(true);
          break;
        case SharedScriptsViewerCommand.ShowHelpText:
          HelpTextVisible = true;
          break;
        case SharedScriptsViewerCommand.HideHelpText:
          HelpTextVisible = false;
          break;
        case SharedScriptsViewerCommand.EditScript:
          EditScript();
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

    private BeforeSharedScriptsViewerActionDelegate _beforeSharedScriptsViewerAction;
    public event BeforeSharedScriptsViewerActionDelegate BeforeSharedScriptsViewerAction
    {
      add { _beforeSharedScriptsViewerAction += value; }
      remove { _beforeSharedScriptsViewerAction -= value; }
    }
    private void FireBeforeSharedScriptsViewerAction( SharedScriptsViewerCommand action )
    {
      if (_beforeSharedScriptsViewerAction == null)
      {
        return;
      }

      Delegate[] delegates = _beforeSharedScriptsViewerAction.GetInvocationList();
      foreach (BeforeSharedScriptsViewerActionDelegate del in delegates)
      {
        try
        {
          SharedScriptsViewerEventArgs args = new SharedScriptsViewerEventArgs();
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

    private AfterSharedScriptsViewerActionDelegate _afterSharedScriptsViewerAction;
    public event AfterSharedScriptsViewerActionDelegate AfterSharedScriptsViewerAction
    {
      add { _afterSharedScriptsViewerAction += value; }
      remove { _afterSharedScriptsViewerAction -= value; }
    }
    private void FireAfterSharedScriptsViewerAction( SharedScriptsViewerCommand action )
    {
      if (_afterSharedScriptsViewerAction == null)
      {
        return;
      }

      Delegate[] delegates = _afterSharedScriptsViewerAction.GetInvocationList();
      foreach (AfterSharedScriptsViewerActionDelegate del in delegates)
      {
        try
        {
          SharedScriptsViewerEventArgs args = new SharedScriptsViewerEventArgs();
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

    private EventHandler _afterSharedScriptsViewerClosed;
    public event EventHandler AfterSharedScriptsViewerClosed
    {
      add { _afterSharedScriptsViewerClosed += value; }
      remove { _afterSharedScriptsViewerClosed -= value; }
    }
    public void FireAfterSharedScriptsViewerClosed( )
    {
      if (_afterSharedScriptsViewerClosed == null)
      {
        return;
      }

      Delegate[] delegates = _afterSharedScriptsViewerClosed.GetInvocationList();
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

    public void ShowSharedScriptsViewer( )
    {
      Program.MainForm.ShowSharedScripts();
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

        if (firstNode.Tag is SharedScriptsItemData)
        {
          Point pos = new Point();
          pos.X = e.X;
          pos.Y = e.Y;
          pos = tv.PointToClient(pos);

          TreeNode dropNode = tv.GetNodeAt(pos);
          SharedScriptsItemData dropData = SharedScriptsItemDataFactory.GetNodeData(dropNode);
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
        AddScript(dropNode, dropText);
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
        if (firstNode.Tag is SharedScriptsItemData)
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
      SaveHelpTextIfCahanged(true);
    }

    private void tv_AfterSelNodeChanged( object sender, EventArgs e )
    {
      LoadItemHelpText(SelectedNode);
      if (_selectedItemChangedEventHandler != null)
      {
        _selectedItemChangedEventHandler(this, SelectedNodeData);
      }

      FireAfterSelectedNodesChanged();
    }

    private void tv_DoubleClick( object sender, EventArgs e )
    {
      if (_connParams == null || SelectedNodeData == null || SelectedNodeData.Type != GenericItemType.Item || Mode != SharedScriptsMode.Edit)
      {
        return;
      }
      OnAction_EditScript_Execute(sender, e);
    }

    private void buttonSpecAny1_Click(object sender, EventArgs e)
    {
      HelpTextVisible = !HelpTextVisible;

    }
  }

  public delegate void SelectedItemChangedEventHandler( object sender, SharedScriptsItemData itemData );
}
