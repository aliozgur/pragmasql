// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System;
using System.Security.Permissions;
using System.Windows.Forms;

using ICSharpCode.Core;

namespace HtmlHelp2
{
	public abstract class HelpToolbarCommand : AbstractCommand
	{
		public static HtmlHelp2TocPad TocPad
		{
			get
			{
        return HtmlHelp2TocPad.Current;
			}
		}
		
		public WebBrowser Browser
		{
			get
			{
        return WebBrowserHelper.WebBrowser;
			}
		}
		
		public static void BringTocPadToFront()
		{
      HtmlHelp2TocPad.Current.ShowPad();
		}
	}
	
	[PermissionSet(SecurityAction.LinkDemand, Name="Execution")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name="Execution")]
	public class SyncTocCommand : HelpToolbarCommand
	{
		public override void Run()
		{
			TocPad.SyncToc(Browser.Url.ToString());
			BringTocPadToFront();
		}
	}
	
	[PermissionSet(SecurityAction.LinkDemand, Name="Execution")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name="Execution")]
	public class PreviousTopicCommand : HelpToolbarCommand
	{
		public override void Run()
		{
			try
			{
				TocPad.GetPrevFromNode();
			}
			catch (System.ArgumentException)
			{
				TocPad.GetPrevFromUrl(Browser.Url.ToString());
			}
			BringTocPadToFront();
		}
	}
	
	[PermissionSet(SecurityAction.LinkDemand, Name="Execution")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name="Execution")]
	public class NextTopicCommand : HelpToolbarCommand
	{
		public override void Run()
		{
			try
			{
				TocPad.GetNextFromNode();
			}
			catch (System.ArgumentException)
			{
				TocPad.GetNextFromUrl(Browser.Url.ToString());
			}
			BringTocPadToFront();
		}
	}
}
