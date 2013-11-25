namespace HtmlHelp2
{
  partial class HtmlHelp2TocPad
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
      if (help2TocControl != null)
      {
        help2TocControl.Dispose();
      }

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlHelp2TocPad));
      this.SuspendLayout();
      // 
      // HtmlHelp2TocPad2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(291, 575);
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "HtmlHelp2TocPad2";
      this.ShowInTaskbar = false;
      this.TabText = "Contents";
      this.Text = "Contents";
      this.ResumeLayout(false);

    }

    #endregion
  }
}