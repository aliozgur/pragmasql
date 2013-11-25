using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using PragmaSQL.WebBrowserEx;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class HelpProvider
  {
    private static frmWebBrowser _webBrowser = null;
    private static HelpSettings _helpSettings = null;
    
    public static void ProvideHelpFor(string wordAtCursor)
    {
      if (ConfigHelper.Current == null || ConfigHelper.Current.HelpSettings == null)
      {
        MessageBox.Show("Help not configured properly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      _helpSettings = ConfigHelper.Current.HelpSettings;
      switch (_helpSettings.ProviderType)
      {
        case HelpProviderType.CustomOnline:
          ProvideCustomOnline(wordAtCursor);
          break;
        case HelpProviderType.Help1:
          ProvideHelp1(wordAtCursor);
          break;
        case HelpProviderType.Help2:
          ProvideHelp2(wordAtCursor);
          break;
        default:
          throw new Exception("HelpProviderType not supported!");
      }
    }

    private static void ProvideHelp1(string wordAtCursor)
    {
      string keywords = _helpSettings.Help1.Parameters.ToLowerInvariant().Replace("$wordatcursor$", wordAtCursor);
      Help.ShowHelp(null, _helpSettings.Help1.Url, _helpSettings.Help1.NavigatorCommand, keywords);
    }

    private static void ProvideHelp2(string wordAtCursor)
    {
      System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
      psi.FileName = _helpSettings.Help2.DocumentExplorerPath;
      psi.Arguments = " /helpcol "
        + _helpSettings.Help2.HelpCollection
        + " /filter "
        + "\"" + _helpSettings.Help2.Filter + "\""
        + " /LaunchFKeywordTopic "
        + "\"" + wordAtCursor + "\"";
      System.Diagnostics.Process.Start(psi);
    }

    private static void ProvideCustomOnline(string wordAtCursor)
    {
      string url = String.Empty;
      if (_helpSettings.CustomOnline.UsePragmaSQLWebBrowser)
      {
        if (_webBrowser == null)
        {
          _webBrowser = WebBrowserFactory.CreateAndBrowse();
          _webBrowser.FormClosed += new FormClosedEventHandler(_webBrowser_FormClosed);
        }

        url = _helpSettings.CustomOnline.Url.ToLowerInvariant().Replace("$wordatcursor$", wordAtCursor);
        url = url.Replace(" ","%20");
        _webBrowser.ManualNavigate(url);
        WebBrowserFactory.ShowWebBrowser(_webBrowser);
      }
      else
      {
        url = _helpSettings.CustomOnline.Url.ToLowerInvariant().Replace("$wordatcursor$", wordAtCursor);      
        url = url.Replace(" ","%20");
        System.Diagnostics.Process.Start(url);
      }
    }

    private static void _webBrowser_FormClosed(object sender, FormClosedEventArgs e)
    {
      _webBrowser = null;
    }

  }
}
