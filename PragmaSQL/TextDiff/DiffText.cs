using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Actions;

using DifferenceEngine;

namespace PragmaSQL
{
    public enum TargetEditorType
    {
        Source,
        Dest
    }

    public partial class DiffText : UserControl
    {
        
        private AfterOpenFileDelegate _onAfterOpenFile;
        public event AfterOpenFileDelegate OnAfterOpenFile
        {
            add { _onAfterOpenFile += value; }
            remove { _onAfterOpenFile -= value; }
        }

        private AfterPasteFromClipboardDelegate _onAfterPasteFromClipboard;
        public event AfterPasteFromClipboardDelegate OnAfterPasteFromClipboard
        {
            add { _onAfterPasteFromClipboard += value; }
            remove { _onAfterPasteFromClipboard -= value; }
        }

        private AfterTextDragDropDelegate _onAfterTextDragDrop;
        public event AfterTextDragDropDelegate OnAfterTextDragDrop
        {
            add { _onAfterTextDragDrop += value; }
            remove { _onAfterTextDragDrop -= value; }
        }

        private bool _allowFileDrop = false;
        public bool AllowFileDrop
        {
            get { return _allowFileDrop; }
            set { _allowFileDrop = value; }
        }

        private bool _allowTextDrop = false;
        public bool AllowTextDrop
        {
            get { return _allowTextDrop; }
            set { _allowTextDrop = value; }
        }

        public bool ShowOpenFileMenuButtons
        {
            get { return btnOpenSource.Visible; }
            set { btnOpenSource.Visible = value; btnOpenDest.Visible = value; }
        }

        public bool ShowPasteMenuButtons
        {
            get { return btnPasteSource.Visible; }
            set { btnPasteSource.Visible = value; btnPasteDest.Visible = value; }
        }

        public bool ContextMenusEnabled
        {
            get { return txtSource.ContextMenuStrip != null; }
            set
            {
                if (value)
                {
                    txtSource.ContextMenuStrip = contextMenuStripSource;
                    txtDest.ContextMenuStrip = contextMenuStripDest;
                }
                else
                {
                    txtSource.ContextMenuStrip = null;
                    txtDest.ContextMenuStrip = null;
                }
            }
        }


        //public IHighlightingStrategy SourceHighlightingStrategy
        //{
        //    get { return txtSource.Document.HighlightingStrategy; }
        //    set { txtSource.Document.HighlightingStrategy = value; }
        //}

        //public IHighlightingStrategy DestHighlightingStrategy
        //{
        //    get { return txtDest.Document.HighlightingStrategy; }
        //    set { txtDest.Document.HighlightingStrategy = value; }
        //}

        [Category("Headers")]
        public string SourceHeaderText
        {
            get { return txtSourceHeader.Text; }
            set { txtSourceHeader.Text = value; }
        }

        [Category("Headers")]
        public string DestHeaderText
        {
            get { return txtDestHeader.Text; }
            set { txtDestHeader.Text = value; }
        }


        [Category("Headers")]
        public bool HeadersVisible
        {
            get
            {
                return (layoutPanel.RowStyles[0].Height > 0);
            }
            set
            {
                if (value)
                {
                    layoutPanel.RowStyles[0].Height = 25;
                }
                else
                {
                    layoutPanel.RowStyles[0].Height = 0;
                }
            }
        }

        private string _originalSourceText = String.Empty;
        [Category("Diff Data")]
        public string SourceText
        {
            get { return txtSource.Text; }
            set
            {
                ClearCustomLines();
                try
                {
                    SourceDoc.ReadOnly = false;
                    txtSource.Text = value;
                    txtDest.Text = _originalDestText;
                    _originalSourceText = value;
                }
                finally
                {
                    SourceDoc.ReadOnly = true;
                    InvalidateTextEditors();
                }
            }
        }

        private string _originalDestText = String.Empty;
        [Category("Diff Data")]
        public string DestText
        {
            get { return txtDest.Text; }
            set
            {
                ClearCustomLines();
                try
                {
                    DestDoc.ReadOnly = false;
                    txtDest.Text = value;
                    txtSource.Text = _originalSourceText;
                    _originalDestText = value;
                }
                finally
                {
                    DestDoc.ReadOnly = true;
                    InvalidateTextEditors();
                }
            }
        }

        private TextArea SourceTextArea
        {
            get { return txtSource.ActiveTextAreaControl.TextArea; }
        }

        private IDocument SourceDoc
        {
            get { return txtSource.Document; }
        }

        private TextArea DestTextArea
        {
            get { return txtDest.ActiveTextAreaControl.TextArea; }
        }

        private IDocument DestDoc
        {
            get { return txtDest.Document; }
        }

        //private HighlightColor _destDocHighlightColor;
        //private HighlightColor _sourceDocHighlightColor;


        public DiffText()
        {
            InitializeComponent();
            InitializeTextEditors();
        }

        private void InitializeTextEditors()
        {
            txtSource.TextEditorProperties.UseCustomLine = true;
            txtDest.TextEditorProperties.UseCustomLine = true;

            txtSource.TextEditorProperties.ShowEOLMarker = false;
            txtDest.TextEditorProperties.ShowEOLMarker = false;

            txtSource.TextEditorProperties.ShowSpaces = false;
            txtDest.TextEditorProperties.ShowSpaces = false;

            txtSource.TextEditorProperties.ShowTabs = false;
            txtDest.TextEditorProperties.ShowTabs = false;

            /*
            txtSource.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
            txtDest.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
            */

            SourceDoc.DocumentChanged += new DocumentEventHandler(SourceDoc_DocumentChanged);
            txtSource.ActiveTextAreaControl.VScrollBar.ValueChanged += new EventHandler(txtSource_TextAreaControl_VScrollValueChanged);
            txtSource.ActiveTextAreaControl.HScrollBar.ValueChanged += new EventHandler(txtSource_TextAreaControl_HScrollValueChanged);

            DestDoc.DocumentChanged += new DocumentEventHandler(DestDoc_DocumentChanged);
            txtDest.ActiveTextAreaControl.VScrollBar.ValueChanged += new EventHandler(txtDest_TextAreaControl_VScrollValueChanged);
            txtDest.ActiveTextAreaControl.HScrollBar.ValueChanged += new EventHandler(txtDest_TextAreaControl_HScrollValueChanged);


            SourceTextArea.AllowDrop = true;
            SourceTextArea.DragDrop += new DragEventHandler(OnTextArea_DragDrop);
            SourceTextArea.DragOver += new DragEventHandler(OnTextArea_DragOver);

            DestTextArea.AllowDrop = true;
            DestTextArea.DragDrop += new DragEventHandler(OnTextArea_DragDrop);
            DestTextArea.DragOver += new DragEventHandler(OnTextArea_DragOver);

            SourceDoc.ReadOnly = true;
            DestDoc.ReadOnly = true;
            
            //_destDocHighlightColor = DestDoc.HighlightingStrategy.GetColorFor("Default");
            //_sourceDocHighlightColor = SourceDoc.HighlightingStrategy.GetColorFor("Default");

            //if (_destDocHighlightColor != null)
            //{
            //    toolStripStatusLabel1.BackColor = _destDocHighlightColor.BackgroundColor;
            //    toolStripStatusLabel1.ForeColor = _destDocHighlightColor.Color;
            //}
        }


        void OnTextArea_DragOver(object sender, DragEventArgs e)
        {
            string[] fileDrop = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileDrop != null && fileDrop.Length > 0 && _allowFileDrop)
            {
                e.Effect = DragDropEffects.All;
                return;
            }

            string tmp = e.Data.GetData(typeof(string)) as string;
            if (!String.IsNullOrEmpty(tmp) && _allowTextDrop)
            {
                e.Effect = DragDropEffects.All;
                return;
            }
            e.Effect = DragDropEffects.None;
        }

        void OnTextArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileDrop = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileDrop != null && fileDrop.Length > 0)
            {
                if (sender == SourceTextArea)
                {
                    OpenSourceFile(fileDrop[0]);
                }
                else if (sender == DestTextArea)
                {
                    OpenDestFile(fileDrop[0]);
                }
                return;
            }

            string tmp = e.Data.GetData(typeof(string)) as string;
            if (!String.IsNullOrEmpty(tmp))
            {
                if (sender == SourceTextArea)
                {
                    SourceText = tmp;
                    if (_onAfterTextDragDrop != null)
                    {
                        _onAfterTextDragDrop(this, TargetEditorType.Source);
                    }
                }
                else if (sender == DestTextArea)
                {
                    DestText = tmp;
                    if (_onAfterTextDragDrop != null)
                    {
                        _onAfterTextDragDrop(this, TargetEditorType.Dest);
                    }
                }
                return;
            }
        }




        private bool _canScroll = true;
        void txtSource_TextAreaControl_VScrollValueChanged(object sender, EventArgs e)
        {
            if (!_canScroll)
            {
                return;
            }

            if (txtSource.ActiveTextAreaControl.VScrollBar.Value <= txtDest.ActiveTextAreaControl.VScrollBar.Maximum)
            {
                _canScroll = false;
                txtDest.ActiveTextAreaControl.VScrollBar.Value = txtSource.ActiveTextAreaControl.VScrollBar.Value;
                _canScroll = true;
            }
        }

        void txtSource_TextAreaControl_HScrollValueChanged(object sender, EventArgs e)
        {
            if (!_canScroll)
            {
                return;
            }
            if (txtSource.ActiveTextAreaControl.HScrollBar.Value <= txtDest.ActiveTextAreaControl.HScrollBar.Maximum)
            {
                _canScroll = false;
                txtDest.ActiveTextAreaControl.HScrollBar.Value = txtSource.ActiveTextAreaControl.HScrollBar.Value;
                _canScroll = true;
            }
        }

        void txtDest_TextAreaControl_VScrollValueChanged(object sender, EventArgs e)
        {
            if (!_canScroll)
            {
                return;
            }

            if (txtDest.ActiveTextAreaControl.VScrollBar.Value <= txtSource.ActiveTextAreaControl.VScrollBar.Maximum)
            {
                _canScroll = false;
                txtSource.ActiveTextAreaControl.VScrollBar.Value = txtDest.ActiveTextAreaControl.VScrollBar.Value;
                _canScroll = true;
            }
        }

        void txtDest_TextAreaControl_HScrollValueChanged(object sender, EventArgs e)
        {
            if (!_canScroll)
            {
                return;
            }
            if (txtDest.ActiveTextAreaControl.HScrollBar.Value <= txtSource.ActiveTextAreaControl.HScrollBar.Maximum)
            {
                _canScroll = false;
                txtSource.ActiveTextAreaControl.HScrollBar.Value = txtDest.ActiveTextAreaControl.HScrollBar.Value;
                _canScroll = true;
            }
        }

        private bool _isDisplayingDiff = false;
        void DestDoc_DocumentChanged(object sender, DocumentEventArgs e)
        {
            if (_isDisplayingDiff)
            {
                return;
            }

            ClearCustomLines();
        }

        void SourceDoc_DocumentChanged(object sender, DocumentEventArgs e)
        {
            if (_isDisplayingDiff)
            {
                return;
            }
            ClearCustomLines();
        }

        public void OpenSourceFile(string fileName)
        {
            ClearCustomLines();
            txtSource.LoadFile(fileName, false, true);
            _originalSourceText = txtSource.Text;
            InvalidateTextEditors();

            if (_onAfterOpenFile != null)
                _onAfterOpenFile(this, TargetEditorType.Source, fileName);

        }

        public void OpenSourceFile()
        {
            openFileDialog1.Title = "Open Source File";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            OpenSourceFile(openFileDialog1.FileName);
        }

        public void OpenDestFile(string fileName)
        {
            ClearCustomLines();
            txtDest.LoadFile(fileName, false, true);
            _originalDestText = txtDest.Text;
            InvalidateTextEditors();
            if (_onAfterOpenFile != null)
                _onAfterOpenFile(this, TargetEditorType.Dest, fileName);
        }

        public void OpenDestFile()
        {
            openFileDialog1.Title = "Open Destination File";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            OpenDestFile(openFileDialog1.FileName);
        }


        private void ClearCustomLines()
        {
            txtSource.Document.CustomLineManager.Clear();
            txtDest.Document.CustomLineManager.Clear();
            InvalidateTextEditors();
        }

        private void InvalidateTextEditors()
        {
            SourceTextArea.Invalidate();
            SourceTextArea.Update();

            DestTextArea.Invalidate();
            DestTextArea.Update();
        }

        private void CleanResultTextEditors()
        {
            txtSource.Text = String.Empty;
            txtSource.Invalidate();
            txtSource.Update();

            txtDest.Text = String.Empty;
            txtDest.Invalidate();
            txtDest.Update();
        }

        public void PasteSourceFromClipboard()
        {
            try
            {
                SourceDoc.ReadOnly = false;

                ClearCustomLines();
                txtSource.Text = String.Empty;
                SourceTextArea.ClipboardHandler.Paste(null, null);
                _originalSourceText = SourceDoc.TextContent;
                txtDest.Text = _originalDestText;

                if (_onAfterPasteFromClipboard != null)
                    _onAfterPasteFromClipboard(this, TargetEditorType.Source);

            }
            finally
            {
                InvalidateTextEditors();
                SourceDoc.ReadOnly = true;
            }
        }

        public void PasteDestFromClipboard()
        {
            try
            {
                DestDoc.ReadOnly = false;
                ClearCustomLines();

                txtDest.Text = String.Empty;
                DestTextArea.ClipboardHandler.Paste(null, null);
                _originalDestText = DestDoc.TextContent;
                txtSource.Text = _originalSourceText;

                if (_onAfterPasteFromClipboard != null)
                    _onAfterPasteFromClipboard(this, TargetEditorType.Dest);
            }
            finally
            {
                DestDoc.ReadOnly = true;
                InvalidateTextEditors();
            }
        }

        public void Compare()
        {
            TextDiff sourceDiffList = new TextDiff(_originalSourceText);
            TextDiff destDiffList = new TextDiff(_originalDestText);

            DiffEngine de = new DiffEngine();
            de.ProcessDiff(sourceDiffList, destDiffList, DiffEngineLevel.Medium);
            ArrayList rep = de.DiffReport();
            DisplayDiff(sourceDiffList, destDiffList, rep);
        }


        private void DisplayDiff(TextDiff source, TextDiff destination, ArrayList DiffLines)
        {
            CleanResultTextEditors();
            int cnt = 1;
            int i;

            try
            {
                _isDisplayingDiff = true;
                SourceDoc.ReadOnly = false;
                DestDoc.ReadOnly = false;
                SourceTextArea.BeginUpdate();
                DestTextArea.BeginUpdate();

                

                foreach (DiffResultSpan drs in DiffLines)
                {
                    switch (drs.Status)
                    {
                        case DiffResultSpanStatus.DeleteSource:
                            for (i = 0; i < drs.Length; i++)
                            {
                                var line = ((DiffTextLine)source.GetByIndex(drs.SourceIndex + i)).Line + "\n";
                                SourceTextArea.InsertString( line );
                                //SourceDoc.CustomLineManager.AddCustomLine(cnt - 1, Color.MistyRose, false);
                                
                                var segment = SourceDoc.GetLineSegment(cnt - 1);
                                if (segment.Length > 0)
                                {
                                    var marker = new TextMarker(segment.Offset, segment.Length, TextMarkerType.SolidBlock, Color.Green, Color.White);
                                    SourceDoc.MarkerStrategy.AddMarker(marker);
                                }

                                DestTextArea.InsertString("\n");
                                DestDoc.CustomLineManager.AddCustomLine(cnt - 1, Color.LightGray, false);

                                cnt++;
                            }
                            break;
                        case DiffResultSpanStatus.NoChange:
                            for (i = 0; i < drs.Length; i++)
                            {
                                var bgColor = SourceDoc.HighlightingStrategy.GetColorFor("Default");
                                SourceTextArea.InsertString(((DiffTextLine)source.GetByIndex(drs.SourceIndex + i)).Line + "\n");
                                //SourceDoc.CustomLineManager.AddCustomLine(cnt - 1, bgColor != null ? bgColor.BackgroundColor : Color.White, false);
                                
                                bgColor = DestDoc.HighlightingStrategy.GetColorFor("Default");
                                DestTextArea.InsertString(((DiffTextLine)destination.GetByIndex(drs.DestIndex + i)).Line + "\n");
                                //DestDoc.CustomLineManager.AddCustomLine(cnt - 1, bgColor != null ? bgColor.BackgroundColor : Color.White, false);

                                cnt++;
                            }

                            break;
                        case DiffResultSpanStatus.AddDestination:
                            for (i = 0; i < drs.Length; i++)
                            {
                                SourceTextArea.InsertString("\n");
                                SourceDoc.CustomLineManager.AddCustomLine(cnt - 1, Color.LightGray, false);

                                var line = ((DiffTextLine)destination.GetByIndex(drs.DestIndex + i)).Line + "\n";
                                DestTextArea.InsertString(line);
                                
                                var segment = DestDoc.GetLineSegment(cnt - 1);
                                if (segment.Length > 0)
                                {
                                    var marker = new TextMarker(segment.Offset, segment.Length, TextMarkerType.SolidBlock, Color.Red, Color.White);
                                    DestDoc.MarkerStrategy.AddMarker(marker);
                                }
                                
                                //DestDoc.CustomLineManager.AddCustomLine(cnt - 1, Color.LightGreen, false);
                                
                                cnt++;
                            }

                            break;
                        case DiffResultSpanStatus.Replace:

                            for (i = 0; i < drs.Length; i++)
                            {
                                var line = ((DiffTextLine)source.GetByIndex(drs.SourceIndex + i)).Line + "\n";
                                SourceTextArea.InsertString(line);
                                //SourceDoc.CustomLineManager.AddCustomLine(cnt - 1, Color.MistyRose, false);
                                var segment = SourceDoc.GetLineSegment(cnt - 1);
                                if (segment.Length > 0)
                                {
                                    var marker = new TextMarker(segment.Offset, segment.Length, TextMarkerType.SolidBlock, Color.Gold, Color.Black);
                                    SourceDoc.MarkerStrategy.AddMarker(marker);
                                }


                                line = ((DiffTextLine)destination.GetByIndex(drs.DestIndex + i)).Line + "\n";
                                DestTextArea.InsertString(line);

                                segment = DestDoc.GetLineSegment(cnt - 1);
                                if (segment.Length > 0)
                                {
                                    var marker = new TextMarker(segment.Offset, segment.Length, TextMarkerType.SolidBlock, Color.Gold, Color.Black);
                                    DestDoc.MarkerStrategy.AddMarker(marker);
                                }

                                //DestDoc.CustomLineManager.AddCustomLine(cnt - 1, Color.LightGreen, false);
                                cnt++;
                            }

                            break;
                    }
                }
            }
            finally
            {
                SourceTextArea.EndUpdate();
                DestTextArea.EndUpdate();

                SourceTextArea.ScrollTo(0);
                DestTextArea.ScrollTo(0);
                _isDisplayingDiff = false;
                SourceDoc.ReadOnly = true;
                DestDoc.ReadOnly = true;
            }
        }


        private void btnCompare_Click(object sender, EventArgs e)
        {
            Compare();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenSourceFile();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenDestFile();
        }

        private void btnPasteSource_Click(object sender, EventArgs e)
        {
            PasteSourceFromClipboard();
        }

        private void btnPasteDest_Click(object sender, EventArgs e)
        {
            PasteDestFromClipboard();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSourceFile();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteSourceFromClipboard();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenDestFile();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PasteDestFromClipboard();
        }
    }// Class end

    public delegate void AfterOpenFileDelegate(object sender, TargetEditorType targetType, string fileName);
    public delegate void AfterPasteFromClipboardDelegate(object sender, TargetEditorType targetType);
    public delegate void AfterTextDragDropDelegate(object sender, TargetEditorType targetType);

}//Namespace end
