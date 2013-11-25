using System;
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
  public partial class TableEdit : UserControl
  {
    #region Fields And Properties
    private PrimaryKeyEdit _primaryKeys;
    private ForeignKeyEdit _foreignKeys;

    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      private set
      {
        try
        {
          _initializingColumn = true;
          if (value != null)
          {
            _cp = value.CreateCopy();
            PopulateDataTypes();
            DbCmd.PopulateDefaultsCombo(cmbDefaultBinding, _cp, false,false);
            DbCmd.PopulateRulesCombo(cmbRuleBinding, _cp, false);
            DbCmd.PopulateOwnerCombo(cmbOwner, _cp, _cp.Database);
            DbCmd.PopulateFileGroupsCombo(cmbFileGroup, _cp, true);
            DbCmd.PopulateCollationsCombo(cmbCollation, _cp);
            cmbFileGroup.SelectedIndex = 0;
            _primaryKeys.ConnectionParams = _cp;
            _foreignKeys.ConnectionParams = _cp;
          }
          else
          {
            _cp = null;
            colDataType.DataSource = null;
            _primaryKeys.ConnectionParams = null;
            _foreignKeys.ConnectionParams = null;

          }
        }
        finally
        {
          _initializingColumn = false;
        }
      }
    }

    private TableWrapper _table;
    public TableWrapper TableObj
    {
      get { return _table; }
      private set
      {
        if (value != null)
        {
          _table = value;
          _table.ConnectionParams = _cp;
        }
      }
    }

    private EditMode _mode;
    public EditMode Mode
    {
      get { return _mode; }
      set { _mode = value; ApplyMode(); }
    }

    public ColumnWrapper CurrentCol
    {
      get
      {
        if (bsCols.Current == null)
          return null;
        else
          return bsCols.Current as ColumnWrapper;
      }
    }

    private void ApplyMode( )
    {
      switch (_mode)
      {
        case EditMode.New:
          cmbOwner.Enabled = true;
          txtName.ReadOnly = false;
          btnPK.Visible = true;

          if (tabControl1.TabPages.Contains(tpPK))
            tabControl1.TabPages.Remove(tpPK);

          if (tabControl1.TabPages.Contains(tpFK))
            tabControl1.TabPages.Remove(tpFK);

          break;
        case EditMode.Modify:
          cmbOwner.Enabled = false;
          txtName.ReadOnly = true;
          btnPK.Visible = false;
          if (!tabControl1.TabPages.Contains(tpPK))
            tabControl1.TabPages.Add(tpPK);
          if (!tabControl1.TabPages.Contains(tpFK))
            tabControl1.TabPages.Add(tpFK);
          break;
        default:
          break;
      }
    }


    #endregion //Fields And Properties

    #region Events

    private EventHandler _afterTableCreated;
    public event EventHandler AfterTableCreated
    {
      add { _afterTableCreated += value; }
      remove { _afterTableCreated -= value; }
    }
    #endregion //Events

    #region CTOR
    public TableEdit( )
    {
      InitializeComponent();
      ClearAdditionalColumnPropertyControls(true);
      SetAdditionalColumnPropError(true);
      CreatePrimaryKeyControl();
      CreateForeignKeyControl();
      colDefault.Visible = false;
    }

    private void CreatePrimaryKeyControl( )
    {
      _primaryKeys = new PrimaryKeyEdit();
      tpPK.Controls.Add(_primaryKeys);
      _primaryKeys.Parent = tpPK;
      _primaryKeys.Dock = DockStyle.Fill;
    }

    private void CreateForeignKeyControl( )
    {
      _foreignKeys = new ForeignKeyEdit();
      tpFK.Controls.Add(_foreignKeys);
      _foreignKeys.Parent = tpFK;
      _foreignKeys.Dock = DockStyle.Fill;
    }

    #endregion //CTOR

    #region Data Type Related
    DataTable _tblTypes = null;
    private void PopulateDataTypes( )
    {
      if (_tblTypes != null)
      {
        _tblTypes = null;
      }

      _tblTypes = DbCmd.GetDataTypes(_cp);
      colDataType.DataSource = _tblTypes;
      colDataType.ValueMember = "name";
      colDataType.DisplayMember = "name";
      colDataType.DataPropertyName = "DataType";
    }

    private DataTypeWrap GetDataTypeInfo( string typeName )
    {
      DataRow row = _tblTypes.Rows.Find(typeName);
      if (row == null)
        return null;

      DataTypeWrap result = new DataTypeWrap();
      result.Name = (string)row["name"];
      result.Width = Utils.IsDbValueValid(row["length"]) ? ((short)row["length"]).ToString() : String.Empty;
      result.Precision = Utils.IsDbValueValid(row["prec"]) ? ((short)row["prec"]).ToString() : String.Empty;
      result.Scale = Utils.IsDbValueValid(row["scale"]) ? ((byte)row["scale"]).ToString() : String.Empty;

      return result;
    }

    #endregion //Data Type Related

    #region DataType,Identity And Computed Column Evaluation

    private void ValidateDataTypeDefaults( )
    {
      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;

      if (col.DataType == col.OldDataType)
        return;

      int rowIndex = bsCols.Position;

      DataTypeWrap dType = Utils.GetDataTypeDefaults(col.DataType);
      DataTypeProperties props = Utils.EvaluateDataTypeProps(col.DataType);

      if ((props & DataTypeProperties.None) == DataTypeProperties.None)
      {
        grd.Rows[rowIndex].Cells[colWidth.Index].Value = String.Empty;
        grd.Rows[rowIndex].Cells[colPrecision.Index].Value = String.Empty;
        grd.Rows[rowIndex].Cells[colScale.Index].Value = String.Empty;
      }
      else if ((props & DataTypeProperties.All) == DataTypeProperties.All)
      {
        grd.Rows[rowIndex].Cells[colWidth.Index].Value = dType.Width;
        grd.Rows[rowIndex].Cells[colPrecision.Index].Value = dType.Precision;
        grd.Rows[rowIndex].Cells[colScale.Index].Value = dType.Scale;
      }
      else
      {
        if ((props & DataTypeProperties.Width) == DataTypeProperties.Width)
        {
          grd.Rows[rowIndex].Cells[colWidth.Index].Value = dType.Width;
        }
        else
        {
          grd.Rows[rowIndex].Cells[colWidth.Index].Value = String.Empty;
        }

        if ((props & DataTypeProperties.Precision) == DataTypeProperties.Precision)
        {
          grd.Rows[rowIndex].Cells[colPrecision.Index].Value = dType.Precision;
        }
        else
        {
          grd.Rows[rowIndex].Cells[colPrecision.Index].Value = String.Empty;
        }

        if ((props & DataTypeProperties.Scale) == DataTypeProperties.Scale)
        {
          grd.Rows[rowIndex].Cells[colScale.Index].Value = dType.Scale;
        }
        else
        {
          grd.Rows[rowIndex].Cells[colScale.Index].Value = String.Empty;
        }
      }
    }


    private void ValidateColumns( ColumnValidationType validationType )
    {
      if (grd.CurrentRow == null || grd.CurrentRow.IsNewRow)
        return;

      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      int rowIndex = bsCols.Position;

      if (!_bindingCompleted || col == null || rowIndex < 0)
        return;

      bool isAll = (ColumnValidationType.All & validationType) == ColumnValidationType.All;
      bool isNone = (ColumnValidationType.None & validationType) == ColumnValidationType.None;

      if (isNone)
        return;
      #region 0- Collation support check

      if (isAll || (ColumnValidationType.Collation & validationType) == ColumnValidationType.Collation)
        cmbCollation.Enabled = Utils.SupportsCollation(col.DataType);

      #endregion

      #region 1- Check if column is a computed column. If true this overrides others

      if (isAll || (ColumnValidationType.Computed & validationType) == ColumnValidationType.Computed)
      {
        for (int i = 3; i <= 10; i++)
        {
          grd.Rows[rowIndex].Cells[i].ReadOnly = col.IsComputed;
          grd.InvalidateCell(i, rowIndex);
        }

        if (col.IsComputed)
        {
          return;
        }
      }
      #endregion

      #region 2- Data Type related checks

      if (isAll || (ColumnValidationType.DataType & validationType) == ColumnValidationType.DataType)
      {
        DataTypeWrap dType = Utils.GetDataTypeDefaults(col.DataType);
        DataTypeProperties props = Utils.EvaluateDataTypeProps(col.DataType);

        if ((props & DataTypeProperties.None) == DataTypeProperties.None)
        {
          grd.Rows[rowIndex].Cells[colWidth.Index].ReadOnly = true;
          grd.Rows[rowIndex].Cells[colPrecision.Index].ReadOnly = true;
          grd.Rows[rowIndex].Cells[colScale.Index].ReadOnly = true;

        }
        else if ((props & DataTypeProperties.All) == DataTypeProperties.All)
        {
          grd.Rows[rowIndex].Cells[colWidth.Index].ReadOnly = false;
          grd.Rows[rowIndex].Cells[colPrecision.Index].ReadOnly = false;
          grd.Rows[rowIndex].Cells[colScale.Index].ReadOnly = false;

        }
        else
        {
          if ((props & DataTypeProperties.Width) == DataTypeProperties.Width)
          {
            grd.Rows[rowIndex].Cells[colWidth.Index].ReadOnly = false;
          }
          else
          {
            grd.Rows[rowIndex].Cells[colWidth.Index].ReadOnly = true;
          }

          if ((props & DataTypeProperties.Precision) == DataTypeProperties.Precision)
          {
            grd.Rows[rowIndex].Cells[colPrecision.Index].ReadOnly = false;
          }
          else
          {
            grd.Rows[rowIndex].Cells[colPrecision.Index].ReadOnly = true;
          }

          if ((props & DataTypeProperties.Scale) == DataTypeProperties.Scale)
          {
            grd.Rows[rowIndex].Cells[colScale.Index].ReadOnly = false;
          }
          else
          {
            grd.Rows[rowIndex].Cells[colScale.Index].ReadOnly = true;
          }
        }

        grd.InvalidateCell(colWidth.Index, rowIndex);
        grd.InvalidateCell(colPrecision.Index, rowIndex);
        grd.InvalidateCell(colScale.Index, rowIndex);
      }
      #endregion

      #region 3- Check identity
      if (isAll || (ColumnValidationType.Identity & validationType) == ColumnValidationType.Identity)
      {
        grd.Rows[rowIndex].Cells[colSeed.Index].ReadOnly = !col.IsIdentity;
        grd.Rows[rowIndex].Cells[colIncrement.Index].ReadOnly = !col.IsIdentity;

        cmbDefaultBinding.Enabled = !col.IsIdentity;

        if (!col.IsIdentity)
        {
          grd.Rows[rowIndex].Cells[colSeed.Index].Value = String.Empty;
          grd.Rows[rowIndex].Cells[colIncrement.Index].Value = String.Empty;
        }
        else
        {
          object val = grd.Rows[rowIndex].Cells[colSeed.Index].Value;
          if (val != null && String.IsNullOrEmpty(val.ToString()))
            grd.Rows[rowIndex].Cells[colSeed.Index].Value = "1";

          val = grd.Rows[rowIndex].Cells[colIncrement.Index].Value;
          if (val != null && String.IsNullOrEmpty(val.ToString()))
            grd.Rows[rowIndex].Cells[colIncrement.Index].Value = "1";
        }

        grd.InvalidateCell(colSeed.Index, rowIndex);
        grd.InvalidateCell(colIncrement.Index, rowIndex);
      }
      #endregion

    }


    #endregion // DataType And Identity Evaluation

    #region Table Initialization
    private bool _initializing = false;
    public void ModifyTable( ConnectionParams cp, long tableId )
    {
      try
      {
        _initializing = true;
        this.ConnectionParams = cp;
        TableObj = new TableWrapper(tableId);
        PopulateTableData();
        BindTableColumns();
        Mode = EditMode.Modify;
        _primaryKeys.InitializePrimaryKeys(TableKeyEditorMode.SingleTable, true, TableObj);
        _foreignKeys.InitializeForeignKeys(TableKeyEditorMode.SingleTable, TableObj, false);
        cmbFileGroup.Enabled = false;
      }
      finally
      {
        _initializing = false;
      }
    }

    public void PostInitialize( )
    {
      InitialCellValidation();
      RenderAdditionalColumnProperties(TableObj.Columns.Count > 0 ? TableObj.Columns[TableObj.Columns.Count - 1] : null);
      if (bsCols.Count > 0)
      {
        bsCols.Position = 0;
      }
    }

    public void CreateTable( ConnectionParams cp )
    {
      try
      {
        _initializing = true;
        this.ConnectionParams = cp;
        TableObj = new TableWrapper();

        //This is a fake column. We need to hack some unknown behaviour of the grid
        ColumnWrapper col = new ColumnWrapper();
        col.Name = "Column";
        col.DataType = "nchar";
        col.Width = "10";
				col.AllowNulls = true;
				col.CollectInitialValues();

        TableObj.Columns.Add(col);

        cmbOwner.SelectedIndex = cmbOwner.FindStringExact("dbo");

        BindTableColumns();
        Mode = EditMode.New;

        RenderAdditionalColumnProperties(bsCols.Current as ColumnWrapper);
        ValidateColumns(ColumnValidationType.All);
      }
      finally
      {
        _initializing = false;
      }
    }

    private void PopulateTableData( )
    {
      _table.LoadProperties();
      _table.LoadColumns();
      RenderTableProperties();
    }

    private bool _bindingCompleted = false;
    private void BindTableColumns( )
    {
      try
      {
        if (_table == null || _table.Columns == null)
        {
          bsCols.DataSource = null;
          grd.DataSource = null;
        }
        else
        {
          _bindingCompleted = false;
          bsCols.DataSource = _table.Columns;
          grd.DataSource = bsCols;
          _bindingCompleted = true;
        }
      }
      finally
      {
      }
    }

    private void UnbindTableColumns( )
    {
      bsCols.DataSource = null;
    }


    private bool _cellsInitialized = false;
    public void InitialCellValidation( )
    {
      if (_cellsInitialized)
        return;

      try
      {
        _initializing = true;
        _cellsInitialized = true;
        foreach (DataGridViewRow row in grd.Rows)
        {
          bsCols.Position = row.Index;
          ValidateColumns(ColumnValidationType.All);
        }
      }
      finally
      {
        _initializing = false;
      }
    }



    #endregion Table Initialization

    #region Column Related Operations

    private bool _initializingColumn = false;
    private void RenderAdditionalColumnProperties( ColumnWrapper col )
    {
      try
      {
        _initializingColumn = true;
        cmbDefaultBinding.Text = String.Empty;
        if (col == null)
        {
          SetAdditionalColumnPropError(true);
          ClearAdditionalColumnPropertyControls(true);
          return;
        }

        SetAdditionalColumnPropError(false);
        ClearAdditionalColumnPropertyControls(false);

        if (col.IsIdentity)
        {
          cmbDefaultBinding.SelectedIndex = -1;
          cmbDefaultBinding.Enabled = false;
        }
        else
        {
          cmbDefaultBinding.Enabled = true;
          if (!String.IsNullOrEmpty(col.Default))
          {
            cmbDefaultBinding.Text = col.Default;
          }
          else
          {
            cmbDefaultBinding.SelectedIndex = cmbDefaultBinding.FindStringExact(col.DefaultBinding);
          }
        }
        cmbRuleBinding.SelectedIndex = cmbRuleBinding.FindStringExact(col.RuleBinding);

        cmbCollation.SelectedIndex = cmbCollation.FindStringExact(col.Collation);
        txtDescription.Text = col.Description;

        chkComputed.Checked = col.IsComputed;
        txtFormula.ReadOnly = !chkComputed.Checked;
        txtFormula.Text = col.Formula;

        lblHeader.Text = col.Name;
      }
      finally
      {
        _initializingColumn = false;
      }
    }

    private void ApplyAdditionalColumnProps( ColumnWrapper col )
    {
      if (cmbDefaultBinding.SelectedIndex == -1)
      {
        col.Default = cmbDefaultBinding.Text;
        col.DefaultBinding = String.Empty;
      }
      else
      {
        col.DefaultBinding = (string)cmbDefaultBinding.SelectedItem;
        col.Default = String.Empty;
      }
      col.RuleBinding = (string)cmbRuleBinding.SelectedItem;

      col.Collation = cmbCollation.SelectedItem as string;
      col.Description = txtDescription.Text;

      col.IsComputed = chkComputed.Checked;
      col.Formula = txtFormula.Text;
    }

    private void ClearAdditionalColumnPropertyControls( bool disable )
    {
      cmbRuleBinding.SelectedIndex = -1;
      cmbDefaultBinding.SelectedIndex = -1;
      cmbDefaultBinding.Text = String.Empty;

      cmbCollation.SelectedIndex = -1;
      txtDescription.Text = String.Empty;
      txtFormula.Text = String.Empty;

      chkComputed.Checked = false;
      gbAdditionalProps.Enabled = !disable;
      lblHeader.Text = String.Empty;
    }

    private void SetAdditionalColumnPropError( bool visible )
    {
      lblAdditionalPropError.Visible = visible;
    }

    #endregion //Column Related Operations

    #region Table Related Operations
    private void RenderTableProperties( )
    {
      cmbOwner.SelectedItem = _table.Owner;
      txtName.Text = _table.Name;
      if (String.IsNullOrEmpty(_table.FileGroup))
        cmbFileGroup.SelectedIndex = 0;
      else
        cmbFileGroup.SelectedItem = _table.FileGroup;
    }

    private void ApplyTableProperties( )
    {
      _table.Owner = (string)cmbOwner.SelectedItem;
      _table.Name = txtName.Text;
      _table.FileGroup = cmbFileGroup.SelectedItem as string;
    }

    private void TogglePrimaryKeyOfSelectedCols( )
    {
      if (grd.SelectedRows.Count == 0)
        return;

      ColumnWrapper col = null;
      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        bsCols.Position = row.Index;
        col = bsCols.Current as ColumnWrapper;
        if (col == null)
          return;
        col.IsPrimaryKey = !col.IsPrimaryKey;

        if (col.IsPrimaryKey && col.AllowNulls)
          col.AllowNulls = false;
      }
      grd.Refresh();
    }

    private void RefreshTable( )
    {
      switch (_mode)
      {
        case EditMode.New:
          break;
        case EditMode.Modify:
          try
          {
            _initializing = true;
            UnbindTableColumns();

            PopulateTableData();
            BindTableColumns();

            _cellsInitialized = false;
            _primaryKeys.RefreshPrimaryKeys(true, _table);
            _foreignKeys.RefreshForeignKeys(_table);
            RenderAdditionalColumnProperties(bsCols.Current as ColumnWrapper);
          }
          finally
          {
            _initializing = false;
          }
          PostInitialize();
          break;
        default:
          break;
      }
    }

    #endregion //Table Related Operations

    #region Create or Alter

    private bool ValidateTableDefinition( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Some table definition values are not valid!\n";

      if (String.IsNullOrEmpty(txtName.Text))
      {
        errorMsg += " - Table name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(cmbOwner.SelectedItem as string))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Table owner not selected.";
        result = false;
      }

      if (String.IsNullOrEmpty(cmbOwner.SelectedItem as string))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Table owner not selected.";
        result = false;
      }

      if (_table.Columns == null || _table.Columns.Count == 0)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Table columns not specified.";
        result = false;
      }

      bool hasIdentityDataTypeError = false;
      bool invalidDataType = false;
      bool hasFormulaError = false;
      bool hasDefaultError = false;

      string tmpDataTypeErrCols = String.Empty;
      string commaDataTypeErr = String.Empty;

      string tmpIdentityErrCols = String.Empty;
      string commaIdentityErr = String.Empty;

      string tmpComputedErrCols = String.Empty;
      string commaComputedErr = String.Empty;

      string tmpDefaultErrCols = String.Empty;
      string commaDefaultErr = String.Empty;

      DataTable tblTypes = DbCmd.GetDataTypes(_cp);
      string baseType = String.Empty;
      DataRow row = null;
      foreach (ColumnWrapper col in _table.Columns)
      {
        row = tblTypes.Rows.Find(col.DataType);
        if (invalidDataType || row == null || !Utils.IsDbValueValid(row["basename"]))
        {
          tmpDataTypeErrCols = commaDataTypeErr + col.Name;
          commaDataTypeErr = ", ";
          invalidDataType = true;
          continue;
        }

        baseType = (string)row["basename"];

        if (col.IsIdentity && !Utils.IsDataTypeValidForIdentity(baseType, col.Scale))
        {
          tmpIdentityErrCols = commaIdentityErr + col.Name;
          commaIdentityErr = ", ";
          hasIdentityDataTypeError = true;
        }

        if (col.IsComputed && String.IsNullOrEmpty(col.Formula))
        {
          tmpComputedErrCols = commaComputedErr + col.Name;
          commaComputedErr = ", ";
          hasFormulaError = true;
        }

        if (!String.IsNullOrEmpty(col.Default) && !String.IsNullOrEmpty(col.DefaultBinding))
        {
          tmpDefaultErrCols = commaDefaultErr + col.Name;
          commaDefaultErr = ", ";
          hasDefaultError = true;
        }
      }

      if (hasIdentityDataTypeError)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Columns ( " + tmpIdentityErrCols + ") does not have valid data type for identity specification.";
        result = false;
      }

      if (invalidDataType)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Columns ( " + tmpDataTypeErrCols + ") have invalid data type.";
        result = false;
      }

      if (hasFormulaError)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Columns ( " + tmpComputedErrCols + ") are computed but do not have formula.";
        result = false;
      }

      if (hasDefaultError)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Columns ( " + tmpDefaultErrCols + ") have default binding and constraint defined at the same time.";
        result = false;
      }
      return result;
    }

    private void CreateOrAlterTable( )
    {
      if (grd.CurrentRow != null && grd.CurrentRow.IsNewRow)
      {
        grd.CancelEdit();
      }
      else if (grd.CurrentRow != null)
      {
        grd.EndEdit();
      }


      string err = String.Empty;
      if (!ValidateTableDefinition(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      ApplyTableProperties();

      switch (_mode)
      {
        case EditMode.New:
          if (CreateTable())
            ReInitializeKeys();
          break;
        case EditMode.Modify:
          if (AlterTable())
            ReInitializeKeys();
          break;
        default:
          break;
      }


    }

    private void ReInitializeKeys( )
    {
      _primaryKeys.InitializePrimaryKeys(TableKeyEditorMode.SingleTable, true, TableObj);
      if (!tabControl1.TabPages.Contains(tpPK))
        tabControl1.TabPages.Add(tpPK);

      _foreignKeys.InitializeForeignKeys(TableKeyEditorMode.SingleTable, TableObj, false);
      if (!tabControl1.TabPages.Contains(tpFK))
        tabControl1.TabPages.Add(tpFK);
    }

    private bool CreateTable( )
    {

      try
      {
        _table.CreateTable();
        cmbFileGroup.Enabled = false;
        if (_afterTableCreated != null)
          _afterTableCreated(_table, EventArgs.Empty);
        return true;
      }
      catch (Exception ex)
      {
        if (ex.GetType() != typeof(CancelledByUserException))
          Utils.ShowException(ex);
        return false;
      }


    }

    private bool AlterTable( )
    {
      try
      {
        long oldId = _table.ID;
        if (_table.AlterTable())
        {
          ChangeTableId(oldId, _table.ID);
        }
        return true;
      }
      catch (Exception ex)
      {
        if (ex.GetType() != typeof(CancelledByUserException))
          Utils.ShowException(ex);
        return false;
      }
    }

    private void ChangeTableId( long oldId, long newId )
    {
      IObjectExplorerService srvc = HostServicesSingleton.HostServices.ObjectExplorerService;
      srvc.ChangeObjectID(_cp.Server.ToLowerInvariant(), _cp.Database.ToLowerInvariant(), oldId, newId);
    }

    #endregion //Create or Alter

    private void bsCols_AddingNew( object sender, AddingNewEventArgs e )
    {
      ColumnWrapper col = new ColumnWrapper();
      col.DataType = "nchar";
      col.Width = "10";
			col.AllowNulls = true;
			col.CollectInitialValues();
      e.NewObject = col;
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      CreateOrAlterTable();
    }

    private void grd_CellPainting( object sender, DataGridViewCellPaintingEventArgs e )
    {
      if (e.RowIndex >= 0 && e.ColumnIndex > 0 && grd.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
      {
        Color gridBrushColor = ((DataGridView)sender).GridColor;
        Color bgColor = Color.FromArgb(238, 238, 238);// FromKnownColor(KnownColor.Control);

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
              br = Brushes.Red;
            }
            else
            {
              br = SystemBrushes.HighlightText;
            }

            e.Graphics.DrawString("", e.CellStyle.Font,
                br, e.CellBounds.X + 2,
                e.CellBounds.Y + 2, StringFormat.GenericDefault);
            e.Handled = true;
          }

        }
      }
    }



    private void bsCols_PositionChanged( object sender, EventArgs e )
    {
      if (_initializing)
        return;

      RenderAdditionalColumnProperties(bsCols.Current as ColumnWrapper);
      ValidateColumns(ColumnValidationType.All);
    }


    private void chkComputed_CheckedChanged( object sender, EventArgs e )
    {
      if (bsCols.Current == null)
        return;

      txtFormula.ReadOnly = !chkComputed.Checked;
      (bsCols.Current as ColumnWrapper).IsComputed = chkComputed.Checked;
      ValidateColumns(ColumnValidationType.All);
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      TogglePrimaryKeyOfSelectedCols();
    }

    private void toolStripButton1_Click( object sender, EventArgs e )
    {
      RefreshTable();
    }

    private void cmbDefaultBinding_SelectedIndexChanged( object sender, EventArgs e )
    {
      /*
      if (_initializingColumn)
        return;

      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;

      if (cmbDefaultBinding.SelectedIndex == -1)
      {
        col.DefaultBinding = String.Empty;
        col.Default = String.Empty;
      }
      else
      {
        col.DefaultBinding = cmbDefaultBinding.SelectedItem as string;
        col.Default = String.Empty;
      }
      */
    }

    private void cmbRuleBinding_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializingColumn)
        return;

      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;
      col.RuleBinding = cmbRuleBinding.SelectedItem as string;
    }

    private void txtDescription_TextChanged( object sender, EventArgs e )
    {
      if (_initializingColumn)
        return;

      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;

      col.Description = txtDescription.Text;
    }

    private void txtFormula_TextChanged( object sender, EventArgs e )
    {
      if (_initializingColumn)
        return;

      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;

      col.Formula = txtFormula.Text;
    }

    private void cmbCollation_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializingColumn)
        return;

      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;
      col.Collation = cmbCollation.SelectedItem as string;
    }

    [Flags]
    private enum ColumnValidationType
    {
      None = 0x01,
      DataType = 0x02,
      Identity = 0x04,
      Computed = 0x08,
      Collation = 0x16,
      All = 0x32
    }

    private void grd_CellValidated( object sender, DataGridViewCellEventArgs e )
    {
      if (e.ColumnIndex == colDataType.Index || e.ColumnIndex == colIdentity.Index)
      {
        ValidateColumns(ColumnValidationType.All);
      }

      if (e.ColumnIndex == colName.Index)
      {
        lblHeader.Text = CurrentCol != null ? CurrentCol.Name : String.Empty; 
      }

    }

    private void grd_CellValueChanged( object sender, DataGridViewCellEventArgs e )
    {
      if (e.RowIndex < 0)
        return;

      if (e.ColumnIndex == colDataType.Index)
        ValidateDataTypeDefaults();

      if (e.ColumnIndex == colIdentity.Index)
      {
        object val = grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        if (val != null && (bool)val)
        {
          cmbDefaultBinding.SelectedIndex = -1;
        }
      }
    }

    private void grd_CellValidating( object sender, DataGridViewCellValidatingEventArgs e )
    {
      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;

      if (e.ColumnIndex == colDataType.Index && col.IsIdentity)
      {
        if (!Utils.IsDataTypeValidForIdentity((string)e.FormattedValue, (string)grd.Rows[e.RowIndex].Cells[5].Value))
        {
          MessageService.ShowWarning("Only int, smallint, tinyint and bigint data types can be used as identity columns.");
          e.Cancel = true;
          grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = col.DataType;
        }
      }
      else if (e.ColumnIndex == colAllowNull.Index)
      {
        if (col.IsPrimaryKey && ((bool)e.FormattedValue))
        {
          MessageService.ShowWarning("Allow Null property can not be set on a column which is part of the primary key.");
          e.Cancel = true;
          grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
        }
      }
      else if (e.ColumnIndex == colIdentity.Index)
      {
        if ((bool)e.FormattedValue)
        {
          if (!Utils.IsDataTypeValidForIdentity((string)grd.Rows[e.RowIndex].Cells[2].Value, (string)grd.Rows[e.RowIndex].Cells[5].Value))
          {
            MessageService.ShowWarning("Only int, smallint, tinyint, bigint, numeric or decimal (with zero scale) can be used as identity columns.");
            e.Cancel = true;
            grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
          }
        }

      }
    }

    private void cmbDefaultBinding_TextChanged( object sender, EventArgs e )
    {
      if (_initializingColumn)
        return;

      ColumnWrapper col = bsCols.Current as ColumnWrapper;
      if (col == null)
        return;

      if (cmbDefaultBinding.SelectedIndex == -1 || cmbDefaultBinding.Items.IndexOf(cmbDefaultBinding.Text) == -1)
      {
        col.DefaultBinding = String.Empty;
        col.Default = cmbDefaultBinding.Text;
      }
      else
      {
        col.DefaultBinding = cmbDefaultBinding.SelectedItem as string;
        col.Default = String.Empty;
      }
    }

  }


}
