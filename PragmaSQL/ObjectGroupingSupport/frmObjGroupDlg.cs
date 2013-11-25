using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;
namespace PragmaSQL
{
  public partial class frmObjGroupDlg : KryptonForm
	{
		private IList<TreeNode> _nodes;
    private ObjectInfo _objInfo;
    private bool _isImport = false;
		public frmObjGroupDlg()
		{
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif
      InitializeComponent();
		}

    public static void ShowObjectGroupingDlg(ConnectionParams cp, IList<TreeNode> nodes,string caption, bool isImport)
    {
      if (cp == null)
				throw new Exception("Connection parameters object is null!");

			if (nodes == null || nodes.Count == 0)
				throw new Exception("Objects collection is null or empty!");

      

			frmObjGroupDlg frm = new frmObjGroupDlg();
      frm.Text = caption;

			if (!frm.og.InitializeObjectGrouping(cp, true))
			{
				frm.Dispose();
				return;
			}

      frm._isImport = isImport;
      frm._nodes = nodes;
			frm.ShowDialog();
    }

		public static void ShowObjectGroupingDlg(ConnectionParams cp, IList<TreeNode> nodes, string caption)
		{
      ShowObjectGroupingDlg(cp, nodes,caption, false);
		}

   
		public static void ShowObjectGroupingDlg(ConnectionParams cp, ObjectInfo objInfo, string caption)
		{
			if (cp == null)
				throw new Exception("Connection parameters object is null!");

			if (objInfo == null || objInfo.ObjectID <= 0)
				throw new Exception("Objects information is null!");

			frmObjGroupDlg frm = new frmObjGroupDlg();
      frm.Text = caption;
			if (!frm.og.InitializeObjectGrouping(cp, true))
			{
				frm.Dispose();
				return;
			}

			frm._objInfo = objInfo;

			frm.ShowDialog();
		}

		private void ShowError(string message)
		{
			MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}


		private void AddNodesToSelectedObjectGroup()
		{
			TreeNode selNode = og.SelectedNode;
			if (selNode == null)
			{
				ShowError("Please select an object grouping folder!");
				return;
			}

			ObjectGroupingItemData selData = ObjectGroupingItemDataFactory.GetNodeData(selNode);
			if (selData.Type != DBObjectType.GroupingFolderY)
			{
				ShowError("Please select an object grouping folder!");
				return;
			}

			StringBuilder sb = new StringBuilder();
			foreach (TreeNode node in _nodes)
			{
				try
				{
					if(_isImport)
            og.ImportObject(node, selNode, true);
          else
          og.AddObjectFromObjectExplorer(node, selNode, true);
				}
				catch (Exception ex)
				{
					sb.AppendLine("-> " + ex.Message);
				}
			}

			if (sb.Length > 0)
			{
				GenericErrorDialog.ShowError("Object Grouping Error", "Some objects can not be added to selected group folder.", sb.ToString());
			}
			this.DialogResult = DialogResult.OK;
		}

		private void AddObjectToGroup()
		{
			TreeNode selNode = og.SelectedNode;
			if (selNode == null)
			{
				ShowError("Please select an object grouping folder!");
				return;
			}

			ObjectGroupingItemData selData = ObjectGroupingItemDataFactory.GetNodeData(selNode);
			if (selData.Type != DBObjectType.GroupingFolderY)
			{
				ShowError("Please select an object grouping folder!");
				return;
			}

			try
			{
				og.AddObjectToGroup(_objInfo, selNode, true);
			}
			catch (Exception ex)
			{
				GenericErrorDialog.ShowError("Object Grouping Error", "Object can not be added to the selected group folder.", ex.Message);
			}

			this.DialogResult = DialogResult.OK;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (_nodes != null)
				AddNodesToSelectedObjectGroup();
      else if (_objInfo != null)
				AddObjectToGroup();
		}
	}
}