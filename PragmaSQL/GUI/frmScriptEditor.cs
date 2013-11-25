using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

using WeifenLuo.WinFormsUI;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using PragmaSQL.GUI;
using PragmaSQL.Common;

using PragmaSQL.Database;

namespace PragmaSQL.GUI
{
  public partial class frmScriptEditor : DockContent
  {
    private TextEditorControl _textEditor = null;
    private CodeCompletionPresenter _codeCompWindow;
    private TextAreaControl _textArea = null;

    private ConnectionParams _connParams;
    private int _objectType = DBObjectType.None;
    private string _script = String.Empty;
    private ConnectionParamsCollection _connParamsCollection = null;

    private SqlConnection _conn = new SqlConnection();
    private string _fileName = String.Empty;

    private SqlCommand _cmd = null;
    private BackgroundWorker workerThread = new BackgroundWorker();

    private IList<DataGridView> _grids = new List<DataGridView>();

    private IList<SqlMessage> _sqlMessages = new List<SqlMessage>();

    private bool _isInitializing = false;

    public frmScriptEditor()
    {
      InitializeComponent();
    }

    #region Initialization


    public void InitializeScriptEditor(string script, int objType, ConnectionParams source, string initialCatalog)
    {
      try
      {
        _isInitializing = true;

        if (source == null)
        {
          throw new NullReferenceException("Connection parameters object can not be null!");
        }

        _script = script;
        _objectType = objType;
        _textEditor.Text = _script;

        _connParams = source.CreateCopy();
        if (String.IsNullOrEmpty(initialCatalog))
        {
          _connParams.InitialCatalog = "master";
        }
        else
        {
          _connParams.InitialCatalog = initialCatalog;
        }

        _connParams.IsConnected = false;
        try
        {
          _conn.ConnectionString = _connParams.ConnectionString;
          _conn.Open();
          PopulateServers(_connParams.Name);
          PopulateDatabases(_connParams.InitialCatalog);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Can not open connection!\nException:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          cmbServers.Items.Clear();
          cmbServers.Items.Add(_connParams.Name);
          cmbServers.SelectedIndex = 0;

          cmbDatabases.Items.Clear();
          cmbDatabases.Items.Add(_connParams.InitialCatalog);
          cmbDatabases.SelectedIndex = 0;
        }
      }
      finally
      {
        _isInitializing = false;
      }

    }

    private void PopulateServers(string defaultServerName)
    {
      cmbServers.Items.Clear();

      if (_conn.State != ConnectionState.Open)
      {
        return;
      }

      int serverIndex = -1;

      _connParamsCollection = ConnectionParamsFactory.GetConnections();
      foreach (ConnectionParams cp in _connParamsCollection)
      {
        cmbServers.Items.Add(cp.Name);
        if (defaultServerName.ToLowerInvariant() == cp.Name.ToLowerInvariant())
        {
          cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
          serverIndex = cmbServers.SelectedIndex;
        }
      }

      if (serverIndex == -1)
      {
        cmbServers.Items.Add(defaultServerName);
        cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
      }
    }

    private void PopulateDatabases(string defaultDatabaseName)
    {
      cmbDatabases.Items.Clear();

      if (_conn.State != ConnectionState.Open)
      {
        return;
      }

      DataTable dbs = _conn.GetSchema("Databases");

      dbs.DefaultView.Sort = "database_name";
      dbs = dbs.DefaultView.ToTable();

      int dbIndex = -1;
      foreach (DataRow row in dbs.Rows)
      {
        string dbName = (string)row["database_name"];

        cmbDatabases.Items.Add(dbName);
        if (defaultDatabaseName.ToLowerInvariant() == dbName.ToLowerInvariant())
        {
          cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
          dbIndex = cmbDatabases.SelectedIndex;
        }
      }

      if (dbIndex == -1)
      {
        cmbDatabases.Items.Add(defaultDatabaseName);
        cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
      }
    }


    #endregion //Initialization

    #region Text editor related

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

    private string PreviousNonWSLineParts
    {
      get
      {
        int caretPos = ActiveTextArea.Caret.Offset;
        if (caretPos == 0)
        {
          return string.Empty;
        }

        int offset = caretPos - 1;

        while (offset >= 0 && !Char.IsWhiteSpace(ActiveTextArea.Document.GetCharAt(offset)))
        {
          offset--;
        }

        if (offset == -1)
        {
          return ActiveTextArea.Document.GetText(0, caretPos);
        }
        else
        {
          return ActiveTextArea.Document.GetText(offset + 1, caretPos - (offset + 1));
        }
      }
    }

    private void InitializeTextEditor()
    {
      if (_textEditor != null)
      {
        return;
      }


      _textEditor = new TextEditorControl();
      panEditor.Controls.Add(_textEditor);
      _textEditor.Dock = DockStyle.Fill;
      _textEditor.BringToFront();
      _textEditor.ConvertTabsToSpaces = true;
      _textEditor.ShowEOLMarkers = false;
      _textEditor.ShowInvalidLines = false;
      _textEditor.ShowLineNumbers = true;
      _textEditor.ShowMatchingBracket = true;
      _textEditor.ShowSpaces = false;
      _textEditor.ShowTabs = false;
      _textEditor.IndentStyle = IndentStyle.Auto;
      _textEditor.TabIndent = 2;
      _textEditor.VRulerRow = 120;
      _textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");

      _textArea = new TextAreaControl(_textEditor);
      _textEditor.ContextMenuStrip = contextMenuEditor;
      ActiveTextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(OnTextEditorKeyDown);
    }

    private void InitializeCodeCompletionWindow()
    {
      if (_codeCompWindow != null)
      {
        return;
      }

      _codeCompWindow = new CodeCompletionPresenter();
      RegisterForCodeCompletionEvents();
    }

    private void OnTextEditorKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Space && e.Control)
      {
        e.SuppressKeyPress = true;
        ShowSelector();
      }
    }

    private void ShowSelector()
    {
      Point final = GetCaretPosition();
      _codeCompWindow.InitializeSelector(_conn);

      _codeCompWindow.SetLocation(final.X + 10, final.Y - 10);
      _codeCompWindow.ShowSelector();

      _codeCompWindow.JumpTo(PreviousNonWSLineParts);
    }

    private void RegisterForCodeCompletionEvents()
    {
      if (_codeCompWindow == null)
      {
        return;
      }

      _codeCompWindow.KeyPressed += new KeyPressHandler(HandleCodeCompletionKeyPress);
      _codeCompWindow.UserMadeFinalSelection += new NotificationHandler(HandleCodeCompletionSelection);
      _codeCompWindow.UserPressedBackSpace += new NotificationHandler(HandleCodeCompletionBackSpace);
    }


    private void HandleCodeCompletionKeyPress(char c)
    {
      switch (c)
      {
        case (char)27: //ESC
          _codeCompWindow.DismissSelector();
          return;
        /*
        case '\b':
          return;
        case '\n':
          return;
        */
        default:
          if (Char.IsControl(c))
          {
            return;
          }
          ActiveTextArea.InsertChar(c);
          _codeCompWindow.JumpTo(PreviousNonWSLineParts);
          break;
      }
    }

    private void HandleCodeCompletionSelection()
    {
      if (_codeCompWindow.Selector.HasMultipleSelection)
      {
        ActiveTextArea.InsertString(_codeCompWindow.SelectedItemsAsCommaSeparatedString);
      }
      else
      {
        DeleteWordBeforeCaret();
        ActiveTextArea.InsertString(_codeCompWindow.SelectedItem);
      }
      _codeCompWindow.DismissSelector();
    }

    private void MoveCaretToEOL()
    {
      ActiveTextArea.Caret.Column = TextUtilities.GetLineAsString(ActiveTextArea.Document, ActiveTextArea.Caret.Line).Length;
    }

    private void HandleCodeCompletionBackSpace()
    {
      new ICSharpCode.TextEditor.Actions.Backspace().Execute(ActiveTextArea);
      _codeCompWindow.JumpTo(PreviousNonWSLineParts);
    }

    public int DeleteWordBeforeCaret()
    {
      int start = TextUtilities.FindPrevWordStart(ActiveTextArea.Document, ActiveTextArea.Caret.Offset);

      if (ActiveTextArea.Document.GetText(start, ActiveTextArea.Caret.Offset - start) != ".")
      {
        ActiveTextArea.Document.Remove(start, ActiveTextArea.Caret.Offset - start);
        ActiveTextArea.Caret.Column = start;
      }
      return start;
    }

    private Point GetCaretPosition()
    {
      TextArea textArea = ActiveTextArea;
      Point caretPos = textArea.Caret.Position;

      int xpos = textArea.TextView.GetDrawingXPos(caretPos.Y, caretPos.X);
      int rulerHeight = textArea.TextEditorProperties.ShowHorizontalRuler ? textArea.TextView.FontHeight : 0;
      Point pos = new Point(textArea.TextView.DrawingPosition.X + xpos,
                            textArea.TextView.DrawingPosition.Y + (textArea.Document.GetVisibleLine(caretPos.Y)) * textArea.TextView.FontHeight - textArea.TextView.TextArea.VirtualTop.Y + textArea.TextView.FontHeight + rulerHeight);

      Point location = _textEditor.ActiveTextAreaControl.PointToScreen(pos);
      return location;
    }

    #endregion //Text editor related

    #region Script I/O

    public void OpenScriptFromFile()
    {
      openFileDialog1.FileName = String.Empty;
      if (openFileDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      _textEditor.LoadFile(openFileDialog1.FileName, false, true);
      _fileName = openFileDialog1.FileName;
      statLblFileName.Text = openFileDialog1.FileName;

      FileInfo fi = new FileInfo(_fileName);
      this.TabText = fi.Name;
      this.Text = this.TabText;
    }

    public void SaveScriptAs()
    {
      string objName = ObjectNameInEditor;
      if (!String.IsNullOrEmpty(objName))
      {
        saveFileDialog1.FileName = objName;
      }
      else
      {
        if (!String.IsNullOrEmpty(_fileName))
        {
          saveFileDialog1.FileName = new FileInfo(_fileName).Name;
        }
      }

      if (saveFileDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }


      _textEditor.SaveFile(saveFileDialog1.FileName);
      _fileName = saveFileDialog1.FileName;
      statLblFileName.Text = _fileName;

      FileInfo fi = new FileInfo(_fileName);
      this.TabText = fi.Name;
      this.Text = this.TabText;

    }

    public void SaveScript()
    {
      if (String.IsNullOrEmpty(_fileName))
      {
        SaveScriptAs();
      }
      else
      {
        _textEditor.SaveFile(_fileName);
        FileInfo fi = new FileInfo(_fileName);
        this.TabText = fi.Name;
        this.Text = this.TabText;
      }
    }

    #endregion //Script I/O

    #region Utilities

    public string ObjectNameInEditor
    {
      get
      {
        int objType = DBObjectType.None;
        bool isAlter = false;
        return ProgrammabilityHelper.GetObjectNameFromScript(_textEditor.Text, ref objType, ref isAlter);
      }
    }

    public bool OutputPaneVisible
    {
      get
      {
        return panOutput.Visible;
      }
      set
      {
        if (value)
        {
          panOutput.Height = (this.Height / 2) - 10;        
        }
        panOutput.Visible = value;
        splitterOutput.Visible = value;
      }
    }

    private void InvalidateButtonsAndMenuItems(bool isRunning)
    {
      btnRun.Enabled = !isRunning;
      mnuItemRun.Enabled = btnRun.Enabled;

      btnStop.Enabled = isRunning;
      mnuItemStop.Enabled = btnStop.Enabled;

      btnSave.Enabled = !isRunning;
      btnSaveAs.Enabled = !isRunning;
      btnOpen.Enabled = !isRunning;
      cmbDatabases.Enabled = !isRunning;
      cmbServers.Enabled = !isRunning;
      btnCheckSyntax.Enabled = !isRunning;
      mnuItemSave.Enabled = !isRunning;
      mnuItemCheckSyntax.Enabled = !isRunning;
    }

    public int LineCount(string value)
    {
      string[] lines = value.Replace("\n\r", "\r").Split('\r');
      return lines.Length;
    }

    public void AddMessageToList(SqlMessage sqlMessage)
    {

      if (sqlMessage == null)
      {
        return;
      }


      ListViewItem item = lv.Items.Add(String.Empty, (int)sqlMessage.MsgType);
      item.UseItemStyleForSubItems = false;

      Color foreColor = Color.Navy;
      Color bgColor = Color.White;
      switch (sqlMessage.MsgType)
      {
        case MessageType.Info:
          foreColor = Color.Navy;
          break;
        case MessageType.Warning:
          foreColor = Color.Maroon;
          break;
        case MessageType.Error:
          foreColor = Color.Red;
          break;
        case MessageType.None:
        default:
          foreColor = Color.Black;
          break;

      }

      string lineText = String.Empty;
      string stateText = String.Empty;
      string typeText = String.Empty;
      if (sqlMessage.Line > 0)
      {
        lineText = sqlMessage.Line.ToString();
      }
      if (sqlMessage.Type > 0)
      {
        typeText = sqlMessage.Type.ToString();
      }
      if (sqlMessage.State > 0)
      {
        stateText = sqlMessage.State.ToString();
      }

      item.SubItems.Add(sqlMessage.Message, foreColor, bgColor, lv.Font);
      item.SubItems.Add(lineText, foreColor, bgColor, lv.Font);
      item.SubItems.Add(typeText, foreColor, bgColor, lv.Font);
      item.SubItems.Add(stateText, foreColor, bgColor, lv.Font);

      lv.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
      lv.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    public void ClearMessagesList()
    {
      lv.Items.Clear();
    }

    #endregion //Utilities

    #region Query Execution

   
    private void ClearResults()
    {
      ClearMessagesList();
      _sqlMessages.Clear();

      tabOutput.SelectTab(0);
      _grids.Clear();
      while (tabOutput.TabPages.Count > 1)
      {
        tabOutput.TabPages.Remove(tabOutput.TabPages[1]);
        Application.DoEvents();
      }
    }

    private void RunScript()
    {
      if (_conn.State != ConnectionState.Open)
      {
        _conn.Open();
      }

      if (String.IsNullOrEmpty(_textEditor.Text))
      {
        return;
      }

      ClearResults();

      OutputPaneVisible = true;
      AddMessageToList(SqlMessage.CreateInfoMessage("Excuting script..."));
      
      string scriptText = String.Empty;
      if (ActiveTextArea.SelectionManager.HasSomethingSelected)
      {
        scriptText = ActiveTextArea.SelectionManager.SelectedText;
      }
      else
      {
        scriptText = _textEditor.Text;
      }

      workerThread.RunWorkerAsync(scriptText);

      InvalidateButtonsAndMenuItems(true);
    }

    private void StopExecution()
    {
      workerThread.CancelAsync();
      if (_cmd != null)
      {
        _cmd.Cancel();
      }
    }

    private void DoBackgroundWork(object sender, DoWorkEventArgs e)
    {

      // Do not access the form's BackgroundWorker reference directly.
      // Instead, use the reference provided by the sender parameter.
      BackgroundWorker bw = sender as BackgroundWorker;

      // Extract the argument.
      string arg = (string)e.Argument;


      // Start the time-consuming operation.
      e.Result = ExecuteScript(bw, arg);

      // If the operation was canceled by the user, 
      // set the DoWorkEventArgs.Cancel property to true.
      if (bw.CancellationPending)
      {
        e.Cancel = true;
      }
    }

    private void BackgroundWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Cancelled)
      {
        _sqlMessages.Clear();
        _sqlMessages.Add(SqlMessage.CreateWarningMessage("Operation cancelled by the user."));
      }
      else if (e.Error != null)
      {
        // There was an error during the operation.
        string msg = String.Format("An error occurred: {0}", e.Error.Message);
        //MessageBox.Show(msg);
      }
      else
      {
        //Completed normally
        RenderResults((IList<DataSet>)e.Result);
      }

      RenderMessagesAndErrors();

      InvalidateButtonsAndMenuItems(false);

      if (tabOutput.TabPages.Count > 1)
      {
        tabOutput.SelectTab(1);
      }
    }
    
    private IList<DataSet> ExecuteScript(BackgroundWorker bw, string queryText)
    {
      int totalLineCnt = 0;
      int currentLineCnt = 0;

      _grids.Clear();

      IList<DataSet> dataSets = new List<DataSet>();
      if (workerThread.CancellationPending)
      {
        return dataSets;
      }

      _conn.InfoMessage += new SqlInfoMessageEventHandler(HandleSqlInfo);
      try
      {
        IList<string> batches = ProgrammabilityHelper.SplitBatches(queryText);
        while (batches.Count > 0)
        {
          try
          {
            DataSet toFill = new DataSet();
            dataSets.Add(toFill);

            string batch = batches[0];
            currentLineCnt = LineCount(batch);
            totalLineCnt += currentLineCnt;

            if (String.IsNullOrEmpty(batch))
            {
              batches.RemoveAt(0);
              continue;
            }
            
            batches.RemoveAt(0);

            _cmd = new SqlCommand(batch, _conn);
            _cmd.CommandTimeout = 30;


            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = _cmd;
            int recordsAffected = adapter.Fill(toFill);

            if (batches.Count == 0)
            {
              _sqlMessages.Add(SqlMessage.CreateInfoMessage("Command(s) completed sucesfully"));
              _sqlMessages.Add(SqlMessage.CreateMessage(""));
            }

            if (toFill.Tables.Count == 0)
            {
              _sqlMessages.Add(SqlMessage.CreateInfoMessage("( " + recordsAffected.ToString() + " row(s) affected ) "));
            }

          }
          catch (SqlException sqlEx)
          {
            int lineNo = totalLineCnt - currentLineCnt + sqlEx.LineNumber;
           _sqlMessages.Add( SqlMessage.CreateErrorMessage(sqlEx.Message, lineNo, sqlEx.Number, sqlEx.State) );
          }
          catch (Exception ex)
          {
           _sqlMessages.Add( SqlMessage.CreateErrorMessage("Exception of type \"" + ex.GetType().ToString() + "\": " + ex.Message, -1, -1, -1));
          }
        }
      }
      finally
      {
        _conn.InfoMessage -= new SqlInfoMessageEventHandler(HandleSqlInfo);
      }

      return dataSets;
    }


    private void HandleSqlInfo(object o, SqlInfoMessageEventArgs e)
    {
      foreach (SqlError err in e.Errors)
      {
        _sqlMessages.Add(SqlMessage.CreateInfoMessage(err.Message));
      }
    }

    #endregion //Query Execution

    #region Rendering functions

    private void RenderResults(IList<DataSet> dataSets)
    {
      if (dataSets != null && dataSets.Count > 0)
      {
        int queryNo = 0;
        int batchNo = 0;

        foreach (DataSet ds in dataSets)
        {
          batchNo++;
          if (ds != null && ds.Tables.Count > 0)
          {
            for (int i = 0; i < ds.Tables.Count; i++)
            {
              queryNo = i + 1;
              int recordCnt = ds.Tables[i].Rows.Count;

              if (i == 0)
              {
                _sqlMessages.Add(SqlMessage.CreateInfoMessage("Batch #" + batchNo.ToString() + " : " + ds.Tables.Count.ToString() + " resultset(s) were returned."));
              }

              _sqlMessages.Add(SqlMessage.CreateInfoMessage("Query " + queryNo.ToString() + ": Returned " + recordCnt.ToString() + " record(s)."));

              // create tab page
              tabOutput.TabPages.Add("Query " + queryNo.ToString() + " [" + recordCnt.ToString() + "]");

              //crate individual grids
              DataGridView grd = new DataGridView();
              grd.DataSource = ds.Tables[i];
              grd.ReadOnly = true;
              grd.Parent = tabOutput.TabPages[tabOutput.TabPages.Count - 1];
              grd.Dock = DockStyle.Fill;
              grd.CellPainting += new DataGridViewCellPaintingEventHandler(OnCellPainting);
              grd.AllowUserToAddRows = false;
              grd.BackgroundColor = SystemColors.Window;
              grd.AllowUserToResizeRows = false;
              grd.BorderStyle = BorderStyle.None;
              grd.ShowEditingIcon = false;

              _grids.Add(grd);
            }
            _sqlMessages.Add(SqlMessage.CreateMessage(""));
          }
        }
      }
      else
      {
        _grids.Clear();
      }
    }

    private void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      if ((e.Value != null) && (e.Value.GetType() == typeof(DBNull)))
      {
        Color gridBrushColor = ((DataGridView)sender).GridColor;
        Color bgColor = Color.LemonChiffon;
        
        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
        {
          bgColor = SystemColors.Highlight;
        }

        Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
            e.CellBounds.Y + 1, e.CellBounds.Width - 4,
            e.CellBounds.Height - 4);


        using (
            Brush gridBrush = new SolidBrush(gridBrushColor),
            backColorBrush = new SolidBrush(bgColor))
        {


          using (Pen gridLinePen = new Pen(gridBrush))
          {

            // Erase the cell.
            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            // Draw the grid lines (only the right and bottom lines;
            // DataGridView takes care of the others).
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                e.CellBounds.Bottom - 1);
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                e.CellBounds.Top, e.CellBounds.Right - 1,
                e.CellBounds.Bottom);

            // Draw the inset highlight box.
            //e.Graphics.DrawRectangle(Pens.Blue, newRect);


            // Draw the text content of the cell, ignoring alignment.
            Brush br = null;
            if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected)
            {
              br = Brushes.Black;
            }
            else
            {
              br = SystemBrushes.HighlightText;
            }

            e.Graphics.DrawString((string)"(NULL)", e.CellStyle.Font,
                br, e.CellBounds.X + 2,
                e.CellBounds.Y + 2, StringFormat.GenericDefault);
            e.Handled = true;
          }

        }
      }

    }

    private void RenderMessagesAndErrors()
    {

      OutputPaneVisible = true;
      foreach (SqlMessage msg in _sqlMessages)
      {
        AddMessageToList(msg);
      }
    }

    #endregion //Rendering function

    private void frmQueryEditor_Load(object sender, EventArgs e)
    {
      OutputPaneVisible = false;
      InitializeTextEditor();
      InitializeCodeCompletionWindow();

      this.ContextMenuStrip = contextMenuTabPage;
      workerThread.WorkerSupportsCancellation = true;
      workerThread.DoWork += new System.ComponentModel.DoWorkEventHandler(DoBackgroundWork);
      workerThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorkCompleted);

    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.DockPanel != null)
      {
        if (this.DockPanel.DocumentStyle == DocumentStyles.SystemMdi)
        {
          foreach (Form form in MdiChildren)
            form.Close();
        }
        else
        {
          foreach (IDockContent content in this.DockPanel.Documents)
            content.DockHandler.Close();
        }
      }
    }

    private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.DockPanel != null)
      {
        if (this.DockPanel.DocumentStyle == DocumentStyles.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form == this)
              continue;
            form.Close();
          }
        }
        else
        {
          foreach (IDockContent content in this.DockPanel.Documents)
          {
            if (content == this)
              continue;
            content.DockHandler.Close();
          }
        }
      }
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      OpenScriptFromFile();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      SaveScript();
    }

    private void btnSaveAs_Click(object sender, EventArgs e)
    {
      SaveScriptAs();
    }

    private void mnuItemSave_Click(object sender, EventArgs e)
    {
      SaveScript();
    }


    private void toggleOutputPaneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OutputPaneVisible = !OutputPaneVisible;
    }

    private void btnRun_Click(object sender, EventArgs e)
    {
      RunScript();
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      StopExecution();
    }

    private void mnuItemRun_Click(object sender, EventArgs e)
    {
      RunScript();
    }

    private void mnuItemStop_Click(object sender, EventArgs e)
    {
      StopExecution();
    }

    private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }

      if (_conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
      }
      _conn.ChangeDatabase(cmbDatabases.Text);
    }

    private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }

      if (_conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
      }

      if (_connParamsCollection == null)
      {
        throw new Exception("Connections collection is null!");
      }

      _conn.Close();
      _conn.ConnectionString = String.Empty;

      ConnectionParams cp = _connParamsCollection[cmbServers.SelectedIndex];
      if (cp == null)
      {
        return;
      }

      _conn.ConnectionString = cp.ConnectionString;
      _conn.Open();

      if (String.IsNullOrEmpty(cp.InitialCatalog))
      {
        cp.InitialCatalog = "master";
      }
      PopulateDatabases(cp.InitialCatalog);
    }


    private void lblCloseoutputPane_MouseEnter(object sender, EventArgs e)
    {
      lblCloseoutputPane.ForeColor = Color.FromKnownColor( KnownColor.LightBlue );
    }

    private void lblCloseoutputPane_MouseLeave(object sender, EventArgs e)
    {
      lblCloseoutputPane.ForeColor = Color.White;
    }

    private void lblCloseoutputPane_MouseClick(object sender, MouseEventArgs e)
    {
      OutputPaneVisible = false;
    }

    private void lblCloseoutputPane_MouseDown(object sender, MouseEventArgs e)
    {
      lblCloseoutputPane.ForeColor = Color.FromKnownColor( KnownColor.SteelBlue);
    }

    private void lblCloseoutputPane_MouseUp(object sender, MouseEventArgs e)
    {
      lblCloseoutputPane.ForeColor = Color.FromKnownColor( KnownColor.LightBlue );
    }

    private void lv_DoubleClick(object sender, EventArgs e)
    {
      if (lv.SelectedItems.Count == 0)
      {
        return;
      }

      ListViewItem selItem = lv.SelectedItems[0];
      string selText = selItem.SubItems[2].Text;
      if (String.IsNullOrEmpty(selText))
      {
        return;
      }

      int lineNo = Convert.ToInt32(selText);
      if (lineNo > ActiveTextArea.Document.TotalNumberOfLines)
      {
        return;
      }

      ActiveTextArea.Caret.Column = 0;
      ActiveTextArea.Caret.Line = lineNo-1;
      ActiveTextArea.Select();
      Point startPoint = ActiveTextArea.Caret.Position;
      Point endPoint = ActiveTextArea.Caret.Position;
      endPoint.X = endPoint.X + ActiveTextArea.Document.GetLineSegment(lineNo-1).Length;
      ActiveTextArea.SelectionManager.SetSelection(startPoint, endPoint);
    }

    private void btnOutDent_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.ShiftTab().Execute(ActiveTextArea);
    }

    private void btnIndent_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.Tab().Execute(ActiveTextArea);
    }

    private void btnToggleBlockComment_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.ToggleBlockComment().Execute(ActiveTextArea);
    }

    private void btnToggleLineComment_Click(object sender, EventArgs e)
    {
      new ICSharpCode.TextEditor.Actions.ToggleLineComment().Execute(ActiveTextArea);
    }

  }
}