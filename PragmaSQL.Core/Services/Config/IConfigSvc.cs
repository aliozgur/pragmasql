using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public interface IConfigSvc
	{
		TreeNode AddFolder(string text);
		TreeNode AddItem(TreeNode parent, string text, IConfigContentEditor editor);
		void ShowOptionsEditor(string caption);

		event EventHandler DialogOpened;
		event EventHandler DialogClosed;
		event ConfigFinalSelectionEventHandler FinalSelection;
	}
}
