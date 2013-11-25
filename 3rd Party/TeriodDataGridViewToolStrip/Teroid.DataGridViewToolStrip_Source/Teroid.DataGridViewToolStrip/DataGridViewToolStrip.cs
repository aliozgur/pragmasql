namespace Teroid.DataGridViewToolStrip
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Teroid.DataGridViewToolStrip.Properties;

    [ToolboxBitmap(typeof(Teroid.DataGridViewToolStrip.DataGridViewToolStrip))]
    public sealed class DataGridViewToolStrip : ToolStrip
    {
        private int CurrentColumnIndex;
        private int CurrentRowIndex;
        private object CurrentValue;
        private DataGridViewExportControl DGVExportCtl;
        private DataGridViewPrintControl DGVPrintCtl;
        private FilterDialog dlgFilter;
        private ReplaceDialog dlgReplace;
        private SearchDialog dlgSearch;
        private SortDialog dlgSort;
        private Licensing Licence;
        private BindingSource m_BindingSource;
        private Teroid.DataGridViewToolStrip.ButtonsVisible m_ButtonsVisible;
        private System.Windows.Forms.DataGridView m_DataGridView;
        private Teroid.DataGridViewToolStrip.ToolTipText m_ToolTipText;
        private bool RecordNumberChangedInternally;
        private bool RecordNumberKeyValid;
        private ArrayList RedoList;
        private ComponentResourceManager resources = new ComponentResourceManager(typeof(Teroid.DataGridViewToolStrip.DataGridViewToolStrip));
        private ToolStripButton tsbCopy;
        private ToolStripButton tsbCut;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbExportToCsv;
        private ToolStripButton tsbExportToHtml;
        private ToolStripButton tsbExportToXml;
        private ToolStripDropDownButton tsbFilter;
        private ToolStripButton tsbFilterBySelection;
        private ToolStripButton tsbFirst;
        private ToolStripButton tsbLast;
        private ToolStripButton tsbNew;
        private ToolStripButton tsbNext;
        private ToolStripButton tsbPageSetup;
        private ToolStripButton tsbPaste;
        private ToolStripButton tsbPrevious;
        private ToolStripButton tsbPrint;
        private ToolStripButton tsbPrintPreview;
        private ToolStripButton tsbRedo;
        private ToolStripButton tsbRemoveFilter;
        private ToolStripDropDownButton tsbReplace;
        private ToolStripButton tsbSave;
        private ToolStripDropDownButton tsbSearch;
        private ToolStripDropDownButton tsbSort;
        private ToolStripButton tsbUndo;
        private ToolStripLabel tslRecordCount;
        private ToolStripSeparator tssCutCopyPaste;
        private ToolStripSeparator tssFilter;
        private ToolStripSeparator tssNavigation;
        private ToolStripSeparator tssNewAndDelete;
        private ToolStripSeparator tssPrinting;
        private ToolStripSeparator tssSearchAndReplace;
        private ToolStripSeparator tssSort;
        private ToolStripSeparator tssUndoAndRedo;
        private ToolStripTextBox tstbRecordNumber;
        private ArrayList UndoList;
        private bool UndoOrRedoAction;
        private bool Unsaved;

        public event EventHandler SaveButtonClicked;

        public DataGridViewToolStrip()
        {
            base.GripStyle = ToolStripGripStyle.Hidden;
            this.m_ButtonsVisible = new Teroid.DataGridViewToolStrip.ButtonsVisible();
            this.m_ButtonsVisible.ButtonsVisibleChanged += new EventHandler(this.OnButtonsVisibleChanged);
            this.m_ToolTipText = new Teroid.DataGridViewToolStrip.ToolTipText();
            this.m_ToolTipText.ToolTipTextChanged += new EventHandler(this.OnToolTipTextChanged);
            this.dlgFilter = new FilterDialog();
            this.dlgSort = new SortDialog();
            this.dlgSort.LostFocus += new EventHandler(this.dlgSort_LostFocus);
            this.dlgSort.SortOrderSet += new EventHandler(this.dlgSort_SortOrderSet);
            this.dlgSearch = new SearchDialog();
            this.dlgReplace = new ReplaceDialog();
            this.CreateButtons();
            this.DGVPrintCtl = new DataGridViewPrintControl();
            this.DGVExportCtl = new DataGridViewExportControl();
            this.UndoList = new ArrayList();
            this.RedoList = new ArrayList();
            if (this.Licence == null)
            {
                this.Licence = new Licensing(0x11, "DataGridViewToolStrip", base.DesignMode);
            }
        }

        private void AllowUserToAddRowsChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabledness();
        }

        private void AllowUserToDeleteRowsChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabledness();
        }

        private void CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.UndoOrRedoAction)
            {
                UserAction action = new UserAction(UserAction.UserActionTypes.ValueChanged, e.ColumnIndex, e.RowIndex, this.CurrentValue, this.m_DataGridView.CurrentCell.Value);
                this.UndoList.Insert(0, action);
                this.Unsaved = true;
                this.SetButtonEnabledness();
            }
        }

        private void Copy()
        {
            Clipboard.SetText(this.m_DataGridView.CurrentCell.Value.ToString());
            this.SetButtonEnabledness();
        }

        private void CreateButtons()
        {
            this.tsbFirst = new ToolStripButton();
            this.tsbFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbFirst.Image = Resources.first;
            this.tsbFirst.ImageTransparentColor = Color.Magenta;
            this.tsbFirst.Name = "tsbFirst";
            this.tsbFirst.Size = new Size(0x17, 0x16);
            this.tsbFirst.Text = "First";
            this.tsbFirst.Click += new EventHandler(this.OnFirstClick);
            this.tsbPrevious = new ToolStripButton();
            this.tsbPrevious.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbPrevious.Image = Resources.previous;
            this.tsbPrevious.ImageTransparentColor = Color.Magenta;
            this.tsbPrevious.Name = "tsbPrevious";
            this.tsbPrevious.Size = new Size(0x17, 0x16);
            this.tsbPrevious.Text = "Previous";
            this.tsbPrevious.Click += new EventHandler(this.OnPreviousClick);
            this.tstbRecordNumber = new ToolStripTextBox();
            this.tstbRecordNumber.BorderStyle = BorderStyle.Fixed3D;
            this.tstbRecordNumber.Size = new Size(40, this.tstbRecordNumber.Size.Height);
            this.tstbRecordNumber.AutoSize = true;
            this.tstbRecordNumber.Text = "0";
            this.tstbRecordNumber.KeyDown += new KeyEventHandler(this.tstbRecordNumber_KeyDown);
            this.tstbRecordNumber.KeyPress += new KeyPressEventHandler(this.tstbRecordNumber_KeyPress);
            this.tslRecordCount = new ToolStripLabel();
            this.tslRecordCount.Size = new Size(40, this.tslRecordCount.Size.Height);
            this.tslRecordCount.Text = "of 0";
            this.tsbNext = new ToolStripButton();
            this.tsbNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbNext.Image = Resources.next;
            this.tsbNext.ImageTransparentColor = Color.Magenta;
            this.tsbNext.Name = "tsbNext";
            this.tsbNext.Size = new Size(0x17, 0x16);
            this.tsbNext.Text = "Next";
            this.tsbNext.Click += new EventHandler(this.OnNextClick);
            this.tsbLast = new ToolStripButton();
            this.tsbLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbLast.Image = Resources.last;
            this.tsbLast.ImageTransparentColor = Color.Magenta;
            this.tsbLast.Name = "tsbLast";
            this.tsbLast.Size = new Size(0x17, 0x16);
            this.tsbLast.Text = "Last";
            this.tsbLast.Click += new EventHandler(this.OnLastClick);
            this.tssNavigation = new ToolStripSeparator();
            this.tsbNew = new ToolStripButton();
            this.tsbNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = Resources._new;
            this.tsbNew.ImageTransparentColor = Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new Size(0x17, 0x16);
            this.tsbNew.Text = "New";
            this.tsbNew.Click += new EventHandler(this.OnNewClick);
            this.tsbDelete = new ToolStripButton();
            this.tsbDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = Resources.delete;
            this.tsbDelete.ImageTransparentColor = Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new Size(0x17, 0x16);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.Click += new EventHandler(this.OnDeleteClick);
            this.tsbSave = new ToolStripButton();
            this.tsbSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = Resources.save;
            this.tsbSave.ImageTransparentColor = Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new Size(0x17, 0x16);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new EventHandler(this.OnSaveClick);
            this.tssNewAndDelete = new ToolStripSeparator();
            this.tsbFilter = new ToolStripDropDownButton();
            this.tsbFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbFilter.Image = Resources.filter;
            this.tsbFilter.ImageTransparentColor = Color.Magenta;
            this.tsbFilter.Name = "tsbFilter";
            this.tsbFilter.Size = new Size(0x17, 0x16);
            this.tsbFilter.Text = "Filter";
            this.tsbFilter.Click += new EventHandler(this.OnFilterClick);
            this.tsbFilterBySelection = new ToolStripButton();
            this.tsbFilterBySelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbFilterBySelection.Image = Resources.filterbyselection;
            this.tsbFilterBySelection.ImageTransparentColor = Color.Magenta;
            this.tsbFilterBySelection.Name = "tsbFilterBySelection";
            this.tsbFilterBySelection.Size = new Size(0x17, 0x16);
            this.tsbFilterBySelection.Text = "Filter by selection";
            this.tsbFilterBySelection.Click += new EventHandler(this.OnFilterBySelectionClick);
            this.tsbRemoveFilter = new ToolStripButton();
            this.tsbRemoveFilter.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbRemoveFilter.Image = Resources.removefilter;
            this.tsbRemoveFilter.ImageTransparentColor = Color.Magenta;
            this.tsbRemoveFilter.Name = "tsbRemoveFilter";
            this.tsbRemoveFilter.Size = new Size(0x17, 0x16);
            this.tsbRemoveFilter.Text = "Remove filter";
            this.tsbRemoveFilter.Click += new EventHandler(this.OnRemoveFilterClick);
            this.tssFilter = new ToolStripSeparator();
            this.tsbSort = new ToolStripDropDownButton();
            this.tsbSort.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbSort.Image = Resources.sort;
            this.tsbSort.ImageTransparentColor = Color.Magenta;
            this.tsbSort.Name = "tsbSort";
            this.tsbSort.Size = new Size(0x17, 0x16);
            this.tsbSort.Text = "Sort";
            this.tsbSort.Click += new EventHandler(this.OnSortClick);
            this.tssSort = new ToolStripSeparator();
            this.tsbSearch = new ToolStripDropDownButton();
            this.tsbSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbSearch.Image = Resources.search;
            this.tsbSearch.ImageTransparentColor = Color.Magenta;
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.Size = new Size(0x17, 0x16);
            this.tsbSearch.Text = "Search";
            this.tsbSearch.Click += new EventHandler(this.OnSearchClick);
            this.tsbReplace = new ToolStripDropDownButton();
            this.tsbReplace.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbReplace.Image = Resources.replace;
            this.tsbReplace.ImageTransparentColor = Color.Magenta;
            this.tsbReplace.Name = "tsbReplace";
            this.tsbReplace.Size = new Size(0x17, 0x16);
            this.tsbReplace.Text = "Replace";
            this.tsbReplace.Click += new EventHandler(this.OnReplaceClick);
            this.tssSearchAndReplace = new ToolStripSeparator();
            this.tsbUndo = new ToolStripButton();
            this.tsbUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Image = Resources.undo;
            this.tsbUndo.ImageTransparentColor = Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new Size(0x17, 0x16);
            this.tsbUndo.Text = "Undo";
            this.tsbUndo.Click += new EventHandler(this.OnUndoClick);
            this.tsbRedo = new ToolStripButton();
            this.tsbRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = Resources.redo;
            this.tsbRedo.ImageTransparentColor = Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new Size(0x17, 0x16);
            this.tsbRedo.Text = "Redo";
            this.tsbRedo.Click += new EventHandler(this.OnRedoClick);
            this.tssUndoAndRedo = new ToolStripSeparator();
            this.tsbCut = new ToolStripButton();
            this.tsbCut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbCut.Image = Resources.cut;
            this.tsbCut.ImageTransparentColor = Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new Size(0x17, 0x16);
            this.tsbCut.Text = "Cut";
            this.tsbCut.Click += new EventHandler(this.OnCutClick);
            this.tsbCopy = new ToolStripButton();
            this.tsbCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Image = Resources.copy;
            this.tsbCopy.ImageTransparentColor = Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new Size(0x17, 0x16);
            this.tsbCopy.Text = "Copy";
            this.tsbCopy.Click += new EventHandler(this.OnCopyClick);
            this.tsbPaste = new ToolStripButton();
            this.tsbPaste.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Image = Resources.paste;
            this.tsbPaste.ImageTransparentColor = Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new Size(0x17, 0x16);
            this.tsbPaste.Text = "Paste";
            this.tsbPaste.Click += new EventHandler(this.OnPasteClick);
            this.tssCutCopyPaste = new ToolStripSeparator();
            this.tsbPageSetup = new ToolStripButton();
            this.tsbPageSetup.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbPageSetup.Image = Resources.pagesetup;
            this.tsbPageSetup.ImageTransparentColor = Color.Magenta;
            this.tsbPageSetup.Name = "tsbPageSetup";
            this.tsbPageSetup.Size = new Size(0x17, 0x16);
            this.tsbPageSetup.Text = "Page setup";
            this.tsbPageSetup.Click += new EventHandler(this.OnPageSetupClick);
            this.tsbPrintPreview = new ToolStripButton();
            this.tsbPrintPreview.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbPrintPreview.Image = Resources.printpreview;
            this.tsbPrintPreview.ImageTransparentColor = Color.Magenta;
            this.tsbPrintPreview.Name = "tsbPrintPreview";
            this.tsbPrintPreview.Size = new Size(0x17, 0x16);
            this.tsbPrintPreview.Text = "Print preview";
            this.tsbPrintPreview.Click += new EventHandler(this.OnPrintPreviewClick);
            this.tsbPrint = new ToolStripButton();
            this.tsbPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = Resources.print;
            this.tsbPrint.ImageTransparentColor = Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new Size(0x17, 0x16);
            this.tsbPrint.Text = "Print";
            this.tsbPrint.Click += new EventHandler(this.OnPrintClick);
            this.tssPrinting = new ToolStripSeparator();
            this.tsbExportToCsv = new ToolStripButton();
            this.tsbExportToCsv.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbExportToCsv.Image = Resources.exporttocsv;
            this.tsbExportToCsv.ImageTransparentColor = Color.Magenta;
            this.tsbExportToCsv.Name = "tsbExportToCsv";
            this.tsbExportToCsv.Size = new Size(0x17, 0x16);
            this.tsbExportToCsv.Text = "Export to csv";
            this.tsbExportToCsv.Click += new EventHandler(this.OnExportToCsvClick);
            this.tsbExportToHtml = new ToolStripButton();
            this.tsbExportToHtml.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbExportToHtml.Image = Resources.exporttohtml;
            this.tsbExportToHtml.ImageTransparentColor = Color.Magenta;
            this.tsbExportToHtml.Name = "tsbExportToHtml";
            this.tsbExportToHtml.Size = new Size(0x17, 0x16);
            this.tsbExportToHtml.Text = "Export to html";
            this.tsbExportToHtml.Click += new EventHandler(this.OnExportToHtmlClick);
            this.tsbExportToXml = new ToolStripButton();
            this.tsbExportToXml.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tsbExportToXml.Image = Resources.exporttoxml;
            this.tsbExportToXml.ImageTransparentColor = Color.Magenta;
            this.tsbExportToXml.Name = "tsbExportToXml";
            this.tsbExportToXml.Size = new Size(0x17, 0x16);
            this.tsbExportToXml.Text = "Export to xml";
            this.tsbExportToXml.Click += new EventHandler(this.OnExportToXmlClick);
            this.Items.AddRange(new ToolStripItem[] { 
                this.tsbFirst, this.tsbPrevious, this.tstbRecordNumber, this.tslRecordCount, this.tsbNext, this.tsbLast, this.tssNavigation, this.tsbNew, this.tsbDelete, this.tsbSave, this.tssNewAndDelete, this.tsbCut, this.tsbCopy, this.tsbPaste, this.tssCutCopyPaste, this.tsbFilter, 
                this.tsbFilterBySelection, this.tsbRemoveFilter, this.tssFilter, this.tsbSort, this.tssSort, this.tsbSearch, this.tsbReplace, this.tssSearchAndReplace, this.tsbUndo, this.tsbRedo, this.tssUndoAndRedo, this.tsbPageSetup, this.tsbPrintPreview, this.tsbPrint, this.tssPrinting, this.tsbExportToCsv, 
                this.tsbExportToHtml, this.tsbExportToXml
             });
            this.DisableAllButtons();
        }

        private void CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.m_DataGridView.CurrentCell != null)
            {
                this.CurrentColumnIndex = this.m_DataGridView.CurrentCell.ColumnIndex;
                this.CurrentRowIndex = this.m_DataGridView.CurrentCell.RowIndex;
                this.CurrentValue = this.m_DataGridView.CurrentCell.Value;
            }
            else
            {
                this.CurrentColumnIndex = -1;
                this.CurrentRowIndex = -1;
                this.CurrentValue = null;
            }
            this.SetButtonEnabledness();
        }

        private void Cut()
        {
            Clipboard.SetText(this.m_DataGridView.CurrentCell.Value.ToString());
            this.m_DataGridView.CurrentCell.Value = "";
            this.SetButtonEnabledness();
        }

        private void Delete()
        {
            this.m_BindingSource.RemoveCurrent();
            this.Unsaved = true;
            this.SetButtonEnabledness();
        }

        private void DisableAllButtons()
        {
            foreach (ToolStripItem item in this.Items)
            {
                item.Enabled = false;
            }
        }

        private void dlgSort_LostFocus(object sender, EventArgs e)
        {
            if (this.dlgSort.Visible)
            {
                this.dlgSort.Hide();
            }
        }

        private void dlgSort_SortOrderSet(object sender, EventArgs e)
        {
            this.m_BindingSource.Sort = this.dlgSort.SortOrder;
        }

        private void ExportToCsv()
        {
            this.DGVExportCtl.ExportToCsv();
        }

        private void ExportToHtml()
        {
            this.DGVExportCtl.ExportToHtml();
        }

        private void ExportToXml()
        {
            this.DGVExportCtl.ExportToXml();
        }

        private void Filter()
        {
            if (this.dlgFilter.Visible)
            {
                this.dlgFilter.Hide();
            }
            else
            {
                int left = this.tsbFilter.Bounds.Left;
                int y = this.tsbFilter.Bounds.Top + base.Height;
                Point p = new Point(left, y);
                p = base.PointToScreen(p);
                this.dlgFilter.Left = p.X;
                this.dlgFilter.Top = p.Y;
                this.dlgFilter.Show();
            }
        }

        private void FilterBySelection()
        {
            string str = "'";
            DataView dataView = null;
            DataRowView view2 = (DataRowView) this.m_BindingSource[0];
            dataView = view2.DataView;
            DataTypes notSupported = DataTypes.NotSupported;
            string dataPropertyName = this.m_DataGridView.Columns[this.m_DataGridView.CurrentCell.ColumnIndex].DataPropertyName;
            notSupported = this.GetDataType(dataView.Table.Columns[dataPropertyName].DataType.ToString());
            if (notSupported != DataTypes.NotSupported)
            {
                string str3 = dataPropertyName;
                switch (notSupported)
                {
                    case DataTypes.Integer:
                    case DataTypes.Float:
                        str3 = str3 + " = " + this.m_DataGridView.CurrentCell.Value.ToString();
                        break;

                    case DataTypes.Text:
                    {
                        string str5 = str3;
                        str3 = str5 + " = " + str + this.m_DataGridView.CurrentCell.Value.ToString() + str;
                        break;
                    }
                    case DataTypes.Boolean:
                    {
                        string str4 = this.m_DataGridView.CurrentCell.Value.ToString();
                        if (str4 == "")
                        {
                            str3 = str3 + " Is Null";
                        }
                        else
                        {
                            str3 = str3 + " = " + str4;
                        }
                        break;
                    }
                    case DataTypes.DateTime:
                        str3 = str3 + " = " + this.GetDateAsSQLstring(this.m_DataGridView.CurrentCell.Value.ToString());
                        break;
                }
                this.m_BindingSource.Filter = str3;
            }
        }

        private void First()
        {
            if (this.m_BindingSource != null)
            {
                this.m_BindingSource.MoveFirst();
                this.SetButtonEnabledness();
            }
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

        private void Last()
        {
            if (this.m_BindingSource != null)
            {
                this.m_BindingSource.MoveLast();
                this.SetButtonEnabledness();
            }
        }

        private void m_BindingSource_PositionChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabledness();
        }

        private void m_DataGridView_DataMemberChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabledness();
        }

        private void m_DataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabledness();
        }

        private void m_DataGridView_RowsAdded(object sender, EventArgs e)
        {
            this.Unsaved = true;
            this.SetButtonEnabledness();
        }

        private void m_DataGridView_RowsRemoved(object sender, EventArgs e)
        {
            this.Unsaved = true;
            this.SetButtonEnabledness();
        }

        private void New()
        {
            this.m_DataGridView.CurrentCell = this.m_DataGridView[0, this.m_DataGridView.Rows.Count - 1];
        }

        private void Next()
        {
            if (this.m_BindingSource != null)
            {
                this.m_BindingSource.MoveNext();
                this.SetButtonEnabledness();
            }
        }

        private void OnButtonsVisibleChanged(object sender, EventArgs e)
        {
            this.SetButtonVisibility();
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            this.Copy();
        }

        private void OnCutClick(object sender, EventArgs e)
        {
            this.Cut();
        }

        private void OnDeleteClick(object sender, EventArgs e)
        {
            this.Delete();
        }

        private void OnExportToCsvClick(object sender, EventArgs e)
        {
            this.ExportToCsv();
        }

        private void OnExportToHtmlClick(object sender, EventArgs e)
        {
            this.ExportToHtml();
        }

        private void OnExportToXmlClick(object sender, EventArgs e)
        {
            this.ExportToXml();
        }

        private void OnFilterBySelectionClick(object sender, EventArgs e)
        {
            this.FilterBySelection();
        }

        private void OnFilterClick(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void OnFirstClick(object sender, EventArgs e)
        {
            this.First();
        }

        private void OnLastClick(object sender, EventArgs e)
        {
            this.Last();
        }

        private void OnNewClick(object sender, EventArgs e)
        {
            this.New();
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            this.Next();
        }

        private void OnPageSetupClick(object sender, EventArgs e)
        {
            this.PageSetup();
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            this.Paste();
        }

        private void OnPreviousClick(object sender, EventArgs e)
        {
            this.Previous();
        }

        private void OnPrintClick(object sender, EventArgs e)
        {
            this.Print();
        }

        private void OnPrintPreviewClick(object sender, EventArgs e)
        {
            this.PrintPreview();
        }

        private void OnRedoClick(object sender, EventArgs e)
        {
            this.Redo();
        }

        private void OnRemoveFilterClick(object sender, EventArgs e)
        {
            this.RemoveFilter();
        }

        private void OnReplaceClick(object sender, EventArgs e)
        {
            this.Replace();
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            this.Save();
        }

        private void OnSearchClick(object sender, EventArgs e)
        {
            this.Search();
        }

        private void OnSortClick(object sender, EventArgs e)
        {
            this.Sort();
        }

        private void OnToolTipTextChanged(object sender, EventArgs e)
        {
            this.SetToolTipText();
        }

        private void OnUndoClick(object sender, EventArgs e)
        {
            this.Undo();
        }

        private void PageSetup()
        {
            this.DGVPrintCtl.PageSetup();
        }

        private void Paste()
        {
            this.m_DataGridView.CurrentCell.Value = Clipboard.GetText();
            Clipboard.Clear();
            this.SetButtonEnabledness();
        }

        private void Previous()
        {
            if (this.m_BindingSource != null)
            {
                this.m_BindingSource.MovePrevious();
                this.SetButtonEnabledness();
            }
        }

        private void Print()
        {
            this.DGVPrintCtl.Print();
        }

        private void PrintPreview()
        {
            this.DGVPrintCtl.PrintPreview();
        }

        private void ReadOnlyChanged(object sender, EventArgs e)
        {
            this.SetButtonEnabledness();
        }

        private void Redo()
        {
            UserAction action = (UserAction) this.RedoList[0];
            this.m_DataGridView.CurrentCell = this.m_DataGridView[action.ColumnIndex, action.RowIndex];
            this.UndoOrRedoAction = true;
            this.m_DataGridView.CurrentCell.Value = action.OldValue;
            this.UndoOrRedoAction = false;
            UserAction action2 = new UserAction(action.UserActionType, action.ColumnIndex, action.RowIndex, action.NewValue, action.OldValue);
            this.UndoList.Insert(0, action2);
            this.RedoList.RemoveAt(0);
            this.SetButtonEnabledness();
        }

        private void RemoveFilter()
        {
            this.m_BindingSource.Filter = "";
        }

        private void Replace()
        {
            if (this.dlgReplace.Visible)
            {
                this.dlgReplace.Hide();
                this.dlgReplace.Reset();
            }
            else
            {
                int left = this.tsbReplace.Bounds.Left;
                int y = this.tsbReplace.Bounds.Top + base.Height;
                Point p = new Point(left, y);
                p = base.PointToScreen(p);
                this.dlgReplace.Left = p.X;
                this.dlgReplace.Top = p.Y;
                this.dlgReplace.Show();
            }
        }

        private void Save()
        {
            if (this.SaveButtonClicked != null)
            {
                this.SaveButtonClicked(this, new EventArgs());
            }
            this.Unsaved = false;
            this.SetButtonEnabledness();
        }

        private void Search()
        {
            if (this.dlgSearch.Visible)
            {
                this.dlgSearch.Hide();
                this.dlgSearch.Reset();
            }
            else
            {
                int left = this.tsbSearch.Bounds.Left;
                int y = this.tsbSearch.Bounds.Top + base.Height;
                Point p = new Point(left, y);
                p = base.PointToScreen(p);
                this.dlgSearch.Left = p.X;
                this.dlgSearch.Top = p.Y;
                this.dlgSearch.Show();
            }
        }

        private void SetButtonEnabledness()
        {
            int position = this.m_BindingSource.Position;
            if (position < 0)
            {
                position = 0;
            }
            else
            {
                position++;
            }
            this.RecordNumberChangedInternally = true;
            this.tstbRecordNumber.Text = position.ToString();
            this.RecordNumberChangedInternally = false;
            this.tslRecordCount.Text = "of " + this.m_BindingSource.Count.ToString();
            if (this.m_BindingSource.Position == 0)
            {
                this.tsbFirst.Enabled = false;
                this.tsbPrevious.Enabled = false;
            }
            else
            {
                this.tsbFirst.Enabled = true;
                this.tsbPrevious.Enabled = true;
            }
            if (this.m_BindingSource.Position == (this.m_BindingSource.Count - 1))
            {
                this.tsbNext.Enabled = false;
                this.tsbLast.Enabled = false;
            }
            else
            {
                this.tsbNext.Enabled = true;
                this.tsbLast.Enabled = true;
            }
            if (this.m_DataGridView.AllowUserToAddRows && this.m_BindingSource.AllowNew)
            {
                this.tsbNew.Enabled = true;
            }
            else
            {
                this.tsbNew.Enabled = false;
            }
            if ((this.m_DataGridView.AllowUserToDeleteRows && this.m_BindingSource.AllowRemove) && (this.m_DataGridView.CurrentCell != null))
            {
                this.tsbDelete.Enabled = true;
            }
            else
            {
                this.tsbDelete.Enabled = false;
            }
            if (this.Unsaved)
            {
                this.tsbSave.Enabled = true;
            }
            else
            {
                this.tsbSave.Enabled = false;
            }
            this.tstbRecordNumber.Enabled = true;
            this.tslRecordCount.Enabled = true;
            if ((!this.m_DataGridView.ReadOnly && this.m_BindingSource.AllowEdit) && (this.m_DataGridView.CurrentCell != null))
            {
                this.tsbCut.Enabled = true;
            }
            else
            {
                this.tsbCut.Enabled = false;
            }
            if ((!this.m_DataGridView.ReadOnly && this.m_BindingSource.AllowEdit) && (this.m_DataGridView.CurrentCell != null))
            {
                this.tsbCopy.Enabled = true;
            }
            else
            {
                this.tsbCopy.Enabled = false;
            }
            if ((!this.m_DataGridView.ReadOnly && this.m_BindingSource.AllowEdit) && ((this.m_DataGridView.CurrentCell != null) && Clipboard.ContainsText()))
            {
                this.tsbPaste.Enabled = true;
            }
            else
            {
                this.tsbPaste.Enabled = false;
            }
            this.tsbFilter.Enabled = true;
            if (this.m_DataGridView.CurrentCell != null)
            {
                this.tsbFilterBySelection.Enabled = true;
            }
            else
            {
                this.tsbFilterBySelection.Enabled = false;
            }
            if (this.m_BindingSource.Filter != "")
            {
                this.tsbRemoveFilter.Enabled = true;
            }
            else
            {
                this.tsbRemoveFilter.Enabled = false;
            }
            this.tsbSort.Enabled = true;
            this.tsbSearch.Enabled = true;
            if (!this.m_DataGridView.ReadOnly && this.m_BindingSource.AllowEdit)
            {
                this.tsbReplace.Enabled = true;
            }
            else
            {
                this.tsbReplace.Enabled = false;
            }
            if (this.UndoList.Count > 0)
            {
                this.tsbUndo.Enabled = true;
            }
            else
            {
                this.tsbUndo.Enabled = false;
            }
            if (this.RedoList.Count > 0)
            {
                this.tsbRedo.Enabled = true;
            }
            else
            {
                this.tsbRedo.Enabled = false;
            }
            this.tsbPageSetup.Enabled = true;
            this.tsbPrintPreview.Enabled = true;
            this.tsbPrint.Enabled = true;
            this.tsbExportToCsv.Enabled = true;
            this.tsbExportToHtml.Enabled = true;
            this.tsbExportToXml.Enabled = true;
        }

        private void SetButtonVisibility()
        {
            this.tsbFirst.Visible = this.m_ButtonsVisible.Navigation;
            this.tsbPrevious.Visible = this.m_ButtonsVisible.Navigation;
            this.tsbNext.Visible = this.m_ButtonsVisible.Navigation;
            this.tsbLast.Visible = this.m_ButtonsVisible.Navigation;
            this.tslRecordCount.Visible = this.m_ButtonsVisible.RecordNumbers;
            if (this.m_ButtonsVisible.Navigation || this.m_ButtonsVisible.RecordNumbers)
            {
                this.tssNavigation.Visible = true;
            }
            else
            {
                this.tssNavigation.Visible = false;
            }
            this.tsbNew.Visible = this.m_ButtonsVisible.New;
            this.tsbDelete.Visible = this.m_ButtonsVisible.Delete;
            this.tsbSave.Visible = this.m_ButtonsVisible.Save;
            if (this.m_ButtonsVisible.New || this.m_ButtonsVisible.Delete)
            {
                this.tssNewAndDelete.Visible = true;
            }
            else
            {
                this.tssNewAndDelete.Visible = false;
            }
            this.tsbFilter.Visible = this.m_ButtonsVisible.Filter;
            this.tsbFilterBySelection.Visible = this.m_ButtonsVisible.Filter;
            this.tsbRemoveFilter.Visible = this.m_ButtonsVisible.Filter;
            this.tssFilter.Visible = this.m_ButtonsVisible.Filter;
            this.tsbSort.Visible = this.m_ButtonsVisible.Sort;
            this.tssSort.Visible = this.m_ButtonsVisible.Sort;
            this.tsbSearch.Visible = this.m_ButtonsVisible.Search;
            this.tsbReplace.Visible = this.m_ButtonsVisible.Replace;
            if (this.m_ButtonsVisible.Search || this.m_ButtonsVisible.Replace)
            {
                this.tssSearchAndReplace.Visible = true;
            }
            else
            {
                this.tssSearchAndReplace.Visible = false;
            }
            this.tsbUndo.Visible = this.m_ButtonsVisible.UndoAndRedo;
            this.tsbRedo.Visible = this.m_ButtonsVisible.UndoAndRedo;
            this.tssUndoAndRedo.Visible = this.m_ButtonsVisible.UndoAndRedo;
            this.tsbCut.Visible = this.m_ButtonsVisible.CutCopyPaste;
            this.tsbCopy.Visible = this.m_ButtonsVisible.CutCopyPaste;
            this.tsbPaste.Visible = this.m_ButtonsVisible.CutCopyPaste;
            this.tssCutCopyPaste.Visible = this.m_ButtonsVisible.CutCopyPaste;
            this.tsbPageSetup.Visible = this.m_ButtonsVisible.Printing;
            this.tsbPrintPreview.Visible = this.m_ButtonsVisible.Printing;
            this.tsbPrint.Visible = this.m_ButtonsVisible.Printing;
            this.tssPrinting.Visible = this.m_ButtonsVisible.Printing;
            this.tsbExportToCsv.Visible = this.m_ButtonsVisible.ExportToCsv;
            this.tsbExportToHtml.Visible = this.m_ButtonsVisible.ExportToHtml;
            this.tsbExportToXml.Visible = this.m_ButtonsVisible.ExportToXml;
        }

        private void SetLicensedFunctionality()
        {
            MessageBox.Show("SetLicensedFunctionality");
            if (!this.Licence.Licensed)
            {
                this.ShowPurchaseMessage();
            }
        }

        private void SetToolTipText()
        {
            this.tsbFirst.ToolTipText = this.m_ToolTipText.First;
            this.tsbFirst.Text = this.m_ToolTipText.First;
            this.tsbPrevious.ToolTipText = this.m_ToolTipText.Previous;
            this.tsbPrevious.Text = this.m_ToolTipText.Previous;
            this.tsbNext.ToolTipText = this.m_ToolTipText.Next;
            this.tsbNext.Text = this.m_ToolTipText.Next;
            this.tsbLast.ToolTipText = this.m_ToolTipText.Last;
            this.tsbLast.Text = this.m_ToolTipText.Last;
            this.tsbNew.ToolTipText = this.m_ToolTipText.New;
            this.tsbNew.Text = this.m_ToolTipText.New;
            this.tsbDelete.ToolTipText = this.m_ToolTipText.Delete;
            this.tsbDelete.Text = this.m_ToolTipText.Delete;
            this.tsbSave.ToolTipText = this.m_ToolTipText.Save;
            this.tsbSave.Text = this.m_ToolTipText.Save;
            this.tsbFilter.ToolTipText = this.m_ToolTipText.Filter;
            this.tsbFilter.Text = this.m_ToolTipText.Filter;
            this.tsbFilterBySelection.ToolTipText = this.m_ToolTipText.FilterBySelection;
            this.tsbFilterBySelection.Text = this.m_ToolTipText.FilterBySelection;
            this.tsbRemoveFilter.ToolTipText = this.m_ToolTipText.RemoveFilter;
            this.tsbRemoveFilter.Text = this.m_ToolTipText.RemoveFilter;
            this.tsbSort.ToolTipText = this.m_ToolTipText.Sort;
            this.tsbSort.Text = this.m_ToolTipText.Sort;
            this.tsbSearch.ToolTipText = this.m_ToolTipText.Search;
            this.tsbSearch.Text = this.m_ToolTipText.Search;
            this.tsbReplace.ToolTipText = this.m_ToolTipText.Replace;
            this.tsbReplace.Text = this.m_ToolTipText.Replace;
            this.tsbUndo.ToolTipText = this.m_ToolTipText.Undo;
            this.tsbUndo.Text = this.m_ToolTipText.Undo;
            this.tsbRedo.ToolTipText = this.m_ToolTipText.Redo;
            this.tsbRedo.Text = this.m_ToolTipText.Redo;
            this.tsbCut.ToolTipText = this.m_ToolTipText.Cut;
            this.tsbCut.Text = this.m_ToolTipText.Cut;
            this.tsbCopy.ToolTipText = this.m_ToolTipText.Copy;
            this.tsbCopy.Text = this.m_ToolTipText.Copy;
            this.tsbPaste.ToolTipText = this.m_ToolTipText.Paste;
            this.tsbPaste.Text = this.m_ToolTipText.Paste;
            this.tsbPageSetup.ToolTipText = this.m_ToolTipText.PageSetup;
            this.tsbPageSetup.Text = this.m_ToolTipText.PageSetup;
            this.tsbPrintPreview.ToolTipText = this.m_ToolTipText.PrintPreview;
            this.tsbPrintPreview.Text = this.m_ToolTipText.PrintPreview;
            this.tsbPrint.ToolTipText = this.m_ToolTipText.Print;
            this.tsbPrint.Text = this.m_ToolTipText.Print;
            this.tsbExportToCsv.ToolTipText = this.m_ToolTipText.ExportToCsv;
            this.tsbExportToCsv.Text = this.m_ToolTipText.ExportToCsv;
            this.tsbExportToHtml.ToolTipText = this.m_ToolTipText.ExportToHtml;
            this.tsbExportToHtml.Text = this.m_ToolTipText.ExportToHtml;
            this.tsbExportToXml.ToolTipText = this.m_ToolTipText.ExportToXml;
            this.tsbExportToXml.Text = this.m_ToolTipText.ExportToXml;
        }

        private void ShowPurchaseMessage()
        {
            MessageBox.Show("Data Grid View Toolstrip - Freeware from Teroid Software\n\nwww.teroid.com", "Teroid Data Grid View Toolstrip", MessageBoxButtons.OK);
        }

        private void Sort()
        {
            if (this.dlgSort.Visible)
            {
                this.dlgSort.Hide();
            }
            else
            {
                int left = this.tsbSort.Bounds.Left;
                int y = this.tsbSort.Bounds.Top + base.Height;
                Point p = new Point(left, y);
                p = base.PointToScreen(p);
                this.dlgSort.Left = p.X;
                this.dlgSort.Top = p.Y;
                this.dlgSort.Show();
            }
        }

        private void tstbRecordNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9))
            {
                this.RecordNumberKeyValid = true;
            }
            else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9))
            {
                this.RecordNumberKeyValid = true;
            }
            else if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Return))
            {
                this.RecordNumberKeyValid = true;
            }
            else
            {
                this.RecordNumberKeyValid = false;
            }
            if ((e.KeyCode == Keys.Return) && (this.tstbRecordNumber.Text != ""))
            {
                int num = Convert.ToInt32(this.tstbRecordNumber.Text) - 1;
                this.m_BindingSource.Position = num;
            }
        }

        private void tstbRecordNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!this.RecordNumberKeyValid)
            {
                e.Handled = true;
            }
        }

        private void Undo()
        {
            UserAction action = (UserAction) this.UndoList[0];
            this.m_DataGridView.CurrentCell = this.m_DataGridView[action.ColumnIndex, action.RowIndex];
            this.UndoOrRedoAction = true;
            this.m_DataGridView.CurrentCell.Value = action.OldValue;
            this.UndoOrRedoAction = false;
            UserAction action2 = new UserAction(action.UserActionType, action.ColumnIndex, action.RowIndex, action.NewValue, action.OldValue);
            this.RedoList.Insert(0, action2);
            this.UndoList.RemoveAt(0);
            this.SetButtonEnabledness();
        }

        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("A set of properties specifying the visibility of buttons")]
        public Teroid.DataGridViewToolStrip.ButtonsVisible ButtonsVisible
        {
            get
            {
                return this.m_ButtonsVisible;
            }
        }

        [Description("The DataGridView object associated with the tool strip"), DefaultValue((string) null), Category("Data")]
        public System.Windows.Forms.DataGridView DataGridView
        {
            get
            {
                return this.m_DataGridView;
            }
            set
            {
                this.m_DataGridView = value;
                this.DGVPrintCtl.DataGridView = this.m_DataGridView;
                this.DGVExportCtl.DataGridView = this.m_DataGridView;
                if (this.m_DataGridView != null)
                {
                    this.m_DataGridView.DataSourceChanged += new EventHandler(this.m_DataGridView_DataSourceChanged);
                    this.m_DataGridView.DataMemberChanged += new EventHandler(this.m_DataGridView_DataMemberChanged);
                    this.m_DataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.m_DataGridView_RowsAdded);
                    this.m_DataGridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.m_DataGridView_RowsRemoved);
                    this.m_DataGridView.CellValueChanged += new DataGridViewCellEventHandler(this.CellValueChanged);
                    this.m_DataGridView.CurrentCellChanged += new EventHandler(this.CurrentCellChanged);
                    this.m_DataGridView.AllowUserToAddRowsChanged += new EventHandler(this.AllowUserToAddRowsChanged);
                    this.m_DataGridView.AllowUserToDeleteRowsChanged += new EventHandler(this.AllowUserToDeleteRowsChanged);
                    this.m_DataGridView.ReadOnlyChanged += new EventHandler(this.ReadOnlyChanged);
                    this.dlgSearch.DataGridView = this.m_DataGridView;
                    this.dlgReplace.DataGridView = this.m_DataGridView;
                    if (this.m_DataGridView.DataSource.GetType().ToString() == "System.Windows.Forms.BindingSource")
                    {
                        this.m_BindingSource = (BindingSource) this.m_DataGridView.DataSource;
                        this.m_BindingSource.PositionChanged += new EventHandler(this.m_BindingSource_PositionChanged);
                        this.SetButtonEnabledness();
                        this.dlgSort.Columns.Clear();
                        for (int i = 0; i < this.m_DataGridView.Columns.Count; i++)
                        {
                            this.dlgSort.Columns.Add(this.m_DataGridView.Columns[i].DataPropertyName);
                        }
                        this.dlgSort.Populate();
                        this.dlgFilter.BindingSource = this.m_BindingSource;
                    }
                    this.SetButtonEnabledness();
                }
                else
                {
                    this.DisableAllButtons();
                }
            }
        }

        [Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("A set of properties specifying the tool tip text of buttons")]
        public Teroid.DataGridViewToolStrip.ToolTipText ToolTipText
        {
            get
            {
                return this.m_ToolTipText;
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
    }
}

