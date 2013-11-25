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
  public partial class PwdDialog : KryptonForm
  {
    public static bool ShowPwdDialog(string caption, ref string pwd)
    {
      PwdDialog frm = new PwdDialog();
      frm.Text = !String.IsNullOrEmpty(caption) ? caption : "Set Password";
      if (frm.ShowDialog() == DialogResult.OK)
      {
        pwd = frm.edtPwd.Text;
        return true;
      }
      else
      {
        pwd = String.Empty;
        return false;
      }
    }

    public PwdDialog()
    {
      InitializeComponent();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (edtPwd.Text != edtPwd2.Text)
      {
        MessageBox.Show("Password and Confirm Password field values do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        DialogResult = DialogResult.None;
        return;
      }

      if (String.IsNullOrEmpty(edtPwd.Text))
      {
        DialogResult dlgres = MessageBox.Show("Password is empty!\r\n"
                                + "Empty passwords may cause serious security vulnerabilities.\r\n"
                                + "Are you sure you want to set empty password?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
        if (dlgres == DialogResult.No)
        {
          DialogResult = DialogResult.None;
          return;
        }
      }
      DialogResult = DialogResult.OK;
    }


  }
}