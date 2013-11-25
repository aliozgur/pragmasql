/********************************************************************
  Class ScriptExecutionResult
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Common;
namespace PragmaSQL
{
  public class ScriptExecutionResult
  {
    public DataSet DataSet = null;
    public IList<SqlMessage> Messages = new List<SqlMessage>();
    public int BatchNo = -1;
    public int QueryNoStartsFrom = -1;
    public int TotalNumOfResults = -1;
  }
}
