namespace PragmaSQL.GUI
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaveScripts));
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnSkipAll = new System.Windows.Forms.Button();
      this.btnSaveChecked = new System.Windows.Forms.Button();
      this.lv = new System.Windows.Forms.ListView();
      this.colName = new System.Windows.Forms.ColumnHeader();
      this.colFileName = new System.Windows.Forms.ColumnHeader();
      this.colLastModified = new System.Windows.Forms.ColumnHeader();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnSkipAll);
      this.panel1.Controls.Add(this.btnSaveChecked);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 257);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(525, 41);
      this.panel1.TabIndex = 0;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(422, 6);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(91, 26);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnSkipAll
      // 
      this.btnSkipAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSkipAll.Location = new System.Drawing.Point(197, 6);
      this.btnSkipAll.Name = "btnSkipAll";
      this.btnSkipAll.Size = new System.Drawing.Size(91, 26);
      this.btnSkipAll.TabIndex = 1;
      this.btnSkipAll.Text = "Skip All";
      this.btnSkipAll.UseVisualStyleBackColor = true;
      this.btnSkipAll.Click += new System.EventHandler(this.btnSkipAll_Click);
      // 
      // btnSaveChecked
      // 
      this.btnSaveChecked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveChecked.Location = new System.Drawing.Point(319, 6);
      this.btnSaveChecked.Name = "btnSaveChecked";
      this.btnSaveChecked.Size = new System.Drawing.Size(97, 26);
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
            this.colFileName,
            this.colLastModified});
      this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lv.FullRowSelect = true;
      this.lv.LargeImageList = this.imageList1;
      this.lv.Location = new System.Drawing.Point(0, 0);
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(525, 257);
      this.lv.SmallImageList = this.imageList1;
      this.lv.TabIndex = 1;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      this.lv.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lv_ItemChecked);
      // 
      // colName
      // 
      this.colName.Text = "Name";
      this.colName.Width = 171;
      // 
      // colFileName
      // 
      this.colFileName.Text = "File Name";
      this.colFileName.Width = 87;
      // 
      // colLastModified
      // 
      this.colLastModified.Text = "Last Modifed On";
      this.colLastModified.Width = 130;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "sql.ico");
      // 
      // frmSaveScripts
      // 
      this.AcceptButton = this.btnSaveChecked;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(525, 298);
      this.Controls.Add(this.lv);
      this.Controls.Add(this.panel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimizeBox = false;
      this.Name = "frmSaveScripts";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Save Scripts";
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
    private System.Windows.Forms.ColumnHeader colLastModified;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ColumnHeader colFileName;
  }
}