using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public class DataTableFilterSortData
	{
		public string EditingFilterExpression = String.Empty;
		public string EditingSortExpression = String.Empty;

		public string CurrentFilterExpression = String.Empty;
		public string CurrenSortExpression = String.Empty;
		public string FilterType = "Custom";
		public int RowCount = 0;

		public bool IsFiltered = false;
		public bool IsSorted = false;

		private IList<string> _filterExpressions = new List<string>();
		public IList<string> FilterExpressions
		{
			get { return _filterExpressions; }
		}

		private IList<string> _sortExpressions = new List<string>();
		public IList<string> SortExpressions
		{
			get { return _sortExpressions; }
		}

		private IDictionary<string, SortOrder> _sortCols = new Dictionary<string, SortOrder>();
		public IDictionary<string, SortOrder> SortCols
		{
			get { return _sortCols; }
		}

		public void PrepareSortOrderFor(string colName)
		{
			if (!_sortCols.ContainsKey(colName))
			{
				_sortCols.Add(colName, SortOrder.None);
			}

			SortOrder order = _sortCols[colName];
			switch (order)
			{
				case SortOrder.Ascending:
					_sortCols[colName] = SortOrder.Descending;
					break;
				case SortOrder.Descending:
					_sortCols[colName] = SortOrder.Ascending;
					break;
				case SortOrder.None:
					_sortCols[colName] = SortOrder.Ascending;
					break;
				default:
					break;
			}
		}

		public string GenerateSortExpression()
		{
			string result = String.Empty;
			string comma = String.Empty;
			foreach (string col in _sortCols.Keys)
			{
				SortOrder order = _sortCols[col];
				if (order == SortOrder.None)
					continue;


				result = result + comma + col + (order == SortOrder.Descending ? " DESC" : String.Empty);
				comma = ", ";
			}
			return result;
		}

		public static string[] ListToStringArray(IList<string> list)
		{
			string[] result = new string[list.Count];
			list.CopyTo(result, 0);
			return result;
		}

	}
}
