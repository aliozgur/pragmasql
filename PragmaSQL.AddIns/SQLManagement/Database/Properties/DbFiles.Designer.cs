namespace SQLManagement
{
  partial class DbFiles
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.bsDataFiles = new System.Windows.Forms.BindingSource(this.components);
			this.bsLogs = new System.Windows.Forms.BindingSource(this.components);
			this.bsSpaceUsage = new System.Windows.Forms.BindingSource(this.components);
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.grdSpaceUsage = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grdDataFiles = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.grdLogs = new System.Windows.Forms.DataGridView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.bsDataFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsLogs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsSpaceUsage)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdSpaceUsage)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdDataFiles)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdLogs)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.grdSpaceUsage);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 293);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(683, 165);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Data Space Usage";
			// 
			// grdSpaceUsage
			// 
			this.grdSpaceUsage.AllowUserToAddRows = false;
			this.grdSpaceUsage.AllowUserToDeleteRows = false;
			this.grdSpaceUsage.AllowUserToOrderColumns = true;
			this.grdSpaceUsage.AllowUserToResizeRows = false;
			this.grdSpaceUsage.AutoGenerateColumns = false;
			this.grdSpaceUsage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.grdSpaceUsage.BackgroundColor = System.Drawing.SystemColors.Window;
			this.grdSpaceUsage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdSpaceUsage.ColumnHeadersVisible = false;
			this.grdSpaceUsage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn7});
			this.grdSpaceUsage.DataSource = this.bsSpaceUsage;
			this.grdSpaceUsage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSpaceUsage.Location = new System.Drawing.Point(3, 16);
			this.grdSpaceUsage.Name = "grdSpaceUsage";
			this.grdSpaceUsage.ReadOnly = true;
			this.grdSpaceUsage.RowHeadersVisible = false;
			this.grdSpaceUsage.RowTemplate.Height = 25;
			this.grdSpaceUsage.Size = new System.Drawing.Size(677, 146);
			this.grdSpaceUsage.TabIndex = 5;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "prop";
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridViewTextBoxColumn1.HeaderText = "Prop";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Width = 5;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "value";
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridViewTextBoxColumn7.HeaderText = "Value";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Width = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBox1.Controls.Add(this.grdDataFiles);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(683, 140);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Data Files";
			// 
			// grdDataFiles
			// 
			this.grdDataFiles.AllowUserToAddRows = false;
			this.grdDataFiles.AllowUserToDeleteRows = false;
			this.grdDataFiles.AllowUserToOrderColumns = true;
			this.grdDataFiles.AllowUserToResizeRows = false;
			this.grdDataFiles.AutoGenerateColumns = false;
			this.grdDataFiles.BackgroundColor = System.Drawing.SystemColors.Window;
			this.grdDataFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdDataFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
			this.grdDataFiles.DataSource = this.bsDataFiles;
			this.grdDataFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdDataFiles.Location = new System.Drawing.Point(3, 16);
			this.grdDataFiles.Name = "grdDataFiles";
			this.grdDataFiles.ReadOnly = true;
			this.grdDataFiles.RowHeadersVisible = false;
			this.grdDataFiles.RowTemplate.Height = 25;
			this.grdDataFiles.Size = new System.Drawing.Size(677, 121);
			this.grdDataFiles.TabIndex = 3;
			this.grdDataFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDataFiles_CellContentClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.grdLogs);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 143);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(683, 147);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Transaction Logs";
			// 
			// grdLogs
			// 
			this.grdLogs.AllowUserToAddRows = false;
			this.grdLogs.AllowUserToDeleteRows = false;
			this.grdLogs.AllowUserToOrderColumns = true;
			this.grdLogs.AllowUserToResizeRows = false;
			this.grdLogs.AutoGenerateColumns = false;
			this.grdLogs.BackgroundColor = System.Drawing.SystemColors.Window;
			this.grdLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewButtonColumn1});
			this.grdLogs.DataSource = this.bsLogs;
			this.grdLogs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLogs.Location = new System.Drawing.Point(3, 16);
			this.grdLogs.Name = "grdLogs";
			this.grdLogs.ReadOnly = true;
			this.grdLogs.RowHeadersVisible = false;
			this.grdLogs.RowTemplate.Height = 25;
			this.grdLogs.Size = new System.Drawing.Size(677, 128);
			this.grdLogs.TabIndex = 6;
			this.grdLogs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdLogs_CellContentClick);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 140);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(683, 3);
			this.splitter1.TabIndex = 17;
			this.splitter1.TabStop = false;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(0, 290);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(683, 3);
			this.splitter2.TabIndex = 18;
			this.splitter2.TabStop = false;
			// 
			// Column1
			// 
			this.Column1.DataPropertyName = "groupname";
			this.Column1.Frozen = true;
			this.Column1.HeaderText = "Group";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Width = 61;
			// 
			// Column2
			// 
			this.Column2.DataPropertyName = "name";
			this.Column2.Frozen = true;
			this.Column2.HeaderText = "Name";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			this.Column2.Width = 60;
			// 
			// Column3
			// 
			this.Column3.DataPropertyName = "filename";
			this.Column3.Frozen = true;
			this.Column3.HeaderText = "Physical Name";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Width = 120;
			// 
			// Column4
			// 
			this.Column4.DataPropertyName = "size";
			this.Column4.Frozen = true;
			this.Column4.HeaderText = "Size (KB)";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			this.Column4.Width = 80;
			// 
			// Column5
			// 
			this.Column5.DataPropertyName = "maxsize";
			this.Column5.Frozen = true;
			this.Column5.HeaderText = "Max. Size";
			this.Column5.Name = "Column5";
			this.Column5.ReadOnly = true;
			// 
			// Column6
			// 
			this.Column6.DataPropertyName = "growth";
			this.Column6.Frozen = true;
			this.Column6.HeaderText = "File Growth";
			this.Column6.Name = "Column6";
			this.Column6.ReadOnly = true;
			// 
			// Column7
			// 
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			this.Column7.DefaultCellStyle = dataGridViewCellStyle3;
			this.Column7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Column7.Frozen = true;
			this.Column7.HeaderText = "";
			this.Column7.Name = "Column7";
			this.Column7.ReadOnly = true;
			this.Column7.Text = "Edit";
			this.Column7.UseColumnTextForButtonValue = true;
			this.Column7.Width = 50;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
			this.dataGridViewTextBoxColumn2.Frozen = true;
			this.dataGridViewTextBoxColumn2.HeaderText = "Name";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "filename";
			this.dataGridViewTextBoxColumn3.Frozen = true;
			this.dataGridViewTextBoxColumn3.HeaderText = "Physical Name";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 150;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "size";
			this.dataGridViewTextBoxColumn4.Frozen = true;
			this.dataGridViewTextBoxColumn4.HeaderText = "Size (KB)";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "maxsize";
			this.dataGridViewTextBoxColumn5.Frozen = true;
			this.dataGridViewTextBoxColumn5.HeaderText = "Max. Size";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "growth";
			this.dataGridViewTextBoxColumn6.Frozen = true;
			this.dataGridViewTextBoxColumn6.HeaderText = "File Growth";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			// 
			// dataGridViewButtonColumn1
			// 
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewButtonColumn1.Frozen = true;
			this.dataGridViewButtonColumn1.HeaderText = "";
			this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
			this.dataGridViewButtonColumn1.ReadOnly = true;
			this.dataGridViewButtonColumn1.Text = "Edit";
			this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
			this.dataGridViewButtonColumn1.Width = 50;
			// 
			// DbFiles
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.groupBox1);
			this.Name = "DbFiles";
			this.Size = new System.Drawing.Size(683, 458);
			((System.ComponentModel.ISupportInitialize)(this.bsDataFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsLogs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsSpaceUsage)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdSpaceUsage)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdDataFiles)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdLogs)).EndInit();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource bsDataFiles;
    private System.Windows.Forms.BindingSource bsLogs;
    private System.Windows.Forms.BindingSource bsSpaceUsage;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.DataGridView grdSpaceUsage;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.DataGridView grdDataFiles;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.DataGridView grdLogs;
    private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewButtonColumn Column7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;

  }
}
