/********************************************************************
  Class      : TextEditorFactory
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace PragmaSQL
{
	internal static class TextEditorFactory
	{
		private static int _instanceCnt = 0;
		internal static int InstanceCnt
		{
			get { return _instanceCnt; }
		}

		internal static WimdowNumerator Numerator = null;

		static TextEditorFactory()
		{
			Numerator = new WimdowNumerator();
		}

		internal static void ResetNumerator()
		{
			if (Numerator.WindowCount != 0)
				return;
			_instanceCnt = 0;
			Numerator.ClearReclaimedNumbers();
		}

		internal static void ShowTextEditor(frmTextEditor frm)
		{
			if (frm == null)
			{
				return;
			}

			if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
			{
				frm.MdiParent = Program.MainForm;
				frm.Show();
				frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			}
			else if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.DockingWindow)
			{
				frm.Show();
			}
			else
			{
				frm.Show(Program.MainForm.DockPanel);
			}
		}


		internal static frmTextEditor Create()
		{
			return Create(String.Empty, String.Empty);
		}

		internal static frmTextEditor Create(string script)
		{
			return Create(String.Empty, script);
		}

    internal static frmTextEditor Create(string caption, string script)
    {
      return Create(caption, script, String.Empty);
    }

    internal static frmTextEditor Create(string caption, string script, string syntaxMode)
    {
      return Create(caption, script, syntaxMode, String.Empty);
    }

		internal static frmTextEditor Create(string caption, string script, string syntaxMode, string filePath)
		{
			int? windowNo = Numerator.NextNumber;
			if (!windowNo.HasValue)
			{
				_instanceCnt++;
				windowNo = _instanceCnt;
			}

			string c = String.IsNullOrEmpty(caption) ? String.Format("Text {0}", windowNo) : caption;
			frmTextEditor frm = new frmTextEditor();
			frm.WindowNo = windowNo;
			frm.InitializeTextEditor(c, script);
      if(!String.IsNullOrEmpty(syntaxMode))
        frm.SetSyntaxMode(syntaxMode);

      if (!String.IsNullOrEmpty(filePath))
        frm.SetFilePath(filePath);
      return frm;
		}

		internal static frmTextEditor OpenFile(string fileName)
		{
			frmTextEditor frm = Create();
			if (!frm.OpenFile(fileName))
			{
				frm.Close();
				frm.Dispose();
				return null;
			}
      frm.ContentPersister.ContentType = EditorContentType.File;
      return frm;
		}

		internal static frmTextEditor OpenSharedSnippet(SharedSnippetItemData itemData)
		{
			string caption = String.Empty;
			string script = String.Empty;

			if (itemData == null)
			{
				throw new NullParameterException("ItemData is null!");
			}

			caption = itemData.Name;
			script = itemData.Snippet;

			frmTextEditor frm = new frmTextEditor();
			frm.Icon = PragmaSQL.Properties.Resources.sharedSnippet;
			frm.ContentPersister = new SharedSnippetContentPersister();
			frm.ContentPersister.Data = itemData;
			frm.ContentPersister.Hint = "This is a shared snippet: " + itemData.Name;
			frm.ContentInfo = frm.ContentPersister.Hint;

			frm.ContentPersister.FilePath = itemData.Name;
			frm.InitializeTextEditor(caption, script);
			return frm;
		}

    internal static frmTextEditor CreateSharedSnippet(string caption, string snippet)
    {
      frmTextEditor frm = new frmTextEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedSnippet;
      frm.ContentPersister = new SharedSnippetContentPersister();
      frm.InitializeTextEditor(caption, snippet);
      return frm;
    }

    internal static frmTextEditor CreateSharedScript(string caption, string script)
    {
      frmTextEditor frm = new frmTextEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedScript;
      frm.ContentPersister = new SharedScriptContentPersister();

      frm.InitializeTextEditor(caption, script);
      return frm;
    }

		internal static frmTextEditor OpenSharedScript(SharedScriptsItemData itemData)
		{
			string caption = String.Empty;
			string script = String.Empty;

			if (itemData == null)
			{
				throw new NullParameterException("ItemData is null!");
			}

			caption = itemData.Name;
			script = itemData.Script;

			frmTextEditor frm = new frmTextEditor();
			frm.Icon = PragmaSQL.Properties.Resources.sharedScript;
			frm.ContentPersister = new SharedScriptContentPersister();
			frm.ContentPersister.Data = itemData;
			frm.ContentPersister.Hint = "This is a shared script: " + itemData.Name;
			frm.ContentInfo = frm.ContentPersister.Hint;

			frm.ContentPersister.FilePath = itemData.Name;
			frm.InitializeTextEditor(caption, script);
			return frm;
		}

	} // Class end
} // Namespace end
