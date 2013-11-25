using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ICSharpCode.Core;

namespace PragmaSQL.Core
{

	public partial class DataFilter : UserControl
	{
		#region Private Enums
		private enum ColumnDataTypes
		{
			Text,
			Integer,
			Float,
			DateTime,
			Boolean,
			NotSupported
		}

		public enum InvalidCriteriaActions
		{
			ShowMessageBox,
			RaiseException
		}

		#endregion //Private Enums

		#region Fields And Properties

		private int _rowFilterCount = 0;

		private BindingSource _bindingSource;
		public BindingSource BindingSource
		{
			get
			{
				return this._bindingSource;
			}
			set
			{
				this._dataView = null;
				this._bindingSource = value;
				if (this._bindingSource != null)
				{
					this.PrepareGridView();
				}
				if ((this._bindingSource == null) && (this._dataView == null))
				{
					this.EmptyControl();
				}
			}
		}

		private DataView _dataView;
		public DataView DataView
		{
			get
			{
				return this._dataView;
			}
			set
			{
				this._bindingSource = null;
				this._dataView = value;
				if (this._dataView != null)
				{
					this.PrepareGridView();
				}
				if ((this._bindingSource == null) && (this._dataView == null))
				{
					this.EmptyControl();
				}
			}
		}


		private bool _showButtons = true;
		public bool ShowButtons
		{
			get
			{
				return this._showButtons;
			}
			set
			{
				this._showButtons = value;
				this.ShowOrHideButtons();
			}
		}

		private InvalidCriteriaActions _invalidCriteriaAction;
		public InvalidCriteriaActions InvalidCriteriaAction
		{
			get
			{
				return this._invalidCriteriaAction;
			}
			set
			{
				this._invalidCriteriaAction = value;
			}
		}

		public string Filter
		{
			get
			{
				return this.GetFilter();
			}
		}

		#endregion //Fields And Properties

		#region CTOR

		public DataFilter()
		{
			this.InitializeComponent();
		}

		#endregion //CTRO

		#region Public Methods

		public void ApplyFilter()
		{
			this.grdCr.CurrentCell = null;
			if (this._dataView != null)
				this._dataView.RowFilter = this.GetFilter();
			else if (this._bindingSource != null)
				this._bindingSource.Filter = this.GetFilter();
		}

		public void Clear()
		{
			this.grdCr.CurrentCell = null;

			for (int i = 0; i < this.grdCr.Rows.Count; i++)
			{
				if (!this.grdCr[0, i].ReadOnly)
					this.grdCr[0, i].Value = "(none)";

				if (!this.grdCr[1, i].ReadOnly)
					this.grdCr[1, i].Value = String.Empty;

				if (this.grdCr[0, i].Style.BackColor == SystemColors.Info)
					this.grdCr[0, i].Style.BackColor = this.grdCr.DefaultCellStyle.BackColor;

				if (this.grdCr[1, i].Style.BackColor == SystemColors.Info)
					this.grdCr[1, i].Style.BackColor = this.grdCr.DefaultCellStyle.BackColor;
			}
		}

		public bool ValidateInput()
		{
			ColumnDataTypes dataType = ColumnDataTypes.NotSupported;
			string error = String.Empty;

			DataView dataView = null;

			if (this._dataView != null)
				dataView = this._dataView;
			else if (this._bindingSource != null)
				dataView = ((DataRowView)this._bindingSource[0]).DataView;


			for (int i = 0; i < dataView.Table.Columns.Count; i++)
			{
				dataType = this.GetColumnDataType(dataView.Table.Columns[i].DataType.ToString());

				if (this.grdCr[0, i].Style.BackColor == SystemColors.Info)
					this.grdCr[0, i].Style.BackColor = this.grdCr.DefaultCellStyle.BackColor;

				if (this.grdCr[1, i].Style.BackColor == SystemColors.Info)
					this.grdCr[1, i].Style.BackColor = this.grdCr.DefaultCellStyle.BackColor;

				if (grdCr[1, i].Value != null && !String.IsNullOrEmpty(grdCr[1, i].Value.ToString()))
				{
					switch (dataType)
					{
						case ColumnDataTypes.Integer:
							long tmp1 = 0;
							if (!Int64.TryParse(this.grdCr[1, i].Value.ToString(), out tmp1))
							{
								error += String.Format("{0} is invalid.\n", this.grdCr.Rows[i].HeaderCell.Value);
								this.grdCr[1, i].Style.BackColor = SystemColors.Info;
							}
							break;
						case ColumnDataTypes.Float:
							double tmp2 = 0;
							if (!Double.TryParse(this.grdCr[1, i].Value.ToString(), out tmp2))
							{
								error += String.Format("{0} is invalid.\n", this.grdCr.Rows[i].HeaderCell.Value);
								this.grdCr[1, i].Style.BackColor = SystemColors.Info;
							}
							break;
						case ColumnDataTypes.DateTime:
							DateTime tmp3 = DateTime.Now;
							if (!DateTime.TryParse(this.grdCr[1, i].Value.ToString(), out tmp3))
							{
								error += String.Format("{0} is invalid.\n", this.grdCr.Rows[i].HeaderCell.Value);
								this.grdCr[1, i].Style.BackColor = SystemColors.Info;
							}
							break;
						default:
							break;
					}
				}


				if ((((dataType != ColumnDataTypes.Boolean) && (dataType != ColumnDataTypes.NotSupported)) && ((this.grdCr[0, i].Value.ToString() != "Null") && (this.grdCr[0, i].Value.ToString() != "Not Null"))) && ((this.grdCr[0, i].Value.ToString() != "(none)") && (this.grdCr[1, i].Value == null || this.grdCr[1, i].Value.ToString() == String.Empty)))
				{
					error += String.Format("{0} value missing.\n",this.grdCr.Rows[i].HeaderCell.Value);
					this.grdCr[1, i].Style.BackColor = SystemColors.Info;
				}

				if ((((dataType != ColumnDataTypes.Boolean) && (dataType != ColumnDataTypes.NotSupported)) && (((this.grdCr[0, i].Value.ToString() == "Null") || (this.grdCr[0, i].Value.ToString() == "Not Null")) || (this.grdCr[0, i].Value.ToString() == "(none)"))) && (this.grdCr[1, i].Value == null || this.grdCr[1, i].Value.ToString() != String.Empty))
				{
					error += String.Format("{0} condition missing.\n", this.grdCr.Rows[i].HeaderCell.Value);
					this.grdCr[0, i].Style.BackColor = SystemColors.Info;
				}
			}

			if (!String.IsNullOrEmpty(error))
			{
				if (this._invalidCriteriaAction == InvalidCriteriaActions.RaiseException)
					throw new Exception(error);

				MessageService.ShowError(error);
				return false;
			}

			return true;
		}
		#endregion //Public Methods

		#region Private Methods

		private void EmptyControl()
		{
			this.grdCr.Rows.Clear();
			this.grdCr.Columns.Clear();
		}

		private ColumnDataTypes GetColumnDataType(string strType)
		{
			switch (strType)
			{
				case "System.DateTime":
					return ColumnDataTypes.DateTime;
				
				case "System.Boolean":
					return ColumnDataTypes.Boolean;
				
				case "System.String":
					return ColumnDataTypes.Text;

				case "System.Byte":
				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
					return ColumnDataTypes.Integer;

				case "System.Decimal":
				case "System.Single":
				case "System.Double":
					return ColumnDataTypes.Float;
			}
			return ColumnDataTypes.NotSupported;
		}

		private string GetDateAsSQLString(string value)
		{
			DateTime time = Convert.ToDateTime(value);
			return ("#" + time.Month.ToString() + "/" + time.Day.ToString() + "/" + time.Year.ToString() + "#");
		}

		private string GetFilter()
		{
			string str = String.Empty;
			string strQuote = "'";
			DataView dataView = null;
			
			ColumnDataTypes dataType = ColumnDataTypes.NotSupported;
			if (this._dataView != null)
				dataView = this._dataView;
			else if (this._bindingSource != null)
				dataView = ((DataRowView)this._bindingSource[0]).DataView;

			if (!base.DesignMode)
			{
				this._rowFilterCount++;
			}

			if (this.ValidateInput())
			{
				for (int i = 0; i < this.grdCr.Rows.Count; i++)
				{
					dataType = this.GetColumnDataType(dataView.Table.Columns[i].DataType.ToString());
					if (this.grdCr[0, i].Value.ToString() == "True")
					{
						if (str.Length > 0)
							str += " And ";

						str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " = True";
					}
					else if (this.grdCr[0, i].Value.ToString() == "False")
					{
						if (str.Length > 0)
							str += " And ";

						str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " = False";
					}
					else if (this.grdCr[0, i].Value.ToString() == "Null")
					{
						if (str.Length > 0)
							str += " And ";

						str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " Is Null";
					}
					else if (this.grdCr[0, i].Value.ToString() == "Not Null")
					{
						if (str.Length > 0)
							str += " And ";
						
						str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " Is Not Null";
					}
					else if (this.grdCr[0, i].Value.ToString() == "Like")
					{
						if (str.Length > 0)
							str += " And ";

						str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " Like " + strQuote + this.grdCr[1, i].Value.ToString() + strQuote;
					}
					else if (this.grdCr[0, i].Value.ToString() == "Not Like")
					{
						if (str.Length > 0)
							str += " And ";
						
						str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " Not Like " + strQuote + this.grdCr[1, i].Value.ToString() + strQuote;
					}
					else if ((((this.grdCr[0, i].Value.ToString() == "=") || (this.grdCr[0, i].Value.ToString() == ">")) || ((this.grdCr[0, i].Value.ToString() == "<") || (this.grdCr[0, i].Value.ToString() == ">="))) || ((this.grdCr[0, i].Value.ToString() == "<=") || (this.grdCr[0, i].Value.ToString() == "<>")))
					{
						if (str.Length > 0)
							str += " And ";
						
						switch (dataType)
						{
							case ColumnDataTypes.Text:
									str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " " + this.grdCr[0, i].Value.ToString() + " " + strQuote + this.grdCr[1, i].Value.ToString() + strQuote;
									break;
							case ColumnDataTypes.Integer:
							case ColumnDataTypes.Float:
									str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " " + this.grdCr[0, i].Value.ToString() + " " + this.grdCr[1, i].Value.ToString();
									break;
							case ColumnDataTypes.DateTime:
									str += this.grdCr.Rows[i].HeaderCell.Value.ToString() + " " + this.grdCr[0, i].Value.ToString() + " " + this.GetDateAsSQLString(this.grdCr[1, i].Value.ToString());
									break;
						}
					}
				}
			}
			return str;
		}

		private void PrepareGridView()
		{
			DataView dataView = null;
			if (this._dataView != null)
			{
				dataView = this._dataView;
			}
			else if (this._bindingSource != null)
			{
				DataRowView view2 = (DataRowView)this._bindingSource[0];
				dataView = view2.DataView;
			}
			
			this.EmptyControl();
			ColumnDataTypes dataType = ColumnDataTypes.NotSupported;
			
			this.grdCr.RowHeadersWidth = 100;

			DataGridViewComboBoxCell cellTemplate = new DataGridViewComboBoxCell();
			this.grdCr.Columns.Add(new DataGridViewColumn(cellTemplate));
			this.grdCr.Columns[0].Name = "Condition";
			
			DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
			this.grdCr.Columns.Add(new DataGridViewColumn(cell3));
			this.grdCr.Columns[1].Name = "Value";
			
			for (int i = 0; i < dataView.Table.Columns.Count; i++)
			{
				dataType = this.GetColumnDataType(dataView.Table.Columns[i].DataType.ToString());
				this.grdCr.Rows.Add(new object[] { "(none)", String.Empty });
				this.grdCr.Rows[i].HeaderCell.Value = dataView.Table.Columns[i].ColumnName;
				DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.grdCr[0, i];
				switch (dataType)
				{
					case ColumnDataTypes.Text:
						cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Like", "Not Like", "Null", "Not Null" });
						cell.Value = "(none)";
						cell.MaxDropDownItems = 11;
						break;

					case ColumnDataTypes.Integer:
						cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Null", "Not Null" });
						cell.Value = "(none)";
						cell.MaxDropDownItems = 9;
						break;

					case ColumnDataTypes.Float:
						cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Null", "Not Null" });
						cell.Value = "(none)";
						cell.MaxDropDownItems = 9;
						break;

					case ColumnDataTypes.DateTime:
						cell.Items.AddRange(new object[] { "(none)", "=", ">", "<", ">=", "<=", "<>", "Null", "Not Null" });
						cell.Value = "(none)";
						cell.MaxDropDownItems = 11;
						break;

					case ColumnDataTypes.Boolean:
						cell.Items.AddRange(new object[] { "(none)", "True", "False", "Null", "Not Null" });
						cell.Value = "(none)";
						cell.MaxDropDownItems = 5;
						this.grdCr[1, i].Style.BackColor = SystemColors.GradientInactiveCaption;
						this.grdCr[1, i].Style.SelectionBackColor = SystemColors.GradientInactiveCaption;
						this.grdCr[1, i].ReadOnly = true;
						break;

					case ColumnDataTypes.NotSupported:
						cell.Items.AddRange(new object[] { String.Empty });
						cell.Value = "";
						this.grdCr[0, i].Style.BackColor = SystemColors.GradientInactiveCaption;
						this.grdCr[0, i].Style.SelectionBackColor = SystemColors.GradientInactiveCaption;
						this.grdCr[0, i].ReadOnly = true;
						this.grdCr[1, i].Style.BackColor = SystemColors.GradientInactiveCaption;
						this.grdCr[1, i].Style.SelectionBackColor = SystemColors.GradientInactiveCaption;
						this.grdCr[1, i].ReadOnly = true;
						break;
				}
			}
			this.grdCr.CurrentCell = null;
		}

		private void ShowOrHideButtons()
		{
			if (this._showButtons)
			{
				this.ts.Show();
				this.grdCr.Height = base.Height - this.ts.Height;
				this.grdCr.Top = this.ts.Height;
			}
			else
			{
				this.ts.Hide();
				this.grdCr.Height = base.Height;
				this.grdCr.Top = 0;
			}
		}



		#endregion //Private Methods

		private void tsBtnClear_Click(object sender, EventArgs e)
		{
			this.Clear();
		}

		private void tsBtnRemove_Click(object sender, EventArgs e)
		{
			this.Clear();
			this.ApplyFilter();
		}
	}
}

