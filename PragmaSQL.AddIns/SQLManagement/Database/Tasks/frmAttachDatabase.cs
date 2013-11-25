using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using ICSharpCode.Core;
using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace SQLManagement
{
  public partial class frmAttachDatabase : KryptonForm
  {
    private enum ValidationType
    {
      None,
      Data,
      Log,
      Both
    }
    
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

    public static DialogResult ShowAttachDatabaseDialog(  )
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

      if (String.IsNullOrEmpty(srv.SelNode.ConnParams.Database))
      {
        MessageService.ShowError("Selected node is not a database or child of a database!");
        return DialogResult.Abort;
      }

      frmAttachDatabase frm = new frmAttachDatabase();
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.Text = "Attach Database {" + frm.ConnectionParams.Server + "}";
      return frm.ShowDialog();
    }
    
    public frmAttachDatabase( )
    {
      InitializeComponent();
      fsData.Filter = "Data Files|*.mdf|All Files|*.*";
      fsLog.Filter = "Log Files|*.ldf|All Files|*.*";
    }

    private bool ValidateFiles( ValidationType valType, ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Values listed below are invalid!\n";

      switch (valType)
      {
        case ValidationType.None:
          result = true;
          break;
        case ValidationType.Data:
          if (!File.Exists(fsData.Path))
          {
            errorMsg += " - Data File does not exist";
            result = false;
          }
          break;
        case ValidationType.Log:
          if (!File.Exists(fsLog.Path))
          {
            errorMsg += " - Log File does not exist";
            result = false;
          }
          break;
        case ValidationType.Both:
          if (!File.Exists(fsData.Path))
          {
            errorMsg += " - Data File does not exist";
            result = false;
          }
          if (!File.Exists(fsLog.Path))
          {
            errorMsg += (!result ? "\n" : String.Empty) + " - Log File does not exist";
            result = false;
          }  
          break;
        default:
          result = true;
          break;
      }

      if (String.IsNullOrEmpty(txtName.TextBoxText))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Database name";
        result = false;
      }

      return result;
    }


    private void btnAttach_Click( object sender, EventArgs e )
    {
      string err = String.Empty;
      if (!String.IsNullOrEmpty(fsLog.Path))
      {
        if (!ValidateFiles(ValidationType.Both, ref err))
        {
          MessageService.ShowError(err);
          return;
        }
        using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
        {
          DbCmd.Attach(conn, txtName.TextBoxText, fsData.Path, fsLog.Path);
          DialogResult = DialogResult.OK;
        }
      }
      else
      {
        if (!ValidateFiles(ValidationType.Data, ref err))
        {
          MessageService.ShowError(err);
          return;
        }
        using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
        {
          DbCmd.AttachSingle(conn, txtName.TextBoxText, fsData.Path);
          DialogResult = DialogResult.OK;
        }
      }
    }

    private void fsData_AfterFileOpened( object sender, EventArgs e )
    {
      if (String.IsNullOrEmpty(txtName.TextBoxText))
      {
        FileInfo fi = new FileInfo(fsData.Path);
        txtName.TextBoxText = fi.Name.Replace(fi.Extension, String.Empty);
      }

    }
  }

}