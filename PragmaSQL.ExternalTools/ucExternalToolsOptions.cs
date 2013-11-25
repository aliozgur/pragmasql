using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL.ExternalTools
{
  public partial class ucExternalToolsOptions : UserControl, IConfigContentEditor 
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
      
    private static ucExternalToolsOptions _optionsUI;
    public static void ConfigSvc_DialogOpened(object sender, EventArgs e)
    {
      // to have a folder of its own
      // TreeNode jNode = HostServicesSingleton.HostServices.ConfigSvc.AddFolder("JIRA Client addin");
      _optionsUI = new ucExternalToolsOptions();
      _optionsUI.PrepareMacrosMenu();
      TreeNode oNode = HostServicesSingleton.HostServices.ConfigSvc.AddItem(null, "External Tools", _optionsUI);

      if (oNode != null && oNode.Parent != null)
        oNode.Parent.Expand();
    }
    
		public static void ConfigSvc_DialogClosed(object sender, EventArgs e)
    {
    
		}
    
		public static void ConfigSvc_FinalSelection(object sender, ConfigEventArgs args)
    {
    
		}
    
    public ucExternalToolsOptions()
    {
      InitializeComponent();

			tbTitle.TextBox.ReadOnly = false;
			tbArgs.TextBox.ReadOnly = false;

			tbTitle.TextBox.TextChanged += new EventHandler(TitleTextBox_TextChanged);
			tbArgs.TextBox.TextChanged += new EventHandler(ArgsTextBox_TextChanged);
			tbCmd.TextBox.TextChanged += new EventHandler(tbCmd_TextChanged);
			tbWorkingDir.TextBox.TextChanged += new EventHandler(tbWorkingDir_TextChanged);
		      
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
    private void ArgsTextBox_TextChanged(object sender, EventArgs e)
    {
      if (_initializing)
        return;

      if (CurrentDef == null || StrEquals(CurrentDef.Args, tbArgs.TextBoxText))
        return;

      CurrentDef.Args = tbArgs.TextBoxText;
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
        chkSaveBeforeExecute.Checked = exDef.SaveBeforeExecute;
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
        mnuItem = new ToolStripMenuItem();
        mnuItem.Text = EnumStrValAttr.GetStrValue(m);
        mnuItem.Tag = m;
        mnuItem.Click += new EventHandler(OnAddMacroClick);
        ctxMacros.Items.Add(mnuItem);
      }
    }
    
    private bool StrEquals(string v1, string v2)
    {
      string tmp1 = String.IsNullOrEmpty(v1) ? String.Empty : v1;
      string tmp2 = String.IsNullOrEmpty(v2) ? String.Empty : v2;
      return tmp1.ToLowerInvariant().Trim() == tmp2.ToLowerInvariant().Trim();
    }



    private void btnAdd_Click(object sender, EventArgs e)
    {
      AddTool();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      RemoveTool();
    }

    private void btnMacroList_Click(object sender, EventArgs e)
    {
      Point p = this.PointToScreen(btnMacroList.Location);
      p.X = p.X + btnMacroList.Width;
      ctxMacros.Show(p);
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
    
    private void chkSaveBeforeExecute_CheckedChanged(object sender, EventArgs e)
    {
      if (_initializing)
        return;

      if (CurrentDef == null || CurrentDef.SaveBeforeExecute  == chkUseOutput.Checked)
        return;

      CurrentDef.SaveBeforeExecute = chkUseOutput.Checked;
      _shallSave = true;
    }
        
    #region IConfigContentEditor Members

    private bool _contentLoaded = false;
    public bool ContentLoaded
    {
      get { return _contentLoaded; }
    }

    public void HideContent()
    {
      this.Visible = false;
    }

    public string ItemClassName
    {
      get { return "ExternalToolDef"; }
    }

    public bool LoadContent()
    { 
      _contentLoaded = false;
      LoadExternalToolDefs();
      _contentLoaded = true;
      
      return true;
    }

    public bool Modified
    {
      get { return _shallSave; }
    }

    public bool SaveContent()
    {
      ExternalToolsCfg.Save();
      return true;
    }

    public void ShowContent()
    {
      this.Show();
    }

    #endregion








  }
}
