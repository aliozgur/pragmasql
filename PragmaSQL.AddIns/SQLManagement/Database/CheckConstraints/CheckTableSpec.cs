/********************************************************************
  Class CheckTableSpec
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
  public class CheckTableSpec
  {
    public string Owner = String.Empty;
    public string Name = String.Empty;

    public CheckTableSpec( )
    {
    }

    public CheckTableSpec( string owner, string name )
    {
      Owner = owner;
      Name = name;
    }

    public string FullName
    {
      get
      {
        return String.Format("{0}].{1}", Owner, Name);
      }
    }

    public string FullNameQuoted
    {
      get
      {
        return String.Format("[{0}].[{1}]",Owner,Name);
      }
    }


    public override string ToString( )
    {
      return FullNameQuoted;
    }
  }
}
