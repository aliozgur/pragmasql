using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
	public partial class DataFilterDlg : KryptonForm
	{
		public DataFilterDlg()
		{
			InitializeComponent();
		}

		public static bool ShowDataFilterDlg(DataView dataView, out string filter)
		{
			if (dataView == null)
				throw new NullParameterException("DataView can not be null!");

			DataFilterDlg frm = new DataFilterDlg();
			frm.dataFilter1.DataView = dataView;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				filter = frm.dataFilter1.Filter;
				return true;
			}
			else
			{
				filter = String.Empty;
				return false;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (dataFilter1.ValidateInput())
				DialogResult = DialogResult.OK;
		}

	}
}