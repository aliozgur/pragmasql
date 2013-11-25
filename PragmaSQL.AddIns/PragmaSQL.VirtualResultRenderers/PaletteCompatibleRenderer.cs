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
	public partial class PaletteCompatibleRenderer : UserControl
	{
		#region Fields And Properties

		public DataGridView Grid
		{
			get { return grd; }
		}

		private DataTableFilterSortData _filterSortData = new DataTableFilterSortData();

		public DataView DataView
		{
			get
			{
				DataTable dt = grd.DataSource as DataTable;

				if (dt == null)
					return null;

				return dt.DefaultView;
			}
		}

		public DataTable DataTable
		{
			get
			{
				return grd.DataSource as DataTable;
			}
		}

		public ContextMenuStrip PopupMenu
		{
			get { return popUpGrid; }
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
    public PaletteCompatibleRenderer()
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

			DataView v = DataView;
			if (v == null)
				return;


			v.RowFilter = filterExp;

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

			DataTable dt = DataTable;
			if (dt == null)
				return;

			DataView v = DataView;
			if (v == null)
				return;

			string or = String.Empty;
			string finalExpression = String.Empty;

			foreach (DataColumn col in dt.Columns)
			{
				if (col.DataType != typeof(string))
					continue;
				finalExpression = finalExpression + or + String.Format("{0} Like('{1}%')", col.ColumnName, filterExp);
				or = " OR ";
			}

			v.RowFilter = finalExpression;

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
				Cursor.Current = Cursors.WaitCursor;

				ShowStatusProgress("Clearing filter...");
				grd.DefaultCellStyle.BackColor = Color.White;

				if (DataTable == null)
					return;

				DataView.RowFilter = String.Empty;
				_filterSortData.CurrentFilterExpression = String.Empty;
				RefreshResultFilterStatus();
			}
			finally
			{
				Cursor.Current = currentCursor;
				HideStatusProgress();
			}
		}

		private void ClearSort()
		{
			DataView v = DataView;
			if (v == null)
				return;
			
			Cursor currentCursor = Cursor.Current; 
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				ShowStatusProgress("Clearing sort");
				v.Sort = String.Empty;
				_filterSortData.CurrenSortExpression = String.Empty;
				RefreshResultSortStatus();
			}
			finally
			{
				Cursor.Current = currentCursor; 
				HideStatusProgress();
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
				Cursor.Current = currentCursor;
				HideStatusProgress();
			}
		}


		private void RefreshResultRecordCountStatus()
		{
			DataView v = DataView;
			statLblRecordCount.Text = v == null ? String.Empty : String.Format("{0} row(s)", v.Count);
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
			DataView v = DataView;
			string filter = String.Empty;
			if (v == null)
				return;

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
			DataTable dt = null;
			DataView view = DataView;
			if (view != null && !String.IsNullOrEmpty(view.RowFilter))
				dt = view.ToTable();
			else
				dt = DataTable;

			if (dt == null)
				throw new Exception("DataTable can not be extracted from the current gird!");

			DataExport.ExportGridToFile(dt);
		}

		private void ShowStatusProgress(string msg)
		{
			if (!_progressStatusEnabled)
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
			grd.DataSource = dataTable;
			foreach(DataGridViewColumn col in grd.Columns)
				col.SortMode = DataGridViewColumnSortMode.Programmatic;

			_filterSortData = new DataTableFilterSortData();
			InitializeFilterSortToolStripData();
		}

		#endregion //Public Methods

		private void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
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
            if (e.RowIndex != -1)
            {
              e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.CellStyle.Font,
                      new SolidBrush(foreColor), e.CellBounds.X + 10,
                      e.CellBounds.Y + 3, StringFormat.GenericDefault);
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
			if (data == null)
				return;

			DataGridViewColumn col = grd.Columns[e.ColumnIndex];
			DataView v = DataView;
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
					//data.SortCols.Clear();
					//data.PrepareSortOrderFor(col.DataPropertyName);
					//data.SortCols[col.DataPropertyName] = grd.SortOrder;
					//data.CurrenSortExpression = data.GenerateSortExpression();
					//return;

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
				data.CurrenSortExpression = v.Sort;
			}
			finally
			{
				Cursor.Current = currentCursor;
				HideStatusProgress();
				RefreshResultSortStatus();
			}
		}

		private void grd_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
		{
      if (e.RowIndex < 0)
        e.ToolTipText = Properties.Resources.MultiColumnSortTooltip;
      else
        e.ToolTipText = String.Format("Record #{0}", e.RowIndex+1);
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
	}
}
