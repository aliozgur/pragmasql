namespace PragmaSQL
{
	partial class frmMultiConnectionSpec2
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMultiConnectionSpec2));
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lv = new PragmaSQL.Core.DetailListView(this.components);
      this.colServer = new System.Windows.Forms.ColumnHeader();
      this.colDb = new System.Windows.Forms.ColumnHeader();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.cmbTemplates = new System.Windows.Forms.ToolStripComboBox();
      this.btnEdit = new System.Windows.Forms.ToolStripButton();
      this.btnNew = new System.Windows.Forms.ToolStripButton();
      this.btnRename = new System.Windows.Forms.ToolStripButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
      this.btnDel = new System.Windows.Forms.ToolStripButton();
      this.btnSessionDefault = new System.Windows.Forms.Button();
      this.ctxSessionDefault = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.loadDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.deleteDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1.SuspendLayout();
      this.ctxSessionDefault.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(294, 306);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 27);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(375, 306);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 27);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // lv
      // 
      this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lv.ColumnClickSortEnabled = true;
      this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colServer,
            this.colDb});
      this.lv.FullRowSelect = true;
      this.lv.HideSelection = false;
      this.lv.Location = new System.Drawing.Point(3, 26);
      this.lv.Name = "lv";
      this.lv.Size = new System.Drawing.Size(453, 269);
      this.lv.TabIndex = 6;
      this.lv.UseCompatibleStateImageBehavior = false;
      this.lv.View = System.Windows.Forms.View.Details;
      // 
      // colServer
      // 
      this.colServer.Text = "Server";
      this.colServer.Width = 184;
      // 
      // colDb
      // 
      this.colDb.Text = "Database";
      this.colDb.Width = 257;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripLabel1,
            this.cmbTemplates,
            this.btnEdit,
            this.btnNew,
            this.btnRename,
            this.toolStripButton2,
            this.btnDel});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(459, 25);
      this.toolStrip1.TabIndex = 8;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton1.Image = global::PragmaSQL.Properties.Resources.Refresh;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton1.Text = "Refresh Template List";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
      this.toolStripLabel1.Text = "Template";
      // 
      // cmbTemplates
      // 
      this.cmbTemplates.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.cmbTemplates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cmbTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbTemplates.Name = "cmbTemplates";
      this.cmbTemplates.Size = new System.Drawing.Size(250, 25);
      this.cmbTemplates.ToolTipText = "Please select a template";
      this.cmbTemplates.SelectedIndexChanged += new System.EventHandler(this.cmbTemplates_SelectedIndexChanged);
      // 
      // btnEdit
      // 
      this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnEdit.Image = global::PragmaSQL.Properties.Resources.edit;
      this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(23, 22);
      this.btnEdit.Text = "Edit Template";
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // btnNew
      // 
      this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnNew.Image = global::PragmaSQL.Properties.Resources.new_style_2;
      this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(23, 22);
      this.btnNew.Text = "New Template";
      this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
      // 
      // btnRename
      // 
      this.btnRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnRename.Image = global::PragmaSQL.Properties.Resources.RenameFolder;
      this.btnRename.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(23, 22);
      this.btnRename.Text = "Rename Template";
      this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButton2.Image = global::PragmaSQL.Properties.Resources.refresh2;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
      this.toolStripButton2.Text = "Reload Template";
      this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
      // 
      // btnDel
      // 
      this.btnDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnDel.Image = global::PragmaSQL.Properties.Resources.delete;
      this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnDel.Name = "btnDel";
      this.btnDel.Size = new System.Drawing.Size(23, 22);
      this.btnDel.Text = "Delete Template";
      this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
      // 
      // btnSessionDefault
      // 
      this.btnSessionDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSessionDefault.Image = global::PragmaSQL.Properties.Resources.PlayHS_Black;
      this.btnSessionDefault.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btnSessionDefault.Location = new System.Drawing.Point(3, 306);
      this.btnSessionDefault.Name = "btnSessionDefault";
      this.btnSessionDefault.Size = new System.Drawing.Size(86, 27);
      this.btnSessionDefault.TabIndex = 9;
      this.btnSessionDefault.Text = "Default";
      this.btnSessionDefault.UseVisualStyleBackColor = true;
      this.btnSessionDefault.Click += new System.EventHandler(this.btnSessionDefault_Click);
      // 
      // ctxSessionDefault
      // 
      this.ctxSessionDefault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDefaultToolStripMenuItem,
            this.saveAsDefaultToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteDefaultToolStripMenuItem});
      this.ctxSessionDefault.Name = "ctxSessionDefault";
      this.ctxSessionDefault.Size = new System.Drawing.Size(163, 76);
      // 
      // loadDefaultToolStripMenuItem
      // 
      this.loadDefaultToolStripMenuItem.Name = "loadDefaultToolStripMenuItem";
      this.loadDefaultToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
      this.loadDefaultToolStripMenuItem.Text = "Load Default";
      this.loadDefaultToolStripMenuItem.Click += new System.EventHandler(this.loadDefaultToolStripMenuItem_Click);
      // 
      // saveAsDefaultToolStripMenuItem
      // 
      this.saveAsDefaultToolStripMenuItem.Name = "saveAsDefaultToolStripMenuItem";
      this.saveAsDefaultToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
      this.saveAsDefaultToolStripMenuItem.Text = "Save As Default";
      this.saveAsDefaultToolStripMenuItem.Click += new System.EventHandler(this.saveAsDefaultToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
      // 
      // deleteDefaultToolStripMenuItem
      // 
      this.deleteDefaultToolStripMenuItem.Name = "deleteDefaultToolStripMenuItem";
      this.deleteDefaultToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
      this.deleteDefaultToolStripMenuItem.Text = "Delete Default";
      this.deleteDefaultToolStripMenuItem.Click += new System.EventHandler(this.deleteDefaultToolStripMenuItem_Click);
      // 
      // frmMultiConnectionSpec2
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(459, 343);
      this.Controls.Add(this.btnSessionDefault);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.lv);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMultiConnectionSpec2";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Execute In Multiple Databases";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMultiConnectionSpec_FormClosed);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ctxSessionDefault.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

    private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private PragmaSQL.Core.DetailListView lv;
		private System.Windows.Forms.ColumnHeader colServer;
    private System.Windows.Forms.ColumnHeader colDb;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripComboBox cmbTemplates;
    private System.Windows.Forms.ToolStripButton btnEdit;
    private System.Windows.Forms.ToolStripButton btnNew;
    private System.Windows.Forms.ToolStripButton btnDel;
    private System.Windows.Forms.ToolStripButton btnRename;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.Button btnSessionDefault;
    private System.Windows.Forms.ContextMenuStrip ctxSessionDefault;
    private System.Windows.Forms.ToolStripMenuItem loadDefaultToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsDefaultToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem deleteDefaultToolStripMenuItem;


	}
}