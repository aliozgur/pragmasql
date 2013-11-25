using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;

using Sonic.Net;
using Sonic.Net.ThreadPoolTaskFramework;

namespace AsynchronousCodeBlocks
{
    /// <summary>
    /// Delegate that will be executed after completion of execution
    /// of the asynchronous code blocks
    /// </summary>
    /// <param name="objAsync"></param>
    public delegate void AsyncCodeBlockExecutionCompleteCallback(Async objAsync);

    /// <summary>
    /// Class for wrapping part of method code into an object
    /// that will be executed on Managed IOCP based ThreadPool.
    /// By default async code blocks uses AppDomain wide Managed
    /// IOCP based ThreadPool. If required developers can specify
    /// a different instance of Managed IOCP ThreadPool
    /// </summary>
    public class Async
    {
        #region Static Constructor
        /*
         * Used for deserializing async code blocks
         * 
        static async()
        {
            AssemblyName asmName = new AssemblyName("ACBAssembly");
            _asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                asmName, AssemblyBuilderAccess.Run);
            _modBuilder = _asmBuilder.DefineDynamicModule("ACBModule", true);
        }
         * */
        #endregion

        #region Public Constructor(s)
        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        public Async(AsyncDelegate ad)
        {
            Initialize(ad,null,null,null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
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
        public Async(AsyncDelegate ad, AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, null, null, executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        public Async(AsyncDelegate ad, Sonic.Net.ThreadPool tp)
        {
            Initialize(ad, tp, null, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
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
        public Async(AsyncDelegate ad, Sonic.Net.ThreadPool tp, 
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, null,executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        public Async(AsyncDelegate ad, Async dependentOnAsync)
        {
            Initialize(ad, null, new Async[] { dependentOnAsync }, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
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
        public Async(AsyncDelegate ad, Async dependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, null, new Async[] { dependentOnAsync }, executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        public Async(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async dependentOnAsync)
        {
            Initialize(ad, tp, new Async[] { dependentOnAsync }, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
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
        public Async(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async dependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, new Async[] { dependentOnAsync }, executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object array on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object array has completed execution
        /// </param>
        public Async(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async[] arrDependentOnAsync)
        {
            Initialize(ad, tp, arrDependentOnAsync, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object array on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object array has completed execution
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public Async(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async[] arrDependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, arrDependentOnAsync, executionCompleteCallback);
        }
        #endregion

        #region Private Constructors
        /*
         * Used for deserialization of async code blocks
         * 
        public async(SerializableCodeBlock scb)
        {
            lock (_asyncTypeTable)
            {
                if (_asyncTypeTable.ContainsKey(scb.TypeName) == false)
                {
                    // Fill in our async object with the data from SCB
                    //
                    // Create the type
                    //
                    TypeBuilder typBuilder = _modBuilder.DefineType(scb.TypeName, TypeAttributes.Class | TypeAttributes.Public);
                    if (scb.InstanceID != 0)
                    {
                        foreach (FieldValue fv in scb.FieldValueList)
                        {
                            // Add fields to the type
                            //
                            typBuilder.DefineField(fv.Name, fv.Value.GetType(), FieldAttributes.Public);
                        }
                    }
                    // Add the method to our type
                    //
                    MethodBuilder methBuilder = typBuilder.DefineMethod(scb.MethodName,
                        (scb.InstanceID == 0) ? MethodAttributes.Static | MethodAttributes.Public :
                        MethodAttributes.Public);
                    ILGenerator ilg = methBuilder.GetILGenerator();
                    ilg.EmitWriteLine(scb.MethodName);
                    // Add the type to our type table
                    //
                    Type asyncType = typBuilder.CreateType();
                    _asyncTypeTable[scb.TypeName] = asyncType;
                    // Add the async method to our type method table
                    //
                    MethodInfo asyncMethInfo = asyncType.GetMethod(scb.MethodName);
                    _asyncTypeMethodTable[asyncType] = asyncMethInfo;
                    // Add the IL to our method
                    //
                    // Get the token for our method
                    //
                    MethodToken methToken = methBuilder.GetToken();
                    // Get the pointer to the method body.
                    GCHandle hmem = GCHandle.Alloc((Object)scb.MethodIL, GCHandleType.Pinned);
                    IntPtr addr = hmem.AddrOfPinnedObject();
                    int cbSize = scb.MethodIL.Length;
                    // Swap the old method body with the new body.
                    MethodRental.SwapMethodBody(
                                    asyncType,
                                    methToken.Token,
                                    addr,
                                    cbSize,
                                    MethodRental.JitOnDemand);
                    AsyncDelegate asyncDlg = Delegate.CreateDelegate(
                        typeof(AsyncDelegate), asyncMethInfo) as 
                        AsyncDelegate;
                    asyncDlg();
                }                
            }
        }
         * */
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the value of any local variable used within
        /// the code wrapped by this async object
        /// </summary>
        /// <param name="name">Name of the local variable</param>
        /// <returns>Value of the local variable</returns>
        public object GetObject(string name)
        {
            object obj = _targetType.InvokeMember(name, BindingFlags.GetField, null, _targetObject, null);
            return obj;
        }

        /// <summary>
        /// Executes the given AsyncDelegate in the SynchronizationContext associated with this
        /// instance of async class
        /// </summary>
        /// <param name="ad">AsyncDelegate object</param>
        public void ExecuteInSychronizationContext(AsyncDelegate ad)
        {
            if (_synchronizationContext != null)
            {
                _synchronizationContext.Send(
                    Delegate.CreateDelegate(typeof(SendOrPostCallback), ad.Method) as SendOrPostCallback, 
                    null);
            }
            else
            {
                throw new InvalidOperationException(
                    "SynchronizationContext object is not available to execute the supplied code");
            }
        }
        #endregion

        #region Public Virtual Methods
        /// <summary>
        /// Calling code cannot wait for code execution completion
        /// that is wrapped by async objects
        /// </summary>
        /// <param name="msWaitTime"></param>
        /// <returns></returns>
        public virtual bool Wait(int msWaitTime)
        {
            return false;
        }

        /// <summary>
        /// Returns the Exception object if the execution of the code 
        /// wrapped by this async object threw any exception
        /// </summary>
        public virtual Exception CodeException
        {
            get
            {
                Exception ex = null;
                AsyncDelegateTask adt = _task as AsyncDelegateTask;
                if (adt != null)
                {
                    if (adt.CodeException != null)
                        ex = adt.CodeException;
                }
                return ex;
            }
        }
        #endregion

        #region Internal Methods
        internal bool AddToDependencyCodeBlockList(Async asyncObj)
        {
            bool added = false;
            lock (_dependentCodeBlockList)
            {
                if (_executionCompleted == false)
                {
                    _dependentCodeBlockList.Add(asyncObj);
                    added = true;
                }
            }
            return added;
        }

        internal void MarkCompleted()
        {
            // Execute the execution completion callback
            //
            if (_executionCompleteCallback != null) _executionCompleteCallback(this);

            // Schedule execution of dependent async objects
            //
            lock (_dependentCodeBlockList)
            {
                _executionCompleted = true;
                // Dispatch all dependent async code blocks for
                // execution
                //
                if (_dependentCodeBlockList.Count > 0)
                {
                    foreach (Async asyncObj in _dependentCodeBlockList)
                    {
                        asyncObj.ExecuteSelf();
                    }
                    // Release our references to the dependent async objects
                    //
                    _dependentCodeBlockList.Clear();
                }
            }
        }
        #endregion

        #region Protected Virtual Methods
        protected virtual void Initialize(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async[] arrDependentOnAsync,
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, arrDependentOnAsync, executionCompleteCallback, false);
        }
        #endregion

        #region Protected Methods
        protected void Initialize(AsyncDelegate ad, Sonic.Net.ThreadPool tp, Async[] arrDependentOnAsync, 
            AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback, bool waitable)
        {
            if (ad != null)
            {
                _waitable = waitable;
                _ad = ad;
                _tp = tp;
                _targetObject = ad.Target;
                _targetType = ad.Method.DeclaringType;
                if (_waitable == false)
                {
                    AsyncDelegateTask adt = new AsyncDelegateTask(ad);
                    adt.TaskCompleted = this.MarkCompleted;
                    _task = adt;
                }
                else
                {
                    WaitableAsyncDelegateTask wadt = new WaitableAsyncDelegateTask(ad);
                    wadt.TaskCompleted = this.MarkCompleted;
                    _task = wadt;
                }
                _executionCompleteCallback = executionCompleteCallback;
                bool dispatchForExecution = true;
                if (arrDependentOnAsync != null)
                {
                    lock (_syncObject)
                    {
                        foreach (Async asyncObj in arrDependentOnAsync)
                        {
                            if (asyncObj.AddToDependencyCodeBlockList(this) == true) _dependentCount++;
                        }
                        if (_dependentCount == 0) 
                            dispatchForExecution = true;
                        else
                            dispatchForExecution = false;
                    }
                }
                // Store the current SynchronizationContext
                //
                _synchronizationContext = SynchronizationContext.Current;
                if (dispatchForExecution == true)
                {
                    Execute(ad, _task, tp);
                }
            }
        }
        #endregion

        #region Private Methods
        private void ExecuteSelf()
        {
            lock (_syncObject)
            {
                if ((_dependentCount > 0) && (--_dependentCount == 0))
                {
                    Execute(_ad, _task, _tp);
                }
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Identifies whether the calling code wait on this
        /// async object until the code wrapped by it executed.
        /// true: Can wait, false: Cannot wait.
        /// For async objects this property always returns 'false'
        /// </summary>
        public bool Waitable
        {
            get { return _waitable; }
        }

        /// <summary>
        /// Gets the SynchronizationContext associated with this instance of the
        /// async class
        /// </summary>
        public SynchronizationContext SynchronizationContext
        {
            get { return _synchronizationContext; }
        }
        #endregion

        #region Public Static Constructor
        static Async()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);
        }
        #endregion

        #region Private Static Methods
        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Initializes the AppDomain wide ManagedIOCP ThreadPool
        /// used by async object execution
        /// </summary>
        public static void Open()
        {
            lock (typeof(Async))
            {
                if (s_AsyncCodeDelegateTP == null)
                {
                    s_AsyncCodeDelegateTP = new Sonic.Net.ThreadPool(1, 1);
                }
            }
        }

        /// <summary>
        /// Closes the AppDomain wide ManagedIOCP ThreadPool
        /// used by async object execution
        /// </summary>
        public static void Close()
        {
            lock (typeof(Async))
            {
                if (s_AsyncCodeDelegateTP != null)
                {
                    s_AsyncCodeDelegateTP.Close();
                    s_AsyncCodeDelegateTP = null;
                }
            }
        }

        public static Sonic.Net.ThreadPool DefaultThreadPool
        {
            get
            {
                return s_AsyncCodeDelegateTP;
            }
        }
        #endregion

        #region Public Static Conversions
        /*
         * More research is required in the area of serializing and deserializing
         * async code blocks. Here is the status...
         * 
         * Pending creation of MSIL for the anonymous method body.
         * MethodBody.GetILAsByteArray() is only giving the raw IL Opcodes.
         * Opcodes for code size, maxstacksize and local variable initialization
         * are missing. Also even if I was able to serialize and deserialize the
         * IL of the method body somehow, I need to figure out a way to add any 
         * references to the assemblies used by method body, while executing the
         * deserialized code block
         * 
        public static explicit operator SerializableCodeBlock(async objAsync)
        {
            // Create a SerializableCodeBlock object from async object data
            //
            SerializableCodeBlock scb = new SerializableCodeBlock();
            if (objAsync._targetType != null)
                scb.TypeName = objAsync._targetType.FullName;
            else
                scb.TypeName = "T" + Guid.NewGuid().ToString().Replace("-", "");
            if (objAsync._targetObject != null)
            {
                scb.InstanceID = objAsync._targetObject.GetHashCode();
                FieldInfo[] arrFI = objAsync._targetType.GetFields();
                FieldValue[] arrFV = new FieldValue[arrFI.Length];
                int i = 0;
                foreach (FieldInfo fi in arrFI)
                {
                    object val = objAsync._targetType.InvokeMember(fi.Name, BindingFlags.GetField, null, objAsync._targetObject, null);
                    arrFV[i] = new FieldValue();
                    arrFV[i].Name = fi.Name;
                    arrFV[i].Value = val;
                    i++;
                }
                scb.FieldValueList = arrFV;
            }
            scb.MethodName = objAsync._ad.Method.Name;
            if (objAsync._tp != null) scb.ThreadPoolID = objAsync._tp.GetHashCode().ToString();
            MethodBody mb = objAsync._ad.Method.GetMethodBody();
            byte[] il = mb.GetILAsByteArray();
            List<byte> codeSizeList = new List<byte>();
            codeSizeList.Add(0x03);
            codeSizeList.Add(0x30);
            codeSizeList.Add(0x0A);
            codeSizeList.Add(0x00);
            codeSizeList.Add((byte)il.Length);                // code size
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.AddRange(il);
            scb.MethodIL = codeSizeList.ToArray();
            return scb;
        }
         * */
        #endregion

        #region Private Static Methods
        private static void Execute(AsyncDelegate ad, ITask task, Sonic.Net.ThreadPool tp)
        {
            if (s_AsyncCodeDelegateTP != null)
            {
                if (tp == null)
                {
                    s_AsyncCodeDelegateTP.Dispatch(task);
                }
                else
                {
                    tp.Dispatch(task);
                }
            }
            else
            {
                throw new ApplicationException("Thread Pool used by AsynchronousCodeBlock class is closed. " +
                    "Cannot execute any more asynchronous code blocks on default Thread pool. Please open the " +
                    "Thread Pool used by AsynchronousCodeBlock class or supply your own Thread Pool object for " +
                    "asynchronous code block");
            }
        }
        #endregion

        #region Protected Data Members
        protected ITask _task = null;
        protected bool _waitable = false;
        protected AsyncDelegate _ad = null;
        protected Sonic.Net.ThreadPool _tp = null;
        protected Type _targetType = null;
        protected object _targetObject = null;
        protected volatile bool _executionCompleted = false;
        protected List<Async> _dependentCodeBlockList = new List<Async>();
        protected object _syncObject = new object();
        protected volatile int _dependentCount = 0;
        protected AsyncCodeBlockExecutionCompleteCallback _executionCompleteCallback = null;
        protected SynchronizationContext _synchronizationContext = null;
        #endregion

        #region Private Static Data Members
        private static Sonic.Net.ThreadPool s_AsyncCodeDelegateTP = new Sonic.Net.ThreadPool(1, 1);
        /*
         * Used for serialization and deserialization of async code blocks
         * 
        private static AssemblyBuilder _asmBuilder = null;
        private static ModuleBuilder _modBuilder = null;
        private Dictionary<string, Sonic.Net.ThreadPool> _threadPoolTable = new Dictionary<string,ThreadPool>();
        private Dictionary<string, Type> _asyncTypeTable = new Dictionary<string,Type>();
        private Dictionary<Type, MethodInfo> _asyncTypeMethodTable = new Dictionary<Type, MethodInfo>();
        private Dictionary<Type, Dictionary<string, object>> _asyncTypeObjectTable = new Dictionary<Type, Dictionary<string, object>>();
         * */
        #endregion
    }
}