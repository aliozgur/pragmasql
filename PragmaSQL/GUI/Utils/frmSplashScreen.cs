/********************************************************************
  Class      : frmSplashScreen
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.GUI
{
  public partial class frmSplashScreen : Form
  {
    public frmSplashScreen( )
    {
      InitializeComponent();
    }


    public void ShowAssemblies( object sender, AssemblyLoadEventArgs e )
    {
      lblLoad.Text = e.LoadedAssembly.GetName().Name;
      Application.DoEvents();
    }

  }
}