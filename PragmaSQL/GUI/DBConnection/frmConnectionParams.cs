using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;

using System.Data.SqlClient;

using PragmaSQL.Database;

namespace PragmaSQL.GUI
{
  public partial class frmConnectionParams : Form
  {
    public frmConnectionParams()
    {
      InitializeComponent();
    }

    public frmConnectionParams(ConnectionParams cp, bool canEditFriendlyName):this(canEditFriendlyName)
    {
      if (cp != null)
      {
        RenderConnectionParams(cp);
      }
    }

    public frmConnectionParams(bool canEditFriendlyName)
    {
      InitializeComponent();
      txtFriendlyName.Text = String.Empty;
      txtFriendlyName.Enabled = canEditFriendlyName;

    }

    public void RenderConnectionParams(ConnectionParams cp)
    {
      //txtServer.Enabled = false;
      txtServer.Text = cp.Name;
      txtDefultDB.Text = cp.InitialCatalog;
      txtTimeOut.Text = cp.TimeOut;
      if (cp.FriendlyName != null)
        txtFriendlyName.Text = cp.FriendlyName;

      if (cp.IntegratedSecurity.Length == 0)
      {
        rbUseIntegratedSecurity.Checked = false;
        txtUserName.Text = cp.UserName;
        txtPassword.Text = cp.Password;
      }
      else
      {
        rbUseIntegratedSecurity.Checked = true;
        txtUserName.Text = "";
        txtPassword.Text = "";
      }    
    }

    public ConnectionParams GetCurrentConnectionSpec()
    {
      ConnectionParams connSpec = new ConnectionParams();
      connSpec.ID = Guid.NewGuid();
      connSpec.InitialCatalog = txtDefultDB.Text;
      if (rbUseIntegratedSecurity.Checked)
        connSpec.IntegratedSecurity = "SSPI";
      else
        connSpec.IntegratedSecurity = "";
      
      connSpec.IsConnected = true;
      connSpec.Name = txtServer.Text;
      connSpec.PersistSecurityInfo = "TRUE";
      connSpec.TimeOut = txtTimeOut.Text;
      connSpec.UserName = txtUserName.Text;
      connSpec.Password = txtPassword.Text;
      connSpec.FriendlyName = txtFriendlyName.Text;
      return connSpec;
    }

    private void PopulateServerNames()
    {
      if (txtServer.Items.Count > 0)
      {
        return;
      }

      DataTable  servers = SqlDataSourceEnumerator.Instance.GetDataSources();
      foreach (DataRow row in servers.Rows)
      {
        txtServer.Items.Add((string)row["ServerName"]);
      }
      if (servers.Rows.Count == 0)
      {
        txtServer.Items.Add("(local)");
      }
    }

    private bool TestConnection(bool showErrorDialog)
    {
      bool result = false;
      using(SqlConnection conn = new SqlConnection() )
      {
        conn.ConnectionString = GetCurrentConnectionSpec().ConnectionString;
        try
        {
          conn.Open();
          result = true;
        }
        catch(Exception ex)
        {
          if (showErrorDialog)
          {
            MessageBox.Show(ex.Message, "Connection Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
          }
          else
          {
            throw ex;
          }
        }
      }

      return result;
    }

    private void rbUseIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
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
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.None;
      if (String.IsNullOrEmpty(txtDefultDB.Text))
      {
        MessageBox.Show("Default database not specified.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        return;
      }
      else
      {
        DialogResult = DialogResult.OK;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (TestConnection(true))
      {
        MessageBox.Show("Connection is sucessfull.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);          
      }
    }

    private void txtServer_DropDown(object sender, EventArgs e)
    {
      PopulateServerNames();
    }
  }
}