/********************************************************************
  Class PopupBlockerFilterLevel
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL
{
  [Serializable]
  public enum PopupBlockerFilterLevel
  {
    /// <summary>
    /// No pop-ups are blocked
    /// </summary>
    None = 0,
    /// <summary>
    /// Pop-ups of secure sites are allowed
    /// </summary>
    Low,
    /// <summary>
    /// Most pop-ups are blocked, unless the Ctrl key is pressed
    /// </summary>
    Medium,
    /// <summary>
    /// All pop-ups are blocked, unless the Ctrl key is pressed
    /// </summary>
    High
  }
}
