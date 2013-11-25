/********************************************************************
  Class      : EditorShortCuts
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using System.Reflection;


namespace PragmaSQL.Core
{
  

  public static class ScriptEditorShortcutKeysProvider
  {
    #region Default shortcuts
    private static Keys defExecute = Keys.F5;
    public static Keys DefExecute
    {
      get { return ScriptEditorShortcutKeysProvider.defExecute; }
    }

		private static Keys defMultiExecute = Keys.Control | Keys.Shift | Keys.F5;
		public static Keys DefMultiExecute
		{
			get { return ScriptEditorShortcutKeysProvider.defMultiExecute; }
		}
		
		private static Keys defCheckSyntax = Keys.Control | Keys.F5;
    public static Keys DefCheckSyntax
    {
      get { return ScriptEditorShortcutKeysProvider.defCheckSyntax; }
    }

    private static Keys defStop = Keys.Control | Keys.F2;
    public static Keys DefStop
    {
      get { return ScriptEditorShortcutKeysProvider.defStop; }
    }

    private static Keys defToggleOutputPane = Keys.Control | Keys.R;
    public static Keys DefToggleOutputPane
    {
      get { return ScriptEditorShortcutKeysProvider.defToggleOutputPane; }
    }

    private static Keys defSearchForward = Keys.F3;
    public static Keys DefSearchForward
    {
      get { return ScriptEditorShortcutKeysProvider.defSearchForward; }
    }

    private static Keys defSearchBackward = Keys.Control | Keys.F3;
    public static Keys DefSearchBackward
    {
      get { return ScriptEditorShortcutKeysProvider.defSearchBackward; }
    }

    private static Keys defFind = Keys.Control | Keys.F;
    public static Keys DefFind
    {
      get { return ScriptEditorShortcutKeysProvider.defFind; }
    }

    private static Keys defReplace = Keys.Control | Keys.H;
    public static Keys DefReplace
    {
      get { return ScriptEditorShortcutKeysProvider.defReplace; }
    }

    private static Keys defGoToLine = Keys.Control | Keys.G;
    public static Keys DefGoToLine
    {
      get { return ScriptEditorShortcutKeysProvider.defGoToLine; }
    }

    private static Keys defKeywordsToUppercase = Keys.Control | Keys.Shift | Keys.U;
    public static Keys DefKeywordsToUppercase
    {
      get { return ScriptEditorShortcutKeysProvider.defKeywordsToUppercase; }
    }

    private static Keys defKeywordsToLowercase = Keys.Control | Keys.Shift | Keys.L;
    public static Keys DefKeywordsToLowercase
    {
      get { return ScriptEditorShortcutKeysProvider.defKeywordsToLowercase; }
    }

    private static Keys defCapitalizeKeywords = Keys.Control | Keys.Shift | Keys.C;
    public static Keys DefCapitalizeKeywords
    {
      get { return ScriptEditorShortcutKeysProvider.defCapitalizeKeywords; }
    }

    private static Keys defScriptToUppercase = Keys.Control | Keys.U;
    public static Keys DefScriptToUppercase
    {
      get { return ScriptEditorShortcutKeysProvider.defScriptToUppercase; }
    }

    private static Keys defScriptToLowercase = Keys.Control | Keys.L;
    public static Keys DefScriptToLowercase
    {
      get { return ScriptEditorShortcutKeysProvider.defScriptToLowercase; }
    }

    private static Keys defUndo = Keys.Control | Keys.Z;
    public static Keys DefUndo
    {
      get { return ScriptEditorShortcutKeysProvider.defUndo; }
    }

    private static Keys defRedo = Keys.Control | Keys.Y;
    public static Keys DefRedo
    {
      get { return ScriptEditorShortcutKeysProvider.defRedo; }
    }

    private static Keys defCut = Keys.Control | Keys.X;
    public static Keys DefCut
    {
      get { return ScriptEditorShortcutKeysProvider.defCut; }
    }

    private static Keys defCopy = Keys.Control | Keys.C;
    public static Keys DefCopy
    {
      get { return ScriptEditorShortcutKeysProvider.defCopy; }
    }

    private static Keys defPaste = Keys.Control | Keys.V;
    public static Keys DefPaste
    {
      get { return ScriptEditorShortcutKeysProvider.defPaste; }
    }

    private static Keys defSave = Keys.Control | Keys.S;
    public static Keys DefSave
    {
      get { return ScriptEditorShortcutKeysProvider.defSave; }
    }

    private static Keys defSaveAs = Keys.Control | Keys.Shift | Keys.S;
    public static Keys DefSaveAs
    {
      get { return ScriptEditorShortcutKeysProvider.defSaveAs; }
    }

    private static Keys defOpen = Keys.Control | Keys.O;
    public static Keys DefOpen
    {
      get { return ScriptEditorShortcutKeysProvider.defOpen; }
    }

    private static Keys defOpenNewFromFile = Keys.Control | Keys.Shift | Keys.O;
    public static Keys DefOpenNewFromFile
    {
      get { return ScriptEditorShortcutKeysProvider.defOpenNewFromFile; }
    }

    private static Keys defNewScript = Keys.Control | Keys.N;
    public static Keys DefNewScript
    {
      get { return ScriptEditorShortcutKeysProvider.defNewScript; }
    }

    private static Keys defOutdentSelection = Keys.Shift | Keys.Tab;
    public static Keys DefOutdentSelection
    {
      get { return ScriptEditorShortcutKeysProvider.defOutdentSelection; }
    }

    private static Keys defIndentSelection = Keys.Tab;
    public static Keys DefIndentSelection
    {
      get { return ScriptEditorShortcutKeysProvider.defIndentSelection; }
    }

    private static Keys defCommentBlock = Keys.Control | Keys.E | Keys.B;
    public static Keys DefCommentBlock
    {
      get { return ScriptEditorShortcutKeysProvider.defCommentBlock; }
    }

    private static Keys defCommentLine = Keys.Control | Keys.E | Keys.L;
    public static Keys DefCommentLine
    {
      get { return ScriptEditorShortcutKeysProvider.defCommentLine; }
    }

    private static Keys defModifySelectedObject = Keys.F2;
    public static Keys DefModifySelectedObject
    {
      get { return ScriptEditorShortcutKeysProvider.defModifySelectedObject; }
    }

    private static Keys defOpenSelectedObject = Keys.Control | Keys.Shift | Keys.F2;
    public static Keys DefOpenSelectedObject
    {
      get { return ScriptEditorShortcutKeysProvider.defOpenSelectedObject; }
    }

    private static Keys defOpenProcExecScript = Keys.Shift | Keys.F2;
    public static Keys DefOpenProcExecScript
    {
      get { return ScriptEditorShortcutKeysProvider.defOpenProcExecScript; }
    }

    private static Keys defListPermissions = Keys.Control | Keys.Shift | Keys.P;
    public static Keys DefListPermissions
    {
      get { return ScriptEditorShortcutKeysProvider.defListPermissions; }
    }

    private static Keys defListReferences = Keys.Control | Keys.Shift | Keys.R;
    public static Keys DefListReferences
    {
      get { return ScriptEditorShortcutKeysProvider.defListReferences; }
    }

    private static Keys defListDependencies = Keys.Control | Keys.Shift | Keys.D;
    public static Keys DefListDependencies
    {
      get { return ScriptEditorShortcutKeysProvider.defListDependencies; }
    }

    private static Keys defObjectHelp = Keys.Alt | Keys.F1;
    public static Keys DefObjectHelp
    {
      get { return ScriptEditorShortcutKeysProvider.defObjectHelp; }
    }

		private static Keys defFastScriptPreview = Keys.Alt | Keys.F2;
		public static Keys DefFastScriptPreview
		{
			get { return ScriptEditorShortcutKeysProvider.defFastScriptPreview; }
		}


    private static Keys defHelpOnWordAtCursor = Keys.F1;
    public static Keys DefHelpOnWordAtCursor
    {
      get { return ScriptEditorShortcutKeysProvider.defHelpOnWordAtCursor; }
    }
    
    private static Keys defMarkSelectionAsCodeBlock = Keys.Alt | Keys.Shift | Keys.B;
    public static Keys DefMarkSelectionAsCodeBlock
    {
      get { return ScriptEditorShortcutKeysProvider.defMarkSelectionAsCodeBlock; }
    }

    private static Keys defObjectChangeHistory = Keys.Control | Keys.Shift | Keys.H;
    public static Keys DefObjectChangeHistory
    {
      get { return ScriptEditorShortcutKeysProvider.defObjectChangeHistory; }
    }

		private static Keys defIncrementalSearch = Keys.Control | Keys.I;
		public static Keys DefIncrementalSearch
		{
			get { return ScriptEditorShortcutKeysProvider.defIncrementalSearch; }
		}

		private static Keys defReverseIncrementalSearch = Keys.Control | Keys.Shift | Keys.I;
		public static Keys DefReverseIncrementalSearch
		{
			get { return ScriptEditorShortcutKeysProvider.defReverseIncrementalSearch; }
		}

    #endregion

    public static Keys Execute = defExecute;
		public static Keys MultiExecute = defMultiExecute;

    public static Keys CheckSyntax = defCheckSyntax;
    public static Keys Stop = defStop;
    public static Keys ToggleOutputPane = defToggleOutputPane;
    public static Keys SearchForward = defSearchForward;
    public static Keys SearchBackward = defSearchBackward;
    public static Keys Find = defFind;
    public static Keys Replace = defReplace;
    public static Keys GoToLine = defGoToLine;
    public static Keys KeywordsToUppercase = defKeywordsToUppercase;
    public static Keys KeywordsToLowercase = defKeywordsToLowercase;
    public static Keys CapitalizeKeywords = defCapitalizeKeywords;

    public static Keys ScriptToUppercase = defScriptToUppercase;
    public static Keys ScriptToLowercase = defScriptToLowercase;

    public static Keys Undo = defUndo;
    public static Keys Redo = defRedo;

    public static Keys Cut = defCut;
    public static Keys Copy = defCopy;
    public static Keys Paste = defPaste;

    public static Keys Save = defSave;
    public static Keys SaveAs = defSaveAs;
    public static Keys Open = defOpen;
    public static Keys OpenNewFromFile = defOpenNewFromFile;
    public static Keys NewScript = defNewScript;

    public static Keys OutdentSelection = defOutdentSelection;
    public static Keys IndentSelection = defIndentSelection;

    public static Keys CommentBlock = defCommentBlock;
    public static Keys CommentLine = defCommentLine;

    public static Keys ModifySelectedObject = defModifySelectedObject;
    public static Keys OpenSelectedObject = defOpenSelectedObject;
    public static Keys OpenProcExecScript = defOpenProcExecScript;

    public static Keys ListPermissions = defListPermissions;
    public static Keys ListReferences = defListReferences;
    public static Keys ListDependencies = defListDependencies;
    public static Keys ObjectHelp = defObjectHelp;
		public static Keys FastScriptPreview = defFastScriptPreview;

    public static Keys HelpOnWordAtCursor = defHelpOnWordAtCursor;
    public static Keys MarkSelectionAsCodeBlock = defMarkSelectionAsCodeBlock;
    public static Keys ObjectChangeHistory = defObjectChangeHistory;
		
		public static Keys IncrementalSearch = defIncrementalSearch;
		public static Keys ReverseIncrementalSearch = defReverseIncrementalSearch;
    

    public static Keys GetShortCut( ScriptEditorActions action )
    {
      FieldInfo fi = typeof(ScriptEditorShortcutKeysProvider).GetField(Enum.GetName(action.GetType(), action));
      if (fi == null)
      {
        throw new InvalidOperationException(String.Format("Field \"{0}\" is not defined in ScriptEditorShortcutKeysProvider class", Enum.GetName(action.GetType(), action)));
      }
      return (Keys)fi.GetValue(null);
    }

    public static Keys GetDefaultShortCut( ScriptEditorActions action )
    {
      PropertyInfo pi = typeof(ScriptEditorShortcutKeysProvider).GetProperty("Def" + Enum.GetName(action.GetType(), action));
      if (pi == null)
      {
        throw new InvalidOperationException(String.Format("Property \"{0}\" is not defined in ScriptEditorShortcutKeysProvider class", "Def" + Enum.GetName(action.GetType(), action)));
      }
      return (Keys)pi.GetValue(null,null);
    }

    public static void SetShortCut( ScriptEditorActions action, Keys keys )
    {
      FieldInfo fi = typeof(ScriptEditorShortcutKeysProvider).GetField(Enum.GetName(action.GetType(), action));
      if (fi == null)
      {
        throw new InvalidOperationException(String.Format("Field \"{0}\" is not defined in ScriptEditorShortcutKeysProvider class", Enum.GetName(action.GetType(), action)));
      }
      fi.SetValue(null, keys);
    }

    public static string ShortcutKeysAsStringFromAction( ScriptEditorActions action )
    {
      string result = String.Empty;

      Keys keys = ScriptEditorShortcutKeysProvider.GetShortCut(action);
      Keys modKeys = Keys.Modifiers & keys;
      //Ctrl+Alt+Shift+M


      if ((Keys.Control & modKeys) == Keys.Control)
      {
        result += "Ctrl";
      }

      if ((Keys.Alt & modKeys) == Keys.Alt)
      {
        if (!String.IsNullOrEmpty(result))
        {
          result += "+Alt";
        }
        else
        {
          result += "Alt";
        }
      }

      if ((Keys.Shift & modKeys) == Keys.Shift)
      {
        if (!String.IsNullOrEmpty(result))
        {
          result += "+Shift";
        }
        else
        {
          result += "Shift";
        }
      }

      Keys key = Keys.KeyCode & keys;
      if (String.IsNullOrEmpty(result))
      {
        result += Enum.GetName(typeof(Keys), key);
      }
      else
      {
        result += "+" + Enum.GetName(typeof(Keys), key);
      }

      return result;
    }

    public static string ShortcutKeysAsStringFromKeys( Keys keys )
    {
      string result = String.Empty;

      Keys modKeys = Keys.Modifiers & keys;
      //Ctrl+Alt+Shift+M


      if ((Keys.Control & modKeys) == Keys.Control)
      {
        result += "Ctrl";
      }

      if ((Keys.Alt & modKeys) == Keys.Alt)
      {
        if (!String.IsNullOrEmpty(result))
        {
          result += "+Alt";
        }
        else
        {
          result += "Alt";
        }
      }

      if ((Keys.Shift & modKeys) == Keys.Shift)
      {
        if (!String.IsNullOrEmpty(result))
        {
          result += "+Shift";
        }
        else
        {
          result += "Shift";
        }
      }

      Keys key = Keys.KeyCode & keys;
      if (String.IsNullOrEmpty(result))
      {
        result += Enum.GetName(typeof(Keys), key);
      }
      else
      {
        result += "+" + Enum.GetName(typeof(Keys), key);
      }

      return result;
    }

    public static void RestoreDefaults( )
    {
      Execute = defExecute;
			MultiExecute = defMultiExecute;
			CheckSyntax = defCheckSyntax;
      Stop = defStop;
      ToggleOutputPane = defToggleOutputPane;
      SearchForward = defSearchForward;
      SearchBackward = defSearchBackward;
      Find = defFind;
      Replace = defReplace;
      GoToLine = defGoToLine;
      KeywordsToUppercase = defKeywordsToUppercase;
      KeywordsToLowercase = defKeywordsToLowercase;
      CapitalizeKeywords = defCapitalizeKeywords;

      ScriptToUppercase = defScriptToUppercase;
      ScriptToLowercase = defScriptToLowercase;

      Undo = defUndo;
      Redo = defRedo;

      Cut = defCut;
      Copy = defCopy;
      Paste = defPaste;

      Save = defSave;
      SaveAs = defSaveAs;
      Open = defOpen;
      OpenNewFromFile = defOpenNewFromFile;
      NewScript = defNewScript;

      OutdentSelection = defOutdentSelection;
      IndentSelection = defIndentSelection;

      CommentBlock = defCommentBlock;
      CommentLine = defCommentLine;

      ModifySelectedObject = defModifySelectedObject;
      OpenSelectedObject = defOpenSelectedObject;
      OpenProcExecScript = defOpenProcExecScript;

      ListPermissions = defListPermissions;
      ListReferences = defListReferences;
      ListDependencies = defListDependencies;
      ObjectHelp = defObjectHelp;
			FastScriptPreview = defFastScriptPreview;
      HelpOnWordAtCursor = defHelpOnWordAtCursor;
      MarkSelectionAsCodeBlock = defMarkSelectionAsCodeBlock;

			ObjectChangeHistory = defObjectChangeHistory;
			IncrementalSearch = defIncrementalSearch;
			ReverseIncrementalSearch = defReverseIncrementalSearch;
		}

    public static void LoadFromConfig(ConfigurationContent configContent)
    {
      if(configContent == null)
      {
        throw new NullParameterException("Configuration content parameter is null!");
      }
      
      foreach(ScriptEditorActions action in configContent.ScriptEditorShortcuts.Keys )
      {
        SetShortCut(action,configContent.ScriptEditorShortcuts[action]);
      }
    }

    public static void LoadFrom(SerializableDictionary<ScriptEditorActions, Keys> scriptEditorShortcuts)
    {
      foreach(ScriptEditorActions action in scriptEditorShortcuts.Keys )
      {
        SetShortCut(action,scriptEditorShortcuts[action]);
      }
    }
  }
}
