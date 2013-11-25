using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Reflection;

namespace PragmaSQL
{
  
  public class ColumnWrapper
  {
    private bool _allowNulls;
    public bool AllowNulls
    {
      get { return _allowNulls; }
      set 
      { 
        _allowNulls = value;
      }
    }


    private string _default = String.Empty;
    public string Default
    {
      get { return _default; }
      set 
      { 
        _default = value;
      }
    }

    private string _defaultBinding = String.Empty;
    public string DefaultBinding
    {
      get { return _defaultBinding; }
      set { _defaultBinding = value; }
    }


    private string _defaultConstraint;
    public string DefaultConstraint
    {
      get { return _defaultConstraint; }
      set { _defaultConstraint = value;  }
    }


    private string _increment = String.Empty;
    public string Increment
    {
      get { return _increment; }
      set { _increment = value;}
    }

    private string _seed = String.Empty;
    public string Seed
    {
      get { return _seed; }
      set { _seed = value; }
    }

    private bool _isIdentity;
    public bool IsIdentity
    {
      get { return _isIdentity; }
      set { _isIdentity = value; }
    }

    private bool _isPrimaryKey;
    public bool IsPrimaryKey
    {
      get { return _isPrimaryKey; }
      set 
      { 
        _isPrimaryKey = value;
      }
    }

    private bool _isComputed = false;
    public bool IsComputed
    {
      get { return _isComputed; }
      set { _isComputed = value;  }
    }

   

    private string _formula = String.Empty;
    public string Formula
    {
      get { return _formula; }
      set { _formula = value; }
    }


    private string _width;
    public string Width
    {
      get { return _width; }
      set { _width = value;  }
    }

    private string _name = String.Empty;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }


    private string _precision = String.Empty;
    public string Precision
    {
      get { return _precision; }
      set { _precision = value;  }
    }

    private string _scale = String.Empty;
    public string Scale
    {
      get { return _scale; }
      set { _scale = value;  }
    }

    
    private string _dataType = String.Empty;
    public string DataType
    {
      get { return _dataType; }
      set { _dataType = value;  }
    }

    
    private string _colId = String.Empty;
    public string ColId
    {
      get { return _colId; }
      set { _colId = value;  }
    }

    private string _ruleBinding = String.Empty;
    public string RuleBinding
    {
      get
      { return _ruleBinding; }
      set { _ruleBinding = value; }
    }

    private string _collation = String.Empty;
    public string Collation
    {
      get { return _collation; }
      set { _collation = value;  }
    }

    private int _collationId;
    public int CollationId
    {
      get { return _collationId; }
      set { _collationId = value; }
    }

		public string LowerName
		{
			get
			{
				if (String.IsNullOrEmpty(_name))
					return String.Empty;

				return _name.ToLowerInvariant();
			}
		}
		public ColumnWrapper()
		{

		}
  }
}
