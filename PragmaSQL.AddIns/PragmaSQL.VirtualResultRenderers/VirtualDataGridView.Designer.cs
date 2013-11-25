namespace PragmaSQL.VirtualResultRenderers
{
	partial class VirtualDataGridView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.statFilterSort = new System.Windows.Forms.StatusStrip();
      this.statLblRecordCount = new System.Windows.Forms.ToolStripStatusLabel();
      this.statFilterSep = new System.Windows.Forms.ToolStripStatusLabel();
      this.statLblFilter = new System.Windows.Forms.ToolStripStatusLabel();
      this.statLblFilterDt = new System.Windows.Forms.ToolStripStatusLabel();
      this.statBtnClearFilterDt = new System.Windows.Forms.ToolStripStatusLabel();
      this.statSortSep = new System.Windows.Forms.ToolStripStatusLabel();
      this.statLblSort = new System.Windows.Forms.ToolStripStatusLabel();
      this.statLblSortDt = new System.Windows.Forms.ToolStripStatusLabel();
      this.statBtnClearSortDt = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblWait = new System.Windows.Forms.ToolStripStatusLabel();
      this.tsFilterSort = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
      this.ddlFilterType = new System.Windows.Forms.ToolStripComboBox();
      this.txtFilterDt = new System.Windows.Forms.ToolStripTextBox();
      this.btnFilterDt = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.grd = new System.Windows.Forms.DataGridView();
      this.popUpGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.popUpItemCopyGridToClipboard = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpItemCopyGridToClipboardWithCols = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
      this.quickFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpFilterClear = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpSortClear = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripSeparator();
      this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
      this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.statFilterSort.SuspendLayout();
      this.tsFilterSort.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.popUpGrid.SuspendLayout();
      this.SuspendLayout();
      // 
      // statFilterSort
      // 
      this.statFilterSort.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.statFilterSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statLblRecordCount,
            this.statFilterSep,
            this.statLblFilter,
            this.statLblFilterDt,
            this.statBtnClearFilterDt,
            this.statSortSep,
            this.statLblSort,
            this.statLblSortDt,
            this.statBtnClearSortDt,
            this.lblWait});
      this.statFilterSort.Location = new System.Drawing.Point(0, 498);
      this.statFilterSort.Name = "statFilterSort";
      this.statFilterSort.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
      this.statFilterSort.ShowItemToolTips = true;
      this.statFilterSort.Size = new System.Drawing.Size(660, 22);
      this.statFilterSort.SizingGrip = false;
      this.statFilterSort.TabIndex = 6;
      // 
      // statLblRecordCount
      // 
      this.statLblRecordCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.statLblRecordCount.ForeColor = System.Drawing.Color.DarkGreen;
      this.statLblRecordCount.Name = "statLblRecordCount";
      this.statLblRecordCount.Size = new System.Drawing.Size(54, 17);
      this.statLblRecordCount.Text = "0 row(s)";
      // 
      // statFilterSep
      // 
      this.statFilterSep.Name = "statFilterSep";
      this.statFilterSep.Size = new System.Drawing.Size(10, 17);
      this.statFilterSep.Text = "|";
      // 
      // statLblFilter
      // 
      this.statLblFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.statLblFilter.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.statLblFilter.ForeColor = System.Drawing.Color.Navy;
      this.statLblFilter.Image = global::PragmaSQL.VirtualResultRenderers.Properties.Resources.Filter2;
      this.statLblFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.statLblFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.statLblFilter.Name = "statLblFilter";
      this.statLblFilter.Size = new System.Drawing.Size(16, 17);
      this.statLblFilter.Text = "Filter : ";
      // 
      // statLblFilterDt
      // 
      this.statLblFilterDt.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.statLblFilterDt.ForeColor = System.Drawing.Color.Blue;
      this.statLblFilterDt.Name = "statLblFilterDt";
      this.statLblFilterDt.Size = new System.Drawing.Size(0, 17);
      this.statLblFilterDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // statBtnClearFilterDt
      // 
      this.statBtnClearFilterDt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.statBtnClearFilterDt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
      this.statBtnClearFilterDt.ForeColor = System.Drawing.Color.Red;
      this.statBtnClearFilterDt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.statBtnClearFilterDt.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.statBtnClearFilterDt.IsLink = true;
      this.statBtnClearFilterDt.LinkColor = System.Drawing.Color.Red;
      this.statBtnClearFilterDt.Name = "statBtnClearFilterDt";
      this.statBtnClearFilterDt.Size = new System.Drawing.Size(32, 17);
      this.statBtnClearFilterDt.Text = "Clear";
      this.statBtnClearFilterDt.ToolTipText = "Clear Filter";
      this.statBtnClearFilterDt.VisitedLinkColor = System.Drawing.Color.Red;
      this.statBtnClearFilterDt.Click += new System.EventHandler(this.statBtnClearFilterDt_Click);
      // 
      // statSortSep
      // 
      this.statSortSep.Name = "statSortSep";
      this.statSortSep.Size = new System.Drawing.Size(10, 17);
      this.statSortSep.Text = "|";
      // 
      // statLblSort
      // 
      this.statLblSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.statLblSort.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.statLblSort.ForeColor = System.Drawing.Color.Navy;
      this.statLblSort.Image = global::PragmaSQL.VirtualResultRenderers.Properties.Resources.SortHS;
      this.statLblSort.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.statLblSort.Name = "statLblSort";
      this.statLblSort.Size = new System.Drawing.Size(16, 17);
      this.statLblSort.Text = "Sort : ";
      // 
      // statLblSortDt
      // 
      this.statLblSortDt.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.statLblSortDt.ForeColor = System.Drawing.Color.Blue;
      this.statLblSortDt.Name = "statLblSortDt";
      this.statLblSortDt.Size = new System.Drawing.Size(0, 17);
      this.statLblSortDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // statBtnClearSortDt
      // 
      this.statBtnClearSortDt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.statBtnClearSortDt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
      this.statBtnClearSortDt.ForeColor = System.Drawing.Color.Red;
      this.statBtnClearSortDt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.statBtnClearSortDt.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.statBtnClearSortDt.IsLink = true;
      this.statBtnClearSortDt.LinkColor = System.Drawing.Color.Red;
      this.statBtnClearSortDt.Name = "statBtnClearSortDt";
      this.statBtnClearSortDt.Size = new System.Drawing.Size(32, 17);
      this.statBtnClearSortDt.Text = "Clear";
      this.statBtnClearSortDt.ToolTipText = "Clear Sort";
      this.statBtnClearSortDt.VisitedLinkColor = System.Drawing.Color.Red;
      this.statBtnClearSortDt.Click += new System.EventHandler(this.statBtnClearSortDt_Click);
      // 
      // lblWait
      // 
      this.lblWait.BackColor = System.Drawing.SystemColors.Info;
      this.lblWait.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblWait.ForeColor = System.Drawing.SystemColors.InfoText;
      this.lblWait.Name = "lblWait";
      this.lblWait.Size = new System.Drawing.Size(444, 17);
      this.lblWait.Spring = true;
      this.lblWait.Text = "Please wait...";
      this.lblWait.Visible = false;
      // 
      // tsFilterSort
      // 
      this.tsFilterSort.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.tsFilterSort.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tsFilterSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel5,
            this.ddlFilterType,
            this.txtFilterDt,
            this.btnFilterDt,
            this.toolStripButton4,
            this.toolStripButton5});
      this.tsFilterSort.Location = new System.Drawing.Point(0, 0);
      this.tsFilterSort.Name = "tsFilterSort";
      this.tsFilterSort.Size = new System.Drawing.Size(660, 25);
      this.tsFilterSort.TabIndex = 5;
      this.tsFilterSort.Text = "toolStrip1";
      // 
      // toolStripLabel5
      // 
      this.toolStripLabel5.Name = "toolStripLabel5";
      this.toolStripLabel5.Size = new System.Drawing.Size(33, 22);
      this.toolStripLabel5.Text = "Filter";
      // 
      // ddlFilterType
      // 
      this.ddlFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ddlFilterType.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ddlFilterType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.ddlFilterType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.ddlFilterType.Items.AddRange(new object[] {
            "Custom",
            "Like"});
      this.ddlFilterType.Name = "ddlFilterType";
      this.ddlFilterType.Size = new System.Drawing.Size(75, 25);
      // 
      // txtFilterDt
      // 
      this.txtFilterDt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.txtFilterDt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.txtFilterDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.txtFilterDt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.txtFilterDt.Name = "txtFilterDt";
      this.txtFilterDt.Size = new System.Drawing.Size(300, 25);
      this.txtFilterDt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilterDt_KeyUp);
      // 
      // btnFilterDt
      // 
      this.btnFilterDt.BackColor = System.Drawing.SystemColors.Control;
      this.btnFilterDt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnFilterDt.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.btnFilterDt.ForeColor = System.Drawing.SystemColors.ControlText;
      this.btnFilterDt.Image = global::PragmaSQL.VirtualResultRenderers.Properties.Resources.arrow_refresh_small;
      this.btnFilterDt.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFilterDt.Name = "btnFilterDt";
      this.btnFilterDt.Size = new System.Drawing.Size(23, 22);
      this.btnFilterDt.Text = "Execute Filter";
      this.btnFilterDt.Click += new System.EventHandler(this.btnFilterDt_Click);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.toolStripButton4.ForeColor = System.Drawing.SystemColors.ControlText;
      this.toolStripButton4.Image = global::PragmaSQL.VirtualResultRenderers.Properties.Resources.funnel_edit;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton4.Text = "Quick Filter";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Image = global::PragmaSQL.VirtualResultRenderers.Properties.Resources.DeleteHS;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton5.Text = "Clear Filter";
      this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.BackgroundColor = System.Drawing.Color.White;
      this.grd.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.grd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.ContextMenuStrip = this.popUpGrid;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 25);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.ShowEditingIcon = false;
      this.grd.Size = new System.Drawing.Size(660, 473);
      this.grd.TabIndex = 7;
      this.grd.VirtualMode = true;
      this.grd.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grd_ColumnHeaderMouseClick);
      this.grd.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.grd_CellValueNeeded);
      this.grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.OnCellPainting);
      this.grd.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.grd_CellValuePushed);
      this.grd.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.grd_CellToolTipTextNeeded);
      this.grd.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.OnDataError);
      // 
      // popUpGrid
      // 
      this.popUpGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popUpItemCopyGridToClipboard,
            this.popUpItemCopyGridToClipboardWithCols,
            this.toolStripMenuItem10,
            this.quickFilterToolStripMenuItem,
            this.popUpFilterClear,
            this.popUpSortClear,
            this.toolStripMenuItem23,
            this.printPreviewToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripMenuItem12,
            this.exportToFileToolStripMenuItem});
      this.popUpGrid.Name = "popUpGrid";
      this.popUpGrid.Size = new System.Drawing.Size(217, 198);
      this.popUpGrid.Opened += new System.EventHandler(this.popUpGrid_Opened);
      this.popUpGrid.Opening += new System.ComponentModel.CancelEventHandler(this.popUpGrid_Opening);
      // 
      // popUpItemCopyGridToClipboard
      // 
      this.popUpItemCopyGridToClipboard.Name = "popUpItemCopyGridToClipboard";
      this.popUpItemCopyGridToClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.popUpItemCopyGridToClipboard.Size = new System.Drawing.Size(216, 22);
      this.popUpItemCopyGridToClipboard.Text = "Copy";
      this.popUpItemCopyGridToClipboard.Click += new System.EventHandler(this.popUpItemCopyGridToClipboard_Click);
      // 
      // popUpItemCopyGridToClipboardWithCols
      // 
      this.popUpItemCopyGridToClipboardWithCols.Name = "popUpItemCopyGridToClipboardWithCols";
      this.popUpItemCopyGridToClipboardWithCols.Size = new System.Drawing.Size(216, 22);
      this.popUpItemCopyGridToClipboardWithCols.Text = "Copy With Column Headers";
      this.popUpItemCopyGridToClipboardWithCols.Click += new System.EventHandler(this.popUpItemCopyGridToClipboardWithCols_Click);
      // 
      // toolStripMenuItem10
      // 
      this.toolStripMenuItem10.Name = "toolStripMenuItem10";
      this.toolStripMenuItem10.Size = new System.Drawing.Size(213, 6);
      // 
      // quickFilterToolStripMenuItem
      // 
      this.quickFilterToolStripMenuItem.Name = "quickFilterToolStripMenuItem";
      this.quickFilterToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.quickFilterToolStripMenuItem.Text = "Quick Filter";
      this.quickFilterToolStripMenuItem.Click += new System.EventHandler(this.quickFilterToolStripMenuItem_Click);
      // 
      // popUpFilterClear
      // 
      this.popUpFilterClear.Name = "popUpFilterClear";
      this.popUpFilterClear.Size = new System.Drawing.Size(216, 22);
      this.popUpFilterClear.Text = "Clear Filter";
      this.popUpFilterClear.Click += new System.EventHandler(this.popUpFilterClear_Click);
      // 
      // popUpSortClear
      // 
      this.popUpSortClear.Name = "popUpSortClear";
      this.popUpSortClear.Size = new System.Drawing.Size(216, 22);
      this.popUpSortClear.Text = "Clear Sort";
      this.popUpSortClear.Click += new System.EventHandler(this.popUpSortClear_Click);
      // 
      // toolStripMenuItem23
      // 
      this.toolStripMenuItem23.Name = "toolStripMenuItem23";
      this.toolStripMenuItem23.Size = new System.Drawing.Size(213, 6);
      // 
      // printPreviewToolStripMenuItem
      // 
      this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
      this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.printPreviewToolStripMenuItem.Text = "Print Preview";
      this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.printToolStripMenuItem.Text = "Print";
      this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
      // 
      // toolStripMenuItem12
      // 
      this.toolStripMenuItem12.Name = "toolStripMenuItem12";
      this.toolStripMenuItem12.Size = new System.Drawing.Size(213, 6);
      // 
      // exportToFileToolStripMenuItem
      // 
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
      this.exportToFileToolStripMenuItem.Text = "Export To File";
      this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
      // 
      // printDocument1
      // 
      this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
      // 
      // VirtualDataGridView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Controls.Add(this.statFilterSort);
      this.Controls.Add(this.tsFilterSort);
      this.Name = "VirtualDataGridView";
      this.Size = new System.Drawing.Size(660, 520);
      this.statFilterSort.ResumeLayout(false);
      this.statFilterSort.PerformLayout();
      this.tsFilterSort.ResumeLayout(false);
      this.tsFilterSort.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.popUpGrid.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statFilterSort;
		private System.Windows.Forms.ToolStripStatusLabel statLblRecordCount;
		private System.Windows.Forms.ToolStripStatusLabel statFilterSep;
		private System.Windows.Forms.ToolStripStatusLabel statLblFilter;
		private System.Windows.Forms.ToolStripStatusLabel statLblFilterDt;
		private System.Windows.Forms.ToolStripStatusLabel statBtnClearFilterDt;
		private System.Windows.Forms.ToolStripStatusLabel statSortSep;
		private System.Windows.Forms.ToolStripStatusLabel statLblSort;
		private System.Windows.Forms.ToolStripStatusLabel statLblSortDt;
		private System.Windows.Forms.ToolStripStatusLabel statBtnClearSortDt;
		private System.Windows.Forms.ToolStrip tsFilterSort;
		private System.Windows.Forms.ToolStripLabel toolStripLabel5;
		private System.Windows.Forms.ToolStripComboBox ddlFilterType;
		private System.Windows.Forms.ToolStripTextBox txtFilterDt;
		private System.Windows.Forms.ToolStripButton btnFilterDt;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStripButton toolStripButton5;
		private System.Windows.Forms.DataGridView grd;
		private System.Windows.Forms.ContextMenuStrip popUpGrid;
		private System.Windows.Forms.ToolStripMenuItem popUpItemCopyGridToClipboard;
		private System.Windows.Forms.ToolStripMenuItem popUpItemCopyGridToClipboardWithCols;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
		private System.Windows.Forms.ToolStripMenuItem quickFilterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem popUpFilterClear;
		private System.Windows.Forms.ToolStripMenuItem popUpSortClear;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem23;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
		private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.ToolStripStatusLabel lblWait;
	}
}
