using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using PragmaSQL.Core;

namespace PragmaSQL.VirtualResultRenderers
{
	public partial class VirtualDataGridView : UserControl
	{
		#region Fields And Properties

		public DataGridView Grid
		{
			get { return grd; }
		}

		private DataTableFilterSortData _filterSortData = new DataTableFilterSortData();



		private DataTable _sourceDataTable = null;
		private DataTable _prevUnsortedTable = null;
		
		private DataTable _activeDataTable = null;
		private DataTable ActiveDataTable
		{
			get { return _activeDataTable; }
			set 
			{
				if (_activeDataTable != null && _activeDataTable != _sourceDataTable)
				{
					if (_activeDataView != null)
						_activeDataView.Dispose();

					_activeDataTable.Dispose();
				}

				_activeDataTable = value;
			}
		}

		public ContextMenuStrip PopupMenu
		{
			get { return popUpGrid; }
		}

		private DataView _activeDataView = null;
		public DataView ActiveDataView
		{
			get
			{
				return _activeDataView;
			}
		}

		private bool _statusAlwaysVisible = true;
		public bool StatusAlwaysVisible
		{
			get { return _statusAlwaysVisible; }
			set { _statusAlwaysVisible = value; }
		}

		private bool _progressStatusEnabled = true;
		public bool ProgressStatusEnabled
		{
			get { return _progressStatusEnabled; }
			set { _progressStatusEnabled = value; }
		}

		#endregion //Fields And Properties

		#region CTOR
		public VirtualDataGridView()
		{
			InitializeComponent();
			ddlFilterType.SelectedIndex = 0;

			grd.GotFocus += new EventHandler(grd_GotFocus);

		}

		void grd_GotFocus(object sender, EventArgs e)
		{
			this.OnGotFocus(EventArgs.Empty);
		}

		#endregion //CTOR

		#region Result Filtering And Sorting

		private void InitializeFilterSortToolStripData()
		{
			ddlFilterType.Text = _filterSortData.FilterType;
			txtFilterDt.Text = _filterSortData.EditingFilterExpression;

			txtFilterDt.AutoCompleteCustomSource.Clear();
			txtFilterDt.AutoCompleteCustomSource.AddRange(DataTableFilterSortData.ListToStringArray(_filterSortData.FilterExpressions));

			RefreshResultFilterStatus();
		}

		private void ResetFilterSortToolStripData()
		{
			ddlFilterType.SelectedIndex = 0;

			txtFilterDt.Text = String.Empty;

			statLblFilterDt.Text = String.Empty;
			statLblFilterDt.Text = String.Empty;
			statLblSortDt.Text = String.Empty;
		}

		private void CustomFilterDataTable(string filterExp)
		{
			if (String.IsNullOrEmpty(filterExp))
				return;

			if (_activeDataTable == null)
				return;

			DataView v = NeedNewActiveDataView();

			v.RowFilter = filterExp;
			ActiveDataTable = v.ToTable();
			grd.RowCount = ActiveDataTable.Rows.Count;

			if (txtFilterDt.AutoCompleteCustomSource.IndexOf(filterExp) == -1)
				txtFilterDt.AutoCompleteCustomSource.Add(filterExp);

			_filterSortData.CurrentFilterExpression = filterExp;
			_filterSortData.FilterExpressions.Add(filterExp);
			_filterSortData.FilterType = "Custom";
			RefreshResultFilterStatus();
			grd.DefaultCellStyle.BackColor = Color.FromArgb(240, 242, 242);
		}

		private void LikeSearchDataTable(string filterExp)
		{
			if (String.IsNullOrEmpty(filterExp))
				return;

			if (_activeDataTable == null)
				return;


			DataView v = NeedNewActiveDataView();

			string or = String.Empty;
			string finalExpression = String.Empty;

			foreach (DataColumn col in ActiveDataTable.Columns)
			{
				if (col.DataType != typeof(string))
					continue;
				finalExpression = finalExpression + or + String.Format("{0} Like('{1}%')", col.ColumnName, filterExp);
				or = " OR ";
			}

			v.RowFilter = finalExpression;
			ActiveDataTable = v.ToTable();
			grd.RowCount = ActiveDataTable.Rows.Count;

			if (txtFilterDt.AutoCompleteCustomSource.IndexOf(filterExp) == -1)
				txtFilterDt.AutoCompleteCustomSource.Add(filterExp);

			_filterSortData.CurrentFilterExpression = filterExp;
			_filterSortData.FilterExpressions.Add(filterExp);
			_filterSortData.FilterType = "Like";

			RefreshResultFilterStatus();
			grd.DefaultCellStyle.BackColor = Color.FromArgb(240, 242, 242);
		}

		private void ClearFilter()
		{
			Cursor currentCursor = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor; ShowStatusProgress("Clearing filter...");
				grd.DefaultCellStyle.BackColor = Color.White;
				_filterSortData.CurrentFilterExpression = String.Empty;
				if (!String.IsNullOrEmpty(_filterSortData.CurrenSortExpression))
				{
					ActiveDataTable = _sourceDataTable;
					DataView v = NeedNewActiveDataView();
					v.Sort = _filterSortData.CurrenSortExpression;
					ActiveDataTable = v.ToTable();
					_prevUnsortedTable = null;
				}
				else
					ActiveDataTable = _sourceDataTable;

				if (ActiveDataTable != null)
					grd.RowCount = ActiveDataTable.Rows.Count;
				else
					grd.RowCount = 0;
			}
			finally
			{
				Cursor.Current = currentCursor;
				HideStatusProgress();
				RefreshResultFilterStatus();
				
			}
		}

		private void ClearSort()
		{
			Cursor currentCursor = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ShowStatusProgress("Clearing sort...");
				if (_prevUnsortedTable != null)
				{
					_activeDataTable = _prevUnsortedTable;
					_prevUnsortedTable = null;
				}
				else
					_activeDataTable = _sourceDataTable;

				grd.RowCount = _activeDataTable.Rows.Count;
				_filterSortData.CurrenSortExpression = String.Empty;
			}
			finally
			{
				Cursor.Current = currentCursor;
				HideStatusProgress();
				RefreshResultSortStatus();
				grd.Invalidate();
			}
		}

		private void DoFilterDataTable()
		{
			if (String.IsNullOrEmpty(txtFilterDt.Text))
			{
				ClearFilter();
				return;
			}

			Cursor currentCursor = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor; 
				ShowStatusProgress("Filtering data...");
				if (ddlFilterType.Text == "Custom")
				{
					CustomFilterDataTable(txtFilterDt.Text);
				}
				else if (ddlFilterType.Text == "Like")
				{
					LikeSearchDataTable(txtFilterDt.Text);
				}
			}
			catch (Exception ex)
			{
				GenericErrorDialog.ShowError("Filter Error", "Invalid filter expression", ex.Message);
			}
			finally
			{
				HideStatusProgress();
				Cursor.Current = currentCursor;
			}
		}


		private void RefreshResultRecordCountStatus()
		{
			statLblRecordCount.Text = String.Format("{0} row(s)", grd.RowCount);
		}

		private void RefreshResultFilterStatus()
		{
			string summary = (String.IsNullOrEmpty(_filterSortData.CurrentFilterExpression) || _filterSortData.CurrentFilterExpression.Length < 50) ? _filterSortData.CurrentFilterExpression : (_filterSortData.CurrentFilterExpression.Substring(0, 50) + "...");

			if (_filterSortData.FilterType == "Like")
				statLblFilterDt.Text = String.IsNullOrEmpty(_filterSortData.CurrentFilterExpression) ? String.Empty : String.Format("Like ('{0}%')", summary);
			else
				statLblFilterDt.Text = summary;


			statLblFilter.Text = String.Format("Filter ({0}) : ", _filterSortData.FilterType);
			statLblFilterDt.ToolTipText = _filterSortData.CurrentFilterExpression;

			RefreshResultRecordCountStatus();
			EvaluateResultStatusVisibility();
		}

		private void RefreshResultSortStatus()
		{
			DataTableFilterSortData data = _filterSortData;
			string summary = (String.IsNullOrEmpty(data.CurrenSortExpression) || data.CurrenSortExpression.Length < 50) ? data.CurrenSortExpression : (data.CurrenSortExpression.Substring(0, 50) + "...");
			statLblSortDt.Text = summary;
			statLblSortDt.ToolTipText = data.CurrenSortExpression;

			EvaluateResultStatusVisibility();
		}

		private void EvaluateResultStatusVisibility()
		{
			statLblFilterDt.Visible = !String.IsNullOrEmpty(statLblFilterDt.Text);
			statLblFilter.Visible = statLblFilterDt.Visible;
			statBtnClearFilterDt.Visible = statLblFilterDt.Visible;
			statFilterSep.Visible = statLblFilterDt.Visible;

			statLblSortDt.Visible = !String.IsNullOrEmpty(statLblSortDt.Text);
			statLblSort.Visible = statLblSortDt.Visible;
			statBtnClearSortDt.Visible = statLblSortDt.Visible;
			statSortSep.Visible = statLblFilterDt.Visible && statLblSortDt.Visible;
		}


		private void ShowQuickResultDataFilterDlg()
		{
			if (_activeDataTable == null)
				return;

			DataView v = NeedNewActiveDataView();
			string filter = String.Empty;

			if (DataFilterDlg.ShowDataFilterDlg(v, out filter))
			{
				txtFilterDt.Text = filter;
				DoFilterDataTable();
			}
		}
		#endregion //Result Filtering And Sorting

		#region Utilities

		private void CopyGridContentToClipboard(DataGridViewClipboardCopyMode mode)
		{
			if (grd != null)
			{
				grd.ClipboardCopyMode = mode;
				DataObject dtObj = grd.GetClipboardContent();
				if (dtObj == null)
				{
					return;
				}
				Clipboard.SetDataObject(dtObj);
				return;
			}
		}

		private void PrintGrid(bool isPreview)
		{

			InputDialog inputDlg = new InputDialog();
			inputDlg.Text = "Print Document Info";
			inputDlg.InitializeInputEdits(new string[2] { "Document Name", "Document Title" }, new string[] { "Document", "Title" });
			if (inputDlg.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			if (SetupThePrinting(grd, inputDlg["Document Name"], inputDlg["Document Title"]))
			{
				if (isPreview)
				{
					PrintPreviewDialog printPreviewDlg = new PrintPreviewDialog();
					printPreviewDlg.Document = printDocument1;
					printPreviewDlg.ShowDialog();
				}
				else
				{
					printDocument1.Print();
				}
			}

		}

		private DataGridViewPrinter _dataGridViewPrinter;
		private bool SetupThePrinting(DataGridView grd, string docName, string title)
		{
			PrintDialog printDlg = new PrintDialog();
			printDlg.AllowCurrentPage = false;
			printDlg.AllowPrintToFile = false;
			printDlg.AllowSelection = false;
			printDlg.AllowSomePages = false;
			printDlg.PrintToFile = false;
			printDlg.ShowHelp = false;
			printDlg.ShowNetwork = false;

			if (printDlg.ShowDialog() != DialogResult.OK)
			{
				return false;
			}

			printDocument1.DocumentName = docName;
			printDocument1.PrinterSettings = printDlg.PrinterSettings;
			printDocument1.DefaultPageSettings = printDlg.PrinterSettings.DefaultPageSettings;
			printDocument1.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

			_dataGridViewPrinter = new DataGridViewPrinter(grd, printDocument1, true, true, title, new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
			return true;
		}

		private void ExportGridToFile()
		{
			if (_activeDataTable == null)
				throw new Exception("DataTable can not be extracted from the current gird!");

			DataExport.ExportGridToFile(_activeDataTable);
		}

		private DataView NeedNewActiveDataView()
		{
			if (_activeDataView != null)
			{
				_activeDataView.Dispose();
			}

			if (_activeDataTable == null)
				_activeDataView = null;
			else
				_activeDataView = new DataView(_activeDataTable);

			return _activeDataView;
		}

		private void ShowStatusProgress(string msg)
		{
			if (!ProgressStatusEnabled)
				return;

			if (String.IsNullOrEmpty(msg))
			{
				lblWait.Visible = false;
				return;
			}

			lblWait.Visible = true;
			lblWait.Text = msg;
			Application.DoEvents();
		}

		private void HideStatusProgress()
		{
			lblWait.Text = String.Empty;
			lblWait.Visible = false;
			Application.DoEvents();
		}

		#endregion //Utilities

		#region Public Methods

		public void RenderDataTable(DataTable dataTable)
		{
			_sourceDataTable = dataTable;
			ActiveDataTable = dataTable;

			CreateColumns();
			_filterSortData = new DataTableFilterSortData();
			InitializeFilterSortToolStripData();
		}

		private void CreateColumns()
		{
			grd.Columns.Clear();
			DataGridViewColumn dgCol = null;

			foreach (DataColumn col in ActiveDataTable.Columns)
			{
				dgCol = new DataGridViewTextBoxColumn();
				dgCol.DataPropertyName = col.ColumnName;
				dgCol.HeaderText = col.ColumnName;
				dgCol.SortMode = DataGridViewColumnSortMode.Automatic;
				dgCol.SortMode = DataGridViewColumnSortMode.Programmatic;
				grd.Columns.Add(dgCol);
			}

			grd.RowCount = ActiveDataTable != null ? ActiveDataTable.Rows.Count : 0;
		}

		#endregion //Public Methods

		private void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex >= 0)
			{
				DataGridViewColumn col = ((DataGridView)sender).Columns[e.ColumnIndex];
				if (col.ValueType == typeof(bool) && col.SortMode != DataGridViewColumnSortMode.Automatic)
					col.SortMode = DataGridViewColumnSortMode.Automatic;
			}

			if (e.ColumnIndex == -1)
			{
				Color bgColor = SystemColors.Control;
				Color foreColor = Color.Black;

				if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
				{
					bgColor = SystemColors.Highlight;
					foreColor = SystemColors.HighlightText;
				}

				Color gridBrushColor = ((DataGridView)sender).GridColor;
				using (Brush gridBrush = new SolidBrush(gridBrushColor))
				{
					using (Pen gridLinePen = new Pen(gridBrush))
					{
						e.Graphics.FillRectangle(new SolidBrush(bgColor), e.CellBounds);
						// Draw the grid lines (only the right and bottom lines;
						// DataGridView takes care of the others).
						e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
							e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
							e.CellBounds.Bottom - 1);
						e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
							e.CellBounds.Top, e.CellBounds.Right - 1,
							e.CellBounds.Bottom);

						if (e.RowIndex != -1)
						{
							e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.CellStyle.Font,
											new SolidBrush(foreColor), e.CellBounds.X + 2,
											e.CellBounds.Y + 2, StringFormat.GenericDefault);
						}
						e.Handled = true;
					}
				}
			}
			else if ((e.Value != null) && (e.Value.GetType() == typeof(DBNull)))
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

		private void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		private void grd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DataTableFilterSortData data = _filterSortData;
			if (data == null || ActiveDataTable == null)
				return;

			if (String.IsNullOrEmpty(data.CurrenSortExpression))
				_prevUnsortedTable = ActiveDataTable;

			DataGridViewColumn col = grd.Columns[e.ColumnIndex];

			DataView v = NeedNewActiveDataView();

			Cursor currentCursor = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor; 
				ShowStatusProgress("Sorting data...");

				if (Control.ModifierKeys == Keys.Shift)
				{
					if (data.SortCols.ContainsKey(col.DataPropertyName))
						data.SortCols.Remove(col.DataPropertyName);
				}
				else if (Control.ModifierKeys != Keys.Control)
				{
					SortOrder sortOrder = SortOrder.None;

					if (data.SortCols.ContainsKey(col.DataPropertyName) && data.SortCols.Count == 1)
						sortOrder = data.SortCols[col.DataPropertyName];

					data.SortCols.Clear();
					data.SortCols.Add(col.DataPropertyName, sortOrder);
					data.PrepareSortOrderFor(col.DataPropertyName);
				}
				else
					data.PrepareSortOrderFor(col.DataPropertyName);


				v.Sort = data.GenerateSortExpression();
				ActiveDataTable = v.ToTable();
				grd.RowCount = ActiveDataTable.Rows.Count;

				data.CurrenSortExpression = v.Sort;
			}
			finally
			{
				Cursor.Current = currentCursor;
				HideStatusProgress();
				RefreshResultSortStatus();
				grd.Invalidate();
			}
		}

		private void grd_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
		{
			if (e.RowIndex < 0)
				e.ToolTipText = Properties.Resources.MultiColumnSortTooltip;
		}

		private void btnFilterDt_Click(object sender, EventArgs e)
		{
			DoFilterDataTable();
		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			ShowQuickResultDataFilterDlg();
		}

		private void toolStripButton5_Click(object sender, EventArgs e)
		{
			ClearFilter();
		}

		private void statBtnClearFilterDt_Click(object sender, EventArgs e)
		{
			ClearFilter();
		}

		private void statBtnClearSortDt_Click(object sender, EventArgs e)
		{
			ClearSort();
		}

		private void txtFilterDt_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				DoFilterDataTable();
		}

		private void popUpItemCopyGridToClipboard_Click(object sender, EventArgs e)
		{
			CopyGridContentToClipboard(DataGridViewClipboardCopyMode.EnableWithoutHeaderText);
		}

		private void popUpItemCopyGridToClipboardWithCols_Click(object sender, EventArgs e)
		{
			CopyGridContentToClipboard(DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText);
		}

		private void quickFilterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowQuickResultDataFilterDlg();
		}

		private void popUpFilterClear_Click(object sender, EventArgs e)
		{
			ClearFilter();
		}

		private void popUpSortClear_Click(object sender, EventArgs e)
		{
			ClearSort();
		}

		private void popUpGrid_Opening(object sender, CancelEventArgs e)
		{
			DataTableFilterSortData data = _filterSortData;
			popUpFilterClear.Visible = (data != null && !String.IsNullOrEmpty(data.CurrentFilterExpression));
			popUpSortClear.Visible = (data != null && !String.IsNullOrEmpty(data.CurrenSortExpression));
		}

		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrintGrid(true);
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrintGrid(false);
		}

		private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExportGridToFile();
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			e.HasMorePages = _dataGridViewPrinter.DrawDataGridView(e.Graphics);
		}

		private void popUpGrid_Opened(object sender, EventArgs e)
		{
			if (this.Visible)
				this.Focus();
		}

		private void grd_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
		{
			//if (e.Value == null || e.Value.GetType() == typeof(DBNull))
			//{
			//  grd[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.LemonChiffon;
			//}
		}

		private void grd_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			if (ActiveDataTable == null || ActiveDataTable.Rows.Count == 0) // || e.RowIndex >= DataTable.Rows.Count || e.ColumnIndex >= DataTable.Columns.Count)
				return;

			e.Value = ActiveDataTable.Rows[e.RowIndex].ItemArray[e.ColumnIndex];
		}
	}
}
