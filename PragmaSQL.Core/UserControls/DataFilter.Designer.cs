using System;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	partial class DataFilter
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grdCr = new System.Windows.Forms.DataGridView();
			this.ts = new System.Windows.Forms.ToolStrip();
			this.tsBtnClear = new System.Windows.Forms.ToolStripButton();
			this.tsBtnRemove = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.grdCr)).BeginInit();
			this.ts.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdCr
			// 
			this.grdCr.AllowUserToAddRows = false;
			this.grdCr.AllowUserToDeleteRows = false;
			this.grdCr.AllowUserToResizeRows = false;
			this.grdCr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.grdCr.BackgroundColor = System.Drawing.SystemColors.Window;
			this.grdCr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.grdCr.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.grdCr.Location = new System.Drawing.Point(0, 25);
			this.grdCr.MultiSelect = false;
			this.grdCr.Name = "grdCr";
			this.grdCr.ShowEditingIcon = false;
			this.grdCr.Size = new System.Drawing.Size(405, 251);
			this.grdCr.TabIndex = 0;
			// 
			// ts
			// 
			this.ts.AllowMerge = false;
			this.ts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ts.AutoSize = false;
			this.ts.Dock = System.Windows.Forms.DockStyle.None;
			this.ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnClear,
            this.tsBtnRemove});
			this.ts.Location = new System.Drawing.Point(0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(405, 25);
			this.ts.TabIndex = 1;
			this.ts.Text = "toolStrip1";
			// 
			// tsBtnClear
			// 
			this.tsBtnClear.Image = global::PragmaSQL.Core.Properties.Resources.delete_all_sel;
			this.tsBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBtnClear.Name = "tsBtnClear";
			this.tsBtnClear.Size = new System.Drawing.Size(52, 22);
			this.tsBtnClear.Text = "Clear";
			this.tsBtnClear.Click += new System.EventHandler(this.tsBtnClear_Click);
			// 
			// tsBtnRemove
			// 
			this.tsBtnRemove.Image = global::PragmaSQL.Core.Properties.Resources.delete;
			this.tsBtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsBtnRemove.Name = "tsBtnRemove";
			this.tsBtnRemove.Size = new System.Drawing.Size(66, 22);
			this.tsBtnRemove.Text = "Remove";
			this.tsBtnRemove.ToolTipText = "Remove";
			this.tsBtnRemove.Click += new System.EventHandler(this.tsBtnRemove_Click);
			// 
			// DataFilter
			// 
			this.Controls.Add(this.grdCr);
			this.Controls.Add(this.ts);
			this.MinimumSize = new System.Drawing.Size(100, 50);
			this.Name = "DataFilter";
			this.Size = new System.Drawing.Size(405, 276);
			((System.ComponentModel.ISupportInitialize)(this.grdCr)).EndInit();
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView grdCr;
		private System.Windows.Forms.ToolStrip ts;
		private System.Windows.Forms.ToolStripButton tsBtnClear;
		private System.Windows.Forms.ToolStripButton tsBtnRemove;

	}
}
