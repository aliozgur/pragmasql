#region Copyright
// <author>Adityanand Pasumarthi, 2005-2006</author>
#endregion

using System;

namespace Sonic.Net
{
	/// <summary>
	/// Base class for all ManagedIOCP exceptions
	/// </summary>
	[Serializable()]
	public class ManagedIOCPException : ApplicationException
	{
		public ManagedIOCPException()
		{
		}
		public ManagedIOCPException(string message) 
			: base(message)
		{
		}
		public ManagedIOCPException(string message,Exception innerException)
			: base(message,innerException)
		{
		}
	}
}
