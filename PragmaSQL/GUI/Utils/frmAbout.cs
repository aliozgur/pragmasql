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

namespace PragmaSQL.GUI
{
  public partial class frmAbout : Form
  {
    public frmAbout( )
    {
      InitializeComponent();
    }

    public void PopulateSystemInfo( )
    {
      lv.Items.Clear();

      ListViewItem item = lv.Items.Add("Computer Name");
      item.SubItems.Add(SystemInformation.ComputerName);

      item = lv.Items.Add("Username");
      item.SubItems.Add(SystemInformation.UserName);
      
      item = lv.Items.Add("Domain");
      item.SubItems.Add(SystemInformation.UserDomainName);

      item = lv.Items.Add("Connected to network");
      item.SubItems.Add(SystemInformation.Network ? "Yes": "No");

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

      item = lv.Items.Add("");
      item = lv.Items.Add("Loaded Assemblies (Name)");
      item.SubItems.Add("Loaded Assemblies (Full Name)");

      System.Reflection.Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

      foreach (System.Reflection.Assembly ass in loadedAssemblies)
      {
        item = lv.Items.Add(ass.GetName().Name + " ( " + ass.GetName().Version.ToString() + " )");
        item.SubItems.Add(ass.FullName);
      }

      lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

      System.Reflection.Assembly app = System.Reflection.Assembly.GetExecutingAssembly();
      lblVersion.Text = app.GetName().Version.ToString();
    }

    public static void ShowAbout( )
    {
      frmAbout frm = new frmAbout();
      frm.PopulateSystemInfo();
      frm.ShowDialog();
    }

    private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
    {
      System.Diagnostics.Process.Start("mailto:pragmasql@gmail.com?Subject=PragmaSQL");
    }
  }
}