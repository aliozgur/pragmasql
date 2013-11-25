using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.Globalization;

using ICSharpCode.Core;
using MSHelpServices;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace HtmlHelp2
{
  public partial class HtmlHelp2IndexResultsPad : DockContent
  {
    private static HtmlHelp2IndexResultsPad _current = null;
    internal static HtmlHelp2IndexResultsPad Current
    {
      get { return HtmlHelp2IndexResultsPad._current; }
      set { HtmlHelp2IndexResultsPad._current = value; }
    }

    public ListView IndexResultsListView
    {
      get { return listView; }
    }

    public HtmlHelp2IndexResultsPad( )
    {
      InitializeComponent();
      listView.Resize += new EventHandler(ListViewResize);
      listView.DoubleClick += new EventHandler(ListViewDoubleClick);
      listView.ColumnClick += new ColumnClickEventHandler(ColumnClick);
    }

    public void BringPadToFront( )
    {
      HostServicesSingleton.HostServices.ShowForm(this);
    }

    public void SortLV( int listViewColumn )
    {
      listView.ListViewItemSorter = new ListViewItemComparer(listViewColumn);
      listView.Sort();
    }

    private void ListViewResize( object sender, EventArgs e )
    {
      int w = (listView.Width - 60) / 2;
      title.Width = w;
      location.Width = w;
    }

    private void ListViewDoubleClick( object sender, EventArgs e )
    {
      ListViewItem lvi = listView.SelectedItems[0];
      if (lvi != null && lvi.Tag != null && lvi.Tag is IHxTopic)
      {
        WebBrowserHelper.OpenHelpView((IHxTopic)lvi.Tag);
      }
    }

    private void ColumnClick( object sender, ColumnClickEventArgs e )
    {
      this.SortLV(e.Column);
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
      if (listView.Items.Count > 1)
      {

        string text = StringParser.Parse("${res:AddIns.HtmlHelp2.ResultsOfIndexResults}",
                                         new string[,]
			 	                                  {{"0", indexTerm},
			 	                                  {"1", listView.Items.Count.ToString(CultureInfo.InvariantCulture)},
			                                 		{"2", (listView.Items.Count == 1)? ResourceService.GetString("AddIns.HtmlHelp2.SingleTopic"):ResourceService.GetString("AddIns.HtmlHelp2.MultiTopic")}}
																				);

				HostServicesSingleton.HostServices.MsgService.InfoMsg(text);
			}
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
        return String.Compare(((ListViewItem)x).SubItems[col].Text,
                              ((ListViewItem)y).SubItems[col].Text);
      }
    }
    #endregion
  }
}