using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	/// <summary>
	/// Summary description for TreeViewMS.
	/// </summary>
	public class TreeViewEx : TreeView
	{
		protected List<TreeNode> _selNodes;
		protected TreeNode _lastNode;
		protected TreeNode _firstNode;

		public TreeViewEx()
		{
			_selNodes = new List<TreeNode>();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: Add custom paint code here

			// Calling the base class OnPaint
			base.OnPaint(pe);
		}


		public ReadOnlyCollection<TreeNode> SelNodes
		{
			get
			{
				return _selNodes.AsReadOnly();
			}
		}

		public void SelectNode(TreeNode node)
		{
			if (_selNodes.Contains(node))
				return;

			_selNodes.Add(node);

			node.BackColor = SystemColors.Highlight;
			node.ForeColor = SystemColors.HighlightText;
		}

		public void DeselectNode(TreeNode node)
		{
			if (!_selNodes.Contains(node))
				return;

			_selNodes.Remove(node);

			node.BackColor = this.BackColor;
			node.ForeColor = this.ForeColor;
		}

		public void ClearSelectedNodes()
		{
			RemovePaintFromNodes();
			_selNodes.Clear();
		}

		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			base.OnBeforeSelect(e);

			bool bControl = (ModifierKeys == Keys.Control);
			bool bShift = (ModifierKeys == Keys.Shift);

			// selecting twice the node while pressing CTRL ?
			if (bControl && _selNodes.Contains(e.Node))
			{
				// unselect it (let framework know we don't want selection this time)
				e.Cancel = true;

				// update nodes
				RemovePaintFromNodes();
				_selNodes.Remove(e.Node);
				PaintSelectedNodes();
				return;
			}

			_lastNode = e.Node;
			if (!bShift) 
				_firstNode = e.Node; // store begin of shift sequence
		}

		protected override void OnAfterSelect(TreeViewEventArgs e)
		{	
			base.OnAfterSelect(e);
			bool bControl = (ModifierKeys == Keys.Control);
			bool bShift = (ModifierKeys == Keys.Shift);

			if (bControl)
			{
				if (!_selNodes.Contains(e.Node)) // new node ?
				{
					if (_selNodes.Count > 0)
					{
						if (_selNodes[0].Parent != e.Node.Parent)
						{
							return;
						}
					}
					_selNodes.Add(e.Node);
				}
				else  // not new, remove it from the collection
				{
					RemovePaintFromNodes();
					_selNodes.Remove(e.Node);
				}
				PaintSelectedNodes();
			}
			else
			{
				// SHIFT is pressed
				if (bShift)
				{
					Queue<TreeNode> myQueue = new Queue<TreeNode>();

					TreeNode uppernode = _firstNode;
					TreeNode bottomnode = e.Node;
					// case 1 : begin and end nodes are parent
					bool bParent = IsParent(_firstNode, e.Node); // is m_firstNode parent (direct or not) of e.Node
					if (!bParent)
					{
						bParent = IsParent(bottomnode, uppernode);
						if (bParent) // swap nodes
						{
							TreeNode t = uppernode;
							uppernode = bottomnode;
							bottomnode = t;
						}
					}
					if (bParent)
					{
						TreeNode n = bottomnode;
						while (n != uppernode.Parent)
						{
							if (!_selNodes.Contains(n)) // new node ?
								myQueue.Enqueue(n);

							n = n.Parent;
						}
					}
					// case 2 : nor the begin nor the end node are descendant one another
					else
					{
						if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) // are they siblings ?
						{
							int nIndexUpper = uppernode.Index;
							int nIndexBottom = bottomnode.Index;
							if (nIndexBottom < nIndexUpper) // reversed?
							{
								TreeNode t = uppernode;
								uppernode = bottomnode;
								bottomnode = t;
								nIndexUpper = uppernode.Index;
								nIndexBottom = bottomnode.Index;
							}

							TreeNode n = uppernode;
							while (nIndexUpper <= nIndexBottom)
							{
								if (!_selNodes.Contains(n)) // new node ?
									myQueue.Enqueue(n);

								n = n.NextNode;

								nIndexUpper++;
							} // end while

						}
						else
						{
							if (!_selNodes.Contains(uppernode)) myQueue.Enqueue(uppernode);
							if (!_selNodes.Contains(bottomnode)) myQueue.Enqueue(bottomnode);
						}
					}

					_selNodes.AddRange(myQueue);

					PaintSelectedNodes();
					_firstNode = e.Node; // let us chain several SHIFTs if we like it
				} // end if m_bShift
				else
				{
					// in the case of a simple click, just add this item
					if (_selNodes != null && _selNodes.Count > 0)
					{
						RemovePaintFromNodes();
						_selNodes.Clear();
					}
					_selNodes.Add(e.Node);
				}
			}
		}



		// Helpers
		//
		//


		protected bool IsParent(TreeNode parentNode, TreeNode childNode)
		{
			if (parentNode == childNode)
				return true;

			TreeNode n = childNode;
			bool bFound = false;
			while (!bFound && n != null)
			{
				n = n.Parent;
				bFound = (n == parentNode);
			}
			return bFound;
		}

		protected void PaintSelectedNodes()
		{
			foreach (TreeNode n in _selNodes)
			{
				n.BackColor = SystemColors.Highlight;
				n.ForeColor = SystemColors.HighlightText;
			}
		}

		protected void RemovePaintFromNodes()
		{
			if (_selNodes.Count == 0)
				return;

			TreeNode n0 = (TreeNode)_selNodes[0];
			Color back = n0.TreeView.BackColor;
			Color fore = n0.TreeView.ForeColor;

			foreach (TreeNode n in _selNodes)
			{
				n.BackColor = back;
				n.ForeColor = fore;
			}

		}

	}
}
