using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using Microsoft.SqlServer.Management.Smo;

using PragmaSQL.Core;
using ICSharpCode.Core;
using WizardBase;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace PragmaSQL.Scripting.Smo
{
  public partial class BatchScripterDialog : KryptonForm
  {
    #region Nested Class
  

    private class ScripterResult
    {
      public string Content = String.Empty;
      public string RecentErrors = String.Empty;
      public Exception Error = null;
    }

    #endregion //Nested Class

    #region Fields And Properties

    private BatchScripter _scripter = null;
    private ConnectionParams _cp;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ConnectionParams ConnParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
          _cp = value.CreateCopy();
        else
          _cp = value;
        objList.ConnParams = _cp;
      }
    }

        private string _destFile = String.Empty;
        private string DestFile
        {
            get { return _destFile; }
            set { _destFile = value; edtDestFile.Text = value; }
        }
        
        private ScriptingOptions _options = new ScriptingOptions();
    private DateTime _startTime = DateTime.Now;
   

        string _taskProgressTemplate = String.Empty;

    #endregion //Fields And Properties

    #region CTOR

    public BatchScripterDialog()
    {
      InitializeComponent();
      
            _options.DriPrimaryKey = true;
      _options.DriForeignKeys = true;
      _options.TargetServerVersion = SqlServerVersion.Version90;
      _options.Triggers = true;
            _options.IncludeHeaders = true;
            _options.DriDefaults = true;

      propOptions.SelectedObject = _options;
      lblObjectInfo.Text = String.Empty;
    }

    #endregion //CTOR

    #region Static Methods

    public static void ShowBatchScriptDialog()
    {
      ShowBatchScriptDialog(String.Empty);
    }

        public static void ShowBatchScriptDialog(string objectList)
        {
            IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
            if (srv == null)
            {
                MessageService.ShowError("No object explorer available!");
                return;
            }

            if (srv.SelNode == null || srv.SelNode.ConnParams == null)
            {
                MessageService.ShowError("Database data is not available!");
                return;
            }

            if (String.IsNullOrEmpty(srv.SelNode.ConnParams.Database))
            {
                MessageService.ShowError("Selected node is not a database or child of a database!");
                return;
            }

            ConnectionParams cp = srv.SelNode.ConnParams.CreateCopy();
            cp.Database = srv.SelNode.DatabaseName;
            ShowBatchScriptDialog(cp,objectList);
        }

    public static void ShowBatchScriptDialog(ConnectionParams cp, string objectList)
    {
     
            if (cp == null)
        throw new ArgumentNullException("cp", "Connection parameters object is null!");

            try
            {
                FuzzyWait.ShowFuzzyWait("Preparing PragmaSQL Scripter wizard...");
                BatchScripterDialog frm = new BatchScripterDialog();
                frm.ConnParams = cp;
                frm.Text = "PragmaSQL Scripter [" + cp.InfoDbServer + "]";
        try
        {
          if (!String.IsNullOrEmpty(objectList))
          {
            frm.objList.PrepareObjectFromContent(objectList);
          }
        }
        catch (Exception ex)
        {
          Utils.ShowError("Can not prepare object list.\r\nError:" + ex.Message, MessageBoxButtons.OK);
        }

        frm.Show();
            }
            finally
            {
                FuzzyWait.CloseFuzzyWait();
            }
    }

    #endregion // Static Methods

    #region Methods

    private void ResetProgressInfo()
    {
      ResetProgressInfo(String.Empty);
    }

    private void ResetProgressInfo(string genTaskInfo)
    {

      lblGenTaskInfo.Text = String.IsNullOrEmpty(genTaskInfo) ? "Ready for scripting." : genTaskInfo;
      lblObjectInfo.Text = String.Empty;
      lblTimer.Text = "Elapsed: 00:00:00";
      pb.Value = 0;
    }

    private void UpdateTaskProgressInfo(object sender, string info)
    {
      lblGenTaskInfo.Text = info;
    }

    private void UpdateProgressBar(object sender, ProgressReportEventArgs e)
    {
      pb.Maximum = e.Total;
      pb.Value = e.TotalCount;

      if (TaskbarManager.IsPlatformSupported)
      {
        TaskbarManager.Instance.SetProgressValue(e.TotalCount, e.Total,this.Handle);
      }
    }

    private void UpdateProgressObjectInfo(object sender, ProgressReportEventArgs e)
    {
      string objName = e.Current.GetNameForType(e.Current.Type);

      string template = "Processing object ({0} of {1}): [{2}] " + objName;
      lblObjectInfo.Text = String.Format(template, e.TotalCount, e.Total, e.Current.Type);
    }

    private bool PromptForFileName()
    {
      DestFile = String.Empty;

            if (rdToWindow.Checked)
                return true;

      if (saveFileDialog1.ShowDialog() != DialogResult.OK)
        return false;

      DestFile = saveFileDialog1.FileName;
      return true;
    }

    private void OutputScript(string script)
    {
      
            if (rdToWindow.Checked || String.IsNullOrEmpty(DestFile))
      {
        string caption = "Generated Script For [" + _cp.InfoDbServer + "]";
        HostServicesSingleton.HostServices.EditorServices.CreateScriptEditor(caption, script, _cp);
      }
      else if (rdToFile.Checked)
      {
        File.WriteAllText(DestFile, script);
      }
            else if (rbToFileAndOpen.Checked)
            {
                File.WriteAllText(DestFile, script);
                HostServicesSingleton.HostServices.EditorServices.LoadScriptFile(DestFile, _cp);				
            }
    }

    private void DumpSelectedObjects()
    {
      string caption = "Scripted Objects [" + _cp.InfoDbServer + "]";
      HostServicesSingleton.HostServices.EditorServices.CreateTextEditor(caption, objList.DumpSelectedObjects());
    }

    private void DisposeScripter()
    {
      if (_scripter == null)
        return;

      _scripter.WalkingDependencies -= new ProgressReportEventHandler(_scripter_Progress);
      _scripter.ScriptingInProgress -= new ProgressReportEventHandler(_scripter_Progress);
      _scripter.TaskProgressInfo -= new TaskProgressInfoDelegate(_scripter_TaskProgressInfo);

      _scripter.Dispose();
      _scripter = null;
    }

    private bool CancelScripting(bool confirm)
    {
      if (_scripter == null || !_isScripting)
        return true;

      _scripter.PauseScripting();
      if (confirm && !MessageService.AskQuestion("Do you want to cancel scripting?"))
      {
        _scripter.ResumeScripting();
        return false;
      }
      _scripter.ResumeScripting();
      _scripter.Cancel();

      return true;
    }

    private void ResetWizardState()
    {
      wizardControl1.BackButtonEnabled = true;
      wizardControl1.NextButtonEnabled = true;

      pb.Value = 0;
      timer1.Enabled = false;
      _isScripting = false;

      DisposeScripter();
    }

    private bool CheckAndShowErrors(Exception ex, string recentErrors, bool shallLog)
    {
      if (ex == null)
      {
        if (!String.IsNullOrEmpty(recentErrors))
          PrintError("PragmaSQL Scripter completed with some errors.", null, recentErrors, shallLog);
        return false;
      }

      PrintError("PragmaSQL Scripter stopped with some errors.", ex, recentErrors, shallLog);

      return true;
    }

    private void PrintError(string description, Exception ex, string recentErrors, bool shallLog)
    {
      string errorInfo = (String.IsNullOrEmpty(description) ? String.Empty : description + "\r\n")
        + (ex != null ? "EXCEPTION: " + ex.Message : String.Empty)
        + (ex != null && ex.InnerException != null ? "\r\nEXCEPTION DETAIL: " + ex.InnerException.Message : String.Empty)
        + (ex != null && ex.InnerException != null && ex.InnerException.InnerException != null ? "\r\n" + ex.InnerException.InnerException.Message : String.Empty)
        + (!String.IsNullOrEmpty(recentErrors) ? "\r\nSCRIPTING ERRORS: " + recentErrors : String.Empty);

      edtError.Text = errorInfo;
      gbError.Visible = true;

      /*
      if (shallLog)
        HostServicesSingleton.HostServices.MsgService.ErrorMsg(errorInfo);
      */
    }

    private void NotifyUserOrClose()
    {
      if (rbOnCompleteClose.Checked)
        this.Close();
            else if (rbOnCompleteMessage.Checked)
            {
                MessageBox.Show("Scripting of objects completed." 
                    + ( String.IsNullOrEmpty(DestFile) ? String.Empty : "File: " + DestFile)
                    , "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void StartScripting()
        {
            gbError.Visible = false;

            ResetProgressInfo();

            if (!PromptForFileName())
                return;

            BatchScripter.ScriptingArgs args = PrepareWorkerThreadArgs();

            wizardControl1.BackButtonEnabled = false;
            wizardControl1.NextButtonEnabled = false;

            PrepareScripter();
            StartTimer();

            bw.RunWorkerAsync(args);
        }

        private BatchScripter.ScriptingArgs PrepareWorkerThreadArgs()
        {
            BatchScripter.ScriptingArgs args = new BatchScripter.ScriptingArgs();
            args.Objects = objList.SelectedObjects;
            args.Options = _options;
            args.BatchSeparator = edtBatchSep.Text;
            return args;
        }

        private void PrepareScripter()
        {
            _scripter = new BatchScripter(_cp);
            _scripter.ScriptingInProgress += new ProgressReportEventHandler(_scripter_Progress);
            _scripter.WalkingDependencies += new ProgressReportEventHandler(_scripter_Progress);
            _scripter.TaskProgressInfo += new TaskProgressInfoDelegate(_scripter_TaskProgressInfo);
        }

        private void StartTimer()
        {
            _startTime = DateTime.Now;
            timer1.Enabled = true;
        }

    #endregion //Methods

    private void wizardControl1_HelpButtonClick(object sender, EventArgs e)
    {
      MessageBox.Show("PragmaSQL Scripter\r\nCopyright 2007 - 2009 Ali Özgür", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
    {
      if (TaskbarManager.IsPlatformSupported)
      {
        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal, this.Handle);
        TaskbarManager.Instance.SetProgressValue(0, 100, this.Handle);
      }
      StartScripting();
    }

    private bool _isScripting = false;
    private void bw_DoWork(object sender, DoWorkEventArgs e)
    {
            BatchScripter.ScriptingArgs args = e.Argument as BatchScripter.ScriptingArgs;
      _isScripting = true;

      ScripterResult result = new ScripterResult();

      try
      {
        result.Content = _scripter.ScriptObjects(args);
      }
      catch (Exception ex)
      {
        result.Error = ex;
      }
      finally
      {
        e.Cancel = _scripter.CancelRequested;
        result.RecentErrors = _scripter.RecentErrors;
        e.Result = result;
      }     
    }

    private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (TaskbarManager.IsPlatformSupported)
      {
        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress,this.Handle);
        TaskbarManager.Instance.SetProgressValue(0,100,this.Handle);
      }
      ResetWizardState();
     
      if (e.Cancelled)
      {
        ResetProgressInfo("Scripting was cancelled by the user.");
        return;
      }

      if (e.Error != null)
      {
        ResetProgressInfo("Scripting completed with errors.");
        CheckAndShowErrors(e.Error, String.Empty, true);
        return;
      }

      ScripterResult result = e.Result as ScripterResult;
      bool hasErrors = !String.IsNullOrEmpty(result.RecentErrors) || result.Error != null;
      if (hasErrors)
      {
        ResetProgressInfo("Scripting completed with errors.");
        CheckAndShowErrors(result.Error, result.RecentErrors, true);
      }
      else
      {
        ResetProgressInfo("Scripting completed succesfully.");
        OutputScript(result.Content);
        NotifyUserOrClose();
      }
            this.BringToFront();
    }

    private void _scripter_TaskProgressInfo(object sender, string info)
    {
      if (_scripter == null)
        return;

      this.Invoke(new TaskProgressInfoDelegate(UpdateTaskProgressInfo), new object[2] { sender, info });
    }

    private void _scripter_Progress(object sender, ProgressReportEventArgs e)
    {
      if (_scripter == null)
        return;

      lblGenTaskInfo.Invoke(new ProgressReportEventHandler(UpdateProgressObjectInfo), new object[2] { sender, e });
      pb.Invoke(new ProgressReportEventHandler(UpdateProgressBar), new object[2] { sender, e });
    }

    private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
    {
      if (wizardControl1.CurrentStepIndex == 3)
      {
        if (_isScripting)
          CancelScripting(true);
        else
          Close();
      }
      else
      {
        Close();
      }
    }

    private void BatchScripterDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      //if (!CancelScripting(true))
      //{
      //  e.Cancel = true;
      //}
      //else
      //{
      //  while (bw.IsBusy)
      //  {
      //    Thread.Sleep(10);
      //  }
      //}

      if (_isScripting)
      {
        if (_scripter != null)
          _scripter.PauseScripting();

        MessageService.ShowWarning("Scripting in progress.\r\nYou must first cancel scripting in order to close the wizard.");
        e.Cancel = true;

        if (_scripter != null)
          _scripter.ResumeScripting();
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      TimeSpan startSpan = TimeSpan.FromTicks(_startTime.Ticks);
      TimeSpan endSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);

      TimeSpan diff = endSpan - startSpan;
      diff = endSpan.Subtract(startSpan);

      lblTimer.Text = "Elapsed: " + diff.Hours.ToString("00")
        + ":" + diff.Minutes.ToString("00")
        + ":" + diff.Seconds.ToString("00");
        }

    private void objList_DumpToTexteditorCompleted(object sender, EventArgs e)
    {
      this.BringToFront();
    }

        private void wizardControl1_NextButtonClick(object sender, GenericCancelEventArgs<WizardControl> tArgs)
        {
            
            if (wizardControl1.CurrentStepIndex == 1 && objList.SelectedObjectCount == 0)
            {
                MessageService.ShowError("You did not selected any objects.");
                tArgs.Cancel = true;
            }
        }
  }
}