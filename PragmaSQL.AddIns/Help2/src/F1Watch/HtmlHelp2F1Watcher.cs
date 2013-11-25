/********************************************************************
  Class HtmlHelp2F1Watcher
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using HtmlHelp2.Environment;

namespace HtmlHelp2
{
  public static class HtmlHelp2F1Watcher
  {
    private static IHostServices _hostServices = null;
    public static IHostServices HostServices
    {
      get { return _hostServices; }
    }


    private static IScriptEditor _scriptEditor;

    public static void Initialize( )
    {
      _hostServices = HostServicesSingleton.HostServices;
      if (_hostServices == null)
      {
        return;
      }

      SubscribeToEvents();
    }

    public static void UnInitialize( )
    {
      if (_hostServices != null)
      {
        UnsubscribeFromEvents();
      }
    }

    private static void SubscribeToEvents( )
    {
      if (_hostServices == null)
      {
        throw new Exception("Can not subscribe to events.HostServices is null!");
      }
      _hostServices.AfterServicesInitialized += new EventHandler(_hostServices_AfterServicesInitialized);
      _hostServices.ActiveContentChanged += new EventHandler(_hostServices_ActiveContentChanged);
    }



    private static void UnsubscribeFromEvents( )
    {
      if (_hostServices == null)
      {
        throw new Exception("Can not unsubscribe from events.HostServices is null!");
      }
      _hostServices.AfterServicesInitialized -= new EventHandler(_hostServices_AfterServicesInitialized);
      _hostServices.ActiveContentChanged -= new EventHandler(_hostServices_ActiveContentChanged);
    }


    private static void SubscribeToScriptEditorEvents()
    {
      if (_scriptEditor == null)
      {
        return;
      }
      _scriptEditor.BeforeHelpRequested += new BeforeHelpRequestedDelegate(_scriptEditor_BeforeHelpRequested);
      _scriptEditor.EditorClosed += new FormClosedEventHandler(_scriptEditor_AfterTextEditorClosed);
    }



    private static void UnsubscribeFromScriptEditorEvents( )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      _scriptEditor.BeforeHelpRequested -= new BeforeHelpRequestedDelegate(_scriptEditor_BeforeHelpRequested);
      _scriptEditor.EditorClosed -= new FormClosedEventHandler(_scriptEditor_AfterTextEditorClosed);
    }

    private static void _hostServices_AfterServicesInitialized( object sender, EventArgs e )
    {
      //Nothing
    }

    private static void _hostServices_ActiveContentChanged( object sender, EventArgs e )
    {
      if (sender is IScriptEditor)
      {
        if (_scriptEditor == sender)
        {
          return;
        }
        UnsubscribeFromScriptEditorEvents();
        _scriptEditor = sender as IScriptEditor;
        SubscribeToScriptEditorEvents();
      }
      else
      {
        UnsubscribeFromScriptEditorEvents();
        _scriptEditor = null;
      }
    }

    static void _scriptEditor_BeforeHelpRequested( object sender, HelpRequestedEventArgs args )
    {
      if (!HtmlHelp2Environment.Config.UseForKeywordHelp)
      {
        return;
      }

      if (args == null || String.IsNullOrEmpty(args.RequestedFor))
      {
        return;
      }

      switch (HtmlHelp2Environment.Config.PadType)
      {
        case F1PadType.Index:
          ShowIndexMenuCommand indexCmd = new ShowIndexMenuCommand();
          indexCmd.Run();
          if (HtmlHelp2IndexPad.Current == null)
          {
            throw new Exception("Can supply help with Help2 Index!");
          }
          HtmlHelp2IndexPad.Current.Search(args.RequestedFor);
          break;
        case F1PadType.Search:
          ShowSearchMenuCommand searchCmd = new ShowSearchMenuCommand();
          searchCmd.InitializeSearchPad();
          if (HtmlHelp2SearchPad.Current == null)
          {
            throw new Exception("Can not supply help with Help2 Search!");
          }
          HtmlHelp2SearchPad.Current.PerformSearch(args.RequestedFor);
          break;
        default:
          throw new Exception("Unsupported F1 pad type!");
      }
      
      args.Cancel = HtmlHelp2Environment.Config.CancelDefaultBehaviour;
      
    }
    
    static void _scriptEditor_AfterTextEditorClosed( object sender, FormClosedEventArgs e )
    {
      UnsubscribeFromScriptEditorEvents();
      _scriptEditor = null;
    }


  }
}
