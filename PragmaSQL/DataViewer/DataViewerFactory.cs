/********************************************************************
  Class      : TableEditorFactory
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class DataViewerFactory
  {

    public static void ShowDataViewer( frmDataViewer frm )
    {
      if (frm == null)
      {
        return;
      }

      if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
      {
        frm.MdiParent = Program.MainForm;
        frm.Show();
				frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			}
      else
      {
        frm.Show(Program.MainForm.DockPanel);
      }
    }

    public static frmDataViewer CreateDataViewer( NodeData data, string caption, string script, bool isReadonly, bool autoLoad )
    {
      if (data == null)
      {
        throw new NullParameterException("NodeData paremeter is null!");
      }

      frmDataViewer result = CreateDataViewer(data.ConnParams,data.DBName,data.Name,caption,script,isReadonly,autoLoad);
      return result;
    }


    public static frmDataViewer CreateDataViewer( ConnectionParams connParams, string dbName, string tableName, string caption, string script, bool isReadonly, bool autoLoad )
    {
      if (connParams == null)
      {
        throw new NullParameterException("connParams paremeter is null!");
      }

      ConnectionParams cp = connParams.CreateCopy();
      cp.Database = dbName;

      string windowId = TableDataEditorManager.ProduceWindowId(tableName, cp.Server, dbName);
      if (TableDataEditorManager.Contains(windowId))
        return TableDataEditorManager.Get(windowId);


      frmDataViewer result = new frmDataViewer();
      result.TableName = tableName;
      result.InitializeDataViewer(windowId, caption, script, isReadonly, cp);
      if (autoLoad)
      {
        try
        {
          result.LoadData(true);
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
