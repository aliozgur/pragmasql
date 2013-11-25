/********************************************************************
  Class      : frmSaveScripts
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI;

namespace PragmaSQL.GUI
{
  public partial class frmSaveScripts : Form
  {
    public frmSaveScripts( )
    {
      InitializeComponent();
    }

    public void PopulateList(DockPanel dockPanel,frmScriptEditor skipThis)
    {

      if (dockPanel != null)
      {
        if (dockPanel.DocumentStyle == DocumentStyles.SystemMdi)
        {
          foreach (Form form in Program.MainForm.MdiChildren )
          {
            if(form is frmScriptEditor && form != skipThis )
            {
              AddToList(form as frmScriptEditor);
            }
          }
        }
        else
        {
          foreach (IDockContent content in dockPanel.Documents)
          {
            if(content is frmScriptEditor && content != skipThis )
            {
              AddToList(content as frmScriptEditor);
            }
          }
        }
      }
    }
    
    private void AddToList(frmScriptEditor scriptEditor)
    {
      if(scriptEditor == null || !scriptEditor.ScriptModified)
      {
        return;
      }

      ListViewItem  item = lv.Items.Add(scriptEditor.Caption,0);
      item.Checked = true;

      item.SubItems.Add(scriptEditor.FileName);

      if(scriptEditor.LastModifiedOn.HasValue)
      {
        item.SubItems.Add(scriptEditor.LastModifiedOn.Value.ToString());
      }
      else
      {
        item.SubItems.Add(String.Empty);
      }
      
      item.Tag = scriptEditor;
      lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    public static DialogResult ShowSaveScriptsDialog(DockPanel dockPanel, frmScriptEditor skipThis)
    {   
      if(!CheckIfShallShow(dockPanel,skipThis))
      {
        return DialogResult.Ignore;
      }

      frmSaveScripts frm = new frmSaveScripts();
      frm.PopulateList(dockPanel,skipThis);
      return frm.ShowDialog();
    }
    
    private static bool CheckIfShallShow(DockPanel dockPanel,frmScriptEditor skipThis)
    {
      bool result = false;
      int cnt = 0;

      if (dockPanel != null)
      {
        if (dockPanel.DocumentStyle == DocumentStyles.SystemMdi)
        {
          foreach (Form form in Program.MainForm.MdiChildren)
          {
            if(form is frmScriptEditor && form != skipThis)
            {
              if( (form as frmScriptEditor).ScriptModified)
              {
                cnt++;
                if(cnt == 2)
                {
                  result = true;
                  break;
                }
              }
            }
          }
        }
        else
        {
          foreach (IDockContent content in dockPanel.Documents)
          {
            if(content is frmScriptEditor && content != skipThis )
            {
              if( (content as frmScriptEditor).ScriptModified)
              {
                cnt++;
                if(cnt == 2)
                {
                  result = true;
                  break;
                }
              }
            }
          }
        }
      }
      return result;
    }

    private void btnSaveChecked_Click( object sender, EventArgs e )
    {      
      foreach(ListViewItem item  in lv.Items )
      {
        frmScriptEditor editor = item.Tag as frmScriptEditor;
        if(item.Checked)
        {
          editor.SaveScript();
        }
        editor.CheckSave = false;
      }
      DialogResult = DialogResult.OK;
    }

    private void btnSkipAll_Click( object sender, EventArgs e )
    {
      DialogResult dlgRes = MessageBox.Show("Do you really want to skip changes?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
      if(dlgRes == DialogResult.No )
      {
        return;
      }

      foreach(ListViewItem item  in lv.Items )
      {
        frmScriptEditor editor = item.Tag as frmScriptEditor;
        editor.CheckSave = false;
      }
      DialogResult = DialogResult.Ignore;
    }

    private void lv_ItemChecked( object sender, ItemCheckedEventArgs e )
    {
      if(lv.CheckedItems.Count == 0)
      {
        btnSaveChecked.Enabled = false;
      }
      else
      {
        btnSaveChecked.Enabled = true;      
      }
    }
  }
}