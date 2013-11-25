/********************************************************************
  Class FontOptions
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PragmaSQL.Core
{
  [Serializable]
  public class FontOptions
  {
    private string _name;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    private float _emSize;
    public float Size
    {
      get { return _emSize; }
      set { _emSize = value; }
    }

    private FontStyle _style = FontStyle.Regular;
    public FontStyle Style
    {
      get { return _style; }
      set { _style = value; }
    }

    public FontOptions( )
    {
      _name = "Courier New";
      _emSize = 10;
    }
   
    public FontOptions( Font f )
    {
      _name = f.Name;
      _emSize = f.Size;
      _style = f.Style;
    }

    public Font CreateFont( )
    {
      return new Font(_name, _emSize,_style);
    }
  }
}
