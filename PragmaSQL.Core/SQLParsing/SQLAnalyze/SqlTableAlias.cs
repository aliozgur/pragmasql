/********************************************************************
  Class      : SqlTableAlias
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public enum TableAliasType
  {
    From,
    Join
  }
  
  public class SqlTableAlias
  {
    public string TableName = String.Empty;
    public string TableAlias = String.Empty;

    private TableAliasType _aliasType;
    public TableAliasType AliasType
    {
      get { return _aliasType; }
    }

    public SqlTableAlias(TableAliasType aliasType)
    {
      _aliasType = aliasType;      
    }
  }
}
