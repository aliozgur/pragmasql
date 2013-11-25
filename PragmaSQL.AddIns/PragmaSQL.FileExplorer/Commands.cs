using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL.FileExplorer
{
	public class RunExplorer : AbstractMenuCommand
	{
		public override void Run()
		{
			frmFileExplorer frm = new frmFileExplorer();
			HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.DockLeft);
		}
	}
	
}
