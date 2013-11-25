using System;
using System.Data;

namespace PragmaSQL.Core
{

  public abstract class DataSetConverter
  {
    public abstract object Convert(DataSet dataset);
    public abstract object Convert(DataTable table);
  }
}
