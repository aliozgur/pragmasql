/********************************************************************
  Class UdtWrapper
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public class UdtWrapper : DatabaseObjectBase
  {

    #region Fields And Properties

    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set { _cp = value; }
    }


    private bool _allowNulls = false;
    public bool AllowNulls
    {
      get { return _allowNulls; }
      set { _allowNulls = value; }
    }

    private string _baseDataType;
    public string BaseDataType
    {
      get { return _baseDataType; }
      set { _baseDataType = value; }
    }

    private string _oldDefault = String.Empty;
    private string _default = String.Empty;
    public string Default
    {
      get { return _default; }
      set { _default = value; }
    }

    private string _oldRule = String.Empty;
    private string _rule = String.Empty;
    public string Rule
    {
      get { return _rule; }
      set { _rule = value; }
    }

    private string _scale = String.Empty;
    public string Scale
    {
      get { return _scale; }
      set { _scale = value; }
    }

    private string _width = String.Empty;
    public string Width
    {
      get { return _width; }
      set { _width = value; }
    }

    #endregion //Fields And Properties

    public UdtWrapper( )
    {

    }

    public UdtWrapper( ConnectionParams cp )
    {
      ConnectionParams = cp;
    }

    public void Drop( )
    {
      string cmdText = String.Empty;
      cmdText = "EXEC sp_droptype N'" + this.NormalizedName + "'";
      DbCmd.ExecuteCommand(cmdText, _cp);
    }
    
    public void LoadProperties( )
    {
      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";
      cmdText += "SELECT name, CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(uid)  ELSE SCHEMA_NAME(uid) END  as owner FROM systypes S "
        + " WHERE xusertype > 256 AND usertype = " + ID.ToString();

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
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
      }
    }

    public void LoadPropertiesByName( )
    {
      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";
      cmdText += "SELECT usertype, CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(uid)  ELSE SCHEMA_NAME(uid) END as owner FROM systypes S "
        + " WHERE xusertype > 256 AND name = '" + this.NormalizedName + "'";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            this.ID = reader.GetInt16(0);
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

    public void LoadDefinition( )
    {
      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";
      cmdText += "SELECT S.name, CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(S.uid)  ELSE SCHEMA_NAME(S.uid) END, T.name, S.length, S.scale, S.allownulls";
      cmdText += " FROM systypes S,systypes T WHERE S.usertype=" + ID.ToString()
        + " AND S.xtype=T.xtype AND T.xusertype<257 AND T.xusertype NOT IN(256,189)";

      using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            this.Name = reader.GetString(0);
            this.Owner = reader.GetString(1);

            _baseDataType = reader.GetString(2);
            _width = reader.GetValue(3).ToString();
            _scale = reader.GetValue(4).ToString();
            _allowNulls = Convert.ToBoolean(reader.GetValue(5));
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
          reader.Dispose();
        }

        //Default
        cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";
        cmdText += "SELECT (CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(O.uid)  ELSE SCHEMA_NAME(O.uid) END) + '.' + (O.name) FROM sysobjects O, systypes T WHERE T.usertype=" + ID.ToString()
          + " AND O.type='D' AND T.tdefault=O.id";
        reader = DbCmd.ExecuteReader(cmdText,conn);
        try
        {
          while (reader.Read())
          {
            _default = reader.GetString(0);
            _oldDefault = _default;
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
          reader.Dispose();
        }

        //Rule
        cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";
        cmdText += "SELECT (CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(O.uid)  ELSE SCHEMA_NAME(O.uid) END) + '.' + (O.name) FROM sysobjects O, systypes T WHERE T.usertype="
          + ID.ToString() + " AND O.type='R' AND T.domain=O.id";
        reader = DbCmd.ExecuteReader(cmdText, conn);
        try
        {
          while (reader.Read())
          {
            _rule = reader.GetString(0);
            _oldRule = _rule;
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
          reader.Dispose();
        }
      }
    }

    public void Create( )
    {
      string cmdText = "EXEC sp_addtype " + this.NormalizedName;
      cmdText += ", ";
      
      //add width and scale if there's
      if (!String.IsNullOrEmpty(_width))
      {
        if (!String.IsNullOrEmpty(_scale) && _scale != "0")
        {
          cmdText += "'" + _baseDataType + "(" + _width + "," + _scale + ")'";
        }
        else
        {
          cmdText += "'" + _baseDataType + "(" + _width + ")'";
        }
      }
      else
      {
        cmdText += "'" + _baseDataType + "'";
      }

      if (_allowNulls)
      {
        cmdText += ", 'NULL'";
      }
      else
      {
        cmdText += ", 'NOT NULL'";
      }

      DbCmd.ExecuteCommand(cmdText,_cp);

      if (!String.IsNullOrEmpty(_default))
      {
        BindDefault(_default);
      }
      if (!String.IsNullOrEmpty(_rule))
      {
        BindRule(_rule);
      }

      LoadPropertiesByName();
    }

    public void BindDefault( string defaultName )
    {
      string cmdText = "EXEC sp_bindefault '" + Utils.ReplaceQuatations(defaultName) 
        + "', '" + this.NormalizedName + "'";
      DbCmd.ExecuteCommand(cmdText,_cp);
    }
    
    public void BindRule( string ruleName )
    {
      string cmdText = "EXEC sp_bindrule '" + Utils.ReplaceQuatations(ruleName) 
        + "', '" + this.NormalizedName + "'";
      DbCmd.ExecuteCommand(cmdText,_cp);
    }

    public void UnBindDefault( )
    {
      string cmdText = "EXEC sp_unbindefault '" + this.NormalizedName + "'";
      DbCmd.ExecuteCommand(cmdText,_cp);
    }

    public void UnBindRule( )
    {
      string cmdText = "EXEC sp_unbindrule '" + this.NormalizedName + "'";
      DbCmd.ExecuteCommand(cmdText, _cp);
    }

    public void Rename( string newName )
    {
      string cmdText = "EXEC sp_rename '" + this.NormalizedFullName + "', '" + Utils.ReplaceQuatations(newName) + "',USERDATATYPE";
      DbCmd.ExecuteCommand(cmdText, _cp);
      this.Name = newName;
    }

    public void Alter( )
    {

      if (!String.IsNullOrEmpty(_default))
      {
        BindDefault(_default);
      }
      else if(!String.IsNullOrEmpty(_oldDefault))
      {
        UnBindDefault();
      }
      _oldDefault = _default;

      
      if (!String.IsNullOrEmpty(_rule))
      {
        BindRule(_rule);
      }
      else if (!String.IsNullOrEmpty(_oldRule))
      {
        UnBindRule();
      }
      _oldRule = _rule;
    }

    public DataTable GetDependencies( )
    {
      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";
      cmdText += "SELECT O.id, CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(O.uid)  ELSE SCHEMA_NAME(O.uid) END as tableOwner, O.name as tableName, C.name as columnName" 
        + " FROM sysobjects O,syscolumns C WHERE C.xusertype=" + ID.ToString() 
        + " AND O.type='U' AND C.id=O.id ORDER BY 1";
      return DbCmd.ExecuteDataTable(cmdText, _cp);
    }
  }
}
