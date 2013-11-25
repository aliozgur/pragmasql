namespace PragmaSQL
{
  partial class frmSaveScripts
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveScripts));
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnSkipAll = new System.Windows.Forms.Button();
      this.btnSaveChecked = new System.Windows.Forms.Button();
      this.lv = new System.Windows.Forms.ListView();
      this.colName = new System.Windows.Forms.ColumnHeader();
      this.colHint = new System.Windows.Forms.ColumnHeader();
      this.colLastModified = new System.Windows.Forms.ColumnHeader();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnSkipAll);
      this.panel1.Controls.Add(this.btnSaveChecked);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 253);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(475, 41);
      this.panel1.TabIndex = 0;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(385, 8);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(80, 26);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnSkipAll
      // 
      this.btnSkipAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSkipAll.Location = new System.Drawing.Point(300, 8);
      this.btnSkipAll.Name = "btnSkipAll";
      this.btnSkipAll.Size = new System.Drawing.Size(80, 26);
      this.btnSkipAll.TabIndex = 1;
      this.btnSkipAll.Text = "Skip All";
      this.btnSkipAll.UseVisualStyleBackColor = true;
      this.btnSkipAll.Click += new System.EventHandler(this.btnSkipAll_Click);
      // 
      // btnSaveChecked
      // 
      this.btnSaveChecked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveChecked.Location = new System.Drawing.Point(190, 8);
      this.btnSaveChecked.Name = "btnSaveChecked";
      this.btnSaveChecked.Size = new System.Drawing.Size(86, 26);
      this.btnSaveChecked.TabIndex = 0;
      this.btnSaveChecked.Text = "Save Checked";
      this.btnSaveChecked.UseVisualStyleBackColor = true;
      this.btnSaveChecked.Click += new System.EventHandler(this.btnSaveChecked_Click);
      // 
      // lv
      // 
      this.lv.CheckBoxes = true;
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colLastModified,
            this.colHint});
      this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lv.FullRowSelect = true;
      this.lv.Location = new System.Drawing.Point(0, 0);
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(475, 253);
      this.lv.TabIndex = 3;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      // 
      // colName
      // 
      this.colName.Text = "Name";
      this.colName.Width = 157;
      // 
      // colHint
      // 
      this.colHint.Text = "Path";
      this.colHint.Width = 204;
      // 
      // colLastModified
      // 
      this.colLastModified.Text = "Modifed On";
      this.colLastModified.Width = 108;
      // 
      // frmSaveScripts
      // 
      this.AcceptButton = this.btnSaveChecked;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(475, 294);
      this.Controls.Add(this.lv);
      this.Controls.Add(this.panel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimizeBox = false;
      this.Name = "frmSaveScripts";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Save Changes";
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnSkipAll;
    private System.Windows.Forms.Button btnSaveChecked;
    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader colName;
    private System.Windows.Forms.ColumnHeader colHint;
    private System.Windows.Forms.ColumnHeader colLastModified;
  }
}