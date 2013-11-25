using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace SQLManagement
{
  public partial class frmDropLogin : KryptonForm
  {
    private DataTable _dbs = new DataTable();

    private string _loginName = String.Empty;
    public string LoginName
    {
      get { return _loginName; }
      private set { _loginName = value; }
    }

    private bool _isWindowsLogin = false;

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

    public static DialogResult ConfirmDropLogin(ConnectionParams cp,bool isWindowsLogin, string loginName)
    {
      frmDropLogin frm = new frmDropLogin();
      frm.label1.Text = String.Format("Do you want to drop the login with name \"{0}\"?", loginName);
      frm.LoginName = loginName;
      frm._isWindowsLogin = isWindowsLogin;
      frm.ConnectionParams = cp;
      frm.LoadDatabases();
      frm.Text = "Drop Login/User {" + loginName + "}";
      return frm.ShowDialog();
    }

    private frmDropLogin( )
    {
      InitializeComponent();
    }

    private void LoadDatabases( )
    {
      bs.DataSource = null;

      DataTable tbl = DbCmd.GetDatabasesAsDataTable(_cp);
      if (tbl.Rows.Count == 0)
        return;

      string cmdText = "declare @r table(";
      cmdText += "  databasename text null,";
      cmdText += "  username text null) " ;

      foreach (DataRow row in tbl.Rows)
      {
        cmdText += " insert into @r ";
        cmdText += "SELECT  '" + row["name"].ToString().Replace("'", "''") + "' as databasename, name as username FROM " + row["name"].ToString().Replace("'", "''") + ".dbo.sysusers WHERE SUSER_SNAME(sid)='" + _loginName.Replace("'", "''") + "'";
      }

      cmdText += "  select * from @r ";
      bs.DataSource = DbCmd.ExecuteDataTable(cmdText, _cp);
    }

    private void DropLogin( )
    {
      //delete login from every all associated databases
      foreach (DataGridViewRow row in grd.Rows)
      {
        if (checkBox1.Checked && !row.Selected)
          continue;
        if (row.Cells[1].Value == null || row.Cells[1].GetType() == typeof(DBNull))
          continue;
        if (row.Cells[0].Value == null || row.Cells[0].GetType() == typeof(DBNull))
          continue;

        DbCmd.DropUser(_cp, row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString());
      }
      
      if(!checkBox1.Checked)
        DbCmd.DeleteLogin(_cp,_isWindowsLogin, _loginName);
    }

    private void button2_Click( object sender, EventArgs e )
    {
      DropLogin();
      DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click( object sender, EventArgs e )
    {

    }
  }
}