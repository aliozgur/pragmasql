/********************************************************************
  Class EditorServices
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL
{
	public class EditorServices : IEditorService
	{
		#region IEditorServices Members

		public ITextEditor CurrentTextEditor
		{
			get
			{
				if (Program.MainForm == null)
				{
					return null;
				}
				return Program.MainForm.GetCurrentTextEditor();
			}
		}

		public IList<ITextEditor> TextEditors
		{
			get
			{
				if (Program.MainForm == null)
				{
					return null;
				}
				return Program.MainForm.GetAllTextEditors();
			}
		}

		public IScriptEditor CurrentScriptEditor
		{
			get
			{
				if (Program.MainForm == null)
				{
					return null;
				}
				return Program.MainForm.GetCurrentScriptEditor();
			}
		}

		public IList<IScriptEditor> ScriptEditors
		{
			get
			{
				if (Program.MainForm == null)
				{
					return null;
				}
				return Program.MainForm.GetAllScriptEditors();
			}
		}

		public ITextEditor CurrentEditor
		{
			get
			{
				if (Program.MainForm == null)
				{
					return null;
				}
				return Program.MainForm.GetCurrentEditor();
			}
		}

    public IList<ITextEditor> Editors
    {
      get
      {
        if (Program.MainForm == null)
        {
          return null;
        }
        return Program.MainForm.GetAllEditors();
      }
    }

		private FormClosedEventHandler _editorClosed;
		public event FormClosedEventHandler EditorClosed
		{
			add { _editorClosed += value; }
			remove { _editorClosed -= value; }
		}

		internal void FireEditorClosed(object sender, FormClosedEventArgs e)
		{
			if (_editorClosed == null)
			{
				return;
			}

			Delegate[] delegates = _editorClosed.GetInvocationList();
			foreach (FormClosedEventHandler del in delegates)
			{
				try
				{
					del.Invoke(sender, e);
				}
				catch (Exception ex)
				{
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
				}
			}
		}



		private FormClosingEventHandler _editorClosing;
		public event FormClosingEventHandler EditorClosing
		{
			add { _editorClosing += value; }
			remove { _editorClosing -= value; }
		}

		internal bool FireEditorClosing(object sender, FormClosingEventArgs e)
		{
			if (_editorClosing == null)
			{
				return false;
			}

			bool cancel = false;
			Delegate[] delegates = _editorClosing.GetInvocationList();
			foreach (FormClosingEventHandler del in delegates)
			{
				try
				{
					FormClosingEventArgs args = new FormClosingEventArgs(e.CloseReason, false);
					del.Invoke(sender, args);
					cancel = args.Cancel || cancel;
				}
				catch (Exception ex)
				{
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
				}
			}

			return cancel;
		}


		private EventHandler _editorReady;
		public event EventHandler EditorReady
		{
			add { _editorReady += value; }
			remove { _editorReady -= value; }
		}
		internal void FireEditorReadyEvent(object sender)
		{
			if (_editorReady != null)
				_editorReady(sender, EventArgs.Empty);
		}

		public void CreateScriptEditor(string caption, string script, ConnectionParams cp)
		{
			frmScriptEditor frm = null;
			try
			{
				frm = ScriptEditorFactory.Create(caption, script, cp);
				ScriptEditorFactory.ShowScriptEditor(frm);
			}
			catch (Exception ex)
			{
				frm.Dispose();
				frm = null;
				throw ex;
			}
		}

		public void CreateTextEditor(string caption, string script)
		{
			frmTextEditor frm = null;
			try
			{
				frm = TextEditorFactory.Create(caption, script);
				TextEditorFactory.ShowTextEditor(frm);
			}
			catch (Exception ex)
			{
				frm.Dispose();
				frm = null;
				throw ex;
			}
		}

		public void LoadTextFile(string fileName)
		{
			frmTextEditor frm = null;
			try
			{
				frm = TextEditorFactory.OpenFile(fileName);
				TextEditorFactory.ShowTextEditor(frm);
			}
			catch (Exception ex)
			{
				frm.Dispose();
				frm = null;
				throw ex;
			}
		}

		public void LoadScriptFile(string fileName, ConnectionParams cp)
		{
			frmScriptEditor frm = null;
			try
			{
				frm = ScriptEditorFactory.OpenFile(fileName, cp);
				ScriptEditorFactory.ShowScriptEditor(frm);
			}
			catch (Exception ex)
			{
				frm.Dispose();
				frm = null;
				throw ex;
			}
		}

		#endregion
	}
}
