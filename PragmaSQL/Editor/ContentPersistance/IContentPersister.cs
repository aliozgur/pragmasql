/********************************************************************
  Interface  : IContentSaveProvider
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.TextEditor;
using PragmaSQL.Core;

namespace PragmaSQL
{
 

  public interface IContentPersister:IContentPersisterEventSink
  {
    object Data
    {
      get;
      set;
    }

    EditorContentType ContentType
    {
      get;
      set;
    }

    string FilePath
    {
      get;
      set;
    }

    string Hint
    {
      get;
      set;
    }

    bool SaveContent(string saveHint, TextEditorControl textEditor);
    bool SaveContentAs(string saveHint, TextEditorControl textEditor);
		bool SaveContentToFile(string fileName, TextEditorControl textEditor);
		bool LoadContent(object content, TextEditorControl textEditor);

  }
}
