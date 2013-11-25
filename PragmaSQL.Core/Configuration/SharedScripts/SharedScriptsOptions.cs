/********************************************************************
  Class      : SharedScriptsOptions
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class SharedScriptsOptions
  {
    #region Default options
    public static bool defAlwaysShowHelpText = false;
    public static bool defAlwaysUseOfflineScriptEditor = false;
    public static bool defConfirmHelpTextSave = true;
    public static bool defShowItemToolTip = false;
    #endregion

    #region Fields

    public bool AlwaysShowHelpText = false;
    public bool AlwaysUseOfflineScriptEditor = false;
    public bool ConfirmHelpTextSave = true;
    public bool ShowItemToolTip = false;

    #endregion

    #region Methods

    public void ResetToDefaults()
    {
      AlwaysShowHelpText = defAlwaysShowHelpText;
      AlwaysUseOfflineScriptEditor = defAlwaysUseOfflineScriptEditor;
      ConfirmHelpTextSave = defConfirmHelpTextSave;
      ShowItemToolTip = defShowItemToolTip;
    }

    public void CopyFrom(SharedScriptsOptions source)
    {
      AlwaysShowHelpText = source.AlwaysShowHelpText;
      AlwaysUseOfflineScriptEditor = source.AlwaysUseOfflineScriptEditor;
      ConfirmHelpTextSave = source.ConfirmHelpTextSave;
      ShowItemToolTip = source.ShowItemToolTip;
    }

    public SharedScriptsOptions CreateCopy()
    {
      SharedScriptsOptions newOpts = new SharedScriptsOptions();
      newOpts.AlwaysShowHelpText = this.AlwaysShowHelpText;
      newOpts.AlwaysUseOfflineScriptEditor = this.AlwaysUseOfflineScriptEditor;
      newOpts.ConfirmHelpTextSave = this.ConfirmHelpTextSave;
      newOpts.ShowItemToolTip = this.ShowItemToolTip;

      return newOpts;
    }
    #endregion
  }
}
