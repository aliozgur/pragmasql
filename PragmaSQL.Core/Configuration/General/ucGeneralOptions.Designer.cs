namespace PragmaSQL.Core
{
  partial class ucGeneralOptions
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
      this.label3 = new System.Windows.Forms.Label();
      this.cmbPopupBlockerFilterLvl = new System.Windows.Forms.ComboBox();
      this.btnGoToUrl = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.txtHomeUrl = new System.Windows.Forms.TextBox();
      this.chkShowWebBrowserOnStartup = new System.Windows.Forms.CheckBox();
      this.lblEditPanel = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.chkUseCustomPalette = new System.Windows.Forms.CheckBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.cmbPaletteMode = new System.Windows.Forms.ComboBox();
      this.cmbDocStyle = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.button2 = new System.Windows.Forms.Button();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.chkSingleInstance = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.chkRestoreWorkspace = new System.Windows.Forms.CheckBox();
      this.chkStartWithNewScriptEditor = new System.Windows.Forms.CheckBox();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.label4 = new System.Windows.Forms.Label();
      this.nudAutoSaveInterval = new System.Windows.Forms.NumericUpDown();
      this.chkAutoSave = new System.Windows.Forms.CheckBox();
      this.chkConnectionPooling = new System.Windows.Forms.CheckBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.tabPage4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudAutoSaveInterval)).BeginInit();
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(11, 86);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(429, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Popup Blocker Filter Level  (Windows XP SP2 or higher is required for the pop-up " +
    "blocker)";
      // 
      // cmbPopupBlockerFilterLvl
      // 
      this.cmbPopupBlockerFilterLvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbPopupBlockerFilterLvl.FormattingEnabled = true;
      this.cmbPopupBlockerFilterLvl.Location = new System.Drawing.Point(14, 101);
      this.cmbPopupBlockerFilterLvl.Name = "cmbPopupBlockerFilterLvl";
      this.cmbPopupBlockerFilterLvl.Size = new System.Drawing.Size(121, 21);
      this.cmbPopupBlockerFilterLvl.TabIndex = 4;
      this.cmbPopupBlockerFilterLvl.SelectedIndexChanged += new System.EventHandler(this.cmbPopupBlockerFilterLvl_SelectedIndexChanged);
      // 
      // btnGoToUrl
      // 
      this.btnGoToUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGoToUrl.Location = new System.Drawing.Point(574, 59);
      this.btnGoToUrl.Name = "btnGoToUrl";
      this.btnGoToUrl.Size = new System.Drawing.Size(50, 22);
      this.btnGoToUrl.TabIndex = 3;
      this.btnGoToUrl.Text = "Go";
      this.btnGoToUrl.UseVisualStyleBackColor = true;
      this.btnGoToUrl.Click += new System.EventHandler(this.btnGoToUrl_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 42);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Home Url";
      // 
      // txtHomeUrl
      // 
      this.txtHomeUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHomeUrl.Location = new System.Drawing.Point(13, 59);
      this.txtHomeUrl.Name = "txtHomeUrl";
      this.txtHomeUrl.Size = new System.Drawing.Size(555, 20);
      this.txtHomeUrl.TabIndex = 1;
      this.txtHomeUrl.TextChanged += new System.EventHandler(this.txtHomeUrl_TextChanged);
      // 
      // chkShowWebBrowserOnStartup
      // 
      this.chkShowWebBrowserOnStartup.AutoSize = true;
      this.chkShowWebBrowserOnStartup.Location = new System.Drawing.Point(14, 16);
      this.chkShowWebBrowserOnStartup.Name = "chkShowWebBrowserOnStartup";
      this.chkShowWebBrowserOnStartup.Size = new System.Drawing.Size(154, 17);
      this.chkShowWebBrowserOnStartup.TabIndex = 0;
      this.chkShowWebBrowserOnStartup.Text = "Show web browser on start";
      this.chkShowWebBrowserOnStartup.UseVisualStyleBackColor = true;
      this.chkShowWebBrowserOnStartup.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // lblEditPanel
      // 
      this.lblEditPanel.AutoSize = true;
      this.lblEditPanel.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblEditPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblEditPanel.ForeColor = System.Drawing.Color.Blue;
      this.lblEditPanel.Location = new System.Drawing.Point(231, 83);
      this.lblEditPanel.Name = "lblEditPanel";
      this.lblEditPanel.Size = new System.Drawing.Size(99, 13);
      this.lblEditPanel.TabIndex = 30;
      this.lblEditPanel.Text = "Edit Custom Palette";
      this.lblEditPanel.Click += new System.EventHandler(this.label9_Click);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(336, 13);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(88, 13);
      this.label7.TabIndex = 29;
      this.label7.Text = "(Restart Needed)";
      // 
      // chkUseCustomPalette
      // 
      this.chkUseCustomPalette.AutoSize = true;
      this.chkUseCustomPalette.Location = new System.Drawing.Point(108, 83);
      this.chkUseCustomPalette.Name = "chkUseCustomPalette";
      this.chkUseCustomPalette.Size = new System.Drawing.Size(117, 17);
      this.chkUseCustomPalette.TabIndex = 25;
      this.chkUseCustomPalette.Text = "Use custom palette";
      this.chkUseCustomPalette.UseVisualStyleBackColor = true;
      this.chkUseCustomPalette.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(11, 60);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(70, 13);
      this.label8.TabIndex = 28;
      this.label8.Text = "Palette Mode";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(11, 13);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(82, 13);
      this.label6.TabIndex = 28;
      this.label6.Text = "Document Style";
      // 
      // cmbPaletteMode
      // 
      this.cmbPaletteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbPaletteMode.FormattingEnabled = true;
      this.cmbPaletteMode.Location = new System.Drawing.Point(108, 52);
      this.cmbPaletteMode.Name = "cmbPaletteMode";
      this.cmbPaletteMode.Size = new System.Drawing.Size(222, 21);
      this.cmbPaletteMode.TabIndex = 27;
      this.cmbPaletteMode.SelectedIndexChanged += new System.EventHandler(this.cmbDocStyle_SelectedIndexChanged);
      // 
      // cmbDocStyle
      // 
      this.cmbDocStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbDocStyle.FormattingEnabled = true;
      this.cmbDocStyle.Location = new System.Drawing.Point(108, 10);
      this.cmbDocStyle.Name = "cmbDocStyle";
      this.cmbDocStyle.Size = new System.Drawing.Size(222, 21);
      this.cmbDocStyle.TabIndex = 27;
      this.cmbDocStyle.SelectedIndexChanged += new System.EventHandler(this.cmbDocStyle_SelectedIndexChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(10, 116);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(91, 13);
      this.label5.TabIndex = 23;
      this.label5.Text = "Application Folder";
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.Location = new System.Drawing.Point(532, 114);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 22);
      this.button2.TabIndex = 22;
      this.button2.Text = "Open Folder";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // textBox2
      // 
      this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox2.Location = new System.Drawing.Point(108, 114);
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(418, 20);
      this.textBox2.TabIndex = 21;
      // 
      // chkSingleInstance
      // 
      this.chkSingleInstance.AutoSize = true;
      this.chkSingleInstance.Location = new System.Drawing.Point(14, 56);
      this.chkSingleInstance.Name = "chkSingleInstance";
      this.chkSingleInstance.Size = new System.Drawing.Size(196, 17);
      this.chkSingleInstance.TabIndex = 20;
      this.chkSingleInstance.Text = "PragmaSQL Editor is single instance";
      this.chkSingleInstance.UseVisualStyleBackColor = true;
      this.chkSingleInstance.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 144);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(61, 13);
      this.label2.TabIndex = 19;
      this.label2.Text = "User Folder";
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(532, 142);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 22);
      this.button1.TabIndex = 18;
      this.button1.Text = "Open Folder";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(108, 143);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(418, 20);
      this.textBox1.TabIndex = 17;
      // 
      // chkRestoreWorkspace
      // 
      this.chkRestoreWorkspace.AutoSize = true;
      this.chkRestoreWorkspace.Location = new System.Drawing.Point(14, 12);
      this.chkRestoreWorkspace.Name = "chkRestoreWorkspace";
      this.chkRestoreWorkspace.Size = new System.Drawing.Size(156, 17);
      this.chkRestoreWorkspace.TabIndex = 24;
      this.chkRestoreWorkspace.Text = "Restore workspace on start";
      this.chkRestoreWorkspace.UseVisualStyleBackColor = true;
      this.chkRestoreWorkspace.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // chkStartWithNewScriptEditor
      // 
      this.chkStartWithNewScriptEditor.AutoSize = true;
      this.chkStartWithNewScriptEditor.Location = new System.Drawing.Point(14, 33);
      this.chkStartWithNewScriptEditor.Name = "chkStartWithNewScriptEditor";
      this.chkStartWithNewScriptEditor.Size = new System.Drawing.Size(178, 17);
      this.chkStartWithNewScriptEditor.TabIndex = 30;
      this.chkStartWithNewScriptEditor.Text = "Open empty script editor on start";
      this.chkStartWithNewScriptEditor.UseVisualStyleBackColor = true;
      this.chkStartWithNewScriptEditor.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(646, 308);
      this.tabControl1.TabIndex = 19;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.chkConnectionPooling);
      this.tabPage1.Controls.Add(this.chkStartWithNewScriptEditor);
      this.tabPage1.Controls.Add(this.label5);
      this.tabPage1.Controls.Add(this.textBox2);
      this.tabPage1.Controls.Add(this.button2);
      this.tabPage1.Controls.Add(this.textBox1);
      this.tabPage1.Controls.Add(this.button1);
      this.tabPage1.Controls.Add(this.chkRestoreWorkspace);
      this.tabPage1.Controls.Add(this.label2);
      this.tabPage1.Controls.Add(this.chkSingleInstance);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(638, 282);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "General";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.lblEditPanel);
      this.tabPage2.Controls.Add(this.chkUseCustomPalette);
      this.tabPage2.Controls.Add(this.cmbPaletteMode);
      this.tabPage2.Controls.Add(this.label7);
      this.tabPage2.Controls.Add(this.label6);
      this.tabPage2.Controls.Add(this.cmbDocStyle);
      this.tabPage2.Controls.Add(this.label8);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(638, 282);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Style";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.label3);
      this.tabPage3.Controls.Add(this.cmbPopupBlockerFilterLvl);
      this.tabPage3.Controls.Add(this.btnGoToUrl);
      this.tabPage3.Controls.Add(this.chkShowWebBrowserOnStartup);
      this.tabPage3.Controls.Add(this.label1);
      this.tabPage3.Controls.Add(this.txtHomeUrl);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(638, 282);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Web Browser";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.label4);
      this.tabPage4.Controls.Add(this.nudAutoSaveInterval);
      this.tabPage4.Controls.Add(this.chkAutoSave);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(638, 282);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Auto Recover";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(96, 43);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(43, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "minutes";
      // 
      // nudAutoSaveInterval
      // 
      this.nudAutoSaveInterval.Location = new System.Drawing.Point(30, 38);
      this.nudAutoSaveInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
      this.nudAutoSaveInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudAutoSaveInterval.Name = "nudAutoSaveInterval";
      this.nudAutoSaveInterval.Size = new System.Drawing.Size(63, 20);
      this.nudAutoSaveInterval.TabIndex = 2;
      this.nudAutoSaveInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudAutoSaveInterval.ValueChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // chkAutoSave
      // 
      this.chkAutoSave.AutoSize = true;
      this.chkAutoSave.Location = new System.Drawing.Point(11, 15);
      this.chkAutoSave.Name = "chkAutoSave";
      this.chkAutoSave.Size = new System.Drawing.Size(203, 17);
      this.chkAutoSave.TabIndex = 1;
      this.chkAutoSave.Text = "Perform auto save for recovery every:";
      this.chkAutoSave.UseVisualStyleBackColor = true;
      this.chkAutoSave.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // chkConnectionPooling
      // 
      this.chkConnectionPooling.AutoSize = true;
      this.chkConnectionPooling.Location = new System.Drawing.Point(14, 79);
      this.chkConnectionPooling.Name = "chkConnectionPooling";
      this.chkConnectionPooling.Size = new System.Drawing.Size(138, 17);
      this.chkConnectionPooling.TabIndex = 31;
      this.chkConnectionPooling.Text = "Use connection pooling";
      this.chkConnectionPooling.UseVisualStyleBackColor = true;
      this.chkConnectionPooling.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
      // 
      // ucGeneralOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.Controls.Add(this.tabControl1);
      this.Name = "ucGeneralOptions";
      this.Size = new System.Drawing.Size(646, 308);
      this.Load += new System.EventHandler(this.ucGeneralOptions_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudAutoSaveInterval)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox chkShowWebBrowserOnStartup;
    private System.Windows.Forms.TextBox txtHomeUrl;
    private System.Windows.Forms.Button btnGoToUrl;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.CheckBox chkSingleInstance;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cmbPopupBlockerFilterLvl;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.CheckBox chkRestoreWorkspace;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbDocStyle;
    private System.Windows.Forms.CheckBox chkStartWithNewScriptEditor;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cmbPaletteMode;
    private System.Windows.Forms.CheckBox chkUseCustomPalette;
    private System.Windows.Forms.Label lblEditPanel;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown nudAutoSaveInterval;
    private System.Windows.Forms.CheckBox chkAutoSave;
    private System.Windows.Forms.CheckBox chkConnectionPooling;
  }
}
