/********************************************************************
  Class ITextDiffService
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
  public interface ITextDiffService
  {
    void DiffText( string caption, string source, string destination );
    void DiffText( string source, string destination );

    void DiffFile( string caption, string sourceFileName, string destinationFileName );
    void DiffFile( string sourceFileName, string destinationFileName );
  }
}
