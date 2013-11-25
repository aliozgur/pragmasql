using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using WeifenLuo.WinFormsUI;
using PragmaSQL.Common;
using PragmaSQL.Database;

namespace PragmaSQL.GUI
{
  public static class DBObjectSearchFactory
  {
    public static void ShowForm(frmDbObjectSearch frm)
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

    public static frmDbObjectSearch CreateDBObjectSearchForm(NodeData data, string caption, string searchText, bool autoLoad)
    {
      if (data == null)
      {
        throw new NullParameterException("NodeData paremeter is null!");
      }

      ConnectionParams cp = data.ConnParams.CreateCopy();
      cp.InitialCatalog = data.DBName;

      frmDbObjectSearch result = new frmDbObjectSearch();
      result.InitializeForm(caption, searchText, cp);

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
