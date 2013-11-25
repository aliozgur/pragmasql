using System;
using System.Collections.Generic;
using System.Text;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace PragmaSQL
{
  public static class TableDataEditorManager
  {

    private static IDictionary<string, frmDataViewer> _editors = new Dictionary<string, frmDataViewer>();
    public static string ProduceWindowId(string name, string server, string db)
    {
      if ( String.IsNullOrEmpty(name))
        return String.Empty;

      string sep = ((Char)31).ToString();
      return name + sep + server + sep + db;
    }


    public static void Remember(string id, frmDataViewer editor)
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

    public static frmDataViewer Get(string id)
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
