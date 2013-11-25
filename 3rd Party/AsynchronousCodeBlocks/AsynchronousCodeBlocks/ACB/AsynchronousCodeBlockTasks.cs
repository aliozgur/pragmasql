using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

using Sonic.Net;
using Sonic.Net.ThreadPoolTaskFramework;

namespace AsynchronousCodeBlocks
{
    /// <summary>
    /// Delegate that will be executed after completion of the
    /// execution of the AsyncDelegateTask or WaitableAsyncDelegateTask
    /// </summary>
    public delegate void AsyncTaskCompleted();

    /// <summary>
    /// Task that contains the asynchronous code block, which
    /// will be executed when the Task object is executed on 
    /// the Managed IOCP ThreadPool
    /// </summary>
    public class AsyncDelegateTask : GenericTask
    {
        public AsyncDelegateTask(object obj)
            : base(null, obj)
        {
        }
        public override void Execute(Sonic.Net.ThreadPool tp)
        {
            AsyncDelegate ad = this.UserObject as AsyncDelegate;
            if (ad != null)
            {
                try
                {
                    ad();
                }
                catch (Exception ex)
                {
                    _exception = ex;
                }

                try
                {
                    if (_asyncTaskCompleted != null) _asyncTaskCompleted();
                }
                catch (Exception exInner)
                {
                    _executionCompletionDelegateException = exInner;
                }
            }
        }

        public AsyncTaskCompleted TaskCompleted
        {
            get
            {
                return _asyncTaskCompleted;
            }
            set
            {
                _asyncTaskCompleted = value;
            }
        }

        public Exception CodeException
        {
            get { return _exception; }
        }

        public Exception ExecutionCompletionDelegateException
        {
            get { return _executionCompletionDelegateException; }
        }

        private Exception _exception = null;
        private Exception _executionCompletionDelegateException = null;
        private AsyncTaskCompleted _asyncTaskCompleted = null;
    }

    /// <summary>
    /// Waitable Task that contains the asynchronous code block, 
    /// which will be executed when the waitable task object is 
    /// executed on the Managed IOCP ThreadPool
    /// </summary>
    public class WaitableAsyncDelegateTask : WaitableGenericTask
    {
        public WaitableAsyncDelegateTask(object obj)
            : base(null, obj)
        {
        }

        public override void Execute(Sonic.Net.ThreadPool tp)
        {
            AsyncDelegate ad = this.UserObject as AsyncDelegate;
            if (ad != null)
            {
                try
                {
                    ad();
                }
                catch (Exception ex)
                {
                    _exception = ex;
                }

                try
                {
                    if (_asyncTaskCompleted != null) _asyncTaskCompleted();
                }
                catch (Exception exInner)
                {
                    _executionCompletionDelegateException = exInner;
                }
            }
            this._ev.Set();
        }
        public AsyncTaskCompleted TaskCompleted
        {
            get
            {
                return _asyncTaskCompleted;
            }
            set
            {
                _asyncTaskCompleted = value;
            }
        }

        //public override void Done()
        //{
        //    if (_asyncTaskCompleted != null) _asyncTaskCompleted();
        //    base.Done();
        //}

        public Exception CodeException
        {
            get { return _exception; }
        }

        public Exception ExecutionCompletionDelegateException
        {
            get { return _executionCompletionDelegateException; }
        }

        private Exception _exception = null;
        private Exception _executionCompletionDelegateException = null;
        private AsyncTaskCompleted _asyncTaskCompleted = null;
    }

    /// <summary>
    /// Delegate representing the asynchronous code block
    /// </summary>
    public delegate void AsyncDelegate();
}
