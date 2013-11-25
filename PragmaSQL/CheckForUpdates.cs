using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

using ICSharpCode.Core;

using PragmaSQL.ProducUpdateCheck;
using PragmaSQL.Core;

using PragmaSQL.Licencing;

namespace PragmaSQL
{
	public static class CheckForUpdates
	{
		private static ProductInfo pi = new ProductInfo();
		private static object syncObj = new object();

		public static void ExecuteSync(bool silent)
		{
			try
			{
				FuzzyWait.ShowFuzzyWait("Checking for updates...");
				string version = String.Empty;
				string newVersion = String.Empty;
				bool hasUpdate;

				ExecuteInternal(out version, out newVersion, out hasUpdate);

				string msg = String.Format(Properties.Resources.ProductUpdateNotification, pi.CurrentCodeName.ToString(), version, newVersion);
				FuzzyWait.CloseFuzzyWait();

				if (hasUpdate)
				{
					if (!MessageService.AskQuestion(msg))
						return;
					HostServicesSingleton.HostServices.WebBrowserService.Navigate(Properties.Settings.Default.PragmaSQLDownloadsUrl);
				}
				else if (!silent)
				{
					MessageService.ShowMessage("No new version available.");
				}
			}
			catch (Exception ex)
			{
				FuzzyWait.CloseFuzzyWait();
				frmException.ShowAppError("Can not check for updates!", ex);
			}
		}

		public static VersionInfo ExecuteAsync()
		{
			lock (syncObj)
			{
				bool hasUpdate = false;

				HostServicesSingleton.HostServices.MsgService.InfoMsg("Checking for updates...");
				string version = String.Empty;
				string newVersion = String.Empty;

				ExecuteInternal(out version, out newVersion, out hasUpdate);
				
				VersionInfo vi = new VersionInfo();
				vi.CodeName = pi.CurrentCodeName.ToString();
				vi.HasUpdate = hasUpdate;
				vi.Version = version;
				vi.NewVersion = newVersion;

				return vi;
			}
		}

		private static void ExecuteInternal(out string version, out string newVersion, out bool hasUpdate)
		{
			CheckUpdates svc = new CheckUpdates();
			Assembly ass = Assembly.GetExecutingAssembly();
			version = ass.GetName().Version.ToString();
			newVersion = String.Empty;
			hasUpdate = svc.HasUpdate("PragmaSQL", pi.CurrentCodeName.ToString(), version, ref newVersion);
		}
	}

	public struct VersionInfo
	{
		public string CodeName;
		public string Version;
		public string NewVersion;
		public bool HasUpdate;
	}
}
