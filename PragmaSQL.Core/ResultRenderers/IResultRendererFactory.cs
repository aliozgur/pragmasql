using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public interface IResultRendererFactory
	{
		string FactoryName { get;}
		string FactoryDescription { get;}
		DataView ActiveDataView { get;}

		void RenderResults(IScriptEditor editor, TabControl tabControl, IList<ScriptExecutionResult> execResults);
		void DisposeFactory();


	}
}
