using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
	public class WimdowNumerator
	{
		private SortedList<int, int> _reclaimedNumbers = new SortedList<int, int>();

		private int _windowCount = 0;
		public int WindowCount
		{
			get { return _windowCount; }
			set { _windowCount = value; }
		}

		public int? NextNumber
		{
			get
			{
				if (_reclaimedNumbers.Count == 0)
				{
					return null;
				}

				int result = _reclaimedNumbers.Keys[0];
				_reclaimedNumbers.Remove(result);
				return result;
			}
		}

		public void ReclaimNumber(int editorNo)
		{
			if (_reclaimedNumbers.ContainsKey(editorNo))
				return;

			_reclaimedNumbers.Add(editorNo, editorNo);
		}

		public void ReclaimNumber(int? editorNo)
		{
			if (!editorNo.HasValue)
				return;
			ReclaimNumber(editorNo.Value);
		}

		public void ClearReclaimedNumbers()
		{
			_reclaimedNumbers.Clear();		
		}
	}
}
