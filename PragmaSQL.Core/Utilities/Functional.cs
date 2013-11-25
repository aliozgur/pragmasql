/********************************************************************
  Class Functional
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public delegate void ActionF( );
  public delegate void ActionF<A, B>(A arg1, B arg2);
  public delegate void ActionF<A, B, C>( A arg1, B arg2, C arg3 );
  public delegate R Func<R>( );
  public delegate R Func<A, R>( A arg1 );
  public delegate R Func<A, B, R>( A arg1, B arg2 );
  public delegate R Func<A, B, C, R>( A arg1, B arg2, C arg3 );
	
}
