using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;
using PragmaSQL.Core;

namespace HtmlHelp2
{
  public partial class HtmlHelp2SearchResults : DockContent
  {
    static HtmlHelp2SearchResults _instance;
    public static HtmlHelp2SearchResults Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new HtmlHelp2SearchResults();
          HostServicesSingleton.HostServices.ShowForm(_instance, AddInDockState.DockBottomAutoHide);
        }
        return _instance;
      }
    }
    
    public HtmlHelp2SearchResults( )
    {
      InitializeComponent();
      InitializeView();
    }

    private void InitializeView( )
    {
      _view = new HtmlHelp2SearchResultsView();
      this.Controls.Add(_view);
      _view.Parent = this;
      _view.Dock = DockStyle.Fill;
      _view.Show();
    }

    private HtmlHelp2SearchResultsView _view = null;
    public HtmlHelp2SearchResultsView View
    {
      get { return _view; }
    }

    public void BringPadToFront( )
    {
     HostServicesSingleton.HostServices.ShowForm(this);
    }
  }
}