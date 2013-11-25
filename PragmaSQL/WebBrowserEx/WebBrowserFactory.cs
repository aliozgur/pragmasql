/********************************************************************
  Class      : WebBrowserFactory
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace PragmaSQL.WebBrowserEx
{
	internal static class WebBrowserFactory
	{
		private static int _instanceCnt = 0;
		internal static WimdowNumerator Numerator = null;

		static WebBrowserFactory()
		{
			Numerator = new WimdowNumerator();
		}

		internal static void ResetNumerator()
		{
			if (Numerator.WindowCount != 0)
				return;
			_instanceCnt = 0;
			Numerator.ClearReclaimedNumbers();
		}

		internal static void ShowWebBrowser(frmWebBrowser frm)
		{
			if (frm == null)
			{
				return;
			}

			if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
			{
				frm.MdiParent = Program.MainForm;
				frm.Show();
				frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			}
			else
			{
				frm.Show(Program.MainForm.DockPanel,DockState.Document);
			}
		}

		internal static frmWebBrowser CreateAndBrowse()
		{
			return CreateAndBrowse(String.Empty, String.Empty);
		}

		internal static frmWebBrowser CreateAndBrowse(string caption, string url)
		{
			int? windowNo = Numerator.NextNumber;
			if (!windowNo.HasValue)
			{
				_instanceCnt++;
				windowNo = _instanceCnt;
			}

			frmWebBrowser frm = new frmWebBrowser();
			string tmpCaption = String.Format("WebBrowser {0}", windowNo);

			if (!String.IsNullOrEmpty(caption))
			{
				tmpCaption = caption;
			}

			frm.Text = tmpCaption;
			frm.TabText = tmpCaption;
			frm.WindowNo = windowNo;

			string tmpUrl = url;

			if (String.IsNullOrEmpty(url) && ConfigHelper.Current != null && ConfigHelper.Current.GeneralOptions != null)
			{
				tmpUrl = ConfigHelper.Current.GeneralOptions.WebBrowser_HomeUrl;
			}


			//if (String.IsNullOrEmpty(url))
			//{
			//  if (ConfigHelper.Current != null && ConfigHelper.Current.GeneralOptions != null)
			//  {
			//    tmpUrl = ConfigHelper.Current.GeneralOptions.WebBrowser_HomeUrl;
			//    if (String.IsNullOrEmpty(tmpUrl))
			//    {
			//      tmpUrl = Properties.Resources.DefaultHomePage;
			//    }
			//  }
			//  else
			//  {
			//    tmpUrl = Properties.Resources.DefaultHomePage;
			//  }
			//}

			if (!String.IsNullOrEmpty(tmpUrl))
				frm.ManualNavigate(tmpUrl);

			return frm;

		}

		internal static frmWebBrowser Create()
		{
			return Create(String.Empty);
		}

		internal static frmWebBrowser Create(string caption)
		{
			int? windowNo = Numerator.NextNumber;
			if (!windowNo.HasValue)
			{
				_instanceCnt++;
				windowNo = _instanceCnt;
			}
			frmWebBrowser frm = new frmWebBrowser();
			string tmpCaption = String.Format("WebBrowser {0}", windowNo);

			if (!String.IsNullOrEmpty(caption))
			{
				tmpCaption = caption;
			}

			frm.Text = tmpCaption;
			frm.TabText = tmpCaption;
			frm.WindowNo = windowNo;

			return frm;

		}

		internal static frmWebBrowser OpenFile(string fileName)
		{
			frmWebBrowser frm = new frmWebBrowser();
			if (!frm.OpenFile(fileName))
			{
				frm.Dispose();
				frm = null;
			}

			return frm;
		}
	}
}
