using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using PragmaSQL.Core;

namespace PragmaSQL.ExternalTools
{
	[Serializable]
	public class ExternalToolDef
	{
		private string _uid;
		public string Uid
		{
			get { return _uid; }
			set { _uid = value; }
		}

		private string _command;
		public string Command
		{
			get { return _command; }
			set { _command = value; }
		}

		private string _title;
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		private string _args;
		public string Args
		{
			get { return _args; }
			set { _args = value; }
		}

		private string _workingDir;
		public string WorkingDir
		{
			get { return _workingDir; }
			set { _workingDir = value; }
		}

		private bool _useOutput = false;
		public bool UseOuput
		{
			get { return _useOutput; }
			set { _useOutput = value; }
		}

    private bool _saveBeforeExecute;
    public bool SaveBeforeExecute
    {
      get { return _saveBeforeExecute; }
      set { _saveBeforeExecute = value; }
    }
	

		public override string ToString()
		{
			return this.Title;
		}
	}

	public class ExternalToolsCfg
	{
		private static List<ExternalToolDef> _current = new List<ExternalToolDef>();
		public static List<ExternalToolDef> Current
		{
			get { return _current; }
			set { _current = value; }
		}

		private static string _defaultCfgFile = String.Empty;
		static ExternalToolsCfg()
		{
			_defaultCfgFile = HostServicesSingleton.HostServices.HostOptions.UserAppDataFolder;
			_defaultCfgFile += "ExternalTools.cfg"; 
		}

		public static void Load()
		{
			if (!File.Exists(_defaultCfgFile))
			{
				_current = new List<ExternalToolDef>();
			}
			else
			{
				_current = ObjectXMLSerializer<List<ExternalToolDef>>.Load(_defaultCfgFile);
				if (_current == null)
				{
					_current = new List<ExternalToolDef>();
				}
			}
		}

		public static void Save()
		{
			ObjectXMLSerializer<List<ExternalToolDef>>.Save(_current, _defaultCfgFile);
		}
	}
}
