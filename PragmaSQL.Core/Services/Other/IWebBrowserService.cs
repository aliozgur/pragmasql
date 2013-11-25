using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace PragmaSQL.Core
{
  public interface IWebBrowserService
  {
    void Navigate( string caption, string url );
    void Navigate( string url );
    void OpenFile( string fileName );
    void OpenFile( );
    IWebBrowser CreateAndBrowse( string caption,string url );
    IWebBrowser CreateAndBrowse( string url );
    IWebBrowser Create( );
		IWebBrowser Create( string caption );
  }
}
