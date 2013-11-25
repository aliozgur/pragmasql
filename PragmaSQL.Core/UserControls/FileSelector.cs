using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public partial class FileSelector : UserControl
  {
    public FileSelector( )
    {
      InitializeComponent();
      _openDialog = new OpenFileDialog();
      _browseFolderDialog = new FolderBrowserDialog();
    }


    private OpenFileDialog _openDialog = null;
    public OpenFileDialog OpenDialog
    {
      get { return _openDialog; }
      set { _openDialog = value; }
    }

    private FolderBrowserDialog _browseFolderDialog = null;
    public FolderBrowserDialog BrowseFolderDialog
    {
      get { return _browseFolderDialog; }
      set { _browseFolderDialog = value; }
    }

    private DialogType _dialogType = DialogType.File;
    public DialogType DialogType
    {
      get { return _dialogType; }
      set { _dialogType = value; }
    }

    public string Path
    {
      get { return edtPath.Text; }
      set { edtPath.Text = value; }
    }

		public TextBox TextBox
		{
			get { return this.edtPath; }
		}

    public string LabelText
    {
      get { return lblPath.Text; }
      set { lblPath.Text = value; }
    }

    public bool ReadOnly
    {
      get { return edtPath.ReadOnly; }
      set { edtPath.ReadOnly = value; }
    }

    private EventHandler _afterFileOpened;
    public event EventHandler AfterFileOpened
    {
      add { _afterFileOpened += value; }
      remove { _afterFileOpened -= value; }
    }

    public string Filter
    {
      get
      {
        return _openDialog != null ? _openDialog.Filter : null;
      }
      set
      {
        if( _openDialog != null)
          _openDialog.Filter = value;
      }
    }
    
    public DialogResult SelectFile( string initialDir, char seperator )
    {
      if (_openDialog == null)
      {
        throw new Exception("OpenFile dialog is null!");
      }

      _openDialog.InitialDirectory = initialDir;
      DialogResult result = _openDialog.ShowDialog();
      if (result != DialogResult.OK)
      {
        return result;
      }

      if (_openDialog.FileNames.Length > 1)
      {
        foreach (string fileName in _openDialog.FileNames)
        {
          edtPath.Text += fileName + seperator;
        }
      }
      else
      {
        edtPath.Text = _openDialog.FileName;
      }
      return result;
    }

    public DialogResult SelectFolder( )
    {
      if (_browseFolderDialog == null)
      {
        throw new Exception("FolderBrowser dialog is null!");
      }

      DialogResult result = _browseFolderDialog.ShowDialog();
      if (result != DialogResult.OK)
      {
        return result;
      }

      edtPath.Text = _browseFolderDialog.SelectedPath + (!_browseFolderDialog.SelectedPath.EndsWith("\\") ? "\\" : String.Empty) ;
      return result;
    }

    private void btnOpen_Click( object sender, EventArgs e )
    {
      switch (_dialogType)
      {
        case DialogType.File:
          if (SelectFile(String.Empty, ';') != DialogResult.OK)
          {
            return;
          }
          break;
        case DialogType.Folder:
          if (SelectFolder() != DialogResult.OK)
          {
            return;
          }
          break;
        default:
          break;
      }

      if (_afterFileOpened != null)
      {
        _afterFileOpened(this, EventArgs.Empty);
      }
    }
  }

  public enum DialogType
  {
    File,
    Folder
  }

}
