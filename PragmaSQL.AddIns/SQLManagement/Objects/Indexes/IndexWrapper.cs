/********************************************************************
  Class Index
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using System.Data;
using System.Data.SqlClient;
using PragmaSQL.Core;

namespace SQLManagement
{
  public class IndexWrapper : DatabaseObjectBase
  {

    #region Fields And Properties
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
          _cp = null;
        }
      }
    }

    private bool _unique;
    public bool Unique
    {
      get { return _unique; }
      set { _unique = value; }
    }

    private bool _padIndex;
    public bool PadIndex
    {
      get { return _padIndex; }
      set { _padIndex = value; }
    }


    private bool _sortInTempDB;
    public bool SortInTempDB
    {
      get { return _sortInTempDB; }
      set { _sortInTempDB = value; }
    }

    private bool _noRecompute;
    public bool NoRecompute
    {
      get { return _noRecompute; }
      set { _noRecompute = value; }
    }

    private int _fillFactor;
    public int FillFactor
    {
      get { return _fillFactor; }
      set { _fillFactor = value; }
    }

    private bool _clustered;
    public bool Clustered
    {
      get { return _clustered; }
      set { _clustered = value; }
    }

    private bool _ignoreDupKey;
    public bool IgnoreDupKey
    {
      get { return _ignoreDupKey; }
      set { _ignoreDupKey = value; }
    }

    private string _fileGroup;
    public string FileGroup
    {
      get { return _fileGroup; }
      set { _fileGroup = value; }
    }

    private ArrayList _columns;
    public ArrayList Columns
    {
      get { return _columns; }
      set { _columns = value; }
    }

   

    #endregion //Fields And Properties

    public IndexWrapper( )
    {
      this.Columns = new ArrayList();
    }

    public IndexWrapper( ConnectionParams cp )
      : this()
    {
      _cp = cp.CreateCopy();
    }

    public void Create(bool dropExistsing )
    {
      string cmdText = "";
      cmdText += "CREATE";

      if (_unique)
        cmdText += " UNIQUE";
      if (Clustered)
        cmdText += " CLUSTERED";

      cmdText += " INDEX " + Utils.Qualify(Name);
      cmdText += " ON " + QualifiedOwnerObjectName;

      if (this.Columns.Count > 0)
      {
        cmdText += " (";
        foreach (string col in this.Columns)
        {
          if (this.Columns.Count > 1 && this.Columns.IndexOf(col) > 0)
            cmdText += ",";
          cmdText += col;
        }
        cmdText += ")";
      }
      
      ArrayList withOptions = new ArrayList();

      //With options
      if (_padIndex)
        withOptions.Add("PAD_INDEX");
      if (FillFactor > 0)
        withOptions.Add("FILLFACTOR = " + FillFactor);
      if (_ignoreDupKey && _unique)
        withOptions.Add("IGNORE_DUP_KEY");
      if (_noRecompute)
        withOptions.Add("STATISTICS_NORECOMPUTE");
      if (_sortInTempDB)
        withOptions.Add("SORT_IN_TEMPDB");
      if(dropExistsing)
        withOptions.Add("DROP_EXISTING");


      if (withOptions.Count > 0)
        cmdText += " WITH " + String.Join(",", withOptions.ToArray(typeof(string)) as string[]);

      cmdText += " ON [" + this.FileGroup + "]";
      DbCmd.ExecuteCommand(cmdText,_cp);
    }


   

    public void Alter( )
    {
      Create(true);
    }

    
    public void Drop( )
    {
      try
      {
        string cmdText = "DROP INDEX " + QualifiedOwnerObjectName + "." + Utils.Qualify(Name);
        DbCmd.ExecuteCommand(cmdText,_cp);
      }
      catch (Exception)
      {
        throw;
      }
    }

    
    public void Rename( string newName )
    {
      DbCmd.ExecuteCommand("EXEC sp_rename '" + NormalizedOwnerObjectName + "." + NormalizedName + "','" + Utils.ReplaceQuatations(newName) + "','INDEX'", _cp);
      Name = newName;
    }

    
    public DataTable GetStatistics( )
    {
      string cmdText = "SELECT id, indid, name, dpages,reserved,used,minlen,xmaxlen,rowmodctr,maxirow,lockflags,pgmodctr FROM sysindexes WHERE id=" + OwnerObjectId.ToString() + " AND indid='" + this.ID.ToString() + "'";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }

    /// <summary>
    /// Load index identity properties
    /// </summary>
    public void LoadProperties( )
    {
      string cmdText = String.Format(ResManager.GetDBScript("Script_GetIndexProperties"), ID, OwnerObjectId);

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText,conn);
        try
        {
          while (reader.Read())
          {
            this.Name = reader.GetString(0);
            this.Owner = reader.GetString(1);
            this.OwnerObjectId = reader.GetInt32(2);
            this.OwnerObjectName = reader.GetString(3);
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
        }
      }
    }


    /// <summary>
    /// Load all index properties
    /// </summary>
    public void LoadAllProperties( )
    {
      string cmdText = "SELECT case when(I.status & 16) = 16 then 1 else 0 end as [clustered],";
      cmdText += " case when(I.status & 1) = 1 then 1 else 0 end as [IgnoreDupKey],";
      cmdText += " case when(I.status & 16777216) = 16777216 then 1 else 0 end as [NoRecompute],";
      cmdText += " case when(I.status & 256) = 256 then 1 else 0 end as [PadIndex],";
      cmdText += " case when(I.status & 2) = 2 then 1 else 0 end as [unique],";
      cmdText += " FILEGROUP_NAME(I.groupid) as [filegroup],";
      cmdText += " I.OrigFillFactor";

      cmdText += " FROM sysobjects O,sysindexes I ";
      cmdText += " WHERE I.indid = " + ID.ToString() + " AND O.id = " + OwnerObjectId.ToString();
      cmdText += " AND I.status&2048!=2048 AND I.status&4096!=4096 AND I.id=O.id AND type in ('U','V') ";
      cmdText += " AND (INDEXPROPERTY(I.id,I.name,'IsStatistics') <> 1) AND (INDEXPROPERTY(I.id,I.name,'IsAutoStatistics') <> 1) ";
      cmdText += " AND (INDEXPROPERTY(I.id,I.name,'IsHypothetical') <> 1) AND O.type!='S'";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            this.Clustered = Convert.ToBoolean(reader.GetValue(0));
            _ignoreDupKey = Convert.ToBoolean(reader.GetValue(1));
            _noRecompute = Convert.ToBoolean(reader.GetValue(2));
            _padIndex = Convert.ToBoolean(reader.GetValue(3));
            _unique = Convert.ToBoolean(reader.GetValue(4));
            this.FileGroup = reader.GetValue(5).ToString();
            this.FillFactor = Convert.ToInt32(reader.GetValue(6));
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
        }
      }
    }

    /// <summary>
    /// Gets the columns not in the index
    /// </summary>
    /// <returns>Columns not indexed</returns>
    public DataTable GetIndexNoColumns( )
    {
      string cmdText = "";
      cmdText += "SELECT dbo.syscolumns.name AS [column], dbo.systypes.name AS type";
      cmdText += " FROM dbo.syscolumns LEFT OUTER JOIN";
      cmdText += " dbo.systypes ON dbo.syscolumns.xusertype = dbo.systypes.xusertype";
      cmdText += " WHERE dbo.syscolumns.id = OBJECT_ID('" + this.NormalizedOwnerObjectName + "')";
      cmdText += " AND dbo.syscolumns.name NOT IN (SELECT COL_NAME(id, colid) FROM sysindexkeys WHERE id=OBJECT_ID('" + NormalizedOwnerObjectName + "') AND indid=" + ID.ToString() + ")";

      return DbCmd.ExecuteDataTable(cmdText,_cp);
    }

    /// <summary>
    /// Gets the columns in the index
    /// </summary>
    /// <returns>Columns indexed</returns>
    public DataTable GetIndexColumns( )
    {
      string cmdText = "";
      cmdText += " SELECT keyno,COL_NAME(id, colid) as [column]";
      cmdText += " FROM sysindexkeys ";
      cmdText += " WHERE id=OBJECT_ID('" + NormalizedOwnerObjectName + "') AND indid=" + ID.ToString() + " ORDER BY keyno";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }
  }
}
