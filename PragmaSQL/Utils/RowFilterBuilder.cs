using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Codeproject
{
    static public class RowFilterBuilder
    {

        public static string BuildMultiColumnFilter(string filterExpression, DataTable table)
        {
            return BuildMultiColumnFilter(filterExpression, table.Columns);
        }

        public static string BuildMultiColumnFilter(string filterExpression, DataView view)
        {
            return BuildMultiColumnFilter(filterExpression, view.Table);
        }

        public static string BuildMultiColumnFilter(string filterExpression, DataColumnCollection coloumns)
        {
            System.Collections.Specialized.StringCollection coloumNames = new System.Collections.Specialized.StringCollection();

            foreach (DataColumn col in coloumns)
            {
                coloumNames.Add(col.ColumnName);
            }

            return BuildMultiColumnFilter(filterExpression, coloumNames);
        }


        public static string BuildMultiColumnFilter(string filterExpression, System.Collections.Specialized.StringCollection coloumns)
        {
            string[] coloumnNames = new string[coloumns.Count];
            coloumns.CopyTo(coloumnNames, 0);
            return BuildMultiColumnFilter(filterExpression, coloumnNames);
        }
        
        /// <summary>
        /// Builds a string that can be used for DataViews as Row filter.
        /// You might pass 2 arguments: a string that repressents the expressiuon for the filter, seperated by blancs
        /// and an array of coloumn names
        /// </summary>
        /// <param name="filterExpression"></param>
        /// An Expression that might be used for filter. for Example: "Thomas Haller"
        /// <param name="coloumns"></param>
        /// An String Array with the Name of Coloumns
        /// for Example "Prename, Name"
        /// <returns></returns>
        public static string BuildMultiColumnFilter(string filterExpression, string[] coloumnsString )
        {
            filterExpression = filterExpression.Replace("'", "''");
            
            string result = "";
            string[] filters = filterExpression.Split(" ".ToCharArray());


            for (int i = 0; i < filters.Length; i++)
            {
                if (i!=0)
                {
                    result += " AND ";
                }
                result += "(";
                string filter = filters[i];
                for (int j = 0; j < coloumnsString.Length; j++)
                {
                    string column = coloumnsString[j];
                    //we need an prefix "OR" for every operator - but not for the first one
                    if (j != 0)
	                {
                        result += " OR ";
	                }

                    result += " (CONVERT( [" + column + "], 'System.String') like '%" + filter + "%' )";
                }

                result += ")";
                
            }
            return result;
        }


    }
}
