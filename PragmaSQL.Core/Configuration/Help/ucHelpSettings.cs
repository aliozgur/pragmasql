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
  public partial class ucHelpSettings : UserControl, IConfigContentEditor
  {
    public ucHelpSettings()
    {
      InitializeComponent();
      PopulateHelpProviderTypes();
      PopulateNavigatorCommands();
    }

    private bool _isInitializing = false;
    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private HelpSettings _helpSettings = null;

    public HelpSettings HelpSettings
    {
      get { return _helpSettings; }
      set { _helpSettings = value; }
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
        return "HelpSettings";
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

    public bool LoadContent()
    {
      return LoadContent(ConfigHelper.Current);
    }

    public bool LoadContent(ConfigurationContent configContent)
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
      _helpSettings = _configContent.HelpSettings;


      LoadInitial();

      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent()
    {
      _helpSettings.ProviderType = (HelpProviderType)Enum.Parse(typeof(HelpProviderType), (string)cmbHelpProviderType.SelectedItem);
      _helpSettings.Help1.NavigatorCommand = (HelpNavigator)Enum.Parse(typeof(HelpNavigator), (string)cmbNavigatorCommands.SelectedItem);
      _helpSettings.Help1.Url = txtHelp1Url.Text ;
      _helpSettings.Help1.Parameters = txtHelp1Params.Text ;

      _helpSettings.Help2.DocumentExplorerPath = txtHelp2Path.Text;
      _helpSettings.Help2.HelpCollection = txtHelp2Collection.Text;
      _helpSettings.Help2.Filter = txtHelp2Filter.Text;

      _helpSettings.CustomOnline.UsePragmaSQLWebBrowser = chkUsePragmaSqlWebBrowser.Checked;
      _helpSettings.CustomOnline.Url = txtCustomHelpUrl.Text;

      
      _isModified = false;
      return true;
    }

    public void ShowContent()
    {
      this.Show();
    }

    public void HideContent()
    {
      this.Hide();
    }

    #endregion

    #region Methods
    private void LoadInitial()
    {
      _isInitializing = true;
      cmbHelpProviderType.SelectedItem = _helpSettings.ProviderType.ToString();
      LoadHelp1Settings();
      LoadHelp2Settings();
      LoadCustomOnlineSettings();
      _isInitializing = false;
    }

    private void LoadHelp1Settings()
    {
      cmbNavigatorCommands.SelectedItem = _helpSettings.Help1.NavigatorCommand.ToString();
      txtHelp1Url.Text = _helpSettings.Help1.Url;
      txtHelp1Params.Text = _helpSettings.Help1.Parameters;
    }

    private void LoadHelp2Settings()
    {
      txtHelp2Path.Text = _helpSettings.Help2.DocumentExplorerPath;
      txtHelp2Collection.Text = _helpSettings.Help2.HelpCollection;
      txtHelp2Filter.Text = _helpSettings.Help2.Filter;
    }

    private void LoadCustomOnlineSettings()
    {
      chkUsePragmaSqlWebBrowser.Checked = _helpSettings.CustomOnline.UsePragmaSQLWebBrowser;
      txtCustomHelpUrl.Text = _helpSettings.CustomOnline.Url;
    }

    private void PopulateHelpProviderTypes()
    {
      _isInitializing = true;
      cmbHelpProviderType.Items.Clear();

      string[] names = Enum.GetNames(typeof(HelpProviderType));
      foreach (string name in names)
      {
        cmbHelpProviderType.Items.Add(name);   
      }
      _isInitializing = false;
   
    }

    private void PopulateNavigatorCommands()
    {
      _isInitializing = true;
      cmbNavigatorCommands.Items.Clear();

      string[] names = Enum.GetNames(typeof(HelpNavigator));
      foreach (string name in names)
      {
        cmbNavigatorCommands.Items.Add(name);
      }
      _isInitializing = false;

    }

    
    #endregion

    private void cmbHelpProviderType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }

      _isModified = true;
    }

    private void txtHelp1Url_TextChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }

      _isModified = true;
    }

    private void chkUsePragmaSqlWebBrowser_CheckedChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }

      _isModified = true;
    }

    private void label11_DoubleClick(object sender, EventArgs e)
    {
      txtHelp1Params.SelectedText = "$WordAtCursor$";
    }

    private void label10_DoubleClick(object sender, EventArgs e)
    {
      txtCustomHelpUrl.AppendText("$WordAtCursor$");
    }
  }
}
