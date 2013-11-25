using System;
using System.Collections.Generic;
using System.Text;

namespace SQLManagement
{
  public class CancelledByUserException : Exception
  {
    public CancelledByUserException(string message)
      : base(message) { }
  }
}
