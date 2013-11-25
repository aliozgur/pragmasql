/********************************************************************
  Class IndexColumn
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
  public class IndexColumnWrapper
  {
    public string Name = String.Empty;
    public int Order = -1;

    public IndexColumnWrapper( )
    {
    }

    public IndexColumnWrapper(string name, int order)
    {
      Name = name;
      Order = order;
    }

    public override string ToString( )
    {
      return Name;
    }
  }
}
