/********************************************************************
  Class AddInLoader
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;

using System.Windows.Forms;

using ICSharpCode.Core;

using PragmaSQL.Proxy;
using PragmaSQL.Core;

namespace PragmaSQL
{
	internal static class ICSharpCoreWrapper
	{
		internal static CoreStartup Core = null;

		internal static void StartCoreServices()
		{
			frmSplashScreen.PrintStatus("Starting core services...");

			Assembly exe = PragmaSQLApp.ApplicationProxy;
			FileUtility.ApplicationRootPath = Path.GetDirectoryName(exe.Location);

			Core = new CoreStartup("PragmaSQL");

			// Specify the name of the application settings file (.xml is automatically appended)
			Core.PropertiesName = "AppProperties";

			// Initializes the Core services (ResourceService, PropertyService, etc.)
			Core.StartCoreServices();

			PragmaSQLApp.RegisterApplicationResources();

			//ResourceService.RegisterNeutralStrings(new ResourceManager("PragmaSQL.StringResources", exe));
			//ResourceService.RegisterNeutralImages(new ResourceManager("PragmaSQL.ImageResources", exe));

			// Registeres the default (English) strings and images. They are compiled as
			// Localized strings are automatically picked up when they are put into the
			// "data/resources" directory.
		}
	}

	internal static class AddInLoader
	{
		internal static bool Load()
		{
			bool result = false;
			try
			{
				frmSplashScreen.PrintStatus("Loading AddIns...");

				// Searches for ".addin" files in the application directory.
				string addInDir = Path.Combine(FileUtility.ApplicationRootPath, "AddIns");
				if (!Directory.Exists(addInDir))
				{
					Directory.CreateDirectory(addInDir);
				}
				
        ICSharpCoreWrapper.Core.AddAddInsFromDirectory(addInDir);

				// Searches for a "AddIns.xml" in the user profile that specifies the names of the
				// add-ins that were deactivated by the user, and adds "external" AddIns.
				ICSharpCoreWrapper.Core.ConfigureExternalAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddIns.xml"));

				// Searches for add-ins installed by the user into his profile directory. This also
				// performs the job of installing, uninstalling or upgrading add-ins if the user
				// requested it the last time this application was running.
				ICSharpCoreWrapper.Core.ConfigureUserAddIns(Path.Combine(PropertyService.ConfigDirectory, "AddInInstallTemp"),
																				Path.Combine(PropertyService.ConfigDirectory, "AddIns"));

				LoggingService.Info("Loading AddInTree...");
				// Now finally initialize the application. This parses the ".addin" files and
				// creates the AddIn tree. It also automatically runs the commands in
				// "/Workspace/Autostart"
				ICSharpCoreWrapper.Core.RunInitialization();

				if (ICSharpCoreWrapper.Core.AddInLoadErrors.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string err in ICSharpCoreWrapper.Core.AddInLoadErrors)
					{
						LoggingService.Error(err);
						sb.AppendLine(err);
					}

					GenericErrorDialog.ShowError("Error", "Can not load listed AddIns.", sb.ToString());
				}

				LoggingService.Info("AddIns loaded sucessfully.");
				result = true;
      }
			catch (Exception ex)
			{
				frmSplashScreen.HideSplash();
				frmException.ShowAppError("Fatal Error : AddIns can not be loaded", ex);
			}

			return result;
		}

	}
}
