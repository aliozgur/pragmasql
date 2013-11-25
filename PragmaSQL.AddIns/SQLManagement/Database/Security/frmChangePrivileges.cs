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
  public partial class frmChangePrivileges : KryptonForm
  {
    public frmChangePrivileges()
    {
      InitializeComponent();
    }

    public static bool ShowChangePrivilegesDlg(PrivilegeTypes enabledPrivileges, ref PrivilegeTypes result)
    {

      frmChangePrivileges frm = new frmChangePrivileges();
      frm.ApplyEnabledPrivileges(enabledPrivileges);

      if (frm.ShowDialog() == DialogResult.OK)
      {
        result = frm.GetSelectedPrivileges();
        return true;
      }
      else
      {
        result = PrivilegeTypes.None;
        return false;
      }
    }

    private void ApplyEnabledPrivileges(PrivilegeTypes enabledPrivileges)
    {
      bool isNone = (enabledPrivileges & PrivilegeTypes.None) != PrivilegeTypes.None;

      if (isNone || (enabledPrivileges & PrivilegeTypes.Select) != PrivilegeTypes.Select)
      {
        chkSelect.Enabled = false;
      }

      if (isNone || (enabledPrivileges & PrivilegeTypes.Execute) != PrivilegeTypes.Execute)
      {
        chkExecute.Enabled = false;
      }

      if (isNone || (enabledPrivileges & PrivilegeTypes.Refs) != PrivilegeTypes.Refs)
      {
        chkRefs.Enabled = false;
      }

      if (isNone || (enabledPrivileges & PrivilegeTypes.Insert) != PrivilegeTypes.Insert)
      {
        chkInsert.Enabled = false;
      }

      if (isNone || (enabledPrivileges & PrivilegeTypes.Update) != PrivilegeTypes.Update)
      {
        chkUpdate.Enabled = false;
      }

      if (isNone || (enabledPrivileges & PrivilegeTypes.Delete) != PrivilegeTypes.Delete)
      {
        chkDelete.Enabled = false;
      }
    }

    private PrivilegeTypes GetSelectedPrivileges()
    {
      PrivilegeTypes result = PrivilegeTypes.None;
      if (chkSelect.Checked)
      {
        result = result | PrivilegeTypes.Select;
      }

      if (chkExecute.Checked)
      {
        result = result | PrivilegeTypes.Execute;
      }
      
      if (chkRefs.Checked)
      {
        result = result | PrivilegeTypes.Refs;
      }

      if ( chkInsert.Checked)
      {
        result = result | PrivilegeTypes.Insert;
      }
      
      if (chkUpdate.Checked)
      {
        result = result | PrivilegeTypes.Update;
      }

      if (chkDelete.Checked)
      {
        result = result | PrivilegeTypes.Delete;
      }

      return result;
    }

  }

  [Flags]
  public enum PrivilegeTypes
  {
    None = 0,
    Select = 1,
    Execute= 2,
    Refs = 4,
    Insert = 8,
    Update = 16,
    Delete = 32
  }
}