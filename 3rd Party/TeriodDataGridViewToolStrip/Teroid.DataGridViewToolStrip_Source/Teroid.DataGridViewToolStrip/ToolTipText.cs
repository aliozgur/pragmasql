namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(ToolTipTextConverter))]
    public class ToolTipText
    {
        private string m_Copy = "Copy";
        private string m_Cut = "Cut";
        private string m_Delete = "Delete";
        private string m_ExportToCsv = "Export to csv";
        private string m_ExportToHtml = "Export to html";
        private string m_ExportToXml = "Export to xml";
        private string m_Filter = "Filter";
        private string m_FilterBySelection = "Filter by selection";
        private string m_First = "First";
        private string m_Last = "Last";
        private string m_New = "New";
        private string m_Next = "Next";
        private string m_PageSetup = "Page setup";
        private string m_Paste = "Paste";
        private string m_Previous = "Previous";
        private string m_Print = "Print";
        private string m_PrintPreview = "Print preview";
        private string m_Redo = "Redo";
        private string m_RemoveFilter = "Remove filter";
        private string m_Replace = "Replace";
        private string m_Save = "Save";
        private string m_Search = "Search";
        private string m_Sort = "Sort";
        private string m_Undo = "Undo";

        internal event EventHandler ToolTipTextChanged;

        [DefaultValue("Copy")]
        public string Copy
        {
            get
            {
                return this.m_Copy;
            }
            set
            {
                this.m_Copy = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Cut")]
        public string Cut
        {
            get
            {
                return this.m_Cut;
            }
            set
            {
                this.m_Cut = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Delete")]
        public string Delete
        {
            get
            {
                return this.m_Delete;
            }
            set
            {
                this.m_Delete = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Export to csv")]
        public string ExportToCsv
        {
            get
            {
                return this.m_ExportToCsv;
            }
            set
            {
                this.m_ExportToCsv = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Export to html")]
        public string ExportToHtml
        {
            get
            {
                return this.m_ExportToHtml;
            }
            set
            {
                this.m_ExportToHtml = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Export to xml")]
        public string ExportToXml
        {
            get
            {
                return this.m_ExportToXml;
            }
            set
            {
                this.m_ExportToXml = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Filter")]
        public string Filter
        {
            get
            {
                return this.m_Filter;
            }
            set
            {
                this.m_Filter = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Filter by selection")]
        public string FilterBySelection
        {
            get
            {
                return this.m_FilterBySelection;
            }
            set
            {
                this.m_FilterBySelection = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("First")]
        public string First
        {
            get
            {
                return this.m_First;
            }
            set
            {
                this.m_First = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Last")]
        public string Last
        {
            get
            {
                return this.m_Last;
            }
            set
            {
                this.m_Last = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("New")]
        public string New
        {
            get
            {
                return this.m_New;
            }
            set
            {
                this.m_New = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Next")]
        public string Next
        {
            get
            {
                return this.m_Next;
            }
            set
            {
                this.m_Next = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Page setup")]
        public string PageSetup
        {
            get
            {
                return this.m_PageSetup;
            }
            set
            {
                this.m_PageSetup = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Paste")]
        public string Paste
        {
            get
            {
                return this.m_Paste;
            }
            set
            {
                this.m_Paste = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Previous")]
        public string Previous
        {
            get
            {
                return this.m_Previous;
            }
            set
            {
                this.m_Previous = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Print")]
        public string Print
        {
            get
            {
                return this.m_Print;
            }
            set
            {
                this.m_Print = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Print preview")]
        public string PrintPreview
        {
            get
            {
                return this.m_PrintPreview;
            }
            set
            {
                this.m_PrintPreview = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Redo")]
        public string Redo
        {
            get
            {
                return this.m_Redo;
            }
            set
            {
                this.m_Redo = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Remove filter")]
        public string RemoveFilter
        {
            get
            {
                return this.m_RemoveFilter;
            }
            set
            {
                this.m_RemoveFilter = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Replace")]
        public string Replace
        {
            get
            {
                return this.m_Replace;
            }
            set
            {
                this.m_Replace = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Save")]
        public string Save
        {
            get
            {
                return this.m_Save;
            }
            set
            {
                this.m_Save = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Search")]
        public string Search
        {
            get
            {
                return this.m_Search;
            }
            set
            {
                this.m_Search = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Sort")]
        public string Sort
        {
            get
            {
                return this.m_Sort;
            }
            set
            {
                this.m_Sort = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue("Undo")]
        public string Undo
        {
            get
            {
                return this.m_Undo;
            }
            set
            {
                this.m_Undo = value;
                if (this.ToolTipTextChanged != null)
                {
                    this.ToolTipTextChanged(this, new EventArgs());
                }
            }
        }
    }
}

