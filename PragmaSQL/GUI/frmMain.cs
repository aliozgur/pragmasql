using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI;

using PragmaSQL.Database;

namespace PragmaSQL.GUI
{
  public partial class frmMain : Form
  {
    frmSearchAndReplace frmSearchAndReplace = null;

    private frmObjectExplorer _objectExplorer = new frmObjectExplorer();
    public frmObjectExplorer ObjectExplorer
    {
      get { return _objectExplorer; }
    }

    private frmSharedRepository _sharedRepository = new frmSharedRepository();
    private frmSharedScriptTemplates _sharedScriptTemplates = new frmSharedScriptTemplates();
    private frmFileExplorer _fileExplorer = new frmFileExplorer();
    private frmLocalScriptTemplates _localScriptTemplates = new frmLocalScriptTemplates();

    private DockPanel _dockPanel = new DockPanel();
    public DockPanel DockPanel
    {
      get { return _dockPanel; }
    }

    public frmMain()
    {
      InitializeComponent();
    }

    #region Docking 
    
    private void InitializeDockPanel()
    {
      _dockPanel.ActiveAutoHideContent = null;
      _dockPanel.Dock = DockStyle.Fill;
      _dockPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
      _dockPanel.Location = new System.Drawing.Point(0, 49);
      _dockPanel.Name = "_dockPanel";
      _dockPanel.Size = new System.Drawing.Size(579, 338);
      _dockPanel.TabIndex = 0;

      this.Controls.Add(_dockPanel);
      _dockPanel.BringToFront();
      _dockPanel.ShowDocumentIcon = true;
      _dockPanel.DocumentStyle = DocumentStyles.DockingMdi;
    }


    private void CloseAllDocuments()
    {
      if( frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel,null) == DialogResult.Cancel )
      {
        return;
      }

      if (_dockPanel.DocumentStyle == DocumentStyles.SystemMdi)
      {
        foreach (Form form in MdiChildren)
          form.Close();
      }
      else
      {
        foreach (IDockContent content in _dockPanel.Documents)
          content.DockHandler.Close();
      }
    }

    private IDockContent FindDocument(string text)
    {
      if (_dockPanel.DocumentStyle == DocumentStyles.SystemMdi)
      {
        foreach (Form form in MdiChildren)
          if (form.Text == text)
            return form as IDockContent;

        return null;
      }
      else
      {
        IDockContent[] documents = _dockPanel.Documents;

        foreach (IDockContent content in documents)
          if (content.DockHandler.TabText == text)
            return content;

        return null;
      }
    }
    #endregion

    #region Methods

    private const int WM_CLOSE = 16;
    private bool _cancelClose = false;
    protected override void WndProc( ref Message m )
    {
      if(m.Msg == WM_CLOSE)
      {
        if( frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel,null) == DialogResult.Cancel )
        {
          _cancelClose = true;
        }
        else
        {
          _cancelClose = false;
          base.WndProc(ref m);
        }
      }
      else
      {
        base.WndProc(ref m);
      }
    }

    public NodeData GetCurrentSelectedNodeDataFromObjectExplorer()
    {
      if(_objectExplorer == null)
      {
        return null;
      }

      return _objectExplorer.GetCurrentSelectedNodeData();
    }

    public void CloseDocuments(Form skipThis)
    {
      if (DockPanel != null)
      {
        if (DockPanel.DocumentStyle == DocumentStyles.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form == skipThis)
              continue;
            form.Close();
          }
        }
        else
        {
          foreach (IDockContent content in DockPanel.Documents)
          {
            if (content == skipThis)
              continue;
            content.DockHandler.Close();
          }
        }
      }

    }

    #endregion

    private void menuItemLockLayout_Click(object sender, EventArgs e)
    {
      _dockPanel.AllowRedocking = !_dockPanel.AllowRedocking;
    }

    private void objectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _objectExplorer.Show(_dockPanel);
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      if(Program.splashScreen != null)
      {
        Program.splashScreen.Close();
      }
      
      InitializeDockPanel();
      _objectExplorer.Show(_dockPanel,DockState.DockLeft);
      _sharedRepository.Show(_dockPanel, DockState.DockLeft);
      _sharedScriptTemplates.Show(_dockPanel, DockState.DockLeft);
      _fileExplorer.Show(_dockPanel, DockState.DockLeft);
      _localScriptTemplates.Show(_dockPanel, DockState.DockLeft);

      _objectExplorer.Show();
    }

    private void fileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _fileExplorer.Show(_dockPanel);
    }

    private void sharedRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _sharedRepository.Show(_dockPanel);
    }

    private void sharedScriptTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _sharedScriptTemplates.Show(_dockPanel);

    }

    private void localScriptTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _localScriptTemplates.Show(_dockPanel);
    }

    private void dataSourcesRespoitoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConnectionRepository frm = new frmConnectionRepository();
      frm.ShowDialog();
    }

    private void aboutToolStripMenuItem_Click( object sender, EventArgs e )
    {
      frmAbout.ShowAbout();
    }

    private void newScriptToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if(_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.CreateScriptEditor();
    }

    private void newScriptFromFileToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if(_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.OpenFileInNewScriptEditor();

    }

    private void frmMain_FormClosing( object sender, FormClosingEventArgs e )
    {
      if(_cancelClose)
      {
        e.Cancel = true;
        _cancelClose = false;
      }
    }

    private void closeAllDocumentsToolStripMenuItem_Click( object sender, EventArgs e )
    {
      CloseAllDocuments();
    }

    private void searchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.CreateDatabaseSearchForm();
    }
  }
}