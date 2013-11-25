/********************************************************************
  Class      : Help2Settings
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Ali Özgür - 2007
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
  public class Help2Settings
  {
    private static string defDocumentExplorerPath = @"C:\Program Files\Common Files\Microsoft Shared\Help 8\dexplore.exe";
    private static string defHelpCollection = @"ms-help://MS.SQLCC.v9";
    private static string defFilter = "SQL Server 2005";

    #region Properties
    
    public string DocumentExplorerPath = defDocumentExplorerPath;
    public string HelpCollection = defHelpCollection;
    public string Filter = defFilter;
    
    #endregion //Properties

    #region Methods
    public void ResetDefaults()
    {
      DocumentExplorerPath = defDocumentExplorerPath;
      HelpCollection = defHelpCollection;
      Filter = defFilter;    
    }

    public Help2Settings CreateCopy()
    {
      Help2Settings result = new Help2Settings();
      result.DocumentExplorerPath = DocumentExplorerPath;
      result.HelpCollection = HelpCollection;
      result.Filter = Filter; 
   
      return result;
    }

    public void CopyFrom(Help2Settings source)
    {
      this.DocumentExplorerPath = source.DocumentExplorerPath;
      this.HelpCollection = source.HelpCollection;
      this.Filter = source.Filter;    
    }

    #endregion
  }
}
