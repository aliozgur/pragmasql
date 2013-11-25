/********************************************************************
  Class      : frmException
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;


namespace PragmaSQL
{
  public partial class frmException : KryptonForm
  {
    Exception exception = null;
    public frmException( )
    {
      InitializeComponent();

    }

    #region Static methods

    private static frmException CreateExceptionForm(Exception ex )
    {
      frmException frm = new frmException();
      frm.exception = ex;
      if (ex != null)
      {
        frm.txtDetailMsg.Text = "EXCEPTION TYPE:\r\n" + ex.GetType().ToString();
        frm.txtDetailMsg.Text += "\r\n";
        frm.txtDetailMsg.Text += "\r\nMESSAGE:\r\n " + ex.Message;
        frm.txtDetailMsg.Text += "\r\n";
        if (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
        {
          frm.txtDetailMsg.Text += "\r\nINNER EXCEPTION MESSAGE:\r\n" + ex.InnerException.Message;
          frm.txtDetailMsg.Text += "\r\n";
        }
        frm.txtDetailMsg.Text += "\r\nSTACK TRACE:\r\n " + ex.StackTrace;
        frm.txtDetailMsg.SelectionStart = 0;
        frm.txtDetailMsg.SelectionLength = 0;

				frm.txtShortMsg.Text = ex.Message;
      }
      else
      {
        frm.txtDetailMsg.Text = "Unknown error.";
				frm.txtShortMsg.Text = frm.txtDetailMsg.Text;
      }

      return frm;
    }

    public static void ShowAppError( string summary, Exception ex )
    {
      frmException frm = CreateExceptionForm(ex);
      frm.txtDetailMsg.Text = "ERROR SUMMARY: " + summary + "\r\n" + "\r\n" + frm.txtDetailMsg.Text;
			if(!String.IsNullOrEmpty(summary))
				frm.txtShortMsg.Text = summary;
			
			System.Media.SystemSounds.Exclamation.Play();
      frm.ShowDialog();    
    }

    public static void ShowAppError(Exception ex)
    {
      frmException frm = CreateExceptionForm(ex);
      System.Media.SystemSounds.Exclamation.Play();
      frm.ShowDialog();
    }
    #endregion

    private void btnReport_Click( object sender, EventArgs e )
    { 
      string msgBody = "EXCEPTION TYPE:%20" + exception.GetType().ToString()
        + "%0d%0d"
        + "MESSAGE:%20" +  exception.Message
        + "%0d%0d"
        + "SOURCE:%20" +  exception.Source
        + "%0d%0d"
        + "TARGET:%0d" 
        + " - TARGETSITE NAME%20:%20" + exception.TargetSite.Name
        + "%0d"
        + " - ASSEMBLYNAME%20:%20" + exception.TargetSite.DeclaringType.Assembly.FullName
        + "%0d"
        + " - TYPE NAME%20:%20" + exception.TargetSite.DeclaringType.Name
        + "%0d"
        + " - NAMESPACE%20:%20" + exception.TargetSite.DeclaringType.Namespace
        + "%0d";
      
      string  cmd = "mailto:ali.ozgur@pragmasql.com?subject=PragmaSQL%20Unhandled%20Error&body=" + msgBody;
      
      try
      {
        System.Diagnostics.Process.Start(cmd);
        Close();
      }
      catch(Exception ex)
      {
        MessageBox.Show("Can not generate error e-mail!\nError was: " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
      }
    }

    private void btnContinue_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void btnClose_Click( object sender, EventArgs e )
    {
      Application.Exit();
    }


  }
}