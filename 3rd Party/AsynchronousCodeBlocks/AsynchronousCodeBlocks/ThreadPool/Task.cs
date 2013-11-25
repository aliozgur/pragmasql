#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;

using Sonic.Net.DataStructures.LockFree;

namespace Sonic.Net.ThreadPoolTaskFramework
{
	/// <summary>
	/// Base class for all Task objects that can be processed by ThreadPool.
	/// </summary>
	public abstract class Task : ITask
	{
		#region Public Methods

		/// <summary>
		/// Unique identification of the task object
		/// </summary>
		public object Id
		{
			get
			{
				return _id;
			}
		}

		/// <summary>
		/// User object if any that is associated with this task object
		/// </summary>
		public abstract object UserObject
		{
			get;set;
		}

		#endregion

        #region Public Overrided of ITask

        public abstract void Execute(ThreadPool tp);
        public abstract void Done();

        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }

        #endregion

		#region Protected Data Members

		protected object _id;
		protected bool _active = true;

		#endregion
	}
}
