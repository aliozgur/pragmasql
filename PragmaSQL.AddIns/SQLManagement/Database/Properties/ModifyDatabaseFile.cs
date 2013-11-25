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
  public partial class ModifyDatabaseFile : UserControl
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
        }
        else
        {
          _cp = null;
        }
      }
    }


    private double _size = 0;

    #endregion //Fields And properties
    
    public ModifyDatabaseFile( )
    {
      InitializeComponent();
      cmbGrowthType.SelectedIndex = 0;
    }

    private bool ValidateProperties( ref string errorMsg )
    {
      bool result = true;
      double value = 0;
      errorMsg = "Values listed below are not valid!\n";
      if (String.IsNullOrEmpty(txtSize.TextBoxText) || !Double.TryParse(txtSize.TextBoxText, out value))
      {
        errorMsg += " - Size ";
        result = false;
      }

      if (String.IsNullOrEmpty(txtGrowthRate.TextBoxText) || !Double.TryParse(txtGrowthRate.TextBoxText, out value))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Growth rate";
        result = false;
      }

      if (rbSize.Checked && ( String.IsNullOrEmpty(txtMaxSize.Text) || !Double.TryParse(txtMaxSize.Text, out value)))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Max size " ;
        result = false;
      }
      return result;
    }

    public void LoadInitial(string fileName,double size, double growth, double growthPerc, double maxSize )
    {

      _size = size * 1024;
      lblFileName.Text = fileName;
      txtSize.TextBoxText = (size * 1024).ToString();
      if (growth != 0)
      {
        //growth allowed
        chkAllowGrowth.Checked = true;
        
        if (growthPerc != 0)
        {
          //percentage
          cmbGrowthType.SelectedIndex = 0;
          txtGrowthRate.TextBoxText = growth.ToString(); 
        }
        else
        {
          //size
          cmbGrowthType.SelectedIndex = 1;
          txtGrowthRate.TextBoxText = (growth * 8).ToString();
        }
        
      }
      else
      {
        chkAllowGrowth.Checked = false;

        txtGrowthRate.Enabled = false;
        cmbGrowthType.Enabled = false;
      }


      //max size
      if (maxSize == -1)
      {
        rbUnlimited.Checked = true;
      }
      else
      {
        rbSize.Checked = true;
        txtMaxSize.Text = (maxSize * 8).ToString();
      }
    }

    public bool UpdateFileProperties( )
    {
      string err = String.Empty;
      if (!ValidateProperties(ref err))
      {
        MessageService.ShowError(err);
        return false;
      }

      string fileName = lblFileName.Text;
      string maxSize = "";
      string size = "";


      if (Convert.ToDouble(txtSize.TextBoxText) > _size)
      {
        size = txtSize.TextBoxText + "KB";
      }

      //check Log Max size
      if (rbUnlimited.Checked)
      {
        maxSize = "UNLIMITED";
      }
      else
      {
        maxSize = txtMaxSize.Text + "KB";
      }
      string growth = txtGrowthRate.TextBoxText + cmbGrowthType.Text;

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        DbCmd.ModifyFile(conn, _cp.Database, fileName, size, maxSize, growth);
      }
      return true;
    }

    private void rbUnlimited_CheckedChanged( object sender, EventArgs e )
    {
      txtMaxSize.Enabled = !rbUnlimited.Checked;
    }
  }
}
