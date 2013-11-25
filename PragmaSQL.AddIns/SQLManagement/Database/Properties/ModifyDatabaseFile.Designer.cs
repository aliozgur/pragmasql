namespace SQLManagement
{
  partial class ModifyDatabaseFile
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
      this.chkAllowGrowth = new System.Windows.Forms.CheckBox();
      this.cmbGrowthType = new System.Windows.Forms.ComboBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtMaxSize = new System.Windows.Forms.TextBox();
      this.rbSize = new System.Windows.Forms.RadioButton();
      this.rbUnlimited = new System.Windows.Forms.RadioButton();
      this.txtGrowthRate = new PragmaSQL.Core.LabelTextBox();
      this.txtSize = new PragmaSQL.Core.LabelTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lblFileName = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // chkAllowGrowth
      // 
      this.chkAllowGrowth.AutoSize = true;
      this.chkAllowGrowth.Checked = true;
      this.chkAllowGrowth.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkAllowGrowth.Location = new System.Drawing.Point(6, 75);
      this.chkAllowGrowth.Name = "chkAllowGrowth";
      this.chkAllowGrowth.Size = new System.Drawing.Size(92, 19);
      this.chkAllowGrowth.TabIndex = 1;
      this.chkAllowGrowth.Text = "Allow Growth";
      this.chkAllowGrowth.UseVisualStyleBackColor = true;
      // 
      // cmbGrowthType
      // 
      this.cmbGrowthType.FormattingEnabled = true;
      this.cmbGrowthType.Items.AddRange(new object[] {
            "%",
            "KB"});
      this.cmbGrowthType.Location = new System.Drawing.Point(147, 121);
      this.cmbGrowthType.Name = "cmbGrowthType";
      this.cmbGrowthType.Size = new System.Drawing.Size(61, 23);
      this.cmbGrowthType.TabIndex = 3;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMaxSize);
      this.groupBox1.Controls.Add(this.rbSize);
      this.groupBox1.Controls.Add(this.rbUnlimited);
      this.groupBox1.Location = new System.Drawing.Point(5, 152);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(203, 89);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Max size";
      // 
      // txtMaxSize
      // 
      this.txtMaxSize.Enabled = false;
      this.txtMaxSize.Location = new System.Drawing.Point(87, 45);
      this.txtMaxSize.Name = "txtMaxSize";
      this.txtMaxSize.Size = new System.Drawing.Size(80, 23);
      this.txtMaxSize.TabIndex = 2;
      // 
      // rbSize
      // 
      this.rbSize.AutoSize = true;
      this.rbSize.Location = new System.Drawing.Point(15, 47);
      this.rbSize.Name = "rbSize";
      this.rbSize.Size = new System.Drawing.Size(65, 19);
      this.rbSize.TabIndex = 1;
      this.rbSize.Text = "Size (KB)";
      this.rbSize.UseVisualStyleBackColor = true;
      // 
      // rbUnlimited
      // 
      this.rbUnlimited.AutoSize = true;
      this.rbUnlimited.Checked = true;
      this.rbUnlimited.Location = new System.Drawing.Point(15, 22);
      this.rbUnlimited.Name = "rbUnlimited";
      this.rbUnlimited.Size = new System.Drawing.Size(73, 19);
      this.rbUnlimited.TabIndex = 0;
      this.rbUnlimited.TabStop = true;
      this.rbUnlimited.Text = "Unlimited";
      this.rbUnlimited.UseVisualStyleBackColor = true;
      this.rbUnlimited.CheckedChanged += new System.EventHandler(this.rbUnlimited_CheckedChanged);
      // 
      // txtGrowthRate
      // 
      this.txtGrowthRate.LabelText = "Growth Rate";
      this.txtGrowthRate.Location = new System.Drawing.Point(0, 100);
      this.txtGrowthRate.Name = "txtGrowthRate";
      this.txtGrowthRate.ReadOnly = false;
      this.txtGrowthRate.Size = new System.Drawing.Size(141, 44);
      this.txtGrowthRate.TabIndex = 2;
      this.txtGrowthRate.TextBoxText = "";
      // 
      // txtSize
      // 
      this.txtSize.LabelText = "Size (KB)";
      this.txtSize.Location = new System.Drawing.Point(0, 25);
      this.txtSize.Name = "txtSize";
      this.txtSize.ReadOnly = false;
      this.txtSize.Size = new System.Drawing.Size(141, 45);
      this.txtSize.TabIndex = 0;
      this.txtSize.TextBoxText = "";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.ForeColor = System.Drawing.Color.Navy;
      this.label1.Location = new System.Drawing.Point(5, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(64, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "File Name:";
      // 
      // lblFileName
      // 
      this.lblFileName.AutoSize = true;
      this.lblFileName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblFileName.ForeColor = System.Drawing.Color.Navy;
      this.lblFileName.Location = new System.Drawing.Point(67, 8);
      this.lblFileName.Name = "lblFileName";
      this.lblFileName.Size = new System.Drawing.Size(79, 13);
      this.lblFileName.TabIndex = 6;
      this.lblFileName.Text = "<File Name>";
      // 
      // ModifyDatabaseFile
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.lblFileName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cmbGrowthType);
      this.Controls.Add(this.txtGrowthRate);
      this.Controls.Add(this.chkAllowGrowth);
      this.Controls.Add(this.txtSize);
      this.Name = "ModifyDatabaseFile";
      this.Size = new System.Drawing.Size(241, 277);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private PragmaSQL.Core.LabelTextBox txtSize;
    private System.Windows.Forms.CheckBox chkAllowGrowth;
    private PragmaSQL.Core.LabelTextBox txtGrowthRate;
    private System.Windows.Forms.ComboBox cmbGrowthType;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtMaxSize;
    private System.Windows.Forms.RadioButton rbSize;
    private System.Windows.Forms.RadioButton rbUnlimited;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblFileName;
  }
}
