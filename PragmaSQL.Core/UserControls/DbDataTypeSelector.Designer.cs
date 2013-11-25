namespace PragmaSQL.Core
{
  partial class DbDataTypeSelector
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
      this.cmbType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtWidth = new System.Windows.Forms.MaskedTextBox();
      this.txtScale = new System.Windows.Forms.MaskedTextBox();
      this.txtPrecision = new System.Windows.Forms.MaskedTextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(-2, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Data Type";
      // 
      // cmbType
      // 
      this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbType.FormattingEnabled = true;
      this.cmbType.Location = new System.Drawing.Point(1, 23);
      this.cmbType.Name = "cmbType";
      this.cmbType.Size = new System.Drawing.Size(146, 23);
      this.cmbType.TabIndex = 1;
      this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(153, 4);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(36, 15);
      this.label2.TabIndex = 2;
      this.label2.Text = "Width";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(269, 4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(32, 15);
      this.label3.TabIndex = 4;
      this.label3.Text = "Scale";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(211, 4);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(50, 15);
      this.label4.TabIndex = 6;
      this.label4.Text = "Precision";
      // 
      // txtWidth
      // 
      this.txtWidth.Location = new System.Drawing.Point(155, 23);
      this.txtWidth.Mask = "0000";
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(52, 23);
      this.txtWidth.TabIndex = 8;
      // 
      // txtScale
      // 
      this.txtScale.Location = new System.Drawing.Point(271, 23);
      this.txtScale.Mask = "0000";
      this.txtScale.Name = "txtScale";
      this.txtScale.Size = new System.Drawing.Size(52, 23);
      this.txtScale.TabIndex = 9;
      // 
      // txtPrecision
      // 
      this.txtPrecision.Location = new System.Drawing.Point(213, 23);
      this.txtPrecision.Mask = "0000";
      this.txtPrecision.Name = "txtPrecision";
      this.txtPrecision.Size = new System.Drawing.Size(52, 23);
      this.txtPrecision.TabIndex = 10;
      // 
      // DbDataTypeSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtPrecision);
      this.Controls.Add(this.txtScale);
      this.Controls.Add(this.txtWidth);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cmbType);
      this.Controls.Add(this.label1);
      this.Name = "DbDataTypeSelector";
      this.Size = new System.Drawing.Size(323, 47);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbType;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.MaskedTextBox txtWidth;
    private System.Windows.Forms.MaskedTextBox txtScale;
    private System.Windows.Forms.MaskedTextBox txtPrecision;
  }
}
