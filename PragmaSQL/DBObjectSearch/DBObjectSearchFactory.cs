using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class DBObjectSearchFactory
  {
    public static void ShowForm(frmDbObjectSearch frm)
    {
      if (frm == null || frm.IsDisposed)
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

    public static frmDbObjectSearch CreateDBObjectSearchForm(NodeData data, string caption, string searchText)
    {
      if (data == null)
      {
        throw new NullParameterException("Data parameter is null!");
      }
      return CreateDBObjectSearchForm(data.ConnParams, data.DBName, caption, searchText);
    }

    public static frmDbObjectSearch CreateDBObjectSearchForm(ConnectionParams connParams, string dbName, string caption, string searchText)
    {
      if (connParams == null)
      {
        throw new NullParameterException("Connection params is null!");
      }

      ConnectionParams cp = connParams.CreateCopy();
      if(!String.IsNullOrEmpty(dbName))
        cp.Database = dbName;

      frmDbObjectSearch result = new frmDbObjectSearch();
      result.InitializeForm(caption, cp);

      if (!String.IsNullOrEmpty(searchText))
      {
        try
        {
          result.AddSearchTextCriteria(searchText);
          result.PerformSearch();
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
    
    public static IList<ObjectInfo> SearchAndSelectObjectFromDb(ConnectionParams cp, string caption, bool autoLoad)
    {
      if (cp == null)
      {
        throw new NullParameterException("Connection not specified!");
      }

      frmDbObjectSearch frm = new frmDbObjectSearch();
      frm.InitializeForm(caption, cp);

      if (autoLoad)
      {
        try
        {
          frm.PerformSearch();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Can not load data!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          frm.Close();
          frm.Dispose();
          frm = null;
          return null;
        }
      }
      
      frm.IsDialog = true;
			frm.RowSelectAllowed = true;

      frm.ShowDialog();
      return frm.GetSelectedObjects();
    }
  }
}
