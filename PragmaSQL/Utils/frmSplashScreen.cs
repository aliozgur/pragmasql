/********************************************************************
  Class      : frmSplashScreen
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
using System.Reflection;
using ICSharpCode.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public partial class frmSplashScreen : KryptonForm
  {
    public frmSplashScreen( )
    {
      InitializeComponent();
      label2.Text = String.Format("© 2007-{0} PragmaTouch and Ali Özgür. All rights reserved.", DateTime.Now.Year);
#if PERSONAL_EDITION
      lblEdition.Text = "Personal Edition";
#endif
      Assembly app = Assembly.GetExecutingAssembly();
      Version v = app.GetName().Version;

      lblVersion.Text = String.Format("Version: {0}.{1}   Build: {2}",v.Major,v.Minor,v.Revision );
			this.TopLevel = true;
		}


    public void ShowAssemblies( object sender, AssemblyLoadEventArgs e )
    {
      //lblLoad.Text = "Loading " + e.LoadedAssembly.GetName().Name;
      Application.DoEvents();
    }

		public static void PrintStatus(string msg)
		{
			if (Program.splashScreen == null)
				return;
			
			if (!Program.splashScreen.Visible)
				Program.splashScreen.Show();

			Program.splashScreen.lblStatusMsg.Text = msg;
			LoggingService.Info(msg);

			Application.DoEvents();
		}

		public static void ClearStatus()
		{
			if (Program.splashScreen == null)
				return;
			Program.splashScreen.lblStatusMsg.Text = String.Empty;
			Application.DoEvents();
		}

		public static void ShowSplash()
		{
			if (Program.splashScreen == null)
				return;

			Program.splashScreen.Show();
			Application.DoEvents();
		}

		public static void HideSplash()
		{
			if (Program.splashScreen == null)
				return;

			Program.splashScreen.Hide();
			Application.DoEvents();
		}

		public static void SendSplashToBack()
		{
			if (Program.splashScreen == null)
				return;

			Program.splashScreen.TopMost = false;
			Program.splashScreen.SendToBack();
		}

		public static void BringSplashToFront()
		{
			if (Program.splashScreen == null)
				return;

			Program.splashScreen.TopMost = true;
			Program.splashScreen.BringToFront();
		}

		public static void CloseSplash()
		{
			if (Program.splashScreen == null)
				return;

			Program.splashScreen.Close();
			Program.splashScreen = null;
			Application.DoEvents();
		}
  }
}