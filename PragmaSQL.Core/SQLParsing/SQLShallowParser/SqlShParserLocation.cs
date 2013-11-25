/********************************************************************
  Class      : SqlSelectLocation
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using com.calitha.goldparser;

namespace PragmaSQL.Core
{
  public struct SqlShParserLocation
  {
    /*
    public int LineNr = 0;
    public int ColumnNr = 0;
    public int Position = 0;
    */
    
    public int LineNr ;
    public int ColumnNr ;
    public int Position ;

    public SqlStatement Statement ;
    public SqlShBoundary Boundary ;

    /*
    public SqlStatement Statement = null;
    public SqlShBoundary Boundary = null;
    */

    public bool IsNullParserLocation
    {
      get
      {
        return LineNr == -1 && ColumnNr == -1 && Position == -1;
      }
      set
      {
        LineNr = -1;
        ColumnNr = -1;
        Position = -1;
      }
    }

    public SqlShParserLocation(int lineNr, int columnNr, int position)
    {
      LineNr = lineNr;
      ColumnNr = columnNr;
      Position = position;
      Statement = null;
      Boundary = null;
    }

    public static SqlShParserLocation NullParserLocation()
    {
      return new SqlShParserLocation(-1, -1, -1);
    }

    public static SqlShParserLocation CreateCopyFromParserLocation(Location source, int offsetCorrection)
    {
      SqlShParserLocation result = new SqlShParserLocation();
      result.LineNr = source.LineNr;
      result.ColumnNr = source.ColumnNr;
      result.Position = source.Position + offsetCorrection;

      return result;
    }

    public static SqlShParserLocation CreateCopyFromParserLocation(Location source)
    {
      return SqlShParserLocation.CreateCopyFromParserLocation(source,0);
    }
  }

  public class LocationOffsetComparer:IComparer
  {
    public int Compare(Object x,	Object y)
    {
      SqlShParserLocation lx = (SqlShParserLocation)x ;
      SqlShParserLocation ly = (SqlShParserLocation)y;
      return lx.Position.CompareTo(ly.Position);
    }

  }

	public class LocationLineComparer:IComparer
  {
		public int Compare(Object x,	Object y)
    {
      SqlShParserLocation lx = (SqlShParserLocation)x ;
      SqlShParserLocation ly = (SqlShParserLocation)y ;
      
      if(( lx.LineNr == ly.LineNr ) && (lx.Statement != null && ly.Statement != null) )
      {
        if(lx.Statement.Type == ly.Statement.Type)
        {
          return lx.Position.CompareTo(ly.Position);
        }
				else if (lx.Statement.Type == SqlShParserStatmentType.Select 
					&& ly.Statement.Type == SqlShParserStatmentType.Case)
        {
          return 1;
        }
				else if (ly.Statement.Type == SqlShParserStatmentType.Select 
					&& lx.Statement.Type == SqlShParserStatmentType.Case)
        {
          return -1;
        }
        else
        {
          return lx.Position.CompareTo(ly.Position);        
        }
      }
      else
      {
        return lx.Position.CompareTo(ly.Position);
      }
    }

  }
}
