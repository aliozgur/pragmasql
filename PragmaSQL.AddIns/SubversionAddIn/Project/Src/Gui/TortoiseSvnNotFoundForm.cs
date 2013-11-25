using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PragmaSQL.Svn
{
  public partial class TortoiseSvnNotFoundForm : SvnDialogBase
  {
    public TortoiseSvnNotFoundForm( )
    {
      InitializeComponent();
    }

    private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
    {
      Process.Start("http://www.tortoisesvn.tigris.org");
    }
  }
}