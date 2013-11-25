#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;
using System.Threading;

namespace Sonic.Net.ThreadPoolTaskFramework
{
	/// <summary>
	/// Class that provides a generic Task object to be used with ThreadPool
	/// </summary>
	public abstract class GenericTask : Task
	{
		#region Public Constructor(s)
        /// <summary>
        /// Creates a generic task object with an identification and user object
        /// </summary>
        /// <param name="id">Unique identity of this task object</param>
        /// <param name="obj">User object that can stored and retrieved from this task object</param>
        public GenericTask(object id, object obj)
		{
            _id = id;
            _object = obj;
		}
		#endregion

		#region Public Overrides of Task
		/// <summary>
		/// Get/Set the user object associated with this Task instance 
		/// </summary>
		public override object UserObject
		{
			get
			{
				return _object;
			}
			set
			{
				_object = value;
			}
		}
		#endregion

		#region Public Overrides of ITask
		public override void Done()
		{
			// No-Op;
		}
		#endregion
		
        #region Private Data Members
		private object _object;
		#endregion
	}
}
