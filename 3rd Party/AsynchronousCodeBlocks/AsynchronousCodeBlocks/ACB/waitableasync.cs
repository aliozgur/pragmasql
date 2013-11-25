using System;
using System.Collections.Generic;
using System.Threading;

using Sonic.Net;
using Sonic.Net.ThreadPoolTaskFramework;

namespace AsynchronousCodeBlocks
{
    /// <summary>
    /// async class variant, where calling code can wait until the 
    /// code wrapped by waitableasync object is executed
    /// </summary>
    public class waitableasync : Async
    {
        #region Public Constructor(s)
        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        public waitableasync(AsyncDelegate ad)
            : base(ad)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public waitableasync(AsyncDelegate ad,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
            : base(ad,executionCompleteCallback)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        public waitableasync(AsyncDelegate ad, Sonic.Net.ThreadPool tp)
            : base(ad, tp)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public waitableasync(AsyncDelegate ad, Sonic.Net.ThreadPool tp,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
            : base(ad, tp, executionCompleteCallback)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of waitableasync 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        public waitableasync(AsyncDelegate ad, Async dependentOnAsync)
            : base(ad, dependentOnAsync)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of waitableasync 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public waitableasync(AsyncDelegate ad, Async dependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
            : base(ad, dependentOnAsync, executionCompleteCallback)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide or 
        /// developer supplied Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of waitableasync 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        public waitableasync(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async dependentOnAsync)
            : base(ad,tp,dependentOnAsync)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide or 
        /// developer supplied Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of waitableasync 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public waitableasync(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async dependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
            : base(ad, tp, dependentOnAsync, executionCompleteCallback)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide or 
        /// developer supplied Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object array on which the current instance of waitableasync 
        /// depends on. The code wrapped by the current instance 
        /// of waitableasync object will be executed after the code wrapped 
        /// by dependentOnAsync object array has completed execution
        /// </param>
        public waitableasync(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async[] arrDependentOnAsync)
            : base(ad,tp,arrDependentOnAsync)
        {
        }

        /// <summary>
        /// Creates an instance of waitableasync class, that executes
        /// the wrapped code block on the default AppDomain wide or 
        /// developer supplied Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object array on which the current instance of waitableasync 
        /// depends on. The code wrapped by the current instance 
        /// of waitableasync object will be executed after the code wrapped 
        /// by dependentOnAsync object array has completed execution
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public waitableasync(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async[] arrDependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
            : base(ad, tp, arrDependentOnAsync, executionCompleteCallback)
        {
        }
        #endregion

        #region Public Overrides
        /// <summary>
        /// Calling code will wait until the code wrapped by this instance
        /// of the waitableasync object is executed or the specified time
        /// is elapsed.
        /// </summary>
        /// <param name="msWaitTime">
        /// Time in milliseconds to wait for the completion of 
        /// code execution
        /// </param>
        /// <returns>
        /// true, if the wait returned after the completion of execution 
        /// of the wrapped code. false, if the wait returned due to time
        /// out before completion of execution of code
        /// </returns>
        public override bool Wait(int msWaitTime)
        {
            bool waitDone = false;
            WaitableAsyncDelegateTask wadt = _task as WaitableAsyncDelegateTask;
            if (wadt != null)
            {
                waitDone = wadt.Wait(msWaitTime, WaitableGenericTask.WaitType.ReuseWaitEvent);
            }
            return waitDone;
        }

        /// <summary>
        /// Returns the Exception object if the execution of the code 
        /// wrapped by this waitableasync object threw any exception
        /// </summary>
        public override Exception CodeException
        {
            get
            {
                Exception ex = null;
                WaitableAsyncDelegateTask wadt = _task as WaitableAsyncDelegateTask;
                if (wadt != null)
                {
                    if (wadt.CodeException != null)
                        ex = wadt.CodeException;
                }
                return ex;
            }
        }
        #endregion

        #region Protected Overrides
        protected override void Initialize(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async[] arrDependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, arrDependentOnAsync, executionCompleteCallback, true);
        }
        #endregion


        #region Public Static Methods
        /// <summary>
        /// Wait until any one of the supplied waitablesync objects
        /// has completed or until the wait time specified in 
        /// milliSeconds is elapsed. if milliSeconds is -1, this method
        /// call waits indefinitely.
        /// </summary>
        /// <param name="arrWAsync">Array of waitableasync object</param>
        /// <param name="milliSeconds">Time-out for wait</param>
        /// <returns>
        /// True, if any of the supplied waitableasync objects has completed
        /// its execution, else False in all other cases
        /// </returns>
        public static bool WaitAny(waitableasync[] arrWAsync, int milliSeconds)
        {
            return WaitMany(arrWAsync, milliSeconds, false);
        }

        /// <summary>
        /// Wait until all of the supplied waitablesync objects
        /// has completed or until the wait time specified in 
        /// milliSeconds is elapsed. if milliSeconds is -1, this method
        /// call waits indefinitely.
        /// </summary>
        /// <remarks>
        /// WaitAll uses internal AutoResetEvent object of the code execution
        /// task posted to the Managed IOCP ThreadPool for execution. So it
        /// has a limit of being able to wait on only 64 waitableasync objects
        /// as WaitHandle can wait on only 64 handles using its own WaitAll
        /// method.
        /// </remarks>
        /// <param name="arrWAsync">Array of waitableasync object</param>
        /// <param name="milliSeconds">Time-out for wait</param>
        /// <returns>
        /// True, if all of the supplied waitableasync objects has completed
        /// its execution, else False in all other cases
        /// </returns>
        public static bool WaitAll(waitableasync[] arrWAsync, int milliSeconds)
        {
            return WaitMany(arrWAsync, milliSeconds, true);
        }

        /// <summary>
        /// Wait until all of the supplied waitablesync objects
        /// has completed or until the wait time specified in 
        /// milliSeconds is elapsed. if milliSeconds is -1, this method
        /// call waits indefinitely.
        /// </summary>
        /// <param name="arrWAsync">Array of waitableasync object</param>
        /// <param name="milliSeconds">Time-out for wait</param>
        /// <returns>
        /// True, if all of the supplied waitableasync objects has completed
        /// its execution, else False in all other cases
        /// </returns>
        public static bool WaitAllEx(waitableasync[] arrWAsync, int milliSeconds)
        {
            bool result = false;
            long st = DateTime.Now.Ticks;
            long length = arrWAsync.LongLength;
            for (long i = 0; i < length; i++)
            {
                result = WaitOne(arrWAsync[i], milliSeconds);
                long et = DateTime.Now.Ticks;
                bool timeout = false;
                if (milliSeconds >= 0)
                {
                    timeout = (((et - st) / 10000) >= milliSeconds);
                }                
                if (timeout == true)
                {
                    break;
                }
            }
            return result;
        }
        #endregion

        #region Private Static Methods
        private static bool WaitMany(waitableasync[] arrWAsync, int milliSeconds, bool waitAll)
        {
            bool result = false;
            List<WaitHandle> handleList = new List<WaitHandle>();
            foreach (waitableasync wasync in arrWAsync)
            {
                WaitableAsyncDelegateTask wadt = wasync._task as WaitableAsyncDelegateTask;
                if (wadt != null)
                {
                    WaitHandle wh = wadt.TaskWaitHandle;
                    if (handleList.Contains(wh) == false)
                    {
                        handleList.Add(wh);
                    }
                }
            }
            WaitHandle[] whList = handleList.ToArray();
            if (waitAll == true)
                result = WaitHandle.WaitAll(whList, milliSeconds, false);
            else
                result = (WaitHandle.WaitAny(whList, milliSeconds, false) != WaitHandle.WaitTimeout) ? true : false;
            return result;
        }

        private static bool WaitOne(waitableasync wasync, int milliSeconds)
        {
            bool result = false;
            WaitableAsyncDelegateTask wadt = wasync._task as WaitableAsyncDelegateTask;
            if (wadt != null)
            {
                WaitHandle wh = wadt.TaskWaitHandle;
                result = wh.WaitOne(milliSeconds, false);
            }
            return result;
        }
        #endregion
    }
}
