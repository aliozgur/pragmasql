#region Copyright And Revision History

/*---------------------------------------------------------------------------

	MainForm.cs
	Copyright © 2002 Bill Menees.  All rights reserved.
	Bill@Menees.com

	Who		When		What
	-------	----------	-----------------------------------------------------
	BMenees	9.22.2002	Created.

-----------------------------------------------------------------------------*/

#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Menees.DiffUtils;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;

namespace TestDiffUtils
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabBinary;
		private System.Windows.Forms.Button btnBinaryFile;
		private System.Windows.Forms.Button btnBinaryChar;
		private System.Windows.Forms.TextBox txtBinary;
		private System.Windows.Forms.TextBox edtBinaryVersion;
		private System.Windows.Forms.TextBox edtBinaryBase;
		private System.Windows.Forms.TabPage tabMyers;
		private System.Windows.Forms.Button btnTestMyers;
		private System.Windows.Forms.TextBox txtMyers;
		private System.Windows.Forms.TextBox edtMyersTwo;
		private System.Windows.Forms.TextBox edtMyersOne;
		private System.Windows.Forms.TabPage tabText;
		private System.Windows.Forms.TextBox edtTextTwo;
		private System.Windows.Forms.TextBox edtTextOne;
		private System.Windows.Forms.Button btnTestText;
		private System.Windows.Forms.Button btnTextOne;
		private System.Windows.Forms.Button btnTextTwo;
		private System.Windows.Forms.OpenFileDialog OpenDlg;
		private System.Windows.Forms.CheckBox chkIgnoreCase;
		private System.Windows.Forms.CheckBox chkIgnoreWhitespace;
		private System.Windows.Forms.ComboBox cbHashType;
		private System.Windows.Forms.TabPage tabTesting;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.Label lblScroll;
		private System.Windows.Forms.Panel panel1;
		private Menees.DiffUtils.Controls.DiffControl diffControl1;
		private System.Windows.Forms.CheckBox chkXML;
		private System.Windows.Forms.GroupBox grpXML;
		private System.Windows.Forms.ComboBox cbXMLWhitespace;
		private System.Windows.Forms.GroupBox grpDiff;
		private System.Windows.Forms.TabPage tabDir;
		private System.Windows.Forms.TextBox edtDir1;
		private System.Windows.Forms.TextBox edtDir2;
		private Menees.DiffUtils.Controls.DirDiffControl dirDiffControl1;
		private System.Windows.Forms.Button btnDirCompare;
		private System.Windows.Forms.CheckBox chkShowOnlyInA;
		private System.Windows.Forms.CheckBox chkShowOnlyInB;
		private System.Windows.Forms.CheckBox chkShowDifferent;
		private System.Windows.Forms.CheckBox chkShowSame;
		private System.Windows.Forms.CheckBox chkRecursive;
		private System.Windows.Forms.CheckBox chkFullRowSelect;
		private System.Windows.Forms.CheckBox chkIgnoreDirectoryComparison;
		private System.Windows.Forms.Button btnDocDiff;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabText = new System.Windows.Forms.TabPage();
			this.btnTestText = new System.Windows.Forms.Button();
			this.grpDiff = new System.Windows.Forms.GroupBox();
			this.chkIgnoreWhitespace = new System.Windows.Forms.CheckBox();
			this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
			this.cbHashType = new System.Windows.Forms.ComboBox();
			this.grpXML = new System.Windows.Forms.GroupBox();
			this.cbXMLWhitespace = new System.Windows.Forms.ComboBox();
			this.chkXML = new System.Windows.Forms.CheckBox();
			this.edtTextOne = new System.Windows.Forms.TextBox();
			this.edtTextTwo = new System.Windows.Forms.TextBox();
			this.diffControl1 = new Menees.DiffUtils.Controls.DiffControl();
			this.btnTextTwo = new System.Windows.Forms.Button();
			this.btnTextOne = new System.Windows.Forms.Button();
			this.tabDir = new System.Windows.Forms.TabPage();
			this.chkIgnoreDirectoryComparison = new System.Windows.Forms.CheckBox();
			this.chkFullRowSelect = new System.Windows.Forms.CheckBox();
			this.chkRecursive = new System.Windows.Forms.CheckBox();
			this.chkShowSame = new System.Windows.Forms.CheckBox();
			this.chkShowDifferent = new System.Windows.Forms.CheckBox();
			this.chkShowOnlyInB = new System.Windows.Forms.CheckBox();
			this.chkShowOnlyInA = new System.Windows.Forms.CheckBox();
			this.btnDirCompare = new System.Windows.Forms.Button();
			this.dirDiffControl1 = new Menees.DiffUtils.Controls.DirDiffControl();
			this.edtDir1 = new System.Windows.Forms.TextBox();
			this.edtDir2 = new System.Windows.Forms.TextBox();
			this.tabMyers = new System.Windows.Forms.TabPage();
			this.edtMyersTwo = new System.Windows.Forms.TextBox();
			this.edtMyersOne = new System.Windows.Forms.TextBox();
			this.txtMyers = new System.Windows.Forms.TextBox();
			this.btnTestMyers = new System.Windows.Forms.Button();
			this.tabBinary = new System.Windows.Forms.TabPage();
			this.txtBinary = new System.Windows.Forms.TextBox();
			this.btnBinaryChar = new System.Windows.Forms.Button();
			this.btnBinaryFile = new System.Windows.Forms.Button();
			this.edtBinaryVersion = new System.Windows.Forms.TextBox();
			this.edtBinaryBase = new System.Windows.Forms.TextBox();
			this.tabTesting = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblScroll = new System.Windows.Forms.Label();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.OpenDlg = new System.Windows.Forms.OpenFileDialog();
			this.btnDocDiff = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabText.SuspendLayout();
			this.grpDiff.SuspendLayout();
			this.grpXML.SuspendLayout();
			this.tabDir.SuspendLayout();
			this.tabMyers.SuspendLayout();
			this.tabBinary.SuspendLayout();
			this.tabTesting.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabText);
			this.tabControl1.Controls.Add(this.tabDir);
			this.tabControl1.Controls.Add(this.tabMyers);
			this.tabControl1.Controls.Add(this.tabBinary);
			this.tabControl1.Controls.Add(this.tabTesting);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(588, 421);
			this.tabControl1.TabIndex = 0;
			// 
			// tabText
			// 
			this.tabText.Controls.Add(this.btnTestText);
			this.tabText.Controls.Add(this.grpDiff);
			this.tabText.Controls.Add(this.grpXML);
			this.tabText.Controls.Add(this.edtTextOne);
			this.tabText.Controls.Add(this.edtTextTwo);
			this.tabText.Controls.Add(this.diffControl1);
			this.tabText.Controls.Add(this.btnTextTwo);
			this.tabText.Controls.Add(this.btnTextOne);
			this.tabText.Location = new System.Drawing.Point(4, 22);
			this.tabText.Name = "tabText";
			this.tabText.Size = new System.Drawing.Size(580, 395);
			this.tabText.TabIndex = 2;
			this.tabText.Text = "TextDiff";
			// 
			// btnTestText
			// 
			this.btnTestText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTestText.Location = new System.Drawing.Point(496, 68);
			this.btnTestText.Name = "btnTestText";
			this.btnTestText.Size = new System.Drawing.Size(75, 72);
			this.btnTestText.TabIndex = 4;
			this.btnTestText.Text = "&Compare";
			this.btnTestText.Click += new System.EventHandler(this.btnTestText_Click);
			// 
			// grpDiff
			// 
			this.grpDiff.Controls.Add(this.chkIgnoreWhitespace);
			this.grpDiff.Controls.Add(this.chkIgnoreCase);
			this.grpDiff.Controls.Add(this.cbHashType);
			this.grpDiff.Location = new System.Drawing.Point(8, 64);
			this.grpDiff.Name = "grpDiff";
			this.grpDiff.Size = new System.Drawing.Size(236, 76);
			this.grpDiff.TabIndex = 11;
			this.grpDiff.TabStop = false;
			this.grpDiff.Text = "Compare Options";
			// 
			// chkIgnoreWhitespace
			// 
			this.chkIgnoreWhitespace.Location = new System.Drawing.Point(104, 20);
			this.chkIgnoreWhitespace.Name = "chkIgnoreWhitespace";
			this.chkIgnoreWhitespace.Size = new System.Drawing.Size(120, 16);
			this.chkIgnoreWhitespace.TabIndex = 6;
			this.chkIgnoreWhitespace.Text = "Ignore Whitespace";
			// 
			// chkIgnoreCase
			// 
			this.chkIgnoreCase.Location = new System.Drawing.Point(12, 20);
			this.chkIgnoreCase.Name = "chkIgnoreCase";
			this.chkIgnoreCase.Size = new System.Drawing.Size(96, 16);
			this.chkIgnoreCase.TabIndex = 5;
			this.chkIgnoreCase.Text = "Ignore Case";
			// 
			// cbHashType
			// 
			this.cbHashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbHashType.Items.AddRange(new object[] {
															"Compare By .NET HashCode",
															"Compare By CRC32",
															"Compare By Unique Index"});
			this.cbHashType.Location = new System.Drawing.Point(12, 44);
			this.cbHashType.Name = "cbHashType";
			this.cbHashType.Size = new System.Drawing.Size(212, 21);
			this.cbHashType.TabIndex = 7;
			// 
			// grpXML
			// 
			this.grpXML.Controls.Add(this.cbXMLWhitespace);
			this.grpXML.Controls.Add(this.chkXML);
			this.grpXML.Location = new System.Drawing.Point(252, 64);
			this.grpXML.Name = "grpXML";
			this.grpXML.Size = new System.Drawing.Size(236, 76);
			this.grpXML.TabIndex = 10;
			this.grpXML.TabStop = false;
			this.grpXML.Text = "XML Options";
			// 
			// cbXMLWhitespace
			// 
			this.cbXMLWhitespace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbXMLWhitespace.Items.AddRange(new object[] {
																 "Keep All Whitespace Nodes",
																 "Keep Significant Whitespace Nodes",
																 "Keep No Whitespace Nodes"});
			this.cbXMLWhitespace.Location = new System.Drawing.Point(12, 44);
			this.cbXMLWhitespace.Name = "cbXMLWhitespace";
			this.cbXMLWhitespace.Size = new System.Drawing.Size(212, 21);
			this.cbXMLWhitespace.TabIndex = 10;
			// 
			// chkXML
			// 
			this.chkXML.Location = new System.Drawing.Point(12, 20);
			this.chkXML.Name = "chkXML";
			this.chkXML.Size = new System.Drawing.Size(128, 16);
			this.chkXML.TabIndex = 9;
			this.chkXML.Text = "Treat Data As XML";
			// 
			// edtTextOne
			// 
			this.edtTextOne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtTextOne.Location = new System.Drawing.Point(8, 12);
			this.edtTextOne.Name = "edtTextOne";
			this.edtTextOne.Size = new System.Drawing.Size(524, 20);
			this.edtTextOne.TabIndex = 0;
			this.edtTextOne.Text = "C:\\XMLFile1.xml";
			// 
			// edtTextTwo
			// 
			this.edtTextTwo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtTextTwo.Location = new System.Drawing.Point(8, 40);
			this.edtTextTwo.Name = "edtTextTwo";
			this.edtTextTwo.Size = new System.Drawing.Size(524, 20);
			this.edtTextTwo.TabIndex = 2;
			this.edtTextTwo.Text = "C:\\XMLFile2.xml";
			// 
			// diffControl1
			// 
			this.diffControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.diffControl1.Location = new System.Drawing.Point(8, 144);
			this.diffControl1.Name = "diffControl1";
			this.diffControl1.ShowWhitespaceInLineDiff = true;
			this.diffControl1.Size = new System.Drawing.Size(564, 244);
			this.diffControl1.TabIndex = 8;
			this.diffControl1.ViewFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			// 
			// btnTextTwo
			// 
			this.btnTextTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTextTwo.Location = new System.Drawing.Point(540, 40);
			this.btnTextTwo.Name = "btnTextTwo";
			this.btnTextTwo.Size = new System.Drawing.Size(32, 20);
			this.btnTextTwo.TabIndex = 3;
			this.btnTextTwo.Text = "...";
			this.btnTextTwo.Click += new System.EventHandler(this.btnTextTwo_Click);
			// 
			// btnTextOne
			// 
			this.btnTextOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTextOne.Location = new System.Drawing.Point(540, 12);
			this.btnTextOne.Name = "btnTextOne";
			this.btnTextOne.Size = new System.Drawing.Size(32, 20);
			this.btnTextOne.TabIndex = 1;
			this.btnTextOne.Text = "...";
			this.btnTextOne.Click += new System.EventHandler(this.btnTextOne_Click);
			// 
			// tabDir
			// 
			this.tabDir.Controls.Add(this.chkIgnoreDirectoryComparison);
			this.tabDir.Controls.Add(this.chkFullRowSelect);
			this.tabDir.Controls.Add(this.chkRecursive);
			this.tabDir.Controls.Add(this.chkShowSame);
			this.tabDir.Controls.Add(this.chkShowDifferent);
			this.tabDir.Controls.Add(this.chkShowOnlyInB);
			this.tabDir.Controls.Add(this.chkShowOnlyInA);
			this.tabDir.Controls.Add(this.btnDirCompare);
			this.tabDir.Controls.Add(this.dirDiffControl1);
			this.tabDir.Controls.Add(this.edtDir1);
			this.tabDir.Controls.Add(this.edtDir2);
			this.tabDir.Location = new System.Drawing.Point(4, 22);
			this.tabDir.Name = "tabDir";
			this.tabDir.Size = new System.Drawing.Size(580, 395);
			this.tabDir.TabIndex = 4;
			this.tabDir.Text = "DirDiff";
			// 
			// chkIgnoreDirectoryComparison
			// 
			this.chkIgnoreDirectoryComparison.Checked = true;
			this.chkIgnoreDirectoryComparison.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIgnoreDirectoryComparison.Location = new System.Drawing.Point(236, 88);
			this.chkIgnoreDirectoryComparison.Name = "chkIgnoreDirectoryComparison";
			this.chkIgnoreDirectoryComparison.Size = new System.Drawing.Size(168, 16);
			this.chkIgnoreDirectoryComparison.TabIndex = 7;
			this.chkIgnoreDirectoryComparison.Text = "Ignore Directory Comparison";
			// 
			// chkFullRowSelect
			// 
			this.chkFullRowSelect.Checked = true;
			this.chkFullRowSelect.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFullRowSelect.Location = new System.Drawing.Point(356, 68);
			this.chkFullRowSelect.Name = "chkFullRowSelect";
			this.chkFullRowSelect.Size = new System.Drawing.Size(104, 16);
			this.chkFullRowSelect.TabIndex = 8;
			this.chkFullRowSelect.Text = "Full Row Select";
			this.chkFullRowSelect.CheckedChanged += new System.EventHandler(this.chkFullRowSelect_CheckedChanged);
			// 
			// chkRecursive
			// 
			this.chkRecursive.Checked = true;
			this.chkRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRecursive.Location = new System.Drawing.Point(236, 68);
			this.chkRecursive.Name = "chkRecursive";
			this.chkRecursive.Size = new System.Drawing.Size(104, 16);
			this.chkRecursive.TabIndex = 6;
			this.chkRecursive.Text = "Recursive";
			// 
			// chkShowSame
			// 
			this.chkShowSame.Checked = true;
			this.chkShowSame.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowSame.Location = new System.Drawing.Point(124, 88);
			this.chkShowSame.Name = "chkShowSame";
			this.chkShowSame.Size = new System.Drawing.Size(104, 16);
			this.chkShowSame.TabIndex = 5;
			this.chkShowSame.Text = "Show If Same";
			// 
			// chkShowDifferent
			// 
			this.chkShowDifferent.Checked = true;
			this.chkShowDifferent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowDifferent.Location = new System.Drawing.Point(124, 68);
			this.chkShowDifferent.Name = "chkShowDifferent";
			this.chkShowDifferent.Size = new System.Drawing.Size(116, 16);
			this.chkShowDifferent.TabIndex = 4;
			this.chkShowDifferent.Text = "Show If Different";
			// 
			// chkShowOnlyInB
			// 
			this.chkShowOnlyInB.Checked = true;
			this.chkShowOnlyInB.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowOnlyInB.Location = new System.Drawing.Point(8, 88);
			this.chkShowOnlyInB.Name = "chkShowOnlyInB";
			this.chkShowOnlyInB.Size = new System.Drawing.Size(116, 16);
			this.chkShowOnlyInB.TabIndex = 3;
			this.chkShowOnlyInB.Text = "Show If Only In B";
			// 
			// chkShowOnlyInA
			// 
			this.chkShowOnlyInA.Checked = true;
			this.chkShowOnlyInA.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowOnlyInA.Location = new System.Drawing.Point(8, 68);
			this.chkShowOnlyInA.Name = "chkShowOnlyInA";
			this.chkShowOnlyInA.Size = new System.Drawing.Size(120, 16);
			this.chkShowOnlyInA.TabIndex = 2;
			this.chkShowOnlyInA.Text = "Show If Only In A";
			// 
			// btnDirCompare
			// 
			this.btnDirCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDirCompare.Location = new System.Drawing.Point(496, 72);
			this.btnDirCompare.Name = "btnDirCompare";
			this.btnDirCompare.TabIndex = 9;
			this.btnDirCompare.Text = "&Compare";
			this.btnDirCompare.Click += new System.EventHandler(this.btnDirCompare_Click);
			// 
			// dirDiffControl1
			// 
			this.dirDiffControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.dirDiffControl1.Location = new System.Drawing.Point(8, 108);
			this.dirDiffControl1.Name = "dirDiffControl1";
			this.dirDiffControl1.Size = new System.Drawing.Size(564, 280);
			this.dirDiffControl1.TabIndex = 10;
			this.dirDiffControl1.ShowFileDifferences += new Menees.DiffUtils.DifferenceEventHandler(this.dirDiffControl1_ShowFileDifferences);
			// 
			// edtDir1
			// 
			this.edtDir1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtDir1.Location = new System.Drawing.Point(8, 12);
			this.edtDir1.Name = "edtDir1";
			this.edtDir1.Size = new System.Drawing.Size(564, 20);
			this.edtDir1.TabIndex = 0;
			this.edtDir1.Text = "C:\\Projects\\CSharp\\MeneesDiffUtilsOld";
			// 
			// edtDir2
			// 
			this.edtDir2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtDir2.Location = new System.Drawing.Point(8, 40);
			this.edtDir2.Name = "edtDir2";
			this.edtDir2.Size = new System.Drawing.Size(564, 20);
			this.edtDir2.TabIndex = 1;
			this.edtDir2.Text = "C:\\Projects\\CSharp\\MeneesDiffUtils";
			// 
			// tabMyers
			// 
			this.tabMyers.Controls.Add(this.edtMyersTwo);
			this.tabMyers.Controls.Add(this.edtMyersOne);
			this.tabMyers.Controls.Add(this.txtMyers);
			this.tabMyers.Controls.Add(this.btnTestMyers);
			this.tabMyers.Location = new System.Drawing.Point(4, 22);
			this.tabMyers.Name = "tabMyers";
			this.tabMyers.Size = new System.Drawing.Size(580, 395);
			this.tabMyers.TabIndex = 1;
			this.tabMyers.Text = "MyersDiff";
			// 
			// edtMyersTwo
			// 
			this.edtMyersTwo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtMyersTwo.Location = new System.Drawing.Point(8, 40);
			this.edtMyersTwo.Name = "edtMyersTwo";
			this.edtMyersTwo.Size = new System.Drawing.Size(564, 20);
			this.edtMyersTwo.TabIndex = 1;
			this.edtMyersTwo.Text = "bcd";
			// 
			// edtMyersOne
			// 
			this.edtMyersOne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtMyersOne.Location = new System.Drawing.Point(8, 12);
			this.edtMyersOne.Name = "edtMyersOne";
			this.edtMyersOne.Size = new System.Drawing.Size(564, 20);
			this.edtMyersOne.TabIndex = 0;
			this.edtMyersOne.Text = "ad";
			// 
			// txtMyers
			// 
			this.txtMyers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMyers.Location = new System.Drawing.Point(8, 100);
			this.txtMyers.Multiline = true;
			this.txtMyers.Name = "txtMyers";
			this.txtMyers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMyers.Size = new System.Drawing.Size(564, 288);
			this.txtMyers.TabIndex = 3;
			this.txtMyers.Text = "";
			// 
			// btnTestMyers
			// 
			this.btnTestMyers.Location = new System.Drawing.Point(8, 68);
			this.btnTestMyers.Name = "btnTestMyers";
			this.btnTestMyers.TabIndex = 2;
			this.btnTestMyers.Text = "Test";
			this.btnTestMyers.Click += new System.EventHandler(this.btnTestMyers_Click);
			// 
			// tabBinary
			// 
			this.tabBinary.Controls.Add(this.btnDocDiff);
			this.tabBinary.Controls.Add(this.txtBinary);
			this.tabBinary.Controls.Add(this.btnBinaryChar);
			this.tabBinary.Controls.Add(this.btnBinaryFile);
			this.tabBinary.Controls.Add(this.edtBinaryVersion);
			this.tabBinary.Controls.Add(this.edtBinaryBase);
			this.tabBinary.Location = new System.Drawing.Point(4, 22);
			this.tabBinary.Name = "tabBinary";
			this.tabBinary.Size = new System.Drawing.Size(580, 395);
			this.tabBinary.TabIndex = 0;
			this.tabBinary.Text = "BinaryDiff";
			// 
			// txtBinary
			// 
			this.txtBinary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBinary.Location = new System.Drawing.Point(8, 100);
			this.txtBinary.Multiline = true;
			this.txtBinary.Name = "txtBinary";
			this.txtBinary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtBinary.Size = new System.Drawing.Size(564, 288);
			this.txtBinary.TabIndex = 4;
			this.txtBinary.Text = "";
			// 
			// btnBinaryChar
			// 
			this.btnBinaryChar.Location = new System.Drawing.Point(92, 68);
			this.btnBinaryChar.Name = "btnBinaryChar";
			this.btnBinaryChar.TabIndex = 3;
			this.btnBinaryChar.Text = "Char Diff";
			this.btnBinaryChar.Click += new System.EventHandler(this.btnBinaryChar_Click);
			// 
			// btnBinaryFile
			// 
			this.btnBinaryFile.Location = new System.Drawing.Point(8, 68);
			this.btnBinaryFile.Name = "btnBinaryFile";
			this.btnBinaryFile.TabIndex = 2;
			this.btnBinaryFile.Text = "File Diff";
			this.btnBinaryFile.Click += new System.EventHandler(this.btnBinaryFile_Click);
			// 
			// edtBinaryVersion
			// 
			this.edtBinaryVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtBinaryVersion.Location = new System.Drawing.Point(8, 40);
			this.edtBinaryVersion.Name = "edtBinaryVersion";
			this.edtBinaryVersion.Size = new System.Drawing.Size(564, 20);
			this.edtBinaryVersion.TabIndex = 1;
			this.edtBinaryVersion.Text = "ABCDQEFGH";
			// 
			// edtBinaryBase
			// 
			this.edtBinaryBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.edtBinaryBase.Location = new System.Drawing.Point(8, 12);
			this.edtBinaryBase.Name = "edtBinaryBase";
			this.edtBinaryBase.Size = new System.Drawing.Size(564, 20);
			this.edtBinaryBase.TabIndex = 0;
			this.edtBinaryBase.Text = "ABCDEFGH";
			// 
			// tabTesting
			// 
			this.tabTesting.Controls.Add(this.panel1);
			this.tabTesting.Controls.Add(this.lblScroll);
			this.tabTesting.Controls.Add(this.vScrollBar1);
			this.tabTesting.Controls.Add(this.listBox1);
			this.tabTesting.Location = new System.Drawing.Point(4, 22);
			this.tabTesting.Name = "tabTesting";
			this.tabTesting.Size = new System.Drawing.Size(580, 395);
			this.tabTesting.TabIndex = 3;
			this.tabTesting.Text = "Testing";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.AutoScrollMinSize = new System.Drawing.Size(200, 200);
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(4, 188);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(568, 200);
			this.panel1.TabIndex = 3;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// lblScroll
			// 
			this.lblScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblScroll.Location = new System.Drawing.Point(4, 4);
			this.lblScroll.Name = "lblScroll";
			this.lblScroll.Size = new System.Drawing.Size(568, 23);
			this.lblScroll.TabIndex = 2;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar1.Location = new System.Drawing.Point(536, 32);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(36, 148);
			this.vScrollBar1.TabIndex = 1;
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listBox1.HorizontalExtent = 500;
			this.listBox1.HorizontalScrollbar = true;
			this.listBox1.Items.AddRange(new object[] {
														  "case EditType.Change:",
														  "strText = String.Format(\"Changed {0} lines at {1}/{2}\\r\\n\", E.Length, E.StartA, E" +
														  ".StartB);",
														  "break;",
														  "case EditType.Delete:",
														  "strText = String.Format(\"Deleted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Insert:",
														  "strText = String.Format(\"Inserted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Change:",
														  "strText = String.Format(\"Changed {0} lines at {1}/{2}\\r\\n\", E.Length, E.StartA, E" +
														  ".StartB);",
														  "break;",
														  "case EditType.Delete:",
														  "strText = String.Format(\"Deleted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Insert:",
														  "strText = String.Format(\"Inserted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Change:",
														  "strText = String.Format(\"Changed {0} lines at {1}/{2}\\r\\n\", E.Length, E.StartA, E" +
														  ".StartB);",
														  "break;",
														  "case EditType.Delete:",
														  "strText = String.Format(\"Deleted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Insert:",
														  "strText = String.Format(\"Inserted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Change:",
														  "strText = String.Format(\"Changed {0} lines at {1}/{2}\\r\\n\", E.Length, E.StartA, E" +
														  ".StartB);",
														  "break;",
														  "case EditType.Delete:",
														  "strText = String.Format(\"Deleted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Insert:",
														  "strText = String.Format(\"Inserted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Change:",
														  "strText = String.Format(\"Changed {0} lines at {1}/{2}\\r\\n\", E.Length, E.StartA, E" +
														  ".StartB);",
														  "break;",
														  "case EditType.Delete:",
														  "strText = String.Format(\"Deleted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;",
														  "case EditType.Insert:",
														  "strText = String.Format(\"Inserted {0} lines at {1}\\r\\n\", E.Length, E.StartA);",
														  "break;"});
			this.listBox1.Location = new System.Drawing.Point(4, 32);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(524, 147);
			this.listBox1.TabIndex = 0;
			this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
			// 
			// OpenDlg
			// 
			this.OpenDlg.Filter = "All Files (*.*)|*.*";
			// 
			// btnDocDiff
			// 
			this.btnDocDiff.Location = new System.Drawing.Point(176, 68);
			this.btnDocDiff.Name = "btnDocDiff";
			this.btnDocDiff.TabIndex = 5;
			this.btnDocDiff.Text = "Doc Diff";
			this.btnDocDiff.Click += new System.EventHandler(this.btnDocDiff_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(588, 421);
			this.Controls.Add(this.tabControl1);
			this.Name = "MainForm";
			this.Text = "Test DiffUtils";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabText.ResumeLayout(false);
			this.grpDiff.ResumeLayout(false);
			this.grpXML.ResumeLayout(false);
			this.tabDir.ResumeLayout(false);
			this.tabMyers.ResumeLayout(false);
			this.tabBinary.ResumeLayout(false);
			this.tabTesting.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void btnBinaryFile_Click(object sender, System.EventArgs e)
		{
			if (File.Exists(edtBinaryBase.Text) && File.Exists(edtBinaryVersion.Text))
			{
				FileStream Base = new FileStream(edtBinaryBase.Text, FileMode.Open);
				FileStream Version = new FileStream(edtBinaryVersion.Text, FileMode.Open);
				DoBinaryDiff(Base, Version, 8);
				Base.Close();
				Version.Close();
			}
		}

		private void btnBinaryChar_Click(object sender, System.EventArgs e)
		{
			ASCIIEncoding E = new ASCIIEncoding();
			MemoryStream Base = new MemoryStream(E.GetBytes(edtBinaryBase.Text));
			MemoryStream Version = new MemoryStream(E.GetBytes(edtBinaryVersion.Text));
			DoBinaryDiff(Base, Version, 1);
			Base.Close();
			Version.Close();
		}

		private void DoBinaryDiff(Stream Base, Stream Version, int iFootprintLength)
		{
			BinaryDiff Diff = new BinaryDiff();
			Diff.FootprintLength = iFootprintLength;
			AddCopyList List = Diff.Execute(Base, Version);
			ShowAddCopyList(List);
		}

		private void ShowAddCopyList(AddCopyList List)
		{
			txtBinary.Text = "Total Byte Length: " + List.TotalByteLength.ToString() + "\r\n";
			foreach(object o in List)
			{
				string strText;
				if (o is Addition)
				{
					Addition A = (Addition)o;
					strText = String.Format("Add bytes: {0}\r\n", GetByteText(A.arBytes));
				}
				else
				{
					Copy C = (Copy)o;
					strText = String.Format("Copy from base {0} to {1} (length {2})\r\n", C.iBaseOffset, C.iBaseOffset + C.iLength - 1, C.iLength);
				}

				txtBinary.AppendText(strText);
			}

			//Get the GDIFF representation too
			MemoryStream Diff = new MemoryStream();
			List.GDIFF(Diff);
			byte[] arDiffBytes = new byte[Diff.Length];
			Diff.Seek(0, SeekOrigin.Begin);
			Diff.Read(arDiffBytes, 0, arDiffBytes.Length);
			txtBinary.AppendText(String.Format("\r\nGDIFF: {0}", GetByteText(arDiffBytes)));
		}

		private string GetByteText(byte[] arBytes)
		{
			StringBuilder SB = new StringBuilder(arBytes.Length * 3);
			foreach(byte B in arBytes)
			{
				SB.AppendFormat("{0:X2} ", B);
			}
			return SB.ToString();
		}

		private void btnTestMyers_Click(object sender, System.EventArgs e)
		{
			txtMyers.Text = "";

			DoMyersTest(GetIntArray("a"), GetIntArray("a"));

			int[] A3 = { 97, 98, 99 }; //abc
			int[] B3 = { 97, 99 }; //ac
			DoMyersTest(A3, B3);

			int[] A2 = { 97, 98, 99 }; //abc
			int[] B2 = { 98, 97, 99 }; //bac
			DoMyersTest(A2, B2);

			int[] A = { 97, 98, 99, 97, 98, 98, 97 }; //abcabba
			int[] B = { 99, 98, 97, 98, 97, 99 }; //cbabac
			DoMyersTest(A, B);

			DoMyersTest(GetIntArray("ad"), GetIntArray("bcd"));
			DoMyersTest(GetIntArray("Testing"), GetIntArray("BoringTest"));
			DoMyersTest(GetIntArray(edtMyersOne.Text), GetIntArray(edtMyersTwo.Text));
		}

		private void DoMyersTest(int[] A, int[] B)
		{
			MyersDiff Diff = new MyersDiff(A, B);

			int iSESLength = Diff.GetSESLength();
			int iRevSESLength = Diff.GetReverseSESLength();
			Debug.Assert(iSESLength == iRevSESLength, "The forward and reverse SES lengths must be equal.");

			int[] LCS = Diff.GetLCS();
			Debug.Assert(Diff.GetLCSLength() == LCS.Length, "SES Length must equal N+M-2*(LCS Length)");
			string strLCS = GetStringFromChars(LCS);

			EditScript Script = Diff.Execute();
			CheckEditScript(Script, iSESLength);
            
			txtMyers.AppendText(String.Format("A:\t{0}\r\nB:\t{1}\r\nLCS:\t{2}\r\nSES Length:\t{3}\r\nRevSES Length:\t{4}\r\nEdit Script:\t{5}\r\n\r\n", 
				GetStringFromChars(A), GetStringFromChars(B), strLCS, iSESLength, iRevSESLength, GetEditScriptText(Script)));
		}

		private string GetStringFromChars(int[] arCharCodes)
		{
			StringBuilder SB = new StringBuilder(arCharCodes.Length);
			foreach (int i in arCharCodes)
			{
				SB.Append((char)i);
			}
			return SB.ToString();
		}

		private int[] GetIntArray(string strText)
		{
			int[] arData = new int[strText.Length];
			for(int i = 0; i < strText.Length; i++)
			{
				arData[i] = strText[i];
			}

			return arData;
		}

		private string GetEditScriptText(EditScript Script)
		{
			StringBuilder SB = new StringBuilder(12*Script.Count);

			foreach(Edit D in Script)
			{
				SB.Append("(");
				switch(D.Type)
				{
					case EditType.Delete:
						SB.Append("-");
						break;
					case EditType.Insert:
						SB.Append("+");
						break;
					case EditType.Change:
						SB.Append("~");
						break;
				}
				SB.AppendFormat(",{0},{1},{2}) ", D.StartA, D.StartB, D.Length);
			}

			return SB.ToString();
		}

		[Conditional("DEBUG")]
		private void CheckEditScript(EditScript Script, int iRequiredLength)
		{
			int iActualLength = 0;
			foreach(Edit D in Script)
			{
				if (D.Type == EditType.Change)
					iActualLength += 2*D.Length;
				else
					iActualLength += D.Length;
			}

			Debug.Assert(iActualLength == iRequiredLength, "The actual and required SES lengths must be equal.");
		}

		private void btnTextOne_Click(object sender, System.EventArgs e)
		{
			GetFileName(edtTextOne);
		}

		private void btnTextTwo_Click(object sender, System.EventArgs e)
		{
			GetFileName(edtTextTwo);
		}

		private void GetFileName(TextBox Edit)
		{
			OpenDlg.FileName = Edit.Text;
			OpenDlg.Title = "Get File Name";
			if (OpenDlg.ShowDialog(this) == DialogResult.OK)
			{
				Edit.Text = OpenDlg.FileName;
			}
		}

		private void btnTestText_Click(object sender, System.EventArgs e)
		{
			DiffTextFiles(edtTextOne.Text, edtTextTwo.Text);
		}

		private void DiffTextFiles(string strA, string strB)
		{
			try
			{
				edtTextOne.Text = strA;
				edtTextTwo.Text = strB;

				TextDiff Diff = new TextDiff((HashType)cbHashType.SelectedIndex, chkIgnoreCase.Checked, chkIgnoreWhitespace.Checked);

				StringCollection A, B;
				if (chkXML.Checked)
				{
					WhitespaceHandling eWS = (WhitespaceHandling)cbXMLWhitespace.SelectedIndex;
					A = Functions.GetXMLTextLines(strA, eWS);
					B = Functions.GetXMLTextLines(strB, eWS);
				}
				else
				{
					A = Functions.GetFileTextLines(strA);
					B = Functions.GetFileTextLines(strB);
				}

				EditScript Script = Diff.Execute(A, B);

				diffControl1.SetData(A, B, Script, strA, strB);
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			cbHashType.SelectedIndex = 0;
			cbXMLWhitespace.SelectedIndex = (int)WhitespaceHandling.None;

			Size Sz = new Size(listBox1.HorizontalExtent, listBox1.ItemHeight * listBox1.Items.Count);
			panel1.AutoScrollMinSize = Sz;

			chkFullRowSelect.Checked = dirDiffControl1.FullRowSelect;
		}

		private void listBox1_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			Graphics G = e.Graphics;
			int iGutter = 50;
			G.DrawString(listBox1.Items[e.Index].ToString() + e.Bounds.ToString(), e.Font, SystemBrushes.WindowText, e.Bounds.X + iGutter, e.Bounds.Y);
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Point P = panel1.AutoScrollPosition;
			int iVPos = Math.Abs(P.Y);
			int iHPos = Math.Abs(P.X);

			int iFirstLine = Math.Max(0, (int)Math.Floor((iVPos + e.ClipRectangle.Y)/(double)listBox1.ItemHeight));
			int iLastLine = Math.Min(listBox1.Items.Count-1, iFirstLine + (int)Math.Ceiling(e.ClipRectangle.Height/(double)listBox1.ItemHeight));

			const int GUTTER = 25;
			for (int i = iFirstLine; i <= iLastLine; i++)
			{
				int x = GUTTER - iHPos;
				int y = (listBox1.ItemHeight * i) - iVPos;

				//Draw the item string first
				e.Graphics.DrawString(listBox1.Items[i].ToString(), Font, SystemBrushes.WindowText, x, y);
			}

			lblScroll.Text = String.Format("VPos: {0} HPos: {1} Lines: {2}-{3} R: {4}", iVPos, iHPos, iFirstLine, iLastLine, e.ClipRectangle.ToString());
		}

		private void btnDirCompare_Click(object sender, System.EventArgs e)
		{
			try
			{
				DirectoryDiff Diff = new DirectoryDiff(chkShowOnlyInA.Checked, chkShowOnlyInB.Checked, chkShowDifferent.Checked, chkShowSame.Checked, chkRecursive.Checked, chkIgnoreDirectoryComparison.Checked, null);
				DirectoryDiffResults Results = Diff.Execute(edtDir1.Text, edtDir2.Text);
				dirDiffControl1.SetData(Results);
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void chkFullRowSelect_CheckedChanged(object sender, System.EventArgs e)
		{
			dirDiffControl1.FullRowSelect = chkFullRowSelect.Checked;
		}

		private void dirDiffControl1_ShowFileDifferences(object sender, Menees.DiffUtils.DifferenceEventArgs e)
		{
			try
			{
				if (Functions.IsBinaryFile(e.A) || Functions.IsBinaryFile(e.B))
				{
					bool bDifferent = Functions.AreFilesDifferent(e.A, e.B);
					if (bDifferent)
						MessageBox.Show("The binary files are different.");
					else
						MessageBox.Show("The binary files are identical.");
				}
				else
				{
					DiffTextFiles(e.A, e.B);
					tabControl1.SelectedTab = tabText;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnDocDiff_Click(object sender, System.EventArgs e)
		{
			try 
			{
				txtBinary.Clear();

				string strExt = ".doc";
				DiffTest test = new DiffTest("Base" + strExt, txtBinary);
				test.AddChange("First" + strExt);
				test.ExtractNewest("Final" + strExt);

				ShowAddCopyList(test.LastChange);
			} 
			catch(Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
