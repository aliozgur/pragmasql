using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public class Help1Settings
  {
    #region Defaults
    
    public static HelpNavigator defNavigatorCommand = HelpNavigator.KeywordIndex;
    public static string defUrl = String.Empty;
    public static string defParameters = "$WordAtCursor$";
    
    #endregion

    #region Properties
    public HelpNavigator NavigatorCommand = defNavigatorCommand;
    public string Url = defUrl;
    public string Parameters = defParameters;
    
    #endregion

    #region Methods
    public void ResetDefaults()
    {
      NavigatorCommand = defNavigatorCommand;
      Url = defUrl;
      Parameters = defParameters;
    }

    public Help1Settings CreateCopy()
    {
      Help1Settings result = new Help1Settings();
      result.NavigatorCommand = NavigatorCommand;
      result.Url = Url;
      result.Parameters = Parameters;

      return result;
    }

    public void CopyFrom(Help1Settings source)
    {
      this.NavigatorCommand = source.NavigatorCommand;
      this.Url = source.Url;
      this.Parameters = source.Parameters;
    }

    #endregion
  }
}
