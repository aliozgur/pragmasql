using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace PragmaSQL.Core
{
    public partial class frmConnectionRepository : KryptonForm
    {
        private readonly int _maxPersonalEditionConnCnt = 3;
        private IDictionary<string, ListViewGroup> _groups = new Dictionary<string, ListViewGroup>();

        private bool _useForConnecting = true;
        public bool UseForConnecting
        {
            get { return _useForConnecting; }
            set { _useForConnecting = value; }
        }


        private ConnectionParamsCollection _connParams = null;
        public ConnectionParamsCollection ConnParams
        {
            get { return _connParams; }
        }

        public frmConnectionRepository()
        {
            InitializeComponent();
            PopulateDBConnections();
            HostServicesSingleton.HostServices.SetMainFormAsOwner(this);
        }

        public ConnectionParams SelectedDataSource
        {
            get
            {
                if (lvConnections.SelectedItems.Count == 0)
                {
                    return null;
                }
                return _connParams.FindByID((Guid)lvConnections.SelectedItems[0].Tag);
            }
        }

        public IList<ConnectionParams> SelectedDataSources
        {
            get
            {
                IList<ConnectionParams> result = new List<ConnectionParams>();

                foreach (ListViewItem item in lvConnections.SelectedItems)
                {
                    ConnectionParams cp = _connParams.FindByID((Guid)item.Tag);
                    if (cp != null)
                    {
                        result.Add(cp);
                    }
                }
                return result;
            }
        }
        private void AddConnectionSpecToList(ConnectionParams connSpec)
        {

            ListViewGroup group = null;

            string key = ConnectionParams.PrepareConnKey(connSpec);
            string normalKey = key.Replace(((Char)29).ToString(), " as ");
            string serverName = connSpec.Server.Trim().ToLowerInvariant();

            if (_groups.ContainsKey(serverName))
                group = _groups[serverName];
            else
            {
                group = new ListViewGroup(serverName, connSpec.Server);
                lvConnections.Groups.Add(group);
                _groups.Add(serverName, group);
            }

            ListViewItem item = new ListViewItem(normalKey, group);
            item.SubItems.Add(connSpec.Database);
            item.Tag = connSpec.ID;

            lvConnections.SelectedItems.Clear();
            item.Selected = true;

            lvConnections.Items.Add(item);
            _connParams.Add(connSpec);

            InvalidateButtons();
        }

        private void PopulateDBConnections()
        {
            ListViewItem item = null;
            ListViewGroup group = null;
            _connParams = ConnectionParamsFactory.GetConnections();
            
            lvConnections.Items.Clear();
            string serverName = String.Empty;
            int cnt = 0;
            foreach (ConnectionParams connSpec in _connParams.OrderBy(x => x.Server))
            {
#if PERSONAL_EDITION
        cnt++;
        if (cnt > _maxPersonalEditionConnCnt)
        {
          lblPersonalEdLimit.Visible = true;
          lblPersonalEdLimit.Text = String.Format("Only {0} connections are listed.", _maxPersonalEditionConnCnt);
          lblPersonalEdLimit.ToolTipText = String.Format("Personal Edition does not support more than {0} saved connections.", _maxPersonalEditionConnCnt);

          break;
        }
#endif
                string key = ConnectionParams.PrepareConnKey(connSpec);
                string normalKey = key.Replace(((Char)29).ToString(), " as ");
                serverName = connSpec.Server.Trim().ToLowerInvariant();

                if (_groups.ContainsKey(serverName))
                {
                    group = _groups[serverName];
                }
                else
                {
                    group = new ListViewGroup(serverName, connSpec.Server);
                    lvConnections.Groups.Add(group);
                    _groups.Add(serverName, group);
                }
                item = new ListViewItem(normalKey, group);
                item.SubItems.Add(connSpec.Database);
                item.Tag = connSpec.ID;
                lvConnections.Items.Add(item);
            }

            if (_connParams.Count > 0)
            {
                lvConnections.Items[0].Selected = true;
            }

            InvalidateButtons();
        }

        private void AddNewConnectionSpec()
        {
#if PERSONAL_EDITION
      if (lvConnections.Items.Count >= _maxPersonalEditionConnCnt)
        throw new PersonalEditionLimitation(String.Format("Personal Edition does not support more than {0} saved connections.", _maxPersonalEditionConnCnt));
#endif
            frmConnectionParams frm = new frmConnectionParams();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddConnectionSpecToList(frm.GetCurrentConnectionSpec());
                ConnectionParamsFactory.Save(_connParams);
                InvalidateButtons();
            }
        }

        private void EditSelectedConnectionSpec()
        {
            if (lvConnections.SelectedItems.Count == 0)
            {
                return;
            }

            frmConnectionParams frm = new frmConnectionParams(this.SelectedDataSource, true, false);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _connParams.Delete(this.SelectedDataSource);
                lvConnections.Items.Remove(lvConnections.SelectedItems[0]);
                AddConnectionSpecToList(frm.GetCurrentConnectionSpec());
                ConnectionParamsFactory.Save(_connParams);
                InvalidateButtons();
            }
        }

        private void RemoveSelectedConnectionSpec()
        {
            if (lvConnections.SelectedItems.Count == 0)
            {
                return;
            }

            if (lvConnections.SelectedItems[0] == null)
            {
                return;
            }
            DialogResult dlgRes = MessageBox.Show("Delete selected database connection?", "Confirm",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dlgRes == DialogResult.No)
            {
                return;
            }

            _connParams.Delete(_connParams.FindByID((Guid)lvConnections.SelectedItems[0].Tag));
            lvConnections.Items.Remove(lvConnections.SelectedItems[0]);
            InvalidateButtons();
            return;
        }

        private void InvalidateButtons()
        {
            btnEdit.Enabled = !(lvConnections.SelectedItems.Count == 0 || lvConnections.Items.Count == 0);
            btnRemove.Enabled = btnEdit.Enabled;
        }

        #region Static Methods

        public static IList<ConnectionParams> SelectConnection()
        {
            return SelectConnection(false, true);
        }

        public static IList<ConnectionParams> SelectConnection(bool checkAnyItemExist)
        {
            return SelectConnection(checkAnyItemExist, true);
        }

        public static IList<ConnectionParams> SelectConnection(bool checkAnyItemExist, bool canEdit)
        {
            frmConnectionRepository frm = new frmConnectionRepository();
            if (checkAnyItemExist && frm.ConnParams == null || frm.ConnParams.Count == 0)
            {
                frm.Dispose();
                frm = null;
                MessageBox.Show("Saved connections list is empty and new connection dialog will be opened.\n"
                  + "NOTE: You can define saved connections by using \"View -> Saved Connections\" menu."
                  , "Warning"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Warning
                  );

                ConnectionParams cp = frmConnectionParams.CreateConnection();
                IList<ConnectionParams> result = new List<ConnectionParams>();
                if (cp != null)
                {
                    result.Add(cp);
                }
                return result;
            }
            else
            {
                frm.toolStrip1.Visible = canEdit;
                frm.ShowDialog();
                return frm.SelectedDataSources;
            }
        }

        public static ConnectionParams SelectSingleConnection()
        {
            return SelectSingleConnection(false, true);
        }

        public static ConnectionParams SelectSingleConnection(bool checkAnyItemExist)
        {
            return SelectSingleConnection(checkAnyItemExist, true);
        }

        public static ConnectionParams SelectSingleConnection(bool checkAnyItemExist, bool canEdit)
        {
            frmConnectionRepository frm = new frmConnectionRepository();
            frm.lvConnections.MultiSelect = false;

            if (checkAnyItemExist && frm.ConnParams == null || frm.ConnParams.Count == 0)
            {
                frm.Dispose();
                frm = null;
                MessageBox.Show("Saved connections list is empty and new connection dialog will be opened.\n"
                    + "NOTE: You can define saved connections by using \"View -> Saved Connections\" menu."
                    , "Warning"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Warning
                    );

                ConnectionParams cp = frmConnectionParams.CreateConnection();
                return cp;
            }
            else
            {
                frm.toolStrip1.Visible = canEdit;
                if (frm.ShowDialog() != DialogResult.OK)
                    return null;

                return frm.SelectedDataSource;
            }
        }


        #endregion //Static Methods

        private void frmConnectionRepository_Load(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedConnectionSpec();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedConnectionSpec();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            AddNewConnectionSpec();
        }

        private void lvConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvalidateButtons();
        }

        private void lvConnections_DoubleClick(object sender, EventArgs e)
        {
            if (lvConnections.SelectedItems.Count == 0)
                return;

            if (UseForConnecting)
                this.DialogResult = DialogResult.OK;
            else
                btnEdit_Click(btnEdit, EventArgs.Empty);
        }

        private void toolStrip1_Click(object sender, EventArgs e)
        {
            //Utils.ShowInfo(String.Format("Personal Edition does not support more than {0} saved connections.", _maxPersonalEditionConnCnt), MessageBoxButtons.OK);
        }
    }
}