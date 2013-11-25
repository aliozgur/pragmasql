using System;
using System.Windows.Forms;

namespace HtmlHelp2
{
  partial class MSHelp2TocControl
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
      if (tocControl != null)
      {
        tocControl.Dispose();
      }

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
    private void InitializeComponent( )
    {
      this.components = new System.ComponentModel.Container();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panTocControl = new System.Windows.Forms.Panel();
      this.filterCombobox = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.printContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.printTopic = new System.Windows.Forms.ToolStripMenuItem();
      this.printTopicAndSubTopics = new System.Windows.Forms.ToolStripMenuItem();
      this.infoLabel = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.printContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.infoLabel);
      this.panel1.Controls.Add(this.filterCombobox);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(366, 78);
      this.panel1.TabIndex = 0;
      // 
      // panTocControl
      // 
      this.panTocControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panTocControl.Location = new System.Drawing.Point(0, 78);
      this.panTocControl.Name = "panTocControl";
      this.panTocControl.Size = new System.Drawing.Size(366, 531);
      this.panTocControl.TabIndex = 1;
      // 
      // filterCombobox
      // 
      this.filterCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.filterCombobox.FormattingEnabled = true;
      this.filterCombobox.Location = new System.Drawing.Point(9, 27);
      this.filterCombobox.Name = "filterCombobox";
      this.filterCombobox.Size = new System.Drawing.Size(341, 23);
      this.filterCombobox.TabIndex = 3;
      this.filterCombobox.SelectedIndexChanged += new EventHandler(this.FilterChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(60, 15);
      this.label1.TabIndex = 2;
      this.label1.Text = "Filtered by:";
      // 
      // printContextMenu
      // 
      this.printContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printTopic,
            this.printTopicAndSubTopics});
      this.printContextMenu.Name = "printContextMenu";
      this.printContextMenu.Size = new System.Drawing.Size(168, 48);
      // 
      // printTopic
      // 
      this.printTopic.Name = "printTopic";
      this.printTopic.Size = new System.Drawing.Size(167, 22);
      this.printTopic.Text = "Print topic...";
      this.printTopic.Click += new EventHandler(this.PrintTopic);
      // 
      // printTopicAndSubTopics
      // 
      this.printTopicAndSubTopics.Name = "printTopicAndSubTopics";
      this.printTopicAndSubTopics.Size = new System.Drawing.Size(167, 22);
      this.printTopicAndSubTopics.Text = "Print subtopics...";
      this.printTopicAndSubTopics.Click += new EventHandler(this.PrintTopicAndSubTopics);
      // 
      // infoLabel
      // 
      this.infoLabel.AutoSize = true;
      this.infoLabel.Location = new System.Drawing.Point(6, 60);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(60, 15);
      this.infoLabel.TabIndex = 4;
      this.infoLabel.Text = "Filtered by:";
      // 
      // MSHelp2TocControl2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panTocControl);
      this.Controls.Add(this.panel1);
      this.Name = "MSHelp2TocControl2";
      this.Size = new System.Drawing.Size(366, 609);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.printContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.ComboBox filterCombobox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panTocControl;
    private System.Windows.Forms.ContextMenuStrip printContextMenu;
    private System.Windows.Forms.ToolStripMenuItem printTopic;
    private System.Windows.Forms.ToolStripMenuItem printTopicAndSubTopics;
    private System.Windows.Forms.Label infoLabel;
  }
}
