namespace PragmaSQL.Core
{
	partial class frmAsyncConnectionOpener
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.pd = new PragmaSQL.Core.ProgressDisk();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblInfo = new System.Windows.Forms.Label();
			this.bw = new System.ComponentModel.BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.pd);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.lblInfo);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(249, 83);
			this.panel1.TabIndex = 0;
			// 
			// pd
			// 
			this.pd.ActiveForeColor1 = System.Drawing.Color.DarkMagenta;
			this.pd.ActiveForeColor2 = System.Drawing.Color.Navy;
			this.pd.BackColor = System.Drawing.Color.Transparent;
			this.pd.BackGroundColor = System.Drawing.Color.Transparent;
			this.pd.BlockSize = PragmaSQL.Core.BlockSize.Medium;
			this.pd.InactiveForeColor1 = System.Drawing.Color.DarkGray;
			this.pd.InactiveForeColor2 = System.Drawing.Color.Gainsboro;
			this.pd.Location = new System.Drawing.Point(5, 11);
			this.pd.Name = "pd";
			this.pd.Size = new System.Drawing.Size(30, 30);
			this.pd.SliceCount = 15;
			this.pd.SquareSize = 30;
			this.pd.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(87, 47);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 27);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.BackColor = System.Drawing.Color.Transparent;
			this.lblInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblInfo.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lblInfo.Location = new System.Drawing.Point(41, 20);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(115, 13);
			this.lblInfo.TabIndex = 1;
			this.lblInfo.Text = "Connecting to {0}. ";
			// 
			// bw
			// 
			this.bw.WorkerSupportsCancellation = true;
			this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
			this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 500;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// frmAsyncConnectionOpener
			// 
			this.AcceptButton = this.btnCancel;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(249, 83);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAsyncConnectionOpener";
			this.Opacity = 0.95;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PragmaSQL is connecting...";
			this.Shown += new System.EventHandler(this.frmAsyncConnectionOpener_Shown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblInfo;
		private System.ComponentModel.BackgroundWorker bw;
		private System.Windows.Forms.Button btnCancel;
		private PragmaSQL.Core.ProgressDisk pd;
		private System.Windows.Forms.Timer timer1;
	}
}