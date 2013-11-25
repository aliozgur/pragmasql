using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL;
using PragmaSQL.Core;

namespace PragmaSQL.WebBrowserEx
{
	public partial class frmWebBrowser : DockContent, IWebBrowser
	{
		const int OLECMDID_SAVEAS = 4;
		const int OLECMDEXECOPT_DODEFAULT = 0;
		const int OLECMDID_PRINTPREVIEW = 7;

		internal int? WindowNo = null;

		public frmWebBrowser()
		{
			InitializeComponent();
			WireUpBrowserEvents();
		}

		#region Initialization
		private void WireUpBrowserEvents()
		{
			browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(OnDocumentCompleted);
			browser.DocumentTitleChanged += new EventHandler(OnDocumentTitleChanges);
			browser.ProgressChanged += new WebBrowserProgressChangedEventHandler(OnBrowserProgress);
			browser.StatusTextChanged += new EventHandler(OnBrowserStatusChanged);
			browser.StartNewWindow += new EventHandler<BrowserExtendedNavigatingEventArgs>(OnStartNewWindow);
		}

		#endregion

		#region Custom Events
		private void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			ReclaimWindowNumber();
			progressBar.Visible = false;
			cmbAdress.Text = browser.Document.Url.ToString();
			lblStatus.Text = String.Empty;

			if (!String.IsNullOrEmpty(browser.DocumentTitle))
			{
				Text = browser.DocumentTitle;
			}
			else
			{
				Text = "Untitled Web Page";
			}

			TabText = Text;
			FireAfterCompleted();
		}

		private void OnDocumentTitleChanges(object sender, EventArgs e)
		{

		}

		private void OnBrowserProgress(object sender, WebBrowserProgressChangedEventArgs e)
		{
			int position = (int)(e.CurrentProgress / e.MaximumProgress) * 100;
			progressBar.Value = position;
		}

		private void OnBrowserStatusChanged(object sender, EventArgs e)
		{
			lblStatus.Text = browser.StatusText;
		}

		private void OnStartNewWindow(object sender, BrowserExtendedNavigatingEventArgs e)
		{
			// Here we do the pop-up blocker work

			// Note that in Windows 2000 or lower this event will fire, but the
			// event arguments will not contain any useful information
			// for blocking pop-ups.

			// There are 4 filter levels.
			// None: Allow all pop-ups
			// Low: Allow pop-ups from secure sites
			// Medium: Block most pop-ups
			// High: Block all pop-ups (Use Ctrl to override)

			// We need the instance of the main form, because this holds the instance
			// to the WindowManager.

			PopupBlockerFilterLevel lvl = PopupBlockerFilterLevel.Medium;
			if (ConfigHelper.Current != null || ConfigHelper.Current.GeneralOptions != null)
			{
				lvl = ConfigHelper.Current.GeneralOptions.WebBrowser_PopupBlockerFilterLevel;
			}


			// Allow a popup when there is no information available or when the Ctrl key is pressed
			bool allowPopup = (e.NavigationContext == UrlContext.None) || ((e.NavigationContext & UrlContext.OverrideKey) == UrlContext.OverrideKey);

			if (!allowPopup)
			{
				// Give None, Low & Medium still a chance.
				switch (lvl)
				{
					case PopupBlockerFilterLevel.None:
						allowPopup = true;
						break;
					case PopupBlockerFilterLevel.Low:
						// See if this is a secure site
						if (this.browser.EncryptionLevel != WebBrowserEncryptionLevel.Insecure)
							allowPopup = true;
						else
							// Not a secure site, handle this like the medium filter
							goto case PopupBlockerFilterLevel.Medium;
						break;
					case PopupBlockerFilterLevel.Medium:
						// This is the most dificult one.
						// Only when the user first inited and the new window is user inited
						if ((e.NavigationContext & UrlContext.UserFirstInited) == UrlContext.UserFirstInited && (e.NavigationContext & UrlContext.UserInited) == UrlContext.UserInited)
							allowPopup = true;
						break;
				}
			}

			if (allowPopup)
			{
				// Check wheter it's a HTML dialog box. If so, allow the popup but do not open a new tab
				if (!((e.NavigationContext & UrlContext.HtmlDialog) == UrlContext.HtmlDialog))
				{
					frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse(e.Frame, e.Url.ToString());
					e.AutomationObject = frm.browser.Application;
					WebBrowserFactory.ShowWebBrowser(frm);
				}
			}
			else
			{
				// Here you could notify the user that the pop-up was blocked
				MessageBox.Show("Popup was blocked.\r\n" + e.Url.ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				e.Cancel = true;
			}
		}

		private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (!progressBar.Visible)
			{
				progressBar.Visible = true;
			}

			string prefix = String.Empty;
			if (!String.IsNullOrEmpty(e.TargetFrameName))
			{
				prefix = "{" + e.TargetFrameName + "}";
			}
			lblStatus.Text = prefix + e.Url;
		}

		#endregion

		#region Methods
		private void AddToHistory(string url)
		{
			if (String.IsNullOrEmpty(url))
			{
				return;
			}

			if (!cmbAdress.Items.Contains(url))
			{
				cmbAdress.Items.Add(url);
			}
		}

		public void ManualNavigate(string url)
		{
			if (browser.IsBusy)
				browser.Stop();

			Text = "Navigating...";
			TabText = Text;
			ReclaimWindowNumber();

			browser.Navigate(url);
			cmbAdress.Text = url;
			AddToHistory(url);
		}

		private void Navigate()
		{
			Text = "Navigating...";
			TabText = Text;

			ReclaimWindowNumber();
			browser.Navigate(cmbAdress.Text);
			AddToHistory(cmbAdress.Text);
		}

		private void ReclaimWindowNumber()
		{
			WebBrowserFactory.Numerator.ReclaimNumber(WindowNo);
			WindowNo = null;
		}

		private void ReclaimWindowNumber_OnClose()
		{
			ReclaimWindowNumber();
			WebBrowserFactory.Numerator.WindowCount--;
			WebBrowserFactory.ResetNumerator();
		}


		public bool OpenFile(string fileName)
		{
			string tmpFileName = fileName;
			if (String.IsNullOrEmpty(tmpFileName))
			{
				if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
				{
					return false;
				}
				tmpFileName = openFileDialog1.FileName;
			}

			FileInfo fi = new FileInfo(tmpFileName);
			this.Text = fi.Name;
			this.TabText = this.Text;
			ReclaimWindowNumber();

			ManualNavigate(tmpFileName);
			
			return true;
		}

		#endregion

		#region IWebBrowser Members

		public object DomDocument
		{
			get { return browser.Document.DomDocument; }
		}

		public WebBrowser WebBrowser
		{
			get { return browser; }
		}

		public string Caption
		{
			get { return this.TabText; }
			set
			{
				this.TabText = value;
				this.Text = value;
			}
		}

		public bool IsBusy
		{
			get { return browser.IsBusy; }
		}

		public void Navigate(string url)
		{
			ManualNavigate(url);
		}

		public void GoBack()
		{
			browser.GoBack();
		}

		public void GoForward()
		{
			browser.GoForward();
		}

		public void Stop()
		{
			browser.Stop();
		}

		public void GoHome()
		{
			if (ConfigHelper.Current != null && ConfigHelper.Current.GeneralOptions != null)
			{
				string tmpUrl = ConfigHelper.Current.GeneralOptions.WebBrowser_HomeUrl;

				if (String.IsNullOrEmpty(tmpUrl))
				{
					ManualNavigate(Properties.Resources.DefaultHomePage);
					//browser.GoHome();
				}
				else
				{
					ManualNavigate(tmpUrl);
				}
			}
			else
			{
				browser.GoHome();
			}
		}

		public void GoSearch()
		{
			browser.GoSearch();
		}

		private EventHandler _afterClosed;
		public event EventHandler AfterClosed
		{
			add { _afterClosed += value; }
			remove { _afterClosed -= value; }
		}

		private void FireAfterClosed()
		{
			if (_afterClosed == null)
			{
				return;
			}
			Delegate[] delegates = _afterClosed.GetInvocationList();
			foreach (EventHandler del in delegates)
			{

				try
				{
					del.Invoke(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
				}
			}
		}

		private EventHandler _afterCompleted;
		public event EventHandler AfterCompleted
		{
			add { _afterCompleted += value; }
			remove { _afterCompleted -= value; }
		}

		private void FireAfterCompleted()
		{
			if (_afterCompleted == null)
			{
				return;
			}
			Delegate[] delegates = _afterCompleted.GetInvocationList();
			foreach (EventHandler del in delegates)
			{
				try
				{
					del.Invoke(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(ex, del.Method);
					HostServicesSingleton.HostServices.MsgService.ShowMessages();
				}
			}
		}

		#endregion

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool result = false;
			bool navigate = false;

			if (
				((keyData & Keys.Modifiers & Keys.Control) == Keys.Control)
				&& ((keyData & Keys.Modifiers & Keys.Shift) == Keys.Shift)
				)
			{
				result = false;
			}
			else if ((keyData & Keys.Modifiers & Keys.Control) == Keys.Control)
			{
				if (msg.WParam == (IntPtr)Keys.F5)
					navigate = true;
			}
			else if (msg.WParam == (IntPtr)Keys.F5)
				navigate = true;

			if (navigate)
			{
				this.Navigate(cmbAdress.Text);
				result = true;
			}

			if (!result)
				return base.ProcessCmdKey(ref msg, keyData);
			else
				return result;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			GoBack();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			GoForward();
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			GoHome();
		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			GoSearch();
		}

		private void toolStripButton5_Click(object sender, EventArgs e)
		{
			Navigate();
		}

		private void toolStripButton6_Click(object sender, EventArgs e)
		{
			OpenFile(String.Empty);
		}

		private void toolStripButton9_Click(object sender, EventArgs e)
		{
			browser.Stop();
		}

		private void toolStripButton7_Click(object sender, EventArgs e)
		{
			browser.Print();
		}

		private void toolStripButton8_Click(object sender, EventArgs e)
		{
			object o = System.Reflection.Missing.Value;
			Type bType = browser.ActiveXInstance.GetType();
			bType.InvokeMember("ExecWB", BindingFlags.InvokeMethod, null, browser.ActiveXInstance, new object[] { OLECMDID_SAVEAS, OLECMDEXECOPT_DODEFAULT, o, o });
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Program.MainForm.CloseDocuments(null);
		}

		private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Program.MainForm.CloseDocuments(this);
		}

		private void toolStripButton10_Click(object sender, EventArgs e)
		{
			object o = System.Reflection.Missing.Value;
			Type bType = browser.ActiveXInstance.GetType();
			bType.InvokeMember("ExecWB", BindingFlags.InvokeMethod, null, browser.ActiveXInstance, new object[] { OLECMDID_PRINTPREVIEW, OLECMDEXECOPT_DODEFAULT, o, o });
		}

		private void cmbAdress_SelectedIndexChanged(object sender, EventArgs e)
		{
			Navigate();
		}

		private void cmbAdress_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Navigate();
			}
		}

		private void toolStripButton11_Click(object sender, EventArgs e)
		{
			frmWebBrowser frm = WebBrowserFactory.CreateAndBrowse();
			WebBrowserFactory.ShowWebBrowser(frm);
		}

		private void frmWebBrowser_FormClosed(object sender, FormClosedEventArgs e)
		{
			ReclaimWindowNumber_OnClose();
			FireAfterClosed();
		}

	}
}