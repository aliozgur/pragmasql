using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Reflection;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace SQLManagement
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
        PerformVersioning("AllowNulls",value);
      }
    }


    private string _default = String.Empty;
    public string Default
    {
      get { return _default; }
      set 
      { 
        _default = value;
        PerformVersioning("Default", value);
      }
    }

    private string _defaultBinding = String.Empty;
    public string DefaultBinding
    {
      get { return _defaultBinding; }
      set { _defaultBinding = value; PerformVersioning("DefaultBinding", value); }
    }


    private string _defaultConstraint;
    public string DefaultConstraint
    {
      get { return _defaultConstraint; }
      set { _defaultConstraint = value; PerformVersioning("DefaultConstraint", value); }
    }


    private string _increment = String.Empty;
    public string Increment
    {
      get { return _increment; }
      set { _increment = value; PerformVersioning("Increment", value); }
    }

    private string _seed = String.Empty;
    public string Seed
    {
      get { return _seed; }
      set { _seed = value; PerformVersioning("Seed", value); }
    }

    private bool _isIdentity;
    public bool IsIdentity
    {
      get { return _isIdentity; }
      set { _isIdentity = value; PerformVersioning("IsIdentity", value); }
    }

    private bool _oldIdentity;
    public bool OldIdentity
    {
      get { return _oldIdentity; }
      set { _oldIdentity = value; }
    }

    private Image _pkImage = global::SQLManagement.Properties.Resources.DataContainer_MoveNextHS;
    public Image PkImage
    {
      get { return _pkImage; }
    }


    private bool _isPrimaryKey;
    public bool IsPrimaryKey
    {
      get { return _isPrimaryKey; }
      set 
      { 
        _isPrimaryKey = value;
        if (_isPrimaryKey)
        {
          _pkImage = global::SQLManagement.Properties.Resources.PrimaryKeyHS;
        }
        else
        {
          _pkImage = global::SQLManagement.Properties.Resources.DataContainer_MoveNextHS;
        }
        PerformVersioning("IsPrimaryKey", value);
      }
    }

    private bool _isComputed = false;
    public bool IsComputed
    {
      get { return _isComputed; }
      set { _isComputed = value; PerformVersioning("IsComputed", value); }
    }

    private bool _oldComputed = false;
    public bool OldComputed
    {
      get { return _oldComputed; }
      set { _oldComputed = value; }
    }

    private string _formula = String.Empty;
    public string Formula
    {
      get { return _formula; }
      set { _formula = value; PerformVersioning("Formula", value); }
    }


    private string _width;
    public string Width
    {
      get { return _width; }
      set { _width = value; PerformVersioning("Width", value); }
    }

    private string _name = String.Empty;
    public string Name
    {
      get { return _name; }
      set { _name = value; PerformVersioning("Name", value); }
    }

    private string _oldName = String.Empty;
    public string OldName
    {
      get { return _oldName; }
      set { _oldName = value; }
    }

    private string _precision = String.Empty;
    public string Precision
    {
      get { return _precision; }
      set { _precision = value; PerformVersioning("Precision", value); }
    }

    private string _scale = String.Empty;
    public string Scale
    {
      get { return _scale; }
      set { _scale = value; PerformVersioning("Scale", value); }
    }

    public SqlDataType SqlDataType
    {
      get
      {
        string dType = !String.IsNullOrEmpty(_dataType) ? _dataType.ToLowerInvariant() : String.Empty;
        switch (dType)
        {
          case "bigint":
            return SqlDataType.BigInt;
          case "bit":
            return SqlDataType.Bit;
          case "datetime":
            return SqlDataType.DateTime;
          case "float":
            return SqlDataType.Float;
          case "image":
            return SqlDataType.Image;
          case "int":
            return SqlDataType.Int;
          case "money":
            return SqlDataType.Money;
          case "ntext":
            return SqlDataType.NText;
          case "real":
            return SqlDataType.Real;
          case "smalldatetime":
            return SqlDataType.SmallDateTime;
          case "smallint":
            return SqlDataType.SmallInt;
          case "smallmoney":
            return SqlDataType.SmallMoney;
          case "sql_variant":
            return SqlDataType.Variant;
          case "text":
            return SqlDataType.Text;
          case "timestamp":
            return SqlDataType.Timestamp;
          case "tinyint":
            return SqlDataType.TinyInt;
          case "uniqueidentifier":
            return SqlDataType.UniqueIdentifier;
          case "xml":
            return SqlDataType.Xml;
          case "binary":
            return SqlDataType.Binary;
          case "char":
            return SqlDataType.Char;
          case "nchar":
            return SqlDataType.NChar;
          case "nvarchar":
            return SqlDataType.NVarChar;
          case "varbinary":
            return SqlDataType.VarBinary;
          case "varchar":
            return SqlDataType.VarChar;
          case "decimal":
            return SqlDataType.Decimal;
          case "numeric":
            return SqlDataType.Numeric;
          default:
            return SqlDataType.UserDefinedDataType;
        }


      }


    }

    private string _dataType = String.Empty;
    public string DataType
    {
      get { return _dataType; }
      set { _dataType = value; PerformVersioning("DataType", value); }
    }

    private string _oldDataType = String.Empty;
    public string OldDataType
    {
      get { return _oldDataType; }
      set { _oldDataType = value;}
    }


    private string _colId = String.Empty;
    public string ColId
    {
      get { return _colId; }
      set { _colId = value; PerformVersioning("ColId", value); }
    }

    private string _ruleBinding = String.Empty;
    public string RuleBinding
    {
      get
      { return _ruleBinding; }
      set { _ruleBinding = value; PerformVersioning("RuleBinding", value); }
    }

    private string _collation = String.Empty;
    public string Collation
    {
      get { return _collation; }
      set { _collation = value; PerformVersioning("Collation", value); }
    }

    private int _collationId;
    public int CollationId
    {
      get { return _collationId; }
      set { _collationId = value; PerformVersioning("CollationId", value); }
    }


    private string _description = String.Empty;
    public string Description
    {
      get { return _description; }
      set 
      { 
        _description = value; 
      }
    }

    private bool _hasDescriptionProperty;
    public bool HasDescriptionProperty
    {
      get { return _hasDescriptionProperty; }
      set { _hasDescriptionProperty = value; }
    }

    private string _oldDescription = String.Empty;
    public string OldDescription
    {
      get { return _oldDescription; }
      set { _oldDescription = value; }
    }

    public bool Changed
    {
      get
      {
        return _changedProps.Count > 0 ;
      }
    }

    /// <summary>
    /// Returns special characters removed version of name
    /// </summary>
    public string NormalizedName
    {
      get { return Utils.ReplaceQuatations(_name); }
    }

    /// <summary>
    /// Returns special characters removed version of oldName
    /// </summary>
    public string NormalizedOldName
    {
      get { return Utils.ReplaceQuatations(_oldName); }
    }

    /// <summary>
    /// [name]
    /// <para>Special characters not removed</para>
    /// </summary>
    public string QualifiedName
    {
      get { return Utils.Qualify(_name); }
    }

    public ColumnWrapper()
    {

    }

    #region Version Management
    private IDictionary<string, string> _initialValues = new Dictionary<string, string>();
    public void CollectInitialValues()
    {
      _initialValues.Clear();
      Type type = this.GetType();
      PropertyInfo[] props = type.GetProperties();
      foreach (PropertyInfo pi in props)
      {
        object value = pi.GetValue(this, null);
        _initialValues.Add(pi.Name, value != null ? value.ToString() : String.Empty);
      }
    }

    public void ResetChanges()
    {
      _initialValues.Clear();
      _changedProps.Clear();
    }


    private IList<string> _changedProps = new List<string>();
    public bool PropertyChanged(string propName)
    {
      return _changedProps.Contains(propName);
    }

    private void PerformVersioning(string propName, object value)
    {
      if (!_initialValues.ContainsKey(propName))
        return;

      string iVal = _initialValues[propName];
      if (iVal != (value != null ? value.ToString() : String.Empty))
      {
        if (!_changedProps.Contains(propName))
        {
          _changedProps.Add(propName);
        }
      }
      else
        _changedProps.Remove(propName);    
    }
    
    #endregion //Version Management

    public void MakeOldValuesActual()
    {
      this.OldDataType = this.DataType;
      this.OldDescription = this.OldDescription;
      this.OldIdentity = this.IsIdentity;
      this.OldName = this.Name;
    }

  }
}
