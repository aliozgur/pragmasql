namespace PragmaSQL.ExternalTools
{
	partial class RunToolForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunToolForm));
			this.btnRun = new System.Windows.Forms.Button();
			this.lb = new System.Windows.Forms.ListBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tbArgs = new System.Windows.Forms.TextBox();
			this.tbCmd = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbWorkingDir = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.chkClearOutput = new System.Windows.Forms.CheckBox();
			this.btnOptions = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnRun
			// 
			this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnRun.Location = new System.Drawing.Point(420, 272);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(61, 24);
			this.btnRun.TabIndex = 3;
			this.btnRun.Text = "Run (F5)";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// lb
			// 
			this.lb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lb.FormattingEnabled = true;
			this.lb.Location = new System.Drawing.Point(2, 1);
			this.lb.Margin = new System.Windows.Forms.Padding(0);
			this.lb.Name = "lb";
			this.lb.Size = new System.Drawing.Size(484, 238);
			this.lb.TabIndex = 2;
			this.lb.SelectedIndexChanged += new System.EventHandler(this.lb_SelectedIndexChanged);
			this.lb.DoubleClick += new System.EventHandler(this.lb_DoubleClick);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.Location = new System.Drawing.Point(420, 298);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(61, 23);
			this.btnRefresh.TabIndex = 11;
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 302);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "Arguments";
			// 
			// tbArgs
			// 
			this.tbArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbArgs.Location = new System.Drawing.Point(75, 300);
			this.tbArgs.Name = "tbArgs";
			this.tbArgs.Size = new System.Drawing.Size(342, 20);
			this.tbArgs.TabIndex = 14;
			// 
			// tbCmd
			// 
			this.tbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbCmd.Location = new System.Drawing.Point(75, 275);
			this.tbCmd.Name = "tbCmd";
			this.tbCmd.Size = new System.Drawing.Size(342, 20);
			this.tbCmd.TabIndex = 16;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 277);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Command";
			// 
			// tbWorkingDir
			// 
			this.tbWorkingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbWorkingDir.Location = new System.Drawing.Point(75, 325);
			this.tbWorkingDir.Name = "tbWorkingDir";
			this.tbWorkingDir.Size = new System.Drawing.Size(342, 20);
			this.tbWorkingDir.TabIndex = 18;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 327);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 13);
			this.label3.TabIndex = 17;
			this.label3.Text = "Working Dir.";
			// 
			// chkClearOutput
			// 
			this.chkClearOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkClearOutput.AutoSize = true;
			this.chkClearOutput.Checked = true;
			this.chkClearOutput.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkClearOutput.Location = new System.Drawing.Point(75, 252);
			this.chkClearOutput.Name = "chkClearOutput";
			this.chkClearOutput.Size = new System.Drawing.Size(174, 17);
			this.chkClearOutput.TabIndex = 19;
			this.chkClearOutput.Text = "Clear output before running tool";
			this.chkClearOutput.UseVisualStyleBackColor = true;
			// 
			// btnOptions
			// 
			this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOptions.Image")));
			this.btnOptions.Location = new System.Drawing.Point(420, 323);
			this.btnOptions.Name = "btnOptions";
			this.btnOptions.Size = new System.Drawing.Size(61, 23);
			this.btnOptions.TabIndex = 20;
			this.btnOptions.UseVisualStyleBackColor = true;
			this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
			// 
			// RunToolForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(489, 350);
			this.Controls.Add(this.lb);
			this.Controls.Add(this.btnOptions);
			this.Controls.Add(this.chkClearOutput);
			this.Controls.Add(this.tbWorkingDir);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbCmd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbArgs);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.btnRun);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RunToolForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TabText = "External Tools";
			this.Text = "External Tools";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RunToolForm_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.ListBox lb;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbArgs;
		private System.Windows.Forms.TextBox tbCmd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbWorkingDir;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkClearOutput;
		private System.Windows.Forms.Button btnOptions;
	}
}