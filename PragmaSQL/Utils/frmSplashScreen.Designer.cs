namespace PragmaSQL
{
  partial class frmSplashScreen
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplashScreen));
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblEdition = new System.Windows.Forms.Label();
      this.lblVersion = new System.Windows.Forms.Label();
      this.lblStatusMsg = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.lblEdition);
      this.panel1.Controls.Add(this.lblVersion);
      this.panel1.Controls.Add(this.lblStatusMsg);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.pictureBox2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(406, 236);
      this.panel1.TabIndex = 0;
      this.panel1.UseWaitCursor = true;
      // 
      // lblEdition
      // 
      this.lblEdition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblEdition.AutoSize = true;
      this.lblEdition.BackColor = System.Drawing.Color.Transparent;
      this.lblEdition.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
      this.lblEdition.ForeColor = System.Drawing.Color.White;
      this.lblEdition.Location = new System.Drawing.Point(6, 157);
      this.lblEdition.Name = "lblEdition";
      this.lblEdition.Size = new System.Drawing.Size(0, 14);
      this.lblEdition.TabIndex = 45;
      this.lblEdition.UseWaitCursor = true;
      // 
      // lblVersion
      // 
      this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblVersion.AutoSize = true;
      this.lblVersion.BackColor = System.Drawing.Color.Transparent;
      this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.lblVersion.ForeColor = System.Drawing.Color.White;
      this.lblVersion.Location = new System.Drawing.Point(5, 176);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(69, 13);
      this.lblVersion.TabIndex = 45;
      this.lblVersion.Text = "Ver: 1.0.0.0";
      this.lblVersion.UseWaitCursor = true;
      // 
      // lblStatusMsg
      // 
      this.lblStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblStatusMsg.AutoSize = true;
      this.lblStatusMsg.BackColor = System.Drawing.Color.Transparent;
      this.lblStatusMsg.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.lblStatusMsg.ForeColor = System.Drawing.Color.White;
      this.lblStatusMsg.Location = new System.Drawing.Point(6, 213);
      this.lblStatusMsg.Name = "lblStatusMsg";
      this.lblStatusMsg.Size = new System.Drawing.Size(110, 13);
      this.lblStatusMsg.TabIndex = 44;
      this.lblStatusMsg.Text = "Loading assemblies...";
      this.lblStatusMsg.UseWaitCursor = true;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(5, 194);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(216, 13);
      this.label2.TabIndex = 43;
      this.label2.Text = "© 2007-2009 Ali Özgür. All rights reserved.";
      this.label2.UseWaitCursor = true;
      // 
      // pictureBox2
      // 
      this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox2.BackColor = System.Drawing.Color.White;
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(1, 1);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(402, 153);
      this.pictureBox2.TabIndex = 37;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.UseWaitCursor = true;
      // 
      // frmSplashScreen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(406, 236);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmSplashScreen";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "frmSplashScreen";
      this.TopMost = true;
      this.UseWaitCursor = true;
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblStatusMsg;
		private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblEdition;

  }
}