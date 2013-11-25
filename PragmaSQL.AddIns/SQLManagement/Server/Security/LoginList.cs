using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class LoginList : UserControl
  {

    private DataTable _tbl = new DataTable();

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
    }

    
    public LoginList( )
    {
      InitializeComponent();
    }


    public void RefreshLogins( )
    {
      PopulateLogins();    
    }

    public void LoadLogins( ConnectionParams cp )
    {
      if (cp == null)
      {
        throw new Exception("Connection parameters must be supplied to load data!");
      }
      _cp = cp.CreateCopy();
      PopulateLogins();
    }

    private void PopulateLogins( )
    {
      string cmdText = "USE [master]; ";
      cmdText += " SELECT sid,loginname, isntname, isntuser, hasaccess, dbname, createdate, master.dbo.syslanguages.alias as language"
        + " FROM master.dbo.syslogins , master.dbo.syslanguages"
        + " WHERE ((master.dbo.syslogins.language like master.dbo.syslanguages.alias or master.dbo.syslogins.language like master.dbo.syslanguages.name) )"
        + " ORDER BY loginname";

			using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
			{
				SqlDataAdapter adapter = new SqlDataAdapter(cmdText, conn);
				_tbl.Clear();
				adapter.Fill(_tbl);
			}
			bs.DataSource = _tbl;
    }

    public bool DropSelected( )
    {
      bool shallRefresh = false;
      bool isWindowsLogin = false;

      if (grd.SelectedRows.Count == 0)
        return shallRefresh;

      bool hasErrors = false;
      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (row.Cells[0].Value == null || row.Cells[0].GetType() == typeof(DBNull))
        {
          continue;
        }

        try
        {
          isWindowsLogin = false;
          if (! ( row.Cells[1].Value == null || row.Cells[1].GetType() == typeof(DBNull)) )
          {
            if (row.Cells[1].Value.ToString() == "1")
              isWindowsLogin = true;
          }

          if (frmDropLogin.ConfirmDropLogin(_cp,isWindowsLogin, row.Cells[0].Value.ToString()) == DialogResult.OK && shallRefresh == false)
          {
            shallRefresh = true;
          }
        }
        catch (Exception ex)
        {
          hasErrors = true;
          HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex.Message);
        }
      }

      if (hasErrors)
      {
        HostServicesSingleton.HostServices.MsgService.ShowMessages();
      }

      return shallRefresh;
    }

    public bool ModifySingleLogin( )
    {
      string loginName = String.Empty;
      string defaultDb = String.Empty;
      string defaultLanguage = String.Empty;

      DataRowView row = bs.Current as DataRowView;
      if (row == null)
        return false;

      loginName = row["loginname"] != null && row["loginname"].GetType() != typeof(DBNull) ? (string)row["loginname"] : String.Empty;
      defaultDb = row["dbname"] != null && row["dbname"].GetType() != typeof(DBNull) ? (string)row["dbname"] : String.Empty;
      defaultLanguage = row["language"] != null && row["language"].GetType() != typeof(DBNull) ? (string)row["language"] : String.Empty;

      if (String.IsNullOrEmpty(loginName))
      {
        MessageService.ShowError("Selected login is not valid!");
        return false;
      }

      return frmModifyLogin.ShowLoginDetails(_cp, loginName, defaultDb, defaultLanguage);
    }
    
    public void ChangePwd(string login)
    {
      string pwd = String.Empty;
      if (!PwdDialog.ShowPwdDialog("Change Password [" + login + "]", ref pwd))
        return;
      DbCmd.ChangeLoginPwd(_cp, login, pwd);
    }


    public void ChangePwdOfCurrentLogin()
    {
      string loginName = String.Empty;

      DataRowView row = bs.Current as DataRowView;
      if (row == null)
        return;

      loginName = row["loginname"] != null && row["loginname"].GetType() != typeof(DBNull) ? (string)row["loginname"] : String.Empty;
      ChangePwd(loginName);
    }


    private void grd_CellPainting( object sender, DataGridViewCellPaintingEventArgs e )
    {
      if (e.RowIndex != -1 && e.ColumnIndex == 1)
      {
        Color bgColor = grd.BackgroundColor;
        Brush br = SystemBrushes.ControlText;
        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
        {
          bgColor = SystemColors.Highlight;
          br = SystemBrushes.HighlightText;

        }

        using (
                   Brush gridBrush = new SolidBrush(grd.GridColor),
                   backColorBrush = new SolidBrush(bgColor))
        {


          using (Pen gridLinePen = new Pen(gridBrush))
          {

            // Erase the cell.
            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            // Draw the grid lines (only the right and bottom lines;
            // DataGridView takes care of the others).
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                e.CellBounds.Bottom - 1);
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                e.CellBounds.Top, e.CellBounds.Right - 1,
                e.CellBounds.Bottom);

            // Draw the inset highlight box.
            //e.Graphics.DrawRectangle(Pens.Blue, newRect);



            object val = e.Value;
            if (val == null || val.GetType() == typeof(DBNull))
            {
              return;
            }

            string isntname = val.ToString();

            string isntuser = grd.Rows[e.RowIndex].Cells[2] != null && grd.Rows[e.RowIndex].Cells[2].GetType() != typeof(DBNull) 
                ? grd.Rows[e.RowIndex].Cells[2].ToString()  
                : String.Empty;

            string type = String.Empty;
            switch (isntname)
            {
              case "1":
                switch (isntuser)
                {
                  case "1":
                    type = "NT User";
                    break;
                  default:
                    type = "NT Group";
                    break;
                }
                break;
              default:
                type = "Standard";
                break;
            }

            e.Graphics.DrawString(type.ToString(), e.CellStyle.Font,
                  br, e.CellBounds.X + 2,
                  e.CellBounds.Y + 2, StringFormat.GenericDefault);
            e.Handled = true; 
          }
        }
      }
    }

    private void grd_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex < 0 || e.RowIndex < 0)
        return;

      DataGridViewColumn col = grd.Columns[e.ColumnIndex];
      if (col == colChangePwd)
      {
        ChangePwd((string)grd.CurrentRow.Cells[colLoginName.Index].Value);
      }
    }

  }
}
