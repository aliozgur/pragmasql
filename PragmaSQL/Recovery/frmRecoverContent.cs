using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using ComponentFactory.Krypton.Toolkit;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public partial class frmRecoverContent : KryptonForm
  {
    private enum OperationMode
    {
      Recover,
      Restore
    }

    private IDictionary<string, ListViewGroup> _groups = new Dictionary<string, ListViewGroup>();
    private OperationMode OpMode = OperationMode.Recover;
    private string DataFolder = RecoverContent.RecoverFolder;
    private IDictionary<string, ConnectionParams> _connRep = null;

    public frmRecoverContent()
    {
      InitializeComponent();
      _connRep = ConnectionParamsFactory.EnumerateConnections();
    }

    public static void ShowRecoverForm()
    {
      frmRecoverContent frm = new frmRecoverContent();
      frm.OpMode = OperationMode.Recover;
      frm.ApplyOperationMode();
      frm.PopulateItems();
      frm.ShowDialog();
    }

    private void ApplyOperationMode()
    {
      switch (OpMode)
      {
        case OperationMode.Restore:
          DataFolder = RecoverContent.WorkspaceFolder;
          label1.Text = Properties.Resources.RecoverFormInfo_WorkspaceRestore;
          break;
        case OperationMode.Recover:
        default:
          DataFolder = RecoverContent.RecoverFolder;
          label1.Text = Properties.Resources.RecoverFormInfo_UnexpectedShutDown;
          break;
      }

    }

    public static void ShowWorkspaceRestoreForm()
    {
      frmRecoverContent frm = new frmRecoverContent();
      frm.OpMode = OperationMode.Restore;
      frm.ApplyOperationMode();
      frm.PopulateItems();
      frm.ShowDialog();
    }

    private void PopulateItems()
    {
      lv.Items.Clear();
      IList<RecoverContent> items = RecoverContent.LoadAll(DataFolder);
      ListViewGroup group = null;
      foreach (RecoverContent item in items)
      {
        group = GetGroup(item);
        ListViewItem lvItem = new ListViewItem(item.Title, group);

        lvItem.SubItems.Add(item.ItemType.ToString());
        if (item.ItemType == RecoverContentType.Script || item.ItemType == RecoverContentType.ScriptFile)
          lvItem.SubItems.Add(item.Database);
        else
          lvItem.SubItems.Add(String.Empty);

        lvItem.SubItems.Add(item.FileName);
        lvItem.SubItems.Add(DateTime.FromFileTime(item.Time).ToString());
        lvItem.Checked = true;
        lv.Items.Add(lvItem);
        lvItem.Tag = item;
      }
    }

    private ListViewGroup GetGroup(RecoverContent item)
    {
      if (item == null || String.IsNullOrEmpty(item.Server))
        return null;

      string svName = item.Server.ToLowerInvariant();
      ListViewGroup group = null;
      if (_groups.ContainsKey(svName))
        return _groups[svName];
      else
      {
        group = new ListViewGroup(String.Format("Server : {0}",item.Server));
        _groups.Add(svName, group);
        lv.Groups.Add(group);
      }

      return group;
    }

    private void btnRecover_Click(object sender, EventArgs e)
    {
      PerformRestore();
      DialogResult = DialogResult.OK;
    }

    private void PerformRestore()
    {
      ConnectionParams cp = null;
      IList<frmTextEditor> textEditors = new List<frmTextEditor>();
      IList<frmScriptEditor> scriptEditors = new List<frmScriptEditor>();
      StringBuilder sbErrors = new StringBuilder();
      bool showTextEditorInfoHeader = false;
      bool failedConnections = false;
      string scriptWarningText = String.Empty;

      InitializeProgress(lv.CheckedItems.Count);
      try
      {
        foreach (ListViewItem lvItem in lv.Items)
        {
          cp = null;
          scriptWarningText = String.Empty;
          RecoverContent item = lvItem.Tag as RecoverContent;
          if (!lvItem.Checked || item == null)
            continue;

          string itemCaption = item.Title;
          failedConnections = false;
          showTextEditorInfoHeader = false;
          frmTextEditor txtEditor = null;
          frmScriptEditor scriptEditor = null;

          try
          {
            if (item.ItemType == RecoverContentType.Script || item.ItemType == RecoverContentType.ScriptFile)
            {
              if (item.HasConnectionInfo)
              {
                cp = PrepareWorkspaceItemConnection(item, out failedConnections);

                if (cp == null && !failedConnections)
                  Utils.ShowWarning(String.Format(Properties.Resources.RecoverScriptWarning, item.Database, item.Server), MessageBoxButtons.OK);
              }

              if (cp == null)
              {
                item.SyntaxMode = "SQL";
                if (item.ItemType == RecoverContentType.Script)
                  item.ItemType = RecoverContentType.Text;
                else if (item.ItemType == RecoverContentType.ScriptFile)
                  item.ItemType = RecoverContentType.TextFile;
                showTextEditorInfoHeader = true;
                scriptWarningText = !item.HasConnectionInfo ? Properties.Resources.RecoverScript_NoConnWarningInfoHeader 
                  : String.Format(Properties.Resources.RecoverScriptWarningInfoHeader, item.Database, item.Server) ;

              }
              else
              {
                if (item.ItemType == RecoverContentType.Script)
                  scriptEditor = ScriptEditorFactory.CreateWithAsyncConnection(itemCaption, item.Content, cp, cp.Database, String.Empty);
                else if (item.ItemType == RecoverContentType.ScriptFile)
                  scriptEditor = ScriptEditorFactory.CreateWithAsyncConnection(itemCaption, item.Content, cp, cp.Database, item.FileName);

                scriptEditor.Uid = item.Uid;
                scriptEditor.IsRecoveredContent = true;
                scriptEditor.ObjectType = item.ObjectType;
                if (OpMode == OperationMode.Recover)
                  scriptEditor.ShowInfo(String.Format("Recovered content. Content automatically saved on {0}", DateTime.FromFileTime(item.Time)));
                scriptEditors.Add(scriptEditor);
              }
            }

            txtEditor = null;
            switch (item.ItemType)
            {
              case RecoverContentType.Script:
              case RecoverContentType.ScriptFile:
                break;
              case RecoverContentType.SharedScript:
                txtEditor = TextEditorFactory.CreateSharedScript(itemCaption, item.Content);
                txtEditor.SetSyntaxMode("SQL");
                txtEditor.Uid = item.Uid;
                txtEditor.IsRecoveredContent = true;
                break;
              case RecoverContentType.SharedSnippet:
                txtEditor = TextEditorFactory.CreateSharedSnippet(itemCaption, item.Content);
                txtEditor.SetSyntaxMode("SQL");
                txtEditor.Uid = item.Uid;
                txtEditor.IsRecoveredContent = true;
                break;
              case RecoverContentType.Text:
                txtEditor = TextEditorFactory.Create(itemCaption, item.Content, item.SyntaxMode);
                txtEditor.Uid = item.Uid;
                txtEditor.IsRecoveredContent = true;
                break;
              case RecoverContentType.TextFile:
                txtEditor = TextEditorFactory.Create(itemCaption, item.Content, item.SyntaxMode, item.FileName);
                txtEditor.Uid = item.Uid;
                txtEditor.IsRecoveredContent = true;
                break;
              default:
                throw new Exception(String.Format("Unknowm item type: {0}", item.ItemType));
            }

            if (txtEditor != null)
            { 
              if (showTextEditorInfoHeader)
                txtEditor.ShowInfo(scriptWarningText);
              else if (OpMode == OperationMode.Recover)
                txtEditor.ShowInfo(String.Format("Recovered content. Automatically saved on: {0}", DateTime.FromFileTime(item.Time)));

              textEditors.Add(txtEditor);
            }

            ChangeProgress(String.Format("Restoring {0}", item.Title));
          }
          catch (Exception ex)
          {
            sbErrors.AppendLine(String.Format("- Can not restore '{0} ({1})'. Error: {2}", item.Title, item.ItemType, ex.Message));
          }
          Application.DoEvents();
        }

      }
      finally
      {
        ResetProgress();
      }

      try
      {
        this.Visible = false;
        foreach (frmTextEditor editor in textEditors)
          TextEditorFactory.ShowTextEditor(editor);
        foreach (frmScriptEditor editor in scriptEditors)
          ScriptEditorFactory.ShowScriptEditor(editor);
      }
      catch (Exception ex)
      {
        this.Visible = true;
        throw ex;
      }

      if (sbErrors.Length > 0)
        GenericErrorDialog.ShowError("Content Restore Error", "Some content items can not be restored.", sbErrors.ToString());
    }

    IDictionary<string, ConnectionParams> _confirmedConnections = new Dictionary<string, ConnectionParams>();
    IDictionary<string, ConnectionParams> _failedConnections = new Dictionary<string, ConnectionParams>();

    /*
    private ConnectionParams PrepareWorkspaceItemConnection(RecoverContent item, out bool failedConnections)
    {
      failedConnections = false;
      string key = item.Database.Trim().ToLowerInvariant();
      key += (Char)29 + item.Server.Trim().ToLowerInvariant();

      ConnectionParams cp = null;
      if (_confirmedConnections.ContainsKey(key))
        return _confirmedConnections[key];
      else if (_failedConnections.ContainsKey(key))
      {
        failedConnections = true;
        return null;
      }
      else
      {
        cp = new ConnectionParams();
        cp.Server = item.Server;
        cp.Database = item.Database;
        cp.UserName = item.IntegratedSecurity.Length == 0 ? item.Username : String.Empty;
        cp.IntegratedSecurity = item.IntegratedSecurity;
        cp = ConfirmConnection(cp);

        if (cp == null)
        {
          if (!_failedConnections.ContainsKey(key))
            _failedConnections.Add(key, cp);

          return null;
        }

        if (!_confirmedConnections.ContainsKey(key))
          _confirmedConnections.Add(key, cp);

        return cp;
      }
    }

    private ConnectionParams ConfirmConnection(ConnectionParams cp)
    {
      string normalServerName = cp.Server.Trim().ToLowerInvariant();
      frmConnectionParams frm = new frmConnectionParams(cp, false, true);
      frm.InfoMessage = "Content Restore operation needs you to confirm the database connection.";
      frm.ResetPassword();
      frm.LogonOnly = true;
      return frm.ShowDialog() != DialogResult.OK ? null : frm.GetCurrentConnectionSpec();
    }
    */

    private ConnectionParams PrepareWorkspaceItemConnection(RecoverContent item, out bool failedConnections)
    {
      failedConnections = false;

      string key = item.Database.Trim().ToLowerInvariant();
      key += (Char)29 + item.Server.Trim().ToLowerInvariant();

      ConnectionParams cp = null;
      if (_confirmedConnections.ContainsKey(key))
      {
        return _confirmedConnections[key];
      }
      else
      {
        cp = new ConnectionParams();
        cp.Server = item.Server;
        cp.Database = item.Database;
        if (item.IntegratedSecurity.Length == 0)
          cp.UserName = item.Username;
        else
          cp.UserName = String.Empty;

        cp.IntegratedSecurity = item.IntegratedSecurity;

        cp = ConfirmConnection(cp);
        if (cp == null)
        {
          if (!_failedConnections.ContainsKey(key))
            _failedConnections.Add(key, cp);
          return null;
        }
        key = item.Database.Trim().ToLowerInvariant();
        key += (Char)29 + cp.Server.Trim().ToLowerInvariant();
        if (!_confirmedConnections.ContainsKey(key))
        {
          _confirmedConnections.Add(key, cp);
        }
        return cp;
      }
    }

    private ConnectionParams ConfirmConnection(ConnectionParams cp)
    {
      string normalServerName = cp.Server.Trim().ToLowerInvariant();
      if (_connRep.ContainsKey(normalServerName))
      {
        ConnectionParams result = _connRep[normalServerName];
        result.Database = cp.Database;
        return result;
      }


      frmConnectionParams frm = new frmConnectionParams(cp, false, true);
      frm.InfoMessage = "Content Restore operation needs you to confirm the database connection.";

      frm.ResetPassword();
      if (frm.ShowDialog() != DialogResult.OK)
      {
        return null;
      }
      else
      {
        return frm.GetCurrentConnectionSpec();
      }
    }

    private void ChangeProgress(string progressMsg)
    {
      if (!pnlProgress.Visible)
        pnlProgress.Visible = true;

      lblProgress.Text = progressMsg;
      pb.Increment(1);
    }

    private void InitializeProgress(int max)
    {
      pb.Minimum = 0;
      pb.Maximum = max;

      if (!pnlProgress.Visible)
        pnlProgress.Visible = true;
    }

    private void ResetProgress()
    {
      pb.Minimum = 0;
      pb.Maximum = 0;

      if (pnlProgress.Visible)
        pnlProgress.Visible = false;
    }

    private void lv_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      btnRecover.Enabled = lv.CheckedItems.Count > 0;
    }

  }
}