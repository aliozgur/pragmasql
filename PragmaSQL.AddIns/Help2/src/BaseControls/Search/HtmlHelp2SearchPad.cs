using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using HtmlHelp2.Environment;
using ICSharpCode.Core;
using MSHelpServices;
using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace HtmlHelp2
{
  public partial class HtmlHelp2SearchPad : DockContent, IPad
  {
    private static HtmlHelp2SearchPad _current = null;
    internal static HtmlHelp2SearchPad Current
    {
      get { return HtmlHelp2SearchPad._current; }
      set { HtmlHelp2SearchPad._current = value; }
    }

    private bool searchIsBusy;

    public HtmlHelp2SearchPad( )
    {
      InitializeComponent();
      UpdateControls();
      HtmlHelp2Environment.FilterQueryChanged += new EventHandler(FilterQueryChanged);
      HtmlHelp2Environment.NamespaceReloaded += new EventHandler(NamespaceReloaded);
    }

    public void FocusSearchTextBox( )
    {
      searchTerm.Focus();
    }

    public bool HiliteEnabled
    {
      get { return hiliteTopics.Checked; }
    }

    public void ShowPad( )
    {
      HostServicesSingleton.HostServices.ShowForm(this);
    }

    private void UpdateControls( )
    {
      titlesOnly.Enabled = HtmlHelp2Environment.SessionIsInitialized;
      enableStemming.Enabled = HtmlHelp2Environment.SessionIsInitialized;
      hiliteTopics.Enabled = HtmlHelp2Environment.SessionIsInitialized;
      filterCombobox.Enabled = HtmlHelp2Environment.SessionIsInitialized;
      searchTerm.Enabled = HtmlHelp2Environment.SessionIsInitialized;

      searchTerm.Text = string.Empty;
      searchTerm.Items.Clear();
      filterCombobox.Items.Clear();

      if (HtmlHelp2Environment.SessionIsInitialized)
      {
        HtmlHelp2Environment.BuildFilterList(filterCombobox);
      }
    }

    private void FilterChanged( object sender, EventArgs e )
    {
      string selectedFilterName = filterCombobox.SelectedItem.ToString();
      if (!string.IsNullOrEmpty(selectedFilterName))
      {
        HtmlHelp2Environment.FindFilterQuery(selectedFilterName);
      }
    }

    #region Help 2.0 Environment Events
    private void FilterQueryChanged( object sender, EventArgs e )
    {

      string selectedFilterName = filterCombobox.SelectedItem.ToString();
      if (string.Compare(selectedFilterName, HtmlHelp2Environment.CurrentFilterName) != 0)
      {
        filterCombobox.SelectedIndexChanged -= new EventHandler(FilterChanged);
        filterCombobox.SelectedIndex = filterCombobox.Items.IndexOf(HtmlHelp2Environment.CurrentFilterName);
        filterCombobox.SelectedIndexChanged += new EventHandler(FilterChanged);
      }
    }

    private void NamespaceReloaded( object sender, EventArgs e )
    {
      this.UpdateControls();

      if (HtmlHelp2Environment.SessionIsInitialized)
      {
        filterCombobox.SelectedIndexChanged -= new EventHandler(FilterChanged);
        HtmlHelp2Environment.BuildFilterList(filterCombobox);
        filterCombobox.SelectedIndexChanged += new EventHandler(FilterChanged);
      }
    }
    #endregion

    private void SearchButtonClick( object sender, EventArgs e )
    {
      PerformSearch(searchTerm.Text);
    }

    public void PerformSearch( string keyword)
    {
      searchTerm.Text = keyword;
      if (!string.IsNullOrEmpty(keyword))
      {
        this.AddTermToList(keyword);
        this.PerformFts(keyword);
      }
    }

    private void SearchTextChanged( object sender, EventArgs e )
    {
      searchButton.Enabled = (!string.IsNullOrEmpty(searchTerm.Text));
    }

    private void KeyPressed( object sender, KeyPressEventArgs e )
    {
      if (e.KeyChar == (char)13 && searchTerm.Text.Length > 0)
      {
        e.Handled = true;
        this.AddTermToList(searchTerm.Text);
        this.PerformFts(searchTerm.Text);
      }
    }

    private void AddTermToList( string searchText )
    {
      if (searchTerm.Items.IndexOf(searchText) == -1)
      {
        searchTerm.Items.Insert(0, searchText);
        if (searchTerm.Items.Count > 10) searchTerm.Items.RemoveAt(10);
        searchTerm.SelectedIndex = 0;
      }
    }

    #region FTS
    private void PerformFts( string searchWord )
    {
      this.PerformFts(searchWord, false);
    }

    private void PerformFts( string searchWord, bool useDynamicHelp )
    {
      if (!HtmlHelp2Environment.SessionIsInitialized || string.IsNullOrEmpty(searchWord) || searchIsBusy)
      {
        return;
      }

      HtmlHelp2SearchResultsView searchResults = HtmlHelp2SearchResults.Instance.View;

      HtmlHelp2Dialog searchDialog = new HtmlHelp2Dialog();
      try
      {
        searchIsBusy = true;
        IHxTopicList matchingTopics = null;

        HxQuery_Options searchFlags = HxQuery_Options.HxQuery_No_Option;
        searchFlags |= (titlesOnly.Checked) ? HxQuery_Options.HxQuery_FullTextSearch_Title_Only : HxQuery_Options.HxQuery_No_Option;
        searchFlags |= (enableStemming.Checked) ? HxQuery_Options.HxQuery_FullTextSearch_Enable_Stemming : HxQuery_Options.HxQuery_No_Option;
        searchFlags |= (reuseMatches.Checked) ? HxQuery_Options.HxQuery_FullTextSearch_SearchPrevious : HxQuery_Options.HxQuery_No_Option;

        searchDialog.Text = StringParser.Parse("${res:AddIns.HtmlHelp2.HelpSearchCaption}");
        searchDialog.ActionLabel = StringParser.Parse("${res:AddIns.HtmlHelp2.HelpSearchInProgress}",
                                                          new string[,] { { "0", searchWord } });
        searchDialog.Show();
        Application.DoEvents();
        Cursor.Current = Cursors.WaitCursor;
        if (useDynamicHelp)
          matchingTopics = HtmlHelp2Environment.GetMatchingTopicsForDynamicHelp(searchWord);
        else
          matchingTopics = HtmlHelp2Environment.Fts.Query(searchWord, searchFlags);

        Cursor.Current = Cursors.Default;

        try
        {
          searchResults.CleanUp();
          searchResults.SearchResultsListView.BeginUpdate();

          foreach (IHxTopic topic in matchingTopics)
          {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = topic.get_Title(HxTopicGetTitleType.HxTopicGetRLTitle,
                                               HxTopicGetTitleDefVal.HxTopicGetTitleFileName);
            lvi.Tag = topic;
            lvi.SubItems.Add(topic.Location);
            lvi.SubItems.Add(topic.Rank.ToString(CultureInfo.CurrentCulture));

            searchResults.SearchResultsListView.Items.Add(lvi);
          }

          reuseMatches.Enabled = true;
        }
        finally
        {
          searchResults.SearchResultsListView.EndUpdate();
          searchResults.SetStatusMessage(searchTerm.Text);
          HtmlHelp2SearchResults.Instance.BringPadToFront();
          searchIsBusy = false;
        }
      }
      catch (System.Runtime.InteropServices.COMException ex)
      {
        LoggingService.Error("Help 2.0: cannot get matching search word; " + ex.ToString());

        foreach (Control control in this.Controls)
        {
          control.Enabled = false;
        }
      }
      finally
      {
        searchDialog.Dispose();
      }
    }

    public bool PerformF1Fts( string keyword )
    {
      return this.PerformF1Fts(keyword, false);
    }

    public bool PerformF1Fts( string keyword, bool useDynamicHelp )
    {
      if (!HtmlHelp2Environment.SessionIsInitialized || string.IsNullOrEmpty(keyword) || searchIsBusy)
      {
        return false;
      }

      this.PerformFts(keyword, useDynamicHelp);

      HtmlHelp2SearchResultsView searchResults = HtmlHelp2SearchResults.Instance.View;
      return searchResults.SearchResultsListView.Items.Count > 0;
    }
    #endregion
  }
}