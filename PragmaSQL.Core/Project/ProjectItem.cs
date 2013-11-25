/********************************************************************
  Class ProjectItem
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  [Serializable]
  public class ProjectItem:IComparable
  {
    #region Fields and Properties

    private Guid _uid = Guid.NewGuid();
    public Guid Uid
    {
      get { return _uid; }
      set { _uid = value; }
    }

    private ProjectItemType _itemType = ProjectItemType.Unknown;
    public ProjectItemType ItemType
    {
      get { return _itemType; }
      set { _itemType = value; }
    }


    private int _dbObjectType = DBObjectType.None;
    public int DbObjectType
    {
      get { return _dbObjectType; }
      set { _dbObjectType = value; }
    }


    private ConnectionParams _connectionSpec = null;
    public ConnectionParams ConnectionSpec
    {
      get { return _connectionSpec; }
      set { _connectionSpec = value; }
    }

    private SerializableDictionary<string, ProjectItem> _children = new SerializableDictionary<string, ProjectItem>();
    public SerializableDictionary<string, ProjectItem> Children
    {
      get { return _children; }
      set { _children = value; }
    }

    private string _name = String.Empty;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    private bool _isConnectedItem = false;
    public bool IsConnectedItem
    {
      get { return _isConnectedItem; }
      set { _isConnectedItem = value; }
    }

    private string _extension = String.Empty;
    public string Extension
    {
      get { return _extension; }
      set { _extension = value; }
    }

    private string NameWithExtension
    {
      get
      {
        return _name + "" + _extension;
      }
    }

    private string _relativeFolder = String.Empty;
    public string RelativeFolder
    {
      get { return _relativeFolder; }
      set { _relativeFolder = value; }
    }

    private string _parentFolder = String.Empty;
    public string ParentFolder
    {
      get { return _parentFolder; }
      set
      {
        _parentFolder = value;
        switch (_itemType)
        {
          case ProjectItemType.ConnectionSpec:
            _relativeFolder = String.Empty;
            break;
          case ProjectItemType.Folder:
            if (String.IsNullOrEmpty(_parentFolder))
            {
              _relativeFolder = _name;
            }
            else
            {
              _relativeFolder = _parentFolder + @"\" + _name;
            }
            break;
          case ProjectItemType.ExternalFile:
          case ProjectItemType.TextFile:
          case ProjectItemType.ScriptFile:
            _relativeFolder = _parentFolder;
            break;
          case ProjectItemType.NonScriptableDBObject:
            _relativeFolder = String.Empty;
            break;
          case ProjectItemType.Project:
            break;
          case ProjectItemType.ScriptableDBObject:
            _relativeFolder = _parentFolder;
            break;
          case ProjectItemType.Unknown:
            break;
        }
      }
    }


    #endregion //Fields and Properties

    #region Constructor

    private ProjectItem( )
    {

    }

    internal ProjectItem( ProjectItemType itemType )
    {
      _itemType = itemType;
    }

    #endregion //Constructor

    #region Child item related operations

    public void AddChild( ProjectItem item )
    {
      if (item == null)
      {
        return;
      }

      if (item == this)
      {
        throw new InvalidOperationException("Self can not be added as a child!");
      }

      if (_children.ContainsKey(item.Uid.ToString()))
      {
        return;
      }
      this._children.Add(item.Uid.ToString(), item);
    }

    public void RemoveChild( ProjectItem item )
    {
      if (item == null)
      {
        return;
      }

      if (item == this)
      {
        throw new InvalidOperationException("Self is not a child, thus can not be removed from the children collection!");
      }


      if (!_children.ContainsKey(item.Uid.ToString()))
      {
        throw new ItemNotInCollectionException("Item can not be found in children collection!");
      }
      _children.Remove(item.Uid.ToString());
    }

    #endregion //Child item related operations

    #region Methods
    public override string ToString( )
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Name: " + _name);
      sb.AppendLine("Uid: " + _uid.ToString());
      sb.AppendLine("Type: " + _itemType.ToString());
      sb.AppendLine("DbType: " + _dbObjectType.ToString());
      sb.AppendLine("IsConnected: " + _isConnectedItem.ToString());
      sb.AppendLine("ParentFolder: " + _parentFolder);
      sb.AppendLine("RelativeFolder: " + _relativeFolder);
      sb.AppendLine("Extension: " + _extension.ToString());
      if(_connectionSpec != null)
      {
        sb.AppendLine("Connection: " + _connectionSpec.Server + "@" + _connectionSpec.Database + " as " + _connectionSpec.CurrentUsername);
      }
     return sb.ToString();
    }

    public string GetFullPath( Project prj )
    {
      if (prj == null)
      {
        throw new NullParameterException("Project parameter is null!");
      }
      return prj.GetDirectory() + @"\" + this._relativeFolder;
    }

    public string GetFullFileName( Project prj )
    {
      if (prj == null)
      {
        throw new NullParameterException("Project parameter is null!");
      }
      string result = String.Empty;
      string prjDir = prj.GetDirectory();
      
      if(!prjDir.EndsWith(@"\"))
      {
        result += prjDir + @"\";
      }
      
      result += this._relativeFolder;
      if(!result.EndsWith(@"\"))
      {
        result += @"\" + NameWithExtension;
      }
      else
      {
        result += NameWithExtension;
      }

      //return prj.GetDirectory() + @"\" + this._relativeFolder + @"\" + NameWithExtension;
      return result;
    }

    public void RenameItem( Project prj, string newName )
    {
      string fileName = String.Empty;
      string newFileName = String.Empty;

      switch (_itemType)
      {
        case ProjectItemType.Unknown:
          break;
        case ProjectItemType.ConnectionSpec:
          break;
        case ProjectItemType.Project:
          break;
        case ProjectItemType.NonScriptableDBObject:
          _name = newName;
          break;
        case ProjectItemType.ExternalFile:
        case ProjectItemType.TextFile:
        case ProjectItemType.ScriptFile:
          fileName = this.GetFullFileName(prj);
          this.Name = newName;
          newFileName = this.GetFullFileName(prj);
          File.Move(fileName, newFileName);
          //File.Delete(fileName);
          break;
        case ProjectItemType.ScriptableDBObject:
          fileName = this.GetFullFileName(prj);
          this.Name = newName;
          newFileName = this.GetFullFileName(prj);
          File.Move(fileName, newFileName);
          //File.Delete(fileName);
          break;
        case ProjectItemType.Folder:
          fileName = GetFullPath(prj);
          _name = newName;
          if (!String.IsNullOrEmpty(_parentFolder))
          {
            _relativeFolder = _parentFolder + @"\" + _name;
          }
          else
          {
            _relativeFolder = _name;
          }

          newFileName = this.GetFullPath(prj);
          Directory.Move(fileName, newFileName);
          foreach (ProjectItem child in _children.Values)
          {
            child.RecursiveUpdateParentFolders(_relativeFolder);
          }
          break;
        default:
          break;
      }
    }

    private void RecursiveUpdateParentFolders( string parentFolder )
    {
      ParentFolder = parentFolder;
      foreach (ProjectItem child in _children.Values)
      {
        child.RecursiveUpdateParentFolders(this.RelativeFolder);
      }
    }
    
    private void RecursiveUpdateConnectedStatus( bool value)
    {
      this.IsConnectedItem = value;
      foreach (ProjectItem child in _children.Values)
      {
        child.RecursiveUpdateConnectedStatus(this.IsConnectedItem);
      }
    }

    private void DeleteDirectory( string path )
    {
      //Delete files
      string[] files = Directory.GetFiles(path);
      foreach (string file in files)
      {
        File.Delete(file);
      }

      // Delete sub-directories
      string[] dirs = Directory.GetDirectories(path);
      foreach (string dir in dirs)
      {
        DeleteDirectory(dir);
      }

      // Delete self
      Directory.Delete(path);
    }

    public void DeleteItemPhysically( Project prj )
    {
      string fullPath = String.Empty;
      switch (_itemType)
      {
        case ProjectItemType.Unknown:
          break;
        case ProjectItemType.ConnectionSpec:
          foreach (ProjectItem child in _children.Values)
          {
            child.DeleteItemPhysically(prj);
          }
          break;
        case ProjectItemType.Project:
          break;
        case ProjectItemType.NonScriptableDBObject:
          break;
        case ProjectItemType.ExternalFile:
        case ProjectItemType.TextFile:
        case ProjectItemType.ScriptFile:
          fullPath = this.GetFullFileName(prj);
          File.Delete(fullPath);
          break;
        case ProjectItemType.ScriptableDBObject:
          fullPath = this.GetFullFileName(prj);
          File.Delete(fullPath);
          break;
        case ProjectItemType.Folder:
          fullPath = GetFullPath(prj);
          DeleteDirectory(fullPath);
          _children.Clear();
          break;
        default:
          break;
      }
    }

    public void ChangeParent( Project prj, ProjectItem parent )
    {
      if(prj == null)
      {
        return;
      }
      
      if(parent != null && parent.ItemType != ProjectItemType.Folder && parent.ItemType != ProjectItemType.ConnectionSpec)
      {
        return;
      }

      string parentPath = String.Empty;
      string fullPath = String.Empty;
      string newPath = String.Empty;
      
      if(parent != null)
      {
        parentPath = parent.GetFullPath(prj);
        fullPath = String.Empty;
        newPath = parentPath;
      }
      else
      {
        parentPath = prj.GetDirectory() + @"\";
        fullPath = String.Empty;
        newPath = parentPath;      
      }

      switch (_itemType)
      {
        case ProjectItemType.Unknown:
          break;
        case ProjectItemType.ConnectionSpec:
          break;
        case ProjectItemType.Project:
          break;
        case ProjectItemType.NonScriptableDBObject:
          break;
        case ProjectItemType.ScriptableDBObject:
        case ProjectItemType.ExternalFile:
        case ProjectItemType.TextFile:
        case ProjectItemType.ScriptFile:
          fullPath = this.GetFullFileName(prj);
          if(!newPath.EndsWith(@"\"))
            newPath += @"\" + _name + _extension;
          else
            newPath += _name + _extension;

          if(fullPath.Trim().ToLowerInvariant() != newPath.Trim().ToLowerInvariant())
          {
            File.Move(fullPath,newPath);
          }
          break;
        case ProjectItemType.Folder:
          fullPath = GetFullPath(prj);
          
          if(!newPath.EndsWith(@"\"))
            newPath += @"\" + _name;
          else
            newPath += _name;

          if(fullPath.Trim().ToLowerInvariant() != newPath.Trim().ToLowerInvariant())
          {
            Directory.Move(fullPath,newPath);
          }
          break;
        default:
          break;
      }
      
      if(parent != null)
      {
        RecursiveUpdateParentFolders(parent.RelativeFolder);
        RecursiveUpdateConnectedStatus(parent.IsConnectedItem);
      }
      else
      {
        RecursiveUpdateParentFolders(String.Empty);
        RecursiveUpdateConnectedStatus(false);      
      }
    }

    #endregion //Methods

    #region IComparable 
    public int CompareTo(Object obj)
    {
      if(obj.GetType() != typeof(ProjectItem))
      {
        throw new InvalidOperationException("Can not compare ProjectItem type with " + obj.GetType().ToString() );
      }

      ProjectItem itemObj = obj as ProjectItem;
      if(this.ItemType == itemObj.ItemType)
      {
        return this.Name.ToLowerInvariant().CompareTo(itemObj.Name.ToLowerInvariant());
      }
      else
      {
        return this.ItemType.CompareTo(itemObj.ItemType);
      }
    }

    #endregion
  } //Class end

  /// <summary>
  /// Project item factory 
  /// </summary>
  public static class ProjectItemFactory
  {
    public static ProjectItem CreateConnectionSpec( Project prj, ConnectionParams cp )
    {
      if (cp == null)
      {
        throw new NullParameterException("ConnectionParams paramater is null!");
      }

      ProjectItem result = new ProjectItem(ProjectItemType.ConnectionSpec);
      result.Name = cp.Server + " {" + cp.Database + "}";
      if (prj != null)
      {
        if (!prj.ConnectedItems.ContainsKey(cp.ID.ToString()))
        {
          prj.ConnectedItems.Add(cp.ID.ToString(), result);
        }
      }

      result.IsConnectedItem = true;
      result.ConnectionSpec = cp.CreateCopy();
      return result;
    }

    public static ProjectItem CreateFolder( ProjectItem parent, string name )
    {
      if (parent != null &&
        (parent.ItemType != ProjectItemType.Folder
        && parent.ItemType != ProjectItemType.ConnectionSpec
        && parent.ItemType != ProjectItemType.Project
        )
        )
      {
        throw new InvalidOperationException("Parent item type is invalid.");
      }

      ProjectItem result = new ProjectItem(ProjectItemType.Folder);
      result.Name = name;
      if (parent != null)
      {
        result.ParentFolder = parent.RelativeFolder;
        parent.AddChild(result);
        result.IsConnectedItem = parent.IsConnectedItem;
      }
      else
      {
        result.RelativeFolder = name;
      }

      return result;
    }

    public static ProjectItem CreateScriptFile( ProjectItem parent, string filePath )
    {
      if (parent != null &&
        (parent.ItemType != ProjectItemType.Folder
        && parent.ItemType != ProjectItemType.ConnectionSpec
        && parent.ItemType != ProjectItemType.Project
        )
        )
      {
        throw new InvalidOperationException("Parent item type is invalid.");
      }

      FileInfo fi = new FileInfo(filePath);
      ProjectItem result = new ProjectItem(ProjectItemType.ScriptFile);
      result.Name = fi.Name.Replace(fi.Extension, String.Empty);
      result.Extension = fi.Extension;

      if (parent != null)
      {
        parent.AddChild(result);
        result.ParentFolder = parent.RelativeFolder;
        result.IsConnectedItem = parent.IsConnectedItem;
      }
      return result;
    }

    public static ProjectItem CreateTextFile( ProjectItem parent, string filePath )
    {
      if (parent != null &&
        (parent.ItemType != ProjectItemType.Folder
        && parent.ItemType != ProjectItemType.ConnectionSpec
        && parent.ItemType != ProjectItemType.Project
        )
        )
      {
        throw new InvalidOperationException("Parent item type is invalid.");
      }

      FileInfo fi = new FileInfo(filePath);
      ProjectItem result = new ProjectItem(ProjectItemType.TextFile);
      result.Name = fi.Name.Replace(fi.Extension, String.Empty);
      result.Extension = fi.Extension;

      if (parent != null)
      {
        parent.AddChild(result);
        result.ParentFolder = parent.RelativeFolder;
        result.IsConnectedItem = parent.IsConnectedItem;
      }
      return result;
    }

    public static ProjectItem CreateExternalFile( ProjectItem parent, string filePath )
    {
      if (parent != null &&
        (parent.ItemType != ProjectItemType.Folder
        && parent.ItemType != ProjectItemType.ConnectionSpec
        && parent.ItemType != ProjectItemType.Project
        )
        )
      {
        throw new InvalidOperationException("Parent item type is invalid.");
      }

      FileInfo fi = new FileInfo(filePath);
      ProjectItem result = new ProjectItem(ProjectItemType.ExternalFile);
      result.Name = fi.Name.Replace(fi.Extension, String.Empty);
      result.Extension = fi.Extension;

      if (parent != null)
      {
        parent.AddChild(result);
        result.ParentFolder = parent.RelativeFolder;
        result.IsConnectedItem = parent.IsConnectedItem;
      }
      return result;
    }

    public static ProjectItem CreateScriptableDBObject( ProjectItem parent, int dbObjectType, string filePath )
    {
      if (parent != null &&
        (parent.ItemType != ProjectItemType.Folder
        && parent.ItemType != ProjectItemType.ConnectionSpec
        && parent.ItemType != ProjectItemType.Project
        )
        )
      {
        throw new InvalidOperationException("Parent item type is invalid.");
      }

      FileInfo fi = new FileInfo(filePath);
      ProjectItem result = new ProjectItem(ProjectItemType.ScriptableDBObject);

      result.DbObjectType = dbObjectType;
      result.Name = fi.Name.Replace(fi.Extension, String.Empty);
      result.Extension = fi.Extension;
      if (parent != null)
      {
        result.ParentFolder = parent.RelativeFolder;
        parent.AddChild(result);
        result.IsConnectedItem = parent.IsConnectedItem;
      }
      return result;
    }

    public static ProjectItem CreateNonScriptableDBObject( ProjectItem parent, int dbObjectType, string objectName )
    {
      if (parent != null &&
        (parent.ItemType != ProjectItemType.Folder
        && parent.ItemType != ProjectItemType.ConnectionSpec
        && parent.ItemType != ProjectItemType.Project
        )
        )
      {
        throw new InvalidOperationException("Parent item type is invalid.");
      }

      ProjectItem result = new ProjectItem(ProjectItemType.NonScriptableDBObject);
      result.DbObjectType = dbObjectType;
      result.Name = objectName;
      if (parent != null)
      {
        parent.AddChild(result);
        result.IsConnectedItem = parent.IsConnectedItem;
      }
      return result;
    }
  }
}// Namespave end
