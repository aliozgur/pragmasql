using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;


namespace RichTextBoxEx
{

  #region StampActions
  public enum StampActions
  {
    EditedBy = 1,
    DateTime = 2,
    Custom = 4
  }
  #endregion

  /// <summary>
  /// An extended RichTextBox that contains a toolbar.
  /// </summary>
  public class RichTextEditor : System.Windows.Forms.UserControl
  {
    private bool _isInitializing = false;
    //Used for looping
    private ExRichTextBox rtbTemp = new ExRichTextBox ();

    #region Windows Generated

    private ExRichTextBox rtb1;
    private System.Windows.Forms.ImageList imgList1;
    private System.Windows.Forms.ContextMenu cmColors;
    private System.Windows.Forms.MenuItem miBlack;
    private System.Windows.Forms.MenuItem miBlue;
    private System.Windows.Forms.MenuItem miRed;
    private System.Windows.Forms.MenuItem miGreen;
    private System.Windows.Forms.OpenFileDialog ofd1;
    private System.Windows.Forms.SaveFileDialog sfd1;
    private System.Windows.Forms.ContextMenu cmFonts;
    private System.Windows.Forms.MenuItem miArial;
    private System.Windows.Forms.MenuItem miGaramond;
    private System.Windows.Forms.MenuItem miTahoma;
    private System.Windows.Forms.MenuItem miTimesNewRoman;
    private System.Windows.Forms.MenuItem miVerdana;
    private System.Windows.Forms.MenuItem miCourierNew;
    private System.Windows.Forms.MenuItem miMicrosoftSansSerif;
    private System.Windows.Forms.ContextMenu cmFontSizes;
    private System.Windows.Forms.MenuItem mi8;
    private System.Windows.Forms.MenuItem mi9;
    private System.Windows.Forms.MenuItem mi10;
    private System.Windows.Forms.MenuItem mi11;
    private System.Windows.Forms.MenuItem mi12;
    private System.Windows.Forms.MenuItem mi14;
    private System.Windows.Forms.MenuItem mi16;
    private System.Windows.Forms.MenuItem mi18;
    private System.Windows.Forms.MenuItem mi20;
    private System.Windows.Forms.MenuItem mi22;
    private System.Windows.Forms.MenuItem mi24;
    private System.Windows.Forms.MenuItem mi26;
    private System.Windows.Forms.MenuItem mi28;
    private System.Windows.Forms.MenuItem mi36;
    private System.Windows.Forms.MenuItem mi48;
    private System.Windows.Forms.MenuItem mi72;
    private System.ComponentModel.IContainer components;
    private ToolStrip toolStrip1;
    private ToolStripComboBox cmbFontNames;
    private ToolStripComboBox cmbFontSize;
    private ToolStripButton tbbColor;
    private ToolStripButton tbbBold;
    private ToolStripButton tbbItalic;
    private ToolStripButton tbbUnderline;
    private ToolStripButton tbbLeft;
    private ToolStripButton tbbCenter;
    private ToolStripButton tbbRight;
    private ToolStripSeparator tbbSeparator1;
    private ToolStripSeparator tbbSeparator2;
    private ToolStripSeparator tbbSeparator4;
    private ToolStripButton tbbUndo;
    private ToolStripButton tbbRedo;
    private ToolStripButton tbbCut;
    private ToolStripButton tbbCopy;
    private ToolStripButton tbbPaste;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton tbbInsertImage;
    private ToolStripButton tbbStamp;
    private ToolStripButton tbbInsertHyperlink;
    private ToolStripButton tbbStrikeout;
    private ToolStripButton tbbOpen;
    private ToolStripButton tbbSave;
    private ColorDialog colorDialog1;
    private ToolStripSeparator tbbSeparator3;

    public RichTextEditor( )
    {
      // This call is required by the Windows.Forms Form Designer.
      InitializeComponent();

      //Update the graphics on the toolbar
      UpdateToolbar();
			
      
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    #endregion

    #region Component Designer generated code
    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichTextEditor));
      this.cmFonts = new System.Windows.Forms.ContextMenu();
      this.miArial = new System.Windows.Forms.MenuItem();
      this.miCourierNew = new System.Windows.Forms.MenuItem();
      this.miGaramond = new System.Windows.Forms.MenuItem();
      this.miMicrosoftSansSerif = new System.Windows.Forms.MenuItem();
      this.miTahoma = new System.Windows.Forms.MenuItem();
      this.miTimesNewRoman = new System.Windows.Forms.MenuItem();
      this.miVerdana = new System.Windows.Forms.MenuItem();
      this.cmFontSizes = new System.Windows.Forms.ContextMenu();
      this.mi8 = new System.Windows.Forms.MenuItem();
      this.mi9 = new System.Windows.Forms.MenuItem();
      this.mi10 = new System.Windows.Forms.MenuItem();
      this.mi11 = new System.Windows.Forms.MenuItem();
      this.mi12 = new System.Windows.Forms.MenuItem();
      this.mi14 = new System.Windows.Forms.MenuItem();
      this.mi16 = new System.Windows.Forms.MenuItem();
      this.mi18 = new System.Windows.Forms.MenuItem();
      this.mi20 = new System.Windows.Forms.MenuItem();
      this.mi22 = new System.Windows.Forms.MenuItem();
      this.mi24 = new System.Windows.Forms.MenuItem();
      this.mi26 = new System.Windows.Forms.MenuItem();
      this.mi28 = new System.Windows.Forms.MenuItem();
      this.mi36 = new System.Windows.Forms.MenuItem();
      this.mi48 = new System.Windows.Forms.MenuItem();
      this.mi72 = new System.Windows.Forms.MenuItem();
      this.cmColors = new System.Windows.Forms.ContextMenu();
      this.miBlack = new System.Windows.Forms.MenuItem();
      this.miBlue = new System.Windows.Forms.MenuItem();
      this.miRed = new System.Windows.Forms.MenuItem();
      this.miGreen = new System.Windows.Forms.MenuItem();
      this.imgList1 = new System.Windows.Forms.ImageList(this.components);
      this.rtb1 = new ExRichTextBox();
      this.ofd1 = new System.Windows.Forms.OpenFileDialog();
      this.sfd1 = new System.Windows.Forms.SaveFileDialog();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.tbbOpen = new System.Windows.Forms.ToolStripButton();
      this.tbbSave = new System.Windows.Forms.ToolStripButton();
      this.tbbSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.cmbFontNames = new System.Windows.Forms.ToolStripComboBox();
      this.cmbFontSize = new System.Windows.Forms.ToolStripComboBox();
      this.tbbColor = new System.Windows.Forms.ToolStripButton();
      this.tbbSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.tbbBold = new System.Windows.Forms.ToolStripButton();
      this.tbbItalic = new System.Windows.Forms.ToolStripButton();
      this.tbbUnderline = new System.Windows.Forms.ToolStripButton();
      this.tbbStrikeout = new System.Windows.Forms.ToolStripButton();
      this.tbbSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.tbbLeft = new System.Windows.Forms.ToolStripButton();
      this.tbbCenter = new System.Windows.Forms.ToolStripButton();
      this.tbbRight = new System.Windows.Forms.ToolStripButton();
      this.tbbSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.tbbUndo = new System.Windows.Forms.ToolStripButton();
      this.tbbRedo = new System.Windows.Forms.ToolStripButton();
      this.tbbCut = new System.Windows.Forms.ToolStripButton();
      this.tbbCopy = new System.Windows.Forms.ToolStripButton();
      this.tbbPaste = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.tbbInsertImage = new System.Windows.Forms.ToolStripButton();
      this.tbbStamp = new System.Windows.Forms.ToolStripButton();
      this.tbbInsertHyperlink = new System.Windows.Forms.ToolStripButton();
      this.colorDialog1 = new System.Windows.Forms.ColorDialog();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmFonts
      // 
      this.cmFonts.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miArial,
            this.miCourierNew,
            this.miGaramond,
            this.miMicrosoftSansSerif,
            this.miTahoma,
            this.miTimesNewRoman,
            this.miVerdana});
      // 
      // miArial
      // 
      this.miArial.Index = 0;
      this.miArial.Text = "Arial";
      this.miArial.Click += new System.EventHandler(this.Font_Click);
      // 
      // miCourierNew
      // 
      this.miCourierNew.Index = 1;
      this.miCourierNew.Text = "Courier New";
      this.miCourierNew.Click += new System.EventHandler(this.Font_Click);
      // 
      // miGaramond
      // 
      this.miGaramond.Index = 2;
      this.miGaramond.Text = "Garamond";
      this.miGaramond.Click += new System.EventHandler(this.Font_Click);
      // 
      // miMicrosoftSansSerif
      // 
      this.miMicrosoftSansSerif.Index = 3;
      this.miMicrosoftSansSerif.Text = "Microsoft Sans Serif";
      this.miMicrosoftSansSerif.Click += new System.EventHandler(this.Font_Click);
      // 
      // miTahoma
      // 
      this.miTahoma.Index = 4;
      this.miTahoma.Text = "Tahoma";
      this.miTahoma.Click += new System.EventHandler(this.Font_Click);
      // 
      // miTimesNewRoman
      // 
      this.miTimesNewRoman.Index = 5;
      this.miTimesNewRoman.Text = "Times New Roman";
      this.miTimesNewRoman.Click += new System.EventHandler(this.Font_Click);
      // 
      // miVerdana
      // 
      this.miVerdana.Index = 6;
      this.miVerdana.Text = "Verdana";
      this.miVerdana.Click += new System.EventHandler(this.Font_Click);
      // 
      // cmFontSizes
      // 
      this.cmFontSizes.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mi8,
            this.mi9,
            this.mi10,
            this.mi11,
            this.mi12,
            this.mi14,
            this.mi16,
            this.mi18,
            this.mi20,
            this.mi22,
            this.mi24,
            this.mi26,
            this.mi28,
            this.mi36,
            this.mi48,
            this.mi72});
      // 
      // mi8
      // 
      this.mi8.Index = 0;
      this.mi8.Text = "8";
      this.mi8.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi9
      // 
      this.mi9.Index = 1;
      this.mi9.Text = "9";
      this.mi9.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi10
      // 
      this.mi10.Index = 2;
      this.mi10.Text = "10";
      this.mi10.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi11
      // 
      this.mi11.Index = 3;
      this.mi11.Text = "11";
      this.mi11.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi12
      // 
      this.mi12.Index = 4;
      this.mi12.Text = "12";
      this.mi12.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi14
      // 
      this.mi14.Index = 5;
      this.mi14.Text = "14";
      this.mi14.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi16
      // 
      this.mi16.Index = 6;
      this.mi16.Text = "16";
      this.mi16.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi18
      // 
      this.mi18.Index = 7;
      this.mi18.Text = "18";
      this.mi18.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi20
      // 
      this.mi20.Index = 8;
      this.mi20.Text = "20";
      this.mi20.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi22
      // 
      this.mi22.Index = 9;
      this.mi22.Text = "22";
      this.mi22.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi24
      // 
      this.mi24.Index = 10;
      this.mi24.Text = "24";
      this.mi24.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi26
      // 
      this.mi26.Index = 11;
      this.mi26.Text = "26";
      this.mi26.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi28
      // 
      this.mi28.Index = 12;
      this.mi28.Text = "28";
      this.mi28.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi36
      // 
      this.mi36.Index = 13;
      this.mi36.Text = "36";
      this.mi36.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi48
      // 
      this.mi48.Index = 14;
      this.mi48.Text = "48";
      this.mi48.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // mi72
      // 
      this.mi72.Index = 15;
      this.mi72.Text = "72";
      this.mi72.Click += new System.EventHandler(this.FontSize_Click);
      // 
      // cmColors
      // 
      this.cmColors.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miBlack,
            this.miBlue,
            this.miRed,
            this.miGreen});
      // 
      // miBlack
      // 
      this.miBlack.Index = 0;
      this.miBlack.Text = "Black";
      this.miBlack.Click += new System.EventHandler(this.Color_Click);
      // 
      // miBlue
      // 
      this.miBlue.Index = 1;
      this.miBlue.Text = "Blue";
      this.miBlue.Click += new System.EventHandler(this.Color_Click);
      // 
      // miRed
      // 
      this.miRed.Index = 2;
      this.miRed.Text = "Red";
      this.miRed.Click += new System.EventHandler(this.Color_Click);
      // 
      // miGreen
      // 
      this.miGreen.Index = 3;
      this.miGreen.Text = "Green";
      this.miGreen.Click += new System.EventHandler(this.Color_Click);
      // 
      // imgList1
      // 
      this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
      this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imgList1.Images.SetKeyName(0, "");
      this.imgList1.Images.SetKeyName(1, "");
      this.imgList1.Images.SetKeyName(2, "");
      this.imgList1.Images.SetKeyName(3, "");
      this.imgList1.Images.SetKeyName(4, "");
      this.imgList1.Images.SetKeyName(5, "");
      this.imgList1.Images.SetKeyName(6, "");
      this.imgList1.Images.SetKeyName(7, "");
      this.imgList1.Images.SetKeyName(8, "");
      this.imgList1.Images.SetKeyName(9, "");
      this.imgList1.Images.SetKeyName(10, "");
      this.imgList1.Images.SetKeyName(11, "");
      this.imgList1.Images.SetKeyName(12, "");
      this.imgList1.Images.SetKeyName(13, "");
      this.imgList1.Images.SetKeyName(14, "");
      this.imgList1.Images.SetKeyName(15, "");
      this.imgList1.Images.SetKeyName(16, "");
      this.imgList1.Images.SetKeyName(17, "");
      this.imgList1.Images.SetKeyName(18, "");
      this.imgList1.Images.SetKeyName(19, "");
      this.imgList1.Images.SetKeyName(20, "");
      // 
      // rtb1
      // 
      this.rtb1.AutoWordSelection = true;
      this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rtb1.HideSelection = false;
      this.rtb1.Location = new System.Drawing.Point(0, 25);
      this.rtb1.Name = "rtb1";
      this.rtb1.Size = new System.Drawing.Size(721, 327);
      this.rtb1.TabIndex = 1;
      this.rtb1.Text = "";
      this.rtb1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtb1_LinkClicked);
      this.rtb1.SelectionChanged += new System.EventHandler(this.rtb1_SelectionChanged);
      this.rtb1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb1_KeyDown);
      this.rtb1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb1_KeyPress);
      // 
      // ofd1
      // 
      this.ofd1.DefaultExt = "rtf";
      this.ofd1.Filter = "Rich Text Files|*.rtf|Plain Text File|*.txt";
      this.ofd1.Title = "Open File";
      // 
      // sfd1
      // 
      this.sfd1.DefaultExt = "rtf";
      this.sfd1.Filter = "Rich Text File|*.rtf|Plain Text File|*.txt";
      this.sfd1.Title = "Save As";
      // 
      // toolStrip1
      // 
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbOpen,
            this.tbbSave,
            this.tbbSeparator3,
            this.cmbFontNames,
            this.cmbFontSize,
            this.tbbColor,
            this.tbbSeparator1,
            this.tbbBold,
            this.tbbItalic,
            this.tbbUnderline,
            this.tbbStrikeout,
            this.tbbSeparator2,
            this.tbbLeft,
            this.tbbCenter,
            this.tbbRight,
            this.tbbSeparator4,
            this.tbbUndo,
            this.tbbRedo,
            this.tbbCut,
            this.tbbCopy,
            this.tbbPaste,
            this.toolStripSeparator4,
            this.tbbInsertImage,
            this.tbbStamp,
            this.tbbInsertHyperlink});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStrip1.Size = new System.Drawing.Size(721, 25);
      this.toolStrip1.TabIndex = 2;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // tbbOpen
      // 
      this.tbbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbOpen.Image = global::RichTextBoxEx.Properties.Resources.OpenFolder;
      this.tbbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbOpen.Name = "tbbOpen";
      this.tbbOpen.Size = new System.Drawing.Size(23, 22);
      this.tbbOpen.Tag = "open";
      this.tbbOpen.Text = "Open";
      this.tbbOpen.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbSave
      // 
      this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbSave.Image = global::RichTextBoxEx.Properties.Resources.Save;
      this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbSave.Name = "tbbSave";
      this.tbbSave.Size = new System.Drawing.Size(23, 22);
      this.tbbSave.Tag = "save";
      this.tbbSave.Text = "Save";
      this.tbbSave.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbSeparator3
      // 
      this.tbbSeparator3.Name = "tbbSeparator3";
      this.tbbSeparator3.Size = new System.Drawing.Size(6, 25);
      // 
      // cmbFontNames
      // 
      this.cmbFontNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFontNames.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbFontNames.MaxDropDownItems = 16;
      this.cmbFontNames.Name = "cmbFontNames";
      this.cmbFontNames.Size = new System.Drawing.Size(121, 25);
      this.cmbFontNames.ToolTipText = "Font Family";
      this.cmbFontNames.Validated += new System.EventHandler(this.cmbFontNames_Validated);
      // 
      // cmbFontSize
      // 
      this.cmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbFontSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmbFontSize.Items.AddRange(new object[] {
            "8",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
      this.cmbFontSize.MaxDropDownItems = 15;
      this.cmbFontSize.Name = "cmbFontSize";
      this.cmbFontSize.Size = new System.Drawing.Size(75, 25);
      this.cmbFontSize.ToolTipText = "Font Size";
      this.cmbFontSize.Validated += new System.EventHandler(this.cmbFontSize_Validated);
      // 
      // tbbColor
      // 
      this.tbbColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbColor.Image = ((System.Drawing.Image)(resources.GetObject("tbbColor.Image")));
      this.tbbColor.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbColor.Name = "tbbColor";
      this.tbbColor.Size = new System.Drawing.Size(23, 22);
      this.tbbColor.Tag = "color";
      this.tbbColor.Text = "Font Color";
      this.tbbColor.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbSeparator1
      // 
      this.tbbSeparator1.Name = "tbbSeparator1";
      this.tbbSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // tbbBold
      // 
      this.tbbBold.CheckOnClick = true;
      this.tbbBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbBold.Image = global::RichTextBoxEx.Properties.Resources.Bold;
      this.tbbBold.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbBold.Name = "tbbBold";
      this.tbbBold.Size = new System.Drawing.Size(23, 22);
      this.tbbBold.Tag = "bold";
      this.tbbBold.Text = "Bold";
      this.tbbBold.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbItalic
      // 
      this.tbbItalic.CheckOnClick = true;
      this.tbbItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbItalic.Image = global::RichTextBoxEx.Properties.Resources.Italic;
      this.tbbItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbItalic.Name = "tbbItalic";
      this.tbbItalic.Size = new System.Drawing.Size(23, 22);
      this.tbbItalic.Tag = "italic";
      this.tbbItalic.Text = "Italic";
      this.tbbItalic.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbUnderline
      // 
      this.tbbUnderline.CheckOnClick = true;
      this.tbbUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbUnderline.Image = global::RichTextBoxEx.Properties.Resources.Underline;
      this.tbbUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbUnderline.Name = "tbbUnderline";
      this.tbbUnderline.Size = new System.Drawing.Size(23, 22);
      this.tbbUnderline.Tag = "underline";
      this.tbbUnderline.Text = "Underline";
      this.tbbUnderline.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbStrikeout
      // 
      this.tbbStrikeout.CheckOnClick = true;
      this.tbbStrikeout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbStrikeout.Image = global::RichTextBoxEx.Properties.Resources.strikeout;
      this.tbbStrikeout.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbStrikeout.Name = "tbbStrikeout";
      this.tbbStrikeout.Size = new System.Drawing.Size(23, 22);
      this.tbbStrikeout.Tag = "strikeout";
      this.tbbStrikeout.Text = "Strike Out";
      this.tbbStrikeout.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbSeparator2
      // 
      this.tbbSeparator2.Name = "tbbSeparator2";
      this.tbbSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // tbbLeft
      // 
      this.tbbLeft.CheckOnClick = true;
      this.tbbLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbLeft.Image = global::RichTextBoxEx.Properties.Resources.align_left;
      this.tbbLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbLeft.Name = "tbbLeft";
      this.tbbLeft.Size = new System.Drawing.Size(23, 22);
      this.tbbLeft.Tag = "left";
      this.tbbLeft.Text = "Align Left";
      this.tbbLeft.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbCenter
      // 
      this.tbbCenter.CheckOnClick = true;
      this.tbbCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbCenter.Image = global::RichTextBoxEx.Properties.Resources.align_center;
      this.tbbCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbCenter.Name = "tbbCenter";
      this.tbbCenter.Size = new System.Drawing.Size(23, 22);
      this.tbbCenter.Tag = "center";
      this.tbbCenter.Text = "Align Center";
      this.tbbCenter.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbRight
      // 
      this.tbbRight.CheckOnClick = true;
      this.tbbRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbRight.Image = global::RichTextBoxEx.Properties.Resources.align_right;
      this.tbbRight.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbRight.Name = "tbbRight";
      this.tbbRight.Size = new System.Drawing.Size(23, 22);
      this.tbbRight.Tag = "right";
      this.tbbRight.Text = "Align Right";
      this.tbbRight.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbSeparator4
      // 
      this.tbbSeparator4.Name = "tbbSeparator4";
      this.tbbSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // tbbUndo
      // 
      this.tbbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbUndo.Image = global::RichTextBoxEx.Properties.Resources.undo;
      this.tbbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbUndo.Name = "tbbUndo";
      this.tbbUndo.Size = new System.Drawing.Size(23, 22);
      this.tbbUndo.Tag = "undo";
      this.tbbUndo.Text = "Undo";
      this.tbbUndo.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbRedo
      // 
      this.tbbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbRedo.Image = global::RichTextBoxEx.Properties.Resources.redo;
      this.tbbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbRedo.Name = "tbbRedo";
      this.tbbRedo.Size = new System.Drawing.Size(23, 22);
      this.tbbRedo.Tag = "redo";
      this.tbbRedo.Text = "Redo";
      this.tbbRedo.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbCut
      // 
      this.tbbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbCut.Image = global::RichTextBoxEx.Properties.Resources.cut;
      this.tbbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbCut.Name = "tbbCut";
      this.tbbCut.Size = new System.Drawing.Size(23, 22);
      this.tbbCut.Tag = "cut";
      this.tbbCut.Text = "Cut";
      this.tbbCut.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbCopy
      // 
      this.tbbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbCopy.Image = global::RichTextBoxEx.Properties.Resources.copy;
      this.tbbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbCopy.Name = "tbbCopy";
      this.tbbCopy.Size = new System.Drawing.Size(23, 22);
      this.tbbCopy.Tag = "copy";
      this.tbbCopy.Text = "Copy";
      this.tbbCopy.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbPaste
      // 
      this.tbbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbPaste.Image = global::RichTextBoxEx.Properties.Resources.paste;
      this.tbbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbPaste.Name = "tbbPaste";
      this.tbbPaste.Size = new System.Drawing.Size(23, 22);
      this.tbbPaste.Tag = "paste";
      this.tbbPaste.Text = "Paste";
      this.tbbPaste.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
      // 
      // tbbInsertImage
      // 
      this.tbbInsertImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbInsertImage.Image = global::RichTextBoxEx.Properties.Resources.insert_image;
      this.tbbInsertImage.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbInsertImage.Name = "tbbInsertImage";
      this.tbbInsertImage.Size = new System.Drawing.Size(23, 22);
      this.tbbInsertImage.Tag = "insert image";
      this.tbbInsertImage.Text = "Insert Image";
      this.tbbInsertImage.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbStamp
      // 
      this.tbbStamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbStamp.Image = global::RichTextBoxEx.Properties.Resources.stamp;
      this.tbbStamp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbStamp.Name = "tbbStamp";
      this.tbbStamp.Size = new System.Drawing.Size(23, 22);
      this.tbbStamp.Tag = "edit stamp";
      this.tbbStamp.Text = "Insert Edit Stamp";
      this.tbbStamp.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // tbbInsertHyperlink
      // 
      this.tbbInsertHyperlink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tbbInsertHyperlink.Image = global::RichTextBoxEx.Properties.Resources.link;
      this.tbbInsertHyperlink.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tbbInsertHyperlink.Name = "tbbInsertHyperlink";
      this.tbbInsertHyperlink.Size = new System.Drawing.Size(23, 22);
      this.tbbInsertHyperlink.Tag = "insert hyperlink";
      this.tbbInsertHyperlink.Text = "Insert Hyperlink";
      this.tbbInsertHyperlink.Click += new System.EventHandler(this.toolStripButton11_Click);
      // 
      // colorDialog1
      // 
      this.colorDialog1.AnyColor = true;
      this.colorDialog1.FullOpen = true;
      // 
      // RichTextBoxExtended
      // 
      this.Controls.Add(this.rtb1);
      this.Controls.Add(this.toolStrip1);
      this.Name = "RichTextBoxExtended";
      this.Size = new System.Drawing.Size(721, 352);
      this.Load += new System.EventHandler(this.RichTextBoxExtended_Load);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    #region Selection Change event
    [Description("Occurs when the selection is changed"),
    Category("Behavior")]
    // Raised in tb1 SelectionChanged event so that user can do useful things
    public event System.EventHandler SelChanged;
    #endregion

    #region Stamp Event Stuff
    [Description("Occurs when the stamp button is clicked"),
     Category("Behavior")]
    public event System.EventHandler Stamp;

    /// <summary>
    /// OnStamp event
    /// </summary>
    protected virtual void OnStamp( EventArgs e )
    {
      if (Stamp != null)
        Stamp(this, e);

      switch (StampAction)
      {
        case StampActions.EditedBy:
          {
            StringBuilder stamp = new StringBuilder(""); //holds our stamp text
            if (rtb1.Text.Length > 0) stamp.Append("\r\n\r\n"); //add two lines for space
            stamp.Append("Edited by ");
            //use the CurrentPrincipal name if one exsist else use windows logon username
            if (Thread.CurrentPrincipal == null || Thread.CurrentPrincipal.Identity == null || Thread.CurrentPrincipal.Identity.Name.Length <= 0)
              stamp.Append(Environment.UserName);
            else
              stamp.Append(Thread.CurrentPrincipal.Identity.Name);
            stamp.Append(" on " + DateTime.Now.ToLongDateString() + "\r\n");

            rtb1.SelectionLength = 0; //unselect everything basicly
            rtb1.SelectionStart = rtb1.Text.Length; //start new selection at the end of the text
            rtb1.SelectionColor = this.StampColor; //make the selection blue
            rtb1.SelectionFont = new Font(rtb1.SelectionFont, FontStyle.Bold); //set the selection font and style
            rtb1.AppendText(stamp.ToString()); //add the stamp to the richtextbox
            rtb1.Focus(); //set focus back on the richtextbox
          } break; //end edited by stamp
        case StampActions.DateTime:
          {
            StringBuilder stamp = new StringBuilder(""); //holds our stamp text
            if (rtb1.Text.Length > 0) stamp.Append("\r\n\r\n"); //add two lines for space
            stamp.Append(DateTime.Now.ToLongDateString() + "\r\n");
            rtb1.SelectionLength = 0; //unselect everything basicly
            rtb1.SelectionStart = rtb1.Text.Length; //start new selection at the end of the text
            rtb1.SelectionColor = this.StampColor; //make the selection blue
            rtb1.SelectionFont = new Font(rtb1.SelectionFont, FontStyle.Bold); //set the selection font and style
            rtb1.AppendText(stamp.ToString()); //add the stamp to the richtextbox
            rtb1.Focus(); //set focus back on the richtextbox
          } break;
      } //end select
    }
    #endregion

    #region Update Toolbar
    /// <summary>
    ///     Update the toolbar button statuses
    /// </summary>
    public void UpdateToolbar( )
    {
      // Get the font, fontsize and style to apply to the toolbar buttons
      Font fnt = GetFontDetails();
      // Set font style buttons to the styles applying to the entire selection
      FontStyle style = fnt.Style;

      //Set all the style buttons using the gathered style
      tbbBold.Checked = fnt.Bold; //bold button
      tbbItalic.Checked = fnt.Italic; //italic button
      tbbUnderline.Checked = fnt.Underline; //underline button
      tbbStrikeout.Checked = fnt.Strikeout; //strikeout button
      tbbLeft.Checked = (rtb1.SelectionAlignment == HorizontalAlignment.Left); //justify left
      tbbCenter.Checked = (rtb1.SelectionAlignment == HorizontalAlignment.Center); //justify center
      tbbRight.Checked = (rtb1.SelectionAlignment == HorizontalAlignment.Right); //justify right

      //Check the correct color
      foreach (MenuItem mi in cmColors.MenuItems)
        mi.Checked = (rtb1.SelectionColor == Color.FromName(mi.Text));

      //Check the correct font
      foreach (MenuItem mi in cmFonts.MenuItems)
        mi.Checked = (fnt.FontFamily.Name == mi.Text);

      //Check the correct font size
      foreach (MenuItem mi in cmFontSizes.MenuItems)
        mi.Checked = ((int)fnt.SizeInPoints == float.Parse(mi.Text));


      cmbFontNames.Text = fnt.FontFamily.Name;
      cmbFontSize.Text = ((int)fnt.SizeInPoints).ToString();
    }

    #endregion

    #region Update Toolbar Seperators

    private void UpdateToolbarSeperators( )
    {
      //Save & Open
      if (!tbbSave.Visible && !tbbOpen.Visible)
        tbbSeparator3.Visible = false;
      else
        tbbSeparator3.Visible = true;

      //Bold, Italic, Underline, & Strikeout
      if (!tbbBold.Visible && !tbbItalic.Visible && !tbbUnderline.Visible && !tbbStrikeout.Visible)
        tbbSeparator1.Visible = false;
      else
        tbbSeparator1.Visible = true;

      //Left, Center, & Right
      if (!tbbLeft.Visible && !tbbCenter.Visible && !tbbRight.Visible)
        tbbSeparator2.Visible = false;
      else
        tbbSeparator2.Visible = true;

      //Undo & Redo
      if (!tbbUndo.Visible && !tbbRedo.Visible)
        tbbSeparator4.Visible = false;
      else
        tbbSeparator4.Visible = true;
    }
    #endregion

    #region RichTextBox Selection Change
    /// <summary>
    ///		Change the toolbar buttons when new text is selected
    ///		and raise event SelChanged
    /// </summary>
    private void rtb1_SelectionChanged( object sender, System.EventArgs e )
    {
      //Update the toolbar buttons
      UpdateToolbar();

      //Send the SelChangedEvent
      if (SelChanged != null)
        SelChanged(this, e);
    }
    #endregion

    #region Color Click
    /// <summary>
    ///     Change the richtextbox color
    /// </summary>
    private void Color_Click( object sender, System.EventArgs e )
    {
      //set the richtextbox color based on the name of the menu item
      ChangeFontColor(Color.FromName(((MenuItem)sender).Text));
    }
    #endregion

    #region Font Click
    /// <summary>
    ///     Change the richtextbox font
    /// </summary>
    private void Font_Click( object sender, System.EventArgs e )
    {
      // Set the font for the entire selection
      ChangeFont(((MenuItem)sender).Text);
    }
    #endregion

    #region Font Size Click
    /// <summary>
    ///     Change the richtextbox font size
    /// </summary>
    private void FontSize_Click( object sender, System.EventArgs e )
    {
      //set the richtextbox font size based on the name of the menu item
      ChangeFontSize(float.Parse(((MenuItem)sender).Text));
    }
    #endregion

    #region Link Clicked
    /// <summary>
    /// Starts the default browser if a link is clicked
    /// </summary>
    private void rtb1_LinkClicked( object sender, System.Windows.Forms.LinkClickedEventArgs e )
    {
      //System.Diagnostics.Process.Start(e.LinkText);
    }
    #endregion

    #region Public Properties
    /// <summary>
    ///     The toolbar that is contained with-in the RichTextBoxExtened control
    /// </summary>
    [Description("The internal toolbar control"),
    Category("Internal Controls"),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

    public ToolStrip ToolBar
    {
      get { return toolStrip1; }
    }

    /// <summary>
    ///     The RichTextBox that is contained with-in the RichTextBoxExtened control
    /// </summary>
    [Description("The internal richtextbox control"),
    Category("Internal Controls"),
    DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RichTextBox RichTextBox
    {
      get { return rtb1; }
    }

    /// <summary>
    ///     Show the insert image button or not
    /// </summary>
    [Description("Show the insert image button or not"),
    Category("Appearance")]
    public bool ShowInsertImage
    {
      get { return tbbInsertImage.Visible; }
      set { tbbInsertImage.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the insert hyperlink button or not
    /// </summary>
    [Description("Show the insert hyperlink button or not"),
    Category("Appearance")]
    public bool ShowInsertHyperlink
    {
      get { return tbbInsertHyperlink.Visible; }
      set { tbbInsertHyperlink.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the save button or not
    /// </summary>
    [Description("Show the save button or not"),
    Category("Appearance")]
    public bool ShowSave
    {
      get { return tbbSave.Visible; }
      set { tbbSave.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///    Show the open button or not 
    /// </summary>
    [Description("Show the open button or not"),
    Category("Appearance")]
    public bool ShowOpen
    {
      get { return tbbOpen.Visible; }
      set { tbbOpen.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the stamp button or not
    /// </summary>
    [Description("Show the stamp button or not"),
     Category("Appearance")]
    public bool ShowStamp
    {
      get { return tbbStamp.Visible; }
      set { tbbStamp.Visible = value; }
    }

    /// <summary>
    ///     Show the color button or not
    /// </summary>
    [Description("Show the color button or not"),
    Category("Appearance")]
    public bool ShowColors
    {
      get { return tbbColor.Visible; }
      set { tbbColor.Visible = value; }
    }

    /// <summary>
    ///     Show the undo button or not
    /// </summary>
    [Description("Show the undo button or not"),
    Category("Appearance")]
    public bool ShowUndo
    {
      get { return tbbUndo.Visible; }
      set { tbbUndo.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the redo button or not
    /// </summary>
    [Description("Show the redo button or not"),
    Category("Appearance")]
    public bool ShowRedo
    {
      get { return tbbRedo.Visible; }
      set { tbbRedo.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the bold button or not
    /// </summary>
    [Description("Show the bold button or not"),
    Category("Appearance")]
    public bool ShowBold
    {
      get { return tbbBold.Visible; }
      set { tbbBold.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the italic button or not
    /// </summary>
    [Description("Show the italic button or not"),
    Category("Appearance")]
    public bool ShowItalic
    {
      get { return tbbItalic.Visible; }
      set { tbbItalic.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the underline button or not
    /// </summary>
    [Description("Show the underline button or not"),
    Category("Appearance")]
    public bool ShowUnderline
    {
      get { return tbbUnderline.Visible; }
      set { tbbUnderline.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the strikeout button or not
    /// </summary>
    [Description("Show the strikeout button or not"),
    Category("Appearance")]
    public bool ShowStrikeout
    {
      get { return tbbStrikeout.Visible; }
      set { tbbStrikeout.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the left justify button or not
    /// </summary>
    [Description("Show the left justify button or not"),
    Category("Appearance")]
    public bool ShowLeftJustify
    {
      get { return tbbLeft.Visible; }
      set { tbbLeft.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the right justify button or not
    /// </summary>
    [Description("Show the right justify button or not"),
    Category("Appearance")]
    public bool ShowRightJustify
    {
      get { return tbbRight.Visible; }
      set { tbbRight.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Show the center justify button or not
    /// </summary>
    [Description("Show the center justify button or not"),
    Category("Appearance")]
    public bool ShowCenterJustify
    {
      get { return tbbCenter.Visible; }
      set { tbbCenter.Visible = value; UpdateToolbarSeperators(); }
    }

    /// <summary>
    ///     Determines how the stamp button will respond
    /// </summary>
    StampActions m_StampAction = StampActions.EditedBy;
    [Description("Determines how the stamp button will respond"),
    Category("Behavior")]
    public StampActions StampAction
    {
      get { return m_StampAction; }
      set { m_StampAction = value; }
    }

    /// <summary>
    ///     Color of the stamp text
    /// </summary>
    Color m_StampColor = Color.Blue;

    [Description("Color of the stamp text"),
    Category("Appearance")]
    public Color StampColor
    {
      get { return m_StampColor; }
      set { m_StampColor = value; }
    }

    /// <summary>
    ///     Show the font button or not
    /// </summary>
    [Description("Show the font button or not"),
    Category("Appearance")]
    public bool ShowFont
    {
      get { return cmbFontNames.Visible; }
      set { cmbFontNames.Visible = value; }
    }

    /// <summary>
    ///     Show the font size button or not
    /// </summary>
    [Description("Show the font size button or not"),
    Category("Appearance")]
    public bool ShowFontSize
    {
      get { return cmbFontSize.Visible; }
      set { cmbFontSize.Visible = value; }
    }

    /// <summary>
    ///     Show the cut button or not
    /// </summary>
    [Description("Show the cut button or not"),
    Category("Appearance")]
    public bool ShowCut
    {
      get { return tbbCut.Visible; }
      set { tbbCut.Visible = value; }
    }

    /// <summary>
    ///     Show the copy button or not
    /// </summary>
    [Description("Show the copy button or not"),
    Category("Appearance")]
    public bool ShowCopy
    {
      get { return tbbCopy.Visible; }
      set { tbbCopy.Visible = value; }
    }

    /// <summary>
    ///     Show the paste button or not
    /// </summary>
    [Description("Show the paste button or not"),
    Category("Appearance")]
    public bool ShowPaste
    {
      get { return tbbPaste.Visible; }
      set { tbbPaste.Visible = value; }
    }

    /// <summary>
    ///     Detect URLs with-in the richtextbox
    /// </summary>
    [Description("Detect URLs with-in the richtextbox"),
    Category("Behavior")]
    public bool DetectURLs
    {
      get { return rtb1.DetectUrls; }
      set { rtb1.DetectUrls = value; }
    }

    /// <summary>
    /// Determines if the TAB key moves to the next control or enters a TAB character in the richtextbox
    /// </summary>
    [Description("Determines if the TAB key moves to the next control or enters a TAB character in the richtextbox"),
    Category("Behavior")]
    public bool AcceptsTab
    {
      get { return rtb1.AcceptsTab; }
      set { rtb1.AcceptsTab = value; }
    }

    /// <summary>
    /// Determines if auto word selection is enabled
    /// </summary>
    [Description("Determines if auto word selection is enabled"),
    Category("Behavior")]
    public bool AutoWordSelection
    {
      get { return rtb1.AutoWordSelection; }
      set { rtb1.AutoWordSelection = value; }
    }

    /// <summary>
    /// Determines if this control can be edited
    /// </summary>
    [Description("Determines if this control can be edited"),
    Category("Behavior")]
    public bool ReadOnly
    {
      get { return rtb1.ReadOnly; }
      set
      {
        toolStrip1.Visible = !value;
        rtb1.ReadOnly = value;
      }
    }


    /// <summary>
    /// Determines if the buttons on the toolbar will show there text or not
    /// </summary>
    [Description("Determines if the buttons on the toolbar will show there text or not"),
    Category("Behavior")]
    public bool ShowToolBarText
    {
      get { return toolStrip1.ShowItemToolTips; }
      set
      {
        toolStrip1.ShowItemToolTips = value;

        if (value)
        {
          tbbSave.Text = "Save";
          tbbOpen.Text = "Open";
          tbbBold.Text = "Bold";
          cmbFontNames.ToolTipText = "Font";
          cmbFontSize.ToolTipText = "Font Size";
          tbbColor.Text = "Font Color";
          tbbItalic.Text = "Italic";
          tbbStrikeout.Text = "Strikeout";
          tbbUnderline.Text = "Underline";
          tbbLeft.Text = "Left";
          tbbCenter.Text = "Center";
          tbbRight.Text = "Right";
          tbbUndo.Text = "Undo";
          tbbRedo.Text = "Redo";
          tbbCut.Text = "Cut";
          tbbCopy.Text = "Copy";
          tbbPaste.Text = "Paste";
          tbbStamp.Text = "Stamp";
        }
        else
        {
          tbbSave.Text = "";
          tbbOpen.Text = "";
          tbbBold.Text = "";
          cmbFontNames.ToolTipText = "";
          cmbFontSize.ToolTipText = "";
          tbbColor.Text = "";
          tbbItalic.Text = "";
          tbbStrikeout.Text = "";
          tbbUnderline.Text = "";
          tbbLeft.Text = "";
          tbbCenter.Text = "";
          tbbRight.Text = "";
          tbbUndo.Text = "";
          tbbRedo.Text = "";
          tbbCut.Text = "";
          tbbCopy.Text = "";
          tbbPaste.Text = "";
          tbbStamp.Text = "";
        }

        this.Invalidate();
        this.Update();
      }
    }

    #endregion

    #region Change font
    /// <summary>
    ///     Change the richtextbox font for the current selection
    /// </summary>
    public void ChangeFont( string fontFamily )
    {
      //This method should handle cases that occur when multiple fonts/styles are selected
      // Parameters:-
      // fontFamily - the font to be applied, eg "Courier New"

      // Reason: The reason this method and the others exist is because
      // setting these items via the selection font doen't work because
      // a null selection font is returned for a selection with more 
      // than one font!

      int rtb1start = rtb1.SelectionStart;
      int len = rtb1.SelectionLength;
      int rtbTempStart = 0;

      // If len <= 1 and there is a selection font, amend and return
      if (len <= 1 && rtb1.SelectionFont != null)
      {
        rtb1.SelectionFont =
					new Font(fontFamily, rtb1.SelectionFont.Size, rtb1.SelectionFont.Style);
        return;
      }

      // Step through the selected text one char at a time
      rtbTemp.Rtf = rtb1.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);
        rtbTemp.SelectionFont = new Font(fontFamily, rtbTemp.SelectionFont.Size, rtbTemp.SelectionFont.Style);
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      rtb1.SelectedRtf = rtbTemp.SelectedRtf;
      rtb1.Select(rtb1start, len);
      return;
    }
    #endregion

    #region Change font style
    /// <summary>
    ///     Change the richtextbox style for the current selection
    /// </summary>
    public void ChangeFontStyle( FontStyle style, bool add )
    {
      //This method should handle cases that occur when multiple fonts/styles are selected
      // Parameters:-
      //	style - eg FontStyle.Bold
      //	add - IF true then add else remove

      // throw error if style isn't: bold, italic, strikeout or underline
      if (style != FontStyle.Bold
        && style != FontStyle.Italic
        && style != FontStyle.Strikeout
        && style != FontStyle.Underline)
        throw new System.InvalidProgramException("Invalid style parameter to ChangeFontStyle");

      int rtb1start = rtb1.SelectionStart;
      int len = rtb1.SelectionLength;
      int rtbTempStart = 0;

      //if len <= 1 and there is a selection font then just handle and return
      if (len <= 1 && rtb1.SelectionFont != null)
      {
        //add or remove style 
        if (add)
          rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style | style);
        else
          rtb1.SelectionFont = new Font(rtb1.SelectionFont, rtb1.SelectionFont.Style & ~style);

        return;
      }

      // Step through the selected text one char at a time	
      rtbTemp.Rtf = rtb1.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);

        //add or remove style 
        if (add)
          rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont, rtbTemp.SelectionFont.Style | style);
        else
          rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont, rtbTemp.SelectionFont.Style & ~style);
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      rtb1.SelectedRtf = rtbTemp.SelectedRtf;
      rtb1.Select(rtb1start, len);
      return;
    }
    #endregion

    #region Change font size
    /// <summary>
    ///     Change the richtextbox font size for the current selection
    /// </summary>
    public void ChangeFontSize( float fontSize )
    {
      //This method should handle cases that occur when multiple fonts/styles are selected
      // Parameters:-
      // fontSize - the fontsize to be applied, eg 33.5

      if (fontSize <= 0.0)
        throw new System.InvalidProgramException("Invalid font size parameter to ChangeFontSize");

      int rtb1start = rtb1.SelectionStart;
      int len = rtb1.SelectionLength;
      int rtbTempStart = 0;

      // If len <= 1 and there is a selection font, amend and return
      if (len <= 1 && rtb1.SelectionFont != null)
      {
        rtb1.SelectionFont =
					new Font(rtb1.SelectionFont.FontFamily, fontSize, rtb1.SelectionFont.Style);
        return;
      }

      // Step through the selected text one char at a time
      rtbTemp.Rtf = rtb1.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);
        rtbTemp.SelectionFont = new Font(rtbTemp.SelectionFont.FontFamily, fontSize, rtbTemp.SelectionFont.Style);
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      rtb1.SelectedRtf = rtbTemp.SelectedRtf;
      rtb1.Select(rtb1start, len);
      return;
    }
    #endregion

    #region Change font color
    /// <summary>
    ///     Change the richtextbox font color for the current selection
    /// </summary>
    public void ChangeFontColor( Color newColor )
    {
      //This method should handle cases that occur when multiple fonts/styles are selected
      // Parameters:-
      //	newColor - eg Color.Red

      int rtb1start = rtb1.SelectionStart;
      int len = rtb1.SelectionLength;
      int rtbTempStart = 0;

      //if len <= 1 and there is a selection font then just handle and return
      if (len <= 1 && rtb1.SelectionFont != null)
      {
        rtb1.SelectionColor = newColor;
        return;
      }

      // Step through the selected text one char at a time	
      rtbTemp.Rtf = rtb1.SelectedRtf;
      for (int i = 0; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);

        //change color
        rtbTemp.SelectionColor = newColor;
      }

      // Replace & reselect
      rtbTemp.Select(rtbTempStart, len);
      rtb1.SelectedRtf = rtbTemp.SelectedRtf;
      rtb1.Select(rtb1start, len);
      return;
    }
    #endregion

    #region Get Font Details
    /// <summary>
    ///     Returns a Font with:
    ///     1) The font applying to the entire selection, if none is the default font. 
    ///     2) The font size applying to the entire selection, if none is the size of the default font.
    ///     3) A style containing the attributes that are common to the entire selection, default regular.
    /// </summary>		
    /// 
    public Font GetFontDetails( )
    {
      //This method should handle cases that occur when multiple fonts/styles are selected

      int rtb1start = rtb1.SelectionStart;
      int len = rtb1.SelectionLength;
      int rtbTempStart = 0;

      if (len <= 1)
      {
        // Return the selection or default font
        if (rtb1.SelectionFont != null)
          return rtb1.SelectionFont;
        else
          return rtb1.Font;
      }

      // Step through the selected text one char at a time	
      // after setting defaults from first char
      rtbTemp.Rtf = rtb1.SelectedRtf;

      //Turn everything on so we can turn it off one by one
      FontStyle replystyle =
        FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout | FontStyle.Underline;

      // Set reply font, size and style to that of first char in selection.
      rtbTemp.Select(rtbTempStart, 1);
      string replyfont = rtbTemp.SelectionFont.Name;
      float replyfontsize = rtbTemp.SelectionFont.Size;
      replystyle = replystyle & rtbTemp.SelectionFont.Style;

      // Search the rest of the selection
      for (int i = 1; i < len; ++i)
      {
        rtbTemp.Select(rtbTempStart + i, 1);

        // Check reply for different style
        replystyle = replystyle & rtbTemp.SelectionFont.Style;

        // Check font
        if (replyfont != rtbTemp.SelectionFont.FontFamily.Name)
          replyfont = "";

        // Check font size
        if (replyfontsize != rtbTemp.SelectionFont.Size)
          replyfontsize = (float)0.0;
      }

      // Now set font and size if more than one font or font size was selected
      if (replyfont == "")
        replyfont = rtbTemp.Font.FontFamily.Name;

      if (replyfontsize == 0.0)
        replyfontsize = rtbTemp.Font.Size;

      // generate reply font
      Font reply
        = new Font(replyfont, replyfontsize, replystyle);

      return reply;
    }
    #endregion

    #region Keyboard Handler

    private void rtb1_KeyDown( object sender, System.Windows.Forms.KeyEventArgs e )
    {
      if (e.Modifiers == Keys.Control)
      {
        ToolStripButton tbb = null;

        switch (e.KeyCode)
        {
          case Keys.B:
            tbb = this.tbbBold;
            break;
          case Keys.I:
            tbb = this.tbbItalic;
            break;
          case Keys.S:
            tbb = this.tbbStamp;
            break;
          case Keys.U:
            tbb = this.tbbUnderline;
            break;
          case Keys.OemMinus:
            tbb = this.tbbStrikeout;
            break;
        }

        if (tbb != null)
        {
          if (e.KeyCode != Keys.S) tbb.Checked = !tbb.Checked;
          toolStripButton11_Click(tbb, new EventArgs());
        }
      }

      //Insert a tab if the tab key was pressed.
      /* NOTE: This was needed because in rtb1_KeyPress I tell the richtextbox not
       * to handle tab events.  I do that because CTRL+I inserts a tab for some
       * strange reason.  What was MicroSoft thinking?
       * Richard Parsons 02/08/2007
       */
      if (e.KeyCode == Keys.Tab)
        rtb1.SelectedText = "\t";

    }

    private void rtb1_KeyPress( object sender, System.Windows.Forms.KeyPressEventArgs e )
    {
      if ((int)e.KeyChar == 9)
        e.Handled = true; // Stops Ctrl+I from inserting a tab (char HT) into the richtextbox
    }
    #endregion

    private void RichTextBoxExtended_Load( object sender, EventArgs e )
    {
      _isInitializing = true;
      cmbFontNames.Items.Clear();
      foreach (FontFamily family in FontFamily.Families)
      {
        cmbFontNames.Items.Add(family.Name);
      }
      cmbFontNames.Text = rtb1.SelectionFont.Name;
      _isInitializing = false;
    }

    private void toolStripButton11_Click( object sender, EventArgs e )
    {
      ToolStripButton btn = sender as ToolStripButton;

      // true if style to be added
      // false to remove style
      bool add = btn.Checked;


      //Switch based on the tag of the button pressed
      switch (btn.Tag.ToString().ToLowerInvariant())
      {
        case "bold":
          ChangeFontStyle(FontStyle.Bold, add);
          break;
        case "italic":
          ChangeFontStyle(FontStyle.Italic, add);
          break;
        case "underline":
          ChangeFontStyle(FontStyle.Underline, add);
          break;
        case "strikeout":
          ChangeFontStyle(FontStyle.Strikeout, add);
          break;
        case "left":
          //change horizontal alignment to left
          rtb1.SelectionAlignment = HorizontalAlignment.Left;
          tbbCenter.Checked = false;
          tbbRight.Checked = false;
          break;
        case "center":
          //change horizontal alignment to center
          tbbLeft.Checked = false;
          rtb1.SelectionAlignment = HorizontalAlignment.Center;
          tbbRight.Checked = false;
          break;
        case "right":
          //change horizontal alignment to right
          tbbLeft.Checked = false;
          tbbCenter.Checked = false;
          rtb1.SelectionAlignment = HorizontalAlignment.Right;
          break;
        case "edit stamp":
          OnStamp(new EventArgs()); //send stamp event
          break;
        case "insert image":
          ShowInsertImageDlg();
          break;
        case "color":
          if (colorDialog1.ShowDialog() == DialogResult.OK)
          {
            rtb1.SelectionColor = colorDialog1.Color;
          }
          break;
        case "undo":
          rtb1.Undo();
          break;
        case "redo":
          rtb1.Redo();
          break;
        case "open":
          try
          {
            if (ofd1.ShowDialog() == DialogResult.OK && ofd1.FileName.Length > 0)
              if (System.IO.Path.GetExtension(ofd1.FileName).ToLower().Equals(".rtf"))
                rtb1.LoadFile(ofd1.FileName, RichTextBoxStreamType.RichText);
              else
                rtb1.LoadFile(ofd1.FileName, RichTextBoxStreamType.PlainText);
          }
          catch (ArgumentException ae)
          {
            if (ae.Message == "Invalid file format.")
              MessageBox.Show("There was an error loading the file: " + ofd1.FileName);
          }
          break;
        case "save":
          if (sfd1.ShowDialog() == DialogResult.OK && sfd1.FileName.Length > 0)
            if (System.IO.Path.GetExtension(sfd1.FileName).ToLower().Equals(".rtf"))
              rtb1.SaveFile(sfd1.FileName);
            else
              rtb1.SaveFile(sfd1.FileName, RichTextBoxStreamType.PlainText);
          break;
        case "cut":
          {
            if (rtb1.SelectedText.Length <= 0) break;
            rtb1.Cut();
            break;
          }
        case "copy":
          {
            if (rtb1.SelectedText.Length <= 0) break;
            rtb1.Copy();
            break;
          }
        case "paste":
          {
            try
            {
              rtb1.Paste();
            }
            catch
            {
              MessageBox.Show("Paste Failed");
            }
            break;
          }
      } //end select    
    }

    private void cmbFontNames_Validated( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      ChangeFont(cmbFontNames.Text);

    }

    private void cmbFontSize_Validated( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      ChangeFontSize(float.Parse(cmbFontSize.Text));
    }

    public void ShowInsertImageDlg()
    {
      OpenFileDialog _dialog = new OpenFileDialog();
      _dialog.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff|" +
               "Windows Bitmap(*.bmp)|*.bmp|" +
               "Windows Icon(*.ico)|*.ico|" +
               "Graphics Interchange Format (*.gif)|(*.gif)|" +
               "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg|" +
               "Portable Network Graphics (*.png)|*.png|" +
               "Tag Image File Format (*.tif)|*.tif;*.tiff";
      if (DialogResult.OK == _dialog.ShowDialog(this))
      {
        try
        {
          // If file is an icon
          if (_dialog.FileName.ToUpper().EndsWith(".ICO"))
          {
            // Create a new icon, get it's handle, and create a bitmap from
            // its handle
            rtb1.InsertImage(Bitmap.FromHicon((new Icon(_dialog.FileName)).Handle));
          }
          else
          {
            // Create a bitmap from the filename
            rtb1.InsertImage(Image.FromFile(_dialog.FileName));
          }
        }
        catch (Exception _e)
        {
          MessageBox.Show("The file could not be opened:\n\n" + _e.Message, "File Open Error");
        }
      }

    }

    

  } //end class
} //end namespace

