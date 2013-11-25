using System;
using System.Text.RegularExpressions;

namespace PragmaSQL
{
  /// <summary>
  /// Search event arguments for a control search
  /// </summary>
  public class SearchEventArgs : EventArgs
  {
    private bool _successful = false;
    public bool Successful
    {
      get { return _successful; }
      set { _successful = value; }
    }

    private Regex _searchRegularExpression;
    public Regex SearchRegularExpression
    {
      get { return _searchRegularExpression; }
    }


    /// <summary>
    /// Makes search event arguments for a control search
    /// </summary>
    public SearchEventArgs( Regex searchRegularExpression)
    {
      _searchRegularExpression = searchRegularExpression;
    }
  }
}
