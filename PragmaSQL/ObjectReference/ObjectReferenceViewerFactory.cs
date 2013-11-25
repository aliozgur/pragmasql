/********************************************************************
  Class      : ObjectReferenceListFactory
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
  public static class ObjectReferenceViewerFactory
  {
    public static void ShowViewer(frmObjectReferencesViewer frm)
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

    public static frmObjectReferencesViewer CreateObjectReferencesViewer(string caption, string objName, ConnectionParams cp, string dbName, RefDetail refDetail, bool autoLoad)
    {
      frmObjectReferencesViewer frm = new frmObjectReferencesViewer();
      frm.TabText = caption;
      frm.Text = caption;
      frm.ObjectRefList.Initialize(objName,cp,dbName,refDetail);
      if(autoLoad)
      {
        frm.ObjectRefList.LoadData();
      }
      
      return frm;
    }
  }
}
