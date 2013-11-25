using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace HtmlHelp2
{
  public partial class HtmlHelp2IndexPad : DockContent,IPad
  {
    private static HtmlHelp2IndexPad _current = null;
    internal static HtmlHelp2IndexPad Current
    {
      get { return HtmlHelp2IndexPad._current; }
      set { HtmlHelp2IndexPad._current = value; }
    }


    public HtmlHelp2IndexPad( )
    {
      InitializeComponent();
      help2IndexControl = new MSHelp2IndexControl();
      help2IndexControl.Parent = this;
      this.Controls.Add(help2IndexControl);
      help2IndexControl.Dock = DockStyle.Fill;
      help2IndexControl.Show();
      help2IndexControl.BringToFront();
    }

    MSHelp2IndexControl help2IndexControl;
    public Control Control
    {
      get { return help2IndexControl; }
    }

    public void ShowPad( )
    {
      HostServicesSingleton.HostServices.ShowForm(this);
    }

    public void Search( string keyword )
    {
      if (help2IndexControl == null)
      {
        throw new Exception("Help2 index control is null");
      }

      help2IndexControl.PerformSearch(keyword);
    }
  }
}