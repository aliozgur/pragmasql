namespace PragmaSQL.Core
{
  partial class ucSharedSnippetsOptions 
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
      this.chkConfirmDescriptionSave = new System.Windows.Forms.CheckBox();
      this.chkAlwaysUseOfflineScriptEditor = new System.Windows.Forms.CheckBox();
      this.chkAlwaysShowDescription = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.rdOrderLast = new System.Windows.Forms.RadioButton();
      this.rdOrderFirst = new System.Windows.Forms.RadioButton();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.chkShowItemToolTip);
      this.groupBox1.Controls.Add(this.chkConfirmDescriptionSave);
      this.groupBox1.Controls.Add(this.chkAlwaysUseOfflineScriptEditor);
      this.groupBox1.Controls.Add(this.chkAlwaysShowDescription);
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
      // chkConfirmDescriptionSave
      // 
      this.chkConfirmDescriptionSave.AutoSize = true;
      this.chkConfirmDescriptionSave.Location = new System.Drawing.Point(16, 84);
      this.chkConfirmDescriptionSave.Name = "chkConfirmDescriptionSave";
      this.chkConfirmDescriptionSave.Size = new System.Drawing.Size(141, 17);
      this.chkConfirmDescriptionSave.TabIndex = 2;
      this.chkConfirmDescriptionSave.Text = "Confirm description save";
      this.chkConfirmDescriptionSave.UseVisualStyleBackColor = true;
      this.chkConfirmDescriptionSave.CheckedChanged += new System.EventHandler(this.chkAlwaysShowDescription_CheckedChanged);
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
      // chkAlwaysShowDescription
      // 
      this.chkAlwaysShowDescription.AutoSize = true;
      this.chkAlwaysShowDescription.Location = new System.Drawing.Point(16, 20);
      this.chkAlwaysShowDescription.Name = "chkAlwaysShowDescription";
      this.chkAlwaysShowDescription.Size = new System.Drawing.Size(220, 17);
      this.chkAlwaysShowDescription.TabIndex = 0;
      this.chkAlwaysShowDescription.Text = "Initially item descriptions pane is visible (*)";
      this.chkAlwaysShowDescription.UseVisualStyleBackColor = true;
      this.chkAlwaysShowDescription.CheckedChanged += new System.EventHandler(this.chkAlwaysShowDescription_CheckedChanged);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(5, 178);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(176, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "(*) Changes will be applied next time";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.rdOrderLast);
      this.groupBox2.Controls.Add(this.rdOrderFirst);
      this.groupBox2.Location = new System.Drawing.Point(8, 123);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(373, 47);
      this.groupBox2.TabIndex = 16;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Code Completion List Order";
      // 
      // rdOrderLast
      // 
      this.rdOrderLast.AutoSize = true;
      this.rdOrderLast.Location = new System.Drawing.Point(76, 19);
      this.rdOrderLast.Name = "rdOrderLast";
      this.rdOrderLast.Size = new System.Drawing.Size(45, 17);
      this.rdOrderLast.TabIndex = 1;
      this.rdOrderLast.Text = "Last";
      this.rdOrderLast.UseVisualStyleBackColor = true;
      this.rdOrderLast.CheckedChanged += new System.EventHandler(this.rdOrderFirst_CheckedChanged);
      // 
      // rdOrderFirst
      // 
      this.rdOrderFirst.AutoSize = true;
      this.rdOrderFirst.Checked = true;
      this.rdOrderFirst.Location = new System.Drawing.Point(16, 19);
      this.rdOrderFirst.Name = "rdOrderFirst";
      this.rdOrderFirst.Size = new System.Drawing.Size(44, 17);
      this.rdOrderFirst.TabIndex = 0;
      this.rdOrderFirst.TabStop = true;
      this.rdOrderFirst.Text = "First";
      this.rdOrderFirst.UseVisualStyleBackColor = true;
      this.rdOrderFirst.CheckedChanged += new System.EventHandler(this.rdOrderFirst_CheckedChanged);
      // 
      // ucSharedSnippetsOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.groupBox1);
      this.Name = "ucSharedSnippetsOptions";
      this.Size = new System.Drawing.Size(394, 201);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkConfirmDescriptionSave;
    private System.Windows.Forms.CheckBox chkAlwaysUseOfflineScriptEditor;
    private System.Windows.Forms.CheckBox chkAlwaysShowDescription;
    private System.Windows.Forms.CheckBox chkShowItemToolTip;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton rdOrderLast;
    private System.Windows.Forms.RadioButton rdOrderFirst;
  }
}
