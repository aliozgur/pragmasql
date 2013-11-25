using System;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class frmCheckEdit: DockContent
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
        if (_checkEdit != null)
          _checkEdit.ConnectionParams = _cp;
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

    private CheckEdit _checkEdit;

    #endregion //Fields and properties
      
    #region Static show

    public static void ShowCreateCheck( ConnectionParams cp )
    {
      frmCheckEdit.ShowCheckEditor(cp, EditMode.New, String.Empty, String.Empty, -1, String.Empty, true, false);
    }

    public static void ShowCheckEditor(ConnectionParams cp,  EditMode mode, string owner, string tabelName, int checkId, string checkName, bool checkEnabled , bool noRep)
    {
      
      frmCheckEdit frm = new frmCheckEdit();
      frm._checkEdit.Mode = mode;
      string prefix = String.Empty;
      switch (mode)
      {
        case EditMode.New:
          prefix = "New Check Constraint :";
          break;
        case EditMode.Modify:
          prefix = "Modify Check Constraint [" + checkName + "] :";
          break;
        default:
          prefix = "Check Constraint Editor :";
          break;
      }

      frm.Caption = prefix + cp.Server + " { " + cp.Database + "}";
      frm.ConnectionParams = cp;
      if (mode == EditMode.Modify)
      {
        frm._checkEdit.LoadCheck(checkId, owner, tabelName, checkName, checkEnabled, noRep);
      }

      //frm.ShowDialog();
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmCheckEdit( )
    {
      InitializeComponent();
      InitializeCheckEdit();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeCheckEdit( )
    {
      // Login List
      _checkEdit = new CheckEdit();
      panCheckEdit.Controls.Add(_checkEdit);
      _checkEdit.Parent = panCheckEdit;
      _checkEdit.Dock = DockStyle.Fill;
      _checkEdit.OriginForm = this;
      _checkEdit.AfterCheckCreated += new EventHandler(_checkEdit_AfterCheckNameChanged);
      _checkEdit.AfterCheckRenamed += new EventHandler(_checkEdit_AfterCheckNameChanged);
    }

   
    void _checkEdit_AfterCheckNameChanged( object sender, EventArgs e )
    {
      Caption = "Modify Check Constraint  [" + _checkEdit.CheckName + "] :" + _cp.Server + " { " + _cp.Database + "}";
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