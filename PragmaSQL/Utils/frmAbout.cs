/******************************************************************************
  File  : frmAbout.cs
  Class : frmAbout
  Description:

  Created By: Ali Özgür
  Created On: 09/28/2006 17:39:22
  Contact   : ali_ozgur@hotmail.com

  Revisions:
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PragmaSQL.WebBrowserEx;
using ComponentFactory.Krypton.Toolkit;
using PragmaSQL.Core;
using System.Diagnostics;

namespace PragmaSQL
{
    public partial class frmAbout : KryptonForm
    {
        public frmAbout()
        {
            InitializeComponent();
            label4.Text = String.Format("© 2007-{0} PragmaTouch and Ali Özgür. All rights reserved.", DateTime.Now.Year);
#if PERSONAL_EDITION
      lblEdition.Text = "Personal Edition";
#endif
        }

        public void PopulateSystemInfo()
        {
            lv.Items.Clear();

            ListViewItem item = lv.Items.Add("Computer Name");
            item.SubItems.Add(SystemInformation.ComputerName);

            item = lv.Items.Add("Username");
            item.SubItems.Add(SystemInformation.UserName);

            item = lv.Items.Add("Domain");
            item.SubItems.Add(SystemInformation.UserDomainName);

            item = lv.Items.Add("Connected to network");
            item.SubItems.Add(SystemInformation.Network ? "Yes" : "No");

            item = lv.Items.Add("Current Directory");
            item.SubItems.Add(System.Environment.CurrentDirectory);

            item = lv.Items.Add("OS Name");
            item.SubItems.Add(System.Environment.OSVersion.VersionString);

            item = lv.Items.Add("OS Platform");
            item.SubItems.Add(System.Environment.OSVersion.Platform.ToString());

            item = lv.Items.Add("OS Version");
            item.SubItems.Add(System.Environment.OSVersion.Version.ToString());

            item = lv.Items.Add("OS Service Pack");
            item.SubItems.Add(System.Environment.OSVersion.ServicePack);

            var process = Process.GetCurrentProcess();

            item = lv.Items.Add("-----------------------------------------");
            item = lv.Items.Add("THREAD COUNT");
            item.SubItems.Add(process.Threads.Count.ToString());

            item = lv.Items.Add("-----------------------------------------");
            item = lv.Items.Add("MEMORY USAGE");
            item = lv.Items.Add("Memory [NonPaged System] bytes");
            item.SubItems.Add(String.Format("{0}", process.NonpagedSystemMemorySize64));

            item = lv.Items.Add("Memory [Paged System] bytes");
            item.SubItems.Add(String.Format("{0}", process.PagedSystemMemorySize64));

            item = lv.Items.Add("Memory [Peak Paged ] bytes");
            item.SubItems.Add(String.Format("{0}", process.PeakPagedMemorySize64));

            item = lv.Items.Add("Memory [Peak Virtual] bytes");
            item.SubItems.Add(String.Format("{0}", process.PeakVirtualMemorySize64));

            item = lv.Items.Add("Memory [Peak WorkingSet] bytes");
            item.SubItems.Add(String.Format("{0}", process.PeakWorkingSet64));

            item = lv.Items.Add("Memory [Private] bytes");
            item.SubItems.Add(String.Format("{0}", process.PrivateMemorySize64));

            item = lv.Items.Add("Memory [Virtual] bytes");
            item.SubItems.Add(String.Format("{0}", process.VirtualMemorySize64));

            item = lv.Items.Add("Memory [WorkingSet] bytes");
            item.SubItems.Add(String.Format("{0}", process.WorkingSet64));

            item = lv.Items.Add("-----------------------------------------");
            item = lv.Items.Add("PROCESSOR USAGE");
            item = lv.Items.Add("Processor [Privileged Time]");
            item.SubItems.Add(String.Format("{0}", process.PrivilegedProcessorTime));

            item = lv.Items.Add("Processor [User Time]");
            item.SubItems.Add(String.Format("{0}", process.UserProcessorTime));

            item = lv.Items.Add("Processor [Total Time]");
            item.SubItems.Add(String.Format("{0}", process.TotalProcessorTime));

            item = lv.Items.Add("");
            item = lv.Items.Add("-----------------------------------------");
            item = lv.Items.Add("Loaded Assemblies");
            item.SubItems.Add("Assembly Path");

            System.Reflection.Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (System.Reflection.Assembly ass in loadedAssemblies)
            {
                //if(ass.IsDynamic || String.IsNullOrEmpty(ass.Location))
                if (ass.IsDynamic || String.IsNullOrEmpty(ass.Location))
                {
                    continue;
                }

                item = lv.Items.Add(ass.GetName().Name + " ( " + ass.GetName().Version.ToString() + " )");
                item.SubItems.Add(ass.Location);
            }

            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            System.Reflection.Assembly app = System.Reflection.Assembly.GetExecutingAssembly();
            Version v = app.GetName().Version;
            lblVersion.Text = String.Format("Version: {0}.{1}   Build: {2}", v.Major, v.Minor, v.Revision);

            //lblVersion.Text = app.GetName().Version.ToString();
        }

        public void LoadLicenceInfo()
        {
            tabControl1.TabPages.Remove(tabPage3);
        }

        public static void ShowAbout()
        {
            frmAbout frm = new frmAbout();
            frm.PopulateSystemInfo();

            frm.LoadLicenceInfo();
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:aliozgur@outlook.com?Subject=PragmaSQL");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.icsharpcode.net/OpenSource/SD/");
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.icsharpcode.net/OpenSource/SD/");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.codeproject.com/cs/miscctrl/CradsActions.asp");
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/cs/miscctrl/CradsActions.asp");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.codeproject.com/cs/library/AsynchronousCodeBlocks.asp");
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/cs/library/AsynchronousCodeBlocks.asp");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://sourceforge.net/projects/dockpanelsuite");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/dotnet/System_File_Association.asp");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.xs4all.nl/~rvanloen/");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.devincook.com/goldparser/");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/cs/miscctrl/mwcontrols.asp");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/cs/miscctrl/richtextboxextended.asp");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "aliozgur.net");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void lblThrowException_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            throw new Exception("This is a test exception thrown by the user!");
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/cs/algorithms/diffengine.asp");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://360.yahoo.com/ozgengungor");
            WebBrowserFactory.ShowWebBrowser(frm);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            //tabPage2.BackgroundImage = global::PragmaSQL.Properties.Resources.AppBackground_Stones;
            //tabPage2.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void linkLabel15_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "http://www.codeproject.com/KB/cs/WizardDemo.aspx");
            WebBrowserFactory.ShowWebBrowser(frm);
        }
    }
}