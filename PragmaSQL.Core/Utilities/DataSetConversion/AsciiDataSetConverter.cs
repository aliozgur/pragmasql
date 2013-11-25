using System;
using System.Data;
using System.IO;

namespace PragmaSQL.Core
{

  /// <summary>
  /// Takes a dataset or datatable and converts it to fixed width string
  /// </summary>
  public class AsciiDataSetConverter : DataSetConverter
  {
    private readonly int MAX_ROWS_TO_SAMPLE = 20;
    private readonly string COLUMN_SEPARATOR = " ";

    public override object Convert(DataSet dataset)
    {
      string output = "";
      foreach (DataTable table in dataset.Tables)
      {
        output += "\r\n" + (string)Convert(table);
      }
      return output;
    }

    public override object Convert(DataTable table)
    {
      /*
      StringWriter sw = new StringWriter();
      table.WriteXml(sw, XmlWriteMode.IgnoreSchema, false);

      return sw.ToString();
      */


      string output = "";
      int[] colWidths = GetColumnWidthFromSample(table, MAX_ROWS_TO_SAMPLE);

      //add header to stream
      int i = 0;
      foreach (DataColumn col in table.Columns)
      {
        output += FormatCell(col.Caption, colWidths[i], 1) + COLUMN_SEPARATOR;
        i++;
      }

      //output header underline
      i = 0;
      output += "\r\n";
      foreach (DataColumn col in table.Columns)
      {
        output += "".PadRight(colWidths[i], '=') + COLUMN_SEPARATOR;
        i++;
      }

      //output rows
      foreach (DataRow row in table.Rows)
      {
        output += "\r\n";
        for (int j = 0; j < table.Columns.Count; j++)
        {
          string cellVal = System.Convert.ToString(row[j]);
          if (row[j] == DBNull.Value)
          {
            cellVal = "NULL";
          }
          output += "" + FormatCell(cellVal, colWidths[j], 1) + COLUMN_SEPARATOR;
        }

      }
      return output;
    }

    private int[] GetColumnWidthFromSample(DataTable table, int rowsToSample)
    {
      int[] widths = new int[table.Columns.Count];

      //set minimum width
      for (int i = 0; i < widths.Length; i++)
      {
        widths[i] = table.Columns[i].Caption.Length;
      }

      int count = 0;
      foreach (DataRow row in table.Rows)
      {
        for (int i = 0; i < table.Columns.Count; i++)
        {

          string thisRowText = System.Convert.ToString(row[i]);
          if (thisRowText.IndexOf('\n') > -1)
          {
            thisRowText = thisRowText.Substring(0, thisRowText.IndexOf('\n') - 1);
          }

          if (System.Convert.ToString(row[i]).Length > widths[i])
          {
            widths[i] = thisRowText.Length;
          }
        }

        //TODO: sample the last column just for good measure. This means that 
        //we're more likely to get it right if the set is ordered.

        count++;
        if (count > rowsToSample)
        {
          return widths;
        }
      }
      return widths;
    }

    /// <summary>
    /// Format text to a given height and width. Height not working yet!
    /// </summary>
    /// <param name="text"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string FormatCell(string text, int width, int height)
    {
      string output = text;
      int firstBreak = output.IndexOf('\n') - 1;

      if (firstBreak > -1)
      {
        output = output.Substring(0, firstBreak);
      }

      //trim if too long
      if (output.Length > width)
      {
        output = output.Substring(0, width);
      }

      output = output.PadRight(width, ' ');

      return output;
    }
  }
}
