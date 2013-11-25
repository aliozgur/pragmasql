/********************************************************************
  Class      : SqlSelectLocation
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using com.calitha.goldparser;

namespace PragmaSQL.Core
{
  public class SqlShBoundary
  {
		private bool _doNotPaint = false;
		public bool DoNotPaint
		{
			get { return _doNotPaint; }
			set { _doNotPaint = value; }
		}

		private bool _doNotFold = false;
		public bool DoNotFold
		{
			get { return _doNotFold; }
			set { _doNotFold = value; }
		}

    private int _startOffset = 0;
    public int StartOffset
    {
      get { return _startOffset; }
    }

    private int _endOffset = 0;

    public int EndOffset
    {
      get { return _endOffset; }
    }

    private int _startLine = 0;
    public int StartLine
    {
      get { return _startLine; }
    }

    private int _endLine = 0;
    public int EndLine
    {
      get { return _endLine; }
    }

    private int _startCol = 0;
    public int StartCol
    {
      get { return _startCol; }
    }

    private int _endCol = 0;
    public int EndCol
    {
      get { return _endCol; }
    }

    public string Text = String.Empty;

    public Location StartAsLocation
    {
      get
      {
        return new Location(_startOffset,_startLine,_startCol);
      }
      set
      {
        if(value != null)
        {
          _startOffset = value.Position;
          _startLine = value.LineNr;
          _startCol = value.ColumnNr;
        }
        else
        {
          _startOffset = 0;
          _startLine = 0;
          _startCol = 0;        
        }
      }
    }
    
    public Location EndAsLocation
    {
      get
      {
        return new Location(_endOffset,_endLine,_endCol);
      }
      set
      {
        if(value != null)
        {
          _endOffset = value.Position;
          _endLine = value.LineNr;
          _endCol = value.ColumnNr;
        }
        else
        {
          _endOffset = 0;
          _endLine = 0;
          _endCol = 0;        
        }
      }
    }

    public SqlShBoundary()
    {
    }
    
    public SqlShBoundary(SqlShParserLocation start, SqlShParserLocation end)
    {
      _startOffset = start.Position;
      _endOffset = end.Position;
      
      _startLine = start.LineNr;
      _endLine = end.LineNr;

      _startCol = start.ColumnNr;
      _endCol = end.ColumnNr;

      start.Boundary = this;
      end.Boundary = this;
    }

    
  }
}
