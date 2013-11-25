/********************************************************************
  Class      : frmSaveScripts
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PragmaSQL.Core;
using WeifenLuo.WinFormsUI.Docking;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public partial class frmSaveScripts : KryptonForm
  {
    private IDictionary<EditorContentType, ListViewGroup> _groups = new Dictionary<EditorContentType, ListViewGroup>();

    public frmSaveScripts()
    {
      InitializeComponent();
      PrepareGroups();
    }

    private void PrepareGroups()
    {
      string[] names = Enum.GetNames(typeof(EditorContentType));
      ListViewGroup group = null;
      foreach (string gName in names)
      {
        group = lv.Groups.Add(gName, gName);
        _groups.Add((EditorContentType)Enum.Parse(typeof(EditorContentType), gName), group);
      }
    }

    public void PopulateList(DockPanel dockPanel, IPragmaEditor skipThis)
    {

      if (dockPanel != null)
      {
        if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in Program.MainForm.MdiChildren)
          {
            if (form is IPragmaEditor && form != skipThis)
            {
              AddToList(form as IPragmaEditor);
            }
          }
        }
        else
        {
          IList<IDockContent> contents = new List<IDockContent>();
          foreach (FloatWindow wnd in dockPanel.FloatWindows)
          {
            foreach (DockPane pane in wnd.NestedPanes)
            {
              foreach (IDockContent content in pane.Contents)
              {
                if (!contents.Contains(content) && (content is IPragmaEditor) && content != skipThis)
                {
                  if ((content as IPragmaEditor).ContentModified)
                  {
                    contents.Add(content);
                    AddToList(content as IPragmaEditor);
                  }
                }
              }
            }
          }

          IDockContent[] docs = dockPanel.DocumentsToArray();
          foreach (IDockContent content in docs)
          {
            if (!contents.Contains(content) && (content is IPragmaEditor) && content != skipThis)
            {
              AddToList(content as IPragmaEditor);
            }
          }
        }
      }
    }

    /*
    private int GetImageIndex(EditorContentType contentType)
    {
      switch (contentType)
      {
        case EditorContentType.File:
          return 0;
        case EditorContentType.SharedSnippet:
          return 1;
        default:
          return -1;
      }
    }
    */
    
    private void AddToList(IPragmaEditor scriptEditor)
    {
      if (scriptEditor == null || !scriptEditor.ContentModified)
        return;

      if (scriptEditor.ContentPersister == null)
        return;

      ListViewGroup group = null;
      if (_groups.ContainsKey(scriptEditor.ContentPersister.ContentType))
        group = _groups[scriptEditor.ContentPersister.ContentType];

      ListViewItem item = new ListViewItem(scriptEditor.Caption, 0,group);
      lv.Items.Add(item);
      item.Checked = true;

      if (scriptEditor.LastModifiedOn.HasValue)
        item.SubItems.Add(scriptEditor.LastModifiedOn.Value.ToString());
      else
        item.SubItems.Add(String.Empty);

      item.SubItems.Add(scriptEditor.ContentPersister.FilePath);
      item.Tag = scriptEditor;
    }

    public static DialogResult ShowSaveScriptsDialog(DockPanel dockPanel, IPragmaEditor skipThis)
    {
      if (!CheckIfShallShow(dockPanel, skipThis))
      {
        return DialogResult.Ignore;
      }

      frmSaveScripts frm = new frmSaveScripts();
      frm.PopulateList(dockPanel, skipThis);
      return frm.ShowDialog();
    }


    private static bool CheckIfShallShow(DockPanel dockPanel, IPragmaEditor skipThis)
    {
      bool result = false;
      int cnt = 0;

      if (dockPanel != null)
      {
        if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in Program.MainForm.MdiChildren)
          {
            if (form is IPragmaEditor && form != skipThis)
            {
              if ((form as IPragmaEditor).ContentModified)
              {
                return true;
              }
            }
          }
        }
        else
        {
          IList<IDockContent> contents = new List<IDockContent>();

          foreach (FloatWindow wnd in dockPanel.FloatWindows)
          {
            foreach (DockPane pane in wnd.NestedPanes)
            {
              foreach (IDockContent content in pane.Contents)
              {
                if (!contents.Contains(content) && (content is IPragmaEditor) && content != skipThis)
                {
                  contents.Add(content);
                  if ((content as IPragmaEditor).ContentModified)
                  {
                    return true;
                  }
                }
              }
            }
          }

          IDockContent[] docs = dockPanel.DocumentsToArray();
          foreach (IDockContent content in docs)
          {
            if (!contents.Contains(content) && (content is IPragmaEditor) && content != skipThis)
            {
              if ((content as IPragmaEditor).ContentModified)
              {
                return true;
              }
            }
          }
        }
      }
      return result;
    }

    private void btnSaveChecked_Click(object sender, EventArgs e)
    {
      foreach (ListViewItem item in lv.Items)
      {
        IPragmaEditor editor = item.Tag as IPragmaEditor;
        if (item.Checked)
        {
          editor.SaveContent();
        }
        editor.CheckSave = false;
      }
      DialogResult = DialogResult.OK;
    }

    private void btnSkipAll_Click(object sender, EventArgs e)
    {
      DialogResult dlgRes = MessageBox.Show("Do you really want to skip changes?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
      if (dlgRes == DialogResult.No)
      {
        return;
      }

      foreach (ListViewItem item in lv.Items)
      {
        IPragmaEditor editor = item.Tag as IPragmaEditor;
        editor.CheckSave = false;
      }
      DialogResult = DialogResult.Ignore;
    }

    private void lv_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (lv.CheckedItems.Count == 0)
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