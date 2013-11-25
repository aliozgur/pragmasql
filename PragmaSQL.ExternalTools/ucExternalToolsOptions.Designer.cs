namespace PragmaSQL.ExternalTools
{
  partial class ucExternalToolsOptions
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucExternalToolsOptions));
			this.lb = new System.Windows.Forms.ListBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.tbTitle = new PragmaSQL.Core.LabelTextBox();
			this.tbCmd = new PragmaSQL.Core.FileSelector();
			this.tbArgs = new PragmaSQL.Core.LabelTextBox();
			this.tbWorkingDir = new PragmaSQL.Core.FileSelector();
			this.chkUseOutput = new System.Windows.Forms.CheckBox();
			this.btnMacroList = new System.Windows.Forms.Button();
			this.ctxMacros = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkSaveBeforeExecute = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lb
			// 
			this.lb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lb.FormattingEnabled = true;
			this.lb.Location = new System.Drawing.Point(12, 13);
			this.lb.Name = "lb";
			this.lb.Size = new System.Drawing.Size(420, 160);
			this.lb.TabIndex = 1;
			this.lb.SelectedIndexChanged += new System.EventHandler(this.lb_SelectedIndexChanged);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Location = new System.Drawing.Point(438, 42);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(62, 25);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(438, 13);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(62, 25);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// tbTitle
			// 
			this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbTitle.LabelText = "Title";
			this.tbTitle.Location = new System.Drawing.Point(12, 179);
			this.tbTitle.Name = "tbTitle";
			this.tbTitle.ReadOnly = false;
			this.tbTitle.Size = new System.Drawing.Size(497, 38);
			this.tbTitle.TabIndex = 8;
			this.tbTitle.TextBoxText = "";
			// 
			// tbCmd
			// 
			this.tbCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbCmd.DialogType = PragmaSQL.Core.DialogType.File;
			this.tbCmd.Filter = "";
			this.tbCmd.LabelText = "Command";
			this.tbCmd.Location = new System.Drawing.Point(13, 223);
			this.tbCmd.Name = "tbCmd";
			this.tbCmd.Path = "";
			this.tbCmd.ReadOnly = false;
			this.tbCmd.Size = new System.Drawing.Size(494, 40);
			this.tbCmd.TabIndex = 9;
			// 
			// tbArgs
			// 
			this.tbArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbArgs.LabelText = "Arguments";
			this.tbArgs.Location = new System.Drawing.Point(11, 269);
			this.tbArgs.Name = "tbArgs";
			this.tbArgs.ReadOnly = false;
			this.tbArgs.Size = new System.Drawing.Size(461, 40);
			this.tbArgs.TabIndex = 10;
			this.tbArgs.TextBoxText = "";
			// 
			// tbWorkingDir
			// 
			this.tbWorkingDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbWorkingDir.DialogType = PragmaSQL.Core.DialogType.Folder;
			this.tbWorkingDir.Filter = "";
			this.tbWorkingDir.LabelText = "Working Dir";
			this.tbWorkingDir.Location = new System.Drawing.Point(12, 315);
			this.tbWorkingDir.Name = "tbWorkingDir";
			this.tbWorkingDir.Path = "";
			this.tbWorkingDir.ReadOnly = false;
			this.tbWorkingDir.Size = new System.Drawing.Size(494, 40);
			this.tbWorkingDir.TabIndex = 14;
			// 
			// chkUseOutput
			// 
			this.chkUseOutput.AutoSize = true;
			this.chkUseOutput.Location = new System.Drawing.Point(17, 361);
			this.chkUseOutput.Name = "chkUseOutput";
			this.chkUseOutput.Size = new System.Drawing.Size(245, 17);
			this.chkUseOutput.TabIndex = 15;
			this.chkUseOutput.Text = "Redirect output/erros to Application Messages";
			this.chkUseOutput.UseVisualStyleBackColor = true;
			this.chkUseOutput.CheckedChanged += new System.EventHandler(this.chkUseOutput_CheckedChanged);
			// 
			// btnMacroList
			// 
			this.btnMacroList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMacroList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.btnMacroList.Image = ((System.Drawing.Image)(resources.GetObject("btnMacroList.Image")));
			this.btnMacroList.Location = new System.Drawing.Point(471, 286);
			this.btnMacroList.Name = "btnMacroList";
			this.btnMacroList.Size = new System.Drawing.Size(29, 22);
			this.btnMacroList.TabIndex = 16;
			this.btnMacroList.UseVisualStyleBackColor = true;
			this.btnMacroList.Click += new System.EventHandler(this.btnMacroList_Click);
			// 
			// ctxMacros
			// 
			this.ctxMacros.Name = "ctxMacros";
			this.ctxMacros.Size = new System.Drawing.Size(61, 4);
			// 
			// chkSaveBeforeExecute
			// 
			this.chkSaveBeforeExecute.AutoSize = true;
			this.chkSaveBeforeExecute.Location = new System.Drawing.Point(17, 384);
			this.chkSaveBeforeExecute.Name = "chkSaveBeforeExecute";
			this.chkSaveBeforeExecute.Size = new System.Drawing.Size(217, 17);
			this.chkSaveBeforeExecute.TabIndex = 17;
			this.chkSaveBeforeExecute.Text = "Save file in current editor before execute";
			this.chkSaveBeforeExecute.UseVisualStyleBackColor = true;
			this.chkSaveBeforeExecute.CheckedChanged += new System.EventHandler(this.chkSaveBeforeExecute_CheckedChanged);
			// 
			// ucExternalToolsOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkSaveBeforeExecute);
			this.Controls.Add(this.btnMacroList);
			this.Controls.Add(this.chkUseOutput);
			this.Controls.Add(this.tbWorkingDir);
			this.Controls.Add(this.tbArgs);
			this.Controls.Add(this.tbCmd);
			this.Controls.Add(this.tbTitle);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lb);
			this.Name = "ucExternalToolsOptions";
			this.Size = new System.Drawing.Size(575, 413);
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox lb;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.Button btnAdd;
    private PragmaSQL.Core.LabelTextBox tbTitle;
    private PragmaSQL.Core.FileSelector tbCmd;
    private PragmaSQL.Core.LabelTextBox tbArgs;
    private PragmaSQL.Core.FileSelector tbWorkingDir;
    private System.Windows.Forms.CheckBox chkUseOutput;
    private System.Windows.Forms.Button btnMacroList;
    private System.Windows.Forms.ContextMenuStrip ctxMacros;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.CheckBox chkSaveBeforeExecute;
  }
}
