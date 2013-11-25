/********************************************************
 *	Author: Andrew Deren
 *	Date: July, 2004
 *	http://www.adersoftware.com
 * 
 *	StringTokenizer class. You can use this class in any way you want
 * as long as this header remains in this file.
 * 
 **********************************************************/

using System;

namespace PragmaSQL.Core
{
	public enum CustomTokenKind
	{
		Unknown,
		Word,
		QuotedString,
		WhiteSpace,
		Symbol,
		EOL,
		EOF
	}

	public class CustomToken
	{
		int line;
		int column;
		string value;
		CustomTokenKind kind;

		public CustomToken(CustomTokenKind kind, string value, int line, int column)
		{
			this.kind = kind;
			this.value = value;
			this.line = line;
			this.column = column;
		}

		public int Column
		{
			get { return this.column; }
		}

		public CustomTokenKind Kind
		{
			get { return this.kind; }
		}

		public int Line
		{
			get { return this.line; }
		}

		public string Value
		{
			get { return this.value; }
		}
	}

}
