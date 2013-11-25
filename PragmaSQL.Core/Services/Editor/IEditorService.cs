using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public interface IEditorService
	{
		ITextEditor CurrentTextEditor { get; }
		IList<ITextEditor> TextEditors { get; }

		IScriptEditor CurrentScriptEditor { get; }
		IList<IScriptEditor> ScriptEditors { get; }

		ITextEditor CurrentEditor { get; }
    IList<ITextEditor> Editors { get; }

		event EventHandler EditorReady;
		event FormClosedEventHandler EditorClosed;
		event FormClosingEventHandler EditorClosing;
		void CreateScriptEditor(string caption, string script, ConnectionParams cp);
    void CreateTextEditor(string caption, string script);

		void LoadTextFile(string fileName);
		void LoadScriptFile(string fileName, ConnectionParams cp);

	}
}
