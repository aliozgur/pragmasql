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
  public partial class RoleUsersEdit : UserControl
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
          PopulateUsers();
        }
        else
        {
          _cp = null;
          bsUsers.DataSource = null;
          _tblUsers = null;

        }
      }
    }

    private string _roleName = String.Empty;
    public string RoleName
    {
      get { return _roleName; }
      set { _roleName = value; }
    }

    private DataTable _tblUsers = new DataTable();

    #endregion //Fields And properties

    #region CTOR
    public RoleUsersEdit( )
    {
      InitializeComponent();
      SetModified(false);
    }

    #endregion //CTOR

    #region Private Methods
    
    

    public void PopulateUsers( )
    {
      bsUsers.Sort = String.Empty;
      bsUsers.Filter = String.Empty;

      bsUsers.DataSource = null;
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        _tblUsers = DbCmd.GetUsersInRole(conn,conn.Database, _roleName);
        _tblUsers.ColumnChanged += new DataColumnChangeEventHandler(_tblRoles_ColumnChanged);
        bsUsers.DataSource = _tblUsers;
        bsUsers.Sort = "isin DESC";
        SetModified(false);
      }
    }

    void _tblRoles_ColumnChanged( object sender, DataColumnChangeEventArgs e )
    {
      SetModified(true);
    }

    private void SetModified( bool value )
    {
      lblModified.Visible = value;
      pbModified.Visible = value;
    }

    private void UpdateUsers( )
    {
      string errorText = "Users in role can not be changed!\n";
      string action = String.Empty;
      bool hasErrors = false;
      bool isin = false;
      bool original = false;
      string userName = String.Empty;

      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        foreach (DataRow row in _tblUsers.Rows)
        {
          if (!Utils.IsRowItemValid(row, 0) || !Utils.IsRowItemValid(row, 1))
            continue;

          try
          {
            isin = (bool)row[1];
            original = (bool)row[2];

            if (isin == original)
              continue;

            userName = (string)row[0];
            if (isin)
            {
              action = "ADD";
              DbCmd.AddUserToRole(conn, userName, _roleName);
            }
            else
            {
              action = "DROP";
              DbCmd.DropUserFromRole(conn, userName, _roleName);
            }
          }
          catch (Exception ex)
          {
            errorText += "Action: " + action + " , Role: " + (string)row.ItemArray[1] + ", Error:" + ex.Message + "\n";
            hasErrors = true;
          }
        }
      }

      if (hasErrors)
      {
        MessageService.ShowError(errorText);
      }

      SetModified(false);
    }

    private void FilterIsIn(bool clearFilter )
    {
      if (bsUsers.DataSource == null)
        return;

      if (!clearFilter)
        bsUsers.Filter = "isin = true";
      else
        bsUsers.Filter = String.Empty;
    }

    #endregion //Private Methods

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      UpdateUsers();
    }

    private void chkFilterIn_CheckedChanged( object sender, EventArgs e )
    {
      FilterIsIn(!chkFilterIn.Checked);
    }
  }
}
