namespace PragmaSQL
{
  partial class frmTextDiff
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextDiff));
      this.diffControl = new PragmaSQL.DiffText();
      this.SuspendLayout();
      // 
      // diffControl
      // 
      this.diffControl.AllowFileDrop = true;
      this.diffControl.AllowTextDrop = true;
      this.diffControl.ContextMenusEnabled = true;
      this.diffControl.DestHeaderText = "Dest";
      this.diffControl.DestText = "";
      this.diffControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.diffControl.HeadersVisible = true;
      this.diffControl.Location = new System.Drawing.Point(0, 0);
      this.diffControl.Name = "diffControl";
      this.diffControl.ShowOpenFileMenuButtons = true;
      this.diffControl.ShowPasteMenuButtons = true;
      this.diffControl.Size = new System.Drawing.Size(866, 615);
      this.diffControl.SourceHeaderText = "Source";
      this.diffControl.SourceText = "";
      this.diffControl.TabIndex = 0;
      this.diffControl.OnAfterPasteFromClipboard += new PragmaSQL.AfterPasteFromClipboardDelegate(this.diffControl_OnAfterPasteFromClipboard);
      this.diffControl.OnAfterTextDragDrop += new PragmaSQL.AfterTextDragDropDelegate(this.diffControl_OnAfterTextDragDrop);
      this.diffControl.OnAfterOpenFile += new PragmaSQL.AfterOpenFileDelegate(this.diffControl_OnAfterOpenFile);
      // 
      // frmTextDiff
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(866, 615);
      this.Controls.Add(this.diffControl);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmTextDiff";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "PragmaSQL Text Diff";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTextDiff_FormClosed);
      this.Activated += new System.EventHandler(this.frmTextDiff_Activated);
      this.ResumeLayout(false);

    }

    #endregion

    internal DiffText diffControl;

  }
}