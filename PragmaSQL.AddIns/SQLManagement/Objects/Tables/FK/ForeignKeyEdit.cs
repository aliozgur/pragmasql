using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using Crad.Windows.Forms.Actions;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class ForeignKeyEdit : UserControl
  {
    #region Fields And Properties
    ActionList _actions = new ActionList();

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
          _cp = null;
        }
      }
    }

    private TableKeyEditorMode _mode;
    public TableKeyEditorMode Mode
    {
      get { return _mode; }
      set { _mode = value; ApplyMode(); }
    }

    private ForeignKeyWrapper _newKey = null;
    public ForeignKeyWrapper SelectedKey
    {
      get
      {
        if (_newKey == null)
        {
          return cmbFk.SelectedItem as ForeignKeyWrapper;
        }
        else
        {
          return _newKey;
        }
      }
    }

    public string SelectedHostTable
    {
      set
      {
        cmbHostTables.SelectedIndex = cmbHostTables.FindStringExact(value);
      }
    }

    private bool _initializing = false;
    private TableWrapper _hostTable;
    public bool HasHostTable
    {
      get { return _hostTable != null; }
    }

    private bool _canUserAddNewKey = true;
    public bool CanUserAddNewKey
    {
      get 
      { 
        return _canUserAddNewKey; 
      }
      set 
      { 
        _canUserAddNewKey = value;
        btnNew.Visible = value;
      }
    }

    #endregion //Fields And Properties

    #region CTOR
    public ForeignKeyEdit( )
    {
      InitializeComponent();
      InitializeActions();
      CreateColsTable();
      CreateHostColsTable();
    }

    private void ApplyMode( )
    {
    }

    #endregion //CTOR

    #region Events
    private ForeignKeyEditorEventHandler _afterKeySaved;
    public event ForeignKeyEditorEventHandler AfterKeySaved
    {
      add { _afterKeySaved += value; }
      remove { _afterKeySaved -= value; }
    }

    private ForeignKeyEditorEventHandler _afterNewKeySaved;
    public event ForeignKeyEditorEventHandler AfterNewKeySaved
    {
      add { _afterNewKeySaved += value; }
      remove { _afterNewKeySaved -= value; }
    }

    private ForeignKeyEditorEventHandler _afterKeyDropped;
    public event ForeignKeyEditorEventHandler AfterKeyDropped
    {
      add { _afterKeyDropped += value; }
      remove { _afterKeyDropped -= value; }
    }

    private ForeignKeyEditorEventHandler _afterKeyRenamed;
    public event ForeignKeyEditorEventHandler AfterKeyRenamed
    {
      add { _afterKeyRenamed += value; }
      remove { _afterKeyRenamed -= value; }
    }

    private ForeignKeyEditorEventHandler _afterKeyStateChanged;
    public event ForeignKeyEditorEventHandler AfterKeyStateChanged
    {
      add { _afterKeyStateChanged += value; }
      remove { _afterKeyStateChanged -= value; }
    }

    private ForeignKeyEditorEventHandler _afterNewKeyCreated;
    public event ForeignKeyEditorEventHandler AfterNewKeyCreated
    {
      add { _afterNewKeyCreated += value; }
      remove { _afterNewKeyCreated -= value; }
    }

    #endregion //Events

    #region Initialization

    public void InitializeForeignKeys( TableKeyEditorMode mode, TableWrapper hostTable, bool renderLast )
    {
      try
      {
        _initializing = true;

        Mode = mode;
        
        _hostTable = null;
        _newKey = null;
        _hostTable = hostTable;
        
        cmbFk.Enabled = true;

        // Populate host tables. If host table is not null select proper table
        DbCmd.PopulateUserDefinedTablesCombo(cmbHostTables, _cp, ((_hostTable != null && Mode == TableKeyEditorMode.SingleTable) ? _hostTable.ID : -1));
        
        if (_hostTable != null)
        {
          cmbHostTables.SelectedIndex = cmbHostTables.FindStringExact(_hostTable.Name);
          PopulateHostColumnsCombo();
        }
        cmbHostTables.Enabled = (_hostTable != null && Mode == TableKeyEditorMode.SingleTable);

        //Populate reference tables combo
        DbCmd.PopulateUserDefinedTablesCombo(cmbRefTables, _cp, -1);

        //Populate foreign keys combo box
        DbCmd.PopulateForeignKeysComboSimple(cmbFk, _cp,_hostTable  != null ? _hostTable.ID : - 1);

        //Select first item in the foreign keys combo box
        if (cmbFk.Items.Count > 0)
        {
          if( renderLast)
            cmbFk.SelectedIndex = cmbFk.Items.Count - 1;
          else
            cmbFk.SelectedIndex = 0;

          ForeignKeyWrapper fk = cmbFk.SelectedItem as ForeignKeyWrapper;
          if (fk != null && !fk.AllPropsLoaded)
            fk.LoadAllProperties();

          RenderKeyProperties();
        }
        else
        {
          cmbFk.SelectedIndex = -1;
        }
      }
      finally
      {
        _initializing = false;
        if(cmbFk.Items.Count == 0)
          InitializeNewKey();
      }
    }

    public void InitializeForeignKeys( TableKeyEditorMode mode, TableWrapper hostTable, long keyid)
    {
      try
      {
        _initializing = true;


        Mode = mode;
        
        cmbFk.Items.Clear();

        _hostTable = null;
        _newKey = null;
        _hostTable = hostTable;

        cmbFk.Enabled = true;

        // Populate host tables. If host table is not null select proper table
        DbCmd.PopulateUserDefinedTablesCombo(cmbHostTables, _cp, ((_hostTable != null && Mode == TableKeyEditorMode.SingleTable) ? _hostTable.ID : -1));

        if (_hostTable != null)
        {
          cmbHostTables.SelectedIndex = cmbHostTables.FindStringExact(_hostTable.Name);
          PopulateHostColumnsCombo();
        }
        cmbHostTables.Enabled = (_hostTable != null && Mode == TableKeyEditorMode.SingleTable);

        //Populate reference tables combo
        DbCmd.PopulateUserDefinedTablesCombo(cmbRefTables, _cp, -1);

        ForeignKeyWrapper fk = new ForeignKeyWrapper(_cp);
        fk.ID = keyid;
        fk.LoadProperties();
        fk.GetKey();

        cmbFk.Items.Add(fk);
        cmbFk.SelectedIndex = 0;
        RenderKeyProperties();
      }
      finally
      {
        _initializing = false;
        if (cmbFk.Items.Count == 0)
          InitializeNewKey();
      }
    }


    public void ExternalCreateNewKey( )
    {
      _hostTable = null;
      cmbFk.Items.Clear();
      cmbFk.Enabled = true;

      // Populate host tables. If host table is not null select proper table
      DbCmd.PopulateUserDefinedTablesCombo(cmbHostTables, _cp, -1);
      if (cmbHostTables.Items.Count > 0)
        cmbHostTables.SelectedIndex = 0;
      
      cmbHostTables.Enabled = true;

      //Populate reference tables combo
      DbCmd.PopulateUserDefinedTablesCombo(cmbRefTables, _cp, -1);
      InitializeNewKey();
    }

    public void RefreshForeignKeys( TableWrapper hostTable )
    {
      InitializeForeignKeys(_mode, hostTable,false);
    }

    public void ResetEditor( )
    {
      _hostTable = null;
      cmbFk.Items.Clear();
      cmbHostTables.Items.Clear();
      cmbRefTables.Items.Clear();
      cmbRefPk.Items.Clear();
      ClearKeyPropertyControls();
    }

    #endregion //Initialization

    #region Columns Related Operations

    DataTable _tblCols = new DataTable();

    private void CreateColsTable( )
    {
      _tblCols.Columns.Clear();

      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "hostCol";
      _tblCols.Columns.Add(column);


      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "refCol";
      _tblCols.Columns.Add(column);
    }

    private void HandleReferencePrimaryKeyChanged( )
    {
      _tblCols.Clear();

      TableWrapper hostTable = cmbRefTables.SelectedItem as TableWrapper;
      NameIdPair pkConstraint = cmbRefPk.SelectedItem as NameIdPair;
      if (hostTable == null || pkConstraint == null)
        return;

      DataTable tbl = DbCmd.GetColumnsByForeignKey(_cp, hostTable.ID, pkConstraint.Id);
      DataRow newRow = null;
      foreach (DataRow row in tbl.Rows)
      {
        newRow = _tblCols.NewRow();
        newRow["refCol"] = row["colName"];
        _tblCols.Rows.Add(newRow);
      }
      bsCols.DataSource = _tblCols;
    }

    private void PopulateColumns( )
    {
      _tblCols.Clear();
      TableWrapper hostTable = cmbRefTables.SelectedItem as TableWrapper;
      NameIdPair pkConstraint = cmbRefPk.SelectedItem as NameIdPair;
      ForeignKeyWrapper fk = cmbFk.SelectedItem as ForeignKeyWrapper;


      if (hostTable == null || pkConstraint == null || fk == null)
        return;

      DataTable tbl = DbCmd.GetColumnsByForeignKey(_cp, hostTable.ID, pkConstraint.Id);
      DataRow newRow = null;
      foreach (DataRow row in tbl.Rows)
      {
        newRow = _tblCols.NewRow();
        newRow["refCol"] = row["colName"];
        newRow["hostCol"] = fk.GetColumns((string)row["colName"]);
        _tblCols.Rows.Add(newRow);
      }
      bsCols.DataSource = _tblCols;
    }

    DataTable _tblHostCols = new DataTable();
    private void CreateHostColsTable( )
    {
      _tblHostCols.Columns.Clear();

      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "colName";
      _tblHostCols.Columns.Add(column);
    }

    private void PopulateHostColumnsCombo( )
    {
      _tblCols.Clear();
      colHostColumns.DataSource = null;
      _tblHostCols.Clear();

      TableWrapper hostTable = cmbHostTables.SelectedItem as TableWrapper;
      if (hostTable == null)
        return;

      DataRow row = null;
      DataTable tbl = DbCmd.GetColumnsSimple(_cp, hostTable.NormalizedFullName);

      foreach (DataRow colRow in tbl.Rows)
      {
        row = _tblHostCols.NewRow();
        row["colName"] = colRow["colName"];
        _tblHostCols.Rows.Add(row);
      }

      colHostColumns.DataSource = _tblHostCols;
      colHostColumns.DisplayMember = "colName";
      colHostColumns.ValueMember = "colName";
    }

    private void PopulateReferenceTablePrimaryKeys( string refPk )
    {
      TableWrapper refTbl = cmbRefTables.SelectedItem as TableWrapper;
      if (refTbl == null)
      {
        cmbRefPk.Items.Clear();
        return;
      }

      _tblCols.Clear();
      DbCmd.PopulateReferenceTablePKCombo(cmbRefPk, _cp, refTbl.ID);
      if (!String.IsNullOrEmpty(refPk))
      {
        cmbRefPk.SelectedIndex = cmbRefPk.FindStringExact(refPk);
      }
      else if (cmbRefPk.Items.Count > 0)
      {
        cmbRefPk.SelectedIndex = 0;
      }
    }

    private void GenerateCols( out string hostCols, out string refCols )
    {
      hostCols = String.Empty;
      refCols = String.Empty;
      string seperator = String.Empty;

      foreach (DataRow row in _tblCols.Rows)
      {
        hostCols += seperator + (string)row["hostCol"];
        refCols += seperator + (string)row["refCol"];
        seperator = ", ";
      }

    }

    #endregion //Columns Related Operations

    #region Key Property Related Operations

    private void RenderKeyProperties( )
    {
      ForeignKeyWrapper fk = cmbFk.SelectedItem as ForeignKeyWrapper;
      if (fk == null)
      {
        ClearKeyPropertyControls();
        return;
      }

      //Select host table from combo
      cmbHostTables.SelectedIndex = cmbHostTables.FindStringExact(fk.HostTable.Name);

      txtName.Text = fk.Name;
      txtName.ReadOnly = true;
      chkCascadeUpdate.Checked = fk.CascadeUpdate;
      chkCascadeDelete.Checked = fk.CascadeDelete;
      chkNotForRep.Checked = fk.NotForReplication;

      //Select reference table from combo
      cmbRefTables.SelectedIndex = cmbRefTables.FindStringExact(fk.RefTable.Name);

      //Populate reference primary keys and select exact key in combo
      PopulateReferenceTablePrimaryKeys(fk.ReferencedKey.Name);

      //Populate columns
      PopulateColumns();
    }


    private void ClearKeyPropertyControls( )
    {

      _tblCols.Clear();
      _tblHostCols.Clear();

      txtName.Text = String.Empty;

      chkCascadeDelete.Checked = false;
      chkCascadeUpdate.Checked = false;
      chkNotForRep.Checked = false;

      cmbHostTables.SelectedItem = null;
      cmbRefTables.SelectedItem = null;
      cmbRefPk.SelectedItem = null;
    }

    private bool ValidateInput( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Some foreign key properties are not valid!\n";

      if (String.IsNullOrEmpty(cmbHostTables.Text))
      {
        errorMsg += " - Host Table not selected.";
        result = false;
      }

      if (String.IsNullOrEmpty(cmbRefTables.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Reference Table not selected.";
        result = false;
      }

      if (String.IsNullOrEmpty(cmbRefPk.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Reference Table primary key constraint not selected.";
        result = false;
      }

      if (!String.IsNullOrEmpty(cmbRefPk.Text) && _tblCols.Rows.Count == 0)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - No columns specified";
        result = false;
      }
      else
      {
        bool hasInvalidCol = false;
        foreach (DataRow row in _tblCols.Rows)
        {
          if (!Utils.IsDbValueValid(row["hostCol"]) || String.IsNullOrEmpty((string)row["hostCol"]))
          {
            hasInvalidCol = true;
            break;
          }

        }
        if (hasInvalidCol)
        {
          errorMsg += (!result ? "\n" : String.Empty) + " - Some columns not specified!";
          result = false;
        }
      }
      return result;
    }


    #endregion //Key Property Related Operations

    #region Create, Save , Drop, Rename and Change State

    private void InitializeNewKey( )
    {

      ClearKeyPropertyControls();
      _newKey = new ForeignKeyWrapper(_cp);

      txtName.ReadOnly = false;

      _newKey.HostTable = cmbHostTables.SelectedItem as TableWrapper;
      cmbFk.SelectedItem = null;
      cmbFk.Enabled = false;

      if (_hostTable != null)
      {
        SelectedHostTable = _hostTable.Name;
      }
      else if (cmbHostTables.Items.Count > 0)
      {
        cmbHostTables.SelectedIndex = 0;
      }

      if (_afterNewKeyCreated != null)
        _afterNewKeyCreated(this, SelectedKey);

    }

    private void SaveKey( )
    {
      grd.EndEdit();
      string err = String.Empty;
      if (!ValidateInput(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      
      string hostCols = String.Empty;
      string refCols = String.Empty;
      GenerateCols(out hostCols, out refCols);
      SelectedKey.ColsHost = hostCols;
      SelectedKey.ColsRef = refCols;
      bool isNew = _newKey != null;
      try
      {
        _initializing = true;
        if (_newKey != null)
        {
          _newKey.Create();
          _newKey = null;
        }
        else if (SelectedKey != null)
        {
          SelectedKey.DropAndRecreateKey();
        }

        InitializeForeignKeys(_mode, _hostTable,true);
        
        if (isNew && _afterNewKeySaved != null)
          _afterNewKeySaved(this, SelectedKey);
        else if (_afterKeySaved != null)
          _afterKeySaved(this, SelectedKey);

      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
      finally
      {
        _initializing = false;
      }
    }

    private void DropKey( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to drop the foreign key?"))
        return;

      if (SelectedKey == null)
        return;

      int selIndex = -1;
      selIndex = cmbFk.SelectedIndex;

      try
      {
        SelectedKey.Drop();
        if (_afterKeyDropped != null)
          _afterKeyDropped(this, SelectedKey);

        cmbFk.Items.Remove(SelectedKey);

        if (cmbFk.Items.Count >= selIndex)
        {
          cmbFk.SelectedIndex = selIndex;
        }
        else
        {
          ClearKeyPropertyControls();
          InitializeNewKey();
        }
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
    }

    private void RenameKey( )
    {
      if (SelectedKey == null)
        return;

      string newName = SelectedKey.Name;
      if (InputDialog.ShowDialog("Rename Primary Key", "New Name", ref newName) != DialogResult.OK)
        return;

      if (SelectedKey.Name.ToLowerInvariant() == newName.ToLowerInvariant())
        return;

      try
      {
        string hostCols = String.Empty;
        string refCols = String.Empty;
        GenerateCols(out hostCols, out refCols);
        SelectedKey.ColsHost = hostCols;
        SelectedKey.ColsRef = refCols;

        SelectedKey.Rename(newName);
        txtName.Text = newName;

        ForeignKeyWrapper key = SelectedKey;
        
        int index = cmbFk.SelectedIndex;
        cmbFk.Items.Remove(key);
        cmbFk.Items.Insert(index, key);
        cmbFk.SelectedIndex = index;

        if (_afterKeyRenamed != null)
          _afterKeyRenamed(this, SelectedKey);
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
    }

    private void ToggleKeyState( )
    {
      if (SelectedKey.Disabled)
        SelectedKey.Enable();
      else
        SelectedKey.Disable();

      if (_afterKeyStateChanged != null)
        _afterKeyStateChanged(this, SelectedKey);
    }

    #endregion //Create, Save , Drop, Rename and Change State

    #region Actions
    private void InitializeActions( )
    {
      Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_Save_Update);
      ac.Execute += new EventHandler(OnAction_Save_Execute);
      ac.Text = "Save";

      _actions.Actions.Add(ac);
      _actions.SetAction(btnSave, ac);


      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_Drop_Update);
      ac.Execute += new EventHandler(OnAction_Drop_Execute);
      ac.Text = "Drop";

      _actions.Actions.Add(ac);
      _actions.SetAction(btnDrop, ac);

      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_Rename_Update);
      ac.Execute += new EventHandler(OnAction_Rename_Execute);
      ac.Text = "Rename";

      _actions.Actions.Add(ac);
      _actions.SetAction(btnRename, ac);

      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_New_Update);
      ac.Execute += new EventHandler(OnAction_New_Execute);
      ac.Text = "New";

      _actions.Actions.Add(ac);
      _actions.SetAction(btnNew, ac);


      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_Status_Update);
      ac.Execute += new EventHandler(OnAction_Status_Execute);
      ac.Text = "Disable";

      _actions.Actions.Add(ac);
      _actions.SetAction(btnToggleStatus, ac);
    }

    private void OnAction_Save_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

      ac.Enabled = (SelectedKey != null);
    }

    private void OnAction_Save_Execute( object sender, EventArgs args )
    {
      SaveKey();
    }

    private void OnAction_Drop_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

      ac.Enabled = (cmbFk.SelectedItem != null);
    }

    private void OnAction_Drop_Execute( object sender, EventArgs args )
    {
      DropKey();
    }

    private void OnAction_Rename_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

      ac.Enabled = (cmbFk.SelectedItem != null);
    }

    private void OnAction_Rename_Execute( object sender, EventArgs args )
    {
      RenameKey();
    }

    private void OnAction_New_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

    }

    private void OnAction_New_Execute( object sender, EventArgs args )
    {
      InitializeNewKey();
    }

    private void OnAction_Status_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

      
      if (ac.Enabled != (cmbFk.SelectedItem != null))
      {
        ac.Enabled = (cmbFk.SelectedItem != null);
        ac.Text = "Disable";

        if (!ac.Enabled)
        {
          lblDisableNotify.Visible = false;
          return;
        }
      }

      if (ac.Enabled)
      {
        ac.Text = SelectedKey.Disabled ? "Enable" : "Disable";
        lblDisableNotify.Visible = SelectedKey.Disabled;
      }

    }

    private void OnAction_Status_Execute( object sender, EventArgs args )
    {
      ToggleKeyState();
    }


    #endregion //Actions


    private void cmbRefTables_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializing)
        return;

      PopulateReferenceTablePrimaryKeys(String.Empty);
      if (_newKey != null)
      {
        _newKey.RefTable = cmbRefTables.SelectedItem as TableWrapper;
      }
    }

    private void cmbHostTables_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializing)
        return;
      PopulateHostColumnsCombo();
      if (_newKey != null)
      {
        _newKey.HostTable = cmbHostTables.SelectedItem as TableWrapper;
      }
    }

    private void cmbRefPk_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializing )
        return;
      HandleReferencePrimaryKeyChanged();
    }

    private void cmbFk_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      if (!SelectedKey.AllPropsLoaded)
        SelectedKey.LoadAllProperties();

      RenderKeyProperties();
    }

    private void txtName_TextChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      SelectedKey.Name = txtName.Text;
    }

    private void chkNotForRep_CheckedChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      SelectedKey.NotForReplication = chkNotForRep.Checked;
    }

    private void chkCascadeDelete_CheckedChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      SelectedKey.CascadeDelete = chkCascadeDelete.Checked;
    }

    private void chkCascadeUpdate_CheckedChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      SelectedKey.CascadeUpdate = chkCascadeUpdate.Checked;

    }
  }


  public delegate void ForeignKeyEditorEventHandler( object sender, ForeignKeyWrapper key);
}
