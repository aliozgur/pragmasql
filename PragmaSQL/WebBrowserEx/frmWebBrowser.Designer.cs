namespace PragmaSQL.WebBrowserEx
{
  partial class frmWebBrowser
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWebBrowser));
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.cmbAdress = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.contextMenuTabPage = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.browser = new PragmaSQL.WebBrowserEx.ExtendedWebBrowser(this.components);
      this.toolStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.contextMenuTabPage.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton11,
            this.toolStripButton6,
            this.toolStripButton8,
            this.toolStripButton7,
            this.toolStripButton10,
            this.toolStripSeparator3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.cmbAdress,
            this.toolStripButton5,
            this.toolStripButton9});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(810, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton11
      // 
      this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton11.Image = global::PragmaSQL.Properties.Resources.NewWindow;
      this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton11.Name = "toolStripButton11";
      this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton11.Text = "New Window";
      this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // toolStripButton6
      // 
      this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton6.Image = global::PragmaSQL.Properties.Resources.open;
      this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton6.Name = "toolStripButton6";
      this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton6.Text = "Open";
      this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
      // 
      // toolStripButton8
      // 
      this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton8.Image = global::PragmaSQL.Properties.Resources.SaveAs;
      this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton8.Name = "toolStripButton8";
      this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton8.Text = "Save As";
      this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
      // 
      // toolStripButton7
      // 
      this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton7.Image = global::PragmaSQL.Properties.Resources.Print;
      this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton7.Name = "toolStripButton7";
      this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton7.Text = "Print";
      this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
      // 
      // toolStripButton10
      // 
      this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton10.Image = global::PragmaSQL.Properties.Resources.print_preview;
      this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton10.Name = "toolStripButton10";
      this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton10.Text = "Print Preview";
      this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::PragmaSQL.Properties.Resources.back;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Back";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = global::PragmaSQL.Properties.Resources.forward;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton2.Text = "Forward";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton3.Image = global::PragmaSQL.Properties.Resources.home;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton3.Text = "Home";
      this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.Image = global::PragmaSQL.Properties.Resources.SearchWeb;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton4.Text = "Search The Web";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // cmbAdress
      // 
      this.cmbAdress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.cmbAdress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
      this.cmbAdress.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbAdress.MaxDropDownItems = 20;
      this.cmbAdress.Name = "cmbAdress";
      this.cmbAdress.Size = new System.Drawing.Size(400, 25);
      this.cmbAdress.SelectedIndexChanged += new System.EventHandler(this.cmbAdress_SelectedIndexChanged);
      this.cmbAdress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbAdress_KeyDown);
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Image = global::PragmaSQL.Properties.Resources.GoLtr;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton5.Text = "Go";
      this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
      // 
      // toolStripButton9
      // 
      this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
      this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton9.Name = "toolStripButton9";
      this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton9.Text = "Stop";
      this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
      this.statusStrip1.Location = new System.Drawing.Point(0, 475);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
      this.statusStrip1.Size = new System.Drawing.Size(810, 22);
      this.statusStrip1.TabIndex = 2;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(795, 17);
      this.lblStatus.Spring = true;
      this.lblStatus.Text = "Status:";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // progressBar
      // 
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(100, 16);
      this.progressBar.Visible = false;
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
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "htm";
      this.openFileDialog1.Filter = "Htm Files|*.htm|Html Files|*.html|All Files|*.*";
      // 
      // browser
      // 
      this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.browser.Location = new System.Drawing.Point(0, 25);
      this.browser.MinimumSize = new System.Drawing.Size(20, 20);
      this.browser.Name = "browser";
      this.browser.Size = new System.Drawing.Size(810, 450);
      this.browser.TabIndex = 0;
      this.browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.browser_Navigating);
      // 
      // frmWebBrowser
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(810, 497);
      this.Controls.Add(this.browser);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmWebBrowser";
      this.TabPageContextMenuStrip = this.contextMenuTabPage;
      this.TabText = "Web Browser";
      this.Text = "Web Browser";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWebBrowser_FormClosed);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.contextMenuTabPage.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripComboBox cmbAdress;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    private System.Windows.Forms.ToolStripProgressBar progressBar;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton toolStripButton6;
    private System.Windows.Forms.ToolStripButton toolStripButton7;
    private System.Windows.Forms.ToolStripButton toolStripButton8;
		private System.Windows.Forms.ToolStripButton toolStripButton9;
    private System.Windows.Forms.ContextMenuStrip contextMenuTabPage;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton toolStripButton10;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    internal ExtendedWebBrowser browser;
    private System.Windows.Forms.ToolStripButton toolStripButton11;
  }
}