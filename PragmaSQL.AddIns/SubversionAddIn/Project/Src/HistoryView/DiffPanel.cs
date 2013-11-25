using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;


using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui;
using NSvn.Core;
using NSvn.Common;

using PragmaSQL.Core;

namespace PragmaSQL.Svn.Gui
{
	public partial class DiffPanel
	{
		private IViewContent viewContent;
		private TextEditorControl textEditor;
		
		public DiffPanel(IViewContent viewContent)
		{
			this.viewContent = viewContent;
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			textEditor = new TextEditorControl();
			textEditor.Dock = DockStyle.Fill;
			diffViewPanel.Controls.Add(textEditor);

      ApplyDefaultTextEditorProperties(textEditor);
			textEditor.Document.ReadOnly = true;
			textEditor.Enabled = false;
			
			textEditor.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("Patch");
			
			ListViewItem newItem;
			newItem = new ListViewItem(new string[] { "Base", "", "", "" });
			newItem.Tag = Revision.Base;
			leftListView.Items.Add(newItem);
			newItem.Selected = true;
			newItem = new ListViewItem(new string[] { "Work", "", "", "" });
			newItem.Tag = Revision.Working;
			rightListView.Items.Add(newItem);
		}

    private void ApplyDefaultTextEditorProperties( TextEditorControl control )
    {
      if(control == null)
      {
        return;
      }

      control.ConvertTabsToSpaces = false;
      control.ShowEOLMarkers = false;
      control.ShowInvalidLines = false;
      control.ShowLineNumbers = true;
      control.ShowMatchingBracket = true;
      control.ShowSpaces = false;
      control.ShowTabs = false;
      control.IndentStyle = IndentStyle.Smart;
      control.TabIndent = 2;
      control.VRulerRow = 120;
      control.LineViewerStyle = LineViewerStyle.None;
      control.UseAntiAliasFont = false;
      control.EnableFolding = true;
    }

		private void LeftListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			ShowDiff();
		}
		
		private void RightListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			ShowDiff();
		}
		

		string output = null;
		string fileName = null;
		Revision fromRevision;
		Revision toRevision;
		
		private void DoDiffOperation()
		{
			output = null;
			MemoryStream outStream = new MemoryStream();
			MemoryStream errStream = new MemoryStream();
			SvnClient.Instance.Client.Diff(new string [] {} ,
			                               fileName,
			                               fromRevision,
			                               fileName,
			                               toRevision,
			                               Recurse.None,
			                               false,
			                               true,
			                               outStream,
			                               errStream);
			output = Encoding.Default.GetString(outStream.ToArray());
			HostServicesSingleton.SafeThreadCall(SetOutput);
		}
		
		private void SetOutput()
		{
			textEditor.Enabled = true;
			diffLabel.Text = "Diff from revision " + fromRevision + " to " + toRevision + ":";
			textEditor.Text = output;
		}
		
		private void Disable()
		{
			textEditor.Enabled = false;
			diffLabel.Text = "Diff:";
			textEditor.Text = "";
		}
		
		private void ShowDiff()
		{
			Disable();
			
			if (leftListView.SelectedItems.Count == 0 || rightListView.SelectedItems.Count == 0 ) {
				return;
			}
			
			fromRevision = leftListView.SelectedItems[0].Tag as Revision;
			toRevision   = rightListView.SelectedItems[0].Tag as Revision;
			fileName     = Path.GetFullPath(viewContent.FileName);
			
			if (fromRevision.ToString() == toRevision.ToString()) {
				output = "";
			} else {
				SvnClient.Instance.OperationStart("Diff", DoDiffOperation);
			}
		}

		public void AddLogMessage(LogMessage logMessage)
		{
			ListViewItem newItem = new ListViewItem(new string[] {
			                                        	logMessage.Revision.ToString(),
			                                        	logMessage.Author,
			                                        	logMessage.Date.ToString(),
			                                        	logMessage.Message
			                                        });
			newItem.Tag = Revision.FromNumber(logMessage.Revision);
			leftListView.Items.Add(newItem);
			
			ListViewItem newItem2 = new ListViewItem(new string[] {
			                                         	logMessage.Revision.ToString(),
			                                         	logMessage.Author,
			                                         	logMessage.Date.ToString(),
			                                         	logMessage.Message
			                                         });
			newItem2.Tag = Revision.FromNumber(logMessage.Revision);
			rightListView.Items.Add(newItem2);
		}
	}
}
