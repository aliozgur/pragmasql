using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using PragmaSQL.Core;

namespace PragmaSQL.AddIns.SampleAddIn
{
  public partial class frmSampleAddIn : DockContent
  {
    private ITextEditor _textEditor = null;
    private IScriptEditor _scriptEditor = null;
    private IObjectExplorerService _objectExplorer = null;
    private IProjectExplorerServices _projectExplorer = null;

    private IHostServices _hostServices = null;

    public frmSampleAddIn( )
    {
      InitializeComponent();
      InitializeHostServices();
      InitializeTextEditor();
      InitializeObjectExplorer();
      InitializeProjectExplorer();
    }
    
    private void InitializeHostServices( )
    {
      _hostServices = HostServicesSingleton.HostServices;
      if (_hostServices == null)
      {
        AddError("Host Services not available!");
      }

      _hostServices.ActiveContentChanged += new EventHandler(_hostServices_ActiveContentChanged);
			_hostServices.EditorServices.EditorClosed += new FormClosedEventHandler(EditorServices_EditorClosed);
			_hostServices.EditorServices.EditorClosing += new FormClosingEventHandler(EditorServices_EditorClosing);
			_hostServices.EditorServices.EditorReady += new EventHandler(EditorServices_EditorReady);
		}

		private void UnsubscribeOtherEvents()
		{
			_hostServices.ActiveContentChanged -= new EventHandler(_hostServices_ActiveContentChanged);
			_hostServices.EditorServices.EditorClosed -= new FormClosedEventHandler(EditorServices_EditorClosed);
			_hostServices.EditorServices.EditorClosing -= new FormClosingEventHandler(EditorServices_EditorClosing);
			_hostServices.EditorServices.EditorReady -= new EventHandler(EditorServices_EditorReady);
		}

		void EditorServices_EditorReady(object sender, EventArgs e)
		{
			ITextEditor edt = sender as ITextEditor;
			if (edt == null)
				return;

			AddEvent("Editor Ready:" + edt.Caption);
		}

		void EditorServices_EditorClosing(object sender, FormClosingEventArgs e)
		{
			ITextEditor edt = sender as ITextEditor;
			if (edt == null)
				return;

			if(MessageBox.Show(String.Format("Close editor: {0} ?",edt.Caption),"Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
				e.Cancel = true;
		}

		void EditorServices_EditorClosed(object sender, FormClosedEventArgs e)
		{
			ITextEditor edt = sender as ITextEditor;
			if (edt == null)
				return;

			AddEvent("Editor Closed:" + edt.Caption);
		}

    void _hostServices_ActiveContentChanged( object sender, EventArgs e )
    {
      if (sender is IScriptEditor)
      {
        if (sender == _scriptEditor)
        {
          return;
        }

        UnsubscribeFromTextEditorEvents();
        UnSubscribeFromScriptEditorEvents();

        _scriptEditor = sender as IScriptEditor;
        _textEditor = _scriptEditor;
        
        SubscribeToScriptEditorEvents();
        SubscribeToTextEditorEvents();
      }
      else if (sender is ITextEditor)
      {
        if (sender == _textEditor)
        {
          return;
        }

        UnsubscribeFromTextEditorEvents();
        UnSubscribeFromScriptEditorEvents();

        _textEditor = sender as ITextEditor;
        _scriptEditor = null;

        SubscribeToTextEditorEvents();
      }
      else if (sender is IObjectExplorerService)
      {
        if (sender == _objectExplorer)
        {
          return;
        }

        UnSubscribeFromObjectExplorerEvents();
        _objectExplorer = sender as IObjectExplorerService;
        SubscribeToObjectExplorerEvents();
      }
      else if (sender is IProjectExplorerServices)
      {
        if (sender == _projectExplorer)
        {
          return;
        }

        UnSubscribeToProjectExplorerEvents();
         _projectExplorer = sender as IProjectExplorerServices;
        SubscribeToProjectExplorerEvents();
      }
    }

    private void InitializeTextEditor( )
    {
      ClearMessages();

      if (_textEditor != null)
      {
        UnsubscribeFromTextEditorEvents();
      }
      if (_scriptEditor != null)
      {
        UnsubscribeFromTextEditorEvents();
      }

      _textEditor = GetCurrentTextEditor();
      if (_textEditor != null)
      {
        AddMessage("Text Editor:" + _textEditor.Caption);
        _scriptEditor = null;
      }
      else
      {
        _scriptEditor = GetCurrentScriptEditor();
        _textEditor = _scriptEditor;
        if (_textEditor == null)
        {
          AddError("TextEditor not available!");
        }
      }
      SubscribeToTextEditorEvents();
      SubscribeToScriptEditorEvents();
    }

    private void InitializeObjectExplorer( )
    {
      if (_objectExplorer != null)
      {
        UnSubscribeFromObjectExplorerEvents();
      }

      _objectExplorer = GetCurrentObjectExplorer();
      if (_objectExplorer == null)
      {
        AddError("ObjectExplorer is null!");
      }
      else
      {
        SubscribeToObjectExplorerEvents();
      }
    }

    private void InitializeProjectExplorer( )
    {
      if (_projectExplorer != null)
      {
        UnSubscribeToProjectExplorerEvents();
      }

      _projectExplorer = _hostServices.ProjectExplorerService;
      if (_projectExplorer == null)
      {
        AddError("ProjectExplorer is null!");
      }
      else
      {
        SubscribeToProjectExplorerEvents();
      }
    }

    private ITextEditor GetCurrentTextEditor( )
    {
      if (_hostServices == null)
      {
        return null;
      }

      IEditorService edtServices = _hostServices.EditorServices;
      if (edtServices == null)
      {
        AddError("EditorService is null!");
        return null;
      }

      ITextEditor txtEditor = edtServices.CurrentTextEditor;
      if (txtEditor == null)
      {
        AddError("Current text editor is null.");
        return null;
      }

      return txtEditor;
    }

    private IObjectExplorerService GetCurrentObjectExplorer( )
    {
      if (_hostServices == null)
      {
        return null;
      }

      return _hostServices.ObjectExplorerService;
    }

    private IScriptEditor GetCurrentScriptEditor( )
    {
      if (_hostServices == null)
      {
        return null;
      }

      IEditorService edtServices = _hostServices.EditorServices;
      if (edtServices == null)
      {
        AddError("EditorService is null!");
        return null;
      }

      IScriptEditor scriptEditor = edtServices.CurrentScriptEditor;
      if (scriptEditor == null)
      {
        AddError("Current script editor is null.");
        return null;
      }

      return scriptEditor;
    }

    private void SubscribeToTextEditorEvents( )
    {
      if (_textEditor == null)
      {
        return;
      }

      AddMessage("Subscribing to TextEditor events...");
      _textEditor.AfterOpenedFile += new AfterOpenedFileDelegate(txtEditor_AfterOpenedFile);
      _textEditor.BeforeOpenedFile += new BeforeOpenedFileDelegate(_textEditor_BeforeOpenedFile);
      _textEditor.AfterSavedContent += new AfterSavedContentDelegate(txtEditor_AfterSavedContent);
      _textEditor.BeforeSavedContent += new BeforeSavedContentDelegate(_textEditor_BeforeSavedContent);
      _textEditor.AfterCaretPositionChanged += new AfterCaretPositionChangedDelegate(txtEditor_AfterCaretPositionChanged);
      _textEditor.BeforeCaretPositionChanged += new BeforeCaretPositionChangedDelegate(_textEditor_BeforeCaretPositionChanged);
      _textEditor.AfterContentChanged += new EventHandler(txtEditor_AfterContentChanged);
      _textEditor.AfterSaveContentToFile += new AfterSavedContentToFileDelegate(txtEditor_AfterSaveContentToFile);
      _textEditor.BeforeSaveContentToFile += new BeforeSavedContentToFileDelegate(_textEditor_BeforeSaveContentToFile);
      _textEditor.BeforeCodeCompletionShowed += new BeforeCodeCompletionShowedDelegete(_textEditor_BeforeCodeCompletionShowed);
      _textEditor.AfterCodeCompletionShowed += new AfterCodeCompletionShowedDelegete(_textEditor_AfterCodeCompletionShowed);
      _textEditor.EditorClosed += new FormClosedEventHandler(_textEditor_AfterTextEditorClosed);
			_textEditor.EditorClosing += new FormClosingEventHandler(_textEditor_EditorClosing);
			AddMessage("Subscribed to TextEditor  evennts.");
    }


    private void UnsubscribeFromTextEditorEvents( )
    {
      if (_textEditor == null)
      {
        return;
      }

      AddMessage("Unsubscribing from TextEditor events...");
      _textEditor.AfterOpenedFile -= new AfterOpenedFileDelegate(txtEditor_AfterOpenedFile);
      _textEditor.BeforeOpenedFile -= new BeforeOpenedFileDelegate(_textEditor_BeforeOpenedFile);
      _textEditor.AfterSavedContent -= new AfterSavedContentDelegate(txtEditor_AfterSavedContent);
      _textEditor.BeforeSavedContent -= new BeforeSavedContentDelegate(_textEditor_BeforeSavedContent);
      _textEditor.AfterCaretPositionChanged -= new AfterCaretPositionChangedDelegate(txtEditor_AfterCaretPositionChanged);
      _textEditor.BeforeCaretPositionChanged -= new BeforeCaretPositionChangedDelegate(_textEditor_BeforeCaretPositionChanged);
      _textEditor.AfterContentChanged -= new EventHandler(txtEditor_AfterContentChanged);
      _textEditor.AfterSaveContentToFile -= new AfterSavedContentToFileDelegate(txtEditor_AfterSaveContentToFile);
      _textEditor.BeforeSaveContentToFile -= new BeforeSavedContentToFileDelegate(_textEditor_BeforeSaveContentToFile);
      _textEditor.BeforeCodeCompletionShowed -= new BeforeCodeCompletionShowedDelegete(_textEditor_BeforeCodeCompletionShowed);
      _textEditor.AfterCodeCompletionShowed -= new AfterCodeCompletionShowedDelegete(_textEditor_AfterCodeCompletionShowed);
			_textEditor.EditorClosed -= new FormClosedEventHandler(_textEditor_AfterTextEditorClosed);
			_textEditor.EditorClosing -= new FormClosingEventHandler(_textEditor_EditorClosing);

			AddMessage("Unsubscribed from TextEditor events.");
    }


    private void SubscribeToScriptEditorEvents( )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      AddMessage("Subscribing to ScriptEditor events...");
			_scriptEditor.ScriptExecuting += new ScriptExecutingDelegate(_scriptEditor_BeforeScriptExecuted);
			_scriptEditor.ScriptExecuted += new ScriptExecutedDelegate(_scriptEditor_AfterScriptExecuted);

      _scriptEditor.AfterHelpRequested += new AfterHelpRequestedDelegate(_scriptEditor_AfterHelpRequested);
      _scriptEditor.BeforeHelpRequested += new BeforeHelpRequestedDelegate(_scriptEditor_BeforeHelpRequested);

      AddMessage("Subscribed to ScriptEditor evennts.");
    }

    private void UnSubscribeFromScriptEditorEvents( )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      AddMessage("UnSubscribing to ScriptEditor events...");
			_scriptEditor.ScriptExecuting -= new ScriptExecutingDelegate(_scriptEditor_BeforeScriptExecuted);
			_scriptEditor.ScriptExecuted -= new ScriptExecutedDelegate(_scriptEditor_AfterScriptExecuted);

      _scriptEditor.AfterHelpRequested -= new AfterHelpRequestedDelegate(_scriptEditor_AfterHelpRequested);
      _scriptEditor.BeforeHelpRequested -= new BeforeHelpRequestedDelegate(_scriptEditor_BeforeHelpRequested);
      AddMessage("UnSubscribed to ScriptEditor evennts.");

    }

    private void SubscribeToObjectExplorerEvents( )
    {
      if (_objectExplorer == null)
      {
        return;
      }

      AddMessage("Subscribing to ObjectExplorer events...");
      _objectExplorer.AfterContextMenuActionExecuted += new AfterContextMenuActionExecutedDelegate(_objectExplorer_AfterActionExecuted);
      _objectExplorer.BeforeContextMenuActionExecuted += new BeforeContextMenuActionExecutedDelegate(_objectExplorer_BeforeContextMenuActionExecuted);
      _objectExplorer.AfterConnected += new AfterConnectedDelegate(_objectExplorer_AfterConnected);
      _objectExplorer.AfterDisconnected += new AfterDisconnectedDelegate(_objectExplorer_AfterDisconnected);
      _objectExplorer.AfterObjectExplorerClosed += new EventHandler(_objectExplorer_AfterObjectExplorerClosed);
      AddMessage("Subscribed to ObjectExplorer evennts.");
    }

    
    private void UnSubscribeFromObjectExplorerEvents( )
    {
      if (_objectExplorer == null)
      {
        return;
      }

      AddMessage("UnSubscribing from ObjectExplorer events...");
      _objectExplorer.AfterContextMenuActionExecuted -= new AfterContextMenuActionExecutedDelegate(_objectExplorer_AfterActionExecuted);
      _objectExplorer.BeforeContextMenuActionExecuted -= new BeforeContextMenuActionExecutedDelegate(_objectExplorer_BeforeContextMenuActionExecuted);
      _objectExplorer.AfterConnected -= new AfterConnectedDelegate(_objectExplorer_AfterConnected);
      _objectExplorer.AfterDisconnected -= new AfterDisconnectedDelegate(_objectExplorer_AfterDisconnected);
      AddMessage("UnSubscribed from ObjectExplorer evennts.");
    }

    private void SubscribeToProjectExplorerEvents( )
    {
      if (_projectExplorer == null)
      {
        return;
      }

      AddMessage("Subscribing to ProjectExplorer events...");
      _projectExplorer.AfterProjectExplorerAction += new AfterProjectExplorerActionDelegate(_projectExplorer_AfterProjectExplorerAction);
      _projectExplorer.AfterProjectExplorerClosed += new EventHandler(_projectExplorer_AfterProjectExplorerClosed);
      _projectExplorer.AfterSelectedNodesChanged += new EventHandler(_projectExplorer_AfterSelectedNodesChanged);
      _projectExplorer.BeforeProjectExplorerAction += new BeforeProjectExplorerActionDelegate(_projectExplorer_BeforeProjectExplorerAction);
      AddMessage("Subscribed to ProjectExplorer evennts.");
    }   

    private void UnSubscribeToProjectExplorerEvents( )
    {
      if (_projectExplorer == null)
      {
        return;
      }

      AddMessage("UnSubscribing to ProjectExplorer events...");
      _projectExplorer.AfterProjectExplorerAction -= new AfterProjectExplorerActionDelegate(_projectExplorer_AfterProjectExplorerAction);
      _projectExplorer.AfterProjectExplorerClosed -= new EventHandler(_projectExplorer_AfterProjectExplorerClosed);
      _projectExplorer.AfterSelectedNodesChanged -= new EventHandler(_projectExplorer_AfterSelectedNodesChanged);
      _projectExplorer.BeforeProjectExplorerAction -= new BeforeProjectExplorerActionDelegate(_projectExplorer_BeforeProjectExplorerAction);
      AddMessage("UnSubscribed to ProjectExplorer evennts.");
    }


		private void _textEditor_EditorClosing(object sender, FormClosingEventArgs e)
		{
			if (_textEditor == null || _textEditor != sender)
			{
				return;
			}

			//e.Cancel = true;

			AddMessage("TextEditorClosing.");
			
			UnsubscribeFromTextEditorEvents();
			UnSubscribeFromScriptEditorEvents();

			_textEditor = null;
			_scriptEditor = null;
		}


    private void _textEditor_AfterTextEditorClosed( object sender, FormClosedEventArgs e )
    {
      if (_textEditor == null || _textEditor != sender)
      {
        return;
      }

      AddMessage("TextEditorClosed.");
      UnsubscribeFromTextEditorEvents();
      UnSubscribeFromScriptEditorEvents();

      _textEditor = null;
      _scriptEditor = null;
    }

    void _textEditor_AfterCodeCompletionShowed( object sender, AfterCodeCompletionShowedEventArgs args )
    {
      AddMessage("TextEditor.AfterCodeCompletionShowed. Args = " + args.ToString());
    }

    void _textEditor_BeforeCodeCompletionShowed( object sender, BeforeCodeCompletionShowedEventArgs args )
    {
      AddEvent("TextEditor.BeforeCodeCompletionShowed. Args = " + args.ToString());
    }

    void txtEditor_AfterSaveContentToFile( object sender, string fileName )
    {
      AddEvent("AfterSaveContentToFile. FileName: " + fileName);
    }
    
    void _textEditor_BeforeSaveContentToFile( object sender, FileOperationEventArgs args )
    {
      AddEvent("TextEditor.BeforeSaveContentToFile. FileName = " + args.FileName);
    }


    void txtEditor_AfterContentChanged( object sender, EventArgs e )
    {
      AddEvent("AfterContentChanged.");
    }

    void txtEditor_AfterCaretPositionChanged( object sender, CaretPosition newPos )
    {
      AddEvent("AfterCaretPositionChanged. " + newPos.ToString());
    }

    void _textEditor_BeforeCaretPositionChanged( object sender)
    {
      AddEvent("TextEditor.BeforeCaretPositionChanged");
    }


    void txtEditor_AfterSavedContent( object sender, string fileName )
    {
      AddEvent("AfterSavedContent. FileName: " + fileName);
    }

    void _textEditor_BeforeSavedContent( object sender, EventArgs args )
    {
      AddEvent("TextEditor.BeforeSavedContent");
    }


    void txtEditor_AfterOpenedFile( object sender, string fileName )
    {
      AddEvent("AfterOpenedFile.FileName: " + fileName);
    }
    
    void _textEditor_BeforeOpenedFile( object sender, BeforeOpenedFileEventArgs args )
    {
      AddEvent("TextEditor.BeforeOpenedFile. FileName = " + args.FileName);
    }

    void _objectExplorer_AfterObjectExplorerClosed( object sender, EventArgs e )
    {
      if (_objectExplorer != sender)
      {
        return;
      }

      UnSubscribeFromObjectExplorerEvents();
      _objectExplorer = null;
      AddMessage("ObjectExplorer closed.");
    }
    
    void _objectExplorer_AfterDisconnected( string serverName )
    {
      AddEvent("ObjectExplorer.AfterDisconnected. ServerName = " + serverName);
    }

    void _objectExplorer_AfterConnected( string serverName, string connectionString )
    {
      AddEvent("ObjectExplorer.AfterConnected. ServerName = " + serverName + ", ConnectionString = " + connectionString);
    }

    void _objectExplorer_AfterActionExecuted( object sender, AfterContextMenuActionExecutedEventArgs args )
    {
      AddEvent("ObjectExplorer.AfterActionExecuted. Action = " + args.Action.ToString()+ ", ActionPath = " + args.ActionPath + ", SelNode:\r\n" + args.SelectedNode.ToString());
    }

    void _objectExplorer_BeforeContextMenuActionExecuted( object sender, BeforeContextMenuActionExecutedEventArgs args )
    {
      AddEvent("ObjectExplorer.BeforeContextMenuActionExecuted. Action = " + args.Action.ToString() + ", ActionPath = " + args.ActionPath + ", SelNode:\r\n" + args.SelectedNode.ToString());
    }

    void _scriptEditor_AfterScriptExecuted( object sender, ScriptExecutedEventArgs args )
    {
      AddEvent("ScriptEditor.AfterScriptExecuted. Args = " + args.ToString());
    }

    void _scriptEditor_BeforeScriptExecuted( object sender, ScriptExecutingEventArgs args )
    {
			if (MessageBox.Show("Cancel execution?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
				args.Cancel = true;

			AddEvent("ScriptEditor.BeforeScriptExecutes. Args = " + args.ToString());
    }

    void _scriptEditor_BeforeHelpRequested( object sender, HelpRequestedEventArgs args )
    {
      AddEvent("ScriptEditor.BeforeHelpRequested. Args = " + args.ToString());
    }

    void _scriptEditor_AfterHelpRequested( object sender, HelpRequestedEventArgs args)
    {
      AddEvent("ScriptEditor.AfterHelpRequested. Args = " + args.ToString());
    }

    void _projectExplorer_AfterSelectedNodesChanged( object sender, EventArgs e )
    {
      if (_projectExplorer == null)
      {
        return;
      }

      AddMessage("ProjectExplorer.AfterSelectedNodesChanged");
      if (_projectExplorer.SelectedItems != null)
      {
        AddMessage("Printing node data...");
        foreach (ProjectNodeData data in _projectExplorer.SelectedItems)
        {
          AddMessage(data.ToString());
        }
        AddMessage("COmpleted printing node data.");
      }
    }

    void _projectExplorer_AfterProjectExplorerClosed( object sender, EventArgs e )
    {
      if (_projectExplorer == null || _projectExplorer != sender)
      {
        return;
      }

      AddMessage("ProjectExplorerClosed.");
      UnSubscribeToProjectExplorerEvents();

      _projectExplorer = null;
    }

    void _projectExplorer_AfterProjectExplorerAction( object sender, ProjectExplorerEventArgs args )
    {
      AddMessage("ProjectExplorer.AfterProjectExplorerAction");
      AddMessage(" Action = " + args.Action.ToString());
      AddMessage(" Items {" + (args.Items != null ? args.Items.Count.ToString() : "0") + "} ");
    }

    void _projectExplorer_BeforeProjectExplorerAction( object sender, ProjectExplorerEventArgs args )
    {
      AddMessage("ProjectExplorer.BeforeProjectExplorerAction");
      AddMessage(" Action = " + args.Action.ToString());
      AddMessage(" Items {" + (args.Items != null ? args.Items.Count.ToString() : "0") + "} ");
    }


    #region Message Utils
    private void AddMessage( string msg )
    {
      textBox1.Text += msg + "\r\n";
    }

    private void AddError( string msg )
    {
      AddMessage("[Error] " + msg);
    }

    private void AddTest( string msg )
    {
      AddMessage("[Test] " + msg);
    }

    private void AddEvent( string msg )
    {
      AddMessage("[Event] " + msg);
    }

    private void StartTest( string msg )
    {
      AddMessage("[Test Started] " + msg);
    }

    private void CompletedTest( string msg )
    {
      AddMessage("[Test Completed] " + msg);
    }

    private void AddEmpty( )
    {
      AddMessage("");
    }

    private void ClearMessages( )
    {
      textBox1.Text = String.Empty;
    }

    #endregion //Message Utils

    #region Methods
    private void BuildObjectExplorerCommandsMenuItems( )
    {
      string[] names = Enum.GetNames(typeof(ObjectExplorerCommand));
      foreach (string cmd in names)
      {
        mnuItemExecCommand.DropDown.Items.Add(cmd, null, OnObjectExplorerCommand);
      }
    }
    
    private void OnObjectExplorerCommand( object sender, EventArgs e )
    {
      if (_objectExplorer == null)
      {
        return;
      }
      ToolStripItem item = sender as ToolStripItem;
      if(item == null)
      {
        return;
      }
      ObjectExplorerCommand cmd = (ObjectExplorerCommand)Enum.Parse(typeof(ObjectExplorerCommand), item.Text);
      _objectExplorer.ExecuteCommand(cmd);
    }

    private void BuildProjectExplorerCommandsMenuItems( )
    {
      string[] names = Enum.GetNames(typeof(ProjectExplorerCommand));
      foreach (string cmd in names)
      {
        mnuItemProjectExplorerCommand.DropDown.Items.Add(cmd, null, OnProjectExplorerCommand);
      }
    }

    private void OnProjectExplorerCommand( object sender, EventArgs e )
    {
      if (_projectExplorer == null)
      {
        return;
      }
      ToolStripItem item = sender as ToolStripItem;
      if (item == null)
      {
        return;
      }
      ProjectExplorerCommand cmd = (ProjectExplorerCommand)Enum.Parse(typeof(ProjectExplorerCommand), item.Text);
      _projectExplorer.ExecuteCommand(cmd);
    }
    #endregion


    private void frmSampleAddIn_FormClosed( object sender, FormClosedEventArgs e )
    {
      SampleAddIn.MainForm = null;
      UnsubscribeFromTextEditorEvents();
      UnSubscribeFromScriptEditorEvents();
      UnSubscribeFromObjectExplorerEvents();
			UnsubscribeOtherEvents();
		}


    
    private void openFileToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_textEditor == null)
      {
        return;
      }

      AddMessage("Perform LoadFromFile");
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        _textEditor.LoadContentFromFile(openFileDialog1.FileName);
      }
      AddMessage("Performed LoadFromFile");
    }

    private void saveToFileToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_textEditor == null)
      {
        return;
      }

      AddMessage("Perform SaveToFile");
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        _textEditor.SaveContentToFile(saveFileDialog1.FileName);
      }
      AddMessage("Performed SaveToFile");
    }


   
    private void OnContentRelatedActionClick( object sender, EventArgs e )
    {
      ToolStripMenuItem mnuItem = sender as ToolStripMenuItem;
      if (mnuItem == null)
      {
        return;
      }
      if (_textEditor == null)
      {
        return;
      }
      
      string tag = mnuItem.Tag as String;
      if (String.IsNullOrEmpty(tag))
      {
        return;
      }
      tag = tag.ToLowerInvariant();
      switch(tag)
      {
        case "getcontent":
        AddMessage("---------> [Begin TextEditor GetContent] <---------");
        AddMessage(_textEditor.Content);
        AddMessage("---------> [End TextEditor GetContent] <---------");
          break;
        case "setcontent":
          _textEditor.Content = "This is sample content from the add in!";
          break;
        case "getwordatcursor":
          AddMessage("WordAtCursor: " + _textEditor.WordAtCursor);
          break;
        case "getselectedtext":
          AddMessage("Selected Text: " + _textEditor.SelectedText);
          break;
        case "clearselection":
          _textEditor.ClearSelection();
          AddMessage("Selection cleared.");
          break;
        case "deleteselection":
          _textEditor.DeleteSelection();
          AddMessage("Selection deleted.");
          break;
        case "insertcontent":
          _textEditor.InsertContent("This is INSERTED content from addin.");
          AddMessage("Content inserted.");
          break;
        case "appendcontent":
          _textEditor.AppendContent("This is APPENDED content from addin.");
          AddMessage("Content appended.");
          break;
        default:
          AddError("UNKNOWN ACTION!");
          break;
      }
      
    }

    private void setContentToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_textEditor == null)
      {
        return;
      }

    }

    private void editObjectToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      string objName = _scriptEditor.WordAtCursor;
      _scriptEditor.EditObject(objName);
      AddMessage("Opened object for editing: " + objName);
    }

    private void addMessageToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }

      _scriptEditor.AddMessage(MessageType.Error, "This is sample ERROR message from addin.");
      _scriptEditor.AddMessage(MessageType.Info, "This is sample INFO message from addin.");
      _scriptEditor.AddMessage(MessageType.None, "This is sample NONE message from addin.");
      _scriptEditor.AddMessage(MessageType.Warning, "This is sample WARNING message from addin.");

    }

    private void clearMessagesToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      _scriptEditor.ClearMessagesList();
    }

    private void clearResultsToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      _scriptEditor.ClearResults();

    }

    private void clearOutputPanelToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      _scriptEditor.ClearOutputPane();

    }

    private void getObjectTypeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }
      
      AddMessage("ObjectType in script editor is: " + _scriptEditor.ObjectType.ToString());
    }

    private void inspectServersToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }

      AddMessage("Current Server: " + _scriptEditor.Server);
      IList<string> servers = _scriptEditor.Servers;
      AddMessage("Server List: ");
      foreach (string server in servers)
      {
        AddMessage(server);      
      }
    }

    private void databasesToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }

      AddMessage("Current Database: " + _scriptEditor.Database);
      IList<string> dbs = _scriptEditor.Databases;
      AddMessage("Database List: ");
      foreach (string db in dbs)
      {
        AddMessage(db);
      }
    }

    private void insepectDataSetsToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }

      AddMessage("Inspecting Datasets:" );
      IList<ScriptExecutionResult> eResults = _scriptEditor.ScriptExecutionResults;

			foreach (ScriptExecutionResult eResult in eResults)
			{
				if (eResult.DataSets == null || eResult.DataSets.Count == 0)
					continue;

				foreach (DataSet ds in eResult.DataSets)
				{
					AddMessage(String.Format("{0} : DataSet TableCnt {1}",eResult.ConnParams.InfoDbServer,ds.Tables.Count));
				}
			}
    }

    private void connectionStringToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }

      AddMessage("Connection string: " + _scriptEditor.ConnectionString);
    }

    private void objectInfoToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_scriptEditor == null)
      {
        return;
      }

      string objName = _scriptEditor.WordAtCursor;
      ObjectInfo objInfo = _scriptEditor.GetObjectInfo(objName);
      if (objInfo != null)
      {
        AddMessage("ObjectInfo for: " + objName);
        AddMessage(objInfo.ToString());
      }
      else
      {
        AddError("ObjectInfo not found: " + objName);
      }
    }


    private void selectedNodeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_objectExplorer == null)
      {
        return;
      }
      
      AddMessage("Show ObjectExplorer SelectedNode");
      AddMessage("Node:\r\n" + _objectExplorer.SelNode.ToString());
    }

    private void executeCommanToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_objectExplorer == null)
      {
        return;
      }
    }

    private void frmSampleAddIn_Load( object sender, EventArgs e )
    {
      BuildObjectExplorerCommandsMenuItems();
      BuildProjectExplorerCommandsMenuItems();
    }

    private void selectedNodesToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_objectExplorer == null)
      {
        return;
      }
      AddMessage("Show ObjectExplorer SelectedNodes");
      foreach (ObjectExplorerNode node in _objectExplorer.SelNodes)
      {
        AddMessage("Node:\r\n" + node.ToString());
      }
    }

    private void showMessagesFormToolStripMenuItem_Click( object sender, EventArgs e )
    {
      IHostServices host = HostServicesSingleton.HostServices;
      if (host == null)
      {
        return;
      }

      host.MsgService.ShowMessages();

    }

    private void showObjectExplorerToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.ShowObjectExplorer();
    }

    private void clearMessageToolStripMenuItem_Click( object sender, EventArgs e )
    {
      textBox1.Text = String.Empty;
    }

    private void showProjectExplorerToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_projectExplorer == null)
      {
        return;
      }

      _projectExplorer.ShowProjectExplorer();
    }

    private void selectedNodeToolStripMenuItem1_Click( object sender, EventArgs e )
    {
      if (_projectExplorer == null)
      {
        return;
      }

      if (_projectExplorer.SelectedItem != null)
      {
        AddMessage(_projectExplorer.SelectedItem.ToString());
      }

    }

    private void selectedNodesToolStripMenuItem1_Click( object sender, EventArgs e )
    {
      if (_projectExplorer == null)
      {
        return;
      }

      if (_projectExplorer.SelectedItems != null)
      {
        foreach (ProjectNodeData data in _projectExplorer.SelectedItems)
        {
          AddMessage(data.ToString());
        }
      }

    }

    private void webBrowserNavigateToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_hostServices == null || _hostServices.WebBrowserService == null)
      {
        return;
      }

      string value = "http://www.pragmasql.net";
      if (InputDialog.ShowDialog("Url", ref value) != DialogResult.OK)
      {
        return;
      }
      _hostServices.WebBrowserService.Navigate(value);
    }

    private void webBrowserOpenFile1ToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_hostServices == null || _hostServices.WebBrowserService == null)
      {
        return;
      }
      _hostServices.WebBrowserService.OpenFile();
    }

    private void webBrowserOpenFile2ToolStripMenuItem_Click( object sender, EventArgs e )
    {
      if (_hostServices == null || _hostServices.WebBrowserService == null)
      {
        return;
      }
      if (openFileDialog1.ShowDialog() != DialogResult.OK)
      {
        return;
      }
      _hostServices.WebBrowserService.OpenFile(openFileDialog1.FileName);
    }

		private void getCurrentEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ITextEditor editor = HostServicesSingleton.HostServices.EditorServices.CurrentEditor;
			if (editor is IScriptEditor)
				MessageBox.Show("Current editor is a SCRIPT EDITOR");
			else if (editor is ITextEditor)
				MessageBox.Show("Current editor is a TEXT EDITOR");
			else
				MessageBox.Show("No current editor.");
		}

		private void changeActiveTextAreaCursorToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void toggleEditorCursorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ITextEditor editor = HostServicesSingleton.HostServices.EditorServices.CurrentEditor;
			if (editor == null)
				return;

			if (editor.ActiveTextAreaCursor == Cursors.IBeam)
				editor.ActiveTextAreaCursor = Cursors.Hand;
			else
				editor.ActiveTextAreaCursor = Cursors.IBeam;

			if (editor.Cursor == Cursors.Default)
				editor.Cursor = Cursors.Hand;
			else
				editor.Cursor = Cursors.Default;
		}

		private void toggleReadOnlyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_textEditor == null)
			{
				return;
			}

			AddMessage("Perform ToggleReadOnly");
			_textEditor.ReadOnly = !_textEditor.ReadOnly;
			AddMessage("Performed ToggleReadOnly");
		}

		private void addButtonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_textEditor == null)
			{
				return;
			}

			ToolStripButton btn = new ToolStripButton();
			btn.ForeColor = Color.Red;
			btn.Text = "Test Button";

			_textEditor.AddItemToNotificationStrip(btn);
		}

		private void addLabelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_textEditor == null)
			{
				return;
			}

			ToolStripLabel lbl = new ToolStripLabel();
			lbl.ForeColor = Color.Blue;
			lbl.Text = "Test Label";

			_textEditor.AddItemToNotificationStrip(lbl);

		}

		private void addButtonToAddInStripToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_textEditor == null)
			{
				return;
			}

			ToolStripButton btn = new ToolStripButton();
			btn.ForeColor = Color.Red;
			btn.Text = "Test Button";

			_textEditor.AddItemToAddInStrip(btn);
		}

		private void addLabelFromAddInStripToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_textEditor == null)
			{
				return;
			}

			ToolStripLabel lbl = new ToolStripLabel();
			lbl.ForeColor = Color.Blue;
			lbl.Text = "Test Label";

			_textEditor.AddItemToAddInStrip(lbl);

		}
  }
}