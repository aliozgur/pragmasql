namespace PragmaSQL.Scripting.Smo
{
	partial class BatchScripterDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchScripterDialog));
      this.wizardControl1 = new WizardBase.WizardControl();
      this.startStep1 = new WizardBase.StartStep();
      this.label1 = new System.Windows.Forms.Label();
      this.stepSelObjects = new WizardBase.IntermediateStep();
      this.objList = new PragmaSQL.Core.DbObjectList();
      this.stepSetOptions = new WizardBase.IntermediateStep();
      this.edtBatchSep = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.propOptions = new System.Windows.Forms.PropertyGrid();
      this.stepFinish = new WizardBase.IntermediateStep();
      this.edtDestFile = new System.Windows.Forms.TextBox();
      this.gbError = new System.Windows.Forms.GroupBox();
      this.edtError = new System.Windows.Forms.TextBox();
      this.lblTimer = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rbOnCompleteClose = new System.Windows.Forms.RadioButton();
      this.rbOnCompleteMessage = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rbToFileAndOpen = new System.Windows.Forms.RadioButton();
      this.rdToFile = new System.Windows.Forms.RadioButton();
      this.rdToWindow = new System.Windows.Forms.RadioButton();
      this.lblObjectInfo = new System.Windows.Forms.Label();
      this.pb = new System.Windows.Forms.ProgressBar();
      this.lblGenTaskInfo = new System.Windows.Forms.Label();
      this.bw = new System.ComponentModel.BackgroundWorker();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.stepStart = new WizardBase.StartStep();
      this.startStep1.SuspendLayout();
      this.stepSelObjects.SuspendLayout();
      this.stepSetOptions.SuspendLayout();
      this.stepFinish.SuspendLayout();
      this.gbError.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // wizardControl1
      // 
      this.wizardControl1.BackButtonEnabled = true;
      this.wizardControl1.BackButtonVisible = true;
      this.wizardControl1.CancelButtonEnabled = true;
      this.wizardControl1.CancelButtonVisible = true;
      this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Top;
      this.wizardControl1.EulaButtonEnabled = false;
      this.wizardControl1.EulaButtonText = "eula";
      this.wizardControl1.EulaButtonVisible = false;
      this.wizardControl1.FinishButtonText = "Generate";
      this.wizardControl1.HelpButtonEnabled = false;
      this.wizardControl1.HelpButtonVisible = true;
      this.wizardControl1.Location = new System.Drawing.Point(0, 0);
      this.wizardControl1.Name = "wizardControl1";
      this.wizardControl1.NextButtonEnabled = true;
      this.wizardControl1.NextButtonVisible = true;
      this.wizardControl1.Size = new System.Drawing.Size(601, 425);
      this.wizardControl1.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.startStep1,
            this.stepSelObjects,
            this.stepSetOptions,
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
      this.startStep1.Subtitle = "Script your database objects by selecting them from list, loading from file or cu" +
          "rrent text editor";
      this.startStep1.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("startStep1.SubtitleAppearence")));
      this.startStep1.Title = "PragmaSQL Scripter";
      this.startStep1.TitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("startStep1.TitleAppearence")));
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.Location = new System.Drawing.Point(172, 100);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(359, 96);
      this.label1.TabIndex = 0;
      this.label1.Text = resources.GetString("label1.Text");
      // 
      // stepSelObjects
      // 
      this.stepSelObjects.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepSelObjects.BindingImage")));
      this.stepSelObjects.Controls.Add(this.objList);
      this.stepSelObjects.ForeColor = System.Drawing.SystemColors.ControlText;
      this.stepSelObjects.Name = "stepSelObjects";
      this.stepSelObjects.Subtitle = "You can select database objects or import your configuration from a file or curre" +
          "nt text editor.";
      this.stepSelObjects.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepSelObjects.SubtitleAppearence")));
      this.stepSelObjects.Title = "Select Database Objects";
      this.stepSelObjects.TitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepSelObjects.TitleAppearence")));
      // 
      // objList
      // 
      this.objList.Location = new System.Drawing.Point(1, 63);
      this.objList.Name = "objList";
      this.objList.Size = new System.Drawing.Size(600, 320);
      this.objList.TabIndex = 3;
      this.objList.DumpToTexteditorCompleted += new System.EventHandler(this.objList_DumpToTexteditorCompleted);
      // 
      // stepSetOptions
      // 
      this.stepSetOptions.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepSetOptions.BindingImage")));
      this.stepSetOptions.Controls.Add(this.edtBatchSep);
      this.stepSetOptions.Controls.Add(this.label2);
      this.stepSetOptions.Controls.Add(this.propOptions);
      this.stepSetOptions.ForeColor = System.Drawing.SystemColors.ControlText;
      this.stepSetOptions.Name = "stepSetOptions";
      this.stepSetOptions.Subtitle = "Specify options to be used while scripting selected database objects.";
      this.stepSetOptions.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepSetOptions.SubtitleAppearence")));
      this.stepSetOptions.Title = "Scripting Options";
      this.stepSetOptions.TitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepSetOptions.TitleAppearence")));
      // 
      // edtBatchSep
      // 
      this.edtBatchSep.Location = new System.Drawing.Point(89, 66);
      this.edtBatchSep.Name = "edtBatchSep";
      this.edtBatchSep.Size = new System.Drawing.Size(75, 20);
      this.edtBatchSep.TabIndex = 2;
      this.edtBatchSep.Text = "GO";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(2, 69);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(84, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Batch Separator";
      // 
      // propOptions
      // 
      this.propOptions.Location = new System.Drawing.Point(5, 94);
      this.propOptions.Name = "propOptions";
      this.propOptions.Size = new System.Drawing.Size(593, 284);
      this.propOptions.TabIndex = 0;
      // 
      // stepFinish
      // 
      this.stepFinish.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepFinish.BindingImage")));
      this.stepFinish.Controls.Add(this.edtDestFile);
      this.stepFinish.Controls.Add(this.gbError);
      this.stepFinish.Controls.Add(this.lblTimer);
      this.stepFinish.Controls.Add(this.groupBox2);
      this.stepFinish.Controls.Add(this.groupBox1);
      this.stepFinish.Controls.Add(this.lblObjectInfo);
      this.stepFinish.Controls.Add(this.pb);
      this.stepFinish.Controls.Add(this.lblGenTaskInfo);
      this.stepFinish.ForeColor = System.Drawing.SystemColors.ControlText;
      this.stepFinish.Name = "stepFinish";
      this.stepFinish.Subtitle = "Press Generate button to start script generation process.";
      this.stepFinish.SubtitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepFinish.SubtitleAppearence")));
      this.stepFinish.Title = "Generate Script";
      this.stepFinish.TitleAppearence = ((WizardBase.TextAppearence)(resources.GetObject("stepFinish.TitleAppearence")));
      // 
      // edtDestFile
      // 
      this.edtDestFile.BackColor = System.Drawing.SystemColors.Control;
      this.edtDestFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtDestFile.Location = new System.Drawing.Point(16, 201);
      this.edtDestFile.Name = "edtDestFile";
      this.edtDestFile.ReadOnly = true;
      this.edtDestFile.Size = new System.Drawing.Size(527, 13);
      this.edtDestFile.TabIndex = 8;
      // 
      // gbError
      // 
      this.gbError.Controls.Add(this.edtError);
      this.gbError.Location = new System.Drawing.Point(16, 221);
      this.gbError.Name = "gbError";
      this.gbError.Size = new System.Drawing.Size(534, 158);
      this.gbError.TabIndex = 7;
      this.gbError.TabStop = false;
      this.gbError.Text = "Last Error";
      this.gbError.Visible = false;
      // 
      // edtError
      // 
      this.edtError.BackColor = System.Drawing.SystemColors.Control;
      this.edtError.ForeColor = System.Drawing.Color.Red;
      this.edtError.Location = new System.Drawing.Point(8, 17);
      this.edtError.Multiline = true;
      this.edtError.Name = "edtError";
      this.edtError.ReadOnly = true;
      this.edtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.edtError.Size = new System.Drawing.Size(518, 134);
      this.edtError.TabIndex = 0;
      // 
      // lblTimer
      // 
      this.lblTimer.AutoSize = true;
      this.lblTimer.Location = new System.Drawing.Point(460, 93);
      this.lblTimer.Name = "lblTimer";
      this.lblTimer.Size = new System.Drawing.Size(90, 13);
      this.lblTimer.TabIndex = 6;
      this.lblTimer.Text = "Elapsed 00:00:00";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.rbOnCompleteClose);
      this.groupBox2.Controls.Add(this.rbOnCompleteMessage);
      this.groupBox2.Location = new System.Drawing.Point(403, 133);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(147, 60);
      this.groupBox2.TabIndex = 5;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "On Completion";
      // 
      // rbOnCompleteClose
      // 
      this.rbOnCompleteClose.AutoSize = true;
      this.rbOnCompleteClose.Checked = true;
      this.rbOnCompleteClose.Location = new System.Drawing.Point(6, 16);
      this.rbOnCompleteClose.Name = "rbOnCompleteClose";
      this.rbOnCompleteClose.Size = new System.Drawing.Size(51, 17);
      this.rbOnCompleteClose.TabIndex = 1;
      this.rbOnCompleteClose.TabStop = true;
      this.rbOnCompleteClose.Text = "Close";
      this.rbOnCompleteClose.UseVisualStyleBackColor = true;
      // 
      // rbOnCompleteMessage
      // 
      this.rbOnCompleteMessage.AutoSize = true;
      this.rbOnCompleteMessage.Location = new System.Drawing.Point(6, 39);
      this.rbOnCompleteMessage.Name = "rbOnCompleteMessage";
      this.rbOnCompleteMessage.Size = new System.Drawing.Size(97, 17);
      this.rbOnCompleteMessage.TabIndex = 0;
      this.rbOnCompleteMessage.Text = "Show message";
      this.rbOnCompleteMessage.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rbToFileAndOpen);
      this.groupBox1.Controls.Add(this.rdToFile);
      this.groupBox1.Controls.Add(this.rdToWindow);
      this.groupBox1.Location = new System.Drawing.Point(15, 133);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(382, 60);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Script Destination";
      // 
      // rbToFileAndOpen
      // 
      this.rbToFileAndOpen.AutoSize = true;
      this.rbToFileAndOpen.Location = new System.Drawing.Point(243, 18);
      this.rbToFileAndOpen.Name = "rbToFileAndOpen";
      this.rbToFileAndOpen.Size = new System.Drawing.Size(128, 17);
      this.rbToFileAndOpen.TabIndex = 1;
      this.rbToFileAndOpen.Text = "Script to file and open";
      this.rbToFileAndOpen.UseVisualStyleBackColor = true;
      // 
      // rdToFile
      // 
      this.rdToFile.AutoSize = true;
      this.rdToFile.Location = new System.Drawing.Point(145, 18);
      this.rdToFile.Name = "rdToFile";
      this.rdToFile.Size = new System.Drawing.Size(80, 17);
      this.rdToFile.TabIndex = 1;
      this.rdToFile.Text = "Script to file";
      this.rdToFile.UseVisualStyleBackColor = true;
      // 
      // rdToWindow
      // 
      this.rdToWindow.AutoSize = true;
      this.rdToWindow.Checked = true;
      this.rdToWindow.Location = new System.Drawing.Point(10, 17);
      this.rdToWindow.Name = "rdToWindow";
      this.rdToWindow.Size = new System.Drawing.Size(126, 17);
      this.rdToWindow.TabIndex = 0;
      this.rdToWindow.TabStop = true;
      this.rdToWindow.Text = "Script to new window";
      this.rdToWindow.UseVisualStyleBackColor = true;
      // 
      // lblObjectInfo
      // 
      this.lblObjectInfo.AutoSize = true;
      this.lblObjectInfo.Location = new System.Drawing.Point(12, 114);
      this.lblObjectInfo.Name = "lblObjectInfo";
      this.lblObjectInfo.Size = new System.Drawing.Size(58, 13);
      this.lblObjectInfo.TabIndex = 2;
      this.lblObjectInfo.Text = "Object info";
      // 
      // pb
      // 
      this.pb.Location = new System.Drawing.Point(12, 69);
      this.pb.Name = "pb";
      this.pb.Size = new System.Drawing.Size(538, 18);
      this.pb.Step = 1;
      this.pb.TabIndex = 1;
      // 
      // lblGenTaskInfo
      // 
      this.lblGenTaskInfo.AutoSize = true;
      this.lblGenTaskInfo.Location = new System.Drawing.Point(12, 93);
      this.lblGenTaskInfo.Name = "lblGenTaskInfo";
      this.lblGenTaskInfo.Size = new System.Drawing.Size(98, 13);
      this.lblGenTaskInfo.TabIndex = 0;
      this.lblGenTaskInfo.Text = "Ready for scripting.";
      // 
      // bw
      // 
      this.bw.WorkerReportsProgress = true;
      this.bw.WorkerSupportsCancellation = true;
      this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
      this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
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
      // BatchScripterDialog
      // 
      this.ClientSize = new System.Drawing.Size(601, 421);
      this.Controls.Add(this.wizardControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "BatchScripterDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PragmaSQL Scripter";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatchScripterDialog_FormClosing);
      this.startStep1.ResumeLayout(false);
      this.startStep1.PerformLayout();
      this.stepSelObjects.ResumeLayout(false);
      this.stepSetOptions.ResumeLayout(false);
      this.stepSetOptions.PerformLayout();
      this.stepFinish.ResumeLayout(false);
      this.stepFinish.PerformLayout();
      this.gbError.ResumeLayout(false);
      this.gbError.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

    private WizardBase.WizardControl wizardControl1;
		private WizardBase.IntermediateStep stepSelObjects;
    private WizardBase.IntermediateStep stepSetOptions;
		private System.Windows.Forms.PropertyGrid propOptions;
		private WizardBase.IntermediateStep stepFinish;
		private System.Windows.Forms.Label lblObjectInfo;
		private System.Windows.Forms.ProgressBar pb;
		private System.Windows.Forms.Label lblGenTaskInfo;
		private System.ComponentModel.BackgroundWorker bw;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdToFile;
		private System.Windows.Forms.RadioButton rdToWindow;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbOnCompleteClose;
		private System.Windows.Forms.RadioButton rbOnCompleteMessage;
		private System.Windows.Forms.Label lblTimer;
		private System.Windows.Forms.Timer timer1;
    private PragmaSQL.Core.DbObjectList objList;
    private WizardBase.StartStep stepStart;
    private WizardBase.StartStep startStep1;
    private System.Windows.Forms.GroupBox gbError;
    private System.Windows.Forms.TextBox edtError;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox edtDestFile;
		private System.Windows.Forms.RadioButton rbToFileAndOpen;
		private System.Windows.Forms.TextBox edtBatchSep;
		private System.Windows.Forms.Label label2;

	}
}