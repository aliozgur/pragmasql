#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;
using System.Collections;
using System.Threading; 

namespace Sonic.Net
{
	/// <summary>
	/// IOCP Handle used by Threads to wait on IOCP events
	/// </summary>
	public class IOCPHandle<T> where T: class
	{
		#region Internal Constructors
		/// <summary>
		/// Internal constructor used by Managed IOCP to create an instance of 
		/// IOCP Handle. This is called when a new thread registers with a
		/// Managed IOCP instance
		/// </summary>
		/// <param name="mIOCP">Managed IOCP instance</param>
		/// <param name="owningThread">Thread that is trying to register with the
		/// Managed IOCP instance identified by 'mIOCP' param</param>
		internal IOCPHandle(ManagedIOCP<T> mIOCP,Thread owningThread)
		{
			_mIOCP = mIOCP;
			_owningThread = owningThread;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Thread calling this method will be suspended untill a new object
		/// is available in the Managed IOCP queue.If the current thread has
		/// unregistered with the ManagedIOCP instance which is held by this
		/// IOCPHandle instance, then an exception is thrown.
		/// </summary>
		/// <returns>Object from the ManagedIOCP queue</returns>
		public T Wait()
		{
			if (_mIOCP != null)
			{
				try
				{
					// Tell IOCP that this thread is going to wait mode
					// for the next queued object, if it is in active state earlier.
					//
					if (_active == 1)
					{
						_mIOCP.DecrementActiveThreads();
						_active = 0;
					}
					else if (_active == 2) 
					{
						_active = 0;
					}

#if (DYNAMIC_IOCP)
                    if (_mIOCP.ActiveThreads >= _mIOCP.ConcurrentThreads)
                    {
                        // We might be an additional IOCPHandle more than the required concurrent
                        // IOCPHandles. So suspend this IOCPHandle untill it is woken up again
                        // by dispatch to service more requests
                        //
                        _mIOCP.SuspendIOCPHandle(this);
                        _event.WaitOne();
                    }
#endif

                    // Do not come out untill...
                    // 1. IOCP instructs this thread about the availability of a queued object 
                    //		_and_
                    // 2. This thread was able to grab a valid queued object 
                    //		_and_
                    // 3. The number of active threads working on queued objects are less than 
                    //		the allowed concurrent threads for the Managed IOCP instance
                    //		to which this IOCPHandle belongs to 
                    //
                    if (_firstRun == true)
                    {
                        _mIOCP.QueueIOCPHandle(this);
                        _firstRun = false;
                    }
					T obj = null;
					do
					{
						if (_mIOCP.IsRunning == true)
						{
							if (_mIOCP.IncrementActiveThreads() == true)
							{
								bool qEmpty = false;
								obj = _mIOCP.GetNextObject(ref qEmpty);
								if(qEmpty == true)
								{
									_mIOCP.DecrementActiveThreads();
									_waitingOnIOCP = true;
                                    //_mIOCP.QueueIOCPHandle(this);
									_event.WaitOne();
									_waitingOnIOCP = false;
								}
								else
								{
									// We have a valid object from queue.
									// Come out of wait loop
									//
									break;
								}
							}
							else
							{
								_waitingOnIOCP = true;
                                //_mIOCP.QueueIOCPHandle(this);
								_event.WaitOne();
								_waitingOnIOCP = false;
							}
						}
						else
						{
							_waitingOnIOCP = true;
                            //_mIOCP.QueueIOCPHandle(this);
							_event.WaitOne();
							_waitingOnIOCP = false;
						}
					} while(true);
					// Tell IOCP that this thread is active 
					//
					_active = 1;
                    // This is required because ManagedIOCP has to check if we 
                    // are running when a dispatch arrive and if not, then it should
                    // wake-up another waiting IOCPHandle. If we donot queue ourself
                    // then ManagedIOCP will not find us and thus cannot maintain 
                    // max concurrency limit properly.
                    //
                    _mIOCP.QueueIOCPHandle(this);
					return obj;
				}
				catch(Exception)
				{
					if (_mIOCP == null)
						throw new ManagedIOCPException("Invalid ManagedIOCP instance." + 
							" Check whether you have unregistered the current thread from the" +
							" ManagedIOCP instance held by this IOCPHandle .or. ManagedIOCP" +
							" instance is closed.");
					else
						throw;
				}
			}
			else
			{
				throw new ManagedIOCPException("Invalid ManagedIOCP instance." + 
					" Check whether you have unregistered the current thread from the" +
					" ManagedIOCP instance held by this IOCPHandle.or. ManagedIOCP" +
					" instance is closed.");
			}
		}

		/// <summary>
		/// Thread calling this method will be suspended untill a new object
		/// is available in the Managed IOCP queue.If the current thread has
		/// unregistered with the ManagedIOCP instance which is held by this
		/// IOCPHandle instance, then an exception is thrown.
		/// If specified timeout expired before the current thread could grab 
		/// an object from the associated ManagedIOCP queue then the function
		/// returne to caller with null object and with timeOut parameter 
		/// value set to true.
		/// </summary>
		/// <param name="milliSec">Milliseconds to wait for object arrival</param>
		/// <param name="timeOut">Set to true if the Wait timed-out else false</param>
		/// <returns></returns>
		public T Wait(int milliSec, ref bool timeOut)
		{
			if (_mIOCP != null)
			{
				try
				{
					// Tell IOCP that this thread is going to wait mode
					// for the next queued object, if it is in active state earlier.
					//
					if (_active == 1)
					{
						_mIOCP.DecrementActiveThreads();
						_active = 0;
					}
                    else if (_active == 2)
                    {
                        _active = 0;
                    }

#if (DYNAMIC_IOCP)
                    if (_mIOCP.ActiveThreads >= _mIOCP.ConcurrentThreads)
                    {
                        // We might be an additional IOCPHandle more than the required concurrent
                        // IOCPHandles. So suspend this IOCPHandle untill it is woken up again
                        // by dispatch to service more requests
                        //
                        _mIOCP.SuspendIOCPHandle(this);
                        _event.WaitOne();
                    }
#endif
					// Do not come out untill...
					// 1. IOCP instructs this thread about the availability of a queued object 
					//		_and_
					// 2. This thread was able to grab a valid queued object 
					//		_and_
					// 3. The number of active threads working on queued objects are less than 
					//		the allowed concurrent threads for the Managed IOCP instance
					//		to which this IOCPHandle belongs to 
					//
					T obj = null;
					do
					{
						if (_mIOCP.IsRunning == true)
						{
							if (_mIOCP.IncrementActiveThreads() == true)
							{
								bool qEmpty = false;
								obj = _mIOCP.GetNextObject(ref qEmpty);
								if(qEmpty == true)
								{
									_mIOCP.DecrementActiveThreads();
									_waitingOnIOCP = true;
                                    //_mIOCP.QueueIOCPHandle(this);
									if (_event.WaitOne(milliSec,false) == false)
									{
										timeOut = true;
										_waitingOnIOCP = false;
										break;
									}
									else
									{
										_waitingOnIOCP = false;
									}
								}
								else
								{
									// We have a valid object from queue.
									// Come out of wait loop
									//
									break;
								}
							}
							else
							{
								_waitingOnIOCP = true;
                                //_mIOCP.QueueIOCPHandle(this);
								if (_event.WaitOne(milliSec,false) == false)
								{
									timeOut = true;
									_waitingOnIOCP = false;
									break;
								}
								else
								{
									_waitingOnIOCP = false;
								}
							}
						}
						else
						{
							_waitingOnIOCP = true;
                            //_mIOCP.QueueIOCPHandle(this);
							if (_event.WaitOne(milliSec,false) == false)
							{
								timeOut = true;
								_waitingOnIOCP = false;
								break;
							}
							else
							{
								_waitingOnIOCP = false;
							}
						}
					} while(true);
					// Tell IOCP that this thread is active 
					//
					_active = 1;
                    // This is required because ManagedIOCP has to check if we 
                    // are running when a dispatch arrive and if not, then it should
                    // wake-up another waiting IOCPHandle. If we donot queue ourself
                    // then ManagedIOCP will not find us and thus cannot maintain 
                    // max concurrency limit properly.
                    //
                    _mIOCP.QueueIOCPHandle(this);
					return obj;
				}
				catch(Exception e)
				{
					if (_mIOCP == null)
						throw new ManagedIOCPException("Invalid ManagedIOCP instance." + 
							" Check whether you have unregistered the current thread from the" +
							" ManagedIOCP instance held by this IOCPHandle .or. ManagedIOCP" +
							" instance is closed.",e);
					else
						throw;
				}
			}
			else
			{
				throw new ManagedIOCPException("Invalid ManagedIOCP instance." + 
									" Check whether you have unregistered the current thread from the" +
									" ManagedIOCP instance held by this IOCPHandle.or. ManagedIOCP" +
									" instance is closed.");
			}
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Used internally by Managed IOCP instance to notify the thread waiting on this
		/// IOCP Handle about availability of a queued object
		/// </summary>
		internal void SetEvent()
		{
			_event.Set();
		}

		/// <summary>
		/// Used internally by Managed IOCPinstance to close the AutoResetEvent handle
		/// help by this instance of IOCPHandle
		/// </summary>
		internal void Close()
		{
			_event.Close();
		}

		/// <summary>
		/// Called internally by the ManagedIOCP instance to which this IOCPHandle
		/// instance belongs to, when a thread related to this instance of the IOCPHandle
		/// unregisters with the ManagedIOCP instance related to this IOCPHandle instance 
		/// </summary>
		internal void InvalidateOwningManagedIOCP()
		{
			_mIOCP = null;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Retrieves the owning thread for this IOCP Handle instance. Used by 
		/// Managed IOCP in its default 'ChooseThread' policy
		/// </summary>
		public Thread OwningThread
		{
			get
			{
				return _owningThread;
			}
		}

		/// <summary>
		/// Identifies whether a Thread using this IOCP Handle is waiting for
		/// the availability of an object in the Managed IOCP queue. Used by 
		/// Managed IOCP in its default 'ChooseThread' policy
		/// </summary>
		public bool WaitingOnIOCP
		{
			get
			{
				return _waitingOnIOCP;
			}
		}
		
		/// <summary>
		/// Identifies whether a Thread using this IOCP Handle has went past the 'Wait'
		/// state. This means that the thread is actively operating on a queued object
		/// of a Managed IOCP instance
		/// </summary>
		public int Active
		{
			get
			{
				return _active;
			}
		}

		/// <summary>
		/// Identifies if the current instance of IOCPHandle is holding
		/// a valid Managed IOCP instance.
		/// </summary>
		public bool Valid
		{
			get
			{
				return (_mIOCP == null) ? false : true;
			}
		}
		#endregion

		#region Internal Data Members
		/// <summary>
		/// Indicates whether the IOCPHandle is actively processing a 
		/// request dispatched to it by ManagedIOCP
		/// </summary>
		internal int _active = 0;
		#endregion

		#region Private Data Members
		private AutoResetEvent _event = new AutoResetEvent(false);
		private ManagedIOCP<T> _mIOCP = null;
		private Thread _owningThread = null;
		private bool _waitingOnIOCP = false;
        private bool _firstRun = true;
		#endregion
	}

	/// <summary>
	/// Provides an all managed implementation of Native Microsoft Windows 
	/// IOCP feature. This class does not provide IOCP based on I/O handles.
	/// Rather it provides waitable object queuing features of Native IOCP using 
	/// all managed .Net threading and synchronization primitives. 
	/// 
	/// This class provides the following features...
	/// 
	/// 1. Multiple Managed IOCP instances per process
	/// 2. Registration of multiple threads per Managed IOCP instance
	/// 3. Dispatching System.Object types to a queue maintained by each 
	///		Managed IOCP instance
	/// 4. Waitable multi-thread safe retrieval of objects from the Managed IOCP
	///		instance queue by all the threads registered for that particular
	///		Managed IOCP instance
	///	5. Ability to restrict number of concurrent active threads processing the
	///		queued objects related to a particular Managed IOCP instance
    ///	6. Policy based replaceable/customizable approach for choosing a 
    ///		registered thread to process next available queued object
    ///	7. Pause the Managed IOCP processing. Internally pauses processing of queued objects
    ///		by registered threads. Also by default disallows enqueuing new objects.
    ///	8. Run the Managed IOCP instance. Internally re-starts the processing of 
    ///		queued objects by registered threads. Also allows enqueuing new objects
    ///	9. Modify the concurrent threads at runtime
    ///	10. Provides easy accesibility to Managed IOCP instance runtime properties like...
    ///		10.1. Number of active concurrent threads
    ///		10.2. Number of objects left in queue
    ///		10.3. Running status
    ///		10.4. Number of allowed concurrent threads
    ///	11. Safe and controlled closing of an Managed IOCP instance. Read the code 
    ///			comments/documentation on ManagedIOCP.Close() method
	/// </summary>
	public class ManagedIOCP<T> where T: class
	{
		#region Public Constructors
		/// <summary>
		/// Public constructor for ManagedIOCP class. Defaults to 1 
		/// concurrent thread that can actively process objects queued to this 
		/// Managed IOCP instance.
		/// 
		/// Note:
		/// -----
		/// We are using Monitor on the ManagedIOCP instance to sync access to 
		/// Register, UnRegister & Close methods. As these methods use enumerators
		/// that fail during multi-threaded operations
		/// </summary>
		public ManagedIOCP()
		{
			Init(1);
		}
		/// <summary>
		/// Public constructor for ManagedIOCP class
		/// </summary>
		/// <param name="concurrentThreads">
		/// Number of concurrent threads that can 
		/// actively process objects queued to this Managed IOCP instance
		/// </param>
		public ManagedIOCP(int concurrentThreads)
		{
			Init(concurrentThreads);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Registers the calling thread with this instance of ManagedIOCP
		/// </summary>
		/// <returns>
		/// IOCPHandle class instance that can be used by this thread
		/// to retrieve the next available object from this ManagedIOCP 
		/// instance queue.
		/// </returns>
		public IOCPHandle<T> Register()
		{
			IOCPHandle<T> hIOCP = null;
			Thread th = Thread.CurrentThread;
			try
			{
				Monitor.Enter(this);
				if (_regThreads.ContainsKey(th))
					hIOCP = _regThreads[th] as IOCPHandle<T>;
				else
				{
					hIOCP = new IOCPHandle<T>(this,th);
					_regThreads.Add(th,hIOCP);
					_regIOCPHandles.Add(hIOCP);
				}
			}
			catch(Exception)
			{
				throw;
			}
			finally
			{
				Monitor.Exit(this);
			}
			return hIOCP;
		}

		/// <summary>
		/// UnRegisters the calling thread with this instance of ManagedIOCP. After this call
		/// the thread will not be able to wait on its IOCPHandle for receiving any 
		/// objects queued to this instance of ManagedIOCP. The calling thread can re-register
		/// with this instance of ManagedIOCP and receive objects queued to this instance
		/// of ManagedIOCP
		/// </summary>
		public void UnRegister()
		{
			IOCPHandle<T> hIOCP = null;
			Thread th = Thread.CurrentThread;
			try
			{
				Monitor.Enter(this);
				if (_regThreads.ContainsKey(th))
				{
					hIOCP = _regThreads[th] as IOCPHandle<T>;
					_regThreads.Remove(th);
					_regIOCPHandles.Remove(hIOCP);
					hIOCP.InvalidateOwningManagedIOCP();
					hIOCP.SetEvent();

    				if (hIOCP.Active == 1)
						DecrementActiveThreads();
				}
			}
			catch(Exception)
			{
				throw;
			}
			finally
			{
				Monitor.Exit(this);
			}
		}

		public void Close()
		{
			try
			{
				Monitor.Enter(this);
				// Make sure no dispatch or wait activity can happen
				// by setting ManagedIOCP to Pause mode
				//
				_run = false;

				// Invalidate all IOCPHandles registered with this instance
				// of ManagedIOCP. Wake up any waiting threads
				//
				foreach(IOCPHandle<T> hIOCP in _regIOCPHandles)
				{
					hIOCP.InvalidateOwningManagedIOCP();
					hIOCP.SetEvent();
					hIOCP.Close();
				}

				// Clear all our data structures
				//
				_regThreads.Clear();
				_regIOCPHandles.Clear();
				_qObjects.Clear();

				// Initialize our key data members to their defaults
				//
				_activeThreads = 0;
				_run = true;
			}
			catch(Exception)
			{
				throw;
			}
			finally
			{
				Monitor.Exit(this);
			}
		}

		/// <summary>
		/// Enqueues a new object ono the queue maintained by this instance 
		/// of ManagedIOCP
		/// </summary>
		/// <param name="obj"></param>
		public void Dispatch(T obj)
		{
			int count = _regIOCPHandles.Count;
			if (count > 0)
			{
				// Enqueue the object only if we are running .OR. if we are paused
				// and enqueuing is enabled in pause mode
				//
				if ((_run == true) || (_canEnqueueOnPause == true))
				{
					_qObjects.Enqueue(obj);
				}
				else
				{
					throw new Exception("Cannot dispatch objects. Currently in pause mode");
				}
                WakeupNextThread();
			}
			else
			{
				throw new Exception("No threads available to service this dispatch request");
			}
		}

		/// <summary>
		/// Pauses the processing of queued objects of this ManagedIOCP instance 
		/// by the threads registered with this instance of ManagedIOCP
		/// </summary>
		public void Pause()
		{
			_run = false;
		}

		/// <summary>
		/// Start the processing of queued objects of this ManagedIOCP instance 
		/// by the threads registered with this instance of ManagedIOCP.
		/// This is the default mode for any newly created instance of ManagedIOCP
		/// </summary>
		public void Run()
		{
			_run = true;
            WakeupNextThread();
		}
		#endregion

		#region Internal Methods
		internal bool IncrementActiveThreads()
		{
			bool incremented = true;
			do
			{
				int curActThreads = _activeThreads;
				int newActThreads = curActThreads + 1;
				if (newActThreads <= _concurrentThreads)
				{
					// Break if we had successfully incremented the active threads
					//
					if (Interlocked.CompareExchange(ref _activeThreads,newActThreads,curActThreads) == curActThreads)
						break;
				}
				else
				{
					incremented = false;
					break;
				}
			} while(true);
			return incremented;
		}

		internal void DecrementActiveThreads()
		{
			if (_activeThreads > 0)
				Interlocked.Decrement(ref _activeThreads);
		}

		internal T GetNextObject(ref bool qEmpty)
		{
			T obj = null;
			if (_qObjects.Count > 0)
			{
				try
				{
					obj = _qObjects.Dequeue(ref qEmpty);
				}
				catch (Exception)
				{
					// Possible when multiple threads
					// are trying to Wait on a ManagedIOCP object
					//
					qEmpty = true;
				}
			}
			else
			{
				qEmpty = true;
			}
			return obj;			
		}

		internal void QueueIOCPHandle(IOCPHandle<T> hIOCP)
		{
			_qIOCPHandle.Enqueue(hIOCP);
		}

#if (DYNAMIC_IOCP)
        internal void SuspendIOCPHandle(IOCPHandle<T> hIOCP)
        {
            _qSuspendedIOCPHandle.Enqueue(hIOCP);
        }
#endif
		#endregion
		
		#region Private methods
		private void WakeupNextThread()
		{
			bool empty = false;
#if (DYNAMIC_IOCP)
            // First check if we should service this request from suspended 
            // IOCPHandle queue
            //
            if ((_activeThreads < _concurrentThreads) &&
                (_qIOCPHandle.Count >= _concurrentThreads))
            {
                IOCPHandle<T> hSuspendedIOCP = _qSuspendedIOCPHandle.Dequeue(ref empty);
                if ((empty == false) && (hSuspendedIOCP != null))
                {
                    hSuspendedIOCP.SetEvent();
                    return;
                }
            }
            empty = false;
#endif
			while (true)
			{
				IOCPHandle<T> hIOCP = _qIOCPHandle.Dequeue(ref empty);
    			if ((empty == false) && (hIOCP != null))
				{
					if (hIOCP.WaitingOnIOCP == true)
					{
						hIOCP.SetEvent();
						break;
					}
					else
					{
						if (hIOCP.OwningThread.ThreadState != ThreadState.Running)
						{
							// Set the active flag to 2 and decrement the active threads
							// so that other waiting threads can process requests
							//
							int activeTemp = hIOCP._active;
							int newActiveState = 2;
							if (Interlocked.CompareExchange(ref hIOCP._active, newActiveState, activeTemp) == activeTemp)
							{
								DecrementActiveThreads();
							}
						}
						// This is required because, Thread associated with hIOCP
						// may have got null out of ManagedIOCP queue, but still 
						// not yet reached the QueuIOCPHandle and Wait state. 
						// Now we had a dispatch and we enqueued the object and 
						// trying to wake up any waiting threads. If we ignore this
						// running thread, this may be the only thread for us and we
						// will never be able to service this dispatch untill another
						// dispatch comes in.
						//
						hIOCP.SetEvent();
						break;
					}
				}
				else
				{
					// Do we need to throw this exception ???
					//
					//throw new Exception("No threads avialable to handle the dispatch");
					break;
				}
			}
		}

		private void Init(int concurrentThreads)
		{
			_concurrentThreads = concurrentThreads;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Specifies to this ManagedIOCP instance that, it should not allow
		/// any registered threads to enqueue objects onto its queue while it is 
		/// in 'Pause' mode
		/// </summary>
		public bool CanEnqueueOnPause
		{
			get
			{
				return _canEnqueueOnPause;
			}
			set
			{
				_canEnqueueOnPause = value;
			}
		}

		/// <summary>
		/// Specifies if this instance of ManagedIOCP is currently allowing 
		/// its registered threads to process any queued objects
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return _run;
			}
		}

		/// <summary>
		/// Number of threads that are concurrently processing the objects queued
		/// onto this instance of ManagedIOCP
		/// </summary>
		public int ActiveThreads
		{
			get
			{
				return _activeThreads;
			}
		}

		/// <summary>
		/// Max number of concurrent threads allowed to process objects queued onto this
		/// instance of ManagedIOCP
		/// </summary>
		public int ConcurrentThreads
		{
			get
			{
				return _concurrentThreads;
			}
			set
			{
				_concurrentThreads = value;
			}
		}

		/// <summary>
		/// Current count of objects queued onto this ManagedIOCP instance. 
		/// NOTE: This value may change very quickly as multiple concurrent threads might 
		/// be processing objects from this instance of ManagedIOCP queue. 
		/// So _do not_ depend on this value for logical operations. Use this only for
		/// monitoring purpose (Status reporting, etc) and during cleanup processes 
		/// (like not exiting main thread untill the queued object becomes 0, 
		/// i.e. no more objects to be processed, etc)
		/// </summary>
        public long QueuedObjectCount
        {
            get
            {
                return _qObjects.Count;
            }
        }

		/// <summary>
		/// Number of threads that registered with this instance of ManagedIOCP
		/// </summary>
        public int RegisteredThreads
        {
            get
            {
                return _regThreads.Count;
            }
        }
		#endregion

		#region Private Data Members	
		private ArrayList _regIOCPHandles = ArrayList.Synchronized(new ArrayList(5)); 
		private Hashtable _regThreads = Hashtable.Synchronized(new Hashtable(5));
        private Sonic.Net.DataStructures.LockFree.Queue<T> _qObjects = new Sonic.Net.DataStructures.LockFree.Queue<T>();
        private Sonic.Net.DataStructures.LockFree.Queue<IOCPHandle<T>> _qIOCPHandle = new Sonic.Net.DataStructures.LockFree.Queue<IOCPHandle<T>>();
#if (DYNAMIC_IOCP)
        private Sonic.Net.DataStructures.LockFree.Queue<T> _qSuspendedIOCPHandle = new Sonic.Net.DataStructures.LockFree.Queue<T>(100);
#endif

        private volatile int _activeThreads = 0;
		private int _concurrentThreads = 1;
		private bool _run = true;
		private bool _canEnqueueOnPause = false;
		#endregion
	}
}