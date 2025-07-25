using System;
using System.Data.SqlClient;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using PragmaSQL.Core;

namespace PragmaSQL.Scripting.Smo
{
	public enum ScriptObjectType
	{
		None = 0,
		Table,
		View,
		StoredProcedure,
		Function,
		Trigger
	}


	public static class SmoHelpers
	{
		public static ScriptObjectType GetDBObjectType(string typeAbb)
		{
			string lType = typeAbb.ToLowerInvariant().Trim();
			switch (lType)
			{
				case "fn":
				case "tf":
				case "if":
					return ScriptObjectType.Function;
				case "x":
				case "p":
					return ScriptObjectType.StoredProcedure;
				case "tr":
					return ScriptObjectType.Trigger;
				case "v":
					return ScriptObjectType.View;
				case "u":
					return ScriptObjectType.Table;
				default:
					return ScriptObjectType.None;
			}
		}

		public static string FormatExceptionMsg(Exception ex)
		{
			return FormatExceptionMsg(ex, String.Empty);
		}

		public static string FormatExceptionMsg(Exception ex, string extraInfo)
		{
			if (ex == null)
				return String.Empty;

			string template = "->\r\n"+(String.IsNullOrEmpty(extraInfo) ? String.Empty : extraInfo + "\r\n")+"EXCEPTION TYPE: {0}.\r\nINNER EXCEPTION TYPE: {1}\r\nMESSAGE: {2}\r\n";

			string exMsg = ex.Message;
			exMsg += ex.InnerException != null ? (" " + ex.InnerException.Message) : String.Empty;
			return String.Format(template, ex.GetType().Name, (ex.InnerException != null ? ex.InnerException.GetType().Name : String.Empty), exMsg);
		}

		public static Version GetServerVersion(ConnectionParams cp)
		{
			Version result = null;
			var connStr = cp.GetConnectionString(false, false);

			ServerConnection sqlConn = null;
			try
			{
				sqlConn = new ServerConnection(connStr);
				Server srvr = new Server(sqlConn);
				result = srvr.Information.Version;
			}
			finally
			{
				if (sqlConn != null)
				{
					if (sqlConn.InUse)
						sqlConn.Cancel();
					if(sqlConn.IsOpen)
						sqlConn.Disconnect();
				}
			}
			return result;
		}

		public static int GetServerMajorVersion(ConnectionParams cp)
		{
			Version v = GetServerVersion(cp);
			return v == null ? 0 : v.Major;
		}
	}
}
