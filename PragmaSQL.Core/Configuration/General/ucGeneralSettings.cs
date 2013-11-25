/********************************************************************
  Class      : ucGeneralSettings
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Common;

namespace PragmaSQL
{
  public partial class ucGeneralSettings : UserControl,IConfigurationContentItem
  {
    private bool _isInitializing = false;
    private bool _isModified = false;
    private bool _isContentLoaded = false;


    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }


    public ucGeneralSettings( )
    {
      InitializeComponent();
    }

    #region IConfigurationContentItem Members

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

    public bool LoadContent( ConfigurationContent configContent )
    {
      if (configContent == null)
      {
        throw new NullParameterException("Configuration content param is null!");
      }

      if (configContent.TextEditorOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain TextEditorOptions item!");
      }

      _configContent = configContent;
      //TODO:
      LoadInitial();
      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      //TODO:
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
      //TODO:
    }
    #endregion

  }
}
