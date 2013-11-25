using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PragmaSQL.Core
{
	public class ScriptExecutionResult
	{
		public ConnectionParams ConnParams = null;
		public IList<DataSet> DataSets = null;
	}
}
