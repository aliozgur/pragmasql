namespace PragmaSQL.Core
{
    partial class InputDialog
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
					System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDialog));
					this.edtInput = new System.Windows.Forms.TextBox();
					this.lblInput = new System.Windows.Forms.Label();
					this.panel1 = new System.Windows.Forms.Panel();
					this.btnOK = new System.Windows.Forms.Button();
					this.btnCancel = new System.Windows.Forms.Button();
					this.panel1.SuspendLayout();
					this.SuspendLayout();
					// 
					// edtInput
					// 
					this.edtInput.Location = new System.Drawing.Point(9, 25);
					this.edtInput.Name = "edtInput";
					this.edtInput.Size = new System.Drawing.Size(420, 20);
					this.edtInput.TabIndex = 0;
					// 
					// lblInput
					// 
					this.lblInput.AutoSize = true;
					this.lblInput.Location = new System.Drawing.Point(8, 5);
					this.lblInput.Name = "lblInput";
					this.lblInput.Size = new System.Drawing.Size(0, 13);
					this.lblInput.TabIndex = 3;
					// 
					// panel1
					// 
					this.panel1.Controls.Add(this.btnOK);
					this.panel1.Controls.Add(this.btnCancel);
					this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
					this.panel1.Location = new System.Drawing.Point(0, 61);
					this.panel1.Name = "panel1";
					this.panel1.Size = new System.Drawing.Size(441, 36);
					this.panel1.TabIndex = 4;
					// 
					// btnOK
					// 
					this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
					this.btnOK.Location = new System.Drawing.Point(273, 7);
					this.btnOK.Name = "btnOK";
					this.btnOK.Size = new System.Drawing.Size(75, 23);
					this.btnOK.TabIndex = 4;
					this.btnOK.Text = "OK";
					this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
					// 
					// btnCancel
					// 
					this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.btnCancel.Location = new System.Drawing.Point(354, 7);
					this.btnCancel.Name = "btnCancel";
					this.btnCancel.Size = new System.Drawing.Size(75, 23);
					this.btnCancel.TabIndex = 3;
					this.btnCancel.Text = "Cancel";
					// 
					// InputDialog
					// 
					this.AcceptButton = this.btnOK;
					this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
					this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
					this.CancelButton = this.btnCancel;
					this.ClientSize = new System.Drawing.Size(441, 97);
					this.Controls.Add(this.panel1);
					this.Controls.Add(this.lblInput);
					this.Controls.Add(this.edtInput);
					this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
					this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
					this.MaximizeBox = false;
					this.MinimizeBox = false;
					this.Name = "InputDialog";
					this.Opacity = 0.9;
					this.ShowInTaskbar = false;
					this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
					this.Text = "Input Dialog";
					this.panel1.ResumeLayout(false);
					this.ResumeLayout(false);
					this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtInput;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

    }
}