namespace PragmaSQL
{
  partial class ObjectRefList
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
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
    private void InitializeComponent( )
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectRefList));
      this.grd = new System.Windows.Forms.DataGridView();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.mnuItemModify = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
      this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
      this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnObjRefs = new System.Windows.Forms.ToolStripButton();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.saveDlgExport = new System.Windows.Forms.SaveFileDialog();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
      this.bindingNavigator1.SuspendLayout();
      this.SuspendLayout();
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.ContextMenuStrip = this.contextMenuStrip1;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 25);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.Size = new System.Drawing.Size(658, 495);
      this.grd.TabIndex = 0;
      this.grd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grd_KeyDown);
      this.grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grd_CellPainting);
      this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
      this.grd.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CellContentClick);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemModify,
            this.mnuItemOpen,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.exportToFileToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(152, 98);
      this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
      // 
      // mnuItemModify
      // 
      this.mnuItemModify.Name = "mnuItemModify";
      this.mnuItemModify.Size = new System.Drawing.Size(151, 22);
      this.mnuItemModify.Text = "Modify";
      this.mnuItemModify.Click += new System.EventHandler(this.mnuItemModify_Click);
      // 
      // mnuItemOpen
      // 
      this.mnuItemOpen.Name = "mnuItemOpen";
      this.mnuItemOpen.Size = new System.Drawing.Size(151, 22);
      this.mnuItemOpen.Text = "Open";
      this.mnuItemOpen.Click += new System.EventHandler(this.mnuItemOpen_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
      // 
      // exportToFileToolStripMenuItem
      // 
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
      this.exportToFileToolStripMenuItem.Text = "Export To File";
      this.exportToFileToolStripMenuItem.Click += new System.EventHandler(this.exportToFileToolStripMenuItem_Click);
      // 
      // bindingNavigator1
      // 
      this.bindingNavigator1.AddNewItem = null;
      this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
      this.bindingNavigator1.DeleteItem = null;
      this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.toolStripSeparator1,
            this.btnObjRefs});
      this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
      this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
      this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
      this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
      this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
      this.bindingNavigator1.Name = "bindingNavigator1";
      this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
      this.bindingNavigator1.Size = new System.Drawing.Size(658, 25);
      this.bindingNavigator1.TabIndex = 1;
      this.bindingNavigator1.Text = "bindingNavigator1";
      // 
      // bindingNavigatorCountItem
      // 
      this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
      this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
      this.bindingNavigatorCountItem.Text = "of {0}";
      this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::PragmaSQL.Properties.Resources.Refresh;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Refresh";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // bindingNavigatorMoveFirstItem
      // 
      this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
      this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
      this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
      this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
      this.bindingNavigatorMoveFirstItem.Text = "Move first";
      // 
      // bindingNavigatorMovePreviousItem
      // 
      this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
      this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
      this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
      this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
      this.bindingNavigatorMovePreviousItem.Text = "Move previous";
      // 
      // bindingNavigatorSeparator
      // 
      this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
      this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
      // 
      // bindingNavigatorPositionItem
      // 
      this.bindingNavigatorPositionItem.AccessibleName = "Position";
      this.bindingNavigatorPositionItem.AutoSize = false;
      this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
      this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
      this.bindingNavigatorPositionItem.Text = "0";
      this.bindingNavigatorPositionItem.ToolTipText = "Current position";
      // 
      // bindingNavigatorSeparator1
      // 
      this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
      this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // bindingNavigatorMoveNextItem
      // 
      this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
      this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
      this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
      this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
      this.bindingNavigatorMoveNextItem.Text = "Move next";
      // 
      // bindingNavigatorMoveLastItem
      // 
      this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
      this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
      this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
      this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
      this.bindingNavigatorMoveLastItem.Text = "Move last";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnObjRefs
      // 
      this.btnObjRefs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.btnObjRefs.Image = ((System.Drawing.Image)(resources.GetObject("btnObjRefs.Image")));
      this.btnObjRefs.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnObjRefs.Name = "btnObjRefs";
      this.btnObjRefs.Size = new System.Drawing.Size(130, 22);
      this.btnObjRefs.Text = "Show Object References";
      this.btnObjRefs.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.imageList1.Images.SetKeyName(0, "database.bmp");
      this.imageList1.Images.SetKeyName(1, "User.bmp");
      this.imageList1.Images.SetKeyName(2, "DataSet_TableView.bmp");
      this.imageList1.Images.SetKeyName(3, "View1.ico");
      this.imageList1.Images.SetKeyName(4, "Proc2.ico");
      this.imageList1.Images.SetKeyName(5, "function.ico");
      this.imageList1.Images.SetKeyName(6, "Autoformat Grid.bmp");
      // 
      // saveDlgExport
      // 
      this.saveDlgExport.DefaultExt = "xls";
      this.saveDlgExport.Filter = "Excel Files|*.xls|CSV Files|*.csv|XML Files|*.xml|All Files|*.*";
      this.saveDlgExport.Title = "Export Data To File";
      // 
      // ObjectRefList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Controls.Add(this.bindingNavigator1);
      this.Name = "ObjectRefList";
      this.Size = new System.Drawing.Size(658, 520);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
      this.bindingNavigator1.ResumeLayout(false);
      this.bindingNavigator1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.BindingNavigator bindingNavigator1;
    private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
    private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
    private System.Windows.Forms.ToolStripButton btnObjRefs;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mnuItemOpen;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem exportToFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuItemModify;
    private System.Windows.Forms.SaveFileDialog saveDlgExport;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
  }
}
