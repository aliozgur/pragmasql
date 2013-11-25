namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Teroid.DataGridViewToolStrip.Properties;

    internal class FilterDialog : Form
    {
        private IContainer components;
        private DataGridView dgv;
        private System.Windows.Forms.BindingSource m_BindingSource;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbApply;
        private ToolStripButton tsbClear;
        private ToolStripButton tsbHide;

        public FilterDialog()
        {
            this.InitializeComponent();
        }

        public void ApplyFilter()
        {
            this.dgv.CurrentCell = null;
            if (this.m_BindingSource != null)
            {
                string filter = this.GetFilter();
                this.m_BindingSource.Filter = filter;
            }
        }

        public void Clear()
        {
            this.dgv.CurrentCell = null;
            for (int i = 0; i < this.dgv.Rows.Count; i++)
            {
                if (!this.dgv[0, i].ReadOnly)
                {
                    this.dgv[0, i].Value = "(none)";
                }
                if (!this.dgv[1, i].ReadOnly)
                {
                    this.dgv[1, i].Value = "";
                }
                if (this.dgv[0, i].Style.BackColor == SystemColors.Info)
                {
                    this.dgv[0, i].Style.BackColor = this.dgv.DefaultCellStyle.BackColor;
                }
                if (this.dgv[1, i].Style.BackColor == SystemColors.Info)
                {
                    this.dgv[1, i].Style.BackColor = this.dgv.DefaultCellStyle.BackColor;
                }
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

        private void EmptyControl()
        {
            this.dgv.Rows.Clear();
            this.dgv.Columns.Clear();
        }

        private DataTypes GetDataType(string strType)
        {
            DataTypes notSupported = DataTypes.NotSupported;
            switch (strType)
            {
                case "System.DateTime":
                    return DataTypes.DateTime;

                case "System.Boolean":
                    return DataTypes.Boolean;

                case "System.String":
                    return DataTypes.Text;

                case "System.Byte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    return DataTypes.Integer;

                case "System.Decimal":
                case "System.Single":
                case "System.Double":
                    return DataTypes.Float;
            }
            return notSupported;
        }

        private string GetDateAsSQLstring(string strDate)
        {
            DateTime time = Convert.ToDateTime(strDate);
            return ("#" + time.Month.ToString() + "/" + time.Day.ToString() + "/" + time.Year.ToString() + "#");
        }

        private string GetFilter()
        {
            string str = "";
            string str2 = "'";
            DataView dataView = null;
            DataTypes notSupported = DataTypes.NotSupported;
            if (this.m_BindingSource != null)
            {
                DataRowView view2 = (DataRowView) this.m_BindingSource[0];
                dataView = view2.DataView;
            }
            if (this.ValidateCriteria())
            {
                for (int i = 0; i < this.dgv.Rows.Count; i++)
                {
                    notSupported = this.GetDataType(dataView.Table.Columns[i].DataType.ToString());
                    if (this.dgv[0, i].Value.ToString() == "True")
                    {
                        if (str.Length > 0)
                        {
                            str = str + " And ";
                        }
                        str = str + this.dgv.Rows[i].HeaderCell.Value.ToString() + " = True";
                    }
                    else if (this.dgv[0, i].Value.ToString() == "False")
                    {
                        if (str.Length > 0)
                        {
                            str = str + " And ";
                        }
                        str = str + this.dgv.Rows[i].HeaderCell.Value.ToString() + " = False";
                    }
                    else if (this.dgv[0, i].Value.ToString() == "Null")
                    {
                        if (str.Length > 0)
                        {
                            str = str + " And ";
                        }
                        str = str + this.dgv.Rows[i].HeaderCell.Value.ToString() + " Is Null";
                    }
                    else if (this.dgv[0, i].Value.ToString() == "Not Null")
                    {
                        if (str.Length > 0)
                        {
                            str = str + " And ";
                        }
                        str = str + this.dgv.Rows[i].HeaderCell.Value.ToString() + " Is Not Null";
                    }
                    else if (this.dgv[0, i].Value.ToString() == "Like")
                    {
                        if (str.Length > 0)
                        {
                            str = str + " And ";
                        }
                        string str3 = str;
                        str = str3 + this.dgv.Rows[i].HeaderCell.Value.ToString() + " Like " + str2 + this.dgv[1, i].Value.ToString() + str2;
                    }
                    else if (this.dgv[0, i].Value.ToString() == "Not Like")
                    {
                        if (str.Length > 0)
                        {
                            str = str + " And ";
                        }
                        string str4 = str;
                        str = str4 + this.dgv.Rows[i].HeaderCell.Value.ToString() + " Not Like " + str2 + this.dgv[1, i].Value.ToString() + str2;
                    }
                    else if ((((this.dgv[0, i].Value.ToString() == "=") || (this.dgv[0, i].Value.ToString() == ">")) || ((this.dgv[0, i].Value.ToString() == "<") || (this.dgv[0, i].Value.ToString() == ">="))) || ((this.dgv[0, i].Value.ToString() == "<=") || (this.dgv[0, i].Value.ToString() == "<>")))
                    {
                        if (str.Length > 0)
                        {
                            str = str + " And ";
                        }
                        switch (notSupported)
                        {
                            case DataTypes.Text:
                            {
                                string str5 = str;
                                str = str5 + this.dgv.Rows[i].HeaderCell.Value.ToString() + " " + this.dgv[0, i].Value.ToString() + " " + str2 + this.dgv[1, i].Value.ToString() + str2;
                                break;
                            }
                            case DataTypes.Integer:
                            case DataTypes.Float:
                            {
                                string str6 = str;
                                str = str6 + this.dgv.Rows[i].HeaderCell.Value.ToString() + " " + this.dgv[0, i].Value.ToString() + " " + this.dgv[1, i].Value.ToString();
                                break;
                            }
                            case DataTypes.DateTime:
                            {
                                string str7 = str;
                                str = str7 + this.dgv.Rows[i].HeaderCell.Value.ToString() + " " + this.dgv[0, i].Value.ToString() + " " + this.GetDateAsSQLstring(this.dgv[1, i].Value.ToString());
                                break;
                            }
                        }
                    }
                }
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.toolStrip1 = new ToolStrip();
            this.tsbClear = new ToolStripButton();
            this.tsbHide = new ToolStripButton();
            this.tsbApply = new ToolStripButton();
            this.panel1 = new Panel();
            this.dgv = new DataGridView();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.dgv).BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = DockStyle.None;
            this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbClear, this.tsbHide, this.tsbApply });
            this.toolStrip1.Location = new Point(0, 0xed);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x151, 0x19);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbClear.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbClear.Image = Resources.clear;
            this.tsbClear.ImageTransparentColor = Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new Size(0x17, 0x16);
            this.tsbClear.Text = "toolStripButton1";
            this.tsbClear.Click += new EventHandler(this.tsbClear_Click);
            this.tsbHide.Alignment = ToolStripItemAlignment.Right;
            this.tsbHide.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbHide.Image = Resources.closedropdown;
            this.tsbHide.ImageTransparentColor = Color.Magenta;
            this.tsbHide.Name = "tsbHide";
            this.tsbHide.Size = new Size(0x17, 0x16);
            this.tsbHide.Text = "toolStripButton4";
            this.tsbHide.Click += new EventHandler(this.tsbHide_Click);
            this.tsbApply.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbApply.Image = Resources.apply;
            this.tsbApply.ImageTransparentColor = Color.Magenta;
            this.tsbApply.Name = "tsbApply";
            this.tsbApply.Size = new Size(0x17, 0x16);
            this.tsbApply.Text = "toolStripButton2";
            this.tsbApply.Click += new EventHandler(this.tsbApply_Click);
            this.panel1.BackColor = SystemColors.GradientInactiveCaption;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x153, 0x108);
            this.panel1.TabIndex = 1;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BorderStyle = BorderStyle.None;
            this.dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.EditMode = DataGridViewEditMode.EditOnEnter;
            this.dgv.Location = new Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ShowEditingIcon = false;
            this.dgv.Size = new Size(0x153, 0xed);
            this.dgv.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x153, 0x108);
            base.ControlBox = false;
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "FilterDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "FilterDialog";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.dgv).EndInit();
            base.ResumeLayout(false);
        }

        private void Populate()
        {
            DataView dataView = null;
            if (this.m_BindingSource != null)
            {
                DataRowView view2 = (DataRowView) this.m_BindingSource[0];
                dataView = view2.DataView;
            }
            this.EmptyControl();
            DataTypes notSupported = DataTypes.NotSupported;
            this.dgv.RowHeadersWidth = 0x77;
            DataGridViewComboBoxCell cellTemplate = new DataGridViewComboBoxCell();
            this.dgv.Columns.Add(new DataGridViewColumn(cellTemplate));
            this.dgv.Columns[0].Name = "Condition";
            this.dgv.Columns[0].Width = 100;
            DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
            this.dgv.Columns.Add(new DataGridViewColumn(cell3));
            this.dgv.Columns[1].Name = "Value";
            this.dgv.Columns[1].Width = 120;
            for (int i = 0; i < dataView.Table.Columns.Count; i++)
            {
                notSupported = this.GetDataType(dataView.Table.Columns[i].DataType.ToString());
                this.dgv.Rows.Add(new object[] { "(none)", "" });
                this.dgv.Rows[i].HeaderCell.Value = dataView.Table.Columns[i].ColumnName;
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell) this.dgv[0, i];
                switch (notSupported)
                {
                    case DataTypes.Text:
                        cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Like", "Not Like", "Null", "Not Null" });
                        cell.Value = "(none)";
                        cell.MaxDropDownItems = 11;
                        break;

                    case DataTypes.Integer:
                        cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Null", "Not Null" });
                        cell.Value = "(none)";
                        cell.MaxDropDownItems = 9;
                        break;

                    case DataTypes.Float:
                        cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Null", "Not Null" });
                        cell.Value = "(none)";
                        cell.MaxDropDownItems = 9;
                        break;

                    case DataTypes.DateTime:
                        cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Null", "Not Null" });
                        cell.Value = "(none)";
                        cell.MaxDropDownItems = 11;
                        break;

                    case DataTypes.Boolean:
                        cell.Items.AddRange(new object[] { "(none)", "True", "False", "Null", "Not Null" });
                        cell.Value = "(none)";
                        cell.MaxDropDownItems = 5;
                        this.dgv[1, i].Style.BackColor = SystemColors.GradientInactiveCaption;
                        this.dgv[1, i].Style.SelectionBackColor = SystemColors.GradientInactiveCaption;
                        this.dgv[1, i].ReadOnly = true;
                        break;

                    case DataTypes.NotSupported:
                        cell.Items.AddRange(new object[] { "" });
                        cell.Value = "";
                        this.dgv[0, i].Style.BackColor = SystemColors.GradientInactiveCaption;
                        this.dgv[0, i].Style.SelectionBackColor = SystemColors.GradientInactiveCaption;
                        this.dgv[0, i].ReadOnly = true;
                        this.dgv[1, i].Style.BackColor = SystemColors.GradientInactiveCaption;
                        this.dgv[1, i].Style.SelectionBackColor = SystemColors.GradientInactiveCaption;
                        this.dgv[1, i].ReadOnly = true;
                        break;
                }
            }
            this.dgv.CurrentCell = null;
        }

        private void tsbApply_Click(object sender, EventArgs e)
        {
            this.ApplyFilter();
            base.Hide();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void tsbHide_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        private bool ValidateCriteria()
        {
            string text = "";
            DataView dataView = null;
            DataTypes notSupported = DataTypes.NotSupported;
            if (this.m_BindingSource != null)
            {
                DataRowView view2 = (DataRowView) this.m_BindingSource[0];
                dataView = view2.DataView;
            }
            for (int i = 0; i < dataView.Table.Columns.Count; i++)
            {
                notSupported = this.GetDataType(dataView.Table.Columns[i].DataType.ToString());
                if (this.dgv[0, i].Style.BackColor == SystemColors.Info)
                {
                    this.dgv[0, i].Style.BackColor = this.dgv.DefaultCellStyle.BackColor;
                }
                if (this.dgv[1, i].Style.BackColor == SystemColors.Info)
                {
                    this.dgv[1, i].Style.BackColor = this.dgv.DefaultCellStyle.BackColor;
                }
                if (this.dgv[1, i].Value.ToString() != "")
                {
                    switch (notSupported)
                    {
                        case DataTypes.Integer:
                            try
                            {
                                Convert.ToInt64(this.dgv[1, i].Value);
                            }
                            catch
                            {
                                text = text + this.dgv.Rows[i].HeaderCell.Value + " invalid\n";
                                this.dgv[1, i].Style.BackColor = SystemColors.Info;
                            }
                            goto Label_0264;

                        case DataTypes.Float:
                            goto Label_019B;

                        case DataTypes.DateTime:
                            goto Label_0201;
                    }
                }
                goto Label_0264;
            Label_019B:
                try
                {
                    Convert.ToDouble(this.dgv[1, i].Value);
                }
                catch
                {
                    text = text + this.dgv.Rows[i].HeaderCell.Value + " invalid\n";
                    this.dgv[1, i].Style.BackColor = SystemColors.Info;
                }
                goto Label_0264;
            Label_0201:
                try
                {
                    Convert.ToDateTime(this.dgv[1, i].Value);
                }
                catch
                {
                    text = text + this.dgv.Rows[i].HeaderCell.Value + " invalid\n";
                    this.dgv[1, i].Style.BackColor = SystemColors.Info;
                }
            Label_0264:
                if ((((notSupported != DataTypes.Boolean) && (notSupported != DataTypes.NotSupported)) && ((this.dgv[0, i].Value.ToString() != "Null") && (this.dgv[0, i].Value.ToString() != "Not Null"))) && ((this.dgv[0, i].Value.ToString() != "(none)") && (this.dgv[1, i].Value.ToString() == "")))
                {
                    text = text + this.dgv.Rows[i].HeaderCell.Value + " value missing\n";
                    this.dgv[1, i].Style.BackColor = SystemColors.Info;
                }
                if ((((notSupported != DataTypes.Boolean) && (notSupported != DataTypes.NotSupported)) && (((this.dgv[0, i].Value.ToString() == "Null") || (this.dgv[0, i].Value.ToString() == "Not Null")) || (this.dgv[0, i].Value.ToString() == "(none)"))) && (this.dgv[1, i].Value.ToString() != ""))
                {
                    text = text + this.dgv.Rows[i].HeaderCell.Value + " condition missing\n";
                    this.dgv[0, i].Style.BackColor = SystemColors.Info;
                }
            }
            if (text != "")
            {
                MessageBox.Show(text);
            }
            if (text != "")
            {
                return false;
            }
            return true;
        }

        [DefaultValue((string) null), Description("The BindingSource object used to populate control"), Category("Data")]
        public System.Windows.Forms.BindingSource BindingSource
        {
            get
            {
                return this.m_BindingSource;
            }
            set
            {
                this.m_BindingSource = value;
                if (this.m_BindingSource != null)
                {
                    this.Populate();
                }
                if (this.m_BindingSource == null)
                {
                    this.EmptyControl();
                }
            }
        }

        [Browsable(false), Description("Filter string created from user input, and usable as the RowFilter property of a DataView or the Filter property of a BindingSource"), Category("Data")]
        public string Filter
        {
            get
            {
                return this.GetFilter();
            }
        }

        private enum DataTypes
        {
            Text,
            Integer,
            Float,
            DateTime,
            Boolean,
            NotSupported
        }

        public enum InvalidCriteriaActions
        {
            ShowMessageBox,
            RaiseException
        }
    }
}

