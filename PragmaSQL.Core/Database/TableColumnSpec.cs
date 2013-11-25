/********************************************************************
  Class      : TableColumnSpec
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class TableColumnSpec
  {
    public int TableId = -1;
    public string TableName = String.Empty;
    public string Name = String.Empty;
    public string Collation = String.Empty;
    
    public bool IsNullable = false;
    public bool IsComputed = false;
    public bool IsIdentity = false;
    public bool IsInPrimaryKey = false;

    public decimal IdentitySeed = 0;
    public decimal IdentityIncrement = 0;

    public string FullyQualifiedDataType = String.Empty;
    public string DataType = String.Empty;
    public int Length = 0;
    public int Scale = 0;
    public short Precision = 0;

  }
}
