using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;

namespace PragmaSQL.WorkspaceMan
{
  [Serializable]
	public class WorkspaceItem
	{
		public WorkspaceItemType ItemType = WorkspaceItemType.None;
    public WorkspaceItemTarget Target = WorkspaceItemTarget.None;

    private List<ContentPart> _contentParts = new List<ContentPart>();

    public List<ContentPart> ContentParts
    {
      get { return _contentParts; }
      set { _contentParts = value; }
    }

		public string FileName = String.Empty;
		
		public string ObjectName = String.Empty;
		public string Server = String.Empty;
		public string Database = String.Empty;
    public string Username = String.Empty;
    public string IntegratedSecurity = String.Empty;
    public string SyntaxMode = "Default";

    public string Content = String.Empty;

    public void AddContentPart(ContentPart part)
    {
      _contentParts.Add(part);
    }

    public static WorkspaceItem CreateScriptContent(ConnectionParams cp)
    {
      WorkspaceItem result = new WorkspaceItem();
      result.Target = WorkspaceItemTarget.ScriptEditor;
      result.FileName = String.Empty;
      result.ItemType = WorkspaceItemType.Content;
      result.Content = String.Empty;
      result.Server = cp.Server;
      result.Database = cp.Database;
      result.Username = cp.CurrentUsername;
      result.IntegratedSecurity = cp.IntegratedSecurity;

      return result;
    }
    
    public static WorkspaceItem CreateTextContent(string content, string syntaxMode)
    {
      WorkspaceItem result = new WorkspaceItem();
      result.Target = WorkspaceItemTarget.TextContent;
      result.FileName = String.Empty;
      result.ItemType = WorkspaceItemType.Content;
      result.SyntaxMode = syntaxMode;
      result.Content = content;
      return result;
    }

    public static WorkspaceItem CreateTextFile(string fileName)
		{
			WorkspaceItem result = new WorkspaceItem();
      result.Target = WorkspaceItemTarget.TextEditor;
			result.FileName = fileName;
			result.ItemType = WorkspaceItemType.TextFile;
			return result;
		}

		public static WorkspaceItem CreateScriptFile(ConnectionParams cp, string fileName)
		{
			WorkspaceItem result = new WorkspaceItem();
      result.Target = WorkspaceItemTarget.ScriptEditor;
			result.FileName = fileName;
			result.Server = cp.Server;
			result.Database = cp.Database;
      result.Username = cp.CurrentUsername;
      result.IntegratedSecurity = cp.IntegratedSecurity;
      result.ItemType = WorkspaceItemType.ScriptFile;
			return result;
		}

		public static WorkspaceItem CreateProjectFile(string fileName)
		{
			WorkspaceItem result = new WorkspaceItem();
      result.Target = WorkspaceItemTarget.ProjectExplorer;
			result.FileName = fileName;
			result.ItemType = WorkspaceItemType.ProjectFile;
			return result;
		}

		public static WorkspaceItem CreateDatabaseObject(ConnectionParams cp, string objectName)
		{
			WorkspaceItem result = new WorkspaceItem();
      result.Target = WorkspaceItemTarget.ScriptEditor;
			result.ObjectName = objectName;
			result.Server = cp.Server;
			result.Database = cp.Database;
      result.Username = cp.CurrentUsername;
      result.IntegratedSecurity = cp.IntegratedSecurity;
      result.ItemType = WorkspaceItemType.DatabaseObject;
			return result;
		}

		public static WorkspaceItem CreateDatabaseConnection(ConnectionParams cp)
		{
			WorkspaceItem result = new WorkspaceItem();
      result.Target = WorkspaceItemTarget.ObjectExplorer;
			result.Server = cp.Server;
			result.Database = cp.Database;
      result.Username = cp.CurrentUsername;
      result.IntegratedSecurity = cp.IntegratedSecurity;
      result.ItemType = WorkspaceItemType.Connection;
			return result;
		}
	}

  [Serializable]
  public class ContentPart
  {
    public ContentPartType PartType = ContentPartType.None;
    public string Content = String.Empty;

    public ContentPart()
    {
    
    }

    public ContentPart(ContentPartType partType,string content)
    {
      PartType = partType;
      Content = content;
    }
  }

  public enum ContentPartType
  {
    None,
    Object,
    Text
  }
}
