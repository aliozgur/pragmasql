using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using PragmaSQL.Core;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace SQLManagement
{
    public static class SMOUtils
    {
        private static string CompactStrings(StringCollection strings)
        {
            if (strings == null)
            {
                return String.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (string value in strings)
                {
                    sb.Append(value);
                    sb.AppendLine();
                }
                return sb.ToString();
            }
        }


        public static string ScriptForeignKeys(ServerConnection conn, string tblName)
        {

            Server srv = new Server(conn);
            Database db = srv.Databases[conn.DatabaseName];
            Table tbl = db.Tables[tblName];

            if (tbl.ForeignKeys.Count == 0)
                return String.Empty;

            Scripter scripter = new Scripter(srv);
            scripter.Options.ToFileOnly = false;

            Urn[] urn = new Urn[tbl.ForeignKeys.Count];
            for (int i = 0; i < tbl.ForeignKeys.Count; i++)
            {
                urn[i] = tbl.ForeignKeys[i].Urn;
            }

            return CompactStrings(scripter.Script(urn));
        }

        public static string ScriptIndexes(ServerConnection conn, string tblName)
        {

            Server srv = new Server(conn);
            Database db = srv.Databases[conn.DatabaseName];
            Table tbl = db.Tables[tblName];

            if (tbl.Indexes.Count == 0)
                return String.Empty;

            Scripter scripter = new Scripter(srv);
            scripter.Options.ToFileOnly = false;

            UrnCollection urn = new UrnCollection();

            for (int i = 0; i < tbl.Indexes.Count; i++)
            {
                if (tbl.Indexes[i].IndexKeyType == IndexKeyType.DriPrimaryKey)
                    continue;
                urn.Add(tbl.Indexes[i].Urn);
            }

            return CompactStrings(scripter.Script(urn));
        }

        public static string ScriptPrimaryKey(ServerConnection conn, string tblName)
        {
            Server srv = new Server(conn);
            Database db = srv.Databases[conn.DatabaseName];
            Table tbl = db.Tables[tblName];

            if (tbl.Indexes.Count == 0)
                return String.Empty;


            Scripter scripter = new Scripter(srv);
            scripter.Options.ToFileOnly = false;
            string result = String.Empty;
            for (int i = 0; i < tbl.Indexes.Count; i++)
            {
                if (tbl.Indexes[i].IndexKeyType == IndexKeyType.DriPrimaryKey)
                {
                    Urn[] urn = new Urn[1];
                    urn[0] = tbl.Indexes[i].Urn;
                    result = CompactStrings(scripter.Script(urn));
                    break;
                }
            }

            return result;
        }

        public static string GetTriggerScripts(ServerConnection conn, string tblName)
        {
            Server srv = new Server(conn);
            Database db = srv.Databases[conn.DatabaseName];
            Table tbl = db.Tables[tblName];

            if (tbl.Indexes.Count == 0)
                return String.Empty;


            Scripter scripter = new Scripter(srv);
            scripter.Options.ToFileOnly = false;

            string result = String.Empty;
            for (int i = 0; i < tbl.Triggers.Count; i++)
            {
                /*
                if (tbl.Triggers[i].TextHeader )
                {
                    Urn[] urn = new Urn[1];
                    urn[0] = tbl.Indexes[i].Urn;
                    result = CompactStrings(scripter.Script(urn));
                    break;
                }
                */
                result += tbl.Triggers[i].TextHeader + "\r\n" + tbl.Triggers[i].TextBody + "\r\n" + conn.BatchSeparator + "\r\n";
            }

            return result;
        }

        public static string ScriptReferencingForeignKeys(ServerConnection conn, long refTableId)
        {
            string cmdText = @"select distinct fk.constid , const.name 'constname'
                            , sopk.id 'reftableid',sopk.name 'reftablename'
                            , sofk.id 'fktableid',sofk.name 'fktablename'
                          from sysforeignkeys fk
                            join sysobjects sopk on fk.rkeyid = sopk.id
                            join sysobjects sofk on fk.fkeyid = sofk.id
                            join sysobjects const on fk.constid = const.id";
            cmdText += "\r\n";
            cmdText += "where sopk.id = " + refTableId.ToString();

            StringBuilder result = new StringBuilder();
            Server srv = new Server(conn);
            Database db = srv.Databases[conn.DatabaseName];
            Table tbl = null;

            DataTable records = new DataTable();

            using (SqlConnection tmpConn = new SqlConnection(conn.ConnectionString))
            {
                tmpConn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmdText, tmpConn);
                try
                {
                    adapter.Fill(records);
                    if (records.Rows.Count == 0)
                        return string.Empty;


                    UrnCollection urns = new UrnCollection();
                    string fkTableName = String.Empty;
                    string refTableName = String.Empty;

                    foreach (DataRow row in records.Rows)
                    {
                        fkTableName = (string)row["fktablename"];
                        refTableName = (string)row["reftablename"];
                        tbl = db.Tables[fkTableName];

                        foreach (ForeignKey fk in tbl.ForeignKeys)
                        {
                            if (fk.ReferencedTable.ToLowerInvariant() == refTableName.ToLowerInvariant())
                            {
                                result.AppendLine(CompactStrings(fk.Script()));
                            }
                        }
                    }
                }
                finally
                {
                    adapter = null;
                    records = null;
                }
            }

            return result.ToString();
        }

        public static string CreateDropScriptOfReferencingForeignKeys(ServerConnection conn, long refTableId, out string fkList)
        {
            fkList = String.Empty;
            string cmdText = @"select distinct fk.constid , const.name 'constname'
                            , sopk.id 'reftableid',sopk.name 'reftablename'
                            , sofk.id 'fktableid',sofk.name 'fktablename'
                          from sysforeignkeys fk
                            join sysobjects sopk on fk.rkeyid = sopk.id
                            join sysobjects sofk on fk.fkeyid = sofk.id
                            join sysobjects const on fk.constid = const.id";
            cmdText += "\r\n";
            cmdText += "where sopk.id = " + refTableId.ToString();

            StringBuilder result = new StringBuilder();
            var reader = conn.ExecuteReader(cmdText);
            try
            {
                if (!reader.HasRows)
                    return string.Empty;


                string constname = String.Empty;
                string fktablename = String.Empty;
                string lineBreak = String.Empty;
                while (reader.Read())
                {
                    constname = (string)reader["constname"];
                    fktablename = (string)reader["fktablename"];
                    result.AppendLine("ALTER TABLE " + Utils.Qualify(fktablename) + " DROP CONSTRAINT " + Utils.Qualify(constname));
                    fkList += lineBreak + "Table: " + fktablename + ", ForeignKey:" + constname;
                    lineBreak = "\r\n";
                }
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();
            }

            return result.ToString();
        }

        public static void PopulateForeignKeysCombo(ComboBox cmb, ConnectionParams cp, string tableName)
        {
            if (cmb == null)
            {
                return;
            }

            cmb.Items.Clear();
            var connStr = cp.GetConnectionString(true, false);
            ServerConnection srvConn = new ServerConnection();
            srvConn.ConnectionString = connStr;
            try
            {
                srvConn.Connect();
                Server srv = new Server(srvConn);
                Database db = srv.Databases[srvConn.DatabaseName];
                Table hostTbl = db.Tables[tableName];
                ForeignKeyWrapper key = null;

                foreach (ForeignKey fk in hostTbl.ForeignKeys)
                {
                    key = new ForeignKeyWrapper(cp);
                    //Load properties
                    key.ID = fk.ID;
                    key.Name = fk.Name;

                    key.HostTable.ID = fk.Parent.ID;
                    key.HostTable.Name = fk.Parent.Name;
                    key.HostTable.Owner = fk.Parent.Schema;
                    key.HostTable.FileGroup = fk.Parent.FileGroup;
                    key.HostTable.LoadColumns(fk.Parent);

                    Table refTbl = db.Tables[fk.ReferencedTable];
                    key.RefTable.ID = refTbl.ID;
                    key.RefTable.Name = refTbl.Name;
                    key.RefTable.Owner = refTbl.Schema;
                    key.RefTable.FileGroup = refTbl.FileGroup;
                    key.RefTable.LoadColumns(refTbl);

                    cmb.Items.Add(key);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading foreign keys: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (srvConn != null && srvConn.IsOpen)
                {
                    srvConn.Disconnect();
                }
            }
        }

    }


}
