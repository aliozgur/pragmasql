using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using PragmaSQL.Core;

namespace SQLManagement
{
  public class ForeignKeyWrapper : DatabaseObjectBase
  {
    #region Fields An Properties
    private TableWrapper _hostTable;
    public TableWrapper HostTable
    {
      get { return _hostTable; }
      set { _hostTable = value; }
    }

    private TableWrapper _refTable;
    public TableWrapper RefTable
    {
      get { return _refTable; }
      set { _refTable = value; }
    }

    private bool _cascadeDelete = false;
    public bool CascadeDelete
    {
      get { return _cascadeDelete; }
      set { _cascadeDelete = value; }
    }

    private bool _cascadeUpdate = false;
    public bool CascadeUpdate
    {
      get { return _cascadeUpdate; }
      set { _cascadeUpdate = value; }
    }

    private bool _notForReplication = false;
    public bool NotForReplication
    {
      get { return _notForReplication; }
      set { _notForReplication = value; }
    }


    private string _colsHost = String.Empty;
    public string ColsHost
    {
      get { return _colsHost; }
      set { _colsHost = value; }
    }


    private string _colsRef = String.Empty;
    public string ColsRef
    {
      get { return _colsRef; }
      set { _colsRef = value; }
    }


    private bool _disabled = false;
    public bool Disabled
    {
      get { return _disabled; }
      set { _disabled = value; }
    }


    private NameIdPair _referencedKey = new NameIdPair();
    public NameIdPair ReferencedKey
    {
      get { return _referencedKey; }
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

    private bool _allPropsLoaded = false;
    public bool AllPropsLoaded
    {
      get { return _allPropsLoaded; }
      set { _allPropsLoaded = value; }
    }

    #endregion //Fields An Properties

    public ForeignKeyWrapper( ConnectionParams cp )
    {
      ConnectionParams = cp;
      _hostTable = new TableWrapper(cp);
      _refTable = new TableWrapper(cp);

    }

    public ForeignKeyWrapper( )
    {
      _hostTable = new TableWrapper();
      _refTable = new TableWrapper();
    }


    public void LoadBasicProperties( )
    {
      string cmdText = " SELECT  dbo.sysobjects.name, dbo.sysforeignkeys.fkeyid, dbo.sysforeignkeys.rkeyid, dbo.sysobjects.id ";
      cmdText += " FROM dbo.sysobjects INNER JOIN dbo.sysforeignkeys ON dbo.sysobjects.id = dbo.sysforeignkeys.constid";
      cmdText += " WHERE (dbo.sysobjects.id = " + ID.ToString() + ")";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            this.Name = reader.GetString(0);
          }
        }
        finally
        {
          reader.Close();
        }
      }
    }

    public void LoadBasicPropertiesByName( )
    {
      string cmdText = " SELECT  dbo.sysobjects.name,dbo.sysobjects.id, dbo.sysforeignkeys.fkeyid, dbo.sysforeignkeys.rkeyid";
      cmdText += " FROM dbo.sysobjects INNER JOIN dbo.sysforeignkeys ON dbo.sysobjects.id = dbo.sysforeignkeys.constid";
      cmdText += " WHERE dbo.sysobjects.name = '" + this.NormalizedName + "'";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            this.Name = reader.GetString(0);
            this.ID = reader.GetInt32(1);
          }
        }
        finally
        {
          reader.Close();
        }
      }
    }

    public void LoadAllProperties( )
    {
      LoadProperties();
      GetKey();
      _allPropsLoaded = true;
    }

    public void LoadProperties( )
    {
      string cmdText = " SELECT  dbo.sysobjects.name, dbo.sysforeignkeys.fkeyid, dbo.sysforeignkeys.rkeyid, dbo.sysobjects.id ";
      cmdText += " FROM dbo.sysobjects INNER JOIN dbo.sysforeignkeys ON dbo.sysobjects.id = dbo.sysforeignkeys.constid";
      cmdText += " WHERE (dbo.sysobjects.id = " + ID.ToString() + ")";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            this.Name = reader.GetString(0);
            this.HostTable.ID = Convert.ToInt64(reader.GetInt32(1));
            this.HostTable.LoadProperties();
            this.HostTable.LoadColumns();

            this.RefTable.ID = Convert.ToInt64(reader.GetInt32(2));
            this.RefTable.LoadProperties();
            this.RefTable.LoadColumns();
          }
        }
        finally
        {
          reader.Close();
        }
      }
    }

    public void GetKey( )
    {
      //Set the key Option			
      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";  
      cmdText += "SELECT ";
      cmdText += " ObjectProperty(" + ID.ToString() + " , 'CnstIsDisabled') as IsDisabled,";
      cmdText += " ObjectProperty(" + ID.ToString() + " , 'CnstIsNotRepl') as IsNotRepl,";
      cmdText += " ObjectProperty(" + ID.ToString() + " , 'CnstIsUpdateCascade') as IsUpdateCascade,";
      cmdText += " ObjectProperty(" + ID.ToString() + " , 'CnstIsDeleteCascade') as IsDeleteCascade,";
      cmdText += " dbo.sysreferences.constid, CASE WHEN @cmplevel < 90  THEN  USER_NAME(hostTable.uid)  ELSE SCHEMA_NAME(hostTable.uid) END   AS unamehost, hostTable.name AS host, hostTable.id as hostID, refTable.id as refID, CASE WHEN @cmplevel < 90  THEN  USER_NAME(refTable.uid)  ELSE SCHEMA_NAME(refTable.uid) END   AS unameref, refTable.name AS ref";
      cmdText += " FROM dbo.sysreferences INNER JOIN dbo.sysobjects hostTable ON dbo.sysreferences.fkeyid = hostTable.id INNER JOIN dbo.sysobjects refTable ON dbo.sysreferences.rkeyid = refTable.id";
      cmdText += " WHERE (dbo.sysreferences.constid = " + ID.ToString() + ")";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {

            _notForReplication = Convert.ToBoolean(reader["IsNotRepl"]);
            _disabled = Convert.ToBoolean(reader["IsDisabled"]);
            _cascadeUpdate = Convert.ToBoolean(reader["IsUpdateCascade"]);
            _cascadeDelete = Convert.ToBoolean(reader["IsDeleteCascade"]);

            _hostTable.ID = Convert.ToInt64(reader["hostID"]);
            _hostTable.LoadProperties();
            _hostTable.LoadColumns();

            _refTable.ID = Convert.ToInt64(reader["refID"]);
            _refTable.LoadProperties();
            _refTable.LoadColumns();
          }
        }
        finally
        {
          reader.Close();
          reader.Dispose();
        }

        //Key referenced
        cmdText = "SELECT sysindexes.indid, sysindexes.name as keyName"
          + " FROM sysindexes, sysreferences "
          + " WHERE sysreferences.rkeyid=sysindexes.id AND sysreferences.rkeyindid=sysindexes.indid "
          + " AND sysreferences.rkeyid=" + _refTable.ID
          + " AND OBJECT_NAME(sysreferences.constid)='" + this.NormalizedName + "'";

        SqlDataReader readerKey = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (readerKey.Read())
          {
            _referencedKey.Id = (short)readerKey["indid"];
            _referencedKey.Name = (string)readerKey["keyName"];

          }
        }
        finally
        {
          readerKey.Close();
          readerKey.Dispose();
        }
      }
    }


    public string GetColumns( string columnName )
    {
      string strColName = "";
      string cmdText = "";
      cmdText += " SELECT COL_NAME(fkeyid,fkey) as columnName";
      cmdText += " FROM sysforeignkeys";
      cmdText += " WHERE constid = " + ID.ToString() + " AND COL_NAME(rkeyid,rkey) = '" + columnName + "'  ORDER BY keyno";
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader readerKey = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (readerKey.Read())
          {
            strColName = readerKey["columnName"].ToString();
          }
        }
        finally
        {
          readerKey.Close();
          readerKey.Dispose();
        }
      }
      return strColName;
    }

    public string GetCreateScript( )
    {

      string cmdText = "ALTER TABLE " + _hostTable.QualifiedFullName + " ADD ";

      if (!String.IsNullOrEmpty(Name))
      {
        cmdText += "CONSTRAINT " + this.QualifiedName;
      }

      cmdText += " FOREIGN KEY (" + _colsHost + ")";
      cmdText += " REFERENCES " + _refTable.QualifiedFullName + " (" + _colsRef + ")";

      if (_cascadeDelete)
      {
        cmdText += " ON DELETE CASCADE";
      }

      if (_cascadeUpdate)
      {
        cmdText += " ON UPDATE CASCADE";
      }

      if (_notForReplication)
      {
        cmdText += " NOT FOR REPLICATION";
      }
      return cmdText;
    }

    public void Create( )
    {
      DbCmd.ExecuteCommand(GetCreateScript(), _cp);
    }


    public string GetDropScript( )
    {
      return "ALTER TABLE " + _hostTable.QualifiedFullName + " DROP CONSTRAINT " + this.QualifiedName;
    }

    public void Drop( )
    {
      DbCmd.ExecuteCommand(GetDropScript(), _cp);
    }

    public string GetRenameScript( string newName )
    {
      return "EXEC sp_rename '" + this.NormalizedName + "','" + Utils.ReplaceQuatations(newName) + "'";
    }

    public void Rename( string newName )
    {
      string tmpName = this.Name;
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlTransaction tran = null;
        try
        {
          tran = conn.BeginTransaction();
          DbCmd.ExecuteCommand(GetDropScript(), conn, tran);
          this.Name = newName;
          DbCmd.ExecuteCommand(GetCreateScript(), conn, tran);
          tran.Commit();
        }
        catch (Exception ex)
        {
          this.Name = tmpName;
          if (tran != null)
            tran.Rollback();
          throw ex;
        }
      }
      LoadBasicPropertiesByName();
    }

    public void DropAndRecreateKey( )
    {
      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlTransaction tran = null;
        try
        {
          tran = conn.BeginTransaction();
          DbCmd.ExecuteCommand(GetDropScript(), conn, tran);
          DbCmd.ExecuteCommand(GetCreateScript(), conn, tran);
          tran.Commit();
          LoadBasicPropertiesByName();
        }
        catch (Exception ex)
        {
          if (tran != null)
            tran.Rollback();
          throw ex;
        }
      }
    }

    public string GetDisableScript( )
    {
      return "ALTER TABLE " + _hostTable.QualifiedFullName + " NOCHECK CONSTRAINT " + this.QualifiedName;
    }

    public void Disable( )
    {
      DbCmd.ExecuteCommand(GetDisableScript(), _cp);
      Disabled = true;
    }

    public string GetEnableScript( )
    {
      return "ALTER TABLE " + _hostTable.QualifiedFullName + " CHECK CONSTRAINT " + this.QualifiedName;
    }

    public void Enable( )
    {
      DbCmd.ExecuteCommand(GetEnableScript(), _cp);
      Disabled = false;
    }

    public static void Drop( ConnectionParams cp, string hostTableName, string FkName )
    {
      DbCmd.ExecuteCommand(
        "ALTER TABLE " + hostTableName
       + " DROP CONSTRAINT " + FkName
       , cp);
    }

    public void PrepareHostAndRefCols( )
    {
      DataTable tbl = DbCmd.GetColumnsByForeignKey(_cp, _refTable.ID, _referencedKey.Id);

      string comma = String.Empty;
      _colsHost = String.Empty;
      _colsRef = String.Empty;

      foreach (DataRow row in tbl.Rows)
      {
        _colsRef += comma + (string)row["colName"];
        _colsHost += comma + GetColumns((string)row["colName"]);
        comma = ", ";
      }
    }

    public void SetTableName( )
    {
      string cmdText = "";
      cmdText += "SELECT OBJECT_NAME(I.id) as tableName, I.id as tableID FROM sysindexes I ";
      cmdText += " WHERE I.name = '" + this.NormalizedName + "' AND (((I.status & 0x800)=0x800) OR ((I.status & 0x1000)=0x1000)) ";
      cmdText += " ORDER BY I.indid";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            _hostTable.ID = Convert.ToInt64(reader[1].ToString());
            _hostTable.LoadProperties();
            _hostTable.LoadColumns();
          }
        }
        finally
        {
          reader.Close();
          reader.Dispose();
        }
      }
    }


    public override string ToString( )
    {
      return Name;
    }


  }
}
