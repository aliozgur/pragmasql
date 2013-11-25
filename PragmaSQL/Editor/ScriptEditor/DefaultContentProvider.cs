/********************************************************************
  Class      : DefaultContentProvider
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Windows.Forms;
using ICSharpCode.TextEditor;

using PragmaSQL.Common;
using PragmaSQL.Database;

namespace PragmaSQL
{
  public class DefaultContentProvider: IContentProvider
  {
    #region Fields and Properties
    private SaveFileDialog _saveDialog = new SaveFileDialog();
    private OpenFileDialog _openDialog = new OpenFileDialog();

    #endregion //Fields and Properties

    #region IContentProvider Members
    
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

    private ScriptEditorContentType _contentType = ScriptEditorContentType.File;
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

    private string _fileName = String.Empty;
    public string ContentName
    {
      get
      {
        return _fileName;
      }
      set
      {
        _fileName = value;
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
      if(textEditor == null)
      {
        throw new NullParameterException("TextEditor parameter is null!");
      }


      if(String.IsNullOrEmpty(_fileName))
      {
        return SaveContentAs(saveHint,textEditor);
      }

     
      textEditor.SaveFile(_fileName);
      FileInfo fi = new FileInfo(_fileName);
      _hint = fi.Name;
      return true;
    }

    public bool SaveContentAs(string saveHint, TextEditorControl textEditor)
    {
      if(textEditor == null)
      {
        throw new NullParameterException("TextEditor parameter is null!");
      }

      if(!String.IsNullOrEmpty(saveHint))
      {
        _saveDialog.FileName = saveHint;
      }
      
      if(_saveDialog.ShowDialog() != DialogResult.OK)
      {
        return false;
      }

      textEditor.SaveFile(_saveDialog.FileName);

      _fileName = _saveDialog.FileName;
      FileInfo fi = new FileInfo(_fileName);
      _hint = fi.Name;
      return true;    
    }

    public bool LoadContent(object content, TextEditorControl textEditor)
    {
      string itemName = String.Empty;
      
      if(content != null)
      {
        itemName = content as string;
      }

      if(textEditor == null)
      {
        throw new NullParameterException("TextEditor parameter is null!");
      }

      string tmp = itemName;

      if (String.IsNullOrEmpty(itemName))
      {
        _openDialog.FileName = String.Empty;
        if (_openDialog.ShowDialog() != DialogResult.OK)
        {
          return false;
        }
        tmp = _openDialog.FileName;
      }

      _fileName = tmp;
      textEditor.LoadFile(_fileName, false, true);

      FileInfo fi = new FileInfo(_fileName);
      _hint = fi.Name;
      return true;
    }

    #endregion

    #region Constructor
    public DefaultContentProvider()
    {
      _saveDialog.DefaultExt = "sql";
      _saveDialog.Filter = "SQL Files|*.sql|Query Files|*.qry|All Files|*.*";
      
      _openDialog.DefaultExt = "sql";
      _openDialog.Filter = "SQL Files|*.sql|Query Files|*.qry|All Files|*.*";
    }

    #endregion //Constructor

  }
}
