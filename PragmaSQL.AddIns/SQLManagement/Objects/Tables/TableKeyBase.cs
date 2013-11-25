using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;
namespace SQLManagement
{
  /// <summary>
  /// Tablee keys class (primary, unique, index)
  /// </summary>
  public class TableKeyBase
  {
    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
        }
        else
        {
          _cp = value;
        }
      }
    }


    private bool _clustered;
    public bool Clustered
    {
      get{return _clustered;}
      set{_clustered = value;}
    }

    private IList<string> _columns;
    public IList<string> Columns
    {
      get{return _columns;}
      set{_columns = value;}
    }

    private string _filegroup;
    public string Filegroup
    {
      get{return _filegroup;}
      set{_filegroup = value;}
    }

 
    
    private long _id;
    public long ID
    {
      get{return _id;}
      set{_id = value;}
    }

    private string _name;
    public string Name
    {
      get{return _name;}
      set{_name = value;}
    }


    private TableWrapper _table;
    public TableWrapper Table
    {
      get{return _table;}
      set{_table = value;}
    }

    private byte _fillFactor;
    public byte FillFactor
    {
      get{return _fillFactor;}
      set{_fillFactor = value;}
    }



    /// <summary>
    /// Constructor
    /// </summary>
    public TableKeyBase(ConnectionParams cp )
    {
      ConnectionParams = cp;

      _table = new TableWrapper();
      _table.ConnectionParams = cp;
    }
  }
}
