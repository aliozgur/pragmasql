/********************************************************************
  Class      : ucScriptEditorShortCuts
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
using System.Reflection;

using PragmaSQL.Core;
using Crad.Windows.Forms.Actions;

namespace PragmaSQL.Core
{
  public partial class ucScriptEditorShortCuts : UserControl, IConfigContentEditor
  {
    private int sortColumn = -1;
    private bool _isModified = false;
    private bool _isContentLoaded = false;
    private SerializableDictionary<ScriptEditorActions, Keys> _newShortcuts = new SerializableDictionary<ScriptEditorActions, Keys>();

    private SerializableDictionary<ScriptEditorActions, Keys> _scriptEditorShortcuts = null;
    public SerializableDictionary<ScriptEditorActions, Keys> ScriptEditorShortcuts
    {
      get { return _scriptEditorShortcuts; }
    }

    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }


    private ActionList _actionList = new ActionList();

    public ucScriptEditorShortCuts( )
    {
      InitializeComponent();
      InitializeActions();
      LoadKeycodes();
    }

    #region ActionList Initialization
    public void InitializeActions( )
    {
      Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update +=new EventHandler(OnAction_EditSelectedItem_Update);
      ac.Execute += new EventHandler(OnAction_EditSelectedItem_Execute);
      ac.ShortcutKeys = Keys.F2;
      ac.Text = "Edit";
      _actionList.Actions.Add(ac);
    }

    private void OnAction_EditSelectedItem_Update( object sender, EventArgs e )
    {
    }

    private void OnAction_EditSelectedItem_Execute( object sender, EventArgs e )
    {
      EditSelectedItem();
    }

    #endregion

    #region IConfigurationContentItem Members

    public string ItemClassName
    {
      get
      {
        return "ScriptEditorShortcuts";
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
      return LoadContent(ConfigHelper.Current);
    }

    public bool LoadContent( ConfigurationContent configContent )
    {
      if (configContent == null)
      {
        throw new NullParameterException("Configuration content param is null!");
      }

      if (configContent.ScriptEditorShortcuts == null)
      {
        throw new NullPropertyException("Configuration content does not contain ScriptEditorShortcuts item!");
      }

      _configContent = configContent;
      _scriptEditorShortcuts = _configContent.ScriptEditorShortcuts;
      LoadInitial();
      _isContentLoaded = true;
      return true;
    }

    public bool SaveContent( )
    {
      foreach (ScriptEditorActions action in _newShortcuts.Keys)
      {
        _scriptEditorShortcuts[action] = _newShortcuts[action];
        ScriptEditorShortcutKeysProvider.SetShortCut(action, _newShortcuts[action]);
      }

      ScriptEditorShortcutKeysProvider.LoadFrom(_scriptEditorShortcuts);
      _newShortcuts.Clear();
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
    private void LoadKeycodes( )
    {
      cmbKeyCodes.Items.Clear();
      Array values = Enum.GetValues(typeof(Keys));

      foreach (Keys key in values)
      {
        if ((key & Keys.KeyCode) == key)
        {
          cmbKeyCodes.Items.Add(key);
        }
      }
      cmbKeyCodes.SelectedIndex = -1;
    }

    private void LoadInitial( )
    {
      lv.Items.Clear();
      Array values = Enum.GetValues(typeof(ScriptEditorActions));

      foreach (ScriptEditorActions action in values)
      {
        ListViewItem item = lv.Items.Add(action.ToString());
        item.SubItems.Add(ScriptEditorShortcutKeysProvider.ShortcutKeysAsStringFromAction(action));
        item.Tag = ScriptEditorShortcutKeysProvider.GetShortCut(action);
      }
    }

    private void LoadFrom(SerializableDictionary<ScriptEditorActions, Keys> source)
    {
      lv.Items.Clear();
      Array values = Enum.GetValues(typeof(ScriptEditorActions));

      foreach (ScriptEditorActions action in source.Keys)
      {
        ListViewItem item = lv.Items.Add(action.ToString());
        
        item.SubItems.Add(ScriptEditorShortcutKeysProvider.ShortcutKeysAsStringFromKeys(source[action]));
        item.Tag = source[action];
      }
    }

    private void EditSelectedItem( )
    {
      try
      {
        panEdit.SuspendLayout();
        if (lv.SelectedItems.Count == 0)
        {
          panEdit.Enabled = false;
          chkAlt.Checked = false;
          chkControl.Checked = false;
          chkAlt.Checked = false;
          cmbKeyCodes.Text = String.Empty;
          textBox1.Text = String.Empty;
          return;
        }
        else
        {
          panEdit.Enabled = true;
        }

        ListViewItem item = lv.SelectedItems[0];
        textBox1.Text = item.Text;
        Keys keys = (Keys)item.Tag;
        RenderKeys(keys);
      }
      finally
      {
        panEdit.ResumeLayout();
      }
    }

    private void RenderKeys( Keys keys )
    {
      Keys modKeys = Keys.Modifiers & keys;

      chkControl.Checked = ((Keys.Control & modKeys) == Keys.Control);
      chkAlt.Checked = ((Keys.Alt & modKeys) == Keys.Alt);
      chkShift.Checked = ((Keys.Shift & modKeys) == Keys.Shift);

      Keys key = Keys.KeyCode & keys;
      cmbKeyCodes.SelectedItem = key;
    }

    private Keys CreateKeysFromUserInput( )
    {
      Keys result = (Keys)cmbKeyCodes.SelectedItem;

      result = result | (chkControl.Checked ? Keys.Control : result);
      result = result | (chkAlt.Checked ? Keys.Alt : result);
      result = result | (chkShift.Checked ? Keys.Shift : result);

      return result;
    }

    #endregion

    private void lv_SelectedIndexChanged( object sender, EventArgs e )
    {
      EditSelectedItem();
    }

    private void btnSet_Click( object sender, EventArgs e )
    {
      if (lv.SelectedItems.Count == 0)
      {
        MessageBox.Show("No shortcut is currently selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      ListViewItem item = lv.SelectedItems[0];
      ScriptEditorActions action = (ScriptEditorActions)Enum.Parse(typeof(ScriptEditorActions), item.Text);

      Keys result = CreateKeysFromUserInput();
      item.Tag = result;
      item.SubItems[1].Text = ScriptEditorShortcutKeysProvider.ShortcutKeysAsStringFromKeys(result);
      _isModified = true;

      if (_newShortcuts.ContainsKey(action))
      {
        _newShortcuts[action] = result;
      }
      else
      {
        _newShortcuts.Add(action, result);
      }
    }

    private void lv_ColumnClick( object sender, ColumnClickEventArgs e )
    {
      // Determine whether the column is the same as the last column clicked.
      if (e.Column != sortColumn)
      {
        // Set the sort column to the new column.
        sortColumn = e.Column;
        // Set the sort order to ascending by default.
        lv.Sorting = SortOrder.Ascending;
      }
      else
      {
        // Determine what the last sort order was and change it.
        if (lv.Sorting == SortOrder.Ascending)
          lv.Sorting = SortOrder.Descending;
        else
          lv.Sorting = SortOrder.Ascending;
      }

      // Call the sort method to manually sort.
      lv.Sort();
      // Set the ListViewItemSorter property to a new ListViewItemComparer
      // object.
      this.lv.ListViewItemSorter = new ListViewItemComparer(e.Column, lv.Sorting);
    }

    private void button1_Click( object sender, EventArgs e )
    {
      if (_configContent == null)
      {
        return;
      }

      //MessageBox.Show("Not implemented yet!","Not Implemented",MessageBoxButtons.OK,MessageBoxIcon.Warning);

      DialogResult dlgRes = MessageBox.Show("Do you want to restore default short cuts for all actions?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (dlgRes == DialogResult.No)
      {
        return;
      }

      _newShortcuts.Clear();
      string[] actionNames = Enum.GetNames(typeof(ScriptEditorActions));
      foreach (string actionName in actionNames)
      {
        ScriptEditorActions action = (ScriptEditorActions)Enum.Parse(typeof(ScriptEditorActions), actionName);
        Keys defShortcut = ScriptEditorShortcutKeysProvider.GetDefaultShortCut(action);
        if (_newShortcuts.ContainsKey(action))
        {
          _newShortcuts[action] = defShortcut;
        }
        else
        {
          _newShortcuts.Add(action, defShortcut);
        }
      }
      LoadFrom(_newShortcuts);
      panEdit.Enabled = false;
      _isModified = true;
    }

  }
}
