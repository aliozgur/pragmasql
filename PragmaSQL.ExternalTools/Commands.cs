using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL.ExternalTools
{
	public class ConfigureTools : AbstractMenuCommand
	{
		public override void Run()
		{
			if (ConfigForm.ConfigureExternalTools() == DialogResult.OK && RunToolForm.HasInstance)
			{
				RunToolForm.ShowForm(ExternalToolsCfg.Current);
			}
		}
	}


  public class ExternalToolsSubscribeToEventsCommand : AbstractMenuCommand
	{
    public override void Run()
    {
      HostServicesSingleton.HostServices.ConfigSvc.DialogOpened += ucExternalToolsOptions.ConfigSvc_DialogOpened;
      HostServicesSingleton.HostServices.ConfigSvc.DialogClosed += ucExternalToolsOptions.ConfigSvc_DialogClosed;
      HostServicesSingleton.HostServices.ConfigSvc.FinalSelection += ucExternalToolsOptions.ConfigSvc_FinalSelection;
    }
	}

	public class RunExternalTool : AbstractMenuCommand
	{
		public override void Run()
		{
			ExternalToolsCfg.Load();
			RunToolForm.ShowForm(ExternalToolsCfg.Current);
		}
	}
	
}
