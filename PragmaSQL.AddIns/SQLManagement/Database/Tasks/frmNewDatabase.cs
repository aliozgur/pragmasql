using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.Core;
using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace SQLManagement
{
  public partial class frmNewDatabase : KryptonForm
  {
    #region Fields And Properties

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
          PopulateDatabaseOptions();
        }
        else
        {
          _cp = null;
          bsOptions.DataSource = null;
          _tblOptions = null;
        }
      }
    }

    private string _caption = String.Empty;
    public string Caption
    {
      get { return _caption; }
      set
      {
        _caption = value;
        Text = value;
      }
    }
    private DataTable _tblOptions = null;
    
    #endregion //Fields And Properties

    #region Static Show
    public static DialogResult ShowNewDatabaseDialog( )
    {
      IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
      if (srv == null)
      {
        MessageService.ShowError("No object explorer available!");
        return DialogResult.Abort;
      }

      if (srv.SelNode == null || srv.SelNode.ConnParams == null)
      {
        MessageService.ShowError("Database data is not available!");
        return DialogResult.Abort;
      }


      frmNewDatabase frm = new frmNewDatabase();

      frm.Caption = "New Database {" + srv.SelNode.ConnParams.Server + "}";
      ConnectionParams cp = srv.SelNode.ConnParams.CreateCopy();
      cp.Database = "master";
      frm.ConnectionParams = cp;
      cp = null;

      return frm.ShowDialog();
    }

    #endregion //Static Show
    
    public frmNewDatabase( )
    {
      InitializeComponent();
      cmbDataFileGrowthType.SelectedIndex = 0;
      cmbLogFileGrowthType.SelectedIndex = 0;
    }

    private void PopulateDatabaseOptions( )
    {
      bsOptions.DataSource = null;

      _tblOptions = DbCmd.PrepareDatabaseOptionsTable(_cp);
      bsOptions.DataSource = _tblOptions;

    }

    private bool ValidateDataFileProps( ref string errorMsg )
    {
      bool result = true;
      double value = 0;
      errorMsg = "Data file values listed below are not valid.\n";
      if (String.IsNullOrEmpty(txtDataFileName.TextBoxText))
      {
        errorMsg += " - File Name";
        result = false;
      }

      if (String.IsNullOrEmpty(txtDataFilePath.Path))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - File path is empty";
        result = false;
      }
      else if (!Directory.Exists(txtDataFilePath.Path))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - File path is not valid";
        result = false;
      }

      if (String.IsNullOrEmpty(txtDataFileSize.TextBoxText) || !Double.TryParse(txtDataFileSize.TextBoxText, out value))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Size ";
        result = false;
      }


      return result;
    }

    private bool ValidateLogFileProps( ref string errorMsg )
    {
      bool result = true;
      double value = 0;
      errorMsg = "Log file values listed below are not valid.\n";
      if (String.IsNullOrEmpty(txtLogFileName.TextBoxText))
      {
        errorMsg += " - File Name";
        result = false;
      }

      if (String.IsNullOrEmpty(txtLogFilePath.Path))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - File path is empty";
        result = false;
      }
      else if (!Directory.Exists(txtLogFilePath.Path))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - File path is not valid";
        result = false;
      }

      if (String.IsNullOrEmpty(txtLogFileSize.TextBoxText) || !Double.TryParse(txtLogFileSize.TextBoxText, out value))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Size ";
        result = false;
      }


      return result;
    }

    private bool ValidateDataFileGrowthData( ref string errorMsg )
    {
      bool result = true;
      double value = 0;
      errorMsg = "Data file growth values listed below are not valid.\n";
      if (String.IsNullOrEmpty(txtDataFileGSize.TextBoxText) || !Double.TryParse(txtDataFileGSize.TextBoxText, out value))
      {
        errorMsg += " - Size ";
        result = false;
      }

      if (String.IsNullOrEmpty(txtDataFileGrowthRate.TextBoxText) || !Double.TryParse(txtDataFileGrowthRate.TextBoxText, out value))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Growth rate";
        result = false;
      }

      if (rbDataFileGSize.Checked && (String.IsNullOrEmpty(txtDataFileGMaxSize.Text) || !Double.TryParse(txtDataFileGMaxSize.Text, out value)))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Max size ";
        result = false;
      }
      return result;
    }

    private bool ValidateLogFileGrowthData( ref string errorMsg )
    {
      bool result = true;
      double value = 0;
      errorMsg = "Log file growth values listed below are not valid.\n";
      if (String.IsNullOrEmpty(txtLogFileSize.TextBoxText) || !Double.TryParse(txtLogFileSize.TextBoxText, out value))
      {
        errorMsg += " - Size ";
        result = false;
      }

      if (String.IsNullOrEmpty(txtLogFileGrowthRate.TextBoxText) || !Double.TryParse(txtLogFileGrowthRate.TextBoxText, out value))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Growth rate";
        result = false;
      }

      if (rbLogFileGSize.Checked && (String.IsNullOrEmpty(txtLogFileGMaxSize.Text) || !Double.TryParse(txtLogFileGMaxSize.Text, out value)))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Max size ";
        result = false;
      }
      return result;
    }

    private bool ValidateAllData( ref string errorMsg )
    {
      string tmp = String.Empty;
      bool result = true;
      bool valid = false;
      errorMsg = String.Empty;
      if (String.IsNullOrEmpty(txtName.TextBoxText))
      {
        tmp = "Database name is empty.";
        errorMsg += tmp;
        result = false;
      }

      tmp = String.Empty;
      valid = ValidateDataFileProps(ref tmp);
      result = result & valid;
      if (!valid)
        errorMsg += tmp;

      tmp = String.Empty;
      valid = ValidateDataFileGrowthData(ref tmp);
      result = result & valid;
      if (!valid)
        errorMsg += (!result ? "\n" : String.Empty) + tmp;

      tmp = String.Empty;
      valid = ValidateLogFileProps(ref tmp);
      result = result & valid;
      if (!valid)
        errorMsg += (!result ? "\n" : String.Empty) + tmp;

      tmp = String.Empty;
      valid = ValidateLogFileGrowthData(ref tmp);
      result = result & valid;
      if (!valid)
        errorMsg += (!result ? "\n" : String.Empty) + tmp;

      return result;
    }

    private bool CreateDatabase( )
    {
      string err = String.Empty;
      if (!ValidateAllData(ref err))
      {
        MessageService.ShowError(err);
        return false;
      }

      string _strName = txtName.TextBoxText;
      string _strDataName = txtDataFileName.TextBoxText;
      string _strDataPath = txtDataFilePath.Path + _strDataName + ".mdf";
      string _strDataSize = txtDataFileSize.TextBoxText + "MB";

      string _strLogName = txtLogFileName.TextBoxText;
      string _strLogPath = txtLogFilePath.Path + _strLogName + ".ldf";
      string _strLogSize = txtLogFileSize.TextBoxText + "MB";

      string _strLogGrowth = txtLogFileGrowthRate.TextBoxText + cmbLogFileGrowthType.Text;
      string _strDataGrowth = txtLogFileGrowthRate.TextBoxText + cmbDataFileGrowthType.Text;
      string _strDataMaxSize;
      string _strLogMaxSize;

      //check Database Max size
      if (rbDataFileGUnlimited.Checked)
      {
        _strDataMaxSize = "UNLIMITED";
      }
      else
      {
        _strDataMaxSize = txtDataFileGMaxSize.Text + "MB";
      }

      //check Log Max size
      if (rbLogFileGUnlimited.Checked)
      {
        _strLogMaxSize = "UNLIMITED";
      }
      else
      {
        _strLogMaxSize = txtLogFileGMaxSize + "MB";
      }


      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {

        DbCmd.CreateDatabase(conn, _strName, _strDataName, _strDataPath, _strDataSize, _strDataMaxSize, _strDataGrowth, _strLogName, _strLogPath, _strLogSize, _strLogMaxSize, _strLogGrowth, chkAttach.Checked);
        try
        {
          ApplyDatabaseOptions(conn, txtName.TextBoxText);
        }
        catch (Exception ex)
        {
          MessageService.ShowError("Database was created but some options could not be applied!\nError Message:" + ex.Message);
          return true;
        }
      }

      return true;
    }

    private void ApplyDatabaseOptions( SqlConnection conn, string dbName )
    {
      string option = String.Empty;
      bool isset = false;

      foreach (DataRow row in _tblOptions.Rows)
      {
        if (!Utils.IsRowItemValid(row, 0) || !Utils.IsRowItemValid(row, 1))
          continue;

        option = (string)row[0];
        isset = (bool)row[1];


        if (isset)
        {
          DbCmd.AddDatabaseOption(conn, dbName, option);
        }
      }
    }

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }

    private void btnCreate_Click( object sender, EventArgs e )
    {
      if (CreateDatabase())
        DialogResult = DialogResult.OK;
    }
  }
}