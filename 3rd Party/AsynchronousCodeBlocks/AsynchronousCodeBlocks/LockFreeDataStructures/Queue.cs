#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;
using System.Threading;
using System.IO;

namespace Sonic.Net.DataStructures.LockFree
{
	/// <summary>
	/// Node class used by all other data structures
	/// </summary>
	public class Node<T> where T: class
	{
		#region Public Constructors
		/// <summary>
		/// Creates an instance of a Node class with 
		/// data element as null
		/// </summary>
		public Node()
		{
			Init(null);
		}

		/// <summary>
		/// Creates an instance of the Node class with
		/// data element as the passed in object
		/// </summary>
		/// <param name="data">Data of the Node instance</param>
		public Node(T data)
		{
			Init(data);
		}
		#endregion

		#region Private Methods
		private void Init(T data)
		{
			Data = data;
			NextNode = null;
		}
		#endregion

		#region Public Data Members
		public T Data;
		public Node<T> NextNode;
		#endregion
	}

	/// <summary>
	/// Lock Free Queue
	/// </summary>
	public class Queue<T> where T: class
	{
		#region Public Constructors
		/// <summary>
		/// Creates a new instance of Lock-Free Queue
		/// </summary>
		public Queue()
		{
			Init(0);
		}

		/// <summary>
		/// Creates a new instance of Lock-Free Queue with n-number of 
		/// pre-created nodes to hold objects queued on to this instance.
		/// </summary>
		/// <param name="nodeCount">Number of Nodes to pre-create</param>
		public Queue(int nodeCount)
		{
			Init(nodeCount);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Enqueue the given object onto this Queue instance
		/// </summary>
		/// <param name="data">Object to be enqueued</param>
		public void Enqueue(T data)
		{
			Node<T> tempTail = null;
			Node<T> tempTailNext = null;
            Node<T> newNode = new Node<T>(data);
			newNode.Data = data;
			do
			{
				tempTail = _tail;
				tempTailNext = tempTail.NextNode;
				if (tempTail == _tail)
				{
					if (tempTailNext == null)
					{
						// If the tail node we are referring to is really the last
						// node in the queue (i.e. its next node is null), then
						// try to point its next node to our new node
						//
						if (Interlocked.CompareExchange(ref tempTail.NextNode,newNode,tempTailNext) == tempTailNext)
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
						Interlocked.CompareExchange(ref _tail,tempTailNext,tempTail);
					}
				}
			} while (true);

			// If we were able to successfully change the next node of the current tail node
			// to point to our new node, then re-point the tail node also to our new node
			//
			Interlocked.CompareExchange(ref _tail,newNode,tempTail);
			Interlocked.Increment(ref _count);
		}
		
		/// <summary>
		/// Dequeue an object from the front of the queue
		/// </summary>
		/// <param name="empty">true if the queue is empty else false</param>
		/// <returns>
		/// null if the queue is empty else returns the first element at 
		/// the front of the queue
		/// </returns>
		public T Dequeue(ref bool empty)
		{
			T data = null;
			Node<T> tempTail = null;
			Node<T> tempHead = null;
			Node<T> tempHeadNext = null;
			do
			{
				tempHead = _head;
				tempTail = _tail;
				tempHeadNext = tempHead.NextNode;
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
							return null;
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
                        data = tempHeadNext.Data;
                        if (Interlocked.CompareExchange(ref _head, tempHeadNext, tempHead) == tempHead)
                            break;
					}
				}
			} while (true);
			Interlocked.Decrement(ref _count);
			tempHead.Data = null;
			return data;
		}
		
		/// <summary>
		/// Remove all the elements from the queue
		/// </summary>
		public void Clear()
		{
			Init(0);
		}

		/// <summary>
		/// Remove all the elements from the queue
		/// </summary>
		public void Clear(int nodeCount)
		{
			Init(nodeCount);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Count of elements in the queue
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
		/// <summary>
		/// Initialize the queue
		/// </summary>
		/// <param name="nodeCount">Number of nodes to pre-create</param>
		private void Init(int nodeCount)
		{
			_count = 0;
            _head = _tail = new Node<T>();
		}
		#endregion

		#region Private Data Members
		/// <summary>
		/// Head node of the queue
		/// </summary>
		private volatile Node<T> _head;
		/// <summary>
		/// Head node of the queue
		/// </summary>
		private volatile Node<T> _tail;
		/// <summary>
		/// Count of elements in the queue
		/// </summary>
		private long _count = 0;
		#endregion
	}
}
