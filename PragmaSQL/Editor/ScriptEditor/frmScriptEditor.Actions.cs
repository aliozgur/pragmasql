/********************************************************************
  Class      : frmScriptEditor
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using Crad.Windows.Forms.Actions;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Util;
using ICSharpCode.TextEditor.Document;

using PragmaSQL.Core;


namespace PragmaSQL
{
    /// <summary>
    /// Action initialization part of the script editor.
    /// </summary>
    partial class frmScriptEditor
    {
        private IDictionary<ScriptEditorActions, Crad.Windows.Forms.Actions.Action> _actionKeys = new Dictionary<ScriptEditorActions, Crad.Windows.Forms.Actions.Action>();

        private void InitiailizeActions()
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

        private void OnAction_Generic_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = !_isExecuting && !base.ReadOnly;

        }

        #region File Actions
        private void InitializeActions_FileOperations()
        {
            #region  Open file action
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.open;
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Execute += new EventHandler(OnAction_OpenFile_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Open);
            _actionKeys.Add(ScriptEditorActions.Open, ac);

            ac.Text = "Load From File";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemOpen, ac);
            _actionList.SetAction(btnOpen, ac);
            #endregion

            #region Open New Editor from File
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OpenNewFromFile);
            _actionKeys.Add(ScriptEditorActions.OpenNewFromFile, ac);

            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Execute += new EventHandler(OnAction_OpenFromFile_Execute);
            ac.Text = "Open File In New Editor";
            ac.Image = Properties.Resources.folder_page_white;
            _actionList.Actions.Add(ac);

            _actionList.SetAction(cMnuScriptFromFile, ac);

            #endregion

            #region Save file action
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.save;
            ac.Update += new EventHandler(OnAction_Generic_Update);
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
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.SaveAs;
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Execute += new EventHandler(OnAction_SaveFileAs_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SaveAs);
            _actionKeys.Add(ScriptEditorActions.SaveAs, ac);
            ac.Text = "Save Script As";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(mnuItemSaveAs, ac);
            _actionList.SetAction(btnSaveAs, ac);

            #endregion

            #region Shared Script Operation

            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.generic;
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Shared Script Operation";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemSharedScriptOperations, ac);

            #endregion Shared Script Operation


        }

        private void OnAction_OpenFromFile_Execute(object sender, EventArgs e)
        {
            NodeData data = Program.MainForm.GetCurrentSelectedNodeDataFromObjectExplorer();
            if (data == null)
            {
                return;
            }
            frmScriptEditor editor = ScriptEditorFactory.OpenFile(String.Empty, data);
            ScriptEditorFactory.ShowScriptEditor(editor);
        }

        private void OnAction_OpenFile_Execute(object sender, EventArgs e)
        {
            OpenFile(String.Empty);
        }

        private void OnAction_SaveFile_Execute(object sender, EventArgs e)
        {
            SaveContent();
        }

        private void OnAction_SaveFileAs_Execute(object sender, EventArgs e)
        {
            SaveContentAs();
        }

        #endregion

        #region Script editor Actions
        private void InitializeActions_EditorOperations()
        {
            #region  New script
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.new1;
            ac.Execute += new EventHandler(OnAction_NewScript_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.NewScript);
            _actionKeys.Add(ScriptEditorActions.NewScript, ac);
            ac.Text = "New Script";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(cMnuNewScript, ac);
            #endregion

            #region  Close
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Execute += new EventHandler(OnAction_Close_Execute);
            ac.Text = "Close";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(cMnuItemClose, ac);

            #endregion

            #region Close All
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Execute += new EventHandler(OnAction_CloseAll_Execute);
            ac.Text = "Close All";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(cMnuCloseAll, ac);

            #endregion

            #region  Close All But This
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Execute += new EventHandler(OnAction_CloseAllButThis_Execute);
            ac.Text = "Close All But This";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(cMnuCloseAllButThis, ac);

            #endregion

            #region Toggle output pane
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Execute += new EventHandler(OnAction_ToggleOutputPane_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ToggleOutputPane);
            _actionKeys.Add(ScriptEditorActions.ToggleOutputPane, ac);
            ac.Text = "Toggle Output Pane";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(mnuItemToggleOutputPane, ac);

            #endregion

        }

        private void OnAction_NewScript_Execute(object sender, EventArgs e)
        {
            NodeData data = Program.MainForm.GetCurrentSelectedNodeDataFromObjectExplorer();
            if (data == null)
            {
                return;
            }

            frmScriptEditor editor = ScriptEditorFactory.Create(data);
            ScriptEditorFactory.ShowScriptEditor(editor);
        }

        private void OnAction_Close_Execute(object sender, EventArgs e)
        {
            Close();
        }

        private void OnAction_CloseAll_Execute(object sender, EventArgs e)
        {
            if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, null) == DialogResult.Cancel)
            {
                return;
            }
            Program.MainForm.CloseDocuments(null);
        }

        private void OnAction_CloseAllButThis_Execute(object sender, EventArgs e)
        {
            if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, this) == DialogResult.Cancel)
            {
                return;
            }
            Program.MainForm.CloseDocuments(this);
        }

        private void OnAction_ToggleOutputPane_Execute(object sender, EventArgs e)
        {
            OutputPaneVisible = !OutputPaneVisible;
        }


        #endregion

        #region Script Execution
        private void InitializeActions_ExecuteScriptOperations()
        {
            #region Run Script
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.Run;
            ac.Update += new EventHandler(OnAction_RunScript_Update);
            ac.Execute += new EventHandler(OnAction_RunScript_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Execute);
            _actionKeys.Add(ScriptEditorActions.Execute, ac);
            ac.Text = "Execute";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnRun, ac);
            _actionList.SetAction(mnuItemRun, ac);


            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.multi_exec;
            ac.Update += new EventHandler(OnAction_MultiExecScript_Update);
            ac.Execute += new EventHandler(OnAction_MultiExecScript_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.MultiExecute);
            _actionKeys.Add(ScriptEditorActions.MultiExecute, ac);
            ac.Text = "Multi Execute";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnMultiExec, ac);
            _actionList.SetAction(mnuItemMultiRun, ac);

            #endregion

            #region Check syntax
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.correct;
            ac.Update += new EventHandler(OnAction_CheckSyntax_Update);
            ac.Execute += new EventHandler(OnAction_CheckSyntax_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.CheckSyntax);
            _actionKeys.Add(ScriptEditorActions.CheckSyntax, ac);
            ac.Text = "Check Syntax";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnCheckSyntax, ac);
            _actionList.SetAction(mnuItemCheckSyntax, ac);
            #endregion

            #region Show Plan
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.gear_1;
            ac.Update += new EventHandler(OnAction_ShowPlan_Update);
            ac.Execute += new EventHandler(OnAction_ShowPlan_Execute);
            ac.Text = "Show Plan";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnShowPlan, ac);
            _actionList.SetAction(mnuItemShowPlan, ac);
            #endregion

            #region Stop execution
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = global::PragmaSQL.Properties.Resources.Stop;
            ac.Update += new EventHandler(OnAction_StopExecution_Update);
            ac.Execute += new EventHandler(OnAction_StopExecution_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Stop);
            _actionKeys.Add(ScriptEditorActions.Stop, ac);
            ac.Text = "Stop";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnStop, ac);
            _actionList.SetAction(mnuItemStop, ac);

            #endregion
        }

        private void OnAction_RunScript_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = !_isExecuting && !base.ReadOnly;
        }

        private void OnAction_RunScript_Execute(object sender, EventArgs e)
        {
            ExecScript(ScriptRunType.Execute);
        }


        private void OnAction_MultiExecScript_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = !_isExecuting && !base.ReadOnly;

        }

        private void OnAction_MultiExecScript_Execute(object sender, EventArgs e)
        {
            ExecuteScriptInMultiDb();
        }



        private void OnAction_CheckSyntax_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = !_isExecuting && !base.ReadOnly;
        }

        private void OnAction_CheckSyntax_Execute(object sender, EventArgs e)
        {
            ExecScript(ScriptRunType.CheckSyntax);
        }

        private void OnAction_ShowPlan_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = !_isExecuting && !base.ReadOnly;
        }

        private void OnAction_ShowPlan_Execute(object sender, EventArgs e)
        {
            ExecScript(ScriptRunType.ShowPlan);
        }

        private void OnAction_StopExecution_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = _isExecuting && !base.ReadOnly;
        }

        private void OnAction_StopExecution_Execute(object sender, EventArgs e)
        {
            CancelScriptExecution();
        }


        #endregion

        #region Search And Replace
        private void InitializeActions_SearchAndReplaceOperations()
        {
            #region Quick search forward
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
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
            ac = new Crad.Windows.Forms.Actions.Action();
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
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.GoToLine);
            _actionKeys.Add(ScriptEditorActions.GoToLine, ac);
            ac.Execute += new EventHandler(OnAction_GoToLine_Execute);
            ac.Text = "Go To Line";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemGoToLine, ac);
            #endregion

            #region Find
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Find);
            _actionKeys.Add(ScriptEditorActions.Find, ac);
            ac.Execute += new EventHandler(OnAction_Find_Execute);
            ac.Text = "Find";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemFind, ac);
            #endregion

            #region Replace
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.Replace);
            _actionKeys.Add(ScriptEditorActions.Replace, ac);
            ac.Execute += new EventHandler(OnAction_Replace_Execute);
            ac.Text = "Replace";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemReplace, ac);
            #endregion

            #region Incremental Search
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.IncrementalSearch);
            _actionKeys.Add(ScriptEditorActions.IncrementalSearch, ac);
            ac.Execute += new EventHandler(OnAction_IncrementalSearch_Execute);
            ac.Text = "Incremental Search";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemIncSearch, ac);

            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ReverseIncrementalSearch);
            _actionKeys.Add(ScriptEditorActions.ReverseIncrementalSearch, ac);
            ac.Execute += new EventHandler(OnAction_ReverseIncrementalSearch_Execute);
            ac.Text = "Reverse Incremental Search";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemRevIncSearch, ac);

            #endregion //Incremental Search

        }

        private void OnAction_QuickSearchForward_Execute(object sender, EventArgs e)
        {
            if (_fwdIncSearch != null)
            {
                edtMatchText.Text = _fwdIncSearch.SearchText;
                DisposeIncSearchObjects();
            }

            MatchNext(SearchTerm);
        }

        private void OnAction_QuickSearchBackward_Execute(object sender, EventArgs e)
        {
            if (_revIncSearch != null)
            {
                edtMatchText.Text = _revIncSearch.SearchText;
                DisposeIncSearchObjects();
            }

            MatchPrev(SearchTerm);
        }

        private void OnAction_GoToLine_Execute(object sender, EventArgs e)
        {
            ShowGoToLineDialog();
        }

        private void OnAction_Find_Execute(object sender, EventArgs e)
        {
            ShowSearchDialog();
        }

        private void OnAction_Replace_Execute(object sender, EventArgs e)
        {
            ShowReplaceDialog();
        }

        private IncrementalSearch _fwdIncSearch;
        private void OnAction_IncrementalSearch_Execute(object sender, EventArgs e)
        {
            statIncSearch.Text = String.Empty;
            if (_revIncSearch != null)
            {
                DisposeIncrementalSearchEvents(_revIncSearch);
                _revIncSearch.Dispose();
                _revIncSearch = null;
            }

            if (_fwdIncSearch != null)
            {
                DisposeIncrementalSearchEvents(_fwdIncSearch);
                _fwdIncSearch.Dispose();
                _fwdIncSearch = null;
                return;
            }

            _fwdIncSearch = new IncrementalSearch(_textEditor, true);
            _fwdIncSearch.OnIncrementalSearchMessage += new IncrmentalSearchMessageDelegate(OnIncrementalSearchMessage);
            _fwdIncSearch.OnIncrementalSearchStopped += new EventHandler(OnIncrementalSearchStopped);
        }


        private IncrementalSearch _revIncSearch;
        private void OnAction_ReverseIncrementalSearch_Execute(object sender, EventArgs e)
        {
            statIncSearch.Text = String.Empty;
            if (_fwdIncSearch != null)
            {
                DisposeIncrementalSearchEvents(_fwdIncSearch);
                _fwdIncSearch.Dispose();
                _fwdIncSearch = null;
            }

            if (_revIncSearch != null)
            {
                DisposeIncrementalSearchEvents(_revIncSearch);
                _revIncSearch.Dispose();
                _revIncSearch = null;
                return;
            }


            _revIncSearch = new IncrementalSearch(_textEditor, false);
            _revIncSearch.OnIncrementalSearchMessage += new IncrmentalSearchMessageDelegate(OnIncrementalSearchMessage);
            _revIncSearch.OnIncrementalSearchStopped += new EventHandler(OnIncrementalSearchStopped);
        }

        private void OnIncrementalSearchMessage(string msg, bool highlight)
        {
            statIncSearch.Text = msg;
        }

        private void OnIncrementalSearchStopped(object sender, EventArgs e)
        {
            if (sender == _revIncSearch)
            {
                DisposeIncrementalSearchEvents(_revIncSearch);
                _revIncSearch = null;
            }
            else if (sender == _fwdIncSearch)
            {
                DisposeIncrementalSearchEvents(_fwdIncSearch);
                _fwdIncSearch = null;
            }

            statIncSearch.Text = String.Empty;
        }

        private void DisposeIncSearchObjects()
        {
            if (_fwdIncSearch != null)
            {
                DisposeIncrementalSearchEvents(_fwdIncSearch);
                _fwdIncSearch.Dispose();
                _fwdIncSearch = null;
            }

            if (_revIncSearch != null)
            {
                DisposeIncrementalSearchEvents(_revIncSearch);
                _revIncSearch.Dispose();
                _revIncSearch = null;
            }
        }

        private void DisposeIncrementalSearchEvents(IncrementalSearch incSearch)
        {
            if (incSearch == null)
                return;
            incSearch.OnIncrementalSearchMessage += new IncrmentalSearchMessageDelegate(OnIncrementalSearchMessage);
            incSearch.OnIncrementalSearchStopped += new EventHandler(OnIncrementalSearchStopped);
        }

        #endregion

        #region Script Formatting Actions
        private void InitializeActions_ScriptFormatOperations()
        {
            #region Outdent
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OutdentSelection);
            _actionKeys.Add(ScriptEditorActions.OutdentSelection, ac);
            ac.Image = global::PragmaSQL.Properties.Resources.IndentRTL;
            ac.Execute += new EventHandler(OnAction_OutdentSelection_Execute);
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Outdent selection";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnOutDent, ac);

            #endregion

            #region Indent
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.IndentSelection);
            _actionKeys.Add(ScriptEditorActions.IndentSelection, ac);
            ac.Image = global::PragmaSQL.Properties.Resources.Indent;
            ac.Execute += new EventHandler(OnAction_IndentSelection_Execute);
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Outdent selection";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnIndent, ac);

            #endregion

            #region Keywords to uppercase
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.KeywordsToUppercase);
            _actionKeys.Add(ScriptEditorActions.KeywordsToUppercase, ac);
            ac.Image = global::PragmaSQL.Properties.Resources.font_increase;
            ac.Execute += new EventHandler(OnAction_KeywordsToUppercase_Execute);
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Keywords To Uppercase ";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnKeywordsToUppercase, ac);
            _actionList.SetAction(mnuItemKeywordsToUppercase, ac);

            #endregion

            #region Keywords to lowercase
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.KeywordsToLowercase);
            _actionKeys.Add(ScriptEditorActions.KeywordsToLowercase, ac);
            ac.Image = global::PragmaSQL.Properties.Resources.font_decrease;
            ac.Execute += new EventHandler(OnAction_KeywordsToLowercase_Execute);
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Keywords To Lowercase ";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnKeywordsToLowercase, ac);
            _actionList.SetAction(mnuItemKeywordsToLowercase, ac);

            #endregion

            #region Capitalize keywords
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.CapitalizeKeywords);
            _actionKeys.Add(ScriptEditorActions.CapitalizeKeywords, ac);
            ac.Image = global::PragmaSQL.Properties.Resources.font_capitalize;
            ac.Execute += new EventHandler(OnAction_CapitalizeKeywords_Execute);
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Captalize Keywords";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(btnCapitalizeKeywords, ac);
            _actionList.SetAction(mnuItemCapitilizeKeywords, ac);

            #endregion

            #region Script to uppercase
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ScriptToUppercase);
            _actionKeys.Add(ScriptEditorActions.ScriptToUppercase, ac);
            ac.Execute += new EventHandler(OnAction_ScriptToUppercase_Execute);
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Script To Uppercase";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(mnuItemScriptToUppercase, ac);

            #endregion

            #region Script to lowercase
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ScriptToLowercase);
            _actionKeys.Add(ScriptEditorActions.ScriptToLowercase, ac);
            ac.Execute += new EventHandler(OnAction_ScriptToLowercase_Execute);
            ac.Update += new EventHandler(OnAction_Generic_Update);
            ac.Text = "Script To Lowercase";
            _actionList.Actions.Add(ac);

            _actionList.SetAction(mnuItemScriptToLowercase, ac);

            #endregion

            #region Mark selection as code block
            ac = new Crad.Windows.Forms.Actions.Action();
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

        private void OnAction_IndentSelection_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.Tab().Execute(ActiveTextArea);
        }

        private void OnAction_OutdentSelection_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.ShiftTab().Execute(ActiveTextArea);
        }

        private void OnAction_KeywordsToUppercase_Execute(object sender, EventArgs e)
        {
            ConvertTokensTo(TokenConversionType.Upper);
        }

        private void OnAction_KeywordsToLowercase_Execute(object sender, EventArgs e)
        {
            ConvertTokensTo(TokenConversionType.Lower);
        }

        private void OnAction_CapitalizeKeywords_Execute(object sender, EventArgs e)
        {
            ConvertTokensTo(TokenConversionType.Capitalize);
        }

        private void OnAction_ScriptToUppercase_Execute(object sender, EventArgs e)
        {
            ChangeScriptCase(TokenConversionType.Upper);
        }

        private void OnAction_ScriptToLowercase_Execute(object sender, EventArgs e)
        {
            ChangeScriptCase(TokenConversionType.Lower);
        }

        private void OnAction_MarkSelAsCodeBlock_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            if (ac == null)
            {
                return;
            }

            ac.Enabled = !base.ReadOnly && (ActiveTextArea != null && ActiveTextArea.SelectionManager.HasSomethingSelected);
        }

        private void OnAction_MarkSelAsCodeBlock_Execute(object sender, EventArgs e)
        {
            MarkSelectionAsCodeBlock();
        }

        #endregion

        #region Text editor edit actions
        private void InitializeActions_ScriptEditOperations()
        {
            #region Undo
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
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
            ac = new Crad.Windows.Forms.Actions.Action();
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
            ac = new Crad.Windows.Forms.Actions.Action();
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
            ac = new Crad.Windows.Forms.Actions.Action();
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
            ac = new Crad.Windows.Forms.Actions.Action();
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


            #region Block Comment
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Execute += new EventHandler(OnAction_ToggleBlockComment_Execute);
            ac.Update += new EventHandler(OnAction_TextEditor_Update);
            ac.Image = Properties.Resources.CommentOut;
            ac.Text = "Toggle Block Comment";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(btnToggleBlockComment, ac);

            #endregion //Block Comment

            #region Line Comment
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Image = Properties.Resources.CommentOut_Line;
            ac.Execute += new EventHandler(OnAction_ToggleLineComment_Execute);
            ac.Update += new EventHandler(OnAction_TextEditor_Update);
            ac.Text = "Toggle Line Comment";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(btnToggleLineComment, ac);
            #endregion //Line Comment

        }

        private void OnAction_TextEditor_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            if (ac == null)
            {
                return;
            }

            ac.Enabled = !base.ReadOnly && ActiveTextArea != null && ActiveTextArea.Focused;
        }

        private void OnAction_Undo_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.Undo().Execute(ActiveTextArea);
        }

        private void OnAction_Redo_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.Redo().Execute(ActiveTextArea);
        }

        private void OnAction_Cut_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.Cut().Execute(ActiveTextArea);
        }

        private void OnAction_Copy_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            if (ac == null)
            {
                return;
            }
            ac.Enabled = !base.ReadOnly && (ActiveTextArea != null && ActiveTextArea.Focused);
        }

        private void OnAction_Copy_Execute(object sender, EventArgs e)
        {
            if (ActiveTextArea != null && ActiveTextArea.Focused)
            {
                new ICSharpCode.TextEditor.Actions.Copy().Execute(ActiveTextArea);
            }
        }

        private void OnAction_Paste_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.Paste().Execute(ActiveTextArea);
        }

        private void OnAction_ToggleBlockComment_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.ToggleBlockComment().Execute(ActiveTextArea);
        }

        private void OnAction_ToggleLineComment_Execute(object sender, EventArgs e)
        {
            new ICSharpCode.TextEditor.Actions.ToggleLineComment().Execute(ActiveTextArea);
        }


        #endregion

        #region Object help actions
        private void InitializeActions_ObjectHelpOperations()
        {
            #region Modify Object
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ModifySelectedObject);
            _actionKeys.Add(ScriptEditorActions.ModifySelectedObject, ac);
            //ac.Update += new EventHandler(OnAction_ModifySelectedObject_Update);
            ac.Execute += new EventHandler(OnAction_ModifySelectedObject_Execute);

            ac.Text = "Modify";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemModifySelObject, ac);


            #endregion

            #region Execute Stored Proc
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OpenProcExecScript);
            _actionKeys.Add(ScriptEditorActions.OpenProcExecScript, ac);
            ac.Execute += new EventHandler(OnAction_CreateExecScriptForSelectedProc_Execute);

            ac.Text = "Execute";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemExecProc, ac);


            #endregion


            #region Select top 100 Rows
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.SelectTop100Rows);
            _actionKeys.Add(ScriptEditorActions.SelectTop100Rows, ac);
            ac.Execute += new EventHandler(OnAction_SelectTop100Rows);

            ac.Text = "Select Top 100 Rows";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemSelectTop100SelObject, ac);


            #endregion

            #region Open Object
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.OpenSelectedObject);
            _actionKeys.Add(ScriptEditorActions.OpenSelectedObject, ac);
            ac.Execute += new EventHandler(OnAction_OpenSelectedObject_Execute);
            ac.Text = "Open";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemOpenSelObject, ac);

            #endregion

            #region Object change history
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ObjectChangeHistory);
            _actionKeys.Add(ScriptEditorActions.ObjectChangeHistory, ac);
            ac.Execute += new EventHandler(OnAction_ShowObjectChangeHist_Execute);

            ac.Text = "Object Change History";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemObjectChangeHist, ac);


            #endregion

            #region Permissions
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ListPermissions);
            _actionKeys.Add(ScriptEditorActions.ListPermissions, ac);
            ac.Execute += new EventHandler(OnAction_ListPermissions_Execute);
            ac.Text = "Permissions";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemListPermissions, ac);

            #endregion

            #region References
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ListReferences);
            _actionKeys.Add(ScriptEditorActions.ListReferences, ac);
            ac.Execute += new EventHandler(OnAction_ListReferences_Execute);
            ac.Text = "References";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemListReferences, ac);

            #endregion

            #region Dependencies
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ListDependencies);
            _actionKeys.Add(ScriptEditorActions.ListDependencies, ac);
            ac.Execute += new EventHandler(OnAction_ListDependencies_Execute);
            ac.Text = "Dependencies";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemListDependencies, ac);

            #endregion

            #region Object Help
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.ObjectHelp);
            _actionKeys.Add(ScriptEditorActions.ObjectHelp, ac);
            ac.Execute += new EventHandler(OnAction_ObjectHelp_Execute);
            ac.Text = "Object Help";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemObjectHelp, ac);

            #endregion

            #region Fast Script Preview
            ac = new Crad.Windows.Forms.Actions.Action();
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.FastScriptPreview);
            _actionKeys.Add(ScriptEditorActions.FastScriptPreview, ac);
            ac.Execute += new EventHandler(OnAction_FastScriptPreview_Execute);
            ac.Text = "Fast Script Preview";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemFastScriptPreview, ac);
            #endregion //Fast Script Preview
        }

        private void OnAction_ModifySelectedObject_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = !base.ReadOnly && (_textEditor != null && _textEditor.Focused);
        }

        private void OnAction_ModifySelectedObject_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if ((objInfo == null) || (!DBConstants.DoesObjectTypeHasScript(objInfo.ObjectTypeAbb)))
            {
                MessageBox.Show("Object is not a procedure or function!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenObjectScripInNewEditor(objInfo);
        }


        private void OnAction_CreateExecScriptForSelectedProc_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if ((objInfo == null) || (objInfo.ObjectType != DBObjectType.StoredProc))
            {
                MessageBox.Show("Object is not a procedure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenExecuteScriptInNewEditor(objInfo);
        }

        private void OnAction_SelectTop100Rows(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if ((objInfo == null) || (!DBConstants.DoesObjectTypeHoldsData(objInfo.ObjectTypeAbb)))
            {
                MessageBox.Show("Object is not a table or view!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var script = $"SELECT TOP 100 * FROM {objInfo.FullNameQuoted}";
            ExecScript(script,ScriptRunType.Execute,0,false,false);
        }


        private void OnAction_OpenSelectedObject_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if ((objInfo == null) || (!DBConstants.DoesObjectTypeHoldsData(objInfo.ObjectTypeAbb)))
            {
                MessageBox.Show("Object is not a view or table!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LoadTableOrViewData(objInfo);
        }


        private void OnAction_ShowObjectChangeHist_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if ((objInfo == null) || (!DBConstants.DoesObjectTypeHasScript(objInfo.ObjectTypeAbb)))
            {
                MessageBox.Show("Object does not have script!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowObjectChangeHistory(objInfo);
        }

        private void OnAction_ListPermissions_Execute(object sender, EventArgs e)
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


        private void OnAction_ListReferences_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if (objInfo == null)
            {
                return;
            }
            ShowReferences(objInfo);
        }


        private void OnAction_ListDependencies_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if (objInfo == null)
            {
                return;
            }
            ExecuteObjectHelp(ActionType.Dependencies, objInfo);
        }


        private void OnAction_ObjectHelp_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if (objInfo == null)
            {
                ClearOutputPane();
                return;
            }

            ExecuteObjectHelp(ActionType.ObjectHelp, objInfo);

        }

        private void OnAction_FastScriptPreview_Execute(object sender, EventArgs e)
        {
            ObjectInfo objInfo = GetObjectInfoForWordAtCursor();
            if (objInfo == null)
            {
                return;
            }

            ShowFastScriptPreview(objInfo);
        }

        #endregion

        #region WordAtCursor Help

        private void InitializeActions_WordAtCursorHelp()
        {
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
            ac.Execute += new EventHandler(OnAction_ShowHelpOnWordAtCursor_Execute);
            ac.ShortcutKeys = ScriptEditorShortcutKeysProvider.GetShortCut(ScriptEditorActions.HelpOnWordAtCursor);
            ac.Text = "Help On WordAtCursor";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemHelpOnWordAtCursor, ac);
        }

        private void OnAction_ShowHelpOnWordAtCursor_Execute(object sender, EventArgs e)
        {
            this.ShowHelpOnWordAtCursor();
        }

        #endregion

        #region Folding Operations

        private void InitializeActions_FoldingOperations()
        {
            Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
            ac.Update += new EventHandler(OnAction_FoldingsEnabled_Update);
            ac.Execute += new EventHandler(OnAction_CollapseAllFoldings_Execute);
            ac.Text = "Collapse All";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemCollapseAllFoldings, ac);
            _actionList.SetAction(tsMnuItemCollapseFoldings, ac);

            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Update += new EventHandler(OnAction_FoldingsEnabled_Update);
            ac.Execute += new EventHandler(OnAction_ExpandAllFoldings_Execute);
            ac.Text = "Expand All";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemExpandAllFoldings, ac);
            _actionList.SetAction(tsMnuItemExpandFoldings, ac);

            ac = new Crad.Windows.Forms.Actions.Action();
            ac.Update += new EventHandler(OnAction_FoldingsEnabled_Update);
            ac.Execute += new EventHandler(OnAction_ToggleFoldings_Execute);
            ac.Text = "Toggle";
            _actionList.Actions.Add(ac);
            _actionList.SetAction(mnuItemToggleFoldings, ac);
            _actionList.SetAction(tsMnuItemToggleFoldings, ac);
        }

        private void OnAction_FoldingsEnabled_Update(object sender, EventArgs e)
        {
            Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
            ac.Enabled = (ActiveTextEditorProps != null) && (ActiveTextEditorProps.EnableFolding);
        }

        private void OnAction_CollapseAllFoldings_Execute(object sender, EventArgs e)
        {
            CollapseAllFoldings();
        }


        private void OnAction_ExpandAllFoldings_Execute(object sender, EventArgs e)
        {
            ExpandAllFoldings();
        }


        private void OnAction_ToggleFoldings_Execute(object sender, EventArgs e)
        {
            ToggleFoldings();
        }

        #endregion

    } // Class end
} // Namespace end
