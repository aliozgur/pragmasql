using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using System.Text.RegularExpressions;

using PragmaSQL.Core;

/*
 Single-line, multiline comment regex: (\/\*(\s*|.*?)*\*\/)|(--.*)
 
 
 */
namespace PragmaSQL.Core
{
    public struct BatchInfo
    {
        public int LineCount;
        public string Content;

        public BatchInfo(string content, int lineCnt)
        {
            LineCount = lineCnt;
            Content = content;
        }
    }

    public class ScriptingHelper
    {
        #region Command Constants
        public const string cmdCreateProcedure = "createprocedure";

        public const string cmdAlterProcedure = "alterprocedure";
        public const string cmdCreateFunction = "createfunction";
        public const string cmdAlterFunction = "alterfunction";
        public const string cmdCreateView = "createview";
        public const string cmdAlterView = "alterview";
        public const string cmdCreateTrigger = "createtrigger";
        public const string cmdAlterTrigger = "altertrigger";

        #endregion //Command Constants

        #region Static variables

        public static string BatchSeperator = "go";

        #endregion // Static variables

        /// <summary>
        /// Determine if input character is not seperator or control character
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsValidChar(char value)
        {
            if (Char.IsSeparator(value) || Char.IsControl(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Gets create script of a stored proc, function, view or triggert
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public static string GetObjectCreateScript(SqlConnection conn, long objectId)
        {
            StringBuilder sb = new StringBuilder();

            if (conn == null)
                throw new Exception("Database connection object null.");

            if (conn.State != ConnectionState.Open)
            {
                throw new InvalidConnectionState(String.Format("Invalid database connection state. {0}", conn.State));
            }

            SqlCommand cmd = new SqlCommand(ResManager.GetDBScript("Script_GetObjectCreateScript"), conn);
            cmd.CommandTimeout = 0;
            SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.Int);
            param.Value = objectId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sb.Append((string)reader["text"]);
            }
            reader.Close();

            string result = sb.ToString();
            result = result.TrimEnd('\r', '\n');
            return result;
        }

        public static IList<BatchInfo> SplitBatches(string source)
        {
            IList<BatchInfo> batches = new List<BatchInfo>();

            string tmpSrc = source.Replace("\n\r", "\r");
            tmpSrc = tmpSrc.Replace("\r\n", "\r");
            tmpSrc = tmpSrc.Replace("\n", "\r");

            string[] lines = tmpSrc.Split('\r');
            string tmp = String.Empty;
            bool isComment = false;
            int lineCnt = 0;
            foreach (string lineText in lines)
            {
                lineCnt++;
                int pos1 = lineText.IndexOf("--");
                int pos2 = lineText.IndexOf("/*");


                if (lineText.Contains("--") && (pos2 == -1 || (pos2 != -1 && pos1 < pos2)))
                {
                    int posx = lineText.IndexOf("--");
                    if (posx > 0 && lineText.Substring(0, posx).Trim().ToLowerInvariant() == BatchSeperator)
                    {
                        batches.Add(new BatchInfo(tmp.TrimEnd('\r', '\n'), lineCnt));
                        lineCnt = 0;
                        tmp = lineText.Remove(0, posx) + "\r\n";
                    }
                    else
                    {
                        tmp += lineText + "\r\n";
                    }
                }
                else if (lineText.Contains("/*"))
                {
                    int posx = lineText.IndexOf("/*");
                    if (posx > 0 && lineText.Substring(0, posx).Trim().ToLowerInvariant() == BatchSeperator)
                    {
                        batches.Add(new BatchInfo(tmp.TrimEnd('\r', '\n'), lineCnt));
                        lineCnt = 0;
                        tmp = lineText.Remove(0, posx) + "\r\n";
                    }
                    else
                    {
                        tmp += lineText + "\r\n";
                    }

                    if (lineText.Contains("*/"))
                    {
                        isComment = false;
                    }
                    else
                    {
                        isComment = true;
                    }
                }
                else if (lineText.Contains("*/"))
                {
                    int posx = lineText.IndexOf("*/");
                    if (posx > 0 && lineText.Remove(0, posx).Trim().ToLowerInvariant() == BatchSeperator)
                    {
                        batches.Add(new BatchInfo(tmp + lineText.Substring(0, posx) + "\r\n", lineCnt));
                        lineCnt = 0;
                        tmp = String.Empty;
                    }
                    else
                    {
                        tmp += lineText + "\r\n";
                    }
                    isComment = false;
                }
                else if (lineText.Trim().ToLowerInvariant() == BatchSeperator.ToLowerInvariant() && !isComment)
                {
                    batches.Add(new BatchInfo(tmp.TrimEnd('\r', '\n'), lineCnt));
                    lineCnt = 0;
                    tmp = String.Empty;
                }
                else
                {
                    tmp += lineText + "\r\n";
                }
            }

            if (!String.IsNullOrEmpty(tmp.Trim()))
            {
                batches.Add(new BatchInfo(tmp.TrimEnd('\r', '\n'), lineCnt));
            }
            return batches;
        }

        /// <summary>
        /// Try to replace procedure, function, view and trigger script
        /// create portion with alter in a single source line
        /// </summary>
        /// <param name="ObjectType">DBObjectType</param>
        /// <param name="sourceLine">Source line</param>
        /// <param name="result">Replaced line text </param>
        /// <returns></returns>
        private static bool TryToReplaceCreateWithAlter(int ObjectType, string sourceLine, ref string result)
        {
            string createCommand = String.Empty;
            string alterCommand = String.Empty;
            result = sourceLine;

            DetermineCommandConstants(ObjectType, ref createCommand, ref alterCommand);
            if (String.IsNullOrEmpty(createCommand) || String.IsNullOrEmpty(alterCommand))
            {

                throw new ObjectTypeNotSupportedByOperation(String.Format("Object type \"{0}\"is not supported by this operation.", DBObjectType.GetNameOfType(ObjectType)));
            }

            int x = 0;
            string command = String.Empty;
            bool isCreateCommand = false;
            bool isCreate = false;

            int createEndPos = -1;
            while (x < sourceLine.Length)
            {
                if (IsValidChar(sourceLine[x]))
                {
                    command += sourceLine[x];
                }
                else if (!String.IsNullOrEmpty(command))
                {
                    if (!isCreate)
                    {
                        isCreate = (command.Trim().ToLowerInvariant() == "create");
                        if (isCreate)
                        {
                            createEndPos = x;
                        }
                    }

                    isCreateCommand = (command.Trim().ToLowerInvariant() == createCommand);
                    if (isCreateCommand)
                    {
                        command = String.Empty;
                        break;
                    }
                    else if (command[command.Length - 1] == ' ')
                    {
                        command += " ";
                    }
                }
                x++;
            }

            if (isCreateCommand || (command.Trim().ToLowerInvariant() == createCommand))
            {
                string tmp = sourceLine.Substring(0, createEndPos - 6);
                if (tmp.Length > 0 && !Char.IsSeparator(tmp[tmp.Length - 1]))
                {
                    tmp += " ";
                }
                tmp += alterCommand;
                tmp += sourceLine.Substring(x, sourceLine.Length - x);
                result = tmp;
                isCreateCommand = true;
            }

            return isCreateCommand;
        }

        /// <summary>
        /// Determine command constant (create/alter) for the specified object type
        /// </summary>
        /// <param name="ObjectType">DBObjectType</param>
        /// <param name="createCommand">create command</param>
        /// <param name="alterCommand">alter command</param>
        private static void DetermineCommandConstants(int ObjectType, ref string createCommand, ref string alterCommand)
        {
            switch (ObjectType)
            {
                case DBObjectType.StoredProc:
                    createCommand = cmdCreateProcedure;
                    alterCommand = "ALTER PROCEDURE";
                    break;
                case DBObjectType.View:
                    createCommand = cmdCreateView;
                    alterCommand = "ALTER VIEW";
                    break;
                case DBObjectType.Trigger:
                    createCommand = cmdCreateTrigger;
                    alterCommand = "ALTER TRIGGER";
                    break;
                case DBObjectType.TableValuedFunction:
                    createCommand = cmdCreateFunction;
                    alterCommand = "ALTER FUNCTION";
                    break;
                case DBObjectType.ScalarValuedFunction:
                    createCommand = cmdCreateFunction;
                    alterCommand = "ALTER FUNCTION";
                    break;
                default:
                    createCommand = String.Empty;
                    alterCommand = String.Empty;
                    break;
            }
        }

        /// <summary>
        /// Replace create [DBObjectType] as alter [DBObjectType]
        /// </summary>
        /// <param name="ObjectType">DBObjectType</param>
        /// <param name="source">source script</param>
        /// <returns>replaced source </returns>
        public static string ReplaceCreateWithAlter(int ObjectType, string source)
        {
            string tmpSrc = source.Replace("\n\r", "\r");
            tmpSrc = tmpSrc.Replace("\r\n", "\r");
            tmpSrc = tmpSrc.Replace("\n", "\r");

            string[] lines = tmpSrc.Split('\r');
            for (int i = 0; i < lines.Length; i++)
            {
                string lineText = lines[i];
                int pos1 = lineText.IndexOf("--");
                int pos2 = lineText.IndexOf("/*");

                if (lineText.Contains("--") && (pos2 == -1 || (pos2 != -1 && pos1 < pos2)))
                {
                    int posx = lineText.IndexOf("--");
                    if (!String.IsNullOrEmpty(lineText.Substring(0, posx).Trim()))
                    {
                        string modText = String.Empty;
                        if (TryToReplaceCreateWithAlter(ObjectType, lineText, ref modText))
                        {
                            lines[i] = modText;
                            break;
                        }
                    }
                }
                else if (lineText.Contains("/*"))
                {
                    int posx = lineText.IndexOf("/*");
                    if (!String.IsNullOrEmpty(lineText.Substring(0, posx).Trim()))
                    {
                        string modText = String.Empty;
                        if (TryToReplaceCreateWithAlter(ObjectType, lineText, ref modText))
                        {
                            lines[i] = modText;
                            break;
                        }
                    }
                }
                else if (lineText.Contains("*/"))
                {
                    int posx = lineText.IndexOf("*/");
                    if (!String.IsNullOrEmpty(lineText.Remove(0, posx).Trim()))
                    {
                        string modText = String.Empty;
                        if (TryToReplaceCreateWithAlter(ObjectType, lineText, ref modText))
                        {
                            lines[i] = modText;
                            break;
                        }
                    }
                }
                else
                {
                    string modText = String.Empty;
                    if (TryToReplaceCreateWithAlter(ObjectType, lineText, ref modText))
                    {
                        lines[i] = modText;
                        break;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (string lineText in lines)
            {
                sb.AppendLine(lineText);
            }

            string result = sb.ToString();
            result = result.TrimEnd('\r', '\n');
            return result;
            //return sb.ToString().Replace("\n\r", String.Empty);
        }

        /// <summary>
        /// Determine if cmdText is one of the accepted commands
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="ObjectType"></param>
        /// <param name="isAlter"></param>
        /// <returns></returns>
        private static bool IsCommand(string cmdText, ref int ObjectType, ref bool isAlter)
        {
            ObjectType = DBObjectType.None;
            isAlter = false;

            string intext = cmdText.ToLowerInvariant();

            if (intext.Equals(cmdCreateProcedure))
            {
                isAlter = false;
                ObjectType = DBObjectType.StoredProc;
                return true;
            }

            if (intext.Equals(cmdAlterProcedure))
            {
                isAlter = true;
                ObjectType = DBObjectType.StoredProc;
                return true;
            }

            if (intext.Equals(cmdCreateFunction))
            {
                isAlter = false;
                ObjectType = DBObjectType.Function;
                return true;
            }

            if (intext.Equals(cmdAlterFunction))
            {
                isAlter = true;
                ObjectType = DBObjectType.Function;
                return true;
            }

            if (intext.Equals(cmdCreateView))
            {
                isAlter = false;
                ObjectType = DBObjectType.View;
                return true;
            }

            if (intext.Equals(cmdAlterView))
            {
                isAlter = true;
                ObjectType = DBObjectType.View;
                return true;
            }

            if (intext.Equals(cmdCreateTrigger))
            {
                isAlter = false;
                ObjectType = DBObjectType.Trigger;
                return true;
            }

            if (intext.Equals(cmdAlterTrigger))
            {
                isAlter = true;
                ObjectType = DBObjectType.Trigger;
                return true;
            }

            return false;
        }

        private static string TryToGetObjectNameFromScript(string source, ref int ObjectType, ref bool isAlter)
        {
            bool commandFound = false;
            string result = String.Empty;

            ObjectType = DBObjectType.None;
            isAlter = false;

            int x = 0;
            bool hasEnclosingBracket = false;
            bool metFirstNonSpace = false;
            string command = String.Empty;
            while (x < source.Length)
            {
                if (!commandFound)
                {
                    if (!IsValidChar(source[x]) || (source[x] == '[' || source[x] == ']'))
                    {
                        x++;
                        continue;
                    }

                    command += source[x];
                    if (IsCommand(command, ref ObjectType, ref isAlter))
                    {
                        command = String.Empty;
                        commandFound = true;
                    }
                }
                else
                {
                    if (!metFirstNonSpace && Char.GetUnicodeCategory(source[x]) == UnicodeCategory.SpaceSeparator)
                    {
                        x++;
                        continue;
                    }
                    metFirstNonSpace = true;
                    if (source[x] == '.')
                    {
                        result = String.Empty;
                    }
                    else if (source[x] == '[' || source[x] == ']')
                    {
                        if (source[x] == '[')
                        {
                            hasEnclosingBracket = true;
                        }
                        else
                        {
                            hasEnclosingBracket = false;
                        }
                        x++;
                        continue;
                    }
                    else if (!hasEnclosingBracket && (source[x] == '(' || Char.GetUnicodeCategory(source[x]) == UnicodeCategory.SpaceSeparator))
                    {
                        result = result.Trim();
                        break;
                    }
                    else
                    {
                        result += source[x];
                    }
                }
                x++;
            }

            return result;
        }

        public static string GetObjectNameFromScript(string source, ref int ObjectType, ref bool isAlter)
        {
            string result = String.Empty;
            ObjectType = DBObjectType.None;
            isAlter = false;

            string tmpSrc = source.Replace("\n\r", "\r");
            tmpSrc = tmpSrc.Replace("\r\n", "\r");
            tmpSrc = tmpSrc.Replace("\n", "\r");


            string[] lines = tmpSrc.Split('\r');

            for (int i = 0; i < lines.Length; i++)
            {
                string lineText = lines[i];
                int pos1 = lineText.IndexOf("--");
                int pos2 = lineText.IndexOf("/*");

                if (lineText.Contains("--") && (pos2 == -1 || (pos2 != -1 && pos1 < pos2)))
                {
                    int posx = lineText.IndexOf("--");
                    if (!String.IsNullOrEmpty(lineText.Substring(0, posx).Trim()))
                    {
                        result = TryToGetObjectNameFromScript(lineText, ref ObjectType, ref isAlter);
                        if (!String.IsNullOrEmpty(result.Trim()))
                        {
                            break;
                        }
                    }
                }
                else if (lineText.Contains("/*"))
                {
                    int posx = lineText.IndexOf("/*");
                    if (!String.IsNullOrEmpty(lineText.Substring(0, posx).Trim()))
                    {
                        result = TryToGetObjectNameFromScript(lineText, ref ObjectType, ref isAlter);
                        if (!String.IsNullOrEmpty(result.Trim()))
                        {
                            break;
                        }
                    }
                }
                else if (lineText.Contains("*/"))
                {
                    int posx = lineText.IndexOf("*/");
                    if (!String.IsNullOrEmpty(lineText.Remove(0, posx).Trim()))
                    {
                        result = TryToGetObjectNameFromScript(lineText, ref ObjectType, ref isAlter);
                        if (!String.IsNullOrEmpty(result.Trim()))
                        {
                            break;
                        }
                    }
                }
                else
                {
                    result = TryToGetObjectNameFromScript(lineText, ref ObjectType, ref isAlter);
                    if (!String.IsNullOrEmpty(result.Trim()))
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public static string GetAlterScript(SqlConnection conn, long objid, int objType)
        {
            if (conn == null)
            {
                throw new NullParameterException("Connection is null!");
            }
            return GetAlterScript(conn.ConnectionString, conn.Database, objid, objType);
        }

        public static string GetAlterScript(ConnectionParams cp, long objid, int objType)
        {
            if (cp == null)
            {
                throw new NullParameterException("Connection is null!");
            }
            return GetAlterScript(cp.ConnectionString, cp.Database, objid, objType);
        }

        public static string GetAlterScript(ConnectionParams connParams, string database, long objid, int objType)
        {
            if (connParams == null)
            {
                throw new NullParameterException("Connection params is null!");
            }
            return GetAlterScript(connParams.ConnectionString, database, objid, objType);
        }

        public static string GetAlterScript(string connectionString, string database, long objid, int objType)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new NullParameterException("Connection string is null or empty!");
            }


            string script = String.Empty;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (!String.IsNullOrEmpty(database))
                {
                    conn.ChangeDatabase(database);
                }
                script = ScriptingHelper.GetObjectCreateScript(conn, objid);
                script = ScriptingHelper.ReplaceCreateWithAlter(objType, script);
            }
            return script;
        }

        public static IList<string> GetVariables(string script)
        {
            // Datatyped variable search regular expression:
            // \100+\w+\s* (bigint|binary\s*\(\s*\d+\s*\)|bit|char\s*\(\s*\d+\s*\)|datetime|decimal\s*\(\s*\d+\s*\)|decimal\s*\(\s*\d+\s*,\s*\d+\s*\)|float|image|int|money|nchar\s*\(\s*\d+\s*\)|ntext|numeric\s*\(\s*\d+\s*\)|numeric\s*\(\s*\d+\s*,\s*\d+\s*\)|nvarchar\s*\(\s*\d+\s*\)|real|smalldatetime|smallint|smallmoney|sql_variant|text|timestamp|tinyint|uniqueidentifier|varbinary\(\s*\d+\s*\)|varchar\(\s*\d+\s*\)|xml)

            IList<string> result = new List<string>();
            string s = script;
            string pat = @"\100+\w+";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m;

            for (m = r.Match(s); m.Success; m = m.NextMatch())
            {
                if (!result.Contains(m.Value))
                    result.Add(m.Value);

            }
            return result;
        }

        public static IList<string> GetVariablesWithDataTypes(string script)
        {
            // Datatyped variable search regular expression:

            IList<string> result = new List<string>();
            string s = script;
            string pat = @"\100+\w+\s*"
              + @"(bigint|binary\s*\(\s*\d+\s*\)|bit|char\s*\(\s*\d+\s*\)|datetime|"
              + @"decimal\s*\(\s*\d+\s*\)|decimal\s*\(\s*\d+\s*,\s*\d+\s*\)|float|image|"
              + @"int|money|nchar\s*\(\s*\d+\s*\)|ntext|numeric\s*\(\s*\d+\s*\)|"
              + @"numeric\s*\(\s*\d+\s*,\s*\d+\s*\)|nvarchar\s*\(\s*\d+\s*\)|real|"
              + @"smalldatetime|smallint|smallmoney|sql_variant|text|timestamp|tinyint|"
              + @"uniqueidentifier|varbinary\(\s*\d+\s*\)|varchar\(\s*\d+\s*\)|xml)";

            Regex r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m;

            for (m = r.Match(s); m.Success; m = m.NextMatch())
            {
                if (!result.Contains(m.Value))
                    result.Add(m.Value);

            }
            return result;
        }

        public static string GenerateDropScript(ConnectionParams cp, string name, int type)
        {
            return GenerateDropScript(cp, name, type, true, true);
        }

        public static string GenerateDropScript(ConnectionParams cp, string name, int type, bool seperateBatches, bool generateIfExists)
        {
            string ifPart = String.Empty;
            string dropCommand = String.Empty;
            string comment = "/*** Drop script generated with PragmaSQL on " + DateTime.Now.ToString() + " by [" + cp.CurrentUsername + "] ***/ \n";
            string result = String.Empty;

            switch (type)
            {
                case DBObjectType.UserTable:
                    return GenerateTableDropScript(cp, name, seperateBatches, generateIfExists);
                case DBObjectType.StoredProc:
                    ifPart = String.Format(@"IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'{0}') AND type in (N'P', N'PC'))", name);
                    dropCommand = String.Format(@"DROP PROCEDURE {0}", name);
                    break;
                case DBObjectType.TableValuedFunction:
                //Fall to next case
                case DBObjectType.ScalarValuedFunction:
                    ifPart = String.Format(@"IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'{0}') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))", name);
                    dropCommand = String.Format(@"DROP FUNCTION {0}", name);
                    break;
                case DBObjectType.View:
                    ifPart = String.Format(@"IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'{0}') AND type = 'V')", name);
                    dropCommand = String.Format(@"DROP VIEW {0}", name);
                    break;
                case DBObjectType.Trigger:
                    ifPart = String.Format(@"IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'{0}') AND type = 'TR')", name);
                    dropCommand = String.Format(@"DROP TRIGGER {0}", name);
                    break;
                case DBObjectType.Synonym:
                    ifPart = String.Format(@"IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'{0}') AND type = 'SN')", name);
                    dropCommand = String.Format(@"DROP SYNONYM {0}", name);
                    break;
                default:
                    return result;
            }

            return comment
              + (!String.IsNullOrEmpty(cp.Database) ? String.Format("USE[{0}]\n", cp.Database) : String.Empty)
              + (generateIfExists ? ifPart : String.Empty)
              + "\n" + dropCommand;
        }

        public static string GenerateTableDropScript(ConnectionParams cp, string tblName, bool seperateBatches, bool generateIfExists)
        {
            IList<string> fkeys = GetForeignKeys(cp, tblName);
            string dropKeysPart = String.Empty;
            string dropTblPart = String.Empty;
            string usePart = (!String.IsNullOrEmpty(cp.Database) ? String.Format("USE[{0}]\n", cp.Database) : String.Empty);
            string comment = "/*** Drop script generated with PragmaSQL on " + DateTime.Now.ToString() + " by [" + cp.CurrentUsername + "] ***/ \n";
            string tmp = String.Empty;

            string tmpFkExist = @"IF  EXISTS (SELECT * FROM sysforeignkeys WHERE constid = OBJECT_ID(N'{0}') AND parent_obj = OBJECT_ID(N'[{1}]'))" + "\n";
            string tmpDropConstraint = @"ALTER TABLE {1} DROP CONSTRAINT [{0}]" + "\n";
            string tmpTableExist = @"IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'{0}') AND type in (N'U'))" + "\n";
            string tmpDropTable = @"DROP TABLE {0}" + "\n";

            // 1- Drop foregin keys part
            foreach (string fk in fkeys)
            {
                tmp = (generateIfExists ? tmpFkExist : String.Empty)
                  + tmpDropConstraint
                  + (seperateBatches ? "GO\n" : String.Empty);
                tmp = String.Format(tmp, fk, tblName);
                dropKeysPart += tmp;
            }

            if (fkeys.Count > 0)
            {
                dropKeysPart = usePart
                  + (seperateBatches ? "\nGO\n" : String.Empty)
                  + dropKeysPart;
            }

            // 2- Drop table part
            tmp = (generateIfExists ? tmpTableExist : String.Empty) + tmpDropTable;
            tmp = String.Format(tmp, tblName);
            dropTblPart = usePart
              + (seperateBatches ? "\nGO\n" : String.Empty)
              + tmp;

            return comment + dropKeysPart + dropTblPart;
        }

        public static IList<string> GetForeignKeys(ConnectionParams cp, string tblName)
        {
            IList<string> result = new List<string>();
            using (SqlConnection conn = new SqlConnection(cp.ConnectionString))
            {
                conn.Open();
                string cmdText = "sp_fkeys @fktable_name = N'{0}'";
                cmdText = String.Format(cmdText, tblName);
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandTimeout = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        result.Add((string)reader["FK_NAME"]);
                    }
                }
                finally
                {

                    reader.Close();
                }
            }
            return result;
        }

    }
}
