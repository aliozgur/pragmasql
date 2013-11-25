using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL.FastExcelExport
{
	public class ExportAll : AbstractMenuCommand
	{
		public override void Run()
		{
      if (HostServicesSingleton.HostServices.EditorServices.CurrentScriptEditor == null)
        return;
      Exporter.ExportAll(HostServicesSingleton.HostServices.EditorServices.CurrentScriptEditor.ScriptExecutionResults);
		}
	}


  public class ExportCurrent : AbstractMenuCommand
	{
    public override void Run()
    {
      if (HostServicesSingleton.HostServices.EditorServices.CurrentScriptEditor == null)
        return;

      DataView dw = HostServicesSingleton.HostServices.EditorServices.CurrentScriptEditor.ActiveDataView;
      if (dw == null)
      {
        Utils.ShowError("Active resultset not selected\r\nPlease select a resultset.", MessageBoxButtons.OK);
        return;
      }

      Exporter.Export(dw.Table);

    }
	}
	
}
