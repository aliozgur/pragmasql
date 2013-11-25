using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmDbProperties : DockContent
  {
    #region Fields And properties
    private DbFiles _filesEditor = null;
    private DbOptions _optionsEditor = null;

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = null;
          _cp = value.CreateCopy();
          InitializeUserControls();
        }
        else
        {
          _cp = null;
        }
      }
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

    private long _dbID = -1;
    public long DbID
    {
      get { return _dbID; }
      private set { _dbID = value; }
    }

    #endregion //Fields And properties

    #region Static show
    public static void ShowDatabaseProperties( )
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

      frmDbProperties frm = new frmDbProperties();
      frm.Caption = String.Format("Database Properties ({0} on {1})", srv.SelNode.ServerName, srv.SelNode.DatabaseName);
      ConnectionParams cp = srv.SelNode.ConnParams.CreateCopy();
      cp.Database = srv.SelNode.DatabaseName;
      frm.DbID = srv.SelNode.DbId;
      frm.ConnectionParams = cp;

      cp = null;

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }
    #endregion //Static show

    #region CTOR
    public frmDbProperties( )
    {
      InitializeComponent();
      CreateUserControls();
    }


    #endregion //CTOR

    #region Private Initialization Function
    private void CreateUserControls( )
    {
      _filesEditor = new DbFiles();
      panFiles.Controls.Add(_filesEditor);
      _filesEditor.Parent = panFiles;
      _filesEditor.Dock = DockStyle.Fill;

      _optionsEditor = new DbOptions();
      splitContainer1.Panel2.Controls.Add(_optionsEditor);
      _optionsEditor.Parent = splitContainer1.Panel2;
      _optionsEditor.Dock = DockStyle.Fill;

    }

    private void InitializeUserControls( )
    {
      if (_filesEditor != null)
      {
        _filesEditor.ConnectionParams = _cp;
      }

      if (_optionsEditor != null)
      {
        _optionsEditor.ConnectionParams = _cp;
      }
    }

    private void RefreshDatabaseProperties( )
    {
      InitializeUserControls();
    }

    #endregion //Private Initialization Function

    #region Database Tasks
    private bool DropDatabase( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to drop the selected database?\nDatabase name: " + _cp.Database))
        return false;

      ConnectionParams tmp = _cp.CreateCopy();
      tmp.Database = "master";
      DbCmd.DropDb(tmp, _cp.Database, _dbID);
      return true;
    }

    private bool DetachDatabase( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to detach the selected database?\nDatabase name: " + _cp.Database))
        return false;

      ConnectionParams tmp = _cp.CreateCopy();
      tmp.Database = "master";
      DbCmd.DetachDb(tmp, _cp.Database);
      return true;
    }

    private bool TruncateLogs( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to truncate logs for the selected database?\nDatabase name: " + _cp.Database))
        return false;

      ConnectionParams tmp = _cp.CreateCopy();
      tmp.Database = "master";
      DbCmd.TruncLog(tmp, _cp.Database);
      return true;
    }

    private bool ShrinkDatabase( )
    {
      return frmShrinkDb.ShowShrinkDbDialog(_cp) == DialogResult.OK;
    }

    #endregion //Database Tasks

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }

    private void toolStripButton5_Click( object sender, EventArgs e )
    {
      RefreshDatabaseProperties();
    }

    private void truncateLogsToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (TruncateLogs())
        RefreshDatabaseProperties();
    }

    private void shrinkDBToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (ShrinkDatabase())
        RefreshDatabaseProperties();

    }

    private void dropDBToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (DropDatabase())
        Close();

    }

    private void detachDBToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (DetachDatabase())
        Close();
    }
  }
}