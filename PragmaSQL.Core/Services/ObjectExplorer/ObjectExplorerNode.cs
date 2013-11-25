/********************************************************************
  Class ObjectExplorerNode
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace PragmaSQL.Core
{
  public class ObjectExplorerNode
  {
    public int Type = -1;
    public long id = -1;
    public string Name = String.Empty;
    
    public string ServerName = String.Empty;
    public string DatabaseName = String.Empty;
    public long DbId = -1;

    public string ParentName = String.Empty;
    public int ParentType = -1;
    public string Owner = String.Empty;
    public TreeNode Node = null;

    public short DbCompatibilityLevel = -1;

    private ConnectionParams _cp = null;
    public ConnectionParams ConnParams
    {
      get
      {
        return _cp;
      }
      set
      {
        _cp = value != null ? value.CreateCopy():null;
      }
    }

    public override string ToString( )
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("ID: " + id.ToString());
      sb.AppendLine("Name: " + Name);
      sb.AppendLine("Type: " + Type.ToString());
      sb.AppendLine("DatabaseName: " + DatabaseName);
      sb.AppendLine("DatabaseID: " + DbId);
      sb.AppendLine("ServerName: " + ServerName);

      sb.AppendLine("ParentName: " + ParentName);
      sb.AppendLine("ParentType: " + ParentType.ToString());
      return sb.ToString();
    }
  }
}
