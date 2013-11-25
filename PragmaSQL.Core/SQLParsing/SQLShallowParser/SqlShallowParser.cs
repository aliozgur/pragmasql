/********************************************************************
  Class SqlShallowParser
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using com.calitha.goldparser;
using com.calitha.commons;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
	public class SqlShallowParser
	{
		private Stack<Paranthesis> _openingParanths = new Stack<Paranthesis>();

		private bool _parseSelectsAndCases = true;
		public bool ParseSelectsAndCases
		{
			get { return _parseSelectsAndCases; }
			set { _parseSelectsAndCases = value; }
		}

		private bool _parseCodeBlocks = true;
		public bool ParseCodeBlocks
		{
			get { return _parseCodeBlocks; }
			set { _parseCodeBlocks = value; }
		}

		private bool _parseComments = true;
		public bool ParseComments
		{
			get { return _parseComments; }
			set { _parseComments = value; }
		}


		public SqlParserResult ParseScript(BackgroundWorker workerThread, StringTokenizer tokenizer, string scriptText)
		{
			if (tokenizer == null)
			{
				throw new NullParameterException("Tokenizer is null!");
			}

			SqlParserResult result = new SqlParserResult();

			if (!_parseCodeBlocks && !_parseSelectsAndCases)
			{
				return result;
			}

			bool inBlockComment = false;
			bool inLineComment = false;
			string tokText = String.Empty;

			Paranthesis tmpParan = null;
			Paranthesis openingParan = null;

			SqlShBoundary currentComment = null;
			SqlShBoundary topComment = null;

			SqlStatement currentSelect = null;
			Stack<SqlStatement> selectStatments = new Stack<SqlStatement>();

			SqlStatement currentCase = null;
			Stack<SqlStatement> caseStatments = new Stack<SqlStatement>();

			Stack<SqlShBoundary> codeBlockBoundaries = new Stack<SqlShBoundary>();

			TerminalToken prevToken = null;
			TerminalToken tok = tokenizer.RetrieveToken();
			int commentLineNr = -1;
			bool elseOrEndConsumed = false;
	
	
			try
			{
				while (tok.Symbol.GetType() != typeof(SymbolEnd) && (workerThread == null || !workerThread.CancellationPending))
				{
					tokText = tok.Text.ToLowerInvariant().Trim();

					// This is comment start. Consume without any action and notify inComment for later passes
					
					if (tok.Symbol.GetType() == typeof(SymbolCommentStart))
					{
						inBlockComment = true;
						prevToken = tok;
						tok = tokenizer.RetrieveToken();
						
						if (_parseComments )
						{
							currentComment = new SqlShBoundary();
							currentComment.StartAsLocation =  new Location(tok.Location.Position - 2,tok.Location.LineNr,tok.Location.ColumnNr - 2);
							
						}
						continue;
					}

					// At the moment we are in comment block and we consume the token without any action
					if (inBlockComment && tok.Symbol.GetType() == typeof(SymbolCommentEnd))
					{
						inBlockComment = false;
						prevToken = tok;
						tok = tokenizer.RetrieveToken();

						if (_parseComments && currentComment != null)
						{
							currentComment.EndAsLocation = new Location(tok.Location);
							result.Comments.Add(currentComment);

							if (currentSelect != null || currentCase != null)
							{
								//currentComment.DoNotFold = true;
								currentComment.DoNotPaint = true;
							}
						}
						currentComment = null;

						continue;
					}

					if (inBlockComment)
					{
						prevToken = tok;
						tok = tokenizer.RetrieveToken();
						continue;
					}

					// Single line comment is starting. We consume following tokens without any action
					if (tok.Symbol.GetType() == typeof(SymbolCommentLine))
					{
						inLineComment = true;
						commentLineNr = tok.Location.LineNr;
						prevToken = tok;
						tok = tokenizer.RetrieveToken();

						
						/* We will not fold single line comments
						if (_parseComments)
						{
							currentComment = new SqlShBoundary();
							currentComment.StartAsLocation = new Location(tok.Location.Position - 2, tok.Location.LineNr, tok.Location.ColumnNr - 2);
							currentComment.EndAsLocation = currentComment.StartAsLocation;
														
							result.Comments.Add(currentComment);
							topComment = currentComment;
							if (currentSelect != null || currentCase != null)
							{
								currentComment.DoNotFold = true;
								currentComment.DoNotPaint = true;
							}
						}
						currentComment = null;
						*/

						continue;
					}

					// We are in comment line. Consume the token without any action
					if (inLineComment && (commentLineNr == tok.Location.LineNr))
					{
						if (ParseCodeBlocks)
						{
							if (tok.Text.ToLowerInvariant() == "#block")
							{
								SqlShBoundary b = new SqlShBoundary();
								b.StartAsLocation = tok.Location;
								codeBlockBoundaries.Push(b);
								if(topComment != null)
									result.Comments.Remove(topComment);
							}
							else if (codeBlockBoundaries.Count > 0 && tok.Text.ToLowerInvariant() == "#endblock")
							{
								SqlShBoundary startBoundary = codeBlockBoundaries.Pop();
								startBoundary.EndAsLocation = tok.Location;
								result.CodeBlocks.Add(startBoundary);
								if (topComment != null)
									result.Comments.Remove(topComment);
							}
						}

						prevToken = tok;
						tok = tokenizer.RetrieveToken();
						continue;
					}
					else
					{
						inLineComment = false;
						commentLineNr = -1;
					}



					//Whitespace. Consume token without any action.
					if (tok.Symbol.GetType() == typeof(SymbolWhiteSpace))
					{
						prevToken = tok;
						tok = tokenizer.RetrieveToken();
						continue;
					}


					if (!ParseSelectsAndCases)
					{
						prevToken = tok;
						tok = tokenizer.RetrieveToken();
						continue;
					}

					result.Tokens.Add(tok);
					elseOrEndConsumed = false;

					// Thanks god :) We met a "select". 
					if (tokText == "select")
					{
						currentSelect = new SqlStatement(result.StartLocations, SqlShParserStatmentType.Select);
						if (openingParan != null)
						{
							currentSelect.PushStartLocation(prevToken.Location);
						}
						else
						{
							currentSelect.PushStartLocation(tok.Location);
						}


						// This is probably start of the subselect or paranthesized select statement
						if (openingParan != null)
						{
							openingParan.IsPreSelect = true;
							if (selectStatments.Count > 0)
							{
								SqlStatement parentStmnt = selectStatments.Peek();
								parentStmnt.HasAnySubStatement = true;
								parentStmnt.PushEndLocation(prevToken.Location);
								currentSelect.ParentStatment = parentStmnt;
								currentSelect.IsSubStatement = true;
							}
						}
						/*
						 * This is a new select block. If there are any other
						 * select statments in the stack pop them
						*/
						else if (selectStatments.Count > 0)
						{
							SqlStatement stm = selectStatments.Pop();
							if (stm != null)
							{
								result.Statements.Add(stm);
							}
							stm.PushEndLocation(prevToken.Location);
							if (stm.ParentStatment != null)
							{
								stm.ParentStatment.PushStartLocation(prevToken.Location);
							}
						}
						selectStatments.Push(currentSelect);
					}
					else
					{
						// Here we evaluate CASE statments
						SqlStatement topCase = null;
						if (caseStatments.Count > 0)
						{
							topCase = caseStatments.Peek();
						}

						if (tokText == "case")
						{
							currentCase = new SqlStatement(result.StartLocations, SqlShParserStatmentType.Case);
							if (caseStatments.Count > 0)
							{
								SqlStatement parentCase = caseStatments.Peek();
								parentCase.HasAnySubStatement = true;
								parentCase.PushEndLocation(tok.Location);

								currentCase.ParentStatment = parentCase;
								currentCase.IsSubStatement = true;
							}
							else if (selectStatments.Count > 0)
							{
								SqlStatement parentStatment = selectStatments.Peek();
								parentStatment.PushEndLocation(tok.Location, -1 * tokText.Length);
							}

							caseStatments.Push(currentCase);
							currentCase.PushStartLocation(tok.Location);
							currentCase.AddTokenToHierarchy(tokText);
						}
						else if (tokText == "else")
						{
							if (caseStatments.Count > 0)
							{
								elseOrEndConsumed = true;
							}
						}
						else if (tokText == "end")
						{
							if (caseStatments.Count > 0)
							{
								SqlStatement caseStatement = caseStatments.Pop();
                caseStatement.PushEndLocation(tok.Location, tokText.Length);
                result.Statements.Add(caseStatement);
								if (caseStatments.Count > 0)
								{
									SqlStatement parentCase = caseStatments.Peek();
									parentCase.PushStartLocation(tok.Location, tokText.Length);
								}

								if (selectStatments.Count > 0)
								{
									SqlStatement parentStatment = selectStatments.Peek();
									caseStatement.ParentStatment = parentStatment;
									parentStatment.PushStartLocation(tok.Location, tokText.Length);
								}
								elseOrEndConsumed = true;
							}
						}

						if (topCase != null)
						{
							topCase.AddTokenToHierarchy(tokText);
						}
					}

					/*
					 * Token is an opening paranthesis. We will use this notification
					 * to detect subselects if this paranthesis is directly followed by 
					 * a "select" token
					*/
					if (tokText == "(")
					{
						openingParan = new Paranthesis(tok.Text, false);
						_openingParanths.Push(openingParan);
					}
					else
					{
						openingParan = null;
					}


					/* Peek and remember the top select statment here, because we may pop top level select
					 * somewhere down here
					*/
					SqlStatement topSelect = null;
					if (selectStatments.Count > 0)
					{
						topSelect = selectStatments.Peek();
					}

					// We met a closing paranthesis. If we have a select stament in the stack and this
					// select is marked as a subselect we have to add this to the select statements list
					if (tokText == ")")
					{
						if (_openingParanths.Count == 0)
						{
							result.ParseErrors.Add("Unmatched paranthesis!");
							tmpParan = null;
						}
						else
						{
							tmpParan = _openingParanths.Pop();
						}

						/* Ok top level opening paranthesis in the stack is marked 
						 * as potentially preceding a select statment
						*/
						if (tmpParan != null && tmpParan.IsPreSelect)
						{
							// Do we really have a select waiting in the stack. If we have one we will
							// pop this select and add it to the list
							if (selectStatments.Count > 0)
							{
								SqlStatement stm = selectStatments.Pop();
								if (stm != null)
								{
									result.Statements.Add(stm);
								}
								stm.PushEndLocation(tok.Location);
								if (stm.ParentStatment != null)
								{
									stm.ParentStatment.PushStartLocation(tok.Location);
								}
							}
						}

						if (topSelect != null)
						{
							topSelect.AddTokenToHierarchy(tok.Text);
						}
					}

					/* Check if we met a token notifying potential select end.
					 * We also have to check if we have a select statement in the stack. If we have on
					 * we pop this stament and add it to the list
					*/
					else if (!elseOrEndConsumed && SqlKeywords.SelectEnds.Contains(tokText) && selectStatments.Count > 0)
					{
						SqlStatement stm = selectStatments.Pop();
						if (stm != null)
						{
							result.Statements.Add(stm);
							stm.PushEndLocation(prevToken.Location, -1 * tokText.Length);
						}
					}
					// We are not interested in the token
					else
					{
						if (topSelect != null)
						{
							topSelect.AddTokenToHierarchy(tok.Text);
						}
					}

					prevToken = tok;
					tok = tokenizer.RetrieveToken();
				}

				// We still have a select statement in the stack.
				if (selectStatments.Count > 0)
				{
					SqlStatement stm = selectStatments.Pop();
					if (stm != null)
					{
						result.Statements.Add(stm);
					}
          /*
          if (tok.Symbol.GetType() == typeof(SymbolEnd) && tok.Location.LineNr > 0 )
          {
            Location tmp = new Location(tok.Location.Position - 1, tok.Location.LineNr - 1, tok.Location.ColumnNr);
            stm.PushEndLocation(tmp);
          }
          else
          {
            stm.PushEndLocation(tok.Location);
          }
          */

          stm.PushEndLocation(tok.Location);
          if (stm.ParentStatment != null)
					{
						stm.ParentStatment.PushStartLocation(tok.Location);
					}
				}
			}
			catch (Exception ex)
			{
				result.Exceptions.Add(ex.Message);
			}
			return result;
		}
	}
}
