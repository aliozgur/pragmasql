using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class DBConstants
  {
    public static IList<string> SystemDbNames = new List<string>();
    public static IList<string> DataTypeNames1 = new List<string>();
    public static IList<string> DataTypeNames2 = new List<string>();
    public static IList<string> DatabaseObjectTypes = new List<string>();

    static DBConstants()
    {
      InitializeSystemDatabaseNames();
      InitializeDataTypeNames();
      InitializeObjectTypeNames();
    }

    private static void InitializeSystemDatabaseNames()
    {
      SystemDbNames.Add("master");
      SystemDbNames.Add("model");
      SystemDbNames.Add("msdb");
      SystemDbNames.Add("tempdb");
    }

    private static void InitializeDataTypeNames()
    {
  
      DataTypeNames1.Add("binary");
      DataTypeNames1.Add("char");
      DataTypeNames1.Add("nchar");
      DataTypeNames1.Add("nvarchar");
      DataTypeNames1.Add("varbinary");
      DataTypeNames1.Add("varchar");

      DataTypeNames2.Add("numeric");
      DataTypeNames2.Add("decimal");
    }

    private static void InitializeObjectTypeNames()
    {
      DatabaseObjectTypes.Add("C".ToLowerInvariant());
      DatabaseObjectTypes.Add("D".ToLowerInvariant());
      DatabaseObjectTypes.Add("F".ToLowerInvariant());
      DatabaseObjectTypes.Add("L".ToLowerInvariant());
      DatabaseObjectTypes.Add("FN".ToLowerInvariant());
      DatabaseObjectTypes.Add("IF".ToLowerInvariant());
      DatabaseObjectTypes.Add("P".ToLowerInvariant());
      DatabaseObjectTypes.Add("PK".ToLowerInvariant());
      DatabaseObjectTypes.Add("RF".ToLowerInvariant());
      DatabaseObjectTypes.Add("S".ToLowerInvariant());
      DatabaseObjectTypes.Add("TF".ToLowerInvariant());
      DatabaseObjectTypes.Add("TR".ToLowerInvariant());
      DatabaseObjectTypes.Add("U".ToLowerInvariant());
      DatabaseObjectTypes.Add("UQ".ToLowerInvariant());
      DatabaseObjectTypes.Add("V".ToLowerInvariant());
      DatabaseObjectTypes.Add("X".ToLowerInvariant());
    }

    public static bool DoesObjectTypeHasScript(int type)
    {
      bool result = false;
      switch(type)
      {
        case DBObjectType.TableValuedFunction:
          result = true;
          break;
        case DBObjectType.ScalarValuedFunction:
          result = true;
          break;
        case DBObjectType.StoredProc:
          result = true;
          break;
        case DBObjectType.Trigger:
          result = true;
          break;
        case DBObjectType.View:
          result = true;
          break;
        default:
          break;
      }
      return result;
    }

    public static bool DoesObjectTypeHasScript(string type)
    {
      bool result = false;
      string lType = type.ToLowerInvariant().Trim();
      switch(lType)
      {
        case "fn":
          result = true;
          break;
        case "if":
          result = true;
          break;
        case "p":
          result = true;
          break;
        case "tf":
          result = true;
          break;
        case "tr":
          result = true;
          break;
        case "v":
          result = true;
          break;
        case "x":
          result = true;
          break;
        default:
          break;
      }
      return result;
    }

    public static bool DoesObjectTypeHoldsData(int type)
    {
      bool result = false;
      switch(type)
      {
        case DBObjectType.SystemTable:
          result = true;
          break;
        case DBObjectType.UserTable:
          result = true;
          break;
        case DBObjectType.View:
          result = true;
          break;
        default:
          break;
      }
      return result;    
    }

    public static bool DoesObjectTypeHoldsData(string type)
    {
      bool result = false;
      string lType = type.ToLowerInvariant().Trim();
      switch(lType)
      {
        case "s":
          result = true;
          break;
        case "u":
          result = true;
          break;
        case "v":
          result = true;
          break;
        default:
          break;
      }
      return result;    
    }

    public static string GetObjectTypeAbb(int type)
    {
      string result = String.Empty;
      switch(type)
      {
        case DBObjectType.Function:
        case DBObjectType.ScalarValuedFunction:
          result = "fn";
          break;
        case DBObjectType.StoredProc:
          result = "p";
          break;
        case DBObjectType.TableValuedFunction:
          result = "tf";
          break;
        case DBObjectType.Trigger:
          result = "tr";
          break;
        case DBObjectType.View:
          result = "v";
          break;
        case DBObjectType.SystemTable:
          result = "s";
          break;
        case DBObjectType.UserTable:
          result = "u";
          break;
        default:
          result = String.Empty;
          break;
      }
      return result;
    }

    public static int GetDBObjectType(string typeAbb)
    {
      int result = DBObjectType.None;
      string lType = typeAbb.ToLowerInvariant().Trim();
      switch(lType)
      {
        case "fn":
          result = DBObjectType.ScalarValuedFunction;
          break;
        case "if":
          result = DBObjectType.TableValuedFunction;
          break;
        case "p":
          result = DBObjectType.StoredProc;
          break;
        case "tf":
          result = DBObjectType.TableValuedFunction;
          break;
        case "tr":
          result = DBObjectType.Trigger;
          break;
        case "v":
          result = DBObjectType.View;
          break;
        case "x":
          result = DBObjectType.StoredProc;
          break;
        case "s":
          result = DBObjectType.SystemTable;
          break;
        case "u":
          result = DBObjectType.UserTable;
          break;
        default:
          break;
      }
      return result;
    }

    public static string GetFullyQualifiedDataTypeName(bool qualified, string dataTypeName, int len1, int scale, int precision)
    {
      string tmpName = ( qualified ? "[" + dataTypeName + "]" : dataTypeName);
      string tmpDataTypeName = dataTypeName.ToLowerInvariant();

      if(DataTypeNames1.Contains(tmpDataTypeName))
      {
        return tmpName + "(" + ( len1 < 0 ? "MAX" : len1.ToString()) + ")";
      }
      else if(DataTypeNames2.Contains(tmpDataTypeName))
      {
        return tmpName + "(" + precision.ToString()+ "," + scale.ToString()+ ")";
      }
      else
      {
        return tmpName;
      }
    }
  }
}
