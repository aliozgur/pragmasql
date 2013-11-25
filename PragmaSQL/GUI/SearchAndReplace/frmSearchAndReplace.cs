/********************************************************************
  Class      : frmSearchAndReplace
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PragmaSQL.GUI
{
  enum SearchType
  {
    PlainTextSearch,
    Wildcards,
    RegularExpression
  }

  public enum SearchAndRelaceDialogMode
  {
    Replace,
    Search
  }

  public partial class frmSearchAndReplace : Form
  {

    private SearchEventHandler searchRequested;
    public event SearchEventHandler SearchRequested
    {
      add
      {
        searchRequested += value;
      }
      remove
      {
        searchRequested -= value;
      }

    }

    private ReplaceEventHandler replaceRequested;
    public event ReplaceEventHandler ReplaceRequested
    {
      add
      {
        replaceRequested += value;
      }
      remove
      {
        replaceRequested -= value;
      }
    }

    public frmSearchAndReplace( )
    {
      InitializeComponent();
      foreach (SearchType searchType in Enum.GetValues(typeof(SearchType)))
      {
        searchTypeComboBox.Items.Add(searchType.ToString()[0] + Regex.Replace(searchType.ToString().Substring(1), @"(\B[A-Z])", " $1").ToLower());
      }
      searchTypeComboBox.SelectedIndex = 0;
    }

    public void ValidateMode( SearchAndRelaceDialogMode mode )
    {
      switch (mode)
      {
        case SearchAndRelaceDialogMode.Search:
          this.Height = panFind.Height + panOptions.Height + 24;
          panReplace.Visible = false;
          chkReplace.Checked = false;
          this.AcceptButton = searchButton;
          break;
        case SearchAndRelaceDialogMode.Replace:
          this.Height = panReplace.Height + panFind.Height + panOptions.Height + 24;
          panReplace.Visible = true;
          chkReplace.Checked = true;
          this.AcceptButton = replaceButton;
          break;
      }
    }

    private void cancelButton_Click( object sender, EventArgs e )
    {
      this.Hide();
    }

    private void chkReplace_CheckedChanged( object sender, EventArgs e )
    {
      if (chkReplace.Checked)
      {
        ValidateMode(SearchAndRelaceDialogMode.Replace);
      }
      else
      {
        ValidateMode(SearchAndRelaceDialogMode.Search);
      }
    }

    private Regex PrepareSearch()
    {
      string text;

      RegexOptions options = RegexOptions.None;

      // Ignore case is simply a regular expression parameter
      if (chkIgnoreCase.Checked)
      {
        options |= RegexOptions.IgnoreCase;
      }

      switch ((SearchType)searchTypeComboBox.SelectedIndex)
      {
        case SearchType.RegularExpression:
          // If user selected Regular Expression, just pass the text directly
          text = Regex.Escape(searchHistoryComboBox.Text).Replace(@"\*", ".*").Replace(@"\?", ".");
          break;

        case SearchType.Wildcards:
          // Escapes the text, then converts wildcard tokens to regex equivalents
          text = searchHistoryComboBox.Text;
          break;

        case SearchType.PlainTextSearch:
        default:
          // Just a plain text search... 'escape' the text so it can be used in the regular expression
          text = Regex.Escape(searchHistoryComboBox.Text);
          break;
      }
      Regex searchRegularExpression = null;
      try
      {
        searchRegularExpression = new Regex(text, options);
      }
      catch (Exception exception)
      {
        // relay the error to the user
        MessageBox.Show(this, exception.Message, "Regular expression error");
      }
      return searchRegularExpression;
    }

    private void RequestSearch( )
    {
      
      Regex searchRegularExpression = PrepareSearch();
      if (searchRegularExpression != null)
      {
        if (searchRequested != null)
        {
          SearchEventArgs args = new SearchEventArgs(searchRegularExpression);
          searchRequested(this, args);
        }
      }
    }

    private void RequestReplace( )
    {
      
      Regex searchRegularExpression = PrepareSearch();
      if (replaceRequested != null)
      {
        if (replaceRequested != null)
        {
          ReplaceEventArgs args = new ReplaceEventArgs(searchRegularExpression,replaceHistoryComboBox.Text,false);
          replaceRequested(this, args);
        }
      }
    }

    private void RequestReplaceAll( )
    {
      
      Regex searchRegularExpression = PrepareSearch();
      if (replaceRequested != null)
      {
        if (replaceRequested != null)
        {
          ReplaceEventArgs args = new ReplaceEventArgs(searchRegularExpression,replaceHistoryComboBox.Text,true);
          replaceRequested(this, args);
        }
      }
    }

    private void searchButton_Click( object sender, EventArgs e )
    {
      // Add text to history
      if ((searchHistoryComboBox.Items.Count == 0) || !searchHistoryComboBox.Text.Equals(searchHistoryComboBox.Items[searchHistoryComboBox.Items.Count - 1]))
      {
        searchHistoryComboBox.Items.Add(searchHistoryComboBox.Text);
      }
      
      RequestSearch();
    }

    private void replaceButton_Click( object sender, EventArgs e )
    {
      // Add text to history
      if ((replaceHistoryComboBox.Items.Count == 0) || !replaceHistoryComboBox.Text.Equals(replaceHistoryComboBox.Items[replaceHistoryComboBox.Items.Count - 1]))
      {
        replaceHistoryComboBox.Items.Add(replaceHistoryComboBox.Text);
      }
      
      RequestReplace();
    }

    private void replaceAllButton_Click( object sender, EventArgs e )
    {
      // Add text to history
      if ((replaceHistoryComboBox.Items.Count == 0) || !replaceHistoryComboBox.Text.Equals(replaceHistoryComboBox.Items[replaceHistoryComboBox.Items.Count - 1]))
      {
        replaceHistoryComboBox.Items.Add(replaceHistoryComboBox.Text);
      }
      RequestReplaceAll();
    }
    

  }
}