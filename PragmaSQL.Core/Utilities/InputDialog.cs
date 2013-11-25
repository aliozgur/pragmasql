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
    public delegate bool ValidateInputNotification(object sender, ref string ErrorMessage);
    public partial class InputDialog : KryptonForm
    {
        public ValidateInputNotification OnValidateInput;
        IDictionary<string,TextBox> inputEdits = new Dictionary<string, TextBox>();

        public static DialogResult ShowDialog(string caption, ref string value)
        {
            return InputDialog.ShowDialog("Input Dialog", caption, ref value);
        }

        public static DialogResult ShowDialog(string dialogCaption, string caption, ref string value)
        {
            InputDialog inputDlg = new InputDialog();
            inputDlg.Text = dialogCaption;
            inputDlg.lblInput.Text = caption;
            inputDlg.edtInput.Text = value;

            DialogResult result = inputDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                value = inputDlg.edtInput.Text;
            }

            return result;  
        }

        public InputDialog()
        {
            InitializeComponent();
        }

        public void InitializeInputEdits(string[] captions, string[] values)
        {
            if (captions == null || captions.Length == 0)
            {
                lblInput.Text = "Enter Value";
                inputEdits.Add(lblInput.Text, edtInput);
                return;
            }

            inputEdits.Add(captions[0], edtInput);
            lblInput.Text = captions[0];
            if (values != null && values.Length > 0)
            {
                edtInput.Text = values[0];
            }

            int currentPos = edtInput.Bottom;

            for (int i = 1; i < captions.Length; i++)
            {

                currentPos += 5;
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Location = new System.Drawing.Point(6, currentPos);
                lbl.Name = "lblInput" + i.ToString();
                lbl.Size = new System.Drawing.Size(0, 13);
                lbl.Text = captions[i];

                currentPos = lbl.Bottom + 5;

                TextBox edit = new TextBox();
                edit.Location = new System.Drawing.Point(9, currentPos);
                edit.Name = "edtInput" + i.ToString();
                edit.Size = new System.Drawing.Size(420, 20);
                edit.TabIndex = i;

                inputEdits.Add(captions[i],edit);
                currentPos = edit.Bottom;
                
                if (values != null && values.Length > i)
                {
                    edit.Text = values[i];
                }

                Controls.Add(lbl);
                Controls.Add(edit);

            }
            
            this.Height = currentPos + panel1.Height + 50;
        }

        public string this[string labelCaption]
        {
            get
            {
                return inputEdits[labelCaption].Text;
            }
        }

        public ICollection<TextBox> InputEditors
        {
            get
            {
                return inputEdits.Values;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OnValidateInput == null)
            {
                return;
            }

            Delegate[] invList = OnValidateInput.GetInvocationList();
            if (invList == null)
            {
                return;
            }

            bool validationFailed = false;
            string errorMessage = String.Empty;

            foreach (ValidateInputNotification vi in invList)
            {
                if (!vi(this, ref errorMessage))
                {
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (!validationFailed)
                    {
                        validationFailed = true;
                    }
                }
            }
            if (validationFailed)
            {
                this.DialogResult = DialogResult.None;
            }
        }
    }
}