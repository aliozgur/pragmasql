namespace PragmaSQL.DxResultRenderer
{
	partial class DxResultRenderer
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DxResultRenderer));
      this.grd = new DevExpress.XtraGrid.GridControl();
      this.grdPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.cmbGridStyle = new System.Windows.Forms.ToolStripComboBox();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.grdView = new DevExpress.XtraGrid.Views.Grid.GridView();
      this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
      this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
      this.ps = new DevExpress.XtraPrinting.PrintingSystem(this.components);
      this.grdPrintLink = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.grdPopup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdView)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ps)).BeginInit();
      this.SuspendLayout();
      // 
      // grd
      // 
      this.grd.ContextMenuStrip = this.grdPopup;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.EmbeddedNavigator.Buttons.Append.Visible = false;
      this.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
      this.grd.EmbeddedNavigator.Buttons.Edit.Visible = false;
      this.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
      this.grd.EmbeddedNavigator.Buttons.Remove.Visible = false;
      this.grd.EmbeddedNavigator.Name = "";
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.MainView = this.grdView;
      this.grd.Name = "grd";
      this.grd.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
      this.grd.Size = new System.Drawing.Size(793, 510);
      this.grd.TabIndex = 0;
      this.grd.ToolTipController = this.toolTipController1;
      this.grd.UseEmbeddedNavigator = true;
      this.grd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdView});
      // 
      // grdPopup
      // 
      this.grdPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripMenuItem3,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripMenuItem2,
            this.cmbGridStyle,
            this.optionsToolStripMenuItem});
      this.grdPopup.Name = "grdPopup";
      this.grdPopup.Size = new System.Drawing.Size(211, 151);
      this.toolTipController1.SetSuperTip(this.grdPopup, null);
      this.grdPopup.Opening += new System.ComponentModel.CancelEventHandler(this.grdPopup_Opening);
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
      this.copyToolStripMenuItem.Text = "Copy   (Ctrl+C)";
      this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
      // 
      // exportToolStripMenuItem
      // 
      this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
      this.exportToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
      this.exportToolStripMenuItem.Text = "Export";
      this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(207, 6);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
      this.printToolStripMenuItem.Text = "Print";
      this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
      // 
      // printPreviewToolStripMenuItem
      // 
      this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
      this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
      this.printPreviewToolStripMenuItem.Text = "Print Preview";
      this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(207, 6);
      // 
      // cmbGridStyle
      // 
      this.cmbGridStyle.AutoToolTip = true;
      this.cmbGridStyle.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbGridStyle.Name = "cmbGridStyle";
      this.cmbGridStyle.Size = new System.Drawing.Size(150, 21);
      this.cmbGridStyle.ToolTipText = "Select a style scheme";
      this.cmbGridStyle.SelectedIndexChanged += new System.EventHandler(this.cmbGridStyle_SelectedIndexChanged);
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
      this.optionsToolStripMenuItem.Text = "Options";
      this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
      // 
      // grdView
      // 
      this.grdView.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
      this.grdView.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.grdView.Appearance.ColumnFilterButton.Options.UseBackColor = true;
      this.grdView.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
      this.grdView.Appearance.ColumnFilterButton.Options.UseForeColor = true;
      this.grdView.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
      this.grdView.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
      this.grdView.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
      this.grdView.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.grdView.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
      this.grdView.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
      this.grdView.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
      this.grdView.Appearance.Empty.BackColor = System.Drawing.Color.White;
      this.grdView.Appearance.Empty.Options.UseBackColor = true;
      this.grdView.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.EvenRow.Options.UseBackColor = true;
      this.grdView.Appearance.EvenRow.Options.UseForeColor = true;
      this.grdView.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
      this.grdView.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.grdView.Appearance.FilterCloseButton.Options.UseBackColor = true;
      this.grdView.Appearance.FilterCloseButton.Options.UseBorderColor = true;
      this.grdView.Appearance.FilterCloseButton.Options.UseForeColor = true;
      this.grdView.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
      this.grdView.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
      this.grdView.Appearance.FilterPanel.Options.UseBackColor = true;
      this.grdView.Appearance.FilterPanel.Options.UseForeColor = true;
      this.grdView.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
      this.grdView.Appearance.FixedLine.Options.UseBackColor = true;
      this.grdView.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
      this.grdView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.FocusedCell.Options.UseBackColor = true;
      this.grdView.Appearance.FocusedCell.Options.UseForeColor = true;
      this.grdView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
      this.grdView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
      this.grdView.Appearance.FocusedRow.Options.UseBackColor = true;
      this.grdView.Appearance.FocusedRow.Options.UseForeColor = true;
      this.grdView.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
      this.grdView.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.grdView.Appearance.FooterPanel.Options.UseBackColor = true;
      this.grdView.Appearance.FooterPanel.Options.UseBorderColor = true;
      this.grdView.Appearance.FooterPanel.Options.UseForeColor = true;
      this.grdView.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
      this.grdView.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
      this.grdView.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.GroupButton.Options.UseBackColor = true;
      this.grdView.Appearance.GroupButton.Options.UseBorderColor = true;
      this.grdView.Appearance.GroupButton.Options.UseForeColor = true;
      this.grdView.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
      this.grdView.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
      this.grdView.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.GroupFooter.Options.UseBackColor = true;
      this.grdView.Appearance.GroupFooter.Options.UseBorderColor = true;
      this.grdView.Appearance.GroupFooter.Options.UseForeColor = true;
      this.grdView.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
      this.grdView.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.GroupPanel.Options.UseBackColor = true;
      this.grdView.Appearance.GroupPanel.Options.UseForeColor = true;
      this.grdView.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
      this.grdView.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
      this.grdView.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
      this.grdView.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.GroupRow.Options.UseBackColor = true;
      this.grdView.Appearance.GroupRow.Options.UseBorderColor = true;
      this.grdView.Appearance.GroupRow.Options.UseFont = true;
      this.grdView.Appearance.GroupRow.Options.UseForeColor = true;
      this.grdView.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
      this.grdView.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
      this.grdView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
      this.grdView.Appearance.HeaderPanel.Options.UseBackColor = true;
      this.grdView.Appearance.HeaderPanel.Options.UseBorderColor = true;
      this.grdView.Appearance.HeaderPanel.Options.UseForeColor = true;
      this.grdView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
      this.grdView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
      this.grdView.Appearance.HideSelectionRow.Options.UseBackColor = true;
      this.grdView.Appearance.HideSelectionRow.Options.UseForeColor = true;
      this.grdView.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
      this.grdView.Appearance.HorzLine.Options.UseBackColor = true;
      this.grdView.Appearance.OddRow.BackColor = System.Drawing.Color.White;
      this.grdView.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.OddRow.Options.UseBackColor = true;
      this.grdView.Appearance.OddRow.Options.UseForeColor = true;
      this.grdView.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
      this.grdView.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
      this.grdView.Appearance.Preview.Options.UseBackColor = true;
      this.grdView.Appearance.Preview.Options.UseForeColor = true;
      this.grdView.Appearance.Row.BackColor = System.Drawing.Color.White;
      this.grdView.Appearance.Row.ForeColor = System.Drawing.Color.Black;
      this.grdView.Appearance.Row.Options.UseBackColor = true;
      this.grdView.Appearance.Row.Options.UseForeColor = true;
      this.grdView.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
      this.grdView.Appearance.RowSeparator.Options.UseBackColor = true;
      this.grdView.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
      this.grdView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
      this.grdView.Appearance.SelectedRow.Options.UseBackColor = true;
      this.grdView.Appearance.SelectedRow.Options.UseForeColor = true;
      this.grdView.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
      this.grdView.Appearance.VertLine.Options.UseBackColor = true;
      this.grdView.GridControl = this.grd;
      this.grdView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "", null, " ( Count = {0} )")});
      this.grdView.Name = "grdView";
      this.grdView.OptionsBehavior.AllowIncrementalSearch = true;
      this.grdView.OptionsBehavior.Editable = false;
      this.grdView.OptionsCustomization.AllowRowSizing = true;
      this.grdView.OptionsFilter.UseNewCustomFilterDialog = true;
      this.grdView.OptionsPrint.EnableAppearanceEvenRow = true;
      this.grdView.OptionsPrint.EnableAppearanceOddRow = true;
      this.grdView.OptionsSelection.MultiSelect = true;
      this.grdView.OptionsView.ColumnAutoWidth = false;
      this.grdView.OptionsView.EnableAppearanceEvenRow = true;
      this.grdView.OptionsView.EnableAppearanceOddRow = true;
      this.grdView.OptionsView.ShowAutoFilterRow = true;
      this.grdView.PaintStyleName = "Flat";
      this.grdView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grdView_CustomDrawCell);
      // 
      // repositoryItemTextEdit1
      // 
      this.repositoryItemTextEdit1.AutoHeight = false;
      this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
      this.repositoryItemTextEdit1.NullText = "(NULL)";
      // 
      // ps
      // 
      this.ps.Links.AddRange(new object[] {
            this.grdPrintLink});
      // 
      // grdPrintLink
      // 
      this.grdPrintLink.CustomPaperSize = new System.Drawing.Size(0, 0);
      this.grdPrintLink.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("grdPrintLink.ImageStream")));
      this.grdPrintLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
      this.grdPrintLink.PrintingSystem = this.ps;
      this.grdPrintLink.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "xls";
      this.saveFileDialog1.Filter = "Excel Files |*.xls|HTML Files|*.html|Text Files|*.txt";
      this.saveFileDialog1.Title = "Save Result As";
      // 
      // DxResultRenderer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Name = "DxResultRenderer";
      this.Size = new System.Drawing.Size(793, 510);
      this.toolTipController1.SetSuperTip(this, null);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.grdPopup.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdView)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ps)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl grd;
		private DevExpress.XtraGrid.Views.Grid.GridView grdView;
		private System.Windows.Forms.ContextMenuStrip grdPopup;
		private DevExpress.XtraPrinting.PrintingSystem ps;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private DevExpress.XtraPrinting.PrintableComponentLink grdPrintLink;
		private DevExpress.Utils.ToolTipController toolTipController1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox cmbGridStyle;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
	}
}
