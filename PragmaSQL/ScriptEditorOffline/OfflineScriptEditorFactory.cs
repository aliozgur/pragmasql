/********************************************************************
  Class      : ScriptEditorFactory
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Database;
using PragmaSQL.Common;
using WeifenLuo.WinFormsUI;

namespace PragmaSQL
{
  public static class OfflineScriptEditorFactory
  {
    private static int _instanceCnt = 0;
    
    public static void ShowScriptEditor(frmOfflineScriptEditor frm)
    {
      if(frm == null)
      {
        return;
      }

      if ( Program.MainForm.DockPanel.DocumentStyle == DocumentStyles.SystemMdi)
      {
        frm.MdiParent = Program.MainForm;
        frm.Show();
      }
      else if ( Program.MainForm.DockPanel.DocumentStyle == DocumentStyles.DockingWindow)
      {
        frm.Show();
      }
      else
      {
        frm.Show(Program.MainForm.DockPanel);
      }
    }
    
    public static frmOfflineScriptEditor CreateScriptEditor()
    {
      return CreateScriptEditor("Offline Script " + (++_instanceCnt).ToString(),String.Empty,DBObjectType.None);
    }
    
    public static frmOfflineScriptEditor CreateScriptEditor(string caption, string script, int objType)
    {
      frmOfflineScriptEditor frm = new frmOfflineScriptEditor();
      frm.InitializeScriptEditor(caption, script, objType);
      return frm;
    }
    
    public static frmOfflineScriptEditor OpenSharedSnippetInScriptEditor(SharedSnippetItemData itemData)
    {
      string caption = String.Empty;
      string script = String.Empty;

      if(itemData == null)
      {
        throw new NullParameterException("ItemData is null!");
      }
      
      caption = itemData.Name + "{Shared snippet}";
      script = itemData.Snippet;

      frmOfflineScriptEditor frm = new frmOfflineScriptEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedSnippet;
      frm.ContentProvider = new SharedSnippetContentProvider();
      frm.ContentProvider.Data = itemData;
      frm.ContentProvider.Hint = "This is a shared snippet: " + itemData.Name;
      frm.ContentInfo = frm.ContentProvider.Hint;

       frm.ContentProvider.ContentName = itemData.Name;
      frm.InitializeScriptEditor(caption, script, DBObjectType.None);
      return frm;
    }
    
    public static frmOfflineScriptEditor OpenSharedScriptInScriptEditor(SharedScriptsItemData itemData)
    {
      string caption = String.Empty;
      string script = String.Empty;

      if (itemData == null)
      {
        throw new NullParameterException("ItemData is null!");
      }

      caption = itemData.Name + "{Shared script}";
      script = itemData.Script;

      frmOfflineScriptEditor frm = new frmOfflineScriptEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedScript;
      frm.ContentProvider = new SharedScriptContentProvider();
      frm.ContentProvider.Data = itemData;
      frm.ContentProvider.Hint = "This is a shared script: " + itemData.Name;
      frm.ContentInfo = frm.ContentProvider.Hint;

      frm.ContentProvider.ContentName = itemData.Name;
      frm.InitializeScriptEditor(caption, script, DBObjectType.None);
      return frm;
    }

  } // Class end
} // Namespace end
