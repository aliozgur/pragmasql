/********************************************************************
  Class      : ConfigurationContent
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
 
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  [Serializable]
  public class ConfigurationContent
  {
    #region Fields and Properties
    private TextEditorOptions _textEditorOptions = new TextEditorOptions();
    public TextEditorOptions TextEditorOptions
    {
      get { return _textEditorOptions; }
      set { _textEditorOptions = value; }
    }

    private SerializableDictionary<ScriptEditorActions, Keys> _scriptEditorShortcuts = new SerializableDictionary<ScriptEditorActions,Keys>();
    public SerializableDictionary<ScriptEditorActions, Keys> ScriptEditorShortcuts
    {
      get { return _scriptEditorShortcuts; }
      set { _scriptEditorShortcuts = value; }
    }

    private GeneralOptions _generalOptions = new GeneralOptions();
    public GeneralOptions GeneralOptions
    {
      get { return _generalOptions; }
      set { _generalOptions = value; }
    }

    private ConnectionParams _pragmsSqlDbConn = new ConnectionParams();
    public ConnectionParams PragmaSqlDbConn
    {
      get { return _pragmsSqlDbConn; }
      set { _pragmsSqlDbConn = value; }
    }

    private SharedSnippetsOptions _sharedSnippetsOptions = new SharedSnippetsOptions();
    public SharedSnippetsOptions SharedSnippetsOptions
    {
      get { return _sharedSnippetsOptions; }
      set { _sharedSnippetsOptions = value; }
    }

    private SharedScriptsOptions _sharedScriptsOptions = new SharedScriptsOptions();
    public SharedScriptsOptions SharedScriptsOptions
    {
      get { return _sharedScriptsOptions; }
      set { _sharedScriptsOptions = value; }
    }


    private SystemFileAssociationOptions _systemFileAssociationOptions = new SystemFileAssociationOptions();
    public SystemFileAssociationOptions SystemFileAssociationOptions
    {
      get { return _systemFileAssociationOptions; }
      set { _systemFileAssociationOptions = value; }
    }

    private ObjectExplorerOptions _objectExplorerOptions = new ObjectExplorerOptions();
    public ObjectExplorerOptions ObjectExplorerOptions
    {
      get { return _objectExplorerOptions; }
      set { _objectExplorerOptions = value; }
    }

    public bool PragmaSql_UtilsDisabled = true;
    public bool PragmaSql_ObjectChangeHistoryLogEnabled = false;
    public bool PragmaSql_SharedSnippetsEnabled = false;
    public bool PragmaSql_SharedScriptsEnabled = false;

    private HelpSettings _helpSettings = new HelpSettings();
    public HelpSettings HelpSettings
    {
      get { return _helpSettings; }
      set { _helpSettings = value; }
    }

    private List<WebSearchSite> _webSearchOptions = new List<WebSearchSite>();
    public List<WebSearchSite> WebSearchOptions
    {
      get { return _webSearchOptions; }
      set { _webSearchOptions = value; }
    }

		private string _defaultResultRenderer;
		public string DefaultResultRenderer
		{
			get { return _defaultResultRenderer; }
			set { _defaultResultRenderer = value; }
		}

    #endregion //Fields and Properties

    #region Constructor
    
    public ConfigurationContent()
    {
    
    }
    
    #endregion //Constructor

    #region Methods
		public bool CanLogObjectChanges()
		{
			return !PragmaSql_UtilsDisabled && PragmaSql_ObjectChangeHistoryLogEnabled;
		}

		public bool SharedSnippetsEnabled()
		{
			return !PragmaSql_UtilsDisabled && PragmaSql_SharedSnippetsEnabled;
		}

		public bool SharedScriptsEnabled()
		{
			return !PragmaSql_UtilsDisabled && PragmaSql_SharedScriptsEnabled;
		}
		#endregion //Methods
  }
}
