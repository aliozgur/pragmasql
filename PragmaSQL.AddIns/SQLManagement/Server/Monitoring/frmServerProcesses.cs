using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;
using ICSharpCode.Core;

namespace SQLManagement
{
  public partial class frmServerProcesses: DockContent
  {
    public frmServerProcesses()
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

    public void LoadServerProcesses()
    {
      try
      {
        lblProgress.Text = "Refreshing processes...";
        Application.DoEvents();
        tblInfo.Clear();
				using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
				{
					SqlDataAdapter adapter = new SqlDataAdapter("SELECT spid, rtrim(status) as Status, SUSER_SNAME(sid) as UserName, rtrim(hostname) as hostname"
						+ ", rtrim(program_name) as program_name, memusage, cpu, blocked"
						+ ", CASE WHEN dbid=0 THEN '' ELSE DB_NAME(dbid) END as DB"
						+ ", cmd, rtrim(nt_domain) as nt_domain, rtrim(nt_username) as nt_username"
						+ ", net_address, net_library FROM master.dbo.sysprocesses (nolock) ORDER BY spid"
						, conn);
					adapter.Fill(tblInfo);
				}
				bs.DataSource = tblInfo;
        grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
      }
      finally
      {
        lblProgress.Text = String.Empty;
      }
    }

    private void KillSelectedProcess()
    {
      if (grd.SelectedRows.Count == 0)
        return;
      
      if (!MessageService.AskQuestion("Kill selected processes?"))
        return;

      bool hasErrors = false;
      using (SqlConnection conn = _cp.CreateSqlConnection(true,false))
      {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        foreach (DataGridViewRow row in grd.SelectedRows)
        {
          if (row.Cells[0].Value == null)
          {
            continue;
          }
          cmd.CommandText = "KILL " + row.Cells[0].Value.ToString();
          try
          {
            cmd.ExecuteNonQuery();
          }
          catch (Exception ex)
          {
            hasErrors = true;
            HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex.Message);
          }
        }
      }

      if (hasErrors)
      {
        HostServicesSingleton.HostServices.MsgService.ShowMessages();
      }
      LoadServerProcesses();
    }

    public static void ShowServerProcesses()
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

      frmServerProcesses frm = new frmServerProcesses();
      frm.Caption = "Server Processes {" + srv.SelNode.ServerName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams;
      frm.LoadServerProcesses();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
			frm.timer1.Enabled = true;
    }

    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LoadServerProcesses();
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
      LoadServerProcesses();
    }

    private void grd_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
      object cellVal = this.grd.Rows[e.RowIndex].Cells[1].Value;
      string statString = cellVal != null ? cellVal.ToString().ToLowerInvariant() : String.Empty;

      if (statString == "sleeping")
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      }
      else if (statString == "background")
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
      }
      else if (statString == "runnable")
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(128, 255, 128);
      }
      else if (statString == "running")
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(102, 204, 51);
      }
      else if (statString == "blocking")
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 51, 0);
      }
      else if (statString == "suspended")
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255,192,192);
      }
      else
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);     
      }

    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      LoadServerProcesses();
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      KillSelectedProcess();
    }

    private void killToolStripMenuItem_Click(object sender, EventArgs e)
    {
      KillSelectedProcess();
    }

    private void killToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      KillSelectedProcess();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      LoadServerProcesses();
    }

    private void toolStripTextBox1_Validating(object sender, CancelEventArgs e)
    {
      int interval = 30;
      if (!Int32.TryParse(toolStripTextBox1.Text, out interval))
      {
        e.Cancel = true;
        toolStripTextBox1.Text = "30";
      }
      else
      {
        if (interval <= 0)
        {
          toolStripTextBox1.Text = "30";
          return;
        }
        timer1.Interval = interval * 1000;
      }
    }

		private void frmServerProcesses_FormClosed(object sender, FormClosedEventArgs e)
		{
			timer1.Enabled = false;
		}
  }
}