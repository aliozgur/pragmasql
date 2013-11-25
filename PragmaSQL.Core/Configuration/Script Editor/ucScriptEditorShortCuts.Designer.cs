namespace PragmaSQL.Core
{
  partial class ucScriptEditorShortCuts
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
      this.lv = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.panEdit = new System.Windows.Forms.Panel();
      this.btnSet = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.cmbKeyCodes = new System.Windows.Forms.ComboBox();
      this.chkShift = new System.Windows.Forms.CheckBox();
      this.chkAlt = new System.Windows.Forms.CheckBox();
      this.chkControl = new System.Windows.Forms.CheckBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.button1 = new System.Windows.Forms.Button();
      this.panEdit.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // lv
      // 
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lv.FullRowSelect = true;
      this.lv.HideSelection = false;
      this.lv.Location = new System.Drawing.Point(0, 0);
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(483, 179);
      this.lv.TabIndex = 0;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
      this.lv.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Action";
      this.columnHeader1.Width = 248;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Shortcut Key";
      this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.columnHeader2.Width = 135;
      // 
      // panEdit
      // 
      this.panEdit.Controls.Add(this.btnSet);
      this.panEdit.Controls.Add(this.textBox1);
      this.panEdit.Controls.Add(this.cmbKeyCodes);
      this.panEdit.Controls.Add(this.chkShift);
      this.panEdit.Controls.Add(this.chkAlt);
      this.panEdit.Controls.Add(this.chkControl);
      this.panEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panEdit.Enabled = false;
      this.panEdit.Location = new System.Drawing.Point(0, 179);
      this.panEdit.Name = "panEdit";
      this.panEdit.Size = new System.Drawing.Size(483, 62);
      this.panEdit.TabIndex = 2;
      // 
      // btnSet
      // 
      this.btnSet.Location = new System.Drawing.Point(333, 20);
      this.btnSet.Name = "btnSet";
      this.btnSet.Size = new System.Drawing.Size(87, 23);
      this.btnSet.TabIndex = 5;
      this.btnSet.Text = "Set Shortcut";
      this.btnSet.UseVisualStyleBackColor = true;
      this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.SystemColors.Control;
      this.textBox1.Location = new System.Drawing.Point(8, 8);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(291, 20);
      this.textBox1.TabIndex = 4;
      // 
      // cmbKeyCodes
      // 
      this.cmbKeyCodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbKeyCodes.FormattingEnabled = true;
      this.cmbKeyCodes.Location = new System.Drawing.Point(178, 36);
      this.cmbKeyCodes.Name = "cmbKeyCodes";
      this.cmbKeyCodes.Size = new System.Drawing.Size(121, 21);
      this.cmbKeyCodes.Sorted = true;
      this.cmbKeyCodes.TabIndex = 3;
      // 
      // chkShift
      // 
      this.chkShift.AutoSize = true;
      this.chkShift.Location = new System.Drawing.Point(120, 38);
      this.chkShift.Name = "chkShift";
      this.chkShift.Size = new System.Drawing.Size(56, 17);
      this.chkShift.TabIndex = 2;
      this.chkShift.Text = "Shift +";
      this.chkShift.UseVisualStyleBackColor = true;
      // 
      // chkAlt
      // 
      this.chkAlt.AutoSize = true;
      this.chkAlt.Location = new System.Drawing.Point(64, 38);
      this.chkAlt.Name = "chkAlt";
      this.chkAlt.Size = new System.Drawing.Size(47, 17);
      this.chkAlt.TabIndex = 1;
      this.chkAlt.Text = "Alt +";
      this.chkAlt.UseVisualStyleBackColor = true;
      // 
      // chkControl
      // 
      this.chkControl.AutoSize = true;
      this.chkControl.Location = new System.Drawing.Point(9, 38);
      this.chkControl.Name = "chkControl";
      this.chkControl.Size = new System.Drawing.Size(50, 17);
      this.chkControl.TabIndex = 0;
      this.chkControl.Text = "Ctrl +";
      this.chkControl.UseVisualStyleBackColor = true;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.button1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point(0, 241);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(483, 34);
      this.panel2.TabIndex = 3;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(9, 5);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(104, 24);
      this.button1.TabIndex = 7;
      this.button1.Text = "Restotre Defaults";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // ucScriptEditorShortCuts
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.Controls.Add(this.lv);
      this.Controls.Add(this.panEdit);
      this.Controls.Add(this.panel2);
      this.Name = "ucScriptEditorShortCuts";
      this.Size = new System.Drawing.Size(483, 275);
      this.panEdit.ResumeLayout(false);
      this.panEdit.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.Panel panEdit;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.CheckBox chkShift;
    private System.Windows.Forms.CheckBox chkAlt;
    private System.Windows.Forms.CheckBox chkControl;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.ComboBox cmbKeyCodes;
    private System.Windows.Forms.Button btnSet;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button button1;
  }
}
