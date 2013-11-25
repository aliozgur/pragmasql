/********************************************************************
  Class      : SharedScriptContentProvider 
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor;
using PragmaSQL.Common;
using PragmaSQL.Database;

namespace PragmaSQL
{


  public class SharedScriptContentProvider : IContentProvider
  {
    #region Fields and Properties
    #endregion //Fields and Properties

    private DefaultContentProvider _defaultProvider = new DefaultContentProvider();
    private SharedScriptsFacade _facade = new SharedScriptsFacade();

    #region IContentProvider Members

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

    private object _data = null;
    public object Data
    {
      get
      {
        return _data;
      }
      set
      {
        _data = value;
      }
    }

    private ScriptEditorContentType _contentType = ScriptEditorContentType.SharedScript;
    public ScriptEditorContentType ContentType
    {
      get
      {
        return _contentType;
      }
      set
      {
        _contentType = value;
      }
    }

    private string _contentName = String.Empty;
    public string ContentName
    {
      get
      {
        return _contentName;
      }
      set
      {
        _contentName = value;
      }
    }

    private string _hint = String.Empty;
    public string Hint
    {
      get
      {
        return _hint;
      }
      set
      {
        _hint = value;
      }
    }

    public bool SaveContent(string saveHint, TextEditorControl textEditor)
    {
      if (Data == null)
      {
        throw new NullPropertyException("Data field is null!");
      }

      if (ScriptData == null)
      {
        return SaveContentAs(saveHint, textEditor);
      }

      ScriptData.Script = textEditor.Text;
      _facade.UpdateItem(ScriptData);
      _contentName = ScriptData.Name;
      _hint = ScriptData.Name;
      return true;
    }

    public bool SaveContentAs(string saveHint, TextEditorControl textEditor)
    {
      if (textEditor == null)
      {
        throw new NullParameterException("TextEditor parameter is null!");
      }
      return _defaultProvider.SaveContentAs(saveHint, textEditor);
    }

    public bool LoadContent(object content, TextEditorControl textEditor)
    {
      if (textEditor == null)
      {
        throw new NullParameterException("TextEditor parameter is null!");
      }

      return _defaultProvider.LoadContent(content, textEditor);
    }

    #endregion

    #region Constructor
    public SharedScriptContentProvider()
    {
      _facade.ConnParams = ConfigurationLoader.CurrentConfig.PragmaSqlDbConn;
    }

    #endregion //Constructor

  }
}
