using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public struct DataSetInfo
	{
		public string ServerName;
		public string DbName;
		public string ServerDbInfo;
		public string BatchNo;
	}
	
  public interface IScriptEditor:ITextEditor
  {
    /// <summary>
    /// Modify database object by name
    /// </summary>
    /// <param name="objName"></param>
    void EditObject( string objName );

    /// <summary>
    /// Add message to Messages 
    /// </summary>
    /// <param name="msgType">Message type</param>
    /// <param name="msg">Message to be added</param>
    void AddMessage( MessageType msgType, string msg );

    /// <summary>
    /// Clear all messages
    /// </summary>
    void ClearMessagesList( );

    /// <summary>
    /// Clear all results
    /// </summary>
    void ClearResults( );

    /// <summary>
    /// Clear results and messages
    /// </summary>
    void ClearOutputPane( );

    /// <summary>
    /// Type of the object in editor
    /// </summary>
    int ObjectType { get;}

    /// <summary>
    /// Current server name
    /// </summary>
    string Server { get;}

    /// <summary>
    /// List of all server names
    /// </summary>
    IList<string> Servers { get;}

    /// <summary>
    /// Current database name
    /// </summary>
    string Database { get;}

    /// <summary>
    /// List of all database names
    /// </summary>
    IList<string> Databases { get;}

    /// <summary>
    /// Datasets
    /// </summary>
    IList<ScriptExecutionResult> ScriptExecutionResults { get;}

    /// <summary>
    /// Returns DataTable bound to the current result renderer view
    /// </summary>
		DataView ActiveDataView { get; }
		
    /// <summary>
    /// Current connetction string
    /// </summary>
    string ConnectionString { get; }


    /// <summary>
    /// Gets database object info for the specified object from 
    /// the database specified by the current script editor connection.
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    ObjectInfo GetObjectInfo( string objName );

    /// <summary>
    /// Gets database object info for object under caret in script editor from 
    /// the database specified by the current script editor connection.
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    ObjectInfo GetObjectInfoForWordAtCursor( );

		DataSetInfo GetDataSetInfo(DataSet dataSet);

		void PrepareAddInSupportForResultContextMenu(ToolStripItemCollection parent);
	
		/// <summary>
		/// Gets the object name opened in the editor
		/// </summary>
		/// <returns></returns>
		//string GetEditorObjectName();
    IList<BatchInfo> Batches { get;}
    IList<string> ObjectNames { get;}
		string ObjectName { get; }
		long ObjectId { get; }
		string Username { get;}
    string IntegratedSecurity { get;}
    ConnectionParams CurrentConnection { get;}

    #region Event Definitions

    event ScriptExecutedDelegate ScriptExecuted;
    event ScriptExecutingDelegate ScriptExecuting;


    /// <summary>
    /// Fired before user hits F1 and requests help for selected text or 
    /// word under caret.
    /// </summary>
    event BeforeHelpRequestedDelegate BeforeHelpRequested;

    /// <summary>
    /// Fired after user hits F1 and requests help for selected text or 
    /// word under caret.
    /// </summary>
    event AfterHelpRequestedDelegate AfterHelpRequested;




    #endregion //Event Definitions


  }
}
