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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectSelectorEx));
      this.panel1 = new System.Windows.Forms.Panel();
      this.grdObjects = new System.Windows.Forms.DataGridView();
      this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.panel2 = new System.Windows.Forms.Panel();
      this.cmbCodeCompletionList = new System.Windows.Forms.ComboBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdObjects)).BeginInit();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.grdObjects);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 31);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(239, 538);
      this.panel1.TabIndex = 0;
      // 
      // grdObjects
      // 
      this.grdObjects.AllowUserToAddRows = false;
      this.grdObjects.AllowUserToDeleteRows = false;
      this.grdObjects.AllowUserToResizeRows = false;
      this.grdObjects.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grdObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.grdObjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.grdObjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.Color.SteelBlue;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grdObjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.grdObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colDisplayName,
            this.colName});
      this.grdObjects.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdObjects.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.grdObjects.GridColor = System.Drawing.SystemColors.Window;
      this.grdObjects.Location = new System.Drawing.Point(0, 0);
      this.grdObjects.Name = "grdObjects";
      this.grdObjects.ReadOnly = true;
      this.grdObjects.RowHeadersVisible = false;
      this.grdObjects.RowTemplate.Height = 25;
      this.grdObjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grdObjects.ShowEditingIcon = false;
      this.grdObjects.Size = new System.Drawing.Size(239, 538);
      this.grdObjects.StandardTab = true;
      this.grdObjects.TabIndex = 1;
      this.grdObjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdObjects_KeyDown);
      this.grdObjects.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdObjects_CellPainting);
      this.grdObjects.DoubleClick += new System.EventHandler(this.grdObjects_DoubleClick);
      this.grdObjects.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdObjects_KeyPress);
      // 
      // colType
      // 
      this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.colType.DataPropertyName = "Type";
      dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
      this.colType.DefaultCellStyle = dataGridViewCellStyle4;
      this.colType.Frozen = true;
      this.colType.HeaderText = "Type";
      this.colType.Name = "colType";
      this.colType.ReadOnly = true;
      this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colType.Width = 60;
      // 
      // colDisplayName
      // 
      this.colDisplayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colDisplayName.DataPropertyName = "DisplayName";
      this.colDisplayName.HeaderText = "Object";
      this.colDisplayName.Name = "colDisplayName";
      this.colDisplayName.ReadOnly = true;
      // 
      // colName
      // 
      this.colName.HeaderText = "Name";
      this.colName.Name = "colName";
      this.colName.ReadOnly = true;
      this.colName.Visible = false;
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
      this.panel2.Controls.Add(this.cmbCodeCompletionList);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(239, 31);
      this.panel2.TabIndex = 1;
      // 
      // cmbCodeCompletionList
      // 
      this.cmbCodeCompletionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cmbCodeCompletionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbCodeCompletionList.FormattingEnabled = true;
      this.cmbCodeCompletionList.Location = new System.Drawing.Point(3, 4);
      this.cmbCodeCompletionList.Name = "cmbCodeCompletionList";
      this.cmbCodeCompletionList.Size = new System.Drawing.Size(231, 23);
      this.cmbCodeCompletionList.TabIndex = 0;
      this.cmbCodeCompletionList.SelectedIndexChanged += new System.EventHandler(this.cmbCodeCompletionList_SelectedIndexChanged);
      // 
      // frmObjectSelectorEx
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(239, 569);
      this.ControlBox = false;
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel2);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmObjectSelectorEx";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "DB Objects";
      this.TopMost = true;
      this.Deactivate += new System.EventHandler(this.frmCodeCompletion_Deactivate);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdObjects)).EndInit();
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.DataGridView grdObjects;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.DataGridViewTextBoxColumn colType;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayName;
    private System.Windows.Forms.DataGridViewTextBoxColumn colName;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.ComboBox cmbCodeCompletionList;


  }
}