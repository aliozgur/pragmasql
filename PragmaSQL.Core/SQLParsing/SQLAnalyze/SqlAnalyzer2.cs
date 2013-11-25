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

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public static class SqlAnalyzer2
  {
    private static string CleanIdentifier( string value )
    {
      string result = value;
      result = result.Replace("[", String.Empty);
      result = result.Replace("]", String.Empty);
      return result.Trim();
    }

    
    private static string MatchTableName(string value)
    {
      string expression = ResManager.GetRegularExpression("SqlAnalyze_Words");
      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      string result = value;
      Match m = null;;
      string prevMatch = String.Empty;
      for (m = r.Match(value); m.Success; m = m.NextMatch())
      {
        prevMatch = result;
        result = m.Value;
      }

      if(result.Trim().ToLowerInvariant() == "as")
      {
        return prevMatch;
      }
      else
      {
        return result;
      }
    }
    

    private static string ReplaceDataTypesWithQualifiedOnes( string expression )
    {
      string[] dataTypes = new string[26]{"bigint","binary","bit","datetime","decimal","decimal","float","image","int","money",
        "nchar","ntext","numeric","nvarchar","real","smalldatetime","smallint","smallmoney",
        "sql_variant","text","timestamp","tinyint","uniqueidentifier","varbinary","varchar","xml"};


      string result = expression;
      foreach (string dataType in dataTypes)
      {
        result = result.Replace(dataType, @"\[?\s*" + dataType + @"\s*\]?");
      }

      return result;
    }


    #region Variable

    public static IDictionary<string, SqlVariable> GetVariables( string sqlText, bool withDataTypes )
    {
      if (withDataTypes)
      {
        return GetVariables_WithDataTypes(sqlText);
      }
      else
      {
        return GetVariables_NameOnly(sqlText);
      }
    }

    private static IDictionary<string, SqlVariable> GetVariables_NameOnly( string sqlText )
    {
      IDictionary<string, SqlVariable> result = new Dictionary<string, SqlVariable>();

      string expression = ResManager.GetRegularExpression("SqlAnalyze_VariableNameOnly");
      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      Match m;

      SqlVariable v = null;
      for (m = r.Match(sqlText); m.Success; m = m.NextMatch())
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

    private static IDictionary<string, SqlVariable> GetVariables_WithDataTypes( string sqlText )
    {
      IDictionary<string, SqlVariable> result = new Dictionary<string, SqlVariable>();

      string expression = ResManager.GetRegularExpression("SqlAnalyze_VariableWithDataType");
      expression = ReplaceDataTypesWithQualifiedOnes(expression);

      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      Match m;

      SqlVariable v = null;
      Group g = null;
      for (m = r.Match(sqlText); m.Success; m = m.NextMatch())
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

        dName = CleanIdentifier(g.Value);
        v = new SqlVariable();
        v.Name = dName;
        v.FullyQualifiedName = dName;
        g = m.Groups["DataType"];
        result.Add(dName, v);

        if (g != null)
        {
          v.DataType = CleanIdentifier(g.Value);
          v.FullyQualifiedName += " " + v.DataType;
        }

      }
      return result;
    }

    #endregion

    #region Table Alias

    public static IDictionary<string, SqlTableAlias> GetTableAliases( string sqlText, TableAliasType aliasType )
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

      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      Match m;
      
      SqlTableAlias ta = null;
      Group g = null;
      for (m = r.Match(sqlText); m.Success; m = m.NextMatch())
      {
        g = m.Groups["TableName"];
        if (g == null)
        {
          continue;
        }

        string tName = CleanIdentifier(g.Value);
        if(tName.Contains("(") || tName.Contains(")"))
        {
          continue;
        }
        
        tName = MatchTableName(tName);
        if (result.ContainsKey(tName))
        {
          continue;
        }

        ta = new SqlTableAlias(aliasType);
        ta.TableName = tName;
        result.Add(ta.TableName, ta);

        g = m.Groups["TableAlias"];
        
        if (g != null)
        {
          if( CleanIdentifier(g.Value).Trim() == "on")
          {
            ta.TableAlias = ta.TableName;
          }
          else
          {
            ta.TableAlias = CleanIdentifier(g.Value);                      
          }
        }

      }
      return result;
    }

    #endregion

    #region Table Declarations
    public static IDictionary<string, SqlTable> GetTables( string sqlText, TableType tableType )
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

      expression = ReplaceDataTypesWithQualifiedOnes(expression);

      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      Match m;

      SqlTable tbl = null;
      Group g = null;
      for (m = r.Match(sqlText); m.Success; m = m.NextMatch())
      {
        g = m.Groups["TableName"];
        if (g == null)
        {
          continue;
        }
        string tName = CleanIdentifier(g.Value);
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
            cName = CleanIdentifier(c.Value);
            tbl.ColumnNames.Add(cName);
          }
        }

        string dName = String.Empty;
        g = m.Groups["DataTypes"];
        if (g != null)
        {
          foreach (Capture c in g.Captures)
          {
            dName = CleanIdentifier(c.Value);
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
    #endregion

    public static SqlAnalyzerResults AnalyzeSql( string sqlText, bool combine )
    {
      SqlAnalyzerResults result = new SqlAnalyzerResults();

      //1- Variables

      result.VariableNamesWithDataTypes = GetVariables(sqlText, true);
      //result.VariableNames = GetVariables(sqlText,false);

      //2- Table aliases
      result.TableAliasFrom = GetTableAliases(sqlText, TableAliasType.From);
      result.TableAliasJoin = GetTableAliases(sqlText, TableAliasType.Join);

      //3- Table definitions

      result.TableAsTemp = GetTables(sqlText, TableType.Temp);
      result.TableAsVariable = GetTables(sqlText, TableType.Variable);

      //4- Combine all
      if (combine)
      {
        result.CombineResults();
      }

      return result;
    }

    public static string RemoveComments( string sqlText )
    {
      string s = sqlText;
      string expression = ResManager.GetRegularExpression("SqlAnalyze_Comments");
      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

      return r.Replace(sqlText, String.Empty);
    }
  }
}
