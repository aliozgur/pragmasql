namespace PragmaSQL
{
  partial class frmObjectChangeHistoryViewer
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectChangeHistoryViewer));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.cmbObjectType = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.txtObjectName = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnList = new System.Windows.Forms.ToolStripButton();
      this.btnStop = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnToggleScriptPanel = new System.Windows.Forms.ToolStripButton();
      this.panScript = new System.Windows.Forms.Panel();
      this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
      this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
      this.splitterScript = new System.Windows.Forms.Splitter();
      this.grd = new System.Windows.Forms.DataGridView();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cMnuDiff = new System.Windows.Forms.ToolStripMenuItem();
      this.cMnuDiffAsSource = new System.Windows.Forms.ToolStripMenuItem();
      this.cMnuDiffAsDest = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
      this.txtServerName = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
      this.txtDBName = new System.Windows.Forms.ToolStripTextBox();
      this.popUpEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.tsMnuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
      this.popUpTab = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cMnuItemClose = new System.Windows.Forms.ToolStripMenuItem();
      this.cMnuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
      this.cMnuCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1.SuspendLayout();
      this.panScript.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.popUpEditor.SuspendLayout();
      this.popUpTab.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.cmbObjectType,
            this.toolStripLabel1,
            this.txtObjectName,
            this.toolStripSeparator1,
            this.btnList,
            this.btnStop,
            this.toolStripSeparator2,
            this.btnToggleScriptPanel});
      this.toolStrip1.Location = new System.Drawing.Point(0, 25);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(827, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(42, 22);
      this.toolStripLabel2.Text = "Type   ";
      // 
      // cmbObjectType
      // 
      this.cmbObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbObjectType.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
      this.cmbObjectType.Name = "cmbObjectType";
      this.cmbObjectType.Size = new System.Drawing.Size(130, 25);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(77, 22);
      this.toolStripLabel1.Text = "Object Name";
      // 
      // txtObjectName
      // 
      this.txtObjectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtObjectName.Name = "txtObjectName";
      this.txtObjectName.Size = new System.Drawing.Size(200, 25);
      this.txtObjectName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObjectName_KeyDown);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // btnList
      // 
      this.btnList.Image = global::PragmaSQL.Properties.Resources.search;
      this.btnList.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnList.Name = "btnList";
      this.btnList.Size = new System.Drawing.Size(45, 22);
      this.btnList.Text = "List";
      this.btnList.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // btnStop
      // 
      this.btnStop.Enabled = false;
      this.btnStop.Image = global::PragmaSQL.Properties.Resources.Stop;
      this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(51, 22);
      this.btnStop.Text = "Stop";
      this.btnStop.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // btnToggleScriptPanel
      // 
      this.btnToggleScriptPanel.Checked = true;
      this.btnToggleScriptPanel.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnToggleScriptPanel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnToggleScriptPanel.Image = global::PragmaSQL.Properties.Resources.EditCode;
      this.btnToggleScriptPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnToggleScriptPanel.Name = "btnToggleScriptPanel";
      this.btnToggleScriptPanel.Size = new System.Drawing.Size(23, 22);
      this.btnToggleScriptPanel.Text = "Toggle Script Panel";
      this.btnToggleScriptPanel.Click += new System.EventHandler(this.btnToggleScriptPanel_Click);
      // 
      // panScript
      // 
      this.panScript.BackColor = System.Drawing.SystemColors.Window;
      this.panScript.Controls.Add(this.kryptonHeader1);
      this.panScript.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panScript.Location = new System.Drawing.Point(0, 280);
      this.panScript.Name = "panScript";
      this.panScript.Size = new System.Drawing.Size(827, 233);
      this.panScript.TabIndex = 4;
      // 
      // kryptonHeader1
      // 
      this.kryptonHeader1.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
      this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
      this.kryptonHeader1.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
      this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
      this.kryptonHeader1.Name = "kryptonHeader1";
      this.kryptonHeader1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.kryptonHeader1.Size = new System.Drawing.Size(827, 28);
      this.kryptonHeader1.TabIndex = 3;
      this.kryptonHeader1.Text = "Object Script";
      this.kryptonHeader1.Values.Description = "";
      this.kryptonHeader1.Values.Heading = "Object Script";
      this.kryptonHeader1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonHeader1.Values.Image")));
      // 
      // buttonSpecAny1
      // 
      this.buttonSpecAny1.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Inherit;
      this.buttonSpecAny1.ExtraText = "";
      this.buttonSpecAny1.Image = null;
      this.buttonSpecAny1.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
      this.buttonSpecAny1.Text = "Close";
      this.buttonSpecAny1.UniqueName = "577E7C49D63A4DCE577E7C49D63A4DCE";
      this.buttonSpecAny1.Click += new System.EventHandler(this.buttonSpecAny1_Click);
      // 
      // splitterScript
      // 
      this.splitterScript.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitterScript.Location = new System.Drawing.Point(0, 277);
      this.splitterScript.Name = "splitterScript";
      this.splitterScript.Size = new System.Drawing.Size(827, 3);
      this.splitterScript.TabIndex = 5;
      this.splitterScript.TabStop = false;
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToOrderColumns = true;
      this.grd.AllowUserToResizeRows = false;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.grd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.ContextMenuStrip = this.contextMenuStrip1;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 50);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.Size = new System.Drawing.Size(827, 227);
      this.grd.TabIndex = 6;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMnuDiff});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
      this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
      // 
      // cMnuDiff
      // 
      this.cMnuDiff.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMnuDiffAsSource,
            this.cMnuDiffAsDest});
      this.cMnuDiff.Enabled = false;
      this.cMnuDiff.Name = "cMnuDiff";
      this.cMnuDiff.Size = new System.Drawing.Size(93, 22);
      this.cMnuDiff.Text = "Diff";
      // 
      // cMnuDiffAsSource
      // 
      this.cMnuDiffAsSource.Name = "cMnuDiffAsSource";
      this.cMnuDiffAsSource.Size = new System.Drawing.Size(126, 22);
      this.cMnuDiffAsSource.Text = "As Source";
      this.cMnuDiffAsSource.Click += new System.EventHandler(this.cMnuDiffAsSource_Click);
      // 
      // cMnuDiffAsDest
      // 
      this.cMnuDiffAsDest.Name = "cMnuDiffAsDest";
      this.cMnuDiffAsDest.Size = new System.Drawing.Size(126, 22);
      this.cMnuDiffAsDest.Text = "As Dest";
      this.cMnuDiffAsDest.Click += new System.EventHandler(this.cMnuDiffAsDest_Click);
      // 
      // toolStrip2
      // 
      this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel5,
            this.txtServerName,
            this.toolStripLabel6,
            this.txtDBName});
      this.toolStrip2.Location = new System.Drawing.Point(0, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(827, 25);
      this.toolStrip2.TabIndex = 7;
      this.toolStrip2.Text = "toolStrip2";
      // 
      // toolStripLabel5
      // 
      this.toolStripLabel5.Name = "toolStripLabel5";
      this.toolStripLabel5.Size = new System.Drawing.Size(39, 22);
      this.toolStripLabel5.Text = "Server";
      // 
      // txtServerName
      // 
      this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtServerName.Name = "txtServerName";
      this.txtServerName.Size = new System.Drawing.Size(130, 25);
      this.txtServerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObjectName_KeyDown);
      // 
      // toolStripLabel6
      // 
      this.toolStripLabel6.Name = "toolStripLabel6";
      this.toolStripLabel6.Size = new System.Drawing.Size(70, 22);
      this.toolStripLabel6.Text = "Database     ";
      // 
      // txtDBName
      // 
      this.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtDBName.Name = "txtDBName";
      this.txtDBName.Size = new System.Drawing.Size(130, 25);
      this.txtDBName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObjectName_KeyDown);
      // 
      // popUpEditor
      // 
      this.popUpEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMnuItemCopy});
      this.popUpEditor.Name = "contextMenuEditor";
      this.popUpEditor.Size = new System.Drawing.Size(103, 26);
      // 
      // tsMnuItemCopy
      // 
      this.tsMnuItemCopy.Image = global::PragmaSQL.Properties.Resources.copy;
      this.tsMnuItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsMnuItemCopy.Name = "tsMnuItemCopy";
      this.tsMnuItemCopy.ShortcutKeyDisplayString = "";
      this.tsMnuItemCopy.Size = new System.Drawing.Size(102, 22);
      this.tsMnuItemCopy.Text = "Copy";
      this.tsMnuItemCopy.Click += new System.EventHandler(this.tsMnuItemCopy_Click);
      // 
      // popUpTab
      // 
      this.popUpTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMnuItemClose,
            this.cMnuCloseAll,
            this.cMnuCloseAllButThis});
      this.popUpTab.Name = "contextMenuTab";
      this.popUpTab.Size = new System.Drawing.Size(167, 70);
      // 
      // cMnuItemClose
      // 
      this.cMnuItemClose.Name = "cMnuItemClose";
      this.cMnuItemClose.Size = new System.Drawing.Size(166, 22);
      this.cMnuItemClose.Text = "Close";
      this.cMnuItemClose.Click += new System.EventHandler(this.cMnuItemClose_Click);
      // 
      // cMnuCloseAll
      // 
      this.cMnuCloseAll.Name = "cMnuCloseAll";
      this.cMnuCloseAll.Size = new System.Drawing.Size(166, 22);
      this.cMnuCloseAll.Text = "Close All";
      this.cMnuCloseAll.Click += new System.EventHandler(this.cMnuCloseAll_Click);
      // 
      // cMnuCloseAllButThis
      // 
      this.cMnuCloseAllButThis.Name = "cMnuCloseAllButThis";
      this.cMnuCloseAllButThis.Size = new System.Drawing.Size(166, 22);
      this.cMnuCloseAllButThis.Text = "Close All But This";
      this.cMnuCloseAllButThis.Click += new System.EventHandler(this.cMnuCloseAllButThis_Click);
      // 
      // frmObjectChangeHistoryViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(827, 513);
      this.Controls.Add(this.grd);
      this.Controls.Add(this.splitterScript);
      this.Controls.Add(this.panScript);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.toolStrip2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmObjectChangeHistoryViewer";
      this.ShowInTaskbar = false;
      this.TabPageContextMenuStrip = this.popUpTab;
      this.TabText = "Change Hist.";
      this.Text = "Change Hist.";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmObjectChangeHistoryViewer_FormClosing);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.panScript.ResumeLayout(false);
      this.panScript.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.popUpEditor.ResumeLayout(false);
      this.popUpTab.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox txtObjectName;
    private System.Windows.Forms.ToolStripButton btnList;
    private System.Windows.Forms.Panel panScript;
    private System.Windows.Forms.Splitter splitterScript;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.ToolStripComboBox cmbObjectType;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel5;
    private System.Windows.Forms.ToolStripTextBox txtServerName;
    private System.Windows.Forms.ToolStripLabel toolStripLabel6;
    private System.Windows.Forms.ToolStripTextBox txtDBName;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ContextMenuStrip popUpEditor;
    private System.Windows.Forms.ToolStripMenuItem tsMnuItemCopy;
    private System.Windows.Forms.ContextMenuStrip popUpTab;
    private System.Windows.Forms.ToolStripMenuItem cMnuItemClose;
    private System.Windows.Forms.ToolStripMenuItem cMnuCloseAll;
    private System.Windows.Forms.ToolStripMenuItem cMnuCloseAllButThis;
    private System.Windows.Forms.ToolStripButton btnToggleScriptPanel;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem cMnuDiff;
    private System.Windows.Forms.ToolStripMenuItem cMnuDiffAsSource;
    private System.Windows.Forms.ToolStripMenuItem cMnuDiffAsDest;
    private System.Windows.Forms.ToolStripButton btnStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader kryptonHeader1;
    private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
  }
}