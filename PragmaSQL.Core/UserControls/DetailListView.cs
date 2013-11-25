using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public partial class DetailListView : ListView
	{
		private ListViewColumnSorter _lvwColumnSorter = new ListViewColumnSorter();

		private bool _columnClickSortEnabled = true;
		public bool ColumnClickSortEnabled
		{
			get { return _columnClickSortEnabled; }
			set 
			{ 
				_columnClickSortEnabled = value;
				this.ListViewItemSorter = _columnClickSortEnabled ? _lvwColumnSorter : null;
				if (_columnClickSortEnabled)
					this.Sorting = SortOrder.None;
			}
		}


		[Browsable(false)]
		public new View View
		{
			get { return base.View; }
			set { base.View = value; }
		}

		public new SortOrder Sorting
		{
			get { return base.Sorting; }
			set 
			{
				if (value != SortOrder.None)
					_columnClickSortEnabled = false;
			}
		}

		public DetailListView()
		{
			InitializeComponent();
			CustomInitialize();
		}

		public DetailListView(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			CustomInitialize();
		}

		private void CustomInitialize()
		{
			this.View = View.Details;
			this.ListViewItemSorter = _lvwColumnSorter;
		}

		private void DetailListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (!_columnClickSortEnabled)
				return;

			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == _lvwColumnSorter.SortColumn)
			{
				// Reverse the current sort direction for this column.
				if (_lvwColumnSorter.Order == SortOrder.Ascending)
				{
					_lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					_lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				_lvwColumnSorter.SortColumn = e.Column;
				_lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			this.Sort();
		}
	}
}
