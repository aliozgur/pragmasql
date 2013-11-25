/********************************************************************
  Class      : SharedSnippetContentProvider
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using PragmaSQL.Core;

namespace PragmaSQL
{


  public class SharedSnippetContentPersister : ContentPersisterBase
  {
    private SharedSnippetsService _facade = new SharedSnippetsService();
    private SharedSnippetItemData SnippetData
    {
      get
      {
        return _data as SharedSnippetItemData;
      }
      set
      {
        _data = value;
      }
    }

    #region Constructor
    public SharedSnippetContentPersister()
      : base()
    {
      _facade.ConnParams = ConfigHelper.Current.PragmaSqlDbConn;
      ContentType = EditorContentType.SharedSnippet;

    }
    #endregion //Constructor


    public override bool SaveContent(string saveHint, TextEditorControl textEditor)
    {
      if (SnippetData == null)
      {
        Utils.ShowWarning("Not enough information exists to save the shared snippet to the database.\r\nYou will be asked to save the snippet as file.", MessageBoxButtons.OK);
        return SaveContentAs(saveHint, textEditor);
      }

      SnippetData.Snippet = textEditor.Text;
      _facade.UpdateItem(SnippetData);
      _contentName = SnippetData.Name;
      _hint = SnippetData.Name;
      return true;
    }


  }
}
