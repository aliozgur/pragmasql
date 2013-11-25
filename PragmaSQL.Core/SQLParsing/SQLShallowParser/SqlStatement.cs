/********************************************************************
  Class      : SqlSelectStatement
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using com.calitha.goldparser;

namespace PragmaSQL.Core
{

  public enum SqlShParserStatmentType
  {
    Unknown,
    Select,
    Case
  }

  public class SqlStatement
  {
    #region Fields and Properties

    public SqlShParserLocation TopMostStartLocation;

    private Stack<SqlShParserLocation> _locationStarts = new Stack<SqlShParserLocation>();

    private IList<SqlShBoundary> _boundaries = new List<SqlShBoundary>();
    public IList<SqlShBoundary> Boundaries
    {
      get { return _boundaries; }
    }

    private bool _hasAnySubStatement = false;
    public bool HasAnySubStatement
    {
      get { return _hasAnySubStatement; }
      set { _hasAnySubStatement = value; }
    }

    private bool _isSubStatement = false;
    public bool IsSubStatement
    {
      get { return _isSubStatement; }
      set { _isSubStatement = value; }
    }

    private SqlStatement _parentStatment = null;
    public SqlStatement ParentStatment
    {
      get { return _parentStatment; }
      set
      {
        if (value != null)
        {
          value._children.Add(this);
        }
        else
        {
          if (_parentStatment != null)
          {
            _parentStatment._children.Remove(this);
          }
        }
        _parentStatment = value;
      }
    }

    private string _text = String.Empty;
    public string Text
    {
      get { return _text; }
    }

    private SqlShParserStatmentType _type = SqlShParserStatmentType.Unknown;
    public SqlShParserStatmentType Type
    {
      get { return _type; }
    }

    private IList<SqlStatement> _children = new List<SqlStatement>();
    /// <summary>
    /// This is read only
    /// </summary>
    public IList<SqlStatement> Children
    {
      get { return new List<SqlStatement>(_children).AsReadOnly(); }
    }

    private ArrayList _startLocationStorage = null;
    public ArrayList StartLocationStorage
    {
      get { return _startLocationStorage; }
    }

    #endregion //Fields and Properties

    #region Constructors
    public SqlStatement( ArrayList startLocationStorage, SqlShParserStatmentType type )
    {
      _type = type;
      _startLocationStorage = startLocationStorage;
      TopMostStartLocation = SqlShParserLocation.NullParserLocation();
    }

    #endregion

    #region Methods

    public void AddBoundary( SqlShParserLocation start, SqlShParserLocation end )
    {
      SqlShBoundary b = new SqlShBoundary(start, end);
      _boundaries.Add(b);

      if (_startLocationStorage != null)
      {
        _startLocationStorage.Add(start);
      }
    }

    public void ClearBoundaries( )
    {
      _boundaries.Clear();
    }

    private void PushStartLocation( SqlShParserLocation start )
    {
      _locationStarts.Push(start);
      if (_locationStarts.Count == 1)
        TopMostStartLocation = start;
    }

    public void PushStartLocation( Location start, int offsetCorrection )
    {
      SqlShParserLocation loc = SqlShParserLocation.CreateCopyFromParserLocation(start, offsetCorrection);
      loc.Statement = this;
      PushStartLocation(loc);
    }

    public void PushStartLocation( Location start )
    {
      PushStartLocation(start, 0);
    }

    private void PushEndLocation( SqlShParserLocation end )
    {
      if (_locationStarts.Count == 0)
      {
        return;
      }

      SqlShParserLocation start = _locationStarts.Pop();
      AddBoundary(start, end);
    }

    public void PushEndLocation( Location end, int offsetCorrection )
    {
      SqlShParserLocation loc = SqlShParserLocation.CreateCopyFromParserLocation(end, offsetCorrection);
      loc.Statement = this;
      PushEndLocation(loc);
    }

    public void PushEndLocation( Location end )
    {
      PushEndLocation(end, 0);
    }

    public void AddToken( string tok )
    {
      _text += " " + tok;
    }

    public void AddTokenToHierarchy( string tok )
    {
      _text += " " + tok;
      if (_parentStatment != null)
      {
        _parentStatment.AddTokenToHierarchy(tok);
      }
    }

    public void ClearChildren( )
    {
      IList<SqlStatement> children = this.Children;
      foreach (SqlStatement child in children)
      {
        child.ParentStatment = null;
      }
    }

    public bool IsOffsetBetweenAnyBoundary(int offset)
    {
      bool result = false;
      foreach (SqlShBoundary b in this.Boundaries)
      {
        if (offset >= b.StartOffset && offset <= b.EndOffset)
        {
          result = true;
          break;
        }
      }
      return result;
    }

    public void CalculateMinMaxLine( out int minStartLine, out int maxEndLine )
    {
      minStartLine = Int32.MaxValue;
      maxEndLine = -1;

      foreach (SqlShBoundary b in this.Boundaries)
      {
        if (b.StartLine <= minStartLine)
        {
          minStartLine = b.StartLine;
        }

        if (b.EndLine >= maxEndLine)
        {
          maxEndLine = b.EndLine;
        }
      }

      foreach (SqlStatement child in this._children)
      {
        foreach (SqlShBoundary b in child.Boundaries)
        {
          if (b.StartLine <= minStartLine)
          {
            minStartLine = b.StartLine;
          }
          if (b.EndLine >= maxEndLine)
          {
            maxEndLine = b.EndLine;
          }
        }
      }
    }

    #endregion //Methods

  }//Class End
}//Namespace End
