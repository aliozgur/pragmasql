namespace SQLManagement
{
  partial class RulesList 
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
      this.grd = new System.Windows.Forms.DataGridView();
      this.bsRules = new System.Windows.Forms.BindingSource(this.components);
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsRules)).BeginInit();
      this.SuspendLayout();
      // 
      // grd
      // 
      this.grd.AllowUserToAddRows = false;
      this.grd.AllowUserToDeleteRows = false;
      this.grd.AllowUserToResizeRows = false;
      this.grd.AutoGenerateColumns = false;
      this.grd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.grd.BackgroundColor = System.Drawing.SystemColors.Window;
      this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column7,
            this.Column6});
      this.grd.DataSource = this.bsRules;
      this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd.Location = new System.Drawing.Point(0, 0);
      this.grd.Name = "grd";
      this.grd.ReadOnly = true;
      this.grd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.grd.RowTemplate.Height = 25;
      this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grd.Size = new System.Drawing.Size(707, 428);
      this.grd.TabIndex = 1;
      this.grd.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "id";
      this.Column1.HeaderText = "ID";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Visible = false;
      this.Column1.Width = 43;
      // 
      // Column2
      // 
      this.Column2.DataPropertyName = "owner";
      this.Column2.HeaderText = "Owner";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Width = 64;
      // 
      // Column7
      // 
      this.Column7.DataPropertyName = "ruleName";
      this.Column7.HeaderText = "Name";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      this.Column7.Width = 61;
      // 
      // Column6
      // 
      this.Column6.DataPropertyName = "crdate";
      this.Column6.HeaderText = "Created";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      this.Column6.Width = 71;
      // 
      // RulesList
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.grd);
      this.Name = "RulesList";
      this.Size = new System.Drawing.Size(707, 428);
      ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsRules)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView grd;
    private System.Windows.Forms.BindingSource bsRules;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
  }
}
