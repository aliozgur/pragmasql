using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class ObjectPrivilegesEdit : UserControl
  {
    #region Fields And properties

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = null;
          _cp = value.CreateCopy();
          PopulateObjectPrivileges();
        }
        else
        {
          _cp = null;
          bs.DataSource = null;
          _tbl.Clear();
        }
      }
    }

    private string _prinicpal = String.Empty;
    public string Principal
    {
      get { return _prinicpal; }
      set { _prinicpal = value; }
    }


    private DataTable _tbl = new DataTable();

    private bool _isInitializing = false;
    private IList<ObjectType> _modifiedViewList = new List<ObjectType>();
    private IDictionary<ObjectType, string> _filters = new Dictionary<ObjectType, string>();

    private ToolStripMenuItem _currentCheckedFilterItem = null;

    #endregion //Fields And properties

    #region CTOR

    public ObjectPrivilegesEdit( )
    {
      InitializeComponent();
      _currentCheckedFilterItem = rbAll;
      _filters.Add(ObjectType.InlinedFunction, GenerateObjectTypeFilter(ObjectType.InlinedFunction));
      _filters.Add(ObjectType.None, GenerateObjectTypeFilter(ObjectType.None));
      _filters.Add(ObjectType.Procedure, GenerateObjectTypeFilter(ObjectType.Procedure));
      _filters.Add(ObjectType.ScalarValuedFunction, GenerateObjectTypeFilter(ObjectType.ScalarValuedFunction));
      _filters.Add(ObjectType.Table, GenerateObjectTypeFilter(ObjectType.Table));
      _filters.Add(ObjectType.TableValueFunction, GenerateObjectTypeFilter(ObjectType.TableValueFunction));
      _filters.Add(ObjectType.View, GenerateObjectTypeFilter(ObjectType.View));
    }



    #endregion //CTOR

    private void CreatePrimaryKeys( )
    {
      DataColumn[] PrimaryKeyColumns = new DataColumn[1];
      PrimaryKeyColumns[0] = _tbl.Columns["objName"];
      _tbl.PrimaryKey = PrimaryKeyColumns;
    }

    private void SetModified( bool value )
    {
      lblModified.Visible = value;
    }

    private void AddToModifiedViewList( ObjectType type )
    {
      if (_modifiedViewList.Contains(type))
        return;
      _modifiedViewList.Add(type);
      UpdateModifiedLabel();
    }

    private void RemoveFromModifiedViewList( ObjectType type )
    {
      if (!_modifiedViewList.Contains(type))
        return;
      _modifiedViewList.Remove(type);
      UpdateModifiedLabel();
    }

    private void ClearModifiedViewsList( )
    {
      _modifiedViewList.Clear();
      UpdateModifiedLabel();
    }

    private void UpdateModifiedLabel( )
    {
      bool hasAnyMod = false;
      lblModified.Text = String.Empty;
      foreach (ObjectType type in _modifiedViewList)
      {
        lblModified.Text += type.ToString() + ", ";
        hasAnyMod = true;
      }

      if (hasAnyMod)
      {
        lblModified.Text = " Modified " + lblModified.Text.Remove(lblModified.Text.Length - 2, 2);
      }
    }

    private ObjectType GetCurrentObjectType( )
    {
      switch (comboBox1.SelectedIndex)
      {
        case 0:
          return ObjectType.Table;
        case 1:
          return ObjectType.View;
        case 2:
          return ObjectType.Procedure;
        case 3:
          return ObjectType.TableValueFunction;
        case 4:
          return ObjectType.ScalarValuedFunction;
        case 5:
          return ObjectType.InlinedFunction;
        default:
          return ObjectType.None;
      }
    }

    void _tbl_ColumnChanged( object sender, DataColumnChangeEventArgs e )
    {
      if (_isInitializing)
        return;

      switch (comboBox1.SelectedIndex)
      {
        case 0:
          AddToModifiedViewList(ObjectType.Table);
          break;
        case 1:
          AddToModifiedViewList(ObjectType.View);
          break;
        case 2:
          AddToModifiedViewList(ObjectType.Procedure);
          break;
        case 3:
          AddToModifiedViewList(ObjectType.TableValueFunction);
          break;
        case 4:
          AddToModifiedViewList(ObjectType.ScalarValuedFunction);
          break;
        case 5:
          AddToModifiedViewList(ObjectType.InlinedFunction);
          break;
        default:
          break;
      }

      SetModified(true);
    }

    private void PopulateObjectPrivileges( )
    {
      SqlDataReader reader = null;
      string action = String.Empty;
      string protectType = String.Empty;
      string objName = String.Empty;
      DataRow row = null;

      bs.DataSource = null;
      _tbl.Clear();
      _tbl.PrimaryKey = null;

      try
      {
        _isInitializing = true;

        using (SqlConnection conn = _cp.CreateSqlConnection(true))
        {
          _tbl = DbCmd.GetBaseDatabaseObjectsForPrivileges(conn);
          _tbl.ColumnChanged += new DataColumnChangeEventHandler(_tbl_ColumnChanged);
          CreatePrimaryKeys();

          try
          {
            reader = DbCmd.GetObjectPrivilegesAsDataReader(conn, _prinicpal);
            while (reader.Read())
            {
              if (!Utils.IsReaderItemValid(reader, "ProtectType") || !Utils.IsReaderItemValid(reader, "Object"))
                continue;

              protectType = (string)reader["ProtectType"];
              if (protectType.ToLowerInvariant().Trim() != "grant")
                continue;

              action = (string)reader["Action"];
              objName = (string)reader["Object"];

              row = _tbl.Rows.Find(objName);

              if (row == null)
                continue;

              row["can" + action.ToLowerInvariant()] = true;
              row["oldCan" + action.ToLowerInvariant()] = true;
            }
          }
          catch (Exception ex)
          {
            string errMsg = "Object privileges not available! Principal: {0}, Error message: {1}";
            errMsg = String.Format(errMsg, _prinicpal, ex.Message);
            HostServicesSingleton.HostServices.MsgService.ErrorMsg(errMsg, (MethodInfo)MethodInfo.GetCurrentMethod());
          }
          finally
          {
            if (reader != null)
              reader.Close();

            bs.DataSource = _tbl;
            bs.Filter = GetFilter(ObjectType.Table);
            comboBox1.SelectedIndex = 0;
            SetColumnVisibility(colObject, coldelete, colinsert, colreferences, colselect, colupdate);
          }
        }
      }
      finally
      {
        SetModified(false);
        _isInitializing = false;
      }

    }

    private string GetFilter( ObjectType type )
    {
      string filter2 = GetCheckedFilter();
      string result = String.Empty;


      if (!_filters.ContainsKey(type))
      {
        result = GenerateObjectTypeFilter(type);
        _filters.Add(type, result);
      }

      if (!String.IsNullOrEmpty(filter2))
      {
        result = _filters[type] + ( !String.IsNullOrEmpty(_filters[type]) ? " AND ": String.Empty) + "( " +filter2 + " ) ";
      }
      else
      {
        result = _filters[type];
      }

      return result;
    }

    private string GenerateObjectTypeFilter( ObjectType type )
    {
      string result = String.Empty;
      switch (type)
      {
        case ObjectType.Table:
          result = " ( objType = 'U' or objType = 'S' ) ";
          break;
        case ObjectType.View:
          result = " ( objType = 'V' ) ";
          break;
        case ObjectType.Procedure:
          result = " ( objType = 'P' or objType = 'X' ) ";
          break;
        case ObjectType.TableValueFunction:
          result = " ( objType = 'TF' ) ";
          break;
        case ObjectType.ScalarValuedFunction:
          result = " ( objType = 'FN' ) ";
          break;
        case ObjectType.InlinedFunction:
          result = " ( objType = 'IF' ) ";
          break;
        default:
          break;
      }
      return result;
    }

    private string GetCheckedFilter( )
    {
      string result = String.Empty;
      if (rbAll.Checked)
        return result;

      string[] predefinedPrivileges = new string[]
      {
        "references",
        "select",
        "insert",
        "delete",
        "update",
        "execute"
      };


      string conditionValue = String.Empty;
      string operatorValue = String.Empty;
      string tmp = String.Empty;

      if (rbChecked.Checked)
      {
        conditionValue = " true ";
        operatorValue = " OR ";
      }
      else if (rbUnchecked.Checked)
      {
        conditionValue = " false ";
        operatorValue = " AND ";
      }

      foreach (string privilege in predefinedPrivileges)
      {
        result += (!String.IsNullOrEmpty(tmp) ? tmp : String.Empty) + " ISNULL(can" + privilege + ", false)=" + conditionValue;
        tmp = operatorValue;
      }

      return result;
    }

    private void SetColumnVisibility( params DataGridViewColumn[] visibleCols )
    {
      foreach (DataGridViewColumn col in grd.Columns)
      {
        col.Visible = Array.IndexOf(visibleCols, col) >= 0 ? true : false;
      }
    }

    private void UpdatePrivileges( bool updateAll )
    {
			grd.EndEdit();
			string objName = String.Empty;
      string owner = String.Empty;
      string filter = String.Empty;
      bool hasError = false;
      string errorMsg = "Can not update some object privileges!\r\n";
      try
      {
        _isInitializing = true;
        filter = bs.Filter;

        using (SqlConnection conn = _cp.CreateSqlConnection(true))
        {
          foreach (ObjectType type in _filters.Keys)
          {
            if (!updateAll && type != GetCurrentObjectType())
              continue;

            bs.Filter = _filters[type];
            RemoveFromModifiedViewList(type);
            foreach (DataRow row in _tbl.Rows)
            {
              if (!Utils.IsRowItemValid(row, "objName") || !Utils.IsRowItemValid(row, "owner"))
                continue;

              objName = (string)row["objName"];
              owner = (string)row["owner"];
              try
              {
                TryToUpdateObjectPrivileges(conn, row, objName, owner);
              }
              catch (Exception ex)
              {
                errorMsg += "- Error: " + ex.Message + "\r\n";
                hasError = true;
              }
            }
          }
        }
      }
      finally
      {
        if (_modifiedViewList.Count == 0)
          SetModified(false);

        bs.Filter = filter;

        _isInitializing = false;
				if (hasError)
				{
					GenericErrorDialog.ShowError("PragmaSQL Error", "Some privileges can not be updated.", errorMsg);
				}
			}
    }

    private void TryToUpdateObjectPrivileges( SqlConnection conn, DataRow row, string objName, string owner )
    {
      string[] predefinedPrivileges = new string[]
      {
        "references",
        "select",
        "insert",
        "delete",
        "update",
        "execute"
      };

      foreach (string privilege in predefinedPrivileges)
      {
        if (row["can" + privilege] == row["oldCan" + privilege])
        {
          continue;
        }

        if (!Utils.IsRowItemValid(row, "can" + privilege))
          continue;

        bool value = (bool)row["can" + privilege];
        if (value)
        {
          DbCmd.GrantObjectPrivilege(conn, _prinicpal, owner, privilege, objName);
        }
        else
        {
          DbCmd.RevokeObjectPrivilege(conn, _prinicpal, owner, privilege, objName);
        }
        row["oldCan" + privilege] = value;
      }
    }

    private void ChangeSelectedObjectPrivileges()
    {
      if (grd.SelectedRows.Count == 0)
        return;

      PrivilegeTypes result = PrivilegeTypes.None;
      if (!frmChangePrivileges.ShowChangePrivilegesDlg(GetVisiblePrivilegeTypes(), ref result))
        return;

      foreach (DataGridViewRow row in grd.SelectedRows)
      {

        bool isNone = (result & PrivilegeTypes.None) != PrivilegeTypes.None;

        if(colselect.Visible)
        {
          if ( isNone || (result & PrivilegeTypes.Select) != PrivilegeTypes.Select)
            row.Cells[colselect.Name].Value = false;
          else
            row.Cells[colselect.Name].Value = true;
        }

        if (colexecute.Visible)
        {
          if (isNone || (result & PrivilegeTypes.Execute) != PrivilegeTypes.Execute)
            row.Cells[colexecute.Name].Value = false;
          else
            row.Cells[colexecute.Name].Value = true;
        }

        if (colreferences.Visible)
        {
          if (isNone || (result & PrivilegeTypes.Refs) != PrivilegeTypes.Refs)
            row.Cells[colreferences.Name].Value = false;
          else
            row.Cells[colreferences.Name].Value = true;
        }

        if (colinsert.Visible)
        {
          if (isNone || (result & PrivilegeTypes.Insert) != PrivilegeTypes.Insert)
            row.Cells[colinsert.Name].Value = false;
          else
            row.Cells[colinsert.Name].Value = true;
        }
        
        if (colupdate.Visible)
        {
          if (isNone || (result & PrivilegeTypes.Update) != PrivilegeTypes.Update)
            row.Cells[colupdate.Name].Value = false;
          else
            row.Cells[colupdate.Name].Value = true;
        }

        if (coldelete.Visible)
        {
          if (isNone || (result & PrivilegeTypes.Delete) != PrivilegeTypes.Delete)
            row.Cells[coldelete.Name].Value = false;
          else
            row.Cells[coldelete.Name].Value = true;
        }
      }
    }

    private PrivilegeTypes GetVisiblePrivilegeTypes()
    {
      PrivilegeTypes result = PrivilegeTypes.None;
      if (colselect.Visible)
      {
        result = result | PrivilegeTypes.Select;
      }

      if (colexecute.Visible)
      {
        result = result | PrivilegeTypes.Execute;
      }

      if (colreferences.Visible)
      {
        result = result | PrivilegeTypes.Refs;
      }

      if (colinsert.Visible)
      {
        result = result | PrivilegeTypes.Insert;
      }

      if (colupdate.Visible)
      {
        result = result | PrivilegeTypes.Update;
      }

      if (coldelete.Visible)
      {
        result = result | PrivilegeTypes.Delete;
      }

      return result;
    }

    private void comboBox1_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_isInitializing)
        return;

      switch (comboBox1.SelectedIndex)
      {
        case 0:
          bs.Filter = GetFilter(ObjectType.Table);
          SetColumnVisibility(colObject, coldelete, colinsert, colreferences, colselect, colupdate);
          btnUpdate.Text = "Update Table Privileges";
          break;
        case 1:
          bs.Filter = GetFilter(ObjectType.View);
          SetColumnVisibility(colObject, coldelete, colreferences, colinsert, colselect, colupdate);
          btnUpdate.Text = "Update View Privileges";
          break;
        case 2:
          bs.Filter = GetFilter(ObjectType.Procedure);
          SetColumnVisibility(colObject, colexecute);
          btnUpdate.Text = "Update Procedure Privileges";
          break;
        case 3:
          bs.Filter = GetFilter(ObjectType.TableValueFunction);
          SetColumnVisibility(colObject, colreferences, colselect);
          btnUpdate.Text = "Update Function Privileges";
          break;
        case 4:
          bs.Filter = GetFilter(ObjectType.ScalarValuedFunction);
          SetColumnVisibility(colObject, colreferences, colexecute);
          btnUpdate.Text = "Update Function Privileges";
          break;
        case 5:
          bs.Filter = GetFilter(ObjectType.InlinedFunction);
          SetColumnVisibility(colObject, colreferences, colselect,colupdate);
          btnUpdate.Text = "Update Function Privileges";
          break;
        default:
          break;
      }
    }

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      UpdatePrivileges(false);
    }

    private void btnUpdateAll_Click( object sender, EventArgs e )
    {
      UpdatePrivileges(true);
    }

    private void OnShowFilterClicked(object sender, EventArgs e)
    {
			if (bs.DataSource == null)
			{
				return;
			}
			
			ToolStripMenuItem mnuItem = sender as ToolStripMenuItem;
      if (mnuItem == null || mnuItem.Tag == null)
        return;

      if (mnuItem == _currentCheckedFilterItem)
        return;

      if (_currentCheckedFilterItem != null)
        _currentCheckedFilterItem.Checked = false;

      switch (mnuItem.Tag.ToString())
      {
        case "all":
          rbAll.Checked = true;
          _currentCheckedFilterItem = rbAll;
          break;
        case "checked":
          rbChecked.Checked = true;
          _currentCheckedFilterItem = rbChecked;
          break;
        case "unchecked":
          rbUnchecked.Checked = true;
          _currentCheckedFilterItem =  rbUnchecked;
          break;
        default:
					throw new Exception("Unknown filter tag!");
      }

			btnShowFilter.Text = "Show " + mnuItem.Text;
			bs.Filter = GetFilter(GetCurrentObjectType());
    }

    private void btnChangePrivileges_Click(object sender, EventArgs e)
    {
      ChangeSelectedObjectPrivileges();
    }
  }


}
