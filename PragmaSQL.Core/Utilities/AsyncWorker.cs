using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace PragmaSQL.Core
{

  /// <summary>
  /// This class is a base class for running jobs asynchronously.  This class provides
  /// the semantics for invoking the work and handling job cancellations.
  /// </summary>
  /// <remarks>
  /// Classes deriving from this class should provide a Begin/End pair for running a job
  /// asynchronously.
  /// </remarks>
  public abstract class AsyncWorker
  {

    public static void ThreadPool_WaitCallback( object state )
    {

      CancellableWorkItem item = (CancellableWorkItem)state;

      item._currentThread = Thread.CurrentThread;

      try
      {
        item.DoWork();
      }
      catch (ThreadAbortException ex)
      {
        Console.WriteLine(ex.ExceptionState.ToString());
        Thread.ResetAbort();
      }
      finally
      {
        item._currentThread = null;
      }

      item.Complete();
    }
  }


  /// <summary>
  /// A class that provides the begin/end syntax for the asynchronous model
  /// </summary>
  public class AsyncStringWorker : AsyncWorker
  {

    private AsyncStringWorker( ) { }

    public static IAsyncResult StartWork( string stringToBuild, int iterations, AsyncCallback callback )
    {
      return StartWork(stringToBuild, iterations, callback, null);
    }

    public static IAsyncResult StartWork( string stringToBuild, int iterations, AsyncCallback callback, object state )
    {
      StringWorkItem result = new StringWorkItem(stringToBuild, iterations, callback, state);
      ThreadPool.QueueUserWorkItem(ThreadPool_WaitCallback, result);
      return result;
    }

    public static StringWorkItem EndWork( IAsyncResult result )
    {
      return (StringWorkItem)result;
    }
  }


  /// <summary>
  /// A class that provides the begin/end syntax for the asynchronous model
  /// </summary>
  public class AsyncRegexWorker : AsyncWorker
  {

    private AsyncRegexWorker( ) { }

    public static IAsyncResult StartWork( string source, string pattern, RegexOptions options, AsyncCallback callback )
    {
      return StartWork(source, pattern, options, callback, null);
    }

    public static IAsyncResult StartWork( string source, string pattern, RegexOptions options, AsyncCallback callback, object state )
    {
      RegexWorkItem result = new RegexWorkItem(source, pattern, options, callback, state);
      ThreadPool.QueueUserWorkItem(ThreadPool_WaitCallback, result);
      return result;
    }

    public static RegexWorkItem EndWork( IAsyncResult result )
    {
      return (RegexWorkItem)result;
    }
  }

}
