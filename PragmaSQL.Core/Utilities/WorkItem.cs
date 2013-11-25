using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace PragmaSQL.Core
{

  /// <summary>
  /// The lower-level plumbing for the implementation of the IAsyncResult and the cancel semantics
  /// </summary>
  public abstract class CancellableWorkItem : IAsyncResult
  {

    internal AsyncCallback _callback = null;
    internal object _asyncState = null;
    internal bool _completedSynchronously = false;
    internal bool _complete = false;
    internal ManualResetEvent _waitHandle = null;

    internal Thread _currentThread = null;
    bool _cancelled = false;

    public abstract void DoWork( );

    #region IAsyncResult Members

    public object AsyncState
    {
      get { return _asyncState; }
    }

    public WaitHandle AsyncWaitHandle
    {
      get { return _waitHandle; }
    }

    public bool CompletedSynchronously
    {
      get { return _completedSynchronously; }
    }

    public bool IsCompleted
    {
      get { return _complete; }
    }

    #endregion

    public bool IsCancelled
    {
      get { return this._cancelled; }
    }


    public bool Cancel( )
    {
      if (this._complete)
      {
        return false;
      }

      if (this._currentThread != null)
      {
          //this._currentThread.Abort();
          this._cancelled = true;
        
      }

      return true;
    }


    internal void Complete( )
    {
      if (!this._complete)
      {
        this._complete = true;
        this._waitHandle.Set();
      }

      if (this._callback != null)
      {
        this._callback(this);
      }
    }
  }


  /// <summary>
  /// This is a job that we want to run but to be able to cancel at any given time
  /// </summary>
  public class StringWorkItem : CancellableWorkItem
  {

    string _stringToBuild = string.Empty;
    int _iterations;
    string _result = string.Empty;


    public StringWorkItem( string stringToBuild, int iterations, AsyncCallback callback, object state )
    {
      this._stringToBuild = stringToBuild;
      this._iterations = iterations;
      this._callback = callback;
      this._asyncState = state;
      this._waitHandle = new ManualResetEvent(this._completedSynchronously);
    }


    public override void DoWork( )
    {
      StringBuilder sb = new StringBuilder(this._iterations);
      for (int i = 0; i < this._iterations; i++)
      {
        sb.Append(this._stringToBuild);
        Thread.Sleep(500); // sleep for half a second
      }
      this._result = sb.ToString();
    }

    public string Result
    {
      get { return this._result; }
    }
  }


  /// <summary>
  /// This is a job that we want to run but to be able to cancel at any given time
  /// </summary>
  public class RegexWorkItem : CancellableWorkItem
  {

    string _source = string.Empty;
    string _pattern = string.Empty;
    RegexOptions _options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
    Match _result = Match.Empty;


    public RegexWorkItem( string source, string pattern, RegexOptions options, AsyncCallback callback, object state )
    {
      this._source = source;
      this._pattern = pattern;
      this._options = options;
      this._callback = callback;
      this._asyncState = state;
      this._waitHandle = new ManualResetEvent(this._completedSynchronously);
    }


    public override void DoWork( )
    {
      Regex re = new Regex(this._pattern, this._options);
      this._result = re.Match(this._source);
    }

    public Match Result
    {
      get { return this._result; }
    }
  }

}
