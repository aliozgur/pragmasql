using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using ICSharpCode.Core;

namespace SQLManagement
{
  public partial class NewUser : UserControl
  {
    #region Fields And properties

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = null;
          _cp = value.CreateCopy();
          DbCmd.PopulateLoginsCombo(cmbLogin, _cp);
          PopulateRoles();
          if (cmbLogin.Items.Count > 0)
            cmbLogin.SelectedIndex = 0;
        }
        else
        {
          _cp = null;
          cmbLogin.Items.Clear();
          lvRoles.Items.Clear();
        }
      }
    }

    #endregion //Fields And properties

    #region CTOR
    public NewUser( )
    {
      InitializeComponent();
    }
    #endregion //CTOR

    #region Private methods
    
    private void PopulateRoles( )
    {
      lvRoles.Items.Clear();
      string roleName = String.Empty;
      SqlDataReader reader = null;
      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        reader = DbCmd.GetRolesAsDataReader(conn.Database, conn);
        try
        {
          while (reader.Read())
          {
            roleName = (string)reader["name"];
            if (roleName.ToLowerInvariant() == "public")
            {
              continue;
            }
            ListViewItem item = new ListViewItem(roleName);
            lvRoles.Items.Add(item);
          }
        }
        finally
        {
          if (reader != null)
            reader.Close();
        }
      }
    }

   
    private string GetSelectedRoles( )
    {
      string result = String.Empty;
      foreach (ListViewItem item in lvRoles.CheckedItems)
      {
        result += item.Text + ";";
      }
      return result;
    }

    private void DoRoleListAction( RoleListAction action )
    {
      switch (action)
      {
        case RoleListAction.UncheckAll:
          foreach (ListViewItem item in lvRoles.CheckedItems)
          {
            item.Checked = false;
          }
          break;
        case RoleListAction.CheckAll:
          foreach (ListViewItem item in lvRoles.Items)
          {
            if (item.Checked)
              continue;
            item.Checked = true;
          }
          break;
        case RoleListAction.Toggle:
          foreach (ListViewItem item in lvRoles.Items)
          {
            item.Checked = !item.Checked;
          }
          break;
        default:
          break;
      }
    }

    private bool ValidateUserDefinition( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "User definition data is not valid!\n";
      if (String.IsNullOrEmpty(cmbLogin.Text))
      {
        errorMsg += " - Login name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(txtUsername.Text))
      {
        errorMsg += ( !result ? "\n" : String.Empty ) + " - User name is empty.";
        result = false;
      }

      return result;
    }

    #endregion //Private methods

    #region Public Methods
    
    public void ResetUserDefinition( )
    {
      cmbLogin.SelectedIndex = 0;
      txtUsername.Text = String.Empty;
      foreach (ListViewItem item in lvRoles.Items)
      {
        if (item.Checked)
          item.Checked = false;
      }
    }

    public bool CreateUser( )
    {
      string errMsg = String.Empty;
      if (!ValidateUserDefinition(ref errMsg))
      {
        MessageService.ShowError(errMsg);
        return false;
      }
      string serverVersion = String.Empty;
      string cmdText = String.Empty;

      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        serverVersion = DbCmd.QueryServerVersion(conn);
        if (serverVersion == String.Empty)
        {
          MessageService.ShowError("SQL Server version can not be determined!");
          return false;
        }
        
        cmdText = DbCmd.PrepareCreateUserAndAddToRoleStatments(serverVersion, conn.Database, cmbLogin.Text, txtUsername.Text, GetSelectedRoles());
        DbCmd.ExecuteCommand(cmdText, conn);
      }
      return true;
    }

    #endregion //Public Methods

    private void checkAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DoRoleListAction(RoleListAction.CheckAll);
    }

    private void uncheckAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DoRoleListAction(RoleListAction.UncheckAll);
    }

    private void toggleToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DoRoleListAction(RoleListAction.Toggle);
    }

  }
}
