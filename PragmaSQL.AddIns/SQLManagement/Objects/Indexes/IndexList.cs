using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public partial class IndexList : UserControl
  {
    private DataTable _tblIndexes = null;

    private ConnectionParams _cp;
    public ConnectionParams ConnectionParams
    {
      get { return _cp; }
      set
      {
        if (value != null)
        {
          _cp = value.CreateCopy();
          if (!_showAllIndexes)
            PopulateIndexes();
          else
            PopulateAllIndexes();
        }
        else
        {
          _cp = null;
          bsIndexes.DataSource = null;
          _tblIndexes = null;
        }
      }
    }

    public bool ObjectNameVisible
    {
      get { return colObjectName.Visible; }
      set { colObjectName.Visible = value; }
    }

    private string _objectName;
    public string ObjectName
    {
      get { return _objectName; }
      set { _objectName = value; }
    }

    private long _objectId = -1;
    public long ObjectId
    {
      get { return _objectId; }
      set { _objectId = value; }
    }

    private string _objectOwner = String.Empty;
    public string ObjectOwner
    {
      get { return _objectOwner; }
      set { _objectOwner = value; }
    }

    public IndexList( )
    {
      InitializeComponent();
    }

    private bool _showAllIndexes = false;
    public bool ShowAllIndexes
    {
      get { return _showAllIndexes; }
      set { _showAllIndexes = value; }
    }

    private void PopulateIndexes( )
    {
      bsIndexes.DataSource = null;
      _tblIndexes = null;
      _tblIndexes = DbCmd.GetIndexes(_cp, _objectId);
      bsIndexes.DataSource = _tblIndexes;
    }

    private void PopulateAllIndexes( )
    {
      bsIndexes.DataSource = null;
      _tblIndexes = null;
      _tblIndexes = DbCmd.GetAllIndexes(_cp);
      bsIndexes.DataSource = _tblIndexes;
    }

    public void RefreshIndexes( )
    {
      if (!_showAllIndexes)
        PopulateIndexes();
      else
        PopulateAllIndexes();
    }

    public void ModifySelectedIndexes( )
    {
      if (grd.SelectedRows.Count == 0)
        return;

      foreach (DataGridViewRow row in grd.SelectedRows)
      {

        if (!Utils.IsDbValueValid(row.Cells[0])
          || !Utils.IsDbValueValid(row.Cells[1])
          || !Utils.IsDbValueValid(row.Cells[2])
          || !Utils.IsDbValueValid(row.Cells[3])
          || !Utils.IsDbValueValid(row.Cells[4]))
          continue;

        frmIndexEdit.ShowIndexEditor(_cp, EditMode.Modify,
          (string)row.Cells[2].Value
          , (short)row.Cells[1].Value
          , (string)row.Cells[4].Value
          , (int)row.Cells[0].Value
          , (string)row.Cells[3].Value);
      }

    }

    public void ShowDBCC( )
    {
      if (grd.SelectedRows.Count == 0)
        return;

      foreach (DataGridViewRow row in grd.SelectedRows)
      {

        if (!Utils.IsDbValueValid(row.Cells[0])
          || !Utils.IsDbValueValid(row.Cells[1])
          || !Utils.IsDbValueValid(row.Cells[2])
          || !Utils.IsDbValueValid(row.Cells[3])
          || !Utils.IsDbValueValid(row.Cells[4]))
          continue;

        frmIndexDBCC.ShowIndexDBCC(_cp, 
          (string)row.Cells[2].Value, 
          (int)row.Cells[0].Value, 
          (string)row.Cells[3].Value, 
          (short)row.Cells[1].Value, 
          (string)row.Cells[4].Value);
      }
    }

    public bool DropSelectedIndexes(bool confirm )
    {
      if (grd.SelectedRows.Count == 0)
        return false;

      if (confirm && !MessageService.AskQuestion("Are you sure you want to drop selected indexes?"))
        return false;
      IndexWrapper idx = null;
      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (!Utils.IsDbValueValid(row.Cells[0])
          || !Utils.IsDbValueValid(row.Cells[1])
          || !Utils.IsDbValueValid(row.Cells[2])
          || !Utils.IsDbValueValid(row.Cells[3])
          || !Utils.IsDbValueValid(row.Cells[4]))
          continue;


        idx = new IndexWrapper(_cp);
        idx.ID = (short)row.Cells[1].Value;
        idx.OwnerObjectId = (int)row.Cells[0].Value;
        idx.OwnerObjectName = (string)row.Cells[3].Value;
        idx.Owner = (string)row.Cells[2].Value;
        idx.Name = (string)row.Cells[4].Value;
        idx.Drop();
      }

      RefreshIndexes();
      return true;
    }

    private void grd_DoubleClick( object sender, EventArgs e )
    {
      ModifySelectedIndexes();
    }


  }
}
