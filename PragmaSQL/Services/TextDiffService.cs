/********************************************************************
  Class TextDiffService
  Created On: $datetime$
  Created by: Ali Özgür
  Contact: ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

using PragmaSQL.Core;

namespace PragmaSQL
{
  public class TextDiffService:ITextDiffService
  {

    #region ITextDiffService Members

    public void DiffText( string caption, string source, string destination )
    {
      frmTextDiff frm = TextDiffFactory.CreateDiffFromText(caption, source, destination);
      frm.Show();
    }

    public void DiffText( string source, string destination )
    {
      DiffText(string.Empty, source, destination);
    }

    public void DiffFile( string caption, string sourceFileName, string destinationFileName )
    {
      frmTextDiff frm = TextDiffFactory.CreateDiffFromFile(caption, sourceFileName, destinationFileName);
      frm.Show();
    }

    public void DiffFile( string sourceFileName, string destinationFileName )
    {
      DiffFile(String.Empty, sourceFileName, destinationFileName);
    }

    #endregion
  }
}
