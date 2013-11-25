namespace SQLManagement
{
  partial class frmShrinkDb
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShrinkDb));
      this.btnClose = new System.Windows.Forms.Button();
      this.btnShrink = new System.Windows.Forms.Button();
      this.lblInfo = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbOption = new System.Windows.Forms.ComboBox();
      this.txtPrecent = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(231, 155);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 27);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnShrink
      // 
      this.btnShrink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnShrink.Location = new System.Drawing.Point(150, 155);
      this.btnShrink.Name = "btnShrink";
      this.btnShrink.Size = new System.Drawing.Size(75, 27);
      this.btnShrink.TabIndex = 1;
      this.btnShrink.Text = "Shrink";
      this.btnShrink.UseVisualStyleBackColor = true;
      this.btnShrink.Click += new System.EventHandler(this.btnShrink_Click);
      // 
      // lblInfo
      // 
      this.lblInfo.AutoSize = true;
      this.lblInfo.Location = new System.Drawing.Point(6, 8);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new System.Drawing.Size(63, 15);
      this.lblInfo.TabIndex = 2;
      this.lblInfo.Text = "Information";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 35);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(99, 15);
      this.label1.TabIndex = 3;
      this.label1.Text = "Target Percentage:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 80);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(88, 15);
      this.label2.TabIndex = 4;
      this.label2.Text = "Truncate Option:";
      // 
      // cmbOption
      // 
      this.cmbOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbOption.FormattingEnabled = true;
      this.cmbOption.Items.AddRange(new object[] {
            "NOTRUNCATE",
            "TRUNCATEONLY"});
      this.cmbOption.Location = new System.Drawing.Point(10, 98);
      this.cmbOption.Name = "cmbOption";
      this.cmbOption.Size = new System.Drawing.Size(146, 23);
      this.cmbOption.TabIndex = 5;
      // 
      // txtPrecent
      // 
      this.txtPrecent.Location = new System.Drawing.Point(10, 53);
      this.txtPrecent.MaxLength = 2;
      this.txtPrecent.Name = "txtPrecent";
      this.txtPrecent.Size = new System.Drawing.Size(103, 23);
      this.txtPrecent.TabIndex = 6;
      // 
      // frmShrinkDb
      // 
      this.AcceptButton = this.btnShrink;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(319, 191);
      this.Controls.Add(this.txtPrecent);
      this.Controls.Add(this.cmbOption);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lblInfo);
      this.Controls.Add(this.btnShrink);
      this.Controls.Add(this.btnClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmShrinkDb";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Shrink Database";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnShrink;
    private System.Windows.Forms.Label lblInfo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbOption;
    private System.Windows.Forms.TextBox txtPrecent;
  }
}