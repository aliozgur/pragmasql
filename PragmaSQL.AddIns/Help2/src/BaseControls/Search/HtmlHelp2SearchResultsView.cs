using System;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;

using ICSharpCode.Core;
using MSHelpServices;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace HtmlHelp2
{
  public partial class HtmlHelp2SearchResultsView : UserControl
  {
    public HtmlHelp2SearchResultsView( )
    {
      InitializeComponent();
      ListViewResize(this, null);

    }

    public override void Refresh( )
    {
      base.Refresh();
    }

    public ListView SearchResultsListView
    {
      get { return listView; }
    }

    private void ListViewResize( object sender, EventArgs e )
    {
      rank.Width = 80;
      int w = (listView.Width - rank.Width - 40) / 2;
      title.Width = w;
      location.Width = w;
    }

    private void ListViewDoubleClick( object sender, EventArgs e )
    {
      bool hiliteMatches = (HtmlHelp2SearchPad.Current != null && HtmlHelp2SearchPad.Current.HiliteEnabled);

      ListViewItem lvi = listView.SelectedItems[0];
      if (lvi != null && lvi.Tag != null && lvi.Tag is IHxTopic)
      {
        WebBrowserHelper.OpenHelpView((IHxTopic)lvi.Tag, hiliteMatches);
      }
    }

    private void ColumnClick( object sender, ColumnClickEventArgs e )
    {
      listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
      listView.Sort();
    }

    public void CleanUp( )
    {
      foreach (ListViewItem lvi in listView.Items)
      {
        if (lvi.Tag != null) { lvi.Tag = null; }
      }

      listView.Items.Clear();
    }

    public void SetStatusMessage( string indexTerm )
    {
      string text = StringParser.Parse("${res:AddIns.HtmlHelp2.ResultsOfSearchResults}",
                                       new string[,]
			                                 {{"0", indexTerm},
			                                 	{"1", listView.Items.Count.ToString(CultureInfo.InvariantCulture)},
			                                 	{"2", (listView.Items.Count == 1)? ResourceService.GetString("AddIns.HtmlHelp2.SingleTopic"):ResourceService.GetString("AddIns.HtmlHelp2.MultiTopic")}}
                                      );

      HostServicesSingleton.HostServices.MsgService.InfoMsg(text);
    }

    #region Sorting
    class ListViewItemComparer : IComparer
    {
      private int col;

      public ListViewItemComparer( int column )
      {
        col = column;
      }

      public int Compare( object x, object y )
      {
        ListViewItem itemA = x as ListViewItem;
        ListViewItem itemB = y as ListViewItem;

        switch (col)
        {
          case 2:
            int a = Int32.Parse(itemA.SubItems[col].Text, CultureInfo.InvariantCulture);
            int b = Int32.Parse(itemB.SubItems[col].Text, CultureInfo.InvariantCulture);
            if (a > b) return 1;
            else if (a < b) return -1;
            else return 0;
          default:
            return string.Compare(itemA.SubItems[col].Text, itemB.SubItems[col].Text);
        }
      }
    }
    #endregion
  }
}
