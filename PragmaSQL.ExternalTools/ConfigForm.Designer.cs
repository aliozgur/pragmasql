namespace PragmaSQL.ExternalTools
{
	partial class ConfigForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
			this.lb = new System.Windows.Forms.ListBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.ctxMacros = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tbArgs = new PragmaSQL.Core.LabelTextBox();
			this.tbTitle = new PragmaSQL.Core.LabelTextBox();
			this.tbCmd = new PragmaSQL.Core.FileSelector();
			this.chkUseOutput = new System.Windows.Forms.CheckBox();
			this.tbWorkingDir = new PragmaSQL.Core.FileSelector();
			this.SuspendLayout();
			// 
			// lb
			// 
			this.lb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lb.FormattingEnabled = true;
			this.lb.Location = new System.Drawing.Point(5, 10);
			this.lb.Name = "lb";
			this.lb.Size = new System.Drawing.Size(371, 173);
			this.lb.TabIndex = 0;
			this.toolTip1.SetToolTip(this.lb, "Double click on the item in order to edit external tool definition.");
			this.lb.SelectedIndexChanged += new System.EventHandler(this.lb_SelectedIndexChanged);
			this.lb.DoubleClick += new System.EventHandler(this.lb_DoubleClick);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(382, 9);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(62, 25);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Location = new System.Drawing.Point(382, 38);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(62, 25);
			this.btnRemove.TabIndex = 2;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(361, 401);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 26);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(280, 401);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 26);
			this.btnOk.TabIndex = 10;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(401, 293);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(28, 22);
			this.button1.TabIndex = 11;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// ctxMacros
			// 
			this.ctxMacros.Name = "ctxMacros";
			this.ctxMacros.Size = new System.Drawing.Size(61, 4);
			// 
			// tbArgs
			// 
			this.tbArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbArgs.LabelText = "Arguments";
			this.tbArgs.Location = new System.Drawing.Point(3, 277);
			this.tbArgs.Name = "tbArgs";
			this.tbArgs.ReadOnly = false;
			this.tbArgs.Size = new System.Drawing.Size(398, 40);
			this.tbArgs.TabIndex = 8;
			this.tbArgs.TextBoxText = "";
			// 
			// tbTitle
			// 
			this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbTitle.LabelText = "Title";
			this.tbTitle.Location = new System.Drawing.Point(3, 189);
			this.tbTitle.Name = "tbTitle";
			this.tbTitle.ReadOnly = false;
			this.tbTitle.Size = new System.Drawing.Size(434, 38);
			this.tbTitle.TabIndex = 7;
			this.tbTitle.TextBoxText = "";
			// 
			// tbCmd
			// 
			this.tbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbCmd.DialogType = PragmaSQL.Core.DialogType.File;
			this.tbCmd.Filter = "";
			this.tbCmd.LabelText = "Command";
			this.tbCmd.Location = new System.Drawing.Point(5, 231);
			this.tbCmd.Name = "tbCmd";
			this.tbCmd.Path = "";
			this.tbCmd.ReadOnly = false;
			this.tbCmd.Size = new System.Drawing.Size(431, 40);
			this.tbCmd.TabIndex = 6;
			// 
			// chkUseOutput
			// 
			this.chkUseOutput.AutoSize = true;
			this.chkUseOutput.Location = new System.Drawing.Point(8, 366);
			this.chkUseOutput.Name = "chkUseOutput";
			this.chkUseOutput.Size = new System.Drawing.Size(245, 17);
			this.chkUseOutput.TabIndex = 12;
			this.chkUseOutput.Text = "Redirect output/erros to Application Messages";
			this.chkUseOutput.UseVisualStyleBackColor = true;
			this.chkUseOutput.CheckedChanged += new System.EventHandler(this.chkUseOutput_CheckedChanged);
			// 
			// tbWorkingDir
			// 
			this.tbWorkingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbWorkingDir.DialogType = PragmaSQL.Core.DialogType.Folder;
			this.tbWorkingDir.Filter = "";
			this.tbWorkingDir.LabelText = "Working Dir";
			this.tbWorkingDir.Location = new System.Drawing.Point(4, 320);
			this.tbWorkingDir.Name = "tbWorkingDir";
			this.tbWorkingDir.Path = "";
			this.tbWorkingDir.ReadOnly = false;
			this.tbWorkingDir.Size = new System.Drawing.Size(431, 40);
			this.tbWorkingDir.TabIndex = 13;
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(449, 437);
			this.Controls.Add(this.tbWorkingDir);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.chkUseOutput);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tbArgs);
			this.Controls.Add(this.tbTitle);
			this.Controls.Add(this.tbCmd);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lb);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Configure External Tools";
			this.Load += new System.EventHandler(this.ConfigForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lb;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private PragmaSQL.Core.FileSelector tbCmd;
		private PragmaSQL.Core.LabelTextBox tbTitle;
		private PragmaSQL.Core.LabelTextBox tbArgs;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ContextMenuStrip ctxMacros;
		private System.Windows.Forms.CheckBox chkUseOutput;
		private PragmaSQL.Core.FileSelector tbWorkingDir;
	}
}