namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Teroid.DataGridViewToolStrip.Properties;

    internal class PrintPreviewDialog : Form
    {
        private ToolStripButton btn1x1;
        private ToolStripButton btn2x1;
        private ToolStripButton btn2x2;
        private ToolStripButton btn3x1;
        private ToolStripButton btn3x2;
        private ToolStripButton btnFirst;
        private ToolStripButton btnLast;
        private ToolStripButton btnNext;
        private ToolStripButton btnPrev;
        private ToolStripButton btnPrint;
        private ToolStripDropDownButton btnZoom;
        private IContainer components;
        private ToolStripLabel lblPage;
        private ToolStripMenuItem mnu10;
        private ToolStripMenuItem mnu100;
        private ToolStripMenuItem mnu150;
        private ToolStripMenuItem mnu200;
        private ToolStripMenuItem mnu25;
        private ToolStripMenuItem mnu50;
        private ToolStripMenuItem mnu500;
        private ToolStripMenuItem mnu75;
        private ToolStripMenuItem mnuAuto;
        private PrintPreviewControl pprev;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;

        internal event EventHandler PrintButtonClicked;

        public PrintPreviewDialog()
        {
            this.InitializeComponent();
        }

        private void btn1x1_Click(object sender, EventArgs e)
        {
            this.pprev.Rows = 1;
            this.pprev.Columns = 1;
        }

        private void btn2x1_Click(object sender, EventArgs e)
        {
            this.pprev.Rows = 1;
            this.pprev.Columns = 2;
        }

        private void btn2x2_Click(object sender, EventArgs e)
        {
            this.pprev.Rows = 2;
            this.pprev.Columns = 2;
        }

        private void btn3x1_Click(object sender, EventArgs e)
        {
            this.pprev.Rows = 1;
            this.pprev.Columns = 3;
        }

        private void btn3x2_Click(object sender, EventArgs e)
        {
            this.pprev.Rows = 2;
            this.pprev.Columns = 3;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (this.pprev.StartPage > 0)
            {
                this.pprev.StartPage = 0;
            }
            this.SetPageCount();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (this.pprev.StartPage < (this.pprev.Document.PrinterSettings.MaximumPage - 1))
            {
                this.pprev.StartPage = this.pprev.Document.PrinterSettings.MaximumPage - 1;
            }
            this.SetPageCount();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.pprev.StartPage < (this.pprev.Document.PrinterSettings.MaximumPage - 1))
            {
                this.pprev.StartPage++;
            }
            this.SetPageCount();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (this.pprev.StartPage > 0)
            {
                this.pprev.StartPage--;
            }
            this.SetPageCount();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.PrintButtonClicked != null)
            {
                this.PrintButtonClicked(this, new EventArgs());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Teroid.DataGridViewToolStrip.PrintPreviewDialog));
            this.pprev = new PrintPreviewControl();
            this.toolStrip1 = new ToolStrip();
            this.btnPrint = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.btnZoom = new ToolStripDropDownButton();
            this.mnuAuto = new ToolStripMenuItem();
            this.mnu500 = new ToolStripMenuItem();
            this.mnu200 = new ToolStripMenuItem();
            this.mnu150 = new ToolStripMenuItem();
            this.mnu100 = new ToolStripMenuItem();
            this.mnu75 = new ToolStripMenuItem();
            this.mnu50 = new ToolStripMenuItem();
            this.mnu25 = new ToolStripMenuItem();
            this.mnu10 = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.btn1x1 = new ToolStripButton();
            this.btn2x1 = new ToolStripButton();
            this.btn3x1 = new ToolStripButton();
            this.btn2x2 = new ToolStripButton();
            this.btn3x2 = new ToolStripButton();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.btnFirst = new ToolStripButton();
            this.btnPrev = new ToolStripButton();
            this.lblPage = new ToolStripLabel();
            this.btnNext = new ToolStripButton();
            this.btnLast = new ToolStripButton();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.pprev.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pprev.Location = new Point(0, 0x19);
            this.pprev.Name = "pprev";
            this.pprev.Size = new Size(0x2ba, 0x155);
            this.pprev.TabIndex = 0;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.btnPrint, this.toolStripSeparator1, this.btnZoom, this.toolStripSeparator2, this.btn1x1, this.btn2x1, this.btn3x1, this.btn2x2, this.btn3x2, this.toolStripSeparator3, this.btnFirst, this.btnPrev, this.lblPage, this.btnNext, this.btnLast });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x2ba, 0x19);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = Resources.print;
            this.btnPrint.ImageTransparentColor = Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(0x17, 0x16);
            this.btnPrint.Text = "&Print";
            this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.btnZoom.DropDownItems.AddRange(new ToolStripItem[] { this.mnuAuto, this.mnu500, this.mnu200, this.mnu150, this.mnu100, this.mnu75, this.mnu50, this.mnu25, this.mnu10 });
            this.btnZoom.Image = Resources.zoom;
            this.btnZoom.ImageTransparentColor = Color.Magenta;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new Size(0x3b, 0x16);
            this.btnZoom.Text = "Auto";
            this.btnZoom.ToolTipText = "Zoom";
            this.mnuAuto.Name = "mnuAuto";
            this.mnuAuto.Size = new Size(0x72, 0x16);
            this.mnuAuto.Text = "Auto";
            this.mnuAuto.Click += new EventHandler(this.mnuAuto_Click);
            this.mnu500.Name = "mnu500";
            this.mnu500.Size = new Size(0x72, 0x16);
            this.mnu500.Text = "500%";
            this.mnu500.Click += new EventHandler(this.mnu500_Click);
            this.mnu200.Name = "mnu200";
            this.mnu200.Size = new Size(0x72, 0x16);
            this.mnu200.Text = "200%";
            this.mnu200.Click += new EventHandler(this.mnu200_Click);
            this.mnu150.Name = "mnu150";
            this.mnu150.Size = new Size(0x72, 0x16);
            this.mnu150.Text = "150%";
            this.mnu150.Click += new EventHandler(this.mnu150_Click);
            this.mnu100.Name = "mnu100";
            this.mnu100.Size = new Size(0x72, 0x16);
            this.mnu100.Text = "100%";
            this.mnu100.Click += new EventHandler(this.mnu100_Click);
            this.mnu75.Name = "mnu75";
            this.mnu75.Size = new Size(0x72, 0x16);
            this.mnu75.Text = "75%";
            this.mnu75.Click += new EventHandler(this.mnu75_Click);
            this.mnu50.Name = "mnu50";
            this.mnu50.Size = new Size(0x72, 0x16);
            this.mnu50.Text = "50%";
            this.mnu50.Click += new EventHandler(this.mnu50_Click);
            this.mnu25.Name = "mnu25";
            this.mnu25.Size = new Size(0x72, 0x16);
            this.mnu25.Text = "25%";
            this.mnu25.Click += new EventHandler(this.mnu25_Click);
            this.mnu10.Name = "mnu10";
            this.mnu10.Size = new Size(0x72, 0x16);
            this.mnu10.Text = "10%";
            this.mnu10.Click += new EventHandler(this.mnu10_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.btn1x1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btn1x1.Image = (Image) manager.GetObject("btn1x1.Image");
            this.btn1x1.ImageTransparentColor = Color.Magenta;
            this.btn1x1.Name = "btn1x1";
            this.btn1x1.Size = new Size(0x17, 0x16);
            this.btn1x1.Text = "1 page";
            this.btn1x1.ToolTipText = "1 page";
            this.btn1x1.Click += new EventHandler(this.btn1x1_Click);
            this.btn2x1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btn2x1.Image = (Image) manager.GetObject("btn2x1.Image");
            this.btn2x1.ImageTransparentColor = Color.Magenta;
            this.btn2x1.Name = "btn2x1";
            this.btn2x1.Size = new Size(0x17, 0x16);
            this.btn2x1.Text = "2 pages";
            this.btn2x1.ToolTipText = "2 pages";
            this.btn2x1.Click += new EventHandler(this.btn2x1_Click);
            this.btn3x1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btn3x1.Image = Resources.pp3x1;
            this.btn3x1.ImageTransparentColor = Color.Magenta;
            this.btn3x1.Name = "btn3x1";
            this.btn3x1.Size = new Size(0x17, 0x16);
            this.btn3x1.Text = "3 pages";
            this.btn3x1.ToolTipText = "3 pages";
            this.btn3x1.Click += new EventHandler(this.btn3x1_Click);
            this.btn2x2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btn2x2.Image = Resources.pp2x2;
            this.btn2x2.ImageTransparentColor = Color.Magenta;
            this.btn2x2.Name = "btn2x2";
            this.btn2x2.Size = new Size(0x17, 0x16);
            this.btn2x2.Text = "4 pages";
            this.btn2x2.ToolTipText = "4 pages";
            this.btn2x2.Click += new EventHandler(this.btn2x2_Click);
            this.btn3x2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btn3x2.Image = Resources.pp3x2;
            this.btn3x2.ImageTransparentColor = Color.Magenta;
            this.btn3x2.Name = "btn3x2";
            this.btn3x2.Size = new Size(0x17, 0x16);
            this.btn3x2.Text = "6 pages";
            this.btn3x2.ToolTipText = "6 pages";
            this.btn3x2.Click += new EventHandler(this.btn3x2_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(6, 0x19);
            this.btnFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = Resources.first;
            this.btnFirst.ImageScaling = ToolStripItemImageScaling.None;
            this.btnFirst.ImageTransparentColor = Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new Size(0x17, 0x16);
            this.btnFirst.Text = "First page";
            this.btnFirst.ToolTipText = "First";
            this.btnFirst.Click += new EventHandler(this.btnFirst_Click);
            this.btnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnPrev.Image = Resources.previous;
            this.btnPrev.ImageTransparentColor = Color.Magenta;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new Size(0x17, 0x16);
            this.btnPrev.Text = "Previous page";
            this.btnPrev.ToolTipText = "Previous";
            this.btnPrev.Click += new EventHandler(this.btnPrev_Click);
            this.lblPage.BackColor = SystemColors.Window;
            this.lblPage.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new Size(0x3e, 0x16);
            this.lblPage.Text = "Page 0 of 0";
            this.lblPage.ToolTipText = "Page Number";
            this.btnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = Resources.next;
            this.btnNext.ImageTransparentColor = Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x17, 0x16);
            this.btnNext.Text = "Next page";
            this.btnNext.ToolTipText = "Next";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = Resources.last;
            this.btnLast.ImageTransparentColor = Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x17, 0x16);
            this.btnLast.Text = "Last page";
            this.btnLast.ToolTipText = "Last";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2ba, 0x16e);
            base.Controls.Add(this.toolStrip1);
            base.Controls.Add(this.pprev);
            base.Name = "PrintPreviewDialog";
            this.Text = "Print Preview";
            base.WindowState = FormWindowState.Maximized;
            base.VisibleChanged += new EventHandler(this.PrintPreviewDialog_VisibleChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void mnu10_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 0.1;
            this.btnZoom.Text = "10%";
        }

        private void mnu100_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 1.0;
            this.btnZoom.Text = "100%";
        }

        private void mnu150_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 1.5;
            this.btnZoom.Text = "150%";
        }

        private void mnu200_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 2.0;
            this.btnZoom.Text = "200%";
        }

        private void mnu25_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 0.25;
            this.btnZoom.Text = "25%";
        }

        private void mnu50_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 0.5;
            this.btnZoom.Text = "50%";
        }

        private void mnu500_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 5.0;
            this.btnZoom.Text = "500%";
        }

        private void mnu75_Click(object sender, EventArgs e)
        {
            this.pprev.Zoom = 0.75;
            this.btnZoom.Text = "75%";
        }

        private void mnuAuto_Click(object sender, EventArgs e)
        {
            this.pprev.AutoZoom = true;
            this.btnZoom.Text = "Auto";
        }

        private void PrintPreviewDialog_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.pprev.InvalidatePreview();
                this.SetPageCount();
            }
        }

        private void SetPageCount()
        {
            this.lblPage.Text = "Page " + ((this.pprev.StartPage + 1)).ToString() + " of " + this.pprev.Document.PrinterSettings.MaximumPage.ToString();
        }

        internal PrintDocument Document
        {
            get
            {
                return this.pprev.Document;
            }
            set
            {
                this.pprev.Document = value;
            }
        }
    }
}

