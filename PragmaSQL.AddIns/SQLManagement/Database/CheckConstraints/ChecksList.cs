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
using WeifenLuo.WinFormsUI.Docking;

namespace SQLManagement
{
  public partial class ChecksList : UserControl
  {
    #region Fields And properties
    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
    }

    #endregion //Fields And properties

    #region ctor
    public ChecksList( )
    {
      InitializeComponent();
    }
    #endregion ctro

    #region Private Methods
    private void PopulateChecks( )
    {
      bsChecks.DataSource = null;
      DataTable tbl = DbCmd.GetCheckConstraints(_cp);
      bsChecks.DataSource = tbl;
    }
    #endregion //Private methods


    #region Public Methods
    public void LoadChecks( ConnectionParams cp )
    {
      if (cp == null)
      {
        throw new Exception("Connection parameters must be supplied to load data!");
      }
      _cp = cp.CreateCopy();
      PopulateChecks();
    }

    public void RefreshChecks( )
    {
      PopulateChecks();
    }

    public void ModifySelectedChecks( )
    {
      
      
      if (grd.SelectedRows.Count == 0)
        return;

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (!Utils.IsGridRowItemValid(row, 0) || !Utils.IsGridRowItemValid(row, 1) || !Utils.IsGridRowItemValid(row, 2))
          continue;

        frmCheckEdit.ShowCheckEditor(_cp, EditMode.Modify, 
          (string)row.Cells[1].Value
          , (string)row.Cells[2].Value
          , (int)row.Cells[0].Value
          , (string)row.Cells[3].Value
          , ((string)row.Cells[4].Value).ToLower() == "enabled"
          , ((string)row.Cells[5].Value).ToLower() == "yes");
      }
     
    }

    public bool DropSelectedChecks( bool confirm )
    {
      
      if (grd.SelectedRows.Count == 0)
        return false;

      if (confirm && !MessageService.AskQuestion("Are you sure you want to drop selected check constraints?"))
        return false;

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        foreach (DataGridViewRow row in grd.SelectedRows)
        {
          if (!Utils.IsGridRowItemValid(row, 1) || !Utils.IsGridRowItemValid(row, 2) || !Utils.IsGridRowItemValid(row, 3))
            continue;

          DbCmd.DropCheck(conn, (string)row.Cells[1].Value, (string)row.Cells[2].Value, (string)row.Cells[3].Value);
        }
      }

      RefreshChecks();
      return true;
    }

    #endregion //Public methods

    private void grd_DoubleClick( object sender, EventArgs e )
    {
      ModifySelectedChecks();
    }

  }
}
