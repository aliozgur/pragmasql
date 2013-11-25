using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.Core;

using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class CheckEdit : UserControl
  {
    private EditMode _mode = EditMode.New;
    public EditMode Mode
    {
      get { return _mode; }
      set { _mode = value; ApplyMode(); }
    }

    private ConnectionParams _cp = null;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
          PopulateTablesCombo();
        }
        else
        {
          _cp = null;
          cmbTable.Items.Clear();
        }
      }
    }

    private int _checkId;
    public int CheckId
    {
      get { return _checkId; }
      private set { _checkId = value; }
    }

    private string _checkName;
    public string CheckName
    {
      get { return _checkName; }
      private set { _checkName = value; }
    }

    private string _owner;
    public string Owner
    {
      get { return _owner; }
      private set { _owner = value; }
    }

    private string _tableName;
    public string TableName
    {
      get { return _tableName; }
      set { _tableName = value; }
    }

    private bool _checkEnabled = true;
    public bool CheckEnabled
    {
      get { return _checkEnabled; }
      private set
      {
        _checkEnabled = value;
        if (value)
        {
          btnToggleState.Text = "Disable";
        }
        else
        {
          btnToggleState.Text = "Enable";
        }
      }
    }

    private bool _originalNoRep = false;
    private string _originalDefinition = String.Empty;

    private EventHandler _afterCheckCreated;
    public event EventHandler AfterCheckCreated
    {
      add { _afterCheckCreated += value; }
      remove { _afterCheckCreated -= value; }
    }

    private EventHandler _afterCheckRenamed;
    public event EventHandler AfterCheckRenamed
    {
      add { _afterCheckRenamed += value; }
      remove { _afterCheckRenamed -= value; }
    }

    private EventHandler _afterDefinitionUpdated;
    public event EventHandler AfterDefinitionUpdated
    {
      add { _afterDefinitionUpdated += value; }
      remove { _afterDefinitionUpdated -= value; }
    }

    public Form OriginForm = null;

    #region Text Editor related properties
    protected TextEditorControl _textEditor = null;
    public TextArea ActiveTextArea
    {
      get
      {
        if (_textEditor == null)
        {
          return null;
        }
        else
        {
          return _textEditor.ActiveTextAreaControl.TextArea;
        }
      }
    }

    public IDocument ActiveDocument
    {
      get
      {
        return ActiveTextArea.Document;
      }
    }

    public string Content
    {
      get
      {
        return ActiveDocument.TextContent;
      }
      set
      {
        try
        {
          ActiveTextArea.BeginUpdate();
          ActiveDocument.TextContent = value;
        }
        finally
        {
          ActiveTextArea.EndUpdate();
          ActiveTextArea.Invalidate();
        }
      }
    }

    public ITextEditorProperties ActiveTextEditorProps
    {
      get
      {
        if (ActiveTextArea == null || ActiveTextArea.MotherTextAreaControl == null)
        {
          return null;
        }
        else
        {
          return ActiveTextArea.TextEditorProperties;
        }
      }
    }

    #endregion //Text Editor related properties

    public CheckEdit( )
    {
      InitializeComponent();
      InitializeTextEditor();
      SetModified(false);
    }

    private void ApplyMode( )
    {
      switch (_mode)
      {
        case EditMode.New:
          btnUpdate.Visible = false;
          btnCreate.Visible = true;
          btnRename.Enabled = false;
          btnToggleState.Enabled = false;
          btnDrop.Enabled = false;

          chkBackup.Visible = false;
          cmbTable.Enabled = true;
          txtName.ReadOnly = false;
          break;
        case EditMode.Modify:
          btnUpdate.Visible = true;
          btnCreate.Visible = false;
          btnRename.Enabled = true;
          btnToggleState.Enabled = true;
          btnDrop.Enabled = true;

          chkBackup.Visible = true;
          cmbTable.Enabled = false;
          txtName.ReadOnly = true;
          break;
        default:
          break;
      }
    }

    private void InitializeTextEditor( )
    {
      if (_textEditor != null)
      {
        return;
      }

      _textEditor = new TextEditorControl();
      panEditor.Controls.Add(_textEditor);
      _textEditor.Dock = DockStyle.Fill;
      _textEditor.BringToFront();
      _textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
      _textEditor.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);

      ActiveTextEditorProps.AllowCaretBeyondEOL = false;
      ActiveTextEditorProps.ConvertTabsToSpaces = false;
      ActiveTextEditorProps.EnableFolding = false;
      ActiveTextEditorProps.IndentStyle = IndentStyle.Smart;
      ActiveTextEditorProps.ShowEOLMarker = false;
      ActiveTextEditorProps.ShowInvalidLines = false;
      ActiveTextEditorProps.ShowLineNumbers = true;
      ActiveTextEditorProps.ShowSpaces = false;
      ActiveTextEditorProps.ShowTabs = false;
      ActiveTextEditorProps.ShowVerticalRuler = true;
      ActiveTextEditorProps.TabIndent = 2;
      ActiveTextEditorProps.VerticalRulerRow = 120;

      _textEditor.Focus();
    }
    
    private void SetModified( bool value )
    {
      lblModified.Visible = value;
      pbModified.Visible = value;
    }

    private void Document_DocumentChanged( object sender, DocumentEventArgs e )
    {
      if (e.Document.UndoStack.CanUndo)
      {
        SetModified(true);
      }
      else
      {
        SetModified(false);
      }
    }

    private void PopulateTablesCombo( )
    {
      DbCmd.PopulateTablesComboForCheckConstraint(cmbTable, _cp);

      if (cmbTable.Items.Count > 0)
        cmbTable.SelectedIndex = 0;
    }

    public void LoadCheck( int checkId, string owner, string tableName, string checkName, bool checkEnabled, bool noRep )
    {
      CheckId = checkId;
      CheckName = checkName;
      Owner = owner;
      TableName = tableName;
      CheckEnabled = checkEnabled;
      _originalNoRep = noRep;
      cmbTable.Text = tableName;
      txtName.Text = checkName;
      PopulateCheckDefinition();
      SetModified(false);
    }

    private void PopulateCheckDefinition( )
    {

      string ruleDef = DbCmd.GetCheckDefinition(_cp, _owner, _checkName);
      Content = ruleDef;
      _originalDefinition = ruleDef;

    }


    private bool ValidateRuleDefinition( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Some check constraint properties are not valid!\n";
      if (String.IsNullOrEmpty(cmbTable.Text))
      {
        errorMsg += " - Table name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(txtName.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Check constraint name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(Content))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Definition is empty.";
        result = false;
      }

      return result;
    }


    private void CreateCheck( bool fireEvent )
    {

      string err = String.Empty;
      if (!ValidateRuleDefinition(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      CheckTableSpec tblSpec = cmbTable.SelectedItem as CheckTableSpec;

      DbCmd.CreateCheck(_cp, tblSpec.Owner, tblSpec.Name , txtName.Text, Content, chkNotForRep.Checked);
      Mode = EditMode.Modify;
      CheckName = tblSpec.Name;
      Owner = tblSpec.Owner;
      TableName = cmbTable.Text;
      CheckId = DbCmd.GetCheckConstraintId(_cp, CheckName);
      CheckEnabled = true;
      _originalDefinition = Content;
      _originalNoRep = chkNotForRep.Checked;
      SetModified(false);
      if (fireEvent && _afterCheckCreated != null)
        _afterCheckCreated(this, EventArgs.Empty);
    }

    private void UpdateCheckDefinition( )
    {
      try
      {
        //Create temporary check
        string tmpCheck = "tempCheck_" + Guid.NewGuid().ToString().Replace('-','_');
        DbCmd.CreateCheck(_cp, Owner, TableName, tmpCheck , Content , chkNotForRep.Checked);
        
        //Create backup of the original
        if (chkBackup.Checked)
          DbCmd.CreateCheck(_cp, Owner, TableName, CheckName + "_bak_" + Utils.GetFormattedNow(), _originalDefinition, _originalNoRep);
        
        //Drop original
        DbCmd.DropCheck(_cp, Owner, TableName, CheckName);
        
        //Rename temp to original
        DbCmd.RenameCheck(_cp, Owner, tmpCheck, CheckName);
        _originalDefinition = Content;
        //CreateCheck(false);

        SetModified(false);
        if (_afterDefinitionUpdated != null)
          _afterDefinitionUpdated(this, EventArgs.Empty);
      }
      catch(Exception ex)
      {
        MessageService.ShowError("Can not update check constraint definitions!\nError Message:" + ex.Message);
      }
      
    }

    private void RenameCheck( )
    {
      string newName = CheckName;
      if (InputDialog.ShowDialog("Rename Check Constraint", "New Name", ref newName) != DialogResult.OK)
        return;

      if (CheckName.ToLowerInvariant() == newName.ToLowerInvariant())
        return;
      
      DbCmd.RenameCheck(_cp, _owner, _checkName, newName);


      CheckName = newName;
      txtName.Text = CheckName;

      if (_afterCheckRenamed != null)
        _afterCheckRenamed(this, EventArgs.Empty);
    }

    private void ToggleCheckState( )
    {
      DbCmd.ToggleCheckState(_cp, !_checkEnabled, Owner, TableName, CheckName);
      CheckEnabled = !_checkEnabled;
    }
    
    private void DropCheck( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to drop this check constraint?"))
        return;

      DbCmd.DropCheck(_cp, Owner, TableName, CheckName);
      if (OriginForm != null)
        OriginForm.Close();
    }

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      UpdateCheckDefinition();
    }

    private void btnCreate_Click( object sender, EventArgs e )
    {
      CreateCheck(true);
    }

    private void btnRename_Click( object sender, EventArgs e )
    {
      RenameCheck();
    }

    private void btnToggleState_Click( object sender, EventArgs e )
    {
      ToggleCheckState();
    }

    private void btnDrop_Click( object sender, EventArgs e )
    {
      DropCheck();
    }
  }

  
}
