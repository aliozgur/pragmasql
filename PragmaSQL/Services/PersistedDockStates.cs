using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PragmaSQL.Core;

namespace PragmaSQL
{
	public class PersistedDockStateData 
	{
		private SerializableDictionary<string,AddInDockState> _dockStates= new SerializableDictionary<string,AddInDockState>();

		public SerializableDictionary<string, AddInDockState> DockStates
		{
			get{return _dockStates;}
			set{_dockStates = value;}
		}

		public void AddState(Type type, AddInDockState state)
		{
			if (type == null)
				return;
			
			if (_dockStates.ContainsKey(type.Name))
				_dockStates[type.Name] = state;
			else
				_dockStates.Add(type.Name, state);
		}

		public void RemoveState(Type type)
		{
			if (type == null || !_dockStates.ContainsKey(type.Name))
				return;

			_dockStates.Remove(type.Name);
		}

		public AddInDockState GetState(Type type)
		{
			if (type == null || !_dockStates.ContainsKey(type.Name))
				return AddInDockState.Unknown;

			return _dockStates[type.Name];
		}

		public void ClearStates()
		{
			_dockStates.Clear();
		}
	}

	public static class PersistedDockStateCfg
	{
		private static PersistedDockStateData _current = new PersistedDockStateData();
		public static PersistedDockStateData Current 
		{
			get { return _current; }
			set { _current = value; }
		}

		private static string _defaultCfgFile = String.Empty;
		static PersistedDockStateCfg()
		{
			_defaultCfgFile = HostServicesSingleton.HostServices.HostOptions.UserAppDataFolder;
			_defaultCfgFile += "DockStates.cfg";
		}

		public static void Load()
		{
			if (!File.Exists(_defaultCfgFile))
			{
				_current = new PersistedDockStateData();
			}
			else
			{
				_current = ObjectXMLSerializer<PersistedDockStateData>.Load(_defaultCfgFile);
				if (_current == null)
				{
					_current = new PersistedDockStateData();
				}
			}
		}

		public static void Save()
		{
			ObjectXMLSerializer<PersistedDockStateData>.Save(_current, _defaultCfgFile);
		}
	}

	public class PersistedDockStatService : IPersistedDockStateService
	{
		private object _padLock = new object();

		public void AddState(Type type, AddInDockState state)
		{
			lock (_padLock)
			{
				PersistedDockStateCfg.Current.AddState(type, state);
				PersistedDockStateCfg.Save();
			}
		}

		public AddInDockState GetState(Type type)
		{
			lock (_padLock)
			{
				return PersistedDockStateCfg.Current.GetState(type);
			}
		}
		
		public void RemoveState(Type type)
		{
			lock (_padLock)
			{
				PersistedDockStateCfg.Current.RemoveState(type);
				PersistedDockStateCfg.Save();
			}
		}
	}
}
