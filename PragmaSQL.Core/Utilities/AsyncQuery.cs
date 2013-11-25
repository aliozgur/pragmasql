using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PragmaSQL.Core
{
  public class ExecutionCompletedEventArgs
  {
    public DataSet Result = null;
    public Exception Error = null;
    public bool Cancelled = false;
  }

  class QueryArgs
  {
    public string ConnectionString = String.Empty;
    public string CommandText = String.Empty;
  }


  public class AsyncQuery
  {
    private BackgroundWorker _bw = new BackgroundWorker();
    private SqlCommand _cmd = null;

    private string _connectionString = String.Empty;
    public string ConnectionString
    {
      get { return _connectionString; }
      set { _connectionString = value; }
    }

    private string _commandText = String.Empty;
    public string CommandText
    {
      get { return _commandText; }
      set { _commandText = value; }
    }

    private bool _isExecuting = false;
    public bool IsExecuting
    {
      get { return _isExecuting; }
    }


    private ExecutionCompletedDelegate _afterExecutionCompleted;
    public event ExecutionCompletedDelegate AfterExecutionCompleted
    {
      add
      {
        _afterExecutionCompleted += value;
      }
      remove
      {
        _afterExecutionCompleted -= value;
      }
    }

    private List<SqlParameter> _params = new List<SqlParameter>();
    public List<SqlParameter> Params
    {
      get { return _params; }
    }


    public AsyncQuery()
    {
      WireUpBackgroundWorkerEvents();
    }

    public AsyncQuery(string connectionString, string commandText)
      : this()
    {
      _connectionString = connectionString;
      _commandText = commandText;
    }

    private void WireUpBackgroundWorkerEvents()
    {
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(DoExecuteQuery);
      _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnQueryExecutionCompleted);
    }

    private void DoExecuteQuery(object sender, DoWorkEventArgs e)
    {
      // Do not access the form's BackgroundWorker reference directly.
      // Instead, use the reference provided by the sender parameter.
      BackgroundWorker bw = sender as BackgroundWorker;

      // Extract the argument.
      QueryArgs arg = (QueryArgs)e.Argument;


      // Start the time-consuming operation.
      e.Result = ExecuteScriptWithDataAdapter(bw, arg);

      // If the operation was canceled by the user, 
      // set the DoWorkEventArgs.Cancel property to true.
      if (bw.CancellationPending)
      {
        e.Cancel = true;
      }
    }

    private void OnQueryExecutionCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      _isExecuting = false;
      if (_afterExecutionCompleted != null)
      {
        ExecutionCompletedEventArgs args = new ExecutionCompletedEventArgs();
        args.Cancelled = e.Cancelled;
        args.Error = e.Error;

        if (!args.Cancelled && e.Error == null)
        {
          args.Result = e.Result as DataSet;
        }
        _afterExecutionCompleted(args);
      }
    }

    private DataSet ExecuteScriptWithDataAdapter(BackgroundWorker bw, QueryArgs args)
    {
      _isExecuting = true;
      DataSet result = new DataSet();
      SqlDataAdapter adapter = new SqlDataAdapter();
      _cmd = new SqlCommand();
      try
      {
        using (SqlConnection conn = new SqlConnection(args.ConnectionString))
        {
          conn.Open();
          _cmd.Connection = conn;
          _cmd.CommandTimeout = 0;
          _cmd.CommandText = args.CommandText;

          adapter.SelectCommand = _cmd;
          try
          {
            BindParams(_cmd);
            adapter.Fill(result);
          }
          catch(SqlException sqlex)
          {
            if (!bw.CancellationPending)
            {
              throw sqlex;
            }
          }
        }
      }
      finally
      {
        if (_cmd != null)
        {
          _cmd.Dispose();
          _cmd = null;
        }
        if (adapter != null)
        {
          adapter.Dispose();
          adapter = null;
        }
      }
      return result;
    }

    public void Execute()
    {
      if (_bw.CancellationPending)
      {
        throw new InvalidAsynchronousStateException("CancellationPending");
      }

      QueryArgs args = new QueryArgs();
      args.ConnectionString = _connectionString;
      args.CommandText = _commandText;

      _bw.RunWorkerAsync(args);
    }

    public void Cancel()
    {
      if (_bw.CancellationPending)
      {
        throw new InvalidAsynchronousStateException("CancellationPending");
      }
      if (!_isExecuting)
      {
        return;
      }

      _bw.CancelAsync();
      if (_cmd != null)
      {
        _cmd.Cancel();
      }
    }

    private void BindParams(SqlCommand cmd)
    {
      foreach (SqlParameter param in _params)
      {
        cmd.Parameters.Add(param);
      }
    }

    public void AddParam(string paramName, SqlDbType type, object value)
    {
      SqlParameter param = new SqlParameter(paramName, type);
      _params.Add(param);
      if (value != null)
      {
        param.Value = value;
      }
      else
      {
        param.Value = DBNull.Value;
      }
    }
  }

  public delegate void ExecutionCompletedDelegate(ExecutionCompletedEventArgs args);
}
