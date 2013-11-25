
using System;
using System.Threading;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Svn.Gui;
using NSvn.Core;
using PragmaSQL.Core;

namespace PragmaSQL.Svn
{
  /// <summary>
  /// Description of SvnClient.
  /// </summary>
  public class SvnClient
  {
    public static SvnClient Instance = new SvnClient();

    Client client;
    string logMessage = String.Empty;

    public NSvn.Core.Client Client
    {
      get { return client; }
    }

    public string LogMessage
    {
      get { return logMessage; }
      set { logMessage = value; }
    }

    string GetKindString( NodeKind kind )
    {
      switch (kind)
      {
        case NodeKind.Directory:
          return "directory ";
        case NodeKind.File:
          return "file ";
      }
      return null;
    }

    public static string GetActionString( ChangedPathAction action )
    {
      switch (action)
      {
        case ChangedPathAction.Add:
          return GetActionString(NotifyAction.CommitAdded);
        case ChangedPathAction.Delete:
          return GetActionString(NotifyAction.CommitDeleted);
        case ChangedPathAction.Modify:
          return GetActionString(NotifyAction.CommitModified);
        case ChangedPathAction.Replace:
          return GetActionString(NotifyAction.CommitReplaced);
        default:
          return "unknown";
      }
    }

    public static string GetActionString( NotifyAction action )
    {
      switch (action)
      {
        case NotifyAction.Add:
        case NotifyAction.UpdateAdd:
        case NotifyAction.CommitAdded:
          return "added";
        case NotifyAction.Copy:
          return "copied";
        case NotifyAction.Delete:
        case NotifyAction.UpdateDelete:
        case NotifyAction.CommitDeleted:
          return "deleted";
        case NotifyAction.Restore:
          return "restored";
        case NotifyAction.Revert:
          return "reverted";
        case NotifyAction.FailedRevert:
          return "revert failed";
        case NotifyAction.Resolved:
          return "resolved";
        case NotifyAction.Skip:
          return "skipped";
        case NotifyAction.UpdateUpdate:
          return "updated";
        case NotifyAction.CommitPostfixTxDelta:
        case NotifyAction.UpdateCompleted:
          return "";
        case NotifyAction.UpdateExternal:
          return "updated external";
        case NotifyAction.CommitModified:
          return "modified";
        case NotifyAction.CommitReplaced:
          return "replaced";
        default:
          return "unknown";
      }
    }

    void ReceiveNotification( object sender, NotificationEventArgs e )
    {
      if (e.Action == NotifyAction.UpdateCompleted)
      {
        HostServicesSingleton.HostServices.MsgService.InfoMsg(Environment.NewLine + "Updated " + e.Path + " to revision " + e.RevisionNumber + ".");              
        return;
      }
      if (e.Action == NotifyAction.CommitPostfixTxDelta)
      {
        HostServicesSingleton.HostServices.MsgService.InfoMsg(".");
        return;
      }

      string kind = GetKindString(e.NodeKind);
      string action = GetActionString(e.Action);
      HostServicesSingleton.HostServices.MsgService.InfoMsg(Environment.NewLine + kind + action + " : " + e.Path);
    }

    void SetLogMessage( object sender, LogMessageEventArgs e )
    {
      if (e.Message == null)
      {
        e.Message = logMessage;
      }
    }

    void WriteMid( string str )
    {
      const int max = 40;
      string filler = new String('-', max - str.Length / 2);
      string msg = Environment.NewLine + filler + " " + str + " " + filler;

      if (str.Length % 2 == 0)
      {
        msg += "-";
      }
      
      HostServicesSingleton.HostServices.MsgService.InfoMsg(msg);
    }

    class ThreadStartWrapper
    {
      ThreadStart innerDelegate;

      public ThreadStartWrapper( ThreadStart innerDelegate )
      {
        this.innerDelegate = innerDelegate;
      }

      public void Start( )
      {
        try
        {
          innerDelegate();
        }
        catch (ThreadAbortException)
        {
          // don't show error message, silently cancel thread
        }
        catch (Exception e)
        {
          SvnClient.Instance.OperationDone();

          MessageService.ShowError(e);
        }
        finally
        {
          SvnClient.Instance.OperationDone();
        }
      }
    }

    InOperationDialog inOperationForm;
    bool done = false;
    
    public void OperationStart( string operationName, ThreadStart threadStart )
    {
      done = false;
      WriteMid(operationName);

      Thread thread = new Thread(new ThreadStart(new ThreadStartWrapper(threadStart).Start));
      thread.Name = "SvnOperation";
      thread.IsBackground = true;
      inOperationForm = new InOperationDialog(operationName, thread);
      inOperationForm.Owner = (Form) HostServicesSingleton.HostServices.Wb;
      inOperationForm.Show();
      thread.Start();
    }

    void OperationDone( )
    {
      if (done)
      {
        return;
      }
      HostServicesSingleton.SafeThreadCall(WriteMid, "Done");
      try
      {
        if (inOperationForm != null)
        {
          inOperationForm.Operation = null;
          HostServicesSingleton.SafeThreadCall(inOperationForm.Close);
          inOperationForm = null;
        }
      }
      catch (Exception e)
      {
        MessageService.ShowError(e);
      }
      finally
      {
        done = true;
      }
    }

    public void WaitForOperationEnd( )
    {
      while (!done)
      {
        Application.DoEvents();
      }
    }

    SvnClient( )
    {
      LoggingService.Info("SVN: SvnClient initialized");
      client = new Client();
      client.LogMessage += new LogMessageDelegate(SetLogMessage);
      client.Notification += new NotificationDelegate(ReceiveNotification);

      client.AuthBaton.Add(AuthenticationProvider.GetUsernameProvider());
      client.AuthBaton.Add(AuthenticationProvider.GetSimpleProvider());
      client.AuthBaton.Add(AuthenticationProvider.GetSimplePromptProvider(new SimplePromptDelegate(this.PasswordPrompt), 3));
      client.AuthBaton.Add(AuthenticationProvider.GetSslServerTrustFileProvider());
      client.AuthBaton.Add(AuthenticationProvider.GetSslServerTrustPromptProvider(new SslServerTrustPromptDelegate(this.SslServerTrustPrompt)));
      client.AuthBaton.Add(AuthenticationProvider.GetSslClientCertPasswordFileProvider());
      client.AuthBaton.Add(AuthenticationProvider.GetSslClientCertPasswordPromptProvider(new SslClientCertPasswordPromptDelegate(this.ClientCertificatePasswordPrompt), 3));
      client.AuthBaton.Add(AuthenticationProvider.GetSslClientCertFileProvider());
      client.AuthBaton.Add(AuthenticationProvider.GetSslClientCertPromptProvider(new SslClientCertPromptDelegate(this.ClientCertificatePrompt), 3));
    }

    SimpleCredential PasswordPrompt( string realm, string userName, bool maySave )
    {
      using (LoginDialog loginDialog = new LoginDialog(realm, userName, maySave))
      {
        if (loginDialog.ShowDialog(HostServicesSingleton.HostServices.Wb as Form) == DialogResult.OK)
        {
          return loginDialog.Credential;
        }
      }
      return null;
    }

    SslServerTrustCredential SslServerTrustPrompt( string realm, SslFailures failures, SslServerCertificateInfo info, bool maySave )
    {
      using (SslServerTrustDialog sslServerTrustDialog = new SslServerTrustDialog(info, failures, maySave))
      {
        if (sslServerTrustDialog.ShowDialog(HostServicesSingleton.HostServices.Wb as Form) == DialogResult.OK)
        {
          return sslServerTrustDialog.Credential;
        }
      }
      return null;
    }

    SslClientCertificatePasswordCredential ClientCertificatePasswordPrompt( string realm, bool maySave )
    {
      using (ClientCertPassphraseDialog clientCertPassphraseDialog = new ClientCertPassphraseDialog(realm, maySave))
      {
        if (clientCertPassphraseDialog.ShowDialog(HostServicesSingleton.HostServices.Wb as Form) == DialogResult.OK)
        {
          return clientCertPassphraseDialog.Credential;
        }
      }
      return null;
    }

    SslClientCertificateCredential ClientCertificatePrompt( string realm, bool maySave )
    {
      using (ClientCertDialog clientCertDialog = new ClientCertDialog(realm, maySave))
      {
        if (clientCertDialog.ShowDialog(HostServicesSingleton.HostServices.Wb as Form) == DialogResult.OK)
        {
          return clientCertDialog.Credential;
        }
      }
      return null;
    }
  }
}
