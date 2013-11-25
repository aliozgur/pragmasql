/********************************************************************
  Class WebBrowserService
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;
using PragmaSQL.WebBrowserEx;

namespace PragmaSQL
{
  public class WebBrowserService:IWebBrowserService
  {
    #region IWebBrowserService Members

    public void Navigate( string caption, string url )
    {
      frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(caption, url);
      WebBrowserFactory.ShowWebBrowser(frm);
    }

    public void Navigate( string url )
    {
      this.Navigate(String.Empty, url);
    }

    public void OpenFile( string fileName )
    {
      frmWebBrowser frm = WebBrowserFactory.OpenFile(fileName);
      WebBrowserFactory.ShowWebBrowser(frm);
    }

    public void OpenFile( )
    {
      this.OpenFile(String.Empty);
    }

		private IWebBrowser Create(string caption, string url, bool navigate)
		{
			frmWebBrowser frm = null;
			if(navigate)
				frm = WebBrowserFactory.CreateAndBrowse(caption, url);
			else
				frm = WebBrowserFactory.Create(caption);

			if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
			{
				frm.MdiParent = Program.MainForm;
				frm.Show();
			}
			else
			{
				frm.Show(Program.MainForm.DockPanel);
			} return frm;		
		}

    public IWebBrowser CreateAndBrowse( string caption, string url )
    {
			return Create(caption, url, true);
    }

    public IWebBrowser CreateAndBrowse( string url )
    {
      return this.CreateAndBrowse(String.Empty, url);
    }
    
    public IWebBrowser Create( )
    {
			return Create(String.Empty, String.Empty, false);
    }

		public IWebBrowser Create(string caption)
		{
			return Create(caption,String.Empty,false);
		}

    #endregion
  }
}
