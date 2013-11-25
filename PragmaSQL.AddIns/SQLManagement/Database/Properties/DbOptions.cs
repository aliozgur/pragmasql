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
  public partial class DbOptions : UserControl
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
          PopulateOptions();
        }
        else
        {
          _cp = null;
          bs.DataSource = null;
        }
      }
    }

    private DataTable _tbl = null;

    #endregion //Fields And properties

    public DbOptions( )
    {
      InitializeComponent();
      SetModified(false);
    }

    private void _tbl_ColumnChanged( object sender, DataColumnChangeEventArgs e )
    {
      SetModified(true);
    }

    private void SetModified( bool value )
    {
      lblModified.Visible = value;
      pbModified.Visible = value;
    }

    public void PopulateOptions( )
    {
      bs.DataSource = null;
      if (_tbl != null)
      {
        _tbl.ColumnChanged -= new DataColumnChangeEventHandler(_tbl_ColumnChanged);
        _tbl.Clear();
      }

      _tbl = DbCmd.GetDatabaseOptions(_cp, _cp.Database);
      bs.DataSource = _tbl;
      _tbl.ColumnChanged += new DataColumnChangeEventHandler(_tbl_ColumnChanged);
    }

    private void UpdateOptions( )
    {
      string option = String.Empty;
      bool isset = false;
      bool oldisset = false;

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        foreach (DataRow row in _tbl.Rows)
        {
          if (!Utils.IsRowItemValid(row, 0) || !Utils.IsRowItemValid(row, 1) || !Utils.IsRowItemValid(row, 2))
            continue;

          option = (string)row[0];
          isset = (bool)row[1];
          oldisset = (bool)row[2];

          if (isset == oldisset)
            continue;

          if (isset)
          {
            DbCmd.AddDatabaseOption(conn, _cp.Database, option);
          }
          else
          {
            DbCmd.DropDatabaseOption(conn, _cp.Database, option);
          }
        }
      }

      SetModified(false);
    }

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      UpdateOptions();
    }
  }
}
