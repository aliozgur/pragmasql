namespace PragmaSQL
{
  partial class frmDBObjectScripter
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBObjectScripter));
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnStop = new System.Windows.Forms.Button();
      this.btnStart = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.rdToFolder = new System.Windows.Forms.RadioButton();
      this.rdToFile = new System.Windows.Forms.RadioButton();
      this.rdToWindow = new System.Windows.Forms.RadioButton();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.lblStatus = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.txtSearchText = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmbSearchType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.button3 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.lv = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnStop);
      this.panel1.Controls.Add(this.btnStart);
      this.panel1.Controls.Add(this.btnClose);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 537);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(437, 44);
      this.panel1.TabIndex = 0;
      // 
      // btnStop
      // 
      this.btnStop.Enabled = false;
      this.btnStop.Location = new System.Drawing.Point(89, 8);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(77, 29);
      this.btnStop.TabIndex = 2;
      this.btnStop.Text = "Stop";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // btnStart
      // 
      this.btnStart.Location = new System.Drawing.Point(6, 7);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(77, 29);
      this.btnStart.TabIndex = 1;
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(350, 8);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(77, 29);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.rdToFolder);
      this.groupBox1.Controls.Add(this.rdToFile);
      this.groupBox1.Controls.Add(this.rdToWindow);
      this.groupBox1.Location = new System.Drawing.Point(10, 7);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(417, 96);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Script Destination";
      // 
      // rdToFolder
      // 
      this.rdToFolder.AutoSize = true;
      this.rdToFolder.Location = new System.Drawing.Point(10, 67);
      this.rdToFolder.Name = "rdToFolder";
      this.rdToFolder.Size = new System.Drawing.Size(96, 19);
      this.rdToFolder.TabIndex = 2;
      this.rdToFolder.Text = "Script to folder";
      this.rdToFolder.UseVisualStyleBackColor = true;
      this.rdToFolder.CheckedChanged += new System.EventHandler(this.rdToFolder_CheckedChanged);
      // 
      // rdToFile
      // 
      this.rdToFile.AutoSize = true;
      this.rdToFile.Location = new System.Drawing.Point(10, 44);
      this.rdToFile.Name = "rdToFile";
      this.rdToFile.Size = new System.Drawing.Size(83, 19);
      this.rdToFile.TabIndex = 1;
      this.rdToFile.Text = "Script to file";
      this.rdToFile.UseVisualStyleBackColor = true;
      this.rdToFile.CheckedChanged += new System.EventHandler(this.rdToFile_CheckedChanged);
      // 
      // rdToWindow
      // 
      this.rdToWindow.AutoSize = true;
      this.rdToWindow.Checked = true;
      this.rdToWindow.Location = new System.Drawing.Point(10, 19);
      this.rdToWindow.Name = "rdToWindow";
      this.rdToWindow.Size = new System.Drawing.Size(128, 19);
      this.rdToWindow.TabIndex = 0;
      this.rdToWindow.TabStop = true;
      this.rdToWindow.Text = "Script to new window";
      this.rdToWindow.UseVisualStyleBackColor = true;
      this.rdToWindow.CheckedChanged += new System.EventHandler(this.rdToWindow_CheckedChanged);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "sql";
      this.saveFileDialog1.Filter = "SQL Script|*.sql|All Files|*.*";
      // 
      // lblStatus
      // 
      this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblStatus.AutoSize = true;
      this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
      this.lblStatus.Location = new System.Drawing.Point(8, 516);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(49, 16);
      this.lblStatus.TabIndex = 3;
      this.lblStatus.Text = "Status:";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.txtSearchText);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.cmbSearchType);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Location = new System.Drawing.Point(10, 106);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(417, 77);
      this.groupBox2.TabIndex = 7;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Object Search";
      // 
      // txtSearchText
      // 
      this.txtSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSearchText.Location = new System.Drawing.Point(133, 40);
      this.txtSearchText.Name = "txtSearchText";
      this.txtSearchText.Size = new System.Drawing.Size(278, 23);
      this.txtSearchText.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(130, 23);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(111, 15);
      this.label2.TabIndex = 13;
      this.label2.Text = "Object Name Criteria";
      // 
      // cmbSearchType
      // 
      this.cmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbSearchType.FormattingEnabled = true;
      this.cmbSearchType.Location = new System.Drawing.Point(10, 40);
      this.cmbSearchType.Name = "cmbSearchType";
      this.cmbSearchType.Size = new System.Drawing.Size(121, 23);
      this.cmbSearchType.TabIndex = 12;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 15);
      this.label1.TabIndex = 11;
      this.label1.Text = "Search Type";
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.button3);
      this.groupBox3.Controls.Add(this.button2);
      this.groupBox3.Controls.Add(this.button1);
      this.groupBox3.Controls.Add(this.lv);
      this.groupBox3.Location = new System.Drawing.Point(10, 185);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(415, 326);
      this.groupBox3.TabIndex = 8;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Object Types";
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(176, 22);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(81, 28);
      this.button3.TabIndex = 10;
      this.button3.Text = "Toggle Check";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button2.Location = new System.Drawing.Point(92, 22);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(81, 28);
      this.button2.TabIndex = 9;
      this.button2.Text = "Uncheck All";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(8, 22);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(81, 28);
      this.button1.TabIndex = 8;
      this.button1.Text = "Check All";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // lv
      // 
      this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lv.CheckBoxes = true;
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.lv.Location = new System.Drawing.Point(9, 58);
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(398, 254);
      this.lv.TabIndex = 7;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "";
      // 
      // frmDBObjectScripter
      // 
      this.AcceptButton = this.btnStart;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(437, 581);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmDBObjectScripter";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Script Database Objects";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDBObjectScripter_FormClosing);
      this.panel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton rdToFolder;
    private System.Windows.Forms.RadioButton rdToFile;
    private System.Windows.Forms.RadioButton rdToWindow;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.TextBox txtSearchText;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cmbSearchType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader columnHeader1;
  }
}