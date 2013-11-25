namespace HtmlHelp2
{
  partial class HtmlHelp2IndexResultsPad
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlHelp2IndexResultsPad));
      this.listView = new System.Windows.Forms.ListView();
      this.title = new System.Windows.Forms.ColumnHeader();
      this.location = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // listView
      // 
      this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.title,
            this.location});
      this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView.FullRowSelect = true;
      this.listView.Location = new System.Drawing.Point(0, 0);
      this.listView.Name = "listView";
      this.listView.Size = new System.Drawing.Size(333, 610);
      this.listView.TabIndex = 1;
      this.listView.UseCompatibleStateImageBehavior = false;
      this.listView.View = System.Windows.Forms.View.Details;
      // 
      // title
      // 
      this.title.Text = "Title";
      this.title.Width = 169;
      // 
      // location
      // 
      this.location.Text = "Location";
      this.location.Width = 138;
      // 
      // HtmlHelp2IndexResultsPad
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(333, 610);
      this.Controls.Add(this.listView);
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "HtmlHelp2IndexResultsPad";
      this.ShowInTaskbar = false;
      this.TabText = "Index Results";
      this.Text = "Index Results";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView listView;
    private System.Windows.Forms.ColumnHeader title;
    private System.Windows.Forms.ColumnHeader location;
  }
}