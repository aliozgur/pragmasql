namespace PragmaSQL
{
	partial class frmScriptPreview
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScriptPreview));
      this.panEditor = new System.Windows.Forms.Panel();
      this.fastPreviewToolStrip = new System.Windows.Forms.ToolStrip();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
      this.btnStick = new System.Windows.Forms.ToolStripButton();
      this.fastPreviewToolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // panEditor
      // 
      this.panEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panEditor.Location = new System.Drawing.Point(0, 25);
      this.panEditor.Name = "panEditor";
      this.panEditor.Size = new System.Drawing.Size(728, 437);
      this.panEditor.TabIndex = 0;
      // 
      // fastPreviewToolStrip
      // 
      this.fastPreviewToolStrip.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
      this.fastPreviewToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.fastPreviewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.btnStick});
      this.fastPreviewToolStrip.Location = new System.Drawing.Point(0, 0);
      this.fastPreviewToolStrip.Name = "fastPreviewToolStrip";
      this.fastPreviewToolStrip.Size = new System.Drawing.Size(728, 25);
      this.fastPreviewToolStrip.TabIndex = 2;
      this.fastPreviewToolStrip.Text = "FastPreviewToolStrip";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.Image = global::PragmaSQL.Properties.Resources.edit;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(119, 22);
      this.toolStripButton1.Text = "Edit In Script Editor";
      this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
      // 
      // btnStick
      // 
      this.btnStick.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.btnStick.CheckOnClick = true;
      this.btnStick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStick.Image = ((System.Drawing.Image)(resources.GetObject("btnStick.Image")));
      this.btnStick.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStick.Name = "btnStick";
      this.btnStick.Size = new System.Drawing.Size(23, 22);
      this.btnStick.Text = "Stick Preview Form";
      this.btnStick.CheckStateChanged += new System.EventHandler(this.btnStick_CheckStateChanged);
      // 
      // frmScriptPreview
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(728, 462);
      this.Controls.Add(this.panEditor);
      this.Controls.Add(this.fastPreviewToolStrip);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmScriptPreview";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.Text = "Script Preview";
      this.Deactivate += new System.EventHandler(this.frmScriptPreview_Deactivate);
      this.fastPreviewToolStrip.ResumeLayout(false);
      this.fastPreviewToolStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panEditor;
		private System.Windows.Forms.ToolStrip fastPreviewToolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton btnStick;
	}
}