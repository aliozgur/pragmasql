using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public partial class ucWebSearchOptions : UserControl, IConfigContentEditor
  {
    private bool _isInitializing = false;
    private bool _isModified = false;
    private bool _isContentLoaded = false;

    private IList<WebSearchSite> _sites = new List<WebSearchSite>();
    private BindingSource _bs = new BindingSource();
    private List<WebSearchSite> _sourceSites = null;
    public ucWebSearchOptions( )
    {
      InitializeComponent();
      _bs.ListChanged += new ListChangedEventHandler(_bs_ListChanged);
    }

    void _bs_ListChanged( object sender, ListChangedEventArgs e )
    {
      if (_isInitializing)
      {
        return;
      }
      _isModified = true;
    }

    #region IConfigurationContentItem Members

    public string ItemClassName
    {
      get
      {
        return "WebSearchOptions";
      }
    }

    public bool ContentLoaded
    {
      get
      {
        return _isContentLoaded;
      }
    }

    public bool Modified
    {
      get
      {
        return _isModified;
      }
    }

    public bool LoadContent( )
    {
      return LoadContent(ConfigHelper.Current);
    }

    public bool LoadContent( ConfigurationContent content )
    {
      if (content == null)
      {
        throw new NullParameterException("Configuration content param is null!");
      }

      if (content.WebSearchOptions == null)
      {
        throw new NullPropertyException("Configuration content does not contain WebSearchOptions item!");
      }

      _isInitializing = true;
      _sourceSites = content.WebSearchOptions;
      _sites.Clear();
      foreach (WebSearchSite site in _sourceSites)
      {
        _sites.Add(site.Copy());
      }
      _bs.DataSource = _sites;
      grd.DataSource = _bs;
      _isInitializing = false;
      return true;
    }

    public bool SaveContent( )
    {
      _sourceSites.Clear();
      _sourceSites.AddRange(_sites);
      _isModified = false;
      return true;
    }

    public void ShowContent( )
    {
      this.Show();
    }

    public void HideContent( )
    {
      this.Hide();
    }

    #endregion

    #region Methods
    private void AddSite( )
    {
      _bs.Add(new WebSearchSite());
      _isModified = true;
    }

    private void RemoveSelectedSites( )
    {
      _isModified = true;
      ArrayList removeList = new ArrayList();
      foreach (DataGridViewRow row in grd.SelectedRows)
      {
        if (row.DataBoundItem == null)
        {
          continue;
        }
        removeList.Add(row.DataBoundItem);
      }
      
      foreach (object site in removeList)
      {
        _bs.Remove(site);
      }
    }
    
    private void MoveUp( )
    {
    }
    #endregion  //Methods

    private void btnAdd_Click( object sender, EventArgs e )
    {
      AddSite();
    }

    private void btnRemove_Click( object sender, EventArgs e )
    {
      RemoveSelectedSites();
    }

    private void addToolStripMenuItem_Click( object sender, EventArgs e )
    {
      AddSite();
    }

    private void removeToolStripMenuItem_Click( object sender, EventArgs e )
    {
      RemoveSelectedSites();
    }
  }
}
