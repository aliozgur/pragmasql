namespace PragmaSQL.Core
{
	partial class DbObjectList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbObjectList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdSource = new System.Windows.Forms.DataGridView();
            this.colSourceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSourceOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.selMatch = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddSel = new System.Windows.Forms.ToolStripButton();
            this.btnAddAll = new System.Windows.Forms.ToolStripButton();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.grdDest = new System.Windows.Forms.DataGridView();
            this.colObjType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsDest = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.selCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnRemoveSel = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveAll = new System.Windows.Forms.ToolStripButton();
            this.kryptonHeader2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportSelectedObjectToTextEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentEditorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadObjectsFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbDump = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.edtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDbInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSource)).BeginInit();
            this.statusStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDest)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 45);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdDest);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer1.Panel2.Controls.Add(this.kryptonHeader2);
            this.splitContainer1.Size = new System.Drawing.Size(898, 521);
            this.splitContainer1.SplitterDistance = 503;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdSource);
            this.panel3.Controls.Add(this.statusStrip2);
            this.panel3.Controls.Add(this.toolStrip2);
            this.panel3.Controls.Add(this.kryptonHeader1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(503, 521);
            this.panel3.TabIndex = 5;
            // 
            // grdSource
            // 
            this.grdSource.AllowUserToAddRows = false;
            this.grdSource.AllowUserToDeleteRows = false;
            this.grdSource.AllowUserToResizeRows = false;
            this.grdSource.AutoGenerateColumns = false;
            this.grdSource.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSource.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSourceType,
            this.colSourceName,
            this.colSourceOwner});
            this.grdSource.DataSource = this.bsSource;
            this.grdSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSource.Location = new System.Drawing.Point(0, 56);
            this.grdSource.Name = "grdSource";
            this.grdSource.ReadOnly = true;
            this.grdSource.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdSource.Size = new System.Drawing.Size(503, 443);
            this.grdSource.TabIndex = 4;
            this.grdSource.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdSource_CellDoubleClick);
            // 
            // colSourceType
            // 
            this.colSourceType.DataPropertyName = "longType";
            this.colSourceType.HeaderText = "Obj. Type";
            this.colSourceType.Name = "colSourceType";
            this.colSourceType.ReadOnly = true;
            // 
            // colSourceName
            // 
            this.colSourceName.DataPropertyName = "name";
            this.colSourceName.HeaderText = "Obj. Name";
            this.colSourceName.Name = "colSourceName";
            this.colSourceName.ReadOnly = true;
            // 
            // colSourceOwner
            // 
            this.colSourceOwner.DataPropertyName = "owner";
            this.colSourceOwner.HeaderText = "Owner";
            this.colSourceOwner.Name = "colSourceOwner";
            this.colSourceOwner.ReadOnly = true;
            // 
            // bsSource
            // 
            this.bsSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bsSource_ListChanged);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selMatch});
            this.statusStrip2.Location = new System.Drawing.Point(0, 499);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(503, 22);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 10;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // selMatch
            // 
            this.selMatch.Name = "selMatch";
            this.selMatch.Size = new System.Drawing.Size(110, 17);
            this.selMatch.Text = "Available Objects: 0";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSel,
            this.btnAddAll});
            this.toolStrip2.Location = new System.Drawing.Point(0, 31);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(503, 25);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddSel
            // 
            this.btnAddSel.Image = global::PragmaSQL.Core.Properties.Resources.add_sel;
            this.btnAddSel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSel.Name = "btnAddSel";
            this.btnAddSel.Size = new System.Drawing.Size(96, 22);
            this.btnAddSel.Text = "Add Selected";
            this.btnAddSel.Click += new System.EventHandler(this.btnAddSingle_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.Image = global::PragmaSQL.Core.Properties.Resources.add_all_sel;
            this.btnAddAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(66, 22);
            this.btnAddAll.Text = "Add All";
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader1.Size = new System.Drawing.Size(503, 31);
            this.kryptonHeader1.TabIndex = 12;
            this.kryptonHeader1.Text = "Available Database Objects";
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "Available Database Objects";
            this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
            // 
            // grdDest
            // 
            this.grdDest.AllowUserToAddRows = false;
            this.grdDest.AllowUserToDeleteRows = false;
            this.grdDest.AllowUserToResizeRows = false;
            this.grdDest.AutoGenerateColumns = false;
            this.grdDest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdDest.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdDest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colObjType,
            this.colName});
            this.grdDest.DataSource = this.bsDest;
            this.grdDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDest.Location = new System.Drawing.Point(0, 56);
            this.grdDest.Name = "grdDest";
            this.grdDest.ReadOnly = true;
            this.grdDest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDest.Size = new System.Drawing.Size(391, 443);
            this.grdDest.TabIndex = 7;
            this.grdDest.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDest_CellDoubleClick);
            // 
            // colObjType
            // 
            this.colObjType.DataPropertyName = "ObjType";
            this.colObjType.HeaderText = "Obj.Type";
            this.colObjType.Name = "colObjType";
            this.colObjType.ReadOnly = true;
            this.colObjType.Width = 75;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Obj. Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 82;
            // 
            // bsDest
            // 
            this.bsDest.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bsDest_ListChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selCnt});
            this.statusStrip1.Location = new System.Drawing.Point(0, 499);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(391, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // selCnt
            // 
            this.selCnt.Name = "selCnt";
            this.selCnt.Size = new System.Drawing.Size(106, 17);
            this.selCnt.Text = "Selected Objects: 0";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRemoveSel,
            this.btnRemoveAll});
            this.toolStrip3.Location = new System.Drawing.Point(0, 31);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(391, 25);
            this.toolStrip3.TabIndex = 8;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnRemoveSel
            // 
            this.btnRemoveSel.Image = global::PragmaSQL.Core.Properties.Resources.delete_sel;
            this.btnRemoveSel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveSel.Name = "btnRemoveSel";
            this.btnRemoveSel.Size = new System.Drawing.Size(117, 22);
            this.btnRemoveSel.Text = "Remove Selected";
            this.btnRemoveSel.Click += new System.EventHandler(this.btnRemoveSingle_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Image = global::PragmaSQL.Core.Properties.Resources.delete_all_sel;
            this.btnRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(87, 22);
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // kryptonHeader2
            // 
            this.kryptonHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader2.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader2.Name = "kryptonHeader2";
            this.kryptonHeader2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
            this.kryptonHeader2.Size = new System.Drawing.Size(391, 31);
            this.kryptonHeader2.TabIndex = 13;
            this.kryptonHeader2.Text = "Selected Database Objects";
            this.kryptonHeader2.Values.Description = "";
            this.kryptonHeader2.Values.Heading = "Selected Database Objects";
            this.kryptonHeader2.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader2.Values.Image")));
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.pbDump,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbType,
            this.toolStripLabel2,
            this.edtFilter,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(898, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportSelectedObjectToTextEditorToolStripMenuItem,
            this.loadObjectsFromFileToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::PragmaSQL.Core.Properties.Resources.application_go;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(100, 22);
            this.toolStripDropDownButton1.Text = "Dump/Load";
            // 
            // exportSelectedObjectToTextEditorToolStripMenuItem
            // 
            this.exportSelectedObjectToTextEditorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentEditorToolStripMenuItem1});
            this.exportSelectedObjectToTextEditorToolStripMenuItem.Name = "exportSelectedObjectToTextEditorToolStripMenuItem";
            this.exportSelectedObjectToTextEditorToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.exportSelectedObjectToTextEditorToolStripMenuItem.Text = "Dump Selected Objects To";
            // 
            // currentEditorToolStripMenuItem1
            // 
            this.currentEditorToolStripMenuItem1.Name = "currentEditorToolStripMenuItem1";
            this.currentEditorToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.currentEditorToolStripMenuItem1.Text = "Current Editor";
            this.currentEditorToolStripMenuItem1.Click += new System.EventHandler(this.currentEditorToolStripMenuItem1_Click);
            // 
            // loadObjectsFromFileToolStripMenuItem
            // 
            this.loadObjectsFromFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.currentEditorToolStripMenuItem,
            this.objectExplorerToolStripMenuItem});
            this.loadObjectsFromFileToolStripMenuItem.Name = "loadObjectsFromFileToolStripMenuItem";
            this.loadObjectsFromFileToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.loadObjectsFromFileToolStripMenuItem.Text = "Load Selection From";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // currentEditorToolStripMenuItem
            // 
            this.currentEditorToolStripMenuItem.Name = "currentEditorToolStripMenuItem";
            this.currentEditorToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.currentEditorToolStripMenuItem.Text = "Current Editor";
            this.currentEditorToolStripMenuItem.Click += new System.EventHandler(this.currentEditorToolStripMenuItem_Click);
            // 
            // objectExplorerToolStripMenuItem
            // 
            this.objectExplorerToolStripMenuItem.Name = "objectExplorerToolStripMenuItem";
            this.objectExplorerToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.objectExplorerToolStripMenuItem.Text = "Object Explorer";
            this.objectExplorerToolStripMenuItem.Click += new System.EventHandler(this.objectExplorerToolStripMenuItem_Click);
            // 
            // pbDump
            // 
            this.pbDump.Name = "pbDump";
            this.pbDump.Size = new System.Drawing.Size(100, 22);
            this.pbDump.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(114, 22);
            this.toolStripLabel1.Text = "List object(s) of type";
            // 
            // cmbType
            // 
            this.cmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbType.Items.AddRange(new object[] {
            "All",
            "Table",
            "View",
            "StoredProcedure",
            "UserDefinedFunction"});
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel2.Text = "with name like";
            // 
            // edtFilter
            // 
            this.edtFilter.Name = "edtFilter";
            this.edtFilter.Size = new System.Drawing.Size(100, 25);
            this.edtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.edtFilter_KeyUp);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::PragmaSQL.Core.Properties.Resources.Run;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(45, 22);
            this.toolStripButton1.Text = "List";
            this.toolStripButton1.ToolTipText = "List";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::PragmaSQL.Core.Properties.Resources.help_2;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Help";
            this.toolStripButton4.ToolTipText = "Help";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.Filter = "Text Files|*.txt|SQL Files|*.sql|All Files|*.*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDbInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(898, 20);
            this.panel1.TabIndex = 7;
            // 
            // lblDbInfo
            // 
            this.lblDbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDbInfo.Location = new System.Drawing.Point(0, 0);
            this.lblDbInfo.Name = "lblDbInfo";
            this.lblDbInfo.Size = new System.Drawing.Size(898, 20);
            this.lblDbInfo.TabIndex = 0;
            this.lblDbInfo.Text = "Connection Info:";
            this.lblDbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DbObjectList
            // 
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DbObjectList";
            this.Size = new System.Drawing.Size(898, 566);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSource)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDest)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.DataGridView grdSource;
		private System.Windows.Forms.DataGridView grdDest;
		private System.Windows.Forms.BindingSource bsSource;
		private System.Windows.Forms.BindingSource bsDest;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox cmbType;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripTextBox edtFilter;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblDbInfo;
		private System.Windows.Forms.DataGridViewTextBoxColumn colObjType;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSourceType;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSourceName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSourceOwner;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStripProgressBar pbDump;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton btnAddSel;
		private System.Windows.Forms.ToolStripButton btnAddAll;
		private System.Windows.Forms.ToolStrip toolStrip3;
		private System.Windows.Forms.ToolStripButton btnRemoveSel;
		private System.Windows.Forms.ToolStripButton btnRemoveAll;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel selCnt;
		private System.Windows.Forms.StatusStrip statusStrip2;
		private System.Windows.Forms.ToolStripStatusLabel selMatch;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem exportSelectedObjectToTextEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadObjectsFromFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem currentEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem objectExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem currentEditorToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader2;

	}
}
