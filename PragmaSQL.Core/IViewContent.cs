using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public interface IViewContent
  {
    /// <summary>
    /// Returns the file name (if any) assigned to this view.
    /// </summary>
    string FileName{get;set;}
    
    /// <summary>
    /// Returns current content.
    /// </summary>
    string Content{get;}


  }
}
