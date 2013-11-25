/********************************************************************
  Class      : ucSharedSnippetsOptions
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public partial class ucSharedScriptsOptions : UserControl, IConfigContentEditor
  {
    public ucSharedScriptsOptions( )
    {
      InitializeComponent();
    }
    
    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private SharedScriptsOptions _sharedScriptsOptions = null;
    public SharedScriptsOptions SharedScriptsOptions
    {
      get { return _sharedScriptsOptions; }
    }

    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }

    #region IConfigurationContentItem Members

    public string ItemClassName
    {
      get
      {
        return "SharedScriptsOptions";
      }
    }

    public bool ContentLoaded
    {
      get
      {
        return _isContentLoaded;
      }
    }

    public bool Modified
    {
      get
      {
        return _isModified;
      }
    }

    public bool LoadContent( )
    {
      return LoadContent(ConfigHelper.Current);
    }

    public bool LoadContent( ConfigurationContent configContent )
    {
      if (configContent == null)
      {
        throw new NullParameterException("Configuration content param is null!");
      }

      if (configContent.SharedScriptsOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain SharedScriptsOptions item!");
      }

      _configContent = configContent;
      _sharedScriptsOptions = _configContent.SharedScriptsOptions;


      LoadInitial();

      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      _sharedScriptsOptions.AlwaysShowHelpText = chkAlwaysShowHelpText.Checked;
      _sharedScriptsOptions.AlwaysUseOfflineScriptEditor = chkAlwaysUseOfflineScriptEditor.Checked;
      _sharedScriptsOptions.ConfirmHelpTextSave = chkConfirmHelpTextSave.Checked;
      _sharedScriptsOptions.ShowItemToolTip = chkShowItemToolTip.Checked;
      _isModified = false;
      return true;
    }

    public void ShowContent( )
    {
      this.Show();
    }

    public void HideContent( )
    {
      this.Hide();
    }

    #endregion

    #region Methods
    private void LoadInitial( )
    {
      
      chkAlwaysShowHelpText.Checked = _sharedScriptsOptions.AlwaysShowHelpText;
      chkAlwaysUseOfflineScriptEditor.Checked = _sharedScriptsOptions.AlwaysUseOfflineScriptEditor;
      chkConfirmHelpTextSave.Checked = _sharedScriptsOptions.ConfirmHelpTextSave;
      chkShowItemToolTip.Checked = _sharedScriptsOptions.ShowItemToolTip;

    }
    #endregion

    private void chkAlwaysShowDescription_CheckedChanged( object sender, EventArgs e )
    {
      _isModified = true;
    }

  }
}
