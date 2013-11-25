using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using NSvn.Core;
using PragmaSQL.Core;
using ICSharpCode.Core;

namespace PragmaSQL.Svn.Gui
{
  public partial class HistoryViewPanel : Panel
  {
    IViewContent _viewContent;
    InfoPanel _infoPanel;
    DiffPanel _diffPanel;

    public HistoryViewPanel( )
    {
      InitializeComponent();
    }

    public HistoryViewPanel( IContainer container )
    {
      container.Add(this);
      InitializeComponent();
    }

    public HistoryViewPanel( IViewContent viewContent )
    {
      this._viewContent = viewContent;
      InitializeComponent();
    }

    protected override void OnVisibleChanged( EventArgs e )
    {
      base.OnVisibleChanged(e);
      if (Visible && _infoPanel == null)
      {
        Initialize();
      }
    }

    void Initialize( )
    {
      TabControl mainTab = new TabControl();
      mainTab.Dock = DockStyle.Fill;
      mainTab.Alignment = TabAlignment.Bottom;


      TabPage infoTabPage = new TabPage("Info");
      _infoPanel = new InfoPanel(_viewContent);
      _infoPanel.Dock = DockStyle.Fill;
      infoTabPage.Controls.Add(_infoPanel);
      mainTab.TabPages.Add(infoTabPage);


      TabPage diffTabPage = new TabPage("Diff");
      _diffPanel = new DiffPanel(_viewContent);
      _diffPanel.Dock = DockStyle.Fill;
      diffTabPage.Controls.Add(_diffPanel);
      mainTab.TabPages.Add(diffTabPage);

      Controls.Add(mainTab);

      Thread logMessageThread = new Thread(new ThreadStart(GetLogMessages));
      logMessageThread.Name = "svnLogMessage";
      logMessageThread.IsBackground = true;
      logMessageThread.Start();
    }

    void GetLogMessages( )
    {
      try
      {
        string fileName = Path.GetFullPath(_viewContent.FileName);
        LoggingService.Info("SVN: Get log of " + fileName);
        if (File.Exists(fileName))
        {
          Client client = SvnClient.Instance.Client;
          client.Log(new string[] { fileName },
                     Revision.Head,          // Revision start
                     Revision.FromNumber(1), // Revision end
                     false,                  // bool discoverChangePath
                     false,                  // bool strictNodeHistory
                     new LogMessageReceiver(ReceiveLogMessage));
        }
      }
      catch (Exception ex)
      {
        // if exceptions aren't caught here, they force SD to exit
        if (ex is SvnClientException || ex is System.Runtime.InteropServices.SEHException)
        {
          LoggingService.Warn(ex);
          HostServicesSingleton.SafeThreadAsyncCall(_infoPanel.ShowError, ex);
        }
        else
        {
          MessageService.ShowError(ex);
        }
      }
    }

    void ReceiveLogMessage( LogMessage logMessage )
    {
      HostServicesSingleton.SafeThreadAsyncCall(_infoPanel.AddLogMessage, logMessage);
      HostServicesSingleton.SafeThreadAsyncCall(_diffPanel.AddLogMessage, logMessage);
    }
  }
}
