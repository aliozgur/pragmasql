using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

using PragmaSQL.Core;


namespace PragmaSQL.DxResultRenderer
{
	public class DxRendererOption
	{
		private string _currentStyleSchema = "Blue Office";
		
		[Browsable(false)]
		public string CurrentStyleSchema
		{
			get { return _currentStyleSchema; }
			set { _currentStyleSchema = value; }
		}

		private bool _showNavigationBar = true;
		public bool ShowNavigationBar
		{
			get { return _showNavigationBar; }
			set { _showNavigationBar = value; }
		}

		private bool _showFilterPanel = true;
		public bool ShowFilterPanel
		{
			get { return _showFilterPanel; }
			set { _showFilterPanel = value; }
		}

		private bool _showAutoFilterRow = true;
		public bool ShowAutoFilterRow
		{
			get { return _showAutoFilterRow; }
			set { _showAutoFilterRow = value; }
		}

		private bool _allowFilter = true;
		public bool AllowFilter
		{
			get { return _allowFilter; }
			set { _allowFilter = value; }
		}

		private bool _showGroupPanel = true;
		public bool ShowGroupPanel
		{
			get { return _showGroupPanel; }
			set { _showGroupPanel = value; }
		}

		private GroupDrawMode _groupDrawMode = GroupDrawMode.Default;
		public GroupDrawMode GroupDrawMode
		{
			get { return _groupDrawMode; }
			set { _groupDrawMode = value; }
		}

		private bool _columnAutoWidth = false;
		public bool ColumnAutoWidth
		{
			get { return _columnAutoWidth; }
			set { _columnAutoWidth = value; }
		}

		private bool _rowAutoHeight = false;
		public bool RowAutoHeight
		{
			get { return _rowAutoHeight; }
			set { _rowAutoHeight = value; }
		}

		private string _nullValueDisplayText = "(NULL)";
		public string NullValueDisplayText
		{
			get { return _nullValueDisplayText; }
			set { _nullValueDisplayText = value; }
		}


		private string _nullValueBackColorStr = Utils.SerializeColor(Color.LemonChiffon);
		[Browsable(false)]
		public string NullValueBackColorStr
		{
			get { return _nullValueBackColorStr; }
			set { _nullValueBackColorStr = value; }
		}

		[XmlIgnore]
		public Color NullValueBackColor
		{
			get { return Utils.DeserializeColor(NullValueBackColorStr);}
			set { NullValueBackColorStr = Utils.SerializeColor(value);}
		}

		private string _nullValueForeColorStr = Utils.SerializeColor(Color.Black);
		[Browsable(false)]
		public string NullValueForeColorStr
		{
			get { return _nullValueForeColorStr; }
			set { _nullValueForeColorStr = value; }
		}

		[XmlIgnore]
		public Color NullValueForeColor
		{
			get { return Utils.DeserializeColor(NullValueForeColorStr); }
			set { NullValueForeColorStr = Utils.SerializeColor(value); }
		}

	}

	public class DxRendererOptionCfg
	{
		private static DxRendererOption _current = new DxRendererOption();
		public static DxRendererOption Current
		{
			get { return _current; }
			set { _current = value; }
		}

		private static string _defaultCfgFile = String.Empty;
		static DxRendererOptionCfg()
		{
			_defaultCfgFile = HostServicesSingleton.HostServices.HostOptions.UserAppDataFolder;
			_defaultCfgFile += "DxRenderer.cfg";
		}

		public static void Load()
		{
			if (!File.Exists(_defaultCfgFile))
			{
				_current = new DxRendererOption();
			}
			else
			{
				_current = ObjectXMLSerializer<DxRendererOption>.Load(_defaultCfgFile);
				if (_current == null)
				{
					_current = new DxRendererOption();
				}
			}
		}

		public static void Save()
		{
			ObjectXMLSerializer<DxRendererOption>.Save(_current, _defaultCfgFile);
		}
	}
}
