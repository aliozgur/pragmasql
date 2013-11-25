using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Crad.Windows.Forms.Actions;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class UdtEdit : UserControl
  {
    #region Fields And Properties
    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set 
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
          InitializeControls();
        }
        else
        {
          _cp = value;
          dtSelector.ResetControl();
          cmbDefaultBinding.Items.Clear();
          cmbRuleBinding.Items.Clear();
        }
      }
    }

    private UdtWrapper _udt = null;
    private UdtWrapper _newUdt = null;

    private ActionList _actions = new ActionList();

    #endregion //Fields And Properties

    #region CTOR
    public UdtEdit( )
    {
      InitializeComponent();
      InitializeActions();
    }
    #endregion //CTOR

    #region Initilization
    private void InitializeControls( )
    {
      dtSelector.LoadDataTypes(_cp, true);
      DbCmd.PopulateDefaultsCombo(cmbDefaultBinding, _cp, true, true);
      DbCmd.PopulateRulesCombo(cmbRuleBinding, _cp, true);
    }

    private void PopulateDependencies( )
    {
      bsDepends.DataSource = null;
      DataTable tbl = _udt.GetDependencies();
      bsDepends.DataSource = tbl;
    }

    #endregion //Initilization

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

      ac.Enabled = (_udt != null || _newUdt != null);
    }

    private void OnAction_Save_Execute( object sender, EventArgs args )
    {
      SaveUdt();
    }

    private void OnAction_Drop_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

      ac.Enabled = (_udt != null);
    }

    private void OnAction_Drop_Execute( object sender, EventArgs args )
    {
      DropUdt();
    }

    private void OnAction_Rename_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

      ac.Enabled = (_udt != null);
    }

    private void OnAction_Rename_Execute( object sender, EventArgs args )
    {
      RenameUdt();
    }

    private void OnAction_New_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      if (ac == null)
        return;

      ac.Enabled = (_newUdt == null);
    }

    private void OnAction_New_Execute( object sender, EventArgs args )
    {
      CreateNew();
    }

    #endregion //Actions



    #region Public Methods
    public void LoadUdt( long id )
    {
      _udt = new UdtWrapper(_cp);
      _udt.ID = id;

      _udt.LoadProperties();
      _udt.LoadDefinition();
      PopulateDependencies();
      RenderUdt();
      SetEdit(false);
    }

    #endregion //Public Methods

    #region Rendering,Apply and Validation
    private void ClearControls( )
    {
      txtName.Text = String.Empty;
      dtSelector.SelectedDataType = String.Empty;
      chkAllowNulls.Checked = false;

      cmbDefaultBinding.SelectedIndex = -1;
      cmbRuleBinding.SelectedIndex = -1;
      bsDepends.DataSource = null;
    }

    private void RenderUdt( )
    {
      ClearControls();

      txtName.Text = _udt.Name;
      chkAllowNulls.Checked = _udt.AllowNulls;
      cmbDefaultBinding.SelectedIndex = cmbDefaultBinding.FindStringExact(_udt.Default);
      cmbRuleBinding.SelectedIndex = cmbRuleBinding.FindStringExact(_udt.Rule);

      dtSelector.DTWidth = 0;
      dtSelector.DTPrecision = 0;
      dtSelector.DTPrecision = 0;

      dtSelector.SelectedDataType = _udt.BaseDataType;
      if (dtSelector.WidthEnabled && !String.IsNullOrEmpty(_udt.Width))
      {
        dtSelector.DTWidth = Convert.ToInt32(_udt.Width);
      }
      if (dtSelector.PrecisionEnabled && !String.IsNullOrEmpty(_udt.Width))
      {
        dtSelector.DTPrecision = Convert.ToInt32(_udt.Width);
      }

      if (!String.IsNullOrEmpty(_udt.Scale))
        dtSelector.DTPrecision = Convert.ToInt32(_udt.Scale);
    }

    private void ApplyInputToUdt(UdtWrapper udt)
    {
      if (udt == null)
        return;

      udt.Name = txtName.Text;
      udt.AllowNulls = chkAllowNulls.Checked;
      
      udt.BaseDataType = dtSelector.SelectedDataType;
      if (dtSelector.WidthEnabled )
      {
        udt.Width = dtSelector.DTWidth.ToString();
      }
      else if (dtSelector.PrecisionEnabled)
      {
        udt.Width = dtSelector.DTPrecision.ToString();
      }
      else
      {
        udt.Width = String.Empty;
      }

            
      if(dtSelector.ScaleEnabled)
      {
        udt.Scale = dtSelector.DTScale.ToString();
      }
      else
      {
        udt.Scale = String.Empty;
      }

      udt.Default = cmbDefaultBinding.SelectedItem as string;
      udt.Rule = cmbRuleBinding.SelectedItem as string;

    }

    private void SetEdit( bool value )
    {
      dtSelector.Enabled = value;
      txtName.Enabled = value;
      dtSelector.Enabled = value;
      chkAllowNulls.Enabled = value;
      if(!value)
        bsDepends.DataSource = null;
    }

    #endregion //Rendering,Apply and  Validation

    #region Udt Operations
    public void CreateNew( )
    {
      ClearControls();
      _udt = null;
      _newUdt = new UdtWrapper(_cp);
      SetEdit(true);
    }

    private void SaveUdt( )
    {
      if (_newUdt != null)
      {
        ApplyInputToUdt(_newUdt);
        _newUdt.Create();
        _udt = _newUdt;
        _newUdt = null;
        SetEdit(false);
      }
      else if (_udt != null)
      {
        _udt.Default = cmbDefaultBinding.SelectedItem as string;
        _udt.Rule = cmbRuleBinding.SelectedItem as string;
        _udt.Alter();
      }
    }

    private void RenameUdt( )
    {
      if (_udt == null)
        return;

      string newName = _udt.Name;
      if (InputDialog.ShowDialog("Rename Udt", "New Name", ref newName) != DialogResult.OK)
        return;

      if (_udt.Name.ToLowerInvariant() == newName.ToLowerInvariant())
        return;

      try
      {
        _udt.Rename(newName);
        txtName.Text = newName;
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
    }

    private void DropUdt( )
    {
      if (_udt == null)
        return;
      try
      {
        if (!MessageService.AskQuestion("Are you sure you want to drop user data type?"))
          return;

        _udt.Drop();
        CreateNew();
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }
    }
    #endregion //Udt Operations
  }
}
