namespace SQLManagement
{
  partial class frmServerInfo
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServerInfo));
			this.bs = new System.Windows.Forms.BindingSource(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.grd = new System.Windows.Forms.DataGridView();
			this.colParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.lblHostName = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblServiceName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblTotalLocks = new System.Windows.Forms.Label();
			this.lblOpenTransactions = new System.Windows.Forms.Label();
			this.lblBlockedProcesses = new System.Windows.Forms.Label();
			this.lblConnectedProcesses = new System.Windows.Forms.Label();
			this.lblNumberDatabases = new System.Windows.Forms.Label();
			this.lblServerStartTime = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(124, 26);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem,
            this.toolStripMenuItem1,
            this.refreshToolStripMenuItem1});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(167, 98);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// closeAllToolStripMenuItem
			// 
			this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
			this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.closeAllToolStripMenuItem.Text = "Close All";
			this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
			// 
			// closeAllButThisToolStripMenuItem
			// 
			this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
			this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.closeAllButThisToolStripMenuItem.Text = "Close All But This";
			this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
			// 
			// refreshToolStripMenuItem1
			// 
			this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
			this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
			this.refreshToolStripMenuItem1.Text = "Refresh";
			this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.ContextMenuStrip = this.contextMenuStrip1;
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(714, 553);
			this.panel1.TabIndex = 3;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox2.ContextMenuStrip = this.contextMenuStrip1;
			this.groupBox2.Controls.Add(this.grd);
			this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.groupBox2.ForeColor = System.Drawing.Color.Navy;
			this.groupBox2.Location = new System.Drawing.Point(12, 215);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(690, 304);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Version Info";
			// 
			// grd
			// 
			this.grd.AllowUserToAddRows = false;
			this.grd.AllowUserToDeleteRows = false;
			this.grd.AllowUserToResizeRows = false;
			this.grd.AutoGenerateColumns = false;
			this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
			this.grd.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.grd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grd.ColumnHeadersVisible = false;
			this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParam,
            this.colValue});
			this.grd.ContextMenuStrip = this.contextMenuStrip1;
			this.grd.DataSource = this.bs;
			this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd.Location = new System.Drawing.Point(3, 17);
			this.grd.Name = "grd";
			this.grd.ReadOnly = true;
			this.grd.RowHeadersVisible = false;
			this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.grd.RowTemplate.Height = 25;
			this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.grd.Size = new System.Drawing.Size(684, 284);
			this.grd.TabIndex = 5;
			// 
			// colParam
			// 
			this.colParam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colParam.DataPropertyName = "Name";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.colParam.DefaultCellStyle = dataGridViewCellStyle1;
			this.colParam.Frozen = true;
			this.colParam.HeaderText = "Parameter";
			this.colParam.Name = "colParam";
			this.colParam.ReadOnly = true;
			this.colParam.Width = 5;
			// 
			// colValue
			// 
			this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colValue.DataPropertyName = "Character_Value";
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.NullValue = "None";
			this.colValue.DefaultCellStyle = dataGridViewCellStyle2;
			this.colValue.HeaderText = "Value";
			this.colValue.Name = "colValue";
			this.colValue.ReadOnly = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
			this.groupBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.lblHostName);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.lblServiceName);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.lblTotalLocks);
			this.groupBox1.Controls.Add(this.lblOpenTransactions);
			this.groupBox1.Controls.Add(this.lblBlockedProcesses);
			this.groupBox1.Controls.Add(this.lblConnectedProcesses);
			this.groupBox1.Controls.Add(this.lblNumberDatabases);
			this.groupBox1.Controls.Add(this.lblServerStartTime);
			this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.groupBox1.ForeColor = System.Drawing.Color.Navy;
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(690, 191);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Summary";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label9.ForeColor = System.Drawing.Color.Black;
			this.label9.Location = new System.Drawing.Point(15, 37);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(71, 13);
			this.label9.TabIndex = 26;
			this.label9.Text = "Host Name:";
			// 
			// lblHostName
			// 
			this.lblHostName.AutoSize = true;
			this.lblHostName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblHostName.ForeColor = System.Drawing.Color.Black;
			this.lblHostName.Location = new System.Drawing.Point(178, 37);
			this.lblHostName.Name = "lblHostName";
			this.lblHostName.Size = new System.Drawing.Size(19, 13);
			this.lblHostName.TabIndex = 25;
			this.lblHostName.Text = "...";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label7.ForeColor = System.Drawing.Color.Black;
			this.label7.Location = new System.Drawing.Point(15, 17);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(87, 13);
			this.label7.TabIndex = 24;
			this.label7.Text = "Service Name:";
			// 
			// lblServiceName
			// 
			this.lblServiceName.AutoSize = true;
			this.lblServiceName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblServiceName.ForeColor = System.Drawing.Color.Black;
			this.lblServiceName.Location = new System.Drawing.Point(178, 17);
			this.lblServiceName.Name = "lblServiceName";
			this.lblServiceName.Size = new System.Drawing.Size(19, 13);
			this.lblServiceName.TabIndex = 23;
			this.lblServiceName.Text = "...";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(15, 164);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 13);
			this.label1.TabIndex = 22;
			this.label1.Text = "Total Locks:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(15, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 13);
			this.label2.TabIndex = 21;
			this.label2.Text = "Open transactions:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(15, 122);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 13);
			this.label3.TabIndex = 20;
			this.label3.Text = "Blocked processes:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(15, 100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(130, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "Connected processes:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Location = new System.Drawing.Point(15, 79);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(130, 13);
			this.label5.TabIndex = 18;
			this.label5.Text = "Number of databases:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label6.ForeColor = System.Drawing.Color.Black;
			this.label6.Location = new System.Drawing.Point(15, 58);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Start Time:";
			// 
			// lblTotalLocks
			// 
			this.lblTotalLocks.AutoSize = true;
			this.lblTotalLocks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblTotalLocks.ForeColor = System.Drawing.Color.Black;
			this.lblTotalLocks.Location = new System.Drawing.Point(178, 164);
			this.lblTotalLocks.Name = "lblTotalLocks";
			this.lblTotalLocks.Size = new System.Drawing.Size(19, 13);
			this.lblTotalLocks.TabIndex = 16;
			this.lblTotalLocks.Text = "...";
			// 
			// lblOpenTransactions
			// 
			this.lblOpenTransactions.AutoSize = true;
			this.lblOpenTransactions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblOpenTransactions.ForeColor = System.Drawing.Color.Black;
			this.lblOpenTransactions.Location = new System.Drawing.Point(178, 143);
			this.lblOpenTransactions.Name = "lblOpenTransactions";
			this.lblOpenTransactions.Size = new System.Drawing.Size(19, 13);
			this.lblOpenTransactions.TabIndex = 15;
			this.lblOpenTransactions.Text = "...";
			// 
			// lblBlockedProcesses
			// 
			this.lblBlockedProcesses.AutoSize = true;
			this.lblBlockedProcesses.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblBlockedProcesses.ForeColor = System.Drawing.Color.Black;
			this.lblBlockedProcesses.Location = new System.Drawing.Point(178, 122);
			this.lblBlockedProcesses.Name = "lblBlockedProcesses";
			this.lblBlockedProcesses.Size = new System.Drawing.Size(19, 13);
			this.lblBlockedProcesses.TabIndex = 14;
			this.lblBlockedProcesses.Text = "...";
			// 
			// lblConnectedProcesses
			// 
			this.lblConnectedProcesses.AutoSize = true;
			this.lblConnectedProcesses.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblConnectedProcesses.ForeColor = System.Drawing.Color.Black;
			this.lblConnectedProcesses.Location = new System.Drawing.Point(178, 100);
			this.lblConnectedProcesses.Name = "lblConnectedProcesses";
			this.lblConnectedProcesses.Size = new System.Drawing.Size(19, 13);
			this.lblConnectedProcesses.TabIndex = 13;
			this.lblConnectedProcesses.Text = "...";
			// 
			// lblNumberDatabases
			// 
			this.lblNumberDatabases.AutoSize = true;
			this.lblNumberDatabases.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblNumberDatabases.ForeColor = System.Drawing.Color.Black;
			this.lblNumberDatabases.Location = new System.Drawing.Point(178, 79);
			this.lblNumberDatabases.Name = "lblNumberDatabases";
			this.lblNumberDatabases.Size = new System.Drawing.Size(19, 13);
			this.lblNumberDatabases.TabIndex = 12;
			this.lblNumberDatabases.Text = "...";
			// 
			// lblServerStartTime
			// 
			this.lblServerStartTime.AutoSize = true;
			this.lblServerStartTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblServerStartTime.ForeColor = System.Drawing.Color.Black;
			this.lblServerStartTime.Location = new System.Drawing.Point(178, 58);
			this.lblServerStartTime.Name = "lblServerStartTime";
			this.lblServerStartTime.Size = new System.Drawing.Size(19, 13);
			this.lblServerStartTime.TabIndex = 11;
			this.lblServerStartTime.Text = "...";
			// 
			// frmServerInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(714, 553);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmServerInfo";
			this.ShowInTaskbar = false;
			this.TabPageContextMenuStrip = this.contextMenuStrip2;
			this.TabText = "Server Info";
			this.Text = "Server Info";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmServerInfo_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource bs;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label lblTotalLocks;
    private System.Windows.Forms.Label lblOpenTransactions;
    private System.Windows.Forms.Label lblBlockedProcesses;
    private System.Windows.Forms.Label lblConnectedProcesses;
    private System.Windows.Forms.Label lblNumberDatabases;
    private System.Windows.Forms.Label lblServerStartTime;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label lblHostName;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label lblServiceName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colParam;
    private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
  }
}