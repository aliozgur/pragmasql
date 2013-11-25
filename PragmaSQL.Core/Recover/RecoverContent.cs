using System;
using System.Collections.Generic;
using System.Text;
using PragmaSQL.Core;
using System.IO;

namespace PragmaSQL.Core
{
  [Serializable]
  public class RecoverContent
  {
    public static readonly string RecoverFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "PragmaSQL\\RecoveryData");
    public static readonly string WorkspaceFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "PragmaSQL\\WorkspaceData");
    public RecoverContentType ItemType = RecoverContentType.Text;
    public int ObjectType = -1;

    public Guid Uid;
    public string FileName = String.Empty;
    public string Server = String.Empty;
    public string Database = String.Empty;
    public string Username = String.Empty;
    public string IntegratedSecurity = String.Empty;
    public string SyntaxMode = "SQL";
    public string Title = String.Empty;
    public long Time = DateTime.Now.ToFileTime();
    public string Content = String.Empty;
    public string DbInfo
    {
      get 
      {
        return String.Format("{0} on {1} [{2}]", Database, Server, !String.IsNullOrEmpty(IntegratedSecurity) ? "WindowsAuthentication" : Username);
      }
    }

    public bool HasConnectionInfo
    {
      get 
      {
        return !String.IsNullOrEmpty(Server) && !String.IsNullOrEmpty(Database) && (!String.IsNullOrEmpty(Username) || !String.IsNullOrEmpty(IntegratedSecurity));
      }
    }

    static RecoverContent()
    {
      if (!Directory.Exists(RecoverFolder))
        Directory.CreateDirectory(RecoverFolder);
      
      if (!Directory.Exists(WorkspaceFolder))
        Directory.CreateDirectory(WorkspaceFolder);
    }

    public static RecoverContent CreateTextContent(ITextEditor editor)
    {
      RecoverContent item = new RecoverContent();
      item.Uid = editor.Uid;
      item.Content = editor.Content;
      item.SyntaxMode = editor.CurrentSytaxMode;
      item.Title = editor.Caption;
      item.FileName = String.IsNullOrEmpty(editor.FileName) ? String.Empty : editor.FileName;
      item.ObjectType = -1;
      switch (editor.ContentType)
      {
        case EditorContentType.Text:
          item.ItemType = RecoverContentType.Text;
          break;
        case EditorContentType.File:
          item.ItemType = RecoverContentType.TextFile;
          break;
        case EditorContentType.SharedSnippet:
          item.ItemType = RecoverContentType.SharedSnippet;
          break;
        case EditorContentType.SharedScript:
          item.ItemType = RecoverContentType.SharedScript;
          break;
        default:
          break;
      }
      return item;
    
    }

    public static RecoverContent CreateScriptContent(IScriptEditor editor)
    {
      RecoverContent item = new RecoverContent();
      item.Uid = editor.Uid;
      item.Content = editor.Content;
      item.SyntaxMode = editor.CurrentSytaxMode;
      item.Title = editor.Caption;
      item.Server = editor.CurrentConnection  == null ? String.Empty : editor.CurrentConnection.Server;
      item.Database = editor.CurrentConnection  == null ? String.Empty : editor.CurrentConnection.Database;
      item.Username = editor.CurrentConnection == null ? String.Empty : editor.CurrentConnection.UserName;
      item.IntegratedSecurity = editor.CurrentConnection == null ? String.Empty : editor.CurrentConnection.IntegratedSecurity;
      item.FileName = String.IsNullOrEmpty(editor.FileName) ? String.Empty : editor.FileName;
      item.ObjectType = editor.ObjectType;
      switch (editor.ContentType)
      {
        case EditorContentType.Script:
          item.ItemType = RecoverContentType.Script;
          break;
        case EditorContentType.File:
          item.ItemType = RecoverContentType.ScriptFile;
          break;
        case EditorContentType.SharedSnippet:
          item.ItemType = RecoverContentType.SharedSnippet;
          break;
        case EditorContentType.SharedScript:
          item.ItemType = RecoverContentType.SharedScript;
          break;
        default:
          item.ItemType = RecoverContentType.SharedScript;
          break;
      }   

      return item;
    }

    public static void Save(string folderPath, RecoverContent item)
    {
      if (!Directory.Exists(folderPath))
        Directory.CreateDirectory(folderPath);

      string path = Path.Combine(folderPath, item.Uid + ".recover");

      ObjectXMLSerializer<RecoverContent>.Save(item, path, SerializedFormat.Document);
    
    }

    public static void Save(RecoverContent item)
    {
      Save(RecoverFolder, item);
    }

    public static void RemoveContent(string folderPath, ITextEditor editor)
    {
      if (!Directory.Exists(folderPath) || editor == null)
        return;

      string path = Path.Combine(folderPath, editor.Uid + ".recover");
      if (File.Exists(path))
        File.Delete(path);
    }

    public static void RemoveContent(ITextEditor editor)
    {
      RemoveContent(RecoverFolder, editor);
    }

    public static IList<RecoverContent> LoadAll(string folderPath)
    {
      if (!Directory.Exists(folderPath))
        return null;

      IList<RecoverContent> result = new List<RecoverContent>();

      string[] fileNames = Directory.GetFiles(folderPath, "*.recover");
      foreach (string fileName in fileNames)
      {
        result.Add(ObjectXMLSerializer<RecoverContent>.Load(fileName, SerializedFormat.Document));
      }
      return result;
    }

    public static IList<RecoverContent> LoadAll()
    {
      return LoadAll(RecoverFolder);
    }

    public static int GetCount(string folderPath)
    {
      if (!Directory.Exists(folderPath))
        return 0;

      return Directory.GetFiles(folderPath, "*.recover").Length;
    }
    
    public static int GetCount()
    {
      return GetCount(RecoverFolder);
    }

    public static void CleanAll(string folderPath)
    {
      if (!Directory.Exists(folderPath))
        return;

      string[] fileNames = Directory.GetFiles(folderPath, "*.recover");
      foreach (string fileName in fileNames)
      {
        File.Delete(fileName);
      }
    }

    public static void CleanAll()
    {
      CleanAll(RecoverFolder);
    }
  }
}
