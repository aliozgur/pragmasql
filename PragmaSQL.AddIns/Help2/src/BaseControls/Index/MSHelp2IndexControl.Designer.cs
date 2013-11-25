namespace HtmlHelp2
{
  partial class MSHelp2IndexControl
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
      if (indexControl != null)
      {
        indexControl.Dispose();
      }

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
      this.filterCombobox = new System.Windows.Forms.ComboBox();
      this.searchTerm = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.panIndexControl = new System.Windows.Forms.Panel();
      this.infoLabel = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.infoLabel);
      this.panel1.Controls.Add(this.filterCombobox);
      this.panel1.Controls.Add(this.searchTerm);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(386, 123);
      this.panel1.TabIndex = 0;
      // 
      // filterCombobox
      // 
      this.filterCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.filterCombobox.FormattingEnabled = true;
      this.filterCombobox.Location = new System.Drawing.Point(8, 72);
      this.filterCombobox.Name = "filterCombobox";
      this.filterCombobox.Size = new System.Drawing.Size(366, 23);
      this.filterCombobox.TabIndex = 3;
      this.filterCombobox.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
      // 
      // searchTerm
      // 
      this.searchTerm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.searchTerm.FormattingEnabled = true;
      this.searchTerm.Location = new System.Drawing.Point(8, 24);
      this.searchTerm.Name = "searchTerm";
      this.searchTerm.Size = new System.Drawing.Size(366, 23);
      this.searchTerm.TabIndex = 2;
      this.searchTerm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchKeyPress);
      this.searchTerm.TextChanged += new System.EventHandler(this.SearchTextChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 54);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(46, 15);
      this.label2.TabIndex = 1;
      this.label2.Text = "Filter By";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 5);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(47, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Look For";
      // 
      // panIndexControl
      // 
      this.panIndexControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panIndexControl.Location = new System.Drawing.Point(0, 123);
      this.panIndexControl.Name = "panIndexControl";
      this.panIndexControl.Size = new System.Drawing.Size(386, 574);
      this.panIndexControl.TabIndex = 1;
      // 
      // infoLabel
      // 
      this.infoLabel.AutoSize = true;
      this.infoLabel.Location = new System.Drawing.Point(8, 102);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(0, 15);
      this.infoLabel.TabIndex = 4;
      // 
      // MSHelp2IndexControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panIndexControl);
      this.Controls.Add(this.panel1);
      this.Name = "MSHelp2IndexControl";
      this.Size = new System.Drawing.Size(386, 697);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panIndexControl;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox filterCombobox;
    private System.Windows.Forms.ComboBox searchTerm;
    private System.Windows.Forms.Label infoLabel;
  }
}
