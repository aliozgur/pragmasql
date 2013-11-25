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
  public partial class frmIndexEdit: DockContent
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
        if (_indexEdit != null)
          _indexEdit.ConnectionParams = _cp;
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

    private IndexEdit _indexEdit;

    #endregion //Fields and properties
      
    #region Static show

    public static void ShowCreateIndex( ConnectionParams cp, string owner,long objectId, string objectName  )
    {
      frmIndexEdit.ShowIndexEditor(cp, EditMode.New, owner, - 1, String.Empty,objectId, objectName);
    }

    public static void ShowIndexEditor( ConnectionParams cp, EditMode mode,string owner, long indexId, string indexName, long objectId, string objectName )
    {

      frmIndexEdit frm = new frmIndexEdit();
      frm._indexEdit.Mode = mode;
      string prefix = String.Empty;
      switch (mode)
      {
        case EditMode.New:
          prefix = "New Index ";
          break;
        case EditMode.Modify:
          prefix = "Modify Index [" + indexName + "]";
          break;
        default:
          prefix = "Index Editor ";
          break;
      }

      frm.Caption = prefix + " {" + cp.InfoDbServer + "}";
      frm.ConnectionParams = cp;
      if (mode == EditMode.Modify)
      {
        frm._indexEdit.PrepareExistingIndex(indexId,objectId,objectName);
      }
      else if (mode == EditMode.New)
      {
        frm._indexEdit.PrepareNewIndex(owner, objectId, objectName);      
      }

      //frm.ShowDialog();
      HostServicesSingleton.HostServices.ShowForm(frm, AddInDockState.Document);
    }

    #endregion //Static show

    #region CTOR

    public frmIndexEdit( )
    {
      InitializeComponent();
      InitializeIndexEdit();
    }

    #endregion //CTOR

    #region Initialization

    private void InitializeIndexEdit( )
    {
      // Login List
      _indexEdit = new IndexEdit();
      panIndexEdit.Controls.Add(_indexEdit);
      _indexEdit.Parent = panIndexEdit;
      _indexEdit.Dock = DockStyle.Fill;
      _indexEdit.OriginForm = this;
      _indexEdit.AfterIndexCreated += new EventHandler(_indexEdit_AfterIndexNameChanged);
      _indexEdit.AfterIndexRenamed += new EventHandler(_indexEdit_AfterIndexNameChanged);
    }

   
    void _indexEdit_AfterIndexNameChanged( object sender, EventArgs e )
    {
      Caption = "Modify Index [" + _indexEdit.Index.Name + "] {" + _cp.InfoDbServer + "}";
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