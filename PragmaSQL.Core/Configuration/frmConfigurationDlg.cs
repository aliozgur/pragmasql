/********************************************************************
  Class      : frmConfiguration
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
  public enum OptionEditorType
  {
    None,
    General,
    TextEditor,
    TextEditorShortcuts,
    CodeCompletionLists,
    SelectCaseHighlighting,
    PragmaSQLSystem,
    SharedScripts,
    SharedSnippets,
    SystemFileAssociations,
    Help,
    WebSearch,
    WorkspaceOptions,
    ObjectExplorerOptions
  }

  public enum ConfigAction
  {
    None,
    Cancel,
    Save,
    Apply
  }


  public partial class frmConfigurationDlg : KryptonForm
	{
		private static frmConfigurationDlg _instance = null;
		public static frmConfigurationDlg Instance
		{
			get { return _instance; }
		}

		private static ConfigSvc ConficSvc
		{
			get
			{
				return HostServicesSingleton.HostServices.ConfigSvc as ConfigSvc;
			}
		}

		#region Fields And Properties

		private ucTextEditorOptions _opTextEditor = null;
    private ucScriptEditorShortCuts _opScriptEditorShortcuts = null;
    private ucGeneralOptions _opGeneralSettings = null;
    private ucPragmaSqlSystem _opPragmaSqlConn = null;
    private ucSharedSnippetsOptions _opSharedSnippets = null;
    private ucObjectExplorerOptions _opObjectExplorer = null;

    private ucSharedScriptsOptions _opSharedScripts = null;
    private ucCodeCompletionLists _opCodeCompletionLists = null;

    private ucSystemFileAssociation _opSystemFileAssociation = null;
    private ucHelpSettings _opHelpSettings = null;
    private ucWebSearchOptions _opWebSearchOptions = null;

    private IConfigContentEditor _currentItem = null;
    private IList<IConfigContentEditor> _configItems = new List<IConfigContentEditor>();

    private ConfigAction _action = ConfigAction.Cancel;
    private ConfigurationContent _configContent = null;


    private IList<string> _changedOptions = new List<string>();
    public IList<string> ChangedOptions
    {
      get { return _changedOptions; }
    }

    private ConfigFinalSelectionEventHandler _onFinalSelection;
    public event ConfigFinalSelectionEventHandler FinalSelection
    {
      add
      {
        _onFinalSelection += value;
      }
      remove
      {
        _onFinalSelection -= value;
      }
    }
		private TreeNode _moduleOptionsRoot = null;
		public TreeNode ModuleOptionsRoot
		{
			get { return _moduleOptionsRoot; }
			set { _moduleOptionsRoot = value; }
		}
		
		#endregion //Fields And Properties

		#region CTOR

		public frmConfigurationDlg( )
    {
      InitializeComponent();
      CreateControl_TextEditorOptions();
      CreateControl_ScriptEditorShortcuts();
      CreateControl_GeneralSettings();
      CreateControl_SharedSnippetsOptions();
      CreateControl_SystemFileAssociations();
      CreateControl_SharedScriptsOptions();
      CreateControl_HelpSettings();
      CreateControl_WebSearchOptions();
      CreateControl_ObjectExplorerOptions();
#if PERSONAL_EDITION == false
      CreateControl_PragmaSqlConnection();
      CreateControl_CodeCompletionList();
#endif

      HostServicesSingleton.HostServices.SetMainFormAsOwner(this);
		}

		#endregion //CTOR

		#region Create configuration controls

		private void CreateControl_TextEditorOptions( )
    {
      if (_opTextEditor != null)
      {
        _configItems.Remove(_opTextEditor);
        _opTextEditor.Dispose();
        _opTextEditor = null;
      }

      _opTextEditor = new ucTextEditorOptions();
      _opTextEditor.Hide();
      _opTextEditor.Parent = panContent;
      _opTextEditor.Dock = DockStyle.Fill;

      _configItems.Add(_opTextEditor);
    }

    private void CreateControl_ScriptEditorShortcuts( )
    {
      if (_opScriptEditorShortcuts != null)
      {
        _configItems.Remove(_opScriptEditorShortcuts);
        _opScriptEditorShortcuts.Dispose();
        _opScriptEditorShortcuts = null;
      }

      _opScriptEditorShortcuts = new ucScriptEditorShortCuts();
      _opScriptEditorShortcuts.Hide();
      _opScriptEditorShortcuts.Parent = panContent;
      _opScriptEditorShortcuts.Dock = DockStyle.Fill;

      _configItems.Add(_opScriptEditorShortcuts);
    }

    private void CreateControl_GeneralSettings( )
    {
      if (_opGeneralSettings != null)
      {
        _configItems.Remove(_opGeneralSettings);
        _opGeneralSettings.Dispose();
        _opGeneralSettings = null;
      }

      _opGeneralSettings = new ucGeneralOptions();
      _opGeneralSettings.Hide();
      _opGeneralSettings.Parent = panContent;
      _opGeneralSettings.Dock = DockStyle.Fill;

      _configItems.Add(_opGeneralSettings);
    }
    

    private void CreateControl_PragmaSqlConnection()
    {
      if (_opPragmaSqlConn != null)
      {
        _configItems.Remove(_opPragmaSqlConn);
        _opPragmaSqlConn.Dispose();
        _opPragmaSqlConn = null;
      }

      _opPragmaSqlConn = new ucPragmaSqlSystem();
      _opPragmaSqlConn.Hide();
      _opPragmaSqlConn.Parent = panContent;
      _opPragmaSqlConn.Dock = DockStyle.Fill;

      _configItems.Add(_opPragmaSqlConn);
    }

    private void CreateControl_SharedSnippetsOptions( )
    {
      if (_opSharedSnippets != null)
      {
        _configItems.Remove(_opSharedSnippets);
        _opSharedSnippets.Dispose();
        _opSharedSnippets = null;
      }

      _opSharedSnippets = new ucSharedSnippetsOptions();
      _opSharedSnippets.Hide();
      _opSharedSnippets.Parent = panContent;
      _opSharedSnippets.Dock = DockStyle.Fill;

      _configItems.Add(_opSharedSnippets);
    }

    private void CreateControl_SystemFileAssociations( )
    {
      if (_opSystemFileAssociation != null)
      {
        _configItems.Remove(_opSystemFileAssociation);
        _opSystemFileAssociation.Dispose();
        _opSystemFileAssociation = null;
      }

      _opSystemFileAssociation = new ucSystemFileAssociation();
      _opSystemFileAssociation.Hide();
      _opSystemFileAssociation.Parent = panContent;
      _opSystemFileAssociation.Dock = DockStyle.Fill;

      _configItems.Add(_opSystemFileAssociation);
    }

    private void CreateControl_SharedScriptsOptions()
    {
      if (_opSharedScripts != null)
      {
        _configItems.Remove(_opSharedScripts);
        _opSharedScripts.Dispose();
        _opSharedScripts = null;
      }

      _opSharedScripts = new ucSharedScriptsOptions();
      _opSharedScripts.Hide();
      _opSharedScripts.Parent = panContent;
      _opSharedScripts.Dock = DockStyle.Fill;

      _configItems.Add(_opSharedScripts);
    }

    private void CreateControl_CodeCompletionList()
    {
      if (_opCodeCompletionLists != null)
      {
        _configItems.Remove(_opCodeCompletionLists);
        _opCodeCompletionLists.Dispose();
        _opCodeCompletionLists = null;
      }

      _opCodeCompletionLists = new ucCodeCompletionLists();
      _opCodeCompletionLists.Hide();
      _opCodeCompletionLists.Parent = panContent;
      _opCodeCompletionLists.Dock = DockStyle.Fill;

      _configItems.Add(_opCodeCompletionLists);
    }
    
    private void CreateControl_HelpSettings()
    {
      if (_opHelpSettings != null)
      {
        _configItems.Remove(_opHelpSettings);
        _opHelpSettings.Dispose();
        _opHelpSettings = null;
      }

      _opHelpSettings = new ucHelpSettings();
      _opHelpSettings.Hide();
      _opHelpSettings.Parent = panContent;
      _opHelpSettings.Dock = DockStyle.Fill;

      _configItems.Add(_opHelpSettings);
    }

    private void CreateControl_WebSearchOptions( )
    {
      if (_opWebSearchOptions != null)
      {
        _configItems.Remove(_opWebSearchOptions);
        _opWebSearchOptions.Dispose();
        _opWebSearchOptions = null;
      }

      _opWebSearchOptions = new ucWebSearchOptions();
      _opWebSearchOptions.Hide();
      _opWebSearchOptions.Parent = panContent;
      _opWebSearchOptions.Dock = DockStyle.Fill;

      _configItems.Add(_opWebSearchOptions);
    }

    private void CreateControl_ObjectExplorerOptions()
    {
      if (_opObjectExplorer != null)
      {
        _configItems.Remove(_opObjectExplorer);
        _opObjectExplorer.Dispose();
        _opObjectExplorer = null;
      }

      _opObjectExplorer = new ucObjectExplorerOptions();
      _opObjectExplorer.Hide();
      _opObjectExplorer.Parent = panContent;
      _opObjectExplorer.Dock = DockStyle.Fill;

      _configItems.Add(_opObjectExplorer);
    }
    #endregion

    #region Initialization


    public void InitializeConfiguration(ConfigurationContent configContent)
    {
      if(configContent == null)
      {
        throw new NullParameterException("Configuration content is null!");
      }
      _configContent = configContent;
      BuildConfigurationItems();
    }

		private TreeNode AddNode(TreeNode parent, string key, string text)
		{
			TreeNodeCollection nodes = (parent != null ? parent.Nodes : tv.Nodes);
			TreeNode result = nodes.Add(key, text);

			if (parent != null)
			{
				parent.ImageIndex = 1;
				parent.SelectedImageIndex = 1;
			}

      result.ImageIndex = 2;
      result.SelectedImageIndex = 0;
      
      return result;		
		}

    private TreeNode AddNode( TreeNode parent, OptionEditorType type, string text )
    {
			return AddNode(parent, type.ToString(), text);
    }

    private TreeNode AddNode( OptionEditorType type, string text )
    {
      return AddNode(null, type, text);
    }

    private TreeNode AddNode( string text )
    {
      return AddNode(null, text, text);
    }

    private TreeNode AddNode(TreeNode parent, string text )
    {
      return AddNode(parent, text, text);
    }

    private void BuildConfigurationItems( )
    {
      TreeNode node = null;
      tv.Nodes.Clear();
      TreeNode parentNode = AddNode(OptionEditorType.None,"PragmaSQL Options");

      TreeNode generalSettingsNode = AddNode(parentNode,"Application");
      generalSettingsNode.Tag = _opGeneralSettings;

      node = AddNode(parentNode, OptionEditorType.ObjectExplorerOptions, "Object Explorer");
      node.Tag = _opObjectExplorer;

      Build_ScriptEditorOptions(parentNode);

      //node = AddNode(parentNode,OptionEditorType.SelectCaseHighlighting, "Select/Case Highlighting");
      //node.Tag = _opTextEditor;

#if ( PERSONAL_EDITION == false)
      node = AddNode(parentNode,"PragmaSQL System");
      node.Tag = _opPragmaSqlConn;
#endif

      node = AddNode(parentNode, OptionEditorType.SharedSnippets, "Shared Snippets");
      node.Tag = _opSharedSnippets;

      node = AddNode(parentNode, OptionEditorType.SharedScripts, "Shared Scripts");
      node.Tag = _opSharedScripts;

      node = AddNode(parentNode, OptionEditorType.SystemFileAssociations, "System File Associations");
      node.Tag = _opSystemFileAssociation;

      node = AddNode(parentNode,OptionEditorType.Help, "T-SQL Help");
      node.Tag = _opHelpSettings;

      node = AddNode( parentNode, OptionEditorType.WebSearch, "Web Search");
      node.Tag = _opWebSearchOptions;

      parentNode.Expand();
      tv.SelectedNode = generalSettingsNode;

			_moduleOptionsRoot = tv.Nodes.Add("Modules");
			_moduleOptionsRoot.ImageIndex = 1;
			_moduleOptionsRoot.SelectedImageIndex = 1;

    }

    private void Build_ScriptEditorOptions( TreeNode parentNode )
    {
      // Script editor
      TreeNode node = null;
      TreeNode tmpNode = null;

      node = AddNode(parentNode,"Script Editor");

			tmpNode = AddNode(node, OptionEditorType.TextEditor, "Style and behavior");
      tmpNode.Tag = _opTextEditor;

      tmpNode = AddNode(node,OptionEditorType.TextEditorShortcuts,"Shortcut Keys");
      tmpNode.Tag = _opScriptEditorShortcuts;


#if PERSONAL_EDITION == false
      tmpNode =  AddNode(node,OptionEditorType.CodeCompletionLists,"Code Completion Lists");
      tmpNode.Tag = _opCodeCompletionLists;
#endif
		}


    #endregion

    #region Utility Methods
    private void RaiseFinalSelectionEvent( ConfigAction action )
    {
      if (_onFinalSelection != null)
      {
        ConfigEventArgs args = new ConfigEventArgs();
        args.action = action;
        args.content = _configContent;
        args.ChangedOptions = _changedOptions;
        _onFinalSelection(this, args);
				
				frmConfigurationDlg.ConficSvc.FireFinalSelection(action, _changedOptions);
			}
    }

    private void ShowSelectedContent( TreeNode node )
    {
      if (node == null)
      {
        return;
      }

      if ((node.Tag == null) || !(node.Tag is IConfigContentEditor))
      {
        return;
      }


      IConfigContentEditor configItem = (node.Tag as IConfigContentEditor);

      if (_currentItem == configItem)
      {
        return;
      }

      if (_currentItem != null)
      {
        _currentItem.HideContent();
      }

      _currentItem = configItem;
      if (!_currentItem.ContentLoaded)
      {
        _currentItem.LoadContent();
      }

      if(_currentItem == _opCodeCompletionLists)
      {
        this.AcceptButton = null;
      }
      else
      {
        this.AcceptButton = btnSave;
      }

      lblIntro.SendToBack();
      kryptonHeader1.Text = node.Text;
      configItem.ShowContent();
      kryptonHeader1.SendToBack();
    }

    private bool SaveAllChangedContentItems()
    {
      _changedOptions.Clear();
      bool result = false;
      foreach( IConfigContentEditor item in _configItems )
      {
        if(item.Modified)
        {
          _changedOptions.Add(item.ItemClassName);
          item.SaveContent();
          result = true;
        }
      }

      return result;
    }

    private void SaveChanges()
    {
      if(SaveAllChangedContentItems())
      {
        ConfigHelper.SaveAsDefault(_configContent);
      }
    }

		public void ShowOptionsEditor(string editorName)
		{
			if (String.IsNullOrEmpty(editorName))
				return;

			TreeNode[] nodes = tv.Nodes.Find(editorName, true);
			if (nodes.Length > 0)
			{
				tv.SelectedNode = nodes[0];
			}
		}
		
		public void ShowOptionsEditor(OptionEditorType type)
		{
			ShowOptionsEditor(type == OptionEditorType.None ? String.Empty : type.ToString());
		}

    #endregion

    #region Static Methods
		
		public static void ShowConfigurationDlg(ConfigurationContent configContent, string initialEditor, ConfigFinalSelectionEventHandler onFinalSelectionHandler)
		{
			if (configContent == null)
			{
				throw new NullParameterException("Configuration content is null!");
			}

			if (_instance == null)
				_instance = new frmConfigurationDlg();

			_instance.FinalSelection += onFinalSelectionHandler;
			_instance.InitializeConfiguration(configContent);
			ConficSvc.FireDialogOpenedEvent();
			_instance.Show();
			_instance.ShowOptionsEditor(initialEditor);
		}

		public static void ShowConfigurationDlg(ConfigurationContent configContent, OptionEditorType editorType , ConfigFinalSelectionEventHandler onFinalSelectionHandler)
		{
			ShowConfigurationDlg(configContent, editorType == OptionEditorType.None ? String.Empty : editorType.ToString() , onFinalSelectionHandler);
		}

		public static void ShowConfigurationDlg(ConfigurationContent configContent,ConfigFinalSelectionEventHandler onFinalSelectionHandler )
    {
			ShowConfigurationDlg(configContent, String.Empty, onFinalSelectionHandler);
    }

    #endregion

		#region IConfigSvc Support Methods

		public TreeNode AddFolder(string text)
		{
			TreeNode node = _moduleOptionsRoot.Nodes.Add(text);
			node.Text = text;
			node.ImageIndex = 4;
			node.SelectedImageIndex = 4;
			return node;
		}

		public TreeNode AddItem(TreeNode parent, string text, IConfigContentEditor editor)
		{
			if (String.IsNullOrEmpty(text))
				throw new NullParameterException("Can not add item to configuration dialog.Text is null or empty.");

			if (editor == null )
				return null;

			UserControl uc = editor as UserControl;
			if (uc == null)
				return null;

			TreeNode node = null;
			if (parent == null)
				node = _moduleOptionsRoot.Nodes.Add(text,text);
			else
				node = parent.Nodes.Add(text,text);

			node.Text = text;
			node.ImageIndex = 2;
			node.SelectedImageIndex = 0;
			node.Tag = editor;
			_configItems.Add(editor);

			uc.Hide();
			uc.Parent = panContent;
			uc.Dock = DockStyle.Fill;

			return node;
		}

		#endregion //IConfigSvc Support Methods


    private void btnApply_Click( object sender, EventArgs e )
    {
      _action = ConfigAction.Cancel;
      SaveChanges();
      RaiseFinalSelectionEvent(ConfigAction.Apply);
    }

    private void frmConfigurationDlg_FormClosed( object sender, FormClosedEventArgs e )
    {
      RaiseFinalSelectionEvent(_action);
			frmConfigurationDlg.ConficSvc.FireDialogClosedEvent();
			_instance = null;
		}

    private void btnCancel_Click( object sender, EventArgs e )
    {
      _action = ConfigAction.Cancel;
      Close();
    }

    private void btnSave_Click( object sender, EventArgs e )
    {
      _action = ConfigAction.Save;
      SaveChanges();
      Close();
    }

    private void tv_AfterSelect( object sender, TreeViewEventArgs e )
    {
      ShowSelectedContent(e.Node);
    }

  }// Class End

  public class ConfigEventArgs:EventArgs
  {
    public ConfigAction action = ConfigAction.None;
    public ConfigurationContent content = null;
    public IList<string> ChangedOptions = new List<string>();
  }

  public delegate void ConfigFinalSelectionEventHandler( object sender, ConfigEventArgs args);
  
} // Namespace End