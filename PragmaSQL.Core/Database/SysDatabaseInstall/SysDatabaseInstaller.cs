/********************************************************************
  Class PragmaSQLDatabaseInstaller
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace PragmaSQL.Core
{
  public static class SysDatabaseInstaller
  {
    public static string GetDatabaseObjectInstallScript( )
    {
      Assembly thisAss = System.Reflection.Assembly.GetExecutingAssembly();
      StreamReader file = null;
      try
      {
        file = new StreamReader(thisAss.GetManifestResourceStream("PragmaSQL.Core.Database.SysDatabaseInstall.PragmaSQL_Objects.sql"));
        string result = file.ReadToEnd();
        return result;
      }
      finally
      {
        if (file != null)
        {
          file.Close();
        }
      }
    }

    public static string GetDatabaseInstallScript( )
    {
      Assembly thisAss = System.Reflection.Assembly.GetExecutingAssembly();
      StreamReader file = null;
      try
      {
        file = new StreamReader(thisAss.GetManifestResourceStream("PragmaSQL.Core.Database.SysDatabaseInstall.PragmaSQL_DB.sql"));
        string result = file.ReadToEnd();
        return result;
      }
      finally
      {
        if (file != null)
        {
          file.Close();
        }
      }
    }

  }
}
