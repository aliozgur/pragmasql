namespace PragmaSQL.GUI
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
      this.grdObjects = new System.Windows.Forms.DataGridView();
      this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel3 = new System.Windows.Forms.Panel();
      this.panelBottom = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.button1 = new System.Windows.Forms.Button();
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.label2 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdObjects)).BeginInit();
      this.panel3.SuspendLayout();
      this.panelBottom.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.grdObjects);
      this.panel1.Controls.Add(this.panel3);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(284, 493);
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
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.SteelBlue;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grdObjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.grdObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grdObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
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
      this.grdObjects.Size = new System.Drawing.Size(284, 437);
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
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
      dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
      this.colType.DefaultCellStyle = dataGridViewCellStyle2;
      this.colType.Frozen = true;
      this.colType.HeaderText = "Type";
      this.colType.Name = "colType";
      this.colType.ReadOnly = true;
      this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colType.Width = 59;
      // 
      // colName
      // 
      this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colName.DataPropertyName = "Name";
      this.colName.HeaderText = "Object";
      this.colName.Name = "colName";
      this.colName.ReadOnly = true;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.panelBottom);
      this.panel3.Controls.Add(this.panel2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 437);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(284, 56);
      this.panel3.TabIndex = 7;
      // 
      // panelBottom
      // 
      this.panelBottom.BackColor = System.Drawing.SystemColors.Control;
      this.panelBottom.Controls.Add(this.label1);
      this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelBottom.Location = new System.Drawing.Point(0, 32);
      this.panelBottom.Name = "panelBottom";
      this.panelBottom.Size = new System.Drawing.Size(284, 24);
      this.panelBottom.TabIndex = 7;
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
      this.panel2.Controls.Add(this.button1);
      this.panel2.Controls.Add(this.label2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(284, 56);
      this.panel2.TabIndex = 8;
      // 
      // button1
      // 
      this.button1.BackColor = System.Drawing.SystemColors.Control;
      this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
      this.button1.ImageIndex = 10;
      this.button1.ImageList = this.ImageList1;
      this.button1.Location = new System.Drawing.Point(4, 3);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(28, 26);
      this.button1.TabIndex = 3;
      this.button1.UseVisualStyleBackColor = false;
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.ImageList1.Images.SetKeyName(0, "database.bmp");
      this.ImageList1.Images.SetKeyName(1, "User.bmp");
      this.ImageList1.Images.SetKeyName(2, "DataSet_TableView.bmp");
      this.ImageList1.Images.SetKeyName(3, "View1.ico");
      this.ImageList1.Images.SetKeyName(4, "Proc2.ico");
      this.ImageList1.Images.SetKeyName(5, "function.ico");
      this.ImageList1.Images.SetKeyName(6, "grid select column.bmp");
      this.ImageList1.Images.SetKeyName(7, "paramin.ico");
      this.ImageList1.Images.SetKeyName(8, "keys.bmp");
      this.ImageList1.Images.SetKeyName(9, "Control_TextBox.bmp");
      this.ImageList1.Images.SetKeyName(10, "refresh.bmp");
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(37, 11);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(156, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Catalog = \'???\', Schema = \'???\'";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // frmObjectSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(284, 493);
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
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grdObjects)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panelBottom.ResumeLayout(false);
      this.panelBottom.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.DataGridView grdObjects;
    private System.Windows.Forms.ImageList ImageList1;
    private System.Windows.Forms.DataGridViewTextBoxColumn colType;
    private System.Windows.Forms.DataGridViewTextBoxColumn colName;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panelBottom;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label2;


  }
}