/********************************************************************
  Class      : CustomHelpSettings
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class CustomOnlineHelpSettings
  {
    private static bool defUsePragmaSQLWebBrowser = true;
    private static string defUrl = String.Empty;

    #region Properties
    
    public bool UsePragmaSQLWebBrowser = defUsePragmaSQLWebBrowser;
    public string Url = defUrl;
    
    #endregion

    #region Methods
    
    public void ResetDefaults()
    {
      this.UsePragmaSQLWebBrowser = defUsePragmaSQLWebBrowser;
      this.Url = defUrl;    
    }
    
    public CustomOnlineHelpSettings CreateCopy()
    {
      CustomOnlineHelpSettings result = new CustomOnlineHelpSettings();
      result.UsePragmaSQLWebBrowser = this.UsePragmaSQLWebBrowser;
      result.Url = this.Url;

      return result;
    }

    public void CopyFrom(CustomOnlineHelpSettings source)
    {
      this.UsePragmaSQLWebBrowser = source.UsePragmaSQLWebBrowser;
      this.Url = source.Url;
    }

    #endregion


  }
}
