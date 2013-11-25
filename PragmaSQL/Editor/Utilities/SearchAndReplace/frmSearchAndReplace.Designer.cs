namespace PragmaSQL
{
  partial class SearchAndReplaceForm
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
      this.panFind = new System.Windows.Forms.Panel();
      this.panMax = new System.Windows.Forms.Panel();
      this.panMin = new System.Windows.Forms.Panel();
      this.cmbSearchType = new System.Windows.Forms.ComboBox();
      this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
      this.replaceAllButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.lblReplaceWith = new System.Windows.Forms.Label();
      this.cmbReplaceHist = new System.Windows.Forms.ComboBox();
      this.replaceButton = new System.Windows.Forms.Button();
      this.chkReplace = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cmbSearchHist = new System.Windows.Forms.ComboBox();
      this.searchButton = new System.Windows.Forms.Button();
      this.panFind.SuspendLayout();
      this.SuspendLayout();
      // 
      // panFind
      // 
      this.panFind.Controls.Add(this.panMax);
      this.panFind.Controls.Add(this.panMin);
      this.panFind.Controls.Add(this.cmbSearchType);
      this.panFind.Controls.Add(this.chkIgnoreCase);
      this.panFind.Controls.Add(this.replaceAllButton);
      this.panFind.Controls.Add(this.cancelButton);
      this.panFind.Controls.Add(this.lblReplaceWith);
      this.panFind.Controls.Add(this.cmbReplaceHist);
      this.panFind.Controls.Add(this.replaceButton);
      this.panFind.Controls.Add(this.chkReplace);
      this.panFind.Controls.Add(this.label1);
      this.panFind.Controls.Add(this.cmbSearchHist);
      this.panFind.Controls.Add(this.searchButton);
      this.panFind.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panFind.Location = new System.Drawing.Point(0, 0);
      this.panFind.Name = "panFind";
      this.panFind.Size = new System.Drawing.Size(398, 138);
      this.panFind.TabIndex = 0;
      // 
      // panMax
      // 
      this.panMax.Location = new System.Drawing.Point(391, 1);
      this.panMax.Name = "panMax";
      this.panMax.Size = new System.Drawing.Size(4, 134);
      this.panMax.TabIndex = 44;
      this.panMax.Visible = false;
      // 
      // panMin
      // 
      this.panMin.Location = new System.Drawing.Point(1, -1);
      this.panMin.Name = "panMin";
      this.panMin.Size = new System.Drawing.Size(5, 92);
      this.panMin.TabIndex = 42;
      this.panMin.Visible = false;
      // 
      // cmbSearchType
      // 
      this.cmbSearchType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbSearchType.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbSearchType.FormattingEnabled = true;
      this.cmbSearchType.Location = new System.Drawing.Point(94, 110);
      this.cmbSearchType.Name = "cmbSearchType";
      this.cmbSearchType.Size = new System.Drawing.Size(136, 21);
      this.cmbSearchType.TabIndex = 6;
      // 
      // chkIgnoreCase
      // 
      this.chkIgnoreCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkIgnoreCase.AutoSize = true;
      this.chkIgnoreCase.Checked = true;
      this.chkIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkIgnoreCase.Location = new System.Drawing.Point(10, 112);
      this.chkIgnoreCase.Name = "chkIgnoreCase";
      this.chkIgnoreCase.Size = new System.Drawing.Size(82, 17);
      this.chkIgnoreCase.TabIndex = 7;
      this.chkIgnoreCase.Text = "Ignore &case";
      this.chkIgnoreCase.UseVisualStyleBackColor = true;
      // 
      // replaceAllButton
      // 
      this.replaceAllButton.Location = new System.Drawing.Point(306, 62);
      this.replaceAllButton.Name = "replaceAllButton";
      this.replaceAllButton.Size = new System.Drawing.Size(79, 26);
      this.replaceAllButton.TabIndex = 3;
      this.replaceAllButton.Text = "Replace &All";
      this.replaceAllButton.UseVisualStyleBackColor = true;
      this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(305, 107);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(79, 26);
      this.cancelButton.TabIndex = 5;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // lblReplaceWith
      // 
      this.lblReplaceWith.AutoSize = true;
      this.lblReplaceWith.Location = new System.Drawing.Point(6, 52);
      this.lblReplaceWith.Name = "lblReplaceWith";
      this.lblReplaceWith.Size = new System.Drawing.Size(69, 13);
      this.lblReplaceWith.TabIndex = 41;
      this.lblReplaceWith.Text = "Replace with";
      // 
      // cmbReplaceHist
      // 
      this.cmbReplaceHist.Location = new System.Drawing.Point(9, 67);
      this.cmbReplaceHist.Name = "cmbReplaceHist";
      this.cmbReplaceHist.Size = new System.Drawing.Size(291, 21);
      this.cmbReplaceHist.TabIndex = 1;
      // 
      // replaceButton
      // 
      this.replaceButton.Location = new System.Drawing.Point(306, 29);
      this.replaceButton.Name = "replaceButton";
      this.replaceButton.Size = new System.Drawing.Size(79, 26);
      this.replaceButton.TabIndex = 2;
      this.replaceButton.Text = "&Replace";
      this.replaceButton.UseVisualStyleBackColor = true;
      this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
      // 
      // chkReplace
      // 
      this.chkReplace.AutoSize = true;
      this.chkReplace.Location = new System.Drawing.Point(304, 7);
      this.chkReplace.Name = "chkReplace";
      this.chkReplace.Size = new System.Drawing.Size(66, 17);
      this.chkReplace.TabIndex = 4;
      this.chkReplace.Text = "Replace";
      this.chkReplace.UseVisualStyleBackColor = true;
      this.chkReplace.CheckedChanged += new System.EventHandler(this.chkReplace_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 11);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(47, 13);
      this.label1.TabIndex = 34;
      this.label1.Text = "Find text";
      // 
      // cmbSearchHist
      // 
      this.cmbSearchHist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.cmbSearchHist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbSearchHist.Location = new System.Drawing.Point(9, 28);
      this.cmbSearchHist.Name = "cmbSearchHist";
      this.cmbSearchHist.Size = new System.Drawing.Size(291, 21);
      this.cmbSearchHist.TabIndex = 0;
      // 
      // searchButton
      // 
      this.searchButton.Location = new System.Drawing.Point(306, 26);
      this.searchButton.Name = "searchButton";
      this.searchButton.Size = new System.Drawing.Size(79, 26);
      this.searchButton.TabIndex = 2;
      this.searchButton.Text = "&Search";
      this.searchButton.UseVisualStyleBackColor = true;
      this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
      // 
      // SearchAndReplaceForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(398, 138);
      this.Controls.Add(this.panFind);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximumSize = new System.Drawing.Size(900, 220);
      this.MinimumSize = new System.Drawing.Size(356, 133);
      this.Name = "SearchAndReplaceForm";
      this.Opacity = 0.9D;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Search And Replace";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchAndReplaceForm_FormClosed);
      this.Load += new System.EventHandler(this.SearchAndReplaceForm_Load);
      this.panFind.ResumeLayout(false);
      this.panFind.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panFind;
    private System.Windows.Forms.CheckBox chkReplace;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button searchButton;
    internal System.Windows.Forms.ComboBox cmbSearchHist;
    private System.Windows.Forms.ComboBox cmbSearchType;
    private System.Windows.Forms.CheckBox chkIgnoreCase;
    private System.Windows.Forms.Button replaceAllButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label lblReplaceWith;
    internal System.Windows.Forms.ComboBox cmbReplaceHist;
    private System.Windows.Forms.Button replaceButton;
    private System.Windows.Forms.Panel panMax;
    private System.Windows.Forms.Panel panMin;


  }
}