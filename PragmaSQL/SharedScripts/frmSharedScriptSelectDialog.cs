using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;

using MWControls;
using MWCommon;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public enum SharedScriptSelectDialogMode
  {
    Open,
    Save
  }

  public partial class frmSharedScriptSelectDialog : KryptonForm
  {
    public frmSharedScriptSelectDialog()
    {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif
      InitializeComponent();
      ucSharedScripts1.InitializeAddInSupport();      
    }

    public ucSharedScripts SharedScriptsControl
    {
      get
      {
        return ucSharedScripts1;
      }
    }

    private string _script = String.Empty;
    public string Script
    {
      get { return _script; }
      set { _script = value; }
    }

    private SharedScriptsItemData _savedItemData = null;
    public SharedScriptsItemData SavedItemData
    {
      get { return _savedItemData; }
    }

    private SharedScriptSelectDialogMode _mode = SharedScriptSelectDialogMode.Open;
    public SharedScriptSelectDialogMode Mode
    {
      get
      { return _mode; }
      set
      {
        _mode = value;
        ApplyMode();
      }
    }
    private void ApplyMode()
    {
      switch (_mode)
      {
        case SharedScriptSelectDialogMode.Open:
          Text = "Open Shared Script";
          panItemName.Visible = false;
          btnOk.Text = "Open";
          Icon = PragmaSQL.Properties.Resources.sharedScript;
          ucSharedScripts1.Mode = SharedScriptsMode.Open;
          break;
        case SharedScriptSelectDialogMode.Save:
          Text = "Save Shared Script";
          panItemName.Visible = true;
          btnOk.Text = "Save";
          Icon = PragmaSQL.Properties.Resources.saveItem;
          ucSharedScripts1.Mode = SharedScriptsMode.Save;
          break;
        default:
          throw new Exception("Invalid shared scripts dialog mode!");
      }
    }

    public string ItemName
    {
      get { return txtItemName.Text; }
      set { txtItemName.Text = value; }
    }

    private bool SaveScript()
    {
      if (ConfigHelper.Current == null || ConfigHelper.Current.PragmaSqlDbConn == null)
      {
        MessageBox.Show("PragmaSQL Systtem connection not specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }

      SharedScriptsService facade = new SharedScriptsService();
      facade.ConnParams = ConfigHelper.Current.PragmaSqlDbConn;

      SharedScriptsItemData data = ucSharedScripts1.SelectedNodeData;
      int? parentID = null;
      
      if (data.Type == GenericItemType.Folder)
      {
        parentID = data.ID;
        data = SharedScriptsItemDataFactory.Create(txtItemName.Text, GenericItemType.Item, null, parentID, String.Empty);
        data.Script = Script;
        facade.AddItem(data);
        _savedItemData = data;
        return true;
      }
      else if (data.Type == GenericItemType.Item)
      {
        if (data.Name.ToLowerInvariant() == txtItemName.Text.ToLowerInvariant())
        {
          DialogResult dlgRes = MessageBox.Show("Item with name \"" + data.Name + "\" already exists!\n"
            + "Do you want to overwrite this item?", "Warning"
            , MessageBoxButtons.YesNo
            , MessageBoxIcon.Warning
            , MessageBoxDefaultButton.Button2
            );
          
          if (dlgRes == DialogResult.No)
          {
            return false;
          }
          data.Script = Script;
          facade.UpdateItem(data);
          _savedItemData = data;
          return true;
        }
        else
        {
          parentID = data.ParentID;
          data = SharedScriptsItemDataFactory.Create(txtItemName.Text, GenericItemType.Item, null, parentID, String.Empty);
          data.Script = Script;
          facade.AddItem(data);
          _savedItemData = data;
          return true;
        }
      }
      else
      {
        throw new Exception("Item type not supported!");
      }
    }


    #region Static Methods

    private static IList<SharedScriptsItemData> ShowOpenSharedScriptDialog_MultiSelect()
    {
      IList<SharedScriptsItemData> result = new List<SharedScriptsItemData>();

      frmSharedScriptSelectDialog frm = new frmSharedScriptSelectDialog();
      frm.Mode = SharedScriptSelectDialogMode.Open;
      if (!frm.SharedScriptsControl.LoadFromDefaultConnection(true) || (frm.ShowDialog() != DialogResult.OK))
      {
        frm.Dispose();
        frm = null;
        return result;
      }

      foreach (MWTreeNodeWrapper nodeWrapper in frm.SharedScriptsControl.SelectedNodesRaw.Values)
      {
        SharedScriptsItemData data = SharedScriptsItemDataFactory.GetNodeData(nodeWrapper.Node);
        if (data == null)
        {
          continue;
        }
        result.Add(data);
      }
      return result;
    }

		private static SharedScriptsItemData ShowOpenSharedScriptDialog_SingleSelect()
		{
			frmSharedScriptSelectDialog frm = new frmSharedScriptSelectDialog();
			frm.Mode = SharedScriptSelectDialogMode.Open;
			frm.ucSharedScripts1.tv.MultiSelect = TreeViewMultiSelect.NoMulti;
			if (!frm.SharedScriptsControl.LoadFromDefaultConnection(true) || (frm.ShowDialog() != DialogResult.OK))
			{
				frm.Dispose();
				frm = null;
				return null;
			}

			return frm.ucSharedScripts1.SelectedNodeData;
		}

    private static SharedScriptsItemData ShowSaveSharedScriptDialog(string script, string itemName)
    {
      frmSharedScriptSelectDialog frm = new frmSharedScriptSelectDialog();
      frm.Script = script;
      frm.Mode = SharedScriptSelectDialogMode.Save;
      frm.ItemName = itemName;
      if (!frm.SharedScriptsControl.LoadFromDefaultConnection(true) || (frm.ShowDialog() != DialogResult.OK))
      {
        frm.Dispose();
        frm = null;
        return null;
      }

      return frm.SavedItemData;
    }
		
    public static bool OpenSharedScripts(IPragmaEditor currentEditor, ConnectionParams cp)
    {
			bool result = false;
      if (currentEditor != null && currentEditor.ContentModified)
      {
        DialogResult dlgRes = MessageBox.Show("Save changes to \"" + currentEditor.Caption + "\" before opening another script in current script editor", "Save Script", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        if (dlgRes == DialogResult.Cancel)
        {
          return false;
        }
        else if (dlgRes == DialogResult.Yes)
        {
          currentEditor.SaveContent();
        }
      } 

      IList<IPragmaEditor> editors = new List<IPragmaEditor>();
      IList<SharedScriptsItemData> scripts = frmSharedScriptSelectDialog.ShowOpenSharedScriptDialog_MultiSelect();
      int i = 0;
      IPragmaEditor frm = null;
			foreach (SharedScriptsItemData data in scripts)
      {
        if (data.Type != GenericItemType.Item)
        {
          continue;
        }

        i++;
        if (i == 1 && currentEditor != null)
        {
          currentEditor.Caption = data.Name;
          currentEditor.Icon = PragmaSQL.Properties.Resources.sharedScript;
          currentEditor.ContentPersister = new SharedScriptContentPersister();
          currentEditor.ContentPersister.Data = data;
          currentEditor.ContentPersister.Hint = "This is a shared script: " + data.Name;
          currentEditor.ContentInfo = currentEditor.ContentPersister.Hint;
          currentEditor.ContentPersister.FilePath = data.Name;
          currentEditor.ActiveDocument.TextContent = data.Script;
          currentEditor.ContentModified = false;
					result = true;
          continue;
        }
        if (cp != null)
        {
          frm = ScriptEditorFactory.OpenSharedScript(data, cp);
          editors.Add(frm);
        }
        else
        {
          frm = TextEditorFactory.OpenSharedScript(data);
          editors.Add(frm);        
        }

      }

      foreach (IPragmaEditor editor in editors)
      {
        if (editor is frmScriptEditor)
        {
          ScriptEditorFactory.ShowScriptEditor(editor as frmScriptEditor);
        }
        else if (editor is frmTextEditor)
        {
          TextEditorFactory.ShowTextEditor(editor as frmTextEditor);
        }
      }
			
			return result;
    }

		public static bool OpenSharedScript(IPragmaEditor currentEditor, ConnectionParams cp)
		{
			if (currentEditor != null && currentEditor.ContentModified)
			{
				DialogResult dlgRes = MessageBox.Show("Save changes to \"" + currentEditor.Caption + "\" before opening another script in current script editor", "Save Script", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (dlgRes == DialogResult.Cancel)
				{
					return false;
				}
				else if (dlgRes == DialogResult.Yes)
				{
					currentEditor.SaveContent();
				}
			}

			SharedScriptsItemData data = frmSharedScriptSelectDialog.ShowOpenSharedScriptDialog_SingleSelect();
			if (data != null && currentEditor != null)
			{
				currentEditor.Caption = data.Name;
				currentEditor.Icon = PragmaSQL.Properties.Resources.sharedScript;
				currentEditor.ContentPersister = new SharedScriptContentPersister();
				currentEditor.ContentPersister.Data = data;
				currentEditor.ContentPersister.Hint = "This is a shared script: " + data.Name;
				currentEditor.ContentInfo = currentEditor.ContentPersister.Hint;
				currentEditor.ContentPersister.FilePath = data.Name;
				currentEditor.ActiveDocument.TextContent = data.Script;
				currentEditor.ContentModified = false;
				return true;
			}
			else
				return false;
		}
   

    public static bool SaveAsSharedScript(IPragmaEditor currentEditor, string itemName)
    {
      if (currentEditor == null)
        return false;

      SharedScriptsItemData savedItemData = frmSharedScriptSelectDialog.ShowSaveSharedScriptDialog(currentEditor.ActiveDocument.TextContent, currentEditor.Caption);

      if (savedItemData == null)
      {
        return false;
      }
      currentEditor.Caption = savedItemData.Name;
      currentEditor.Icon = PragmaSQL.Properties.Resources.sharedScript;
      currentEditor.ContentPersister = new SharedScriptContentPersister();
      currentEditor.ContentPersister.Data = savedItemData;
      currentEditor.ContentPersister.Hint = "This is a shared script: " + savedItemData.Name;
      currentEditor.ContentInfo = currentEditor.ContentPersister.Hint;
      currentEditor.ContentPersister.FilePath = savedItemData.Name;

			return true;
    }


    #endregion


    private void btnOk_Click(object sender, EventArgs e)
    {
			if (_mode == SharedScriptSelectDialogMode.Open)			
			{
				if (ucSharedScripts1.SelectedNode != null)
					DialogResult = DialogResult.OK;
				else
					DialogResult = DialogResult.None;
			}
      else if (_mode == SharedScriptSelectDialogMode.Save)
      {
        if (String.IsNullOrEmpty(txtItemName.Text))
        {
          MessageBox.Show("Item name not specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          DialogResult = DialogResult.None;
        }
        else if (SaveScript())
        {
          DialogResult = DialogResult.OK;
        }
      }
    }

    private void ucSharedScripts1_SelectedItemChanged(object sender, SharedScriptsItemData itemData)
    {
      if (itemData == null || itemData.Type != GenericItemType.Item)
      {
        if(_mode == SharedScriptSelectDialogMode.Open)
          txtItemName.Text = String.Empty;
        return;
      }

      txtItemName.Text = itemData.Name;
    }
  }
}