using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public partial class DbObjectSelectorCombo : UserControl
  {
    public DbObjectSelectorCombo( )
    {
      InitializeComponent();
    }

    public string Caption
    {
      get { return label1.Text; }
      set { label1.Text = value; }
    }

    private ConnectionParams _cp;
    [Browsable(false)]
    public ConnectionParams Cp
    {
      get { return _cp; }
      set
      {
        if (value != null)
          _cp = value.CreateCopy();
        else
          _cp = value;

      }
    }

    private DbObjectSelectorItemTypes _itemTypes = DbObjectSelectorItemTypes.All;
    [Browsable(false)]
    public DbObjectSelectorItemTypes ItemTypes
    {
      get { return _itemTypes; }
    }
    
    public void SetItemTypes(DbObjectSelectorItemTypes types)
    {
      _itemTypes = types; 
    }


    public DbObjectSelectorItem SelectedItem
    {
      get { return cmb.SelectedItem as DbObjectSelectorItem; }
    }

    public ComboBox Items
    {
      get { return cmb; }
    }


    public DbObjectSelectorItemTypes GetItemType( string type )
    {
      string tmp = type.ToLowerInvariant();
      switch (tmp)
      {
        case "fn":
        case "if":
        case "tf":
          return DbObjectSelectorItemTypes.Function;
        case "p":
        case "x":
          return DbObjectSelectorItemTypes.Procedure;
        case "u":
          return DbObjectSelectorItemTypes.UserTable;
        case "s":
          return DbObjectSelectorItemTypes.SystemTable;
        case "v":
          return DbObjectSelectorItemTypes.View;
        case "tr":
          return DbObjectSelectorItemTypes.Trigger;
        default:
          return DbObjectSelectorItemTypes.None;
      }
    }
    
    private string GenerateInFilter( )
    {
      string result = String.Empty;
      string sep = String.Empty;

      if ((_itemTypes & DbObjectSelectorItemTypes.None) == DbObjectSelectorItemTypes.None)
        return String.Empty;

      if ((_itemTypes & DbObjectSelectorItemTypes.All) == DbObjectSelectorItemTypes.All)
        return "('FN','IF','TF','P','X','S','TR','U','V')";

      if ((_itemTypes & DbObjectSelectorItemTypes.Function) == DbObjectSelectorItemTypes.Function)
      {
        result += (String.IsNullOrEmpty(sep) ? String.Empty : sep);
        result += "'FN', 'IF', 'TF'";
        sep = ", ";
      }

      if ((_itemTypes & DbObjectSelectorItemTypes.Procedure) == DbObjectSelectorItemTypes.Procedure)
      {
        result += (String.IsNullOrEmpty(sep) ? String.Empty : sep);
        result += "'P', 'X'";
        sep = ", ";
      }

      if ((_itemTypes & DbObjectSelectorItemTypes.SystemTable) == DbObjectSelectorItemTypes.SystemTable)
      {
        result += (String.IsNullOrEmpty(sep) ? String.Empty : sep);
        result += "'S'";
        sep = ", ";
      }

      if ((_itemTypes & DbObjectSelectorItemTypes.Trigger) == DbObjectSelectorItemTypes.Trigger)
      {
        result += (String.IsNullOrEmpty(sep) ? String.Empty : sep);
        result += "'TR'";
        sep = ", ";
      }

      if ((_itemTypes & DbObjectSelectorItemTypes.UserTable) == DbObjectSelectorItemTypes.UserTable)
      {
        result += (String.IsNullOrEmpty(sep) ? String.Empty : sep);
        result += "'U'";
        sep = ", ";
      }

      if ((_itemTypes & DbObjectSelectorItemTypes.View) == DbObjectSelectorItemTypes.View)
      {
        result += (String.IsNullOrEmpty(sep) ? String.Empty : sep);
        result += "'V'";
        sep = ", ";
      }

      if (!String.IsNullOrEmpty(result))
        result = "(" + result + ")";
      
      return result;
    }

    public void LoadObjects( ConnectionParams cp, bool wantEmpty )
    {
      cmb.Items.Clear();

      string inFilter = GenerateInFilter();
      if (String.IsNullOrEmpty(inFilter))
        return;


      ConnectionParams tmpCp = cp;
      if (tmpCp == null)
        tmpCp = _cp;

      string cmdText = "declare @cmplevel int select @cmplevel = cmptlevel  from  master..sysdatabases where name = DB_NAME() ";
      cmdText += "select so.id, so.name, so.xtype,   CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(so.uid)  ELSE SCHEMA_NAME(so.uid) END 'owner' from sysobjects so";
      cmdText += " WHERE so.xtype in " + inFilter;
      cmdText += " order by CASE WHEN  @cmplevel  < 90 THEN  USER_NAME(so.uid) ELSE SCHEMA_NAME(so.uid) END, so.name";

      if (wantEmpty)
        cmb.Items.Add(null);

      using (SqlConnection conn = tmpCp.CreateSqlConnection(true, false))
      {
        SqlDataReader reader = null;
        DbObjectSelectorItem item = null;

        SqlCommand cmd = new SqlCommand(cmdText, conn);
        reader = cmd.ExecuteReader();
        try
        {
          while (reader.Read())
          {
            item = new DbObjectSelectorItem();
            item.ID = reader.GetInt32(0);
            item.Name = reader.GetString(1);
            item.ItemType = GetItemType(reader.GetString(2));
            item.Owner = reader.GetString(3);
            cmb.Items.Add(item);
          }
        }
        finally
        {
          if (reader != null && !reader.IsClosed)
            reader.Close();
        }
      }

      if (cmb.Items.Count > 0)
        cmb.SelectedIndex = 0;

    }
  }

  public class DbObjectSelectorItem
  {
    public string Owner;
    public string Name;
    public int ID;
    public DbObjectSelectorItemTypes ItemType;
    
    public override string ToString( )
    {
      if (String.IsNullOrEmpty(Owner))
        return Name;
      else
        return string.Format("[{0}].[{1}]",Owner,Name);
    }
  }

  [Flags]
  public enum DbObjectSelectorItemTypes
  {
    None = 0x01,
    UserTable = 0x02,
    SystemTable = 0x04,
    View = 0x08,
    Procedure = 0x16,
    Function = 0x32,
    Trigger = 0x64,
    All = 0x128
  }
}
