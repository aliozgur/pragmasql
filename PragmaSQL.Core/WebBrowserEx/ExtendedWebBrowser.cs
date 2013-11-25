using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace PragmaSQL.WebBrowserEx
{
  public partial class ExtendedWebBrowser : WebBrowser
  {
    
		public ExtendedWebBrowser( )
    {
      InitializeComponent();
    }

    public ExtendedWebBrowser( IContainer container )
    {
      container.Add(this);
      InitializeComponent();
    }

    private UnsafeNativeMethods.IWebBrowser2 axIWebBrowser2;

    /// <summary>
    /// This method supports the .NET Framework infrastructure and is not intended to be used directly from your code. 
    /// Called by the control when the underlying ActiveX control is created. 
    /// </summary>
    /// <param name="nativeActiveXObject"></param>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    protected override void AttachInterfaces(object nativeActiveXObject)
    {
      this.axIWebBrowser2 = (UnsafeNativeMethods.IWebBrowser2)nativeActiveXObject;
      base.AttachInterfaces(nativeActiveXObject);
		}

    /// <summary>
    /// This method supports the .NET Framework infrastructure and is not intended to be used directly from your code. 
    /// Called by the control when the underlying ActiveX control is discarded. 
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    protected override void DetachInterfaces()
    {
      this.axIWebBrowser2 = null;
      base.DetachInterfaces();
    }

    /// <summary>
    /// Returns the automation object for the web browser
    /// </summary>
    public object Application
    {
      get { return axIWebBrowser2.Application; }
    }

    System.Windows.Forms.AxHost.ConnectionPointCookie cookie;
    WebBrowserExtendedEvents events;

    /// <summary>
    /// This method will be called to give you a chance to create your own event sink
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    protected override void CreateSink()
    {
      // Make sure to call the base class or the normal events won't fire
      base.CreateSink();
      events = new WebBrowserExtendedEvents(this);
      cookie = new AxHost.ConnectionPointCookie(this.ActiveXInstance, events, typeof(UnsafeNativeMethods.DWebBrowserEvents2));
    }

    /// <summary>
    /// Detaches the event sink
    /// </summary>
    [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
    protected override void DetachSink()
    {
      if (null != cookie)
      {
        cookie.Disconnect();
        cookie = null;
      }
    }

    /// <summary>
    /// Raised when a new window is to be created. Extends DWebBrowserEvents2::NewWindow2 with additional information about the new window.
    /// </summary>
    public event EventHandler<BrowserExtendedNavigatingEventArgs> StartNewWindow;

    internal void OnStartNewWindow(BrowserExtendedNavigatingEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException("e");

      if (this.StartNewWindow != null)
        this.StartNewWindow(this, e);
    }

  }
}
