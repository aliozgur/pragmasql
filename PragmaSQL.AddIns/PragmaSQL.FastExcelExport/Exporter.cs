using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using PragmaSQL.Core;

namespace PragmaSQL.FastExcelExport
{
  public static class Exporter
  {
    public static void Export(System.Data.DataTable dt)
    {
      if (dt == null)
        return;

      System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
      ApplicationClass excelApp = null;
      try
      {
        excelApp = new ApplicationClass();
        excelApp.Visible = true;

        Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);

        int sheetIndex = 0;
        object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

        for (int col = 0; col < dt.Columns.Count; col++)
        {
          rawData[0, col] = dt.Columns[col].ColumnName;
        }

        for (int col = 0; col < dt.Columns.Count; col++)
        {
          for (int row = 0; row < dt.Rows.Count; row++)
          {
            rawData[row + 1, col] = dt.Rows[row].ItemArray[col];
            System.Windows.Forms.Application.DoEvents();
          }
        }

        string finalColLetter = string.Empty;
        string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int colCharsetLen = colCharset.Length;

        if (dt.Columns.Count > colCharsetLen)
        {
          finalColLetter = colCharset.Substring(
            (dt.Columns.Count - 1) / colCharsetLen - 1, 1);
        }

        finalColLetter += colCharset.Substring(
            (dt.Columns.Count - 1) % colCharsetLen, 1);

        // Create a new Sheet
        sheetIndex++;
        Worksheet excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
          Type.Missing, excelWorkbook.Sheets.get_Item(excelWorkbook.Sheets.Count), 1, XlSheetType.xlWorksheet);

        excelSheet.Name = String.Format("Result {0}", sheetIndex);

        // Fast data export to Excel
        string excelRange = string.Format("A1:{0}{1}",
          finalColLetter, dt.Rows.Count + 1);

        excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;

        // Mark the first row as BOLD
        ((Range)excelSheet.Rows[1, Type.Missing]).Font.Bold = true;
        excelApp.Visible = true;
        excelApp.UserControl = true;
        Utils.ShowInfo("Fast export to excel completed.",MessageBoxButtons.OK);
      }
      catch (Exception ex)
      {
        GenericErrorDialog.ShowError("Error", "Can not export resultsets to excel.", ex);
        if (excelApp != null)
        {
          excelApp.Quit();
          excelApp = null;

          // Collect the unreferenced objects
          GC.Collect();
          GC.WaitForPendingFinalizers();
        }
      }
    }

    public static void ExportAll(IList<ScriptExecutionResult> executionResults)
    {
      if (executionResults == null || executionResults.Count == 0)
        return;


      System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
      ApplicationClass excelApp = null;
      try
      {
        // Create the Excel Application object
        excelApp = new ApplicationClass();
        excelApp.Visible = true;

        // Create a new Excel Workbook
        Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
        
        //for (int i = 1; i <= excelWorkbook.Sheets.Count ;i++ )
        //{
        //  Worksheet sheet = (Worksheet)excelWorkbook.Sheets.get_Item(i);
        //  sheet.Visible = XlSheetVisibility.xlSheetVeryHidden;
        //}

        int sheetIndex = 0;
        int erIndex = 0;
        int dsIndex = 0;
        
        for(int i= executionResults.Count-1; i >= 0; i--) 
        {
          ScriptExecutionResult sr = executionResults[i];
          erIndex++;
          dsIndex = 0;
          foreach (System.Data.DataSet dataset in sr.DataSets)
          {
            dsIndex++;
            sheetIndex = 0;
            System.Windows.Forms.Application.DoEvents();

            // Copy each DataTable
            foreach (System.Data.DataTable dt in dataset.Tables)
            {
              // Copy the DataTable to an object array
              object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

              // Copy the column names to the first row of the object array
              for (int col = 0; col < dt.Columns.Count; col++)
              {
                rawData[0, col] = dt.Columns[col].ColumnName;
              }

              
              // Copy the values to the object array
              for (int col = 0; col < dt.Columns.Count; col++)
              {
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                  rawData[row + 1, col] = dt.Rows[row].ItemArray[col];
                  System.Windows.Forms.Application.DoEvents();

                }
              }

              // Calculate the final column letter
              string finalColLetter = string.Empty;
              //string colCharset = "ABCDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ";
              string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

              int colCharsetLen = colCharset.Length;

              if (dt.Columns.Count > colCharsetLen)
              {
                finalColLetter = colCharset.Substring(
                  (dt.Columns.Count - 1) / colCharsetLen - 1, 1);
              }

              finalColLetter += colCharset.Substring(
                  (dt.Columns.Count - 1) % colCharsetLen, 1);

              // Create a new Sheet
              sheetIndex++;
              Worksheet excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                Type.Missing, excelWorkbook.Sheets.get_Item(excelWorkbook.Sheets.Count), 1, XlSheetType.xlWorksheet);

              excelSheet.Name = String.Format("{0}.{1} Result {2}", erIndex,dsIndex,sheetIndex);

              // Fast data export to Excel
              string excelRange = string.Format("A1:{0}{1}",
                finalColLetter, dt.Rows.Count + 1);

              excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;

              // Mark the first row as BOLD
              ((Range)excelSheet.Rows[1, Type.Missing]).Font.Bold = true;
            }

          }
        }

        excelApp.Visible = true;
        excelApp.UserControl = true;
        Utils.ShowInfo("Fast export to excel completed.",MessageBoxButtons.OK);

      }
      catch (Exception ex)
      {
        GenericErrorDialog.ShowError("Error", "Can not export resultsets to excel.", ex);
        if (excelApp != null)
        {
          excelApp.Quit();
          excelApp = null;

          // Collect the unreferenced objects
          GC.Collect();
          GC.WaitForPendingFinalizers();
        }
      }
    }

  }
}
