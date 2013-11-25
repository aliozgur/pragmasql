using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using PragmaSQL.Core;

namespace PragmaSQL.DxResultRenderer
{
	public class DxResultRendererFactory:IResultRendererFactory
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
			get { return "DxRenderer"; }
		}

		public string FactoryDescription
		{
			get { return "Renderer using Developer Express grid."; }
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

		public void DisposeFactory()
		{
			while (_disposeList.Count > 0)
			{
				Control item = _disposeList[0];
				item.Dispose();
				item = null;
				_disposeList.RemoveAt(0);
			}
		}
		
		#endregion
		

		private void RenderResult(IScriptEditor editor, TabControl tabControl, ScriptExecutionResult execResult)
		{
			if(execResult == null || execResult.DataSets == null || execResult.DataSets.Count == 0)
				return;

			TabPage tp = null;
			DxResultRenderer grd = null;
			int tblNo = 1;
			foreach (DataSet ds in execResult.DataSets)
			{
				if (ds == null || ds.Tables.Count == 0)
					continue;

				DataSetInfo dsInfo = editor.GetDataSetInfo(ds);
				tblNo = 1;
				foreach (DataTable tbl in ds.Tables)
				{
					string tabCaption = String.Format("Qry {0}.{1} [{2}]", dsInfo.BatchNo,tblNo, tbl.Rows.Count);
					tp = new TabPage(tabCaption);
					tp.ToolTipText = String.Format(tabCaption + " [{0}]", dsInfo.ServerDbInfo);

					tabControl.TabPages.Add(tp);

					grd = new DxResultRenderer();
					tp.Controls.Add(grd);

					grd.Enter += new EventHandler(grd_Enter);

					editor.PrepareAddInSupportForResultContextMenu(grd.PopUpMenu.Items);
					grd.Parent = tp;
					grd.Dock = DockStyle.Fill;
					grd.RenderDataTable(tbl);
					_disposeList.Add(grd);
					tblNo++;
				}
			}
		}

		void grd_Enter(object sender, EventArgs e)
		{
			DxResultRenderer grd = sender as DxResultRenderer;
			if (grd == null)
			{
				_activeDataView = null;
				return;
			}
			_activeDataView = grd.ActiveDataView;
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
