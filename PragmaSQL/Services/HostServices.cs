/********************************************************************
  Class HostServices
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using WeifenLuo.WinFormsUI.Docking;

using PragmaSQL.Core;

namespace PragmaSQL
{
	public class HostServices : IHostServices
	{

		public HostServices(string hostTitle)
			: this()
		{
			_hostTitile = hostTitle;
		}

		public HostServices()
		{
		}

		#region IHostServices Members

		private IEditorService _editorServices = null;
		public IEditorService EditorServices
		{
			get { return _editorServices; }
		}

		private IObjectExplorerService _objectExplorerService = null;
		public IObjectExplorerService ObjectExplorerService
		{
			get { return _objectExplorerService; }
		}

		private IAppMessageService _msgService = null;
		public IAppMessageService MsgService
		{
			get { return _msgService; }
		}

		private IProjectExplorerServices _projectExplorerService;
		public IProjectExplorerServices ProjectExplorerService
		{
			get
			{
				return _projectExplorerService;
			}
		}

		private IWebBrowserService _webBrowserService = new WebBrowserService();
		public IWebBrowserService WebBrowserService
		{
			get { return _webBrowserService; }
		}

		private ITextDiffService _textDiffService = new TextDiffService();
		public ITextDiffService TextDiffService
		{
			get
			{
				return _textDiffService;
			}
		}

		private ISharedScriptsViewerService _sharedScriptsViewerService;
		public ISharedScriptsViewerService SharedScriptsViewerService
		{
			get
			{
				return _sharedScriptsViewerService;
			}
		}

		private ISharedSnippetsViewerService _sharedSnippetsViewerService;
		public ISharedSnippetsViewerService SharedSnippetsViewerService
		{
			get
			{
				return _sharedSnippetsViewerService;
			}
		}

		private IWorkbench _workbench;
		public IWorkbench Wb
		{
			get
			{
				return _workbench;
			}
		}

		internal IConfigSvc _configSvc = new ConfigSvc();
		public IConfigSvc ConfigSvc
		{
			get { return _configSvc; }
		}

		internal IResultRendererService _resultRendererService = new ResultRendererService();
		public IResultRendererService ResultRendererService 
		{
			get { return _resultRendererService; } 
		}

		internal IPersistedDockStateService _persistedDockStateService = new PersistedDockStatService();
		private bool _loadedPersistedDockStateCfg = false;
		public IPersistedDockStateService PersistedDockStateService
		{
			get 
			{
				if (!_loadedPersistedDockStateCfg)
					PersistedDockStateCfg.Load();
				return _persistedDockStateService; 
			}
		}

		public HostOptions HostOptions
		{
			get
			{
				HostOptions opts = new HostOptions();
				opts.SysDbConnectionParams = ConfigHelper.Current.PragmaSqlDbConn.CreateCopy();
				opts.SysUtilsDisabled = ConfigHelper.Current.PragmaSql_UtilsDisabled;
				return opts;
			}
		}

    private HostEditionType _hostEdition = HostEditionType.Professional;
    public HostEditionType HostEdition 
    {
      get { return _hostEdition; } 
      internal set {_hostEdition = value;}
    }

		public IList<CodeCompletionList> CodeCompletionLists
		{
			get { return CodeCompletionListLoader.Lists; }
		}

		private string _hostTitile = String.Empty;
		public string HostTitle
		{
			get { return _hostTitile; }
		}

		private bool _servicesInitialized = false;
		public bool ServicesInitialized
		{
			get { return _servicesInitialized; }
			internal set { _servicesInitialized = value; }
		}

		public void ShowForm(Form frm, AddInDockState dockState)
		{
			if (frm != null && Program.MainForm != null && Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
			{
				frm.MdiParent = Program.MainForm;
				frm.WindowState = FormWindowState.Maximized;
				frm.Show();
				return;
			}

			DockContent dockableForm = frm as DockContent;
			if (dockableForm == null || Program.MainForm == null || Program.MainForm.DockPanel == null)
			{
				frm.Show();
				return;
			}

			if (dockableForm.PanelPane != null)
			{
				dockableForm.Show(Program.MainForm.DockPanel);
				dockableForm.PanelPane.Show();
			}
			else
			{
				DockState ds = (DockState)Enum.Parse(typeof(DockState), dockState.ToString());
				dockableForm.Show(Program.MainForm.DockPanel, ds);
			}
			Program.MainForm.DockPanel.Refresh();
		}


		public void ShowForm(Form frm)
		{
			ShowForm(frm, AddInDockState.DockRight);
			/*
			if (frm != null && Program.MainForm != null && Program.MainForm.DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
			{
				frm.MdiParent = Program.MainForm;
				frm.WindowState = FormWindowState.Maximized;
				frm.Show();
				return;
			} 
			
			DockContent dockableForm = frm as DockContent;
			if (dockableForm == null || Program.MainForm == null || Program.MainForm.DockPanel == null)
			{
				frm.Show();
				return;
			}

			if (dockableForm.PanelPane != null)
			{
				dockableForm.Show(Program.MainForm.DockPanel);
				dockableForm.PanelPane.Show();
			}
			else
			{
				dockableForm.Show(Program.MainForm.DockPanel, DockState.DockRight);
			}
			*/
		}

		public void CloseForm(Form frm)
		{
			Program.MainForm.CloseDocuments(frm);
		}

		public void SelectContent(Form frm)
		{
			DockContent dockableForm = frm as DockContent;
			if (dockableForm == null
				|| Program.MainForm == null
				|| Program.MainForm.DockPanel == null
				|| !Program.MainForm.DockPanel.Contents.Contains(dockableForm)
				)
			{
				frm.Show();
				return;
			}

			if (Program.MainForm.DockPanel.Contents.Contains(dockableForm))
			{
				dockableForm.Activate();
			}
		}

		private EventHandler _activeContentChanged;
		public event EventHandler ActiveContentChanged
		{
			add
			{
				_activeContentChanged += value;
			}
			remove
			{
				_activeContentChanged -= value;
			}
		}

		internal void FireActiveDocumentChangedEvent(object sender)
		{
			if (_activeContentChanged == null)
			{
				return;
			}

			Delegate[] delegates = _activeContentChanged.GetInvocationList();
			foreach (EventHandler del in delegates)
			{
				try
				{
					del.Invoke(sender, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
				}
			}
		}

		public ISharedScriptsService CreateSharedScriptsService(ConnectionParams cp)
		{
			if (cp == null)
			{
				throw new NullParameterException("Database connection parameters object is null!");
			}

			return new SharedScriptsService(cp);
		}

		public ISharedSnippetsService CreateSharedSnippetsService(ConnectionParams cp)
		{
			if (cp == null)
			{
				throw new NullParameterException("Database connection parameters object is null!");
			}

			return new SharedSnippetsService(cp, false);
		}

		public void ShowSharedScripts()
		{
			Program.MainForm.ShowSharedScripts();
		}

		public void ReloadSharedScripts()
		{
			if (Program.MainForm.SharedScripts == null)
			{
				return;
			}

			Program.MainForm.SharedScripts.SharedScriptsControl.LoadInitial();
		}

		public void ShowSharedSnippets()
		{
			Program.MainForm.ShowSharedSnippets();
		}

		public void ReloadSharedSnippets()
		{
			if (Program.MainForm.SharedSnippets == null)
			{
				return;
			}

			Program.MainForm.SharedSnippets.SharedSnippetsControl.LoadInitial();
		}


		private EventHandler _beforeServicesInitialized;
		public event EventHandler BeforeServicesInitialized
		{
			add { _beforeServicesInitialized += value; }
			remove { _beforeServicesInitialized -= value; }
		}

		internal void FireBeforeServicesInitializedEvent()
		{
			if (_beforeServicesInitialized == null)
			{
				return;
			}

			Delegate[] delegates = _beforeServicesInitialized.GetInvocationList();
			foreach (EventHandler del in delegates)
			{
				try
				{
					del.Invoke(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
				}
			}
		}

		private EventHandler _afterServicesInitialized;
		public event EventHandler AfterServicesInitialized
		{
			add { _afterServicesInitialized += value; }
			remove { _afterServicesInitialized -= value; }
		}
		internal void FireAfterServicesInitializedEvent()
		{
			if (_afterServicesInitialized == null)
			{
				return;
			}

			Delegate[] delegates = _afterServicesInitialized.GetInvocationList();
			foreach (EventHandler del in delegates)
			{
				try
				{
					del.Invoke(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
				}
			}
		}



		public string EvalMacro(PragmaSqlMacros macro)
		{
			string result = String.Empty;
			ITextEditor txtEditor = HostServicesSingleton.HostServices.EditorServices.CurrentEditor;
			IScriptEditor scriptEditor = HostServicesSingleton.HostServices.EditorServices.CurrentScriptEditor;
			switch (macro)
			{
				case PragmaSqlMacros.Content:
					if (txtEditor != null)
						result = txtEditor.Content;

					break;
				case PragmaSqlMacros.SelectedContent:
					if (txtEditor != null)
						result = txtEditor.SelectedText;
					break;
				case PragmaSqlMacros.FileName:
					if (txtEditor != null)
						result = txtEditor.FileName;
					break;
				case PragmaSqlMacros.ObjectName:
					if (scriptEditor != null)
						result = scriptEditor.ObjectName;
					break;
				case PragmaSqlMacros.ObjectNames:
					if (scriptEditor != null)
					{
						IList<string> names = scriptEditor.ObjectNames;
						string comma = String.Empty;
						foreach (string s in names)
						{
							result += (String.IsNullOrEmpty(comma) ? String.Empty : comma) + s;
							comma = ";";
						}
					}
					break;
				case PragmaSqlMacros.WordAtCursor:
					if (txtEditor != null)
						result = txtEditor.WordAtCursor;
					break;
				case PragmaSqlMacros.Connection:
					if (scriptEditor != null)
					{
						result = scriptEditor.ConnectionString;
					}
					break;
				case PragmaSqlMacros.ServerName:
					if (scriptEditor != null)
					{
						result = scriptEditor.Server;
					}
					break;
				case PragmaSqlMacros.DatabaseName:
					if (scriptEditor != null)
					{
						result = scriptEditor.Database;
					}
					break;
				case PragmaSqlMacros.Username:
					scriptEditor = HostServicesSingleton.HostServices.EditorServices.CurrentScriptEditor;
					if (scriptEditor != null)
					{
						result = scriptEditor.Username;
					}
					break;
				case PragmaSqlMacros.ObjExplorerNode:
					ObjectExplorerNode objExpNode = HostServicesSingleton.HostServices.ObjectExplorerService.SelNode;
					if (objExpNode != null)
						result = objExpNode.Name;
					break;
				case PragmaSqlMacros.StartupPath:
					result = Application.StartupPath;
					break;
				default:
					break;
			}

			return result;
		}

		public string EvalMacro(string macro)
		{
			if (String.IsNullOrEmpty(macro))
				return macro;

			string result = macro;
			Regex r = new Regex("\\$\\((?<Macro>\\w+)\\)", RegexOptions.IgnoreCase);
			Match m = null;
			Group g = null;
			IList<PragmaSqlMacros> skipList = new List<PragmaSqlMacros>();

			//for (m = r.Match(macro); m.Success; m = m.NextMatch())
			m = r.Match(macro);
			while (m.Success)
			{
				g = m.Groups["Macro"];
				if (g == null)
					continue;

				PragmaSqlMacros macroEnum = (PragmaSqlMacros)Enum.Parse(typeof(PragmaSqlMacros), g.Value, true);
				if (skipList.Contains(macroEnum))
					continue;
				skipList.Add(macroEnum);
				result = result.Replace(m.ToString(), EvalMacro(macroEnum));
				m = m.NextMatch();
			}
			return result;
		}

		public void SetMainFormAsOwner(Form form)
		{
			if (form == null)
				return;
			form.Owner = Program.MainForm;
		}

		public void PerformWebSearch(string searchText)
		{
			Program.MainForm.PerformWebSearch(searchText);
		}

		public void ShowOptionsDialog()
		{
			Program.MainForm.ShowOptionsDialog();
		}

		public void ShowOptionsDialog(string editorName)
		{
			Program.MainForm.ShowOptionsDialog(editorName);			
		}

		public void ShowOptionsDialog(OptionEditorType editorType)
		{
			Program.MainForm.ShowOptionsDialog(editorType == OptionEditorType.None ? String.Empty : editorType.ToString());						
		}

		#endregion


		internal void InitializeWorkbench(frmMain workbench)
		{
			_workbench = workbench;
		}

		internal void InitializeMsgServices(ListView appMessageList, EventHandler showMessagesCallback)
		{
			AppMsgService service = new AppMsgService(appMessageList);
			if (showMessagesCallback != null)
			{
				service.ShowMessagesCallback += showMessagesCallback;
			}
			_msgService = service;
		}

		internal void InitializeEditorService()
		{
			_editorServices = new EditorServices();
		}

		internal void InitializeObjectExplorerService(IObjectExplorerService objectExplorerService)
		{
			_objectExplorerService = objectExplorerService;
		}

		internal void InitializeProjectExplorerService(IProjectExplorerServices projectExplorer)
		{
			_projectExplorerService = projectExplorer;
		}

		internal void InitializeSharedScriptsViewerService(ISharedScriptsViewerService sharedScriptsControl)
		{
			_sharedScriptsViewerService = sharedScriptsControl;
		}

		internal void InitializeSharedSnippetsViewerService(ISharedSnippetsViewerService sharedSnippetsControl)
		{
			_sharedSnippetsViewerService = sharedSnippetsControl;
		}

		internal void FireTextEditorClosed(object sender, FormClosedEventArgs e)
		{
			EditorServices es = (EditorServices)_editorServices;
			es.FireEditorClosed(sender, e);
		}

		internal bool FireTextEditorClosing(object sender, FormClosingEventArgs e)
		{
			EditorServices es = (EditorServices)_editorServices;
			return es.FireEditorClosing(sender, e);
		}

		internal void FireTextEditorReadyEvent(object sender)
		{
			EditorServices es = (EditorServices)_editorServices;
			es.FireEditorReadyEvent(sender);
		}

		internal void RegisterDefaultResultRenderers()
		{
			Type t = typeof(ResultRendererFactory_SeperatePageForEach);
			IResultRendererFactory factory  = Activator.CreateInstance(t) as IResultRendererFactory;

			ResultRendererSpec spec = new ResultRendererSpec();
			spec.Name = factory.FactoryName;
			spec.Description = factory.FactoryDescription;
			spec.FullName = String.Format("{0}, {1}",t.FullName,t.Assembly.FullName);
			spec.RendererType = t;

			_resultRendererService.Register(spec);

			if (String.IsNullOrEmpty(Program.MainForm.DefaultResultRenderer))
				Program.MainForm.DefaultResultRenderer = spec.Name;

			t = typeof(ResultRendererFactory_AllInOnePage);
			factory = Activator.CreateInstance(t) as IResultRendererFactory;

			spec = new ResultRendererSpec();
			spec.Name = factory.FactoryName;
			spec.Description = factory.FactoryDescription;
			spec.FullName = String.Format("{0}, {1}", t.FullName, t.Assembly.FullName);
			spec.RendererType = t;
			_resultRendererService.Register(spec);

		}
	}
}
