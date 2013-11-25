using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using PragmaSQL.Core;
using AsynchronousCodeBlocks;
using ComponentFactory.Krypton.Toolkit;
namespace PragmaSQL
{
	public partial class frmObjectSelector : KryptonForm
	{
		private IDictionary<string, string> _tableAliases = new Dictionary<string, string>();

		private DataTable _tblDatabases = null;
		private BindingSource _bsDatabases = new BindingSource();

		private DataTable _tblUsers = null;
		private BindingSource _bsUsers = new BindingSource();

		private DataTable _tblObjects = null;
		private BindingSource _bsObjects = new BindingSource();
		private SqlConnection _conn = new SqlConnection();

		private string _currentDatabase = String.Empty;
		private string _defaultDatabase = String.Empty;
		private bool _parsedObjectsAvailable = false;

		private bool _isInitialized = false;
		public bool IsInitialized
		{
			get { return _isInitialized; }
		}
		private SqlAnalyzerResults _aResults = null;

		public event NotificationHandler UserRequestedDismiss;
		public event NotificationHandler UserMadeFinalSelection;
		public event KeyPressHandler UserPressedKey;
		public event NotificationHandler UserPressedBackSpace;

    private int CodeCompletionCacheTimeout
    {
      get
      {
        return ConfigHelper.Current.TextEditorOptions.CodeCompCacheTimeout * 60;  
      }
    }

		public frmObjectSelector()
		{
			InitializeComponent();
		}

		public void SetLocation(int x, int y)
		{
			int tmpHeight = 500;
			this.Height = 0;
			//this.Location = new Point(x, y + 15);
			Screen scr = Screen.FromControl(this);
			Rectangle r = scr.WorkingArea;
			if (y + tmpHeight > r.Bottom)
			{
				if ((y - tmpHeight) < r.Top)
				{
					this.Location = new Point(x, 0);
					this.Height = y - 15;
				}
				else
				{
					this.Location = new Point(x, y - tmpHeight - 15);
					this.Height = 500;
				}
			}
			else
			{
				this.Location = new Point(x, y + 15);
				this.Height = 500;
			}
		}

		public void SetConnection(SqlConnection conn)
		{
			if (conn == null)
			{
				throw new Exception("Database connection is null!");
			}

			if (conn.State != ConnectionState.Open)
			{
				throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
			}

			if (_conn.State == ConnectionState.Open)
			{
				_conn.Close();
			}

			_conn.ConnectionString = conn.ConnectionString;
			_defaultDatabase = conn.Database;
			PopulateDatabasesAndUsers(false);
		}

		public void InitializeCompletionProposal(SqlConnection conn)
		{
			_currentDatabase = String.Empty;
			SetConnection(conn);
			PopulateDatabasesAndUsers(false);
		}

		private void PopulateDatabasesAndUsers(bool forceCacheRefresh)
		{
			bool closeConn = false;
			if (_conn.State != ConnectionState.Open)
			{
				_conn.Open();
				closeConn = true;
			}

			try
			{
				PopulateDatabases(forceCacheRefresh);
				PopulateUsers(forceCacheRefresh);
			}
			finally
			{
				if (closeConn)
				{
					_conn.Close();
				}
			}
		}

		private void PopulateDatabases(bool forceCacheRefresh)
		{
			if (_tblDatabases != null)
			{
				_tblDatabases.Clear();
				_tblDatabases.Dispose();
			}

			CachedDataTableIdentifier id = new CachedDataTableIdentifier(_conn.DataSource, _conn.Database, "Databases", CachedDataType.Databases);
      id.Timeout = CodeCompletionCacheTimeout;
			if (forceCacheRefresh)
				CodeCompletionProposalCache.StoreData(_conn, id);

			_tblDatabases = CodeCompletionProposalCache.GetData(_conn, id);
			_bsDatabases.DataSource = _tblDatabases;
		}

		private void PopulateUsers(bool forceCacheRefresh)
		{
			if (_tblUsers != null)
			{
				_tblUsers.Clear();
				_tblUsers.Dispose();
			}

			CachedDataTableIdentifier id = new CachedDataTableIdentifier(_conn.DataSource, _conn.Database, "Users", CachedDataType.Users);
      id.Timeout = CodeCompletionCacheTimeout;
			if (forceCacheRefresh)
				CodeCompletionProposalCache.StoreData(_conn, id);

			_tblUsers = CodeCompletionProposalCache.GetData(_conn, id);
			_bsUsers.DataSource = _tblUsers;
		}

		private void PopulateObjects(bool forceCacheRefresh)
		{
			if (_tblObjects != null)
			{
				_tblObjects.Clear();
				_tblObjects.Dispose();
			}

			CachedDataTableIdentifier id = new CachedDataTableIdentifier(_conn.DataSource, _conn.Database, "Objects", CachedDataType.Objects);
      id.Timeout = CodeCompletionCacheTimeout;
			if (forceCacheRefresh)
				CodeCompletionProposalCache.StoreData(_conn, id);

			_tblObjects = CodeCompletionProposalCache.GetData(_conn, id);
		}

		public void PopulateCodeCompletionList(bool forceCacheRefresh)
		{
			try
			{
				grd.Cursor = Cursors.WaitCursor;
				this.Cursor = Cursors.WaitCursor;

				label1.Visible = false;

				_conn.Open();
				if (_conn.Database.ToLowerInvariant() != _currentDatabase && !String.IsNullOrEmpty(_currentDatabase))
				{
					_conn.ChangeDatabase(_currentDatabase);
				}


				_bsObjects.Sort = String.Empty;
				_bsObjects.Filter = String.Empty;
				_bsObjects.RaiseListChangedEvents = false;

				PopulateDatabases(forceCacheRefresh);
				PopulateUsers(forceCacheRefresh);
				PopulateObjects(forceCacheRefresh);

				_isInitialized = true;
				BindGrid();

				if (forceCacheRefresh)
				{
					ClearParsedObjects();
					InsertParsedObjects(_aResults);
				}
			}
			finally
			{
				_conn.Close();
				if (_bsObjects.Count > 0)
				{
					_bsObjects.Position = 0;
				}
				_bsObjects.RaiseListChangedEvents = true;
				label1.Visible = true;
				this.Cursor = Cursors.Default;
				grd.Cursor = Cursors.Default;
			}
		}

		private bool IsValidDatabase(string dbName)
		{
			if (_tblDatabases == null)
			{
				return false;
			}
			return (_bsDatabases.Find("Name", dbName) >= 0);
		}

		private bool IsValidUser(string userName)
		{
			if (_tblUsers == null)
			{
				return false;
			}
			return (_bsUsers.Find("Name", userName) >= 0);
		}

		private string TableNameFromAlias(string potentialAlias)
		{
			if (_tableAliases.ContainsKey(potentialAlias.ToLowerInvariant()))
			{
				return _tableAliases[potentialAlias.ToLowerInvariant()];
			}
			else
			{
				return potentialAlias;
			}
		}

		private void BindGrid()
		{
			_bsObjects.DataSource = null;
			grd.DataSource = null;

			_bsObjects.DataSource = _tblObjects;
			grd.DataSource = _bsObjects;

			
			foreach (DataGridViewColumn col in grd.Columns)
			{
				col.Visible = col.DataPropertyName == "ParentName"  || col.DataPropertyName == "Type" || col.DataPropertyName == "Name" || col.DataPropertyName == "QualifiedDataType";
        col.Name = String.Format("col{0}", col.DataPropertyName);

        if (col.DataPropertyName == "Type")
				{
					col.Width = 40;
				}
				
				if (col.DataPropertyName == "Type")
				{
					col.DisplayIndex= 0;
					col.HeaderText = "Type";
        }
				else if (col.DataPropertyName == "DiplayName")
				{
					col.DisplayIndex = 1;
					col.HeaderText = "Object Name";
        }
				else if (col.DataPropertyName == "Name")
				{
					col.DisplayIndex = 2;
					col.HeaderText = "Object Name";
					col.Width = 165;
          col.Name = "colName";
        }
				else if (col.DataPropertyName == "QualifiedDataType")
				{
					col.DisplayIndex = 3;
					col.HeaderText = "DataType";
				}
				else if (col.DataPropertyName == "ParentName")
				{
					col.DisplayIndex = 4;
					col.HeaderText = "Parent";
				}
			}
			

			_bsObjects.Sort = "Order ASC, Name ASC";
		}

		private void PerformCustomFilter(string value, bool lostFocus)
		{
			if (_bsObjects.DataSource == null)
			{
				return;
			}
			
			string filterStr = String.Empty;
			string[] filterParts = value.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			
			if (filterParts.Length == 1)
			{
				if (value.Contains("."))
				{
					filterStr = "ParentName Like '%{0}%'";
					filterStr = String.Format(filterStr, filterParts[0]);
				}
				else
				{
					filterStr = "DisplayName Like '%{0}%' OR DataType Like '%{0}%'";
					filterStr = String.Format(filterStr, filterParts[0]);
				}
			}
			else if (filterParts.Length >= 2)
			{
				filterStr = " ParentName Like '%{0}%' AND DisplayName Like '%{1}%' ";
				filterStr = String.Format(filterStr, filterParts[filterParts.Length-2], filterParts[filterParts.Length - 1]);
			}

			_bsObjects.Filter = filterStr;
			_bsObjects.Sort = "Order ASC, Name ASC";
			if (lostFocus)
			{
				grd.Focus();
			}

		}
		private void frmCodeCompletion_Deactivate(object sender, EventArgs e)
		{
			_parsedObjectsAvailable = false;
			this.Hide();
		}

		public void JumpTo(string jumpString, SqlAnalyzerResults aResults)
		{
			_aResults = aResults;
			string parentNamePlaceHolder = "$\x26\x26\x26$";
			string parentName = String.Empty;

			string filterStr = String.Empty;
			string dbName = _defaultDatabase;
			string schemaName = "???";
			bool isInvalidFilter = false;

			label1.Text = jumpString;
			CustomStringTokenizer tok = new CustomStringTokenizer(jumpString);
			IList<CustomToken> tokens = new List<CustomToken>();

			CustomToken token = null;
			do
			{
				token = tok.Next();

				if (token.Kind == CustomTokenKind.EOL || token.Kind == CustomTokenKind.EOF || token.Kind == CustomTokenKind.Unknown
						|| (token.Kind == CustomTokenKind.Symbol && token.Value != ".")
					)
				{
					continue;
				}
				tokens.Add(token);

			} while (token.Kind != CustomTokenKind.EOF);


			if (tokens.Count == 0)
			{
				filterStr = " ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr')";
				schemaName = "dbo";
			}
			// 1-  Object1
			else if (tokens.Count == 1)
			{
				filterStr = " ( Type <> 'Param' AND Type <> 'Col' ) "
					+ " AND ( Name Like '" + tokens[0].Value + "%') ";

				schemaName = "dbo";
			}
			// 2- Object1.
			else if (tokens.Count == 2)
			{
				CustomToken lastToken = tokens[1];

				if (lastToken.Value == ".")
				{
					CustomToken parentToken = tokens[0];
					if (IsValidDatabase(parentToken.Value))
					{
						dbName = parentToken.Value;
						filterStr = "( Type = 'Usr' )";
					}
					else if (IsValidUser(parentToken.Value))
					{
						dbName = _defaultDatabase;
						filterStr = "( Catalog = '" + _defaultDatabase + "' OR Catalog = '') "
							+ " AND ( Schema = '" + parentToken.Value + "' OR Schema = '') "
							+ " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr' AND Type <> 'Db') ";
						schemaName = parentToken.Value;
					}
					else
					{
						dbName = _defaultDatabase;
						parentName = parentToken.Value;
						filterStr = "ParentName = '" + parentNamePlaceHolder + "' "
							+ " AND ( "
							+ " ( ( Catalog = '" + _defaultDatabase + "' OR Catalog = '') "
							+ " AND ( Schema = 'dbo' ) OR ( Schema = '')"
							+ " AND ( Type = 'Param' OR Type = 'Col' ) ) "
							+ " OR (IsOffline = 1) "
							+ " )";

						schemaName = "dbo";
					}
				}
				else
				{
					dbName = _defaultDatabase;
					// Invalid filter
					filterStr = " ( Type = '@@@')";
					isInvalidFilter = true;
				}
			}
			// 3- Object1.Object2, Object1..
			else if (tokens.Count == 3)
			{
				CustomToken lastToken = tokens[2];
				// 3.1 - Object1..
				if (lastToken.Value == ".")
				{
					CustomToken parentToken = tokens[0];
					if (IsValidDatabase(parentToken.Value))
					{
						dbName = parentToken.Value;
						filterStr = "( Catalog = '" + dbName + "' OR Catalog = '') "
							+ " AND ( Schema = 'dbo' OR Schema = '') "
							+ " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Db' AND Type <> 'Usr') ";

						schemaName = "dbo";
					}
					else
					{
						dbName = _defaultDatabase;
						// Invalid filter
						filterStr = " ( Type = '@@@')";
						isInvalidFilter = true;
					}
				}
				// 3.2 - Object1.Object2
				else
				{
					CustomToken parentToken = tokens[0]; // Object1
					if (IsValidDatabase(parentToken.Value))
					{
						dbName = parentToken.Value;
						filterStr = "Name Like '" + tokens[2].Value + "%' "
							+ " AND ( Type = 'Usr')";
						schemaName = "dbo";
					}
					else if (IsValidUser(parentToken.Value))
					{
						dbName = _defaultDatabase;
						filterStr = "( Catalog = '" + dbName + "' OR Catalog = '') "
							+ " AND ( Schema = '" + parentToken.Value + "' OR Schema = '') "
							+ " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Db' AND Type <> 'Usr') "
							+ " AND ( Name Like '" + tokens[2].Value + "%') ";
						schemaName = parentToken.Value;
					}
					else
					{
						dbName = _defaultDatabase;
						parentName = parentToken.Value;
						filterStr = "ParentName = '" + parentNamePlaceHolder + "' "
							+ " AND ( Catalog = '" + dbName + "' OR Catalog = '') "
							+ " AND ( Schema = 'dbo' OR Schema = '') "
							+ " AND ( Type = 'Param' OR Type = 'Col') "
							+ " AND ( Name Like '" + tokens[2].Value + "%') ";
						schemaName = "dbo";
					}
				}
			}
			// 4- Object1.Object2.
			else if (tokens.Count == 4)
			{

				if (tokens[3].Value == ".") //.
				{
					if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
					{
						dbName = tokens[0].Value;
						filterStr = "( Catalog = '" + dbName + "' OR Catalog = '') "
							+ " AND ( Schema = '" + tokens[2].Value + "' OR Schema = '')"
							+ " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr' AND Type <> 'Db' ) ";
						schemaName = tokens[2].Value;
					}
					else if (IsValidUser(tokens[0].Value))
					{
						dbName = _defaultDatabase;
						parentName = tokens[2].Value;
						filterStr = "( Catalog = '" + dbName + "' OR Catalog = '') "
							+ " AND ( Schema = '" + tokens[0].Value + "' OR Schema = '')"
							+ " AND ( Type = 'Param' OR Type = 'Col')"
							+ " AND ( ParentName = '" + parentNamePlaceHolder + "' ) ";
						schemaName = tokens[2].Value;
					}
					else
					{
						dbName = _defaultDatabase;
						// Invalid filter
						filterStr = " ( Type = '@@@')";
						isInvalidFilter = true;
					}
				}
				else
				{
					dbName = _defaultDatabase;
					// Invalid filter
					filterStr = " ( Type = '@@@')";
					isInvalidFilter = true;
				}
			}
			// 5- Object1.Object2.Object3
			else if (tokens.Count == 5)
			{
				if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
				{
					dbName = tokens[0].Value;
					filterStr = "( Catalog = '" + dbName + "' OR Catalog = '') "
						+ " AND ( Schema = '" + tokens[2].Value + "' OR Schema = '')"
						+ " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr' AND Type <> 'Db' ) "
						+ " AND ( Name Like '" + tokens[4].Value + "%') ";
					schemaName = tokens[2].Value;
				}
				else
				{
					dbName = _defaultDatabase;
					// Invalid filter
					filterStr = " ( Type = '@@@')";
					isInvalidFilter = true;
				}
			}
			// 6- Object1.Object2.Object3.
			else if (tokens.Count == 6)
			{
				if (tokens[5].Value == ".")
				{
					if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
					{
						dbName = tokens[0].Value;
						parentName = tokens[4].Value;
						filterStr = "( Catalog = '" + dbName + "' OR Catalog = '') "
							+ " AND ( Schema = '" + tokens[2].Value + "' OR Schema = '')"
							+ " AND ( Type = 'Param' OR Type = 'Col')"
							+ " AND ( ParentName = '" + parentNamePlaceHolder + "') ";
						schemaName = tokens[2].Value;
					}
					else
					{
						dbName = _defaultDatabase;
						// Invalid filter
						filterStr = " ( Type = '@@@')";
						isInvalidFilter = true;
					}
				}
				else
				{
					dbName = _defaultDatabase;
					// Invalid filter
					filterStr = " ( Type = '@@@')";
					isInvalidFilter = true;
				}

			}
			// 7- Object1.Object2.Object3.Object4
			else if (tokens.Count == 7)
			{
				if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
				{
					dbName = tokens[0].Value;
					parentName = tokens[4].Value;
					filterStr = "( Catalog = '" + dbName + "' OR Catalog = '') "
						+ " AND ( Schema = '" + tokens[2].Value + "' OR Schema = '')"
						+ " AND ( Type = 'Param' OR Type = 'Col')"
						+ " AND ( ParentName = '" + parentNamePlaceHolder + "') "
						+ " AND ( Name Like '" + tokens[6].Value + "%') ";
				}
				else
				{
					dbName = _defaultDatabase;
					// Invalid filter
					filterStr = " ( Type = '@@@')";
					isInvalidFilter = true;
				}
			}
			else
			{
				dbName = _defaultDatabase;
				// Invalid filter
				filterStr = " ( Type = '@@@')";
				isInvalidFilter = true;
			}

			if (_currentDatabase.ToLowerInvariant() != dbName.ToLowerInvariant())
			{
				_currentDatabase = dbName;
				PopulateCodeCompletionList(false);
			}

			if (_bsObjects.DataSource == null || _tblObjects == null)
			{
				this.Hide();
				return;
			}


			if (!_parsedObjectsAvailable)
			{
				ClearParsedObjects();
				InsertParsedObjects(aResults);
			}

			if (!String.IsNullOrEmpty(parentName))
			{
				parentName = TableNameFromAlias(parentName);
				filterStr = filterStr.Replace(parentNamePlaceHolder, parentName);
			}

			_bsObjects.Filter = filterStr;
			_bsObjects.Sort = "Order ASC, Name ASC";

			if (_bsObjects.Count > 0)
			{
				_bsObjects.Position = 0;
			}

			if (!isInvalidFilter)
			{
				label2.Text = "Catalog = '" + dbName + "', Schema = '" + schemaName + "'";
			}
			else
			{
				label2.Text = "Can not parse code completion proposal!";
			}
		}

		public void ClearParsedObjects()
		{
			if (_bsObjects.DataSource == null)
			{
				return;
			}

			_bsObjects.Filter = "IsOffline = 1";
			while (_bsObjects.Current != null)
			{
				_bsObjects.RemoveCurrent();
			}
			_tableAliases.Clear();
			_parsedObjectsAvailable = false;
		}

		public void InsertParsedObjects(SqlAnalyzerResults aResults)
		{
			if (aResults == null)
			{
				return;
			}

			DataRow row = null;



			//1- Insert temp tables
			if (aResults.TableAsTemp != null && aResults.TableAsTemp.Values != null)
			{
				foreach (SqlTable tbl in aResults.TableAsTemp.Values)
				{
					row = _tblObjects.NewRow();
					row["Type"] = "Tbl";
					row["Order"] = 0;
					row["Name"] = tbl.TableName;
					row["DisplayName"] = tbl.TableName;
					row["QualifiedDataType"] = "Temp Tbl";
					row["IsOffline"] = true;
					row["Catalog"] = "";
					row["Schema"] = "";
					_tblObjects.Rows.Add(row);
					for (int i = 0; i < tbl.ColumnNames.Count; i++)
					{
						row = _tblObjects.NewRow();
						row["Order"] = 5;
						row["Type"] = "Col";
						row["Name"] = tbl.ColumnNames[i];
						row["DisplayName"] = tbl.FullyQualifiedColumns[i];
						row["QualifiedDataType"] = tbl.ColumnDataTypes[i];
						row["ParentName"] = tbl.TableName;
						row["IsOffline"] = true;
						row["Catalog"] = "";
						row["Schema"] = "";
						_tblObjects.Rows.Add(row);
					}
				}
			}

			//2- Insert local variable tables
			if (aResults.TableAsVariable != null && aResults.TableAsVariable.Values != null)
			{
				foreach (SqlTable tbl in aResults.TableAsVariable.Values)
				{
					row = _tblObjects.NewRow();
					row["Type"] = "Tbl";
					row["Order"] = 0;
					row["Name"] = tbl.TableName;
					row["DisplayName"] = tbl.TableName;
					row["QualifiedDataType"] = "Local Var Tbl";
					row["IsOffline"] = true;
					row["Catalog"] = "";
					row["Schema"] = "";
					_tblObjects.Rows.Add(row);
					for (int i = 0; i < tbl.ColumnNames.Count; i++)
					{
						row = _tblObjects.NewRow();
						row["Type"] = "Col";
						row["Order"] = 5;
						row["Name"] = tbl.ColumnNames[i];
						row["DisplayName"] = tbl.FullyQualifiedColumns[i];
						row["QualifiedDataType"] = tbl.ColumnDataTypes[i];
						row["ParentName"] = tbl.TableName;
						row["IsOffline"] = true;
						row["Catalog"] = "";
						row["Schema"] = "";
						_tblObjects.Rows.Add(row);
					}
				}
			}

			//3- Insert variable names
			if (aResults.VariableNamesWithDataTypes != null && aResults.VariableNamesWithDataTypes.Values != null)
			{
				foreach (SqlVariable v in aResults.VariableNamesWithDataTypes.Values)
				{
					row = _tblObjects.NewRow();
					row["Order"] = -1;
					row["Type"] = "localVar";
					row["Name"] = v.Name;
					row["DisplayName"] = v.FullyQualifiedName;
					row["QualifiedDataType"] = v.DataType;
					row["IsOffline"] = true;
					row["Catalog"] = "";
					row["Schema"] = "";
					_tblObjects.Rows.Add(row);
				}
			}

			//4- Insert table aliases (From)
			if (aResults.TableAliasFrom != null && aResults.TableAliasFrom.Values != null)
			{
				foreach (SqlTableAlias tblAlias in aResults.TableAliasFrom.Values)
				{
					if (tblAlias.TableAlias.ToLowerInvariant() == tblAlias.TableName.ToLowerInvariant())
					{
						continue;
					}

					if (_tableAliases.ContainsKey(tblAlias.TableAlias.ToLowerInvariant()))
					{
						continue;
					}
					_tableAliases.Add(tblAlias.TableAlias.ToLowerInvariant(), tblAlias.TableName.ToLowerInvariant());

					row = _tblObjects.NewRow();
					row["Type"] = "Tbl";
					row["Order"] = 0;
					row["Name"] = tblAlias.TableAlias;
					row["DisplayName"] = tblAlias.TableAlias;
					row["QualifiedDataType"] = "Table Alias";
					row["IsOffline"] = true;
					row["Catalog"] = "";
					row["Schema"] = "";
					_tblObjects.Rows.Add(row);
				}
			}

			//5- Insert table aliases (Join)
			if (aResults.TableAliasJoin != null && aResults.TableAliasJoin.Values != null)
			{
				foreach (SqlTableAlias tblAlias in aResults.TableAliasJoin.Values)
				{
					if (tblAlias.TableAlias.ToLowerInvariant() == tblAlias.TableName.ToLowerInvariant())
					{
						continue;
					}
					if (_tableAliases.ContainsKey(tblAlias.TableAlias.ToLowerInvariant()))
					{
						continue;
					}
					_tableAliases.Add(tblAlias.TableAlias.ToLowerInvariant(), tblAlias.TableName.ToLowerInvariant());

					row = _tblObjects.NewRow();
					row["Type"] = "Tbl";
					row["Order"] = 0;
					row["Name"] = tblAlias.TableAlias;
					row["DisplayName"] = tblAlias.TableAlias;
					row["QualifiedDataType"] = "Table Alias";
					row["IsOffline"] = true;
					row["Catalog"] = "";
					row["Schema"] = "";
					_tblObjects.Rows.Add(row);
				}
			}

			//_bsObjects.Filter = " ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr')";
			//_bsObjects.Sort = "Order ASC, Name ASC";

			_parsedObjectsAvailable = true;
			
		}

		private void grdObjects_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete) && UserRequestedDismiss != null)
			{
				UserRequestedDismiss();
			}

			if (e.KeyCode == Keys.Back && UserPressedBackSpace != null)
			{
				UserPressedBackSpace();
				e.Handled = true; //don't want this to turn in to a keypress
			}

			if ((e.KeyCode == Keys.Enter) && UserMadeFinalSelection != null)
			{
				UserMadeFinalSelection();
			}
		}

		private void grdObjects_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (UserPressedKey != null)
			{
				UserPressedKey(e.KeyChar);
			}
			e.Handled = true;
		}

		private void grdObjects_DoubleClick(object sender, EventArgs e)
		{
			UserMadeFinalSelection();
		}

		public string SelectedItem
		{
			get
			{
				DataRowView row = _bsObjects.Current as DataRowView;
				if (row == null)
				{
					return String.Empty;
				}

				return (string)row["Name"];
			}
		}

		public ArrayList SelectedItemsAsStrings
		{
			get
			{
				ArrayList results = new ArrayList();
				foreach (DataGridViewRow row in grd.SelectedRows)
				{
          results.Add((string)row.Cells["colName"].Value);
				}
				return results;
			}
		}

		public bool HasMultipleSelection
		{
			get
			{
				return (grd.SelectedRows.Count > 1);
			}
		}

		private void grdObjects_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.Value != null && e.Value.GetType() != typeof(DBNull) && e.ColumnIndex == 2)
			{
				Color gridBrushColor = ((DataGridView)sender).GridColor;
				Color bgColor = Color.WhiteSmoke;

				if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
				{
					bgColor = SystemColors.Highlight;
				}

				Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
						e.CellBounds.Y + 1, e.CellBounds.Width - 4,
						e.CellBounds.Height - 4);


				using (
						Brush gridBrush = new SolidBrush(gridBrushColor),
						backColorBrush = new SolidBrush(bgColor))
				{


					using (Pen gridLinePen = new Pen(gridBrush))
					{
						// Erase the cell.
						e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

						// Draw the grid lines (only the right and bottom lines;
						// DataGridView takes care of the others).
						e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
								e.CellBounds.Bottom, e.CellBounds.Right,
								e.CellBounds.Bottom);
						e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
								e.CellBounds.Top, e.CellBounds.Right - 1,
								e.CellBounds.Bottom);

						// Draw the text content of the cell, ignoring alignment.
						Brush br = null;
						if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected)
						{
							br = Brushes.Black;
						}
						else
						{
							br = SystemBrushes.HighlightText;
						}

						Image img = null;
						switch ((string)e.Value)
						{
							case "Db":
								img = imageList1.Images[0];
								break;
							case "Usr":
								img = imageList1.Images[1];
								break;
							case "Tbl":
								img = imageList1.Images[2];
								break;
							case "Vw":
								img = imageList1.Images[3];
								break;
							case "Pr":
								img = imageList1.Images[4];
								break;
							case "Fu":
								img = imageList1.Images[5];
								break;
							case "Col":
								img = imageList1.Images[6];
								break;
							case "Param":
								img = imageList1.Images[7];
								break;
							case "Tr":
								img = imageList1.Images[11];
								break;
							case "localVar":
								img = imageList1.Images[12];
								break;
							default:
								img = null;
								break;
						}

						if (img != null)
						{
							e.Graphics.DrawImageUnscaled(img, e.CellBounds.X + (e.CellBounds.Width - 16) / 2, e.CellBounds.Y + (e.CellBounds.Height - 16) / 2);
							e.Handled = true;
						}
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string filter = _bsObjects.Filter;
			string sort = _bsObjects.Sort;

			PopulateCodeCompletionList(true);

			_bsObjects.Filter = filter;
			_bsObjects.Sort = sort;

			if (_bsObjects.Count > 0)
			{
				_bsObjects.Position = 0;
			}

			SetFocusToGrid();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			PerformCustomFilter(txtCustomFilter.Text, true);
		}

		private void txtCustomFilter_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				PerformCustomFilter(txtCustomFilter.Text, true);
			}
			else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				grd.Focus();
			}
			else if (e.KeyCode == Keys.Escape && UserRequestedDismiss != null)
			{
				UserRequestedDismiss();
			}
		}

		public void ClearCustomFilter()
		{
			txtCustomFilter.Text = String.Empty;
		}

		public void SetFocusToGrid()
		{
			grd.Focus();
		}

		private void frmObjectSelector_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_conn == null)
				return;

			if (_conn.State == ConnectionState.Open)
				_conn.Close();
			_conn.Dispose();

		}

		private void txtCustomFilter_TextChanged(object sender, EventArgs e)
		{
			PerformCustomFilter(txtCustomFilter.Text, false);
		}

		private void grd_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}
	}

	public delegate void NotificationHandler();
	public delegate void KeyPressHandler(char character);


}