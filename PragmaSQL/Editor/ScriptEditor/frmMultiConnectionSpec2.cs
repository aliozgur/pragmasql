using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PragmaSQL.Core;
using System.Data.SqlClient;
using ICSharpCode.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
	public partial class frmMultiConnectionSpec2 : KryptonForm
	{
		#region Fields And Properties

		private IDictionary<string, ListViewGroup> _groups = new Dictionary<string, ListViewGroup>();
    private SerializableDictionary<string, ConnectionParams> _selectedConnections;
    private string SelectedTemplateFileName
    {
      get
      {
        if (cmbTemplates.SelectedIndex == 0)
          return String.Empty;

        return String.Format("{0}{1}{2}", MultiExecSpec.TemplatesDirectory, cmbTemplates.SelectedItem, MultiExecSpec.TemplateFileExt);
      }
    }
		
    #endregion //Fields And Properties

		#region CTOR
		public frmMultiConnectionSpec2()
		{
			InitializeComponent();
      LoadTemplates();
		}
		#endregion //CTOR

    public static SerializableDictionary<string, ConnectionParams> SelectConnections(SerializableDictionary<string, ConnectionParams> currentConnections, ref string templateName)
    {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif 

      frmMultiConnectionSpec2 frm = new frmMultiConnectionSpec2();

      if (!String.IsNullOrEmpty(templateName) && frm.cmbTemplates.Items.Contains(templateName))
      {
        frm.cmbTemplates.SelectedItem = templateName;
      }
      else if (currentConnections != null)
      {
        templateName = String.Empty;
        frm._selectedConnections = new SerializableDictionary<string, ConnectionParams>(currentConnections);
        frm.RenderConnectionsInListView(frm._selectedConnections);
      }
      else
        frm.LoadDefaultTemplate();

      DialogResult dlgResult = frm.ShowDialog();
      templateName = frm.cmbTemplates.SelectedIndex == 0 ? String.Empty : frm.cmbTemplates.SelectedItem as string;
      
      if (dlgResult  == DialogResult.OK)
        return new SerializableDictionary<string, ConnectionParams>(frm._selectedConnections);
      else
        return null;
    }


    private bool _loadingTemplates = false;
    public void LoadTemplates()
    {
      _loadingTemplates = true;
      cmbTemplates.Items.Clear();
      cmbTemplates.Items.Add("<Select Template>");

      string[] fileNames = Directory.GetFiles(MultiExecSpec.TemplatesDirectory, "*" + MultiExecSpec.TemplateFileExt);
      foreach (string fileName in fileNames)
      {
        cmbTemplates.Items.Add(Path.GetFileNameWithoutExtension(fileName));
      }
      cmbTemplates.SelectedIndex = 0;
      _loadingTemplates = false;
      
    }


		private void RenderConnectionsInListView(IDictionary<string,ConnectionParams> collection)
		{
      lv.Items.Clear();
			if (collection == null)
				return;

			foreach (ConnectionParams cp in collection.Values)
			{
				RenderConnectionInList(cp);
			}
		}


		private void RenderConnectionInList(ConnectionParams cp)
		{
			string key = cp.PrepareConnKey();
			string normalKey = key.Replace(((Char)29).ToString(), " as ");

			ListViewGroup group = null;
			if (_groups.ContainsKey(key))
				group = _groups[key];
			else
			{
				group = lv.Groups.Add(key, normalKey);
				_groups.Add(key, group);
			}

			ListViewItem item = new ListViewItem(normalKey, group);
			item.SubItems.Add(cp.Database);
			item.Tag = cp;

			lv.Items.Add(item);
		}

    private void EditSelectedSpec()
    {
      if (cmbTemplates.SelectedIndex == 0)
        return;
      
      MultiDbTemplateEditorResult result = frmMultiDbTemplateEditor.EditSpec(cmbTemplates.SelectedItem as string);
      if (result.DialogResult == DialogResult.OK)
      {
        _selectedConnections = result.Connections;
        RenderConnectionsInListView(_selectedConnections);
      }
    }

    private void CreateNewSpec()
    {
      MultiDbTemplateEditorResult result = frmMultiDbTemplateEditor.CreateNewSpec();
      if (result.DialogResult == DialogResult.OK)
      {
        LoadTemplates();
        cmbTemplates.SelectedItem = result.TemplateName;
        _selectedConnections = result.Connections;
        RenderConnectionsInListView(_selectedConnections);
      }      
    }

    private void DeleteSelectedSpec()
    {
      if (cmbTemplates.SelectedIndex == 0)
        return;
      
      if (MessageBox.Show("Delete selected template?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
        return;

      if (File.Exists(SelectedTemplateFileName))
        File.Delete(SelectedTemplateFileName);

      ClearConnectionListView();
      LoadTemplates();
    }

    private void LoadDefaultTemplate()
    {
      LoadDefaultTemplate(false);
    }

    private void LoadDefaultTemplate(bool showNoDefaultInfo)
    {
      if (!File.Exists(MultiExecSpec.DefaultTemplate))
      {
        ClearConnectionListView();
        if (showNoDefaultInfo)
          Utils.ShowInfo("Default connection list template not defined.\r\nPlease load a template and use Default -> Save As Default to save a default connection list template.", MessageBoxButtons.OK);
        return;
      }

      _selectedConnections = MultiExecSpec.Load(MultiExecSpec.DefaultTemplate);
      _loadingTemplates = true;
      cmbTemplates.SelectedIndex = 0;
      _loadingTemplates = false;

      RenderConnectionsInListView(_selectedConnections);
    }

    private void ClearConnectionListView()
    {
      _selectedConnections = null;
      lv.Items.Clear();
    }

    private void RenameTemplate()
    {
      if (cmbTemplates.SelectedIndex == 0)
        return;

      string templateFileName = SelectedTemplateFileName;
      if (!File.Exists(templateFileName))
      {
        Utils.ShowError(String.Format("Template file does not exist.\r\n{0}", templateFileName), MessageBoxButtons.OK);
        return;
      }

      string tmpName = cmbTemplates.SelectedItem as string;
      if (InputDialog.ShowDialog("Rename Template", "New Template Name", ref tmpName) != DialogResult.OK)
      {
        return;
      }

      tmpName = tmpName.ToLowerInvariant().Trim();
      if ( tmpName == (cmbTemplates.SelectedItem as string).ToLowerInvariant().Trim())
        return;

      string destFile = MultiExecSpec.TemplatesDirectory + tmpName + MultiExecSpec.TemplateFileExt;
      if (File.Exists(destFile))
      {
        Utils.ShowError(String.Format("Template with name \'{0}\' already exists!", tmpName), MessageBoxButtons.OK);
        return;
      }

      File.Move(templateFileName, destFile);
      LoadTemplates();
      cmbTemplates.SelectedItem = tmpName;
    }

    private void RefreshTemplateList()
    {
      ClearConnectionListView();
      LoadTemplates();
    }


		private void frmMultiConnectionSpec_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

    private void cmbTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_loadingTemplates)
        return;

      LoadSelectedTemplate();
    }

    private void LoadSelectedTemplate()
    {
      try
      {
        toolStrip1.Enabled = false;

        ClearConnectionListView();
        if (cmbTemplates.SelectedIndex == 0)
          return;

        string templateFileName = SelectedTemplateFileName;
        if (!File.Exists(templateFileName))
        {
          Utils.ShowError(String.Format("Template file does not exist.\r\n{0}", templateFileName), MessageBoxButtons.OK);
          return;
        }

        _selectedConnections = MultiExecSpec.Load(templateFileName);
        RenderConnectionsInListView(_selectedConnections);
      }
      finally
      {
        toolStrip1.Enabled = true;
      }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      EditSelectedSpec();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
      CreateNewSpec();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (_selectedConnections == null || _selectedConnections.Count == 0)
        return;
      else
        DialogResult = DialogResult.OK;
    }

    private void btnRename_Click(object sender, EventArgs e)
    {
      RenameTemplate();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      DeleteSelectedSpec();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      RefreshTemplateList();
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      LoadSelectedTemplate();
    }

    private void btnSessionDefault_Click(object sender, EventArgs e)
    {
      Point p = this.PointToScreen(btnSessionDefault.Location);
      p.X = p.X + btnSessionDefault.Width;
      ctxSessionDefault.Show(p);

    }

    private void loadDefaultToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LoadDefaultTemplate(true);
    }

  

    private void saveAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (_selectedConnections == null || _selectedConnections.Count == 0)
      {
        Utils.ShowWarning("Connection list is empty.", MessageBoxButtons.OK);
        return;
      }

      MultiExecSpec.Save(_selectedConnections, MultiExecSpec.DefaultTemplate);
    }

    private void deleteDefaultToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!File.Exists(MultiExecSpec.DefaultTemplate) 
        || Utils.AskYesNoQuestion("Delete default connection list template?", MessageBoxDefaultButton.Button2) != DialogResult.Yes )
        return;

      File.Delete(MultiExecSpec.DefaultTemplate);
    }


	}
}