using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;
using ICSharpCode.Core;

namespace SQLManagement
{
  public partial class frmServerInfo : DockContent
  {
    public frmServerInfo()
    {
      InitializeComponent();
    }

    private DataTable tblInfo = new DataTable();

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

    public void LoadServerInfo()
    {
      tblInfo.Clear();
      LoadServerSummary();
      LoadServerVersion();
    }

    private void LoadServerVersion()
    {
			using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
			{
				SqlDataAdapter adapter = new SqlDataAdapter("EXEC master.dbo.xp_msver", conn);
				adapter.Fill(tblInfo);
			}
			bs.DataSource = tblInfo;
    }

    private void LoadServerSummary( )
    {
      using (SqlConnection conn = _cp.CreateSqlConnection(true,false))
      {
        string cmdText = "SELECT ServerStartTime = (SELECT crdate FROM master..sysdatabases WITH (NOLOCK) WHERE name = 'tempdb'), NumberDatabases = (SELECT COUNT(*) FROM master..sysdatabases WITH (NOLOCK))";
        SqlCommand cmd = new SqlCommand(cmdText,conn);
        SqlDataReader reader = cmd.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            lblServerStartTime.Text = reader.GetDateTime(0).ToString();
            lblNumberDatabases.Text = reader.GetInt32(1).ToString();
          }
        }
        finally
        {
          reader.Close();
        }


        cmdText = "SELECT ConnectedProcesses = (SELECT COUNT(*)  FROM master..sysprocesses WITH (nolock)),"
                                              + "BlockedProcesses = (SELECT COUNT(*)  FROM master..sysprocesses WITH (nolock) WHERE blocked <> 0), "
                                              + "OpenTransactions = (SELECT COUNT(*)  FROM master..sysprocesses WITH (nolock) WHERE open_tran <> 0), "
                                              + "TotalLocks = (SELECT COUNT(l.req_spid) FROM master.dbo.syslockinfo l, master.dbo.sysprocesses p, master.dbo.spt_values  v WITH (nolock) WHERE l.req_spid = p.spid AND l.rsc_type = v.number AND v.type = 'L') ";
        cmd.CommandText = cmdText;
        reader = cmd.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            lblConnectedProcesses.Text = reader.GetInt32(0).ToString();
            lblBlockedProcesses.Text = reader.GetInt32(1).ToString();
            lblOpenTransactions.Text = reader.GetInt32(2).ToString();
            lblTotalLocks.Text = reader.GetInt32(3).ToString();
          }
        }
        finally
        {
          reader.Close();
        }

        cmdText = "SELECT @@SERVICENAME as ServiceName, ISNULL(@@SERVERNAME,'Host') as HostName";
        cmd.CommandText = cmdText;
        reader = cmd.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            lblServiceName.Text = reader.GetString(0).ToString();
            lblHostName.Text = reader.GetString(1).ToString();
          }
        }
        finally
        {
          reader.Close();
        }

        

      }
    }

    public static void ShowServerInfo()
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

      frmServerInfo frm = new frmServerInfo();
      frm.Caption = "Server Info {" + srv.SelNode.ServerName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams;
      frm.LoadServerInfo();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LoadServerInfo();
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      LoadServerInfo();
    }

		private void frmServerInfo_FormClosed(object sender, FormClosedEventArgs e)
		{

		}
  }
}