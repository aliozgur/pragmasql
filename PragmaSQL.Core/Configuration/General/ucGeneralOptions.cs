/********************************************************************
  Class      : ucGeneralSettings
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
using System.Reflection;
using System.IO;


using PragmaSQL.Core;
using PragmaSQL.WebBrowserEx;
using WeifenLuo.WinFormsUI.Docking;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
  public partial class ucGeneralOptions : UserControl,IConfigContentEditor
  {
    private bool _isInitializing = false;
    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private GeneralOptions _generalOptions = null;
    public GeneralOptions GeneralOptions
    {
      get { return _generalOptions; }
    }

    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }


    public ucGeneralOptions( )
    {
      InitializeComponent();
			cmbDocStyle.Items.Add(DocumentStyle.DockingMdi);
			cmbDocStyle.Items.Add(DocumentStyle.SystemMdi);

      FillPaletteModes();
      chkUseCustomPalette.Enabled = File.Exists(ConfigHelper.CustomPaletteFileName);
      lblEditPanel.Text = chkUseCustomPalette.Enabled ? "Edit Custom Palette" : "Create Custom Palette";
    }

    private void FillPaletteModes()
    {
      Array values = Enum.GetValues(typeof(PaletteModeManager));
      foreach (object val in values)
        cmbPaletteMode.Items.Add(val);

      cmbPaletteMode.Items.Remove(PaletteModeManager.Custom);
    }

    #region IConfigurationContentItem Members

    public string ItemClassName
    {
      get
      {
        return "GeneralOptions";
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

      if (configContent.GeneralOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain GeneralOptions item!");
      }

      _configContent = configContent;
      _generalOptions = _configContent.GeneralOptions;

      
      LoadInitial();
      
      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      _generalOptions.WebBrowser_ShowonStart = chkShowWebBrowserOnStartup.Checked;
      _generalOptions.AutoSaveEnabled = chkAutoSave.Checked;
      _generalOptions.AutoSaveInterval = (int)nudAutoSaveInterval.Value;

      _generalOptions.WebBrowser_HomeUrl = txtHomeUrl.Text;
      _generalOptions.IsSingleInstance = chkSingleInstance.Checked;
      _generalOptions.UseConnectionPooling = chkConnectionPooling.Checked;
			//_generalOptions.CheckForUpdatesOnStart = chkUpdatesOnStart.Checked;
      _generalOptions.UseCustomPalette = chkUseCustomPalette.Checked;
      _generalOptions.RestoreWorkspace = chkRestoreWorkspace.Checked;
      _generalOptions.WantEmptyScriptEditorOnStart = chkStartWithNewScriptEditor.Checked;
			_generalOptions.HostDocumentStyle = (DocumentStyle)cmbDocStyle.SelectedItem;
      _generalOptions.PaletteMode = (PaletteModeManager) cmbPaletteMode.SelectedItem;

			PopupBlockerFilterLevel lvl = (PopupBlockerFilterLevel)Enum.Parse(typeof(PopupBlockerFilterLevel), cmbPopupBlockerFilterLvl.Text);
      if(lvl != _generalOptions.WebBrowser_PopupBlockerFilterLevel)
      {
        _generalOptions.WebBrowser_PopupBlockerFilterLevel = lvl;
      }

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
    private void LoadInitial()
    {
      _isInitializing = true;
      
      chkShowWebBrowserOnStartup.Checked = _generalOptions.WebBrowser_ShowonStart;
      chkAutoSave.Checked = _generalOptions.AutoSaveEnabled;
      nudAutoSaveInterval.Value = _generalOptions.AutoSaveInterval;

      txtHomeUrl.Text = _generalOptions.WebBrowser_HomeUrl;
      cmbPopupBlockerFilterLvl.Items.Clear();
      chkRestoreWorkspace.Checked = _generalOptions.RestoreWorkspace;
			cmbDocStyle.SelectedItem = _generalOptions.HostDocumentStyle;
      cmbPaletteMode.SelectedItem = _generalOptions.PaletteMode;

      chkStartWithNewScriptEditor.Checked = _generalOptions.WantEmptyScriptEditorOnStart;

			string[] popupBlockerLvls = Enum.GetNames(typeof(PopupBlockerFilterLevel));
      foreach(string popupBlockerLvl in popupBlockerLvls)
      {
        cmbPopupBlockerFilterLvl.Items.Add(popupBlockerLvl);
        if(popupBlockerLvl.ToLowerInvariant() == _generalOptions.WebBrowser_PopupBlockerFilterLevel.ToString().ToLowerInvariant())
        {
          cmbPopupBlockerFilterLvl.SelectedIndex = cmbPopupBlockerFilterLvl.Items.Count-1;
        }
      }

      chkSingleInstance.Checked = _generalOptions.IsSingleInstance;
      chkConnectionPooling.Checked = _generalOptions.UseConnectionPooling;
			//chkUpdatesOnStart.Checked = _generalOptions.CheckForUpdatesOnStart;
      chkUseCustomPalette.Checked = _generalOptions.UseCustomPalette;
      _isInitializing = false;

    }
    #endregion

    private void OnCheckedChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

    private void txtHomeUrl_TextChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;

    }

    private void btnGoToUrl_Click(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(txtHomeUrl.Text))
      {
        return;
      }

			IWebBrowser	wb = HostServicesSingleton.HostServices.WebBrowserService.CreateAndBrowse(String.Empty, txtHomeUrl.Text);
			if(wb != null)
				HostServicesSingleton.HostServices.ShowForm((Form)wb, AddInDockState.Document);
    }

    private void ucGeneralOptions_Load( object sender, EventArgs e )
    {
      textBox1.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL";

      Assembly thisAss = System.Reflection.Assembly.GetExecutingAssembly();
      FileInfo fi = new FileInfo(thisAss.Location);
      textBox2.Text = fi.Directory.FullName;
    }

    private void button1_Click( object sender, EventArgs e )
    {
      System.Diagnostics.Process.Start(textBox1.Text);
    }

    private void cmbPopupBlockerFilterLvl_SelectedIndexChanged( object sender, EventArgs e )
    {
      if(_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

    private void button2_Click( object sender, EventArgs e )
    {
      System.Diagnostics.Process.Start(textBox2.Text);
    }

		private void cmbDocStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_isInitializing)
			{
				return;
			}
			_isModified = true;
		}

    private void label9_Click(object sender, EventArgs e)
    {
      DialogResult dlgResult = frmCustomPaletteEditor.ShowCustomPaletteEditor();
      chkUseCustomPalette.Enabled = File.Exists(ConfigHelper.CustomPaletteFileName);
      lblEditPanel.Text = chkUseCustomPalette.Enabled ? "Edit Custom Palette" : "Create Custom Palette";

      if (dlgResult == DialogResult.OK)
        _isModified = true;
    }

    private void chkUseCustomPalette_CheckedChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }


      _isModified = true;
    }

  }
}
