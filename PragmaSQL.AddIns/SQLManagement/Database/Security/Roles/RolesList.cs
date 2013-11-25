using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace SQLManagement
{
	public partial class RolesList : UserControl
	{
		#region Fields And properties
		private ConnectionParams _cp = null;
		public ConnectionParams ConnectionParams
		{
			get { return _cp; }
		}

		#endregion //Fields And properties

		#region ctor
		public RolesList()
		{
			InitializeComponent();
		}
		#endregion ctro

		#region Private Methods
		private void PopulateRoles()
		{
			bsRoles.DataSource = null;
			DataTable tbl = DbCmd.GetRoles(_cp, _cp.Database);
			bsRoles.DataSource = tbl;
		}
		#endregion //Private methods

		#region Public Methods
		public void LoadRoles(ConnectionParams cp)
		{
			if (cp == null)
			{
				throw new Exception("Connection parameters must be supplied to load data!");
			}
			_cp = cp.CreateCopy();
			PopulateRoles();
		}

		public void RefreshRoles()
		{
			PopulateRoles();
		}

		public void ModifySelectedRoles(DockContent parentContent)
		{

			if (grd.SelectedRows.Count == 0)
				return;

			try
			{
				FuzzyWait.ShowFuzzyWait("Processing roles.Please wait...");
				foreach (DataGridViewRow row in grd.SelectedRows)
				{
					if (row.Cells[2].Value == null || row.Cells[2].GetType() == typeof(DBNull))
						continue;
					frmModifyRole.ModifyRoleDlg(parentContent, _cp, row.Cells[2].Value.ToString());
				}
				FuzzyWait.CloseFuzzyWait();
			}
			catch (Exception ex)
			{
				FuzzyWait.CloseFuzzyWait();
				throw ex;
			}
		}

		public bool DropSelectedRoles(bool confirm)
		{
			if (grd.SelectedRows.Count == 0)
				return false;

			bool removeUsers = false;
			if (confirm && DropRoleConfirmation.ShowConfirmation(ref removeUsers) != DialogResult.Yes)
				return false;

			string roletype = String.Empty;
			using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
			{
				foreach (DataGridViewRow row in grd.SelectedRows)
				{
					if (row.Cells[2].Value == null || row.Cells[2].GetType() == typeof(DBNull))
						continue;

					//Remove users
					if (removeUsers)
						DbCmd.RemoveUsersFromRole(conn, row.Cells[2].Value.ToString());

					roletype = (string)row.Cells[3].Value;
					DbCmd.DropRole(conn, _cp.Database, row.Cells[2].Value.ToString(), roletype.ToLower() == "standard");
				}
			}

			RefreshRoles();
			return true;
		}

		#endregion //Public methods

		private void grd_DoubleClick(object sender, EventArgs e)
		{
			ModifySelectedRoles(this.Parent as DockContent);
		}

	}
}
