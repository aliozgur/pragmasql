﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ICSharpCode.TextEditor.Undo
{
	/// <summary>
	/// This class stacks the last x operations from the undostack and makes
	/// one undo/redo operation from it.
	/// </summary>
	public class UndoQueue : IUndoableOperation
	{
		List<IUndoableOperation> undolist = new List<IUndoableOperation>();
		
		/// <summary>
		/// </summary>
		public UndoQueue(UndoStack stack, int numops)
		{
			if (stack == null)  {
				throw new ArgumentNullException("stack");
			}
			
			Debug.Assert(numops > 0 , "ICSharpCode.TextEditor.Undo.UndoQueue : numops should be > 0");
			
			for (int i = 0; i < numops; ++i) {
				if (stack._UndoStack.Count > 0) {
					undolist.Add(stack._UndoStack.Pop());
				}
			}
		}
		public void Undo()
		{
			for (int i = 0; i < undolist.Count; ++i) {
				undolist[i].Undo();
			}
		}
		
		public void Redo()
		{
			for (int i = undolist.Count - 1 ; i >= 0 ; --i) {
				undolist[i].Redo();
			}
		}		
	}
}
