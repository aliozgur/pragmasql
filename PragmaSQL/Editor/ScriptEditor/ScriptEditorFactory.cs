/********************************************************************
  Class      : ScriptEditorFactory
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PragmaSQL.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace PragmaSQL
{
    internal static class ScriptEditorFactory
    {
        private static int _instanceCnt = 0;
        internal static WimdowNumerator Numerator = null;

        static ScriptEditorFactory()
        {
            Numerator = new WimdowNumerator();
        }

        internal static void ResetNumerator()
        {
            if (Numerator.WindowCount != 0)
                return;
            _instanceCnt = 0;
            Numerator.ClearReclaimedNumbers();
        }

        internal static void ShowScriptEditor(frmScriptEditor frm)
        {
            if (frm == null || frm.IsDisposed)
                return;

            if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                frm.MdiParent = Program.MainForm;
                frm.Show();
                frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
            else if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.DockingWindow)
            {
                frm.Show();
            }
            else
            {
                frm.Show(Program.MainForm.DockPanel);
            }
        }

        internal static void ShowScriptEditorIn(DockPanel dockPanel, frmScriptEditor frm)
        {
            if (frm == null)
            {
                return;
            }

            if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                frm.MdiParent = Program.MainForm;
                frm.Show();
            }
            else if (Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.DockingWindow)
            {
                frm.Show();
            }
            else
            {
                frm.Show(dockPanel);
            }
        }

        internal static frmScriptEditor Create(string caption, string script, NodeData data)
        {
            if (data == null)
            {
                throw new NullParameterException("NodeData paremeter is null!");
            }

            frmScriptEditor frm = null;
            string windowId = ScriptEditorManager.ProduceWindowId(caption, data.ID, data.Type, data.ConnParams.Server, data.DBName);
            frm = CheckEditorAlreadyExits(windowId, caption, script);
            if (frm != null)
                return frm;

            frm = new frmScriptEditor();
            frm.InitializeScriptEditor(caption, script, data.ID, data.Type, data.ConnParams, data.DBName);
            return frm;
        }

        internal static frmScriptEditor Create(NodeData data)
        {
            int? windowNo = Numerator.NextNumber;

            if (!windowNo.HasValue)
            {
                _instanceCnt++;
                windowNo = _instanceCnt;
            }

            string caption = String.Format("Script {0}", windowNo);
            frmScriptEditor editor = Create(caption, data.ConnParams);
            editor.WindowNo = windowNo;
            return editor;
        }

        internal static frmScriptEditor Create(string caption, string script, long objectId, int objectType, ConnectionParams cp, string dBName)
        {
            return Create(caption, script, objectId, objectType, cp, dBName, String.Empty);
        }

        internal static frmScriptEditor Create(string caption, string script, long objectId, int objectType, ConnectionParams cp, string dBName, string filePath)
        {
            if (cp == null)
            {
                throw new NullParameterException("ConnectionParams paremeter is null!");
            }

            frmScriptEditor frm = null;
            string windowId = ScriptEditorManager.ProduceWindowId(caption, objectId, objectType, cp.Server, dBName);
            frm = CheckEditorAlreadyExits(windowId, caption, script);
            if (frm != null)
                return frm;

            frm = new frmScriptEditor();
            frm.InitializeScriptEditor(caption, script, objectId, objectType, cp, dBName);
            if (!String.IsNullOrEmpty(filePath))
                frm.SetFilePath(filePath);
            return frm;
        }

        internal static frmScriptEditor Create(ObjectInfo objInfo, ConnectionParams cp, string dBName)
        {
            if (objInfo == null)
                throw new InvalidOperationException("ObjectInfo parameter is null!");

            if (cp == null)
                throw new NullParameterException("ConnectionParams paremeter is null!");


            ConnectionParams tmp = cp.CreateCopy();
            if (!String.IsNullOrEmpty(dBName))
                tmp.Database = dBName;

            string script = ScriptingHelper.GetAlterScript(tmp, objInfo.ObjectID, objInfo.ObjectType);
            string caption = objInfo.ObjectName;

            frmScriptEditor frm = null;
            string windowId = ScriptEditorManager.ProduceWindowId(objInfo.ObjectName, objInfo.ObjectID, objInfo.ObjectType, cp.Server, dBName);
            frm = CheckEditorAlreadyExits(windowId, caption, script);
            if (frm != null)
                return frm;

            frm = new frmScriptEditor();
            frm.InitializeScriptEditor(caption, script, objInfo.ObjectID, objInfo.ObjectType, cp, String.IsNullOrEmpty(dBName) ? cp.Database : dBName);
            return frm;
        }

        internal static frmScriptEditor Create(string caption, string script, ConnectionParams cp)
        {
            return Create(caption, script, cp, String.Empty);
        }

        internal static frmScriptEditor Create(string caption, string script, ConnectionParams cp, string filePath)
        {
            return Create(caption, script, -1, DBObjectType.None, cp, cp.Database, filePath);
        }

        internal static frmScriptEditor Create(string caption, ConnectionParams cp)
        {
            return Create(caption, String.Empty, cp);
        }

        internal static frmScriptEditor Create(ConnectionParams cp)
        {
            int? windowNo = Numerator.NextNumber;
            if (!windowNo.HasValue)
            {
                _instanceCnt++;
                windowNo = _instanceCnt;
            }

            string caption = String.Format("Script {0}", windowNo);
            frmScriptEditor editor = Create(caption, cp);
            editor.WindowNo = windowNo;

            return editor;
        }

        internal static frmScriptEditor Create(string caption, string script, int objectType, NodeData data)
        {
            frmScriptEditor frm = Create(caption, script, data);
            frm.ObjectType = objectType;
            return frm;
        }


        internal static frmScriptEditor CreateWithAsyncConnection(string caption, string script, ConnectionParams cp, string initialCatalog, string filePath)
        {
            if (cp == null)
                throw new NullParameterException("ConnectionParams paremeter is null!");


            frmScriptEditor frm = new frmScriptEditor();
            frm.InitializeScriptEditorWithAsyncConnection(caption, script, -1, -1, cp, initialCatalog, false);
            if (!String.IsNullOrEmpty(filePath))
                frm.SetFilePath(filePath);
            return frm;
        }

        internal static frmScriptEditor CreateNew(string caption, string script, NodeData data)
        {
            if (data == null)
            {
                throw new NullParameterException("NodeData paremeter is null!");
            }

            frmScriptEditor frm = new frmScriptEditor();
            frm.InitializeScriptEditor(caption, script, data.ID, data.Type, data.ConnParams, data.DBName, false);
            return frm;
        }

        internal static frmScriptEditor OpenFile(string fileName, NodeData data)
        {
            ConnectionParams cp = data.ConnParams.CreateCopy();
            cp.Database = data.DBName;
            frmScriptEditor frm = Create(String.Empty, cp);
            if (!frm.OpenFile(fileName))
            {
                frm.Close();
                frm.Dispose();
                return null;
            }
            frm.ContentPersister.ContentType = EditorContentType.File;
            return frm;
        }

        internal static frmScriptEditor OpenFile(string fileName, ConnectionParams cp)
        {
            frmScriptEditor frm = Create(String.Empty, cp);
            if (!frm.OpenFile(fileName))
            {
                frm.Close();
                frm.Dispose();
                return null;
            }
            frm.ContentPersister.ContentType = EditorContentType.File;
            return frm;
        }

        internal static frmScriptEditor OpenSharedSnippet(SharedSnippetItemData itemData, ConnectionParams cp)
        {
            string dbName = String.Empty;
            string caption = String.Empty;
            string script = String.Empty;

            if (itemData == null)
                throw new NullParameterException("ItemData is null!");


            caption = itemData.Name;
            script = itemData.Snippet;
            if (cp != null)
                dbName = cp.Database;


            frmScriptEditor frm = null;
            string windowId = ScriptEditorManager.ProduceWindowId(itemData.Name, itemData.ID ?? -1, DBObjectType.SharedSnippet, cp.Server, dbName);
            frm = CheckEditorAlreadyExits(windowId, caption, script);
            if (frm != null)
                return frm;


            frm = new frmScriptEditor();
            frm.Icon = PragmaSQL.Properties.Resources.sharedSnippet;
            frm.ContentPersister = new SharedSnippetContentPersister();
            frm.ContentPersister.Data = itemData;
            frm.ContentPersister.Hint = "This is a shared snippet: " + itemData.Name;
            frm.ContentInfo = frm.ContentPersister.Hint;
            frm.ContentPersister.FilePath = itemData.Name;
            frm.InitializeScriptEditor(caption, script, itemData.ID ?? -1, DBObjectType.SharedSnippet, cp, dbName);
            return frm;
        }

        internal static frmScriptEditor OpenSharedScript(SharedScriptsItemData itemData, ConnectionParams cp)
        {
            string dbName = String.Empty;
            string caption = String.Empty;
            string script = String.Empty;

            if (itemData == null)
                throw new NullParameterException("ItemData is null!");


            caption = itemData.Name;
            script = itemData.Script;
            if (cp != null)
                dbName = cp.Database;

            frmScriptEditor frm = null;
            string windowId = ScriptEditorManager.ProduceWindowId(itemData.Name, itemData.ID ?? -1, DBObjectType.SharedScript, cp.Server, dbName);
            frm = CheckEditorAlreadyExits(windowId, caption, script);
            if (frm != null)
                return frm;


            frm = new frmScriptEditor();
            frm.Icon = PragmaSQL.Properties.Resources.sharedScript;

            frm.ContentPersister = new SharedScriptContentPersister();
            frm.ContentPersister.Data = itemData;
            frm.ContentPersister.Hint = "This is a shared script: " + itemData.Name;
            frm.ContentInfo = frm.ContentPersister.Hint;
            frm.ContentPersister.FilePath = itemData.Name;
            frm.InitializeScriptEditor(caption, script, itemData.ID ?? -1, DBObjectType.SharedScript, cp, dbName);

            return frm;
        }

        private static frmScriptEditor CheckEditorAlreadyExits(string windowId, string windowTitle, string newContent)
        {
            string input1 = String.Empty;
            string input2 = String.Empty;

            frmScriptEditor frm = null;
            if (!String.IsNullOrEmpty(windowId) && ScriptEditorManager.Contains(windowId))
            {
                frm = ScriptEditorManager.Get(windowId);
                input1 = !String.IsNullOrEmpty(frm.Content) ? frm.Content.Replace("\r", String.Empty) : String.Empty;
                input2 = !String.IsNullOrEmpty(newContent) ? newContent.Replace("\r", String.Empty) : String.Empty;

                if (!input1.Equals(input2, StringComparison.InvariantCultureIgnoreCase)
                  && Utils.AskYesNoQuestion(String.Format(Properties.Resources.CanReloadScriptEditorContent, windowTitle), MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    frm.ScriptText = newContent;
                    frm.ContentModified = false;
                }
            }

            return frm;
        }

    } // Class end
} // Namespace end
