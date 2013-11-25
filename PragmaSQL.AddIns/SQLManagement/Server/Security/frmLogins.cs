using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmLogins : DockContent
  {
    private LoginList _loginList = null;
    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set { _cp = value; }
    }

    private string _caption = String.Empty;
    public string Caption
    {
      get { return _caption; }
      set
      {
        _caption = value;
        TabText = value;
        Text = value;
      }
    }

    public static void ShowLogins()
    {
      IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
      if (srv == null)
      {
        MessageService.ShowError("No object explorer available!");
        return;
      }

      if (srv.SelNode == null || srv.SelNode.ConnParams == null)
      {
        MessageService.ShowError("Server data is not available!");
        return;
      }

      frmLogins frm = new frmLogins();
      frm.Caption = "Logins {" + srv.SelNode.ServerName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams;
      frm.LoadLogins();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);

    }

    public frmLogins( )
    {
      InitializeComponent();
      InitializeLoginList();
    }

    private void InitializeLoginList( )
    {
      // Login List
      _loginList = new LoginList();
      this.Controls.Add(_loginList);
      _loginList.Parent = this;
      _loginList.Dock = DockStyle.Fill;
      _loginList.BringToFront();
      _loginList.ContextMenuStrip = mnuStripLoginList;
    }

    public void LoadLogins( )
    {
      _loginList.LoadLogins(_cp);
    }


    private void RefreshLoginList( )
    {
      _loginList.RefreshLogins();  
    }

    private void DropSelectedLogins( )
    {
      if (_loginList.DropSelected())
      {
        _loginList.RefreshLogins();
      }
    }

    private void AddNewLogin( )
    {
      if (frmNewLogin.ShowNewLoginDalog(_cp) == DialogResult.Cancel)
        return;

      RefreshLoginList();    
    }

    private void ModifyLogin( )
    {
      if (!_loginList.ModifySingleLogin())
        return;
      RefreshLoginList();          
    }

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      this.Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }

    private void toolStripButton1_Click( object sender, EventArgs e )
    {
      RefreshLoginList();
    }

    private void refreshToolStripMenuItem1_Click( object sender, EventArgs e )
    {
      RefreshLoginList();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelectedLogins();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshLoginList();
    }

    private void dropToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelectedLogins();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      AddNewLogin();
    }

    private void addToolStripMenuItem_Click( object sender, EventArgs e )
    {
      AddNewLogin();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifyLogin();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifyLogin();
    }

    private void toolStripButton5_Click(object sender, EventArgs e)
    {
      _loginList.ChangePwdOfCurrentLogin();
    }

   

  }//Class
}//Namespace