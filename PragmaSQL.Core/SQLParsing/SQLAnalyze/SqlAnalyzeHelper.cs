/********************************************************************
  Class SqlAnalyzeHelper
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PragmaSQL.Core
{
  public static class SqlAnalyzeHelper
  {
    public static string ReplaceDataTypesWithQualifiedOnes( string expression )
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

    public static string CleanIdentifier( string value )
    {
      string result = value;
      result = result.Replace("[", String.Empty);
      result = result.Replace("]", String.Empty);
      return result.Trim();
    }

    public static string MatchTableName( string value )
    {
      string expression = ResManager.GetRegularExpression("SqlAnalyze_Words");
      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
      string result = value;
      Match m = null; ;
      string prevMatch = String.Empty;
      for (m = r.Match(value); m.Success; m = m.NextMatch())
      {
        prevMatch = result;
        result = m.Value;
      }

      if (result.Trim().ToLowerInvariant() == "as")
      {
        return prevMatch;
      }
      else
      {
        return result;
      }
    }

    public static string RemoveComments( string sqlText )
    {
      string s = sqlText;
      string expression = ResManager.GetRegularExpression("SqlAnalyze_Comments");
      Regex r = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

      return r.Replace(sqlText, " ");
    }
  }
}
