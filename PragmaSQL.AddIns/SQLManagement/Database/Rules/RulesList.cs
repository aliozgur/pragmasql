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
  public partial class RulesList : UserControl
  {
    #region Fields And properties
    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
    }

    #endregion //Fields And properties

    #region ctor
    public RulesList( )
    {
      InitializeComponent();
    }
    #endregion ctro

    #region Private Methods
    private void PopulateRules( )
    {
      bsRules.DataSource = null;
      DataTable tbl = DbCmd.GetRules(_cp, _cp.Database);
      bsRules.DataSource = tbl;
    }
    #endregion //Private methods


    #region Public Methods
    public void LoadRules( ConnectionParams cp )
    {
      if (cp == null)
      {
        throw new Exception("Connection parameters must be supplied to load data!");
      }
      _cp = cp.CreateCopy();
      PopulateRules();
    }

    public void RefreshRules( )
    {
      PopulateRules();
    }

    public void ModifySelectedRules( )
    {
      
      if (grd.SelectedRows.Count == 0)
        return;

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (!Utils.IsGridRowItemValid(row, 0) || !Utils.IsGridRowItemValid(row, 1) || !Utils.IsGridRowItemValid(row, 2))
          continue;

        frmRuleEdit.ShowRuleEditor(_cp, (int)row.Cells[0].Value, (string)row.Cells[2].Value, (string)row.Cells[1].Value, EditMode.Modify);
      }
     
    }

    public bool DropSelectedRules( bool confirm )
    {
      
      if (grd.SelectedRows.Count == 0)
        return false;

      if (confirm && !MessageService.AskQuestion("Are you sure you want to drop selected rules?"))
        return false;

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        foreach (DataGridViewRow row in grd.SelectedRows)
        {
          if (!Utils.IsGridRowItemValid(row, 0) || !Utils.IsGridRowItemValid(row, 1) || !Utils.IsGridRowItemValid(row, 2))
            continue;

          DbCmd.DropRule(conn, (int)row.Cells[0].Value, (string)row.Cells[2].Value, (string)row.Cells[1].Value);
        }
      }

      RefreshRules();
     
      return true;
    }

    #endregion //Public methods

    private void grd_DoubleClick( object sender, EventArgs e )
    {
      ModifySelectedRules();
    }

  }
}
