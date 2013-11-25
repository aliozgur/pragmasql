namespace PragmaSQL
{
  partial class frmRecoverContent
  {

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
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
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecoverContent));
      this.lv = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
      this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
      this.btnRecover = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.pb = new System.Windows.Forms.ProgressBar();
      this.pnlProgress = new System.Windows.Forms.Panel();
      this.lblProgress = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.pnlProgress.SuspendLayout();
      this.SuspendLayout();
      // 
      // lv
      // 
      this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lv.CheckBoxes = true;
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader4});
      this.lv.Location = new System.Drawing.Point(8, 48);
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(537, 317);
      this.lv.TabIndex = 0;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      this.lv.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lv_ItemChecked);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Title";
      this.columnHeader1.Width = 137;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Type";
      this.columnHeader2.Width = 67;
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Db";
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "FilePath";
      this.columnHeader3.Width = 100;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "DateTime";
      this.columnHeader4.Width = 112;
      // 
      // btnRecover
      // 
      this.btnRecover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRecover.Location = new System.Drawing.Point(383, 392);
      this.btnRecover.Name = "btnRecover";
      this.btnRecover.Size = new System.Drawing.Size(75, 25);
      this.btnRecover.TabIndex = 1;
      this.btnRecover.Text = "Restore";
      this.btnRecover.UseVisualStyleBackColor = true;
      this.btnRecover.Click += new System.EventHandler(this.btnRecover_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(464, 392);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 25);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.Location = new System.Drawing.Point(40, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(502, 34);
      this.label1.TabIndex = 2;
      this.label1.Text = "You see this dialog because previous PragmaSQL shut down was unexpected.\r\nPlease " +
          "select content items you want to be restored.";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(6, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(32, 32);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      // 
      // pb
      // 
      this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.pb.Location = new System.Drawing.Point(7, 21);
      this.pb.Name = "pb";
      this.pb.Size = new System.Drawing.Size(354, 13);
      this.pb.TabIndex = 4;
      // 
      // pnlProgress
      // 
      this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlProgress.Controls.Add(this.lblProgress);
      this.pnlProgress.Controls.Add(this.pb);
      this.pnlProgress.Location = new System.Drawing.Point(0, 378);
      this.pnlProgress.Name = "pnlProgress";
      this.pnlProgress.Size = new System.Drawing.Size(369, 39);
      this.pnlProgress.TabIndex = 5;
      this.pnlProgress.Visible = false;
      // 
      // lblProgress
      // 
      this.lblProgress.AutoSize = true;
      this.lblProgress.Location = new System.Drawing.Point(5, 4);
      this.lblProgress.Name = "lblProgress";
      this.lblProgress.Size = new System.Drawing.Size(60, 13);
      this.lblProgress.TabIndex = 5;
      this.lblProgress.Text = "Progress....";
      // 
      // frmRecoverContent
      // 
      this.AcceptButton = this.btnRecover;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(551, 427);
      this.Controls.Add(this.pnlProgress);
      this.Controls.Add(this.lv);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnRecover);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmRecoverContent";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PragmaSQL - Restore Content";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.pnlProgress.ResumeLayout(false);
      this.pnlProgress.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.Button btnRecover;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ProgressBar pb;
    private System.Windows.Forms.Panel pnlProgress;
    private System.Windows.Forms.Label lblProgress;
  }
}

