namespace PragmaSQL.Core
{
  partial class PwdDialog
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PwdDialog));
					this.edtPwd = new System.Windows.Forms.TextBox();
					this.lblInput = new System.Windows.Forms.Label();
					this.label1 = new System.Windows.Forms.Label();
					this.edtPwd2 = new System.Windows.Forms.TextBox();
					this.btnOK = new System.Windows.Forms.Button();
					this.btnCancel = new System.Windows.Forms.Button();
					this.SuspendLayout();
					// 
					// edtPwd
					// 
					this.edtPwd.Location = new System.Drawing.Point(107, 12);
					this.edtPwd.Name = "edtPwd";
					this.edtPwd.PasswordChar = '*';
					this.edtPwd.Size = new System.Drawing.Size(229, 20);
					this.edtPwd.TabIndex = 0;
					// 
					// lblInput
					// 
					this.lblInput.AutoSize = true;
					this.lblInput.Location = new System.Drawing.Point(10, 15);
					this.lblInput.Name = "lblInput";
					this.lblInput.Size = new System.Drawing.Size(53, 13);
					this.lblInput.TabIndex = 4;
					this.lblInput.Text = "Password";
					// 
					// label1
					// 
					this.label1.AutoSize = true;
					this.label1.Location = new System.Drawing.Point(10, 45);
					this.label1.Name = "label1";
					this.label1.Size = new System.Drawing.Size(91, 13);
					this.label1.TabIndex = 5;
					this.label1.Text = "Confirm Password";
					// 
					// edtPwd2
					// 
					this.edtPwd2.Location = new System.Drawing.Point(107, 42);
					this.edtPwd2.Name = "edtPwd2";
					this.edtPwd2.PasswordChar = '*';
					this.edtPwd2.Size = new System.Drawing.Size(229, 20);
					this.edtPwd2.TabIndex = 1;
					// 
					// btnOK
					// 
					this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
					this.btnOK.Location = new System.Drawing.Point(176, 76);
					this.btnOK.Name = "btnOK";
					this.btnOK.Size = new System.Drawing.Size(75, 25);
					this.btnOK.TabIndex = 2;
					this.btnOK.Text = "OK";
					this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
					// 
					// btnCancel
					// 
					this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
					this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.btnCancel.Location = new System.Drawing.Point(257, 76);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.Size = new System.Drawing.Size(75, 25);
					this.btnCancel.TabIndex = 3;
					this.btnCancel.Text = "Cancel";
					// 
					// PwdDialog
					// 
					this.AcceptButton = this.btnOK;
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.CancelButton = this.btnCancel;
					this.ClientSize = new System.Drawing.Size(346, 109);
					this.Controls.Add(this.btnOK);
					this.Controls.Add(this.btnCancel);
					this.Controls.Add(this.label1);
					this.Controls.Add(this.edtPwd2);
					this.Controls.Add(this.lblInput);
					this.Controls.Add(this.edtPwd);
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.MaximizeBox = false;
					this.MinimizeBox = false;
					this.Name = "PwdDialog";
					this.Opacity = 0.9;
					this.ShowInTaskbar = false;
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
					this.Text = "Set Password ";
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtPwd;
    private System.Windows.Forms.Label lblInput;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox edtPwd2;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;

    }
}