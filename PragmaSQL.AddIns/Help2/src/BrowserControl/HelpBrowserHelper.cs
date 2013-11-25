// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mathias Simmack" email="mathias@simmack.de"/>
//     <version>$Revision: 1965 $</version>
// </file>

namespace HtmlHelp2
{
	// With a big "Thank you" to Robert_G (Delphi-PRAXiS)
	
	using System;
	using System.Security.Permissions;
	using System.Windows.Forms;

  using MSHelpServices;
  using PragmaSQL.Core;

	public static class WebBrowserHelper
	{
    static string _browserCaption = String.Empty;

    static IWebBrowser _webBrowser = null;
    public static WebBrowser WebBrowser
    {
      get { return WebBrowserHelper._webBrowser.WebBrowser; }
    }

    static bool hiliteMatches;
		static IHxTopic lastTopic;

		public static void OpenHelpView(IHxTopic topic)
		{
			if (topic == null)
			{
				throw new ArgumentNullException("topic");
			}
			OpenHelpView(topic.URL, null, false);
		}

		public static void OpenHelpView(IHxTopic topic, bool hiliteMatchingWords)
		{
			if (topic == null)
			{
				throw new ArgumentNullException("topic");
			}
			OpenHelpView(topic.URL, topic, hiliteMatchingWords);
		}

		public static void OpenHelpView(string topicLink)
		{
			OpenHelpView(topicLink, null, false);
		}

		public static void OpenHelpView(string topicLink, bool hiliteMatchingWords)
		{
			OpenHelpView(topicLink, null, hiliteMatchingWords);
		}

		public static void OpenHelpView(string topicLink, IHxTopic topic, bool hiliteMatchingWords)
		{
			hiliteMatches = hiliteMatchingWords;
			lastTopic = topic;
      _browserCaption = topic != null ? topic.get_Title(HxTopicGetTitleType.HxTopicGetHTMTitle, HxTopicGetTitleDefVal.HxTopicGetTitleFullURL) : topicLink;
      if (_webBrowser == null)
      {
        _webBrowser = HostServicesSingleton.HostServices.WebBrowserService.CreateAndBrowse(_browserCaption);
				_webBrowser.AfterClosed += new EventHandler(_webBrowser_AfterClosed);
      }

      if (_webBrowser == null)
      {
        return;
      }

			HostServicesSingleton.HostServices.ShowForm((_webBrowser as Form), AddInDockState.Document);
			
			_webBrowser.Stop();
			_webBrowser.Navigate(topicLink);
		}

    static void _webBrowser_AfterCompleted( object sender, EventArgs e )
    {
      IWebBrowser browser = sender as IWebBrowser;
      if (browser == null)
      {
        return;
      }

      browser.Caption = _browserCaption;
    }

    static void _webBrowser_AfterClosed( object sender, EventArgs e )
    {
      _webBrowser.AfterClosed -= new EventHandler(_webBrowser_AfterClosed);
			_webBrowser = null;
    }

		[PermissionSet(SecurityAction.LinkDemand, Name="Execution")]
		public static void HighlightDocument()
		{
      if (hiliteMatches && lastTopic != null && _webBrowser != null)
			{
				lastTopic.HighlightDocument(_webBrowser.DomDocument);
			}
		}
	}
}
