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
  public partial class frmUdtEdit : KryptonForm
  {
    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set 
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
        }
        else
        {
          _cp = value;
        }

        if (_udtEdit != null)
        {
          _udtEdit.ConnectionParams = _cp;
        }
      }
    }

    private UdtEdit _udtEdit = null;

    #region Static Show
    public static void ShowUdt(ConnectionParams cp)
    {
      frmUdtEdit.ShowUdtForm(cp, -1);
    }
    
    public static void ShowUdt( ConnectionParams cp, long udtId )
    {
      frmUdtEdit.ShowUdtForm(cp, udtId);
    }

    private static void ShowUdtForm( ConnectionParams cp, long udtId )
    {
      frmUdtEdit frm = new frmUdtEdit();
      frm.ConnectionParams = cp;
      if (udtId > 0)
      {
        frm._udtEdit.LoadUdt(udtId);
      }
      else
      {
        frm._udtEdit.CreateNew();
      }
      frm.ShowDialog();
    }


    #endregion //Static Show
    
    public frmUdtEdit( )
    {
      InitializeComponent();
      InitializeUdtEditControl();
    }

    private void InitializeUdtEditControl( )
    {
      _udtEdit = new UdtEdit();
      this.Controls.Add(_udtEdit);
      _udtEdit.Parent = this;
      _udtEdit.Dock = DockStyle.Fill;
    }

  }
}