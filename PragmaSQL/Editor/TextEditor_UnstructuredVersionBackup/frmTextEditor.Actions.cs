/********************************************************************
  Class      : frmScriptEditor
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Common;
using PragmaSQL.Database;
using Crad.Windows.Forms.Actions;

namespace PragmaSQL
{
  /// <summary>
  /// Action initialization part of the script editor.
  /// </summary>
  partial class frmTextEditor
  {
    private IDictionary<ScriptEditorActions,Action> _actionKeys = new Dictionary<ScriptEditorActions,Action>();

    private void InitiailizeActions( )
    {
      InitializeActions_FileOperations();
      InitializeActions_EditorOperations();
      InitializeActions_SearchAndReplaceOperations();
      InitializeActions_ScriptFormatOperations();
      InitializeActions_ScriptEditOperations();
    }


    #region File Actions
    private void InitializeActions_FileOperations( )
    {
      #region  Open file action
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.open;
      ac.Execute += new EventHandler(OnAction_OpenFile_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Open);
      _actionKeys.Add(ScriptEditorActions.Open,ac);

      ac.Text = "Open File";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemOpen, ac);
      _actionList.SetAction(btnOpen, ac);
      #endregion

      #region Open New Editor from File
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OpenNewFromFile);
      _actionKeys.Add(ScriptEditorActions.OpenNewFromFile,ac);

      ac.Execute += new EventHandler(OnAction_OpenFromFile_Execute);
      ac.Text = "New From File";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuScriptFromFile, ac);

      #endregion

      #region Save file action
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.save;
      ac.Execute += new EventHandler(OnAction_SaveFile_Execute);
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Save);
      _actionKeys.Add(ScriptEditorActions.Save,ac);
      ac.Text = "Save File";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemSave, ac);
      _actionList.SetAction(btnSave, ac);
      _actionList.SetAction(cMnuItemSave, ac);

      #endregion

      #region Save file as action
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.SaveAs;
      ac.Execute += new EventHandler(OnAction_SaveFileAs_Execute);
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SaveAs);
      _actionKeys.Add(ScriptEditorActions.SaveAs,ac);
      ac.Text = "Save File As";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemSaveAs, ac);
      _actionList.SetAction(btnSaveAs, ac);

      #endregion
    }

    private void OnAction_OpenFromFile_Execute( object sender, EventArgs e )
    {
      NodeData data = Program.MainForm.GetCurrentSelectedNodeDataFromObjectExplorer();
      if (data == null)
      {
        return;
      }
      frmScriptEditor editor = ScriptEditorFactory.OpenFile(String.Empty, data);
      ScriptEditorFactory.ShowScriptEditor(editor);
    }

    private void OnAction_OpenFile_Execute( object sender, EventArgs e )
    {
      OpenFile(String.Empty);
    }

    private void OnAction_SaveFile_Execute( object sender, EventArgs e )
    {
      SaveContent();
    }

    private void OnAction_SaveFileAs_Execute( object sender, EventArgs e )
    {
      SaveContentAs();
    }

    #endregion

    #region Script editor Actions
    private void InitializeActions_EditorOperations( )
    {
      #region  New script
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.new1;
      ac.Execute += new EventHandler(OnAction_NewScript_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.NewScript);
      _actionKeys.Add(ScriptEditorActions.NewScript,ac);
      ac.Text = "New Script";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuNewScript, ac);
      #endregion

      #region  Close
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_Close_Execute);
      ac.Text = "Close";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuItemClose, ac);

      #endregion

      #region Close All
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_CloseAll_Execute);
      ac.Text = "Close All";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuCloseAll, ac);

      #endregion

      #region  Close All But This
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_CloseAllButThis_Execute);
      ac.Text = "Close All But This";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuCloseAllButThis, ac);

      #endregion
    }

    private void OnAction_NewScript_Execute( object sender, EventArgs e )
    {
      NodeData data = Program.MainForm.GetCurrentSelectedNodeDataFromObjectExplorer();
      if (data == null)
      {
        return;
      }

      frmScriptEditor editor = ScriptEditorFactory.Create(data);
      ScriptEditorFactory.ShowScriptEditor(editor);
    }

    private void OnAction_Close_Execute( object sender, EventArgs e )
    {
      Close();
    }

    private void OnAction_CloseAll_Execute( object sender, EventArgs e )
    {
      if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, null) == DialogResult.Cancel)
      {
        return;
      }
      Program.MainForm.CloseDocuments(null);
    }

    private void OnAction_CloseAllButThis_Execute( object sender, EventArgs e )
    {
      if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, this) == DialogResult.Cancel)
      {
        return;
      }
      Program.MainForm.CloseDocuments(this);
    }

    #endregion


    #region Search And Replace
    private void InitializeActions_SearchAndReplaceOperations( )
    {
      #region Quick search forward
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.down;
      ac.Execute += new EventHandler(OnAction_QuickSearchForward_Execute);
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SearchForward);
      _actionKeys.Add(ScriptEditorActions.SearchForward,ac);
      ac.Text = "Search Forward";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnFindNext, ac);
      _actionList.SetAction(mnuItemSearchForward, ac);

      #endregion

      #region Quick search backward
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.up;
      ac.Execute += new EventHandler(OnAction_QuickSearchBackward_Execute);
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SearchBackward);
      _actionKeys.Add(ScriptEditorActions.SearchBackward,ac);
      ac.Text = "Search Backwards";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnFindPrev, ac);
      _actionList.SetAction(mnuItemSearchBackward, ac);
      #endregion

      #region GoTo Line
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.GoToLine);
      _actionKeys.Add(ScriptEditorActions.GoToLine,ac);
      ac.Execute += new EventHandler(OnAction_GoToLine_Execute);
      ac.Text = "Go To Line";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemGoToLine, ac);
      #endregion

      #region Find
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Find);
      _actionKeys.Add(ScriptEditorActions.Find,ac);
      ac.Execute += new EventHandler(OnAction_Find_Execute);
      ac.Text = "Find";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemFind, ac);
      #endregion

      #region Replace
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Replace);
      _actionKeys.Add(ScriptEditorActions.Replace,ac);
      ac.Execute += new EventHandler(OnAction_Replace_Execute);
      ac.Text = "Replace";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemReplace, ac);
      #endregion
    }

    private void OnAction_QuickSearchForward_Execute( object sender, EventArgs e )
    {
      MatchNext(edtMatchText.Text);
    }

    private void OnAction_QuickSearchBackward_Execute( object sender, EventArgs e )
    {
      MatchPrev(edtMatchText.Text);
    }

    private void OnAction_GoToLine_Execute( object sender, EventArgs e )
    {
      ShowGoToLineDialog();
    }

    private void OnAction_Find_Execute( object sender, EventArgs e )
    {
      ShowSearchDialog();
    }

    private void OnAction_Replace_Execute( object sender, EventArgs e )
    {
      ShowReplaceDialog();
    }

    #endregion

    #region Script Formatting Actions
    private void InitializeActions_ScriptFormatOperations( )
    {
      #region Outdent
      Action ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OutdentSelection);
      _actionKeys.Add(ScriptEditorActions.OutdentSelection,ac);
      ac.Image = global::PragmaSQL.Properties.Resources.IndentRTL;
      ac.Execute += new EventHandler(OnAction_OutdentSelection_Execute);
      ac.Text = "Outdent selection";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnOutDent, ac);

      #endregion

      #region Indent
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.IndentSelection);
      _actionKeys.Add(ScriptEditorActions.IndentSelection,ac);
      ac.Image = global::PragmaSQL.Properties.Resources.Indent;
      ac.Execute += new EventHandler(OnAction_IndentSelection_Execute);
      ac.Text = "Outdent selection";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnIndent, ac);

      #endregion

      #region Keywords to uppercase
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.KeywordsToUppercase);
      _actionKeys.Add(ScriptEditorActions.KeywordsToUppercase,ac);
      ac.Image = global::PragmaSQL.Properties.Resources.font_increase;
      ac.Execute += new EventHandler(OnAction_KeywordsToUppercase_Execute);
      ac.Text = "Keywords To Uppercase ";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnKeywordsToUppercase, ac);
      _actionList.SetAction(mnuItemKeywordsToUppercase, ac);

      #endregion

      #region Keywords to lowercase
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.KeywordsToLowercase);
      _actionKeys.Add(ScriptEditorActions.KeywordsToLowercase,ac);
      ac.Image = global::PragmaSQL.Properties.Resources.font_decrease;
      ac.Execute += new EventHandler(OnAction_KeywordsToLowercase_Execute);
      ac.Text = "Keywords To Lowercase ";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnKeywordsToLowercase, ac);
      _actionList.SetAction(mnuItemKeywordsToLowercase, ac);

      #endregion

      #region Capitalize keywords
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.CapitalizeKeywords);
      _actionKeys.Add(ScriptEditorActions.CapitalizeKeywords,ac);
      ac.Image = global::PragmaSQL.Properties.Resources.font_capitalize;
      ac.Execute += new EventHandler(OnAction_CapitalizeKeywords_Execute);
      ac.Text = "Captalize Keywords";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnCapitalizeKeywords, ac);
      _actionList.SetAction(mnuItemCapitilizeKeywords, ac);

      #endregion

      #region Script to uppercase
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ScriptToUppercase);
      _actionKeys.Add(ScriptEditorActions.ScriptToUppercase,ac);
      ac.Execute += new EventHandler(OnAction_ScriptToUppercase_Execute);
      ac.Text = "Script To Uppercase";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemScriptToUppercase, ac);

      #endregion

      #region Script to lowercase
      ac = new Action();
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ScriptToLowercase);
      _actionKeys.Add(ScriptEditorActions.ScriptToLowercase,ac);
      ac.Execute += new EventHandler(OnAction_ScriptToLowercase_Execute);
      ac.Text = "Script To Lowercase";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemScriptToLowercase, ac);

      #endregion
    }

    private void OnAction_IndentSelection_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.Tab().Execute(ActiveTextArea);
    }

    private void OnAction_OutdentSelection_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.ShiftTab().Execute(ActiveTextArea);
    }

    private void OnAction_KeywordsToUppercase_Execute( object sender, EventArgs e )
    {
      ConvertTokensTo(TokenConversionType.Upper);
    }

    private void OnAction_KeywordsToLowercase_Execute( object sender, EventArgs e )
    {
      ConvertTokensTo(TokenConversionType.Lower);
    }

    private void OnAction_CapitalizeKeywords_Execute( object sender, EventArgs e )
    {
      ConvertTokensTo(TokenConversionType.Capitalize);
    }

    private void OnAction_ScriptToUppercase_Execute( object sender, EventArgs e )
    {
      ChangeScriptCase(TokenConversionType.Upper);
    }

    private void OnAction_ScriptToLowercase_Execute( object sender, EventArgs e )
    {
      ChangeScriptCase(TokenConversionType.Lower);
    }

    #endregion

    #region Text editor edit actions
    private void InitializeActions_ScriptEditOperations( )
    {
      #region Undo
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.undo;
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Undo);
      _actionKeys.Add(ScriptEditorActions.Undo,ac);
      ac.Update += new EventHandler(OnAction_TextEditor_Update);
      ac.Execute += new EventHandler(OnAction_Undo_Execute);

      ac.Text = "Undo";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemUndo, ac);
      _actionList.SetAction(tsMnuItemUndo, ac);

      #endregion

      #region Redo
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.redo;
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Redo);
      _actionKeys.Add(ScriptEditorActions.Redo,ac);
      ac.Update += new EventHandler(OnAction_TextEditor_Update);
      ac.Execute += new EventHandler(OnAction_Redo_Execute);

      ac.Text = "Redo";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemRedo, ac);
      _actionList.SetAction(tsMnuItemRedo, ac);

      #endregion

      #region Cut
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.cut_2;
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Cut);
      _actionKeys.Add(ScriptEditorActions.Cut,ac);
      ac.Update += new EventHandler(OnAction_TextEditor_Update);
      ac.Execute += new EventHandler(OnAction_Cut_Execute);

      ac.Text = "Cut";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCut, ac);
      _actionList.SetAction(tsMnuItemCut, ac);

      #endregion

      #region Copy
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.copy;
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Copy);
      _actionKeys.Add(ScriptEditorActions.Copy,ac);
      ac.Update += new EventHandler(OnAction_TextEditor_Update);
      ac.Execute += new EventHandler(OnAction_Copy_Execute);

      ac.Text = "Copy";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCopy, ac);
      _actionList.SetAction(tsMnuItemCopy, ac);

      #endregion

      #region Paste
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.copy;
      ac.ShortcutKeys =ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Paste);
      _actionKeys.Add(ScriptEditorActions.Paste,ac);
      ac.Update += new EventHandler(OnAction_TextEditor_Update);
      ac.Execute += new EventHandler(OnAction_Paste_Execute);

      ac.Text = "Paste";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemPaste, ac);
      _actionList.SetAction(tsMnuItemPaste, ac);

      #endregion

    }

    private void OnAction_TextEditor_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      if(ac == null)
      {
        return;
      }
      ac.Enabled = ActiveTextArea.Focused;
    }
    
    private void OnAction_Undo_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.Undo().Execute(ActiveTextArea);
    }

    private void OnAction_Redo_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.Redo().Execute(ActiveTextArea);
    }

    private void OnAction_Cut_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.Cut().Execute(ActiveTextArea);
    }

    private void OnAction_Copy_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.Copy().Execute(ActiveTextArea);
    }

    private void OnAction_Paste_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.Paste().Execute(ActiveTextArea);
    }


    #endregion

  } // Class end
} // Namespace end
