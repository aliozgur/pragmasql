/********************************************************8
 *	Author: Andrew Deren
 *	Date: July, 2004
 *	http://www.adersoftware.com
 * 
 *	StringTokenizer class. You can use this class in any way you want
 * as long as this header remains in this file.
 * 
 **********************************************************/
using System;
using System.IO;
using System.Text;

namespace PragmaSQL.Core
{
	/// <summary>
	/// StringTokenizer tokenized string (or stream) into tokens.
	/// </summary>
	public class CustomStringTokenizer
	{
		const char EOF = (char)0;

		int line;
		int column;
		int pos;	// position within data

		string data;

		bool ignoreWhiteSpace;
		char[] symbolChars;

		int saveLine;
		int saveCol;
		int savePos;

		public CustomStringTokenizer(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");

			data = reader.ReadToEnd();

			Reset();
		}

		public CustomStringTokenizer(string data)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			this.data = data;

			Reset();
		}

		/// <summary>
		/// gets or sets which characters are part of TokenKind.Symbol
		/// </summary>
		public char[] SymbolChars
		{
			get { return this.symbolChars; }
			set { this.symbolChars = value; }
		}

		/// <summary>
		/// if set to true, white space characters will be ignored,
		/// but EOL and whitespace inside of string will still be tokenized
		/// </summary>
		public bool IgnoreWhiteSpace
		{
			get { return this.ignoreWhiteSpace; }
			set { this.ignoreWhiteSpace = value; }
		}

		private void Reset()
		{
			this.ignoreWhiteSpace = false;
			this.symbolChars = new char[]{'=', '+', '-', '/', ',', '.', '*', '~', '!', '@', '#', '$', '%', '^', '&', '(', ')', '{', '}', '[', ']', ':', ';', '<', '>', '?', '|', '\\'};

			line = 1;
			column = 1;
			pos = 0;
		}

		protected char LA(int count)
		{
			if (pos + count >= data.Length)
				return EOF;
			else
				return data[pos+count];
		}

		protected char Consume()
		{
			char ret = data[pos];
			pos++;
			column++;

			return ret;
		}

		protected CustomToken CreateToken(CustomTokenKind kind, string value)
		{
			return new CustomToken(kind, value, line, column);
		}

		protected CustomToken CreateToken(CustomTokenKind kind)
		{
			string tokenData = data.Substring(savePos, pos-savePos);
			return new CustomToken(kind, tokenData, saveLine, saveCol);
		}

		public CustomToken Next()
		{
			ReadToken:

			char ch = LA(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(CustomTokenKind.EOF, string.Empty);

				case ' ':
				case '\t':
				{
					if (this.ignoreWhiteSpace)
					{
						Consume();
						goto ReadToken;
					}
					else
						return ReadWhitespace();
				}
				case '\r':
				{
					StartRead();
					Consume();
					if (LA(0) == '\n')
						Consume();	// on DOS/Windows we have \r\n for new line

					line++;
					column=1;

					return CreateToken(CustomTokenKind.EOL);
				}
				case '\n':
				{
					StartRead();
					Consume();
					line++;
					column=1;
					
					return CreateToken(CustomTokenKind.EOL);
				}

				case '"':
				{
					return ReadString();
				}
				default:
				{
          if (Char.IsLetterOrDigit(ch) || ch == '_' || ch == '#' || ch == '@')
						return ReadWord();
					else if (IsSymbol(ch))
					{
						StartRead();
						Consume();
						return CreateToken(CustomTokenKind.Symbol);
					}
					else
					{
						StartRead();
						Consume();
						return CreateToken(CustomTokenKind.Unknown);						
					}
				}

			}
		}

		/// <summary>
		/// save read point positions so that CreateToken can use those
		/// </summary>
		private void StartRead()
		{
			saveLine = line;
			saveCol = column;
			savePos = pos;
		}

		/// <summary>
		/// reads all whitespace characters (does not include newline)
		/// </summary>
		/// <returns></returns>
		protected CustomToken ReadWhitespace()
		{
			StartRead();

			Consume(); // consume the looked-ahead whitespace char

			while (true)
			{
				char ch = LA(0);
				if (ch == '\t' || ch == ' ')
					Consume();
				else
					break;
			}

			return CreateToken(CustomTokenKind.WhiteSpace);
			
		}

		/// <summary>
		/// reads word. Word contains any alpha character or _
		/// </summary>
		protected CustomToken ReadWord()
		{
			StartRead();

			Consume(); // consume first character of the word

			while (true)
			{
				char ch = LA(0);
				if (Char.IsLetterOrDigit(ch) || ch == '_' || ch == '#' || ch == '@')
					Consume();
				else
					break;
			}

			return CreateToken(CustomTokenKind.Word);
		}

		/// <summary>
		/// reads all characters until next " is found.
		/// If "" (2 quotes) are found, then they are consumed as
		/// part of the string
		/// </summary>
		/// <returns></returns>
		protected CustomToken ReadString()
		{
			StartRead();

			Consume(); // read "

			while (true)
			{
				char ch = LA(0);
				if (ch == EOF)
					break;
				else if (ch == '\r')	// handle CR in strings
				{
					Consume();
					if (LA(0) == '\n')	// for DOS & windows
						Consume();

					line++;
					column = 1;
				}
				else if (ch == '\n')	// new line in quoted string
				{
					Consume();

					line++;
					column = 1;
				}
				else if (ch == '"')
				{
					Consume();
					if (LA(0) != '"')
						break;	// done reading, and this quotes does not have escape character
					else
						Consume(); // consume second ", because first was just an escape
				}
				else
					Consume();
			}

			return CreateToken(CustomTokenKind.QuotedString);
		}

		/// <summary>
		/// checks whether c is a symbol character.
		/// </summary>
		protected bool IsSymbol(char c)
		{
			for (int i=0; i<symbolChars.Length; i++)
				if (symbolChars[i] == c)
					return true;

			return false;
		}
	}
}
