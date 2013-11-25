namespace PragmaSQL
{
  partial class frmCrudGenerator
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrudGenerator));
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnGenerate = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.button1 = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.txtTableName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtGroup = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtPrefix = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.grd = new System.Windows.Forms.DataGridView();
      this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGenerate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.colPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnGenerate);
      this.panel1.Controls.Add(this.btnClose);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 356);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(549, 43);
      this.panel1.TabIndex = 0;
      // 
      // btnGenerate
      // 
      this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGenerate.Location = new System.Drawing.Point(378, 8);
      this.btnGenerate.Name = "btnGenerate";
      this.btnGenerate.Size = new System.Drawing.Size(75, 28);
      this.btnGenerate.TabIndex = 1;
      this.btnGenerate.Text = "Generate";
      this.btnGenerate.UseVisualStyleBackColor = true;
      this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(459, 8);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 28);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.button1);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.txtTableName);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.txtGroup);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.txtPrefix);
      this.groupBox1.Location = new System.Drawing.Point(9, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(528, 86);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Naming Template";
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(443, 37);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 28);
      this.button1.TabIndex = 6;
      this.button1.Text = "Apply To All";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(165, 23);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(66, 15);
      this.label3.TabIndex = 5;
      this.label3.Text = "Table Name";
      // 
      // txtTableName
      // 
      this.txtTableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTableName.Location = new System.Drawing.Point(169, 40);
      this.txtTableName.Name = "txtTableName";
      this.txtTableName.Size = new System.Drawing.Size(272, 23);
      this.txtTableName.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(72, 23);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(37, 15);
      this.label2.TabIndex = 3;
      this.label2.Text = "Group";
      // 
      // txtGroup
      // 
      this.txtGroup.Location = new System.Drawing.Point(75, 40);
      this.txtGroup.Name = "txtGroup";
      this.txtGroup.Size = new System.Drawing.Size(90, 23);
      this.txtGroup.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 23);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "Prefix";
      // 
      // txtPrefix
      // 
      this.txtPrefix.Location = new System.Drawing.Point(12, 40);
      this.txtPrefix.Name = "txtPrefix";
      this.txtPrefix.Size = new System.Drawing.Size(61, 23);
      this.txtPrefix.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.grd);
      this.groupBox2.Location = new System.Drawing.Point(9, 104);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(528, 237);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "CRUD Procedures";
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colGenerate,
            this.colPrefix,
            this.colGroup,
            this.colTableName,
            this.colOperation});
      this.grd.Location = new System.Drawing.Point(15, 22);
      this.grd.Name = "grd";
      this.grd.RowHeadersVisible = false;
      this.grd.RowTemplate.Height = 25;
      this.grd.Size = new System.Drawing.Size(498, 200);
      this.grd.TabIndex = 30;
      // 
      // colType
      // 
      this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colType.DataPropertyName = "Type";
      this.colType.HeaderText = "Type";
      this.colType.Name = "colType";
      this.colType.ReadOnly = true;
      // 
      // colGenerate
      // 
      this.colGenerate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colGenerate.DataPropertyName = "Generate";
      this.colGenerate.HeaderText = "Generate?";
      this.colGenerate.Name = "colGenerate";
      // 
      // colPrefix
      // 
      this.colPrefix.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colPrefix.DataPropertyName = "Prefix";
      this.colPrefix.HeaderText = "Prefix";
      this.colPrefix.Name = "colPrefix";
      // 
      // colGroup
      // 
      this.colGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colGroup.DataPropertyName = "Group";
      this.colGroup.HeaderText = "Group";
      this.colGroup.Name = "colGroup";
      // 
      // colTableName
      // 
      this.colTableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colTableName.DataPropertyName = "TableName";
      this.colTableName.HeaderText = "TableName";
      this.colTableName.Name = "colTableName";
      // 
      // colOperation
      // 
      this.colOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colOperation.DataPropertyName = "Operation";
      this.colOperation.HeaderText = "Operation";
      this.colOperation.Name = "colOperation";
      // 
      // frmCrudGenerator
      // 
      this.AcceptButton = this.btnGenerate;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnClose;
      this.ClientSize = new System.Drawing.Size(549, 399);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.panel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmCrudGenerator";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Generate CRUD Procedures";
      this.panel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtTableName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtGroup;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtPrefix;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.DataGridViewTextBoxColumn colType;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colGenerate;
    private System.Windows.Forms.DataGridViewTextBoxColumn colPrefix;
    private System.Windows.Forms.DataGridViewTextBoxColumn colGroup;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTableName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colOperation;
  }
}