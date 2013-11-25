using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.Core;


using System.Reflection;

namespace PragmaSQL.Proxy
{
	public static class PragmaSQLApp
	{
    public static readonly string UserDataFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "PragmaSQL");
    public static readonly string AppStartIndicatorPath = Path.Combine(UserDataFolder, "AppStartIndicator.tmp");

    public static Stream GetCompiledSqlGrammarStream()
		{
			return ApplicationProxy.GetManifestResourceStream("PragmaSQL.Proxy.sqlselect.cgt");
		}

		public static string ApplicationRootPath()
		{
			return Path.GetDirectoryName(ApplicationProxy.Location);		
		}

		public static Assembly ApplicationProxy
		{
			get
			{
				return Assembly.GetExecutingAssembly();
			}
		}
    

		public static void RegisterApplicationResources()
		{
	  	Assembly pAss = ApplicationProxy;
			ResourceService.RegisterStrings("PragmaSQL.Proxy.StringResources", pAss);
			ResourceService.RegisterImages("PragmaSQL.Proxy.ImageResources", pAss);
		}

    public static void CreateAppStartIndicator()
    {
      FileStream fs = new FileStream(AppStartIndicatorPath, FileMode.Create);
      fs.Flush();
      fs.Close();
    }

    public static void RemoveAppStartIndicator()
    {
      if (File.Exists(AppStartIndicatorPath))
        File.Delete(AppStartIndicatorPath);
    }

    public static bool AppStartIndicatorExist
    {
      get { return File.Exists(AppStartIndicatorPath); } 
    }


	}
}
