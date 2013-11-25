/********************************************************************
  Class      : ReplaceEventArgs
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com
  
  Copyright: Istanbul Bilgi University
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PragmaSQL.GUI
{
  public class ReplaceEventArgs : EventArgs
  {
    private string _replaceText;
    public string ReplaceText
    {
      get { return _replaceText; }
      set { _replaceText = value; }
    }


    private Regex _searchRegularExpression;
    public Regex SearchRegularExpression
    {
      get { return _searchRegularExpression; }
    }

    private bool _isReplaceAll = false;
    public bool IsReplaceAll
    {
      get { return _isReplaceAll; }
      set { _isReplaceAll = value; }
    }

    public ReplaceEventArgs(Regex searchRegularExpression,string replaceText, bool isReplaceAll)
    {
      _searchRegularExpression = searchRegularExpression;
      _replaceText = replaceText;
      _isReplaceAll = isReplaceAll;
    }
  }
}
