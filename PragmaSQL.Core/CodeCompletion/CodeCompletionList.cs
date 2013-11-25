using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  /// <summary>
  /// Collection of code completion lists. Defines dictionary of items in a list.
	/// <remarks>Used internally by PragmaSQL application.</remarks>
	/// </summary>
	public class CodeCompletionList
  {
    public string Name = String.Empty;

    private SerializableDictionary<string, string> _items = new SerializableDictionary<string, string>();
    public SerializableDictionary<string, string> Items
    {
      get { return _items; }
      set { _items = value; }
    }

    public string this[string key]
    {
      get
      {
        return _items[key];
      }
      set
      {
        _items[key] = value;      
      }
    }

    public override string ToString()
    {
      return this.Name;
    }
  }
}
