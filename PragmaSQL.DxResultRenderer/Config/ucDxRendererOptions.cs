using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using PragmaSQL.Core;

using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace PragmaSQL.DxResultRenderer
{
  public partial class ucDxRendererOptions : UserControl, IConfigContentEditor 
  {
    private bool _shallSave = false;
		private bool _initializing = false;
		private XAppearances _xApp = null;
    private static ucDxRendererOptions _optionsUI;
    
		public static void ConfigSvc_DialogOpened(object sender, EventArgs e)
    {
      // to have a folder of its own
      // TreeNode jNode = HostServicesSingleton.HostServices.ConfigSvc.AddFolder("JIRA Client addin");
      _optionsUI = new ucDxRendererOptions();
      TreeNode oNode = HostServicesSingleton.HostServices.ConfigSvc.AddItem(null, "DxRenderer Options", _optionsUI);

      if (oNode != null && oNode.Parent != null)
        oNode.Parent.Expand();
    }
    
		public static void ConfigSvc_DialogClosed(object sender, EventArgs e)
    {
    
		}
    public static void ConfigSvc_FinalSelection(object sender, ConfigEventArgs args)
    {
    
		}

		public ucDxRendererOptions()
    {
      InitializeComponent();     
    }

		private void LoadStyles()
		{
			string fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DxRenderer.Appearances.xml";
			try
			{
				_initializing = true;
				if (!File.Exists(fileName))
				{
					string errStr = "Can not load style schemes. File not found: " + fileName;
					cmbGridStyle.BackColor = Color.FromArgb(255, 192, 192);
					toolTip1.SetToolTip(cmbGridStyle,errStr);
					HostServicesSingleton.HostServices.MsgService.ErrorMsg(errStr, MethodBase.GetCurrentMethod());
					return;
				}

				_xApp = new XAppearances(fileName);
				cmbGridStyle.Items.Clear();
				cmbGridStyle.Items.AddRange(_xApp.FormatNames);
				cmbGridStyle.Text = DxRendererOptionCfg.Current.CurrentStyleSchema;
			}
			finally
			{
				_initializing = false;
			}
		}

		#region IConfigContentEditor Members

    private bool _contentLoaded = false;
    public bool ContentLoaded
    {
      get { return _contentLoaded; }
    }

    public void HideContent()
    {
      this.Visible = false;
    }

    public string ItemClassName
    {
      get { return "DxRendererOption"; }
    }

    public bool LoadContent()
    { 
      _contentLoaded = false;
			DxRendererOptionCfg.Load();
			LoadStyles();
			propertyGrid1.SelectedObject = DxRendererOptionCfg.Current;
			_contentLoaded = true;
      return true;
    }

    public bool Modified
    {
      get { return _shallSave; }
    }

    public bool SaveContent()
    {
			DxRendererOptionCfg.Save();
			return true;
    }

    public void ShowContent()
    {
      this.Show();
    }

    #endregion

		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (!_contentLoaded)
				return;
			_shallSave = true;
		}

		private void cmbGridStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_initializing || _xApp == null || cmbGridStyle.SelectedItem == null)
				return;

			DxRendererOptionCfg.Current.CurrentStyleSchema = cmbGridStyle.SelectedItem.ToString();
			_shallSave = true;
		}
  }
}
