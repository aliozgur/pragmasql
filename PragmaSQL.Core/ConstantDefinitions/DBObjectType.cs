using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace PragmaSQL.Core
{
  /// <summary>
  /// Constant db object type definitions
  /// </summary>
  public class DBObjectType
  {
    public const int None = -1;
    public const int Server = 0;
    public const int GroupingFolderY = 1;
    public const int GroupingFolderB = 2;
    public const int Database = 3;
    public const int UserTable = 4;
    public const int SystemTable = 5;
    public const int View = 6;
    public const int StoredProc = 7;
    public const int TableValuedFunction = 8;
    public const int ScalarValuedFunction = 9;
    public const int ParameterIn = 10;
    public const int ParameterOut = 11;
    public const int Column = 12;
    public const int PrimaryKey = 13;
    public const int ForeignKey = 14;
    public const int PrimaryAndForeignKey = 15;
    public const int Dependecy = 16;
    public const int Index = 17;
    public const int Trigger = 18;
    public const int UsersGroup = 19;
    public const int User = 20;
    public const int Error = 21;
    public const int IdentityCol = 22;
    public const int Function = 23;
    public const int Table = 24;
    public const int SharedScript = 25;
    public const int SharedSnippet = 26;
    public const int Synonym = 26;


    /// <summary>
    /// Extracts field name,value structure for constants with reflection
    /// </summary>
    /// <returns></returns>
    public static IList<DBObjectTypeDesc> GetTypeDescriptions()
    {
      DBObjectType o = new DBObjectType();

      IList<DBObjectTypeDesc> result = new List<DBObjectTypeDesc>();
      Type t = typeof(DBObjectType);
      FieldInfo[] fi = t.GetFields();
     
      foreach (FieldInfo i in fi)
      {
        try
        {
          if (null != i.GetValue(o))
          {
            result.Add(new DBObjectTypeDesc((int)i.GetValue(o), i.Name));
          }
        }
        catch
        {
        }
      }

      return result;
    }
    
    /// <summary>
    /// Extracts type name of supported value using reflection
    /// </summary>
    /// <param name="ObjectType"></param>
    /// <returns></returns>
    public static string GetNameOfType(int ObjectType)
    {
      DBObjectType o = new DBObjectType();

      string result = "Not a known object type!";
      Type t = typeof(DBObjectType);
      FieldInfo[] fi = t.GetFields();

      foreach (FieldInfo i in fi)
      {
        try
        {
          object value = i.GetValue(o);
          if (null != value && (int)value == ObjectType)
          {
            result = i.Name;
          }
        }
        catch
        {
        }
      }

      return result;
    }

		public static bool CanTypeBeDumpedForScriptingWizardUsage(int typeId)
		{

			switch (typeId)
			{
				case DBObjectType.Table:
				case DBObjectType.UserTable:
				case DBObjectType.StoredProc:
				case DBObjectType.TableValuedFunction:
				case DBObjectType.ScalarValuedFunction:
				case DBObjectType.View:
					return true;
				default:
					return false;
			}
		}

    public static bool IsFolder(int typeId)
    {

      switch (typeId)
      {
        case DBObjectType.GroupingFolderB:
        case DBObjectType.GroupingFolderY:
        case DBObjectType.Server:
        case DBObjectType.Database:
          return true;
        default:
          return false;
      }
    }

    public static bool CanTypeBeImportedToObjectGroup(int typeId)
    {

      switch (typeId)
      {
        case DBObjectType.Table:
        case DBObjectType.UserTable:
        case DBObjectType.StoredProc:
        case DBObjectType.TableValuedFunction:
        case DBObjectType.ScalarValuedFunction:
        case DBObjectType.View:
          return true;
        default:
          return false;
      }
    }
	}

  /// <summary>
  /// Stores value, description for db object types
  /// </summary>
  public struct DBObjectTypeDesc
  {
    public int Value;
    public string Description;

    public DBObjectTypeDesc(int value, string description)
    {
      Value = value;
      Description = description;
    }

    public override string ToString()
    {
      return Description;
    }
  }
}
