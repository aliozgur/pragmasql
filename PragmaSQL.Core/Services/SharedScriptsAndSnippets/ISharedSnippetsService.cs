using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public interface ISharedSnippetsService
  {
    ConnectionParams ConnParams { get; }

    IList<SharedSnippetItemData> GetChildren( int? parentID );
    void AddItem( SharedSnippetItemData data );
    void DeleteItem( int? id );
    void UpdateItem( SharedSnippetItemData data );
  }
}
