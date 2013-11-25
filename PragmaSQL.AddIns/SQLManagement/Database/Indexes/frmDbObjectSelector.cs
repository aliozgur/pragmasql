using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace SQLManagement
{
  public partial class frmDbObjectSelector : KryptonForm
  {
    public frmDbObjectSelector( )
    {
      InitializeComponent();
    }

    public static DbObjectSelectorItem ShowSelector(ConnectionParams cp)
    {
      frmDbObjectSelector frm = new frmDbObjectSelector();
      frm.InitializeSelector(cp);
      if (frm.ShowDialog() != DialogResult.OK)
        return null;
      else
        return frm.dbObjectSelector1.SelectedItem;
    }

    public void InitializeSelector( ConnectionParams cp )
    {
      dbObjectSelector1.SetItemTypes(DbObjectSelectorItemTypes.UserTable | DbObjectSelectorItemTypes.View);
      dbObjectSelector1.LoadObjects(cp, false);
    }

    private void btnOk_Click( object sender, EventArgs e )
    {
      if (dbObjectSelector1.SelectedItem != null)
      {
        DialogResult = DialogResult.OK;
      }
    }
  }
}