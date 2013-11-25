using System;
using System.Collections;
using System.IO;
using Menees.DiffUtils;
using System.Windows.Forms;

namespace TestDiffUtils
{
	public class DiffTest
	{
		// To store multiple changes eventually.
		ArrayList _changes = new ArrayList();
		byte[] _baseData = null;
		TextBox m_txtBinary;
		
		public DiffTest(String fileName, TextBox txtBinary)
		{
			m_txtBinary = txtBinary;

			WriteLine("Creating DiffTest from {0}", fileName);
			Stream stream = File.OpenRead(fileName);
			_baseData = new byte[stream.Length];
			stream.Read(_baseData, 0, _baseData.Length);
			stream.Close();
			WriteLine("Created successfully.");
		}
		
		public void AddChange(String verFileName)
		{
			WriteLine("Adding changes from {0}", verFileName);
			Stream baseStream = new MemoryStream(_baseData);
			Stream stream = File.OpenRead(verFileName);			
			BinaryDiff diff = new BinaryDiff();
			AddCopyList change = diff.Execute(baseStream, stream);
			_changes.Add(change);
			stream.Close();
			baseStream.Close();
			WriteLine("Changes added successfully.  AddCopy byte length: {0}", change.TotalByteLength);
		}
		
		public void ExtractNewest(String fileName)
		{
			WriteLine("Extracting newest changes to {0}", fileName);
			// Just in case we add more than one revision; get the latest changeset.
			AddCopyList change = LastChange;
			Stream original = new MemoryStream(_baseData);
			Stream newest = File.OpenWrite(fileName);
			
			byte[] temp = null;
			
			// Shouldn't have to seek to the end of newest stream; since we
			// are just writing to it in sequence, it should always be at the end.
			foreach(object o in change) {
				if(o is Addition) {
					Addition add = (Addition)o;
					// Simply write the data to the newest stream.
					newest.Write(add.arBytes, 0, add.arBytes.Length);
				} else if(o is Copy) {
					Copy copy = (Copy)o;
					// Seek to position in original stream and read the
					// appropriate length, then write it to the newest stream.
					original.Seek(copy.iBaseOffset, SeekOrigin.Begin);
					temp = new byte[copy.iLength];
					original.Read(temp, 0, temp.Length);
					newest.Write(temp, 0, temp.Length);
				}
			}
			
			newest.Close();
			original.Close();
			WriteLine("Extracted changes successfully.");
		}

		public AddCopyList LastChange
		{
			get
			{
				if (_changes.Count > 0)
				{
					return (AddCopyList)_changes[_changes.Count-1];
				}
				else
				{
					return null;
				}
			}
		}

		private void WriteLine(string strFmt, object oArg)
		{
			m_txtBinary.AppendText(string.Format(strFmt, oArg));
			m_txtBinary.AppendText("\r\n");
		}

		private void WriteLine(string strText)
		{
			m_txtBinary.AppendText(strText);
			m_txtBinary.AppendText("\r\n");
		}
	}
}