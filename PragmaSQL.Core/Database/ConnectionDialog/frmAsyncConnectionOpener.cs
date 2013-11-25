using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
	public partial class frmAsyncConnectionOpener : KryptonForm
	{
		private ConnectionParams _cp = null;
		private SqlConnection _conn = null;

		private AsyncConnectionResult _result = AsyncConnectionResult.None;
		private bool _connecting = false;
		private bool _cancelled = false;
		private string _lastError = String.Empty;

		public frmAsyncConnectionOpener()
		{
			InitializeComponent();
			HostServicesSingleton.HostServices.SetMainFormAsOwner(this);
		}

		public static AsyncConnectionResult TryToOpenConnection(ConnectionParams cp, ref string error)
		{
			if (cp == null)
				throw new Exception("Connection parameters instance is null!");

			frmAsyncConnectionOpener frm = new frmAsyncConnectionOpener();
			frm._cp = cp.CreateCopy();
			frm._conn = null;
			frm.BringToFront();
			frm.ShowDialog();
			error = frm._lastError;
			return frm._result;
		}

		public static AsyncConnectionResult TryToOpenConnection(SqlConnection conn, ref string error)
		{
			if (conn == null)
				throw new Exception("Connection instance is null!");

			frmAsyncConnectionOpener frm = new frmAsyncConnectionOpener();
			frm._cp = null;
			frm._conn = conn;
			frm.BringToFront();
			frm.ShowDialog();
			error = frm._lastError;
			return frm._result;
		}

		private void frmAsyncConnectionOpener_Shown(object sender, EventArgs e)
		{
			if (_connecting)
				return;

			btnCancel.Enabled = true;
			UpdateInfo();
			bw.RunWorkerAsync();
		}

		private void UpdateInfo()
		{
			if (_cp != null)
			{
				lblInfo.Text = "Connecting to \"" + _cp.Database + " on " + _cp.Server + "\"";
			}
			else if (_conn != null)
			{
				lblInfo.Text = "Connecting to \"" + _conn.Database + " on " + _conn.DataSource + "\"";
			}
			else
			{
				lblInfo.Text = "No connection specified!";
			}

			this.Width = lblInfo.Width + 50;
			Application.DoEvents();
		}

		private void ConnectToDatabase()
		{
			if (_cp != null)
			{
				TryToConnect();
			}
			else
			{
				OpenConnection();
			}
		}

		private void TryToConnect()
		{
			_cp.TimeOut = "15";
			using (SqlConnection conn = _cp.CreateSqlConnection(false, false))
			{
				try
				{
					conn.Open();
				}
				catch (Exception ex)
				{
					if (!_cancelled)
						throw ex;
				}
			}
		}

		private void OpenConnection()
		{
			try
			{
				_conn.Open();
			}
			catch (Exception ex)
			{
				if (_cancelled)
				{
					_conn.Dispose();
					_conn = null;
				}
				else
					throw ex;
			}
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			_connecting = true;
			ConnectToDatabase();
		}

		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Cancelled)
				{
					_result = AsyncConnectionResult.Cancel;
					DialogResult = DialogResult.Cancel;
					return;
				}

				if (e.Error != null)
				{
					_lastError = e.Error.Message;
					_result = AsyncConnectionResult.Error;
					DialogResult = DialogResult.Abort;
					return;
				}

				_result = AsyncConnectionResult.Success;
				DialogResult = DialogResult.OK;
			}
			finally
			{
				_connecting = false;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (_cancelled)
				return;

			bw.CancelAsync();
			_cancelled = true;

			_result = AsyncConnectionResult.Cancel;
			DialogResult = DialogResult.Cancel;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (pd.Value == pd.SliceCount)
				pd.Value = 1;
			else
				pd.Value++;
		}
	}

	public enum AsyncConnectionResult
	{
		None,
		Success,
		Cancel,
		Error
	}
}