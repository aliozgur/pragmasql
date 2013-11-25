/********************************************************************
  Class      : SqlAnalyzer
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public static class SqlAnalyzer
  {
    private static void WaitAll( WaitHandle[] waitHandles )
    {
      if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
      {
        foreach (WaitHandle myWaitHandle in waitHandles)
        {
          WaitHandle.WaitAny(new WaitHandle[] { myWaitHandle },2000,false);
        }
      }
      else
      {
        WaitHandle.WaitAll(waitHandles, 2000, false);
      }
    }

    public static SqlAnalyzerResults AnalyzeSql( string sqlText, bool combine )
    {
      SqlAnalyzerResults result = new SqlAnalyzerResults();
      SqlVariablesWorkItem varsItem1 = (SqlVariablesWorkItem)AsyncSqlVariablesWorker.StartWork(sqlText, false, HandleVariablesEndRegexWork);
      SqlTableAliasesWorkItem tblAliasesItem1 = (SqlTableAliasesWorkItem)AsyncSqlTableAliasesWorker.StartWork(sqlText, TableAliasType.From, HandleTableAliasesEndRegexWork);
      SqlTableAliasesWorkItem tblAliasesItem2 = (SqlTableAliasesWorkItem)AsyncSqlTableAliasesWorker.StartWork(sqlText, TableAliasType.Join, HandleTableAliasesEndRegexWork);
      SqlTablesWorkItem tablesItem1 = (SqlTablesWorkItem)AsyncSqlTablesWorker.StartWork(sqlText, TableType.Temp, HandleTablesEndRegexWork);
      SqlTablesWorkItem tablesItem2 = (SqlTablesWorkItem)AsyncSqlTablesWorker.StartWork(sqlText, TableType.Variable, HandleTablesEndRegexWork);


      CancellableWorkItem[] jobs = new CancellableWorkItem[] {
                varsItem1,
                tblAliasesItem1,
                tblAliasesItem2,
                tablesItem1,
                tablesItem2
      };

      // Wait for 2 seconds.  After that we cancel any jobs that have not completed
      WaitHandle[] waitHandles = new WaitHandle[] { varsItem1.AsyncWaitHandle, 
                tblAliasesItem1.AsyncWaitHandle,
                tblAliasesItem2.AsyncWaitHandle,
                tablesItem1.AsyncWaitHandle,
                tablesItem2.AsyncWaitHandle};

      WaitAll(waitHandles);


      for (int i = 0; i < jobs.Length; i++)
      {
        if (jobs[i].IsCompleted)
        {
          if (jobs[i] is SqlVariablesWorkItem)
          {
            SqlVariablesWorkItem vars = jobs[i] as SqlVariablesWorkItem;
            if (!vars.NamesOnly)
            {
              result.VariableNamesWithDataTypes = vars.Result;
            }
            else
            {
              result.Variables = vars.Result;
            }
          }
          else if (jobs[i] is SqlTableAliasesWorkItem)
          {
            SqlTableAliasesWorkItem aliases = jobs[i] as SqlTableAliasesWorkItem;
            switch (aliases.AliasType)
            {
              case TableAliasType.From:
                result.TableAliasFrom = aliases.Result;
                break;
              case TableAliasType.Join:
                result.TableAliasJoin = aliases.Result;
                break;
              default:
                break;
            }
          }
          else if (jobs[i] is SqlTablesWorkItem)
          {
            SqlTablesWorkItem tables = jobs[i] as SqlTablesWorkItem;
            switch (tables.TableType)
            {
              case TableType.Temp:
                result.TableAsTemp = tables.Result;
                break;
              case TableType.Variable:
                result.TableAsVariable = tables.Result;
                break;
              default:
                break;
            }
          }
        }
        else
        {
          jobs[i].Cancel();
        }
      }

      if (combine)
      {
        result.CombineResults();
      }

      return result;
    }

    private static void HandleVariablesEndRegexWork( IAsyncResult ar )
    {
      SqlVariablesWorkItem item = AsyncSqlVariablesWorker.EndWork(ar);

      if (item != null && item.IsCancelled)
      {
        if (!item.NamesOnly)
        {
          HostServicesSingleton.HostServices.MsgService.LogWarning("SqlAnalyzer timed out for: Variables with names.");
        }
        else
        {
          HostServicesSingleton.HostServices.MsgService.LogWarning("SqlAnalyzer timed out for: Variables with names and datatypes.");
        }
      }
    }
    private static void HandleTableAliasesEndRegexWork( IAsyncResult ar )
    {
      SqlTableAliasesWorkItem item = AsyncSqlTableAliasesWorker.EndWork(ar);
      if (item != null &&  item.IsCancelled)
      {
        switch (item.AliasType)
        {
          case TableAliasType.From:
            HostServicesSingleton.HostServices.MsgService.LogWarning("SqlAnalyzer timed out for: Table aliases (From).");
            break;
          case TableAliasType.Join:
            HostServicesSingleton.HostServices.MsgService.LogWarning("SqlAnalyzer timed out for: Table aliases (Join).");
            break;
          default:
            break;
        }
      }
    }
    private static void HandleTablesEndRegexWork( IAsyncResult ar )
    {
      SqlTablesWorkItem item = AsyncSqlTablesWorker.EndWork(ar);
      if (item != null && item.IsCancelled)
      {
        switch (item.TableType)
        {
          case TableType.Temp:
            HostServicesSingleton.HostServices.MsgService.LogWarning("SqlAnalyzer timed out for: Table (Temporary).");
            break;
          case TableType.Variable:
            HostServicesSingleton.HostServices.MsgService.LogWarning("SqlAnalyzer timed out for: Table (Variable).");
            break;
          default:
            break;
        }
      }

    }
  }
}
