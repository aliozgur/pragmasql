using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace SQLManagement
{
  public partial class UsersList : UserControl
  {
    #region Fields And properties
    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
    }

    #endregion //Fields And properties

    #region ctor
    public UsersList()
    {
      InitializeComponent();
    }
    #endregion ctro

    #region Private Methods
    private void PopulateUsers()
    {
      bsUsers.DataSource = null;
      DataTable tbl = DbCmd.GetUsers(_cp.Database, _cp);
      bsUsers.DataSource = tbl;
    }
    #endregion //Private methods


    #region Public Methods
    public void LoadUsers(ConnectionParams cp)
    {
      if (cp == null)
      {
        throw new Exception("Connection parameters must be supplied to load data!");
      }
      _cp = cp.CreateCopy();
      PopulateUsers();
    }

    public void RefreshUsers()
    {
      PopulateUsers();
    }

    public void ModifySelectedUsers(DockContent parentContent)
    {

      if (grd.SelectedRows.Count == 0)
        return;

      try
      {
        FuzzyWait.ShowFuzzyWait("Processing users.Please wait..."); 
        foreach (DataGridViewRow row in grd.SelectedRows)
        {
          if (row.Cells[1].Value == null || row.Cells[1].GetType() == typeof(DBNull))
            continue;
          frmModifyUser.ModifyUserDlg(parentContent, _cp, row.Cells[1].Value.ToString());
        }
        FuzzyWait.CloseFuzzyWait();
      }
      catch (Exception ex)
      {
        FuzzyWait.CloseFuzzyWait();
        throw ex;
      }
    }

    public bool DropSelectedUsers(bool confirm)
    {
      if (grd.SelectedRows.Count == 0)
        return false;


      if (confirm && !MessageService.AskQuestion("Drop selected users?"))
        return false;

      //delete login from every all associated databases
      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (row.Cells[1].Value == null || row.Cells[1].GetType() == typeof(DBNull))
          continue;
        DbCmd.DropUser(_cp, row.Cells[1].Value.ToString(), _cp.Database);
      }
      RefreshUsers();
      return true;
    }

    #endregion //Public methods

    private void grd_DoubleClick(object sender, EventArgs e)
    {
      ModifySelectedUsers(this.Parent as DockContent);
    }

  }
}
