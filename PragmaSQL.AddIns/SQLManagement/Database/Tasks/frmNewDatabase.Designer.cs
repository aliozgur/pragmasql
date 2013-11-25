namespace SQLManagement
{
  partial class frmNewDatabase
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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewDatabase));
      this.chkAttach = new System.Windows.Forms.CheckBox();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnCreate = new System.Windows.Forms.Button();
      this.bsOptions = new System.Windows.Forms.BindingSource(this.components);
      this.txtName = new PragmaSQL.Core.LabelTextBox();
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.groupBox6 = new System.Windows.Forms.GroupBox();
      this.txtLogFileGMaxSize = new System.Windows.Forms.TextBox();
      this.rbLogFileGSize = new System.Windows.Forms.RadioButton();
      this.rbLogFileGUnlimited = new System.Windows.Forms.RadioButton();
      this.cmbLogFileGrowthType = new System.Windows.Forms.ComboBox();
      this.txtLogFileGrowthRate = new PragmaSQL.Core.LabelTextBox();
      this.chkLogFileGAllowGrowth = new System.Windows.Forms.CheckBox();
      this.txtLogFileGSize = new PragmaSQL.Core.LabelTextBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.txtDataFileGMaxSize = new System.Windows.Forms.TextBox();
      this.rbDataFileGSize = new System.Windows.Forms.RadioButton();
      this.rbDataFileGUnlimited = new System.Windows.Forms.RadioButton();
      this.cmbDataFileGrowthType = new System.Windows.Forms.ComboBox();
      this.txtDataFileGrowthRate = new PragmaSQL.Core.LabelTextBox();
      this.chkDataFileGAllowGrowth = new System.Windows.Forms.CheckBox();
      this.txtDataFileGSize = new PragmaSQL.Core.LabelTextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txtLogFileSize = new PragmaSQL.Core.LabelTextBox();
      this.txtLogFilePath = new PragmaSQL.Core.FileSelector();
      this.txtLogFileName = new PragmaSQL.Core.LabelTextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtDataFileSize = new PragmaSQL.Core.LabelTextBox();
      this.txtDataFilePath = new PragmaSQL.Core.FileSelector();
      this.txtDataFileName = new PragmaSQL.Core.LabelTextBox();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.grdOptions = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.bsOptions)).BeginInit();
      this.tabControl2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPage4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdOptions)).BeginInit();
      this.SuspendLayout();
      // 
      // chkAttach
      // 
      this.chkAttach.AutoSize = true;
      this.chkAttach.Location = new System.Drawing.Point(10, 46);
      this.chkAttach.Name = "chkAttach";
      this.chkAttach.Size = new System.Drawing.Size(81, 17);
      this.chkAttach.TabIndex = 2;
      this.chkAttach.Text = "For Attach?";
      this.chkAttach.UseVisualStyleBackColor = true;
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(671, 543);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 8;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnCreate
      // 
      this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCreate.Location = new System.Drawing.Point(495, 543);
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new System.Drawing.Size(171, 23);
      this.btnCreate.TabIndex = 7;
      this.btnCreate.Text = "Create Database";
      this.btnCreate.UseVisualStyleBackColor = true;
      this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
      // 
      // txtName
      // 
      this.txtName.LabelText = "Database Name";
      this.txtName.Location = new System.Drawing.Point(3, 2);
      this.txtName.Name = "txtName";
      this.txtName.ReadOnly = false;
      this.txtName.Size = new System.Drawing.Size(271, 40);
      this.txtName.TabIndex = 1;
      this.txtName.TextBoxText = "";
      // 
      // tabControl2
      // 
      this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl2.Controls.Add(this.tabPage3);
      this.tabControl2.Controls.Add(this.tabPage4);
      this.tabControl2.Location = new System.Drawing.Point(10, 68);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(738, 465);
      this.tabControl2.TabIndex = 9;
      // 
      // tabPage3
      // 
      this.tabPage3.AutoScroll = true;
      this.tabPage3.Controls.Add(this.groupBox5);
      this.tabPage3.Controls.Add(this.groupBox3);
      this.tabPage3.Controls.Add(this.groupBox2);
      this.tabPage3.Controls.Add(this.groupBox1);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(730, 439);
      this.tabPage3.TabIndex = 0;
      this.tabPage3.Text = "Files";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.groupBox6);
      this.groupBox5.Controls.Add(this.cmbLogFileGrowthType);
      this.groupBox5.Controls.Add(this.txtLogFileGrowthRate);
      this.groupBox5.Controls.Add(this.chkLogFileGAllowGrowth);
      this.groupBox5.Controls.Add(this.txtLogFileGSize);
      this.groupBox5.Location = new System.Drawing.Point(472, 225);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(244, 201);
      this.groupBox5.TabIndex = 16;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Data File Growth";
      // 
      // groupBox6
      // 
      this.groupBox6.Controls.Add(this.txtLogFileGMaxSize);
      this.groupBox6.Controls.Add(this.rbLogFileGSize);
      this.groupBox6.Controls.Add(this.rbLogFileGUnlimited);
      this.groupBox6.Location = new System.Drawing.Point(17, 125);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new System.Drawing.Size(203, 62);
      this.groupBox6.TabIndex = 9;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Max size";
      // 
      // txtLogFileGMaxSize
      // 
      this.txtLogFileGMaxSize.Enabled = false;
      this.txtLogFileGMaxSize.Location = new System.Drawing.Point(87, 39);
      this.txtLogFileGMaxSize.Name = "txtLogFileGMaxSize";
      this.txtLogFileGMaxSize.Size = new System.Drawing.Size(80, 20);
      this.txtLogFileGMaxSize.TabIndex = 2;
      // 
      // rbLogFileGSize
      // 
      this.rbLogFileGSize.AutoSize = true;
      this.rbLogFileGSize.Location = new System.Drawing.Point(15, 41);
      this.rbLogFileGSize.Name = "rbLogFileGSize";
      this.rbLogFileGSize.Size = new System.Drawing.Size(68, 17);
      this.rbLogFileGSize.TabIndex = 1;
      this.rbLogFileGSize.Text = "Size (KB)";
      this.rbLogFileGSize.UseVisualStyleBackColor = true;
      // 
      // rbLogFileGUnlimited
      // 
      this.rbLogFileGUnlimited.AutoSize = true;
      this.rbLogFileGUnlimited.Checked = true;
      this.rbLogFileGUnlimited.Location = new System.Drawing.Point(15, 19);
      this.rbLogFileGUnlimited.Name = "rbLogFileGUnlimited";
      this.rbLogFileGUnlimited.Size = new System.Drawing.Size(68, 17);
      this.rbLogFileGUnlimited.TabIndex = 0;
      this.rbLogFileGUnlimited.TabStop = true;
      this.rbLogFileGUnlimited.Text = "Unlimited";
      this.rbLogFileGUnlimited.UseVisualStyleBackColor = true;
      // 
      // cmbLogFileGrowthType
      // 
      this.cmbLogFileGrowthType.FormattingEnabled = true;
      this.cmbLogFileGrowthType.Items.AddRange(new object[] {
            "%",
            "KB"});
      this.cmbLogFileGrowthType.Location = new System.Drawing.Point(159, 97);
      this.cmbLogFileGrowthType.Name = "cmbLogFileGrowthType";
      this.cmbLogFileGrowthType.Size = new System.Drawing.Size(61, 21);
      this.cmbLogFileGrowthType.TabIndex = 8;
      // 
      // txtLogFileGrowthRate
      // 
      this.txtLogFileGrowthRate.LabelText = "Growth Rate";
      this.txtLogFileGrowthRate.Location = new System.Drawing.Point(12, 80);
      this.txtLogFileGrowthRate.Name = "txtLogFileGrowthRate";
      this.txtLogFileGrowthRate.ReadOnly = false;
      this.txtLogFileGrowthRate.Size = new System.Drawing.Size(141, 38);
      this.txtLogFileGrowthRate.TabIndex = 7;
      this.txtLogFileGrowthRate.TextBoxText = "11";
      // 
      // chkLogFileGAllowGrowth
      // 
      this.chkLogFileGAllowGrowth.AutoSize = true;
      this.chkLogFileGAllowGrowth.Checked = true;
      this.chkLogFileGAllowGrowth.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkLogFileGAllowGrowth.Location = new System.Drawing.Point(18, 58);
      this.chkLogFileGAllowGrowth.Name = "chkLogFileGAllowGrowth";
      this.chkLogFileGAllowGrowth.Size = new System.Drawing.Size(88, 17);
      this.chkLogFileGAllowGrowth.TabIndex = 6;
      this.chkLogFileGAllowGrowth.Text = "Allow Growth";
      this.chkLogFileGAllowGrowth.UseVisualStyleBackColor = true;
      // 
      // txtLogFileGSize
      // 
      this.txtLogFileGSize.LabelText = "Size (KB)";
      this.txtLogFileGSize.Location = new System.Drawing.Point(12, 15);
      this.txtLogFileGSize.Name = "txtLogFileGSize";
      this.txtLogFileGSize.ReadOnly = false;
      this.txtLogFileGSize.Size = new System.Drawing.Size(141, 39);
      this.txtLogFileGSize.TabIndex = 5;
      this.txtLogFileGSize.TextBoxText = "10";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.groupBox4);
      this.groupBox3.Controls.Add(this.cmbDataFileGrowthType);
      this.groupBox3.Controls.Add(this.txtDataFileGrowthRate);
      this.groupBox3.Controls.Add(this.chkDataFileGAllowGrowth);
      this.groupBox3.Controls.Add(this.txtDataFileGSize);
      this.groupBox3.Location = new System.Drawing.Point(472, 19);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(244, 201);
      this.groupBox3.TabIndex = 15;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Data File Growth";
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.txtDataFileGMaxSize);
      this.groupBox4.Controls.Add(this.rbDataFileGSize);
      this.groupBox4.Controls.Add(this.rbDataFileGUnlimited);
      this.groupBox4.Location = new System.Drawing.Point(11, 120);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(203, 65);
      this.groupBox4.TabIndex = 19;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Max size";
      // 
      // txtDataFileGMaxSize
      // 
      this.txtDataFileGMaxSize.Enabled = false;
      this.txtDataFileGMaxSize.Location = new System.Drawing.Point(87, 39);
      this.txtDataFileGMaxSize.Name = "txtDataFileGMaxSize";
      this.txtDataFileGMaxSize.Size = new System.Drawing.Size(80, 20);
      this.txtDataFileGMaxSize.TabIndex = 2;
      // 
      // rbDataFileGSize
      // 
      this.rbDataFileGSize.AutoSize = true;
      this.rbDataFileGSize.Location = new System.Drawing.Point(15, 41);
      this.rbDataFileGSize.Name = "rbDataFileGSize";
      this.rbDataFileGSize.Size = new System.Drawing.Size(68, 17);
      this.rbDataFileGSize.TabIndex = 1;
      this.rbDataFileGSize.Text = "Size (KB)";
      this.rbDataFileGSize.UseVisualStyleBackColor = true;
      // 
      // rbDataFileGUnlimited
      // 
      this.rbDataFileGUnlimited.AutoSize = true;
      this.rbDataFileGUnlimited.Checked = true;
      this.rbDataFileGUnlimited.Location = new System.Drawing.Point(15, 19);
      this.rbDataFileGUnlimited.Name = "rbDataFileGUnlimited";
      this.rbDataFileGUnlimited.Size = new System.Drawing.Size(68, 17);
      this.rbDataFileGUnlimited.TabIndex = 0;
      this.rbDataFileGUnlimited.TabStop = true;
      this.rbDataFileGUnlimited.Text = "Unlimited";
      this.rbDataFileGUnlimited.UseVisualStyleBackColor = true;
      // 
      // cmbDataFileGrowthType
      // 
      this.cmbDataFileGrowthType.FormattingEnabled = true;
      this.cmbDataFileGrowthType.Items.AddRange(new object[] {
            "%",
            "KB"});
      this.cmbDataFileGrowthType.Location = new System.Drawing.Point(152, 94);
      this.cmbDataFileGrowthType.Name = "cmbDataFileGrowthType";
      this.cmbDataFileGrowthType.Size = new System.Drawing.Size(61, 21);
      this.cmbDataFileGrowthType.TabIndex = 18;
      // 
      // txtDataFileGrowthRate
      // 
      this.txtDataFileGrowthRate.LabelText = "Growth Rate";
      this.txtDataFileGrowthRate.Location = new System.Drawing.Point(6, 77);
      this.txtDataFileGrowthRate.Name = "txtDataFileGrowthRate";
      this.txtDataFileGrowthRate.ReadOnly = false;
      this.txtDataFileGrowthRate.Size = new System.Drawing.Size(141, 38);
      this.txtDataFileGrowthRate.TabIndex = 17;
      this.txtDataFileGrowthRate.TextBoxText = "11";
      // 
      // chkDataFileGAllowGrowth
      // 
      this.chkDataFileGAllowGrowth.AutoSize = true;
      this.chkDataFileGAllowGrowth.Checked = true;
      this.chkDataFileGAllowGrowth.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkDataFileGAllowGrowth.Location = new System.Drawing.Point(12, 59);
      this.chkDataFileGAllowGrowth.Name = "chkDataFileGAllowGrowth";
      this.chkDataFileGAllowGrowth.Size = new System.Drawing.Size(88, 17);
      this.chkDataFileGAllowGrowth.TabIndex = 16;
      this.chkDataFileGAllowGrowth.Text = "Allow Growth";
      this.chkDataFileGAllowGrowth.UseVisualStyleBackColor = true;
      // 
      // txtDataFileGSize
      // 
      this.txtDataFileGSize.LabelText = "Size (KB)";
      this.txtDataFileGSize.Location = new System.Drawing.Point(6, 16);
      this.txtDataFileGSize.Name = "txtDataFileGSize";
      this.txtDataFileGSize.ReadOnly = false;
      this.txtDataFileGSize.Size = new System.Drawing.Size(141, 39);
      this.txtDataFileGSize.TabIndex = 15;
      this.txtDataFileGSize.TextBoxText = "10";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.txtLogFileSize);
      this.groupBox2.Controls.Add(this.txtLogFilePath);
      this.groupBox2.Controls.Add(this.txtLogFileName);
      this.groupBox2.Location = new System.Drawing.Point(14, 225);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(439, 201);
      this.groupBox2.TabIndex = 14;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Log  File";
      // 
      // txtLogFileSize
      // 
      this.txtLogFileSize.LabelText = "File Size (MB)";
      this.txtLogFileSize.Location = new System.Drawing.Point(8, 105);
      this.txtLogFileSize.Name = "txtLogFileSize";
      this.txtLogFileSize.ReadOnly = false;
      this.txtLogFileSize.Size = new System.Drawing.Size(80, 40);
      this.txtLogFileSize.TabIndex = 4;
      this.txtLogFileSize.TextBoxText = "10";
      // 
      // txtLogFilePath
      // 
      this.txtLogFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLogFilePath.DialogType = PragmaSQL.Core.DialogType.Folder;
      this.txtLogFilePath.Filter = "";
      this.txtLogFilePath.LabelText = "File Path";
      this.txtLogFilePath.Location = new System.Drawing.Point(10, 57);
      this.txtLogFilePath.Name = "txtLogFilePath";
      this.txtLogFilePath.Path = "";
      this.txtLogFilePath.ReadOnly = false;
      this.txtLogFilePath.Size = new System.Drawing.Size(415, 44);
      this.txtLogFilePath.TabIndex = 3;
      // 
      // txtLogFileName
      // 
      this.txtLogFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtLogFileName.LabelText = "File Name";
      this.txtLogFileName.Location = new System.Drawing.Point(8, 14);
      this.txtLogFileName.Name = "txtLogFileName";
      this.txtLogFileName.ReadOnly = false;
      this.txtLogFileName.Size = new System.Drawing.Size(384, 40);
      this.txtLogFileName.TabIndex = 2;
      this.txtLogFileName.TextBoxText = "";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtDataFileSize);
      this.groupBox1.Controls.Add(this.txtDataFilePath);
      this.groupBox1.Controls.Add(this.txtDataFileName);
      this.groupBox1.Location = new System.Drawing.Point(14, 19);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(439, 201);
      this.groupBox1.TabIndex = 13;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Data  File";
      // 
      // txtDataFileSize
      // 
      this.txtDataFileSize.LabelText = "File Size (MB)";
      this.txtDataFileSize.Location = new System.Drawing.Point(8, 105);
      this.txtDataFileSize.Name = "txtDataFileSize";
      this.txtDataFileSize.ReadOnly = false;
      this.txtDataFileSize.Size = new System.Drawing.Size(80, 40);
      this.txtDataFileSize.TabIndex = 4;
      this.txtDataFileSize.TextBoxText = "10";
      // 
      // txtDataFilePath
      // 
      this.txtDataFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDataFilePath.DialogType = PragmaSQL.Core.DialogType.Folder;
      this.txtDataFilePath.Filter = "";
      this.txtDataFilePath.LabelText = "File Path";
      this.txtDataFilePath.Location = new System.Drawing.Point(10, 57);
      this.txtDataFilePath.Name = "txtDataFilePath";
      this.txtDataFilePath.Path = "";
      this.txtDataFilePath.ReadOnly = false;
      this.txtDataFilePath.Size = new System.Drawing.Size(420, 44);
      this.txtDataFilePath.TabIndex = 3;
      // 
      // txtDataFileName
      // 
      this.txtDataFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDataFileName.LabelText = "File Name";
      this.txtDataFileName.Location = new System.Drawing.Point(8, 14);
      this.txtDataFileName.Name = "txtDataFileName";
      this.txtDataFileName.ReadOnly = false;
      this.txtDataFileName.Size = new System.Drawing.Size(389, 40);
      this.txtDataFileName.TabIndex = 2;
      this.txtDataFileName.TextBoxText = "";
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.grdOptions);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(730, 439);
      this.tabPage4.TabIndex = 1;
      this.tabPage4.Text = "Options";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // grdOptions
      // 
      this.grdOptions.AllowUserToAddRows = false;
      this.grdOptions.AllowUserToDeleteRows = false;
      this.grdOptions.AllowUserToResizeRows = false;
      this.grdOptions.AutoGenerateColumns = false;
      this.grdOptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.grdOptions.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grdOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdOptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
      this.grdOptions.DataSource = this.bsOptions;
      this.grdOptions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdOptions.Location = new System.Drawing.Point(3, 3);
      this.grdOptions.Name = "grdOptions";
      this.grdOptions.RowHeadersVisible = false;
      this.grdOptions.RowTemplate.Height = 25;
      this.grdOptions.Size = new System.Drawing.Size(724, 433);
      this.grdOptions.TabIndex = 3;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "name";
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
      this.Column1.HeaderText = "Option";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Width = 63;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "isset";
      this.Column2.HeaderText = "Is Set?";
      this.Column2.Name = "Column2";
      this.Column2.Width = 46;
      // 
      // frmNewDatabase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(760, 576);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnCreate);
      this.Controls.Add(this.tabControl2);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.chkAttach);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmNewDatabase";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "New Database";
      ((System.ComponentModel.ISupportInitialize)(this.bsOptions)).EndInit();
      this.tabControl2.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdOptions)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private PragmaSQL.Core.LabelTextBox txtName;
    private System.Windows.Forms.CheckBox chkAttach;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnCreate;
    private System.Windows.Forms.BindingSource bsOptions;
    private System.Windows.Forms.TabControl tabControl2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.TextBox txtLogFileGMaxSize;
    private System.Windows.Forms.RadioButton rbLogFileGSize;
    private System.Windows.Forms.RadioButton rbLogFileGUnlimited;
    private System.Windows.Forms.ComboBox cmbLogFileGrowthType;
    private PragmaSQL.Core.LabelTextBox txtLogFileGrowthRate;
    private System.Windows.Forms.CheckBox chkLogFileGAllowGrowth;
    private PragmaSQL.Core.LabelTextBox txtLogFileGSize;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.TextBox txtDataFileGMaxSize;
    private System.Windows.Forms.RadioButton rbDataFileGSize;
    private System.Windows.Forms.RadioButton rbDataFileGUnlimited;
    private System.Windows.Forms.ComboBox cmbDataFileGrowthType;
    private PragmaSQL.Core.LabelTextBox txtDataFileGrowthRate;
    private System.Windows.Forms.CheckBox chkDataFileGAllowGrowth;
    private PragmaSQL.Core.LabelTextBox txtDataFileGSize;
    private System.Windows.Forms.GroupBox groupBox2;
    private PragmaSQL.Core.LabelTextBox txtLogFileSize;
    private PragmaSQL.Core.FileSelector txtLogFilePath;
    private PragmaSQL.Core.LabelTextBox txtLogFileName;
    private System.Windows.Forms.GroupBox groupBox1;
    private PragmaSQL.Core.LabelTextBox txtDataFileSize;
    private PragmaSQL.Core.FileSelector txtDataFilePath;
    private PragmaSQL.Core.LabelTextBox txtDataFileName;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.DataGridView grdOptions;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
  }
}