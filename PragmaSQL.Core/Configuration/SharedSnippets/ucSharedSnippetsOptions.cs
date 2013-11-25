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
  public partial class ucSharedSnippetsOptions : UserControl, IConfigContentEditor
  {
    public ucSharedSnippetsOptions()
    {
      InitializeComponent();
    }
    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private SharedSnippetsOptions _sharedSnippetsOptions = null;
    public SharedSnippetsOptions SharedSnippetsOptions
    {
      get { return _sharedSnippetsOptions; }
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
        return "SharedSnippetsOptions";
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

      if (configContent.SharedSnippetsOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain SharedSnippetsOptions item!");
      }

      _configContent = configContent;
      _sharedSnippetsOptions = _configContent.SharedSnippetsOptions;


      LoadInitial();

      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      _sharedSnippetsOptions.AlwaysShowDescription = chkAlwaysShowDescription.Checked;
      _sharedSnippetsOptions.AlwaysUseOfflineScriptEditor = chkAlwaysUseOfflineScriptEditor.Checked;
      _sharedSnippetsOptions.ConfirmDescriptionSave = chkConfirmDescriptionSave.Checked;
      _sharedSnippetsOptions.ShowItemToolTip = chkShowItemToolTip.Checked;
      
      if(rdOrderFirst.Checked )
      {
        _sharedSnippetsOptions.CodeCompletionListOrder = SharedSnippetsCodeCompletionListOrder.First;
      }
      else if(rdOrderLast.Checked )
      {
        _sharedSnippetsOptions.CodeCompletionListOrder = SharedSnippetsCodeCompletionListOrder.Last;      
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
    private void LoadInitial( )
    {
      chkAlwaysShowDescription.Checked = _sharedSnippetsOptions.AlwaysShowDescription;
      chkAlwaysUseOfflineScriptEditor.Checked = _sharedSnippetsOptions.AlwaysUseOfflineScriptEditor;
      chkConfirmDescriptionSave.Checked = _sharedSnippetsOptions.ConfirmDescriptionSave;
      chkShowItemToolTip.Checked = _sharedSnippetsOptions.ShowItemToolTip;

      switch(_sharedSnippetsOptions.CodeCompletionListOrder)
      {
        case SharedSnippetsCodeCompletionListOrder.First:
          rdOrderFirst.Checked = true;
          break;
        case SharedSnippetsCodeCompletionListOrder.Last:
          rdOrderLast.Checked = true;
          break;
        default:
          break;
      }
    }
    #endregion

    private void chkAlwaysShowDescription_CheckedChanged( object sender, EventArgs e )
    {
      _isModified = true;
  }

    private void rdOrderFirst_CheckedChanged( object sender, EventArgs e )
    {
      _isModified = true;
    }

  }
}
