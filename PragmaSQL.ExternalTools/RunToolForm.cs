using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL.ExternalTools
{
	public partial class RunToolForm : DockContent
	{
		private static RunToolForm _instance = null;

		public static bool HasInstance
		{
			get { return _instance != null; }
		}

		private ExternalToolDef CurrentDef
		{
			get
			{
				return lb.SelectedItem as ExternalToolDef;
			}
		}

		public RunToolForm()
		{
			InitializeComponent();
			tbCmd.ReadOnly = true;
			tbArgs.ReadOnly = true;
			tbWorkingDir.ReadOnly = true;
		}



		public static void ShowForm(IList<ExternalToolDef> toolDefs)
		{
			if (_instance == null)
				_instance = new RunToolForm();

			
			AddInDockState dockState = HostServicesSingleton.HostServices.PersistedDockStateService.GetState(_instance.GetType());

			HostServicesSingleton.HostServices.ShowForm(_instance, dockState == AddInDockState.Unknown ? AddInDockState.DockBottomAutoHide : dockState);
			_instance.LoadExternalToolDefs(toolDefs);
		}

		private void LoadExternalToolDefs(IList<ExternalToolDef> toolDefs)
		{
			lb.Items.Clear();
			if (toolDefs == null)
				return;

			foreach (ExternalToolDef def in toolDefs)
			{
				lb.Items.Add(def);
			}

			if (lb.Items.Count > 0)
				lb.SelectedIndex = 0;
		}

		private void RenderExternalToolDef(ExternalToolDef exDef)
		{
			tbCmd.Text = String.Empty;
			tbArgs.Text = String.Empty;
			tbWorkingDir.Text = String.Empty;

			if (exDef == null)
				return;

			tbCmd.Text = exDef.Command;
			tbArgs.Text = !String.IsNullOrEmpty(exDef.Args) ? HostServicesSingleton.HostServices.EvalMacro(exDef.Args) : exDef.Args;
			tbWorkingDir.Text = exDef.WorkingDir;
		}

		private IDictionary<long, ExternalToolDef> _runningToolDefs = new Dictionary<long, ExternalToolDef>();
		private void RunTool()
		{
			if (CurrentDef == null)
				return;

			SaveBeforeExecute();

			ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
			tbArgs.Text = !String.IsNullOrEmpty(CurrentDef.Args) ? HostServicesSingleton.HostServices.EvalMacro(CurrentDef.Args) : CurrentDef.Args;

			psi.FileName = CurrentDef.Command;
			psi.Arguments = tbArgs.Text;
			psi.WorkingDirectory = CurrentDef.WorkingDir;
			psi.RedirectStandardOutput = CurrentDef.UseOuput;
			psi.RedirectStandardError = CurrentDef.UseOuput;
			psi.CreateNoWindow = CurrentDef.UseOuput;
			psi.UseShellExecute = !CurrentDef.UseOuput;

			Process p = new Process();
			p.EnableRaisingEvents = CurrentDef.UseOuput;
			if (CurrentDef.UseOuput)
				p.Exited += new EventHandler(p_Exited);

			p.StartInfo = psi;
			p.Start();

			if (CurrentDef.UseOuput)
				_runningToolDefs.Add(p.Handle.ToInt64(), CurrentDef);
		}

		private void SaveBeforeExecute()
		{
			if (CurrentDef.SaveBeforeExecute)
			{
				ITextEditor currentEditor = HostServicesSingleton.HostServices.EditorServices.CurrentEditor;
				if (currentEditor != null)
				{
					string fileName = currentEditor.FileName;
					if (string.IsNullOrEmpty(fileName))
					{
						OpenFileDialog op1 = new OpenFileDialog();
						op1.CheckFileExists = false;
						op1.InitialDirectory = CurrentDef.WorkingDir;
						op1.SupportMultiDottedExtensions = true;
						op1.Title = "Save file";
						op1.ShowDialog();
						fileName = op1.FileName;
					}

					if (!string.IsNullOrEmpty(fileName))
						currentEditor.SaveContentToFile(fileName);
				}
			}
		}

		void p_Exited(object sender, EventArgs e)
		{
			Process p = sender as Process;
			if (p == null)
				return;

			long handle = p.Handle.ToInt64();
			if (!_runningToolDefs.ContainsKey(handle))
				return;

			try
			{
				ExternalToolDef def = _runningToolDefs[handle];

				if (chkClearOutput.Checked)
					HostServicesSingleton.HostServices.MsgService.ClearMessages();

				bool shallShow = false;

				while (p.StandardOutput.Peek() > -1)
				{
					string info = p.StandardOutput.ReadLine();
					if (!String.IsNullOrEmpty(info))
					{
						HostServicesSingleton.HostServices.MsgService.InfoMsg(info, "Tool : " + def.Title, String.Empty, String.Empty);
						shallShow = true;
					}
				}

				while (p.StandardError.Peek() > -1)
				{
					string error = p.StandardError.ReadLine();
					if (!String.IsNullOrEmpty(error))
					{
						HostServicesSingleton.HostServices.MsgService.ErrorMsg(error, "Tool : " + def.Title, String.Empty, String.Empty);
						shallShow = true;
					}
				}

				if (shallShow == true)
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
			}
			finally
			{
				_runningToolDefs.Remove(handle);
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (((keyData & Keys.Modifiers & Keys.Control) != Keys.Control)
				&& ((keyData & Keys.Modifiers & Keys.Shift) != Keys.Shift))
			{
				if (msg.WParam == (IntPtr)Keys.F5)
				{
					RunTool();
					return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void lb_SelectedIndexChanged(object sender, EventArgs e)
		{
			RenderExternalToolDef(CurrentDef);
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			if (CurrentDef == null)
				return;

			int selectedIDX = lb.SelectedIndex;
			LoadExternalToolDefs(ExternalToolsCfg.Current);
			if (selectedIDX <= lb.Items.Count - 1)
				lb.SelectedIndex = selectedIDX;

			RenderExternalToolDef(CurrentDef);
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			RunTool();
		}

		private void btnOptions_Click(object sender, EventArgs e)
		{
			HostServicesSingleton.HostServices.ShowOptionsDialog("External Tools");
		}

		private void RunToolForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			AddInDockState dockState = (AddInDockState)Enum.Parse(typeof(AddInDockState), DockState.ToString());
			HostServicesSingleton.HostServices.PersistedDockStateService.AddState(this.GetType(), dockState);
			_instance = null;
		}

		private void lb_DoubleClick(object sender, EventArgs e)
		{
			Rectangle	r = lb.GetItemRectangle(lb.SelectedIndex);
			Point p = lb.PointToClient(Control.MousePosition);
			if (!r.Contains(p))
				return;

			RunTool();
		}
	}
}