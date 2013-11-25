/********************************************************************
  Class NameIdPair
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SQLManagement
{
  public class NameIdPair
  {
    public long Id = -1;
    public string Name = String.Empty;

    public NameIdPair( ) { }
    public NameIdPair( long id, string name )
    {
      Id = id;
      Name = name;
    }


    public override string ToString( )
    {
      return Name;
    }
  }
}
