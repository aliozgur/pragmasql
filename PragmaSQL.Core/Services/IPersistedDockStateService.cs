using System;

namespace PragmaSQL.Core
{
	public interface IPersistedDockStateService
	{
		void AddState(Type type, AddInDockState state);
		AddInDockState GetState(Type type);
		void RemoveState(Type type);
	}
}
