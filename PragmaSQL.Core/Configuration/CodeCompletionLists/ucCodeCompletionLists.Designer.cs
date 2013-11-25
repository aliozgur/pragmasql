namespace PragmaSQL.Core
{
  partial class ucCodeCompletionLists
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.lbLists = new System.Windows.Forms.ListBox();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.txtListName = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
      this.panel5 = new System.Windows.Forms.Panel();
      this.lblOutputPaneHeader = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label3 = new System.Windows.Forms.Label();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel2 = new System.Windows.Forms.Panel();
      this.panel7 = new System.Windows.Forms.Panel();
      this.panel8 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.txtTemplateCode = new System.Windows.Forms.TextBox();
      this.splitter2 = new System.Windows.Forms.Splitter();
      this.lbItems = new System.Windows.Forms.ListBox();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.txtTemplateName = new System.Windows.Forms.ToolStripTextBox();
      this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
      this.panel4 = new System.Windows.Forms.Panel();
      this.panel6 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel7.SuspendLayout();
      this.panel8.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.panel6.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lbLists);
      this.panel1.Controls.Add(this.toolStrip1);
      this.panel1.Controls.Add(this.panel5);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 28);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(217, 389);
      this.panel1.TabIndex = 0;
      // 
      // lbLists
      // 
      this.lbLists.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbLists.FormattingEnabled = true;
      this.lbLists.Location = new System.Drawing.Point(0, 45);
      this.lbLists.Name = "lbLists";
      this.lbLists.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lbLists.Size = new System.Drawing.Size(217, 342);
      this.lbLists.TabIndex = 1;
      this.lbLists.SelectedIndexChanged += new System.EventHandler(this.lbLists_SelectedIndexChanged);
      this.lbLists.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbLists_KeyUp);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.txtListName,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
      this.toolStrip1.Location = new System.Drawing.Point(0, 20);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(217, 25);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(39, 22);
      this.toolStripLabel2.Text = "Name";
      // 
      // txtListName
      // 
      this.txtListName.Name = "txtListName";
      this.txtListName.Size = new System.Drawing.Size(100, 25);
      this.txtListName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtListName_KeyDown);
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::PragmaSQL.Core.Properties.Resources.new_style_2;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "New List";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = global::PragmaSQL.Core.Properties.Resources.delete;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton2.Text = "Delete";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // toolStripButton3
      // 
      this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton3.Image = global::PragmaSQL.Core.Properties.Resources.RenameFolder;
      this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton3.Text = "Rename";
      this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
      // 
      // panel5
      // 
      this.panel5.BackColor = System.Drawing.SystemColors.Control;
      this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel5.Controls.Add(this.lblOutputPaneHeader);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel5.Location = new System.Drawing.Point(0, 0);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(217, 20);
      this.panel5.TabIndex = 2;
      // 
      // lblOutputPaneHeader
      // 
      this.lblOutputPaneHeader.BackColor = System.Drawing.SystemColors.Control;
      this.lblOutputPaneHeader.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblOutputPaneHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblOutputPaneHeader.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblOutputPaneHeader.Location = new System.Drawing.Point(0, 0);
      this.lblOutputPaneHeader.Name = "lblOutputPaneHeader";
      this.lblOutputPaneHeader.Size = new System.Drawing.Size(213, 16);
      this.lblOutputPaneHeader.TabIndex = 0;
      this.lblOutputPaneHeader.Text = "Code Completion Lists";
      this.lblOutputPaneHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.label3);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(647, 28);
      this.panel3.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 5);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(221, 13);
      this.label3.TabIndex = 15;
      this.label3.Text = "Use Control+J to invoke code completion lists";
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(217, 28);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 389);
      this.splitter1.TabIndex = 5;
      this.splitter1.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.panel7);
      this.panel2.Controls.Add(this.panel4);
      this.panel2.Controls.Add(this.panel6);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(220, 28);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(427, 389);
      this.panel2.TabIndex = 6;
      // 
      // panel7
      // 
      this.panel7.Controls.Add(this.panel8);
      this.panel7.Controls.Add(this.splitter2);
      this.panel7.Controls.Add(this.lbItems);
      this.panel7.Controls.Add(this.toolStrip2);
      this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel7.Location = new System.Drawing.Point(0, 20);
      this.panel7.Name = "panel7";
      this.panel7.Size = new System.Drawing.Size(427, 369);
      this.panel7.TabIndex = 5;
      // 
      // panel8
      // 
      this.panel8.Controls.Add(this.label2);
      this.panel8.Controls.Add(this.txtTemplateCode);
      this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel8.Location = new System.Drawing.Point(0, 188);
      this.panel8.Name = "panel8";
      this.panel8.Size = new System.Drawing.Size(427, 181);
      this.panel8.TabIndex = 8;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 5);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Code";
      // 
      // txtTemplateCode
      // 
      this.txtTemplateCode.AcceptsReturn = true;
      this.txtTemplateCode.AcceptsTab = true;
      this.txtTemplateCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTemplateCode.HideSelection = false;
      this.txtTemplateCode.Location = new System.Drawing.Point(9, 23);
      this.txtTemplateCode.Multiline = true;
      this.txtTemplateCode.Name = "txtTemplateCode";
      this.txtTemplateCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtTemplateCode.Size = new System.Drawing.Size(405, 147);
      this.txtTemplateCode.TabIndex = 7;
      this.txtTemplateCode.Leave += new System.EventHandler(this.txtItemContent_Leave);
      // 
      // splitter2
      // 
      this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter2.Location = new System.Drawing.Point(0, 185);
      this.splitter2.Name = "splitter2";
      this.splitter2.Size = new System.Drawing.Size(427, 3);
      this.splitter2.TabIndex = 7;
      this.splitter2.TabStop = false;
      // 
      // lbItems
      // 
      this.lbItems.Dock = System.Windows.Forms.DockStyle.Top;
      this.lbItems.FormattingEnabled = true;
      this.lbItems.Location = new System.Drawing.Point(0, 25);
      this.lbItems.Name = "lbItems";
      this.lbItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lbItems.Size = new System.Drawing.Size(427, 160);
      this.lbItems.TabIndex = 4;
      this.lbItems.SelectedIndexChanged += new System.EventHandler(this.lbItems_SelectedIndexChanged);
      this.lbItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbItems_KeyUp);
      // 
      // toolStrip2
      // 
      this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtTemplateName,
            this.toolStripButton4,
            this.toolStripButton5});
      this.toolStrip2.Location = new System.Drawing.Point(0, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new System.Drawing.Size(427, 25);
      this.toolStrip2.TabIndex = 5;
      this.toolStrip2.Text = "toolStrip2";
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(57, 22);
      this.toolStripLabel1.Text = "Template";
      // 
      // txtTemplateName
      // 
      this.txtTemplateName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtTemplateName.Name = "txtTemplateName";
      this.txtTemplateName.Size = new System.Drawing.Size(120, 25);
      this.txtTemplateName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContentKey_KeyDown);
      // 
      // toolStripButton4
      // 
      this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.Image = global::PragmaSQL.Core.Properties.Resources.new_style_2;
      this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton4.Text = "New List";
      this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
      // 
      // toolStripButton5
      // 
      this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Image = global::PragmaSQL.Core.Properties.Resources.delete;
      this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton5.Text = "Delete";
      this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
      // 
      // panel4
      // 
      this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel4.Location = new System.Drawing.Point(0, 20);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(427, 369);
      this.panel4.TabIndex = 1;
      // 
      // panel6
      // 
      this.panel6.BackColor = System.Drawing.SystemColors.Control;
      this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel6.Controls.Add(this.label1);
      this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel6.Location = new System.Drawing.Point(0, 0);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(427, 20);
      this.panel6.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Control;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(423, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "List Content";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ucCodeCompletionLists
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel3);
      this.Name = "ucCodeCompletionLists";
      this.Size = new System.Drawing.Size(647, 417);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.panel5.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel7.ResumeLayout(false);
      this.panel7.PerformLayout();
      this.panel8.ResumeLayout(false);
      this.panel8.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.panel6.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ListBox lbLists;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Label lblOutputPaneHeader;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel7;
    private System.Windows.Forms.ListBox lbItems;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox txtTemplateName;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
    private System.Windows.Forms.Panel panel8;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTemplateCode;
    private System.Windows.Forms.Splitter splitter2;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripTextBox txtListName;
    private System.Windows.Forms.Label label3;
  }
}
