using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using com.calitha.goldparser;

using PragmaSQL.Proxy;

namespace PragmaSQL
{
  public static class GoldTokenizer
  {
    private static StringTokenizer _tokenizer = null;
    public static StringTokenizer Tokenizer
    {
      get { return _tokenizer; }
    }

    private static void InitializeGoldParserEngine()
    {
      if (_tokenizer != null)
      {
        _tokenizer = null;
      }

			Stream file = PragmaSQLApp.GetCompiledSqlGrammarStream();
      CGTReader reader = new CGTReader(file);
      _tokenizer = reader.CreateNewTokenizer();
    }

    static GoldTokenizer()
    {
      InitializeGoldParserEngine();
    }
  }
}
