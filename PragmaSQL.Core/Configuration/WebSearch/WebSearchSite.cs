/********************************************************************
  Class WebSearchSite
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class WebSearchSite
  {
    private string _name = String.Empty;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    private string _url = String.Empty;
    public string Url
    {
      get { return _url; }
      set { _url = value; }
    }

    public WebSearchSite Copy( )
    {
      WebSearchSite result = new WebSearchSite();
      result._name = this._name;
      result._url= this._url;
      return result;
    }

    public bool EqualsTo( WebSearchSite site )
    {
      if (site == null)
      {
        return false;
      }

      return ( _name.Trim().ToLowerInvariant() == site.Name.Trim().ToLowerInvariant() && _url.Trim().ToLowerInvariant() == site.Url.Trim().ToLowerInvariant() );
    }
  }
}
