namespace PragmaSQL.Svn.Gui
{
  partial class InfoPanel
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.label1 = new System.Windows.Forms.Label();
      this.revisionList = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.commentRichTextBox = new System.Windows.Forms.RichTextBox();
      this.changesList = new System.Windows.Forms.ListView();
      this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.label1);
      this.splitContainer1.Panel1.Controls.Add(this.revisionList);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.label3);
      this.splitContainer1.Panel2.Controls.Add(this.label2);
      this.splitContainer1.Panel2.Controls.Add(this.commentRichTextBox);
      this.splitContainer1.Panel2.Controls.Add(this.changesList);
      this.splitContainer1.Size = new System.Drawing.Size(627, 488);
      this.splitContainer1.SplitterDistance = 212;
      this.splitContainer1.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(5, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 15);
      this.label1.TabIndex = 10;
      this.label1.Text = "Revision history:";
      // 
      // revisionList
      // 
      this.revisionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.revisionList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.revisionList.FullRowSelect = true;
      this.revisionList.GridLines = true;
      this.revisionList.HideSelection = false;
      this.revisionList.Location = new System.Drawing.Point(8, 31);
      this.revisionList.MultiSelect = false;
      this.revisionList.Name = "revisionList";
      this.revisionList.Size = new System.Drawing.Size(604, 172);
      this.revisionList.TabIndex = 9;
      this.revisionList.UseCompatibleStateImageBehavior = false;
      this.revisionList.View = System.Windows.Forms.View.Details;
      this.revisionList.SelectedIndexChanged += new System.EventHandler(this.RevisionListViewSelectionChanged);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Revision";
      this.columnHeader1.Width = 80;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Author";
      this.columnHeader2.Width = 114;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "Date";
      this.columnHeader3.Width = 91;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Comment";
      this.columnHeader4.Width = 241;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(8, 7);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 15);
      this.label3.TabIndex = 13;
      this.label3.Text = "Comments:";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 133);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(50, 15);
      this.label2.TabIndex = 12;
      this.label2.Text = "Changes:";
      // 
      // commentRichTextBox
      // 
      this.commentRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.commentRichTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.commentRichTextBox.Location = new System.Drawing.Point(9, 24);
      this.commentRichTextBox.Name = "commentRichTextBox";
      this.commentRichTextBox.ReadOnly = true;
      this.commentRichTextBox.Size = new System.Drawing.Size(603, 106);
      this.commentRichTextBox.TabIndex = 11;
      this.commentRichTextBox.Text = "";
      this.commentRichTextBox.WordWrap = false;
      // 
      // changesList
      // 
      this.changesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.changesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
      this.changesList.FullRowSelect = true;
      this.changesList.GridLines = true;
      this.changesList.HideSelection = false;
      this.changesList.Location = new System.Drawing.Point(8, 151);
      this.changesList.MultiSelect = false;
      this.changesList.Name = "changesList";
      this.changesList.Size = new System.Drawing.Size(604, 115);
      this.changesList.TabIndex = 10;
      this.changesList.UseCompatibleStateImageBehavior = false;
      this.changesList.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Action";
      this.columnHeader5.Width = 80;
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "Path";
      this.columnHeader6.Width = 114;
      // 
      // columnHeader7
      // 
      this.columnHeader7.Text = "Copy From";
      this.columnHeader7.Width = 91;
      // 
      // InfoPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "InfoPanel";
      this.Size = new System.Drawing.Size(627, 488);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ListView revisionList;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ListView changesList;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ColumnHeader columnHeader7;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.RichTextBox commentRichTextBox;
  }
}
