/********************************************************************
  Class      : ObjectChangeHistoryViewerFactory
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using WeifenLuo.WinFormsUI.Docking;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class ObjectChangeHistoryViewerFactory
  {
    public static void ShowViewer(frmObjectChangeHistoryViewer frm)
    {
      if(frm == null)
      {
        return;
      }

      if ( Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
      {
        frm.MdiParent = Program.MainForm;
        frm.Show();
				frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			}
      else if ( Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.DockingWindow)
      {
        frm.Show();
      }
      else
      {
        frm.Show(Program.MainForm.DockPanel);
      }
    }

    public static frmObjectChangeHistoryViewer CreateViewer(string caption)
    {
      frmObjectChangeHistoryViewer result = new frmObjectChangeHistoryViewer();
      if(!String.IsNullOrEmpty(caption))
      {
        result.Text = caption;
        result.TabText = result.Text;
      }
      return result;
    }

    public static frmObjectChangeHistoryViewer CreateViewer()
    {
      return CreateViewer(String.Empty);
    }

    public static frmObjectChangeHistoryViewer CreateViewer(string serverName, string databaseName)
    {
      frmObjectChangeHistoryViewer result = CreateViewer();
      result.InitializeServerAndDatabase(serverName,databaseName);
      return result;
    }
  }
}
