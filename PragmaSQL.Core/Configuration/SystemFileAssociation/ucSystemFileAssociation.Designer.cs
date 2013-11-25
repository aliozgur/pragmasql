namespace PragmaSQL.Core
{
  partial class ucSystemFileAssociation
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.extensionsListBox = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel3 = new System.Windows.Forms.Panel();
      this.chkAssociateSqlProjectFiles = new System.Windows.Forms.CheckBox();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.panel4 = new System.Windows.Forms.Panel();
      this.button1 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.panel5 = new System.Windows.Forms.Panel();
      this.lbAssociated = new System.Windows.Forms.ListBox();
      this.label2 = new System.Windows.Forms.Label();
      this.panel6 = new System.Windows.Forms.Panel();
      this.extensionGroupBox = new System.Windows.Forms.GroupBox();
      this.PerceivedTypeTextBox = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.extensionLabel = new System.Windows.Forms.Label();
      this.programIdTextBox = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.contentTypeTextBox = new System.Windows.Forms.TextBox();
      this.openWithListBox = new System.Windows.Forms.ListBox();
      this.label10 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel6.SuspendLayout();
      this.extensionGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel1.Controls.Add(this.extensionsListBox);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 53);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(154, 306);
      this.panel1.TabIndex = 2;
      // 
      // extensionsListBox
      // 
      this.extensionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.extensionsListBox.FormattingEnabled = true;
      this.extensionsListBox.Location = new System.Drawing.Point(0, 20);
      this.extensionsListBox.Name = "extensionsListBox";
      this.extensionsListBox.Size = new System.Drawing.Size(150, 277);
      this.extensionsListBox.TabIndex = 2;
      this.extensionsListBox.SelectedIndexChanged += new System.EventHandler(this.extensionsListBox_SelectedIndexChanged);
      this.extensionsListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.extensionsListBox_KeyPress);
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Control;
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(150, 20);
      this.label1.TabIndex = 1;
      this.label1.Text = "Available Extensions";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.chkAssociateSqlProjectFiles);
      this.panel3.Controls.Add(this.checkBox1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(614, 53);
      this.panel3.TabIndex = 3;
      // 
      // chkAssociateSqlProjectFiles
      // 
      this.chkAssociateSqlProjectFiles.AutoSize = true;
      this.chkAssociateSqlProjectFiles.Location = new System.Drawing.Point(3, 31);
      this.chkAssociateSqlProjectFiles.Name = "chkAssociateSqlProjectFiles";
      this.chkAssociateSqlProjectFiles.Size = new System.Drawing.Size(205, 17);
      this.chkAssociateSqlProjectFiles.TabIndex = 21;
      this.chkAssociateSqlProjectFiles.Text = "Associate .sqlprj files with PragmaSQL";
      this.chkAssociateSqlProjectFiles.UseVisualStyleBackColor = true;
      this.chkAssociateSqlProjectFiles.CheckedChanged += new System.EventHandler(this.chkAssociateSqlProjectFiles_CheckedChanged);
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(3, 9);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(150, 17);
      this.checkBox1.TabIndex = 20;
      this.checkBox1.Text = "Show File Association Info";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.button1);
      this.panel4.Controls.Add(this.button4);
      this.panel4.Controls.Add(this.button3);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel4.Location = new System.Drawing.Point(154, 53);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(116, 306);
      this.panel4.TabIndex = 6;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(6, 126);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(105, 28);
      this.button1.TabIndex = 2;
      this.button1.Text = "Refresh Ext.";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(6, 57);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(105, 28);
      this.button4.TabIndex = 1;
      this.button4.Text = "Remove <<";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(6, 23);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(105, 28);
      this.button3.TabIndex = 0;
      this.button3.Text = "Associate >>";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // panel5
      // 
      this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel5.Controls.Add(this.lbAssociated);
      this.panel5.Controls.Add(this.label2);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel5.Location = new System.Drawing.Point(270, 53);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(159, 306);
      this.panel5.TabIndex = 7;
      // 
      // lbAssociated
      // 
      this.lbAssociated.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbAssociated.FormattingEnabled = true;
      this.lbAssociated.Location = new System.Drawing.Point(0, 20);
      this.lbAssociated.Name = "lbAssociated";
      this.lbAssociated.Size = new System.Drawing.Size(155, 277);
      this.lbAssociated.TabIndex = 3;
      this.lbAssociated.SelectedIndexChanged += new System.EventHandler(this.lbAssociated_SelectedIndexChanged);
      this.lbAssociated.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbAssociated_KeyPress);
      // 
      // label2
      // 
      this.label2.BackColor = System.Drawing.SystemColors.Control;
      this.label2.Dock = System.Windows.Forms.DockStyle.Top;
      this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label2.Location = new System.Drawing.Point(0, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(155, 20);
      this.label2.TabIndex = 2;
      this.label2.Text = "Associated Extensions";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panel6
      // 
      this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel6.Controls.Add(this.extensionGroupBox);
      this.panel6.Controls.Add(this.label10);
      this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel6.Location = new System.Drawing.Point(429, 53);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(185, 306);
      this.panel6.TabIndex = 8;
      this.panel6.Visible = false;
      // 
      // extensionGroupBox
      // 
      this.extensionGroupBox.Controls.Add(this.PerceivedTypeTextBox);
      this.extensionGroupBox.Controls.Add(this.label8);
      this.extensionGroupBox.Controls.Add(this.label7);
      this.extensionGroupBox.Controls.Add(this.label6);
      this.extensionGroupBox.Controls.Add(this.label5);
      this.extensionGroupBox.Controls.Add(this.extensionLabel);
      this.extensionGroupBox.Controls.Add(this.programIdTextBox);
      this.extensionGroupBox.Controls.Add(this.label9);
      this.extensionGroupBox.Controls.Add(this.contentTypeTextBox);
      this.extensionGroupBox.Controls.Add(this.openWithListBox);
      this.extensionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.extensionGroupBox.Location = new System.Drawing.Point(0, 20);
      this.extensionGroupBox.Name = "extensionGroupBox";
      this.extensionGroupBox.Size = new System.Drawing.Size(181, 282);
      this.extensionGroupBox.TabIndex = 2;
      this.extensionGroupBox.TabStop = false;
      // 
      // PerceivedTypeTextBox
      // 
      this.PerceivedTypeTextBox.Location = new System.Drawing.Point(105, 109);
      this.PerceivedTypeTextBox.Name = "PerceivedTypeTextBox";
      this.PerceivedTypeTextBox.ReadOnly = true;
      this.PerceivedTypeTextBox.Size = new System.Drawing.Size(114, 20);
      this.PerceivedTypeTextBox.TabIndex = 33;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(6, 138);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(80, 13);
      this.label8.TabIndex = 32;
      this.label8.Text = "Open With List:";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 111);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(85, 13);
      this.label7.TabIndex = 31;
      this.label7.Text = "Perceived Type:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 81);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(74, 13);
      this.label6.TabIndex = 30;
      this.label6.Text = "Content Type:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 51);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(43, 13);
      this.label5.TabIndex = 29;
      this.label5.Text = "ProgID:";
      // 
      // extensionLabel
      // 
      this.extensionLabel.Location = new System.Drawing.Point(106, 25);
      this.extensionLabel.Name = "extensionLabel";
      this.extensionLabel.Size = new System.Drawing.Size(113, 15);
      this.extensionLabel.TabIndex = 17;
      // 
      // programIdTextBox
      // 
      this.programIdTextBox.Location = new System.Drawing.Point(105, 47);
      this.programIdTextBox.Name = "programIdTextBox";
      this.programIdTextBox.ReadOnly = true;
      this.programIdTextBox.Size = new System.Drawing.Size(114, 20);
      this.programIdTextBox.TabIndex = 0;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(6, 25);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(44, 13);
      this.label9.TabIndex = 16;
      this.label9.Text = "Current:";
      // 
      // contentTypeTextBox
      // 
      this.contentTypeTextBox.Location = new System.Drawing.Point(105, 77);
      this.contentTypeTextBox.Name = "contentTypeTextBox";
      this.contentTypeTextBox.ReadOnly = true;
      this.contentTypeTextBox.Size = new System.Drawing.Size(114, 20);
      this.contentTypeTextBox.TabIndex = 1;
      // 
      // openWithListBox
      // 
      this.openWithListBox.FormattingEnabled = true;
      this.openWithListBox.Location = new System.Drawing.Point(106, 138);
      this.openWithListBox.Name = "openWithListBox";
      this.openWithListBox.Size = new System.Drawing.Size(113, 82);
      this.openWithListBox.TabIndex = 3;
      // 
      // label10
      // 
      this.label10.BackColor = System.Drawing.SystemColors.Control;
      this.label10.Dock = System.Windows.Forms.DockStyle.Top;
      this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
      this.label10.Location = new System.Drawing.Point(0, 0);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(181, 20);
      this.label10.TabIndex = 3;
      this.label10.Text = "File Association Info";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ucSystemFileAssociation
      // 
      this.Controls.Add(this.panel6);
      this.Controls.Add(this.panel5);
      this.Controls.Add(this.panel4);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel3);
      this.Name = "ucSystemFileAssociation";
      this.Size = new System.Drawing.Size(614, 359);
      this.panel1.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.panel6.ResumeLayout(false);
      this.extensionGroupBox.ResumeLayout(false);
      this.extensionGroupBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ListBox extensionsListBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.ListBox lbAssociated;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.GroupBox extensionGroupBox;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label extensionLabel;
    private System.Windows.Forms.TextBox programIdTextBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox contentTypeTextBox;
    private System.Windows.Forms.ListBox openWithListBox;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox PerceivedTypeTextBox;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.CheckBox chkAssociateSqlProjectFiles;
  }
}
