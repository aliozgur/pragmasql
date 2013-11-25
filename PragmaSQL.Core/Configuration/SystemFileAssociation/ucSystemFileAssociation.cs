/********************************************************************
  Class      : ucSystemFileAssociation
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;
using BrendanGrant.Helpers.FileAssociation;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public partial class ucSystemFileAssociation : UserControl, IConfigContentEditor
  {
    private DateTime lastUpdate = DateTime.Now;
    private string existingString = "";


    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private SystemFileAssociationOptions _options = null;
    public SystemFileAssociationOptions Options
    {
      get { return _options; }
    }

    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }

    public string ItemClassName
    {
      get
      {
        return "SystemFileAssociations";
      }
    }

    private IList<string> _addBuffer = new List<string>();
    private IList<string> _removeBuffer = new List<string>();


    public ucSystemFileAssociation( )
    {
      InitializeComponent();
    }


    #region IConfigurationContentItem Members

    public bool ContentLoaded
    {
      get
      {
        return _isContentLoaded;
      }
    }

    public bool Modified
    {
      get
      {
        return _isModified;
      }
    }

    public bool LoadContent( )
    {
      return LoadContent(ConfigHelper.Current);
    }

    public bool LoadContent( ConfigurationContent configContent )
    {
      if (configContent == null)
      {
        throw new NullParameterException("Configuration content param is null!");
      }

      if (configContent.SystemFileAssociationOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain SystemFileAssociationOptions item!");
      }

      _configContent = configContent;
      _options = _configContent.SystemFileAssociationOptions;


      LoadInitial();

      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      MergeChanges();
      MergeSqlProjectFileChanges();
      _isModified = false;
      return true;
    }

    public void ShowContent( )
    {
      this.Show();
    }

    public void HideContent( )
    {
      this.Hide();
    }

    #endregion


    #region Methods
    private void LoadInitial( )
    {
      LoadExtensions();
      LoadAssociatedExtensions();
      chkAssociateSqlProjectFiles.Checked = _options.SqlProjectFileAssociated;
    }

    private void LoadExtensions( )
    {
      RegistryKey root = Registry.ClassesRoot;

      string[] subKeys = root.GetSubKeyNames();

      extensionsListBox.Items.Clear();
      foreach (string subKey in subKeys)
      {
        if (subKey.StartsWith("."))
        {
          extensionsListBox.Items.Add(subKey);
        }
      }
    }

    private void LoadAssociatedExtensions( )
    {
      lbAssociated.Items.Clear();
      if (_options == null)
      {
        return;
      }

      foreach (string ass in _options.Associations.Keys)
      {
        lbAssociated.Items.Add(ass);
      }
    }



    private void ShowExtensionDetails( string extension )
    {
      FileAssociationInfo fa = new FileAssociationInfo(extension);
      if (fa == null || !fa.Exists)
      {
        contentTypeTextBox.Text = String.Empty;
        contentTypeTextBox.Tag = String.Empty;

        programIdTextBox.Text = String.Empty;
        programIdTextBox.Tag = String.Empty;

        openWithListBox.DataSource = null;

        PerceivedTypeTextBox.Text = String.Empty;
        extensionLabel.Text = String.Empty;
        return;
      }

      contentTypeTextBox.Text = fa.ContentType;
      contentTypeTextBox.Tag = contentTypeTextBox.Text;

      programIdTextBox.Text = fa.ProgID;
      programIdTextBox.Tag = programIdTextBox.Text;

      openWithListBox.DataSource = fa.OpenWithList;

      PerceivedTypeTextBox.Text = fa.PerceivedType.ToString();
      extensionLabel.Text = fa.Extension;
    }

    private void AddExtension( )
    {
      string ext = (string)extensionsListBox.SelectedItem;
      if (String.IsNullOrEmpty(ext))
      {
        return;
      }

      if (_options.Associations.ContainsKey(ext) || _addBuffer.Contains(ext))
      {
        return;
      }

      _addBuffer.Add(ext);
      lbAssociated.Items.Add(ext);
      _isModified = true;
    }

    private void RemoveExtension( )
    {
      string ext = (string)lbAssociated.SelectedItem;
      if (String.IsNullOrEmpty(ext))
      {
        return;
      }

      if (_removeBuffer.Contains(ext))
      {
        return;
      }

      if (_addBuffer.Contains(ext))
      {
        _addBuffer.Remove(ext);
      }

      _removeBuffer.Add(ext);
      lbAssociated.Items.Remove(ext);
      _isModified = true;
    }

    private void MergeSqlProjectFileChanges( )
    {
      // Actually not changed
      if(_options.SqlProjectFileAssociated == chkAssociateSqlProjectFiles.Checked)
      {
        return;
      }

      _options.SqlProjectFileAssociated = chkAssociateSqlProjectFiles.Checked;
      FileAssociationInfo fa = null;
      ProgramAssociationInfo pa = null;

      if (chkAssociateSqlProjectFiles.Checked)
      {
        fa = new FileAssociationInfo(".sqlprj");
        
        // File association info exists. We have to backup current settings
        if (fa.Exists)
        {
          //Backup open with list
          _options.SqlProjectOpenWithBackup.AddRange(fa.OpenWithList);
          
          //Backup verbs
          pa = new ProgramAssociationInfo(fa.ProgID);
          if (pa.Exists)
          {
            _options.SqlProjectVerbsBackup.AddRange(pa.Verbs);
            _options.SqlProjectFileProgramIDBackup = pa.ProgID;
          }
        }
        // File association info does not exists. We will create
        else
        {
          fa.Create("PragmaSQL Project File");
          pa = new ProgramAssociationInfo(fa.ProgID);
        }

        fa.OpenWithList = new string[1] { "PragmaSQL.exe" };
        
        pa = new ProgramAssociationInfo(fa.ProgID);
        if(!pa.Exists)
        {
          pa.Create();
        }
        if ( pa != null && pa.Exists )
        {
          ProgramVerb verb = new ProgramVerb("open", SystemFileAssociationOptions.PragmaSQLOpenCommand);
          pa.Verbs = new ProgramVerb[1] { verb };
        }
      }
      else
      {
        // Rollback to backup
        fa = new FileAssociationInfo(".sqlprj");
        if(fa.Exists)
        {
          fa.OpenWithList = _options.SqlProjectOpenWithBackup.ToArray();
          _options.SqlProjectOpenWithBackup.Clear();
          
          pa = new ProgramAssociationInfo(fa.ProgID);
          if(pa.Exists)
          {
            pa.Delete();
            if(!String.IsNullOrEmpty(_options.SqlProjectFileProgramIDBackup))
            {
              pa = new ProgramAssociationInfo(_options.SqlProjectFileProgramIDBackup);
              if(!pa.Exists)
              {
                pa.Create();
              }
              pa.Verbs = _options.SqlProjectVerbsBackup.ToArray();
            }
            _options.SqlProjectVerbsBackup.Clear();
          }
        }
      }

    }

    private void MergeChanges( )
    {
      FileAssociationInfo fa = null;
      ProgramAssociationInfo pa = null;

      // 1- Add new associations and backup current settings
      foreach (string ext in _addBuffer)
      {
        if (_options.Associations.ContainsKey(ext))
        {
          continue;
        }

        // 1.1 Add association
        _options.Associations.Add(ext, ext);

        // 1.2 Open with backup
        fa = new FileAssociationInfo(ext);
        if (fa == null || !fa.Exists)
        {
          continue;
        }

        _options.OpenWithBackup.Add(ext, fa.OpenWithList);

        // 1.3 Verb nackup
        pa = new ProgramAssociationInfo(fa.ProgID);
        if (pa == null || !pa.Exists)
        {
          continue;
        }
        _options.VerbsBackup.Add(ext, pa.Verbs);
      }

      //2- Remove associations and restore backed up settings
      string[] backedUpOpenWithList = null;
      ProgramVerb[] backedUpVerbs = null;

      foreach (string ext in _removeBuffer)
      {
        if (!_options.OpenWithBackup.ContainsKey(ext))
        {
          continue;
        }

        // 2.1 Rollback to backed up open with list
        fa = new FileAssociationInfo(ext);
        if (fa == null || !fa.Exists)
        {
          continue;
        }
        backedUpOpenWithList = _options.OpenWithBackup[ext];
        fa.OpenWithList = backedUpOpenWithList;

        //2.2 Rollback to backed up verbs
        pa = new ProgramAssociationInfo(fa.ProgID);
        if (pa == null || !pa.Exists)
        {
          continue;
        }

        backedUpVerbs = _options.VerbsBackup[ext];
        pa.Verbs = backedUpVerbs;

        _options.Associations.Remove(ext);
        _options.OpenWithBackup.Remove(ext);
        _options.VerbsBackup.Remove(ext);
      }


      //3- Apply verbs and open with list
      foreach (string ext in _options.Associations.Keys)
      {
        // 3.1 Apply open with
        fa = new FileAssociationInfo(ext);
        if (fa == null || !fa.Exists)
        {
          continue;
        }
        fa.OpenWithList = new string[1] { "PragmaSQL.exe" };


        // 3.2 Apply verbs
        pa = new ProgramAssociationInfo(fa.ProgID);
        if (pa == null || !pa.Exists)
        {
          continue;
        }
        ProgramVerb verb = new ProgramVerb("open", SystemFileAssociationOptions.PragmaSQLOpenCommand);
        pa.Verbs = new ProgramVerb[1] { verb };
      }
    }

    #endregion

    private void button1_Click( object sender, EventArgs e )
    {
      LoadExtensions();
    }

    private void extensionsListBox_KeyPress( object sender, KeyPressEventArgs e )
    {
      if (DateTime.Now.Subtract(lastUpdate).TotalMilliseconds > 250)
      {
        existingString = "";
      }

      existingString += e.KeyChar;
      for (int x = 0; x < extensionsListBox.Items.Count; x++)
      {
        if (extensionsListBox.Items[x].ToString().ToLower().StartsWith(existingString.ToLower()))
        {
          extensionsListBox.SelectedIndex = x;
          break;
        }
      }

      lastUpdate = DateTime.Now;
    }

    private void extensionsListBox_SelectedIndexChanged( object sender, EventArgs e )
    {
      ShowExtensionDetails((string)extensionsListBox.SelectedItem);
    }

    private void lbAssociated_SelectedIndexChanged( object sender, EventArgs e )
    {
      ShowExtensionDetails((string)extensionsListBox.SelectedItem);
    }

    private void lbAssociated_KeyPress( object sender, KeyPressEventArgs e )
    {
      if (DateTime.Now.Subtract(lastUpdate).TotalMilliseconds > 250)
      {
        existingString = "";
      }

      existingString += e.KeyChar;
      for (int x = 0; x < extensionsListBox.Items.Count; x++)
      {
        if (lbAssociated.Items[x].ToString().ToLower().StartsWith(existingString.ToLower()))
        {
          lbAssociated.SelectedIndex = x;
          break;
        }
      }

      lastUpdate = DateTime.Now;
    }

    private void button3_Click( object sender, EventArgs e )
    {
      AddExtension();
    }

    private void button4_Click( object sender, EventArgs e )
    {
      RemoveExtension();
    }

    private void checkBox1_CheckStateChanged( object sender, EventArgs e )
    {
      panel6.Visible = checkBox1.Checked;
    }

    private void chkAssociateSqlProjectFiles_CheckedChanged( object sender, EventArgs e )
    {
      _isModified = true;
    }

  }
}
