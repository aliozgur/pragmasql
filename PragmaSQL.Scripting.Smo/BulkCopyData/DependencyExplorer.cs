
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using Microsoft.SqlServer.Management.Common;

using PragmaSQL.Core;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace PragmaSQL.Scripting.Smo
{
	[Flags]
	public enum DependentObjectTypes
	{
		Table=0,
		View=1,
		StoredProcedure=2,
		UserDefinedFunction=3,
		Trigger = 4
	}

	public class DependencyExplorer:IDisposable
	{
		#region Fields and Properties

		private Server srvr = null;
		private Database db = null;
		private ServerConnection sqlConn = null;

		private ConnectionParams _connParams = null;
		public ConnectionParams ConnParams
		{
			get { return _connParams; }
			private set
			{
				if (value != null)
				{
					_connParams = value.CreateCopy();
				}
				else
				{
					_connParams = value;
				}
			}
		}

		private ProgressReportEventHandler _walkingDependencies = null;
		public event ProgressReportEventHandler WalkingDependencies
		{
			add { _walkingDependencies += value; }
			remove { _walkingDependencies -= value; }
		}

		private DependentObjectTypes _objectTypesToBeExplored;
		public DependentObjectTypes ObjectTypesToBeExplored
		{
			get { return _objectTypesToBeExplored; }
			set { _objectTypesToBeExplored = value; }
		}

		#endregion //Fields and Properties

		#region Constructor
		public DependencyExplorer(ConnectionParams cp)
    {
			if (cp == null)
				throw new ArgumentNullException("cp", "Connection parameters object is null!");
			
			ConnParams = cp;
      using (SqlConnection conn = _connParams.CreateSqlConnection(false,false))
      {
        sqlConn = new ServerConnection(conn);
        srvr = new Server(sqlConn);

        db = srvr.Databases[_connParams.Database];
			}			
		}

    #endregion //Constructor

		#region Explore Dependencies

		private string QualifiedObjectName(Urn urn)
		{
			if (urn == null)
				return String.Empty;
			string name = urn.GetNameForType(urn.Type);
			string schema = urn.GetAttribute("Schema", urn.Type);
			return "[" + schema + "]" + ".[" + name + "]";
		}
	
		private bool CanExplore(Urn urn)
		{
			return CanExplore(urn.Type);
		}

		private bool CanExplore(string type)
		{
			DependentObjectTypes dType = (DependentObjectTypes)Enum.Parse(typeof(DependentObjectTypes), type);
			return (_objectTypesToBeExplored & dType) == dType;
		}

		private int GetImageIndex(Urn urn)
		{
			return GetImageIndex(urn.Type);
		}

		private int GetImageIndex(string type)
		{
			DependentObjectTypes dType = (DependentObjectTypes)Enum.Parse(typeof(DependentObjectTypes), type,true);
			return (int)dType;
		}

		
		private void TraverseTreeNodes(TreeNodeCollection nodes, DependencyTreeNode parent)
		{
			if (parent == null)
				return ;

			int imageIndex = GetImageIndex(parent.Urn);
			string name = QualifiedObjectName(parent.Urn);

			TreeNode parentNode = new TreeNode(name, imageIndex, imageIndex);

			nodes.Add(parentNode);

			TraverseTreeNodes(parentNode.Nodes, parent.FirstChild);			
			TraverseTreeNodes(nodes, parent.NextSibling);
		}

		private IList<string> _parentNames = new List<string>();
		private void PrepareTreeViewNodes(TreeNodeCollection appendTo,DependencyTree dTree)
		{
			_parentNames.Clear();
			if (dTree == null || dTree.Count == 0)
				return;

			TraverseTreeNodes(appendTo, dTree.FirstChild);
			return;
		}

		private Urn[] PrepareObjectUrns(IList<DbObjectList.DbObjectInfo> objects)
		{
			Urn[] urns = new Urn[objects.Count];
			DbObjectList.DbObjectInfo objInfo = null;
			string urnTemplate = "Server[@Name='{0}']/Database[@Name='{1}']/{2}[@Name='{3}' and @Schema='{4}']";///Schema[@Name='{4}']";

			for (int i = 0; i < objects.Count; i++)
			{

				objInfo = objects[i];
        urns[i] = new Urn(String.Format(urnTemplate, sqlConn.TrueName, _connParams.Database, objInfo.ObjType, objInfo.Name, objInfo.Owner));
			}
			return urns;
		}

		public void DiscoverDependencies(TreeNodeCollection appendTo, IList<DbObjectList.DbObjectInfo> objects)
		{
			if (objects == null || objects.Count == 0)
				return;

			DependencyWalker dWalk = new DependencyWalker(srvr);
			
			dWalk.DiscoveryProgress += new ProgressReportEventHandler(dWalk_DiscoveryProgress);
			DependencyTree dTree = dWalk.DiscoverDependencies(PrepareObjectUrns(objects), DependencyType.Parents);
			PrepareTreeViewNodes(appendTo, dTree);
		}

		private void dWalk_DiscoveryProgress(object sender, ProgressReportEventArgs e)
		{
			//while (_pauseScripting)
			//{
			//  Thread.Sleep(10);
			//}

			//if (_cancelRequested)
			//  throw new CancelledByUserException();

			if (_walkingDependencies != null)
				_walkingDependencies(this, e);
		}

		#endregion //Explore Dependencies

		#region IDisposable Members

		private bool _isDisposing = false;
		public void Dispose()
		{
			if (_isDisposing)
				return;


			_isDisposing = true;

			if (sqlConn == null)
				return;

			if (sqlConn.InUse)
				sqlConn.Cancel();

			sqlConn.Disconnect();
			sqlConn = null;


		}

		#endregion //IDisposable Members

	}
}
