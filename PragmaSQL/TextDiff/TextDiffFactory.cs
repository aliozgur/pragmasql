/********************************************************************
  Class TextDiffFactory
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL
{
  public static class TextDiffFactory
  {
    public static frmTextDiff CreateDiff()
    {
      frmTextDiff frm = new frmTextDiff();
      return frm;
    }
    
    public static frmTextDiff CreateDiffFromText(string caption,string source, string destination)
    {
      frmTextDiff frm = CreateDiff();
      if(!String.IsNullOrEmpty(caption))
      {
        frm.Text = caption;
      }

      frm.diffControl.SourceText = source;
      frm.diffControl.DestText = destination;
      return frm;
    }

    public static frmTextDiff CreateDiffFromFile(string caption, string sourceFileName, string destFileName)
    {
      frmTextDiff frm = CreateDiff();
      if(!String.IsNullOrEmpty(caption))
      {
        frm.Text = caption;
      }

      frm.diffControl.OpenSourceFile(sourceFileName);
      frm.diffControl.OpenDestFile(destFileName);
      return frm;
    }
  }
}
