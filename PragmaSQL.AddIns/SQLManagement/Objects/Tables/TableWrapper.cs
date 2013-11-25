using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using Microsoft.SqlServer.Management.Common;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public class TableWrapper : DatabaseObjectBase
  {
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


    private IDictionary<string, ColumnWrapper> _originalCols = new Dictionary<string, ColumnWrapper>();

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

    public void LoadProperties( )
    {
      string cmdText = String.Empty;
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        cmdText = "SELECT (so.name), (t.TABLE_SCHEMA) as 'owner' , so.id  FROM sysobjects so LEFT OUTER JOIN INFORMATION_SCHEMA.TABLES t on so.name = t.TABLE_NAME WHERE so.id = " + ID.ToString();

        SqlDataReader reader = null;
        try
        {
          reader = DbCmd.ExecuteReader(cmdText, conn);

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
          reader = DbCmd.ExecuteReader(cmdText, conn);
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

    public void RefreshTableID( )
    {
      string cmdText = String.Empty;
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {

        cmdText = "declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() ";
        cmdText += "SELECT name, CASE WHEN @cmplevel < 90  THEN  USER_NAME(uid)  ELSE SCHEMA_NAME(uid) END , id FROM sysobjects WHERE name = '" + Utils.ReplaceQuatations(Name) + "'";

        SqlDataReader reader = null;
        try
        {
          reader = DbCmd.ExecuteReader(cmdText, conn);

          while (reader.Read())
          {
            this.ID = reader.GetInt32(2);
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
        }
      }
    }

    public void LoadProperties( long objectID )
    {
      this.ID = objectID;
      LoadProperties();
    }

    public string GetCommaSeperatedColumnList( bool oldNames )
    {
      string result = String.Empty;
      string comma = String.Empty;
      foreach (ColumnWrapper col in this.Columns)
      {
        if (col.IsComputed)
          continue;

        result += comma + (oldNames ? col.OldName : col.Name);
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

    public DataTable GetColumnsNonNullableSimple( )
    {
      string cmdText = "SELECT dbo.syscolumns.name AS [column], dbo.systypes.name AS type";
      cmdText += " FROM dbo.syscolumns LEFT OUTER JOIN";
      cmdText += " dbo.systypes ON dbo.syscolumns.xusertype = dbo.systypes.xusertype";
      cmdText += " WHERE dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedFullName + "') AND (dbo.syscolumns.isnullable = 0)";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }

    public DataTable GetColumnsSimple( )
    {
      string cmdText = "SELECT dbo.syscolumns.name AS [column], dbo.systypes.name AS type";
      cmdText += " FROM dbo.syscolumns LEFT OUTER JOIN";
      cmdText += " dbo.systypes ON dbo.syscolumns.xusertype = dbo.systypes.xusertype";
      cmdText += " WHERE dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedFullName + "')";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }

    public DataTable GetConstraintForForeignKey( )
    {
      string cmdText = "SELECT name,status,indid FROM sysindexes WHERE id=OBJECT_ID('" + this.NormalizedFullName + "') AND (status & 2=2)";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }

    public DataTable GetConstraintForForeignKey( long tableID )
    {
      string cmdText = "SELECT name,status,indid FROM sysindexes WHERE id=" + tableID + " AND (status & 2=2)";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }

    public DataTable GetColumnsByForeignKey( long hostTableID, string strColId )
    {
      string cmdText = "SELECT COL_NAME(id, colid) as colName FROM sysindexkeys WHERE id=" + hostTableID + " AND indid=" + strColId + " ORDER BY keyno";
      return DbCmd.ExecuteDataTable(cmdText, _cp); ;
    }

    public DataTable GetColumnsByForeignKey( string strColId )
    {
      string cmdText = "SELECT COL_NAME(id, colid) as colName FROM sysindexkeys WHERE id=" + this.ID + " AND indid=" + strColId + " ORDER BY keyno";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }

    public DataTable GetColumnsChildTableByTypeParentTable( long childTableID, long parentTableID, string colName )
    {
      string cmdtext = "";
      cmdtext = cmdtext + " SELECT dbo.syscolumns.name";
      cmdtext = cmdtext + " FROM dbo.syscolumns, dbo.systypes";
      cmdtext = cmdtext + " WHERE dbo.syscolumns.xusertype = dbo.systypes.xusertype and (dbo.syscolumns.id = " + childTableID + ")";
      cmdtext = cmdtext + " AND dbo.systypes.name = (SELECT T.name FROM syscolumns C, systypes T WHERE C.id = " + parentTableID + " AND C.name = '" + colName + "' AND C.xusertype *= T.xusertype)";
      cmdtext = cmdtext + " ORDER BY dbo.syscolumns.colid";
      return DbCmd.ExecuteDataTable(cmdtext, _cp);
    }

    public string LoadDefConstraintOnCol( string columnName )
    {
      string cmdtext = "";
      cmdtext = cmdtext + "SELECT isNull(OBJECT_NAME(syscolumns.cdefault),'') AS defConstraint FROM dbo.syscolumns WHERE (dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedFullName + "')) and dbo.syscolumns.name = '" + columnName + "'";
      DataTable tbl = DbCmd.ExecuteDataTable(cmdtext, _cp);
      if (tbl == null || tbl.Rows.Count == 0)
        return String.Empty;
      else
        return tbl.Rows[0]["defConstraint"].ToString();
    }

    public void LoadColumns( )
    {
      _columns.Clear();
      _originalCols.Clear();

      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() ";
      cmdText += "SELECT DISTINCT dbo.syscolumns.name, CASE WHEN (CASE WHEN @cmplevel < 90  THEN  USER_NAME(dbo.systypes.uid)  ELSE SCHEMA_NAME(dbo.systypes.uid) END)  = 'sys'";
      cmdText += " THEN dbo.systypes.name ELSE (CASE WHEN @cmplevel < 90  THEN  USER_NAME(dbo.systypes.uid)  ELSE SCHEMA_NAME(dbo.systypes.uid) END) + '.' + (dbo.systypes.name) END AS datatype, dbo.systypes.type,";
      cmdText += " case when LOWER(dbo.systypes.name) = 'nvarchar' or  LOWER(dbo.systypes.name) = 'nchar' THEN dbo.syscolumns.length / 2 ELSE dbo.syscolumns.length END 'length'";
      cmdText += ", dbo.syscolumns.prec,";
      cmdText += "				dbo.syscolumns.scale, dbo.syscolumns.colid, CAST(dbo.syscolumns.isnullable as bit) AS isnull, COLUMNPROPERTY(dbo.syscolumns.id, dbo.syscolumns.name, 'IsIdentity') AS [identity], isNull(OBJECT_NAME(syscolumns.cdefault),'') AS defConstraint,";
      cmdText += " (CASE WHEN @cmplevel < 90  THEN  USER_NAME(O.uid)  ELSE SCHEMA_NAME(O.uid) END) + '.' + (O.name) as ruleBind, O.Name as ruleBindName";
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

      string cmdDefaults = @"declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() 
                             SELECT 
                              (CASE WHEN @cmplevel < 90  THEN  USER_NAME(O.uid)  ELSE SCHEMA_NAME(O.uid) END)+'.'+(O.name) 'objName'
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
        DataTable columnsDataTable = DbCmd.ExecuteDataTable(cmdText, conn);

        DataTable dscTbl = DbCmd.ExecuteDataTable(cmdDesc, conn);
        DataColumn[] dscPkCols = new DataColumn[2];
        dscPkCols[0] = dscTbl.Columns["objname"];
        dscPkCols[1] = dscTbl.Columns["objtype"];
        dscTbl.PrimaryKey = dscPkCols;

        DataTable defTbl = DbCmd.ExecuteDataTable(cmdDefaults, conn);
        DataColumn[] defPkCols = new DataColumn[1];
        defPkCols[0] = defTbl.Columns["colName"];
        defTbl.PrimaryKey = defPkCols;

        foreach (DataRow columnRow in columnsDataTable.Rows)
        {
          ColumnWrapper newColumn = new ColumnWrapper();
          newColumn.Name = columnRow["name"].ToString();
          newColumn.OldName = newColumn.Name;
          newColumn.DataType = columnRow["datatype"].ToString();
          newColumn.OldDataType = newColumn.DataType;

          newColumn.Width = columnRow["length"].ToString();
          newColumn.Scale = columnRow["scale"].ToString();
          newColumn.IsIdentity = Convert.ToBoolean(columnRow["identity"]);
          newColumn.OldIdentity = newColumn.IsIdentity;

          newColumn.IsComputed = Convert.ToBoolean(columnRow["iscomputed"]);
          newColumn.OldComputed = newColumn.IsComputed;

          newColumn.Formula = columnRow["formula"].ToString();

          newColumn.DefaultConstraint = columnRow["defConstraint"].ToString();

          newColumn.AllowNulls = Convert.ToBoolean(columnRow["isnull"]);
          newColumn.ColId = columnRow["colid"].ToString();
          newColumn.Precision = columnRow["prec"].ToString();
          newColumn.RuleBinding = columnRow["ruleBindName"].ToString();
          newColumn.Collation = columnRow["collation"].ToString();
          newColumn.CollationId = Utils.IsDbValueValid(columnRow["collationid"]) ? (int)columnRow["collationid"] : -1; ;

          if (newColumn.IsIdentity)
          {
            newColumn.Seed = ((int)(Utils.IsDbValueValid(columnRow["ident_seed"]) ? (decimal)columnRow["ident_seed"] : 0)).ToString();
            newColumn.Increment = ((int)(Utils.IsDbValueValid(columnRow["ident_increment"]) ? (decimal)columnRow["ident_increment"] : 0)).ToString();
          }


          //Primary key
          newColumn.IsPrimaryKey = Utils.IsDbValueValid(columnRow["IsInPK"]) && (bool)columnRow["IsInPK"] == true;


          //Descriptions
          DataRow descRow = dscTbl.Rows.Find(new object[2] { newColumn.Name, "COLUMN" });
          if (descRow != null)
          {
            newColumn.Description = Utils.IsDbValueValid(descRow["value"]) ? (string)descRow["value"] : String.Empty;
            newColumn.OldDescription = newColumn.Description;
            newColumn.HasDescriptionProperty = true;
          }



          //Default
          DataRow defRow = defTbl.Rows.Find(newColumn.Name);
          if (defRow != null)
          {
            if (Utils.IsDbValueValid(defRow["sysconst"]) && (int)defRow["sysconst"] == 0)
            {
              newColumn.Default = "";
              newColumn.DefaultBinding = Utils.IsDbValueValid(defRow["defName"]) ? (string)defRow["defName"] : String.Empty;
            }
            else
            {
              newColumn.Default = Utils.IsDbValueValid(defRow["text"]) ? ((string)defRow["text"]).Trim('(', ')') : String.Empty;
              newColumn.DefaultBinding = "";
            }
          }

          newColumn.CollectInitialValues();
          _columns.Add(newColumn);
          _originalCols.Add(newColumn.Name.ToLowerInvariant(), newColumn);
        }
      }
    }

    public void LoadColumns( Table tbl )
    {
      _columns.Clear();
      _originalCols.Clear();



      foreach (Column col in tbl.Columns)
      {
        ColumnWrapper newColumn = new ColumnWrapper();
        newColumn.Name = col.Name;
        newColumn.OldName = newColumn.Name;
        newColumn.DataType = col.DataType.Name;
        newColumn.OldDataType = newColumn.DataType;

        newColumn.Width = col.DataType.MaximumLength.ToString();
        newColumn.Scale = col.DataType.NumericScale.ToString();
        newColumn.Precision = col.DataType.NumericPrecision.ToString();

        newColumn.IsIdentity = col.Identity;
        newColumn.OldIdentity = newColumn.IsIdentity;

        newColumn.IsComputed = col.Computed;
        newColumn.OldComputed = newColumn.IsComputed;
        newColumn.Formula = col.ComputedText;

        newColumn.DefaultConstraint = col.DefaultConstraint != null ? col.DefaultConstraint.Name : String.Empty;

        newColumn.AllowNulls = col.Nullable;
        newColumn.ColId = col.ID.ToString();

        newColumn.RuleBinding = col.Rule;
        newColumn.Collation = col.Collation;
        newColumn.CollationId = -1;

        //Seed & increment
        newColumn.Seed = col.IdentitySeed.ToString();
        newColumn.Increment = col.IdentityIncrement.ToString();

        newColumn.IsPrimaryKey = col.InPrimaryKey;
        newColumn.CollectInitialValues();
        _columns.Add(newColumn);
        _originalCols.Add(newColumn.Name.ToLowerInvariant(), newColumn);
      }
    }

    public IList<ColumnWrapper> GetSimpleColumnList( )
    {

      IList<ColumnWrapper> result = new List<ColumnWrapper>();

      string cmdText = "";

      cmdText += "SELECT dbo.syscolumns.name";
      cmdText += " FROM        dbo.syscolumns";
      cmdText += " WHERE       dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedFullName + "')";
      cmdText += " ORDER BY    dbo.syscolumns.name";



      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        DataTable columnsDataTable = DbCmd.ExecuteDataTable(cmdText, conn);
        foreach (DataRow columnRow in columnsDataTable.Rows)
        {
          ColumnWrapper newColumn = new ColumnWrapper();
          newColumn.Name = columnRow["name"].ToString();
          result.Add(newColumn);
        }
      }

      return result;
    }

    public IList<ColumnWrapper> GetDetailedColumnList( )
    {
      IList<ColumnWrapper> result = new List<ColumnWrapper>();
      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() ";
      cmdText += "SELECT DISTINCT dbo.syscolumns.name,  CASE WHEN CASE WHEN @cmplevel < 90  THEN  USER_NAME(dbo.systypes.uid)  ELSE SCHEMA_NAME(dbo.systypes.uid) END  = 'sys'";
      cmdText += " THEN dbo.systypes.name ELSE (CASE WHEN CASE WHEN @cmplevel < 90  THEN  USER_NAME(dbo.systypes.uid)  ELSE SCHEMA_NAME(dbo.systypes.uid) END) + '.' + (dbo.systypes.name) END AS datatype, dbo.systypes.type,";
      cmdText += " case when LOWER(dbo.systypes.name) = 'nvarchar' or  LOWER(dbo.systypes.name) = 'nchar' THEN dbo.syscolumns.length / 2 ELSE dbo.syscolumns.length END 'length'";
      cmdText += ", dbo.syscolumns.prec,";
      cmdText += "				dbo.syscolumns.scale, dbo.syscolumns.colid, CAST(dbo.syscolumns.isnullable as bit) AS isnull, COLUMNPROPERTY(dbo.syscolumns.id, dbo.syscolumns.name, 'IsIdentity') AS [identity], isNull(OBJECT_NAME(syscolumns.cdefault),'') AS defConstraint, (CASE WHEN CASE WHEN @cmplevel < 90  THEN  USER_NAME(O.uid)  ELSE SCHEMA_NAME(O.uid) END) + '.' + (O.name) as ruleBind, O.Name as ruleBindName";
      cmdText += "        ,dbo.syscolumns.collation, dbo.syscolumns.collationid ";
      cmdText += "        ,CASE ISNULL(dbo.syscolumns.iscomputed,0) WHEN 1 THEN 1 ELSE 0 END iscomputed";
      cmdText += "        ,sc.text as formula";
      cmdText += "        ,IDENT_SEED('" + this.NormalizedFullName + "') as ident_seed,IDENT_INCR('" + this.NormalizedFullName + "') as ident_increment";
      cmdText += "        ,CASE WHEN ISNULL(sysindexkeys.id,-1) = -1 THEN CAST(0 as bit) ELSE CAST(1 as bit) END as IsInPK ";
      cmdText += " FROM        dbo.syscolumns";
      cmdText += "             LEFT OUTER JOIN dbo.systypes ON dbo.syscolumns.xusertype = dbo.systypes.xusertype";
      cmdText += "             LEFT OUTER JOIN dbo.sysindexkeys ON dbo.syscolumns.colid = dbo.sysindexkeys.colid AND dbo.syscolumns.id = dbo.sysindexkeys.id AND sysindexkeys.indid=1 AND COL_NAME(sysindexkeys.id, sysindexkeys.colid) = dbo.syscolumns.name";
      cmdText += "             LEFT OUTER JOIN dbo.sysconstraints ON dbo.syscolumns.colid = dbo.sysconstraints.colid AND dbo.syscolumns.id = dbo.sysconstraints.id";
      cmdText += "             LEFT OUTER JOIN sysobjects O ON syscolumns.domain = O.id";
      cmdText += "             LEFT OUTER JOIN syscomments sc ON syscolumns.id = sc.id and syscolumns.colid = sc.number";
      cmdText += " WHERE       (dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedFullName + "'))";
      cmdText += " ORDER BY    dbo.syscolumns.colid";

      string cmdDesc = "select objtype, objname, name, value ";
      cmdDesc += "from ::fn_listextendedproperty(N'MS_Description', N'USER', N'dbo',N'TABLE', N'" + this.NormalizedName + "', N'COLUMN', NULL)";

      string cmdDefaults = @" declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() 
                            SELECT 
                              ( CASE WHEN @cmplevel < 90  THEN  USER_NAME(O.uid)  ELSE SCHEMA_NAME(O.uid) END )+'.'+(O.name) 'objName'
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
        DataTable columnsDataTable = DbCmd.ExecuteDataTable(cmdText, conn);

        DataTable dscTbl = DbCmd.ExecuteDataTable(cmdDesc, conn);
        DataColumn[] dscPkCols = new DataColumn[2];
        dscPkCols[0] = dscTbl.Columns["objname"];
        dscPkCols[1] = dscTbl.Columns["objtype"];
        dscTbl.PrimaryKey = dscPkCols;

        DataTable defTbl = DbCmd.ExecuteDataTable(cmdDefaults, conn);
        DataColumn[] defPkCols = new DataColumn[1];
        defPkCols[0] = defTbl.Columns["colName"];
        defTbl.PrimaryKey = defPkCols;

        foreach (DataRow columnRow in columnsDataTable.Rows)
        {
          ColumnWrapper newColumn = new ColumnWrapper();
          newColumn.Name = columnRow["name"].ToString();
          newColumn.OldName = newColumn.Name;
          newColumn.DataType = columnRow["datatype"].ToString();
          newColumn.OldDataType = newColumn.DataType;

          newColumn.Width = columnRow["length"].ToString();
          newColumn.Scale = columnRow["scale"].ToString();
          newColumn.IsIdentity = Convert.ToBoolean(columnRow["identity"]);
          newColumn.OldIdentity = newColumn.IsIdentity;

          newColumn.IsComputed = Convert.ToBoolean(columnRow["iscomputed"]);
          newColumn.OldComputed = newColumn.IsComputed;

          newColumn.Formula = columnRow["formula"].ToString();

          newColumn.DefaultConstraint = columnRow["defConstraint"].ToString();

          newColumn.AllowNulls = Convert.ToBoolean(columnRow["isnull"]);
          newColumn.ColId = columnRow["colid"].ToString();
          newColumn.Precision = columnRow["prec"].ToString();
          newColumn.RuleBinding = columnRow["ruleBindName"].ToString();
          newColumn.Collation = columnRow["collation"].ToString();
          newColumn.CollationId = Utils.IsDbValueValid(columnRow["collationid"]) ? (int)columnRow["collationid"] : -1; ;

          if (newColumn.IsIdentity)
          {
            newColumn.Seed = ((int)(Utils.IsDbValueValid(columnRow["ident_seed"]) ? (decimal)columnRow["ident_seed"] : 0)).ToString();
            newColumn.Increment = ((int)(Utils.IsDbValueValid(columnRow["ident_increment"]) ? (decimal)columnRow["ident_increment"] : 0)).ToString();
          }


          //Primary key
          newColumn.IsPrimaryKey = Utils.IsDbValueValid(columnRow["IsInPK"]) && (bool)columnRow["IsInPK"] == true;

          //Descriptions
          DataRow descRow = dscTbl.Rows.Find(new object[2] { newColumn.Name, "COLUMN" });
          if (descRow != null)
          {
            newColumn.Description = Utils.IsDbValueValid(descRow["value"]) ? (string)descRow["value"] : String.Empty;
            newColumn.OldDescription = newColumn.Description;
            newColumn.HasDescriptionProperty = true;
          }



          //Default
          DataRow defRow = defTbl.Rows.Find(newColumn.Name);
          if (defRow != null)
          {
            if (Utils.IsDbValueValid(defRow["sysconst"]) && (int)defRow["sysconst"] == 0)
            {
              newColumn.Default = "";
              newColumn.DefaultBinding = Utils.IsDbValueValid(defRow["defName"]) ? (string)defRow["defName"] : String.Empty;
            }
            else
            {
              newColumn.Default = Utils.IsDbValueValid(defRow["text"]) ? ((string)defRow["text"]).Trim('(', ')') : String.Empty;
              newColumn.DefaultBinding = "";
            }
          }

          newColumn.CollectInitialValues();
          result.Add(newColumn);
        }
      }

      return result;
    }

    public void DropDefaultConstraint( string defaultConstraint )
    {
      string cmdtext = "ALTER TABLE " + this.QualifiedFullName + " DROP CONSTRAINT " + defaultConstraint;
      DbCmd.ExecuteCommand(cmdtext, _cp);
    }

    public void DropDefaultBinding( string colName )
    {
      string cmdtext = "EXEC sp_unbindefault '" + this.NormalizedFullName + "." + colName + "'";
      DbCmd.ExecuteCommand(cmdtext, _cp);
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

      return DbCmd.ExecuteDataTable(cmdtext, _cp);
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

      return DbCmd.ExecuteDataTable(cmdtext, _cp);
    }

    private void UpdateColumnDescription( ColumnWrapper col )
    {
      string cmdText = PrepareColumnDescriptionUpdateScript(col);
      DbCmd.ExecuteCommand(cmdText, _cp);
      col.HasDescriptionProperty = true;
    }

    private string PrepareColumnDescriptionUpdateScript( ColumnWrapper col )
    {
      return PrepareColumnDescriptionUpdateScript(col, !col.HasDescriptionProperty);
    }

    private string PrepareColumnDescriptionUpdateScript( ColumnWrapper col, bool addProp )
    {
      string spName = !addProp ? "sp_updateextendedproperty" : "sp_addextendedproperty";

      string cmdText = "DECLARE @v sql_variant";
      cmdText += " SET @v = N'" + col.Description + "'";
      cmdText += " EXECUTE " + spName + " N'MS_Description', @v "
        + ", N'USER', N'" + Owner + "'"
        + ", N'TABLE', N'" + Name + "'"
        + ", N'COLUMN', N'" + Utils.ReplaceQuatations(col.Name) + "'";

      return cmdText;
    }

    public void CreateTable( )
    {
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        ServerConnection srvConn = null;
        try
        {
          srvConn = new ServerConnection(conn);
          srvConn.BeginTransaction();
          CreateTable(srvConn, true, false);
          ResetColumnChanges();
          MakeOldColumnValuesActual();
          srvConn.CommitTransaction();
        }
        catch (Exception ex)
        {
          srvConn.RollBackTransaction();
          throw ex;
        }
        finally
        {
          srvConn.Disconnect();
        }
      }
    }

    public Table CreateTable( ServerConnection srvConn, bool autoCreatePk, bool skipDescriptionUpdate )
    {
      IList<Column> primaryKeyCols = new List<Column>();
      IDictionary<Column, ColumnWrapper> defaultBindings = new Dictionary<Column, ColumnWrapper>();
      IDictionary<Column, ColumnWrapper> ruleBindings = new Dictionary<Column, ColumnWrapper>();

      Table tbl = null;

      Server server = new Server(srvConn);
      Database db = server.Databases[_cp.Database];
      tbl = new Table(db, this.Name, this.Owner);

      Column newCol = null;
      DataType dType = null;
      foreach (ColumnWrapper col in _columns)
      {
        newCol = new Column(tbl, col.Name);
        // 1- Data Type specification

        dType = newCol.DataType;
        dType.SqlDataType = col.SqlDataType;
        if (Utils.IsUserDefinedType(col.DataType))
        {

          KeyValuePair<string, string> parsedDType = Utils.ParseUserDefinedDataTypeParts(col.DataType);
          dType.Schema = parsedDType.Key;
          dType.Name = parsedDType.Value;
        }

        if (col.IsComputed)
        {
          newCol.Computed = col.IsComputed;
          newCol.ComputedText = col.Formula;
        }
        else
        {

          if (Utils.CanHaveWidth(col.DataType) && !String.IsNullOrEmpty(col.Width))
          {
            dType.MaximumLength = Convert.ToInt32(col.Width);
          }
          else if (Utils.CanHavePrecAndScale(col.DataType) && (!String.IsNullOrEmpty(col.Precision) && !String.IsNullOrEmpty(col.Scale)))
          {
            dType.NumericPrecision = Convert.ToInt32(col.Precision);
            dType.NumericScale = Convert.ToInt32(col.Scale);
          }


          // 2- Collation
          if (Utils.SupportsCollation(col.DataType) && !String.IsNullOrEmpty(col.Collation))
          {
            newCol.Collation = col.Collation;
          }

          // 3- Identity, nullability and defaults
          if (col.IsIdentity)
          {
            newCol.Identity = col.IsIdentity;
            newCol.IdentitySeed = Convert.ToInt64(col.Seed);
            newCol.IdentityIncrement = Convert.ToInt64(col.Increment);
          }
          else
          {
            if (!String.IsNullOrEmpty(col.Default))
            {
              newCol.Default = col.Default;
            }
            newCol.Nullable = col.AllowNulls;
          }

          if (col.IsPrimaryKey)
            primaryKeyCols.Add(newCol);


          //4- Default and rule bindings
          if (!String.IsNullOrEmpty(col.Default))
          {
            newCol.Default = col.Default;
          }
          else
          {
            if (!String.IsNullOrEmpty(col.DefaultBinding))
            {
              defaultBindings.Add(newCol, col);
            }
          }

          if (!String.IsNullOrEmpty(col.RuleBinding))
          {
            ruleBindings.Add(newCol, col);
          }

        }

        tbl.Columns.Add(newCol);

      }

      //Define file group
      if (_fileGroup.ToLowerInvariant() != "default")
      {
        tbl.FileGroup = _fileGroup;
      }
      tbl.Create();



      // Create primary key
      if (autoCreatePk && primaryKeyCols.Count > 0)
      {
        Index idx = new Index(tbl, "PK_" + this.Name);
        idx.IndexKeyType = IndexKeyType.DriPrimaryKey;
        foreach (Column col in primaryKeyCols)
        {
          IndexedColumn idxc = new IndexedColumn(idx, col.Name, false);
          idx.IndexedColumns.Add(idxc);
        }

        idx.IsClustered = true;
        idx.IsUnique = true;
        idx.Create();
      }

      // Bind defaults
      ColumnWrapper colWrap = null;
      foreach (Column dbColumn in defaultBindings.Keys)
      {
        if (dbColumn.Identity)
          continue;
        colWrap = defaultBindings[dbColumn];
        dbColumn.BindDefault(String.Empty, colWrap.DefaultBinding);
      }

      //Bind rules
      foreach (Column dbColumn in ruleBindings.Keys)
      {
        colWrap = ruleBindings[dbColumn];
        dbColumn.BindRule(String.Empty, colWrap.RuleBinding);
      }

      if (!skipDescriptionUpdate)
        UpdateColumnDescriptions(srvConn);

      // Reset original column list
      _originalCols.Clear();
      foreach (ColumnWrapper col in _columns)
      {
        _originalCols.Add(col.Name.ToLowerInvariant(), col);
      }

      this.ID = tbl.ID;
      return tbl;
    }



    public bool AlterTable()
    {
      bool idChanged = false;

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        ServerConnection srvConn = null;
        try
        {
          srvConn = new ServerConnection(conn);

          bool recreateNeeded = RecreateTableNeeded();
          if (!recreateNeeded)
          {
            try
            {
              srvConn.BeginTransaction();
              AlterTableSimple(srvConn);
              idChanged = false;
              srvConn.CommitTransaction();
            }
            catch (Exception ex)
            {
              srvConn.RollBackTransaction();
              throw ex;
            }
          }
          else
          {
            try
            {
              RenameColumns(srvConn);
            }
            catch (Exception ex)
            {
              srvConn.RollBackTransaction();
              throw ex;
            }

            try
            {
              srvConn.BeginTransaction();
              RecreateTableAndMoveData(srvConn);
              idChanged = true;
              srvConn.CommitTransaction();
            }
            catch (Exception ex)
            {
              srvConn.RollBackTransaction();
              throw ex;
            }
          }

          ResetColumnChanges();
          MakeOldColumnValuesActual();

          return idChanged;
        }
        catch (Exception ex)
        {
          srvConn.RollBackTransaction();
          throw ex;
        }
        finally
        {
          srvConn.Disconnect();
        }
      }

    }



    public void AlterTableSimple( ServerConnection srvConn )
    {
      Column dbColumn = null;
      string name = String.Empty;
      string oldName = String.Empty;
      bool alterColAfterCreate = false;

      IList<string> actualCols = new List<string>();
      IList<Column> primaryKeyCols = new List<Column>();
      IDictionary<Column, ColumnWrapper> defaultBindings = new Dictionary<Column, ColumnWrapper>();
      IDictionary<Column, ColumnWrapper> ruleBindings = new Dictionary<Column, ColumnWrapper>();

      Table tbl = null;
      Server server = new Server(srvConn);
      Database db = server.Databases[_cp.Database];
      tbl = db.Tables[this.Name, this.Owner];
      tbl.Initialize();
      tbl.Columns.Refresh();

      foreach (ColumnWrapper col in _columns)
      {
        actualCols.Add(col.Name);

        name = col.Name.ToLowerInvariant();
        oldName = col.OldName.ToLowerInvariant();

        // If not changed and not a new column
        if (!col.Changed && (_originalCols.Keys.Contains(name) || _originalCols.Keys.Contains(oldName)))
          continue;


        // Already in original list so will modify
        if (_originalCols.Keys.Contains(name))
        {
          dbColumn = tbl.Columns[name];
          ApplyColumnPropertiesToDbColumn(false,dbColumn, col, primaryKeyCols, defaultBindings, ruleBindings);
          dbColumn.Alter();
        }
        else
        {
          // Check if old version is in list.
          if (_originalCols.Keys.Contains(oldName) && (!col.IsIdentity || !col.OldIdentity))
          {
            dbColumn = tbl.Columns[oldName];
            ApplyColumnPropertiesToDbColumn(false,dbColumn, col, primaryKeyCols, defaultBindings, ruleBindings);
            dbColumn.Rename(col.Name);
            dbColumn.Alter();
            _originalCols.Remove(oldName);
            _originalCols.Add(name.ToLowerInvariant(), col);

          }
          // This is a new column
          else
          {
            //Create column here
            dbColumn = new Column(tbl, col.Name);
            ApplyColumnPropertiesToDbColumn(true,dbColumn, col, primaryKeyCols, defaultBindings, ruleBindings);
            dbColumn.Create();


            alterColAfterCreate = false;
            /*
						if (!String.IsNullOrEmpty(col.Default))
            {
              dbColumn.Default = col.Default;
              alterColAfterCreate = true;
            }
						*/

            if (!col.AllowNulls)
            {
              dbColumn.Nullable = col.AllowNulls;
              alterColAfterCreate = true;
            }

            if (alterColAfterCreate)
              dbColumn.Alter();

            _originalCols.Add(name.ToLowerInvariant(), col);
          }
        }
      }

      // Alter primary key
      Index pkIndex = null;
      tbl.Indexes.Refresh();
      foreach (Index idx in tbl.Indexes)
      {
        if (idx.IndexKeyType == IndexKeyType.DriPrimaryKey)
        {
          pkIndex = idx;
          break;
        }
      }


      // Drop old pk and create new one if necessary
      Index newIdx = null;
      if (pkIndex == null && primaryKeyCols.Count > 0)
      {
        newIdx = CreateNewPkIndex(tbl);
      }
      else if (pkIndex != null)
      {
        newIdx = pkIndex;
        bool shallDropPk = false;
        //Check if there is any deleted column which is also in primary key
        foreach (IndexedColumn idxc in pkIndex.IndexedColumns)
        {
          if (!actualCols.Contains(idxc.Name))
          {
            shallDropPk = true;
            break;
          }
        }

        //We have to drop the primary key
        if (shallDropPk)
        {
          string fkList = String.Empty;
          string dropScript = SMOUtils.CreateDropScriptOfReferencingForeignKeys(srvConn, this.ID, out fkList);
          if (!String.IsNullOrEmpty(dropScript))
          {
            string msgStr = "Primary key \"" + pkIndex.Name + "\" is referenced by the following foreign keys.\r\n\r\n";
            msgStr += fkList + "\r\n\r\n";
            msgStr += "Foreign keys listed above have to be dropped as well.\r\n";
            msgStr += "Are you sure you want to modify the table?";

            if (!MessageService.AskQuestion(msgStr))
            {
              throw new CancelledByUserException("Operation cancelled by the user.");
            }
            else
            {
              srvConn.ExecuteNonQuery(dropScript);
            }
          }

          pkIndex.Drop();
          newIdx = CreateNewPkIndex(tbl);
        }
      }

      // Add columns to primary key
      if (primaryKeyCols.Count > 0)
      {
        bool shallCreatePk = false;
        foreach (Column col in primaryKeyCols)
        {
          if (!newIdx.IndexedColumns.Contains(col.Name))
          {
            IndexedColumn idxc = new IndexedColumn(newIdx, col.Name, false);
            newIdx.IndexedColumns.Add(idxc);
            shallCreatePk = true;
          }
        }

        if (shallCreatePk)
          newIdx.Create();
      }

      DropIndexes(ref dbColumn, actualCols, tbl);



      //Drop columns
      IList<string> delList = new List<string>();
      foreach (ColumnWrapper col in _originalCols.Values)
      {
        if (!actualCols.Contains(col.Name))
        {
          dbColumn = tbl.Columns[col.Name];
          if (!String.IsNullOrEmpty(dbColumn.Default))
            dbColumn.UnbindDefault();
          if (!String.IsNullOrEmpty(dbColumn.Rule))
            dbColumn.UnbindRule();

          dbColumn.Drop();
          delList.Add(col.Name);
        }
      }

      //Remove dropped columns from original column list
      foreach (string colName in delList)
      {
        if (_originalCols.ContainsKey(colName.ToLowerInvariant()))
        {
          _originalCols.Remove(colName);
        }
      }


      // Bind defaults
      ColumnWrapper colWrap = null;
      foreach (Column dbCol in defaultBindings.Keys)
      {
        if (dbColumn.Identity)
          continue;

        colWrap = defaultBindings[dbCol];

        if (String.IsNullOrEmpty(colWrap.DefaultBinding))
          dbColumn.UnbindDefault();
        else
          dbColumn.BindDefault(String.Empty, colWrap.DefaultBinding);

      }

      //Bind rules
      foreach (Column dbCol in ruleBindings.Keys)
      {
        colWrap = ruleBindings[dbCol];        
        if (!String.IsNullOrEmpty(colWrap.RuleBinding))
          dbColumn.BindRule(String.Empty, colWrap.RuleBinding);
        else
          dbColumn.UnbindRule();

      }

      //Update column descriptions
      foreach (ColumnWrapper col in _columns)
      {
        if (col.Description != col.OldDescription)
        {
          string cmdText = PrepareColumnDescriptionUpdateScript(col);
          srvConn.ExecuteNonQuery(cmdText);
          col.HasDescriptionProperty = true;
          col.OldDescription = col.Description;
        }
      }

      /*
      if (this.FileGroup.ToLowerInvariant() != tbl.FileGroup.ToLowerInvariant())
      {
        tbl.FileGroup = this.FileGroup;
        tbl.Alter();
      }
      */
    }

    private void DropIndexes( ref Column dbColumn, IList<string> actualCols, Table tbl )
    {
      // Check if dropped columns has any index defined on and notify user
      string dropIndexNotification = "Columns listed below are referenced by some indexes.\r\n";
      string tmpIndexNotification = String.Empty;

      IList<Index> _colIndexDrop = new List<Index>();
      foreach (ColumnWrapper col in _originalCols.Values)
      {
        if (!actualCols.Contains(col.Name))
        {
          tmpIndexNotification = String.Empty;
          dbColumn = tbl.Columns[col.Name];
          foreach (Index i in tbl.Indexes)
          {
            if (i.IndexKeyType == IndexKeyType.DriPrimaryKey)
              continue;

            foreach (IndexedColumn ic in i.IndexedColumns)
            {
              if (ic.Name.ToLowerInvariant() == dbColumn.Name.ToLowerInvariant())
              {
                _colIndexDrop.Add(i);
                tmpIndexNotification += (String.IsNullOrEmpty(tmpIndexNotification) ? "\r\nColumn: " + col.Name + "\r\n Index:" + i.Name : "\r\n Index:" + i.Name);
                break;
              }
            }
          }

          if (!String.IsNullOrEmpty(tmpIndexNotification))
            dropIndexNotification += tmpIndexNotification;
        }
      }

      dropIndexNotification += "\r\n\r\n" + "Are you sure you want to drop listed columns above?";
      if (_colIndexDrop.Count > 0)
      {
        if (!MessageService.AskQuestion(dropIndexNotification))
          throw new CancelledByUserException("Operation cancelled by the user!");
      }

      foreach (Index i in _colIndexDrop)
      {
        i.Drop();
      }
      _colIndexDrop.Clear();
      _colIndexDrop = null;
    }

    private void RecreateTableAndMoveData( ServerConnection srvConn )
    {
      RenameColumns(srvConn);

      TableWrapper tmpTbl = this.CreateCopy();

      string tmpTableName = Guid.NewGuid().ToString();
      tmpTableName = tmpTableName.Replace("-", String.Empty);

      tmpTbl.Name = tmpTableName;
      tmpTbl.ID = 0;

      Table tbl = null;


      string pkScript = SMOUtils.ScriptPrimaryKey(srvConn, this.Name);
      string fkScript = SMOUtils.ScriptForeignKeys(srvConn, this.Name);
      string indexScript = SMOUtils.ScriptIndexes(srvConn, this.Name);


      //Create temp table
      tbl = tmpTbl.CreateTable(srvConn, false, true);

      //Check identity insert
      bool hasIdentity = tmpTbl.HasIdentityColumn();
      string idInsertSwitch = hasIdentity ? "SET IDENTITY_INSERT " + tmpTbl.QualifiedFullName : String.Empty;
      StringBuilder sb = new StringBuilder();

      //Move data from original table to temp table
      sb.AppendLine(!String.IsNullOrEmpty(idInsertSwitch) ? idInsertSwitch + " ON" : String.Empty);
      sb.AppendLine(" IF EXISTS(SELECT * FROM " + this.QualifiedFullName + ")");
      sb.AppendLine(" EXEC('INSERT INTO " + tmpTbl.QualifiedFullName + "(" + tmpTbl.GetCommaSeperatedColumnList(false) + ")");
      sb.AppendLine(" SELECT " + this.GetCommaSeperatedColumnList(true) + " FROM " + this.QualifiedFullName + " WITH (HOLDLOCK TABLOCKX)')");
      sb.AppendLine(!String.IsNullOrEmpty(idInsertSwitch) ? idInsertSwitch + " OFF" : String.Empty);

      //Script triggers
      string triggerScripts = SMOUtils.GetTriggerScripts(srvConn, this.Name);

      //Script referencing foreign keys
      string refFkScript = SMOUtils.ScriptReferencingForeignKeys(srvConn, this.ID);


      //5- Drop original table
      string fkList = String.Empty;
      string refFkDropScript = SMOUtils.CreateDropScriptOfReferencingForeignKeys(srvConn, this.ID, out fkList);
      sb.AppendLine(refFkDropScript);
      sb.AppendLine("DROP TABLE " + this.QualifiedFullName);

      //Rename temp table to old table
      sb.AppendLine("EXECUTE sp_rename N'" + tmpTbl.NormalizedFullName + "', N'" + this.NormalizedName + "', 'OBJECT'");

      //Create foreign and primary keys
      sb.AppendLine(pkScript);
      sb.AppendLine(fkScript);

      if (!String.IsNullOrEmpty(indexScript))
        sb.AppendLine("SET ARITHABORT ON");
      sb.AppendLine(indexScript);

      //Recreate foreign keys referencing the table
      sb.AppendLine(refFkScript);
      

      srvConn.ExecuteNonQuery(sb.ToString());
      srvConn.ExecuteNonQuery(triggerScripts);
      UpdateColumnDescriptions(srvConn);

      this.ID = tmpTbl.ID;

    }

    private void RenameColumns(ServerConnection srvConn)
    {
      Server srv = new Server(srvConn);
      Database db = srv.Databases[srvConn.DatabaseName];
      Table tbl = db.Tables[this.Name];
      Column col = null;
      foreach (ColumnWrapper colWrap in _columns)
      {
        if (colWrap.Name != colWrap.OldName)
        {
          col = tbl.Columns[colWrap.OldName];

          // EKLENECEK!!!
          if (col == null)
            continue;

          col.Rename(colWrap.Name);
          colWrap.OldName = colWrap.Name;
        }
      }
    }


    private bool HasIdentityColumn( )
    {
      foreach (ColumnWrapper col in _columns)
      {
        if (col.IsIdentity)
          return true;
      }

      return false;
    }



    private Index CreateNewPkIndex( Table tbl )
    {
      Index idx = new Index(tbl, "PKI_" + this.Name);
      idx.IndexKeyType = IndexKeyType.DriPrimaryKey;
      idx.IsClustered = true;
      idx.IsUnique = true;
      return idx;
    }

    public void ApplyColumnPropertiesToDbColumn(bool isNewCol, Column dbCol, ColumnWrapper col,
      IList<Column> pkList, IDictionary<Column, ColumnWrapper> defBindingList, IDictionary<Column, ColumnWrapper> ruleBindingList )
    {
      DataType dType = null;

      dType = dbCol.DataType;
      dType.SqlDataType = col.SqlDataType;
      if (Utils.IsUserDefinedType(col.DataType))
      {

        KeyValuePair<string, string> parsedDType = Utils.ParseUserDefinedDataTypeParts(col.DataType);
        //dType = DataType.UserDefinedDataType(parsedDType.Value, parsedDType.Key);
        dType.Schema = parsedDType.Key;
        dType.Name = parsedDType.Value;
      }


      if (col.IsComputed)
      {
        dbCol.Computed = col.IsComputed;
        dbCol.ComputedText = col.Formula;
      }
      else
      {
        
        if (Utils.CanHaveWidth(col.DataType) && !String.IsNullOrEmpty(col.Width))
        {
          dType.MaximumLength = Convert.ToInt32(col.Width);
        }
        else if (Utils.CanHavePrecAndScale(col.DataType) && (!String.IsNullOrEmpty(col.Precision) && !String.IsNullOrEmpty(col.Scale)))
        {
          dType.NumericPrecision = Convert.ToInt32(col.Precision);
          dType.NumericScale = Convert.ToInt32(col.Scale);
        }


        // 2- Collation
        if (Utils.SupportsCollation(col.DataType) && !String.IsNullOrEmpty(col.Collation))
        {
          dbCol.Collation = col.Collation;
        }

        // 3- Identity, nullability and defaults
        if (col.IsIdentity)
        {
          dbCol.Identity = col.IsIdentity;
          dbCol.IdentitySeed = Convert.ToInt64(col.Seed);
          dbCol.IdentityIncrement = Convert.ToInt64(col.Increment);
        }

        if (col.IsPrimaryKey && pkList != null)
          pkList.Add(dbCol);

        dbCol.Nullable = col.AllowNulls;

        //4- Default and rule bindings
				bool defaultUnbound = false;

        if (col.PropertyChanged("Default"))
        {
					if (dbCol.DefaultConstraint != null)
            dbCol.DefaultConstraint.Drop();

					if (!String.IsNullOrEmpty(col.Default) )
          {
            dbCol.AddDefaultConstraint();
            dbCol.DefaultConstraint.Text = col.Default;

						if (!String.IsNullOrEmpty(dbCol.Default))
						{
							dbCol.UnbindDefault();
							defaultUnbound = true;
						}

						if (!isNewCol)
						{
							dbCol.DefaultConstraint.Create();
						}
          }
        }
       
        if (col.PropertyChanged("DefaultBinding") && defBindingList != null)
        {
					if (!defaultUnbound)
						defBindingList.Add(dbCol, col);
        }

        if (col.PropertyChanged("RuleBinding") && ruleBindingList != null)
        {
          ruleBindingList.Add(dbCol, col);
        }
      }

    }

    public void Rename( string newName )
    {
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        ServerConnection srvConn = null;
        Table tbl = null;
        try
        {
          srvConn = new ServerConnection(conn);
          srvConn.BeginTransaction();
          Server server = new Server(srvConn);
          Database db = server.Databases[_cp.Database];
          tbl = db.Tables[this.Name, this.Owner];
          if (tbl == null)
            throw new Exception("Table can not be found on database\r\n Table:+"+this.Name);
          tbl.Initialize();
          tbl.Rename(newName);
          this.Name = Utils.Qualify(newName);
          srvConn.CommitTransaction();
        }
        catch (Exception ex)
        {
          srvConn.RollBackTransaction();
          throw ex;
        }
      }
    }


    public override string ToString( )
    {
      return this.Name;
    }

    private void ResetColumnChanges( )
    {
      foreach (ColumnWrapper colWrap in _columns)
      {
        colWrap.ResetChanges();
      }
    }

    private void MakeOldColumnValuesActual( )
    {
      foreach (ColumnWrapper colWrap in _columns)
      {
        colWrap.MakeOldValuesActual();
      }
    }

    private bool WideCharacterDataTypeDetected( )
    {
      bool conversionNeeded = false;

      string oldType = String.Empty;
      string currentType = String.Empty;

      foreach (ColumnWrapper col in _columns)
      {
        if (!col.Changed)
          continue;

        oldType = col.OldDataType.ToLowerInvariant();
        currentType = col.DataType.ToLowerInvariant();

        if (oldType == currentType)
          continue;

        conversionNeeded = (currentType == "nchar" || currentType == "nvarchar");
        if (conversionNeeded)
          break;
      }

      return conversionNeeded;
    }


    private bool DataTypeConversionNeeded( )
    {
      bool conversionNeeded = false;

      string oldType = String.Empty;
      string currentType = String.Empty;

      foreach (ColumnWrapper col in _columns)
      {
        if (!col.Changed)
          continue;

        oldType = col.OldDataType.ToLowerInvariant();
        currentType = col.DataType.ToLowerInvariant();

        if (oldType == currentType)
          continue;

        conversionNeeded = (!conversionNeeded)
                          && ((oldType == "text" || oldType == "ntext")
                                &&
                                (
                                  currentType == "text" || currentType == "ntext"
                                  || currentType == "char" || currentType == "nchar"
                                  || currentType == "varchar" || currentType == "nvarchar"
                                )
                             );
        if (conversionNeeded)
          break;
      }

      return conversionNeeded;
    }

   
    private bool RecreateTableNeeded( )
    {
      bool hasIdentityCol = false;
      bool hasComputedCol = false;
      string oldType = String.Empty;
      string currentType = String.Empty;
      bool conversionNeeded = false;
      bool result = false;

      foreach (ColumnWrapper col in _columns)
      {

        // Identity specification changed
        if (col.Changed && (col.IsIdentity || col.OldIdentity))
        {
          hasIdentityCol = true;
          break;
        }

        if (col.Changed && (col.IsComputed != col.OldComputed))
        {
          hasComputedCol = true;
          break;
        }

        // Primary key data type changed
        if (col.PropertyChanged("DataType") && col.IsPrimaryKey)
        {
          result = true;
          break;
        }

        // Conversion needed for text data type
        if (oldType != currentType)
        {

          conversionNeeded = (!conversionNeeded)
                            && ((oldType == "text" || oldType == "ntext")
                                  &&
                                  (
                                    currentType == "text" || currentType == "ntext"
                                    || currentType == "char" || currentType == "nchar"
                                    || currentType == "varchar" || currentType == "nvarchar"
                                  )
                               );
          if (conversionNeeded)
          {
            result = true;
            break;
          }

          //Conversion needed for wide text data types
          conversionNeeded = (currentType == "nchar" || currentType == "nvarchar");
          if (conversionNeeded)
          {
            result = true;
            break;
          }
        }

      }
      return (result | hasIdentityCol | hasComputedCol);
    }

    private TableWrapper CreateCopy( )
    {
      TableWrapper result = new TableWrapper(_cp);
      result._columns = this._columns;
      result._fileGroup = this._fileGroup;
      result._originalCols = this._originalCols;
      result.Name = this.Name;
      result.Owner = this.Owner;
      result.ID = this.ID;
      return result;
    }

    private void UpdateColumnDescriptions( ServerConnection srvConn )
    {
      //Update column descriptions
      foreach (ColumnWrapper col in _columns)
      {
        if (col.Description != col.OldDescription)
        {
          string cmdText = PrepareColumnDescriptionUpdateScript(col);
          srvConn.ExecuteNonQuery(cmdText);
          col.HasDescriptionProperty = true;
          col.OldDescription = col.Description;
        }
      }
    }

  }

}
