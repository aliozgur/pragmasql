/********************************************************************
  Class      : EditorPopupBuilder
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using System.Windows.Forms;

using PragmaSQL.Common;
using PragmaSQL.Database;

namespace PragmaSQL
{
  public enum MergeType
  {
    Insert,
    Append
  }

  public enum ActionType
  {
    None,
    Modify,
    Open,
    Execute,
    References,
    Permissions,
    TablePermissions,
    ColumnPermissions,
    ForeignKeys,
    ForeignKeysIn,
    Constraints,
    IdentityColumns,
    UsedSpace,
    Statistics,
    ObjectHelp,
    Dependencies,
    AppendColsAsSelectList,
    AppendColsAsParamList1,
    AppendColsAsParamList2,
    AppendProcedureParams,
    AppendFunctionParams,
    AppendProcedureParamsWithDataType,
    AppendFunctionParamsWithDataType,
    AppendFunctionResultAsLocalTable
  }

  

  public class ObjectHelpActionEventArgs : EventArgs
  {
    public readonly ObjectInfo objectInfo = new ObjectInfo();
    public bool cancel = false;
    public ActionType ActionType = ActionType.None;
  }

  public delegate void ObjectHelpActionEventHandler( object sender, ObjectHelpActionEventArgs e );


  /// <summary>
  /// - Edit object
  /// - Open object
  /// - *********************************************
  /// - Table Permissions
  /// - Column Permissions
  /// - Permissions 
  /// - Object Info
  ///   - Foreign Keys
  ///   - Foreign Keys In
  ///   - Constraints
  ///   - Identity Columns
  ///   - Used Space
  ///   - Statistics
  ///  - Object Help
  ///  - Dependencies
  ///  - *********************************************
  ///  - Insert Columns As Select List
  ///  - Insert Columns As Param List
  ///  - Insert Columns As Param List (No Datatype)
  ///  - Insert Procedure Params
  ///  - Insert Function Params
  ///  - *********************************************
  /// </summary>
  public class ObjectHelperPopupBuilder
  {
    private IList<ToolStripItem> _items = new List<ToolStripItem>();
    private ToolStripItem[] _sourceItems = null;
    
    private SqlConnection _conn = null;

    #region Fields and Properties
    private ContextMenuStrip _menuStrip = null;
    
    public ContextMenuStrip MenuStrip
    {
      get { return _menuStrip; }
      set 
      { 
        _menuStrip = value; 
        _sourceItems = new ToolStripItem[_menuStrip.Items.Count];
        
        for(int i= 0; i < _menuStrip.Items.Count;i++)
        {
          _sourceItems[i] = _menuStrip.Items[i];
        }

      }
    }

    private string _wordAtCursor = String.Empty;
    public string wordAtCursor
    {
      get { return _wordAtCursor; }
      set { _wordAtCursor = value; }
    }

    private MergeType _buildType = MergeType.Insert;
    public MergeType BuildType
    {
      get { return _buildType; }
      set { _buildType = value; }
    }

    private ObjectInfo _objectInfo = new ObjectInfo();
    public ObjectInfo ObjectInfo
    {
      get { return _objectInfo; }
      set { _objectInfo = value; }
    }

    private ObjectHelpActionEventHandler _objectHelpRequested;
    public event ObjectHelpActionEventHandler ObjectHelpRequested
    {
      add
      {
        _objectHelpRequested += value;
      }
      remove
      {
        _objectHelpRequested -= value;
      }
    }

    #endregion //Fields and Properties


    #region Constructor
    public ObjectHelperPopupBuilder( ContextMenuStrip menuStrip )
    {
      MenuStrip = menuStrip;
    }

    #endregion //Constructor

    #region Methods

    public void Cleanup( )
    {
      _objectInfo = null;

      if (_menuStrip == null)
      {
        return;
      }

      foreach (ToolStripItem item in _items)
      {
        _menuStrip.Items.Remove(item);
      }

    }

    public void BuildMenuItems( SqlConnection conn, MergeType buildType, string wordAtCursor )
    {
      if (_menuStrip == null)
      {
        return;
      }

      if (conn == null)
      {
        throw new NullParameterException("Connection param is null!");
      }

      if (conn.State != System.Data.ConnectionState.Open)
      {
        throw new InvalidConnectionState("Invalid connection state! State = \"" + conn.State.ToString() + "\".");
      }

      _conn = conn;
      _wordAtCursor = wordAtCursor;
      Cleanup();

      GetObjectInfo();
      if (_objectInfo == null)
      {
        return;
      }

      CreateMenuItems();
    }

    private void GetObjectInfo( )
    {
      string script = ResManager.GetDBScript("Script_GetObjectInfoByName");
      script = String.Format(script,_wordAtCursor);
      SqlCommand cmd = new SqlCommand(script, _conn);
      cmd.CommandTimeout = 0;
      SqlDataReader reader = cmd.ExecuteReader();
      try
      {
        while (reader.Read())
        {
          _objectInfo = new ObjectInfo();
          _objectInfo.ObjectID = (int)reader["id"];
          _objectInfo.ObjectName = (string)reader["name"];
          _objectInfo.ObjectTypeAbb = (string)reader["xtype"];
          _objectInfo.ObjectType = DBConstants.GetDBObjectType((reader["xtype"] as string));
        }
      }
      finally
      {
        reader.Close();
      }
    }

    private void CreateMenuItems( )
    {
      switch (_objectInfo.ObjectType)
      {
        case DBObjectType.UserTable:
          CreateTableRelatedMenuItems();
          break;
        case DBObjectType.SystemTable:
          CreateTableRelatedMenuItems();
          break;
        case DBObjectType.View:
          CreateViewRelatedMenuItems();
          break;
        case DBObjectType.Trigger:
          CreateTriggerRelatedMenuItems();
          break;
        case DBObjectType.StoredProc:
          CreateStoredProcRelatedMenuItems();
          break;
        case DBObjectType.TableValuedFunction:
          CreateFunctionRelatedMenuItems();
          break;
        case DBObjectType.ScalarValuedFunction:
          CreateFunctionRelatedMenuItems();
          break;
        default:
          break;
      }
    }

    private void AddSep()
    {
      ToolStripItem item = _menuStrip.Items.Add("-",null,null); 
      _items.Add(item);
    }

    private ToolStripMenuItem AddMenuItem(string name, ActionType actionType)
    {
      ToolStripMenuItem result = (ToolStripMenuItem)_menuStrip.Items.Add(name,null,OnMenuItemClick);
      result.Tag = actionType;
      _items.Add(result);

      return result; 
    }
    
    private ToolStripMenuItem AddSubMenu(string name)
    {
      ToolStripMenuItem result = (ToolStripMenuItem)_menuStrip.Items.Add(name,null,null);
      _items.Add(result);
      return result;
    }

    private ToolStripMenuItem AddSubMenuItem(ToolStripMenuItem subMenu,string name,ActionType actionType)
    {
      ToolStripMenuItem  result = (ToolStripMenuItem)subMenu.DropDownItems.Add(name,null,OnMenuItemClick);
      result.Tag = actionType;
      return result; 
    }

    /// <summary>
    /// - Open
    /// - References
    /// - *********************************************
    /// - Permissions
    /// - Column Permissions
    /// - Object Info
    ///   - Foreign Keys
    ///   - Foreign Keys In
    ///   - Constraints
    ///   - Identity Columns
    ///   - Used Space
    ///   - Statistics
    /// - Object Help
    /// - Dependencies
    /// - *********************************************
    /// - Insert Columns As Select List
    /// - Insert Columns As Param List
    /// - Insert Columns As Param List (No Datatype)
    /// - *********************************************    
    /// </summary>
    private void CreateTableRelatedMenuItems( )
    {
      if(_buildType == MergeType.Insert)
      {
        _menuStrip.Items.Clear();
      }
      else
      {
        AddSep();
      }

      ToolStripMenuItem mnuItem = null;

      ToolStripMenuItem item = AddMenuItem("Open",ActionType.Open);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.OpenSelectedObject;

      item = AddMenuItem("References",ActionType.References);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListReferences;

      item = AddMenuItem("Permissions",ActionType.TablePermissions);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListPermissions;

      
      mnuItem = AddSubMenu("Object Info");
      AddSubMenuItem(mnuItem,"Foreign Keys",ActionType.ForeignKeys);
      AddSubMenuItem(mnuItem,"Foreign Keys In",ActionType.ForeignKeysIn);
      AddSubMenuItem(mnuItem,"Constraints",ActionType.Constraints);
      AddSubMenuItem(mnuItem,"Identity Columns",ActionType.IdentityColumns);
      AddSubMenuItem(mnuItem,"Used Space",ActionType.UsedSpace);
      AddSubMenuItem(mnuItem,"Statistics",ActionType.Statistics);
      
      item = AddMenuItem("Object Help",ActionType.ObjectHelp);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ObjectHelp;

      item = AddMenuItem("Dependencies",ActionType.Dependencies);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListDependencies;
      AddSep();

      AddMenuItem("Append Cols As Select List",ActionType.AppendColsAsSelectList);
      AddMenuItem("Append Cols As Param List ",ActionType.AppendColsAsParamList1);
      AddMenuItem("Append Cols As Param List (No DataType)",ActionType.AppendColsAsParamList2);

      if(_buildType == MergeType.Insert)
      {
        AddSep();
        _menuStrip.Items.AddRange(_sourceItems);
      }
    }

    /// <summary>
    /// - Modify
    /// - Open
    /// - References
    /// - *********************************************
    /// - Permissions
    /// - Column Permissions
    /// - *********************************************
    /// - Constraints
    /// - Object Help
    /// - Dependencies
    /// - *********************************************
    /// - Insert Columns As Select List
    /// - Insert Columns As Param List
    /// - Insert Columns As Param List (No Datatype)
    /// - *********************************************    
    /// </summary>    
    private void  CreateViewRelatedMenuItems()
    {
      if(_buildType == MergeType.Insert)
      {
        _menuStrip.Items.Clear();
      }
      else
      {
        AddSep();
      }

      ToolStripMenuItem item = AddMenuItem("Modify",ActionType.Modify);
      item.ShortcutKeys =  ScriptEditorShortcutKeysProvider.ModifySelectedObject;

      item = AddMenuItem("Open",ActionType.Open);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.OpenSelectedObject;

      item = AddMenuItem("References",ActionType.References);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListReferences;

      item = AddMenuItem("Permissions",ActionType.TablePermissions);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListPermissions;

      AddSep();      
      AddMenuItem("Constraints",ActionType.Constraints);
      
      item = AddMenuItem("Object Help",ActionType.ObjectHelp);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ObjectHelp;

      item = AddMenuItem("Dependencies",ActionType.Dependencies);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListDependencies;
      AddSep();
      AddMenuItem("Append Cols As Select List",ActionType.AppendColsAsSelectList);
      AddMenuItem("Append Cols As Param List",ActionType.AppendColsAsParamList1);
      AddMenuItem("Append Cols As Param List (No DataType)",ActionType.AppendColsAsParamList2);

      if(_buildType == MergeType.Insert)
      {
        AddSep();
        _menuStrip.Items.AddRange(_sourceItems);
      }
    }


    /// <summary>
    /// - Modify
    /// - References
    /// - Permissions
    /// - *********************************************
    /// - Constraints
    /// - Object Help
    /// - Dependencies
    /// - *********************************************
    /// - Insert Columns As Select List
    /// - Insert Columns As Param List
    /// - Insert Columns As Param List (No Datatype)
    /// - *********************************************    
    /// </summary>    
    private void CreateStoredProcRelatedMenuItems( )
    {
      if(_buildType == MergeType.Insert)
      {
        _menuStrip.Items.Clear();
      }
      else
      {
        AddSep();
      }

      ToolStripMenuItem item = AddMenuItem("Modify",ActionType.Modify);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ModifySelectedObject;
      
      item = AddMenuItem("Execute",ActionType.Execute);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.OpenProcExecScript;
      AddSep();

      item = AddMenuItem("References",ActionType.References);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListReferences;

      item = AddMenuItem("Permissions",ActionType.Permissions);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListPermissions;

      AddSep();      
      item = AddMenuItem("Object Help",ActionType.ObjectHelp);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ObjectHelp;

      item = AddMenuItem("Dependencies",ActionType.Dependencies);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListDependencies;
      AddSep();
      AddMenuItem("Append Params",ActionType.AppendProcedureParamsWithDataType);
      AddMenuItem("Append Params (No DataType)",ActionType.AppendProcedureParams);

      if(_buildType == MergeType.Insert)
      {
        AddSep();
        _menuStrip.Items.AddRange(_sourceItems);
      }

    }

    /// <summary>
    /// - Modify
    /// - References
    /// - Permissions
    /// - *********************************************
    /// - Constraints
    /// - Object Help
    /// - Dependencies
    /// </summary>    
    private void CreateTriggerRelatedMenuItems( )
    {
      if(_buildType == MergeType.Insert)
      {
        _menuStrip.Items.Clear();
      }
      else
      {
        AddSep();
      }

      ToolStripMenuItem item = AddMenuItem("Modify",ActionType.Modify);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ModifySelectedObject;

      item = AddMenuItem("Object Help",ActionType.ObjectHelp);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ObjectHelp;

      if(_buildType == MergeType.Insert)
      {
        AddSep();
        _menuStrip.Items.AddRange(_sourceItems);
      }

    }
    private void CreateFunctionRelatedMenuItems( )
    {
      if(_buildType == MergeType.Insert)
      {
        _menuStrip.Items.Clear();
      }
      else
      {
        AddSep();
      }

      ToolStripMenuItem item = AddMenuItem("Modify",ActionType.Modify);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ModifySelectedObject;

      item = AddMenuItem("References",ActionType.References);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListReferences;

      item = AddMenuItem("Permissions",ActionType.Permissions);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListPermissions;

      AddSep();      
      item = AddMenuItem("Object Help",ActionType.ObjectHelp);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ObjectHelp;

      item = AddMenuItem("Dependencies",ActionType.Dependencies);
      item.ShortcutKeys = ScriptEditorShortcutKeysProvider.ListDependencies;

      AddSep();
      AddMenuItem("Append Params",ActionType.AppendFunctionParamsWithDataType);
      AddMenuItem("Append Params (No DataType)",ActionType.AppendFunctionParams);
      if(_objectInfo.ObjectType == DBObjectType.TableValuedFunction)
      {
        AddMenuItem("Append Result As Local Table",ActionType.AppendFunctionResultAsLocalTable);
      }

      if(_buildType == MergeType.Insert)
      {
        AddSep();
        _menuStrip.Items.AddRange(_sourceItems);
      }

    }

    private void OnMenuItemClick(object sender, EventArgs e)
    {
      ToolStripItem item = sender as ToolStripItem;
      if(item == null)
      {
        return;
      }

      if(_objectHelpRequested != null && _objectInfo != null)
      {
        ObjectHelpActionEventArgs args = new ObjectHelpActionEventArgs();
        args.ActionType =  (ActionType)item.Tag;
        args.objectInfo.CopyFrom(_objectInfo);
        _objectHelpRequested(sender,args);
      }

    }
    #endregion //Methods


  }
}
