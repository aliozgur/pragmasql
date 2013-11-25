using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
	public enum PragmaSqlMacros
	{
		[EnumStrValAttr("Startup Path")]
		StartupPath,
		[EnumStrValAttr("Content")]
		Content,
		[EnumStrValAttr("Selected Content")]
		SelectedContent,
		[EnumStrValAttr("File Name")]
		FileName,
		[EnumStrValAttr("Object Name")]
		ObjectName,
		[EnumStrValAttr("Object Names")]
		ObjectNames,
		[EnumStrValAttr("Obj. Explorer Node Name")]
		ObjExplorerNode,
		[EnumStrValAttr("Word At Cursor")]
		WordAtCursor,
		[EnumStrValAttr("Connection")]
		Connection,
		[EnumStrValAttr("Server Name")]
		ServerName,
		[EnumStrValAttr("Database Name")]
		DatabaseName,
		[EnumStrValAttr("Username")]
		Username
	}
}
