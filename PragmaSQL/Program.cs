using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;

using ICSharpCode.Core;
using PragmaSQL.Core;
using PragmaSQL.Proxy;
using Microsoft.WindowsAPICodePack.Taskbar;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace PragmaSQL
{
  static class Program
  {
    private static string _fileName = String.Empty;
    private static frmMain _mainForm = null;
    public static frmMain MainForm
    {
      get { return Program._mainForm; }
    }

    public static frmSplashScreen splashScreen = null;
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      ConfigHelper.LoadFromDefault();
      bool singleInstanceOn = true;

      if (ConfigHelper.Current != null && ConfigHelper.Current.GeneralOptions != null)
        singleInstanceOn = ConfigHelper.Current.GeneralOptions.IsSingleInstance;

      if (!singleInstanceOn || SingletonController.IamFirst(new SingletonController.ReceiveDelegate(OnSingletonControllerReceive)))
      {
        if (User32.FindWindow(null, "PragmaSQL") != IntPtr.Zero)
        {
          if(SendJumpListMsg(args))
            return;
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.ThreadException += new ThreadExceptionEventHandler(Program.OnThreadException);
        splashScreen = new frmSplashScreen();
        splashScreen.Show();

        try
        {
          ICSharpCoreWrapper.StartCoreServices();
        }
        catch (Exception ex)
        {
          frmSplashScreen.HideSplash();
          GenericErrorDialog.ShowError("PragmaSQL Error", "Fatal Error : Core services can not be started.", ex);
          return;
        }

        AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(splashScreen.ShowAssemblies);
        HostServices hostSvc = new HostServices();
#if PERSONAL_EDITION
        hostSvc.HostEdition = HostEditionType.Personal;
#endif
        HostServicesSingleton.HostServices = hostSvc;


        _mainForm = new frmMain();

        if (args != null && args.Length > 0)
        {
          FileInfo fi = new FileInfo(args[0]);
          if (fi.Exists)
          {
            if (fi.Extension.ToLowerInvariant() == ".sqlprj")
            {
              _mainForm.CommandLineProjectFileName = args[0];
            }
            else
            {
              _mainForm.CommandLineScriptFileName = args[0];
            }
          }

        }
        else
        {
          SendJumpListMsg(args);
        }
        Application.Run(_mainForm);
      }
      else
      {
        // send command line args to running app, then terminate
        SingletonController.Send(args);
        SingletonController.Cleanup();
      }
    }

    #region Singleton application related

    static void OpenFileInNewScriptEditor()
    {
      if (String.IsNullOrEmpty(_fileName) || !File.Exists(_fileName))
      {
        return;
      }

      object[] parameters = new object[1];
      parameters[0] = _fileName;
      Program._mainForm.ObjectExplorer.Invoke(new OpenFileInScriptEditorDelegate(Program._mainForm.ObjectExplorer.OpenFileInNewScriptEditor), parameters);
      Program._mainForm.Invoke(new ShowObjectExplorerDelegate(Program._mainForm.ShowObjectExplorer));
      Program._mainForm.Invoke(new EnsureMainFormIsMaximizedDelegate(Program._mainForm.EnsureMaximized));
    }

    static void OpenProjectFile()
    {
      if (String.IsNullOrEmpty(_fileName))
      {
        return;
      }

      object[] parameters = new object[1];
      parameters[0] = _fileName;
      Program._mainForm.Invoke(new EnsureMainFormIsMaximizedDelegate(Program._mainForm.EnsureMaximized));
      Program._mainForm.Invoke(new ShowProjectExplorerDelegate(Program._mainForm.ShowProjectExplorer));
      Program._mainForm.ProjectExplorer.Invoke(new OpenProjectFileDelegate(Program._mainForm.ProjectExplorer.OpenProject), parameters);
    }

    static void CreateEmptyScriptEditor()
    {
      Program._mainForm.Invoke(new EnsureMainFormIsMaximizedDelegate(Program._mainForm.EnsureMaximized));
      Program._mainForm.Invoke(new ShowObjectExplorerDelegate(Program._mainForm.ShowObjectExplorer));
      Program._mainForm.ObjectExplorer.Invoke(new CreateEmptyScriptEditorDelegate(Program._mainForm.ObjectExplorer.CreateScriptEditor), null);
    }

    public static void OnSingletonControllerReceive(string[] args)
    {
      if (args != null && args.Length > 0)
      {
        _fileName = args[0];

        FileInfo fi = new FileInfo(_fileName);
        Thread t = null;

        if (fi.Exists)
        {
          if (fi.Extension.ToLowerInvariant() == ".sqlprj")
          {
            t = new Thread(new ThreadStart(OpenProjectFile));
          }
          else
          {
            t = new Thread(new ThreadStart(OpenFileInNewScriptEditor));
          }
          t.SetApartmentState(ApartmentState.STA);
          t.Start();
        }
        else
        {
          SendJumpListMsg(args);
        }

      }
      else
      {
        _fileName = String.Empty;
        Thread t = new Thread(new ThreadStart(CreateEmptyScriptEditor));
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
      }
    }

    static bool SendJumpListMsg(string[] args)
    {
      if (!TaskbarManager.IsPlatformSupported)
        return false;
      
      if (args.Length == 0 || User32.GetJumpListMsg(args[0]) == -1)
        return false;

      User32.SendMessage("PragmaSQL", User32.GetJumpListMsg(args[0]), IntPtr.Zero, IntPtr.Zero);
      return true;
    }

    #endregion

    public static void OnThreadException(object sender, ThreadExceptionEventArgs e)
    {
      Exception ex = e.Exception;
      if (!TaskDialog.IsPlatformSupported)
      {
        if (ex != null && ex.GetType() == typeof(PersonalEditionLimitation))
        {
          Utils.ShowInfo(ex.Message, MessageBoxButtons.OK);
          return;
        }

        frmException.ShowAppError(e.Exception);
      }
      else
      {
        TaskDialog taskdlg = new TaskDialog();
        TaskDialogStandardButtons button;
        if (ex != null && ex.GetType() == typeof(PersonalEditionLimitation))
        {
          button = TaskDialogStandardButtons.Ok;
          taskdlg.Icon = TaskDialogStandardIcon.Information;
          taskdlg.Caption = "PragmaSQL Information";
          taskdlg.InstructionText = "PragmaSQL Edition Limitation";
          taskdlg.Text = String.IsNullOrEmpty(e.Exception.Message) ? "Requested action is not supported by this edition of PragmaSQL" : e.Exception.Message;
          taskdlg.StandardButtons = button;
          taskdlg.DetailsExpandedLabel = e.Exception.InnerException != null ? "View details" : String.Empty;
          taskdlg.DetailsExpandedText = e.Exception.InnerException != null ?  e.Exception.InnerException.Message : String.Empty;
          taskdlg.ExpansionMode = e.Exception.InnerException != null ? TaskDialogExpandedDetailsLocation.ExpandFooter : TaskDialogExpandedDetailsLocation.Hide;
          taskdlg.Show();
        }
        else
        {
          button = TaskDialogStandardButtons.Ok;
          taskdlg.Icon = TaskDialogStandardIcon.Error;
          taskdlg.Caption = "PragmaSQL Error";
          taskdlg.InstructionText = "Unhandled error occured.";
          taskdlg.Text = e.Exception.Message;
          taskdlg.StandardButtons = button;
          taskdlg.DetailsExpandedLabel = "View Stack Trace";
          taskdlg.DetailsExpandedText = e.Exception.StackTrace;
          taskdlg.ExpansionMode = TaskDialogExpandedDetailsLocation.ExpandFooter;
          taskdlg.Show();
        }
      }
    }

  }
}