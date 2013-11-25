using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace PragmaSQL.Core
{
	public class ResultRendererFactory_AllInOnePage:IResultRendererFactory
	{
		private IList<Control> _disposeList = new List<Control>();

		#region IResultRendererFactory Members
		private DataView _activeDataView = null;
		public DataView ActiveDataView 
		{
			get { return _activeDataView; }
		}

		public string FactoryName
		{
			get { return "AllInOnePage"; }
		}

		public string FactoryDescription
		{
			get { return "Single page for all results."; }
		}

		public void DisposeFactory()
		{
			while (_disposeList.Count > 0)
			{
				Control item = _disposeList[0];
				if (item is DBQueryResultViewer)
				{
					(item as DBQueryResultViewer).ActiveGridChanged -= viewer_ActiveGridChanged;
					(item as DBQueryResultViewer).ClearAll();
				}
				item.Dispose();
				item = null;
				_disposeList.RemoveAt(0);
			}
		}
		
		public void RenderResults(IScriptEditor editor, TabControl tabControl, IList<ScriptExecutionResult> ScriptExecutionResults)
		{
			if (ScriptExecutionResults.Count == 1)
			{
				RenderResult(editor, tabControl, ScriptExecutionResults[0]);
			}
			else if (ScriptExecutionResults.Count > 1)
			{
				RenderResults_Internal(editor, tabControl, ScriptExecutionResults);
			}
		}

		#endregion

		private void RenderResult(IScriptEditor editor, TabControl tabControl, ScriptExecutionResult execResult)
		{
			if (execResult == null || execResult.DataSets == null || execResult.DataSets.Count == 0)
				return;

			foreach (DataSet dataSet in execResult.DataSets)
			{
				if (dataSet == null || dataSet.Tables.Count == 0)
					continue;

				DataSetInfo dsInfo = editor.GetDataSetInfo(dataSet);

				string tabCaption = String.Format("{0} result(s) [{1}]", dataSet.Tables.Count, dsInfo.ServerDbInfo);
				TabPage tp = new TabPage(tabCaption);
				tp.ToolTipText = tabCaption;

				tabControl.TabPages.Add(tp);

				DBQueryResultViewer viewer = new DBQueryResultViewer();
				viewer.Parent = tp;
				viewer.Dock = DockStyle.Fill;
				viewer.ActiveGridChanged += new ActiveGridChangedDelegate(viewer_ActiveGridChanged);
				viewer.RenderDataSet(editor, dataSet);

				_disposeList.Add(viewer);
			}
		}

		void viewer_ActiveGridChanged(object sender, PragmaDataGridView grd)
		{
			if (grd == null)
			{
				_activeDataView = null;
				return;
			}

			_activeDataView = grd.DataView;
		}


		public void RenderResults_Internal(IScriptEditor editor, TabControl tabControl, IList<ScriptExecutionResult> execResults)
		{
			if (execResults == null || execResults.Count == 0)
				return;

			TabPage mainTp = null;
			TabControl childTab = null;

			foreach (ScriptExecutionResult eResult in execResults)
			{
				if (eResult == null || eResult.DataSets == null || eResult.DataSets.Count == 0)
					continue;

				string mainTabCaption = String.Format("Results for [{0}]", eResult.ConnParams.InfoDbServer);
				mainTp = new TabPage(mainTabCaption);
				mainTp.ToolTipText = mainTabCaption;
				tabControl.TabPages.Add(mainTp);
				
				childTab = new TabControl();
				childTab.Parent = mainTp;
				childTab.Dock = DockStyle.Fill;
				mainTp.Controls.Add(childTab);
				_disposeList.Add(childTab);

				RenderResult(editor, childTab, eResult);
			}
		}
	}
}
