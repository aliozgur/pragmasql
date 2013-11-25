namespace PragmaSQL.AddIns.SampleAddIn
{
  partial class frmSampleAddIn
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent( )
    {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSampleAddIn));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showMessagesFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.webBrowserNavigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.webBrowserOpenFile1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.webBrowserOpenFile2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.clearMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getCurrentEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleEditorCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.getContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getWordAtCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appendContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.toggleReadOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptEditorTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.addMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearOutputPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getObjectTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.inspectServersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.databasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insepectDataSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectionStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.objectInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.objectExplorerTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showObjectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectedNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectedNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuItemExecCommand = new System.Windows.Forms.ToolStripMenuItem();
			this.projectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showProjectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectedNodeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.selectedNodesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuItemProjectExplorerCommand = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
			this.addButtonToAddInStripToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addLabelFromAddInStripToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsTab = true;
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(7, 28);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(587, 342);
			this.textBox1.TabIndex = 1;
			this.textBox1.WordWrap = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.otherToolStripMenuItem,
            this.testsToolStripMenuItem,
            this.scriptEditorTestToolStripMenuItem,
            this.objectExplorerTestsToolStripMenuItem,
            this.projectExplorerToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(602, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// otherToolStripMenuItem
			// 
			this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMessagesFormToolStripMenuItem,
            this.webBrowserNavigateToolStripMenuItem,
            this.webBrowserOpenFile1ToolStripMenuItem,
            this.webBrowserOpenFile2ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.clearMessageToolStripMenuItem,
            this.getCurrentEditorToolStripMenuItem,
            this.toggleEditorCursorToolStripMenuItem});
			this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
			this.otherToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.otherToolStripMenuItem.Text = "Other";
			// 
			// showMessagesFormToolStripMenuItem
			// 
			this.showMessagesFormToolStripMenuItem.Name = "showMessagesFormToolStripMenuItem";
			this.showMessagesFormToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.showMessagesFormToolStripMenuItem.Text = "Show Messages Form";
			this.showMessagesFormToolStripMenuItem.Click += new System.EventHandler(this.showMessagesFormToolStripMenuItem_Click);
			// 
			// webBrowserNavigateToolStripMenuItem
			// 
			this.webBrowserNavigateToolStripMenuItem.Name = "webBrowserNavigateToolStripMenuItem";
			this.webBrowserNavigateToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.webBrowserNavigateToolStripMenuItem.Text = "WebBrowser Navigate";
			this.webBrowserNavigateToolStripMenuItem.Click += new System.EventHandler(this.webBrowserNavigateToolStripMenuItem_Click);
			// 
			// webBrowserOpenFile1ToolStripMenuItem
			// 
			this.webBrowserOpenFile1ToolStripMenuItem.Name = "webBrowserOpenFile1ToolStripMenuItem";
			this.webBrowserOpenFile1ToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.webBrowserOpenFile1ToolStripMenuItem.Text = "WebBrowser OpenFile1";
			this.webBrowserOpenFile1ToolStripMenuItem.Click += new System.EventHandler(this.webBrowserOpenFile1ToolStripMenuItem_Click);
			// 
			// webBrowserOpenFile2ToolStripMenuItem
			// 
			this.webBrowserOpenFile2ToolStripMenuItem.Name = "webBrowserOpenFile2ToolStripMenuItem";
			this.webBrowserOpenFile2ToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.webBrowserOpenFile2ToolStripMenuItem.Text = "WebBrowser OpenFile2";
			this.webBrowserOpenFile2ToolStripMenuItem.Click += new System.EventHandler(this.webBrowserOpenFile2ToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 6);
			// 
			// clearMessageToolStripMenuItem
			// 
			this.clearMessageToolStripMenuItem.Name = "clearMessageToolStripMenuItem";
			this.clearMessageToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.clearMessageToolStripMenuItem.Text = "Clear Test Message";
			this.clearMessageToolStripMenuItem.Click += new System.EventHandler(this.clearMessageToolStripMenuItem_Click);
			// 
			// getCurrentEditorToolStripMenuItem
			// 
			this.getCurrentEditorToolStripMenuItem.Name = "getCurrentEditorToolStripMenuItem";
			this.getCurrentEditorToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.getCurrentEditorToolStripMenuItem.Text = "Get Current Editor";
			this.getCurrentEditorToolStripMenuItem.Click += new System.EventHandler(this.getCurrentEditorToolStripMenuItem_Click);
			// 
			// toggleEditorCursorToolStripMenuItem
			// 
			this.toggleEditorCursorToolStripMenuItem.Name = "toggleEditorCursorToolStripMenuItem";
			this.toggleEditorCursorToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.toggleEditorCursorToolStripMenuItem.Text = "Toggle Editor Cursor";
			this.toggleEditorCursorToolStripMenuItem.Click += new System.EventHandler(this.toggleEditorCursorToolStripMenuItem_Click);
			// 
			// testsToolStripMenuItem
			// 
			this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveToFileToolStripMenuItem,
            this.toolStripMenuItem2,
            this.getContentToolStripMenuItem,
            this.setContentToolStripMenuItem,
            this.getWordAtCursorToolStripMenuItem,
            this.getSelectionToolStripMenuItem,
            this.clearSelectionToolStripMenuItem,
            this.removeSelectionToolStripMenuItem,
            this.insertContentToolStripMenuItem,
            this.appendContentToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toggleReadOnlyToolStripMenuItem,
            this.addButtonToolStripMenuItem,
            this.addLabelToolStripMenuItem,
            this.toolStripMenuItem9,
            this.addButtonToAddInStripToolStripMenuItem,
            this.addLabelFromAddInStripToolStripMenuItem});
			this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
			this.testsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
			this.testsToolStripMenuItem.Text = "TextEditor ";
			// 
			// openFileToolStripMenuItem
			// 
			this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
			this.openFileToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.openFileToolStripMenuItem.Text = "Open File";
			this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
			// 
			// saveToFileToolStripMenuItem
			// 
			this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
			this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.saveToFileToolStripMenuItem.Text = "Save To File";
			this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(208, 6);
			// 
			// getContentToolStripMenuItem
			// 
			this.getContentToolStripMenuItem.Name = "getContentToolStripMenuItem";
			this.getContentToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.getContentToolStripMenuItem.Tag = "getcontent";
			this.getContentToolStripMenuItem.Text = "GetContent";
			this.getContentToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// setContentToolStripMenuItem
			// 
			this.setContentToolStripMenuItem.Name = "setContentToolStripMenuItem";
			this.setContentToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.setContentToolStripMenuItem.Tag = "setcontent";
			this.setContentToolStripMenuItem.Text = "SetContent";
			this.setContentToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// getWordAtCursorToolStripMenuItem
			// 
			this.getWordAtCursorToolStripMenuItem.Name = "getWordAtCursorToolStripMenuItem";
			this.getWordAtCursorToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.getWordAtCursorToolStripMenuItem.Tag = "getwordatcursor";
			this.getWordAtCursorToolStripMenuItem.Text = "Get WordAtCursor";
			this.getWordAtCursorToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// getSelectionToolStripMenuItem
			// 
			this.getSelectionToolStripMenuItem.Name = "getSelectionToolStripMenuItem";
			this.getSelectionToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.getSelectionToolStripMenuItem.Tag = "getselectedtext";
			this.getSelectionToolStripMenuItem.Text = "Get SelectedText";
			this.getSelectionToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// clearSelectionToolStripMenuItem
			// 
			this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
			this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.clearSelectionToolStripMenuItem.Tag = "clearselection";
			this.clearSelectionToolStripMenuItem.Text = "Clear Selection";
			this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// removeSelectionToolStripMenuItem
			// 
			this.removeSelectionToolStripMenuItem.Name = "removeSelectionToolStripMenuItem";
			this.removeSelectionToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.removeSelectionToolStripMenuItem.Tag = "deleteselection";
			this.removeSelectionToolStripMenuItem.Text = "Delete Selection";
			this.removeSelectionToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// insertContentToolStripMenuItem
			// 
			this.insertContentToolStripMenuItem.Name = "insertContentToolStripMenuItem";
			this.insertContentToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.insertContentToolStripMenuItem.Tag = "insertcontent";
			this.insertContentToolStripMenuItem.Text = "Insert Content";
			this.insertContentToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// appendContentToolStripMenuItem
			// 
			this.appendContentToolStripMenuItem.Name = "appendContentToolStripMenuItem";
			this.appendContentToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.appendContentToolStripMenuItem.Tag = "appendcontent";
			this.appendContentToolStripMenuItem.Text = "Append Content";
			this.appendContentToolStripMenuItem.Click += new System.EventHandler(this.OnContentRelatedActionClick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(208, 6);
			// 
			// toggleReadOnlyToolStripMenuItem
			// 
			this.toggleReadOnlyToolStripMenuItem.Name = "toggleReadOnlyToolStripMenuItem";
			this.toggleReadOnlyToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.toggleReadOnlyToolStripMenuItem.Text = "Toggle ReadOnly";
			this.toggleReadOnlyToolStripMenuItem.Click += new System.EventHandler(this.toggleReadOnlyToolStripMenuItem_Click);
			// 
			// addButtonToolStripMenuItem
			// 
			this.addButtonToolStripMenuItem.Name = "addButtonToolStripMenuItem";
			this.addButtonToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.addButtonToolStripMenuItem.Text = "Add Button To Notification";
			this.addButtonToolStripMenuItem.Click += new System.EventHandler(this.addButtonToolStripMenuItem_Click);
			// 
			// addLabelToolStripMenuItem
			// 
			this.addLabelToolStripMenuItem.Name = "addLabelToolStripMenuItem";
			this.addLabelToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.addLabelToolStripMenuItem.Text = "Add Label To Notification";
			this.addLabelToolStripMenuItem.Click += new System.EventHandler(this.addLabelToolStripMenuItem_Click);
			// 
			// scriptEditorTestToolStripMenuItem
			// 
			this.scriptEditorTestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editObjectToolStripMenuItem,
            this.toolStripMenuItem6,
            this.addMessageToolStripMenuItem,
            this.clearMessagesToolStripMenuItem,
            this.clearResultsToolStripMenuItem,
            this.clearOutputPanelToolStripMenuItem,
            this.getObjectTypeToolStripMenuItem,
            this.toolStripMenuItem5,
            this.inspectServersToolStripMenuItem,
            this.databasesToolStripMenuItem,
            this.insepectDataSetsToolStripMenuItem,
            this.connectionStringToolStripMenuItem,
            this.toolStripMenuItem7,
            this.objectInfoToolStripMenuItem});
			this.scriptEditorTestToolStripMenuItem.Name = "scriptEditorTestToolStripMenuItem";
			this.scriptEditorTestToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
			this.scriptEditorTestToolStripMenuItem.Text = "ScriptEditor ";
			// 
			// editObjectToolStripMenuItem
			// 
			this.editObjectToolStripMenuItem.Name = "editObjectToolStripMenuItem";
			this.editObjectToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.editObjectToolStripMenuItem.Text = "Edit Object";
			this.editObjectToolStripMenuItem.Click += new System.EventHandler(this.editObjectToolStripMenuItem_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(173, 6);
			// 
			// addMessageToolStripMenuItem
			// 
			this.addMessageToolStripMenuItem.Name = "addMessageToolStripMenuItem";
			this.addMessageToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.addMessageToolStripMenuItem.Text = "Add Message";
			this.addMessageToolStripMenuItem.Click += new System.EventHandler(this.addMessageToolStripMenuItem_Click);
			// 
			// clearMessagesToolStripMenuItem
			// 
			this.clearMessagesToolStripMenuItem.Name = "clearMessagesToolStripMenuItem";
			this.clearMessagesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.clearMessagesToolStripMenuItem.Text = "Clear Messages";
			this.clearMessagesToolStripMenuItem.Click += new System.EventHandler(this.clearMessagesToolStripMenuItem_Click);
			// 
			// clearResultsToolStripMenuItem
			// 
			this.clearResultsToolStripMenuItem.Name = "clearResultsToolStripMenuItem";
			this.clearResultsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.clearResultsToolStripMenuItem.Text = "Clear Results";
			this.clearResultsToolStripMenuItem.Click += new System.EventHandler(this.clearResultsToolStripMenuItem_Click);
			// 
			// clearOutputPanelToolStripMenuItem
			// 
			this.clearOutputPanelToolStripMenuItem.Name = "clearOutputPanelToolStripMenuItem";
			this.clearOutputPanelToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.clearOutputPanelToolStripMenuItem.Text = "Clear Output Panel";
			this.clearOutputPanelToolStripMenuItem.Click += new System.EventHandler(this.clearOutputPanelToolStripMenuItem_Click);
			// 
			// getObjectTypeToolStripMenuItem
			// 
			this.getObjectTypeToolStripMenuItem.Name = "getObjectTypeToolStripMenuItem";
			this.getObjectTypeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.getObjectTypeToolStripMenuItem.Text = "Get ObjectType";
			this.getObjectTypeToolStripMenuItem.Click += new System.EventHandler(this.getObjectTypeToolStripMenuItem_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(173, 6);
			// 
			// inspectServersToolStripMenuItem
			// 
			this.inspectServersToolStripMenuItem.Name = "inspectServersToolStripMenuItem";
			this.inspectServersToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.inspectServersToolStripMenuItem.Text = "Inspect Servers";
			this.inspectServersToolStripMenuItem.Click += new System.EventHandler(this.inspectServersToolStripMenuItem_Click);
			// 
			// databasesToolStripMenuItem
			// 
			this.databasesToolStripMenuItem.Name = "databasesToolStripMenuItem";
			this.databasesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.databasesToolStripMenuItem.Text = "Inspect Databases";
			this.databasesToolStripMenuItem.Click += new System.EventHandler(this.databasesToolStripMenuItem_Click);
			// 
			// insepectDataSetsToolStripMenuItem
			// 
			this.insepectDataSetsToolStripMenuItem.Name = "insepectDataSetsToolStripMenuItem";
			this.insepectDataSetsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.insepectDataSetsToolStripMenuItem.Text = "Insepect DataSets";
			this.insepectDataSetsToolStripMenuItem.Click += new System.EventHandler(this.insepectDataSetsToolStripMenuItem_Click);
			// 
			// connectionStringToolStripMenuItem
			// 
			this.connectionStringToolStripMenuItem.Name = "connectionStringToolStripMenuItem";
			this.connectionStringToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.connectionStringToolStripMenuItem.Text = "Connection String";
			this.connectionStringToolStripMenuItem.Click += new System.EventHandler(this.connectionStringToolStripMenuItem_Click);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(173, 6);
			// 
			// objectInfoToolStripMenuItem
			// 
			this.objectInfoToolStripMenuItem.Name = "objectInfoToolStripMenuItem";
			this.objectInfoToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.objectInfoToolStripMenuItem.Text = "Object Info";
			this.objectInfoToolStripMenuItem.Click += new System.EventHandler(this.objectInfoToolStripMenuItem_Click);
			// 
			// objectExplorerTestsToolStripMenuItem
			// 
			this.objectExplorerTestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showObjectExplorerToolStripMenuItem,
            this.selectedNodeToolStripMenuItem,
            this.selectedNodesToolStripMenuItem,
            this.toolStripMenuItem8,
            this.mnuItemExecCommand});
			this.objectExplorerTestsToolStripMenuItem.Name = "objectExplorerTestsToolStripMenuItem";
			this.objectExplorerTestsToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
			this.objectExplorerTestsToolStripMenuItem.Text = "ObjectExplorer ";
			// 
			// showObjectExplorerToolStripMenuItem
			// 
			this.showObjectExplorerToolStripMenuItem.Name = "showObjectExplorerToolStripMenuItem";
			this.showObjectExplorerToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.showObjectExplorerToolStripMenuItem.Text = "Show ObjectExplorer";
			this.showObjectExplorerToolStripMenuItem.Click += new System.EventHandler(this.showObjectExplorerToolStripMenuItem_Click);
			// 
			// selectedNodeToolStripMenuItem
			// 
			this.selectedNodeToolStripMenuItem.Name = "selectedNodeToolStripMenuItem";
			this.selectedNodeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.selectedNodeToolStripMenuItem.Text = "Selected Node";
			this.selectedNodeToolStripMenuItem.Click += new System.EventHandler(this.selectedNodeToolStripMenuItem_Click);
			// 
			// selectedNodesToolStripMenuItem
			// 
			this.selectedNodesToolStripMenuItem.Name = "selectedNodesToolStripMenuItem";
			this.selectedNodesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.selectedNodesToolStripMenuItem.Text = "Selected Nodes";
			this.selectedNodesToolStripMenuItem.Click += new System.EventHandler(this.selectedNodesToolStripMenuItem_Click);
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(183, 6);
			// 
			// mnuItemExecCommand
			// 
			this.mnuItemExecCommand.Name = "mnuItemExecCommand";
			this.mnuItemExecCommand.Size = new System.Drawing.Size(186, 22);
			this.mnuItemExecCommand.Text = "Execute Command";
			this.mnuItemExecCommand.Click += new System.EventHandler(this.executeCommanToolStripMenuItem_Click);
			// 
			// projectExplorerToolStripMenuItem
			// 
			this.projectExplorerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showProjectExplorerToolStripMenuItem,
            this.selectedNodeToolStripMenuItem1,
            this.selectedNodesToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.mnuItemProjectExplorerCommand});
			this.projectExplorerToolStripMenuItem.Name = "projectExplorerToolStripMenuItem";
			this.projectExplorerToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
			this.projectExplorerToolStripMenuItem.Text = "ProjectExplorer";
			// 
			// showProjectExplorerToolStripMenuItem
			// 
			this.showProjectExplorerToolStripMenuItem.Name = "showProjectExplorerToolStripMenuItem";
			this.showProjectExplorerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.showProjectExplorerToolStripMenuItem.Text = "Show ProjectExplorer";
			this.showProjectExplorerToolStripMenuItem.Click += new System.EventHandler(this.showProjectExplorerToolStripMenuItem_Click);
			// 
			// selectedNodeToolStripMenuItem1
			// 
			this.selectedNodeToolStripMenuItem1.Name = "selectedNodeToolStripMenuItem1";
			this.selectedNodeToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
			this.selectedNodeToolStripMenuItem1.Text = "Selected Node";
			this.selectedNodeToolStripMenuItem1.Click += new System.EventHandler(this.selectedNodeToolStripMenuItem1_Click);
			// 
			// selectedNodesToolStripMenuItem1
			// 
			this.selectedNodesToolStripMenuItem1.Name = "selectedNodesToolStripMenuItem1";
			this.selectedNodesToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
			this.selectedNodesToolStripMenuItem1.Text = "Selected Nodes";
			this.selectedNodesToolStripMenuItem1.Click += new System.EventHandler(this.selectedNodesToolStripMenuItem1_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(185, 6);
			// 
			// mnuItemProjectExplorerCommand
			// 
			this.mnuItemProjectExplorerCommand.Name = "mnuItemProjectExplorerCommand";
			this.mnuItemProjectExplorerCommand.Size = new System.Drawing.Size(188, 22);
			this.mnuItemProjectExplorerCommand.Text = "Perform Action";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(208, 6);
			// 
			// addButtonToAddInStripToolStripMenuItem
			// 
			this.addButtonToAddInStripToolStripMenuItem.Name = "addButtonToAddInStripToolStripMenuItem";
			this.addButtonToAddInStripToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.addButtonToAddInStripToolStripMenuItem.Text = "Add Button To AddIn Strip";
			this.addButtonToAddInStripToolStripMenuItem.Click += new System.EventHandler(this.addButtonToAddInStripToolStripMenuItem_Click);
			// 
			// addLabelFromAddInStripToolStripMenuItem
			// 
			this.addLabelFromAddInStripToolStripMenuItem.Name = "addLabelFromAddInStripToolStripMenuItem";
			this.addLabelFromAddInStripToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.addLabelFromAddInStripToolStripMenuItem.Text = "Add Label To AddIn Strip";
			this.addLabelFromAddInStripToolStripMenuItem.Click += new System.EventHandler(this.addLabelFromAddInStripToolStripMenuItem_Click);
			// 
			// frmSampleAddIn
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(602, 375);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSampleAddIn";
			this.TabText = "Sample AddIn";
			this.Text = "Sample AddIn";
			this.Load += new System.EventHandler(this.frmSampleAddIn_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSampleAddIn_FormClosed);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem getContentToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem setContentToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem getWordAtCursorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem getSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem removeSelectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem insertContentToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem appendContentToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem scriptEditorTestToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem objectExplorerTestsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editObjectToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addMessageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearMessagesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearResultsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearOutputPanelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem getObjectTypeToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    private System.Windows.Forms.ToolStripMenuItem inspectServersToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem databasesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem insepectDataSetsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem connectionStringToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
    private System.Windows.Forms.ToolStripMenuItem objectInfoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectedNodeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectedNodesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
    private System.Windows.Forms.ToolStripMenuItem mnuItemExecCommand;
    private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showMessagesFormToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem showObjectExplorerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem projectExplorerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearMessageToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem showProjectExplorerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectedNodeToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem selectedNodesToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem mnuItemProjectExplorerCommand;
    private System.Windows.Forms.ToolStripMenuItem webBrowserNavigateToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem webBrowserOpenFile1ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem webBrowserOpenFile2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getCurrentEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toggleEditorCursorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toggleReadOnlyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addButtonToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addLabelToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem addButtonToAddInStripToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addLabelFromAddInStripToolStripMenuItem;
  }
}