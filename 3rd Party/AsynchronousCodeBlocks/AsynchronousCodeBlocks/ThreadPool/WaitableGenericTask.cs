#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;
using System.Threading;

namespace Sonic.Net.ThreadPoolTaskFramework
{
	/// <summary>
	/// Waitable Generic Task provides the features of Generic Task
    /// with the ability to wait on the task until it is executed by 
    /// Thread Pool
    /// </summary>
	public abstract class WaitableGenericTask : GenericTask
	{
        /// <summary>
        /// Type of wait to be used by WaitableGenericTask while waiting
        /// on the task to complete
        /// </summary>
        public enum WaitType
        {
            /// <summary>
            /// Close the AutoResetEvent used by task immediately after wait
            /// has returned
            /// </summary>
            CloseWaitEvent = 1,
            /// <summary>
            /// Close the AutoResetEvent used by task immediately after wait
            /// has returned only if the wait succeeds
            /// </summary>
            CloseWaitEventOnSuccessfulWait = 2,
            /// <summary>
            /// Do not close the AutoResetEvent used by task after wait returns
            /// </summary>
            ReuseWaitEvent = 3
        }

		#region Public Constructor
        /// <summary>
        /// Creates a waitable generic task object with an identification and user object
        /// </summary>
        /// <param name="id">Unique identity of this task object</param>
        /// <param name="obj">User object that can stored and retrieved from this task object</param>
		public WaitableGenericTask(object id, object obj)
            : base(id,obj)
		{
			// No-Op
		}
		#endregion

		#region Public Methods
        /// <summary>
        /// Wait till the current Geenric Task instance is executed by the
        /// Thread Pool. User can specify a time-out for the wait operation. 
        /// </summary>
        /// <param name="msWaitTime">Tme-Out for wait in milliseconds</param>
        /// <returns>True/False of the wait operation</returns>
        public bool Wait(int msWaitTime)
        {
            return this.Wait(msWaitTime, WaitType.CloseWaitEvent);
        }
		
        /// <summary>
		/// Wait till the current Geenric Task instance is executed by the
		/// Thread Pool. User can specify a time-out for the wait operation. 
		/// </summary>
		/// <param name="msWaitTime">Tme-Out for wait in milliseconds</param>
        /// <param name="waitType">Type of wait operation requested</param>
		/// <returns>True/False of the wait operation</returns>
		public bool Wait(int msWaitTime,WaitableGenericTask.WaitType waitType)
		{
            bool waitResult = _ev.WaitOne(msWaitTime, false);
            // We will enable this code if we face any
            // event resource leak problems. For now when the
            // task is GCed the internal event object is 
            // disposed as it implements IDisposable interface
            // (as per documentation)
            //
            /*
            switch (waitType)
            {
                case WaitType.CloseWaitEvent:
                    {
                        AutoResetEvent evTemp = _ev;
                        if (Interlocked.CompareExchange<AutoResetEvent>(ref _ev, null, evTemp) != evTemp)
                        {
                            evTemp.Close();
                            evTemp = null;
                        }
                    }
                    break;
                case WaitType.CloseWaitEventOnSuccessfulWait:
                    if (waitResult == true)
                    {
                        AutoResetEvent evTemp = _ev;
                        if (Interlocked.CompareExchange<AutoResetEvent>(ref _ev, null, evTemp) != evTemp)
                        {
                            evTemp.Close();
                            evTemp = null;
                        }
                    }
                    break;
            }
             * */
			return waitResult;
		}
		#endregion

        #region Private Properties
        public WaitHandle TaskWaitHandle
        {
            get
            {
                return _ev;
            }
        }
        #endregion

        #region Private Data Members
        protected AutoResetEvent _ev = new AutoResetEvent(false);
		#endregion
    }
}
