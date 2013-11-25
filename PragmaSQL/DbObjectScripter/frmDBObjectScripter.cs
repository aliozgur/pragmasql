using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using PragmaSQL.Scripting.Smo;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public partial class frmDBObjectScripter : Form
  {
    private ScriptDestination _destination = ScriptDestination.Window;
    private string _destPath = String.Empty;

    private DbObjectScripter _scripter = null;

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
      private set
      {
        if (value != null)
        {
          _connParams = value.CreateCopy();
        }
        else
        {
          _connParams = value;
        }
      }
    }

    private frmDBObjectScripter( )
    {
      InitializeComponent();
      InitializeObjectTypes();
      InitializeSearchTypeCombo();

      InitializeWorkerThread();
      lblStatus.Text = "Press \"Start\" to script database objects.";
    }

    public frmDBObjectScripter( ConnectionParams cp )
      : this()
    {
      ConnParams = cp;
    }

    public static void ShowScripterDialog(ConnectionParams cp)
    {
      frmDBObjectScripter frm = new frmDBObjectScripter(cp);
      frm.Text = "Script Database Objects [" + cp.InfoDbServer + "]";
      frm.Show();
    }

    private void InitializeObjectTypes( )
    {
      lv.Items.Clear();
      Array objTypes = Enum.GetValues(typeof(ScriptObjectTypes));
      foreach (ScriptObjectTypes objType in objTypes)
      {
        ListViewItem item = lv.Items.Add(objType.ToString());
        item.Tag = objType;
        item.Checked = true;
      }
      lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    private void InitializeSearchTypeCombo( )
    {
      cmbSearchType.Items.Clear();
      Array searchTypes = Enum.GetValues(typeof(SearchType));
      foreach (SearchType searchType in searchTypes)
      {
        cmbSearchType.Items.Add(searchType);
      }
      cmbSearchType.SelectedIndex = 0;
    }

    private BackgroundWorker _workerThread = new BackgroundWorker();
    private bool _isInProgress = false;

    private void InitializeWorkerThread( )
    {
      _workerThread.WorkerReportsProgress = true;
      _workerThread.WorkerSupportsCancellation = true;
      _workerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(DoBackgroundWork);
      _workerThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorkCompleted);
      _workerThread.ProgressChanged +=new ProgressChangedEventHandler(_workerThread_ProgressChanged);
    }

    void _workerThread_ProgressChanged( object sender, ProgressChangedEventArgs e )
    {
      ScripProgressArgs args = e.UserState as ScripProgressArgs;
      if (args != null)
      {
        lblStatus.Text = String.Format("Scripting {0} [ {1}/{2} ]: {3}", args.objectType, args.current, args.total, args.objectName);
      }
    }

    private void OnScriptingProgress( string objType, string objectName, int total, int current )
    {
      if (_workerThread.CancellationPending)
      {
        return;
      }

      ScripProgressArgs args = new ScripProgressArgs();
      args.current = current;
      args.total = total;
      args.objectName = objectName;
      args.objectType = objType;
      _workerThread.ReportProgress(0, args);
    }


    private void DoBackgroundWork( object sender, DoWorkEventArgs e )
    {
      // Do not access the form's BackgroundWorker reference directly.
      // Instead, use the reference provided by the sender parameter.
      BackgroundWorker bw = sender as BackgroundWorker;

      // Extract the argument.
      DbObjectScripterArgs args = (DbObjectScripterArgs)e.Argument;


      // Start the time-consuming operation.
      _isInProgress = true;

      e.Result = ScriptObjects(args);

      // If the operation was canceled by the user, 
      // set the DoWorkEventArgs.Cancel property to true.
      if (bw.CancellationPending)
      {
        e.Cancel = true;
      }
    }

    private void BackgroundWorkCompleted( object sender, RunWorkerCompletedEventArgs e )
    {
      try
      {
				DisposeScripter();
        _isInProgress = false;

        btnStart.Enabled = true;
        btnClose.Enabled = true;
        btnStop.Enabled = false;
        groupBox1.Enabled = true;
        groupBox2.Enabled = true;
        groupBox3.Enabled = true;

        bool isCancelled = false;

        if (e.Cancelled)
        {
          lblStatus.Text = "Scripting cancelled!";
          isCancelled = true;
          return;
        }

        if (e.Error != null)
        {
          lblStatus.Text = "Worker thread error occured!";
          GenericErrorDialog.ShowError("Error", "Worker thread error occured.See details below.", e.Error.Message);
          return;
        }

        DbObjectScripterResult result = e.Result as DbObjectScripterResult;
        if (result == null)
        {
          lblStatus.Text = "Nothing was scripted!";
          return;
        }

        PostAction(result, isCancelled);
      }
      finally
      {
        System.Media.SystemSounds.Exclamation.Play();
        this.BringToFront();
      }
    }

    private void PostAction( DbObjectScripterResult result, bool isCancelled )
    {
      string errors = String.Empty;
      if (result.errors != null && result.errors.Count > 0)
      {
        if (!isCancelled)
        {
          lblStatus.Text = "Completed with errors!";
        }

        foreach (Exception ex in result.errors)
        {
          errors += "- " + ex.Message.Replace("\n", " ").Replace("\r", " ") + "\r\n";
        }
      }
      else
      {
        if (!isCancelled)
        {
          lblStatus.Text = "Completed. Press \"Start\" to script objects.";
        }
      }

      switch (_destination)
      {
        case ScriptDestination.Window:
          string caption = "Database Objects [" + _connParams.Server + " {" + _connParams.Database + "} ]";
          frmScriptEditor frm = ScriptEditorFactory.Create(caption, result.script, _connParams);
          ScriptEditorFactory.ShowScriptEditor(frm);
          break;
        case ScriptDestination.File:
          File.AppendAllText(_destPath, result.script);
          break;
        case ScriptDestination.Folder:
          break;
      }

      if (!String.IsNullOrEmpty(errors))
      {
        GenericErrorDialog.ShowError("Error", "Scripting completed with errors! See details below.", errors);
      }
    }

    private DbObjectScripterResult ScriptObjects( DbObjectScripterArgs args )
    {
      DbObjectScripterResult result = new DbObjectScripterResult();
      _scripter = new DbObjectScripter(args.cp);
      _scripter.ScriptingProgress += new ScriptingProgressDelegate(OnScriptingProgress);
      _scripter.ObjectTypes = args.objectTypes;
      _scripter.DbObjectSearchType = args.searchType;
      _scripter.SearchText = args.searchText;

      string script = String.Empty;
      switch (args.destination)
      {
        case ScriptDestination.Window:
          result.errors = _scripter.ScriptObjects(out script);
          result.script = script;
          break;
        case ScriptDestination.File:
          result.errors = _scripter.ScriptObjects(out script);
          result.script = script;
          break;
        case ScriptDestination.Folder:
          result.errors = _scripter.ScriptObjectsToFolder(args.path);
          break;
      }
      return result;
    }

		private void DisposeScripter()
		{
			if (_scripter != null)
			{
				_scripter.Dispose();
				_scripter = null;
			}
		}

    private void StartScripting( )
    {
      if (_isInProgress)
      {
        CancelScripting();
      }

      while (_workerThread.IsBusy)
      {
        Application.DoEvents();
      }

      if (lv.CheckedItems.Count == 0)
      {
        MessageBox.Show("Please select object types first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      DbObjectScripterArgs args = new DbObjectScripterArgs();
      args.destination = _destination;
      foreach (ListViewItem item in lv.CheckedItems)
      {
        args.objectTypes.Add((ScriptObjectTypes)item.Tag);
      }

      switch (_destination)
      {
        case ScriptDestination.Folder:
          if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
          {
            return;
          }
          _destPath = folderBrowserDialog1.SelectedPath;
          break;
        case ScriptDestination.File:
          if (saveFileDialog1.ShowDialog() != DialogResult.OK)
          {
            return;
          }
          _destPath = saveFileDialog1.FileName;
          break;
        default:
          _destPath = String.Empty;
          break;
      }
      args.path = _destPath;
      args.cp = _connParams;
      args.searchText = txtSearchText.Text;
      args.searchType = (SearchType)cmbSearchType.SelectedItem;

      btnStart.Enabled = false;
      btnClose.Enabled = false;
      btnStop.Enabled = true;
      groupBox1.Enabled = false;
      groupBox2.Enabled = false;
      groupBox3.Enabled = false;

      lblStatus.Text = "Scripting in progress...";
      _workerThread.RunWorkerAsync(args);
    }

    private void CancelScripting( )
    {
      if (!_isInProgress)
      {
        return;
      }
      if (_scripter != null)
      {
        _scripter.Cancelled = true;
      }

      _workerThread.CancelAsync();
      while (_workerThread.IsBusy)
      {
        Application.DoEvents();
      }

			DisposeScripter();
    }

    private void rdToWindow_CheckedChanged( object sender, EventArgs e )
    {
      _destination = ScriptDestination.Window;
    }

    private void rdToFile_CheckedChanged( object sender, EventArgs e )
    {
      _destination = ScriptDestination.File;
    }

    private void rdToFolder_CheckedChanged( object sender, EventArgs e )
    {
      _destination = ScriptDestination.Folder;
    }

    private void btnStart_Click( object sender, EventArgs e )
    {
      StartScripting();
    }

    private void btnStop_Click( object sender, EventArgs e )
    {
      CancelScripting();
    }

    private void frmDBObjectScripter_FormClosing( object sender, FormClosingEventArgs e )
    {
      CancelScripting();
    }

    private void button1_Click( object sender, EventArgs e )
    {
      foreach (ListViewItem item in lv.Items)
      {
        item.Checked = true;
      }

    }

    private void button2_Click( object sender, EventArgs e )
    {
      foreach (ListViewItem item in lv.Items)
      {
        item.Checked = false;
      }

    }

    private void button3_Click( object sender, EventArgs e )
    {
      foreach (ListViewItem item in lv.Items)
      {
        item.Checked = !item.Checked;
      }
    }

    private void btnClose_Click( object sender, EventArgs e )
    {
      Close();
    }
  }
}