/************************************************************************************************************
 * Ali Özgür
 * ali_ozgur@hotmail.com
 * www.pragmasql.com 
 * 
 * Source code included in this file can not be used without written
 * permissions of the owner mentioned above. 
 * All rigths reserver
 * Copyright PragmaSQL 2007 
 ************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using WeifenLuo.WinFormsUI.Docking;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public class ObjectGroupingFormFactory
  {
    public static void ShowForm(frmObjectGrouping frm)
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

    public static frmObjectGrouping CreateObjectGroupingForm(string caption,ConnectionParams connParams, string dbName)
    {

#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif
      
      if(connParams == null)
      {
        throw new NullParameterException("Connection params is null!");
      }

      frmObjectGrouping frm = new frmObjectGrouping();
      string tmpCaption = caption;
      if(String.IsNullOrEmpty(tmpCaption) && connParams != null)
      {
        tmpCaption = "Object Grouping " + connParams.Server + " {" + connParams.Database + "}";
      }
      
      frm.Text = tmpCaption;
      frm.TabText = tmpCaption;
      ConnectionParams copyConn = connParams.CreateCopy();
      copyConn.Database = dbName;
      if(!frm.InitializeObjectGrouping(copyConn) )
      {
        frm.Dispose();
        frm = null;
      }
      
      return frm;

    }
  }
}
