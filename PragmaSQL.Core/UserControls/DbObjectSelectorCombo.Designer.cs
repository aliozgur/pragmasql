namespace PragmaSQL.Core
{
  partial class DbObjectSelectorCombo
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
      this.label1 = new System.Windows.Forms.Label();
      this.cmb = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(-1, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(43, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Objects";
      // 
      // cmb
      // 
      this.cmb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmb.FormattingEnabled = true;
      this.cmb.Location = new System.Drawing.Point(2, 19);
      this.cmb.Name = "cmb";
      this.cmb.Size = new System.Drawing.Size(305, 23);
      this.cmb.TabIndex = 1;
      // 
      // DbObjectSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.cmb);
      this.Controls.Add(this.label1);
      this.Name = "DbObjectSelector";
      this.Size = new System.Drawing.Size(308, 43);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmb;
  }
}
