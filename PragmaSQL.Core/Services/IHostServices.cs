/********************************************************************
  Class IHostServices
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public enum HostEditionType
  {
    Professional,
    Personal
  }

  public interface IHostServices
  {
    string HostTitle { get; }
    IEditorService EditorServices { get; }
    IObjectExplorerService ObjectExplorerService { get; }
    IAppMessageService MsgService { get; }
    IProjectExplorerServices ProjectExplorerService { get; }
    IWebBrowserService WebBrowserService { get; }
    ITextDiffService TextDiffService { get; }
    ISharedScriptsViewerService SharedScriptsViewerService { get; }
    ISharedSnippetsViewerService SharedSnippetsViewerService{ get; }
		IConfigSvc ConfigSvc { get; }
		IPersistedDockStateService PersistedDockStateService { get; }
		IResultRendererService ResultRendererService { get; }
    HostEditionType HostEdition { get; }
    /// <summary>
    /// Workbenc. ( Aka main form of the application)
    /// </summary>
    IWorkbench Wb { get; }

    HostOptions HostOptions { get; }
    IList<CodeCompletionList> CodeCompletionLists { get; }
    
    ISharedScriptsService CreateSharedScriptsService( ConnectionParams cp );
    ISharedSnippetsService CreateSharedSnippetsService( ConnectionParams cp );
		
		void SetMainFormAsOwner(Form form);


    void ShowForm( Form frm, AddInDockState dockState );
    void ShowForm( Form frm );
    void CloseForm(Form frm);
    void SelectContent(Form frm);
    void ShowSharedScripts( );
    void ReloadSharedScripts( );
    void ShowSharedSnippets( );
    void ReloadSharedSnippets( );
		
		void PerformWebSearch(string searchText);
		void ShowOptionsDialog();

		void ShowOptionsDialog(string editorName);
		void ShowOptionsDialog(OptionEditorType editorType);

		string EvalMacro(PragmaSqlMacros macro);
		string EvalMacro(string macro);
    
		event EventHandler ActiveContentChanged;
    event EventHandler BeforeServicesInitialized;
    event EventHandler AfterServicesInitialized;

	
  }
}
