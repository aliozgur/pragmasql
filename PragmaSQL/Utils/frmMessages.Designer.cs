using PragmaSQL.Core;

namespace PragmaSQL
{
  partial class frmMessages
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessages));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnClear = new System.Windows.Forms.ToolStripButton();
			this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
			this.imgListMessages = new System.Windows.Forms.ImageList(this.components);
			this.lv = new System.Windows.Forms.ListView();
			this.colMessageType = new System.Windows.Forms.ColumnHeader();
			this.colDateTime = new System.Windows.Forms.ColumnHeader();
			this.colMessage = new System.Windows.Forms.ColumnHeader();
			this.colMethod = new System.Windows.Forms.ColumnHeader();
			this.colClass = new System.Windows.Forms.ColumnHeader();
			this.colAssembly = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.toolStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnSaveAs});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(754, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnClear
			// 
			this.btnClear.Image = global::PragmaSQL.Properties.Resources.delete;
			this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(52, 22);
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnSaveAs
			// 
			this.btnSaveAs.Image = global::PragmaSQL.Properties.Resources.SaveAs;
			this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSaveAs.Name = "btnSaveAs";
			this.btnSaveAs.Size = new System.Drawing.Size(85, 22);
			this.btnSaveAs.Text = "Save To File";
			this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
			// 
			// imgListMessages
			// 
			this.imgListMessages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListMessages.ImageStream")));
			this.imgListMessages.TransparentColor = System.Drawing.Color.Magenta;
			this.imgListMessages.Images.SetKeyName(0, "");
			this.imgListMessages.Images.SetKeyName(1, "");
			this.imgListMessages.Images.SetKeyName(2, "");
			this.imgListMessages.Images.SetKeyName(3, "bug.ico");
			this.imgListMessages.Images.SetKeyName(4, "");
			this.imgListMessages.Images.SetKeyName(5, "");
			this.imgListMessages.Images.SetKeyName(6, "");
			this.imgListMessages.Images.SetKeyName(7, "");
			// 
			// lv
			// 
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessageType,
            this.colDateTime,
            this.colMessage,
            this.colMethod,
            this.colClass,
            this.colAssembly});
			this.lv.ContextMenuStrip = this.contextMenuStrip1;
			this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv.FullRowSelect = true;
			this.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lv.HideSelection = false;
			this.lv.LabelWrap = false;
			this.lv.LargeImageList = this.imgListMessages;
			this.lv.Location = new System.Drawing.Point(0, 25);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(754, 397);
			this.lv.SmallImageList = this.imgListMessages;
			this.lv.TabIndex = 2;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			// 
			// colMessageType
			// 
			this.colMessageType.Text = "";
			this.colMessageType.Width = 28;
			// 
			// colDateTime
			// 
			this.colDateTime.Text = "DateTime";
			this.colDateTime.Width = 122;
			// 
			// colMessage
			// 
			this.colMessage.Text = "Message";
			this.colMessage.Width = 142;
			// 
			// colMethod
			// 
			this.colMethod.Text = "Method";
			this.colMethod.Width = 119;
			// 
			// colClass
			// 
			this.colClass.Text = "Class";
			this.colClass.Width = 121;
			// 
			// colAssembly
			// 
			this.colAssembly.Text = "Assembly";
			this.colAssembly.Width = 127;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.saveToFileToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(144, 48);
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// saveToFileToolStripMenuItem
			// 
			this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
			this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.saveToFileToolStripMenuItem.Text = "Save To File";
			this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "txt";
			this.saveFileDialog1.Filter = "Text Files|*.txt|Log Files|*.log|All Files|*.*";
			// 
			// frmMessages
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(754, 422);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.toolStrip1);
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMessages";
			this.TabText = "Application Messages";
			this.Text = "Application Messages";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnClear;
    private System.Windows.Forms.ToolStripButton btnSaveAs;
    private System.Windows.Forms.ImageList imgListMessages;
    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader colMessageType;
    private System.Windows.Forms.ColumnHeader colMessage;
    private System.Windows.Forms.ColumnHeader colMethod;
    private System.Windows.Forms.ColumnHeader colClass;
    private System.Windows.Forms.ColumnHeader colAssembly;
    private System.Windows.Forms.ColumnHeader colDateTime;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
  }
}