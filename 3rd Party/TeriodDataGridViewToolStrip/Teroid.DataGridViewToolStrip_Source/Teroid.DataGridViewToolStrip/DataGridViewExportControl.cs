namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(DataGridViewExportControl))]
    internal class DataGridViewExportControl : Component
    {
        private System.Windows.Forms.DataGridView m_DataGridView;
        private bool m_IncludeColumnNames;
        private bool m_OpenAfterExport = true;

        public event EventHandler ExportCompleted;

        private string ConvertFieldForCsv(string strField)
        {
            string str = strField;
            if (str == "")
            {
                return (Convert.ToString('"') + Convert.ToString('"'));
            }
            str = str.Trim();
            string oldValue = Convert.ToString('"');
            string newValue = Convert.ToString('"') + Convert.ToString('"');
            str = str.Replace(oldValue, newValue);
            bool flag = false;
            foreach (char ch in str)
            {
                switch (ch)
                {
                    case ',':
                    case '\n':
                    case '"':
                        flag = true;
                        goto Label_0090;
                }
            }
        Label_0090:
            if (flag)
            {
                str = '"' + str + '"';
            }
            return str;
        }

        public void ExportToCsv()
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property is null");
            }
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save As";
            dialog.Filter = "Comma separated values file (*.csv)|*.csv";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                this.ToCsv(filePath);
            }
            dialog.Dispose();
        }

        public void ExportToCsv(string FilePath)
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property is null");
            }
            this.ToCsv(FilePath);
        }

        public void ExportToHtml()
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property is null");
            }
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save As";
            dialog.Filter = "HTML file (*.htm;*.html)|*.htm;*.html";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                this.ToHtml(filePath);
            }
            dialog.Dispose();
        }

        public void ExportToHtml(string FilePath)
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property is null");
            }
            this.ToHtml(FilePath);
        }

        public void ExportToXml()
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property is null");
            }
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save As";
            dialog.Filter = "XML file (*.xml)|*.xml";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                this.ToXml(filePath);
            }
            dialog.Dispose();
        }

        public void ExportToXml(string FilePath)
        {
            if (this.m_DataGridView == null)
            {
                throw new Exception("DataGridView property is null");
            }
            this.ToXml(FilePath);
        }

        private void ToCsv(string FilePath)
        {
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            int num2 = this.m_DataGridView.Columns.Count;
            StreamWriter writer = new StreamWriter(FilePath);
            if (this.m_IncludeColumnNames)
            {
                string strField = "";
                for (int j = 0; j < num2; j++)
                {
                    strField = this.m_DataGridView.Columns[j].HeaderText;
                    strField = this.ConvertFieldForCsv(strField);
                    writer.Write(strField);
                    if (j < (num2 - 1))
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();
            }
            for (int i = 0; i < count; i++)
            {
                for (int k = 0; k < num2; k++)
                {
                    string str = this.m_DataGridView[k, i].FormattedValue.ToString();
                    str = this.ConvertFieldForCsv(str);
                    writer.Write(str);
                    if (k < (num2 - 1))
                    {
                        writer.Write(",");
                    }
                }
                if (i < (count - 1))
                {
                    writer.WriteLine();
                }
            }
            writer.Close();
            if (this.m_OpenAfterExport)
            {
                Process.Start(FilePath);
            }
            if (this.ExportCompleted != null)
            {
                this.ExportCompleted(this, new EventArgs());
            }
        }

        private void ToHtml(string FilePath)
        {
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            int num2 = this.m_DataGridView.Columns.Count;
            StreamWriter writer = new StreamWriter(FilePath);
            writer.WriteLine("<HTML>");
            writer.WriteLine("");
            writer.WriteLine("<HEAD>");
            writer.WriteLine("<TITLE>");
            writer.WriteLine("Data");
            writer.WriteLine("</TITLE>");
            writer.WriteLine("</HEAD>");
            writer.WriteLine("");
            writer.WriteLine("<BODY>");
            writer.WriteLine("");
            writer.WriteLine("<TABLE BORDER=1 CELLSPACING=0>");
            writer.WriteLine("");
            if (this.m_IncludeColumnNames)
            {
                string headerText = "";
                writer.WriteLine("<TR>");
                for (int j = 0; j < num2; j++)
                {
                    headerText = this.m_DataGridView.Columns[j].HeaderText;
                    if (headerText.Length == 0)
                    {
                        headerText = "&nbsp";
                    }
                    writer.Write("<TH>");
                    writer.Write(headerText);
                    writer.WriteLine("</TH>");
                }
                writer.WriteLine("</TR>");
                writer.WriteLine();
            }
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine("<TR>");
                for (int k = 0; k < num2; k++)
                {
                    string str = this.m_DataGridView[k, i].FormattedValue.ToString();
                    if (str.Length == 0)
                    {
                        str = "&nbsp";
                    }
                    writer.Write("<TD>");
                    writer.Write(str);
                    writer.WriteLine("</TD>");
                }
                writer.WriteLine("</TR>");
                if (i < (count - 1))
                {
                    writer.WriteLine();
                }
            }
            writer.WriteLine("");
            writer.WriteLine("");
            writer.WriteLine("</TABLE>");
            writer.WriteLine("");
            writer.WriteLine("</BODY>");
            writer.WriteLine("");
            writer.WriteLine("</HTML>");
            writer.Close();
            if (this.m_OpenAfterExport)
            {
                Process.Start(FilePath);
            }
            if (this.ExportCompleted != null)
            {
                this.ExportCompleted(this, new EventArgs());
            }
        }

        private void ToXml(string FilePath)
        {
            int count = this.m_DataGridView.Rows.Count;
            if (this.m_DataGridView.AllowUserToAddRows)
            {
                count--;
            }
            StreamWriter writer = new StreamWriter(FilePath);
            writer.WriteLine(string.Concat(new object[] { "<?xml version=", '"', "1.0", '"', "?>" }));
            writer.WriteLine("");
            writer.WriteLine("<DATA>");
            writer.WriteLine();
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine("<ROW>");
                for (int j = 0; j < this.m_DataGridView.Columns.Count; j++)
                {
                    string str2 = this.m_DataGridView.Columns[j].HeaderText.Replace(" ", "_");
                    string str = this.m_DataGridView[j, i].FormattedValue.ToString();
                    writer.Write("<" + str2 + ">");
                    writer.Write(str);
                    writer.WriteLine("</" + str2 + ">");
                }
                writer.WriteLine("</ROW>");
                if (i < (count - 1))
                {
                    writer.WriteLine();
                }
            }
            writer.WriteLine();
            writer.WriteLine("</DATA>");
            writer.Close();
            if (this.m_OpenAfterExport)
            {
                Process.Start(FilePath);
            }
            if (this.ExportCompleted != null)
            {
                this.ExportCompleted(this, new EventArgs());
            }
        }

        [Description("The data grid view which the control will export"), DefaultValue((string) null), Category("Data")]
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

        [Category("Behavior"), DefaultValue(false), Description("Specifies whether column names will be included in the export file. Applies to csv and html only.")]
        public bool IncludeColumnNames
        {
            get
            {
                return this.m_IncludeColumnNames;
            }
            set
            {
                this.m_IncludeColumnNames = value;
            }
        }

        [DefaultValue(true), Description("Specifies whether the export file will be opened after the export is completed."), Category("Behavior")]
        public bool OpenAfterExport
        {
            get
            {
                return this.m_OpenAfterExport;
            }
            set
            {
                this.m_OpenAfterExport = value;
            }
        }
    }
}

