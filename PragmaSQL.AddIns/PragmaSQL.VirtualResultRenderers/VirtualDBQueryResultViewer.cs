using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using PragmaSQL.Core;

namespace PragmaSQL.VirtualResultRenderers
{
  public partial class VirtualDBQueryResultViewer : UserControl
  {
    #region Fields And Properties
    AsyncQuery asyncQry = null;

    private int _initialPos = -1;

		private IList<VirtualDataGridView> _grids = new List<VirtualDataGridView>();
		public IList<VirtualDataGridView> Grids
    {
			get { return new List<VirtualDataGridView>(_grids).AsReadOnly(); }
    }

    public int GridCount
    {
      get { return _grids.Count; }
    }

    public Color ResultBackColor
    {
      get { return pan.BackColor; }
      set 
      { 
        pan.BackColor = value;
        panel1.BackColor = value;
      }
    }

    private Color _splitterBackColor = Color.FromKnownColor(KnownColor.Control);
    public Color SplitterBackColor
    {
      get { return _splitterBackColor; }
      set { _splitterBackColor = value; }
    }

    private int _gridHeight = 220;
    public int GridHeight
    {
      get { return _gridHeight; }
      set { _gridHeight = value; }
    }

    public bool IsExecutingAsync
    {
      get { return asyncQry != null && asyncQry.IsExecuting; }
    }

    private ContextMenuStrip _gridContextMenuStrip;
    public ContextMenuStrip GridContextMenuStrip
    {
      get { return _gridContextMenuStrip; }
      set { _gridContextMenuStrip = value; }
    }

		private VirtualDataGridView _activeGrid = null;
		public VirtualDataGridView ActiveGrid
		{
			get { return _activeGrid; }
		}

		
		private IList<DataTable> _tables = new List<DataTable>();

		private ActiveGridChangedDelegate _activeGridChanged = null;
		public event ActiveGridChangedDelegate ActiveGridChanged
		{
			add { _activeGridChanged += value; }
			remove { _activeGridChanged -= value; }
		}

		private IDictionary<DataGridView, DataTableFilterSortData> _resultFilters = new Dictionary<DataGridView, DataTableFilterSortData>();

    #endregion //Fields And Properties

    #region CTOR
		public VirtualDBQueryResultViewer()
    {
      InitializeComponent();
      pan.Height = 400;
    }

    #endregion //CTOR

    #region Data binding
		
		public void RenderDataTable(DataTable tbl)
		{
			RenderDataTable(null, tbl);
		}

		public void RenderDataTable(IScriptEditor editor, DataTable tbl)
    {
      if (IsExecutingAsync)
        throw new Exception("Query is already executing!");

      _tables.Add(tbl);
			VirtualDataGridView grd = new VirtualDataGridView();
			grd.StatusAlwaysVisible = true;

			if (editor != null)
				editor.PrepareAddInSupportForResultContextMenu(grd.PopupMenu.Items);

			pan.Controls.Add(grd);
      grd.Parent = pan;
      grd.Height = _gridHeight;

			grd.Left = 0;
      grd.Top = Int32.MaxValue - 100;
      grd.Dock = DockStyle.Top;
			grd.RenderDataTable(tbl);
			grd.Enter += new EventHandler(grd_Enter);
      _grids.Add(grd);
      grd.BringToFront();
			Splitter splt = null;

			if (true)
      {
        splt = new Splitter();
        splt.SplitterMoved += new SplitterEventHandler(splt_SplitterMoved);
        splt.SplitterMoving += new SplitterEventHandler(splt_SplitterMoving);
        splt.BackColor = _splitterBackColor;
        splt.MinExtra = 15;
        splt.MinSize = 15;
        pan.Controls.Add(splt);
        splt.Dock = DockStyle.Top;
        splt.BringToFront();
      }

			pan.Height += grd.Height + ( splt != null ? splt.Height : 0);

		}

    void splt_SplitterMoving( object sender, SplitterEventArgs e )
    {
      if (_initialPos == -1)
        _initialPos = e.SplitY;
    }

    void splt_SplitterMoved( object sender, SplitterEventArgs e )
    {
      Splitter splt = sender as Splitter;
      pan.Height += e.Y - _initialPos;
      _initialPos = -1;
    }
  
    void grd_Enter( object sender, EventArgs e )
    {
			_activeGrid = sender as VirtualDataGridView;
		
			if (_activeGridChanged != null)
				_activeGridChanged(this, _activeGrid);
		}

		public void RenderDataSet(DataSet dataSet)
		{
			RenderDataSet(null, dataSet);
		}

    public void RenderDataSet(IScriptEditor editor, DataSet dataSet )
    {
      if (IsExecutingAsync)
        throw new Exception("Query is already executing!");

      foreach (DataTable tbl in dataSet.Tables)
      {
        RenderDataTable(editor,tbl);
      }
    }

    private void DisposeResultTables()
    {
      foreach (DataTable t in _tables)
      {
        t.Clear();
        t.Dispose();
      }
      _tables.Clear();
    }

    public void ClearAll( )
    {
      foreach (Control ctrl in pan.Controls)
      {
        ctrl.Dispose();
      }
      
			pan.Controls.Clear();
			
			//_activeGrid.Dispose();
			//_activeGrid = null;
      _grids.Clear();

			pan.Height = 400;
      DisposeResultTables();
    }

    #endregion Data binding

    #region Query Execution
    public void ExecuteAsync( ConnectionParams cp, string commandText )
    {
      if (IsExecutingAsync)
        throw new Exception("Query is already executing!");

      ClearAll();
      asyncQry = new AsyncQuery(cp.ConnectionString, commandText);
      asyncQry.AfterExecutionCompleted += new ExecutionCompletedDelegate(qry_AfterExecutionCompleted);
      asyncQry.Execute();
    }

    public void CancelAsync( )
    {
      if (!IsExecutingAsync)
        return;

      asyncQry.Cancel();
    }

    public void Execute( ConnectionParams cp, string commandText )
    {
      ClearAll();
      DataSet dataSet = new DataSet();
      SqlDataAdapter adapter = new SqlDataAdapter();
      SqlCommand cmd = new SqlCommand();

      try
      {
        using (SqlConnection conn = cp.CreateSqlConnection(true, false))
        {
          cmd.Connection = conn;
          cmd.CommandTimeout = 0;
          cmd.CommandText = commandText;

          adapter.SelectCommand = cmd;
          adapter.Fill(dataSet);
        }
      }
      finally
      {
        if (cmd != null)
        {
          cmd.Dispose();
          cmd = null;
        }
        if (adapter != null)
        {
          adapter.Dispose();
          adapter = null;
        }
      }
      RenderDataSet(dataSet);
    }

    void qry_AfterExecutionCompleted( ExecutionCompletedEventArgs args )
    {
      if (args.Cancelled | args.Error != null)
        return;

      RenderDataSet(args.Result);
      asyncQry = null;
    }

    #endregion // Query Execution
	
	}

	public delegate void ActiveGridChangedDelegate(object sender, VirtualDataGridView grd);
}
