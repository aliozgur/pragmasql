/********************************************************************
  Class      : ObjectRefList
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
using System.Data.SqlClient;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public enum RefDetail
  {
    Any,
    Objects
  }

  public partial class ObjectRefList : UserControl
  {
    private BindingSource _bs = new BindingSource();
    private SqlDataAdapter _adapter = new SqlDataAdapter();
    private DataTable _tbl = new DataTable("ObjReferences");

    private ConnectionParams _connParams = null;

    private string _objName = String.Empty;
    private RefDetail _refDetail = RefDetail.Any;
    private string _dbName = String.Empty;

    public DataGridView Grid
    {
      get
      {
        return grd;
      }
    }
    public ObjectRefList( )
    {
      InitializeComponent();
    }

    public void Initialize( string objName, ConnectionParams cp, string dbName, RefDetail refDetail )
    {
      if (cp == null)
      {
        throw new NullParameterException("ConnectionParams is null!");
      }

      _objName = objName;
      _dbName = dbName;
      _refDetail = refDetail;
      _connParams = cp.CreateCopy();
    }

    public void LoadData( )
    {
     
      _bs.DataSource = null;
      _tbl.Clear();

      string script = String.Empty;
      switch (_refDetail)
      {
        case RefDetail.Any:
          script = ResManager.GetDBScript("Script_ReferencesAny");
          break;
        case RefDetail.Objects:
          script = ResManager.GetDBScript("Script_ReferencesAny");
          break;
        default:
          throw new Exception("RefDetail value specified is not supported!");
      }

      using (SqlConnection conn = _connParams.CreateSqlConnection(true, false))
      {
        script = String.Format(script, _objName);
        SqlCommand cmd = new SqlCommand(script, conn);
        cmd.CommandTimeout = 0;
        _adapter.SelectCommand = cmd;
        _adapter.Fill(_tbl);
      }

      RefreshBindings();
    }

    private void RefreshBindings( )
    {
      _bs.DataSource = _tbl;

      bindingNavigator1.BindingSource = _bs;
      grd.DataSource = _bs;


      DataGridViewColumn col = grd.Columns[0];
      col.HeaderText = "Name";

      col = grd.Columns[1];
      col.HeaderText = "Type";

      col.DisplayIndex = 0;
      grd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

    }

    private void grd_CellContentClick( object sender, DataGridViewCellEventArgs e )
    {

    }

    private void toolStripButton1_Click( object sender, EventArgs e )
    {
      btnObjRefs.Checked = !btnObjRefs.Checked;
      if (btnObjRefs.Checked)
      {
        _refDetail = RefDetail.Objects;
      }
      else
      {
        _refDetail = RefDetail.Objects;
      }
      LoadData();
    }

    private void toolStripButton1_Click_1( object sender, EventArgs e )
    {
      LoadData();
    }

    private void grd_CellPainting( object sender, DataGridViewCellPaintingEventArgs e )
    {
      if (e.Value != null && e.Value.GetType() != typeof(DBNull) && e.ColumnIndex == 1)
      {
        Color gridBrushColor = ((DataGridView)sender).GridColor;
        Color bgColor = Color.FromKnownColor(KnownColor.Window); //Color.WhiteSmoke;

        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
        {
          bgColor = SystemColors.Highlight;
        }

        Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
            e.CellBounds.Y + 1, e.CellBounds.Width - 4,
            e.CellBounds.Height - 4);


        using (
            Brush gridBrush = new SolidBrush(gridBrushColor),
            backColorBrush = new SolidBrush(bgColor))
        {


          using (Pen gridLinePen = new Pen(gridBrush))
          {
            // Erase the cell.
            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            // Draw the grid lines (only the right and bottom lines;
            // DataGridView takes care of the others).
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                e.CellBounds.Bottom - 1);
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                e.CellBounds.Top, e.CellBounds.Right - 1,
                e.CellBounds.Bottom);


            // Draw the text content of the cell, ignoring alignment.
            Brush br = null;
            if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected)
            {
              br = Brushes.Black;
            }
            else
            {
              br = SystemBrushes.HighlightText;
            }

            Image img = null;
            switch ((e.Value as string).ToLowerInvariant().Trim())
            {
              case "s":
                img = imageList1.Images[2];
                break;
              case "u":
                img = imageList1.Images[2];
                break;
              case "v":
                img = imageList1.Images[3];
                break;
              case "x":
                img = imageList1.Images[4];
                break;
              case "rf":
                img = imageList1.Images[4];
                break;
              case "p":
                img = imageList1.Images[4];
                break;
              case "if":
                img = imageList1.Images[5];
                break;
              case "fn":
                img = imageList1.Images[5];
                break;
              case "tf":
                img = imageList1.Images[5];
                break;
              case "tr":
                img = imageList1.Images[6];
                break;
              default:
                img = null;
                break;
            }

            if (img != null)
            {
              e.Graphics.DrawImageUnscaled(img, e.CellBounds.X + (e.CellBounds.Width - 16) / 2, e.CellBounds.Y + (e.CellBounds.Height - 16) / 2);
              e.Handled = true;
            }
          }

        }
      }
    }

    private void ModifySelectedObjects( )
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;
      IList<frmScriptEditor> editors = new List<frmScriptEditor>();

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellName = row.Cells[0];
        DataGridViewCell cellType = row.Cells[1];
        DataGridViewCell cellObjid = row.Cells[2];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;

        if (DBConstants.DoesObjectTypeHasScript(objType))
        {
          int type = DBConstants.GetDBObjectType(objType);
          string script = String.Empty;
          using (SqlConnection conn = _connParams.CreateSqlConnection(true, false))
          {
            script = ScriptingHelper.GetAlterScript(conn, objId, type);
          }
          frmScriptEditor editor = ScriptEditorFactory.Create(objName, script, objId, type, _connParams, _dbName);
          editors.Add(editor);
        }
      }

      foreach (frmScriptEditor editor in editors)
      {
        ScriptEditorFactory.ShowScriptEditor(editor);
      }
    }

    private void OpenSelectedObjects( )
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;
      IList<frmDataViewer> viewers = new List<frmDataViewer>();

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellName = row.Cells[0];
        DataGridViewCell cellType = row.Cells[1];
        DataGridViewCell cellObjid = row.Cells[2];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;

        if (DBConstants.DoesObjectTypeHoldsData(objType))
        {
          int type = DBConstants.GetDBObjectType(objType);
          string caption = objName + "{" + _dbName + " on " + _connParams.Server + "}";
          string script = " select * from [" + objName + "]";
          bool isReadOnly = (type == DBObjectType.View) ? true : false;

          frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, _dbName, objName, caption, script, isReadOnly, false);
          viewers.Add(viewer);
        }
      }

      foreach (frmDataViewer viewer in viewers)
      {
        viewer.LoadData(true);
        DataViewerFactory.ShowDataViewer(viewer);
      }
    }

    private void RenderContextMenu( )
    {
      mnuItemOpen.Visible = false;
      mnuItemModify.Visible = false;

      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        DataGridViewCell cellName = row.Cells[0];
        DataGridViewCell cellType = row.Cells[1];
        DataGridViewCell cellObjid = row.Cells[2];

        if (cellName.ValueType != typeof(string) || cellName.Value == null)
        {
          continue;
        }

        if (cellType.ValueType != typeof(string) || cellType.Value == null)
        {
          continue;
        }

        if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
        {
          continue;
        }

        objId = (int)cellObjid.Value;
        objType = (string)cellType.Value;
        objName = (string)cellName.Value;

        if (!mnuItemOpen.Visible && DBConstants.DoesObjectTypeHoldsData(objType))
        {
          mnuItemOpen.Visible = true;
        }

        if (!mnuItemModify.Visible && DBConstants.DoesObjectTypeHasScript(objType))
        {
          mnuItemModify.Visible = true;

        }
      }
    }

    private void PerformActionOnFirstSelectedRow( )
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      int objId = -1;
      string objType = String.Empty;
      string objName = String.Empty;


      DataGridViewRow row = grd.SelectedRows[0];

      DataGridViewCell cellName = row.Cells[0];
      DataGridViewCell cellType = row.Cells[1];
      DataGridViewCell cellObjid = row.Cells[2];

      if (cellName.ValueType != typeof(string) || cellName.Value == null)
      {
        return;
      }

      if (cellType.ValueType != typeof(string) || cellType.Value == null)
      {
        return;
      }

      if (cellObjid.ValueType != typeof(int) || cellObjid.Value == null)
      {
        return;
      }

      objId = (int)cellObjid.Value;
      objType = (string)cellType.Value;
      objName = (string)cellName.Value;

      if (DBConstants.DoesObjectTypeHasScript(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string script = String.Empty;
        using (SqlConnection conn = _connParams.CreateSqlConnection(true, false))
        {
          script = ScriptingHelper.GetAlterScript(conn, objId, type);
        }
        frmScriptEditor editor = ScriptEditorFactory.Create(objName, script, objId, type, _connParams, _dbName);
        ScriptEditorFactory.ShowScriptEditor(editor);
      }
      else if (DBConstants.DoesObjectTypeHoldsData(objType))
      {
        int type = DBConstants.GetDBObjectType(objType);
        string caption = objName + "{" + _dbName + " on " + _connParams.Server + "}";
        string script = " select * from [" + objName + "]";
        bool isReadOnly = (type == DBObjectType.View) ? true : false;

        frmDataViewer viewer = DataViewerFactory.CreateDataViewer(_connParams, _dbName, objName, caption, script, isReadOnly, true);
        DataViewerFactory.ShowDataViewer(viewer);
      }

    }

    
    

    private void mnuItemOpen_Click( object sender, EventArgs e )
    {
      OpenSelectedObjects();
    }

    private void contextMenuStrip1_Opening( object sender, CancelEventArgs e )
    {
      RenderContextMenu();
    }

    private void mnuItemModify_Click( object sender, EventArgs e )
    {
      ModifySelectedObjects();
    }

    private void grd_DoubleClick( object sender, EventArgs e )
    {
      PerformActionOnFirstSelectedRow();
    }

    private void exportToFileToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DataExport.ExportGridToFile(_tbl);
    }

    private void copyToolStripMenuItem_Click( object sender, EventArgs e )
    {
      DataObject dtObj = grd.GetClipboardContent();
      if(dtObj == null)
      {
        return;
      }


      Clipboard.SetDataObject(dtObj);
    }

    private void grd_KeyDown( object sender, KeyEventArgs e )
    {
      if( e.KeyCode == Keys.Return)
      {
        PerformActionOnFirstSelectedRow();
      }
    }
  }
}
