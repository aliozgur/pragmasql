namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Teroid.DataGridViewToolStrip.Properties;

    internal class SearchDialog : Form
    {
        private Point cell = new Point(0, 0);
        private CheckBox chkMatchCase;
        private CheckBox chkWholeWord;
        private IContainer components;
        private System.Windows.Forms.DataGridView m_DataGridView;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbClose;
        private ToolStripButton tsbGo;
        private TextBox txtSearchString;

        public SearchDialog()
        {
            this.InitializeComponent();
        }

        private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            this.cell.X = 0;
            this.cell.Y = 0;
        }

        private void chkWholeWord_CheckedChanged(object sender, EventArgs e)
        {
            this.cell.X = 0;
            this.cell.Y = 0;
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
            this.panel1 = new Panel();
            this.txtSearchString = new TextBox();
            this.chkWholeWord = new CheckBox();
            this.chkMatchCase = new CheckBox();
            this.toolStrip1 = new ToolStrip();
            this.tsbClose = new ToolStripButton();
            this.tsbGo = new ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtSearchString);
            this.panel1.Controls.Add(this.chkWholeWord);
            this.panel1.Controls.Add(this.chkMatchCase);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xcc, 0x63);
            this.panel1.TabIndex = 0;
            this.txtSearchString.Location = new Point(11, 11);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new Size(0xb3, 20);
            this.txtSearchString.TabIndex = 6;
            this.txtSearchString.TextChanged += new EventHandler(this.txtSearchString_TextChanged);
            this.chkWholeWord.AutoSize = true;
            this.chkWholeWord.Checked = true;
            this.chkWholeWord.CheckState = CheckState.Checked;
            this.chkWholeWord.Location = new Point(0x6b, 0x2c);
            this.chkWholeWord.Name = "chkWholeWord";
            this.chkWholeWord.Size = new Size(0x4d, 0x11);
            this.chkWholeWord.TabIndex = 5;
            this.chkWholeWord.Text = "Whole text";
            this.chkWholeWord.UseVisualStyleBackColor = true;
            this.chkWholeWord.CheckedChanged += new EventHandler(this.chkWholeWord_CheckedChanged);
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.Checked = true;
            this.chkMatchCase.CheckState = CheckState.Checked;
            this.chkMatchCase.Location = new Point(11, 0x2c);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new Size(0x52, 0x11);
            this.chkMatchCase.TabIndex = 4;
            this.chkMatchCase.Text = "Match case";
            this.chkMatchCase.UseVisualStyleBackColor = true;
            this.chkMatchCase.CheckedChanged += new EventHandler(this.chkMatchCase_CheckedChanged);
            this.toolStrip1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = DockStyle.None;
            this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbClose, this.tsbGo });
            this.toolStrip1.Location = new Point(0, 0x4a);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0xcb, 0x19);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbClose.Alignment = ToolStripItemAlignment.Right;
            this.tsbClose.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = Resources.closedropdown;
            this.tsbClose.ImageTransparentColor = Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new Size(0x17, 0x16);
            this.tsbClose.Text = "toolStripButton1";
            this.tsbClose.Click += new EventHandler(this.tsbClose_Click);
            this.tsbGo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbGo.Image = Resources.go;
            this.tsbGo.ImageTransparentColor = Color.Magenta;
            this.tsbGo.Name = "tsbGo";
            this.tsbGo.Size = new Size(0x17, 0x16);
            this.tsbGo.Text = "toolStripButton1";
            this.tsbGo.Click += new EventHandler(this.tsbGo_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.GradientInactiveCaption;
            base.ClientSize = new Size(0xcc, 0x63);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SearchDialog";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "SearchDialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        public void Reset()
        {
            this.txtSearchString.Text = "";
            this.chkMatchCase.Checked = true;
            this.chkWholeWord.Checked = true;
            this.cell.X = 0;
            this.cell.Y = 0;
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            base.Hide();
            this.Reset();
        }

        private void tsbGo_Click(object sender, EventArgs e)
        {
            bool flag = false;
            bool flag2 = this.chkMatchCase.Checked;
            bool flag3 = this.chkWholeWord.Checked;
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            string text = this.txtSearchString.Text;
            string strB = "";
            if (!flag2)
            {
                text = text.ToUpper();
            }
            while (!flag && (this.cell.Y < count))
            {
                strB = this.m_DataGridView[this.cell.X, this.cell.Y].Value.ToString();
                if (!flag2)
                {
                    strB = strB.ToUpper();
                }
                if (flag3)
                {
                    if (string.Compare(text, strB) == 0)
                    {
                        this.m_DataGridView.CurrentCell = this.m_DataGridView[this.cell.X, this.cell.Y];
                        flag = true;
                    }
                }
                else if (strB.Contains(text))
                {
                    this.m_DataGridView.CurrentCell = this.m_DataGridView[this.cell.X, this.cell.Y];
                }
                if (this.cell.X < (this.m_DataGridView.Columns.Count - 1))
                {
                    this.cell.X++;
                }
                else
                {
                    this.cell.X = 0;
                    this.cell.Y++;
                }
            }
        }

        private void txtSearchString_TextChanged(object sender, EventArgs e)
        {
            this.cell.X = 0;
            this.cell.Y = 0;
        }

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
    }
}

