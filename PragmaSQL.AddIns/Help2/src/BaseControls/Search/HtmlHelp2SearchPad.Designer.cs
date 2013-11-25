using System;
using System.Windows.Forms;

namespace HtmlHelp2
{
  partial class HtmlHelp2SearchPad
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlHelp2SearchPad));
			this.label1 = new System.Windows.Forms.Label();
			this.searchTerm = new System.Windows.Forms.ComboBox();
			this.filterCombobox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.searchButton = new System.Windows.Forms.Button();
			this.titlesOnly = new System.Windows.Forms.CheckBox();
			this.enableStemming = new System.Windows.Forms.CheckBox();
			this.reuseMatches = new System.Windows.Forms.CheckBox();
			this.hiliteTopics = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Look for:";
			// 
			// searchTerm
			// 
			this.searchTerm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.searchTerm.FormattingEnabled = true;
			this.searchTerm.Location = new System.Drawing.Point(9, 28);
			this.searchTerm.Name = "searchTerm";
			this.searchTerm.Size = new System.Drawing.Size(242, 21);
			this.searchTerm.TabIndex = 1;
			this.searchTerm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressed);
			this.searchTerm.TextChanged += new System.EventHandler(this.SearchTextChanged);
			// 
			// filterCombobox
			// 
			this.filterCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.filterCombobox.FormattingEnabled = true;
			this.filterCombobox.Location = new System.Drawing.Point(9, 69);
			this.filterCombobox.Name = "filterCombobox";
			this.filterCombobox.Size = new System.Drawing.Size(242, 21);
			this.filterCombobox.TabIndex = 3;
			this.filterCombobox.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Filtered by:";
			// 
			// searchButton
			// 
			this.searchButton.Location = new System.Drawing.Point(9, 95);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(75, 22);
			this.searchButton.TabIndex = 4;
			this.searchButton.Text = "Search";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.SearchButtonClick);
			// 
			// titlesOnly
			// 
			this.titlesOnly.AutoSize = true;
			this.titlesOnly.Location = new System.Drawing.Point(9, 130);
			this.titlesOnly.Name = "titlesOnly";
			this.titlesOnly.Size = new System.Drawing.Size(117, 17);
			this.titlesOnly.TabIndex = 5;
			this.titlesOnly.Text = "Search in titles only";
			this.titlesOnly.UseVisualStyleBackColor = true;
			// 
			// enableStemming
			// 
			this.enableStemming.AutoSize = true;
			this.enableStemming.Location = new System.Drawing.Point(9, 149);
			this.enableStemming.Name = "enableStemming";
			this.enableStemming.Size = new System.Drawing.Size(127, 17);
			this.enableStemming.TabIndex = 6;
			this.enableStemming.Text = "Look for similar words";
			this.enableStemming.UseVisualStyleBackColor = true;
			// 
			// reuseMatches
			// 
			this.reuseMatches.AutoSize = true;
			this.reuseMatches.Location = new System.Drawing.Point(9, 171);
			this.reuseMatches.Name = "reuseMatches";
			this.reuseMatches.Size = new System.Drawing.Size(182, 17);
			this.reuseMatches.TabIndex = 7;
			this.reuseMatches.Text = "Search in previously found topics";
			this.reuseMatches.UseVisualStyleBackColor = true;
			// 
			// hiliteTopics
			// 
			this.hiliteTopics.AutoSize = true;
			this.hiliteTopics.Location = new System.Drawing.Point(9, 192);
			this.hiliteTopics.Name = "hiliteTopics";
			this.hiliteTopics.Size = new System.Drawing.Size(110, 17);
			this.hiliteTopics.TabIndex = 8;
			this.hiliteTopics.Text = "Highlight matches";
			this.hiliteTopics.UseVisualStyleBackColor = true;
			// 
			// HtmlHelp2SearchPad
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(263, 604);
			this.Controls.Add(this.hiliteTopics);
			this.Controls.Add(this.reuseMatches);
			this.Controls.Add(this.enableStemming);
			this.Controls.Add(this.titlesOnly);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.filterCombobox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.searchTerm);
			this.Controls.Add(this.label1);
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "HtmlHelp2SearchPad";
			this.ShowInTaskbar = false;
			this.TabText = "Search";
			this.Text = "Search";
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox searchTerm;
    private System.Windows.Forms.ComboBox filterCombobox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button searchButton;
    private System.Windows.Forms.CheckBox titlesOnly;
    private System.Windows.Forms.CheckBox enableStemming;
    private System.Windows.Forms.CheckBox reuseMatches;
    private System.Windows.Forms.CheckBox hiliteTopics;
  }
}