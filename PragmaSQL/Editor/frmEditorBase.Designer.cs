namespace PragmaSQL
{
  partial class frmEditorBase
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditorBase));
      this.panEditor = new System.Windows.Forms.Panel();
      this.tsNotifications = new System.Windows.Forms.ToolStrip();
      this.fsWatcher = new System.IO.FileSystemWatcher();
      this.infoHeader = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
      this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panEditor.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).BeginInit();
      this.SuspendLayout();
      // 
      // panEditor
      // 
      this.panEditor.Controls.Add(this.infoHeader);
      this.panEditor.Controls.Add(this.tsNotifications);
      this.panEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panEditor.Location = new System.Drawing.Point(0, 0);
      this.panEditor.Name = "panEditor";
      this.panEditor.Size = new System.Drawing.Size(751, 519);
      this.panEditor.TabIndex = 0;
      // 
      // tsNotifications
      // 
      this.tsNotifications.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tsNotifications.Location = new System.Drawing.Point(0, 0);
      this.tsNotifications.Name = "tsNotifications";
      this.tsNotifications.Size = new System.Drawing.Size(751, 25);
      this.tsNotifications.TabIndex = 0;
      this.tsNotifications.Text = "toolStrip1";
      this.tsNotifications.Visible = false;
      // 
      // fsWatcher
      // 
      this.fsWatcher.EnableRaisingEvents = true;
      this.fsWatcher.SynchronizingObject = this;
      this.fsWatcher.Renamed += new System.IO.RenamedEventHandler(this.fsWatcher_Renamed);
      this.fsWatcher.Deleted += new System.IO.FileSystemEventHandler(this.fsWatcher_Deleted);
      this.fsWatcher.Changed += new System.IO.FileSystemEventHandler(this.fsWatcher_Changed);
      // 
      // infoHeader
      // 
      this.infoHeader.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
      this.infoHeader.Dock = System.Windows.Forms.DockStyle.Top;
      this.infoHeader.Location = new System.Drawing.Point(0, 0);
      this.infoHeader.Name = "infoHeader";
      this.infoHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Global;
      this.infoHeader.Size = new System.Drawing.Size(751, 30);
      this.infoHeader.TabIndex = 11;
      this.toolTip1.SetToolTip(this.infoHeader, "Double click to close this info message.");
      this.infoHeader.Values.Description = "";
      this.infoHeader.Values.Heading = "";
      this.infoHeader.Values.Image = ((System.Drawing.Image)(resources.GetObject("infoHeader.Values.Image")));
      this.infoHeader.Visible = false;
      this.infoHeader.DoubleClick += new System.EventHandler(this.OnCloseInfoHeader);
      // 
      // buttonSpecAny1
      // 
      this.buttonSpecAny1.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Inherit;
      this.buttonSpecAny1.ExtraText = "";
      this.buttonSpecAny1.Image = null;
      this.buttonSpecAny1.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit;
      this.buttonSpecAny1.Text = "Close";
      this.buttonSpecAny1.UniqueName = "DF8600BCD578441DDF8600BCD578441D";
      this.buttonSpecAny1.Click += new System.EventHandler(this.OnCloseInfoHeader);
      // 
      // frmEditorBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(751, 519);
      this.Controls.Add(this.panEditor);
      this.Name = "frmEditorBase";
      this.TabText = "frmEditorBase";
      this.Text = "frmEditorBase";
      this.Enter += new System.EventHandler(this.frmEditorBase_Enter);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditorBase_FormClosed);
      this.Leave += new System.EventHandler(this.frmEditorBase_Leave);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditorBase_FormClosing);
      this.panEditor.ResumeLayout(false);
      this.panEditor.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.fsWatcher)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

		protected System.Windows.Forms.Panel panEditor;
		protected System.Windows.Forms.ToolStrip tsNotifications;
		protected System.IO.FileSystemWatcher fsWatcher;
    private ComponentFactory.Krypton.Toolkit.KryptonHeader infoHeader;
    private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
    private System.Windows.Forms.ToolTip toolTip1;

  }
}