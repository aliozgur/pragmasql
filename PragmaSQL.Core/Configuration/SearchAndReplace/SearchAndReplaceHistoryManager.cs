using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
	public static class SearchAndReplaceHistoryManager
	{
		private static string _defaultFileName = String.Empty;
		public static SearchAndReplaceHistory Hist = null;

		static SearchAndReplaceHistoryManager()
    {
      string appDataDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL";
      if(!Directory.Exists(appDataDir))
      {
        Directory.CreateDirectory(appDataDir);
      }
			_defaultFileName = appDataDir + "\\SearchAndReplaceHistory.config";
    }

		public static SearchAndReplaceHistory LoadWorkspaceFrom(string fileName)
		{
			SearchAndReplaceHistory result = null;

			if (!File.Exists(fileName))
			{
				Hist = new SearchAndReplaceHistory();
				return Hist;
			}

			result = ObjectXMLSerializer<SearchAndReplaceHistory>.Load(fileName);
			if (result == null)
			{
				result = new SearchAndReplaceHistory();
			}
			Hist = result;
			return result;
		}

		public static SearchAndReplaceHistory LoadFromDefault()
		{
			return LoadWorkspaceFrom(_defaultFileName);
		}

		public static void SaveHistAs(SearchAndReplaceHistory hist, string fileName)
		{
			ObjectXMLSerializer<SearchAndReplaceHistory>.Save(hist, fileName);
		}

		public static void SaveAsDefault(SearchAndReplaceHistory hist)
		{
			SaveHistAs(hist, _defaultFileName);
		}

	}
}
