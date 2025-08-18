
using ICSharpCode.Core;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using PragmaSQL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLManagement
{
    /// <summary>
    /// Class for manage primary keys
    /// </summary>
    public class PrimaryKeyWrapper : TableKeyBase
    {

        string _owner;

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }


        public string QualifiedName
        {
            get
            {
                return Utils.Qualify(Name);
            }
        }

        public string QualifiedFullName
        {
            get
            {
                return Utils.Qualify(_owner) + "." + Utils.Qualify(Name);
            }
        }

        public string NormalizedName
        {
            get
            {
                return Utils.ReplaceQuatations(Name);
            }
        }

        public string NormalizedFullName
        {
            get
            {
                return Utils.ReplaceQuatations(_owner) + "." + Utils.ReplaceQuatations(Name);
            }
        }

        public string FullName
        {
            get
            {
                return _owner + "." + Name;
            }
        }

        public void LoadProperties()
        {
            string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() ";
            cmdText += "SELECT name, CASE WHEN @cmplevel < 90  THEN  USER_NAME(uid)  ELSE SCHEMA_NAME(uid) END, parent_obj FROM sysobjects WHERE id = " + ID.ToString();
            using (SqlConnection conn = base.ConnectionParams.CreateSqlConnection(true, false))
            {
                SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
                try
                {
                    while (reader.Read())
                    {
                        this.Name = reader.GetString(0);
                        this.Owner = reader.GetString(1);
                        this.Table.ID = Convert.ToInt64(reader.GetInt32(2));
                        this.Table.LoadProperties();
                        this.Table.LoadColumns();
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }



        public PrimaryKeyWrapper(ConnectionParams cp)
          : base(cp)
        {
            Columns = new List<string>();
        }



        public void Create()
        {
            DbCmd.ExecuteCommand(GetCreateScript(), base.ConnectionParams);
            RefreshOtherInformation();
            LoadColumns();
        }

        public string GetCreateScript()
        {
            string cmdText = "ALTER TABLE " + this.Table.QualifiedFullName + " ADD CONSTRAINT " + this.QualifiedName + " PRIMARY KEY";
            if (this.Clustered)
            {
                cmdText += " CLUSTERED";
            }
            else
            {
                cmdText += " NONCLUSTERED";
            }
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

            if (this.FillFactor > 0)
            {
                cmdText += " WITH FILLFACTOR=" + this.FillFactor.ToString();
            }
            cmdText += " ON [" + this.Filegroup + "]";

            return cmdText;
        }


        public void Drop()
        {
            DbCmd.ExecuteCommand("ALTER TABLE " + this.Table.QualifiedFullName + " DROP CONSTRAINT " + this.QualifiedName, base.ConnectionParams);
        }

        public void DropWithDepends(ServerConnection srvConn)
        {
            Server srv = new Server(srvConn);
            Database db = srv.Databases[srvConn.DatabaseName];
            Table tbl = db.Tables[this.Table.Name];

            string fkList = String.Empty;
            string dropScript = SMOUtils.CreateDropScriptOfReferencingForeignKeys(srvConn, this.Table.ID, out fkList);
            if (!String.IsNullOrEmpty(dropScript))
            {
                string msgStr = "Primary key \"" + this.Name + "\" is referenced by the following foreign keys.\r\n\r\n";
                msgStr += fkList + "\r\n\r\n";
                msgStr += "Foreign keys listed above have to be dropped in order to perform this operation.\r\n";
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

            srvConn.ExecuteNonQuery("ALTER TABLE " + this.Table.QualifiedFullName + " DROP CONSTRAINT " + this.QualifiedName);
        }

        public void DropWithDepends()
        {
            var connStr = this.ConnectionParams.GetConnectionString(true, false);
            ServerConnection srvConn = new ServerConnection();
            srvConn.ConnectionString = connStr;
            try
            {
                srvConn.Connect();
                srvConn.BeginTransaction();
                DropWithDepends(srvConn);
                srvConn.CommitTransaction();
            }
            catch (Exception ex)
            {
                srvConn.RollBackTransaction();
                throw ex;
            }
            finally
            {
                if (srvConn.IsOpen)
                {
                    srvConn.Disconnect();
                }
            }
        }

        public void DropAndRecreate()
        {
            var connStr = this.ConnectionParams.GetConnectionString(true, false);

            ServerConnection srvConn = new ServerConnection();
            srvConn.ConnectionString = connStr;
            try
            {
                srvConn.Connect();
                srvConn.BeginTransaction();
                DropWithDepends(srvConn);
                srvConn.ExecuteNonQuery(GetCreateScript());
                srvConn.CommitTransaction();

                RefreshOtherInformation();
            }
            catch (Exception ex)
            {
                srvConn.RollBackTransaction();
                throw ex;
            }
            finally
            {
                if (srvConn.IsOpen)
                {
                    srvConn.Disconnect();
                }
            }
        }

        public void Rename(string newName)
        {
            var connStr = this.ConnectionParams.GetConnectionString(true, false);
            ServerConnection srvConn = new ServerConnection();
            srvConn.ConnectionString = connStr;
            try
            {
                srvConn.Connect();
                srvConn.BeginTransaction();
                DropWithDepends(srvConn);
                this.Name = newName;
                srvConn.ExecuteNonQuery(GetCreateScript());
                srvConn.CommitTransaction();

                RefreshOtherInformation();
            }
            catch (Exception ex)
            {
                srvConn.RollBackTransaction();
                throw ex;
            }
            finally
            {
                if (srvConn.IsOpen)
                {
                    srvConn.Disconnect();
                }
            }
        }


        public DataTable GetKeyColumns()
        {
            string cmdText = " SELECT COL_NAME(id, colid) as [column]";
            cmdText += " FROM sysindexkeys ";
            cmdText += " WHERE id=OBJECT_ID('" + this.Table.NormalizedFullName + "') AND indid=" + this.ID.ToString() + " ORDER BY keyno";

            return DbCmd.ExecuteDataTable(cmdText, base.ConnectionParams);
        }

        public void LoadColumns()
        {
            Columns.Clear();
            DataTable tbl = GetKeyColumns();
            foreach (DataRow row in tbl.Rows)
            {
                if (!Utils.IsDbValueValid("column"))
                    continue;
                Columns.Add((string)row["column"]);
            }
        }

        public DataTable GetKeyNoColumns()
        {
            string cmdText = "SELECT dbo.syscolumns.name AS [column], dbo.systypes.name AS type";
            cmdText += " FROM dbo.syscolumns LEFT OUTER JOIN";
            cmdText += " dbo.systypes ON dbo.syscolumns.xusertype = dbo.systypes.xusertype";
            cmdText += " WHERE dbo.syscolumns.id = OBJECT_ID('" + this.Table.NormalizedFullName + "') AND (dbo.syscolumns.isnullable = 0)";
            cmdText += " AND dbo.syscolumns.name NOT IN (SELECT COL_NAME(id, colid) FROM sysindexkeys WHERE id=OBJECT_ID('" + this.Table.NormalizedFullName + "') AND indid=1)";

            return DbCmd.ExecuteDataTable(cmdText, base.ConnectionParams);
        }

        public void GetOtherInformation()
        {
            string cmdText = "SELECT indid, OrigFillFactor, FILEGROUP_NAME(groupid)";
            cmdText += " FROM sysindexes WHERE id=OBJECT_ID('" + this.Table.NormalizedFullName + "') AND name = '" + this.NormalizedName + "'";
            cmdText += " AND (((status & 0x800)=0x800) OR ((status & 0x1000)=0x1000))";

            using (SqlConnection conn = base.ConnectionParams.CreateSqlConnection(true, false))
            {
                SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
                while (reader.Read())
                {
                    this.ID = Convert.ToInt32(reader[0].ToString());
                    if (reader.GetInt16(0) == 1)
                    {
                        this.Clustered = true;
                    }
                    else
                    {
                        this.Clustered = false;
                    }
                    this.Filegroup = reader.GetString(2);
                    this.FillFactor = Convert.ToByte(reader[1].ToString());
                }
                reader.Close();
            }

            LoadColumns();
        }


        public void RefreshOtherInformation()
        {
            string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel from  master..sysdatabases where name = DB_NAME() ";
            cmdText += "SELECT si.indid , si.OrigFillFactor 'fillfactor', FILEGROUP_NAME(si.groupid) 'filegroup',  CASE WHEN @cmplevel < 90  THEN  USER_NAME(so.uid)  ELSE SCHEMA_NAME(so.uid) END 'owner'";
            cmdText += " FROM sysindexes si";
            cmdText += " JOIN sysobjects so on si.id = so.id";
            cmdText += " WHERE si.id=OBJECT_ID('" + this.Table.NormalizedFullName + "') AND si.name = '" + this.Name + "'";
            cmdText += " AND (((si.status & 0x800)=0x800) OR ((si.status & 0x1000)=0x1000))";

            using (SqlConnection conn = base.ConnectionParams.CreateSqlConnection(true, false))
            {
                SqlDataReader reader = DbCmd.ExecuteReader(cmdText, conn);
                while (reader.Read())
                {
                    this.ID = Convert.ToInt32(reader[0].ToString());
                    this.Owner = reader.GetString(3);
                }
                reader.Close();
            }
        }


        public override string ToString()
        {
            return FullName;
        }
    }
}
