using System;
using System.Data;

namespace PragmaSQL.Core
{
  public class HtmlDataSetConverter : DataSetConverter
  {

    public override object Convert(DataSet dataset)
    {
      string output = "";
      foreach (DataTable table in dataset.Tables)
      {
        output += "\n" + (string)Convert(table);
      }
      return output;
    }

    public override object Convert(DataTable table)
    {
      string output = "";

      output += "<table>";
      output += "\n\t<tr>";
      foreach (DataColumn col in table.Columns)
      {
        output += "\n\t\t<th>" + col.Caption + "</th>";
      }
      output += "\n\t</tr>";
      foreach (DataRow row in table.Rows)
      {
        output += "\n\t<tr>";
        for (int i = 0; i < table.Columns.Count; i++)
        {
          output += "\n\t\t<td>";
          output += "" + System.Convert.ToString(row[i]);
          output += "</td>";
        }
        output += "\n\t</tr>";
      }
      output += "\n</table>";

      return output;
    }
  }
}
