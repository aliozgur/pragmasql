/********************************************************************
  Class Commands
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.Core;
using PragmaSQL.Core;
using PragmaSQL.Scripting.Smo;

namespace SQLManagement
{
  #region ObjectExplorer Service Helper
  internal static class ObjectExplorerServiceWrapper
  {
    internal static ConnectionParams CurrentConnection
    {
      get
      {
        IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
        if (srv == null)
        {
          MessageService.ShowError("No object explorer available!");
          return null;
        }

        if (srv.SelNode == null || srv.SelNode.ConnParams == null)
        {
          MessageService.ShowError("Database data is not available!");
          return null;
        }

        if (String.IsNullOrEmpty(srv.SelNode.ConnParams.Database))
        {
          MessageService.ShowError("Selected node is not a database or child of a database!");
          return null;
        }
        
        ConnectionParams cp = srv.SelNode.ConnParams.CreateCopy();
        cp.Database = srv.SelNode.DatabaseName;

        return cp;
      }
    }

    internal static long CurrentDatabaseId
    {
      get
      {
        IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
        if (srv == null)
        {
          MessageService.ShowError("No object explorer available!");
          return -1;
        }

        if (srv.SelNode == null)
        {
          MessageService.ShowError("Database data is not available!");
          return -1;
        }

        return srv.SelNode.DbId;;
      }
    }
  }
  #endregion //ObjectExplorer Service Helper

  #region Condition Evaluators
  public class DatabaseCommandsEnabled : IConditionEvaluator
  {
    public bool IsValid( object caller, Condition condition )
    {
      return DatabaseCommandsEnabled.CheckCondition();
    }

    public static bool CheckCondition( )
    {
      if (HostServicesSingleton.HostServices == null || HostServicesSingleton.HostServices.ObjectExplorerService == null)
        return false;

      IObjectExplorerService service = HostServicesSingleton.HostServices.ObjectExplorerService;

      if (service == null)
        return false;

      ObjectExplorerNode node = service.SelNode;
      if (node == null || node.ConnParams == null)
        return false;

      return String.IsNullOrEmpty(node.DatabaseName) ? false : true;
    }
  }
  #endregion //Condition Evaluators

  public class ShowDatabaseUsersCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmUsers.ShowUsers();
    }    
  }

  public class ShowDatabasePropertiesCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmDbProperties.ShowDatabaseProperties();
    }
  }

  public class AttachDatabaseCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmAttachDatabase.ShowAttachDatabaseDialog();
    }
  }

  public class DetachDatabaseCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      ConnectionParams cp = ObjectExplorerServiceWrapper.CurrentConnection;
      if (cp == null)
        return;
      DatabaseTasks.DetachDatabase(cp);
    }
  }

  public class DropDatabaseCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      ConnectionParams cp = ObjectExplorerServiceWrapper.CurrentConnection;
      if (cp == null)
        return;
      DatabaseTasks.DropDatabase(cp, ObjectExplorerServiceWrapper.CurrentDatabaseId);
    }
  }


  public class ShrinkDatabaseCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      ConnectionParams cp = ObjectExplorerServiceWrapper.CurrentConnection;
      if (cp == null)
        return;
      DatabaseTasks.ShrinkDatabase(cp);
    }
  }

  public class TruncateDatabaseLogsCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      ConnectionParams cp = ObjectExplorerServiceWrapper.CurrentConnection;
      if (cp == null)
        return;
      DatabaseTasks.TruncateLogs(cp);
    }
  }

  public class ShowNewDatabaseDialogCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmNewDatabase.ShowNewDatabaseDialog();
    }
  }

  public class ShowRolesCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmRoles.ShowRoles();
    }
  }

  public class ShowRulesCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmRules.ShowRules();
    }
  }

  public class ShowCheckConstraintsCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmChecks.ShowChecks();
    }
  }

  public class ShowAllIndexesCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmAllIndexes.ShowAllIndexes();
    }
  }

  public class ShowAllForeignKeysCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmAllForeignKeys.ShowAllForeignKeys();
    }
  }

  public class ShowUdtListCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmUdtList.ShowUdtList();
    }
  }

  public class ShowPragmaSQLScripterDialog : MenuCommandBase
	{
		public override void Run()
		{
      base.Run();
      BatchScripterDialog.ShowBatchScriptDialog();
		}
	}

  public class ShowPragmaSQLBulkCopyDialog : MenuCommandBase
	{
		public override void Run()
		{
      base.Run();
      BulkCopyDialog.ShowBulkCopyDialog();
		}
	}

  public class DumpObjectInfosFromExplorer : MenuCommandBase
	{
		public override void Run()
		{
      base.Run();
      DumpObjectInfos();
		}

		private void DumpObjectInfos()
		{
			if (HostServicesSingleton.HostServices == null || HostServicesSingleton.HostServices.ObjectExplorerService == null)
				throw new InvalidOperationException("ObjectExplorer is not active or visible!");

			IList<ObjectExplorerNode> selNodes = HostServicesSingleton.HostServices.ObjectExplorerService.SelNodes;
			
			IDictionary<string, StringBuilder> dumpJobs = new Dictionary<string, StringBuilder>();
			StringBuilder sb = null;

			string template = "{0}=[{1}].[{2}]";
			string objType = String.Empty;
			string dbInfo = String.Empty;

			foreach (ObjectExplorerNode node in selNodes)
			{
				if (!DBObjectType.CanTypeBeDumpedForScriptingWizardUsage(node.Type))
					continue;
				if(node.ConnParams == null)
					continue;

				dbInfo = node.DatabaseName + " on " + node.ConnParams.Server;
				if (dumpJobs.ContainsKey(dbInfo))
					sb = dumpJobs[dbInfo];
				else
				{
					sb = new StringBuilder();
					dumpJobs.Add(dbInfo, sb);
				}

				objType = DbObjectListUtils.EncodeObjectExplorerNodeType(node.Type);
				sb.AppendLine(String.Format(template, objType, node.Owner, node.Name));
			}

			string editorCaption = String.Empty;
			string errors = String.Empty;
			foreach (string info in dumpJobs.Keys)
			{
				sb = dumpJobs[info];
				try
				{
					editorCaption = "Objects [" + info + "]";
					HostServicesSingleton.HostServices.EditorServices.CreateTextEditor(editorCaption, sb.ToString());
				}
				catch (Exception ex)
				{
					errors += "Error Type: " + ex.GetType().Name + ", Message: " + ex.Message + "\r\n";
				}
			}

			if (!String.IsNullOrEmpty(errors))
			{
				GenericErrorDialog.ShowError("Dump Error", "Some errors occured during object dump.", errors);
			}
		}
	}
}
