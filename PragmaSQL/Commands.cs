/********************************************************************
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;

using PragmaSQL.Core;
namespace PragmaSQL
{
  public class NewScriptCommand:AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.NewScript();
    }

   
  }

  public class NewScriptFromFileCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.NewScriptFromFile();
    }
  }

  public class NewTextEditorCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.NewTextEditor();
    }
  }

  public class NewTextDiffCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.NewTextDiff();
    }
  }

  public class NewWebBrowserCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.NewWebBrowser();
    }
  }

  public class OpenProjectCommand: AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.OpenProject();
    }
  }

  public class ShowObjectExplorerCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowObjectExplorer();
    }
  }

  public class ShowProjectExplorerCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowProjectExplorer();
    }
  }

  public class ShowObjectGroupingFormCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowObjectGropingForm();
    }
  }

  public class ShowSearchDatabaseCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowSearchDatabaseForm();
    }
  }

  public class ShowObjectChangeHistoryViewerCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowObjectChnageHistoryViewer();
    }
  }

  public class ShowSharedSnippetsCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowSharedSnippets();
    }
  }

  public class ShowSharedScriptsCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowSharedScripts();
    }
  }

  public class ShowSavedConnectionsCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowSavedConnections();
    }
  }

  public class ShowOptionsCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.ShowOptionsDialog();
    }
  }

  public class CloseAllWindowsCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmMain.Instance.CloseDocuments(null);
    }
  }

  public class ShowAboutCommand : AbstractMenuCommand
  {

    public override void Run( )
    {
      frmAbout.ShowAbout();
    }
  }
}
