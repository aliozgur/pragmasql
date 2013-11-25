namespace SQLManagement
{
  partial class IndexEdit
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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.grdDestCols = new System.Windows.Forms.DataGridView();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsDestCols = new System.Windows.Forms.BindingSource(this.components);
      this.grdSourceCols = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsSourceCols = new System.Windows.Forms.BindingSource(this.components);
      this.button3 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.chkIgnoreDupKey = new System.Windows.Forms.CheckBox();
      this.chkNoRecompute = new System.Windows.Forms.CheckBox();
      this.chkTempDb = new System.Windows.Forms.CheckBox();
      this.chkPadIndex = new System.Windows.Forms.CheckBox();
      this.cmbFileGroup = new System.Windows.Forms.ComboBox();
      this.txtFillFactor = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.chkClustered = new System.Windows.Forms.CheckBox();
      this.chkUnique = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnDrop = new System.Windows.Forms.Button();
      this.btnAction = new System.Windows.Forms.Button();
      this.btnRename = new System.Windows.Forms.Button();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.gbStatistics = new System.Windows.Forms.GroupBox();
      this.grdStats = new System.Windows.Forms.DataGridView();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsStats = new System.Windows.Forms.BindingSource(this.components);
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdDestCols)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDestCols)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.grdSourceCols)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsSourceCols)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.gbStatistics.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdStats)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsStats)).BeginInit();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
      this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
      this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.gbStatistics);
      this.splitContainer1.Size = new System.Drawing.Size(660, 735);
      this.splitContainer1.SplitterDistance = 514;
      this.splitContainer1.TabIndex = 4;
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.grdDestCols);
      this.groupBox3.Controls.Add(this.grdSourceCols);
      this.groupBox3.Controls.Add(this.button3);
      this.groupBox3.Controls.Add(this.button4);
      this.groupBox3.Controls.Add(this.button2);
      this.groupBox3.Controls.Add(this.button1);
      this.groupBox3.Location = new System.Drawing.Point(5, 290);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(649, 215);
      this.groupBox3.TabIndex = 8;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Columns";
      // 
      // grdDestCols
      // 
      this.grdDestCols.AllowUserToAddRows = false;
      this.grdDestCols.AllowUserToDeleteRows = false;
      this.grdDestCols.AllowUserToResizeRows = false;
      this.grdDestCols.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.grdDestCols.AutoGenerateColumns = false;
      this.grdDestCols.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.grdDestCols.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grdDestCols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdDestCols.ColumnHeadersVisible = false;
      this.grdDestCols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.dataGridViewTextBoxColumn1});
      this.grdDestCols.DataSource = this.bsDestCols;
      this.grdDestCols.Location = new System.Drawing.Point(242, 25);
      this.grdDestCols.Name = "grdDestCols";
      this.grdDestCols.ReadOnly = true;
      this.grdDestCols.RowHeadersVisible = false;
      this.grdDestCols.RowTemplate.Height = 25;
      this.grdDestCols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grdDestCols.Size = new System.Drawing.Size(164, 180);
      this.grdDestCols.TabIndex = 7;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "keyno";
      this.Column2.HeaderText = "keyno";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Visible = false;
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.DataPropertyName = "column";
      this.dataGridViewTextBoxColumn1.HeaderText = "ColName";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // grdSourceCols
      // 
      this.grdSourceCols.AllowUserToAddRows = false;
      this.grdSourceCols.AllowUserToDeleteRows = false;
      this.grdSourceCols.AllowUserToResizeRows = false;
      this.grdSourceCols.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.grdSourceCols.AutoGenerateColumns = false;
      this.grdSourceCols.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.grdSourceCols.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grdSourceCols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdSourceCols.ColumnHeadersVisible = false;
      this.grdSourceCols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
      this.grdSourceCols.DataSource = this.bsSourceCols;
      this.grdSourceCols.GridColor = System.Drawing.SystemColors.Window;
      this.grdSourceCols.Location = new System.Drawing.Point(14, 24);
      this.grdSourceCols.Name = "grdSourceCols";
      this.grdSourceCols.ReadOnly = true;
      this.grdSourceCols.RowHeadersVisible = false;
      this.grdSourceCols.RowTemplate.Height = 25;
      this.grdSourceCols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grdSourceCols.Size = new System.Drawing.Size(164, 180);
      this.grdSourceCols.TabIndex = 6;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "column";
      this.Column1.HeaderText = "ColName";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      // 
      // button3
      // 
      this.button3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button3.Location = new System.Drawing.Point(410, 76);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(47, 25);
      this.button3.TabIndex = 5;
      this.button3.Text = "Down";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button4
      // 
      this.button4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button4.Location = new System.Drawing.Point(410, 47);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(47, 25);
      this.button4.TabIndex = 4;
      this.button4.Text = "Up";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button2.Location = new System.Drawing.Point(187, 76);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(47, 25);
      this.button2.TabIndex = 3;
      this.button2.Text = "<-";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button1.Location = new System.Drawing.Point(187, 47);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(47, 25);
      this.button1.TabIndex = 2;
      this.button1.Text = "->";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.chkIgnoreDupKey);
      this.groupBox2.Controls.Add(this.chkNoRecompute);
      this.groupBox2.Controls.Add(this.chkTempDb);
      this.groupBox2.Controls.Add(this.chkPadIndex);
      this.groupBox2.Controls.Add(this.cmbFileGroup);
      this.groupBox2.Controls.Add(this.txtFillFactor);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.chkClustered);
      this.groupBox2.Controls.Add(this.chkUnique);
      this.groupBox2.Location = new System.Drawing.Point(5, 54);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(650, 234);
      this.groupBox2.TabIndex = 7;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Properties";
      // 
      // chkIgnoreDupKey
      // 
      this.chkIgnoreDupKey.AutoSize = true;
      this.chkIgnoreDupKey.Location = new System.Drawing.Point(16, 198);
      this.chkIgnoreDupKey.Name = "chkIgnoreDupKey";
      this.chkIgnoreDupKey.Size = new System.Drawing.Size(321, 19);
      this.chkIgnoreDupKey.TabIndex = 9;
      this.chkIgnoreDupKey.Text = "Ignore duplicate keys (IGNORE_DUP_KEY - only unique index)";
      this.chkIgnoreDupKey.UseVisualStyleBackColor = true;
      // 
      // chkNoRecompute
      // 
      this.chkNoRecompute.AutoSize = true;
      this.chkNoRecompute.Location = new System.Drawing.Point(16, 173);
      this.chkNoRecompute.Name = "chkNoRecompute";
      this.chkNoRecompute.Size = new System.Drawing.Size(448, 19);
      this.chkNoRecompute.TabIndex = 8;
      this.chkNoRecompute.Text = "Disable automatic recomputation of distribution statistics (STATISTICS_NORECOMPUT" +
          "E)";
      this.chkNoRecompute.UseVisualStyleBackColor = true;
      // 
      // chkTempDb
      // 
      this.chkTempDb.AutoSize = true;
      this.chkTempDb.Enabled = false;
      this.chkTempDb.Location = new System.Drawing.Point(16, 148);
      this.chkTempDb.Name = "chkTempDb";
      this.chkTempDb.Size = new System.Drawing.Size(495, 19);
      this.chkTempDb.TabIndex = 7;
      this.chkTempDb.Text = "Allow intermediate sort results user to build the index to be stored in tempdb (S" +
          "ORT_IN_TEMPDB)";
      this.chkTempDb.UseVisualStyleBackColor = true;
      // 
      // chkPadIndex
      // 
      this.chkPadIndex.AutoSize = true;
      this.chkPadIndex.Location = new System.Drawing.Point(16, 123);
      this.chkPadIndex.Name = "chkPadIndex";
      this.chkPadIndex.Size = new System.Drawing.Size(546, 19);
      this.chkPadIndex.TabIndex = 6;
      this.chkPadIndex.Text = "Leave enough space for a minimum or two rows of the index maximum size in each in" +
          "dex node (PAD_INDEX)";
      this.chkPadIndex.UseVisualStyleBackColor = true;
      // 
      // cmbFileGroup
      // 
      this.cmbFileGroup.FormattingEnabled = true;
      this.cmbFileGroup.Location = new System.Drawing.Point(94, 68);
      this.cmbFileGroup.Name = "cmbFileGroup";
      this.cmbFileGroup.Size = new System.Drawing.Size(125, 23);
      this.cmbFileGroup.TabIndex = 5;
      // 
      // txtFillFactor
      // 
      this.txtFillFactor.Location = new System.Drawing.Point(94, 94);
      this.txtFillFactor.Name = "txtFillFactor";
      this.txtFillFactor.Size = new System.Drawing.Size(125, 23);
      this.txtFillFactor.TabIndex = 4;
      this.txtFillFactor.Text = "0";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(16, 97);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(54, 15);
      this.label3.TabIndex = 3;
      this.label3.Text = "Fill Factor";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(16, 71);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 15);
      this.label2.TabIndex = 2;
      this.label2.Text = "File Group";
      // 
      // chkClustered
      // 
      this.chkClustered.AutoSize = true;
      this.chkClustered.Location = new System.Drawing.Point(16, 43);
      this.chkClustered.Name = "chkClustered";
      this.chkClustered.Size = new System.Drawing.Size(72, 19);
      this.chkClustered.TabIndex = 1;
      this.chkClustered.Text = "Clustered";
      this.chkClustered.UseVisualStyleBackColor = true;
      // 
      // chkUnique
      // 
      this.chkUnique.AutoSize = true;
      this.chkUnique.Location = new System.Drawing.Point(16, 18);
      this.chkUnique.Name = "chkUnique";
      this.chkUnique.Size = new System.Drawing.Size(90, 19);
      this.chkUnique.TabIndex = 0;
      this.chkUnique.Text = "Unique Index";
      this.chkUnique.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.btnDrop);
      this.groupBox1.Controls.Add(this.btnAction);
      this.groupBox1.Controls.Add(this.btnRename);
      this.groupBox1.Controls.Add(this.txtName);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Location = new System.Drawing.Point(5, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(650, 49);
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Definition";
      // 
      // btnDrop
      // 
      this.btnDrop.Location = new System.Drawing.Point(406, 15);
      this.btnDrop.Name = "btnDrop";
      this.btnDrop.Size = new System.Drawing.Size(64, 25);
      this.btnDrop.TabIndex = 4;
      this.btnDrop.Text = "Drop";
      this.btnDrop.UseVisualStyleBackColor = true;
      this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
      // 
      // btnAction
      // 
      this.btnAction.Location = new System.Drawing.Point(472, 15);
      this.btnAction.Name = "btnAction";
      this.btnAction.Size = new System.Drawing.Size(64, 25);
      this.btnAction.TabIndex = 3;
      this.btnAction.Text = "Create";
      this.btnAction.UseVisualStyleBackColor = true;
      this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
      // 
      // btnRename
      // 
      this.btnRename.Enabled = false;
      this.btnRename.Location = new System.Drawing.Point(340, 15);
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(64, 25);
      this.btnRename.TabIndex = 2;
      this.btnRename.Text = "Rename";
      this.btnRename.UseVisualStyleBackColor = true;
      this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(84, 16);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(253, 23);
      this.txtName.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Index Name";
      // 
      // gbStatistics
      // 
      this.gbStatistics.Controls.Add(this.grdStats);
      this.gbStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbStatistics.Location = new System.Drawing.Point(0, 0);
      this.gbStatistics.Name = "gbStatistics";
      this.gbStatistics.Size = new System.Drawing.Size(660, 217);
      this.gbStatistics.TabIndex = 4;
      this.gbStatistics.TabStop = false;
      this.gbStatistics.Text = "Statistics";
      // 
      // grdStats
      // 
      this.grdStats.AllowUserToAddRows = false;
      this.grdStats.AllowUserToDeleteRows = false;
      this.grdStats.AllowUserToResizeRows = false;
      this.grdStats.AutoGenerateColumns = false;
      this.grdStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.grdStats.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grdStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdStats.ColumnHeadersVisible = false;
      this.grdStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4});
      this.grdStats.DataSource = this.bsStats;
      this.grdStats.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdStats.Location = new System.Drawing.Point(3, 19);
      this.grdStats.Name = "grdStats";
      this.grdStats.ReadOnly = true;
      this.grdStats.RowHeadersVisible = false;
      this.grdStats.RowTemplate.Height = 25;
      this.grdStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grdStats.Size = new System.Drawing.Size(654, 195);
      this.grdStats.TabIndex = 8;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "name";
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
      this.Column3.HeaderText = "Name";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 5;
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "value";
      this.Column4.HeaderText = "Value";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.Width = 5;
      // 
      // IndexEdit
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "IndexEdit";
      this.Size = new System.Drawing.Size(660, 735);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdDestCols)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsDestCols)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.grdSourceCols)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsSourceCols)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.gbStatistics.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdStats)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsStats)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox chkIgnoreDupKey;
    private System.Windows.Forms.CheckBox chkNoRecompute;
    private System.Windows.Forms.CheckBox chkTempDb;
    private System.Windows.Forms.CheckBox chkPadIndex;
    private System.Windows.Forms.ComboBox cmbFileGroup;
    private System.Windows.Forms.TextBox txtFillFactor;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chkClustered;
    private System.Windows.Forms.CheckBox chkUnique;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox gbStatistics;
    private System.Windows.Forms.Button btnDrop;
    private System.Windows.Forms.Button btnAction;
    private System.Windows.Forms.Button btnRename;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.DataGridView grdSourceCols;
    private System.Windows.Forms.DataGridView grdDestCols;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.BindingSource bsSourceCols;
    private System.Windows.Forms.BindingSource bsDestCols;
    private System.Windows.Forms.DataGridView grdStats;
    private System.Windows.Forms.BindingSource bsStats;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

  }
}
