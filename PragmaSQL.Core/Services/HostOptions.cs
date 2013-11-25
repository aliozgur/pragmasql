/********************************************************************
  Class UserOptions
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
  public class HostOptions
  {
    public ConnectionParams SysDbConnectionParams = null;
    public bool SysUtilsDisabled = false;
		public string UserAppDataFolder
		{
			get
			{
				return System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\PragmaSQL\\";
			}
		}
	}
}
