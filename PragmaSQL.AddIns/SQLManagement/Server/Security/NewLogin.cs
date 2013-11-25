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
  public partial class NewLogin : UserControl
  {
    

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      private set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
        }
        else
        {
          _cp = null;
        }
      }
    }

    private string SelDatabase
    {
      get
      {
        return cmbDb.Text == "<Default>" ? String.Empty : cmbDb.Text; 
        //cmbDb.SelectedValue != null ? cmbDb.SelectedValue.ToString() : String.Empty;
      }
    }

    private string SelLanguage
    {
      get
      {
        return cmbLanguage.Text == "<Default>" ? String.Empty : cmbLanguage.Text; 
        // cmbLanguage.SelectedValue != null ? cmbLanguage.SelectedValue.ToString() : String.Empty;
      }
    }

    private DataTable _tblDbs = new DataTable();
    private Dictionary<short, IList<ListViewItem>> _roleMap = new Dictionary<short, IList<ListViewItem>>();

    public NewLogin( ConnectionParams cp )
    {
      InitializeComponent();
      CreateDatabasesTable();
      ConnectionParams = cp;
      PopulateDbsAndLanguages();
      PopulateDatabasesAndRoles();
    }

    private void CreateDatabasesTable( )
    {
      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Boolean");
      column.ColumnName = "Map";
      _tblDbs.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.Int16");
      column.ColumnName = "Id";
      _tblDbs.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "Database";
      _tblDbs.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "User";
      _tblDbs.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "DefaulSchema";
      _tblDbs.Columns.Add(column);

    }

    private void PopulateDbsAndLanguages( )
    {
      cmbDb.Items.Clear();
      DataTable tbl = DbCmd.GetDatabasesAsDataTable(_cp);
      
      DataRow row = tbl.NewRow();
      row["description"] = "<Default>";
      row["name"] = String.Empty;
      tbl.Rows.Add(row);

      cmbDb.DataSource = tbl;
      cmbDb.DisplayMember = "description";
      cmbDb.ValueMember = "name";

      cmbLanguage.Items.Clear();
      tbl = DbCmd.GetLanguages(_cp);
      
      row = tbl.NewRow();
      row["description"] = "<Default>";
      row["alias"] = String.Empty;
      tbl.Rows.Add(row);

      cmbLanguage.DataSource = tbl;
      cmbLanguage.DisplayMember = "description";
      cmbLanguage.ValueMember = "alias";
    }

    private void PopulateDatabasesAndRoles( )
    {
      bsDbs.DataSource = null;
      _tblDbs.Clear();
      _roleMap.Clear();
      SqlDataReader reader = null;
      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        reader = DbCmd.GetDatabasesAsDataReader(conn);
        DataRow row = null;
        try
        {
          while (reader.Read())
          {
            row = _tblDbs.NewRow();
            row["Database"] = reader["name"];
            row["Id"] = reader["dbid"];
            row["Map"] = false;
            _tblDbs.Rows.Add(row);

            PopulateRoles((string)row["Database"], (short)row["Id"]);
          }
        }
        finally
        {
          if (reader != null)
            reader.Close();
        }
      }

      bsDbs.DataSource = _tblDbs;
    }

    private void PopulateRoles( string dbName, short dbId )
    {
      string roleName = String.Empty;
      SqlDataReader reader = null;
      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        reader = DbCmd.GetRolesAsDataReader(dbName, conn);
        IList<ListViewItem> roles = new List<ListViewItem>();
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
            roles.Add(item);
          }
          _roleMap.Add(dbId, roles);
        }
        finally
        {
          if (reader != null)
            reader.Close();
        }
      }
    }

    private void RenderRoles( short dbId, bool enabled )
    {
      IList<ListViewItem> items = _roleMap[dbId];
      lvRoles.Items.Clear();
      ListViewItem[] itemArray = new ListViewItem[items.Count];
      items.CopyTo(itemArray, 0);
      lvRoles.Items.AddRange(itemArray);
      lvRoles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      lvRoles.Enabled = enabled;
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

    public void ResetLoginDefinition( )
    {
      txtLoginName.Text = String.Empty;
      txtPassword.Text = String.Empty;
      txtRePassword.Text = String.Empty;


      cmbDb.SelectedIndex = cmbDb.Items.Count > 0 ? 0 : -1;
      cmbLanguage.SelectedIndex = cmbLanguage.Items.Count > 0 ? 0 : -1;

      PopulateDatabasesAndRoles();
    }

    public bool CreateLogin( )
    {
      string errMsg = String.Empty;
      if (!ValidateLoginDefinition(ref errMsg))
      {
        MessageService.ShowError(errMsg);
        return false;
      }

      string serverVersion = String.Empty;
      string cmdText = String.Empty;
      string roles = String.Empty;
      bool map = false;
      string loginName = txtLoginName.Text;
      string pwd = txtPassword.Text;
      string defdb = SelDatabase;
      string deflang = SelLanguage;
      short dbid = -1;

      string database = String.Empty;
      string user = String.Empty;
      
      using (SqlConnection conn = _cp.CreateSqlConnection(true))
      {
        serverVersion = DbCmd.QueryServerVersion(conn);
        if (serverVersion == String.Empty)
        {
          MessageService.ShowError("SQL Server version can not be determined!");
          return false;
        }

        // 1 - Create login
        try
        {
          cmdText = DbCmd.PrepareCreateLoginStatments(serverVersion, rdWindowsAuth.Checked, loginName, pwd, defdb, deflang);
          DbCmd.ExecuteCommand(cmdText, conn);
        }
        catch (Exception ex)
        {
          MessageService.ShowError(ex.Message);
          return false;
        }

        // 2 - Create user and add to roles
        try
        {
          foreach (DataRow row in _tblDbs.Rows)
          {
            map = (bool)row["Map"];
            if (!map)
              continue;

            database = (string)row["Database"];
            user = row["User"] != null && row["User"].GetType() != typeof(DBNull) ? (string)row["User"] : loginName;
            dbid = (short)row["Id"];
            roles = GetSelectedRoles(dbid);

            cmdText = DbCmd.PrepareCreateUserAndAddToRoleStatments(serverVersion,database, loginName, user, roles);
            DbCmd.ExecuteCommand(cmdText, conn);
          }
        }
        catch (Exception ex)
        {
          MessageService.ShowError(ex.Message);
          return false;
        }

      }

      return true;
    }

    private string GetSelectedRoles( short dbId )
    {
      string result = String.Empty;
      IList<ListViewItem> items = _roleMap[dbId];
      foreach (ListViewItem item in items)
      {
        if (!item.Checked)
          continue;
        result += item.Text + ";";
      }
      return result;
    }

    private bool ValidateLoginDefinition( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Login definition data is not valid!\n";
      if (String.IsNullOrEmpty(txtLoginName.Text))
      {
        errorMsg += " - Login name is empty.";
        result = false;
      }

      if (txtPassword.Text != txtRePassword.Text)
      {
        errorMsg += ( !result ? "\n" : String.Empty ) + " - Password values does not match.";
        result = false;
      }

      return result;
    }
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

    private void bsDbs_CurrentChanged( object sender, EventArgs e )
    {
      DataRowView rowView = bsDbs.Current as DataRowView;
      if (rowView != null)
      {
        RenderRoles((short)rowView["Id"], (bool)rowView["Map"]);
      }
    }

    private void rdSqlServerAuth_CheckedChanged( object sender, EventArgs e )
    {
      txtPassword.Enabled = rdSqlServerAuth.Checked;
      txtRePassword.Enabled = rdSqlServerAuth.Checked;
    }

    private void rdWindowsAuth_CheckedChanged( object sender, EventArgs e )
    {
      txtPassword.Enabled = !rdWindowsAuth.Checked;
      txtRePassword.Enabled = !rdWindowsAuth.Checked;
    }

    private void bsDbs_CurrentItemChanged( object sender, EventArgs e )
    {
      DataRowView rowView = bsDbs.Current as DataRowView;
      if (rowView != null)
      {
        bool canMap = (bool)rowView["Map"];
        lvRoles.Enabled = canMap;
      }
    }

    private void grd_CellValueChanged( object sender, DataGridViewCellEventArgs e )
    {
      if (e.RowIndex != -1 && e.ColumnIndex == 0)
      {
        bool canMap = (bool)grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        lvRoles.Enabled = canMap;
      }
    }

  }


}
