using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace PragmaSQL.Core
{
	public class SqlMessage
	{
		#region Instance Fields

		public string Message = String.Empty;
		public MessageType MsgType = MessageType.None;
		public int Line = -1;
		public int State = -1;
		public int Type = -1;
		public DateTime MsgDateTime = DateTime.Now;
		
		public string Server = String.Empty;
		public string Database = String.Empty;
		
		#endregion //Instance Fields

		#region Static Methods

		#region Base Message Factory Methods

		public static SqlMessage CreateMessage(MessageType msgType, string msgText, string server, string db, int line, int type, int state)
		{
			SqlMessage result = new SqlMessage();

			string tmpMessage = msgText.Replace("\n", String.Empty);
			tmpMessage = tmpMessage.Replace("\r", String.Empty);
			tmpMessage = tmpMessage.Replace("\t", " ");

			result.Message = tmpMessage;
			result.MsgType = msgType;
			result.Line = line;
			result.Type = type;
			result.State = state;
			result.Server = server;
			result.Database = db;

			return result;
		}

		public static SqlMessage CreateMessage(MessageType msgType, string msgText, int line, int type, int state)
		{
			return CreateMessage(msgType, msgText, String.Empty, String.Empty, line, type, state);
		}

		public static SqlMessage CreateMessage(string msgText)
		{
			return CreateMessage(MessageType.None, msgText, -1, -1, -1);
		}

		public static SqlMessage CreateMessage(string msgText,SqlConnection conn)
		{
			string serverName = conn != null ? conn.DataSource : String.Empty;
			string dbName = conn != null ? conn.Database: String.Empty;

			return CreateMessage(MessageType.None, msgText,serverName,dbName, -1, -1, -1);
		}

		public static SqlMessage CreateMessage(string msgText, string server, string db)
		{
			return CreateMessage(MessageType.None, msgText, server, db, -1, -1, -1);		
		}

		#endregion //Base Message Factory Methods

		#region Error Message Factory Methods

		public static SqlMessage CreateErrorMessage(string errorMsg, int line, int type, int state)
		{
			return CreateMessage(MessageType.Error, errorMsg, line, type, state);
		}

		public static SqlMessage CreateErrorMessage(string errorMsg)
		{
			return CreateMessage(MessageType.Error, errorMsg, -1, -1, -1);
		}

		public static SqlMessage CreateErrorMessage(string errorMsg, string server, string db, int line, int type, int state)
		{
			return CreateMessage(MessageType.Error, errorMsg,server,db, line, type, state);
		}

		public static SqlMessage CreateErrorMessage(string errorMsg,string server, string db)
		{
			return CreateMessage(MessageType.Error, errorMsg,server,db, -1, -1, -1);
		}

		public static SqlMessage CreateErrorMessage(string errorMsg, SqlConnection conn, int line, int type, int state)
		{
			string server = conn != null ? conn.DataSource : String.Empty;
			string db = conn != null ? conn.Database : String.Empty;

			return CreateMessage(MessageType.Error, errorMsg, server, db, line, type, state);
		}

		public static SqlMessage CreateErrorMessage(string errorMsg, SqlConnection conn)
		{
			string server = conn != null ? conn.DataSource : String.Empty;
			string db = conn != null ? conn.Database : String.Empty;
			
			return CreateMessage(MessageType.Error, errorMsg, server, db, -1, -1, -1);
		}

		#endregion //Error Message Factory Methods

		#region Info Message Factory Methods

		public static SqlMessage CreateInfoMessage(string infoMsg, int line, int state)
		{
			return CreateMessage(MessageType.Info, infoMsg, line, -1, state);
		}

		public static SqlMessage CreateInfoMessage(string infoMsg)
		{
			return CreateMessage(MessageType.Info, infoMsg, -1, -1, -1);
		}

		public static SqlMessage CreateInfoMessage(string infoMsg, string server, string db, int line, int state)
		{
			return CreateMessage(MessageType.Info, infoMsg, server,db, line, -1, state);
		}

		public static SqlMessage CreateInfoMessage(string infoMsg, SqlConnection conn, int line, int state)
		{
			string server = conn != null ? conn.DataSource : String.Empty;
			string db = conn != null ? conn.Database : String.Empty;
			return CreateMessage(MessageType.Info, infoMsg, server, db, line, -1, state);
		}

		public static SqlMessage CreateInfoMessage(string infoMsg,string server, string db)
		{
			return CreateMessage(MessageType.Info, infoMsg,server, db, -1, -1, -1);
		}

		public static SqlMessage CreateInfoMessage(string infoMsg, SqlConnection conn)
		{
			string server = conn != null ? conn.DataSource : String.Empty;
			string db = conn != null ? conn.Database : String.Empty;
			return CreateMessage(MessageType.Info, infoMsg, server, db, -1, -1, -1);
		}

		#endregion Info Message Factory Methods

		#region Warning Message Factory Methods

		public static SqlMessage CreateWarningMessage(string warningMsg)
		{
			return CreateMessage(MessageType.Warning, warningMsg, -1, -1, -1);
		}

		public static SqlMessage CreateWarningMessage(string warningMsg,string server, string db)
		{
			return CreateMessage(MessageType.Warning, warningMsg,server, db, -1, -1, -1);
		}

		public static SqlMessage CreateWarningMessage(string warningMsg, SqlConnection conn)
		{
			string server = conn != null ? conn.DataSource : String.Empty;
			string db = conn != null ? conn.Database : String.Empty;
			return CreateMessage(MessageType.Warning, warningMsg, server, db, -1, -1, -1);
		}

		#endregion Warning Message Factory Methods

		#region None Message
		public static SqlMessage CreateBoldMessage(string msg)
		{
			return CreateMessage(MessageType.Bold, msg, -1, -1, -1);
		}

		public static SqlMessage CreateBoldMessage(string msg, string server, string db)
		{
			return CreateMessage(MessageType.Bold, msg, server, db, -1, -1, -1);
		}

		public static SqlMessage CreateBoldMessage(string msg, SqlConnection conn)
		{
			string server = conn != null ? conn.DataSource : String.Empty;
			string db = conn != null ? conn.Database : String.Empty;
			return CreateMessage(MessageType.Bold, msg, server, db, -1, -1, -1);
		}

		#endregion //None Message


		#endregion //Static Methods
	}
}
