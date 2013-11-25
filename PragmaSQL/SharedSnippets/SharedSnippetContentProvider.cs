/********************************************************************
  Class      : SharedSnippetContentProvider
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
 

  public class SharedSnippetContentProvider:IContentProvider
  {
    #region Fields and Properties
    #endregion //Fields and Properties

    private DefaultContentProvider _defaultProvider = new DefaultContentProvider();
    private SharedSnippetsFacade _facade = new SharedSnippetsFacade();

    #region IContentProvider Members
    
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

    private ScriptEditorContentType _contentType = ScriptEditorContentType.SharedSnippet;
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
      if(Data == null)
      {
        throw new NullPropertyException("Data field is null!");
      }
     
      if(SnippetData == null)
      {
        return SaveContentAs(saveHint,textEditor);
      }

      SnippetData.Snippet = textEditor.Text;
      _facade.UpdateItem(SnippetData);
      _contentName = SnippetData.Name;
      _hint = SnippetData.Name;
      return true;
    }

    public bool SaveContentAs(string saveHint, TextEditorControl textEditor)
    {
      if(textEditor == null)
      {
        throw new NullParameterException("TextEditor parameter is null!");
      }
      return _defaultProvider.SaveContentAs(saveHint,textEditor);
    }

    public bool LoadContent(object content, TextEditorControl textEditor)
    {
      if(textEditor == null)
      {
        throw new NullParameterException("TextEditor parameter is null!");
      }

      return _defaultProvider.LoadContent(content,textEditor);
    }

    #endregion

    #region Constructor
    public SharedSnippetContentProvider()
    {
      _facade.ConnParams = ConfigurationLoader.CurrentConfig.PragmaSqlDbConn;
    }

    #endregion //Constructor

  }
}
