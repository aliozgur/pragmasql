/********************************************************************
  Class      : SqlParserResult
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using com.calitha.goldparser;

namespace PragmaSQL.Core
{
  public class SqlParserResult
  {
    public IList<SqlStatement> Statements = new List<SqlStatement>();
		public IList<SqlShBoundary> Comments = new List<SqlShBoundary>();
		public IList<string> ParseErrors = new List<string>();
    public IList<string> Exceptions = new List<string>();
    public ArrayList StartLocations = new ArrayList();		
		public IList<TerminalToken> Tokens = new List<TerminalToken>();
    public IList<SqlShBoundary> CodeBlocks = new List<SqlShBoundary>();
  

  }
}
