using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.WorkspaceMan
{
	public enum WorkspaceItemType
	{
		None,
		DatabaseObject,
		TextFile,
		ScriptFile,
		ProjectFile,
    Content,
		Connection
	}

	public enum WorkspaceItemTarget
	{
		None,
		ScriptEditor,
		TextEditor,
		ObjectExplorer,
		ProjectExplorer,
    TextContent
	}
}
