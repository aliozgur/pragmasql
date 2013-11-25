/********************************************************************
  Class ContentPersisterBase
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Windows.Forms;
using ICSharpCode.TextEditor;

using PragmaSQL.Core;


namespace PragmaSQL
{
  public class ContentPersisterBase:IContentPersisterEventSink,IContentPersister
  {
    #region Fields and Properties
    
    protected SaveFileDialog _saveDialog = new SaveFileDialog();
    protected OpenFileDialog _openDialog = new OpenFileDialog();

    #endregion //Fields and Properties

    #region IContentPersister Members

    protected object _data = null;
    public virtual object Data
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

    protected EditorContentType _contentType;
    public virtual EditorContentType ContentType
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

    protected string _contentName = String.Empty;
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

    protected string _filePath = String.Empty;
    public virtual string FilePath
    {
      get
      {
        return _filePath;
      }
      set
      {
        _filePath = value;
      }
    }

    protected string _hint = String.Empty;
    public virtual string Hint
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

    public virtual bool SaveContent(string saveHint, TextEditorControl textEditor)
    {
      if(textEditor == null)
        throw new NullParameterException("TextEditor parameter is null!");

      if(String.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
        return SaveContentAs(saveHint,textEditor);
    
      textEditor.SaveFile(_filePath);
      ContentType = EditorContentType.File;
      FileInfo fi = new FileInfo(_filePath);
      _hint = fi.Name;
      return true;
    }

    public virtual bool SaveContentToFile(string filePath, TextEditorControl textEditor)
    {
      if (textEditor == null)
        throw new NullParameterException("TextEditor parameter is null!");


      FireBeforeSavedContentToFile(filePath);
      textEditor.SaveFile(filePath);
      ContentType = EditorContentType.File;
      _filePath = filePath;
      FileInfo fi = new FileInfo(_filePath);
      _hint = fi.Name;
      return true;

    }

    public virtual bool SaveContentAs(string saveHint, TextEditorControl textEditor)
    {
      if (textEditor == null)
        throw new NullParameterException("TextEditor parameter is null!");

      if (!String.IsNullOrEmpty(saveHint))
        _saveDialog.FileName = saveHint;

      if (_saveDialog.ShowDialog() != DialogResult.OK)
        return false;

      FireBeforeSavedContentToFile(_saveDialog.FileName);
      textEditor.SaveFile(_saveDialog.FileName);
      ContentType = EditorContentType.File;
      _filePath = _saveDialog.FileName;
      FileInfo fi = new FileInfo(_filePath);
      _hint = fi.Name;
      return true;
    }

    public virtual bool LoadContent(object content, TextEditorControl textEditor)
    {
      string itemName = String.Empty;

      if (content != null)
        itemName = content as string;

      if (textEditor == null)
        throw new NullParameterException("TextEditor parameter is null!");

      string tmp = itemName;

      if (String.IsNullOrEmpty(itemName))
      {
        _openDialog.FileName = String.Empty;
        if (_openDialog.ShowDialog() != DialogResult.OK)
        {
          return false;
        }
        tmp = _openDialog.FileName;
        Program.MainForm.AddFileToMru(tmp);
      }


      _filePath = tmp;
      FileInfo fi = new FileInfo(_filePath);
      _hint = fi.Name;

      if (fi.Extension.ToLowerInvariant() == ".sql" || fi.Extension.ToLowerInvariant() == ".qry")
        textEditor.LoadFile(_filePath, true, true);
      else
        textEditor.LoadFile(_filePath, true, true);
      return true;
    }


    #endregion

    #region Constructor
    public ContentPersisterBase()
    {
      _saveDialog.DefaultExt = "sql";
      _saveDialog.Filter = "SQL Files|*.sql|Query Files|*.qry|Text Files|*.txt|All Files|*.*";
      
      _openDialog.DefaultExt = "sql";
      _openDialog.Filter = "SQL Files|*.sql|Query Files|*.qry|Text Files|*.txt|All Files|*.*";
    }

    #endregion //Constructor
   
    #region IContentPersisterEventSink Members

    private BeforeSavedContentToFileDelegate _beforeSaveContentToFile;
    public event BeforeSavedContentToFileDelegate BeforeSavedContentToFile
    {
      add
      {
        _beforeSaveContentToFile += value;
      }
      remove
      {
        _beforeSaveContentToFile -= value;  
      }
    }

    protected void FireBeforeSavedContentToFile(string fileName)
    {
      if (_beforeSaveContentToFile == null)
      {
        return;
      }

      Delegate[] delegates = _beforeSaveContentToFile.GetInvocationList();
      foreach (BeforeSavedContentToFileDelegate del in delegates)
      {
        try
        {
          FileOperationEventArgs args = new FileOperationEventArgs();
          args.FileName = fileName;
          del.Invoke(this, args);
        }
        catch (Exception ex)
        {
          HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
          HostServicesSingleton.HostServices.MsgService.ShowMessages();
        }
      }
    }

    
    #endregion
  }
}
