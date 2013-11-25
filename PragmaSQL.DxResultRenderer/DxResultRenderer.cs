using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PragmaSQL.Core;
using System.Reflection;
using System.IO;

using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace PragmaSQL.DxResultRenderer
{
	public partial class DxResultRenderer : UserControl
	{
		private DataTable _activeDataTable = null;
		
		XAppearances _xApp = null;
		
		public DataView ActiveDataView
		{
			get 
			{
				return _activeDataTable == null ? null : _activeDataTable.DefaultView;
			}
		}

		public ContextMenuStrip PopUpMenu
		{
			get 
			{
				return grdPopup;
			}
		}


		public Color NullValueBackColor
		{
			get 
			{
				return DxRendererOptionCfg.Current != null ? DxRendererOptionCfg.Current.NullValueBackColor : Color.LemonChiffon;
			}
		}

		public Color NullValueForeColor
		{
			get
			{
				return DxRendererOptionCfg.Current != null ? DxRendererOptionCfg.Current.NullValueForeColor : Color.FromKnownColor(KnownColor.ControlText);
			}
		}

		public string NullValueDisplayText
		{
			get
			{
				return DxRendererOptionCfg.Current != null ? DxRendererOptionCfg.Current.NullValueDisplayText : "(NULL)";
			}
		}

		
		public DxResultRenderer()
		{
			InitializeComponent();
			grdPrintLink.Component = grd;
			toolTipController1.SetToolTip(grd, Properties.Resources.GridToolTip);
			LoadStyles();
			LoadOptions();
		}

		private void LoadOptions()
		{
			DxRendererOptionCfg.Load();
			if (DxRendererOptionCfg.Current == null)
				return;

			DxRendererOption op = DxRendererOptionCfg.Current;

			grdView.OptionsCustomization.AllowFilter = op.AllowFilter;
			grdView.OptionsView.GroupDrawMode = op.GroupDrawMode;
			grdView.OptionsView.ShowAutoFilterRow = op.ShowAutoFilterRow;
			grdView.OptionsView.ShowFilterPanel = op.ShowFilterPanel;
			grdView.OptionsView.ShowGroupPanel = op.ShowGroupPanel;
			grdView.OptionsView.ColumnAutoWidth = op.ColumnAutoWidth;
			grdView.OptionsView.RowAutoHeight = op.RowAutoHeight;
			grd.UseEmbeddedNavigator = op.ShowNavigationBar;

			_loadingStyles = true;
			cmbGridStyle.Text = op.CurrentStyleSchema;
			_loadingStyles = false;
		}

		private bool _loadingStyles = false;
		private void LoadStyles()
		{
			string fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DxRenderer.Appearances.xml";
			try
			{
				_loadingStyles = true;
				if (!File.Exists(fileName))
				{
					string errStr = "Can not load style schemes. File not found: " + fileName;

					cmbGridStyle.BackColor = Color.FromArgb(255, 192, 192);
					cmbGridStyle.ToolTipText = errStr;
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(errStr, MethodBase.GetCurrentMethod());
					return;
				}

				_xApp = new XAppearances(fileName);
				cmbGridStyle.Items.Clear();
				cmbGridStyle.Items.AddRange(_xApp.FormatNames);
				cmbGridStyle.SelectedText = "Blue Office";
			}
			finally
			{
				_loadingStyles = false;
			}
		}

		public void RenderDataTable(DataTable dataTable)
		{
			_activeDataTable = dataTable;
			grd.DataSource = dataTable;
			
			if (_xApp != null && dataTable != null )
				_xApp.LoadScheme(cmbGridStyle.Text, grdView);
		}

		private void ExportGridToFile()
		{
			if (_activeDataTable == null)
				throw new Exception("Data not available.");

			if (saveFileDialog1.ShowDialog() != DialogResult.OK)
				return;

			string fileName = saveFileDialog1.FileName;

			Cursor currentCursor = Cursor.Current;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
        
				switch (saveFileDialog1.FilterIndex)
				{
					case 1:
						grdView.ExportToExcelOld(fileName);
						break;
					case 2:
						grdView.ExportToHtml(fileName);
						break;
					case 3:
						grdView.ExportToText(fileName);
						break;
					default:
						break;
				}
			}
			finally
			{
				Cursor.Current = currentCursor;
			}
		}


		private void grdPopup_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = grdPopup.Items.Count == 0;
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			grdPrintLink.CreateDocument();
			grdPrintLink.PrintDlg();
		}

		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Cursor currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			grdPrintLink.CreateDocument();

      
			ps.PreviewFormEx.Owner = ( HostServicesSingleton.HostServices.Wb as Form);
			ps.PreviewFormEx.Show();

			Cursor.Current = currentCursor;
		}

		private void cmbGridStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_xApp == null)
				return;

			if (_loadingStyles)
				return;

			_xApp.LoadScheme(cmbGridStyle.SelectedItem.ToString(), grdView);
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			HostServicesSingleton.HostServices.ShowOptionsDialog("DxRenderer Options");
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExportGridToFile();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			grdView.CopyToClipboard();
		}

		private void grdView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{

			if (e.RowHandle < 0)
				return;

			bool selected = false;
			if (grdView.SelectedRowsCount > 0)
			{
				selected = Array.IndexOf<int>(grdView.GetSelectedRows(), e.RowHandle) >= 0;
			}

			if (e.CellValue == null || e.CellValue.GetType() == typeof(DBNull))
			{
				e.DisplayText = NullValueDisplayText;
				e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
				e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

				if (!selected)
				{
					e.Appearance.BackColor = NullValueBackColor;
					e.Appearance.ForeColor = NullValueForeColor;
				}
			}
		}

	}
}
