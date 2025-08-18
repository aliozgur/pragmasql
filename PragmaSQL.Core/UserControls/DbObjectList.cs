using ICSharpCode.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PragmaSQL.Core
{

    public partial class DbObjectList : UserControl
    {
        #region Nested Classes
        public class DbObjectInfo
        {
            private string _name = String.Empty;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _objType;
            public string ObjType
            {
                get { return _objType; }
                set { _objType = value; }
            }

            private string _owner;
            public string Owner
            {
                get { return _owner; }
                set { _owner = value; }
            }

            public int Priority
            {
                get
                {
                    return DbObjectListUtils.EncodeObjectTypePriority(ObjType);
                }
            }

            public override bool Equals(object obj)
            {
                DbObjectInfo inObj = obj as DbObjectInfo;
                if (inObj == null)
                    return false;

                return Name.Equals(inObj.Name, StringComparison.InvariantCulture)
                    && ObjType.Equals(inObj.ObjType, StringComparison.InvariantCulture)
                    && Owner.Equals(inObj.Owner, StringComparison.InvariantCulture);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public string QualifiedSchemaAndName
            {
                get { return "[" + Owner + "].[" + Name + "]"; }
            }

            public string SchemaAndName
            {
                get { return Owner + "." + Name; }
            }

            public string Qualify(string value)
            {
                string result = value;
                if (!value.StartsWith("["))
                    result = "[" + result;

                if (!value.EndsWith("]"))
                    result = result + "]";

                return result;
            }
        }

        private class ComparerDbObjectsByPriority : IComparer<DbObjectInfo>
        {
            public int Compare(DbObjectInfo x, DbObjectInfo y)
            {
                if (x == null && y != null)
                    return -1;
                else if (x != null && y == null)
                    return 1;
                else if (x == null && y == null)
                    return 0;
                else
                    return x.Priority.CompareTo(y.Priority);
            }
        }
        #endregion // Nested Classes

        #region Fields And Properties

        private DataTable _dtSource = null;
        //private IList<DbObjectInfo> _selectedObjects = new List<DbObjectInfo>();
        private List<DbObjectInfo> _selectedObjects = new List<DbObjectInfo>();
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<DbObjectInfo> SelectedObjects
        {
            get
            {
                SortSelectedObjects();
                return new List<DbObjectInfo>(_selectedObjects).AsReadOnly();
            }
        }

        public int SelectedObjectCount
        {
            get { return _selectedObjects.Count; }
        }

        private ConnectionParams _cp;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ConnectionParams ConnParams
        {
            get { return _cp; }
            set
            {
                if (value != null)
                    _cp = value.CreateCopy();
                else
                    _cp = value;

                lblDbInfo.Text = "Connection Info : " + _cp == null ? String.Empty : _cp.InfoDbServer;
            }
        }


        private EventHandler _dumpToTexteditorCompleted = null;
        public event EventHandler DumpToTexteditorCompleted
        {
            add { _dumpToTexteditorCompleted += value; }
            remove { _dumpToTexteditorCompleted -= value; }
        }

        private DbObjectListObjectTypes _objectTypes =
            DbObjectListObjectTypes.Table |
            DbObjectListObjectTypes.View |
            DbObjectListObjectTypes.StoredProcedure |
            DbObjectListObjectTypes.UserDefinedFunction;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DbObjectListObjectTypes ObjectTypes
        {
            get { return _objectTypes; }
            set { _objectTypes = value; PopulateObjectTypesCombo(); }
        }

        #endregion //Fields And Properties

        #region CTOR

        public DbObjectList()
        {
            InitializeComponent();
            bsDest.DataSource = _selectedObjects;
            cmbType.SelectedIndex = 0;
        }


        #endregion //CTOR

        #region Methods
        private void PopulateObjectTypesCombo()
        {
            cmbType.Items.Clear();
            Array values = Enum.GetValues(typeof(DbObjectListObjectTypes));
            foreach (DbObjectListObjectTypes t in values)
            {
                if ((_objectTypes & t) == t)
                    cmbType.Items.Add(t.ToString());
            }

            cmbType.Items.Insert(0, "All");
            cmbType.SelectedIndex = 0;
        }

        private bool IsObjectTypeValid(string typeName)
        {
            DbObjectListObjectTypes t = (DbObjectListObjectTypes)Enum.Parse(typeof(DbObjectListObjectTypes), typeName);
            return (_objectTypes & t) == t;
        }

        private string PrepareInCriteria()
        {
            string result = String.Empty;
            Array values = Enum.GetValues(typeof(DbObjectListObjectTypes));
            string comma = String.Empty;

            foreach (DbObjectListObjectTypes t in values)
            {
                if ((_objectTypes & t) == t)
                {
                    result += comma + "'" + t.ToString() + "'";
                    comma = ", ";
                }
            }

            return result;
        }

        private void SortSelectedObjects()
        {
            _selectedObjects.Sort(new ComparerDbObjectsByPriority());
        }

        private void ListObjects(string objType, string filter)
        {
            string commandText = String.Empty;
            if (objType == "All")
            {
                string inCr = PrepareInCriteria(); //ResManager.GetDBScript("Script_ObjectList_InCriteria");
                commandText = String.Format(ResManager.GetDBScript("Script_ObjectListAll"), inCr, filter);
            }
            else
                commandText = String.Format(ResManager.GetDBScript("Script_ObjectList"), objType, filter);

            grdSource.DataSource = null;

            if (_dtSource != null)
            {
                _dtSource.Dispose();
                _dtSource = null;
            }


            using (SqlConnection conn = _cp.CreateSqlConnection(true, false))
            {

                SqlCommand cmd = new SqlCommand(commandText);
                cmd.Connection = conn;

                SqlDataAdapter ad = new SqlDataAdapter();
                try
                {
                    ad.SelectCommand = cmd;
                    _dtSource = new DataTable();
                    ad.Fill(_dtSource);
                    bsSource.DataSource = _dtSource;
                    grdSource.DataSource = bsSource;
                }
                finally
                {
                    ad.Dispose();
                    ad = null;
                }
            }
        }

        private void AddSelectedToDest()
        {
            if (grdSource.SelectedRows.Count == 0)
                return;


            DbObjectInfo sel = null;
            foreach (DataGridViewRow row in grdSource.SelectedRows)
            {
                sel = new DbObjectInfo();

                sel.Name = (string)row.Cells[colSourceName.Name].Value;
                sel.ObjType = (string)row.Cells[colSourceType.Name].Value;
                sel.Owner = (string)row.Cells[colSourceOwner.Name].Value;
                if (_selectedObjects.Contains(sel))
                    continue;

                _selectedObjects.Add(sel);
            }

            bsDest.ResetBindings(false);
        }

        private void AddAllToDest()
        {
            DbObjectInfo sel = null;
            foreach (DataGridViewRow row in grdSource.Rows)
            {
                sel = new DbObjectInfo();
                sel.Name = (string)row.Cells[colSourceName.Name].Value;
                sel.ObjType = (string)row.Cells[colSourceType.Name].Value;
                sel.Owner = (string)row.Cells[colSourceOwner.Name].Value;

                if (_selectedObjects.Contains(sel))
                    continue;
                _selectedObjects.Add(sel);
            }

            bsDest.ResetBindings(false);
        }

        private void RemoveSelectedFromDest()
        {
            if (grdDest.SelectedRows.Count == 0)
                return;

            IList<DbObjectInfo> removeList = new List<DbObjectInfo>();

            foreach (DataGridViewRow row in grdDest.SelectedRows)
            {
                removeList.Add(row.DataBoundItem as DbObjectInfo);
            }

            foreach (DbObjectInfo sel in removeList)
                _selectedObjects.Remove(sel);
            removeList.Clear();
            bsDest.ResetBindings(false);
        }

        private void RemoveAllFromDest()
        {
            _selectedObjects.Clear();
            bsDest.ResetBindings(false);
        }

        private void LoadDestFromFile()
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string fileContent = String.Empty;
            using (StreamReader fs = File.OpenText(openFileDialog1.FileName))
            {
                fileContent = fs.ReadToEnd();
            }

            if (String.IsNullOrEmpty(fileContent))
            {
                MessageService.ShowError("No content available in the selected file!");
                return;
            }


            PrepareObjectFromContent(fileContent);
        }

        private void LoadDestFromEditor()
        {
            ITextEditor editor = HostServicesSingleton.HostServices.EditorServices.CurrentEditor;
            if (editor == null)
                return;

            string content = String.IsNullOrEmpty(editor.SelectedText) ? editor.Content : editor.SelectedText;
            if (String.IsNullOrEmpty(content))
            {
                MessageService.ShowError("No content available or selected in the current text editor!");
                return;
            }

            PrepareObjectFromContent(content);
        }

        private void LoadFromObjectExplorer()
        {
            if (HostServicesSingleton.HostServices == null || HostServicesSingleton.HostServices.ObjectExplorerService == null)
                throw new InvalidOperationException("ObjectExplorer is not active or visible!");

            IList<ObjectExplorerNode> selNodes = HostServicesSingleton.HostServices.ObjectExplorerService.SelNodes;
            StringBuilder sb = new StringBuilder();
            string template = "{0}=[{1}].[{2}]";
            string objType = String.Empty;
            foreach (ObjectExplorerNode node in selNodes)
            {
                if (!DBObjectType.CanTypeBeDumpedForScriptingWizardUsage(node.Type) || node.ConnParams == null)
                    continue;

                if (node.ConnParams.Server != _cp.Server || !node.DatabaseName.Equals(_cp.Database, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                objType = DbObjectListUtils.EncodeObjectExplorerNodeType(node.Type);
                sb.AppendLine(String.Format(template, objType, node.Owner, node.Name));
            }

            PrepareObjectFromContent(sb.ToString());
        }

        public string DumpSelectedObjects()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                pbDump.Value = 0;
                pbDump.Maximum = _selectedObjects.Count;
                pbDump.Visible = true;

                IList<DbObjectInfo> objects = this.SelectedObjects;
                foreach (DbObjectInfo info in objects)
                {
                    if (!IsObjectTypeValid(info.ObjType))
                        continue;
                    string objType = DbObjectListUtils.EncodeObjectType(info.ObjType);
                    sb.AppendLine(objType + "=" + QualifiedStr(info.Owner) + "." + QualifiedStr(info.Name));
                    pbDump.Value++;
                    pbDump.Invalidate();
                }
            }
            finally
            {
                pbDump.Visible = false;
            }

            return sb.ToString();
        }

        public void DumpSelectedObjectsToTextEditor()
        {
            if (_selectedObjects.Count == 0)
            {
                MessageService.ShowError("You did not selected any objects.");
                return;
            }

            string caption = "Objects [" + _cp.InfoDbServer + "]";
            HostServicesSingleton.HostServices.EditorServices.CreateTextEditor(caption, DumpSelectedObjects());
            if (_dumpToTexteditorCompleted != null)
                _dumpToTexteditorCompleted(this, EventArgs.Empty);
        }

        private string QualifiedStr(string value)
        {
            string result = value;
            if (!value.StartsWith("["))
                result = "[" + result;

            if (!value.EndsWith("]"))
                result = result + "]";

            return result;
        }

        #endregion //Methods

        #region Batch Config Parsing
        public void PrepareObjectFromContent(string content)
        {
            _selectedObjects.Clear();
            bsDest.DataSource = null;

            string newContent = content.Replace("\r", String.Empty);
            string pattern = @"(?<ObjType>SPR|FNC|TBL|TRG|VW)\s*\=\s*\[{0,1}\s*(?<Schema>\w*)\s*\]{0,1}\s*\.{1}\s*\[{0,1}\s*(?<ObjName>\w*)\s*\]{0,1}\s*";
            MatchCollection matches = Regex.Matches(newContent, pattern, RegexOptions.IgnoreCase);

            string objType = String.Empty;
            string schema = String.Empty;
            string objName = String.Empty;
            DbObjectInfo sel = null;
            foreach (Match m in matches)
            {
                if (!m.Success)
                    continue;

                objType = String.Empty;
                schema = String.Empty;
                objName = String.Empty;

                objType = m.Groups["ObjType"].Value;
                schema = m.Groups["Schema"].Value;
                objName = m.Groups["ObjName"].Value;

                if (String.IsNullOrEmpty(objType) || String.IsNullOrEmpty(schema) || String.IsNullOrEmpty(objName))
                    continue;

                objType = DbObjectListUtils.DecodeObjectType(objType);
                if (!IsObjectTypeValid(objType))
                    continue;

                if (string.IsNullOrEmpty(objType))
                    continue;

                sel = new DbObjectInfo();
                sel.Name = objName;
                sel.Owner = schema;
                sel.ObjType = objType;

                _selectedObjects.Add(sel);
            }

            bsDest.DataSource = _selectedObjects;
        }



        #endregion //Batch Config Parsinsg

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PerformListObjects();
        }

        private void PerformListObjects()
        {
            if (cmbType.SelectedIndex < 0)
            {
                MessageService.ShowError("Please select object type!");
            }

            ListObjects(cmbType.Text, (String.IsNullOrEmpty(edtFilter.Text) ? "%" : edtFilter.Text));
        }

        private void btnAddSingle_Click(object sender, EventArgs e)
        {
            AddSelectedToDest();
        }

        private void btnRemoveSingle_Click(object sender, EventArgs e)
        {
            RemoveSelectedFromDest();
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            AddAllToDest();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            RemoveAllFromDest();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Resources.GenerateScript_Help, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void grdSource_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            AddSelectedToDest();
        }

        private void grdDest_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            RemoveSelectedFromDest();
        }

        private void edtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PerformListObjects();
        }

        private void bsDest_ListChanged(object sender, ListChangedEventArgs e)
        {
            selCnt.Text = String.Format("Selected Objects: {0}", bsDest.Count);
        }

        private void bsSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            selMatch.Text = String.Format("Available Objects: {0}", bsSource.Count);
        }

        private void currentEditorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DumpSelectedObjectsToTextEditor();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDestFromFile();
        }

        private void objectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFromObjectExplorer();
        }

        private void currentEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDestFromEditor();
        }
    }

    [Flags]
    public enum DbObjectListObjectTypes
    {
        Table = 1,
        StoredProcedure = 2,
        UserDefinedFunction = 4,
        View = 8
    }

    public static class DbObjectListUtils
    {
        public static string DecodeObjectType(string value)
        {
            switch (value.ToLowerInvariant())
            {
                case "tbl":
                    return "Table";
                case "spr":
                    return "StoredProcedure";
                case "fnc":
                    return "UserDefinedFunction";
                case "vw":
                    return "View";
                default:
                    throw new Exception("Unsupported object type! Can not decode.");
            }
        }

        public static string EncodeObjectType(string value)
        {
            switch (value)
            {
                case "Table":
                    return "TBL";
                case "StoredProcedure":
                    return "SPR";
                case "UserDefinedFunction":
                    return "FNC";
                case "View":
                    return "VW";
                default:
                    throw new Exception("Unsupported object type! Can not encode.");
            }
        }

        public static int EncodeObjectTypePriority(string value)
        {
            switch (value)
            {
                case "Table":
                    return 1;
                case "View":
                    return 2;
                case "StoredProcedure":
                    return 3;
                case "UserDefinedFunction":
                    return 4;
                default:
                    throw new Exception("Unsupported object type! Can not encode object type priority.");
            }
        }

        public static string EncodeObjectExplorerNodeType(int typeId)
        {

            switch (typeId)
            {
                case DBObjectType.Table:
                case DBObjectType.UserTable:
                    return "TBL";
                case DBObjectType.StoredProc:
                    return "SPR";
                case DBObjectType.TableValuedFunction:
                case DBObjectType.ScalarValuedFunction:
                    return "FNC";
                case DBObjectType.View:
                    return "VW";
                default:
                    throw new Exception("Unsupported object type! Can not encode.");
            }
        }
    }

}
