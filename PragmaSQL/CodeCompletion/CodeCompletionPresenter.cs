using System;
using System.Collections;
using System.Data.SqlClient;
using AsynchronousCodeBlocks;
using System.Windows.Forms;
using PragmaSQL.Core;

namespace PragmaSQL
{

  public class CodeCompletionPresenter
  {
    private frmObjectSelector _selector = new frmObjectSelector();
    public frmObjectSelector Selector
    {
      get { return _selector; }
    }
    private string _lastJump = "";

    public event KeyPressHandler KeyPressed;
    public event NotificationHandler UserMadeFinalSelection;
    public event NotificationHandler UserPressedBackSpace;


    public CodeCompletionPresenter()
    {
      RegisterForSelectorWindowEvents();
    }

    private void RegisterForSelectorWindowEvents( )
    {
      _selector.UserRequestedDismiss += new NotificationHandler(DismissSelector);
      _selector.UserMadeFinalSelection += new NotificationHandler(AnnounceFinalSelection);
      _selector.UserPressedKey += new KeyPressHandler(AnnounceKeyPress);
      _selector.UserPressedBackSpace += new NotificationHandler(AnnounceBackSpace);
    }


    public void SetConnection( SqlConnection conn )
    {
      _selector.SetConnection(conn);
    }

    public void InitializeCompletionProposal( SqlConnection conn )
    {
      _selector.InitializeCompletionProposal(conn);
    }

    public void ShowSelector()
    {
      _selector.Show();
    }

    public void DismissSelector( )
    {
      _selector.ClearCustomFilter();
      _selector.SetFocusToGrid();
      _selector.Hide();
    }


    public void SetLocation( int x, int y )
    {
      _selector.SetLocation(x, y);
    }

    private void AnnounceFinalSelection( )
    {
      if (UserMadeFinalSelection != null)
      {
        UserMadeFinalSelection();
      }
    }

    private void AnnounceKeyPress( char c )
    {
      if (KeyPressed != null)
      {
        KeyPressed(c);
      }
    }

    private void AnnounceBackSpace( )
    {
      if (UserPressedBackSpace != null)
      {
        UserPressedBackSpace();
      }
    }

    public string SelectedItem
    {
      get { return _selector.SelectedItem; }
    }

    public ArrayList SelectedItems
    {
      get { return _selector.SelectedItemsAsStrings; }
    }


    public string SelectedItemsAsCommaSeperatedString
    {
      get
      {
        ArrayList items = SelectedItems;
        string foundAsString = "";

        foreach (string item in items)
        {
          foundAsString += item;
          if (item != (string)items[items.Count - 1])
          {
            foundAsString += ", ";
          }
        }
        return foundAsString;
      }
    }

    


    public void JumpTo(Control sender, string text, SqlAnalyzerResults aResults )
    {
			try
			{
				if(sender != null)
					sender.Cursor = Cursors.WaitCursor;
				
				_lastJump = text;
				_selector.JumpTo(text, aResults);
			}
			finally
			{
				if(sender != null)
					sender.Cursor = Cursors.Default;
			}
    }

    public void CloseSelector()
    {
      if (_selector != null)
        _selector.Close();
    }
  }
}
