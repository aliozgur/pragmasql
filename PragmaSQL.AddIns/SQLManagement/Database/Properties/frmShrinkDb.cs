using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmShrinkDb : Form
  {
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
        }
        else
        {
          _cp = null;
        }
      }
    }

    #region Static Show Dialog
    public static DialogResult ShowShrinkDbDialog( ConnectionParams cp )
    {
      frmShrinkDb frm = new frmShrinkDb();
      frm.ConnectionParams = cp;
      frm.lblInfo.Text = "Shrink database with name \"" + cp.Database + "\".";
      return frm.ShowDialog();
    }

    #endregion //Static Show Dialog

    public frmShrinkDb( )
    {
      InitializeComponent();
      cmbOption.SelectedIndex = 0;
    }

    private bool ValidateOptions( ref string errorMsg )
    {
      bool result = true;
      double value = 0;

      errorMsg = "Shrink options listed below are not valid!\n";
      if (String.IsNullOrEmpty(cmbOption.Text))
      {
        errorMsg += " - Shrink option.";
        result = false;
      }

      if (String.IsNullOrEmpty(txtPrecent.Text) || !Double.TryParse(txtPrecent.Text, out value))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Shrink percentage.";
        result = false;
      }

      return result;
    }

    private void btnShrink_Click( object sender, EventArgs e )
    {
      string err = String.Empty;
      if (!ValidateOptions(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      using(SqlConnection conn = _cp.CreateSqlConnection(true,false))
      {
        DbCmd.ShrinkDB(conn, conn.Database, txtPrecent.Text, cmbOption.Text);
        DialogResult = DialogResult.OK;
      }
    }
  }
}