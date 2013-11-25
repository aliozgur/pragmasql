/********************************************************************
  Interface  : IConfigurationContentItem
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public interface IConfigContentEditor
  {
    bool Modified
    {
      get;
    }

    bool ContentLoaded
    {
      get;
    }

    string ItemClassName
    {
      get;
    }

    bool LoadContent();
    bool SaveContent();
    void ShowContent();
    void HideContent();
  }
}
