using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public partial class ucObjectExplorerOptions : UserControl, IConfigContentEditor
  {
    public ucObjectExplorerOptions()
    {
      InitializeComponent();
    }
    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private ObjectExplorerOptions _options= null;
    public ObjectExplorerOptions Options
    {
      get { return _options; }
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
        return "ObjectExplorerOptions";
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

      if (configContent.ObjectExplorerOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain ObjectExplorerOptions item!");
      }

      _configContent = configContent;
      _options = _configContent.ObjectExplorerOptions;


      LoadInitial();

      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent()
    {
      _options.ShowSystemTables = chkShowSysTables.Checked;
      _options.ShowDatabaseCompatibilityLevel = chkShowDbComLevel.Checked;
      _options.ShowSystemDatabases = chkShowSysDatabases.Checked;
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
      chkShowSysTables.Checked = _options.ShowSystemTables;
      chkShowDbComLevel.Checked = _options.ShowDatabaseCompatibilityLevel;
      chkShowSysDatabases.Checked = _options.ShowSystemDatabases;
    }
    #endregion

    private void chkShowFullObjName_CheckStateChanged(object sender, EventArgs e)
    {
      _isModified = true;
    }

  }
}
