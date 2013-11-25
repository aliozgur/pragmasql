namespace SQLManagement
{
  partial class frmAttachDatabase
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttachDatabase));
      this.btnClose = new System.Windows.Forms.Button();
      this.btnAttach = new System.Windows.Forms.Button();
      this.txtName = new PragmaSQL.Core.LabelTextBox();
      this.fsLog = new PragmaSQL.Core.FileSelector();
      this.fsData = new PragmaSQL.Core.FileSelector();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(361, 201);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 28);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // btnAttach
      // 
      this.btnAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAttach.Location = new System.Drawing.Point(280, 201);
      this.btnAttach.Name = "btnAttach";
      this.btnAttach.Size = new System.Drawing.Size(75, 28);
      this.btnAttach.TabIndex = 1;
      this.btnAttach.Text = "Attach";
      this.btnAttach.UseVisualStyleBackColor = true;
      this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
      // 
      // txtName
      // 
      this.txtName.LabelText = "Database Name";
      this.txtName.Location = new System.Drawing.Point(1, 4);
      this.txtName.Name = "txtName";
      this.txtName.ReadOnly = false;
      this.txtName.Size = new System.Drawing.Size(271, 46);
      this.txtName.TabIndex = 4;
      this.txtName.TextBoxText = "";
      // 
      // fsLog
      // 
      this.fsLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fsLog.Filter = "";
      this.fsLog.LabelText = "Log File";
      this.fsLog.Location = new System.Drawing.Point(4, 113);
      this.fsLog.Name = "fsLog";
      this.fsLog.Path = "";
      this.fsLog.ReadOnly = false;
      this.fsLog.Size = new System.Drawing.Size(431, 51);
      this.fsLog.TabIndex = 3;
      // 
      // fsData
      // 
      this.fsData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fsData.Filter = "";
      this.fsData.LabelText = "Data File";
      this.fsData.Location = new System.Drawing.Point(4, 56);
      this.fsData.Name = "fsData";
      this.fsData.Path = "";
      this.fsData.ReadOnly = false;
      this.fsData.Size = new System.Drawing.Size(431, 51);
      this.fsData.TabIndex = 2;
      this.fsData.AfterFileOpened += new System.EventHandler(this.fsData_AfterFileOpened);
      // 
      // frmAttachDatabase
      // 
      this.AcceptButton = this.btnAttach;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(447, 242);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.fsLog);
      this.Controls.Add(this.fsData);
      this.Controls.Add(this.btnAttach);
      this.Controls.Add(this.btnClose);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAttachDatabase";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Attach Database";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnAttach;
    private PragmaSQL.Core.FileSelector fsData;
    private PragmaSQL.Core.FileSelector fsLog;
    private PragmaSQL.Core.LabelTextBox txtName;
  }
}