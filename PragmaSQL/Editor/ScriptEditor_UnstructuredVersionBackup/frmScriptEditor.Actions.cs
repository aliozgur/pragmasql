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


using Crad.Windows.Forms.Actions;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

using PragmaSQL.Common;
using PragmaSQL.Database;


namespace PragmaSQL
{
  /// <summary>
  /// Action initialization part of the script editor.
  /// </summary>
  partial class frmScriptEditor
  {
    private IDictionary<ScriptEditorActions, Action> _actionKeys = new Dictionary<ScriptEditorActions, Action>();

    private void InitiailizeActions( )
    {
      InitializeActions_FileOperations();
      InitializeActions_EditorOperations();
      InitializeActions_ExecuteScriptOperations();
      InitializeActions_SearchAndReplaceOperations();
      InitializeActions_ScriptFormatOperations();
      InitializeActions_ScriptEditOperations();
      InitializeActions_ObjectHelpOperations();
      InitializeActions_WordAtCursorHelp();
      InitializeActions_FoldingOperations();
    }

    private void OnAction_Generic_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    #region File Actions
    private void InitializeActions_FileOperations( )
    {
      #region  Open file action
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.open;
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_OpenFile_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Open);
      _actionKeys.Add(ScriptEditorActions.Open, ac);

      ac.Text = "Open Script";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemOpen, ac);
      _actionList.SetAction(btnOpen, ac);
      #endregion

      #region Open New Editor from File
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OpenNewFromFile);
      _actionKeys.Add(ScriptEditorActions.OpenNewFromFile, ac);

      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_OpenFromFile_Execute);
      ac.Text = "New Script From File";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuScriptFromFile, ac);

      #endregion

      #region Save file action
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.save;
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_SaveFile_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Save);
      _actionKeys.Add(ScriptEditorActions.Save, ac);
      ac.Text = "Save Script";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemSave, ac);
      _actionList.SetAction(btnSave, ac);
      _actionList.SetAction(cMnuItemSave, ac);

      #endregion

      #region Save file as action
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.SaveAs;
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_SaveFileAs_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SaveAs);
      _actionKeys.Add(ScriptEditorActions.SaveAs, ac);
      ac.Text = "Save Script As";
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
      _actionKeys.Add(ScriptEditorActions.NewScript, ac);
      ac.Text = "New Script";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuNewScript, ac);
      #endregion

      #region  Close
      ac = new Action();
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_Close_Execute);
      ac.Text = "Close";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuItemClose, ac);

      #endregion

      #region Close All
      ac = new Action();
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_CloseAll_Execute);
      ac.Text = "Close All";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuCloseAll, ac);

      #endregion

      #region  Close All But This
      ac = new Action();
      ac.Update +=new EventHandler(OnAction_Generic_Update);
      ac.Execute += new EventHandler(OnAction_CloseAllButThis_Execute);
      ac.Text = "Close All But This";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(cMnuCloseAllButThis, ac);

      #endregion

      #region Toggle output pane
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_ToggleOutputPane_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ToggleOutputPane);
      _actionKeys.Add(ScriptEditorActions.ToggleOutputPane, ac);
      ac.Text = "Toggle Output Pane";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemToggleOutputPane, ac);

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

    private void OnAction_ToggleOutputPane_Execute( object sender, EventArgs e )
    {
      OutputPaneVisible = !OutputPaneVisible;
    }
    #endregion

    #region Script Execution
    private void InitializeActions_ExecuteScriptOperations( )
    {
      #region Run Script
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.Run;
      ac.Update +=new EventHandler(OnAction_RunScript_Update);
      ac.Execute += new EventHandler(OnAction_RunScript_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Execute);
      _actionKeys.Add(ScriptEditorActions.Execute, ac);
      ac.Text = "Execute";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnRun, ac);
      _actionList.SetAction(mnuItemRun, ac);

      #endregion

      #region Check syntax
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.correct;
      ac.Update +=new EventHandler(OnAction_CheckSyntax_Update);
      ac.Execute += new EventHandler(OnAction_CheckSyntax_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.CheckSyntax);
      _actionKeys.Add(ScriptEditorActions.CheckSyntax, ac);
      ac.Text = "Check Syntax";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnCheckSyntax, ac);
      _actionList.SetAction(mnuItemCheckSyntax, ac);
      #endregion

      #region Show Plan
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.gear_1;
      ac.Update +=new EventHandler(OnAction_ShowPlan_Update);
      ac.Execute += new EventHandler(OnAction_ShowPlan_Execute);
      ac.Text = "Show Plan";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnShowPlan, ac);
      _actionList.SetAction(mnuItemShowPlan, ac);
      #endregion

      #region Stop execution
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.Stop;
      ac.Update +=new EventHandler(OnAction_StopExecution_Update);
      ac.Execute += new EventHandler(OnAction_StopExecution_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Stop);
      _actionKeys.Add(ScriptEditorActions.Stop, ac);
      ac.Text = "Stop";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnStop, ac);
      _actionList.SetAction(mnuItemStop, ac);

      #endregion
    }

    private void OnAction_RunScript_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    private void OnAction_RunScript_Execute( object sender, EventArgs e )
    {
      ExecScript(RunType.Execute);
    }

    private void OnAction_CheckSyntax_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    private void OnAction_CheckSyntax_Execute( object sender, EventArgs e )
    {
      ExecScript(RunType.CheckSyntax);
    }

    private void OnAction_ShowPlan_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = !_isExecuting;
    }

    private void OnAction_ShowPlan_Execute( object sender, EventArgs e )
    {
      ExecScript(RunType.ShowPlan);
    }

    private void OnAction_StopExecution_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = _isExecuting;
    }

    private void OnAction_StopExecution_Execute( object sender, EventArgs e )
    {
      CancelScriptExecution();
    }


    #endregion

    #region Search And Replace
    private void InitializeActions_SearchAndReplaceOperations( )
    {
      #region Quick search forward
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.down;
      ac.Execute += new EventHandler(OnAction_QuickSearchForward_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SearchForward);
      _actionKeys.Add(ScriptEditorActions.SearchForward, ac);
      ac.Text = "Search Forward";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnFindNext, ac);
      _actionList.SetAction(mnuItemSearchForward, ac);

      #endregion

      #region Quick search backward
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.up;
      ac.Execute += new EventHandler(OnAction_QuickSearchBackward_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SearchBackward);
      _actionKeys.Add(ScriptEditorActions.SearchBackward, ac);
      ac.Text = "Search Backwards";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnFindPrev, ac);
      _actionList.SetAction(mnuItemSearchBackward, ac);
      #endregion

      #region GoTo Line
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.GoToLine);
      _actionKeys.Add(ScriptEditorActions.GoToLine, ac);
      ac.Execute += new EventHandler(OnAction_GoToLine_Execute);
      ac.Text = "Go To Line";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemGoToLine, ac);
      #endregion

      #region Find
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Find);
      _actionKeys.Add(ScriptEditorActions.Find, ac);
      ac.Execute += new EventHandler(OnAction_Find_Execute);
      ac.Text = "Find";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemFind, ac);
      #endregion

      #region Replace
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Replace);
      _actionKeys.Add(ScriptEditorActions.Replace, ac);
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
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OutdentSelection);
      _actionKeys.Add(ScriptEditorActions.OutdentSelection, ac);
      ac.Image = global::PragmaSQL.Properties.Resources.IndentRTL;
      ac.Execute += new EventHandler(OnAction_OutdentSelection_Execute);
      ac.Text = "Outdent selection";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnOutDent, ac);

      #endregion

      #region Indent
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.IndentSelection);
      _actionKeys.Add(ScriptEditorActions.IndentSelection, ac);
      ac.Image = global::PragmaSQL.Properties.Resources.Indent;
      ac.Execute += new EventHandler(OnAction_IndentSelection_Execute);
      ac.Text = "Outdent selection";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnIndent, ac);

      #endregion

      #region Keywords to uppercase
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.KeywordsToUppercase);
      _actionKeys.Add(ScriptEditorActions.KeywordsToUppercase, ac);
      ac.Image = global::PragmaSQL.Properties.Resources.font_increase;
      ac.Execute += new EventHandler(OnAction_KeywordsToUppercase_Execute);
      ac.Text = "Keywords To Uppercase ";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnKeywordsToUppercase, ac);
      _actionList.SetAction(mnuItemKeywordsToUppercase, ac);

      #endregion

      #region Keywords to lowercase
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.KeywordsToLowercase);
      _actionKeys.Add(ScriptEditorActions.KeywordsToLowercase, ac);
      ac.Image = global::PragmaSQL.Properties.Resources.font_decrease;
      ac.Execute += new EventHandler(OnAction_KeywordsToLowercase_Execute);
      ac.Text = "Keywords To Lowercase ";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnKeywordsToLowercase, ac);
      _actionList.SetAction(mnuItemKeywordsToLowercase, ac);

      #endregion

      #region Capitalize keywords
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.CapitalizeKeywords);
      _actionKeys.Add(ScriptEditorActions.CapitalizeKeywords, ac);
      ac.Image = global::PragmaSQL.Properties.Resources.font_capitalize;
      ac.Execute += new EventHandler(OnAction_CapitalizeKeywords_Execute);
      ac.Text = "Captalize Keywords";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(btnCapitalizeKeywords, ac);
      _actionList.SetAction(mnuItemCapitilizeKeywords, ac);

      #endregion

      #region Script to uppercase
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ScriptToUppercase);
      _actionKeys.Add(ScriptEditorActions.ScriptToUppercase, ac);
      ac.Execute += new EventHandler(OnAction_ScriptToUppercase_Execute);
      ac.Text = "Script To Uppercase";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemScriptToUppercase, ac);

      #endregion

      #region Script to lowercase
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ScriptToLowercase);
      _actionKeys.Add(ScriptEditorActions.ScriptToLowercase, ac);
      ac.Execute += new EventHandler(OnAction_ScriptToLowercase_Execute);
      ac.Text = "Script To Lowercase";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemScriptToLowercase, ac);

      #endregion

      #region Mark selection as code block
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.MarkSelectionAsCodeBlock);
      _actionKeys.Add(ScriptEditorActions.MarkSelectionAsCodeBlock, ac);
      ac.Update += new EventHandler(OnAction_MarkSelAsCodeBlock_Update);
      ac.Execute += new EventHandler(OnAction_MarkSelAsCodeBlock_Execute);
      ac.Text = "Mark Selection As Code Block";
      _actionList.Actions.Add(ac);

      _actionList.SetAction(mnuItemMarkSelAsCodeBlock, ac);
      _actionList.SetAction(tsMnuItemMarkSelAsCodeBlock, ac);

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

    private void OnAction_MarkSelAsCodeBlock_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      if(ac == null)
      {
        return;
      }

      ac.Enabled = ( ActiveTextArea != null && ActiveTextArea.SelectionManager.HasSomethingSelected);
    }

    private void OnAction_MarkSelAsCodeBlock_Execute( object sender, EventArgs e )
    {
      MarkSelectionAsCodeBlock();
    }

    #endregion

    #region Text editor edit actions
    private void InitializeActions_ScriptEditOperations( )
    {
      #region Undo
      Action ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.undo;
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Undo);
      _actionKeys.Add(ScriptEditorActions.Undo, ac);
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
      _actionKeys.Add(ScriptEditorActions.Redo, ac);
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
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Cut);
      _actionKeys.Add(ScriptEditorActions.Cut, ac);
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
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Copy);
      _actionKeys.Add(ScriptEditorActions.Copy, ac);
      ac.Update += new EventHandler(OnAction_Copy_Update);
      ac.Execute += new EventHandler(OnAction_Copy_Execute);

      ac.Text = "Copy";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCopy, ac);
      _actionList.SetAction(tsMnuItemCopy, ac);

      #endregion

      #region Paste
      ac = new Action();
      ac.Image = global::PragmaSQL.Properties.Resources.copy;
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Paste);
      _actionKeys.Add(ScriptEditorActions.Paste, ac);
      ac.Update += new EventHandler(OnAction_TextEditor_Update);
      ac.Execute += new EventHandler(OnAction_Paste_Execute);

      ac.Text = "Paste";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemPaste, ac);
      _actionList.SetAction(tsMnuItemPaste, ac);

      #endregion

      #region Copy grid to clipboard
      ac = new Action();
      ac.Execute += new EventHandler(OnAction_CopyGridToClipboard_Execute);
      ac.Text = "Copy To Clipboard";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(popUpItemCopyGridToClipboard, ac);
      #endregion
    }

    private void OnAction_TextEditor_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      if(ac == null)
      {
        return;
      }

      ac.Enabled = ActiveTextArea != null && ActiveTextArea.Focused;
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

    private void OnAction_Copy_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      if(ac == null)
      {
        return;
      }

      if((ActiveTextArea != null && ActiveTextArea.Focused) || IsResultGridFocused())
      {
        ac.Enabled = true;
      }
      else
      {
        ac.Enabled = false;
      }
    }
    
    private void OnAction_Copy_Execute( object sender, EventArgs e )
    {
      if (ActiveTextArea != null && ActiveTextArea.Focused)
      {
        new ICSharpCode.TextEditor.Actions.Copy().Execute(ActiveTextArea);
      }
      else
      {
        CopyGridContentToClipboard();
      }
    }

    private void OnAction_Paste_Execute( object sender, EventArgs e )
    {
      new ICSharpCode.TextEditor.Actions.Paste().Execute(ActiveTextArea);
    }


    private void OnAction_CopyGridToClipboard_Execute( object sender, EventArgs e )
    {
      CopyGridContentToClipboard();
    }

    #endregion

    #region Object help actions
    private void InitializeActions_ObjectHelpOperations( )
    {
      #region Modify Object
      Action ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ModifySelectedObject);
      _actionKeys.Add(ScriptEditorActions.ModifySelectedObject, ac);
      //ac.Update += new EventHandler(OnAction_ModifySelectedObject_Update);
      ac.Execute += new EventHandler(OnAction_ModifySelectedObject_Execute);

      ac.Text = "Modify";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemModifySelObject, ac);


      #endregion

      #region Execute Stored Proc
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OpenProcExecScript);
      _actionKeys.Add(ScriptEditorActions.OpenProcExecScript, ac);
      ac.Execute += new EventHandler(OnAction_CreateExecScriptForSelectedProc_Execute);

      ac.Text = "Execute";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemExecProc, ac);


      #endregion

      #region Open Object
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OpenSelectedObject);
      _actionKeys.Add(ScriptEditorActions.OpenSelectedObject, ac);
      ac.Execute += new EventHandler(OnAction_OpenSelectedObject_Execute);
      ac.Text = "Open";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemOpenSelObject, ac);

      #endregion


      #region Permissions
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ListPermissions);
      _actionKeys.Add(ScriptEditorActions.ListPermissions, ac);
      ac.Execute += new EventHandler(OnAction_ListPermissions_Execute);
      ac.Text = "Permissions";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemListPermissions, ac);

      #endregion

      #region References
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ListReferences);
      _actionKeys.Add(ScriptEditorActions.ListReferences, ac);
      ac.Execute += new EventHandler(OnAction_ListReferences_Execute);
      ac.Text = "References";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemListReferences, ac);

      #endregion

      #region Dependencies
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ListDependencies);
      _actionKeys.Add(ScriptEditorActions.ListDependencies, ac);
      ac.Execute += new EventHandler(OnAction_ListDependencies_Execute);
      ac.Text = "Dependencies";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemListDependencies, ac);

      #endregion

      #region Object Help
      ac = new Action();
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ObjectHelp);
      _actionKeys.Add(ScriptEditorActions.ObjectHelp, ac);
      ac.Execute += new EventHandler(OnAction_ObjectHelp_Execute);
      ac.Text = "Object Help";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemObjectHelp, ac);

      #endregion
    }

    private void OnAction_ModifySelectedObject_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = (_textEditor != null &&_textEditor.Focused);
    }

    private void OnAction_ModifySelectedObject_Execute( object sender, EventArgs e )
    {
      ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
      if ((objInfo == null) || (!DBConstants.DoesObjectTypeHasScript(objInfo.ObjectTypeAbb)))
      {
        MessageBox.Show("Object is not a procedure or function!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      OpenObjectScripInNewEditor(objInfo);
    }


    private void OnAction_CreateExecScriptForSelectedProc_Execute( object sender, EventArgs e )
    {
      ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
      if ((objInfo == null) || (objInfo.ObjectType != DBObjectType.StoredProc))
      {
        MessageBox.Show("Object is not a procedure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      OpenExecuteScriptInNewEditor(objInfo);
    }

    private void OnAction_OpenSelectedObject_Execute( object sender, EventArgs e )
    {
      ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
      if ((objInfo == null) || (!DBConstants.DoesObjectTypeHoldsData(objInfo.ObjectTypeAbb)))
      {
        MessageBox.Show("Object is not a view or table!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
      LoadTableOrViewData(objInfo);
    }


    private void OnAction_ListPermissions_Execute( object sender, EventArgs e )
    {
      ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
      if (objInfo == null)
      {
        return;
      }

      switch (objInfo.ObjectType)
      {
        case DBObjectType.StoredProc:
        case DBObjectType.TableValuedFunction:
        case DBObjectType.ScalarValuedFunction:
          ExecuteObjectHelp(ActionType.Permissions, objInfo);
          break;
        case DBObjectType.SystemTable:
        case DBObjectType.UserTable:
          ExecuteObjectHelp(ActionType.TablePermissions, objInfo);
          break;
        default:
          break;
      }
    }


    private void OnAction_ListReferences_Execute( object sender, EventArgs e )
    {
      ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
      if (objInfo == null)
      {
        return;
      }
      ShowReferences(objInfo);
    }


    private void OnAction_ListDependencies_Execute( object sender, EventArgs e )
    {
      ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
      if (objInfo == null)
      {
        return;
      }
      ExecuteObjectHelp(ActionType.Dependencies, objInfo);
    }


    private void OnAction_ObjectHelp_Execute( object sender, EventArgs e )
    {
      ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
      if (objInfo == null)
      {
        ClearOutputPane();
        return;
      }
      ExecuteObjectHelp(ActionType.ObjectHelp, objInfo);

    }

    #endregion

    #region WordAtCursor Help

    private void InitializeActions_WordAtCursorHelp( )
    {
      Action ac = new Action();
      ac.Execute += new EventHandler(OnAction_ShowHelpOnWordAtCursor_Execute);
      ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.HelpOnWordAtCursor);
      ac.Text = "Help On WordAtCursor";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemHelpOnWordAtCursor, ac);
    }

    private void OnAction_ShowHelpOnWordAtCursor_Execute( object sender, EventArgs e )
    {
      this.ShowHelpOnWordAtCursor();
    }

    #endregion

    #region Folding Operations

    private void InitializeActions_FoldingOperations( )
    {
      Action ac = new Action();
      ac.Update += new EventHandler(OnAction_FoldingsEnabled_Update);
      ac.Execute += new EventHandler(OnAction_CollapseAllFoldings_Execute);
      ac.Text = "Collapse All";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCollapseAllFoldings, ac);
      _actionList.SetAction(tsMnuItemCollapseFoldings, ac);

      ac = new Action();
      ac.Update += new EventHandler(OnAction_FoldingsEnabled_Update);
      ac.Execute += new EventHandler(OnAction_ExpandAllFoldings_Execute);
      ac.Text = "Expand All";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemExpandAllFoldings, ac);
      _actionList.SetAction(tsMnuItemExpandFoldings, ac);

      ac = new Action();
      ac.Update += new EventHandler(OnAction_FoldingsEnabled_Update);
      ac.Execute += new EventHandler(OnAction_ToggleFoldings_Execute);
      ac.Text = "Toggle";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemToggleFoldings, ac);
      _actionList.SetAction(tsMnuItemToggleFoldings, ac);
    }

    private void OnAction_FoldingsEnabled_Update( object sender, EventArgs e )
    {
      Action ac = sender as Action;
      ac.Enabled = ( ActiveTextEditorProps != null ) && (ActiveTextEditorProps.EnableFolding);
    }

    private void OnAction_CollapseAllFoldings_Execute( object sender, EventArgs e )
    {
      try
      {
        ActiveTextArea.BeginUpdate();
        foreach (FoldMarker marker in ActiveDocument.FoldingManager.FoldMarker)
        {
          marker.IsFolded = true;
        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
        ActiveTextArea.Invalidate();
      }
    }

    private void OnAction_ExpandAllFoldings_Execute( object sender, EventArgs e )
    {
      try
      {
        ActiveTextArea.BeginUpdate();
        foreach (FoldMarker marker in ActiveDocument.FoldingManager.FoldMarker)
        {
          marker.IsFolded = false;
        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
        ActiveTextArea.Invalidate();
      }
    }

    private void OnAction_ToggleFoldings_Execute( object sender, EventArgs e )
    {
      try
      {
        ActiveTextArea.BeginUpdate();
        foreach (FoldMarker marker in ActiveDocument.FoldingManager.FoldMarker)
        {
          marker.IsFolded = !marker.IsFolded;
        }
      }
      finally
      {
        ActiveTextArea.EndUpdate();
        ActiveTextArea.Invalidate();
      }
    }

    #endregion

  } // Class end
} // Namespace end
