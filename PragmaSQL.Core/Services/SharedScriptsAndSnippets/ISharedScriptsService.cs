/********************************************************************
  Class ISharedScriptsService
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public interface ISharedScriptsService
  {
    ConnectionParams ConnParams { get; }

    IList<SharedScriptsItemData> GetChildren( int? parentID );
    void AddItem( SharedScriptsItemData data );
    void DeleteItem( int? id );
    void UpdateItem( SharedScriptsItemData data );
  }
}
