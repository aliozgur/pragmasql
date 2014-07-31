/********************************************************************
  Class      : frmObjectChangeHistoryViewer
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Document;

using PragmaSQL.Core;


namespace PragmaSQL
{
  public partial class frmObjectChangeHistoryViewer : DockContent
  {

    private TextEditorControl _textEditor = null;

    private BindingSource _bs = new BindingSource();
    private DataTable _dataTbl = new DataTable("Data");
    private SqlDataAdapter _adapter = new SqlDataAdapter();
    private AsyncQuery _asyncQuery = new AsyncQuery();

    private TextArea ActiveTextArea
    {
      get
      {
        if (_textEditor == null)
        {
          return null;
        }
        else
        {
          return _textEditor.ActiveTextAreaControl.TextArea;
        }
      }
    }

    private IDocument ActiveDocument
    {
      get
      {
        return ActiveTextArea.Document;
      }
    }

    public bool ScriptPaneVisible
    {
      get
      {
        return panScript.Visible;
      }
      set
      {
        if (value)
        {
          panScript.Height = (this.Height / 3) - 10;
        }
        panScript.Visible = value;
        splitterScript.Visible = value;
        splitterScript.BringToFront();
        grd.BringToFront();
        btnToggleScriptPanel.Checked = value;
      }
    }

    public bool IsSearching
    {
      get { return _asyncQuery.IsExecuting; }
    }

   
    public frmObjectChangeHistoryViewer()
    {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#else
      InitializeComponent();
      PopulateObjectTypes();
      InitializeTextEditor();
      _asyncQuery.AfterExecutionCompleted += new ExecutionCompletedDelegate(AfterExecutionCompleted);
      _bs.CurrentChanged += new EventHandler(_bs_CurrentChanged);

			txtServerName.TextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(TextBox_PreviewKeyDown);
			txtDBName.TextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(TextBox_PreviewKeyDown);
			txtObjectName.TextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(TextBox_PreviewKeyDown);
#endif
    }

		void TextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = true;
		}
   
    public void InitializeServerAndDatabase(string serverName, string databaseName)
    {
      txtServerName.Text = serverName;
      txtDBName.Text = databaseName;
    }

    private void InitializeTextEditor()
    {
      if (_textEditor != null)
      {
        return;
      }


      _textEditor = new TextEditorControl();
      panScript.Controls.Add(_textEditor);
      _textEditor.Dock = DockStyle.Fill;
      _textEditor.BringToFront();
      Program.MainForm.ConfigContent.TextEditorOptions.ApplyToControl(_textEditor);
      _textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
      _textEditor.ContextMenuStrip = popUpEditor;
      _textEditor.Focus();
      ActiveTextArea.TextEditorProperties.EnableFolding = false;
      ActiveDocument.ReadOnly = true;
    }

    private void PopulateObjectTypes()
    {
      cmbObjectType.Items.Clear();
      cmbObjectType.Items.Add(new ObjectType("<Any>", String.Empty));
      cmbObjectType.Items.Add(new ObjectType("Stored Procedure", "p"));
      cmbObjectType.Items.Add(new ObjectType("View", "v"));
      cmbObjectType.Items.Add(new ObjectType("Function", "fn"));
      cmbObjectType.Items.Add(new ObjectType("Trigger", "tr"));

      cmbObjectType.SelectedIndex = 0;
    }

    public void LoadDataAsync()
    {
      if (_asyncQuery.IsExecuting)
      {
        return;
      }

      ClearResults();


      _asyncQuery.Params.Clear();
      _asyncQuery.AddParam("@ServerName",SqlDbType.VarChar, String.IsNullOrEmpty(txtServerName.Text) ? null : txtServerName.Text);
      _asyncQuery.AddParam("@DatabaseName", SqlDbType.VarChar, String.IsNullOrEmpty(txtDBName.Text) ? null : txtDBName.Text);
      _asyncQuery.AddParam("@ObjectName", SqlDbType.VarChar, String.IsNullOrEmpty(txtObjectName.Text) ? null : txtObjectName.Text);
      _asyncQuery.AddParam("@ObjectType", SqlDbType.VarChar, cmbObjectType.SelectedIndex == 0 ? null : ((ObjectType)(cmbObjectType.SelectedItem)).Abb);
      
      _asyncQuery.ConnectionString = ConfigHelper.Current.PragmaSqlDbConn.ConnectionString;
      string cmdText = "exec spPragmaSQL_ObjectChangeHist_List @ServerName, @DatabaseName, @ObjectName, @ObjectType";
      if (String.IsNullOrEmpty(cmdText))
      {
        return;
      }

      _asyncQuery.CommandText = cmdText;
      _asyncQuery.Execute();

      btnList.Enabled = false;
      btnStop.Enabled = true;
      SetInputControlsEnabled(false);
    }
    
    private void AfterExecutionCompleted(ExecutionCompletedEventArgs args)
    {
      try
      {
        if (args == null)
        {
          return;
        }

        if (args.Error != null)
        {
          MessageBox.Show(args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (args.Cancelled)
        {
          return;
        }

        if (args.Result == null || args.Result.Tables.Count == 0)
        {
          return;
        }

        _dataTbl = args.Result.Tables[0];
        _bs.DataSource = _dataTbl;
        grd.DataSource = _bs;
        HideColumns();

      }
      finally
      {
        btnList.Enabled = true;
        btnStop.Enabled = false;
        SetInputControlsEnabled(true);
      }
    }

    public void ClearResults()
    {
      _bs.DataSource = null;
      _dataTbl.Clear();

      ActiveDocument.TextContent = String.Empty;
      ActiveTextArea.Invalidate();
      ActiveTextArea.Update();
    }

    public void PerformSearch()
    {
      if (_asyncQuery.IsExecuting)
      {
        return;
      }

      UpdateText();
      LoadDataAsync();
    }

    private void UpdateText()
    {
      this.Text = "Change Hist." + (String.IsNullOrEmpty(txtObjectName.Text) ? String.Empty : " {" + txtObjectName.Text + "}");
      this.TabText = this.Text;
    }
    
    public void CancelSearch()
    {
      if (!_asyncQuery.IsExecuting)
      {
        return;
      }
      _asyncQuery.Cancel();
      btnList.Enabled = true;
      btnStop.Enabled = false;
      SetInputControlsEnabled(true);
    }

    public void StopSearch()
    {
      CancelSearch();
      while (_asyncQuery.IsExecuting)
      {
        Application.DoEvents();
      }
    }

    public void SetCriteria(string server, string database, int objType, string objName)
    {
      txtServerName.Text = server;
      txtDBName.Text = database;
      txtObjectName.Text = objName;
      ObjectType oType = null;

      switch (objType)
      {
        case DBObjectType.StoredProc:
          oType = new ObjectType("Stored Procedure", "p");
          break;
        case DBObjectType.View:
          oType = new ObjectType("View", "v");
          break;
        case DBObjectType.TableValuedFunction:
        case DBObjectType.Function:
        case DBObjectType.ScalarValuedFunction:
          oType = new ObjectType("Function", "fn");
          break;
        case DBObjectType.Trigger:
          oType = new ObjectType("Trigger", "tr");
          break;
        default:
          oType = new ObjectType("<Any>", String.Empty);
          break;
      }
      cmbObjectType.SelectedIndex = cmbObjectType.FindStringExact(oType.ToString());
    }
    
    private void _bs_CurrentChanged(object sender, EventArgs e)
    {
      DataRowView row = _bs.Current as DataRowView;

      if (row == null)
      {
        ActiveDocument.TextContent = String.Empty;
        ActiveTextArea.Invalidate();
        ActiveTextArea.Update();
        return;
      }
     
      ActiveDocument.TextContent = row["ObjectScript"] as string;
      kryptonHeader1.Text = String.Format("Script of '{0}'",row["ObjectName"]);
      ActiveTextArea.Invalidate();
      ActiveTextArea.Update();
      _textEditor.BringToFront();
    }

    private void HideColumns()
    {
      foreach (DataGridViewColumn col in grd.Columns)
      {
        if (col.DataPropertyName.ToLowerInvariant() == "ObjectChangeHistID".ToLowerInvariant())
        {
          col.Visible = false;
          continue;
        }
        if (col.DataPropertyName.ToLowerInvariant() == "ObjectScript".ToLowerInvariant())
        {
          col.Visible = false;
          continue;
        }
        if (col.DataPropertyName.ToLowerInvariant() == "Comment".ToLowerInvariant())
        {
          col.Visible = false;
          continue;
        }
      }
    }

    private void SendScriptToTextDiff(bool isSource)
    {
      DataRowView row = _bs.Current as DataRowView;
      if (row == null)
      {
        return;
      }

      string script = row["ObjectScript"] as string;
      string caption = String.Format("{0} ({1} | {2})"
          , row["ObjectName"] as string
          , row["CreatedOn"]
          , row["CreatedBy"] as string);

      frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
      if (diffForm == null)
      {
        diffForm = TextDiffFactory.CreateDiff();
      }

      if (isSource)
      {
        diffForm.diffControl.SourceText = script;
        diffForm.diffControl.SourceHeaderText = caption;
      }
      else
      {
        diffForm.diffControl.DestText = script;
        diffForm.diffControl.DestHeaderText = caption;
      }
      diffForm.Show();
      diffForm.BringToFront();
    }

    private void SetInputControlsEnabled(bool value)
    {
      if (this.IsDisposed)
        return;

      cmbObjectType.Enabled = value;
      txtDBName.Enabled = value;
      txtObjectName.Enabled = value;
      txtServerName.Enabled = value;
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      PerformSearch();
    }

    private void txtObjectName_KeyDown(object sender, KeyEventArgs e)
    {
			if (e.KeyCode == Keys.Enter)
      {
        PerformSearch();
      }
    }

    private void tsMnuItemCopy_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Copy().Execute(ActiveTextArea);
    }

    private void cMnuItemClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void cMnuCloseAll_Click(object sender, EventArgs e)
    {
      Program.MainForm.CloseDocuments(null);
    }

    private void cMnuCloseAllButThis_Click(object sender, EventArgs e)
    {
      Program.MainForm.CloseDocuments(this);
    }


    private void btnToggleScriptPanel_Click(object sender, EventArgs e)
    {
      ScriptPaneVisible = !ScriptPaneVisible;
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      cMnuDiff.Enabled = (_dataTbl != null && _dataTbl.Rows.Count > 0);
    }

    private void cMnuDiffAsSource_Click(object sender, EventArgs e)
    {
      SendScriptToTextDiff(true);
    }

    private void cMnuDiffAsDest_Click(object sender, EventArgs e)
    {
      SendScriptToTextDiff(false);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      CancelSearch();
    }


    private void frmObjectChangeHistoryViewer_FormClosing(object sender, FormClosingEventArgs e)
    {
      CancelSearch();
    }

    private void buttonSpecAny1_Click(object sender, EventArgs e)
    {
      ScriptPaneVisible = !ScriptPaneVisible;
    }

  } //Class

  class ObjectType
  {
    public string Description = String.Empty;
    public string Abb = String.Empty;

    public override string ToString()
    {
      return Description;
    }

    public ObjectType(string description, string abb)
    {
      Description = description;
      Abb = abb;
    }
  }//Class

} // Namespace