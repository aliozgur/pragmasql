namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Teroid.DataGridViewToolStrip.Properties;

    public class ReplaceDialog : Form
    {
        private IContainer components;
        private Label label1;
        private Label label2;
        private System.Windows.Forms.DataGridView m_DataGridView;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbClose;
        private ToolStripButton tsbGo;
        private TextBox txtReplace;
        private TextBox txtWith;

        public ReplaceDialog()
        {
            this.InitializeComponent();
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
            this.label2 = new Label();
            this.label1 = new Label();
            this.txtWith = new TextBox();
            this.txtReplace = new TextBox();
            this.toolStrip1 = new ToolStrip();
            this.tsbClose = new ToolStripButton();
            this.tsbGo = new ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtWith);
            this.panel1.Controls.Add(this.txtReplace);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x159, 0x5e);
            this.panel1.TabIndex = 0;
            this.label2.Location = new Point(11, 0x25);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x39, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "with";
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.label1.Location = new Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x39, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Replace";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.txtWith.AcceptsReturn = true;
            this.txtWith.Location = new Point(0x4a, 0x25);
            this.txtWith.Name = "txtWith";
            this.txtWith.Size = new Size(0x102, 20);
            this.txtWith.TabIndex = 2;
            this.txtReplace.Location = new Point(0x4a, 11);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new Size(0x102, 20);
            this.txtReplace.TabIndex = 1;
            this.toolStrip1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = DockStyle.None;
            this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbClose, this.tsbGo });
            this.toolStrip1.Location = new Point(1, 0x45);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x158, 0x19);
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
            base.ClientSize = new Size(0x159, 0x5e);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ReplaceDialog";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "ReplaceDialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }

        public void Reset()
        {
            this.txtReplace.Text = "";
            this.txtWith.Text = "";
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            base.Hide();
            this.Reset();
        }

        private void tsbGo_Click(object sender, EventArgs e)
        {
            string text = this.txtReplace.Text;
            string str2 = this.txtWith.Text;
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            int num2 = this.m_DataGridView.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    if (this.m_DataGridView[j, i].Value.ToString() == text)
                    {
                        this.m_DataGridView[j, i].Value = str2;
                    }
                }
            }
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

