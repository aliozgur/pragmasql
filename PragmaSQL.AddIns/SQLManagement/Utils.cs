using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public static class Utils
  {
    public static void ShowException(Exception ex)
    {

      MessageService.ShowError(ex.Message + ((ex.InnerException != null) ? "\r\n" + GetInnerExceptionMsg(ex.InnerException) : String.Empty));

    }

    private static string GetInnerExceptionMsg(Exception ex)
    {
      string result = ex.Message;

      if (ex == null || ex.InnerException == null)
        return result;

      result += "\r\n" + GetInnerExceptionMsg(ex.InnerException);
      return result;
    }

    #region General utility functions
    
    public static string GetFormattedNow()
    {
      DateTime t = DateTime.UtcNow;
      return
        t.Year.ToString("0000") + t.Month.ToString("00") + t.Day.ToString("00") + "_" + t.Hour.ToString("00") + t.Minute.ToString("00") + t.Second.ToString("00") + t.Millisecond.ToString("000");

    }

    public static bool IsDbValueValid(object value)
    {
      return (value != null && value.GetType() != typeof(DBNull));
    }

    public static bool IsRowItemValid(DataRow row, int index)
    {
      return !(row.ItemArray[index] == null || row.ItemArray[index].GetType() == typeof(DBNull));
    }

    public static bool IsRowItemValid(DataRow row, string colName)
    {
      return !(row[colName] == null || row[colName].GetType() == typeof(DBNull));
    }

    public static bool IsGridRowItemValid(DataGridViewRow row, int index)
    {
      return !(row.Cells[index].Value == null || row.Cells[index].Value.GetType() == typeof(DBNull));
    }

    public static bool IsReaderItemValid(SqlDataReader reader, string colName)
    {
      return !(reader[colName] == null || reader[colName].GetType() == typeof(DBNull));
    }


    public static string ReplaceQuatations(string value)
    {
      return value.Replace("'", "''");
    }

    public static string Qualify(string value)
    {
      return "[" + value + "]";
    }

    

    #endregion //General utility functions

    #region DataType Related
    public static DataTypeProperties EvaluateDataTypeProps(string typeName)
    {
      DataTypeProperties result = DataTypeProperties.None;
      DataTypeWrap dType = new DataTypeWrap();
      string type = !String.IsNullOrEmpty(typeName) ? typeName.ToLowerInvariant() : String.Empty;

      switch (type)
      {
        case "bigint":
        case "bit":
        case "datetime":
        case "float":
        case "image":
        case "int":
        case "money":
        case "ntext":
        case "real":
        case "smalldatetime":
        case "smallint":
        case "smallmoney":
        case "sql_variant":
        case "text":
        case "timestamp":
        case "tinyint":
        case "uniqueidentifier":
        case "xml":
          result = DataTypeProperties.None;
          break;
        case "binary":
        case "char":
        case "nchar":
        case "nvarchar":
        case "varbinary":
        case "varchar":
          result = DataTypeProperties.Width;
          break;
        case "decimal":
        case "numeric":
          result = DataTypeProperties.Precision | DataTypeProperties.Scale;
          break;
        default:
          result = DataTypeProperties.None;
          break;
      }
      return result;
    }

    public static bool SupportsCollation( string typeName )
    {
      bool result = false;

      string type = !String.IsNullOrEmpty(typeName) ? typeName.ToLowerInvariant() : String.Empty;

      switch (type)
      {
        case "ntext":
        case "text":
        case "char":
        case "nchar":
        case "nvarchar":
        case "varchar":
          result = true;
          break;
        default:
          result = false;
          break;
      }
      return result;
    }

    public static bool IsDataTypeValidForIdentity( string typeName, string scale )
    {
      bool result = false;

      string type = !String.IsNullOrEmpty(typeName) ? typeName.ToLowerInvariant() : String.Empty;

      switch (type)
      {
        case "bigint":
        case "int":
        case "smallint":
        case "tinyint":
          result = true;
          break;
        case "decimal":
        case "numeric":
          return ( scale == "0" || String.IsNullOrEmpty(scale));
        default:
          result = false;
          break;
      }
      return result;
    }

    public static DataTypeWrap GetDataTypeDefaults(string typeName)
    {
      DataTypeWrap dType = new DataTypeWrap();
      string type = !String.IsNullOrEmpty(typeName) ? typeName.ToLowerInvariant() : String.Empty;

      switch (type)
      {
        
        case "char":
        case "nchar":
          dType.Width = "10";
          break;
        case "binary":
        case "nvarchar":
        case "varbinary":
        case "varchar":
          dType.Width = "50";
          break;
        case "decimal":
        case "numeric":
          dType.Precision = "18";
          dType.Scale = "0";
          break;
        default:
          break;
      }
      return dType;
    }

    public static bool IsUserDefinedType( string typeName )
    {
      string type = !String.IsNullOrEmpty(typeName) ? typeName.ToLowerInvariant() : "";
      switch (type)
      {
        case "bigint":
        case "bit":
        case "datetime":
        case "float":
        case "image":
        case "int":
        case "money":
        case "ntext":
        case "real":
        case "smalldatetime":
        case "smallint":
        case "smallmoney":
        case "sql_variant":
        case "text":
        case "timestamp":
        case "tinyint":
        case "uniqueidentifier":
        case "xml":
        case "binary":
        case "char":
        case "nchar":
        case "nvarchar":
        case "varbinary":
        case "varchar":
        case "decimal":
        case "numeric":
        case "":
          return false;
        default:
          return true;
      }
    }

    public static KeyValuePair<string, string> ParseUserDefinedDataTypeParts(string dataType)
    {
      Match m = Regex.Match(dataType, @"(?<Owner>.+)\.(?<DataType>.+)");

      if (m.Success)
        return new KeyValuePair<string, string>(m.Groups["Owner"].Value,m.Groups["DataType"].Value);
      else
        return new KeyValuePair<string, string>();
    }

    public static string GetDataTypeInfo(string typeName, int width, int scale, int precision)
    {
      DataTypeProperties props = EvaluateDataTypeProps(typeName);
      if ((props & DataTypeProperties.Width) == DataTypeProperties.Width)
      {
        return typeName + "(" + width.ToString() + ")";      
      }
      else if ((props & (DataTypeProperties.Scale | DataTypeProperties.Precision)) == (DataTypeProperties.Scale | DataTypeProperties.Precision))
      {
        return typeName + "(" + precision.ToString() + "," + scale.ToString() + ")";      
      }
      else      
      {
        return typeName;
      }
    }

    public static bool CanHaveWidth( string dataType )
    {
      DataTypeProperties props = Utils.EvaluateDataTypeProps(dataType);

      return 
        ((props & DataTypeProperties.None) != DataTypeProperties.None)
        &&
        (
          ((props & DataTypeProperties.Width) == DataTypeProperties.Width) 
          || ((props & DataTypeProperties.All) == DataTypeProperties.All) 
        ); 
 
    }

    public static bool CanHavePrec( string dataType )
    {
      DataTypeProperties props = Utils.EvaluateDataTypeProps(dataType);
      return
        ((props & DataTypeProperties.None) != DataTypeProperties.None)
        &&
        (
          ((props & DataTypeProperties.Precision) == DataTypeProperties.Precision)
          || ((props & DataTypeProperties.All) == DataTypeProperties.All)
        );
    }

    public static bool CanHaveScale( string dataType )
    {
      DataTypeProperties props = Utils.EvaluateDataTypeProps(dataType);
      return
        ((props & DataTypeProperties.None) != DataTypeProperties.None)
        && 
        (
          ((props & DataTypeProperties.Scale) == DataTypeProperties.Scale) 
          || ((props & DataTypeProperties.All) == DataTypeProperties.All) 
        ); 
    }

    public static bool CanHavePrecAndScale( string dataType )
    {
      return CanHavePrec(dataType) && CanHaveScale(dataType);
    }

    #endregion //DataType Related

  }
}
