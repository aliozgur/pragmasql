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
  public static class ScriptEditorFactory
  {
    private static int _instanceCnt = 0;

    public static void ShowScriptEditor(frmScriptEditor frm)
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
    
    public static void ShowScriptEditorIn( DockPanel dockPanel, frmScriptEditor frm)
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
        frm.Show(dockPanel);
      }
    }

    public static frmScriptEditor Create(string caption, NodeData data)
    {
      if (data == null)
      {
        throw new NullParameterException("NodeData paremeter is null!");
      }
      frmScriptEditor frm = new frmScriptEditor();
      frm.InitializeScriptEditor(caption, String.Empty, DBObjectType.None, data.ConnParams, data.DBName);
      return frm;
    }
    
    public static frmScriptEditor Create(string caption, ConnectionParams cp)
    {
      if (cp == null)
      {
        throw new NullParameterException("ConnectionParams paremeter is null!");
      }
      frmScriptEditor frm = new frmScriptEditor();
      frm.InitializeScriptEditor(caption, String.Empty, DBObjectType.None, cp, cp.Database);
      return frm;
    }

    public static frmScriptEditor Create(string caption, string script, ConnectionParams cp)
    {
      if (cp == null)
      {
        throw new NullParameterException("ConnectionParams paremeter is null!");
      }
      frmScriptEditor frm = new frmScriptEditor();
      frm.InitializeScriptEditor(caption, script, DBObjectType.None, cp, cp.Database);
      return frm;
    }


    public static frmScriptEditor Create(NodeData data)
    {    
      string caption = "Script " + (++_instanceCnt).ToString();
      return Create(caption,data);
    }

    public static frmScriptEditor Create(ConnectionParams cp)
    {    
      string caption = "Script " + (++_instanceCnt).ToString();
      return Create(caption,cp);
    }


    public static frmScriptEditor Create(string caption,string script, NodeData data)
    {
      frmScriptEditor frm = Create(caption,data);
      frm.ScriptText = script;
      return frm;
    }

    public static frmScriptEditor Create(string caption,string script,int objectType, ConnectionParams cp, string dBName)
    {
      if (cp == null)
      {
        throw new NullParameterException("ConnectionParams paremeter is null!");
      }
      frmScriptEditor frm = new frmScriptEditor();
      frm.InitializeScriptEditor(caption, script, objectType, cp, dBName);
      return frm;
    }

    public static frmScriptEditor Create(string caption,string script,int objectType, NodeData data)
    {
      frmScriptEditor frm = Create(caption, script, data);
      frm.ObjectType = objectType;
      return frm;
    }

    public static frmScriptEditor OpenFile(string fileName, NodeData data)
    {
      frmScriptEditor frm = Create(data);
      if(!frm.OpenFile(fileName))
      {
        _instanceCnt--;
        frm.Close();
        frm.Dispose();
        return null;
      }
      return frm;
    }

    public static frmScriptEditor OpenFile(string fileName, ConnectionParams cp)
    {
      frmScriptEditor frm = Create(cp);
      if(!frm.OpenFile(fileName))
      {
        _instanceCnt--;
        frm.Close();
        frm.Dispose();
        return null;
      }
      return frm;
    }

    public static frmScriptEditor OpenSharedSnippet(SharedSnippetItemData itemData,ConnectionParams cp)
    {
      string dbName = String.Empty;
      string caption = String.Empty;
      string script = String.Empty;

      if(itemData == null)
      {
        throw new NullParameterException("ItemData is null!");
      }
      

      if(cp != null)
      {
        dbName = cp.Database;
      }
        


      caption = itemData.Name + "{Shared snippet}";
      script = itemData.Snippet;

      frmScriptEditor frm = new frmScriptEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedSnippet;
      frm.ContentPersister = new SharedSnippetContentPersister();
      frm.ContentPersister.Data = itemData;
      frm.ContentPersister.Hint = "This is a shared snippet: " + itemData.Name;
      frm.ContentInfo = frm.ContentPersister.Hint;
      frm.ContentPersister.ContentName = itemData.Name;
      frm.InitializeScriptEditor(caption, script, DBObjectType.None, cp, dbName);
      return frm;
    }

    public static frmScriptEditor OpenSharedScript(SharedScriptsItemData itemData, ConnectionParams cp)
    {
      string dbName = String.Empty;
      string caption = String.Empty;
      string script = String.Empty;

      if (itemData == null)
      {
        throw new NullParameterException("ItemData is null!");
      }


      if (cp != null)
      {
        dbName = cp.Database;
      }



      caption = itemData.Name + "{Shared script}";
      script = itemData.Script;

      frmScriptEditor frm = new frmScriptEditor();
      frm.Icon = PragmaSQL.Properties.Resources.sharedScript;
      
      frm.ContentPersister = new SharedScriptContentPersister();
      frm.ContentPersister.Data = itemData;
      frm.ContentPersister.Hint = "This is a shared script: " + itemData.Name;
      frm.ContentInfo = frm.ContentPersister.Hint;
      frm.ContentPersister.ContentName = itemData.Name;
      frm.InitializeScriptEditor(caption, script, DBObjectType.None, cp, dbName);

      return frm;
    }
  } // Class end
} // Namespace end
