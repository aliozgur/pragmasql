/********************************************************************
  Class SqlVariablesWorkItem
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using System.Threading;

namespace PragmaSQL.Core
{
  public class SqlVariablesWorkItem : CancellableWorkItem
  {
    string _source = string.Empty;

    bool _namesOnly = false;
    public bool NamesOnly
    {
      get { return _namesOnly; }
    }

    private IDictionary<string, SqlVariable> _result = null;
    public IDictionary<string, SqlVariable> Result
    {
      get { return _result; }
    }

    public SqlVariablesWorkItem( string source, bool namesOnly, AsyncCallback callback, object state )
    {
      this._source = source;
      this._callback = callback;
      this._asyncState = state;
      this._namesOnly = namesOnly;
      this._waitHandle = new ManualResetEvent(this._completedSynchronously);
    }


    public override void DoWork( )
    {
      GetVariables();
    }

   


    private void GetVariables( )
    {
      if (!_namesOnly)
      {
        _result = GetVariables_WithDataTypes(_source);
      }
      else
      {
        _result = GetVariables_NameOnly(_source);
      }
    }

    private IDictionary<string, SqlVariable> GetVariables_NameOnly( string sqlText )
    {
      IDictionary<string, SqlVariable> result = new Dictionary<string, SqlVariable>();

      string expression = ResManager.GetRegularExpression("SqlAnalyze_VariableNameOnly");
      
      Match m = null;

      SqlVariable v = null;
      for (m = Regex.Match(sqlText, expression, RegexOptions.IgnoreCase); m.Success; m = m.NextMatch())
      {
        if (!result.ContainsKey(m.Value))
        {
          v = new SqlVariable();
          v.Name = m.Value;
          v.FullyQualifiedName = m.Value;
          result.Add(m.Value, v);
        }
      }
      return result;
    }

    private IDictionary<string, SqlVariable> GetVariables_WithDataTypes( string sqlText )
    {
      IDictionary<string, SqlVariable> result = new Dictionary<string, SqlVariable>();

      string expression = ResManager.GetRegularExpression("SqlAnalyze_VariableWithDataType");
      expression = SqlAnalyzeHelper.ReplaceDataTypesWithQualifiedOnes(expression);

      //Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      Match m;

      SqlVariable v = null;
      Group g = null;
      for (m = Regex.Match(sqlText, expression, RegexOptions.IgnoreCase); m.Success; m = m.NextMatch())
      {
        g = m.Groups["Name"];
        if (g == null)
        {
          continue;
        }

        string dName = String.Empty;

        if (result.ContainsKey(g.Value))
        {
          continue;
        }

        dName = SqlAnalyzeHelper.CleanIdentifier(g.Value);
        v = new SqlVariable();
        v.Name = dName;
        v.FullyQualifiedName = dName;
        g = m.Groups["DataType"];
        result.Add(dName, v);

        if (g != null)
        {
          v.DataType = SqlAnalyzeHelper.CleanIdentifier(g.Value);
          v.FullyQualifiedName += " " + v.DataType;
        }

      }
      return result;
    }
  }

  public class AsyncSqlVariablesWorker: AsyncWorker
  {
    private AsyncSqlVariablesWorker( ) { }

    public static IAsyncResult StartWork( string source, bool namesOnly, AsyncCallback callback )
    {
      return StartWork(source, namesOnly, callback, null);
    }

    public static IAsyncResult StartWork( string source, bool namesOnly, AsyncCallback callback, object state )
    {
      SqlVariablesWorkItem result = new SqlVariablesWorkItem(source, namesOnly, callback, state);
      ThreadPool.QueueUserWorkItem(ThreadPool_WaitCallback, result);
      return result;
    }

    public static SqlVariablesWorkItem EndWork( IAsyncResult result )
    {
      return (SqlVariablesWorkItem)result;
    }
  }
}
