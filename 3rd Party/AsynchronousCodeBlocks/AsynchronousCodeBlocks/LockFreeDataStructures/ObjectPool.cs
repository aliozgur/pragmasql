#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;
using System.Threading;
using System.Reflection;
using System.IO;

namespace Sonic.Net.DataStructures.LockFree
{
	/// <summary>
	/// Defines a factory interface to be implemented by classes
	/// that creates new poolable objects
	/// </summary>
	public abstract class PoolableObjectFactory
	{
		#region Public Abstract Methods
		/// <summary>
		/// Create a new instance of a poolable object
		/// </summary>
		/// <returns>Instance of user defined PoolableObject derived type</returns>
		public abstract PoolableObject CreatePoolableObject();
		#endregion
	}
	
	/// <summary>
	/// Poolable object type. One can define new poolable types by deriving
	/// from this class.
	/// </summary>
	public class PoolableObject
	{
		#region Public Constructors
		/// <summary>
		/// Default constructor. Poolable types need to have a no-argument 
		/// constructor for the poolable object factory to easily create 
		/// new poolable objects when required.
		/// </summary>
		public PoolableObject()
		{
			Initialize();
		}
		#endregion

		#region Public Virtual Methods
		/// <summary>
		/// Called when a poolable object is being returned from the pool
		/// to caller.
		/// </summary>
		public virtual void Initialize()
		{
			LinkedObject = null;
		}
		/// <summary>
		/// Called when a poolable object is being returned back to the pool.
		/// </summary>
		public virtual void UnInitialize()
		{
			LinkedObject = null;
            if (++_val == long.MaxValue) _val = 0;
		}
		#endregion

		#region Internal Data Members
#if NET_11
        internal object LinkedObject;
#else
		internal volatile PoolableObject LinkedObject;
#endif
        internal long _val = 0;
		#endregion
	}

	/// <summary>
	/// Lock Free ObjectPool
	/// </summary>
	public class ObjectPool
	{
		#region Public Constructors
		/// <summary>
		/// Creates a new instance of ObjectPool
		/// </summary>
		/// <param name="pof">
		/// Factory class to be used by ObjectPool to
		/// create new poolable object instance when required.
		/// </param>
		/// <param name="bCreateObjects">
		/// Flag identifying whether this ObjectPool instance
		/// is allowed to create new objects when the pool is empty
		/// and the user requested a new object from the pool.
		/// </param>
		public ObjectPool(PoolableObjectFactory pof,bool bCreateObjects)
		{
			_pof = pof;
			_bCreateObjects = bCreateObjects;
			Init(0);
		}
		
		/// <summary>
		/// Creates a new instance of ObjectPool with n-number of pre-created
		/// objects in the pool.
		/// </summary>
		/// <param name="pof">
		/// Factory class to be used by ObjectPool to
		/// create new poolable object instance when required.
		/// </param>
		/// <param name="bCreateObjects">
		/// Flag identifying whether this ObjectPool instance
		/// is allowed to create new objects when the pool is empty
		/// and the user requested a new object from the pool.
		/// </param>
		/// <param name="objectCount">Numberof objects to pre-create</param>
		public ObjectPool(PoolableObjectFactory pof,bool bCreateObjects,int objectCount)
		{
			_pof = pof;
			_bCreateObjects = bCreateObjects;
			Init(objectCount);
		}
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Add the poolable object to the object pool. The object is uninitialized
		/// before adding it to the pool.
		/// </summary>
		/// <param name="newNode">PoolableObject instance</param>
		public void AddToPool(PoolableObject newNode)
		{
            if (newNode == null)
            {
                return;
            }
            newNode.UnInitialize();

            PoolableObject tempTail = null;
            PoolableObject tempTailNext = null;
            
			do
            {
#if NET_11
                tempTail = _tail as PoolableObject;
                tempTailNext = tempTail.LinkedObject as PoolableObject;
#else
                tempTail = _tail;
                tempTailNext = tempTail.LinkedObject;
#endif
                if (tempTail == _tail)
                {
                    if (tempTailNext == null)
                    {
                        // If the tail node we are referring to is really the last
                        // node in the queue (i.e. its next node is null), then
                        // try to point its next node to our new node
                        //
                        if (Interlocked.CompareExchange(ref tempTail.LinkedObject, newNode, tempTailNext) == tempTailNext)
                            break;
                    }
                    else
                    {
                        // This condition occurs when we have failed to update
                        // the tail's next node. And the next time we try to update
                        // the next node, the next node is pointing to a new node
                        // updated by other thread. But the other thread has not yet
                        // re-pointed the tail to its new node.
                        // So we try to re-point to the tail node to the next node of the
                        // current tail
                        //
                        Interlocked.CompareExchange(ref _tail, tempTailNext, tempTail);
                    }
                }
            } while (true);

            // If we were able to successfully change the next node of the current tail node
            // to point to our new node, then re-point the tail node also to our new node
            //
            Interlocked.CompareExchange(ref _tail, newNode, tempTail);
            Interlocked.Increment(ref _count);
		}
		
		/// <summary>
		/// Returns an existing object from the pool or creates a 
		/// new object if the pool is empty. If an existing object is being
		/// returned it is initialized before returned to the caller.
		/// </summary>
		/// <returns>PoolableObject instance</returns>
		public PoolableObject GetObject()
		{
			bool empty = false;
            PoolableObject result = null;
			PoolableObject tempTail = null;
			PoolableObject tempHead = null;
			PoolableObject tempHeadNext = null;
            
			do
            {
#if NET_11
                long val = (_head as PoolableObject)._val;
                tempHead = _head as PoolableObject;
                tempTail = _tail as PoolableObject;
                tempHeadNext = tempHead.LinkedObject as PoolableObject;
#else
                long val = _head._val;
                tempHead = _head;
                tempTail = _tail;
                tempHeadNext = tempHead.LinkedObject;
#endif
                if (tempHead == _head)
                {
                    // There may not be any elements in the queue
                    //
                    if (tempHead == tempTail)
                    {
                        if (tempHeadNext == null)
                        {
                            // If the queue is really empty come out of dequeue operation
                            //
                            empty = true;
                            break;
                        }
                        else
                        {
                            // Some other thread could be in the middle of the
                            // enqueue operation. it could have changed the next node of the tail
                            // to point to the new node.
                            // So let us advance the tail node to point to the next node of the
                            // current tail
                            Interlocked.CompareExchange(ref _tail,tempHeadNext,tempTail);
                        }
                    }
                    else
                    {
                        // Move head one element down. 
                        // If succeeded Try to get the data from head and
                        // break out of the loop.
                        //
                        // *** Note ***
                        // We should actually compare with !=_head. But it is not working.
                        // So we are comparing it with tempHead. Check this logic later.
                        //
                        if (Interlocked.CompareExchange(ref _head, tempHeadNext, tempHead) == tempHead)
                        {
                            if (tempHead._val == val)
                            {
                                break;
                            }
                            else
                            {
                                // Else we had hit A.B.A problem, so go back and try to get the
                                // head again
                                //
                                //StreamWriter sw = new StreamWriter(File.Open(@"c:\Aditya\ABATrace\ABA-Pool-" + DateTime.Now.Ticks.ToString(), FileMode.CreateNew));
                                //sw.WriteLine("Orig Val: {0}, New Val: {1}", val, tempHead._val);
                                //sw.Close();
                            }
                        }
                    }
                }
            } while (true);
            
			if (empty == false)
            {
                Interlocked.Decrement(ref _count);
                tempHead.Initialize();
                result = tempHead;
            }
            else
            {
                if (_bCreateObjects == true)
                    result = _pof.CreatePoolableObject();
            }
            return result;
        }
		
		/// <summary>
		/// Removes all the poolable objects from the pool.
		/// </summary>
		public void Clear()
		{
			Clear(0);
		}
		
		/// <summary>
		/// Removes all the poolable objects from the pool. And fills the pool 
		/// with n-number of pre-created objects
		/// </summary>
		public void Clear(int objectCount)
		{
			_count = 0;
			Init(objectCount);
		}
		
		/// <summary>
		/// Count of poolable objects in the pool
		/// </summary>
		public long Count
		{
			get
			{
				return _count;
			}
		}
		#endregion

		#region Private Methods
		private void Init(int objectCount)
		{
			_head = _tail = _pof.CreatePoolableObject();
			if (objectCount > 0)
			{
				for(int count=1; count<=objectCount; count++)
				{
					AddToPool(_pof.CreatePoolableObject());
				}
			}
		}
		#endregion

		#region Private Data Members
#if NET_11
		private object _head;
		private object _tail;
#else
		private volatile PoolableObject _head;
		private volatile PoolableObject _tail;
#endif
		private long _count = 0;
		private bool _bCreateObjects;
		private PoolableObjectFactory _pof;
		#endregion
	}
}
