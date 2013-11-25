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
  public partial class UserRoleEdit : UserControl
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
          PopulateRoles();
        }
        else
        {
          _cp = null;
          bsRoles.DataSource = null;
          _tblRoles.Clear();
        }
      }
    }

    private string _userName = String.Empty;
    public string UserName
    {
      get { return _userName; }
      set { _userName = value; }
    }

    private DataTable _tblRoles = new DataTable();

    #endregion //Fields And properties

    #region CTOR
    public UserRoleEdit( )
    {
      InitializeComponent();
      CreateRolesTable();
      SetModified(false);
    }

    #endregion //CTOR

    #region Private Methods
    
    private void CreateRolesTable( )
    {
      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "isin";
      _tblRoles.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "role";
      _tblRoles.Columns.Add(column);
    }


    public void PopulateRoles( )
    {
      bsRoles.DataSource = null;
      _tblRoles.Clear();
      _tblRoles = DbCmd.GetUserRoleMap(_cp, UserName);
      _tblRoles.ColumnChanged += new DataColumnChangeEventHandler(_tblRoles_ColumnChanged);
      bsRoles.DataSource = _tblRoles;
      SetModified(false);
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

    private void UpdateRoles( )
    {
      string errorText = "Roles can not be changed!\n";
      string action = String.Empty;
      bool hasErrors = false;
      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        foreach (DataRow row in _tblRoles.Rows)
        {
          if (!Utils.IsRowItemValid(row,0) ||  !Utils.IsRowItemValid(row,1))
            continue;

          try
          {
            if ((bool)row.ItemArray[0])
            {
              if (DbCmd.IsUserInRole(conn, _userName, (string)row.ItemArray[1]))
                continue;
              action = "ADD";
              DbCmd.AddUserToRole(conn, _userName, (string)row.ItemArray[1]);
            }
            else
            {
              if (!DbCmd.IsUserInRole(conn, _userName, (string)row.ItemArray[1]))
                continue;

              action = "DROP";
              DbCmd.DropUserFromRole(conn, _userName, (string)row.ItemArray[1]);
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

    #endregion //Private Methods

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      UpdateRoles();
    }
  }
}
