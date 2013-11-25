namespace PragmaSQL
{
  partial class frmDataViewer
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataViewer));
      this.navigator = new System.Windows.Forms.BindingNavigator(this.components);
      this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
      this.bindingNavigatorRefresh = new System.Windows.Forms.ToolStripButton();
      this.btnStop = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
      this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
      this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnDelete = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.lblStatus = new System.Windows.Forms.ToolStripLabel();
      this.grd = new System.Windows.Forms.DataGridView();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.actionList1 = new Crad.Windows.Forms.Actions.ActionList();
      this.actDeleteSelected = new Crad.Windows.Forms.Actions.Action();
      this.panel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
      this.lblHeader = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.navigator)).BeginInit();
      this.navigator.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.contextMenuTabPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.actionList1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.panel3)).BeginInit();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // navigator
      // 
      this.navigator.AddNewItem = this.bindingNavigatorAddNewItem;
      this.navigator.CountItem = this.bindingNavigatorCountItem;
      this.navigator.DeleteItem = null;
      this.navigator.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.navigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.navigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorRefresh,
            this.btnStop,
            this.toolStripSeparator1,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.btnDelete,
            this.toolStripSeparator2,
            this.lblStatus});
      this.navigator.Location = new System.Drawing.Point(0, 25);
      this.navigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
      this.navigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
      this.navigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
      this.navigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
      this.navigator.Name = "navigator";
      this.navigator.PositionItem = this.bindingNavigatorPositionItem;
      this.navigator.Size = new System.Drawing.Size(740, 25);
      this.navigator.TabIndex = 1;
      this.navigator.Text = "bindingNavigator1";
      // 
      // bindingNavigatorAddNewItem
      // 
      this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
      this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
      this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
      this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
      this.bindingNavigatorAddNewItem.Text = "Add new";
      // 
      // bindingNavigatorCountItem
      // 
      this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
      this.bindingNavigatorCountItem.Size = new System.Drawing.Size(33, 22);
      this.bindingNavigatorCountItem.Text = "of {0}";
      this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
      // 
      // bindingNavigatorRefresh
      // 
      this.bindingNavigatorRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.bindingNavigatorRefresh.Image = global::PragmaSQL.Properties.Resources.Refresh;
      this.bindingNavigatorRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.bindingNavigatorRefresh.Name = "bindingNavigatorRefresh";
      this.bindingNavigatorRefresh.Size = new System.Drawing.Size(23, 22);
      this.bindingNavigatorRefresh.Text = "Reload";
      this.bindingNavigatorRefresh.Click += new System.EventHandler(this.bindingNavigatorRefresh_Click);
      // 
      // btnStop
      // 
      this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStop.Enabled = false;
      this.btnStop.Image = global::PragmaSQL.Properties.Resources.Stop;
      this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(23, 22);
      this.btnStop.Text = "Cancel";
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
      // bindingNavigatorSeparator2
      // 
      this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
      this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnDelete
      // 
      this.actionList1.SetAction(this.btnDelete, this.actDeleteSelected);
      this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDelete.Enabled = false;
      this.btnDelete.Image = global::PragmaSQL.Properties.Resources.DeleteHS;
      this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(23, 22);
      this.btnDelete.Text = "Delete";
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(39, 22);
      this.lblStatus.Text = "Status";
      // 
      // grd
      // 
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToOrderColumns = true;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.grd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.ContextMenuStrip = this.contextMenuStrip1;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 50);
      this.grd.Name = "grd";
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(740, 440);
      this.grd.TabIndex = 2;
      this.grd.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_RowValidated);
      this.grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grd_CellPainting);
      this.grd.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grd_DataError);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRecordToolStripMenuItem,
            this.deleteRecordToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem1,
            this.copyToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(327, 120);
      // 
      // addRecordToolStripMenuItem
      // 
      this.addRecordToolStripMenuItem.Image = global::PragmaSQL.Properties.Resources.DataContainer_NewRecordHS;
      this.addRecordToolStripMenuItem.Name = "addRecordToolStripMenuItem";
      this.addRecordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)));
      this.addRecordToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
      this.addRecordToolStripMenuItem.Text = "Add";
      this.addRecordToolStripMenuItem.Click += new System.EventHandler(this.addRecordToolStripMenuItem_Click);
      // 
      // deleteRecordToolStripMenuItem
      // 
      this.actionList1.SetAction(this.deleteRecordToolStripMenuItem, this.actDeleteSelected);
      this.deleteRecordToolStripMenuItem.Enabled = false;
      this.deleteRecordToolStripMenuItem.Image = global::PragmaSQL.Properties.Resources.DeleteHS;
      this.deleteRecordToolStripMenuItem.Name = "deleteRecordToolStripMenuItem";
      this.deleteRecordToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Del";
      this.deleteRecordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
      this.deleteRecordToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
      this.deleteRecordToolStripMenuItem.Text = "Delete";
      // 
      // refreshToolStripMenuItem
      // 
      this.refreshToolStripMenuItem.Image = global::PragmaSQL.Properties.Resources.Refresh;
      this.refreshToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.refreshToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
      this.refreshToolStripMenuItem.Text = "Refresh";
      this.refreshToolStripMenuItem.Click += new System.EventHandler(this.bindingNavigatorRefresh_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(323, 6);
      // 
      // copyToolStripMenuItem1
      // 
      this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
      this.copyToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                  | System.Windows.Forms.Keys.C)));
      this.copyToolStripMenuItem1.Size = new System.Drawing.Size(326, 22);
      this.copyToolStripMenuItem1.Text = "Copy Selection";
      this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                  | System.Windows.Forms.Keys.H)));
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
      this.copyToolStripMenuItem.Text = "Copy Selection With Column Header";
      this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
      // 
      // contextMenuTabPage
      // 
      this.contextMenuTabPage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem});
      this.contextMenuTabPage.Name = "contextMenuTab";
      this.contextMenuTabPage.Size = new System.Drawing.Size(167, 70);
      // 
      // closeToolStripMenuItem
      // 
      this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
      this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeToolStripMenuItem.Text = "Close";
      this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
      // 
      // closeAllToolStripMenuItem
      // 
      this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
      this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllToolStripMenuItem.Text = "Close All";
      this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
      // 
      // closeAllButThisToolStripMenuItem
      // 
      this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
      this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
      this.closeAllButThisToolStripMenuItem.Text = "Close All But This";
      this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
      // 
      // actionList1
      // 
      this.actionList1.Actions.Add(this.actDeleteSelected);
      this.actionList1.ContainerControl = this;
      // 
      // actDeleteSelected
      // 
      this.actDeleteSelected.Enabled = false;
      this.actDeleteSelected.Image = global::PragmaSQL.Properties.Resources.DeleteHS;
      this.actDeleteSelected.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
      this.actDeleteSelected.Text = "Delete";
      this.actDeleteSelected.ToolTipText = "Delete Selected";
      this.actDeleteSelected.Update += new System.EventHandler(this.actDeleteSelected_Update);
      this.actDeleteSelected.Execute += new System.EventHandler(this.actDeleteSelected_Execute);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.lblHeader);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.panel3.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
      this.panel3.Size = new System.Drawing.Size(740, 25);
      this.panel3.TabIndex = 9;
      // 
      // lblHeader
      // 
      this.lblHeader.BackColor = System.Drawing.Color.Transparent;
      this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.lblHeader.Location = new System.Drawing.Point(0, 0);
      this.lblHeader.Name = "lblHeader";
      this.lblHeader.Size = new System.Drawing.Size(740, 25);
      this.lblHeader.TabIndex = 1;
      this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // frmDataViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(740, 490);
      this.Controls.Add(this.grd);
      this.Controls.Add(this.navigator);
      this.Controls.Add(this.panel3);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmDataViewer";
      this.TabPageContextMenuStrip = this.contextMenuTabPage;
      this.TabText = "frmTableEditor";
      this.Text = "Data Editor";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDataViewer_FormClosed);
      ((System.ComponentModel.ISupportInitialize)(this.navigator)).EndInit();
      this.navigator.ResumeLayout(false);
      this.navigator.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.contextMenuTabPage.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.actionList1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.panel3)).EndInit();
      this.panel3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.BindingNavigator navigator;
    private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
    private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
    private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
    private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton bindingNavigatorRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton btnDelete;
    private Crad.Windows.Forms.Actions.ActionList actionList1;
    private Crad.Windows.Forms.Actions.Action actDeleteSelected;
    private System.Windows.Forms.ToolStripLabel lblStatus;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnStop;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addRecordToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteRecordToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private ComponentFactory.Krypton.Toolkit.KryptonPanel panel3;
    private System.Windows.Forms.Label lblHeader;

  }
}