using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public class ConfigSvc:IConfigSvc
	{
		private EventHandler _dialogOpened;
		public event EventHandler DialogOpened
		{
			add { _dialogOpened += value; }
			remove { _dialogOpened -= value; }
		}

		private EventHandler _dialogClosed;
		public event EventHandler DialogClosed
		{
			add { _dialogClosed += value; }
			remove { _dialogClosed -= value; }
		}

		private ConfigFinalSelectionEventHandler _finalSelection;
		public event ConfigFinalSelectionEventHandler FinalSelection
		{
			add { _finalSelection += value; }
			remove { _finalSelection -= value; }
		}


		public TreeNode AddFolder(string text)
		{
			if (frmConfigurationDlg.Instance == null)
				throw new InvalidOperationException("AddFolder operation is valid only when dialog is opened!");
			return frmConfigurationDlg.Instance.AddFolder(text);
		}

		public TreeNode AddItem(TreeNode parent, string text, IConfigContentEditor editor)
		{
			if (frmConfigurationDlg.Instance == null)
				throw new InvalidOperationException("AddFolder operation is valid only when dialog is opened!");

			return frmConfigurationDlg.Instance.AddItem(parent, text, editor);
		}

		public bool CheckOptionChanged(string itemClassName)
		{
			return false;		
		}

		internal void FireDialogOpenedEvent()
		{
			if (frmConfigurationDlg.Instance != null && _dialogOpened != null)
				_dialogOpened(this, EventArgs.Empty);
		}

		internal void FireDialogClosedEvent()
		{
			if (frmConfigurationDlg.Instance != null && _dialogClosed != null)
				_dialogClosed(this, EventArgs.Empty);
		}

		internal void FireFinalSelection(ConfigAction action, IList<string> changedOptions)
		{
			if (frmConfigurationDlg.Instance != null && _finalSelection != null)
			{
				ConfigEventArgs args = new ConfigEventArgs();
				args.action = action;
				args.ChangedOptions = changedOptions;
				_finalSelection(this, args);
			}
		}

		public void ShowOptionsEditor(string caption)
		{
			if (frmConfigurationDlg.Instance == null)
				return;
			frmConfigurationDlg.Instance.ShowOptionsEditor(caption);
		}

	}
}
