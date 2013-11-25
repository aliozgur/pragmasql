/********************************************************************
  Class      : TableEditorFactory
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI;
using PragmaSQL.Database;
using PragmaSQL.Common;

namespace PragmaSQL.GUI
{
  public static class TableEditorFactory
  {

    public static void ShowEditor( frmTableEditor frm )
    {
      if (frm == null)
      {
        return;
      }

      if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyles.SystemMdi)
      {
        frm.MdiParent = Program.MainForm;
        frm.Show();
      }
      else
      {
        frm.Show(Program.MainForm.DockPanel);
      }
    }

    public static frmTableEditor CreateTableEditor( NodeData data, string caption, string script, bool isReadonly, bool autoLoad )
    {
      if (data == null)
      {
        throw new NullParameterException("NodeData paremeter is null!");
      }

      ConnectionParams cp = data.ConnParams.CreateCopy();
      cp.InitialCatalog = data.DBName;

      frmTableEditor result = new frmTableEditor();
      result.TableName = data.Name;
      result.InitializeDataEditor(caption, script, isReadonly, cp);

      if (autoLoad)
      {
        try
        {
          result.LoadData();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Can not load data!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          result.Close();
          result.Dispose();
          result = null;
        }
      }
      return result;
    }
  }
}
