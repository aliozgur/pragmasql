/********************************************************************
  Class      : SharedSnippetsOptions
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public enum SharedSnippetsCodeCompletionListOrder
  {
    First,
    Last
  }

  public class SharedSnippetsOptions
  {
    #region Default options
    public static bool defAlwaysShowDescription = false;
    public static bool defAlwaysUseOfflineScriptEditor = false;
    public static bool defConfirmDescriptionSave = true;
    public static bool defShowItemToolTip = false;
    public static SharedSnippetsCodeCompletionListOrder defCodeCompletionListOrder = SharedSnippetsCodeCompletionListOrder.First;
    #endregion

    #region Fields
    
    public bool AlwaysShowDescription = false;
    public bool AlwaysUseOfflineScriptEditor = false;
    public bool ConfirmDescriptionSave = true;
    public bool ShowItemToolTip = false;
    public SharedSnippetsCodeCompletionListOrder CodeCompletionListOrder = SharedSnippetsCodeCompletionListOrder.First;
    
    #endregion 

    #region Methods
    
    public void ResetToDefaults()
    {
      AlwaysShowDescription = defAlwaysShowDescription;
      AlwaysUseOfflineScriptEditor = defAlwaysUseOfflineScriptEditor;
      ConfirmDescriptionSave = defConfirmDescriptionSave;
      ShowItemToolTip = defShowItemToolTip;
      CodeCompletionListOrder = defCodeCompletionListOrder;
    }

    public void CopyFrom(SharedSnippetsOptions source)
    {
      AlwaysShowDescription = source.AlwaysShowDescription;
      AlwaysUseOfflineScriptEditor = source.AlwaysUseOfflineScriptEditor;
      ConfirmDescriptionSave = source.ConfirmDescriptionSave;
      ShowItemToolTip = source.ShowItemToolTip;
      CodeCompletionListOrder = source.CodeCompletionListOrder;
    }

    public SharedSnippetsOptions CreateCopy()
    {
      SharedSnippetsOptions newOpts = new SharedSnippetsOptions();
      newOpts.AlwaysShowDescription = this.AlwaysShowDescription;
      newOpts.AlwaysUseOfflineScriptEditor = this.AlwaysUseOfflineScriptEditor;
      newOpts.ConfirmDescriptionSave = this.ConfirmDescriptionSave;
      newOpts.ShowItemToolTip = this.ShowItemToolTip;
      newOpts.CodeCompletionListOrder = this.CodeCompletionListOrder;

      return newOpts;
    }
    #endregion
  }
}
