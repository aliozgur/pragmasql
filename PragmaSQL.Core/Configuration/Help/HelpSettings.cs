using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class HelpSettings
  {
    public HelpProviderType ProviderType = HelpProviderType.Help2;

    public Help1Settings Help1 = new Help1Settings();
    public Help2Settings Help2 = new Help2Settings();
    public CustomOnlineHelpSettings  CustomOnline = new CustomOnlineHelpSettings();


    public HelpSettings CreateCopy()
    {
      HelpSettings result = new HelpSettings();
      result.Help1.CopyFrom(this.Help1);
      result.Help2.CopyFrom(this.Help2);
      result.CustomOnline.CopyFrom(this.CustomOnline);
      result.ProviderType = this.ProviderType;

      return result;
    }

    public void CopyFrom(HelpSettings source)
    {
      this.Help1.CopyFrom(source.Help1);
      this.Help2.CopyFrom(source.Help2);
      this.CustomOnline.CopyFrom(source.CustomOnline);
      this.ProviderType = source.ProviderType;
    }

  }//Class end
}//namespace end
