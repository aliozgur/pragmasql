/********************************************************************
  Class      : frmSearchAndReplace
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
  public enum SearchAndReplaceDialogMode
  {
    Replace,
    Search
  }

  public partial class SearchAndReplaceForm : KryptonForm
  {
    #region Static Fields And Properties

    private static string globalSearchCr = String.Empty;
    private static string globalReplaceCr = String.Empty;

    private static SearchAndReplaceDialogMode _mode = SearchAndReplaceDialogMode.Search;
    public static SearchAndReplaceDialogMode Mode
    {
      get { return _mode; }
    }

    private static string _initialSearchString = String.Empty;
    public static string InitialSearchText
    {
      get
      {
        if (_instance == null)
          return String.Empty;

        return _initialSearchString;
      }
      set
      {
        if (_instance == null)
          return;

        _initialSearchString = value;      
        _instance.cmbSearchHist.Text = value;
      }
    }
    
    private static SearchEventHandler _searchRequested;
    public static event SearchEventHandler SearchRequested
    {
      add
      {
        _searchRequested += value;
      }
      remove
      {
        _searchRequested -= value;
      }

    }

    private static ReplaceEventHandler _replaceRequested;
    public static event ReplaceEventHandler ReplaceRequested
    {
      add
      {
        _replaceRequested += value;
      }
      remove
      {
        _replaceRequested -= value;
      }
    }

    private static FormClosedEventHandler _searchAndReplaceFormClosed;
    public static event FormClosedEventHandler SearchAndReplaceFormClosed
    {
      add
      {
        _searchAndReplaceFormClosed += value;
      }
      remove
      {
        _searchAndReplaceFormClosed -= value;
      }
    }

    private static SearchAndReplaceForm _instance;
    public static SearchAndReplaceForm Instance
    {
      get { return _instance; }
    }

    public static string CurrentSearchText
    {
      get 
      {
        if (_instance == null)
          return String.Empty;

        return _instance.cmbSearchHist.Text;
      }
    }


    #endregion //Static Fields And Properties

    #region Instance Fields And Properties
    private SearchAndReplaceHistory _hist;
    private int MaxHistItemCnt
    {
      get 
      {
        if (ConfigHelper.Current == null || ConfigHelper.Current.TextEditorOptions == null)
          return TextEditorOptions.defSearchAndReplaceItemCnt;
        else
          return ConfigHelper.Current.TextEditorOptions.SearchAndReplaceItemCnt;
      }
    }

    #endregion //Instance Fields And Properties
    
    #region CTOR

    public SearchAndReplaceForm()
    {
      InitializeComponent();
      
      foreach (SearchType searchType in Enum.GetValues(typeof(SearchType)))
      {
        cmbSearchType.Items.Add(searchType.ToString()[0] + Regex.Replace(searchType.ToString().Substring(1), @"(\B[A-Z])", " $1").ToLower());
      }
      
      cmbSearchType.SelectedIndex = 0;

      try
      {
        _hist = SearchAndReplaceHistoryManager.LoadFromDefault();
        cmbSearchHist.Items.AddRange(_hist.SearchHistoryArray);
        cmbReplaceHist.Items.AddRange(_hist.ReplaceHistoryArray);
      }
      catch { }
      
      cmbSearchHist.Text = globalSearchCr;
      cmbReplaceHist.Text = globalReplaceCr;
    }

    #endregion //CTOR

    #region Static Methods
    
    public static void ShowSearchAndReplaceForm(SearchAndReplaceDialogMode mode)
    {
      if (_instance == null)
      {
        _instance = new SearchAndReplaceForm();
        _instance.Owner = Program.MainForm;
      }

      _mode = mode;
      ValidateMode(_mode);
      _instance.Show();
    }

    public static void RegisterToEvents(Form owner, SearchEventHandler searchRequestDelegate
      , ReplaceEventHandler replaceRequestDelegate
      , FormClosedEventHandler formClosedDelegate
      )
    {
      if (_instance == null)
        return;
      
      if (searchRequestDelegate != null)
        SearchRequested += searchRequestDelegate;

      if (replaceRequestDelegate != null)
        ReplaceRequested += replaceRequestDelegate;

      
      if(formClosedDelegate != null)
        _searchAndReplaceFormClosed += formClosedDelegate;
    }

    public static void UnRegisterFromEvents(SearchEventHandler searchRequestDelegate
      , ReplaceEventHandler replaceRequestDelegate
      , FormClosedEventHandler formClosedDelegate)
    {

      if (_instance == null)
        return;


      if (searchRequestDelegate != null)
        SearchRequested -= searchRequestDelegate;

      if (replaceRequestDelegate != null)
        ReplaceRequested -= replaceRequestDelegate;

      if (formClosedDelegate != null)
        _searchAndReplaceFormClosed -= formClosedDelegate;
    }

    #endregion //Static Methods

    #region Search And Replace Methods

    private Regex PrepareSearch()
    {
      string text;

      RegexOptions options = RegexOptions.None;

      // Ignore case is simply a regular expression parameter
      if (chkIgnoreCase.Checked)
      {
        options |= RegexOptions.IgnoreCase;
      }

      switch ((SearchType)cmbSearchType.SelectedIndex)
      {
        case SearchType.RegularExpression:
          // If user selected Regular Expression, just pass the text directly
          text = cmbSearchHist.Text;
          break;

        case SearchType.Wildcards:
          // Escapes the text, then converts wildcard tokens to regex equivalents
          text = Regex.Escape(cmbSearchHist.Text).Replace(@"\*", ".*").Replace(@"\?", ".");
          break;

        case SearchType.PlainText:
        default:
          // Just a plain text search... 'escape' the text so it can be used in the regular expression
          text = Regex.Escape(cmbSearchHist.Text);
          break;
      }

      Regex searchRegularExpression = null;
      try
      {
        searchRegularExpression = new Regex( text, options);
      }
      catch (Exception exception)
      {
        // relay the error to the user
        MessageBox.Show(this, exception.Message, "Regular expression error");
      }
      return searchRegularExpression;
    }

    private static void RequestSearch()
    {
      if (_instance == null)
        return;

      Regex searchRegularExpression = _instance.PrepareSearch();
      if (searchRegularExpression != null)
      {
        if (_searchRequested != null)
        {
          SearchEventArgs args = new SearchEventArgs(searchRegularExpression);
          _searchRequested(_instance, args);
        }
      }
    }

    private static void RequestReplace()
    {
      if (_instance == null)
        return;
      
      Regex searchRegularExpression = _instance.PrepareSearch();
      if (_replaceRequested != null)
      {
        if (_replaceRequested != null)
        {
          ReplaceEventArgs args = new ReplaceEventArgs(searchRegularExpression, _instance.cmbReplaceHist.Text, false);
          _replaceRequested(_instance, args);
        }
      }
    }

    private static void RequestReplaceAll()
    {

      if (_instance == null)
        return;

      Regex searchRegularExpression = _instance.PrepareSearch();
      if (_replaceRequested != null)
      {
        if (_replaceRequested != null)
        {
          ReplaceEventArgs args = new ReplaceEventArgs(searchRegularExpression, _instance.cmbReplaceHist.Text, true);
          _replaceRequested(_instance, args);
        }
      }
    }

    #endregion //Search And Replace Methods

    #region Utility Methods

    public static void ValidateMode(SearchAndReplaceDialogMode mode)
    {
      _instance.ValidateMode_Internal(mode);
    }
    
    private void ValidateMode_Internal( SearchAndReplaceDialogMode mode )
    {
      switch (mode)
      {
        case SearchAndReplaceDialogMode.Search:
          this.Height = panMin.Height + 24;
          chkReplace.Checked = false;
          SetReplaceControlsVisibility(false);
          SetSearchControlsVisibility(true);

          this.AcceptButton = searchButton;
          this.Text = "PragmaSQL Search";
          break;
        case SearchAndReplaceDialogMode.Replace:
          this.Height = panMax.Height + 24;
          chkReplace.Checked = true;
          SetReplaceControlsVisibility(true);
          SetSearchControlsVisibility(false);
          this.AcceptButton = replaceButton;
          this.Text = "PragmaSQL Replace";
          break;
      }
    }
    
    private void SetReplaceControlsVisibility(bool value)
    {
      replaceAllButton.Visible = value;
      replaceButton.Visible = value;
      cmbReplaceHist.Visible = value;
      lblReplaceWith.Visible = value;
    }

    private void SetSearchControlsVisibility(bool value)
    {
      searchButton.Visible = value;
    }

    private void PersistSearchHistory(bool canSave)
    {
      if (_hist == null || String.IsNullOrEmpty(cmbSearchHist.Text))
        return;

      if (!_hist.SearchHistory.Contains(cmbSearchHist.Text))
      {
      
        _hist.SearchHistory.Add(cmbSearchHist.Text);
        cmbSearchHist.Items.Add(cmbSearchHist.Text);
        if (_hist.SearchHistory.Count > MaxHistItemCnt)
        {
          _hist.SearchHistory.RemoveAt(0);
          cmbSearchHist.Items.RemoveAt(0);
        }
        
        try
        {
          if (canSave)
            SearchAndReplaceHistoryManager.SaveAsDefault(_hist);
        }
        catch { }
      }
    }

    private void PersistReplaceHistory()
    {
      if (_hist == null || String.IsNullOrEmpty(cmbReplaceHist.Text))
        return;

      PersistSearchHistory(false);

      if (cmbReplaceHist.FindString(cmbReplaceHist.Text) < 0 )
      {
        _hist.ReplaceHistory.Add(cmbReplaceHist.Text);
        cmbReplaceHist.Items.Add(cmbReplaceHist.Text);
        
        if (_hist.ReplaceHistory.Count > MaxHistItemCnt)
        {
          _hist.ReplaceHistory.RemoveAt(0);
          cmbReplaceHist.Items.RemoveAt(0);
        }

        try
        {
          SearchAndReplaceHistoryManager.SaveAsDefault(_hist);
        }
        catch { }
      }

    }

    #endregion //Utility Methods


    private void cancelButton_Click( object sender, EventArgs e )
    {
      this.Close();
    }

    private void chkReplace_CheckedChanged( object sender, EventArgs e )
    {
      if (chkReplace.Checked)
      {
        ValidateMode(SearchAndReplaceDialogMode.Replace);
      }
      else
      {
        ValidateMode(SearchAndReplaceDialogMode.Search);
      }
    }
    
    private void searchButton_Click(object sender, EventArgs e)
    {
      if (_searchRequested == null)
      {
        Utils.ShowWarning(Properties.Resources.SelectDocumentForSearchAndReplace, MessageBoxButtons.OK);
        return;
      }

      globalSearchCr = cmbSearchHist.Text;
      RequestSearch();
      PersistSearchHistory(true);
    }

    private void replaceButton_Click(object sender, EventArgs e)
    {
      if (_replaceRequested == null)
      {
        Utils.ShowWarning(Properties.Resources.SelectDocumentForSearchAndReplace, MessageBoxButtons.OK);
        return;
      }

      globalSearchCr = cmbSearchHist.Text;
      globalReplaceCr = cmbReplaceHist.Text;
      RequestReplace();
      PersistReplaceHistory();
    }

    private void replaceAllButton_Click(object sender, EventArgs e)
    {
      if (_replaceRequested == null)
      {
        Utils.ShowWarning(Properties.Resources.SelectDocumentForSearchAndReplace, MessageBoxButtons.OK);
        return;
      }

      globalSearchCr = cmbSearchHist.Text;
      globalReplaceCr = cmbReplaceHist.Text;
      RequestReplaceAll();
      PersistReplaceHistory();

    }

    private void SearchAndReplaceForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (_searchAndReplaceFormClosed != null)
        _searchAndReplaceFormClosed(this, e);
      _instance = null;
    }

    private void SearchAndReplaceForm_Load(object sender, EventArgs e)
    {

    }


   

    

  }
}