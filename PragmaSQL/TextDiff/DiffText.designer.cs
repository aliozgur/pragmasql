namespace PragmaSQL
{
  partial class DiffText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiffText));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpenSource = new System.Windows.Forms.ToolStripButton();
            this.btnOpenDest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPasteSource = new System.Windows.Forms.ToolStripButton();
            this.btnPasteDest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCompare = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statLblDeleteSource = new System.Windows.Forms.ToolStripStatusLabel();
            this.statLblAddDest = new System.Windows.Forms.ToolStripStatusLabel();
            this.statLblNoChange = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.txtSource = new ICSharpCode.TextEditor.TextEditorControl();
            this.contextMenuStripSource = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMnuOpenSource = new System.Windows.Forms.ToolStripMenuItem();
            this.cMnuPasteSource = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.compareToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtDest = new ICSharpCode.TextEditor.TextEditorControl();
            this.contextMenuStripDest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMnuOpenDest = new System.Windows.Forms.ToolStripMenuItem();
            this.cMnuPasteDest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panSource = new System.Windows.Forms.Panel();
            this.txtSourceHeader = new System.Windows.Forms.TextBox();
            this.panDest = new System.Windows.Forms.Panel();
            this.txtDestHeader = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.layoutPanel.SuspendLayout();
            this.contextMenuStripSource.SuspendLayout();
            this.contextMenuStripDest.SuspendLayout();
            this.panSource.SuspendLayout();
            this.panDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenSource,
            this.btnOpenDest,
            this.toolStripSeparator3,
            this.btnPasteSource,
            this.btnPasteDest,
            this.toolStripSeparator2,
            this.btnCompare});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1353, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpenSource
            // 
            this.btnOpenSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenSource.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenSource.Image")));
            this.btnOpenSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenSource.Name = "btnOpenSource";
            this.btnOpenSource.Size = new System.Drawing.Size(23, 22);
            this.btnOpenSource.Text = "Open Source";
            this.btnOpenSource.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnOpenDest
            // 
            this.btnOpenDest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenDest.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDest.Image")));
            this.btnOpenDest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenDest.Name = "btnOpenDest";
            this.btnOpenDest.Size = new System.Drawing.Size(23, 22);
            this.btnOpenDest.Text = "Open Dest";
            this.btnOpenDest.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPasteSource
            // 
            this.btnPasteSource.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPasteSource.Image = ((System.Drawing.Image)(resources.GetObject("btnPasteSource.Image")));
            this.btnPasteSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPasteSource.Name = "btnPasteSource";
            this.btnPasteSource.Size = new System.Drawing.Size(23, 22);
            this.btnPasteSource.Text = "Paste Source";
            this.btnPasteSource.Click += new System.EventHandler(this.btnPasteSource_Click);
            // 
            // btnPasteDest
            // 
            this.btnPasteDest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPasteDest.Image = ((System.Drawing.Image)(resources.GetObject("btnPasteDest.Image")));
            this.btnPasteDest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPasteDest.Name = "btnPasteDest";
            this.btnPasteDest.Size = new System.Drawing.Size(23, 22);
            this.btnPasteDest.Text = "Paste Dest";
            this.btnPasteDest.Click += new System.EventHandler(this.btnPasteDest_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCompare
            // 
            this.btnCompare.Image = ((System.Drawing.Image)(resources.GetObject("btnCompare.Image")));
            this.btnCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(76, 22);
            this.btnCompare.Text = "Compare";
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statLblDeleteSource,
            this.statLblAddDest,
            this.statLblNoChange,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 875);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1353, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statLblDeleteSource
            // 
            this.statLblDeleteSource.BackColor = System.Drawing.Color.MistyRose;
            this.statLblDeleteSource.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statLblDeleteSource.Name = "statLblDeleteSource";
            this.statLblDeleteSource.Size = new System.Drawing.Size(27, 17);
            this.statLblDeleteSource.Text = "  -  ";
            this.statLblDeleteSource.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // statLblAddDest
            // 
            this.statLblAddDest.BackColor = System.Drawing.Color.LightGreen;
            this.statLblAddDest.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statLblAddDest.Name = "statLblAddDest";
            this.statLblAddDest.Size = new System.Drawing.Size(31, 17);
            this.statLblAddDest.Text = "  +  ";
            this.statLblAddDest.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // statLblNoChange
            // 
            this.statLblNoChange.BackColor = System.Drawing.Color.LightGray;
            this.statLblNoChange.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statLblNoChange.Name = "statLblNoChange";
            this.statLblNoChange.Size = new System.Drawing.Size(31, 17);
            this.statLblNoChange.Text = "  ~  ";
            this.statLblNoChange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel1.Text = "  =  ";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "All Files|*.*";
            this.openFileDialog1.Title = "Open File";
            // 
            // layoutPanel
            // 
            this.layoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.layoutPanel.ColumnCount = 2;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanel.Controls.Add(this.label2, 1, 0);
            this.layoutPanel.Controls.Add(this.panSource, 0, 1);
            this.layoutPanel.Controls.Add(this.panDest, 1, 1);
            this.layoutPanel.Controls.Add(this.label1, 0, 0);
            this.layoutPanel.Controls.Add(this.txtSource, 0, 2);
            this.layoutPanel.Controls.Add(this.txtDest, 1, 2);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 25);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Size = new System.Drawing.Size(1353, 850);
            this.layoutPanel.TabIndex = 10;
            // 
            // txtSource
            // 
            this.txtSource.AllowDrop = true;
            this.txtSource.ContextMenuStrip = this.contextMenuStripSource;
            this.txtSource.ConvertTabsToSpaces = true;
            this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSource.EnableFolding = false;
            this.txtSource.IsIconBarVisible = false;
            this.txtSource.Location = new System.Drawing.Point(4, 48);
            this.txtSource.Name = "txtSource";
            this.txtSource.ShowEOLMarkers = true;
            this.txtSource.ShowInvalidLines = false;
            this.txtSource.ShowSpaces = true;
            this.txtSource.ShowTabs = true;
            this.txtSource.ShowVRuler = true;
            this.txtSource.Size = new System.Drawing.Size(669, 798);
            this.txtSource.TabIndent = 2;
            this.txtSource.TabIndex = 22;
            this.txtSource.UseAntiAliasFont = true;
            this.txtSource.VRulerRow = 120;
            // 
            // contextMenuStripSource
            // 
            this.contextMenuStripSource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMnuOpenSource,
            this.cMnuPasteSource,
            this.toolStripMenuItem1,
            this.compareToolStripMenuItem1});
            this.contextMenuStripSource.Name = "contextMenuStripSource";
            this.contextMenuStripSource.Size = new System.Drawing.Size(186, 76);
            // 
            // cMnuOpenSource
            // 
            this.cMnuOpenSource.Image = ((System.Drawing.Image)(resources.GetObject("cMnuOpenSource.Image")));
            this.cMnuOpenSource.Name = "cMnuOpenSource";
            this.cMnuOpenSource.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.cMnuOpenSource.Size = new System.Drawing.Size(185, 22);
            this.cMnuOpenSource.Text = "Open Source";
            this.cMnuOpenSource.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // cMnuPasteSource
            // 
            this.cMnuPasteSource.Image = ((System.Drawing.Image)(resources.GetObject("cMnuPasteSource.Image")));
            this.cMnuPasteSource.Name = "cMnuPasteSource";
            this.cMnuPasteSource.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.cMnuPasteSource.Size = new System.Drawing.Size(185, 22);
            this.cMnuPasteSource.Text = "Paste Source";
            this.cMnuPasteSource.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 6);
            // 
            // compareToolStripMenuItem1
            // 
            this.compareToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("compareToolStripMenuItem1.Image")));
            this.compareToolStripMenuItem1.Name = "compareToolStripMenuItem1";
            this.compareToolStripMenuItem1.Size = new System.Drawing.Size(185, 22);
            this.compareToolStripMenuItem1.Text = "Compare";
            this.compareToolStripMenuItem1.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // txtDest
            // 
            this.txtDest.AllowDrop = true;
            this.txtDest.ContextMenuStrip = this.contextMenuStripDest;
            this.txtDest.ConvertTabsToSpaces = true;
            this.txtDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDest.EnableFolding = false;
            this.txtDest.IsIconBarVisible = false;
            this.txtDest.Location = new System.Drawing.Point(680, 48);
            this.txtDest.Name = "txtDest";
            this.txtDest.ShowEOLMarkers = true;
            this.txtDest.ShowInvalidLines = false;
            this.txtDest.ShowSpaces = true;
            this.txtDest.ShowTabs = true;
            this.txtDest.ShowVRuler = true;
            this.txtDest.Size = new System.Drawing.Size(669, 798);
            this.txtDest.TabIndent = 2;
            this.txtDest.TabIndex = 21;
            this.txtDest.UseAntiAliasFont = true;
            this.txtDest.VRulerRow = 120;
            // 
            // contextMenuStripDest
            // 
            this.contextMenuStripDest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMnuOpenDest,
            this.cMnuPasteDest,
            this.toolStripMenuItem2,
            this.compareToolStripMenuItem});
            this.contextMenuStripDest.Name = "contextMenuStripDest";
            this.contextMenuStripDest.Size = new System.Drawing.Size(173, 76);
            // 
            // cMnuOpenDest
            // 
            this.cMnuOpenDest.Image = ((System.Drawing.Image)(resources.GetObject("cMnuOpenDest.Image")));
            this.cMnuOpenDest.Name = "cMnuOpenDest";
            this.cMnuOpenDest.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.cMnuOpenDest.Size = new System.Drawing.Size(172, 22);
            this.cMnuOpenDest.Text = "Open Dest";
            this.cMnuOpenDest.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // cMnuPasteDest
            // 
            this.cMnuPasteDest.Image = ((System.Drawing.Image)(resources.GetObject("cMnuPasteDest.Image")));
            this.cMnuPasteDest.Name = "cMnuPasteDest";
            this.cMnuPasteDest.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.cMnuPasteDest.Size = new System.Drawing.Size(172, 22);
            this.cMnuPasteDest.Text = "Paste Dest";
            this.cMnuPasteDest.Click += new System.EventHandler(this.pasteToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("compareToolStripMenuItem.Image")));
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.compareToolStripMenuItem.Text = "Compare";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // panSource
            // 
            this.panSource.Controls.Add(this.txtSourceHeader);
            this.panSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panSource.Location = new System.Drawing.Point(4, 25);
            this.panSource.Name = "panSource";
            this.panSource.Size = new System.Drawing.Size(669, 16);
            this.panSource.TabIndex = 8;
            // 
            // txtSourceHeader
            // 
            this.txtSourceHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSourceHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSourceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSourceHeader.Location = new System.Drawing.Point(0, 0);
            this.txtSourceHeader.Name = "txtSourceHeader";
            this.txtSourceHeader.ReadOnly = true;
            this.txtSourceHeader.Size = new System.Drawing.Size(669, 16);
            this.txtSourceHeader.TabIndex = 1;
            this.txtSourceHeader.TabStop = false;
            this.txtSourceHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSourceHeader.WordWrap = false;
            // 
            // panDest
            // 
            this.panDest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panDest.Controls.Add(this.txtDestHeader);
            this.panDest.Location = new System.Drawing.Point(680, 25);
            this.panDest.Name = "panDest";
            this.panDest.Size = new System.Drawing.Size(669, 16);
            this.panDest.TabIndex = 12;
            // 
            // txtDestHeader
            // 
            this.txtDestHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDestHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDestHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtDestHeader.Location = new System.Drawing.Point(0, 0);
            this.txtDestHeader.Name = "txtDestHeader";
            this.txtDestHeader.ReadOnly = true;
            this.txtDestHeader.Size = new System.Drawing.Size(669, 16);
            this.txtDestHeader.TabIndex = 2;
            this.txtDestHeader.TabStop = false;
            this.txtDestHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDestHeader.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(669, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Source";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(680, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(669, 20);
            this.label2.TabIndex = 24;
            this.label2.Text = "Destination";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DiffText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DiffText";
            this.Size = new System.Drawing.Size(1353, 897);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.layoutPanel.ResumeLayout(false);
            this.contextMenuStripSource.ResumeLayout(false);
            this.contextMenuStripDest.ResumeLayout(false);
            this.panSource.ResumeLayout(false);
            this.panSource.PerformLayout();
            this.panDest.ResumeLayout(false);
            this.panDest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel statLblDeleteSource;
    private System.Windows.Forms.ToolStripStatusLabel statLblNoChange;
    private System.Windows.Forms.ToolStripStatusLabel statLblAddDest;
    private System.Windows.Forms.ToolStripButton btnCompare;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.TableLayoutPanel layoutPanel;
    private System.Windows.Forms.Panel panDest;
    private System.Windows.Forms.Panel panSource;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.ToolStripButton btnOpenSource;
    private System.Windows.Forms.ToolStripButton btnOpenDest;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.TextBox txtSourceHeader;
    private System.Windows.Forms.TextBox txtDestHeader;
    private ICSharpCode.TextEditor.TextEditorControl txtSource;
    private ICSharpCode.TextEditor.TextEditorControl txtDest;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripButton btnPasteDest;
    private System.Windows.Forms.ToolStripButton btnPasteSource;
    private System.Windows.Forms.ContextMenuStrip contextMenuStripSource;
    private System.Windows.Forms.ToolStripMenuItem cMnuOpenSource;
    private System.Windows.Forms.ToolStripMenuItem cMnuPasteSource;
    private System.Windows.Forms.ContextMenuStrip contextMenuStripDest;
    private System.Windows.Forms.ToolStripMenuItem cMnuOpenDest;
    private System.Windows.Forms.ToolStripMenuItem cMnuPasteDest;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
  }
}
