using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DemoApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private RichTextBoxExtended.RichTextBoxExtended richTextBoxExtended1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.richTextBoxExtended1 = new RichTextBoxExtended.RichTextBoxExtended();
			this.SuspendLayout();
			// 
			// richTextBoxExtended1
			// 
			this.richTextBoxExtended1.AcceptsTab = true;
			this.richTextBoxExtended1.AutoWordSelection = true;
			this.richTextBoxExtended1.Controls.Add(this.richTextBoxExtended1.RichTextBox);
			this.richTextBoxExtended1.Controls.Add(this.richTextBoxExtended1.Toolbar);
			this.richTextBoxExtended1.DetectURLs = true;
			this.richTextBoxExtended1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxExtended1.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxExtended1.Name = "richTextBoxExtended1";
			this.richTextBoxExtended1.ReadOnly = false;
			// 
			// richTextBoxExtended1.RichTextBox
			// 
			this.richTextBoxExtended1.RichTextBox.AcceptsTab = true;
			this.richTextBoxExtended1.RichTextBox.AutoWordSelection = true;
			this.richTextBoxExtended1.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxExtended1.RichTextBox.Location = new System.Drawing.Point(0, 26);
			this.richTextBoxExtended1.RichTextBox.Name = "rtb1";
			this.richTextBoxExtended1.RichTextBox.Size = new System.Drawing.Size(504, 196);
			this.richTextBoxExtended1.RichTextBox.TabIndex = 1;
			this.richTextBoxExtended1.ShowBold = true;
			this.richTextBoxExtended1.ShowCenterJustify = true;
			this.richTextBoxExtended1.ShowColors = true;
			this.richTextBoxExtended1.ShowCopy = true;
			this.richTextBoxExtended1.ShowCut = true;
			this.richTextBoxExtended1.ShowFont = true;
			this.richTextBoxExtended1.ShowFontSize = true;
			this.richTextBoxExtended1.ShowItalic = true;
			this.richTextBoxExtended1.ShowLeftJustify = true;
			this.richTextBoxExtended1.ShowOpen = true;
			this.richTextBoxExtended1.ShowPaste = true;
			this.richTextBoxExtended1.ShowRedo = true;
			this.richTextBoxExtended1.ShowRightJustify = true;
			this.richTextBoxExtended1.ShowSave = true;
			this.richTextBoxExtended1.ShowStamp = true;
			this.richTextBoxExtended1.ShowStrikeout = true;
			this.richTextBoxExtended1.ShowToolBarText = false;
			this.richTextBoxExtended1.ShowUnderline = true;
			this.richTextBoxExtended1.ShowUndo = true;
			this.richTextBoxExtended1.Size = new System.Drawing.Size(504, 222);
			this.richTextBoxExtended1.StampAction = RichTextBoxExtended.StampActions.EditedBy;
			this.richTextBoxExtended1.StampColor = System.Drawing.Color.Blue;
			this.richTextBoxExtended1.TabIndex = 0;
			// 
			// richTextBoxExtended1.Toolbar
			// 
			this.richTextBoxExtended1.Toolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.richTextBoxExtended1.Toolbar.ButtonSize = new System.Drawing.Size(16, 16);
			this.richTextBoxExtended1.Toolbar.Divider = false;
			this.richTextBoxExtended1.Toolbar.DropDownArrows = true;
			this.richTextBoxExtended1.Toolbar.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxExtended1.Toolbar.Name = "tb1";
			this.richTextBoxExtended1.Toolbar.ShowToolTips = true;
			this.richTextBoxExtended1.Toolbar.Size = new System.Drawing.Size(504, 26);
			this.richTextBoxExtended1.Toolbar.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 222);
			this.Controls.Add(this.richTextBoxExtended1);
			this.Name = "Form1";
			this.Text = "RichTextBoxExtended Demo Application";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
	}
}
