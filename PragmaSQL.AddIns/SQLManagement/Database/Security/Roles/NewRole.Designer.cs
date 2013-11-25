namespace SQLManagement
{
  partial class NewRole
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
      this.cmbOwner = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbStandart = new System.Windows.Forms.RadioButton();
      this.rbApp = new System.Windows.Forms.RadioButton();
      this.txtPwd = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmbOwner
      // 
      this.cmbOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbOwner.FormattingEnabled = true;
      this.cmbOwner.Location = new System.Drawing.Point(83, 12);
      this.cmbOwner.Name = "cmbOwner";
      this.cmbOwner.Size = new System.Drawing.Size(228, 23);
      this.cmbOwner.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(16, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(39, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "Owner";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(83, 41);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(228, 23);
      this.txtName.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(16, 44);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 15);
      this.label2.TabIndex = 3;
      this.label2.Text = "Role Name";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.txtPwd);
      this.groupBox1.Controls.Add(this.rbApp);
      this.groupBox1.Controls.Add(this.rbStandart);
      this.groupBox1.Location = new System.Drawing.Point(19, 79);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(292, 112);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Role type";
      // 
      // rbStandart
      // 
      this.rbStandart.AutoSize = true;
      this.rbStandart.Checked = true;
      this.rbStandart.Location = new System.Drawing.Point(11, 22);
      this.rbStandart.Name = "rbStandart";
      this.rbStandart.Size = new System.Drawing.Size(90, 19);
      this.rbStandart.TabIndex = 0;
      this.rbStandart.TabStop = true;
      this.rbStandart.Text = "Standart Role";
      this.rbStandart.UseVisualStyleBackColor = true;
      this.rbStandart.CheckedChanged += new System.EventHandler(this.rbStandart_CheckedChanged);
      // 
      // rbApp
      // 
      this.rbApp.AutoSize = true;
      this.rbApp.Location = new System.Drawing.Point(11, 47);
      this.rbApp.Name = "rbApp";
      this.rbApp.Size = new System.Drawing.Size(104, 19);
      this.rbApp.TabIndex = 1;
      this.rbApp.Text = "Application Role";
      this.rbApp.UseVisualStyleBackColor = true;
      // 
      // txtPwd
      // 
      this.txtPwd.Location = new System.Drawing.Point(85, 69);
      this.txtPwd.Name = "txtPwd";
      this.txtPwd.PasswordChar = '*';
      this.txtPwd.ReadOnly = true;
      this.txtPwd.Size = new System.Drawing.Size(196, 23);
      this.txtPwd.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(26, 72);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(51, 15);
      this.label3.TabIndex = 4;
      this.label3.Text = "Password";
      // 
      // NewRole
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmbOwner);
      this.Name = "NewRole";
      this.Size = new System.Drawing.Size(337, 209);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox cmbOwner;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtPwd;
    private System.Windows.Forms.RadioButton rbApp;
    private System.Windows.Forms.RadioButton rbStandart;
  }
}
