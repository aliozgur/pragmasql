/********************************************************************
  Class DatabaseTasks
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using ICSharpCode.Core;
using PragmaSQL.Core;

namespace SQLManagement
{
  public static class DatabaseTasks
  {
    
    public static bool DropDatabase(ConnectionParams cp, long dbid )
    {
      if (!MessageService.AskQuestion("Are you sure you want to drop the selected database?\nDatabase name: " + cp.Database))
        return false;

      ConnectionParams tmp = cp.CreateCopy();
      tmp.Database = "master";
      using (SqlConnection conn = tmp.CreateSqlConnection(true, false))
      {
        DbCmd.DropDb(conn, cp.Database, dbid);
      }
      return true;
    }

    public static bool DetachDatabase( ConnectionParams cp  )
    {
      if (!MessageService.AskQuestion("Are you sure you want to detach the selected database?\nDatabase name: " + cp.Database))
        return false;

      ConnectionParams tmp = cp.CreateCopy();
      tmp.Database = "master";
      using (SqlConnection conn = tmp.CreateSqlConnection(true, false))
      {
        DbCmd.DetachDb(conn, cp.Database);
      }
      return true;
    }

    public static bool TruncateLogs( ConnectionParams cp )
    {
      if (!MessageService.AskQuestion("Are you sure you want to truncate logs for the selected database?\nDatabase name: " + cp.Database))
        return false;

      ConnectionParams tmp = cp.CreateCopy();
      tmp.Database = "master";
      using (SqlConnection conn = tmp.CreateSqlConnection(true, false))
      {
        DbCmd.TruncLog(conn, cp.Database);
      }
      return true;
    }

    public static bool ShrinkDatabase( ConnectionParams cp )
    {
      return frmShrinkDb.ShowShrinkDbDialog(cp) == DialogResult.OK;
    }

  }
}
