/********************************************************************
  Class      : ScriptData
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL
{
  public enum RunType
  {
    Execute,
    CheckSyntax,
    ShowPlan
  }

  public struct ScriptData
  {
    public string ScriptText;
    public RunType ScriptRunType;
    public int SelStartLineNo;
  }
}
