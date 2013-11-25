namespace PragmaSQL.Scripting.Smo
{
	partial class BulkCopyDialog
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkCopyDialog));
      this.wizardControl1 = new WizardBase.WizardControl();
      this.startStep1 = new WizardBase.StartStep();
      this.label1 = new System.Windows.Forms.Label();
      this.stepSelObjects = new WizardBase.IntermediateStep();
      this.objList = new PragmaSQL.Core.DbObjectList();
      this.stepFinish = new WizardBase.IntermediateStep();
      this.lblRowPrg = new System.Windows.Forms.Label();
      this.lblTotal = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbEmptyAll = new System.Windows.Forms.RadioButton();
      this.rbEmpty = new System.Windows.Forms.RadioButton();
      this.label4 = new System.Windows.Forms.Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.lvOptions = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.btnDestNew = new System.Windows.Forms.Button();
      this.btnDestFromRepo = new System.Windows.Forms.Button();
      this.edtDestConn = new System.Windows.Forms.TextBox();
      this.chkStopOnError = new System.Windows.Forms.CheckBox();
      this.pbRow = new System.Windows.Forms.ProgressBar();
      this.label2 = new System.Windows.Forms.Label();
      this.lblTimer = new System.Windows.Forms.Label();
      this.lblOverallPrg = new System.Windows.Forms.Label();
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.bw = new System.ComponentModel.BackgroundWorker();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.stepStart = new WizardBase.StartStep();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.startStep1.SuspendLayout();
      this.stepSelObjects.SuspendLayout();
      this.stepFinish.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.SuspendLayout();
      // 
      // wizardControl1
      // 
      this.wizardControl1.BackButtonEnabled = true;
      this.wizardControl1.BackButtonVisible = true;
      this.wizardControl1.CancelButtonEnabled = true;
      this.wizardControl1.CancelButtonVisible = true;
      this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.wizardControl1.EulaButtonEnabled = false;
      this.wizardControl1.EulaButtonText = "eula";
      this.wizardControl1.EulaButtonVisible = false;
      this.wizardControl1.FinishButtonText = "Copy";
      this.wizardControl1.HelpButtonEnabled = false;
      this.wizardControl1.HelpButtonVisible = true;
      this.wizardControl1.Location = new System.Drawing.Point(0, 0);
      this.wizardControl1.Name = "wizardControl1";
      this.wizardControl1.NextButtonEnabled = true;
      this.wizardControl1.NextButtonVisible = true;
      this.wizardControl1.Size = new System.Drawing.Size(606, 444);
      this.wizardControl1.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.startStep1,
            this.stepSelObjects,
            this.stepFinish});
      this.wizardControl1.HelpButtonClick += new System.EventHandler(this.wizardControl1_HelpButtonClick);
      this.wizardControl1.FinishButtonClick += new System.EventHandler(this.wizardControl1_FinishButtonClick);
      this.wizardControl1.NextButtonClick += new WizardBase.GenericCancelEventHandler<WizardBase.WizardControl>(this.wizardControl1_NextButtonClick);
      this.wizardControl1.CancelButtonClick += new System.EventHandler(this.wizardControl1_CancelButtonClick);
      // 
      // startStep1
      // 
      this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
      this.startStep1.Controls.Add(this.label1);
      this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
      this.startStep1.Name = "startStep1";
      this.startStep1.Subtitle = "Copy table data from source server to a destination server.";
      this.startStep1.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("startStep1.SubtitleAppearence")));
      this.startStep1.Title = "PragmaSQL Bulk Copy";
      this.startStep1.TitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("startStep1.TitleAppearence")));
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.Location = new System.Drawing.Point(175, 64);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(319, 104);
      this.label1.TabIndex = 0;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // stepSelObjects
      // 
      this.stepSelObjects.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepSelObjects.BindingImage")));
      this.stepSelObjects.Controls.Add(this.objList);
      this.stepSelObjects.ForeColor = System.Drawing.SystemColors.ControlText;
      this.stepSelObjects.Name = "stepSelObjects";
      this.stepSelObjects.Subtitle = "You can select tables/views from source server or import your configuration from " +
          "a file or current text editor.";
      this.stepSelObjects.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepSelObjects.SubtitleAppearence")));
      this.stepSelObjects.Title = "Select Tables or Views";
      this.stepSelObjects.TitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepSelObjects.TitleAppearence")));
      // 
      // objList
      // 
      this.objList.Location = new System.Drawing.Point(2, 63);
      this.objList.Name = "objList";
      this.objList.Size = new System.Drawing.Size(604, 340);
      this.objList.TabIndex = 3;
      this.objList.DumpToTexteditorCompleted += new System.EventHandler(this.objList_DumpToTexteditorCompleted);
      // 
      // stepFinish
      // 
      this.stepFinish.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepFinish.BindingImage")));
      this.stepFinish.Controls.Add(this.lblRowPrg);
      this.stepFinish.Controls.Add(this.lblTotal);
      this.stepFinish.Controls.Add(this.groupBox1);
      this.stepFinish.Controls.Add(this.label4);
      this.stepFinish.Controls.Add(this.pictureBox2);
      this.stepFinish.Controls.Add(this.lvOptions);
      this.stepFinish.Controls.Add(this.btnDestNew);
      this.stepFinish.Controls.Add(this.btnDestFromRepo);
      this.stepFinish.Controls.Add(this.edtDestConn);
      this.stepFinish.Controls.Add(this.chkStopOnError);
      this.stepFinish.Controls.Add(this.pbRow);
      this.stepFinish.Controls.Add(this.label2);
      this.stepFinish.Controls.Add(this.lblTimer);
      this.stepFinish.Controls.Add(this.lblOverallPrg);
      this.stepFinish.ForeColor = System.Drawing.SystemColors.ControlText;
      this.stepFinish.Name = "stepFinish";
      this.stepFinish.Subtitle = "Press Copy button to start copying data from source to destination";
      this.stepFinish.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepFinish.SubtitleAppearence")));
      this.stepFinish.Title = "Copy Data";
      this.stepFinish.TitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepFinish.TitleAppearence")));
      // 
      // lblRowPrg
      // 
      this.lblRowPrg.AutoSize = true;
      this.lblRowPrg.Location = new System.Drawing.Point(234, 166);
      this.lblRowPrg.Name = "lblRowPrg";
      this.lblRowPrg.Size = new System.Drawing.Size(81, 13);
      this.lblRowPrg.TabIndex = 2;
      this.lblRowPrg.Text = "Rows copied: 0";
      // 
      // lblTotal
      // 
      this.lblTotal.AutoSize = true;
      this.lblTotal.Location = new System.Drawing.Point(14, 184);
      this.lblTotal.Name = "lblTotal";
      this.lblTotal.Size = new System.Drawing.Size(120, 13);
      this.lblTotal.TabIndex = 20;
      this.lblTotal.Text = "Total rows processed: 0";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rbEmptyAll);
      this.groupBox1.Controls.Add(this.rbEmpty);
      this.groupBox1.Location = new System.Drawing.Point(17, 222);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(356, 62);
      this.groupBox1.TabIndex = 19;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Delete Data Options";
      // 
      // rbEmptyAll
      // 
      this.rbEmptyAll.AutoSize = true;
      this.rbEmptyAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.rbEmptyAll.ForeColor = System.Drawing.SystemColors.WindowText;
      this.rbEmptyAll.Location = new System.Drawing.Point(6, 39);
      this.rbEmptyAll.Name = "rbEmptyAll";
      this.rbEmptyAll.Size = new System.Drawing.Size(337, 17);
      this.rbEmptyAll.TabIndex = 0;
      this.rbEmptyAll.Text = "Empty all tables from the destination database before copying data";
      this.rbEmptyAll.UseVisualStyleBackColor = true;
      this.rbEmptyAll.CheckedChanged += new System.EventHandler(this.OnDeleteDataCheckedChanged);
      // 
      // rbEmpty
      // 
      this.rbEmpty.AutoSize = true;
      this.rbEmpty.Checked = true;
      this.rbEmpty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.rbEmpty.ForeColor = System.Drawing.Color.Red;
      this.rbEmpty.Location = new System.Drawing.Point(6, 16);
      this.rbEmpty.Name = "rbEmpty";
      this.rbEmpty.Size = new System.Drawing.Size(280, 17);
      this.rbEmpty.TabIndex = 0;
      this.rbEmpty.TabStop = true;
      this.rbEmpty.Text = "Empty destination tables before copying data";
      this.rbEmpty.UseVisualStyleBackColor = true;
      this.rbEmpty.CheckedChanged += new System.EventHandler(this.OnDeleteDataCheckedChanged);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label4.Location = new System.Drawing.Point(48, 67);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(472, 26);
      this.label4.TabIndex = 17;
      this.label4.Text = "PragmaSQL Bulk Copy Wizard assumes that destination tables already exist in the \r" +
          "\ndestination database and  has the same columns with corresponding source table." +
          "";
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(12, 64);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(32, 32);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox2.TabIndex = 16;
      this.pictureBox2.TabStop = false;
      // 
      // lvOptions
      // 
      this.lvOptions.CheckBoxes = true;
      this.lvOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.lvOptions.Location = new System.Drawing.Point(15, 312);
      this.lvOptions.MultiSelect = false;
      this.lvOptions.Name = "lvOptions";
      this.lvOptions.Size = new System.Drawing.Size(538, 86);
      this.lvOptions.TabIndex = 15;
      this.lvOptions.UseCompatibleStateImageBehavior = false;
      this.lvOptions.View = System.Windows.Forms.View.List;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Copy Option";
      this.columnHeader1.Width = 183;
      // 
      // btnDestNew
      // 
      this.btnDestNew.Image = global::PragmaSQL.Scripting.Smo.Properties.Resources.database_connect;
      this.btnDestNew.Location = new System.Drawing.Point(509, 115);
      this.btnDestNew.Name = "btnDestNew";
      this.btnDestNew.Size = new System.Drawing.Size(44, 24);
      this.btnDestNew.TabIndex = 14;
      this.toolTip1.SetToolTip(this.btnDestNew, "Specify destination connection");
      this.btnDestNew.UseVisualStyleBackColor = true;
      this.btnDestNew.Click += new System.EventHandler(this.btnDestNew_Click);
      // 
      // btnDestFromRepo
      // 
      this.btnDestFromRepo.Image = global::PragmaSQL.Scripting.Smo.Properties.Resources.database_save;
      this.btnDestFromRepo.Location = new System.Drawing.Point(465, 115);
      this.btnDestFromRepo.Name = "btnDestFromRepo";
      this.btnDestFromRepo.Size = new System.Drawing.Size(44, 24);
      this.btnDestFromRepo.TabIndex = 13;
      this.toolTip1.SetToolTip(this.btnDestFromRepo, "Select destination from saved connections");
      this.btnDestFromRepo.UseVisualStyleBackColor = true;
      this.btnDestFromRepo.Click += new System.EventHandler(this.btnDestFromRepo_Click);
      // 
      // edtDestConn
      // 
      this.edtDestConn.Location = new System.Drawing.Point(15, 117);
      this.edtDestConn.Name = "edtDestConn";
      this.edtDestConn.ReadOnly = true;
      this.edtDestConn.Size = new System.Drawing.Size(447, 20);
      this.edtDestConn.TabIndex = 11;
      // 
      // chkStopOnError
      // 
      this.chkStopOnError.AutoSize = true;
      this.chkStopOnError.Checked = true;
      this.chkStopOnError.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkStopOnError.Location = new System.Drawing.Point(22, 290);
      this.chkStopOnError.Name = "chkStopOnError";
      this.chkStopOnError.Size = new System.Drawing.Size(203, 17);
      this.chkStopOnError.TabIndex = 10;
      this.chkStopOnError.Text = "Stop on error and rollback transaction";
      this.chkStopOnError.UseVisualStyleBackColor = true;
      // 
      // pbRow
      // 
      this.pbRow.Location = new System.Drawing.Point(16, 145);
      this.pbRow.Name = "pbRow";
      this.pbRow.Size = new System.Drawing.Size(538, 18);
      this.pbRow.Step = 1;
      this.pbRow.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.pbRow.TabIndex = 9;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.Location = new System.Drawing.Point(14, 101);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(117, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Destination Connection";
      // 
      // lblTimer
      // 
      this.lblTimer.AutoSize = true;
      this.lblTimer.Location = new System.Drawing.Point(464, 166);
      this.lblTimer.Name = "lblTimer";
      this.lblTimer.Size = new System.Drawing.Size(90, 13);
      this.lblTimer.TabIndex = 6;
      this.lblTimer.Text = "Elapsed 00:00:00";
      // 
      // lblOverallPrg
      // 
      this.lblOverallPrg.AutoSize = true;
      this.lblOverallPrg.Location = new System.Drawing.Point(14, 166);
      this.lblOverallPrg.Name = "lblOverallPrg";
      this.lblOverallPrg.Size = new System.Drawing.Size(83, 13);
      this.lblOverallPrg.TabIndex = 0;
      this.lblOverallPrg.Text = "Overall progress";
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.ImageList1.Images.SetKeyName(0, "");
      this.ImageList1.Images.SetKeyName(1, "");
      this.ImageList1.Images.SetKeyName(2, "");
      this.ImageList1.Images.SetKeyName(3, "");
      this.ImageList1.Images.SetKeyName(4, "");
      // 
      // bw
      // 
      this.bw.WorkerReportsProgress = true;
      this.bw.WorkerSupportsCancellation = true;
      this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
      this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
      this.bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bw_ProgressChanged);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "sql";
      this.saveFileDialog1.Filter = "SQL Script|*.sql|All Files|*.*";
      // 
      // timer1
      // 
      this.timer1.Interval = 1000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // stepStart
      // 
      this.stepStart.BindingImage = null;
      this.stepStart.Icon = null;
      this.stepStart.Name = "stepStart";
      this.stepStart.Title = "PragmaSQL Scripter";
      // 
      // BulkCopyDialog
      // 
      this.ClientSize = new System.Drawing.Size(606, 444);
      this.Controls.Add(this.wizardControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "BulkCopyDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PragmaSQL Bulk Copy";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatchScripterDialog_FormClosing);
      this.startStep1.ResumeLayout(false);
      this.startStep1.PerformLayout();
      this.stepSelObjects.ResumeLayout(false);
      this.stepFinish.ResumeLayout(false);
      this.stepFinish.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.ResumeLayout(false);

		}

		#endregion

    private WizardBase.WizardControl wizardControl1;
		private WizardBase.IntermediateStep stepSelObjects;
		private WizardBase.IntermediateStep stepFinish;
		private System.Windows.Forms.Label lblRowPrg;
		private System.ComponentModel.BackgroundWorker bw;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label lblTimer;
		private System.Windows.Forms.Timer timer1;
    private PragmaSQL.Core.DbObjectList objList;
    private WizardBase.StartStep stepStart;
		private WizardBase.StartStep startStep1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList ImageList1;
		private System.Windows.Forms.ProgressBar pbRow;
		private System.Windows.Forms.Label lblOverallPrg;
		private System.Windows.Forms.TextBox edtDestConn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkStopOnError;
		private System.Windows.Forms.Button btnDestFromRepo;
		private System.Windows.Forms.Button btnDestNew;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ListView lvOptions;
    private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbEmptyAll;
		private System.Windows.Forms.RadioButton rbEmpty;
		private System.Windows.Forms.Label lblTotal;

	}
}