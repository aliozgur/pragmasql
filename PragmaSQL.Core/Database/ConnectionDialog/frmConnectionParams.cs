using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;

using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;

namespace PragmaSQL.Core
{
    public partial class frmConnectionParams : KryptonForm
    {
        #region Fields And Properties

        private bool _mustConnect = false;
        public bool MustConnect
        {
            get { return _mustConnect; }
            set { _mustConnect = value; }
        }

        private bool _logonOnly = false;
        public bool LogonOnly
        {
            get { return _logonOnly; }
            set
            {
                _logonOnly = value;
                ApplyLogOnOnly();
            }
        }

        public string InfoMessage
        {
            get { return lblInfoMsg.Text; }
            set { lblInfoMsg.Text = value; }
        }

        #endregion //Fields And Properties

        #region CTOR
        public frmConnectionParams()
        {
            InitializeComponent();
            HostServicesSingleton.HostServices.SetMainFormAsOwner(this);
        }

        public frmConnectionParams(ConnectionParams cp, bool canEditFriendlyName, bool mustConnect)
            : this(canEditFriendlyName, mustConnect)
        {
            if (cp != null)
            {
                RenderConnectionParams(cp);
            }
        }
        public frmConnectionParams(bool canEditFriendlyName, bool mustConnect)
            : this(canEditFriendlyName, mustConnect, false) { }

        public frmConnectionParams(bool canEditFriendlyName, bool mustConnect, bool canUseTemplates)
        {
            InitializeComponent();
            HostServicesSingleton.HostServices.SetMainFormAsOwner(this);
            
            _mustConnect = mustConnect;

            txtFriendlyName.Text = String.Empty;

            txtFriendlyName.Visible = canEditFriendlyName;
            lblFriendlyName.Visible = canEditFriendlyName;
            btnLoadTemplateFromRepo.Enabled = canUseTemplates;
        }

        

        #endregion //CTOR

        #region Static methods
        
        public static ConnectionParams CreateConnection()
        {
            return CreateConnection(false);
        }

        public static ConnectionParams CreateConnection(bool canUseTemplates)
        {
            frmConnectionParams frm = new frmConnectionParams(false, false,canUseTemplates);
            DialogResult dlgRes = frm.ShowDialog();
            if (dlgRes != DialogResult.OK)
            {
                return null;
            }

            return frm.GetCurrentConnectionSpec();
        }

        #endregion //Static Methods

        #region Methods

    public void ApplyLogOnOnly()
    {
      grpServerDb.Enabled = !_logonOnly;
      btnTest.Visible = !_logonOnly;
      if (_logonOnly)
        this.ActiveControl = txtPassword;
    }

        public void RenderConnectionParams(ConnectionParams cp)
        {
            txtServer.Text = cp.Server;
            txtDefultDB.Text = cp.Database;
            txtTimeOut.Text = cp.TimeOut;
            chkEncrypt.Checked = cp.Encrypt;

            if (cp.FriendlyName != null)
            {
                txtFriendlyName.Text = cp.FriendlyName;
            }

            if (cp.IntegratedSecurity.Length == 0)
            {
                rbUseIntegratedSecurity.Checked = false;
                txtUserName.Text = cp.UserName;
                txtPassword.Text = cp.Password;
            }
            else
            {
                rbUseIntegratedSecurity.Checked = true;
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
        }

        public void ResetLogOnInformation()
        {
            rbUseIntegratedSecurity.Checked = false;
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        public void ResetPassword()
        {
            txtPassword.Text = "";
        }

        public ConnectionParams GetCurrentConnectionSpec()
        {
            ConnectionParams connSpec = new ConnectionParams();
            connSpec.ID = Guid.NewGuid();
            connSpec.Database = txtDefultDB.Text;
            if (rbUseIntegratedSecurity.Checked)
                connSpec.IntegratedSecurity = "SSPI";
            else
                connSpec.IntegratedSecurity = "";

            connSpec.IsConnected = true;
            connSpec.Server = txtServer.Text;
            connSpec.TimeOut = txtTimeOut.Text;
            connSpec.Encrypt = chkEncrypt.Checked;
            connSpec.UserName = txtUserName.Text;
            connSpec.Password = txtPassword.Text;
            connSpec.FriendlyName = txtFriendlyName.Text;
            return connSpec;
        }

        private void PopulateServerNames()
        {
            if (txtServer.Items.Count > 0)
            {
                return;
            }

            string name = String.Empty;
            string instanceName = String.Empty;

            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
            foreach (DataRow row in servers.Rows)
            {
                name = row["ServerName"] != null && row["ServerName"].GetType() != typeof(DBNull) ? (string)row["ServerName"] : String.Empty;
                instanceName = row["InstanceName"] != null && row["InstanceName"].GetType() != typeof(DBNull) ? "\\" + (string)row["InstanceName"] : String.Empty;

                if (String.IsNullOrEmpty(name))
                    continue;

                txtServer.Items.Add(name + instanceName);
            }
            if (servers.Rows.Count == 0)
            {
                txtServer.Items.Add("(local)");
            }
        }

        private bool SyncTestConnection(bool showErrorDialog)
        {
            bool result = false;
            ConnectionParams cConn = GetCurrentConnectionSpec();
            cConn.TimeOut = "15";
            
            using (SqlConnection conn = cConn.CreateSqlConnection(false, false))
            {
                try
                {
                    conn.Open();
                    result = true;
                }
                catch (Exception ex)
                {
                    if (showErrorDialog)
                    {
                        MessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return result;
        }

        private bool AsyncTestConnection(bool showErrorDialog)
        {
            bool result = false;
            ConnectionParams cConn = GetCurrentConnectionSpec();
            cConn.TimeOut = "15";


            string error = String.Empty;
            AsyncConnectionResult aResult = frmAsyncConnectionOpener.TryToOpenConnection(cConn, ref error);
            if (aResult == AsyncConnectionResult.Success)
            {
                result = true;
            }
            else if (aResult == AsyncConnectionResult.Error)
            {
                if (showErrorDialog)
                    MessageBox.Show(error, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    throw new Exception(error);
            }

            return result;
        }

        private void LoadTemplateFromRepository()
        {
            ConnectionParams result = frmConnectionRepository.SelectSingleConnection(false, false);
            if (result == null)
                return;
            RenderConnectionParams(result);
        }

        #endregion //Methods

        private void rbUseIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUseIntegratedSecurity.Checked)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            if (String.IsNullOrEmpty(txtDefultDB.Text))
            {
                MessageBox.Show("Default database not specified.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!LogonOnly)
                {
                    DialogResult = DialogResult.OK;
                }
                else if (LogonOnly || MustConnect)
                {
                    if (AsyncTestConnection(true))
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AsyncTestConnection(true))
            {
                MessageBox.Show("Connection is sucessfull.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtServer_DropDown(object sender, EventArgs e)
        {
            PopulateServerNames();
        }

        private void btnLoadTemplateFromRepo_Click(object sender, EventArgs e)
        {
            LoadTemplateFromRepository();
        }


    }


}