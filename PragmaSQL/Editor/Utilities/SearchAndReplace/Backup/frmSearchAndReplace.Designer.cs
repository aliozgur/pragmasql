namespace PragmaSQL
{
  partial class frmSearchAndReplace
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
      this.panOptions = new System.Windows.Forms.Panel();
      this.searchTypeComboBox = new System.Windows.Forms.ComboBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
      this.panReplace = new System.Windows.Forms.Panel();
      this.replaceAllButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.replaceHistoryComboBox = new System.Windows.Forms.ComboBox();
      this.replaceButton = new System.Windows.Forms.Button();
      this.panFind = new System.Windows.Forms.Panel();
      this.chkReplace = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.searchHistoryComboBox = new System.Windows.Forms.ComboBox();
      this.searchButton = new System.Windows.Forms.Button();
      this.panOptions.SuspendLayout();
      this.panReplace.SuspendLayout();
      this.panFind.SuspendLayout();
      this.SuspendLayout();
      // 
      // panOptions
      // 
      this.panOptions.Controls.Add(this.searchTypeComboBox);
      this.panOptions.Controls.Add(this.cancelButton);
      this.panOptions.Controls.Add(this.chkIgnoreCase);
      this.panOptions.Dock = System.Windows.Forms.DockStyle.Top;
      this.panOptions.Location = new System.Drawing.Point(0, 149);
      this.panOptions.Name = "panOptions";
      this.panOptions.Size = new System.Drawing.Size(399, 48);
      this.panOptions.TabIndex = 2;
      // 
      // searchTypeComboBox
      // 
      this.searchTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.searchTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.searchTypeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.searchTypeComboBox.FormattingEnabled = true;
      this.searchTypeComboBox.Location = new System.Drawing.Point(93, 14);
      this.searchTypeComboBox.Name = "searchTypeComboBox";
      this.searchTypeComboBox.Size = new System.Drawing.Size(136, 23);
      this.searchTypeComboBox.TabIndex = 1;
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(306, 10);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(79, 30);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // chkIgnoreCase
      // 
      this.chkIgnoreCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkIgnoreCase.AutoSize = true;
      this.chkIgnoreCase.Checked = true;
      this.chkIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkIgnoreCase.Location = new System.Drawing.Point(9, 17);
      this.chkIgnoreCase.Name = "chkIgnoreCase";
      this.chkIgnoreCase.Size = new System.Drawing.Size(81, 19);
      this.chkIgnoreCase.TabIndex = 0;
      this.chkIgnoreCase.Text = "Ignore &case";
      this.chkIgnoreCase.UseVisualStyleBackColor = true;
      // 
      // panReplace
      // 
      this.panReplace.Controls.Add(this.replaceAllButton);
      this.panReplace.Controls.Add(this.label2);
      this.panReplace.Controls.Add(this.replaceHistoryComboBox);
      this.panReplace.Controls.Add(this.replaceButton);
      this.panReplace.Dock = System.Windows.Forms.DockStyle.Top;
      this.panReplace.Location = new System.Drawing.Point(0, 68);
      this.panReplace.Name = "panReplace";
      this.panReplace.Size = new System.Drawing.Size(399, 81);
      this.panReplace.TabIndex = 1;
      // 
      // replaceAllButton
      // 
      this.replaceAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.replaceAllButton.Location = new System.Drawing.Point(306, 44);
      this.replaceAllButton.Name = "replaceAllButton";
      this.replaceAllButton.Size = new System.Drawing.Size(79, 30);
      this.replaceAllButton.TabIndex = 2;
      this.replaceAllButton.Text = "Replace &All";
      this.replaceAllButton.UseVisualStyleBackColor = true;
      this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 10);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(69, 15);
      this.label2.TabIndex = 33;
      this.label2.Text = "Replace with";
      // 
      // replaceHistoryComboBox
      // 
      this.replaceHistoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.replaceHistoryComboBox.FormattingEnabled = true;
      this.replaceHistoryComboBox.Location = new System.Drawing.Point(9, 28);
      this.replaceHistoryComboBox.Name = "replaceHistoryComboBox";
      this.replaceHistoryComboBox.Size = new System.Drawing.Size(291, 23);
      this.replaceHistoryComboBox.TabIndex = 0;
      // 
      // replaceButton
      // 
      this.replaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.replaceButton.Location = new System.Drawing.Point(306, 8);
      this.replaceButton.Name = "replaceButton";
      this.replaceButton.Size = new System.Drawing.Size(79, 30);
      this.replaceButton.TabIndex = 1;
      this.replaceButton.Text = "&Replace";
      this.replaceButton.UseVisualStyleBackColor = true;
      this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
      // 
      // panFind
      // 
      this.panFind.Controls.Add(this.chkReplace);
      this.panFind.Controls.Add(this.label1);
      this.panFind.Controls.Add(this.searchHistoryComboBox);
      this.panFind.Controls.Add(this.searchButton);
      this.panFind.Dock = System.Windows.Forms.DockStyle.Top;
      this.panFind.Location = new System.Drawing.Point(0, 0);
      this.panFind.Name = "panFind";
      this.panFind.Size = new System.Drawing.Size(399, 68);
      this.panFind.TabIndex = 0;
      // 
      // chkReplace
      // 
      this.chkReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkReplace.AutoSize = true;
      this.chkReplace.Location = new System.Drawing.Point(306, 8);
      this.chkReplace.Name = "chkReplace";
      this.chkReplace.Size = new System.Drawing.Size(64, 19);
      this.chkReplace.TabIndex = 2;
      this.chkReplace.Text = "Replace";
      this.chkReplace.UseVisualStyleBackColor = true;
      this.chkReplace.CheckedChanged += new System.EventHandler(this.chkReplace_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(49, 15);
      this.label1.TabIndex = 34;
      this.label1.Text = "Find text";
      // 
      // searchHistoryComboBox
      // 
      this.searchHistoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.searchHistoryComboBox.FormattingEnabled = true;
      this.searchHistoryComboBox.Location = new System.Drawing.Point(9, 32);
      this.searchHistoryComboBox.Name = "searchHistoryComboBox";
      this.searchHistoryComboBox.Size = new System.Drawing.Size(291, 23);
      this.searchHistoryComboBox.TabIndex = 0;
      // 
      // searchButton
      // 
      this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.searchButton.Location = new System.Drawing.Point(306, 30);
      this.searchButton.Name = "searchButton";
      this.searchButton.Size = new System.Drawing.Size(79, 30);
      this.searchButton.TabIndex = 1;
      this.searchButton.Text = "&Search";
      this.searchButton.UseVisualStyleBackColor = true;
      this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
      // 
      // frmSearchAndReplace
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(399, 198);
      this.Controls.Add(this.panOptions);
      this.Controls.Add(this.panReplace);
      this.Controls.Add(this.panFind);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MaximumSize = new System.Drawing.Size(900, 250);
      this.MinimumSize = new System.Drawing.Size(356, 149);
      this.Name = "frmSearchAndReplace";
      this.Opacity = 0.9;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Search And Replace";
      this.panOptions.ResumeLayout(false);
      this.panOptions.PerformLayout();
      this.panReplace.ResumeLayout(false);
      this.panReplace.PerformLayout();
      this.panFind.ResumeLayout(false);
      this.panFind.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panOptions;
    private System.Windows.Forms.ComboBox searchTypeComboBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.CheckBox chkIgnoreCase;
    private System.Windows.Forms.Panel panReplace;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button replaceButton;
    private System.Windows.Forms.Panel panFind;
    private System.Windows.Forms.CheckBox chkReplace;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button searchButton;
    private System.Windows.Forms.Button replaceAllButton;
    internal System.Windows.Forms.ComboBox replaceHistoryComboBox;
    internal System.Windows.Forms.ComboBox searchHistoryComboBox;


  }
}