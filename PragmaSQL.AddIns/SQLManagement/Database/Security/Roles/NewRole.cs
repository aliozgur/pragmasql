using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class NewRole : UserControl
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
          DbCmd.PopulateOwnerCombo(cmbOwner,_cp,_cp.Database);
          
          if (cmbOwner.Items.Count > 0)
            cmbOwner.SelectedIndex = 0;
        }
        else
        {
          _cp = null;
          cmbOwner.Items.Clear();
        }
      }
    }

    #endregion //Fields And properties

    public NewRole( )
    {
      InitializeComponent();
    }
    
    private bool ValidateRoleDefinition( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Role definition data is not valid!\n";
      if (String.IsNullOrEmpty(cmbOwner.Text))
      {
        errorMsg += " - Owner name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(txtName.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Role name is empty.";
        result = false;
      }

      return result;
    }


    public bool CreateRole( )
    {
      string err = String.Empty;
      if (!ValidateRoleDefinition(ref err))
      {
        MessageService.ShowError(err);
        return false;
      }

      using(SqlConnection conn = _cp.CreateSqlConnection(true,false))
      {
        string serverVersion = DbCmd.QueryServerVersion(conn);
        DbCmd.CreateRole(serverVersion, conn, _cp.Database, txtName.Text, cmbOwner.Text, rbStandart.Checked, txtPwd.Text);
      }

      return true;
    }

    public void ResetRoleDefinition( )
    {
      if (cmbOwner.Items.Count > 0)
        cmbOwner.SelectedIndex = 0;
      txtName.Text = String.Empty;
      rbStandart.Checked = true;
    }
    private void rbStandart_CheckedChanged( object sender, EventArgs e )
    {
      txtPwd.ReadOnly = !rbStandart.Checked;
    }
  }
}
