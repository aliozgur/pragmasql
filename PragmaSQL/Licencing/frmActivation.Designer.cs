namespace PragmaSQL
{
  partial class frmActivation
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActivation));
			this.cmbCodeName = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtActivationKey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtMachineKey = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbPurchaseType = new System.Windows.Forms.ComboBox();
			this.btnActivate = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtEMail = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmbCodeName
			// 
			this.cmbCodeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCodeName.FormattingEnabled = true;
			this.cmbCodeName.Location = new System.Drawing.Point(12, 256);
			this.cmbCodeName.Name = "cmbCodeName";
			this.cmbCodeName.Size = new System.Drawing.Size(143, 21);
			this.cmbCodeName.TabIndex = 0;
			this.cmbCodeName.TabStop = false;
			this.cmbCodeName.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 240);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "PragmaSQL Code Name";
			this.label1.Visible = false;
			// 
			// txtActivationKey
			// 
			this.txtActivationKey.Location = new System.Drawing.Point(101, 42);
			this.txtActivationKey.Name = "txtActivationKey";
			this.txtActivationKey.Size = new System.Drawing.Size(285, 20);
			this.txtActivationKey.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Activation Key";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Machine Key";
			// 
			// txtMachineKey
			// 
			this.txtMachineKey.Location = new System.Drawing.Point(101, 68);
			this.txtMachineKey.Name = "txtMachineKey";
			this.txtMachineKey.ReadOnly = true;
			this.txtMachineKey.Size = new System.Drawing.Size(285, 20);
			this.txtMachineKey.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "License Type";
			// 
			// cmbPurchaseType
			// 
			this.cmbPurchaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPurchaseType.FormattingEnabled = true;
			this.cmbPurchaseType.Location = new System.Drawing.Point(101, 15);
			this.cmbPurchaseType.Name = "cmbPurchaseType";
			this.cmbPurchaseType.Size = new System.Drawing.Size(143, 21);
			this.cmbPurchaseType.TabIndex = 0;
			this.cmbPurchaseType.SelectedIndexChanged += new System.EventHandler(this.cmbPurchaseType_SelectedIndexChanged);
			// 
			// btnActivate
			// 
			this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnActivate.Location = new System.Drawing.Point(229, 135);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(75, 26);
			this.btnActivate.TabIndex = 2;
			this.btnActivate.Text = "Activate";
			this.btnActivate.UseVisualStyleBackColor = true;
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(310, 135);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 26);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(18, 98);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "E-Mail";
			// 
			// txtEMail
			// 
			this.txtEMail.Location = new System.Drawing.Point(101, 95);
			this.txtEMail.Name = "txtEMail";
			this.txtEMail.ReadOnly = true;
			this.txtEMail.Size = new System.Drawing.Size(285, 20);
			this.txtEMail.TabIndex = 8;
			// 
			// frmActivation
			// 
			this.AcceptButton = this.btnActivate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(398, 171);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtEMail);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnActivate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cmbPurchaseType);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtMachineKey);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtActivationKey);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbCodeName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmActivation";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PragmaSQL Product Activation";
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cmbCodeName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtActivationKey;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtMachineKey;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cmbPurchaseType;
    private System.Windows.Forms.Button btnActivate;
    private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtEMail;
  }
}