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
  public partial class frmServerLocks : DockContent
  {
    public static void ShowServerLocks()
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

      frmServerLocks frm = new frmServerLocks();
      frm.Caption = "Locks {" + srv.SelNode.ServerName + "}";
      frm.ConnectionParams = srv.SelNode.ConnParams;
      frm.LoadServerLocks();

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
			frm.timer1.Enabled = true;
		}

    public frmServerLocks()
    {
      InitializeComponent();


      InitializeLockTypeCombo();
    }

    private void InitializeLockTypeCombo()
    {
      /*
      DataTable tblLockTypes = new DataTable();
      tblLockTypes.TableName = "LockTypes";
      DataColumn col = null;

      col = new DataColumn();
      col.ColumnName = "value";
      col.DataType = System.Type.GetType("System.Int32");
      tblLockTypes.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "name";
      col.DataType = System.Type.GetType("System.String");
      tblLockTypes.Columns.Add(col);

      Array vals = Enum.GetValues(typeof(LockType));
      DataRow row = null;

      foreach (LockType lType in vals)
      {
        row = tblLockTypes.NewRow();
        row["value"] = (int)lType;
        row["name"] = lType.ToString();
        tblLockTypes.Rows.Add(row);
      }

      colLockType.DataSource = tblLockTypes;
      colLockType.DisplayMember = "name";
      colLockType.ValueMember = "value";
      */
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

    public void LoadServerLocks()
    {
      try
      {
        lblProgress.Text = "Refreshing locks...";
        Application.DoEvents();
        tblInfo.Clear();
        string cmdText = "";
        cmdText += "SELECT master.dbo.sysprocesses.spid, rtrim(master.dbo.sysprocesses.status) as status, master.dbo.syslockinfo.rsc_type, master.dbo.spt_values.name, SUSER_SNAME(master.dbo.sysprocesses.sid) AS UserName, DB_NAME(master.dbo.syslockinfo.rsc_dbid) AS DB, OBJECT_NAME(master.dbo.syslockinfo.rsc_objid) AS Object, RTRIM(master.dbo.sysprocesses.hostname) AS HostName, RTRIM(master.dbo.sysprocesses.program_name) AS Application, master.dbo.sysprocesses.cmd AS Command, master.dbo.sysprocesses.cpu AS CPU, master.dbo.sysprocesses.physical_io AS IO, master.dbo.sysprocesses.memusage AS MemUsage";
        cmdText += " FROM master.dbo.syslockinfo INNER JOIN master.dbo.sysprocesses ON master.dbo.syslockinfo.req_spid = master.dbo.sysprocesses.spid INNER JOIN master.dbo.spt_values ON master.dbo.syslockinfo.rsc_type = master.dbo.spt_values.number";
        cmdText += " WHERE (master.dbo.spt_values.type = 'L')";
				using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
				{
					SqlDataAdapter adapter = new SqlDataAdapter(cmdText, conn);
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

      if (!MessageService.AskQuestion("Kill selected locks?"))
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
      LoadServerLocks();
    }



    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
      LoadServerLocks();
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
      LoadServerLocks();
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
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);
      }
      else
      {
        grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
      }
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      LoadServerLocks();
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
      LoadServerLocks();
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

    private void grd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {

      if (e.RowIndex != -1 && e.ColumnIndex == 2)
      {
        Color bgColor = grd.Rows[e.RowIndex].DefaultCellStyle.BackColor;

        Brush br = SystemBrushes.ControlText;
        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
        {
          bgColor = SystemColors.Highlight;
          br = SystemBrushes.HighlightText; 

        }

        using (
                   Brush gridBrush = new SolidBrush(grd.GridColor),
                   backColorBrush = new SolidBrush(bgColor))
        {


          using (Pen gridLinePen = new Pen(gridBrush))
          {

            // Erase the cell.
            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            // Draw the grid lines (only the right and bottom lines;
            // DataGridView takes care of the others).
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                e.CellBounds.Bottom - 1);
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                e.CellBounds.Top, e.CellBounds.Right - 1,
                e.CellBounds.Bottom);

            // Draw the inset highlight box.
            //e.Graphics.DrawRectangle(Pens.Blue, newRect);


            
            object val = e.Value;
            if (val == null || val.GetType() == typeof(DBNull))
            {
              return;
            }


            LockType lType = (LockType)Int32.Parse(val.ToString());
            e.Graphics.DrawString(lType.ToString(), e.CellStyle.Font,
                  br, e.CellBounds.X + 2,
                  e.CellBounds.Y + 2, StringFormat.GenericDefault);
            e.Handled = true;
          }

        }
      }
    }

    private void grd_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
    }

		private void frmServerLocks_FormClosed(object sender, FormClosedEventArgs e)
		{
			timer1.Enabled = false;
		}
  }
}