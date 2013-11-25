using System;
using System.Collections;
using System.Collections.Specialized;
using com.calitha.goldparser;

namespace com.calitha.goldparser.lalr
{

	/// <summary>
	/// Type-safe list of Action items.
	/// </summary>
	public class GPActionCollection : IEnumerable
	{
		private IDictionary table;

		public GPActionCollection()
		{
			table = new HybridDictionary();
		}

		public IEnumerator GetEnumerator()
		{
			return table.Values.GetEnumerator();
		}

		public void Add(GPAction action)
		{
			table.Add(action.symbol,action);
		}

		public GPAction Get(Symbol symbol)
		{
			return table[symbol] as GPAction;
		}

		public GPAction this[Symbol symbol]
		{
			get { return Get(symbol);}
		}
	}

	/// <summary>
	/// Abstract action class. All actions in a LALR must be inherited from this class.
	/// </summary>
	public abstract class GPAction
	{
		internal Symbol symbol;
	}
}
