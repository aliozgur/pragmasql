using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.ExternalTools
{
	public partial class ConfigForm : KryptonForm
	{
		private ExternalToolDef CurrentDef
		{
			get
			{
				return lb.SelectedItem as ExternalToolDef;
			}
		}

		private bool _doNotRender = false;
		private bool _initializing = false;
		private bool _shallSave = false;

		public static DialogResult ConfigureExternalTools()
		{
			ConfigForm frm = new ConfigForm();
			frm.LoadExternalToolDefs();
			return frm.ShowDialog();
		}

		public ConfigForm()
		{
			InitializeComponent();
			tbTitle.TextBox.ReadOnly = false;
			tbArgs.TextBox.ReadOnly = false;

			tbTitle.TextBox.TextChanged += new EventHandler(TitleTextBox_TextChanged);
			tbArgs.TextBox.TextChanged += new EventHandler(ArgsTextBox_TextChanged);
			tbCmd.TextBox.TextChanged += new EventHandler(tbCmd_TextChanged);
			tbWorkingDir.TextBox.TextChanged += new EventHandler(tbWorkingDir_TextChanged);
		}

		private bool StrEquals(string v1, string v2)
		{
			string tmp1 = String.IsNullOrEmpty(v1) ? String.Empty : v1;
			string tmp2 = String.IsNullOrEmpty(v2) ? String.Empty : v2;
			return tmp1.ToLowerInvariant().Trim() == tmp2.ToLowerInvariant().Trim();
		}


		private void TitleTextBox_TextChanged(object sender, EventArgs e)
		{
			if (_initializing)
				return;

			if (CurrentDef == null || StrEquals(CurrentDef.Title, tbTitle.TextBoxText))
				return;
			
			CurrentDef.Title = tbTitle.TextBoxText;
			_doNotRender = true;
			lb.Items[lb.SelectedIndex] = CurrentDef;
			_doNotRender = false;
			lb.Refresh();
			_shallSave = true;
		}

		private void tbCmd_TextChanged(object sender, EventArgs e)
		{
			if (_initializing)
				return;

			if (CurrentDef == null || StrEquals(CurrentDef.Command, tbCmd.Path))
				return;

			CurrentDef.Command = tbCmd.Path;
			_shallSave = true;
		}

		private void tbWorkingDir_TextChanged(object sender, EventArgs e)
		{
			if (_initializing)
				return;

			if (CurrentDef == null || StrEquals(CurrentDef.WorkingDir, tbWorkingDir.Path))
				return;

			CurrentDef.WorkingDir = tbWorkingDir.Path;
			_shallSave = true;
		}

		private void ArgsTextBox_TextChanged(object sender, EventArgs e)
		{
			if (_initializing)
				return;

			if (CurrentDef == null || StrEquals(CurrentDef.Args, tbArgs.TextBoxText))
				return;

			CurrentDef.Args = tbArgs.TextBoxText;
			_shallSave = true;
		}



		private void ClearControls()
		{
			tbTitle.TextBoxText = String.Empty;
			tbArgs.TextBoxText = String.Empty;
			tbCmd.Path = String.Empty;
		}


		private void LoadExternalToolDefs()
		{
			lb.Items.Clear();
			ClearControls();
			ExternalToolsCfg.Load();
			List<ExternalToolDef> current = ExternalToolsCfg.Current;
			foreach (ExternalToolDef def in current)
			{
				lb.Items.Add(def);
			}

			if (lb.Items.Count > 0)
				lb.SelectedIndex = 0;

			RenderExternalToolDef(CurrentDef);
		}

		private void RenderExternalToolDef(ExternalToolDef exDef)
		{
			if (_doNotRender)
				return;

			try
			{
				_initializing = true;
				ClearControls();
				if (exDef == null)
					return;

				tbTitle.TextBoxText = exDef.Title;
				tbCmd.Path = exDef.Command;
				tbArgs.TextBoxText = exDef.Args;
				tbWorkingDir.Path = exDef.WorkingDir;
				chkUseOutput.Checked = exDef.UseOuput;
			}
			finally
			{
				_initializing = false;
			}
		}

		private void AddTool()
		{
			string title = String.Empty;
			if (InputDialog.ShowDialog("Tool Title", ref title) != DialogResult.OK)
				return;

			ExternalToolDef def = null;
			try
			{
				_initializing = true;
				def = new ExternalToolDef();
				def.Title = title;
				def.Uid = Guid.NewGuid().ToString();
				lb.Items.Add(def);
				RenderExternalToolDef(def);
				ExternalToolsCfg.Current.Add(def);
				_shallSave = true;
			}
			finally
			{
				_initializing = false;
			}
		}

		private void RemoveTool()
		{
			if (CurrentDef == null)
				return;
			try
			{
				_initializing = true;
				ExternalToolsCfg.Current.Remove(CurrentDef);
				lb.Items.Remove(CurrentDef);

				if (lb.Items.Count > 0)
					lb.SelectedItem = lb.Items[0];
				RenderExternalToolDef(lb.SelectedItem as ExternalToolDef);
				_shallSave = true;
			}
			finally
			{
				_initializing = false;
			}

		}

		private void PrepareMacrosMenu()
		{
			ctxMacros.Items.Clear();
			Array values = Enum.GetValues(typeof(PragmaSqlMacros));
			ToolStripItem mnuItem = null;
			foreach (PragmaSqlMacros m in values)
			{
				mnuItem = new ToolStripMenuItem ();
				mnuItem.Text = EnumStrValAttr.GetStrValue(m);
				mnuItem.Tag = m;
				mnuItem.Click += new EventHandler(OnAddMacroClick);
				ctxMacros.Items.Add(mnuItem);
			}
		}



		private void btnAdd_Click(object sender, EventArgs e)
		{
			AddTool();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			RemoveTool();
		}
		
		private void button1_Click(object sender, EventArgs e)
		{
			Point p = this.PointToScreen(button1.Location);
			p.X = p.X + button1.Width;
			ctxMacros.Show(p);
		}		
		private void lb_DoubleClick(object sender, EventArgs e)
		{
			//RenderExternalToolDef(CurrentDef);
		}		
		
		private void OnAddMacroClick(object sender, EventArgs args)
		{
			ToolStripItem mnuItem = sender as ToolStripItem;
			if (mnuItem == null)
				return;
			
			PragmaSqlMacros m = (PragmaSqlMacros)mnuItem.Tag;
		  tbArgs.TextBox.SelectedText = ("$(" + m.ToString() + ")");
		}
		
		private void lb_SelectedIndexChanged(object sender, EventArgs e)
		{
			RenderExternalToolDef(CurrentDef);
		}

		private void chkUseOutput_CheckedChanged(object sender, EventArgs e)
		{
			if (_initializing)
				return;

			if (CurrentDef == null || CurrentDef.UseOuput == chkUseOutput.Checked)
				return;

			CurrentDef.UseOuput = chkUseOutput.Checked;
			_shallSave = true;

		}
				
		
		
		
		
		
		
		
		
		private void btnOk_Click(object sender, EventArgs e)
		{
			if (_shallSave)
			{
				ExternalToolsCfg.Save();
				_shallSave = false;
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.Ignore;
			}
		}



		private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_shallSave && MessageService.AskQuestion("Save changes?"))
			{
				ExternalToolsCfg.Save();
				_shallSave = false;
				DialogResult = DialogResult.OK;
			}
		}



		private void ConfigForm_Load(object sender, EventArgs e)
		{
			PrepareMacrosMenu();
		}


		
	}
}