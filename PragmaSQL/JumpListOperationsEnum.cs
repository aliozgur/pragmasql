using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PragmaSQL
{
  public enum JumpListOperationsEnum
  {
    None,
    NewScript,
    NewText,
    NewDiff,
    NewBrowser,
    OpenFile,
    OpenScript,
    OpenProject,
    SaveScriptsForRecovery,
    ConnectTo,
    ConnectionFromList,
    SavedConnections
  }
}
