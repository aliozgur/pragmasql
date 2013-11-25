namespace PragmaSQL.Core
{
  partial class ucHelpSettings
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.cmbHelpProviderType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabHelp1 = new System.Windows.Forms.TabPage();
      this.label11 = new System.Windows.Forms.Label();
      this.txtHelp1Params = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtHelp1Url = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cmbNavigatorCommands = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tabHelp2 = new System.Windows.Forms.TabPage();
      this.txtHelp2Filter = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtHelp2Collection = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtHelp2Path = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.tabCustomHelp = new System.Windows.Forms.TabPage();
      this.label10 = new System.Windows.Forms.Label();
      this.txtCustomHelpUrl = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.chkUsePragmaSqlWebBrowser = new System.Windows.Forms.CheckBox();
      this.panel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabHelp1.SuspendLayout();
      this.tabHelp2.SuspendLayout();
      this.tabCustomHelp.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.cmbHelpProviderType);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(549, 54);
      this.panel1.TabIndex = 0;
      // 
      // cmbHelpProviderType
      // 
      this.cmbHelpProviderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbHelpProviderType.FormattingEnabled = true;
      this.cmbHelpProviderType.Location = new System.Drawing.Point(10, 25);
      this.cmbHelpProviderType.Name = "cmbHelpProviderType";
      this.cmbHelpProviderType.Size = new System.Drawing.Size(162, 21);
      this.cmbHelpProviderType.Sorted = true;
      this.cmbHelpProviderType.TabIndex = 1;
      this.cmbHelpProviderType.SelectedIndexChanged += new System.EventHandler(this.cmbHelpProviderType_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 7);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(105, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Help Provider In Use";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabHelp1);
      this.tabControl1.Controls.Add(this.tabHelp2);
      this.tabControl1.Controls.Add(this.tabCustomHelp);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 54);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(549, 252);
      this.tabControl1.TabIndex = 1;
      // 
      // tabHelp1
      // 
      this.tabHelp1.Controls.Add(this.label11);
      this.tabHelp1.Controls.Add(this.txtHelp1Params);
      this.tabHelp1.Controls.Add(this.label5);
      this.tabHelp1.Controls.Add(this.txtHelp1Url);
      this.tabHelp1.Controls.Add(this.label3);
      this.tabHelp1.Controls.Add(this.cmbNavigatorCommands);
      this.tabHelp1.Controls.Add(this.label2);
      this.tabHelp1.Location = new System.Drawing.Point(4, 22);
      this.tabHelp1.Name = "tabHelp1";
      this.tabHelp1.Padding = new System.Windows.Forms.Padding(3);
      this.tabHelp1.Size = new System.Drawing.Size(541, 226);
      this.tabHelp1.TabIndex = 0;
      this.tabHelp1.Text = "Help 1x";
      this.tabHelp1.UseVisualStyleBackColor = true;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Cursor = System.Windows.Forms.Cursors.Hand;
      this.label11.Location = new System.Drawing.Point(10, 145);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(320, 13);
      this.label11.TabIndex = 16;
      this.label11.Text = "You can use $WordAtCursor$ to retreive word at text editor cursor.";
      this.label11.DoubleClick += new System.EventHandler(this.label11_DoubleClick);
      // 
      // txtHelp1Params
      // 
      this.txtHelp1Params.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHelp1Params.Location = new System.Drawing.Point(13, 115);
      this.txtHelp1Params.Name = "txtHelp1Params";
      this.txtHelp1Params.Size = new System.Drawing.Size(504, 20);
      this.txtHelp1Params.TabIndex = 7;
      this.txtHelp1Params.TextChanged += new System.EventHandler(this.txtHelp1Url_TextChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(10, 99);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(60, 13);
      this.label5.TabIndex = 6;
      this.label5.Text = "Parameters";
      // 
      // txtHelp1Url
      // 
      this.txtHelp1Url.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHelp1Url.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.txtHelp1Url.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
      this.txtHelp1Url.Location = new System.Drawing.Point(13, 72);
      this.txtHelp1Url.Name = "txtHelp1Url";
      this.txtHelp1Url.Size = new System.Drawing.Size(504, 20);
      this.txtHelp1Url.TabIndex = 5;
      this.txtHelp1Url.TextChanged += new System.EventHandler(this.txtHelp1Url_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(84, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Url or CHM Path";
      // 
      // cmbNavigatorCommands
      // 
      this.cmbNavigatorCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbNavigatorCommands.FormattingEnabled = true;
      this.cmbNavigatorCommands.Location = new System.Drawing.Point(13, 27);
      this.cmbNavigatorCommands.Name = "cmbNavigatorCommands";
      this.cmbNavigatorCommands.Size = new System.Drawing.Size(162, 21);
      this.cmbNavigatorCommands.Sorted = true;
      this.cmbNavigatorCommands.TabIndex = 3;
      this.cmbNavigatorCommands.SelectedIndexChanged += new System.EventHandler(this.cmbHelpProviderType_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 11);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(103, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Navigator Command";
      // 
      // tabHelp2
      // 
      this.tabHelp2.Controls.Add(this.txtHelp2Filter);
      this.tabHelp2.Controls.Add(this.label8);
      this.tabHelp2.Controls.Add(this.txtHelp2Collection);
      this.tabHelp2.Controls.Add(this.label6);
      this.tabHelp2.Controls.Add(this.txtHelp2Path);
      this.tabHelp2.Controls.Add(this.label7);
      this.tabHelp2.Location = new System.Drawing.Point(4, 22);
      this.tabHelp2.Name = "tabHelp2";
      this.tabHelp2.Padding = new System.Windows.Forms.Padding(3);
      this.tabHelp2.Size = new System.Drawing.Size(514, 176);
      this.tabHelp2.TabIndex = 1;
      this.tabHelp2.Text = "Help 2x";
      this.tabHelp2.UseVisualStyleBackColor = true;
      // 
      // txtHelp2Filter
      // 
      this.txtHelp2Filter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHelp2Filter.Location = new System.Drawing.Point(12, 110);
      this.txtHelp2Filter.Name = "txtHelp2Filter";
      this.txtHelp2Filter.Size = new System.Drawing.Size(477, 20);
      this.txtHelp2Filter.TabIndex = 13;
      this.txtHelp2Filter.TextChanged += new System.EventHandler(this.txtHelp1Url_TextChanged);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(9, 94);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(29, 13);
      this.label8.TabIndex = 12;
      this.label8.Text = "Filter";
      // 
      // txtHelp2Collection
      // 
      this.txtHelp2Collection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHelp2Collection.Location = new System.Drawing.Point(12, 68);
      this.txtHelp2Collection.Name = "txtHelp2Collection";
      this.txtHelp2Collection.Size = new System.Drawing.Size(477, 20);
      this.txtHelp2Collection.TabIndex = 11;
      this.txtHelp2Collection.TextChanged += new System.EventHandler(this.txtHelp1Url_TextChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(9, 52);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(78, 13);
      this.label6.TabIndex = 10;
      this.label6.Text = "Help Collection";
      // 
      // txtHelp2Path
      // 
      this.txtHelp2Path.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHelp2Path.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.txtHelp2Path.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
      this.txtHelp2Path.Location = new System.Drawing.Point(12, 25);
      this.txtHelp2Path.Name = "txtHelp2Path";
      this.txtHelp2Path.Size = new System.Drawing.Size(477, 20);
      this.txtHelp2Path.TabIndex = 9;
      this.txtHelp2Path.TextChanged += new System.EventHandler(this.txtHelp1Url_TextChanged);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(9, 9);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(141, 13);
      this.label7.TabIndex = 8;
      this.label7.Text = "MS Document Explorer Path";
      // 
      // tabCustomHelp
      // 
      this.tabCustomHelp.Controls.Add(this.label10);
      this.tabCustomHelp.Controls.Add(this.txtCustomHelpUrl);
      this.tabCustomHelp.Controls.Add(this.label9);
      this.tabCustomHelp.Controls.Add(this.chkUsePragmaSqlWebBrowser);
      this.tabCustomHelp.Location = new System.Drawing.Point(4, 22);
      this.tabCustomHelp.Name = "tabCustomHelp";
      this.tabCustomHelp.Padding = new System.Windows.Forms.Padding(3);
      this.tabCustomHelp.Size = new System.Drawing.Size(514, 176);
      this.tabCustomHelp.TabIndex = 2;
      this.tabCustomHelp.Text = "Custom Online";
      this.tabCustomHelp.UseVisualStyleBackColor = true;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Cursor = System.Windows.Forms.Cursors.Hand;
      this.label10.Location = new System.Drawing.Point(15, 91);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(320, 13);
      this.label10.TabIndex = 15;
      this.label10.Text = "You can use $WordAtCursor$ to retreive word at text editor cursor.";
      this.label10.DoubleClick += new System.EventHandler(this.label10_DoubleClick);
      // 
      // txtCustomHelpUrl
      // 
      this.txtCustomHelpUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCustomHelpUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.txtCustomHelpUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
      this.txtCustomHelpUrl.Location = new System.Drawing.Point(18, 58);
      this.txtCustomHelpUrl.Name = "txtCustomHelpUrl";
      this.txtCustomHelpUrl.Size = new System.Drawing.Size(477, 20);
      this.txtCustomHelpUrl.TabIndex = 7;
      this.txtCustomHelpUrl.TextChanged += new System.EventHandler(this.txtHelp1Url_TextChanged);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(15, 42);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(20, 13);
      this.label9.TabIndex = 6;
      this.label9.Text = "Url";
      // 
      // chkUsePragmaSqlWebBrowser
      // 
      this.chkUsePragmaSqlWebBrowser.AutoSize = true;
      this.chkUsePragmaSqlWebBrowser.Location = new System.Drawing.Point(18, 18);
      this.chkUsePragmaSqlWebBrowser.Name = "chkUsePragmaSqlWebBrowser";
      this.chkUsePragmaSqlWebBrowser.Size = new System.Drawing.Size(172, 17);
      this.chkUsePragmaSqlWebBrowser.TabIndex = 0;
      this.chkUsePragmaSqlWebBrowser.Text = "Use PragmaSQL Web Browser";
      this.chkUsePragmaSqlWebBrowser.UseVisualStyleBackColor = true;
      this.chkUsePragmaSqlWebBrowser.CheckedChanged += new System.EventHandler(this.chkUsePragmaSqlWebBrowser_CheckedChanged);
      // 
      // ucHelpSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.panel1);
      this.Name = "ucHelpSettings";
      this.Size = new System.Drawing.Size(549, 306);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabHelp1.ResumeLayout(false);
      this.tabHelp1.PerformLayout();
      this.tabHelp2.ResumeLayout(false);
      this.tabHelp2.PerformLayout();
      this.tabCustomHelp.ResumeLayout(false);
      this.tabCustomHelp.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabHelp1;
    private System.Windows.Forms.TabPage tabHelp2;
    private System.Windows.Forms.TabPage tabCustomHelp;
    private System.Windows.Forms.ComboBox cmbHelpProviderType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbNavigatorCommands;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtHelp1Url;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtHelp1Params;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtHelp2Collection;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtHelp2Path;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtHelp2Filter;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtCustomHelpUrl;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.CheckBox chkUsePragmaSqlWebBrowser;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label10;
  }
}
