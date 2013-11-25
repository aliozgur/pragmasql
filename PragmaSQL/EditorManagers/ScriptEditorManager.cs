using System;
using System.Collections.Generic;
using System.Text;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class ScriptEditorManager
  {

    private static IDictionary<string, frmScriptEditor> _editors = new Dictionary<string, frmScriptEditor>();
    
    public static bool CanBeRemembered(int type)
    {
      return (type == DBObjectType.Function)
        || (type == DBObjectType.ScalarValuedFunction)
        || (type == DBObjectType.SharedScript)
        || (type == DBObjectType.SharedSnippet)
        || (type == DBObjectType.StoredProc)
        || (type == DBObjectType.SystemTable)
        || (type == DBObjectType.Table)
        || (type == DBObjectType.TableValuedFunction)
        || (type == DBObjectType.Trigger)
        || (type == DBObjectType.UserTable)
        || (type == DBObjectType.View);

    }

    public static string ProduceWindowId(string caption, long id, int type, string server, string db)
    {
      if ((id == -1 || type == -1) || !(CanBeRemembered(type)))
        return String.Empty;

      string sep = ((Char)31).ToString();
      return 
        ( String.IsNullOrEmpty(caption) ? Guid.NewGuid().ToString().ToLowerInvariant() : caption.ToLowerInvariant() ) 
        +  sep  + id.ToString() +  sep + type.ToString() + sep + server + sep + db;
    }


    public static void Remember(string id, frmScriptEditor editor)
    {
      if (String.IsNullOrEmpty(id) || editor == null)
        return;

      if(_editors.ContainsKey(id))
      {
        _editors[id] = editor;
      }
      else
      {
        _editors.Add(id, editor);
      }
    }

    public static void Forget(string id)
    {
      if (String.IsNullOrEmpty(id))
        return; 
      if (_editors.ContainsKey(id))
      {
        _editors.Remove(id);
      }    
    }

    public static bool Contains(string id)
    {
      if (String.IsNullOrEmpty(id))
        return false;
 
      return _editors.ContainsKey(id);
    }

    public static frmScriptEditor Get(string id)
    {
      if (String.IsNullOrEmpty(id) )
        return null;

      if (!_editors.ContainsKey(id))
        return null;
      return _editors[id];
    }


    public static void Clear()
    {
      _editors.Clear();
    }

    public static int Count
    {
      get { return _editors.Count; }
    }
  }

}
