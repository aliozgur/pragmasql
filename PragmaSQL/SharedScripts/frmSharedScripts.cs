using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public partial class frmSharedScripts : DockContent
  {

    public ucSharedScripts SharedScriptsControl
    {
      get
      {
        return ucSharedScripts1;
      }
    }
    
    public frmSharedScripts()
    {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif
      InitializeComponent();
      ucSharedScripts1.InitializeAddInSupport();
    }

    private void frmSharedScripts_FormClosed( object sender, FormClosedEventArgs e )
    {
      ucSharedScripts1.FireAfterSharedScriptsViewerClosed();
    }
  }
}