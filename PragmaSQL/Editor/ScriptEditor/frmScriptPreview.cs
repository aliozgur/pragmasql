using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.Core;
using ICSharpCode.TextEditor.Util;

using PragmaSQL.Core;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL
{
	public partial class frmScriptPreview : KryptonForm
	{
		#region Fields And Properties

		protected TextEditorControl _textEditor = null;
		
		public TextArea ActiveTextArea
		{
			get
			{
				if (_textEditor == null)
				{
					return null;
				}
				else
				{
					return _textEditor.ActiveTextAreaControl.TextArea;
				}
			}
		}

		public IDocument ActiveDocument
		{
			get
			{
				if (ActiveTextArea == null)
					return null;

				return ActiveTextArea.Document;
			}
		}

		public string Content
		{
			get { return ActiveDocument.TextContent; }
			set
			{
				try
				{
					ActiveTextArea.BeginUpdate();
					ActiveDocument.TextContent += value;
				}
				finally
				{
					ActiveTextArea.EndUpdate();
					ActiveTextArea.Invalidate();
				}
			}
		}

		private ConnectionParams _cp;
		public ConnectionParams ConnParams
		{
			get { return _cp; }
			set 
			{
				if (value == null)
					_cp = value;
				else
					_cp = value.CreateCopy();
			}
		}

		private ObjectInfo _objectInfo;

		public bool IsSticked
		{
			get { return btnStick.Checked; }
		}

		#endregion //Fields And Properties

		#region CTOR

		public frmScriptPreview()
		{
			InitializeComponent();
			InitializeTextEditor();
		}

		#endregion //CTOR

		#region Initialization

		private void InitializeTextEditor()
		{
			if (_textEditor != null)
			{
				return;
			}

			_textEditor = new TextEditorControl();
			panEditor.Controls.Add(_textEditor);
			_textEditor.Dock = DockStyle.Fill;
			_textEditor.BringToFront();
			_textEditor.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
			ActiveTextArea.TextEditorProperties.EnableFolding = false;
			ActiveTextArea.TextEditorProperties.AllowCaretBeyondEOL = true;

			if (ConfigHelper.Current != null && ConfigHelper.Current.TextEditorOptions != null)
				ActiveTextArea.MotherTextEditorControl.Encoding = ConfigHelper.Current.TextEditorOptions.Encoding;
			else
				ActiveTextArea.MotherTextEditorControl.Encoding = Encoding.Default;

			ConfigHelper.Current.TextEditorOptions.ApplyToControl(_textEditor);
			ActiveDocument.ReadOnly = true;

			_textEditor.Focus();
		}
		
		#endregion //Initialization

		#region Methods
		public void SetLocation(int x, int y)
		{
			int tmpHeight = 500;
			this.Height = 0;
			//this.Location = new Point(x, y + 15);
			Screen scr = Screen.FromControl(this);
			Rectangle r = scr.WorkingArea;
			if (y + tmpHeight > r.Bottom)
			{
				if ((y - tmpHeight) < r.Top)
				{
					this.Location = new Point(x, 0);
					this.Height = y - 15;
				}
				else
				{
					this.Location = new Point(x, y - tmpHeight - 15);
					this.Height = 500;
				}
			}
			else
			{
				this.Location = new Point(x, y + 15);
				this.Height = 500;
			}
		}

		private bool EditObjectInScriptEditor()
		{
			if (_objectInfo == null || _cp == null)
				return false;

			frmScriptEditor editor = ScriptEditorFactory.Create(_objectInfo,_cp, _cp.Database);
			ScriptEditorFactory.ShowScriptEditor(editor);
			return true;
		}

		#endregion //Methods

		#region Static Methods
		public static frmScriptPreview ShowFastPreview(ObjectInfo objInfo, ConnectionParams cp, int posX, int posY)
		{
			if (objInfo == null)
				throw new InvalidOperationException("ObjectInfo parameter is null!");
			if(cp == null)
				throw new InvalidOperationException("Connection params object is null!");

			string script = ScriptingHelper.GetAlterScript(cp, objInfo.ObjectID, objInfo.ObjectType);
			string caption = "Preview " + objInfo.ObjectName + " {" + cp.InfoDbServer + "}";
			
			frmScriptPreview frm = new frmScriptPreview();
			frm.Owner = Program.MainForm;
			if (posX > 0 && posY > 0)
				frm.SetLocation(posX, posY);

			frm.ConnParams = cp;
			if (!String.IsNullOrEmpty(caption))
				frm.Text = caption;

			frm._objectInfo = objInfo;
			frm.Content = script;
			frm.btnStick.Checked = ConfigHelper.Current != null && ConfigHelper.Current.TextEditorOptions != null && ConfigHelper.Current.TextEditorOptions.ScriptPreviewSticked;

			frm.Show();

			return frm;
		}

	
		#endregion //Static Methods

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				this.Close();
				return true;
			}
			else
				return base.ProcessCmdKey(ref msg, keyData);
		}

		private void frmScriptPreview_Deactivate(object sender, EventArgs e)
		{
			if (!btnStick.Checked)
				this.Close();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (EditObjectInScriptEditor())
				this.Close();
		}

		private void btnStick_CheckStateChanged(object sender, EventArgs e)
		{
			btnStick.Image = btnStick.Checked ? Properties.Resources.lock1 : Properties.Resources.unlock;
			btnStick.Text = btnStick.Checked ? "Unstick Preview Window" : "Stick Preview Window";
		}

	
	}
}