/********************************************************************
  Class      : frmException
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.GUI
{
  public partial class frmException : Form
  {
    Exception exception = null;
    public frmException( )
    {
      InitializeComponent();
    }


    #region Static methods

    public static void ShowAppError(Exception ex)
    {
      frmException frm = new frmException();
      frm.exception = ex;
      if(ex != null)
      {
        frm.textBox1.Text = "EXCEPTION TYPE:\r\n" + ex.GetType().ToString();
        frm.textBox1.Text += "\r\n";
        frm.textBox1.Text += "\r\nMESSAGE:\r\n " +  ex.Message;
        frm.textBox1.Text += "\r\n";
        frm.textBox1.Text += "\r\nSTACK TRACE:\r\n " +  ex.StackTrace;
        frm.textBox1.SelectionStart = 0;
        frm.textBox1.SelectionLength  = 0;
      }
      else
      {
        frm.textBox1.Text = "Unknown error.";
      }

      System.Media.SystemSounds.Exclamation.Play();
      frm.ShowDialog();
    }
    #endregion

    private void btnReport_Click( object sender, EventArgs e )
    { 
      string msgBody = "EXCEPTION TYPE:\n" + exception.GetType().ToString()
        + "\n"
        + "MESSAGE:\n" +  exception.Message
        + "\n";
        //+ "STACK TRACE:\n" +  exception.StackTrace
        //+ "\n";
      
      string  cmd = "mailto:pragmasql@gmail.com?subject=PragmaSQL%20Unhandled%20Error";
      System.Diagnostics.Process.Start(cmd);
      Close();
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