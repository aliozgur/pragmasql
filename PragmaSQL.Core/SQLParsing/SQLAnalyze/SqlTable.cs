/********************************************************************
  Class      : SqlTable
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public enum TableType
  {
    Variable,
    Temp
  }
  
  public class SqlTable
  {
    public string TableName = String.Empty;
    public IList<string> FullyQualifiedColumns = new List<string>();
    public IList<string> ColumnNames = new List<string>();
    public IList<string> ColumnDataTypes = new List<string>();

    private TableType _tableType;
    public TableType TableType
    {
      get { return _tableType; }
    }

    public SqlTable(TableType tableType)
    {
      _tableType = tableType;
    }
  }
}
