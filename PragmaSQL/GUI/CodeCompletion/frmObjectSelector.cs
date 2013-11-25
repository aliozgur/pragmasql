using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using PragmaSQL.Common;
using PragmaSQL.Database;

namespace PragmaSQL.GUI
{
  public partial class frmObjectSelector : Form
  {
    private DataSet _tblDatabases = null;
    private BindingSource _bsDatabases = new BindingSource();

    private DataSet _tblUsers = null;
    private BindingSource _bsUsers = new BindingSource();

    private DataSet _tblObjects = null;
    private BindingSource _bsObjects = new BindingSource();
    private SqlConnection _conn = new SqlConnection();

    private string _currentDatabase = String.Empty;
    private string _defaultDatabase = String.Empty;

    private bool _isInitialized = false;
    public bool IsInitialized
    {
      get { return _isInitialized; }
    }

    public event NotificationHandler UserRequestedDismiss;
    public event NotificationHandler UserMadeFinalSelection;
    public event KeyPressHandler UserPressedKey;
    public event NotificationHandler UserPressedBackSpace;

    public frmObjectSelector( )
    {
      InitializeComponent();
    }

    public void SetLocation( int x, int y )
    {
      this.Location = new Point(x, y + 15);
    }


    public void Initialize( SqlConnection conn )
    {
      if (conn == null)
      {
        throw new Exception("Database connection is null!");
      }

      if (conn.State != ConnectionState.Open)
      {
        throw new InvalidConnectionState("Connection is in state \"" + _conn.State.ToString() + "\".");
      }

      if (_conn.State == ConnectionState.Open)
      {
        _conn.Close();
      }

      _conn.ConnectionString = conn.ConnectionString;
      _defaultDatabase = conn.Database.ToLower();
    }

    private void PopulateDatabases( )
    {
      if (_tblDatabases == null)
      {
        _tblDatabases = new DataSet();
      }
      else
      {
        _tblDatabases.Clear();
      }

      SqlCommand cmd = new SqlCommand(global::PragmaSQL.Properties.Resources.Script_GetDatabases, _conn);
      SqlDataAdapter adapter = new SqlDataAdapter();
      adapter.SelectCommand = cmd;
      adapter.Fill(_tblDatabases);

      _bsDatabases.DataSource = _tblDatabases.Tables[0];
    }

    private void PopulateUsers( )
    {
      if (_tblUsers == null)
      {
        _tblUsers = new DataSet();
      }
      else
      {
        _tblUsers.Clear();
      }

      SqlCommand cmd = new SqlCommand(global::PragmaSQL.Properties.Resources.Script_GetUsers, _conn);
      SqlDataAdapter adapter = new SqlDataAdapter();
      adapter.SelectCommand = cmd;
      adapter.Fill(_tblUsers);

      _bsUsers.DataSource = _tblUsers.Tables[0];
    }

    private void PopulateObjects( )
    {
      if (_tblObjects == null)
      {
        _tblObjects = new DataSet();
      }
      else
      {
        _tblObjects.Clear();
      }


      SqlCommand cmd = new SqlCommand(global::PragmaSQL.Properties.Resources.Script_CodeCompletionProposal, _conn);
      SqlDataAdapter adapter = new SqlDataAdapter();
      adapter.SelectCommand = cmd;
      adapter.Fill(_tblObjects);
    }

    public void PopulateCodeCompletionList( )
    {
      try
      {
        label1.Visible = false;
        Application.UseWaitCursor = true;

        _conn.Open();
        if (_conn.Database.ToLower() != _currentDatabase)
        {
          _conn.ChangeDatabase(_currentDatabase);
        }


        _bsObjects.Sort = String.Empty;
        _bsObjects.Filter = String.Empty;
        _bsObjects.RaiseListChangedEvents = false;

        PopulateDatabases();
        PopulateUsers();
        PopulateObjects();

        if (_bsObjects.Count > 0)
        {
          _bsObjects.Position = 0;
        }

        _isInitialized = true;
      }
      finally
      {
        _conn.Close();
        BindGrid();
        _bsObjects.RaiseListChangedEvents = true;
        Application.UseWaitCursor = false;
        label1.Visible = true;

      }
    }

    private bool IsValidDatabase( string dbName )
    {
      if (_tblDatabases == null)
      {
        return false;
      }
      return (_bsDatabases.Find("Name", dbName) >= 0);
    }

    private bool IsValidUser( string userName )
    {
      if (_tblUsers == null)
      {
        return false;
      }
      return (_bsUsers.Find("Name", userName) >= 0);
    }

    private void BindGrid( )
    {
      if (_bsObjects.DataSource != null)
      {
        return;
      }

      _bsObjects.DataSource = _tblObjects.Tables[0];
      grdObjects.DataSource = _bsObjects;

      foreach (DataGridViewColumn col in grdObjects.Columns)
      {
        if (col.DataPropertyName == "Type" || col.DataPropertyName == "Name")
        {
          col.Visible = true;
        }
        else
        {
          col.Visible = false;
        }
      }

      _bsObjects.Sort = "Order ASC, Name ASC";
    }

    private void frmCodeCompletion_Deactivate( object sender, EventArgs e )
    {
      this.Hide();
    }


    public void JumpTo( string jumpString )
    {
      string filterStr = String.Empty;
      string dbName = _defaultDatabase;
      string schemaName = "???";
      bool isInvalidFilter = false;

      label1.Text = jumpString;
      StringTokenizer tok = new StringTokenizer(jumpString);
      IList<Token> tokens = new List<Token>();

      Token token = null;
      do
      {
        token = tok.Next();

        if (token.Kind == TokenKind.EOL || token.Kind == TokenKind.EOF || token.Kind == TokenKind.Unknown
            || (token.Kind == TokenKind.Symbol && token.Value != ".")
          )
        {
          continue;
        }
        tokens.Add(token);

      } while (token.Kind != TokenKind.EOF);


      if(tokens.Count == 0)
      {
        filterStr = " ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr')";
        //  + " AND ( Catalog = '" + _defaultDatabase + "' OR Catalog = '') "
        //  + " AND ( ( Schema = 'dbo') OR (Schema = '') )";
        schemaName = "dbo";
      }
      // 1-  Object1
      else if (tokens.Count == 1)
      {
        filterStr = " ( Type <> 'Param' AND Type <> 'Col' ) "
          + " AND ( Name Like '" + tokens[0].Value + "%') ";
          //+ " AND ( Catalog = '" + _defaultDatabase + "')"
          //+ " AND ( Schema = 'dbo')";
        
        schemaName = "dbo";
      }
      // 2- Object1.
      else if (tokens.Count == 2)
      {
        Token lastToken = tokens[1];

        if (lastToken.Value == ".")
        {
          Token parentToken = tokens[0];
          if (IsValidDatabase(parentToken.Value))
          {
            dbName = parentToken.Value;
            filterStr = "( Type = 'Usr' )";
          }
          else
          {
            dbName = _defaultDatabase;
            filterStr = "ParentName = '" + parentToken.Value + "' "
              + " AND ( Catalog = '" + _defaultDatabase + "') "
              + " AND ( Schema = 'dbo' ) "
              + " AND ( Type = 'Param' OR Type = 'Col' ) ";
            schemaName = "dbo";
          }
        }
        else
        {
          dbName = _defaultDatabase;
          // Invalid filter
          filterStr = " ( Type = '@@@')";
          isInvalidFilter = true;
        }
      }
      // 3- Object1.Object2, Object1..
      else if (tokens.Count == 3)
      {
        Token lastToken = tokens[2];
        // 3.1 - Object1..
        if (lastToken.Value == ".")
        {
          Token parentToken = tokens[0];
          if (IsValidDatabase(parentToken.Value))
          {
            dbName = parentToken.Value;
            filterStr = "( Catalog = '" + dbName + "') "
              + " AND ( Schema = 'dbo' ) "
              + " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Db' AND Type <> 'Usr') ";
            
            schemaName = "dbo";
          }
          else
          {
            dbName = _defaultDatabase;
            // Invalid filter
            filterStr = " ( Type = '@@@')";
            isInvalidFilter = true;
          }
        }
        // 3.2 - Object1.Object2
        else
        {
          Token parentToken = tokens[0]; // Object1
          if (IsValidDatabase(parentToken.Value))
          {
            dbName = parentToken.Value;
            filterStr = "Name Like '" + tokens[2].Value + "%' "
              + " AND ( Type = 'Usr')";
            schemaName = "dbo";
          }
          else
          {
            dbName = _defaultDatabase;
            filterStr = "ParentName = '" + parentToken.Value + "' "
              + " AND ( Catalog = '" + dbName + "') "
              + " AND ( Schema = 'dbo' ) "
              + " AND ( Type = 'Param' OR Type = 'Col') "
              + " AND ( Name Like '" + tokens[2].Value + "%') ";
            schemaName = "dbo";
          }
        }
      }
      // 4- Object1.Object2.
      else if (tokens.Count == 4)
      {

        if (tokens[3].Value == ".") //.
        {
          if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
          {
            dbName = tokens[0].Value;
            filterStr = "( Catalog = '" + dbName + "') "
              + " AND ( Schema = '" + tokens[2].Value + "')"
              + " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr' AND Type <> 'Db' ) ";
            schemaName = tokens[2].Value ;
          }
          else
          {
            dbName = _defaultDatabase;
            // Invalid filter
            filterStr = " ( Type = '@@@')";
            isInvalidFilter = true;
          }
        }
        else
        {
          dbName = _defaultDatabase;
          // Invalid filter
          filterStr = " ( Type = '@@@')";
          isInvalidFilter = true;
        }
      }
      // 5- Object1.Object2.Object3
      else if (tokens.Count == 5)
      {
        if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
        {
          dbName = tokens[0].Value;
          filterStr = "( Catalog = '" + dbName + "') "
            + " AND ( Schema = '" + tokens[2].Value + "')"
            + " AND ( Type <> 'Param' AND Type <> 'Col' AND Type <> 'Usr' AND Type <> 'Db' ) "
            + " AND ( Name Like '" + tokens[4].Value + "%') ";
          schemaName = tokens[2].Value;
        }
        else
        {
          dbName = _defaultDatabase;
          // Invalid filter
          filterStr = " ( Type = '@@@')";
          isInvalidFilter = true;
        }
      }
      // 6- Object1.Object2.Object3.
      else if (tokens.Count == 6)
      {
        if (tokens[5].Value == ".")
        {
          if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
          {
            dbName = tokens[0].Value;
            filterStr = "( Catalog = '" + dbName + "') "
              + " AND ( Schema = '" + tokens[2].Value + "')"
              + " AND ( Type = 'Param' OR Type = 'Col')"
              + " AND ( ParentName = '" + tokens[4].Value + "') ";
            schemaName = tokens[2].Value;
          }
          else
          {
            dbName = _defaultDatabase;
            // Invalid filter
            filterStr = " ( Type = '@@@')";
            isInvalidFilter = true;
          }
        }
        else
        {
          dbName = _defaultDatabase;
          // Invalid filter
          filterStr = " ( Type = '@@@')";
          isInvalidFilter = true;
        }

      }
      // 7- Object1.Object2.Object3.Object4
      else if (tokens.Count == 7)
      {
        if (IsValidDatabase(tokens[0].Value) && IsValidUser(tokens[2].Value)) // Object1 and Object2
        {
          dbName = tokens[0].Value;
          filterStr = "( Catalog = '" + dbName + "') "
            + " AND ( Schema = '" + tokens[2].Value + "')"
            + " AND ( Type = 'Param' OR Type = 'Col')"
            + " AND ( ParentName = '" + tokens[4].Value + "') "
            + " AND ( Name Like '" + tokens[6].Value + "%') ";
        }
        else
        {
          dbName = _defaultDatabase;
          // Invalid filter
          filterStr = " ( Type = '@@@')";
          isInvalidFilter = true;
        }
      }
      else
      {
        dbName = _defaultDatabase;
        // Invalid filter
        filterStr = " ( Type = '@@@')";
        isInvalidFilter = true;
      }

      if (_currentDatabase != dbName)
      {
        _currentDatabase = dbName;
        PopulateCodeCompletionList();
      }

      if (_bsObjects.DataSource == null || _tblObjects == null)
      {
        this.Hide();
        return;
      }

      _bsObjects.Filter = filterStr;
      _bsObjects.Sort = "Order ASC, Name ASC";

      if (_bsObjects.Count > 0)
      {
        _bsObjects.Position = 0;
      }

      if(!isInvalidFilter)
      {
        label2.Text = "Catalog = '" + dbName + "', Schema = '" + schemaName + "'";
      }
      else
      {
        label2.Text = "Can not parse code completion proposal!";
      }
    }

    public void JumpTo_Old( string jumpString )
    {
      string dbName = _defaultDatabase;

      label1.Text = jumpString;
      StringTokenizer tok = new StringTokenizer(jumpString);
      IList<Token> tokens = new List<Token>();

      Token token = null;
      do
      {
        token = tok.Next();
        if (token.Kind == TokenKind.EOL || token.Kind == TokenKind.EOF || token.Kind == TokenKind.Unknown
            || (token.Kind == TokenKind.Symbol && token.Value != ".")
          )
        {
          continue;
        }
        tokens.Add(token);

      } while (token.Kind != TokenKind.EOF);

      // By default we do not want params and cols in list.
      string filterStr = "( Name Like'%%') AND ( Type <> 'Param' AND Type <> 'Col' )";

      if (tokens.Count > 0)
      {
        Token lastToken = tokens[tokens.Count - 1];

        //i.e: Catalog.Schema.DBObject.Colu
        if (lastToken.Kind == TokenKind.Word)
        {
          // 
          if (tokens.Count - 3 >= 0)
          {
            Token parentToken = tokens[tokens.Count - 3];
            // Check if parent token is a user.
            if (parentToken.Value == ".")
            {
              // We must try to find the database
              if (tokens.Count - 4 >= 0)
              {
                Token dbToken = tokens[tokens.Count - 4];
                dbName = dbToken.Value.ToLower();
                filterStr = "( Schema = '" + "dbo" + "' ) "
                  + " AND ( Catalog = '" + dbToken.Value + "' ) "
                  + " AND ( Name Like '" + lastToken.Value + "%' ) "
                  + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
              }
              // No need to find the database
              else
              {
                filterStr = "( Schema = '" + "dbo" + "') "
                  + " AND ( Name Like '" + lastToken.Value + "%' ) "
                  + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
              }
            }
            else if (IsValidUser(parentToken.Value))
            {
              // We must try to find the database
              if (tokens.Count - 5 >= 0)
              {
                Token dbToken = tokens[tokens.Count - 5];
                dbName = dbToken.Value.ToLower();
                filterStr = "( Schema = '" + parentToken.Value + "' ) "
                  + " AND ( Catalog = '" + dbToken.Value + "' ) "
                  + " AND ( Name Like '" + lastToken.Value + "%' ) "
                  + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
              }
              // No need to find the database
              else
              {
                filterStr = "( Schema = '" + parentToken.Value + "') "
                  + " AND ( Name Like '" + lastToken.Value + "%' ) "
                  + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
              }
            }
            //Check if parent token is a database
            else if (IsValidDatabase(parentToken.Value))
            {
              dbName = parentToken.Value.ToLower();
              filterStr = "( Type = 'Usr') "
                + " AND ( Name Like '" + lastToken.Value + "%' )";
            }
            else
            {
              filterStr = "( ParentName = '" + parentToken.Value + "') "
              + " AND ( Name Like '" + lastToken.Value + "%' ) ";
            }
          }
          // Parent token does not exist
          else
          {
            filterStr = "( Name Like '" + lastToken.Value + "%' ) "
            + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
          }
        }
        else if (lastToken.Value == ".")
        {
          if (tokens.Count - 2 >= 0)
          {
            Token parentToken = tokens[tokens.Count - 2];
            // Check if parent token is a user.
            if (parentToken.Value == "." || IsValidUser(parentToken.Value))
            {

              // We must try to find the database
              if (tokens.Count - 4 >= 0 && parentToken.Value != ".")
              {
                Token dbToken = tokens[tokens.Count - 4];
                dbName = dbToken.Value.ToLower();
                filterStr = "( Schema = '" + parentToken.Value + "' ) "
                  + " AND ( Catalog = '" + dbToken.Value + "' ) "
                  + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
              }
              else if (tokens.Count - 3 >= 0 && parentToken.Value == ".")
              {
                Token dbToken = tokens[tokens.Count - 3];
                dbName = dbToken.Value.ToLower();
                filterStr = "( Schema = '" + "dbo" + "' ) "
                  + " AND ( Catalog = '" + dbToken.Value + "' ) "
                  + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
              }
              // No need to find the database
              else
              {
                filterStr = "( Schema = '" + parentToken.Value + "') "
                  + " AND ( Type <> 'Param' AND Type <> 'Col' ) ";
              }
            }
            //Check if parent token is a database
            else if (IsValidDatabase(parentToken.Value))
            {
              dbName = parentToken.Value.ToLower();
              filterStr = "( Type = 'Usr')";
            }
            else
            {
              filterStr = "ParentName = '" + parentToken.Value + "'";
            }
          }
        }
      }

      if (_currentDatabase != dbName)
      {
        _currentDatabase = dbName;
        PopulateCodeCompletionList();
      }

      if (_bsObjects.DataSource == null || _tblObjects == null)
      {
        this.Hide();
        return;
      }

      _bsObjects.Filter = filterStr;
      _bsObjects.Sort = "Order ASC, Name ASC";

      if (_bsObjects.Count > 0)
      {
        _bsObjects.Position = 0;
      }
    }

    private void grdObjects_KeyDown( object sender, KeyEventArgs e )
    {
      if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete) && UserRequestedDismiss != null)
      {
        UserRequestedDismiss();
      }

      if (e.KeyCode == Keys.Back && UserPressedBackSpace != null)
      {
        UserPressedBackSpace();
        e.Handled = true; //don't want this to turn in to a keypress
      }

      if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode == Keys.Tab) && UserMadeFinalSelection != null)
      {
        UserMadeFinalSelection();
      }
    }

    private void grdObjects_KeyPress( object sender, KeyPressEventArgs e )
    {
      if (UserPressedKey != null)
      {
        UserPressedKey(e.KeyChar);
      }
      e.Handled = true;
    }

    private void grdObjects_DoubleClick( object sender, EventArgs e )
    {
      UserMadeFinalSelection();
    }

    public string SelectedItem
    {
      get
      {
        DataRowView row = _bsObjects.Current as DataRowView;
        if (row == null)
        {
          return String.Empty;
        }

        return (string)row["Name"];
      }
    }

    public ArrayList SelectedItemsAsStrings
    {
      get
      {
        ArrayList results = new ArrayList();
        foreach (DataGridViewRow row in grdObjects.SelectedRows)
        {
          results.Add((string)row.Cells["colName"].Value);
        }
        return results;
      }
    }

    public bool HasMultipleSelection
    {
      get
      {
        return (grdObjects.SelectedRows.Count > 1);
      }
    }

    private void grdObjects_CellPainting( object sender, DataGridViewCellPaintingEventArgs e )
    {

      if (e.Value != null && e.Value.GetType() != typeof(DBNull) && e.ColumnIndex == 1)
      {
        Color gridBrushColor = ((DataGridView)sender).GridColor;
        Color bgColor = Color.WhiteSmoke;

        if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
        {
          bgColor = SystemColors.Highlight;
        }

        Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
            e.CellBounds.Y + 1, e.CellBounds.Width - 4,
            e.CellBounds.Height - 4);


        using (
            Brush gridBrush = new SolidBrush(gridBrushColor),
            backColorBrush = new SolidBrush(bgColor))
        {


          using (Pen gridLinePen = new Pen(gridBrush))
          {
            // Erase the cell.
            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

            // Draw the grid lines (only the right and bottom lines;
            // DataGridView takes care of the others).
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                e.CellBounds.Bottom, e.CellBounds.Right,
                e.CellBounds.Bottom);
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                e.CellBounds.Top, e.CellBounds.Right - 1,
                e.CellBounds.Bottom);

            // Draw the text content of the cell, ignoring alignment.
            Brush br = null;
            if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.Selected)
            {
              br = Brushes.Black;
            }
            else
            {
              br = SystemBrushes.HighlightText;
            }

            Image img = null;
            switch ((string)e.Value)
            {
              case "Db":
                img = ImageList1.Images[0];
                break;
              case "Usr":
                img = ImageList1.Images[1];
                break;
              case "Tbl":
                img = ImageList1.Images[2];
                break;
              case "Vw":
                img = ImageList1.Images[3];
                break;
              case "Pr":
                img = ImageList1.Images[4];
                break;
              case "Fu":
                img = ImageList1.Images[5];
                break;
              case "Col":
                img = ImageList1.Images[6];
                break;
              case "Param":
                img = ImageList1.Images[7];
                break;
              default:
                img = null;
                break;
            }

            if (img != null)
            {
              e.Graphics.DrawImageUnscaled(img, e.CellBounds.X + (e.CellBounds.Width - 16) / 2, e.CellBounds.Y + (e.CellBounds.Height - 16) / 2);
              e.Handled = true;
            }
          }

        }
      }
    }

    private void button1_Click( object sender, EventArgs e )
    {
      string filter = _bsObjects.Filter;
      string sort = _bsObjects.Sort;
            
      PopulateCodeCompletionList();
      
      _bsObjects.Filter = filter;
      _bsObjects.Sort = sort;
      
      if (_bsObjects.Count > 0)
      {
        _bsObjects.Position = 0;
      }
    }
  }

  public delegate void NotificationHandler( );
  public delegate void KeyPressHandler( char character );


}