using System;
using System.IO;
using System.Collections;

namespace DifferenceEngine
{
	public class TextFileDiff : IDiffList
	{
		private ArrayList _lines;

		public TextFileDiff(string fileName)
		{
			_lines = new ArrayList();
			using (StreamReader sr = new StreamReader(fileName)) 
			{
				String line;
				// Read and display lines from the file until the end of 
				// the file is reached.
				while ((line = sr.ReadLine()) != null) 
				{
					_lines.Add(new DiffTextLine(line));
				}
			}
		}

		#region IDiffList Members

		public int Count()
		{
			return _lines.Count;
		}

		public IComparable GetByIndex(int index)
		{
			return (DiffTextLine)_lines[index];
		}

		#endregion
	
	}
}