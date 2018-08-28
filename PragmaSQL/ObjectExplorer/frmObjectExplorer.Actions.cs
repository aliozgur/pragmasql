/************************************************************************************************************
 * Ali Özgür
 * ali_ozgur@hotmail.com
 * www.pragmasql.com 
 * 
 * Source code included in this file can not be used without written
 * permissions of the owner mentioned above. 
 * All rigths reserveript = 
 * Copyright PragmaSQL 2007 
 ************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.Core;
using MWCommon;

using PragmaSQL.Core;

namespace PragmaSQL
{
    partial class frmObjectExplorer
    {

        #region Build Node Context Menu

        public ContextMenuStrip CreateNodeContextMenu(int objectType)
        {
            if (_contextMenus.Keys.Contains(objectType))
            {
                return _contextMenus[objectType];
            }

            ToolStripMenuItem item = null;
            ContextMenuStrip mnu = null;
            mnu = new ContextMenuStrip(this.components);
            mnu.Opening += new System.ComponentModel.CancelEventHandler(mnu_Opening);
            switch (objectType)
            {
                case DBObjectType.Server:
                    mnu.Items.Add("Connect To", null, OnNewConnectionClick);
                    mnu.Items.Add("Connection From List", null, OnNewConnectionFromRepositoryClick);
                    mnu.Items.Add("Disconnect From Server", null, OnDisconnectClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/Server");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.UsersGroup:
                    //mnu.Items.Add("View As List", null, null);
                    mnu.Items.Add("Filter", null, OnFilterClick);
                    mnu.Items.Add("Clear Filter", null, OnClearFilterClick);
                    //mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/UsersGroup");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.Database:
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);

                    //mnu.Items.Add("Script Objects", null, OnScriptDatabaseObjects);
                    //mnu.Items.Add("Bulk Copy Data", null, OnBulkCopyData);

                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Search on Db", null, OnCreateDatabaseSearchForm);
                    mnu.Items.Add("Chanage History", null, OnShowObjectChangeHistoryViewer);
                    mnu.Items.Add("Object Grouping", null, OnCreateObjectGroupingForm);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/Database");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.GroupingFolderB:
                    //mnu.Items.Add("View As List", null, null);
                    mnu.Items.Add("Filter", null, OnFilterClick);
                    mnu.Items.Add("Clear Filter", null, OnClearFilterClick);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Search on Db", null, OnCreateDatabaseSearchForm);
                    mnu.Items.Add("Chanage History", null, OnShowObjectChangeHistoryViewer);
                    mnu.Items.Add("Object Grouping", null, OnCreateObjectGroupingForm);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/Folder");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.GroupingFolderY:
                    //mnu.Items.Add("View As List", null, null);
                    mnu.Items.Add("Filter", null, OnFilterClick);
                    mnu.Items.Add("Clear Filter", null, OnClearFilterClick);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Search on Db", null, OnCreateDatabaseSearchForm);
                    mnu.Items.Add("Chanage History", null, OnShowObjectChangeHistoryViewer);
                    mnu.Items.Add("Object Grouping", null, OnCreateObjectGroupingForm);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/Folder");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.Column:
                    mnu.Items.Add("Permissions", null, OnHelpColumnPermissionClick);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/Column");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.UserTable:
                    mnu.Items.Add("Select Top 100 Rows", null, OnSelectTop100RowsClick);
                    mnu.Items.Add("Open", null, OnOpenTableDataEditorClick);

                    mnu.Items.Add("Drop", null, OnDropObject);
                    mnu.Items.Add("View Columns", null, OnViewColumnsClick);
                    mnu.Items.Add("View References", null, OnAnyReferenceClick);

                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Generate", null, null);
                    item.DropDownItems.Add("Drop Script", null, OnGenerateDropScript);
                    //item.DropDownItems.Add("Create", null, OnGenerateCreateScript);
                    item.DropDownItems.Add("CRUD Procedures", null, OnGenerateCrudProcedures);

                    item = (ToolStripMenuItem)mnu.Items.Add("Table Info", null, null);
                    item.DropDownItems.Add("Table Permissions", null, OnHelpPermissionClick);
                    item.DropDownItems.Add("Column Permissions", null, OnHelpTableColumnPermissionClick);
                    item.DropDownItems.Add("Foreign Keys", null, OnHelpForeignKeysClick);
                    item.DropDownItems.Add("Foreign Key In", null, OnHelpForeignKeyInClick);
                    item.DropDownItems.Add("Constraints", null, OnHelpConstraintsClick);
                    item.DropDownItems.Add("Special Columns", null, OnHelpSpecialColumnsClick);
                    item.DropDownItems.Add("-", null, null);
                    item.DropDownItems.Add("Used Space", null, OnHelpUsedSpaceClick);
                    item.DropDownItems.Add("Statistics", null, OnHelpStatisticsClick);

                    mnu.Items.Add("Object Help", null, OnObjectHelpClick);
                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Add Object To Group", null, OnAddSerlectedObjectsToGroup);
                    mnu.Items.Add("Show Grouping Statistics", null, OnShowObjectGroupingStatistics);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);

                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/UserTable");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.SystemTable:
                    mnu.Items.Add("Select Top 100 Rows", null, OnSelectTop100RowsClick);
                    mnu.Items.Add("Open", null, OnOpenTableDataEditorClick);
                    mnu.Items.Add("View Columns", null, OnViewColumnsClick);
                    mnu.Items.Add("View References", null, OnAnyReferenceClick);

                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Table Info", null, null);
                    item.DropDownItems.Add("Table Permissions", null, OnHelpPermissionClick);
                    item.DropDownItems.Add("Column Permissions", null, OnHelpTableColumnPermissionClick);
                    item.DropDownItems.Add("Foreign Keys", null, OnHelpForeignKeysClick);
                    item.DropDownItems.Add("Foreign Key In", null, OnHelpForeignKeyInClick);
                    item.DropDownItems.Add("Constraints", null, OnHelpConstraintsClick);
                    item.DropDownItems.Add("Special Columns", null, OnHelpSpecialColumnsClick);
                    item.DropDownItems.Add("-", null, null);
                    item.DropDownItems.Add("Used Space", null, OnHelpUsedSpaceClick);
                    item.DropDownItems.Add("Statistics", null, OnHelpStatisticsClick);

                    mnu.Items.Add("Object Help", null, OnObjectHelpClick);
                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/SystemTable");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.View:
                    mnu.Items.Add("Select Top 100 Rows", null, OnSelectTop100RowsClick);
                    mnu.Items.Add("Open", null, OnOpenViewDataEditorClick);
                    mnu.Items.Add("Modify", null, OnModifyClick);
                    mnu.Items.Add("Modify (Append to current)", null, OnModifyInCurrentScriptEditorClick);
                    mnu.Items.Add("Change History", null, OnShowObjectChangeHistory);

                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("View Columns", null, OnViewColumnsClick);
                    mnu.Items.Add("View References", null, OnAnyReferenceClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Drop Object", null, OnDropObject);
                    mnu.Items.Add("Generate Drop Script", null, OnGenerateDropScript);

                    item = (ToolStripMenuItem)mnu.Items.Add("Diff", null, null);
                    item.DropDownItems.Add("As Source", null, OnDiffAsSourceClick);
                    item.DropDownItems.Add("As Dest", null, OnDiffAsDestClick);

                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Info", null, null);
                    item.DropDownItems.Add("Object Help", null, OnObjectHelpClick);
                    item.DropDownItems.Add("Constraints", null, OnHelpConstraintsClick);
                    item.DropDownItems.Add("View Permissions", null, OnHelpPermissionClick);
                    item.DropDownItems.Add("Column Permissions", null, OnHelpTableColumnPermissionClick);

                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Add Object To Group", null, OnAddSerlectedObjectsToGroup);
                    mnu.Items.Add("Show Grouping Statistics", null, OnShowObjectGroupingStatistics);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/View");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.Trigger:
                    mnu.Items.Add("Modify", null, OnModifyClick);
                    mnu.Items.Add("Modify (Append to current)", null, OnModifyInCurrentScriptEditorClick);
                    mnu.Items.Add("Change History", null, OnShowObjectChangeHistory);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Drop Object", null, OnDropObject);
                    mnu.Items.Add("Generate Drop Script", null, OnGenerateDropScript);

                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Diff", null, null);
                    item.DropDownItems.Add("As Source", null, OnDiffAsSourceClick);
                    item.DropDownItems.Add("As Dest", null, OnDiffAsDestClick);

                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Object Help", null, OnObjectHelpClick);
                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Add Object To Group", null, OnAddSerlectedObjectsToGroup);
                    mnu.Items.Add("Show Grouping Statistics", null, OnShowObjectGroupingStatistics);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/Trigger");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.StoredProc:
                    mnu.Items.Add("Modify", null, OnModifyClick);
                    mnu.Items.Add("Modify (Append to current)", null, OnModifyInCurrentScriptEditorClick);
                    mnu.Items.Add("Execute", null, OnExecuteClick);
                    mnu.Items.Add("Change History", null, OnShowObjectChangeHistory);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Drop Object", null, OnDropObject);
                    mnu.Items.Add("Generate Drop Script", null, OnGenerateDropScript);

                    mnu.Items.Add("View References", null, OnAnyReferenceClick);
                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Diff", null, null);
                    item.DropDownItems.Add("As Source", null, OnDiffAsSourceClick);
                    item.DropDownItems.Add("As Dest", null, OnDiffAsDestClick);

                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Object Help", null, OnObjectHelpClick);
                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Add Object To Group", null, OnAddSerlectedObjectsToGroup);
                    mnu.Items.Add("Show Grouping Statistics", null, OnShowObjectGroupingStatistics);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/StoredProcedure");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.Synonym:
                    mnu.Items.Add("Synonym Properties", null, OnShowSynonymProperties);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Drop Object", null, OnDropObject);
                    mnu.Items.Add("Generate Drop Script", null, OnGenerateDropScript);

                    mnu.Items.Add("View References", null, OnAnyReferenceClick);
                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Diff", null, null);
                    item.DropDownItems.Add("As Source", null, OnDiffAsSourceClick);
                    item.DropDownItems.Add("As Dest", null, OnDiffAsDestClick);

                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Object Help", null, OnObjectHelpClick);
                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Add Object To Group", null, OnAddSerlectedObjectsToGroup);
                    mnu.Items.Add("Show Grouping Statistics", null, OnShowObjectGroupingStatistics);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/Synonym");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.TableValuedFunction:
                    mnu.Items.Add("Modify", null, OnModifyClick);
                    mnu.Items.Add("Modify (Append to current)", null, OnModifyInCurrentScriptEditorClick);
                    mnu.Items.Add("Change History", null, OnShowObjectChangeHistory);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Drop Object", null, OnDropObject);
                    mnu.Items.Add("Generate Drop Script", null, OnGenerateDropScript);

                    mnu.Items.Add("View References", null, OnAnyReferenceClick);
                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Diff", null, null);
                    item.DropDownItems.Add("As Source", null, OnDiffAsSourceClick);
                    item.DropDownItems.Add("As Dest", null, OnDiffAsDestClick);

                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Object Help", null, OnObjectHelpClick);
                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Add Object To Group", null, OnAddSerlectedObjectsToGroup);
                    mnu.Items.Add("Show Grouping Statistics", null, OnShowObjectGroupingStatistics);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/TableValuedFunction");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                case DBObjectType.ScalarValuedFunction:
                    mnu.Items.Add("Modify", null, OnModifyClick);
                    mnu.Items.Add("Modify (Append to current)", null, OnModifyInCurrentScriptEditorClick);
                    mnu.Items.Add("Change History", null, OnShowObjectChangeHistory);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Drop Object", null, OnDropObject);
                    mnu.Items.Add("Generate Drop Script", null, OnGenerateDropScript);

                    mnu.Items.Add("View References", null, OnAnyReferenceClick);
                    mnu.Items.Add("-", null, null);

                    item = (ToolStripMenuItem)mnu.Items.Add("Diff", null, null);
                    item.DropDownItems.Add("As Source", null, OnDiffAsSourceClick);
                    item.DropDownItems.Add("As Dest", null, OnDiffAsDestClick);

                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Object Help", null, OnObjectHelpClick);
                    mnu.Items.Add("Dependencies", null, OnHelpDependsClick);
                    mnu.Items.Add("Permissions", null, OnHelpObjectPermissionClick);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("Add Object To Group", null, OnAddSerlectedObjectsToGroup);
                    mnu.Items.Add("Show Grouping Statistics", null, OnShowObjectGroupingStatistics);
                    mnu.Items.Add("-", null, null);
                    mnu.Items.Add("New Script", null, OnCreateScriptEditorClick);
                    mnu.Items.Add("Refresh", null, OnRefreshNodeClick);
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu/ScalarValuedFunction");
                    MenuService.AddItemsToMenu(mnu.Items, this, "/Workspace/ObjectExplorer/ContextMenu");
                    break;
                default:
                    break;
            }

            if (mnu != null)
            {
                _contextMenus.Add(objectType, mnu);
            }

            return mnu;
        }

        void mnu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //AddInTree.BuildItems("/Workspace/ObjectExplorer/ContextMenu",this,false);
        }

        #endregion //Build Node Context Menu

        #region General Node ContextMenu Item Actions

        private void OnNewConnectionClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.NewConnection);
            CreateNewConnection(false);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.NewConnection);
        }

        private void OnNewConnectionFromRepositoryClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.NewConnectionFromList);
            CreateNewConnectionFromRepository(false);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.NewConnectionFromList);
        }

        private void OnDisconnectClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.Disconnect);
            DisconnectFromServer(SelectedNode);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.Disconnect);
        }

        private void OnRefreshNodeClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.RefreshNode);
            LoadNodeData(SelectedNode, true);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.RefreshNode);
        }

        private void OnDiffAsSourceClick(object sender, EventArgs e)
        {
            if (!IsSelectedObjectScriptable())
            {
                return;
            }
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.DiffAsSource);
            SendSelectedObjectToDiff(true);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.DiffAsSource);
        }

        private void OnDiffAsDestClick(object sender, EventArgs e)
        {
            if (!IsSelectedObjectScriptable())
            {
                return;
            }

            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.DiffAsDest);
            SendSelectedObjectToDiff(false);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.DiffAsDest);
        }

        private void OnModifyClick(object sender, EventArgs e)
        {
            if (!IsSelectedObjectScriptable())
            {
                return;
            }

            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ModifyInNewEditor);
            ModifySelectedObjectInScriptWindow();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ModifyInNewEditor);
        }

        private void OnShowObjectChangeHistory(object sender, EventArgs e)
        {
            if (!IsSelectedObjectScriptable())
            {
                return;
            }

            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectChangeHistory);
            ShowObjectChangeHistoryOfSelectedObject();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectChangeHistory);
        }

        private void OnModifyInCurrentScriptEditorClick(object sender, EventArgs e)
        {
            if (!IsSelectedObjectScriptable())
            {
                return;
            }

            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ModifyInCurrentEditor);
            ModifySelectedObjectInCurrentScriptWindow();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ModifyInCurrentEditor);
        }


        private void OnExecuteClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.Execute);
            ExecuteSelectedObjectInScriptWindow();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.Execute);
        }

        private void OnViewColumnsClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ViewColumns);
            ViewColumnsOfSelectedObjectInScriptWindow();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ViewColumns);
        }

        private void OnGenerateDropScript(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.GenerateDropScript);
            GenerateObjectDropScriptToScriptWindow();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.GenerateDropScript);
        }

      
        private void OnGenerateCrudProcedures(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.GenerateCrudProcs);
            GenerateTableCrudProceduresToScriptWindow();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.GenerateCrudProcs);
        }

        private void OnDropObject(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.DeleteSelectedObjects);
            DropSelectedObjects(true);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.DeleteSelectedObjects);
        }

        #endregion //General Node ContextMenu Item Actions

        #region GoTo Reference Action

        private void OnAnyReferenceClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.References);

            TreeNode node = SelectedNode;
            if (node == null)
            {
                return;
            }
            node = SelectedNode;
            if (node == null)
            {
                return;
            }

            NodeData data = NodeDataFactory.GetNodeData(node.Tag);

            frmObjectReferencesViewer frm = ObjectReferenceViewerFactory.CreateObjectReferencesViewer("References {" + data.Name + "}", data.Name, data.ConnParams, data.DBName, RefDetail.Any, true);
            ObjectReferenceViewerFactory.ShowViewer(frm);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.References);
        }

        #endregion

        #region Show Synonym Info
        private void OnShowSynonymProperties(object sender, EventArgs args)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ShowSynonymInfo);
            ShowSynonymInfo(true);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ShowSynonymInfo);
        }
        #endregion //Show Synonym Info

        #region Object Help Actions
        private void OnHelpPermissionClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.Permissions);

            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_table_privileges", "PERMISSIONS", true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.Permissions);
        }

        private void OnHelpTableColumnPermissionClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.TableColumnPermissions);
            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_column_privileges", "COLUMN PERMISSIONS", true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.TableColumnPermissions);
        }

        private void OnHelpColumnPermissionClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ColumnPermissions);
            frmScriptEditor editor = CreateScriptEditorForColumnPermissions("COLUMN PERMISSIONS", true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ColumnPermissions);
        }

        private void OnHelpForeignKeyInClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ForeignKeysIn);
            NodeData data = SelectedNode != null ? NodeDataFactory.GetNodeData(SelectedNode.Tag) : null;
            if (data == null)
                return;

            string[] parameters = new string[2] { String.Format("@pktable_name = N'{0}'", data.Name), String.Format("@pktable_owner = N'{0}'", data.Owner) };

            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_fkeys ", "FOREIGN_KEY_IN", true, true, parameters);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ForeignKeysIn);
        }

        private void OnHelpForeignKeysClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ForeignKeys);
            NodeData data = SelectedNode != null ? NodeDataFactory.GetNodeData(SelectedNode.Tag) : null;
            if (data == null)
                return;

            string[] parameters = new string[2] { String.Format("@fktable_name = N'{0}'", data.Name), String.Format("@fktable_owner = N'{0}'", data.Owner) };
            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_fkeys ", "FOREIGN_KEYS", true, true, parameters);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ForeignKeys);
        }

        private void OnHelpConstraintsClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.Contraints);
            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_helpconstraint", "CONSTRAINTS", true, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.Contraints);
        }

        private void OnHelpSpecialColumnsClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.SpecialColumns);

            NodeData data = SelectedNode != null ? NodeDataFactory.GetNodeData(SelectedNode.Tag) : null;
            if (data == null)
                return;

            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_special_columns", "SPECIAL_COLS", true, data.Name, data.Owner);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.SpecialColumns);
        }

        private void OnHelpUsedSpaceClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.UsedSpace);
            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_spaceused", "USED_SPACE", true, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.UsedSpace);
        }

        private void OnHelpStatisticsClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.Statistics);

            NodeData data = SelectedNode != null ? NodeDataFactory.GetNodeData(SelectedNode.Tag) : null;
            if (data == null)
                return;

            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_statistics", "STATISTICS", true, data.Name, data.Owner);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.Statistics);
        }

        private void OnHelpDependsClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.Dependencies);

            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_depends", "DEPENDENCIES", true, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.Dependencies);
        }

        private void OnHelpObjectPermissionClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ObjectPermissions);
            frmScriptEditor editor = CreateObjectPermissionsScriptEditor(true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ObjectPermissions);
        }


        private void OnObjectHelpClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ObjectHelp);
            frmScriptEditor editor = CreateScriptEditorForObjectInfo("sp_help", "OBJECT HELP", true, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Help;
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ObjectHelp);
        }

        private void OnCreateScriptEditorClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.NewScript);
            CreateScriptEditor();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.NewScript);
        }


        #endregion //Object Help Actions

        #region Show DataEditor Actions

        private void OnShowObjectGroupingStatistics(object sender, EventArgs e)
        {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif 
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectGroupingStatistics);

            NodeData data = GetCurrentSelectedNodeData();
            if (data == null)
            {
                return;
            }

            ObjectGroupingService svc = new ObjectGroupingService();
            svc.ConnParams = data.ConnParams.CreateCopy();
            svc.ConnParams.Database = data.DBName;
            svc.ConnParams.Server = data.ServerName;
            if (!svc.IsObjectGroupingSupportInstalled())
            {
                MessageBox.Show("Object grouping not installed to this database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string caption = "Obj. Grp. Stat. for " + data.Name + " {" + data.DBName + " on " + data.ServerName + "}";
            string script = String.Format(PragmaSQL.Core.ResManager.GetDBScript("Script_ObjectGroupingSupportGroupStats"), data.Name);
            frmDataViewer editor = DataViewerFactory.CreateDataViewer(data, caption, script, true, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.library_icon;
            DataViewerFactory.ShowDataViewer(editor);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectGroupingStatistics);
        }

        private void OnSelectTop100RowsClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.SelectTop100Rows);

            NodeData data = GetCurrentSelectedNodeData();
            if (data == null)
            {
                return;
            }

            var isTable = (data?.Type ?? DBObjectType.None) == DBObjectType.UserTable
              || (data?.Type ?? DBObjectType.None) == DBObjectType.SystemTable
              || (data?.Type ?? DBObjectType.None) == DBObjectType.View;


            if (!isTable)
            {
                return;
            }
            GenerateSelectTop100ScriptToScriptWindow();

            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.SelectTop100Rows);
        }


        private void OnOpenTableDataEditorClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.TableDataEditor);

            NodeData data = GetCurrentSelectedNodeData();
            if (data == null)
            {
                return;
            }

            if ((data.Type != DBObjectType.UserTable) && (data.Type != DBObjectType.SystemTable))
            {
                return;
            }

            string caption = data.QualifiedFullName + " [ " + data.DBName + " on " + data.ServerName + " ]";
            string script = "select * from " + data.QualifiedFullName;
            frmDataViewer editor = DataViewerFactory.CreateDataViewer(data, caption, script, false, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.table;
            DataViewerFactory.ShowDataViewer(editor);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.TableDataEditor);
        }

        private void OnOpenViewDataEditorClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ViewDataEditor);

            NodeData data = GetCurrentSelectedNodeData();
            if (data == null)
            {
                return;
            }

            if (data.Type != DBObjectType.View)
            {
                return;
            }

            string caption = data.QualifiedFullName + " [ " + data.DBName + " on " + data.ServerName + " ]";
            string script = "select * from " + data.QualifiedFullName;
            frmDataViewer editor = DataViewerFactory.CreateDataViewer(data, caption, script, true, true);
            editor.Icon = global::PragmaSQL.Properties.Resources.Preview;
            DataViewerFactory.ShowDataViewer(editor);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ViewDataEditor);
        }

        #endregion //Show DataEditor Actions

        #region Filter
        private void OnFilterClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.Filter);
            ShowFilterDialog(SelectedNode);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.Filter);
        }

        private void OnClearFilterClick(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ClearFilter);
            ClearNodeFilter(SelectedNode);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ClearFilter);
        }

        #endregion //Filter

        #region  Other
        private void OnOpenFileInNewScriptEditor(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.OpenFile);
            OpenFileInNewScriptEditor();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.OpenFile);
        }

        private void OnCreateDatabaseSearchForm(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ShowDatabaseSearch);
            CreateDatabaseSearchForm();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ShowDatabaseSearch);
        }

        private void OnCreateObjectGroupingForm(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectGrouping);
            CreateObjectGroupingForm();
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectGrouping);
        }

        private void OnShowObjectChangeHistoryViewer(object sender, EventArgs e)
        {
            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectChangeHistoryViewer);
            ShowObjectChangeHistoryViewer(SelectedNode);
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.ShowObjectChangeHistoryViewer);
        }

        private void OnAddSerlectedObjectsToGroup(object sender, EventArgs e)
        {
            if (tv.SelNodes.Count == 0)
                return;

            FireBeforeContextMenuActionExecuted(sender, ObjectExplorerAction.AddObjectsToGroup);
            ConnectionParams cp = null;
            IList<TreeNode> nodes = new List<TreeNode>();
            foreach (MWTreeNodeWrapper nodeWrapper in tv.SelNodes.Values)
            {
                if (cp == null)
                {
                    NodeData data = NodeDataFactory.GetNodeData(nodeWrapper.Node.Tag);
                    cp = data.ConnParams.CreateCopy();
                    cp.Database = data.DBName;
                }
                nodes.Add(nodeWrapper.Node);
            }
            frmObjGroupDlg.ShowObjectGroupingDlg(cp, nodes, "Add To Object Group");
            FireAfterContextMenuActionExecuted(sender, ObjectExplorerAction.AddObjectsToGroup);
        }

        #endregion //Other
    }
}
