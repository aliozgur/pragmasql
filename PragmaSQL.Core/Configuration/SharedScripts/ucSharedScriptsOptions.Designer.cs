namespace PragmaSQL.Core
{
  partial class ucSharedScriptsOptions
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkShowItemToolTip = new System.Windows.Forms.CheckBox();
      this.chkConfirmHelpTextSave = new System.Windows.Forms.CheckBox();
      this.chkAlwaysUseOfflineScriptEditor = new System.Windows.Forms.CheckBox();
      this.chkAlwaysShowHelpText = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.chkShowItemToolTip);
      this.groupBox1.Controls.Add(this.chkConfirmHelpTextSave);
      this.groupBox1.Controls.Add(this.chkAlwaysUseOfflineScriptEditor);
      this.groupBox1.Controls.Add(this.chkAlwaysShowHelpText);
      this.groupBox1.Location = new System.Drawing.Point(8, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(373, 114);
      this.groupBox1.TabIndex = 14;
      this.groupBox1.TabStop = false;
      // 
      // chkShowItemToolTip
      // 
      this.chkShowItemToolTip.AutoSize = true;
      this.chkShowItemToolTip.Location = new System.Drawing.Point(16, 42);
      this.chkShowItemToolTip.Name = "chkShowItemToolTip";
      this.chkShowItemToolTip.Size = new System.Drawing.Size(114, 17);
      this.chkShowItemToolTip.TabIndex = 3;
      this.chkShowItemToolTip.Text = "Show item tool tips";
      this.chkShowItemToolTip.UseVisualStyleBackColor = true;
      this.chkShowItemToolTip.CheckedChanged += new System.EventHandler(this.chkAlwaysShowDescription_CheckedChanged);
      // 
      // chkConfirmHelpTextSave
      // 
      this.chkConfirmHelpTextSave.AutoSize = true;
      this.chkConfirmHelpTextSave.Location = new System.Drawing.Point(16, 84);
      this.chkConfirmHelpTextSave.Name = "chkConfirmHelpTextSave";
      this.chkConfirmHelpTextSave.Size = new System.Drawing.Size(130, 17);
      this.chkConfirmHelpTextSave.TabIndex = 2;
      this.chkConfirmHelpTextSave.Text = "Confirm help text save";
      this.chkConfirmHelpTextSave.UseVisualStyleBackColor = true;
      this.chkConfirmHelpTextSave.CheckedChanged += new System.EventHandler(this.chkAlwaysShowDescription_CheckedChanged);
      // 
      // chkAlwaysUseOfflineScriptEditor
      // 
      this.chkAlwaysUseOfflineScriptEditor.AutoSize = true;
      this.chkAlwaysUseOfflineScriptEditor.Location = new System.Drawing.Point(16, 62);
      this.chkAlwaysUseOfflineScriptEditor.Name = "chkAlwaysUseOfflineScriptEditor";
      this.chkAlwaysUseOfflineScriptEditor.Size = new System.Drawing.Size(177, 17);
      this.chkAlwaysUseOfflineScriptEditor.TabIndex = 1;
      this.chkAlwaysUseOfflineScriptEditor.Text = "Always use text editor for editing";
      this.chkAlwaysUseOfflineScriptEditor.UseVisualStyleBackColor = true;
      this.chkAlwaysUseOfflineScriptEditor.CheckedChanged += new System.EventHandler(this.chkAlwaysShowDescription_CheckedChanged);
      // 
      // chkAlwaysShowHelpText
      // 
      this.chkAlwaysShowHelpText.AutoSize = true;
      this.chkAlwaysShowHelpText.Location = new System.Drawing.Point(16, 20);
      this.chkAlwaysShowHelpText.Name = "chkAlwaysShowHelpText";
      this.chkAlwaysShowHelpText.Size = new System.Drawing.Size(204, 17);
      this.chkAlwaysShowHelpText.TabIndex = 0;
      this.chkAlwaysShowHelpText.Text = "Initially item help text pane is visible (*)";
      this.chkAlwaysShowHelpText.UseVisualStyleBackColor = true;
      this.chkAlwaysShowHelpText.CheckedChanged += new System.EventHandler(this.chkAlwaysShowDescription_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 127);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(176, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "(*) Changes will be applied next time";
      // 
      // ucSharedScriptsOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.label1);
      this.Controls.Add(this.groupBox1);
      this.Name = "ucSharedScriptsOptions";
      this.Size = new System.Drawing.Size(394, 151);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkConfirmHelpTextSave;
    private System.Windows.Forms.CheckBox chkAlwaysUseOfflineScriptEditor;
    private System.Windows.Forms.CheckBox chkAlwaysShowHelpText;
    private System.Windows.Forms.CheckBox chkShowItemToolTip;
    private System.Windows.Forms.Label label1;
  }
}
