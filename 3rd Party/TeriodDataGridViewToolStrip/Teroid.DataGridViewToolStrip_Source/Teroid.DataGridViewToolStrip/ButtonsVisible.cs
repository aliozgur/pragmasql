namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [TypeConverter(typeof(ButtonsVisibleConverter))]
    public class ButtonsVisible
    {
        private bool m_CutCopyPaste = true;
        private bool m_Delete = true;
        private bool m_ExportToCsv = true;
        private bool m_ExportToHtml = true;
        private bool m_ExportToXml = true;
        private bool m_Filter = true;
        private bool m_Navigation = true;
        private bool m_New = true;
        private bool m_Printing = true;
        private bool m_RecordNumbers = true;
        private bool m_Replace = true;
        private bool m_Save = true;
        private bool m_Search = true;
        private bool m_Sort = true;
        private bool m_UndoAndRedo = true;

        internal event EventHandler ButtonsVisibleChanged;

        [DefaultValue(true)]
        public bool CutCopyPaste
        {
            get
            {
                return this.m_CutCopyPaste;
            }
            set
            {
                this.m_CutCopyPaste = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Delete
        {
            get
            {
                return this.m_Delete;
            }
            set
            {
                this.m_Delete = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool ExportToCsv
        {
            get
            {
                return this.m_ExportToCsv;
            }
            set
            {
                this.m_ExportToCsv = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool ExportToHtml
        {
            get
            {
                return this.m_ExportToHtml;
            }
            set
            {
                this.m_ExportToHtml = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool ExportToXml
        {
            get
            {
                return this.m_ExportToXml;
            }
            set
            {
                this.m_ExportToXml = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Filter
        {
            get
            {
                return this.m_Filter;
            }
            set
            {
                this.m_Filter = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Navigation
        {
            get
            {
                return this.m_Navigation;
            }
            set
            {
                this.m_Navigation = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool New
        {
            get
            {
                return this.m_New;
            }
            set
            {
                this.m_New = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Printing
        {
            get
            {
                return this.m_Printing;
            }
            set
            {
                this.m_Printing = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool RecordNumbers
        {
            get
            {
                return this.m_RecordNumbers;
            }
            set
            {
                this.m_RecordNumbers = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Replace
        {
            get
            {
                return this.m_Replace;
            }
            set
            {
                this.m_Replace = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Save
        {
            get
            {
                return this.m_Save;
            }
            set
            {
                this.m_Save = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Search
        {
            get
            {
                return this.m_Search;
            }
            set
            {
                this.m_Search = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool Sort
        {
            get
            {
                return this.m_Sort;
            }
            set
            {
                this.m_Sort = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }

        [DefaultValue(true)]
        public bool UndoAndRedo
        {
            get
            {
                return this.m_UndoAndRedo;
            }
            set
            {
                this.m_UndoAndRedo = value;
                if (this.ButtonsVisibleChanged != null)
                {
                    this.ButtonsVisibleChanged(this, new EventArgs());
                }
            }
        }
    }
}

