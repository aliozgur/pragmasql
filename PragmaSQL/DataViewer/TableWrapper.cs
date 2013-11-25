using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public class TableWrapper 
  {
		private string _owner;
		public string Owner
		{
			get { return _owner; }
			set { _owner = value; }
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private long _id;
		public long ID
		{
			get { return _id; }
			set { _id = value; }
		}

    private string _fileGroup;
    public string FileGroup
    {
      get { return _fileGroup; }
      set { _fileGroup = value; }
    }

    private IList<ColumnWrapper> _columns;
    public IList<ColumnWrapper> Columns
    {
      get
      {
        return _columns;
      }
    }

    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
        }
        else
        {
          _cp = value;
        }
      }
    }

		IDictionary<string, ColumnWrapper> _computedCols = new Dictionary<string, ColumnWrapper>();
		public IDictionary<string, ColumnWrapper> ComputedCols
		{
			get { return _computedCols; }
		}


		public string NormalizedName
		{
			get
			{
				return TableWrapper.ReplaceQuatations(_name);
			}
		}

		/// <summary>
		/// owner.name
		/// <para>Special characters removed</para>
		/// </summary>
		public string NormalizedFullName
		{
			get
			{
				return TableWrapper.ReplaceQuatations(_owner) + "." + TableWrapper.ReplaceQuatations(_name);
			}
		}

		public string FullName
		{
			get
			{
				return _owner + "." + _name;
			}
		}

    public TableWrapper( )
    {
      _columns = new List<ColumnWrapper>();
    }

    public TableWrapper( ConnectionParams cp )
      : this()
    {
      ConnectionParams = cp;
    }

    public TableWrapper( long id )
      : this()
    {
      this.ID = id;
    }

		public TableWrapper(string name)
			:this()
		{
			this.Name = name;
			LoadPropsUsingName();
		}

		public void LoadPropsUsingId( )
    {
      string cmdText = String.Empty;
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        cmdText = "declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME()\r\n";
        cmdText += "SELECT so.name, CASE WHEN @cmplevel < 90  THEN  USER_NAME(so.uid)  ELSE SCHEMA_NAME(so.uid) END 'owner' FROM sysobjects so WHERE so.id = " + ID.ToString();

        SqlDataReader reader = null;
        try
        {
					reader = TableWrapper.ExecuteReader(cmdText, conn);

          while (reader.Read())
          {
            this.Name = reader.GetString(0);
            this.Owner = reader.GetString(1);
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
        }

        try
        {
          cmdText = @" select distinct fg.groupname from  sysindexes i 
                      join sysobjects so on i.id = so.id
                      join sysfilegroups fg on i.groupid = fg.groupid
                      where so.id = " + ID.ToString();
          reader = TableWrapper.ExecuteReader(cmdText, conn);
          if (reader.HasRows)
          {
            reader.Read();
            this.FileGroup = reader.GetString(0);
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
        }
      }
    }

    public void LoadPropsUsingName( )
    {
      string cmdText = String.Empty;
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        cmdText = "SELECT so.name, t.TABLE_SCHEMA as 'owner' , so.id  FROM sysobjects so LEFT OUTER JOIN INFORMATION_SCHEMA.TABLES t on so.name = t.TABLE_NAME WHERE so.name = '" + TableWrapper.ReplaceQuatations(Name) + "'";

        SqlDataReader reader = null;
        try
        {
					reader = TableWrapper.ExecuteReader(cmdText, conn);

          while (reader.Read())
          {
            this.ID = reader.GetInt32(2);
						this.Owner = reader.GetString(1);
					}
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
        }
      }
    }


    public string GetCommaSeperatedColumnList(  )
    {
      string result = String.Empty;
      string comma = String.Empty;
      foreach (ColumnWrapper col in this.Columns)
      {
        if (col.IsComputed)
          continue;

        result += comma + col.Name;
        comma = ", ";
      }
      return result;
    }

    public string GetCommaSeperatedPKColumnList( )
    {
      string result = String.Empty;
      string comma = String.Empty;
      foreach (ColumnWrapper col in this.Columns)
      {
        if (!col.IsPrimaryKey)
          continue;

        result += comma + col.Name;
        comma = ", ";
      }
      return result;
    }

    public void LoadColumns( )
    {
      _columns.Clear();
			_computedCols.Clear();

      string cmdText = "";
      cmdText += "declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME()\r\n";
      cmdText += "SELECT DISTINCT dbo.syscolumns.name, dbo.systypes.name AS datatype, dbo.systypes.type,";
      cmdText += " case when LOWER(dbo.systypes.name) = 'nvarchar' or  LOWER(dbo.systypes.name) = 'nchar' THEN dbo.syscolumns.length / 2 ELSE dbo.syscolumns.length END 'length'";
      cmdText += ", dbo.syscolumns.prec";
      cmdText += ", dbo.syscolumns.scale, dbo.syscolumns.colid, CAST(dbo.syscolumns.isnullable as bit) AS isnull, COLUMNPROPERTY(dbo.syscolumns.id, dbo.syscolumns.name, 'IsIdentity') AS [identity], isNull(OBJECT_NAME(syscolumns.cdefault),'') AS defConstraint,";
      cmdText += " CASE WHEN @cmplevel < 90 THEN  CASE WHEN O.uid IS NULL THEN O.name  ELSE USER_NAME(O.uid) + '.' + O.name END";
      cmdText += "  ELSE CASE WHEN O.uid IS NULL THEN (O.name) ELSE (SCHEMA_NAME(O.uid)) + '.' + O.name END  END ruleBind";
      cmdText += ", O.Name as ruleBindName";
      cmdText += "        ,dbo.syscolumns.collation, dbo.syscolumns.collationid ";
      cmdText += "        ,CASE ISNULL(dbo.syscolumns.iscomputed,0) WHEN 1 THEN 1 ELSE 0 END iscomputed";
      cmdText += "        ,sc.text as formula";
      cmdText += "        ,IDENT_SEED('" + this.NormalizedFullName + "') as ident_seed,IDENT_INCR('" + this.NormalizedFullName + "') as ident_increment";
      cmdText += "        ,CASE WHEN ISNULL(sysindexkeys.id,-1) = -1 THEN CAST(0 as bit) ELSE CAST(1 as bit) END as IsInPK ";
      cmdText += " FROM        dbo.syscolumns";
      cmdText += "             LEFT OUTER JOIN dbo.systypes ON dbo.syscolumns.xusertype = dbo.systypes.xusertype";
      cmdText += "             LEFT OUTER JOIN dbo.sysindexes ON dbo.syscolumns.id = dbo.sysindexes.id AND (dbo.sysindexes.status & 0x800 = 0x800)";
      cmdText += "             LEFT OUTER JOIN dbo.sysindexkeys ON dbo.sysindexkeys.indid =  sysindexes.indid AND dbo.syscolumns.colid = dbo.sysindexkeys.colid AND dbo.syscolumns.id = dbo.sysindexkeys.id AND COL_NAME(sysindexkeys.id, sysindexkeys.colid) = dbo.syscolumns.name";
      cmdText += "             LEFT OUTER JOIN dbo.sysconstraints ON dbo.syscolumns.colid = dbo.sysconstraints.colid AND dbo.syscolumns.id = dbo.sysconstraints.id AND ( dbo.sysconstraints.status & 1 = 1)";
      cmdText += "             LEFT OUTER JOIN sysobjects O ON syscolumns.domain = O.id";
      cmdText += "             LEFT OUTER JOIN syscomments sc ON syscolumns.id = sc.id and syscolumns.colid = sc.number";
      cmdText += " WHERE       (dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedFullName + "'))";
      cmdText += " ORDER BY    dbo.syscolumns.colid";

      string cmdDesc = "select objtype, objname, name, value ";
      cmdDesc += "from ::fn_listextendedproperty(N'MS_Description', N'USER', N'dbo',N'TABLE', N'" + this.NormalizedName + "', N'COLUMN', NULL)";

      string cmdDefaults = @"  declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() 
                               SELECT 
                               CASE WHEN  @cmplevel < 90 
                                  THEN  CASE WHEN O.uid IS NULL 
                                          THEN (O.name)  
                                          ELSE (USER_NAME(O.uid)) + '.' + (O.name) 
                                        END  
                                  ELSE CASE WHEN O.uid IS NULL 
                                        THEN (O.name) 
                                        ELSE (SCHEMA_NAME(O.uid)) + '.' + (O.name) 
                                  END  
                              END 'objName'
                              , M.text 
                              , isnull( (SELECT constid FROM sysconstraints where constid = O.id), 0) as sysconst
                              ,O.name defName 
                              ,C.Name colName
                            FROM syscomments M,syscolumns C,sysobjects O 
                            WHERE C.id=OBJECT_ID('{0}') 
                            AND O.type='D' AND M.id=C.cdefault AND M.id=O.id";
      
      cmdDefaults = String.Format(cmdDefaults, this.NormalizedFullName);


      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
				DataTable columnsDataTable = TableWrapper.ExecuteDataTable(cmdText, conn);

				DataTable dscTbl = TableWrapper.ExecuteDataTable(cmdDesc, conn);
        DataColumn[] dscPkCols = new DataColumn[2];
        dscPkCols[0] = dscTbl.Columns["objname"];
        dscPkCols[1] = dscTbl.Columns["objtype"];
        dscTbl.PrimaryKey = dscPkCols;

				DataTable defTbl = TableWrapper.ExecuteDataTable(cmdDefaults, conn);
        DataColumn[] defPkCols = new DataColumn[1];
        defPkCols[0] = defTbl.Columns["colName"];
        defTbl.PrimaryKey = defPkCols;

        foreach (DataRow columnRow in columnsDataTable.Rows)
        {
          ColumnWrapper newColumn = new ColumnWrapper();
          newColumn.Name = DbNullSafeToString(columnRow["name"]);
					newColumn.DataType = DbNullSafeToString(columnRow["datatype"]);

					newColumn.Width = DbNullSafeToString(columnRow["length"]);
          newColumn.Scale = DbNullSafeToString(columnRow["scale"]);
					newColumn.IsIdentity = DbNullSafeToBool(columnRow["identity"]);
					newColumn.IsComputed = DbNullSafeToBool(columnRow["iscomputed"]);

					newColumn.Formula = DbNullSafeToString(columnRow["formula"]);

					newColumn.DefaultConstraint = DbNullSafeToString(columnRow["defConstraint"]);

					newColumn.AllowNulls = DbNullSafeToBool(columnRow["isnull"]);
          newColumn.ColId = DbNullSafeToString(columnRow["colid"]);
          newColumn.Precision = DbNullSafeToString(columnRow["prec"]);
          newColumn.RuleBinding = DbNullSafeToString(columnRow["ruleBindName"]);
          newColumn.Collation = DbNullSafeToString(columnRow["collation"]);
					newColumn.CollationId = TableWrapper.IsDbValueValid(columnRow["collationid"]) ? (int)columnRow["collationid"] : -1; ;

          if (newColumn.IsIdentity)
          {
						newColumn.Seed = ((int)(TableWrapper.IsDbValueValid(columnRow["ident_seed"]) ? (decimal)columnRow["ident_seed"] : 0)).ToString();
						newColumn.Increment = ((int)(TableWrapper.IsDbValueValid(columnRow["ident_increment"]) ? (decimal)columnRow["ident_increment"] : 0)).ToString();
          }


          //Primary key
					newColumn.IsPrimaryKey = TableWrapper.IsDbValueValid(columnRow["IsInPK"]) && (bool)columnRow["IsInPK"] == true;


          //Default
          DataRow defRow = defTbl.Rows.Find(newColumn.Name);
          if (defRow != null)
          {
						if (TableWrapper.IsDbValueValid(defRow["sysconst"]) && (int)defRow["sysconst"] == 0)
            {
              newColumn.Default = "";
              newColumn.DefaultBinding = TableWrapper.IsDbValueValid(defRow["defName"]) ? (string)defRow["defName"] : String.Empty;
            }
            else
            {
							newColumn.Default = TableWrapper.IsDbValueValid(defRow["text"]) ? ((string)defRow["text"]).Trim('(', ')') : String.Empty;
              newColumn.DefaultBinding = "";
            }
          }
          
					_columns.Add(newColumn);

					if (newColumn.IsComputed)
					{
						_computedCols.Add(newColumn.LowerName, newColumn);
					}
        }
      }
    }

    
   
    public DataTable GetConstraints( )
    {
      string cmdtext = " SELECT sysconstraints.constid as id, OBJECT_NAME(sysconstraints.constid) as ConstName, 'PK' as ConstType FROM sysconstraints";
      cmdtext += " WHERE sysconstraints.id=" + this.ID + " AND (sysconstraints.status & 0xf)=1";
      cmdtext += " UNION";

      cmdtext += " SELECT sysconstraints.constid as id, OBJECT_NAME(sysconstraints.constid) as ConstName, 'UK' as ConstType FROM sysconstraints";
      cmdtext += " WHERE sysconstraints.id=" + this.ID + " AND (sysconstraints.status & 0xf)=2";
      cmdtext += " UNION";

      cmdtext += " SELECT sysconstraints.constid as id, OBJECT_NAME(sysconstraints.constid) as ConstName, 'CC' as ConstType FROM sysconstraints";
      cmdtext += " WHERE sysconstraints.id=" + this.ID + " AND (sysconstraints.status & 0xf)=4";
      cmdtext += " UNION";
      cmdtext += " SELECT sysconstraints.constid as id, OBJECT_NAME(sysconstraints.constid) as ConstName, 'FK' as ConstType FROM sysconstraints";
      cmdtext += " WHERE sysconstraints.id=" + this.ID + " AND ((sysconstraints.status & 0x3)=0x3)";

			return TableWrapper.ExecuteDataTable(cmdtext, _cp);
    }

    public DataTable GetDependencies( )
    {
      string cmdtext = " SELECT      dbo.sysobjects.id as id, dbo.sysobjects.name AS name, dbo.sysobjects.xtype AS type, 'Ref' AS direction";
      cmdtext += " FROM        dbo.sysobjects INNER JOIN dbo.sysdepends ON dbo.sysobjects.id = dbo.sysdepends.id";
      cmdtext += " WHERE       (dbo.sysdepends.depid = OBJECT_ID('" + this.NormalizedFullName + "')) AND (dbo.sysobjects.type <> 'C')";

      cmdtext += " UNION";

      cmdtext += " SELECT      dbo.sysobjects.id as id, dbo.sysobjects.name AS name, 'U' AS type, 'Ref' AS direction";
      cmdtext += " FROM        dbo.sysobjects INNER JOIN dbo.sysreferences ON dbo.sysobjects.id = dbo.sysreferences.fkeyid";
      cmdtext += " WHERE       (dbo.sysreferences.rkeyid = OBJECT_ID('" + this.NormalizedFullName + "'))";

      cmdtext += " UNION";

      cmdtext += " SELECT      dbo.sysobjects.id as id, name AS name, 'TR' AS type, 'Ref' AS direction";
      cmdtext += " FROM        dbo.sysobjects";
      cmdtext += " WHERE       (type = 'TR') AND (parent_obj = OBJECT_ID('" + this.NormalizedFullName + "'))";

      cmdtext += " UNION";

      cmdtext += " SELECT      dbo.sysindexes.indid as id, name AS name, 'I' AS type, 'Ref' AS direction";
      cmdtext += " FROM        dbo.sysindexes";
      cmdtext += " WHERE       (id = OBJECT_ID('" + this.NormalizedFullName + "')) AND (indid > 0) ";
      cmdtext += "            AND (indid < 255) AND (status & 2048 <> 2048) AND (status & 4096 <> 4096) ";
      cmdtext += "            AND (INDEXPROPERTY(id, name, 'IsStatistics') <> 1) AND (INDEXPROPERTY(id, name, 'IsAutoStatistics') <> 1) ";
      cmdtext += "            AND (INDEXPROPERTY(id, name, 'IsHypothetical') <> 1)";

      cmdtext += " UNION";

      cmdtext += " SELECT      dbo.syscolumns.id as id, S.name AS name, 'D' AS type, 'Dep' AS direction";
      cmdtext += " FROM        dbo.syscolumns INNER JOIN";
      cmdtext += "            dbo.systypes S ON dbo.syscolumns.usertype = S.usertype INNER JOIN";
      cmdtext += "            dbo.systypes T ON S.type = T.type";
      cmdtext += " WHERE       (dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedFullName + "')) AND (dbo.syscolumns.usertype > 100) AND (T.usertype <> 18) AND (T.usertype < 100)";

      cmdtext += " UNION";

      cmdtext += " SELECT      dbo.sysobjects.id as id, OBJECT_NAME(dbo.sysreferences.rkeyid) AS name, 'U' AS type, 'Dep' AS direction";
      cmdtext += " FROM        dbo.sysreferences INNER JOIN dbo.sysobjects ON dbo.sysreferences.rkeyid = dbo.sysobjects.id";
      cmdtext += " WHERE       (dbo.sysreferences.fkeyid = OBJECT_ID('" + this.NormalizedFullName + "'))";

			return TableWrapper.ExecuteDataTable(cmdtext, _cp);
    }

		public ColumnWrapper GetIdentityColumn()
		{
			foreach (ColumnWrapper col in _columns)
			{
				if (col.IsIdentity)
					return col;
			}
			return null;
		}

		public bool HasIdentityColumn()
    {
      foreach (ColumnWrapper col in _columns)
      {
        if (col.IsIdentity)
          return true;
      }

      return false;
    }

		public bool HasComputedColumn()
		{
			return _computedCols.Count > 0;
		}

    public override string ToString( )
    {
      return this._id.ToString() + ":" + this._name;
    }

		#region Execution helper functions
		public static DataTable ExecuteDataTable(string cmdText, ConnectionParams cp)
		{
			if (cp == null)
			{
				throw new Exception("Connection parameters not specified!");
			}

			return ExecuteDataTable(cmdText, cp.CreateSqlConnection(true, false));
		}

		public static DataTable ExecuteDataTable(string cmdText, SqlConnection conn)
		{
			if (conn == null)
			{
				throw new Exception("Connection not specified!");
			}

			DataTable result = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter(cmdText, conn);
			adapter.Fill(result);
			return result;
		}

		public static SqlDataReader ExecuteReader(string cmdText, SqlConnection conn)
		{
			if (conn == null)
			{
				throw new Exception("Connection not specified!");
			}

			SqlCommand cmd = new SqlCommand(cmdText, conn);
			return cmd.ExecuteReader();
		}

		public static void ExecuteCommand(string cmdText, ConnectionParams cp)
		{
			if (cp == null)
			{
				throw new Exception("Connection parameters not specified!");
			}
			ExecuteCommand(cmdText, cp.CreateSqlConnection(true, false));
		}

		public static void ExecuteCommand(string cmdText, SqlConnection conn)
		{
			ExecuteCommand(cmdText, conn, null);
		}

		public static void ExecuteCommand(string cmdText, SqlConnection conn, SqlTransaction tran)
		{
			if (conn == null)
			{
				throw new Exception("Connection parameters not specified!");
			}

			SqlCommand cmd = new SqlCommand(cmdText, conn);
			if (tran != null)
				cmd.Transaction = tran;

			cmd.ExecuteNonQuery();
		}

		#endregion //Execution helper functions

		#region General utility functions
		public static string ReplaceQuatations(string value)
		{
			return value.Replace("'", "''");
		}

		public static string Qualify(string value)
		{
			return "[" + value + "]";
		}

		public static string GetFormattedNow()
		{
			DateTime t = DateTime.UtcNow;
			return
				t.Year.ToString("0000") + t.Month.ToString("00") + t.Day.ToString("00") + "_" + t.Hour.ToString("00") + t.Minute.ToString("00") + t.Second.ToString("00") + t.Millisecond.ToString("000");

		}

		public static bool IsDbValueValid(object value)
		{
			return (value != null && value.GetType() != typeof(DBNull));
		}

		public static bool IsRowItemValid(DataRow row, int index)
		{
			return !(row.ItemArray[index] == null || row.ItemArray[index].GetType() == typeof(DBNull));
		}

		public static bool IsRowItemValid(DataRow row, string colName)
		{
			return !(row[colName] == null || row[colName].GetType() == typeof(DBNull));
		}

		public static bool IsGridRowItemValid(DataGridViewRow row, int index)
		{
			return !(row.Cells[index].Value == null || row.Cells[index].Value.GetType() == typeof(DBNull));
		}

		public static bool IsReaderItemValid(SqlDataReader reader, string colName)
		{
			return !(reader[colName] == null || reader[colName].GetType() == typeof(DBNull));
		}

		public static bool DbNullSafeToBool(object value)
		{
			return !TableWrapper.IsDbValueValid(value) ? false : Convert.ToBoolean(value);
		}

		public static string DbNullSafeToString(object value)
		{
			return !TableWrapper.IsDbValueValid(value) ? String.Empty : value.ToString();
		}


		#endregion //General utility functions

	}

}
