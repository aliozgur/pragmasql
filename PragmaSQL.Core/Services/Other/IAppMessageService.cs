using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public interface IAppMessageService
  {
    ListView ListView { get;}
    ApplicationMessage Create( MessageType msgType, string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName );

    ApplicationMessage DebugMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName );
    ApplicationMessage DebugMsg( string msgText );
    ApplicationMessage DebugMsg( string msg, MethodBase methodInfo );
    ApplicationMessage DebugMsg( Exception ex, MethodBase methodInfo );

    ApplicationMessage ErrorMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName );
    ApplicationMessage ErrorMsg( string msgText);
		ApplicationMessage ErrorMsg(string msg, MethodBase methodInfo);
		ApplicationMessage ErrorMsg(Exception ex, MethodBase methodInfo);
    
    ApplicationMessage WarningMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName );
    ApplicationMessage WarningMsg( string msgText);
		ApplicationMessage WarningMsg(string msg, MethodBase methodInfo);
    
    ApplicationMessage InfoMsg( string msgText, string methodName, string methodDeclaringTypeName, string methodAssemblyName );
    ApplicationMessage InfoMsg( string msgText );
		ApplicationMessage InfoMsg(string msg, MethodBase methodInfo);

		ApplicationMessage AddMessage(MessageType type, string msg, MethodBase methodInfo);
    void AddMessage( ApplicationMessage msg );
    void ShowMessages( );
		void ClearMessages( );

    void LogDebug( string msg);
		void LogDebug(string msg, MethodBase methodInfo);

    void LogError( string msg );
		void LogError(string msg, MethodBase methodInfo);
    
    void LogInfo( string msg );
		void LogInfo(string msg, MethodBase methodInfo);

    void LogWarning( string msg );
		void LogWarning(string msg, MethodBase methodInfo);

    void LogMessage( MessageType type, string msg );
		void LogMessage(MessageType type, string msg, MethodBase methodInfo);
  }
}
