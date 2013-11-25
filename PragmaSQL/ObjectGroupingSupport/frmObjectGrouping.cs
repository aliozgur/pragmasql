/************************************************************************************************************
 * Ali Özgür
 * ali_ozgur@hotmail.com
 * www.pragmasql.com 
 * 
 * Source code included in this file can not be used without written
 * permissions of the owner mentioned above. 
 * All rigths reserver
 * Copyright PragmaSQL 2007 
 ************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using PragmaSQL.Core;
using ICSharpCode.TextEditor;

using Crad.Windows.Forms.Actions;

namespace PragmaSQL
{
  public partial class frmObjectGrouping : DockContent
  {
    private ActionList _actionList = new ActionList();

   

    public frmObjectGrouping( )
    {
#if PERSONAL_EDITION
      throw new PersonalEditionLimitation();
#endif
      
      InitializeComponent();
      InitializeActions();
    }

    public bool InitializeObjectGrouping(ConnectionParams connParams )
    {
      return ucObjectGrouping1.InitializeObjectGrouping(connParams);
    }

    private void InitializeActions()
    {
     Crad.Windows.Forms.Actions.Action ac = new Crad.Windows.Forms.Actions.Action();
      ac.Update +=new EventHandler(OnAction_FolderOnlyAction_Update);
      ac.Execute += new EventHandler(OnAction_AddSubFolder_Execute);
      ac.Text = "New Sub Folder";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemNewSubFolder, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.Execute += new EventHandler(OnAction_AddRootFolder_Execute);
      ac.Text = "New Root Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemNewRootFolder, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.F5;
      ac.Update +=new EventHandler(OnAction_FolderOnlyAction_Update);
      ac.Execute += new EventHandler(OnAction_RefreshFolder_Execute);
      ac.Text = "Refresh Folder";
      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemRefresh, ac);


     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.Control | Keys.F5;
      ac.Execute += new EventHandler(OnAction_LoadInitial_Execute);
      ac.Text = "Reload";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemReload, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.Control | Keys.Delete;
      ac.Update +=new EventHandler(OnAction_DeleteItem_Update);
      ac.Execute += new EventHandler(OnAction_DeleteItem_Execute);
      ac.Text = "Delete";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemRemoveSelected, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.F2;
      ac.Update +=new EventHandler(OnAction_FolderOnlyAction_Update);
      ac.Execute += new EventHandler(OnAction_RenameItem_Execute);
      ac.Text = "Rename Folder";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemRename, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.Control | Keys.X;
      ac.Update +=new EventHandler(OnAction_EditOperation_Update);
      ac.Execute += new EventHandler(OnAction_EditOperationCut_Execute);
      ac.Text = "Cut";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCut, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.Control | Keys.C;
      ac.Update +=new EventHandler(OnAction_EditOperation_Update);
      ac.Execute += new EventHandler(OnAction_EditOperationCopy_Execute);
      ac.Text = "Copy";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemCopy, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.Control | Keys.V;
      ac.Update +=new EventHandler(OnAction_EditOperation_Update);
      ac.Execute += new EventHandler(OnAction_EditOperationPaste_Execute);
      ac.Text = "Paste";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemPaste, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.Control | Keys.S;
      ac.Update +=new EventHandler(OnAction_SaveHelpText_Update);
      ac.Execute += new EventHandler(OnAction_SaveHelpText_Execute);
      ac.Text = "Save HelpText";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemSaveHelpText, ac);

     ac = new Crad.Windows.Forms.Actions.Action();
      ac.ShortcutKeys = Keys.Control | Keys.Shift | Keys.H;
      ac.Update +=new EventHandler(OnAction_ShowHelpText_Update);
      ac.Execute += new EventHandler(OnAction_ShowHelpText_Execute);
      ac.Text = "Show HelpText";

      _actionList.Actions.Add(ac);
      _actionList.SetAction(mnuItemShowHelpText, ac);

      

    }

    private void OnAction_FolderOnlyAction_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;  
      ac.Enabled = ( ucObjectGrouping1 != null && ucObjectGrouping1.CanPerformFolderOnlyAction());
    }


    private void OnAction_AddRootFolder_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.AddFolder(null);
    }

    private void OnAction_AddSubFolder_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.AddFolderUnderSelectedNode();
    }

   
    private void OnAction_RefreshFolder_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.RefreshFolder();
    }

    private void OnAction_LoadInitial_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.LoadInitial();
    }

    private void OnAction_DeleteItem_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;

      ac.Enabled = ( ucObjectGrouping1 != null && ucObjectGrouping1.CanDeleteItem());
    }

   
    private void OnAction_DeleteItem_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.DeleteSelectedItems();
    }

    
    private void OnAction_RenameItem_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.RenameItem();
    }

    private void OnAction_EditOperation_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = ( ucObjectGrouping1 != null && ucObjectGrouping1.CanPerformEditOperation());
    }
    
    private void OnAction_EditOperationCut_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.PerformCut();
    }
    
    private void OnAction_EditOperationCopy_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.PerformCopy();
    }

    private void OnAction_EditOperationPaste_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.PerformPaste();
    }
   
    private void OnAction_SaveHelpText_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = ( ucObjectGrouping1 != null && ucObjectGrouping1.CanPerformSaveHelpText());
    }
    private void OnAction_SaveHelpText_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.SaveHelpTextIfChanged(true);
    }

    private void OnAction_ShowHelpText_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = ( ucObjectGrouping1 != null && ucObjectGrouping1.CanShowHelpText());
      
    }
    private void OnAction_ShowHelpText_Execute( object sender, EventArgs e )
    {
      ucObjectGrouping1.HelpTextVisible = !ucObjectGrouping1.HelpTextVisible;
    }
    
    private void OnAction_ToggleHelpTextSource_Update( object sender, EventArgs e )
    {
      Crad.Windows.Forms.Actions.Action ac = sender as Crad.Windows.Forms.Actions.Action;
      ac.Enabled = ( ucObjectGrouping1 != null );
    }
    

    private void cMnuItemClose_Click( object sender, EventArgs e )
    {
      Close();
    }

    private void cMnuCloseAll_Click( object sender, EventArgs e )
    {
      Program.MainForm.CloseDocuments(null);
    }

    private void cMnuCloseAllButThis_Click( object sender, EventArgs e )
    {
      Program.MainForm.CloseDocuments(this);
    }

    private void ucObjectGrouping1_ConnectionParamsChanged( object sender, string serverName, string databaseName, bool hasError )
    {
      if(hasError)
      {
        this.Text = "Object Grouping";  
        this.TabText = this.Text;
      }
      else
      {
        this.Text = "Object Grouping " + serverName + " {" + databaseName + "}";  
        this.TabText = this.Text;      
      }
    }
  }
}