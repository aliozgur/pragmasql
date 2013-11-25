using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PragmaSQL.Core;
using System.Data.SqlClient;
using ICSharpCode.Core;
using ComponentFactory.Krypton.Toolkit;
namespace PragmaSQL
{
	public partial class frmMultiDbTemplateEditor : KryptonForm
	{
		#region Fields And Properties

    private bool _isNewTemplate = true;
		private IList<ConnectionParams> _cpList = new List<ConnectionParams>();

		private ConnectionParamsCollection Connections
		{
			get
			{
				return ConnectionParamsFactory.GetConnections();
			}
		}
		
		private ConnectionParams _currentConnPrm;
		public ConnectionParams CurrentConnPrm
		{
			get { return _currentConnPrm; }
		}

		private SqlConnection _conn = null;
		private int _currentServerIndex = -1;
		private bool _initializing = false;

		private SerializableDictionary<string, ConnectionParams> _selectedConnections;
		private IDictionary<string, ListViewGroup> _groups = new Dictionary<string, ListViewGroup>();

		#endregion //Fields And Properties

		#region CTOR
    public frmMultiDbTemplateEditor()
		{
			InitializeComponent();

		}
		#endregion //CTOR

		#region Static Methods

    public static MultiDbTemplateEditorResult CreateNewSpec()
    {
      frmMultiDbTemplateEditor frm = new frmMultiDbTemplateEditor();
      frm.PrepareNew();

      MultiDbTemplateEditorResult result = new MultiDbTemplateEditorResult();
      if (frm.ShowDialog() == DialogResult.OK)
      {
        result.DialogResult = DialogResult.OK;
        result.Connections = new SerializableDictionary<string, ConnectionParams>(frm._selectedConnections);
        result.TemplateName = frm.tbName.Text;
      }
      else
      {
        result.DialogResult = DialogResult.Cancel;
      }

      return result;
    }

    public static MultiDbTemplateEditorResult EditSpec(string templateName)
    {
      frmMultiDbTemplateEditor frm = new frmMultiDbTemplateEditor();
      frm._isNewTemplate = false;
      frm.tbName.ReadOnly = true;
      frm.PrepareEdit(templateName);
      
      MultiDbTemplateEditorResult result = new MultiDbTemplateEditorResult();
      if (frm.ShowDialog() == DialogResult.OK)
      {
        result.DialogResult = DialogResult.OK;
        result.Connections = new SerializableDictionary<string, ConnectionParams>(frm._selectedConnections);
        result.TemplateName = templateName;
      }
      else
      {
        result.DialogResult = DialogResult.Cancel;
      }

      return result;
    }

    #endregion //Static Methods

    private void PrepareNew()
    {
      PopulateServers();
      PopulateDatabases();
      _selectedConnections = new SerializableDictionary<string, ConnectionParams>();
    }

    private void PrepareEdit(string templateName)
    {
      string templateFile = MultiExecSpec.TemplatesDirectory + templateName + MultiExecSpec.TemplateFileExt;
      try
      {
        _initializing = true;
        tbName.Text = templateName;
        _selectedConnections = MultiExecSpec.Load(templateFile);
        PopulateServers();
        PopulateDatabases();
        RenderConnectionsInListView(_selectedConnections);
      }
      finally
      {
        _initializing = false;
      } 

    }


		

		private void RenderConnectionsInListView(IDictionary<string,ConnectionParams> collection)
		{
			if (collection == null)
				return;

			foreach (ConnectionParams cp in collection.Values)
			{
				RenderConnectionInList(cp);
			}
		}

		private void AddConnection(ConnectionParams cp)
		{
			string key = ConnectionParams.PrepareConnKeyWithDb(cp);
			
			if (_selectedConnections.ContainsKey(key))
				return;

			ConnectionParams tmp = cp.CreateCopy();
			_selectedConnections.Add(key, tmp);
			RenderConnectionInList(tmp);
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

		private void RemoveSelected()
		{
			ListViewItem item = null;
			ConnectionParams cp = null;
			string key = null;

			while (lv.SelectedItems.Count > 0)
			{
				item = lv.SelectedItems[0];
				cp = item.Tag as ConnectionParams;
				lv.Items.Remove(item);
				if (cp == null)
					continue;

				key = ConnectionParams.PrepareConnKeyWithDb(cp);
				if (_selectedConnections.ContainsKey(key))
					_selectedConnections.Remove(key);
			}
		}

		private void PopulateServers()
		{
			_cpList.Clear();
			ConnectionParamsCollection cons = Connections;
			foreach (ConnectionParams cp in cons)
			{
				//if (cmbServers.FindStringExact(cp.Server) != -1)
				if (cmbServers.FindStringExact(ConnectionParams.PrepareConnKey(cp)) != -1)
					continue;

				//cmbServers.Items.Add(cp.Server);
				cmbServers.Items.Add(ConnectionParams.PrepareConnKey(cp));
				_cpList.Add(cp);
        
			}
      cmbServers.Text = "<Select Server>";
      _currentServerIndex = -1;

		}

    private void PopulateDatabases(string defaultDatabaseName)
    {
      cmbDatabases.Items.Clear();
      cmbDatabases.Text = "<Select Database>";
      if (_conn == null || _conn.State != ConnectionState.Open)
      {
        return;
      }

      DataTable dbs = _conn.GetSchema("Databases");

      dbs.DefaultView.Sort = "database_name";
      dbs = dbs.DefaultView.ToTable();

      int dbIndex = -1;
      foreach (DataRow row in dbs.Rows)
      {
        string dbName = (string)row["database_name"];

        cmbDatabases.Items.Add(dbName);
        if (defaultDatabaseName.ToLowerInvariant() == dbName.ToLowerInvariant())
        {
          cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
          dbIndex = cmbDatabases.SelectedIndex;
        }
      }

      if (dbIndex == -1)
      {
        cmbDatabases.Items.Add(defaultDatabaseName);
        cmbDatabases.SelectedIndex = cmbDatabases.Items.Count - 1;
      }
    }

    private void PopulateDatabases()
		{
      PopulateDatabases(String.Empty);
		}

		private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_initializing)
			{
				return;
			}

      cmbDatabases.Items.Clear();
			ConnectionParams cp = _cpList[cmbServers.SelectedIndex];
			_currentConnPrm = cp.CreateCopy();

			try
			{
				if (_conn != null)
				{
					if (_conn.State == ConnectionState.Open)
						_conn.Close();
					_conn.Dispose();
				}

				_conn = cp.CreateSqlConnection(true, false);
				_currentServerIndex = cmbServers.SelectedIndex;
				if (String.IsNullOrEmpty(cp.Database))
				{
					cp.Database = "master";
				}
				PopulateDatabases(cp.Database);
			}
			catch
			{
				MessageBox.Show("Can not connect to selected server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _initializing = true;
        _currentServerIndex = -1;
        _initializing = false;
      }
		}

		private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_initializing)
			{
				return;
			}

			if (_conn.State != ConnectionState.Open)
			{
				throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
			}


			string tmp = String.Empty;
			try
			{
				tmp = _conn.Database;
				_conn.ChangeDatabase(cmbDatabases.Text);
				_currentConnPrm.Database = cmbDatabases.Text;
			}
			catch
			{
				MessageBox.Show("Can not change connection to selected database!\nPrevious database will be restored.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_conn.ChangeDatabase(tmp);
				_initializing = true;
				cmbDatabases.Text = tmp;
				_currentConnPrm.Database = tmp;
				_initializing = false;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
      if (_currentConnPrm == null)
        return;
			AddConnection(_currentConnPrm);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count == 0 || !MessageService.AskQuestion("Remove selected connections from list?"))
				return;
			RemoveSelected();
		}

		private void frmMultiConnectionSpec_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_conn != null)
				_conn.Dispose();
		}

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_selectedConnections.Count == 0)
      {
        Utils.ShowWarning("Connection list is empty.",MessageBoxButtons.OK);
        return;
      }

      string templatesFolder = MultiExecSpec.TemplatesDirectory;
      if(!Directory.Exists(templatesFolder))
        Directory.CreateDirectory(templatesFolder);

      string templateName = tbName.Text.Trim();
      if (_isNewTemplate && String.IsNullOrEmpty(templateName))
      {
        Utils.ShowError("Template name is empty!",MessageBoxButtons.OK);
        return;
      }

      string fileName = templatesFolder + templateName + MultiExecSpec.TemplateFileExt;
      if (_isNewTemplate && File.Exists(fileName))
      {
        Utils.ShowError(String.Format("Template with name \'{0}\' already exists!",tbName.Text),MessageBoxButtons.OK);
        return;
      }

      MultiExecSpec.Save(_selectedConnections, fileName);
      DialogResult = DialogResult.OK;
    }
	
	}

  public struct MultiDbTemplateEditorResult
  {
    public string TemplateName;
    public DialogResult DialogResult;
    public SerializableDictionary<string, ConnectionParams> Connections;
  }

}