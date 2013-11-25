/********************************************************************
  Class      : frmObjectExplorerFilter
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Istanbul Bilgi University
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using PragmaSQL.Database;
using PragmaSQL.Common;

namespace PragmaSQL.GUI
{
  public partial class frmObjectExplorerFilter : Form
  {
    private NodeData _data = null;
    private Filter _filter;

    private bool _hasParentFilter = false;
    public bool HasParentFilter
    {
      get 
      { 
        return _hasParentFilter; 
      }
      set 
      { 
        _hasParentFilter = value; 
        btnApplyParentFilter.Visible = value;
      }
    }

    public frmObjectExplorerFilter( )
    {
      InitializeComponent();
      foreach ( string typeName in Enum.GetNames(typeof(FilterType)))
      {
        cmbFilterType.Items.Add(typeName);
      }
      cmbFilterType.SelectedIndex = 0;
    }

    public void InitializeFilterDlg(NodeData data)
    {
      if(data == null)
      {
        throw new NullParameterException("Data parameter is null!");
      }

      if(!data.Filter.HasValue)
      {
        throw new NullParameterException("Data.Filter is null!");      
      }

      _data = data;
      _filter = _data.Filter.Value;

      cmbFilterType.SelectedIndex = (int)_filter.FilterType;
      edtFilterText.Text = _filter.FilterText;
      chkApplyToChildren.Checked = _filter.ApplyToChildren;
    }

    public void LoadFrom(Filter? filter)
    {
      if(!filter.HasValue)
      {
        return;
      }
      cmbFilterType.SelectedIndex = (int)filter.Value.FilterType;
      edtFilterText.Text = filter.Value.FilterText;
      chkApplyToChildren.Checked = filter.Value.ApplyToChildren;
    }

    private void btnClear_Click( object sender, EventArgs e )
    {
      cmbFilterType.SelectedIndex = 0;
      edtFilterText.Text = String.Empty;
      chkApplyToChildren.Checked = false;
    }

    private void btnApply_Click( object sender, EventArgs e )
    {
      _filter.ApplyToChildren = chkApplyToChildren.Checked;
      _filter.FilterText = edtFilterText.Text;
      _filter.FilterType = (FilterType)cmbFilterType.SelectedIndex;
      _filter.UseOwn = true;
      _data.Filter = _filter;
    }

    private void btnApplyParentFilter_Click( object sender, EventArgs e )
    {
      if(HasParentFilter)
      {
        _filter.ApplyToChildren = false;
        _filter.FilterText = String.Empty;
        _filter.UseOwn = false;
        _filter.FilterType = FilterType.Plain;
        _data.Filter = _filter;
        DialogResult = DialogResult.OK;
      }
    }
  }
}