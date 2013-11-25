namespace PragmaSQL.Core
{
  partial class GenericErrorDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericErrorDialog));
			this.btnClose = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblShortDescription = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtLongDescription = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(445, 270);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(78, 26);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.lblShortDescription);
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(515, 34);
			this.panel1.TabIndex = 1;
			// 
			// lblShortDescription
			// 
			this.lblShortDescription.AutoSize = true;
			this.lblShortDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblShortDescription.ForeColor = System.Drawing.Color.Red;
			this.lblShortDescription.Location = new System.Drawing.Point(7, 5);
			this.lblShortDescription.Name = "lblShortDescription";
			this.lblShortDescription.Size = new System.Drawing.Size(71, 13);
			this.lblShortDescription.TabIndex = 1;
			this.lblShortDescription.Text = "Description";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.txtLongDescription);
			this.panel2.Location = new System.Drawing.Point(4, 41);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(515, 221);
			this.panel2.TabIndex = 2;
			// 
			// txtLongDescription
			// 
			this.txtLongDescription.AcceptsReturn = true;
			this.txtLongDescription.AcceptsTab = true;
			this.txtLongDescription.BackColor = System.Drawing.Color.White;
			this.txtLongDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLongDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.txtLongDescription.ForeColor = System.Drawing.Color.Red;
			this.txtLongDescription.Location = new System.Drawing.Point(0, 0);
			this.txtLongDescription.Multiline = true;
			this.txtLongDescription.Name = "txtLongDescription";
			this.txtLongDescription.ReadOnly = true;
			this.txtLongDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLongDescription.Size = new System.Drawing.Size(515, 221);
			this.txtLongDescription.TabIndex = 0;
			this.txtLongDescription.WordWrap = false;
			// 
			// GenericErrorDialog
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(534, 300);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GenericErrorDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Generic Error";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.TextBox txtLongDescription;
    private System.Windows.Forms.Label lblShortDescription;
  }
}