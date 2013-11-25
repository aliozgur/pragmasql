using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmModifyDatabaseFile : Form
  {
    private ModifyDatabaseFile _modifyFile = null;

    public static DialogResult ShowModifyDbFileDialog( ConnectionParams cp, string fileName, double size, double growth, double growthPerc, double maxSize )
    {
      frmModifyDatabaseFile frm = new frmModifyDatabaseFile();
      frm._modifyFile.ConnectionParams = cp;
      frm._modifyFile.LoadInitial(fileName, size, growth,growthPerc, maxSize );
      return frm.ShowDialog();
    }

    public frmModifyDatabaseFile( )
    {
      InitializeComponent();
      InitializeModifyFileControl();
    }

    private void InitializeModifyFileControl( )
    {
      _modifyFile = new ModifyDatabaseFile();
      panControl.Controls.Add(_modifyFile);
      _modifyFile.Parent = panControl;
      _modifyFile.Dock = DockStyle.Fill;
    }

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      if (_modifyFile == null)
        return;

      if (_modifyFile.UpdateFileProperties())
        DialogResult = DialogResult.OK;

    }
  }
}