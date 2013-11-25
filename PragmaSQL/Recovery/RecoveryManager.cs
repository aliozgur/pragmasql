using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public enum RecoveryManagerState
  {
    Started,
    Stopped
  }

  public class RecoveryManager:Control
  {
    private System.Threading.Timer _timer = null;
    
    private int _waitMinutes = 5;//60;
    public int WaitMinutes
    {
      get { return _waitMinutes; }
    }

    private int WaitMiliseconds
    {
      get { return _waitMinutes * 60 * 1000; }
    }

    private RecoveryManagerState _state = RecoveryManagerState.Stopped;
    public RecoveryManagerState State
    {
      get { return _state; }
    }

    public RecoveryManager()
    {
      _timer = new System.Threading.Timer(new TimerCallback(OnTimer), null, Timeout.Infinite, 0);
    }

    private void OnTimer(object state)
    {
      PerformAutoSave();
    }

    public void PerformAutoSave()
    {
      PerformAutoSave(false);
    }


    void _bw_DoWork(object sender, DoWorkEventArgs e)
    {
      PerformAutoSave();
    }


    public void PerformAutoSave(bool wantException)
    {
      try
      {
        RecoverContent.CleanAll();
        SaveScriptEditors();
        SaveTextEditors();
      }
      catch (Exception ex)
      {
        if (!wantException)
          HostServicesSingleton.HostServices.MsgService.ErrorMsg(String.Format("Can not perform auto save.Error:{0}", ex.Message));
        else
          throw ex;
      }
    }

    private void SaveScriptEditors()
    {
      IList<IScriptEditor> editors = HostServicesSingleton.HostServices.EditorServices.ScriptEditors;
      foreach (IScriptEditor scriptEditor in editors)
      {
        if (String.IsNullOrEmpty(scriptEditor.Content.Trim()))
          continue;

        RecoverContent.Save(RecoverContent.CreateScriptContent(scriptEditor));
      }
    }

    private void SaveTextEditors()
    {
      IList<ITextEditor> editors = HostServicesSingleton.HostServices.EditorServices.TextEditors;
      foreach (ITextEditor editor in editors)
      {
        if (String.IsNullOrEmpty(editor.Content.Trim()))
          continue;

        RecoverContent.Save(RecoverContent.CreateTextContent(editor));
      }
    }

    void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {

    }

    public void Start(int intervalMinutes)
    {
      _waitMinutes = intervalMinutes;
      _state = RecoveryManagerState.Started;
      _timer.Change(0, WaitMiliseconds);
    }


    public void Stop()
    {
      _state = RecoveryManagerState.Stopped;
      _timer.Change(Timeout.Infinite, 0);
    }

    public void ApplyAutoSaveOptions(ConfigurationContent configContent)
    {
      if (configContent.GeneralOptions.AutoSaveEnabled && (State == RecoveryManagerState.Stopped || WaitMinutes != configContent.GeneralOptions.AutoSaveInterval))
        Start(configContent.GeneralOptions.AutoSaveInterval);
      else if (!configContent.GeneralOptions.AutoSaveEnabled && State == RecoveryManagerState.Started)
        Stop();
    }
  }
}
