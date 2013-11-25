using System;
using System.Windows.Forms;

namespace HtmlHelp2
{
  partial class HtmlHelp2SearchResultsView
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
      this.listView = new System.Windows.Forms.ListView();
      this.title = new System.Windows.Forms.ColumnHeader();
      this.location = new System.Windows.Forms.ColumnHeader();
      this.rank = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // listView
      // 
      this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.title,
            this.location,
            this.rank});
      this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView.FullRowSelect = true;
      this.listView.Location = new System.Drawing.Point(0, 0);
      this.listView.Name = "listView";
      this.listView.Size = new System.Drawing.Size(382, 576);
      this.listView.TabIndex = 2;
      this.listView.UseCompatibleStateImageBehavior = false;
      this.listView.View = System.Windows.Forms.View.Details;
      this.listView.DoubleClick += new System.EventHandler(this.ListViewDoubleClick);
      this.listView.Resize += new System.EventHandler(this.ListViewResize);
      this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ColumnClick);
      // 
      // title
      // 
      this.title.Text = "Title";
      this.title.Width = 152;
      // 
      // location
      // 
      this.location.Text = "Location";
      this.location.Width = 120;
      // 
      // rank
      // 
      this.rank.Text = "Rank";
      this.rank.Width = 77;
      // 
      // HtmlHelp2SearchResultsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.listView);
      this.Name = "HtmlHelp2SearchResultsView";
      this.Size = new System.Drawing.Size(382, 576);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView listView;
    private System.Windows.Forms.ColumnHeader title;
    private System.Windows.Forms.ColumnHeader location;
    private System.Windows.Forms.ColumnHeader rank;
  }
}
