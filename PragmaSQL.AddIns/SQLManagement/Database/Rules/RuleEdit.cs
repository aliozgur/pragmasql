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
  public partial class RuleEdit : UserControl
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
          PopulateOwnersCombo();
        }
        else
        {
          _cp = null;
          bsDepends.DataSource = null;
          _tblDepends = null;
          cmbOwner.Items.Clear();
        }
      }
    }

    private int _ruleId;
    public int RuleId
    {
      get { return _ruleId; }
      private set { _ruleId = value; }
    }

    private string _ruleName;
    public string RuleName
    {
      get { return _ruleName; }
      private set { _ruleName = value; }
    }

    private string _owner;
    public string Owner
    {
      get { return _owner; }
      private set { _owner = value; }
    }

    private DataTable _tblDepends = null;
    private string _originalDefinition = String.Empty;

    private EventHandler _afterRuleCreated;
    public event EventHandler AfterRuleCreated
    {
      add { _afterRuleCreated += value; }
      remove { _afterRuleCreated -= value; }
    }

    private EventHandler _afterRuleRenamed;
    public event EventHandler AfterRuleRenamed
    {
      add { _afterRuleRenamed += value; }
      remove { _afterRuleRenamed -= value; }
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

    public RuleEdit( )
    {
      InitializeComponent();
      InitializeTextEditor();
      SetModified(false);
    }
    private void SetModified( bool value )
    {
      lblModified.Visible = value;
      pbModified.Visible = value;
    }

    private void ApplyMode( )
    {
      switch (_mode)
      {
        case EditMode.New:
          btnUpdate.Visible = false;
          btnCreate.Visible = true;
          btnRename.Enabled = false;
          btnDrop.Enabled = false;
          
          gbDepends.Visible = false;
          chkBackup.Visible = false;
          cmbOwner.Enabled = true;
          txtName.ReadOnly = false;
          break;
        case EditMode.Modify:
          btnUpdate.Visible = true;
          btnCreate.Visible = false;
          btnRename.Enabled = true;
          btnDrop.Enabled = true;
          
          gbDepends.Visible = true;
          chkBackup.Visible = true;
          cmbOwner.Enabled = false;
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


    private void PopulateOwnersCombo( )
    {
      DbCmd.PopulateOwnerCombo(cmbOwner, _cp, _cp.Database);

      if (cmbOwner.Items.Count > 0)
        cmbOwner.SelectedIndex = 0;
    }

    public void LoadRule(int ruleId, string ruleName, string owner)
    {
      RuleId = ruleId;
      RuleName = ruleName;
      Owner = owner;
      cmbOwner.Text = owner;
      txtName.Text = ruleName;
      PopulateRuleDefinition();
      PopulateRuleDepends();
      SetModified(false);
    }

    private void PopulateRuleDefinition( )
    {
      string ruleDef = DbCmd.GetRuleDefinition(_cp, _owner, _ruleName);
      Content = ruleDef;
      _originalDefinition = ruleDef;
    }

    private void PopulateRuleDepends( )
    {
      bsDepends.DataSource = null;
      _tblDepends = null;
      _tblDepends = DbCmd.GetRuleDepends(_cp, _ruleId);
      bsDepends.DataSource = _tblDepends;
    }

    private bool ValidateRuleDefinition( ref string errorMsg )
    {
      bool result = true;
      errorMsg = "Some rule properties are not valid!\n";
      if (String.IsNullOrEmpty(cmbOwner.Text))
      {
        errorMsg += " - Owner name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(txtName.Text))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Rule name is empty.";
        result = false;
      }

      if (String.IsNullOrEmpty(Content))
      {
        errorMsg += (!result ? "\n" : String.Empty) + " - Definition is empty.";
        result = false;
      }

      return result;
    }


    private void CreateRule( )
    {
      string err = String.Empty;
      if (!ValidateRuleDefinition(ref err))
      {
        MessageService.ShowError(err);
        return;
      }

      DbCmd.CreateRule(_cp,cmbOwner.Text, txtName.Text, Content);
      Mode = EditMode.Modify;
      RuleName = txtName.Text;
      Owner = cmbOwner.Text;
      RuleId = DbCmd.GetRuleId(_cp, RuleName);
      _originalDefinition = Content;
      SetModified(false);
      if (_afterRuleCreated != null)
        _afterRuleCreated(this, EventArgs.Empty);
    }

    private void UpdateRuleDefinition( )
    {
      try
      {
        DbCmd.AlterRuleDefinition(_cp, _ruleId, _ruleName, Content, _owner, chkBackup.Checked);
        SetModified(false);
        if (_afterDefinitionUpdated != null)
          _afterDefinitionUpdated(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        MessageService.ShowError("Can not update rule definition!\nError Message:" + ex.Message);
      }
    }

    private void RenameRule( )
    {
      string newName = RuleName;
      if (InputDialog.ShowDialog("Rename Rule", "New Name", ref newName) != DialogResult.OK)
        return;

      if (RuleName.ToLowerInvariant() == newName.ToLowerInvariant())
        return;

      DbCmd.RenameRule(_cp, _owner, _ruleName, newName);
      RuleName = newName;
      txtName.Text = RuleName;
      
      if (_afterRuleRenamed!= null)
        _afterRuleRenamed(this, EventArgs.Empty);
    }

    private void DropRule( )
    {
      if (!MessageService.AskQuestion("Are you sure you want to drop this rule?"))
        return;

      DbCmd.DropRule(_cp, RuleId, RuleName, Owner);
      if (OriginForm != null)
        OriginForm.Close();
    }

    private void btnUpdate_Click( object sender, EventArgs e )
    {
      UpdateRuleDefinition();
    }

    private void btnCreate_Click( object sender, EventArgs e )
    {
      CreateRule();
    }

    private void btnRename_Click( object sender, EventArgs e )
    {
      RenameRule();
    }

    private void btnDrop_Click( object sender, EventArgs e )
    {
      DropRule();
    }
    
  }
}
