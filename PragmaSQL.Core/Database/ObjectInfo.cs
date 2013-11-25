/********************************************************************
  Class      : ObjectInfo
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;

namespace PragmaSQL.Core
{
  public class ObjectInfo
  {
    public int ObjectID = -1;
    public int ObjectType = DBObjectType.None;
    public string ObjectTypeAbb = String.Empty;
    public string ObjectName = String.Empty;
		public string ObjectOwner = String.Empty;

    public string FullNameQuoted
    {
      get 
      {
        return String.Format("[{0}].[{1}]", ObjectOwner, ObjectName);
      }
    }

    public string FullName
    {
      get
      {
        return String.Format("{0}.{1}", ObjectOwner, ObjectName);
      }
    }

    public void CopyFrom(ObjectInfo source)
    {
      this.ObjectID = source.ObjectID;
      this.ObjectType = source.ObjectType;
      this.ObjectName = source.ObjectName;
      this.ObjectTypeAbb = source.ObjectTypeAbb;
			this.ObjectOwner = source.ObjectOwner;
		}

    public bool HoldsData
    {
      get
      {
        return DBConstants.DoesObjectTypeHoldsData(ObjectTypeAbb);
      }
    }
    
    public bool HasScript
    {
      get
      {
        return DBConstants.DoesObjectTypeHasScript(ObjectTypeAbb);
      }
    }

    public override string ToString( )
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("ObjectID: " + ObjectID.ToString());
      sb.AppendLine("ObjectType: " + ObjectType.ToString());
      sb.AppendLine("ObjectTypeAbb: " + ObjectTypeAbb);
      sb.AppendLine("ObjectName: " + ObjectName);
			sb.AppendLine("ObjectOwner: " + ObjectOwner);
			sb.AppendLine("HoldsData: " + HoldsData.ToString());
      sb.AppendLine("HasScript: " + HasScript.ToString());

      return sb.ToString();
    }
  }
}
