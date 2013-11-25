using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmRuleEdit: DockContent
  {
    #region Fields and properties
    private ConnectionParams _cp = null;
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
        if (_ruleEdit != null)
          _ruleEdit.ConnectionParams = _cp;
      }
    }

    private string _caption = String.Empty;
    public string Caption
    {
      get { return _caption; }
      set
      {
        _caption = value;
        TabText = value;
        Text = value;
      }
    }

    private RuleEdit _ruleEdit;

    #endregion //Fields and properties
      
    #region Static show

    public static void ShowCreateRule( ConnectionParams cp )
    {
      frmRuleEdit.ShowRuleEditor(cp, -1, String.Empty, String.Empty, EditMode.New);
    }

    public static void ShowRuleEditor(ConnectionParams cp, int ruleId, string ruleName,string owner, EditMode mode )
    {
      
      frmRuleEdit frm = new frmRuleEdit();
      frm._ruleEdit.Mode = mode;
      string prefix = String.Empty;
      switch (mode)
      {
        case EditMode.New:
          prefix = "New Rule :";
          break;
        case EditMode.Modify:
          prefix = "Modify Rule [" + ruleName + "] :";
          break;
        default:
          prefix = "Rule Editor :";
          break;
      }

      frm.Caption = prefix + cp.Server + " { " + cp.Database + "}";
      frm.ConnectionParams = cp;
      if (mode == EditMode.Modify)
      {
        frm._ruleEdit.LoadRule(ruleId, ruleName, owner);
      }

      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmRuleEdit( )
    {
      InitializeComponent();
      InitializeRuleEdit();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeRuleEdit( )
    {
      // Login List
      _ruleEdit = new RuleEdit();
      panRuleEdit.Controls.Add(_ruleEdit);
      _ruleEdit.Parent = panRuleEdit;
      _ruleEdit.Dock = DockStyle.Fill;
      _ruleEdit.OriginForm = this;
      _ruleEdit.AfterRuleCreated += new EventHandler(_ruleEdit_AfterRuleNameChanged);
      _ruleEdit.AfterRuleRenamed += new EventHandler(_ruleEdit_AfterRuleNameChanged);
    }

   
    void _ruleEdit_AfterRuleNameChanged( object sender, EventArgs e )
    {
      Caption = "Modify Rule [" + _ruleEdit.RuleName + "] :" + _cp.Server + " { " + _cp.Database + "}";
    }


    #endregion //Initialization

    

    private void closeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      this.Close();
    }

    private void closeAllToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(null);
    }

    private void closeAllButThisToolStripMenuItem_Click( object sender, EventArgs e )
    {
      HostServicesSingleton.HostServices.CloseForm(this);
    }


  }
}