using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AxMSHelpControls;
using HtmlHelp2.Environment;
using ICSharpCode.Core;
using MSHelpControls;
using PrintOptions = MSHelpServices.HxHierarchy_PrintNode_Options;
using TSC = MSHelpControls.HxTreeStyleConstant;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace HtmlHelp2
{
  public partial class HtmlHelp2TocPad : DockContent
  {
    

    private static HtmlHelp2TocPad _current = null;
    internal static HtmlHelp2TocPad Current
    {
      get { return HtmlHelp2TocPad._current; }
      set { HtmlHelp2TocPad._current = value; }
    }

		MSHelp2TocControl help2TocControl;
		public Control Control
		{
			get { return help2TocControl; }
		}

    public HtmlHelp2TocPad( )
    {
      InitializeComponent();
      help2TocControl = new MSHelp2TocControl();
      this.Controls.Add(help2TocControl);
      help2TocControl.Parent = this;
      help2TocControl.Dock = DockStyle.Fill;
    }
   
    public void ShowPad( )
    {
      HostServicesSingleton.HostServices.ShowForm(this);
    }

		public void SyncToc(string topic)
		{
			help2TocControl.SynchronizeToc(topic);
		}

		public void GetPrevFromNode()
		{
			help2TocControl.GetPrevFromNode();
		}

		public void GetPrevFromUrl(string topic)
		{
			help2TocControl.GetPrevFromUrl(topic);
		}

		public void GetNextFromNode()
		{
			help2TocControl.GetNextFromNode();
		}

		public void GetNextFromUrl(string topic)
		{
			help2TocControl.GetNextFromUrl(topic);
		}

		public bool IsNotFirstNode
		{
			get { return help2TocControl.IsNotFirstNode; }
		}

		public bool IsNotLastNode
		{
			get { return help2TocControl.IsNotLastNode; }
		}
  }
}