using System;
using System.Collections;
using System.Data.SqlClient;
using AsynchronousCodeBlocks;

namespace PragmaSQL
{

  public class CodeCompletionPresenterEx
  {
    private frmObjectSelectorEx _selector = new frmObjectSelectorEx();
    public frmObjectSelectorEx Selector
    {
      get { return _selector; }
    }
    private string _lastJump = "";

    public event KeyPressHandler KeyPressed;
    public event NotificationHandler UserMadeFinalSelection;
    public event NotificationHandler UserPressedBackSpace;


    public CodeCompletionPresenterEx( )
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

    public void ShowSelector( )
    {
      _selector.PopulateCodeCompletionLists();
      _selector.Show();
    }

    public void DismissSelector( )
    {
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


    public string SelectedItemsAsCommaSeparatedString
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


    public void JumpTo( string text )
    {
      _lastJump = text;
      _selector.JumpTo(text);
    }


  }
}
