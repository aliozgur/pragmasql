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
  public partial class frmIndexDBCC: DockContent
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

    private string _objectName;
    public string ObjectName
    {
      get { return _objectName; }
      set { _objectName = value; }
    }

    private string _indexName;
    public string IndexName
    {
      get { return _indexName; }
      set { _indexName = value; }
    }

    private int _objectId = -1;
    public int ObjectId
    {
      get { return _objectId; }
      set { _objectId = value; }
    }

    private int _indexId = -1;
    public int IndexId
    {
      get { return _indexId; }
      set { _indexId  = value; }
    }

    private string _objectOwner = String.Empty;
    public string ObjectOwner
    {
      get { return _objectOwner; }
      set { _objectOwner = value; }
    }


    #endregion //Fields and properties
      
    #region Static show
    public static void ShowIndexDBCC(ConnectionParams cp, string owner, int objectId , string objectName, int indexId, string indexName)
    {
      
      frmIndexDBCC frm = new frmIndexDBCC();
      
      frm.Caption = "Index DBCC: " +  objectName + "." + indexName + " {" + cp.InfoDbServer + "}";
      frm.ConnectionParams = cp.CreateCopy();

      frm.IndexId = indexId;
      frm.IndexName = indexName;
      frm.ObjectOwner = owner;
      frm.ObjectId = objectId;
      frm.ObjectName = objectName;

      frm.lblIndexName.Text = indexName;
      frm.lblObjectName.Text = objectName;

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmIndexDBCC( )
    {
      InitializeComponent();
      cmbOperation.SelectedIndex = 0;
    }

    #endregion //CTOR

    private string GetQuery( )
    {
      string cmdText = "";
      switch (cmbOperation.SelectedIndex)
      {
        case 0:
          cmdText = "DBCC SHOWCONTIG(" + ObjectId + ", " + IndexId.ToString() + ") WITH TABLERESULTS";
          break;
        case 1:
          cmdText = "DBCC CHECKTABLE('" + Utils.ReplaceQuatations(ObjectOwner + "." + ObjectName) + "', " + IndexId.ToString() + ") WITH TABLERESULTS";
          break;
        case 2:
          if (rbUseOriginal.Checked)
          {
            cmdText = "DBCC DBREINDEX('" + Utils.ReplaceQuatations(ObjectOwner + "." + ObjectName) + "', '" + Utils.ReplaceQuatations(IndexName) + "')";
          }
          else if (rbReset.Checked)
          {
            cmdText = "DBCC DBREINDEX('" + Utils.ReplaceQuatations(ObjectOwner + "." + ObjectName) + "', '" + Utils.ReplaceQuatations(IndexName) + "', " + maskedTextBox1.Text + ")";
          }
          break;
        case 3:
          cmdText = "DBCC SHOW_STATISTICS('" + Utils.ReplaceQuatations(ObjectOwner + "." + ObjectName) + "', " + Utils.Qualify(IndexName) + ")";
          break;
        case 4:

          string _command = "DBCC UPDATEUSAGE(0, '" + Utils.ReplaceQuatations(ObjectOwner + "." + ObjectName) + "', " + IndexId.ToString() + ")";
          if (chkWithCountRows.Checked)
          {
            _command += " WITH COUNT_ROWS";
          }
          cmdText = _command;
          
          break;
      }
      return cmdText;
    }
    
    private void ExecuteCommand( string cmdText)
    {
      DbCmd.ExecuteCommand(cmdText, _cp);
    }

    private void ExecuteCommand( )
    {
      queryViewer.ClearAll();
      string cmdText = GetQuery();
      if (String.IsNullOrEmpty(cmdText))
      {
        return;
      }
      try
      {
        lblProgress.Visible = true;
        Application.DoEvents();
        if (cmbOperation.SelectedIndex == 2 || cmbOperation.SelectedIndex == 4)
        {
          ExecuteCommand(cmdText);
        }
        else
        {
          queryViewer.Execute(_cp, cmdText);
        }
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
      finally
      {
        lblProgress.Visible = false;
      }
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

    private void button1_Click( object sender, EventArgs e )
    {
      ExecuteCommand();
    }

    private void cmbOperation_SelectedIndexChanged( object sender, EventArgs e )
    {
      switch (cmbOperation.SelectedIndex)
      {
        case 2:
          gbRebuild.Visible = true;
          gbUpdateUsage.Visible = false;
          break;
        case 4:
          gbUpdateUsage.Left = gbRebuild.Left;
          gbRebuild.Visible = false;
          gbUpdateUsage.Visible = true;
          break;
        default:
          gbRebuild.Visible = false;
          gbUpdateUsage.Visible = false;
          break;
      }
    }

    private void rbUseOriginal_CheckedChanged( object sender, EventArgs e )
    {
      maskedTextBox1.ReadOnly = rbUseOriginal.Checked;
    }

  }
}