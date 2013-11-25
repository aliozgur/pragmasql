/********************************************************************
  Class      : CodeCompletionListLoader
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.Sql;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
	/// <summary>
	/// Loader class for user defined code completion lists.
	/// <remarks>Used internally by PragmaSQL application.</remarks>
	/// </summary>
  public static class CodeCompletionListLoader
  {
    private static DataSet _dsLists = new DataSet();
    public static DataSet ListsAsDataSet
    {
      get { return CodeCompletionListLoader._dsLists; }
    }

    private static List<CodeCompletionList> _lists = null;
    public static List<CodeCompletionList> Lists
    {
      get { return CodeCompletionListLoader._lists; }
    }

    private static string defaultFileName = String.Empty;

    static CodeCompletionListLoader( )
    {

      string appDataDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL";
      if (!Directory.Exists(appDataDir))
      {
        Directory.CreateDirectory(appDataDir);
      }

      if (!Directory.Exists(appDataDir))
      {
        FileInfo fi = new FileInfo(Application.ExecutablePath);
        defaultFileName = fi.Directory + "\\PragmaSQL_CodeCompletionLists.config";
      }
      else
      {
        defaultFileName = appDataDir + "\\PragmaSQL_CodeCompletionLists.config";
      }
    }

    public static List<CodeCompletionList> Load( )
    {
      return Load(defaultFileName);
    }

    public static List<CodeCompletionList> Load( string fileName )
    {
      List<CodeCompletionList> result = null;

      if (!File.Exists(fileName))
      {
        _lists = new List<CodeCompletionList>();
        return _lists;
      }

      result = ObjectXMLSerializer<List<CodeCompletionList>>.Load(fileName);
      if (result == null)
      {
        result = new List<CodeCompletionList>();
      }
      _lists = result;
      FillDataSet();
      return result;
    }

    public static void Save( List<CodeCompletionList> codeCompletionLists )
    {
      Save(codeCompletionLists, defaultFileName);
    }

    public static void Save( List<CodeCompletionList> codeCompletionLists, string fileName )
    {
      ObjectXMLSerializer<List<CodeCompletionList>>.Save(codeCompletionLists, fileName);
    }

    #region Private Methods
    
    private static DataTable CreateDataTable(string tblName )
    {
      DataTable result = new DataTable();
      result.TableName = tblName;
      DataColumn col = null;

      col = new DataColumn();
      col.ColumnName = "Type";
      col.DataType = System.Type.GetType("System.String");
      result.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "DisplayValue";
      col.DataType = System.Type.GetType("System.String");
      result.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "Value";
      col.DataType = System.Type.GetType("System.String");
      result.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "DisplayName";
      col.DataType = System.Type.GetType("System.String");
      result.Columns.Add(col);

      return result;
    }

    private static void PopulateTable( CodeCompletionList list, DataTable table )
    {
      if (table == null || list == null)
      {
        return;
      }
      
      string name = String.Empty;

      DataRow row;
      foreach (string key in list.Items.Keys)
      {
        row = table.NewRow();
        row["Type"] = "ccListItem";
        row["DisplayName"] = key;
        name = String.IsNullOrEmpty(list[key]) ? key : list[key];
        row["Value"] = name;
        
        name = name.Replace("\n", " ");
        name = name.Replace("\r", " ");
        name = name.Replace("\t", " ");
        row["DisplayValue"] = name;
        table.Rows.Add(row);
      }

    }
    
    public static void FillDataSet( )
    {
      _dsLists.Clear();
      _dsLists.Tables.Clear();
      DataTable tbl = null;
      foreach(CodeCompletionList list in _lists)
      {
        tbl = CreateDataTable(list.Name);
        PopulateTable(list,tbl);
        _dsLists.Tables.Add(tbl);
      }
    }

    #endregion
  }
}
