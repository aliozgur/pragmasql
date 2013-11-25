namespace PragmaSQL
{
  partial class frmObjectSelectorEx
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectSelectorEx));
      this.panel1 = new System.Windows.Forms.Panel();
      this.grdItems = new System.Windows.Forms.DataGridView();
      this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDisplayValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.panel2 = new System.Windows.Forms.Panel();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.lblListName = new System.Windows.Forms.Label();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.panel4 = new System.Windows.Forms.Panel();
      this.button2 = new System.Windows.Forms.Button();
      this.txtCustomFilter = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.panel4.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.grdItems);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 19);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(239, 331);
      this.panel1.TabIndex = 0;
      // 
      // grdItems
      // 
      this.grdItems.AllowUserToAddRows = false;
      this.grdItems.AllowUserToDeleteRows = false;
      this.grdItems.AllowUserToResizeRows = false;
      this.grdItems.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grdItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.grdItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.grdItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.SteelBlue;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grdItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDisplayName,
            this.colDisplayValue,
            this.colValue});
      this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.grdItems.GridColor = System.Drawing.SystemColors.Window;
      this.grdItems.Location = new System.Drawing.Point(0, 0);
      this.grdItems.Name = "grdItems";
      this.grdItems.ReadOnly = true;
      this.grdItems.RowHeadersVisible = false;
      this.grdItems.RowTemplate.Height = 25;
      this.grdItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grdItems.ShowEditingIcon = false;
      this.grdItems.Size = new System.Drawing.Size(239, 331);
      this.grdItems.StandardTab = true;
      this.grdItems.TabIndex = 1;
      this.grdItems.DoubleClick += new System.EventHandler(this.grdItems_DoubleClick);
      this.grdItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdItems_KeyDown);
      this.grdItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdItems_KeyPress);
      this.grdItems.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdItems_KeyUp);
      // 
      // colDisplayName
      // 
      this.colDisplayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colDisplayName.DataPropertyName = "DisplayName";
      this.colDisplayName.HeaderText = "Key";
      this.colDisplayName.Name = "colDisplayName";
      this.colDisplayName.ReadOnly = true;
      // 
      // colDisplayValue
      // 
      this.colDisplayValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colDisplayValue.DataPropertyName = "DisplayValue";
      this.colDisplayValue.HeaderText = "Code";
      this.colDisplayValue.Name = "colDisplayValue";
      this.colDisplayValue.ReadOnly = true;
      // 
      // colValue
      // 
      this.colValue.DataPropertyName = "Value";
      this.colValue.HeaderText = "Value";
      this.colValue.Name = "colValue";
      this.colValue.ReadOnly = true;
      this.colValue.Visible = false;
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
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.SystemColors.Control;
      this.panel2.Controls.Add(this.pictureBox3);
      this.panel2.Controls.Add(this.lblListName);
      this.panel2.Controls.Add(this.pictureBox1);
      this.panel2.Controls.Add(this.pictureBox2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(239, 19);
      this.panel2.TabIndex = 1;
      // 
      // pictureBox3
      // 
      this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Left;
      this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
      this.pictureBox3.Location = new System.Drawing.Point(0, 0);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(16, 19);
      this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox3.TabIndex = 3;
      this.pictureBox3.TabStop = false;
      this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
      // 
      // lblListName
      // 
      this.lblListName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblListName.Location = new System.Drawing.Point(0, 0);
      this.lblListName.Name = "lblListName";
      this.lblListName.Size = new System.Drawing.Size(207, 19);
      this.lblListName.TabIndex = 2;
      this.lblListName.Text = "List Name";
      this.lblListName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(207, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(16, 19);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // pictureBox2
      // 
      this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(223, 0);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(16, 19);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox2.TabIndex = 1;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
      // 
      // panel4
      // 
      this.panel4.BackColor = System.Drawing.SystemColors.Control;
      this.panel4.Controls.Add(this.button2);
      this.panel4.Controls.Add(this.txtCustomFilter);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel4.Location = new System.Drawing.Point(0, 350);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(239, 26);
      this.panel4.TabIndex = 9;
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.Location = new System.Drawing.Point(191, 2);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(46, 22);
      this.button2.TabIndex = 1;
      this.button2.Text = "Filter";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // txtCustomFilter
      // 
      this.txtCustomFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCustomFilter.Location = new System.Drawing.Point(2, 3);
      this.txtCustomFilter.Name = "txtCustomFilter";
      this.txtCustomFilter.Size = new System.Drawing.Size(189, 20);
      this.txtCustomFilter.TabIndex = 0;
      this.txtCustomFilter.TextChanged += new System.EventHandler(this.txtCustomFilter_TextChanged);
      this.txtCustomFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomFilter_KeyDown);
      // 
      // frmObjectSelectorEx
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(239, 376);
      this.ControlBox = false;
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel4);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmObjectSelectorEx";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Code Completion Lists";
      this.TopMost = true;
      this.Deactivate += new System.EventHandler(this.frmCodeCompletion_Deactivate);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.DataGridView grdItems;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Label lblListName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayValue;
    private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TextBox txtCustomFilter;


  }
}