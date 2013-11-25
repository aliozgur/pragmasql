using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

using AxMSHelpControls;
using MSHelpControls;
using MSHelpServices;
using HtmlHelp2.Environment;

using ICSharpCode.Core;

namespace HtmlHelp2
{
  public partial class MSHelp2IndexControl : UserControl
  {
    AxHxIndexCtrl indexControl;
    bool indexControlFailed;
    bool itemClicked;

    [PermissionSet(SecurityAction.LinkDemand, Name = "Execution")]
    public MSHelp2IndexControl( )
    {
      InitializeComponent();
      InitializeIndexControl();
      this.UpdateControls();
      HtmlHelp2Environment.FilterQueryChanged += new EventHandler(this.FilterQueryChanged);
      HtmlHelp2Environment.NamespaceReloaded += new EventHandler(this.NamespaceReloaded);
    }

    private void UpdateControls( )
    {
      filterCombobox.Enabled =
        (HtmlHelp2Environment.SessionIsInitialized && !this.indexControlFailed);
      searchTerm.Enabled =
        (HtmlHelp2Environment.SessionIsInitialized && !this.indexControlFailed);
      infoLabel.Visible = false;

      if (this.indexControlFailed)
      {
        this.ShowInfoMessage
          ("Help system is not available!");
      }
      else if (!HtmlHelp2Environment.SessionIsInitialized)
      {
        if (indexControl != null) indexControl.Visible = false;
        this.ShowInfoMessage
          ("Help collection is empty!");
      }
      else
      {
        indexControl.Visible = true;
        this.LoadIndex();
      }
    }

    private void InitializeIndexControl( )
    {
      if (Help2ControlsValidation.IsIndexControlRegistered)
      {
        try
        {
          indexControl = new AxHxIndexCtrl();
          indexControl.BeginInit();
          this.panIndexControl.Controls.Add(indexControl);
          indexControl.Parent = panIndexControl;
          indexControl.Dock = DockStyle.Fill;
          indexControl.ItemClick += new AxMSHelpControls.IHxIndexViewEvents_ItemClickEventHandler(this.IndexItemClick);
          indexControl.EndInit();
          indexControl.CreateControl();
          indexControl.BorderStyle = HxBorderStyle.HxBorderStyle_FixedSingle;
          indexControl.FontSource = HxFontSourceConstant.HxFontExternal;
        }
        catch (System.Runtime.InteropServices.COMException ex)
        {
          LoggingService.Error("Help 2.0: Index control failed; " + ex.ToString());
          this.indexControlFailed = true;
        }
      }

      this.indexControlFailed = (this.indexControlFailed || indexControl == null);

    }

    private void ShowInfoMessage( string infoText )
    {
      filterCombobox.Items.Clear();
      searchTerm.Items.Clear();
      searchTerm.Text = string.Empty;
      infoLabel.Text = infoText;
      infoLabel.Visible = true;
    }

    public void RedrawContent( )
    {
      label1.Text = "Filter By";// StringParser.Parse("${res:AddIns.HtmlHelp2.FilteredBy}");
      label2.Text = "Look For"; // StringParser.Parse("${res:AddIns.HtmlHelp2.LookFor}");
    }

    private void IndexItemClick( object sender, IHxIndexViewEvents_ItemClickEvent e )
    {
      string indexTerm = indexControl.IndexData.GetFullStringFromSlot(e.iItem, ",");
      int indexSlot = e.iItem;

      itemClicked = true;
      searchTerm.Items.Insert(0, indexTerm);
      searchTerm.SelectedIndex = 0;
      itemClicked = false;

      this.ShowSelectedItemEntry(indexTerm, indexSlot);
    }

    private void FilterChanged( object sender, EventArgs e )
    {
      string selectedFilterName = filterCombobox.SelectedItem.ToString();
      if (!string.IsNullOrEmpty(selectedFilterName))
      {
        Cursor.Current = Cursors.WaitCursor;
        this.SetIndex(selectedFilterName);
        Cursor.Current = Cursors.Default;
      }
    }

    private void SearchTextChanged( object sender, EventArgs e )
    {
      if (!this.itemClicked && searchTerm.Text.Length > 0)
      {
        indexControl.Selection =
          indexControl.IndexData.GetSlotFromString(searchTerm.Text);
      }
    }

    private void SearchKeyPress( object sender, KeyPressEventArgs e )
    {
      if (e.KeyChar == (char)13)
      {
        PerformSearch(searchTerm.Text);
      }
    }

    public void PerformSearch(string keyword )
    {
      int indexSlot = indexControl.IndexData.GetSlotFromString(keyword);
      string indexTerm = indexControl.IndexData.GetFullStringFromSlot(indexSlot, ",");

      searchTerm.Items.Insert(0, indexTerm);
      searchTerm.SelectedIndex = 0;
      if (searchTerm.Items.Count > 10)
      {
        searchTerm.Items.RemoveAt(10);
      }

      this.ShowSelectedItemEntry(indexTerm, indexSlot);
    }

    private void LoadIndex( )
    {
      if (this.SetIndex(HtmlHelp2Environment.CurrentFilterName))
      {
        searchTerm.Text = string.Empty;
        searchTerm.Items.Clear();
        filterCombobox.SelectedIndexChanged -= new EventHandler(this.FilterChanged);
        HtmlHelp2Environment.BuildFilterList(filterCombobox);
        filterCombobox.SelectedIndexChanged += new EventHandler(this.FilterChanged);
      }
    }

    private bool SetIndex( string filterName )
    {
      try
      {
        indexControl.IndexData =
          HtmlHelp2Environment.GetIndex(HtmlHelp2Environment.FindFilterQuery(filterName));
        return true;
      }
      catch (System.Runtime.InteropServices.COMException)
      {
        LoggingService.Error("Help 2.0: cannot connect to IHxIndex interface (Index)");
        return false;
      }
    }

    private void ShowSelectedItemEntry( string indexTerm, int indexSlot )
    {
  
      if (HtmlHelp2IndexResultsPad.Current == null)
      {
        ShowIndexResultsMenuCommand cmd = new ShowIndexResultsMenuCommand();
        cmd.Run();
        if (HtmlHelp2IndexResultsPad.Current == null)
        {
          return; 
        }
      }

      try
      {
        IHxTopicList matchingTopics = indexControl.IndexData.GetTopicsFromSlot(indexSlot);

        try
        {
          HtmlHelp2IndexResultsPad.Current.CleanUp();
          HtmlHelp2IndexResultsPad.Current.IndexResultsListView.BeginUpdate();

          foreach (IHxTopic topic in matchingTopics)
          {
            ListViewItem lvi = new ListViewItem();
            lvi.Text =
              topic.get_Title(HxTopicGetTitleType.HxTopicGetRLTitle,
                              HxTopicGetTitleDefVal.HxTopicGetTitleFileName);
            lvi.Tag = topic;
            lvi.SubItems.Add(topic.Location);
            HtmlHelp2IndexResultsPad.Current.IndexResultsListView.Items.Add(lvi);
          }
        }
        finally
        {
          HtmlHelp2IndexResultsPad.Current.IndexResultsListView.EndUpdate();
          HtmlHelp2IndexResultsPad.Current.SortLV(0);
          HtmlHelp2IndexResultsPad.Current.SetStatusMessage(indexTerm);
        }

        switch (matchingTopics.Count)
        {
          case 0:
            break;
          case 1:
            IHxTopic topic = (IHxTopic)matchingTopics.ItemAt(1);
            if (topic != null) WebBrowserHelper.OpenHelpView(topic);
            break;
          default:
            HtmlHelp2IndexResultsPad.Current.BringPadToFront();
            break;
        }
      }
      catch (System.Runtime.InteropServices.COMException cEx)
      {
        LoggingService.Error("Help 2.0: cannot get matching index entries; " + cEx.ToString());
      }
    }

    #region Help 2.0 Environment Events
    private void FilterQueryChanged( object sender, EventArgs e )
    {
      Application.DoEvents();

      string currentFilterName = filterCombobox.SelectedItem.ToString();
      if (string.Compare(currentFilterName, HtmlHelp2Environment.CurrentFilterName) != 0)
      {
        filterCombobox.SelectedIndexChanged -= new EventHandler(this.FilterChanged);
        filterCombobox.SelectedIndex =
          filterCombobox.Items.IndexOf(HtmlHelp2Environment.CurrentFilterName);
        this.SetIndex(HtmlHelp2Environment.CurrentFilterName);
        filterCombobox.SelectedIndexChanged += new EventHandler(this.FilterChanged);
      }
    }

    private void NamespaceReloaded( object sender, EventArgs e )
    {
      this.UpdateControls();
    }
    #endregion
  }
}
