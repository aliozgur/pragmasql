/********************************************************************
  Class      : TextEditorFactory
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
  public static class TextEditorFactory
  {
    private static int _instanceCnt = 0;
    
    public static void ShowTextEditor(frmTextEditor frm)
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
    
    public static frmTextEditor Create()
    {
      return Create("Text " + (++_instanceCnt).ToString(),String.Empty,DBObjectType.None);
    }
    
    public static frmTextEditor Create(string caption, string script, int objType)
    {
      frmTextEditor frm = new frmTextEditor();
      frm.InitializeScriptEditor(caption, script, objType);
      return frm;
    }

    public static frmTextEditor OpenFile( string fileName )
    {
      frmTextEditor frm = Create();
      if (!frm.OpenFile(fileName))
      {
        _instanceCnt--;
        frm.Close();
        frm.Dispose();
        return null;
      }
      return frm;
    }

    public static frmTextEditor OpenSharedSnippet(SharedSnippetItemData itemData)
    {
      string caption = String.Empty;
      string script = String.Empty;

      if(itemData == null)
      {
        throw new NullParameterException("ItemData is null!");
      }
      
      caption = itemData.Name + "{Shared snippet}";
      script = itemData.Snippet;

      frmTextEditor frm = new frmTextEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedSnippet;
      frm.ContentPersister = new SharedSnippetContentPersister();
      frm.ContentPersister.Data = itemData;
      frm.ContentPersister.Hint = "This is a shared snippet: " + itemData.Name;
      frm.ContentInfo = frm.ContentPersister.Hint;

       frm.ContentPersister.ContentName = itemData.Name;
      frm.InitializeScriptEditor(caption, script, DBObjectType.None);
      return frm;
    }
    
    public static frmTextEditor OpenSharedScript(SharedScriptsItemData itemData)
    {
      string caption = String.Empty;
      string script = String.Empty;

      if (itemData == null)
      {
        throw new NullParameterException("ItemData is null!");
      }

      caption = itemData.Name + "{Shared script}";
      script = itemData.Script;

      frmTextEditor frm = new frmTextEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedScript;
      frm.ContentPersister = new SharedScriptContentPersister();
      frm.ContentPersister.Data = itemData;
      frm.ContentPersister.Hint = "This is a shared script: " + itemData.Name;
      frm.ContentInfo = frm.ContentPersister.Hint;

      frm.ContentPersister.ContentName = itemData.Name;
      frm.InitializeScriptEditor(caption, script, DBObjectType.None);
      return frm;
    }

    

  } // Class end
} // Namespace end
