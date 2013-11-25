/********************************************************************
  Class      : SqlCommandGenerator
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PragmaSQL
{
  public static class SqlCommandGenerator
  {
    private static string QuoteSQLString( string str )
    {
      return str.Replace("'", "''");
    }

    private static string QuoteSQLString( object ostr )
    {
      return ostr.ToString().Replace("'", "''");
    }

    private static bool IsDateTime( string sDateTime )
    {
      bool bIsDateTime = false;

      try
      {
        System.DateTime.Parse(sDateTime);
        bIsDateTime = true;
      }
      catch
      {
        bIsDateTime = false;
      }

      return bIsDateTime;
    }
    
		public static SqlCommand GenerateInsertCommand( TableWrapper tblSchema, DataRow row, string tableName )
    {

			IList<ColumnWrapper> validColumns = new List<ColumnWrapper>();
      IList<object> currentVals = new List<object>();

      //1- Lets create insert part of the command
			foreach (ColumnWrapper col in tblSchema.Columns)
      {
				//Skip computed columns

				if (col.IsComputed || col.IsIdentity)
					continue;

        object cvCurrent = row[col.Name, DataRowVersion.Current];

        if (cvCurrent == null || cvCurrent.GetType() == typeof(DBNull))
        {
          continue;
        }
        currentVals.Add(cvCurrent);
        validColumns.Add(col);
      }

      //Nothing to be inserted. Exit!
      if (validColumns.Count == 0)
      {
        return null;
      }

      SqlCommand result = new SqlCommand();

      string colNames = "( ";
      string colParams = "( ";
      int paramCnt = 1;

      for (int i = 0; i < validColumns.Count; i++)
      {
        ColumnWrapper col = validColumns[i];

        if (i == 0)
        {
          colNames += "[" + col.Name+ "]";
          colParams += "@p" + paramCnt.ToString();
        }
        else
        {
          colNames += ", [" + col.Name + "]";
          colParams += ", @p" + paramCnt.ToString();
        }
        result.Parameters.AddWithValue("@p" + paramCnt.ToString(), currentVals[i]);
        paramCnt++;
      }

      colNames += " ) ";
      colParams += " ) ";

      result.CommandText = "INSERT INTO [" + tableName + "]" + colNames
        + "\n"
        + "VALUES " + colParams;
      return result;
    }

    public static SqlCommand GenerateDeleteCommand( DataColumnCollection columns, DataRow row, string tableName )
    {

      IList<string> whereParts = new List<string>();

      if (columns.Count == 0)
      {
        return null;
      }

      SqlCommand result = new SqlCommand();

      int paramCnt = 1;
      for (int i = 0; i < columns.Count; i++)
      {
        DataColumn col = columns[i];
        object cvCurrent = row[col, DataRowVersion.Original];
        if (cvCurrent == null || cvCurrent.GetType() == typeof(DBNull))
        {
          whereParts.Add("( [" + col.ColumnName + "] is null )");
        }
        else
        {
          result.Parameters.AddWithValue("@p" + paramCnt.ToString(), cvCurrent);
          whereParts.Add("( [" + col.ColumnName + "] = @p" + paramCnt.ToString() + " )");
          paramCnt++;
        }
      }

      string wherePart = String.Empty;

      for (int i = 0; i < whereParts.Count; i++)
      {
        if (i == 0)
        {
          wherePart += whereParts[i];
        }
        else
        {
          wherePart += " AND " + whereParts[i];
        }
      }

      result.CommandText = "DELETE FROM [" + tableName + "]"
        + "\n"
        + "WHERE"
        + wherePart
        + "\t";

      return result;
    }

    public static SqlCommand GenerateUpdateCommand(TableWrapper tblSchema, DataRow row, string tableName )
    {
      IList<string> whereParts = new List<string>();
      IList<string> updateParts = new List<string>();

			if (tblSchema.Columns.Count == 0)
      {
        return null;
      }

      SqlCommand result = new SqlCommand();
      string param = String.Empty;

      int paramCnt = 0;
			for (int i = 0; i < tblSchema.Columns.Count; i++)
      {
				ColumnWrapper col = tblSchema.Columns[i];
        object cvCurrent = row[col.Name, DataRowVersion.Current];
        object cvOriginal = row[col.Name, DataRowVersion.Original];

        if (cvOriginal == null || cvOriginal.GetType() == typeof(DBNull))
        {
          whereParts.Add("( [" + col.Name + "] is null )");
        }
        else
        {
          paramCnt++;
          param = "@p" + paramCnt.ToString();
          result.Parameters.AddWithValue(param, cvOriginal);
          whereParts.Add("( [" + col.Name + "] = " + param + " )");
        }


				if (!col.IsComputed && !col.IsIdentity)
				{
					if (cvCurrent == null || cvCurrent.GetType() == typeof(DBNull))
					{
						updateParts.Add("[" + col.Name + "] = NULL ");
					}
					else
					{
						paramCnt++;
						param = "@p" + paramCnt.ToString();
						result.Parameters.AddWithValue(param, cvCurrent);
						updateParts.Add("[" + col.Name + "] = " + param);
					}
				}
      }

      string wherePart = String.Empty;
      string updatePart = String.Empty;

			for (int i = 0; i < updateParts.Count; i++)
			{
				if (i == 0)
				{
					updatePart += updateParts[i];
				}
				else
				{
					updatePart += ", " + updateParts[i];
				}
			}
			
			for (int i = 0; i < whereParts.Count; i++)
      {
        if (i == 0)
        {
          wherePart += whereParts[i];
        }
        else
        {
          wherePart += " AND " + whereParts[i];
        }
      }


      result.CommandText = "UPDATE [" + tableName + "]"
        + "\n"
        + "SET "
        + updatePart
        + "\n"
        + "WHERE "
        + wherePart
        + "\t";

      return result;
    }

		public static SqlCommand GenerateSelectCommand(TableWrapper tblWrap, DataRow row, bool useComputedColsInWhere)
		{

			IList<string> whereParts = new List<string>();

			if (tblWrap == null || tblWrap.Columns.Count == 0)
			{
				return null;
			}

			SqlCommand result = new SqlCommand();


			int paramCnt = 1;
			for (int i = 0; i < tblWrap.Columns.Count; i++)
			{
				ColumnWrapper col = tblWrap.Columns[i];
				
				//Do not include computed columns in where part
				if (!useComputedColsInWhere && col.IsComputed)
					continue;

				object cvCurrent = row[col.Name, DataRowVersion.Original];
				if (cvCurrent == null || cvCurrent.GetType() == typeof(DBNull))
				{
					whereParts.Add("( [" + col.Name + "] is null )");
				}
				else
				{
					result.Parameters.AddWithValue("@p" + paramCnt.ToString(), cvCurrent);
					whereParts.Add("( [" + col.Name + "] = @p" + paramCnt.ToString() + " )");
					paramCnt++;
				}
			}
			
			string wherePart = String.Empty;
			string andVal = String.Empty;
			for (int i = 0; i < whereParts.Count; i++)
			{
				wherePart += String.IsNullOrEmpty(andVal) ? whereParts[i] : andVal + whereParts[i];
				andVal = " AND ";
			}

 
			result.CommandText = "select * from [" + tblWrap.Name + "]"
				+ "\n"
				+ "WHERE"
				+ wherePart
				+ "\t";

			return result;
		}

	}
}
