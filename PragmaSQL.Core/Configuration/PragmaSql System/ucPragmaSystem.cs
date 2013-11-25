using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Reflection;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public partial class ucPragmaSqlSystem : UserControl, IConfigContentEditor
  {
    private bool _isInitializing = false;
    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private ConnectionParams _connParams = null;
    public ConnectionParams ConnParams
    {
      get { return _connParams; }
      set { _connParams = value; }
    }

    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }

    public ucPragmaSqlSystem( )
    {
      InitializeComponent();
    }

    #region IConfigurationContentItem Members

    public string ItemClassName
    {
      get
      {
        return "PragmaSqlSystem";
      }
    }

    public bool ContentLoaded
    {
      get
      {
        return _isContentLoaded;
      }
    }

    public bool Modified
    {
      get
      {
        return _isModified;
      }
    }

    public bool LoadContent( )
    {
      return LoadContent(ConfigHelper.Current);
    }

    public bool LoadContent( ConfigurationContent configContent )
    {
      if (configContent == null)
      {
        throw new NullParameterException("Configuration content param is null!");
      }

      if (configContent.PragmaSqlDbConn == null)
      {
        throw new NullPropertyException("Configuration content does not contain PragmaSqlDbConn item!");
      }

      _configContent = configContent;
      _connParams = _configContent.PragmaSqlDbConn;


      LoadInitial();

      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      _connParams.Database = txtDefultDB.Text;
      if (rbUseIntegratedSecurity.Checked)
        _connParams.IntegratedSecurity = "SSPI";
      else
        _connParams.IntegratedSecurity = "";

      _connParams.IsConnected = true;
      _connParams.Server = txtServer.Text;
      _connParams.PersistSecurityInfo = "TRUE";
      _connParams.TimeOut = txtTimeOut.Text;
      _connParams.UserName = txtUserName.Text;
      _connParams.Password = txtPassword.Text;

      _configContent.PragmaSql_UtilsDisabled = chkUtilsDisabled.Checked;
      _configContent.PragmaSql_ObjectChangeHistoryLogEnabled = chkObjectLoggingEnabled.Checked;
      _configContent.PragmaSql_SharedScriptsEnabled = chkScriptSharingEnabled.Checked;
      _configContent.PragmaSql_SharedSnippetsEnabled = chkUseSharedSnippets.Checked;

      _isModified = false;
      return true;
    }

    public void ShowContent( )
    {
      this.Show();
    }

    public void HideContent( )
    {
      this.Hide();
    }

    #endregion

    #region Methods

    private void LoadInitial( )
    {
      _isInitializing = true;
      RenderConnectionParams();
      _isInitializing = false;
    }

    public void RenderConnectionParams( )
    {
      txtServer.Text = _connParams.Server;
      txtDefultDB.Text = _connParams.Database;
      txtTimeOut.Text = _connParams.TimeOut;

      txtUserName.Text = _connParams.UserName;
      txtPassword.Text = _connParams.Password;

      if (_connParams.IntegratedSecurity.Length == 0)
      {
        rbUseIntegratedSecurity.Checked = false;
      }
      else
      {
        rbUseIntegratedSecurity.Checked = true;
      }

      chkUtilsDisabled.Checked = _configContent.PragmaSql_UtilsDisabled;
      chkObjectLoggingEnabled.Checked = _configContent.PragmaSql_ObjectChangeHistoryLogEnabled;
      chkScriptSharingEnabled.Checked = _configContent.PragmaSql_SharedScriptsEnabled;
      chkUseSharedSnippets.Checked = _configContent.PragmaSql_SharedSnippetsEnabled;

      groupBox2.Enabled = !chkUtilsDisabled.Checked;
      groupBox3.Enabled = !chkUtilsDisabled.Checked;

    }

    public ConnectionParams GetCurrentConnectionSpec( )
    {
      ConnectionParams connSpec = new ConnectionParams();
      connSpec.ID = Guid.NewGuid();
      connSpec.Database = txtDefultDB.Text;
      if (rbUseIntegratedSecurity.Checked)
        connSpec.IntegratedSecurity = "SSPI";
      else
        connSpec.IntegratedSecurity = "";

      connSpec.IsConnected = true;
      connSpec.Server = txtServer.Text;
      connSpec.PersistSecurityInfo = "TRUE";
      connSpec.TimeOut = txtTimeOut.Text;
      connSpec.UserName = txtUserName.Text;
      connSpec.Password = txtPassword.Text;
      return connSpec;
    }

    private bool TestConnection( bool showErrorDialog )
    {
      bool result = false;

      using (SqlConnection conn = new SqlConnection())
      {
        conn.ConnectionString = GetCurrentConnectionSpec().ConnectionString;
        try
        {
          conn.Open();
          result = true;
        }
        catch (Exception ex)
        {
          if (showErrorDialog)
          {
            MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
          else
          {
            throw ex;
          }
        }
      }
      return result;
    }

    private void PopulateServerNames( )
    {
      if (txtServer.Items.Count > 0)
      {
        return;
      }

      DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
      foreach (DataRow row in servers.Rows)
      {
        txtServer.Items.Add((string)row["ServerName"]);
      }
      if (servers.Rows.Count == 0)
      {
        txtServer.Items.Add("(local)");
      }
    }

    #endregion

    private void rbUseIntegratedSecurity_CheckedChanged( object sender, EventArgs e )
    {
      if (rbUseIntegratedSecurity.Checked)
      {
        txtUserName.Enabled = false;
        txtPassword.Enabled = false;
      }
      else
      {
        txtUserName.Enabled = true;
        txtPassword.Enabled = true;
      }

      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }


    private void txtServer_DropDown( object sender, EventArgs e )
    {
      PopulateServerNames();
    }

    private void txtDefultDB_TextChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      _isModified = true;
    }

    private void txtServer_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      _isModified = true;

    }


    private void chkUtilsDisabled_CheckedChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      groupBox2.Enabled = !chkUtilsDisabled.Checked;
      groupBox3.Enabled = !chkUtilsDisabled.Checked;

      _isModified = true;
    }

    private void chkObjectLoggingEnabled_CheckedChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }

      _isModified = true;

    }

    private void txtServer_TextChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

    private void label2_Click( object sender, EventArgs e )
    {
      string msgStr = "PragmaSQL Editor uses its own SQL Server database to\n\n"
        + "1) Log changes for stored procedures, functions, triggers and views\n"
        + "   every time they are commited to related database\n"
        + "2) Enable code snippet sharing\n"
        + "3) Enable script sharing\n\n"
        + "You have to install PragmaSQL database and specify connection parameters\n"
        + "in order to be able to use these enhancements.";
      MessageBox.Show(msgStr, "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
    }

    private void button1_Click( object sender, EventArgs e )
    {
      if (TestConnection(true))
      {
        MessageBox.Show("Connection is sucessfull.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void button2_Click( object sender, EventArgs e )
    {
      ConnectionParams cp = GetCurrentConnectionSpec();
      cp.Database = "master";
      string msg =
        "PragmaSQL System Database will be installed to:" + "\n"
        + "  * Server : " + cp.Server + "\n"
        + "  * Required Credentials : Database owner access for \"" + cp.CurrentUsername + "\" on database \"" + cp.Database + "\""
        + "\n\n"  
        + "Do you want to install PragmaSQL System Database?";
      
      DialogResult dlgRes = MessageBox.Show(msg, "Install Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
      if (dlgRes != DialogResult.Yes)
      {
        return;
      }

      string installScript = SysDatabaseInstaller.GetDatabaseInstallScript();
      installScript = String.Format(installScript, txtDefultDB.Text.ToLowerInvariant(), txtDefultDB.Text);
      using (SqlConnection conn = new SqlConnection(cp.ConnectionString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(installScript, conn);
        try
        {
          cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
      }

      cp.Database = txtDefultDB.Text;
      installScript = SysDatabaseInstaller.GetDatabaseObjectInstallScript();
      using (SqlConnection conn = new SqlConnection(cp.ConnectionString))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(installScript, conn);
        cmd.ExecuteNonQuery();
      }
      MessageBox.Show("PragmaSQL System Database was installed.", "Install Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

    }
  }
}
