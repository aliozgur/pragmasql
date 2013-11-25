/********************************************************************
  Class Enums
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SQLManagement
{
  /// <summary>
  /// Server Locks type
  /// </summary>
  public enum LockType
  {
    /// <summary>
    /// None LockType
    /// </summary>
    None = 1,
    /// <summary>
    /// Database LockType
    /// </summary>
    Database = 2,
    /// <summary>
    /// File LockType
    /// </summary>
    File = 3,
    /// <summary>
    /// Index LockType
    /// </summary>
    Index = 4,
    /// <summary>
    /// Table LockType
    /// </summary>
    Table = 5,
    /// <summary>
    /// Page LockType
    /// </summary>
    Page = 6,
    /// <summary>
    /// Key LockType
    /// </summary>
    Key = 7,
    /// <summary>
    /// Extent LockType
    /// </summary>
    Extent = 8,
    /// <summary>
    /// RID LockType
    /// </summary>
    RID = 9,
    /// <summary>
    /// Application LockType
    /// </summary>
    Application = 10
  }

  public enum ObjectType
  {
    None,
    Table,
    View,
    Procedure,
    TableValueFunction,
    ScalarValuedFunction,
    InlinedFunction
  }

  public enum RoleListAction
  {
    UncheckAll,
    CheckAll,
    Toggle
  }

  public enum EditMode
  {
    New,
    Modify
  }

  [Flags]
  public enum DataTypeProperties
  {
    None = 0x01
    ,Width = 0x02
    ,Scale = 0x04
    ,Precision = 0x08
    ,All = 0x16
  }

  public enum ColumnOperation
  {
    Add,
    Drop,
    Modify
  }
}
