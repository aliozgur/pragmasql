/********************************************************************
  Class SqlTablesWorkItem
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
  public class SqlTableAliasesWorkItem : CancellableWorkItem
  {
    string _source = string.Empty;

    TableAliasType _aliasType = TableAliasType.From;
    public TableAliasType AliasType
    {
      get { return _aliasType; }
    }

    private IDictionary<string, SqlTableAlias> _result = null;
    public IDictionary<string, SqlTableAlias> Result
    {
      get { return _result; }
    }

    public SqlTableAliasesWorkItem( string source, TableAliasType aliasType, AsyncCallback callback, object state )
    {
      this._source = source;
      this._callback = callback;
      this._asyncState = state;
      this._aliasType = aliasType;
      this._waitHandle = new ManualResetEvent(this._completedSynchronously);
    }


    public override void DoWork( )
    {
      _result = GetTableAliases(_source, _aliasType);
    }

    public IDictionary<string, SqlTableAlias> GetTableAliases( string sqlText, TableAliasType aliasType )
    {
      IDictionary<string, SqlTableAlias> result = new Dictionary<string, SqlTableAlias>();
      string expression = String.Empty;
      switch (aliasType)
      {
        case TableAliasType.From:
          expression = ResManager.GetRegularExpression("SqlAnalyze_TableAliasFrom");
          break;
        case TableAliasType.Join:
          expression = ResManager.GetRegularExpression("SqlAnalyze_TableAliasJoin");
          break;
        default:
          return result;
      }

      Match m;

      SqlTableAlias ta = null;
      Group g = null;
      for (m = Regex.Match(sqlText,expression, RegexOptions.IgnoreCase); m.Success; m = m.NextMatch())
      {
        g = m.Groups["TableName"];
        if (g == null)
        {
          continue;
        }

        string tName = SqlAnalyzeHelper.CleanIdentifier(g.Value);
        if (tName.Contains("(") || tName.Contains(")"))
        {
          continue;
        }

        tName = SqlAnalyzeHelper.MatchTableName(tName);
        if (result.ContainsKey(tName))
        {
          continue;
        }

				if (ShallIgnore(m))
					continue;

        ta = new SqlTableAlias(aliasType);
        ta.TableName = tName;
        result.Add(ta.TableName, ta);

        g = m.Groups["TableAlias"];

			

        if (g != null)
        {
          if (SqlAnalyzeHelper.CleanIdentifier(g.Value).Trim() == "on")
          {
            ta.TableAlias = ta.TableName;
          }
          else
          {
            ta.TableAlias = SqlAnalyzeHelper.CleanIdentifier(g.Value);
          }
        }

      }
      return result;
    }

		private bool ShallIgnore(Match m)
		{
			Group g = m.Groups["Ignore"];
			if (g == null)
				return false;

			if (g.Value.Trim().ToLowerInvariant() == "order" || g.Value.Trim().ToLowerInvariant() == "group")
			{
				g = m.Groups["TableAlias"];
				if (g == null)
					return false;

				if (g.Value.Trim().ToLowerInvariant() == "by")
					return true;
			}
			else if (g.Value.Trim().ToLowerInvariant() == "left" || g.Value.Trim().ToLowerInvariant() == "right"
					|| g.Value.Trim().ToLowerInvariant() == "inner" || g.Value.Trim().ToLowerInvariant() == "outer"
				)
			{
				g = m.Groups["TableAlias"];
				if (g == null)
					return false;

				if (g.Value.Trim().ToLowerInvariant() == "join")
					return true;
			}
			else
			{
				g = m.Groups["TableAlias"];
				if (g == null)
					return false;				
				
				if(SqlKeywords.Keywords.Contains(g.Value.Trim().ToLowerInvariant()))
					return true;
			}
		
			return false;
		}
  }

  public class AsyncSqlTableAliasesWorker : AsyncWorker
  {
    private AsyncSqlTableAliasesWorker( ) { }

    public static IAsyncResult StartWork( string source, TableAliasType aliasType, AsyncCallback callback)
    {
      return StartWork(source, aliasType, callback, null);
    }

    public static IAsyncResult StartWork( string source, TableAliasType aliasType, AsyncCallback callback, object state)
    {
      SqlTableAliasesWorkItem result = new SqlTableAliasesWorkItem(source, aliasType, callback, state);
      ThreadPool.QueueUserWorkItem(ThreadPool_WaitCallback, result);
      return result;
    }

    public static SqlTableAliasesWorkItem EndWork( IAsyncResult result )
    {
      return (SqlTableAliasesWorkItem)result;
    }
  }
}
