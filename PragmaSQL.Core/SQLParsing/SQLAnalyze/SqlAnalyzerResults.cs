/********************************************************************
  Class      : SqlAnalyzerResults
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class SqlAnalyzerResults
  {
    public IDictionary<string, SqlVariable> VariableNames = null;
    public IDictionary<string, SqlVariable> VariableNamesWithDataTypes = null;
    public IDictionary<string, SqlVariable> Variables = new Dictionary<string, SqlVariable>();

    public IDictionary<string, SqlTableAlias> TableAliasFrom  = null;
    public IDictionary<string, SqlTableAlias> TableAliasJoin  = null;
    public IDictionary<string, SqlTableAlias> TableAliases  = new Dictionary<string, SqlTableAlias>();

    public IDictionary<string, SqlTable> TableAsVariable  = null;
    public IDictionary<string, SqlTable> TableAsTemp  = null;
    public IDictionary<string, SqlTable> Tables  = new Dictionary<string, SqlTable>();

    public void CombineResults()
    {
      Variables.Clear();
      
      if(VariableNamesWithDataTypes != null)
      {
        foreach(string key in VariableNamesWithDataTypes.Keys)
        {
          if(!Variables.ContainsKey(key))
          {
            Variables.Add(key,VariableNamesWithDataTypes[key]);
          }
        }
      }
      
      if(VariableNames != null)
      {
        foreach(string key in VariableNames.Keys)
        {
          if(!Variables.ContainsKey(key))
          {
            Variables.Add(key,VariableNames[key]);
          }
        }
      }
      
      

      TableAliases.Clear();
      if(TableAliasFrom != null)
      {
        foreach(string key in TableAliasFrom.Keys)
        {
          /*
          if(TableAliasFrom[key].TableAlias.ToLowerInvariant() == TableAliasFrom[key].TableName.ToLowerInvariant())
          {
            continue;
          }
          */

          if(!TableAliases.ContainsKey(key))
          {
            TableAliases.Add(key,TableAliasFrom[key]);
          }
        }
      }

      if(TableAliasJoin != null)
      {
        foreach(string key in TableAliasJoin.Keys)
        {
          /*
          if(TableAliasJoin[key].TableAlias.ToLowerInvariant() == TableAliasJoin[key].TableName.ToLowerInvariant())
          {
            continue;
          }
          */

          if(!TableAliases.ContainsKey(key))
          {
            TableAliases.Add(key,TableAliasJoin[key]);
          }
        }
      }

      Tables.Clear();
      if(TableAsTemp != null)
      {
        foreach(string key in TableAsTemp.Keys)
        {
          if(!Tables.ContainsKey(key))
          {
            Tables.Add(key,TableAsTemp[key]);
          }
        }
      }
      
      if(TableAsVariable != null)
      {
        foreach(string key in TableAsVariable.Keys)
        {
          if(!Tables.ContainsKey(key))
          {
            Tables.Add(key,TableAsVariable[key]);
          }
        }
      }
    }
  }
}
