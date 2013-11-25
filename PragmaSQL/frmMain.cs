using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.Win32;
using WeifenLuo.WinFormsUI.Docking;
using ICSharpCode.Core;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using PragmaSQL.Core;
using PragmaSQL.Proxy;
using PragmaSQL.WebBrowserEx;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Shell;

namespace PragmaSQL
{
  public partial class frmMain : KryptonForm, IWorkbench
  {
    #region Fields And Properties

    private bool _canSaveLayout = true;

    private static frmMain _instance = null;
    public static frmMain Instance
    {
      get
      {
        return _instance;
      }
    }

    private ToolStripMenuItem _currentSearchSite = null;
    private LayoutProvider _layoutProvider = new LayoutProvider();

    private HostServices _hostServices = null;
    internal HostServices HostSvc
    {
      get { return _hostServices; }
    }

    public string SearchTerm 
    { 
      get { return cmbWebSearch.Text; }
      set { cmbWebSearch.Text = value; }
    }

    static string mruRegKey = "SOFTWARE\\ALIOZGUR\\PragmaSQL";
    protected MruStripMenuInline mruMenu;

    private DeserializeDockContent OnDeserializeDockContent;
    public string _layoutConfigFile = String.Empty;
    public string CommandLineScriptFileName = String.Empty;
    public string CommandLineProjectFileName = String.Empty;

    private frmObjectExplorer _objectExplorer = null;
    public frmObjectExplorer ObjectExplorer
    {
      get { return _objectExplorer; }
    }


    private frmMessages _messagesForm = null;
    public frmMessages MessagesForm
    {
      get { return _messagesForm; }
    }

    private frmProjectExplorer _projectExplorer;
    public frmProjectExplorer ProjectExplorer
    {
      get { return _projectExplorer; }
    }

    private frmSharedScripts _sharedScripts = null;
    public frmSharedScripts SharedScripts
    {
      get { return _sharedScripts; }
    }

    private frmSharedSnippets _sharedSnippets = null;
    public frmSharedSnippets SharedSnippets
    {
      get { return _sharedSnippets; }
    }

    private DockPanel _dockPanel = new DockPanel();
    public DockPanel DockPanel
    {
      get { return _dockPanel; }
    }

    private ConfigurationContent _configContent = null;
    public ConfigurationContent ConfigContent
    {
      get { return _configContent; }
    }

    private PadType _loadedPadTypes = PadType.None;
    private int? _startPosition = null;


    private bool _applicationIsClosing = false;
    public bool ApplicationIsClosing
    {
      get { return _applicationIsClosing; }
    }

    private IDictionary<string, ConnectionParams> _connRep = null;
    private IList<IDockContent> _dummyContent = new List<IDockContent>();

    private frmWebBrowser _searchBrowser = null;

    public string DefaultResultRenderer
    {
      get
      {
        return (ConfigHelper.Current != null) ? ConfigHelper.Current.DefaultResultRenderer : null;
      }
      set
      {
        if (ConfigHelper.Current != null)
        {
          ConfigHelper.Current.DefaultResultRenderer = value;
          ConfigHelper.SaveAsDefault(ConfigHelper.Current);
        }
      }
    }

    private RecoveryManager _asWorker = new RecoveryManager();

    #endregion //Fields And Properties

    #region CTOR

    public frmMain()
    {
      InitializeComponent();

      frmMain._instance = this;
      _hostServices = (HostServices)HostServicesSingleton.HostServices;

#if PERSONAL_EDITION
      mnuUpgradeProfessional.Visible = true;
      mnuReloadHighlighters.Visible = false;
      _dockPanel.ContentAdded += new EventHandler<DockContentEventArgs>(_dockPanel_ContentAdded);
      _dockPanel.ContentRemoved += new EventHandler<DockContentEventArgs>(_dockPanel_ContentRemoved);
#else
      mnuReloadHighlighters.Visible = CustomHighlightersEnabled;
      InitializeCustomSyntaxHighlighters();

#endif
    }

    private void InitializeCustomSyntaxHighlighters()
    {
      if (!CustomHighlightersEnabled)
        return;


      string folderOld = Path.Combine(PragmaSQLApp.ApplicationRootPath(), "SyntaxHighlighters");
      string folder = Path.Combine(PragmaSQLApp.UserDataFolder, "SyntaxHighlighters");

      try
      {
        if (!Directory.Exists(folder))
          Directory.CreateDirectory(folder);

        TryMovingSyntaxHighlightersFromProgramFiles(folderOld, folder);
        FileSyntaxModeProvider fmp = new FileSyntaxModeProvider(folder);
        HighlightingManager.Manager.AddSyntaxModeFileProvider(fmp);
      }
      catch (Exception ex)
      {
        string error = "Can not load custom syntax highlighters. Error was: " + ex.Message;
        Utils.ShowError(error, MessageBoxButtons.OK);
      }

    }

    private void TryMovingSyntaxHighlightersFromProgramFiles(string folderOld, string folder)
    {
      try
      {
        if (!Directory.Exists(folderOld) || !Directory.Exists(folder))
          return;

        string[] files = Directory.GetFiles(folderOld);
        FileInfo fi;
        foreach (string file in files)
        {
          fi = new FileInfo(file);
          string newFileName = Path.Combine(folder, fi.Name);
          if (File.Exists(newFileName))
            continue;
          File.Copy(file, newFileName);
        }
        Directory.Delete(folderOld);
      }
      catch { }
    }

#if PERSONAL_EDITION
    private readonly int _maxContentCnt = 10;
    private int _currentContentCnt = 0;

    void _dockPanel_ContentAdded(object sender, DockContentEventArgs e)
    {
      _currentContentCnt++;
      if (_currentContentCnt > _maxContentCnt)
      {
        _currentContentCnt--;
        throw new PersonalEditionLimitation(String.Format("Personal Edition does not support more than {0} active windows.", _maxContentCnt));
      }
    }

    void _dockPanel_ContentRemoved(object sender, DockContentEventArgs e)
    {
      _currentContentCnt--;
    }
#endif

    #endregion //CTOR

    #region Initialization

    private void OpenCommandLineFiles()
    {
      if (!String.IsNullOrEmpty(CommandLineScriptFileName))
      {
        _objectExplorer.OpenFileInNewScriptEditor(CommandLineScriptFileName);
        CommandLineScriptFileName = String.Empty;
      }

      if (!String.IsNullOrEmpty(CommandLineProjectFileName))
      {
        _projectExplorer.OpenProject(CommandLineProjectFileName);
        CommandLineProjectFileName = String.Empty;
      }
    }

    private void InitializePads()
    {
      bool showSharedContent = false;

      try
      {
        PrintStatMessage("Loading layout...");

        _loadedPadTypes = PadType.None;

        if (File.Exists(_layoutConfigFile))
        {
          _dockPanel.LoadFromXml(_layoutConfigFile, OnDeserializeDockContent);
          CloseDummyContent();
        }
        else
        {
          showSharedContent = true;
        }
      }
      finally
      {
        ClearStatMessage();
      }

      // Object explorer 
      if (!String.IsNullOrEmpty(CommandLineScriptFileName))
      {
        _objectExplorer.IsInitialShow = false;
      }


      try
      {
        if (PragmaSQLApp.AppStartIndicatorExist)
        {
          if (RecoverContent.GetCount() > 0)
          {
            PrintStatMessage("Recovering content from auto saved content ...");
            frmSplashScreen.HideSplash();
            frmRecoverContent.ShowRecoverForm();
            frmSplashScreen.ShowSplash();
          }
        }
        else
        {
          // Workspace restore
          if (ConfigContent.GeneralOptions.RestoreWorkspace && RecoverContent.GetCount(RecoverContent.WorkspaceFolder) > 0)
          {
            PrintStatMessage("Restoring content ...");
            frmSplashScreen.HideSplash();
            frmRecoverContent.ShowWorkspaceRestoreForm();
            frmSplashScreen.ShowSplash();
          }
        }
      }
      catch (Exception ex)
      {
        GenericErrorDialog.ShowError("Restore/Recover Error", "Can not restore/recover content.", ex);
      }

      RecoverContent.CleanAll(RecoverContent.WorkspaceFolder);

      if (!ContainsPadType(_loadedPadTypes, PadType.ObjectExplorer))
        _objectExplorer.Show(_dockPanel, DockState.DockLeft);

      // Project explorer
      if (!ContainsPadType(_loadedPadTypes, PadType.ProjectExplorer))
      {
        _projectExplorer.Show(_dockPanel, DockState.DockLeft);
      }

      if (!ContainsPadType(_loadedPadTypes, PadType.Messages))
      {
        _messagesForm.Show(_dockPanel, DockState.DockBottomAutoHide);
      }

#if ( PERSONAL_EDITION == false )
      if (showSharedContent)
      {
        ShowSharedSnippetsUnInitialized();
        ShowSharedScriptsUnInitialized();
      }
#endif

      _projectExplorer.Show();
      _objectExplorer.Show();


      if (ConfigHelper.Current.GeneralOptions.WebBrowser_ShowonStart)
      {
        try
        {
          PrintStatMessage("Opening web browser...");
          frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse();
          WebBrowserFactory.ShowWebBrowser(frm);
        }
        finally
        {
          ClearStatMessage();
        }
      }
      else
      {
        try
        {
          PrintStatMessage("Waiting for connection...");
          frmSplashScreen.HideSplash();
          _objectExplorer.CreateNewConnectionFromRepository(this.DockPanel.Contents.Count == 0 && WantEmptyScriptEditorOnStart);
        }
        finally
        {
          ClearStatMessage();
        }
      }
    }

    private bool WantEmptyScriptEditorOnStart
    {
      get
      {
        if (ConfigHelper.Current == null || ConfigHelper.Current.GeneralOptions == null)
          return true;

        return ConfigHelper.Current.GeneralOptions.WantEmptyScriptEditorOnStart;
      }
    }

    private bool CustomHighlightersEnabled
    {
      get
      {
        if (ConfigHelper.Current == null || ConfigHelper.Current.TextEditorOptions == null)
          return false;

        return ConfigHelper.Current.TextEditorOptions.CustomHighlightersEnabled;
      }
    }


    private void InitializeApplication()
    {
      _connRep = ConnectionParamsFactory.EnumerateConnections();

      CreatePads();
      InitializeServices();
      InitializeAddInSupport();

      InitializeDockPanel();
      InitializeMruMenu();

      _layoutConfigFile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL\\PragmaSQL_Layout.config";
      OnDeserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
      _dockPanel.ActiveContentChanged += new EventHandler(_dockPanel_ActiveContentChanged);
    }

    private void CreatePads()
    {
      try
      {
        PrintStatMessage("Initializing pads...");
        _messagesForm = new frmMessages();
        _objectExplorer = new frmObjectExplorer();
        _projectExplorer = new frmProjectExplorer();
      }
      finally
      {
        ClearStatMessage();
      }
    }

    private void InitializeConfiguration()
    {
      try
      {
        PrintStatMessage("Loading configuration...");
        _configContent = ConfigHelper.LoadFromDefault();
        RenderWebSearchSitesMenuItems();
        ScriptEditorShortcutKeysProvider.LoadFromConfig(_configContent);
        CodeCompletionListLoader.Load();
        CodeCompletionProposalCache.CollectInterval = _configContent.TextEditorOptions.CodeCompCacheCollectInterval * 60;
        ApplyPalette(_configContent);
      }
      finally
      {
        ClearStatMessage();
      }
    }

    private void ApplyPalette(ConfigurationContent configContent)
    {
      bool loadGlobalMode = true;
      if (configContent.GeneralOptions.UseCustomPalette)
      {
        try
        {
          kryptonManager1.GlobalPalette = null;
          kryptonPaletteCustom.Import(ConfigHelper.CustomPaletteFileName);
          kryptonManager1.GlobalPalette = kryptonPaletteCustom;
          loadGlobalMode = false;
        }
        catch (Exception ex)
        {
          GenericErrorDialog.ShowError("Palette Load Error", "Custom palette load failed.", ex);
        }
      }

      if (loadGlobalMode)
      {
        kryptonManager1.GlobalPalette = null;
        kryptonManager1.GlobalPaletteMode = configContent.GeneralOptions.PaletteMode;
      }

      PaletteOffice2007Silver p = new PaletteOffice2007Silver();
    }

    private void ApplyAutoSaveOptions(ConfigurationContent configContent)
    {
      _asWorker.ApplyAutoSaveOptions(configContent);
    }

    private bool InitializeLayoutProviderEx()
    {
      _startPosition = Int32.MinValue;
      _canSaveLayout = true;
      return true;
    }

    private void _dockPanel_ActiveContentChanged(object sender, EventArgs e)
    {
      if (_dockPanel.ActiveContent != null && _dockPanel.ActiveContent.DockHandler != null && _dockPanel.ActiveContent.DockHandler.Form != null)
      {
        Form activeForm = _dockPanel.ActiveContent.DockHandler.Form;
        _hostServices.FireActiveDocumentChangedEvent(activeForm);
      }
      else
      {
        _hostServices.FireActiveDocumentChangedEvent(null);
      }
    }

    private void UpdateToolTipText(DockContentCollection contents)
    {
      if (contents == null)
      {
        return;
      }
      foreach (IDockContent form in contents)
      {
        if (form is IToolTipProvider)
        {
          (form as IToolTipProvider).UpdateToolTipText();
        }
      }
    }

    #endregion //Initialization

    #region MRU Related

    private void InitializeMruMenu()
    {
      RegistryKey regKey = Registry.CurrentUser.OpenSubKey(mruRegKey);
      mruMenu = new MruStripMenuInline(mnuItemRecentFiles, mnuItemRecentFile, new MruStripMenu.ClickedHandler(OnMruFile), mruRegKey + "\\MRU");
      mruMenu.MaxEntries = 10;
    }

    private void OnMruFile(int number, string filename)
    {
      if (!File.Exists(filename))
      {
        MessageBox.Show("File does not exists!\n" + filename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        mruMenu.RemoveFile(number);
        return;
      }
      FileInfo fi = new FileInfo(filename);

      if (fi.Extension.ToLowerInvariant() == ".sqlprj")
      {
        _projectExplorer.OpenProject(filename);
        ShowProjectExplorer();
      }
      else if (fi.Extension.ToLowerInvariant() == ".sql" || fi.Extension.ToLowerInvariant() == ".qry")
      {
        if (_objectExplorer == null)
        {
          return;
        }
        _objectExplorer.OpenFileInNewScriptEditor(filename);
      }
      else
      {
        frmTextEditor frm = TextEditorFactory.OpenFile(filename);
        TextEditorFactory.ShowTextEditor(frm);
      }
    }

    public void AddFileToMru(string fileName)
    {
      mruMenu.AddFile(fileName);
    }

    #endregion

    #region Docking

    private void InitializeDockPanel()
    {

      _dockPanel.ActiveAutoHideContent = null;
      _dockPanel.Dock = DockStyle.Fill;
      _dockPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
      _dockPanel.Location = new System.Drawing.Point(0, 49);
      _dockPanel.Name = "_dockPanel";
      _dockPanel.Size = new System.Drawing.Size(579, 338);
      _dockPanel.TabIndex = 0;
      this.Controls.Add(_dockPanel);
      _dockPanel.BringToFront();
      _dockPanel.ShowDocumentIcon = true;
#if PERSONAL_EDITION
      _dockPanel.DocumentStyle = DocumentStyle.DockingMdi;
#else
      _dockPanel.DocumentStyle = ConfigContent == null || ConfigContent.GeneralOptions == null ? DocumentStyle.DockingMdi : ConfigContent.GeneralOptions.HostDocumentStyle;
#endif

      //_dockPanel.DocumentStyle = DocumentStyle.DockingMdi;
    }

    public IDockContent FindDocument(Type docType)
    {
      bool typeMathches = false;
      if (_dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
      {
        foreach (Form form in MdiChildren)
        {
          typeMathches = docType != null ? (form.GetType() == docType) : true;
          if (typeMathches)
            return form as IDockContent;
        }
        return null;
      }
      else
      {
        IDockContent[] docs = _dockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          typeMathches = docType != null ? (content.GetType() == docType) : true;
          if (typeMathches)
            return content;
        }
        return null;
      }
    }

    public IDockContent FindDocument(string text, Type docType)
    {
      bool typeMathches = false;
      if (_dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
      {
        foreach (Form form in MdiChildren)
        {
          typeMathches = docType != null ? (form.GetType() == docType) : true;
          if (typeMathches && (form.Text.ToLowerInvariant() == text.ToLowerInvariant()))
            return form as IDockContent;
        }
        return null;
      }
      else
      {
        IDockContent[] docs = _dockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          typeMathches = docType != null ? (content.GetType() == docType) : true;
          if (typeMathches && (content.DockHandler.TabText.ToLowerInvariant() == text.ToLowerInvariant()))
            return content;
        }
        return null;
      }
    }


    public IDockContent FindDocument(string text)
    {
      return FindDocument(text, null);
    }

    private bool ContainsPadType(PadType types, PadType type)
    {
      return (types & type) == type;
    }

    private IDockContent GetContentFromPersistString(string persistString)
    {
      if (persistString == typeof(frmProjectExplorer).ToString())
      {
        _loadedPadTypes = _loadedPadTypes | PadType.ProjectExplorer;
        return _projectExplorer;
      }
      else if (persistString == typeof(frmObjectExplorer).ToString())
      {
        _loadedPadTypes = _loadedPadTypes | PadType.ObjectExplorer;
        return _objectExplorer;
      }
      else if (persistString == typeof(frmMessages).ToString())
      {
        _loadedPadTypes = _loadedPadTypes | PadType.Messages;
        return _messagesForm;
      }
      else if (persistString == typeof(frmSharedSnippets).ToString())
      {
#if ( PERSONAL_EDITION == false )
        CreateSharedSnippetsForm();
        _loadedPadTypes = _loadedPadTypes | PadType.SharedSnippets;
#endif
        return _sharedSnippets;
      }
      else if (persistString == typeof(frmSharedScripts).ToString())
      {
#if ( PERSONAL_EDITION == false )
        CreateSharedScriptsForm();
        _loadedPadTypes = _loadedPadTypes | PadType.SharedScripts;
#endif
        return _sharedScripts;
      }
      else
      {
        IDockContent dummy = new DockContent();
        _dummyContent.Add(dummy);
        return dummy;
      }
    }

    private void CloseDummyContent()
    {
      DockContent dummy = null;
      while (_dummyContent.Count > 0)
      {
        dummy = _dummyContent[0] as DockContent;
        _dummyContent.Remove(dummy);
        dummy.Close();
      }
    }
    #endregion

    #region Methods

    private const int WM_CLOSE = 16;
    private bool _cancelClose = false;
    protected override void WndProc(ref Message m)
    {
      if (m.Msg == WM_CLOSE)
      {
        _applicationIsClosing = true;
        if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, null) == DialogResult.Cancel)
        {
          _cancelClose = true;
          _applicationIsClosing = false;
          RecoverContent.CleanAll(RecoverContent.WorkspaceFolder);
        }
        else
        {
          PerformClose();
          _cancelClose = false;
          base.WndProc(ref m);
        }

      }
      else if (HandleJumpListAction(m))
      {
        return;
      }
      else
      {
        base.WndProc(ref m);
      }

      return;
    }

    private bool HandleJumpListAction(Message m)
    {
      bool result = true;
      if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.NewScript))
      {
        NewScript();
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.NewText))
      {
        NewTextEditor();
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.NewDiff))
      {
        NewTextDiff();
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.NewBrowser))
      {
        NewWebBrowser();
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.OpenFile))
      {
        NewTextFromFile();
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.OpenScript))
      {
        NewScriptFromFile();
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.OpenProject))
      {
        OpenProject();
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.SaveScriptsForRecovery))
      {
        _asWorker.PerformAutoSave(true);
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.ConnectTo))
      {
        _objectExplorer.CreateNewConnection(false);
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.ConnectionFromList))
      {
        _objectExplorer.CreateNewConnectionFromRepository(false);
      }
      else if (m.Msg == User32.GetJumpListMsg(JumpListOperationsEnum.SavedConnections))
      {
        ShowSavedConnections();
      }
      else
      {
        result = false;
      }
      return result;
    }


    public NodeData GetCurrentSelectedNodeDataFromObjectExplorer()
    {
      if (_objectExplorer == null)
      {
        return null;
      }

      return _objectExplorer.GetCurrentSelectedNodeData();
    }

    private void CloseAllFloatingWindows()
    {
      IList<Form> tmpclose = new List<Form>();
      foreach (Form form in DockPanel.FloatWindows)
      {
        tmpclose.Add(form);
      }

      foreach (Form form in tmpclose)
      {
        form.Close();
      }
      tmpclose.Clear();
      tmpclose = null;
    }

    public void CloseDocuments(Form skipThis)
    {
      if (DockPanel != null)
      {
        if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form == skipThis)
              continue;
            form.Close();
          }
        }
        else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
        {
          IList<Form> tmpclose = new List<Form>();
          foreach (Form form in DockPanel.FloatWindows)
          {
            if (form == skipThis)
              continue;
            tmpclose.Add(form);
          }

          foreach (Form form in tmpclose)
          {
            form.Close();
          }
          tmpclose.Clear();
          tmpclose = null;
        }



        IDockContent[] docs = DockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          if (content == skipThis)
            continue;

          Form frm = content as Form;
          if (frm != null)
            frm.Close();
          else
            content.DockHandler.Close();
        }
      }
    }

    public IList<frmObjectGrouping> GetAllObjectGroupingEditors()
    {
      IList<frmObjectGrouping> result = new List<frmObjectGrouping>();

      if (DockPanel != null)
      {
        if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form is frmObjectGrouping)
            {
              result.Add(form as frmObjectGrouping);
            }
          }
        }
        else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
        {
          foreach (Form form in DockPanel.FloatWindows)
          {
            if (form is frmObjectGrouping)
            {
              result.Add(form as frmObjectGrouping);
            }
          }
        }
        IDockContent[] docs = DockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          if (content is frmObjectGrouping)
          {
            result.Add(content as frmObjectGrouping);
          }
        }
      }

      return result;
    }

    public frmScriptEditor GetCurrentScriptEditor()
    {
      /*
      if (DockPanel == null)
      {
        return null;
      }

      if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
      {
        if (ActiveMdiChild is frmScriptEditor)
        {
          return ActiveMdiChild as frmScriptEditor;
        }
        else
        {
          return null;
        }
      }
      else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
      {
        if (DockPanel.ActiveDocument is frmScriptEditor)
        {
          return DockPanel.ActiveDocument as frmScriptEditor;
        }
        else
        {
          return null;
        }
      }
      else
      {
        return null;
      }
      */

      frmEditorBase editor = GetCurrentEditor();
      if (editor is frmScriptEditor)
        return editor as frmScriptEditor;
      else
        return null;
    }

    public frmTextEditor GetCurrentTextEditor()
    {
      /*
      if (DockPanel == null)
      {
        return null;
      }

      if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
      {
        if (ActiveMdiChild is frmTextEditor)
        {
          return ActiveMdiChild as frmTextEditor;
        }
        else
        {
          return null;
        }
      }
      else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
      {
        if (DockPanel.ActiveDocument is frmTextEditor)
        {
          return DockPanel.ActiveDocument as frmTextEditor;
        }
        else
        {
          return null;
        }
      }
      else
      {
        return null;
      }
      */

      frmEditorBase editor = GetCurrentEditor();
      if (editor is frmTextEditor)
        return editor as frmTextEditor;
      else
        return null;
    }

    public frmEditorBase GetCurrentEditor()
    {
      if (DockPanel == null)
      {
        return null;
      }

      if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
      {
        if (ActiveMdiChild is frmEditorBase)
        {
          return ActiveMdiChild as frmEditorBase;
        }
        else
        {
          return null;
        }
      }
      else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
      {
        if (DockPanel.ActiveDocument is frmEditorBase)
        {
          return DockPanel.ActiveDocument as frmEditorBase;
        }
        else
        {
          return null;
        }
      }
      else
      {
        return null;
      }
    }

    public IList<IPragmaEditor> GetAllPragmaEditors()
    {
      IList<IPragmaEditor> result = new List<IPragmaEditor>();

      if (DockPanel != null)
      {
        if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form is IPragmaEditor)
            {
              result.Add(form as IPragmaEditor);
            }
          }
        }
        else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
        {
          foreach (Form form in DockPanel.FloatWindows)
          {
            if (form is IPragmaEditor)
            {
              result.Add(form as IPragmaEditor);
            }
          }
        }
        IDockContent[] docs = DockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          if (content is IPragmaEditor)
          {
            result.Add(content as IPragmaEditor);
          }
        }
      }

      return result;
    }

    public IList<ITextEditor> GetAllEditors()
    {
      IList<ITextEditor> result = new List<ITextEditor>();

      if (DockPanel != null)
      {
        if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form is ITextEditor)
            {
              result.Add(form as ITextEditor);
            }
          }
        }
        else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
        {
          foreach (Form form in DockPanel.FloatWindows)
          {
            if (form is ITextEditor)
            {
              result.Add(form as ITextEditor);
            }
          }
        }
        IDockContent[] docs = DockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          if (content is ITextEditor)
          {
            result.Add(content as ITextEditor);
          }
        }
      }

      return result;
    }

    public IList<ITextEditor> GetAllTextEditors()
    {
      IList<ITextEditor> result = null;

      if (DockPanel != null)
      {
        result = new List<ITextEditor>();
        if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form is frmTextEditor)
            {
              result.Add(form as ITextEditor);
            }
          }
        }
        else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
        {
          foreach (Form form in DockPanel.FloatWindows)
          {
            if (form is frmTextEditor)
            {
              result.Add(form as ITextEditor);
            }
          }
        }
        IDockContent[] docs = DockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          if (content is frmTextEditor)
          {
            result.Add(content as ITextEditor);
          }
        }
      }

      return result;
    }

    public IList<IScriptEditor> GetAllScriptEditors()
    {
      IList<IScriptEditor> result = null;

      if (DockPanel != null)
      {
        result = new List<IScriptEditor>();
        if (DockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        {
          foreach (Form form in MdiChildren)
          {
            if (form is frmScriptEditor)
            {
              result.Add(form as IScriptEditor);
            }
          }
        }
        else if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi)
        {
          foreach (Form form in DockPanel.FloatWindows)
          {
            if (form is frmScriptEditor)
            {
              result.Add(form as IScriptEditor);
            }
          }
        }
        IDockContent[] docs = DockPanel.DocumentsToArray();
        foreach (IDockContent content in docs)
        {
          if (content is frmScriptEditor)
          {
            result.Add(content as IScriptEditor);
          }
        }
      }

      return result;
    }



#if PERSONAL_EDITION
    public void ShowSharedScripts()
    {
      throw new PersonalEditionLimitation();
    }

    public void ShowSharedSnippets()
    {
      throw new PersonalEditionLimitation();
    }
#endif

#if (PERSONAL_EDITION == false)
    public void CreateSharedScriptsForm()
    {
      if (_sharedScripts == null)
      {
        _sharedScripts = new frmSharedScripts();
        _sharedScripts.FormClosed += new FormClosedEventHandler(OnSharedScriptsFormClosed);
        _hostServices.InitializeSharedScriptsViewerService(_sharedScripts.SharedScriptsControl);
      }
    }

    public void ShowSharedScriptsUnInitialized()
    {
      CreateSharedScriptsForm();
      if (_sharedScripts.PanelPane == null)
      {
        _sharedScripts.Show(_dockPanel, DockState.DockLeft);
      }
      else
      {
        _sharedScripts.Show(_dockPanel);
        _sharedScripts.PanelPane.Show();
      }
    }

    public void ShowSharedScripts()
    {
      if (ConfigContent.SharedScriptsEnabled())
      {
        if (_sharedScripts == null)
        {
          _sharedScripts = new frmSharedScripts();
          _sharedScripts.FormClosed += new FormClosedEventHandler(OnSharedScriptsFormClosed);
          _hostServices.InitializeSharedScriptsViewerService(_sharedScripts.SharedScriptsControl);
        }

        _sharedScripts.SharedScriptsControl.InitializeSharedScripts(ConfigContent.PragmaSqlDbConn);
        if (_sharedScripts.PanelPane == null)
        {
          _sharedScripts.Show(_dockPanel, DockState.DockLeft);
        }
        else
        {
          _sharedScripts.Show(_dockPanel);
          _sharedScripts.PanelPane.Show();
        }
      }
      else
      {
        MessageBox.Show("Shared scripts option is not set."
          + "\nPlease enable \"Tools -> Options -> PragmaSQL System -> Use shared scripts\" option."
          , "Information"
          , MessageBoxButtons.OK
          , MessageBoxIcon.Information);

        if (_sharedScripts != null)
        {
          _sharedScripts.Close();
        }
      }
    }

    public void CreateSharedSnippetsForm()
    {
      if (_sharedSnippets == null)
      {
        _sharedSnippets = new frmSharedSnippets();
        _sharedSnippets.FormClosed += new FormClosedEventHandler(OnSharedSnippetsFormClosed);
        _hostServices.InitializeSharedSnippetsViewerService(_sharedSnippets.SharedSnippetsControl);
      }
    }

    public void ShowSharedSnippetsUnInitialized()
    {
      CreateSharedSnippetsForm();
      if (_sharedSnippets.PanelPane == null)
      {
        _sharedSnippets.Show(_dockPanel, DockState.DockLeft);
      }
      else
      {
        _sharedSnippets.Show(_dockPanel);
        _sharedSnippets.PanelPane.Show();
      }

    }

    public void ShowSharedSnippets()
    {

      if (ConfigContent.SharedSnippetsEnabled())
      {
        if (_sharedSnippets == null)
        {
          _sharedSnippets = new frmSharedSnippets();
          _sharedSnippets.FormClosed += new FormClosedEventHandler(OnSharedSnippetsFormClosed);
          _hostServices.InitializeSharedSnippetsViewerService(_sharedSnippets.SharedSnippetsControl);
        }

        _sharedSnippets.SharedSnippetsControl.InitializeSharedSnippets(ConfigContent.PragmaSqlDbConn);
        if (_sharedSnippets.PanelPane == null)
        {
          _sharedSnippets.Show(_dockPanel, DockState.DockLeft);
        }
        else
        {
          _sharedSnippets.Show(_dockPanel);
          _sharedSnippets.PanelPane.Show();
        }
      }
      else
      {
        MessageBox.Show("Shared code snippets option is not set."
          + "\nPlease enable \"Tools -> Options -> PragmaSQL System -> Use shared code snippets\" option."
          , "Information"
          , MessageBoxButtons.OK
          , MessageBoxIcon.Information);

        if (_sharedSnippets != null)
        {
          _sharedSnippets.Close();
        }
      }

    }
#endif

    private void RefreshCodeCompletionListsForAllEditors()
    {
      IList<IPragmaEditor> scriptEditors = GetAllPragmaEditors();
      foreach (IPragmaEditor scriptEditor in scriptEditors)
      {
        scriptEditor.RefreshCodeCompletionLists();
      }
    }

    public void EnsureMaximized()
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.WindowState = FormWindowState.Maximized;
      }
    }

    public void ToggleProjectExplorer()
    {
      if (_projectExplorer == null)
        return;

      bool visible = _projectExplorer.DockState == DockState.Hidden;

      if (visible)
        ShowProjectExplorer();
      else
        _projectExplorer.DockState = DockState.Hidden;
    }

    public void ShowProjectExplorer()
    {
      if (_projectExplorer.PanelPane != null)
      {
        _projectExplorer.Show(_dockPanel);
        _projectExplorer.PanelPane.Show();
      }
      else
      {
        _projectExplorer.Show(_dockPanel, DockState.DockLeft);
      }
    }

    public void ToggleObjectExplorer()
    {
      if (_objectExplorer == null)
        return;

      bool visible = _objectExplorer.DockState == DockState.Hidden;

      if (visible)
        ShowObjectExplorer();
      else
        _objectExplorer.DockState = DockState.Hidden;
    }

    public void ShowObjectExplorer()
    {
      if (_objectExplorer.PanelPane != null)
      {
        _objectExplorer.Show(_dockPanel);
        _objectExplorer.PanelPane.Show();
      }
      else
      {
        _objectExplorer.Show(_dockPanel, DockState.DockLeft);
      }
    }

    public void ToggleMessagesForm()
    {
      if (_messagesForm == null)
        return;

      bool visible = _messagesForm.DockState == DockState.Hidden;

      if (visible)
        ShowMessagesForm();
      else
        _messagesForm.DockState = DockState.Hidden;
    }


    public void ShowMessagesForm()
    {
      if (!_messagesForm.InvokeRequired)
      {
        ShowMessagesForm_Internal();
      }
      else
      {
        this.Invoke(new ActionF(ShowMessagesForm_Internal));
      }
    }

    private void ShowMessagesForm_Internal()
    {
      if (_messagesForm.PanelPane != null)
      {
        _messagesForm.Show(_dockPanel);
        _messagesForm.PanelPane.Show();
      }
      else
      {
        _messagesForm.Show(_dockPanel, DockState.DockBottomAutoHide);
      }
    }

    public bool IsFormAutoHidden(DockState state)
    {
      if ((state == DockState.DockLeftAutoHide)
        || (state == DockState.DockRightAutoHide)
        || (state == DockState.DockTopAutoHide)
        || (state == DockState.DockBottomAutoHide)
        )
      {
        return true;
      }
      else
      {
        return false;
      }


    }

    public DockState ToggleAutoHideState(DockState state)
    {
      if (state == DockState.DockLeftAutoHide)
        return DockState.DockLeft;
      else if (state == DockState.DockRightAutoHide)
        return DockState.DockRight;
      else if (state == DockState.DockTopAutoHide)
        return DockState.DockTop;
      else if (state == DockState.DockBottomAutoHide)
        return DockState.DockBottom;
      else
        return state;
      /*
      else if (state == DockState.DockLeft)
        return DockState.DockLeftAutoHide;
      else if (state == DockState.DockRight)
        return DockState.DockRightAutoHide;
      else if (state == DockState.DockTop)
        return DockState.DockTopAutoHide;
      else if (state == DockState.DockBottom)
        return DockState.DockBottomAutoHide;
      else
        return state;
      */
    }

    public void Navigate(string caption, string url)
    {
      IWebBrowserService svc = HostServicesSingleton.HostServices.WebBrowserService;
      svc.Navigate(caption, url);
    }

    public void Navigate(string url)
    {
      IWebBrowserService svc = HostServicesSingleton.HostServices.WebBrowserService;
      svc.Navigate(url);
    }

    #endregion //Methods

    #region Application Options
    private void OnConfigDlgFinalSelection(object sender, ConfigEventArgs e)
    {
      switch (e.action)
      {
        case ConfigAction.Cancel:
          break;
        case ConfigAction.Save:
          CodeCompletionListLoader.Load();
          ApplyOptions(e);
          break;
        case ConfigAction.Apply:
          CodeCompletionListLoader.Load();
          ApplyOptions(e);
          break;
        case ConfigAction.None:
        default:
          CodeCompletionListLoader.Load();
          break;
      }

    }

    private void ApplyOptions(ConfigEventArgs e)
    {
      ApplyOptions_General(e);
      ApplyOptions_TextEditor(e);
      ApplyOptions_ScriptEditorShortcuts();
      ApplyOptions_SharedSnippets(e);
      ApplyOptions_SharedScripts();
      ApplyOptions_SingleInstanceOptions(e);
      ApplyOptions_WebSearch(e);
    }

    private void ApplyOptions_General(ConfigEventArgs e)
    {
      ConfigurationContent configContent = ConfigHelper.Current;
      if (configContent == null)
      {
        return;
      }

      if (e.ChangedOptions.Contains("GeneralOptions"))
      {
        //if (kryptonManager1.GlobalPaletteMode != configContent.GeneralOptions.PaletteMode)
        //  kryptonManager1.GlobalPaletteMode = configContent.GeneralOptions.PaletteMode;
        ApplyPalette(configContent);
        ApplyAutoSaveOptions(configContent);
      }

    }

    private void ApplyOptions_TextEditor(ConfigEventArgs e)
    {
      ConfigurationContent configContent = ConfigHelper.Current;
      if (configContent == null)
      {
        return;
      }

      if (e.ChangedOptions.Contains("TextEditorOptions"))
      {
        CodeCompletionProposalCache.CollectInterval = configContent.TextEditorOptions.CodeCompCacheCollectInterval * 60;
        CodeCompletionProposalCache.ApplyTimeoutChange(configContent.TextEditorOptions.CodeCompCacheTimeout * 60);
      }

      IList<IPragmaEditor> scriptEditors = GetAllPragmaEditors();
      foreach (IPragmaEditor scriptEditor in scriptEditors)
      {
        configContent.TextEditorOptions.ApplyToControl(scriptEditor.TextEditor);
        scriptEditor.SharedScriptOperationsVisible = (configContent != null) && (configContent.SharedScriptsEnabled());

        if (e.ChangedOptions.Contains("CodeCompletionLists"))
        {
          scriptEditor.RefreshCodeCompletionLists();
        }

        if (e.ChangedOptions.Contains("TextEditorOptions"))
        {
          scriptEditor.ApplyTextEditorOptionsFromCurrentConfig();
        }

        if (e.ChangedOptions.Contains("PragmaSqlSystem"))
        {
          scriptEditor.InspectPragmaSQLDbConnection();
        }
      }
    }

    private void ApplyOptions_ScriptEditorShortcuts()
    {
      IList<IPragmaEditor> scriptEditors = GetAllPragmaEditors();
      foreach (IPragmaEditor scriptEditor in scriptEditors)
      {
        scriptEditor.RefreshShortcuts();
      }
    }

    public void ApplyOptions_SharedSnippets(ConfigEventArgs e)
    {
      if (_sharedSnippets == null)
      {
        return;
      }

      if (e.ChangedOptions.Contains("SharedSnippetsOptions")
        || e.ChangedOptions.Contains("PragmaSqlSystem"))
      {
        if (!ConfigContent.SharedScriptsEnabled())
        {
          _sharedSnippets.Close();
          SharedSnippetProvider.InitialLoadFromDefaultShare();
        }
        else
        {
          _sharedSnippets.SharedSnippetsControl.InitializeSharedSnippets(ConfigContent.SharedSnippetsEnabled() ? ConfigContent.PragmaSqlDbConn : null);
        }
      }
    }

    public void ApplyOptions_SharedScripts()
    {
      if (_sharedScripts == null)
      {
        return;
      }

      if (!ConfigContent.SharedScriptsEnabled())
      {
        _sharedScripts.Close();
      }
      else
      {
        _sharedScripts.SharedScriptsControl.InitializeSharedScripts(ConfigContent.SharedScriptsEnabled() ? ConfigContent.PragmaSqlDbConn : null);
      }
    }

    private void ApplyOptions_SingleInstanceOptions(ConfigEventArgs e)
    {
      if (!e.ChangedOptions.Contains("GeneralOptions"))
      {
        return;
      }

      try
      {
        if (!e.content.GeneralOptions.IsSingleInstance)
        {
          SingletonController.Cleanup();
        }
        else
        {
          SingletonController.IamFirst(new SingletonController.ReceiveDelegate(Program.OnSingletonControllerReceive));
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Can not apply \"General Setings -> Single instance\" option!\nError was: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ApplyOptions_WebSearch(ConfigEventArgs e)
    {
      ConfigurationContent configContent = ConfigHelper.Current;
      if (configContent == null)
      {
        return;
      }
      RenderWebSearchSitesMenuItems();
    }

    private void RenderWebSearchSitesMenuItems()
    {
      mnuWebSearch.Text = "Web Search";
      mnuWebSearch.DropDownItems.Clear();
      cmbWebSearch.Text = String.Empty;

      try
      {
        ConfigurationContent configContent = ConfigHelper.Current;
        if (configContent == null)
        {
          return;
        }

        WebSearchSite currentSite = null;
        if (_currentSearchSite != null && _currentSearchSite.Tag != null)
        {
          currentSite = _currentSearchSite.Tag as WebSearchSite;
        }

        _currentSearchSite = null;
        bool markedCurrent = false;

        ToolStripMenuItem item = null;
        IList<WebSearchSite> sites = configContent.WebSearchOptions;
        foreach (WebSearchSite site in sites)
        {
          item = (ToolStripMenuItem)mnuWebSearch.DropDownItems.Add(site.Name);
          item.ForeColor = Color.Blue;
          item.Tag = site.Copy();
          item.Click += new EventHandler(OnWebSearchWebSiteClicked);
          if (!markedCurrent && currentSite != null && currentSite.EqualsTo(site))
          {
            markedCurrent = true;
            item.Checked = true;
            _currentSearchSite = item;
            mnuWebSearch.Text = "Search " + item.Text;
          }
        }

        if (!markedCurrent && mnuWebSearch.DropDownItems.Count > 0)
        {
          _currentSearchSite = (ToolStripMenuItem)mnuWebSearch.DropDownItems[0];
          _currentSearchSite.Checked = true;
          mnuWebSearch.Text = "Search " + _currentSearchSite.Text;
        }
      }
      finally
      {
        if (mnuWebSearch.DropDownItems.Count > 0)
        {
          mnuWebSearch.DropDownItems.Add("-");
        }
        mnuWebSearch.DropDownItems.Add("Configure...", null, OnConfigureWebSearchClick);
      }
    }

    private void OnConfigureWebSearchClick(object sender, EventArgs args)
    {
      ShowOptionsDialog();
      if (frmConfigurationDlg.Instance != null)
      {
        frmConfigurationDlg.Instance.ShowOptionsEditor(OptionEditorType.WebSearch);
      }
    }

    private void OnWebSearchWebSiteClicked(object sender, EventArgs args)
    {
      ToolStripMenuItem item = sender as ToolStripMenuItem;
      if (item == null)
      {
        return;
      }

      if (item == _currentSearchSite)
      {
        return;
      }

      if (_currentSearchSite != null)
      {
        _currentSearchSite.Checked = false;
      }
      item.Checked = true;
      _currentSearchSite = item;
      mnuWebSearch.Text = "Search " + item.Text;
    }

    public void PerformWebSearch()
    {
      PerformWebSearch(cmbWebSearch.Text);
    }

    public void PerformWebSearch(string searchText)
    {
      if (_currentSearchSite == null || _currentSearchSite.Tag == null)
      {
        MessageBox.Show(
          "Search sites not defined.\n"
          + "You can define sites via Tools -> Options -> Web Search"
          , "Warning"
          , MessageBoxButtons.OK
          , MessageBoxIcon.Warning);
        return;
      }

      WebSearchSite site = _currentSearchSite.Tag as WebSearchSite;
      string url = String.Format(site.Url, searchText);
      if (!String.IsNullOrEmpty(searchText) && !cmbWebSearch.Items.Contains(searchText))
      {
        cmbWebSearch.SelectedIndex = cmbWebSearch.Items.Add(searchText);
      }

      if (_searchBrowser == null)
      {
        _searchBrowser = WebBrowserFactory.Create();
        _searchBrowser.FormClosed += new FormClosedEventHandler(_searchBrowser_FormClosed);
      }

      WebBrowserFactory.ShowWebBrowser(_searchBrowser);
      _searchBrowser.Stop();
      _searchBrowser.Navigate(url);
    }

    void _searchBrowser_FormClosed(object sender, FormClosedEventArgs e)
    {
      _searchBrowser = null;
    }


    public void ShowOptionsDialog()
    {
      ShowOptionsDialog(String.Empty);
    }

    public void ShowOptionsDialog(string editorName)
    {
      frmConfigurationDlg.ShowConfigurationDlg(this.ConfigContent, editorName, OnConfigDlgFinalSelection);
    }

    #endregion

    #region AddIn Support

    private void InitializeServices()
    {
      try
      {
        PrintStatMessage("Initializing services...");
        _hostServices.FireBeforeServicesInitializedEvent();
        _hostServices.InitializeWorkbench(this);
        _hostServices.InitializeMsgServices(_messagesForm.ListView, MsgServiceShowMessagesCallback);
        _hostServices.InitializeObjectExplorerService(_objectExplorer);
        _hostServices.InitializeProjectExplorerService(_projectExplorer);
        _hostServices.InitializeEditorService();
        _hostServices.ServicesInitialized = true;
        _hostServices.FireAfterServicesInitializedEvent();
      }
      finally
      {
        ClearStatMessage();
      }
    }



    private void MsgServiceShowMessagesCallback(object sender, EventArgs args)
    {
      ShowMessagesForm();
    }

    private void InitializeAddInSupport()
    {
      try
      {
        PrintStatMessage("Initializing add-in support...");

        _hostServices.RegisterDefaultResultRenderers();
        AddInLoader.Load();

        mnuItemTools.DropDownItems.Remove(mnuOptions);
        mnuItemTools.DropDownItems.Remove(mnuReloadHighlighters);
        mnuItemTools.DropDownItems.Remove(sepOptions);
        MenuService.AddItemsToMenu(mnuItemTools.DropDownItems, this, "/Workspace/ToolsMenu");
        if (mnuItemTools.DropDownItems.Count > 0)
          mnuItemTools.DropDownItems.Add(sepOptions);

        mnuItemTools.DropDownItems.Add(mnuReloadHighlighters);
        mnuItemTools.DropDownItems.Add(mnuOptions);

        mnuHelp.DropDownItems.Remove(mnuAbout);
        MenuService.AddItemsToMenu(mnuHelp.DropDownItems, this, "/Workspace/HelpMenu");
        mnuHelp.DropDownItems.Add(mnuAbout);

        ToolStrip toolbar = ToolbarService.CreateToolStrip(this, "/Workspace/Toolbar");
        if (toolbar.Items.Count == 0)
        {
          toolbar.Dispose();
          toolbar = null;
        }
        else
        {
          this.Controls.Add(toolbar);
          toolbar.RenderMode = ToolStripRenderMode.ManagerRenderMode;
          toolbar.GripStyle = ToolStripGripStyle.Hidden;
          toolbar.Dock = DockStyle.Top;
          toolbar.BringToFront();
        }

        _objectExplorer.InitializeAddInSupport();
        _projectExplorer.InitializeAddInSupport();
      }
      catch (Exception ex)
      {
        frmException.ShowAppError("[Main Form] AddIn support can not be initialized!", ex);
      }
      finally
      {
        ClearStatMessage();
      }
    }

    #endregion //AddIn Support

    #region MainMenu Commands

    public void NewScript()
    {
      if (_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.CreateScriptEditor();
    }

    public void NewScriptFromFile()
    {
      if (_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.OpenFileInNewScriptEditor();
    }

    public void NewTextFromFile()
    {
      frmTextEditor frm = TextEditorFactory.OpenFile(String.Empty);
      if (frm == null)
        return;

      TextEditorFactory.ShowTextEditor(frm);
    }

    public void NewTextEditor()
    {
      frmTextEditor frm = TextEditorFactory.Create();
      TextEditorFactory.ShowTextEditor(frm);
    }

    public void NewTextDiff()
    {
      frmTextDiff frm = TextDiffFactory.CreateDiff();
      frm.Show();
    }

    public void NewWebBrowser()
    {
      frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, String.Empty);
      WebBrowserFactory.ShowWebBrowser(frm);
    }

    public void ShowObjectGropingForm()
    {
      if (_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.CreateObjectGroupingForm();
    }

    public void ShowSearchDatabaseForm()
    {
      if (_objectExplorer == null)
      {
        return;
      }
      _objectExplorer.CreateDatabaseSearchForm();
    }

    public void ShowObjectChnageHistoryViewer()
    {
      frmObjectChangeHistoryViewer frm = ObjectChangeHistoryViewerFactory.CreateViewer();
      ObjectChangeHistoryViewerFactory.ShowViewer(frm);
    }

    public void ShowSavedConnections()
    {
      frmConnectionRepository frm = new frmConnectionRepository();
      frm.UseForConnecting = false;
      frm.ShowDialog();
    }

    public void OpenProject()
    {
      OpenProject(String.Empty);
    }

    public void OpenProject(string fileName)
    {
      if (_projectExplorer == null)
      {
        return;
      }

      if (string.IsNullOrEmpty(fileName))
      {
        if (!_projectExplorer.OpenProject())
          return;
      }
      else
      {
        _projectExplorer.OpenProject(fileName);
      }

      ShowProjectExplorer();
    }

    private static void ReloadSyntaxHighlighters()
    {
      HighlightingManager.Manager.ReloadSyntaxModes();
      IList<ITextEditor> editors = HostServicesSingleton.HostServices.EditorServices.Editors;
      foreach (ITextEditor editor in editors)
      {
        editor.RefreshSyntaxHighlighter();
      }
    }

    #endregion  //MainMenu Commands

    #region Utils

    public int? DetermineStartPosition()
    {
      return _startPosition;
    }

    private void PrintStatMessage(string msg)
    {
      statMsg.Text = msg;
      if (Program.splashScreen == null)
        return;
      frmSplashScreen.PrintStatus(msg);
    }


    private void ClearStatMessage()
    {
      statMsg.Text = "Ready";
    }

    #endregion //Utils

    #region JumpList
    JumpList _jumpList = null;

    private void InitializeJumpList()
    {
      if (!TaskbarManager.IsPlatformSupported)
        return;

      try
      {
      if (_jumpList == null)
        _jumpList = JumpList.CreateJumpList();

      _jumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Neither;
      var path = Path.GetDirectoryName(Application.ExecutablePath);
      var exePath = Path.Combine(path, "PragmaSQL.exe");

      HostServicesSingleton.Svc.MsgService.InfoMsg(path, MethodInfo.GetCurrentMethod());
      HostServicesSingleton.Svc.MsgService.InfoMsg(exePath, MethodInfo.GetCurrentMethod());


      _jumpList.AddUserTasks(new JumpListLink(exePath,
                       "New Script")
      {
        Arguments = ((int)JumpListOperationsEnum.NewScript).ToString(),
        IconReference = new IconReference(Path.Combine(path, "sql.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListLink(exePath,
                     "New Text")
      {
        Arguments = ((int)JumpListOperationsEnum.NewText).ToString(),
        IconReference = new IconReference(Path.Combine(path, "text.ico"), 0)
      });



      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "New Diff")
      {
        Arguments = ((int)JumpListOperationsEnum.NewDiff).ToString(),
        IconReference = new IconReference(Path.Combine(path, "generic.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "New Browser")
      {
        Arguments = ((int)JumpListOperationsEnum.NewBrowser).ToString(),
        IconReference = new IconReference(Path.Combine(path, "globe.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListSeparator());
      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "Open File")
      {
        Arguments = ((int)JumpListOperationsEnum.OpenFile).ToString(),
        IconReference = new IconReference(Path.Combine(path, "folderopen.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "Open Script")
      {
        Arguments = ((int)JumpListOperationsEnum.OpenScript).ToString(),
        IconReference = new IconReference(Path.Combine(path, "folderopen.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListLink(exePath,
                    "Open Project")
      {
        Arguments = ((int)JumpListOperationsEnum.OpenProject).ToString(),
        IconReference = new IconReference(Path.Combine(path, "folderopen.ico"), 0)
      });
      _jumpList.AddUserTasks(new JumpListSeparator());
      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "Save Scripts For Recovery")
      {
        Arguments = ((int)JumpListOperationsEnum.SaveScriptsForRecovery).ToString(),
        IconReference = new IconReference(Path.Combine(path, "saveall.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListSeparator());
      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "Saved Connections")
      {
        Arguments = ((int)JumpListOperationsEnum.SavedConnections).ToString(),
        IconReference = new IconReference(Path.Combine(path, "dbs.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListSeparator());
      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "Connect to")
      {
        Arguments = ((int)JumpListOperationsEnum.ConnectTo).ToString(),
        IconReference = new IconReference(Path.Combine(path, "database1.ico"), 0)
      });

      _jumpList.AddUserTasks(new JumpListLink(exePath,
                      "Connection From List")
      {
        Arguments = ((int)JumpListOperationsEnum.ConnectionFromList).ToString(),
        IconReference = new IconReference(Path.Combine(path, "dbs.ico"), 0)
      });

      _jumpList.Refresh();
      }
      catch (Exception ex)
      {
        HostServicesSingleton.Svc.MsgService.ErrorMsg(ex, MethodInfo.GetCurrentMethod());
      }
    }

    #endregion //JumpList

    private void frmMain_Load(object sender, EventArgs e)
    {
      try
      {
        bool x = InitializeLayoutProvider();
        if (!x)
        {
          _layoutProvider.SuspendLayout();
          return;
        }
        else
        {
          _layoutProvider.ResumeLayout();
        }


        InitializeConfiguration();
        InitializeApplication();
        InitializePads();
        frmSplashScreen.CloseSplash();
        OpenCommandLineFiles();
        ApplyAutoSaveOptions(_configContent);
      }
      catch (Exception ex)
      {
        frmSplashScreen.CloseSplash();
        frmException.ShowAppError(ex);
      }
      finally
      {
        InitializeJumpList();
        PragmaSQLApp.CreateAppStartIndicator();
      }
    }

    private bool InitializeLayoutProvider()
    {
      return InitializeLayoutProviderEx();
    }

    private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
    {
      //PerformClose();
    }

    private void PerformClose()
    {
      try
      {
        CloseAllFloatingWindows();
        CloseDocuments(null);
        SaveLayout();

        CodeCompletionProposalCache.StopCleanupThread();
        _asWorker.Stop();
      }
      finally
      {
        PragmaSQLApp.RemoveAppStartIndicator();
      }
    }

    private void SaveLayout()
    {
      if (_canSaveLayout)
      {
        _dockPanel.SaveAsXml(_layoutConfigFile);
        if (mruMenu != null)
        {
          mruMenu.SaveToRegistry();
        }
      }
    }


    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_cancelClose)
      {
        e.Cancel = true;
        _cancelClose = false;
      }
      //else
      //{
      //  SaveLayout();
      //}
    }

    private void objectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToggleObjectExplorer();
    }

    private void localScriptTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToggleProjectExplorer();
    }

    private void dataSourcesRespoitoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowSavedConnections();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmAbout.ShowAbout();
    }

    private void closeAllDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (frmSaveScripts.ShowSaveScriptsDialog(this.DockPanel, null) == DialogResult.Cancel)
        return;
      CloseDocuments(null);
    }

    private void searchDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowSearchDatabaseForm();
    }

    private void newScriptToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      NewScript();
    }

    private void scriptFromFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void webBrowserToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NewWebBrowser();
    }

    private void objectGroupingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowObjectGropingForm();
    }

    private void OnSharedSnippetsFormClosed(object sender, FormClosedEventArgs e)
    {
      _sharedSnippets = null;
    }

    private void OnSharedScriptsFormClosed(object sender, FormClosedEventArgs e)
    {
      _sharedScripts = null;
    }

    private void frmMain_DragOver(object sender, DragEventArgs e)
    {
      string[] fileDrop = e.Data.GetData(DataFormats.FileDrop) as string[];
      if (fileDrop != null && fileDrop.Length > 0)
      {
        e.Effect = DragDropEffects.All;
        return;
      }

      string tmp = e.Data.GetData(typeof(string)) as string;
      if (!String.IsNullOrEmpty(tmp))
      {
        e.Effect = DragDropEffects.All;
        return;
      }
      e.Effect = DragDropEffects.None;

    }

    private void frmMain_DragDrop(object sender, DragEventArgs e)
    {
      string[] fileDrop = e.Data.GetData(DataFormats.FileDrop) as string[];
      if (fileDrop != null && fileDrop.Length > 0)
      {
        if (_objectExplorer == null)
        {
          return;
        }
        foreach (string file in fileDrop)
        {
          FileInfo fi = new FileInfo(file);
          string ext = fi.Extension.ToLowerInvariant();
          if (ext == ".sql" || ext == ".qry")
          {
            _objectExplorer.OpenFileInNewScriptEditor(file);
          }
          else
          {
            frmTextEditor editor = TextEditorFactory.OpenFile(file);
            TextEditorFactory.ShowTextEditor(editor);
          }

          this.Activate();
        }
        return;
      }

      string tmp = e.Data.GetData(typeof(string)) as string;
      if (!String.IsNullOrEmpty(tmp))
      {
        if (_objectExplorer == null)
        {
          return;
        }

        _objectExplorer.NewScriptEditor(tmp);
        e.Effect = DragDropEffects.All;
        return;
      }
      e.Effect = DragDropEffects.None;

    }

    private void objectChangeHToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowObjectChnageHistoryViewer();
    }

    private void sharedSnippetsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowSharedSnippets();
    }

    private void sharedScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ShowSharedScripts();
    }

    private void offlineEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NewTextEditor();
    }

    private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenProject();
    }

    private void mnuItemFile_DropDownOpening(object sender, EventArgs e)
    {

      int index = mnuItemFile.DropDownItems.IndexOf(mnuItemSepRecentFiles);
      if (index >= 0)
      {
        mnuItemFile.DropDownItems.RemoveAt(index);
        mnuItemFile.DropDownItems.Insert(mnuItemFile.DropDownItems.Count, mnuItemSepRecentFiles);
      }

      index = mnuItemFile.DropDownItems.IndexOf(autoSaveToolStripMenuItem);
      if (index >= 0)
      {
        mnuItemFile.DropDownItems.RemoveAt(index);
        mnuItemFile.DropDownItems.Insert(mnuItemFile.DropDownItems.Count, autoSaveToolStripMenuItem);
      }

      index = mnuItemFile.DropDownItems.IndexOf(mnuItemRecentFiles);
      if (index >= 0)
      {
        mnuItemFile.DropDownItems.RemoveAt(index);
        mnuItemFile.DropDownItems.Insert(mnuItemFile.DropDownItems.Count, mnuItemRecentFiles);
      }

      index = mnuItemFile.DropDownItems.IndexOf(mnuItemExit);
      if (index >= 0)
      {
        mnuItemFile.DropDownItems.RemoveAt(index);
        mnuItemFile.DropDownItems.Insert(mnuItemFile.DropDownItems.Count, mnuItemExit);
      }
    }

    private void textDiffToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NewTextDiff();
    }

    private void openScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NewScriptFromFile();
    }

    private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      NewTextFromFile();
    }

    private void applicationMessagesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ToggleMessagesForm();
    }

    private void searchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PerformWebSearch();
    }

    private void cmbWebSearch_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        PerformWebSearch();
      }
    }

    private void jumpToWebSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      cmbWebSearch.SelectAll();
      cmbWebSearch.Focus();
    }

    private void toolStripMenuItem6_Click(object sender, EventArgs e)
    {
      frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(String.Empty, "www.pragmasql.com");
      WebBrowserFactory.ShowWebBrowser(frm);
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void mnuOptions_Click(object sender, EventArgs e)
    {
      ShowOptionsDialog();
    }
    private frmWebBrowser _upgradeBrowser = null;
    private void mnuUpgradeProfessional_Click(object sender, EventArgs e)
    {
      if (_upgradeBrowser == null)
      {
        _upgradeBrowser = WebBrowserFactory.CreateAndBrowse(String.Empty, Properties.Settings.Default.DownloadUrl);
        _upgradeBrowser.FormClosed += new FormClosedEventHandler(_upgradeBrowser_FormClosed);
      }
      else
        _upgradeBrowser.Navigate(Properties.Settings.Default.DownloadUrl);

      WebBrowserFactory.ShowWebBrowser(_upgradeBrowser);
    }

    void _upgradeBrowser_FormClosed(object sender, FormClosedEventArgs e)
    {
      _upgradeBrowser = null;
    }

    private void mnuReloadHighlighters_Click(object sender, EventArgs e)
    {
      ReloadSyntaxHighlighters();
    }

    private void autoSaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      _asWorker.PerformAutoSave(true);
    }




  }

  public delegate void EnsureMainFormIsMaximizedDelegate();
  public delegate void ShowProjectExplorerDelegate();
  public delegate void ShowObjectExplorerDelegate();

  [Flags]
  public enum PadType
  {
    None = 0,
    ObjectExplorer = 1,
    ProjectExplorer = 2,
    Messages = 4,
    SharedSnippets = 8,
    SharedScripts = 16
  }
}