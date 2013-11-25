/********************************************************************
  Class SqlTablsWorkItem
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
  public class SqlTablesWorkItem:CancellableWorkItem
  {
    string _source = string.Empty;
    TableType _tableType = TableType.Temp;
    public TableType TableType
    {
      get
      {
        return _tableType;
      }
    }
    
    private IDictionary<string, SqlTable> _result = null;
    public IDictionary<string, SqlTable> Result
    {
      get { return _result; }
    }

    public SqlTablesWorkItem( string source, TableType tableType, AsyncCallback callback, object state )
    {
      this._source = source;
      this._callback = callback;
      this._asyncState = state;
      this._tableType = tableType;
      this._waitHandle = new ManualResetEvent(this._completedSynchronously);
    }


    public override void DoWork( )
    {
      _result = GetTables(_source, _tableType);
    }

    private IDictionary<string, SqlTable> GetTables( string sqlText, TableType tableType )
    {
      Dictionary<string, SqlTable> result = new Dictionary<string, SqlTable>();
      string expression = String.Empty;
      switch (tableType)
      {
        case TableType.Temp:
          expression = ResManager.GetRegularExpression("SqlAnalyze_TempTable");
          break;
        case TableType.Variable:
          expression = ResManager.GetRegularExpression("SqlAnalyze_LocalTableVariable");
          break;
        default:
          return result;
      }

      expression =   SqlAnalyzeHelper.ReplaceDataTypesWithQualifiedOnes(expression);

      Match m;

      SqlTable tbl = null;
      Group g = null;
      for (m = Regex.Match(sqlText, expression, RegexOptions.Compiled); m.Success; m = m.NextMatch())
      {
        g = m.Groups["TableName"];
        if (g == null)
        {
          continue;
        }
        string tName = SqlAnalyzeHelper.CleanIdentifier(g.Value);
        if (result.ContainsKey(tName))
        {
          continue;
        }

        tbl = new SqlTable(tableType);
        tbl.TableName = tName;
        result.Add(tbl.TableName, tbl);


        g = m.Groups["ColNames"];
        string cName = String.Empty;
        if (g != null)
        {
          foreach (Capture c in g.Captures)
          {
            cName = SqlAnalyzeHelper.CleanIdentifier(c.Value);
            tbl.ColumnNames.Add(cName);
          }
        }

        string dName = String.Empty;
        g = m.Groups["DataTypes"];
        if (g != null)
        {
          foreach (Capture c in g.Captures)
          {
            dName = SqlAnalyzeHelper.CleanIdentifier(c.Value);
            tbl.ColumnDataTypes.Add(dName);
          }
        }

        for (int i = 0; i < tbl.ColumnNames.Count; i++)
        {
          string colName = tbl.ColumnNames[i];
          string dataType = String.Empty;
          if (i < tbl.ColumnDataTypes.Count)
          {
            dataType = tbl.ColumnDataTypes[i];
          }

          tbl.FullyQualifiedColumns.Add(colName + (String.IsNullOrEmpty(dataType) ? String.Empty : " " + dataType));

        }
      }
      return result;
    }
  }

  public class AsyncSqlTablesWorker: AsyncWorker
  {
    private AsyncSqlTablesWorker( ) { }

    public static IAsyncResult StartWork( string source, TableType tableType, AsyncCallback callback )
    {
      return StartWork(source, tableType, callback, null);
    }

    public static IAsyncResult StartWork( string source, TableType tableType, AsyncCallback callback, object state )
    {
      SqlTablesWorkItem result = new SqlTablesWorkItem(source, tableType, callback, state);
      ThreadPool.QueueUserWorkItem(ThreadPool_WaitCallback, result);
      return result;
    }

    public static SqlTablesWorkItem EndWork( IAsyncResult result )
    {
      return (SqlTablesWorkItem)result;
    }
  }
}
