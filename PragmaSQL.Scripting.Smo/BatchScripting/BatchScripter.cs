
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

using Microsoft.SqlServer.Management.Smo;
using SFC = Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using Microsoft.SqlServer.Management.Common;

using PragmaSQL.Core;


namespace PragmaSQL.Scripting.Smo
{
	

	public class BatchScripter : IDisposable
	{
		#region Nested Classes
		public class ScriptingArgs
		{
			public IList<DbObjectList.DbObjectInfo> Objects;
			public ScriptingOptions Options;
			public string BatchSeparator = String.Empty;
		}
		#endregion //Nested Classes

		#region Fields and Properties

		private bool _cancelRequested = false;
    public bool CancelRequested
    {
      get { return _cancelRequested; }
    }

    private bool _pauseScripting = false;

		private Server srvr = null;
		private Scripter scrp = null;
		private Database db = null;
		private ServerConnection sqlConn = null;

		private ConnectionParams _connParams = null;
		public ConnectionParams ConnParams
		{
			get { return _connParams; }
			private set
			{
				if (value != null)
				{
					_connParams = value.CreateCopy();
				}
				else
				{
					_connParams = value;
				}
			}
		}

		private ProgressReportEventHandler _walkingDependencies = null;
		public event ProgressReportEventHandler WalkingDependencies
		{
			add { _walkingDependencies += value; }
			remove { _walkingDependencies -= value; }
		}

		private ProgressReportEventHandler _scriptingInProgress = null;
		public event ProgressReportEventHandler ScriptingInProgress
		{
			add { _scriptingInProgress += value; }
			remove { _scriptingInProgress -= value; }
		}

		private TaskProgressInfoDelegate _taskProgressInfo = null;
		public event TaskProgressInfoDelegate TaskProgressInfo
		{
			add { _taskProgressInfo += value; }
			remove { _taskProgressInfo -= value; }
		}

    private StringBuilder _recentErrors = null;
    public string RecentErrors
    {
      get { return _recentErrors == null ? String.Empty : _recentErrors.ToString(); }
    }


    #endregion //Fields and Properties

	  #region Constructor
		public BatchScripter(ConnectionParams cp)
    {
			if (cp == null)
				throw new ArgumentNullException("cp", "Connection parameters object is null!");
			
			ConnParams = cp;
      using (SqlConnection conn = _connParams.CreateSqlConnection(false,false))
      {
        sqlConn = new ServerConnection(conn);
        srvr = new Server(sqlConn);

        db = srvr.Databases[_connParams.Database];
        scrp = new Scripter(srvr);
				scrp.ScriptingProgress += new ProgressReportEventHandler(scrp_ScriptingProgress);
			}			
		}

    #endregion //Constructor

		#region Script with dependencies

		private SFC.Urn[] PrepareObjectUrns(IList<DbObjectList.DbObjectInfo> objects)
		{
			SFC.Urn[] urns = new SFC.Urn[objects.Count];
			DbObjectList.DbObjectInfo objInfo = null;
			string urnTemplate = "Server[@Name='{0}']/Database[@Name='{1}']/{2}[@Name='{3}' and @Schema='{4}']";///Schema[@Name='{4}']";

      for (int i = 0; i < objects.Count; i++)
			{
				
				objInfo = objects[i];
        urns[i] = new SFC.Urn(String.Format(urnTemplate, sqlConn.TrueName, _connParams.Database, objInfo.ObjType, objInfo.Name, objInfo.Owner));
			}
			return urns;
		}

		public string ScriptObjects(ScriptingArgs args)
		{
			if (args == null || args.Objects == null || args.Objects.Count == 0)
				return String.Empty;

      FireTaskProgressInfo("Preparing for scripting...");

      scrp.Options = args.Options;
			scrp.Options.ToFileOnly = false;
			scrp.Options.AppendToFile = false;
			scrp.Options.FileName = String.Empty;
			scrp.Server.ConnectionContext.BatchSeparator = "GO";
			StringCollection scr = null;
      _recentErrors = null;

      if (scrp.Options.ContinueScriptingOnError)
      {
        _recentErrors = new StringBuilder();
        scrp.ScriptingError += new ScriptingErrorEventHandler(scrp_ScriptingError);
      }

      try
      {
        if (scrp.Options.WithDependencies)
        {
					scrp.DiscoveryProgress += new ProgressReportEventHandler(dWalk_DiscoveryProgress);
					FireTaskProgressInfo("Discovering dependencies. (Task 1 of 3)");
					DependencyTree dTree = scrp.DiscoverDependencies(PrepareObjectUrns(args.Objects), DependencyType.Parents);
					FireTaskProgressInfo("Walking dependencies. (Task 2 of 3)");
					DependencyCollection dCol = scrp.WalkDependencies(dTree);
					FireTaskProgressInfo("Scripting objects. (Task 3 of 3)");
					scr = scrp.ScriptWithList(dCol);
				}
        else
        {
          FireTaskProgressInfo("Scripting objects. (Task 1 of 1)");
          scr = scrp.ScriptWithList(PrepareObjectUrns(args.Objects));
        }
      }
      catch (Exception ex)
      {
        if (_cancelRequested)
          return String.Empty;
        throw ex;
      }

			string myComment = "/***** Script generated with PragmaSQL Scripter on " + DateTime.Now.ToString() + " ******/";
			string[] result = new string[scr.Count];
			scr.CopyTo(result, 0);

			return myComment + "\r\n" + String.Join((String.IsNullOrEmpty(args.BatchSeparator) ? "\r\n" : "\r\n" + args.BatchSeparator + "\r\n"), result);
		}

		private void dWalk_DiscoveryProgress(object sender, ProgressReportEventArgs e)
		{
      while (_pauseScripting)
      {
        Thread.Sleep(10);
      }

      if (_cancelRequested)
        throw new CancelledByUserException();
      
      if (_walkingDependencies != null)
				_walkingDependencies(this, e);
		}

		private void scrp_ScriptingProgress(object sender, ProgressReportEventArgs e)
		{
      while (_pauseScripting)
      {
        Thread.Sleep(10);
      }

      if (_cancelRequested)
        throw new CancelledByUserException();
      
      if (_scriptingInProgress != null)
				_scriptingInProgress(this, e);

		}

    private void scrp_ScriptingError(object sender, ScriptingErrorEventArgs e)
    {

      string error = "- Error: " + e.InnerException.Message;
      error = error + ( e.InnerException.InnerException != null ? "." + e.InnerException.InnerException.Message : String.Empty) ;

      _recentErrors.AppendLine(error);
    }

		private void FireTaskProgressInfo(string info)
		{
			if (_taskProgressInfo != null)
				_taskProgressInfo(this, info);
		}

    public void Cancel()
    {
      _cancelRequested = true;
    }

    public void PauseScripting()
    {
      if (_pauseScripting)
        return;

      _pauseScripting = true;
    }

    public void ResumeScripting()
    {
      if (!_pauseScripting)
        return;

      _pauseScripting = false;
    }

		#endregion //Script with dependencies

		#region IDisposable Members

		private bool _isDisposing = false;
		public void Dispose()
		{
			if (_isDisposing)
				return;


      _isDisposing = true;

      if (sqlConn == null)
				return;

			if (sqlConn.InUse)
				sqlConn.Cancel();

			sqlConn.Disconnect();
			sqlConn = null;
		

		}
		
		#endregion //IDisposable Members

	}

	public delegate void TaskProgressInfoDelegate(object sender, string info);
  
  public class CancelledByUserException : Exception
  {
  }
}
