using System;
using System.Collections.Generic;
using System.Text;

namespace SQLManagement
{
  public class DatabaseObjectBase
  {
    private long _id;
    public long ID
    {
      get { return _id; }
      set { _id = value; }
    }

    private string _name = String.Empty;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }
  
    //public string UnqualifiedName
    //{
    //  get
    //  {
    //    return String.IsNullOrEmpty(_name) ? _name : _name.Replace("[", String.Empty).Replace("]", String.Empty);
    //  }
    //}

    //public string UnqualifiedFullName
    //{
    //  get
    //  {
    //    return String.Format("{0}.{1}",UnqualifiedOwner,UnqualifiedName);
    //  }
    //}

    private string _owner;
    public string Owner
    {
      get { return _owner; }
      set { _owner = value; }
    }

    //public string UnqualifiedOwner
    //{
    //  get 
    //  {
    //      return String.IsNullOrEmpty(_owner) ? _owner : _owner.Replace("[",String.Empty).Replace("]",String.Empty);
    //  }
    //}

    private string _ownerObjectName;
    public string OwnerObjectName
    {
      get { return _ownerObjectName; }
      set { _ownerObjectName = value; }
    }


    private long _ownerObjectId;
    public long OwnerObjectId
    {
      get { return _ownerObjectId; }
      set { _ownerObjectId = value; }
    }

    /// <summary>
    /// [owner].[name]
    /// <para>Special characters not removed</para>
    /// </summary>
    public string QualifiedOwnerObjectName
    {
      get
      {
        return String.Format("[{0}].[{1}]", _owner, _ownerObjectName);
      }
    }

    /// <summary>
    /// owner.name
    /// <para>Special characters removed</para>
    /// </summary>
    public  string NormalizedOwnerObjectName
    {
      get
      {
        return Utils.ReplaceQuatations(_owner) + "." + Utils.ReplaceQuatations(_ownerObjectName);
      }
    }

    public DatabaseObjectBase()
    {

    }

    public DatabaseObjectBase(long ObjectID)
    {
      _id = ObjectID;
    }

    /// <summary>
    /// [owner].[name] 
    /// <para>Special characters not removed</para>
    /// </summary>
    public string QualifiedFullName
    {
      get
      {
        return String.Format("[{0}].[{1}]", _owner, _name);
      }
    }

    public string QualifiedName
    {
      get
      {
        return String.Format("[{0}]",_name);
      }
    }

    /// <summary>
    /// Returns special characters removed version of name
    /// </summary>
    public string NormalizedName
    {
      get
      {
        return Utils.ReplaceQuatations(_name);
      }
    }
    
    /// <summary>
    /// owner.name
    /// <para>Special characters removed</para>
    /// </summary>
    public string NormalizedFullName
    {
      get
      {
        return Utils.ReplaceQuatations(_owner) + "." + Utils.ReplaceQuatations(_name);
      }
    }

    public string FullName
    {
      get
      {
        return _owner + "." + _name;      
      }
    }

    public override string ToString()
    {
      return _id.ToString() + ":" + QualifiedFullName;
    }
  }
}
