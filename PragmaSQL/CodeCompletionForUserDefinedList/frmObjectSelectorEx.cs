using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;
namespace PragmaSQL
{
  public partial class frmObjectSelectorEx : KryptonForm
  {
    private DataSet _lists = new DataSet();
    private BindingSource _bsListItems = new BindingSource();
    private IList<string> _tblNames = new List<string>();

    private DataTable _tblSnippets = null;

    private string _jumpString = String.Empty;
    private int _currentListIndex = -1;

    private bool _isInitialized = false;
    public bool IsInitialized
    {
      get { return _isInitialized; }
    }

    public event NotificationHandler UserRequestedDismiss;
    public event NotificationHandler UserMadeFinalSelection;
    public event KeyPressHandler UserPressedKey;
    public event NotificationHandler UserPressedBackSpace;

    public frmObjectSelectorEx( )
    {
      InitializeComponent();
      LoadCodeCompletionLists();
      MoveToNext();
    }

    #region Methods

    public void SetLocation( int x, int y )
    {
      int tmpHeight = 400;
      this.Height = 0;
      //this.Location = new Point(x, y + 15);
      Screen scr = Screen.FromControl(this);
      Rectangle r = scr.WorkingArea;
      if (y + tmpHeight > r.Bottom)
      {
        if ((y - tmpHeight) < r.Top)
        {
          this.Location = new Point(x, 0);
          this.Height = y - 15;
        }
        else
        {
          this.Location = new Point(x, y - tmpHeight - 15);
          this.Height = 400;
        }
      }
      else
      {
        this.Location = new Point(x, y + 15);
        this.Height = 400;
      }
    }

    public string SelectedItem
    {
      get
      {
        DataRowView row = _bsListItems.Current as DataRowView;
        if (row == null)
        {
          return String.Empty;
        }

        return (string)row["Value"];
      }
    }

    public ArrayList SelectedItemsAsStrings
    {
      get
      {
        ArrayList results = new ArrayList();
        foreach (DataGridViewRow row in grdItems.SelectedRows)
        {
          results.Add((string)row.Cells["colValue"].Value);
        }
        return results;
      }
    }

    public bool HasMultipleSelection
    {
      get
      {
        return (grdItems.SelectedRows.Count > 1);
      }
    }

    private void BindGrid( )
    {
      if (_bsListItems.DataSource == null)
      {
        return;
      }

      grdItems.DataSource = _bsListItems;
      foreach (DataGridViewColumn col in grdItems.Columns)
      {
        if (col.DataPropertyName == "DisplayValue" || col.DataPropertyName == "DisplayName")
        {
          col.Visible = true;
        }
        else
        {
          col.Visible = false;
        }
      }
      _bsListItems.Sort = "DisplayName ASC";
    }

    public void LoadCodeCompletionLists( )
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        _currentListIndex = -1;
        DataSet ds = CodeCompletionListLoader.ListsAsDataSet;
        _lists.Clear();
        _lists.Tables.Clear();
        _tblNames.Clear();
        _tblSnippets = null;

        foreach (DataTable tbl in ds.Tables)
        {
          _tblNames.Add(tbl.TableName);
          _lists.Tables.Add(tbl.Copy());
        }
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    public void RefreshSharedSnippetsData( )
    {
      int tmpIndex = -1;
      string tmpName = String.Empty;

      if (_currentListIndex >= 0 && _currentListIndex < _tblNames.Count)
      {
        tmpIndex = _currentListIndex;
        tmpName = _tblNames[_currentListIndex];
      }

      RefreshSharedSnippetsDataInner();

      if (tmpIndex >= 0 && tmpIndex < _tblNames.Count && _tblNames[tmpIndex] == tmpName)
      {
        MoveTo(tmpIndex);
      }
      else
      {
        MoveToNext();
      }

      _bsListItems.Filter = String.Empty;
      _bsListItems.Sort = String.Empty;

    }

    private void RefreshSharedSnippetsDataInner( )
    {
      if (_tblSnippets != null)
      {
        _lists.Tables.Remove(_tblSnippets);
        _tblNames.Remove(_tblSnippets.TableName);
      }

      if (SharedSnippetProvider.SnippetsData == null)
      {
        _tblSnippets = null;
        return;
      }
      else
      {
        _tblSnippets = SharedSnippetProvider.SnippetsData.Copy();
      }

      bool configExists = (ConfigHelper.Current != null
        && ConfigHelper.Current.SharedSnippetsEnabled()
        && ConfigHelper.Current.SharedSnippetsOptions != null
       );

      SharedSnippetsOptions snpOpt = configExists ? ConfigHelper.Current.SharedSnippetsOptions : null;
      if (snpOpt != null && snpOpt.CodeCompletionListOrder == SharedSnippetsCodeCompletionListOrder.First)
      {
        DataSet tmpDs = _lists.Copy();
        _lists.Tables.Clear();
        _tblNames.Clear();

        _tblNames.Add(_tblSnippets.TableName);
        _lists.Tables.Add(_tblSnippets);
        while (tmpDs.Tables.Count > 0)
        {
          DataTable tbl = tmpDs.Tables[0];
          tmpDs.Tables.RemoveAt(0);
          _lists.Tables.Add(tbl); ;
          _tblNames.Add(tbl.TableName);
        }
      }
      else if (snpOpt != null && snpOpt.CodeCompletionListOrder == SharedSnippetsCodeCompletionListOrder.Last)
      {
        _tblNames.Add(_tblSnippets.TableName);
        _lists.Tables.Add(_tblSnippets);
      }
    }


    public void RefreshCodeCompletionLists( )
    {
      int tmpIndex = -1;
      string tmpName = String.Empty;

      if (_currentListIndex >= 0 && _currentListIndex < _tblNames.Count)
      {
        tmpIndex = _currentListIndex;
        tmpName = _tblNames[_currentListIndex];
      }


      LoadCodeCompletionLists();
      RefreshSharedSnippetsData();

      if (tmpIndex >= 0 && tmpIndex < _tblNames.Count && _tblNames[tmpIndex] == tmpName)
      {
        MoveTo(tmpIndex);
      }
      else
      {
        MoveToNext();
      }

      _bsListItems.Filter = String.Empty;
      _bsListItems.Sort = String.Empty;

    }


    public void JumpTo( string jumpString )
    {
      
      _jumpString = jumpString;
      string filterStr = String.Empty;
      CustomStringTokenizer tok = new CustomStringTokenizer(jumpString);
      IList<CustomToken> tokens = new List<CustomToken>();
      string tmp = String.Empty;

      CustomToken token = null;
      do
      {
        token = tok.Next();

        if (token.Kind == CustomTokenKind.EOL || token.Kind == CustomTokenKind.EOF || token.Kind == CustomTokenKind.Unknown || token.Kind == CustomTokenKind.WhiteSpace
            || (token.Kind == CustomTokenKind.Symbol && token.Value != ".")
          )
        {
          continue;
        }

        tokens.Add(token);

      } while (token.Kind != CustomTokenKind.EOF);

      if (tokens.Count == 0 || tokens[tokens.Count - 1].Value == ".")
      {
        filterStr = String.Empty;
      }
      else
      {
        filterStr = "( DisplayName Like '" + tokens[tokens.Count - 1].Value + "%')";
      }

      _bsListItems.Filter = filterStr;

      if (_bsListItems.Count > 0)
      {
        _bsListItems.Position = 0;
        _bsListItems.Sort = "DisplayName ASC";
      }
      else
      {
        _bsListItems.Sort = String.Empty;
      }
     
    }

     private void PerformCustomFilter(string value, bool lostFocus)
    {
      if(_bsListItems.DataSource == null)
      {
        return;
      }

      string filterStr = String.Empty;
      filterStr = "DisplayName Like '%{0}%' OR Value Like '%{0}%'";
      filterStr = String.Format(filterStr,value);

      _bsListItems.Filter = filterStr;
      _bsListItems.Sort = "DisplayName ASC";
     if(lostFocus)
     {
      grdItems.Focus();
     }
    }

    #endregion //Methods

    #region List navigation

    public void MoveToNext( )
    {
      if (_currentListIndex + 1 == _lists.Tables.Count)
      {
        return;
      }
      _currentListIndex++;
      _bsListItems.DataSource = _lists.Tables[_currentListIndex];
      lblListName.Text = _lists.Tables[_currentListIndex].TableName + "  ( " + (_currentListIndex + 1).ToString() + " of " + _lists.Tables.Count + " ) ";
      BindGrid();
    }

    public void MoveToPrev( )
    {
      if (_currentListIndex - 1 < 0)
      {
        return;
      }
      _currentListIndex--;
      _bsListItems.DataSource = _lists.Tables[_currentListIndex];
      lblListName.Text = _lists.Tables[_currentListIndex].TableName + "  ( " + (_currentListIndex + 1).ToString() + " of " + _lists.Tables.Count + " ) ";
      BindGrid();
    }

    private void MoveTo( int listIndex )
    {
      if (listIndex < 0 || listIndex >= _lists.Tables.Count)
      {
        return;
      }
      _currentListIndex = listIndex;
      _bsListItems.DataSource = _lists.Tables[_currentListIndex];
      lblListName.Text = _lists.Tables[_currentListIndex].TableName + "  ( " + (_currentListIndex + 1).ToString() + " of " + _lists.Tables.Count + " ) ";
      BindGrid();
    }

    #endregion

    private void grdItems_KeyDown( object sender, KeyEventArgs e )
    {
      if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete) && UserRequestedDismiss != null)
      {
        UserRequestedDismiss();
      }

      if (e.KeyCode == Keys.Back && UserPressedBackSpace != null)
      {
        UserPressedBackSpace();
        e.Handled = true; //don't want this to turn in to a keypress
      }

      if ((e.KeyCode == Keys.Enter) && UserMadeFinalSelection != null)
      {
        UserMadeFinalSelection();
      }


    }

    private void grdItems_KeyPress( object sender, KeyPressEventArgs e )
    {
      if (UserPressedKey != null)
      {
        UserPressedKey(e.KeyChar);
      }
      e.Handled = true;
    }

    private void grdItems_DoubleClick( object sender, EventArgs e )
    {
      UserMadeFinalSelection();
    }



    private void frmCodeCompletion_Deactivate( object sender, EventArgs e )
    {
      this.Hide();
    }

    private void pictureBox2_Click( object sender, EventArgs e )
    {
      MoveToNext();
    }

    private void pictureBox1_Click( object sender, EventArgs e )
    {
      MoveToPrev();
    }

    private void grdItems_KeyUp( object sender, KeyEventArgs e )
    {
      if (e.KeyCode == Keys.Right)
      {
        MoveToNext();
      }
      else if (e.KeyCode == Keys.Left)
      {
        MoveToPrev();
      }
    }

    private void pictureBox3_Click( object sender, EventArgs e )
    {
      RefreshCodeCompletionLists();
    }

    public void ClearCustomFilter()
    {
      txtCustomFilter.Text = String.Empty;
    }

    public void SetFocusToGrid()
    {
      grdItems.Focus();
    }

    private void txtCustomFilter_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Enter)
      {
        PerformCustomFilter(txtCustomFilter.Text,true);
      }
      else if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
      {
        grdItems.Focus();
      }
      else if (e.KeyCode == Keys.Escape && UserRequestedDismiss != null)
      {
        UserRequestedDismiss();
      }
    }

    private void txtCustomFilter_TextChanged(object sender, EventArgs e)
    {
      PerformCustomFilter(txtCustomFilter.Text, false);   
    }
  }
}