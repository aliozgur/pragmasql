// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision: 1965 $</version>
// </file>

namespace HtmlHelp2
{
	using System;
	using ICSharpCode.Core;

	public class MSHelpProvider 
	{
		public bool TryShowHelp(string fullTypeName)
		{
			LoggingService.Info("Help 2.0: MsHelpProvider.TryShowHelp");
      throw new Exception("Not implemented yet!");
      /*
		  PadDescriptor search = WorkbenchSingleton.Workbench.GetPad(typeof(HtmlHelp2SearchPad));
			return ((HtmlHelp2SearchPad)search.PadContent).PerformF1Fts(fullTypeName, true);
		  */
    }

		public bool TryShowHelpByKeyword(string keyword)
		{
			LoggingService.Info("Help 2.0: MsHelpProvider.TryShowHelpByKeyword");
      throw new Exception("Not implemented yet!");
      /*
			LadDescriptor search = WorkbenchSingleton.Workbench.GetPad(typeof(HtmlHelp2SearchPad));
			return ((HtmlHelp2SearchPad)search.PadContent).PerformF1Fts(keyword);
      */
		}
	}
}
