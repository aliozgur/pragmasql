using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace PragmaSQL.Core
{
  /// <summary>
  /// Cache class
  /// </summary>
	public static class CodeCompletionProposalCache
  {
    #region Fields and properties
   
    private static readonly object _padlock = new object();

		private static IDictionary<CachedDataTableIdentifier, DataTable> _cache = new Dictionary<CachedDataTableIdentifier, DataTable>();
		
    private static IDictionary<CachedDataTableIdentifier, CacheDataTableTimeoutSpec> _updateHist = new Dictionary<CachedDataTableIdentifier, CacheDataTableTimeoutSpec>();

    private static CodeCompletionProposalCacheCleanup _cleanup = null;
    
    private static int _collectInterval = 30 * 60 * 1000;
    public static int CollectInterval
    {
      get { return _collectInterval;}
      set 
      {
        _collectInterval = value;
        if (_cleanup != null)
        {
          _cleanup.Dispose();
          _cleanup = null;
        } 
        
        if (_collectInterval > 0)
        {
          _cleanup = new CodeCompletionProposalCacheCleanup(_collectInterval);
        }
      }
    }

    #endregion //Fields and properties

    #region Static CTOR
    
    static CodeCompletionProposalCache()
    {
      
    }

    #endregion //Static CTOR


    public static void StoreData(SqlConnection conn, CachedDataTableIdentifier id)
    {
      lock (_padlock)
      {
        if (id.DataType == CachedDataType.None)
          throw new Exception("CachedDataType is None!");

        if (_cache.ContainsKey(id))
        {
          DataTable rm = _cache[id];
          _cache.Remove(id);
          if (rm != null)
          {
            rm.Clear();
            rm.Dispose();
          }
        }

        if (_updateHist.ContainsKey(id))
          _updateHist.Remove(id);

        string script = String.Empty;
        switch (id.DataType)
        {
          case CachedDataType.Databases:
            script = ResManager.GetDBScript("Script_GetDatabases");
            break;
          case CachedDataType.Users:
            script = ResManager.GetDBScript("Script_GetUsers");
            break;
          case CachedDataType.Objects:
            script = ResManager.GetDBScript("Script_CodeCompletionProposalWithoutProcParams");
            break;
          default:
            break;
        }

        SqlCommand cmd = null;
        SqlDataAdapter adapter = null;
        DataTable tbl = null;

        try
        {
          cmd = new SqlCommand(script, conn);
          cmd.CommandTimeout = 0;
          adapter = new SqlDataAdapter();
          adapter.SelectCommand = cmd;
          tbl = new DataTable();
          adapter.Fill(tbl);
        }
        finally
        {
          if (cmd != null)
            cmd.Dispose();
          if (adapter != null)
            adapter.Dispose();
        }

        CachedDataTableIdentifier key = new CachedDataTableIdentifier();
        key.CopyFrom(id);
        if (key.Timeout > 0)
        {
          CacheDataTableTimeoutSpec tmSpec = new CacheDataTableTimeoutSpec();
          tmSpec.Timeout = key.Timeout;
          tmSpec.RetrievedOn = DateTime.Now;
          _updateHist.Add(key, tmSpec);
        }
        _cache.Add(key, tbl);
      }
    }

    public static DataTable GetData(SqlConnection conn, CachedDataTableIdentifier id)
    {
      lock (_padlock)
      {

        bool isOutdated = false;
        if (_updateHist.ContainsKey(id))
        {
          CacheDataTableTimeoutSpec tmSpec = _updateHist[id];
          TimeSpan ts = DateTime.Now.Subtract(tmSpec.RetrievedOn);

          isOutdated = ts.TotalSeconds > tmSpec.Timeout;
        }

        if (!_cache.ContainsKey(id) || isOutdated)
          StoreData(conn, id);

        DataTable result = _cache[id];
        return result != null ? result.Copy() : null;
      }
    }

    public static bool IsInCache(CachedDataTableIdentifier id)
    {
      return _cache.ContainsKey(id);
    }

    public static bool HasTimeout(CachedDataTableIdentifier id)
    {
      return _updateHist.ContainsKey(id);
    }

    public static bool HasExpired(CachedDataTableIdentifier id)
    {
      if (_updateHist.ContainsKey(id))
      {
        CacheDataTableTimeoutSpec tmSpec = _updateHist[id];
        TimeSpan ts = DateTime.Now.Subtract(tmSpec.RetrievedOn);
        return ts.Minutes > tmSpec.Timeout;
      }
      else
      {
        return false;
      }
    }

    public static void ClearCache()
    {
      lock (_padlock)
      {
        _cache.Clear();
        _updateHist.Clear();
      }
    }

    public static void StopCleanupThread()
    {

      if (_cleanup == null)
        return;

      _cleanup.Dispose();
    }

    public static void ClearExpiredDataTables()
    {
      lock (_padlock)
      {
        CacheDataTableTimeoutSpec tmSpec;
        IList<CachedDataTableIdentifier> removeList = new List<CachedDataTableIdentifier>();
        bool isOutdated = false; 
       
        foreach (CachedDataTableIdentifier id in _updateHist.Keys)
        {
          tmSpec = _updateHist[id];
          TimeSpan ts = DateTime.Now.Subtract(tmSpec.RetrievedOn);
          isOutdated = ts.TotalSeconds > tmSpec.Timeout;
          if (isOutdated)
          {
            removeList.Add(id);
          }
        }

        foreach (CachedDataTableIdentifier id in removeList)
        {
          if (_cache.ContainsKey(id))
          {
            DataTable rm = _cache[id];
            _cache.Remove(id);
            if (rm != null)
            {
              rm.Clear();
              rm.Dispose();
            }
          }

          if (_updateHist.ContainsKey(id))
            _updateHist.Remove(id);
        }
      }
    }

    public static void ApplyTimeoutChange(int newTimeout)
    {
      lock (_padlock)
      {
        CacheDataTableTimeoutSpec tmSpec;
        IList<CachedDataTableIdentifier> removeList = new List<CachedDataTableIdentifier>();
        IList<CachedDataTableIdentifier> updateList = new List<CachedDataTableIdentifier>();
        foreach (CachedDataTableIdentifier id in _updateHist.Keys)
        {
          if (newTimeout <= 0)
          {
            removeList.Add(id);
          }
          else
          {
            tmSpec = _updateHist[id];
            TimeSpan ts = DateTime.Now.Subtract(tmSpec.RetrievedOn);
            if (tmSpec.Timeout - ts.TotalSeconds > newTimeout)
            {
              removeList.Add(id);
              updateList.Add(id);
            }
          }
        }

        foreach (CachedDataTableIdentifier id in removeList)
        {
          if (_updateHist.ContainsKey(id))
            _updateHist.Remove(id);

          if (updateList.Contains(id))
          {
            CachedDataTableIdentifier newId = new CachedDataTableIdentifier();
            newId.CopyFrom(id);
            newId.Timeout = newTimeout;
            tmSpec = new CacheDataTableTimeoutSpec();
            tmSpec.RetrievedOn = DateTime.Now;
            tmSpec.Timeout = newTimeout;
            _updateHist.Add(newId, tmSpec);
          }
        }
      }
    }
  }

  /// <summary>
  /// Cleanup thread wrapper
  /// </summary>
  internal class CodeCompletionProposalCacheCleanup:IDisposable
  {
    #region Fields and Properties
    
    private bool _isDisposing = false;
    private static Thread _cleanupThread = null;
    private int _sleepInterval = 30 * 60 * 1000;
    internal int SleepInterval
    {
      get 
      { 
        lock(_padlock)
        {
          return _sleepInterval;
        } 
      }
      set 
      {
        lock (_padlock)
        {
          _sleepInterval = value;
        }
      }
    }
    private static readonly object _padlock = new object();
    
    #endregion //Fields and Properties

    #region CTOR
    
    public CodeCompletionProposalCacheCleanup(int sleepInterval)
    {
      _sleepInterval = sleepInterval;
      _cleanupThread = new Thread(new ThreadStart(DoWork));
      _cleanupThread.Start();
    }
    
    #endregion //CTOR

    #region IDisposable Members

    public void Dispose()
    {
      if (_isDisposing)
        return;

      _isDisposing = true;
      _cleanupThread.Abort();
      while (_cleanupThread.ThreadState != ThreadState.Stopped && _cleanupThread.ThreadState != ThreadState.Aborted)
      {
        Thread.Sleep(100);  
      }
    }

    #endregion

    private void DoWork()
    {
      TimeSpan ts = new TimeSpan(0, 0, SleepInterval);
      while (true == true)
      {
        Thread.Sleep(ts);
        ts = new TimeSpan(0, 0, SleepInterval);
        CodeCompletionProposalCache.ClearExpiredDataTables();
      }
    }
  }

  /// <summary>
  /// Timeout specification structure
  /// </summary>
	public struct CacheDataTableTimeoutSpec
	{
		public int Timeout;
		public DateTime RetrievedOn;
	}

  /// <summary>
  /// Cached data table identifier
  /// </summary>
	public struct CachedDataTableIdentifier
	{
		public string Server;
		public string Database;
		public CachedDataType DataType;
		public string Name;
		public int Timeout;

		public CachedDataTableIdentifier(string server, string database, string name, CachedDataType dataType)
		{
			Server = server;
			Database = database;
			DataType = dataType;
			Name = name;
			Timeout = 0;
		}

		public void CopyFrom(CachedDataTableIdentifier source)
		{
			Server = source.Server;
			Database = source.Database;
			DataType = source.DataType;
			Name = source.Name;
			Timeout = source.Timeout;
		}

		public override string ToString()
		{
			return Server.ToLowerInvariant() + ";" + Database.ToLowerInvariant() + ";" + DataType.ToString() + ";" + Name.ToLowerInvariant();
		}

		public static bool operator ==(CachedDataTableIdentifier x, CachedDataTableIdentifier y)
		{
			return x.ToString() == y.ToString();
		}

		public static bool operator !=(CachedDataTableIdentifier x, CachedDataTableIdentifier y)
		{
			return x.ToString() != y.ToString();
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return this.ToString().Equals(obj.ToString());
		}
	}

  /// <summary>
  /// Type of the data cached
  /// </summary>
	public enum CachedDataType
	{
		None,
		Databases,
		Users,
		Objects
	}
}
