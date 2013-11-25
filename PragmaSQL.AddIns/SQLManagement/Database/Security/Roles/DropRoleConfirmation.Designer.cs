namespace SQLManagement
{
  partial class DropRoleConfirmation
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DropRoleConfirmation));
      this.label1 = new System.Windows.Forms.Label();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.btnNo = new System.Windows.Forms.Button();
      this.btnYes = new System.Windows.Forms.Button();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(51, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(226, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Are you sure you want to drop selected roles?";
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(52, 32);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(96, 19);
      this.checkBox1.TabIndex = 1;
      this.checkBox1.Text = "Remove users?";
      this.checkBox1.UseVisualStyleBackColor = true;
      // 
      // btnNo
      // 
      this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
      this.btnNo.Location = new System.Drawing.Point(215, 75);
      this.btnNo.Name = "btnNo";
      this.btnNo.Size = new System.Drawing.Size(74, 27);
      this.btnNo.TabIndex = 2;
      this.btnNo.Text = "No";
      this.btnNo.UseVisualStyleBackColor = true;
      this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
      // 
      // btnYes
      // 
      this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
      this.btnYes.Location = new System.Drawing.Point(134, 75);
      this.btnYes.Name = "btnYes";
      this.btnYes.Size = new System.Drawing.Size(74, 27);
      this.btnYes.TabIndex = 3;
      this.btnYes.Text = "Yes";
      this.btnYes.UseVisualStyleBackColor = true;
      this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(6, 8);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(32, 32);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      // 
      // DropRoleConfirmation
      // 
      this.AcceptButton = this.btnYes;
      this.CancelButton = this.btnNo;
      this.ClientSize = new System.Drawing.Size(303, 114);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.btnYes);
      this.Controls.Add(this.btnNo);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DropRoleConfirmation";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Confirm Drop Role";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.Button btnNo;
    private System.Windows.Forms.Button btnYes;
    private System.Windows.Forms.PictureBox pictureBox1;
  }
}