using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using Microsoft.SqlServer.Management.Common;

using PragmaSQL.Core;
using ICSharpCode.Core;
using WizardBase;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace PragmaSQL.Scripting.Smo
{
	public partial class BulkCopyDialog : KryptonForm
	{
		#region Nested Classes

		private enum ProgressType
		{
			TableProgress,
			RowTotals,
			RowProgress
		}

		private class ProgressData
		{
			public ProgressType PrgType = ProgressType.TableProgress;
			public string Info = String.Empty;
			public long Total = 0;
			public long Progress = 0;
		}

		public class BulkCopyArgs
		{
			public IList<DbObjectList.DbObjectInfo> Objects = null;
			public bool StopOnError = true;
			public SqlBulkCopyOptions CopyOptions = SqlBulkCopyOptions.Default;
		}

		public class BulkCopyResult
		{
			public string Errors = String.Empty;
		}

		#endregion //NestedClasses

		#region Fields And Properties

		private ConnectionParams _cp;
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ConnectionParams ConnParams
		{
			get { return _cp; }
			set
			{
				if (value != null)
					_cp = value.CreateCopy();
				else
					_cp = value;
				objList.ConnParams = _cp;
			}
		}

		private DateTime _startTime = DateTime.Now;
		private ConnectionParams _cpDest = null;
		private bool _cancelled = false;
		private bool _copying = false;
		private int _totalCopied = 0;
		#endregion //Fields And Properties

		#region CTOR

		public BulkCopyDialog()
		{
			InitializeComponent();
			lblRowPrg.Text = String.Empty;
			lblTotal.Text = String.Empty;
			objList.ObjectTypes = DbObjectListObjectTypes.Table | DbObjectListObjectTypes.View;
			InitializeCopyOptions();
		}

		#endregion //CTOR

		#region Static Methods

		public static void ShowBulkCopyDialog()
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

			ConnectionParams cp = srv.SelNode.ConnParams.CreateCopy();
			cp.Database = srv.SelNode.DatabaseName;
			ShowBulkCopyDialog(cp);
		}

		public static void ShowBulkCopyDialog(ConnectionParams cp)
		{

			if (cp == null)
				throw new ArgumentNullException("cp", "Connection parameters object is null!");

			try
			{
				FuzzyWait.ShowFuzzyWait("Preparing PragmaSQL Bulk Copy wizard...");
				BulkCopyDialog frm = new BulkCopyDialog();
				frm.ConnParams = cp;
				frm.Text = "PragmaSQL Bulk Copy from [" + cp.InfoDbServer + "]";
				frm.Show();
			}
			finally
			{
				FuzzyWait.CloseFuzzyWait();
			}
		}

		#endregion // Static Methods

		#region Methods

		private void DumpSelectedObjects()
		{
			string caption = "Objects [" + _cp.InfoDbServer + "]";
			HostServicesSingleton.HostServices.EditorServices.CreateTextEditor(caption, objList.DumpSelectedObjects());
		}

		private void ResetWizardState()
		{
			wizardControl1.BackButtonEnabled = true;
			wizardControl1.NextButtonEnabled = true;
			pbRow.Value = 0;
			timer1.Enabled = false;
		}

		private void StartTimer()
		{
			_startTime = DateTime.Now;
			timer1.Enabled = true;
		}

		private bool LoadDataAndCopy(string tableName, SqlBulkCopyOptions options, SqlConnection sourceConn, SqlConnection destConn, SqlTransaction tr, out string errors, out bool cancelled)
		{
			bool result = true;
			string[] strTable = tableName.Split('.');
			SqlCommand sourceCmd = null;
			SqlDataReader sourceReader = null;

			SqlBulkCopy bulkCopy = null;
			SqlCommand mapCmd = null;
			errors = String.Empty;
			cancelled = false;
			try
			{
				NotifyTotalRows(tableName, sourceConn);

				//Load data
				sourceCmd = new SqlCommand("SELECT * FROM " + tableName, sourceConn);
				sourceCmd.CommandTimeout = 0;
				sourceReader = sourceCmd.ExecuteReader();


				//Map Columns
				bulkCopy = new SqlBulkCopy(destConn, options, tr);
				bulkCopy.NotifyAfter = 1;
				bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(bulkCopy_SqlRowsCopied);

				//mapCmd = new SqlCommand("SELECT COLUMN_NAME,COLUMNPROPERTY( OBJECT_ID('" + strTable[0] + "." + strTable[1] + "'),COLUMN_NAME,'IsComputed')AS 'IsComputed' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '" + strTable[0] + "' AND TABLE_NAME = '" + strTable[1] + "'", destConn);
				mapCmd = new SqlCommand(String.Format(Properties.Resources.Script_ColumnMap, strTable[0], strTable[1], strTable[0], strTable[1]), destConn);
				mapCmd.Transaction = tr;

				SqlDataReader mapColReader = null;
				try
				{
					mapColReader = mapCmd.ExecuteReader();
					while (mapColReader.Read())
					{
						if (mapColReader.GetInt32(1) != 1)
							bulkCopy.ColumnMappings.Add(mapColReader.GetString(0), mapColReader.GetString(0));
					}

				}
				finally
				{
					if (!mapColReader.IsClosed)
						mapColReader.Close();
					mapColReader.Dispose();
				}

				//Copy data
				bulkCopy.BatchSize = 50;
				bulkCopy.BulkCopyTimeout = 0;
				bulkCopy.DestinationTableName = tableName;
				bulkCopy.WriteToServer(sourceReader);
			}
			catch (OperationAbortedException)
			{
				cancelled = true;
				result = false;
			}
			catch (Exception ex)
			{
				result = false;
				errors = SmoHelpers.FormatExceptionMsg(ex, "Table/View: " + tableName);
			}
			finally
			{
				if (sourceCmd != null)
					sourceCmd.Dispose();

				if (sourceReader != null)
				{
					if (!sourceReader.IsClosed)
						sourceReader.Close();

					sourceReader.Dispose();
				}

				if (mapCmd != null)
					mapCmd.Dispose();

				if (bulkCopy != null)
				{
					bulkCopy.Close();
				}
			}
			return result;
		}

		private void NotifyTotalRows(string tblName, SqlConnection conn)
		{
			ProgressData pd = new ProgressData();
			pd.PrgType = ProgressType.RowTotals;
			pd.Total = SourceRecordCnt(tblName, conn);
			bw.ReportProgress(0, pd);
		}

		private int SourceRecordCnt(string tblName, SqlConnection conn)
		{
			SqlCommand sourceCmd = null;
			SqlDataReader sourceReader = null;
			int result = 0;

			try
			{
				sourceCmd = new SqlCommand("SELECT count(*) FROM " + tblName, conn);
				sourceCmd.CommandTimeout = 0;
				sourceReader = sourceCmd.ExecuteReader();


				if (sourceReader.HasRows && sourceReader.Read())
				{
					result = sourceReader.GetInt32(0);
				}
			}
			finally
			{
				if (sourceCmd != null)
					sourceCmd.Dispose();

				if (sourceReader != null)
				{
					if (!sourceReader.IsClosed)
						sourceReader.Close();

					sourceReader.Dispose();
				}
			}

			return result;
		}

		void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
		{
			if (_cancelled)
			{
				e.Abort = true;
			}

			ProgressData pd = new ProgressData();
			pd.PrgType = ProgressType.RowProgress;
			pd.Progress = e.RowsCopied;
			bw.ReportProgress(0, pd);
		}

		private void DeleteAllData(SqlConnection destConn, SqlTransaction joinTr)
		{
			SqlCommand cmd = null;
			try
			{
				cmd = destConn.CreateCommand();
				cmd.CommandTimeout = 0;
				if (joinTr != null)
					cmd.Transaction = joinTr;

				cmd.CommandText = Properties.Resources.Script_DeleteAllDatabaseData;
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new Exception("Can not empty database.", ex);
			}
			finally
			{
				if (cmd == null)
					cmd.Dispose();
			}
		}

		private void DeleteData(SqlConnection destConn, SqlTransaction joinTr, IList<DbObjectList.DbObjectInfo> objects)
		{
			SqlCommand cmd = null;
			//SqlTransaction tr = null;
			try
			{
				cmd = destConn.CreateCommand();
				cmd.CommandTimeout = 0;

				//tr = destConn.BeginTransaction();
				if (joinTr != null)
					cmd.Transaction = joinTr;

				if (objects.Count > 0)
				{

					//Disable Constraints on all tables before deleting data
					foreach (DbObjectList.DbObjectInfo obj in objects)
					{
						cmd.CommandText = "ALTER TABLE " + obj.QualifiedSchemaAndName + " NOCHECK CONSTRAINT ALL";
						cmd.CommandText += " ALTER TABLE " + obj.QualifiedSchemaAndName + " DISABLE TRIGGER ALL";
						cmd.ExecuteNonQuery();
					}

					//Delete data in selected tables and log results
					foreach (DbObjectList.DbObjectInfo obj in objects)
					{
						//string disableConstraintsCmdText = "ALTER TABLE " + obj.QualifiedSchemaAndName + " NOCHECK CONSTRAINT ALL \r\n";
						//string disableTrgCmdText = " ALTER TABLE " + obj.QualifiedSchemaAndName + " DISABLE TRIGGER ALL \r\n";

						cmd.CommandText = "SELECT OBJECTPROPERTY ( object_id('" + obj.QualifiedSchemaAndName + "'),'TableHasForeignRef')";
            object scalarVal = cmd.ExecuteScalar();

						int intref = Convert.ToInt32(scalarVal.GetType() == typeof(DBNull) ? 0 :scalarVal);
						if (intref == 1)
						{
              cmd.CommandText = "DELETE FROM " + obj.Qualify(obj.QualifiedSchemaAndName);
						}
						else
						{
              cmd.CommandText = "TRUNCATE TABLE " + obj.Qualify(obj.QualifiedSchemaAndName);
						}
						cmd.ExecuteNonQuery();

					}

					//Enable Constraints on all tables
					foreach (DbObjectList.DbObjectInfo obj in objects)
					{
						cmd.CommandText = "ALTER TABLE " + obj.QualifiedSchemaAndName + " CHECK CONSTRAINT ALL";
						cmd.CommandText += " ALTER TABLE " + obj.QualifiedSchemaAndName + " ENABLE TRIGGER ALL";
						cmd.ExecuteNonQuery();
					}
				}
				//tr.Commit();
			}
			catch (Exception ex)
			{
				//tr.Rollback();
				throw new Exception("Can not empty specified table data on destination database.", ex);
			}
			finally
			{
				if (cmd == null)
					cmd.Dispose();
			}

		}

		#endregion //Methods

		#region Copy Options

		private void InitializeCopyOptions()
		{
			lvOptions.Items.Clear();
			Array values = Enum.GetValues(typeof(SqlBulkCopyOptions));
			foreach (SqlBulkCopyOptions opt in values)
			{
				if (opt == SqlBulkCopyOptions.UseInternalTransaction)
					continue;

				ListViewItem item = lvOptions.Items.Add(opt.ToString());
				switch (opt)
				{
					case SqlBulkCopyOptions.KeepIdentity:
					case SqlBulkCopyOptions.TableLock:
						item.Checked = true;
						break;
					default:
						break;
				}
			}

			lvOptions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private SqlBulkCopyOptions EvalSqlBulkCopyOptions()
		{
			if (lvOptions.CheckedItems.Count == 0)
				return SqlBulkCopyOptions.Default;

			SqlBulkCopyOptions result = SqlBulkCopyOptions.Default;
			bool reset = true;
			foreach (ListViewItem item in lvOptions.CheckedItems)
			{
				SqlBulkCopyOptions opt = (SqlBulkCopyOptions)Enum.Parse(typeof(SqlBulkCopyOptions), item.Text);
				if (reset)
					result = opt;
				else
					result = result | opt;
			}
			return result;
		}

		#endregion //Copy Options


		private void wizardControl1_HelpButtonClick(object sender, EventArgs e)
		{
			MessageBox.Show("PragmaSQL Bulk Copy\r\nCopyright 2007 - 2009 Ali Özgür", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
		{
      if (TaskbarManager.IsPlatformSupported)
      {  
        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal,this.Handle);
        TaskbarManager.Instance.SetProgressValue(0, 100,this.Handle);
      }
      StartBulkCopy();
		}

		private void StartBulkCopy()
		{
			if (_cpDest == null)
			{
				MessageService.ShowError("Destination connection not specified!");
				return;
			}

			string confirmMsg = String.Format(Properties.Resources.CopyDataConfirmation, _cp.InfoDbServer, _cpDest.InfoDbServer);
			SqlBulkCopyOptions ops = EvalSqlBulkCopyOptions();
			if ((ops & SqlBulkCopyOptions.TableLock) == SqlBulkCopyOptions.TableLock)
			{
				int majorServerVersion = SmoHelpers.GetServerMajorVersion(_cpDest);
				if (majorServerVersion == 9 && !MessageService.AskQuestion(Properties.Resources.TableLock_Notification + confirmMsg))
					return;
			}
			else if (!MessageService.AskQuestion(confirmMsg))
				return;

			BulkCopyArgs args = new BulkCopyArgs();
			args.Objects = objList.SelectedObjects;
			args.StopOnError = chkStopOnError.Checked;
			args.CopyOptions = EvalSqlBulkCopyOptions();
			_copying = true;
			_cancelled = false;

			wizardControl1.NextButtonEnabled = false;
			wizardControl1.BackButtonEnabled = false;

			_totalCopied = 0;
			lblTotal.Text = String.Empty;

			StartTimer();

			bw.RunWorkerAsync(args);
		}

		private void bw_DoWork(object sender, DoWorkEventArgs e)
		{

			BulkCopyArgs args = e.Argument as BulkCopyArgs;
			if (args == null)
				return;

			BulkCopyResult result = new BulkCopyResult();


			ProgressData pd = new ProgressData();
			pd.Total = args.Objects.Count;
			SqlTransaction tr = null;
			string copyErrors = String.Empty;
			bool copyCancelled = false;
			SqlConnection sourceConn = null;
			SqlConnection destConn = null;

			try
			{
				sourceConn = _cp.CreateSqlConnection(true, false);
				destConn = _cpDest.CreateSqlConnection(true, false);
				try
				{
					if (args.StopOnError)
						tr = destConn.BeginTransaction();


					try
					{
						if (rbEmpty.Checked)
						{
							DeleteData(destConn, tr, args.Objects);
						}
						else if (rbEmptyAll.Checked)
						{
							DeleteAllData(destConn, tr);						
						}
					}
					catch (Exception ex)
					{
						if (args.StopOnError)
							tr.Rollback();

						result.Errors += SmoHelpers.FormatExceptionMsg(ex);
						return;
					}

					foreach (DbObjectList.DbObjectInfo selObj in args.Objects)
					{
						try
						{
							pd.Info = selObj.Name;
							pd.Progress++;

							if (_cancelled || bw.CancellationPending)
							{
								e.Cancel = true;
								if (args.StopOnError)
									tr.Rollback();

								break;
							}
							else
							{
								bw.ReportProgress(0, pd);
								if (!LoadDataAndCopy(selObj.SchemaAndName, args.CopyOptions, sourceConn, destConn, tr, out copyErrors, out copyCancelled))
								{
									if (args.StopOnError)
										tr.Rollback();

									if (copyCancelled)
									{
										e.Cancel = true;
										break;
									}


									result.Errors += copyErrors;
									if (args.StopOnError)
										break;
								}
							}
						}
						catch (Exception ex)
						{
							if (args.StopOnError)
							{
								tr.Rollback();
								result.Errors += SmoHelpers.FormatExceptionMsg(ex, "Table/View: " + selObj.Name);
								break;
							}
							else
							{
								result.Errors += SmoHelpers.FormatExceptionMsg(ex, "Table/View: " + selObj.Name);
							}
						}
					}

					if (args.StopOnError && tr.Connection != null)
						tr.Commit();
				}
				catch (Exception ex)
				{
					result.Errors += SmoHelpers.FormatExceptionMsg(ex);
				}
			}
			finally
			{
				e.Result = result;

				if (sourceConn != null)
				{
					if (sourceConn.State != ConnectionState.Closed)
						sourceConn.Close();
					sourceConn.Dispose();
				}

				if (destConn != null)
				{
					if (destConn.State != ConnectionState.Closed)
						destConn.Close();
					destConn.Dispose();
				}
			}
		}


		private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
      if (TaskbarManager.IsPlatformSupported)
      {
        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress,this.Handle);
        TaskbarManager.Instance.SetProgressValue(0,100,this.Handle);
      }

      timer1.Enabled = false;
			lblTimer.Text = "Elapsed: 00:00:00";

			lblRowPrg.Text = String.Empty;
			pbRow.Value = 0;
			_copying = false;
			_cancelled = false;

			wizardControl1.NextButtonEnabled = true;
			wizardControl1.BackButtonEnabled = true;

			if (e.Cancelled)
			{
				lblOverallPrg.Text = "Cancelled by the user";
				lblTotal.Text = String.Empty;
				return;
			}
			else if (e.Error == null)
			{
				lblOverallPrg.Text = "Completed";
			}

			if (e.Error != null)
			{
				lblOverallPrg.Text = "Terminated with errors";
				lblTotal.Text = String.Empty;
				GenericErrorDialog.ShowError("Bulk Copy Error", "Bulk copy terminated because the following error occured.", e.Error);
				return;
			}

			BulkCopyResult result = e.Result as BulkCopyResult;
			DisplayErrors(result.Errors);
		}

		private void DisplayErrors(string errors)
		{
			if (String.IsNullOrEmpty(errors))
				return;
			lblOverallPrg.Text = "Completed with errors";
			GenericErrorDialog.ShowError("Bulk Copy Error", "Bulk copy completed with errors.", errors);
		}

		private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
		{
			if (wizardControl1.CurrentStepIndex == 2)
			{
				if (bw.IsBusy)
					CancelBulkCopy();
				else
					this.Close();
			}
			else
				this.Close();

		}

		private void CancelBulkCopy()
		{
			_cancelled = true;
			bw.CancelAsync();
		}

		private void BatchScripterDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (bw.IsBusy)
			{
				CancelBulkCopy();
				while (bw.IsBusy)
				{
					Application.DoEvents();
				}
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			TimeSpan startSpan = TimeSpan.FromTicks(_startTime.Ticks);
			TimeSpan endSpan = TimeSpan.FromTicks(DateTime.Now.Ticks);

			TimeSpan diff = endSpan - startSpan;
			diff = endSpan.Subtract(startSpan);

			lblTimer.Text = "Elapsed: " + diff.Hours.ToString("00")
				+ ":" + diff.Minutes.ToString("00")
				+ ":" + diff.Seconds.ToString("00");
		}

		private void objList_DumpToTexteditorCompleted(object sender, EventArgs e)
		{
			this.BringToFront();
		}

		private void wizardControl1_NextButtonClick(object sender, GenericCancelEventArgs<WizardControl> tArgs)
		{
			if (wizardControl1.CurrentStepIndex == 1 && objList.SelectedObjectCount == 0)
			{
				MessageService.ShowError("You did not selected any objects.");
				tArgs.Cancel = true;
			}
		}


		private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ProgressData pd = e.UserState as ProgressData;
			if (pd == null)
				return;

			switch (pd.PrgType)
			{
				case ProgressType.TableProgress:
					lblOverallPrg.Text = String.Format("Copying ({0} of {1}) : {2}", pd.Progress, pd.Total, pd.Info);
					lblRowPrg.Text = String.Empty;
					pbRow.Value = 0;
					break;
				case ProgressType.RowTotals:
					lblRowPrg.Text = String.Format("Copied (0 of {0}) row(s)", pd.Total);
					pbRow.Maximum = (int)pd.Total;
					pbRow.Value = 0;
					lblTotal.Text = "Total rows processed: " + _totalCopied.ToString();
					break;
				case ProgressType.RowProgress:
					lblRowPrg.Text = String.Format("Copied ({0} of {1}) row(s)", pd.Progress, pbRow.Maximum);
					pbRow.Value = (int)pd.Progress;
          if (TaskbarManager.IsPlatformSupported)
          {
            TaskbarManager.Instance.SetProgressValue((int)pd.Progress, pbRow.Maximum,this.Handle);
          }

					_totalCopied++;
					lblTotal.Text = "Total rows processed: " + _totalCopied.ToString();
					break;
				default:
					break;
			}
		}

		private void OnDeleteDataCheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = sender as RadioButton;
			if (rb == null)
				return;

			if (rb.Checked)
			{
				rb.ForeColor = Color.Red;
				rb.Font = new Font(chkStopOnError.Font, FontStyle.Bold);
			}
			else
			{
				rb.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
				rb.Font = new Font(chkStopOnError.Font, FontStyle.Regular);
			}
		}

		private void SelectDestConnFromRepo()
		{
			ConnectionParams cp = frmConnectionRepository.SelectSingleConnection();
			if (cp == null)
				return;

			_cpDest = cp.CreateCopy();
			edtDestConn.Text = _cpDest.InfoDbServer;
		}

		private void CreateNewDestConn()
		{
			ConnectionParams cp = frmConnectionParams.CreateConnection(true);
			if (cp == null)
				return;

			_cpDest = cp.CreateCopy();
			edtDestConn.Text = _cpDest.InfoDbServer;
		}

		private void btnDestFromRepo_Click(object sender, EventArgs e)
		{
			SelectDestConnFromRepo();
		}

		private void btnDestNew_Click(object sender, EventArgs e)
		{
			CreateNewDestConn();
		}
	}
}