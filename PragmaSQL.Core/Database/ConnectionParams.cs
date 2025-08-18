using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
    /// <summary>
    /// ConnectionParams - Encapsulates all attributes assosiated with with the connection.
    /// </summary>
    [Serializable]
    public class ConnectionParams
    {

        public Guid ID;
        //public string Provider = String.Empty;
        public string IntegratedSecurity = String.Empty;
        public string Server = String.Empty;
        public string FriendlyName = String.Empty;
        public string Database = String.Empty;
        public string UserName = String.Empty;
        public string Password = String.Empty;
        public string TimeOut = "15";
        public bool IsConnected = true;
        public object Connection;
        public bool SaveEncrypted = true;
        public bool Encrypt = false;


        private bool UseConnectionPooling
        {
            get
            {
                if (ConfigHelper.Current != null && ConfigHelper.Current.GeneralOptions != null)
                    return ConfigHelper.Current.GeneralOptions.UseConnectionPooling;
                else
                    return false;
            }
        }


        public string ConnectionString
        {
            get
            {
                return SqlClientConnectionString;
            }
        }



       

        private string SqlClientConnectionString
        {
            get
            {

                string connString = "Data Source=" + Server +
                                                        "; Initial Catalog=" + Database +
                                                        "; Connection Timeout=" + TimeOut +
                                                        "; Application Name=PragmaSQL";

                if (IntegratedSecurity.Length > 0)
                {
                    connString += "; Integrated Security=" + IntegratedSecurity;
                }
                else
                {
                    connString += 
                        "; User ID=" + UserName +
                        "; Password=" + Password;
                }

                if(Encrypt)
                {
                    connString += "; Encrypt=True";
                }
                else
                {
                    connString += "; Encrypt=False";
                }

                return connString;
            }
        }

        public ConnectionParams CreateCopy()
        {
            ConnectionParams result = new ConnectionParams();
            result.ID = this.ID;
            //result.Provider = this.Provider;
            result.IntegratedSecurity = this.IntegratedSecurity;
            result.Server = this.Server;
            result.FriendlyName = this.FriendlyName;
            result.Database = this.Database;
            result.UserName = this.UserName;
            result.Password = this.Password;
            result.TimeOut = this.TimeOut;
            result.Encrypt = this.Encrypt;
            result.IsConnected = this.IsConnected;
            result.Connection = this.Connection;
            result.SaveEncrypted = this.SaveEncrypted;

            return result;
        }

        public string CurrentUsername
        {
            get
            {
                if (this.IntegratedSecurity.Length != 0)
                {
                    return System.Environment.UserName + "@" + System.Environment.UserDomainName;
                }
                else
                {
                    return UserName;
                }
            }
        }

        public SqlConnection CreateSqlConnection(bool open, bool? enablePooling)
        {
            var connStr = GetConnectionString(open: open, enablePooling: enablePooling);
            SqlConnection result = new SqlConnection(connStr);
            if (open)
                result.Open();
            return result;
        }

        public string GetConnectionString(bool open, bool? enablePooling)
        {
            string connStr = this.ConnectionString;
            if (UseConnectionPooling)
            {
                connStr = connStr + "; Pooling = true";
            }
            else if (enablePooling.HasValue)
            {
                connStr = connStr + (enablePooling.Value ? "; Pooling = true" : "; Pooling = false");
            }

            return connStr;
        }

        public SqlConnection CreateSqlConnection(bool open)
        {
            return CreateSqlConnection(open, null);
        }

        public string InfoDbServer
        {
            get
            {
                return this.Database + " on " + this.Server;
            }
        }

        public static string CreateInfoDbServer(string db, string server)
        {
            return db + " on " + server;
        }

        public static string CreateInfoDbServer(string db, ConnectionParams cp)
        {
            if (cp == null)
                return String.Empty;

            return db + " on " + cp.Server;
        }


        public string PrepareConnKey()
        {
            return ConnectionParams.PrepareConnKey(this);
        }
        public string PrepareConnKeyWithDb()
        {
            return ConnectionParams.PrepareConnKeyWithDb(this);
        }

        public string GetHostName()
        {
            string srvIp = Server.Trim();
            Match m = Regex.Match(srvIp, @"^\d+.\d+.\d+.\d+", RegexOptions.Singleline);
            if (!m.Success)
                return String.Empty;
            else
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(m.Value);
                if (hostEntry == null)
                    return String.Empty;

                return hostEntry.HostName;
            }
        }

        #region Connection Key Utils

        public static string PrepareConnKey(ConnectionParams cp)
        {
            return cp.Server.Trim().ToLowerInvariant() + ((Char)29).ToString() + cp.CurrentUsername.Trim().ToLowerInvariant();
        }

        public static string PrepareConnKeyWithDb(ConnectionParams cp)
        {
            return cp.Server.Trim().ToLowerInvariant() + ((Char)29).ToString() + cp.CurrentUsername.Trim().ToLowerInvariant() + ((Char)29).ToString() + cp.Database.Trim().ToLowerInvariant();
        }

        public static string PrepareConnKey(string serverName, string currentUsername)
        {
            return serverName.Trim().ToLowerInvariant() + ((Char)29).ToString() + currentUsername.Trim().ToLowerInvariant();
        }

        public static string[] ParseConnKey(string key)
        {

            if (String.IsNullOrEmpty(key))
                return new string[0];
            else
                return key.Split(new char[1] { (Char)29 }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion //Connection Key Utils

    }


    /// <summary>
    /// ConnectionParamsCollection - A collection of DataSourceDefinitions
    /// </summary>
    [Serializable]
    public class ConnectionParamsCollection : Collection<ConnectionParams>
    {
        public virtual void Add(
            string Provider,
            string IntegratedSecurity,
            string Name,
            string PersistSecurityInfo,
            string InitialCatalog,
            string UserName,
            string Password,
            string TimeOut,
            bool Encrypt,
            bool IsConnected)
        {
            ConnectionParams cp = new ConnectionParams();
            cp.ID = Guid.NewGuid();
            //cp.Provider = Provider;
            cp.IntegratedSecurity = IntegratedSecurity;
            cp.Server = Name;
            cp.Database = InitialCatalog;
            cp.UserName = UserName;
            cp.Password = Password;
            cp.TimeOut = TimeOut;
            cp.Encrypt = Encrypt;
            cp.IsConnected = IsConnected;
            this.Add(cp);
        }
        public virtual void Add(ConnectionParams cp)
        {
            this.Items.Add(cp);
        }

        public virtual ConnectionParams this[int Index]
        {
            get
            {
                return (ConnectionParams)this.Items[Index];
            }
        }

        public ConnectionParams FindByID(Guid id)
        {
            foreach (ConnectionParams cp in this)
            {
                if (cp.ID == id)
                    return cp;
            }
            return null;
        }

        public void Delete(ConnectionParams cp)
        {
            int index = 0;
            foreach (ConnectionParams d in this)
            {
                if (d.ID == cp.ID)
                {
                    this.RemoveAt(index);
                    ConnectionParamsFactory.Save(this);
                    return;
                }
                index++;
            }
        }
    }

    /// <summary>
    ///  Deserializes DBConnections.config to a DataSourceDefinitionCollection
    /// </summary>
    public class ConnectionParamsFactory
    {
        private static string _path = String.Empty;

        static ConnectionParamsFactory()
        {
            string appDataDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL";
            if (!Directory.Exists(appDataDir))
            {
                Directory.CreateDirectory(appDataDir);
            }

            if (!Directory.Exists(appDataDir))
            {
                FileInfo fi = new FileInfo(Application.ExecutablePath);
                _path = fi.Directory + "\\DBConnections.config";
            }
            else
            {
                _path = appDataDir + "\\DBConnections.config";
            }
        }

        public static ConnectionParamsCollection GetConnections()
        {
            ConnectionParamsCollection _connParams;
            bool SaveEncrypted = false;

            if (!File.Exists(_path))
            {
                return new ConnectionParamsCollection();
            }


            _connParams = ObjectXMLSerializer<ConnectionParamsCollection>.Load(_path);

            foreach (ConnectionParams cp in _connParams)
            {
                if (!cp.SaveEncrypted)
                    SaveEncrypted = true;
                else
                    cp.Password = EncryiptionHelper.Decrypt(cp.Password);
            }

            if (SaveEncrypted)
                Save(_connParams);


            return _connParams;
        }

        public static IDictionary<string, ConnectionParams> EnumerateConnections()
        {
            ConnectionParamsCollection _connParams;
            IDictionary<string, ConnectionParams> result = new Dictionary<string, ConnectionParams>();

            if (!File.Exists(_path))
            {
                return result;
            }
            _connParams = ObjectXMLSerializer<ConnectionParamsCollection>.Load(_path);
            string normalServerName = String.Empty;
            foreach (ConnectionParams cp in _connParams)
            {
                if (cp.SaveEncrypted)
                    cp.Password = EncryiptionHelper.Decrypt(cp.Password);

                normalServerName = cp.Server.Trim().ToLowerInvariant();
                if (!result.ContainsKey(normalServerName))
                    result.Add(normalServerName, cp);
            }

            return result; ;
        }

        public static void Save(ConnectionParamsCollection connParams)
        {
            foreach (ConnectionParams cp in connParams)
            {
                cp.SaveEncrypted = true;
                cp.Password = EncryiptionHelper.Encrypt(cp.Password);
            }
            /*
            string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DBConnections.config");
            XmlSerializer ser = new XmlSerializer(typeof(ConnectionParamsCollection));
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, connParams);
            writer.Close();
            */

            ObjectXMLSerializer<ConnectionParamsCollection>.Save(connParams, _path);
            // Reset password
            foreach (ConnectionParams cp in connParams)
                cp.Password = EncryiptionHelper.Decrypt(cp.Password);

        }

    }
}
