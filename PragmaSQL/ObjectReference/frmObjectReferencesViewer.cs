/********************************************************************
  Class      : frmObjectReferences
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace PragmaSQL
{
  public partial class frmObjectReferencesViewer : DockContent
  {
    public frmObjectReferencesViewer( )
    {
      InitializeComponent();
    }

    public ObjectRefList ObjectRefList
    {
      get
      {
        return objectRefList1;
      }
    }

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    { 
      Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Program.MainForm.CloseDocuments(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Program.MainForm.CloseDocuments(this);
    }
  }
}