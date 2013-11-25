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

namespace PragmaSQL.GUI
{
  public static class ScriptEditorFactory
  {
    private static int _instanceCnt = 0;

    public static frmScriptEditor CreateScriptEditor(string caption, NodeData data)
    {
      if (data == null)
      {
        throw new NullParameterException("NodeData paremeter is null!");
      }
      frmScriptEditor frm = new frmScriptEditor();
      frm.InitializeScriptEditor(caption, String.Empty, DBObjectType.None, data.ConnParams, data.DBName);
      return frm;
    }
    
    
    public static frmScriptEditor CreateScriptEditor(NodeData data)
    {    
      string caption = "Script " + (++_instanceCnt).ToString();
      return CreateScriptEditor(caption,data);
    }


    public static frmScriptEditor CreateScriptEditor(string caption,string script, NodeData data)
    {
      frmScriptEditor frm = CreateScriptEditor(caption,data);
      frm.ScriptText = script;
      return frm;
    }

    public static frmScriptEditor CreateScriptEditor(string caption,string script,int objectType, NodeData data)
    {
      frmScriptEditor frm = CreateScriptEditor(caption, script, data);
      frm.ObjectType = objectType;
      return frm;
    }

    public static frmScriptEditor OpenFile(string fileName, NodeData data)
    {
      frmScriptEditor frm = CreateScriptEditor(data);
      if(!frm.OpenScriptFromFile(String.Empty))
      {
        _instanceCnt--;
        frm.Close();
        frm.Dispose();
        return null;
      }
      
      
      return frm;
    }

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
      else
      {
        frm.Show(Program.MainForm.DockPanel);
      }
    }

  }
}
