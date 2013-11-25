namespace PragmaSQL
{
  partial class frmSharedScriptSelectDialog
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.panItemName = new System.Windows.Forms.Panel();
      this.txtItemName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.ucSharedScripts1 = new PragmaSQL.ucSharedScripts();
      this.panel1.SuspendLayout();
      this.panItemName.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.panItemName);
      this.panel1.Controls.Add(this.btnOk);
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 291);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(502, 58);
      this.panel1.TabIndex = 0;
      // 
      // panItemName
      // 
      this.panItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panItemName.Controls.Add(this.txtItemName);
      this.panItemName.Controls.Add(this.label1);
      this.panItemName.Location = new System.Drawing.Point(3, 9);
      this.panItemName.Name = "panItemName";
      this.panItemName.Size = new System.Drawing.Size(333, 39);
      this.panItemName.TabIndex = 2;
      this.panItemName.Visible = false;
      // 
      // txtItemName
      // 
      this.txtItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtItemName.Location = new System.Drawing.Point(76, 7);
      this.txtItemName.Name = "txtItemName";
      this.txtItemName.Size = new System.Drawing.Size(251, 23);
      this.txtItemName.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Script Name";
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(340, 14);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 30);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "Open";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(419, 14);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 30);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // ucSharedScripts1
      // 
      this.ucSharedScripts1.Disabled = false;
      this.ucSharedScripts1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ucSharedScripts1.HelpTextVisible = false;
      this.ucSharedScripts1.Location = new System.Drawing.Point(0, 0);
      this.ucSharedScripts1.Mode = PragmaSQL.SharedScriptsMode.Edit;
      this.ucSharedScripts1.Name = "ucSharedScripts1";
      this.ucSharedScripts1.SelectedNode = null;
      this.ucSharedScripts1.Size = new System.Drawing.Size(502, 291);
      this.ucSharedScripts1.TabIndex = 1;
      this.ucSharedScripts1.SelectedItemChanged += new PragmaSQL.SelectedItemChangedEventHandler(this.ucSharedScripts1_SelectedItemChanged);
      // 
      // frmSharedScriptSelectDialog
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(502, 349);
      this.Controls.Add(this.ucSharedScripts1);
      this.Controls.Add(this.panel1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSharedScriptSelectDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Open Shared Script";
      this.panel1.ResumeLayout(false);
      this.panItemName.ResumeLayout(false);
      this.panItemName.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Panel panItemName;
    private System.Windows.Forms.TextBox txtItemName;
    private System.Windows.Forms.Label label1;
    private ucSharedScripts ucSharedScripts1;
  }
}