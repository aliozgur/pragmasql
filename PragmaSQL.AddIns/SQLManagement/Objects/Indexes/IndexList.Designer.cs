namespace SQLManagement
{
  partial class IndexList
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
      this.bsIndexes = new System.Windows.Forms.BindingSource(this.components);
      this.grd = new System.Windows.Forms.DataGridView();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.bsIndexes)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      this.SuspendLayout();
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.AutoGenerateColumns = false;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column6,
            this.Column1,
            this.colObjectName,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
      this.grd.DataSource = this.bsIndexes;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.Size = new System.Drawing.Size(619, 354);
      this.grd.TabIndex = 2;
      this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
      // 
      // Column7
      // 
      this.Column7.DataPropertyName = "objectID";
      this.Column7.HeaderText = "Object ID";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      // 
      // Column6
      // 
      this.Column6.DataPropertyName = "indexID";
      this.Column6.HeaderText = "Index ID";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "owner";
      this.Column1.HeaderText = "Owner";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      // 
      // colObjectName
      // 
      this.colObjectName.DataPropertyName = "objectName";
      this.colObjectName.HeaderText = "Object Name";
      this.colObjectName.Name = "colObjectName";
      this.colObjectName.ReadOnly = true;
      this.colObjectName.Visible = false;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "indexName";
      this.Column2.HeaderText = "Index Name";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "unique";
      this.Column3.HeaderText = "Unique";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      // 
      // Column4
      // 
      this.Column4.DataPropertyName = "clustered";
      this.Column4.HeaderText = "Clustered";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      // 
      // Column5
      // 
      this.Column5.DataPropertyName = "filegroup";
      this.Column5.HeaderText = "File Group";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      // 
      // IndexList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Name = "IndexList";
      this.Size = new System.Drawing.Size(619, 354);
      ((System.ComponentModel.ISupportInitialize)(this.bsIndexes)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource bsIndexes;
    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn colObjectName;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
  }
}
