/********************************************************************
  Class WebBrowserExtendedEvents
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PragmaSQL.WebBrowserEx
{
     #region The Implementation of DWebBrowserEvents2 for firing extra events

    //This class will capture events from the WebBrowser
    class WebBrowserExtendedEvents : UnsafeNativeMethods.DWebBrowserEvents2
    {
      public WebBrowserExtendedEvents() { }

      ExtendedWebBrowser _Browser;
      public WebBrowserExtendedEvents(ExtendedWebBrowser browser) { _Browser = browser; }

      #region DWebBrowserEvents2 Members

      //Implement whichever events you wish

      //The NewWindow2 event, used on Windows XP SP1 and below
      public void NewWindow2(ref object pDisp, ref bool cancel)
      {
        BrowserExtendedNavigatingEventArgs args = new BrowserExtendedNavigatingEventArgs(pDisp, null, null, UrlContext.None);
        _Browser.OnStartNewWindow(args);
        cancel = args.Cancel;
        pDisp = args.AutomationObject;
      }
      
      // NewWindow3 event, used on Windows XP SP2 and higher
      public void NewWindow3(ref object ppDisp, ref bool Cancel, uint dwFlags, string bstrUrlContext, string bstrUrl)
      {
        BrowserExtendedNavigatingEventArgs args = new BrowserExtendedNavigatingEventArgs(ppDisp, new Uri(bstrUrl), null, (UrlContext)dwFlags);
        _Browser.OnStartNewWindow(args);
        Cancel = args.Cancel;
        ppDisp = args.AutomationObject;
      }

      #region Unused events
      public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
      {
      
      }

      // This event doesn't fire. 
      [DispId(0x00000107)]
      public void WindowClosing(bool isChildWindow, ref bool cancel)
      {
      }

      public void OnQuit()
      {
        
      }

      // Fired when downloading begins
      public void DownloadBegin()
      {
      }

      // Fired when downloading is completed
      public void DownloadComplete()
      {

      }

      public void StatusTextChange(string text)
      {
      
      }

      public void ProgressChange(int progress, int progressMax)
      {
      }

      public void TitleChange(string text)
      {
      }

      public void PropertyChange(string szProperty)
      {
      }

      public void NavigateComplete2(object pDisp, ref object URL)
      {
      }

      public void DocumentComplete(object pDisp, ref object URL)
      {
      }

      public void OnVisible(bool visible)
      {
      }

      public void OnToolBar(bool toolBar)
      {
      }

      public void OnMenuBar(bool menuBar)
      {
      }

      public void OnStatusBar(bool statusBar)
      {
      }

      public void OnFullScreen(bool fullScreen)
      {
      }

      public void OnTheaterMode(bool theaterMode)
      {
      }

      public void WindowSetResizable(bool resizable)
      {
      }

      public void WindowSetLeft(int left)
      {
      }

      public void WindowSetTop(int top)
      {
      }

      public void WindowSetWidth(int width)
      {
      }

      public void WindowSetHeight(int height)
      {
      }
      
      public void SetSecureLockIcon(int secureLockIcon)
      {
      }

      public void FileDownload(ref bool cancel)
      {
      }

      public void NavigateError(object pDisp, ref object URL, ref object frame, ref object statusCode, ref bool cancel)
      {
      }

      public void PrintTemplateInstantiation(object pDisp)
      {
      }

      public void PrintTemplateTeardown(object pDisp)
      {
      }

      public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone)
      {
      }

      public void PrivacyImpactedStateChange(bool bImpacted)
      {
      }

      public void CommandStateChange(int Command, bool Enable)
      {
      }

      public void ClientToHostWindow(ref int CX, ref int CY)
      {
      }
      #endregion

      #endregion
    }

    #endregion

}
