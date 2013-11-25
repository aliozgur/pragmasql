namespace PragmaSQL
{
  partial class frmSharedScripts
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSharedScripts));
      this.ucSharedScripts1 = new PragmaSQL.ucSharedScripts();
      this.SuspendLayout();
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
      this.ucSharedScripts1.Size = new System.Drawing.Size(325, 710);
      this.ucSharedScripts1.TabIndex = 0;
      // 
      // frmSharedScripts
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(325, 710);
      this.Controls.Add(this.ucSharedScripts1);
      this.HideOnClose = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmSharedScripts";
      this.TabText = "Shared Scripts";
      this.Text = "Shared Scripts";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSharedScripts_FormClosed);
      this.ResumeLayout(false);

    }

    #endregion

    private ucSharedScripts ucSharedScripts1;
  }
}