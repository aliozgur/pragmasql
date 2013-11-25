/********************************************************************
  Interface  : IScriptEditor
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using WeifenLuo.WinFormsUI.Docking;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace PragmaSQL
{
  public interface IPragmaEditor : IDockContent
  {
    bool ContentModified { get;set;}
    IContentPersister ContentPersister { get;set;}
    string Caption { get;set;}
    DateTime? LastModifiedOn { get;}
    bool CheckSave { get;set;}
    TextEditorControl TextEditor { get;}
    TextArea ActiveTextArea { get;}
    IDocument ActiveDocument { get;}
    bool SharedScriptOperationsVisible { get;set;}
    Icon Icon { get;set;}
    string ContentInfo { get;set;}

    void RefreshCodeCompletionLists( );
    void RefreshShortcuts( );
    void SaveContent( );
    void SaveContentAs( );
    bool OpenFile( string fileName );
		void WatchFile();
		void StopFileWatcher();

    void ApplyTextEditorOptionsFromCurrentConfig( );
    void InspectPragmaSQLDbConnection( );
  }
}
