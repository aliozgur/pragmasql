namespace PragmaSQL
{
  partial class frmObjectExplorer
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectExplorer));
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.stripMnuConnection = new System.Windows.Forms.ToolStripDropDownButton();
      this.stripMnuNewConnectionFromRepo = new System.Windows.Forms.ToolStripMenuItem();
      this.stripMnuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
      this.stripMnuDisconnectSeperator = new System.Windows.Forms.ToolStripSeparator();
      this.stripMnuDisconnect = new System.Windows.Forms.ToolStripMenuItem();
      this.btnRefresh = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnFilter = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.edtPath = new System.Windows.Forms.TextBox();
      this.tv = new MWControls.MWTreeView();
      this.toolStrip1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.ImageList1.Images.SetKeyName(0, "");
      this.ImageList1.Images.SetKeyName(1, "");
      this.ImageList1.Images.SetKeyName(2, "VSFolder_closed.bmp");
      this.ImageList1.Images.SetKeyName(3, "");
      this.ImageList1.Images.SetKeyName(4, "");
      this.ImageList1.Images.SetKeyName(5, "");
      this.ImageList1.Images.SetKeyName(6, "");
      this.ImageList1.Images.SetKeyName(7, "");
      this.ImageList1.Images.SetKeyName(8, "");
      this.ImageList1.Images.SetKeyName(9, "");
      this.ImageList1.Images.SetKeyName(10, "");
      this.ImageList1.Images.SetKeyName(11, "");
      this.ImageList1.Images.SetKeyName(12, "");
      this.ImageList1.Images.SetKeyName(13, "");
      this.ImageList1.Images.SetKeyName(14, "");
      this.ImageList1.Images.SetKeyName(15, "");
      this.ImageList1.Images.SetKeyName(16, "");
      this.ImageList1.Images.SetKeyName(17, "");
      this.ImageList1.Images.SetKeyName(18, "");
      this.ImageList1.Images.SetKeyName(19, "");
      this.ImageList1.Images.SetKeyName(20, "");
      this.ImageList1.Images.SetKeyName(21, "Serious.bmp");
      this.ImageList1.Images.SetKeyName(22, "");
      this.ImageList1.Images.SetKeyName(23, "class.bmp");
      this.ImageList1.Images.SetKeyName(24, "class.bmp");
      this.ImageList1.Images.SetKeyName(25, "class.bmp");
      this.ImageList1.Images.SetKeyName(26, "Synonym.png");
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMnuConnection,
            this.btnRefresh,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.btnFilter,
            this.toolStripButton3,
            this.toolStripButton5,
            this.toolStripButton4});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(286, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // stripMnuConnection
      // 
      this.stripMnuConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.stripMnuConnection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMnuNewConnectionFromRepo,
            this.stripMnuNewConnection,
            this.stripMnuDisconnectSeperator,
            this.stripMnuDisconnect});
      this.stripMnuConnection.Image = global::PragmaSQL.Properties.Resources.Database;
      this.stripMnuConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.stripMnuConnection.Name = "stripMnuConnection";
      this.stripMnuConnection.Size = new System.Drawing.Size(65, 22);
      this.stripMnuConnection.Text = "Connect";
      // 
      // stripMnuNewConnectionFromRepo
      // 
      this.stripMnuNewConnectionFromRepo.Name = "stripMnuNewConnectionFromRepo";
      this.stripMnuNewConnectionFromRepo.Size = new System.Drawing.Size(199, 22);
      this.stripMnuNewConnectionFromRepo.Text = "Connection From List";
      this.stripMnuNewConnectionFromRepo.Click += new System.EventHandler(this.connectionFromRepositoryToolStripMenuItem_Click);
      // 
      // stripMnuNewConnection
      // 
      this.stripMnuNewConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.stripMnuNewConnection.Name = "stripMnuNewConnection";
      this.stripMnuNewConnection.Size = new System.Drawing.Size(199, 22);
      this.stripMnuNewConnection.Text = "Connect To";
      this.stripMnuNewConnection.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
      // 
      // stripMnuDisconnectSeperator
      // 
      this.stripMnuDisconnectSeperator.Name = "stripMnuDisconnectSeperator";
      this.stripMnuDisconnectSeperator.Size = new System.Drawing.Size(196, 6);
      // 
      // stripMnuDisconnect
      // 
      this.stripMnuDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.stripMnuDisconnect.Name = "stripMnuDisconnect";
      this.stripMnuDisconnect.Size = new System.Drawing.Size(199, 22);
      this.stripMnuDisconnect.Text = "Disconnect From Server";
      this.stripMnuDisconnect.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
      // 
      // btnRefresh
      // 
      this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRefresh.Image = global::PragmaSQL.Properties.Resources.Refresh;
      this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(23, 22);
      this.btnRefresh.Text = "Refresh";
      this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::PragmaSQL.Properties.Resources.open;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Open File";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = global::PragmaSQL.Properties.Resources.new1;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton2.Text = "New Script";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnFilter
      // 
      this.btnFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnFilter.Enabled = false;
      this.btnFilter.Image = global::PragmaSQL.Properties.Resources.funnel_edit;
      this.btnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFilter.Name = "btnFilter";
      this.btnFilter.Size = new System.Drawing.Size(23, 22);
      this.btnFilter.Text = "Filter";
      this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton3.Image = global::PragmaSQL.Properties.Resources.search;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton3.Text = "Search on Db";
      this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Image = global::PragmaSQL.Properties.Resources.history;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton5.Text = "Object Change History";
      this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.Image = global::PragmaSQL.Properties.Resources.library;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton4.Text = "Object Grouping";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.edtPath);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 500);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(286, 21);
      this.panel1.TabIndex = 3;
      // 
      // edtPath
      // 
      this.edtPath.BackColor = System.Drawing.SystemColors.Control;
      this.edtPath.Dock = System.Windows.Forms.DockStyle.Fill;
      this.edtPath.Location = new System.Drawing.Point(0, 0);
      this.edtPath.Name = "edtPath";
      this.edtPath.ReadOnly = true;
      this.edtPath.Size = new System.Drawing.Size(286, 20);
      this.edtPath.TabIndex = 0;
      // 
      // tv
      // 
      this.tv.CheckedNodes = ((System.Collections.Hashtable)(resources.GetObject("tv.CheckedNodes")));
      this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tv.HideSelection = false;
      this.tv.ImageIndex = 0;
      this.tv.ImageList = this.ImageList1;
      this.tv.Location = new System.Drawing.Point(0, 25);
      this.tv.Name = "tv";
      this.tv.SelectedImageIndex = 0;
      this.tv.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("tv.SelNodes")));
      this.tv.ShowNodeToolTips = true;
      this.tv.Size = new System.Drawing.Size(286, 475);
      this.tv.TabIndex = 2;
      this.tv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tv_MouseUp);
      this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
      this.tv.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tv_ItemDrag);
      this.tv.DoubleClick += new System.EventHandler(this.tv_DoubleClick);
      this.tv.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_BeforeExpand);
      // 
      // frmObjectExplorer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(286, 521);
      this.Controls.Add(this.tv);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.toolStrip1);
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmObjectExplorer";
      this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
      this.TabText = "Object Explorer";
      this.Text = "Object Explorer";
      this.Shown += new System.EventHandler(this.frmObjectExplorer_Shown);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmObjectExplorer_FormClosed);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripDropDownButton stripMnuConnection;
    private System.Windows.Forms.ToolStripMenuItem stripMnuNewConnection;
    private System.Windows.Forms.ToolStripMenuItem stripMnuDisconnect;
    private System.Windows.Forms.ToolStripButton btnRefresh;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripMenuItem stripMnuNewConnectionFromRepo;
    private System.Windows.Forms.ToolStripSeparator stripMnuDisconnectSeperator;
    private System.Windows.Forms.ImageList ImageList1;
    private MWControls.MWTreeView tv;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox edtPath;
    private System.Windows.Forms.ToolStripButton btnFilter;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripButton toolStripButton5;

  }
}