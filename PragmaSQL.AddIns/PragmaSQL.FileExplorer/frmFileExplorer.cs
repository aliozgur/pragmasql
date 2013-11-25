using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
//using FileBrowser;

namespace PragmaSQL.FileExplorer
{
	public partial class frmFileExplorer : DockContent
	{
		//private Browser _browser = null;

		public frmFileExplorer()
		{
			InitializeComponent();
			CreateFileBrowser();
		}

		//private void CreateFileBrowser()
		//{
		//  _browser = new Browser();
		//  this.Controls.Add(_browser);
		//  _browser.Parent = this;
		//  _browser.Dock = DockStyle.Fill;
		//}
	}
}