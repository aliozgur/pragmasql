/********************************************************************
  Interface  : IContentSaveProvider
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.TextEditor;

namespace PragmaSQL
{
  public interface IContentProvider
  {
    object Data
    {
      get;
      set;
    }

    ScriptEditorContentType ContentType
    {
      get;
      set;
    }

    string ContentName
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
    bool LoadContent(object content, TextEditorControl textEditor);
  }
}
