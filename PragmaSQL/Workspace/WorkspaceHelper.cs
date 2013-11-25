using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using WeifenLuo.WinFormsUI.Docking;

using PragmaSQL.WorkspaceMan;

namespace PragmaSQL
{
	public class WorkspaceHelper
	{
    List<WorkspaceItem> _items = new List<WorkspaceItem>();

    public void SaveWorkspace()
    {
      WorkspaceManager.SaveAsDefault(_items);
    }

    public List<WorkspaceItem> LoadWorkspace()
    {
      return WorkspaceManager.LoadFromDefault();
    }

    public void PrepareObjectAndProjectExplorer()
		{
			frmMain mainForm = Program.MainForm;

			// Save project explorer project
			if (mainForm.ProjectExplorer != null && mainForm.ProjectExplorer.CurrentProject != null)
			{
				_items.Add(WorkspaceItem.CreateProjectFile(mainForm.ProjectExplorer.CurrentProject.FullPath));
			}
		}

    public void AddItem(WorkspaceItem item)
    {
      if (item == null)
        return;
      _items.Add(item);
    }

    public void Clear()
    {
      _items.Clear();
    }
	}
}
