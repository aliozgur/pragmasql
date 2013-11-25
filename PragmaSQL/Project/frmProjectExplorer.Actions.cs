/********************************************************************
  Class frmProjectExplorer
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using Crad.Windows.Forms.Actions;
using PragmaSQL.Core;

namespace PragmaSQL
{
  partial class frmProjectExplorer
  {
    private ActionList _actionList = new ActionList();
    private IDictionary<ProjectNodeActions, Crad.Windows.Forms.Actions.Action> _actions = new Dictionary<ProjectNodeActions, Crad.Windows.Forms.Actions.Action>();
    private void InitializeActions( )
    {

      #region Add Connection From List
      Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_NewConnectionFromList_Update);
      ac.Execute += new EventHandler(OnAction_NewConnectionFromList_Execute);

      ac.Text = "Connection From List";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuConnectionFromList, ac);
      _actions.Add(ProjectNodeActions.AddConnectionFromList, ac);
      #endregion New Connection From List

      #region Add New Connection
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_NewConnection_Update);
      ac.Execute += new EventHandler(OnAction_NewConnection_Execute);

      ac.Text = "Connection";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuNewConnection, ac);
      _actions.Add(ProjectNodeActions.AddNewConnection, ac);

      #endregion New Connection From List

      #region Create project
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.NewWindow;
      ac.Execute += new EventHandler(OnAction_NewProject_Execute);

      ac.Text = "New Project";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuNewProject, ac);
      _actions.Add(ProjectNodeActions.CreateProject, ac);

      #endregion

      #region Open Project
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.open;
      ac.Execute += new EventHandler(OnAction_OpenProject_Execute);

      ac.Text = "Open Project";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuOpenProject, ac);
      _actions.Add(ProjectNodeActions.OpenProject, ac);

      #endregion

      #region Add Folder
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_AddFolder_Update);
      ac.Execute += new EventHandler(OnAction_AddFolder_Execute);

      ac.Text = "Folder";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuNewFolder, ac);
      _actions.Add(ProjectNodeActions.AddFolder, ac);

      #endregion

      #region Refresh
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.Refresh;
      ac.Update += new EventHandler(OnAction_Refresh_Update);
      ac.Execute += new EventHandler(OnAction_Refresh_Execute);

      ac.Text = "Refresh";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuRefreshNode, ac);
      _actions.Add(ProjectNodeActions.RefreshNode, ac);

      #endregion

      #region Close Project
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.Exit;
      ac.Update += new EventHandler(OnAction_CloseProject_Update);
      ac.Execute += new EventHandler(OnAction_CloseProject_Execute);

      ac.Text = "Close Project";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuCloseProject, ac);
      _actions.Add(ProjectNodeActions.CloseProject, ac);

      #endregion

      #region Rename selected item
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.RenameFolder;
      ac.Update += new EventHandler(OnAction_RenameSelectedItem_Update);
      ac.Execute += new EventHandler(OnAction_RenameSelectedItem_Execute);

      ac.Text = "Rename";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuRename, ac);
      _actions.Add(ProjectNodeActions.RenameItems, ac);

      #endregion

      #region Create new script
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_NewScript_Update);
      ac.Execute += new EventHandler(OnAction_NewScript_Execute);

      ac.Text = "Script";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuNewScript, ac);
      _actions.Add(ProjectNodeActions.AddScriptFile, ac);

      #endregion

      #region Create new text
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_NewText_Update);
      ac.Execute += new EventHandler(OnAction_NewText_Execute);

      ac.Text = "Text";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuNewText, ac);
      _actions.Add(ProjectNodeActions.AddTextFile, ac);

      #endregion

      #region Add external file
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_AddExternalFile_Update);
      ac.Execute += new EventHandler(OnAction_AddExternalFile_Execute);

      ac.Text = "Existing File";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuAddFile, ac);
      _actions.Add(ProjectNodeActions.AddExternalFile, ac);

      #endregion

      #region Delete selected items
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.delete;
      ac.Update += new EventHandler(OnAction_DeleteSelectedItems_Update);
      ac.Execute += new EventHandler(OnAction_DeleteSelectedItems_Execute);

      ac.Text = "Delete";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuDelete, ac);
      _actions.Add(ProjectNodeActions.DeleteSelectedItems, ac);
      #endregion

      #region Add Database Object
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update += new EventHandler(OnAction_AddDatabaseObject_Update);
      ac.Execute += new EventHandler(OnAction_AddDatabaseObject_Execute);

      ac.Text = "Object From Database";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuAddObjectFromDatabase, ac);
      _actions.Add(ProjectNodeActions.AddDatabaseObject, ac);

      #endregion

      #region Modify connection
      ac = new Crad.Windows.Forms.Actions.Action();
      ac.Image = PragmaSQL.Properties.Resources.db_edit;
      ac.Update += new EventHandler(OnAction_ModifyConnection_Update);
      ac.Execute += new EventHandler(OnAction_ModifyConnection_Execute);

      ac.Text = "Modify Connection";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuModifyConnectionSpec, ac);
      _actions.Add(ProjectNodeActions.ModifyConnection, ac);

      #endregion
    }

    #region Update and execute actions


    private void OnAction_NewConnectionFromList_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.NewConnectionFromList);    
    }

    private void OnAction_NewConnection_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.NewConnection);
    }
    
    private void OnAction_Generic_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = (_project != null);
    }

    private void OnAction_NewConnection_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.NewConnection);
      AddNewConnection();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.NewConnection);
    }

    private void OnAction_NewConnectionFromList_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.NewConnectionFromList);
      AddConnectionFromList();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.NewConnectionFromList);
    }

    private void OnAction_NewProject_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.CreateProject);
      CreateNewProject();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.CreateProject);
    }

    private void OnAction_OpenProject_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.OpenProject);
      OpenProject();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.OpenProject);
    }

    private void OnAction_AddFolder_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.AddFolder);
    }

    private void OnAction_AddFolder_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.AddFolder);
      AddFolder();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.AddFolder);
    }

    private void OnAction_Refresh_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.Refresh);
    }

    private void OnAction_Refresh_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.Refresh);
      RefreshNode(tv.SelNode, true);
      FireAfterProjectExplorerAction(ProjectExplorerCommand.Refresh);
    }

    private void OnAction_CloseProject_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.CloseProject);
    }


    private void OnAction_CloseProject_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.CloseProject);
      CloseProject();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.CloseProject);
    }

    private void OnAction_RenameSelectedItem_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.RenameSelectedItem);
    }

    private void OnAction_RenameSelectedItem_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.RenameSelectedItem);
      RenameSelectedItem();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.RenameSelectedItem);      
    }

    private void OnAction_NewScript_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.AddScript);
    }

    private void OnAction_NewText_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.AddText);    
    }

    private void OnAction_NewScript_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.AddScript);
      AddContentFile(ProjectItemType.ScriptFile);
      FireAfterProjectExplorerAction(ProjectExplorerCommand.AddScript);
    }

    private void OnAction_NewText_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.AddText);
      AddContentFile(ProjectItemType.TextFile);
      FireAfterProjectExplorerAction(ProjectExplorerCommand.AddText);
    }


    private void OnAction_AddExternalFile_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.AddExternalFile);
    }

    private void OnAction_AddExternalFile_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.AddExternalFile);
      AddExternalFile();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.AddExternalFile);
    }

    private void OnAction_DeleteSelectedItems_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.DeleteSelectedItems);
    }

    private void OnAction_DeleteSelectedItems_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.DeleteSelectedItems);
      DeleteSelectedItems();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.DeleteSelectedItems);
    }

    private void OnAction_AddDatabaseObject_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.AddObjectFromDatabase);
    }

    private void OnAction_AddDatabaseObject_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.AddObjectFromDatabase);
      AddDatabaseObject();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.AddObjectFromDatabase);
    }

    private void OnAction_ModifyConnection_Update( object sender, EventArgs args )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = CanPerformCommand(ProjectExplorerCommand.ModifyConnection);
    }

    private void OnAction_ModifyConnection_Execute( object sender, EventArgs args )
    {
      FireBeforeProjectExplorerAction(ProjectExplorerCommand.ModifyConnection);
      ModifyConnectionSpec();
      FireAfterProjectExplorerAction(ProjectExplorerCommand.ModifyConnection);      
    }


    #endregion //Update and execute actions    
  }//Class

  public enum ProjectNodeActions
  {
    AddConnectionFromList,
    AddNewConnection,
    CreateProject,
    OpenProject,
    AddFolder,
    RefreshNode,
    CloseProject,
    RenameItems,
    AddScriptFile,
    AddTextFile,
    AddExternalFile,
    DeleteSelectedItems,
    AddDatabaseObject,
    ModifyConnection
  }


}//Namespace 
