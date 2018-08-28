/********************************************************************
  Class DatabaseObjectType
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
    [Serializable]
    public enum ProjectItemType
    {
        Unknown = 0,
        Project = 1,
        ConnectionSpec = 2,
        Folder = 3,
        ScriptableDBObject = 4,
        NonScriptableDBObject = 5,
        ScriptFile = 6,
        ExternalFile = 7,
        TextFile = 8
    }

    public enum SharedScriptsViewerCommand
    {
        None,
        Reload,
        RefreshNode,
        NewRootFolder,
        NewSubFolder,
        NewScript,
        RenameItem,
        DeleteSelectedItems,
        SaveHelpText,
        ShowHelpText,
        HideHelpText,
        EditScript
    }

    public enum SharedSnippetsViewerCommand
    {
        None,
        Reload,
        RefreshNode,
        NewRootFolder,
        NewSubFolder,
        NewSnippet,
        RenameItem,
        DeleteSelectedItems,
        SaveDescription,
        ShowDescription,
        HideDescription,
        EditSnippet
    }

    public enum ProjectExplorerCommand
    {
        None,
        OpenProject,
        CreateProject,
        CloseProject,
        NewConnection,
        NewConnectionFromList,
        Refresh,
        RenameSelectedItem,
        ModifyConnection,
        DeleteSelectedItems,
        AddFolder,
        AddScript,
        AddText,
        AddExternalFile,
        AddObjectFromDatabase,
        EditSelectedObjects,
        EditWithExternalApp,
        OpenDatabaseObject
    }

    public enum ScriptRunType
    {
        None,
        Execute,
        CheckSyntax,
        ShowPlan
    }

    public enum ObjectExplorerCommand
    {
        CloseCurrentConnection,
        NewConnection,
        NewConnectionFromList,
        RefreshCurrentNode,
        OpenFile,
        NewScript,
        ShowFilterDialog,
        ShowDatabaseSearch,
        ShowObjectChangeHistoryViewer,
        ShowObjectGrouping
    }

    public enum SearchType
    {
        PlainText,
        Wildcards,
        RegularExpression
    }

    public enum MessageType
    {
        None = -1,
        Info = 0,
        Warning = 1,
        Error = 2,
        Debug = 3,
        Bold = 4
    }

    public enum AddInDockState
    {
        Unknown = 0,
        Float = 1,
        DockTopAutoHide = 2,
        DockLeftAutoHide = 3,
        DockBottomAutoHide = 4,
        DockRightAutoHide = 5,
        Document = 6,
        DockTop = 7,
        DockLeft = 8,
        DockBottom = 9,
        DockRight = 10,
        Hidden = 11,
    }

    public enum CodeCompletionType
    {
        UserDefinedList,
        DatabaseObjectList
    }

    public enum ObjectExplorerAction
    {
        None,
        References,
        Permissions,
        TableColumnPermissions,
        ColumnPermissions,
        ForeignKeysIn,
        ForeignKeys,
        Contraints,
        SpecialColumns,
        UsedSpace,
        Statistics,
        Dependencies,
        ObjectPermissions,
        ObjectHelp,
        NewScript,
        TableDataEditor,
        ViewDataEditor,
        NewConnection,
        NewConnectionFromList,
        Disconnect,
        RefreshNode,
        ScriptDatabaseObjects,
        DiffAsSource,
        DiffAsDest,
        ModifyInNewEditor,
        ModifyInCurrentEditor,
        Execute,
        ViewColumns,
        GenerateDropScript,
        GenerateTableCreateScript,
        GenerateCrudProcs,
        Filter,
        ClearFilter,
        OpenFile,
        ShowDatabaseSearch,
        ShowObjectChangeHistoryViewer,
        ShowObjectGrouping,
        DeleteSelectedObjects,
        ShowSynonymInfo,
        ShowObjectChangeHistory,
        AddObjectsToGroup,
        ShowObjectGroupingStatistics,
        BulkCopyData,
        SelectTop100Rows
    }

    [Serializable]
    public enum ResultViewType
    {
        GridsInOwnTabs
        , GridsInSingleTab
        //,Text
    }

    public enum ScriptCompletionStatus
    {
        None,
        Completed,
        HasErrors,
        Cancelled
    }

    public enum TableKeyEditorMode
    {
        SingleTable,
        All
    }

    public enum ScriptEditorActions
    {
        Execute,
        CheckSyntax,
        Stop,
        ToggleOutputPane,
        SearchForward,
        SearchBackward,
        Find,
        Replace,
        GoToLine,
        KeywordsToUppercase,
        KeywordsToLowercase,
        CapitalizeKeywords,

        ScriptToUppercase,
        ScriptToLowercase,

        Undo,
        Redo,

        Cut,
        Copy,
        Paste,

        Save,
        SaveAs,
        Open,
        OpenNewFromFile,
        NewScript,

        OutdentSelection,
        IndentSelection,

        CommentBlock,
        CommentLine,

        ModifySelectedObject,
        OpenSelectedObject,
        OpenProcExecScript,
        SelectTop100Rows,

        ListPermissions,
        ListReferences,
        ListDependencies,
        ObjectHelp,
        HelpOnWordAtCursor,
        MarkSelectionAsCodeBlock,
        ObjectChangeHistory,
        MultiExecute,
        IncrementalSearch,
        ReverseIncrementalSearch,
        FastScriptPreview
    }

    public enum EditorContentType
    {
        Unknown,
        Script,
        Text,
        File,
        SharedSnippet,
        SharedScript
    }

    [Serializable]
    public enum RecoverContentType
    {
        Text,
        TextFile,
        Script,
        ScriptFile,
        SharedScript,
        SharedSnippet
    }
}
