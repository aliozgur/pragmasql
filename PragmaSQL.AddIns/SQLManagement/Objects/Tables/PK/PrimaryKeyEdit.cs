using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using Crad.Windows.Forms.Actions;

namespace SQLManagement
{
  public partial class PrimaryKeyEdit : UserControl
  {
    #region Fields And Properties
    private ActionList _actions = new ActionList();

    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
          try
          {
            _initializing = true;
            DbCmd.PopulateFileGroupsCombo(cmbFileGroup, _cp, false);
            if (cmbFileGroup.Items.Count > 0)
              cmbFileGroup.SelectedIndex = 0;
          }
          finally
          {
            _initializing = false;
          }
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

    private bool _initializing = false;

    #endregion //Fields And Properties

    public PrimaryKeyEdit( )
    {
      InitializeComponent();
      InitializeActions();
    }

    private void ApplyMode( )
    {
      
    }

    private PrimaryKeyWrapper _newKey = null;
    public PrimaryKeyWrapper SelectedKey
    {
      get
      {
        if (_newKey == null)
        {
          return cmbPk.SelectedItem as PrimaryKeyWrapper;
        }
        else
        {
          return _newKey;
        }
      }
    }

    public string SelectedTable
    {
      set
      {
        cmbTables.SelectedIndex = cmbTables.FindStringExact(value);
      }
    }

    private TableWrapper _initialTable;

    #region Initialization

    public void InitializePrimaryKeys( TableKeyEditorMode mode, bool loadProps, TableWrapper initialTable )
    {
      try
      {
        _initializing = true;
        Mode = mode;
        DbCmd.PopulatePrimaryKeysCombo(cmbPk, _cp, loadProps, (initialTable != null ? initialTable.ID : -1));
        _initialTable = initialTable;

        DbCmd.PopulateUserDefinedTablesCombo(cmbTables, _cp, (initialTable != null ? initialTable.ID : -1));
      }
      finally
      {
        _initializing = false;
      }

      if (cmbPk.Items.Count > 0)
      {
        cmbPk.SelectedIndex = 0;
      }
      else
      {
        CreateNewKey();
      }
    }

    public void RefreshPrimaryKeys( bool loadProps, TableWrapper initialTable )
    {
      string keyName = String.Empty;
      try
      {
        _initializing = true;
        keyName = SelectedKey != null ? SelectedKey.ToString() : String.Empty;
        DbCmd.PopulatePrimaryKeysCombo(cmbPk, _cp, loadProps, (initialTable != null ? initialTable.ID : -1));
        _initialTable = initialTable;

        DbCmd.PopulateUserDefinedTablesCombo(cmbTables, _cp, (initialTable != null ? initialTable.ID : -1));
        
      }
      finally
      {
        _initializing = false;
      }

      if (cmbPk.Items.Count > 0)
      {
        cmbPk.SelectedIndex = cmbPk.FindStringExact(keyName);
        if (cmbPk.SelectedIndex < 0)
          cmbPk.SelectedIndex = 0;
      }
      else
      {
        CreateNewKey();
      }
    }

    #endregion //Initialization

    #region Key Column Related Operations

    private void InitializeTableColumns( )
    {
      lbTableCols.Items.Clear();
      if (SelectedKey == null)
        return;

      foreach(ColumnWrapper col in SelectedKey.Table.Columns)
      {
        if (lbPkCols.FindStringExact(col.Name) == -1)
        {
          lbTableCols.Items.Add(col.Name);
        }
      }
    }

    private void InitializeKeyColumns( )
    {
      lbPkCols.Items.Clear();
      
      if (SelectedKey == null)
        return;

      foreach (string col in SelectedKey.Columns)
      {
        lbPkCols.Items.Add(col);
      }
    }

    private void AddSelectedColumnToKey( )
    {
      if (SelectedKey == null)
        return;

      if (lbTableCols.SelectedItems.Count == 0)
        return;

      IList<string> delList = new List<string>();
      foreach (string col in lbTableCols.SelectedItems)
      {
        lbPkCols.Items.Add(col);
        delList.Add(col);
        SelectedKey.Columns.Add(col);
      }

      foreach (string col in delList)
      {
        lbTableCols.Items.Remove(col);
      }
    }

    private void RemoveSelectedColumnFromKey( )
    {
      if (SelectedKey == null)
        return;

      if (lbPkCols.SelectedItems.Count == 0)
        return;

      IList<string> delList = new List<string>();
      foreach (string col in lbPkCols.SelectedItems)
      {
        lbTableCols.Items.Add(col);
        delList.Add(col);
        SelectedKey.Columns.Remove(col);
      }

      foreach (string col in delList)
      {
        lbPkCols.Items.Remove(col);
      }
    }

    #endregion //Key Column Related Operations

    #region Key Properties Related Operations
    
    private bool ValidateInput( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Some index properties are not valid!\n";
      if (String.IsNullOrEmpty(cmbTables.Text))
      {
        errorMsg += " - Table not selected.";
        result = false;
      }

      if (String.IsNullOrEmpty(txtName.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(cmbFileGroup.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - File group not selected.";
        result = false;
      }

      int val = 0;
      if (String.IsNullOrEmpty(txtFillFactor.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Fill factor is empty.";
        result = false;
      }
      else if (!Int32.TryParse(txtFillFactor.Text, out val) || val < 0)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Fill factor is not a valid number.";
        result = false;
      }

      if (lbPkCols.Items.Count == 0)
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - No columns specified.";
        result = false;
      }

      return result;
    }

    private void RenderKeyProperties( )
    {
      try
      {
        _initializing = true;
        ClearKeyPropertyControls();

        if (SelectedKey == null)
          return;

        PrimaryKeyWrapper key = SelectedKey;

        SelectedTable = key.Table.Name;
        txtName.Text = key.Name;
        chkClustered.Checked = key.Clustered;
        txtFillFactor.Text = key.FillFactor.ToString();
        cmbFileGroup.SelectedIndex = cmbFileGroup.FindStringExact(key.Filegroup);
        InitializeKeyColumns();
        InitializeTableColumns();
      }
      finally
      {
        _initializing = false;
      }
    
    }

    private void ClearKeyPropertyControls( )
    {
      cmbFileGroup.SelectedIndex = -1;
      chkClustered.Checked = false;
      txtFillFactor.Text = "0";
      cmbTables.SelectedIndex = -1;
      txtName.Text = String.Empty;

      lbTableCols.Items.Clear();
      lbPkCols.Items.Clear();
    }

    private void SetKeyDefinitionControlVisibility( bool value )
    {
      cmbTables.Enabled = value;
      txtName.Enabled = value;
    }

    #endregion //Key Properties Related Operations

    #region Create, Save , Drop and Rename
    
    public void CreateNewKey( )
    {
    
      ClearKeyPropertyControls();
      _newKey = new PrimaryKeyWrapper(_cp);
      SetKeyDefinitionControlVisibility(true);

      txtName.Text = "PK_" + _initialTable.Name;
      if (_initialTable != null)
      {
        SelectedTable = _initialTable.Name;
      }
      else if ( cmbTables.Items.Count > 0)
      {
      cmbTables.SelectedIndex = 0;
      }
    }

    public void SaveKey( )
    {
      string err = String.Empty;
      if (!ValidateInput(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      try
      {
        if (_newKey != null)
        {
          _newKey.Create();

          _initializing = true;
          cmbPk.Items.Add(_newKey);
          cmbPk.SelectedItem = _newKey;
          _newKey = null;
          _initializing = false;

        }
        else if (SelectedKey != null)
        {
          SelectedKey.DropAndRecreate();
        }
      }
      catch (Exception ex)
      {
        if (ex.GetType() != typeof(CancelledByUserException))
        {
          Utils.ShowException(ex);
        }
      }
    }

    private void DropKey( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to drop the primary key?"))
        return;
      
      if (SelectedKey == null)
        return;

      int selIndex = -1;
      selIndex = cmbPk.SelectedIndex;

      try
      {
        SelectedKey.DropWithDepends();
        cmbPk.Items.Remove(SelectedKey);

        if (_mode == TableKeyEditorMode.SingleTable)
        {
          CreateNewKey();
        }
        else
        {
          ClearKeyPropertyControls();
          SetKeyDefinitionControlVisibility(false);
          if (cmbPk.Items.Count >= selIndex)
          {
            cmbPk.SelectedIndex = selIndex;
          }
        }
      }
      catch (Exception ex)
      {
        if(ex.GetType() != typeof(CancelledByUserException))
        {
          Utils.ShowException(ex);
        }
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
        SelectedKey.Rename(newName);
        txtName.Text = newName;

        PrimaryKeyWrapper key = SelectedKey;
        int index = cmbPk.SelectedIndex;
        cmbPk.Items.Remove(key);
        cmbPk.Items.Insert(index, key);
        cmbPk.SelectedIndex = index;
      }
      catch (Exception ex)
      {
        if (ex.GetType() != typeof(CancelledByUserException))
        {
          Utils.ShowException(ex);
        }
      }
    }

    #endregion //Create or Save

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

      ac.Enabled = (cmbPk.SelectedItem != null);
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

      ac.Enabled = (cmbPk.SelectedItem != null);
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

      ac.Enabled = ( ( _mode == TableKeyEditorMode.SingleTable && cmbPk.Items.Count == 0 ) || (_mode == TableKeyEditorMode.All) );
    }

    private void OnAction_New_Execute( object sender, EventArgs args )
    {
      CreateNewKey();
    }

    #endregion //Actions

    private void btnAddCols_Click( object sender, EventArgs e )
    {
      AddSelectedColumnToKey();
    }

    private void btnRemoveCols_Click( object sender, EventArgs e )
    {
      RemoveSelectedColumnFromKey();
    }

    private void cmbPk_SelectedIndexChanged( object sender, EventArgs e )
    {
      SetKeyDefinitionControlVisibility(false);
      if (_initializing)
        return;

      _newKey = null;
      RenderKeyProperties();
    }

    private void cmbTables_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      TableWrapper selTable = cmbTables.SelectedItem as TableWrapper;
      if (selTable == null)
      {
        SelectedKey.Table = selTable;
        return;
      }

      TableWrapper tbl = new TableWrapper();
      tbl.ConnectionParams = _cp;
      tbl.ID = selTable.ID;
      tbl.LoadProperties();
      tbl.LoadColumns();
      SelectedKey.Table = tbl;

      
      lbPkCols.Items.Clear();
      SelectedKey.Columns.Clear();
      InitializeTableColumns();
    }

    private void txtName_TextChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      SelectedKey.Name = txtName.Text;
    }

    private void chkClustered_CheckStateChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      SelectedKey.Clustered = chkClustered.Checked;
    }

    private void txtFillFactor_TextChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;
      SelectedKey.FillFactor = Convert.ToByte(txtFillFactor.Text);
    }


    private void cmbFileGroup_SelectedIndexChanged( object sender, EventArgs e )
    {
      if (_initializing || SelectedKey == null)
        return;

      SelectedKey.Filegroup = cmbFileGroup.SelectedItem as string;
    }
  }
}
