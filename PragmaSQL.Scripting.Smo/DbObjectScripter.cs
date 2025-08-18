/********************************************************************
  Class DatabaseScripter
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using PragmaSQL.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SFC = Microsoft.SqlServer.Management.Sdk.Sfc;

namespace PragmaSQL.Scripting.Smo
{

    public sealed class DbObjectScripter : IDisposable
    {
        #region Fields and Properties

        public bool Cancelled = false;
        private Server srvr = null;
        private Scripter scrp = null;
        private Database db = null;
        private ServerConnection sqlConn = null;
        private string srvrVersion = String.Empty;

        private SearchType _dbObjectSearchType = SearchType.PlainText;
        public SearchType DbObjectSearchType
        {
            get { return _dbObjectSearchType; }
            set
            {
                _dbObjectSearchType = value;
                EscapeSearchText();
            }
        }

        private string _escapedText = String.Empty;

        private string _searchText = String.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                EscapeSearchText();
            }
        }



        private ScriptingProgressDelegate _scriptingProgress;
        public event ScriptingProgressDelegate ScriptingProgress
        {
            add
            {
                _scriptingProgress += value;
            }
            remove
            {
                _scriptingProgress -= value;
            }
        }

        private ConnectionParams _connParams = null;
        public ConnectionParams ConnParams
        {
            get { return _connParams; }
            private set
            {
                if (value != null)
                {
                    _connParams = value.CreateCopy();
                }
                else
                {
                    _connParams = value;
                }
            }
        }

        private ScriptObjectTypesList _objectTypes = new ScriptObjectTypesList();
        public ScriptObjectTypesList ObjectTypes
        {
            get { return _objectTypes; }
            set { _objectTypes = value; }
        }


        #endregion //Fields and Properties

        #region Constructor
        public DbObjectScripter(ConnectionParams cp)
        {
            ConnParams = cp;
            var connStr = _connParams.GetConnectionString(false, false);
            sqlConn = new ServerConnection();
            sqlConn.ConnectionString = connStr;
            sqlConn.Connect();
            srvr = new Server(sqlConn);


            srvr.SetDefaultInitFields(true);
            srvrVersion = srvr.Information.VersionString;
            db = srvr.Databases[_connParams.Database];
            scrp = new Scripter(srvr);

            PrepareScripterOptions();
        }

        private void PrepareScripterOptions()
        {
            // set common scipter options
            scrp.Options.AnsiFile = false;
            scrp.Options.AppendToFile = false;
            scrp.Options.ContinueScriptingOnError = true;
            scrp.Options.ExtendedProperties = true;
            scrp.Options.IncludeHeaders = true;
            scrp.Options.Permissions = false;
            scrp.Options.PrimaryObject = true;
            scrp.Options.SchemaQualify = true;
            scrp.Options.ToFileOnly = false;

            scrp.Options.DriAll = true;
            scrp.Options.Triggers = true;
            scrp.Options.Indexes = true;
            scrp.Options.ClusteredIndexes = true;


            scrp.Options.ConvertUserDefinedDataTypesToBaseType = true;

            if (srvrVersion.StartsWith("8."))
            {
                scrp.Options.TargetServerVersion = SqlServerVersion.Version80;
            }
            else if (srvrVersion.StartsWith("9."))
            {
                scrp.Options.TargetServerVersion = SqlServerVersion.Version90;
            }
            else
            {
                throw new Exception("Only SQL Server 2000 and SQL Server 2005 databases are supported!");
            }
        }

        #endregion //Constructor

        #region Utils
        private string CompactStrings(StringCollection strings)
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

        private void OnScriptingProgress(string objType, string objectName, int total, int current)
        {
            if (_scriptingProgress != null)
            {
                _scriptingProgress(objType, objectName, total, current);
            }
        }

        private void EscapeSearchText()
        {
            if (String.IsNullOrEmpty(_searchText))
            {
                _escapedText = String.Empty;
                return;
            }

            switch (_dbObjectSearchType)
            {
                case SearchType.RegularExpression:
                    // If user selected Regular Expression, just pass the text directly
                    _escapedText = _searchText;
                    break;

                case SearchType.Wildcards:
                    // Escapes the text, then converts wildcard tokens to regex equivalents
                    _escapedText = Regex.Escape(_searchText).Replace(@"\*", ".*").Replace(@"\?", ".");
                    break;

                case SearchType.PlainText:
                default:
                    // Just a plain text search... 'escape' the text so it can be used in the regular expression
                    _escapedText = Regex.Escape(_searchText);
                    break;
            }
        }

        private bool MatchDbObject(string objName)
        {
            if (String.IsNullOrEmpty(_escapedText))
            {
                return true;
            }

            RegexOptions options = RegexOptions.None;
            options |= RegexOptions.IgnoreCase;
            Regex regex = new Regex(_escapedText, options);
            return regex.Match(objName).Success;
        }

        #endregion

        #region InMemory Scripting Methods

        private string ScriptDatabase()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            urn[0] = db.Urn;
            return CompactStrings(scrp.Script(urn));
        }

        public string ScriptUserTable(int tableId)
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();
            Table tbl = db.Tables.ItemById(tableId);
            if (tbl == null || tbl.IsSystemObject)
                return String.Empty;

            urn[0] = tbl.Urn;

            // set common scipter options
            scrp.Options.AnsiFile = false;
            scrp.Options.AppendToFile = false;
            scrp.Options.ContinueScriptingOnError = true;
            scrp.Options.ExtendedProperties = true;
            scrp.Options.IncludeHeaders = true;
            scrp.Options.PrimaryObject = true;
            scrp.Options.SchemaQualify = true;
            scrp.Options.ToFileOnly = false;
            scrp.Options.Permissions = false;
            scrp.Options.DriAll = false;
            scrp.Options.Triggers = false;
            scrp.Options.Indexes = true;
            scrp.Options.ClusteredIndexes = true;


            string script = CompactStrings(scrp.Script(urn));
            if (!String.IsNullOrEmpty(script))
            {
                sb.Append(script);
            }

            return sb.ToString();
        }

        private string ScriptUserTables()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();
            int total = db.Tables.Count;
            int current = 0;

            foreach (Table tbl in db.Tables)
            {
                current++;

                if (Cancelled)
                {
                    break;
                }
                if (tbl.IsSystemObject || !MatchDbObject(tbl.Name))
                {
                    OnScriptingProgress("Table (Will Skip)", tbl.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Table", tbl.Name, total, current);

                urn[0] = tbl.Urn;
                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptViews()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.Views.Count;
            int current = 0;

            foreach (View vw in db.Views)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }
                if (vw.IsSystemObject || !MatchDbObject(vw.Name))
                {
                    OnScriptingProgress("View (Will Skip)", vw.Name, total, current);
                    continue;
                }
                OnScriptingProgress("View", vw.Name, total, current);

                urn[0] = vw.Urn;
                string script = CompactStrings(scrp.Script(urn));

                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptStoredProcs()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.StoredProcedures.Count;
            int current = 0;

            foreach (StoredProcedure sp in db.StoredProcedures)
            {
                current++;

                if (Cancelled)
                {
                    break;
                }
                if (sp.IsSystemObject || !MatchDbObject(sp.Name))
                {
                    OnScriptingProgress("Stored Proc (Will Skip)", sp.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Stored Proc", sp.Name, total, current);

                urn[0] = sp.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptUserDefinedFunctions()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.UserDefinedFunctions.Count;
            int current = 0;

            foreach (UserDefinedFunction fn in db.UserDefinedFunctions)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }
                if (fn.IsSystemObject || !MatchDbObject(fn.Name))
                {
                    OnScriptingProgress("Function (Will Skip)", fn.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Function", fn.Name, total, current);

                urn[0] = fn.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScripApplicationRoles()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.ApplicationRoles.Count;
            int current = 0;
            foreach (ApplicationRole role in db.ApplicationRoles)
            {
                current++;

                if (Cancelled)
                {
                    break;
                }

                if (!MatchDbObject(role.Name))
                {
                    OnScriptingProgress("App Role (Will Skip)", role.Name, total, current);
                    continue;
                }

                OnScriptingProgress("App Role", role.Name, total, current);

                urn[0] = role.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptDatabaseRoles()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.Roles.Count;
            int current = 0;

            foreach (DatabaseRole role in db.Roles)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }
                if (role.IsFixedRole || !MatchDbObject(role.Name))
                {
                    OnScriptingProgress("Db Role (Will Skip)", role.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Db Role", role.Name, total, current);

                urn[0] = role.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptSchemas()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.Schemas.Count;
            int current = 0;

            foreach (Schema schema in db.Schemas)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }

                if (schema.Name.StartsWith("db_") || !MatchDbObject(schema.Name))
                {
                    OnScriptingProgress("Schema (Will Skip)", schema.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Schema", schema.Name, total, current);

                urn[0] = schema.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptUsers()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.Users.Count;
            int current = 0;

            foreach (User user in db.Users)
            {
                current++;

                if (Cancelled)
                {
                    break;
                }

                if (!MatchDbObject(user.Name))
                {
                    OnScriptingProgress(" User (Will Skip)", user.Name, total, current);
                    continue;
                }

                OnScriptingProgress("User", user.Name, total, current);

                urn[0] = user.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptFullTextCatalogs()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.FullTextCatalogs.Count;
            int current = 0;

            foreach (FullTextCatalog cat in db.FullTextCatalogs)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }

                if (!MatchDbObject(cat.Name))
                {
                    OnScriptingProgress("FullText Catalog (Will Skip)", cat.Name, total, current);
                    continue;
                }

                OnScriptingProgress("FullText Catalog", cat.Name, total, current);

                urn[0] = cat.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }


        private string ScriptDatabaseTriggers()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.Triggers.Count;
            int current = 0;

            foreach (DatabaseDdlTrigger trg in db.Triggers)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }

                if (!MatchDbObject(trg.Name))
                {
                    OnScriptingProgress("Db Trigger (Will Skip)", trg.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Db Trigger", trg.Name, total, current);

                urn[0] = trg.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptTableTriggers()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();
            int total = db.Tables.Count;
            int current = 0;

            foreach (Table tbl in db.Tables)
            {
                current++;

                if (Cancelled)
                {
                    OnScriptingProgress("Triggers for table (Will Skip) ", tbl.Name, total, current);
                    break;
                }

                foreach (Trigger trg in tbl.Triggers)
                {
                    if (Cancelled)
                    {
                        break;
                    }
                    if (!MatchDbObject(trg.Name))
                    {
                        OnScriptingProgress("Triggers for table (Will Skip) ", tbl.Name, total, current);
                        continue;
                    }

                    urn[0] = trg.Urn;
                    string script = CompactStrings(scrp.Script(urn));
                    if (!String.IsNullOrEmpty(script))
                    {
                        sb.Append(script);
                    }
                }

                OnScriptingProgress("Triggers for table ", tbl.Name, total, current);
            }
            return sb.ToString();
        }

        private string ScriptSynonyms()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.Synonyms.Count;
            int current = 0;

            foreach (Synonym syn in db.Synonyms)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }
                if (!MatchDbObject(syn.Name))
                {
                    OnScriptingProgress("Synonym (Will Skip)", syn.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Synonym", syn.Name, total, current);

                urn[0] = syn.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptUserDefinedDataTypes()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.UserDefinedDataTypes.Count;
            int current = 0;

            foreach (UserDefinedDataType udt in db.UserDefinedDataTypes)
            {
                current++;
                if (Cancelled)
                {
                    break;
                }
                if (!MatchDbObject(udt.Name))
                {
                    OnScriptingProgress("User Defined DataType (Will Skip)", udt.Name, total, current);
                    continue;
                }

                OnScriptingProgress("User Defined DataType", udt.Name, total, current);

                urn[0] = udt.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }

        private string ScriptXmlSchemaCollections()
        {
            SFC.Urn[] urn = new SFC.Urn[1];
            StringBuilder sb = new StringBuilder();

            int total = db.XmlSchemaCollections.Count;
            int current = 0;

            foreach (XmlSchemaCollection xmlSchemaCol in db.XmlSchemaCollections)
            {
                if (Cancelled)
                {
                    break;
                }

                if (!MatchDbObject(xmlSchemaCol.Name))
                {
                    OnScriptingProgress("Xml Schema Collection (Will Skip)", xmlSchemaCol.Name, total, current);
                    continue;
                }

                OnScriptingProgress("Xml Schema Collection", xmlSchemaCol.Name, total, current);

                urn[0] = xmlSchemaCol.Urn;

                string script = CompactStrings(scrp.Script(urn));
                if (!String.IsNullOrEmpty(script))
                {
                    sb.Append(script);
                }
            }
            return sb.ToString();
        }


        public IList<Exception> ScriptObjects(out string script)
        {

            scrp.Options.ToFileOnly = false;

            IList<Exception> errors = new List<Exception>();
            script = String.Empty;

            StringBuilder sb = new StringBuilder();
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Database))
            {
                try
                {
                    sb.Append(ScriptDatabase());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.DatabaseTriggers))
            {
                try
                {
                    sb.Append(ScriptDatabaseTriggers());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.FullTextCatalogs))
            {
                try
                {
                    sb.Append(ScriptFullTextCatalogs());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Schemas))
            {
                try
                {
                    sb.Append(ScriptSchemas());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.XmlSchemaCollections))
            {
                try
                {
                    sb.Append(ScriptXmlSchemaCollections());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }


            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.DatabaseRoles))
            {
                try
                {
                    sb.Append(ScriptDatabaseRoles());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.ApplicationRoles))
            {
                try
                {
                    sb.Append(ScripApplicationRoles());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Users))
            {
                try
                {
                    sb.Append(ScriptUsers());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.UserDefinedDataTypes))
            {
                sb.Append(ScriptUserDefinedDataTypes());
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.UserTables))
            {
                try
                {
                    sb.Append(ScriptUserTables());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Views))
            {
                try
                {
                    sb.Append(ScriptViews());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.UserDefinedFunctions))
            {
                try
                {
                    sb.Append(ScriptUserDefinedFunctions());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.StoredProcs))
            {
                try
                {
                    sb.Append(ScriptStoredProcs());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Synonyms))
            {
                try
                {
                    sb.Append(ScriptSynonyms());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.TableTriggers))
            {
                try
                {
                    sb.Append(ScriptTableTriggers());
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            Cancelled = false;
            script = sb.ToString();
            return errors;
        }

        #endregion //Methods

        #region Script objects to file

        private void ScriptObjectToFile(SFC.Urn[] urn, Scripter scrp, string filename)
        {
            scrp.Options.FileName = filename;
            scrp.Script(urn);
        }

        public IList<Exception> ScriptObjectsToFolder(string folderPath)
        {
            scrp.Options.ToFileOnly = true;

            IList<Exception> errors = new List<Exception>();
            string dbPath = String.Empty;
            string srvrPath = folderPath;
            try
            {
                // srvrPath will be our root directory
                while (srvrPath.EndsWith(@"\"))
                {
                    srvrPath = srvrPath.Substring(0, srvrPath.Length - 1);
                }
                srvrPath += @"\" + _connParams.Server.Replace(@"\", @"$").ToUpper();
                if (Directory.Exists(srvrPath) == false)
                {
                    Directory.CreateDirectory(srvrPath);
                }

                dbPath = srvrPath + @"\" + _connParams.Database + @"\Schema Objects";
                // if dbPath already exists, delete it -- we want to start with a clean slate
                if (Directory.Exists(dbPath))
                {
                    Directory.Delete(dbPath, true); // true deletes any files and subdirectories
                }
                // recreate the database directory
                Directory.CreateDirectory(dbPath);
            }
            catch (Exception ex)
            {
                errors.Add(ex);
                return errors;
            }

            string filename;
            SFC.Urn[] urn = new SFC.Urn[1];
            /*******************************************************************************
            *
            * Script the Database
            *
            ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Database))
            {
                urn[0] = db.Urn;
                filename = dbPath + @"\" + db.Name + ".database.sql";

                // script the database
                try
                {
                    ScriptObjectToFile(urn, scrp, filename);
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            /*******************************************************************************
             *
             * Script Tables
             *
             ******************************************************************************/
            int total = 0;
            int current = 0;

            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.UserTables))
            {
                current = 0;
                total = db.Tables.Count;

                string tblPath = dbPath + @"\Tables";
                Directory.CreateDirectory(tblPath);

                foreach (Table tbl in db.Tables)
                {
                    current++;
                    if (Cancelled)
                    {
                        break;
                    }
                    // skip system tables
                    if (tbl.IsSystemObject || !MatchDbObject(tbl.Name))
                    {
                        OnScriptingProgress("Table (Will Skip)", tbl.Name, total, current);
                        continue;
                    }

                    OnScriptingProgress("Table", tbl.Name, total, current);

                    urn[0] = tbl.Urn;

                    scrp.Options.DriAll = false;
                    scrp.Options.Indexes = false;
                    scrp.Options.Triggers = false;
                    scrp.Options.NoFileGroup = false;
                    scrp.Options.NoTablePartitioningSchemes = false;
                    scrp.Options.Permissions = false;
                    scrp.Options.ClusteredIndexes = false;
                    scrp.Options.DriForeignKeys = false;
                    scrp.Options.DriPrimaryKey = false;
                    scrp.Options.DriUniqueKeys = false;

                    //scrp.Options.DriAllKeys
                    filename = tblPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name + ".table.sql";

                    // script the table
                    try
                    {
                        ScriptObjectToFile(urn, scrp, filename);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }

                    /****************************************************************************
                    *
                    * Script Table Indexes
                    *
                    ****************************************************************************/
                    string keyPath = tblPath + @"\Keys";
                    Directory.CreateDirectory(keyPath);

                    string ndxPath = tblPath + @"\Indexes";
                    Directory.CreateDirectory(ndxPath);

                    foreach (Index ndx in tbl.Indexes)
                    {
                        if (Cancelled)
                        {
                            break;
                        }

                        urn[0] = ndx.Urn;

                        if (ndx.IndexKeyType.ToString() == "DriUniqueKey")
                        {
                            filename = keyPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name
                                     + "." + ndx.Name + ".ukey.sql";
                        }
                        else if (ndx.IndexKeyType.ToString() == "DriPrimaryKey")
                        {
                            filename = keyPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name
                                     + "." + ndx.Name + ".pkey.sql";
                        }
                        else
                        {
                            filename = ndxPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name
                                     + "." + ndx.Name + ".index.sql";
                        }

                        // script the index
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }

                    /****************************************************************************
                    *
                    * Script Table Triggers
                    *
                    ****************************************************************************/
                    string trgPath = tblPath + @"\Triggers";
                    Directory.CreateDirectory(trgPath);

                    foreach (Trigger trg in tbl.Triggers)
                    {
                        if (Cancelled)
                        {
                            break;
                        }

                        urn[0] = trg.Urn;

                        filename = trgPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name
                                 + "." + trg.Name + ".trigger.sql";

                        // script the trigger
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }

                    /****************************************************************************
                    *
                    * Script Check Constraints
                    *
                    ****************************************************************************/
                    string chkPath = tblPath + @"\Constraints";
                    Directory.CreateDirectory(chkPath);

                    scrp.Options.DriChecks = true;

                    foreach (Check chk in tbl.Checks)
                    {
                        if (Cancelled)
                        {
                            break;
                        }

                        urn[0] = chk.Urn;
                        filename = chkPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name
                                 + "." + chk.Name + ".chkconst.sql";

                        // script the constraint
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }

                    /****************************************************************************
                    *
                    * Script Default Constraints
                    *
                    ****************************************************************************/
                    string defPath = tblPath + @"\Constraints";
                    Directory.CreateDirectory(chkPath);

                    scrp.Options.DriChecks = false;

                    foreach (Column col in tbl.Columns)
                    {
                        if (Cancelled)
                        {
                            break;
                        }
                        if (col.DefaultConstraint != null)
                        {
                            urn[0] = col.DefaultConstraint.Urn;
                            filename = defPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name
                                     + "." + col.DefaultConstraint.Name + ".defconst.sql";

                            // script the constraint
                            try
                            {
                                ScriptObjectToFile(urn, scrp, filename);
                            }
                            catch (Exception ex)
                            {
                                errors.Add(ex);
                            }
                        }
                    }

                    /****************************************************************************
                    *
                    * Script Foreign Keys
                    *
                    ****************************************************************************/
                    scrp.Options.DriForeignKeys = true;

                    foreach (ForeignKey fk in tbl.ForeignKeys)
                    {
                        if (Cancelled)
                        {
                            break;
                        }

                        urn[0] = fk.Urn;
                        filename = keyPath + @"\" + tbl.Schema.Replace(@"\", @"$") + "." + tbl.Name
                                 + "." + fk.Name + ".fkey.sql";

                        // script the constraint
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Views
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Views))
            {
                string vwPath = dbPath + @"\Views";
                Directory.CreateDirectory(vwPath);

                current = 0;
                total = db.Views.Count;

                foreach (View vw in db.Views)
                {
                    current++;
                    if (Cancelled)
                    {
                        break;
                    }
                    // skip system views
                    if (vw.IsSystemObject || !MatchDbObject(vw.Name))
                    {
                        OnScriptingProgress("View (Will Skip)", vw.Name, total, current);
                        continue;
                    }

                    OnScriptingProgress("View", vw.Name, total, current);
                    urn[0] = vw.Urn;

                    scrp.Options.Indexes = false;
                    scrp.Options.Triggers = false;
                    scrp.Options.Permissions = false;
                    scrp.Options.ClusteredIndexes = false;
                    scrp.Options.DriForeignKeys = false;
                    scrp.Options.DriPrimaryKey = false;
                    scrp.Options.DriUniqueKeys = false;

                    filename = vwPath + @"\" + vw.Schema.Replace(@"\", @"$") + "." + vw.Name + ".view.sql";

                    // script the view
                    ScriptObjectToFile(urn, scrp, filename);

                    /****************************************************************************
                    *
                    * Script View Indexes
                    *
                    ****************************************************************************/
                    string ndxPath = vwPath + @"\Indexes";
                    Directory.CreateDirectory(ndxPath);

                    foreach (Index ndx in vw.Indexes)
                    {
                        if (Cancelled)
                        {
                            break;
                        }

                        urn[0] = ndx.Urn;
                        filename = ndxPath + @"\" + vw.Schema.Replace(@"\", @"$") + "." + vw.Name
                                 + "." + ndx.Name + ".index.sql";

                        // script the index
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }

                    /****************************************************************************
                    *
                    * Script View Triggers
                    *
                    ****************************************************************************/
                    string trgPath = vwPath + @"\Triggers";
                    Directory.CreateDirectory(trgPath);

                    foreach (Trigger trg in vw.Triggers)
                    {
                        current++;
                        if (Cancelled)
                        {
                            break;
                        }

                        urn[0] = trg.Urn;
                        filename = trgPath + @"\" + vw.Schema.Replace(@"\", @"$") + "." + vw.Name
                                 + "." + trg.Name + ".trigger.sql";

                        // script the trigger
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Stored Procedures
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.StoredProcs))
            {
                string procPath = dbPath + @"\Stored Procedures";
                Directory.CreateDirectory(procPath);

                current = 0;
                total = db.StoredProcedures.Count;
                foreach (StoredProcedure proc in db.StoredProcedures)
                {
                    current++;
                    if (Cancelled)
                    {
                        break;
                    }

                    // skip system procedures
                    if (proc.IsSystemObject || !MatchDbObject(proc.Name))
                    {
                        OnScriptingProgress("Stored Proc (Will Skip)", proc.Name, total, current);
                        continue;
                    }

                    OnScriptingProgress("Stored Proc", proc.Name, total, current);
                    urn[0] = proc.Urn;

                    filename = procPath + @"\" + proc.Schema.Replace(@"\", @"$") + "." + proc.Name + ".proc.sql";
                    // script the procedure
                    try
                    {
                        ScriptObjectToFile(urn, scrp, filename);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            /*******************************************************************************
             *
             * Script User Defined Functions
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.UserDefinedFunctions))
            {
                string funcPath = dbPath + @"\Functions";
                Directory.CreateDirectory(funcPath);

                current = 0;
                total = db.UserDefinedFunctions.Count;

                foreach (UserDefinedFunction func in db.UserDefinedFunctions)
                {
                    if (Cancelled)
                    {
                        break;
                    }
                    // skip system functions
                    if (func.IsSystemObject || !MatchDbObject(func.Name))
                    {
                        OnScriptingProgress("Function (Will Skip)", func.Name, total, current);
                        continue;
                    }

                    OnScriptingProgress("Function", func.Name, total, current);
                    urn[0] = func.Urn;

                    filename = funcPath + @"\" + func.Schema.Replace(@"\", @"$") + "." + func.Name + ".function.sql";
                    // script the function
                    try
                    {
                        ScriptObjectToFile(urn, scrp, filename);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Application Roles
             *
             ******************************************************************************/
            string securPath = dbPath + @"\Security";
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.ApplicationRoles))
            {
                Directory.CreateDirectory(securPath);

                string approlePath = securPath + @"\Roles\Application Roles";
                Directory.CreateDirectory(approlePath);

                current = 0;
                total = db.ApplicationRoles.Count;

                foreach (ApplicationRole approle in db.ApplicationRoles)
                {
                    current++;
                    if (Cancelled)
                    {
                        break;
                    }

                    if (!MatchDbObject(approle.Name))
                    {
                        OnScriptingProgress("App Role (Will Skip)", approle.Name, total, current);
                        continue;
                    }

                    OnScriptingProgress("App Role", approle.Name, total, current);

                    urn[0] = approle.Urn;
                    filename = approlePath + @"\" + approle.Name + ".approle.sql";

                    // script the role
                    try
                    {
                        ScriptObjectToFile(urn, scrp, filename);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Database Roles
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.DatabaseRoles))
            {
                string dbrolePath = securPath + @"\Roles\Database Roles";
                Directory.CreateDirectory(dbrolePath);

                current = 0;
                total = db.Roles.Count;

                foreach (DatabaseRole dbrole in db.Roles)
                {
                    current++;
                    if (Cancelled)
                    {
                        break;
                    }

                    // skip fixed database roles
                    if (dbrole.IsFixedRole || !MatchDbObject(dbrole.Name))
                    {
                        OnScriptingProgress("Db Role (Will Skip)", dbrole.Name, total, current);
                        continue;
                    }

                    OnScriptingProgress("Db Role", dbrole.Name, total, current);
                    urn[0] = dbrole.Urn;

                    filename = dbrolePath + @"\" + dbrole.Name + ".role.sql";

                    // script the role
                    try
                    {
                        ScriptObjectToFile(urn, scrp, filename);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Schemas
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Schemas))
            {
                if (srvrVersion.StartsWith("9."))
                {
                    string schemaPath = securPath + @"\Schemas";
                    Directory.CreateDirectory(schemaPath);

                    current = 0;
                    total = db.Schemas.Count;

                    foreach (Schema schema in db.Schemas)
                    {
                        current++;
                        if (Cancelled)
                        {
                            break;
                        }
                        // skip fixed schemas
                        if (schema.Name.StartsWith("db_") || !MatchDbObject(schema.Name))
                        {
                            OnScriptingProgress("Schema (Will Skip)", schema.Name, total, current);
                            continue;
                        }

                        OnScriptingProgress("Schema", schema.Name, total, current);

                        urn[0] = schema.Urn;
                        filename = schemaPath + @"\" + schema.Name.Replace(@"\", @"$") + ".schema.sql";

                        // script the schema
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Users
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Users))
            {
                string userPath = securPath + @"\Users";
                Directory.CreateDirectory(userPath);

                current = 0;
                total = db.Users.Count;

                foreach (User user in db.Users)
                {
                    current++;
                    if (Cancelled)
                    {
                        break;
                    }

                    if (!MatchDbObject(user.Name))
                    {
                        OnScriptingProgress("User (Will Skip)", user.Name, total, current);
                        continue;
                    }

                    urn[0] = user.Urn;

                    OnScriptingProgress("User", user.Name, total, current);
                    filename = userPath + @"\" + user.Name.Replace(@"\", @"$") + ".user.sql";

                    // script the schema
                    try
                    {
                        ScriptObjectToFile(urn, scrp, filename);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Full Text Catalogs
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.FullTextCatalogs))
            {
                if (srvrVersion.StartsWith("9."))
                {
                    string storagePath = dbPath + @"\Storage";
                    Directory.CreateDirectory(storagePath);

                    string catPath = storagePath + @"\Full Text Catalogs";
                    Directory.CreateDirectory(catPath);

                    current = 0;
                    total = db.FullTextCatalogs.Count;

                    foreach (FullTextCatalog cat in db.FullTextCatalogs)
                    {
                        current++;
                        if (Cancelled)
                        {
                            break;
                        }
                        if (!MatchDbObject(cat.Name))
                        {
                            OnScriptingProgress("FullText Catalog  (Will Skip)", cat.Name, total, current);
                            continue;
                        }

                        OnScriptingProgress("FullText Catalog", cat.Name, total, current);
                        urn[0] = cat.Urn;
                        filename = catPath + @"\" + cat.Name + ".fulltext.sql";

                        // script the full text catalog
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Database Triggers
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.DatabaseTriggers))
            {
                if (srvrVersion.StartsWith("9."))
                {
                    string dbtrgPath = dbPath + @"\Database Triggers";
                    Directory.CreateDirectory(dbtrgPath);

                    current = 0;
                    total = db.Triggers.Count;

                    foreach (DatabaseDdlTrigger dbtrg in db.Triggers)
                    {
                        current++;
                        if (Cancelled)
                        {
                            break;
                        }
                        if (!MatchDbObject(dbtrg.Name))
                        {
                            OnScriptingProgress("Db Trigger (Will Skip)", dbtrg.Name, total, current);
                            continue;
                        }

                        OnScriptingProgress("Db Trigger", dbtrg.Name, total, current);
                        urn[0] = dbtrg.Urn;
                        filename = dbtrgPath + @"\" + dbtrg.Name + ".ddltrigger.sql";

                        // script the database trigger
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }
            }
            /*******************************************************************************
             *
             * Script Synonyms
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.Synonyms))
            {
                if (srvrVersion.StartsWith("9."))
                {
                    string synPath = dbPath + @"\Synonyms";
                    Directory.CreateDirectory(synPath);

                    current = 0;
                    total = db.Synonyms.Count;

                    foreach (Synonym syn in db.Synonyms)
                    {
                        current++;
                        if (Cancelled)
                        {
                            break;
                        }

                        if (!MatchDbObject(syn.Name))
                        {
                            OnScriptingProgress("Synonym (Will Skip)", syn.Name, total, current);
                            continue;
                        }

                        OnScriptingProgress("Synonym", syn.Name, total, current);
                        urn[0] = syn.Urn;
                        filename = synPath + @"\" + syn.Schema.Replace(@"\", @"$") + "." + syn.Name + ".synonym.sql";

                        // script the synonym
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }
            }
            /*******************************************************************************
             *
             * Script User-defined Types
             *
             ******************************************************************************/
            string typePath = dbPath + @"\Types";
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.UserDefinedDataTypes))
            {
                typePath = dbPath + @"\Types";
                Directory.CreateDirectory(typePath);

                string uddtPath = typePath + @"\User-defined Data Types";
                Directory.CreateDirectory(uddtPath);

                current = 0;
                total = db.UserDefinedDataTypes.Count;
                foreach (UserDefinedDataType uddt in db.UserDefinedDataTypes)
                {
                    current++;
                    if (Cancelled)
                    {
                        break;
                    }
                    if (!MatchDbObject(uddt.Name))
                    {
                        OnScriptingProgress("User Defined DataType (Will Skip)", uddt.Name, total, current);
                        continue;
                    }

                    OnScriptingProgress("User Defined DataType", uddt.Name, total, current);

                    urn[0] = uddt.Urn;
                    filename = uddtPath + @"\" + uddt.Schema.Replace(@"\", @"$") + "." + uddt.Name + ".uddt.sql";

                    // script the user-defined data type
                    try
                    {
                        ScriptObjectToFile(urn, scrp, filename);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }
            }
            /*******************************************************************************
             *
             * Script XML Schema Collections
             *
             ******************************************************************************/
            if (!Cancelled && _objectTypes.Contains(ScriptObjectTypes.XmlSchemaCollections))
            {
                if (srvrVersion.StartsWith("9."))
                {
                    string xmlPath = typePath + @"\XML Schema Collections";
                    Directory.CreateDirectory(xmlPath);

                    current = 0;
                    total = db.XmlSchemaCollections.Count;

                    foreach (XmlSchemaCollection xml in db.XmlSchemaCollections)
                    {
                        current++;
                        if (Cancelled)
                        {
                            break;
                        }
                        if (!MatchDbObject(xml.Name))
                        {
                            OnScriptingProgress("Xml Schema Collection (Will Skip)", xml.Name, total, current);
                            continue;
                        }

                        OnScriptingProgress("Xml Schema Collection", xml.Name, total, current);

                        urn[0] = xml.Urn;
                        filename = xmlPath + @"\" + xml.Schema.Replace(@"\", @"$") + "." + xml.Name + ".xmlschema.sql";

                        // script the xml schema collection
                        try
                        {
                            ScriptObjectToFile(urn, scrp, filename);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }
            }
            Cancelled = false;
            return errors;
        }

        #endregion

        private bool _isDisposing = false;
        public void Dispose()
        {
            if (_isDisposing)
                return;

            _isDisposing = true;
            if (sqlConn == null)
                return;

            if (sqlConn.InUse)
                sqlConn.Cancel();

            sqlConn.Disconnect();
            sqlConn = null;
        }
    }

    public delegate void ScriptingProgressDelegate(string objType, string objectName, int total, int current);
}
