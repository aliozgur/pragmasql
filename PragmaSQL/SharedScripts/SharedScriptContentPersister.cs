/********************************************************************
  Class      : SharedScriptContentProvider 
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


  public class SharedScriptContentPersister :ContentPersisterBase
  {
    private SharedScriptsService _facade = new SharedScriptsService();
    private SharedScriptsItemData ScriptData
    {
      get
      {
        return _data as SharedScriptsItemData;
      }
      set
      {
        _data = value;
      }
    }


    #region Constructor
    public SharedScriptContentPersister()
      :
      base()
    {
      _facade.ConnParams = ConfigHelper.Current.PragmaSqlDbConn;
      ContentType = EditorContentType.SharedScript;
    }

    #endregion //Constructor

    public override bool SaveContent(string saveHint, TextEditorControl textEditor)
    {
      if (ScriptData == null)
      {
        Utils.ShowWarning("Not enough information exists to save the shared script to the database.\r\nYou will be asked to save the script as file.", MessageBoxButtons.OK);
        return SaveContentAs(saveHint, textEditor);
      }

      ScriptData.Script = textEditor.Text;
      _facade.UpdateItem(ScriptData);
      _contentName = ScriptData.Name;
      _hint = ScriptData.Name;
      return true;
    }



  }
}
