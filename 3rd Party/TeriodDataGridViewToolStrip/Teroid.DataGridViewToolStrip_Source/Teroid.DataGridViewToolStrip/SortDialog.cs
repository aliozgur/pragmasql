namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Teroid.DataGridViewToolStrip.Properties;

    internal class SortDialog : Form
    {
        private IContainer components;
        private ListBox lbNotSorted;
        private ListBox lbSorted;
        private ArrayList m_Columns;
        private string m_SortOrder;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStrip toolStrip2;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbAllToUnsorted;
        private ToolStripButton tsbApply;
        private ToolStripButton tsbClose;
        private ToolStripButton tsbMoveDown;
        private ToolStripButton tsbMoveToSorted;
        private ToolStripButton tsbMoveToUnsorted;
        private ToolStripButton tsbMoveUp;

        internal event EventHandler SortOrderSet;

        public SortDialog()
        {
            this.InitializeComponent();
            this.m_Columns = new ArrayList();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SortDialog));
            this.panel1 = new Panel();
            this.toolStrip2 = new ToolStrip();
            this.tsbMoveToSorted = new ToolStripButton();
            this.tsbMoveToUnsorted = new ToolStripButton();
            this.tsbAllToUnsorted = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.tsbMoveUp = new ToolStripButton();
            this.tsbMoveDown = new ToolStripButton();
            this.lbSorted = new ListBox();
            this.lbNotSorted = new ListBox();
            this.toolStrip1 = new ToolStrip();
            this.tsbClose = new ToolStripButton();
            this.tsbApply = new ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BackColor = SystemColors.Control;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Controls.Add(this.lbSorted);
            this.panel1.Controls.Add(this.lbNotSorted);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x147, 0x108);
            this.panel1.TabIndex = 0;
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = DockStyle.None;
            this.toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new ToolStripItem[] { this.tsbMoveToSorted, this.tsbMoveToUnsorted, this.tsbAllToUnsorted, this.toolStripSeparator2, this.tsbMoveUp, this.tsbMoveDown });
            this.toolStrip2.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip2.Location = new Point(150, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new Size(0x19, 0xed);
            this.toolStrip2.TabIndex = 7;
            this.toolStrip2.Text = "toolStrip2";
            this.tsbMoveToSorted.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMoveToSorted.Image = (Image) manager.GetObject("tsbMoveToSorted.Image");
            this.tsbMoveToSorted.ImageTransparentColor = Color.Magenta;
            this.tsbMoveToSorted.Name = "tsbMoveToSorted";
            this.tsbMoveToSorted.Size = new Size(0x17, 20);
            this.tsbMoveToSorted.Text = "toolStripButton1";
            this.tsbMoveToSorted.Click += new EventHandler(this.tsbMoveToSorted_Click);
            this.tsbMoveToUnsorted.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMoveToUnsorted.Image = (Image) manager.GetObject("tsbMoveToUnsorted.Image");
            this.tsbMoveToUnsorted.ImageTransparentColor = Color.Magenta;
            this.tsbMoveToUnsorted.Name = "tsbMoveToUnsorted";
            this.tsbMoveToUnsorted.Size = new Size(0x17, 20);
            this.tsbMoveToUnsorted.Text = "toolStripButton2";
            this.tsbMoveToUnsorted.Click += new EventHandler(this.tsbMoveToUnsorted_Click);
            this.tsbAllToUnsorted.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbAllToUnsorted.Image = Resources.allleft;
            this.tsbAllToUnsorted.ImageTransparentColor = Color.Magenta;
            this.tsbAllToUnsorted.Name = "tsbAllToUnsorted";
            this.tsbAllToUnsorted.Size = new Size(0x17, 20);
            this.tsbAllToUnsorted.Text = "toolStripButton1";
            this.tsbAllToUnsorted.Click += new EventHandler(this.tsbAllToUnsorted_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(0x17, 6);
            this.tsbMoveUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMoveUp.Image = (Image) manager.GetObject("tsbMoveUp.Image");
            this.tsbMoveUp.ImageTransparentColor = Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new Size(0x17, 20);
            this.tsbMoveUp.Text = "toolStripButton3";
            this.tsbMoveUp.Click += new EventHandler(this.tsbMoveUp_Click);
            this.tsbMoveDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbMoveDown.Image = (Image) manager.GetObject("tsbMoveDown.Image");
            this.tsbMoveDown.ImageTransparentColor = Color.Magenta;
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Size = new Size(0x17, 20);
            this.tsbMoveDown.Text = "toolStripButton4";
            this.tsbMoveDown.Click += new EventHandler(this.tsbMoveDown_Click);
            this.lbSorted.BorderStyle = BorderStyle.None;
            this.lbSorted.FormattingEnabled = true;
            this.lbSorted.IntegralHeight = false;
            this.lbSorted.Location = new Point(0xaf, 0);
            this.lbSorted.Name = "lbSorted";
            this.lbSorted.Size = new Size(150, 0xed);
            this.lbSorted.TabIndex = 2;
            this.lbNotSorted.BorderStyle = BorderStyle.None;
            this.lbNotSorted.FormattingEnabled = true;
            this.lbNotSorted.IntegralHeight = false;
            this.lbNotSorted.Location = new Point(0, 0);
            this.lbNotSorted.Name = "lbNotSorted";
            this.lbNotSorted.Size = new Size(150, 0xed);
            this.lbNotSorted.TabIndex = 1;
            this.toolStrip1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = DockStyle.None;
            this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbClose, this.tsbApply });
            this.toolStrip1.Location = new Point(0, 0xed);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x146, 0x19);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbClose.Alignment = ToolStripItemAlignment.Right;
            this.tsbClose.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = (Image) manager.GetObject("tsbClose.Image");
            this.tsbClose.ImageTransparentColor = Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new Size(0x17, 0x16);
            this.tsbClose.Text = "toolStripButton1";
            this.tsbClose.Click += new EventHandler(this.tsbClose_Click);
            this.tsbApply.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbApply.Image = Resources.apply;
            this.tsbApply.ImageTransparentColor = Color.Magenta;
            this.tsbApply.Name = "tsbApply";
            this.tsbApply.Size = new Size(0x17, 0x16);
            this.tsbApply.Text = "toolStripButton1";
            this.tsbApply.Click += new EventHandler(this.tsbApply_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x147, 0x108);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SortDialog";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "SortDialog";
            this.panel1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        internal void Populate()
        {
            this.lbNotSorted.Items.Clear();
            for (int i = 0; i < this.m_Columns.Count; i++)
            {
                this.lbNotSorted.Items.Add(this.m_Columns[i]);
            }
        }

        private void tsbAllToUnsorted_Click(object sender, EventArgs e)
        {
            this.lbSorted.Items.Clear();
            this.Populate();
        }

        private void tsbApply_Click(object sender, EventArgs e)
        {
            this.m_SortOrder = "";
            for (int i = 0; i < this.lbSorted.Items.Count; i++)
            {
                if (this.m_SortOrder != "")
                {
                    this.m_SortOrder = this.m_SortOrder + ", ";
                }
                this.m_SortOrder = this.m_SortOrder + this.lbSorted.Items[i].ToString();
            }
            if (this.SortOrderSet != null)
            {
                this.SortOrderSet(this, new EventArgs());
            }
            base.Hide();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            if (this.lbSorted.SelectedIndex < (this.lbSorted.Items.Count - 1))
            {
                int selectedIndex = this.lbSorted.SelectedIndex;
                this.lbSorted.Items.Insert(selectedIndex + 2, this.lbSorted.Items[selectedIndex]);
                this.lbSorted.Items.RemoveAt(selectedIndex);
                this.lbSorted.SelectedIndex = selectedIndex + 1;
            }
        }

        private void tsbMoveToSorted_Click(object sender, EventArgs e)
        {
            if (this.lbNotSorted.SelectedIndex >= 0)
            {
                this.lbSorted.Items.Add(this.lbNotSorted.SelectedItem);
                this.lbNotSorted.Items.Remove(this.lbNotSorted.SelectedItem);
            }
            if (this.lbNotSorted.Items.Count > 0)
            {
                this.lbNotSorted.SelectedIndex = 0;
            }
        }

        private void tsbMoveToUnsorted_Click(object sender, EventArgs e)
        {
            if (this.lbSorted.SelectedIndex >= 0)
            {
                this.lbNotSorted.Items.Add(this.lbSorted.SelectedItem);
                this.lbSorted.Items.Remove(this.lbSorted.SelectedItem);
            }
            if (this.lbSorted.Items.Count > 0)
            {
                this.lbSorted.SelectedIndex = 0;
            }
        }

        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            if (this.lbSorted.SelectedIndex > 0)
            {
                int selectedIndex = this.lbSorted.SelectedIndex;
                this.lbSorted.Items.Insert(selectedIndex - 1, this.lbSorted.Items[selectedIndex]);
                this.lbSorted.Items.RemoveAt(selectedIndex + 1);
                this.lbSorted.SelectedIndex = selectedIndex - 1;
            }
        }

        internal ArrayList Columns
        {
            get
            {
                return this.m_Columns;
            }
        }

        internal string SortOrder
        {
            get
            {
                return this.m_SortOrder;
            }
        }
    }
}

