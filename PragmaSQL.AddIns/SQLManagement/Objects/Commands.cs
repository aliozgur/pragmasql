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
using System.Windows.Forms;

using ICSharpCode.Core;
using PragmaSQL.Core;
using System.Reflection;

namespace SQLManagement
{
  #region Condition Evaluators
  public class UserTableCommandsEnabled : IConditionEvaluator
  {
    public bool IsValid( object caller, Condition condition )
    {
      return UserTableCommandsEnabled.CheckCondition();
    }

    public static bool CheckCondition( )
    {
      if (HostServicesSingleton.HostServices == null || HostServicesSingleton.HostServices.ObjectExplorerService == null)
        return false;

      IObjectExplorerService service = HostServicesSingleton.HostServices.ObjectExplorerService;

      if (service == null)
        return false;

      ObjectExplorerNode node = service.SelNode;

      if (node == null)
        return false;

      return node.Type != DBObjectType.UserTable ? false : true;
    }
  }

  public class IndexCommandsEnabled : IConditionEvaluator
  {
    public bool IsValid( object caller, Condition condition )
    { 
      return IndexCommandsEnabled.CheckCondition();
    }

    public static bool CheckCondition()
    {
      if (HostServicesSingleton.HostServices == null || HostServicesSingleton.HostServices.ObjectExplorerService == null)
        return false;

      IObjectExplorerService service = HostServicesSingleton.HostServices.ObjectExplorerService;

      if (service == null)
        return false;

      ObjectExplorerNode node = service.SelNode;
      if (node == null)
        return false;

      return (node.Type == DBObjectType.UserTable || node.Type == DBObjectType.SystemTable || node.Type == DBObjectType.View) ? true : false;
    }
  }

  #endregion //Condition Evaluators

  public class ShowIndexesCommand : MenuCommandBase
  {
    public override void Run()
    {
      base.Run();
      frmIndexes.ShowIndexes();
    }
  }


  public class ShowTableEditorCommand : MenuCommandBase
  {
    public override void Run()
    {
      base.Run();
      frmTableEdit.EditTable();
    }
  }

  public class CreateNewTableCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      frmTableEdit.CreateTable();
    }
  }

  public class RenameTableCommand : MenuCommandBase
  {
    public override void Run( )
    {
      base.Run();
      IObjectExplorerService srv = HostServicesSingleton.HostServices.ObjectExplorerService;
      if (srv == null)
      {
        MessageService.ShowError("No object explorer available!");
        return;
      }

      if (srv.SelNode == null || srv.SelNode.ConnParams == null || String.IsNullOrEmpty(srv.SelNode.ConnParams.Database))
      {
        MessageService.ShowError("Database data is not available!");
        return;
      }

      if (srv.SelNode.Type != DBObjectType.UserTable)
      {
        MessageService.ShowError("Selected node is not a user defined table!");
        return;
      }


      ConnectionParams cp = srv.SelNode.ConnParams.CreateCopy();
      cp.Database = srv.SelNode.DatabaseName;

      TableWrapper tbl = new TableWrapper(cp);
      tbl.ID = srv.SelNode.id;
      tbl.LoadProperties();
      //tbl.Name = srv.SelNode.Name;

      string newName = srv.SelNode.Name;
      if (InputDialog.ShowDialog("Rename Table", "New Name", ref newName) != DialogResult.OK)
        return;

      if (tbl.Name.ToLowerInvariant() == newName.ToLowerInvariant())
        return;

      try
      {
        tbl.Rename(newName);
        srv.ChangeObjectName(srv.SelNode.Node, newName);
        srv.LoadNodeData(srv.SelNode.Node, true);
      }
      catch (Exception ex)
      {
        MessageService.ShowError(ex.Message);
      }

    }
  }
}
