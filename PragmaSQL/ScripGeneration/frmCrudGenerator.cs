/********************************************************************
  Class      : frmCrudGenerator
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public partial class frmCrudGenerator : KryptonForm
  {
    #region Fields And Properties
    private DataTable _tbl = null;
    private BindingSource _bs = new BindingSource();

    private string _initialTableName = String.Empty;
    private string _initialFullTableName = String.Empty;
    private string _initialDatabaseName = String.Empty;
    
    private ConnectionParams _cp = null;
    public ConnectionParams ConnParams
    {
      get { return _cp; }
    }
    
    private string _script = String.Empty;
    public string Script
    {
      get { return _script; }
    }


    #endregion Fields And Properties

    #region CTOR
    
    public frmCrudGenerator( )
    {
      InitializeComponent();
    }

    #endregion //CTOR

    #region Methods
    private void InitializeDataTable( )
    {
      _tbl = new DataTable();
      _tbl.TableName = "GenerationSpec";
      DataColumn col = null;


      col = new DataColumn();
      col.ColumnName = "Generate";
      col.DataType = System.Type.GetType("System.Boolean");
      _tbl.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "Prefix";
      col.DataType = System.Type.GetType("System.String");
      _tbl.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "Group";
      col.DataType = System.Type.GetType("System.String");
      _tbl.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "TableName";
      col.DataType = System.Type.GetType("System.String");
      _tbl.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "Operation";
      col.DataType = System.Type.GetType("System.String");
      _tbl.Columns.Add(col);

      col = new DataColumn();
      col.ColumnName = "Type";
      col.DataType = System.Type.GetType("System.String");
      _tbl.Columns.Add(col);

      DataRow row;
      row = _tbl.NewRow();
      row["Generate"] = true;
      row["Prefix"] = "sp";
      row["Group"] = _initialDatabaseName;
      row["TableName"] = "_" + _initialTableName;
      row["Operation"] = "_List";
      row["Type"] = "list";
      _tbl.Rows.Add(row);

      row = _tbl.NewRow();
      row["Generate"] = true;
      row["Prefix"] = "sp";
      row["Group"] = _initialDatabaseName;
      row["TableName"] = "_" + _initialTableName;
      row["Operation"] = "_Get";
      row["Type"] = "get";
      _tbl.Rows.Add(row);

      row = _tbl.NewRow();
      row["Generate"] = true;
      row["Prefix"] = "sp";
      row["Group"] = _initialDatabaseName;
      row["TableName"] = "_" + _initialTableName;
      row["Operation"] = "_Insert";
      row["Type"] = "insert";
      _tbl.Rows.Add(row);

      row = _tbl.NewRow();
      row["Generate"] = true;
      row["Prefix"] = "sp";
      row["Group"] = _initialDatabaseName;
      row["TableName"] = "_" + _initialTableName;
      row["Operation"] = "_Update";
      row["Type"] = "update";
      _tbl.Rows.Add(row);

      row = _tbl.NewRow();
      row["Generate"] = true;
      row["Prefix"] = "sp";
      row["Group"] = _initialDatabaseName;
      row["TableName"] = "_" + _initialTableName;
      row["Operation"] = "_Delete";
      row["Type"] = "delete";
      _tbl.Rows.Add(row);

      _bs.DataSource = _tbl;
      grd.DataSource = _bs;
    }

    public void InitializeGenarator( ConnectionParams cp, string tableName, string tableFullName )
    {
      _cp = cp.CreateCopy();
      _initialTableName = tableName;
      _initialFullTableName = tableFullName;
      _initialDatabaseName = cp.Database;
      InitializeDataTable();

      txtPrefix.Text = "sp";
      txtGroup.Text = cp.Database;
      txtTableName.Text = "_" + tableName;
    }

    private void ApplyToAll( )
    {
      foreach (DataRow row in _tbl.Rows)
      {
        row["Prefix"] = txtPrefix.Text;
        row["Group"] = txtGroup.Text;
        row["TableName"] = txtTableName.Text;
      }
    }

    private void GenerateCrud()
    {
      IList<TableColumnSpec> columns = ProgrammabilityHelper.GetTableColumnsSpecification(_cp,_initialTableName);
      bool canGenerate = false;
      string prefix = String.Empty;
      string group = String.Empty;
      string tableNameAbb = String.Empty;
      string operation = String.Empty;
      string type = String.Empty;
      
      _script = String.Empty;

      foreach (DataRow row in _tbl.Rows)
      {
        canGenerate = (bool)row["Generate"];
        if(!canGenerate)
        {
          continue;
        }

        prefix = (string)row["Prefix"];
        group = (string)row["Group"];
        tableNameAbb = (string)row["TableName"];
        operation = (string)row["Operation"];
        type = (string)row["Type"];
      
        if(type == "list")
        {
          _script += ProgrammabilityHelper.GenerateCrudProc_List(columns,_cp.CurrentUsername,prefix,group,tableNameAbb, operation,_initialFullTableName);
        }
        else if(type == "get")
        {
          _script += ProgrammabilityHelper.GenerateCrudProc_Get(columns, _cp.CurrentUsername, prefix, group, tableNameAbb, operation, _initialFullTableName);
        }
        else if(type == "insert")
        {
          _script += ProgrammabilityHelper.GenerateCrudProc_Insert(columns, _cp.CurrentUsername, prefix, group, tableNameAbb, operation, _initialFullTableName);
        }
        else if(type == "update")
        {
          _script += ProgrammabilityHelper.GenerateCrudProc_Update(columns, _cp.CurrentUsername, prefix, group, tableNameAbb, operation, _initialFullTableName);
        }
        else if(type == "delete")
        {
          _script += ProgrammabilityHelper.GenerateCrudProc_Delete(columns, _cp.CurrentUsername, prefix, group, tableNameAbb, operation, _initialFullTableName);
        }
      }
      DialogResult = DialogResult.OK;
    }

    #endregion //Methods

    #region Static Methods
    public static DialogResult ShowGenerateCrudDialog(ConnectionParams cp, string tableName,string tableFullName, out string script)
    {
      DialogResult result = DialogResult.Cancel;
      script = String.Empty;

      frmCrudGenerator frm = new frmCrudGenerator();
      frm.InitializeGenarator(cp,tableName,tableFullName);
      result = frm.ShowDialog();
      script = frm.Script;

      return result;
    }

    #endregion

    private void button1_Click( object sender, EventArgs e )
    {
      ApplyToAll();
    }

    private void btnGenerate_Click( object sender, EventArgs e )
    {
      GenerateCrud();
    }
  }
}