namespace SQLManagement
{
  partial class frmIndexDBCC
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndexDBCC));
      this.panel1 = new System.Windows.Forms.Panel();
      this.gbRebuild = new System.Windows.Forms.GroupBox();
      this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
      this.rbReset = new System.Windows.Forms.RadioButton();
      this.rbUseOriginal = new System.Windows.Forms.RadioButton();
      this.lblProgress = new System.Windows.Forms.Label();
      this.gbUpdateUsage = new System.Windows.Forms.GroupBox();
      this.chkWithCountRows = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.lblIndexName = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbOperation = new System.Windows.Forms.ComboBox();
      this.lblObjectName = new System.Windows.Forms.Label();
      this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.queryViewer = new PragmaSQL.Core.DBQueryResultViewer();
      this.panel1.SuspendLayout();
      this.gbRebuild.SuspendLayout();
      this.gbUpdateUsage.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.gbRebuild);
      this.panel1.Controls.Add(this.lblProgress);
      this.panel1.Controls.Add(this.gbUpdateUsage);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.lblIndexName);
      this.panel1.Controls.Add(this.button1);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.cmbOperation);
      this.panel1.Controls.Add(this.lblObjectName);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(743, 126);
      this.panel1.TabIndex = 0;
      // 
      // gbRebuild
      // 
      this.gbRebuild.Controls.Add(this.maskedTextBox1);
      this.gbRebuild.Controls.Add(this.rbReset);
      this.gbRebuild.Controls.Add(this.rbUseOriginal);
      this.gbRebuild.Location = new System.Drawing.Point(255, 49);
      this.gbRebuild.Name = "gbRebuild";
      this.gbRebuild.Size = new System.Drawing.Size(155, 55);
      this.gbRebuild.TabIndex = 14;
      this.gbRebuild.TabStop = false;
      this.gbRebuild.Visible = false;
      // 
      // maskedTextBox1
      // 
      this.maskedTextBox1.Location = new System.Drawing.Point(85, 30);
      this.maskedTextBox1.Mask = "000";
      this.maskedTextBox1.Name = "maskedTextBox1";
      this.maskedTextBox1.ReadOnly = true;
      this.maskedTextBox1.Size = new System.Drawing.Size(51, 20);
      this.maskedTextBox1.TabIndex = 2;
      this.maskedTextBox1.Text = "0";
      // 
      // rbReset
      // 
      this.rbReset.AutoSize = true;
      this.rbReset.Location = new System.Drawing.Point(6, 30);
      this.rbReset.Name = "rbReset";
      this.rbReset.Size = new System.Drawing.Size(53, 17);
      this.rbReset.TabIndex = 1;
      this.rbReset.Text = "Reset";
      this.rbReset.UseVisualStyleBackColor = true;
      // 
      // rbUseOriginal
      // 
      this.rbUseOriginal.AutoSize = true;
      this.rbUseOriginal.Checked = true;
      this.rbUseOriginal.Location = new System.Drawing.Point(7, 9);
      this.rbUseOriginal.Name = "rbUseOriginal";
      this.rbUseOriginal.Size = new System.Drawing.Size(80, 17);
      this.rbUseOriginal.TabIndex = 0;
      this.rbUseOriginal.TabStop = true;
      this.rbUseOriginal.Text = "Use original";
      this.rbUseOriginal.UseVisualStyleBackColor = true;
      this.rbUseOriginal.CheckedChanged += new System.EventHandler(this.rbUseOriginal_CheckedChanged);
      // 
      // lblProgress
      // 
      this.lblProgress.AutoSize = true;
      this.lblProgress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblProgress.Location = new System.Drawing.Point(134, 92);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(74, 13);
      this.lblProgress.TabIndex = 21;
      this.lblProgress.Text = "In progress...";
      this.lblProgress.Visible = false;
      // 
      // gbUpdateUsage
      // 
      this.gbUpdateUsage.Controls.Add(this.chkWithCountRows);
      this.gbUpdateUsage.Location = new System.Drawing.Point(416, 49);
      this.gbUpdateUsage.Name = "gbUpdateUsage";
      this.gbUpdateUsage.Size = new System.Drawing.Size(127, 49);
      this.gbUpdateUsage.TabIndex = 20;
      this.gbUpdateUsage.TabStop = false;
      this.gbUpdateUsage.Visible = false;
      // 
      // chkWithCountRows
      // 
      this.chkWithCountRows.AutoSize = true;
      this.chkWithCountRows.Location = new System.Drawing.Point(10, 20);
      this.chkWithCountRows.Name = "chkWithCountRows";
      this.chkWithCountRows.Size = new System.Drawing.Size(103, 17);
      this.chkWithCountRows.TabIndex = 0;
      this.chkWithCountRows.Text = "With count rows";
      this.chkWithCountRows.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.Location = new System.Drawing.Point(12, 25);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 13);
      this.label1.TabIndex = 19;
      this.label1.Text = "Index Name:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label3.Location = new System.Drawing.Point(12, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(108, 13);
      this.label3.TabIndex = 18;
      this.label3.Text = "Table/View Name:";
      // 
      // lblIndexName
      // 
      this.lblIndexName.AutoSize = true;
      this.lblIndexName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblIndexName.Location = new System.Drawing.Point(126, 25);
      this.lblIndexName.Name = "lblIndexName";
      this.lblIndexName.Size = new System.Drawing.Size(69, 13);
      this.lblIndexName.TabIndex = 17;
      this.lblIndexName.Text = "Index Name:";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(10, 87);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(116, 22);
      this.button1.TabIndex = 16;
      this.button1.Text = "Execute Command";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(53, 13);
      this.label2.TabIndex = 15;
      this.label2.Text = "Operation";
      // 
      // cmbOperation
      // 
      this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbOperation.FormattingEnabled = true;
      this.cmbOperation.Items.AddRange(new object[] {
            "Check fragmentation",
            "Check Index",
            "Rebuild Index",
            "Show Statistics",
            "Update Usage"});
      this.cmbOperation.Location = new System.Drawing.Point(12, 57);
      this.cmbOperation.Name = "cmbOperation";
      this.cmbOperation.Size = new System.Drawing.Size(237, 21);
      this.cmbOperation.TabIndex = 13;
      this.cmbOperation.SelectedIndexChanged += new System.EventHandler(this.cmbOperation_SelectedIndexChanged);
      // 
      // lblObjectName
      // 
      this.lblObjectName.AutoSize = true;
      this.lblObjectName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblObjectName.Location = new System.Drawing.Point(126, 9);
      this.lblObjectName.Name = "lblObjectName";
      this.lblObjectName.Size = new System.Drawing.Size(93, 13);
      this.lblObjectName.TabIndex = 12;
      this.lblObjectName.Text = "Table/View Name:";
      // 
      // contextMenuStrip2
      // 
      this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem});
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new System.Drawing.Size(167, 70);
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
      // queryViewer
      // 
      this.queryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.queryViewer.GridContextMenuStrip = null;
      this.queryViewer.GridHeight = 200;
      this.queryViewer.Location = new System.Drawing.Point(0, 126);
      this.queryViewer.Name = "queryViewer";
      this.queryViewer.ResultBackColor = System.Drawing.SystemColors.Window;
      this.queryViewer.Size = new System.Drawing.Size(743, 311);
      this.queryViewer.SplitterBackColor = System.Drawing.SystemColors.Control;
      this.queryViewer.TabIndex = 1;
      // 
      // frmIndexDBCC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(743, 437);
      this.Controls.Add(this.queryViewer);
      this.Controls.Add(this.panel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmIndexDBCC";
      this.ShowInTaskbar = false;
      this.TabPageContextMenuStrip = this.contextMenuStrip2;
      this.TabText = "Index DBCC";
      this.Text = "Index DBCC";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.gbRebuild.ResumeLayout(false);
      this.gbRebuild.PerformLayout();
      this.gbUpdateUsage.ResumeLayout(false);
      this.gbUpdateUsage.PerformLayout();
      this.contextMenuStrip2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox gbRebuild;
    private System.Windows.Forms.MaskedTextBox maskedTextBox1;
    private System.Windows.Forms.RadioButton rbReset;
    private System.Windows.Forms.RadioButton rbUseOriginal;
    private System.Windows.Forms.Label lblProgress;
    private System.Windows.Forms.GroupBox gbUpdateUsage;
    private System.Windows.Forms.CheckBox chkWithCountRows;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lblIndexName;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbOperation;
    private System.Windows.Forms.Label lblObjectName;
    private PragmaSQL.Core.DBQueryResultViewer queryViewer;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;

  }
}