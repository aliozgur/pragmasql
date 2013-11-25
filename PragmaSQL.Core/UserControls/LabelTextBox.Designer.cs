namespace PragmaSQL.Core
{
  partial class LabelTextBox
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
      this.textBox = new System.Windows.Forms.TextBox();
      this.lbl = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textBox
      // 
      this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox.Location = new System.Drawing.Point(7, 20);
      this.textBox.Name = "textBox";
      this.textBox.Size = new System.Drawing.Size(261, 23);
      this.textBox.TabIndex = 2;
      // 
      // lbl
      // 
      this.lbl.AutoSize = true;
      this.lbl.Location = new System.Drawing.Point(4, 2);
      this.lbl.Name = "lbl";
      this.lbl.Size = new System.Drawing.Size(33, 15);
      this.lbl.TabIndex = 3;
      this.lbl.Text = "Label";
      // 
      // LabelTextBox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.lbl);
      this.Controls.Add(this.textBox);
      this.Name = "LabelTextBox";
      this.Size = new System.Drawing.Size(271, 46);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox;
    private System.Windows.Forms.Label lbl;

  }
}
