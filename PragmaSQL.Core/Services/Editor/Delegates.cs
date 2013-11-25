/********************************************************************
  Class Delegates
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

namespace PragmaSQL.Core
{
  public class FileOperationEventArgs : EventArgs
  {
    public string FileName = String.Empty;  
  }
  
  public class BeforeOpenedFileEventArgs : FileOperationEventArgs{}

  public class BeforeCodeCompletionShowedEventArgs : EventArgs
  {
    public string RequestedFor = String.Empty;
    public CodeCompletionType Type = CodeCompletionType.DatabaseObjectList;

    public override string ToString( )
    {
      return "Type: " + Type.ToString() + "\r\n"
        + "Requested For: " + RequestedFor;
    }
  }

  public class AfterCodeCompletionShowedEventArgs : EventArgs
  {
    public CodeCompletionType Type = CodeCompletionType.DatabaseObjectList;
    public string UserSelection = String.Empty;
    public bool UserMadeSelection = false;
    public override string ToString( )
    {
      return "Type: " + Type.ToString() + "\r\n"
        + "User Made Selection: " + UserMadeSelection.ToString() + "\r\n"
        + "User Selection: " + UserSelection;
    }
  }

  public class HelpRequestedEventArgs : EventArgs
  {
    public string RequestedFor = String.Empty;
    public bool Cancel = false;
    public override string ToString( )
    {
      return "RequestedFor:" + RequestedFor;
    }

  }

  public class ScriptEventArgs : EventArgs
  {
    public ScriptRunType Type = ScriptRunType.None;
		public string Server = String.Empty;
		public string Database = String.Empty;
		public string Username = String.Empty;

		public bool IsObjectHelpScript = false;
		
		public override string ToString( )
    {
      return "ScriptRunType:" + Type.ToString();
    }

		public void CopyFrom(ScriptEventArgs e)
		{
			Type = e.Type;
			Server = e.Server;
			Database = e.Database;
			IsObjectHelpScript = e.IsObjectHelpScript;
			Username = e.Username;
		}
  }

	public class ScriptExecutedEventArgs : ScriptEventArgs
  {
    public ScriptCompletionStatus Status = ScriptCompletionStatus.None;
    public IList<SqlMessage> Messages = null;
  }

	public class ScriptExecutingEventArgs : ScriptEventArgs
	{
		public bool Cancel;	
	}

  public delegate void AfterOpenedFileDelegate(object sender,string fileName );
  public delegate void BeforeOpenedFileDelegate( object sender, BeforeOpenedFileEventArgs args );

  public delegate void AfterSavedContentDelegate( object sender, string fileName );
  public delegate void BeforeSavedContentDelegate( object sender, EventArgs args );

  public delegate void AfterSavedContentToFileDelegate( object sender, string fileName );
  public delegate void BeforeSavedContentToFileDelegate( object sender, FileOperationEventArgs args );

  public delegate void AfterCaretPositionChangedDelegate( object sender, CaretPosition newPos );
  public delegate void BeforeCaretPositionChangedDelegate( object sender);

  public delegate void BeforeHelpRequestedDelegate( object sender, HelpRequestedEventArgs args );
  public delegate void AfterHelpRequestedDelegate( object sender, HelpRequestedEventArgs args);

	public delegate void ScriptExecutedDelegate(object sender, ScriptExecutedEventArgs args);
  public delegate void ScriptExecutingDelegate( object sender,  ScriptExecutingEventArgs  args );
  
  public delegate void BeforeCodeCompletionShowedDelegete( object sender, BeforeCodeCompletionShowedEventArgs args );
  public delegate void AfterCodeCompletionShowedDelegete( object sender, AfterCodeCompletionShowedEventArgs args);

}
