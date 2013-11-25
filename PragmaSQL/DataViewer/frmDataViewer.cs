/********************************************************************
  Class      : frmTableEditor
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
using System.Threading;
using ICSharpCode.Core;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;
using AsynchronousCodeBlocks;

namespace PragmaSQL
{
	public partial class frmDataViewer : DockContent
	{
		private BindingSource _bs = new BindingSource();
		private SqlConnection _conn = null;
		private DataTable _dataTable = new DataTable("Data");
		private SqlDataAdapter _adapter = new SqlDataAdapter();
		private SqlCommandBuilder builder = new SqlCommandBuilder();
		/*
		private DataSet _schemaDS = new DataSet();
		private DataTable _schemaDT = null;
		*/

		//private string _autoIncColumnName = String.Empty;
		//private IList<string> _computedCols = new List<string>();

		private TableWrapper _tblSchema = new TableWrapper();
		private ColumnWrapper _idCol = null;

		private bool _generatedCommands = false;
		private bool _isLoading = false;

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
        lblHeader.Text = value;
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
        addRecordToolStripMenuItem.Enabled = !value;
				grd.ReadOnly = value;
			}
		}

		#endregion

		public frmDataViewer()
		{
			InitializeComponent();
			_bs.CurrentChanged += new EventHandler(OnBindingSourcePositionChanged);
			_adapter.RowUpdated += new SqlRowUpdatedEventHandler(OnDataAdapterRowUpdated);
		}

    private string _windowId = String.Empty;
		public void InitializeDataViewer(string windowId, string caption, string script, bool isReadonly, ConnectionParams connParams)
		{
			if (connParams == null)
			{
				throw new NullParameterException("Connection params object is null!");
			}

      _windowId = windowId;
      TableDataEditorManager.Remember(windowId, this);

			_connParams = connParams.CreateCopy();
			Caption = caption;
			Script = script;
			IsReadOnly = isReadonly;
		}

		public void LoadData(bool requiresNewConn)
		{
      if (requiresNewConn)
      {
        if (_conn != null)
        {
          _conn.Dispose();
        }

        _conn = _connParams.CreateSqlConnection(true, false);
      }
      
      _bs.DataSource = null;
			_dataTable.Clear();

			lblStatus.Text = "Loading...";
			Application.DoEvents();
			SynchronizationContext sc = SynchronizationContext.Current;
			btnStop.Enabled = true;

			Async obj = new Async
			(
				delegate
				{
					SqlCommand cmd = new SqlCommand(_script, _conn);
					cmd.CommandTimeout = 0;
					_adapter.SelectCommand = cmd;
					_isLoading = true;
					_adapter.Fill(_dataTable);
					GetTableSchema();
					sc.Send(
						delegate(object state)
						{
							lblStatus.Text = String.Empty;
							_bs.DataSource = _dataTable;
							grd.DataSource = _bs;
							navigator.BindingSource = _bs;

							//We make auto increment column read-only
							if (_idCol != null)
							{
								DataGridViewColumn col = grd.Columns[_idCol.Name];
								if (col != null)
								{
									col.ReadOnly = true;
									col.DefaultCellStyle.ForeColor = Color.Maroon;
								}
							}
							
							//We make computed columns read-only
							if (_tblSchema.HasComputedColumn())
							{
								foreach (ColumnWrapper cw in _tblSchema.Columns)
								{
									if (!cw.IsComputed)
										continue;
									DataGridViewColumn col = grd.Columns[cw.Name];
									if (col != null)
									{
										col.ReadOnly = true;
										col.DefaultCellStyle.ForeColor = Color.Maroon;
									}
								}
							}

							if (!_tblSchema.HasComputedColumn())
							{
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
						}, null
					);
				},
        delegate(Async objAsync)
				{
					sc.Send(
										 delegate(object state)
										 {
                       _isLoading = false;
											 btnStop.Enabled = false;
											 lblStatus.Text = String.Empty;
										 }, null
									 );
					if (objAsync.CodeException != null)
					{
						if(!IsDisposed)
              MessageService.ShowError(objAsync.CodeException.Message);
					}
				}
			);
		}

		private void GetTableSchema()
		{
			_idCol = null;
			if (_tblSchema.ConnectionParams == null)
				_tblSchema.ConnectionParams = _connParams;

			_tblSchema.Name = this.TableName;
			_tblSchema.LoadPropsUsingName();
			_tblSchema.LoadColumns();
			_idCol = _tblSchema.GetIdentityColumn();
		}

		private void UpdateRowToDatabase()
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
						SqlCommand insertCmd = SqlCommandGenerator.GenerateInsertCommand(_tblSchema, LastDataRow, _tableName);
						if (insertCmd == null)
						{
							throw new Exception("Can not apply deleted record to database!");
						}
						insertCmd.Connection = _conn;
						_adapter.InsertCommand = insertCmd;
						_adapter.Update(new DataRow[1] { LastDataRow });
						break;
					case DataRowState.Modified:
						SqlCommand updateCmd = SqlCommandGenerator.GenerateUpdateCommand(_tblSchema, LastDataRow, _tableName);
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
								MessageService.ShowError("This operation causes multiple rows (" + rowCnt.ToString() + ") to be altered."
									+ "\n"
									+ "Correct the errors and attempt to alter the row again.");
							}
							else if (rowCnt == 0)
							{
								tr.Rollback();
								LastDataRow.RejectChanges();
								MessageService.ShowError("Record can not be located."
									+ "\nPossibly row was already updated by another user/session.");
							}
							else
							{
								LastDataRow.AcceptChanges();
								tr.Commit();
							}
						}
						catch (Exception ex)
						{
							LastDataRow.RejectChanges();
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
							//&& !MessageService.AskQuestion("This operation will delete multiple rows!\r\nAre you sure you want to perform this operation?"))
							{
								tr.Rollback();
								LastDataRow.RejectChanges();
								MessageService.ShowError("This operation causes multiple rows (" + rowCnt.ToString() + ") to be deleted."
									+ "\n"
									+ "Correct the errors and attempt to delete the row again.");
							}
							else if (rowCnt == 0)
							{
								tr.Rollback();
								LastDataRow.RejectChanges();
								MessageService.ShowError("Record can not be located."
									+ "\nPossibly row was already deleted by another user/session.");
							}
							else
							{
								LastDataRow.AcceptChanges();
								tr.Commit();
							}
						}
						catch (Exception ex)
						{
							LastDataRow.RejectChanges();
							tr.Rollback();
							throw ex;
						}

						break;
					default:
						break;
				}
			}
		}

		private void OnDataAdapterRowUpdated(object sender, SqlRowUpdatedEventArgs e)
		{
			SqlCommand cmd = null;
			if ((e.Status == UpdateStatus.Continue) && (e.StatementType == StatementType.Insert))
			{
				//Fetch identity value after updating the row
				
				if (_idCol != null)
				{
					cmd = new SqlCommand("select @@IDENTITY", _conn);
					try
					{
						e.Row[_idCol.Name] = cmd.ExecuteScalar();
						e.Row.AcceptChanges();

					}
					finally
					{
						cmd.Dispose();
						cmd = null;
					}
				}

				if (_tblSchema.HasComputedColumn())
				{

					try
					{
						cmd = SqlCommandGenerator.GenerateSelectCommand(_tblSchema, e.Row, false);
						cmd.Connection = _conn;

						SqlDataReader reader = cmd.ExecuteReader();
						try
						{
							bool isSecondRow = false;
							while (reader.Read())
							{
								if (isSecondRow)
								{
									e.Row.RejectChanges();
									MessageService.ShowWarning("More than one row returned while fetching computed column values."
										+ "\r\nPelase reload table data to view correct values of the computed columns.");
									break;
								}

								foreach (ColumnWrapper cCol in _tblSchema.ComputedCols.Values)
								{
									e.Row[cCol.Name] = reader[cCol.Name];
								}
								e.Row.AcceptChanges();
								isSecondRow = true;
							}
						}
						finally
						{
							if (reader != null && !reader.IsClosed)
							{
								reader.Close();
								reader.Dispose();
							}
							reader = null;
						}
					}
					finally
					{
						if (cmd != null)
						{
							cmd.Dispose();
						}
						cmd = null;
					}
				}

				_bs.ResetCurrentItem();
			}
		}

		private void OnBindingSourcePositionChanged(object sender, EventArgs e)
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

		private void DeleteSelectedRows()
		{
			if (grd.SelectedRows.Count == 0)
			{
				return;
			}


			if (!MessageService.AskQuestion("Delete selected row(s)?"))
			{
				return;
			}

			foreach (DataGridViewRow row in grd.SelectedRows)
			{
				if (row.IsNewRow)
				{
					continue;
				}
				grd.Rows.Remove(row);
			}
			if (_generatedCommands)
			{
				_adapter.Update(_dataTable);
			}
		}

    private void CopySelectionToClipboard()
    {
      DataObject dtObj = grd.GetClipboardContent();
      if (dtObj == null)
      {
        return;
      }
      Clipboard.SetDataObject(dtObj);
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

		private void bindingNavigatorRefresh_Click(object sender, EventArgs e)
		{
			LoadData(false);
		}

		private void grd_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			string errText = String.Empty;

			if ((e.Context & DataGridViewDataErrorContexts.Commit) == DataGridViewDataErrorContexts.Commit)
			{
				if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
				{
					e.ThrowException = false;
					errText = "Error for column\"" + grd.Columns[e.ColumnIndex].DataPropertyName + "\""
						+ "\nMessage:" + e.Exception.Message;
					grd.Rows[e.RowIndex].ErrorText = errText;
				}
				else if (e.RowIndex >= 0 && e.ColumnIndex < 0)
				{
					e.ThrowException = false;
					errText = e.Exception.Message;
					grd.Rows[e.RowIndex].ErrorText = errText;
				}
				else if (e.RowIndex < 0 && e.ColumnIndex >= 0)
				{
					e.ThrowException = true;
				}
				else if (e.RowIndex < 0 && e.ColumnIndex < 0)
				{
					e.ThrowException = true;
				}
				System.Media.SystemSounds.Exclamation.Play();
			}
		}

		private void actDeleteSelected_Update(object sender, EventArgs e)
		{
			actDeleteSelected.Enabled = (grd.SelectedRows.Count > 0) && (!IsReadOnly);
		}

		private void actDeleteSelected_Execute(object sender, EventArgs e)
		{
			DeleteSelectedRows();
		}

		private void grd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex < 0)
				return;



			DataGridViewColumn col = grd.Columns[e.ColumnIndex];
			bool isCheckBoxCell = col is DataGridViewCheckBoxColumn;
			if (isCheckBoxCell && col.SortMode != DataGridViewColumnSortMode.Automatic)
				col.SortMode = DataGridViewColumnSortMode.Automatic;

			if ((e.Value != null) && (e.Value.GetType() == typeof(DBNull)))
			{
				Color gridBrushColor = ((DataGridView)sender).GridColor;
				Color bgColor = Color.LemonChiffon;

				if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
				{
					bgColor = SystemColors.Highlight;
				}

				if (isCheckBoxCell)
				{
					e.CellStyle.BackColor = bgColor;
					return;
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

						// Draw the text content of the cell, ignoring alignment.
						Brush br = null;
						if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected)
						{
							if (col.ReadOnly)
							{
								br = Brushes.Maroon;
							}
							else
							{
								br = Brushes.Black;
							}
						}
						else
						{
							br = SystemBrushes.HighlightText;
						}

            SizeF sizeF = e.Graphics.MeasureString("(NULL)", e.CellStyle.Font);

            /*
            e.Graphics.DrawString((string)"(NULL)", e.CellStyle.Font,
                br,e.CellBounds.X + 2,
                e.CellBounds.Y + 2, StringFormat.GenericDefault);
            */
            e.Graphics.DrawString((string)"(NULL)", e.CellStyle.Font,
                br, e.CellBounds.Left + (e.CellBounds.Width - sizeF.Width) / 2,
                e.CellBounds.Top + (e.CellBounds.Height - sizeF.Height) / 2, StringFormat.GenericDefault);

            e.Handled = true;
          }

				}
			}
		}

		private void grd_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			grd.Rows[e.RowIndex].ErrorText = String.Empty;
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			if (_isLoading)
			{
				_adapter.SelectCommand.Cancel();
			}
		}

    private void frmDataViewer_FormClosed(object sender, FormClosedEventArgs e)
    {
      TableDataEditorManager.Forget(_windowId);
      if (_conn != null)
      {
        if (_conn.State == ConnectionState.Open)
          _conn.Close();
        _conn.Dispose();
      }
    }

    private void addRecordToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _bs.AddNew();
    }

    private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      grd.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      CopySelectionToClipboard();
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      grd.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
      CopySelectionToClipboard();
    }

	}
}