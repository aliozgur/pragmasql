using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
	public class SearchAndReplaceHistory
	{
		private List<string> _searchHistory = new List<string>();
		public List<string> SearchHistory
		{
			get { return _searchHistory; }
			set { _searchHistory = value; }
		}

		private List<string> _replaceHistory = new List<string>();
		public List<string> ReplaceHistory
		{
			get { return _replaceHistory; }
			set { _replaceHistory = value; }
		}

		public string[] SearchHistoryArray
		{
			get 
			{
				string[] result = new string[_searchHistory.Count];
				_searchHistory.CopyTo(result);
				return result;
			}
		}

		public string[] ReplaceHistoryArray
		{
			get
			{
				string[] result = new string[_replaceHistory.Count];
				_replaceHistory.CopyTo(result);
				return result;
			}
		}

	}
}
