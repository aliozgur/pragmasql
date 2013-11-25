namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(DataGridViewPrintControl))]
    internal sealed class DataGridViewPrintControl : Component
    {
        private IContainer components;
        private int footerheight;
        private int headerheight;
        private System.Windows.Forms.DataGridView m_DataGridView;
        private bool m_NumberPages = true;
        private string m_PageFooter = "[Page Footer text]";
        private Color m_PageFooterColor = Color.Black;
        private Font m_PageFooterFont;
        private PageHeaderAndFooterLocations m_PageFooterLocation = PageHeaderAndFooterLocations.Center;
        private string m_PageHeader = "[Page Header text]";
        private Color m_PageHeaderColor = Color.Black;
        private Font m_PageHeaderFont;
        private PageHeaderAndFooterLocations m_PageHeaderLocation = PageHeaderAndFooterLocations.Center;
        private Color m_PageNumberColor = Color.Black;
        private Font m_PageNumberFont;
        private string m_PageNumberFormat = "Page # of #";
        private PageNumberLocations m_PageNumberLocation = PageNumberLocations.BottomRight;
        private int NextColumn;
        private int NextRow;
        private int NumberOfPages;
        private int PageNumber = 1;
        private int pagenumberheight;
        private PrintDialog pdlg;
        private PrintDocument pdoc = new PrintDocument();
        private Teroid.DataGridViewToolStrip.PrintPreviewDialog pprev2;
        private PageSetupDialog psudlg;

        public DataGridViewPrintControl()
        {
            this.pdoc.PrintPage += new PrintPageEventHandler(this.pdoc_PrintPage);
            this.pdoc.BeginPrint += new PrintEventHandler(this.pdoc_PrintDocument);
            this.pdlg = new PrintDialog();
            this.pdlg.AllowPrintToFile = false;
            this.pdlg.Document = this.pdoc;
            this.pdlg.AllowSomePages = true;
            this.pdlg.PrinterSettings = this.pdoc.PrinterSettings;
            this.psudlg = new PageSetupDialog();
            this.psudlg.Document = this.pdoc;
            this.pprev2 = new Teroid.DataGridViewToolStrip.PrintPreviewDialog();
            this.pprev2.Document = this.pdoc;
            this.pprev2.PrintButtonClicked += new EventHandler(this.OnPrintPreviewPrintButtonClicked);
            this.m_PageHeaderFont = new Font("Microsoft Sans Serif", 8.25f);
            this.m_PageFooterFont = new Font("Microsoft Sans Serif", 8.25f);
            this.m_PageNumberFont = new Font("Microsoft Sans Serif", 8.25f);
        }

        private void CalculateNumberOfPages()
        {
            int num = (this.psudlg.PageSettings.Bounds.Width - this.psudlg.PageSettings.Margins.Left) - this.psudlg.PageSettings.Margins.Right;
            int num2 = (this.psudlg.PageSettings.Bounds.Height - this.psudlg.PageSettings.Margins.Top) - this.psudlg.PageSettings.Margins.Bottom;
            int num3 = 1;
            int num4 = 1;
            int num5 = 0;
            int num6 = 0;
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            Control control = new Control();
            Graphics graphics = control.CreateGraphics();
            this.footerheight = (int) graphics.MeasureString(this.m_PageFooter, this.m_PageFooterFont).Height;
            this.headerheight = (int) graphics.MeasureString(this.m_PageHeader, this.m_PageHeaderFont).Height;
            this.pagenumberheight = (int) graphics.MeasureString(this.m_PageNumberFormat, this.m_PageNumberFont).Height;
            graphics.Dispose();
            control.Dispose();
            for (int i = 0; i < this.m_DataGridView.Columns.Count; i++)
            {
                if (this.m_DataGridView.Columns[i].Visible)
                {
                    num5 += this.m_DataGridView.Columns[i].Width;
                }
                if (num5 > num)
                {
                    num3++;
                    num5 = 0;
                }
            }
            if (this.m_NumberPages)
            {
                num2 -= this.pagenumberheight;
            }
            if (this.m_DataGridView.ColumnHeadersVisible)
            {
                num2 -= this.m_DataGridView.ColumnHeadersHeight;
            }
            num2 -= 20;
            for (int j = 0; j < count; j++)
            {
                if (this.m_DataGridView.Rows[j].Visible)
                {
                    num6 += this.m_DataGridView.Rows[j].Height;
                }
                if (num6 > num2)
                {
                    num4++;
                    num6 = 0;
                    j--;
                }
            }
            this.NumberOfPages = num3 * num4;
        }

        private string GetPageNumberString(int PageNumber, int PageCount)
        {
            string pageNumberFormat = this.m_PageNumberFormat;
            int index = pageNumberFormat.IndexOf('#');
            pageNumberFormat = pageNumberFormat.Remove(index, 1).Insert(index, PageNumber.ToString());
            int startIndex = pageNumberFormat.IndexOf('#');
            if (startIndex > -1)
            {
                pageNumberFormat = pageNumberFormat.Remove(startIndex, 1).Insert(startIndex, PageCount.ToString());
            }
            return pageNumberFormat;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        private bool IsOdd(int number)
        {
            if (number == 0)
            {
                return false;
            }
            if ((((float) number) / 2f) == (number / 2))
            {
                return false;
            }
            return true;
        }

        private void OnPrintPreviewPrintButtonClicked(object sender, EventArgs e)
        {
            this.Print();
        }

        public void PageSetup()
        {
            this.psudlg.ShowDialog();
        }

        private void pdoc_PrintDocument(object sender, PrintEventArgs e)
        {
        }

        private void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int numberOfPages;
            this.PrintPage(e);
            this.PageNumber++;
            if (this.pdoc.PrinterSettings.ToPage == 0)
            {
                numberOfPages = this.NumberOfPages;
            }
            else
            {
                numberOfPages = this.pdoc.PrinterSettings.ToPage;
            }
            if (this.PageNumber <= numberOfPages)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        public void Print()
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property not set");
            }
            this.CalculateNumberOfPages();
            this.NextColumn = 0;
            this.NextRow = 0;
            this.pdlg.PrinterSettings.MinimumPage = 1;
            this.pdlg.PrinterSettings.MaximumPage = this.NumberOfPages;
            this.pdlg.PrinterSettings.FromPage = 1;
            this.pdlg.PrinterSettings.ToPage = this.NumberOfPages;
            if (this.pdlg.ShowDialog() == DialogResult.OK)
            {
                this.PageNumber = this.pdlg.PrinterSettings.FromPage;
                for (int i = 1; i < this.pdlg.PrinterSettings.FromPage; i++)
                {
                    this.ShiftColumnAndRowNumbersByOnePage();
                }
                this.pdoc.Print();
            }
        }

        private void PrintPage(PrintPageEventArgs e)
        {
            int width = e.MarginBounds.Width;
            int height = e.MarginBounds.Height;
            Graphics graphics = e.Graphics;
            int left = e.MarginBounds.Left;
            int top = e.MarginBounds.Top;
            bool flag = false;
            bool flag2 = false;
            int nextColumn = this.NextColumn;
            int nextRow = this.NextRow;
            Rectangle layoutRectangle = new Rectangle(left, top, width, height);
            if (this.m_NumberPages)
            {
                height -= this.pagenumberheight;
            }
            if (this.m_DataGridView.ColumnHeadersVisible)
            {
                height -= this.m_DataGridView.ColumnHeadersHeight;
            }
            height -= 20;
            Pen pen = new Pen(this.m_DataGridView.GridColor, 1f);
            Font font = new Font(this.m_DataGridView.Font.FontFamily, this.m_DataGridView.Font.Size, this.m_DataGridView.Font.Style);
            SolidBrush brush = new SolidBrush(this.m_DataGridView.AlternatingRowsDefaultCellStyle.BackColor);
            SolidBrush brush2 = new SolidBrush(Color.Black);
            SolidBrush brush3 = new SolidBrush(this.m_DataGridView.ColumnHeadersDefaultCellStyle.BackColor);
            SolidBrush brush4 = new SolidBrush(Color.Black);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.Character;
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            CellBorders both = CellBorders.Both;
            switch (this.m_DataGridView.CellBorderStyle)
            {
                case DataGridViewCellBorderStyle.Single:
                case DataGridViewCellBorderStyle.Raised:
                case DataGridViewCellBorderStyle.Sunken:
                    both = CellBorders.Both;
                    break;

                case DataGridViewCellBorderStyle.None:
                    both = CellBorders.None;
                    break;

                case DataGridViewCellBorderStyle.SingleVertical:
                case DataGridViewCellBorderStyle.RaisedVertical:
                case DataGridViewCellBorderStyle.SunkenVertical:
                    both = CellBorders.Vertical;
                    break;

                case DataGridViewCellBorderStyle.SingleHorizontal:
                case DataGridViewCellBorderStyle.RaisedHorizontal:
                case DataGridViewCellBorderStyle.SunkenHorizontal:
                    both = CellBorders.Horizontal;
                    break;
            }
            if (this.m_NumberPages && (((this.m_PageNumberLocation == PageNumberLocations.TopLeft) || (this.m_PageNumberLocation == PageNumberLocations.TopCenter)) || (this.m_PageNumberLocation == PageNumberLocations.TopRight)))
            {
                switch (this.m_PageNumberLocation)
                {
                    case PageNumberLocations.TopLeft:
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Near;
                        break;

                    case PageNumberLocations.TopCenter:
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Near;
                        break;

                    case PageNumberLocations.TopRight:
                        format.Alignment = StringAlignment.Far;
                        format.LineAlignment = StringAlignment.Near;
                        break;
                }
                string pageNumberString = this.GetPageNumberString(this.PageNumber, this.NumberOfPages);
                graphics.DrawString(pageNumberString, this.m_PageNumberFont, new SolidBrush(this.m_PageNumberColor), layoutRectangle, format);
                layoutRectangle.Y += this.pagenumberheight;
                layoutRectangle.Height -= this.pagenumberheight;
            }
            if (this.m_NumberPages && (((this.m_PageNumberLocation == PageNumberLocations.BottomLeft) || (this.m_PageNumberLocation == PageNumberLocations.BottomCenter)) || (this.m_PageNumberLocation == PageNumberLocations.BottomRight)))
            {
                switch (this.m_PageNumberLocation)
                {
                    case PageNumberLocations.BottomLeft:
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Far;
                        break;

                    case PageNumberLocations.BottomCenter:
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Far;
                        break;

                    case PageNumberLocations.BottomRight:
                        format.Alignment = StringAlignment.Far;
                        format.LineAlignment = StringAlignment.Far;
                        break;
                }
                string str2 = this.GetPageNumberString(this.PageNumber, this.NumberOfPages);
                graphics.DrawString(str2, this.m_PageNumberFont, new SolidBrush(this.m_PageNumberColor), layoutRectangle, format);
                layoutRectangle.Height -= this.pagenumberheight;
            }
            layoutRectangle.Y += 10;
            top = layoutRectangle.Top;
            height = layoutRectangle.Height - 20;
            format.Trimming = StringTrimming.EllipsisCharacter;
            StringFormatFlags formatFlags = format.FormatFlags;
            layoutRectangle.Height = this.m_DataGridView.ColumnHeadersHeight;
            if (this.m_DataGridView.ColumnHeadersVisible)
            {
                if (this.NextColumn < this.m_DataGridView.Columns.Count)
                {
                    layoutRectangle.Width = this.m_DataGridView.Columns[this.NextColumn].Width;
                }
                while ((((layoutRectangle.X + layoutRectangle.Width) - left) <= width) && (this.NextColumn < this.m_DataGridView.Columns.Count))
                {
                    layoutRectangle.Y = top;
                    if (this.m_DataGridView.Columns[this.NextColumn].Visible)
                    {
                        if ((this.m_DataGridView.Columns[this.NextColumn].HeaderCell.InheritedStyle.WrapMode == DataGridViewTriState.NotSet) || (this.m_DataGridView.Columns[this.NextColumn].HeaderCell.InheritedStyle.WrapMode == DataGridViewTriState.False))
                        {
                            format.FormatFlags = StringFormatFlags.NoWrap;
                        }
                        else
                        {
                            format.FormatFlags = formatFlags;
                        }
                        this.SetStringFormatAlignment(format, this.m_DataGridView.Columns[this.NextColumn].HeaderCell.InheritedStyle.Alignment);
                        brush4.Color = this.m_DataGridView.Columns[this.NextColumn].HeaderCell.InheritedStyle.BackColor;
                        graphics.FillRectangle(brush4, layoutRectangle);
                        graphics.DrawRectangle(pen, layoutRectangle.X, layoutRectangle.Top, layoutRectangle.Width, layoutRectangle.Height);
                        brush4.Color = this.m_DataGridView.Columns[this.NextColumn].HeaderCell.InheritedStyle.ForeColor;
                        graphics.DrawString(this.m_DataGridView.Columns[this.NextColumn].HeaderText, this.m_DataGridView.Columns[this.NextColumn].HeaderCell.InheritedStyle.Font, brush4, layoutRectangle, format);
                        layoutRectangle.X += this.m_DataGridView.Columns[this.NextColumn].Width;
                    }
                    this.NextColumn++;
                    if (this.NextColumn < this.m_DataGridView.Columns.Count)
                    {
                        layoutRectangle.Width = this.m_DataGridView.Columns[this.NextColumn].Width;
                    }
                }
            }
            this.NextColumn = nextColumn;
            layoutRectangle.X = left;
            bool flag4 = true;
            if (this.NextColumn < this.m_DataGridView.Columns.Count)
            {
                layoutRectangle.Width = this.m_DataGridView.Columns[this.NextColumn].Width;
            }
            while ((((layoutRectangle.X + layoutRectangle.Width) - left) <= width) && (this.NextColumn < this.m_DataGridView.Columns.Count))
            {
                if (this.m_DataGridView.ColumnHeadersVisible)
                {
                    layoutRectangle.Y = top + this.m_DataGridView.ColumnHeadersHeight;
                }
                else
                {
                    layoutRectangle.Y = top;
                }
                if (!this.m_DataGridView.Columns[this.NextColumn].Visible)
                {
                    goto Label_1465;
                }
                bool flag3 = true;
                while ((((layoutRectangle.Y + layoutRectangle.Height) - top) <= height) && (this.NextRow < count))
                {
                    if (this.m_DataGridView.Rows[this.NextRow].Visible)
                    {
                        layoutRectangle.Height = this.m_DataGridView.Rows[this.NextRow].Height;
                        if ((this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.WrapMode == DataGridViewTriState.NotSet) || (this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.WrapMode == DataGridViewTriState.False))
                        {
                            format.FormatFlags = StringFormatFlags.NoWrap;
                        }
                        else
                        {
                            format.FormatFlags = formatFlags;
                        }
                        brush2.Color = this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.BackColor;
                        graphics.FillRectangle(brush2, layoutRectangle);
                        switch (both)
                        {
                            case CellBorders.Both:
                                graphics.DrawRectangle(pen, layoutRectangle);
                                break;

                            case CellBorders.Vertical:
                                graphics.DrawLine(pen, layoutRectangle.X, layoutRectangle.Y, layoutRectangle.X, layoutRectangle.Y + layoutRectangle.Height);
                                graphics.DrawLine(pen, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y + layoutRectangle.Height);
                                if (flag3)
                                {
                                    graphics.DrawLine(pen, layoutRectangle.X, layoutRectangle.Y, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y);
                                    flag3 = false;
                                }
                                break;

                            case CellBorders.Horizontal:
                                graphics.DrawLine(pen, layoutRectangle.X, layoutRectangle.Y, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y);
                                graphics.DrawLine(pen, layoutRectangle.X, layoutRectangle.Y + layoutRectangle.Height, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y + layoutRectangle.Height);
                                if (flag4)
                                {
                                    graphics.DrawLine(pen, layoutRectangle.X, layoutRectangle.Y, layoutRectangle.X, layoutRectangle.Y + layoutRectangle.Height);
                                }
                                break;
                        }
                        this.SetStringFormatAlignment(format, this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.Alignment);
                        brush2.Color = this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.ForeColor;
                        if (((this.m_DataGridView.Columns[this.NextColumn].CellType == typeof(DataGridViewTextBoxCell)) || (this.m_DataGridView.Columns[this.NextColumn].CellType == typeof(DataGridViewButtonCell))) || (this.m_DataGridView.Columns[this.NextColumn].CellType == typeof(DataGridViewComboBoxCell)))
                        {
                            graphics.DrawString(this.m_DataGridView[this.NextColumn, this.NextRow].FormattedValue.ToString(), this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.Font, brush2, layoutRectangle, format);
                        }
                        else if (this.m_DataGridView.Columns[this.NextColumn].CellType == typeof(DataGridViewCheckBoxCell))
                        {
                            bool flag5;
                            int x = layoutRectangle.X + 3;
                            int y = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - 7;
                            graphics.DrawRectangle(Pens.Black, x, y, 13, 13);
                            if (this.m_DataGridView[this.NextColumn, this.NextRow].Value != DBNull.Value)
                            {
                                flag5 = (bool) this.m_DataGridView[this.NextColumn, this.NextRow].Value;
                            }
                            else
                            {
                                flag5 = false;
                            }
                            if (flag5)
                            {
                                graphics.DrawLine(pen, (int) (x + 3), (int) (y + 5), (int) (x + 5), (int) (y + 7));
                                graphics.DrawLine(pen, (int) (x + 5), (int) (y + 7), (int) (x + 9), (int) (y + 3));
                                graphics.DrawLine(pen, (int) (x + 3), (int) (y + 6), (int) (x + 5), (int) (y + 8));
                                graphics.DrawLine(pen, (int) (x + 5), (int) (y + 8), (int) (x + 9), (int) (y + 4));
                                graphics.DrawLine(pen, (int) (x + 3), (int) (y + 7), (int) (x + 5), (int) (y + 9));
                                graphics.DrawLine(pen, (int) (x + 5), (int) (y + 9), (int) (x + 9), (int) (y + 5));
                            }
                        }
                        else if (this.m_DataGridView.Columns[this.NextColumn].CellType == typeof(DataGridViewImageCell))
                        {
                            if (this.m_DataGridView[this.NextColumn, this.NextRow].Value != null)
                            {
                                int num10 = layoutRectangle.X;
                                int num11 = layoutRectangle.Y;
                                Region region = graphics.Clip.Clone();
                                Image image = (Image) this.m_DataGridView[this.NextColumn, this.NextRow].Value;
                                DataGridViewImageCell cell = (DataGridViewImageCell) this.m_DataGridView[this.NextColumn, this.NextRow];
                                if ((cell.ImageLayout == DataGridViewImageCellLayout.Normal) || (cell.ImageLayout == DataGridViewImageCellLayout.NotSet))
                                {
                                    switch (this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.Alignment)
                                    {
                                        case DataGridViewContentAlignment.NotSet:
                                            num10 = layoutRectangle.X;
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (image.Height / 2);
                                            break;

                                        case DataGridViewContentAlignment.TopLeft:
                                            num10 = layoutRectangle.X;
                                            num11 = layoutRectangle.Y;
                                            break;

                                        case DataGridViewContentAlignment.TopCenter:
                                            num10 = (layoutRectangle.X + (layoutRectangle.Width / 2)) - (image.Width / 2);
                                            num11 = layoutRectangle.Y;
                                            break;

                                        case DataGridViewContentAlignment.TopRight:
                                            num10 = (layoutRectangle.X + layoutRectangle.Width) - image.Width;
                                            num11 = layoutRectangle.Y;
                                            break;

                                        case DataGridViewContentAlignment.MiddleLeft:
                                            num10 = layoutRectangle.X;
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (image.Height / 2);
                                            break;

                                        case DataGridViewContentAlignment.MiddleCenter:
                                            num10 = (layoutRectangle.X + (layoutRectangle.Width / 2)) - (image.Width / 2);
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (image.Height / 2);
                                            break;

                                        case DataGridViewContentAlignment.BottomCenter:
                                            num10 = (layoutRectangle.X + (layoutRectangle.Width / 2)) - (image.Width / 2);
                                            num11 = (layoutRectangle.Y + layoutRectangle.Height) - image.Height;
                                            break;

                                        case DataGridViewContentAlignment.BottomRight:
                                            num10 = (layoutRectangle.X + layoutRectangle.Width) - image.Width;
                                            num11 = (layoutRectangle.Y + layoutRectangle.Height) - image.Height;
                                            break;

                                        case DataGridViewContentAlignment.MiddleRight:
                                            num10 = (layoutRectangle.X + layoutRectangle.Width) - image.Width;
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (image.Height / 2);
                                            break;

                                        case DataGridViewContentAlignment.BottomLeft:
                                            num10 = layoutRectangle.X;
                                            num11 = (layoutRectangle.Y + layoutRectangle.Height) - image.Height;
                                            break;
                                    }
                                    graphics.Clip = new Region(layoutRectangle);
                                    graphics.DrawImage(image, num10, num11, image.Width, image.Height);
                                    graphics.Clip = region;
                                }
                                else if (cell.ImageLayout == DataGridViewImageCellLayout.Stretch)
                                {
                                    graphics.DrawImage(image, layoutRectangle.Left, layoutRectangle.Top, layoutRectangle.Width, layoutRectangle.Height);
                                }
                                else if (cell.ImageLayout == DataGridViewImageCellLayout.Zoom)
                                {
                                    float num12 = 0f;
                                    float num13 = 0f;
                                    float num14 = ((float) image.Height) / ((float) image.Width);
                                    float num15 = ((float) layoutRectangle.Height) / ((float) layoutRectangle.Width);
                                    if (num14 < num15)
                                    {
                                        num12 = layoutRectangle.Width;
                                        num13 = num12 * num14;
                                    }
                                    else
                                    {
                                        num13 = layoutRectangle.Height;
                                        num12 = num13 / num14;
                                    }
                                    int num16 = (int) num12;
                                    int num17 = (int) num13;
                                    switch (this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.Alignment)
                                    {
                                        case DataGridViewContentAlignment.NotSet:
                                            num10 = layoutRectangle.X;
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (num17 / 2);
                                            break;

                                        case DataGridViewContentAlignment.TopLeft:
                                            num10 = layoutRectangle.X;
                                            num11 = layoutRectangle.Y;
                                            break;

                                        case DataGridViewContentAlignment.TopCenter:
                                            num10 = (layoutRectangle.X + (layoutRectangle.Width / 2)) - (num16 / 2);
                                            num11 = layoutRectangle.Y;
                                            break;

                                        case DataGridViewContentAlignment.TopRight:
                                            num10 = (layoutRectangle.X + layoutRectangle.Width) - num16;
                                            num11 = layoutRectangle.Y;
                                            break;

                                        case DataGridViewContentAlignment.MiddleLeft:
                                            num10 = layoutRectangle.X;
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (num17 / 2);
                                            break;

                                        case DataGridViewContentAlignment.MiddleCenter:
                                            num10 = (layoutRectangle.X + (layoutRectangle.Width / 2)) - (num16 / 2);
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (num17 / 2);
                                            break;

                                        case DataGridViewContentAlignment.BottomCenter:
                                            num10 = (layoutRectangle.X + (layoutRectangle.Width / 2)) - (num16 / 2);
                                            num11 = (layoutRectangle.Y + layoutRectangle.Height) - num17;
                                            break;

                                        case DataGridViewContentAlignment.BottomRight:
                                            num10 = (layoutRectangle.X + layoutRectangle.Width) - num16;
                                            num11 = (layoutRectangle.Y + layoutRectangle.Height) - num17;
                                            break;

                                        case DataGridViewContentAlignment.MiddleRight:
                                            num10 = (layoutRectangle.X + layoutRectangle.Width) - num16;
                                            num11 = (layoutRectangle.Y + (layoutRectangle.Height / 2)) - (num17 / 2);
                                            break;

                                        case DataGridViewContentAlignment.BottomLeft:
                                            num10 = layoutRectangle.X;
                                            num11 = (layoutRectangle.Y + layoutRectangle.Height) - num17;
                                            break;
                                    }
                                    graphics.DrawImage(image, (float) num10, (float) num11, num12, num13);
                                }
                            }
                        }
                        else if (this.m_DataGridView.Columns[this.NextColumn].CellType == typeof(DataGridViewLinkCell))
                        {
                            Font font2 = new Font(this.m_DataGridView[this.NextColumn, this.NextRow].InheritedStyle.Font, FontStyle.Underline);
                            graphics.DrawString(this.m_DataGridView[this.NextColumn, this.NextRow].FormattedValue.ToString(), font2, brush2, layoutRectangle, format);
                            font2.Dispose();
                        }
                        if (both == CellBorders.Horizontal)
                        {
                            if ((this.NextColumn + 1) >= this.m_DataGridView.Columns.Count)
                            {
                                graphics.DrawLine(pen, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y + layoutRectangle.Height);
                            }
                            else if ((((layoutRectangle.X + this.m_DataGridView.Columns[this.NextColumn + 1].Width) + this.m_DataGridView.Columns[this.NextColumn + 1].Width) - left) > width)
                            {
                                graphics.DrawLine(pen, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y + layoutRectangle.Height);
                            }
                        }
                        layoutRectangle.Y += this.m_DataGridView.Rows[this.NextRow].Height;
                    }
                    this.NextRow++;
                    if ((both == CellBorders.Vertical) && ((((layoutRectangle.Y + layoutRectangle.Height) - top) > height) || (this.NextRow >= count)))
                    {
                        graphics.DrawLine(pen, layoutRectangle.X, layoutRectangle.Y, layoutRectangle.X + layoutRectangle.Width, layoutRectangle.Y);
                    }
                }
                layoutRectangle.X += this.m_DataGridView.Columns[this.NextColumn].Width;
                goto Label_1487;
            Label_13EF:
                if (this.m_DataGridView.Rows[this.NextRow].Visible)
                {
                    layoutRectangle.Height = this.m_DataGridView.Rows[this.NextRow].Height;
                    layoutRectangle.Y += this.m_DataGridView.Rows[this.NextRow].Height;
                }
                this.NextRow++;
            Label_1465:
                if ((((layoutRectangle.Y + layoutRectangle.Height) - top) <= height) && (this.NextRow < count))
                {
                    goto Label_13EF;
                }
            Label_1487:
                this.NextColumn++;
                if (this.NextColumn < this.m_DataGridView.Columns.Count)
                {
                    layoutRectangle.Width = this.m_DataGridView.Columns[this.NextColumn].Width;
                }
                flag4 = false;
                if (this.NextColumn < this.m_DataGridView.Columns.Count)
                {
                    this.NextRow = nextRow;
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            StringFormat format2 = new StringFormat();
            format2.Alignment = StringAlignment.Center;
            format2.LineAlignment = StringAlignment.Center;
            Font font3 = new Font("Microsoft Sans Serif", 24f);
            string s = "Teroid\nData Grid View Tool Strip 2.0\n\nFreeware\n\nwww.teroid.com";
            e.Graphics.DrawString(s, font3, Brushes.Black, new Rectangle(left, top, width, height), format2);
            font3.Dispose();
            format2.Dispose();
            if (this.NextRow >= count)
            {
                flag2 = false;
            }
            else
            {
                flag2 = true;
                if (!flag)
                {
                    this.NextColumn = 0;
                }
            }
            if (!flag && !flag2)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
            pen.Dispose();
            font.Dispose();
            brush.Dispose();
            brush2.Dispose();
            brush3.Dispose();
            brush4.Dispose();
        }

        public void PrintPreview()
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property not set");
            }
            this.CalculateNumberOfPages();
            this.NextColumn = 0;
            this.NextRow = 0;
            this.PageNumber = 1;
            this.pdlg.PrinterSettings.MinimumPage = 1;
            this.pdlg.PrinterSettings.MaximumPage = this.NumberOfPages;
            this.pdlg.PrinterSettings.FromPage = 1;
            this.pdlg.PrinterSettings.ToPage = this.NumberOfPages;
            this.pprev2.ShowDialog();
        }

        private void SetStringFormatAlignment(StringFormat sf, DataGridViewContentAlignment a)
        {
            DataGridViewContentAlignment alignment = a;
            if (alignment <= DataGridViewContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case DataGridViewContentAlignment.NotSet:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Center;
                        return;

                    case DataGridViewContentAlignment.TopLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Near;
                        return;

                    case DataGridViewContentAlignment.TopCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Near;
                        return;

                    case (DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.TopLeft):
                        return;

                    case DataGridViewContentAlignment.TopRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Near;
                        return;

                    case DataGridViewContentAlignment.MiddleLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Center;
                        return;

                    case DataGridViewContentAlignment.MiddleCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        return;
                }
            }
            else if (alignment <= DataGridViewContentAlignment.BottomLeft)
            {
                switch (alignment)
                {
                    case DataGridViewContentAlignment.MiddleRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Center;
                        break;

                    case DataGridViewContentAlignment.BottomLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                }
            }
            else
            {
                switch (alignment)
                {
                    case DataGridViewContentAlignment.BottomCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Far;
                        return;

                    case DataGridViewContentAlignment.BottomRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Far;
                        return;

                    default:
                        return;
                }
            }
        }

        private void ShiftColumnAndRowNumbersByOnePage()
        {
            int num = (this.psudlg.PageSettings.Bounds.Width - this.psudlg.PageSettings.Margins.Left) - this.psudlg.PageSettings.Margins.Right;
            int num2 = (this.psudlg.PageSettings.Bounds.Height - this.psudlg.PageSettings.Margins.Top) - this.psudlg.PageSettings.Margins.Bottom;
            int width = this.m_DataGridView.Columns[this.NextColumn].Width;
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            Control control = new Control();
            Graphics graphics = control.CreateGraphics();
            int height = (int) graphics.MeasureString(this.m_PageFooter, this.m_PageFooterFont).Height;
            int num6 = (int) graphics.MeasureString(this.m_PageHeader, this.m_PageHeaderFont).Height;
            int num7 = (int) graphics.MeasureString(this.m_PageNumberFormat, this.m_PageNumberFont).Height;
            graphics.Dispose();
            control.Dispose();
            while ((width <= num) && (this.NextColumn < this.m_DataGridView.Columns.Count))
            {
                if (this.m_DataGridView.Columns[this.NextColumn].Visible)
                {
                    width += this.m_DataGridView.Columns[this.NextColumn].Width;
                }
                this.NextColumn++;
            }
            if (this.NextColumn == this.m_DataGridView.Columns.Count)
            {
                this.NextColumn = 0;
                if (this.m_NumberPages)
                {
                    num2 -= num7;
                }
                if (this.m_PageHeader != "")
                {
                    num2 -= num6;
                }
                if (this.m_PageFooter != "")
                {
                    num2 -= height;
                }
                if (this.m_DataGridView.ColumnHeadersVisible)
                {
                    num2 -= this.m_DataGridView.ColumnHeadersHeight;
                }
                int num8 = this.m_DataGridView.Rows[this.NextRow].Height;
                while (((num8 + this.m_DataGridView.Rows[this.NextRow].Height) <= num2) && (this.NextRow < count))
                {
                    if (this.m_DataGridView.Rows[this.NextRow].Visible)
                    {
                        num8 += this.m_DataGridView.Rows[this.NextRow].Height;
                    }
                    this.NextRow++;
                }
            }
        }

        [Description("The data grid view which the control will print"), DefaultValue((string) null), Category("Data")]
        public System.Windows.Forms.DataGridView DataGridView
        {
            get
            {
                return this.m_DataGridView;
            }
            set
            {
                this.m_DataGridView = value;
            }
        }

        private enum CellBorders
        {
            Both,
            None,
            Vertical,
            Horizontal
        }

        public enum PageHeaderAndFooterLocations
        {
            Left,
            Center,
            Right
        }

        public enum PageNumberLocations
        {
            TopLeft,
            TopCenter,
            TopRight,
            BottomLeft,
            BottomCenter,
            BottomRight
        }
    }
}

