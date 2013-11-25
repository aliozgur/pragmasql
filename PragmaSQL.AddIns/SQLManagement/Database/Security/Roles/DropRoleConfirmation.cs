using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace SQLManagement
{
  public partial class DropRoleConfirmation : KryptonForm
  {
    public static DialogResult ShowConfirmation( ref bool removeUsers )
    {
      DropRoleConfirmation frm = new DropRoleConfirmation();
      DialogResult result = frm.ShowDialog();
      removeUsers = frm.checkBox1.Checked;
      return result;
    }

    public DropRoleConfirmation( )
    {
      InitializeComponent();
    }

    private void btnNo_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void btnYes_Click( object sender, EventArgs e )
    {
      DialogResult = DialogResult.Yes;
    }
  }
}