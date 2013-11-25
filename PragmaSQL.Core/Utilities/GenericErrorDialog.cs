using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
  public partial class GenericErrorDialog : KryptonForm
  {
    public GenericErrorDialog( )
    {
      InitializeComponent();
    }

    public static void ShowError(string caption, string shortError, string longError)
    {
      GenericErrorDialog frm = new GenericErrorDialog();
      frm.lblShortDescription.Text = shortError;
      frm.txtLongDescription.Text = longError;
      frm.Text = caption;
      frm.ShowDialog();
    }

		public static void ShowError(string caption, string shortError, Exception ex)
		{
			GenericErrorDialog frm = new GenericErrorDialog();
			frm.lblShortDescription.Text = shortError;
			frm.txtLongDescription.Text = Utils.FormatException(ex);
			frm.Text = caption;
			frm.ShowDialog();		
		}
  }
}