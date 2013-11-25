namespace PragmaSQL
{
  partial class ucGeneralSettings
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
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkShowWebBrowserOnStartup = new System.Windows.Forms.CheckBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnGoToUrl = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
      this.label4.Location = new System.Drawing.Point(2, 7);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(109, 18);
      this.label4.TabIndex = 12;
      this.label4.Text = "General Settings";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnGoToUrl);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.textBox1);
      this.groupBox1.Controls.Add(this.chkShowWebBrowserOnStartup);
      this.groupBox1.Location = new System.Drawing.Point(17, 36);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(452, 139);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Web Browser";
      // 
      // chkShowWebBrowserOnStartup
      // 
      this.chkShowWebBrowserOnStartup.AutoSize = true;
      this.chkShowWebBrowserOnStartup.Location = new System.Drawing.Point(20, 23);
      this.chkShowWebBrowserOnStartup.Name = "chkShowWebBrowserOnStartup";
      this.chkShowWebBrowserOnStartup.Size = new System.Drawing.Size(155, 19);
      this.chkShowWebBrowserOnStartup.TabIndex = 0;
      this.chkShowWebBrowserOnStartup.Text = "Show web browser on start";
      this.chkShowWebBrowserOnStartup.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(20, 67);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(354, 23);
      this.textBox1.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 49);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 15);
      this.label1.TabIndex = 2;
      this.label1.Text = "Home Url";
      // 
      // btnGoToUrl
      // 
      this.btnGoToUrl.Location = new System.Drawing.Point(378, 67);
      this.btnGoToUrl.Name = "btnGoToUrl";
      this.btnGoToUrl.Size = new System.Drawing.Size(60, 23);
      this.btnGoToUrl.TabIndex = 3;
      this.btnGoToUrl.Text = "Go";
      this.btnGoToUrl.UseVisualStyleBackColor = true;
      // 
      // ucGeneralSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label4);
      this.Name = "ucGeneralSettings";
      this.Size = new System.Drawing.Size(487, 385);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkShowWebBrowserOnStartup;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button btnGoToUrl;
    private System.Windows.Forms.Label label1;
  }
}
