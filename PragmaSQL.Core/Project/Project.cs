/********************************************************************
  Class Project
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
  public class Project
  {
    #region Fields and Properties

    private string _createdBy = String.Empty;
    public string CreatedBy
    {
      get { return _createdBy; }
      set { _createdBy = value; }
    }

    private DateTime? _createdOn = null;
    public DateTime? CreatedOn
    {
      get { return _createdOn; }
      set { _createdOn = value; }
    }

    private string _fullPath = String.Empty;
    public string FullPath
    {
      get { return _fullPath; }
      set { _fullPath = value; }
    }

    private string _name = String.Empty;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    private SerializableDictionary<string, ProjectItem> _connectedItems = new SerializableDictionary<string, ProjectItem>();
    public SerializableDictionary<string, ProjectItem> ConnectedItems
    {
      get { return _connectedItems; }
      set { _connectedItems = value; }
    }

    private SerializableDictionary<string, ProjectItem> _disconnectedItems = new SerializableDictionary<string, ProjectItem>();
    public SerializableDictionary<string, ProjectItem> DisconnectedItems
    {
      get { return _disconnectedItems; }
      set { _disconnectedItems = value; }
    }


    #endregion //Fields and Properties

    #region Methods
    public string GetDirectory( )
    {
      FileInfo fi = new FileInfo(_fullPath);
      return fi.Directory.FullName;
    }

    public override string ToString( )
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Name: " + _name);
      sb.AppendLine("Full Path: " + _fullPath);
      sb.AppendLine("Created By: " + _createdBy);
      sb.AppendLine("Created On: " + _createdOn.ToString());
      sb.AppendLine("Connected Items {" + ( _connectedItems != null ? _connectedItems.Count.ToString(): "0") + "} ");
      sb.AppendLine("Disconnected Items {" + (_disconnectedItems != null ? _disconnectedItems.Count.ToString() : "0") + "} ");

      return sb.ToString();
    }
    #endregion //Methods

  }//Class

  public static class ProjectFactory
  {
    public static Project LoadProject( string filePath )
    {
      Project result = ObjectXMLSerializer<Project>.Load(filePath);
      result.FullPath = filePath;
      return result;
    }

    public static void SaveProject( Project prj )
    {
      ObjectXMLSerializer<Project>.Save(prj, prj.FullPath);
    }
  }

}//Namespace
