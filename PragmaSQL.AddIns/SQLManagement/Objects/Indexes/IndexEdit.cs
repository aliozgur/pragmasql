using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class IndexEdit : UserControl
  {
    private EditMode _mode;
    public EditMode Mode
    {
      get { return _mode; }
      set { _mode = value; ApplyMode(); }
    }

    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
          PopulateFileGroupsCombo();
          _index.ConnectionParams = _cp;
        }
        else
        {
          _cp = null;
          cmbFileGroup.Items.Clear();
          UnBindColumns();
        }
      }
    }

    private IndexWrapper _index = new IndexWrapper();
    public IndexWrapper Index
    {
      get { return _index; }
      set { _index = value; }
    }

    private DataTable _tblSourceCols = null;
    private DataTable _tblDestCols = null;
    private DataTable _tblStats = new DataTable();

    public Form OriginForm = null;

    private EventHandler _afterIndexCreated;
    public event EventHandler AfterIndexCreated
    {
      add { _afterIndexCreated += value; }
      remove { _afterIndexCreated -= value; }
    }

    private EventHandler _afterIndexRenamed;
    public event EventHandler AfterIndexRenamed
    {
      add { _afterIndexRenamed += value; }
      remove { _afterIndexRenamed -= value; }
    }


    public IndexEdit( )
    {
      InitializeComponent();
      CreateStatsTable();
    }

    private void ApplyMode( )
    {
      switch (_mode)
      {
        case EditMode.New:
          btnAction.Text = "Create";
          btnDrop.Enabled = false;
          btnRename.Enabled = false;
          gbStatistics.Visible = false;
          txtName.ReadOnly = false;

          break;
        case EditMode.Modify:
          btnAction.Text = "Update";
          btnDrop.Enabled = true;
          btnRename.Enabled = true;
          gbStatistics.Visible = true;
          txtName.ReadOnly = true;
          break;
        default:
          break;
      }
    }

    private void PopulateFileGroupsCombo( )
    {
      DbCmd.PopulateFileGroupsCombo(cmbFileGroup, _cp, false);
      if (cmbFileGroup.Items.Count > 0)
        cmbFileGroup.SelectedIndex = 0;

    }

    private void UnBindColumns( )
    {
      bsSourceCols.DataSource = null;
      _tblSourceCols = null;

      bsDestCols.DataSource = null;
      _tblDestCols = null;

    }

    #region Stats Table Operations
    private void CreateStatsTable( )
    {
      _tblStats.Columns.Clear();
      _tblStats.PrimaryKey = null;

      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "name";
      _tblStats.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Int32");
      column.ColumnName = "value";
      _tblStats.Columns.Add(column);

      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = _tblStats.Columns["name"];
      _tblStats.PrimaryKey = PrimaryKeyColumns;


    }

    private void InitializelStatsTableData( )
    {
      _tblStats.Clear();
      DataRow row = null;
      row = _tblStats.NewRow();
      row[0] = "Data pages";
      row[1] = 0;
      _tblStats.Rows.Add(row);

      row = _tblStats.NewRow();
      row[0] = "Pages reserved";
      row[1] = 0;
      _tblStats.Rows.Add(row);

      row = _tblStats.NewRow();
      row[0] = "Pages used";
      row[1] = 0;
      _tblStats.Rows.Add(row);

      row = _tblStats.NewRow();
      row[0] = "Minimum row size";
      row[1] = 0;
      _tblStats.Rows.Add(row);

      row = _tblStats.NewRow();
      row[0] = "Maximum row size";
      row[1] = 0;
      _tblStats.Rows.Add(row);

      row = _tblStats.NewRow();
      row[0] = "Max size of non-Leaf Index row";
      row[1] = 0;
      _tblStats.Rows.Add(row);

      row = _tblStats.NewRow();
      row[0] = "Total rows modified";
      row[1] = 0;
      _tblStats.Rows.Add(row);
    }

    private void PopulateStatsTable(DataTable tbl)
    {
      bsStats.DataSource = null;

      InitializelStatsTableData();
      if (tbl == null || tbl.Rows.Count == 0)
        return;

      //,,,,,,,lockflags,pgmodctr 
      DataRow row = null;
      row = _tblStats.Rows.Find("Data pages");
      if (row != null)
      {
        row[1] = tbl.Rows[0]["dpages"];
      }

      row = _tblStats.Rows.Find("Pages reserved");
      if (row != null)
      {
        row[1] = tbl.Rows[0]["reserved"];
      }

      row = _tblStats.Rows.Find("Pages used");
      if (row != null)
      {
        row[1] = tbl.Rows[0]["used"];
      }

      row = _tblStats.Rows.Find("Minimum row size");
      if (row != null)
      {
        row[1] = tbl.Rows[0]["minlen"];
      }

      row = _tblStats.Rows.Find("Maximum row size");
      if (row != null)
      {
        row[1] = tbl.Rows[0]["xmaxlen"];
      }

      row = _tblStats.Rows.Find("Max size of non-Leaf Index row");
      if (row != null)
      {
        row[1] = tbl.Rows[0]["maxirow"];
      }

      row = _tblStats.Rows.Find("Total rows modified");
      if (row != null)
      {
        row[1] = tbl.Rows[0]["rowmodctr"];
      }

      bsStats.DataSource = _tblStats;
    }

    private void ClearStatsTable( )
    {
      bsStats.DataSource = null;
      _tblStats.Clear();
    }

    #endregion //Stats Table Operations

    #region Column related operations



    private void CreatePrimaryKeysForIndexColumnTables( )
    {
      if (_tblSourceCols != null)
      {
        DataColumn[] PrimaryKeyColumns = new DataColumn[1];
        PrimaryKeyColumns[0] = _tblSourceCols.Columns["column"];
        _tblSourceCols.PrimaryKey = PrimaryKeyColumns;
      }

      if (_tblDestCols != null)
      {
        DataColumn[] PrimaryKeyColumns = new DataColumn[1];
        PrimaryKeyColumns[0] = _tblDestCols.Columns["column"];
        _tblDestCols.PrimaryKey = PrimaryKeyColumns;
      }
    }

    private void PopulateColumns( )
    {
      UnBindColumns();

      _tblSourceCols = _index.GetIndexNoColumns();
      bsSourceCols.DataSource = _tblSourceCols;

      _tblDestCols = _index.GetIndexColumns();
      bsDestCols.DataSource = _tblDestCols;
      bsDestCols.Sort = "keyno ASC";
      bsSourceCols.Sort = "column ASC";

      CreatePrimaryKeysForIndexColumnTables();

    }


    private void AddSelectedColsToDest( )
    {
      if (grdSourceCols.SelectedRows.Count == 0)
        return;

      // 1- Get last key no in destination grid
      short maxKeyNo = 0;
      bsDestCols.MoveLast();
      DataRowView lastRow = (DataRowView)bsDestCols.Current;
      if (lastRow != null)
      {
        maxKeyNo = (short)lastRow.Row[0];
      }

      IList<string> delList = new List<string>();
      DataRow row = null;

      // 2- Add all selected rows in source to destination
      foreach (DataGridViewRow sRow in grdSourceCols.SelectedRows)
      {
        maxKeyNo++;
        row = _tblDestCols.NewRow();
        row["keyno"] = maxKeyNo;
        row["column"] = sRow.Cells[0].Value;
        _tblDestCols.Rows.Add(row);
        delList.Add(sRow.Cells[0].Value.ToString());

      }

      //3- Remove selected row from source
      foreach (string column in delList)
      {
        row = _tblSourceCols.Rows.Find(column);
        if (row != null)
        {
          _tblSourceCols.Rows.Remove(row);
          row = null;
        }
      }

      bsSourceCols.ResetBindings(false);
      bsDestCols.ResetBindings(false);
    }

    private void RemoveSelectedColsFromDest( )
    {
      if (grdDestCols.SelectedRows.Count == 0)
        return;

      IList<string> delList = new List<string>();
      DataRow row = null;

      foreach (DataGridViewRow dRow in grdDestCols.SelectedRows)
      {
        row = _tblSourceCols.NewRow();
        row["column"] = dRow.Cells[1].Value;

        _tblSourceCols.Rows.Add(row);
        delList.Add(dRow.Cells[1].Value.ToString());

      }

      foreach (string column in delList)
      {
        row = _tblDestCols.Rows.Find(column);
        if (row != null)
        {
          _tblDestCols.Rows.Remove(row);
          row = null;
        }
      }

      ReGenerateDestColOrder();
      bsSourceCols.ResetBindings(false);
      bsDestCols.ResetBindings(false);
    }

    private void MoveColumnsUp( )
    {
      if (grdDestCols.SelectedRows.Count == 0)
        return;

      short keyNo1 = -1;
      short keyNo2 = -1;
      DataRow prevRow = null;
      DataRow currentRow = null;
      foreach (DataGridViewRow dRow in grdDestCols.SelectedRows)
      {
        keyNo1 = (short)dRow.Cells[0].Value;

        if (dRow.Index - 1 < 0)
          continue;

        bsDestCols.Position = dRow.Index;
        currentRow = _tblDestCols.Rows.Find(((DataRowView)bsDestCols.Current).Row[1]);
        bsDestCols.Position = dRow.Index - 1;
        prevRow = _tblDestCols.Rows.Find(((DataRowView)bsDestCols.Current).Row[1]);
        keyNo2 = (short)prevRow[0];


        currentRow[0] = keyNo2;
        prevRow[0] = keyNo1;
      }

      bsDestCols.ResetBindings(false);
    }

    private void MoveColumnsDown( )
    {
      if (grdDestCols.SelectedRows.Count == 0)
        return;

      short keyNo1 = -1;
      short keyNo2 = -1;
      DataRow nextRow = null;
      DataRow currentRow = null;
      foreach (DataGridViewRow dRow in grdDestCols.SelectedRows)
      {
        keyNo1 = (short)dRow.Cells[0].Value;

        if (dRow.Index + 1 >= bsDestCols.Count)
          continue;

        bsDestCols.Position = dRow.Index;
        currentRow = _tblDestCols.Rows.Find(((DataRowView)bsDestCols.Current).Row[1]);
        bsDestCols.Position = dRow.Index + 1;
        nextRow = _tblDestCols.Rows.Find(((DataRowView)bsDestCols.Current).Row[1]);
        keyNo2 = (short)nextRow[0];


        currentRow[0] = keyNo2;
        nextRow[0] = keyNo1;
      }

      bsDestCols.ResetBindings(false);
    }

    private void ReGenerateDestColOrder( )
    {
      int i = 0;
      foreach (DataRow row in _tblDestCols.Rows)
      {
        i++;
        row[0] = i;
      }
    }

    private string GetCurrentIndexColumnsAsString
    {
      get
      {
        string result = String.Empty;
        string seperator = String.Empty;
        foreach (DataRow row in _tblDestCols.Rows)
        {
          result += (!String.IsNullOrEmpty(seperator) ? seperator : String.Empty) + (string)row[1];
          seperator = ",";
        }
        return result;
      }
    }

    private ArrayList GetCurrentIndexColumnsAsList
    {
      get
      {
        ArrayList result = new ArrayList();
        foreach (DataRow row in _tblDestCols.Rows)
        {
          result.Add((string)row[1]);
        }
        return result;
      }
    }
    #endregion //Column related operations

    #region Index Operations
    
    private void RenderIndexData( )
    {
      txtName.Text = _index.Name;
      chkUnique.Checked = _index.Unique;
      chkClustered.Checked = _index.Clustered;
      cmbFileGroup.Text = _index.FileGroup;
      txtFillFactor.Text = _index.FillFactor.ToString();
      chkPadIndex.Checked = _index.PadIndex;
      chkTempDb.Checked = _index.SortInTempDB;
      chkNoRecompute.Checked = _index.NoRecompute;
      chkIgnoreDupKey.Checked = _index.IgnoreDupKey;

      ClearStatsTable();
      PopulateStatsTable(_index.GetStatistics());
    }

    private void ApplyInputToIndex( )
    {
      _index.Name = txtName.Text;
      _index.Unique = chkUnique.Checked;
      _index.Clustered = chkClustered.Checked;
      _index.FileGroup = cmbFileGroup.Text;
      _index.FillFactor = Convert.ToInt32(txtFillFactor.Text);
      _index.PadIndex = chkPadIndex.Checked;
      _index.SortInTempDB = chkTempDb.Checked;
      _index.NoRecompute = chkNoRecompute.Checked;
      _index.IgnoreDupKey = chkIgnoreDupKey.Checked;
      _index.Columns = GetCurrentIndexColumnsAsList;

    }

    public void PrepareNewIndex( string owner, long objectId, string objectName )
    {
      _index.OwnerObjectId = objectId;
      _index.OwnerObjectName = objectName;
      _index.Owner = owner;
      PopulateColumns();
    }

    public void RefreshIndexData( )
    {
      _index.LoadProperties();
      _index.LoadAllProperties();
      RenderIndexData();
      PopulateColumns();
    }

    public void PrepareExistingIndex( long indexId, long objectId, string objectName )
    {
      _index.ID = indexId;
      _index.OwnerObjectId = objectId;
      _index.OwnerObjectName = objectName;
      _index.LoadProperties();
      _index.LoadAllProperties();

      RenderIndexData();
      PopulateColumns();
    }

    public bool DropIndex( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to drop this index?"))
        return false;

      try
      {
        _index.Drop();
        if (OriginForm != null)
          OriginForm.Close();
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
        return false;
      }

      return true;
    }

    public void RenameIndex( )
    {
      string newName = _index.Name;
      if (InputDialog.ShowDialog("Rename Index", "New Name", ref newName) != DialogResult.OK)
        return;

      if (_index.Name.ToLowerInvariant() == newName.ToLowerInvariant())
        return;
      
      try
      {
        _index.Rename(newName);
        txtName.Text = newName;
        if (_afterIndexRenamed != null)
          _afterIndexRenamed(this, EventArgs.Empty);

      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }

    }

    private bool ValidateInput( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Some index properties are not valid!\n";
      if (String.IsNullOrEmpty(cmbFileGroup.Text))
      {
        errorMsg += " - File group not selected.";
        result = false;
      }

      if (String.IsNullOrEmpty(txtName.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Index name is empty.";
        result = false;
      }

      int val = 0;
      if (String.IsNullOrEmpty(txtFillFactor.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Fill factor is empty.";
        result = false;
      }
      else if (!Int32.TryParse(txtFillFactor.Text, out val) || val < 0)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Fill factor is not a valid number.";
        result = false;
      }

      if (_tblDestCols.Rows.Count == 0)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - No columns specified.";
        result = false;
      }

      return result;
    }

    private void CreateIndex( )
    {
      string err = String.Empty;
      if (!ValidateInput(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      ApplyInputToIndex();
      try
      {
        _index.Create(false);
        Mode = EditMode.Modify;
        PopulateStatsTable(_index.GetStatistics());
        if (_afterIndexCreated != null)
          _afterIndexCreated(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }

    }

    private void UpdateIndex( )
    {
      string err = String.Empty;
      if (!ValidateInput(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      ApplyInputToIndex();

      try
      {
        _index.Alter();
        PopulateStatsTable(_index.GetStatistics());
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
    }

    #endregion //Index Operations

    private void button1_Click( object sender, EventArgs e )
    {
      AddSelectedColsToDest();
    }

    private void button2_Click( object sender, EventArgs e )
    {
      RemoveSelectedColsFromDest();
    }

    private void button4_Click( object sender, EventArgs e )
    {
      MoveColumnsUp();
    }

    private void button3_Click( object sender, EventArgs e )
    {
      MoveColumnsDown();
    }

    private void btnDrop_Click( object sender, EventArgs e )
    {
      DropIndex();
    }

    private void btnRename_Click( object sender, EventArgs e )
    {
      RenameIndex();
    }

    private void btnAction_Click( object sender, EventArgs e )
    {
      switch (_mode)
      {
        case EditMode.New:
          CreateIndex();
          break;
        case EditMode.Modify:
          UpdateIndex();
          break;
        default:
          break;
      }
    }
  }

 
}
