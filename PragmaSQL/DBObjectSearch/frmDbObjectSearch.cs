using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public partial class frmDbObjectSearch : DockContent
  {
    #region Fields And Properties
    private IList<ConnectionParams> _cpList = new List<ConnectionParams>();
    private BindingSource _bs = new BindingSource();
    private DataTable _dataTbl = new DataTable("Data");
    private SqlDataAdapter _adapter = new SqlDataAdapter();

    private AsyncQuery _asyncQuery = new AsyncQuery();

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
      set
      {
        if (_asyncQuery.IsExecuting)
          throw new Exception("Can not change conn param while executing!");

        if (value == null)
          _connParams = null;
        else
          _connParams = value.CreateCopy();
      }
    }

    private string _caption = String.Empty;
    public string Caption
    {
      get { return _caption; }
      set
      {
        _caption = value;
        this.Text = value;
        this.TabText = value;
      }
    }

    private bool _isDialog = false;
    public bool IsDialog
    {
      get { return _isDialog; }
      set
      {
        panButtons.Visible = value;
        MaximizeBox = !value;
        MinimizeBox = !value;
        if (value)
        {
          StartPosition = FormStartPosition.CenterScreen;
        }
        else
        {
          StartPosition = FormStartPosition.Manual;
        }
        _isDialog = value;
      }
    }

    public bool IsSearching
    {
      get { return _asyncQuery.IsExecuting; }
    }

    public bool RowSelectAllowed
    {
      get { return grd.SelectionMode == DataGridViewSelectionMode.FullRowSelect; }
      set 
      {
        grd.SelectionMode = value ? DataGridViewSelectionMode.FullRowSelect : DataGridViewSelectionMode.RowHeaderSelect;
      }
    }


    private ConnectionParamsCollection Connections
    {
      get
      {
        return ConnectionParamsFactory.GetConnections();
      }
    }

    private bool _isInitializing = false;
    
    #endregion //Fields And Properties

    #region CTOR

    public frmDbObjectSearch()
    {
      InitializeComponent();
      _asyncQuery.AfterExecutionCompleted += new ExecutionCompletedDelegate(AfterExecutionCompleted);
      if (!String.IsNullOrEmpty(Program.MainForm.SearchTerm))
        _criterias.AddCriteria(Program.MainForm.SearchTerm, String.Empty);
    }

    #endregion CTOR

    #region Initialization

    public void InitializeForm(string caption, ConnectionParams connParams)
    {
      if (connParams == null)
      {
        throw new NullParameterException("Connection params object is null!");
      }


      _isInitializing = true;
      try
      {
        _connParams = connParams.CreateCopy();
        Caption = caption;
        PopulateServerAndDatabaseCombos();
      }
      finally
      {
        _isInitializing = false;
      }
    }

    #endregion //Initialization

    #region Utils

    public void ClearResults()
    {
      _bs.DataSource = null;
      if (_dataTbl != null)
      {
        _dataTbl.Clear();
      }
    }

    public void ClearResultsAndCriterias()
    {
      _criterias.ClearCriterias();
      ClearResults();
    }

    public void AddSearchTextCriteria(string cr)
    {
      _criterias.AddCriteria(cr, String.Empty);
    }

    public IList<ObjectInfo> GetSelectedObjects()
    {
      ObjectInfo objInfo = null;
      IList<ObjectInfo> result = new List<ObjectInfo>();
      if (grd.SelectedRows.Count == 0)
      {
        return result;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;
      string objOwner = String.Empty;
      IList<frmDataViewer> viewers = new List<frmDataViewer>();

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellObjid = row.Cells[0];
        DataGridViewCell cellObjOwner = row.Cells[1];
        DataGridViewCell cellName = row.Cells[2];
        DataGridViewCell cellType = row.Cells[3];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellObjOwner.ValueType != typeof(string) || cellObjOwner.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;
        objOwner = (string)cellObjOwner.Value;

        objInfo = new ObjectInfo();
        objInfo.ObjectID = objId;
        objInfo.ObjectName = objName;
        objInfo.ObjectTypeAbb = objType;
        objInfo.ObjectType = DBConstants.GetDBObjectType(objInfo.ObjectTypeAbb);
        objInfo.ObjectOwner = objOwner;
        result.Add(objInfo);
      }

      return result;
    }

    #endregion //Utils

    #region Search and load results

    private void LoadDataAsync()
    {
      if (_asyncQuery.IsExecuting)
      {
        return;
      }

      ClearResults();
      _asyncQuery.ConnectionString = _connParams.ConnectionString;
      string cmdText = _criterias.BuildSearchScript();
      if (String.IsNullOrEmpty(cmdText))
      {
        return;
      }

      _asyncQuery.CommandText = cmdText;
      _asyncQuery.Execute();

      lblProgress.Text = "Search in progress. Please wait...";
      Icon = global::PragmaSQL.Properties.Resources.hourglass;
      Program.MainForm.DockPanel.Refresh();
      
      //btnAddCriteria.Enabled = false;
      //btnRemoveCriteria.Enabled = false;
      //btnStartSearch.Enabled = false;
      //btnChangeDb.Enabled = false;
      //btnStopSearch.Enabled = true;
      //cmbServers.Enabled = false;
      //cmbDatabases.Enabled = false;
      DisableToolStripButtons(true);
    }

    private void DisableToolStripButtons(bool value)
    {
      btnAddCriteria.Enabled = !value;
      btnRemoveCriteria.Enabled = !value;
      btnStartSearch.Enabled = !value;
      btnChangeDb.Enabled = !value;
      btnStopSearch.Enabled = value;
      cmbServers.Enabled = !value;
      cmbDatabases.Enabled = !value;
    }

    public void PerformSearch()
    {
      if (_asyncQuery.IsExecuting)
      {
        return;
      }
      _criterias.EndEdit();
      LoadDataAsync();
    }

    public void CancelSearch()
    {
      if (!_asyncQuery.IsExecuting)
      {
        return;
      }
      _asyncQuery.Cancel();
      //btnAddCriteria.Enabled = true;
      //btnRemoveCriteria.Enabled = true;
      //btnStartSearch.Enabled = true;
      //btnChangeDb.Enabled = true;
      //btnStopSearch.Enabled = false;
      //cmbServers.Enabled = true;
      //cmbDatabases.Enabled = true;
      DisableToolStripButtons(false);
    }

    public void StopSearch()
    {
      CancelSearch();
      while (_asyncQuery.IsExecuting)
      {
        Application.DoEvents();
      }
    }

    private void AfterExecutionCompleted(ExecutionCompletedEventArgs args)
    {
      try
      {
        lblProgress.Text = String.Empty;
        Icon = global::PragmaSQL.Properties.Resources.search1;
        Program.MainForm.DockPanel.Refresh();
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
        grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
      }
      finally
      {
        //btnAddCriteria.Enabled = true;
        //btnRemoveCriteria.Enabled = true;
        //btnStartSearch.Enabled = true;
        //btnChangeDb.Enabled = true;
        //btnStopSearch.Enabled = false;

        //cmbServers.Enabled = true;
        //cmbDatabases.Enabled = true;
        DisableToolStripButtons(false);

      }
    }

    #endregion //Search and load results

    #region RowSelect Style Obsolote Methods

    private void RenderContextMenuEx()
    {
      mnuItemOpen.Visible = false;
      mnuItemModify.Visible = false;
      mnuItemDiff.Visible = false;

      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellObjid = row.Cells[0];
        DataGridViewCell cellName = row.Cells[2];
        DataGridViewCell cellType = row.Cells[3];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;

        if (!mnuItemOpen.Visible && DBConstants.DoesObjectTypeHoldsData(objType))
        {
          mnuItemOpen.Visible = true;
        }

        if (!mnuItemModify.Visible && DBConstants.DoesObjectTypeHasScript(objType))
        {
          mnuItemModify.Visible = true;
          mnuItemDiff.Visible = true;
        }
      }


    }

    private void ModifySelectedObjectsEx()
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;
      IList<frmScriptEditor> editors = new List<frmScriptEditor>();

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellObjid = row.Cells[0];
        DataGridViewCell cellName = row.Cells[2];
        DataGridViewCell cellType = row.Cells[3];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;

        if (DBConstants.DoesObjectTypeHasScript(objType))
        {
          int type = DBConstants.GetDBObjectType(objType);
          string script = ScriptingHelper.GetAlterScript(_connParams.ConnectionString, _connParams.Database, objId, type);
          frmScriptEditor editor = ScriptEditorFactory.Create(objName, script, objId, type, _connParams, _connParams.Database);
          editors.Add(editor);
        }
      }

      foreach (frmScriptEditor editor in editors)
      {
        ScriptEditorFactory.ShowScriptEditor(editor);
      }
    }

    private void OpenSelectedObjectsEx()
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;
      IList<frmDataViewer> viewers = new List<frmDataViewer>();

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellObjid = row.Cells[0];
        DataGridViewCell cellName = row.Cells[2];
        DataGridViewCell cellType = row.Cells[3];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;

        if (DBConstants.DoesObjectTypeHoldsData(objType))
        {
          int type = DBConstants.GetDBObjectType(objType);
          string caption = objName + "{" + _connParams.InfoDbServer + "}";
          string script = " select * from [" + objName + "]";
          bool isReadOnly = (type == DBObjectType.View) ? true : false;

          frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, _connParams.Database, objName, caption, script, isReadOnly, false);
          viewers.Add(viewer);
        }
      }

      foreach (frmDataViewer viewer in viewers)
      {
        viewer.LoadData(true);
        DataViewerFactory.ShowDataViewer(viewer);
      }
    }

    private void SendSelectedObjectToTextDiffEx(bool isSource)
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;
      IList<frmScriptEditor> editors = new List<frmScriptEditor>();

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellObjid = row.Cells[0];
        DataGridViewCell cellName = row.Cells[2];
        DataGridViewCell cellType = row.Cells[3];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;

        if (DBConstants.DoesObjectTypeHasScript(objType))
        {
          int type = DBConstants.GetDBObjectType(objType);
          string script = ScriptingHelper.GetAlterScript(_connParams, _connParams.Database, objId, type);

          frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
          if (diffForm == null)
          {
            diffForm = TextDiffFactory.CreateDiff();
          }

          if (isSource)
          {
            diffForm.diffControl.SourceText = script;
            diffForm.diffControl.SourceHeaderText = objName;
          }
          else
          {
            diffForm.diffControl.DestText = script;
            diffForm.diffControl.DestHeaderText = objName;
          }
          diffForm.Show();
          diffForm.BringToFront();
          break;
        }
      }
    }

    private void PerformActionOnFirstSelectedRowEx()
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;


      DataGridViewRow row = grd.SelectedRows[0];

      DataGridViewCell cellObjid = row.Cells[0];
      DataGridViewCell cellName = row.Cells[2];
      DataGridViewCell cellType = row.Cells[3];

      if (cellName.ValueType != typeof(string) || cellName.Value == null)
      {
        return;
      }

      if (cellType.ValueType != typeof(string) || cellType.Value == null)
      {
        return;
      }

      if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
      {
        return;
      }

      objId = (int)cellObjid.Value;
      objType = (string)cellType.Value;
      objName = (string)cellName.Value;

      if (DBConstants.DoesObjectTypeHasScript(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string script = ScriptingHelper.GetAlterScript(_connParams, _connParams.Database, objId, type);
        frmScriptEditor editor = ScriptEditorFactory.Create(objName, script, objId, type, _connParams, _connParams.Database);
        ScriptEditorFactory.ShowScriptEditor(editor);
      }
      else if (DBConstants.DoesObjectTypeHoldsData(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string caption = objName + "{" + _connParams.InfoDbServer + "}";
        string script = " select * from [" + objName + "]";
        bool isReadOnly = (type == DBObjectType.View) ? true : false;

        frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, _connParams.Database, objName, caption, script, isReadOnly, true);
        DataViewerFactory.ShowDataViewer(viewer);
      }

    }

    #endregion //RowSelect Style Obsolote Methods

    #region Object Related User Operations

    private void RenderContextMenu()
    {
      mnuItemOpen.Visible = false;
      mnuItemModify.Visible = false;
      mnuItemDiff.Visible = false;

      if (_bs.Current == null)
      {
        return;
      }

      DataRowView rw = _bs.Current as DataRowView;

      int objId = (int)rw.Row.ItemArray[0];
      string objName = (string)rw.Row.ItemArray[2];
      string objType = (string)rw.Row.ItemArray[3];

      if (!mnuItemOpen.Visible && DBConstants.DoesObjectTypeHoldsData(objType))
      {
        mnuItemOpen.Visible = true;
      }

      if (!mnuItemModify.Visible && DBConstants.DoesObjectTypeHasScript(objType))
      {
        mnuItemModify.Visible = true;
        mnuItemDiff.Visible = true;
      }

    }

    private void ModifyCurrentObject()
    {
      if (_bs.Current == null)
      {
        return;
      }

      DataRowView rw = _bs.Current as DataRowView;
      if (rw == null)
        return;


      int objId = (int)rw.Row.ItemArray[0];
      string objName = (string)rw.Row.ItemArray[2];
      string objType = (string)rw.Row.ItemArray[3];

      if (DBConstants.DoesObjectTypeHasScript(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string script = ScriptingHelper.GetAlterScript(_connParams.ConnectionString, _connParams.Database, objId, type);
        frmScriptEditor editor = ScriptEditorFactory.Create(objName, script, objId, type, _connParams, _connParams.Database);
        ScriptEditorFactory.ShowScriptEditor(editor);
      }

    }
    
    private void OpenCurrentObject()
    {
      if (_bs.Current == null)
      {
        return;
      }

      DataRowView rw = _bs.Current as DataRowView;
      if (rw == null)
        return;

      int objId = (int)rw.Row.ItemArray[0];
      string objOwner = (string)rw.Row.ItemArray[1];
      string objName = (string)rw.Row.ItemArray[2];
      string objType = (string)rw.Row.ItemArray[3];
      string objQuotedFullName = (string)rw.Row.ItemArray[4];
      string objFullName = (string)rw.Row.ItemArray[5];

      if (DBConstants.DoesObjectTypeHoldsData(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string caption = objFullName + " [" + _connParams.InfoDbServer + "]";
        string script = " select * from " + objQuotedFullName;
        bool isReadOnly = (type == DBObjectType.View) ? true : false;

        frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, _connParams.Database, objName, caption, script, isReadOnly, false);
        viewer.LoadData(true);
        DataViewerFactory.ShowDataViewer(viewer);
      }


    }

    private void SendCurrentObjectToTextDiff(bool isSource)
    {
      if (_bs.Current == null)
      {
        return;
      }

      DataRowView rw = _bs.Current as DataRowView;
      if (rw == null)
        return;

      int objId = (int)rw.Row.ItemArray[0];
      string objType = (string)rw.Row.ItemArray[3];
      string objName = (string)rw.Row.ItemArray[2];
      
      if (DBConstants.DoesObjectTypeHasScript(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string script = ScriptingHelper.GetAlterScript(_connParams, _connParams.Database, objId, type);

        frmTextDiff diffForm = frmTextDiff.ActiveTextDiff;
        if (diffForm == null)
        {
          diffForm = TextDiffFactory.CreateDiff();
        }

        if (isSource)
        {
          diffForm.diffControl.SourceText = script;
          diffForm.diffControl.SourceHeaderText = objName;
        }
        else
        {
          diffForm.diffControl.DestText = script;
          diffForm.diffControl.DestHeaderText = objName;
        }
        diffForm.Show();
        diffForm.BringToFront();
      }
    }
    
    private void PerformActionOnCurrentRow()
    {
      if (_bs.Current == null)
      {
        return;
      }

      DataRowView rw = _bs.Current as DataRowView;
      if (rw == null)
        return;

      int objId = (int)rw.Row.ItemArray[0];
      string objType = (string)rw.Row.ItemArray[3];
      string objName = (string)rw.Row.ItemArray[2];

      if (DBConstants.DoesObjectTypeHasScript(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string script = ScriptingHelper.GetAlterScript(_connParams, _connParams.Database, objId, type);
        frmScriptEditor editor = ScriptEditorFactory.Create(objName, script, objId, type, _connParams, _connParams.Database);
        ScriptEditorFactory.ShowScriptEditor(editor);
      }
      else if (DBConstants.DoesObjectTypeHoldsData(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string caption = objName + "{" + _connParams.InfoDbServer + "}";
        string script = " select * from [" + objName + "]";
        bool isReadOnly = (type == DBObjectType.View) ? true : false;

        frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, _connParams.Database, objName, caption, script, isReadOnly, true);
        DataViewerFactory.ShowDataViewer(viewer);
      }

    }

    #endregion //Object Related User Operations

    private void PopulateServerAndDatabaseCombos()
    {
      cmbServers.Items.Clear();
      cmbDatabases.Items.Clear();

      PopulateServers(_connParams);
      PopulateDatabases(_connParams.Database);
    }

    private void PopulateServers(ConnectionParams defaultParams)
    {
      _cpList.Clear();

      int serverIndex = -1;
      bool defaultIsInList = false;
      ConnectionParamsCollection cons = Connections;
      foreach (ConnectionParams cp in cons)
      {
        string key = ConnectionParams.PrepareConnKey(cp);
        if (cmbServers.Items.Contains(key))
          continue;

        cmbServers.Items.Add(key);
        if (defaultParams != null && defaultParams.Server.ToLowerInvariant() == cp.Server.ToLowerInvariant())
        {
          cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
          serverIndex = cmbServers.SelectedIndex;
          defaultIsInList = true;
        }
        _cpList.Add(cp);
      }

      if (!defaultIsInList)
      {
        cmbServers.Items.Add(defaultParams.Server);
        cmbServers.SelectedIndex = cmbServers.Items.Count - 1;
        _cpList.Add(defaultParams);
      }
    }


    private void PopulateDatabases(string defaultDatabaseName)
    {
      cmbDatabases.Items.Clear();

      using (SqlConnection conn = _connParams.CreateSqlConnection(true, false))
      {
        DataTable dbs = conn.GetSchema("Databases");

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
    }

    private void txtCriteria_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        PerformSearch();
      }
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Program.MainForm.CloseDocuments(null);
    }

    private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Program.MainForm.CloseDocuments(this);
    }

    private void mnuItemModify_Click(object sender, EventArgs e)
    {
      ModifyCurrentObject();
    }

    private void mnuItemOpen_Click(object sender, EventArgs e)
    {
      OpenCurrentObject();
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DataObject dtObj = grd.GetClipboardContent();
      if (dtObj == null)
      {
        return;
      }

      Clipboard.SetDataObject(dtObj);
    }

    private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      DataExport.ExportGridToFile(_dataTbl);
    }

    private void grd_DoubleClick(object sender, EventArgs e)
    {
      PerformActionOnCurrentRow();
    }

    private void grd_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        PerformActionOnCurrentRow();
      }
    }

    private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
    {
      RenderContextMenu();
    }

    private void asSourceToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SendCurrentObjectToTextDiff(true);
    }

    private void asDestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SendCurrentObjectToTextDiff(false);
    }

    private void contextMenuStrip2_Opened(object sender, EventArgs e)
    {
      mnuItemSperator.Visible = !(!mnuItemModify.Visible && !mnuItemOpen.Visible);
    }

    private void btnAddCriteria_Click(object sender, EventArgs e)
    {
      _criterias.AddCriteria();
    }

    private void btnRemoveCriteria_Click(object sender, EventArgs e)
    {
      _criterias.RemoveSelectedCriteria();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      PerformSearch();
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      CancelSearch();
    }

    private void frmDbObjectSearch_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_asyncQuery.IsExecuting)
      {
        _asyncQuery.Cancel();
        while (_asyncQuery.IsExecuting)
        {
          Application.DoEvents();
        }
      }
    }

    private void btnChangeDb_Click(object sender, EventArgs e)
    {
      frmConnectionRepository frm = new frmConnectionRepository();
      if (frm.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      ConnectionParams cp = frm.SelectedDataSource;
      if (cp == null)
        return;

      string error = String.Empty;

      AsyncConnectionResult cResult = frmAsyncConnectionOpener.TryToOpenConnection(cp, ref error);
      if (cResult == AsyncConnectionResult.Error)
      {
        MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      else if (cResult != AsyncConnectionResult.Success)
        return;

      _connParams = cp;
      Caption = _connParams.InfoDbServer;
    }

    private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }

      ConnParams = _cpList[cmbServers.SelectedIndex];
      PopulateDatabases(ConnParams.Database);
    }

    private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_isInitializing)
      {
        return;
      }

      ClearResults();
      ConnParams.Database = cmbDatabases.Text;
      Caption = String.Format("Search Db ({0})", ConnParams.InfoDbServer);
    }





  } // Class End
} // Namespace End