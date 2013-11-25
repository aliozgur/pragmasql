/********************************************************************
  Class      : Paranthesis
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class Paranthesis
  {
    public string Value = String.Empty;
    public bool IsPreSelect = false;

    public Paranthesis(string value, bool isPreSelect)
    {
      Value = value;
      IsPreSelect = isPreSelect;
    }
  }
}
