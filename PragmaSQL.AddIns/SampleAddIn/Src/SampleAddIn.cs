/********************************************************************
  Class SampleAddIn
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL.AddIns.SampleAddIn
{
  public static class SampleAddIn
  {
    private static frmSampleAddIn _mainForm = null;
    public static frmSampleAddIn MainForm
    {
      get { return _mainForm; }
      set { _mainForm = value; }
    }


    public static void ShowForm( )
    {
      if (_mainForm == null)
      {
        _mainForm = new frmSampleAddIn();
        _mainForm.StartPosition = FormStartPosition.CenterScreen;
        //HostServicesSingleton.HostServices.ShowForm(_mainForm, AddInDockState.Float);
        _mainForm.Show();
        _mainForm.TopMost = true;
      }
      else
      {
        _mainForm.Activate();
      }
    }

    public static void ShowNewFormEveryTime( )
    {
      frmSampleAddIn frm = new frmSampleAddIn();
      frm.StartPosition = FormStartPosition.CenterScreen;
      frm.Show();
    }

  }
}
