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
  public partial class frmRules: DockContent
  {
    #region Fields and properties
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

    private RulesList _ruleList = null;
    public RulesList RoleList
    {
      get { return _ruleList; }
    }

    #endregion //Fields and properties
      
    #region Static show
    public static void ShowRules( )
    {
      IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
      if (srv == null)
      {
        MessageService.ShowError("No object explorer available!");
        return;
      }

      if (srv.SelNode == null || srv.SelNode.ConnParams == null)
      {
        MessageService.ShowError("Database data is not available!");
        return;
      }

      if (String.IsNullOrEmpty(srv.SelNode.ConnParams.Database))
      {
        MessageService.ShowError("Selected node is not a database or child of a database!");
        return;
      }

      frmRules frm = new frmRules();
      frm.Caption = "Rules : " + srv.SelNode.ServerName + " { " + srv.SelNode.DatabaseName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams.CreateCopy();
      frm.ConnectionParams.Database = srv.SelNode.DatabaseName;
      frm.LoadRules();
      //frm.ShowDialog();
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmRules( )
    {
      InitializeComponent();
      InitializeRuleList();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeRuleList( )
    {
      // Login List
      _ruleList = new RulesList();
      panRules.Controls.Add(_ruleList);
      _ruleList.Parent = panRules;
      _ruleList.Dock = DockStyle.Fill;
      _ruleList.ContextMenuStrip = contextMenuStrip1;
      panRules.BringToFront();
    }

    public void LoadRules( )
    {
      _ruleList.LoadRules(_cp);
    }

    #endregion //Initialization

    #region Internal Operations
    private void RefreshRulesList( )
    {
      _ruleList.RefreshRules();
    }

    private void DropSelectedRules( )
    {
      _ruleList.DropSelectedRules(true);
    }

    private void ModifySelectedRules( )
    {
      _ruleList.ModifySelectedRules();
    }

    private void CreateNewRule( )
    {
      frmRuleEdit.ShowCreateRule(_cp);
    }

    #endregion //Internal Operations

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
      RefreshRulesList();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      DropSelectedRules();
    }

    private void drepToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DropSelectedRules();
    }

    private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RefreshRulesList();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      ModifySelectedRules();
    }

    private void modifyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      ModifySelectedRules();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      CreateNewRule();
    }

    private void newToolStripMenuItem_Click( object sender, EventArgs e )
    {
      CreateNewRule();
    }

  }
}