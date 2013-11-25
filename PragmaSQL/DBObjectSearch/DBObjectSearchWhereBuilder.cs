/********************************************************************
  Class      : DBObjectSearchWhereBuilder
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
  public partial class DBObjectSearchWhereBuilder : UserControl
  {
    private BindingSource _bs = new BindingSource();
    private DataTable _tbl = null;

    public DBObjectSearchWhereBuilder( )
    {
      InitializeComponent();
      InitializeDataTable();
    }
    
    

    private void InitializeDataTable()
    {
      _tbl = new DataTable();
      DataColumn column;

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "Containment";
      _tbl.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "Name";
      _tbl.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "ObjName";
      _tbl.Columns.Add(column);

      column = new DataColumn();
      column.DataType = System.Type.GetType("System.String");
      column.ColumnName = "Operator";
      _tbl.Columns.Add(column);
      /*
      DataRow row = _tbl.NewRow();
      _tbl.Rows.Add(row);
      */

      _bs.DataSource = _tbl;
      grd.DataSource = _bs;


    }

    public void AddCriteria()
    {
      _bs.AddNew();
    }

    public void AddCriteria(string searchText, string objName)
    {
      DataRow nr = _tbl.NewRow();
      nr["ObjName"] = objName;
      nr["Name"] = searchText;
      _tbl.Rows.Add(nr);
    }

    public void ClearCriterias()
    {
      _tbl.Rows.Clear();
    }
    
    public void RemoveSelectedCriteria()
    {
      if (grd.SelectedRows.Count == 0)
      {
        return;
      }

      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (row.IsNewRow)
        {
          continue;
        }
        grd.Rows.Remove(row);
      }
    }

    public void EndEdit()
    {
      grd.EndEdit();
      _bs.EndEdit();
    }

    public string BuildSearchScript()
    {
      string result = String.Empty;
      string typePart = BuildObjectTypePart();
      if(String.IsNullOrEmpty( typePart ))
      {
        MessageBox.Show("Object types not selected!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        return result;
      }

      string criteriaPart =BuildCriteriaPart();
      if(String.IsNullOrEmpty( criteriaPart ))
      {
        MessageBox.Show("No criteria specified!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        return result;
      }

      result = ResManager.GetDBScript("Script_SearchDbExtended");
      result = result.Replace("$typeCriteria$",typePart);
      result = result.Replace("$criteria$",criteriaPart);

      return result;
    }

    public string BuildObjectTypePart()
    {
      IList<string> parts = new List<string>();
      string part = String.Empty;
      bool isValid = false;

      if(chkSp.Checked)
      {
        part = " ( OBJECTPROPERTY(id, 'IsProcedure') = 1 ) ";
        parts.Add(part);
        isValid  = true;
      }

      if(chkFn.Checked)
      {
        part = " ( ( OBJECTPROPERTY(id, 'IsScalarFunction') = 1 ) OR ( OBJECTPROPERTY(id, 'IsInlineFunction') = 1) OR ( OBJECTPROPERTY(id, 'IsTableFunction') = 1 ) )";
        parts.Add(part);
        isValid  = true;
      }

      if(chkTbl.Checked)
      {
        part = " ( OBJECTPROPERTY(id, 'IsTable') = 1 )";
        parts.Add(part);
        isValid  = true;
      }

      if(chkView.Checked)
      {
        part = " ( OBJECTPROPERTY(id, 'IsView') = 1 )  ";
        parts.Add(part);
        isValid  = true;
      }
      
      if(chkTrigger.Checked)
      {
        part = " ( OBJECTPROPERTY(id, 'IsTrigger') = 1 ) ";
        parts.Add(part);
        isValid  = true;
      }

      if(chkSystemObjects.Checked)
      {
        isValid = true;
      }

      string result = String.Empty;
      if(!isValid)
      {
        return result;
      }

      if(chkSystemObjects.Checked)
      {
       result = "( OBJECTPROPERTY(id, 'IsMSShipped') = 0 ) " 
         + "\n" 
         + " AND "
         + " ( "
         + "\n";
      }

      for(int i= 0; i < parts.Count; i++)
      {
       if( i== 0 )
       {
        result +=  parts[i];
       }
       else
       {
         result += "\n" + " OR " + parts[i];
       }
      }

      if(chkSystemObjects.Checked)
      {
        result += "\n" + " ) ";
      }
      return result;
    }

    public string BuildCriteriaPart()
    {
      IList<string> parts = new List<string>();
      IList<string> ops = new List<string>();

      string part = String.Empty;
      string op = String.Empty;
      short emptyCnt = 0;
      string objName = String.Empty;
      string name = String.Empty;

      string matchCr = String.Empty;
      string matchCrNot = String.Empty;

      if (chkLike.Checked)
      {
        matchCr = " ( ( ObjectText LIKE '%{0}%' ) {2} ( ColText LIKE '%{0}%' ) {2} ( ObjectName LIKE '%{1}%' ) ) ";
        matchCrNot = " ( ( ObjectText NOT LIKE '%{0}%' ) {2} ( ColText NOT LIKE '%{0}%' ) {2} ( ObjectName NOT LIKE '%{1}%' ) ) ";
      }
      else
      {
        matchCr = " ( ( ObjectText LIKE '{0}' ) {2} ( ColText LIKE '{0}' ) {2} ( ObjectName LIKE '{1}' ) ) ";
        matchCrNot = " ( ( ObjectText NOT LIKE '{0}' ) {2} ( ColText NOT LIKE '{0}' ) {2} ( ObjectName NOT LIKE '{1}' ) ) ";
      }

      foreach (DataRow row in _tbl.Rows)
      {
        emptyCnt = 0;
        string crContainment = String.Empty;
        if( row["Containment"].GetType() == typeof(DBNull) || row["Containment"] == null)
        {
          crContainment = "Contains";  
        }
        else
        {
          crContainment = (string)row["Containment"];
        }
        
        if( row["Containment"].GetType() == typeof(DBNull) || String.IsNullOrEmpty((string)row["Containment"]))
        {
          emptyCnt++;
        }

        objName = (row["ObjName"].GetType() != typeof(DBNull) && row["ObjName"] != null) ? (string)row["ObjName"] : String.Empty;
        name = (row["Name"].GetType() != typeof(DBNull) && row["Name"] != null) ? (string)row["Name"] : String.Empty;

        if (!String.IsNullOrEmpty(objName))
        {
          if(crContainment == "Contains")
          {
            part = String.Format(matchCr,"{0}","{1}", "AND");
          }
          else if (crContainment == "Not Contains")
          {
            part = String.Format(matchCrNot, "{0}", "{1}", "AND");
          }
        }
        else
        {
          if(crContainment == "Contains")
          {
            part = String.Format(matchCr, "{0}", "{0}", "OR");
          }
          else if (crContainment == "Not Contains")
          {
            part = String.Format(matchCrNot, "{0}", "{0}", "AND");
          }
        }


        if (!String.IsNullOrEmpty(objName))
        {
          part = String.Format(part, name, objName);
        }
        else
        {
          part = String.Format(part, name);
        }

        bool noCriteria = false;
        if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(objName) )
        {
          emptyCnt++;
          noCriteria = true;
        }

        // Skip this row if key parts are empty and has another criteria already
        if(( parts.Count > 0 && emptyCnt == 2 ) || noCriteria)
        {
          continue;
        }
        
        parts.Add(part);

        if(row["Operator"].GetType() == typeof(DBNull) || row["Operator"] == null)
        {
          op = "AND";
        }
        else
        {
          op = (string)row["Operator"];        
        }

        ops.Add(op);
      }

      string result = String.Empty;
      if(parts.Count == 0)
      {
        return result;
      }

      for(int i=0; i < parts.Count;i++)
      {
        if(i== 0)
        {
          result += parts[i] ; 
        }
        else
        {
          result += "\n" + ops[i-1] + parts[i] ;
        }
      }
      return result;
    }


  }
}
