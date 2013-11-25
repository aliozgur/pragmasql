using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
	public interface IWebBrowser
	{
		
		object DomDocument { get; }
		WebBrowser WebBrowser { get; }
		string Caption { get; set;}
		bool IsBusy { get; }

		void Navigate(string url);
		void GoBack();
		void GoForward();
		void GoHome();
		void GoSearch();
		void Stop();
		event EventHandler AfterClosed;
		event EventHandler AfterCompleted;

	}
}
