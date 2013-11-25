namespace HtmlHelp2.Environment
{
  partial class HtmlHelp2OptionsDlg
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlHelp2OptionsDlg));
			this.help2Collections = new System.Windows.Forms.ComboBox();
			this.tocPictures = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbPadType = new System.Windows.Forms.ComboBox();
			this.chkCancelDefault = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkEnableForKeywordHelp = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// help2Collections
			// 
			this.help2Collections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.help2Collections.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.help2Collections.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.help2Collections.FormattingEnabled = true;
			this.help2Collections.Location = new System.Drawing.Point(9, 25);
			this.help2Collections.Name = "help2Collections";
			this.help2Collections.Size = new System.Drawing.Size(423, 21);
			this.help2Collections.TabIndex = 0;
			this.toolTip1.SetToolTip(this.help2Collections, "MS Help2 collection to be used.");
			// 
			// tocPictures
			// 
			this.tocPictures.AutoSize = true;
			this.tocPictures.Location = new System.Drawing.Point(8, 133);
			this.tocPictures.Name = "tocPictures";
			this.tocPictures.Size = new System.Drawing.Size(161, 17);
			this.tocPictures.TabIndex = 1;
			this.tocPictures.Text = "Show pictures in the content";
			this.tocPictures.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Help Collections";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(355, 159);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 26);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(274, 159);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 26);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Apply";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Keyword help (F1) pad";
			// 
			// cmbPadType
			// 
			this.cmbPadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPadType.FormattingEnabled = true;
			this.cmbPadType.Location = new System.Drawing.Point(9, 67);
			this.cmbPadType.Name = "cmbPadType";
			this.cmbPadType.Size = new System.Drawing.Size(185, 21);
			this.cmbPadType.Sorted = true;
			this.cmbPadType.TabIndex = 5;
			this.toolTip1.SetToolTip(this.cmbPadType, "Specify which help pad will be used by default when F1 is pressed.");
			// 
			// chkCancelDefault
			// 
			this.chkCancelDefault.AutoSize = true;
			this.chkCancelDefault.Location = new System.Drawing.Point(8, 114);
			this.chkCancelDefault.Name = "chkCancelDefault";
			this.chkCancelDefault.Size = new System.Drawing.Size(167, 17);
			this.chkCancelDefault.TabIndex = 7;
			this.chkCancelDefault.Text = "Cancel default help behaviour";
			this.toolTip1.SetToolTip(this.chkCancelDefault, "Cancels execution of default PragmaSQL help provider selected in application. \r\nT" +
							"hus only integrated help is used");
			this.chkCancelDefault.UseVisualStyleBackColor = true;
			// 
			// toolTip1
			// 
			this.toolTip1.IsBalloon = true;
			// 
			// chkEnableForKeywordHelp
			// 
			this.chkEnableForKeywordHelp.AutoSize = true;
			this.chkEnableForKeywordHelp.Location = new System.Drawing.Point(8, 93);
			this.chkEnableForKeywordHelp.Name = "chkEnableForKeywordHelp";
			this.chkEnableForKeywordHelp.Size = new System.Drawing.Size(167, 17);
			this.chkEnableForKeywordHelp.TabIndex = 8;
			this.chkEnableForKeywordHelp.Text = "Enabled for keyword help (F1)";
			this.toolTip1.SetToolTip(this.chkEnableForKeywordHelp, "If checked integrated Help2 will be used for keyword help\r\nwhenever F1 is hit.\r\n");
			this.chkEnableForKeywordHelp.UseVisualStyleBackColor = true;
			// 
			// HtmlHelp2OptionsDlg
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(444, 195);
			this.Controls.Add(this.chkEnableForKeywordHelp);
			this.Controls.Add(this.chkCancelDefault);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbPadType);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tocPictures);
			this.Controls.Add(this.help2Collections);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HtmlHelp2OptionsDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Help2 Options";
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox help2Collections;
    private System.Windows.Forms.CheckBox tocPictures;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbPadType;
    private System.Windows.Forms.CheckBox chkCancelDefault;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.CheckBox chkEnableForKeywordHelp;
  }
}