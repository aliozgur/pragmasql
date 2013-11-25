using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Database;

namespace PragmaSQL.GUI
{
  public partial class frmConnectionRepository : Form
  {
    private ConnectionParamsCollection _connParams = null;

    public frmConnectionRepository()
    {
      InitializeComponent();
    }

    public ConnectionParams SelectedDataSource
    {
      get
      { 
        if(lvConnections.SelectedItems.Count == 0)
        {
          return null;
        }
        return _connParams.FindByID((Guid)lvConnections.SelectedItems[0].Tag);
      }
    }

    private void AddConnectionSpecToList(ConnectionParams connSpec)
    {
      ListViewItem item;
      if (connSpec.FriendlyName == null || connSpec.FriendlyName.Length == 0)
        item = new ListViewItem(connSpec.Name);
      else
        item = new ListViewItem(connSpec.Name + "  { " + connSpec.FriendlyName + " } ");

      item.SubItems.Add(connSpec.InitialCatalog);
      item.Checked = connSpec.IsConnected;
      item.Tag = connSpec.ID;
      
      lvConnections.SelectedItems.Clear();
      item.Selected = true;

      lvConnections.Items.Add(item);
      _connParams.Add(connSpec);

      InvalidateButtons();
    }
    
    private void PopulateDBConnections()
    {
      _connParams = ConnectionParamsFactory.GetConnections();
      lvConnections.Items.Clear();

      foreach (ConnectionParams connSpec in _connParams)
      {
        ListViewItem item;
        if (connSpec.FriendlyName == null || connSpec.FriendlyName.Length == 0)
          item = new ListViewItem(connSpec.Name);
        else
          item = new ListViewItem(connSpec.Name + "  { " + connSpec.FriendlyName + " } ");

        item.SubItems.Add(connSpec.InitialCatalog);
        item.Checked = connSpec.IsConnected;
        item.Tag = connSpec.ID;
        lvConnections.Items.Add(item);
      }

      if (_connParams.Count > 0)
      {
        lvConnections.Items[0].Selected = true;
      }

      InvalidateButtons();
    }

    private void AddNewConnectionSpec()
    {
      frmConnectionParams frm = new frmConnectionParams();
      frm.TopMost = true;
      if (frm.ShowDialog() == DialogResult.OK)
      {
        AddConnectionSpecToList(frm.GetCurrentConnectionSpec());
        ConnectionParamsFactory.Save(_connParams);
        InvalidateButtons();
      }
    }

    private void EditSelectedConnectionSpec()
    {
      if (lvConnections.SelectedItems.Count == 0)
      {
        return;
      }

      frmConnectionParams frm = new frmConnectionParams(this.SelectedDataSource,true);
      frm.TopMost = true;
      if (frm.ShowDialog() == DialogResult.OK)
      {
        _connParams.Delete(this.SelectedDataSource);
        lvConnections.Items.Remove(lvConnections.SelectedItems[0]);
        AddConnectionSpecToList(frm.GetCurrentConnectionSpec());
        ConnectionParamsFactory.Save(_connParams);
        InvalidateButtons();
      }      
    }

    private void RemoveSelectedConnectionSpec()
    {
      if (lvConnections.SelectedItems.Count == 0)
      {
        return;
      }

      if (lvConnections.SelectedItems[0] == null)
      {
        return;
      }
      DialogResult dlgRes = MessageBox.Show("Delete selected database connection?", "Confirm",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

      if (dlgRes == DialogResult.No)
      {
        return;
      }

      _connParams.Delete(_connParams.FindByID((Guid)lvConnections.SelectedItems[0].Tag));
      lvConnections.Items.Remove(lvConnections.SelectedItems[0]);
      InvalidateButtons();
      return;    
    }

    private void InvalidateButtons()
    {
      btnEdit.Enabled = !( lvConnections.SelectedItems.Count == 0 || lvConnections.Items.Count == 0 );
      btnRemove.Enabled = btnEdit.Enabled;
    }
    
    private void frmConnectionRepository_Load(object sender, EventArgs e)
    {
      PopulateDBConnections();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      RemoveSelectedConnectionSpec();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      EditSelectedConnectionSpec();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
      AddNewConnectionSpec();
    }

    private void lvConnections_SelectedIndexChanged(object sender, EventArgs e)
    {
      InvalidateButtons();
    }

    private void lvConnections_DoubleClick(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }
  }
}