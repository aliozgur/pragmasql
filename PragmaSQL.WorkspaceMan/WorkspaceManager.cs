using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using PragmaSQL.Core;

namespace PragmaSQL.WorkspaceMan
{
	public static class WorkspaceManager
	{
		private static string _defaultFileName = String.Empty;
		public static List<WorkspaceItem> Items = null;

		static WorkspaceManager()
    {
      string appDataDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL";
      if(!Directory.Exists(appDataDir))
      {
        Directory.CreateDirectory(appDataDir);
      }
			_defaultFileName = appDataDir + "\\PragmaSQL.Workspace";
    }

		public static List<WorkspaceItem> LoadWorkspaceFrom(string fileName)
		{
			List<WorkspaceItem> result = null;

			if (!File.Exists(fileName))
			{
				Items = new List<WorkspaceItem>();
				return Items;
			}

			result = ObjectXMLSerializer<List<WorkspaceItem>>.Load(fileName);
			if (result == null)
			{
				result = new List<WorkspaceItem>();
			}
			Items = result;
			return result;
		}

		public static List<WorkspaceItem> LoadFromDefault()
		{
			return LoadWorkspaceFrom(_defaultFileName);
		}

		public static void SaveWorkspaceAs(List<WorkspaceItem> items, string fileName)
		{
			ObjectXMLSerializer<List<WorkspaceItem>>.Save(items, fileName);
		}

		public static void SaveAsDefault(List<WorkspaceItem> items)
		{
			SaveWorkspaceAs(items, _defaultFileName);
		}

	}
}
