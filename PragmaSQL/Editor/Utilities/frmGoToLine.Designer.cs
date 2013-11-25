namespace PragmaSQL
{
  partial class frmGoToLine
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.btnGo = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(8, 27);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(243, 23);
      this.textBox1.TabIndex = 0;
      this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(110, 61);
      this.btnGo.Name = "btnGo";
      this.btnGo.Size = new System.Drawing.Size(66, 28);
      this.btnGo.TabIndex = 1;
      this.btnGo.Text = "Go";
      this.btnGo.UseVisualStyleBackColor = true;
      this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
      // 
      // btnClose
      // 
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(184, 61);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(66, 28);
      this.btnClose.TabIndex = 2;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(5, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 15);
      this.label1.TabIndex = 3;
      this.label1.Text = "Line (Min-Max)";
      // 
      // frmGoToLine
      // 
      this.AcceptButton = this.btnGo;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(263, 101);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnGo);
      this.Controls.Add(this.textBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "frmGoToLine";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Go To Line";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button btnGo;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Label label1;
  }
}