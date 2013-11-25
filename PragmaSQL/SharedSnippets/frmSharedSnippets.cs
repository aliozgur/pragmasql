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
  public partial class frmSharedSnippets : DockContent
  {
    public ucSharedSnippets SharedSnippetsControl
    {
      get
      {
        return ucSharedSnippets1;
      }
    }

    public frmSharedSnippets()
    {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif
      InitializeComponent();
      ucSharedSnippets1.InitializeAddInSupport();
    }

    private void frmSharedSnippets_FormClosed( object sender, FormClosedEventArgs e )
    {
      ucSharedSnippets1.FireAfterSharedSnippetsViewerClosed();
    }
  }
}