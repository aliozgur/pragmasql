/********************************************************************
  Class HostServicesSingleton
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using System.Threading;
using System.Windows.Forms;

namespace PragmaSQL.Core
{

  public static class HostServicesSingleton
  {
    private static IHostServices _hostServices = null;
    public static IHostServices HostServices
    {
      get { return _hostServices; }
      set 
      { 
        _hostServices = value;
      }
    }

    public static IHostServices Svc
    {
      get { return _hostServices; }
    }

    static HostServicesSingleton( ){ }

    #region Safe Thread Caller
    /// <summary>
    /// Description of STAThreadCaller.
    /// </summary>
    private class STAThreadCaller
    {
      Control ctl;

      public STAThreadCaller( Control ctl )
      {
        this.ctl = ctl;
      }

      public object Call( Delegate method, object[] arguments )
      {
        if (method == null)
        {
          throw new ArgumentNullException("method");
        }
        return ctl.Invoke(method, arguments);
      }

      public void BeginCall( Delegate method, object[] arguments )
      {
        if (method == null)
        {
          throw new ArgumentNullException("method");
        }
        ctl.BeginInvoke(method, arguments);
      }
    } // Class 

    public static bool InvokeRequired
    {
      get
      {
        return _hostServices.Wb.InvokeRequired;
      }
    }

    /// <summary>
    /// Makes a call GUI threadsafe. WARNING: This method waits for the result of the
    /// operation, which can result in a dead-lock when the main thread waits for a lock
    /// held by this thread!
    /// </summary>
    public static R SafeThreadFunction<R>( Func<R> method )
    {

      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      return (R)caller.Call(method, new object[0]);
    }

    /// <summary>
    /// Makes a call GUI threadsafe. WARNING: This method waits for the result of the
    /// operation, which can result in a dead-lock when the main thread waits for a lock
    /// held by this thread!
    /// </summary>
    public static R SafeThreadFunction<A, R>( Func<A, R> method, A arg1 )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      return (R)caller.Call(method, new object[] { arg1 });
    }

    /// <summary>
    /// Makes a call GUI threadsafe. WARNING: This method waits for the result of the
    /// operation, which can result in a dead-lock when the main thread waits for a lock
    /// held by this thread!
    /// </summary>
    public static void SafeThreadCall( ActionF method )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.Call(method, new object[0]);
    }

    /// <summary>
    /// Makes a call GUI threadsafe. WARNING: This method waits for the result of the
    /// operation, which can result in a dead-lock when the main thread waits for a lock
    /// held by this thread!
    /// </summary>
    public static void SafeThreadCall<A>( Action<A> method, A arg1 )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.Call(method, new object[] { arg1 });
    }

    /// <summary>
    /// Makes a call GUI threadsafe. WARNING: This method waits for the result of the
    /// operation, which can result in a dead-lock when the main thread waits for a lock
    /// held by this thread!
    /// </summary>
    public static void SafeThreadCall<A, B>( ActionF<A, B> method, A arg1, B arg2 )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.Call(method, new object[] { arg1, arg2 });
    }

    /// <summary>
    /// Makes a call GUI threadsafe. WARNING: This method waits for the result of the
    /// operation, which can result in a dead-lock when the main thread waits for a lock
    /// held by this thread!
    /// </summary>
    public static void SafeThreadCall<A, B, C>( ActionF<A, B, C> method, A arg1, B arg2, C arg3 )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.Call(method, new object[] { arg1, arg2, arg3 });
    }

    /// <summary>
    /// Makes a call GUI threadsafe without waiting for the returned value.
    /// </summary>
    public static void SafeThreadAsyncCall( ActionF method )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.BeginCall(method, new object[0]);
    }

    /// <summary>
    /// Makes a call GUI threadsafe without waiting for the returned value.
    /// </summary>
    public static void SafeThreadAsyncCall<A>( Action<A> method, A arg1 )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.BeginCall(method, new object[] { arg1 });
    }

    /// <summary>
    /// Makes a call GUI threadsafe without waiting for the returned value.
    /// </summary>
    public static void SafeThreadAsyncCall<A, B>( ActionF<A, B> method, A arg1, B arg2 )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.BeginCall(method, new object[] { arg1, arg2 });
    }

    /// <summary>
    /// Makes a call GUI threadsafe without waiting for the returned value.
    /// </summary>
    public static void SafeThreadAsyncCall<A, B, C>( ActionF<A, B, C> method, A arg1, B arg2, C arg3 )
    {
      STAThreadCaller caller = new STAThreadCaller(_hostServices.Wb as Form);
      caller.BeginCall(method, new object[] { arg1, arg2, arg3 });
    }
    #endregion

  }
}
