namespace PragmaSQL.Core
{
  partial class ucObjectExplorerOptions
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkShowSysTables = new System.Windows.Forms.CheckBox();
      this.chkShowSysDatabases = new System.Windows.Forms.CheckBox();
      this.chkShowDbComLevel = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.chkShowSysTables);
      this.groupBox1.Controls.Add(this.chkShowSysDatabases);
      this.groupBox1.Controls.Add(this.chkShowDbComLevel);
      this.groupBox1.Location = new System.Drawing.Point(3, 2);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(356, 90);
      this.groupBox1.TabIndex = 15;
      this.groupBox1.TabStop = false;
      // 
      // chkShowSysTables
      // 
      this.chkShowSysTables.AutoSize = true;
      this.chkShowSysTables.Location = new System.Drawing.Point(13, 17);
      this.chkShowSysTables.Name = "chkShowSysTables";
      this.chkShowSysTables.Size = new System.Drawing.Size(119, 17);
      this.chkShowSysTables.TabIndex = 3;
      this.chkShowSysTables.Text = "Show system tables";
      this.chkShowSysTables.UseVisualStyleBackColor = true;
      this.chkShowSysTables.CheckStateChanged += new System.EventHandler(this.chkShowFullObjName_CheckStateChanged);
      // 
      // chkShowSysDatabases
      // 
      this.chkShowSysDatabases.AutoSize = true;
      this.chkShowSysDatabases.Location = new System.Drawing.Point(13, 60);
      this.chkShowSysDatabases.Name = "chkShowSysDatabases";
      this.chkShowSysDatabases.Size = new System.Drawing.Size(140, 17);
      this.chkShowSysDatabases.TabIndex = 1;
      this.chkShowSysDatabases.Text = "Show system databases";
      this.chkShowSysDatabases.UseVisualStyleBackColor = true;
      this.chkShowSysDatabases.CheckStateChanged += new System.EventHandler(this.chkShowFullObjName_CheckStateChanged);
      // 
      // chkShowDbComLevel
      // 
      this.chkShowDbComLevel.AutoSize = true;
      this.chkShowDbComLevel.Location = new System.Drawing.Point(13, 37);
      this.chkShowDbComLevel.Name = "chkShowDbComLevel";
      this.chkShowDbComLevel.Size = new System.Drawing.Size(185, 17);
      this.chkShowDbComLevel.TabIndex = 1;
      this.chkShowDbComLevel.Text = "Show database compatibility level";
      this.chkShowDbComLevel.UseVisualStyleBackColor = true;
      this.chkShowDbComLevel.CheckStateChanged += new System.EventHandler(this.chkShowFullObjName_CheckStateChanged);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.Location = new System.Drawing.Point(3, 102);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(298, 28);
      this.label1.TabIndex = 4;
      this.label1.Text = "Note: Changes will be applied on next load/refresh of the server,database or obje" +
          "cts folder";
      // 
      // ucObjectExplorerOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.label1);
      this.Controls.Add(this.groupBox1);
      this.Name = "ucObjectExplorerOptions";
      this.Size = new System.Drawing.Size(362, 137);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox chkShowSysTables;
    private System.Windows.Forms.CheckBox chkShowDbComLevel;
    private System.Windows.Forms.CheckBox chkShowSysDatabases;
    private System.Windows.Forms.Label label1;
  }
}
