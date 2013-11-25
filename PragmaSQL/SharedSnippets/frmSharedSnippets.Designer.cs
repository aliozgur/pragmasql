namespace PragmaSQL
{
  partial class frmSharedSnippets
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSharedSnippets));
      this.ucSharedSnippets1 = new PragmaSQL.ucSharedSnippets();
      this.SuspendLayout();
      // 
      // ucSharedSnippets1
      // 
      this.ucSharedSnippets1.DescriptionVisible = false;
      this.ucSharedSnippets1.Disabled = false;
      this.ucSharedSnippets1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ucSharedSnippets1.Location = new System.Drawing.Point(0, 0);
      this.ucSharedSnippets1.Mode = PragmaSQL.SharedSnippetMode.Edit;
      this.ucSharedSnippets1.Name = "ucSharedSnippets1";
      this.ucSharedSnippets1.SelectedNode = null;
      this.ucSharedSnippets1.Size = new System.Drawing.Size(319, 678);
      this.ucSharedSnippets1.TabIndex = 0;
      // 
      // frmSharedSnippets
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(319, 678);
      this.Controls.Add(this.ucSharedSnippets1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmSharedSnippets";
      this.TabText = "Shared Snippets";
      this.Text = "Shared Snippets";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSharedSnippets_FormClosed);
      this.ResumeLayout(false);

    }

    #endregion

    private ucSharedSnippets ucSharedSnippets1;
  }
}