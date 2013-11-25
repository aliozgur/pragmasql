/********************************************************************
  Class IContentPersisterEventSink
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public interface IContentPersisterEventSink
  {
    event BeforeSavedContentToFileDelegate BeforeSavedContentToFile;

  }
}
