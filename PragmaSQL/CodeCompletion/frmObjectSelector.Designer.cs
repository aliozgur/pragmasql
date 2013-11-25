namespace PragmaSQL
{
  
  partial class frmObjectSelector
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectSelector));
      this.panel1 = new System.Windows.Forms.Panel();
      this.grd = new System.Windows.Forms.DataGridView();
      this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colParentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel5 = new System.Windows.Forms.Panel();
      this.button1 = new System.Windows.Forms.Button();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.panel3 = new System.Windows.Forms.Panel();
      this.panelBottom = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.panel4 = new System.Windows.Forms.Panel();
      this.button2 = new System.Windows.Forms.Button();
      this.txtCustomFilter = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.panel5.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panelBottom.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel4.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.grd);
      this.panel1.Controls.Add(this.panel5);
      this.panel1.Controls.Add(this.panel3);
      this.panel1.Controls.Add(this.panel4);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(282, 483);
      this.panel1.TabIndex = 0;
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.grd.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.grd.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.SteelBlue;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colDisplayName,
            this.colName,
            this.colDataType,
            this.colParentName});
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.grd.GridColor = System.Drawing.SystemColors.Window;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowHeadersVisible = false;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.ShowEditingIcon = false;
      this.grd.Size = new System.Drawing.Size(282, 374);
      this.grd.StandardTab = true;
      this.grd.TabIndex = 1;
      this.grd.DoubleClick += new System.EventHandler(this.grdObjects_DoubleClick);
      this.grd.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdObjects_CellPainting);
      this.grd.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grd_DataError);
      this.grd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdObjects_KeyDown);
      this.grd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdObjects_KeyPress);
      // 
      // colType
      // 
      this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colType.DataPropertyName = "Type";
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
      this.colType.DefaultCellStyle = dataGridViewCellStyle2;
      this.colType.FillWeight = 50F;
      this.colType.Frozen = true;
      this.colType.HeaderText = "Type";
      this.colType.Name = "colType";
      this.colType.ReadOnly = true;
      this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colType.Width = 40;
      // 
      // colDisplayName
      // 
      this.colDisplayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colDisplayName.DataPropertyName = "DiplayName";
      this.colDisplayName.HeaderText = "Object Name";
      this.colDisplayName.Name = "colDisplayName";
      this.colDisplayName.ReadOnly = true;
      this.colDisplayName.Visible = false;
      this.colDisplayName.Width = 130;
      // 
      // colName
      // 
      this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colName.DataPropertyName = "Name";
      this.colName.FillWeight = 110F;
      this.colName.HeaderText = "Object Name";
      this.colName.Name = "colName";
      this.colName.ReadOnly = true;
      this.colName.Width = 165;
      // 
      // colDataType
      // 
      this.colDataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colDataType.DataPropertyName = "QualifiedDataType";
      this.colDataType.HeaderText = "DataType";
      this.colDataType.Name = "colDataType";
      this.colDataType.ReadOnly = true;
      this.colDataType.Width = 70;
      // 
      // colParentName
      // 
      this.colParentName.DataPropertyName = "ParentName";
      this.colParentName.HeaderText = "Parent";
      this.colParentName.Name = "colParentName";
      this.colParentName.ReadOnly = true;
      // 
      // panel5
      // 
      this.panel5.BackColor = System.Drawing.SystemColors.Control;
      this.panel5.Controls.Add(this.button1);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel5.Location = new System.Drawing.Point(0, 374);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(282, 28);
      this.panel5.TabIndex = 9;
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.BackColor = System.Drawing.SystemColors.Control;
      this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button1.ImageIndex = 13;
      this.button1.ImageList = this.imageList1;
      this.button1.Location = new System.Drawing.Point(1, 1);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(278, 26);
      this.button1.TabIndex = 4;
      this.button1.TabStop = false;
      this.button1.Text = "Refresh Object List";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.imageList1.Images.SetKeyName(0, "database.bmp");
      this.imageList1.Images.SetKeyName(1, "User.bmp");
      this.imageList1.Images.SetKeyName(2, "DataSet_TableView.bmp");
      this.imageList1.Images.SetKeyName(3, "View1.ico");
      this.imageList1.Images.SetKeyName(4, "Proc2.ico");
      this.imageList1.Images.SetKeyName(5, "function.ico");
      this.imageList1.Images.SetKeyName(6, "grid select column.bmp");
      this.imageList1.Images.SetKeyName(7, "paramin.ico");
      this.imageList1.Images.SetKeyName(8, "keys.bmp");
      this.imageList1.Images.SetKeyName(9, "Control_TextBox.bmp");
      this.imageList1.Images.SetKeyName(10, "refresh.bmp");
      this.imageList1.Images.SetKeyName(11, "Autoformat Grid.bmp");
      this.imageList1.Images.SetKeyName(12, "paramin.ico");
      this.imageList1.Images.SetKeyName(13, "Refresh.bmp");
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.panelBottom);
      this.panel3.Controls.Add(this.panel2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 402);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(282, 55);
      this.panel3.TabIndex = 7;
      this.panel3.Visible = false;
      // 
      // panelBottom
      // 
      this.panelBottom.BackColor = System.Drawing.SystemColors.Control;
      this.panelBottom.Controls.Add(this.label1);
      this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelBottom.Location = new System.Drawing.Point(0, 31);
      this.panelBottom.Name = "panelBottom";
      this.panelBottom.Size = new System.Drawing.Size(282, 24);
      this.panelBottom.TabIndex = 10;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(2, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "JumpTo=\'\'";
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.SystemColors.Control;
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(282, 55);
      this.panel2.TabIndex = 8;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 10);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(156, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Catalog = \'???\', Schema = \'???\'";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panel4
      // 
      this.panel4.BackColor = System.Drawing.SystemColors.Control;
      this.panel4.Controls.Add(this.button2);
      this.panel4.Controls.Add(this.txtCustomFilter);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel4.Location = new System.Drawing.Point(0, 457);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(282, 26);
      this.panel4.TabIndex = 8;
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.Location = new System.Drawing.Point(234, 2);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(46, 22);
      this.button2.TabIndex = 1;
      this.button2.Text = "Filter";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // txtCustomFilter
      // 
      this.txtCustomFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCustomFilter.Location = new System.Drawing.Point(2, 3);
      this.txtCustomFilter.Name = "txtCustomFilter";
      this.txtCustomFilter.Size = new System.Drawing.Size(232, 20);
      this.txtCustomFilter.TabIndex = 0;
      this.txtCustomFilter.TextChanged += new System.EventHandler(this.txtCustomFilter_TextChanged);
      this.txtCustomFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomFilter_KeyDown);
      // 
      // frmObjectSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(282, 483);
      this.ControlBox = false;
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmObjectSelector";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "DB Objects";
      this.TopMost = true;
      this.Deactivate += new System.EventHandler(this.frmCodeCompletion_Deactivate);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmObjectSelector_FormClosed);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.panel5.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panelBottom.ResumeLayout(false);
      this.panelBottom.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panelBottom;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txtCustomFilter;
    private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridViewTextBoxColumn colType;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDataType;
		private System.Windows.Forms.DataGridViewTextBoxColumn colParentName;


  }
}