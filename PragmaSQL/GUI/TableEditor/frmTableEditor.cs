/********************************************************************
  Class      : frmTableEditor
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI;
using PragmaSQL.Database;
using PragmaSQL.Common;

namespace PragmaSQL.GUI
{
  public partial class frmTableEditor : DockContent
  {
    private BindingSource _bs = new BindingSource();
    private SqlConnection _conn = new SqlConnection();
    private DataTable _dataTable = new DataTable("Data");
    private SqlDataAdapter _adapter = new SqlDataAdapter();
    private SqlCommandBuilder builder = new SqlCommandBuilder();
    private DataSet _schemaDS = new DataSet();
    private DataTable _schemaDT = null;
    private string _autoIncColumnName = String.Empty;

    private bool _generatedCommands = false;

    //tracks for PositionChanged event last row
    private DataRow LastDataRow = null;

    #region Properties
    private string _tableName = String.Empty;
    public string TableName
    {
      get { return _tableName; }
      set { _tableName = value; }
    }

    private string _script = String.Empty;
    public string Script
    {
      get { return _script; }
      set { _script = value; }
    }

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
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

    private bool _isReadOnly = false;
    public bool IsReadOnly
    {
      get { return _isReadOnly; }
      set
      {
        _isReadOnly = value;
        bindingNavigatorAddNewItem.Visible = !value;
        bindingNavigatorSeparator2.Visible = !value;
        btnDelete.Visible = !value;

        grd.AllowUserToAddRows = !value;
        //grd.AllowUserToDeleteRows = !value;
        grd.ReadOnly = value;
      }
    }

    #endregion

    public frmTableEditor( )
    {
      InitializeComponent();
      _bs.CurrentChanged += new EventHandler(OnBindingSourcePositionChanged);
      _adapter.RowUpdated += new SqlRowUpdatedEventHandler(OnDataAdapterRowUpdated);
    }

    public void InitializeDataEditor( string caption, string script, bool isReadonly, ConnectionParams connParams )
    {
      if (connParams == null)
      {
        throw new NullParameterException("Connection params object is null!");
      }

      _connParams = connParams.CreateCopy();
      Caption = caption;
      Script = script;
      IsReadOnly = isReadonly;
    }

    public void LoadData( )
    {
      if (_conn.State != ConnectionState.Open)
      {
        _conn.ConnectionString = _connParams.ConnectionString;
        _conn.Open();
      }

      _dataTable.Clear();

      SqlCommand cmd = new SqlCommand(_script, _conn);
      _adapter.SelectCommand = cmd;
      _adapter.Fill(_dataTable);
      GetTableSchema();

      _bs.DataSource = _dataTable;
      grd.DataSource = _bs;
      navigator.BindingSource = _bs;
      grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

      if (!String.IsNullOrEmpty(_autoIncColumnName))
      {
        DataGridViewColumn col = grd.Columns[_autoIncColumnName];
        if (col != null)
        {
          col.ReadOnly = true;
          col.DefaultCellStyle.ForeColor = Color.Gray;
        }
      }

      try
      {
        _generatedCommands = false;
        builder.DataAdapter = _adapter;
        _adapter.InsertCommand = builder.GetInsertCommand();
        _adapter.UpdateCommand = builder.GetUpdateCommand();
        _adapter.DeleteCommand = builder.GetDeleteCommand();
        _generatedCommands = true;
      }
      catch { }
    }

    private void GetTableSchema( )
    {
      _autoIncColumnName = String.Empty;
      _adapter.FillSchema(_schemaDS, SchemaType.Source);
      _schemaDT = _schemaDS.Tables[0];
      foreach (DataColumn col in _schemaDT.Columns)
      {
        if (col.AutoIncrement)
        {
          _autoIncColumnName = col.ColumnName;
          break;
        }
      }
    }

    private void UpdateRowToDatabase( )
    {
      if (LastDataRow != null)
      {
        if (LastDataRow.RowState == DataRowState.Unchanged)
        {
          return;
        }

        if (_generatedCommands)
        {
          _adapter.Update(new DataRow[1] { LastDataRow });
          _bs.ResetCurrentItem();
          return;
        }

        SqlTransaction tr = null;
        switch (LastDataRow.RowState)
        {
          case DataRowState.Added:
            SqlCommand insertCmd = SqlCommandGenerator.GenerateInsertCommand(_dataTable.Columns, LastDataRow, _tableName);
            if (insertCmd == null)
            {
              throw new Exception("Can not apply deleted record to database!");
            }
            insertCmd.Connection = _conn;
            _adapter.InsertCommand = insertCmd;
            _adapter.Update(new DataRow[1] { LastDataRow });
            break;
          case DataRowState.Modified:
            SqlCommand updateCmd = SqlCommandGenerator.GenerateUpdateCommand(_dataTable.Columns, LastDataRow, _tableName);
            if (updateCmd == null)
            {
              throw new Exception("Can not apply updated record to database!");
            }

            updateCmd.Connection = _conn;
            _adapter.UpdateCommand = updateCmd;


            try
            {
              tr = _conn.BeginTransaction();
              updateCmd.Transaction = tr;
              int rowCnt = updateCmd.ExecuteNonQuery();
              if (rowCnt > 1)
              {
                tr.Rollback();
                LastDataRow.RejectChanges();
                MessageBox.Show("The row value(s) updated alter multiple rows(" + rowCnt.ToString() + ")."
                  + "\n"
                  + "Correct the errors and attempt to update the row again."
                  , "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
              else if (rowCnt == 0)
              {
                tr.Rollback();
                LastDataRow.RejectChanges();
                MessageBox.Show("Record can not be located."
                  + "\nPossibly row was already updated by another user/session."
                  , "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
              else
              {
                tr.Commit();
              }
            }
            catch (Exception ex)
            {
              tr.Rollback();
              throw ex;
            }
            break;
          case DataRowState.Deleted:
            SqlCommand deleteCmd = SqlCommandGenerator.GenerateDeleteCommand(_dataTable.Columns, LastDataRow, _tableName);
            if (deleteCmd == null)
            {
              throw new Exception("Can not apply deleted record to database!");
            }

            deleteCmd.Connection = _conn;
            _adapter.DeleteCommand = deleteCmd;
            try
            {
              tr = _conn.BeginTransaction();
              deleteCmd.Transaction = tr;
              int rowCnt = deleteCmd.ExecuteNonQuery();
              if (rowCnt > 1)
              {
                tr.Rollback();
                LastDataRow.RejectChanges();
                MessageBox.Show("The row value(s) deleted alter multiple rows(" + rowCnt.ToString() + ")."
                  + "\n"
                  + "Correct the errors and attempt to delete the row again."
                  , "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
              else if (rowCnt == 0)
              {
                tr.Rollback();
                LastDataRow.RejectChanges();
                MessageBox.Show("Record can not be located."
                  + "\nPossibly row was already deleted by another user/session."
                  , "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
              else
              {
                tr.Commit();
              }
            }
            catch (Exception ex)
            {
              tr.Rollback();
              throw ex;
            }

            break;
          default:
            break;
        }
      }
    }

    private void OnDataAdapterRowUpdated( object sender, SqlRowUpdatedEventArgs e )
    {
      if ((!String.IsNullOrEmpty(_autoIncColumnName)) && (e.Status == UpdateStatus.Continue) && (e.StatementType == StatementType.Insert))
      {
        SqlCommand cmd =new SqlCommand("select @@IDENTITY", _conn);
        e.Row[_autoIncColumnName] = cmd.ExecuteScalar();
        e.Row.AcceptChanges();
        _bs.ResetCurrentItem();
      }
    }

    private void OnBindingSourcePositionChanged( object sender, EventArgs e )
    {
      DataRow ThisDataRow = null;
      // if the user moves to a new row, check if the 
      // last row was changed
      BindingSource thisBindingSource = (BindingSource)sender;
      if (thisBindingSource.Current == null)
      {
        ThisDataRow = null;
      }
      else
      {
        ThisDataRow = ((DataRowView)thisBindingSource.Current).Row;
      }

      UpdateRowToDatabase();
      // track the current row for next 
      // PositionChanged event
      LastDataRow = ThisDataRow;
    }

    private void DeleteSelectedRows( )
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Delete selected row(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dlgRes == DialogResult.No)
      {
        return;
      }

      foreach(DataGridViewRow row in grd.SelectedRows)
      {
        if(row.IsNewRow)
        {
          continue;
        }
        grd.Rows.Remove(row);
      }
      if(_generatedCommands)
      {
        _adapter.Update(_dataTable);
      }
    }

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Program.MainForm.CloseDocuments(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Program.MainForm.CloseDocuments(this);
    }

    private void bindingNavigatorRefresh_Click( object sender, EventArgs e )
    {
      LoadData();
    }

    private void grd_DataError( object sender, DataGridViewDataErrorEventArgs e )
    {
      e.ThrowException = false;
      MessageBox.Show(e.Exception.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void actDeleteSelected_Update( object sender, EventArgs e )
    {
      actDeleteSelected.Enabled = (grd.SelectedRows.Count > 0) && (!IsReadOnly);
    }

    private void actDeleteSelected_Execute( object sender, EventArgs e )
    {
      DeleteSelectedRows();
    }

    private void grd_CellPainting( object sender, DataGridViewCellPaintingEventArgs e )
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

  }
}