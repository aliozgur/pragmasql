using System;
using System.Collections.Generic;
using System.Text;

namespace SQLManagement
{
  public class DataTypeWrap
  {
    
    private string _name;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    private string _width;
    public string Width
    {
      get { return _width; }
      set { _width = value; }
    }


    private string _precision;
    public string Precision
    {
      get { return _precision; }
      set { _precision = value; }
    }

    private string _scale;
    public string Scale
    {
      get { return _scale; }
      set { _scale = value; }
    }
  }
}
