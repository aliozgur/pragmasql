using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
	/// <summary>
	/// Configuration control for user defined code completion list.
	/// <para>This user control is renedered in main options dialog</para>
	/// <see cref="frmConfigurationDlg"/>
	/// </summary>
  public partial class ucCodeCompletionLists : UserControl, IConfigContentEditor
  {
    private bool _isModified = false;
    private bool _isContentLoaded = false;
    private bool _doNotLoad = false;

    List<CodeCompletionList> _codeCompletionLists = null;
    public List<CodeCompletionList> CodeCompletionLists
    {
      get { return _codeCompletionLists; }
    }

    public ucCodeCompletionLists( )
    {
      InitializeComponent();
    }

    #region IConfigurationContentItem Members
    public string ItemClassName
    {
      get
      {
        return "CodeCompletionLists";
      }
    }

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
      CodeCompletionListLoader.Load();
      if (CodeCompletionListLoader.Lists == null)
      {
        throw new NullParameterException("Code Completion Lists can not be loaded!");
      }

      _codeCompletionLists = CodeCompletionListLoader.Lists;


      LoadInitial();

      _isContentLoaded = true;
      return true;
    }

    public bool LoadContent( ConfigurationContent configContent )
    {
      throw new Exception("Use \"public bool Load()\" insetead of \"public bool LoadContent( ConfigurationContent configContent )\" ");
    }

    public bool SaveContent( )
    {
      CodeCompletionListLoader.Save(_codeCompletionLists);
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

    #region Load Methods

    private void LoadInitial( )
    {
      if (_doNotLoad)
      {
        return;
      }

      LoadLists();
    }

    private void LoadLists( )
    {
      if (_doNotLoad)
      {
        return;
      }

      lbLists.Items.Clear();
      if (_codeCompletionLists == null)
      {
        return;
      }

      foreach (CodeCompletionList list in _codeCompletionLists)
      {
        lbLists.Items.Add(list);
      }

      if (lbLists.Items.Count > 0)
      {
        lbLists.SelectedIndex = 0;
      }
      LoadListItems();
    }

    private void LoadListItems( )
    {
      if (_doNotLoad)
      {
        return;
      }

      lbItems.Items.Clear();
      if (_codeCompletionLists == null || lbLists.SelectedItem == null)
      {
        LoadItemContent();
        return;
      }

      CodeCompletionList selList = lbLists.SelectedItem as CodeCompletionList;
      foreach (string key in selList.Items.Keys)
      {
        lbItems.Items.Add(key);
      }

      if (lbItems.Items.Count > 0)
      {
        lbItems.SelectedIndex = 0;
      }
      LoadItemContent();
    }

    private void LoadItemContent( )
    {
      if (_doNotLoad)
      {
        return;
      }

      CodeCompletionList selList = lbLists.SelectedItem as CodeCompletionList;
      string selKey = lbItems.SelectedItem as string;

      if (selList == null || String.IsNullOrEmpty(selKey))
      {
        txtTemplateCode.Text = String.Empty;
        return;
      }
      txtTemplateCode.Text = selList[selKey];
    }

    #endregion

    #region List operations
    private void AddList( )
    {
      string name = txtListName.Text;
      if (String.IsNullOrEmpty(name))
      {
        return;
      }
      CodeCompletionList newList = new CodeCompletionList();
      newList.Name = name;

      _codeCompletionLists.Add(newList);
      lbLists.ClearSelected();
      lbLists.SelectedIndex = lbLists.Items.Add(newList);
      _isModified = true;
    }

    private void RenameList( )
    {
      CodeCompletionList selList = lbLists.SelectedItem as CodeCompletionList;
      if (selList == null)
      {
        return;
      }

      string name = String.Empty;
      if (InputDialog.ShowDialog("Rename List", "Name", ref name) != DialogResult.OK)
      {
        return;
      }

      selList.Name = name;
      _isModified = true;
    }

    private void DeleteSelectedLists( )
    {
      if (lbLists.SelectedItems.Count == 0)
      {
        return;
      }

      DialogResult dlgRes = MessageBox.Show("Delete selected list(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dlgRes == DialogResult.No)
      {
        return;
      }

      foreach (CodeCompletionList selList in lbLists.SelectedItems)
      {
        _codeCompletionLists.Remove(selList);
      }

      try
      {
        _doNotLoad = true;
        while (lbLists.SelectedItems.Count > 0)
        {
          lbLists.Items.Remove(lbLists.SelectedItems[0]);
        }
      }
      finally
      {
        _doNotLoad = false;
      }

      LoadListItems();
      _isModified = true;
    }

    #endregion

    #region List Content Operations

    private void AddNewItem( )
    {

      CodeCompletionList selList = lbLists.SelectedItem as CodeCompletionList;
      if (selList == null)
      {
        return;
      }

      string name = txtTemplateName.Text;
      if (String.IsNullOrEmpty(name))
      {
        return;
      }


      if (selList.Items.ContainsKey(name))
      {
        MessageBox.Show(String.Format("List content with key \"{0}\" already defined!", name)
          , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      selList.Items.Add(name, String.Empty);
      lbItems.ClearSelected();
      lbItems.SelectedIndex = lbItems.Items.Add(name);
      LoadItemContent();

      _isModified = true;
    }

    private void DeleteSelectedItems( )
    {
      if (lbItems.SelectedItems.Count == 0)
      {
        return;
      }

      CodeCompletionList selList = lbLists.SelectedItem as CodeCompletionList;
      if (selList == null)
      {
        return;
      }
      DialogResult dlgRes = MessageBox.Show("Delete selected item(s)?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dlgRes == DialogResult.No)
      {
        return;
      }

      foreach (string key in lbItems.SelectedItems)
      {
        selList.Items.Remove(key);
      }

      try
      {
        _doNotLoad = true;
        while (lbItems.SelectedItems.Count > 0)
        {
          lbItems.Items.Remove(lbItems.SelectedItems[0]);
        }
      }
      finally
      {
        _doNotLoad = false;
      }

      txtTemplateCode.Text = String.Empty;
      if (lbItems.SelectedItems.Count > 0)
      {
        lbItems.SelectedIndex = 0;
      }

      LoadItemContent();
    }

    private void ApplyContentToItem( )
    {
      if (lbItems.SelectedItems.Count == 0)
      {
        return;
      }

      CodeCompletionList selList = lbLists.SelectedItem as CodeCompletionList;
      if (selList == null)
      {
        return;
      }

      string key = lbItems.SelectedItem as string;
      if (String.IsNullOrEmpty(key))
      {
        return;
      }

      if (selList[key].Trim() != txtTemplateCode.Text.Trim())
      {
        selList[key] = txtTemplateCode.Text;
        _isModified = true;
      }
    }


    #endregion

    private void lbLists_SelectedIndexChanged( object sender, EventArgs e )
    {
      LoadListItems();
    }

    private void lbItems_SelectedIndexChanged( object sender, EventArgs e )
    {
      LoadItemContent();
    }

    private void toolStripButton1_Click( object sender, EventArgs e )
    {
      AddList();
    }

    private void toolStripButton2_Click( object sender, EventArgs e )
    {
      DeleteSelectedLists();
    }

    private void toolStripButton3_Click( object sender, EventArgs e )
    {
      RenameList();
    }

    private void toolStripButton4_Click( object sender, EventArgs e )
    {
      AddNewItem();
    }

    private void toolStripButton5_Click( object sender, EventArgs e )
    {
      DeleteSelectedItems();
    }

    private void txtListName_KeyDown( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Enter)
      {
        AddList();
      }
    }

    private void txtContentKey_KeyDown( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Enter)
      {
        AddNewItem();
      }
    }

    private void txtItemContent_Leave( object sender, EventArgs e )
    {
      ApplyContentToItem();
    }

    private void lbItems_KeyUp( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Delete)
      {
        DeleteSelectedItems();
      }
    }

    private void lbLists_KeyUp( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Delete)
      {
        DeleteSelectedLists();
      }
    }
  }
}
